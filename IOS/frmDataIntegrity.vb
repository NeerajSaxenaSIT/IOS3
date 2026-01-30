Imports dotnetCHARTING.WinForms

Public Class frmDataIntegrity
    Dim dtIntegrityBreachedList As New DataTable

    Private Structure GridCellPLMNChart
        Shared dtChartData As DataTable = Nothing
        Shared Tech As String = Nothing
        Shared DataSourceName As String = Nothing
        Shared ChartType As String = Nothing
    End Structure

    Private Structure GridCellCNTRChart
        Shared dtChartData As DataTable = Nothing
        Shared Tech As String = Nothing
        Shared DataSourceName As String = Nothing
        Shared ChartType As String = Nothing
    End Structure

    Private Structure GridCellOSSChart
        Shared dtChartData As DataTable = Nothing
        Shared Tech As String = Nothing
        Shared DataSourceName As String = Nothing
        Shared ChartType As String = Nothing
    End Structure

#Region "Data Integrity Load"

    Private Sub frmDataIntegrity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadNotifications(3600, gcNotifications, gvNotifications)
            LoadDataIntegrity(3601, gcDataIntegrityPLMN, gvDataIntegrityPLMN)
            LoadDataIntegrity(3602, gcDataIntegrityCNTR, gvDataIntegrityCNTR)
            LoadDataIntegrity(3603, gcDataIntegrityOSS, gvDataIntegrityOSS)
            LoadDataIntegrity(3604, gcDataIntegrityOther, gvDataIntegrityOther)

            Dim strSql As String
            strSql = "SELECT [IOS_TECH],[Element_Type],[Element_Name],[Element_Type_Counted],[Data_Source_Name],[Threshhold_Breach] FROM [dbo].[IOS_Data_Integrity_Evaluation] Where [Date_Counted] = cast(floor(cast(GETDATE() - 0 as float)) as datetime);"
            dtIntegrityBreachedList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, strSql)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Private Methods"

    Private Sub LoadNotifications(sqlid As Integer, gctrl As DevExpress.XtraGrid.GridControl, gview As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            Dim strConnection As String
            Dim strQuery As String

            strConnection = GetSQL(sqlid, Nothing)(0)
            strQuery = GetSQL(sqlid, Nothing)(1)

            Dim dtNotifications As New System.Data.DataTable()
            dtNotifications = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, strQuery, 10)

            gview.Columns.Clear()
            gctrl.DataSource = dtNotifications
            gview.Columns(0).DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
            gview.Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom

            gview.Columns(0).Width = gview.Columns(0).GetBestWidth()
            gview.Columns(1).Width = gview.Columns(1).GetBestWidth()

            Dim mEdit As New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
            gctrl.RepositoryItems.Add(mEdit)
            gview.Columns(2).ColumnEdit = mEdit
            gview.OptionsView.RowAutoHeight = True
            gview.OptionsBehavior.Editable = False
            gview.OptionsView.ShowIndicator = False

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataIntegrity(sqlid As Integer, gcDataIntegrity As DevExpress.XtraGrid.GridControl, gvDataIntegrity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim strConnection As String
            Dim strQuery As String

            strConnection = GetSQL(sqlid, Nothing)(0)
            strQuery = GetSQL(sqlid, Nothing)(1)

            Dim dtDataIntegrity As New System.Data.DataTable()
            dtDataIntegrity = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, strQuery)
            gcDataIntegrity.DataSource = Nothing
            gvDataIntegrity.Columns.Clear()
            gvDataIntegrity.OptionsBehavior.AutoPopulateColumns = True
            gcDataIntegrity.DataSource = dtDataIntegrity

            gvDataIntegrity.Bands.Clear()
            Dim gcBandObjCount As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandObjCount.Caption = "Object Count"
            gcBandObjCount.AppearanceHeader.Options.UseTextOptions = True
            gcBandObjCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandObjCount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandSourceName As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandSourceName.Caption = "Data Source Name"
            gcBandSourceName.AppearanceHeader.Options.UseTextOptions = True
            gcBandSourceName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandSourceName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In gvDataIntegrity.Columns
                If col.AbsoluteIndex > 1 Then
                    gcBandSourceName.Columns.Add(col)
                Else
                    gcBandObjCount.Columns.Add(col)
                End If
                col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            Next
            If gvDataIntegrity.Columns.Count > 0 Then
                gvDataIntegrity.Columns(0).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            End If
            gvDataIntegrity.OptionsView.ColumnAutoWidth = True
            gvDataIntegrity.Bands.Add(gcBandObjCount)
            gvDataIntegrity.Bands.Add(gcBandSourceName)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub LoadChart(ByRef dt As DataTable, ByVal tech As String, ByVal dataSourceName As String, ByRef chart As Chart)
        Dim i As Integer
        Dim yaxis1 As Axis = Nothing
        Dim color_R, color_B, color_G As Integer

        Dim chart_elements() As String = Nothing
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0

        chPLMN.ChartAreaLayout.Mode = ChartAreaLayoutMode.VerticalPriority
        chPLMN.DefaultSeries.Type = SeriesType.Column
        chPLMN.DefaultElement.Marker.Visible = False
        chPLMN.ChartAreaSpacing = 5
        chPLMN.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
        chPLMN.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default

        chart.LegendBox.Orientation = Orientation.Bottom
        chart.LegendBox.DefaultEntry.Value = ""
        chart.XAxis.TickLabelMode = TickLabelMode.Angled
        chart.XAxis.TickLabelAngle = 45
        chart.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        chart.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

        chart.XAxis.TimeScaleLabels.RangeIntervals.Clear()
        chart.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
        chart.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
        chart.XAxis.TimeInterval = TimeInterval.Days
        chart.XAxis.FormatString = "dd/MM/yy"
        chart.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
        chart.XAxis.TimeInterval = TimeInterval.Days
        chart.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
        chart.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"

        chart.Annotations.Clear()
        chart.Annotations.Add(New Annotation(tech))
        chart.TitleBox.Label.Text = "Objects: " & dataSourceName
        chart.TitleBox.HeaderLabel.Text = "Data Integrity - " & dataSourceName

        chart.TitleBox.Label.Alignment = StringAlignment.Near
        chart.TitleBox.Label.LineAlignment = StringAlignment.Near
        chart.DefaultElement.Hotspot.ToolTip = "Date Counted: %XValue" & Chr(13) & "%SeriesName: %Value "
        chart.XAxis.FormatString = "dd/MM/yyyy"

        chart.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Range
        ReDim Preserve chart_elements(dt.Columns.Count - 2)
        ReDim Preserve chart_elementsYAxis(dt.Columns.Count - 2)
        ReDim Preserve chart_Eltype(dt.Columns.Count - 2)
        ReDim Preserve chart_ElColor(dt.Columns.Count - 2)

        Dim objRandom As New Random()
        For Each column As DataColumn In dt.Columns
            If column.Ordinal > 0 Then
                chart_elements(column.Ordinal - 1) = column.ColumnName
                chart_Eltype(column.Ordinal - 1) = "Line"
                chart_ElColor(column.Ordinal - 1) = objRandom.Next
                chart_elementsYAxis(column.Ordinal - 1) = "LEFT"
            End If
        Next

        Dim de As DataEngine = New DataEngine(dt)
        de.DataFields = String2DataFields(chart_elements, "Date_Counted")
        de.DataGridFormatString = "N2"

        Dim sc As New SeriesCollection
        sc = de.GetSeries()

        For i = 0 To sc.Count() - 1
            Select Case UCase(chart_Eltype(i).Trim)
                Case "LINE"
                    sc(i).Type = SeriesType.Line
                    sc(i).Line.Width = 3
                    If sc(i).Name = "Threshold" Then
                        sc(i).Line.DashStyle = Drawing2D.DashStyle.Dot
                    End If

                Case "BAR"
                    sc(i).Type = SeriesType.Bar
                Case "AREALINE"
                    sc(i).Type = SeriesType.AreaLine
            End Select
            Select Case UCase(chart_elementsYAxis(i).Trim)
                Case "LEFT"
                    sc(i).YAxis = yaxis1
                Case "RIGHT"
            End Select

            color_R = CLng(chart_ElColor(i)) Mod 256
            color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
            color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

            sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
            sc(i).DefaultElement.Marker.Type = 0
        Next

        chart.SeriesCollection.Clear()
        chart.SeriesCollection.Add(sc)
        chart.RefreshChart()
        chart.ResumeLayout()
    End Sub

#End Region

#Region "Grid Cell Events"

    Private Sub gvDataIntegrityPLMN_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gvDataIntegrityPLMN.RowCellStyle, gvDataIntegrityCNTR.RowCellStyle, gvDataIntegrityOSS.RowCellStyle, gvDataIntegrityOther.RowCellStyle
        Try
            If e.Column.AbsoluteIndex > 1 Then
                If dtIntegrityBreachedList.Rows.Count > 0 Then
                    Dim view As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView = CType(sender, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                    Dim dr() As DataRow
                    Dim ElementType = "PLMN"
                    Dim filter As String = ""
                    If view.Name = "gvDataIntegrityPLMN" Then
                        filter = "[IOS_TECH]='" & view.GetRowCellValue(e.RowHandle, view.Columns(0)) & "' And  [Element_Type]='" & ElementType & "' And [" & view.Columns(1).FieldName & "]='" & view.GetRowCellValue(e.RowHandle, view.Columns(1)) & "' And [Data_Source_Name]='" & e.Column.FieldName & "'"
                    Else
                        filter = "[IOS_TECH]='" & view.GetRowCellValue(e.RowHandle, view.Columns(0)) & "' And  [Element_Type]<>'" & ElementType & "' And [" & view.Columns(1).FieldName & "]='" & view.GetRowCellValue(e.RowHandle, view.Columns(1)) & "' And [Data_Source_Name]='" & e.Column.FieldName & "'"
                    End If

                    dr = dtIntegrityBreachedList.Select(filter)
                    If dr.Length > 0 Then
                        If dr(0).Item("Threshhold_Breach") = 1 Then
                            e.Appearance.BackColor = Color.FromArgb(198, 56, 40)
                            e.Appearance.ForeColor = Color.White
                        Else
                            e.Appearance.BackColor = Color.FromArgb(109, 183, 28)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gvDataIntegrityPLMN_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvDataIntegrityPLMN.RowCellClick
        Try
            chCNTR.Cursor = Cursors.WaitCursor
            If e.RowHandle > -1 Then
                'If dtIntegrityBreachedList.Rows.Count > 0 Then
                Dim view As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView = CType(sender, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                Dim ElementType = "PLMN"
                Dim Tech As String = view.GetRowCellValue(e.RowHandle, view.Columns(0))
                Dim ElementTypeCounted As String = view.GetRowCellValue(e.RowHandle, view.Columns(1))
                Dim DataSourceName As String = e.Column.FieldName.ToString
                Dim dtChartData As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [dbo].[IOS_DataIntegrity_PLMN_HistoryChart] '" & Tech & "','" & DataSourceName & "','" & ElementTypeCounted & "'")

                GridCellPLMNChart.ChartType = "PLMN"
                GridCellPLMNChart.dtChartData = dtChartData
                GridCellPLMNChart.Tech = Tech
                GridCellPLMNChart.DataSourceName = DataSourceName

                LoadChart(dtChartData, Tech, DataSourceName, chPLMN)
                'End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            chCNTR.Cursor = Cursors.Default
        Finally
            chCNTR.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub gvDataIntegrityCNTR_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvDataIntegrityCNTR.RowCellClick
        Try
            chCNTR.Cursor = Cursors.WaitCursor
            If e.RowHandle > -1 Then
                'If dtIntegrityBreachedList.Rows.Count > 0 Then
                Dim view As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView = CType(sender, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                Dim ElementType = "CNTR"
                Dim Tech As String = view.GetRowCellValue(e.RowHandle, view.Columns(0))
                Dim ElementName As String = view.GetRowCellValue(e.RowHandle, view.Columns(1))
                Dim DataSourceName As String = e.Column.FieldName.ToString
                Dim dtChartData As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [dbo].[IOS_DataIntegrity_CNTR_HistoryChart] '" & Tech & "','" & DataSourceName & "','" & ElementName & "'")

                GridCellCNTRChart.ChartType = "CNTR"
                GridCellCNTRChart.dtChartData = dtChartData
                GridCellCNTRChart.Tech = Tech
                GridCellCNTRChart.DataSourceName = DataSourceName

                LoadChart(dtChartData, Tech, DataSourceName, chCNTR)
                'End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            chCNTR.Cursor = Cursors.Default
        Finally
            chCNTR.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub gvDataIntegrityOSS_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvDataIntegrityOSS.RowCellClick
        Try
            chCNTR.Cursor = Cursors.WaitCursor
            If e.RowHandle > -1 Then
                'If dtIntegrityBreachedList.Rows.Count > 0 Then
                Dim view As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView = CType(sender, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                Dim ElementType = "OSS"
                Dim Tech As String = view.GetRowCellValue(e.RowHandle, view.Columns(0))
                Dim ElementTypeCounted As String = view.GetRowCellValue(e.RowHandle, view.Columns(1))
                Dim DataSourceName As String = e.Column.FieldName.ToString
                Dim dtChartData As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [dbo].[IOS_DataIntegrity_OSS_HistoryChart] '" & Tech & "','" & DataSourceName & "','" & ElementTypeCounted & "'")

                GridCellOSSChart.ChartType = "OSS"
                GridCellOSSChart.dtChartData = dtChartData
                GridCellOSSChart.Tech = Tech
                GridCellOSSChart.DataSourceName = DataSourceName

                LoadChart(dtChartData, Tech, DataSourceName, chOSS)
                'End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            chCNTR.Cursor = Cursors.Default
        Finally
            chCNTR.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub gvDataIntegrityOther_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvDataIntegrityOther.RowCellClick
        Try

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

#Region "Chart Click - Zoom In"

    Dim x1 As Integer = -1, y1 As Integer = -1, x2 As Integer = -1, y2 As Integer = -1
    Dim clickCountPLMN As Integer = 0

    Private Sub chPLMN_MouseClick(sender As Object, e As MouseEventArgs) Handles chPLMN.MouseClick
        Try
            clickCountPLMN = clickCountPLMN + 1
            If clickCountPLMN < 3 Then
                ' This sample demonstrates an interactive way to zoom a section of an axis.
                chPLMN.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
                chPLMN.DefaultSeries.Type = SeriesType.Line
                chPLMN.DefaultElement.Marker.Visible = False

                chPLMN.ChartAreaSpacing = 15
                ' Setup x axis ticks.
                chPLMN.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Dynamic
                chPLMN.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Hidden
                chPLMN.XAxis.TimeScaleLabels.DayTick.Label.Text = ""
                chPLMN.XAxis.DefaultTick.Label.Font = New Font("Arial", 8, FontStyle.Bold)
                If clickCountPLMN = 1 Then
                    Dim ht As HitTestInfo = chPLMN.HitTest(e.Location.X, e.Location.Y)
                    If ht.Object IsNot Nothing Then
                        If TypeOf ht.Object Is Element Then
                            x1 = e.Location.X
                            y1 = e.Location.Y
                            Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                            Dim am As New AxisMarker("", Color.Red, val)
                            chPLMN.XAxis.Markers.Add(am)
                        Else
                            clickCountPLMN = 0
                            Exit Sub
                        End If
                    Else
                        clickCountPLMN = 0
                        Exit Sub
                    End If
                End If

                If clickCountPLMN = 2 Then
                    Dim ht As HitTestInfo = chPLMN.HitTest(e.Location.X, e.Location.Y)
                    If ht.Object IsNot Nothing Then
                        If TypeOf ht.Object Is Element Then
                            x2 = e.Location.X
                            y2 = e.Location.Y
                            Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                            Dim am As New AxisMarker("", Color.Red, val)
                            chPLMN.XAxis.Markers.Add(am)
                        Else
                            clickCountPLMN = 1
                            Exit Sub
                        End If
                    Else
                        clickCountPLMN = 1
                        Exit Sub
                    End If
                End If

                If clickCountPLMN = 2 Then
                    ' Get the axis values at xy positions.
                    Dim ht As HitTestInfo = chPLMN.HitTest(x1, y1)
                    Dim ht1 As HitTestInfo = chPLMN.HitTest(x2, y2)

                    If Not (ht.Object Is Nothing) And Not (ht1.Object Is Nothing) Then
                        'If both click positions were valid:
                        'add a zoom area.
                        Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                        Dim val2 As DateTime = CType(ht1.Object, dotnetCHARTING.WinForms.Element).XDateTime
                        chPLMN.XAxis.Markers.Clear()
                        Dim ca As ChartArea = chPLMN.ChartArea.GetXZoomChartArea(chPLMN.XAxis, New ScaleRange(val, val2), New Line(Color.Green, System.Drawing.Drawing2D.DashStyle.Dash))
                        chPLMN.ExtraChartAreas.Add(ca)
                        chPLMN.RefreshChart()
                    End If
                End If
            Else
                chPLMN.ExtraChartAreas.Clear()
                chPLMN.XAxis.Markers.Clear()
                If GridCellPLMNChart.dtChartData IsNot Nothing Then
                    LoadChart(GridCellPLMNChart.dtChartData, GridCellPLMNChart.Tech, GridCellPLMNChart.DataSourceName, chPLMN)
                End If
                chPLMN.RefreshChart()
                clickCountPLMN = 0
            End If

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Dim clickCountCNTR As Integer = 0
    Private Sub chCNTR_MouseClick(sender As Object, e As MouseEventArgs) Handles chCNTR.MouseClick
        Try
            clickCountCNTR = clickCountCNTR + 1
            If clickCountCNTR < 3 Then
                ' This sample demonstrates an interactive way to zoom a section of an axis.
                chCNTR.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
                chCNTR.DefaultSeries.Type = SeriesType.Line
                chCNTR.DefaultElement.Marker.Visible = False

                chCNTR.ChartAreaSpacing = 15
                ' Setup x axis ticks.
                chCNTR.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Dynamic
                chCNTR.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Hidden
                chCNTR.XAxis.TimeScaleLabels.DayTick.Label.Text = ""
                chCNTR.XAxis.DefaultTick.Label.Font = New Font("Arial", 8, FontStyle.Bold)
                If clickCountCNTR = 1 Then
                    Dim ht As HitTestInfo = chCNTR.HitTest(e.Location.X, e.Location.Y)
                    If ht.Object IsNot Nothing Then
                        If TypeOf ht.Object Is Element Then
                            x1 = e.Location.X
                            y1 = e.Location.Y
                            Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                            Dim am As New AxisMarker("", Color.Red, val)
                            chCNTR.XAxis.Markers.Add(am)
                        Else
                            clickCountCNTR = 0
                            Exit Sub
                        End If
                    Else
                        clickCountCNTR = 0
                        Exit Sub
                    End If
                End If

                If clickCountCNTR = 2 Then
                    Dim ht As HitTestInfo = chCNTR.HitTest(e.Location.X, e.Location.Y)
                    If ht.Object IsNot Nothing Then
                        If TypeOf ht.Object Is Element Then
                            x2 = e.Location.X
                            y2 = e.Location.Y
                            Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                            Dim am As New AxisMarker("", Color.Red, val)
                            chCNTR.XAxis.Markers.Add(am)
                        Else
                            clickCountCNTR = 1
                            Exit Sub
                        End If
                    Else
                        clickCountCNTR = 1
                        Exit Sub
                    End If
                End If

                If clickCountCNTR = 2 Then
                    ' Get the axis values at xy positions.
                    Dim ht As HitTestInfo = chCNTR.HitTest(x1, y1)
                    Dim ht1 As HitTestInfo = chCNTR.HitTest(x2, y2)

                    If Not (ht.Object Is Nothing) And Not (ht1.Object Is Nothing) Then
                        'If both click positions were valid:
                        'add a zoom area.
                        Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                        Dim val2 As DateTime = CType(ht1.Object, dotnetCHARTING.WinForms.Element).XDateTime
                        chCNTR.XAxis.Markers.Clear()
                        Dim ca As ChartArea = chCNTR.ChartArea.GetXZoomChartArea(chCNTR.XAxis, New ScaleRange(val, val2), New Line(Color.Green, System.Drawing.Drawing2D.DashStyle.Dash))
                        chCNTR.ExtraChartAreas.Add(ca)
                        chCNTR.RefreshChart()
                    End If
                End If
            Else
                chCNTR.ExtraChartAreas.Clear()
                chCNTR.XAxis.Markers.Clear()
                If GridCellCNTRChart.dtChartData IsNot Nothing Then
                    LoadChart(GridCellCNTRChart.dtChartData, GridCellCNTRChart.Tech, GridCellCNTRChart.DataSourceName, chCNTR)
                End If
                chCNTR.RefreshChart()
                clickCountCNTR = 0
            End If
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Dim clickCountOSS As Integer = 0
    Private Sub chOSS_MouseClick(sender As Object, e As MouseEventArgs) Handles chOSS.MouseClick
        Try
            clickCountOSS = clickCountOSS + 1
            If clickCountOSS < 3 Then
                ' This sample demonstrates an interactive way to zoom a section of an axis.
                chOSS.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
                chOSS.DefaultSeries.Type = SeriesType.Line
                chOSS.DefaultElement.Marker.Visible = False

                chOSS.ChartAreaSpacing = 15
                ' Setup x axis ticks.
                chOSS.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Dynamic
                chOSS.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Hidden
                chOSS.XAxis.TimeScaleLabels.DayTick.Label.Text = ""
                chOSS.XAxis.DefaultTick.Label.Font = New Font("Arial", 8, FontStyle.Bold)
                If clickCountOSS = 1 Then
                    Dim ht As HitTestInfo = chOSS.HitTest(e.Location.X, e.Location.Y)
                    If ht.Object IsNot Nothing Then
                        If TypeOf ht.Object Is Element Then
                            x1 = e.Location.X
                            y1 = e.Location.Y
                            Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                            Dim am As New AxisMarker("", Color.Red, val)
                            chOSS.XAxis.Markers.Add(am)
                        Else
                            clickCountOSS = 0
                            Exit Sub
                        End If
                    Else
                        clickCountOSS = 0
                        Exit Sub
                    End If
                End If

                If clickCountOSS = 2 Then
                    Dim ht As HitTestInfo = chOSS.HitTest(e.Location.X, e.Location.Y)
                    If ht.Object IsNot Nothing Then
                        If TypeOf ht.Object Is Element Then
                            x2 = e.Location.X
                            y2 = e.Location.Y
                            Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                            Dim am As New AxisMarker("", Color.Red, val)
                            chOSS.XAxis.Markers.Add(am)
                        Else
                            clickCountOSS = 1
                            Exit Sub
                        End If
                    Else
                        clickCountOSS = 1
                        Exit Sub
                    End If
                End If

                If clickCountOSS = 2 Then
                    ' Get the axis values at xy positions.
                    Dim ht As HitTestInfo = chOSS.HitTest(x1, y1)
                    Dim ht1 As HitTestInfo = chOSS.HitTest(x2, y2)

                    If Not (ht.Object Is Nothing) And Not (ht1.Object Is Nothing) Then
                        'If both click positions were valid:
                        'add a zoom area.
                        Dim val As DateTime = CType(ht.Object, dotnetCHARTING.WinForms.Element).XDateTime
                        Dim val2 As DateTime = CType(ht1.Object, dotnetCHARTING.WinForms.Element).XDateTime
                        chOSS.XAxis.Markers.Clear()
                        Dim ca As ChartArea = chOSS.ChartArea.GetXZoomChartArea(chOSS.XAxis, New ScaleRange(val, val2), New Line(Color.Green, System.Drawing.Drawing2D.DashStyle.Dash))
                        chOSS.ExtraChartAreas.Add(ca)
                        chOSS.RefreshChart()
                    End If
                End If
            Else
                chOSS.ExtraChartAreas.Clear()
                chOSS.XAxis.Markers.Clear()
                If GridCellOSSChart.dtChartData IsNot Nothing Then
                    LoadChart(GridCellOSSChart.dtChartData, GridCellOSSChart.Tech, GridCellOSSChart.DataSourceName, chOSS)
                End If
                chOSS.RefreshChart()
                clickCountOSS = 0
            End If
        Catch ex As Exception

        Finally

        End Try
    End Sub

#End Region

End Class