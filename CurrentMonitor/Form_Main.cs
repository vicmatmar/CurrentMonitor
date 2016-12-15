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

        bool _device_removed = true;
        bool _sleep_detected = false;
        TimeSpan _sleep_start = TimeSpan.MaxValue;

        delegate void setControlPropertyValueCallback(Control control, object value, string property_name);
        delegate void updateMeasurementsCallback(string voltage_str, string current_str);

        public Form_Main()
        {
            InitializeComponent();

            double value = Properties.Settings.Default.Voltage_Value;
            double tolarance = Properties.Settings.Default.Voltage_Tolarance;
            _volatge_exp_max = value + tolarance * value / 100;
            _volatge_exp_min = value - tolarance * value / 100;

            _ee203 = new ee203(
                cmd_port_name: Properties.Settings.Default.Cmd_Port_Name,
                data_port_name: Properties.Settings.Default.Data_Port_Name);

            //_ee203.CmdPort_Data_Event += _ee203_CmdPort_Data_Event;
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
                    toolStripStatusLabel1.Text = "";

                    try
                    {
                        processData(cells);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        toolStripStatusLabel1.Text = msg;
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
            Form_Settings dlg = new Form_Settings();
            DialogResult res = dlg.ShowDialog();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            label_i_act.Text = "";
            label_i_max.Text = "";
            label_i_min.Text = "";

            label_v_act.Text = "";
            label_v_max.Text = "";
            label_v_min.Text = "";

            toolStripStatusLabel1.Text = "";

            openPorts();
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

            if (_cmd_port.IsOpen)
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

                            _ee203.Interval(ee203.Sampling.Medium);
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
            //string[] ts1 = cells[0].Split(new char[] { ':' });
            //string[] ts2 = ts1[1].Split(new char[] { '.' });
            //TimeSpan timestamp = new TimeSpan(0, 0, Convert.ToInt32(ts1[0]), Convert.ToInt32(ts2[0]), Convert.ToInt32(ts2[1]));

            //updateMeasurements(voltage_str: cells[2], current_str: cells[3]);

            // Voltage
            double voltage = Convert.ToDouble(data[2]);
            if (Double.IsNaN(_volatge_act_max)) _volatge_act_max = voltage;
            else if (voltage > _volatge_act_max) _volatge_act_max = voltage;
            if (Double.IsNaN(_volatge_act_min)) _volatge_act_min = voltage;
            else if (voltage < _volatge_act_min) _volatge_act_min = voltage;

            Color forcolor = Color.Green;
            if (voltage > _volatge_exp_max || voltage < _volatge_exp_min)
                forcolor = Color.Red;
            string text = string.Format("{0:F3} V", voltage);
            SynchronizedInvoke(label_dev_status,
                delegate ()
                {
                    label_v_act.Text = text;
                    label_v_act.ForeColor = forcolor;
                });

            SynchronizedInvoke(label_v_act,
                delegate () { label_v_max.Text = string.Format("{0:F3} V", _volatge_act_max); });
            SynchronizedInvoke(label_v_act,
                delegate () { label_v_min.Text = string.Format("{0:F3} V", _volatge_act_min); });



            // Current
            double current = Convert.ToDouble(data[3]);

            if (Double.IsNaN(_current_act_max)) _current_act_max = current;
            else if (current > _current_act_max) _current_act_max = current;

            if (Double.IsNaN(_current_act_min)) _current_act_min = current;
            else if (current < _current_act_min) _current_act_min = current;

            SynchronizedInvoke(label_i_act,
                delegate () { label_i_act.Text = ToSIPrefixedString(current) + "A"; });
            SynchronizedInvoke(label_i_max,
                delegate () { label_i_max.Text = ToSIPrefixedString(_current_act_max) + "A"; });
            SynchronizedInvoke(label_i_min,
                delegate () { label_i_min.Text = ToSIPrefixedString(_current_act_min) + "A"; });

            // Checks
            forcolor = Color.Black;
            text = "Testing...";
            if (voltage <= Properties.Settings.Default.Voltage_Off_Threshold)
            {
                _sleep_start = TimeSpan.MaxValue;

                forcolor = Color.Red;
                text = "No voltage detected.  Is power supply on and connected?";
            }
            else if (current < Properties.Settings.Default.Current_NoDevice_Threshold)
            {
                _sleep_start = TimeSpan.MaxValue;

                if (_sleep_detected)
                    _device_removed = true;

                forcolor = Color.Blue;
                text = "Device not detected";

            }
            else if (current < 80e-6)
            {
                TimeSpan time = ee203.DateTimeParse(data[0]);
                if (_sleep_start == TimeSpan.MaxValue)
                {
                    _sleep_start = time;
                }

                TimeSpan etime = time - _sleep_start;
                if (etime > new TimeSpan(0, 0, 1))
                {
                    if(!_sleep_detected)
                        _device_removed = false;
                    _sleep_detected = true;

                    forcolor = Color.Green;
                    text = string.Format("Sleep current detected: {0:ss}", etime);
                }
            }
            else if (current > 10E-3)
            {
                _sleep_start = TimeSpan.MaxValue;
                if (!_sleep_detected)
                    _device_removed = false;

                forcolor = Color.Red;
                text = "High current detected";

            }
            else
            {
                _sleep_start = TimeSpan.MaxValue;
                if (!_sleep_detected)
                    _device_removed = false;

                forcolor = Color.Black;
                text = string.Format("Current = {0}", current);
            }

            SynchronizedInvoke(label_dev_status,
                delegate ()
                {
                    label_dev_status.Text = text;
                    label_dev_status.ForeColor = forcolor;
                });

        }

        void SynchronizedInvoke(ISynchronizeInvoke sync, Action action)
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

        /// <summary>
        /// converts the value into a string with SI prefix
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>si prefixed string</returns>
        public string ToSIPrefixedString(double d)
        {
            double exponent = Math.Log10(Math.Abs(d));
            if (Math.Abs(d) >= 1)
            {
                switch ((int)Math.Floor(exponent))
                {
                    case 0:
                    case 1:
                    case 2:
                        return string.Format("{0:00.00}", d);
                    case 3:
                    case 4:
                    case 5:
                        return string.Format("{0:00.00 k}", (d / 1e3));
                    case 6:
                    case 7:
                    case 8:
                        return string.Format("{0:00.00 M}", (d / 1e6));
                    case 9:
                    case 10:
                    case 11:
                        return string.Format("{0:00.00 G}", (d / 1e9));
                    case 12:
                    case 13:
                    case 14:
                        return string.Format("{0:00.00 T}", (d / 1e12));
                    case 15:
                    case 16:
                    case 17:
                        return string.Format("{0:00.00 P}", (d / 1e15));
                    case 18:
                    case 19:
                    case 20:
                        return string.Format("{0:00.00 E}", (d / 1e18));
                    case 21:
                    case 22:
                    case 23:
                        return string.Format("{0:00.00 Z}", (d / 1e21));
                    default:
                        return string.Format("{0:00.00 Y}", (d / 1e24));
                }
            }
            else if (Math.Abs(d) > 0)
            {
                switch ((int)Math.Floor(exponent))
                {
                    case -1:
                    case -2:
                    case -3:
                        return string.Format("{0:00.00 m}", (d * 1e3));
                    case -4:
                    case -5:
                    case -6:
                        return string.Format("{0:00.00 μ}", (d * 1e6));
                    case -7:
                    case -8:
                    case -9:
                        return string.Format("{0:00.00 n}", (d * 1e9));
                    case -10:
                    case -11:
                    case -12:
                        return string.Format("{0:00.00 p}", (d * 1e12));
                    case -13:
                    case -14:
                    case -15:
                        return string.Format("{0:00.00 f}", (d * 1e15));
                    case -16:
                    case -17:
                    case -18:
                        return string.Format("{0:00.00 a}", (d * 1e15));
                    case -19:
                    case -20:
                    case -21:
                        return string.Format("{0:00.00 z}", (d * 1e15));
                    default:
                        return string.Format("{0:00.00 y}", (d * 1e15));
                }
            }
            else
            {
                return "0";
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
        }

    }
}

