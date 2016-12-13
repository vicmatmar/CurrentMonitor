using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Ports;

namespace CurrentMonitor
{
    public class ee203
    {
        public delegate void DataPortDataHandler(object sender, string data);
        public event DataPortDataHandler DataPort_Data_Event;

        public delegate void CmdPortDataHandler(object sender, string data);
        public event CmdPortDataHandler CmdPort_Data_Event;

        string _cmd_port_name = "COM6";
        public string CMD_Port_Name { get { return _cmd_port_name; } }

        string _data_port_name = "COM5";
        public string DATA_Port_Name { get { return _data_port_name; } }

        SerialPort _data_port;
        //public SerialPort DataPort { get { return _data_port; } }

        SerialPort _cmd_port;
        //public SerialPort CmdPort { get { return _cmd_port; } }

        public enum Sampling : int {Fastest=1, Fast=10, Medium=100, Slow=1000};

        int _baud_rate = 576600;
        string _new_line = "\r";

        public ee203(string cmd_port_name, string data_port_name)
        {
            _cmd_port_name = cmd_port_name;
            _data_port_name = data_port_name;

            _cmd_port = new SerialPort(CMD_Port_Name);
            _cmd_port.DataReceived += _cmd_port_DataReceived;
            _cmd_port.BaudRate = _baud_rate;
            _cmd_port.NewLine = _new_line;

            _data_port = new SerialPort(DATA_Port_Name);
            _data_port.DataReceived += _data_port_DataReceived;
            _data_port.BaudRate = _baud_rate;
            _data_port.NewLine = _new_line;
        }

        private void _data_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(DataPort_Data_Event != null)
            {
                DataPort_Data_Event(this, _data_port.ReadExisting());
            }
        }

        private void _cmd_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(CmdPort_Data_Event != null)
            {
                CmdPort_Data_Event(this, _cmd_port.ReadExisting());
            }
        }

         public void ClosePorts()
        {
            _cmd_port.Close();
            _data_port.Close();
        }

        public SerialPort OpenCmdPort()
        {
            int trycount = 0;
            while (true)
            {
                try
                {
                    _cmd_port.Open();
                    break;
                }
                catch { Thread.Sleep(500); }

                if (trycount++ > 10)
                {
                    _cmd_port.Open();
                }
            }
            return _cmd_port;
        }

        public SerialPort OpenDataPort()
        {
            if (_data_port != null && _data_port.IsOpen)
                return _data_port;

            int trycount = 0;
            while (true)
            {
                try
                {
                    _data_port.Open();
                    break;
                }
                catch { Thread.Sleep(500); }

                if (trycount++ > 10)
                {
                    _data_port.Open();
                }
            }
            return _data_port;
        }

        public void WriteLine(string data)
        {
            _cmd_port.WriteLine(data);
        }

        public void Pause()
        {
            _cmd_port.WriteLine("p");
        }
        public void Resume()
        {
            _cmd_port.WriteLine("r");
        }
        public void Zero()
        {
            _cmd_port.WriteLine("z");
        }

        public void Interval(Sampling interval)
        {
            string cmd = string.Format("i {0}", (int)interval);
            _cmd_port.WriteLine(cmd);
        }


    }
}
