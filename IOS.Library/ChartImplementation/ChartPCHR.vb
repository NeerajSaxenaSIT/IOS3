Imports dotnetCHARTING.WinForms
Imports System.Windows.Forms
Imports System.Drawing

Public Class ChartPCHR

    Public Sub RadioBarChart(ByRef chartObje As dotnetCHARTING.WinForms.Chart, ByRef dt As DataTable, ByVal chartTitle As String)
        ChartComman.ChartDataClear(chartObje)
        chartObje.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        chartObje.SuspendLayout()
        chartObje.TempDirectory = "temp"
        chartObje.Dock = DockStyle.Fill

        chartObje.Title = chartTitle
        chartObje.Debug = True
        chartObje.DefaultElement.ToolTip = "%SeriesName (%Name)"
        chartObje.DefaultSeries.Line.Width = 2

        chartObje.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        chartObje.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        chartObje.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        chartObje.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        chartObje.CleanupPeriod = 1

        chartObje.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
        Dim yaxis As New Axis
        yaxis.MinimumInterval = 1.0
        yaxis.Orientation = dotnetCHARTING.WinForms.Orientation.Left
        yaxis.Label.Text = dt.Columns(1).ColumnName.ToString
        yaxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
        yaxis.NumberPrecision = 1

        Dim de As DataEngine = New DataEngine(dt)
        de.DataFields = "XValue=" & dt.Columns(0).ColumnName.ToString & ", Yvalue=" & dt.Columns(1).ColumnName.ToString
        Dim sc As New SeriesCollection
        sc = de.GetSeries()

        chartObje.SeriesCollection.Clear()
        For i = 0 To sc.Count() - 1
            sc(i).YAxis = yaxis
            'sc(i).DefaultElement.Marker.Type = i
        Next
        chartObje.SeriesCollection.Add(sc)
        sc = Nothing
        de = Nothing
        chartObje.RefreshChart()
        chartObje.ResumeLayout()
    End Sub
    Public Sub RadioBubbleChart(ByRef chartObj As dotnetCHARTING.WinForms.Chart, ByRef dt As DataTable, ByVal chartTitle As String)
        ChartComman.ChartDataClear(chartObj)
        chartObj.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        chartObj.SuspendLayout()
        chartObj.Title = chartTitle
        chartObj.TempDirectory = "temp"
        chartObj.Debug = True
        chartObj.Type = ChartType.Bubble
        chartObj.Use3D = False
        chartObj.MaximumBubbleSize = 20

        ''chartObj.DefaultAxis.NumberPrecision = 1
        '' chartObj.DefaultSeries.DefaultElement.ShowValue = True
        ''chartObj.DefaultSeries.DefaultElement.LabelTemplate = "(%Xvalue,%Yvalue)"
        chartObj.DefaultSeries.DefaultElement.ForceMarker = False
        chartObj.Palette = New Color() {Color.Blue, Color.Red, Color.Yellow}
        chartObj.YAxis.Label.Text = dt.Columns(1).ColumnName.ToString
        '.Select.Take(15).CopyToDataTable
        chartObj.SeriesCollection.Add(GetBubbleSeriesData(dt, "Cpich_EcNo_ActiveSet", "Bin_RSCP", "CountSamples")) ''GetPieSeriesData(dt, "Cpich_EcNo_ActiveSet")) ''Bin_Rscp



        chartObj.RefreshChart()
        chartObj.ResumeLayout()

    End Sub
   
    Public Sub OverviewCellBarChart(ByVal chartObj As dotnetCHARTING.WinForms.Chart, ByVal dt As DataTable, ByVal yLable As String, ByVal chartTitle As String)
        ' Dim chartObj As Chart = chartObj
        ChartComman.ChartDataClear(chartObj)
        chartObj.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        chartObj.SuspendLayout()
        chartObj.TempDirectory = "temp"
        chartObj.Dock = DockStyle.Fill
        chartObj.Title = chartTitle
        chartObj.Debug = True
        chartObj.DefaultElement.ToolTip = "%SeriesName (%Name)"
        chartObj.DefaultSeries.Line.Width = 2
        chartObj.LegendBox.Visible = False
        'chartObj.XAxis.Label.Text = "Cell"

        ' chart1.DefaultElement.Marker.Visible = False
        chartObj.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        'chartObj.LegendBox.DefaultEntry.Value = ""
        chartObj.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        chartObj.XAxis.TickLabelMode = TickLabelMode.Angled
        chartObj.XAxis.TickLabelAngle = 45
        'chartObj.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        'chartObj.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
        'nc.ToolTip.InitialDelay = 1
        chartObj.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        chartObj.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        chartObj.CleanupPeriod = 1

        chartObj.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
        Dim yaxis As New Axis
        yaxis.MinimumInterval = 1.0
        yaxis.Orientation = dotnetCHARTING.WinForms.Orientation.Left
        yaxis.Label.Text = yLable ''"Error Count per Cell"
        yaxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
        yaxis.NumberPrecision = 1

        chartObj.LegendBox.Template = "%icon %name"
        Dim de As DataEngine = New DataEngine(dt)
        de.DataFields = "XValue=" & dt.Columns(0).ColumnName.ToString & ", Yvalue=" & dt.Columns(1).ColumnName.ToString
        Dim sc As New SeriesCollection
        sc = de.GetSeries()

        chartObj.SeriesCollection.Clear()
        For i = 0 To sc.Count() - 1
            sc(i).YAxis = yaxis
            'sc(i).DefaultElement.Marker.Type = i
        Next

        chartObj.SeriesCollection.Add(sc)
        sc = Nothing
        de = Nothing
        chartObj.RefreshChart()
        chartObj.ResumeLayout()
    End Sub

    Public Sub OverviewPieChart(ByRef _chartObj As dotnetCHARTING.WinForms.Chart, ByRef dt As DataTable, ByVal chartTitle As String)
        ChartComman.ChartDataClear(_chartObj)
        _chartObj.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        If (dt.Rows.Count <= 0) Then
            Exit Sub
        End If
        _chartObj.SuspendLayout()
        _chartObj.Title = chartTitle
        _chartObj.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        _chartObj.DefaultSeries.DefaultElement.ShowValue = True
        _chartObj.PieLabelMode = PieLabelMode.Inside
        _chartObj.Use3D = False
        _chartObj.Type = ChartType.Pie
        _chartObj.ShadingEffect = False
        _chartObj.DefaultSeries.DefaultElement.Transparency = 20
        _chartObj.TempDirectory = "temp"
        _chartObj.SeriesCollection.Clear()
        _chartObj.SeriesCollection.Add(GetPieSeriesData(dt, dt.Columns(0).ColumnName)) ''"causeValue"
        _chartObj.LegendBox.Template = "%icon %name"
        _chartObj.RefreshChart()
        _chartObj.ResumeLayout()
    End Sub
    Private Function GetPieSeriesData(ByRef dt As DataTable, ByVal seriesColumnName As String) As SeriesCollection
        Dim SC As New SeriesCollection()
        Dim a As Integer
        For a = 0 To dt.Rows.Count - 1
            Dim s As New Series()
            s.Name = dt.Rows(a)(seriesColumnName)
            Dim b As Integer
            For b = 0 To dt.Rows.Count - 1
                Dim e As New Element()
                e.Name = "Element " & b
                e.YValue = dt.Rows(a)(1)
                s.Elements.Add(e)
            Next b
            SC.Add(s)
        Next a
        Return SC
    End Function
    Function GetBubbleSeriesData(ByRef dt As DataTable, ByVal yAxisColumnName As String, ByVal xAxisColumnName As String, ByVal bubbleSizeColumnName As String) As SeriesCollection
        Dim SC As New SeriesCollection()
        Dim myR As New Random(1)
        Dim a As Integer
        For a = 0 To 1
            Dim s As New Series()
            ''s.Name = "Series " & a
            Dim b As Integer
            For b = 0 To dt.Rows.Count - 1
                Dim e As New Element()
                ''e.Name = "Element " & b
                e.YValue = dt.Rows(b)(yAxisColumnName)
                e.XValue = dt.Rows(b)(xAxisColumnName)
                e.BubbleSize = dt.Rows(b)(bubbleSizeColumnName)
                s.Elements.Add(e)
            Next b
            SC.Add(s)
        Next a

        ' Set Different Colors for our Series
        'SC(0).DefaultElement.Color = Color.FromArgb(49, 255, 49)
        'SC(1).DefaultElement.Color = Color.FromArgb(255, 255, 0)
        'SC(2).DefaultElement.Color = Color.FromArgb(255, 99, 49)
        'SC(3).DefaultElement.Color = Color.FromArgb(0, 156, 255)

        Return SC
    End Function 'getRandomData

    Public Sub OverviewErrorChart(ByRef dt As DataTable, ByRef chartObje As dotnetCHARTING.WinForms.Chart, ByVal chartTitle As String)
        Try
            ChartComman.ChartDataClear(chartObje)
            chartObje.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            chartObje.SuspendLayout()

            chartObje.TempDirectory = "temp"
            chartObje.Dock = DockStyle.Fill

            chartObje.Title = chartTitle
            'chartObje.Debug = True
            chartObje.DefaultElement.ToolTip = "%SeriesName (%Name)"
            chartObje.DefaultSeries.Line.Width = 2

            chartObje.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
            chartObje.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
            chartObje.ShadingEffect = True
            If (dt IsNot Nothing) Then
                chartObje.SeriesCollection.Add(GetOverviewErrorChartSeries(dt))
            End If

            chartObje.Type = ChartType.ComboHorizontal
            chartObje.RefreshChart()
            chartObje.ResumeLayout()
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetOverviewErrorChartSeries(ByRef dt As DataTable) As SeriesCollection

        Dim dv As New DataView(dt)
        Dim dtGroup As DataTable = dv.ToTable(True, New String() {dt.Columns(0).ColumnName})
        Dim colName As String = dt.Columns(0).ColumnName
        Dim SC As New SeriesCollection()
        For Each faulttyperow As DataRow In dtGroup.Rows
            Dim seriesrows As DataRow() = dt.Select(colName & "='" + faulttyperow(0).ToString + "'")
            Dim s As New Series()
            s.Name = faulttyperow(0).ToString

            For Each sr As DataRow In seriesrows
                Dim e As New Element()
                e.Name = sr(1)
                e.YValue = sr(2)
                s.Elements.Add(e)
            Next
            SC.Add(s)
        Next
        Return SC
    End Function


End Class
