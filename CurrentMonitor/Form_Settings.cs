﻿using System;
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
            textBox_cmdPortName.Text = Properties.Settings.Default.Cmd_Port_Name;
            textBox_dataPortName.Text = Properties.Settings.Default.Data_Port_Name;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Cmd_Port_Name = textBox_cmdPortName.Text;
            Properties.Settings.Default.Data_Port_Name = textBox_dataPortName.Text;

            Properties.Settings.Default.Save();

        }
    }
}
