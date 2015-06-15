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
        private int pointsAmount = 1000;
        private int pointsRange = 5000;
        private IPointsFunc[] pointsFuncs = new IPointsFunc[] { new PointsFuncDigital(2), new PointsFuncRandom(2), new PointsFuncSin() };
        public enum Funcs { SIN, DIGITAL, RANDOM, MIXED}
        private Funcs funcs;
        private long time;

        private ChartEx chartEx;

        public Form1()
        {
            InitializeComponent();
            chartEx = new ChartEx(chart1);
            rbFuncSin.Checked = true;

            nudPoints.Value = pointsAmount;
            nudInterval.Value = timer1.Interval;
            nudValuesRange.Value = pointsRange;
            nudMaxPoints.Value = chartEx.MaxRenderedPoints;
            cbApproximate.Checked = chartEx.ApproximationEnabled;
            timer1.Start();
            
            
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
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Tick!");
            time += timer1.Interval;
            Generate();
        }

        private void Generate()
        {
            PointF[][] pts = new PointF[12][];
            for (int i = 0; i < 12; i++)
            {
                pts[i] = GetFunc(i).CreatePoints(pointsAmount, pointsRange, 0, i * 5, time);
            }

            chartEx.Series.Clear();
            //chart1.Series.Clear();
            for (int i = 0; i < 12; i++)
            {
                SeriesEx s = new SeriesEx(i.ToString());
                s.WrappedSeries.ChartType = SeriesChartType.Line;
                chartEx.Series.Add(s);
                s.Points.AddRange(pts[i]);

                //Series s = new Series(i.ToString());
                //s.ChartType = SeriesChartType.Line;
                //chart1.Series.Add(s);
                //s.Points.DataBindXY(pts[i], "X", pts[i], "Y");

            }
            Console.WriteLine("Points added.");
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
                }
            }
        }

        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);
        }

        private void nudValuesRange_ValueChanged(object sender, EventArgs e)
        {
            pointsRange = (int)nudValuesRange.Value;
            Generate();
        }

        private void cbApproximate_CheckedChanged(object sender, EventArgs e)
        {
            chartEx.ApproximationEnabled = cbApproximate.Checked;
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
        }
    }
}
