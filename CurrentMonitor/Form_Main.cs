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

        public Form_Main()
        {
            InitializeComponent();
        }

        private void controllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Controller ctrlform = new Form_Controller();
            ctrlform.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Settings dlg = new Form_Settings();
            DialogResult res = dlg.ShowDialog();
            if(res == DialogResult.OK)
            {

            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            //openPorts();
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
                _cmd_port.WriteLine("p");
                _cmd_port.WriteLine("z");

                _ee203.DATA_Port_Name = Properties.Settings.Default.Data_Port_Name;
                _data_port = _ee203.OpenDataPort();
                _data_port.DataReceived += _data_port_DataReceived;

                _cmd_port.WriteLine("i 100");
                _cmd_port.WriteLine("r");

            }
        }

        private void _data_port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }
    }
}
