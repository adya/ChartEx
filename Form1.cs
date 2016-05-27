using GLAR.Windows.Forms.DataVisualization.Charting.ChartEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AsyncChart
{
    public partial class Form1 : Form
    {
        private static Random rnd = new Random(Environment.TickCount);
        private int pointsAmount = 5000;
        private int pointsRange = 6000;
        private int seriesAmount = 12;
        private float delta = 1000;
        private bool[] enabledSeries;
        private IPointsFunc[] pointsFuncs = new IPointsFunc[] { new PointsFuncDigital(2), new PointsFuncRandom(2), new PointsFuncSin(), new PointsFuncRawData()};
        public enum Funcs { SIN, DIGITAL, RANDOM, RAW, MIXED}
        private Funcs funcs;
        private long time;

        private ChartEx chartEx;

        public Form1()
        {
            InitializeComponent();
            chartEx = new ChartEx(chart1);
            rbRaw.Checked = true;
            CreateEnabledSeries();
            Generate();
            UpdateCheckList();
            nudPoints.Value = pointsAmount;
            nudInterval.Value = timer1.Interval;
            nudValuesRange.Value = pointsRange;
            nudMaxPoints.Value = chartEx.MaxRenderedPoints;
            nudSeries.Value = seriesAmount;
            nudDelta.Value = (decimal)delta;
            cbApproximate.Checked = chartEx.ApproximationEnabled;
            cbDash.Checked = chartEx.UseDashLines;
         //   timer1.Start();
            bTimer.Text = (timer1.Enabled ? "Stop" : "Start");
            bTimer.BackColor = (timer1.Enabled ? Color.IndianRed : Color.LimeGreen);
        }

        private IPointsFunc GetFunc(int i)
        {
            switch (funcs)
            {
                default:
                case Funcs.SIN:
                    return pointsFuncs[2];
                case Funcs.DIGITAL:
                    return pointsFuncs[0];
                case Funcs.RANDOM:
                    return pointsFuncs[1];
                case Funcs.MIXED:
                    return pointsFuncs[i%3];
                case Funcs.RAW:
                    return pointsFuncs[3];
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += timer1.Interval;
            Generate();
        }

        private void Generate()
        {
            PointF[][] pts = new PointF[12][];
            chartEx.Series.Clear();
            for (int i = 0; i < seriesAmount; i++)
            {
                SeriesEx s = new SeriesEx(i.ToString());
                s.WrappedSeries.ChartType = SeriesChartType.Line;
                chartEx.Series.Add(s);
                s.WrappedSeries.Enabled = enabledSeries[i];
                if (enabledSeries[i]){
                    pts[i] = GetFunc(i).CreatePoints(pointsAmount, pointsRange, 0, (i+1) * 10, time);
                    s.Points.AddRange(pts[i]);
                }
            }
        }

        private void CreateEnabledSeries()
        {

            if (enabledSeries != null && enabledSeries.Length != seriesAmount)
            {
                bool[] tmp = new bool[seriesAmount];
                int length = Math.Min(enabledSeries.Length, seriesAmount);
                for (int i = 0; i < length; i++)
                {
                    tmp[i] = enabledSeries[i];
                }
                enabledSeries = tmp;
            }
            else
            {
                enabledSeries = new bool[seriesAmount];
                for (int i = 0; i < seriesAmount; i++)
                {
                    enabledSeries[i] = true;
                }
            }
        }

        private void UpdateCheckList()
        {
            clbSeries.Items.Clear();
            for (int i = 0; i < seriesAmount; i++)
            {
                clbSeries.Items.Add("Series " + i.ToString(), enabledSeries[i]);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            chart1.Size = new Size(this.Width - chart1.Left, this.Height - chart1.Top - 30);
        }

        private void nudPoints_ValueChanged(object sender, EventArgs e)
        {
            pointsAmount = (int)nudPoints.Value;
            Generate();
        }

        private void nudInterval_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)nudInterval.Value;
        }

        private void rbFunc_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                switch (rb.Name)
                {
                    case "rbFuncSin": funcs = Funcs.SIN; break;
                    case "rbFuncDigital": funcs = Funcs.DIGITAL; break;
                    case "rbFuncRandom": funcs = Funcs.RANDOM; break;
                    case "rbRandomFunc": funcs = Funcs.MIXED; break;
                    case "rbRaw": funcs = Funcs.RAW; break;
                }
            }
        }

        private void nudValuesRange_ValueChanged(object sender, EventArgs e)
        {
            pointsRange = (int)nudValuesRange.Value;
            chartEx.WrappedChart.ChartAreas[0].AxisX.Minimum = 0;
            chartEx.WrappedChart.ChartAreas[0].AxisX.Maximum = pointsRange;
            Generate();
        }

        private void cbApproximate_CheckedChanged(object sender, EventArgs e)
        {
            chartEx.ApproximationEnabled = cbApproximate.Checked;
        }

        private void cbDash_CheckedChanged(object sender, EventArgs e)
        {
            chartEx.UseDashLines = cbDash.Checked;
        }

        private void nudMaxPoints_ValueChanged(object sender, EventArgs e)
        {
            chartEx.MaxRenderedPoints = (int)nudMaxPoints.Value;
        }

        private void bTimer_Click(object sender, EventArgs e)
        {
            
            if (timer1.Enabled)
                timer1.Stop();
            else
                timer1.Start();
            bTimer.Text = (timer1.Enabled ? "Stop" : "Start");
            bTimer.BackColor = (timer1.Enabled ? Color.IndianRed : Color.LimeGreen);
        }

        private void clbSeries_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            enabledSeries[e.Index] = (e.NewValue == CheckState.Checked);
            chartEx.Series[e.Index].WrappedSeries.Enabled = enabledSeries[e.Index]; 

        }

        private void nudSeries_ValueChanged(object sender, EventArgs e)
        {
            seriesAmount = (int)nudSeries.Value;
            CreateEnabledSeries();
            Generate();
            UpdateCheckList();
        }

        private void nudDelta_ValueChanged(object sender, EventArgs e)
        {
            delta = (float)nudDelta.Value;
            chartEx.SelectionMethod = new ChartExDeltaPeekSelector(delta);
        }

        
    }
}
