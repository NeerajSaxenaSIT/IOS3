Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.Data
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns

Public Class frmListManager

#Region "Variables"

    Private dtObjectTypes As New DataTable
    Private dsObjects As New DataSet
    Private dtTechnology As New DataTable
    Private imgListObject As New ImageList
    Private CountOT As Integer = 0
    Private dtNotSaved As DataTable = Nothing
    Private SupportedObjects As New List(Of String)
    Private imgListKPI As New ImageList
    Private Count_OT As Integer = 0
    Private strTreeFilter As String = ""

    Private countPreFilter As Integer = 0
    Private countPostFilter As Integer = 0

#End Region

#Region "Form & Control Event"

    Private Sub frmListManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            SupportedObjects.AddRange({"PLMN", "RNC", "URNC", "CELL", "UCELL", "LCELL", "NODEB", "UNODEB", "ENODEB", "LOCID", "CLUSTER_ID", "LAC", "UARFCN", "EARFCN"})

            FillList()

            Dim strConnection As String, sqlParam As String
            strConnection = GetSQL(4513, Nothing)(0)
            sqlParam = GetSQL(4513, Nothing)(1)
            dtTechnology = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            BindDevExComboBoxWithValueMember(cmbTechnology, dtTechnology, "IOS_TECH", "IOS_TECH", "Select", False)
            ConfigurListManagerForm(Me.Name)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
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

    Public Sub ConfigurListManagerForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                tsmiDeleteSelectedRow, tsmiPasteFromClipboard
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

#Region "List"

    Private Sub gvList_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvList.CellValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim data As DataRow = gvList.GetFocusedDataRow()
            If data IsNot Nothing Then
                If e.Value = "" Then
                    XtraMessageBox.Show("List name can not leave blank!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    FillList()
                Else
                    Dim strConnection As String, sqlParam As String
                    Dim parray()() As String = {
                                                    New String() {"@ListName", Chr(39) & e.Value & Chr(39)},
                                                    New String() {"@ListID", data.Item(0)}
                                               }

                    strConnection = GetSQL(4527, parray)(0)
                    sqlParam = GetSQL(4527, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
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

    Private Sub gvList_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvList.ShowingEditor
        Try
            Dim data As DataRow = gvList.GetFocusedDataRow()
            If data IsNot Nothing Then
                If data.Item("ListOwner").ToString.ToLower <> Environment.UserName.ToLower Then
                    If ceIsPublic.Checked Then
                        e.Cancel = False
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvList_MouseDown(sender As Object, e As MouseEventArgs) Handles gvList.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(e.Location)
        If hitInfo.InRowCell Then
            view.FocusedRowHandle = hitInfo.RowHandle
            view.FocusedColumn = hitInfo.Column
            DXMouseEventArgs.GetMouseArgs(e).Handled = True
            If e.Clicks = 2 AndAlso e.Button = MouseButtons.Left Then
                view.ShowEditor()
            End If
        End If
    End Sub

    Private Sub gvList_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvList.FocusedRowChanged
        Try
            RemoveHandler ceIsPublic.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
            Dim dr As DataRow = gvList.GetFocusedDataRow()
            If dr IsNot Nothing Then

                Dim drList As DataRow = GetListDetailsByID(dr.Item("ListID"))
                If drList IsNot Nothing Then
                    lblOwner.Text = Convert.ToString(drList("ListOwner"))
                    ceIsPublic.Checked = IIf(IsDBNull(drList("IsPublic")), False, drList("IsPublic"))
                End If

                If lblOwner.Text.ToLower <> Environment.UserName.ToLower Then
                    ceIsPublic.Enabled = False
                    lblOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                    lblOwner.ForeColor = Color.Red
                    grdListContext.Enabled = False
                    If ceIsPublic.Checked Then
                        btnDelete.Enabled = True
                        grdListContext.Enabled = True
                    Else
                        btnDelete.Enabled = False
                    End If
                Else
                    ceIsPublic.Enabled = True
                    lblOwner.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                    lblOwner.ForeColor = Color.Black
                    grdListContext.Enabled = True
                    btnDelete.Enabled = True
                End If
                FillListObjects(dr.Item(0))
                dtNotSaved = Nothing
            End If
            AddHandler ceIsPublic.CheckedChanged, AddressOf ceIsPublic_CheckedChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ceIsPublic_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim parray()() As String = Nothing
            Dim rIndex() As Integer = gvList.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvList.GetRow(rIndex(0)).Row
                Dim listID As Integer = drow("ListID")
                parray = {
                            New String() {"@listID", listID},
                            New String() {"@isPublic", IIf(ceIsPublic.Checked, 1, 0)}
                         }
            End If

            Dim strConnection As String = GetSQL(4571, parray)(0)
            Dim sqlParam As String = GetSQL(4571, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        dlgListName.txtListName.Text = ""
        dlgListName.txtDescription.Text = ""
        If ceIsPublic.Checked Then
            dlgListName.IsPublic = True
        Else
            dlgListName.IsPublic = False
        End If
        If dlgListName.ShowDialog() = DialogResult.OK Then
            FillList()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteList()
    End Sub

    Private Sub grdList_KeyUp(sender As Object, e As KeyEventArgs) Handles grdList.KeyUp
        If e.KeyCode = Keys.Delete Then
            DeleteList()
        End If
    End Sub

    Private Sub txtAlertSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchList.KeyUp
        Try
            If dtCellList IsNot Nothing Then
                If (txtSearchList.Text.Length > 0) Then
                    dtCellList.DefaultView.RowFilter = "[ListName] Like '%" & txtSearchList.Text & "%'"
                Else
                    dtCellList.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Objects"

    Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

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
                tlObjectsTree.Columns.Clear()
                tlObjectsTree.Nodes.Clear()
                Count_OT = 0
                lblObjectTreeCount.Text = "#: 0"
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbObjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbObjectType.SelectedIndexChanged
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()

            RemoveHandler tlObjectsTree.NodeChanged, AddressOf tlObjectsTree_NodeChanged

            If cmbObjectType.SelectedIndex > -1 Then
                CountOT = 0
                lblObjectTreeCount.Text = "#: 0"

                tlObjectsTree.Columns.Clear()
                tlObjectsTree.Nodes.Clear()

                clsIOSImageList.GetKPIImages(imgListKPI)
                clsIOSImageList.SetImages(imgListObject, cmbTechnology.SelectedItem.ToString)
                tlObjectsTree.SelectImageList = imgListObject

                If cmbObjectType.SelectedItem.ToString.ToUpper = "LOCID" Then
                    Dim dtLOCIDObjects As New DataTable
                    Dim strConnection As String = GetSQL(4555, Nothing)(0)
                    Dim sqlParam As String = GetSQL(4555, Nothing)(1)
                    dtLOCIDObjects = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                    Try
                        FillObjectTreeList(tlObjectsTree, dtLOCIDObjects)
                    Catch ex As Exception
                    Finally
                        tlObjectsTree.Cursor = Cursors.Default
                        Application.DoEvents()
                    End Try
                Else
                    FillTreeList(tlObjectsTree, cmbTechnology.SelectedItem.ToString, cmbObjectType.SelectedItem.ToString, "PLMN", Nothing)
                End If
            End If
            CountOT = 0
            lblObjectTreeCount.Text = "#: 0"
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            AddHandler tlObjectsTree.NodeChanged, AddressOf tlObjectsTree_NodeChanged
            tlObjectsTree.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub FillObjectTreeList(ByRef tl As TreeList, ByRef dt As DataTable)
        Try
            tl.Cursor = Cursors.WaitCursor
            tl.BeginUnboundLoad()
            Application.DoEvents()

            Dim colList() As String = {"PLMN", "ObjectName"}
            tl.Columns.Clear()
            For i As Integer = 0 To colList.Length - 1
                Dim col1 As New TreeListColumn()
                col1.Caption = colList(i)
                col1.VisibleIndex = i
                If colList(i) = "PLMN" Then
                    tl.AutoFillColumn = col1
                    col1.Visible = True
                Else
                    col1.Visible = False
                End If
                tl.Columns.Add(col1)
            Next
            tl.Nodes.Clear()

            Dim tlNode As TreeListNode = tl.Nodes.Add(New Object() {"PLMN", "ObjectName"})

            For Each drParent As DataRow In dt.Rows
                Dim subNode As TreeListNode = tl.AppendNode(New Object() {drParent(dt.Columns(dt.Columns.Count - 1).ColumnName), drParent(dt.Columns(dt.Columns.Count - 1).ColumnName)}, tlNode)
            Next

        Catch ex As Exception
        Finally
            tl.EndUnboundLoad()
            If tl.Nodes.Count > 0 Then
                tl.SelectNode(tl.Nodes(0))
                tl.SetFocusedNode(tl.Nodes(0))
                tl.CollapseAll()
                tl.ExpandToLevel(0)
            End If
            tl.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tlObjectsTree_FilterCheckVisible(ByRef nds As TreeListNodes)
        For Each nd As TreeListNode In nds
            If nd.HasChildren Then
                tlObjectsTree_FilterCheckVisible(nd.Nodes)
            End If
            If nd.Visible = True Then
                nd.Checked = True
            Else
                nd.Checked = False
            End If
        Next
    End Sub

    Private Sub tlObjectsTree_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        RemoveHandler tlObjectsTree.NodeChanged, AddressOf tlObjectsTree_NodeChanged
        If e.ChangeType = DevExpress.XtraTreeList.NodeChangeTypeEnum.CheckedState Then
            If e.Node.CheckState = CheckState.Checked Then
                If tlObjectsTree.FindFilterText <> "" Then
                    tlObjectsTree_FilterCheckVisible(e.Node.Nodes)
                Else
                    e.Node.CheckAll()
                End If
            Else
                e.Node.UncheckAll()
            End If

            tlObjectsTree.CheckParentNode(e.Node)
            Dim Count_Checked As Integer = tlObjectsTree.GetEndCheckedNodes(strTreeFilter).Count

            If tlObjectsTree.FindFilterText <> "" Then
                Dim level As Integer = GetNodeLevelByObjectType(cmbTechnology.SelectedItem.ToString, cmbObjectType.SelectedItem.ToString)
                Count_Checked = tlObjectsTree.GetAllCheckedNodes().Where(Function(nd) nd.Level = level).ToList().Count
            End If

            Count_OT = Count_Checked

            If cmbObjectType.Text = "TAGS" Then
                Count_OT = tlObjectsTree.GetAllCheckedNodes().Where(Function(nd) nd.Level = 3).ToList().Count
            End If

            lblObjectTreeCount.Text = "#: " & Count_OT
        End If
        AddHandler tlObjectsTree.NodeChanged, AddressOf tlObjectsTree_NodeChanged
    End Sub

    Private Sub tlObjectsTree_BeforeCheckNode(sender As Object, e As CheckNodeEventArgs) Handles tlObjectsTree.BeforeCheckNode
        If e.Node.Level = 3 Then
            If e.Node.ParentNode.ParentNode.Item("ObjectName") = "Static List" Then
                e.CanCheck = False
                e.State = CheckState.Unchecked
                Exit Sub
            End If
        End If
    End Sub

    Private Sub tvObjectsTree_BeforeCheck(sender As Object, e As TreeViewCancelEventArgs) Handles tvObjectsTree.BeforeCheck
        If e.Node.ForeColor = Color.Gray And (e.Action = TreeViewAction.ByKeyboard Or e.Action = TreeViewAction.ByMouse) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub tlObjectsTree_MouseMove(sender As Object, e As MouseEventArgs) Handles tlObjectsTree.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim lstNode As List(Of TreeListNode) = Treelist_GetCheck(tlObjectsTree.Nodes)
                If lstNode IsNot Nothing And lstNode.Count > 0 Then
                    Dim obj() As Object = {"ObjectTreeDrag", lstNode}
                    tlObjectsTree.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub treeList_CustomRowFilter(sender As Object, e As FilterNodeEventArgs) Handles tlObjectsTree.CustomRowFilter
        Try
            Dim parentNode As TreeListNode = e.Node.ParentNode
            If parentNode IsNot Nothing Then
                If e.Node.ParentNode.Visible = True And (e.Node.Item("ObjectName").ToString().ToUpper().Contains(e.Node.TreeList.FindFilterText.ToUpper()) Or e.Node.ParentNode.Item("ObjectName").ToString().ToUpper().Contains(e.Node.TreeList.FindFilterText.ToUpper())) Then
                    e.Node.Visible = e.Node.Visible OrElse e.Node.ParentNode.Visible
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub treeList_ColumnFilterChanged(sender As Object, e As EventArgs) Handles tlObjectsTree.ColumnFilterChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tl As TreeList = TryCast(sender, TreeList)
            tl.SuspendLayout()

            strTreeFilter = tl.FindFilterText
            If String.IsNullOrWhiteSpace(tl.FindFilterText) Then
                tl.CollapseAll()
                tl.ExpandToLevel(0)
                countPreFilter = tl.GetEndCheckedNodes().Count
                countPostFilter = 0
            Else
                Dim tNode() As TreeListNode = Nothing
                tNode = tl.FindNodes(Function(node) node.GetDisplayText("ObjectName").ToLower() = tl.FindFilterText.ToLower)
                If tNode IsNot Nothing AndAlso tNode.Length > 0 Then
                    tl.FocusedNode = tNode(0)
                End If

                'countPostFilter = tl.GetEndCheckedNodes(strTreeFilter).Count
                countPostFilter = 0
                countPreFilter = 0
            End If

            tl.ResumeLayout()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Context Menu"

    Private Sub tsmiPasteFromClipboard_Click(sender As Object, e As EventArgs) Handles tsmiPasteFromClipboard.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim s As String = Clipboard.GetText()                   'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim delimeter1 As String = ","
            Dim delimeter2 As String = vbTab
            Dim dtContext As DataTable
            dtContext = CType(grdListContext.DataSource, DataTable)

            If dtNotSaved Is Nothing Then
                dtNotSaved = New DataTable()
                dtNotSaved = dtContext.Clone()
            End If

            Dim hasError As Boolean = False

            Dim rIndex() As Integer = gvList.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drList As DataRow = gvList.GetRow(rIndex(0)).Row
                For i As Integer = 0 To rows.Length - 1
                    Dim str_comma() As String = rows(i).Split(delimeter1)
                    Dim str_tab() As String = rows(i).Split(delimeter2)

                    Dim str() As String = Nothing
                    If str_comma.Length = 2 Then
                        str = str_comma
                    Else
                        str = str_tab
                    End If

                    If str.Length = 2 Then
                        Dim drContext As DataRow
                        drContext = dtContext.NewRow
                        drContext("ListID") = drList.Item(0)
                        '    drContext("IOS_TECH") = str(0).Replace(vbLf, "")
                        drContext("ObjectType") = str(0).Replace(vbLf, "")
                        drContext("ObjectName") = str(1).Replace(vbLf, "")
                        dtContext.Rows.Add(drContext)

                        Dim drNotSaved As DataRow
                        drNotSaved = dtNotSaved.NewRow
                        drNotSaved("ListID") = drList.Item(0)
                        '     drNotSaved("IOS_TECH") = str(0).Replace(vbLf, "")
                        drNotSaved("ObjectType") = str(0).Replace(vbLf, "")
                        drNotSaved("ObjectName") = str(1).Replace(vbLf, "")
                        dtNotSaved.Rows.Add(drNotSaved)
                    Else
                        If Not str(0) = vbLf And Not i = rows.Length - 1 Then
                            hasError = True
                        End If

                    End If
                Next
            End If

            If hasError = True Then
                XtraMessageBox.Show("Columns mismatch, column must be: " & vbNewLine & "<ObjectType>,<ObjectName>" & vbNewLine & "e.g.: SITE,10403_U_1" & vbNewLine & vbNewLine & "Supported ObjectTypes: PLMN,RNC,URNC,SITE,UNODEB,ENODEB,CELL,UCELL,LCELL,LOCID,CLUSTER_ID")
                Exit Sub
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvListContext_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvListContext.RowStyle
        Try
            If e.RowHandle > -1 Then
                Dim dr As DataRowView = gvListContext.GetRow(e.RowHandle)
                If dr IsNot Nothing Then
                    'If dtTechnology.Select("[IOS_TECH]='" & dr.Item("IOS_TECH") & "'").Count = 0 Then
                    '    e.Appearance.BackColor = Color.Red
                    '    e.Appearance.ForeColor = Color.Black
                    'End If
                    If Not SupportedObjects.Contains(dr.Item("ObjectType").ToString) Then
                        If dtObjectTypes.Select("[Object]='" & dr.Item("ObjectType") & "'").Count = 0 Then
                            e.Appearance.BackColor = Color.Red
                            e.Appearance.ForeColor = Color.Black
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grdListContext_KeyUp(sender As Object, e As KeyEventArgs) Handles grdListContext.KeyUp
        If e.KeyCode = Keys.Delete Then
            DeleteListObjects()
        End If
    End Sub

    Private Sub grdListContext_DragDrop(sender As Object, e As DragEventArgs) Handles grdListContext.DragDrop
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim mm() As Object = e.Data.GetData("System.Object[]")
            If mm IsNot Nothing Then
                If mm(0) = "ObjectTreeDrag" Then
                    Dim ndList As List(Of TreeListNode) = CType(mm(1), List(Of TreeListNode))
                    Dim dtContext As DataTable
                    dtContext = CType(grdListContext.DataSource, DataTable)

                    If dtNotSaved Is Nothing Then
                        dtNotSaved = New DataTable()
                        dtNotSaved = dtContext.Clone()
                    End If

                    Dim rIndex() As Integer = gvList.GetSelectedRows()
                    If rIndex.Length > 0 Then
                        Dim dr As DataRow = gvList.GetRow(rIndex(0)).Row
                        For Each nd As TreeListNode In ndList
                            If nd.Nodes.Count = 0 Then
                                Dim drContext As DataRow
                                drContext = dtContext.NewRow
                                drContext("ListID") = dr.Item(0)
                                ' drContext("IOS_TECH") = cmbTechnology.SelectedItem.ToString()
                                drContext("ObjectType") = cmbObjectType.SelectedItem.ToString()
                                drContext("ObjectName") = nd.GetDisplayText("ObjectName")
                                dtContext.Rows.Add(drContext)

                                Dim drNotSaved As DataRow
                                drNotSaved = dtNotSaved.NewRow
                                drNotSaved("ListID") = dr.Item(0)
                                '  drNotSaved("IOS_TECH") = cmbTechnology.SelectedItem.ToString()
                                drNotSaved("ObjectType") = cmbObjectType.SelectedItem.ToString()
                                drNotSaved("ObjectName") = nd.GetDisplayText("ObjectName")
                                dtNotSaved.Rows.Add(drNotSaved)
                            End If
                        Next
                    End If
                End If
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ctrl_DragOver(sender As Object, e As DragEventArgs) Handles grdListContext.DragOver
        If e.Data.GetDataPresent("System.Object[]") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[NB_Cell_List_Objects]", dtNotSaved)
            End If

            Dim listID As Integer = dtNotSaved(0)("ListID")
            Dim parray()() As String = {
                New String() {"@ListID", listID}
            }
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4556, parray)(0), GetSQL(4556, parray)(1))
            SetMessage("List Context Committed Successfully")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            dtNotSaved = Nothing
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiDeleteSelectedRow_Click(sender As Object, e As EventArgs) Handles tsmiDeleteSelectedRow.Click
        DeleteListObjects()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim rIndex() As Integer = gvList.GetSelectedRows()
        If rIndex.Length > 0 Then
            Dim drList As DataRow = gvList.GetRow(rIndex(0)).Row
            FillListObjects(drList.Item(0))
            dtNotSaved = Nothing
        End If
    End Sub

#End Region

#Region "Helper"

    Private Function GetListDetailsByID(listID As Integer) As DataRow
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@listID", listID}}
        strConnection = GetSQL(4573, parray)(0)
        sqlParam = GetSQL(4573, parray)(1)

        Dim dtList As New DataTable()
        dtList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dtList.Rows.Count > 0 Then
            Return dtList.Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Private Sub FillList()
        Dim strConnection As String, sqlParam As String
        strConnection = GetSQL(4512, Nothing)(0)
        sqlParam = GetSQL(4512, Nothing)(1)
        dtCellList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdList, gvList, dtCellList, "ALL", {"ListOwner", "IsPublic"}, "ListName")
        gvList.Columns(0).OptionsColumn.AllowEdit = False
    End Sub

    Private Sub FillListObjects(ListID As Integer)
        Dim strConnection As String, sqlParam As String
        Dim parray()() As String = {New String() {"@ListID", ListID}}

        strConnection = GetSQL(4514, parray)(0)
        sqlParam = GetSQL(4514, parray)(1)
        Dim dtContextGrid As New DataTable
        dtContextGrid = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdListContext, gvListContext, dtContextGrid, "ALL", {"ListID"})
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub DeleteListObjects()
        Try
            If XtraMessageBox.Show("Are you sure to delete selected list objects?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                Dim strConnection As String, sqlParam As String
                Dim parray()() As String = Nothing

                Dim rIndex() As Integer = gvList.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim drList As DataRow = gvList.GetRow(rIndex(0)).Row
                    Dim rowIndex() As Integer = gvListContext.GetSelectedRows()

                    For i As Integer = 0 To rowIndex.Length - 1
                        Dim drContext As DataRowView = gvListContext.GetRow(rowIndex(i))
                        If drContext IsNot Nothing Then
                            parray = {
                                        New String() {"@ListID", drList.Item(0)},
                                        New String() {"@ObjectType", "'" & drContext.Item(1) & "'"},
                                        New String() {"@ObjectName", "'" & drContext.Item(2) & "'"}
                                    }

                            strConnection = GetSQL(4525, parray)(0)
                            sqlParam = GetSQL(4525, parray)(1)
                            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                            parray = Nothing
                        End If
                    Next
                    FillListObjects(drList.Item(0))
                End If
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub DeleteList()
        Try
            If XtraMessageBox.Show("Are you sure to delete selected list?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                Dim strConnection As String, sqlParam As String
                Dim parray()() As String = Nothing
                Dim rowIndex() As Integer

                rowIndex = gvList.GetSelectedRows()
                For i As Integer = 0 To rowIndex.Length - 1
                    Dim dr As DataRowView = gvList.GetRow(rowIndex(i))
                    If dr IsNot Nothing Then
                        parray = {
                                    New String() {"@ListID", dr.Item(0)}
                                }

                        strConnection = GetSQL(4511, parray)(0)
                        sqlParam = GetSQL(4511, parray)(1)
                        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                        parray = Nothing
                    End If
                Next
                FillList()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub InsertBulkDataToServer(ConnString As String, DestinationTable As String, dtData As DataTable)
        Using cn As New System.Data.SqlClient.SqlConnection(ConnString)
            cn.Open()
            Using copy As New System.Data.SqlClient.SqlBulkCopy(cn)

                copy.DestinationTableName = DestinationTable
                copy.BatchSize = 5000
                copy.NotifyAfter = 1000
                AddHandler copy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                copy.ColumnMappings.Add("ListID", "ListID")
                'copy.ColumnMappings.Add("IOS_TECH", "IOS_TECH")
                copy.ColumnMappings.Add("ObjectType", "ObjectType")
                copy.ColumnMappings.Add("ObjectName", "ObjectName")

                copy.WriteToServer(dtData, DataRowState.Added)

            End Using
        End Using
    End Sub

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlClient.SqlRowsCopiedEventArgs)
        lblMessage.Text = "Copied " & args.RowsCopied & " so far..."
    End Sub

#End Region

End Class