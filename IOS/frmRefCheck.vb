Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.Data
Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.Data.Filtering
Imports dotnetCHARTING.WinForms

Public Class frmRefCheck

#Region "Variables"

    Private dtMOList As DataTable = Nothing
    Private dtParameterList As DataTable = Nothing
    Private riCmb As RepositoryItemComboBox
    Private riCmbPriority As RepositoryItemComboBox
    Private objThreadResults As System.Threading.Thread
    Private Delegate Sub CallThreadInvokedGetResults(Row As DataRow, Status As Integer)
    Private objResultsThreadLock As New Object
    Private _isResizing As Boolean = False

    Private selectedXmlJobID As Integer = 0
    Private selectedXmlJobName As String = Nothing
    Private dtXmlInputRefCheck As DataTable = Nothing
    Private cmsGridControlName As String = Nothing
    Private RefreshLoadAllTemplates As Boolean = False

    Public copyFromSrcTemplateID As Integer = 0
    Public copyFromSrcTemplateMOConfigID As Integer = 0
    Public copyFilterStringsFromMO As Boolean = False
    Public copyInclusionListFromMO As Boolean = False
    Public copyExclusionListFromMO As Boolean = False
    Public copyParamExclusionListFromTemplate As Boolean = False

#End Region

#Region "Events"

    Private Sub frmRefCheck_Load(sender As Object, e As EventArgs) Handles Me.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            EnableDisableButtons()
            BindVendor()
            AddHandler tbChartHeightStats.EditValueChanged, AddressOf dtb_ChartHeight_EditValueChanged
            ResizeChartHeights(tbChartHeightStats.EditValue)

            If dtIncList.Columns.Count = 0 Then
                dtIncList.Columns.Add("ListID", GetType(System.Int32))
                dtIncList.Columns.Add("ListName", GetType(System.String))
                dtIncList.Columns.Add("ListType", GetType(System.String))
            End If

            If dtExcList.Columns.Count = 0 Then
                dtExcList.Columns.Add("ListID", GetType(System.Int32))
                dtExcList.Columns.Add("ListName", GetType(System.String))
                dtExcList.Columns.Add("ListType", GetType(System.String))
            End If

            FillSearchCombo()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frmRefCheck_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        _isResizing = True
        sccConfigMain.SplitterPosition = sccConfigMain.Height / 2
        sccConfigTop.SplitterPosition = sccConfigTop.Width - sccConfigTop.Panel2.MinSize
        sccConfigBottom.SplitterPosition = sccConfigBottom.Width - sccConfigBottom.Panel2.MinSize
        Charts_ResizeWidth()
        Me.Refresh()
    End Sub

    Private Sub dtb_ChartHeight_EditValueChanged(sender As Object, e As EventArgs)
        ResizeChartHeights(tbChartHeightStats.EditValue)
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            IOSDevExpressGrid.ClearGrid(gcMOParam)
            If cmbVendor.SelectedIndex > 0 Then
                LoadTemplateList()
            Else
                ClearFormData()
            End If
            EnableDisableButtons()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvTemplateList_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvTemplateList.RowCount > 0 AndAlso e IsNot Nothing Then
                gvTemplateList.ClearSelection()
                gvTemplateList.FocusedRowHandle = e.FocusedRowHandle
                gvTemplateList.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler ceIsScheduled.CheckedChanged, AddressOf ceIsScheduled_CheckedChanged
            RemoveHandler ceIsLocked.CheckedChanged, AddressOf ceIsLocked_CheckedChanged
            RemoveHandler ceIsEnabled.CheckedChanged, AddressOf ceIsEnabled_CheckedChanged
            RemoveHandler gvMOConfig.FocusedRowChanged, AddressOf gvMOConfig_FocusedRowChanged

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                'get updated values of IsScheduled, IsEnabled & IsLocked
                Dim drTemplate As DataRow = dtCMTemplate.Select("TemplateID=" & dr("TemplateID"))(0)

                lblTemplateOwner.Text = IIf(IsDBNull(dr("Owner")), "", dr("Owner"))
                ceIsScheduled.Checked = IIf(IsDBNull(drTemplate("IsScheduled")), False, drTemplate("IsScheduled"))
                ceIsLocked.Checked = IIf(IsDBNull(drTemplate("IsLocked")), False, drTemplate("IsLocked"))
                ceIsEnabled.Checked = IIf(IsDBNull(drTemplate("IsEnabled")), False, drTemplate("IsEnabled"))
                lblLatestConfigChange.Text = IIf(IsDBNull(drTemplate("LatestConfigUpdate")), "", drTemplate("LatestConfigUpdate"))
                txtDescription.Text = IIf(IsDBNull(drTemplate("TemplateDescription")), "", drTemplate("TemplateDescription"))
                lblLastRunTime.Text = IIf(IsDBNull(drTemplate("LastRunTime")), "", drTemplate("LastRunTime"))
                lblLastCMDate.Text = IIf(IsDBNull(drTemplate("LastCMDate")), "", drTemplate("LastCMDate"))

                'if <Config> tab is active
                ' If xtcMainOuter.SelectedTabPageIndex = 0 Then
                'load MO config grid for selected template
                LoadMOConfigGrid(CInt(dr("TemplateID")))
                LoadExcludedParamList(CInt(dr("TemplateID")))
                '   End If

                If Not IsDBNull(dr("LastStatus")) Then
                    If dr("LastStatus") = "Running" Then
                        btnGetResults.Text = "Abort Run!"
                        btnGetResults.LookAndFeel.UseDefaultLookAndFeel = False
                    Else
                        btnGetResults.Text = "Get Results"
                        btnGetResults.LookAndFeel.UseDefaultLookAndFeel = True
                    End If
                Else
                    btnGetResults.Text = "Get Results"
                End If

                Try
                    'load all the results tab grids
                    'LoadTemplateSummaryGrid()
                    'LoadInconsistencySummaryGrid()
                    'LoadDetailedDataGrid()
                    'LoadStatusGrid()

                    'Clear all the grids of results tab
                    IOSDevExpressGrid.ClearGrid(gcTemplateSumm)
                    IOSDevExpressGrid.ClearGrid(gcInconSumm)
                    IOSDevExpressGrid.ClearGrid(gcDetailedData)
                    IOSDevExpressGrid.ClearGrid(gcStatus)

                    LoadResultChart1()
                    LoadResultChart2()
                    IOSDevExpressGrid.ClearGrid(grdChart2)
                    ClearChart3()

                    If xtcMainOuter.SelectedTabPageIndex = 2 Then
                        'if <View Template> tab is active
                        LoadViewTemplateGrid()
                    ElseIf xtcMainOuter.SelectedTabPageIndex = 3 Then
                        'if <View Change Log> tab is active
                        LoadViewChangeLogGrid()
                    End If
                Catch
                End Try

            Else
                ClearFormData()
            End If

            AddHandler gvMOConfig.FocusedRowChanged, AddressOf gvMOConfig_FocusedRowChanged
            AddHandler ceIsScheduled.CheckedChanged, AddressOf ceIsScheduled_CheckedChanged
            AddHandler ceIsLocked.CheckedChanged, AddressOf ceIsLocked_CheckedChanged
            AddHandler ceIsEnabled.CheckedChanged, AddressOf ceIsEnabled_CheckedChanged

            If (ceIsLocked.Checked = True) AndAlso (lblTemplateOwner.Text.ToLower <> Environment.UserName.ToLower) Then
                lblTemplateOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblTemplateOwner.ForeColor = Color.Red

                ceIsScheduled.Enabled = False
                ceIsLocked.Enabled = False
                ceIsEnabled.Enabled = False

                btnAddFilter.Enabled = False
                btnDeleteFilter.Enabled = False

                If configMgr.User.IsPowerUser = True Then
                    btnAddFilter.Enabled = True
                    btnDeleteFilter.Enabled = True
                    tlpConfig.Enabled = True
                Else
                    tlpConfig.Enabled = False
                End If
            Else
                lblTemplateOwner.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblTemplateOwner.ForeColor = Color.Black

                ceIsScheduled.Enabled = True
                ceIsLocked.Enabled = True
                ceIsEnabled.Enabled = True

                btnAddFilter.Enabled = True
                btnDeleteFilter.Enabled = True

                tlpConfig.Enabled = True
            End If
            gvMOConfig_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvMOConfig_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvMOConfig.RowCount > 0 AndAlso e IsNot Nothing Then
                gvMOConfig.ClearSelection()
                gvMOConfig.FocusedRowHandle = e.FocusedRowHandle
                gvMOConfig.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {}

            Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
            If dr IsNot Nothing Then
                'load mo param grid
                LoadMOParamGrid(dr("TemplateMOConfigID"), dr("MOName"))

                'load mo filter grid
                LoadMOFilterGrid(dr("TemplateMOConfigID"))

                'load inclusion/exclusion filter lists
                LoadFilterLists(dr("TemplateMOConfigID"))

                ManageGroupControlsFromMOConfig(CBool(dr("IsAllParameters")), CBool(dr("IsActive")))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsScheduled_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateID", dr("TemplateID")},
                    New String() {"@IsScheduled", IIf(ceIsScheduled.Checked, 1, 0)}
                }
                strConnection = GetSQL(4104, parray)(0)
                sqlParam = GetSQL(4104, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                GetTemplateList()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsLocked_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateID", dr("TemplateID")},
                    New String() {"@IsLocked", IIf(ceIsLocked.Checked, 1, 0)}
                }
                strConnection = GetSQL(4103, parray)(0)
                sqlParam = GetSQL(4103, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                GetTemplateList()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsEnabled_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateID", dr("TemplateID")},
                    New String() {"@IsEnabled", IIf(ceIsEnabled.Checked, 1, 0)}
                }
                strConnection = GetSQL(4102, parray)(0)
                sqlParam = GetSQL(4102, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                GetTemplateList()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtDescription_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDescription.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
                If dr IsNot Nothing Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@TemplateID", dr("TemplateID")},
                        New String() {"@templateDescription", Chr(39) & txtDescription.Text.Trim & Chr(39)}
                    }
                    strConnection = GetSQL(4162, parray)(0)
                    sqlParam = GetSQL(4162, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    GetTemplateList()
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbVendor.SelectedIndex > 0 Then
                LoadTemplateList()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objDlgtemplate As New dlgCMTemplate()
            objDlgtemplate.actionType = "ADD"
            objDlgtemplate.SetConnectionString(connStrIOSServer)
            objDlgtemplate.templateVendor = cmbVendor.Text.Trim
            objDlgtemplate.ShowDialog()

            If (newTemplateName IsNot Nothing) Then
                LoadTemplateList()
                gvTemplateList.FocusedRowHandle = gvTemplateList.LocateByValue("TemplateName", newTemplateName)

                'clear all the grids for a new template
                ClearGridsForNewOrEmptyTemplate()
            End If
            gcTemplateList.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (gvTemplateList.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvTemplateList.FocusedRowHandle
                Dim objDlgtemplate As New dlgCMTemplate()
                objDlgtemplate.actionType = "COPY"
                objDlgtemplate.SetConnectionString(connStrIOSServer)
                objDlgtemplate.copyToTemplateID = gvTemplateList.GetRowCellValue(selectedRowHandle, "TemplateID")
                objDlgtemplate.ShowDialog()

                If (newTemplateName IsNot Nothing) Then
                    LoadTemplateList()
                    gvTemplateList.FocusedRowHandle = gvTemplateList.LocateByValue("TemplateName", newTemplateName)
                End If
            End If
            gcTemplateList.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If lblTemplateOwner.Text.ToLower <> Environment.UserName.ToLower Then
                XtraMessageBox.Show("Current user can't delete a template as the template owner is a different user.", "Delete Template!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblTemplateOwner.ForeColor = Color.Red
                lblTemplateOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                Exit Sub
            End If
            If (gvTemplateList.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvTemplateList.FocusedRowHandle
                Dim templateName As String = gvTemplateList.GetRowCellValue(selectedRowHandle, "TemplateName")
                Dim templateID As Integer = gvTemplateList.GetRowCellValue(selectedRowHandle, "TemplateID")
                If XtraMessageBox.Show("Are you sure to delete template name: " & templateName & "?", "Delete Template Name", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteTemplate(templateID)
                    gvTemplateList.DeleteRow(selectedRowHandle)
                    If gvTemplateList.RowCount > 0 Then
                        gvTemplateList.SelectRow(0)
                    End If
                    gcTemplateList.Refresh()
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

    Private Sub btnClearTemplate_Click(sender As Object, e As EventArgs) Handles btnClearTemplate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim isPowerUser As Boolean = False
            If (lblTemplateOwner.Text.ToLower <> Environment.UserName.ToLower) Then
                If configMgr.User.IsPowerUser = True Then
                    isPowerUser = True
                Else
                    XtraMessageBox.Show("Only the template owner or the power user can clear the template", "Clear Template!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    lblTemplateOwner.ForeColor = Color.Red
                    lblTemplateOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                    isPowerUser = False
                    Exit Sub
                End If
            Else
                'template owner
                isPowerUser = True
            End If

            If (isPowerUser = True) Then
                If (gvTemplateList.FocusedRowHandle > 0) Then
                    Dim templateName As String = gvTemplateList.GetFocusedRowCellValue("TemplateName")
                    Dim templateID As Integer = gvTemplateList.GetFocusedRowCellValue("TemplateID")
                    If XtraMessageBox.Show("Are you sure to clear the template: " & templateName & "?", "Clear Template", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        ClearTemplate(templateID)
                        Me.SaveChangeLog(templateID, "", 0, "Template Cleared")
                        gvTemplateList_FocusedRowChanged(Nothing, Nothing)
                    End If
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

    Private Sub lstViewMO_DragOver(sender As Object, e As DragEventArgs) Handles gcMOParam.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub gcMOConfig_DragDrop(sender As Object, e As DragEventArgs) Handles gcMOParam.DragDrop
        Try
            Dim mo() As Object = e.Data.GetData("System.Object[]")
            If mo IsNot Nothing Then
                If mo(0) = "MO2Grid" Then
                    'If chkSearchAllParameter.Checked = False Then
                    '    GetListOfParameters(cmbVendor.SelectedItem.ToString(), "''" & mo(1) & "''")
                    'End If

                    'Check if MO is not yet in TemplateID
                    Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
                    If dr IsNot Nothing Then
                        Dim strConnection As String = Nothing
                        Dim sqlParam As String = Nothing
                        Dim parray()() As String = {
                            New String() {"@TemplateID", dr("TemplateID")},
                            New String() {"@MO", Chr(39) & mo(2).ToString & Chr(39)}
                        }
                        strConnection = GetSQL(4108, parray)(0)
                        sqlParam = GetSQL(4108, parray)(1)
                        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

                        If dt.Rows(0)("mo").ToString = "0" Then
                            LoadDataToMOGrid(dr("TemplateID").ToString, mo(2).ToString, mo(1).ToString, cmbVendor.SelectedItem.ToString)
                        End If
                    End If
                End If
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAddFilter_Click(sender As Object, e As EventArgs) Handles btnAddFilter.Click
        Try
            Dim drTemplate As DataRow = gvTemplateList.GetFocusedDataRow()

            Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
            Dim templateMOConfigID As Integer = CInt(dr("TemplateMOConfigID"))

            Dim objFilter As New dlgObjFilter("MOConfig", templateMOConfigID)
            objFilter.templateID = drTemplate("TemplateID")
            objFilter.ShowDialog()

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadMOFilterGrid(templateMOConfigID)
            gcMOFilter.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteFilter_Click(sender As Object, e As EventArgs) Handles btnDeleteFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (gvMOFilter.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvMOFilter.FocusedRowHandle
                Dim filterName As String = gvMOFilter.GetRowCellValue(selectedRowHandle, "FilterString")
                Dim templateMOFilterID As Integer = gvMOFilter.GetRowCellValue(selectedRowHandle, "TemplateMOFilterID")

                If XtraMessageBox.Show("Are you sure to delete filter: " & filterName & "?", "Delete Filter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteFilter(templateMOFilterID)
                    gvMOFilter.DeleteRow(selectedRowHandle)
                    'save change log
                    Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(gvMOConfig.GetFocusedRowCellValue("MOName")), CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), "Filter: " & filterName & " deleted from the mo: " & gvMOConfig.GetFocusedRowCellValue("MOName").ToString)
                    If gvMOFilter.RowCount > 0 Then
                        gvMOFilter.SelectRow(0)
                    End If
                    gcMOFilter.Refresh()
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

    Private Sub btnDeleteFilterinAllMO_Click(sender As Object, e As EventArgs) Handles btnDeleteFilterinAllMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (gvMOFilter.SelectedRowsCount > 0) Then
                Dim filterName As String = gvMOFilter.GetFocusedRowCellValue("FilterString").ToString
                Dim templateID As Integer = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
                Dim templateName As String = CStr(gvTemplateList.GetFocusedRowCellValue("TemplateName"))
                Dim moFilterID As Integer = CInt(gvMOFilter.GetFocusedRowCellValue("TemplateMOFilterID"))

                If XtraMessageBox.Show("Are you sure to delete filter: " & filterName & " in all MO of template: " & templateName & "?", "Delete Filter in All MO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteFilterInAllMO(templateID, filterName)
                    gvMOFilter.DeleteRow(gvMOFilter.FocusedRowHandle)
                    'save change log
                    Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), "", 0, "Filter: " & filterName & " deleted from the all mo in template: " & templateName)
                    If gvMOFilter.RowCount > 0 Then
                        gvMOFilter.SelectRow(0)
                    End If
                    gcMOFilter.Refresh()
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

    Private Sub gvMOConfig_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvMOConfig.ShowingEditor
        Try
            If (gvMOConfig.FocusedColumn().FieldName = "IsAllParameters") Or (gvMOConfig.FocusedColumn().FieldName = "IsActive") Or
               (gvMOConfig.FocusedColumn().FieldName = "IsAutoSetValue") Or (gvMOConfig.FocusedColumn().FieldName = "CommonalityValue") Or
               (gvMOConfig.FocusedColumn().FieldName = "Priority") Or (gvMOConfig.FocusedColumn().FieldName = "CheckMissingNE") Or (gvMOConfig.FocusedColumn().FieldName = "InfoField") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvMOConfig_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvMOConfig.CellValueChanged
        Try
            Dim templateMOConfigID As Integer = 0
            Dim rIndex() As Integer = Nothing

            Dim data As DataRow = gvMOConfig.GetFocusedDataRow()
            If data IsNot Nothing Then
                If e.Column.FieldName.ToUpper = "COMMONALITYVALUE" Then

                    rIndex = gvMOConfig.GetSelectedRows()
                    If rIndex.Length > 0 Then
                        Dim drow As DataRow = gvMOConfig.GetRow(rIndex(0)).Row
                        templateMOConfigID = drow.Item("TemplateMOConfigID")
                    End If

                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@TemplateMOConfigID", templateMOConfigID},
                        New String() {"@CommonalityValue", Chr(39) & data.Item("CommonalityValue") & Chr(39)}
                    }
                    strConnection = GetSQL(4132, parray)(0)
                    sqlParam = GetSQL(4132, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                ElseIf e.Column.FieldName.ToUpper = "INFOFIELD" Then

                    rIndex = gvMOConfig.GetSelectedRows()
                    If rIndex.Length > 0 Then
                        Dim drow As DataRow = gvMOConfig.GetRow(rIndex(0)).Row
                        templateMOConfigID = drow.Item("TemplateMOConfigID")
                    End If

                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@TemplateMOConfigID", templateMOConfigID},
                        New String() {"@InfoField", Chr(39) & data.Item("InfoField") & Chr(39)}
                    }
                    strConnection = GetSQL(4212, parray)(0)
                    sqlParam = GetSQL(4212, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                End If

                'save change log
                Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(data.Item("MOName")), CInt(data.Item("TemplateMOConfigID")), e.Column.FieldName & " modfied for the MO: " & data.Item("MOName").ToString)

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvMOParam_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvMOParam.CellValueChanged
        Try
            If e.Column.FieldName.ToUpper = "COMMONALITYVALUE" Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim focusedRowHandle As Integer = e.RowHandle
                Dim templateMOParamConfigID As Integer = 0
                Dim focusedRow As DataRowView = gvMOParam.GetFocusedRow()
                If focusedRow IsNot Nothing Then
                    templateMOParamConfigID = CInt(focusedRow("TemplateMOParamConfigID"))
                End If

                Dim data As DataRow = gvMOParam.GetFocusedDataRow()
                If data IsNot Nothing Then
                    UpdateMOParamCommonalityValue(templateMOParamConfigID, data.Item("CommonalityValue"))
                    UpdateTemplateLatestConfig()
                End If

                gcMOParam.Refresh()
                gvMOParam.SelectRow(focusedRowHandle)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvMOParam_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Dim dr As DataRow = gvMOParam.GetFocusedDataRow()
            If dr IsNot Nothing AndAlso CBool(dr("IsConditionActive")) = True Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateMOParamConfigID", CInt(dr("TemplateMOParamConfigID"))}
                }
                strConnection = GetSQL(4107, parray)(0)
                sqlParam = GetSQL(4107, parray)(1)
                Dim dtCondition As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOSDevExpressGrid.PopulateDataInGrid(gcCondition, gvCondition, dtCondition, "ALL", {"ConditionID"}, "ConditionString")
                grpCtrlConditions.Enabled = True
            Else
                IOSDevExpressGrid.ClearGrid(gcCondition)
                grpCtrlConditions.Enabled = False
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvMOParam_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvMOParam.ShowingEditor
        Try
            If (gvMOParam.FocusedColumn().FieldName = "IsAutoSetValue") Or (gvMOParam.FocusedColumn().FieldName = "CommonalityValue") Or
               (gvMOParam.FocusedColumn().FieldName = "IsActive") Or (gvMOParam.FocusedColumn().FieldName = "IsConditionActive") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvMOParam_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        Try
            If e.Column.FieldName = "Operator" Then
                e.RepositoryItem = riCmb
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnGetResults_Click(sender As Object, e As EventArgs) Handles btnGetResults.Click
        Try
            Dim rIndex() As Integer = gvTemplateList.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvTemplateList.GetRow(rIndex(0)).Row
                Dim templateID As Integer = dr("TemplateID")

                If btnGetResults.Text = "Abort Run!" Then
                    dr("LastStatus") = "Idle"
                    gcTemplateList.Refresh()
                    btnGetResults.LookAndFeel.UseDefaultLookAndFeel = True
                    btnGetResults.Text = "Get Results"

                    If objThreadResults IsNot Nothing Then  'AndAlso objThreadResults.ThreadState = Threading.ThreadState.Running
                        objThreadResults.Abort()
                    End If
                Else
                    'clearing grids
                    IOSDevExpressGrid.ClearGrid(gcTemplateSumm)
                    IOSDevExpressGrid.ClearGrid(gcInconSumm)
                    IOSDevExpressGrid.ClearGrid(gcDetailedData)
                    IOSDevExpressGrid.ClearGrid(gcStatus)
                    IOSDevExpressGrid.ClearGrid(gcViewTemplate)

                    btnGetResults.LookAndFeel.UseDefaultLookAndFeel = False
                    btnGetResults.Text = "Abort Run!"
                    dr("LastStatus") = "Running"
                    gcTemplateList.Refresh()
                    Application.DoEvents()

                    Dim objGetResults As New GetResultsClass()
                    objGetResults.templateID = templateID
                    objGetResults.Status = 1
                    objGetResults.templateRow = dr
                    AddHandler objGetResults.ThreadComplete, AddressOf ExecuteAfteresultsThreadComplete
                    objThreadResults = New System.Threading.Thread(AddressOf objGetResults.RunNow)
                    objThreadResults.Start()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfteresultsThreadComplete(row As DataRow, status As Integer, ti As Threading.Thread)
        SyncLock objResultsThreadLock
            If objThreadResults.ThreadState = Threading.ThreadState.Aborted Then
                status = 0
            End If
            Dim arg() As Object = {row, status}
            Me.BeginInvoke(New CallThreadInvokedGetResults(AddressOf SetResultsLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetResultsLastStatus(Row As DataRow, Status As Integer)
        SyncLock objResultsThreadLock
            If objThreadResults.ThreadState = Threading.ThreadState.Aborted Then
                Status = 0
            End If
            If Row IsNot Nothing Then
                If Status = 0 Then
                    Row("LastStatus") = "Idle"
                ElseIf Status = 1 Then
                    Row("LastStatus") = "Running"
                ElseIf Status = -1 Then
                    Row("LastStatus") = "Error"
                End If
                gcTemplateList.Refresh()
                btnGetResults.LookAndFeel.UseDefaultLookAndFeel = True
                btnGetResults.Text = "Get Results"
                Dim rIndex() As Integer = gvTemplateList.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dr As DataRow = gvTemplateList.GetRow(rIndex(0)).Row
                    If Row("TemplateID") = dr("TemplateID") Then
                        'TODO
                    End If
                End If
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub btnParamAdd_Click(sender As Object, e As EventArgs) Handles btnParamAdd.Click
        Try
            If cmbVendor.SelectedIndex = 0 Then
                SetMessage("Please select Vendor")
                Exit Sub
            End If

            Dim moName As String = Nothing
            Dim templateMOConfigID As Integer = Nothing

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objRefChkAddParam As New dlgRefChkAddParam()
            objRefChkAddParam.vendor = cmbVendor.SelectedItem.ToString
            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                objRefChkAddParam.templateID = dr("TemplateID")
                objRefChkAddParam.templateName = dr("TemplateName")
            End If

            Dim drMOConfig As DataRow = gvMOConfig.GetFocusedDataRow()
            If drMOConfig IsNot Nothing Then
                templateMOConfigID = drMOConfig("TemplateMOConfigID")
                moName = drMOConfig("MOName")
                objRefChkAddParam.templateMOConfigID = drMOConfig("TemplateMOConfigID")
                objRefChkAddParam.moName = drMOConfig("MOName")
                objRefChkAddParam.moTableName = drMOConfig("MOTable")
                objRefChkAddParam.moDatabaseName = drMOConfig("MODatabase")
            End If
            objRefChkAddParam.ShowDialog()

            Me.Cursor = Cursors.Default
            Application.DoEvents()

            LoadMOParamGrid(templateMOConfigID, moName)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnParamDelete_Click(sender As Object, e As EventArgs) Handles btnParamDelete.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOParam.GetDataRow(gvMOParam.FocusedRowHandle)
            Dim paramToDelete As String = dr("ParamName").ToString

            If XtraMessageBox.Show("Are you sure to delete param: " & paramToDelete & "?", "Delete MO Param", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateMOParamConfigID", dr("TemplateMOParamConfigID")}
                }
                strConnection = GetSQL(4129, parray)(0)
                sqlParam = GetSQL(4129, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                gvMOParam.DeleteRow(gvMOParam.FocusedRowHandle)
                gcMOParam.Refresh()

                'save change log
                Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), "MO Param: " & dr("ParamName").ToString & " deleted from the template", dr("TemplateMOConfigID"), dr("TemplateMOParamConfigID"))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnParamModify_Click(sender As Object, e As EventArgs) Handles btnParamModify.Click
        Try
            If cmbVendor.SelectedIndex = 0 Then
                SetMessage("Please select Vendor")
                Exit Sub
            End If

            Dim moName As String = Nothing
            Dim templateMOConfigID As Integer = Nothing

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objRefChkModifyParam As New dlgRefChkModifyParam()
            objRefChkModifyParam.vendor = cmbVendor.SelectedItem.ToString
            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                objRefChkModifyParam.templateID = dr("TemplateID")
                objRefChkModifyParam.templateName = dr("TemplateName")
            End If

            Dim drMOConfig As DataRow = gvMOConfig.GetFocusedDataRow()
            If drMOConfig IsNot Nothing Then
                templateMOConfigID = drMOConfig("TemplateMOConfigID")
                moName = drMOConfig("MOName")
                objRefChkModifyParam.templateMOConfigID = drMOConfig("TemplateMOConfigID")
                objRefChkModifyParam.moName = drMOConfig("MOName")
                objRefChkModifyParam.moTableName = drMOConfig("MOTable")
                objRefChkModifyParam.moDatabaseName = drMOConfig("MODatabase")
            End If

            Dim drMOParam As DataRow = gvMOParam.GetFocusedDataRow()
            If drMOParam IsNot Nothing Then
                objRefChkModifyParam.templateMOParamConfigID = drMOParam("TemplateMOParamConfigID")
                objRefChkModifyParam.paramName = drMOParam("ParamName")
                objRefChkModifyParam.ceIsEnabled.Checked = CBool(drMOParam("IsActive"))
                objRefChkModifyParam.ceSetAutoValue.Checked = IIf(IsDBNull(drMOParam("IsAutoSetValue")), False, CBool(drMOParam("IsAutoSetValue")))
                objRefChkModifyParam.txtCommonalityValue.Text = IIf(IsDBNull(drMOParam("CommonalityValue")), "", drMOParam("CommonalityValue"))
                objRefChkModifyParam.cmbOperator.Text = IIf(IsDBNull(drMOParam("Operator")), "", drMOParam("Operator"))

                Dim isVariable As Boolean = False
                If IsDBNull(drMOParam("IsVariable")) = True Then
                    isVariable = False
                Else
                    isVariable = CBool(drMOParam("IsVariable"))
                End If

                objRefChkModifyParam.LoadMatchVariableCombo()

                If isVariable = False Then
                    objRefChkModifyParam.txtParamValues.Text = IIf(IsDBNull(drMOParam("Value")), "", drMOParam("Value"))
                ElseIf isVariable = True Then
                    objRefChkModifyParam.cmbMatchVariable.Text = IIf(IsDBNull(drMOParam("Value")), "", drMOParam("Value").ToString)
                End If
            End If
            objRefChkModifyParam.ShowDialog()

            Me.Cursor = Cursors.Default
            Application.DoEvents()
            'save change log
            Me.SaveChangeLog(CInt(dr("TemplateID")), drMOConfig("MOName").ToString, CInt(drMOConfig("TemplateMOConfigID")), "MO Param: " & drMOParam("ParamName") & " under MO: " & drMOConfig("MOName").ToString & " modified")
            LoadMOParamGrid(templateMOConfigID, moName)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnMOConfigAdd_Click(sender As Object, e As EventArgs) Handles btnMOConfigAdd.Click
        Try
            If cmbVendor.SelectedIndex = 0 Then
                SetMessage("Please select Vendor")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objAddMO As New dlgRefChkAddMO()
            objAddMO.vendor = cmbVendor.SelectedItem.ToString

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                objAddMO.templateID = dr("TemplateID")
                objAddMO.templateName = dr("TemplateName")
            End If
            objAddMO.ShowDialog()

            Me.Cursor = Cursors.Default
            Application.DoEvents()

            ' Refresh MO Config grid
            LoadMOConfigGrid(dr("TemplateID"))

            ' Refresh mo param grid
            Dim drMoConfig As DataRow = gvMOConfig.GetFocusedDataRow()
            If drMoConfig IsNot Nothing Then
                LoadMOParamGrid(drMoConfig("TemplateMOConfigID"), drMoConfig("MOName"))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnMOConfigDelete_Click(sender As Object, e As EventArgs) Handles btnMOConfigDelete.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetDataRow(gvMOConfig.FocusedRowHandle)
            If XtraMessageBox.Show("Are you sure to delete mo: " & dr("MOName").ToString & "?", "Delete MO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateMOConfigID", dr("TemplateMOConfigID")}
                }
                strConnection = GetSQL(4114, parray)(0)
                sqlParam = GetSQL(4114, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                gvMOConfig.DeleteRow(gvMOConfig.FocusedRowHandle)

                'save change log
                Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(dr("MOName")), CInt(dr("TemplateMOConfigID")), "MO: " & dr("MOName").ToString & " deleted from the template")
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnMOConfigClone_Click(sender As Object, e As EventArgs) Handles btnMOConfigClone.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim templateID As Integer = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
            Dim moConfigID As Integer = CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID"))
            Dim moConfigClone As String = CStr(gvMOConfig.GetFocusedRowCellValue("MOName"))

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateID", templateID},
                New String() {"@TemplateMOConfigID", moConfigID}
            }
            strConnection = GetSQL(4182, parray)(0)
            sqlParam = GetSQL(4182, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            If dt IsNot Nothing Then
                XtraMessageBox.Show("Clone of MO: " & moConfigClone & " created successfully", "Create MO Clone", MessageBoxButtons.OK, MessageBoxIcon.Information)

                RemoveHandler gvMOConfig.FocusedRowChanged, AddressOf gvMOConfig_FocusedRowChanged
                'reload MO config grid and select new mo config clone
                LoadMOConfigGrid(templateID)
                AddHandler gvMOConfig.FocusedRowChanged, AddressOf gvMOConfig_FocusedRowChanged

                gvMOConfig.FocusedRowHandle = gvMOConfig.LocateByValue("TemplateMOConfigID", CInt(dt.Rows(0)("NewMOConfigID")))
                gvMOConfig_FocusedRowChanged(Nothing, Nothing)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (cmbVendor.SelectedIndex = 0) Then
                XtraMessageBox.Show("Please select Vendor", "CIOS - Ref Check", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbVendor.Focus()
                Return
            End If
            objGenerateTemplate = New frmGenerateTemplate()
            objGenerateTemplate.templateID = CInt(gvTemplateList.GetFocusedDataRow()("TemplateID"))
            objGenerateTemplate.templateName = gvTemplateList.GetFocusedDataRow()("TemplateName")
            objGenerateTemplate.vendorName = cmbVendor.SelectedItem.ToString
            objGenerateTemplate.ShowDialog()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            gvTemplateList_FocusedRowChanged(Nothing, Nothing)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub xtcResultsInner_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcResultsInner.SelectedPageChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadResultsTabGrids()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub xtcMainOuter_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcMainOuter.SelectedPageChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbVendor.SelectedIndex <> 0 Then
                If xtcMainOuter.SelectedTabPageIndex = 0 Then
                    If gcMOConfig.DataSource Is Nothing Then
                        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
                        LoadMOConfigGrid(dr("TemplateID"))
                    End If
                ElseIf xtcMainOuter.SelectedTabPageIndex = 1 Then
                    LoadResultsTabGrids()
                ElseIf xtcMainOuter.SelectedTabPageIndex = 2 Then
                    LoadViewTemplateGrid()
                ElseIf xtcMainOuter.SelectedTabPageIndex = 3 Then
                    LoadViewChangeLogGrid()
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

    Private Sub riChkMOConfigAllParameters_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            If chkBox IsNot Nothing Then
                UpdateMOConfigIsAllPArameters(chkBox.CheckState)
                UpdateTemplateLatestConfig()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riChkMOConfigActive_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            If chkBox IsNot Nothing Then
                UpdateMOConfigIsActive(chkBox.CheckState)
                UpdateTemplateLatestConfig()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riChkMOConfigAutoSetValue_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            If chkBox IsNot Nothing Then
                UpdateMOConfigIsAutoSetValue(chkBox.CheckState)
                UpdateTemplateLatestConfig()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riChkMOParamAutoSetValue_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            Dim focusedRow As DataRowView = gvMOParam.GetFocusedRow()
            If chkBox IsNot Nothing AndAlso focusedRow IsNot Nothing Then
                UpdateMOParamAutoSetValue(CInt(focusedRow("TemplateMOParamConfigID")), chkBox.CheckState)
                UpdateTemplateLatestConfig()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riChkMOParamActive_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            Dim focusedRow As DataRowView = gvMOParam.GetFocusedRow()
            If chkBox IsNot Nothing AndAlso focusedRow IsNot Nothing Then
                UpdateMOParamIsActive(CInt(focusedRow("TemplateMOParamConfigID")), chkBox.CheckState)
                UpdateTemplateLatestConfig()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riChkMOParamConditionActive_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            Dim focusedRow As DataRowView = gvMOParam.GetFocusedRow()
            If chkBox IsNot Nothing AndAlso focusedRow IsNot Nothing Then
                UpdateMOParamIsConditionActive(CInt(focusedRow("TemplateMOParamConfigID")), chkBox.CheckState)
                UpdateTemplateLatestConfig()

                If chkBox.CheckState = CheckState.Checked Then
                    grpCtrlConditions.Enabled = True
                Else
                    grpCtrlConditions.Enabled = False
                End If
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvMOParam_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs)
        If e.Value.ToString.ToLower = "select operator" Then
            e.Valid = False
        Else
            e.Valid = True
        End If
    End Sub

    Private Sub btnConditionAdd_Click(sender As Object, e As EventArgs) Handles btnConditionAdd.Click
        Try
            OpenAddConditionDialog()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnConditionDelete_Click(sender As Object, e As EventArgs) Handles btnConditionDelete.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (gvCondition.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvCondition.FocusedRowHandle
                Dim conditionString As String = gvCondition.GetRowCellValue(selectedRowHandle, "ConditionString")
                Dim conditionID As Integer = gvCondition.GetRowCellValue(selectedRowHandle, "ConditionID")
                If XtraMessageBox.Show("Are you sure to delete condition: " & conditionString & "?", "Delete Condition", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteCondition(conditionID)
                    gvCondition.DeleteRow(selectedRowHandle)
                    If gvCondition.RowCount > 0 Then
                        gvCondition.SelectRow(0)
                    End If
                    gcCondition.Refresh()
                End If
                'save change log
                Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(gvMOConfig.GetFocusedRowCellValue("MOName")), CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), conditionString & " deleted from the template")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDump2Csv_Click(sender As Object, e As EventArgs) Handles btnDump2Csv.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtExport As New DataTable
            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Comma Delimited|*.csv"
            objFileDlg.Title = "Save a CSV File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Exporting to CSV...")
                    Application.DoEvents()

                    If gvTemplateList.GetFocusedDataRow() IsNot Nothing Then
                        dtExport = CreateData(0, 0, currViewRowFilter)
                    End If

                    Dim dtTemp As DataTable = GetDataTable2Export(dtExport)
                    IOSDevExpressGrid.DataTable2CSV(dtTemp, objFileDlg.FileName)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnDump2Xls_Click(sender As Object, e As EventArgs) Handles btnDump2Xls.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtExport As New DataTable
            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Excel Workbook |*.xls"
            objFileDlg.Title = "Save an excel File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Exporting to excel...")
                    Application.DoEvents()

                    If gvTemplateList.GetFocusedDataRow() IsNot Nothing Then
                        dtExport = CreateData(0, 0, currViewRowFilter)
                    End If

                    Dim dtTemp As DataTable = GetDataTable2Export(dtExport)
                    IOSDevExpressGrid.DataTable2Excel(dtTemp, objFileDlg.FileName)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gvTemplateList.RowCellStyle
        Try
            If e.RowHandle > -1 And e.Column.FieldName = "LastStatus" Then
                If e.CellValue.ToString = "Idle" Then
                    e.Appearance.BackColor = Color.Wheat
                ElseIf e.CellValue.ToString = "Running" Then
                    e.Appearance.BackColor = Color.YellowGreen
                ElseIf e.CellValue.ToString = "Error" Then
                    e.Appearance.BackColor = Color.OrangeRed
                End If

                e.Appearance.BackColor2 = Color.White
                e.Appearance.ForeColor = Color.Black
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub chart_Click(sender As Object, e As MouseEventArgs) Handles chart1.Click, chart2.Click, chart3.Click
        Try
            If e.Button = MouseButtons.Left Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim myChart As Chart = CType(sender, Chart)
                Dim chartName As String = myChart.Name
                Dim hit As HitTestInfo = myChart.HitTest(e.X, e.Y)

                If (myChart IsNot Nothing) Then
                    Try
                        hit = myChart.HitTest()
                    Catch ex As Exception
                    End Try

                    If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                        Dim el As Element = CType(hit.Object, Element)
                        Dim clickedObject As String = Nothing

                        'chart 1 - highlight selected bar & draw chart 3 for clicked bar 
                        If chartName = "chart1" Then
                            chart1.XAxis.Markers.Clear()
                            clickedObject = CType(el.Name, String)
                            ClearChart3()
                            AddAxisMarkerX(chart1, clickedObject)
                            LoadResultChart3(clickedObject)
                        End If

                        'chart 2 - highlight selected bar & load grid for selected object
                        If chartName = "chart2" Then
                            chart2.XAxis.Markers.Clear()
                            clickedObject = CType(el.Name, String)
                            AddAxisMarkerX(chart2, clickedObject)
                            LoadChart2Grid(clickedObject)
                        End If

                        'chart 3 - highlight selected bar & load grid for selected object 
                        If chartName = "chart3" Then
                            chart3.XAxis.Markers.Clear()
                            clickedObject = CType(el.Name, String)
                            AddAxisMarkerX(chart3, clickedObject)
                            LoadChart3Grid(clickedObject)
                        End If

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

    Private Sub btnListMngr_Click(sender As Object, e As EventArgs) Handles btnListMngr.Click
        frmListManager.Show()
    End Sub

    Private Sub gvCondition_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvCondition.CellValueChanged
        Try
            Dim data As DataRow = gvCondition.GetFocusedDataRow()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If e.Column.FieldName.ToUpper = "PARAMSETVALUE" Then

                If data IsNot Nothing Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@conditionID", CInt(data.Item("ConditionID"))},
                        New String() {"@paramSetValue", Chr(39) & e.Value.ToString.Replace("'", "''") & Chr(39)}
                    }
                    strConnection = GetSQL(4164, parray)(0)
                    sqlParam = GetSQL(4164, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                End If
                'save change log
                Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(gvMOConfig.GetFocusedRowCellValue("MOName")), CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), e.Column.FieldName & " modified for the condition in the MO: " & CStr(gvMOConfig.GetFocusedRowCellValue("MOName")))

            ElseIf e.Column.FieldName.ToUpper = "CONDITIONSTRING" Then

                If data IsNot Nothing Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@conditionID", CInt(data.Item("ConditionID"))},
                        New String() {"@conditionString", Chr(39) & e.Value.ToString.Replace("'", "''") & Chr(39)}
                    }
                    strConnection = GetSQL(4173, parray)(0)
                    sqlParam = GetSQL(4173, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                End If
                'save change log
                Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(gvMOConfig.GetFocusedRowCellValue("MOName")), CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), e.Column.FieldName & " modified to " & e.Value.ToString & " for the MO: " & CStr(gvMOConfig.GetFocusedRowCellValue("MOName")))

            End If
            gvMOParam_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvCondition_ShowingEditor(sender As Object, e As CancelEventArgs) Handles gvCondition.ShowingEditor
        Try
            If (gvCondition.FocusedColumn().FieldName = "ParamSetValue") Or (gvCondition.FocusedColumn().FieldName = "ConditionString") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnParamExclusionAdd_Click(sender As Object, e As EventArgs) Handles btnParamExclusionAdd.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            paramToExclude = Nothing
            Dim objExcludeParam As New dlgExcludeParam()
            objExcludeParam.templateID = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
            objExcludeParam.vendor = cmbVendor.SelectedItem.ToString
            objExcludeParam.moTable = gvMOConfig.GetFocusedRowCellValue("MOTable").ToString
            objExcludeParam.ShowDialog()

            If paramToExclude <> "" Then
                LoadExcludedParamList(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")))
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub lstParamExclusion_KeyUp(sender As Object, e As KeyEventArgs) Handles lstParamExclusion.KeyUp
        If e.KeyCode = Keys.Delete Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                DeleteParamExlusion()
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            End Try
        End If
    End Sub

    Private Sub btnParamExclusionDelete_Click(sender As Object, e As EventArgs) Handles btnParamExclusionDelete.Click
        If lstParamExclusion.SelectedIndex > -1 Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                DeleteParamExlusion()
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            End Try
        End If
    End Sub

    Private Sub gvMOFilter_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvMOFilter.ShowingEditor
        Try
            If (gvMOFilter.FocusedColumn().FieldName = "FilterString") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvMOFilter_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvMOFilter.CellValueChanged
        Try
            Dim modifiedFilterStr As String = Nothing
            If e.Column.FieldName.ToUpper = "FILTERSTRING" Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim data As DataRow = gvMOFilter.GetFocusedDataRow()
                If data IsNot Nothing Then

                    If data.Item("FilterString").ToString.ToLower.Contains("in") Or data.Item("FilterString").ToString.ToLower.Contains("not in") Then
                        modifiedFilterStr = data.Item("FilterString").ToString '.Replace("'", "''")
                    Else
                        modifiedFilterStr = data.Item("FilterString").ToString
                    End If

                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@TemplateMOFilterID", CInt(data.Item("TemplateMOFilterID"))},
                        New String() {"@FilterString", Chr(39) & Replace(modifiedFilterStr, Chr(39), Chr(39) & Chr(39)) & Chr(39)}
                    }
                    strConnection = GetSQL(4172, parray)(0)
                    sqlParam = GetSQL(4172, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    'save change log
                    Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(gvMOConfig.GetFocusedRowCellValue("MOName")), CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), "Filter string modified for the mo: " & gvMOConfig.GetFocusedRowCellValue("MOName").ToString)
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

    Private Sub btnParamExcCopyFromTemplate_Click(sender As Object, e As EventArgs) Handles btnParamExcCopyFromTemplate.Click
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()

            If cmbVendor.SelectedIndex > 0 Then
                Dim templateID As Integer = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
                Dim objParamExcCopyTemplate As New dlgParamExcCopyTemplate()
                objParamExcCopyTemplate.templateIDCopyTo = templateID
                objParamExcCopyTemplate.ShowDialog()
                LoadExcludedParamList(templateID)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            'Me.Cursor = Cursors.Default
            'Application.DoEvents()
        End Try
    End Sub

    Private Sub gvMOConfig_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs)
        If e.Value.ToString.ToLower = "select priority" Then
            e.Valid = False
        Else
            e.Valid = True
        End If
    End Sub

    Private Sub gvMOConfig_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        Try
            If e.Column.FieldName = "Priority" Then
                e.RepositoryItem = riCmbPriority
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub riCmbPriority_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cmbPrio As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
            If cmbPrio.SelectedIndex <> 0 Then
                Dim moConfigID As Integer = CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID"))
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@templateMOConfigID", moConfigID},
                    New String() {"@priority", Chr(39) & cmbPrio.SelectedItem.ToString & Chr(39)}
                }
                strConnection = GetSQL(4197, parray)(0)
                sqlParam = GetSQL(4197, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub riChkMOConfigChkMissingNE_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            If chkBox IsNot Nothing Then
                UpdateMOConfigCheckMissingNE(chkBox.CheckState)
                UpdateTemplateLatestConfig()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim objRCTemplateObj As New frmRefChkUpdateTemplateObj()
            objRCTemplateObj.templateID = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
            If txtDescription.Text <> String.Empty Then
                objRCTemplateObj.objectName = txtDescription.Text.Trim.Split("=")(1).ToString
            End If
            objRCTemplateObj.ShowDialog()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnCopyFromFilter_Click(sender As Object, e As EventArgs) Handles btnCopyFromFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objCopyFromTemplate As New frmCopyFromTemplate()
            objCopyFromTemplate.vendorName = cmbVendor.SelectedItem.ToString
            objCopyFromTemplate.ShowDialog()

            If RefCheckCopyFromCommitted = True Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@SrcTemplateID", Me.copyFromSrcTemplateID},
                    New String() {"@TrgTemplateID", CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))},
                    New String() {"@SrcTemplateMOConfigID", Me.copyFromSrcTemplateMOConfigID},
                    New String() {"@TrgTemplateMOConfigID", CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID"))},
                    New String() {"@ObjStaticFilter", IIf(Me.copyFilterStringsFromMO = True, 1, "NULL")},
                    New String() {"@InclusionList", IIf(Me.copyInclusionListFromMO = True, 1, "NULL")},
                    New String() {"@ExclusionList", IIf(Me.copyExclusionListFromMO = True, 1, "NULL")},
                    New String() {"@ParamExlusionFilter", IIf(Me.copyParamExclusionListFromTemplate = True, 1, "NULL")}
                }
                strConnection = GetSQL(4202, parray)(0)
                sqlParam = GetSQL(4202, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                If copyFilterStringsFromMO = True Then
                    Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
                    LoadMOFilterGrid(CInt(dr("TemplateMOConfigID")))
                End If

                LoadFilterLists(CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")))

                If copyParamExclusionListFromTemplate = True Then
                    Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
                    LoadExcludedParamList(CInt(dr("TemplateID")))
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCopyParamToTemplate_Click(sender As Object, e As EventArgs) Handles btnCopyParamToTemplate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()

            Dim objCopyParam2Template As New frmCopyParam2Template()
            objCopyParam2Template.CopyType = "param2mo"
            objCopyParam2Template.templateIDCopyFrom = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
            objCopyParam2Template.templateMOConfigID = CInt(gvMOParam.GetFocusedRowCellValue("TemplateMOConfigID"))
            objCopyParam2Template.MOName = CStr(gvMOConfig.GetFocusedRowCellValue("MOName"))
            objCopyParam2Template.ShowDialog()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            'Me.Cursor = Cursors.Default
            'Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCopyMOToTemplate_Click(sender As Object, e As EventArgs) Handles btnCopyMOToTemplate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()

            Dim objCopyParam2Template As New frmCopyParam2Template()
            objCopyParam2Template.CopyType = "mo2template"
            objCopyParam2Template.vendor = cmbVendor.SelectedItem.ToString
            objCopyParam2Template.templateIDCopyFrom = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
            objCopyParam2Template.ShowDialog()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            'Me.Cursor = Cursors.Default
            'Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbSearch_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbSearch.SelectedIndex > 0 Then

                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                If cmbSearch.SelectedIndex = 1 Then
                    'Filter String
                    LoadAllFilterStrings()
                    btnBulkAdd.Visible = False
                    btnBulkUpdate.Text = "Bulk Update"
                ElseIf cmbSearch.SelectedIndex = 2 Then
                    'Object Inc/Exc 
                    LoadAllIncExcObjects()
                    btnBulkAdd.Visible = False
                    btnBulkUpdate.Text = "Bulk Add"
                ElseIf cmbSearch.SelectedIndex = 3 Then
                    'Param Exclusion
                    LoadAllParamExclusion()
                    btnBulkAdd.Visible = False
                    btnBulkUpdate.Text = "Bulk Add"
                End If
            Else
                btnBulkAdd.Visible = False
                IOSDevExpressGrid.ClearGrid(gcSearch)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnBulkAdd_Click(sender As Object, e As EventArgs) Handles btnBulkAdd.Click
        'Try

        '    Me.Cursor = Cursors.WaitCursor
        '    Application.DoEvents()

        'cmbSearch_SelectedIndexChanged(Nothing, Nothing)
        'gvSearch.ActiveFilterString = objRefChkBulkUpdate.strSearchGridFilter
        'objRefChkBulkUpdate = Nothing

        'Catch ex As Exception
        '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        '    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        '    Application.DoEvents()
        'End Try
    End Sub

    Private Sub btnBulkUpdate_Click(sender As Object, e As EventArgs) Handles btnBulkUpdate.Click
        Try
            Dim objRefChkBulkUpdate As New frmRefChkBulkUpdate()
            If gvSearch.ActiveFilterString <> "" Then

                objRefChkBulkUpdate.strSearchGridFilter = gvSearch.ActiveFilterString

                If cmbSearch.SelectedIndex = 1 Then
                    objRefChkBulkUpdate.itemType = "FilterString"
                ElseIf cmbSearch.SelectedIndex = 2 Then
                    objRefChkBulkUpdate.itemType = "IncExcObject"
                ElseIf cmbSearch.SelectedIndex = 3 Then
                    objRefChkBulkUpdate.itemType = "ExclusionParam"
                    objRefChkBulkUpdate.vendor = cmbVendor.SelectedItem.ToString()
                End If

                objRefChkBulkUpdate.ShowDialog()
            Else
                SetMessage("Please Filter Search Grid")
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            cmbSearch_SelectedIndexChanged(Nothing, Nothing)
            gvSearch.ActiveFilterString = objRefChkBulkUpdate.strSearchGridFilter
            objRefChkBulkUpdate = Nothing
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnBulkDelete_Click(sender As Object, e As EventArgs) Handles btnBulkDelete.Click
        Try
            Dim objRefChkBulkDelete As New frmRefChkBulkDelete()
            If gvSearch.ActiveFilterString <> "" Then

                objRefChkBulkDelete.strSearchGridFilter = gvSearch.ActiveFilterString

                If cmbSearch.SelectedIndex = 1 Then
                    objRefChkBulkDelete.itemType = "FilterString"
                ElseIf cmbSearch.SelectedIndex = 2 Then
                    objRefChkBulkDelete.itemType = "IncExcObject"
                ElseIf cmbSearch.SelectedIndex = 3 Then
                    objRefChkBulkDelete.itemType = "ExclusionParam"
                End If

                objRefChkBulkDelete.ShowDialog()
            Else
                SetMessage("Please Filter Search Grid")
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            cmbSearch_SelectedIndexChanged(Nothing, Nothing)
            gvSearch.ActiveFilterString = objRefChkBulkDelete.strSearchGridFilter
            objRefChkBulkDelete = Nothing
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRunSelected_Click(sender As Object, e As EventArgs) Handles btnRunSelected.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If cmbVendor.SelectedIndex > 0 Then
                If gvMOConfig.RowCount > 0 Then

                    ucProgPnlMO.StartProgress()
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()

                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString & Chr(39)},
                        New String() {"@TemplateID", CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))},
                        New String() {"@MoConfigID", CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID"))}
                    }
                    strConnection = GetSQL(4213, parray)(0)
                    sqlParam = GetSQL(4213, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                End If
            End If
        Catch ex As Exception
            ucProgPnlMO.StopProgress()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            ucProgPnlMO.StopProgress()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Context Menu"

    Private Sub tsmiAddCondition_Click(sender As Object, e As EventArgs) Handles tsmiAddCondition.Click
        Try
            OpenAddConditionDialog()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmsParamConfig_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsParamConfig.Opening
        Try
            Dim isPowerUser As Boolean = False
            If lblTemplateOwner.Text.ToLower <> Environment.UserName.ToLower Then
                If configMgr.User.IsPowerUser = True Then
                    isPowerUser = True
                Else
                    isPowerUser = False
                End If
            Else
                isPowerUser = True
            End If

            tsmiSetAllActive.Enabled = isPowerUser
            tsmiSetAllAutoSetValue.Enabled = isPowerUser
            tsmiSetAllIsConditionActive.Enabled = isPowerUser
            tsmiAddCondition.Enabled = isPowerUser

            If isPowerUser = False Then
                Exit Sub
            End If

            Dim grd As GridControl = CType(cmsParamConfig.SourceControl, GridControl)
            Dim dt As DataTable = grd.DataSource

            If dt.Rows.Count = 0 Then
                tsmiSetAllActive.Enabled = False
                tsmiSetAllAutoSetValue.Enabled = False
                tsmiSetAllIsConditionActive.Enabled = False
                tsmiAddCondition.Enabled = False
                tsmi_DeleteSelectedParams.Enabled = False
            Else
                tsmiSetAllActive.Enabled = True
                tsmiSetAllAutoSetValue.Enabled = True
                tsmiSetAllIsConditionActive.Enabled = True
                tsmiAddCondition.Enabled = True
                tsmi_DeleteSelectedParams.Enabled = True

                Dim dr As DataRow = gvMOParam.GetDataRow(gvMOParam.FocusedRowHandle)
                If dr("IsActive") = True Then
                    tsmiSetAllActive.Text = "Set All Active Off"
                Else
                    tsmiSetAllActive.Text = "Set All Active On"
                End If
                If dr("IsAutoSetValue") = True Then
                    tsmiSetAllAutoSetValue.Text = "Set All AutoSetValue Off"
                Else
                    tsmiSetAllAutoSetValue.Text = "Set All AutoSetValue On"
                End If
                If dr("IsConditionActive") = True Then
                    tsmiSetAllIsConditionActive.Text = "Set All IsConditionActive Off"
                Else
                    tsmiSetAllIsConditionActive.Text = "Set All IsConditionActive On"
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiSetAllActive_Click(sender As Object, e As EventArgs) Handles tsmiSetAllActive.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOParam.GetDataRow(gvMOParam.FocusedRowHandle)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", dr("TemplateMOConfigID")},
                New String() {"@IsActive", IIf(dr("IsActive"), "'0'", "'1'")}
            }
            strConnection = GetSQL(4115, parray)(0)
            sqlParam = GetSQL(4115, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            LoadMOParamGrid(dr("TemplateMOConfigID"), dr("MOName"))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiSetAllAutoSetValue_Click(sender As Object, e As EventArgs) Handles tsmiSetAllAutoSetValue.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOParam.GetDataRow(gvMOParam.FocusedRowHandle)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", dr("TemplateMOConfigID")},
                New String() {"@IsAutoSetValue", IIf(dr("IsAutoSetValue"), "'0'", "'1'")}
            }
            strConnection = GetSQL(4116, parray)(0)
            sqlParam = GetSQL(4116, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            LoadMOParamGrid(dr("TemplateMOConfigID"), dr("MOName"))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiSetAllIsConditionActive_Click(sender As Object, e As EventArgs) Handles tsmiSetAllIsConditionActive.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOParam.GetDataRow(gvMOParam.FocusedRowHandle)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", dr("TemplateMOConfigID")},
                New String() {"@IsConditionActive", IIf(dr("IsConditionActive"), "'0'", "'1'")}
            }
            strConnection = GetSQL(4117, parray)(0)
            sqlParam = GetSQL(4117, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            LoadMOParamGrid(dr("TemplateMOConfigID"), dr("MOName"))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiDeleteCondition_Click(sender As Object, e As EventArgs)
        Try
            'Dim tn As TreeListViewNode = tlvCondition.SelectedNode
            'Dim str = tn.SubItems(0).Text
            'Dim abc As String = tlvCondition.Nodes(0).SubItems(1).Text

            ''execute sp_CM_Template_MOParam_ConditionDelete @TemplateMOParamConfigID, @ParamName, @ConditionString

            'tlvCondition.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmsMOConfig_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsMOConfig.Opening
        Try
            Dim isPowerUser As Boolean = False
            If lblTemplateOwner.Text.ToLower <> Environment.UserName.ToLower Then
                If configMgr.User.IsPowerUser = True Then
                    isPowerUser = True
                Else
                    isPowerUser = False
                End If
            Else
                isPowerUser = True
            End If

            tsmiSetAllActive_MOConfig.Enabled = isPowerUser
            tsmiSetAllAutoSetValue_MOConfig.Enabled = isPowerUser
            tsmiSetAllParameters.Enabled = isPowerUser
            tsmiDeleteMO.Enabled = isPowerUser

            If isPowerUser = False Then
                Exit Sub
            End If

            Dim grd As GridControl = CType(cmsMOConfig.SourceControl, GridControl)
            Dim dt As DataTable = grd.DataSource

            If dt.Rows.Count = 0 Then
                tsmiSetAllActive_MOConfig.Enabled = False
                tsmiSetAllAutoSetValue_MOConfig.Enabled = False
                tsmiSetAllParameters.Enabled = False
                tsmiDeleteMO.Enabled = False
            Else
                tsmiSetAllActive_MOConfig.Enabled = True
                tsmiSetAllAutoSetValue_MOConfig.Enabled = True
                tsmiSetAllParameters.Enabled = True
                tsmiDeleteMO.Enabled = True

                Dim dr As DataRow = gvMOConfig.GetDataRow(gvMOConfig.FocusedRowHandle)
                If dr("IsActive") = True Then
                    tsmiSetAllActive_MOConfig.Text = "Set All Active Off"
                Else
                    tsmiSetAllActive_MOConfig.Text = "Set All Active On"
                End If
                If dr("IsAutoSetValue") = True Then
                    tsmiSetAllAutoSetValue_MOConfig.Text = "Set All AutoSetValue Off"
                Else
                    tsmiSetAllAutoSetValue_MOConfig.Text = "Set All AutoSetValue On"
                End If
                If dr("IsAllParameters") = True Then
                    tsmiSetAllParameters.Text = "Set All AllParameters Off"
                Else
                    tsmiSetAllParameters.Text = "Set All AllParameters On"
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiSetAllActive_MOConfig_Click(sender As Object, e As EventArgs) Handles tsmiSetAllActive_MOConfig.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetDataRow(gvMOConfig.FocusedRowHandle)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateID", dr("TemplateID")},
                New String() {"@IsActive", IIf(dr("IsActive"), "'0'", "'1'")}
            }
            strConnection = GetSQL(4111, parray)(0)
            sqlParam = GetSQL(4111, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            LoadMOConfigGrid(dr("TemplateID"))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiSetAllAutoSetValue_MOConfig_Click(sender As Object, e As EventArgs) Handles tsmiSetAllAutoSetValue_MOConfig.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetDataRow(gvMOConfig.FocusedRowHandle)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateID", dr("TemplateID")},
                New String() {"@IsAutoSetValue", IIf(dr("IsAutoSetValue"), "'0'", "'1'")}
            }
            strConnection = GetSQL(4113, parray)(0)
            sqlParam = GetSQL(4113, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            LoadMOConfigGrid(dr("TemplateID"))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiSetAllParameters_Click(sender As Object, e As EventArgs) Handles tsmiSetAllParameters.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetDataRow(gvMOConfig.FocusedRowHandle)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateID", dr("TemplateID")},
                New String() {"@IsAllParameters", IIf(dr("IsAllParameters"), "'0'", "'1'")}
            }
            strConnection = GetSQL(4112, parray)(0)
            sqlParam = GetSQL(4112, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            LoadMOConfigGrid(dr("TemplateID"))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiDeleteMO_Click(sender As Object, e As EventArgs) Handles tsmiDeleteMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetDataRow(gvMOConfig.FocusedRowHandle)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", dr("TemplateMOConfigID")}
            }
            strConnection = GetSQL(4114, parray)(0)
            sqlParam = GetSQL(4114, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            gvMOConfig.DeleteRow(gvMOConfig.FocusedRowHandle)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmsGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsGrid.Opening
        Try
            Dim dgv As GridControl = CType(sender.sourcecontrol, GridControl)
            tsmi_RecordCount.Text = "Record Count: " & TryCast(dgv.DefaultView, GridView).SelectedRowsCount
            cmsGridControlName = dgv.Name

            If cmsGridControlName = "gcTemplateSumm" OrElse cmsGridControlName = "gcInconSumm" OrElse cmsGridControlName = "gcViewTemplate" OrElse cmsGridControlName = "gcStatus" Then
                tsmiCopyFilteredToClipboard.Enabled = True
                tsmiCopyFilteredToExcel.Enabled = True

                'If dgv.Name = "gcInconSumm" Then
                '    If CType(dgv.MainView, GridView).FocusedColumn.FieldName = "ParameterValue" Then
                '        tsmi_UpdateTemplateWithSelectedValue.Enabled = True
                '    Else
                '        tsmi_UpdateTemplateWithSelectedValue.Enabled = False
                '    End If
                'End If

            Else
                If Not String.IsNullOrEmpty(currViewRowFilter) Then
                    tsmiCopyFilteredToClipboard.Enabled = True
                    tsmiCopyFilteredToExcel.Enabled = True
                Else
                    tsmiCopyFilteredToClipboard.Enabled = False
                    tsmiCopyFilteredToExcel.Enabled = False
                End If
            End If

            If dgv.DefaultView.RowCount = 0 Then
                tsmi_AddSelectionToXMLJob.Enabled = False
            Else
                tsmi_AddSelectionToXMLJob.Enabled = True
            End If

            tsmi_AddSelectionToXMLJob.DropDownItems.Clear()
            'Add tsmi to add new manual tilt campaign
            Dim tsmi_Add2NewXmlJob As ToolStripMenuItem = New ToolStripMenuItem("Add to New XML Job")
            AddHandler tsmi_Add2NewXmlJob.Click, AddressOf tsmi_Add2NewXmlJob_Click
            tsmi_AddSelectionToXMLJob.DropDownItems.Add(tsmi_Add2NewXmlJob)

            tsmi_AddSelectionToXMLJob.DropDownItems.Add(New ToolStripSeparator())

            Dim dt As DataTable = Get_XmlJobList_CurrentUser_PowerUser()

            If dt IsNot Nothing Then
                For Each dr As DataRow In dt.Rows
                    Dim tsmi As ToolStripMenuItem = New ToolStripMenuItem(dr("XMLJobName").ToString)
                    tsmi.Tag = dr("XMLJobID").ToString
                    AddHandler tsmi.Click, AddressOf tsmi_XMLJobClick
                    tsmi_AddSelectionToXMLJob.DropDownItems.Add(tsmi)
                Next
            End If

            If cmsGridControlName = "gcStatus" Then
                tsmi_AddParameterToExclusionList.Enabled = False
            Else
                tsmi_AddParameterToExclusionList.Enabled = True
            End If

            If cmsGridControlName = "gcViewTemplate" OrElse cmsGridControlName = "gcChangeLog" OrElse cmsGridControlName = "gcStatus" Then
                tsmi_ParameterMappingCells.Enabled = False
                tsmi_ParameterMappingVoronoi.Enabled = False
                tsmi_ParameterMappingLabel.Enabled = False
                tsmi_ParamDescResultsGrids.Enabled = False
                tsmi_AddParameterToExclusionList.Enabled = False
            Else
                tsmi_ParameterMappingCells.Enabled = True
                tsmi_ParameterMappingVoronoi.Enabled = True
                tsmi_ParameterMappingLabel.Enabled = True
                tsmi_ParamDescResultsGrids.Enabled = True
                tsmi_AddParameterToExclusionList.Enabled = True
            End If

            If cmsGridControlName = "gcViewTemplate" Then
                tsmi_ParamDescResultsGrids.Enabled = True
                tsmi_AddParameterToExclusionList.Enabled = True
            End If

            If cmsGridControlName = "gcInconSumm" Then
                tsmi_ParameterMappingIncon.Enabled = True
            Else
                tsmi_ParameterMappingIncon.Enabled = False
            End If

            ManageGridSelectionType(dgv.MainView)

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiAllowCellCopy_CheckedChanged(sender As Object, e As EventArgs) Handles tsmiAllowCellCopy.CheckedChanged
        Try
            Dim tempGrid As GridControl = frmMapWindow.GetAttachedGrid(sender)
            If tempGrid IsNot Nothing Then
                Dim grdView As GridView = tempGrid.MainView
                ManageGridSelectionType(grdView)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmiCopyAllToCSV_Click(sender As Object, e As EventArgs) Handles tsmiCopyAllToCSV.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Dim tempGrid As GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As GridView = tempGrid.MainView

            If tempGrid.Name = "gcDetailedData" Then
                Using dtTemp As DataTable = CreateData(0, 0)
                    If dtTemp IsNot Nothing Then
                        IOSDevExpressGrid.PopulateDataInGrid(gcTemp, gvTemp, dtTemp, "ALL")
                        ModifyGridData2Export(gcTemp)
                        IOSDevExpressGrid.ExportDataGridToCSV(gcTemp)
                    End If
                End Using
            Else
                ModifyGridData2Export(tempGrid)
                IOSDevExpressGrid.ExportDataGridToCSV(tempGrid)
                xtcResultsInner_SelectedPageChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiCopySelectionWOHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopySelectionWOHeader.Click
        Try
            Dim tempGrid As GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As GridView = tempGrid.MainView

            If gridView.Name = "gvDetailedData" Then
                IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, False)
            Else
                GetGridDataWithHandlingOperator(tempGrid, gridView, False)
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub GetGridDataWithHandlingOperator(ByRef gridCtrl As GridControl, ByRef gridView As GridView, ByVal isHeader As Boolean)
        Dim copiedRows() As Integer = gridView.GetSelectedRows()
        Dim columnCount As Integer = gridView.Columns.Count

        If copiedRows.Count > 0 Then

            For iCntr = 0 To copiedRows.Length - 1
                Dim dr As DataRow = gridView.GetRow(copiedRows(iCntr)).Row
                For jCntr = 0 To columnCount - 1
                    If dr(jCntr).ToString.StartsWith("=") Then
                        dr(jCntr) = "'" & dr(jCntr).ToString
                    End If
                Next
            Next

            If isHeader = True Then
                IOSDevExpressGrid.CopyGridDataToClipBoard(gridCtrl, gridView, False, True)
            Else
                IOSDevExpressGrid.CopyGridDataToClipBoard(gridCtrl, gridView, False, False)
            End If

            For iCntr = 0 To copiedRows.Length - 1
                Dim dr As DataRow = gridView.GetRow(copiedRows(iCntr)).Row
                For jCntr = 0 To columnCount - 1
                    If dr(jCntr).ToString.StartsWith("'=") Then
                        dr(jCntr) = dr(jCntr).ToString.TrimStart("'")
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub tsmiCopySelectionWithHeaders_Click(sender As Object, e As EventArgs) Handles tsmiCopySelectionWithHeader.Click
        Try
            Dim tempGrid As GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As GridView = tempGrid.MainView

            If gridView.Name = "gvDetailedData" Then
                IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, True)
            Else
                GetGridDataWithHandlingOperator(tempGrid, gridView, True)
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmiCopyFilteredToClipboard_Click(sender As Object, e As EventArgs) Handles tsmiCopyFilteredToClipboard.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tempGrid As GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As GridView = tempGrid.MainView

            If tempGrid.Name = "gcDetailedData" Then
                Using dtTemp As DataTable = CreateData(0, 0, currViewRowFilter)
                    If dtTemp IsNot Nothing Then
                        IOSDevExpressGrid.PopulateDataInGrid(tempGrid, gridView, dtTemp, "ALL")
                        IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, True, True)
                    End If
                End Using
            Else
                IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, True, True)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiCopyFilteredToExcel_Click(sender As Object, e As EventArgs) Handles tsmiCopyFilteredToExcel.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tempGrid As GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As GridView = tempGrid.MainView

            If tempGrid.Name = "gcDetailedData" Then
                Using dtTemp As DataTable = CreateData(0, 0, currViewRowFilter)
                    If dtTemp IsNot Nothing Then
                        IOSDevExpressGrid.PopulateDataInGrid(tempGrid, gridView, dtTemp, "ALL")
                        ModifyGridData2Export(tempGrid)
                        IOSDevExpressGrid.ExportDataGridToExcel(tempGrid)
                        IOSDevExpressGrid.PopulateDataInGrid(tempGrid, gridView, dtTemp, "ALL")
                    End If
                End Using
            Else
                ModifyGridData2Export(tempGrid)
                IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, True, True)
                xtcResultsInner_SelectedPageChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_UpdateTemplateWithSelectedValue_Click(sender As Object, e As EventArgs) Handles tsmi_UpdateTemplateWithSelectedValue.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim paramValue As String = Nothing
            Dim templateID As Integer = Nothing

            If gvInconSumm.FocusedColumn.FieldName.ToLower = "parametervalue" Then

                templateID = CInt(gvInconSumm.GetFocusedRowCellValue("TemplateID"))
                paramValue = gvInconSumm.GetFocusedRowCellValue("ParameterName")
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateID", templateID},
                    New String() {"@MoConfigID", CInt(gvInconSumm.GetFocusedRowCellValue("MoConfigID"))},
                    New String() {"@ParamName", Chr(39) & paramValue & Chr(39)},
                    New String() {"@ParamValue", Chr(39) & gvInconSumm.GetFocusedRowCellValue("ParameterValue") & Chr(39)}
                }
                strConnection = GetSQL(4174, parray)(0)
                sqlParam = GetSQL(4174, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                XtraMessageBox.Show("Parameter value: " & paramValue & " has been updated for the template id: " & templateID, "Update Template With Param Value", MessageBoxButtons.OK)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_Go2Configuration_Click(sender As Object, e As EventArgs) Handles tsmi_Go2Configuration.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If xtcResultsInner.SelectedTabPageIndex = 0 Then

                If gvTemplateSumm.GetFocusedDataRow IsNot Nothing Then
                    Dim MOName As String = CStr(gvTemplateSumm.GetFocusedRowCellValue("MO"))
                    gvMOConfig.FocusedRowHandle = gvMOConfig.LocateByValue("MOName", MOName)

                    Dim index As Integer = -1
                    Dim paramName As String = String.Empty
                    While (paramName <> CStr(gvTemplateSumm.GetFocusedRowCellValue("ParameterName")) AndAlso index <> GridControl.InvalidRowHandle)
                        index = gvMOParam.LocateByDisplayText(index + 1, gvMOParam.Columns("MOName"), MOName)
                        paramName = gvMOParam.GetRowCellDisplayText(index, gvMOParam.Columns("ParamName"))
                    End While
                    gvMOParam.FocusedRowHandle = index
                End If

            ElseIf xtcResultsInner.SelectedTabPageIndex = 1 Then

                If gvInconSumm.GetFocusedDataRow IsNot Nothing Then
                    Dim moConfigID As Integer = CInt(gvInconSumm.GetFocusedRowCellValue("MoConfigID"))
                    gvMOConfig.FocusedRowHandle = gvMOConfig.LocateByValue("TemplateMOConfigID", moConfigID)

                    Dim index As Integer = -1
                    Dim paramName As String = String.Empty
                    While (paramName <> CStr(gvInconSumm.GetFocusedRowCellValue("ParameterName")) AndAlso index <> GridControl.InvalidRowHandle)
                        index = gvMOParam.LocateByDisplayText(index + 1, gvMOParam.Columns("TemplateMOConfigID"), moConfigID)
                        paramName = gvMOParam.GetRowCellDisplayText(index, gvMOParam.Columns("ParamName"))
                    End While
                    gvMOParam.FocusedRowHandle = index
                End If

            ElseIf xtcResultsInner.SelectedTabPageIndex = 2 Then

                If gvDetailedData.GetFocusedDataRow IsNot Nothing Then
                    Dim moConfigID As Integer = CInt(gvDetailedData.GetFocusedRowCellValue("MoConfigID"))
                    gvMOConfig.FocusedRowHandle = gvMOConfig.LocateByValue("TemplateMOConfigID", moConfigID)

                    Dim index As Integer = -1
                    Dim paramName As String = String.Empty
                    While (paramName <> CStr(gvDetailedData.GetFocusedRowCellValue("ParameterName")) AndAlso index <> GridControl.InvalidRowHandle)
                        index = gvMOParam.LocateByDisplayText(index + 1, gvMOParam.Columns("TemplateMOConfigID"), moConfigID)
                        paramName = gvMOParam.GetRowCellDisplayText(index, gvMOParam.Columns("ParamName"))
                    End While
                    gvMOParam.FocusedRowHandle = index

                End If

            ElseIf xtcResultsInner.SelectedTabPageIndex = 3 Then

                If gvStatus.GetFocusedDataRow IsNot Nothing Then
                    Dim moConfigID As Integer = CInt(gvStatus.GetFocusedRowCellValue("MOConfigID"))
                    gvMOConfig.FocusedRowHandle = gvMOConfig.LocateByValue("TemplateMOConfigID", moConfigID)
                End If

            End If

            xtcMainOuter.SelectedTabPageIndex = 0
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_AddIncList_Click(sender As Object, e As EventArgs) Handles tsmi_AddIncList.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
            If dr IsNot Nothing Then
                dlgListToAdd.TemplateID = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
                dlgListToAdd.TemplateMOConfigID = CInt(dr("TemplateMOConfigID"))
                dlgListToAdd.MOName = CStr(gvMOConfig.GetFocusedRowCellValue("MOName"))
                dlgListToAdd.MOType = "current"
                dlgListToAdd.FilterType = "Inclusion"
            End If

            If dlgListToAdd.ShowDialog() = DialogResult.OK Then
                FillInclusionList()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_AddIncList2AllMO_Click(sender As Object, e As EventArgs) Handles tsmi_AddIncList2AllMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                dlgListToAdd.TemplateID = CInt(dr("TemplateID"))
                dlgListToAdd.MOName = "ALL"
                dlgListToAdd.MOType = "all"
                dlgListToAdd.FilterType = "Inclusion"
            End If

            If dlgListToAdd.ShowDialog() = DialogResult.OK Then
                FillInclusionList()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_RemoveIncList_Click(sender As Object, e As EventArgs) Handles tsmi_RemoveIncList.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstInclusion.SelectedIndex >= 0 Then
                Dim inclusionFilter As String = lstInclusion.Text
                If XtraMessageBox.Show("Are you sure to remove filter list: " & inclusionFilter & "?", "Remove Template Filter List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteFilterList(CInt(lstInclusion.SelectedValue))
                    Dim dr As DataRow = dtRefChkList.Select("TemplateMOFilterListID=" & CInt(lstInclusion.SelectedValue))(0)
                    dtRefChkList.Rows.Remove(dr)
                    'FillInclusionList()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            FillInclusionList()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_AddExcList_Click(sender As Object, e As EventArgs) Handles tsmi_AddExcList.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
            If dr IsNot Nothing Then
                dlgListToAdd.TemplateID = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
                dlgListToAdd.TemplateMOConfigID = CInt(dr("TemplateMOConfigID"))
                dlgListToAdd.MOName = CStr(gvMOConfig.GetFocusedRowCellValue("MOName"))
                dlgListToAdd.MOType = "current"
                dlgListToAdd.FilterType = "Exclusion"
            End If

            If dlgListToAdd.ShowDialog() = DialogResult.OK Then
                FillExclusionList()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_AddExcList2AllMO_Click(sender As Object, e As EventArgs) Handles tsmi_AddExcList2AllMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                dlgListToAdd.TemplateID = CInt(dr("TemplateID"))
                dlgListToAdd.MOName = "ALL"
                dlgListToAdd.MOType = "all"
                dlgListToAdd.FilterType = "Exclusion"
            End If

            If dlgListToAdd.ShowDialog() = DialogResult.OK Then
                FillExclusionList()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_RemoveExcList_Click(sender As Object, e As EventArgs) Handles tsmi_RemoveExcList.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstExclusion.SelectedIndex >= 0 Then
                Dim exclusionFilter As String = lstExclusion.Text
                If XtraMessageBox.Show("Are you sure to remove filter list: " & exclusionFilter & "?", "Remove Template Filter List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteFilterList(CInt(lstExclusion.SelectedValue))
                    Dim dr As DataRow = dtRefChkList.Select("TemplateMOFilterListID=" & CInt(lstExclusion.SelectedValue))(0)
                    dtRefChkList.Rows.Remove(dr)
                    'FillExclusionList()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            FillExclusionList()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_Add2NewXmlJob_Click(sender As Object, e As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            cmsGrid.Close()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            selectedXmlJobID = 0
            selectedXmlJobName = Nothing

            selectedXmlJobName = XtraInputBox.Show("XML Job Name: ", "Add New XML Job", "")
            If selectedXmlJobName = "" Then
                Exit Sub
            End If

            SetMessage("Please Wait..While Rows Are Added To XML Job")

            selectedXmlJobID = Save_XmlJob_GetXmlJobID()
            AddRowsToXmlJob()

            SetMessage("Rows Added Successfully To XML Job")

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_XMLJobClick(sender As Object, e As EventArgs)
        Try
            cmsGrid.Close()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            SetMessage("Please Wait..While Rows Are Added To XML Job")

            selectedXmlJobID = TryCast(sender, ToolStripMenuItem).Tag
            AddRowsToXmlJob()

            SetMessage("Rows Added Successfully To XML Job")

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlClient.SqlRowsCopiedEventArgs)
        lblProcessedRows.Text = "Completed - Count: " & args.RowsCopied.ToString
    End Sub

    Private Sub cmTemplateList_Opening(sender As Object, e As CancelEventArgs) Handles cmTemplateList.Opening
        Try
            If gvTemplateList.RowCount = 0 Then
                tsmi_RenameTemplate.Enabled = False
            Else
                tsmi_RenameTemplate.Enabled = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_RenameTemplate_Click(sender As Object, e As EventArgs) Handles tsmi_RenameTemplate.Click
        Try
            cmTemplateList.Close()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim isPowerUser As Boolean = False
            If (lblTemplateOwner.Text.ToLower <> Environment.UserName.ToLower) Then
                If configMgr.User.IsPowerUser = True Then
                    isPowerUser = True
                Else
                    XtraMessageBox.Show("Only the template owner or the power user can rename the template", "Rename Template!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    lblTemplateOwner.ForeColor = Color.Red
                    lblTemplateOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                    isPowerUser = False
                    Exit Sub
                End If
            Else
                'template owner
                isPowerUser = True
            End If

            If (isPowerUser = True) Then
                Dim templateID As Integer = gvTemplateList.GetFocusedRowCellValue("TemplateID")
                Dim renamedTemplateName As String = XtraInputBox.Show("Rename Template Name: ", "Rename Template", CStr(gvTemplateList.GetFocusedRowCellValue("TemplateName")))
                If renamedTemplateName = "" Then
                    Exit Sub
                Else
                    RenameTemplate(templateID, renamedTemplateName)
                    LoadTemplateList()
                    gvTemplateList.FocusedRowHandle = gvTemplateList.LocateByValue("TemplateID", templateID)
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRefreshInconSumm_Click(sender As Object, e As EventArgs) Handles btnRefreshInconSumm.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadInconsistencySummaryGrid()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRefreshDetailedData_Click(sender As Object, e As EventArgs) Handles btnRefreshDetailedData.Click
        Dim dtDetailedData As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If tglSwitchAllTemplates.IsOn Then
                RefreshLoadAllTemplates = True
                LoadDataToGrid()
                lblProcessedRows.Text = "Use Column Filters To Load More Rows"
            Else
                lblProcessedRows.Text = ""
                RefreshLoadAllTemplates = False
                dtDetailedData = CreateData(0, 0)
                If dtDetailedData IsNot Nothing Then
                    IOSDevExpressGrid.PopulateDataInGrid(gcDetailedData, gvDetailedData, dtDetailedData, "ALL")
                    gvDetailedData.Columns("ParameterValue").Width = 500
                    gvDetailedData.Columns("ParameterValue").OptionsColumn.FixedWidth = True

                    gvDetailedData.Columns("TemplateValue").Width = 500
                    gvDetailedData.Columns("TemplateValue").OptionsColumn.FixedWidth = True
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            dtDetailedData = Nothing
        End Try
    End Sub

    Private Sub tsmi_AddParameterToExclusionList_Click(sender As Object, e As EventArgs) Handles tsmi_AddParameterToExclusionList.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gc As GridControl = TryCast(cmsGrid.SourceControl, GridControl)
            Dim gv As GridView = TryCast(gc.DefaultView, GridView)

            Dim selectedRows() As Integer = gv.GetSelectedRows()
            If gc.Name = "gcViewTemplate" Then
                For iCntr As Integer = 0 To selectedRows.Count - 1
                    AddParamExclusionToTemplate(gv.GetRowCellValue(selectedRows(iCntr), "Param_IsActive"))
                Next
            Else
                For iCntr As Integer = 0 To selectedRows.Count - 1
                    AddParamExclusionToTemplate(gv.GetRowCellValue(selectedRows(iCntr), "ParameterName"))
                Next
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_ParamDescResultsGrids_Click(sender As Object, e As EventArgs) Handles tsmi_ParamDescResultsGrids.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim gc As GridControl = TryCast(cmsGrid.SourceControl, GridControl)
            Dim gv As GridView = TryCast(gc.DefaultView, GridView)

            Dim dr As DataRow = gv.GetFocusedDataRow()

            Dim objParamDesc As New frmParameterDescription()
            objParamDesc.moTblName = Nothing

            If gc.Name = "gcViewTemplate" Then
                objParamDesc.moName = dr("MOName").ToString
                objParamDesc.paramName = dr("Param_IsActive").ToString
            Else
                objParamDesc.moName = dr("MO").ToString
                objParamDesc.paramName = dr("ParameterName").ToString
            End If

            objParamDesc.fromLeft = Me.Left + Me.Width
            objParamDesc.fromTop = Me.Top
            objParamDesc.ShowDialog()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ParamDescMOParam_Click(sender As Object, e As EventArgs) Handles tsmi_ParamDescMOParam.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim drMOConfig As DataRow = gvMOConfig.GetFocusedDataRow()
            Dim drParam As DataRow = gvMOParam.GetFocusedDataRow()

            Dim objParamDesc As New frmParameterDescription()
            objParamDesc.moTblName = Nothing
            objParamDesc.paramName = drParam("ParamName").ToString
            objParamDesc.moName = drMOConfig("MOName").ToString
            objParamDesc.fromLeft = Me.Left + Me.Width
            objParamDesc.fromTop = Me.Top
            objParamDesc.ShowDialog()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ParameterMappingCells_Click(sender As Object, e As EventArgs) Handles tsmi_ParameterMappingCells.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()

            Dim dt As DataTable = Nothing
            Dim gc As GridControl = TryCast(cmsGrid.SourceControl, GridControl)
            Dim gv As GridView = TryCast(gc.DefaultView, GridView)
            Dim dr As DataRow = gv.GetFocusedDataRow()

            'If gc.Name = "gcDetailedData" Then

            '    Dim FilterExpression As String = ""
            '    If Not dr.ItemArray().Contains("ObjectConditionColumns") Then
            '        FilterExpression = " x.MO = '" & dr("MO").ToString & "'  AND x.ParameterName = '" & dr("ParameterName").ToString & "'"
            '    Else
            '        If IsDBNull(dr("ObjectConditionColumns")) Then
            '            FilterExpression = " x.MO = '" & dr("MO").ToString & "'  AND x.ParameterName = '" & dr("ParameterName").ToString & "'"
            '        Else
            '            FilterExpression = " x.MO = '" & dr("MO").ToString & "'  AND x.ObjectConditionColumns = '" & dr("ObjectConditionColumns").ToString & "' AND x.ParameterName = '" & dr("ParameterName").ToString & "'"
            '        End If
            '    End If
            '    dt = CreateData(0, 0, FilterExpression)
            'Else
            dt = clsSQLCommands.GetMappingDataForTemplateMOParam(connStrIOSServer, gvTemplateList.GetFocusedRowCellValue("TemplateID"), dr("MO").ToString, dr("ParameterName").ToString)
            'End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ParameterMapping("cells", dt, dr("MO").ToString, dr("ParameterName").ToString)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            'Finally
            '    Me.Cursor = Cursors.Default
            '    Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ParameterMappingVoronoi_Click(sender As Object, e As EventArgs) Handles tsmi_ParameterMappingVoronoi.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()

            Dim dt As DataTable = Nothing
            Dim gc As GridControl = TryCast(cmsGrid.SourceControl, GridControl)
            Dim gv As GridView = TryCast(gc.DefaultView, GridView)
            Dim dr As DataRow = gv.GetFocusedDataRow()

            'If gc.Name = "gcDetailedData" Then
            '    dt = CreateData(0, 0)
            '    Dim dtTemp As DataTable = Nothing
            '    If dt.AsEnumerable().Count(Function(x) x.Field(Of String)("MO") = dr("MO").ToString AndAlso x.Field(Of String)("ParameterName") = dr("ParameterName").ToString) > 0 Then
            '        dtTemp = dt.AsEnumerable().Where(Function(x) x.Field(Of String)("MO") = dr("MO").ToString AndAlso x.Field(Of String)("ParameterName") = dr("ParameterName").ToString).CopyToDataTable()
            '        ParameterMapping("voronoi", dtTemp, dr("MO").ToString, dr("ParameterName").ToString)
            '    End If
            'Else
            dt = clsSQLCommands.GetMappingDataForTemplateMOParam(connStrIOSServer, gvTemplateList.GetFocusedRowCellValue("TemplateID"), dr("MO").ToString, dr("ParameterName").ToString)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ParameterMapping("voronoi", dt, dr("MO").ToString, dr("ParameterName").ToString)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            'Finally
            '    Me.Cursor = Cursors.Default
            '    Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ParameterMappingLabel_Click(sender As Object, e As EventArgs) Handles tsmi_ParameterMappingLabel.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()

            Dim dt As DataTable = Nothing
            Dim gc As GridControl = TryCast(cmsGrid.SourceControl, GridControl)
            Dim gv As GridView = TryCast(gc.DefaultView, GridView)
            Dim dr As DataRow = gv.GetFocusedDataRow()

            'If gc.Name = "gcDetailedData" Then
            '    dt = CreateData(0, 0)
            '    Dim dtTemp As DataTable = Nothing
            '    If dt.AsEnumerable().Count(Function(x) x.Field(Of String)("MO") = dr("MO").ToString AndAlso x.Field(Of String)("ParameterName") = dr("ParameterName").ToString) > 0 Then
            '        dtTemp = dt.AsEnumerable().Where(Function(x) x.Field(Of String)("MO") = dr("MO").ToString AndAlso x.Field(Of String)("ParameterName") = dr("ParameterName").ToString).CopyToDataTable()
            '        ParameterMapping("label", dtTemp, dr("MO").ToString, dr("ParameterName").ToString)
            '    End If
            'Else
            dt = clsSQLCommands.GetMappingDataForTemplateMOParam(connStrIOSServer, gvTemplateList.GetFocusedRowCellValue("TemplateID"), dr("MO").ToString, dr("ParameterName").ToString)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ParameterMapping("label", dt, dr("MO").ToString, dr("ParameterName").ToString)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            'Finally
            '    Me.Cursor = Cursors.Default
            '    Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ParameterMappingIncon_Click(sender As Object, e As EventArgs) Handles tsmi_ParameterMappingIncon.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtSourceData As DataTable = Nothing
            Dim gc As GridControl = TryCast(cmsGrid.SourceControl, GridControl)
            Dim gv As GridView = TryCast(gc.DefaultView, GridView)

            Dim drIncon As DataRow = gv.GetFocusedDataRow()

            'If dtDetailedData.Rows.Count = 0 Then
            dtSourceData = CreateData(0, 0)
            Dim dtTemp As DataTable = Nothing
            If dtSourceData.AsEnumerable().Count(Function(x) x.Field(Of String)("MO") = drIncon("MO").ToString AndAlso x.Field(Of String)("ParameterName") = drIncon("ParameterName").ToString) > 0 Then
                dtTemp = dtSourceData.AsEnumerable().Where(Function(x) x.Field(Of String)("MO") = drIncon("MO").ToString AndAlso x.Field(Of String)("ParameterName") = drIncon("ParameterName").ToString).CopyToDataTable()

                'Else
                'dtSourceData = dtDetailedData.AsEnumerable().Where(Function(x) x.Field(Of String)("MO") = drIncon("MO").ToString AndAlso x.Field(Of String)("ParameterName") = drIncon("ParameterName").ToString).CopyToDataTable()
                'End If

                If dtTemp IsNot Nothing Then
                    Dim dtData As DataTable = dtTemp.DistinctCol({"CELLNAME", "MO", "ParameterName"})
                    If dtData.Rows.Count > 0 Then
                        Dim colStattic As DataColumn = New DataColumn("ParamValue")
                        With colStattic
                            .DataType = System.Type.GetType("System.String")
                            .DefaultValue = drIncon("ParameterValue")
                        End With

                        dtData.Columns.Add(colStattic)
                        frmMapWindow.MapDataToSingleLayer(dtData, "RefCheck_Inconsistency", "CELLNAME", "CELLNAME", "Individual Theme", "ParamValue", "CELLNAME,ParameterName,ParamValue")
                    End If

                    dtSourceData.Dispose()
                    dtSourceData = Nothing
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_RemoveIncListAllMO_Click(sender As Object, e As EventArgs) Handles tsmi_RemoveIncListAllMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstInclusion.SelectedIndex >= 0 Then
                Dim inclusionFilter As String = lstInclusion.Text
                Dim templateID As Integer = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
                If XtraMessageBox.Show("Are you sure to remove filter list " & inclusionFilter & " from All MO?", "Remove Template Filter List From All MO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim dr As DataRow = dtRefChkList.Select("TemplateMOFilterListID=" & CInt(lstInclusion.SelectedValue))(0)
                    DeleteFilterListAllMO(templateID, CInt(dr("ListID")), CStr(dr("InclusionOrExclusion")))
                    dtRefChkList.Rows.Remove(dr)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            FillInclusionList()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_RemoveExcListAllMO_Click(sender As Object, e As EventArgs) Handles tsmi_RemoveExcListAllMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstExclusion.SelectedIndex >= 0 Then
                Dim exclusionFilter As String = lstExclusion.Text
                Dim templateID As Integer = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))
                If XtraMessageBox.Show("Are you sure to remove filter list " & exclusionFilter & " from All MO?", "Remove Template Filter List from All MO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim dr As DataRow = dtRefChkList.Select("TemplateMOFilterListID=" & CInt(lstExclusion.SelectedValue))(0)
                    DeleteFilterListAllMO(templateID, CInt(dr("ListID")), CStr(dr("InclusionOrExclusion")))
                    dtRefChkList.Rows.Remove(dr)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            FillExclusionList()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_DeleteSelectedParams_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteSelectedParams.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
            Dim rIndex() As Integer = gvMOParam.GetSelectedRows()
            If rIndex.Count > 0 Then
                If XtraMessageBox.Show("Are you sure to delete selected params?", "Delete MO Param", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    For iCntr = 0 To rIndex.Count - 1
                        Dim strConnection As String = Nothing
                        Dim sqlParam As String = Nothing
                        Dim parray()() As String = {
                            New String() {"@TemplateMOParamConfigID", gvMOParam.GetRowCellValue(rIndex(iCntr), "TemplateMOParamConfigID")}
                        }
                        strConnection = GetSQL(4129, parray)(0)
                        sqlParam = GetSQL(4129, parray)(1)
                        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                        'save change log
                        Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), dr("MOName").ToString, dr("TemplateMOConfigID"), "MO Param: " & gvMOParam.GetRowCellValue(rIndex(iCntr), "ParamName") & " deleted from the template")
                    Next
                End If
                LoadMOParamGrid(dr("TemplateMOConfigID"), dr("MOName"))
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            'FillExclusionList()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub ManageGroupControlsFromMOConfig(isAllParameters As Boolean, isActive As Boolean)
        If isActive = False Then
            grpCtrlParam.Enabled = False
            grpCtrlConditions.Enabled = False
        End If

        If isActive = True AndAlso isAllParameters = True Then
            grpCtrlParam.Enabled = False
            grpCtrlConditions.Enabled = False
        End If

        If isActive = True AndAlso isAllParameters = False Then
            grpCtrlParam.Enabled = True
            grpCtrlConditions.Enabled = True
        End If
    End Sub

    Private Sub UpdateMOConfigIsAllPArameters(columnValue As Boolean)
        Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
        If dr IsNot Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", CInt(dr("TemplateMOConfigID"))},
                New String() {"@IsAllParameters", IIf(columnValue = True, 1, 0)},
                New String() {"@IsAutoSetValue", IIf(columnValue = True, 1, 0)}
            }
            strConnection = GetSQL(4140, parray)(0)
            sqlParam = GetSQL(4140, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            'Check/uncheck IsAutoSetValue column as per IsAllParameters check/uncheck.
            gvMOConfig.SetRowCellValue(gvMOConfig.FocusedRowHandle, "IsAutoSetValue", columnValue)
            ManageGroupControlsFromMOConfig(columnValue, CBool(dr("IsActive")))

            Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(dr("MOName")), CInt(dr("TemplateMOConfigID")), "IsAllParameters/IsAutoSetValue modified to " & columnValue.ToString & " for the mo: " & CStr(dr("MOName")))
        End If
    End Sub

    Private Sub UpdateMOConfigIsActive(columnValue As Boolean)
        Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
        If dr IsNot Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", CInt(dr("TemplateMOConfigID"))},
                New String() {"@IsActive", IIf(columnValue = True, 1, 0)}
            }
            strConnection = GetSQL(4142, parray)(0)
            sqlParam = GetSQL(4142, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            ManageGroupControlsFromMOConfig(CBool(dr("IsAllParameters")), columnValue)

            Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(dr("MOName")), CInt(dr("TemplateMOConfigID")), "IsActive modified to " & columnValue.ToString & " for the mo: " & dr("MOName").ToString)
        End If
    End Sub

    Private Sub UpdateMOConfigIsAutoSetValue(columnValue As Boolean)
        Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
        If dr IsNot Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", CInt(dr("TemplateMOConfigID"))},
                New String() {"@IsAutoSetValue", IIf(columnValue = True, 1, 0)}
            }
            strConnection = GetSQL(4141, parray)(0)
            sqlParam = GetSQL(4141, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(dr("MOName")), CInt(dr("TemplateMOConfigID")), "IsAutoSetValue modified to " & columnValue.ToString & " for the mo: " & dr("MOName").ToString)
        End If
    End Sub

    Private Sub UpdateMOParamCommonalityValue(templateMOParamConfigID As Integer, commonalityValue As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOParamConfigID", templateMOParamConfigID},
            New String() {"@CommonalityValue", Chr(39) & commonalityValue & Chr(39)}
        }
        strConnection = GetSQL(4134, parray)(0)
        sqlParam = GetSQL(4134, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub UpdateMOParamAutoSetValue(templateMOParamConfigID As Integer, isAutoSetValue As Boolean)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOParamConfigID", templateMOParamConfigID},
            New String() {"@IsAutoSetValue", IIf(isAutoSetValue, 1, 0)}
        }
        strConnection = GetSQL(4133, parray)(0)
        sqlParam = GetSQL(4133, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), "IsAutoSetValue modified for the mo param: " & gvMOParam.GetFocusedRowCellValue("ParamName").ToString, CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), templateMOParamConfigID)
    End Sub

    Private Sub UpdateMOParamIsActive(templateMOParamConfigID As Integer, isActive As Boolean)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOParamConfigID", templateMOParamConfigID},
            New String() {"@IsActive", IIf(isActive, 1, 0)}
        }
        strConnection = GetSQL(4135, parray)(0)
        sqlParam = GetSQL(4135, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), "IsActive modified for the mo param: " & gvMOParam.GetFocusedRowCellValue("ParamName").ToString, CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), templateMOParamConfigID)
    End Sub

    Private Sub UpdateMOParamIsConditionActive(templateMOParamConfigID As Integer, isConditionActive As Boolean)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOParamConfigID", templateMOParamConfigID},
            New String() {"@IsConditionActive", IIf(isConditionActive, 1, 0)}
        }
        strConnection = GetSQL(4137, parray)(0)
        sqlParam = GetSQL(4137, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), "IsConditionActive modified for the mo param: " & gvMOParam.GetFocusedRowCellValue("ParamName").ToString, CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID")), templateMOParamConfigID)
    End Sub

    Private Sub LoadTemplateSummaryGrid()
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", CInt(dr("TemplateID"))}
        }
        strConnection = GetSQL(4143, parray)(0)
        sqlParam = GetSQL(4143, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcTemplateSumm, gvTemplateSumm, dt, "ALL", Nothing, Nothing)
            gvTemplateSumm.Columns("ParameterName").Width = 500
            gvTemplateSumm.Columns("ParameterName").OptionsColumn.FixedWidth = True

            gvTemplateSumm.Columns("TemplateValue").Width = 500
            gvTemplateSumm.Columns("TemplateValue").OptionsColumn.FixedWidth = True
        Else
            IOSDevExpressGrid.ClearGrid(gcTemplateSumm)
        End If
    End Sub

    Private Sub LoadInconsistencySummaryGrid()
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", CInt(dr("TemplateID"))}
        }
        strConnection = GetSQL(4144, parray)(0)
        sqlParam = GetSQL(4144, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            Dim columnsToHide() As String = {"TemplateID", "MoConfigID"}
            IOSDevExpressGrid.PopulateDataInGrid(gcInconSumm, gvInconSumm, dt, "ALL", columnsToHide, Nothing)
            gvInconSumm.Columns("ParameterValue").Width = 500
            gvInconSumm.Columns("ParameterValue").OptionsColumn.FixedWidth = True

            gvInconSumm.Columns("TemplateValue").Width = 500
            gvInconSumm.Columns("TemplateValue").OptionsColumn.FixedWidth = True
        Else
            IOSDevExpressGrid.ClearGrid(gcInconSumm)
        End If
    End Sub

    Private Sub LoadDetailedDataGrid()
        Try
            LoadDataToGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadStatusGrid()
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", CInt(dr("TemplateID"))}
        }
        strConnection = GetSQL(4149, parray)(0)
        sqlParam = GetSQL(4149, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcStatus, gvStatus, dt, "ALL", Nothing, Nothing)
        Else
            IOSDevExpressGrid.ClearGrid(gcStatus)
        End If
    End Sub

    Private Sub SetDefaultSettingsForChart(ByRef ch As Chart)
        ch.DefaultElement.Marker.Visible = False
        ch.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        ch.LegendBox.DefaultEntry.Value = ""
        ch.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        ch.LegendBox.Visible = True

        ch.XAxis.TickLabelMode = TickLabelMode.Angled
        ch.XAxis.TickLabelAngle = 45
        ch.XAxis.Minimum = 0
        ch.XAxis.Maximum = 0

        ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

        ch.ToolTip.InitialDelay = 1
        ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        ch.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        ch.CleanupPeriod = 1

        ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
        ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
        ch.XAxis.TimeInterval = TimeInterval.Days
        ch.XAxis.FormatString = "dd/MM/yyyy"
        ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yyyy"
        'ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
        'ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
        ch.LegendBox.Orientation = Orientation.Bottom
        ch.LegendBox.DefaultCorner = BoxCorner.Round
        ch.LegendBox.ExtraEntries.Clear()

        ch.TitleBox.Position = TitleBoxPosition.Full
        ch.TitleBox.CornerTopLeft = BoxCorner.Round
        ch.TitleBox.CornerTopRight = BoxCorner.Round
        ch.TitleBox.Label.AutoWrap = True
        ch.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
    End Sub

    Private Sub LoadResultChart1()
        Dim k As Integer = 0
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", CInt(dr("TemplateID"))}
        }

        strConnection = GetSQL(4150, parray)(0)
        sqlParam = GetSQL(4150, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(grdChart1, gvChart1, dt, "ALL")

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim dtCopy As New DataTable
            dtCopy.Columns.Add("MO", GetType(System.String))
            dtCopy.Columns.Add("InconsistencyCount", GetType(System.Int32))

            For Each drw As DataRow In dt.Rows
                Dim drow As DataRow = dtCopy.NewRow()
                drow("MO") = drw("MO")
                drow("InconsistencyCount") = drw("InconsistencyCount")
                dtCopy.Rows.Add(drow)
            Next

            chart1.Height = tbChartHeightStats.Value
            chart1.SuspendLayout()
            SetDefaultSettingsForChart(chart1)

            chart1.TitleBox.Label.Text = "TemplateName: " & dr("TemplateName")
            chart1.TitleBox.HeaderLabel.Text = "Number of Objects inconsistent per MO"
            chart1.TitleBox.Label.Alignment = StringAlignment.Near
            chart1.TitleBox.Label.LineAlignment = StringAlignment.Near
            chart1.DefaultElement.Hotspot.ToolTip = "MO: %XValue" & Chr(13) & "%SeriesName: %Value "
            chart1.Annotations.Clear()
            chart1.Annotations.Add(New Annotation(""))
            chart1.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
            chart1.Dock = DockStyle.Fill

            Dim chart_elements() As String = Nothing
            k = 0
            For Each dcol As DataColumn In dtCopy.Columns
                If dcol.ColumnName.ToUpper.Trim <> "MO" Then
                    ReDim Preserve chart_elements(k)
                    chart_elements(k) = dcol.ColumnName
                    k = k + 1
                End If
            Next

            Dim de As DataEngine = New DataEngine(dtCopy)
            de.DataFields = String2DataFields(chart_elements, "MO")
            de.DataGridFormatString = "N2"
            'de.FormatString = "dd/MM/yyyy"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()

            Dim rnd As Random = New Random(11)
            Dim i As Integer = 0
            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Bar
                sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
            Next

            chart1.SeriesCollection.Clear()
            chart1.SeriesCollection.Add(sc)
            chart1.Series.Data = dtCopy

            dt.Dispose()
            dt = Nothing
            dtCopy.Dispose()
            dtCopy = Nothing

            chart1.XAxis.Markers.Clear()
            chart1.RefreshChart()
            chart1.ResumeLayout()
        Else
            chart1.SeriesCollection.Clear()
            chart1.RefreshChart()

            chart3.SeriesCollection.Clear()
            IOSDevExpressGrid.ClearGrid(grdChart3)
        End If
    End Sub

    Private Sub LoadResultChart2()
        Dim k As Integer = 0
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", CInt(dr("TemplateID"))}
        }

        strConnection = GetSQL(4151, parray)(0)
        sqlParam = GetSQL(4151, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        'IOSDevExpressGrid.PopulateDataInGrid(grdChart2, gvChart2, dt, "ALL")

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            chart2.Height = tbChartHeightStats.Value
            chart2.SuspendLayout()
            SetDefaultSettingsForChart(chart2)

            chart2.TitleBox.Label.Text = "TemplateName: " & dr("TemplateName")
            chart2.TitleBox.HeaderLabel.Text = "Objects with most Inconsistencies"
            chart2.TitleBox.Label.Alignment = StringAlignment.Near
            chart2.TitleBox.Label.LineAlignment = StringAlignment.Near
            chart2.DefaultElement.Hotspot.ToolTip = "ObjectName: %XValue" & Chr(13) & "%SeriesName: %Value "
            chart2.Annotations.Clear()
            chart2.Annotations.Add(New Annotation(""))
            chart2.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
            chart2.Dock = DockStyle.Fill

            Dim chart_elements() As String = Nothing
            k = 0
            For Each dcol As DataColumn In dt.Columns
                If dcol.ColumnName.ToUpper.Trim <> "OBJECTNAME" Then
                    ReDim Preserve chart_elements(k)
                    chart_elements(k) = dcol.ColumnName
                    k = k + 1
                End If
            Next

            Dim de As DataEngine = New DataEngine(dt)
            de.DataFields = String2DataFields(chart_elements, "ObjectName")
            de.DataGridFormatString = "N2"
            'de.FormatString = "dd/MM/yyyy"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()

            Dim rnd As Random = New Random(25)
            Dim i As Integer = 0
            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Bar
                sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
            Next

            chart2.SeriesCollection.Clear()
            chart2.SeriesCollection.Add(sc)
            chart2.Series.Data = dt

            dt.Dispose()
            dt = Nothing

            chart2.XAxis.Markers.Clear()

            chart2.RefreshChart()
            chart2.ResumeLayout()
        Else
            chart2.SeriesCollection.Clear()
            chart2.RefreshChart()

            chart3.SeriesCollection.Clear()
            IOSDevExpressGrid.ClearGrid(grdChart3)
        End If
    End Sub

    Private Sub LoadChart2Grid(selectedObject As String)
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", CInt(dr("TemplateID"))},
            New String() {"@clickedObject", Chr(39) & selectedObject & Chr(39)}
        }

        strConnection = GetSQL(4152, parray)(0)
        sqlParam = GetSQL(4152, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(grdChart2, gvChart2, dt, "ALL")
    End Sub

    Private Sub LoadChart3Grid(selectedObject As String)
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", CInt(dr("TemplateID"))},
            New String() {"@clickedObject", Chr(39) & selectedObject & Chr(39)}
        }

        strConnection = GetSQL(4154, parray)(0)
        sqlParam = GetSQL(4154, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(grdChart3, gvChart3, dt, "ALL")
    End Sub

    Private Sub ClearChart3()
        'Remove chart 3 & clear chart 3 grid
        chart3.SeriesCollection.Clear()
        chart3.RefreshChart()
        IOSDevExpressGrid.ClearGrid(grdChart3)
    End Sub

    Private Sub LoadResultChart3(ByVal selectedMO As String)
        Dim k As Integer = 0
        Dim moConfigID As Integer = Nothing
        moConfigID = CInt(TryCast(grdChart1.DataSource, DataTable).Select("MO='" & selectedMO & "'")(0)("MoConfigId"))
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim dtTemp As DataTable = Nothing
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", CInt(dr("TemplateID"))},
            New String() {"@moConfigID", moConfigID}
        }

        strConnection = GetSQL(4153, parray)(0)
        sqlParam = GetSQL(4153, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        'IOSDevExpressGrid.PopulateDataInGrid(grdChart3, gvChart3, dt, "ALL")

        If dt.Rows.Count > 0 Then
            Dim dtCopy As New DataTable
            dtCopy.Columns.Add("ParameterName", GetType(System.String))
            dtCopy.Columns.Add("ObjectCount", GetType(System.Int32))

            For Each drw As DataRow In dt.Rows
                Dim drow As DataRow = dtCopy.NewRow()
                drow("ParameterName") = drw("ParameterName")
                drow("ObjectCount") = drw("ObjectCount")
                dtCopy.Rows.Add(drow)
            Next

            chart3.Height = tbChartHeightStats.Value
            chart3.SuspendLayout()
            SetDefaultSettingsForChart(chart3)
            If dtCopy IsNot Nothing Then

                chart3.TitleBox.Label.Text = "TemplateName: " & dr("TemplateName")
                chart3.TitleBox.HeaderLabel.Text = "Number of Objects inconsistent per MO and Parameter"
                chart3.TitleBox.Label.Alignment = StringAlignment.Near
                chart3.TitleBox.Label.LineAlignment = StringAlignment.Near
                chart3.DefaultElement.Hotspot.ToolTip = "ParameterName: %XValue" & Chr(13) & "%SeriesName: %Value "
                chart3.Annotations.Clear()
                chart3.Annotations.Add(New Annotation(""))
                chart3.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                chart3.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                chart3.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
                chart3.Dock = DockStyle.Fill

                Dim chart_elements() As String = Nothing
                k = 0
                For Each dcol As DataColumn In dtCopy.Columns
                    If dcol.ColumnName.ToUpper.Trim <> "PARAMETERNAME" Then
                        ReDim Preserve chart_elements(k)
                        chart_elements(k) = dcol.ColumnName
                        k = k + 1
                    End If
                Next

                Dim de As DataEngine = New DataEngine(dtCopy)
                de.DataFields = String2DataFields(chart_elements, "ParameterName")
                de.DataGridFormatString = "N2"

                Dim sc As New SeriesCollection
                sc = de.GetSeries()
                'sc.Sort(ElementValue.YValue, "ASC")

                Dim rnd As Random = New Random(17)
                Dim i As Integer = 0
                For i = 0 To sc.Count() - 1
                    sc(i).Type = SeriesType.Bar
                    sc(i).EmptyElement.Mode = EmptyElementMode.TreatAsZero
                    sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
                Next

                chart3.SeriesCollection.Clear()
                chart3.SeriesCollection.Add(sc)
                chart3.Series.Data = dtCopy

                dtCopy.Dispose()
                dtCopy = Nothing

                chart3.RefreshChart()
                chart3.ResumeLayout()
            End If
        Else
            chart3.SeriesCollection.Clear()
            IOSDevExpressGrid.ClearGrid(grdChart3)
        End If
    End Sub

    Private Sub LoadViewTemplateGrid()
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", CInt(dr("TemplateID"))}
        }
        strConnection = GetSQL(4146, parray)(0)
        sqlParam = GetSQL(4146, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcViewTemplate, gvViewTemplate, dt, "ALL", Nothing, Nothing)
            gvViewTemplate.Columns("FilterString").Width = 500
            gvViewTemplate.Columns("FilterString").OptionsColumn.FixedWidth = True
        Else
            IOSDevExpressGrid.ClearGrid(gcViewTemplate)
        End If
    End Sub

    Private Sub LoadViewChangeLogGrid()
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", CInt(dr("TemplateID"))}
        }
        strConnection = GetSQL(4178, parray)(0)
        sqlParam = GetSQL(4178, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcChangeLog, gvChangeLog, dt, "ALL", {"ChangeLogID"}, "EventOccured")
        Else
            IOSDevExpressGrid.ClearGrid(gcChangeLog)
        End If
    End Sub

    Private Sub UpdateTemplateLatestConfig()
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        If dr IsNot Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateID", CInt(dr("TemplateID"))}
            }
            strConnection = GetSQL(4138, parray)(0)
            sqlParam = GetSQL(4138, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        End If
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

    Private Sub OpenAddConditionDialog()
        Dim objCondition As New dlgCondition()
        Dim drTemplate As DataRow = gvTemplateList.GetDataRow(gvTemplateList.FocusedRowHandle)
        objCondition.templateID = drTemplate("TemplateID").ToString

        Dim dr As DataRow = gvMOConfig.GetDataRow(gvMOConfig.FocusedRowHandle)
        objCondition.sourceMODBName = CStr(dr("MODatabase"))
        objCondition.sourceMOTableName = CStr(dr("MOTable"))
        objCondition.moName = CStr(dr("MOName"))

        Dim dr2 As DataRow = gvMOParam.GetDataRow(gvMOParam.FocusedRowHandle)
        objCondition.templateMOParamConfigID = dr2("TemplateMOParamConfigID")
        objCondition.templateMOConfigID = dr2("TemplateMOConfigID")
        objCondition.paramName = dr2("ParamName")
        objCondition.ShowDialog()

        gvMOParam_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadMOConfigGrid(templateID As String)
        RemoveHandler gvMOConfig.ValidatingEditor, AddressOf gvMOConfig_ValidatingEditor
        RemoveHandler gvMOConfig.CustomRowCellEdit, AddressOf gvMOConfig_CustomRowCellEdit

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateID}
        }
        strConnection = GetSQL(4110, parray)(0)
        sqlParam = GetSQL(4110, parray)(1)
        Dim dtMOConfig As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dtMOConfig IsNot Nothing Then
            If dtMOConfig.Rows.Count > 0 Then
                Dim columnsToHide() As String = {"TemplateMOConfigID", "TemplateID", "IOS_Vendor", "MOTable", "MODatabase"}
                IOSDevExpressGrid.PopulateDataInGrid(gcMOConfig, gvMOConfig, dtMOConfig, "ALL", columnsToHide, "MOName")

                Dim riChkMOConfigAllParameters As RepositoryItemCheckEdit = TryCast(gcMOConfig.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                riChkMOConfigAllParameters.CheckStyle = CheckStyles.Standard
                riChkMOConfigAllParameters.AllowGrayed = False
                riChkMOConfigAllParameters.NullStyle = StyleIndeterminate.Unchecked
                gvMOConfig.Columns("IsAllParameters").ColumnEdit = riChkMOConfigAllParameters
                AddHandler riChkMOConfigAllParameters.CheckedChanged, AddressOf riChkMOConfigAllParameters_CheckedChanged

                Dim riChkMOConfigActive As RepositoryItemCheckEdit = TryCast(gcMOConfig.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                riChkMOConfigActive.CheckStyle = CheckStyles.Standard
                riChkMOConfigActive.AllowGrayed = False
                riChkMOConfigActive.NullStyle = StyleIndeterminate.Unchecked
                gvMOConfig.Columns("IsActive").ColumnEdit = riChkMOConfigActive
                AddHandler riChkMOConfigActive.CheckedChanged, AddressOf riChkMOConfigActive_CheckedChanged

                Dim riChkMOConfigAutoSetValue As RepositoryItemCheckEdit = TryCast(gcMOConfig.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                riChkMOConfigAutoSetValue.CheckStyle = CheckStyles.Standard
                riChkMOConfigAutoSetValue.AllowGrayed = False
                riChkMOConfigAutoSetValue.NullStyle = StyleIndeterminate.Unchecked
                gvMOConfig.Columns("IsAutoSetValue").ColumnEdit = riChkMOConfigAutoSetValue
                AddHandler riChkMOConfigAutoSetValue.CheckedChanged, AddressOf riChkMOConfigAutoSetValue_CheckedChanged

                Dim riChkMOConfigChkMissingNE As RepositoryItemCheckEdit = TryCast(gcMOConfig.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                riChkMOConfigChkMissingNE.CheckStyle = CheckStyles.Standard
                riChkMOConfigChkMissingNE.AllowGrayed = False
                riChkMOConfigChkMissingNE.NullStyle = StyleIndeterminate.Unchecked
                gvMOConfig.Columns("CheckMissingNE").ColumnEdit = riChkMOConfigChkMissingNE
                AddHandler riChkMOConfigChkMissingNE.CheckedChanged, AddressOf riChkMOConfigChkMissingNE_CheckedChanged

                riCmbPriority = TryCast(gcMOConfig.RepositoryItems.Add("ComboBoxEdit"), RepositoryItemComboBox)
                Dim items As String() = {"Select Priority", "Critical", "Major", "Normal"}
                riCmbPriority.Items.AddRange(items)
                AddHandler riCmbPriority.SelectedIndexChanged, AddressOf riCmbPriority_SelectedIndexChanged
                AddHandler gvMOConfig.ValidatingEditor, AddressOf gvMOConfig_ValidatingEditor
                AddHandler gvMOConfig.CustomRowCellEdit, AddressOf gvMOConfig_CustomRowCellEdit
            Else
                ClearGridsForNewOrEmptyTemplate()
            End If
        End If
    End Sub

    Private Sub LoadMOParamGrid(templateMOConfigID As String, moName As String)
        RemoveHandler gvMOParam.CustomRowCellEdit, AddressOf gvMOParam_CustomRowCellEdit
        RemoveHandler gvMOParam.FocusedRowChanged, AddressOf gvMOParam_FocusedRowChanged

        Dim dtParam As DataTable = GetParamListForSelectedMO(templateMOConfigID)
        dtParam.Columns.Add("MOName", GetType(String))

        ' Add mo name column to dtParam with mo name
        For Each dr As DataRow In dtParam.Rows
            dr("MOName") = moName
        Next

        Dim columnsToHide() As String = {"TemplateMOParamConfigID", "TemplateMOConfigID"}
        IOSDevExpressGrid.PopulateDataInGrid(gcMOParam, gvMOParam, dtParam, "ALL", columnsToHide, "ParamName")
        gvMOParam.Columns("MOName").VisibleIndex = 0

        Dim riChkMOParamAutoSetValue As RepositoryItemCheckEdit = TryCast(gcMOParam.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkMOParamAutoSetValue.CheckStyle = CheckStyles.Standard
        riChkMOParamAutoSetValue.AllowGrayed = False
        riChkMOParamAutoSetValue.NullStyle = StyleIndeterminate.Unchecked
        gvMOParam.Columns("IsAutoSetValue").ColumnEdit = riChkMOParamAutoSetValue
        AddHandler riChkMOParamAutoSetValue.CheckedChanged, AddressOf riChkMOParamAutoSetValue_CheckedChanged

        Dim riChkMOParamActive As RepositoryItemCheckEdit = TryCast(gcMOParam.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkMOParamActive.CheckStyle = CheckStyles.Standard
        riChkMOParamActive.AllowGrayed = False
        riChkMOParamActive.NullStyle = StyleIndeterminate.Unchecked
        gvMOParam.Columns("IsActive").ColumnEdit = riChkMOParamActive
        AddHandler riChkMOParamActive.CheckedChanged, AddressOf riChkMOParamActive_CheckedChanged

        Dim riChkMOParamConditionActive As RepositoryItemCheckEdit = TryCast(gcMOParam.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkMOParamConditionActive.CheckStyle = CheckStyles.Standard
        riChkMOParamConditionActive.AllowGrayed = False
        riChkMOParamConditionActive.NullStyle = StyleIndeterminate.Unchecked
        gvMOParam.Columns("IsConditionActive").ColumnEdit = riChkMOParamConditionActive
        AddHandler riChkMOParamConditionActive.CheckedChanged, AddressOf riChkMOParamConditionActive_CheckedChanged

        AddHandler gvMOParam.FocusedRowChanged, AddressOf gvMOParam_FocusedRowChanged
        AddHandler gvMOParam.ValidatingEditor, AddressOf gvMOParam_ValidatingEditor
        AddHandler gvMOParam.CustomRowCellEdit, AddressOf gvMOParam_CustomRowCellEdit

        gvMOParam_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadMOFilterGrid(templateMOConfigID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", templateMOConfigID}
        }
        strConnection = GetSQL(4126, parray)(0)
        sqlParam = GetSQL(4126, parray)(1)
        Dim dtMoFilter As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(gcMOFilter, gvMOFilter, dtMoFilter, "ALL", {"TemplateMOFilterID"}, "FilterString")
    End Sub

    Private Sub LoadDataToMOGrid(ByVal templateID As String, ByVal moName As String, ByVal moTable As String, vendor As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateID", templateID},
                New String() {"@VendorTech", Chr(39) & vendor & Chr(39)},
                New String() {"@MO_Name", Chr(39) & moName.ToString & Chr(39)},
                New String() {"@MO_Table", Chr(39) & "data_Huawei_CM.dbo." & moTable.ToString & Chr(39)},
                New String() {"@MO_Database", Chr(39) & "data_Huawei_CM" & Chr(39)},
                New String() {"@isAllParameters", 1},
                New String() {"@isAutoSetValue", 1},
                New String() {"@CommonalityValue", Chr(39) & "20" & Chr(39)},
                New String() {"@isActive", 1}
            }
            strConnection = GetSQL(4109, parray)(0)
            sqlParam = GetSQL(4109, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            If dr IsNot Nothing Then
                LoadMOConfigGrid(dr("TemplateID"))
            End If
            gvMOConfig.MoveLast()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub DeleteFilter(ByVal templateMOFilterID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOFilterID", templateMOFilterID}
        }
        strConnection = GetSQL(4122, parray)(0)
        sqlParam = GetSQL(4122, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub DeleteCondition(ByVal conditionID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ConditionID", conditionID}
        }
        strConnection = GetSQL(4139, parray)(0)
        sqlParam = GetSQL(4139, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub DeleteTemplate(ByVal templateID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateID}
        }
        strConnection = GetSQL(4123, parray)(0)
        sqlParam = GetSQL(4123, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub ClearTemplate(ByVal templateID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateID}
        }
        strConnection = GetSQL(4176, parray)(0)
        sqlParam = GetSQL(4176, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadFilterLists(ByVal templateMOConfigID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateMOConfigID", templateMOConfigID}
        }
        strConnection = GetSQL(4157, parray)(0)
        sqlParam = GetSQL(4157, parray)(1)

        dtRefChkList = New DataTable
        dtRefChkList = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        lstInclusion.DataSource = Nothing
        lstExclusion.DataSource = Nothing

        If dtRefChkList.Rows.Count > 0 Then
            'Fill inclusion filter list
            If dtRefChkList.Select("InclusionOrExclusion='Inclusion'").Count > 0 Then
                Dim dtInclusion As DataTable = dtRefChkList.Select("InclusionOrExclusion='Inclusion'").CopyToDataTable
                lstInclusion.DataSource = dtInclusion
                lstInclusion.DisplayMember = "ListName"
                lstInclusion.ValueMember = "TemplateMOFilterListID"
            End If

            'Fill exclusion filter list
            If dtRefChkList.Select("InclusionOrExclusion='Exclusion'").Count > 0 Then
                Dim dtExclusion As DataTable = dtRefChkList.Select("InclusionOrExclusion='Exclusion'").CopyToDataTable
                lstExclusion.DataSource = dtExclusion
                lstExclusion.DisplayMember = "ListName"
                lstExclusion.ValueMember = "TemplateMOFilterListID"
            End If
        End If
    End Sub

    Private Sub GetTemplateList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateVendor", Chr(39) & cmbVendor.Text.Trim & Chr(39)}
        }
        strConnection = GetSQL(4101, parray)(0)
        sqlParam = GetSQL(4101, parray)(1)

        dtCMTemplate = New DataTable
        dtCMTemplate = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadTemplateList()
        RemoveHandler gvTemplateList.FocusedRowChanged, AddressOf gvTemplateList_FocusedRowChanged
        GetTemplateList()
        Dim columnsToHide() As String = {"TemplateVendor", "TemplateDescription", "Owner", "IsLocked", "IsScheduled", "IsEnabled", "LatestConfigUpdate", "LastRunTime", "LastCMDate"}
        IOSDevExpressGrid.PopulateDataInGrid(gcTemplateList, gvTemplateList, dtCMTemplate, "ALL", columnsToHide, "TemplateName")

        gvTemplateList.Columns(0).BestFit()
        gvTemplateList.Columns(0).Width = 50
        gvTemplateList.Columns(0).Caption = "ID"
        gcTemplateList.Refresh()

        AddHandler gvTemplateList.FocusedRowChanged, AddressOf gvTemplateList_FocusedRowChanged

        gvTemplateList.SelectRow(0)
        gvTemplateList.FocusedRowHandle = 0
        gvTemplateList_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub GetListOfParameters(ByVal vendorName As String, ByVal mo As String, Optional ByVal isAllParam As Integer = 0)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String
            Dim sqlParam As String

            Dim parray()() As String = {
                New String() {"@vendor", "'" & vendorName & "'"},
                New String() {"@mo", "'" & mo & "'"},
                New String() {"@tech", "''"},
                New String() {"@IsAllParam", isAllParam}
            }

            strConnection = GetSQL(3502, parray)(0)
            sqlParam = GetSQL(3502, parray)(1)

            dtParameterList = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            'tlParameterList.DataSource = Nothing
            'tlParameterList.Columns.Clear()
            'tlParameterList.DataSource = dtParameterList
            'If tlParameterList.Columns.Count > 0 Then
            '    tlParameterList.Columns(2).Caption = "Parameter Name"
            '    tlParameterList.Columns(0).Visible = False
            'End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub EnableDisableButtons()
        btnGenerate.Enabled = True
        If cmbVendor.SelectedIndex > 0 Then
            btnAdd.Enabled = True
            btnDelete.Enabled = True
        Else
            btnAdd.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub BindVendor()
        Dim dtCMVendor As DataTable = Nothing
        If (dt_IOS_ObjectConfig IsNot Nothing) Then
            dtCMVendor = New DataView(dt_IOS_ObjectConfig, "TemplateManager=1", "Vendor", DataViewRowState.CurrentRows).ToTable(True, "Vendor")
        End If
        BindDevExComboBoxWithValueMember(cmbVendor, dtCMVendor, "Vendor", "Vendor", "Select Vendor")
    End Sub

    Private Sub ResizeChartHeights(ByVal chartheight As Integer)
        Try
            tlpCharts.AutoScroll = False
            For iCnt As Integer = 0 To tlpCharts.RowCount - 1
                tlpCharts.RowStyles.Item(iCnt).SizeType = SizeType.Absolute
                tlpCharts.RowStyles.Item(iCnt).Height = chartheight
            Next
            tlpCharts.Size = New Size(tlpCharts.Width, chartheight * 4)
            tlpCharts.AutoScroll = True
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Charts_ResizeWidth()
        Me.SuspendLayout()
        Dim ch As Chart = Nothing
        Try
            tlpCharts.AutoScroll = False
            IOS.Configuration.ManageResizingControl.DisableHorizontalScrollBar(tlpCharts)
            tlpCharts.AutoScroll = True
        Catch ex As Exception
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub AddAxisMarkerX(ByRef chart As dotnetCHARTING.WinForms.Chart, ByVal xAxisValue As String)
        Dim selectedObject As String = Nothing
        Dim cl As Color = Color.Orange
        Dim axisMarkerObj As AxisMarker = Nothing
        axisMarkerObj = New AxisMarker("", New Line(cl, 3), xAxisValue)
        axisMarkerObj.LegendEntry.Visible = False
        axisMarkerObj.Label.Alignment = StringAlignment.Near
        axisMarkerObj.Label.LineAlignment = StringAlignment.Far
        axisMarkerObj.BringToFront = True
        chart.XAxis.Markers.Add(axisMarkerObj)
        chart.RefreshChart()
    End Sub

    Private Sub ClearGridsForNewOrEmptyTemplate()
        'Clear all the grids of config tab
        IOSDevExpressGrid.ClearGrid(gcMOConfig)
        IOSDevExpressGrid.ClearGrid(gcMOParam)
        IOSDevExpressGrid.ClearGrid(gcMOFilter)
        IOSDevExpressGrid.ClearGrid(gcCondition)
        'Clear all the grids of results tab
        IOSDevExpressGrid.ClearGrid(gcTemplateSumm)
        IOSDevExpressGrid.ClearGrid(gcInconSumm)
        IOSDevExpressGrid.ClearGrid(gcDetailedData)
        IOSDevExpressGrid.ClearGrid(gcStatus)
        'Clear all the grids of view template tab
        IOSDevExpressGrid.ClearGrid(gcViewTemplate)
    End Sub

    Private Sub FillInclusionList()
        If dtRefChkList.Select("InclusionOrExclusion='Inclusion'").Count > 0 Then
            Dim dtInclusion As DataTable = dtRefChkList.Select("InclusionOrExclusion='Inclusion'").CopyToDataTable
            lstInclusion.DataSource = dtInclusion
            lstInclusion.DisplayMember = "ListName"
            lstInclusion.ValueMember = "TemplateMOFilterListID"
        Else
            lstInclusion.DataSource = Nothing
        End If
        lstInclusion.Refresh()
    End Sub

    Private Sub FillExclusionList()
        If dtRefChkList.Select("InclusionOrExclusion='Exclusion'").Count > 0 Then
            Dim dtExclusion As DataTable = dtRefChkList.Select("InclusionOrExclusion='Exclusion'").CopyToDataTable
            lstExclusion.DataSource = dtExclusion
            lstExclusion.DisplayMember = "ListName"
            lstExclusion.ValueMember = "TemplateMOFilterListID"
        Else
            lstExclusion.DataSource = Nothing
        End If
        lstExclusion.Refresh()
    End Sub

    Private Sub DeleteFilterList(ByVal templateMOFilterListID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateMOFilterListID", templateMOFilterListID}
        }
        strConnection = GetSQL(4159, parray)(0)
        sqlParam = GetSQL(4159, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub ModifyGridData2Export(ByRef tempGrid As GridControl)
        Dim dtData As DataTable = DirectCast(tempGrid.DataSource, DataTable)
        Dim dtTemp As DataTable = GetDataTable2Export(dtData)
        tempGrid.DataSource = dtTemp
    End Sub

    Private Function GetDataTable2Export(ByRef dt As DataTable) As DataTable
        Dim dtTemp As DataTable = dt.Clone()
        For Each dr As DataRow In dt.Rows
            Dim drTemp As DataRow = dtTemp.NewRow()
            drTemp.ItemArray = dr.ItemArray
            If (dtTemp.Columns.Contains("TemplateValue")) Then
                If (drTemp("TemplateValue").ToString.StartsWith("=")) Then
                    drTemp("TemplateValue") = "'" & drTemp("TemplateValue").ToString
                End If
            End If
            dtTemp.AcceptChanges()
            dtTemp.Rows.Add(drTemp)
        Next
        Return dtTemp
    End Function

    Private Sub AddParamExclusionToTemplate(ByVal paramToExclude As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))},
            New String() {"@parameterName", Chr(39) & paramToExclude & Chr(39)}
        }
        strConnection = GetSQL(4166, parray)(0)
        sqlParam = GetSQL(4166, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub DeleteParamExlusion()
        Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", CInt(dr("TemplateID"))},
            New String() {"@parameterName", Chr(39) & lstParamExclusion.SelectedItem(1).ToString & Chr(39)}
        }
        strConnection = GetSQL(4168, parray)(0)
        sqlParam = GetSQL(4168, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        LoadExcludedParamList(CInt(dr("TemplateID")))
    End Sub

    Private Sub LoadExcludedParamList(templateID As Integer)
        Dim dt As DataTable = GetExcludedParamList(templateID)
        lstParamExclusion.DataSource = dt
        lstParamExclusion.DisplayMember = "ParameterName"
        lstParamExclusion.ValueMember = "TemplateID"
    End Sub

    Private Function GetExcludedParamList(ByVal templateID As Integer) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", templateID}
        }
        strConnection = GetSQL(4167, parray)(0)
        sqlParam = GetSQL(4167, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Public Sub SaveChangeLog(ByVal templateID As Integer, ByVal moName As String, ByVal moConfigID As Integer, ByVal actionTaken As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateID},
            New String() {"@UserID", Chr(39) & Environment.UserName.ToString & Chr(39)},
            New String() {"@MOName", Chr(39) & moName & Chr(39)},
            New String() {"@MOConfigID", moConfigID},
            New String() {"@EventOccured", Chr(39) & actionTaken.Trim.Replace("'", "`") & Chr(39)}
        }
        strConnection = GetSQL(4177, parray)(0)
        sqlParam = GetSQL(4177, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Function Get_XmlJobList_CurrentUser_PowerUser() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@xmlJobOwner", Chr(39) & Environment.UserName & Chr(39)},
            New String() {"@isPowerUser", IIf(configMgr.User.IsPowerUser = True, 1, 0)}
        }
        strConnection = GetSQL(4179, parray)(0)
        sqlParam = GetSQL(4179, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Function Save_XmlJob_GetXmlJobID() As Integer
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XmlJobName", Chr(39) & selectedXmlJobName & Chr(39)},
            New String() {"@UserName", Chr(39) & Environment.UserName & Chr(39)},
            New String() {"@XmlVendor", Chr(39) & cmbVendor.SelectedItem.ToString & Chr(39)}
        }
        strConnection = GetSQL(4180, parray)(0)
        sqlParam = GetSQL(4180, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt IsNot Nothing Then
            Return CInt(dt.Rows(0)(0).ToString)
        End If
        Return Nothing
    End Function

    Private Sub InsertBulkDataToServer(ConnString As String, DestinationTable As String, ByRef dtData As DataTable)
        Using cn As New System.Data.SqlClient.SqlConnection(ConnString)
            cn.Open()
            Using copy As New System.Data.SqlClient.SqlBulkCopy(cn)

                copy.DestinationTableName = DestinationTable
                copy.NotifyAfter = 1000
                AddHandler copy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                copy.ColumnMappings.Add("XMLJobID", "XMLJobID")
                copy.ColumnMappings.Add("TemplateID", "TemplateID")
                copy.ColumnMappings.Add("MoConfigID", "MoConfigID")
                copy.ColumnMappings.Add("Vendor", "Vendor")
                copy.ColumnMappings.Add("TemplateName", "TemplateName")
                copy.ColumnMappings.Add("MO", "MO")
                copy.ColumnMappings.Add("ObjectName", "ObjectName")
                copy.ColumnMappings.Add("ObjectConditionColumns", "ObjectConditionColumns")
                copy.ColumnMappings.Add("ParameterName", "ParameterName")
                copy.ColumnMappings.Add("CurrentValue", "CurrentValue")
                copy.ColumnMappings.Add("TargetValue", "TargetValue")

                copy.WriteToServer(dtData)
            End Using
        End Using
    End Sub

    Private Sub AddRowsToXmlJob()

        Dim selectedRows() As Integer = Nothing
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        Dim templateID As Integer = CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID"))

        If cmsGridControlName = "gcDetailedData" Then

            selectedRows = gvDetailedData.GetSelectedRows()
            Dim templateName As String = CStr(gvTemplateList.GetFocusedRowCellValue("TemplateName"))
            Dim templateMOCinfigID As Integer = CInt(gvMOConfig.GetFocusedRowCellValue("TemplateMOConfigID"))

            dtXmlInputRefCheck = New DataTable()
            dtXmlInputRefCheck.Columns.Add("XMLJobID", GetType(Integer))
            dtXmlInputRefCheck.Columns.Add("TemplateID", GetType(Integer))
            dtXmlInputRefCheck.Columns.Add("MoConfigID", GetType(Integer))
            dtXmlInputRefCheck.Columns.Add("Vendor", GetType(String))
            dtXmlInputRefCheck.Columns.Add("TemplateName", GetType(String))
            dtXmlInputRefCheck.Columns.Add("MO", GetType(String))
            dtXmlInputRefCheck.Columns.Add("ObjectName", GetType(String))
            dtXmlInputRefCheck.Columns.Add("ObjectConditionColumns", GetType(String))
            dtXmlInputRefCheck.Columns.Add("ParameterName", GetType(String))
            dtXmlInputRefCheck.Columns.Add("CurrentValue", GetType(String))
            dtXmlInputRefCheck.Columns.Add("TargetValue", GetType(String))

            For iCntr As Integer = 0 To selectedRows.Count - 1

                Dim newRow = dtXmlInputRefCheck.Rows.Add()
                newRow.Item("XMLJobID") = selectedXmlJobID
                newRow.Item("TemplateID") = templateID
                newRow.Item("MoConfigID") = gvDetailedData.GetRowCellValue(selectedRows(iCntr), "MoConfigID")
                newRow.Item("Vendor") = cmbVendor.SelectedItem.ToString
                newRow.Item("TemplateName") = templateName
                newRow.Item("MO") = gvDetailedData.GetRowCellValue(selectedRows(iCntr), "MO")
                newRow.Item("ObjectName") = gvDetailedData.GetRowCellValue(selectedRows(iCntr), "ObjectName")
                newRow.Item("ObjectConditionColumns") = gvDetailedData.GetRowCellValue(selectedRows(iCntr), "ObjectConditionColumns")
                newRow.Item("ParameterName") = gvDetailedData.GetRowCellValue(selectedRows(iCntr), "ParameterName")
                newRow.Item("CurrentValue") = gvDetailedData.GetRowCellValue(selectedRows(iCntr), "ParameterValue")
                newRow.Item("TargetValue") = gvDetailedData.GetRowCellValue(selectedRows(iCntr), "TemplateValue")

            Next

            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                If dtXmlInputRefCheck IsNot Nothing Then
                    InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[XML_InputRefCheck]", dtXmlInputRefCheck)
                    XtraMessageBox.Show("Selected rows added to xml job successfully!", "Add Rows to XML Job", MessageBoxButtons.OK)
                End If
            End If

        ElseIf cmsGridControlName = "gcInconSumm" Then

            selectedRows = gvInconSumm.GetSelectedRows()
            For iCntr As Integer = 0 To selectedRows.Count - 1

                parray = {
                    New String() {"@XMLJOBID", selectedXmlJobID},
                    New String() {"@TemplateID", templateID},
                    New String() {"@MoConfigID", gvInconSumm.GetRowCellValue(selectedRows(iCntr), "MoConfigID")},
                    New String() {"@ObjectConditionColumns", Chr(39) & gvInconSumm.GetRowCellValue(selectedRows(iCntr), "ObjectConditionColumns") & Chr(39)},
                    New String() {"@ParameterName", Chr(39) & gvInconSumm.GetRowCellValue(selectedRows(iCntr), "ParameterName") & Chr(39)},
                    New String() {"@ParameterValue", Chr(39) & gvInconSumm.GetRowCellValue(selectedRows(iCntr), "ParameterValue") & Chr(39)}
                }
                strConnection = GetSQL(4191, parray)(0)
                sqlParam = GetSQL(4191, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            Next

        End If

        parray = Nothing
        parray = {
            New String() {"@XMLJobID", selectedXmlJobID}
        }
        strConnection = GetSQL(4181, parray)(0)
        sqlParam = GetSQL(4181, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, iQryTimeOut)

    End Sub

    Private Sub RenameTemplate(ByVal templateID As Integer, ByVal templateName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateName", Chr(39) & templateName & Chr(39)},
            New String() {"@TemplateID", templateID}
        }
        strConnection = GetSQL(4190, parray)(0)
        sqlParam = GetSQL(4190, parray)(1)
        DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadResultsTabGrids()
        If xtcResultsInner.SelectedTabPageIndex = 0 Then
            If gcTemplateSumm.DataSource Is Nothing Then
                LoadTemplateSummaryGrid()
                ManageGridSelectionType(gcTemplateSumm.MainView)
            End If
        ElseIf xtcResultsInner.SelectedTabPageIndex = 1 Then
            If gcInconSumm.DataSource Is Nothing Then
                LoadInconsistencySummaryGrid()
                ManageGridSelectionType(gcInconSumm.MainView)
            End If
        ElseIf xtcResultsInner.SelectedTabPageIndex = 2 Then
            lblProcessedRows.Text = ""
            If gcDetailedData.DataSource Is Nothing Then
                RefreshLoadAllTemplates = False
                LoadDetailedDataGrid()
                ManageGridSelectionType(gcDetailedData.MainView)
            End If
        ElseIf xtcResultsInner.SelectedTabPageIndex = 3 Then
            If gcStatus.DataSource Is Nothing Then
                LoadStatusGrid()
                ManageGridSelectionType(gcStatus.MainView)
            End If
        ElseIf xtcResultsInner.SelectedTabPageIndex = 4 Then
            LoadResultChart1()
            LoadResultChart2()
            IOSDevExpressGrid.ClearGrid(grdChart2)
            ClearChart3()
        End If
    End Sub

    Private Sub ManageGridSelectionType(ByRef gv As GridView)
        If tsmiAllowCellCopy.Checked Then
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
        Else
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
        End If
    End Sub

    Private Sub DeleteFilterInAllMO(templateID As Integer, filterString As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateID", templateID},
            New String() {"@filterString", Chr(39) & filterString.Replace("'", "''") & Chr(39)}
        }
        strConnection = GetSQL(4193, parray)(0)
        sqlParam = GetSQL(4193, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub UpdateMOConfigCheckMissingNE(columnValue As Boolean)
        Dim dr As DataRow = gvMOConfig.GetFocusedDataRow()
        If dr IsNot Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", CInt(dr("TemplateMOConfigID"))},
                New String() {"@CheckMissingNE", IIf(columnValue = True, 1, 0)}
            }
            strConnection = GetSQL(4198, parray)(0)
            sqlParam = GetSQL(4198, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            Me.SaveChangeLog(CInt(gvTemplateList.GetFocusedRowCellValue("TemplateID")), CStr(dr("MOName")), CInt(dr("TemplateMOConfigID")), "CheckMissingNE modified to " & columnValue.ToString & " for the mo: " & dr("MOName").ToString)
        End If
    End Sub

    Private Sub ParameterMapping(MappingType As String, ByRef dtData As DataTable, MO As String, ParamName As String)
        Dim param_selected = ParamName & "_Map"
        If param_selected.Length > 27 Then
            param_selected = param_selected.Substring(0, 27)
        End If

        Dim tech As String = dtData.Rows(0)("IOS_TECH").ToString
        If dtData.Rows(0)("IOS_TECH").ToString.Contains("NanoBTS") Then
            tech = "NanoBTS"
        ElseIf dtData.Rows(0)("IOS_TECH").ToString.Contains("Nano3G") Then
            tech = "Nano3G"
        End If

        Dim col_param As String = Nothing
        Dim sql_paramOfCells As String = Nothing
        Dim stross As String = Nothing
        Dim drow() As DataRow = Nothing

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        'Dim dtParam As DataTable = clsSQLCommands.GetMODetailsForParameter(connStrIOSServer, ParamName, MO)

        If dtData.Rows.Count > 0 Then
            Try
                tech = dtData.Rows(0)("IOS_TECH").ToString 'dtParam.Rows(0)("Techn").ToString
                drow = dtData.Select("ParameterName = " & Chr(39) & ParamName & Chr(39) & " And MO = " & Chr(39) & MO & Chr(39))
                Dim db_table_name As String = dtData.Rows(0)("DB_table_name").ToString
                col_param = "mo." & dtData.Rows(0)("DB_column_name").ToString & Chr(32) & Chr(34) & param_selected & Chr(34)
                Dim parray()() As String = {
                    New String() {"@columns", col_param},
                    New String() {"@mo_table", dtData.Rows(0)("DB_table_name").ToString}
                }

                If tech.ToUpper = networkAll.Network3G1.ToUpper Then
                    stross = GetSQL(1033, parray)(0)
                    sql_paramOfCells = GetSQL(1033, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network2G1.ToUpper Then
                    stross = GetSQL(1032, parray)(0)
                    sql_paramOfCells = GetSQL(1032, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network2G2.ToUpper Then
                    stross = GetSQL(1514, parray)(0)
                    sql_paramOfCells = GetSQL(1514, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network2G3.ToUpper Then
                    stross = GetSQL(20011, parray)(0)
                    sql_paramOfCells = GetSQL(20011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network3G2.ToUpper Then
                    stross = GetSQL(9512, parray)(0)
                    sql_paramOfCells = GetSQL(9512, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network3G3.ToUpper Then
                    stross = GetSQL(30011, parray)(0)
                    sql_paramOfCells = GetSQL(30011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network4G1.ToUpper Then
                    stross = GetSQL(10011, parray)(0)
                    sql_paramOfCells = GetSQL(10011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network4G2.ToUpper Then
                    stross = GetSQL(15011, parray)(0)
                    sql_paramOfCells = GetSQL(15011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network4G3.ToUpper Then
                    stross = GetSQL(17011, parray)(0)
                    sql_paramOfCells = GetSQL(17011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network5G1.ToUpper Then
                    stross = GetSQL(51011, parray)(0)
                    sql_paramOfCells = GetSQL(51011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network5G2.ToUpper Then
                    stross = GetSQL(52011, parray)(0)
                    sql_paramOfCells = GetSQL(52011, parray)(1)
                ElseIf tech.ToUpper = networkAll.Network5G3.ToUpper Then
                    stross = GetSQL(53011, parray)(0)
                    sql_paramOfCells = GetSQL(53011, parray)(1)
                ElseIf tech.ToUpper = networkAll.NetworkNode1.ToUpper Then
                    stross = GetSQL(0, parray)(0)
                    sql_paramOfCells = GetSQL(0, parray)(1)
                ElseIf tech.ToUpper = networkAll.NetworkNode2.ToUpper Then
                    stross = GetSQL(0, parray)(0)
                    sql_paramOfCells = GetSQL(0, parray)(1)
                ElseIf tech.ToUpper = networkAll.NetworkNode3.ToUpper Then
                    stross = GetSQL(0, parray)(0)
                    sql_paramOfCells = GetSQL(0, parray)(1)
                End If

                If Not sql_paramOfCells.Contains("@motable") Then
                    sql_paramOfCells = Replace(sql_paramOfCells, "mo.", db_table_name + ".")
                End If

                Application.DoEvents()
                Dim dt_paramOfCells As DataTable = DataAccessorODBC.GetDataTable(stross, sql_paramOfCells)
                Dim dt_Map_Configuration As DataTable = GetDataTable("dt_Map_Configuration")
                Dim dtTechLayers As DataTable = dt_Map_Configuration.AsEnumerable().Where(Function(x) x.Field(Of String)("LayerTech") = tech.ToUpper).CopyToDataTable()
                Dim visibleLyrCntr As Integer = 0
                Dim processedLyrCntr As Integer = 0

                'checking visible map tech layers in the map
                For Each drTechLyr As DataRow In dtTechLayers.Rows
                    Dim lyr As MapInfo.Mapping.FeatureLayer = CType(frmMapWindow.MapControl1.Map.Layers(drTechLyr("LayerName").ToString.Trim), MapInfo.Mapping.FeatureLayer)
                    If lyr IsNot Nothing Then
                        If frmMapWindow.MapControl1.Map.Layers(drTechLyr("LayerName")).IsVisible = True Then
                            visibleLyrCntr = visibleLyrCntr + 1
                        End If
                    End If
                Next

                If dt_paramOfCells IsNot Nothing Then
                    'mapping with visible map layers only
                    For Each drw As DataRow In dtTechLayers.Rows
                        Dim lyr As MapInfo.Mapping.FeatureLayer = CType(frmMapWindow.MapControl1.Map.Layers(drw("LayerName").ToString.Trim), MapInfo.Mapping.FeatureLayer)
                        If lyr IsNot Nothing Then
                            If (drw("LayerActive") = True AndAlso drw("LayerTech").ToString.ToUpper = tech.ToUpper) AndAlso (frmMapWindow.MapControl1.Map.Layers(drw("LayerName")).IsVisible = True) Then

                                WaitScreen.ShowWaitScreen("Layers To Process: " & CInt(visibleLyrCntr - processedLyrCntr).ToString & vbCrLf & "Mapping Layer: " & drw("LayerName").ToString.Trim)

                                If MappingType = "cells" Then
                                    frmMapWindow.Parameter_Map(dt_paramOfCells, drw("LayerName").ToString.Trim, tech, param_selected)
                                ElseIf MappingType = "voronoi" Then
                                    frmMapWindow.Parameter_Map(dt_paramOfCells.Copy, drw("LayerName").ToString.Trim & "_Voronoi", tech, param_selected)
                                ElseIf MappingType = "label" Then
                                    frmMapWindow.ParameterLabel_Map(dt_paramOfCells, drw("LayerName").ToString.Trim, tech, param_selected)
                                End If

                                WaitScreen.CloseWaitScreen()
                                processedLyrCntr = processedLyrCntr + 1

                            End If
                        End If
                    Next

                    Dim GridCtrl_Map As GridControl = frmMapWindow.CreateTabWithGridViewForMapData("Parameters Of Cells")
                    Dim GridView_Map As GridView = GridCtrl_Map.MainView
                    IOSDevExpressGrid.PopulateDataInGrid(GridCtrl_Map, GridView_Map, dt_paramOfCells, "ALL")
                    dt_paramOfCells.Dispose()
                    dt_paramOfCells = Nothing
                End If

                'Else
                'create a thematic for selected table & field
                'Dim dt_Map_Configuration As DataTable = GetDataTable("dt_Map_Configuration")

                '    For Each drw As DataRow In dt_Map_Configuration.Rows
                '        If drw("LayerActive") = True And drw("LayerTech").ToString.ToUpper = tech.ToUpper Then
                '            Application.DoEvents()
                '            If MappingType = "cells" Then
                '                frmMapWindow.CreateThematicOfExistingTable(drw("LayerName").ToString.Trim, tn.SubItems(0).Text)
                '            ElseIf MappingType = "voronoi" Then
                '                frmMapWindow.CreateThematicOfExistingTable(drw("LayerName").ToString.Trim & "_Voronoi", tn.SubItems(0).Text)
                '            End If
                '        End If
                '    Next
                'End If

            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                Application.UseWaitCursor = False
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        End If
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Function GetParamListForSelectedMO(templateMOConfigID As Integer) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", templateMOConfigID}
        }

        strConnection = GetSQL(4106, parray)(0)
        sqlParam = GetSQL(4106, parray)(1)

        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub ClearFormData()
        IOSDevExpressGrid.ClearGrid(gcTemplateList)
        IOSDevExpressGrid.ClearGrid(gcMOFilter)
        IOSDevExpressGrid.ClearGrid(gcMOConfig)
        IOSDevExpressGrid.ClearGrid(gcCondition)
        IOSDevExpressGrid.ClearGrid(gcMOParam)
        IOSDevExpressGrid.ClearGrid(gcTemplateSumm)
        IOSDevExpressGrid.ClearGrid(gcInconSumm)
        IOSDevExpressGrid.ClearGrid(gcDetailedData)
        IOSDevExpressGrid.ClearGrid(gcStatus)
        IOSDevExpressGrid.ClearGrid(gcViewTemplate)
        IOSDevExpressGrid.ClearGrid(gcSearch)
        lstInclusion.DataSource = Nothing
        lstExclusion.DataSource = Nothing
        lstParamExclusion.DataSource = Nothing

        txtDescription.Text = String.Empty
        lblTemplateOwner.Text = String.Empty
        ceIsScheduled.Checked = False
        ceIsLocked.Checked = False
        ceIsEnabled.Checked = False
        lblLatestConfigChange.Text = String.Empty
        lblLastRunTime.Text = String.Empty
        lblLastCMDate.Text = String.Empty
        cmbSearch.SelectedIndex = 0
    End Sub

    Private Sub FillSearchCombo()
        RemoveHandler cmbSearch.SelectedIndexChanged, AddressOf cmbSearch_SelectedIndexChanged
        Dim dt As New DataTable
        Dim dr As DataRow = Nothing
        dt.Columns.Add("SearchItem", GetType(String))

        dr = dt.NewRow()
        dr("SearchItem") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SearchItem") = "Filter String"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SearchItem") = "Object Inclusion/Exclusion"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SearchItem") = "Param Exclusion"
        dt.Rows.Add(dr)

        BindDevExComboBoxWithValueMember(cmbSearch, dt, "SearchItem", "SearchItem")
        cmbSearch.SelectedIndex = 0
        AddHandler cmbSearch.SelectedIndexChanged, AddressOf cmbSearch_SelectedIndexChanged
    End Sub

    Private Sub LoadAllFilterStrings()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        Dim parray()() As String = Nothing
        strConnection = GetSQL(4206, parray)(0)
        sqlParam = GetSQL(4206, parray)(1)
        dtFilterStrings = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"TemplateID", "TemplateMOConfigID", "TemplateMOFilterID"}
        IOSDevExpressGrid.PopulateDataInGrid(gcSearch, gvSearch, dtFilterStrings, "ALL", columnsToHide) ', "FilterString"
    End Sub

    Private Sub LoadAllIncExcObjects()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        Dim parray()() As String = Nothing
        strConnection = GetSQL(4207, parray)(0)
        sqlParam = GetSQL(4207, parray)(1)
        dtIncExcObjects = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"ListID", "TemplateMOConfigID", "TemplateID"}
        IOSDevExpressGrid.PopulateDataInGrid(gcSearch, gvSearch, dtIncExcObjects, "ALL", columnsToHide) ', "ListName"
    End Sub

    Private Sub LoadAllParamExclusion()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        Dim parray()() As String = Nothing
        strConnection = GetSQL(4208, parray)(0)
        sqlParam = GetSQL(4208, parray)(1)
        dtExcludedParams = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"TemplateID"}
        IOSDevExpressGrid.PopulateDataInGrid(gcSearch, gvSearch, dtExcludedParams, "ALL", columnsToHide)    ', "ParameterName"
    End Sub

    Private Sub DeleteFilterListAllMO(templateID As Integer, listID As Integer, listType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateID},
            New String() {"@ListID", listID},
            New String() {"@ListType", Chr(39) & listType & Chr(39)}
        }
        strConnection = GetSQL(4214, parray)(0)
        sqlParam = GetSQL(4214, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

#End Region

#Region "Infinite Scrolling"

    Private dtDetailedData As New DataTable
    Private objLock As New Object
    Private queryOffset As Integer = 0
    Private batchSize As Integer = 1000
    Private isFirstTimeLoading As Boolean = False
    Private currViewRowFilter As String = ""
    Private currViewSortStr As String = ""
    Private datetimeEdit As RepositoryItemDateEdit
    Private _virtualServerModeSrouce As VirtualServerModeSource

    Private Function CreateData(ByVal offset As Integer, ByVal batchSize As Integer, Optional currentRowFilter As String = Nothing, Optional sortExpression As String = Nothing) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim columnList As String = Nothing
        Dim filterQry As String = Nothing

        If currentRowFilter IsNot Nothing AndAlso currentRowFilter <> "" Then
            currentRowFilter = currentRowFilter.Replace("[", "x.[")
            filterQry = IIf(filterQry Is Nothing, " AND ", " ") & currentRowFilter.Replace("'", "''")
        End If

        Dim dt As DataTable = Nothing

        If tglSwitchAllTemplates.IsOn AndAlso RefreshLoadAllTemplates = True Then
            If filterQry Is Nothing Then
                batchSize = 1
            End If
            Dim parray()() As String = {
                New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString & Chr(39)},
                New String() {"@n", offset},
                New String() {"@m", batchSize},
                New String() {"@filter", Chr(39) & filterQry & Chr(39)},
                New String() {"@sortExpr", Chr(39) & sortExpression & Chr(39)}
            }
            strConnection = GetSQL(4201, parray)(0)
            sqlParam = GetSQL(4201, parray)(1)
            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, 120)
        Else
            Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
            Dim parray()() As String = {
                New String() {"@TemplateID", CInt(dr("TemplateID"))},
                New String() {"@n", offset},
                New String() {"@m", batchSize},
                New String() {"@filter", Chr(39) & filterQry & Chr(39)},
                New String() {"@sortExpr", Chr(39) & sortExpression & Chr(39)}
            }
            strConnection = GetSQL(4145, parray)(0)
            sqlParam = GetSQL(4145, parray)(1)
            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        End If

        Return dt
    End Function

    Private Sub LoadDataToGrid()
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.UseWaitCursor = True
            'Application.DoEvents()

            dtDetailedData = Nothing
            queryOffset = 0
            isFirstTimeLoading = True
            currViewRowFilter = ""
            currViewSortStr = ""

            If (_virtualServerModeSrouce IsNot Nothing) Then
                RemoveHandler _virtualServerModeSrouce.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
                RemoveHandler _virtualServerModeSrouce.ConfigurationChanged, AddressOf virtualServerModeSource_ConfigurationChanged
                RemoveHandler _virtualServerModeSrouce.MoreRows, AddressOf VirtualServerModeSource_MoreRows
                RemoveHandler _virtualServerModeSrouce.GetUniqueValues, AddressOf virtualServerModeSource_GetUniqueValues
            End If

            _virtualServerModeSrouce = New VirtualServerModeSource()

            AddHandler _virtualServerModeSrouce.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
            AddHandler _virtualServerModeSrouce.ConfigurationChanged, AddressOf virtualServerModeSource_ConfigurationChanged
            AddHandler _virtualServerModeSrouce.MoreRows, AddressOf VirtualServerModeSource_MoreRows
            AddHandler _virtualServerModeSrouce.GetUniqueValues, AddressOf virtualServerModeSource_GetUniqueValues

            gcDetailedData.DataSource = Nothing
            gvDetailedData.OptionsView.ColumnAutoWidth = False
            gvDetailedData.Columns.Clear()
            gcDetailedData.DataSource = _virtualServerModeSrouce

            gvDetailedData.Columns("ParameterValue").Width = 500
            gvDetailedData.Columns("ParameterValue").OptionsColumn.FixedWidth = True

            gvDetailedData.Columns("TemplateValue").Width = 500
            gvDetailedData.Columns("TemplateValue").OptionsColumn.FixedWidth = True

            If dtDetailedData IsNot Nothing Then
                For Each dtCol As DataColumn In dtDetailedData.Columns
                    gvDetailedData.Columns(dtCol.ColumnName).BestFit()
                Next
            End If

        Catch ex As Exception
            'Finally
            '    Me.Cursor = Cursors.Default
            '    Application.UseWaitCursor = False
            '    Application.DoEvents()
        End Try
    End Sub

    Private Sub VirtualServerModeSource_AcquireInnerList(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeAcquireInnerListEventArgs)
        Try
            Dim dtTempColumn As New DataTable
            If dtDetailedData Is Nothing Then
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
                gvDetailedData.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Indicator
            Else
                gvDetailedData.OptionsView.WaitAnimationOptions = WaitAnimationOptions.Panel
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

                          If dtDetailedData IsNot Nothing Then
                              dtDetailedData.Merge(dtData)
                          Else
                              dtDetailedData = dtData
                          End If
                          queryOffset = dtDetailedData.Rows.Count
                          Dim nextBatch = dtDetailedData.Clone()

                          Do While nextBatch.Rows.Count < dtData.Rows.Count
                              nextBatch.ImportRow(dtDetailedData.Rows(rowCount))
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
            dtDetailedData = Nothing

            currViewRowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(e.ConfigurationInfo.Filter)
            If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                currViewSortStr = e.ConfigurationInfo.SortInfo(0).ToString()
            End If

            Dim dtData As New DataTable
            If tglSwitchAllTemplates.IsOn AndAlso RefreshLoadAllTemplates = True Then
                dtData = CreateData(queryOffset, batchSize, currViewRowFilter, currViewSortStr)
            Else
                dtData = CreateData(queryOffset, batchSize, currViewRowFilter, currViewSortStr)
            End If

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
                If tglSwitchAllTemplates.IsOn AndAlso RefreshLoadAllTemplates = True Then
                    Dim dt As New DataTable
                    Dim strSql As String = Nothing
                    Dim strPreFilter As String = Nothing
                    If currViewRowFilter Is Nothing Or currViewRowFilter = "" Then
                        strSql = "EXEC [dbo].[sp_Get_RefCheck_DetailedData_DistColumnAll] '" & cmbVendor.SelectedItem.ToString & "','[" & e.ValuesPropertyName & "]'"
                    Else
                        strPreFilter = currViewRowFilter.Replace("'", "''")
                        strPreFilter = strPreFilter.Replace("[", "x.[")
                        strSql = "EXEC [dbo].[sp_Get_RefCheck_DetailedData_DistColumnAll] '" & cmbVendor.SelectedItem.ToString & "','[" & e.ValuesPropertyName & "]','" & strPreFilter & "'"
                    End If
                    dt = DataAccessorODBC.GetDataTable(connStrIOSServer, strSql)
                    Dim filterValue() As Object = Nothing
                    If dt IsNot Nothing Then
                        filterValue = dt.Rows.OfType(Of DataRow)().Select(Function(x) x.Item(0)).ToArray()
                    End If
                    Return filterValue
                Else
                    Dim dr As DataRow = gvTemplateList.GetFocusedDataRow()
                    If dr IsNot Nothing Then
                        Dim dt As New DataTable
                        Dim strSql As String = Nothing
                        Dim strPreFilter As String = Nothing
                        If currViewRowFilter Is Nothing Or currViewRowFilter = "" Then
                            strSql = "EXEC [dbo].[sp_Get_RefCheck_DetailedData_DistColumn] " & CInt(dr("TemplateID")) & ",'[" & e.ValuesPropertyName & "]'"
                        Else
                            strPreFilter = currViewRowFilter.Replace("'", "''")
                            strSql = "EXEC [dbo].[sp_Get_RefCheck_DetailedData_DistColumn] " & CInt(dr("TemplateID")) & ",'[" & e.ValuesPropertyName & "]','" & strPreFilter & "'"
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
                End If
                Return Nothing
            End Function, e.CancellationToken)
    End Sub

#End Region

End Class

Class GetResultsClass

    Public templateID As Integer
    Public Status As Integer
    Public templateRow As DataRow
    Public Event ThreadComplete(row As DataRow, Status As Integer, ByVal ti As Threading.Thread)

    Sub RunNow()
        Try
            Status = 1
            UpdateTemplateLastStatus(templateID, Status)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@templateID", templateID}
            }

            strConnection = GetSQL(4148, parray)(0)
            sqlParam = GetSQL(4148, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, 600)
            Status = 0
            UpdateTemplateLastStatus(templateID, Status)
        Catch ex As Exception
            Status = -1
            UpdateTemplateLastStatus(templateID, Status)
        Finally
            RaiseEvent ThreadComplete(templateRow, Status, Threading.Thread.CurrentThread)
        End Try
    End Sub

End Class