# ChartEx
`ChartEx` is a WinForms `Chart` wrapper which optimizes rendering of huge portions of data.

# What it does
`ChartEx` optimizes displaying of huge portions of data on a single `Chart` Control from .NET WinForms Framework.
While simple `Chart` doesn't allow threading it will take some time for `Chart` to render huge portions of data, `ChartEx` wraps the `Chart` and efficiently solves the problem.

<details> 
  <summary> See it in action. Rendering of 6 series with 10,000 points in each. Update-rate 100 ms. </summary>
   ![Image](https://raw.githubusercontent.com/adya/ChartEx/master/wiki/ce.gif)
</details>
<details> 
  <summary> See it in action. Scaling of 6 series with 10,000 points in each. Update-rate 100 ms. </summary>
   ![Image](https://raw.githubusercontent.com/adya/ChartEx/master/wiki/ce2.gif)
</details>

# Hot it works
`ChartEx` utilized all data management and provides a way to specify one of the predefined or custom data approximation algorithms.
The key for this problem was to reduce amount of points to be rendered (since points were lose too each other and most of them were rendered on the top of each other which is redundant). 
These algorithms determines major and minor data array peeks and renders on the `Chart` only major points.
Furthermore, it behaves in the same way when scaling the `Chart`: if there are still lots of points `ChartEx` will apply approximation to the scaled area.

# How to use

## Wrap the `Chart` object
```csharp
  Chart chart = new Chart();
  ChartEx chartEx = new ChartEx(chart);
```

## Set approximation algorithm (Optional)
Approximation algorithm is provided by `IChartExSelector` interface.
```csharp
  chartEx.SelectionMethod = new MyApproximationSelector();
```

You can set either predefined selector or create your own.

a) `ChartEx` comes with predefined `IChartExSelector`'s:
* `ChartExSimpleSelector` - Evenly selects points; **(default selector)**
* `ChartExPeekSelector` - Selects stationary points;
* `ChartExDeltaPeekSelector` - Selects stationary points which have delta between two consecutive points greater than specified threshold value;
* `ChartExSavitzkyGolaySelector` - Selects points using [Savitzky–Golay filter](https://en.wikipedia.org/wiki/Savitzky%E2%80%93Golay_filter);

b) To create custom selector you have to implement `IChartExSelector`. 
Example:
```csharp
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
```

## Add series and points

* You can create `SeriesEx` as you usually do with regular `Series`
```csharp
  SeriesEx seriesEx = new SeriesEx("Name"); // SeriesEx wraps regular Series.
```
* Or wrap an existing series
```csharp
  Series series = new Series("Regular");
  SeriesEx seriesEx = new SeriesEx(series);
```
* Add created seriesEx
```csharp
  chartEx.Series.Add(seriesEx);
```
> Points added through `Add` method of the regular series will not be observable and therefore `ChartEx` won't refresh approximation.

## Customize `Chart` and `Series`
Since `ChartEx` and `SeriesEx` were designed as wrappers you are able to access their wrapped objects by `WrappedChart` and `WrappedSeries` respectivelyю

For example, change series `ChartType` to line:
```csharp
  seriesEx.WrappedSeries.ChartType = SeriesChartType.Line;
```
Or set range of chart's X-axis:
```csharp
  chartEx.WrappedChart.ChartAreas[0].AxisX.Minimum = 0;
  chartEx.WrappedChart.ChartAreas[0].AxisX.Maximum = 100;
```
