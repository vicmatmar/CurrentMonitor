namespace CurrentMonitor
{
    partial class Form_Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.controllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_v_act = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_reset_max_min = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_i_act = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_v_max = new System.Windows.Forms.Label();
            this.label_i_max = new System.Windows.Forms.Label();
            this.label_v_min = new System.Windows.Forms.Label();
            this.label_i_min = new System.Windows.Forms.Label();
            this.label_dev_status = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label_results = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controllerToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(437, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // controllerToolStripMenuItem
            // 
            this.controllerToolStripMenuItem.Name = "controllerToolStripMenuItem";
            this.controllerToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.controllerToolStripMenuItem.Text = "Controller...";
            this.controllerToolStripMenuItem.Click += new System.EventHandler(this.controllerToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // label_v_act
            // 
            this.label_v_act.AutoSize = true;
            this.label_v_act.Location = new System.Drawing.Point(55, 40);
            this.label_v_act.Name = "label_v_act";
            this.label_v_act.Size = new System.Drawing.Size(30, 13);
            this.label_v_act.TabIndex = 1;
            this.label_v_act.Text = "VAct";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 145);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Measurements";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.button_reset_max_min, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_i_act, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_v_act, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_v_max, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_i_max, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_v_min, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_i_min, 4, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(266, 100);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // button_reset_max_min
            // 
            this.button_reset_max_min.Location = new System.Drawing.Point(3, 3);
            this.button_reset_max_min.Name = "button_reset_max_min";
            this.button_reset_max_min.Size = new System.Drawing.Size(44, 21);
            this.button_reset_max_min.TabIndex = 5;
            this.button_reset_max_min.Text = "&Reset";
            this.button_reset_max_min.UseVisualStyleBackColor = true;
            this.button_reset_max_min.Click += new System.EventHandler(this.button_reset_max_min_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Voltage:";
            // 
            // label_i_act
            // 
            this.label_i_act.AutoSize = true;
            this.label_i_act.Location = new System.Drawing.Point(55, 80);
            this.label_i_act.Name = "label_i_act";
            this.label_i_act.Size = new System.Drawing.Size(26, 13);
            this.label_i_act.TabIndex = 2;
            this.label_i_act.Text = "IAct";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Actual";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Max";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Min";
            // 
            // label_v_max
            // 
            this.label_v_max.AutoSize = true;
            this.label_v_max.Location = new System.Drawing.Point(98, 40);
            this.label_v_max.Name = "label_v_max";
            this.label_v_max.Size = new System.Drawing.Size(34, 13);
            this.label_v_max.TabIndex = 7;
            this.label_v_max.Text = "VMax";
            // 
            // label_i_max
            // 
            this.label_i_max.AutoSize = true;
            this.label_i_max.Location = new System.Drawing.Point(98, 80);
            this.label_i_max.Name = "label_i_max";
            this.label_i_max.Size = new System.Drawing.Size(30, 13);
            this.label_i_max.TabIndex = 8;
            this.label_i_max.Text = "IMax";
            // 
            // label_v_min
            // 
            this.label_v_min.AutoSize = true;
            this.label_v_min.Location = new System.Drawing.Point(138, 40);
            this.label_v_min.Name = "label_v_min";
            this.label_v_min.Size = new System.Drawing.Size(31, 13);
            this.label_v_min.TabIndex = 9;
            this.label_v_min.Text = "VMin";
            // 
            // label_i_min
            // 
            this.label_i_min.AutoSize = true;
            this.label_i_min.Location = new System.Drawing.Point(138, 80);
            this.label_i_min.Name = "label_i_min";
            this.label_i_min.Size = new System.Drawing.Size(27, 13);
            this.label_i_min.TabIndex = 10;
            this.label_i_min.Text = "IMin";
            // 
            // label_dev_status
            // 
            this.label_dev_status.AutoSize = true;
            this.label_dev_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_dev_status.Location = new System.Drawing.Point(12, 115);
            this.label_dev_status.Name = "label_dev_status";
            this.label_dev_status.Size = new System.Drawing.Size(62, 16);
            this.label_dev_status.TabIndex = 3;
            this.label_dev_status.Text = "Waiting...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 391);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(437, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // label_results
            // 
            this.label_results.AutoSize = true;
            this.label_results.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_results.Location = new System.Drawing.Point(162, 50);
            this.label_results.Name = "label_results";
            this.label_results.Size = new System.Drawing.Size(112, 24);
            this.label_results.TabIndex = 5;
            this.label_results.Text = "labelPaaFail";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 413);
            this.Controls.Add(this.label_results);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label_dev_status);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_Main";
            this.Text = "Form_Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem controllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label label_v_act;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_i_act;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_dev_status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_v_max;
        private System.Windows.Forms.Label label_i_max;
        private System.Windows.Forms.Label label_v_min;
        private System.Windows.Forms.Label label_i_min;
        private System.Windows.Forms.Button button_reset_max_min;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label_results;
    }
}