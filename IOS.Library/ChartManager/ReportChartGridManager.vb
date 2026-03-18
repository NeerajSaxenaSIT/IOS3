Imports System.Drawing
Imports System.Windows.Forms
Imports dotnetCHARTING.WinForms
Imports IOS.DataLibrary

Public Class ReportChartGridManager

    Public Shared Sub SetChartProperty(isByTime As Boolean, chartName As String, ByRef nc As dotnetCHARTING.WinForms.Chart)
        Try
            If Not isByTime Then
                nc.Name = chartName
                'nc.AutoSize = false;
                'Chart Default Properties
                nc.DefaultElement.Marker.Visible = False
                nc.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
                nc.LegendBox.DefaultEntry.Value = ""
                nc.LegendBox.DefaultCorner = BoxCorner.Round
                nc.LegendBox.CornerBottomRight = BoxCorner.Round

                nc.XAxis.TickLabelMode = TickLabelMode.Angled
                nc.XAxis.TickLabelAngle = 45
                nc.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                nc.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

                nc.ToolTip.InitialDelay = 1
                nc.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal

                nc.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
            Else
                nc.Name = chartName
                nc.Dock = DockStyle.Fill
                nc.ToolTip.InitialDelay = 1
                'nc.AutoSize = false;
                'Chart Default Properties
                nc.DefaultElement.Marker.Visible = False
                nc.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
                nc.LegendBox.DefaultEntry.Value = ""
                nc.LegendBox.DefaultCorner = BoxCorner.Round
                nc.LegendBox.CornerBottomRight = BoxCorner.Round

                nc.XAxis.TickLabelMode = TickLabelMode.Angled
                nc.XAxis.TickLabelAngle = 45

            End If

            nc.TitleBox.Position = TitleBoxPosition.Full
            nc.TitleBox.CornerTopLeft = BoxCorner.Round
            nc.TitleBox.CornerTopRight = BoxCorner.Round
            nc.TitleBox.Label.AutoWrap = True
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
        End Try

    End Sub

    Public Shared Sub RefreshChartConfig(ByRef dt_chart As DataTable, ByRef dt As DataTable, ByRef chart_ReportChartGrid As dotnetCHARTING.WinForms.Chart, ByRef ReportAxisData As DataTable)
        If (dt_chart Is Nothing) Then
            Exit Sub
        End If
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""

        Dim ch As Chart = Nothing
        Dim i As Integer = 0
        Dim Y1axislabel As String = Nothing
        Dim Y2axislabel As String = Nothing
        Dim Y1axisAbsorPerc As String = Nothing
        Dim Y2axisAbsOrPerc As String = Nothing
        Dim Y1axisPrecision As Integer = 0
        Dim Y2axisPrecision As Integer = 0
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim color_R As Integer = 0
        Dim color_B As Integer = 0
        Dim color_G As Integer = 0

        Dim lastchart As String = ""

        Dim chart_elements As String() = {"0"}
        Dim chart_elementsYAxis As String() = {"0"}
        Dim chart_Eltype As String() = {"Bar"}
        Dim chart_elsort As String() = {"0", "0"}
        Dim chart_ElColor As Integer() = {0}
        Dim chart_YaxisScale As String() = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim axis_LineThickness As String() = {"3"}
        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1

        'Dim drCalculatedSeries As DataRow() = dt_chart.Select(ReportChartFields.Calculated_Series_Type_ID & " is Not null")

        Dim drCalculatedSeries As DataRow() = dt_chart.Select(CalculatedSeriesTypesFields.StatisticsOrThreshold & "=" & "'" & StatisticsOrThreshold.Statistics.ToString & "' Or " & CalculatedSeriesTypesFields.StatisticsOrThreshold & " IS NULL")
        Dim drThresholdSeries As DataRow() = dt_chart.Select(CalculatedSeriesTypesFields.StatisticsOrThreshold & "=" & "'" & StatisticsOrThreshold.Threshold.ToString & "'")

        For rownum = 0 To dt_chart.Rows.Count - 1
            Dim drow As DataRow = dt_chart.Rows(rownum)
            Try
                While Not ColumnInDataTable(drow(ReportChartFields.SeriesName).ToString().Trim(), dt)
                    rownum = rownum + 1
                    If rownum <= dt_chart.Rows.Count - 1 Then
                        drow = dt_chart.Rows(rownum)
                    Else
                        ' TODO: might not be correct. Was : Exit For
                        Exit While

                    End If
                End While
            Catch ex As Exception
            End Try

            Try

                'configures individual chart when new chartline is detected
                If String.IsNullOrEmpty(lastchart) Or lastchart <> drow(ReportChartFields.ChartTitle).ToString() Then
                    lastchart = drow(ReportChartFields.SeriesName).ToString().Trim()
                    Y1axisAbsorPerc = drow(ReportChartFields.AxisAbsPerc).ToString().Trim()
                    Y2axisAbsOrPerc = Utility.nZ(drow(ReportChartFields.AxisAbsPerc), "Abs")
                    Y1axisPrecision = Convert.ToInt32(drow(ReportChartFields.AxisPrecision))
                    '''Y2axisPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                    Y1axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")
                    ''Y2axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")

                    ch = chart_ReportChartGrid
                    SetChartXAxis(Utility.nZ(drow(ReportChartFields.TimeResolution), "Raw"), ch)

                    Dim techPack As String = Utility.nZ(drow(ReportChartFields.TechnologyPackageName), " ")
                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(techPack.ToUpper))
                    If techPack.Length > 3 Then
                        Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                        ch.Annotations(0).Label.Font = fnt
                    End If
                    ch.Tag = techPack
                    ch.TitleBox.Label.Text = drow(ReportChartFields.ChartTitle).ToString().Trim()

                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                    ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Strings.Chr(13) & "%SeriesName: %Value "

                    'Y-Axis Settings   
                    yaxis1 = New Axis()
                    yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    yaxis1.Label.Text = Y1axislabel
                    yaxis2 = New Axis()
                    yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right

                    Do

                        If ColumnInDataTable(drow(ReportChartFields.SeriesName).ToString().Trim(), dt) Then
                            Array.Resize(chart_elements, j + 1)
                            Array.Resize(chart_elementsYAxis, j + 1)
                            Array.Resize(chart_Eltype, j + 1)
                            Array.Resize(chart_ElColor, j + 1)
                            Array.Resize(axis_LineThickness, j + 1)
                            chart_elements(j) = drow(ReportChartFields.SeriesName).ToString().Trim()
                            chart_elementsYAxis(j) = drow(ReportChartFields.AxisLocation).ToString().Trim()
                            chart_Eltype(j) = drow(ReportChartFields.SeriesChartType).ToString().Trim()
                            chart_ElColor(j) = Convert.ToInt32(drow(ReportChartFields.SeriesColor))
                            axis_LineThickness(j) = drow(ReportChartFields.LineSize)
                            If Strings.UCase(chart_elementsYAxis(j)) = "LEFT" Then
                                chart_YaxisScale(0) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                                If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                                    yaxis1.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                                End If

                                If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                                    If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                        yaxis1.Percent = True
                                    End If
                                End If
                                yaxis1.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                                If yaxis1.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then
                                    yaxis1.MinimumInterval = 1
                                End If
                            ElseIf Strings.UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                                chart_YaxisScale(1) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                                If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                                    yaxis2.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                                End If

                                If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                                    If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                        yaxis2.Percent = True
                                    End If
                                End If
                                yaxis2.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                                If yaxis2.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then
                                    yaxis2.MinimumInterval = 1
                                End If
                            End If
                            If drow(ReportChartFields.SortOrder).ToString.Trim <> "" Then
                                chart_elsort(0) = drow(ReportChartFields.SeriesName).trim
                                chart_elsort(1) = drow(ReportChartFields.SortOrder).ToString.ToUpper()
                            End If

                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > dt_chart.Rows.Count - 1 Then
                            ' TODO: might not be correct. Was : Exit Do
                            Exit Do
                        Else
                            drow = dt_chart.Rows(rownum)
                        End If
                    Loop While drow(ReportChartFields.ChartTitle).ToString().Trim() <> lastchart
                    '5
                    rownum = rownum - 1


                    If Strings.UCase(chart_YaxisScale(0)) = "STACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf Strings.UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If
                    If Strings.UCase(chart_YaxisScale(1)) = "STACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf Strings.UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    'ch.XAxis.ScaleRange.ValueHigh = xaxis_valuehigh;
                    'Dim xaxis As String = ""
                    'If ReportAxisData.Rows.Count > 0 Then
                    '    If ReportAxisData(0)("sandBoxFieldType") = 4 Then
                    '        xaxis = "PERIOD_START_TIME"

                    '    ElseIf ReportAxisData(0)("sandBoxFieldType") = 3 Then
                    '        xaxis = ReportAxisData(0)("ObjectTypeName").ToString

                    '    End If
                    'End If

                    'If chart_elsort(0) <> "0" Then
                    '    dt.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                    '    dt = dt.DefaultView.ToTable
                    'End If


                    'Dim de As New DataEngine(dt)
                    'de.DataFields = String2DataFields(chart_elements, xaxis)
                    'de.DataGridFormatString = "N2"

                    'Dim sc As New SeriesCollection()
                    'sc = de.GetSeries()

                    Dim xaxis As String = ""
                    Dim SplitBy As String = ""
                    If ReportAxisData.Rows.Count = 1 Then
                        If ReportAxisData(0)("sandBoxFieldType") = 4 Then
                            xaxis = "XVal=PERIOD_START_TIME"
                        ElseIf ReportAxisData(0)("sandBoxFieldType") = 3 Then
                            xaxis = "XVal=" & ReportAxisData(0)("DimensionName").ToString
                        End If
                    ElseIf ReportAxisData.Rows.Count = 2 Then
                        For Each dimension In ReportAxisData.Rows
                            If dimension("sandBoxFieldType") = 4 Then
                                xaxis = "Name=PERIOD_START_TIME" & ","
                            ElseIf dimension("sandBoxFieldType") = 3 Then
                                SplitBy = "SplitBy=" & dimension("DimensionName").ToString
                            End If
                        Next
                    End If
                    xaxis = xaxis.TrimEnd(",")

                    If chart_elsort(0) <> "0" Then
                        dt.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                        dt = dt.DefaultView.ToTable
                    End If


                    Dim de As New DataEngine(dt)
                    de.DataFields = String2DataFields(chart_elements, xaxis, SplitBy)
                    de.DataGridFormatString = "N2"
                    '  de.FormatString = sDateFormat

                    Dim sc As New SeriesCollection()
                    sc = de.GetSeries()

                    If SplitBy = "" Then
                        For i = 0 To sc.Count - 1
                            Select Case chart_Eltype(i).ToString().Trim().ToUpper()
                                Case "LINE"
                                    sc(i).Type = SeriesType.Line
                                    sc(i).Line.Width = axis_LineThickness(i)
                                    Exit Select
                                Case "BAR"
                                    sc(i).Type = SeriesType.Bar
                                    Exit Select
                                Case "AREALINE"
                                    sc(i).Type = SeriesType.AreaLine
                                    Exit Select
                            End Select
                            Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                                Case "LEFT"
                                    sc(i).YAxis = yaxis1
                                    Exit Select
                                Case "RIGHT"
                                    sc(i).YAxis = yaxis2
                                    Exit Select
                            End Select

                            color_R = Convert.ToInt32(chart_ElColor(i)) Mod 256
                            color_G = (Convert.ToInt32(chart_ElColor(i)) / 256) Mod 256
                            color_B = ((Convert.ToInt32(chart_ElColor(i)) / 256) \ 256) Mod 256
                            color_R = IIf(color_R > 255, color_R - (color_R - 255), color_R)
                            color_G = IIf(color_G > 255, color_G - (color_G - 255), color_G)
                            color_B = IIf(color_B > 255, color_B - (color_B - 255), color_B)
                            sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)

                            sc(i).DefaultElement.Marker.Type = DirectCast(i, ElementMarkerType)
                        Next

                        If (drCalculatedSeries.Count > 0) Then
                            SetCalculatedSeries(drCalculatedSeries.CopyToDataTable, sc)
                        End If

                        If (drThresholdSeries.Count > 0) Then

                            CreateYAxisMarker(sc, drThresholdSeries)
                        End If
                    Else
                        For i = 0 To sc.Count - 1
                            Select Case chart_Eltype(0).ToString().Trim().ToUpper()
                                Case "LINE"
                                    sc(i).Type = SeriesType.Line
                                    sc(i).Line.Width = axis_LineThickness(0)
                                    Exit Select
                                Case "BAR"
                                    sc(i).Type = SeriesType.Bar
                                    Exit Select
                                Case "AREALINE"
                                    sc(i).Type = SeriesType.AreaLine
                                    Exit Select
                            End Select
                            Select Case chart_elementsYAxis(0).ToString().Trim().ToUpper()
                                Case "LEFT"
                                    sc(i).YAxis = yaxis1
                                    Exit Select
                                Case "RIGHT"
                                    sc(i).YAxis = yaxis2
                                    Exit Select
                            End Select
                        Next
                    End If

                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    sc = Nothing
                    de = Nothing
                    ''ch.XAxis.Markers.Clear()
                    ch.RefreshChart()
                    ch.ResumeLayout()
                    chart_elements = New String(0) {}
                    chart_elementsYAxis = New String(0) {}
                    chart_Eltype = New String(0) {}
                    chart_ElColor = New Integer(0) {}
                    chart_YaxisScale = New String(1) {}
                    j = 0
                End If
            Catch ex As Exception
                Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            End Try
        Next
        'dt_chart.Dispose()
        'ds_chart.Dispose()
        'dt_chart = Nothing
        ''ds_chart = Nothing
        System.GC.Collect()
        ''thread_ReportChartGrid.Abort()
    End Sub

    Public Shared Sub SetCalculatedSeries(ByRef drCalculatedSeries As DataTable, ByRef sc As SeriesCollection)
        For Each dr As DataRow In drCalculatedSeries.Rows
            ''trendSeries = New Series()

            If (dr(CalculatedSeriesTypesFields.Calculated_Series_Type_ID).ToString.Trim.Length > 0) Then
                Dim seName As String = dr(ReportChartFields.SeriesName).ToString.Trim.Substring(0, dr(ReportChartFields.SeriesName).ToString.LastIndexOf("_"))
                Dim ser As Series = sc.GetSeries(seName)
                If (ser IsNot Nothing) Then
                    Dim yaxis As Axis = sc.GetSeries(seName).YAxis

                    Dim rnd As Random = New Random()
                    Dim col As Drawing.Color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))
                    If (dr(CalculatedSeriesTypesFields.Calculated_Series_Type_ID).ToString.Trim = 1) Then
                        Dim trendSeries As New Series()
                        Dim paramvalues As String = dr("CalculatedSeriesParamvalues").ToString
                        trendSeries = ForecastEngine.TrendLinePolynomial(sc.GetSeries(seName), 1, 1, paramvalues.Split(";")(0), 0)
                        ''trendSeries = New SeriesCollection(sc.GetSeries(seName)).Calculate(dr(ReportChartFields.SeriesName).ToString, Calculation.TrendLineLinear)
                        trendSeries.Type = SeriesType.Line
                        trendSeries.YAxis = yaxis
                        trendSeries.DefaultElement.Color = col
                        trendSeries.Line.Width = 5
                        sc.Add(trendSeries)
                    ElseIf (dr(CalculatedSeriesTypesFields.Calculated_Series_Type_ID).ToString.Trim = 2) Then
                        Dim trendSeries As New Series()
                        ''trendSeries = New SeriesCollection(sc.GetSeries(seName)).Calculate(seName, TryCast(ForecastEngine.TrendLineExponential(sc.GetSeries(seName)), Series))
                        trendSeries = ForecastEngine.TrendLineExponential(sc.GetSeries(seName)) ''Calculate("Series 2 Trend ", )
                        ''trendSeries.Elements(0).SmartLabel.Text = "Function: %Function"
                        ''trendSeries.Elements(0).ShowValue = True
                        trendSeries.Type = SeriesType.Line
                        trendSeries.YAxis = yaxis
                        trendSeries.DefaultElement.Color = col
                        trendSeries.Line.Width = 5
                        sc.Add(trendSeries)
                    ElseIf (dr(CalculatedSeriesTypesFields.Calculated_Series_Type_ID).ToString.Trim = 3) Then
                        Dim trendSeries As Series
                        Dim paramvalues As String = dr("CalculatedSeriesParamvalues").ToString
                        trendSeries = ForecastEngine.TrendLinePolynomial(sc.GetSeries(seName), paramvalues.Split(";")(0), 1, paramvalues.Split(";")(1), 0)
                        trendSeries.Type = SeriesType.Line
                        trendSeries.YAxis = yaxis
                        trendSeries.DefaultElement.Color = col
                        trendSeries.Line.Width = 5
                        sc.Add(trendSeries)
                    ElseIf (dr(CalculatedSeriesTypesFields.Calculated_Series_Type_ID).ToString.Trim = 10) Then
                        Dim trendSeries As Series
                        Dim paramvalues As String = dr("CalculatedSeriesParamvalues").ToString
                        trendSeries = StatisticalEngine.SimpleMovingAverage(sc.GetSeries(seName), paramvalues.Split(";")(0)) ''(  New SeriesCollection(sc.GetSeries(seName)).Calculate(seName, Calculation.TrendLineLinear)
                        trendSeries.Type = SeriesType.Line
                        trendSeries.YAxis = yaxis
                        trendSeries.DefaultElement.Color = col
                        trendSeries.Line.Width = 5
                        sc.Add(trendSeries)
                    End If
                End If
            End If
        Next
    End Sub

    Public Shared Sub CreateYAxisMarker(ByRef sc As dotnetCHARTING.WinForms.SeriesCollection, ByVal drThreshold As DataRow())
        If (drThreshold.Count > 0) Then

            sc(0).YAxis.Markers.Clear()
            For Each drow As DataRow In drThreshold

                Dim val As String = drow(ReportChartFields.Calculated_Series_Type_ParamValues).ToString
                Dim number As Decimal = CDbl(IIf(val = "", 0, val))
                Dim thresholdMarker As New AxisMarker(drow(CalculatedSeriesTypesFields.Calculated_Series_Type_Name).ToString, New Line(Color.Black, 4), number)
                thresholdMarker.LegendEntry.Visible = True
                thresholdMarker.Label.Hotspot.ToolTip = drow(CalculatedSeriesTypesFields.Calculated_Series_Type_Name).ToString
                thresholdMarker.Label.Color = Color.Black
                thresholdMarker.Label.Alignment = StringAlignment.Near
                thresholdMarker.Label.LineAlignment = StringAlignment.Far
                thresholdMarker.Label.Text = drow(CalculatedSeriesTypesFields.Calculated_Series_Type_Name).ToString

                thresholdMarker.BringToFront = True
                Dim seresName As String = drow(ReportChartFields.SeriesName).ToString
                Dim startIndex As Integer = seresName.LastIndexOf("_")


                If (sc.Count > 0) Then
                    Dim yaxis As Axis = sc.GetSeries(seresName.Substring(0, startIndex)).YAxis
                    yaxis.Markers.Add(thresholdMarker)
                End If

            Next
        End If
    End Sub
    Public Shared Sub AssignDataToCompareTime(ByRef dt_chart As DataTable, ByRef dt As DataTable, ByRef chart_ReportChartGrid As dotnetCHARTING.WinForms.Chart, ByRef ReportAxisData As DataTable, ByVal sDateFormat As String, ByVal KPIName As String)
        Dim sqlchart As String = ""
        Dim objectscharted As String = ""


        'Assign data to all charts
        '*************************
        Dim ch As Chart = Nothing
        Dim i As Integer
        Dim Y1axislabel As String
        Dim Y2axislabel As String
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim sp As New SmartPalette()
        Dim sc As New SeriesCollection
        'Dim color_R, color_B, color_G As Integer

        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim axis_LineThickness As String() = {"3"}

        For rownum = 0 To 0
            Try
                'collecting elements from chart confguration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                'configures individual chart when new chartline is detected
                If String.IsNullOrEmpty(lastchart) Or lastchart <> drow(ReportChartFields.ChartTitle).ToString() Then
                    lastchart = drow(ReportChartFields.SeriesName).ToString().Trim()
                    Y1axisAbsorPerc = drow(ReportChartFields.AxisAbsPerc).ToString().Trim()
                    Y2axisAbsOrPerc = Utility.nZ(drow(ReportChartFields.AxisAbsPerc), "Abs")
                    Y1axisPrecision = Convert.ToInt32(drow(ReportChartFields.AxisPrecision))
                    Y2axisPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                    Y1axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")
                    Y2axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")

                    chart_elementsYAxis(j) = drow(ReportChartFields.AxisLocation).ToString().Trim()

                    ch = chart_ReportChartGrid
                    SetChartXAxis(Utility.nZ(drow(ReportChartFields.TimeResolution), "Raw"), ch)

                    Dim techPack As String = Utility.nZ(drow(ReportChartFields.TechnologyPackageName), " ")
                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(techPack.ToUpper))
                    ch.Annotations(0).Position = New System.Drawing.Point(ch.Width - 75, 2)
                    ch.Annotations(0).DefaultCorner = BoxCorner.Square
                    ch.Annotations(0).Size = New Size(70, 35)
                    If techPack.Length > 3 Then
                        Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                        ch.Annotations(0).Label.Font = fnt
                    End If
                    ch.Tag = techPack
                    ch.TitleBox.Label.Text = "                               "
                    ch.TitleBox.HeaderLabel.Text = drow(ReportChartFields.ChartTitle).ToString().Trim()

                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                    ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Strings.Chr(13) & "%SeriesName: %Value "

                    'Y-Axis Settings   
                    yaxis1 = New Axis()
                    yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    yaxis1.Label.Text = Y1axislabel
                    yaxis2 = New Axis()
                    yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right


                    axis_LineThickness(0) = drow(ReportChartFields.LineSize)
                    If Strings.UCase(chart_elementsYAxis(j)) = "LEFT" Then
                        chart_YaxisScale(0) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                        If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                            yaxis1.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                        End If

                        If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                            If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                yaxis1.Percent = True
                            End If
                        End If
                        yaxis1.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                        If yaxis1.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then

                            yaxis1.MinimumInterval = 1

                        End If
                        If Utility.nZ(drow(ReportChartFields.IsAutoScale), "0") = "True" Then
                            yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                        End If
                    ElseIf Strings.UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                        chart_YaxisScale(1) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                        If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                            yaxis2.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                        End If

                        If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                            If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                yaxis2.Percent = True
                            End If
                        End If
                        yaxis2.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                        If yaxis2.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then
                            yaxis2.MinimumInterval = 1
                        End If
                        If Utility.nZ(drow(ReportChartFields.IsAutoScale), "0") = "True" Then
                            yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                        End If
                    End If



                    For Each col As DataColumn In dt.Columns
                        ReDim Preserve chart_elements(j)
                        If col.ColumnName.ToUpper <> "ROWSFIELD" Then
                            chart_elements(j) = col.ColumnName.ToUpper
                            j = j + 1
                        End If
                    Next

                    Dim de As DataEngine = New DataEngine(dt)
                    de.DataFields = String2DataFieldsCompareTime(chart_elements, "RowsField")


                    sc = de.GetSeries()

                    Dim LeftAxisDivisor As Int32 = 1
                    Dim RightAxisDivisor As Int32 = 1
                    Dim LeftAxisLabelAddition As String = ""
                    Dim RightAxisLabelAddition As String = ""


                    Dim MaxValueOfSeries As Double = sc(i).Calculate("test", Calculation.Maximum).YValue
                    If MaxValueOfSeries > 1000000000 Then
                        If MaxValueOfSeries > 1000000000000 Then
                            Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                                Case "LEFT"
                                    LeftAxisDivisor = 1000000
                                    LeftAxisLabelAddition = " Million"
                                    Exit Select
                                Case "RIGHT"
                                    RightAxisDivisor = 1000000
                                    RightAxisLabelAddition = " Million"
                                    Exit Select
                            End Select
                        Else
                            Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                                Case "LEFT"
                                    If LeftAxisDivisor < 1000 Then
                                        LeftAxisDivisor = 1000
                                        LeftAxisLabelAddition = " Thousand"
                                    End If
                                    Exit Select
                                Case "RIGHT"
                                    If RightAxisDivisor < 1000 Then
                                        RightAxisDivisor = 1000
                                        RightAxisLabelAddition = " Thousand"
                                    End If
                                    Exit Select
                            End Select
                        End If

                    End If



                    Dim rnd As Random = New Random(11)

                    For i = 0 To sc.Count() - 1
                        sc(i).Type = SeriesType.Line

                        If sc(i).Name = "DAY 0" Or sc(i).Name = "WEEK 0" Then
                            sc(i).Line.Width = 3
                            sc(i).DefaultElement.Color = Color.FromArgb(255, 76, 187, 23)
                            sc(i).DefaultElement.Marker.Type = i
                        Else
                            sc(i).Line.Width = 3
                            sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
                            sc(i).DefaultElement.Marker.Type = i
                        End If

                        If yaxis1 IsNot Nothing Then
                            sc(i).YAxis = yaxis1
                        Else
                            sc(i).YAxis = yaxis2
                        End If

                    Next
                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)
                    ch.Series.Data = dt.Copy()


                    de = Nothing
                    ''ch.XAxis.Markers.Clear()
                    ch.RefreshChart()
                    ch.ResumeLayout()
                    chart_elements = New String(0) {}
                    chart_elementsYAxis = New String(0) {}
                    chart_Eltype = New String(0) {}
                    chart_ElColor = New Integer(0) {}
                    chart_YaxisScale = New String(1) {}
                    j = 0


                End If

            Catch ex As Exception
                Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            End Try
        Next

    End Sub

    Public Shared Sub AssignDataToChart(ByRef dt_chart As DataTable, ByRef dt As DataTable, ByRef chart_ReportChartGrid As dotnetCHARTING.WinForms.Chart, ByRef ReportAxisData As DataTable, ByVal sDateFormat As String, Optional filterString As String = Nothing)
        If (dt_chart Is Nothing) Then
            Exit Sub
        End If
        'Dim connstringconfig As String
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""


        'Assign data to all charts
        '*************************
        Dim ch As Chart = Nothing
        Dim i As Integer = 0
        Dim Y1axislabel As String = Nothing
        Dim Y2axislabel As String = Nothing
        Dim Y1axisAbsorPerc As String = Nothing
        Dim Y2axisAbsOrPerc As String = Nothing
        Dim Y1axisPrecision As Integer = 0
        Dim Y2axisPrecision As Integer = 0
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        'Dim sp As New SmartPalette()
        Dim color_R As Integer = 0
        Dim color_B As Integer = 0
        Dim color_G As Integer = 0

        Dim lastchart As String = ""

        Dim chart_elements As String() = {"0"}
        Dim chart_elementsYAxis As String() = {"0"}
        Dim chart_Eltype As String() = {"Bar"}
        Dim chart_elsort As String() = {"0", "0"}
        Dim chart_ElColor As Integer() = {0}
        Dim chart_YaxisScale As String() = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim axis_LineThickness As String() = {"3"}
        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1

        'Dim drCalculatedSeries As DataRow() = dt_chart.Select(ReportChartFields.Calculated_Series_Type_ID & " is Not null")
        '' Dim drCalculatedSeries As DataRow() = dt_chart.Select(ReportChartFields.Calculated_Series_Type_ID & " is Not null")
        Dim drCalculatedSeries As DataRow() = dt_chart.Select(CalculatedSeriesTypesFields.StatisticsOrThreshold & "=" & "'" & StatisticsOrThreshold.Statistics.ToString & "'")
        Dim drThresholdSeries As DataRow() = dt_chart.Select(CalculatedSeriesTypesFields.StatisticsOrThreshold & "=" & "'" & StatisticsOrThreshold.Threshold.ToString & "'")


        ''The process for Threshold line 
        If (drThresholdSeries.Count > 0) Then

            Dim seriesName As String = drThresholdSeries(0)(ReportChartSeriesFields.SeriesName).ToString
            '  Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - Seriesname " & seriesName)

            If (seriesName.Contains("_Max-Line")) Then
                If (dt.IsValid) Then
                    ''Dim maxValueRow As DataRow() =
                    Dim lstMaxValue As List(Of Integer) = New List(Of Integer)()
                    For Each col As DataColumn In dt.Columns
                        Try
                            If (Not col.ColumnName.ToUpper = "PERIOD_START_TIME") Then
                                lstMaxValue.Add(dt.Compute("max(" & col.ColumnName & ")", String.Empty))
                            End If
                        Catch ex As Exception

                        End Try
                    Next

                    Dim maxValue As Integer = 0
                    If (lstMaxValue.Count > 0) Then
                        maxValue = lstMaxValue.Max()
                        If (maxValue <= Convert.ToInt32(drThresholdSeries(0)(ReportChartSeriesFields.Calculated_Series_Type_ParamValues))) Then
                            Dim newMaxRow As DataRow = dt.NewRow
                            Dim indexColumn As Integer = 0
                            Dim totalRows As Integer = dt.Rows.Count - 1
                            For indexColumn = 0 To dt.Columns.Count - 1
                                If (dt.Columns(indexColumn).ColumnName.ToUpper = seriesName.Replace("_Max-Line", "").ToString.ToUpper) Then
                                    newMaxRow(indexColumn) = Convert.ToInt32(drThresholdSeries(0)(ReportChartSeriesFields.Calculated_Series_Type_ParamValues))
                                ElseIf (dt.Columns(indexColumn).ColumnName.ToUpper = "PERIOD_START_TIME") Then
                                    newMaxRow(indexColumn) = Convert.ToDateTime(dt.Rows(totalRows)(indexColumn).ToString()).AddMinutes(1)
                                Else
                                    newMaxRow(indexColumn) = dt.Rows(totalRows)(indexColumn).ToString()
                                End If
                            Next
                            dt.Rows.Add(newMaxRow)
                        End If
                    End If
                End If
            End If
        End If

        '   Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - Start DtChart " & dt_chart.Rows.Count.ToString)

        For rownum = 0 To dt_chart.Rows.Count - 1
            Dim drow As DataRow = dt_chart.Rows(rownum)
            Try
                While Not ColumnInDataTable(drow(ReportChartFields.SeriesName).ToString().Trim(), dt)
                    rownum = rownum + 1
                    If rownum <= dt_chart.Rows.Count - 1 Then
                        drow = dt_chart.Rows(rownum)
                    Else
                        ' TODO: might not be correct. Was : Exit For
                        Exit While

                    End If
                End While
                ' logger.Error(System.Reflection.MethodBase.GetCurrentMethod().Name + " - " + ex.Message);

            Catch ex As Exception
            End Try

            Try

                'configures individual chart when new chartline is detected
                If String.IsNullOrEmpty(lastchart) Or lastchart <> drow(ReportChartFields.ChartTitle).ToString() Then
                    lastchart = drow(ReportChartFields.SeriesName).ToString().Trim()
                    Y1axisAbsorPerc = drow(ReportChartFields.AxisAbsPerc).ToString().Trim()
                    Y2axisAbsOrPerc = Utility.nZ(drow(ReportChartFields.AxisAbsPerc), "Abs")
                    Y1axisPrecision = Convert.ToInt32(drow(ReportChartFields.AxisPrecision))
                    Y2axisPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                    Y1axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")
                    Y2axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")

                    ch = chart_ReportChartGrid
                    ch.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
                    SetChartXAxis(Utility.nZ(drow(ReportChartFields.TimeResolution), "Raw"), ch)

                    Dim techPack As String = Utility.nZ(drow(ReportChartFields.TechnologyPackageName), " ")
                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(techPack.ToUpper))
                    ch.Annotations(0).Position = New System.Drawing.Point(ch.Width - 75, 2)
                    ch.Annotations(0).DefaultCorner = BoxCorner.Square
                    ch.Annotations(0).Size = New Size(70, 35)
                    If techPack.Length > 3 Then
                        Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                        ch.Annotations(0).Label.Font = fnt
                    End If
                    ch.Tag = techPack
                    ch.TitleBox.Label.Text = "                               "

                    Dim strHeader As String = drow(ReportChartFields.ChartTitle).ToString().Trim() & " (Filter(s): " & filterString & ")"

                    If filterString IsNot Nothing AndAlso filterString <> "" Then
                        ch.TitleBox.HeaderLabel.Text = strHeader
                    Else
                        ch.TitleBox.HeaderLabel.Text = drow(ReportChartFields.ChartTitle).ToString().Trim()
                    End If

                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                    ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Strings.Chr(13) & "%SeriesName: %Value "

                    'Y-Axis Settings   
                    yaxis1 = New Axis()
                    yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    yaxis1.Label.Text = Y1axislabel
                    yaxis2 = New Axis()
                    yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right

                    Do

                        If ColumnInDataTable(drow(ReportChartFields.SeriesName).ToString().Trim(), dt) Then
                            Array.Resize(chart_elements, j + 1)
                            Array.Resize(chart_elementsYAxis, j + 1)
                            Array.Resize(chart_Eltype, j + 1)
                            Array.Resize(chart_ElColor, j + 1)
                            Array.Resize(axis_LineThickness, j + 1)
                            chart_elements(j) = drow(ReportChartFields.SeriesName).ToString().Trim()
                            chart_elementsYAxis(j) = drow(ReportChartFields.AxisLocation).ToString().Trim()
                            chart_Eltype(j) = drow(ReportChartFields.SeriesChartType).ToString().Trim()
                            chart_ElColor(j) = Convert.ToInt32(drow(ReportChartFields.SeriesColor))
                            axis_LineThickness(j) = drow(ReportChartFields.LineSize)
                            If Strings.UCase(chart_elementsYAxis(j)) = "LEFT" Then
                                chart_YaxisScale(0) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                                If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                                    yaxis1.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                                End If

                                If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                                    If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                        yaxis1.Percent = True
                                    End If
                                End If
                                yaxis1.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                                If yaxis1.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then
                                    yaxis1.MinimumInterval = 1
                                End If
                                If Utility.nZ(drow(ReportChartFields.IsAutoScale), "0") = "True" Then
                                    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                                Else
                                    yaxis1.Minimum = 0
                                End If
                            ElseIf Strings.UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                                chart_YaxisScale(1) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                                If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                                    yaxis2.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                                End If

                                If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                                    If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                        yaxis2.Percent = True
                                    End If
                                End If
                                yaxis2.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                                If yaxis2.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then
                                    yaxis2.MinimumInterval = 1
                                End If
                                If Utility.nZ(drow(ReportChartFields.IsAutoScale), "0") = "True" Then
                                    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                                Else
                                    yaxis2.Minimum = 0
                                End If
                            End If
                            If drow(ReportChartFields.SortOrder).ToString.Trim <> "" Then
                                chart_elsort(0) = drow(ReportChartFields.SeriesName).trim
                                chart_elsort(1) = drow(ReportChartFields.SortOrder).ToString.ToUpper()
                            End If

                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > dt_chart.Rows.Count - 1 Then
                            ' TODO: might not be correct. Was : Exit Do
                            Exit Do
                        Else
                            drow = dt_chart.Rows(rownum)
                        End If
                    Loop While drow(ReportChartFields.ChartTitle).ToString().Trim() <> lastchart
                    '5
                    rownum = rownum - 1


                    If Strings.UCase(chart_YaxisScale(0)) = "STACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf Strings.UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If
                    If Strings.UCase(chart_YaxisScale(1)) = "STACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf Strings.UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    'DateTime xaxis_valuehigh = DateTime.Now;  // GetFromTech_DateTimePicker(tech, 2).Value;
                    'if (xaxis_valuehigh.Date == DateAndTime.Now.Date)
                    '{
                    '    switch (true)
                    '    {
                    '        case GetFromTech_RadioButton(tech, "Daily").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date));
                    '            break;

                    '        case GetFromTech_RadioButton(tech, "BH").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date));
                    '            break;
                    '        case GetFromTech_RadioButton(tech, "Weekly").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date);
                    '            break;
                    '        case GetFromTech_RadioButton(tech, "WeeklyBH").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date);
                    '            break;
                    '    }
                    '}

                    'ch.XAxis.ScaleRange.ValueHigh = xaxis_valuehigh;

                    '           Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - Config Done ")

                    Dim xaxis As String = ""
                    Dim SplitBy As String = ""

                    Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - ReportAxisData  " & ReportAxisData.Rows.Count)


                    If ReportAxisData.Rows.Count = 1 Then
                        If ReportAxisData(0)("sandBoxFieldType") = 4 Then
                            xaxis = "XValue=PERIOD_START_TIME"
                        ElseIf ReportAxisData(0)("sandBoxFieldType") = 3 Then
                            xaxis = "XValue=" & ReportAxisData(0)("DimensionName").ToString
                        End If
                    ElseIf ReportAxisData.Rows.Count = 2 Then
                        'objecttime

                        Dim HasPeriodStartTime As Boolean = False
                        Dim dr() As DataRow = ReportAxisData.Select("SandBoxFieldType=4")

                        If dr.Count > 0 Then
                            For Each dimension In ReportAxisData.Rows
                                If dimension("sandBoxFieldType") = 4 Then
                                    xaxis = "xAxis=PERIOD_START_TIME" & ","
                                ElseIf dimension("sandBoxFieldType") = 3 Then
                                    SplitBy = "SplitBy=" & dimension("DimensionName").ToString


                                    If dt.Columns.Contains(dimension("DimensionName")) Then
                                        Dim count As Integer = 0
                                        count =
                                            dt.AsEnumerable().
                                               Select(Function(r) r.Field(Of String)(dimension("DimensionName").ToString)).
                                               Distinct().
                                               Count()
                                        If count < 2 Then
                                            SplitBy = ""
                                        End If
                                    End If

                                End If
                            Next
                        End If


                    End If



                    Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - SplitBy  " & SplitBy)



                        xaxis = xaxis.TrimEnd(",")

                        If chart_elsort(0) <> "0" Then
                            dt.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                            dt = dt.DefaultView.ToTable
                        End If

                        Dim de As New DataEngine(dt)
                        de.DataFields = String2DataFields(chart_elements, xaxis, SplitBy)
                        '   de.DataGridFormatString = "N2"
                        Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - DataFields  Done" & String2DataFields(chart_elements, xaxis, SplitBy))


                        If xaxis.Contains("PERIOD_START_TIME") Then
                            de.FormatString = sDateFormat
                        End If


                        Dim sc As New SeriesCollection()
                        sc = de.GetSeries()

                        Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - DataEngine  Done" & sc.Count.ToString)



                        'find maxValue per axis

                        Dim LeftAxisDivisor As Int32 = 1
                        Dim RightAxisDivisor As Int32 = 1
                        Dim LeftAxisLabelAddition As String = ""
                        Dim RightAxisLabelAddition As String = ""

                        For i = 0 To sc.Count - 1
                            Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - Axis Scale " & sc(i).Name)

                            Dim k As Int16 = 0
                            If SplitBy = "" Then
                                k = i
                            End If

                            Dim MaxValueOfSeries As Double = sc(i).Calculate("test", Calculation.Maximum).YValue
                            If MaxValueOfSeries > 1000000000 Then
                                If MaxValueOfSeries > 1000000000000 Then
                                    Select Case chart_elementsYAxis(k).ToString().Trim().ToUpper()
                                        Case "LEFT"
                                            LeftAxisDivisor = 1000000
                                            LeftAxisLabelAddition = " Million"
                                            Exit Select
                                        Case "RIGHT"
                                            RightAxisDivisor = 1000000
                                            RightAxisLabelAddition = " Million"
                                            Exit Select
                                    End Select
                                Else
                                    Select Case chart_elementsYAxis(k).ToString().Trim().ToUpper()
                                        Case "LEFT"
                                            If LeftAxisDivisor < 1000 Then
                                                LeftAxisDivisor = 1000
                                                LeftAxisLabelAddition = " Thousand"
                                            End If
                                            Exit Select
                                        Case "RIGHT"
                                            If RightAxisDivisor < 1000 Then
                                                RightAxisDivisor = 1000
                                                RightAxisLabelAddition = " Thousand"
                                            End If
                                            Exit Select
                                    End Select
                                End If

                            End If
                        Next

                        If SplitBy = "" Then
                            For i = 0 To sc.Count - 1

                                Select Case chart_Eltype(i).ToString().Trim().ToUpper()
                                    Case "LINE"
                                        sc(i).Type = SeriesType.Line
                                        sc(i).Line.Width = axis_LineThickness(i)
                                        Exit Select
                                    Case "BAR"
                                        sc(i).Type = SeriesType.Bar
                                        Exit Select
                                    Case "AREALINE"
                                        sc(i).Type = SeriesType.AreaLine
                                        Exit Select
                                End Select
                                Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                                    Case "LEFT"

                                        If LeftAxisDivisor > 1 Then
                                            sc(i) = Series.Divide(sc(i), LeftAxisDivisor)
                                        End If
                                        If Not yaxis1.Label.Text.Contains(LeftAxisLabelAddition) Then
                                            yaxis1.Label.Text = yaxis1.Label.Text + LeftAxisLabelAddition
                                        End If

                                        sc(i).YAxis = yaxis1
                                        Exit Select
                                    Case "RIGHT"
                                        If RightAxisDivisor > 1 Then
                                            sc(i) = Series.Divide(sc(i), RightAxisDivisor)
                                        End If

                                        If Not yaxis2.Label.Text.Contains(RightAxisLabelAddition) Then
                                            yaxis2.Label.Text = yaxis2.Label.Text + RightAxisLabelAddition
                                        End If
                                        sc(i).YAxis = yaxis2
                                        Exit Select
                                End Select

                                color_R = Convert.ToInt32(chart_ElColor(i)) Mod 256
                                color_G = (Convert.ToInt32(chart_ElColor(i)) / 256) Mod 256
                                color_B = ((Convert.ToInt32(chart_ElColor(i)) / 256) \ 256) Mod 256
                                color_R = IIf(color_R > 255, color_R - (color_R - 255), color_R)
                                color_G = IIf(color_G > 255, color_G - (color_G - 255), color_G)
                                color_B = IIf(color_B > 255, color_B - (color_B - 255), color_B)
                                sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)

                                sc(i).DefaultElement.Marker.Type = DirectCast(i, ElementMarkerType)

                            Next

                            If (drCalculatedSeries.Count > 0) Then
                                SetCalculatedSeries(drCalculatedSeries.CopyToDataTable, sc)
                            End If

                            If (drThresholdSeries.Count > 0) Then
                                CreateYAxisMarker(sc, drThresholdSeries)
                            End If
                        Else
                            For i = 0 To sc.Count - 1
                                Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - Assigning Axis " & sc(i).Name)

                                Select Case chart_Eltype(0).ToString().Trim().ToUpper()
                                    Case "LINE"
                                        sc(i).Type = SeriesType.Line
                                        sc(i).Line.Width = axis_LineThickness(0)
                                        Exit Select
                                    Case "BAR"
                                        sc(i).Type = SeriesType.Bar
                                        Exit Select
                                    Case "AREALINE"
                                        sc(i).Type = SeriesType.AreaLine
                                        Exit Select
                                End Select
                                Select Case chart_elementsYAxis(0).ToString().Trim().ToUpper()
                                    Case "LEFT"
                                        sc(i).YAxis = yaxis1
                                        Exit Select
                                    Case "RIGHT"
                                        sc(i).YAxis = yaxis2
                                        Exit Select
                                End Select
                            Next
                        End If

                        '             Logger.WriteString_Log("Info - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - Axis Assigned  ")


                        'Label Truncation for TopX
                        If Not xaxis.Contains("PERIOD_START_TIME") Then
                            ch.XAxis.TickLabelAngle = 45
                            ch.XAxis.TickLabelMode = TickLabelMode.Angled
                            ch.XAxis.DefaultTick.Label.AutoWrap = False
                            ch.XAxis.DefaultTick.Label.Truncation.Mode = TruncationMode.Middle
                            ch.XAxis.DefaultTick.Label.Truncation.Length = 25
                            'Else
                            '    ch.XAxis.TickLabelMode = TickLabelMode.Angled
                            '    ch.XAxis.TickLabelAngle = 45
                            '    ch.XAxis.Maximum = 0
                            '    ch.XAxis.Minimum = 0
                        End If

                        ch.SeriesCollection.Clear()
                        ch.SeriesCollection.Add(sc)
                        ch.Series.Data = dt.Copy()

                        sc = Nothing
                        de = Nothing
                        ''ch.XAxis.Markers.Clear()
                        ch.RefreshChart()
                        ch.ResumeLayout()
                        chart_elements = New String(0) {}
                        chart_elementsYAxis = New String(0) {}
                        chart_Eltype = New String(0) {}
                        chart_ElColor = New Integer(0) {}
                        chart_YaxisScale = New String(1) {}
                        j = 0
                    End If
            Catch ex As Exception
                Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " " & ex.StackTrace.ToString)
            End Try
        Next
        'dt_chart.Dispose()
        'ds_chart.Dispose()
        'dt_chart = Nothing
        ''ds_chart = Nothing
        System.GC.Collect()
        ''thread_ReportChartGrid.Abort()
    End Sub

    Public Shared Sub AssignDataToCharts_New(ByRef dt_chart As DataTable, ByVal reportConnString As String, ByVal reportSQL As String, ByRef chart_ReportChartGrid As dotnetCHARTING.WinForms.Chart, ByRef gvChartData As Object)
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""
        Dim ds_chart As DataSet = Utility.QueryData(reportConnString, reportSQL) ''  .ExecuteDataSet(reportConnString, reportSQL)
        If (ds_chart IsNot Nothing) Then
            If (ds_chart.Tables.Count = 0) Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Dim dt As DataTable = New DataTable()
        If (ds_chart.Tables.Count > 0) Then
            dt = ds_chart.Tables(0)
            If dt.Rows.Count > 0 Then
                BindGrid(dt, gvChartData)
            Else
                Return
            End If
        Else
            Exit Sub
        End If

        'Assign data to all charts
        '*************************
        Dim ch As Chart = Nothing
        Dim i As Integer = 0
        Dim Y1axislabel As String = Nothing
        Dim Y2axislabel As String = Nothing
        Dim Y1axisAbsorPerc As String = Nothing
        Dim Y2axisAbsOrPerc As String = Nothing
        Dim Y1axisPrecision As Integer = 0
        Dim Y2axisPrecision As Integer = 0
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        'Dim sp As New SmartPalette()
        Dim color_R As Integer = 0
        Dim color_B As Integer = 0
        Dim color_G As Integer = 0

        Dim lastchart As String = ""

        Dim chart_elements As String() = {"0"}
        Dim chart_elementsYAxis As String() = {"0"}
        Dim chart_Eltype As String() = {"Bar"}
        Dim chart_ElColor As Integer() = {0}
        Dim chart_YaxisScale As String() = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0

        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1

        For rownum = 0 To dt_chart.Rows.Count - 1
            Dim drow As DataRow = dt_chart.Rows(rownum)
            Try
                While Not ColumnInDataTable(drow(ReportChartFields.SeriesName).ToString().Trim(), dt)
                    rownum = rownum + 1
                    If rownum <= dt_chart.Rows.Count - 1 Then
                        drow = dt_chart.Rows(rownum)
                    Else
                        ' TODO: might not be correct. Was : Exit For
                        Exit While

                    End If
                End While
            Catch ex As Exception
            End Try

            Try

                'configures individual chart when new chartline is detected
                If String.IsNullOrEmpty(lastchart) Or lastchart <> drow(ReportChartFields.ChartTitle).ToString() Then
                    lastchart = drow(ReportChartFields.SeriesName).ToString().Trim()

                    Y1axisAbsorPerc = drow(ReportChartFields.AxisAbsPerc).ToString().Trim()
                    Y2axisAbsOrPerc = Utility.nZ(drow(ReportChartFields.AxisAbsPerc), "Abs")
                    Y1axisPrecision = Convert.ToInt32(drow(ReportChartFields.AxisPrecision))
                    Y2axisPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                    Y1axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")
                    Y2axislabel = Utility.nZ(drow(ReportChartFields.AxisLabel), " ")
                    'Dim tabindex As Integer = 0
                    'if (Convert.ToInt32(drow[2].ToString()) == 99)
                    '{
                    '    //tabindex_new = GetTabPageIndex(GetTabControlFromTech(tech), "Custom")
                    '    tabindex_new = GetTabPageIndex(tabcontrol, "Custom");

                    '}

                    ch = chart_ReportChartGrid
                    ' tabcontrol.TabPages(tabindex_new).Controls(0).Controls(chartindex);
                    SetChartXAxis(Utility.nZ(drow(ReportChartFields.TimeResolution), "Raw"), ch)

                    Dim techPack As String = Utility.nZ(drow(ReportChartFields.TechnologyPackageName), " ")
                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(techPack.ToUpper))
                    If techPack.Length > 3 Then
                        Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                        ch.Annotations(0).Label.Font = fnt
                    End If
                    ch.Tag = techPack
                    ch.TitleBox.Label.Text = drow(ReportChartFields.ChartTitle).ToString().Trim()
                    'ch.TitleBox.HeaderLabel.Text = drow[ReportChartFields.ChartTitle].ToString().Trim(); //4


                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                    ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Strings.Chr(13) & "%SeriesName: %Value "

                    'Y-Axis Settings   
                    yaxis1 = New Axis()
                    yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    yaxis1.Label.Text = Y1axislabel
                    yaxis2 = New Axis()
                    yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right

                    Do

                        If ColumnInDataTable(drow(ReportChartFields.SeriesName).ToString().Trim(), dt) Then
                            '5
                            Array.Resize(chart_elements, j + 1)
                            Array.Resize(chart_elementsYAxis, j + 1)
                            Array.Resize(chart_Eltype, j + 1)
                            Array.Resize(chart_ElColor, j + 1)
                            chart_elements(j) = drow(ReportChartFields.SeriesName).ToString().Trim()
                            '5
                            chart_elementsYAxis(j) = drow(ReportChartFields.AxisLocation).ToString().Trim()
                            '7
                            chart_Eltype(j) = drow(ReportChartFields.SeriesChartType).ToString().Trim()
                            '6
                            chart_ElColor(j) = Convert.ToInt32(drow(ReportChartFields.SeriesColor))
                            '12
                            If Strings.UCase(chart_elementsYAxis(j)) = "LEFT" Then
                                chart_YaxisScale(0) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                                '11
                                If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                                    '8
                                    '8
                                    yaxis1.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                                End If

                                If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                                    '9
                                    If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                        '9
                                        yaxis1.Percent = True
                                    End If
                                End If
                                yaxis1.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                                If yaxis1.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then

                                    yaxis1.MinimumInterval = 1

                                End If
                            ElseIf Strings.UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                                chart_YaxisScale(1) = drow(ReportChartFields.AxisScaleProp).ToString().Trim()
                                If Utility.nZ(drow(ReportChartFields.AxisLabel), "").Length > 0 Then
                                    yaxis2.Label.Text = drow(ReportChartFields.AxisLabel).ToString().Trim()
                                End If

                                If Utility.nZ(drow(ReportChartFields.AxisAbsPerc), " ").Length > 1 Then
                                    If drow(ReportChartFields.AxisAbsPerc).ToString().ToUpper() = "PERC" Then
                                        yaxis2.Percent = True
                                    End If
                                End If
                                yaxis2.NumberPrecision = Convert.ToInt32(Utility.nZ(drow(ReportChartFields.AxisPrecision), "0"))
                                If yaxis2.NumberPrecision < 2 And Not (yaxis1.Percent = True) Then

                                    yaxis2.MinimumInterval = 1
                                End If
                            End If


                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > dt_chart.Rows.Count - 1 Then
                            ' TODO: might not be correct. Was : Exit Do
                            Exit Do
                        Else
                            drow = dt_chart.Rows(rownum)
                        End If
                    Loop While drow(ReportChartFields.ChartTitle).ToString().Trim() <> lastchart
                    '5
                    rownum = rownum - 1


                    If Strings.UCase(chart_YaxisScale(0)) = "STACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf Strings.UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If
                    If Strings.UCase(chart_YaxisScale(1)) = "STACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf Strings.UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    'DateTime xaxis_valuehigh = DateTime.Now;  // GetFromTech_DateTimePicker(tech, 2).Value;
                    'if (xaxis_valuehigh.Date == DateAndTime.Now.Date)
                    '{
                    '    switch (true)
                    '    {
                    '        case GetFromTech_RadioButton(tech, "Daily").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date));
                    '            break;

                    '        case GetFromTech_RadioButton(tech, "BH").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date));
                    '            break;
                    '        case GetFromTech_RadioButton(tech, "Weekly").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date);
                    '            break;
                    '        case GetFromTech_RadioButton(tech, "WeeklyBH").Checked:
                    '            xaxis_valuehigh = DateAndTime.DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date);
                    '            break;
                    '    }
                    '}

                    'ch.XAxis.ScaleRange.ValueHigh = xaxis_valuehigh;
                    Dim de As New DataEngine(dt)
                    de.DataFields = String2DataFields(chart_elements, "XVal=PERIOD_START_TIME")
                    de.DataGridFormatString = "N2"

                    Dim sc As New SeriesCollection()
                    sc = de.GetSeries()


                    For i = 0 To sc.Count - 1
                        Select Case chart_Eltype(i).ToString().Trim().ToUpper()
                            Case "LINE"
                                sc(i).Type = SeriesType.Line
                                sc(i).Line.Width = 3
                                Exit Select
                            Case "BAR"
                                sc(i).Type = SeriesType.Bar
                                Exit Select
                            Case "AREALINE"
                                sc(i).Type = SeriesType.AreaLine
                                Exit Select
                        End Select
                        Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                            Case "LEFT"
                                sc(i).YAxis = yaxis1
                                Exit Select
                            Case "RIGHT"
                                sc(i).YAxis = yaxis2
                                Exit Select
                        End Select

                        color_R = Convert.ToInt32(chart_ElColor(i)) Mod 256
                        color_G = (Convert.ToInt32(chart_ElColor(i)) / 256) Mod 256
                        color_B = ((Convert.ToInt32(chart_ElColor(i)) / 256) \ 256) Mod 256

                        sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)

                        sc(i).DefaultElement.Marker.Type = DirectCast(i, ElementMarkerType)
                    Next

                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)



                    sc = Nothing
                    de = Nothing
                    ch.XAxis.Markers.Clear()
                    ch.RefreshChart()
                    ch.ResumeLayout()
                    chart_elements = New String(0) {}
                    chart_elementsYAxis = New String(0) {}
                    chart_Eltype = New String(0) {}
                    chart_ElColor = New Integer(0) {}
                    chart_YaxisScale = New String(1) {}
                    j = 0
                End If
            Catch ex As Exception
                Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            End Try
        Next
        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing
        System.GC.Collect()
        ''thread_ReportChartGrid.Abort()
    End Sub

    Public Shared Function ColumnInDataTable(columname As String, ByRef dt As DataTable) As Boolean
        If (dt.Rows.Count > 0) Then
            For Each col As DataColumn In dt.Columns
                If col.Caption.ToString().Trim().ToUpper() = columname.ToUpper() Then
                    Return True
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function

    Public Shared Sub SetChartXAxis(timeResolution As String, ByRef ch As Chart)
        Try
            ' string TimeResolution = "Raw";
            If timeResolution = "HOUR".ToUpper Or timeResolution = "Raw".ToUpper Then
                ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
                ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.[Default]
                ch.XAxis.FormatString = "HH:mm"
                ch.XAxis.TimeScaleLabels.HourFormatString = "HH:mm"
                ch.XAxis.TimeInterval = TimeInterval.Hours
                ch.XAxis.TimeScaleLabels.DayFormatString = "ddd dd/MM/yy"
                ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Days)
            ElseIf timeResolution = "DAY".ToUpper Then
                ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.[Default]
                ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.[Default]
                ch.XAxis.TimeInterval = TimeInterval.Days
                ch.XAxis.FormatString = "dd/MM/yy"
                ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                ch.XAxis.TimeInterval = TimeInterval.Days
                ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
            ElseIf timeResolution = "BH".ToUpper Then
                ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.[Default]
                ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.[Default]
                ch.XAxis.TimeInterval = TimeInterval.Days
                ch.XAxis.FormatString = "dd/MM/yy"
                ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                ch.XAxis.TimeInterval = TimeInterval.Days
                ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
            ElseIf timeResolution = "WEEK".ToUpper Then
                ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.[Default]
                ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.[Default]
                ch.XAxis.TimeInterval = TimeInterval.Months
                ch.XAxis.FormatString = "MMMM"
                ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM"
                ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Years)
                ch.XAxis.TimeScaleLabels.YearFormatString = "yyyy"
            ElseIf timeResolution = "WEEKBH".ToUpper Then
                ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.[Default]
                ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.[Default]
                ch.XAxis.TimeInterval = TimeInterval.Months
                ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM"
                ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Years)
                ch.XAxis.TimeScaleLabels.YearFormatString = "yyyy"
            End If
        Catch
        End Try
    End Sub

    Public Shared Function String2DataFields(ByRef str As String(), xval As String, Optional SplitBy As String = "") As String
        Dim stroutput As String = Nothing
        Dim i As Integer = 0

        ' stroutput = "XValue=" & xval
        stroutput = xval
        ' a(0)
        For i = 0 To str.Count() - 1
            stroutput = stroutput & "," & " yAxis=" & str(i)
        Next
        stroutput = stroutput & "," & SplitBy
        stroutput = stroutput.TrimEnd(",")
        Return stroutput
    End Function
    Private Shared Function String2DataFieldsCompareTime(ByRef str() As String, ByRef xval As String) As String
        Dim stroutput As String
        Dim i As Integer
        stroutput = "XValue=" & xval ' a(0)
        For i = UBound(str) To 0 Step -1
            stroutput = stroutput & "," & " Yvalue=" & str(i).Replace(",", "\,")
        Next
        Return stroutput
    End Function

    Public Shared Function ExecuteDataSet(connstring As String, sql As String, Optional isStoredProcedure As Boolean = False, Optional queryTimeOut As Integer = 0) As DataSet
        If String.IsNullOrEmpty(sql) Or String.IsNullOrEmpty(connstring) Then
            Return Nothing
        End If

        Dim cnOSS As System.Data.Odbc.OdbcConnection = Nothing
        Dim daOSS As System.Data.Odbc.OdbcDataAdapter = Nothing
        Dim dsOSS As System.Data.DataSet = Nothing

        Try
            cnOSS = New System.Data.Odbc.OdbcConnection(connstring)

            daOSS = New System.Data.Odbc.OdbcDataAdapter(sql, cnOSS)
            dsOSS = New System.Data.DataSet()
            daOSS.SelectCommand.CommandTimeout = queryTimeOut
            If isStoredProcedure = True Then
                daOSS.SelectCommand.CommandType = CommandType.StoredProcedure
            End If
            daOSS.Fill(dsOSS)


            Return dsOSS
        Catch ex As Exception
            ' KeepConnectionOpen = false;
            ' Interaction.MsgBox("Problem getting data from server using: " + connstring.Split(";uid")[0] + Strings.Chr(13) + ex.Message.ToString());
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Return Nothing
        Finally
            If (daOSS IsNot Nothing) Then
                daOSS.Dispose()
            End If

            If (cnOSS IsNot Nothing) Then
                cnOSS.Close()
            End If
        End Try
    End Function

    Public Shared Sub BindGrid(ByRef dtReportChart As DataTable, ByRef vDGV_ReportChartGrid As Object)

        If dtReportChart.Rows.Count > 0 Then
            'VIBlend.WinForms.DataGridView.vDataGridView gvTemp = vDGV_ReportChartGrid;
            'vDGV_ReportChartGrid.SuspendLayout();
            vDGV_ReportChartGrid.Clear()
            'vDGV_ReportChartGrid.Refresh();
            'vDGV_ReportChartGrid.ResumeLayout();
            'RefrashingGrid(vDGV_ReportChartGrid, true);
            vDGV_ReportChartGrid.DataSource = dtReportChart
        End If
    End Sub

End Class

Public Class Logger
    Public Shared Function GetUserDataPath() As String
        Dim basePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim dataPath As String = String.Format("{0}\{1}\{2}\{3}", basePath, Application.CompanyName, Application.ProductName, IOS.Configuration.IOSAppConfigManage.DeploymentName)
        If Not IO.Directory.Exists(dataPath) Then
            IO.Directory.CreateDirectory(dataPath)
        End If
        Return dataPath
    End Function

    Public Shared Sub WriteString_Log(ByVal text2append As String)
        Try
            Dim FILE_NAME As String = GetUserDataPath() & "\session.log"
            Static LogFileLock As New Object()
            SyncLock LogFileLock
                IO.File.AppendAllText(FILE_NAME, text2append & vbCrLf)
            End SyncLock
        Catch ex As Exception
        End Try
    End Sub

End Class