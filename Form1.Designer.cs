using System.Windows.Forms.DataVisualization.Charting;
namespace AsyncChart
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbFuncs = new System.Windows.Forms.GroupBox();
            this.rbRandomFunc = new System.Windows.Forms.RadioButton();
            this.rbFuncRandom = new System.Windows.Forms.RadioButton();
            this.rbFuncDigital = new System.Windows.Forms.RadioButton();
            this.rbFuncSin = new System.Windows.Forms.RadioButton();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudPoints = new System.Windows.Forms.NumericUpDown();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            this.gbFuncs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbFuncs);
            this.panel1.Controls.Add(this.nudInterval);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nudPoints);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 602);
            this.panel1.TabIndex = 1;
            // 
            // gbFuncs
            // 
            this.gbFuncs.Controls.Add(this.rbRandomFunc);
            this.gbFuncs.Controls.Add(this.rbFuncRandom);
            this.gbFuncs.Controls.Add(this.rbFuncDigital);
            this.gbFuncs.Controls.Add(this.rbFuncSin);
            this.gbFuncs.Location = new System.Drawing.Point(16, 91);
            this.gbFuncs.Name = "gbFuncs";
            this.gbFuncs.Size = new System.Drawing.Size(176, 119);
            this.gbFuncs.TabIndex = 4;
            this.gbFuncs.TabStop = false;
            this.gbFuncs.Text = "Functions";
            // 
            // rbRandomFunc
            // 
            this.rbRandomFunc.AutoSize = true;
            this.rbRandomFunc.Location = new System.Drawing.Point(7, 89);
            this.rbRandomFunc.Name = "rbRandomFunc";
            this.rbRandomFunc.Size = new System.Drawing.Size(53, 17);
            this.rbRandomFunc.TabIndex = 3;
            this.rbRandomFunc.TabStop = true;
            this.rbRandomFunc.Tag = "";
            this.rbRandomFunc.Text = "Mixed";
            this.rbRandomFunc.UseVisualStyleBackColor = true;
            this.rbRandomFunc.CheckedChanged += new System.EventHandler(this.rbFunc_CheckedChanged);
            // 
            // rbFuncRandom
            // 
            this.rbFuncRandom.AutoSize = true;
            this.rbFuncRandom.Location = new System.Drawing.Point(7, 66);
            this.rbFuncRandom.Name = "rbFuncRandom";
            this.rbFuncRandom.Size = new System.Drawing.Size(97, 17);
            this.rbFuncRandom.TabIndex = 2;
            this.rbFuncRandom.TabStop = true;
            this.rbFuncRandom.Tag = "";
            this.rbFuncRandom.Text = "Random Points";
            this.rbFuncRandom.UseVisualStyleBackColor = true;
            this.rbFuncRandom.CheckedChanged += new System.EventHandler(this.rbFunc_CheckedChanged);
            // 
            // rbFuncDigital
            // 
            this.rbFuncDigital.AutoSize = true;
            this.rbFuncDigital.Location = new System.Drawing.Point(7, 43);
            this.rbFuncDigital.Name = "rbFuncDigital";
            this.rbFuncDigital.Size = new System.Drawing.Size(86, 17);
            this.rbFuncDigital.TabIndex = 1;
            this.rbFuncDigital.TabStop = true;
            this.rbFuncDigital.Tag = "";
            this.rbFuncDigital.Text = "Digital Signal";
            this.rbFuncDigital.UseVisualStyleBackColor = true;
            this.rbFuncDigital.CheckedChanged += new System.EventHandler(this.rbFunc_CheckedChanged);
            // 
            // rbFuncSin
            // 
            this.rbFuncSin.AutoSize = true;
            this.rbFuncSin.Location = new System.Drawing.Point(7, 20);
            this.rbFuncSin.Name = "rbFuncSin";
            this.rbFuncSin.Size = new System.Drawing.Size(51, 17);
            this.rbFuncSin.TabIndex = 0;
            this.rbFuncSin.TabStop = true;
            this.rbFuncSin.Tag = "";
            this.rbFuncSin.Text = "Sin(x)";
            this.rbFuncSin.UseVisualStyleBackColor = true;
            this.rbFuncSin.CheckedChanged += new System.EventHandler(this.rbFunc_CheckedChanged);
            // 
            // nudInterval
            // 
            this.nudInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudInterval.Location = new System.Drawing.Point(109, 42);
            this.nudInterval.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(83, 20);
            this.nudInterval.TabIndex = 3;
            this.nudInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudInterval.ValueChanged += new System.EventHandler(this.nudInterval_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tick Interval:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Amount of Points:";
            // 
            // nudPoints
            // 
            this.nudPoints.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPoints.Location = new System.Drawing.Point(109, 16);
            this.nudPoints.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nudPoints.Name = "nudPoints";
            this.nudPoints.Size = new System.Drawing.Size(83, 20);
            this.nudPoints.TabIndex = 0;
            this.nudPoints.ValueChanged += new System.EventHandler(this.nudPoints_ValueChanged);
            // 
            // chart1
            // 
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.LineColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.CursorX.LineWidth = 0;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.SkyBlue;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Transparent;
            chartArea1.CursorY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.CursorY.LineWidth = 0;
            chartArea1.CursorY.SelectionColor = System.Drawing.Color.SkyBlue;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(209, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(605, 602);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart";
            this.chart1.DoubleClick += new System.EventHandler(this.chart1_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 602);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(830, 640);
            this.Name = "Form1";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbFuncs.ResumeLayout(false);
            this.gbFuncs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPoints;
        private System.Windows.Forms.NumericUpDown nudInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbFuncs;
        private System.Windows.Forms.RadioButton rbFuncRandom;
        private System.Windows.Forms.RadioButton rbFuncDigital;
        private System.Windows.Forms.RadioButton rbFuncSin;
        private Chart chart1;
        private System.Windows.Forms.RadioButton rbRandomFunc;

    }
}

