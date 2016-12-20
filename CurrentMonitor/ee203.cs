using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Ports;
using System.Management;
using System.Text.RegularExpressions;

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

        public enum Sampling : int { Fastest = 1, Fast = 10, Medium = 100, Slow = 1000 };

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

        /// <summary>
        /// Returns the captions used in device manager for all COM ports in the system
        /// </summary>
        /// <returns></returns>
        public static string[] GetSystemComPortCaptions()
        {
            List<string> port_names = new List<string>(); ;

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["Caption"] != null)
                    {
                        if (queryObj["Caption"].ToString().Contains("(COM"))
                        {
                            port_names.Add(queryObj["Caption"].ToString());
                            
                        }
                    }

                }
            }
            catch (ManagementException e)
            {
                string msg = e.Message;
            }

            return port_names.ToArray();
        }

        public static string[] GetComPortNames(string type_regx)
        {
            string[] system_ports = GetSystemComPortCaptions();
            List<string> port_names = new List<string>(); ;
            foreach(string port_name in system_ports)
            {

                if (Regex.Match(port_name, type_regx).Success)
                {
                    int p1 = port_name.IndexOf('(');
                    int p2 = port_name.IndexOf(')');
                    if (p1 > 0 && p2 > 0 && p2 > p1)
                        port_names.Add(port_name.Substring(p1+1, p2-p1-1));
                }
            }

            return port_names.ToArray();
        }


        private void _data_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (DataPort_Data_Event != null)
            {
                DataPort_Data_Event(this, _data_port.ReadExisting());
            }
        }

        private void _cmd_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (CmdPort_Data_Event != null)
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
            if (_cmd_port != null && _cmd_port.IsOpen)
                return _cmd_port;

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
            OpenCmdPort();
            _cmd_port.WriteLine(data);
        }

        public void Pause()
        {
            WriteLine("p");
        }
        public void Resume()
        {
            WriteLine("r");
        }
        public void Zero()
        {
            WriteLine("z");
        }

        public void Interval(Sampling interval)
        {
            string cmd = string.Format("i {0}", (int)interval);
            WriteLine(cmd);
        }

        public static TimeSpan DateTimeParse(string str)
        {
            string[] ts1 = str.Split(new char[] { ':' });
            string[] ts2 = ts1[1].Split(new char[] { '.' });
            TimeSpan timestamp = new TimeSpan(0, 0, Convert.ToInt32(ts1[0]), Convert.ToInt32(ts2[0]), Convert.ToInt32(ts2[1]));

            return timestamp;
        }
    }
}
