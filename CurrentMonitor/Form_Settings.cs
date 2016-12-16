using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrentMonitor
{
    public partial class Form_Settings : Form
    {
        public Form_Settings()
        {
            InitializeComponent();
        }

        private void Form_Settings_Load(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();

            string caption = "EE203 Command serial port name setting";
            tooltip.SetToolTip(label_cmdPortName, caption);
            tooltip.SetToolTip(textBox_cmdPortName, caption);
            textBox_cmdPortName.Text = Properties.Settings.Default.Cmd_Port_Name;

            caption = "EE203 Data serial port name setting";
            tooltip.SetToolTip(label_dataPortName, caption);
            tooltip.SetToolTip(textBox_dataPortName, caption);
            textBox_dataPortName.Text = Properties.Settings.Default.Data_Port_Name;

            caption = "Current below this level will set the state to \"No Device\" detected";
            tooltip.SetToolTip(textBox_nodevice_threshold, caption);
            tooltip.SetToolTip(label_nodevice_threshold, caption);
            textBox_off_threadhold.Text = Properties.Settings.Default.Voltage_Off_Threshold.ToString();


            textBox_nodevice_threshold.Text = Properties.Settings.Default.Current_NoDevice_Threshold.ToString();

        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Cmd_Port_Name = textBox_cmdPortName.Text;
            Properties.Settings.Default.Data_Port_Name = textBox_dataPortName.Text;

            Properties.Settings.Default.Save();

            Close();

        }

        private void textBox_off_threadhold_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Voltage_Off_Threshold =
                Convert.ToDouble( textBox_off_threadhold.Text );
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Current_NoDevice_Threshold =
                    Convert.ToDouble(textBox_nodevice_threshold.Text);
            }
            catch { };

        }

    }
}
