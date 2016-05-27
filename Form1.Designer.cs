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
            this.gbSeries = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudSeries = new System.Windows.Forms.NumericUpDown();
            this.clbSeries = new System.Windows.Forms.CheckedListBox();
            this.gbTimer = new System.Windows.Forms.GroupBox();
            this.bTimer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.gbApproximation = new System.Windows.Forms.GroupBox();
            this.cbDash = new System.Windows.Forms.CheckBox();
            this.cbApproximate = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudMaxPoints = new System.Windows.Forms.NumericUpDown();
            this.gbPoints = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudDelta = new System.Windows.Forms.NumericUpDown();
            this.nudValuesRange = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudPoints = new System.Windows.Forms.NumericUpDown();
            this.gbFuncs = new System.Windows.Forms.GroupBox();
            this.rbRaw = new System.Windows.Forms.RadioButton();
            this.rbRandomFunc = new System.Windows.Forms.RadioButton();
            this.rbFuncRandom = new System.Windows.Forms.RadioButton();
            this.rbFuncDigital = new System.Windows.Forms.RadioButton();
            this.rbFuncSin = new System.Windows.Forms.RadioButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            this.gbSeries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeries)).BeginInit();
            this.gbTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.gbApproximation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPoints)).BeginInit();
            this.gbPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValuesRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints)).BeginInit();
            this.gbFuncs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbSeries);
            this.panel1.Controls.Add(this.gbTimer);
            this.panel1.Controls.Add(this.gbApproximation);
            this.panel1.Controls.Add(this.gbPoints);
            this.panel1.Controls.Add(this.gbFuncs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 748);
            this.panel1.TabIndex = 1;
            // 
            // gbSeries
            // 
            this.gbSeries.Controls.Add(this.label4);
            this.gbSeries.Controls.Add(this.nudSeries);
            this.gbSeries.Controls.Add(this.clbSeries);
            this.gbSeries.Location = new System.Drawing.Point(12, 465);
            this.gbSeries.Name = "gbSeries";
            this.gbSeries.Size = new System.Drawing.Size(231, 271);
            this.gbSeries.TabIndex = 8;
            this.gbSeries.TabStop = false;
            this.gbSeries.Text = "Series";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Amount of Series:";
            // 
            // nudSeries
            // 
            this.nudSeries.Location = new System.Drawing.Point(142, 24);
            this.nudSeries.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudSeries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeries.Name = "nudSeries";
            this.nudSeries.Size = new System.Drawing.Size(83, 20);
            this.nudSeries.TabIndex = 2;
            this.nudSeries.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeries.ValueChanged += new System.EventHandler(this.nudSeries_ValueChanged);
            // 
            // clbSeries
            // 
            this.clbSeries.CheckOnClick = true;
            this.clbSeries.FormattingEnabled = true;
            this.clbSeries.Location = new System.Drawing.Point(7, 66);
            this.clbSeries.Name = "clbSeries";
            this.clbSeries.Size = new System.Drawing.Size(218, 199);
            this.clbSeries.TabIndex = 0;
            this.clbSeries.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbSeries_ItemCheck);
            // 
            // gbTimer
            // 
            this.gbTimer.Controls.Add(this.bTimer);
            this.gbTimer.Controls.Add(this.label2);
            this.gbTimer.Controls.Add(this.nudInterval);
            this.gbTimer.Location = new System.Drawing.Point(12, 354);
            this.gbTimer.Name = "gbTimer";
            this.gbTimer.Size = new System.Drawing.Size(231, 94);
            this.gbTimer.TabIndex = 7;
            this.gbTimer.TabStop = false;
            this.gbTimer.Text = "Timer Properties";
            // 
            // bTimer
            // 
            this.bTimer.BackColor = System.Drawing.Color.Honeydew;
            this.bTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTimer.Location = new System.Drawing.Point(7, 55);
            this.bTimer.Name = "bTimer";
            this.bTimer.Size = new System.Drawing.Size(218, 33);
            this.bTimer.TabIndex = 4;
            this.bTimer.Text = "Timer";
            this.bTimer.UseVisualStyleBackColor = false;
            this.bTimer.Click += new System.EventHandler(this.bTimer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tick Interval:";
            // 
            // nudInterval
            // 
            this.nudInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudInterval.Location = new System.Drawing.Point(142, 24);
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
            // gbApproximation
            // 
            this.gbApproximation.Controls.Add(this.cbDash);
            this.gbApproximation.Controls.Add(this.cbApproximate);
            this.gbApproximation.Controls.Add(this.label5);
            this.gbApproximation.Controls.Add(this.nudMaxPoints);
            this.gbApproximation.Location = new System.Drawing.Point(12, 123);
            this.gbApproximation.Name = "gbApproximation";
            this.gbApproximation.Size = new System.Drawing.Size(231, 100);
            this.gbApproximation.TabIndex = 6;
            this.gbApproximation.TabStop = false;
            this.gbApproximation.Text = "Approximation Properties";
            // 
            // cbDash
            // 
            this.cbDash.AutoSize = true;
            this.cbDash.Location = new System.Drawing.Point(110, 72);
            this.cbDash.Name = "cbDash";
            this.cbDash.Size = new System.Drawing.Size(93, 17);
            this.cbDash.TabIndex = 2;
            this.cbDash.Text = "Draw as Dash";
            this.cbDash.UseVisualStyleBackColor = true;
            this.cbDash.CheckedChanged += new System.EventHandler(this.cbDash_CheckedChanged);
            // 
            // cbApproximate
            // 
            this.cbApproximate.AutoSize = true;
            this.cbApproximate.Location = new System.Drawing.Point(110, 49);
            this.cbApproximate.Name = "cbApproximate";
            this.cbApproximate.Size = new System.Drawing.Size(115, 17);
            this.cbApproximate.TabIndex = 2;
            this.cbApproximate.Text = "Approximate points";
            this.cbApproximate.UseVisualStyleBackColor = true;
            this.cbApproximate.CheckedChanged += new System.EventHandler(this.cbApproximate_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Max allowed points:";
            // 
            // nudMaxPoints
            // 
            this.nudMaxPoints.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudMaxPoints.Location = new System.Drawing.Point(142, 23);
            this.nudMaxPoints.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nudMaxPoints.Name = "nudMaxPoints";
            this.nudMaxPoints.Size = new System.Drawing.Size(83, 20);
            this.nudMaxPoints.TabIndex = 0;
            this.nudMaxPoints.ValueChanged += new System.EventHandler(this.nudMaxPoints_ValueChanged);
            // 
            // gbPoints
            // 
            this.gbPoints.Controls.Add(this.label6);
            this.gbPoints.Controls.Add(this.label3);
            this.gbPoints.Controls.Add(this.nudDelta);
            this.gbPoints.Controls.Add(this.nudValuesRange);
            this.gbPoints.Controls.Add(this.label1);
            this.gbPoints.Controls.Add(this.nudPoints);
            this.gbPoints.Location = new System.Drawing.Point(12, 12);
            this.gbPoints.Name = "gbPoints";
            this.gbPoints.Size = new System.Drawing.Size(231, 105);
            this.gbPoints.TabIndex = 5;
            this.gbPoints.TabStop = false;
            this.gbPoints.Text = "Points Properties";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Delta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Values Range:";
            // 
            // nudDelta
            // 
            this.nudDelta.DecimalPlaces = 2;
            this.nudDelta.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDelta.Location = new System.Drawing.Point(142, 74);
            this.nudDelta.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudDelta.Name = "nudDelta";
            this.nudDelta.Size = new System.Drawing.Size(83, 20);
            this.nudDelta.TabIndex = 3;
            this.nudDelta.ValueChanged += new System.EventHandler(this.nudDelta_ValueChanged);
            // 
            // nudValuesRange
            // 
            this.nudValuesRange.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudValuesRange.Location = new System.Drawing.Point(142, 49);
            this.nudValuesRange.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudValuesRange.Name = "nudValuesRange";
            this.nudValuesRange.Size = new System.Drawing.Size(83, 20);
            this.nudValuesRange.TabIndex = 0;
            this.nudValuesRange.ValueChanged += new System.EventHandler(this.nudValuesRange_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Amount of Points:";
            // 
            // nudPoints
            // 
            this.nudPoints.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPoints.Location = new System.Drawing.Point(142, 23);
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
            // gbFuncs
            // 
            this.gbFuncs.Controls.Add(this.rbRaw);
            this.gbFuncs.Controls.Add(this.rbRandomFunc);
            this.gbFuncs.Controls.Add(this.rbFuncRandom);
            this.gbFuncs.Controls.Add(this.rbFuncDigital);
            this.gbFuncs.Controls.Add(this.rbFuncSin);
            this.gbFuncs.Location = new System.Drawing.Point(12, 229);
            this.gbFuncs.Name = "gbFuncs";
            this.gbFuncs.Size = new System.Drawing.Size(231, 119);
            this.gbFuncs.TabIndex = 4;
            this.gbFuncs.TabStop = false;
            this.gbFuncs.Text = "Functions";
            // 
            // rbRaw
            // 
            this.rbRaw.AutoSize = true;
            this.rbRaw.Location = new System.Drawing.Point(110, 20);
            this.rbRaw.Name = "rbRaw";
            this.rbRaw.Size = new System.Drawing.Size(71, 17);
            this.rbRaw.TabIndex = 4;
            this.rbRaw.TabStop = true;
            this.rbRaw.Text = "Raw data";
            this.rbRaw.UseVisualStyleBackColor = true;
            this.rbRaw.CheckedChanged += new System.EventHandler(this.rbFunc_CheckedChanged);
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
            this.chart1.Location = new System.Drawing.Point(253, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(672, 748);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 748);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(830, 640);
            this.Name = "Form1";
            this.Text = "ChartEx Demo";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.gbSeries.ResumeLayout(false);
            this.gbSeries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeries)).EndInit();
            this.gbTimer.ResumeLayout(false);
            this.gbTimer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.gbApproximation.ResumeLayout(false);
            this.gbApproximation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPoints)).EndInit();
            this.gbPoints.ResumeLayout(false);
            this.gbPoints.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValuesRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoints)).EndInit();
            this.gbFuncs.ResumeLayout(false);
            this.gbFuncs.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbPoints;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudValuesRange;
        private System.Windows.Forms.GroupBox gbApproximation;
        private System.Windows.Forms.CheckBox cbApproximate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudMaxPoints;
        private System.Windows.Forms.GroupBox gbTimer;
        private System.Windows.Forms.Button bTimer;
        private System.Windows.Forms.CheckBox cbDash;
        private System.Windows.Forms.GroupBox gbSeries;
        private System.Windows.Forms.CheckedListBox clbSeries;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudSeries;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudDelta;
        private System.Windows.Forms.RadioButton rbRaw;

    }
}

