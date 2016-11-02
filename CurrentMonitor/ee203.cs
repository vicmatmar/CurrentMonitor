using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Ports;

namespace CurrentMonitor
{
    class ee203
    {

        string _cmd_port_name = "COM6";
        public string CMD_Port_Name { get { return _cmd_port_name; } set { _cmd_port_name = value; } }

        string _data_port_name = "COM5";
        public string DATA_Port_Name { get { return _data_port_name; } set { _data_port_name = value; } }

        SerialPort _data_port;
        public SerialPort DataPort { get { return _data_port; } }

        SerialPort _cmd_port;
        public SerialPort CmdPort { get { return _cmd_port; } }

        public enum Sampling : int {Fastest=1, Fast=10, Medium=100, Slow=1000};

        int _baud_rate = 576600;
        string _new_line = "\r";

        public SerialPort OpenCmdPort()
        {
            _cmd_port = new SerialPort(CMD_Port_Name);
            _cmd_port.BaudRate = _baud_rate;
            _cmd_port.NewLine = _new_line;
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
            _data_port = new SerialPort(DATA_Port_Name);
            _data_port.BaudRate = _baud_rate;
            _data_port.NewLine = _new_line;
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
