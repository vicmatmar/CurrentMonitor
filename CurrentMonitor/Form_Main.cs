using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO.Ports;

namespace CurrentMonitor
{
    public partial class Form_Main : Form
    {
        ee203 _ee203;
        SerialPort _cmd_port;
        SerialPort _data_port;

        double _volatge_exp_max;
        double _volatge_exp_min;
        double _volatge_act_max = Double.NaN;
        double _volatge_act_min = Double.NaN;
        double _current_act_max = Double.NaN;
        double _current_act_min = Double.NaN;

        bool _sleep_detected = false;
        TimeSpan _sleep_start = TimeSpan.MaxValue;

        Task _task_openPorts;

        enum States { No_Power, Bad_Power, No_Device, Sleep, On, Other };
        States _last_state = States.Other;

        delegate void setControlPropertyValueCallback(Control control, object value, string property_name);
        delegate void updateMeasurementsCallback(string voltage_str, string current_str);

        public Form_Main()
        {
            InitializeComponent();

            //string[] ports = ee203.GetComPortNames("USB Serial Device");

            double value = Properties.Settings.Default.Voltage_Value;
            double tolarance = Properties.Settings.Default.Voltage_Tolarance;
            _volatge_exp_max = value + tolarance * value / 100;
            _volatge_exp_min = value - tolarance * value / 100;

            _ee203 = new ee203(
                cmd_port_name: Properties.Settings.Default.Cmd_Port_Name,
                data_port_name: Properties.Settings.Default.Data_Port_Name);
            _ee203.DataPort_Data_Event += _ee203_DataPort_Data_Event;

        }

        private void _ee203_DataPort_Data_Event(object sender, string data)
        {
            string[] lines = data.Split(new char[] { '\r' });
            foreach (string line in lines)
            {
                string[] cells = line.Split(new char[] { ',' });
                if (cells.Length == 7)
                {
                    try
                    {
                        processData(cells);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
            }
        }

        private void controllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Controller ctrlform = new Form_Controller(_ee203);
            ctrlform.ShowDialog();
        }

        void closePorts()
        {
            _ee203.ClosePorts();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Save the port settings to see whether they changed
            string cmdPortName = Properties.Settings.Default.Cmd_Port_Name;
            string dataPortName = Properties.Settings.Default.Data_Port_Name;

            Form_Settings dlg = new Form_Settings();
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                Properties.Settings.Default.Reload();

                if (cmdPortName != Properties.Settings.Default.Cmd_Port_Name ||
                   dataPortName != Properties.Settings.Default.Data_Port_Name)
                {
                    _ee203.DataPort_Data_Event -= _ee203_DataPort_Data_Event;

                    Task close_ports = new Task(() => closePorts());
                    close_ports.Start();

                    _ee203 = new ee203(
                        cmd_port_name: Properties.Settings.Default.Cmd_Port_Name,
                        data_port_name: Properties.Settings.Default.Data_Port_Name);
                    _ee203.DataPort_Data_Event += _ee203_DataPort_Data_Event;

                    _task_openPorts = new Task(() => openPorts());
                    _task_openPorts.Start();
                }
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            label_i_act.Text = "";
            label_i_max.Text = "";
            label_i_min.Text = "";

            label_v_act.Text = "";
            label_v_max.Text = "";
            label_v_min.Text = "";

            label_results.Text = "";

            Task _task_openPorts = new Task(() => openPorts());
            _task_openPorts.Start();
        }

        bool arePortsOpen()
        {
            return (_cmd_port != null && _data_port != null && _cmd_port.IsOpen && _data_port.IsOpen);
        }

        void openPorts()
        {
            while (true)
            {
                try
                {
                    _cmd_port = _ee203.OpenCmdPort();
                    Thread.Sleep(500);
                    if (_cmd_port.IsOpen)
                        break;
                }
                catch (Exception ex)
                {
                    DialogResult res = MessageBox.Show(
                        string.Format("{0}\r\nPlease reset ee203 device", ex.Message),
                        string.Format("Open port {0} error", _ee203.CMD_Port_Name),
                        MessageBoxButtons.RetryCancel);
                    if (res == DialogResult.Cancel)
                    {
                        break;
                    }
                }
            }


            if (_cmd_port != null && _cmd_port.IsOpen)
            {
                while (true)
                {
                    try
                    {
                        _ee203.Pause();
                        _ee203.Zero();

                        _data_port = _ee203.OpenDataPort();

                        if (_data_port.IsOpen)
                        {
                            _data_port.ReadExisting();
                            Thread.Sleep(200);

                            _ee203.Interval(ee203.Sampling.Medium);
                            Thread.Sleep(200);
                            //_ee203.Interval(ee203.Sampling.Fast);
                            //_ee203.Interval(ee203.Sampling.Fastest);

                            _ee203.Resume();

                            break;
                        }

                    }
                    catch (Exception ex)
                    {
                        DialogResult res = MessageBox.Show(
                            string.Format("{0}", ex.Message),
                            string.Format("{0}", "Error opening ee203 data port"),
                            MessageBoxButtons.RetryCancel);
                        if (res == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                }
            }
        }

        void processData(string[] data)
        {
            string timestamp_str = data[0];
            double voltage = Convert.ToDouble(data[2]);
            double current = Convert.ToDouble(data[3]);
            measurementsDisplay(timestamp_str, voltage, current);

            // Checks
            States state = getState(voltage: voltage, current: current);
            Color forcolor = Color.Black;
            string text = "Testing...";
            if (state == States.No_Power)
            {
                forcolor = Color.Red;
                text = "No voltage detected.  Is power supply on and connected?";
            }
            else if (state == States.Bad_Power)
            {
                forcolor = Color.Red;
                text = "Volatge out of range.  Please adjust power supply";
            }
            else if (state == States.No_Device)
            {
                if (_sleep_detected)
                {
                    syncLabelSetText(label_results, "");
                }

                forcolor = Color.Blue;
                text = "Device not detected";
            }
            else if (state == States.Sleep)
            {
                TimeSpan time = ee203.DateTimeParse(data[0]);
                if (_sleep_start == TimeSpan.MaxValue)
                {
                    _sleep_start = time;
                }

                TimeSpan etime = time - _sleep_start;
                if (etime > new TimeSpan(0, 0, 1))
                {
                    _sleep_detected = true;

                    forcolor = Color.Green;
                    text = string.Format("Sleep current detected: {0:hh\\:mm\\:ss}", etime);

                    syncLabelSetTextAndColor(label_results, "PASS", Color.Green);
                }

            }
            else if (state == States.On)
            {
                forcolor = Color.Red;
                text = "High current detected";
            }
            syncLabelSetTextAndColor(label_dev_status, text, forcolor);

            if (state != States.Sleep)
            {
                _sleep_start = TimeSpan.MaxValue;
            }

            if (_last_state == States.No_Device && state != States.Sleep && state != States.No_Device)
            {
                _ee203.Zero();
            }
            _last_state = state;
        }

        void measurementsDisplay(string timestamp_str, double voltage, double current)
        {
            if (!groupBox1.Visible)
                return;

            syncLabelSetText(label_timestamp, timestamp_str);

            // Voltage
            if (Double.IsNaN(_volatge_act_max)) _volatge_act_max = voltage;
            else if (voltage > _volatge_act_max) _volatge_act_max = voltage;
            if (Double.IsNaN(_volatge_act_min)) _volatge_act_min = voltage;
            else if (voltage < _volatge_act_min) _volatge_act_min = voltage;

            Color forcolor = Color.Green;
            if (voltage > _volatge_exp_max || voltage < _volatge_exp_min)
                forcolor = Color.Red;
            string text = string.Format("{0:F3} V", voltage);

            syncLabelSetTextAndColor(label_v_act, text, forcolor);
            syncLabelSetText(label_v_max, string.Format("{0:F3} V", _volatge_act_max));
            syncLabelSetText(label_v_min, string.Format("{0:F3} V", _volatge_act_min));

            // Current
            if (Double.IsNaN(_current_act_max)) _current_act_max = current;
            else if (current > _current_act_max) _current_act_max = current;

            if (Double.IsNaN(_current_act_min)) _current_act_min = current;
            else if (current < _current_act_min) _current_act_min = current;

            syncLabelSetText(label_i_act, Utils.ToSIPrefixedString(current) + "A");
            syncLabelSetText(label_i_max, Utils.ToSIPrefixedString(_current_act_max) + "A");
            syncLabelSetText(label_i_min, Utils.ToSIPrefixedString(_current_act_min) + "A");

        }

        States getState(double voltage, double current)
        {
            double nodi = Properties.Settings.Default.Current_NoDevice_Threshold;
            States state = States.Other;
            if (voltage <= Properties.Settings.Default.Voltage_Off_Threshold)
            {
                state = States.No_Power;
            }
            else if (voltage > _volatge_exp_max || voltage < _volatge_exp_min)
            {
                state = States.Bad_Power;
            }
            else if (current < Properties.Settings.Default.Current_NoDevice_Threshold)
            {
                state = States.No_Device;
            }
            else if (current < Properties.Settings.Default.Current_Sleep_Threshold)
            {
                state = States.Sleep;
            }
            else if (current > Properties.Settings.Default.Current_High_Threshold)
            {
                state = States.On;
            }

            return state;
        }

        void syncLabelSetText(Label control, string text)
        {
            synchronizedInvoke(control,
                delegate ()
                {
                    control.Text = text;
                });
        }

        void syncLabelSetTextAndColor(Label control, string text, Color forcolor)
        {
            synchronizedInvoke(control,
                delegate ()
                {
                    control.Text = text;
                    control.ForeColor = forcolor;
                });
        }


        void synchronizedInvoke(ISynchronizeInvoke sync, Action action)
        {
            // If the invoke is not required, then invoke here and get out.
            if (!sync.InvokeRequired)
            {
                // Execute action.
                action();

                // Get out.
                return;
            }

            try
            {
                // Marshal to the required context.
                sync.Invoke(action, new object[] { });
                //sync.BeginInvoke(action, new object[] { });
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        void controlSetProperty(Control control, object value, string property_name = "Text")
        {
            if (control.InvokeRequired)
            {
                setControlPropertyValueCallback d = new setControlPropertyValueCallback(controlSetProperty);
                this.Invoke(d, new object[] { control, value, property_name });
            }
            else
            {
                var property = control.GetType().GetProperty(property_name);
                if (property != null)
                {
                    property.SetValue(control, value);
                }
            }
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Task close_data_port_task = new Task(() => closePorts());
            try
            {
                //close_data_port_task.Start();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        private void button_reset_max_min_Click(object sender, EventArgs e)
        {
            _volatge_act_max = Double.NaN;
            _volatge_act_min = Double.NaN;
            _current_act_max = Double.NaN;
            _current_act_min = Double.NaN;

            if (_cmd_port != null && _cmd_port.IsOpen)
                _ee203.Zero();
            _last_state = States.Other;
            label_results.Text = "";
        }

        private void showMesurementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
            if (groupBox1.Visible)
                showMesurementsToolStripMenuItem.Text = "Hide Measurement";
            else
                showMesurementsToolStripMenuItem.Text = "Show Measurement";

        }
    }
}

