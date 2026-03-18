Imports System.Drawing
Imports System.Windows.Forms
Imports dotnetCHARTING.WinForms
Imports IOS.DataLibrary

Partial Public Class ReportChartGrid
    Inherits System.Windows.Forms.UserControl

    Private CallProcessChartGenerateInvoked As System.Windows.Forms.MethodInvoker
    Public Sub New()
        InitializeComponent()
        CallProcessChartGenerateInvoked = New System.Windows.Forms.MethodInvoker(AddressOf ProcessChartGenerate_Invoked)
        chart_ReportChartGrid.Application = "gQzI2MXojPIgHq0nSVxaGkDnjJ5mpGQhDVaFskyiEpJuan0E08iqQMF1Ct16hWyK"
		AddHandler chart_ReportChartGrid.DragDrop, AddressOf Chart_DragDrop
    End Sub

    Public Delegate Sub ChartDragDropHandler(sender As Object, e As DragEventArgs)

    ' declare the public event that other classes can subscribe to
    Public Event ChartDragDropEvent As ChartDragDropHandler

    ' wire up the internal button click event to trigger our custom event
    Public Sub Chart_DragDrop(sender As Object, e As DragEventArgs)
        RaiseEvent ChartDragDropEvent(sender, e)
    End Sub

    Private _threadReportChartGrid As ThreadReportChartGrid
    Private threadChartGrid As System.Threading.Thread
    Private dsCurrentChartData As System.Data.DataSet
    Private seriesDateFormat As String = ""
    Public Shared reportAbort As Boolean = False

    Public Property ReportId() As String
        Get
            Return m_ReportId
        End Get
        Set(value As String)
            m_ReportId = value
        End Set
    End Property
    Private m_ReportId As String

    Public Property ReportOrdinale() As String
        Get
            Return m_ReportOrdinale
        End Get
        Set(value As String)
            m_ReportOrdinale = value
        End Set
    End Property
    Private m_ReportOrdinale As String

    Public Property DashboardID() As String
        Get
            Return m_DashboardID
        End Get
        Set(value As String)
            m_DashboardID = value
        End Set
    End Property
    Private m_DashboardID As String

    Public Overloads Property Height() As Integer
        Get
            Return m_Height
        End Get
        Set(value As Integer)
            m_Height = value
        End Set
    End Property
    Private m_Height As Integer

    Public Overloads Property Width() As Integer
        Get
            Return m_Width
        End Get
        Set(value As Integer)
            m_Width = value
        End Set
    End Property
    Private m_Width As Integer

    Public Property reportSQL() As String
        Get
            Return m_reportSQL
        End Get
        Set(value As String)
            m_reportSQL = value
        End Set
    End Property
    Private m_reportSQL As String

    Public Property reportConnString() As String
        Get
            Return m_reportConnString
        End Get
        Set(value As String)
            m_reportConnString = value
        End Set
    End Property
    Private m_reportConnString As String

    Public dtChartConfig As New DataTable()
    Public Property Dt_ChartConfig() As DataTable
        Get
            Return dtChartConfig
        End Get
        Set(value As DataTable)
            dtChartConfig = value
        End Set
    End Property

    Public Property ChartObjectsData() As String
        Get
            Return m_ChartObjectsData
        End Get
        Set(value As String)
            m_ChartObjectsData = value
        End Set
    End Property
    Private m_ChartObjectsData As String

    Public _reportAxisData As New DataTable()
    Public Property ReportAxisData() As DataTable
        Get
            Return _reportAxisData
        End Get
        Set(value As DataTable)
            _reportAxisData = value
        End Set
    End Property

    Public _reportFilter As New DataTable()
    Public Property ReportFilter() As DataTable
        Get
            Return _reportFilter
        End Get
        Set(value As DataTable)
            _reportFilter = value
        End Set
    End Property

    Public _gridorchart As String
    Public Property GridorChart() As String
        Get
            Return _gridorchart
        End Get
        Set(value As String)
            _gridorchart = value
        End Set
    End Property

    Private _refreshSetting As Boolean
    Public Property RefreshSetting() As Boolean
        Get
            Return _refreshSetting
        End Get
        Set(value As Boolean)
            _refreshSetting = value
        End Set
    End Property

    Public _AlignInterval As String
    Public Property AlignInterval() As String
        Get
            Return _AlignInterval
        End Get
        Set(value As String)
            _AlignInterval = value
        End Set
    End Property

    Private _dtpredefinedReportID As New DataTable
    Public Property DTPredefinedReportID() As DataTable
        Get
            Return _dtpredefinedReportID
        End Get
        Set(value As DataTable)
            _dtpredefinedReportID = value
        End Set
    End Property

    Public Sub GenerateSQL(ByVal sDateFormat As String, ByVal queryTimeOut As Integer)
        Try
            If reportAbort = True Then
                Exit Sub
            End If
            Process_ReportChartGridAppend(sDateFormat, queryTimeOut)
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
        End Try
    End Sub

    Public Sub ClearData()
        chart_ReportChartGrid.ClearAll()
        DataMartGridView.ClearGrid(gcReportChartGrid, gvReportChartGrid)
        reportSQL = String.Empty
        reportConnString = String.Empty
        dtChartConfig = Nothing
    End Sub

    Public WriteOnly Property SetGridContextMenu() As System.Windows.Forms.ContextMenuStrip
        Set(value As System.Windows.Forms.ContextMenuStrip)
            gcReportChartGrid.ContextMenuStrip = value
        End Set
    End Property

    Public WriteOnly Property SetChartContextMenu() As System.Windows.Forms.ContextMenuStrip
        Set(value As System.Windows.Forms.ContextMenuStrip)
            chart_ReportChartGrid.ContextMenuStrip = value
        End Set
    End Property

    Public Sub RefreshChartConfig()
        Dim chrtSeri As New SeriesCollection()
        chrtSeri = chart_ReportChartGrid.SeriesCollection
        If chrtSeri IsNot Nothing Then
            Dim dt As DataTable = DirectCast(gcReportChartGrid.DataSource, DataTable)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    chart_ReportChartGrid.SuspendLayout()
                    chart_ReportChartGrid.SeriesCollection.Clear()
                    ReportChartGridManager.RefreshChartConfig(dtChartConfig, dt, chart_ReportChartGrid, _reportAxisData)
                    chart_ReportChartGrid.Update()
                    chart_ReportChartGrid.ResumeLayout()
                End If
            End If
        End If
    End Sub

#Region "Thread Logic"

    Private Function IsString(numString As String) As Boolean
        Dim numLong As Long = 0
        Dim canConvert As Boolean = Long.TryParse(numString, numLong)
        If canConvert = True Then
            Return False
        Else
            Dim numDecimal As Decimal = 0
            canConvert = Decimal.TryParse(numString, numDecimal)
            If canConvert = True Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Private Function AbortReport(abort As Boolean) As Boolean
        If abort = True Then
            If threadChartGrid IsNot Nothing Then
                threadChartGrid.Abort()
                _threadReportChartGrid = Nothing
            End If
        End If
        Return abort
    End Function

    Private Sub Process_ReportChartGridAppend(ByVal seDateFormat As String, ByVal queryTimeOut As Integer)
        _threadReportChartGrid = New ThreadReportChartGrid()
        threadChartGrid = New System.Threading.Thread(AddressOf _threadReportChartGrid.GetData)
        _threadReportChartGrid.sDateFormat = seDateFormat
        _threadReportChartGrid.connStringThread = reportConnString
        _threadReportChartGrid.sqlQueryTimeOut = queryTimeOut
        If RefreshSetting = True Then
            reportSQL = GetRefreshChartSQL(reportSQL)
        End If
        _threadReportChartGrid.sqlCommandThead = reportSQL
        AddHandler _threadReportChartGrid.ThreadComplete, AddressOf ThreadReportChartGrid_ThreadEnd
        threadChartGrid.Start()
        threadChartGrid.Join()

        If AbortReport(reportAbort) = True Then
            Exit Sub
        End If
    End Sub

    Private Function GetRefreshChartSQL(rptSql As String) As String
        Dim modifiedRptSql As String = ""
        If DTPredefinedReportID IsNot Nothing Then
            rptSql = dtChartConfig.Rows(0)("ReportSQL").ToString
            Dim dt As DataTable = CType(chart_ReportChartGrid.Series.Data, DataTable)
            If rptSql.Contains("PERIOD_START_TIME") AndAlso dt.Columns.Contains("PERIOD_START_TIME") Then
                Dim LastPeriodStartTime As String = dt.Compute("MAX(PERIOD_START_TIME)", "")
                'Dim sqlParts() As String = rptSql.Split(vbLf)
                'Dim modifiedWherePart As String = ""
                'For Each part As String In sqlParts
                '    If part.ToUpper.Contains("BETWEEN") AndAlso part.ToUpper.Contains("AND") Then
                '        Dim whereSqlPart() As String = part.Split(" ")
                '        modifiedWherePart = whereSqlPart(0) & " >= '" & LastPeriodStartTime & "'"
                '        modifiedRptSql = modifiedRptSql & modifiedWherePart & vbLf
                '    Else
                '        modifiedRptSql = modifiedRptSql & part & vbLf
                '    End If
                'Next
                If Not rptSql.Contains(">=") Then
                    modifiedRptSql = rptSql.Replace(" BETWEEN " & DTPredefinedReportID.Rows(0)("SQLStart") & " AND " & DTPredefinedReportID.Rows(0)("SQLEnd"), " >= '" & LastPeriodStartTime & "'")
                Else
                    modifiedRptSql = rptSql.Replace(dt.Rows(dt.Rows.Count - 2)("PERIOD_START_TIME"), LastPeriodStartTime)
                End If
            Else
                modifiedRptSql = rptSql
            End If
            'modifiedRptSql = modifiedRptSql.TrimEnd(vbLf)
        Else
            modifiedRptSql = "Select * From ("
            Dim dt As DataTable = CType(chart_ReportChartGrid.Series.Data, DataTable)
            If rptSql.Contains("PERIOD_START_TIME") Then
                Dim LastPeriodStartTime As String = dt.Compute("MAX(PERIOD_START_TIME)", "")
                If rptSql.Contains("ORDER BY") Then
                    rptSql = rptSql.Substring(0, rptSql.LastIndexOf("ORDER BY"))
                    rptSql = rptSql.Replace("ORDER BY 1", "")
                End If

                If rptSql.Contains("Select * From (") Then
                    modifiedRptSql = rptSql.Split(">=")(0) & " >= " & "'" & LastPeriodStartTime & "'"
                Else
                    modifiedRptSql = modifiedRptSql & rptSql & ") x WHERE PERIOD_START_TIME >= '" & LastPeriodStartTime & "'"
                End If
            End If
        End If
        Return modifiedRptSql
    End Function

    Private Sub ThreadReportChartGrid_ThreadEnd(ds As System.Data.DataSet, sDateFormat As String, ti As System.Threading.Thread)
        Try
            If AbortReport(reportAbort) = True Then
                Exit Sub
            End If

            Dim dt As New DataTable()
            If (ds IsNot Nothing) Then
                If (ds.Tables.Count = 0) Then
                    Return
                Else
                    dt = ds.Tables(0)
                End If
            Else
                Return
            End If

            If dt IsNot Nothing Then
                dsCurrentChartData = New System.Data.DataSet()
                dsCurrentChartData.Tables.Add(dt.Copy())
                If (ds IsNot Nothing) Then
                    ds.Dispose()
                    ds = Nothing
                    dt.Dispose()
                    dt = Nothing
                End If
            Else
                Return
            End If
            seriesDateFormat = sDateFormat
            Me.BeginInvoke(Me.CallProcessChartGenerateInvoked)
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
        End Try
    End Sub

    Private Sub ProcessChartGenerate_Invoked()
        'Cursor.Current = Cursors.WaitCursor
        If AbortReport(reportAbort) = True Then
            Exit Sub
        End If

        Dim filterString As String = String.Empty
        Dim showFilterString As String = String.Empty

        Try
            If (dsCurrentChartData IsNot Nothing) Then
                If dsCurrentChartData.Tables.Count > 0 Then
                    Dim dtData As DataTable = Nothing
                    If (chart_ReportChartGrid IsNot Nothing) Then
                        ReportChartGridManager.SetChartProperty(True, "testChart", chart_ReportChartGrid)
                        Dim reportId As String = "0"
                        If dtChartConfig IsNot Nothing Then
                            If dtChartConfig.Rows.Count > 0 Then
                                reportId = dtChartConfig.Rows(0)("ReportId").ToString()
                                If _reportFilter.Rows.Count > 0 Then
                                    If _reportFilter.Rows(0)("QueryOrResult").ToString.ToUpper = "RESULT" Then
                                        filterString = GetChartFilterString()
                                        If dsCurrentChartData.Tables(0).Rows.Count > 0 Then
                                            Dim drFilter As DataRow() = dsCurrentChartData.Tables(0).[Select](filterString)
                                            If drFilter.Count() > 0 Then
                                                dtData = drFilter.CopyToDataTable()
                                            Else
                                                dtData = dsCurrentChartData.Tables(0).Clone()
                                            End If
                                        Else
                                            dtData = dsCurrentChartData.Tables(0)
                                        End If
                                    Else
                                        dtData = dsCurrentChartData.Tables(0)
                                    End If
                                Else
                                    dtData = dsCurrentChartData.Tables(0)
                                End If
                            Else
                                dtData = dsCurrentChartData.Tables(0)
                            End If

                            If RefreshSetting = True Then
                                dtData = chart_ReportChartGrid.Series.Data
                                If dtData.Columns.Contains("PERIOD_START_TIME") Then
                                    dtData.Merge(dsCurrentChartData.Tables(0))
                                End If
                            End If

                            'If filterString <> String.Empty Then
                            showFilterString = GetChartShowFilterString()
                            'End If

                            If GridorChart.ToUpper = "CHART" Then
                                Dim CompareTime As Int16 = dtChartConfig.Rows(0)("CompareTime").ToString()

                                If dtData.Rows.Count <= 2000 And CompareTime = "0" Then
                                    ReportChartGridManager.AssignDataToChart(dtChartConfig, dtData, chart_ReportChartGrid, _reportAxisData, seriesDateFormat, showFilterString)

                                    If (ChartObjectsData = "") Then
                                        chart_ReportChartGrid.Title = "Objects : PLMN"
                                    Else
                                        If Len(ChartObjectsData) > 150 Then
                                            chart_ReportChartGrid.Title = "Objects : " + ChartObjectsData.Substring(0, 150) + IIf(Len(ChartObjectsData) > 150, "...", "")
                                        Else
                                            chart_ReportChartGrid.Title = "Objects : " + ChartObjectsData
                                        End If
                                    End If

                                    Dim sc_count As Int16 = chart_ReportChartGrid.SeriesCollection.Count

                                    If sc_count > 0 Then
                                        splitC_ReportChartGrid.Panel2Collapsed = False
                                        splitC_ReportChartGrid.Panel1Collapsed = True
                                        splitC_ReportChartGrid.Panel1Collapsed = False
                                        splitC_ReportChartGrid.Panel2Collapsed = True

                                    Else
                                        splitC_ReportChartGrid.Panel2Collapsed = False
                                        splitC_ReportChartGrid.Panel1Collapsed = True
                                        splitC_ReportChartGrid.Panel1.Hide()
                                    End If

                                ElseIf dtData.Rows.Count <= 2000 And CompareTime >= "1" Then

                                    Dim dt_new As New DataTable
                                    Dim dt_new_sort As New DataTable
                                    Dim pvt_Pivot As Pivot = Nothing
                                    Dim dt_pivot As DataTable = Nothing
                                    Dim dtCopy As DataTable = Nothing

                                    Dim KPI As String = dtChartConfig(0)("SeriesName")

                                    Dim displayView = New DataView(dtData)
                                    displayView.RowFilter = ""
                                    dt_new = displayView.ToTable(False, "PERIOD_START_TIME", KPI).Copy

                                    'Pivot data to align interval
                                    'add time columns for pivot
                                    dt_new.Columns.Add(New DataColumn("RowsSortField", GetType(Integer)))
                                    dt_new.Columns.Add(New DataColumn("RowsField", GetType(String)))
                                    dt_new.Columns.Add(New DataColumn("ColumnsField", GetType(String)))

                                    If dt_new.Rows.Count > 1 Then
                                        Dim d1 As DateTime = dt_new(0)("PERIOD_START_TIME")
                                        Dim d2 As DateTime = dt_new(1)("PERIOD_START_TIME")
                                        Dim dlast As DateTime = dt_new(dt_new.Rows.Count - 1)("PERIOD_START_TIME")
                                        Dim dd As Long = DateDiff(DateInterval.Minute, d1, d2)

                                        If dd < 60 Then
                                            For Each dr As DataRow In dt_new.Rows
                                                Dim hourpart As String = "0" + DatePart(DateInterval.Hour, dr("PERIOD_START_TIME")).ToString
                                                Dim minpart As String = "0" + DatePart(DateInterval.Minute, dr("PERIOD_START_TIME")).ToString
                                                dr("RowsSortField") = hourpart.Substring(hourpart.Length - 2, 2) + minpart.Substring(minpart.Length - 2, 2)
                                                dr("RowsField") = hourpart.Substring(hourpart.Length - 2, 2) + ":" + minpart.Substring(minpart.Length - 2, 2)
                                                dr("ColumnsField") = "Day " & (DateDiff(DateInterval.Day, DateSerial(dlast.Year, dlast.Month, dlast.Day), DateSerial(dr("PERIOD_START_TIME").Year, dr("PERIOD_START_TIME").Month, dr("PERIOD_START_TIME").Day), FirstDayOfWeek.System, FirstWeekOfYear.System))
                                            Next
                                        ElseIf dd = 60 Then
                                            For Each dr As DataRow In dt_new.Rows
                                                dr("RowsSortField") = DatePart(DateInterval.Hour, dr("PERIOD_START_TIME"))
                                                dr("RowsField") = DatePart(DateInterval.Hour, dr("PERIOD_START_TIME"))
                                                dr("ColumnsField") = "Day " & (DateDiff(DateInterval.Day, DateSerial(dlast.Year, dlast.Month, dlast.Day), DateSerial(dr("PERIOD_START_TIME").Year, dr("PERIOD_START_TIME").Month, dr("PERIOD_START_TIME").Day), FirstDayOfWeek.System, FirstWeekOfYear.System))
                                            Next
                                        ElseIf dd = 60 * 24 Then
                                            For Each dr As DataRow In dt_new.Rows
                                                dr("RowsSortField") = DatePart(DateInterval.Weekday, dr("PERIOD_START_TIME"), FirstDayOfWeek.System)
                                                dr("RowsField") = WeekdayName(DatePart(DateInterval.Weekday, dr("PERIOD_START_TIME"), FirstDayOfWeek.System), False, FirstDayOfWeek.System)
                                                dr("ColumnsField") = "Week " & (DateDiff(DateInterval.WeekOfYear, DateSerial(dlast.Year, dlast.Month, dlast.Day), DateSerial(dr("PERIOD_START_TIME").Year, dr("PERIOD_START_TIME").Month, dr("PERIOD_START_TIME").Day), FirstDayOfWeek.System, FirstWeekOfYear.System))
                                            Next
                                        End If
                                        'remove date column
                                        dt_new.Columns.Remove("PERIOD_START_TIME")
                                        Dim displayViewSort = New DataView(dt_new)
                                        displayViewSort.Sort = "RowsSortField ASC"
                                        dt_new_sort = displayViewSort.ToTable.Copy
                                        'pivot
                                        'pvt_Pivot = New Pivot(dt_new)
                                        pvt_Pivot = New Pivot(dt_new_sort)
                                        dt_pivot = pvt_Pivot.PivotData("RowsField", KPI, AggregateFunction.Average, "ColumnsField")


                                        ReportChartGridManager.AssignDataToCompareTime(dtChartConfig, dt_pivot, chart_ReportChartGrid, _reportAxisData, seriesDateFormat, KPI)
                                        chart_ReportChartGrid.Series.Data = dtData.Copy
                                    End If

                                    If (ChartObjectsData = "") Then
                                        chart_ReportChartGrid.Title = "Objects : PLMN"
                                    Else
                                        If Len(ChartObjectsData) > 150 Then
                                            chart_ReportChartGrid.Title = "Objects : " + ChartObjectsData.Substring(0, 150) + IIf(Len(ChartObjectsData) > 150, "...", "")
                                        Else
                                            chart_ReportChartGrid.Title = "Objects : " + ChartObjectsData
                                        End If
                                    End If

                                    splitC_ReportChartGrid.Panel2Collapsed = False
                                    splitC_ReportChartGrid.Panel1Collapsed = True
                                    splitC_ReportChartGrid.Panel1Collapsed = False
                                    splitC_ReportChartGrid.Panel2Collapsed = True

                                Else
                                    splitC_ReportChartGrid.Panel2Collapsed = False
                                    splitC_ReportChartGrid.Panel1Collapsed = True
                                    splitC_ReportChartGrid.Panel1.Hide()
                                    MsgBox("Too many objects to create chart. Result is shown in grid.", vbExclamation)
                                End If
                            End If
                        End If
                    End If
                    If (gcReportChartGrid IsNot Nothing) Then
                        If (dtData.Rows.Count > 0) Then
                            DataMartGridView.ClearGrid(gcReportChartGrid, gvReportChartGrid)
                            GridViewMap_AddData(gcReportChartGrid, gvReportChartGrid, dtData, "ALL", seriesDateFormat)
                            chart_ReportChartGrid.RefreshChart()
                        Else
                            ''gcReportChartGrid.ClearAll()
                            gcReportChartGrid.SuspendLayout()
                            Dim item As DevExpress.XtraGrid.Columns.GridColumn = Nothing
                            For Each dc As DataColumn In dtData.Columns
                                item = New DevExpress.XtraGrid.Columns.GridColumn()
                                item.Caption = dc.ColumnName
                                ''Item.Resizable = True
                                gvReportChartGrid.Columns.Add(item)
                                item.Resize(item.Width)
                            Next
                            gcReportChartGrid.Refresh()
                            gcReportChartGrid.Update()
                            gcReportChartGrid.ResumeLayout()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If (dsCurrentChartData IsNot Nothing) Then
                dsCurrentChartData.Dispose()
                dsCurrentChartData = Nothing
                threadChartGrid.Abort()
            End If
            Application.DoEvents()
        End Try
        'Cursor.Current = Cursors.Default
    End Sub

    Private Function GetChartFilterString() As String
        Dim filterString As String = String.Empty
        Dim valParam As String = String.Empty
        For i As Integer = 0 To _reportFilter.Rows.Count - 1
            valParam = (If(IsString(_reportFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()),
                                "'" & _reportFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString() & "'",
                                _reportFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()))
            If i = 0 Then
                filterString = _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString() & Chr(32) & _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32) & valParam
            ElseIf i = 1 Then
                filterString = Convert.ToString((filterString & Convert.ToString(" ")) & _reportFilter.Rows(i - 1)(ReportContentFilterFields.LogicalLink).ToString() & " " &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString() & Chr(32) &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32)) & valParam
            ElseIf i > 1 Then
                filterString = Convert.ToString((Convert.ToString("(") & filterString) + ") " & _reportFilter.Rows(i - 1)(ReportContentFilterFields.LogicalLink).ToString() & Chr(32) &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString() & Chr(32) & _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32)) &
                                            valParam
            End If
        Next
        Return filterString
    End Function

    Private Function GetChartShowFilterString() As String
        Dim filterString As String = String.Empty
        Dim valParam As String = String.Empty
        For i As Integer = 0 To _reportFilter.Rows.Count - 1
            valParam = (If(IsString(_reportFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()),
                                "'" & _reportFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString() & "'",
                                _reportFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()))
            If i = 0 Then
                If _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString().Contains(".") Then
                    filterString = _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString().Split(".")(1) & Chr(32) & _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32) & valParam
                Else
                    filterString = _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString() & Chr(32) & _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32) & valParam
                End If
            ElseIf i = 1 Then
                If _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString().Contains(".") Then
                    filterString = Convert.ToString((filterString & Convert.ToString(" ")) & _reportFilter.Rows(i - 1)(ReportContentFilterFields.LogicalLink).ToString() & " " &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString().Split(".")(1) & Chr(32) &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32)) & valParam
                Else
                    filterString = Convert.ToString((filterString & Convert.ToString(" ")) & _reportFilter.Rows(i - 1)(ReportContentFilterFields.LogicalLink).ToString() & " " &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString() & Chr(32) &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32)) & valParam
                End If

            ElseIf i > 1 Then
                If _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString().Contains(".") Then
                    filterString = Convert.ToString((Convert.ToString("(") & filterString) + ") " & _reportFilter.Rows(i - 1)(ReportContentFilterFields.LogicalLink).ToString() & Chr(32) &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString().Split(".")(1) & Chr(32) & _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32)) &
                                            valParam
                Else
                    filterString = Convert.ToString((Convert.ToString("(") & filterString) + ") " & _reportFilter.Rows(i - 1)(ReportContentFilterFields.LogicalLink).ToString() & Chr(32) &
                                            _reportFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString() & Chr(32) & _reportFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32)) &
                                            valParam
                End If

            End If
        Next
        Return filterString
    End Function

    Private Sub vDGV_ReportChartGrid_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            If e.Control And e.KeyCode = Keys.C Then
                ''Dim tempGrid As VIBlend.WinForms.DataGridView.vDataGridView = TryCast(sender, vDataGridView)
                ''If (tempGrid IsNot Nothing) Then
                DataMartGridView.CopyGridDataToClipBoard(TryCast(sender, DevExpress.XtraGrid.GridControl), TryCast((TryCast(sender, DevExpress.XtraGrid.GridControl).DefaultView), DevExpress.XtraGrid.Views.Grid.GridView))
                ''End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridViewMap_AddData(ByRef gridCtrl As DevExpress.XtraGrid.GridControl, ByRef grdView As DevExpress.XtraGrid.Views.Grid.GridView, dt As DataTable, fit As String, Optional dateFormat As String = Nothing)
        'Try
        '    gridCtrl.DataSource = dt
        '    grdView.OptionsBehavior.AutoPopulateColumns = True
        '    Dim _grid1 = gridCtrl
        '    _grid1.ResumeLayout(False)
        '    _grid1.DataSource = Nothing
        '    _grid1.Refresh()
        '    _grid1.DataSource = dt
        '    _grid1.Update()
        '    _grid1.ResumeLayout(True)
        '    _grid1.Refresh()
        '    '_grid1.AllowContextMenuFiltering = True
        '    '_grid1.ColumnsHierarchy.Filters.Clear()

        '    For k As Integer = 0 To grdView.Columns.Count - 1
        '        grdView.Columns(k).OptionsFilter.AllowFilter = True
        '        If fit = "ALL" Then
        '            grdView.Columns(k).Resize(grdView.Columns(k).GetBestWidth())
        '        Else
        '            grdView.Columns(k).BestFit()
        '        End If
        '    Next
        '    '_grid1.RowsHierarchy.CompactStyleRenderingEnabled = True
        'Catch ex As Exception
        'End Try

        Try
            With gridCtrl
                .SuspendLayout()
                grdView.OptionsView.ColumnAutoWidth = False
                grdView.OptionsBehavior.AutoPopulateColumns = True
                gridCtrl.DataSource = Nothing
                grdView.Columns.Clear()
                gridCtrl.DataSource = dt
                grdView.BestFitMaxRowCount = 10

                For Each dtCol As DataColumn In dt.Columns
                    If grdView.Columns(dtCol.ColumnName) IsNot Nothing Then
                        If grdView.Columns(dtCol.ColumnName).Visible = True Then

                            If dtCol.DataType = GetType(DateTime) Then
                                grdView.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                If dateFormat IsNot Nothing Then
                                    grdView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = dateFormat
                                Else
                                    grdView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
                                End If
                            End If

                            grdView.Columns(dtCol.ColumnName).BestFit()
                            grdView.Columns(dtCol.ColumnName).FieldName = dtCol.ColumnName
                            grdView.Columns(dtCol.ColumnName).Caption = dtCol.ColumnName
                        End If
                    End If
                Next

                .ResumeLayout()
            End With
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

#End Region

    Private Sub chart_ReportChartGrid_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub chart_ReportChartGrid_MouseDown(sender As Object, e As MouseEventArgs)
        Try
            Dim myChart As Chart = DirectCast(sender, Chart)
            myChart.Cursor = Cursors.Hand
            Dim p As System.Drawing.Point = myChart.PointToClient(Cursor.Position)
            Dim strPoint As String = p.X.ToString() + "," + p.Y.ToString()
            Dim mousePoint As System.Drawing.Point = New Point(MousePosition.X, MousePosition.Y)
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
        End Try
    End Sub

    Private Sub chart_ReportChartGrid_MouseMove(sender As Object, e As MouseEventArgs)
        Dim myChart As Chart = DirectCast(sender, Chart)
        ReorderingPosition(myChart)
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            myChart.Cursor = Cursors.Hand
            DoDragDrop(Me, DragDropEffects.All)
            myChart.Invalidate()
        End If
    End Sub

    Private Sub chart_ReportChartGrid_MouseUp(sender As Object, e As MouseEventArgs)
        Dim myChart As Chart = DirectCast(sender, Chart)
        myChart.Cursor = Cursors.[Default]
    End Sub

    Private Sub ReorderingPosition(chartObject As Chart)
        Dim tempSplitCont As SplitContainer = GetSplitControl(chartObject)
        Dim flpDashboard As FlowLayoutPanel
        Try
            If (tempSplitCont IsNot Nothing) Then
                If Object.ReferenceEquals(tempSplitCont.Parent.[GetType](), GetType(FlowLayoutPanel)) Then
                    flpDashboard = DirectCast(tempSplitCont.Parent, FlowLayoutPanel)
                Else
                    flpDashboard = DirectCast(tempSplitCont.Parent.Parent, FlowLayoutPanel)
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetSplitControl(tempControl As Control) As SplitContainer
        Try
            If (tempControl.Parent IsNot Nothing) Then
                If Object.ReferenceEquals(tempControl.Parent.[GetType](), GetType(SplitContainer)) Then
                    Return DirectCast(tempControl.Parent, SplitContainer)
                Else
                    Return GetSplitControl(DirectCast(tempControl.Parent, Control))
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Sub ShowGridChartPanel()
        If GridorChart.ToUpper = "GRID" Then
            splitC_ReportChartGrid.Panel2Collapsed = False
            splitC_ReportChartGrid.Panel1Collapsed = True
            splitC_ReportChartGrid.Panel1.Hide()
        ElseIf GridorChart.ToUpper = "CHART" Then
            splitC_ReportChartGrid.Panel1Collapsed = False
            splitC_ReportChartGrid.Panel2Collapsed = True
            splitC_ReportChartGrid.Panel2.Hide()
        End If
    End Sub

End Class

Public Class ThreadReportChartGrid
    Public ds As System.Data.DataSet
    Public connStringThread As String
    Public sqlCommandThead As String
    Public ds_name As String = Nothing
    Public sDateFormat As String = Nothing
    Public sqlQueryTimeOut As Integer = Nothing
    Public Event ThreadComplete As ThreadCompleteEventHandler
    Public Delegate Sub ThreadCompleteEventHandler(ds As System.Data.DataSet, sDateFormat As String, ti As System.Threading.Thread)

    Public Sub GetData()
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcDataAdapter = Nothing
        Dim dtQODBC As New DataTable()
        Dim dsTemp As New DataSet()
        Try
            cnQODBC = New System.Data.Odbc.OdbcConnection(connStringThread)
            cnQODBC.ConnectionTimeout = 10
            cnQODBC.Open()
            daQODBC = New System.Data.Odbc.OdbcDataAdapter(sqlCommandThead, cnQODBC)
            daQODBC.SelectCommand.CommandTimeout = sqlQueryTimeOut

            ds = New System.Data.DataSet()
            daQODBC.Fill(dsTemp)
            If (dsTemp IsNot Nothing) Then
                ds = MergQueryData(dsTemp)
            Else
                Return
            End If
            If (ds_name IsNot Nothing) Then
                If Not String.IsNullOrEmpty(ds_name) Then
                    ds.DataSetName = ds_name
                End If

            End If
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If (cnQODBC IsNot Nothing) Then
                cnQODBC.Close()
            End If
            If (daQODBC IsNot Nothing) Then
                daQODBC.Dispose()
            End If
            If (cnQODBC IsNot Nothing) Then
                cnQODBC.Dispose()
            End If
            RaiseEvent ThreadComplete(ds, sDateFormat, System.Threading.Thread.CurrentThread)
        End Try
    End Sub

    Private Function MergQueryData(ds As DataSet) As DataSet
        Dim ds_result As New DataSet()
        Try
            For Each dt As DataTable In ds.Tables
                Dim pkcols As DataColumn() = Nothing
                Dim pkcolsindex As Integer = 0
                For Each dc As DataColumn In dt.Columns
                    If dc.DataType <> GetType(Single) And dc.DataType <> GetType(Double) Then
                        Array.Resize(pkcols, pkcolsindex + 1)
                        pkcols(pkcolsindex) = dc
                        pkcolsindex = pkcolsindex + 1
                    End If
                Next
                dt.PrimaryKey = pkcols
                If ds_result.Tables.Count = 0 Then
                    ds_result.Tables.Add(dt.Copy())
                Else
                    ds_result.Tables(0).Merge(dt.Copy())
                End If
            Next
        Catch ex As Exception
            Logger.WriteString_Log("Error - " & System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        Return ds_result
    End Function

End Class