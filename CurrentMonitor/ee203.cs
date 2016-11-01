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

        int _baud_rate = 576600;
        string _new_line = "\r";

        public SerialPort OpenCmdPort()
        {
            SerialPort port = new SerialPort(CMD_Port_Name);
            port.BaudRate = _baud_rate;
            port.NewLine = _new_line;
            int trycount = 0;
            while (true)
            {
                try
                {
                    port.Open();
                    break;
                }
                catch { Thread.Sleep(500); }

                if (trycount++ > 10)
                {
                    port.Open();
                }
            }
            return port;
        }

        public SerialPort OpenDataPort()
        {
            SerialPort port = new SerialPort(DATA_Port_Name);
            port.BaudRate = _baud_rate;
            port.NewLine = _new_line;
            int trycount = 0;
            while (true)
            {
                try
                {
                    port.Open();
                    break;
                }
                catch { Thread.Sleep(500); }

                if (trycount++ > 10)
                {
                    port.Open();
                }
            }
            return port;
        }

    }
}
