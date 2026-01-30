Imports dotnetCHARTING.WinForms
Imports System.Drawing

Public Class SandBoxChartManager
    Private _ChartElements As New List(Of String)

    Public Property ChartElements() As List(Of String)
        Get
            Return _ChartElements
        End Get
        Set(ByVal value As List(Of String))
            _ChartElements = value
        End Set
    End Property


    Private _ChartData As DataTable
    Public Property ChartData() As DataTable
        Get
            Return _ChartData
        End Get
        Set(ByVal value As DataTable)
            _ChartData = value
        End Set
    End Property

    Private _ChartControl As dotnetCHARTING.WinForms.Chart
    Public WriteOnly Property ChartControl() As dotnetCHARTING.WinForms.Chart
        Set(ByVal value As dotnetCHARTING.WinForms.Chart)
            _ChartControl = value
        End Set
    End Property

    Private _ChartHeader As String
    Public Property ChartHeader() As String
        Get
            Return _ChartHeader
        End Get
        Set(ByVal value As String)
            _ChartHeader = value
        End Set
    End Property

    Private _ListOfYAxis As New List(Of SandBoxAxis)

    Public Property ListOfYAxis() As List(Of SandBoxAxis)
        Get
            Return _ListOfYAxis
        End Get
        Set(ByVal value As List(Of SandBoxAxis))
            _ListOfYAxis = value
        End Set
    End Property
    Public Sub New(ByRef ChartControl As dotnetCHARTING.WinForms.Chart, ByRef ChartData As DataTable, ByRef ChartElements As List(Of String), ByRef ChartHeader As String, ByRef ListOfYAxis As List(Of SandBoxAxis))
        Me._ChartControl = ChartControl
        Me._ChartData = ChartData
        Me._ChartElements = ChartElements
        Me.ChartHeader = ChartHeader
        Me.ListOfYAxis = ListOfYAxis
    End Sub
    Public Sub New(ByRef ChartControl As dotnetCHARTING.WinForms.Chart)
        Me._ChartControl = ChartControl
    End Sub

	Public Sub CreateChartOnRSCPandEconCount(ByVal chartType As IOSChartType, ByVal DefaultseriesType As SeriesType, ByVal seriesType As SeriesType, ByVal size As Integer)
		Try
			Me._ChartControl.XAxis.TickLabelMode = TickLabelMode.Angled
			Me._ChartControl.XAxis.TickLabelAngle = 45
			Me._ChartControl.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Range

			Me._ChartControl.LegendBox.DefaultEntry.Value = ""
			Me._ChartControl.DefaultElement.Hotspot.ToolTip = Me.ChartElements(0) + ": %XValue" & Chr(13) & "%SeriesName: %Value "
			GenerateChart(chartType, DefaultseriesType, seriesType, size)
		Catch ex As Exception
		End Try

	End Sub
	Public Sub CreateChartOnTimeStamp(ByVal chartType As IOSChartType, ByVal DefaultseriesType As SeriesType, ByVal seriesType As SeriesType, ByVal size As Integer)
		Try
			Me._ChartControl.XAxis.TickLabelMode = TickLabelMode.Angled
			Me._ChartControl.XAxis.TickLabelAngle = 45
			If ChartElements(0) = "TimeStamp" Then
				Me._ChartControl.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
				Me._ChartControl.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
				Me._ChartControl.XAxis.TimeScaleLabels.RangeIntervals.Clear()
				Me._ChartControl.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
				Me._ChartControl.XAxis.FormatString = "HH:mm:ss:fff"
				Me._ChartControl.XAxis.TimeScaleLabels.MinuteFormatString = "HH:mm"
				Me._ChartControl.XAxis.TimeInterval = TimeInterval.Seconds
				Me._ChartControl.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Minutes)
				Me._ChartControl.LegendBox.DefaultEntry.Value = ""
				Me._ChartControl.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
			Else
				Me._ChartControl.DefaultElement.Hotspot.ToolTip = "%XValue" & Chr(13) & "%SeriesName: %Value "
			End If
			GenerateChart(chartType, DefaultseriesType, seriesType, size)
		Catch ex As Exception
		End Try
	End Sub
	Public Sub CreateYAxisMarker(ByVal data As DataTable, ByVal mSize As Line, ByVal ColumnForLabel As String, ByVal markerColumnName As String, Optional ByVal IsClearOld As Boolean = False, Optional ByVal isLegand As Boolean = False, Optional ByVal alternateColumnsForLabel As String = "")
		If (IsClearOld) Then
			Me._ChartControl.YAxis.Markers.Clear()
		End If

		If Not data Is Nothing Then
			For Each dr As DataRow In data.Rows
				Try
					Dim label As String = Convert.ToString(dr(ColumnForLabel))
					If String.IsNullOrEmpty(label) Then
						label = Convert.ToString(dr(alternateColumnsForLabel))
					End If
					Dim marker As New AxisMarker(label, mSize, dr(markerColumnName))

					marker.LegendEntry.Visible = isLegand
					marker.Label.Hotspot.ToolTip = label
					marker.Label.Color = Color.Empty
					marker.Label.Alignment = StringAlignment.Near
					marker.Label.LineAlignment = StringAlignment.Far
					marker.BringToFront = True
					Me._ChartControl.YAxis.Markers.Add(marker)
				Catch ex As Exception
				End Try
			Next
		End If
		'Me._ChartControl.RefreshChart()

	End Sub
	Public Sub CreateXaxisMarker(ByVal data As DataTable, ByVal mSize As Line, ByVal ColumnForLabel As String, ByVal markerColumnName As String, Optional ByVal IsClearOld As Boolean = False, Optional ByVal isLegand As Boolean = False, Optional ByVal alternateColumnsForLabel As String = "")
		If (IsClearOld) Then
			Me._ChartControl.XAxis.Markers.Clear()
		End If

		If Not data Is Nothing Then
			For Each dr As DataRow In data.Rows
				Try
					Dim label As String = Convert.ToString(dr(ColumnForLabel))
					If String.IsNullOrEmpty(label) Then
						label = Convert.ToString(dr(alternateColumnsForLabel))
					End If
					Dim marker As New AxisMarker(label, mSize, dr(markerColumnName))

					marker.LegendEntry.Visible = isLegand
					marker.Label.Hotspot.ToolTip = label
					marker.Label.Color = Color.Empty
					marker.Label.Alignment = StringAlignment.Near
					marker.Label.LineAlignment = StringAlignment.Far
					marker.BringToFront = True
					Me._ChartControl.XAxis.Markers.Add(marker)
				Catch ex As Exception
				End Try
			Next
		End If
		'Me._ChartControl.RefreshChart()

	End Sub
	Private Function String2DataFields2(ByRef str() As String, ByRef xval As String) As String
		Dim stroutput As String
		Dim i As Integer

		stroutput = "XValue=" & xval ' a(0)
		For i = 1 To UBound(str)
			stroutput = stroutput & "," & " Yvalue=" & str(i)
		Next
		String2DataFields2 = stroutput
	End Function
	Private Sub GenerateChart(ByVal chartType As IOSChartType, ByVal DefaultseriesType As SeriesType, ByVal seriesType As SeriesType, ByVal size As Integer)
		Me._ChartControl.Type = chartType
		Me._ChartControl.DefaultSeries.Type = DefaultseriesType
		'ch.DefaultSeries.EmptyElement.Mode = EmptyElementMode.Fill

		Me._ChartControl.Annotations.Clear()

		Me._ChartControl.TitleBox.HeaderLabel.Text = Me.ChartHeader
		Me._ChartControl.TitleBox.Label.Alignment = StringAlignment.Near
		Me._ChartControl.TitleBox.Label.LineAlignment = StringAlignment.Near

		Dim de As DataEngine = New DataEngine(Me.ChartData)
		de.DataFields = String2DataFields2(ChartElements.ToArray, ChartElements(0))
		de.DataGridFormatString = "N2"

		Dim sc As New SeriesCollection
		sc = de.GetSeries()

		Dim i As Integer
		For i = 0 To sc.Count() - 1
			sc(i).Type = seriesType
			sc(i).Line.Width = 3
			sc(i).DefaultElement.Marker.Size = size

			For index As Integer = 0 To Me.ListOfYAxis.Count - 1
				If Me.ListOfYAxis(index).ElementListToApply.Contains(ChartElements(i + 1)) Then
					sc(i).YAxis = Me.ListOfYAxis(index)
					sc(i).DefaultElement.Marker.Type = Me.ListOfYAxis(index).ElementMarkerType
					Exit For
				End If
			Next
		Next
		Me._ChartControl.SeriesCollection.Clear()
		Me._ChartControl.SeriesCollection.Add(sc)
		Me._ChartControl.Series.Data = Me._ChartData
		sc = Nothing
		de = Nothing

		'Me._ChartControl.RefreshChart()

	End Sub

End Class
