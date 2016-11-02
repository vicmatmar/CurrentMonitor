using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        delegate void updateMeasurementsCallback(double voltage, double current);

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
            if (_cmd_port.IsOpen)
                _cmd_port.Close();
            if (_data_port.IsOpen)
                _data_port.Close();

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Settings dlg = new Form_Settings();
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {

            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
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
            try
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
                            //string[] ts1 = cells[0].Split(new char[] { ':' });
                            //string[] ts2 = ts1[1].Split(new char[] { '.' });
                            //TimeSpan timestamp = new TimeSpan(0, 0, Convert.ToInt32(ts1[0]), Convert.ToInt32(ts2[0]), Convert.ToInt32(ts2[1]));


                            double voltage = Convert.ToDouble(cells[2]);
                            double current = Convert.ToDouble(cells[3]);

                            updateMeasurements(voltage, current);

                        }
                        catch { }

                    }
                }

            }
            catch { }
        }

        void updateMeasurements(double voltage, double current)
        {
            if (label_current.InvokeRequired || label_voltage.InvokeRequired)
            {
                updateMeasurementsCallback d = new updateMeasurementsCallback(updateMeasurements);
                this.Invoke(d, new object[] { voltage, current });
            }
            else
            {
                if (voltage > _volatge_max || voltage < _volatge_min)
                    label_voltage.ForeColor = Color.Red;
                else
                    label_voltage.ForeColor = Color.Green;

                label_voltage.Text = string.Format("{0:F2}", voltage);

                label_current.Text = string.Format("{0:G2}", current);
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
