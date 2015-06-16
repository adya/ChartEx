using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace GLAR.Windows.Forms.DataVisualization.Charting.ChartEx
{
    
    /// <summary>
    /// ChartEx and underlying classes designed as wrappers to the original chart in order to be able to preview points which will be added.
    /// Note: This could be done easier if Microsoft allows overriding of their methods.
    /// </summary>


    ///WARNING: You must Add series BEFORE Adding points into that series. Otherwise Points can't be observed by ChartEx.

    delegate void PointsAddedEvent(SeriesEx series);

    class ChartEx
    {
        /// <summary>
        /// Gets the underlying original Chart object.
        /// </summary>
        public Chart WrappedChart { get; private set; }

        /// <summary>
        /// Determines the maximum number of points which can be drawn on the chart.
        /// </summary>
        public int MaxRenderedPoints { get; set; }

        /// <summary>
        /// Enables or disables approximation.
        /// </summary>
        public bool ApproximationEnabled { get; set; }

        /// <summary>
        /// Indicates whether the approximated lines should be drawn as dashed lines.
        /// </summary>
        public bool UseDashLines { get; set; }

        /// <summary>
        /// Collection of the SeriesEx as a wrapper for Series.
        /// </summary>
        public SeriesCollectionEx Series { get; private set; }

        private int[] detailedRange; // remember detailed range when zooming (to be able to approximate when adding)

        public ChartEx(Chart chart){
            this.WrappedChart = chart;
            Series = new SeriesCollectionEx(WrappedChart.Series);
            Series.PointsAdded += PointsAdded;

            MaxRenderedPoints = 1000;
            ApproximationEnabled = true;
            UseDashLines = true;

            chart.AxisViewChanged+=AxisViewChanged;
            chart.MouseDoubleClick +=MouseDoubleClick;
        }

        private void MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!WrappedChart.ChartAreas[0].AxisX.ScaleView.IsZoomed) return;
            for (int i = 0; i < Series.Count; i++)
            {
                List<PointF> res = ApproximateSeries(Series[i]);
                Series[i].WrappedSeries.Points.DataBindXY(res, "X", res, "Y");
            }
            WrappedChart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            WrappedChart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            detailedRange = null; // clear detailed range
          
        }

        
        private void AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (!e.Axis.Equals(WrappedChart.ChartAreas[0].AxisX))
                return; // if we aren't dealing with X axis ignore that.
            AxisScaleView view = WrappedChart.ChartAreas[0].AxisX.ScaleView;
            if (view.IsZoomed)
            {
                for (int i = 0; i < Series.Count; i++)
                {
                    ZoomApproximation(Series[i]);
                }
            }
            else
            {
                for (int i = 0; i < Series.Count; i++)
                {
                    ApproximateSeries(Series[i]);
                }
            }
            
        }


        private void PointsAdded(SeriesEx series)
        {
            List<PointF> res = ApproximateSeries(series, detailedRange);
            series.WrappedSeries.Points.DataBindXY(res, "X", res, "Y");
        }
        private void ZoomApproximation(SeriesEx s)
        {
            if (!ApproximationEnabled) return;

            List<PointF> storedPoints = s.Points.Items;
            if (storedPoints.Count > MaxRenderedPoints)
            {
                int[] actualIndexes = GetVisiblePointsRange(s);
                if (actualIndexes[0] >= 0 && actualIndexes[1] >= 0)
                {
                    int actualPointsCount = actualIndexes[1] - actualIndexes[0];
                   // List<PointF> resultPoints = (List<PointF>)storedPoints.GetRange(actualIndexes[0], actualPointsCount);
                    List<PointF> res = ApproximateSeries(s, storedPoints, actualIndexes);
                    s.WrappedSeries.Points.DataBindXY(res, "X", res, "Y");
                }

            }
           
        }

        private List<PointF> ApproximateSeries(SeriesEx series, int[] detailedRanges = null)
        {
            return ApproximateSeries(series, series.Points.Items, detailedRanges);
        }

        private List<PointF> ApproximateSeries(SeriesEx series, List<PointF> candidatePoints, int[] detailedRange = null)
        {
            List<PointF> resultPoints = new List<PointF>();
            int detailedRangeWidth = ((detailedRange != null && detailedRange.Length == 2) ? detailedRange[1]  - detailedRange[0] : 0);
            if (ApproximationEnabled && candidatePoints.Count > MaxRenderedPoints)
            {
                int step = GetStep(candidatePoints.Count - detailedRangeWidth);
                if (detailedRangeWidth == 0)
                {
                    resultPoints.AddRange(candidatePoints.Where((x, i) => i % step == 0));
                    if (UseDashLines)
                        series.WrappedSeries.BorderDashStyle = ChartDashStyle.Dash;
                }
                else
                {
                    //IEnumerable<PointF> left = candidatePoints.Where((x, i) => i < detailedRanges[0] && i % step == 0);
                    //IEnumerable<PointF> right = candidatePoints.Where((x, i) => i > detailedRanges[1] && i % step == 0);
                    this.detailedRange = detailedRange;
                    resultPoints.Add(candidatePoints.First());
                    resultPoints.AddRange(ApproximateSeries(series, candidatePoints.GetRange(detailedRange[0], detailedRangeWidth)));
                    resultPoints.Add(candidatePoints.Last());
                }
                
            }
            else
            {
                resultPoints.AddRange(candidatePoints);
                if (UseDashLines)
                    series.WrappedSeries.BorderDashStyle = ChartDashStyle.Solid;
            }
            return resultPoints;
        }

        private int GetStep(int amountOfPoints)
        {
            return (int)Math.Ceiling((double)amountOfPoints / MaxRenderedPoints);
        }

        /// <summary>
        /// Retrieves absolute range from all points.
        /// </summary>
        private int[] GetVisiblePointsRange(SeriesEx series)
        {
            // two shortcuts
            ChartArea area = WrappedChart.ChartAreas[0];

            // these are the X-Values of the zoomed portion:
            double minX = area.AxisX.ScaleView.ViewMinimum;
            double maxX = area.AxisX.ScaleView.ViewMaximum;

            double size = area.AxisX.Maximum - area.AxisX.Minimum;
            if (size <= minX || size <= maxX)
                return new int[] { -1, -1 };
            else
                return new int[] { (int)((minX / size) * series.Points.Items.Count), (int)((maxX / size) * series.Points.Items.Count) };
        }

    }

    class SeriesCollectionEx
    {

        private List<SeriesEx> collection;
        public SeriesCollection WrappedCollection { get; private set; }

        public SeriesCollectionEx(SeriesCollection collection)
        {
            this.collection = new List<SeriesEx>();
            WrappedCollection = collection;
        }

        public int Count { get { return collection.Count; } }

        public SeriesEx Add(String name){

            SeriesEx s = collection.FirstOrDefault(x => name.Equals(x.WrappedSeries.Name));
            if (s == null)
            {
                s = new SeriesEx(WrappedCollection.Add(name));
                s.PointsAdded += PointsAddedHandler;
                collection.Add(s);
                
            }
            return s;
        }
       
        public void Add(SeriesEx series)
        {
            SeriesEx s = collection.FirstOrDefault(x => series.WrappedSeries.Name.Equals(x.WrappedSeries.Name));
            if (s == null)
            {
                collection.Add(series);
                WrappedCollection.Add(series.WrappedSeries);
                series.PointsAdded += PointsAddedHandler;
            }
        }

        public SeriesEx this[int i]
        {
            get { return collection[i]; }
        }

        public SeriesEx this[string name]
        {
            get 
            { 
                return collection.FirstOrDefault(x=> name.Equals(x.WrappedSeries.Name));
            }
        }

        public void Clear()
        {
            WrappedCollection.Clear();
            collection.Clear();
        }

        public event PointsAddedEvent PointsAdded;
        private void OnPointsAdded(SeriesEx series)
        {
            if (PointsAdded != null)
                PointsAdded(series);
        }
        private void PointsAddedHandler(SeriesEx series)
        {
            OnPointsAdded(series);
        }
    }

    class SeriesEx{

        /// <summary>
        /// Gets reference to the wrapped series. User this only to set properties of the series. And do NOT set points through this getter.
        /// </summary>
        public Series WrappedSeries { get; private set; }

        /// <summary>
        /// Gets Points collection. User it for adding points.
        /// </summary>
        public DataPointCollectionEx Points { get; private set; }

        public string Name { get { return WrappedSeries.Name; } set { WrappedSeries.Name = value; } }

        public SeriesEx(string name) : this(new Series(name)) { }

        public SeriesEx(string name, int yValues) : this(new Series(name, yValues)) { }

        public SeriesEx(Series series)
        {
            this.WrappedSeries = series;
            Points = new DataPointCollectionEx();
            Points.PointsAdded += PointsAddedHandler;
        }
        

        public event PointsAddedEvent PointsAdded;
        private void OnPointsAdded()
        {
            if (PointsAdded != null)
                PointsAdded(this);
            else
                Console.WriteLine("There is no event handler for the SeriesEx named '" + Name + "'. You may forgot to add SeriesEx to the ChartEx before adding points into it.");
        }
        private void PointsAddedHandler(SeriesEx series)
        {
            OnPointsAdded();
        }

    }

    class DataPointCollectionEx
    {
        private List<PointF> points; // collection of stored points
        
        public DataPointCollectionEx()
        {
            points = new List<PointF>();
        }

        public List<PointF> Items { get { return points; } }
        
        public event PointsAddedEvent PointsAdded;
        private void OnPointsAdded()
        {
            if (PointsAdded != null)
                PointsAdded(null);
        }

        #region Overriden methods to add data
     
        public void Add(float xValue, float yValue)
        {
            Add(new PointF(xValue, yValue));
        }

        public void Add(PointF point)
        {
            points.Add(point);
            OnPointsAdded();
        }

        public void AddRange(IEnumerable<PointF> dataSource)
        {
            points.AddRange(dataSource);
            OnPointsAdded();
        }

        public void Clear()
        {
            points.Clear();
        }
        #endregion

    }
}
