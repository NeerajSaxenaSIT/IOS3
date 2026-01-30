Imports System.ComponentModel
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmNBManagement

#Region "Variables"

    Private dtNBCampaigns As DataTable = Nothing
    Private countOT As Integer = 0
    Private cmsSourceControl As GridControl = Nothing
    Private dtMmlUserFilter As DataTable = Nothing

    Private dtObjectTypes As New DataTable
    Private dsObjects As New DataSet

    Private objThreadDetect As System.Threading.Thread
    Private objThreadCopy As System.Threading.Thread
    Private objThreadDelete As System.Threading.Thread
    Private objThreadAudit As System.Threading.Thread

    Private IsErrorInCopy As Boolean = False

    Private Delegate Sub CallThreadInvokedDetect(Row As DataRow, Status As Integer)
    Private objDetectThreadLock As New Object

    Private Delegate Sub CallThreadInvokedCopy(Row As DataRow, Status As Integer)
    Private objCopyThreadLock As New Object

    Private Delegate Sub CallThreadInvokedDelete(Row As DataRow, Status As Integer)
    Private objDeleteThreadLock As New Object

    Private Delegate Sub CallThreadInvokedAudit(Row As DataRow, Status As Integer)
    Private objAuditThreadLock As New Object

    Private Delegate Sub CallThreadInvokedNBManual(Row As DataRow)
    Private objNBManualThreadLock As New Object

    Private lstTreeNode As New List(Of TreeNode)

#End Region

#Region "Load Event"

    Private Sub frmNBManagement_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Me.SuspendLayout()

            dlgAddCampaign.newManualCampaignAdded = Nothing
            dlgAddCampaign.newAuditCampaignAdded = Nothing

            If IsThreshHoldBreached() Then
                tlpMain.RowStyles.Item(0).Height = 30
                tlpMain.RowStyles.Item(0).SizeType = SizeType.Absolute
                lblIntegrityMsg.Appearance.Image = My.Resources.red_box
                lblIntegrityMsg.Text = "Warning - Check Data Integrity"
            Else
                tlpMain.RowStyles.Item(0).Height = 0
                tlpMain.RowStyles.Item(0).SizeType = SizeType.Absolute
            End If

            HideDescriptionArea(layerPropGridDetect)
            HideDescriptionArea(layerPropGridCopy)
            HideDescriptionArea(grdPropertyNBAudit)

            LoadCellList()
            LoadLayers()
            FillNBDetectCampaigns()
            FillNBCopyCampaigns()
            FillNBDeleteCampaigns()
            LoadManualCampaigns()
            LoadMMLCampaign()
            LoadMmlConfiguration()
            FillNBAuditCampaigns()

            LoadNBCombos(4557, cmbMMLConfigIDNBAudit)
            LoadNBCombos(4558, cmbTechnologyNBAudit)
            LoadNBCombos(4559, cmbSLayerNBAudit)
            LoadNBCombos(4560, cmbTLayerNBAudit)
            LoadNBCombos(4568, cmbNBType)
            LoadNBCombos(4569, cmbMMLScriptID)
            LoadNBCombos(4513, cmbTechnology)
            BindDevExComboBoxWithValueMember(cmbInclusionListNBAudit, dtCellList, "ListId", "ListName", "Select")

            'Dim cmbValue As New IOS.Library.clsComboBoxItem()
            'cmbValue.Text = "PLMN"
            'cmbValue.Value = 0
            'Dim cmbItem As New DevExpress.XtraEditors.Controls.ComboBoxItem()
            'cmbItem.Value = cmbValue
            'cmbInclusionListNBAudit.Properties.Items.Insert(1, cmbItem)
            'cmbInclusionListNBAudit.SelectedIndex = 1

            BindDevExComboBoxWithValueMember(cmbExclusionListNBAudit, dtCellList, "ListId", "ListName", "Select")

            ConfigurNBForm(Me.Name)
            btnPreFilter.Enabled = True
            seFileSize.Enabled = False
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.ResumeLayout()
        End Try
    End Sub

    Public Sub ConfigurNBForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                tsmiAddNewRow, tsmiCloneSelectedRows, tsmiDeleteSelectedRows, tsmiMapSelectedNB, tsmiTagPastePaste, tsmi_Manual_DeleteRows,
                tpNBDelete, tpNBAudit
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

#End Region

#Region "Common Events"

    Private Sub btnListMngr_Click(sender As Object, e As EventArgs) Handles btnListMngrDetect.Click, btnListMngrCopy.Click, btnListManagerNBAudit.Click
        frmListManager.Show()
    End Sub

    Private Sub gridViewCampaign_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gvDetectCampaigns.RowCellStyle, gvCopyCampaigns.RowCellStyle, gvCampaignManual.RowCellStyle, gvMmlCampaign.RowCellStyle, gvCampNBAudit.RowCellStyle
        Try
            If e.RowHandle > -1 And e.Column.FieldName = "LastStatus" Then
                If e.CellValue IsNot Nothing Then
                    If e.CellValue.ToString = "Idle" Then
                        e.Appearance.BackColor = Color.Wheat
                    ElseIf e.CellValue.ToString = "Running" Then
                        e.Appearance.BackColor = Color.YellowGreen
                    ElseIf e.CellValue.ToString = "Error" Then
                        e.Appearance.BackColor = Color.OrangeRed
                    End If
                End If
                e.Appearance.BackColor2 = Color.White
                e.Appearance.ForeColor = Color.Black
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ceActive_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim ceActive As CheckEdit = CType(sender, CheckEdit)
            UpdateCampaign(ceActive.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ceIsPublic_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim ceIsPublic As CheckEdit = CType(sender, CheckEdit)
            UpdateCampaign(ceIsPublic.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub deScheduleNextStartTime_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Dim datEdit As DateEdit = CType(sender, DateEdit)
            UpdateCampaign(datEdit.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbScheduleRepeatInterval_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cmb As ComboBoxEdit = CType(sender, ComboBoxEdit)
            UpdateCampaign(cmb.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteDetectResultSet_Click(sender As Object, e As EventArgs) Handles btnDeleteDetectResultSet.Click, btnDeleteCopyResultSet.Click, btnDeleteResultSetIdNBAudit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim btn As SimpleButton = CType(sender, SimpleButton)
            Dim cmb As ComboBoxEdit
            If btn.Tag = "NB_Audit" Then
                cmb = cmbResultSetIdNBAudit
            ElseIf btn.Tag = "NB_Copy" Then
                cmb = cmbCopyResultSetID
            Else
                cmb = cmbDetectResultSetID
            End If
            DeleteCampaignResultSet(cmb)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmsConfigurationSummary_Opening(sender As Object, e As CancelEventArgs) Handles cmsConfigurationSummary.Opening
        Try
            Dim cms As ContextMenuStrip = CType(sender, ContextMenuStrip)
            cmsSourceControl = CType(cms.SourceControl, GridControl)
            If cmsSourceControl IsNot Nothing Then
                Dim Owner As String = ""
                tsmiAddNewRow.Visible = True
                If cmsSourceControl.Tag = "NB_Detect" Then
                    Owner = lblOwnerDetect.Text
                ElseIf cmsSourceControl.Tag = "NB_Copy" Then
                    Owner = lblOwnerCopy.Text
                ElseIf cmsSourceControl.Tag = "NB_Audit" Then
                    Owner = lblOwnerNBAudit.Text
                    tsmiAddNewRow.Visible = False
                End If
                If Owner.ToLower <> Environment.UserName.ToLower Then
                    tsmiDeleteSelectedRows.Enabled = False
                    tsmiCloneSelectedRows.Enabled = False
                    tsmiAddNewRow.Enabled = False
                Else
                    tsmiDeleteSelectedRows.Enabled = True
                    tsmiCloneSelectedRows.Enabled = True
                    tsmiAddNewRow.Enabled = True
                End If
            Else
                tsmiAddNewRow.Enabled = False
                tsmiDeleteSelectedRows.Enabled = False
                tsmiCloneSelectedRows.Enabled = False
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub DeleteConfigProperty(campaignID As Integer, configID As Integer, campaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        parray = {
            New String() {"@campaignID", campaignID},
            New String() {"@configID", configID},
            New String() {"@campaignType", Chr(39) & campaignType & Chr(39)}
        }
        strConnection = GetSQL(4548, parray)(0)
        sqlParam = GetSQL(4548, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub tsmiDeleteSelectedRows_Click(sender As Object, e As EventArgs) Handles tsmiDeleteSelectedRows.Click
        Try
            If cmsSourceControl IsNot Nothing Then
                Dim gvConfig As GridView = DirectCast(cmsSourceControl.MainView, GridView)
                Dim drCampaign As DataRow = Nothing

                If cmsSourceControl.Tag = "NB_Detect" Then
                    Dim rIndex2() As Integer = gvDetectCampaigns.GetSelectedRows()
                    If rIndex2.Length > 0 Then
                        drCampaign = gvDetectCampaigns.GetRow(rIndex2(0)).Row
                    End If
                ElseIf cmsSourceControl.Tag = "NB_Copy" Then
                    Dim rIndex2() As Integer = gvCopyCampaigns.GetSelectedRows()
                    If rIndex2.Length > 0 Then
                        drCampaign = gvCopyCampaigns.GetRow(rIndex2(0)).Row
                    End If
                ElseIf cmsSourceControl.Tag = "NB_Audit" Then
                    Dim rIndex2() As Integer = gvCampNBAudit.GetSelectedRows()
                    If rIndex2.Length > 0 Then
                        drCampaign = gvCampNBAudit.GetRow(rIndex2(0)).Row
                    End If
                End If

                If drCampaign IsNot Nothing Then
                    Dim rIndex() As Integer = gvConfig.GetSelectedRows()
                    For i As Integer = 0 To rIndex.Length - 1
                        DeleteConfigProperty(drCampaign.Item("CampaignID"), gvConfig.GetRowCellValue(rIndex(i), "ConfigID"), cmsSourceControl.Tag)
                    Next
                    LoadConfigSummaryGrid(drCampaign("CampaignID"), drCampaign("CampaignType"))
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiCloneSelectedRows_Click(sender As Object, e As EventArgs) Handles tsmiCloneSelectedRows.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmsSourceControl IsNot Nothing Then
                Dim drConfig As DataRow = Nothing
                Dim campaignID As Integer
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = Nothing

                If cmsSourceControl.Tag = "NB_Detect" Then
                    Dim rIndex() As Integer = gvConfigSummDetect.GetSelectedRows()
                    Dim rIndex2() As Integer = gvDetectCampaigns.GetSelectedRows()
                    If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                        Dim drCampaign As DataRow = gvDetectCampaigns.GetRow(rIndex2(0)).Row
                        campaignID = drCampaign("CampaignID")
                        drConfig = gvConfigSummDetect.GetRow(rIndex(0)).Row
                    End If
                ElseIf cmsSourceControl.Tag = "NB_Copy" Then
                    Dim rIndex() As Integer = gvConfigSummCopy.GetSelectedRows()
                    Dim rIndex2() As Integer = gvCopyCampaigns.GetSelectedRows()
                    If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                        Dim drCampaign As DataRow = gvCopyCampaigns.GetRow(rIndex2(0)).Row
                        campaignID = drCampaign("CampaignID")
                        drConfig = gvConfigSummCopy.GetRow(rIndex(0)).Row
                    End If
                ElseIf cmsSourceControl.Tag = "NB_Audit" Then
                    Dim rIndex() As Integer = gvConfigNBAudit.GetSelectedRows()
                    Dim rIndex2() As Integer = gvCampNBAudit.GetSelectedRows()
                    If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                        Dim drCampaign As DataRow = gvCampNBAudit.GetRow(rIndex2(0)).Row
                        campaignID = drCampaign("CampaignID")
                        drConfig = gvCampNBAudit.GetRow(rIndex(0)).Row
                    End If
                End If

                If drConfig IsNot Nothing Then
                    parray = {
                                New String() {"@CampaignID", campaignID},
                                New String() {"@SelectedConfigID", drConfig.Item("ConfigID")},
                                New String() {"@CampaignType", "'" & cmsSourceControl.Tag & "'"}
                             }

                    strConnection = GetSQL(4567, parray)(0)
                    sqlParam = GetSQL(4567, parray)(1)

                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    LoadConfigSummaryGrid(campaignID, cmsSourceControl.Tag)
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

    Private Sub AddNew_NBConfig_Row(campaignID As Integer, campaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
                                        New String() {"@campaignID", campaignID},
                                        New String() {"@campaignType", Chr(39) & campaignType & Chr(39)}
                                   }

        strConnection = GetSQL(4554, parray)(0)
        sqlParam = GetSQL(4554, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub tsmiAddNewRow_Click(sender As Object, e As EventArgs) Handles tsmiAddNewRow.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmsSourceControl IsNot Nothing Then
                Dim drCampaign As DataRow = Nothing
                If cmsSourceControl.Tag = "NB_Detect" Then
                    Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()
                    If rIndex.Length > 0 Then
                        drCampaign = gvDetectCampaigns.GetRow(rIndex(0)).Row
                    End If
                ElseIf cmsSourceControl.Tag = "NB_Copy" Then
                    Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()
                    If rIndex.Length > 0 Then
                        drCampaign = gvCopyCampaigns.GetRow(rIndex(0)).Row
                    End If
                End If

                If drCampaign IsNot Nothing Then
                    AddNew_NBConfig_Row(drCampaign.Item("CampaignID"), drCampaign.Item("CampaignType"))
                    LoadConfigSummaryGrid(drCampaign.Item("CampaignID"), drCampaign.Item("CampaignType"))
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

    Private Sub cmsMapNB_Opening(sender As Object, e As CancelEventArgs) Handles cmsMapNB.Opening
        Try
            cmsSourceControl = CType(cmsMapNB.SourceControl, GridControl)
            Dim dt As DataTable = cmsSourceControl.DataSource
            If dt.Rows.Count = 0 Then
                tsmiMapSelectedNB.Enabled = False
            Else
                If dt.Columns.Contains("S_IOS_CELL_GID") AndAlso dt.Columns.Contains("T_IOS_CELL_GID") Then
                    tsmiMapSelectedNB.Enabled = True
                Else
                    tsmiMapSelectedNB.Enabled = False
                End If
            End If
            dt.Dispose()
            dt = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tsmiMapSelectedNB_Click(sender As Object, e As EventArgs) Handles tsmiMapSelectedNB.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmsSourceControl IsNot Nothing Then
                Dim gc As DevExpress.XtraGrid.GridControl = DirectCast(cmsSourceControl, DevExpress.XtraGrid.GridControl)
                Dim dt As DataTable = DirectCast(gc.DataSource, DataTable)
                Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = CType(gc.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                Dim rIndex() As Integer = gv.GetSelectedRows()

                Dim dtNBPlot As DataTable = dt.Clone()
                For i As Integer = 0 To rIndex.Length - 1
                    dtNBPlot.ImportRow(gv.GetDataRow(rIndex(i)))
                Next
                dtNBPlot.AcceptChanges()

                MapInfo.Engine.Session.Current.Selections.DefaultSelection.Clear()
                For Each dr As DataRow In dtNBPlot.DefaultView.ToTable(True, "S_IOS_CELL_GID", "S_LAYER").Rows
                    IsClearDefaultSelection = False
                    frmMapWindow.Cells_SearchAndDisplay("IOS_CELL_GID", dr.Item("S_IOS_CELL_GID").ToString, True, dr.Item("S_LAYER").ToString)
                    IsClearDefaultSelection = True
                Next

                MapSelectedNB(dtNBPlot)

            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "NB Detect"

    Private Sub FillNBDetectCampaigns()
        Try

            LoadCampaigns()

            Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "LastRunTime", "LastEndTime", "CampaignType", "IsPublic"}
            Dim dtDetectCamp As New DataTable
            dtDetectCamp = dtNBCampaigns.AsEnumerable().Where(Function(x) x.Field(Of String)("CampaignType") = "NB_Detect").CopyToDataTable()
            'dtDetectCamp.Columns(0).ColumnName = "ID"

            Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()

            RemoveHandler gvDetectCampaigns.FocusedRowChanged, AddressOf gvDetectCampaigns_FocusedRowChanged
            RemoveHandler gvDetectCampaigns.RowClick, AddressOf gvDetectCampaigns_RowClick
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcDetectCampaigns, gvDetectCampaigns, dtDetectCamp, "ALL", columnsToHide, "CampaignName")
            gvDetectCampaigns.Columns(0).Caption = "ID"
            gvDetectCampaigns.Columns(0).BestFit()
            AddHandler gvDetectCampaigns.FocusedRowChanged, AddressOf gvDetectCampaigns_FocusedRowChanged
            AddHandler gvDetectCampaigns.RowClick, AddressOf gvDetectCampaigns_RowClick

            If gvDetectCampaigns.RowCount > 0 Then
                gvDetectCampaigns.ClearSelection()
                If rIndex.Length > 0 Then
                    gvDetectCampaigns.SelectRow(rIndex(0))
                    gvDetectCampaigns.FocusedRowHandle = rIndex(0)
                Else
                    gvDetectCampaigns.SelectRow(0)
                    gvDetectCampaigns.FocusedRowHandle = 0
                End If
                gvDetectCampaigns_FocusedRowChanged(Nothing, Nothing)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvDetectCampaigns_RowClick(sender As Object, e As RowClickEventArgs)
        If gvCopyCampaigns.RowCount > 0 Then
            gvDetectCampaigns_FocusedRowChanged(gvDetectCampaigns, Nothing)
        End If
    End Sub

    Private Sub gvDetectCampaigns_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvDetectCampaigns.RowCount > 0 AndAlso e IsNot Nothing Then
                gvDetectCampaigns.ClearSelection()
                gvDetectCampaigns.FocusedRowHandle = e.FocusedRowHandle
                gvDetectCampaigns.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler deSchNxtStartTimeDetect.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            RemoveHandler ceActiveDetect.CheckedChanged, AddressOf ceActive_CheckedChanged
            RemoveHandler ceIsPublicDetect.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            RemoveHandler cmbSchRptIntervalDetect.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged

            Dim dr As DataRow = gvDetectCampaigns.GetFocusedDataRow()
            If dr IsNot Nothing Then

                lblOwnerDetect.Text = dr("CampaignOwner")
                lblLastRunTimeDetect.Text = dr("LastRunTime")
                lblLastEndTimeDetect.Text = dr("LastEndTime")

                If dr("LastStatus").ToString = "Running" Then
                    btnRunNowDetect.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowDetect.Text = "Abort Run!"
                Else
                    btnRunNowDetect.LookAndFeel.UseDefaultLookAndFeel = True
                    btnRunNowDetect.Text = "Run Now"
                End If

                Dim drCampaignDetail As DataRow = GetCampaignDetailsByID(dr("CampaignID"))
                If drCampaignDetail IsNot Nothing Then
                    ceActiveDetect.Checked = IIf(IsDBNull(drCampaignDetail("CampaignEnabled")), False, drCampaignDetail("CampaignEnabled"))
                    ceIsPublicDetect.Checked = IIf(IsDBNull(drCampaignDetail("IsPublic")), False, drCampaignDetail("IsPublic"))
                    deSchNxtStartTimeDetect.EditValue = drCampaignDetail("ScheduleNextStartDate")
                    cmbSchRptIntervalDetect.SelectedItem = drCampaignDetail("ScheduleRepeatInterval")
                End If

                'Load campaign configuration summary grid
                LoadConfigSummaryGrid(dr("CampaignID"), dr("CampaignType"))
                LoadResultSetCombo(cmbDetectResultSetID, dr("CampaignID"), dr("CampaignType"))

            End If

            lblDetectDataRowCount.Visible = False
            'Enable/disable control if the current user is not the owner of the campaign.
            If lblOwnerDetect.Text.ToLower <> Environment.UserName.ToLower Then
                lblOwnerDetect.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblOwnerDetect.ForeColor = Color.Red
                ceIsPublicDetect.Enabled = False

                If ceIsPublicDetect.Checked Then
                    ceActiveDetect.Enabled = True
                    deSchNxtStartTimeDetect.Enabled = False
                    cmbSchRptIntervalDetect.Enabled = False

                    btnDeleteDetect.Enabled = True
                    grpLayerPropDetect.Enabled = True
                Else
                    ceActiveDetect.Enabled = False
                    deSchNxtStartTimeDetect.Enabled = False
                    cmbSchRptIntervalDetect.Enabled = False

                    btnDeleteDetect.Enabled = False
                    grpLayerPropDetect.Enabled = False
                End If
            Else
                lblOwnerDetect.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblOwnerDetect.ForeColor = Color.Black

                ceActiveDetect.Enabled = True
                ceIsPublicDetect.Enabled = True
                deSchNxtStartTimeDetect.Enabled = False
                cmbSchRptIntervalDetect.Enabled = False

                btnDeleteDetect.Enabled = True
                grpLayerPropDetect.Enabled = True
            End If

            AddHandler deSchNxtStartTimeDetect.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            AddHandler ceActiveDetect.CheckedChanged, AddressOf ceActive_CheckedChanged
            AddHandler ceIsPublicDetect.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            AddHandler cmbSchRptIntervalDetect.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvConfigSummDetect_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            LoadCellList()
            LoadLayers()

            Dim dr As DataRow = gvConfigSummDetect.GetFocusedDataRow()
            layerPropGridDetect.Tag = Nothing
            layerPropGridDetect.Tag = dr
            LoadLayerProperties(layerPropGridDetect, "NB_Detect", dr)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDetectRefresh_Click(sender As Object, e As EventArgs) Handles btnDetectRefresh.Click, btnCopyRefresh.Click, btnDeleteRefresh.Click, BtnRefreshCampNBAudit.Click, btnRefreshManual.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim btn As SimpleButton = CType(sender, SimpleButton)
            If btn.Tag = "NB_Detect" Then
                txtSearchDetect.Text = ""
                FillNBDetectCampaigns()
            ElseIf btn.Tag = "NB_Copy" Then
                txtSearchCopy.Text = ""
                FillNBCopyCampaigns()
            ElseIf btn.Tag = "NB_Delete" Then
                txtSearchDelete.Text = ""
                FillNBDeleteCampaigns()
            ElseIf btn.Tag = "NB_Manual" Then
                txtSearchManual.Text = ""
                LoadManualCampaigns()
            ElseIf btn.Tag = "NB_Audit" Then
                txtNBAuditCampSearch.Text = ""
                FillNBAuditCampaigns()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCloneDetect_Click(sender As Object, e As EventArgs) Handles btnCloneDetect.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvDetectCampaigns.GetRow(rIndex(0)).Row
                dlgCampaignClone.campaignID = dr("CampaignID")
                dlgCampaignClone.campaignType = dr("CampaignType").ToString
                If dlgCampaignClone.ShowDialog() = DialogResult.OK Then
                    FillNBDetectCampaigns()
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

    Private Sub btnDeleteDetect_Click(sender As Object, e As EventArgs) Handles btnDeleteDetect.Click
        Try
            Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvDetectCampaigns.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    DeleteCampaign(dr("CampaignID"), dr("CampaignType").ToString)
                    gvDetectCampaigns.DeleteRow(rIndex(0))
                    If gvDetectCampaigns.RowCount > 0 Then
                        gvDetectCampaigns.ClearSelection()
                        gvDetectCampaigns.SelectRow(0)
                        gvDetectCampaigns.FocusedRowHandle = 0
                        gvDetectCampaigns_FocusedRowChanged(Nothing, Nothing)
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

    Private Sub btnRunNowDetect_Click(sender As Object, e As EventArgs) Handles btnRunNowDetect.Click
        Try
            Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvDetectCampaigns.GetRow(rIndex(0)).Row
                Dim campaignID As Integer = dr("CampaignID")
                Dim campaignType As String = dr("CampaignType").ToString

                If btnRunNowDetect.Text = "Abort Run!" Then
                    objThreadDetect.Abort()
                Else
                    btnRunNowDetect.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowDetect.Text = "Abort Run!"
                    dr("LastStatus") = "Running"
                    gcDetectCampaigns.Refresh()
                    Application.DoEvents()

                    Dim objRunDetect As New RunNowClass()
                    objRunDetect.campaignID = campaignID
                    objRunDetect.campaignType = campaignType
                    objRunDetect.Status = 1
                    objRunDetect.CampaignRow = dr
                    AddHandler objRunDetect.ThreadComplete, AddressOf ExecuteAfterDetectThreadComplete
                    objThreadDetect = New System.Threading.Thread(AddressOf objRunDetect.RunNow)
                    objThreadDetect.Start()

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfterDetectThreadComplete(row As DataRow, Status As Integer, ti As Threading.Thread)
        SyncLock objDetectThreadLock
            LoadCampaigns()
            Dim arg() As Object = {row, Status}
            Me.BeginInvoke(New CallThreadInvokedDetect(AddressOf SetDetectCampaignLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetDetectCampaignLastStatus(Row As DataRow, Status As Integer)
        SyncLock objDetectThreadLock
            If Row IsNot Nothing Then
                If Status = 0 Then
                    Row("LastStatus") = "Idle"
                ElseIf Status = 1 Then
                    Row("LastStatus") = "Running"
                ElseIf Status = -1 Then
                    Row("LastStatus") = "Error"
                End If
                gcDetectCampaigns.Refresh()
                btnRunNowDetect.LookAndFeel.UseDefaultLookAndFeel = True
                btnRunNowDetect.Text = "Run Now"
                Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dr As DataRow = gvDetectCampaigns.GetRow(rIndex(0)).Row
                    If Row("CampaignID") = dr("CampaignID") Then
                        LoadResultSetCombo(cmbDetectResultSetID, Row("CampaignID"), Row("CampaignType"))
                    End If
                End If
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub txtSearchDetect_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchDetect.KeyUp
        Try
            Dim dtDetectCamp As DataTable = CType(gcDetectCampaigns.DataSource, DataTable)
            If dtDetectCamp IsNot Nothing Then
                If (txtSearchDetect.Text.Length > 0) Then
                    dtDetectCamp.DefaultView.RowFilter = "[CampaignName] Like '%" & txtSearchDetect.Text.Trim & "%' Or Convert([CampaignID],'System.String') Like '%" & txtSearchDetect.Text.Trim & "%'"
                Else
                    dtDetectCamp.DefaultView.RowFilter = ""
                End If
            End If
            gvDetectCampaigns_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbResultSetID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDetectResultSetID.SelectedIndexChanged
        If cmbDetectResultSetID.SelectedIndex > 0 Then
            LoadResultSummaryGrid(gcCampSummDetect, cmbDetectResultSetID.SelectedText.Trim(), cmbDetectResultSetID.Tag)
        Else
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcCampSummDetect)
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcCampDataDetect)
        End If
        lblDetectDataRowCount.Visible = False
    End Sub

    Private Sub btnDetectDataLoadGrid_Click(sender As Object, e As EventArgs) Handles btnDetectDataLoadGrid.Click
        If cmbDetectResultSetID.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Loading...")
                Dim parray()() As String = {
                    New String() {"@ResultSetID", Chr(39) & cmbDetectResultSetID.SelectedItem.ToString & Chr(39)},
                    New String() {"@CampaignType", Chr(39) & cmbDetectResultSetID.Tag & Chr(39)}
                }
                Dim strConnection As String = GetSQL(4538, parray)(0)
                Dim sqlParam As String = GetSQL(4538, parray)(1)
                Dim dtDetectDataGrid As New DataTable

                dtDetectDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCampDataDetect, gvCampDataDetect, dtDetectDataGrid, "ALL")
                lblDetectDataRowCount.Text = "Count of Records: " & gvCampDataDetect.RowCount
                lblDetectDataRowCount.Visible = True
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Detect Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbDetectResultSetID.Focus()
        End If
    End Sub

    Private Sub btnDetectDataAllCsv_Click(sender As Object, e As EventArgs) Handles btnDetectDataAllCsv.Click
        If cmbDetectResultSetID.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Writing data to CSV...")
                Dim parray()() As String = {
                    New String() {"@ResultSetID", Chr(39) & cmbDetectResultSetID.SelectedItem.ToString & Chr(39)},
                    New String() {"@CampaignType", Chr(39) & cmbDetectResultSetID.Tag & Chr(39)}
                }
                Dim strConnection As String = GetSQL(4539, parray)(0)
                Dim sqlParam As String = GetSQL(4539, parray)(1)
                Dim dtDetectDataGrid As New DataTable

                dtDetectDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

                Dim objFileDlg As New SaveFileDialog()
                objFileDlg.InitialDirectory = "C:\"
                objFileDlg.Filter = "Comma Delimited|*.csv"
                objFileDlg.Title = "Save a CSV File"
                objFileDlg.FileName = gvDetectCampaigns.GetRowCellValue(gvDetectCampaigns.GetSelectedRows()(0), "CampaignName") & "_" & cmbDetectResultSetID.SelectedItem.ToString
                If objFileDlg.ShowDialog() = DialogResult.OK Then
                    If objFileDlg.FileName <> "" Then
                        Dim Content() As Byte = CSVBytesWriter(dtDetectDataGrid)
                        Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                        fs.Write(Content, 0, Content.Length)
                        fs.Close()
                    End If
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Detect Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbDetectResultSetID.Focus()
        End If
    End Sub

#End Region

#Region "NB Copy"

    Private Sub FillNBCopyCampaigns()
        Try
            LoadCampaigns()

            Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "LastRunTime", "LastEndTime", "CampaignType", "IsPublic"}
            Dim dtCopyCamp As New DataTable
            dtCopyCamp = dtNBCampaigns.AsEnumerable().Where(Function(x) x.Field(Of String)("CampaignType") = "NB_Copy").CopyToDataTable()
            'dtCopyCamp.Columns(0).ColumnName = "ID"

            Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()

            RemoveHandler gvCopyCampaigns.FocusedRowChanged, AddressOf gvCopyCampaigns_FocusedRowChanged
            RemoveHandler gvCopyCampaigns.RowClick, AddressOf gvCopyCampaigns_RowClick
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCopyCampaigns, gvCopyCampaigns, dtCopyCamp, "ALL", columnsToHide, "CampaignName")
            gvCopyCampaigns.Columns(0).Caption = "ID"
            gvCopyCampaigns.Columns(0).BestFit()
            AddHandler gvCopyCampaigns.FocusedRowChanged, AddressOf gvCopyCampaigns_FocusedRowChanged
            AddHandler gvCopyCampaigns.RowClick, AddressOf gvCopyCampaigns_RowClick

            If gvCopyCampaigns.RowCount > 0 Then
                gvCopyCampaigns.ClearSelection()
                If rIndex.Length > 0 Then
                    gvCopyCampaigns.SelectRow(rIndex(0))
                    gvCopyCampaigns.FocusedRowHandle = rIndex(0)
                Else
                    gvCopyCampaigns.SelectRow(0)
                    gvCopyCampaigns.FocusedRowHandle = 0
                End If
                gvCopyCampaigns_FocusedRowChanged(Nothing, Nothing)
            End If
        Catch
        End Try
    End Sub

    Private Sub gvCopyCampaigns_RowClick(sender As Object, e As RowClickEventArgs)
        If gvCopyCampaigns.RowCount > 0 Then
            gvCopyCampaigns_FocusedRowChanged(gvCopyCampaigns, Nothing)
        End If
    End Sub

    Private Sub gvCopyCampaigns_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvCopyCampaigns.RowCount > 0 AndAlso e IsNot Nothing Then
                gvCopyCampaigns.ClearSelection()
                gvCopyCampaigns.FocusedRowHandle = e.FocusedRowHandle
                gvCopyCampaigns.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler deSchNxtStartTimeCopy.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            RemoveHandler ceActiveCopy.CheckedChanged, AddressOf ceActive_CheckedChanged
            RemoveHandler ceIsPublicCopy.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            RemoveHandler cmbSchRptIntervalCopy.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged


            Dim dr As DataRow = gvCopyCampaigns.GetFocusedDataRow()
            If dr IsNot Nothing Then

                lblOwnerCopy.Text = dr("CampaignOwner")
                lblLastRunTimeCopy.Text = dr("LastRunTime")
                lblLastEndTimeCopy.Text = dr("LastEndTime")

                If dr("LastStatus").ToString = "Running" Then
                    btnRunNowCopy.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowCopy.Text = "Abort Run!"
                Else
                    btnRunNowCopy.LookAndFeel.UseDefaultLookAndFeel = True
                    btnRunNowCopy.Text = "Run Now"
                End If

                Dim drCampaignDetail As DataRow = GetCampaignDetailsByID(dr("CampaignID"))
                If drCampaignDetail IsNot Nothing Then
                    ceActiveCopy.Checked = IIf(IsDBNull(drCampaignDetail("CampaignEnabled")), False, drCampaignDetail("CampaignEnabled"))
                    ceIsPublicCopy.Checked = IIf(IsDBNull(drCampaignDetail("IsPublic")), False, drCampaignDetail("IsPublic"))
                    deSchNxtStartTimeCopy.EditValue = drCampaignDetail("ScheduleNextStartDate")
                    cmbSchRptIntervalCopy.SelectedItem = drCampaignDetail("ScheduleRepeatInterval")
                End If

                'Load campaign configuration summary grid
                LoadConfigSummaryGrid(dr("CampaignID"), dr("CampaignType"))
                LoadResultSetCombo(cmbCopyResultSetID, dr("CampaignID"), dr("CampaignType"))

            End If

            lblCopyDataRowCount.Visible = False
            'Enable/disable control if the current user is not the owner of the campaign.
            If lblOwnerCopy.Text.ToLower <> Environment.UserName.ToLower Then
                lblOwnerCopy.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblOwnerCopy.ForeColor = Color.Red
                ceIsPublicCopy.Enabled = False

                If ceIsPublicCopy.Checked Then
                    ceActiveCopy.Enabled = True
                    deSchNxtStartTimeCopy.Enabled = False
                    cmbSchRptIntervalCopy.Enabled = False

                    btnDeleteCopy.Enabled = True
                    grpLayerPropCopy.Enabled = True
                Else
                    ceActiveCopy.Enabled = False
                    deSchNxtStartTimeCopy.Enabled = False
                    cmbSchRptIntervalCopy.Enabled = False

                    btnDeleteCopy.Enabled = False
                    grpLayerPropCopy.Enabled = False
                End If
            Else
                lblOwnerCopy.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblOwnerCopy.ForeColor = Color.Black

                ceActiveCopy.Enabled = True
                ceIsPublicCopy.Enabled = True
                deSchNxtStartTimeCopy.Enabled = False
                cmbSchRptIntervalCopy.Enabled = False

                btnDeleteCopy.Enabled = True
                grpLayerPropCopy.Enabled = True
            End If

            AddHandler deSchNxtStartTimeCopy.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            AddHandler ceActiveCopy.CheckedChanged, AddressOf ceActive_CheckedChanged
            AddHandler ceIsPublicCopy.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            AddHandler cmbSchRptIntervalCopy.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvConfigSummCopy_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            LoadCellList()
            LoadLayers()

            Dim dr As DataRow = gvConfigSummCopy.GetFocusedDataRow()
            layerPropGridCopy.Tag = Nothing
            layerPropGridCopy.Tag = dr
            LoadLayerProperties(layerPropGridCopy, "NB_Copy", dr)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCloneCopy_Click(sender As Object, e As EventArgs) Handles btnCloneCopy.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCopyCampaigns.GetRow(rIndex(0)).Row
                dlgCampaignClone.campaignID = dr("CampaignID")
                dlgCampaignClone.campaignType = dr("CampaignType").ToString
                If dlgCampaignClone.ShowDialog() = DialogResult.OK Then
                    FillNBCopyCampaigns()
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

    Private Sub btnDeleteCopy_Click(sender As Object, e As EventArgs) Handles btnDeleteCopy.Click
        Try
            Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCopyCampaigns.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    DeleteCampaign(dr("CampaignID"), dr("CampaignType").ToString)
                    gvCopyCampaigns.DeleteRow(rIndex(0))
                    If gvCopyCampaigns.RowCount > 0 Then
                        gvCopyCampaigns.ClearSelection()
                        gvCopyCampaigns.SelectRow(0)
                        gvCopyCampaigns.FocusedRowHandle = 0
                        gvCopyCampaigns_FocusedRowChanged(Nothing, Nothing)
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

    Private Sub btnRunNowCopy_Click(sender As Object, e As EventArgs) Handles btnRunNowCopy.Click
        Try
            Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCopyCampaigns.GetRow(rIndex(0)).Row
                Dim campaignID As Integer = dr("CampaignID")
                Dim campaignType As String = dr("CampaignType").ToString

                If btnRunNowCopy.Text = "Abort Run!" Then
                    objThreadCopy.Abort()
                Else
                    btnRunNowCopy.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowCopy.Text = "Abort Run!"
                    dr("LastStatus") = "Running"
                    gcCopyCampaigns.Refresh()
                    Application.DoEvents()

                    Dim objRunCopy As New RunNowClass()
                    objRunCopy.campaignID = campaignID
                    objRunCopy.campaignType = campaignType
                    objRunCopy.Status = 1
                    objRunCopy.CampaignRow = dr
                    AddHandler objRunCopy.ThreadComplete, AddressOf ExecuteAfterCopyThreadComplete
                    objThreadCopy = New System.Threading.Thread(AddressOf objRunCopy.RunNow)
                    objThreadCopy.Start()

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfterCopyThreadComplete(row As DataRow, Status As Integer, ti As Threading.Thread)
        SyncLock objCopyThreadLock
            LoadCampaigns()
            Dim arg() As Object = {row, Status}
            Me.BeginInvoke(New CallThreadInvokedCopy(AddressOf SetCopyCampaignLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetCopyCampaignLastStatus(Row As DataRow, Status As Integer)
        SyncLock objCopyThreadLock
            If Row IsNot Nothing Then
                If Status = 0 Then
                    Row("LastStatus") = "Idle"
                ElseIf Status = 1 Then
                    Row("LastStatus") = "Running"
                ElseIf Status = -1 Then
                    Row("LastStatus") = "Error"
                End If
                gcCopyCampaigns.Refresh()
                btnRunNowCopy.LookAndFeel.UseDefaultLookAndFeel = True
                btnRunNowCopy.Text = "Run Now"

                Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dr As DataRow = gvCopyCampaigns.GetRow(rIndex(0)).Row
                    If Row("CampaignID") = dr("CampaignID") Then
                        LoadResultSetCombo(cmbCopyResultSetID, Row("CampaignID"), Row("CampaignType"))
                    End If
                End If
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub txtSearchCopy_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchCopy.KeyUp
        Try
            Dim dtCopyCamp As DataTable = CType(gcCopyCampaigns.DataSource, DataTable)
            If dtCopyCamp IsNot Nothing Then
                If (txtSearchCopy.Text.Length > 0) Then
                    dtCopyCamp.DefaultView.RowFilter = "[CampaignName] Like '%" & txtSearchCopy.Text.Trim & "%' Or Convert([CampaignID],'System.String') Like '%" & txtSearchCopy.Text.Trim & "%'"
                Else
                    dtCopyCamp.DefaultView.RowFilter = ""
                End If
            End If
            gvCopyCampaigns_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbCopyResultSetID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCopyResultSetID.SelectedIndexChanged
        If cmbCopyResultSetID.SelectedIndex > 0 Then
            LoadResultSummaryGrid(gcCampSummCopy, cmbCopyResultSetID.SelectedText.Trim(), cmbCopyResultSetID.Tag)
        Else
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcCampSummCopy)
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcCampDataCopy)
        End If
        lblCopyDataRowCount.Visible = False
    End Sub

    Private Sub btnCopyDataLoadGrid_Click(sender As Object, e As EventArgs) Handles btnCopyDataLoadGrid.Click
        If cmbCopyResultSetID.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Loading...")
                Dim parray()() As String = {
                                                New String() {"@ResultSetID", Chr(39) & cmbCopyResultSetID.SelectedItem.ToString & Chr(39)},
                                                New String() {"@CampaignType", Chr(39) & cmbCopyResultSetID.Tag & Chr(39)}
                                            }
                Dim strConnection As String = GetSQL(4538, parray)(0)
                Dim sqlParam As String = GetSQL(4538, parray)(1)
                Dim dtCopyDataGrid As New DataTable

                dtCopyDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCampDataCopy, gvCampDataCopy, dtCopyDataGrid, "ALL")
                lblCopyDataRowCount.Text = "Count of Records: " & gvCampDataCopy.RowCount
                lblCopyDataRowCount.Visible = True
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Copy Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbCopyResultSetID.Focus()
        End If
    End Sub

    Private Sub btnCopyDataAllCsv_Click(sender As Object, e As EventArgs) Handles btnCopyDataAllCsv.Click
        If cmbCopyResultSetID.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Writing data to CSV...")
                Dim parray()() As String = {
                                                New String() {"@ResultSetID", Chr(39) & cmbCopyResultSetID.SelectedItem.ToString & Chr(39)},
                                                New String() {"@CampaignType", Chr(39) & cmbCopyResultSetID.Tag & Chr(39)}
                                            }
                Dim strConnection As String = GetSQL(4539, parray)(0)
                Dim sqlParam As String = GetSQL(4539, parray)(1)
                Dim dtCopyDataGrid As New DataTable

                dtCopyDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

                Dim objFileDlg As New SaveFileDialog()
                objFileDlg.InitialDirectory = "C:\"
                objFileDlg.Filter = "Comma Delimited|*.csv"
                objFileDlg.Title = "Save a CSV File"
                objFileDlg.FileName = gvCopyCampaigns.GetRowCellValue(gvCopyCampaigns.GetSelectedRows()(0), "CampaignName") & "_" & cmbCopyResultSetID.SelectedItem.ToString
                If objFileDlg.ShowDialog() = DialogResult.OK Then
                    If objFileDlg.FileName <> "" Then
                        Dim Content() As Byte = CSVBytesWriter(dtCopyDataGrid)
                        Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                        fs.Write(Content, 0, Content.Length)
                        fs.Close()
                    End If
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Copy Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbCopyResultSetID.Focus()
        End If
    End Sub

#End Region

#Region "NB Delete"

    Private Sub FillNBDeleteCampaigns()
        Try
            LoadCampaigns()

            Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "LastRunTime", "LastEndTime", "CampaignType", "IsPublic"}
            Dim dtDeleteCamp As New DataTable
            dtDeleteCamp = dtNBCampaigns.AsEnumerable().Where(Function(x) x.Field(Of String)("CampaignType") = "NB_Delete").CopyToDataTable()
            'dtDeleteCamp.Columns(0).ColumnName = "ID"

            Dim rIndex() As Integer = gvDeleteCampaigns.GetSelectedRows()

            RemoveHandler gvDeleteCampaigns.FocusedRowChanged, AddressOf gvDeleteCampaigns_FocusedRowChanged
            RemoveHandler gvDeleteCampaigns.RowClick, AddressOf gvDeleteCampaigns_RowClick
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcDeleteCampaigns, gvDeleteCampaigns, dtDeleteCamp, "ALL", columnsToHide, "CampaignName")
            gvDeleteCampaigns.Columns(0).Caption = "ID"
            gvDeleteCampaigns.Columns(0).BestFit()
            AddHandler gvDeleteCampaigns.FocusedRowChanged, AddressOf gvDeleteCampaigns_FocusedRowChanged
            AddHandler gvDeleteCampaigns.RowClick, AddressOf gvDeleteCampaigns_RowClick

            If gvDeleteCampaigns.RowCount > 0 Then
                gvDeleteCampaigns.ClearSelection()
                If rIndex.Length > 0 Then
                    gvDeleteCampaigns.SelectRow(rIndex(0))
                    gvDeleteCampaigns.FocusedRowHandle = rIndex(0)
                Else
                    gvDeleteCampaigns.SelectRow(0)
                    gvDeleteCampaigns.FocusedRowHandle = 0
                End If
                gvDeleteCampaigns_FocusedRowChanged(Nothing, Nothing)
            End If
        Catch
        End Try
    End Sub

    Private Sub gvDeleteCampaigns_RowClick(sender As Object, e As RowClickEventArgs)
        gvDeleteCampaigns_FocusedRowChanged(gvDeleteCampaigns, Nothing)
    End Sub

    Private Sub gvDeleteCampaigns_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvDeleteCampaigns.RowCount > 0 AndAlso e IsNot Nothing Then
                gvDeleteCampaigns.ClearSelection()
                gvDeleteCampaigns.FocusedRowHandle = e.FocusedRowHandle
                gvDeleteCampaigns.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler deSchNxtStartTimeDelete.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            RemoveHandler ceActiveCopy.CheckedChanged, AddressOf ceActive_CheckedChanged
            RemoveHandler ceIsPublicCopy.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            RemoveHandler cmbSchRptIntervalCopy.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged


            Dim dr As DataRow = gvDeleteCampaigns.GetFocusedDataRow()
            If dr IsNot Nothing Then

                lblOwnerDelete.Text = dr("CampaignOwner")
                lblLastRunTimeDelete.Text = dr("LastRunTime")
                lblLastEndTimeDelete.Text = dr("LastEndTime")

                If dr("LastStatus").ToString = "Running" Then
                    btnRunNowDelete.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowDelete.Text = "Abort Run!"
                Else
                    btnRunNowDelete.LookAndFeel.UseDefaultLookAndFeel = True
                    btnRunNowDelete.Text = "Run Now"
                End If

                Dim drCampaignDetail As DataRow = GetCampaignDetailsByID(dr("CampaignID"))
                If drCampaignDetail IsNot Nothing Then
                    ceActiveDelete.Checked = IIf(IsDBNull(drCampaignDetail("CampaignEnabled")), False, drCampaignDetail("CampaignEnabled"))
                    ceIsPublicDelete.Checked = IIf(IsDBNull(drCampaignDetail("IsPublic")), False, drCampaignDetail("IsPublic"))
                    deSchNxtStartTimeDelete.EditValue = drCampaignDetail("ScheduleNextStartDate")
                    cmbSchRptIntervalDelete.SelectedItem = drCampaignDetail("ScheduleRepeatInterval")
                End If

                'Load campaign configuration summary grid
                LoadConfigSummaryGrid(dr("CampaignID"), dr("CampaignType"))
                LoadResultSetCombo(cmbCopyResultSetID, dr("CampaignID"), dr("CampaignType"))

            End If

            lblCopyDataRowCount.Visible = False
            'Enable/disable control if the current user is not the owner of the campaign.
            If lblOwnerCopy.Text.ToLower <> Environment.UserName.ToLower Then
                lblOwnerCopy.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblOwnerCopy.ForeColor = Color.Red
                ceIsPublicCopy.Enabled = False

                If ceIsPublicCopy.Checked Then
                    ceActiveCopy.Enabled = True
                    deSchNxtStartTimeCopy.Enabled = False
                    cmbSchRptIntervalCopy.Enabled = False

                    btnDeleteCopy.Enabled = True
                    grpLayerPropCopy.Enabled = True
                Else
                    ceActiveCopy.Enabled = False
                    deSchNxtStartTimeCopy.Enabled = False
                    cmbSchRptIntervalCopy.Enabled = False

                    btnDeleteCopy.Enabled = False
                    grpLayerPropCopy.Enabled = False
                End If
            Else
                lblOwnerCopy.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblOwnerCopy.ForeColor = Color.Black

                ceActiveCopy.Enabled = True
                ceIsPublicCopy.Enabled = True
                deSchNxtStartTimeCopy.Enabled = False
                cmbSchRptIntervalCopy.Enabled = False

                btnDeleteCopy.Enabled = True
                grpLayerPropCopy.Enabled = True
            End If

            AddHandler deSchNxtStartTimeCopy.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            AddHandler ceActiveCopy.CheckedChanged, AddressOf ceActive_CheckedChanged
            AddHandler ceIsPublicCopy.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            AddHandler cmbSchRptIntervalCopy.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvConfigSummDelete_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            LoadCellList()
            LoadLayers()

            Dim dr As DataRow = gvConfigSummDelete.GetFocusedDataRow()
            layerPropGridDelete.Tag = Nothing
            layerPropGridDelete.Tag = dr
            LoadLayerProperties(layerPropGridDelete, "NB_Delete", dr)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCloneDelete_Click(sender As Object, e As EventArgs) Handles btnCloneDelete.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim rIndex() As Integer = gvDeleteCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvDeleteCampaigns.GetRow(rIndex(0)).Row
                dlgCampaignClone.campaignID = dr("CampaignID")
                dlgCampaignClone.campaignType = dr("CampaignType").ToString
                If dlgCampaignClone.ShowDialog() = DialogResult.OK Then
                    FillNBDeleteCampaigns()
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

    Private Sub btnDeleteDelete_Click(sender As Object, e As EventArgs) Handles btnDeleteDelete.Click
        Try
            Dim rIndex() As Integer = gvDeleteCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvDeleteCampaigns.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    DeleteCampaign(dr("CampaignID"), dr("CampaignType").ToString)
                    gvDeleteCampaigns.DeleteRow(rIndex(0))
                    If gvDeleteCampaigns.RowCount > 0 Then
                        gvDeleteCampaigns.ClearSelection()
                        gvDeleteCampaigns.SelectRow(0)
                        gvDeleteCampaigns.FocusedRowHandle = 0
                        gvDeleteCampaigns_FocusedRowChanged(Nothing, Nothing)
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

    Private Sub btnRunNowDelete_Click(sender As Object, e As EventArgs) Handles btnRunNowDelete.Click
        Try
            Dim rIndex() As Integer = gvDeleteCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvDeleteCampaigns.GetRow(rIndex(0)).Row
                Dim campaignID As Integer = dr("CampaignID")
                Dim campaignType As String = dr("CampaignType").ToString

                If btnRunNowDelete.Text = "Abort Run!" Then
                    objThreadDelete.Abort()
                Else
                    btnRunNowDelete.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowDelete.Text = "Abort Run!"
                    dr("LastStatus") = "Running"
                    gcDeleteCampaigns.Refresh()
                    Application.DoEvents()

                    Dim objRunDelete As New RunNowClass()
                    objRunDelete.campaignID = campaignID
                    objRunDelete.campaignType = campaignType
                    objRunDelete.Status = 1
                    objRunDelete.CampaignRow = dr
                    AddHandler objRunDelete.ThreadComplete, AddressOf ExecuteAfterDeleteThreadComplete
                    objThreadCopy = New System.Threading.Thread(AddressOf objRunDelete.RunNow)
                    objThreadCopy.Start()

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfterDeleteThreadComplete(row As DataRow, Status As Integer, ti As Threading.Thread)
        SyncLock objDeleteThreadLock
            LoadCampaigns()
            Dim arg() As Object = {row, Status}
            Me.BeginInvoke(New CallThreadInvokedCopy(AddressOf SetDeleteCampaignLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetDeleteCampaignLastStatus(Row As DataRow, Status As Integer)
        SyncLock objDeleteThreadLock
            If Row IsNot Nothing Then
                If Status = 0 Then
                    Row("LastStatus") = "Idle"
                ElseIf Status = 1 Then
                    Row("LastStatus") = "Running"
                ElseIf Status = -1 Then
                    Row("LastStatus") = "Error"
                End If
                gcDeleteCampaigns.Refresh()
                btnRunNowDelete.LookAndFeel.UseDefaultLookAndFeel = True
                btnRunNowDelete.Text = "Run Now"

                Dim rIndex() As Integer = gvDeleteCampaigns.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dr As DataRow = gvDeleteCampaigns.GetRow(rIndex(0)).Row
                    If Row("CampaignID") = dr("CampaignID") Then
                        LoadResultSetCombo(cmbResultSetIDDelete, Row("CampaignID"), Row("CampaignType"))
                    End If
                End If
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub txtSearchDelete_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchDelete.KeyUp
        Try
            Dim dtDeleteCamp As DataTable = CType(gcDeleteCampaigns.DataSource, DataTable)
            If dtDeleteCamp IsNot Nothing Then
                If (txtSearchDelete.Text.Length > 0) Then
                    dtDeleteCamp.DefaultView.RowFilter = "[CampaignName] Like '%" & txtSearchDelete.Text.Trim & "%' Or Convert([CampaignID],'System.String') Like '%" & txtSearchDelete.Text.Trim & "%'"
                Else
                    dtDeleteCamp.DefaultView.RowFilter = ""
                End If
            End If
            gvDeleteCampaigns_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbResultSetIDDelete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResultSetIDDelete.SelectedIndexChanged
        If cmbResultSetIDDelete.SelectedIndex > 0 Then
            LoadResultSummaryGrid(gcCampSummDelete, cmbResultSetIDDelete.SelectedText.Trim(), cmbResultSetIDDelete.Tag)
        Else
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcCampSummDelete)
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcCampDataDelete)
        End If
        lblCopyDataRowCount.Visible = False
    End Sub

    Private Sub btnDataLoadGridDelete_Click(sender As Object, e As EventArgs) Handles btnDataLoadGridDelete.Click
        If cmbResultSetIDDelete.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Loading...")
                Dim parray()() As String = {
                    New String() {"@ResultSetID", Chr(39) & cmbResultSetIDDelete.SelectedItem.ToString & Chr(39)},
                    New String() {"@CampaignType", Chr(39) & cmbResultSetIDDelete.Tag & Chr(39)}
                }
                Dim strConnection As String = GetSQL(4538, parray)(0)
                Dim sqlParam As String = GetSQL(4538, parray)(1)
                Dim dtDeleteDataGrid As New DataTable

                dtDeleteDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCampDataDelete, gvCampDataDelete, dtDeleteDataGrid, "ALL")
                lblDataRowCountDelete.Text = "Count of Records: " & gvCampDataDelete.RowCount
                lblDataRowCountDelete.Visible = True
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Delete Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbCopyResultSetID.Focus()
        End If
    End Sub

    Private Sub btnDataAllCsvDelete_Click(sender As Object, e As EventArgs) Handles btnDataAllCsvDelete.Click
        If cmbResultSetIDDelete.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Writing data to CSV...")
                Dim parray()() As String = {
                    New String() {"@ResultSetID", Chr(39) & cmbResultSetIDDelete.SelectedItem.ToString & Chr(39)},
                    New String() {"@CampaignType", Chr(39) & cmbResultSetIDDelete.Tag & Chr(39)}
                }
                Dim strConnection As String = GetSQL(4539, parray)(0)
                Dim sqlParam As String = GetSQL(4539, parray)(1)
                Dim dtDeleteDataGrid As New DataTable

                dtDeleteDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

                Dim objFileDlg As New SaveFileDialog()
                objFileDlg.InitialDirectory = "C:\"
                objFileDlg.Filter = "Comma Delimited|*.csv"
                objFileDlg.Title = "Save a CSV File"
                objFileDlg.FileName = gvDeleteCampaigns.GetRowCellValue(gvDeleteCampaigns.GetSelectedRows()(0), "CampaignName") & "_" & cmbResultSetIDDelete.SelectedItem.ToString
                If objFileDlg.ShowDialog() = DialogResult.OK Then
                    If objFileDlg.FileName <> "" Then
                        Dim Content() As Byte = CSVBytesWriter(dtDeleteDataGrid)
                        Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                        fs.Write(Content, 0, Content.Length)
                        fs.Close()
                    End If
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Delete Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbCopyResultSetID.Focus()
        End If
    End Sub

#End Region

#Region "NB Manual"

    Private Sub LoadManualCampaigns()
        Try
            LoadCampaigns()

            Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "LastRunTime", "LastEndTime", "CampaignType", "LastStatus", "IsPublic"}
            Dim dtManualCamp As New DataTable
            dtManualCamp = dtNBCampaigns.AsEnumerable().Where(Function(x) x.Field(Of String)("CampaignType") = "Manual").CopyToDataTable()
            'dtManualCamp.Columns(0).ColumnName = "ID"

            Dim rIndex() As Integer = gvCampaignManual.GetSelectedRows()

            RemoveHandler gvCampaignManual.FocusedRowChanged, AddressOf gvCampaignManual_FocusedRowChanged
            RemoveHandler gvCampaignManual.RowClick, AddressOf gvCampaignManual_RowClick
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCampaignManual, gvCampaignManual, dtManualCamp, "ALL", columnsToHide, "CampaignName")
            gvCampaignManual.Columns(0).Caption = "ID"
            gvCampaignManual.Columns(0).BestFit()

            If gvCampaignManual.RowCount > 0 Then
                gvCampaignManual.ClearSelection()
                If rIndex.Length > 0 Then
                    gvCampaignManual.SelectRow(rIndex(0))
                    gvCampaignManual.FocusedRowHandle = rIndex(0)
                Else
                    gvCampaignManual.SelectRow(0)
                    gvCampaignManual.FocusedRowHandle = 0
                End If
                If dlgAddCampaign.newManualCampaignAdded IsNot Nothing Then
                    gvCampaignManual.FocusedRowHandle = gvCampaignManual.LocateByValue("CampaignName", dlgAddCampaign.newManualCampaignAdded)
                End If
                AddHandler gvCampaignManual.FocusedRowChanged, AddressOf gvCampaignManual_FocusedRowChanged
                AddHandler gvCampaignManual.RowClick, AddressOf gvCampaignManual_RowClick
                gvCampaignManual_FocusedRowChanged(Nothing, Nothing)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvCampaignManual_RowClick(sender As Object, e As RowClickEventArgs)
        gvCampaignManual_FocusedRowChanged(gvCampaignManual, Nothing)
    End Sub

    Private Sub gvCampaignManual_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvCampaignManual.RowCount > 0 AndAlso e IsNot Nothing Then
                gvCampaignManual.ClearSelection()
                gvCampaignManual.FocusedRowHandle = e.FocusedRowHandle
                gvCampaignManual.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler ceIsPublicManual.CheckedChanged, AddressOf ceIsPublic_CheckedChanged

            Dim dr As DataRow = gvCampaignManual.GetFocusedDataRow()
            If dr IsNot Nothing Then
                lblOwnerManual.Text = dr("CampaignOwner")

                Dim drManual As DataRow = GetCampaignDetailsByID(dr("CampaignID"))
                If drManual IsNot Nothing Then
                    ceIsPublicManual.Checked = IIf(IsDBNull(drManual.Item("IsPublic")), False, drManual.Item("IsPublic"))
                End If

                'Load campaign configuration summary grid
                LoadConfigSummaryGrid(dr("CampaignID"), dr("CampaignType"))
                lblRecordsCountManual.Text = "Count of records: " & gvManual.RowCount.ToString
                LoadResultSetCombo(cmbManualResultSetID, dr("CampaignID"), dr("CampaignType"))
            End If

            'Enable/disable control if the current user is not the owner of the campaign.
            If lblOwnerManual.Text.ToLower <> Environment.UserName.ToLower Then
                lblOwnerManual.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblOwnerManual.ForeColor = Color.Red
                ceIsPublicManual.Enabled = False

                If ceIsPublicManual.Checked Then
                    btnCommitManual.Enabled = True
                    gvManual.OptionsBehavior.Editable = True
                    gvManual.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True
                    gvManual.OptionsBehavior.ReadOnly = False

                    grpCampPropManual.Enabled = True
                    btnDeleteManual.Enabled = True
                Else
                    btnCommitManual.Enabled = False
                    gvManual.OptionsBehavior.Editable = False
                    gvManual.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False
                    gvManual.OptionsBehavior.ReadOnly = True

                    grpCampPropManual.Enabled = False
                    btnDeleteManual.Enabled = False
                End If
            Else
                lblOwnerManual.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblOwnerManual.ForeColor = Color.Black

                ceIsPublicManual.Enabled = True
                btnCommitManual.Enabled = True
                gvManual.OptionsBehavior.Editable = True

                If gvManual.Columns.Count > 0 Then
                    gvManual.Columns(0).OptionsColumn.AllowEdit = False
                    gvManual.Columns(1).OptionsColumn.AllowEdit = False
                    gvManual.Columns(2).OptionsColumn.AllowEdit = False
                    gvManual.Columns(3).OptionsColumn.AllowEdit = False
                    gvManual.Columns(4).OptionsColumn.AllowEdit = False
                End If

                gvManual.OptionsBehavior.ReadOnly = False
                grpCampPropManual.Enabled = True
                btnDeleteManual.Enabled = True

                AddHandler ceIsPublicManual.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvManual_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) 'Handles gvManual.CellValueChanged
        Try
            If e.Column.FieldName = "HighPrioNB" Or e.Column.FieldName = "ReverseFlag" Or e.Column.FieldName = "DeleteFlag" Then
                If e.Value <> 0 And e.Value <> 1 Then
                    XtraMessageBox.Show(e.Column.FieldName & " accept only 0 or 1.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                    If e.Value > 1 Then
                        gv.SetRowCellValue(e.RowHandle, e.Column, 1)
                    ElseIf e.Value < 0 Then
                        gv.SetRowCellValue(e.RowHandle, e.Column, 0)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gcManual_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles gcManual.KeyDown
        Try
            If (e.KeyCode = Keys.Delete) Then
                Dim grid As GridControl = CType(sender, GridControl)
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView = DirectCast(grid.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If lblOwnerManual.Text.ToLower = Environment.UserName.ToLower Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    If (XtraMessageBox.Show("Do you want to delete rows?", "Confirmation", MessageBoxButtons.YesNo) <> DialogResult.Yes) Then Return
                    'Dim dr As DataRow = grid.MainView.GetRow(view.FocusedRowHandle).Row
                    Dim rIndex() As Integer = gvManual.GetSelectedRows()
                    For i As Integer = rIndex.Length - 1 To 0 Step -1
                        Dim parray()() As String = {
                            New String() {"@campaignID", gvManual.GetRowCellValue(rIndex(i), "CampaignID")},
                            New String() {"@sCellName", Chr(39) & gvManual.GetRowCellValue(rIndex(i), "S_CELLNAME") & Chr(39)},
                            New String() {"@tCellName", Chr(39) & gvManual.GetRowCellValue(rIndex(i), "T_CELLNAME") & Chr(39)}
                        }
                        Dim strConnection As String = GetSQL(4550, parray)(0)
                        Dim sqlParam As String = GetSQL(4550, parray)(1)
                        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                        Application.DoEvents()
                        view.DeleteRow(view.FocusedRowHandle)
                    Next
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub gvManual_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Data.RowDeletingEventArgs) Handles gvManual.RowDeleting
        Try
            If lblOwnerManual.Text.ToLower <> Environment.UserName.ToLower Then
                e.Cancel = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvManual_RowDeleted(ByVal sender As Object, ByVal e As DevExpress.Data.RowDeletedEventArgs) Handles gvManual.RowDeleted
        Try
            lblRecordsCountManual.Text = "Count of Records: " & gvManual.RowCount.ToString
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmManualPaste_Opening(sender As Object, e As CancelEventArgs) Handles cmManualPaste.Opening
        Try
            If lblOwnerManual.Text.ToLower <> Environment.UserName.ToLower Then
                tsmiTagPastePaste.Enabled = False
            Else
                tsmiTagPastePaste.Enabled = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnCloneManual_Click(sender As Object, e As EventArgs) Handles btnCloneManual.Click
        Try

            Dim rIndex() As Integer = gvCampaignManual.GetSelectedRows()

            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignManual.GetRow(rIndex(0)).Row
                dlgCampaignClone.campaignID = dr("CampaignID")
                dlgCampaignClone.campaignType = dr("CampaignType").ToString
                If dlgCampaignClone.ShowDialog() = DialogResult.OK Then
                    LoadManualCampaigns()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAddManual_Click(sender As Object, e As EventArgs) Handles btnAddManual.Click
        Try
            dlgAddCampaign.CampaignType = "Manual"
            dlgAddCampaign.newManualCampaignAdded = Nothing
            If ceIsPublicManual.Checked Then
                dlgAddCampaign.IsPublic = True
            Else
                dlgAddCampaign.IsPublic = False
            End If
            If dlgAddCampaign.ShowDialog() = DialogResult.OK Then
                LoadManualCampaigns()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteManual_Click(sender As Object, e As EventArgs) Handles btnDeleteManual.Click
        Try
            Dim campaignID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex() As Integer = gvCampaignManual.GetSelectedRows()

            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignManual.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Manual Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    campaignID = dr("CampaignID")
                    campaignType = dr("CampaignType").ToString
                    DeleteCampaign(campaignID, campaignType)
                    RemoveHandler gvCampaignManual.FocusedRowChanged, AddressOf gvCampaignManual_FocusedRowChanged
                    gvCampaignManual.DeleteRow(rIndex(0))
                    If gvCampaignManual.RowCount > 0 Then
                        gvCampaignManual.ClearSelection()
                        gvCampaignManual.SelectRow(0)
                        gvCampaignManual.FocusedRowHandle = 0
                        AddHandler gvCampaignManual.FocusedRowChanged, AddressOf gvCampaignManual_FocusedRowChanged
                        gvCampaignManual_FocusedRowChanged(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearchManual_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchManual.KeyUp
        Try
            Dim dtManualCamp As DataTable = CType(gcCampaignManual.DataSource, DataTable)
            If dtManualCamp IsNot Nothing Then
                If (txtSearchManual.Text.Length > 0) Then
                    dtManualCamp.DefaultView.RowFilter = "[CampaignName] Like '%" & txtSearchManual.Text.Trim & "%' Or Convert([CampaignID],'System.String') Like '%" & txtSearchManual.Text.Trim & "%'"
                Else
                    dtManualCamp.DefaultView.RowFilter = ""
                End If
            End If
            gvCampaignManual_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiTagPastePaste_Click(sender As Object, e As EventArgs) Handles tsmiTagPastePaste.Click
        Try
            RemoveHandler gvManual.CellValueChanged, AddressOf gvManual_CellValueChanged
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            IsErrorInCopy = False
            gvManual.PasteFromClipboard()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            AddHandler gvManual.CellValueChanged, AddressOf gvManual_CellValueChanged
        End Try
    End Sub

    Private Sub gvManual_ClipboardRowPasting(sender As Object, e As ClipboardRowPastingEventArgs) Handles gvManual.ClipboardRowPasting
        Try
            If IsErrorInCopy = True Or lblOwnerManual.Text.ToLower <> Environment.UserName.ToLower Then
                e.Cancel = True
                Clipboard.Clear()
                Exit Sub
            End If

            Dim view As GridView = TryCast(sender, GridView)
            If e.OriginalValues.Count > 0 Then
                Dim rIndex() As Integer = gvCampaignManual.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dt As DataTable = Nothing
                    dt = gcManual.DataSource
                    Dim drCamp As DataRow = gvCampaignManual.GetRow(rIndex(0)).Row
                    If e.OriginalValues.Count = 7 Then
                        Dim drData As DataRow
                        drData = dt.NewRow()
                        drData(0) = drCamp.Item("CampaignID")
                        drData(1) = e.OriginalValues(0).ToString().Trim()
                        drData(2) = e.OriginalValues(1).ToString().Trim()
                        drData(3) = e.OriginalValues(2).ToString().Trim()
                        drData(4) = e.OriginalValues(3).ToString().Trim()
                        drData(5) = e.OriginalValues(4).ToString().Trim()
                        drData(6) = e.OriginalValues(5).ToString().Trim()
                        drData(7) = e.OriginalValues(6).ToString().Trim()
                        dt.Rows.Add(drData)
                        lblRecordsCountManual.Text = "Count of records: " & dt.Rows.Count.ToString
                    ElseIf e.OriginalValues(0).ToString() <> "" Then
                        XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<S_CELLNAME>,<S_IOS_TECH>,<T_CELLNAME>,<T_IOS_TECH>,<DeleteFlag>,<ReverseFlag>,<HighPrioNB>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        e.Cancel = True
                        Clipboard.Clear()
                        IsErrorInCopy = True
                    End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<S_CELLNAME>,<S_IOS_TECH>,<T_CELLNAME>,<T_IOS_TECH>,<DeleteFlag>,<ReverseFlag>,<HighPrioNB>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Clipboard.Clear()
            IsErrorInCopy = True
        Finally
            For iCntr = 0 To gvManual.RowCount - 1
                If IsDBNull(gvManual.GetRowCellValue(iCntr, "CampaignID")) Then
                    gvManual.DeleteRow(iCntr)
                End If
            Next
        End Try
    End Sub

    Private Sub gvManual_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            If e.RowHandle > -1 Then
                Dim dr As DataRowView = gvManual.GetRow(e.RowHandle)
                If dr IsNot Nothing Then
                    If dr.Item(5) <> 0 And dr.Item(5) <> 1 Then
                        e.Appearance.BackColor = Color.Red
                        e.Appearance.ForeColor = Color.Black
                    End If

                    If dr.Item(6) <> 0 And dr.Item(6) <> 1 Then
                        e.Appearance.BackColor = Color.Red
                        e.Appearance.ForeColor = Color.Black
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gcManual_KeyUp(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            Try
                Dim dt As DataTable = gcManual.DataSource
                Dim drList As DataRow = gvManual.GetFocusedDataRow()
                If drList IsNot Nothing Then
                    dt.Rows.Remove(drList)
                End If
                dt.AcceptChanges()
                lblRecordsCountManual.Text = "Count of Records: " & gvManual.RowCount.ToString
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally

            End Try
        End If
    End Sub

    Private Sub btnCommitManual_Click(sender As Object, e As EventArgs) Handles btnCommitManual.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim rIndex() As Integer = gvCampaignManual.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvCampaignManual.GetRow(rIndex(0)).Row
                Dim campaignID As Integer = drow.Item(0)
                Dim campaignType As String = drow.Item(2)

                Dim connArr() As String = GetIOSConnection(1000)
                If connArr.Length > 0 Then
                    Dim dtAddedRecords As DataTable = CType(gcManual.DataSource, DataTable).GetChanges(DataRowState.Added)
                    If dtAddedRecords IsNot Nothing Then
                        InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[NB_Manual_Input]", dtAddedRecords)
                    Else
                        Dim changedRecordsTable As DataTable = CType(gcManual.DataSource, DataTable).GetChanges(DataRowState.Modified)
                        If changedRecordsTable IsNot Nothing Then
                            InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[NB_Manual_Input_Temp]", changedRecordsTable)
                            Dim strConnection As String = Nothing
                            Dim sqlParam As String = Nothing

                            strConnection = GetSQL(4551, Nothing)(0)
                            sqlParam = GetSQL(4551, Nothing)(1)
                            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                        End If
                    End If
                End If
                'Run manual campaign.
                Dim objRunManual As New RunNowClass()
                objRunManual.campaignID = campaignID
                AddHandler objRunManual.ThreadComplete, AddressOf ExecuteAfterNBManualThreadComplete
                Dim ObjThreadManual As New System.Threading.Thread(AddressOf objRunManual.RunManual)
                ObjThreadManual.Start()

                'reload result sets for manual campaign
                gvCampaignManual_FocusedRowChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ExecuteAfterNBManualThreadComplete(row As DataRow, Status As Integer, ti As Threading.Thread)
        SyncLock objNBManualThreadLock
            Dim arg() As Object = {row}
            Me.BeginInvoke(New CallThreadInvokedNBManual(AddressOf LoadResultSetComboOnThreadComplete), arg)
        End SyncLock
    End Sub

    Private Sub LoadResultSetComboOnThreadComplete(dr As DataRow)
        SyncLock objNBManualThreadLock
            Try
                LoadResultSetCombo(cmbManualResultSetID, dr("CampaignID"), dr("CampaignType"))
            Catch ex As Exception
            End Try
        End SyncLock
    End Sub

    Private Sub cmbManualResultSetID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbManualResultSetID.SelectedIndexChanged
        If cmbManualResultSetID.SelectedIndex > 0 Then
            LoadResultSummaryGrid(gcCampSummManual, cmbManualResultSetID.SelectedText.Trim(), cmbManualResultSetID.Tag)
        Else
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcCampSummManual)
        End If
    End Sub

#End Region

#Region "NB Audit"

    Private Sub FillNBAuditCampaigns()
        Try

            LoadCampaigns()

            Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "LastRunTime", "LastEndTime", "CampaignType", "IsPublic"}
            Dim dtAuditCamp As New DataTable
            dtAuditCamp = dtNBCampaigns.AsEnumerable().Where(Function(x) x.Field(Of String)("CampaignType") = "NB_Audit").CopyToDataTable()
            'dtAuditCamp.Columns(0).ColumnName = "ID"

            Dim rIndex() As Integer = gvCampNBAudit.GetSelectedRows()

            RemoveHandler gvCampNBAudit.FocusedRowChanged, AddressOf gvCampNBAudit_FocusedRowChanged
            RemoveHandler gvCampNBAudit.RowClick, AddressOf gvCampNBAudit_RowClick
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCampNBAudit, gvCampNBAudit, dtAuditCamp, "ALL", columnsToHide, "CampaignName")
            gvCampNBAudit.Columns(0).Caption = "ID"
            gvCampNBAudit.Columns(0).BestFit()
            AddHandler gvCampNBAudit.FocusedRowChanged, AddressOf gvCampNBAudit_FocusedRowChanged
            AddHandler gvCampNBAudit.RowClick, AddressOf gvCampNBAudit_RowClick

            If gvCampNBAudit.RowCount > 0 Then
                gvCampNBAudit.ClearSelection()
                If rIndex.Length > 0 Then
                    gvCampNBAudit.SelectRow(rIndex(0))
                    gvCampNBAudit.FocusedRowHandle = rIndex(0)
                Else
                    gvCampNBAudit.SelectRow(0)
                    gvCampNBAudit.FocusedRowHandle = 0
                End If
                If dlgAddCampaign.newAuditCampaignAdded IsNot Nothing Then
                    gvCampNBAudit.FocusedRowHandle = gvCampNBAudit.LocateByValue("CampaignName", dlgAddCampaign.newAuditCampaignAdded)
                End If
                gvCampNBAudit_FocusedRowChanged(Nothing, Nothing)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvCampNBAudit_RowClick(sender As Object, e As RowClickEventArgs)
        gvCampNBAudit_FocusedRowChanged(gvCampNBAudit, Nothing)
    End Sub

    Private Sub gvCampNBAudit_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvCampNBAudit.RowCount > 0 AndAlso e IsNot Nothing Then
                gvCampNBAudit.ClearSelection()
                gvCampNBAudit.FocusedRowHandle = e.FocusedRowHandle
                gvCampNBAudit.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler dtpStartTimeNBAudit.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            RemoveHandler chkActiveNBAudit.CheckedChanged, AddressOf ceActive_CheckedChanged
            RemoveHandler ceIsPublicAudit.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            RemoveHandler cmbRepeatIntervalNBAudit.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged

            Dim dr As DataRow = gvCampNBAudit.GetFocusedDataRow()
            If dr IsNot Nothing Then

                lblOwnerNBAudit.Text = dr("CampaignOwner")
                lblLastRunTimeNBAudit.Text = IIf(IsDBNull(dr("LastRunTime")), "", dr("LastRunTime"))
                lblLastEndTimeNBAudit.Text = IIf(IsDBNull(dr("LastEndTime")), "", dr("LastEndTime"))

                If dr("LastStatus").ToString = "Running" Then
                    btnRunNowNBAudit.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowNBAudit.Text = "Abort Run!"
                Else
                    btnRunNowNBAudit.LookAndFeel.UseDefaultLookAndFeel = True
                    btnRunNowNBAudit.Text = "Run Now"
                End If

                Dim drCampaignDetail As DataRow = GetCampaignDetailsByID(dr("CampaignID"))
                If drCampaignDetail IsNot Nothing Then
                    chkActiveNBAudit.Checked = IIf(IsDBNull(drCampaignDetail("CampaignEnabled")), False, drCampaignDetail("CampaignEnabled"))
                    ceIsPublicAudit.Checked = IIf(IsDBNull(drCampaignDetail("IsPublic")), False, drCampaignDetail("IsPublic"))
                    dtpStartTimeNBAudit.EditValue = IIf(IsDBNull(drCampaignDetail("ScheduleNextStartDate")), Today, drCampaignDetail("ScheduleNextStartDate"))
                    cmbRepeatIntervalNBAudit.SelectedItem = IIf(IsDBNull(drCampaignDetail("ScheduleRepeatInterval")), Nothing, drCampaignDetail("ScheduleRepeatInterval"))
                End If

                'Load campaign configuration summary grid
                LoadConfigSummaryGrid(dr("CampaignID"), dr("CampaignType"))
                LoadResultSetCombo(cmbResultSetIdNBAudit, dr("CampaignID"), dr("CampaignType"))

            End If

            lblRecordCountNBAudit.Visible = False
            'Enable/disable control if the current user is not the owner of the campaign.
            If lblOwnerNBAudit.Text.ToLower <> Environment.UserName.ToLower Then
                lblOwnerNBAudit.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblOwnerNBAudit.ForeColor = Color.Red
                ceIsPublicAudit.Enabled = False

                If ceIsPublicAudit.Checked Then
                    chkActiveNBAudit.Enabled = True
                    dtpStartTimeNBAudit.Enabled = False
                    cmbRepeatIntervalNBAudit.Enabled = False

                    BtnDeleteCampNBAudit.Enabled = True
                    grpConfigSummaryNBAudit.Enabled = True
                Else
                    chkActiveNBAudit.Enabled = False
                    dtpStartTimeNBAudit.Enabled = False
                    cmbRepeatIntervalNBAudit.Enabled = False

                    BtnDeleteCampNBAudit.Enabled = False
                    grpConfigSummaryNBAudit.Enabled = False
                End If
            Else
                lblOwnerNBAudit.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblOwnerNBAudit.ForeColor = Color.Black

                chkActiveNBAudit.Enabled = True
                ceIsPublicAudit.Enabled = True
                dtpStartTimeNBAudit.Enabled = False
                cmbRepeatIntervalNBAudit.Enabled = False

                BtnDeleteCampNBAudit.Enabled = True
                grpConfigSummaryNBAudit.Enabled = True
            End If

            If lblOwnerNBAudit.Text.ToLower = "system" Then
                btnCloneCampNBAudit.Enabled = False
                btnRunNowNBAudit.Enabled = False
                grpConfigSummaryNBAudit.Enabled = False
                grpConfigGen.Enabled = False
                grpOptionalSettings.Enabled = False
            Else
                btnCloneCampNBAudit.Enabled = True
                btnRunNowNBAudit.Enabled = True
                grpConfigSummaryNBAudit.Enabled = True
                grpConfigGen.Enabled = True
                grpOptionalSettings.Enabled = True
            End If

            AddHandler dtpStartTimeNBAudit.EditValueChanged, AddressOf deScheduleNextStartTime_EditValueChanged
            AddHandler chkActiveNBAudit.CheckedChanged, AddressOf ceActive_CheckedChanged
            AddHandler ceIsPublicAudit.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            AddHandler cmbRepeatIntervalNBAudit.SelectedIndexChanged, AddressOf cmbScheduleRepeatInterval_SelectedIndexChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvConfigNBAudit_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            LoadCellList()
            LoadLayers()

            Dim dr As DataRow = gvConfigNBAudit.GetFocusedDataRow()
            grdPropertyNBAudit.Tag = Nothing
            grdPropertyNBAudit.Tag = dr
            LoadLayerProperties(grdPropertyNBAudit, "NB_Audit", dr)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCloneCampNBAudit_Click(sender As Object, e As EventArgs) Handles btnCloneCampNBAudit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim rIndex() As Integer = gvCampNBAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampNBAudit.GetRow(rIndex(0)).Row
                dlgCampaignClone.campaignID = dr("CampaignID")
                dlgCampaignClone.campaignType = dr("CampaignType").ToString
                If dlgCampaignClone.ShowDialog() = DialogResult.OK Then
                    FillNBAuditCampaigns()
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

    Private Sub BtnDeleteCampNBAudit_Click(sender As Object, e As EventArgs) Handles BtnDeleteCampNBAudit.Click
        Try
            Dim rIndex() As Integer = gvCampNBAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampNBAudit.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    DeleteCampaign(dr("CampaignID"), dr("CampaignType").ToString)
                    RemoveHandler gvCampNBAudit.FocusedRowChanged, AddressOf gvCampNBAudit_FocusedRowChanged
                    gvCampNBAudit.DeleteRow(rIndex(0))
                    If gvCampNBAudit.RowCount > 0 Then
                        gvCampNBAudit.ClearSelection()
                        gvCampNBAudit.SelectRow(0)
                        gvCampNBAudit.FocusedRowHandle = 0
                        AddHandler gvCampNBAudit.FocusedRowChanged, AddressOf gvCampNBAudit_FocusedRowChanged
                        gvCampNBAudit_FocusedRowChanged(Nothing, Nothing)
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

    Private Sub btnRunNowNBAudit_Click(sender As Object, e As EventArgs) Handles btnRunNowNBAudit.Click
        Try
            Dim rIndex() As Integer = gvCampNBAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampNBAudit.GetRow(rIndex(0)).Row
                Dim campaignID As Integer = dr("CampaignID")
                Dim campaignType As String = dr("CampaignType").ToString

                If btnRunNowNBAudit.Text = "Abort Run!" Then
                    objThreadAudit.Abort()
                Else
                    btnRunNowNBAudit.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowNBAudit.Text = "Abort Run!"
                    dr("LastStatus") = "Running"
                    gcCampNBAudit.Refresh()
                    Application.DoEvents()

                    Dim objRunNBAudit As New RunNowClass()
                    objRunNBAudit.campaignID = campaignID
                    objRunNBAudit.campaignType = campaignType
                    objRunNBAudit.Status = 1
                    objRunNBAudit.CampaignRow = dr
                    AddHandler objRunNBAudit.ThreadComplete, AddressOf ExecuteAfterNBAuditThreadComplete
                    objThreadAudit = New System.Threading.Thread(AddressOf objRunNBAudit.RunNow)
                    objThreadAudit.Start()

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfterNBAuditThreadComplete(row As DataRow, Status As Integer, ti As Threading.Thread)
        SyncLock objAuditThreadLock
            LoadCampaigns()
            Dim arg() As Object = {row, Status}
            Me.BeginInvoke(New CallThreadInvokedDetect(AddressOf SetAuditCampaignLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetAuditCampaignLastStatus(Row As DataRow, Status As Integer)
        SyncLock objAuditThreadLock
            If Row IsNot Nothing Then
                If Status = 0 Then
                    Row("LastStatus") = "Idle"
                ElseIf Status = 1 Then
                    Row("LastStatus") = "Running"
                ElseIf Status = -1 Then
                    Row("LastStatus") = "Error"
                End If
                gcCampNBAudit.Refresh()
                btnRunNowNBAudit.LookAndFeel.UseDefaultLookAndFeel = True
                btnRunNowNBAudit.Text = "Run Now"
                Dim rIndex() As Integer = gvCampNBAudit.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dr As DataRow = gvCampNBAudit.GetRow(rIndex(0)).Row
                    If Row("CampaignID") = dr("CampaignID") Then
                        LoadResultSetCombo(cmbResultSetIdNBAudit, Row("CampaignID"), Row("CampaignType"))
                    End If
                End If
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub txtCampSearchNBAudit_KeyUp(sender As Object, e As KeyEventArgs) Handles txtNBAuditCampSearch.KeyUp
        Try
            Dim dtAuditCamp As DataTable = CType(gcCampNBAudit.DataSource, DataTable)
            If dtAuditCamp IsNot Nothing Then
                If (txtNBAuditCampSearch.Text.Length > 0) Then
                    dtAuditCamp.DefaultView.RowFilter = "[CampaignName] Like '%" & txtNBAuditCampSearch.Text.Trim & "%' Or Convert([CampaignID],'System.String') Like '%" & txtNBAuditCampSearch.Text.Trim & "%'"
                Else
                    dtAuditCamp.DefaultView.RowFilter = ""
                End If
            End If
            gvCampNBAudit_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbResultSetIdNBAudit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResultSetIdNBAudit.SelectedIndexChanged
        If cmbResultSetIdNBAudit.SelectedIndex > 0 Then
            LoadResultSummaryGrid(gcResultSummaryNBAudit, cmbResultSetIdNBAudit.SelectedText.Trim(), cmbResultSetIdNBAudit.Tag)
        Else
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcResultSummaryNBAudit)
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcResultDataNBAudit)
        End If
        lblRecordCountNBAudit.Visible = False
    End Sub

    Private Sub grdProperty_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles grdPropertyNBAudit.PropertyValueChanged, layerPropGridDetect.PropertyValueChanged, layerPropGridCopy.PropertyValueChanged
        Try
            Dim propGrd As PropertyGrid = CType(s, PropertyGrid)
            Dim gvCamp As DevExpress.XtraGrid.Views.Grid.GridView = Nothing
            Dim gvConfig As DevExpress.XtraGrid.Views.Grid.GridView = Nothing
            Dim ceApplyAll As CheckEdit = Nothing
            Dim campaignType As String = Nothing
            If propGrd.Name.Contains("Detect") Then
                gvCamp = gvDetectCampaigns
                gvConfig = gvConfigSummDetect
                ceApplyAll = ceApplyConfigAllDetect
                campaignType = "NB_Detect"
            ElseIf propGrd.Name.Contains("Copy") Then
                gvCamp = gvCopyCampaigns
                gvConfig = gvConfigSummCopy
                ceApplyAll = ceApplyConfigAllCopy
                campaignType = "NB_Copy"
            ElseIf propGrd.Name.Contains("Audit") Then
                gvCamp = gvCampNBAudit
                gvConfig = gvConfigNBAudit
                ceApplyAll = ceApplyConfigAllAudit
                campaignType = "NB_Audit"
            End If

            Dim changedPropertyItem As GridItem = e.ChangedItem
            If (Not changedPropertyItem Is Nothing) Then
                Dim rIndex2() As Integer = gvCamp.GetSelectedRows()
                If propGrd.Tag IsNot Nothing AndAlso rIndex2.Length > 0 Then
                    Dim drConfig As DataRow = CType(propGrd.Tag, DataRow)
                    Dim drCampaign As DataRow = gvCamp.GetRow(rIndex2(0)).Row

                    Dim fieldName As String = changedPropertyItem.PropertyDescriptor.Name
                    Dim value As Object = Nothing
                    If changedPropertyItem.PropertyDescriptor.PropertyType = GetType(Boolean) Then
                        value = IIf(changedPropertyItem.Value = True, 1, 0)
                    ElseIf changedPropertyItem.PropertyDescriptor.PropertyType = GetType(Integer) Then
                        value = CInt(changedPropertyItem.Value)
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "ExclusionList" Or changedPropertyItem.PropertyDescriptor.Name = "InclusionList" Then
                        If changedPropertyItem.PropertyDescriptor.Name = "InclusionList" Then
                            fieldName = "UseInclusionListID"
                        Else
                            fieldName = "UseExceptionListID"
                        End If

                        Dim drList() As DataRow = Nothing
                        drList = dtCellList.AsEnumerable().Where(Function(x) x.Field(Of String)("ListName") = changedPropertyItem.Value).ToArray()
                        If drList.Length > 0 Then
                            value = drList(0).Item("ListID")
                        Else
                            value = 0
                        End If
                    Else
                        value = changedPropertyItem.Value
                    End If

                    If ceApplyAll.Checked Then
                        ApplyConfigPropertyToCampaign(drCampaign.Item("CampaignID"), fieldName, value, campaignType, , True)
                        For iRow As Integer = 0 To gvConfig.RowCount - 1
                            gvConfig.SetRowCellValue(iRow, gvConfig.Columns(fieldName), value)
                        Next
                    Else
                        ApplyConfigPropertyToCampaign(drCampaign.Item("CampaignID"), fieldName, value, campaignType, drConfig.Item("ConfigID"))
                        drConfig.Item(fieldName) = value
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyConfigPropertyToCampaign(ByVal campaignID As Integer, ByVal propName As String, ByVal propValue As String, ByVal campaignType As String, Optional configID As Integer = 0, Optional updateAll As Boolean = False)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        If updateAll = True Then
            Dim parray()() As String = {
                New String() {"@CampaignID", campaignID},
                New String() {"@PropertyName", Chr(39) & propName & Chr(39)},
                New String() {"@PropertyValue", Chr(39) & propValue & Chr(39)},
                New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
            }
            strConnection = GetSQL(4576, parray)(0)
            sqlParam = GetSQL(4576, parray)(1)
        Else
            Dim parray()() As String = {
                New String() {"@ConfigId", configID},
                New String() {"@CampaignID", campaignID},
                New String() {"@PropertyName", Chr(39) & propName & Chr(39)},
                New String() {"@PropertyValue", Chr(39) & propValue & Chr(39)},
                New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
            }
            strConnection = GetSQL(4531, parray)(0)
            sqlParam = GetSQL(4531, parray)(1)
        End If
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub btnLoadToGridNBAudit_Click(sender As Object, e As EventArgs) Handles btnLoadToGridNBAudit.Click
        If cmbResultSetIdNBAudit.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Loading...")
                Dim parray()() As String = {
                                                New String() {"@ResultSetID", Chr(39) & cmbResultSetIdNBAudit.SelectedItem.ToString & Chr(39)},
                                                New String() {"@CampaignType", Chr(39) & cmbResultSetIdNBAudit.Tag & Chr(39)}
                                            }
                Dim strConnection As String = GetSQL(4538, parray)(0)
                Dim sqlParam As String = GetSQL(4538, parray)(1)
                Dim dtAuditDataGrid As New DataTable

                dtAuditDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcResultDataNBAudit, gvResultDataNBAudit, dtAuditDataGrid, "ALL")
                lblRecordCountNBAudit.Text = "Count of Records: " & gvResultDataNBAudit.RowCount
                lblRecordCountNBAudit.Visible = True
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Detect Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbResultSetIdNBAudit.Focus()
        End If
    End Sub

    Private Sub btnDataToCSVNBAudit_Click(sender As Object, e As EventArgs) Handles btnDataToCSVNBAudit.Click
        If cmbResultSetIdNBAudit.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Writing data to CSV...")
                Dim parray()() As String = {
                                                New String() {"@ResultSetID", Chr(39) & cmbResultSetIdNBAudit.SelectedItem.ToString & Chr(39)},
                                                New String() {"@CampaignType", Chr(39) & cmbResultSetIdNBAudit.Tag & Chr(39)}
                                            }
                Dim strConnection As String = GetSQL(4539, parray)(0)
                Dim sqlParam As String = GetSQL(4539, parray)(1)
                Dim dtAuditDataGrid As New DataTable

                dtAuditDataGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

                Dim objFileDlg As New SaveFileDialog()
                objFileDlg.InitialDirectory = "C:\"
                objFileDlg.Filter = "Comma Delimited|*.csv"
                objFileDlg.Title = "Save a CSV File"
                objFileDlg.FileName = gvCampNBAudit.GetRowCellValue(gvCampNBAudit.GetSelectedRows()(0), "CampaignName") & "_" & cmbResultSetIdNBAudit.SelectedItem.ToString
                If objFileDlg.ShowDialog() = DialogResult.OK Then
                    If objFileDlg.FileName <> "" Then
                        Dim Content() As Byte = CSVBytesWriter(dtAuditDataGrid)
                        Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                        fs.Write(Content, 0, Content.Length)
                        fs.Close()
                    End If
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Detect Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbResultSetIdNBAudit.Focus()
        End If
    End Sub

    Private Sub BtnAddCampNBAudit_Click(sender As Object, e As EventArgs) Handles BtnAddCampNBAudit.Click
        Try
            dlgAddCampaign.CampaignType = "NB_Audit"
            dlgAddCampaign.newAuditCampaignAdded = Nothing
            If ceIsPublicAudit.Checked Then
                dlgAddCampaign.IsPublic = True
            Else
                dlgAddCampaign.IsPublic = False
            End If
            If dlgAddCampaign.ShowDialog() = DialogResult.OK Then
                FillNBAuditCampaigns()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAddConfigNBAudit_Click(sender As Object, e As EventArgs) Handles btnAddConfigNBAudit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim rIndex() As Integer = gvCampNBAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drCampaign As DataRow = gvCampNBAudit.GetRow(rIndex(0)).Row

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@CampaignID", drCampaign.Item("CampaignID")},
                    New String() {"@MMLConfigID", cmbMMLConfigIDNBAudit.SelectedItem.ToString()},
                    New String() {"@IOS_TECH", Chr(39) & cmbTechnologyNBAudit.SelectedItem.ToString() & Chr(39)},
                    New String() {"@S_LAYER", Chr(39) & IIf(cmbSLayerNBAudit.SelectedIndex > 0, cmbSLayerNBAudit.SelectedItem.ToString(), "%") & Chr(39)},
                    New String() {"@T_LAYER", Chr(39) & IIf(cmbTLayerNBAudit.SelectedIndex > 0, cmbTLayerNBAudit.SelectedItem.ToString(), "%") & Chr(39)},
                    New String() {"@UseInclusionListID", CType(cmbInclusionListNBAudit.SelectedItem, IOS.Library.clsComboBoxItem).Value},
                    New String() {"@UseExceptionListID", IIf(cmbExclusionListNBAudit.SelectedIndex > 0, CType(cmbExclusionListNBAudit.SelectedItem, IOS.Library.clsComboBoxItem).Value, 0)},
                    New String() {"@Enable", 1},
                    New String() {"@NBType", IIf(cmbNBType.SelectedIndex > 0, Chr(39) & cmbNBType.SelectedItem.ToString() & Chr(39), "NULL")},
                    New String() {"@MMLScriptID", IIf(cmbMMLScriptID.SelectedIndex > 0, cmbMMLScriptID.SelectedItem.ToString(), "NULL")}
                }

                strConnection = GetSQL(4564, parray)(0)
                sqlParam = GetSQL(4564, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                LoadConfigSummaryGrid(drCampaign.Item("CampaignID"), drCampaign.Item("CampaignType"))

                cmbMMLConfigIDNBAudit.SelectedIndex = 0
                cmbTechnologyNBAudit.SelectedIndex = 0
                cmbInclusionListNBAudit.SelectedIndex = 0
                cmbExclusionListNBAudit.SelectedIndex = 0
                cmbSLayerNBAudit.SelectedIndex = 0
                cmbTLayerNBAudit.SelectedIndex = 0

            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbMMLConfigIDNBAudit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMMLConfigIDNBAudit.SelectedIndexChanged, cmbTechnologyNBAudit.SelectedIndexChanged, cmbInclusionListNBAudit.SelectedIndexChanged
        Try
            btnAddConfigNBAudit.Enabled = False
            If cmbMMLConfigIDNBAudit.SelectedIndex > 0 AndAlso cmbTechnologyNBAudit.SelectedIndex > 0 AndAlso cmbInclusionListNBAudit.SelectedIndex > 0 Then
                btnAddConfigNBAudit.Enabled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "NB Fetch"

    Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            If cmbTechnology.SelectedIndex > 0 Then
                Dim strConnection As String, sqlParam As String
                If cmbTechnology.SelectedItem.ToString.ToUpper = "ALL" Then
                    cmbObjectType.Properties.Items.Clear()
                    cmbObjectType.Properties.Items.Add("LOCID")
                    cmbObjectType.SelectedIndex = 0
                Else
                    dtObjectTypes = New DataTable
                    Dim parray()() As String = {New String() {"@tech", Chr(39) & cmbTechnology.SelectedItem.ToString().ToUpper() & Chr(39)}}
                    strConnection = GetSQL(4528, parray)(0)
                    sqlParam = GetSQL(4528, parray)(1)
                    dtObjectTypes = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                    BindDevExComboBoxWithValueMember(cmbObjectType, dtObjectTypes, "Object", "Object", "PLMN", False)
                End If
            Else
                cmbObjectType.Properties.Items.Clear()
                cmbObjectType.SelectedText = ""
                dtObjectTypes = Nothing
                dsObjects = Nothing
                dsObjects = New DataSet
                tvObjectsTree.Nodes.Clear()

                gcNBFetch.DataSource = Nothing
                gvNBFetch.Columns.Clear()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbObjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbObjectType.SelectedIndexChanged
        Try
            If cmbObjectType.SelectedIndex > -1 Then
                If cmbObjectType.SelectedItem.ToString.ToUpper = "LOCID" Then
                    Dim dtLOCIDObjects As New DataTable
                    Dim strConnection As String = GetSQL(4555, Nothing)(0)
                    Dim sqlParam As String = GetSQL(4555, Nothing)(1)
                    dtLOCIDObjects = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                    Try
                        tvObjectsTree.Cursor = Cursors.WaitCursor
                        Application.DoEvents()
                        Dim roottn As TreeNode = New TreeNode()
                        roottn.Text = "PLMN"
                        roottn.ImageKey = "EMPTY"
                        roottn.SelectedImageKey = "EMPTY"
                        tvObjectsTree.Nodes.Clear()
                        tvObjectsTree.Nodes.Add(roottn)
                        Dim tNode As New TreeNode
                        tNode = tvObjectsTree.Nodes(0)

                        For Each drLOC As DataRow In dtLOCIDObjects.Rows
                            Dim parentnode As TreeNode = New TreeNode(drLOC.Item(0).ToString.Trim)
                            parentnode.Name = drLOC.Item(0).ToString.Trim
                            parentnode.ImageKey = "EMPTY"
                            parentnode.SelectedImageKey = "EMPTY"
                            parentnode.Tag = "PLMN"
                            tNode.Nodes.Add(parentnode)
                        Next

                        tNode.Expand()
                        tNode = Nothing
                        System.GC.Collect()
                    Catch ex As Exception
                    Finally
                        tvObjectsTree.Cursor = Cursors.Default
                        Application.DoEvents()
                    End Try
                Else
                    FillObjectTreeData(tvObjectsTree, cmbTechnology.SelectedItem.ToString(), cmbObjectType.SelectedItem.ToString)
                End If
            End If
            countOT = 0
            lblObjectTreeCount.Text = "#: 0"
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearchObject_TextChanged(sender As Object, e As EventArgs) Handles txtSearchObject.TextChanged
        txtObjectSearch_TextChanged(tvObjectsTree, txtSearchObject.Text)
    End Sub

    Private Sub tvObjectsTree_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvObjectsTree.AfterCheck
        CheckTreeNodeAndCount(e.Node, countOT, lblObjectTreeCount)
    End Sub

    Private Sub tvObjectsTree_BeforeCheck(sender As Object, e As TreeViewCancelEventArgs) Handles tvObjectsTree.BeforeCheck
        If e.Node.ForeColor = Color.Gray And (e.Action = TreeViewAction.ByKeyboard Or e.Action = TreeViewAction.ByMouse) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub tvObjectsTree_MouseMove(sender As Object, e As MouseEventArgs) Handles tvObjectsTree.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim lstNode As List(Of TreeNode) = Treeview_GetCheck(tvObjectsTree.Nodes)
                If lstNode IsNot Nothing And lstNode.Count > 0 Then
                    Dim obj() As Object = {"ObjectTreeDrag", lstNode}
                    tvObjectsTree.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnNBFetch_Click(sender As Object, e As EventArgs) Handles btnNBFetch.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbTechnology.SelectedIndex <> 0 AndAlso cmbObjectType.SelectedIndex > -1 Then
                Dim tech As String = cmbTechnology.SelectedItem.ToString()
                Dim objectType As String = cmbObjectType.SelectedItem.ToString()
                Dim selectedObjects As String = TreeView_Checked2String(tech, objectType, "Naked", tvObjectsTree, cmbObjectType)

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                               New String() {"@Tech", Chr(39) & tech & Chr(39)},
                                               New String() {"@ObjectType", Chr(39) & objectType & Chr(39)},
                                               New String() {"@selection", Chr(39) & selectedObjects & Chr(39)}
                                           }
                strConnection = GetSQL(4574, parray)(0)
                sqlParam = GetSQL(4574, parray)(1)

                Dim dtNBFetch As New DataTable()
                dtNBFetch = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcNBFetch, gvNBFetch, dtNBFetch, "ALL")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnNBFetchCells_Click(sender As Object, e As EventArgs) Handles btnNBFetchCells.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbTechnology.SelectedIndex <> 0 AndAlso cmbObjectType.SelectedIndex > -1 Then
                Dim tech As String = cmbTechnology.SelectedItem.ToString()
                Dim objectType As String = cmbObjectType.SelectedItem.ToString()
                Dim selectedObjects As String = TreeView_Checked2String(tech, objectType, "Naked", tvObjectsTree, cmbObjectType)

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                               New String() {"@Tech", Chr(39) & tech & Chr(39)},
                                               New String() {"@ObjectType", Chr(39) & objectType & Chr(39)},
                                               New String() {"@selection", Chr(39) & selectedObjects & Chr(39)}
                                           }
                strConnection = GetSQL(4579, parray)(0)
                sqlParam = GetSQL(4579, parray)(1)

                Dim dtNBFetchCells As New DataTable()
                dtNBFetchCells = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcNBFetch, gvNBFetch, dtNBFetchCells, "ALL")
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

#Region "MML"

    Private Sub LoadMmlConfiguration()
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = Nothing
            strConnection = GetSQL(4503, parray)(0)
            sqlParam = GetSQL(4503, parray)(1)

            dtMmlConfig = New DataTable()
            dtMmlConfig = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            BindDevExComboBoxWithValueMember(cmbMMLConfig, dtMmlConfig, "MMLConfigID", "MMLConfigName", "Select...", True)
            cmbMMLConfig.SelectedIndex = 1
            Dim columnsToHide() As String = {"MMLConfigOwner", "MMLConfigDescription", "IsPublic"}
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcMmlConfig, gvMmlConfig, dtMmlConfig, "ALL", columnsToHide, "MMLConfigName")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadMMLCampaign()
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = Nothing
            strConnection = GetSQL(4502, parray)(0)
            sqlParam = GetSQL(4502, parray)(1)

            Dim dtMmlCamp As New DataTable()
            dtMmlCamp = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            dtMmlCamp.Columns(2).Caption = "ID"

            Dim columnsToHide() As String = {"ToolID", "CampaignID"}
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcMmlCampaign, gvMmlCampaign, dtMmlCamp, "ALL", columnsToHide, "CampaignName")
            gvMmlCampaign.Columns("ResultsCreated").VisibleIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearchMml_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchMml.KeyUp
        Try
            Dim dtMMLCamp As DataTable = CType(gcMmlCampaign.DataSource, DataTable)
            If dtMMLCamp IsNot Nothing Then
                If (txtSearchMml.Text.Length > 0) Then
                    dtMMLCamp.DefaultView.RowFilter = "[CampaignName] Like '%" & txtSearchMml.Text.Trim & "%' Or Convert([CampaignID],'System.String') Like '%" & txtSearchMml.Text.Trim & "%'"
                Else
                    dtMMLCamp.DefaultView.RowFilter = ""
                End If
            End If
            gvMmlCampaign_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtMmlConfigSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtMmlConfigSearch.KeyUp
        Try
            If dtMmlConfig IsNot Nothing Then
                If (txtMmlConfigSearch.Text.Length > 0) Then
                    dtMmlConfig.DefaultView.RowFilter = "[MMLConfigName] Like '%" & txtMmlConfigSearch.Text & "%'"
                Else
                    dtMmlConfig.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnMmlConfigClone_Click(sender As Object, e As EventArgs) Handles btnMmlConfigClone.Click
        Try
            Dim rIndex() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvMmlConfig.GetRow(rIndex(0)).Row
                dlgMmlConfiguration.mmlConfigID = dr("MMLConfigID")
                If dlgMmlConfiguration.ShowDialog() = DialogResult.OK Then
                    LoadMmlConfiguration()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnMmlConfigDelete_Click(sender As Object, e As EventArgs) Handles btnMmlConfigDelete.Click
        Try
            Dim rIndex() As Integer = gvMmlConfig.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvMmlConfig.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete Mml Config: " & dr("MMLConfigID").ToString & "?", "Delete MML Config", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim parray()() As String = {
                        New String() {"@MMLConfigID", dr("MMLConfigID")}
                    }
                    Dim strConnection As String = GetSQL(4523, parray)(0)
                    Dim sqlParam As String = GetSQL(4523, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    gvMmlConfig.DeleteRow(rIndex(0))
                    If gvMmlConfig.RowCount > 0 Then
                        gvMmlConfig.ClearSelection()
                        gvMmlConfig.SelectRow(0)
                        gvMmlConfig.FocusedRowHandle = 0
                        gvMmlConfig_FocusedRowChanged(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvMmlCampaign_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles gvMmlCampaign.FocusedRowChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If gvMmlCampaign.FocusedRowHandle > -1 Then
                If gvMmlConfig.FocusedRowHandle > -1 Then
                    btnValidate.Enabled = True
                    btnPreFilter.Enabled = True

                    gcValidation.DataSource = Nothing
                    gvValidation.Columns.Clear()

                    gcData.DataSource = Nothing
                    gvData.Columns.Clear()

                    gcExcluded.DataSource = Nothing
                    gvExcluded.Columns.Clear()

                    tvSelectionMml.Nodes.Clear()
                Else
                    btnValidate.Enabled = False
                    btnPreFilter.Enabled = False
                End If
                lblOwnerMmlInput.Text = gvMmlCampaign.GetRowCellValue(gvMmlCampaign.FocusedRowHandle, "CampaignOwner").ToString
                lblLastEndTimeMml.Text = gvMmlCampaign.GetRowCellValue(gvMmlCampaign.FocusedRowHandle, "ResultsCreated").ToString

                If lblOwnerMmlInput.Text.ToLower <> Environment.UserName.ToLower Then
                    btnDeleteMml.Enabled = False
                Else
                    btnDeleteMml.Enabled = True
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

    Private Sub gvMmlConfig_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles gvMmlConfig.FocusedRowChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim grdView As GridView = CType(sender, GridView)

            RemoveHandler ceIsPublicMML.CheckedChanged, AddressOf ceIsPublicMml_CheckedChanged

            If grdView.FocusedRowHandle > -1 Then
                If gvMmlCampaign.FocusedRowHandle > -1 Then
                    btnValidate.Enabled = True
                Else
                    btnValidate.Enabled = False
                End If

                LoadScriptsNB(grdView.GetRowCellValue(e.FocusedRowHandle, "MMLConfigID"))
                LoadScriptsExtcell(grdView.GetRowCellValue(e.FocusedRowHandle, "MMLConfigID"))
                LoadStaticScripts(grdView.GetRowCellValue(e.FocusedRowHandle, "MMLConfigID"))

                Dim drConfig As DataRow = GetMmlConfigDetailsByID(grdView.GetRowCellValue(e.FocusedRowHandle, "MMLConfigID"))
                If drConfig IsNot Nothing Then
                    lblOwnerMmlConfig.Text = drConfig("MMLConfigOwner").ToString
                    ceIsPublicMML.Checked = IIf(IsDBNull(drConfig("IsPublic")), False, drConfig("IsPublic"))
                End If

                If lblOwnerMmlConfig.Text.ToLower <> Environment.UserName.ToLower Then
                    ceIsPublicMML.Enabled = False
                    If ceIsPublicMML.Checked Then
                        btnMmlConfigClone.Enabled = True
                        btnMmlConfigDelete.Enabled = True
                    Else
                        btnMmlConfigClone.Enabled = False
                        btnMmlConfigDelete.Enabled = False
                    End If
                Else
                    ceIsPublicMML.Enabled = True
                    btnMmlConfigClone.Enabled = True
                    btnMmlConfigDelete.Enabled = True
                End If
            End If

            AddHandler ceIsPublicMML.CheckedChanged, AddressOf ceIsPublicMml_CheckedChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsPublicMml_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim parray()() As String = Nothing
            Dim rIndex() As Integer = gvMmlConfig.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvMmlConfig.GetRow(rIndex(0)).Row
                Dim configID As Integer = drow("MMLConfigID")
                parray = {
                    New String() {"@mmlConfigID", configID},
                    New String() {"@isPublic", IIf(ceIsPublicMML.Checked, 1, 0)}
                }
            End If

            Dim strConnection As String = GetSQL(4570, parray)(0)
            Dim sqlParam As String = GetSQL(4570, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function GetMmlConfigDetailsByID(configID As Integer) As DataRow
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@mmlConfigID", configID}}
        strConnection = GetSQL(4572, parray)(0)
        sqlParam = GetSQL(4572, parray)(1)

        Dim dtConfig As New DataTable()
        dtConfig = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dtConfig.Rows.Count > 0 Then
            Return dtConfig.Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Private Sub LoadScriptsNB(mmlConfigID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@mmlconfigid", mmlConfigID}
        }

        strConnection = GetSQL(4520, parray)(0)
        sqlParam = GetSQL(4520, parray)(1)

        Dim dtScriptsNB = New DataTable()
        dtScriptsNB = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcScriptsNB, gvScriptsNB, dtScriptsNB, "ALL")
    End Sub

    Private Sub LoadScriptsExtcell(mmlConfigID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@mmlconfigid", mmlConfigID}
        }
        strConnection = GetSQL(4521, parray)(0)
        sqlParam = GetSQL(4521, parray)(1)

        Dim dtScriptsNBExtcell = New DataTable()
        dtScriptsNBExtcell = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcScriptsExtCell, gvScriptsExtCell, dtScriptsNBExtcell, "ALL")
    End Sub

    Private Sub LoadStaticScripts(mmlConfigID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@mmlConfigID", mmlConfigID}
        }
        strConnection = GetSQL(4543, parray)(0)
        sqlParam = GetSQL(4543, parray)(1)

        Dim dtStaticScripts = New DataTable()
        dtStaticScripts = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcStaticScripts, gvStaticScripts, dtStaticScripts, "ALL")
    End Sub

    Private Sub btnPreFilter_Click(sender As Object, e As EventArgs) Handles btnPreFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing
            Dim resultSetID As String = Nothing

            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                Dim dr1 As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row

                campaignID = dr1("CampaignID")
                resultSetID = dr1("ResultSetID")
                campaignType = dr1("CampaignType")
                mmlConfigID = dr2("MMLConfigID")
                GetMmlUserFilter(resultSetID)
                LoadSelectionTree(resultSetID)
            End If

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try

    End Sub

    Private Sub InsertMmlUserFilterBulk(ByVal resultSetID As String)
        '-- Delete Userfilter for Resultsetid first
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@resultsetid", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4534, parray)(0)
        sqlParam = GetSQL(4534, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        '-- Bulkinsert selection
        Dim lstCheckedNode As New List(Of TreeNode)
        lstCheckedNode = Treeview_GetCheck(tvSelectionMml.Nodes, True)

        If lstCheckedNode.Count > 0 Then
            Dim dtmmlUserFilterSave As New DataTable()
            dtmmlUserFilterSave.Columns.Add("ResultSetID")
            dtmmlUserFilterSave.Columns.Add("FilterFieldName")
            dtmmlUserFilterSave.Columns.Add("FilterValue")

            For Each checkedNode As TreeNode In lstCheckedNode
                ApplyMmlSelectionTreeChanges(checkedNode, dtmmlUserFilterSave)
            Next

            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[MML_UserFilter]", dtmmlUserFilterSave, "MML_UserFilter")
            End If
        End If
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Bulk Insert MML User Filter...
            Dim resultSetID As String = ""
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drResultSet As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")
            End If
            InsertMmlUserFilterBulk(resultSetID)

            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                Dim dr1 As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row

                campaignID = dr1("CampaignID")
                resultSetID = dr1("ResultSetID")
                campaignType = dr1("CampaignType")
                mmlConfigID = dr2("MMLConfigID")

                LoadValidationGrid(campaignID, campaignType, resultSetID, mmlConfigID)
                LoadDataGrid(resultSetID)
                LoadExcludedGrid(resultSetID)
                'GetMmlUserFilter(resultSetID)
                'LoadSelectionTree(resultSetID)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadValidationGrid(campaignID As Integer, campaignType As String, resultSetID As String, mmlConfigID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)},
            New String() {"@MMLConfigID", mmlConfigID},
            New String() {"@Debug", 0}
        }
        strConnection = GetSQL(4516, parray)(0)
        sqlParam = GetSQL(4516, parray)(1)

        Dim dtValidation = New DataTable()
        dtValidation = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, 300)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcValidation, gvValidation, dtValidation, "ALL")
    End Sub

    Private Sub LoadDataGrid(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4517, parray)(0)
        sqlParam = GetSQL(4517, parray)(1)

        Dim dtData = New DataTable()
        dtData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, 300)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcData, gvData, dtData, "ALL")
    End Sub

    Private Sub LoadExcludedGrid(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4518, parray)(0)
        sqlParam = GetSQL(4518, parray)(1)

        Dim dtExc = New DataTable()
        dtExc = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, 300)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcExcluded, gvExcluded, dtExc, "ALL")
    End Sub

    Private Sub GetMmlUserFilter(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4535, parray)(0)
        sqlParam = GetSQL(4535, parray)(1)

        dtMmlUserFilter = New DataTable()
        dtMmlUserFilter = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadSelectionTree(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4575, parray)(0)
        sqlParam = GetSQL(4575, parray)(1)

        Dim dtSelTree = New DataTable()
        dtSelTree = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        FillObjectTree(dtSelTree, tvSelectionMml)
        CheckMmlUserFilters()
    End Sub

    Private Sub CheckMmlUserFilters()
        If dtMmlUserFilter IsNot Nothing Then
            For Each dr As DataRow In dtMmlUserFilter.Rows
                Dim nd() As TreeNode = Nothing
                nd = tvSelectionMml.Nodes.Find(dr(2), True)
                For i As Integer = 0 To nd.Length - 1
                    If dr(1) = nd(i).Parent.Name AndAlso dr(2) = nd(i).Name Then
                        nd(i).Checked = True
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub ApplyMmlSelectionTreeChanges(ByRef nd As TreeNode, ByRef dt As DataTable)
        Try
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim ResultSetID As String = dr("ResultSetID").ToString
                Dim drObjectFilter() As DataRow = Nothing

                drObjectFilter = dt.Select("ResultSetID='" & ResultSetID & "' AND FilterFieldName='" & nd.Tag & "' AND FilterValue='" & nd.Name & "'")
                If drObjectFilter.Length = 0 Then
                    Dim drow As DataRow
                    drow = dt.NewRow
                    drow("ResultSetID") = ResultSetID
                    drow("FilterFieldName") = nd.Tag
                    drow("FilterValue") = nd.Name

                    dt.Rows.Add(drow)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillObjectTree(dtData As DataTable, ByRef tree As TreeView)
        Try
            tree.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim rootn As TreeNode = New TreeNode()
            rootn.Text = "ROOT"
            rootn.ImageKey = "EMPTY"
            rootn.SelectedImageKey = "EMPTY"
            tree.Nodes.Clear()
            tree.Nodes.Add(rootn)
            Dim tNode As New TreeNode
            tNode = tree.Nodes(0)

            Dim dtParent As DataTable = dtData.DefaultView.ToTable(True, dtData.Columns(1).ColumnName)
            For Each drParent As DataRow In dtData.Select("ParentID = '" & rootn.Text & "'")
                Dim roottn As TreeNode = New TreeNode()
                roottn.Name = drParent(0).ToString
                roottn.Text = drParent(0).ToString & " (A: " & drParent(5).ToString & "/D: " & drParent(6).ToString & ")"
                roottn.Tag = drParent(4).ToString
                roottn.ImageKey = "EMPTY"
                roottn.SelectedImageKey = "EMPTY"
                tNode.Nodes.Add(roottn)
                PopulateObjectTree(drParent.Table.Columns(1).ColumnName, drParent(0).ToString, roottn, dtData)
            Next
            rootn.Expand()

        Catch ex As Exception
        Finally
            tree.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub PopulateObjectTree(ByVal parentColName As String, ByVal inParentID As String, ByVal inTreeNode As TreeNode, ByVal dt As DataTable)
        Try
            inTreeNode.Nodes.Clear()
            For Each drParent As DataRow In dt.Select(parentColName & "='" & inParentID & "'")
                Dim roottn As TreeNode = New TreeNode()
                roottn.Name = drParent(0).ToString
                roottn.Text = drParent(0).ToString & " (A: " & drParent(5).ToString & "/D: " & drParent(6).ToString & ")"
                roottn.Tag = drParent(4).ToString
                roottn.ImageKey = "EMPTY"
                roottn.SelectedImageKey = "EMPTY"
                inTreeNode.Nodes.Add(roottn)
                PopulateObjectTree(drParent.Table.Columns(1).ColumnName, drParent(0).ToString, roottn, dt)
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearchMMLObject_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMMLObject.TextChanged
        txtObjectSearch_TextChanged(tvSelectionMml, txtSearchMMLObject.Text)
    End Sub

    Private Sub tvSelectionMml_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvSelectionMml.AfterCheck
        CheckTreeNodeAndCount(e.Node, countOT, Nothing)
    End Sub

    Private Sub tvSelectionMml_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvSelectionMml.NodeMouseClick
        tvSelectionMml.SelectedNode = e.Node
    End Sub

    Public Sub CountCheckedNode(ByRef nd As TreeNode, ByRef counter As Integer)
        If nd.Nodes.Count = 0 Then
            If nd.Checked = True Then
                counter = Math.Max(counter + 1, 0)
            Else
                counter = Math.Max(counter - 1, 0)
            End If
        End If
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Bulk Insert MML User Filter...
            Dim resultSetID As String = ""
            Dim campaignName As String = ""
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drResultSet As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")
            End If
            InsertMmlUserFilterBulk(resultSetID)

            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                Dim dr1 As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row

                campaignName = dr1("CampaignName").ToString
                campaignID = dr1("CampaignID")

                campaignType = dr1("CampaignType")
                mmlConfigID = dr2("MMLConfigID")

                LoadValidationGrid(campaignID, campaignType, resultSetID, mmlConfigID)
                LoadDataGrid(resultSetID)
                LoadExcludedGrid(resultSetID)
                'GetMmlUserFilter(resultSetID)
                'LoadSelectionTree(resultSetID)
            End If

            If rIndex.Length > 0 Then
                Dim parray()() As String = {
                    New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
                }
                Dim strConnection As String = GetSQL(4540, parray)(0)
                Dim sqlParam As String = GetSQL(4540, parray)(1)
                Dim dsMmlScript As New DataSet
                dsMmlScript = IOS.DataLibrary.DataAccessorODBC.GetDataSet(strConnection, sqlParam, iQryTimeOut)

                Dim HasData As Boolean = False


                If Not dsMmlScript Is Nothing AndAlso dsMmlScript.Tables.Count > 0 Then
                    For Each dtMmlScript As DataTable In dsMmlScript.Tables
                        If dtMmlScript.Rows.Count > 0 Then
                            HasData = True
                        End If
                    Next

                    If HasData = True Then

                        Dim objFileDlg As New SaveFileDialog()
                        objFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                        objFileDlg.Filter = "Exports|*.txt;*.xml"
                        objFileDlg.Title = "Save a TXT/XML File"
                        objFileDlg.DefaultExt = ""

                        objFileDlg.FileName = campaignName & "_" & resultSetID & IIf(txtFileNameSuffix.Text = "", "", txtFileNameSuffix.Text)

                        If objFileDlg.ShowDialog() = DialogResult.OK Then

                            For Each dtMmlScript As DataTable In dsMmlScript.Tables

                                '  Dim strData() As String = GetStringArrayFromDataTable(dtMmlScript, False)
                                Dim s As String = String.Join(vbLf, dtMmlScript.Rows.OfType(Of DataRow)().[Select](Function(r) r(0).ToString()))

                                If s.Length > 0 Then

                                    If cmbOutputLocation.SelectedIndex = 0 AndAlso dsMmlScript.Tables.IndexOf(dtMmlScript) >= 0 Then '0 index used for single file or for ericsson

                                        If objFileDlg.FileName <> "" Then
                                            If dsMmlScript.Tables.IndexOf(dtMmlScript) = 0 Then
                                                System.IO.File.WriteAllText(objFileDlg.FileName.Substring(0, objFileDlg.FileName.IndexOf(".")) & "_Huawei.txt", s)
                                            Else
                                                If Not IsDBNull(dtMmlScript.Rows(0)("ENM_SOURCE").ToString) Then
                                                    System.IO.File.WriteAllText(objFileDlg.FileName.Substring(0, objFileDlg.FileName.IndexOf(".")) & "_" & dtMmlScript.Rows(0)("ENM_SOURCE").ToString & "_Ericsson.xml", s)
                                                Else
                                                    System.IO.File.WriteAllText(objFileDlg.FileName.Substring(0, objFileDlg.FileName.IndexOf(".")) & "_Ericsson.xml", s)
                                                End If
                                            End If
                                        End If

                                    ElseIf cmbOutputLocation.SelectedIndex = 1 AndAlso dsMmlScript.Tables.IndexOf(dtMmlScript) >= 0 Then '1 index used for spilt file in 2MB chunk
                                        Dim offset As Integer = 0
                                        Dim fileCount As Integer = 1
                                        Dim file_name As String = Nothing   'objFileDlg.FileName.Substring(0, objFileDlg.FileName.IndexOf("."))
                                        Dim outputBytes As String()

                                        If dsMmlScript.Tables.IndexOf(dtMmlScript) = 0 Then
                                            file_name = objFileDlg.FileName.Substring(0, objFileDlg.FileName.IndexOf("."))
                                        Else
                                            If Not IsDBNull(dtMmlScript.Rows(0)("ENM_SOURCE").ToString) Then
                                                file_name = objFileDlg.FileName.Substring(0, objFileDlg.FileName.IndexOf(".")) & "_" & dtMmlScript.Rows(0)("ENM_SOURCE").ToString
                                            Else
                                                file_name = objFileDlg.FileName.Substring(0, objFileDlg.FileName.IndexOf("."))
                                            End If
                                        End If

                                        Dim fileSize As Integer = Convert.ToInt32(seFileSize.EditValue) * 1024 * 1024     'Chunk file size is like 2MB (2097152 bytes)
                                        Dim ChunkSize As Integer = 0
                                        Dim strData() As String = GetStringArrayFromDataTable(dtMmlScript, False)

                                        For Index As Integer = 0 To strData.Length - 1
                                            Dim stringByteCount As Integer = System.Text.Encoding.UTF8.GetByteCount(strData(Index))
                                            If (ChunkSize + stringByteCount) <= fileSize Then
                                                ChunkSize = ChunkSize + stringByteCount
                                                If Index = (strData.Length - 1) Then
                                                    objFileDlg.FileName = file_name & "_" & fileCount & "." & objFileDlg.DefaultExt
                                                    outputBytes = New String(Index - offset) {}
                                                    System.Array.Copy(strData, offset, outputBytes, 0, outputBytes.Length)
                                                    System.IO.File.WriteAllLines(objFileDlg.FileName, outputBytes)
                                                    Exit For
                                                End If
                                            Else
                                                Index = Index - 1
                                                objFileDlg.FileName = file_name & "_" & fileCount & "." & objFileDlg.DefaultExt
                                                outputBytes = New String(Index - offset - 1) {}
                                                System.Array.Copy(strData, offset, outputBytes, 0, outputBytes.Length)
                                                System.IO.File.WriteAllLines(objFileDlg.FileName, outputBytes)
                                                offset = Index
                                                fileCount += 1
                                                ChunkSize = 0
                                            End If
                                        Next

                                    End If

                                End If

                            Next
                            Process.Start(IO.Path.GetDirectoryName(objFileDlg.FileName))

                        End If

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

    Private Sub cmbOutputLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOutputLocation.SelectedIndexChanged
        Try
            If cmbOutputLocation.SelectedIndex = 0 Then
                seFileSize.Enabled = False
            Else
                seFileSize.Enabled = True
                seFileSize.EditValue = 1
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnRefreshMml_Click(sender As Object, e As EventArgs) Handles btnRefreshMml.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            txtSearchMml.Text = String.Empty
            LoadMMLCampaign()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteMml_Click(sender As Object, e As EventArgs) Handles btnDeleteMml.Click
        Try
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Campaign Result Set", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@ResultSetID", Chr(39) & dr("ResultSetID") & Chr(39)},
                        New String() {"@CampaignType", Chr(39) & dr("CampaignType").ToString & Chr(39)}
                    }

                    strConnection = GetSQL(4542, parray)(0)
                    sqlParam = GetSQL(4542, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    gvMmlCampaign.DeleteRow(rIndex(0))
                    If gvMmlCampaign.RowCount > 0 Then
                        gvMmlCampaign.ClearSelection()
                        gvMmlCampaign.SelectRow(0)
                        gvMmlCampaign.FocusedRowHandle = 0
                        gvMmlCampaign_FocusedRowChanged(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvScriptsNB_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvScriptsNB.ShowingEditor
        Try
            If (lblOwnerMmlConfig.Text.ToLower <> Environment.UserName.ToLower) Or (gvScriptsNB.FocusedColumn().FieldName <> "MML_SCRIPT_ID_1") Then
                If ceIsPublicMML.Checked Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvScriptsNB_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles gvScriptsNB.CellValueChanged
        Try
            Dim mmlConfigID As Integer = 0
            Dim rIndex() As Integer = gvMmlConfig.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvMmlConfig.GetRow(rIndex(0)).Row
                mmlConfigID = drow.Item(0)
            End If

            Dim data As DataRow = gvScriptsNB.GetFocusedDataRow()
            If data IsNot Nothing Then

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                                New String() {"@mmlConfigID", mmlConfigID},
                                                New String() {"@iosTech", Chr(39) & data.Item("IOS_TECH").ToString & Chr(39)},
                                                New String() {"@sLayer", Chr(39) & data.Item("S_LAYER").ToString & Chr(39)},
                                                New String() {"@tLayer", Chr(39) & data.Item("T_LAYER").ToString & Chr(39)},
                                                New String() {"@nbType", Chr(39) & data.Item("NB_TYPE").ToString & Chr(39)},
                                                New String() {"@isCoSector", data.Item("IsCoSector")},
                                                New String() {"@isCoSite", data.Item("IsCoSite")},
                                                New String() {"@mmlScriptID1", e.Value}
                                           }
                strConnection = GetSQL(4544, parray)(0)
                sqlParam = GetSQL(4544, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvScriptsExtCell_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvScriptsExtCell.ShowingEditor
        Try
            If lblOwnerMmlConfig.Text.ToLower <> Environment.UserName.ToLower Or (gvScriptsExtCell.FocusedColumn().FieldName <> "MML_STATIC_1") Then
                If ceIsPublicMML.Checked Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvScriptsExtCell_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles gvScriptsExtCell.CellValueChanged
        Try
            Dim data As DataRow = gvScriptsExtCell.GetFocusedDataRow()
            If data IsNot Nothing Then

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                                New String() {"@mmlConfigID", data.Item("MMLConfigID")},
                                                New String() {"@sIOSTech", Chr(39) & data.Item("S_IOS_TECH") & Chr(39)},
                                                New String() {"@tIOSTech", Chr(39) & data.Item("T_IOS_TECH") & Chr(39)},
                                                New String() {"@mmlStatic1", Chr(39) & e.Value.ToString & Chr(39)}
                                           }
                strConnection = GetSQL(4545, parray)(0)
                sqlParam = GetSQL(4545, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvStaticScripts_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvStaticScripts.ShowingEditor
        Try
            If lblOwnerMmlConfig.Text.ToLower <> Environment.UserName.ToLower Or (gvStaticScripts.FocusedColumn().FieldName <> "SCRIPT_TEXT") Then
                If ceIsPublicMML.Checked Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvStaticScripts_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles gvStaticScripts.CellValueChanged
        Try
            Dim data As DataRow = gvStaticScripts.GetFocusedDataRow()
            If data IsNot Nothing Then

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                                New String() {"@mmlConfigID", data.Item("MMLConfigID")},
                                                New String() {"@mmlScriptID", data.Item("MML_SCRIPT_ID")},
                                                New String() {"@scriptText", Chr(39) & e.Value.ToString & Chr(39)}
                                           }
                strConnection = GetSQL(4546, parray)(0)
                sqlParam = GetSQL(4546, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbMMLConfig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMMLConfig.SelectedIndexChanged
        Try
            If cmbMMLConfig.SelectedIndex = 0 Then
                btnValidate.Enabled = False
            Else
                btnValidate.Enabled = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiInsertTempNB_Click(sender As Object, e As EventArgs) Handles tsmiInsertTempNB.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim resultSetID As String = ""
            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then

                Dim drResultSet As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")
                campaignID = drResultSet("CampaignID")
                campaignType = drResultSet("CampaignType")

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                                New String() {"@Resultsetid", Chr(39) & resultSetID & Chr(39)}
                                           }

                strConnection = GetSQL(4577, parray)(0)
                sqlParam = GetSQL(4577, parray)(1)
                Dim inserted As Integer = IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                If inserted - 1 > 0 Then
                    MsgBox("Number of NB inserted: " & inserted - 1)
                Else
                    MsgBox("Press Validate button before inserting objects.")
                End If

                Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()
                If rIndex2.Length > 0 Then
                    Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row
                    mmlConfigID = dr2("MMLConfigID")
                End If
                drResultSet.Item("TemporaryInserted") = True

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiRemoveTempNB_Click(sender As Object, e As EventArgs) Handles tsmiRemoveTempNB.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim resultSetID As String = ""
            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            Dim drResultSet As DataRow = Nothing
            If rIndex.Length > 0 Then
                drResultSet = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")
                campaignID = drResultSet("CampaignID")
                campaignType = drResultSet("CampaignType")

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                                New String() {"@Resultsetid", Chr(39) & resultSetID & Chr(39)}
                                           }

                strConnection = GetSQL(4578, parray)(0)
                sqlParam = GetSQL(4578, parray)(1)
                Dim removed As Integer = IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                MsgBox("Number of NB removed: " & removed - 1)

                Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()
                If rIndex2.Length > 0 Then
                    Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row
                    mmlConfigID = dr2("MMLConfigID")
                End If

                drResultSet.Item("TemporaryInserted") = False

            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiEditTempObjects_Click(sender As Object, e As EventArgs) Handles tsmiEditTempObjects.Click
        Try
            Dim objDlgTempObjects As New dlgTempObjects()
            objDlgTempObjects.StartPosition = FormStartPosition.CenterScreen
            objDlgTempObjects.ShowDialog()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiRemoveAllTempNB_Click(sender As Object, e As EventArgs) Handles tsmiRemoveAllTempNB.Click
        Try
            Dim resultSetID As String = ""
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            Dim drResultSet As DataRow = Nothing
            If rIndex.Length > 0 Then
                drResultSet = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                                                New String() {"@Resultsetid", Chr(39) & resultSetID & Chr(39)},
                                                New String() {"@DeleteAll", 1}
                                           }

                strConnection = GetSQL(4581, parray)(0)
                sqlParam = GetSQL(4581, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Helper"

    Private Sub HideDescriptionArea(ByRef pGrid As PropertyGrid)
        Dim pi As System.Reflection.PropertyInfo
        pi = pGrid.GetType().GetProperty("Controls")
        Dim cc As Control.ControlCollection = pi.GetValue(pGrid, Nothing)
        For Each ctrl As Control In cc
            Dim ct As Type = ctrl.GetType()
            Dim sName As String = ct.Name
            If sName = "DocComment" Then
                pi = ct.GetProperty("Height")
                pi.SetValue(ctrl, 0, Nothing)
                Dim fi As System.Reflection.FieldInfo
                fi = ct.BaseType.GetField("userSized", Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
                fi.SetValue(ctrl, True)
            End If
        Next
        pGrid.Refresh()
    End Sub

    Private Sub LoadLayerProperties(ByRef propertyGridCtrl As PropertyGrid, CampaignType As String, Optional dr As DataRow = Nothing)
        Dim layerProperties As New CustomClass()
        propertyGridCtrl.SelectedObject = layerProperties
        layerProperties.Clear()
        Dim dtProperties As New DataTable

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignType", Chr(39) & CampaignType & Chr(39)}
        }
        strConnection = GetSQL(4537, parray)(0)
        sqlParam = GetSQL(4537, parray)(1)
        dtProperties = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dtProperties IsNot Nothing Then
            Dim drProp As DataRow
            drProp = dtProperties.NewRow
            drProp("ConfigFieldName") = "InclusionList"
            drProp("ConfigFieldType") = "ComboBoxLayer"
            drProp("ConfigFieldDescription") = "InclusionList"
            drProp("ConfigFieldEditable") = 1
            dtProperties.Rows.Add(drProp)

            drProp = dtProperties.NewRow
            drProp("ConfigFieldName") = "ExclusionList"
            drProp("ConfigFieldDescription") = "ExclusionList"
            drProp("ConfigFieldType") = "ComboBoxLayer"
            drProp("ConfigFieldEditable") = 1
            dtProperties.Rows.Add(drProp)

            dtProperties.AcceptChanges()
        End If

        For Each row As DataRow In dtProperties.Rows
            Dim value As Object = Nothing
            Dim fieldValue As Object = Nothing
            Dim fieldName As String = row.Item("ConfigFieldName")
            If dr Is Nothing Then
                value = Convert.ToString(row.Item("ConfigFieldDefaultValue"))
            Else
                If dr.Table.Columns.Contains(fieldName) Then
                    fieldValue = dr(fieldName)
                Else
                    If CampaignType = "NB_Detect" And fieldName = "CampaignID" Then
                        Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()
                        If rIndex.Length > 0 Then
                            Dim drow As DataRow = gvDetectCampaigns.GetRow(rIndex(0)).Row
                            fieldValue = drow.Item("CampaignID")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf CampaignType = "NB_Copy" And fieldName = "CampaignID" Then
                        Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()
                        If rIndex.Length > 0 Then
                            Dim drow As DataRow = gvCopyCampaigns.GetRow(rIndex(0)).Row
                            fieldValue = drow.Item("CampaignID")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf CampaignType = "NB_Delete" And fieldName = "CampaignID" Then
                        Dim rIndex() As Integer = gvDeleteCampaigns.GetSelectedRows()
                        If rIndex.Length > 0 Then
                            Dim drow As DataRow = gvDeleteCampaigns.GetRow(rIndex(0)).Row
                            fieldValue = drow.Item("CampaignID")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "InclusionList" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtCellList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ListID") = dr.Item("UseInclusionListID")).ToArray()
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("ListName")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "ExclusionList" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtCellList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ListID") = dr.Item("UseExceptionListID")).ToArray()
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("ListName")
                        Else
                            fieldValue = ""
                        End If
                    Else
                        fieldValue = ""
                    End If
                End If
                If row.Item("ConfigFieldType") = "ComboBoxBoolean" Then
                    If fieldValue <> "0" And fieldValue <> "1" Then fieldValue = 0
                    value = Convert.ToBoolean(fieldValue)
                Else
                    value = fieldValue
                End If
            End If

            Dim myProp As New CustomProperty(CampaignType & " Properties", row.Item("ConfigFieldName"), row.Item("ConfigFieldType"), row.Item("ConfigFieldDescription"), Not row.Item("ConfigFieldEditable"), value)
            layerProperties.Add(myProp)
        Next
        propertyGridCtrl.Refresh()
    End Sub

    Private Sub grdIncExcDetect_DragOver(sender As Object, e As DragEventArgs)
        Try
            If e.Data.GetDataPresent("System.Object[]") Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadCampaigns()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(4500, parray)(0)
        sqlParam = GetSQL(4500, parray)(1)

        dtNBCampaigns = New DataTable()
        dtNBCampaigns = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadConfigSummaryGrid(CampaignID As Integer, CampaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        Dim columnsToHide() As String = Nothing

        If CampaignType.ToLower = "nb_audit" Then
            parray = Nothing
            parray = {New String() {"@campaignID", CampaignID}}
            strConnection = GetSQL(4556, parray)(0)
            sqlParam = GetSQL(4556, parray)(1)
            parray = {
                New String() {"@CampaignID", CampaignID},
                New String() {"@CampaignType", Chr(39) & CampaignType & Chr(39)}
            }
            strConnection = GetSQL(4504, parray)(0)
            sqlParam = GetSQL(4504, parray)(1)

            Dim dtNbAudit As New DataTable()
            dtNbAudit = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            RemoveHandler gvConfigNBAudit.FocusedRowChanged, AddressOf gvConfigNBAudit_FocusedRowChanged
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcConfigNBAudit, gvConfigNBAudit, dtNbAudit, "ALL")
            AddHandler gvConfigNBAudit.FocusedRowChanged, AddressOf gvConfigNBAudit_FocusedRowChanged
            If gvConfigNBAudit.RowCount > 0 Then
                gvConfigNBAudit.ClearSelection()
                gvConfigNBAudit.FocusedRowHandle = 0
                gvConfigNBAudit.SelectRow(0)
            End If
            gvConfigNBAudit_FocusedRowChanged(Nothing, Nothing)
        Else
            parray = {
                New String() {"@CampaignID", CampaignID},
                New String() {"@CampaignType", Chr(39) & CampaignType & Chr(39)}
            }
            strConnection = GetSQL(4504, parray)(0)
            sqlParam = GetSQL(4504, parray)(1)

            Dim dtNbConfig As New DataTable()
            dtNbConfig = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            If CampaignType.ToLower = "nb_copy" Then
                RemoveHandler gvConfigSummCopy.FocusedRowChanged, AddressOf gvConfigSummCopy_FocusedRowChanged
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcConfigSummCopy, gvConfigSummCopy, dtNbConfig, "ALL")
                AddHandler gvConfigSummCopy.FocusedRowChanged, AddressOf gvConfigSummCopy_FocusedRowChanged
                If gvConfigSummCopy.RowCount > 0 Then
                    gvConfigSummCopy.ClearSelection()
                    gvConfigSummCopy.FocusedRowHandle = 0
                    gvConfigSummCopy.SelectRow(0)
                End If
                gvConfigSummCopy_FocusedRowChanged(Nothing, Nothing)
            ElseIf CampaignType.ToLower = "nb_delete" Then
                RemoveHandler gvConfigSummDelete.FocusedRowChanged, AddressOf gvConfigSummDelete_FocusedRowChanged
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcConfigSummDelete, gvConfigSummDelete, dtNbConfig, "ALL")
                AddHandler gvConfigSummDelete.FocusedRowChanged, AddressOf gvConfigSummDelete_FocusedRowChanged
                If gvConfigSummDelete.RowCount > 0 Then
                    gvConfigSummDelete.ClearSelection()
                    gvConfigSummDelete.FocusedRowHandle = 0
                    gvConfigSummDelete.SelectRow(0)
                End If
                gvConfigSummDelete_FocusedRowChanged(Nothing, Nothing)
            ElseIf CampaignType.ToLower = "manual" Then
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcManual, gvManual, dtNbConfig, "ALL")
            Else
                RemoveHandler gvConfigSummDetect.FocusedRowChanged, AddressOf gvConfigSummDetect_FocusedRowChanged
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcConfigSummDetect, gvConfigSummDetect, dtNbConfig, "ALL")
                AddHandler gvConfigSummDetect.FocusedRowChanged, AddressOf gvConfigSummDetect_FocusedRowChanged
                If gvConfigSummDetect.RowCount > 0 Then
                    gvConfigSummDetect.ClearSelection()
                    gvConfigSummDetect.FocusedRowHandle = 0
                    gvConfigSummDetect.SelectRow(0)
                End If
                gvConfigSummDetect_FocusedRowChanged(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub LoadResultSummaryGrid(ByRef grd As GridControl, resultSetID As String, campaignType As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = Nothing

            parray = {
                New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)},
                New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
            }
            strConnection = GetSQL(4505, parray)(0)
            sqlParam = GetSQL(4505, parray)(1)

            Dim dtResultSumm As New DataTable()
            dtResultSumm = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            Dim columnsToHide() As String = {"CampaignID", "ResultSetID"}
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grd, grd.MainView, dtResultSumm, "ALL", columnsToHide)
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub DeleteCampaign(campaignID As Integer, campaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
        }

        strConnection = GetSQL(4508, parray)(0)
        sqlParam = GetSQL(4508, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub RunNow(campaignID As Integer, campaignType As String)
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@CampaignID", campaignID},
                New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
            }

            strConnection = GetSQL(4509, parray)(0)
            sqlParam = GetSQL(4509, parray)(1)

            IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadResultSetCombo(ByRef cmb As ComboBoxEdit, CampaignID As String, CampaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@CampaignID", CampaignID}}
        strConnection = GetSQL(4530, parray)(0)
        sqlParam = GetSQL(4530, parray)(1)

        Dim dtResultSetID As New DataTable()
        dtResultSetID = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        cmb.Tag = CampaignType
        BindDevExComboBoxWithValueMember(cmb, dtResultSetID, "ResultSetID", "ResultSetID", "Select")
    End Sub

    Private Function GetCampaignDetailsByID(CampaignID As Integer) As DataRow
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@CampaignID", CampaignID}}
        strConnection = GetSQL(4533, parray)(0)
        sqlParam = GetSQL(4533, parray)(1)

        Dim dtCampaignDetails As New DataTable()
        dtCampaignDetails = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dtCampaignDetails.Rows.Count > 0 Then
            Return dtCampaignDetails.Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Private Sub UpdateCampaign(CampaignType As String)
        Dim campaignID As Integer = 0
        Dim parray()() As String = Nothing
        If CampaignType = "NB_Detect" Then
            Dim rIndex() As Integer = gvDetectCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvDetectCampaigns.GetRow(rIndex(0)).Row
                campaignID = drow("CampaignID")
                parray = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@Enabled", IIf(ceActiveDetect.Checked, 1, 0)},
                    New String() {"@SchNextStartTime", Chr(39) & deSchNxtStartTimeDetect.EditValue & Chr(39)},
                    New String() {"@SchRptInterval", Chr(39) & IIf(cmbSchRptIntervalDetect.SelectedText = "", "NULL", cmbSchRptIntervalDetect.SelectedText) & Chr(39)},
                    New String() {"@isPublic", IIf(ceIsPublicDetect.Checked, 1, 0)}
                }
            End If
        ElseIf CampaignType = "NB_Copy" Then
            Dim rIndex() As Integer = gvCopyCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvCopyCampaigns.GetRow(rIndex(0)).Row
                campaignID = drow("CampaignID")
                parray = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@Enabled", IIf(ceActiveCopy.Checked, 1, 0)},
                    New String() {"@SchNextStartTime", Chr(39) & deSchNxtStartTimeCopy.EditValue & Chr(39)},
                    New String() {"@SchRptInterval", Chr(39) & IIf(cmbSchRptIntervalCopy.SelectedText = "", "NULL", cmbSchRptIntervalCopy.SelectedText) & Chr(39)},
                    New String() {"@isPublic", IIf(ceIsPublicCopy.Checked, 1, 0)}
                }
            End If
        ElseIf CampaignType = "NB_Delete" Then
            Dim rIndex() As Integer = gvDeleteCampaigns.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvDeleteCampaigns.GetRow(rIndex(0)).Row
                campaignID = drow("CampaignID")
                parray = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@Enabled", IIf(ceActiveDelete.Checked, 1, 0)},
                    New String() {"@SchNextStartTime", Chr(39) & deSchNxtStartTimeDelete.EditValue & Chr(39)},
                    New String() {"@SchRptInterval", Chr(39) & IIf(cmbSchRptIntervalDelete.SelectedText = "", "NULL", cmbSchRptIntervalDelete.SelectedText) & Chr(39)},
                    New String() {"@isPublic", IIf(ceIsPublicDelete.Checked, 1, 0)}
                }
            End If
        ElseIf CampaignType = "NB_Manual" Then
            Dim rIndex() As Integer = gvCampaignManual.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvCampaignManual.GetRow(rIndex(0)).Row
                campaignID = drow("CampaignID")
                parray = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@Enabled", 1},
                    New String() {"@SchNextStartTime", "NULL"},
                    New String() {"@SchRptInterval", "NULL"},
                    New String() {"@isPublic", IIf(ceIsPublicManual.Checked, 1, 0)}
                }
            End If
        ElseIf CampaignType = "NB_Audit" Then
            Dim rIndex() As Integer = gvCampNBAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvCampNBAudit.GetRow(rIndex(0)).Row
                campaignID = drow("CampaignID")
                parray = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@Enabled", IIf(chkActiveNBAudit.Checked, 1, 0)},
                    New String() {"@SchNextStartTime", Chr(39) & dtpStartTimeNBAudit.EditValue & Chr(39)},
                    New String() {"@SchRptInterval", Chr(39) & IIf(cmbRepeatIntervalNBAudit.SelectedText = "", "NULL", cmbRepeatIntervalNBAudit.SelectedText) & Chr(39)},
                    New String() {"@isPublic", IIf(ceIsPublicAudit.Checked, 1, 0)}
                }
            End If
        End If

        Dim strConnection As String = GetSQL(4526, parray)(0)
        Dim sqlParam As String = GetSQL(4526, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Function GetStringArrayFromDataTable(ByRef dTable As DataTable, Optional ByVal WithHeader As Boolean = True) As String()
        '--------Columns Name-----------
        Dim sb As StringBuilder = New StringBuilder()
        Dim intClmn As Integer = dTable.Columns.Count

        If WithHeader Then
            Dim i As Integer = 0
            For i = 0 To intClmn - 1 Step i + 1
                sb.Append("""" + dTable.Columns(i).ColumnName.ToString() + """")
                If i = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)
        End If

        '--------Data By  Columns---------

        Dim row As DataRow
        For Each row In dTable.Rows
            Dim ir As Integer = 0
            For ir = 0 To intClmn - 1 Step ir + 1
                'sb.Append("""" + row(ir).ToString().Replace("""", """""") + """")
                sb.Append(row(ir).ToString)
                If ir = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)
        Next
        Dim strData() As String = sb.ToString().Split(vbNewLine)
        Return strData
    End Function

    Private Sub DeleteCampaignResultSet(ByRef cmb As ComboBoxEdit)
        If cmb.SelectedIndex > 0 Then
            Dim parray()() As String = {
                New String() {"@ResultSetID", Chr(39) & cmb.SelectedItem.ToString() & Chr(39)},
                New String() {"@CampaignType", Chr(39) & cmb.Tag & Chr(39)}
            }
            Dim strConnection As String = GetSQL(4542, parray)(0)
            Dim sqlParam As String = GetSQL(4542, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            cmb.Properties.Items.Remove(cmb.SelectedItem)
            cmb.SelectedIndex = 0
        Else
            XtraMessageBox.Show("Please select result set id.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmb.Focus()
        End If
    End Sub

    Public Sub InsertBulkDataToServer(ConnString As String, DestinationTable As String, dtData As DataTable, Optional opType As String = "NB_Manual")
        Using cn As New System.Data.SqlClient.SqlConnection(ConnString)
            cn.Open()
            Using copy As New System.Data.SqlClient.SqlBulkCopy(cn)

                copy.DestinationTableName = DestinationTable
                copy.NotifyAfter = 1000
                AddHandler copy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                If opType = "NB_Manual" Then
                    copy.ColumnMappings.Add("CampaignID", "CampaignID")
                    copy.ColumnMappings.Add("S_CELLNAME", "S_CELLNAME")
                    copy.ColumnMappings.Add("S_IOS_TECH", "S_IOS_TECH")
                    copy.ColumnMappings.Add("T_CELLNAME", "T_CELLNAME")
                    copy.ColumnMappings.Add("T_IOS_TECH", "T_IOS_TECH")
                    copy.ColumnMappings.Add("DeleteFlag", "DeleteFlag")
                    copy.ColumnMappings.Add("ReverseFlag", "ReverseFlag")
                    copy.ColumnMappings.Add("HighPrioNB", "HighPrioNB")
                ElseIf opType = "MML_UserFilter" Then
                    copy.ColumnMappings.Add("ResultSetID", "ResultSetID")
                    copy.ColumnMappings.Add("FilterFieldName", "FilterFieldName")
                    copy.ColumnMappings.Add("FilterValue", "FilterValue")
                End If
                copy.WriteToServer(dtData)
            End Using
        End Using
    End Sub

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlClient.SqlRowsCopiedEventArgs)
        lblRecordsCountManual.Text = "Completed - Count: " & args.RowsCopied.ToString
    End Sub

    Private Sub LoadNBCombos(SQLID As String, ByRef cmb As ComboBoxEdit)
        Dim strConnection As String = GetSQL(SQLID, Nothing)(0)
        Dim sqlParam As String = GetSQL(SQLID, Nothing)(1)
        Dim dtList As New DataTable
        dtList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmb, dtList, dtList.Columns(0).ColumnName, dtList.Columns(0).ColumnName, "Select")
    End Sub

    Private Sub tsmi_Manual_DeleteRows_Click(sender As Object, e As EventArgs) Handles tsmi_Manual_DeleteRows.Click
        gcManual_KeyDown(gcManual, New KeyEventArgs(Keys.Delete))
    End Sub

#End Region

End Class

Class RunNowClass
    Public campaignID As Integer
    Public campaignType As String
    Public Status As Integer
    Public CampaignRow As DataRow
    Public Event ThreadComplete(row As DataRow, Status As Integer, ByVal ti As Threading.Thread)

    Sub RunNow()
        Try
            Status = 1
            UpdateCampaignLastStatus(campaignID, Status)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@CampaignID", campaignID},
                New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
            }

            strConnection = GetSQL(4509, parray)(0)
            sqlParam = GetSQL(4509, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, 600)
            Status = 0
            UpdateCampaignLastStatus(campaignID, Status)
        Catch ex As Exception
            Status = -1
            UpdateCampaignLastStatus(campaignID, Status)
        Finally
            RaiseEvent ThreadComplete(CampaignRow, Status, Threading.Thread.CurrentThread)
        End Try
    End Sub

    Sub RunManual()
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@CampaignID", campaignID}
            }

            strConnection = GetSQL(4541, parray)(0)
            sqlParam = GetSQL(4541, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
        Finally
            RaiseEvent ThreadComplete(CampaignRow, Status, Threading.Thread.CurrentThread)
        End Try
    End Sub

End Class