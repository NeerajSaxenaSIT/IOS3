Imports IOS.Configuration
Imports IOS.DataLibrary
Imports IOS.Library
Imports LidorSystems.IntegralUI.Lists
Imports MapInfo.Data
Imports MapInfo.Geometry
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base

Public Class frmTagManager

#Region "Variables"

    Dim isTagRename As Boolean = False
    Dim isTagEdit As Boolean = True
    Dim dtTags As DataTable = Nothing
    Public dtTemplateManagerData As DataTable = Nothing
    Public dtParameterData As DataTable = Nothing
    Dim imgList As New ImageList
    Dim isRequestByTab As Boolean = False
    Dim tagsSubItemToEdit As TreeListViewSubItem = Nothing
    Public isSelectedByTag As Boolean = False
    Public isRegionTabFirstTime As Boolean = False
    Public regionBasedDT As DataTable = Nothing
    Private oldTagName As String = Nothing

#End Region

#Region "Form & Control Events"

    Private Sub frmTagManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.SuspendLayout()

            cmbTagType.Properties.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("Select Tag Type", "Select Tag Type", CheckState.Checked, True, 0))
            cmbTagType.Properties.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("Static List", "Static List", CheckState.Unchecked, True, 1))
            cmbTagType.Properties.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("Admin List", "Admin List", CheckState.Unchecked, False, 2))
            cmbTagType.Properties.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("CM Based List", "CM Based List", CheckState.Unchecked, True, 3))
            cmbTagType.Properties.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("Region Based List", "Region Based List", CheckState.Unchecked, True, 4))

            BindTechnology()
            cmbTagType.Properties.Items.First(Function(x) x.Tag = 0).CheckState = CheckState.Checked
            xtcTagManager.Enabled = False
            imgList.Images.Add("True", EmbeddedImage("square_green.bmp"))
            imgList.Images.Add("False", EmbeddedImage("square_red.bmp"))
            Me.ConfigurTagManagerForm("frmTagManager")
            isRegionTabFirstTime = True
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub frmTagManager_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        frmMapWindow.isTagSelection = False
    End Sub

    Private Sub frmTagManager_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        SetAutoFiltersOnGrid(gcTags, gvTags)
    End Sub

    Private Sub frmTagManager_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If (dlgMappingSelection.Visible) Then
            dlgMappingSelection.BringToFront()
            dlgMappingSelection.TopMost = True
        End If
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub txtbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim txtbox As DevExpress.XtraEditors.TextEdit = CType(sender, DevExpress.XtraEditors.TextEdit)
            Dim rowIndex As Integer = txtbox.Tag
            dtParameterData.Rows(rowIndex)("ParameterValue") = txtbox.Text
            dtParameterData.Rows(rowIndex)("IsUpdate") = True
            btnInsertCMBased.LookAndFeel.SetSkinStyle("Blueprint")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvTags_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gvTags.InitNewRow
        Try
            gvTags.SetRowCellValue(e.RowHandle, gvTags.Columns(1), CInt(gvTagsList.GetFocusedRowCellValue("TagID")))
            btnTagSave.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.UserLookAndFeel.DefaultSkinName)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvTags_KeyDown(sender As Object, e As KeyEventArgs) Handles gvTags.KeyDown
        If e.KeyData = Keys.Delete Then
            Dim rowIndex() As Integer = gvTags.GetSelectedRows()
            For i As Integer = rowIndex.Length - 1 To 0 Step -1
                gvTags.DeleteRow(rowIndex(i))
                gvTags.RefreshData()
            Next
        End If
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            xtcTagManager.Enabled = False
            If Not (cmbTechnology.SelectedIndex = 0) Then
                cmbObjectType.SuspendLayout()
                Try
                    'Dim cmdText As String = "SELECT DISTINCT object FROM dbo.IOS_Object_Configuration where tech='" & cmbTechnology.SelectedItem.ToString & "' and SQLID is Not NULL  order by object"
                    Dim pmData As DataTable = clsSQLCommands.GetDistinctObject(connStrIOSServer, cmbTechnology.SelectedItem.ToString) 'DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
                    BindDevExComboBoxWithValueMember(cmbObjectType, pmData, "object", "object", "Object Type")
                    cmbObjectType.Refresh()
                    cmbObjectType.ResumeLayout()
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                End Try
            Else
                ClearComboBox(cmbObjectType, "Object Type")
            End If
            dtTemplateManagerData = Nothing
            cmbFilterOnObject.Properties.Items.Clear()
            tlvParameter.Nodes.Clear()
            tlvCMParamenter.Nodes.Clear()
            EnableDisableAddTagBtn()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbObjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbObjectType.SelectedIndexChanged
        xtcTagManager.Enabled = False
        If cmbTagType.Properties.Items.First(Function(x) x.Tag = 2).CheckState = CheckState.Checked Then
            cmbTagType.Properties.Items.First(Function(x) x.Tag = 2).CheckState = CheckState.Unchecked
            cmbTagType.Properties.Items.First(Function(x) x.Tag = 0).CheckState = CheckState.Checked
        End If

        If Not (cmbObjectType.SelectedIndex = 0) Then
            BindTagsGrid()
        Else
            IOSDevExpressGrid.ClearGrid(gcTagsList)
            gvTags.Columns.Clear()
        End If
        EnableDisableAddTagBtn()
    End Sub

    Private Sub cmbFilterOnObject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterOnObject.SelectedIndexChanged
        If (dtTemplateManagerData IsNot Nothing) Then
            isRequestByTab = False
            BindParameterData(dtTemplateManagerData)
        End If
    End Sub

    Private Sub gvTagsList_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        If gvTagsList.GetFocusedRow IsNot Nothing Then
            Dim tagType As String = gvTagsList.GetFocusedRowCellValue("TagType").ToString
            xtcTagManager.Enabled = True
            EnableTab(tagType)
            If tagType = "CM Based List" Then
                BindCMBasedParameterTag()
            End If
        End If
    End Sub

    Private Sub tlv_CMParamenter_DragDrop(sender As Object, e As DragEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim node As LidorSystems.IntegralUI.Lists.TreeListViewNode = e.Data.GetData("LidorSystems.IntegralUI.Lists.TreeListViewNode")
            If (node IsNot Nothing AndAlso node.SubItems IsNot Nothing) Then
                btnInsertCMBased.LookAndFeel.SetSkinStyle("Blueprint")
                Dim newnode As TreeListViewNode = New TreeListViewNode()
                newnode.Key = ""
                newnode.Tag = node.SubItems(0).Text
                newnode.Text = node.SubItems(2).Text
                AddParametersNode(newnode, node.SubItems(2).Text)

                newnode.ExpandAll()
                tlvCMParamenter.Nodes.Add(newnode)

                tlvCMParamenter.UpdateCurrentView()
                tlvCMParamenter.ResumeUpdate()
                For Each col As TreeListViewColumn In tlvCMParamenter.Columns
                    tlvCMParamenter.AutoSizeColumn(col)
                Next
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tlv_CMParamenter_DragOver(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub tlv_CMParamenter_MouseDown(sender As Object, e As MouseEventArgs)
        If (e.Button = MouseButtons.Right) Then
            Dim mousePosition As New System.Drawing.Point(e.X, e.Y)
            Dim node As TreeListViewNode = tlvCMParamenter.GetNodeAt(mousePosition)
            If (node IsNot Nothing) Then
                Dim TempsubItem As TreeListViewSubItem = tlvCMParamenter.GetSubItem(node, mousePosition)
                If (TempsubItem IsNot Nothing) Then
                    tagsSubItemToEdit = TempsubItem
                End If
            End If
        End If
    End Sub

    Private Sub togBtnDraw_Click(sender As Object, e As EventArgs) Handles togBtnDraw.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If Not (String.IsNullOrEmpty(txtRegionName.Text)) Then
                togBtnDraw.ChangeToggleState()
                If togBtnDraw.ToggleState = CheckState.Checked Then
                    If (Not IsRegionValidFromList(txtRegionName.Text)) Then
                        If (Not IsRegionValid(txtRegionName.Text)) Then
                            txtRegionName.Text = ""
                            SetMessage("Region Name already exist.")
                            Exit Sub
                        End If
                    Else
                        SetMessage("Region already in list.")
                    End If
                    Me.Visible = False
                    frmMapWindow.isTagSelection = True
                    RegionDT_DeleteUseLessRegion()

                    frmMapWindow.objMapHelper.RemoveLayer(frmMapWindow.tempRegionTagDT)
                    frmMapWindow.addRectagulType = AddRectangleType.RegionTag
                    frmMapWindow.AddTempTabletoMap(frmMapWindow.tempRegionTagDT)
                    frmMapWindow.AddRectangleToolStripButton2.PerformClick()
                    frmMapWindow.Show()
                    togBtnDraw.ToggleState = False
                Else
                End If
            Else
                SetMessage("Enter Region Name.")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvTagsList_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvTagsList.ShowingEditor
        Try
            If gvTagsList.FocusedColumn.FieldName = "TagName" Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch
        End Try
    End Sub

    Private Sub gvTagsList_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (isTagRename) Then
                If (e.Value IsNot Nothing) Then
                    RemoveHandler gvTagsList.CellValueChanged, AddressOf gvTagsList_CellValueChanged
                    If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                        Try
                            If Not (oldTagName.ToUpper = e.Value.ToUpper) Then
                                clsSQLCommands.RenameTag(connStrIOSServer, e.Value, CInt(gvTagsList.GetFocusedRowCellValue("TagID")))
                                SetMessage("Tag Successfully renamed.")
                                isTagRename = False
                                gvTagsList.OptionsBehavior.Editable = False
                                gvTagsList.OptionsBehavior.ReadOnly = True
                            End If
                        Catch ex As Exception
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                        Finally
                        End Try
                    Else
                        SetMessage("You are not owner of this Tag, you can't rename.")
                    End If
                    AddHandler gvTagsList.CellValueChanged, AddressOf gvTagsList_CellValueChanged
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub xtcTagManager_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcTagManager.SelectedPageChanged
        Try
            If xtcTagManager.SelectedTabPage IsNot Nothing Then
                If (xtcTagManager.SelectedTabPage.Tag IsNot Nothing) Then
                    If (xtcTagManager.SelectedTabPage.Tag = "List") Then
                        ListTab_Opening(xtcTagManager.SelectedTabPage)
                    ElseIf (xtcTagManager.SelectedTabPage.Tag = "CMBased") Then
                        btnTagSave.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.UserLookAndFeel.DefaultSkinName)
                        CMBasedTab_Opening(xtcTagManager.SelectedTabPage)
                    ElseIf (xtcTagManager.SelectedTabPage.Tag = "RegionBased") Then
                        RegionBasedTab_Opening(xtcTagManager.SelectedTabPage)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchLongName_TextChanged(sender As Object, e As EventArgs) Handles txtSearchLongName.TextChanged
        If (txtSearchLongName.Text.Trim.Length >= 3) Then
            If (dtTemplateManagerData IsNot Nothing) Then
                BindParameterData(dtTemplateManagerData)
            End If
        End If
    End Sub

    Private Sub tlvParameter_DragDrop(sender As Object, e As DragEventArgs) Handles tlvParameter.DragDrop
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub tlvParameter_MouseDown(sender As Object, e As MouseEventArgs) Handles tlvParameter.MouseDown
        Dim tree As TreeView = TryCast(sender, TreeView)
        If (tree IsNot Nothing) Then
            Dim item As TreeViewHitTestInfo = tree.HitTest(e.Location)
            If item.Node IsNot Nothing Then
                If (e.Button = MouseButtons.Left) Then
                    tree.DoDragDrop(item.Node, DragDropEffects.Copy)
                Else
                    tree.SelectedNode = item.Node
                End If
            End If
        End If
    End Sub

    Private Sub tlvCMParamenter_AfterLabelEdit(sender As Object, e As LidorSystems.IntegralUI.ObjectEditEventArgs) Handles tlvCMParamenter.AfterLabelEdit
        Try
            If (e.Label IsNot Nothing) Then
                If (gvTagsList.FocusedRowHandle <> -1) Then
                    If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                        Try
                            Dim oldLable As String
                            Dim tlvItem As TreeListViewSubItem = TryCast(e.Object, LidorSystems.IntegralUI.Lists.TreeListViewSubItem)
                            If (tlvItem IsNot Nothing) Then
                                oldLable = tlvItem.Text
                                If Not (oldLable.ToUpper() = e.Label.ToUpper()) Then
                                    Dim columnName As String = tlvCMParamenter.Columns.Item(tlvItem.Index).HeaderText
                                    Dim updateColumn As String
                                    If (columnName = "Name") Then
                                        updateColumn = "ParameterName='" & e.Label & "'"
                                    ElseIf (columnName = "Operator") Then
                                        updateColumn = "ParameterOperator='" & e.Label & "'"
                                    ElseIf (columnName = "Value") Then
                                        updateColumn = "ParameterValue='" & e.Label & "'"
                                    Else
                                        SetMessage("Column Not belong to table.")
                                        Exit Sub
                                    End If

                                    dtParameterData.Rows(tlvItem.Key)("ParameterName") = e.Label.ToString
                                    dtParameterData.Rows(tlvItem.Key)("IsUpdate") = True
                                    tlvItem.Text = e.Label
                                    tlvCMParamenter.UpdateCurrentView()
                                    btnTagSave.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.UserLookAndFeel.DefaultSkinName)
                                End If
                            End If
                        Catch ex As Exception
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                        Finally
                        End Try
                    Else
                        SetMessage("Not owner")
                    End If
                End If
            End If
            e.Cancel = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlvCMParamenter_DragDrop(sender As Object, e As DragEventArgs) Handles tlvCMParamenter.DragDrop
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Dim node As LidorSystems.IntegralUI.Lists.TreeListViewNode = e.Data.GetData("LidorSystems.IntegralUI.Lists.TreeListViewNode")
            If (node IsNot Nothing AndAlso node.SubItems IsNot Nothing) Then
                btnInsertCMBased.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.UserLookAndFeel.DefaultSkinName)
                Dim newnode As TreeListViewNode = New TreeListViewNode()
                newnode.Key = ""
                newnode.Tag = node.SubItems(0).Text
                newnode.Text = node.SubItems(2).Text
                AddParametersNode(newnode, node.SubItems(2).Text)

                newnode.ExpandAll()
                tlvCMParamenter.Nodes.Add(newnode)
                tlvCMParamenter.UpdateCurrentView()
                tlvCMParamenter.ResumeUpdate()
                For Each col As TreeListViewColumn In tlvCMParamenter.Columns
                    tlvCMParamenter.AutoSizeColumn(col)
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub tlvCMParamenter_DragOver(sender As Object, e As DragEventArgs) Handles tlvCMParamenter.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub tlvCMParamenter_MouseDown(sender As Object, e As MouseEventArgs) Handles tlvCMParamenter.MouseDown
        If (e.Button = MouseButtons.Right) Then
            Dim mousePosition As New System.Drawing.Point(e.X, e.Y)
            Dim node As TreeListViewNode = tlvCMParamenter.GetNodeAt(mousePosition)
            If (node IsNot Nothing) Then
                Dim TempsubItem As TreeListViewSubItem = tlvCMParamenter.GetSubItem(node, mousePosition)
                If (TempsubItem IsNot Nothing) Then
                    tagsSubItemToEdit = TempsubItem
                End If
            End If
        End If
    End Sub

    Private Sub txtRegionName_Leave(sender As Object, e As EventArgs) Handles txtRegionName.Leave
        If (Not txtRegionName.Text = "" AndAlso Not String.IsNullOrEmpty(txtRegionName.Text)) Then
            If (Not IsRegionValidFromList(txtRegionName.Text)) Then
                If (Not IsRegionValid(txtRegionName.Text)) Then
                    txtRegionName.Text = ""
                    SetMessage("Region Name already exist.")
                Else
                End If
            Else
                SetMessage("Region already in list.")
            End If
        End If
    End Sub

    Private Sub btnManualCommit_Click(sender As Object, e As EventArgs) Handles btnManualCommit.Click
        If (gvTagsList.FocusedRowHandle <> -1) Then
            If (Not txtRegionName.Text = "" AndAlso Not String.IsNullOrEmpty(txtRegionName.Text)) Then
                Try
                    Application.UseWaitCursor = True
                    Application.DoEvents()
                    DataAccessorODBC.KeepConnectionOpen = True
                    InsertRegionFromDT()
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                Finally
                    DataAccessorODBC.KeepConnectionOpen = False
                End Try
            Else
                SetMessage("Enter Region Name.")
            End If
        Else
            SetMessage("Select Tag.")
        End If
        Application.UseWaitCursor = False
    End Sub

    Private Sub cmbTabFile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTabFile.SelectedIndexChanged
        If (cmbTabFile.SelectedIndex > 0) Then
            GetTableColumn(cmbTabFile.SelectedItem.ToString)
        Else
            ClearComboBox(cmbRegionColumn, "Select Column")
        End If
    End Sub

    Private Sub btnRefreshTabFiles_Click(sender As Object, e As EventArgs) Handles btnRefreshTabFiles.Click
        BindMapTable()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        If (cmbTabFile.SelectedIndex > 0) Then
            If (cmbRegionColumn.SelectedIndex > 0) Then
                Try
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                    Application.UseWaitCursor = True
                    Application.DoEvents()
                    IOS.DataLibrary.DataAccessorODBC.KeepConnectionOpen = True
                    Dim tblLayer As MapInfo.Mapping.FeatureLayer = frmMapWindow.MapControl1.Map.Layers(cmbTabFile.SelectedItem.ToString)
                    InsertRegion(cmbRegionColumn.SelectedItem.ToString, tblLayer)
                    regionBasedDT = Nothing
                    BindRegionBasedTag()
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                Finally
                    IOS.DataLibrary.DataAccessorODBC.KeepConnectionOpen = False
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
                End Try
            Else
                SetMessage("Select Region Column.")
            End If
        Else
            SetMessage("Select Tag File.")
        End If
        Application.UseWaitCursor = False
    End Sub

    Private Sub cm_TagManagement_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cm_TagManagement.Opening
        If (gvTagsList.RowCount > 0) Then
            cm_TagManagement.Enabled = True
            tsmi_PreAggregration.Checked = CBool(gvTagsList.GetFocusedRowCellValue("EnablePreAggregation"))
        Else
            cm_TagManagement.Enabled = False
        End If
    End Sub

    Private Sub tsmi_AddTag_Click(sender As Object, e As EventArgs) Handles tsmi_AddTag.Click
        btnTagInsert_Click(sender, e)
    End Sub

    Private Sub tsmi_DeleteTag_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteTag.Click
        Dim gctrl As DevExpress.XtraGrid.GridControl = CType(cm_TagManagement.SourceControl, DevExpress.XtraGrid.GridControl)
        Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = gctrl.MainView
        If Not gv Is Nothing Then
            Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                If ((Environment.UserName = gv.GetFocusedRowCellValue("TagOwner").ToString) Or (configMgr.User.IsPowerUser = True)) Then
                    clsSQLCommands.DeleteTag(connStrIOSServer, CInt(gv.GetFocusedRowCellValue("TagID")))
                    SetMessage("Tag Deleted.")
                    BindTagsGrid()
                Else
                    SetMessage("Tag is owned by " & gv.GetFocusedRowCellValue("TagOwner").ToString & "  and cannot be deleted by " & Environment.UserName & ".")
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            End Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End If
    End Sub

    Private Sub tsmi_RenameTag_Click(sender As Object, e As EventArgs) Handles tsmi_RenameTag.Click
        If (gvTagsList.FocusedRowHandle <> -1) Then
            If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                gvTagsList.OptionsBehavior.Editable = True
                gvTagsList.OptionsBehavior.ReadOnly = False
                oldTagName = gvTagsList.GetFocusedRowCellValue("TagName").ToString
                isTagRename = True
            Else
                SetMessage("You are not owner of this Tag, you can't rename.")
            End If
        End If
    End Sub

    Private Sub tsmi_PreAggregration_Click(sender As Object, e As EventArgs) Handles tsmi_PreAggregration.Click
        Try
            Dim perAggretaion As Boolean = tsmi_PreAggregration.Checked
            If (perAggretaion) Then
                tsmi_PreAggregration.Checked = False
            Else
                tsmi_PreAggregration.Checked = True
            End If
            ChangePreAggregationStatus(tsmi_PreAggregration.Checked)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_TagPaste_Paste_Click(sender As Object, e As EventArgs) Handles tsmi_TagPaste_Paste.Click
        Dim s As String = Clipboard.GetText()                   'Get clipboard data as a string
        Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
        Dim i As Integer
        Dim clipboardmatches As Integer = 0
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            For i = 0 To rows.Length - 1
                'Split row into cells
                Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                Dim objname As String = Nothing
                Dim objtype As String = Nothing

                If bufferCell.Length = 2 Then
                    If bufferCell(0).ToString.Contains(ControlChars.Lf) Then
                        bufferCell(0) = bufferCell(0).ToString.Replace(ControlChars.Lf, "")
                    End If
                    If bufferCell(1).ToString.Contains(ControlChars.Lf) Then
                        bufferCell(1) = bufferCell(1).ToString.Replace(ControlChars.Lf, "")
                    End If
                    objtype = bufferCell(0).ToString.Trim
                    objname = bufferCell(1).ToString.Trim
                    dgv_AddObject(objtype, objname)
                End If
            Next
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_RegionEdit_Click(sender As Object, e As EventArgs) Handles tsmi_RegionEdit.Click
        Try
            If (tlvRegionList.SelectedNode IsNot Nothing) Then
                If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                    tlvRegionList.SelectedNode.BeginEdit()
                Else
                    SetMessage("Not owner")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_RegionDelete_Click(sender As Object, e As EventArgs) Handles tsmi_RegionDelete.Click
        If (gvTagsList.FocusedRowHandle <> -1) Then
            If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                Try
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                    If (tlvRegionList.SelectedNode.Key = "0") Then
                        Dim deletableRows() As DataRow = regionBasedDT.Select("TagDetailsID=0")
                        For Each drDeleting As DataRow In deletableRows
                            drDeleting.Delete()
                        Next
                    Else
                        'Dim cmdText As String = "DELETE FROM [dbo].[IOS_Tags_Details_Region] WHERE TagDetailsID='" & tlvRegionList.SelectedNode.Key & "'"
                        'IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
                        clsSQLCommands.DeleteTagRegionDetail(connStrIOSServer, tlvRegionList.SelectedNode.Key)
                        Dim deletableRows() As DataRow = regionBasedDT.Select("TagDetailsID='" & tlvRegionList.SelectedNode.Key & "'")
                        For Each drDeleting As DataRow In deletableRows
                            drDeleting.Delete()
                        Next
                    End If
                    SetMessage("Deleted successfully")
                    tlvRegionList.SelectedNode.Remove()
                    tlvRegionList.UpdateCurrentView()

                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                End Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Else
                SetMessage("Not owner")
            End If
        End If
    End Sub

    Private Sub tsmi_Edit_Click(sender As Object, e As EventArgs) Handles tsmi_Edit.Click
        If (tagsSubItemToEdit IsNot Nothing) Then
            tagsSubItemToEdit.BeginEdit()
        End If
    End Sub

    Private Sub tsmi_Delete_Click(sender As Object, e As EventArgs) Handles tsmi_Delete.Click
        If (gvTagsList.FocusedRowHandle <> -1) Then
            If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                If (tlvCMParamenter.SelectedNode IsNot Nothing) Then
                    Try
                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                        clsSQLCommands.DeleteTagCMDetail(connStrIOSServer, tlvCMParamenter.SelectedNode.Key)
                        SetMessage("Deleted successfully")
                        Dim rowIndex As String = tlvCMParamenter.SelectedNode.SubItems(0).Key
                        dtParameterData.Rows.RemoveAt(Convert.ToInt32(rowIndex))

                        For index As Integer = tlvCMParamenter.SelectedNode.Index + 1 To tlvCMParamenter.Nodes.Count
                            Dim node As TreeListViewNode = tlvCMParamenter.Nodes(index)
                            If (node IsNot Nothing) Then
                                node.SubItems(0).Key = (Convert.ToInt32(node.SubItems(0).Key) - 1).ToString()
                                Dim comboedit As DevExpress.XtraEditors.ComboBoxEdit = TryCast(node.SubItems(1).Control, DevExpress.XtraEditors.ComboBoxEdit)
                                comboedit.Tag = node.SubItems(0).Key
                                Dim txtValue As DevExpress.XtraEditors.TextEdit = TryCast(node.SubItems(2).Control, DevExpress.XtraEditors.TextEdit)
                                txtValue.Tag = node.SubItems(0).Key
                            End If
                        Next
                        tlvCMParamenter.SelectedNode.Remove()
                        tlvCMParamenter.UpdateCurrentView()
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                    End Try
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
                End If
            End If
        End If
    End Sub

    Private Sub tlvRegionList_AfterLabelEdit(sender As Object, e As LidorSystems.IntegralUI.ObjectEditEventArgs) Handles tlvRegionList.AfterLabelEdit
        Dim tlvRegionNode As New LidorSystems.IntegralUI.Lists.TreeListViewSubItem
        tlvRegionNode = e.Object
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (e.Label IsNot Nothing AndAlso tlvRegionNode IsNot Nothing) Then
                If Not (e.Label = "") AndAlso Not e.Label = tlvRegionNode.Text Then
                    If (gvTagsList.FocusedRowHandle <> -1) Then
                        If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                            tlvRegionList.SuspendLayout()
                            Dim newRegion As String = e.Label.Trim()
                            If (tlvRegionNode.Key = "0") Then
                                RegionDT_Update(tlvRegionNode.Text, e.Label, tlvRegionNode.Key, "Column")
                                tlvRegionNode.Text = newRegion
                                SetMessage("Region successfully edited.")
                            Else
                                'Dim sqlRenameRegion As String = "update IOS_Tags_Details_Region set RegionName='" & newRegion & "' where TagDetailsID='" & tlvRegionNode.Key & "';"
                                Dim result As Integer = clsSQLCommands.RenameTagRegionDetail(connStrIOSServer, newRegion, tlvRegionNode.Key) 'IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sqlRenameRegion)
                                If (result > 0) Then
                                    RegionDT_Update(tlvRegionNode.Text, e.Label, tlvRegionNode.Key, "ID")
                                    tlvRegionNode.Text = newRegion
                                    SetMessage("Region successfully edited.")
                                End If
                            End If
                            tlvRegionList.ResumeUpdate()
                            tlvRegionList.Refresh()
                        Else
                            SetMessage("Not owner")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        e.Cancel = True
    End Sub

    Private Sub tlvRegionList_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvRegionList.SubItemSelectionChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Dim currentzoom As Distance
            currentzoom = New Distance(frmMapWindow.MapControl1.Map.Zoom.Value, DistanceUnit.Kilometer)

            If (tlvRegionList.SelectedNode IsNot Nothing) Then
                Dim RegionName As String = tlvRegionList.SelectedNode.SubItems(0).Text
                Dim regionPoly As String = tlvRegionList.SelectedNode.SubItems(1).Text
                Dim geometry As Geometry = IOSGeomatryHelper.GetGeomatryFromGeomatryString(regionPoly, csysWGS84)
                Dim featureGeometry As FeatureGeometry = TryCast(geometry, FeatureGeometry)
                If ((featureGeometry.Type = GeometryType.MultiPolygon) Or (featureGeometry.Type = GeometryType.Polygon)) Then
                    Dim cStyle As MapInfo.Styles.CompositeStyle = Nothing
                    cStyle = New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.AreaStyle(
           New MapInfo.Styles.SimpleLineStyle(
            New MapInfo.Styles.LineWidth(3, MapInfo.Styles.LineWidthUnit.Pixel), 3, System.Drawing.Color.Black),
            New MapInfo.Styles.SimpleInterior(1, System.Drawing.Color.Black, Color.White, True)))

                    Dim featureLayer As Feature = New Feature(featureGeometry, cStyle)
                    frmMapWindow.objMapHelper.RemoveLayer(frmMapWindow.tempRegionTagDT)
                    frmMapWindow.AddTempTabletoMap(frmMapWindow.tempRegionTagDT, featureLayer)

                    MapInfo.Engine.Session.Current.MapFactory(0).SetView(featureGeometry.Envelope.GeometricCentroid, featureGeometry.CoordSys, currentzoom)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbTagType_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles cmbTagType.CloseUp
        If cmbTagType.Properties.Items.Where(Function(x) x.CheckState = CheckState.Checked).Count = 0 Then
            cmbTagType.Properties.Items(0).CheckState = CheckState.Checked
        End If
        xtcTagManager.Enabled = False
        If Not (cmbObjectType.SelectedIndex = 0) Then
            BindTagsGrid()
        Else
            IOSDevExpressGrid.ClearGrid(gcTagsList)
            gvTags.Columns.Clear()
        End If
        EnableDisableAddTagBtn()
    End Sub

    Private Sub CheckedComboBoxEdit1_Popup(sender As Object, e As EventArgs) Handles cmbTagType.Popup
        Dim list As DevExpress.XtraEditors.CheckedListBoxControl
        list = TryCast(sender, DevExpress.Utils.Win.IPopupControl).PopupWindow.Controls.OfType(Of DevExpress.XtraEditors.PopupContainerControl)().First().Controls.OfType(Of DevExpress.XtraEditors.CheckedListBoxControl)().First()
        AddHandler list.ItemCheck, AddressOf list_ItemCheck
    End Sub

    Private Sub list_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs)
        Try
            If e.State = CheckState.Checked Then
                Dim list As DevExpress.XtraEditors.CheckedListBoxControl = TryCast(sender, DevExpress.XtraEditors.CheckedListBoxControl)
                Dim items As New List(Of DevExpress.XtraEditors.Controls.CheckedListBoxItem)
                For Each index As Integer In list.CheckedIndices
                    If index = e.Index Then Continue For
                    items.Add(list.Items(index))
                Next

                For Each item As DevExpress.XtraEditors.Controls.CheckedListBoxItem In items
                    item.CheckState = CheckState.Unchecked
                Next
                cmbTagType.ClosePopup()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

#End Region

#Region "Button Events"

    Private Sub btnTagInsert_Click(sender As Object, e As EventArgs) Handles btnTagInsert.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (Not cmbTechnology.SelectedIndex = 0 AndAlso Not cmbObjectType.SelectedIndex = 0) Then
                If (cmbTagType.Properties.Items.GetCheckedValues().Count > 0) AndAlso cmbTagType.Properties.Items.First(Function(x) x.Tag = 0).CheckState = CheckState.Unchecked Then
                    Dim tagInsertDialog As New dlgTagInsert()
                    tagInsertDialog.ShowDialog()
                    If (tagInsertDialog.IsValid) Then
                        Dim SqlChk As String = "SELECT * FROM [dbo].[IOS_Tags] WHERE [TagName] = '" & tagInsertDialog.TagName & "' AND [TagType] = '" & cmbTagType.Properties.GetCheckedItems().ToString & "' AND Technology = '" & cmbTechnology.SelectedItem.ToString & "';"

                        Dim dtCount As DataTable = Nothing
                        dtCount = DataAccessorODBC.GetDataTable(connStrIOSServer, SqlChk)
                        Dim iChk As Integer = 0
                        If dtCount IsNot Nothing Then iChk = dtCount.Rows.Count

                        If iChk >= 1 Then
                            SetMessage("Tag Already Exists")
                        Else
                            clsSQLCommands.InsertTag(connStrIOSServer, tagInsertDialog.TagName, cmbTechnology.SelectedItem.ToString, cmbTagType.Properties.Items.First(Function(x) x.CheckState = CheckState.Checked).Value.ToString, tagInsertDialog.TagDescription, cmbObjectType.SelectedItem.ToString, tagInsertDialog.TagIsPrivate)
                            BindTagsGrid()
                            SetMessage("New Tag Successfully Inserted")
                        End If
                    Else
                        'SetMessage("Tag not inserted, cancel by user.")
                    End If
                Else
                    SetMessage("Please Select Tag Type")
                End If
            Else
                SetMessage("Please Select Technology Or Object Name")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnTagSave_Click(sender As Object, e As EventArgs) Handles btnTagSave.Click
        Tag_Save_3G()
        btnTagSave.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.UserLookAndFeel.DefaultSkinName)
    End Sub

    Private Sub btnLoadParameter_Click(sender As Object, e As EventArgs) Handles btnLoadParameter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (Not cmbTechnology.SelectedIndex = 0 AndAlso Not cmbObjectType.SelectedIndex = 0) Then
                Dim pmData As DataTable = GetParametersManagerData(cmbTechnology.Text.Trim, cmbObjectType.Text.Trim)
                If (pmData IsNot Nothing AndAlso pmData.Rows.Count > 0) Then
                    BindParameterData(pmData)
                    Dim dtObject As DataTable = pmData.DefaultView.ToTable(True, "Managed_object")
                    BindDevExComboBoxWithValueMember(cmbFilterOnObject, dtObject, "Managed_object", "Managed_object", "No Filter")
                Else
                    SetMessage("No Parameter Data Found")
                End If
            Else
                SetMessage("Select Technology Or Object.")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnInsertCMBased_Click(sender As Object, e As EventArgs) Handles btnInsertCMBased.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (gvTagsList.FocusedRowHandle <> -1) Then
                isTagEdit = True
                Dim query As String = ""
                If (dtParameterData.Rows.Count > 0 AndAlso dtParameterData IsNot Nothing) Then
                    Dim insertRows() As DataRow = dtParameterData.Select(String.Format("IsNew='{0}'", True))
                    If (insertRows.Length > 0) Then
                        For Each inserRow As DataRow In insertRows
                            query += clsSQLCommands.GetInsertTagDetailsCMSQL(gvTagsList.GetFocusedRowCellValue("TagID").ToString, inserRow("ParameterID").ToString, inserRow("ParameterName").ToString, inserRow("ParameterOperator").ToString, inserRow("ParameterValue").ToString)
                        Next
                    End If

                    Dim updatedRows() As DataRow = dtParameterData.Select(String.Format("IsNew='{0}' AND IsUpdate='{1}'", False, True))
                    If (updatedRows.Length > 0) Then
                        For Each updateRow As DataRow In updatedRows
                            query += clsSQLCommands.GetUpdateTagDetailsCMSQL(gvTagsList.GetFocusedRowCellValue("TagID").ToString, updateRow("ParameterID").ToString, updateRow("ParameterName").ToString, updateRow("ParameterOperator").ToString, updateRow("ParameterValue").ToString, updateRow("TagDetailsID").ToString)
                        Next
                    End If
                End If
                If Not (String.IsNullOrEmpty(query)) Then
                    Dim result As Integer = DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, query)
                    If (result > 0) Then
                        If (gvTagsList.FocusedRowHandle <> -1) Then
                            dtParameterData = GetCMBasedParameterTag(gvTagsList.GetFocusedRowCellValue("TagID").ToString)
                        End If
                        btnInsertCMBased.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.UserLookAndFeel.DefaultSkinName)
                        SetMessage("Committed Successfully")
                    Else
                        SetMessage("Not able to commit data")
                    End If
                Else
                    SetMessage("Did Not detect any change in data")
                End If
            Else
                SetMessage("Select any operator.")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Helper Methods"

    Public Sub SetRectRegionSelection(ByVal geometrySelection As MapInfo.Geometry.FeatureGeometry)
        Try
            If (geometrySelection IsNot Nothing) Then
                Dim wktValue As String = GetGeomatryStringFromGeomatry(geometrySelection)
                Dim drRegion As DataRow = regionBasedDT.NewRow
                drRegion("TagDetailsID") = "0"
                drRegion("RegionName") = txtRegionName.Text.Trim()
                drRegion("RegionPoly") = wktValue
                regionBasedDT.Rows.Add(drRegion)
                If (tlvRegionList.Nodes.Count = 0) Then
                    Dim tlv_col1 As TreeListViewColumn = New TreeListViewColumn()
                    tlv_col1.HeaderText = "RegionName"
                    tlvRegionList.Columns.Add(tlv_col1)
                    Dim tlv_col2 As TreeListViewColumn = New TreeListViewColumn()
                    tlv_col2.HeaderText = "RegionPoly"
                    tlvRegionList.Columns.Add(tlv_col2)
                End If
                Dim newnode As TreeListViewNode = New TreeListViewNode(0)
                newnode.Key = 0
                Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(txtRegionName.Text.Trim())
                si1.Key = 0
                newnode.SubItems.Add(si1)
                Dim si2 As TreeListViewSubItem = New TreeListViewSubItem(wktValue)
                si2.Key = 0
                newnode.SubItems.Add(si2)
                tlvRegionList.Nodes.Add(newnode)
                tlvRegionList.ResumeUpdate()
                tlvRegionList.Refresh()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub SetAutoFiltersOnGrid(ByRef gcObject As DevExpress.XtraGrid.GridControl, ByRef gvObject As DevExpress.XtraGrid.Views.Grid.GridView)
        Dim totalColumns As Integer = gvObject.Columns.Count - 2
        Dim frmTagWidth As Integer = gcObject.Width - 55
        If (totalColumns > 0) Then
            frmTagWidth = frmTagWidth / totalColumns
            For k = 2 To (totalColumns - 1) + 2
                gvObject.Columns(k).Width = frmTagWidth
            Next
        End If
    End Sub

    Sub BindTechnology()
        Try
            Dim distTech As DataTable = clsSQLCommands.GetDistinctTechnology(connStrIOSServer)
            BindDevExComboBoxWithValueMember(cmbTechnology, distTech, "tech", "tech", "Select Technology")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub Tag_Save_3G()
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcDataAdapter = Nothing
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim sql As String = "SELECT * FROM IOS_Tags_Details_List WHERE TagID = " & CInt(gvTagsList.GetFocusedRowCellValue("TagID"))
            Dim table As DataTable = CType(gcTags.DataSource, DataTable)

            Dim updateRows As New ArrayList
            Dim insertRows As New ArrayList
            Dim deletedRows As New ArrayList

            For Each row As DataRow In table.Rows
                If row.RowState <> DataRowState.Unchanged Then
                    Select Case row.RowState
                        Case DataRowState.Added
                            insertRows.Add(row)
                        Case DataRowState.Deleted
                            deletedRows.Add(row)
                        Case DataRowState.Modified
                            updateRows.Add(row)
                    End Select
                End If
            Next
            If updateRows.Count = 0 AndAlso insertRows.Count = 0 AndAlso deletedRows.Count = 0 Then
                Return
            End If

            If insertRows.Count > 0 Then
                cnQODBC = New System.Data.Odbc.OdbcConnection(connStrIOSServer)
                cnQODBC.ConnectionTimeout = 5
                cnQODBC.Open()
                daQODBC = New System.Data.Odbc.OdbcDataAdapter(sql, cnQODBC)
                Dim builder As Odbc.OdbcCommandBuilder = New Odbc.OdbcCommandBuilder(daQODBC)
                Dim rows() As DataRow =
                CType(insertRows.ToArray(GetType(DataRow)), DataRow())
                daQODBC.ContinueUpdateOnError = True
                daQODBC.Update(rows)
            End If

            If updateRows.Count > 0 Then
                Dim sqlUpdate As String
                For Each insertRow As DataRow In updateRows
                    sqlUpdate = "update [dbo].[IOS_Tags_Details_List] set [TagID]='" & insertRow(1).ToString & "',[ObjectID]='" & insertRow(2).ToString & "',[ObjectName]='" & insertRow(3).ToString & "'"
                    sqlUpdate += " where TagDetailsID='" & insertRow(0).ToString & "'"
                    DataAccessorODBC.ExecuteScalar(connStrIOSServer, sqlUpdate)
                Next
            End If

            If deletedRows.Count > 0 Then
                Dim sqlDelete As String
                For Each deleteRow As DataRow In deletedRows
                    sqlDelete = "Delete From [dbo].[IOS_Tags_Details_List] where TagDetailsID='" & deleteRow(0, DataRowVersion.Original).ToString() & "'"
                    DataAccessorODBC.ExecuteScalar(connStrIOSServer, sqlDelete)
                Next
            End If

            ' close...
            daQODBC.Dispose()
            cnQODBC.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            If Not daQODBC Is Nothing Then
                daQODBC.Dispose()
            End If
            If Not cnQODBC Is Nothing Then
                cnQODBC.Dispose()
            End If

            If (gvTagsList.FocusedRowHandle <> -1) Then
                Dim tagType As String = gvTagsList.GetFocusedRowCellValue("TagType").ToString
                xtcTagManager.Enabled = True
                EnableTab(tagType)
            End If
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Public Sub BindTagsGrid()
        cm_TagManagement.Hide()
        Dim dt_tags As DataTable = Nothing
        Try
            RemoveHandler gvTagsList.FocusedRowChanged, AddressOf gvTagsList_FocusedRowChanged
            RemoveHandler gvTagsList.CellValueChanged, AddressOf gvTagsList_CellValueChanged
            If (Not cmbTechnology.SelectedIndex = 0 AndAlso Not cmbObjectType.SelectedIndex = 0) Then
                Dim flg As Boolean = False

                If (cmbTagType.Properties.Items.GetCheckedValues().Count > 0) AndAlso cmbTagType.Properties.Items.First(Function(x) x.Tag = 0).CheckState = CheckState.Unchecked Then
                    flg = False
                Else
                    flg = True
                End If

                dt_tags = clsSQLCommands.GetTagList(connStrIOSServer, flg, cmbTechnology.SelectedItem.ToString, cmbObjectType.SelectedItem.ToString, cmbTagType.Properties.Items.First(Function(x) x.CheckState = CheckState.Checked).Value.ToString) 'DataAccessorODBC.GetDataTable(connStrIOSServer, sqlCommand)
                dtTags = dt_tags

                Dim columnsToHide() As String = {"TagID", "TagOwner", "TagType"}
                IOSDevExpressGrid.PopulateDataInGrid(gcTagsList, gvTagsList, dt_tags, "ALL", columnsToHide, "TagName")

                UpdateTagsGridPreAggregateColumn()

                gcTags.DataSource = Nothing
                gvTags.Columns.Clear()
                gcTags.Refresh()
            Else
                IOSDevExpressGrid.ClearGrid(gcTagsList)
            End If

            If gvTags.RowCount = 1 Then
                gvTagsList_FocusedRowChanged(Nothing, Nothing)
            End If
            AddHandler gvTagsList.FocusedRowChanged, AddressOf gvTagsList_FocusedRowChanged
            AddHandler gvTagsList.CellValueChanged, AddressOf gvTagsList_CellValueChanged
        Catch ex As Exception
        Finally
            If Not dt_tags Is Nothing Then
                dt_tags.Dispose()
                dt_tags = Nothing
            End If
        End Try
    End Sub

    Private Sub UpdateTagsGridPreAggregateColumn()
        If gvTagsList.Columns.Count > 0 Then
            Dim checkEdit As RepositoryItemCheckEdit = TryCast(gcTagsList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
            checkEdit.PictureChecked = imgList.Images(0)
            checkEdit.PictureUnchecked = imgList.Images(1)
            checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
            gvTagsList.Columns("EnablePreAggregation").ColumnEdit = checkEdit
            gvTagsList.Columns("EnablePreAggregation").Caption = "PreAggregation"
            gvTagsList.Columns("EnablePreAggregation").Width = 90
        End If
    End Sub

    Public Sub ConfigurTagManagerForm(ByVal frmName As String)
        Try
            Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
            If Not form Is Nothing Then
                Dim counter As Integer = 0
                ConfigurForm(Me, frmName, counter)

                Dim winCtrl As EntityModel.Control = Nothing
                Dim formControls As List(Of Object) = New List(Of Object) From {
                    tsmi_AddTag, tsmi_DeleteTag, tsmi_RenameTag, tsmi_PreAggregration, tsmi_TagPaste_Paste, tsmi_RegionEdit, tsmi_RegionDelete, tsmi_Edit, tsmi_Delete
                }

                For Each frmControl As Object In formControls
                    winCtrl = form.FindControlByName(frmControl.Name)
                    If Not winCtrl Is Nothing Then
                        frmControl.Enabled = winCtrl.DefaultEnable
                        frmControl.Visible = winCtrl.DefaultVisible
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Tags_Fetch()
        Try
            'Dim sql As String = Nothing
            Dim connstring As String = Nothing
            Dim dt_tags As DataTable = Nothing
            'sql = "SELECT TagID, TagName, TagDescription, TagOwner FROM IOS_Tags WHERE TagOwner = '" & Environment.UserName.ToString & "'"
            dtTags = clsSQLCommands.GetTagListByOwner(connStrIOSServer) 'DataAccessorODBC.GetDataTable(connStrIOSServer, sql)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Function Tag_GetTable()
        Return dtTags
    End Function

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Function GetDTParameterDataTable() As DataTable
        Try
            If (Me.dtParameterData Is Nothing) Then
                dtParameterData = New DataTable()
                dtParameterData.Columns.Add("TagDetailsID")
                dtParameterData.Columns.Add("ParameterID")
                dtParameterData.Columns.Add("ParameterName")
                Dim col As New DataColumn("ParameterOperator")
                col.DefaultValue = "="
                dtParameterData.Columns.Add(col)
                dtParameterData.Columns.Add("ParameterValue")
                Dim IsNew As New DataColumn("IsNew")
                IsNew.DataType = GetType(Boolean)
                IsNew.DefaultValue = False
                dtParameterData.Columns.Add(IsNew)
                Dim IsUpdate As New DataColumn("IsUpdate")
                IsNew.DataType = GetType(Boolean)
                IsNew.DefaultValue = False
                dtParameterData.Columns.Add(IsUpdate)
            End If
        Catch ex As Exception
        End Try
        Return dtParameterData
    End Function

    Function GetCMBasedParameterTag(ByVal tagId As String) As DataTable
        Try
            Dim cmdText As String = "SELECT TagDetailsID,ParameterID,ParameterName,ParameterOperator,ParameterValue FROM IOS_Tags_Details_CM where TagID='" & tagId & "'"
            Dim dtParameter As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
            If (dtParameter IsNot Nothing AndAlso dtParameter.Rows.Count > 0) Then
                dtParameterData = dtParameter.Copy()
                Dim IsNewRow As New DataColumn("IsNew", Type.GetType("System.Boolean"))
                IsNewRow.DefaultValue = False
                dtParameterData.Columns.Add(IsNewRow)
                Dim isUpdated As New DataColumn("IsUpdate", Type.GetType("System.Boolean"))
                isUpdated.DefaultValue = False
                dtParameterData.Columns.Add(isUpdated)
                Return dtParameterData
            End If
        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Private Function GetParametersManagerData(ByVal tech As String, ByVal objectName As String) As DataTable
        Dim temMangData As DataTable = BindParametersManagerData()
        Dim tempDataRow As DataRow()
        Dim tempManagerTb As DataTable = Nothing
        If (temMangData.Rows.Count > 0) Then
            tempDataRow = temMangData.Select("techn='" & tech & "'")
            If (tempDataRow.Count > 0) Then
                tempManagerTb = tempDataRow.CopyToDataTable()
            End If
            tempManagerTb.Columns.Remove("techn")
        End If
        Return tempManagerTb
    End Function

    Private Function BindParametersManagerData() As DataTable
        If (cmbTechnology.SelectedIndex > 0) Then
            If (dtTemplateManagerData Is Nothing) Then
                Dim cmdText As String = "SELECT ID, P_name,DB_Column_Name,Managed_object,Range_Step,Conv_Int_Val,LTRIM(RTRIM(techn)) as techn FROM dbo.qry_IOS_Parameters_CMTAGS  where techn='" & cmbTechnology.Text.Trim & "'"
                dtTemplateManagerData = DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
            End If
        Else
            SetMessage("Select Technology")
        End If
        Return dtTemplateManagerData
    End Function

    Private Function BindOperatorCmbo(ByVal rowIndex As String, ByVal selectedValue As String) As DevExpress.XtraEditors.ComboBoxEdit
        Dim cmbOprator As New DevExpress.XtraEditors.ComboBoxEdit()
        cmbOprator.Tag = rowIndex
        cmbOprator.Properties.Items.Add("=")
        cmbOprator.Properties.Items.Add("<")
        cmbOprator.Properties.Items.Add(">")
        cmbOprator.Properties.Items.Add("<>")
        cmbOprator.Properties.Items.Add("<=")
        cmbOprator.Properties.Items.Add(">=")
        cmbOprator.ForeColor = Color.DarkGray
        cmbOprator.Size = New System.Drawing.Size(82, 16)

        Dim opratorItem As New TreeListViewSubItem(selectedValue)
        opratorItem.Key = rowIndex
        opratorItem.Text = selectedValue
        Dim a = From w In cmbOprator.Properties.Items.Cast(Of String)()
                Where w = selectedValue
                Select w
        For Each Item As String In a
            cmbOprator.SelectedIndex = cmbOprator.Properties.Items.IndexOf(Item)
        Next
        opratorItem.Control = cmbOprator
        AddHandler cmbOprator.SelectedIndexChanged, AddressOf cmbOprator_SelectedIndexChanged
        Return cmbOprator
    End Function

    Private Sub cmbOprator_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cmbOperater As DevExpress.XtraEditors.ComboBoxEdit = CType(sender, DevExpress.XtraEditors.ComboBoxEdit)
        Dim rowIndex As Integer = cmbOperater.Tag
        dtParameterData.Rows(rowIndex)("ParameterOperator") = cmbOperater.SelectedItem.ToString
        dtParameterData.Rows(rowIndex)("IsUpdate") = True
        btnInsertCMBased.LookAndFeel.SetSkinStyle("Blueprint")
    End Sub

    Sub ClearComboBox(ByRef control As DevExpress.XtraEditors.ComboBoxEdit, ByVal firstItem As String)
        control.SuspendLayout()
        control.Properties.Items.Insert(0, firstItem)
        control.SelectedIndex = 0
        control.Refresh()
        control.ResumeLayout()
    End Sub

    Private Sub EnableDisableAddTagBtn()
        If (Not cmbTechnology.SelectedIndex = 0 AndAlso Not cmbObjectType.SelectedIndex = 0 AndAlso (cmbTagType.Properties.Items.Where(Function(x) x.CheckState = CheckState.Checked And x.Tag > 0).Count > 0)) Then
            btnTagInsert.Enabled = True
        Else
            btnTagInsert.Enabled = False
        End If
    End Sub

    Private Sub EnableTab(ByVal tagType As String)
        xtpListManager.PageEnabled = False
        xtpCMBased.PageEnabled = False
        xtpRegionBased.PageEnabled = False
        If (tagType = "Static List") Then
            xtpListManager.PageEnabled = True
            btnTagSave.Enabled = True
            gcTags.Enabled = True
            xtcTagManager.SelectedTabPageIndex = 0
        ElseIf (tagType = "Admin List") Then
            xtpListManager.PageEnabled = True
            btnTagSave.Enabled = False
            gcTags.Enabled = False
            xtcTagManager.SelectedTabPageIndex = 0
        ElseIf (tagType = "CM Based List") Then
            xtpCMBased.PageEnabled = True
            xtcTagManager.SelectedTabPageIndex = 1
            If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                btnInsertCMBased.Enabled = True
                tlvCMParamenter.ContextMenuStrip = cms_CMParameter
                tlvCMParamenter.Enabled = True
            Else
                btnInsertCMBased.Enabled = False
                tlvCMParamenter.ContextMenuStrip = Nothing
                tlvCMParamenter.Enabled = False
            End If
        ElseIf (tagType = "Region Based List") Then
            xtpRegionBased.PageEnabled = True
            xtcTagManager.SelectedTabPageIndex = 2
            If (Environment.UserName = gvTagsList.GetFocusedRowCellValue("TagOwner").ToString) Then
                tlvRegionList.ContextMenuStrip = cms_RegionBase
                tlvRegionList.Enabled = True
                togBtnDraw.Enabled = True
                btnManualCommit.Enabled = True
                btnImport.Enabled = True
            Else
                tlvRegionList.ContextMenuStrip = Nothing
                tlvRegionList.Enabled = False
                togBtnDraw.Enabled = False
                btnManualCommit.Enabled = False
                btnImport.Enabled = False
            End If
        End If
    End Sub

    Sub BindParameterData(ByRef pData As DataTable)
        Dim selectStatement As String = String.Empty
        If (txtSearchLongName.Text.Trim.Length >= 3) Then
            selectStatement += "P_name LIKE '%" & txtSearchLongName.Text.Trim & "%' "
        End If
        If (cmbFilterOnObject.SelectedIndex > 0) Then
            If (String.IsNullOrEmpty(selectStatement)) Then
                selectStatement += " Managed_object='" & cmbFilterOnObject.SelectedItem.ToString.Trim & "'"
            Else
                selectStatement += " AND Managed_object='" & cmbFilterOnObject.SelectedItem.ToString.Trim & "'"
            End If
        End If
        Dim tempTable As DataTable = Nothing
        If (String.IsNullOrEmpty(selectStatement)) Then
            tempTable = pData
        Else
            Dim dv As DataView = New DataView(pData, selectStatement, "", DataViewRowState.CurrentRows)
            tempTable = dv.ToTable()
        End If
        tlvParameter.Nodes.Clear()
        For Each Item As DataRow In tempTable.Rows
            Dim node As New TreeListViewNode()
            For Each Item1 As DataColumn In tempTable.Columns
                Dim s As String = String.Empty
                Try
                    s = Convert.ToString(Item(Item1))
                Catch ex As Exception
                    s = ""
                End Try

                Dim nodeItem As New TreeListViewSubItem(s)
                node.SubItems.Add(nodeItem)
            Next
            tlvParameter.Nodes.Add(node)
        Next
        tlvParameter.UpdateCurrentView()
        For Each col As TreeListViewColumn In tlvParameter.Columns
            tlvParameter.AutoSizeColumn(col)
        Next
        Me.tlvParameter.ResumeUpdate()
    End Sub

    Private Function BindValueTextBox(ByVal rowIndex As String, ByVal value As String) As DevExpress.XtraEditors.TextEdit
        Dim txtBox As New DevExpress.XtraEditors.TextEdit()
        txtBox.Tag = rowIndex
        txtBox.Text = value
        txtBox.ForeColor = Color.DarkGray
        txtBox.Size = New System.Drawing.Size(82, 16)
        AddHandler txtBox.TextChanged, AddressOf txtbox_TextChanged
        Return txtBox
    End Function

    Private Function BindRegionBasedData() As DataTable
        If (regionBasedDT Is Nothing) Then
            If (gvTagsList.FocusedRowHandle <> -1) Then
                Dim cmdText As String = "SELECT [TagDetailsID],[RegionName],CAST(RegionPoly as Geometry).STAsText() as RegionPoly FROM [dbo].[IOS_Tags_Details_Region] where TagID = " & CInt(gvTagsList.GetFocusedRowCellValue("TagID"))
                regionBasedDT = DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
            End If
        End If
        Return regionBasedDT
    End Function

    Private Sub AddParametersNode(ByRef treenode As TreeListViewNode, ByVal parameterName As String)
        dtParameterData = GetDTParameterDataTable()
        Dim dr As DataRow = dtParameterData.NewRow()
        dr("ParameterID") = treenode.Tag
        dr("ParameterName") = parameterName
        dr("ParameterOperator") = "="
        dr("ParameterValue") = ""
        dr("IsNew") = True
        dr("IsUpdate") = False
        dtParameterData.Rows.Add(dr)
        Dim rowIndex As Integer = dtParameterData.Rows.IndexOf(dr)
        Dim fItem As New TreeListViewSubItem(parameterName)
        fItem.Key = rowIndex
        treenode.SubItems.Add(fItem)

        Dim opratorItem As New TreeListViewSubItem()
        opratorItem.Control = BindOperatorCmbo(rowIndex, "=")
        treenode.SubItems.Add(opratorItem)

        Dim sItem As New TreeListViewSubItem()
        sItem.Key = rowIndex
        sItem.Control = BindValueTextBox(rowIndex, "")
        treenode.SubItems.Add(sItem)
    End Sub

    Private Sub RegionDT_DeleteUseLessRegion()
        Dim deletableRows() As DataRow = regionBasedDT.Select("TagDetailsID=0")
        For Each drDeleting As DataRow In deletableRows
            drDeleting.Delete()
        Next
        BindRegionBasedTag()
    End Sub

    Private Sub BindRegionBasedTag()
        Try
            If (gvTagsList.FocusedRowHandle <> -1) Then
                tlvRegionList.SuspendLayout()
                tlvRegionList.Columns.Clear()
                tlvRegionList.Nodes.Clear()
                regionBasedDT = BindRegionBasedData()
                If (regionBasedDT IsNot Nothing AndAlso regionBasedDT.Rows.Count > 0) Then
                    tlvRegionList.Columns.Clear()
                    tlvRegionList.Nodes.Clear()

                    Dim tlv_col1 As TreeListViewColumn = New TreeListViewColumn()
                    tlv_col1.HeaderText = "RegionName"
                    tlvRegionList.Columns.Add(tlv_col1)
                    Dim tlv_col2 As TreeListViewColumn = New TreeListViewColumn()
                    tlv_col2.HeaderText = "RegionPoly"
                    tlvRegionList.Columns.Add(tlv_col2)
                    For Each drow As DataRow In regionBasedDT.Rows
                        Dim newnode As TreeListViewNode = New TreeListViewNode(drow(0).ToString)
                        newnode.Key = drow(0).ToString
                        Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(drow(1).ToString)
                        si1.Key = drow(0).ToString
                        newnode.SubItems.Add(si1)
                        Dim si2 As TreeListViewSubItem = New TreeListViewSubItem(drow(2).ToString)
                        si2.Key = drow(0).ToString
                        newnode.SubItems.Add(si2)
                        tlvRegionList.Nodes.Add(newnode)
                    Next
                    For Each col As TreeListViewColumn In tlvRegionList.Columns
                        tlvRegionList.AutoSizeColumn(col)
                    Next
                    tlvRegionList.ResumeUpdate()
                    tlvRegionList.Refresh()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function IsRegionValid(ByVal regionName As String) As Boolean
        Dim sqlCommand As String = Nothing
        sqlCommand = "Select * from IOS_Tags_Details_Region WHERE [RegionName]='" & regionName & "'"
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, sqlCommand)
        If (dt.Rows.Count > 0) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsRegionValidFromList(ByVal regionName As String) As Boolean
        Dim isExist As Boolean = False
        For Each node As TreeListViewNode In tlvRegionList.Nodes
            If (node.SubItems(0).Text.ToUpper = regionName.ToUpper) Then
                isExist = True
                Exit For
            End If
        Next
        Return isExist
    End Function

    Sub RegionBasedTab_Opening(ByVal selectedTab As DevExpress.XtraTab.XtraTabPage)
        If (selectedTab.Tag = "RegionBased") Then
            regionBasedDT = Nothing
            BindRegionBasedTag()
            If (isRegionTabFirstTime) Then
                BindMapTable()
                isRegionTabFirstTime = False
            End If
        End If
    End Sub

    Private Sub BindMapTable()
        cmbTabFile.Properties.Items.Clear()
        cmbTabFile.Properties.Items.Insert(0, "Select TAB file")
        If (MapInfo.Engine.Session.Current.Catalog IsNot Nothing) Then
            For Each tbl In MapInfo.Engine.Session.Current.Catalog
                If ((Not tbl Is Nothing) AndAlso Not tbl.Alias.StartsWith("Cells_")) Then
                    If tbl.Alias.StartsWith("Quer") = False AndAlso tbl.Alias.StartsWith("Temp_") = False Then
                        If Not (tbl.TableInfo.TableType = TableType.Raster) Then
                            cmbTabFile.Properties.Items.Add(tbl.Alias)
                        End If
                    End If
                End If
            Next
        End If
        cmbTabFile.SelectedIndex = 0
    End Sub

    Sub ListTab_Opening(ByVal selectedTab As DevExpress.XtraTab.XtraTabPage)
        If (selectedTab.Tag = "List") Then
            gcTags.DataSource = Nothing
            'fetch TagContainer stuff
            Dim sqlCommand As String = Nothing
            Dim connstring As String = Nothing
            Dim dt_tagcontainer As DataTable = Nothing
            Try
                sqlCommand = "SELECT * FROM  IOS_Tags_Details_List WHERE TagID = " & CInt(gvTagsList.GetFocusedRowCellValue("TagID"))
                dt_tagcontainer = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, sqlCommand)
                If ((dt_tagcontainer.Rows.Count > 0) AndAlso (dt_tagcontainer IsNot Nothing)) Then
                    gvTags.Columns.Clear()
                    gcTags.DataSource = dt_tagcontainer
                    gvTags.Columns(0).Visible = False
                    gvTags.Columns(1).Visible = False
                Else
                    gcTags.DataSource = Nothing
                    gvTags.Columns.Clear()
                    gcTags.DataSource = dt_tagcontainer
                    gvTags.Columns(0).Visible = False
                    gvTags.Columns(1).Visible = False
                End If
                SetAutoFiltersOnGrid(gcTags, gvTags)
                gcTags.Refresh()
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                If Not dt_tagcontainer Is Nothing Then
                    dt_tagcontainer.Dispose()
                    dt_tagcontainer = Nothing
                End If
            End Try
        Else
            ClearComboBox(cmbFilterOnObject, "No Filter")
            tlvParameter.Nodes.Clear()
        End If
        txtSearchLongName.Text = ""
    End Sub

    Sub CMBasedTab_Opening(ByVal selectedTab As DevExpress.XtraTab.XtraTabPage)
        If (selectedTab.Tag = "CMBased") Then
            isRequestByTab = True
            BindCMBasedParameterTag()
        Else
            ClearComboBox(cmbFilterOnObject, "No Filter")
            tlvParameter.Nodes.Clear()
        End If
    End Sub

    Private Sub BindCMBasedParameterTag()
        Try
            If (gvTagsList.FocusedRowHandle <> -1) Then
                dtParameterData = GetCMBasedParameterTag(CInt(gvTagsList.GetFocusedRowCellValue("TagID")))
                If (dtParameterData IsNot Nothing AndAlso dtParameterData.Rows.Count > 0) Then
                    tlvCMParamenter.Nodes.Clear()
                    For Each row As DataRow In dtParameterData.Rows
                        Dim rowIndex As Integer = dtParameterData.Rows.IndexOf(row)
                        Dim newnode As TreeListViewNode = New TreeListViewNode(row(1).ToString)
                        newnode.Key = row(0).ToString
                        newnode.Tag = row(1).ToString
                        newnode.Text = row(1).ToString()
                        Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(row(2).ToString)
                        si1.Key = rowIndex
                        newnode.SubItems.Add(si1)

                        Dim opratorItem As New TreeListViewSubItem(row(3).ToString)
                        opratorItem.Key = rowIndex
                        opratorItem.Text = row(3).ToString
                        opratorItem.Control = BindOperatorCmbo(rowIndex, row(3).ToString)
                        newnode.SubItems.Add(opratorItem)

                        Dim valueItem As TreeListViewSubItem = New TreeListViewSubItem(row(4).ToString)
                        valueItem.Control = BindValueTextBox(rowIndex, row(4).ToString())
                        newnode.SubItems.Add(valueItem)
                        tlvCMParamenter.Nodes.Add(newnode)
                    Next

                    tlvCMParamenter.UpdateCurrentView()
                    For Each col As TreeListViewColumn In tlvCMParamenter.Columns
                        tlvCMParamenter.AutoSizeColumn(col)
                    Next
                Else
                    tlvCMParamenter.Nodes.Clear()
                    tlvCMParamenter.ContextMenuStrip = Nothing
                End If
                tlvCMParamenter.ResumeLayout()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub InsertRegionFromDT()
        Dim sqlCommand As String = Nothing
        Dim geomColumn As GeometryColumn = Nothing
        Dim commandCounter As Integer = 0
        Dim insertableRows() As DataRow = regionBasedDT.Select("TagDetailsID=0")
        For Each drInserting As DataRow In insertableRows
            commandCounter += 1
            sqlCommand += "DECLARE @geo" & commandCounter & " AS GEOMETRY = GEOMETRY::STGeomFromText('" & drInserting("RegionPoly").ToString & "', 4326);"
            sqlCommand += "Insert into IOS_Tags_Details_Region ([TagID],[RegionName],[RegionPoly])"
            sqlCommand += "VALUES(" & CInt(gvTagsList.GetFocusedRowCellValue("TagID")) & ",'" & drInserting("RegionName").ToString & "',@geo" & commandCounter & ");"
        Next
        If ((sqlCommand IsNot "") AndAlso (sqlCommand IsNot Nothing)) Then
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sqlCommand)
            txtRegionName.Text = ""
            regionBasedDT = Nothing
            BindRegionBasedTag()
            SetMessage("Region successfully inserted.")
        Else
            SetMessage("Region List does not found new region.")
        End If
    End Sub

    Private Sub GetTableColumn(ByVal tableName As String)
        Try
            Dim dataTab As System.Data.DataTable = New System.Data.DataTable
            dataTab.TableName = tableName
            Dim maptbl As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.GetTable(tableName)
            cmbRegionColumn.SuspendLayout()
            cmbRegionColumn.Properties.Items.Clear()
            cmbRegionColumn.Properties.Items.Insert(0, "Select Column")
            cmbRegionColumn.SelectedIndex = 0
            For Each colData As MapInfo.Data.Column In maptbl.TableInfo.Columns
                If Not (colData.Alias.ToUpper() = "OBJ" Or colData.Alias.ToUpper() = "STYLE" Or colData.Alias.ToUpper() = "MI_STYLE") Then
                    cmbRegionColumn.Properties.Items.Add(colData.Alias)
                End If
            Next
            cmbRegionColumn.ResumeLayout()
            cmbRegionColumn.Refresh()
            dataTab.Dispose()
            dataTab = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsertRegion(ByVal regionName As String, ByRef featureLayer As MapInfo.Mapping.FeatureLayer)
        Dim sqlCommand As String = Nothing
        Dim geomColumn As GeometryColumn = Nothing
        Dim commandCounter As Integer = 0
        Dim dataTab As System.Data.DataTable = New System.Data.DataTable(featureLayer.Alias)
        Dim maptbl As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.GetTable(featureLayer.Alias)
        For Each f As Feature In maptbl
            Dim featureGeometry As FeatureGeometry = Nothing
            If f.Geometry IsNot Nothing Then
                featureGeometry = f.Geometry.Copy(csysWGS84)
            End If
            'Dim csysWGS84 As CoordSys = Session.Current.CoordSysFactory.CreateLongLat(157)

            If FeatureGeometry IsNot Nothing Then
                'Test If feature Geometry is Polygon
                If ((featureGeometry.Type = GeometryType.MultiPolygon) Or (featureGeometry.Type = GeometryType.Polygon)) Then
                    Dim wktValue As String = IOSGeomatryHelper.GetGeomatryStringFromGeomatry(featureGeometry)
                    If (wktValue IsNot Nothing) Then
                        commandCounter += 1

                        Dim name As String = f(regionName)
                        sqlCommand += "DECLARE @geo" & commandCounter & " AS GEOMETRY = GEOMETRY::STGeomFromText('" & wktValue & "', 4326);"
                        sqlCommand += "Insert into IOS_Tags_Details_Region ([TagID],[RegionName],[RegionPoly])"
                        sqlCommand += "VALUES(" & CInt(gvTagsList.GetFocusedRowCellValue("TagID")) & ",'" & name & "',@geo" & commandCounter & ");"
                        If (commandCounter >= 1000) Then
                            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sqlCommand)
                            sqlCommand = Nothing
                            commandCounter = 0
                        End If

                    End If
                End If
            End If
        Next
        If ((sqlCommand IsNot "") AndAlso (sqlCommand IsNot Nothing)) Then
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sqlCommand)
            txtRegionName.Text = ""
            SetMessage("Region successfully inserted.")
        Else
            SetMessage("Tab File does not contains any Polygon.")
        End If
    End Sub

    Private Sub ChangePreAggregationStatus(ByVal preAggregationStatus As Boolean)
        If (gvTagsList.FocusedRowHandle <> -1) Then
            Try
                Dim sql As String = "update IOS_Tags set EnablePreAggregation='" & preAggregationStatus & "' where TagID = " & CInt(gvTagsList.GetFocusedRowCellValue("TagID"))
                Dim result As Integer = IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sql)
                If (result > 0) Then
                    UpdateTagsGridPreAggregateColumn()
                    'Dim si3 As TreeListViewSubItem = TryCast(tlvTagsList1.SelectedNode.SubItems(2), TreeListViewSubItem)
                    'si3.StyleFromParent = True
                    'si3.Text = preAggregationStatus
                    'If (preAggregationStatus) Then
                    '    si3.Image = imgList.Images(0)
                    'Else
                    '    si3.Image = imgList.Images(1)
                    'End If
                    SetMessage("Tag Successfully updated.")
                Else
                    SetMessage("Not able to update data")
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub dgv_AddObject(ByVal objtype As String, ByVal objname As String)
        Dim dt As DataTable = CType(gcTags.DataSource, DataTable)
        Dim drow As DataRow = dt.NewRow
        Try
            drow(1) = CInt(gvTagsList.GetFocusedRowCellValue("TagID"))
            drow(2) = objtype
            drow(3) = objname
            dt.Rows.Add(drow)
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub RegionDT_Update(ByVal oldRegion As String, ByVal newRegion As String, ByVal tagDetailsID As String, ByVal updateBy As String)
        Dim updateRows() As DataRow = Nothing
        If (updateBy = "ID") Then
            updateRows = regionBasedDT.Select("TagDetailsID = '" & tagDetailsID & "'")
        ElseIf (updateBy = "Column") Then
            updateRows = regionBasedDT.Select("RegionName = '" & oldRegion & "'")
        End If
        If (updateRows IsNot Nothing) Then
            For Each drUpdating As DataRow In updateRows
                drUpdating("RegionName") = newRegion
            Next
        End If
    End Sub



#End Region

End Class