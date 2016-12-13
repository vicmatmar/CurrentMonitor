namespace CurrentMonitor
{
    partial class Form_Controller
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.comboBox_serialPorts = new System.Windows.Forms.ComboBox();
            this.textBox_serialCommand = new System.Windows.Forms.TextBox();
            this.textBox_serialData = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip_Chart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.contextMenuStrip_Chart.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_serialPorts
            // 
            this.comboBox_serialPorts.FormattingEnabled = true;
            this.comboBox_serialPorts.Location = new System.Drawing.Point(12, 12);
            this.comboBox_serialPorts.Name = "comboBox_serialPorts";
            this.comboBox_serialPorts.Size = new System.Drawing.Size(121, 21);
            this.comboBox_serialPorts.TabIndex = 0;
            // 
            // textBox_serialCommand
            // 
            this.textBox_serialCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_serialCommand.Location = new System.Drawing.Point(12, 345);
            this.textBox_serialCommand.Multiline = true;
            this.textBox_serialCommand.Name = "textBox_serialCommand";
            this.textBox_serialCommand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_serialCommand.Size = new System.Drawing.Size(920, 165);
            this.textBox_serialCommand.TabIndex = 1;
            this.textBox_serialCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_serialCommand_KeyPress);
            // 
            // textBox_serialData
            // 
            this.textBox_serialData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_serialData.Location = new System.Drawing.Point(12, 516);
            this.textBox_serialData.Multiline = true;
            this.textBox_serialData.Name = "textBox_serialData";
            this.textBox_serialData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_serialData.Size = new System.Drawing.Size(920, 239);
            this.textBox_serialData.TabIndex = 2;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ContextMenuStrip = this.contextMenuStrip_Chart;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 52);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(920, 287);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            this.chart1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDoubleClick);
            // 
            // contextMenuStrip_Chart
            // 
            this.contextMenuStrip_Chart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuStrip_Chart.Name = "contextMenuStrip_Chart";
            this.contextMenuStrip_Chart.Size = new System.Drawing.Size(102, 26);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "&Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // Form_Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 767);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.textBox_serialData);
            this.Controls.Add(this.textBox_serialCommand);
            this.Controls.Add(this.comboBox_serialPorts);
            this.Name = "Form_Controller";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Controller_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.contextMenuStrip_Chart.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_serialPorts;
        private System.Windows.Forms.TextBox textBox_serialCommand;
        private System.Windows.Forms.TextBox textBox_serialData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Chart;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    }
}

