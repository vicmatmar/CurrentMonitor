namespace CurrentMonitor
{
    partial class Form_Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_cmdPortName = new System.Windows.Forms.TextBox();
            this.label_cmdPortName = new System.Windows.Forms.Label();
            this.label_dataPortName = new System.Windows.Forms.Label();
            this.textBox_dataPortName = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label_voltage_value = new System.Windows.Forms.Label();
            this.textBox_voltage_value = new System.Windows.Forms.TextBox();
            this.textBox_voltage_tolarance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_off_thresdhold = new System.Windows.Forms.Label();
            this.textBox_off_thresdhold = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_sleep_threshold = new System.Windows.Forms.TextBox();
            this.label_sleep_threshold = new System.Windows.Forms.Label();
            this.textBox_nodevice_threshold = new System.Windows.Forms.TextBox();
            this.label_nodevice_threshold = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_current_high = new System.Windows.Forms.Label();
            this.textBox_current_high = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_cmdPortName
            // 
            this.textBox_cmdPortName.Location = new System.Drawing.Point(96, 20);
            this.textBox_cmdPortName.Name = "textBox_cmdPortName";
            this.textBox_cmdPortName.Size = new System.Drawing.Size(100, 20);
            this.textBox_cmdPortName.TabIndex = 0;
            this.textBox_cmdPortName.Text = "COM6";
            this.textBox_cmdPortName.TextChanged += new System.EventHandler(this.textBox_cmdPortName_TextChanged);
            // 
            // label_cmdPortName
            // 
            this.label_cmdPortName.AutoSize = true;
            this.label_cmdPortName.Location = new System.Drawing.Point(6, 23);
            this.label_cmdPortName.Name = "label_cmdPortName";
            this.label_cmdPortName.Size = new System.Drawing.Size(84, 13);
            this.label_cmdPortName.TabIndex = 1;
            this.label_cmdPortName.Text = "Cmd Port Name:";
            // 
            // label_dataPortName
            // 
            this.label_dataPortName.AutoSize = true;
            this.label_dataPortName.Location = new System.Drawing.Point(6, 53);
            this.label_dataPortName.Name = "label_dataPortName";
            this.label_dataPortName.Size = new System.Drawing.Size(86, 13);
            this.label_dataPortName.TabIndex = 3;
            this.label_dataPortName.Text = "Data Port Name:";
            // 
            // textBox_dataPortName
            // 
            this.textBox_dataPortName.Location = new System.Drawing.Point(96, 50);
            this.textBox_dataPortName.Name = "textBox_dataPortName";
            this.textBox_dataPortName.Size = new System.Drawing.Size(100, 20);
            this.textBox_dataPortName.TabIndex = 2;
            this.textBox_dataPortName.Text = "COM5";
            this.textBox_dataPortName.TextChanged += new System.EventHandler(this.textBox_dataPortName_TextChanged);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(83, 315);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 4;
            this.button_ok.Text = "&Ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(165, 315);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "&Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // label_voltage_value
            // 
            this.label_voltage_value.AutoSize = true;
            this.label_voltage_value.Location = new System.Drawing.Point(6, 26);
            this.label_voltage_value.Name = "label_voltage_value";
            this.label_voltage_value.Size = new System.Drawing.Size(42, 13);
            this.label_voltage_value.TabIndex = 7;
            this.label_voltage_value.Text = "Supply:";
            // 
            // textBox_voltage_value
            // 
            this.textBox_voltage_value.Location = new System.Drawing.Point(54, 23);
            this.textBox_voltage_value.Name = "textBox_voltage_value";
            this.textBox_voltage_value.Size = new System.Drawing.Size(34, 20);
            this.textBox_voltage_value.TabIndex = 6;
            this.textBox_voltage_value.Text = "3.0";
            this.textBox_voltage_value.TextChanged += new System.EventHandler(this.textBox_voltage_value_TextChanged);
            // 
            // textBox_voltage_tolarance
            // 
            this.textBox_voltage_tolarance.Location = new System.Drawing.Point(173, 23);
            this.textBox_voltage_tolarance.Name = "textBox_voltage_tolarance";
            this.textBox_voltage_tolarance.Size = new System.Drawing.Size(34, 20);
            this.textBox_voltage_tolarance.TabIndex = 8;
            this.textBox_voltage_tolarance.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tolarance:";
            // 
            // label_off_thresdhold
            // 
            this.label_off_thresdhold.AutoSize = true;
            this.label_off_thresdhold.Location = new System.Drawing.Point(6, 57);
            this.label_off_thresdhold.Name = "label_off_thresdhold";
            this.label_off_thresdhold.Size = new System.Drawing.Size(74, 13);
            this.label_off_thresdhold.TabIndex = 11;
            this.label_off_thresdhold.Text = "Off Threshold:";
            // 
            // textBox_off_thresdhold
            // 
            this.textBox_off_thresdhold.Location = new System.Drawing.Point(86, 54);
            this.textBox_off_thresdhold.Name = "textBox_off_thresdhold";
            this.textBox_off_thresdhold.Size = new System.Drawing.Size(61, 20);
            this.textBox_off_thresdhold.TabIndex = 10;
            this.textBox_off_thresdhold.Text = "0.001";
            this.textBox_off_thresdhold.TextChanged += new System.EventHandler(this.textBox_off_threadhold_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_voltage_value);
            this.groupBox1.Controls.Add(this.label_off_thresdhold);
            this.groupBox1.Controls.Add(this.textBox_voltage_value);
            this.groupBox1.Controls.Add(this.textBox_off_thresdhold);
            this.groupBox1.Controls.Add(this.textBox_voltage_tolarance);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(11, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 90);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Voltage";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_current_high);
            this.groupBox2.Controls.Add(this.label_current_high);
            this.groupBox2.Controls.Add(this.textBox_sleep_threshold);
            this.groupBox2.Controls.Add(this.label_sleep_threshold);
            this.groupBox2.Controls.Add(this.textBox_nodevice_threshold);
            this.groupBox2.Controls.Add(this.label_nodevice_threshold);
            this.groupBox2.Location = new System.Drawing.Point(11, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 109);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current";
            // 
            // textBox_sleep_threshold
            // 
            this.textBox_sleep_threshold.Location = new System.Drawing.Point(146, 45);
            this.textBox_sleep_threshold.Name = "textBox_sleep_threshold";
            this.textBox_sleep_threshold.Size = new System.Drawing.Size(61, 20);
            this.textBox_sleep_threshold.TabIndex = 14;
            this.textBox_sleep_threshold.Text = "200E-6";
            this.textBox_sleep_threshold.TextChanged += new System.EventHandler(this.textBox_sleep_threshold_TextChanged);
            // 
            // label_sleep_threshold
            // 
            this.label_sleep_threshold.AutoSize = true;
            this.label_sleep_threshold.Location = new System.Drawing.Point(6, 49);
            this.label_sleep_threshold.Name = "label_sleep_threshold";
            this.label_sleep_threshold.Size = new System.Drawing.Size(124, 13);
            this.label_sleep_threshold.TabIndex = 13;
            this.label_sleep_threshold.Text = "Device Sleep Threshold:";
            // 
            // textBox_nodevice_threshold
            // 
            this.textBox_nodevice_threshold.Location = new System.Drawing.Point(146, 17);
            this.textBox_nodevice_threshold.Name = "textBox_nodevice_threshold";
            this.textBox_nodevice_threshold.Size = new System.Drawing.Size(61, 20);
            this.textBox_nodevice_threshold.TabIndex = 12;
            this.textBox_nodevice_threshold.Text = "200E-9";
            this.textBox_nodevice_threshold.TextChanged += new System.EventHandler(this.textBox_noDevice_TextChanged);
            // 
            // label_nodevice_threshold
            // 
            this.label_nodevice_threshold.AutoSize = true;
            this.label_nodevice_threshold.Location = new System.Drawing.Point(6, 20);
            this.label_nodevice_threshold.Name = "label_nodevice_threshold";
            this.label_nodevice_threshold.Size = new System.Drawing.Size(111, 13);
            this.label_nodevice_threshold.TabIndex = 0;
            this.label_nodevice_threshold.Text = "No Device Threshold:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_dataPortName);
            this.groupBox3.Controls.Add(this.textBox_cmdPortName);
            this.groupBox3.Controls.Add(this.label_cmdPortName);
            this.groupBox3.Controls.Add(this.textBox_dataPortName);
            this.groupBox3.Location = new System.Drawing.Point(11, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 85);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serial Ports";
            // 
            // label_current_high
            // 
            this.label_current_high.AutoSize = true;
            this.label_current_high.Location = new System.Drawing.Point(9, 78);
            this.label_current_high.Name = "label_current_high";
            this.label_current_high.Size = new System.Drawing.Size(119, 13);
            this.label_current_high.TabIndex = 15;
            this.label_current_high.Text = "High Current Threshold:";
            // 
            // textBox_current_high
            // 
            this.textBox_current_high.Location = new System.Drawing.Point(146, 75);
            this.textBox_current_high.Name = "textBox_current_high";
            this.textBox_current_high.Size = new System.Drawing.Size(61, 20);
            this.textBox_current_high.TabIndex = 16;
            this.textBox_current_high.Text = "10E-3";
            this.textBox_current_high.TextChanged += new System.EventHandler(this.textBox_current_high_TextChanged);
            // 
            // Form_Settings
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(323, 362);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_Settings";
            this.Text = "Form_Settings";
            this.Load += new System.EventHandler(this.Form_Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_cmdPortName;
        private System.Windows.Forms.Label label_cmdPortName;
        private System.Windows.Forms.Label label_dataPortName;
        private System.Windows.Forms.TextBox textBox_dataPortName;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label_voltage_value;
        private System.Windows.Forms.TextBox textBox_voltage_value;
        private System.Windows.Forms.TextBox textBox_voltage_tolarance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_off_thresdhold;
        private System.Windows.Forms.TextBox textBox_off_thresdhold;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_nodevice_threshold;
        private System.Windows.Forms.Label label_nodevice_threshold;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_sleep_threshold;
        private System.Windows.Forms.Label label_sleep_threshold;
        private System.Windows.Forms.TextBox textBox_current_high;
        private System.Windows.Forms.Label label_current_high;
    }
}