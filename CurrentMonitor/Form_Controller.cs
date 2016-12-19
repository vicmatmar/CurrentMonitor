using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Management;
using System.IO.Ports;
using System.Collections;
using System.Collections.Concurrent;

namespace CurrentMonitor
{
    public partial class Form_Controller : Form
    {
        delegate void setControlPropertyValueCallback(Control control, object value, string property_name);
        delegate void updateSerialPortsCallback();
        delegate void updateTextboxDataCallback(string data);
        delegate void updateChartCallback(string data);

        ee203 _ee203;
        public ee203 ee203 { get { return _ee203; } set { _ee203 = value; } }

        bool _isclosing = false;



        public Form_Controller(ee203 ee203)
        {
            _ee203 = ee203;

            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            init_chart();

            _ee203.Zero();
            _ee203.CmdPort_Data_Event += _ee203_CmdPort_Data_Event;

            _ee203.DataPort_Data_Event += _ee203_DataPort_Data_Event;


            /*
            startUSBWatcher();


            var port_names = SerialPort.GetPortNames();
            foreach(string port_name in port_names)
            {
                using (SerialPort port = new SerialPort(port_name))
                {
                    port.DataReceived += Port_DataReceived;
                    port.Open();
                    port.WriteLine("");
                }
            }
            */
        }

        private void _ee203_DataPort_Data_Event(object sender, string data)
        {
            update_chart(data);
            updateTextboxData(data);
        }

        private void _ee203_CmdPort_Data_Event(object sender, string data)
        {
            updateCommandData(data);
        }

        void updateTextboxData(string data)
        {
            if (textBox_serialData.InvokeRequired)
            {
                updateTextboxDataCallback d = new updateTextboxDataCallback(updateTextboxData);
                try
                {
                    if (!this.IsDisposed && !_isclosing)
                        this.Invoke(d, new object[] { data });
                }
                catch { }
            }
            else
            {
                if (textBox_serialData.Text.Length > 500000)
                {
                    textBox_serialData.Text = textBox_serialData.Text.Substring(500000 / 2);
                }

                data = data.Replace("\r", "\r\n");
                textBox_serialData.AppendText(data);
            }
        }

        void updateCommandData(string data)
        {
            if (textBox_serialCommand.InvokeRequired)
            {
                updateTextboxDataCallback d = new updateTextboxDataCallback(updateCommandData);
                try
                {
                    this.Invoke(d, new object[] { data });
                }
                catch { }
            }
            else
            {
                data = data.Replace("\r", "\r\n");
                textBox_serialCommand.AppendText(data);
            }
        }

        void update_chart(string data)
        {
            if (chart1.InvokeRequired)
            {
                updateChartCallback d = new updateChartCallback(update_chart);
                try
                {
                    this.Invoke(d, new object[] { data });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            else
            {
                string[] lines = data.Split(new char[] { '\r' });
                foreach (string line in lines)
                {
                    string[] cells = line.Split(new char[] { ',' });
                    if (cells.Length == 7)
                    {
                        try
                        {
                            string[] ts1 = cells[0].Split(new char[] { ':' });
                            string[] ts2 = ts1[1].Split(new char[] { '.' });
                            TimeSpan timestamp = new TimeSpan(0, 0, Convert.ToInt32(ts1[0]), Convert.ToInt32(ts2[0]), Convert.ToInt32(ts2[1]));

                            double current = Convert.ToDouble(cells[3]);
                            chart1.Series["Current"].Points.AddXY(timestamp.ToString(@"m\:s", System.Globalization.CultureInfo.InvariantCulture), current);

                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                    }
                }
            }
        }

        private void startUSBWatcher()
        {
            // Used to update available COM ports when devices are plugged/unplugged
            var watcher = new ManagementEventWatcher();
            watcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 or EventType = 3");
            watcher.EventArrived += Watcher_EventArrived;
            watcher.Start();
        }

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            //updateSerialPorts();
        }
/*
        void updateSerialPorts()
        {
            if (comboBox_serialPorts.InvokeRequired)
            {
                updateSerialPortsCallback d = new updateSerialPortsCallback(updateSerialPorts);
                this.Invoke(d, new object[] { });
            }
            else
            {
                comboBox_serialPorts.DataSource = SerialPort.GetPortNames();
                comboBox_serialPorts.Refresh();
            }
        }
*/
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

        private void textBox_serialCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int length = tb.Text.Length;
            tb.SelectionStart = length;

            if (e.KeyChar == '\r')
            {
                string cmd = "";
                int index = length - 1;
                while (true)
                {
                    char c = tb.Text[index--];
                    if (c != '>')
                    {
                        cmd = c + cmd;
                    }
                    else { break; }

                    if (index == 0) break;
                }

                _ee203.WriteLine(cmd);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isclosing = true;

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ee203.Zero();
            chart1.Series["Current"].Points.Clear();
        }

        private void chart1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            //chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 0.000001);
            //chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
        }

        private void Form_Controller_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        void init_chart()
        {
            string name = "Current";
            chart1.Series.Add(name);
            chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            //chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 10);

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;


            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;


            chart1.Series[name].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chart1.Series[name].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTimeOffset;
            //chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            //chart1.ChartAreas[0].AxisX.Interval = 1;

        }

    }
}
