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
    public partial class Form1 : Form
    {
        delegate void setControlPropertyValueCallback(Control control, object value, string property_name);
        delegate void updateSerialPortsCallback();
        delegate void updateTextboxDataCallback();
        delegate void updateChartCallback(string data);

        SerialPort _port_cmd, _port_data;
        ConcurrentQueue<string> _serial_cmd_queue = new ConcurrentQueue<string>();
        ConcurrentQueue<string> _serial_data_queue = new ConcurrentQueue<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            init_chart();

            comboBox_serialPorts.DataSource = SerialPort.GetPortNames();

            openCmdPort();

            if (_port_cmd.IsOpen)
            {
                _port_data = new SerialPort("COM5");
                _port_data.Disposed += _port_data_Disposed;
                _port_data.DataReceived += _port_DataReceived;
                _port_data.BaudRate = 576600;
                int trycount = 0;
                while (true)
                {
                    try
                    {
                        _port_data.Open();
                        break;
                    }
                    catch { }

                    if (trycount++ > 10)
                    {
                        try
                        {
                            _port_data.Open();
                            break;
                        }
                        catch
                        {
                            DialogResult res = MessageBox.Show("Please reset data device", "Open port error", MessageBoxButtons.RetryCancel);
                            if (res == DialogResult.Cancel)
                            {
                                break;
                            }
                        }
                    }
                }

                _port_cmd.WriteLine("h");
                _port_cmd.WriteLine("i 100");
                _port_cmd.WriteLine("r");


            }

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

        private void _port_data_Disposed(object sender, EventArgs e)
        {
        }

        void openCmdPort()
        {
            _port_cmd = new SerialPort("COM6");
            _port_cmd.DataReceived += Port_CommandDataReceived;
            _port_cmd.BaudRate = 576600;
            _port_cmd.NewLine = "\r";
            int trycount = 0;
            while (true)
            {
                try
                {
                    _port_cmd.Open();
                    break;
                }
                catch { }

                if (trycount++ > 10)
                {
                    try
                    {
                        _port_cmd.Open();
                        break;
                    }
                    catch
                    {
                        DialogResult res = MessageBox.Show("Please reset device", "Open port error", MessageBoxButtons.RetryCancel);
                        if (res == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                }
            }

        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _serial_data_queue.Enqueue(_port_data.ReadExisting());
            updateTextboxData();
        }

        void updateTextboxData()
        {
            if (textBox_serialData.InvokeRequired)
            {
                updateTextboxDataCallback d = new updateTextboxDataCallback(updateTextboxData);
                try
                {
                    if(_port_data.IsOpen)
                        this.Invoke(d);
                }
                catch(Exception ex)
                {
                    string m = ex.Message;
                }
            }
            else
            {
                string data;
                if (_serial_data_queue.TryDequeue(out data))
                {
                    update_chart(data);

                    if (textBox_serialData.Text.Length > 500000)
                    {
                        textBox_serialData.Text = textBox_serialData.Text.Substring(500000 / 2);
                    }

                    data = data.Replace("\r", "\r\n");
                    textBox_serialData.AppendText(data);
                }
            }
        }

        private void Port_CommandDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;

            switch (e.EventType)
            {
                case SerialData.Chars:
                    _serial_cmd_queue.Enqueue(_port_cmd.ReadExisting());
                    updateCommandData();
                    break;
                case SerialData.Eof:
                    // means receiving ended
                    break;
            }
        }

        void updateCommandData()
        {
            if (textBox_serialCommand.InvokeRequired)
            {
                updateTextboxDataCallback d = new updateTextboxDataCallback(updateCommandData);
                this.Invoke(d);
            }
            else
            {
                string data;
                if (_serial_cmd_queue.TryDequeue(out data))
                {
                    data = data.Replace("\r", "\r\n");
                    textBox_serialCommand.AppendText(data);
                }
            }
        }

        void update_chart(string data)
        {
            if (chart1.InvokeRequired)
            {
                updateChartCallback d = new updateChartCallback(update_chart);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                try
                {
                    string[] lines = data.Split(new char[] { '\r' });
                    foreach(string line in lines)
                    {
                        string[] cells = line.Split(new char[] { ',' });
                        if(cells.Length == 7)
                        {
                            try
                            {
                                string[] ts1 = cells[0].Split(new char[] { ':' });
                                string[] ts2 = ts1[1].Split(new char[] { '.' });
                                TimeSpan timestamp = new TimeSpan(0, 0, Convert.ToInt32(ts1[0]), Convert.ToInt32(ts2[0]), Convert.ToInt32(ts2[1]));

                                double current = Convert.ToDouble(cells[3]);
                                chart1.Series["Current"].Points.AddXY(timestamp.ToString(), current);

                            }
                            catch { }
                        }
                    }

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
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
            updateSerialPorts();
        }

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

        private void textBox_serialCommand_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox_serialCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int length = tb.Text.Length;
            tb.SelectionStart = length;

            if(e.KeyChar == '\r')
            {
                string cmd = "";
                int index = length-1;
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

                _port_cmd.WriteLine(cmd);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_port_cmd.IsOpen)
                _port_cmd.Close();


            if (_port_data.IsOpen)
            {
                Task close_data_port_task = new Task(() => _port_data.Close());
                close_data_port_task.Start();
            }
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
