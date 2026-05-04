Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmNBIReports

#Region "Variables"

    Dim parray()() As String = Nothing
    Dim ConnStringAndSqlParam() As String = Nothing

    Dim dtNBIReports As DataTable = Nothing
    Dim dtPredefPeriod As DataTable = Nothing

    Dim updateColumnName As String = Nothing

#End Region

#Region "Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub LoadNBIReports()
        RemoveHandler gvReportsList.FocusedRowChanged, AddressOf gvReportsList_FocusedRowChanged

        parray = Nothing
        ConnStringAndSqlParam = Nothing
        ConnStringAndSqlParam = GetSQL(8521, parray)

        dtNBIReports = New DataTable()
        dtNBIReports = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        Dim columnsToHide() As String = {"ReportedPeriodPredefined", "ReportedPeriodStartTime", "ReportedPeriodEndTime", "ReportedObjectAggregation", "ReportedTimeAggregation", "ReportedObjectFilterID", "ReportDescription",
                                         "IsOutputPivotted", "OutputFormat", "OutputDelimiter", "ScheduleStartTime", "ScheduleInterval", "ReportLastRunTime", "ReportOwner", "IsLocked", "SQLQuery", "OutputFolder", "EmailLinkNBI"}
        IOSDevExpressGrid.PopulateDataInGrid(gcReportsList, gvReportsList, dtNBIReports, "ALL", columnsToHide, "ReportName")

        Dim riChkIsScheduled As RepositoryItemCheckEdit = TryCast(gcReportsList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkIsScheduled.CheckStyle = CheckStyles.Standard
        riChkIsScheduled.AllowGrayed = False
        riChkIsScheduled.NullStyle = StyleIndeterminate.Unchecked
        gvReportsList.Columns("IsScheduled").ColumnEdit = riChkIsScheduled
        AddHandler riChkIsScheduled.CheckedChanged, AddressOf riCheckEditColumn_CheckedChanged

        Dim riChkIsEnabled As RepositoryItemCheckEdit = TryCast(gcReportsList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkIsEnabled.CheckStyle = CheckStyles.Standard
        riChkIsEnabled.AllowGrayed = False
        riChkIsEnabled.NullStyle = StyleIndeterminate.Unchecked
        gvReportsList.Columns("IsEnabled").ColumnEdit = riChkIsEnabled
        AddHandler riChkIsEnabled.CheckedChanged, AddressOf riCheckEditColumn_CheckedChanged

        Dim riChkEmailEnabled As RepositoryItemCheckEdit = TryCast(gcReportsList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkEmailEnabled.CheckStyle = CheckStyles.Standard
        riChkEmailEnabled.AllowGrayed = False
        riChkEmailEnabled.NullStyle = StyleIndeterminate.Unchecked
        gvReportsList.Columns("EmailEnabled").ColumnEdit = riChkEmailEnabled
        AddHandler riChkEmailEnabled.CheckedChanged, AddressOf riCheckEditColumn_CheckedChanged

        Dim riChkEmailLinkNBI As RepositoryItemCheckEdit = TryCast(gcReportsList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkEmailLinkNBI.CheckStyle = CheckStyles.Standard
        riChkEmailLinkNBI.AllowGrayed = False
        riChkEmailLinkNBI.NullStyle = StyleIndeterminate.Unchecked
        gvReportsList.Columns("EmailLinkNBI").ColumnEdit = riChkEmailLinkNBI
        AddHandler riChkEmailLinkNBI.CheckedChanged, AddressOf riCheckEditColumn_CheckedChanged

        AddHandler gvReportsList.FocusedRowChanged, AddressOf gvReportsList_FocusedRowChanged
        gvReportsList_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub BindObjectAggrCombo()
        If lblTechnology.Text <> "" AndAlso lblObjectType.Text <> "" Then
            parray = Nothing
            ConnStringAndSqlParam = Nothing
            parray = {
                New String() {"@Tech", Chr(39) & lblTechnology.Text.Trim & Chr(39)},
                New String() {"@ObjectType", Chr(39) & lblObjectType.Text.Trim & Chr(39)}
            }
            ConnStringAndSqlParam = GetSQL(8522, parray)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
            If dt IsNot Nothing Then
                BindDevExComboBoxWithValueMember(cmbObjectAggr, dt, "Aggregate_To", "Aggregate_To", "Select", True)
            End If
        End If
    End Sub

    Public Sub BindComboWithPredefinedPeriod()
        Try
            RemoveHandler cmbPredefTimeStats.SelectedIndexChanged, AddressOf cmbPredefTime_SelectedIndexChanged
            dtPredefPeriod = clsSQLCommands.GetPredefinedPeriodComboBoxNBIReports(connStrIOSServer)
            If dtPredefPeriod IsNot Nothing Then
                If dtPredefPeriod.AsEnumerable().Where(Function(x) x.Field(Of String)("Control") = "cmbPredefTimeStats").Count > 0 Then
                    BindDevExComboBoxWithTagMember(cmbPredefTimeStats, dtPredefPeriod.AsEnumerable().Where(Function(x) x.Field(Of String)("Control") = "cmbPredefTimeStats").CopyToDataTable(), "PredefinedPeriodID", "GUIText", "Select", "PredefinedPeriodID", True)
                End If
            End If
            AddHandler cmbPredefTimeStats.SelectedIndexChanged, AddressOf cmbPredefTime_SelectedIndexChanged
        Catch
        End Try
    End Sub

    Private Sub LoadKPIGrid(ByVal reportID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID}
        }
        ConnStringAndSqlParam = GetSQL(8524, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        IOSDevExpressGrid.PopulateDataInGrid(grdKPI, gvKPI, dt, "ALL", {"SQLKPI_ID"}, "KPI_Name")
    End Sub

    Private Sub LoadObjectFilterGrid(ByVal reportID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID}
        }
        ConnStringAndSqlParam = GetSQL(8525, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        IOSDevExpressGrid.PopulateDataInGrid(grdObjFilter, gvObjFilter, dt, "ALL", {"ReportFilterID"}, "FilterString")
    End Sub

    Private Sub DeleteNBIReport(reportID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID}
        }
        ConnStringAndSqlParam = GetSQL(8527, parray)
        DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
    End Sub

    Private Sub DeleteKPI(ByVal reportID As Integer, ByVal sqlKpiID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID},
            New String() {"@SQLKPIID", sqlKpiID}
        }
        ConnStringAndSqlParam = GetSQL(8529, parray)
        DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
    End Sub

    Private Sub DeleteObjFilter(ByVal filterID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportFilterID", filterID}
        }
        ConnStringAndSqlParam = GetSQL(8533, parray)
        DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
    End Sub

    Private Function LoadNBIReportData() As DataTable
        Dim reportID As Integer = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID},
            New String() {"@AdHocRun", 1},
            New String() {"@ToNBI", 2}
        }
        ConnStringAndSqlParam = GetSQL(8535, parray)
        Return DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1), CInt(IIf(txtTimeout.Text.Trim = "", 300, txtTimeout.Text.Trim)))
    End Function

    Private Sub LoadCountersList()
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@techName", Chr(39) & lblTechnology.Text.Trim & Chr(39)}
        }
        ConnStringAndSqlParam = GetSQL(8536, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        IOSDevExpressGrid.PopulateDataInGrid(gcColumns, gvColumns, dt, "ALL", Nothing, "ColumnName")
    End Sub

    Private Sub LoadKPIsList()
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@techName", Chr(39) & lblTechnology.Text.Trim & Chr(39)}
        }
        ConnStringAndSqlParam = GetSQL(8537, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        IOSDevExpressGrid.PopulateDataInGrid(gcKPIs, gvKPIs, dt, "ALL", {"KPI_SQL"}, "KPI_Name")
    End Sub

    Private Sub LoadTablesList()
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@techName", Chr(39) & lblTechnology.Text.Trim & Chr(39)}
        }
        ConnStringAndSqlParam = GetSQL(8538, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        IOSDevExpressGrid.PopulateDataInGrid(gcTables, gvTables, dt, "ALL", Nothing, "TableName")

    End Sub

    Private Sub LoadAliasesGrid(ByVal reportID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID}
        }
        ConnStringAndSqlParam = GetSQL(8539, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        IOSDevExpressGrid.PopulateDataInGrid(grdAliases, gvAliases, dt, "ALL", {"ReportAliasID"}, "KPI_Name")
    End Sub

    Private Sub DeleteAlias(reportAliasID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportAliasID", reportAliasID}
        }
        ConnStringAndSqlParam = GetSQL(8540, parray)
        DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
    End Sub

    Private Sub LoadOutputFolderTextBoxAutoFill(ByRef txtOF As TextEdit)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        ConnStringAndSqlParam = GetSQL(8544, parray)
        GetTextboxDataWithAutoCompleteFeature(txtOF, ConnStringAndSqlParam(1))
    End Sub

    Private Sub UpdateReportColumn(updateColumnValue As Boolean)
        parray = {
            New String() {"@ColumnName", updateColumnName},
            New String() {"@ColumnValue", IIf(updateColumnValue = True, 1, 0)},
            New String() {"@ReportID", gvReportsList.GetFocusedRowCellValue("ReportID")}
        }
        ConnStringAndSqlParam = GetSQL(8545, parray)
        DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
    End Sub

    Private Sub ChangeBtnSaveReportBGColor()
        If btnSaveReport.Enabled = True Then
            btnSaveReport.Appearance.BackColor = Color.Yellow
        Else
            btnSaveReport.Appearance.BackColor = Nothing
        End If
    End Sub

    Private Sub CopyNBIReport(reportID As Integer)
        parray = {
            New String() {"@ReportID", reportID},
            New String() {"@ReportOwner", Chr(39) & Environment.UserName.ToString & Chr(39)}
        }
        ConnStringAndSqlParam = GetSQL(8546, parray)
        DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
    End Sub

    Private Sub LoadReportStatus(ByVal reportID As Integer)
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID}
        }
        ConnStringAndSqlParam = GetSQL(8547, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        'IOSDevExpressGrid.PopulateDataInGrid(gcReportStatus, gvReportStatus, dt, "ALL", Nothing, "SQLQueryFired")
        SetHyperlinkColumnsInGridControl(gcReportStatus, gvReportStatus, dt)
        gvReportStatus.AutoFillColumn = gvReportStatus.Columns("SQLQueryFired")
        ' SetHyperlinkColumnsInGridControl(gcReportStatus, gvReportStatus, dt)
    End Sub

    Private Function GetReportData(reportID As Integer) As DataTable
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        parray = {
            New String() {"@ReportID", reportID}
        }
        ConnStringAndSqlParam = GetSQL(8549, parray)
        Return DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
    End Function

    Private Sub ConfigureNBIReportForm(frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)
            Dim modelControl As IOS.Configuration.EntityModel.Control = Nothing

            Dim formControls As List(Of Object) = New List(Of Object) From {
                btnAddReport
            }

            For Each frmControl As Object In formControls
                modelControl = form.FindControlByName(frmControl.Name)
                If Not modelControl Is Nothing Then
                    frmControl.Enabled = modelControl.DefaultEnable
                    frmControl.Visible = modelControl.DefaultVisible
                End If
            Next
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub frmNBIReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            BindComboWithPredefinedPeriod()
            LoadNBIReports()
            LoadOutputFolderTextBoxAutoFill(txtOutputFolder)
            ConfigureNBIReportForm("frmNBIReports")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
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

    Private Sub gvReportsList_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvReportsList.RowCount > 0 AndAlso e IsNot Nothing Then
                gvReportsList.ClearSelection()
                gvReportsList.FocusedRowHandle = e.FocusedRowHandle
                gvReportsList.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            Dim reportID As Integer = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))
            Dim dtList As DataTable = GetReportData(reportID)

            If dtList IsNot Nothing AndAlso dtList.Rows.Count > 0 Then
                Dim drList As DataRow = dtList.Rows(0)

                'reportID = CInt(drList("ReportID"))
                lblReportName.Text = CStr(drList("ReportName"))

                'load manual sql grids for the first time or when report technology changes
                If (lblTechnology.Text.Trim = "") OrElse (lblTechnology.Text.Trim.ToUpper <> CStr(drList("IOS_TECH")).Trim.ToUpper) Then
                    lblTechnology.Text = CStr(drList("IOS_TECH"))

                    LoadTablesList()
                    LoadCountersList()
                    LoadKPIsList()

                    sccManualSQL.Collapsed = False
                    sccManualSQL.PanelVisibility = SplitPanelVisibility.Both
                    sccManualSQL.SplitterPosition = sccManualSQL.Width - sccManualSQL.Panel2.MinSize
                End If

                If IsDBNull(drList("ObjectType")) = True Then
                    lblObjectType.Text = ""
                Else
                    lblObjectType.Text = CStr(drList("ObjectType"))
                End If

                BindObjectAggrCombo()

                lblReportOwner.Text = IIf(IsDBNull(drList("ReportOwner")), "", drList("ReportOwner"))

                RemoveHandler ceIsScheduled.CheckedChanged, AddressOf ReportProperty_Modified
                RemoveHandler ceIsLocked.CheckedChanged, AddressOf ReportProperty_Modified
                RemoveHandler ceIsEnabled.CheckedChanged, AddressOf ReportProperty_Modified
                RemoveHandler dePeriodStartTime.EditValueChanged, AddressOf ReportProperty_Modified
                RemoveHandler dePeriodEndTime.EditValueChanged, AddressOf ReportProperty_Modified
                RemoveHandler deScheduleStartTime.EditValueChanged, AddressOf ReportProperty_Modified
                RemoveHandler cmbScheduleInterval.SelectedIndexChanged, AddressOf ReportProperty_Modified
                RemoveHandler cmbObjectAggr.SelectedIndexChanged, AddressOf ReportProperty_Modified
                RemoveHandler cmbTimeAggr.SelectedIndexChanged, AddressOf ReportProperty_Modified
                RemoveHandler cmbOutputFormat.SelectedIndexChanged, AddressOf ReportProperty_Modified
                RemoveHandler ceIsOutputPivotted.CheckedChanged, AddressOf ReportProperty_Modified
                RemoveHandler txtOutputFolder.TextChanged, AddressOf ReportProperty_Modified
                RemoveHandler txtManualSQL.TextChanged, AddressOf ReportProperty_Modified
                RemoveHandler ceEmailEnabled.CheckedChanged, AddressOf ReportProperty_Modified
                RemoveHandler txtEmailAddresses.TextChanged, AddressOf ReportProperty_Modified

                ceIsScheduled.Checked = IIf(IsDBNull(drList("IsScheduled")), False, drList("IsScheduled"))
                ceIsLocked.Checked = IIf(IsDBNull(drList("IsLocked")), False, drList("IsLocked"))
                ceIsEnabled.Checked = IIf(IsDBNull(drList("IsEnabled")), False, drList("IsEnabled"))

                If ceIsLocked.Checked = True Then
                    If Environment.UserName.ToUpper <> lblReportOwner.Text.Trim.ToUpper Then
                        lblReportLockedMsg.Text = "* The report is locked by another user"
                        btnSaveReport.Enabled = False
                    Else
                        lblReportLockedMsg.Text = ""
                        btnSaveReport.Enabled = True
                    End If
                Else
                    lblReportLockedMsg.Text = ""
                    btnSaveReport.Enabled = True
                End If

                If IsDBNull(drList("ReportDescription")) Then
                    lblReportDescription.Text = ""
                Else
                    lblReportDescription.Text = CStr(drList("ReportDescription"))
                End If

                If IsDBNull(drList("ReportLastRunTime")) = True Then
                    lblLastRunTime.Text = ""
                Else
                    lblLastRunTime.Text = CStr(drList("ReportLastRunTime"))
                End If

                If IsDBNull(drList("ReportedPeriodPredefined")) = False Then
                    cmbPredefTimeStats.EditValue = dtPredefPeriod.AsEnumerable().Where(Function(x) x.Field(Of Integer)("PredefinedPeriodID") = CInt(drList("ReportedPeriodPredefined")))(0)("GUIText")
                Else
                    cmbPredefTimeStats.SelectedIndex = 0
                End If

                dePeriodStartTime.EditValue = IIf(IsDBNull(drList("ReportedPeriodStartTime")), "", drList("ReportedPeriodStartTime"))
                dePeriodEndTime.EditValue = IIf(IsDBNull(drList("ReportedPeriodEndTime")), "", drList("ReportedPeriodEndTime"))

                deScheduleStartTime.EditValue = IIf(IsDBNull(drList("ScheduleStartTime")), DateTime.Now, drList("ScheduleStartTime"))
                cmbScheduleInterval.EditValue = IIf(IsDBNull(drList("ScheduleInterval")), "Select", drList("ScheduleInterval"))

                cmbObjectAggr.EditValue = IIf(IsDBNull(drList("ReportedObjectAggregation")), "Select", drList("ReportedObjectAggregation"))
                cmbTimeAggr.EditValue = IIf(IsDBNull(drList("ReportedTimeAggregation")), "Select", drList("ReportedTimeAggregation"))

                cmbOutputFormat.EditValue = IIf(IsDBNull(drList("OutputFormat")), "CSV", drList("OutputFormat"))
                ceIsOutputPivotted.Checked = IIf(IsDBNull(drList("IsOutputPivotted")), False, drList("IsOutputPivotted"))
                txtOutputFolder.Text = IIf(IsDBNull(drList("OutputFolder")), "", drList("OutputFolder"))
                txtManualSQL.Text = IIf(IsDBNull(drList("SQLQuery")), "", drList("SQLQuery"))

                ceEmailEnabled.Checked = IIf(IsDBNull(drList("EmailEnabled")), False, drList("EmailEnabled"))
                txtEmailAddresses.Text = IIf(IsDBNull(drList("EmailAddresses")), "", drList("EmailAddresses"))

                If txtManualSQL.Text.Trim <> "" Then
                    xtpManualSQL.Visible = True
                Else
                    xtpAutoSQL.Visible = True
                End If

                If IsDBNull(drList("LoadTimeout")) = True Then
                    txtTimeout.Text = 300
                Else
                    txtTimeout.Text = CStr(drList("LoadTimeout"))
                End If

                LoadKPIGrid(reportID)
                LoadObjectFilterGrid(reportID)
                LoadAliasesGrid(reportID)
                LoadReportStatus(reportID)

                IOSDevExpressGrid.ClearGrid(gcViewReport)
                lblMessage.Text = String.Empty

                AddHandler ceIsScheduled.CheckedChanged, AddressOf ReportProperty_Modified
                AddHandler ceIsLocked.CheckedChanged, AddressOf ReportProperty_Modified
                AddHandler ceIsEnabled.CheckedChanged, AddressOf ReportProperty_Modified
                AddHandler dePeriodStartTime.EditValueChanged, AddressOf ReportProperty_Modified
                AddHandler dePeriodEndTime.EditValueChanged, AddressOf ReportProperty_Modified
                AddHandler deScheduleStartTime.EditValueChanged, AddressOf ReportProperty_Modified
                AddHandler cmbScheduleInterval.SelectedIndexChanged, AddressOf ReportProperty_Modified
                AddHandler cmbObjectAggr.SelectedIndexChanged, AddressOf ReportProperty_Modified
                AddHandler cmbTimeAggr.SelectedIndexChanged, AddressOf ReportProperty_Modified
                AddHandler cmbOutputFormat.SelectedIndexChanged, AddressOf ReportProperty_Modified
                AddHandler ceIsOutputPivotted.CheckedChanged, AddressOf ReportProperty_Modified
                AddHandler txtOutputFolder.TextChanged, AddressOf ReportProperty_Modified
                AddHandler txtManualSQL.TextChanged, AddressOf ReportProperty_Modified
                AddHandler ceEmailEnabled.CheckedChanged, AddressOf ReportProperty_Modified
                AddHandler txtEmailAddresses.TextChanged, AddressOf ReportProperty_Modified

            End If

        Catch ex As Exception
            UserActionTracking(Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            btnSaveReport.Appearance.BackColor = Nothing
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbPredefTime_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cmbPredefTimeStats.SelectedIndex > 0 Then
            Dim dr() As DataRow = dtPredefPeriod.AsEnumerable().Where(Function(x) x.Field(Of Integer)("PredefinedPeriodID") = TryCast(cmbPredefTimeStats.SelectedItem, clsComboBoxItem).Value And x.Field(Of String)("Control") = "cmbPredefTimeStats").ToArray()
            If Not dr Is Nothing Then
                If dr.Count > 0 Then
                    Dim SQL As String = dr(0)("SQL").ToString
                    Dim dtPeriod As New DataTable
                    dtPeriod = DataAccessorODBC.GetDataTable(connStrIOSServer, SQL)
                    If dtPeriod IsNot Nothing AndAlso dtPeriod.Rows.Count > 0 Then
                        dePeriodStartTime.EditValue = dtPeriod.Rows(0)(0)
                        dePeriodEndTime.EditValue = dtPeriod.Rows(0)(1)
                    End If
                End If
            End If
            ChangeBtnSaveReportBGColor()
        End If
    End Sub

    Private Sub btnAddReport_Click(sender As Object, e As EventArgs) Handles btnAddReport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objAddNBIReport As New dlgAddNBIReport()
            objAddNBIReport.reportID = Nothing
            objAddNBIReport.ShowDialog()

            LoadNBIReports()
            If newNBIReportName IsNot Nothing Then
                gvReportsList.FocusedRowHandle = gvReportsList.LocateByValue("ReportName", newNBIReportName)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnModifyReport_Click(sender As Object, e As EventArgs) Handles btnModifyReport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim isPowerUser As Boolean = False
            If (lblReportOwner.Text.ToLower <> Environment.UserName.ToLower) Then
                If configMgr.User.IsPowerUser = True Then
                    isPowerUser = True
                Else
                    XtraMessageBox.Show("Only the report owner or the power user can modify the report", "Modify NBI Report!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    lblReportOwner.ForeColor = Color.Red
                    lblReportOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                    isPowerUser = False
                End If
            Else
                'report owner
                isPowerUser = True
            End If

            If (isPowerUser = True) Then
                Dim drList As DataRow = gvReportsList.GetFocusedDataRow()
                If drList IsNot Nothing Then
                    Dim objAddNBIReport As New dlgAddNBIReport()
                    objAddNBIReport.reportID = CInt(drList("ReportID"))
                    objAddNBIReport.reportName = CStr(drList("ReportName"))
                    objAddNBIReport.reportDesc = CStr(drList("ReportDescription"))
                    objAddNBIReport.technology = CStr(drList("IOS_TECH"))
                    objAddNBIReport.objectType = CStr(drList("ObjectType"))
                    objAddNBIReport.ShowDialog()
                End If

                LoadNBIReports()
                If newNBIReportName IsNot Nothing Then
                    gvReportsList.FocusedRowHandle = gvReportsList.LocateByValue("ReportName", newNBIReportName)
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

    Private Sub btnDeleteReport_Click(sender As Object, e As EventArgs) Handles btnDeleteReport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lblReportOwner.Text.ToLower <> Environment.UserName.ToLower Then
                XtraMessageBox.Show("Current user can't delete the report as the report owner is a different user.", "Delete NBI Report!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblReportOwner.ForeColor = Color.Red
                lblReportOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                Exit Sub
            End If

            If gvReportsList.FocusedRowHandle > 0 Then
                Dim reportID As Integer = Nothing
                If XtraMessageBox.Show("Are you sure to delete report: " & gvReportsList.GetFocusedRowCellValue("ReportName"), "Delete NBI Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    reportID = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))
                    DeleteNBIReport(reportID)
                End If
                LoadNBIReports()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAddKPI_Click(sender As Object, e As EventArgs) Handles btnAddKPI.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim reportID As Integer = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))

            Dim objDlgAddKPINBIReport As New dlgAddKPINBIReport()
            objDlgAddKPINBIReport.reportID = reportID
            objDlgAddKPINBIReport.iosTech = CStr(gvReportsList.GetFocusedRowCellValue("IOS_TECH"))
            objDlgAddKPINBIReport.objectType = CStr(gvReportsList.GetFocusedRowCellValue("ObjectType"))
            objDlgAddKPINBIReport.ShowDialog()

            'reload KPI grid to show newly added kpi
            LoadKPIGrid(reportID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteKPI_Click(sender As Object, e As EventArgs) Handles btnDeleteKPI.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim reportID As Integer = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))
            If (gvKPI.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvKPI.FocusedRowHandle
                Dim kpiName As String = gvKPI.GetFocusedRowCellValue("KPI_Name")
                Dim kpiID As Integer = gvKPI.GetFocusedRowCellValue("SQLKPI_ID")
                If XtraMessageBox.Show("Are you sure to delete kpi: " & kpiName & "?", "Delete KPI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteKPI(reportID, kpiID)
                    LoadKPIGrid(reportID)
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

    Private Sub btnAddObjFilter_Click(sender As Object, e As EventArgs) Handles btnAddObjFilter.Click
        Try
            Dim reportID As Integer = gvReportsList.GetFocusedRowCellValue("ReportID")
            Dim objFilter As New dlgObjFilter("NBIReport", reportID)
            objFilter.ShowDialog()

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadObjectFilterGrid(reportID)
            grdObjFilter.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteObjFilter_Click(sender As Object, e As EventArgs) Handles btnDeleteObjFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim reportID As Integer = gvReportsList.GetFocusedRowCellValue("ReportID")
            If (gvObjFilter.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvObjFilter.FocusedRowHandle
                Dim filterSting As String = gvObjFilter.GetFocusedRowCellValue("FilterString")
                Dim filterID As Integer = gvObjFilter.GetFocusedRowCellValue("ReportFilterID")
                If XtraMessageBox.Show("Are you sure to delete filter: " & filterSting & "?", "Delete Filter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteObjFilter(filterID)
                    LoadObjectFilterGrid(reportID)
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

    Private Sub btnSaveReport_Click(sender As Object, e As EventArgs) Handles btnSaveReport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim predefinedPeriod As Integer = Nothing
            Dim periodStartTime As String = "NULL"
            Dim periodEndTime As String = "NULL"

            Dim drList As DataRow = gvReportsList.GetFocusedDataRow()
            If drList IsNot Nothing Then

                Dim ReportID As Integer = CInt(drList("ReportID"))
                parray = Nothing
                ConnStringAndSqlParam = Nothing

                If cmbPredefTimeStats.SelectedIndex = 0 Then
                    predefinedPeriod = 0
                    periodStartTime = Chr(39) & dePeriodStartTime.EditValue.ToString & Chr(39)
                    periodEndTime = Chr(39) & dePeriodEndTime.EditValue.ToString & Chr(39)
                Else

                    Try
                        predefinedPeriod = CInt(dtPredefPeriod.AsEnumerable().Where(Function(x) x.Field(Of String)("GUIText") = cmbPredefTimeStats.SelectedItem.ToString).ToArray()(0)("PredefinedPeriodID"))
                        periodStartTime = Chr(39) & dePeriodStartTime.EditValue.ToString & Chr(39)
                        periodEndTime = Chr(39) & dePeriodEndTime.EditValue.ToString & Chr(39)
                    Catch
                    End Try

                End If

                parray = {
                    New String() {"@ReportedPeriodPredefined", IIf(predefinedPeriod = 0, "NULL", predefinedPeriod)},
                    New String() {"@ReportedPeriodStartTime", periodStartTime},
                    New String() {"@ReportedPeriodEndTime", periodEndTime},
                    New String() {"@ReportedObjectAggregation", IIf(cmbObjectAggr.SelectedIndex = 0, "NULL", Chr(39) & cmbObjectAggr.SelectedItem.ToString & Chr(39))},
                    New String() {"@ReportedTimeAggregation", IIf(cmbTimeAggr.SelectedIndex = 0, "NULL", Chr(39) & cmbTimeAggr.SelectedItem.ToString & Chr(39))},
                    New String() {"@IsOutputPivotted", IIf(ceIsOutputPivotted.Checked, 1, 0)},
                    New String() {"@OutputFormat", Chr(39) & cmbOutputFormat.SelectedItem.ToString & Chr(39)},
                    New String() {"@ScheduleStartTime", Chr(39) & deScheduleStartTime.EditValue.ToString & Chr(39)},
                    New String() {"@ScheduleInterval", IIf(cmbScheduleInterval.SelectedIndex = 0, "NULL", Chr(39) & cmbScheduleInterval.SelectedItem.ToString & Chr(39))},
                    New String() {"@IsEnabled", IIf(ceIsEnabled.Checked, 1, 0)},
                    New String() {"@IsLocked", IIf(ceIsLocked.Checked, 1, 0)},
                    New String() {"@IsScheduled", IIf(ceIsScheduled.Checked, 1, 0)},
                    New String() {"@SQLQuery", Chr(39) & txtManualSQL.Text.Trim.Replace("'", "''") & Chr(39)},
                    New String() {"@OutputFolder", Chr(39) & txtOutputFolder.Text.Trim & Chr(39)},
                    New String() {"@EmailEnabled", IIf(ceEmailEnabled.Checked, 1, 0)},
                    New String() {"@EmailAddresses", Chr(39) & txtEmailAddresses.Text.Trim & Chr(39)},
                    New String() {"@LoadTimeout", CInt(txtTimeout.Text.Trim)},
                    New String() {"@ReportID", ReportID}
                }
                ConnStringAndSqlParam = GetSQL(8534, parray)
                DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))

                SetMessage("NBI Report config details saved successfully")
                LoadNBIReports()
                gvReportsList.FocusedRowHandle = gvReportsList.LocateByValue("ReportID", ReportID)
            End If

            'update the auto fill list for new output folder entry saved
            LoadOutputFolderTextBoxAutoFill(txtOutputFolder)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            SetMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            btnSaveReport.Appearance.BackColor = Nothing
        End Try
    End Sub

    Private Sub btnLoadReport_Click(sender As Object, e As EventArgs) Handles btnLoadReport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim sw As Stopwatch = Stopwatch.StartNew()

            Dim dt As DataTable = LoadNBIReportData()
            IOSDevExpressGrid.PopulateDataInGrid(gcViewReport, gvViewReport, dt, "ALL")

            sw.Stop()
            lblMessage.Text = "Total Records: " & dt.Rows.Count.ToString & vbCrLf & "Time executed (ms): " & sw.Elapsed.TotalMilliseconds

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnExport2CSV_Click(sender As Object, e As EventArgs) Handles btnExport2CSV.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Dim dtReportData As DataTable = Nothing
            Dim objFileDlg As New SaveFileDialog()
            Dim ReportID As Integer = 0

            If cmbOutputFormat.Text.Trim.ToUpper = "CSV" Then
                objFileDlg.Filter = "Comma Delimited |*.csv"
            ElseIf cmbOutputFormat.Text.Trim.ToUpper = "CLF" Then
                objFileDlg.Filter = "Common Log Files |*.clf"
            End If

            objFileDlg.Title = "Save a CSV|CLF File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Exporting to " & cmbOutputFormat.Text.Trim & "...")
                    Application.DoEvents()

                    'If gvViewReport.RowCount = 0 Then
                    '    dtReportData = LoadNBIReportData()
                    'Else
                    '    dtReportData = DirectCast(gcViewReport.DataSource, DataTable)
                    'End If

                    'get delimiter from dtNBIReports
                    Dim drList As DataRow = gvReportsList.GetFocusedDataRow()
                    Dim Delim As String = ","
                    If drList IsNot Nothing Then

                        ReportID = CInt(drList("ReportID"))
                        Dim dr() As DataRow = dtNBIReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ReportID") = ReportID).ToArray()
                        If dr.Count > 0 Then
                            Delim = nZ(dr(0)("OutputDelimiter"), ",").ToString
                        End If

                    End If

                    'get connection string for datareader operation
                    Dim connArr() As String = GetIOSConnection(1000)

                    If connArr(1) = "" Then
                        SetMessage("Connection String (ID = 1000) Is Unavailable")
                        Exit Sub
                    End If

                    'get report sql
                    parray = Nothing
                    ConnStringAndSqlParam = Nothing
                    parray = {
                        New String() {"@ReportID", ReportID},
                        New String() {"@AdHocRun", 1},
                        New String() {"@ToNBI", 2}
                    }
                    ConnStringAndSqlParam = GetSQL(8535, parray)

                    'IOSDevExpressGrid.DataTable2CSV(dtReportData, objFileDlg.FileName, Delim)
                    If Not ConnStringAndSqlParam Is Nothing Then
                        ExportCSVData_Stream(connArr(1), ConnStringAndSqlParam(1), objFileDlg.FileName, Delim)
                    Else
                        SetMessage("Query To Execute The Report Procedure Is Unavailable")
                    End If
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub ExportCSVData_Stream(connString As String, sql2Execute As String, filePath As String, delimiter As String)
        Try
            Using srcConnection As New SqlConnection(connString)
                srcConnection.Open()
                Dim sqlCMd As New SqlCommand(sql2Execute, srcConnection)
                sqlCMd.CommandTimeout = txtTimeout.Text
                'Dim dtReader As SqlDataReader = Nothing
                'dtReader = sqlCMd.ExecuteReader()

                Dim RowCount As Long = 0
                Dim bufferSize = 1024 * 1024

                Using FileObject As New FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bufferSize)
                    Using writer As StreamWriter = New StreamWriter(FileObject)
                        Using dtReader As SqlDataReader = sqlCMd.ExecuteReader()

                            If dtReader.HasRows = True Then
                                Dim SchemaTable As DataTable = dtReader.GetSchemaTable()

                                'wrting coluimn names as the first row in the file
                                writer.Write(String.Format("{0}", dtReader.GetName(0)))
                                For i = 1 To dtReader.FieldCount - 1
                                    writer.Write(delimiter)
                                    writer.Write(String.Format("{0}", dtReader.GetName(i)))
                                Next

                                writer.Write(Environment.NewLine)
                                RowCount = RowCount + 1

                                While dtReader.Read

                                    For i = 0 To dtReader.FieldCount - 2
                                        Dim v As Object = dtReader.GetValue(i)

                                        If Not IsDBNull(v) Then
                                            Select Case SchemaTable(i)("DataTypeName").ToString
                                                Case "real"
                                                    writer.Write(String.Format("{0}", CDbl(v)))
                                                Case "float"
                                                    writer.Write(String.Format("{0}", CDbl(v)))
                                                Case "datetime"
                                                    'writer.Write(String.Format("{0}", v))
                                                    If regionalSettings = False Then
                                                        writer.Write(DirectCast(v, DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                                                    Else
                                                        writer.Write(DirectCast(v, DateTime).ToString("g", CultureInfoDefault))
                                                    End If
                                                Case Else
                                                    writer.Write(String.Format("{0}", v))
                                            End Select
                                        Else
                                            writer.Write(String.Format("{0}", v))
                                        End If

                                        writer.Write(delimiter)
                                    Next

                                    Dim v_end As Object = dtReader.GetValue(dtReader.FieldCount - 1)

                                    If Not IsDBNull(v_end) Then
                                        Select Case SchemaTable(dtReader.FieldCount - 1)("DataTypeName").ToString
                                            Case "real"
                                                writer.Write(String.Format("{0}", CDbl(v_end)))
                                            Case "float"
                                                writer.Write(String.Format("{0}", CDbl(v_end)))
                                            Case "datetime"
                                                'writer.Write(String.Format("{0}", v))
                                                If regionalSettings = False Then
                                                    writer.Write(DirectCast(v_end, DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                                                Else
                                                    writer.Write(DirectCast(v_end, DateTime).ToString("g", CultureInfoDefault))
                                                End If
                                            Case Else
                                                writer.Write(String.Format("{0}", v_end))
                                        End Select
                                    Else
                                        writer.Write(String.Format("{0}", v_end))
                                    End If

                                    writer.Write(Environment.NewLine)
                                    RowCount = RowCount + 1

                                    If RowCount Mod 1000 = 0 Then
                                        lblMessage.Text = "Exported Records Count: " & RowCount.ToString
                                        Application.DoEvents()
                                    End If

                                End While

                                writer.Flush()
                            Else
                                SetMessage("No data to export!")
                                Exit Sub
                            End If

                        End Using
                    End Using
                End Using

                lblMessage.Text = "Total Exported Records: " & RowCount.ToString

                srcConnection.Close()
                srcConnection.Dispose()

            End Using
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub xtcSQL_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) 'Handles xtcSQL.SelectedPageChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub Control_DragOver(sender As Object, e As DragEventArgs) Handles txtManualSQL.DragOver, gcTables.DragOver, gcColumns.DragOver, gcKPIs.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub txtManualSQL_DragDrop(sender As Object, e As DragEventArgs) Handles txtManualSQL.DragDrop
        Try
            Dim val() As Object = e.Data.GetData("System.Object[]")
            If val IsNot Nothing Then
                txtManualSQL.Text = txtManualSQL.Text & vbCrLf & val(0).ToString
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gcTables_MouseMove(sender As Object, e As MouseEventArgs) Handles gcTables.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim data As DataRowView = gvTables.GetRow(gvTables.FocusedRowHandle)
                If data IsNot Nothing Then
                    Dim obj() As Object = {data.Item(0)}
                    gcTables.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gcColumns_MouseMove(sender As Object, e As MouseEventArgs) Handles gcColumns.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim data As DataRowView = gvColumns.GetRow(gvColumns.FocusedRowHandle)
                If data IsNot Nothing Then
                    Dim obj() As Object = {data.Item(0)}
                    gcColumns.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gcKPIs_MouseMove(sender As Object, e As MouseEventArgs) Handles gcKPIs.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim data As DataRowView = gvKPIs.GetRow(gvKPIs.FocusedRowHandle)
                If data IsNot Nothing Then
                    Dim obj() As Object = {data.Item(1)}
                    gcKPIs.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Try
            Dim sqlTestToFire As String = Me.txtManualSQL.Text
            If txtManualSQL.Text <> String.Empty Then
                Dim objNBIReportManualSQL As New dlgNBIReportManualSQL()

                If txtManualSQL.Text.Contains("@starttime") Or txtManualSQL.Text.Contains("@endtime") Then
                    If Not dePeriodStartTime.EditValue Is Nothing AndAlso dePeriodStartTime.EditValue.ToString <> "" Then
                        sqlTestToFire = Replace(sqlTestToFire, "@starttime", Chr(39) + dePeriodStartTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss") + Chr(39))
                    End If
                    If Not dePeriodEndTime.EditValue Is Nothing AndAlso dePeriodEndTime.EditValue.ToString <> "" Then
                        sqlTestToFire = Replace(sqlTestToFire, "@endtime", Chr(39) + dePeriodEndTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss") + Chr(39))
                    End If
                End If

                objNBIReportManualSQL.sqlQuery = sqlTestToFire
                objNBIReportManualSQL.ShowDialog()

            End If

        Catch ex As Exception
            lblTestStatus.Text = ex.Message.ToString
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)

        End Try
    End Sub

    Private Sub btnStartTime_Click(sender As Object, e As EventArgs) Handles btnStartTime.Click
        txtManualSQL.Text = txtManualSQL.Text & vbCrLf & "@starttime"
    End Sub

    Private Sub btnEndTime_Click(sender As Object, e As EventArgs) Handles btnEndTime.Click
        txtManualSQL.Text = txtManualSQL.Text & vbCrLf & "@endtime"
    End Sub

    Private Sub btnAddAlises_Click(sender As Object, e As EventArgs) Handles btnAddAlises.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim reportID As Integer = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))

            Dim objAddNBIReportAlias As New dlgAddNBIReportAlias()
            objAddNBIReportAlias.reportID = reportID
            objAddNBIReportAlias.ShowDialog()

            'reload Aliases grid
            LoadAliasesGrid(reportID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteAliases_Click(sender As Object, e As EventArgs) Handles btnDeleteAliases.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim reportID As Integer = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))
            If (gvAliases.SelectedRowsCount > 0) Then
                Dim sAlias As String = gvAliases.GetFocusedRowCellValue("Alias")
                Dim reportAliasID As Integer = gvAliases.GetFocusedRowCellValue("ReportAliasID")
                If XtraMessageBox.Show("Are you sure to delete Alias: " & sAlias & "?", "Delete Alias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteAlias(reportAliasID)
                    LoadAliasesGrid(reportID)
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

    Private Sub gvAliases_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvAliases.CellValueChanged
        Try
            Dim reportAliasID As Integer = CInt(gvAliases.GetFocusedRowCellValue("ReportAliasID"))

            If (e.Column.FieldName.ToUpper = "SOURCECOLUMN") Then
                parray = Nothing
                ConnStringAndSqlParam = Nothing
                parray = {
                    New String() {"@SourceColumn", Chr(39) & e.Value.ToString & Chr(39)},
                    New String() {"@ReportAliasID", reportAliasID}
                }
                ConnStringAndSqlParam = GetSQL(8542, parray)

            ElseIf (e.Column.FieldName.ToUpper = "ALIAS") Then
                parray = Nothing
                ConnStringAndSqlParam = Nothing
                parray = {
                    New String() {"@Alias", Chr(39) & e.Value.ToString & Chr(39)},
                    New String() {"@ReportAliasID", reportAliasID}
                }
                ConnStringAndSqlParam = GetSQL(8543, parray)

            End If

            DataAccessorODBC.ExecuteNonQuery(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub frmNBIReports_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Try
            sccManualSQL.SplitterPosition = sccManualSQL.Width - sccManualSQL.Panel2.MinSize
            sccReports.SplitterPosition = sccReports.Panel1.MinSize + 300
        Catch
        End Try
    End Sub

    Private Sub gvReportsList_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvReportsList.ShowingEditor
        Try
            If ceIsLocked.Checked = False Then
                'If Environment.UserName.ToUpper = lblReportOwner.Text.Trim.ToUpper Then
                If gvReportsList.FocusedColumn.ColumnType.Name = "Boolean" Then
                    updateColumnName = gvReportsList.FocusedColumn.FieldName
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
                'Else
                'e.Cancel = True
                'End If
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub riCheckEditColumn_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            If chkBox IsNot Nothing Then
                UpdateReportColumn(chkBox.CheckState)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ReportProperty_Modified(sender As Object, e As EventArgs)
        Try
            ChangeBtnSaveReportBGColor()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnCopyReport_Click(sender As Object, e As EventArgs) Handles btnCopyReport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If gvReportsList.FocusedRowHandle > 0 Then
                Dim reportID As Integer = CInt(gvReportsList.GetFocusedRowCellValue("ReportID"))
                CopyNBIReport(reportID)
                LoadNBIReports()
                Dim newReportID As Integer = CInt(dtNBIReports.Compute("MAX(ReportID)", ""))
                gvReportsList.FocusedRowHandle = gvReportsList.LocateByValue("ReportID", newReportID)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

End Class