using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GLAR.Windows.Forms.DataVisualization.Charting.ChartEx
{
    
    /// <summary>
    /// ChartEx and underlying classes designed as wrappers to the original chart in order to be able to preview points which will be added.
    /// Note: This could be done easier if Microsoft would allow overriding of their methods.
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
        /// Gets or Sets selection method used to select "valuable" points during approximation. <para/>
        /// Note: Any custom Selector must guarantee that it will select no more than MaxRenderedPoints points. Otherwise selected list of points will be forcibly processed by SimpleSelector.
        /// </summary>
        public IChartExSelector SelectionMethod { get; set; }

        /// <summary>
        /// Gets default selection method. See <see cref="ChartEx.SelectionMethod"/>.
        /// </summary>
        public static IChartExSelector DefaultSelectionMethod { get { return new ChartExPeekSelector(); } }

        /// <summary>
        /// Enables or Disables Right Click Pan.
        /// </summary>
        public bool RightClickPan
        { 
            get
            { 
                return rightClickPan; 
            } 
            set
            {
                rightClickPan = value;
                if (rightClickPan)
                {
                    WrappedChart.MouseMove += MouseMove;
                    WrappedChart.MouseUp += MouseUp;
                    WrappedChart.MouseDown += MouseDown;
                }
                else
                {
                    WrappedChart.MouseMove -= MouseMove;
                    WrappedChart.MouseUp -= MouseUp;
                    WrappedChart.MouseDown -= MouseDown;
                }
                    
            } 
        }

        /// <summary>
        /// Enables or Disables Double Click to reset zooming.
        /// </summary>
        public bool DoubleClickResetZoom
        {
            get
            {
                return doubleClickResetZoom;
            }
            set
            {
                doubleClickResetZoom = value;
                if (doubleClickResetZoom)
                    WrappedChart.MouseDoubleClick += MouseDoubleClick;
                else
                    WrappedChart.MouseDoubleClick -= MouseDoubleClick;
            }
        }

        /// <summary>
        /// Collection of the SeriesEx as a wrapper for Series.
        /// </summary>
        public SeriesCollectionEx Series { get; private set; }

        private int[] detailedRange; // stores detailed range when zooming (to be able to approximate when adding)
        private int lastMousePosX; // used for pan.
        private bool rightClickPan;
        private bool doubleClickResetZoom;

        public ChartEx(Chart chart){
            this.WrappedChart = chart;
            Series = new SeriesCollectionEx(WrappedChart.Series);
            Series.PointsAdded += PointsAdded;

            MaxRenderedPoints = 1000;
            ApproximationEnabled = true;
            UseDashLines = true;

            SelectionMethod = DefaultSelectionMethod;

            WrappedChart.AxisViewChanged+= AxisViewChanged;
            RightClickPan = true;
            DoubleClickResetZoom = true;
        }

        #region Mouse Handlers
        private void MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePosX = e.X; // remember initial position;
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            lastMousePosX = 0; // clear mouse position;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (!WrappedChart.ChartAreas[0].AxisX.ScaleView.IsZoomed) return;
            int delta = e.X - lastMousePosX;
            lastMousePosX = e.X;
            AxisScaleView view = WrappedChart.ChartAreas[0].AxisX.ScaleView;
            double start = view.ViewMinimum - delta;
            view.Scroll(start);
            for (int i = 0; i < Series.Count; i++)
            {
                ZoomApproximation(Series[i]);
            }
        }


        private void MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
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
        #endregion


        // Events which initiate approximation.

        private void AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (!e.Axis.Equals(WrappedChart.ChartAreas[0].AxisX))
                return; // if we aren't dealing with X axis ignore that.
            AxisScaleView view = WrappedChart.ChartAreas[0].AxisX.ScaleView;
            if (view.IsZoomed)
            {
                for (int i = 0; i < Series.Count; i++)
                    ZoomApproximation(Series[i]);
            }
            else
            {
                for (int i = 0; i < Series.Count; i++)
                    ApproximateSeries(Series[i]);
            }
            
        }

        private void PointsAdded(SeriesEx series)
        {
            List<PointF> res = ApproximateSeries(series, detailedRange);
            series.WrappedSeries.Points.DataBindXY(res, "X", res, "Y");
        }

        // Main approximation methods

        private void ZoomApproximation(SeriesEx s)
        {
            if (!ApproximationEnabled) return;
            if (!s.WrappedSeries.Enabled) return;
            List<PointF> storedPoints = s.Points.Items;
            if (storedPoints.Count > MaxRenderedPoints)
            {
                int[] actualIndexes = GetVisiblePointsRange(s);
                if (actualIndexes[0] >= 0 && actualIndexes[1] >= 0)
                {
                    int actualPointsCount = actualIndexes[1] - actualIndexes[0];
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
                if (detailedRangeWidth == 0)
                {
                    resultPoints = SelectionMethod.Select(candidatePoints, MaxRenderedPoints);
                    if (resultPoints.Count > MaxRenderedPoints) // ensure the Selector gives us allowed number of points.
                        resultPoints = resultPoints.Take(MaxRenderedPoints).ToList();
                    if (UseDashLines)
                        series.WrappedSeries.BorderDashStyle = ChartDashStyle.Dash;
                }
                else
                {
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

            double minPointsX = series.Points.Items.Min((point)=> point.X);
            double maxPointX = series.Points.Items.Max((point) => point.X);

            double size = maxPointX - minPointsX;
            if (size <= minX || size <= maxX)
                return new int[] { -1, -1 };
            else
                return new int[] { (int)Math.Ceiling((minX / size) * series.Points.Items.Count), (int)Math.Ceiling((maxX / size) * series.Points.Items.Count) };
        }

    }
    #region Wrapper Classes
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

    #endregion

    #region Selector Interfaces and some Selectors.
    /// <summary>
    /// IChartExSelector interface provides method for selection algorithm.
    /// </summary>
    interface IChartExSelector 
    {
        /// <summary>
        /// Selects points from candidate points.
        /// </summary>
        /// <param name="candidatePoints">List of all points in the approximation range. </param>
        /// <param name="maxPoints">Max capacity of the approximated points list. </param>
        /// <returns></returns>
        List<PointF> Select(List<PointF> candidatePoints, int maxPoints);
    }

    /// <summary>
    /// Simple Selector which will evenly select points based on total number of points and approximating range.
    /// </summary>
    class ChartExSimpleSelector : IChartExSelector
    {
        private int GetStep(int amountOfPoints, int maxRenderedPoints)
        {
            return (int)Math.Ceiling((double)amountOfPoints / maxRenderedPoints);
        }

        public List<PointF> Select(List<PointF> candidatePoints, int maxPoints)
        {
            List<PointF> resultPoints = new List<PointF>();
            int step = GetStep(candidatePoints.Count, maxPoints);

            for (int i = 0; i < candidatePoints.Count; i += step)
                resultPoints.Add(candidatePoints[i]);
            return resultPoints;
        }
    }

    /// <summary>
    /// Selector which will select stationary points.
    /// </summary>
    class ChartExPeekSelector : IChartExSelector
    {

        public List<PointF> Select(List<PointF> candidatePoints, int maxPoints)
        {
            List<PointF> resultPoints = new List<PointF>();
            resultPoints.Add(candidatePoints.First());
            for (int i = 1; i < candidatePoints.Count - 1; i++)
            {
                if ((candidatePoints[i - 1].Y <= candidatePoints[i].Y && candidatePoints[i].Y >= candidatePoints[i + 1].Y) ||
                    (candidatePoints[i - 1].Y >= candidatePoints[i].Y && candidatePoints[i].Y <= candidatePoints[i + 1].Y))
                    resultPoints.Add(candidatePoints[i]);
            }
            resultPoints.Add(candidatePoints.Last());
            return resultPoints;
        }
    }
    /// <summary>
    /// Selector which will select stationary points which have delta between two consecutive points greater than specified threshold value.
    /// </summary>
    class ChartExDeltaPeekSelector : IChartExSelector
    {
        private ChartExPeekSelector peekSelector;

        private float delta;

        public ChartExDeltaPeekSelector(float delta)
        {
            this.delta = delta;
            peekSelector = new ChartExPeekSelector();
        }

        public List<PointF> Select(List<PointF> candidatePoints, int maxPoints)
        {
            List<PointF> resultPoints = new List<PointF>();

            resultPoints = peekSelector.Select(candidatePoints, maxPoints);

            int i = 0;
            while (i < resultPoints.Count - 1)
            {
                if (Math.Abs(resultPoints[i].Y - resultPoints[i + 1].Y) <= delta)
                    resultPoints.RemoveAt(i);
                else
                    ++i;
            }

            return resultPoints;
        }
    }
    /// <summary>
    /// Selector which will select points using Savitzky–Golay filter.
    /// </summary>
    class ChartExSavitzkyGolaySelector : IChartExSelector
    {
        public List<PointF> Select(List<PointF> candidatePoints, int maxPoints)
        {
            List<PointF> resultPoints = new List<PointF>();
            for (int i = 4; i < candidatePoints.Count - 4; i+=9)
            {
                resultPoints.Add(new PointF(candidatePoints[i].X,
                                              (-21.0f * candidatePoints[i - 4].Y + 
                                                14.0f * candidatePoints[i - 3].Y + 
                                                39.0f * candidatePoints[i - 2].Y + 
                                                54.0f * candidatePoints[i - 1].Y + 
                                                59.0f * candidatePoints[i].Y + 
                                                54.0f * candidatePoints[i + 1].Y + 
                                                39.0f * candidatePoints[i + 2].Y + 
                                                14.0f * candidatePoints[i + 3].Y +
                                               -21.0f * candidatePoints[i + 4].Y) * 0.004329004329004329004329004329f)); // /231
            }
            return resultPoints;
        }
    }

   
    #endregion
}
