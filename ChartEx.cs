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

    delegate void PointsWillBeAddedEvent(SeriesEx series, List<PointF> storedPoints, List<PointF> pointsToAdd);

    class ChartEx
    {
        /// <summary>
        /// Gets the underlying original Chart object
        /// </summary>
        public Chart WrappedChart { get; private set; }

        public int MaxRenderedPoints { get; set; }

        public bool ApproximationEnabled { get; set; }

        public ChartEx(Chart chart){
            this.WrappedChart = chart;
            Series = new SeriesCollectionEx(WrappedChart.Series);
            Series.PointsWillBeAdded += PointsWillBeAdded;

            MaxRenderedPoints = 1000;
            ApproximationEnabled = true;
        }

        public SeriesCollectionEx Series { get; private set; }

        private void PointsWillBeAdded(SeriesEx series, List<PointF> storedPoints, List<PointF> pointsToAdd)
        {

            List<PointF> resultPoints = new List<PointF>();
            //1
            if (ApproximationEnabled && storedPoints.Count > MaxRenderedPoints)
            {
                //3
                int step = (int)Math.Ceiling((double)storedPoints.Count / MaxRenderedPoints); // calculate step of approximation
                resultPoints.AddRange(storedPoints.Where((x, i) => i % step == 0));
            }
            else
                resultPoints.AddRange(storedPoints);
            series.WrappedSeries.Points.DataBindXY(resultPoints, "X", resultPoints, "Y");
            
            /*
             * Approximation:
             * 1. Check total number of points (MAX_RENDERED_POINTS)
             * 2. If less than max => draw them all. Otherwise continue optimization.
             * 3. Measure drawing area.
             * 4. Determine necessary number of points.
             * 5. Approximate points.
             * 
             * Zooming:
             * 1. If number of points less than max Apply approximation to zommed area.
             * 2. Otherwise find range of displayed points. 
             * 3. Get that range in original points array.
             * 4. Insert points to the drawing points collection.
             */
        }


        private int[] GetVisiblePointsRange(Series series)
        {
            // two shortcuts
            ChartArea area = WrappedChart.ChartAreas[0];

            // these are the X-Values of the zoomed portion:
            double min = area.AxisX.ScaleView.ViewMinimum;
            double max = area.AxisX.ScaleView.ViewMaximum;

            // these are the respective DataPoints:
            DataPoint pt0 = series.Points.FirstOrDefault(x => x.XValue >= min);
            DataPoint pt1 = series.Points.LastOrDefault(x => x.XValue <= max);

            return new[] {series.Points.IndexOf(pt0), series.Points.IndexOf(pt1)};
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

        public SeriesEx Add(String name){

            SeriesEx s = collection.FirstOrDefault(x => name.Equals(x.WrappedSeries.Name));
            if (s == null)
            {
                s = new SeriesEx(WrappedCollection.Add(name));
                s.PointsWillBeAdded += PointsWillBeAddedHandler;
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
                series.PointsWillBeAdded += PointsWillBeAddedHandler;
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

        public event PointsWillBeAddedEvent PointsWillBeAdded;
        private void OnPointsWillBeAdded(SeriesEx series, List<PointF> storedPoints, List<PointF> pointsToAdd)
        {
            if (PointsWillBeAdded != null)
                PointsWillBeAdded(series, storedPoints, pointsToAdd);
        }
        private void PointsWillBeAddedHandler(SeriesEx series, List<PointF> storedPoints, List<PointF> pointsToAdd)
        {
            OnPointsWillBeAdded(series, storedPoints, pointsToAdd);
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
            Points.PointsWillBeAdded += PointsWillBeAddedHandler;
        }
        

        public event PointsWillBeAddedEvent PointsWillBeAdded;
        private void OnPointsWillBeAdded(List<PointF> storedPoints, List<PointF> pointsToAdd)
        {
            if (PointsWillBeAdded != null)
                PointsWillBeAdded(this, storedPoints, pointsToAdd);
            else
                Console.WriteLine("There is no event handler for the SeriesEx named '" + Name + "'. You may forgot to add SeriesEx to the ChartEx before adding points into it.");
        }
        private void PointsWillBeAddedHandler(SeriesEx series, List<PointF> storedPoints, List<PointF> pointsToAdd)
        {
            OnPointsWillBeAdded(storedPoints, pointsToAdd);
        }

    }

    class DataPointCollectionEx
    {
        private List<PointF> tmpPoints; // collection used to store all points which will be added. (So far there isn't defined public constructor we must use whole Series).
        private List<PointF> mainPoints; // collection of stored points
        
        public DataPointCollectionEx()
        {
            tmpPoints = new List<PointF>();
            mainPoints = new List<PointF>();
        }

        
        public event PointsWillBeAddedEvent PointsWillBeAdded;
        private void OnPointsWillBeAdded()
        {
            if (PointsWillBeAdded != null)
                PointsWillBeAdded(null, mainPoints, tmpPoints);
        }

        //void AllowAdd()
        //{
        //    AllowAdd(0, tmpPoints.Count);
        //}
        //void AllowAdd(int fromIndex, int amount = 0)
        //{
        //    if (fromIndex < 0) fromIndex = 0;
        //    if (amount <= 0)
        //    {
        //        if (tmpPoints.Count == 0) return;
        //        else amount = tmpPoints.Count; 
        //    }

        //    if (fromIndex == 0 && (amount == tmpPoints.Count || amount == 0))
        //        mainPoints.AddRange(tmpPoints);
        //    else
        //        mainPoints.AddRange(tmpPoints.Skip(fromIndex).Take(amount));
        //    tmpPoints.Clear();
        //}

        #region Overriden methods to add data
     
        public void Add(float xValue, float yValue)
        {
            Add(new PointF(xValue, yValue));
        }

        public void Add(PointF point)
        {
            tmpPoints.Clear();
            tmpPoints.Add(point);
            mainPoints.Add(point);
            OnPointsWillBeAdded();
        }

        public void AddRange(IEnumerable<PointF> dataSource)
        {
            tmpPoints.Clear();
            tmpPoints.AddRange(dataSource);
            mainPoints.AddRange(dataSource);
            OnPointsWillBeAdded();
        }

        public void Clear()
        {
            mainPoints.Clear();
        }
        #endregion

    }
}
