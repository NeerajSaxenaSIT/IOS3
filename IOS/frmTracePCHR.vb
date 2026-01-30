Imports DevExpress.XtraGrid.Views.Grid
Imports dotnetCHARTING.WinForms
Imports IOS.DataLibrary
Imports IOS.Library
Imports IOS.Library.PCHRTab
Imports LidorSystems.IntegralUI.Lists

Public Class frmTracePCHR

#Region "Variables"

    Private dsPCHR_CS As DataSet = Nothing
    Private dsPCHR_PS As DataSet = Nothing
    Private dtPCHRProjects As DataTable = Nothing
    Private istoshowpchrgridcontextmeunstrip As Boolean = False
    Private fileID As String = Nothing
    Private callrecNUM As String = Nothing
    Private isCellTabAlreadyFiltered As Boolean = False
    Private isIMSITabAlreadyFiltered As Boolean = False
    Private TreeView_SearchFound As Integer
    Private Treeview_NodeFound As Boolean = False
    Private dtOverviewIMSIBarChart As DataTable = New DataTable()
    Private isRequestByContextMenu As Boolean = False
    Private dtOverviewCellBarChart As DataTable
    Private cmPCHR_SourceControlCellGridHidShow As Control

#End Region

#Region "Form Events"

    Private Sub frmTracePCHR_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        objFrmPCHR = Nothing
    End Sub

    Private Sub frmTracePCHR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            RefrashProjectTreeListView()
            ConfigurPCHRForm(Me.Name)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Form Control Event"

    Private Sub xtcPCHROverCellIMSIUE_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcPCHROverCellIMSIUE.SelectedPageChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag IsNot Nothing) Then
                If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
                    SplitContPCHRCellBarChartGrid.Panel2Collapsed = True
                    If (Not isCellTabAlreadyFiltered) Then
                        BindOverviewCellTab()
                        isCellTabAlreadyFiltered = True
                    End If
                ElseIf (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                    If (Not isIMSITabAlreadyFiltered) Then
                        BindOverviewIMSITab()
                        isIMSITabAlreadyFiltered = True
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCreateProjectPCHR_Click(sender As Object, e As EventArgs) Handles btnCreateProjectPCHR.Click
        Dim projectManagePCHRDialog As New dlgProjectManagePCHR()
        projectManagePCHRDialog.ShowDialog()
    End Sub

    Private Sub btnRefreshPCHR_Click(sender As Object, e As EventArgs) Handles btnRefreshPCHR.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            GetPCHRProjects()
            RefrashProjectTreeListView()
            If (dsPCHR_CS IsNot Nothing) Then
                dsPCHR_CS.Clear()
            End If
            If (dsPCHR_PS IsNot Nothing) Then
                dsPCHR_PS.Clear()
            End If

            chkPCHRPS.Checked = False
            chkPCHRCS.Checked = False

            txtCELL.Text = ""
            txtIMSI.Text = ""
            txtRNC.Text = ""
            txtTAC.Text = ""
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tlvProjectsPCHR_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvProjectsPCHR.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Application.DoEvents()
            If (tlvProjectsPCHR.SelectedNode IsNot Nothing) Then
                Dim projectName As String = tlvProjectsPCHR.SelectedNode.Text
                Dim projectid As String = tlvProjectsPCHR.SelectedNode.Key
                InsertRootNodeInBlockTypeTlv()
                Dim parray()() As String = {New String() {"@projectid", Chr(39) & projectid & Chr(39)}}
                Dim sqlCommandAndConnection() As String = GetSQL(IOSSqlIds.PCHR_PROJECTSDATA, parray, dt_IOS_SQL)
                Dim dtRNC As DataTable = DataAccessorSQL.ExecuteDataTable(sqlCommandAndConnection(0), sqlCommandAndConnection(1))
                If (Not dtRNC Is Nothing) Then
                    If (dtRNC.Rows.Count >= 1) Then
                        txtIMSI.Text = dtRNC.Rows(0)("IMSIValue").ToString()
                        txtRNC.Text = dtRNC.Rows(0)("RNCValue").ToString()
                        dtpPCHRFilterStartDate.EditValue = dtRNC.Rows(0)("MinStartuptime").ToString()
                        dtpPCHRFilterStartDate.Properties.Mask.UseMaskAsDisplayFormat = True
                        dtpPCHRFilterEndDate.EditValue = dtRNC.Rows(0)("MaxStartuptime").ToString()
                        dtpPCHRFilterEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
                    End If
                Else
                    Exit Sub
                End If
            Else
                tlvProjectsPCHR.SelectedNode = tlvProjectsPCHR.Nodes(0)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub xtcPCHRGridCSPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles xtcPCHRGridCSPS.SelectedPageChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (isRequestByContextMenu) Then
                isRequestByContextMenu = False
                Exit Sub
            End If

            If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                If (dsPCHR_CS IsNot Nothing) Then
                    SetPCHR_OverviewCSPSGridData(dsPCHR_CS.Tables(1), "CS")
                    OverviewCharts(dsPCHR_CS.Tables(1), PCHRTab.OverviewGridType.CS)
                End If
            End If
            If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                If (dsPCHR_PS IsNot Nothing) Then
                    SetPCHR_OverviewCSPSGridData(dsPCHR_PS.Tables(1), "PS")
                    OverviewCharts(dsPCHR_PS.Tables(1), PCHRTab.OverviewGridType.PS)
                End If
            End If

            If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
                SplitContPCHRCellBarChartGrid.Panel2Collapsed = True
                BindOverviewCellTab()
            ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                BindOverviewIMSITab()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try

    End Sub

    Private Sub tsmiPCHR_BackToProjectLevel_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_BackToProjectLevel.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
                Dim dtCauseErrorCounter As DataTable = GetCellPieChartData()
                If (dtCauseErrorCounter IsNot Nothing) Then
                    Dim chartPCHR As ChartPCHR = New ChartPCHR()
                    chartPCHR.OverviewPieChart(chPCHRCellErrorPie, dtCauseErrorCounter, "Error Causes - Cell :" & cmbPCHRCellShow.SelectedItem.ToString)
                End If
            ElseIf (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                Dim dtCauseErrorCounter As DataTable = GetIMSIPieChartData()
                If (dtCauseErrorCounter IsNot Nothing) Then
                    Dim chartPCHR As ChartPCHR = New ChartPCHR()
                    chartPCHR.OverviewPieChart(chPCHRIMSIErrorPie, dtCauseErrorCounter, "Error Causes - IMSI :" & cmbPCHRIMSIShow.SelectedItem.ToString)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmsPCHRBackToProjectLevel_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsPCHRBackToProjectLevel.Opening
        If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
            If (chPCHRCellErrorPie.Title.Contains("Cell:")) Then
                cmsPCHRBackToProjectLevel.Show()
            Else
                cmsPCHRBackToProjectLevel.Hide()
            End If
        ElseIf (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
            If (chPCHRCellErrorPie.Title.Contains("IMSI:")) Then
                cmsPCHRBackToProjectLevel.Show()
            Else
                cmsPCHRBackToProjectLevel.Hide()
            End If
        End If
    End Sub

    Private Sub cmsPCHR_GridViewOverviewCSPS_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsPCHR_GridViewOverviewCSPS.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            Dim overviewSCPSGridTmp As DevExpress.XtraGrid.GridControl = TryCast(cmsTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            If Not overviewSCPSGridTmp Is Nothing Then
                tsmiPCHR_CountRecord.Text = "Record Count: " & overviewSCPSGridTmp.DefaultView.RowCount
            End If

            If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper = "CS" Or xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper = "PS") Then
                tsmiPCHR_CopyAll.Enabled = True
                'tsmiPCHR_CopySelectionWOHeader.Enabled = True
                tsmiPCHR_CopySelectionWithHeader.Enabled = True
                tsmiPCHR_ExportExcel.Enabled = True
                tsmi_RightSideTreeView.Enabled = True
                tsmiPCHR_GetRadio.Enabled = True
                tsmiPCHR_GetRadioAll.Enabled = True
                tsmiPCHR_CloseRadioTab.Enabled = IIf(xtcPCHRGridCSPS.TabPages.Count > 2, True, False)
                tsmiPCHR_GetTreeDataForCallRecord.Enabled = True
                tsmiPCHR_GetMsgFlowDataForCallRecord.Enabled = True
            Else
                tsmiPCHR_CopyAll.Enabled = False
                '  tsmiPCHR_CopySelectionWOHeader.Enabled = False
                tsmiPCHR_CopySelectionWithHeader.Enabled = False
                tsmiPCHR_ExportExcel.Enabled = False
                tsmi_RightSideTreeView.Enabled = False
                tsmiPCHR_GetRadio.Enabled = False
                tsmiPCHR_GetRadioAll.Enabled = False
                tsmiPCHR_CloseRadioTab.Enabled = IIf(xtcPCHRGridCSPS.TabPages.Count > 2, True, False)
                tsmiPCHR_GetTreeDataForCallRecord.Enabled = False
                tsmiPCHR_GetMsgFlowDataForCallRecord.Enabled = False
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub xtbcPCHR_MsgFlowRadio_Resize(sender As Object, e As EventArgs) Handles xtcPCHRMsgFlowRadio.Resize
        tbllayout_RadioCharts.Width = xtcPCHRMsgFlowRadio.Width - 40
    End Sub

    Private Sub xtbcPCHR_OverCellIMSIUE_Resize(sender As Object, e As EventArgs) Handles xtcPCHROverCellIMSIUE.Resize
        tbllpOverviewTab.Width = xtcPCHROverCellIMSIUE.Width - 40
    End Sub

    Private Sub btnUpdateGrid_Click(sender As Object, e As EventArgs) Handles btnUpdateGrid.Click
        If (tlvProjectsPCHR.SelectedNode Is Nothing) Then
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If chkPCHRCS.CheckState = CheckState.Checked Then
                xtcPCHRGridCSPS.SelectedTabPage = xtcPCHRGridCSPS.TabPages(0)
            ElseIf chkPCHRPS.CheckState = CheckState.Checked Then
                xtcPCHRGridCSPS.SelectedTabPage = xtcPCHRGridCSPS.TabPages(1)
            End If

            InsertRootNodeInBlockTypeTlv()
            GetDataForCS()
            GetDataForPS()
            isCellTabAlreadyFiltered = False
            isIMSITabAlreadyFiltered = False
            Dim selectCommand As String = Nothing
            If (txtIMSI.Text.Length > 0) Then
                selectCommand = "IMSI='" & txtIMSI.Text.Trim & "'"
            End If

            If (selectCommand IsNot Nothing) Then
                If (txtCELL.Text.Length > 0) Then
                    selectCommand = (selectCommand & " OR Convert(BestCellId, 'System.String') LIKE '%" & txtCELL.Text.Trim & "%' OR Convert(RabSetupAttemptCellId, 'System.String') LIKE '%" & txtCELL.Text.Trim & "%'")
                End If
            Else
                If (txtCELL.Text.Length > 0) Then
                    selectCommand = (" Convert(BestCellId, 'System.String') LIKE '%" & txtCELL.Text.Trim & "%' OR Convert(RabSetupAttemptCellId, 'System.String') LIKE '%" & txtCELL.Text.Trim & "%'")
                End If
            End If

            If (selectCommand IsNot Nothing And False) Then
                If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                    Dim vdgvPCHR_CS As DataTable = TryCast(dgvPCHROverviewCS.DataSource, DataTable)

                    Dim filterWithDate = String.Format(("ConnSetupTime").ToString() & " >= #{0:MM/dd/yyyy  hh:mm:ss}#", dtpPCHRFilterStartDate.EditValue) & " AND " & String.Format(("ConnSetupTime").ToString() & " <= #{0:MM/dd/yyyy  hh:mm:ss}#", dtpPCHRFilterEndDate.EditValue)

                    Dim filteredRowst As DataRow() = vdgvPCHR_CS.Select(selectCommand & " AND " & filterWithDate)
                    If (filteredRowst.Count > 0) Then
                        Dim dt As DataTable = filteredRowst.CopyToDataTable()

                        Dim filteredRows As DataRow() = dt.Select(filterWithDate)
                        If (filterWithDate.Count > 0) Then
                            SetPCHR_OverviewCSPSGridData(filteredRows.CopyToDataTable, "CS")
                            OverviewCharts(dsPCHR_CS.Tables(1), PCHRTab.OverviewGridType.CS)
                        End If
                    End If
                ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                    Dim vdgvPCHR_PS As DataTable = TryCast(dgvPCHROverviewPS.DataSource, DataTable)
                    Dim filteredRows As DataRow() = vdgvPCHR_PS.Select(selectCommand)
                    If (filteredRows.Count > 0) Then
                        Dim filterWithDate = From filteredRow In filteredRows.AsEnumerable
                                             Where (filteredRow.Field(Of Date)("ConnSetupTime") >= Convert.ToDateTime(dtpPCHRFilterStartDate.EditValue) And filteredRow.Field(Of Date)("ConnSetupTime") <= Convert.ToDateTime(dtpPCHRFilterEndDate.EditValue))
                        If (filterWithDate.Count > 0) Then
                            SetPCHR_OverviewCSPSGridData(filterWithDate.CopyToDataTable, "PS")
                            OverviewCharts(dsPCHR_PS.Tables(1), PCHRTab.OverviewGridType.PS)
                        End If
                    End If
                End If
            Else
                If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                    If (dsPCHR_CS IsNot Nothing) Then
                        SetPCHR_OverviewCSPSGridData(dsPCHR_CS.Tables(1), "CS")
                        OverviewCharts(dsPCHR_CS.Tables(1), PCHRTab.OverviewGridType.CS)
                    End If

                ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                    If (dsPCHR_PS IsNot Nothing) Then
                        SetPCHR_OverviewCSPSGridData(dsPCHR_PS.Tables(1), "PS")
                        OverviewCharts(dsPCHR_PS.Tables(1), PCHRTab.OverviewGridType.PS)
                    End If
                End If
            End If

            If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag IsNot Nothing) Then
                If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
                    SplitContPCHRCellBarChartGrid.Panel2Collapsed = True
                    BindOverviewCellTab()
                    isCellTabAlreadyFiltered = True
                ElseIf (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                    BindOverviewIMSITab()
                    isIMSITabAlreadyFiltered = True
                End If

            End If

            'System.Threading.Thread.CurrentThread.CurrentUICulture = Globalization.CultureInfo.GetCultureInfo("en-US")
            'System.Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.GetCultureInfo("en-US")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tsmiPCHR_CellChartShowRadioData_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CellChartShowRadioData.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
                If Not cmsPCHRCellChartGridHideShow.Tag Is Nothing Then
                    Dim item As String = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    If Not IsNumeric(item) Then

                        If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                            Dim dr() As DataRow = CType(dgvPCHROverviewCS.DataSource, DataTable).Select("CellName_Setup = '" & item & "' OR CellName_Release='" & item & "'")
                            If dr.Count > 0 Then
                                Dim setupcell As String = dr(0)("CS_RAB_Setup_Cell")
                                Dim releasecell As String = dr(0)("CS_RAB_Release_Cell")
                                If setupcell <> "-1" Then item = setupcell Else item = releasecell

                            End If
                        ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                            Dim dr() As DataRow = CType(dgvPCHROverviewPS.DataSource, DataTable).Select("CellName_Setup = '" & item & "' OR CellName_Release='" & item & "'")
                            If dr.Count > 0 Then
                                Dim setupcell As String = dr(0)("PS_RAB_Setup_Cell")
                                Dim releasecell As String = dr(0)("PS_RAB_Release_Cell")
                                If setupcell <> "-1" Then item = setupcell Else item = releasecell
                            End If
                        End If
                    End If
                    SetRadioChart(item, RadioValue.CellSetup)
                End If
            ElseIf (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                If Not cmsPCHRCellChartGridHideShow.Tag Is Nothing Then
                    Dim item As String = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    SetRadioChart(item, RadioValue.IMSI)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub txtPCHRSearchBlockType_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPCHRSearchBlockType.KeyDown
        If e.KeyCode = Keys.Enter Then
            If (tlvPCHRBlockType.SelectedNode Is Nothing) Then
                tlvPCHRBlockType.SelectedNode = tlvPCHRBlockType.Nodes(0)
            End If
            txtObject_KeyDown(tlvPCHRBlockType, txtPCHRSearchBlockType.Text.Trim, e)
        End If
    End Sub

    Public Sub txtObject_KeyDown(ByRef tree As TreeView, ByVal text As String, ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim tn As TreeNode = tree.SelectedNode
        If Not tn Is Nothing Then
            If e.KeyCode = Keys.Enter Then
                Treeview_NodeFound = False
                If TreeView_SearchFound = 0 Then
                    TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn), True)
                    If Treeview_NodeFound = False Then
                        TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn))
                    End If
                Else
                    TreeView_SearchFound = 0
                    TreeView_SearchWildCard(tree.Nodes(0), text, 0, False)
                End If

                If tn.Index = tree.SelectedNode.Index Then
                    tn.EnsureVisible()
                    tn.BackColor = Color.Coral
                Else
                    tn.BackColor = Color.White
                End If
            End If
        End If
    End Sub

#End Region

#Region "PCHR Helper"

    Private Function FindDgvInControl(ByRef sourceControl As Control) As DevExpress.XtraGrid.GridControl
        Dim dgv As DevExpress.XtraGrid.GridControl = Nothing
        For Each ctrl As Control In sourceControl.Controls
            If TypeOf ctrl Is DevExpress.XtraGrid.GridControl Then
                dgv = ctrl
            Else
                If ctrl.HasChildren Then
                    dgv = FindDgvInControl(ctrl)
                End If
            End If
        Next
        Return dgv
    End Function

    Public Sub ExportAllDataGridToExcel(ByRef tbc As DevExpress.XtraTab.XtraTabControl)
        Dim savefiledialog1 As New System.Windows.Forms.SaveFileDialog()
        savefiledialog1.FileName = ""
        savefiledialog1.Filter = "Excel Workbook |*.xlsx"

        If savefiledialog1.ShowDialog <> DialogResult.OK Then
            Exit Sub
        End If

        Dim fp As String = savefiledialog1.FileName

        Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = Nothing

        If IO.File.Exists(fp) Then
            IO.File.Delete(fp)
        End If

        Try
            xlApp = New Microsoft.Office.Interop.Excel.Application
            xlWorkBook = xlApp.Workbooks.Add()
            'xlWorkBook.Worksheets.Delete()

            For Each vtp As DevExpress.XtraTab.XtraTabPage In tbc.TabPages
                Dim dgv As DevExpress.XtraGrid.GridControl = FindDgvInControl(vtp)
                If dgv.MainView.RowCount > 0 Then
                    Dim xlWorksheet As Microsoft.Office.Interop.Excel.Worksheet = CType(xlWorkBook.Worksheets.Add(), Microsoft.Office.Interop.Excel.Worksheet)
                    xlWorksheet.Name = vtp.Text.Substring(0, IIf(vtp.Text.Length > 30, 30, vtp.Text.Length - 1))
                    Dim gView As GridView = dgv.MainView
                    xlWorksheet.Range("A1").Resize(dgv.MainView.RowCount + 1, gView.Columns.Count - 1).Value = IOSDevExpressGrid.CopyAllDataFromGridToArray(dgv, dgv.MainView)
                    xlWorksheet = Nothing
                End If
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Application.DoEvents()
            xlWorkBook.SaveAs(fp)
            If Not xlWorkBook Is Nothing Then
                xlWorkBook.Close(SaveChanges:=True)
            End If
            If Not xlApp Is Nothing Then
                xlApp.Quit()
            End If

            xlApp = Nothing
            xlWorkBook = Nothing
        End Try

    End Sub

    Private Sub ConfigurPCHRForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                tsmiDeleteProjectPCHR, tsmiRenameProjectPCHR, tsmiPropertyProjectPCHR, tsmiPCHR_CellChart_Copy, tsmiPCHR_CellChartShowRadioData, tsmiPCHR_CellChart_GetAll, tsmiPCHR_CellAndIMSIBarChartShowTopX, tsmiPCHR_CellChartGridHideShow,
                tsmiPCHR_CellChart_MapAll, tsmiPCHR_CellChart_MapItem, tsmiPCHR_CellChart_FilterGrid, tsmiPCHR_BackToProjectLevel
            }

            For Each frmControl As Object In formControls
                winCtrl = form.FindControlByName(frmControl.Name)
                If Not winCtrl Is Nothing Then
                    frmControl.Enabled = winCtrl.DefaultEnable
                    frmControl.Visible = winCtrl.DefaultVisible
                End If
            Next

        End If
    End Sub

    Private Sub GetPCHRProjects()
        dtPCHRProjects = IOS.DataLibrary.clsSQLCommands.Get_PCHRProjects_Data(connStrIOSServer)
    End Sub

    Public Sub RefrashProjectTreeListView()
        GetPCHRProjects()
        If (dtPCHRProjects IsNot Nothing) Then
            If (dtPCHRProjects.Rows.Count >= 1) Then
                tlvProjectsPCHR.Nodes.Clear()
                tlvProjectsPCHR.SuspendLayout()
                If (dtPCHRProjects Is Nothing) Then
                    GetPCHRProjects()
                End If
                BindProjectTreeListView(tlvProjectsPCHR, dtPCHRProjects)
                tlvProjectsPCHR.SuspendUpdate()
                SetTreeListView1ColumnsWidth()
                tlvProjectsPCHR.ResumeUpdate()
            End If
        End If
    End Sub

    Private Sub BindProjectTreeListView(ByRef tlvProjectsPCHR As LidorSystems.IntegralUI.Lists.TreeListView, ByVal dt As DataTable)
        If (dt.Rows.Count > 0) Then
            tlvProjectsPCHR.SuspendUpdate()
            tlvProjectsPCHR.Nodes.Clear()
            For Each drow As DataRow In dt.Rows
                Dim nodeRoot As TreeListViewNode = New TreeListViewNode(drow("ProjectName").ToString)
                nodeRoot.Key = drow("ProjectId")
                nodeRoot.Tag = drow("ProjectOwner")
                CreateSubItems(nodeRoot, drow)
                tlvProjectsPCHR.Nodes.Add(nodeRoot)
            Next
            tlvProjectsPCHR.Refresh()
            tlvProjectsPCHR.ResumeUpdate()
            tlvProjectsPCHR.UpdateCurrentView()
        End If
    End Sub

    Sub SetTreeListView1ColumnsWidth()
        If (tlvProjectsPCHR.Columns.Count > 2) Then
            tlvProjectsPCHR.Columns(0).FixedWidth = False
            tlvProjectsPCHR.Columns(0).Width = 90
            tlvProjectsPCHR.Columns(1).FixedWidth = False
            tlvProjectsPCHR.Columns(1).Width = 85
            tlvProjectsPCHR.Columns(2).FixedWidth = False
            tlvProjectsPCHR.Columns(2).Width = 70
        End If
    End Sub

    Private Sub CreateSubItems(ByRef parentNode As TreeListViewNode, ByVal dtRow As DataRow)
        Dim projectSubNode As New TreeListViewSubItem(dtRow("ProjectName").ToString)
        parentNode.SubItems.Add(projectSubNode)

        Dim progressSubNode As New TreeListViewSubItem()
        Dim vProgress As LidorSystems.IntegralUI.Controls.ProgressBar = New LidorSystems.IntegralUI.Controls.ProgressBar()
        vProgress.Maximum = CInt(dtRow("TotalFiles"))
        vProgress.Minimum = 1
        vProgress.Value = CInt(dtRow("ParsedFiles"))

        If CInt(dtRow("ParseStatus")) = 1 Then
            vProgress.ColorStyle.ProgressColor = Color.Yellow
        ElseIf CInt(dtRow("ParseStatus")) = 2 Then
            vProgress.ColorStyle.ProgressColor = Color.Green
        End If

        progressSubNode.Control = vProgress
        parentNode.SubItems.Add(progressSubNode)

        Dim ownerSubNode As New TreeListViewSubItem(dtRow("ProjectOwner").ToString)
        parentNode.SubItems.Add(ownerSubNode)
    End Sub

    Private Sub InsertRootNodeInBlockTypeTlv()
        tlvPCHRBlockType.Nodes.Clear()
        If (tlvProjectsPCHR.SelectedNode IsNot Nothing) Then
            WindowsTreeView.InsertNode(tlvPCHRBlockType, tlvProjectsPCHR.SelectedNode.Key & ":" & tlvProjectsPCHR.SelectedNode.Text)
        Else
            WindowsTreeView.InsertNode(tlvPCHRBlockType, "Project Name")
        End If
    End Sub

    Private Function GetCSorPS_Data(ByVal byCSorPS As PCHTType) As DataSet
        Dim dtRNC As DataSet = Nothing
        If (tlvProjectsPCHR.SelectedNode IsNot Nothing) Then
            Dim projectName As String = tlvProjectsPCHR.SelectedNode.Text
            Dim projectid As String = tlvProjectsPCHR.SelectedNode.Key
            Dim commandForCSorPS As String = ""
            Dim connstr As String = ""

            If (byCSorPS = PCHTType.CS) Then
                Dim sqlCommandAndConnection As String() = GetSQL(IOSSqlIds.PCHR_CS, Nothing, dt_IOS_SQL)
                commandForCSorPS = sqlCommandAndConnection(1)
                connstr = sqlCommandAndConnection(0)
            ElseIf (byCSorPS = PCHTType.PS) Then
                Dim sqlCommandAndConnection As String() = GetSQL(IOSSqlIds.PCHR_PS, Nothing, dt_IOS_SQL)
                commandForCSorPS = sqlCommandAndConnection(1)
                connstr = sqlCommandAndConnection(0)
            End If
            commandForCSorPS = commandForCSorPS + " " + projectid & "," & Chr(39) & txtCELL.Text.Trim & Chr(39) & "," & Chr(39) & txtIMSI.Text.Trim & Chr(39) & "," & Chr(39) & dtpPCHRFilterStartDate.EditValue.ToString & Chr(39) & "," & Chr(39) & dtpPCHRFilterEndDate.EditValue.ToString & Chr(39)
            dtRNC = DataAccessorSQL.ExecuteDataSet(connstr, commandForCSorPS, True, 200)
        End If
        Return dtRNC
    End Function

    Private Sub SetPCHRDetails(ByRef ds As DataSet, ByVal IsCSorPS As String)
        If (ds.Tables.Count >= 1) Then
            SetPCHR_OverviewDetailsData(ds.Tables(0), IsCSorPS)
        End If
    End Sub

    Private Sub OverviewCSPSGridData(ByRef ds As DataSet, ByVal IsCSorPS As PCHTType)
        If (ds.Tables.Count >= 2) Then
            SetPCHR_OverviewCSPSGridData(ds.Tables(1), IsCSorPS)
        End If
    End Sub

    Private Sub SetPCHR_OverviewDetailsData(ByRef dt As DataTable, ByVal IsCSorPS As PCHTType)
        Try
            If (IsCSorPS = PCHTType.CS) Then
                If (dt.Rows.Count > 0) Then
                    lblCSNoOfCellCS.Text = dt.Rows(0)(1).ToString()
                    lblCSNoOfFailuresCS.Text = dt.Rows(1)(1).ToString()
                    lblCSFailureRate.Text = dt.Rows(2)(1).ToString()
                    lblCSNoOfUniqIMSI.Text = dt.Rows(3)(1).ToString()
                    If dt.Rows.Count = 5 Then
                        lblCSNoOfUniqIMEI.Text = dt.Rows(4)(1).ToString()
                    End If
                    If dt.Rows.Count = 6 Then
                        lblCSNoOfCells.Text = dt.Rows(5)(1).ToString()
                    End If
                Else
                    ResetPCHR_OverviewDetailsData(IsCSorPS)
                End If
            ElseIf (IsCSorPS = PCHTType.PS) Then
                If (dt.Rows.Count > 0) Then

                    lblPSNoOfCellCS.Text = dt.Rows(0)(1).ToString()
                    lblPSNoOfFailuresCS.Text = dt.Rows(1)(1).ToString()
                    lblPSFailureRate.Text = dt.Rows(2)(1).ToString()
                    lblPSNoOfUniqIMSI.Text = dt.Rows(3)(1).ToString()
                    If dt.Rows.Count = 5 Then
                        lblPSNoOfUniqIMEI.Text = dt.Rows(4)(1).ToString()
                    End If
                    If dt.Rows.Count = 6 Then
                        lblPSNoOfCells.Text = dt.Rows(5)(1).ToString()
                    End If
                Else
                    ResetPCHR_OverviewDetailsData(IsCSorPS)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ResetPCHR_OverviewDetailsData(ByVal IsCSorPS As PCHTType)
        If (IsCSorPS = PCHTType.CS) Then
            lblCSNoOfCellCS.Text = "0"
            lblCSNoOfFailuresCS.Text = "0"
            lblCSFailureRate.Text = "0"
            lblCSNoOfUniqIMSI.Text = "0"
            lblCSNoOfUniqIMEI.Text = "0"
            lblCSNoOfCells.Text = "0"
            lblCSListOfRNC.Text = "0"
        ElseIf (IsCSorPS = PCHTType.PS) Then
            lblPSNoOfCellCS.Text = "0"
            lblPSNoOfFailuresCS.Text = "0"
            lblPSFailureRate.Text = "0"
            lblPSNoOfUniqIMSI.Text = "0"
            lblPSNoOfUniqIMEI.Text = "0"
            lblPSNoOfCells.Text = "0"
        End If
    End Sub

    Private Sub SetPCHR_OverviewCSPSGridData(ByRef dt As DataTable, ByVal IsCSorPS As String)
        dt.Columns("Imsi").SetOrdinal(6)
        dt.Columns("CallRecordNum").SetOrdinal(1)

        If (IsCSorPS.ToUpper = "CS") Then
            If (dt.Rows.Count > 0) Then
                dt.Columns("CS_RAB_Setup_Cell").SetOrdinal(11)
                dt.Columns("CS_RAB_Release_Cell").SetOrdinal(15)
                IOSDevExpressGrid.PopulateDataInGrid(dgvPCHROverviewCS, gvPCHROverviewCS, dt, "ALL")
            End If
        ElseIf (IsCSorPS.ToUpper = "PS") Then
            If (dt.Rows.Count > 0) Then
                dt.Columns("PS_RAB_Setup_Cell").SetOrdinal(11)
                dt.Columns("PS_RAB_Release_Cell").SetOrdinal(15)
                IOSDevExpressGrid.PopulateDataInGrid(dgvPCHROverviewPS, gvPCHROverviewPS, dt, "ALL")
            End If
        End If
    End Sub

    Private Sub SetPCHR_TabGridData(ByRef grid As DevExpress.XtraGrid.GridControl, ByRef gView As DevExpress.XtraGrid.Views.Grid.GridView, ByRef dt As DataTable)
        If (dt.Rows.Count > 0) Then
            IOSDevExpressGrid.PopulateDataInGrid(grid, gView, dt, "ALL")
        End If
    End Sub

    Private Sub SetPCHR_UE(ByRef dt As DataTable, ByVal IsCSorPS As String)
        If (IsCSorPS.ToUpper = "CS") Then
            If (dt.Rows.Count > 0) Then
            End If
        ElseIf (IsCSorPS.ToUpper = "PS") Then
            If (dt.Rows.Count > 0) Then

            End If
        End If
    End Sub

    Private Sub SetPCHR_MsgFlow(ByRef dt As DataTable)
        If (dt.Rows.Count > 0) Then
            IOSDevExpressGrid.PopulateDataInGrid(dgvPCHRMsgFlow, gvPCHRMsgFlow, dt, "ALL")
        End If
    End Sub

    Private Sub OverviewCharts(ByRef dt As DataTable, ByVal isSCorPS As PCHRTab.OverviewGridType)
        Dim dtTemp As DataTable = New DataTable
        Dim pchrTab As PCHRTab = New PCHRTab() ''yy
        Dim chartPCHR As ChartPCHR = New ChartPCHR()
        If (isSCorPS = PCHRTab.OverviewGridType.CS) Then
            chartPCHR.OverviewErrorChart(pchrTab.CSOverviewChartLINQ_ErrorType(dt), chartOverview1, "CS ErrorType")
            chartPCHR.OverviewErrorChart(pchrTab.CSOverviewChartLINQ_ErrorReason(dt), chartOverview2, "CS Error Reason")
            chartPCHR.OverviewErrorChart(pchrTab.CSOverviewChartLINQ_ErrorCause(dt), chartOverview3, "CS Error Cause")
            chartPCHR.OverviewErrorChart(pchrTab.CSOverviewChartLINQ_ReleaseCause(dt), chartOverview4, "CS Error IU Reason Cause")
        ElseIf (isSCorPS = PCHRTab.OverviewGridType.PS) Then
            chartPCHR.OverviewErrorChart(pchrTab.PSOverviewChartLINQ_ErrorType(dt), chartOverview1, "PS ErrorType")
            chartPCHR.OverviewErrorChart(pchrTab.PSOverviewChartLINQ_ErrorReason(dt), chartOverview2, "PS Error Reason")
            chartPCHR.OverviewErrorChart(pchrTab.PSOverviewChartLINQ_ErrorCause(dt), chartOverview3, "PS Error Cause")
            chartPCHR.OverviewErrorChart(pchrTab.PSOverviewChartLINQ_ReleaseCause(dt), chartOverview4, "PS Error IU Reason Cause")
        End If
    End Sub

    Private Sub GetDataForCS()
        ClearDataForPSandCS(OverviewGridType.CS)
        If (chkPCHRCS.Checked) Then
            dsPCHR_CS = GetCSorPS_Data(PCHTType.CS)
            If (dsPCHR_CS IsNot Nothing) Then
                SetPCHRDetails(dsPCHR_CS, PCHTType.CS)
            End If
        End If
    End Sub

    Private Sub GetDataForPS()
        ClearDataForPSandCS(OverviewGridType.PS)
        If (chkPCHRPS.Checked) Then
            dsPCHR_PS = GetCSorPS_Data(PCHTType.PS)
            If (dsPCHR_PS IsNot Nothing) Then
                SetPCHRDetails(dsPCHR_PS, PCHTType.PS)
            End If
        End If
    End Sub

    Private Sub ClearDataForPSandCS(ByVal isCSorPS As OverviewGridType)
        ChartComman.ChartDataClear(chartOverview1)
        ChartComman.ChartDataClear(chartOverview2)
        ChartComman.ChartDataClear(chartOverview3)
        ChartComman.ChartDataClear(chartOverview4)

        IOSDevExpressGrid.ClearGrid(dgvPCHRCellError)
        ChartComman.ChartDataClear(chPCHRCellErrorBar)
        ChartComman.ChartDataClear(chPCHRCellErrorPie)

        IOSDevExpressGrid.ClearGrid(dgvPCHRIMSIError)
        ChartComman.ChartDataClear(chPCHRIMSIErrorBar)
        ChartComman.ChartDataClear(chPCHRIMSIErrorPie)

        IOSDevExpressGrid.ClearGrid(dgvPCHRUEError)
        ChartComman.ChartDataClear(chPCHRUEErrorBar)
        ChartComman.ChartDataClear(chPCHRUEErrorPie)

        IOSDevExpressGrid.ClearGrid(dgvPCHRMsgFlow)

        ChartComman.ChartDataClear(chartRadio1)
        ChartComman.ChartDataClear(chartRadio2)
        ChartComman.ChartDataClear(chartRadio3)

        If (isCSorPS = OverviewGridType.CS) Then
            If (Not dsPCHR_CS Is Nothing) Then
                dsPCHR_CS.Clear()
            End If
            ResetPCHR_OverviewDetailsData(PCHTType.CS)
            IOSDevExpressGrid.ClearGrid(dgvPCHROverviewCS)
        ElseIf (isCSorPS = OverviewGridType.PS) Then
            If (Not dsPCHR_PS Is Nothing) Then
                dsPCHR_PS.Clear()
            End If
            ResetPCHR_OverviewDetailsData(PCHTType.PS)
            IOSDevExpressGrid.ClearGrid(dgvPCHROverviewPS)
        End If
    End Sub

    Private Sub SetDataRightTlv(ByVal fileId As String, ByVal callrecordnum As String)
        Dim tempSplitCont As System.Windows.Forms.SplitContainer = GetSplitControl(tlvPCHRBlockType)
        If (tempSplitCont IsNot Nothing) Then
            tlvPCHRBlockType.Nodes.Clear()
            If (tempSplitCont.Panel2Collapsed.Equals(False)) Then
                Try
                    Dim parray()() As String = {New String() {"@fileid", Chr(39) & fileId & Chr(39)},
                                                New String() {"@callrecordnum", Chr(39) & callrecordnum & Chr(39)}}

                    Dim sqlCommandAndConnection As String() = GetSQL(60010, parray, dt_IOS_SQL)
                    Dim dtMsg As DataTable = DataAccessorSQL.ExecuteDataTable(sqlCommandAndConnection(0), sqlCommandAndConnection(1))
                    If (Not dtMsg Is Nothing) Then
                        If (dtMsg.Rows.Count >= 1) Then
                            BindBlockType_Tlv(dtMsg)
                        End If
                    End If
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                End Try
            End If
        End If
    End Sub

    Private Sub BindBlockType_Tlv(ByRef dtBlockType As DataTable)
        InsertRootNodeInBlockTypeTlv()
        Try
            Dim mainNode As TreeNode = tlvPCHRBlockType.Nodes(0)
            Dim dtDisBlockType = dtBlockType.DefaultView.ToTable(True, "BlockType")
            Dim rooNodeIndex As Integer = 0
            If (dtDisBlockType IsNot Nothing) Then
                For Each drBlockType As DataRow In dtDisBlockType.Rows
                    Dim roottn As TreeNode = New TreeNode
                    roottn.Text = drBlockType("BlockType").ToString()
                    mainNode.Nodes.Add(roottn)

                    Dim parentNode As New TreeNode
                    parentNode = mainNode.Nodes(rooNodeIndex)

                    Dim dtDisParam = dtBlockType.Select("BlockType=" & Chr(39) & drBlockType("BlockType").ToString() & Chr(39)).CopyToDataTable()
                    If (dtDisParam IsNot Nothing) Then
                        FillTreeviewBlockType(parentNode, tlvPCHRBlockType, dtDisParam)
                    End If
                    rooNodeIndex = rooNodeIndex + 1
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            System.GC.Collect()
        End Try
    End Sub

    Public Sub FillTreeviewBlockType(ByVal rootNode As TreeNode, ByRef tree As TreeView, ByRef dt As DataTable)
        Try
            Dim subItemCounter As Integer = 0
            For Each parentrow In dt.Rows
                subItemCounter = 0
                If parentrow.Item(1).ToString <> "" Then
                    Dim parentnode As TreeNode = New TreeNode()
                    parentnode.Name = parentrow.Item(1).ToString.Trim
                    Dim subItems() As String = parentrow.Item(2).ToString.Split("|")
                    If (subItems.Count > 1) Then
                        parentnode.Text = parentrow.Item(1).ToString.Trim
                        For Each splitStr As String In subItems
                            Dim subnode As TreeNode = New TreeNode(parentrow.Item(1).ToString.Trim & "(" & subItemCounter & ")")
                            subnode.Name = parentrow.Item(1).ToString.Trim
                            parentnode.Nodes.Add(subnode)
                            Dim subnode2 As TreeNode = New TreeNode(splitStr)
                            subnode2.Name = splitStr
                            subnode.Nodes.Add(subnode2)
                            subItemCounter = subItemCounter + 1
                        Next
                    Else
                        parentnode.Text = parentrow.Item(1).ToString.Trim & ":" & parentrow.Item(2).ToString.Trim
                    End If
                    rootNode.Nodes.Add(parentnode)
                    parentnode = Nothing
                End If
            Next parentrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        rootNode.Collapse()
        rootNode = Nothing
        System.GC.Collect()
    End Sub

    Private Sub PopulateTreeViewSubNode(ByVal inTreeNode As TreeNode, ByVal dt As DataTable)
        Try
            Dim subItemCounter As Integer = 0
            For Each parentrow In dt.Rows
                subItemCounter = 0
                If parentrow.Item(1).ToString <> "" Then
                    Dim parentnode As TreeNode = New TreeNode()
                    parentnode.Name = parentrow.Item(1).ToString.Trim
                    Dim subItems() As String = parentrow.Item(2).ToString.Split("|")
                    If (subItems.Count > 1) Then
                        parentnode.Text = parentrow.Item(1).ToString.Trim

                        For Each splitStr As String In subItems
                            Dim subnode As TreeNode = New TreeNode(parentrow.Item(1).ToString.Trim & "(" & subItemCounter & ")")
                            subnode.Name = parentrow.Item(1).ToString.Trim
                            parentnode.Nodes.Add(subnode)
                            Dim subnode2 As TreeNode = New TreeNode(splitStr)
                            subnode2.Name = splitStr
                            subnode.Nodes.Add(subnode2)
                            subItemCounter = subItemCounter + 1
                        Next
                    Else
                        parentnode.Text = parentrow.Item(1).ToString.Trim & ":" & parentrow.Item(2).ToString.Trim
                    End If
                    inTreeNode.Nodes.Add(parentnode)
                    parentnode = Nothing
                End If
            Next parentrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function GetRadioTabData(Optional ByVal filterCell As String = "", Optional ByVal filterIMSI As String = "", Optional ByVal callRecordNum As String = "-1|-1") As DataSet
        Dim projectId As String = String.Empty
        Dim filterStartDate As String = String.Empty
        Dim filterEndDate As String = String.Empty
        Try
            If (tlvProjectsPCHR.SelectedNode IsNot Nothing) Then

                projectId = tlvProjectsPCHR.SelectedNode.Key
            Else
                Return Nothing
                Exit Function
            End If
            If (dtpPCHRFilterStartDate.EditValue IsNot Nothing) Then
                filterStartDate = dtpPCHRFilterStartDate.EditValue
            Else
                Return Nothing
                Exit Function
            End If
            If (dtpPCHRFilterEndDate.EditValue IsNot Nothing) Then
                filterEndDate = dtpPCHRFilterEndDate.EditValue
            Else
                Return Nothing
                Exit Function
            End If

            If (txtIMSI.Text <> "" And filterIMSI = "") Then
                filterIMSI = txtIMSI.Text
            End If
            If (txtCELL.Text <> "" And filterCell = "") Then
                filterCell = txtCELL.Text
            End If

            Dim parray()() As String = {New String() {"@FilterCell", Chr(39) & filterCell & Chr(39)},
                                        New String() {"@FilterIMSI", Chr(39) & filterIMSI & Chr(39)},
                                        New String() {"@FileID", Chr(39) & callRecordNum.Split("|")(0) & Chr(39)},
                                        New String() {"@CallRecordNum", Chr(39) & callRecordNum.Split("|")(1) & Chr(39)},
                                        New String() {"@ProjectID", Chr(39) & projectId & Chr(39)},
                                        New String() {"@FilterStartDate", Chr(39) & filterStartDate & Chr(39)},
                                        New String() {"@FilterEndDate", Chr(39) & filterEndDate & Chr(39)}}

            Dim sqlCommandAndConnection As String() = GetSQL(60011, parray, dt_IOS_SQL)
            Dim dsBlocktype As DataSet = DataAccessorSQL.ExecuteDataSet(sqlCommandAndConnection(0), sqlCommandAndConnection(1))
            If (Not dsBlocktype Is Nothing) Then
                Return dsBlocktype
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Private Function GetRadioAllData(Optional ByVal filterCell As String = "", Optional ByVal filterIMSI As String = "", Optional ByVal callRecordNum As String = "-1|-1") As DataSet
        Dim projectId As String = String.Empty
        Dim filterStartDate As String = String.Empty
        Dim filterEndDate As String = String.Empty
        Try
            If (tlvProjectsPCHR.SelectedNode IsNot Nothing) Then

                projectId = tlvProjectsPCHR.SelectedNode.Key
            Else
                Return Nothing
                Exit Function
            End If
            If (dtpPCHRFilterStartDate.EditValue IsNot Nothing) Then
                filterStartDate = dtpPCHRFilterStartDate.EditValue
            Else
                Return Nothing
                Exit Function
            End If
            If (dtpPCHRFilterEndDate.EditValue IsNot Nothing) Then
                filterEndDate = dtpPCHRFilterEndDate.EditValue
            Else
                Return Nothing
                Exit Function
            End If

            If (txtIMSI.Text <> "" And filterIMSI = "") Then
                filterIMSI = txtIMSI.Text
            End If
            If (txtCELL.Text <> "" And filterCell = "") Then
                filterCell = txtCELL.Text
            End If

            Dim sqlCommandAndConnection As String() = GetSQL(60012, Nothing, dt_IOS_SQL)
            Dim commandForCSorPS As String = sqlCommandAndConnection(1)
            commandForCSorPS = commandForCSorPS + " " + projectId & "," & Chr(39) & filterCell & Chr(39) & "," & Chr(39) & filterIMSI & Chr(39) & "," & Chr(39) & callRecordNum.Split("|")(0) & Chr(39) & "," & Chr(39) & callRecordNum.Split("|")(1) & Chr(39) & "," & Chr(39) & dtpPCHRFilterStartDate.EditValue.ToString & Chr(39) & "," & Chr(39) & dtpPCHRFilterEndDate.EditValue.ToString & Chr(39)
            Dim dsBlocktype As DataSet = DataAccessorSQL.ExecuteDataSet(sqlCommandAndConnection(0), commandForCSorPS)

            If (Not dsBlocktype Is Nothing) Then
                Return dsBlocktype
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Private Function GetSplitControl(ByRef tempControl As Control) As System.Windows.Forms.SplitContainer
        If (tempControl.Parent IsNot Nothing) Then
            If tempControl.Parent.GetType() Is GetType(SplitContainer) Then
                Return tempControl.Parent
            Else
                Return GetSplitControl(tempControl.Parent)
            End If
        Else
            Return Nothing
        End If
    End Function

    Private Sub TreeView_SearchWildCard(ByVal nd As TreeNode, ByVal str As String, ByVal startindex As Integer, Optional ByVal ExactMatch As Boolean = False)
        nd.TreeView.SuspendLayout()
        If str.Length < 3 Then
            For Each nd In nd.Nodes
                If Treeview_NodeFound = True Then
                    nd.TreeView.ResumeLayout(True)
                    Exit Sub
                End If
                If nd.Text.ToUpper = str.ToUpper Then
                    If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                        nd.EnsureVisible()
                        nd.TreeView.SelectedNode = nd

                        nd.BackColor = Color.Coral
                        Treeview_NodeFound = True
                        nd.TreeView.ResumeLayout(True)
                        Exit Sub
                    End If

                Else
                    nd.BackColor = Color.White
                End If
                TreeView_SearchWildCard(nd, str, startindex)
            Next
        Else
            For Each nd In nd.Nodes
                If Treeview_NodeFound = True Then
                    nd.TreeView.ResumeLayout(True)
                    Exit Sub
                End If
                If ExactMatch = False Then
                    If nd.Text.ToUpper.StartsWith(str.ToUpper) Then
                        If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                            nd.EnsureVisible()
                            nd.TreeView.SelectedNode = nd
                            nd.BackColor = Color.Coral
                            Treeview_NodeFound = True
                            nd.TreeView.ResumeLayout(True)
                            Exit Sub
                        End If
                    Else
                        nd.BackColor = Color.White
                    End If
                    TreeView_SearchWildCard(nd, str, startindex)
                Else
                    If nd.Text.ToUpper = str.ToUpper Then
                        If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                            nd.EnsureVisible()
                            nd.TreeView.SelectedNode = nd
                            nd.BackColor = Color.Coral
                            Treeview_NodeFound = True
                            nd.TreeView.ResumeLayout(True)
                            Exit Sub
                        End If
                    Else
                        nd.BackColor = Color.White
                    End If
                    TreeView_SearchWildCard(nd, str, startindex, True)
                End If
            Next
        End If
        nd.TreeView.ResumeLayout(True)
    End Sub

    Private Function Treeview_NodePosition(ByVal oTreeView As TreeView, ByVal oNode As TreeNode)
        Dim iPosInTree As Integer = 0
        Do
            Dim iNodeIndex As Integer = oNode.Index
            iPosInTree = iPosInTree + iNodeIndex + 1

            'Get the Parent Node or the TreeView if at the top.
            Dim oParentNode As Object = oNode.Parent
            If oParentNode Is Nothing Then
                oParentNode = oTreeView
            End If

            'Count the Nodes precding this one on the current level.
            Dim I As Integer
            For I = 0 To iNodeIndex - 1
                iPosInTree = iPosInTree + Treeview_NumberOfChildren(oParentNode.Nodes(I))
            Next

            'Go up to the next level.
            oNode = oNode.Parent
        Loop Until oNode Is Nothing
        Return iPosInTree
    End Function

    Function Treeview_NumberOfChildren(ByVal oNode As TreeNode)
        If oNode.LastNode Is Nothing Then
            Return 0 'No children
        End If
        Dim iNumChildren = oNode.LastNode.Index + 1
        Dim oSubNode As TreeNode
        For Each oSubNode In oNode.Nodes
            iNumChildren = iNumChildren + Treeview_NumberOfChildren(oSubNode)
        Next
        Return iNumChildren
    End Function

#End Region

#Region "Context Menu"

    Private Sub tsmiPCHR_ExportExcelAll_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_ExportExcelAll.Click
        Try
            WaitScreen.ShowWaitScreen("Exporting...")
            ExportAllDataGridToExcel(xtcPCHRGridCSPS)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            WaitScreen.CloseWaitScreen()
        End Try
    End Sub

    Private Sub tsmi_RightSideTreeView_Click(sender As Object, e As EventArgs) Handles tsmi_RightSideTreeView.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tempSplitCont As System.Windows.Forms.SplitContainer = GetSplitControl(tlvPCHRBlockType)
            If (tempSplitCont IsNot Nothing) Then
                If (tempSplitCont.Panel2Collapsed.Equals(True)) Then
                    tempSplitCont.Panel2Collapsed = False
                    If (fileID IsNot Nothing AndAlso callrecNUM IsNot Nothing) Then
                        SetDataRightTlv(fileID, callrecNUM)
                    End If
                Else
                    tempSplitCont.Panel2Collapsed = True
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiDeleteProjectPCHR_Click(sender As Object, e As EventArgs) Handles tsmiDeleteProjectPCHR.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tlv As TreeListView = CType(cmsProjectsPCHR.SourceControl, TreeListView)
            Dim nd As TreeListViewNode = tlv.SelectedNode
            If Not nd Is Nothing Then
                clsSQLCommands.DeleteProjectPCHR(connStrIOSServer, nd.Key)
                btnRefreshPCHR_Click(Nothing, Nothing)

                Dim parray()() As String = {New String() {"@projectid", Chr(39) & nd.Key & Chr(39)}}
                Dim sqlCommandAndConnection() As String = GetSQL(60003, parray, dt_IOS_SQL)

                DataAccessorSQL.Async_ConnectionString = sqlCommandAndConnection(0)
                DataAccessorSQL.Async_SQL = sqlCommandAndConnection(1)
                DataAccessorSQL.Async_TimeOut = 0

                System.Threading.ThreadPool.QueueUserWorkItem(AddressOf DataAccessorSQL.ExecuteNonQuery_Async)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiRenameProjectPCHR_Click(sender As Object, e As EventArgs) Handles tsmiRenameProjectPCHR.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tlv As TreeListView = CType(cmsProjectsPCHR.SourceControl, TreeListView)
            Dim nd As TreeListViewNode = tlv.SelectedNode
            Dim NewName As String = InputBox("Enter Project Name: ", "", nd.Text)
            If Not nd Is Nothing And NewName <> "" Then
                Dim parray()() As String = {New String() {"@projectid", Chr(39) & nd.Key & Chr(39)},
                                           New String() {"@newprojectname", Chr(39) & NewName & Chr(39)}}

                Dim sqlCommandAndConnection As String() = GetSQL(60004, parray)
                DataAccessorSQL.ExecuteNonQuery(sqlCommandAndConnection(0), sqlCommandAndConnection(1))

                btnRefreshPCHR_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiPropertyProjectPCHR_Click(sender As Object, e As EventArgs) Handles tsmiPropertyProjectPCHR.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tlv As TreeListView = CType(cmsProjectsPCHR.SourceControl, TreeListView)
            Dim nd As TreeListViewNode = tlv.SelectedNode
            If Not nd Is Nothing Then
                Dim dialogProjectInfoPCHR As New dlgProjectInfoPCHR()
                'dialogProjectInfoPCHR.ConnString = connStrIOSServer
                dialogProjectInfoPCHR.ProjectId = nd.Key
                dialogProjectInfoPCHR.ShowDialog()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cms_ProjectsPCHR_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsProjectsPCHR.Opening
        Dim tlv As TreeListView = CType(cmsProjectsPCHR.SourceControl, TreeListView)
        Dim nd As TreeListViewNode = tlv.SelectedNode
        If Not nd Is Nothing Then
            If nd.Level = 0 Then
                If nd.Tag.ToString.ToUpper = System.Environment.UserName.ToString.ToUpper Then
                    tsmiDeleteProjectPCHR.Enabled = True
                    tsmiRenameProjectPCHR.Enabled = True
                Else
                    '  tsmiDeleteProjectPCHR.Enabled = False
                    '  tsmiRenameProjectPCHR.Enabled = False
                End If
            End If
            tsmiDeleteProjectPCHR.Enabled = True
            tsmiRenameProjectPCHR.Enabled = True
        End If
    End Sub

    Private Sub vtxtSearch_PCHR_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPCHR.TextChanged
        Dim selectStatement As String = String.Empty
        If (txtSearchPCHR.Text.Trim.Length >= 3) Then
            selectStatement += "ProjectName LIKE '%" & txtSearchPCHR.Text.Trim & "%' "
        End If

        If (dtPCHRProjects Is Nothing) Then
            GetPCHRProjects()
        End If

        Dim dtReport As DataTable = Nothing
        If (String.IsNullOrEmpty(selectStatement)) Then
            dtReport = dtPCHRProjects
        Else
            Dim dv As DataView = New DataView(dtPCHRProjects, selectStatement, "", DataViewRowState.CurrentRows)
            dtReport = dv.ToTable()
        End If
        tlvProjectsPCHR.Nodes.Clear()
        tlvProjectsPCHR.SuspendLayout()
        BindProjectTreeListView(tlvProjectsPCHR, dtReport)
    End Sub

    Private Sub tsmiPCHR_RadioIMSI_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_RadioIMSI.Click
        SetRadioChart(tsmiPCHR_RadioIMSI.Tag, RadioValue.IMSI)
    End Sub

    Private Sub tsmiPCHR_RadioCallSetup_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_RadioCellSetup.Click
        SetRadioChart(tsmiPCHR_RadioCellSetup.Tag, RadioValue.CellSetup)
    End Sub

    Private Sub tsmiPCHR_RadioCallRelease_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_RadioCellRelease.Click
        SetRadioChart(tsmiPCHR_RadioCellRelease.Tag, RadioValue.CellRelease)
    End Sub

    Private Sub tsmiPCHR_RadioCallRecord_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_RadioCallRecordAll.DoubleClick, tsmiPCHR_RadioCallRecord.Click
        SetRadioChart(tsmiPCHR_RadioCallRecord.Tag, RadioValue.CallRecordNum)
    End Sub

    Private Sub SetRadioChart(ByVal filterValue As String, ByVal radioValue As RadioValue)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Application.UseWaitCursor = True
            Application.DoEvents()
            Dim chartTitle As String = ""
            If (tlvProjectsPCHR.SelectedNode IsNot Nothing) Then
                chartTitle = tlvProjectsPCHR.SelectedNode.Text
            End If

            Dim ds As DataSet = Nothing
            If (radioValue = RadioValue.IMSI) Then
                ds = GetRadioTabData("", filterValue, "-1|-1")
                chartTitle = chartTitle & " Filter: IMSI=" & filterValue
            ElseIf (radioValue = RadioValue.CellSetup) Then
                ds = GetRadioTabData(filterValue, "", "-1|-1")
                chartTitle = chartTitle & " Filter: CellSetup=" & filterValue
            ElseIf (radioValue = RadioValue.CellRelease) Then
                ds = GetRadioTabData(filterValue, "", "-1|-1")
                chartTitle = chartTitle & " Filter: CellRelease=" & filterValue
            ElseIf (radioValue = RadioValue.CallRecordNum) Then
                ds = GetRadioTabData("", "", filterValue)
                chartTitle = chartTitle & " Filter: FileID=" & Split(filterValue, "|")(0) & " CallRecordNum=" & Split(filterValue, "|")(1)
            ElseIf (radioValue = RadioValue.CellChart) Then
                ds = GetRadioTabData()
            End If

            If (txtIMSI.Text <> "") Then
                chartTitle = chartTitle + " - IMSI=" + txtIMSI.Text
            End If
            If (txtCELL.Text <> "") Then
                chartTitle = chartTitle + " - CELL=" + txtCELL.Text
            End If

            If (ds IsNot Nothing) Then
                Dim chartPCHR As ChartPCHR = New ChartPCHR()
                chartPCHR.RadioBarChart(chartRadio1, ds.Tables(1), chartTitle)
                chartPCHR.RadioBarChart(chartRadio2, ds.Tables(2), chartTitle)
                chartPCHR.RadioBubbleChart(chartRadio3, ds.Tables(0), chartTitle)
                chartRadio3.LegendBox.Visible = False
                xtcPCHRMsgFlowRadio.SelectedTabPageIndex = 1
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub tsmiPCHR_RadioIMSIAll_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_RadioIMSIAll.Click
        isRequestByContextMenu = True
        CreateOverviewRadioTab(tsmiPCHR_RadioIMSIAll.Tag, RadioValue.IMSI)
    End Sub

    Private Sub tsmiPCHR_RadioCallSetupAll_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_RadioCellSetupAll.Click
        CreateOverviewRadioTab(tsmiPCHR_RadioCellSetupAll.Tag, RadioValue.CellSetup)
    End Sub

    Private Sub tsmiPCHR_RadioCallReleaseAll_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_RadioCellReleaseAll.DoubleClick
        CreateOverviewRadioTab(tsmiPCHR_RadioCellReleaseAll.Tag, RadioValue.CellRelease)
    End Sub

    Private Sub tsmiPCHR_RadioCallRecordAll_Click(sender As Object, e As EventArgs)
        CreateOverviewRadioTab(tsmiPCHR_RadioCallRecordAll.Tag, RadioValue.CallRecordNum)
    End Sub

    Private Sub CreateOverviewRadioTab(ByVal filterValue As String, ByVal radioValue As RadioValue)
        Application.UseWaitCursor = True
        Application.DoEvents()
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim chartTitle As String = ""
            If (tlvProjectsPCHR.SelectedNode IsNot Nothing) Then
                chartTitle = tlvProjectsPCHR.SelectedNode.Text
            End If
            isRequestByContextMenu = True
            Dim isSelectedGridTabCSorPS As New OverviewGridType

            If (xtcPCHRGridCSPS.SelectedTabPage.Tag = "PS") Then
                isSelectedGridTabCSorPS = OverviewGridType.PS
            Else
                isSelectedGridTabCSorPS = OverviewGridType.CS
            End If

            Dim ds As DataSet = Nothing
            If (radioValue = RadioValue.IMSI) Then
                ds = GetRadioAllData("", filterValue, "-1|-1")
            ElseIf (radioValue = RadioValue.CellSetup) Then
                ds = GetRadioAllData(filterValue, "", "-1|-1")
            ElseIf (radioValue = RadioValue.CellRelease) Then
                ds = GetRadioAllData(filterValue, "", "-1|-1")
            ElseIf (radioValue = RadioValue.CallRecordNum) Then
                ds = GetRadioAllData("", "", filterValue)
            ElseIf (radioValue = RadioValue.CellChart) Then
                ds = GetRadioAllData()
            End If

            If (ds IsNot Nothing) Then
                xtcPCHRGridCSPS.SuspendLayout()
                For Each dt As DataTable In ds.Tables
                    If (dt.Rows.Count >= 1) Then
                        Dim tableName As String = dt.Rows(0)("TableName").ToString

                        Dim xtp As DevExpress.XtraTab.XtraTabPage = Nothing
                        Dim gc As DevExpress.XtraGrid.GridControl = Nothing

                        xtp = xtcPCHRGridCSPS.TabPages.FirstOrDefault(Function(x) x.Name = "xtp" + tableName)
                        If xtp Is Nothing Then
                            xtp = IOS.Library.IOSDevExTab.CreateTab("xtp" + tableName, tableName, "RadioAll")
                            gc = IOSDevExpressGrid.CreateGrid("gc" + tableName)
                            xtp.Controls.Add(gc)
                            xtcPCHRGridCSPS.TabPages.Add(xtp)
                        Else
                            gc = CType(xtp.Controls(0), DevExpress.XtraGrid.GridControl)
                        End If
                        isRequestByContextMenu = True

                        gc.ContextMenuStrip = cmsPCHR_GridViewOverviewCSPS
                        IOSDevExpressGrid.PopulateDataInGrid(gc, gc.MainView, dt, "All")
                    End If
                Next
                xtcPCHRGridCSPS.ResumeLayout()
                xtcPCHRGridCSPS.Refresh()
            End If
            If (isSelectedGridTabCSorPS = OverviewGridType.PS) Then
                isRequestByContextMenu = True
                xtcPCHRGridCSPS.SelectedTabPage = xtcPCHRGridCSPS.TabPages(1)
            Else
                isRequestByContextMenu = True
                xtcPCHRGridCSPS.SelectedTabPage = xtcPCHRGridCSPS.TabPages(0)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub tsmiPCHR_CopyAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiPCHR_CopyAll.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim grvTemp As DevExpress.XtraGrid.GridControl = GetVDataGridViewByToolStripMenuItem(sender)
            IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, grvTemp.DefaultView)
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Public Function GetVDataGridViewByToolStripMenuItem(ByRef stripMenuItem As Object) As DevExpress.XtraGrid.GridControl
        Dim requestMenu As ToolStripMenuItem = TryCast(stripMenuItem, ToolStripMenuItem)
        Dim conTemp As ContextMenuStrip = TryCast(requestMenu.GetCurrentParent(), ContextMenuStrip)
        Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
        If (conTemp IsNot Nothing) Then
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            If (grvTemp IsNot Nothing) Then
                Return grvTemp
            End If
        End If
        Return Nothing
    End Function

    Private Sub tsmiPCHR_CopySelectionWithHeader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiPCHR_CopySelectionWithHeader.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = GetVDataGridViewByToolStripMenuItem(sender)
            IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, tempGrid.DefaultView, False, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    'Private Sub tsmiPCHR_CopySelectionWOHeader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiPCHR_CopySelectionWOHeader.Click
    '    Try
    '        Dim tempGrid As DevExpress.XtraGrid.GridControl = GetVDataGridViewByToolStripMenuItem(sender)
    '        IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, tempGrid.DefaultView, False, False)
    '    Catch ex As Exception
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
    '    End Try
    'End Sub

    Private Sub tsmiPCHR_ExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiPCHR_ExportExcel.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = GetVDataGridViewByToolStripMenuItem(sender)
            IOSDevExpressGrid.ExportDataGridToExcel(tempGrid)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmiPCHR_CloseRadioTab_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CloseRadioTab.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim isSelectedGridTabCSorPS As New OverviewGridType

            If (xtcPCHRGridCSPS.SelectedTabPage.Tag = "PS") Then
                isSelectedGridTabCSorPS = OverviewGridType.PS
            Else
                isSelectedGridTabCSorPS = OverviewGridType.CS
            End If
            isRequestByContextMenu = True
            RemoveTab("RadioAll", xtcPCHRGridCSPS)
            If (isSelectedGridTabCSorPS = OverviewGridType.PS) Then
                isRequestByContextMenu = True
                xtcPCHRGridCSPS.SelectedTabPageIndex = 1
            Else
                isRequestByContextMenu = True
                xtcPCHRGridCSPS.SelectedTabPageIndex = 0
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub RemoveTab(ByVal tabTag As String, ByRef vtabControl As DevExpress.XtraTab.XtraTabControl)
        Try
            Dim deletableTabPage As List(Of DevExpress.XtraTab.XtraTabPage) = New List(Of DevExpress.XtraTab.XtraTabPage)
            If (vtabControl.TabPages.Count > 2) Then
                For Each pageTab As DevExpress.XtraTab.XtraTabPage In vtabControl.TabPages
                    If (pageTab.Tag.ToString = tabTag) Then
                        deletableTabPage.Add(pageTab)
                    End If
                Next
            End If
            If (deletableTabPage.Count > 0) Then
                For Each pageTab As DevExpress.XtraTab.XtraTabPage In deletableTabPage
                    isRequestByContextMenu = True
                    vtabControl.TabPages.Remove(pageTab)
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_HidemsgFlowRadioPanel_Click(sender As Object, e As EventArgs) Handles tsmi_HidemsgFlowRadioPanel.Click
        Dim tempSplitCont As System.Windows.Forms.SplitContainer = SplitContPCHRTop
        If (tempSplitCont IsNot Nothing) Then
            If (tempSplitCont.Panel2Collapsed.Equals(True)) Then
                tempSplitCont.Panel2Collapsed = False
            Else
                tempSplitCont.Panel2Collapsed = True
                If (fileID IsNot Nothing AndAlso callrecNUM IsNot Nothing) Then
                    SetMsgFlowData(fileID, callrecNUM)
                End If
            End If
        End If
    End Sub

    Private Sub tsmiPCHR_GetTreeDataForCallRecord_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_GetTreeDataForCallRecord.Click
        If (fileID IsNot Nothing AndAlso callrecNUM IsNot Nothing) Then
            SetDataRightTlv(fileID, callrecNUM)
        End If
    End Sub

    Private Sub SetMsgFlowData(ByVal fileId As String, ByVal callRecNum As String)
        Try
            Dim parray()() As String = {New String() {"@fileid", Chr(39) & fileId & Chr(39)},
                                        New String() {"@callrecnum", Chr(39) & callRecNum & Chr(39)}}

            Dim sqlCommandAndConnection As String() = GetSQL(60007, parray)
            Dim dtMsg As DataTable = DataAccessorSQL.ExecuteDataTable(sqlCommandAndConnection(0), sqlCommandAndConnection(1))
            If (Not dtMsg Is Nothing) Then
                If (dtMsg.Rows.Count >= 1) Then
                    SetPCHR_MsgFlow(dtMsg)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub GridViewOverviewCSPS_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvPCHROverviewCS.MouseUp, dgvPCHROverviewPS.MouseUp
        If (e.Button = MouseButtons.Right) Then
            Dim gvtemp As DevExpress.XtraGrid.GridControl = TryCast(sender, DevExpress.XtraGrid.GridControl)
            If (gvtemp IsNot Nothing) Then
                'Dim cell As GridCell = gvtemp.CellsArea.HitTest(New System.Drawing.Point(e.X, e.Y))
                'Dim rowsss As HierarchyItem = gvtemp.RowsHierarchy.HitTest(New System.Drawing.Point(e.X, e.Y))
                Dim gv As GridView = gvtemp.MainView
                Dim rowItem As DataRowView = gv.GetFocusedRow()
                If (rowItem IsNot Nothing) Then
                    'Dim rowItem As HierarchyItem = cell.RowItem
                    'If (rowItem.ItemIndex >= 0) Then
                    'rowItem.Selected = True
                    tsmiPCHR_RadioIMSI.Text = "IMSI : " & rowItem.Item(6).ToString
                    tsmiPCHR_RadioIMSI.Tag = rowItem.Item(6).ToString
                    tsmiPCHR_RadioCellSetup.Text = "Cell Setup : " & rowItem.Item(11).ToString
                    tsmiPCHR_RadioCellSetup.Tag = rowItem.Item(11).ToString
                    tsmiPCHR_RadioCellRelease.Text = "Cell Release : " & rowItem.Item(15).ToString
                    tsmiPCHR_RadioCellRelease.Tag = rowItem.Item(15).ToString
                    tsmiPCHR_RadioCallRecord.Text = "CallRecord : " & rowItem.Item(1).ToString
                    tsmiPCHR_RadioCallRecord.Tag = rowItem.Item(0).ToString & "|" & rowItem.Item(1).ToString

                    tsmiPCHR_RadioIMSIAll.Text = "IMSI : " & rowItem.Item(6).ToString
                    tsmiPCHR_RadioIMSIAll.Tag = rowItem.Item(6).ToString
                    tsmiPCHR_RadioCellSetupAll.Text = "Cell Setup : " & rowItem.Item(11).ToString
                    tsmiPCHR_RadioCellSetupAll.Tag = rowItem.Item(11).ToString
                    tsmiPCHR_RadioCellReleaseAll.Text = "Cell Release : " & rowItem.Item(15).ToString
                    tsmiPCHR_RadioCellReleaseAll.Tag = rowItem.Item(15).ToString
                    tsmiPCHR_RadioCallRecordAll.Text = "CallRecord : " & rowItem.Item(1).ToString
                    tsmiPCHR_RadioCallRecordAll.Tag = rowItem.Item(0).ToString & "|" & rowItem.Item(1).ToString

                    fileID = rowItem.Item(0).ToString
                    callrecNUM = rowItem.Item(1).ToString
                    'End If
                Else
                    gvtemp.ContextMenuStrip.Hide()
                End If
            End If
        End If

    End Sub

    Private Sub tsmiPCHR_GetMsgFlowDataForCallRecord_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_GetMsgFlowDataForCallRecord.Click
        If (fileID IsNot Nothing AndAlso callrecNUM IsNot Nothing) Then
            SetMsgFlowData(fileID, callrecNUM)
        End If
    End Sub

    Private Sub tstxtPCHR_CellAndIMSIBarChartShowTopX_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tstxtPCHR_CellAndIMSIBarChartShowTopX.KeyUp
        If Not (tstxtPCHR_CellAndIMSIBarChartShowTopX.Text.Trim() = "") Then
            If (e.KeyCode = Keys.Enter) Then
                Dim noOfRecords As String = tstxtPCHR_CellAndIMSIBarChartShowTopX.Text.Trim
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                Try
                    Dim chartPCHR As ChartPCHR = New ChartPCHR()
                    If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag IsNot Nothing) Then
                        If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
                            ChartComman.ChartDataClear(chPCHRCellErrorBar)
                            If (dtOverviewCellBarChart IsNot Nothing AndAlso dtOverviewCellBarChart.Rows.Count > 0) Then
                                Dim seriesCounter As Integer = tstxtPCHR_CellAndIMSIBarChartShowTopX.Text.Trim
                                chartPCHR.OverviewCellBarChart(chPCHRCellErrorBar, dtOverviewCellBarChart.Rows.Cast(Of DataRow)().Take(seriesCounter).CopyToDataTable(), "Error Count per Cell", "Cell - Error Count") ''OverviewCellBarChart
                            End If
                        ElseIf (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                            ChartComman.ChartDataClear(chPCHRIMSIErrorBar)
                            If (dtOverviewIMSIBarChart IsNot Nothing AndAlso dtOverviewIMSIBarChart.Rows.Count > 0) Then
                                Dim seriesCounter As Integer = tstxtPCHR_CellAndIMSIBarChartShowTopX.Text.Trim
                                chartPCHR.OverviewCellBarChart(chPCHRIMSIErrorBar, dtOverviewIMSIBarChart.Rows.Cast(Of DataRow)().Take(seriesCounter).CopyToDataTable(), "Error Count per IMSI", "IMSI - Error Count") ''OverviewCellBarChart
                            End If
                        End If
                    End If
                Catch ex As Exception
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                End Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End If
        End If
    End Sub

    Private Sub tsmiPCHR_CellChart_Copy_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CellChart_Copy.Click
        Try
            If Not cmsPCHRCellChartGridHideShow.Tag Is Nothing Then
                Dim item As String = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                Clipboard.SetText(item)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmiPCHR_CellChart_GetAll_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CellChart_GetAll.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If Not cmsPCHRCellChartGridHideShow.Tag Is Nothing Then
                If cmsPCHRCellChartGridHideShow.Tag.ToString.StartsWith("CELL") Then
                    Dim item As String = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    If Not IsNumeric(item) Then
                        If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                            Dim dr() As DataRow = CType(dgvPCHROverviewCS.DataSource, DataTable).Select("CellName_Setup = '" & item & "' OR CellName_Release='" & item & "'")
                            If dr.Count > 0 Then
                                Dim setupcell As String = dr(0)("CS_RAB_Setup_Cell")
                                Dim releasecell As String = dr(0)("CS_RAB_Release_Cell")
                                If setupcell <> "-1" Then item = setupcell Else item = releasecell
                            End If
                        ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                            Dim dr() As DataRow = CType(dgvPCHROverviewPS.DataSource, DataTable).Select("CellName_Setup = '" & item & "' OR CellName_Release='" & item & "'")
                            If dr.Count > 0 Then
                                Dim setupcell As String = dr(0)("PS_RAB_Setup_Cell")
                                Dim releasecell As String = dr(0)("PS_RAB_Release_Cell")
                                If setupcell <> "-1" Then item = setupcell Else item = releasecell
                            End If
                        End If
                    End If
                    CreateOverviewRadioTab(item, RadioValue.CellSetup)
                ElseIf cmsPCHRCellChartGridHideShow.Tag.ToString.StartsWith("IMSI") Then
                    Dim item As String = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    CreateOverviewRadioTab(item, RadioValue.IMSI)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiPCHR_CellChart_MapAll_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CellChart_MapAll.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim dt As DataTable = dgvPCHRCellError.DataSource
            dt.Columns(0).ColumnName = "UCELL"
            SendToMap("HUAWEI 3G", "Count", dt, 1, EnumSendToMap.FromPCHR, , , , , , , "HUAWEI 3G")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiPCHR_CellChart_MapItem_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CellChart_MapItem.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If Not cmsPCHRCellChartGridHideShow.Tag Is Nothing Then
                If cmsPCHRCellChartGridHideShow.Tag.ToString.StartsWith("CELL") Then
                    Dim item As String = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    Dim dt As DataTable = CType(dgvPCHRCellError.DataSource, DataTable)
                    dt.Columns(0).ColumnName = "UCELL"
                    Dim dr() As DataRow = dt.Select("UCELL='" & item & "'")
                    Dim dt2 As DataTable = dr.CopyToDataTable
                    SendToMap("HUAWEI 3G", "Count", dt2, 1, EnumSendToMap.FromPCHR, , , , , , , "HUAWEI 3G")
                ElseIf cmsPCHRCellChartGridHideShow.Tag.ToString.StartsWith("IMSI") Then
                    Dim item As String = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    Dim dgv As DevExpress.XtraGrid.GridControl = Nothing
                    If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                        dgv = dgvPCHROverviewCS
                    ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                        dgv = dgvPCHROverviewPS
                    End If

                    Dim dt0 As DataTable = CType(dgv.DataSource, DataTable).Select("Imsi='" & item & "'").CopyToDataTable
                    Dim dt1 As DataTable = New DataTable
                    Dim queryLINQ = From detalle In dt0.AsEnumerable()
                                    Group detalle By grupoClave = New With
                                                           {
                                                               Key .CellName_Setup = detalle("CellName_Setup")
                                                               } Into g = Group
                                    Select New With
                                    {
                                     .PS_RAB_CellName_SetupSetup_Cell = IIf(g(0).Field(Of String)("CellName_Setup") <> "",
                                                                      g(0).Field(Of String)(DataFieldEntityPS.CellName_Setup),
                                                                      g(0).Field(Of String)(DataFieldEntityPS.CellName_Release)),
                                     .Count = g.Count()
                                 } Order By ("Count") Descending
                    Try
                        dt1 = CellTab.ConvertCellLINQueryToDataTable(queryLINQ)
                        dt1.Columns(0).ColumnName = "UCELL"
                        SendToMap("HUAWEI 3G", "Count", dt1, 1, EnumSendToMap.FromPCHR, , , , , , , "HUAWEI 3G")
                    Catch ex As Exception
                    End Try
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiPCHR_CellChart_FilterGrid_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CellChart_FilterGrid.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If Not cmsPCHRCellChartGridHideShow.Tag Is Nothing Then
                Dim colindex As Integer = 0
                Dim dgv As DevExpress.XtraGrid.GridControl = Nothing
                If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                    dgv = dgvPCHROverviewCS
                ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                    dgv = dgvPCHROverviewPS
                End If
                Dim gView As DevExpress.XtraGrid.Views.Grid.GridView = Nothing
                gView = TryCast(dgv.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim item As String = ""
                If cmsPCHRCellChartGridHideShow.Tag.ToString.StartsWith("CELL") Then
                    item = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    colindex = frmMapWindow.GridView_Map_Col2Index(dgv.DefaultView, "CellName_Setup")
                Else
                    item = cmsPCHRCellChartGridHideShow.Tag.ToString.Split("|")(1)
                    colindex = frmMapWindow.GridView_Map_Col2Index(dgv.DefaultView, "Imsi")
                End If
                gView.Columns(colindex).OptionsFilter.AllowFilter = True
                gView.Columns(colindex).FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
                gView.ActiveFilterString = "[" & gView.Columns(colindex).FieldName & "] = '" & item & "'"

                dgv.Refresh()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Cell Tab"

    Private Sub cmbPCHRCellFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPCHRCellFilter.SelectedIndexChanged
        Try
            Application.UseWaitCursor = True
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            dtOverviewCellBarChart = New DataTable()
            If (Not String.IsNullOrEmpty(cmbPCHRCellFilter.SelectedItem.ToString)) Then
                Dim chartPCHR As ChartPCHR = New ChartPCHR()
                Dim cellTab As CellTab = New CellTab() ''yy
                Dim isCSorPS As PCHRTab.OverviewGridType

                If (xtcPCHRGridCSPS.SelectedTabPage.Tag = "CS") Then
                    dtOverviewCellBarChart = cellTab.GetCSCellBarChartData(dsPCHR_CS.Tables(1), cmbPCHRCellFilter.SelectedItem.ToString.Split("(")(0), cmbPCHRCellFilter.SelectedItem.Tag)
                    isCSorPS = Library.PCHRTab.OverviewGridType.CS
                ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag = "PS") Then
                    dtOverviewCellBarChart = cellTab.GetPSCellBarChartData(dsPCHR_PS.Tables(1), cmbPCHRCellFilter.SelectedItem.ToString.Split("(")(0), cmbPCHRCellFilter.SelectedItem.Tag)
                    isCSorPS = Library.PCHRTab.OverviewGridType.PS
                End If

                ChartComman.ChartDataClear(chPCHRCellErrorBar)
                If (dtOverviewCellBarChart IsNot Nothing AndAlso dtOverviewCellBarChart.Rows.Count > 0) Then
                    Dim seriesCounter As Integer = tstxtPCHR_CellAndIMSIBarChartShowTopX.Text.Trim
                    chartPCHR.OverviewCellBarChart(chPCHRCellErrorBar, dtOverviewCellBarChart.Rows.Cast(Of DataRow)().Take(seriesCounter).CopyToDataTable(), "Error Count per Cell", "Cell - Error Count") ''OverviewCellBarChart
                    SetPCHR_TabGridData(dgvPCHRCellError, gvPCHRCellError, dtOverviewCellBarChart)
                End If
            End If
            If (cmbPCHRCellShow.SelectedIndex >= 0) Then
                cmbPCHRCellShow_SelectedIndexChanged(Nothing, Nothing)
            Else
                cmbPCHRCellShow.SelectedIndex = 2
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub cmbPCHRCellShow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPCHRCellShow.SelectedIndexChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Application.UseWaitCursor = True
            Application.DoEvents()
            If (cmbPCHRCellShow.SelectedIndex >= 0) Then
                Dim dtCauseErrorCounter As DataTable = GetCellPieChartData()
                If (dtCauseErrorCounter IsNot Nothing) Then
                    Dim chartPCHR As ChartPCHR = New ChartPCHR() ''yy
                    chartPCHR.OverviewPieChart(chPCHRCellErrorPie, dtCauseErrorCounter, "Error Causes - Cell :" & cmbPCHRCellShow.SelectedItem.ToString)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub cmsPCHRCellChartGridHideShow_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsPCHRCellChartGridHideShow.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            cmPCHR_SourceControlCellGridHidShow = cmsTemp.SourceControl
            Dim myChart As Chart = cmPCHR_SourceControlCellGridHidShow
            Dim hitchart As HitTestInfo = myChart.HitTest

            tsmiPCHR_CellChart_MapAll.Enabled = True
            If TypeOf hitchart.Object Is Element Then
                Dim el As Element = CType(hitchart.Object, Element)
                If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "CELL") Then
                    cmsPCHRCellChartGridHideShow.Tag = "CELL|" & el.Name
                ElseIf (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                    cmsPCHRCellChartGridHideShow.Tag = "IMSI|" & el.Name
                End If

                tsmiPCHR_CellChart_SelItem.Text = "Selected Item: " & el.Name
                tsmiPCHR_CellChart_FilterGrid.Enabled = True
                tsmiPCHR_CellChart_MapItem.Enabled = True
                tsmiPCHR_CellChartShowRadioData.Enabled = True
                tsmiPCHR_CellChart_GetAll.Enabled = True
            Else
                tsmiPCHR_CellChart_SelItem.Text = "Selected Item: None"
                tsmiPCHR_CellChart_GetAll.Enabled = False
                tsmiPCHR_CellChart_FilterGrid.Enabled = False
                tsmiPCHR_CellChart_MapItem.Enabled = False
                tsmiPCHR_CellChartShowRadioData.Enabled = False
            End If

            If (xtcPCHROverCellIMSIUE.SelectedTabPage.Tag.ToString.ToUpper = "IMSI") Then
                tsmiPCHR_CellChart_MapAll.Enabled = False
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub chPCHRCellErrorBar_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles chPCHRCellErrorBar.MouseDown
        Try
            Application.UseWaitCursor = True
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If e.Button = MouseButtons.Left Then
                Dim hit As HitTestInfo = Nothing
                Try
                    hit = chPCHRCellErrorBar.HitTest()
                Catch ex As Exception
                End Try
                If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                    Dim el As Element = CType(hit.Object, Element)
                    Dim categoryName As String = el.Name
                    dgvPCHRCellError.SuspendLayout()
                    gvPCHRCellError.ClearSelection()

                    Dim rowIndex() As Integer = gvPCHRCellError.GetSelectedRows()
                    For idx As Integer = 0 To rowIndex.Length - 1
                        Dim dr As DataRowView = TryCast(gvPCHRCellError.GetRow(idx), DataRowView)
                        If (dr.Row.Item(0).ToString = categoryName) Then
                            gvPCHRCellError.SelectRow(idx)
                            Exit For
                        End If
                    Next

                    dgvPCHRCellError.Refresh()
                    dgvPCHRCellError.ResumeLayout()

                    Dim dtCauseErrorCounter As DataTable = GetCellPieChartData(categoryName)
                    If (dtCauseErrorCounter IsNot Nothing) Then
                        Dim chartPCHR As ChartPCHR = New ChartPCHR() ''yy
                        chartPCHR.OverviewPieChart(chPCHRCellErrorPie, dtCauseErrorCounter, "Error Causes - Cell :" & categoryName)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub tsmiPCHR_CellCharHideShowGrid_Click(sender As Object, e As EventArgs) Handles tsmiPCHR_CellChartGridHideShow.Click
        Dim tempSplitCont As System.Windows.Forms.SplitContainer = GetSplitControl(cmPCHR_SourceControlCellGridHidShow)
        If (tempSplitCont IsNot Nothing) Then
            If (tempSplitCont.Panel2Collapsed.Equals(True)) Then
                tempSplitCont.Panel2Collapsed = False
                tempSplitCont.Panel1Collapsed = True
            Else
                tempSplitCont.Panel2Collapsed = True
                tempSplitCont.Panel1Collapsed = False
            End If
        End If
    End Sub

    Private Sub BindOverviewCellTab()
        If (xtcPCHRGridCSPS.SelectedTabPage.Tag IsNot Nothing) Then
            If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                If (Not dsPCHR_CS Is Nothing) Then
                    CellTab.BindCellTabFilter(dsPCHR_CS.Tables(1), cmbPCHRCellFilter, PCHRTab.OverviewGridType.CS)
                    cmbPCHRCellFilter.SelectedIndex = -1
                    cmbPCHRCellFilter.SelectedIndex = 0
                End If
            ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                If (Not dsPCHR_PS Is Nothing) Then
                    CellTab.BindCellTabFilter(dsPCHR_PS.Tables(1), cmbPCHRCellFilter, PCHRTab.OverviewGridType.PS)
                    cmbPCHRCellFilter.SelectedIndex = -1
                    cmbPCHRCellFilter.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Function GetCellPieChartData(Optional ByVal selectedChartValue As String = "0") As DataTable
        Dim dtData As DataTable = New DataTable
        If (cmbPCHRCellShow.SelectedItem Is Nothing) Then
            Return Nothing
        End If
        If (cmbPCHRCellFilter.SelectedItem Is Nothing) Then
            Return Nothing
        End If

        Dim showSelectedValue As String = cmbPCHRCellShow.SelectedItem.ToString
        Dim selectedFilter As String = cmbPCHRCellFilter.SelectedItem.ToString.Split("(")(0).TrimEnd
        Dim selectedFilterTag As String = cmbPCHRCellFilter.SelectedItem.Tag
        Dim cellTab As CellTab = New CellTab() ''yy
        Dim isByChart As Boolean = False
        If (Not selectedChartValue = "0") Then
            isByChart = True
        Else
            isByChart = False
        End If
        If (xtcPCHRGridCSPS.SelectedTabPage.Tag = "CS") Then
            If (showSelectedValue = "ErrorType") Then
                dtData = cellTab.GetCSCellPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorType)
            ElseIf (showSelectedValue = "ErrorReason") Then
                dtData = cellTab.GetCSCellPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorReason)
            ElseIf (showSelectedValue = "ErrorCause") Then
                dtData = cellTab.GetCSCellPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorCause)
            ElseIf (showSelectedValue = "Error IU Cause") Then
                dtData = cellTab.GetCSCellPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorIUCause)
            End If
        ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag = "PS") Then
            If (showSelectedValue = "ErrorType") Then
                dtData = cellTab.GetPSCellPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorType)
            ElseIf (showSelectedValue = "ErrorReason") Then
                dtData = cellTab.GetPSCellPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorReason)
            ElseIf (showSelectedValue = "ErrorCause") Then
                dtData = cellTab.GetPSCellPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorCause)
            ElseIf (showSelectedValue = "Error IU Cause") Then
                dtData = cellTab.GetPSCellPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorIUCause)
            End If
        End If
        Return dtData
    End Function

#End Region

#Region "IMSI Tab"

    Private Sub BindOverviewIMSITab()
        If (xtcPCHRGridCSPS.SelectedTabPage.Tag IsNot Nothing) Then
            If (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "CS") Then
                If (Not dsPCHR_CS Is Nothing) Then
                    IMSITab.BindIMSITabFilter(dsPCHR_CS.Tables(1), cmbPCHRIMSIFilter, PCHRTab.OverviewGridType.CS)
                    cmbPCHRIMSIFilter.SelectedIndex = -1
                    cmbPCHRIMSIFilter.SelectedIndex = 0
                End If
            ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag.ToString.ToUpper() = "PS") Then
                If (Not dsPCHR_PS Is Nothing) Then
                    IMSITab.BindIMSITabFilter(dsPCHR_PS.Tables(1), cmbPCHRIMSIFilter, PCHRTab.OverviewGridType.PS)
                    cmbPCHRIMSIFilter.SelectedIndex = -1
                    cmbPCHRIMSIFilter.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Sub cmbPCHRIMSIFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPCHRIMSIFilter.SelectedIndexChanged
        Try
            Application.UseWaitCursor = True
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (Not String.IsNullOrEmpty(cmbPCHRIMSIFilter.SelectedItem.ToString)) Then
                Dim chartPCHR As ChartPCHR = New ChartPCHR()
                Dim imsiTab As IMSITab = New IMSITab() ''yy
                Dim isCSorPS As PCHRTab.OverviewGridType

                If (xtcPCHRGridCSPS.SelectedTabPage.Tag = "CS") Then
                    dtOverviewIMSIBarChart = imsiTab.GetCSIMSIBarChartData(dsPCHR_CS.Tables(1), cmbPCHRIMSIFilter.SelectedItem.ToString.Split("(")(0), cmbPCHRIMSIFilter.SelectedItem.Tag)
                    isCSorPS = Library.PCHRTab.OverviewGridType.CS
                ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag = "PS") Then
                    dtOverviewIMSIBarChart = imsiTab.GetPSIMSIBarChartData(dsPCHR_PS.Tables(1), cmbPCHRIMSIFilter.SelectedItem.ToString.Split("(")(0), cmbPCHRIMSIFilter.SelectedItem.Tag)
                    isCSorPS = Library.PCHRTab.OverviewGridType.PS
                End If

                ChartComman.ChartDataClear(chPCHRIMSIErrorBar)
                If (dtOverviewIMSIBarChart IsNot Nothing AndAlso dtOverviewIMSIBarChart.Rows.Count > 0) Then
                    Dim seriesCounter As Integer = tstxtPCHR_CellAndIMSIBarChartShowTopX.Text.Trim
                    chartPCHR.OverviewCellBarChart(chPCHRIMSIErrorBar, dtOverviewIMSIBarChart.Rows.Cast(Of DataRow)().Take(seriesCounter).CopyToDataTable(), "Error Count per IMSI", "IMSI - Error Count") ''OverviewIMSIBarChart
                    SetPCHR_TabGridData(dgvPCHRIMSIError, gvPCHRIMSIError, dtOverviewIMSIBarChart)
                End If
            End If
            If (cmbPCHRIMSIShow.SelectedIndex >= 0) Then
                cmbPCHRIMSIShow_SelectedIndexChanged(Nothing, Nothing)
            Else
                cmbPCHRIMSIShow.SelectedIndex = 2
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub cmbPCHRIMSIShow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPCHRIMSIShow.SelectedIndexChanged
        Try
            Application.UseWaitCursor = True
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (cmbPCHRIMSIShow.SelectedIndex >= 0) Then
                Dim dtCauseErrorCounter As DataTable = GetIMSIPieChartData()
                If (dtCauseErrorCounter IsNot Nothing) Then
                    Dim chartPCHR As ChartPCHR = New ChartPCHR() ''yy
                    chartPCHR.OverviewPieChart(chPCHRIMSIErrorPie, dtCauseErrorCounter, "Error Causes - IMSI :" & cmbPCHRIMSIShow.SelectedItem.ToString)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub chPCHRIMSIErrorBar_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles chPCHRIMSIErrorBar.MouseDown
        Try
            Application.UseWaitCursor = True
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If e.Button = MouseButtons.Left Or e.Button = MouseButtons.Right Then
                Dim hit As HitTestInfo = Nothing
                Try
                    hit = chPCHRIMSIErrorBar.HitTest()
                Catch ex As Exception

                End Try

                If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                    Dim el As Element = CType(hit.Object, Element)
                    Dim categoryName As String = el.Name
                    dgvPCHRIMSIError.SuspendLayout()
                    gvPCHRIMSIError.ClearSelection()

                    For idx As Integer = 0 To gvPCHRIMSIError.RowCount - 1
                        Dim dr As DataRowView = TryCast(gvPCHRIMSIError.GetRow(idx), DataRowView)
                        If (dr.Row.Item(0).ToString = categoryName) Then
                            gvPCHRIMSIError.SelectRow(idx)
                            Exit For
                        End If
                    Next

                    dgvPCHRIMSIError.Refresh()
                    dgvPCHRIMSIError.ResumeLayout()

                    Dim dtCauseErrorCounter As DataTable = GetIMSIPieChartData(categoryName)
                    If (dtCauseErrorCounter IsNot Nothing) Then
                        Dim chartPCHR As ChartPCHR = New ChartPCHR() ''yy
                        chartPCHR.OverviewPieChart(chPCHRIMSIErrorPie, dtCauseErrorCounter, "Error Causes - IMSI :" & categoryName)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Function GetIMSIPieChartData(Optional ByVal selectedChartValue As String = "0") As DataTable
        Try
            Dim dtData As DataTable = New DataTable
            If (cmbPCHRIMSIShow.SelectedItem Is Nothing) Then
                Return Nothing
            End If
            If (cmbPCHRIMSIFilter.SelectedItem Is Nothing) Then
                Return Nothing
            End If

            Dim showSelectedValue As String = cmbPCHRIMSIShow.SelectedItem.ToString
            Dim selectedFilter As String = cmbPCHRIMSIFilter.SelectedItem.ToString.Split("(")(0).TrimEnd
            Dim selectedFilterTag As String = cmbPCHRIMSIFilter.SelectedItem.Tag
            Dim imsiTab As IMSITab = New IMSITab() ''yy
            Dim isByChart As Boolean = False
            If (Not selectedChartValue = "0") Then
                isByChart = True
            Else
                isByChart = False
            End If
            If (xtcPCHRGridCSPS.SelectedTabPage.Tag = "CS") Then
                If (showSelectedValue = "ErrorType") Then
                    dtData = imsiTab.GetCSIMSIPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorType)
                ElseIf (showSelectedValue = "ErrorReason") Then
                    dtData = imsiTab.GetCSIMSIPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorReason)
                ElseIf (showSelectedValue = "ErrorCause") Then
                    dtData = imsiTab.GetCSIMSIPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorCause)
                ElseIf (showSelectedValue = "Error IU Cause") Then
                    dtData = imsiTab.GetCSIMSIPieChartData(dsPCHR_CS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorIUCause)
                End If
            ElseIf (xtcPCHRGridCSPS.SelectedTabPage.Tag = "PS") Then
                If (showSelectedValue = "ErrorType") Then
                    dtData = imsiTab.GetPSIMSIPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorType)
                ElseIf (showSelectedValue = "ErrorReason") Then
                    dtData = imsiTab.GetPSIMSIPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorReason)
                ElseIf (showSelectedValue = "ErrorCause") Then
                    dtData = imsiTab.GetPSIMSIPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorCause)
                ElseIf (showSelectedValue = "Error IU Cause") Then
                    dtData = imsiTab.GetPSIMSIPieChartData(dsPCHR_PS.Tables(1), selectedFilter, selectedFilterTag, isByChart, selectedChartValue, Library.PCHRTab.PieChartErrorType.ErrorIUCause)
                End If
            End If
            Return dtData
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

End Class
