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

using System.IO.Ports;

namespace CurrentMonitor
{
    public partial class Form_Main : Form
    {
        ee203 _ee203;
        SerialPort _cmd_port;
        SerialPort _data_port;

        double _volatge_max;
        double _volatge_min;


        delegate void setControlPropertyValueCallback(Control control, object value, string property_name);
        delegate void updateMeasurementsCallback(string voltage_str, string current_str);

        public Form_Main()
        {
            InitializeComponent();

            double value = Properties.Settings.Default.Voltage_Value;
            double tolarance = Properties.Settings.Default.Voltage_Tolarance;
            _volatge_max = value + tolarance * value / 100;
            _volatge_min = value - tolarance * value / 100;
        }

        private void controllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closePorts();

            Form_Controller ctrlform = new Form_Controller();
            ctrlform.ShowDialog();

            openPorts();
        }

        void closePorts()
        {
            _cmd_port.Close();
            _data_port.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Settings dlg = new Form_Settings();
            DialogResult res = dlg.ShowDialog();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            label_current.Text = "";
            label_voltage.Text = "";

            openPorts();
        }

        void openPorts()
        {
            _ee203 = new ee203();
            _ee203.CMD_Port_Name = Properties.Settings.Default.Cmd_Port_Name;

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
                _ee203.Pause();
                _ee203.Zero();

                _ee203.DATA_Port_Name = Properties.Settings.Default.Data_Port_Name;
                _data_port = _ee203.OpenDataPort();
                _data_port.DataReceived += _data_port_DataReceived;

                _ee203.Interval(ee203.Sampling.Medium);
                //_ee203.Interval(ee203.Sampling.Fast);
                //_ee203.Interval(ee203.Sampling.Fastest);

                _ee203.Resume();
            }
        }

        private void _data_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            string data = _data_port.ReadExisting();
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

        void processData(string[] data)
        {
            //string[] ts1 = cells[0].Split(new char[] { ':' });
            //string[] ts2 = ts1[1].Split(new char[] { '.' });
            //TimeSpan timestamp = new TimeSpan(0, 0, Convert.ToInt32(ts1[0]), Convert.ToInt32(ts2[0]), Convert.ToInt32(ts2[1]));

            //updateMeasurements(voltage_str: cells[2], current_str: cells[3]);

            // Voltage
            double voltage = Convert.ToDouble(data[2]);
            Color forcolor = Color.Green;
            if (voltage > _volatge_max || voltage < _volatge_min)
                forcolor = Color.Red;
            string text = string.Format("{0:F3} V", voltage);

            SynchronizedInvoke(label_voltage, delegate () { label_voltage.ForeColor = forcolor; });
            SynchronizedInvoke(label_voltage, delegate () { label_voltage.Text = text; });

            // Current
            string current_str = data[3];
            string[] current_parts = current_str.Split(new char[] { 'e' });
            string si = "";


            if (current_parts.Length > 1)
            {
                switch (current_parts[1])
                {
                    case "-03":
                        si = "mA";
                        break;
                    case "-06":
                        si = "uA";
                        break;
                    case "-09":
                        si = "nA";
                        break;
                    case "-12":
                        si = "p";
                        break;
                    case "-15":
                        si = "f";
                        break;
                }
            }

            if (si != "")
            {
                text = current_parts[0] + " " + si;

            }
            else
            {
                text = current_str;
            }
            SynchronizedInvoke(label_current, delegate () { label_current.Text = text; });

            // Checks
            text = "Testing...";
            forcolor = Color.Black;
            if (voltage <= 0.001)
            {
                forcolor = Color.Red;
                text = "No voltage detected.  Is power supply on and connected?";
            }
            SynchronizedInvoke(label_dev_status, delegate () { label_dev_status.ForeColor = forcolor; });
            SynchronizedInvoke(label_dev_status, delegate () { label_dev_status.Text = text; });

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

            // Marshal to the required context.
            sync.Invoke(action, new object[] { });
        }

        void updateMeasurements(string voltage_str, string current_str)
        {
            if (label_current.InvokeRequired || label_voltage.InvokeRequired || label_dev_status.InvokeRequired)
            {
                updateMeasurementsCallback d = new updateMeasurementsCallback(updateMeasurements);
                this.Invoke(d, new object[] { voltage_str, current_str });
            }
            else
            {
                try
                {
                    double voltage = Convert.ToDouble(voltage_str);
                    if (voltage > _volatge_max || voltage < _volatge_min)
                        label_voltage.ForeColor = Color.Red;
                    else
                        label_voltage.ForeColor = Color.Green;

                    label_voltage.Text = string.Format("{0:F3} V", voltage);

                    //label_current.Text = string.Format("{0:G2}", current);
                    string[] current_parts = current_str.Split(new char[] { 'e' });
                    string si = "";
                    if (current_parts.Length > 1)
                    {
                        switch (current_parts[1])
                        {
                            case "-03":
                                si = "mA";
                                break;
                            case "-06":
                                si = "uA";
                                break;
                            case "-09":
                                si = "nA";
                                break;
                            case "-12":
                                si = "p";
                                break;
                            case "-15":
                                si = "f";
                                break;
                        }
                    }

                    if (si != "")
                    {
                        label_current.Text = current_parts[0] + " " + si; ;

                    }
                    else
                    {
                        label_current.Text = current_str;
                    }

                    if (voltage <= 0.001)
                    {
                        label_dev_status.ForeColor = Color.Red;
                        label_dev_status.Text = "No voltage detected.  Is power supply on and connected?";
                    }
                }
                catch (FormatException)
                {

                }

            }

        }

        /// <summary>
        /// converts the value into a string with SI prefix
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>si prefixed string</returns>
        public string ToSIPrefixedString(double value)
        {
            string stringValue = value.ToString("#E+00");
            string[] stringValueParts = stringValue.Split("E".ToCharArray());
            int mantissa = Convert.ToInt32(stringValueParts[0]);
            int exponent = Convert.ToInt32(stringValueParts[1]);
            while (exponent % 3 != 0)
            {
                mantissa *= 10;
                exponent -= 1;
            }

            string prefixedValue = mantissa.ToString();
            switch (exponent)
            {
                case 24:
                    prefixedValue += "Y";
                    break;
                case 21:
                    prefixedValue += "Z";
                    break;
                case 18:
                    prefixedValue += "E";
                    break;
                case 15:
                    prefixedValue += "P";
                    break;
                case 12:
                    prefixedValue += "T";
                    break;
                case 9:
                    prefixedValue += "G";
                    break;
                case 6:
                    prefixedValue += "M";
                    break;
                case 3:
                    prefixedValue += "k";
                    break;
                case 0:
                    break;
                case -3:
                    prefixedValue += "m";
                    break;
                case -6:
                    prefixedValue += "u";
                    break;
                case -9:
                    prefixedValue += "n";
                    break;
                case -12:
                    prefixedValue += "p";
                    break;
                case -15:
                    prefixedValue += "f";
                    break;
                case -18:
                    prefixedValue += "a";
                    break;
                case -21:
                    prefixedValue += "z";
                    break;
                case -24:
                    prefixedValue += "y";
                    break;
                default:
                    prefixedValue = "invalid";
                    break;
            }

            return prefixedValue;
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
            try
            {
                Task close_data_port_task = new Task(() => closePorts());
                close_data_port_task.Start();
            }
            catch { }
        }
    }
}

