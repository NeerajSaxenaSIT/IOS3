Imports System.Data.SqlClient
Imports IOS.Library
Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraEditors

Public Class frmCMTemplate

    Private IsPageLoaded As Boolean = False
    Private IsFirstTime As Boolean = False
    Private dtTech As DataTable = Nothing

    Public contextFlag As Boolean = True
    Public dsVenderData As DataSet = Nothing
    Public dsTemplateData As DataSet = Nothing
    Public dtTemplateManagerData As DataTable = Nothing

#Region "Left Region select controls AND tab controls"

    Public Sub BindPMFilterData()
        Dim pmDataMain As DataTable = GetTemplateManagerData(cmbTechnology.SelectedItem.ToString.Trim, cmbVendor.SelectedItem.ToString.Trim(), "EnabledInTemplate")
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTechnology.SelectedIndexChanged
        IsFirstTime = False
        If Not (cmbTechnology.SelectedIndex = 0) Then
            BindVendor()
        Else
            ClearComboBox(cmbVendor, "Select Vendor")
            ClearComboBox(cmbTargetObject, "Object Type")
        End If
        ManageTemplatemanagerOnTechnologyChange()
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            IsFirstTime = False
            ManageTempalgeManaterOnVenderChange()
            BindObjectType()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub vcmbTargetObject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTargetObject.SelectedIndexChanged
        Try
            TreeViewStats.SuspendLayout()
            TreeViewStats.Nodes.Clear()
            TreeViewStats.Refresh()
            TreeViewStats.ResumeLayout()
            If (cmbTargetObject.SelectedIndex > 0) Then
                If Not cmbTargetObject.SelectedItem Is Nothing Then
                    Dim strTech As String = ""
                    Try
                        Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("Technology='" & cmbTechnology.SelectedItem.ToString & "' AND Vendor='" & cmbVendor.SelectedItem.ToString & "' AND Object='" & cmbTargetObject.SelectedItem.ToString & "'")
                        If dr.Count > 0 Then
                            strTech = dr(0)("Tech").ToString
                        End If
                    Catch
                    End Try
                    FillObjectTreeData(TreeViewStats, strTech, cmbTargetObject.SelectedItem.ToString)
                End If
            End If
            TreeViewStats.Name = "TreeViewStats"
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub vtabParameters_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xtcTabParameters.SelectedPageChanged
        If (IsPageLoaded) Then
            HideShowLeftControls()
            IsFirstTime = True
            ManageTempalgeManaterOnVenderChange()
        End If
    End Sub

    Sub HideShowLeftControls()
        txtSearchOuter.Enabled = False
        cmbTargetObject.Enabled = False
        TreeViewStats.Enabled = False
    End Sub

    Sub BindObjectType()
        cmbTargetObject.SuspendLayout()
        cmbTargetObject.Properties.Items.Clear()
        If (cmbTechnology.SelectedIndex > 0 AndAlso cmbVendor.SelectedIndex > 0) Then
            Dim dtobject As DataTable = Nothing
            If (dt_IOS_ObjectConfig IsNot Nothing) Then
                dtobject = New DataView(dt_IOS_ObjectConfig, "Vendor='" & cmbVendor.SelectedItem.ToString & "' and  Technology='" & cmbTechnology.SelectedItem.ToString & "' and " & "TemplateManager=1", "", DataViewRowState.CurrentRows).ToTable(True, "Object")
            End If
            BindDevExComboBoxWithValueMember(cmbTargetObject, dtobject, "Object", "Object", "Object Type")
        Else
            ClearComboBox(cmbTargetObject, "Object Type")
        End If
        cmbTargetObject.Refresh()
        cmbTargetObject.ResumeLayout()
    End Sub

    Private Sub lstOfCategories_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeViewStats.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub TreeViewStats_AfterCheck(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewStats.AfterCheck
        CheckTreeNodeAndCount(e.Node, 0, Nothing)
    End Sub

    Private Function GetTechnologyName(ByVal tech As String, ByVal vendor As String, ByVal returnObjectColumnsName As String) As String
        Dim rows() As DataRow = dt_IOS_ObjectConfig.Select("Vendor='" & vendor & "' AND Technology='" & tech & "' AND ParamHistory=1")
        If (rows.Count > 0) Then
            Return rows(0)(returnObjectColumnsName).ToString
        End If
        Return ""
    End Function

    Private Sub TreeViewStats_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeViewStats.MouseDown
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

    Private Sub txtSearchOuter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LabelControl4.KeyDown
        txtObjectsearch_KeyDown(TreeViewStats, txtSearchOuter.Text, e)
    End Sub

    Private Sub txtSearchOuter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelControl4.TextChanged
        txtObjectSearch_TextChanged(TreeViewStats, txtSearchOuter.Text)
    End Sub

    Private Sub BindTechnology()
        If (dtTech Is Nothing) Then
            If (dt_IOS_ObjectConfig IsNot Nothing) Then
                dtTech = New DataView(dt_IOS_ObjectConfig, "TemplateManager=1", "", DataViewRowState.CurrentRows).ToTable(True, "Technology")
            End If
        End If
        If (dtTech.Rows.Count > 0) Then
            cmbTechnology.Properties.Items.Clear()
            BindDevExComboBoxWithValueMember(cmbTechnology, dtTech, "Technology", "Technology", "Select Technology")
        End If
        ClearComboBox(cmbFilterOnObject, "No Filter")
        ClearComboBox(cmbTemplate, "Select Template")
        ClearComboBox(cmbVendor, "Select Vendor")
        ClearComboBox(cmbTargetObject, "Object Type")
        TreeListView2.Nodes.Clear()
    End Sub

    Private Sub BindVendor()
        Dim dtVendorPH As DataTable = Nothing
        If (dt_IOS_ObjectConfig IsNot Nothing) Then
            dtVendorPH = New DataView(dt_IOS_ObjectConfig, "TemplateManager=1", "", DataViewRowState.CurrentRows).ToTable(True, "Vendor")
        End If
        cmbVendor.Properties.Items.Clear()
        If (dtVendorPH IsNot Nothing AndAlso dtVendorPH.Rows.Count > 0) Then
            BindDevExComboBoxWithValueMember(cmbVendor, dtVendorPH, "Vendor", "Vendor", "Select Vendor")
        End If
    End Sub

#End Region

#Region "Template Manager"

    Private Sub frmParameterManager_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub frmParameterManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.SuspendLayout()
        Me.ResumeLayout()
        Me.WindowState = FormWindowState.Normal
        Me.BringToFront()

        dsVenderData = Nothing
        SetTreeListView1ColumnsWidth()
        TreeListView1.ResumeUpdate()
        IsPageLoaded = True
        IsFirstTime = True
        HideShowLeftControls()
        BindTechnology()
        ConfigurCMTemplateForm("frmCMTemplate")
    End Sub

    Sub ManageTemplatemanagerOnTechnologyChange()
        Me.dsTemplateData = Nothing
        Me.dsVenderData = Nothing
        TreeListView2.Nodes.Clear()
        ClearComboBox(cmbTemplate, "Select Template")
        TreeListView1.Nodes.Clear()
    End Sub

    Private Sub ConfigurCMTemplateForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                 tsmi_OT_Exception, tsmi_ReloadTree, tsmi_OT_MapCell, tsmi_OT_UnCheck, cm_OT_tsmi_CheckChilds, cm_OT_tsmi_CopyToTag, cm_OT_tsmi_paste, cm_OT_tsmi_copy, tsmiSelectedTemplate, tsmiAllTemplates,
                 tsmiCurrentTemplate, tsmiParameterDescTLV1, tsmiComboBoxGroup, tsmiAddExistingGroup, tsmiAddNewGroup, tsmiDeleteGroup, tsmiDelParameter
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

    Sub ManageTempalgeManaterOnVenderChange()
        If (IsFirstTime) Then
            BindTechnology()
            IsFirstTime = False
        End If
        Me.dsVenderData = Nothing
        txtSearchLongName.Text = ""
        If Not (cmbVendor.SelectedIndex = 0) Then
            Dim cmdText As String = "SELECT * FROM dbo.IOS_Parameters_Templates WHERE   LTRIM(RTRIM(Technology))='" + cmbTechnology.SelectedItem.ToString.Trim + "' and Vendor='" + cmbVendor.SelectedItem.ToString.Trim() + "'"
            Dim data As DataSet = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connStrIOSServer, cmdText)
            If (data IsNot Nothing) Then
                If (data.Tables.Count > 0) Then
                    BindDevExComboBoxWithValueMember(cmbTemplate, data.Tables(0), "TemplateID", "TemplateName", "Select Template")
                End If
                Dim pmData As DataTable = GetFilterData(cmbFilterOnObject, "Managed_Object")
                BindParameterData(pmData) 
                Me.dsVenderData = data
                Me.dsVenderData.Tables.Add(pmData.Copy())
            End If
        Else
            ClearComboBox(cmbFilterOnObject, "No Filter")
            ClearComboBox(cmbTargetObject, "Object Type")
            ClearComboBox(cmbTemplate, "Select Template")
            TreeListView2.Nodes.Clear()
            Me.dsVenderData = Nothing
        End If
        TreeListView1.Nodes.Clear()
        btnDeleteTemplate.Enabled = False
        txtSearchLongName.Text = ""
    End Sub

    Private Function GetDistinctFilterData(ByVal SelectedColumn As String, ByVal pmData As DataTable) As DataTable
        Try
            If Not (pmData Is Nothing) Then
                Dim distObject As DataTable = pmData.DefaultView.ToTable(True, SelectedColumn)
                Return distObject
            End If
            Return pmData
        Catch
            Return Nothing
        End Try
    End Function

    Private Function GetFilterData(ByRef vcmbControl As DevExpress.XtraEditors.ComboBoxEdit, ByVal SelectedColumn As String) As DataTable
        Try
            Dim pmData As DataTable = GetTemplateManagerData(cmbTechnology.SelectedItem.ToString.Trim, cmbVendor.SelectedItem.ToString.Trim(), "EnabledInTemplate")
            Dim distObject As DataTable = GetDistinctFilterData(SelectedColumn, pmData)
            BindDevExComboBoxWithValueMember(vcmbControl, distObject, SelectedColumn, SelectedColumn, "No Filter")
            Return pmData
        Catch
            Return Nothing
        End Try
    End Function

    Private Function GetTemplateManagerData(ByVal tech As String, ByVal vendor As String, ByVal columnName As String) As DataTable
        Dim temMangData As DataTable = BindTemplateManagerData()
        Dim tempDataRow As DataRow()
        Dim tempManagerTb As DataTable = Nothing
        If (temMangData.Rows.Count > 0) Then
            If (columnName = "") Then
                tempDataRow = temMangData.Select("Technology='" & tech & "' And Vendor='" & vendor & "' ")
            Else
                tempDataRow = temMangData.Select("Technology='" & tech & "' And Vendor='" & vendor & "' and " & columnName & "=1")
            End If

            If (tempDataRow.Count > 0) Then
                tempManagerTb = tempDataRow.CopyToDataTable()
            End If
            If Not (tempManagerTb Is Nothing) Then
                tempManagerTb.Columns.Remove("Technology")
                tempManagerTb.Columns.Remove("Vendor")
                tempManagerTb.Columns.Remove("techn")
                tempManagerTb.Columns.Remove("NE_release")
                tempManagerTb.Columns.Remove("EnabledInTemplate")
                tempManagerTb.Columns.Remove("EnabledInCategory")
            End If
        End If
        Return tempManagerTb
    End Function

    Private Function BindTemplateManagerData() As DataTable
        If (dtTemplateManagerData Is Nothing) Then
            Dim cmdText As String = "SELECT ID, P_name,P_abbr_name,Managed_object,Range_Step,Conv_Int_Val,LTRIM(RTRIM(techn)) as techn,LTRIM(RTRIM(NE_release)) as NE_release,EnabledInTemplate,EnabledInCategory,Technology,Vendor FROM dbo.qry_IOS_Parameters where EnabledInTemplate = 1 ORDER BY Techn, P_Abbr_Name"
            dtTemplateManagerData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
        End If
        Return dtTemplateManagerData
    End Function

    Sub SetTreeListView1ColumnsWidth()
        If (TreeListView1.Columns.Count > 2) Then
            TreeListView1.Columns(1).FixedWidth = False
            TreeListView1.Columns(1).Width = 82
            TreeListView1.Columns(2).FixedWidth = False
            TreeListView1.Columns(2).Width = 38
            TreeListView1.Columns(0).FixedWidth = False
            TreeListView1.Columns(0).Width = sccTemplateMngr.Panel1.Width - (82 + 28 + 17)
        End If
    End Sub

    Sub ClearComboBox(ByRef control As DevExpress.XtraEditors.ComboBoxEdit, ByVal firstItem As String)
        control.SuspendLayout()
        control.Properties.Items.Clear()
        control.Properties.Items.Insert(0, firstItem)
        control.SelectedIndex = 0
        control.Refresh()
        control.ResumeLayout()
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
        TreeListView2.Nodes.Clear()
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
            TreeListView2.Nodes.Add(node)
        Next
        TreeListView2.UpdateCurrentView()
        For Each col As TreeListViewColumn In TreeListView2.Columns
            TreeListView2.AutoSizeColumn(col)
        Next
        Me.TreeListView2.ResumeUpdate()
    End Sub

    Function IsValidTemplate(ByVal tempid As String) As Boolean
        If (Me.dsVenderData IsNot Nothing) Then
            Dim tempData As DataTable = Me.dsVenderData.Tables(0)
            Dim selectedTemp() As DataRow = tempData.Select("TemplateID='" & tempid & "' AND TemplateOwner='" & Environment.UserName & "'")
            If (selectedTemp.Length > 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Sub cmbTemplate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If cmbTemplate.Text = "" Then
                Return
            End If
            Me.dsTemplateData = Nothing
            Dim tech As String = ""
            Try
                Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("Technology='" & cmbTechnology.Text & "' AND Vendor='" & cmbVendor.Text & "'")
                tech = dr(0)("Tech").ToString
            Catch ex As Exception

            End Try

            If Not (cmbTemplate.SelectedIndex = 0) Then
                If (Me.dsVenderData IsNot Nothing) Then
                    Dim IsValid As Boolean = IsValidTemplate(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)
                    Dim cmdText As String = "SELECT * FROM dbo.IOS_Parameters_Groups WHERE  GroupTech='" & tech & "' and GroupID in (SELECT PGT.GroupID FROM dbo.IOS_Parameters_Templates PT inner join dbo.IOS_Parameters_Group2Template PGT ON PT.TemplateID= PGT.TemplateID WHERE PT.TemplateID='" & TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value() & "');"
                    cmdText += "SELECT G.[GroupName],R.[ID],R.[P_name],R.[Managed_object],R.[P_abbr_name] ,ISNULL(P.[DefaultValue],'') AS DefaultValue,ISNULL(P.[CheckValue],0) AS CheckValue FROM dbo.IOS_Parameters_Items P inner join dbo.IOS_Parameters_Groups G on P.GroupID= G.GroupId inner join dbo.qry_IOS_Parameters R on R.ID= P.ParameterId WHERE P.TemplateId='" & TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value() & "';"
                    cmdText += "SELECT * FROM dbo.IOS_Parameters_Groups WHERE GroupTech='" & tech & "' and GroupID not in (SELECT PGT.GroupID FROM dbo.IOS_Parameters_Templates PT inner join dbo.IOS_Parameters_Group2Template PGT ON PT.TemplateID= PGT.TemplateID WHERE PT.TemplateID='" & TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value() & "')"
                    Me.dsTemplateData = New DataSet()
                    Me.dsTemplateData = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connStrIOSServer, cmdText)
                    If (Me.dsTemplateData IsNot Nothing) Then
                        If (Me.dsTemplateData.Tables.Count > 0) Then
                            TreeListView1.SuspendUpdate()
                            TreeListView1.Nodes.Clear()
                            Dim distGroup As DataTable = Me.dsTemplateData.Tables(0)
                            For Each row As DataRow In distGroup.Rows
                                Dim groupName As String = row("GroupName").ToString()
                                Dim selectedRows() As DataRow = Me.dsTemplateData.Tables(1).Select("GroupName='" & groupName & "'")
                                Dim treeNode As New TreeListViewNode()
                                treeNode.Text = groupName
                                treeNode.Key = row("groupid").ToString
                                For Each Item As DataRow In selectedRows
                                    AddParametersToGroup(treeNode, Item("ID").ToString(), Item("P_name").ToString(), Item("Managed_object").ToString(), Item("P_abbr_name").ToString(), Item("DefaultValue").ToString(), Convert.ToBoolean(Item("CheckValue")))
                                Next
                                treeNode.ExpandAll()
                                TreeListView1.Nodes.Add(treeNode)
                            Next
                        End If
                        If (Me.dsTemplateData.Tables.Count > 2) Then
                            tsmiComboBoxGroup.Items.Clear()
                            tsmiComboBoxGroup.Items.Add("Select Group")
                            For Each Item As DataRow In dsTemplateData.Tables(2).Rows
                                Dim groupname As String = Item("groupid").ToString() & " | " & Item("groupname").ToString()
                                tsmiComboBoxGroup.Items.Add(groupname)
                            Next
                            tsmiComboBoxGroup.SelectedIndex = 0
                            tsmiCurrentTemplate.Checked = True
                            tsmiAllTemplates.Checked = False
                            tsmiSelectedTemplate.Checked = False
                            tsmiSelectedTemplate.DropDownItems.Clear()
                            For Each Item As clsComboBoxItem In cmbTemplate.Properties.Items
                                Dim index As Integer = cmbTemplate.Properties.Items.IndexOf(Item)
                                If Not (index = 0) Then
                                    If (IsValidTemplate(Item.Value)) Then
                                        Dim menuItem As New ToolStripMenuItem()
                                        menuItem.CheckOnClick = True
                                        menuItem.Text = Item.Text
                                        menuItem.Tag = Item.Value
                                        menuItem.Checked = (index = cmbTemplate.SelectedIndex)
                                        AddHandler menuItem.Click, AddressOf childMenu_Click
                                        tsmiSelectedTemplate.DropDownItems.Add(menuItem)
                                    End If
                                End If
                            Next
                        End If
                    End If
                    vlblMessage.Visible = Not IsValid
                    btnDeleteTemplate.Enabled = IsValid
                    TreeListView1.Enabled = IsValid
                Else
                    TreeListView1.Nodes.Clear()
                    vlblMessage.Visible = False
                    btnDeleteTemplate.Enabled = False
                End If
            Else
                TreeListView1.Nodes.Clear()
                vlblMessage.Visible = False
                btnDeleteTemplate.Enabled = False
            End If
            SetTreeListView1ColumnsWidth()
            TreeListView1.ExpandAll()
            TreeListView1.ResumeUpdate()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    <Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    Public Sub txtbox_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If (e.Button = MouseButtons.Right) Then
            ReleaseCapture()
        End If
    End Sub

    Public Sub AddParametersToGroup(ByRef treenode As TreeListViewNode, ByVal pID As String, ByVal P_Name As String, ByVal pManaged_Object As String, ByVal P_abbr_name As String, ByVal defaultValue As String, ByVal checkValue As Boolean)
        Dim childNode As New TreeListViewNode()
        childNode.StyleFromParent = True
        Dim fItem As New TreeListViewSubItem(P_Name)
        fItem.Key = pID
        fItem.Tag = P_abbr_name & "#" & pManaged_Object

        Dim txtBox As New DevExpress.XtraEditors.TextEdit()
        txtBox.Text = defaultValue
        txtBox.ForeColor = Color.DarkGray
        txtBox.Size = New System.Drawing.Size(82, 16)
        txtBox.Tag = pID & "#" & treenode.Key & "#" & treenode.Text.Trim
        AddHandler txtBox.MouseDown, AddressOf txtbox_MouseDown
        '' txtBox.ContextMenuStrip = cmsTreeListView1
        Dim sItem As New TreeListViewSubItem()
        sItem.Control = txtBox
        AddHandler txtBox.MouseUp, AddressOf txtbox_MouseDown
        AddHandler txtBox.TextChanged, AddressOf txtbox_TextChanged
        Dim checkedValue As Boolean = Convert.ToBoolean(checkValue)
        Dim chkbox As New CheckBox()
        'chkbox.Dock = DockStyle.Fill
        chkbox.Size = New System.Drawing.Size(30, 16)
        chkbox.Checked = checkedValue
        chkbox.Name = pID & "#" & treenode.Key & "#" & treenode.Text.Trim
        Dim thItem As New TreeListViewSubItem()
        thItem.Control = chkbox
        AddHandler chkbox.CheckedChanged, AddressOf chkbox_CheckedChanged
        thItem.UpdateLayout()
        childNode.SubItems.Add(fItem)
        childNode.SubItems.Add(sItem)
        childNode.SubItems.Add(thItem)
        treenode.Nodes.Add(childNode)
    End Sub

    Sub txtbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txtbox As DevExpress.XtraEditors.TextEdit = CType(sender, DevExpress.XtraEditors.TextEdit)
        Dim item() As String = txtbox.Tag.Split("#"c)
        Dim pId As String = item(0)
        Dim groupName As String = item(2)
        Dim groupId As String = item(1)
        Dim templateId As String = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value

        Dim cmdText As String = String.Empty
        If (tsmiCurrentTemplate.Checked) Then
            cmdText = "UPDATE [dbo].[IOS_Parameters_Items] SET [DefaultValue] = '" & txtbox.Text & "' WHERE [ParameterID]=" & pId & " AND [GroupID]=" & groupId & " AND [TemplateID]=" & templateId & ""
        ElseIf (tsmiAllTemplates.Checked) Then
            Dim selectedTemplate As String = String.Empty
            For Each menuItems As ToolStripMenuItem In tsmiSelectedTemplate.DropDownItems
                If (IsValidTemplate(menuItems.Tag)) Then
                    selectedTemplate += "'" & menuItems.Tag & "',"
                End If
            Next
            If Not (String.IsNullOrEmpty(selectedTemplate)) Then
                selectedTemplate = selectedTemplate.Remove(selectedTemplate.Length - 1, 1)
                cmdText = "UPDATE [dbo].[IOS_Parameters_Items] SET [DefaultValue] = '" & txtbox.Text & "' WHERE [ParameterID]=" & pId & " AND [GroupID]=" & groupId & " AND [TemplateID] IN (" & selectedTemplate & ")"
            End If
        ElseIf (tsmiSelectedTemplate.Checked) Then
            Dim selectedTemplate As String = String.Empty

            For Each menuItems As ToolStripMenuItem In tsmiSelectedTemplate.DropDownItems
                If (menuItems.Checked) Then
                    If (IsValidTemplate(menuItems.Tag)) Then
                        selectedTemplate += "'" & menuItems.Tag & "',"
                    End If
                End If
            Next
            If Not (String.IsNullOrEmpty(selectedTemplate)) Then
                selectedTemplate = selectedTemplate.Remove(selectedTemplate.Length - 1, 1)
                cmdText = "UPDATE [dbo].[IOS_Parameters_Items] SET [DefaultValue] = '" & txtbox.Text & "' WHERE [ParameterID]=" & pId & " AND [GroupID]=" & groupId & " AND [TemplateID] IN (" & selectedTemplate & ")"
            End If
        End If
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
    End Sub

    Sub chkbox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkbox As CheckBox = CType(sender, CheckBox)
        Dim item() As String = chkbox.Name.Split("#"c)
        Dim pId As String = item(0)
        Dim groupName As String = item(2)
        Dim groupId As String = item(1)
        Dim templateId As String = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
        Dim cmdText As String = String.Empty
        If (tsmiCurrentTemplate.Checked) Then
            cmdText = "UPDATE [dbo].[IOS_Parameters_Items] SET  [CheckValue] = '" & chkbox.Checked & "' WHERE [ParameterID]=" & pId & " AND [GroupID]=" & groupId & " AND [TemplateID]=" & templateId & ""
        ElseIf (tsmiAllTemplates.Checked) Then
            Dim selectedTemplate As String = String.Empty
            For Each menuItems As ToolStripMenuItem In tsmiSelectedTemplate.DropDownItems
                If (IsValidTemplate(menuItems.Tag)) Then
                    selectedTemplate += "'" & menuItems.Tag & "',"
                End If
            Next
            If Not (String.IsNullOrEmpty(selectedTemplate)) Then
                selectedTemplate = selectedTemplate.Remove(selectedTemplate.Length - 1, 1)
                cmdText = "UPDATE [dbo].[IOS_Parameters_Items] SET  [CheckValue] = '" & chkbox.Checked & "' WHERE [ParameterID]=" & pId & " AND [GroupID]=" & groupId & " AND [TemplateID] IN (" & selectedTemplate & ")"
            End If
        ElseIf (tsmiSelectedTemplate.Checked) Then
            Dim selectedTemplate As String = String.Empty

            For Each menuItems As ToolStripMenuItem In tsmiSelectedTemplate.DropDownItems
                If (menuItems.Checked) Then
                    If (IsValidTemplate(menuItems.Tag)) Then
                        selectedTemplate += "'" & menuItems.Tag & "',"
                    End If
                End If
            Next
            If Not (String.IsNullOrEmpty(selectedTemplate)) Then
                selectedTemplate = selectedTemplate.Remove(selectedTemplate.Length - 1, 1)
                cmdText = "UPDATE [dbo].[IOS_Parameters_Items] SET  [CheckValue] = '" & chkbox.Checked & "' WHERE [ParameterID]=" & pId & " AND [GroupID]=" & groupId & " AND [TemplateID] IN (" & selectedTemplate & ")"
            End If
        End If
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
    End Sub

    Private Sub TreeListView1_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeListView1.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub TreeListView1_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeListView1.DragDrop
        Try
            Dim mousePos As System.Drawing.Point = Me.TreeListView1.PointToClient(New System.Drawing.Point(e.X, e.Y))
            If e.Data.GetDataPresent(GetType(LidorSystems.IntegralUI.Lists.TreeListViewNode)) Then
                Dim dragednode As LidorSystems.IntegralUI.Lists.TreeListViewNode = DirectCast(e.Data.GetData(GetType(LidorSystems.IntegralUI.Lists.TreeListViewNode)), LidorSystems.IntegralUI.Lists.TreeListViewNode)
                Dim targetNode As LidorSystems.IntegralUI.Lists.TreeListViewNode = Me.TreeListView1.GetNodeAt(mousePos)
                Me.TreeListView1.SuspendUpdate()
                dragednode = DirectCast(dragednode.Clone(), LidorSystems.IntegralUI.Lists.TreeListViewNode)
                If (targetNode IsNot Nothing) Then
                    Dim paramId As String = dragednode.SubItems(0).Text
                    Dim groupid As String = String.Empty
                    If (targetNode.SubItems.Count > 0) Then
                        groupid = targetNode.Parent.Key.Trim
                        If Not (IsParametersExists(paramId, groupid, TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)) Then
                            AddParametersToGroup(targetNode.Parent, dragednode.SubItems(0).Text, dragednode.SubItems(1).Text, dragednode.SubItems(3).Text, dragednode.SubItems(2).Text, "", False)
                        Else
                            XtraMessageBox.Show("Parameters already exists in selected Group")
                        End If
                    ElseIf (targetNode.SubItems.Count = 0) Then
                        groupid = targetNode.Key.Trim
                        If Not (IsParametersExists(paramId, groupid, TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)) Then
                            AddParametersToGroup(targetNode, dragednode.SubItems(0).Text, dragednode.SubItems(1).Text, dragednode.SubItems(3).Text, dragednode.SubItems(2).Text, "", False)
                        Else
                            XtraMessageBox.Show("Parameters already exists in selected Group")
                        End If
                    End If
                    SetTreeListView1ColumnsWidth()
                    Me.TreeListView1.ExpandAll()
                End If
                Me.TreeListView1.ResumeUpdate()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Function IsParametersExists(ByVal paramid As String, ByVal groupid As String, ByVal templateid As String) As Boolean
        Dim cmdText As String = "DECLARE @STATUS AS INTEGER;IF(EXISTS(SELECT * FROM dbo.IOS_Parameters_Items WHERE ParameterID='" & paramid & "' and GroupID='" & groupid & "' and TemplateID='" & templateid & "')) BEGIN SET @STATUS=1; END ELSE BEGIN INSERT INTO dbo.IOS_Parameters_Items (ParameterID,GroupID,TemplateID, DefaultValue,CheckValue) SELECT " & paramid & "," & groupid & ",TemplateID,'',0 FROM dbo.IOS_Parameters_Templates WHERE TemplateID IN (SELECT TemplateID FROM dbo.IOS_Parameters_Group2Template WHERE GroupID='" & groupid & "'); SET @STATUS=0;END SELECT @STATUS;"
        Dim result As String = IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(connStrIOSServer, cmdText).ToString()
        If (result = "1") Then
            Return True
        End If
        Return False
    End Function

    Private Sub txtAddNewGroup_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAddNewGroup.KeyUp
        If Not (txtAddNewGroup.Text.Trim() = "") Then
            If (e.KeyCode = Keys.Enter) Then
                Dim newGroup As String = txtAddNewGroup.Text.Trim
                Dim tech As String = ""
                Try
                    Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("Technology='" & cmbTechnology.Text & "' AND Vendor='" & cmbVendor.Text & "'")
                    tech = dr(0)("Tech").ToString
                Catch ex As Exception

                End Try
                Dim template As String = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
                txtAddNewGroup.Text = ""
                Dim queryText As String = "DECLARE @groupid as integer;INSERT INTO dbo.IOS_Parameters_Groups (GroupName, GroupTech) VALUES ('" & newGroup & "','" & tech & "');SET @groupid=scope_identity();INSERT INTO dbo.IOS_Parameters_Group2Template (GroupID,TemplateID) VALUES(@groupid,'" & template & "'); SELECT @groupid;"
                Dim groupid As Integer = IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(connStrIOSServer, queryText)
                Dim treeNode As New TreeListViewNode()
                treeNode.Text = newGroup
                treeNode.Key = groupid
                treeNode.ExpandAll()
                TreeListView1.Nodes.Add(treeNode)
                SetTreeListView1ColumnsWidth()
                TreeListView1.ExpandAll()
                TreeListView1.ResumeUpdate()
                cmsTreeListView1.Hide()
            End If
        End If
    End Sub

    Private Sub TreeListView1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TreeListView1.KeyUp
        If (e.KeyCode = Keys.Delete) Then
            If (Me.TreeListView1.SelectedNode.SubItems.Count = 0) Then
                DeleteGroup()
            ElseIf (Me.TreeListView1.SelectedNode.SubItems.Count > 0) Then
                DeleteParameter()
            End If
        End If
    End Sub

    Private Sub tsmiDelParameter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiDelParameter.Click
        If (Me.TreeListView1.SelectedNode.SubItems.Count > 0) Then
            DeleteParameter()
        End If
    End Sub

    Sub DeleteParameter()
        Dim parameterid As String = Me.TreeListView1.SelectedNode.SubItems(0).Key.Trim
        Dim groupid As String = Me.TreeListView1.SelectedNode.Parent.Key.Trim
        Dim templateid As String = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
        Dim cmdText As String = "delete from dbo.IOS_Parameters_Items where ParameterID='" & parameterid & "' and GroupID='" & groupid & "'"
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
        Me.TreeListView1.SelectedNode.Remove()
    End Sub

    Private Sub cmsTreeListView1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsTreeListView1.Opening
        If (Me.TreeListView1.Nodes.Count > 0) Then
            If (Me.TreeListView1.SelectedNode IsNot Nothing) Then
                tsmiParameterDescTLV1.Enabled = Not (Me.TreeListView1.SelectedNode.SubItems.Count = 0)
                tsmiDeleteGroup.Enabled = (Me.TreeListView1.SelectedNode.SubItems.Count = 0)
                tsmiDelParameter.Enabled = Not (Me.TreeListView1.SelectedNode.SubItems.Count = 0)
            Else
                tsmiParameterDescTLV1.Enabled = False
                tsmiDeleteGroup.Enabled = False
                tsmiDelParameter.Enabled = False
            End If
        Else
            If (cmbTemplate.SelectedIndex > 0) Then
                tsmiParameterDescTLV1.Enabled = False
                tsmiDeleteGroup.Enabled = False
                tsmiDelParameter.Enabled = False
                tsmiAddNewGroup.Enabled = True
                tsmiAddExistingGroup.Enabled = True
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub btnAddTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTemplate.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (cmbTechnology.SelectedIndex = 0) Then
                XtraMessageBox.Show("Please select technology first", "IOS - Parameter Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbTechnology.Focus()
                Return
            ElseIf (cmbVendor.SelectedIndex = 0) Then
                XtraMessageBox.Show("Please select Vendor first", "IOS - Parameter Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbVendor.Focus()
                Return
            End If
            Dim dailog As New dlgParameter()
            dailog.ShowDialog()
            If (String.IsNullOrEmpty(dailog.ReturnData)) Then
            ElseIf (dailog.ReturnData = "NoData") Then
            Else
                Dim tech As String = ""
                Try
                    Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("Technology='" & cmbTechnology.Text & "' AND Vendor='" & cmbVendor.Text & "'")
                    tech = dr(0)("Tech").ToString
                Catch ex As Exception

                End Try
                Dim values() As String = dailog.ReturnData.Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                Dim cmdTest As String = String.Empty
                If (values.Length = 1) Then
                    cmdTest = "DECLARE @templateid as integer;INSERT INTO dbo.IOS_Parameters_Templates (TemplateName,TemplateTech, Technology, Vendor,TemplateOwner) VALUES ('" & values(0) & "','" & tech & "','" & cmbTechnology.SelectedItem.ToString & "','" & cmbVendor.SelectedItem.ToString & "','" & Environment.UserName & "');SET @templateid=scope_identity();SELECT @templateid;"
                ElseIf (values.Length = 2) Then
                    cmdTest = "BEGIN TRY BEGIN TRANSACTION trans1 BEGIN  DECLARE @templateid as integer;INSERT INTO dbo.IOS_Parameters_Templates (TemplateName, TemplateTech, Technology,Vendor,TemplateOwner) VALUES ('" & values(0) & "','" & tech & "','" & cmbTechnology.SelectedItem.ToString & "','" & cmbVendor.SelectedItem.ToString & "','" & Environment.UserName & "');SET @templateid=scope_identity();"
                    cmdTest += "INSERT INTO dbo.IOS_Parameters_Group2Template select GroupID,@templateid from dbo.IOS_Parameters_Group2Template where TemplateID='" & values(1) & "';"
                    cmdTest += "INSERT INTO dbo.IOS_Parameters_Items select ParameterID,GroupID,@templateid,DefaultValue,CheckValue from dbo.IOS_Parameters_Items where TemplateID='" & values(1) & "';"
                    cmdTest += "SELECT @templateid; END	COMMIT TRAN trans1 END TRY BEGIN CATCH ROLLBACK TRAN trans1 END CATCH"
                End If
                If Not (String.IsNullOrEmpty(cmdTest)) Then
                    Dim result As Object = IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(connStrIOSServer, cmdTest)
                    cmbTemplate.SuspendLayout()
                    If (result IsNot Nothing AndAlso Not (result = 0)) Then
                        Dim item As New clsComboBoxItem
                        item.Text = values(0)
                        item.Value = result
                        item.IsChecked = True
                        cmbTemplate.Properties.Items.Add(item)
                        If (Me.dsVenderData IsNot Nothing) Then
                            Dim row As DataRow = Me.dsVenderData.Tables(0).NewRow()
                            row("templateid") = result
                            row("TemplateName") = values(0)
                            row("Technology") = cmbTechnology.SelectedItem.ToString
                            row("Vendor") = cmbVendor.SelectedItem.ToString
                            row("TemplateOwner") = Environment.UserName
                            Me.dsVenderData.Tables(0).Rows.Add(row)
                        End If
                        cmbTemplate.SelectedIndex = cmbTemplate.Properties.Items.IndexOf(item)
                        cmbTemplate.Refresh()
                        vlblMessage.Visible = False
                        btnDeleteTemplate.Enabled = True
                    End If
                    cmbTemplate.ResumeLayout()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiDeleteGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiDeleteGroup.Click
        If (Me.TreeListView1.SelectedNode.SubItems.Count = 0) Then
            DeleteGroup()
        End If
    End Sub

    Sub DeleteGroup()
        Dim groupid As String = Me.TreeListView1.SelectedNode.Key.Trim
        Dim templateid As String = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
        tsmiComboBoxGroup.Items.Add(groupid & " | " & Me.TreeListView1.SelectedNode.Text.Trim)
        Dim cmdText As String = "delete from dbo.IOS_Parameters_Items where GroupID='" & groupid & "' and TemplateID='" & templateid & "';DELETE FROM dbo.IOS_Parameters_Group2Template WHERE GroupID='" & groupid & "' AND TemplateID='" & templateid & "'"
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
        Me.TreeListView1.SelectedNode.Remove()
        TreeListView1.ResumeUpdate()
    End Sub

    Private Sub tsmiComboBoxGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiComboBoxGroup.SelectedIndexChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tsmiComboBoxGroup.SelectedIndex > 0) Then
                Dim groupid As String = tsmiComboBoxGroup.SelectedItem.ToString().Split("|")(0)
                If Not (String.IsNullOrEmpty(groupid)) Then
                    Dim templateid As String = TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
                    Dim cmdText As String = "INSERT INTO dbo.IOS_Parameters_Group2Template (GroupID,TemplateID) VALUES('" & groupid & "','" & templateid & "');SELECT * FROM dbo.qry_IOS_Parameters WHERE ID IN(SELECT DISTINCT parameterid FROM dbo.IOS_Parameters_Items where groupid='" & groupid & "');"
                    cmdText += "INSERT INTO dbo.IOS_Parameters_Items (ParameterID,GroupID,TemplateID) SELECT ID,'" & groupid & "','" & templateid & "' FROM dbo.qry_IOS_Parameters WHERE ID IN(SELECT DISTINCT parameterid FROM dbo.IOS_Parameters_Items where groupid='" & groupid & "')"
                    Dim paramData As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, cmdText)
                    Dim treeNode As New TreeListViewNode()
                    treeNode.Text = tsmiComboBoxGroup.SelectedItem.ToString().Split("|")(1)
                    treeNode.Key = groupid
                    For Each Item As DataRow In paramData.Rows
                        AddParametersToGroup(treeNode, Item("ID").ToString(), Item("P_name").ToString(), Item("Managed_object").ToString(), Item("P_abbr_name").ToString(), "", False)
                    Next
                    treeNode.ExpandAll()
                    TreeListView1.Nodes.Add(treeNode)
                    TreeListView1.ExpandAll()
                    TreeListView1.ResumeUpdate()
                    cmsTreeListView1.Hide()
                    tsmiComboBoxGroup.Items.Remove(tsmiComboBoxGroup.SelectedItem)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiParameterDescTLV1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiParameterDescTLV1.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tn As TreeListViewNode = TreeListView1.SelectedNode
            If (tn IsNot Nothing) Then
                If (tn.SubItems.Count > 0) Then
                    Dim subItem As TreeListViewSubItem = tn.SubItems(0)
                    Dim itemArr() As String = subItem.Tag.ToString().Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    If (itemArr.Length >= 2) Then
                        Dim objParamDesc As New frmParameterDescription()
                        objParamDesc.moTblName = Nothing
                        objParamDesc.paramName = itemArr(0).ToString
                        objParamDesc.moName = itemArr(1).ToString
                        objParamDesc.fromLeft = Me.Left + Me.Width
                        objParamDesc.fromTop = Me.Top
                        objParamDesc.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiParmeterDescTLV2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiParmeterDescTLV2.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tn As TreeListViewNode = TreeListView2.SelectedNode
            If (tn IsNot Nothing) Then
                If (tn.SubItems.Count > 0) Then
                    Dim objParamDesc As New frmParameterDescription()
                    objParamDesc.moTblName = Nothing
                    objParamDesc.paramName = tn.SubItems(2).Text
                    objParamDesc.moName = tn.SubItems(3).Text
                    objParamDesc.fromLeft = Me.Left + Me.Width
                    objParamDesc.fromTop = Me.Top
                    objParamDesc.ShowDialog()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vcmbFilterOnObject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFilterOnObject.SelectedIndexChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (Me.dsVenderData IsNot Nothing) Then
                BindParameterData(Me.dsVenderData.Tables(1))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vtxtSearchLongName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchLongName.TextChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (txtSearchLongName.Text.Trim.Length >= 3) Then
                If (Me.dsVenderData IsNot Nothing) Then
                    BindParameterData(Me.dsVenderData.Tables(1))
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnDeleteTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTemplate.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If IsValidTemplate(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value) Then
                Dim cmdText As String = "DECLARE @TID AS INTEGER=" & TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value & ";DELETE FROM dbo.IOS_Parameters_Items where TemplateID=@TID;DELETE FROM dbo.IOS_Parameters_Group2Template where TemplateID=@TID;DELETE FROM dbo.IOS_Parameters_Templates where TemplateID=@TID;"
                Dim result As System.Windows.Forms.DialogResult = XtraMessageBox.Show("Do you really want to delete this Template.", "Please Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                If (result = DialogResult.OK) Then
                    Dim status As Integer = IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, cmdText)
                    XtraMessageBox.Show("Deleted Successfully")
                    cmbTemplate.SuspendLayout()
                    cmbTemplate.Properties.Items.Remove(cmbTemplate.SelectedItem)
                    cmbTemplate.SelectedIndex = 0
                    cmbTemplate.Refresh()
                    cmbTemplate.ResumeLayout()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub treeListView2_ColumnClick(ByVal sender As Object, ByVal e As LidorSystems.IntegralUI.ObjectClickEventArgs) Handles TreeListView2.ColumnClick
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If TypeOf e.[Object] Is TreeListViewColumn Then
                Dim column As TreeListViewColumn = DirectCast(e.[Object], TreeListViewColumn)
                If column.Index = 1 Or column.Index = 2 Then
                    For i As Integer = 0 To Me.TreeListView2.FlatNodes.Count - 1
                        If Me.TreeListView2.FlatNodes(i).SubItems.Count > 2 Then
                            Me.TreeListView2.FlatNodes(i).SubItems(column.Index).SortTag = Me.TreeListView2.FlatNodes(i).SubItems(column.Index).Text
                        End If
                    Next
                    Me.TreeListView2.ComparerObjectType = LidorSystems.IntegralUI.Lists.ComparerObjectType.String
                    Select Case Me.TreeListView2.Sorting
                        Case SortOrder.Ascending
                            Me.TreeListView2.Sorting = SortOrder.Descending
                            Exit Select
                        Case SortOrder.Descending
                            Me.TreeListView2.Sorting = SortOrder.Ascending
                            Exit Select
                        Case Else
                            Me.TreeListView2.Sorting = SortOrder.Descending
                            Exit Select
                    End Select
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub sccTemplateMngr_SplitterMoved(ByVal sender As System.Object, ByVal e As EventArgs) Handles sccTemplateMngr.SplitterMoved
        SetTreeListView1ColumnsWidth()
    End Sub

    Private Sub tsmiCurrentTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiCurrentTemplate.Click
        tsmiCurrentTemplate.Checked = True
        tsmiSelectedTemplate.Checked = False
        tsmiAllTemplates.Checked = False
    End Sub

    Private Sub tsmiAllTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiAllTemplates.Click
        tsmiCurrentTemplate.Checked = False
        tsmiSelectedTemplate.Checked = False
        tsmiAllTemplates.Checked = True
    End Sub

    Private Sub tsmiSelectedTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiSelectedTemplate.Click
        tsmiCurrentTemplate.Checked = False
        tsmiSelectedTemplate.Checked = True
        tsmiAllTemplates.Checked = False
    End Sub

    Private Sub childMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        contextFlag = False
    End Sub

    Private Sub cmsTreeListView1_Closing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripDropDownClosingEventArgs) Handles cmsTreeListView1.Closing
        e.Cancel = Not contextFlag
        contextFlag = True
    End Sub

#End Region

#Region "Context Menu Code"

    Dim cm_OT_SourceControl As System.Windows.Forms.Control

    Private Sub cm_ObjectTree_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cm_ObjectTree.Opening
        cm_OT_SourceControl = cm_ObjectTree.SourceControl
        Dim tv As TreeView = CType(cm_ObjectTree.SourceControl, TreeView)
        Dim vendor As String = cmbVendor.SelectedItem.ToString
        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
        Dim countchecked As Integer = 0
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim ExactMatch As Boolean = True
            If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                ExactMatch = False
            Else
                ExactMatch = True
            End If

            'count checked boxes
            countchecked = TreeView_CountCheckedNodes(tv.Nodes(0))
            tsmi_OT_Exception.Visible = False

            'enable/disable copy
            If countchecked > 0 Then
                cm_OT_tsmi_copy.Text = "Copy - Objects: " & countchecked
                cm_OT_tsmi_copy.Enabled = True
            Else
                cm_OT_tsmi_copy.Text = "Copy"
                cm_OT_tsmi_copy.Enabled = False
            End If

            'check clipboard
            Dim s As String = Clipboard.GetText()                  'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim i, j As Integer
            If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                cm_OT_tsmi_paste.Text = "Paste - Objects: ?"
                cm_OT_tsmi_paste.Enabled = True
            Else

                Dim clipboardmatches As Integer = 0
                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                    For j = 0 To bufferCell.Length - 1
                        If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                            bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                        End If
                        If bufferCell(j).Trim <> "" Then
                            If Not Treeview_TextSearch(bufferCell(j).Trim, tv.Nodes, ExactMatch) Is Nothing Then
                                clipboardmatches = clipboardmatches + 1
                            End If
                        End If
                    Next
                Next

                'enable/disable paste
                If clipboardmatches > 0 Then
                    cm_OT_tsmi_paste.Text = "Paste - Objects: " & clipboardmatches
                    cm_OT_tsmi_paste.Enabled = True
                Else
                    cm_OT_tsmi_paste.Text = "Paste"
                    cm_OT_tsmi_paste.Enabled = False
                End If
                tv.Cursor = Cursors.Arrow
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

        'tags
        '----

        'get all tags
        Dim sql As String = Nothing
        Dim connstring As String = Nothing
        Dim ds_tag As DataSet = Nothing
        cm_OT_tsmi_CopyToTag.Enabled = False
        Try
            sql = GetSQL(8601, Nothing)(1)
            connstring = GetSQL(8601, Nothing)(0)
            ds_tag = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connstring, sql)

            For Each drow As DataRow In ds_tag.Tables(0).Rows
                Dim tsmi As ToolStripMenuItem = New ToolStripMenuItem(drow(1).ToString.Trim)

                AddHandler tsmi.Click, AddressOf cm_OT_CopyToTag_ItemClick
                cm_OT_tsmi_CopyToTag.DropDownItems.Add(tsmi)

            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            ds_tag.Dispose()
            ds_tag = Nothing
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        'exception list
        If tv.Name = "TreeView_Tuning_Objects" And countchecked > 0 Then
            tsmi_OT_Exception.Visible = True
        End If
    End Sub

    Private Sub cm_OT_CopyToTag_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim tsmi As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        'Dim tv As TreeView
        'tv = cm_OT_SourceControl
        'tv.Dispose()
        'tv = Nothing
    End Sub

    Private Sub cm_OT_tsmi_copy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_OT_tsmi_copy.Click
        Clipboard.Clear()
        Dim tv As TreeView = cm_OT_SourceControl
        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim vendor As String = cmbVendor.SelectedItem.ToString
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
        Try
            Dim copystring As String = TreeView_Checked2String(vendor & " " & tech, aggr_to, "Naked", tv, cmbTargetObject)
            copystring = copystring.Replace(",", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            tv.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub cm_OT_tsmi_paste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_OT_tsmi_paste.Click
        Dim tv As TreeView = cm_OT_SourceControl
        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        tv.Cursor = Cursors.WaitCursor
        Try
            Dim ExactMatch As Boolean = True
            If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                ExactMatch = False
            Else
                ExactMatch = True
            End If

            Dim s As String = Clipboard.GetText()                   'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim i, j As Integer
            Dim clipboardmatches As Integer = 0
            Dim mbresult As MsgBoxResult = MsgBoxResult.Ok

            If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                mbresult = MsgBox("An estimated " & s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length & " strings on clipboard are detected. Selection can take long. Do you wish to continue selection?", MsgBoxStyle.OkCancel)
            End If

            If mbresult = MsgBoxResult.Ok Then
                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                    For j = 0 To bufferCell.Length - 1
                        If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                            bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                        End If
                        Dim tv_result As TreeNode = Treeview_TextSearch(bufferCell(j).Trim, tv.Nodes, ExactMatch)
                        If Not tv_result Is Nothing Then
                            tv_result.Checked = True
                        End If
                    Next
                Next
            End If

            tv.Cursor = Cursors.Arrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            tv.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_OT_tsmi_CheckChilds_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_OT_tsmi_CheckChilds.Click
        Dim tv As TreeView = cm_OT_SourceControl
        Try
            Objecttree_CheckChild(tv.SelectedNode)
        Catch
        End Try
    End Sub

    Private Sub tsmi_OT_UnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_OT_UnCheck.Click
        Dim tv As TreeView = cm_OT_SourceControl
        TreeView_ClearChecks(tv.Nodes(0))
    End Sub

    Private Sub tsmi_OT_MapCell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_OT_MapCell.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tv As TreeView = cm_OT_SourceControl
            Dim tech As String = Me.GetTechnologyName(cmbTechnology.SelectedItem.ToString, cmbVendor.SelectedItem.ToString, "Tech")
            'Dim vendor As String = vcmbVendor.SelectedItem.ToString

            Select Case tech
                Case cmbVendor.SelectedItem.ToString.Trim & " " & "3G"
                    If cmbTargetObject.SelectedItem.ToString = "WCEL" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "WCEL", "Naked", tv, cmbTargetObject), "3G", Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "WBTS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "WBTS", "Naked", tv, cmbTargetObject), "3G", Nothing, True)
                    End If
                Case cmbVendor.SelectedItem.ToString.Trim & " " & "2G"
                    If cmbTargetObject.SelectedItem.ToString = "CELL" Or cmbTargetObject.SelectedItem.ToString = "BTS" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "CELL", "Naked", tv, cmbTargetObject), "2G", Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "BCF" Or cmbTargetObject.SelectedItem.ToString = "SITE" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeView_Checked2String(tech, "BCF", "Naked", tv, cmbTargetObject), "2G", Nothing, True)
                    End If
            End Select
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ReloadTree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_ReloadTree.Click
        Dim tv As TreeView = CType(cm_OT_SourceControl, TreeView)
        Dim tech As String = Nothing
        Dim aggr_to As String = Nothing
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        tv.Nodes.Clear()
        tech = cmbTechnology.SelectedItem.ToString
        Try
            Select Case True
                Case tech.Contains("3G")
                    tech = "3G"
                    dsTree3G_wcel.Dispose()
                    dsTree3G_wbts.Dispose()
                    dsTree3G_rnc.Dispose()
                    Application.DoEvents()
                    IOS_ObjectConfig_Load(tech, True)
                    vcmbTargetObject_SelectedIndexChanged(Nothing, Nothing)
                Case tech.Contains("2G")
                    tech = "2G"
                    dsTree2G_bcf.Dispose()
                    dsTree2G_bsc.Dispose()
                    dsTree2G_cel.Dispose()
                    Application.DoEvents()
                    IOS_ObjectConfig_Load(tech, True)
                    vcmbTargetObject_SelectedIndexChanged(Nothing, Nothing)
            End Select
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            Me.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

        Try
            frmMapWindow.Calendar_GetNetworks_Fill()
            frmMapWindow.Calendar_GetNetwork_Fill_From_DB()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Me.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Me.Cursor = Cursors.Arrow
    End Sub

#End Region

End Class