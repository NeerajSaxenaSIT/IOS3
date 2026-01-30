Imports IOS.DataLibrary
Imports IOS.Library
Imports dotnetCHARTING.WinForms
Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTab
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars
Imports DevExpress.DataAccess.Native.Sql
Imports DevExpress.DataAccess.Sql
Imports System.ComponentModel
Imports System.IO
Imports System.Security.AccessControl
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.Data
Imports DevExpress.DataAccess

Public Class frmDashboard

#Region "Variables"

    Dim cbic As ComboBoxItemCollection
    Dim filterZoneValue As String = "None"
    Dim tlvTicketChange_Old As String = ""
    Dim ticketChange_Old As Int32 = -1
    Dim dsDashboardKPI As DataSet
    Dim dsTickets As DataSet
    Dim dsEvents As DataSet
    Public networksAll As NetworksAll = Nothing
    Private objfrmTechnology As frmTechnology = Nothing
    Dim timeMonitor As DateTime
    Private WithEvents dataRefreshTimer As Threading.Timer

    'Dashboard Report Viewer Related
    Private CellName As String = Nothing
    Private IOS_Tech As String = Nothing
    Private ObjectType As String = Nothing
    Private dbGrids As New Dictionary(Of String, GridControl)
    Private dbFilters As New Dictionary(Of String, String)
    Private resultDS As DataSet = New DataSet()
    Private dtDBReports As DataTable = Nothing

#End Region

#Region "Private Methods"

    Private Sub frm_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RemoveHandler gvIOS_Tickets.FocusedRowChanged, AddressOf gvIOS_Tickets_FocusedRowChanged
            'RemoveHandler dcmb_Filter_Area.SelectedIndexChanged, AddressOf dcmb_Filter_Area_SelectedIndexChanged
            RemoveHandler gvIOS_Tickets.ColumnFilterChanged, AddressOf gvIOS_Tickets_ColumnFilterChanged
            Dashboard_Tickets_Initialize()
            gvIOS_Tickets_FocusedRowChanged(Nothing, Nothing)

            Dashboard_Events_Load()
            WriteString_Log(Now() & "    " & "IOS - Loaded - Dashboard")
            AddHandler gvIOS_Tickets.FocusedRowChanged, AddressOf gvIOS_Tickets_FocusedRowChanged
            'AddHandler dcmb_Filter_Area.SelectedIndexChanged, AddressOf dcmb_Filter_Area_SelectedIndexChanged
            AddHandler gvIOS_Tickets.ColumnFilterChanged, AddressOf gvIOS_Tickets_ColumnFilterChanged
            AddHandler xtcMain.SelectedPageChanged, AddressOf xtcMain_SelectedPageChanged

            btnCreateDashboard.Enabled = Enabled
            btnDesignDashboard.Enabled = Enabled
            cmbDashboards.Enabled = Enabled

            LoadModuleDashboards()

            AddHandler gvEvents.ColumnPositionChanged, AddressOf gvEvents_ColumnPositionChanged
            AddHandler gvEvents.ColumnWidthChanged, AddressOf gvEvents_ColumnWidthChanged
            AddHandler gvEvents.ColumnFilterChanged, AddressOf gvEvents_ColumnFilterChanged
            AddHandler gvEvents.RowCellStyle, AddressOf gvEvents_RowCellStyle

            Dim EventTimerMins As Integer = GetConfigClientKeyValue("EventTimerMins")
            If EventTimerMins <> 0 Then
                dataRefreshTimer = New Threading.Timer(AddressOf RefreshGridData, Nothing, 0, EventTimerMins)
            End If

            AddHandler ReportViewer.DashboardChanged, AddressOf ReportViewer_DashboardChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub RefreshGridData(state As Object)
        'Asynchronously refresh events grid data
        If Me.InvokeRequired Then
            Me.Invoke(Sub()
                          Dashboard_Events_Load()
                      End Sub)
        Else
            Dashboard_Events_Load()
        End If
    End Sub

    'Private Sub Dashboard_Events_Load()
    '    Try
    '        RemoveHandler gvEvents.ColumnPositionChanged, AddressOf gvEvents_ColumnPositionChanged
    '        RemoveHandler gvEvents.ColumnWidthChanged, AddressOf gvEvents_ColumnWidthChanged
    '        RemoveHandler gvEvents.ColumnFilterChanged, AddressOf gvEvents_ColumnFilterChanged
    '        RemoveHandler gvEvents.RowCellStyle, AddressOf gvEvents_RowCellStyle

    '        Dim strConnection As String = Nothing
    '        Dim sqlParam As String = Nothing
    '        Dim parray()() As String = Nothing

    '        strConnection = GetSQL(8100, parray)(0)
    '        sqlParam = GetSQL(8100, parray)(1)

    '        dsEvents = DataAccessorODBC.GetDataSet(strConnection, sqlParam, iQryTimeOut)

    '        IOSDevExpressGrid.PopulateDataInGrid(gcEvents, gvEvents, dsEvents.Tables(0), "ALL", Nothing, Nothing, Nothing, dsGridColumnsConfig)

    '        AddHandler gvEvents.ColumnPositionChanged, AddressOf gvEvents_ColumnPositionChanged
    '        AddHandler gvEvents.ColumnWidthChanged, AddressOf gvEvents_ColumnWidthChanged
    '        AddHandler gvEvents.ColumnFilterChanged, AddressOf gvEvents_ColumnFilterChanged
    '        AddHandler gvEvents.RowCellStyle, AddressOf gvEvents_RowCellStyle

    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
    '    End Try
    'End Sub

    Private Sub Dashboard_Tickets_Initialize()
        'Combobox Area fill
        'Dim dszones As DataSet = New DataSet
        'dszones = DataAccessorODBC.GetDataSet(GetSQL(8002, Nothing, dt_IOS_SQL)(0), GetSQL(8002, Nothing, dt_IOS_SQL)(1))

        'Try
        'cbic = dcmb_Filter_Area.Properties.Items
        'cbic.BeginUpdate()
        'cbic.Add("PLMN")
        'If Not dszones Is Nothing Then
        '    If Not dszones.Tables(0) Is Nothing Then
        '        For Each drow In dszones.Tables(0).Rows
        '            If drow(0).ToString = "" Or drow(0).ToString Is Nothing Then
        '                cbic.Add("No Zone")
        '            Else
        '                cbic.Add(drow(0).ToString)
        '            End If
        '        Next
        '    End If
        'End If
        'cbic.EndUpdate()
        'dcmb_Filter_Area.SelectedIndex = 0
        'Catch ex As Exception
        '    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        'End Try

        'dszones.Dispose()
        'dszones = Nothing

        'combox tech fill
        'cbic.BeginUpdate()
        'cbic.Add("ALL")
        'Dim dv As New DataView(dt_IOS_ObjectConfig)

        'dv.RowFilter = "Len(Technology) = 2"
        'dv.Sort = "Technology ASC"

        'Dim distinctDT As DataTable = dv.ToTable(True, "Technology")
        'For Each dr As DataRow In distinctDT.Rows
        '    cbic.Add(dr("Technology").ToString)
        'Next
        'cbic.EndUpdate()
        'dv.Sort = "Vendor ASC"
        'distinctDT = dv.ToTable(True, "Vendor")
        'cbic.BeginUpdate()
        'For Each dr As DataRow In distinctDT.Rows
        '    cbic.Add(dr("Vendor").ToString)
        'Next
        'cbic.EndUpdate()

        'combobox prio fill
        'Dim dsprio As DataSet = New DataSet
        'dsprio = DataAccessorODBC.GetDataSet(GetSQL(8002, Nothing, dt_IOS_SQL)(0), "SELECT distinct Cat_Prio from IA_Score order by Cat_Prio")

        'Try
        '    cbic.BeginUpdate()
        '    cbic.Add("ALL")
        '    If Not dsprio Is Nothing Then
        '        If Not dsprio.Tables(0) Is Nothing Then
        '            For Each drow In dsprio.Tables(0).Rows
        '                If drow(0).ToString = "" Or drow(0).ToString Is Nothing Then
        '                    cbic.Add("No Prio")
        '                Else
        '                    cbic.Add(drow(0).ToString)
        '                End If
        '            Next
        '        End If
        '    End If
        '    cbic.EndUpdate()
        'Catch ex As Exception
        '    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        'End Try

        'dsprio.Dispose()
        'dsprio = Nothing

        'add chart of cellhistory
        '-------------------------
        xTabPage_CellHistory.Controls.Clear()
        Dim ch As Chart = New Chart()
        ch.Name = "Cell_History_Chart"
        ch.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"

        ch.Width = xTabPage_CellHistory.Width - 30
        ch.Height = 300
        'Chart Default Properties
        ch.DefaultElement.Marker.Visible = False
        ch.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        ch.LegendBox.DefaultEntry.Value = ""
        ch.XAxis.TickLabelMode = TickLabelMode.Angled
        ch.XAxis.TickLabelAngle = 45
        ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
        ch.ToolTip.InitialDelay = 1
        ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
        ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
        ch.XAxis.TimeInterval = TimeInterval.Days
        ch.XAxis.FormatString = "dd/MM/yy"
        ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
        ch.XAxis.TimeInterval = TimeInterval.Days
        ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
        ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
        ch.BackColor = Color.White
        xTabPage_CellHistory.Controls.Add(ch)

        'Add chart of Ticket Score
        '------------------------
        Dim chart1 As Chart = New Chart
        chart1.Type = ChartType.Donut
        chart1.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        chart1.ShadingEffectMode = ShadingEffectMode.Three
        gc_TicketScore.Controls.Add(chart1)
        chart1.Dock = DockStyle.Fill
        chart1.BackColor = Me.BackColor
        chart1.Background.Color = Me.BackColor
        gc_TicketScore.Padding = New Padding(7, 3, 7, 15)

        'Add chart of Ticket Root
        '------------------------
        Dim chart2 As Chart = New Chart
        chart2.Type = ChartType.Gauges
        chart2.DefaultSeries.GaugeType = GaugeType.Horizontal
        chart2.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        chart2.ShadingEffectMode = ShadingEffectMode.Three
        gc_TicketRoot.Controls.Add(chart2)
        chart2.Dock = DockStyle.Fill
        chart2.BackColor = Me.BackColor
        chart2.Background.Color = Me.BackColor
        gc_TicketRoot.Padding = New Padding(7, 3, 7, 15)

        Dim parray()() As String = {
            New String() {"@datum", Chr(39) & DateAdd(DateInterval.Day, -1, Now()).ToString("yyyyMMdd") & Chr(39)}
        }
        dsDashboardKPI = DataAccessorODBC.GetDataSet(GetSQL(8007, parray, dt_IOS_SQL)(0), GetSQL(8007, parray, dt_IOS_SQL)(1))

        'checkbox handlers
        ce_TicketOpen.Checked = True
        AddHandler ce_TicketOpen.CheckedChanged, AddressOf ce_TicketOpen_CheckedChanged
        'run tickets
        '---------
        Dashboard_Tickets_Update()
    End Sub

    Private Sub Dashboard_Tickets_Update()
        'load dataset
        'Try
        '    Dim tester As String = Nothing
        '    tester = dcmb_Filter_Area.Text
        '    If tester = "" Then
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    Exit Sub
        'End Try

        Try
            'Dim ZoneParam As String = dcmb_Filter_Area.EditValue
            'Select Case dcmb_Filter_Area.SelectedItem.ToString
            '    Case "None"
            '        ZoneParam = "=" & Chr(39) & Chr(39)
            '    Case "PLMN"
            '        ZoneParam = "Like " & Chr(39) & "%" & Chr(39)
            '    Case Else
            '        ZoneParam = "=" & Chr(39) & dcmb_Filter_Area.SelectedItem.ToString & Chr(39)
            'End Select
            RemoveHandler gvIOS_Tickets.ColumnFilterChanged, AddressOf gvIOS_Tickets_ColumnFilterChanged

            Dim TicketClosed As String = Nothing
            Select Case ce_TicketOpen.Checked
                Case True
                    TicketClosed = "=0"
                Case False
                    TicketClosed = "Like " & Chr(39) & "%" & Chr(39)
            End Select

            Dim parray()() As String = {
                New String() {"@TicketClosed", TicketClosed}
            }

            dsTickets = Nothing
            dsTickets = New System.Data.DataSet
            dsTickets = DataAccessorODBC.GetDataSet(GetSQL(8000, parray, dt_IOS_SQL)(0), GetSQL(8000, parray, dt_IOS_SQL)(1))

            'fill tlv
            If dsTickets.Tables.Count = 0 Then
                Exit Sub
            End If

            dgvIOS_Tickets.Visible = False
            'gvIOS_Tickets.Columns.Clear()
            'dgvIOS_Tickets.Refresh()

            If (dsTickets.Tables(0).Rows.Count > 0) Then
                dgvIOS_Tickets.DataSource = dsTickets.Tables(0)
                dgvIOS_Tickets.RefreshDataSource()
            End If

            For columnIndex As Integer = 0 To gvIOS_Tickets.Columns.Count - 1
                Dim columnName As String = gvIOS_Tickets.Columns(columnIndex).Name
                If columnName.Contains("%") Then
                    gvIOS_Tickets.Columns(columnIndex).Visible = False
                End If
            Next
            dgvIOS_Tickets.Visible = True

            For Each column As DevExpress.XtraGrid.Columns.GridColumn In gvIOS_Tickets.Columns
                Select Case column.VisibleIndex
                    Case Is < 3
                        column.AppearanceCell.BackColor = Color.AliceBlue
                    Case Is < 7
                        column.AppearanceCell.BackColor = Color.LightYellow
                    Case Is < 11
                        column.AppearanceCell.BackColor = Color.LightGoldenrodYellow
                    Case Else
                        column.AppearanceCell.BackColor = Color.LightCoral
                End Select
            Next

            gvIOS_Tickets.BestFitColumns()

            AddHandler gvIOS_Tickets.ColumnFilterChanged, AddressOf gvIOS_Tickets_ColumnFilterChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try

        'Getting ticket stats
        Dim dsTicketStats As New DataSet

        Try
            Dim ZoneParam As String = Nothing
            Select Case filterZoneValue
                Case "None"
                    ZoneParam = Chr(39) & Chr(39)
                Case "PLMN"
                    ZoneParam = Chr(39) & "___" & Chr(39)
                Case Else
                    ZoneParam = Chr(39) & filterZoneValue & Chr(39)
            End Select

            Dim parray3()() As String = {New String() {"@Zone", ZoneParam}}

            dsTicketStats = DataAccessorODBC.GetDataSet(GetSQL(8008, parray3, dt_IOS_SQL)(0), GetSQL(8008, parray3, dt_IOS_SQL)(1))

            lbl_TicketsOpen.Text = "0"
            lbl_TicketsAssigned.Text = "0"
            lbl_TicketsClosed.Text = "0"
            lbl_TimeOpen.Text = "0"
            lbl_LongOpen.Text = "0"

            For Each drow As DataRow In dsTicketStats.Tables(0).Rows
                Select Case drow(0).ToString.ToUpper.Trim
                    Case "OPEN"
                        lbl_TicketsOpen.Text = drow(1).ToString.Trim
                    Case "ASSIGNED"
                        lbl_TicketsAssigned.Text = drow(1).ToString.Trim
                    Case "CLOSED"
                        lbl_TicketsClosed.Text = drow(1).ToString.Trim
                    Case "AVGTIMETOCLOSE"
                        lbl_TimeOpen.Text = drow(1).ToString.Trim
                    Case "LONGESTOPEN"
                        lbl_LongOpen.Text = drow(1).ToString.Trim
                End Select
            Next
            dsTicketStats.Dispose()
            dsTicketStats = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            If Not dsTicketStats Is Nothing Then
                dsTicketStats.Dispose()
            End If
            dsTicketStats = Nothing
        End Try
    End Sub

    Private Sub ce_TicketOpen_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dashboard_Tickets_Update()
    End Sub

    Private Sub gvIOS_Tickets_ColumnFilterChanged(sender As Object, e As EventArgs) 'Handles gvIOS_Tickets.ColumnFilterChanged
        Dim gv As Views.Grid.GridView = TryCast(sender, Views.Grid.GridView)
        Dim activeFilterCount As Integer = gv.ActiveFilter.Count
        For i As Integer = 0 To activeFilterCount - 1
            If (gv.ActiveFilter(i).Column.FieldName.ToLower = "zone") Then
                filterZoneValue = gv.ActiveFilter(i).Filter.FilterString.ToString.Split(" = ")(2).Replace("'", "")
            End If
        Next
        'Dashboard_Tickets_Update()
    End Sub

    'Private Sub dcmb_Filter_Area_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        If cmbFilterValue_Old <> dcmb_Filter_Area.SelectedItem.ToString Then
    '            cmbFilterValue_Old = dcmb_Filter_Area.SelectedItem.ToString
    '            Dashboard_Tickets_Update()
    '        End If
    '    Catch
    '    End Try
    'End Sub

    Private Sub tlv_IOS_Tickets_Clicked(sender As Object, e As EventArgs)
        Exit Sub
    End Sub

    Private Sub btn_TicketUpdate_Click(sender As Object, e As EventArgs) Handles btn_TicketUpdate.Click
        Dim username As String = Chr(39) & Environment.UserName & Chr(39)
        Dim datum As String = Chr(39) & Now.ToString("yyyyMMdd") & Chr(39)
        Dim ticketstatusupdate As String = Nothing
        Dim ticketid As Integer = Nothing

        If dcmb_TicketStatusUpdate.SelectedItem Is Nothing Then
            Exit Sub
        End If
        ticketstatusupdate = Chr(39) & dcmb_TicketStatusUpdate.SelectedItem.ToString & Chr(39)

        If tlv_TicketHistory.Nodes.Count = 0 Then
            Exit Sub
        ElseIf tlv_TicketHistory.Nodes(tlv_TicketHistory.Nodes.Count - 1)("TicketStatus").ToString.ToUpper.Trim = "CLOSED" Then
            MsgBox("Ticket Already Closed! No Action.")
            Exit Sub
        Else
            ticketid = Val(tlv_TicketHistory.Nodes(0)("TicketID").ToString)
        End If

        'create connection
        Dim sql_insert As String = Nothing
        Dim connstring As String = Nothing

        Try
            Dim parray()() As String = {
                New String() {"@TicketID", ticketid},
                New String() {"@TicketStatus", ticketstatusupdate},
                New String() {"@TicketUpdateDate", datum},
                New String() {"@UserID", username},
                New String() {"@UserComment", Chr(39) & txt_TicketComment.Text & Chr(39)}
            }

            sql_insert = GetSQL(8003, parray, dt_IOS_SQL)(1)
            connstring = GetSQL(8003, parray, dt_IOS_SQL)(0)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Exit Sub
        End Try

        Dim sql_insert_closed As String = Nothing
        If dcmb_TicketStatusUpdate.SelectedItem.ToString.ToUpper = "CLOSED" Then
            Dim parray()() As String = {
                New String() {"@TicketID", ticketid}
            }
            sql_insert_closed = GetSQL(8004, parray, dt_IOS_SQL)(1)
        End If

        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcCommand = Nothing

        Try
            cnQODBC = New System.Data.Odbc.OdbcConnection(connstring)
            cnQODBC.ConnectionTimeout = 5
            cnQODBC.Open()
            daQODBC = New System.Data.Odbc.OdbcCommand(sql_insert, cnQODBC)

            daQODBC.ExecuteNonQuery()

            If Not sql_insert_closed Is Nothing Then
                daQODBC = New System.Data.Odbc.OdbcCommand(sql_insert_closed, cnQODBC)
                daQODBC.ExecuteNonQuery()
            End If

            cnQODBC.Close()
            daQODBC.Dispose()
            cnQODBC.Dispose()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)

            If Not cnQODBC Is Nothing Then
                cnQODBC.Close()
            End If
            If Not daQODBC Is Nothing Then
                daQODBC.Dispose()
            End If
            If Not cnQODBC Is Nothing Then
                cnQODBC.Dispose()
            End If

        End Try
        Dashboard_Tickets_Update()
    End Sub

    Private Sub CreateSubItems(ByVal parentNode As TreeListViewNode, ByVal sufix As String)
        'Create subitems for specified node
        Dim subItem As TreeListViewSubItem = Nothing
        subItem = New TreeListViewSubItem(sufix)
        parentNode.SubItems.Add(subItem)
    End Sub

#End Region

#Region "Dashboard Reports"

    Private Sub btnCreateDashboard_Click(sender As Object, e As EventArgs) Handles btnCreateDashboard.Click
        Try
            Dim objCreateDashbaord As New dlgCreateDashboard()
            objCreateDashbaord.ShowDialog()

            If objCreateDashbaord.DialogResult = DialogResult.OK Then
                Dim parray()() As String = {
                    New String() {"@DashboardName", Chr(39) & objCreateDashbaord.ReportName & Chr(39)},
                    New String() {"@DashboardOwner", Chr(39) & Environment.UserName & Chr(39)},
                    New String() {"@DashboardModule", Chr(39) & "Dashboard" & Chr(39)},
                    New String() {"@JobID", "NULL"},
                    New String() {"@AccessFlag", Chr(39) & objCreateDashbaord.AccessType & Chr(39)}
                }
                Dim strConnection As String = GetSQL(8101, parray)(0)
                Dim sqlParam As String = GetSQL(8101, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                LoadModuleDashboards()
                SetComboBox(cmbDashboards, ComboSelectBased.TextBased, objCreateDashbaord.ReportName)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub LoadModuleDashboards()
        Try
            RemoveHandler cmbDashboards.SelectedIndexChanged, AddressOf cmbDashboards_SelectedIndexChanged
            LoadAllDashboardReports()
            If dtDashboardReports.Rows.Count > 0 Then
                dtDBReports = dtDashboardReports.AsEnumerable().Where(Function(x) x.Field(Of String)("DashboardModule") = "Dashboard" AndAlso (x.Field(Of String)("DashboardOwner") = Environment.UserName Or x.Field(Of String)("AccessFlag") = "Public")).CopyToDataTable()
                BindDevExComboBoxWithValueMember(cmbDashboards, dtDBReports, "DashboardID", "DashboardName", "Select")
                AddHandler cmbDashboards.SelectedIndexChanged, AddressOf cmbDashboards_SelectedIndexChanged
                ReportViewer.Dashboard = Nothing
            Else
                dtDBReports = Nothing
                BindDevExComboBoxWithValueMember(cmbDashboards, Nothing, "DashboardID", "DashboardName", "Select")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub cmbDashboards_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbDashboards.SelectedIndex > 0 Then

                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                LoadDashboardReport()
            Else
                ReportViewer.Dashboard = Nothing
                If rbAutomatic.Checked Then
                    rbAutomatic.Checked = False
                    rbManual.Checked = True
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDesignDashboard_Click(sender As Object, e As EventArgs) Handles btnDesignDashboard.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dashboardID As Integer = Nothing
            If cmbDashboards.SelectedIndex > 0 Then
                dashboardID = CInt(TryCast(cmbDashboards.SelectedItem, clsComboBoxItem).Value)
                Dim objDashDesigner As New frmDashboardDesigner()
                objDashDesigner.dashboardID = dashboardID
                objDashDesigner.dashboardName = cmbDashboards.SelectedItem.ToString
                objDashDesigner.ShowDialog()
            End If

            LoadModuleDashboards()
            SetComboBox(cmbDashboards, ComboSelectBased.ValueBased, dashboardID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadDashboardReport()
        Try
            Dim dashboardXmlFile As String = Nothing
            Dim dashboardID As Integer = CType(cmbDashboards.SelectedItem, clsComboBoxItem).Value
            Dim str = dtDBReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("DashboardID") = dashboardID)(0)("DashboardFile").ToString
            'str = str.Replace("''", "'")

            If str.Trim.Contains("<?xml") Then
                dashboardXmlFile = str
            Else
                dashboardXmlFile = GetDecryptedConnectionString(str)
            End If

            Dim ms As New System.IO.MemoryStream()
            ms = StringToStream(dashboardXmlFile)

            If ms.Length <> 0 Then
                ReportViewer.LoadDashboard(ms)
                'If ReportViewer.Dashboard.Parameters.Count <> 0 Then
                '    Dim grid As GridDashboardItem = CType(ReportViewer.Dashboard.Items(0), GridDashboardItem)
                '    Dim param = New DashboardParameter()
                '    grid.FilterString = ReportViewer.Dashboard.Parameters.Item(0).Name & " in " & ReportViewer.Dashboard.Parameters.Item(0).Value 'SQL_ID in (?SQL_ID)"
                'End If
            Else
                ReportViewer.Dashboard = Nothing
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub ReportViewer_CustomParameters(ByVal sender As Object, ByVal e As CustomParametersEventArgs) Handles ReportViewer.CustomParameters
        Try
            Dim grid As GridDashboardItem = CType(ReportViewer.Dashboard.Items(0), GridDashboardItem)
            Dim customParameter = e.Parameters.FirstOrDefault(Function(p) p.Name = "SQL_ID")
            If customParameter IsNot Nothing Then
                ' Actual value used when retrieving data from the data source.  
                grid.FilterString = "SQL_ID in (?" & customParameter.Name & ")"
            Else
                grid.FilterString = Nothing
            End If
        Catch
        End Try
    End Sub

    Private Sub ReportViewer_DashboardChanged(sender As Object, e As EventArgs)
        Try
            SetTimeout()
            AddHandler ReportViewer.Dashboard.DataSourceCollectionChanged, AddressOf ReportViewer_Dashboard_DataSourceCollectionChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ReportViewer_Dashboard_DataSourceCollectionChanged(sender As Object, e As NotifyingCollectionChangedEventArgs(Of IDashboardDataSource))
        SetTimeout()
    End Sub

    Private Sub SetTimeout()
        Try
            For Each dataSoure In ReportViewer.Dashboard.DataSources
                If TypeOf dataSoure Is SqlDataSource Then
                    TryCast(dataSoure, SqlDataSource).ConnectionOptions.CommandTimeout = 300
                End If
            Next
        Catch
        End Try
    End Sub

#Region "Backward Compatibility"

    Private Sub ReportViewer_ConfigureDataConnection(ByVal sender As Object, ByVal e As DashboardConfigureDataConnectionEventArgs) Handles ReportViewer.ConfigureDataConnection
        Try
            If ReportViewer.Dashboard.DataSources.Count > 1 Then
                'multi data sources of diff kind
                For Each ds As DashboardSqlDataSource In ReportViewer.Dashboard.DataSources
                    ds.ConnectionOptions.CommandTimeout = 300
                    If ds.Connection.ProviderKey.ToUpper = "POSTGRES" Then
                        'ds.ConnectionParameters = CreateConnectionParametersPostGreSql()
                        Dim params As PostgreSqlConnectionParameters = TryCast(e.ConnectionParameters, PostgreSqlConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(3000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.PortNumber = connString.Split(";")(1).Split("=")(1)
                            params.UserName = connString.Split(";")(2).Split("=")(1)
                            params.Password = connString.Split(";")(3).Split("=")(1)
                            params.DatabaseName = connString.Split(";")(4).Split("=")(1)
                        End If
                    ElseIf ds.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then
                        'ds.ConnectionParameters = CreateConnectionParametersSql()
                        Dim params As MsSqlConnectionParameters = TryCast(e.ConnectionParameters, MsSqlConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(2000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.DatabaseName = connString.Split(";")(1).Split("=")(1)
                            params.AuthorizationType = MsSqlAuthorizationType.SqlServer
                            params.UserName = connString.Split(";")(2).Split("=")(1)
                            params.Password = connString.Split(";")(3).Split("=")(1)
                        End If
                    ElseIf ds.Connection.ProviderKey.ToUpper = "ORACLESERVER" Then
                        'ds.ConnectionParameters = CreateConnectionParametersOracle()
                        Dim params As OracleConnectionParameters = TryCast(e.ConnectionParameters, OracleConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(4000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.UserName = connString.Split(";")(1).Split("=")(1)
                            params.Password = connString.Split(";")(2).Split("=")(1)
                            params.ProviderType = OracleProviderType.ODPManaged
                        End If
                    End If
                Next
            Else
                'single data source of a kind
                Dim ds As DashboardSqlDataSource = ReportViewer.Dashboard.DataSources(0)
                If ds.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then
                    e.ConnectionParameters = CreateConnectionParameters()
                ElseIf ds.Connection.ProviderKey.ToUpper = "POSTGRES" Then
                    e.ConnectionParameters = CreateConnectionParametersPostGreSql()
                ElseIf ds.Connection.ProviderKey.ToUpper = "ORACLESERVER" Then
                    e.ConnectionParameters = CreateConnectionParametersOracle()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Function CreateConnectionParameters() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(2000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New MsSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .DatabaseName = connString.Split(";")(1).Split("=")(1),
            .AuthorizationType = MsSqlAuthorizationType.SqlServer,
            .UserName = connString.Split(";")(2).Split("=")(1),
            .Password = connString.Split(";")(3).Split("=")(1)
        }
    End Function

    Private Function CreateConnectionParametersPostGreSql() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(3000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New PostgreSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .PortNumber = connString.Split(";")(1).Split("=")(1),
            .UserName = connString.Split(";")(2).Split("=")(1),
            .Password = connString.Split(";")(3).Split("=")(1),
            .DatabaseName = connString.Split(";")(4).Split("=")(1)
        }
    End Function

    Private Function CreateConnectionParametersOracle() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(3000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New OracleConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .UserName = connString.Split(";")(1).Split("=")(1),
            .Password = connString.Split(";")(2).Split("=")(1),
            .ProviderType = OracleProviderType.ODPManaged
        }
    End Function

#End Region

    Private Sub btnDeleteReport_Click(sender As Object, e As EventArgs) Handles btnDeleteReport.Click
        Try
            Dim isPowerUser As Boolean = False
            Dim reportOwner As String = Nothing

            If cmbDashboards.SelectedIndex > 0 Then
                If XtraMessageBox.Show("Are you sure to delete report: " & cmbDashboards.SelectedItem.ToString & "?", "Delete Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()

                    Dim dashboardID As Integer = CInt(TryCast(cmbDashboards.SelectedItem, clsComboBoxItem).Value)
                    reportOwner = dtDBReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("DashboardID") = dashboardID)(0)("DashboardOwner").ToString

                    'checking current user is the report owner
                    If reportOwner.ToLower = Environment.UserName.ToLower Then
                        isPowerUser = True
                    End If

                    If reportOwner.ToLower <> Environment.UserName.ToLower Then
                        'checking whether the current user (not report owner) is a power user
                        If configMgr.User.IsPowerUser = True Then
                            isPowerUser = True
                        Else
                            SetMessage("Current user can't delete the report as the report owner is a different user.")
                            isPowerUser = False
                        End If
                    End If

                    If (isPowerUser = True) Then
                        DeleteDashboardReport(dashboardID)
                        LoadModuleDashboards()
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub DeleteDashboardReport(dashboardID As Integer)
        Dim parray()() As String = {
            New String() {"@DashboardID", dashboardID}
        }
        Dim strConnection As String = GetSQL(9321, parray)(0)
        Dim sqlParam As String = GetSQL(9321, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub btnSendToWebRptSrvr_Click(sender As Object, e As EventArgs) Handles btnSendToWebRptSrvr.Click
        Try
            'ExportViewerPdf()
            Dim localPath As String = GetConfigClientKeyValue("DashboardFileLocal") '"C:\CIOS\DashboardFiles"
            Dim uncPath As String = GetConfigClientKeyValue("DashboardFileRemote")  '"\\10.244.98.161\DashboardFiles"

            If (localPath Is Nothing) Or (uncPath Is Nothing) Then
                SetMessage("Either local or remote server dashboard file path not configured")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If Directory.Exists(uncPath) Then
                If cmbDashboards.SelectedIndex > 0 Then
                    Dim dashboardXmlFile As String = Nothing
                    Dim dashboardID As Integer = CType(cmbDashboards.SelectedItem, clsComboBoxItem).Value
                    Dim str = dtDBReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("DashboardID") = dashboardID)(0)("DashboardFile").ToString

                    If str.Trim.Contains("<?xml") Then
                        dashboardXmlFile = str
                    Else
                        dashboardXmlFile = GetDecryptedConnectionString(str)
                    End If

                    Dim dbFileName As String = cmbDashboards.SelectedItem.ToString & ".xml"
                    Dim di As DirectoryInfo = Nothing

                    If Not Directory.Exists(localPath) Then
                        Directory.CreateDirectory(localPath)
                        di = New DirectoryInfo(localPath)
                        Dim dsDR As DirectorySecurity = di.GetAccessControl()
                        dsDR.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit Or InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
                        di.SetAccessControl(dsDR)
                    Else
                        di = New DirectoryInfo(localPath)
                        Dim dsDR As DirectorySecurity = di.GetAccessControl()
                        di.SetAccessControl(dsDR)
                    End If

                    'writing dashboard xml file to local machine
                    Using writer As New StreamWriter(di.FullName & "\" & dbFileName, False)
                        writer.WriteLine(dashboardXmlFile)
                    End Using

                    'remove the pre-existing file on the remote server having the same name
                    If File.Exists(uncPath & "\" & dbFileName) Then
                        File.Delete(uncPath & "\" & dbFileName)
                    End If

                    'copy dashboard xml file from local machine to remote server machine
                    File.Copy(di.FullName & "\" & dbFileName, uncPath & "\" & dbFileName)

                    'Delete locally saved xml file
                    File.Delete(di.FullName & "\" & dbFileName)

                    SetMessage("Dashboard File Sent Successfully To Report Server")

                End If
            Else
                SetMessage("Report server directory doesn't exist")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            If cmbDashboards.SelectedIndex > 0 Then

                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                LoadDashboardReport()
            Else
                ReportViewer.Dashboard = Nothing
                SetMessage("Please Select Report")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Public Function ExportViewerPdf()
        '********** Export PDF
        Dim pdfOptions As DashboardPdfExportOptions = New DashboardPdfExportOptions With {
            .PageLayout = DashboardExportPageLayout.Landscape,
            .ShowTitle = True
        }

        Using fs As FileStream = New FileStream("C:\CIOS_Backup\CIOS_Docs\Dashboard.pdf", FileMode.Create)
            ReportViewer.ExportToPdf(fs, pdfOptions)
            Return Nothing
        End Using

        '********** Export Excel
        Dim ExcelOptions As DashboardExcelExportOptions = New DashboardExcelExportOptions With {
            .Format = ExcelFormat.Xlsx
        }

        Using fs As FileStream = New FileStream("C:\CIOS_Backup\CIOS_Docs\Dashboard.xlsx", FileMode.Create)
            ReportViewer.ExportToExcel(fs, ExcelOptions)
            Return Nothing
        End Using

    End Function

#End Region

#Region "Context Menu"

    Private Sub cm_IOS_Tickets_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cm_IOS_Tickets.Opening
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim VIP_Site As String = Nothing

            If cm_IOS_Tickets.SourceControl.Name = "dgvIOS_Tickets" Then
                VIP_Site = gvIOS_Tickets.GetFocusedRowCellValue("VIP_Site")
                tsmi_TicketMapAll.Enabled = True

                If VIP_Site.Length > 3 Then
                    tsmi_ticketremedy.Enabled = True
                Else
                    tsmi_ticketremedy.Enabled = False
                End If

            ElseIf cm_IOS_Tickets.SourceControl.Name = "gcEvents" Then
                tsmi_TicketMapAll.Enabled = False
                tsmi_ticketremedy.Enabled = False
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_TicketMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmi_TicketMap.Click
        Try
            Dim dr As DataRow = Nothing
            Dim siteIndex, techIndex, vendorIndex As Short
            Dim iosTechColIndex As Integer = Nothing
            Dim objectType As String = Nothing
            Dim tech As String = Nothing

            If cm_IOS_Tickets.SourceControl.Name = "dgvIOS_Tickets" Then

                dr = gvIOS_Tickets.GetFocusedDataRow()
                siteIndex = dr.Table.Columns("SITE").Ordinal
                vendorIndex = dr.Table.Columns("VENDOR").Ordinal
                techIndex = dr.Table.Columns("TECH").Ordinal
                tech = dr(vendorIndex).ToString & " " & dr(techIndex).ToString

                If siteIndex <> 0 Then
                    frmMapWindow.Cells_SearchAndDisplay(dr(siteIndex).ToString, tech, "SITE")
                End If

            ElseIf cm_IOS_Tickets.SourceControl.Name = "gcEvents" Then

                dr = gvEvents.GetFocusedDataRow()

                siteIndex = dr.Table.Columns("ObjectName").Ordinal
                iosTechColIndex = dr.Table.Columns("IOS_TECH").Ordinal
                Dim objTypeColIndex As Integer = dr.Table.Columns("ObjectType").Ordinal

                tech = dr(iosTechColIndex).ToString.Trim
                objectType = dr(objTypeColIndex).ToString.Trim

                If siteIndex <> 0 Then
                    frmMapWindow.Cells_SearchAndDisplay(dr(siteIndex).ToString, tech, objectType)
                End If

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_TicketMapAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_TicketMapAll.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dt As DataTable = Nothing
            If cm_IOS_Tickets.SourceControl.Name = "dgvIOS_Tickets" Then
                dt = dsTickets.Tables(0).Copy
            End If

            Dim i As Integer = 0
            While i < dt.Columns.Count
                If dt.Columns(i).ColumnName.Contains("%") Then
                    dt.Columns.Remove(dt.Columns(i))
                Else
                    i += 1
                End If
            End While
            frmMapWindow.Ticket_Map(dt, "Score")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_TicketLaunch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmi_TicketLaunch.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dr As DataRow = Nothing
            Dim tech As String = Nothing
            Dim objectType As String = Nothing
            objfrmTechnology = Nothing

            If cm_IOS_Tickets.SourceControl.Name = "dgvIOS_Tickets" Then

                dr = gvIOS_Tickets.GetFocusedDataRow()
                Dim siteColIndex As Integer = dr.Table.Columns("SITE").Ordinal
                Dim techColIndex As Integer = dr.Table.Columns("TECH").Ordinal
                Dim vendorColIndex As Integer = dr.Table.Columns("VENDOR").Ordinal

                tech = dr(vendorColIndex).ToString.Trim & " " & dr(techColIndex).ToString.Trim
                If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                    frmMDI.OpenTechFormDynamically(tech, objfrmTechnology, True)
                Else
                    objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
                End If

                If siteColIndex <> 0 Then
                    If tech.ToUpper = objfrmTechnology.Network.ToUpper Then

                        If objfrmTechnology.btnApplyStats.Text = "Abort" Then
                            MsgBox("A thread is already running! Try again later.")
                            Exit Sub
                        End If

                        'set stat parameters correctly
                        objfrmTechnology.SetButtonsForLaunch(objfrmTechnology.Network, flpSourceBtn_GetType(objfrmTechnology.Network, "SITE", objfrmTechnology.flpCounterTypeStats).SourceButtonText, False)
                        objfrmTechnology.FindNodeTreeviewStats(dr(siteColIndex).ToString.Trim)
                        objfrmTechnology.rdoDailyStats.Checked = True

                        'launch
                        Call objfrmTechnology.btnApplyStats_Click(objfrmTechnology.btnApplyStats, Nothing)
                        objfrmTechnology.xtcTechnology.SelectedTabPage = objfrmTechnology.xtcTechnology.TabPages(0)
                    End If
                End If

            ElseIf cm_IOS_Tickets.SourceControl.Name = "gcEvents" Then

                dr = gvEvents.GetFocusedDataRow()
                Dim objNameColIndex As Integer = dr.Table.Columns("ObjectName").Ordinal
                Dim iosTechColIndex As Integer = dr.Table.Columns("IOS_TECH").Ordinal
                Dim objTypeColIndex As Integer = dr.Table.Columns("ObjectType").Ordinal

                tech = dr(iosTechColIndex).ToString.Trim
                objectType = dr(objTypeColIndex).ToString.Trim
                If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                    frmMDI.OpenTechFormDynamically(tech, objfrmTechnology, True)
                Else
                    objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
                End If

                If objNameColIndex <> 0 Then
                    If tech.ToUpper = objfrmTechnology.Network.ToUpper Then

                        If objfrmTechnology.btnApplyStats.Text = "Abort" Then
                            MsgBox("A thread is already running! Try again later.")
                            Exit Sub
                        End If

                        'set stat parameters correctly
                        objfrmTechnology.SetButtonsForLaunch(objfrmTechnology.Network, objectType, False)
                        objfrmTechnology.FindNodeTreeviewStats(dr(objNameColIndex).ToString.Trim)
                        objfrmTechnology.rdoDailyStats.Checked = True

                        'launch
                        Call objfrmTechnology.btnApplyStats_Click(objfrmTechnology.btnApplyStats, Nothing)
                        objfrmTechnology.xtcTechnology.SelectedTabPage = objfrmTechnology.xtcTechnology.TabPages(0)
                    End If
                End If

            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_TicketObjectTree_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmi_TicketObjectTree.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

        Dim dr As DataRow = Nothing
        Dim siteColIndex As Integer = Nothing
        Dim techColIndex As Integer = Nothing
        Dim vendorColIndex As Integer = Nothing
        Dim tech As String = Nothing
        Dim objectType As String = Nothing
        objfrmTechnology = Nothing

        Try
            If cm_IOS_Tickets.SourceControl.Name = "dgvIOS_Tickets" Then

                dr = gvIOS_Tickets.GetFocusedDataRow()
                siteColIndex = dr.Table.Columns("SITE").Ordinal
                techColIndex = dr.Table.Columns("TECH").Ordinal
                vendorColIndex = dr.Table.Columns("VENDOR").Ordinal
                tech = dr(vendorColIndex).ToString.Trim & " " & dr(techColIndex).ToString.Trim

                If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                    frmMDI.OpenTechFormDynamically(tech, objfrmTechnology, False)
                Else
                    objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
                End If

                If siteColIndex <> 0 Then
                    If tech.ToUpper = objfrmTechnology.Network.ToUpper Then
                        If objfrmTechnology.btnApplyStats.Text = "Abort" Then
                            MsgBox("A thread is already running! Try again later.")
                            Exit Sub
                        End If
                        objfrmTechnology.SetButtonsForLaunch(objfrmTechnology.Network, flpSourceBtn_GetType(objfrmTechnology.Network, "SITE", objfrmTechnology.flpCounterTypeStats).SourceButtonText, False)
                        objfrmTechnology.FindNodeTreeviewStats(dr(siteColIndex).ToString.Trim)
                    End If
                End If

            ElseIf cm_IOS_Tickets.SourceControl.Name = "gcEvents" Then

                dr = gvEvents.GetFocusedDataRow()
                siteColIndex = dr.Table.Columns("ObjectName").Ordinal
                Dim iosTechColIndex As Integer = dr.Table.Columns("IOS_TECH").Ordinal
                Dim objTypeColIndex As Integer = dr.Table.Columns("ObjectType").Ordinal

                objectType = dr(objTypeColIndex).ToString.Trim
                tech = dr(iosTechColIndex).ToString.Trim

                If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                    frmMDI.OpenTechFormDynamically(tech, objfrmTechnology, False)
                Else
                    objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
                End If

                If siteColIndex <> 0 Then
                    If tech.ToUpper = objfrmTechnology.Network.ToUpper Then
                        If objfrmTechnology.btnApplyStats.Text = "Abort" Then
                            MsgBox("A thread is already running! Try again later.")
                            Exit Sub
                        End If
                        objfrmTechnology.SetButtonsForLaunch(objfrmTechnology.Network, objectType, False)
                        objfrmTechnology.FindNodeTreeviewStats(dr(siteColIndex).ToString.Trim)
                    End If
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ticketremedy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsmi_ticketremedy.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dr As DataRow = Nothing
            Dim inm As String = Nothing

            If cm_IOS_Tickets.SourceControl.Name = "dgvIOS_Tickets" Then
                dr = gvIOS_Tickets.GetFocusedDataRow()
                inm = dr("VIP_Site").ToString
                If inm.Length > 3 Then
                    frmTicketDetail.Show()
                    frmTicketDetail.FetchTicket(inm.Substring(3))
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvIOS_Tickets_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        Try
            tlv_TicketHistory.SuspendLayout()
            If Not e Is Nothing And ticketChange_Old = gvIOS_Tickets.GetFocusedDataRow("TicketId") Then
                Exit Sub
            Else
                ticketChange_Old = gvIOS_Tickets.GetFocusedDataRow("TicketId")
            End If

            Dim dr As DataRow = gvIOS_Tickets.GetFocusedDataRow()
            If dr Is Nothing Then
                Exit Sub
            End If
            Dim ticket_selected = dr("TicketId").ToString

            Dim parray()() As String = {
                New String() {"@Ticket", ticket_selected}
            }
            Dim dsTicketsDetails As DataSet = New System.Data.DataSet
            dsTicketsDetails = DataAccessorODBC.GetDataSet(GetSQL(8001, parray, dt_IOS_SQL)(0), GetSQL(8001, parray, dt_IOS_SQL)(1))

            If dsTicketsDetails Is Nothing Then
                dsTicketsDetails.Dispose()
                Exit Sub
            End If
            If dsTicketsDetails.Tables.Count = 0 Then
                dsTicketsDetails.Dispose()
                dsTicketsDetails = Nothing
                Exit Sub
            End If

            'fill tlv
            tlv_TicketHistory.Nodes.Clear()
            tlv_TicketHistory.Columns.Clear()

            Try
                If tlv_TicketHistory.Nodes.Count = 0 Then
                    'creating columns
                    For Each dcol As DataColumn In dsTicketsDetails.Tables(0).Columns
                        Dim column As Columns.TreeListColumn = New Columns.TreeListColumn()
                        column.Caption = dcol.ColumnName
                        column.VisibleIndex = dcol.Ordinal
                        tlv_TicketHistory.Columns.Add(column)
                    Next

                    Dim parentNode As TreeListNode = Nothing

                    For Each drow As DataRow In dsTicketsDetails.Tables(0).Rows
                        'adding node in first column

                        ''Dim parentnode As TreeListViewNode = New TreeListViewNode(drow(0).ToString)
                        ''parentnode.Key = drow(0).ToString
                        ''Dim newsubitem As TreeListViewSubItem = New TreeListViewSubItem(drow(0).ToString)
                        ''parentnode.SubItems.Add(newsubitem)
                        ''tlv_TicketHistory.Nodes.Add(parentnode)

                        Dim colArray As New List(Of Object)
                        colArray.Add(drow(0).ToString)
                        'parentNode = tlv_TicketHistory.Nodes.Add(New Object() {drow(0).ToString})

                        'adding subitms
                        Dim j As Integer = 1
                        For j = 1 To dsTicketsDetails.Tables(0).Columns.Count - 1
                            'Dim newsubitem2 = New TreeListViewSubItem(drow(j).ToString)
                            'parentNode.SubItems.Add(newsubitem2)
                            colArray.Add(drow(j).ToString)
                        Next

                        parentNode = tlv_TicketHistory.Nodes.Add(colArray.ToArray())
                    Next

                End If
                'For Each col As TreeListViewColumn In tlv_TicketHistory.Columns
                '    tlv_TicketHistory.AutoSizeColumn(col)
                'Next

                tlv_TicketHistory.ResumeLayout()
                dsTicketsDetails.Dispose()
                dsTicketsDetails = Nothing
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                dsTicketsDetails.Dispose()
                dsTicketsDetails = Nothing
            End Try

            'update ticket rule details
            Dim dsTicketRules As DataSet = New System.Data.DataSet

            Try
                Dim siteColIndex = dr.Table.Columns("SITE").Ordinal
                Dim creationDateColIndex = dr.Table.Columns("CREATIONDATE").Ordinal

                Dim parray2()() As String =
                {
                    New String() {"@Cell", Chr(39) & dr(siteColIndex).ToString & Chr(39)},
                    New String() {"@Date", Convert.ToInt32(dr(creationDateColIndex).ToString)},
                    New String() {"@Tech", Chr(39) & dr("TECH").ToString & Chr(39)}
                }

                dsTicketRules = DataAccessorODBC.GetDataSet(GetSQL(8009, parray2, dt_IOS_SQL)(0), GetSQL(8009, parray2, dt_IOS_SQL)(1))

                tlv_TicketDetails.Columns.Clear()
                tlv_TicketDetails.BeginUpdate()
                tlv_TicketDetails.SuspendLayout()

                Dim colList() As String = {"Category", "Value", "Trigger"}
                tlv_TicketDetails.Columns.Clear()
                For i As Integer = 0 To colList.Length - 1
                    Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
                    col1.Caption = colList(i)
                    col1.VisibleIndex = i
                    If colList(i) = "Category" Then
                        tlv_TicketDetails.AutoFillColumn = col1
                    ElseIf colList(i) = "Value" Then
                        col1.Width = 50
                    End If
                    tlv_TicketDetails.Columns.Add(col1)
                Next

                tlv_TicketDetails.Nodes.Clear()
                Dim nodeCategory As TreeListNode = Nothing

                Dim dvObject As DataView = New DataView(dsTicketRules.Tables(0))
                Dim cols(0) As String
                cols(0) = "CAT_Name"
                Dim dtCategory As DataTable = dvObject.ToTable(True, cols)

                For Each drCat As DataRow In dtCategory.Rows

                    nodeCategory = tlv_TicketDetails.Nodes.Add(New Object() {drCat("CAT_Name").ToString, "", ""})
                    nodeCategory.Tag = drCat("CAT_Name").ToString

                    Dim colRule(0) As String
                    colRule(0) = "RuleName"
                    'colRule(1) = "RuleValue"

                    Dim dtRule As DataTable = dsTicketRules.Tables(0).Select("CAT_Name = " & Chr(39) & drCat("CAT_Name").ToString & Chr(39)).CopyToDataTable.DefaultView.ToTable(True, colRule)

                    For Each drRule As DataRow In dtRule.Rows

                        Dim ruleValue As String = dsTicketRules.Tables(0).Select("CAT_Name = " & Chr(39) & drCat("CAT_Name").ToString & Chr(39) & " And RuleName = " & Chr(39) & drRule("RuleName").ToString & Chr(39))(0)("RuleValue").ToString
                        Dim nodeRule As TreeListNode = tlv_TicketDetails.AppendNode(New Object() {drRule("RuleName").ToString.Trim, ruleValue, ""}, nodeCategory)
                        nodeRule.Tag = drRule("RuleName").ToString.Trim

                        Dim colKPI(2) As String
                        colKPI(0) = "KPI_Name"
                        colKPI(1) = "Operator"
                        colKPI(2) = "TriggerValue"

                        Dim dtKPI As DataTable = dsTicketRules.Tables(0).Select("CAT_Name = " & Chr(39) & drCat("CAT_Name").ToString & Chr(39) & " And RuleName = " & Chr(39) & drRule("RuleName").ToString & Chr(39)).CopyToDataTable.DefaultView.ToTable(True, colKPI)
                        For Each drKPI As DataRow In dtKPI.Rows
                            If (Not String.IsNullOrEmpty(drKPI("KPI_Name").ToString.Trim)) Then

                                Dim nodeKPI As TreeListNode = tlv_TicketDetails.AppendNode(New Object() {drKPI("KPI_Name").ToString.Trim, "", drKPI("Operator").ToString.Trim & " " & drKPI("TriggerValue").ToString.Trim}, nodeRule)
                                nodeKPI.Tag = drKPI("KPI_Name").ToString.Trim

                            End If
                        Next
                    Next
                Next

                tlv_TicketDetails.ResumeLayout()
                tlv_TicketDetails.Nodes(0).ExpandAll()
                tlv_TicketDetails.EndUpdate()
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                If Not dsTicketRules Is Nothing Then
                    dsTicketRules.Dispose()
                    dsTicketRules = Nothing
                End If
            End Try

            'updating cell history
            '-----------------------
            Dim dsCellHistory As DataSet = New System.Data.DataSet
            Try
                Dim drNCell As DataRow = gvIOS_Tickets.GetFocusedDataRow()
                Dim siteColIndex = drNCell.Table.Columns("SITE").Ordinal
                Dim techColIndex = drNCell.Table.Columns("TECH").Ordinal

                Dim ch As Chart = CType(xTabPage_CellHistory.Controls(0), Chart)

                If siteColIndex <> 0 Then
                    ch.TitleBox.Label.Text = "Objects: " & drNCell(siteColIndex).ToString
                    ch.TitleBox.HeaderLabel.Text = "Site Score - History"
                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                    ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(dr(techColIndex).ToString))
                    ch.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    'ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                    'ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default


                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                    ch.XAxis.TimeInterval = TimeInterval.Days
                    ch.XAxis.FormatString = "dd/MM/yy"
                    ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                    ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"



                    'ch.XAxis.StaticColumnWidth = 10
                    ' ch.XAxis.SpacingPercentage = 40
                    ch.Dock = DockStyle.Fill
                    Dim parray2()() As String = {
                        New String() {"@Cell", Chr(39) & dr(siteColIndex).ToString & Chr(39)},
                        New String() {"@Tech", Chr(39) & dr(techColIndex).ToString & Chr(39)}
                    }

                    dsCellHistory = DataAccessorODBC.GetDataSet(GetSQL(8005, parray2, dt_IOS_SQL)(0), GetSQL(8005, parray2, dt_IOS_SQL)(1))

                    If dsCellHistory Is Nothing Then
                        dsCellHistory.Dispose()
                        Exit Sub
                    End If
                    If dsCellHistory.Tables.Count = 0 Then
                        dsCellHistory.Dispose()
                        dsCellHistory = Nothing
                        Exit Sub
                    End If

                    Dim chart_elements() As String = Nothing
                    techColIndex = 0
                    For Each dcol As DataColumn In dsCellHistory.Tables(0).Columns
                        If dcol.ColumnName.ToUpper.Trim <> "DATE" Then
                            ReDim Preserve chart_elements(techColIndex)
                            chart_elements(techColIndex) = dcol.ColumnName
                            techColIndex += 1
                        End If
                    Next

                    Dim de As DataEngine = New DataEngine(dsCellHistory.Tables(0))
                    de.DataFields = String2DataFields(chart_elements, "Date")
                    de.DataGridFormatString = "N2"

                    Dim sc As New SeriesCollection
                    sc = de.GetSeries()

                    Dim i As Integer = 0
                    For i = 0 To sc.Count() - 1
                        sc(i).Type = SeriesType.Bar
                        sc(i).Line.Width = 3
                        sc(i).EmptyElement.Mode = EmptyElementMode.TreatAsZero
                    Next
                    sc(0).DefaultElement.Color = Color.FromArgb(49, 255, 49)
                    sc(1).DefaultElement.Color = Color.FromArgb(255, 255, 0)
                    sc(2).DefaultElement.Color = Color.FromArgb(255, 99, 49)
                    sc(3).DefaultElement.Color = Color.FromArgb(0, 156, 255)
                    sc(4).DefaultElement.Color = Color.Magenta

                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    dsCellHistory.Dispose()
                    dsCellHistory = Nothing
                End If

                ch.RefreshChart()
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                dsCellHistory.Dispose()
                dsCellHistory = Nothing
            End Try

            'Update Chart - Ticket Score
            '---------------------------
            Dim dsTicketScore As DataSet = New System.Data.DataSet

            Try
                Dim chart1 As Chart = CType(gc_TicketScore.Controls(0), Chart)
                chart1.LegendBox.Orientation = Orientation.Bottom
                chart1.SeriesCollection.Clear()
                Dim parray3()() As String = {New String() {"@TicketID", ticket_selected}}
                dsTicketScore = DataAccessorODBC.GetDataSet(GetSQL(8006, parray3, dt_IOS_SQL)(0), GetSQL(8006, parray3, dt_IOS_SQL)(1))

                If dsTicketScore.Tables.Count = 0 Then
                    dsTicketScore.Dispose()
                    dsTicketScore = Nothing
                    Exit Sub
                End If

                Dim SC As New SeriesCollection()
                For Each dcol As DataColumn In dsTicketScore.Tables(0).Columns
                    Dim s As New Series()
                    s.Name = dcol.ColumnName
                    Dim el As New Element()
                    el.YValue = dsTicketScore.Tables(0)(0)(dcol.Ordinal)
                    s.Elements.Add(el)
                    SC.Add(s)
                Next

                'Set Different Colors for our Series
                SC(0).DefaultElement.Color = Color.FromArgb(49, 255, 49)
                SC(1).DefaultElement.Color = Color.FromArgb(255, 255, 0)
                SC(2).DefaultElement.Color = Color.FromArgb(255, 99, 49)
                SC(3).DefaultElement.Color = Color.FromArgb(0, 156, 255)
                SC(4).DefaultElement.Color = Color.Magenta

                chart1.ChartArea.Background.Color = Me.BackColor
                chart1.SeriesCollection.Add(SC)
                chart1.RefreshChart()

                dsTicketScore.Dispose()
                dsTicketScore = Nothing
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                dsTicketScore.Dispose()
                dsTicketScore = Nothing
            End Try

            'Update Chart - Ticket Root
            '---------------------------
            Try
                Dim chart1 As Chart = CType(gc_TicketRoot.Controls(0), Chart)
                chart1.LegendBox.Visible = False
                chart1.SeriesCollection.Clear()
                chart1.YAxis.ScaleRange = New ScaleRange(0, 100)
                chart1.DefaultSeries.GaugeType = GaugeType.Bars
                chart1.Margin = "0"

                Dim foundRows() As DataRow
                foundRows = dsTickets.Tables(0).Select("TicketID = " & ticket_selected)

                Dim totalsum As Integer = 0
                For k As Integer = 0 To foundRows(0).Table.Columns.Count - 1
                    If foundRows(0).Table.Columns(k).ColumnName.Contains("%") Then
                        If IsNumeric(foundRows(0)(k).ToString) Then
                            totalsum = totalsum + CInt(foundRows(0)(k).ToString)
                        End If
                    End If
                Next

                Dim SC As New SeriesCollection()
                Dim s As New Series()
                For Each dcol As DataColumn In dsTickets.Tables(0).Columns
                    If dcol.ColumnName.Contains("%") Then
                        Dim el As New Element()
                        el.Name = dcol.ColumnName
                        el.YValue = Math.Round(foundRows(0).Item(dcol.Ordinal) / totalsum * 100, 0)
                        s.Elements.Add(el)
                    End If
                Next
                SC.Add(s)

                'Set Different Colors for our Series
                SC(0).Elements(0).Color = Color.FromArgb(CLng(26367) Mod 256, (CLng(26367) \ 256) Mod 256, ((CLng(26367) \ 256) \ 256) Mod 256)
                SC(0).Elements(1).Color = Color.FromArgb(CLng(13209) Mod 256, (CLng(13209) \ 256) Mod 256, ((CLng(13209) \ 256) \ 256) Mod 256)
                SC(0).Elements(2).Color = Color.FromArgb(CLng(6684774) Mod 256, (CLng(6684774) \ 256) Mod 256, ((CLng(6684774) \ 256) \ 256) Mod 256)
                SC(0).Elements(3).Color = Color.FromArgb(CLng(13395456) Mod 256, (CLng(13395456) \ 256) Mod 256, ((CLng(13395456) \ 256) \ 256) Mod 256)
                SC(0).Elements(4).Color = Color.FromArgb(CLng(65280) Mod 256, (CLng(65280) \ 256) Mod 256, ((CLng(65280) \ 256) \ 256) Mod 256)

                chart1.ChartArea.Background.Color = Me.BackColor
                chart1.SeriesCollection.Add(SC)
                chart1.RefreshChart()

            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            End Try
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gvEvents_RowCellStyle(sender As Object, e As Views.Grid.RowCellStyleEventArgs)
        If e.Column.FieldName = "EventStatus" Then
            If e.CellValue IsNot Nothing Then
                If e.CellValue.ToString.ToUpper = "ACCEPTED" Or e.CellValue.ToString.ToUpper = "OK" Or e.CellValue.ToString.ToUpper = "END" Then
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf e.CellValue.ToString.ToUpper = "ISSUE" Then
                    e.Appearance.BackColor = Color.OrangeRed
                ElseIf e.CellValue.ToString.ToUpper = "NEW" Then
                    e.Appearance.BackColor = Color.Orange
                End If
            End If
            e.Appearance.ForeColor = Color.Black
        End If
    End Sub

    Private Sub xtcMain_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs)
        Try
            If e.Page.TabIndex = 0 Then
                xTabControl_Tickets.Enabled = True
                gc_IssueStatistics.Enabled = True
                gc_TicketScore.Enabled = True
                gc_TicketRoot.Enabled = True
            ElseIf e.Page.TabIndex = 1 Then
                xTabControl_Tickets.Enabled = False
                gc_IssueStatistics.Enabled = False
                gc_TicketScore.Enabled = False
                gc_TicketRoot.Enabled = False
            ElseIf e.Page.TabIndex = 2 Then
                'LoadModuleDashboards()
            End If
        Catch
        End Try
    End Sub

    Private Sub ReportViewer_DashboardItemControlCreated(ByVal sender As Object, ByVal e As DevExpress.DashboardWin.DashboardItemControlEventArgs) Handles ReportViewer.DashboardItemControlCreated
        If e.GridControl IsNot Nothing Then
            dbGrids(e.DashboardItemName) = e.GridControl
        End If
    End Sub

    Private Sub ReportViewer_DashboardItemClick(sender As Object, e As DevExpress.DashboardWin.DashboardItemMouseActionEventArgs) 'Handles ReportViewer.DashboardItemClick
        Try
            'Dim ticketID As String = Nothing
            'Dim cellName As String = Nothing
            'Dim tech As String = Nothing

            'For Each axis As String In e.Data.GetAxisNames()
            '    Dim axisPoint As AxisPoint = e.GetAxisPoint(axis)
            '    If axisPoint Is Nothing Then
            '        Continue For
            '    End If

            '    For Each dimension In e.Data.GetDimensions(axis)
            '        Dim dimValue As DimensionValue = axisPoint.GetDimensionValue(dimension)
            '        If dimValue Is Nothing Then
            '            Continue For
            '        End If

            '        If dimension.Name.ToLower = "ticketid" Then
            '            ticketID = dimValue.DisplayText
            '        End If

            '        If dimension.Name.ToLower = "site" Then
            '            cellName = dimValue.DisplayText
            '        End If

            '        If dimension.Name.ToLower = "tech" Then
            '            tech = dimValue.DisplayText
            '        End If

            '    Next dimension
            'Next axis

            'ReportViewer.Dashboard.BeginUpdate()

            'ReportViewer.Parameters(0).SelectedValue = cellName
            'ReportViewer.Parameters(1).SelectedValue = tech

            'ReportViewer.Dashboard.EndUpdate()

            '*****************************************************************************************************'
            'Dim grid As GridControl = Nothing
            'If grids.TryGetValue(e.DashboardItemName, grid) Then
            '    Dim values = New List(Of String)()
            '    For i As Integer = 0 To grid.DefaultView.RowCount - 1
            '        Dim point As AxisPoint = TryCast(grid.DefaultView.GetRow(i), AxisPoint)
            '        values.Add(point.DimensionValue.DisplayText)
            '    Next i
            '    Debug.WriteLine(String.Join(", ", values))
            'End If
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub timerCountdown_Tick(sender As Object, e As EventArgs) Handles timerCountdown.Tick
    '    If timeMonitor.Hour + timeMonitor.Minute + timeMonitor.Second > 0 Then
    '        timeMonitor = DateAdd(DateInterval.Second, -1, timeMonitor)
    '    Else
    '        Application.DoEvents()
    '        'Dashboard_Events_Load()
    '        timeMonitor = TimeSerial(0, 10, 0)
    '    End If
    'End Sub

    Private Sub gvEvents_ColumnPositionChanged(sender As Object, e As EventArgs)
        Try
            Dim col As Columns.GridColumn = TryCast(sender, Columns.GridColumn)
            Dim gv As Views.Grid.GridView = DirectCast(col.View, Views.Grid.GridView)
            ManageGridColumnsPosition(gv)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gvEvents_ColumnWidthChanged(sender As Object, e As Views.Base.ColumnEventArgs)
        Try
            Dim gv As Views.Grid.GridView = DirectCast(sender, Views.Grid.GridView)
            ManageGridColumnsWidth(gv, e.Column)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gvEvents_ColumnFilterChanged(sender As Object, e As EventArgs)
        Try
            Dim gv As Views.Grid.GridView = DirectCast(sender, Views.Grid.GridView)
            For iCntr As Integer = 0 To gv.Columns.Count - 1
                Dim filterCol As Columns.GridColumn = gv.Columns(iCntr)
                ManageGridColumnsFilter(gv, filterCol)
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRefreshDashboard_Click(sender As Object, e As EventArgs) Handles btnRefreshDashboard.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frm_Dashboard_Load(Nothing, Nothing)
        Catch

        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_ManualUpdateStatus_Click(sender As Object, e As EventArgs) Handles tsmi_ManualUpdateStatus.Click
        Try
            If gvEvents.RowCount > 0 Then
                Dim objEMUS As New dlgEventManualUpdateStatus()
                objEMUS.eventID = CInt(gvEvents.GetFocusedRowCellValue("EventID"))
                objEMUS.eventName = CStr(gvEvents.GetFocusedRowCellValue("EventName"))
                objEMUS.eventStatus = CStr(gvEvents.GetFocusedRowCellValue("EventStatus"))
                objEMUS.ShowDialog()

                If objEMUS.DialogResult = DialogResult.OK Then
                    Dashboard_Events_Load()
                    gvEvents.FocusedRowHandle = gvEvents.LocateByValue("EventID", objEMUS.eventID)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally

        End Try
    End Sub

    Private Sub ReportViewer_PopupMenuShowing(sender As Object, e As DevExpress.DashboardWin.DashboardPopupMenuShowingEventArgs) Handles ReportViewer.PopupMenuShowing
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Clipboard.Clear()
            'Dim copyRow As String = ""

            For Each axis As String In e.Data.GetAxisNames()
                Dim axisPoint As AxisPoint = e.GetAxisPoint(axis)
                If axisPoint Is Nothing Then
                    Continue For
                End If

                For Each dimension In e.Data.GetDimensions(axis)
                    Dim dimValue As DimensionValue = axisPoint.GetDimensionValue(dimension)
                    If dimValue Is Nothing Then
                        Continue For
                    End If

                    If dimension.Name.ToLower = "objecttype" Then
                        ObjectType = dimValue.DisplayText
                    End If

                    If dimension.Name.ToLower = "cellname" Then
                        CellName = dimValue.DisplayText
                    End If

                    If dimension.Name.ToLower = "ios_tech" Then
                        IOS_Tech = dimValue.DisplayText
                    End If

                    'copyRow = copyRow & dimValue.DisplayText & ","
                Next dimension
            Next axis

            'add selected row data into clipboard
            'copyRow = copyRow.TrimEnd(",")
            'Clipboard.SetText(copyRow)

            If resultDS.Tables.Count = 0 Then
                Dim dashboardDS As DashboardSqlDataSource = TryCast(ReportViewer.Dashboard.DataSources(0), DashboardSqlDataSource)
                Dim dsXML As XElement = dashboardDS.SaveToXml()

                If dashboardDS.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then
                    Dim sqlDS As SqlDataSource = New SqlDataSource()
                    sqlDS.LoadFromXml(dsXML)

                    sqlDS.ConnectionParameters = CreateConnectionParameters()
                    sqlDS.Fill()

                    resultDS.DataSetName = dashboardDS.ComponentName
                    Dim rSet As ResultSet = TryCast(TryCast(sqlDS, IListSource).GetList(), ResultSet)
                    For Each table As ResultTable In rSet.Tables
                        Dim dt As DataTable = New DataTable(table.TableName)
                        table.Columns.ForEach(Sub(col) dt.Columns.Add(New DataColumn(col.Name, col.PropertyType)))
                        resultDS.Tables.Add(dt)
                        For Each row In table
                            Dim newRow As DataRow = dt.NewRow()
                            For Each column In table.Columns
                                newRow(column.Name) = column.GetValue(row)
                            Next
                            dt.Rows.Add(newRow)
                        Next
                    Next
                End If
            End If

            dbFilters.Clear()
            Dim filterString As String = Nothing
            For Each dbItem As DashboardItem In ReportViewer.Dashboard.Items
                If dbItem.ComponentName = e.DashboardItemName Then
                    Dim actFilter = DirectCast(dbGrids(e.DashboardItemName).DefaultView, DevExpress.XtraGrid.Views.Base.ColumnView).ActiveFilter
                    For iCntr As Integer = 0 To actFilter.Count - 1
                        Dim colCaption = actFilter(iCntr).Column.Caption
                        Dim operand = DirectCast(DirectCast(actFilter(iCntr).Filter.FilterCriteria, DevExpress.Data.Filtering.BinaryOperator).LeftOperand, DevExpress.Data.Filtering.OperandProperty).PropertyName
                        If filterString Is Nothing Then
                            filterString = DirectCast(dbGrids(e.DashboardItemName).DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).ActiveFilterString.Replace(operand, colCaption)
                        Else
                            filterString = filterString.Replace(operand, colCaption)
                        End If
                    Next
                    dbFilters.Add(DirectCast(dbItem, DataDashboardItem).DataMember, filterString)
                End If
            Next

            'adding context menu items into the existing context menu of the gridview of the report viewer
            Dim dr() As DataRow = dtDBReports.Select("DashboardID=" & CInt(TryCast(cmbDashboards.SelectedItem, clsComboBoxItem).Value))
            If Not IsDBNull(dr(0)("SourceField")) AndAlso Not IsDBNull(dr(0)("MapField")) AndAlso Not IsDBNull(dr(0)("InfoTip")) Then
                If resultDS.Tables.Count <> 0 AndAlso dbFilters.Count <> 0 Then
                    For Each dt As DataTable In resultDS.Tables
                        If dbFilters.ContainsKey(dt.TableName) Then
                            'Dim dtTemp As DataTable = dt.Copy()
                            If dt.Columns.Contains(dr(0)("SourceField")) AndAlso dt.Columns.Contains(dr(0)("MapField")) AndAlso dt.Columns.Contains(dr(0)("InfoTip")) Then

                                Dim bi As New BarButtonItem()
                                bi.Caption = "Map Selected Cell"
                                bi.Name = "barbtnItemTicketMapCell"
                                AddHandler bi.ItemClick, AddressOf barbtnItemTicketMapCell_ItemClick
                                e.Menu.AddItem(bi)

                                e.Menu.ItemLinks(6).BeginGroup = True

                                'checking if the grid has applied filters
                                If dbFilters(dt.TableName) IsNot Nothing Then
                                    Dim bi2 As New BarButtonItem()
                                    bi2.Caption = "Map Filtered Cells"
                                    bi2.Name = "barbtnItemMapFilteredCell"
                                    AddHandler bi2.ItemClick, AddressOf barbtnItemMapFilteredCell_ItemClick
                                    e.Menu.AddItem(bi2)
                                End If

                                Dim bi3 As New BarButtonItem()
                                bi3.Caption = "Map All Cells"
                                bi3.Name = "barbtnItemMapAllCell"
                                AddHandler bi3.ItemClick, AddressOf barbtnItemMapAllCell_ItemClick
                                e.Menu.AddItem(bi3)

                            End If
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub barbtnItemTicketMapCell_ItemClick(sender As Object, e As ItemClickEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtSelected As DataTable = Nothing
            Dim dr() As DataRow = dtDBReports.Select("DashboardID=" & CInt(TryCast(cmbDashboards.SelectedItem, clsComboBoxItem).Value))
            If resultDS.Tables.Count <> 0 AndAlso dbFilters.Count <> 0 Then
                For Each dt As DataTable In resultDS.Tables
                    If dbFilters.ContainsKey(dt.TableName) Then
                        dtSelected = dt.Copy().Select(dr(0)("SourceField") & "='" & CellName & "'").CopyToDataTable
                        frmMapWindow.MapDataToSingleLayer(dtSelected, "Dashboard_" & cmbDashboards.SelectedItem.ToString, dr(0)("SourceField"), dr(0)("MapField"), "Individual Theme", dtSelected.Columns(0).ColumnName, dr(0)("InfoTip"))
                    End If
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub barbtnItemMapFilteredCell_ItemClick(sender As Object, e As ItemClickEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtFiltered As DataTable = Nothing
            If resultDS.Tables.Count <> 0 AndAlso dbFilters.Count <> 0 Then
                For Each dt As DataTable In resultDS.Tables
                    If dbFilters.ContainsKey(dt.TableName) Then
                        Dim strFilter As String = dbFilters(dt.TableName)
                        dtFiltered = dt.Select(strFilter).CopyToDataTable
                        Dim dr() As DataRow = dtDBReports.Select("DashboardID=" & CInt(TryCast(cmbDashboards.SelectedItem, clsComboBoxItem).Value))
                        frmMapWindow.MapDataToSingleLayer(dtFiltered, "Dashboard_" & cmbDashboards.SelectedItem.ToString, dr(0)("SourceField"), dr(0)("MapField"), "Individual Theme", dtFiltered.Columns(0).ColumnName, dr(0)("InfoTip"))
                    End If
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub barbtnItemMapAllCell_ItemClick(sender As Object, e As ItemClickEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtAll As DataTable = Nothing
            If resultDS.Tables.Count <> 0 Then
                For Each dt As DataTable In resultDS.Tables
                    If dbFilters.ContainsKey(dt.TableName) Then
                        dtAll = dt.Copy()
                        Dim dr() As DataRow = dtDBReports.Select("DashboardID=" & CInt(TryCast(cmbDashboards.SelectedItem, clsComboBoxItem).Value))
                        frmMapWindow.MapDataToSingleLayer(dtAll, "Dashboard_" & cmbDashboards.SelectedItem.ToString, dr(0)("SourceField"), dr(0)("MapField"), "Individual Theme", dtAll.Columns(0).ColumnName, dr(0)("InfoTip"))
                    End If
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Events - Infinite Scrolling"

    Private dtEvents As New DataTable
    Private objLock As New Object
    Private queryOffset As Integer = 0
    Private batchSize As Integer = 1000
    Private isFirstTimeLoading As Boolean = False
    Private currViewRowFilter As String = ""
    Private currViewSortStr As String = ""
    Private datetimeEdit As RepositoryItemDateEdit
    Private _virtualServerModeSource As VirtualServerModeSource

    Private Function CreateData(ByVal offset As Integer, ByVal batchSize As Integer, Optional currentRowFilter As String = Nothing, Optional sortExpression As String = Nothing) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim columnList As String = Nothing
        Dim filterQry As String = Nothing

        If currentRowFilter IsNot Nothing AndAlso currentRowFilter <> "" Then
            'currentRowFilter = currentRowFilter.Replace("[", "x.[")
            'If currentRowFilter.Contains("#") Then
            '    filterQry = IIf(filterQry Is Nothing, " AND ", " ") & currentRowFilter.Replace("#", "''")
            'Else
            filterQry = IIf(filterQry Is Nothing, " AND ", " ") & currentRowFilter.Replace("'", "''")
            'End If
        End If

        Dim dt As DataTable = Nothing
        Dim parray()() As String = {
            New String() {"@n", offset},
            New String() {"@m", batchSize},
            New String() {"@filter", Chr(39) & filterQry & Chr(39)},
            New String() {"@sortExpr", Chr(39) & sortExpression & Chr(39)}
        }
        strConnection = GetSQL(8107, parray)(0)
        sqlParam = GetSQL(8107, parray)(1)
        dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Return dt
    End Function

    Private Sub Dashboard_Events_Load()
        Try
            dtEvents = Nothing
            queryOffset = 0
            isFirstTimeLoading = True
            currViewRowFilter = ""
            currViewSortStr = ""

            If (_virtualServerModeSource IsNot Nothing) Then
                RemoveHandler _virtualServerModeSource.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
                RemoveHandler _virtualServerModeSource.ConfigurationChanged, AddressOf virtualServerModeSource_ConfigurationChanged
                RemoveHandler _virtualServerModeSource.MoreRows, AddressOf VirtualServerModeSource_MoreRows
                RemoveHandler _virtualServerModeSource.GetUniqueValues, AddressOf virtualServerModeSource_GetUniqueValues
            End If

            _virtualServerModeSource = New VirtualServerModeSource()

            AddHandler _virtualServerModeSource.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
            AddHandler _virtualServerModeSource.ConfigurationChanged, AddressOf virtualServerModeSource_ConfigurationChanged
            AddHandler _virtualServerModeSource.MoreRows, AddressOf VirtualServerModeSource_MoreRows
            AddHandler _virtualServerModeSource.GetUniqueValues, AddressOf virtualServerModeSource_GetUniqueValues

            gcEvents.DataSource = Nothing
            gvEvents.OptionsView.ColumnAutoWidth = False
            gvEvents.Columns.Clear()
            gcEvents.DataSource = _virtualServerModeSource

            If dtEvents IsNot Nothing Then
                For Each dtCol As DataColumn In dtEvents.Columns
                    If dtCol.DataType = GetType(DateTime) Then
                        gvEvents.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        If regionalSettings = False Then
                            gvEvents.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
                        Else
                            gvEvents.Columns(dtCol.ColumnName).DisplayFormat.FormatString = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                        End If
                    End If
                    gvEvents.Columns(dtCol.ColumnName).BestFit()
                Next
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub VirtualServerModeSource_AcquireInnerList(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeAcquireInnerListEventArgs)
        Try
            Dim dtTempColumn As New DataTable
            If dtEvents Is Nothing Then
                dtTempColumn = CreateData(0, 1)
            End If

            e.InnerList = dtTempColumn.DefaultView
            e.AddMoreRowsFunc = AddressOf AddMoreRows
            e.ClearAndAddRowsFunc = AddressOf ClearAndAddMoreRows
            e.ReleaseAction = AddressOf ReleaseList
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ReleaseList(ByVal list As IList)
        TryCast(list, DataView).Table.Rows.Clear()
    End Sub

    Public Function AddMoreRows(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStr
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ClearAndAddMoreRows(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            TryCast(list, DataView).Table.Rows.Clear()
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStr
            Return list
        Catch ex As Exception
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Function

    Private Sub VirtualServerModeSource_MoreRows(sender As Object, e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            If isFirstTimeLoading Then
                gvEvents.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Indicator
            Else
                gvEvents.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Panel
            End If

            e.RowsTask = Task.Factory.StartNew(
              Function()
                  SyncLock objLock
                      Try
                          Dim dtData As New DataTable
                          If e.UserData Is Nothing Then
                              If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                                  dtData = CreateData(queryOffset, batchSize, currViewRowFilter, currViewSortStr)
                              Else
                                  dtData = CreateData(queryOffset, batchSize, currViewRowFilter)
                              End If
                          Else
                              dtData = CType(e.UserData, DataView).ToTable()
                          End If

                          Dim moreRows As Boolean = True
                          Dim rowCount As Integer = e.CurrentRowCount

                          If dtEvents IsNot Nothing Then
                              dtEvents.Merge(dtData)
                          Else
                              dtEvents = dtData
                          End If
                          queryOffset = dtEvents.Rows.Count
                          Dim nextBatch = dtEvents.Clone()

                          Do While nextBatch.Rows.Count < dtData.Rows.Count
                              nextBatch.ImportRow(dtEvents.Rows(rowCount))
                              rowCount += 1
                          Loop

                          moreRows = e.CurrentRowCount + batchSize <= rowCount
                          Return New VirtualServerModeRowsTaskResult(nextBatch.DefaultView, moreRows, Nothing)

                      Catch
                          Dim dt As New DataTable
                          Return New VirtualServerModeRowsTaskResult(dt.DefaultView, False, Nothing)
                      End Try
                  End SyncLock
              End Function, e.CancellationToken)
            If isFirstTimeLoading Then
                isFirstTimeLoading = False
                e.RowsTask.Wait(e.CancellationToken)
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub virtualServerModeSource_ConfigurationChanged(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            queryOffset = 0
            dtEvents = Nothing

            currViewRowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(e.ConfigurationInfo.Filter)
            If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                currViewSortStr = e.ConfigurationInfo.SortInfo(0).ToString()
            End If

            Dim dtData As New DataTable
            dtData = CreateData(queryOffset, batchSize, currViewRowFilter, currViewSortStr)
            e.UserData = dtData.DefaultView
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub virtualServerModeSource_GetUniqueValues(ByVal sender As Object, ByVal e As VirtualServerModeGetUniqueValuesEventArgs)
        e.UniqueValuesTask =
            New System.Threading.Tasks.Task(Of Object())(
            Function()
                Dim dr As DataRow = gvEvents.GetFocusedDataRow()
                If dr IsNot Nothing Then
                    Dim dt As New DataTable
                    Dim strSql As String = Nothing
                    Dim strPreFilter As String = Nothing
                    If currViewRowFilter Is Nothing Or currViewRowFilter = "" Then
                        strSql = "EXEC [dbo].[sp_Get_Dashbaord_Events_DistColumn] " & "'[" & e.ValuesPropertyName & "]'"
                    Else
                        strPreFilter = currViewRowFilter.Replace("'", "''") '.Replace("#", "''")
                        strSql = "EXEC [dbo].[sp_Get_Dashbaord_Events_DistColumn] " & "'[" & e.ValuesPropertyName & "]','" & strPreFilter & "'"
                    End If
                    dt = DataAccessorODBC.GetDataTable(connStrIOSServer, strSql)
                    Dim filterValue() As Object = Nothing
                    If dt IsNot Nothing Then
                        filterValue = dt.Rows.OfType(Of DataRow)().Select(Function(x) x.Item(0)).ToArray()
                    End If
                    Return filterValue
                Else
                    Return Nothing
                End If
                Return Nothing
            End Function, e.CancellationToken)
    End Sub

#End Region

#Region "Monitor Mode"

    Private Sub rbManual_CheckedChanged(sender As Object, e As EventArgs) Handles rbManual.CheckedChanged
        Try
            If rbManual.Checked = True Then
                timeMonitor = TimeSerial(0, 0, 0)
                timerCountdown.Stop()
                timerCountdown.Enabled = False
                lblTimer.Text = "Not Set"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rbAutomatic_CheckedChanged(sender As Object, e As EventArgs) Handles rbAutomatic.CheckedChanged
        Try
            If rbAutomatic.Checked = True Then
                rbManual.Checked = False
                timerCountdown.Enabled = True
                timeMonitor = TimeSerial(0, CInt(SpinEditMonMode.Value), 0)
                timerCountdown.Start()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub timerCountdown_Tick(sender As Object, e As EventArgs) Handles timerCountdown.Tick
        Try
            If timeMonitor.Hour + timeMonitor.Minute + timeMonitor.Second > 0 Then
                timeMonitor = DateAdd(DateInterval.Second, -1, timeMonitor)
                lblTimer.Text = timeMonitor.ToString("HH:mm:ss")
            Else
                timeMonitor = TimeSerial(0, CInt(SpinEditMonMode.Value), 0)
                lblTimer.Text = "REFRESHING"
                Application.DoEvents()
                LoadDashboardReport()
            End If
        Catch
        End Try
    End Sub

#End Region

End Class