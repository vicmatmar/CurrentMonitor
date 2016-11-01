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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_dataPortName = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_cmdPortName
            // 
            this.textBox_cmdPortName.Location = new System.Drawing.Point(102, 18);
            this.textBox_cmdPortName.Name = "textBox_cmdPortName";
            this.textBox_cmdPortName.Size = new System.Drawing.Size(100, 20);
            this.textBox_cmdPortName.TabIndex = 0;
            this.textBox_cmdPortName.Text = "COM6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cmd Port Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Data Port Name:";
            // 
            // textBox_dataPortName
            // 
            this.textBox_dataPortName.Location = new System.Drawing.Point(102, 48);
            this.textBox_dataPortName.Name = "textBox_dataPortName";
            this.textBox_dataPortName.Size = new System.Drawing.Size(100, 20);
            this.textBox_dataPortName.TabIndex = 2;
            this.textBox_dataPortName.Text = "COM5";
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(61, 226);
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
            this.button_cancel.Location = new System.Drawing.Point(143, 225);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "&Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // Form_Settings
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_dataPortName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_cmdPortName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_Settings";
            this.Text = "Form_Settings";
            this.Load += new System.EventHandler(this.Form_Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_cmdPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_dataPortName;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
    }
}