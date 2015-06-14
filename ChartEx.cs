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
    


    class ChartEx
    {
        public Chart WrappedChart { get; private set; }

        public ChartEx(Chart chart){
            this.WrappedChart = chart;
            Series = new SeriesCollectionEx(chart.Series);
            Series.PointsWillBeAdded += PointsWillBeAdded;
        }

        public SeriesCollectionEx Series { get; private set; }

        private void PointsWillBeAdded(Series destSeries, int index, List<PointF> pointsToAdd)
        {
            Console.WriteLine("Here we goes to interrupt adding");
           // destSeries.Points.DataBindXY(pointsToAdd, "X", pointsToAdd, "Y");
        }


    }

    class SeriesCollectionEx
    {
        private SeriesCollection collection;
        

        public SeriesCollectionEx(SeriesCollection collection)
        {
            this.collection = collection;
        }

        public SeriesEx Add(String name){
            SeriesEx s = new SeriesEx(collection.Add(name));
            s.PointsWillBeAdded += PointsWillBeAddedHandler;
            return s;
        }
       
        public void Add(SeriesEx series)
        {
            collection.Add(series.WrappedSeries);
            series.PointsWillBeAdded += PointsWillBeAddedHandler;
        }
        public delegate void PointsWillBeAddedEvent(Series destSeries, int index, List<PointF> pointsToAdd);
        public event PointsWillBeAddedEvent PointsWillBeAdded;
        private void OnPointsWillBeAdded(Series destSeries, List<PointF> pointsToAdd)
        {
            if (PointsWillBeAdded != null)
                PointsWillBeAdded(destSeries, collection.IndexOf(destSeries), pointsToAdd);
        }
        private void PointsWillBeAddedHandler(Series destSeries, List<PointF> pointsToAdd)
        {
            OnPointsWillBeAdded(destSeries, pointsToAdd);
        }
    }

    class SeriesEx{

        public Series WrappedSeries { get; private set; }

        public SeriesEx(string name) : this(new Series(name)) {}

        public SeriesEx(string name, int yValues) : this(new Series(name, yValues)) {}

        public SeriesEx(Series series)
        {
            this.WrappedSeries = series;
            Points = new DataPointCollectionEx(WrappedSeries.Points);
            Points.PointsWillBeAdded += PointsWillBeAddedHandler;
        }
        
        public DataPointCollectionEx Points {get; private set;}

        public delegate void PointsWillBeAddedEvent(Series destSeries, List<PointF> pointsToAdd);
        public event PointsWillBeAddedEvent PointsWillBeAdded;
        private void OnPointsWillBeAdded(List<PointF> pointsToAdd)
        {
            if (PointsWillBeAdded != null)
                PointsWillBeAdded(WrappedSeries, pointsToAdd);
        }
        private void PointsWillBeAddedHandler(object sender, List<PointF> pointsToAdd)
        {
            OnPointsWillBeAdded(pointsToAdd);
        }

    }

    class DataPointCollectionEx
    {
        public DataPointCollection WrappedPoints { get; private set; }
        private List<PointF> tmpPoints; // collection used to store all points which will be added. (So far there isn't defined public constructor we must use whole Series).
        
        public DataPointCollectionEx(DataPointCollection points)
        {
            this.WrappedPoints = points;
            tmpPoints = new List<PointF>();
        }

        public event EventHandler<List<PointF>> PointsWillBeAdded;
        private void OnPointsWillBeAdded(List<PointF> pointsToAdd)
        {
            if (PointsWillBeAdded != null)
                PointsWillBeAdded(this, pointsToAdd);
        }
        

        #region Overriden methods to add data
     
        public void AddXY(float xValue, float yValue)
        {
            tmpPoints.Clear();
            tmpPoints.Add(new PointF(xValue, yValue));
            OnPointsWillBeAdded(tmpPoints);
        }

        public void AddXY(PointF point)
        {
            tmpPoints.Clear();
            tmpPoints.Add(point);
            OnPointsWillBeAdded(tmpPoints);
        }

        public void DataBind(IEnumerable<PointF> dataSource)
        {
            tmpPoints.Clear();
            tmpPoints.AddRange(dataSource);
            OnPointsWillBeAdded(tmpPoints);
        }

        #endregion

    }
}
