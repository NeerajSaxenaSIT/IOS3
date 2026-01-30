Imports IOS.Configuration.ChartAdjust
Imports dotnetCHARTING.WinForms

Public Class dlgChartAdjustAxis

#Region "Variables"

    Private _tech As String = ""
    Private objFrmTechnology As frmTechnology = Nothing

#End Region

#Region "Properties"

    Private _chartAdjustProperties As ChartAdjustProperties
    Public Property SetChartAdjustProperties() As ChartAdjustProperties
        Get
            Return _chartAdjustProperties
        End Get
        Set(ByVal value As ChartAdjustProperties)
            _chartAdjustProperties = value
        End Set
    End Property

    Private _chart As dotnetCHARTING.WinForms.Chart
    Public Property SetChart() As dotnetCHARTING.WinForms.Chart
        Get
            Return _chart
        End Get
        Set(ByVal value As dotnetCHARTING.WinForms.Chart)
            _chart = value
        End Set
    End Property

    Private _isLeft As Boolean
    Public Property IsLeft() As Boolean
        Get
            Return _isLeft
        End Get
        Set(ByVal value As Boolean)
            _isLeft = value
        End Set
    End Property

    Private _seriesIndex As Integer
    Public Property SeriesIndex() As Integer
        Get
            Return _seriesIndex
        End Get
        Set(ByVal value As Integer)
            _seriesIndex = value
        End Set
    End Property

    Public Property Tech() As String
        Get
            Return _tech
        End Get
        Set(ByVal value As String)
            _tech = value
        End Set
    End Property

#End Region

#Region "Form & Control Event"

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (_chartAdjustProperties IsNot Nothing) Then
            _chartAdjustProperties.IsLeft = _isLeft
            PropGrid_ChartAdjustAxis.SelectedObject = _chartAdjustProperties
        End If
    End Sub

    Private Sub PropertyGrid_ChartAdjustAxis_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles PropGrid_ChartAdjustAxis.PropertyValueChanged
        Try
            Dim changedPropertyItem As GridItem = e.ChangedItem
            If (Not changedPropertyItem Is Nothing) Then

                objFrmTechnology = Nothing
                If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(_tech)) Then
                    frmMDI.OpenTechFormDynamically(_tech, objFrmTechnology, False)
                Else
                    objFrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(Tech)).LastOrDefault()
                End If

                Dim seriesChart As Series = _chart.SeriesCollection(_seriesIndex)
                If (_isLeft) Then

                    If (changedPropertyItem.Label.ToLower = "axislabeltext") Then
                        seriesChart.YAxis.Label.Text = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "axislabelfont") Then
                        seriesChart.YAxis.Label.Font = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "scaleauto") Then
                        If (changedPropertyItem.Value) Then
                            objFrmTechnology.SetChartScaleAuto(_chart, changedPropertyItem.Value)
                            _chartAdjustProperties.ScaleMaximum = Convert.ToDouble(objFrmTechnology.GetChartScaleOrgValues(_chart).Split(",")(0))
                            _chart.YAxis.ScaleRange.ValueHigh = _chartAdjustProperties.ScaleMaximum
                            _chartAdjustProperties.ScaleMinimum = Convert.ToDouble(objFrmTechnology.GetChartScaleOrgValues(_chart).Split(",")(1))
                            _chart.YAxis.ScaleRange.ValueLow = _chartAdjustProperties.ScaleMinimum
                        Else
                            objFrmTechnology.SetChartScaleAuto(_chart, changedPropertyItem.Value)
                            objFrmTechnology.SetChartScaleOrgValues(_chart, _chartAdjustProperties.ScaleMaximum & "," & _chartAdjustProperties.ScaleMinimum)
                        End If
                    ElseIf (changedPropertyItem.Label.ToLower = "scaleminimum") Then
                        _chart.YAxis.ScaleRange.ValueLow = changedPropertyItem.Value
                        seriesChart.YAxis.Minimum = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "scalemaximum") Then
                        _chart.YAxis.ScaleRange.ValueHigh = changedPropertyItem.Value
                        seriesChart.YAxis.Maximum = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "interval") Then
                        seriesChart.YAxis.Interval = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "numberprecision") Then
                        seriesChart.YAxis.NumberPrecision = changedPropertyItem.Value
                    End If
                Else
                    If (changedPropertyItem.Label.ToLower = "axislabeltext") Then
                        seriesChart.YAxis.Label.Text = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "axislabelfont") Then
                        seriesChart.YAxis.Label.Font = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "scaleauto") Then
                        If (changedPropertyItem.Value) Then
                            objFrmTechnology.SetChartScaleAuto(_chart, changedPropertyItem.Value)
                            _chartAdjustProperties.ScaleMaximum = Convert.ToDouble(objFrmTechnology.GetChartScaleOrgValues(_chart).Split(",")(0))
                            _chart.YAxis.ScaleRange.ValueHigh = _chartAdjustProperties.ScaleMaximum
                            _chartAdjustProperties.ScaleMinimum = Convert.ToDouble(objFrmTechnology.GetChartScaleOrgValues(_chart).Split(",")(1))
                            _chart.YAxis.ScaleRange.ValueLow = _chartAdjustProperties.ScaleMinimum
                        Else
                            objFrmTechnology.SetChartScaleAuto(_chart, changedPropertyItem.Value)
                            objFrmTechnology.SetChartScaleOrgValues(_chart, _chartAdjustProperties.ScaleMaximum & "," & _chartAdjustProperties.ScaleMinimum)
                        End If
                    ElseIf (changedPropertyItem.Label.ToLower = "scaleminimum") Then
                        _chart.YAxis.ScaleRange.ValueLow = changedPropertyItem.Value
                        seriesChart.YAxis.Minimum = changedPropertyItem.Value

                    ElseIf (changedPropertyItem.Label.ToLower = "scalemaximum") Then
                        _chart.YAxis.ScaleRange.ValueHigh = changedPropertyItem.Value
                        seriesChart.YAxis.Maximum = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "interval") Then
                        seriesChart.YAxis.Interval = changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "numberprecision") Then
                        seriesChart.YAxis.NumberPrecision = changedPropertyItem.Value
                    End If
                End If
                _chart.RefreshChart()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function Chart_Axis_MinMax(ByVal sc As SeriesCollection, ByVal orientaxis As Orientation) As Double()
        Dim minval As Double = 1000000
        Dim maxval As Double = 0
        Dim minval_stacked As Double = 0
        Dim maxval_stacked As Double = 0

        For Each s As Series In sc
            If s.YAxis.Orientation = orientaxis Then
                If s.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked Then
                    For Each el As Element In s.Elements
                        maxval_stacked = Math.Max(maxval_stacked, Convert.ToDouble(el.YValue))
                    Next
                    minval = 0
                    maxval = (maxval_stacked + maxval)
                    maxval_stacked = 0
                ElseIf s.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal Or s.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Range Then
                    For Each el As Element In s.Elements
                        minval = Math.Min(minval, Convert.ToDouble(el.YValue))
                        maxval = Math.Max(maxval, Convert.ToDouble(el.YValue))
                    Next
                    minval = minval * 0.95
                    maxval = maxval * 1.05

                ElseIf s.YAxis.Scale = dotnetCHARTING.WinForms.Scale.FullStacked Then
                    minval = 0
                    maxval = 100
                End If
            End If
        Next

        Dim output(2) As Double
        output(0) = minval
        output(1) = maxval
        Return output
    End Function

#End Region

End Class