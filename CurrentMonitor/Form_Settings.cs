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

            caption = "Device Supplied Voltage";
            tooltip.SetToolTip(label_voltage_value, caption);
            tooltip.SetToolTip(textBox_voltage_value, caption);
            textBox_voltage_value.Text = Utils.ToSIPrefixedString(
                Properties.Settings.Default.Voltage_Value) + "V";

            textBox_voltage_tolarance.Text = Properties.Settings.Default.Voltage_Tolarance.ToString();

            caption = "Voltage level below this value considered as device not powered\r\nUsually set to 200 mV or less";
            tooltip.SetToolTip(label_off_thresdhold, caption);
            tooltip.SetToolTip(textBox_off_thresdhold, caption);
            textBox_off_thresdhold.Text = Utils.ToSIPrefixedString(
                Properties.Settings.Default.Voltage_Off_Threshold) + "V";

            caption = "Current level below this value considered as device not detected\r\nUsually set to 200 nA or less";
            tooltip.SetToolTip(textBox_nodevice_threshold, caption);
            tooltip.SetToolTip(label_nodevice_threshold, caption);
            textBox_nodevice_threshold.Text = Utils.ToSIPrefixedString(
                Properties.Settings.Default.Current_NoDevice_Threshold) + "A";

            caption = "Current level below this value considered as device is in sleep mode\r\nUsually set to 200 uA or less";
            tooltip.SetToolTip(textBox_sleep_threshold, caption);
            tooltip.SetToolTip(label_sleep_threshold, caption);
            textBox_sleep_threshold.Text = Utils.ToSIPrefixedString(
                Properties.Settings.Default.Current_Sleep_Threshold) + "A";

            caption = "Current level above this value considered as device is in high current mode\r\nUsually set to 10 mA or more";
            tooltip.SetToolTip(textBox_current_high, caption);
            tooltip.SetToolTip(label_current_high, caption);
            textBox_current_high.Text = Utils.ToSIPrefixedString(
                Properties.Settings.Default.Current_High_Threshold) + "A";

        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Cmd_Port_Name = textBox_cmdPortName.Text;
            Properties.Settings.Default.Data_Port_Name = textBox_dataPortName.Text;

            Properties.Settings.Default.Save();
        }

        private void textBox_voltage_value_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double value = convertToDouble(textBox_voltage_value.Text);
                Properties.Settings.Default.Voltage_Value = value;
            }
            catch (FormatException) { };
        }

        private void textBox_off_threadhold_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double value = convertToDouble(textBox_off_thresdhold.Text);
                Properties.Settings.Default.Voltage_Off_Threshold = value;
            }
            catch (FormatException) { };
        }
        private void textBox_noDevice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double value = convertToDouble(textBox_nodevice_threshold.Text);
                Properties.Settings.Default.Current_NoDevice_Threshold = value;
            }
            catch (FormatException) { };
        }

        private void textBox_sleep_threshold_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double value = convertToDouble(textBox_sleep_threshold.Text);
                Properties.Settings.Default.Current_Sleep_Threshold = value;
            }
            catch (FormatException) { };
        }

        private void textBox_current_high_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double value = convertToDouble(textBox_current_high.Text);
                Properties.Settings.Default.Current_High_Threshold = value;
            }
            catch (FormatException) { };
        }

        private void textBox_cmdPortName_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Cmd_Port_Name = textBox_cmdPortName.Text;
        }

        private void textBox_dataPortName_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Data_Port_Name = textBox_dataPortName.Text;
        }

        double convertToDouble(string data)
        {
            double m = 1.0;
            int i = data.IndexOf('m');
            if (i > 0)
                m = 1e-3;
            else
            {
                i = data.IndexOf('u');
                if (i > 0)
                    m = 1e-6;
                else
                {
                    i = data.IndexOf('n');
                    if (i > 0)
                        m = 1e-9;
                }
            }

            double value = 0;
            if (i > 0)
            {
                string numstr = data.Substring(0, i - 1).Trim();
                value = Convert.ToDouble(numstr) * m;
            }
            else
                value = Convert.ToDouble(data);

            return value;
        }
    }
}
