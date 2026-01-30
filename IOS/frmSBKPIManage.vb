Imports IOS.Configuration
Imports IOS.DataLibrary
Imports IOS.Library
Imports DevExpress.XtraGrid
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmSBKPIManage

#Region "Variables"

    Public kpiGroupID As String = Nothing
    Public kpiGroupName As String = Nothing
    Public kpiGroupOwner As String = Nothing
    Public teckPackValue As String = Nothing
    Dim treeSelectionType As ReportSelectionType = ReportSelectionType.NotSelected

#End Region

#Region "Events"

    Private Sub frmSBKPIManage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lblKPIGroup.Text = Me.kpiGroupName.Trim
            lblKPIGroup.Tag = Me.kpiGroupID.Trim
            lblOwner.Text = Me.kpiGroupOwner.Trim

            LoadGridData()
            BindkpiList()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub TreeListKPI_CustomDrawNodeCell(sender As Object, e As CustomDrawNodeCellEventArgs) Handles treeListKPI.CustomDrawNodeCell
        Try
            If CStr(e.Node.Level = 0) Then
                If e.Column.FieldName = "riChkEdit" Then
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds)
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TreeListKPI_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) 'Handles treeListKPI.ShowingEditor
        Try
            e.Cancel = False
            If treeListKPI.FocusedColumn.FieldName = "riChkEdit" Then
                If treeListKPI.FocusedNode.Level = 0 Then
                    e.Cancel = True
                End If
            End If
            'If treeListKPI.FocusedColumn.FieldName = "KPICategoryName" Then
            '    e.Cancel = True
            'End If
        Catch
        End Try
    End Sub

    Private Sub TreeListKPI_FocusedColumnChanged(sender As Object, e As FocusedColumnChangedEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (e.Column.FieldName = KPIGroupFields.KPI_CATEGORY_NAME) Then
                treeListKPI.OptionsBehavior.Editable = False
                treeListKPI.OptionsBehavior.ReadOnly = True
            ElseIf (e.Column.FieldName = "riChkEdit") Then
                treeListKPI.OptionsBehavior.Editable = True
                treeListKPI.OptionsBehavior.ReadOnly = False
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub TreeListKPI_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs) 'Handles treeListKPI.FocusedNodeChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlNode As TreeListNode = treeListKPI.FocusedNode
            If (tlNode.Level = 0) Then
                Me.treeSelectionType = ReportSelectionType.Category
            ElseIf (tlNode.Level = 1) Then
                Me.treeSelectionType = ReportSelectionType.Kpi
                If (treeListKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME) Then
                    treeListKPI.OptionsBehavior.Editable = False
                    treeListKPI.OptionsBehavior.ReadOnly = True
                ElseIf (treeListKPI.FocusedColumn.FieldName = "riChkEdit") Then
                    treeListKPI.OptionsBehavior.Editable = True
                    treeListKPI.OptionsBehavior.ReadOnly = False
                End If
            Else
                Me.treeSelectionType = ReportSelectionType.NotSelected
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub gvKPI_MouseMove(sender As Object, e As MouseEventArgs) Handles gvKPI.MouseMove
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (e.Button = MouseButtons.Left) Then
                Dim gv As Views.Grid.GridView = TryCast(sender, Views.Grid.GridView)
                If (gv IsNot Nothing) Then
                    Dim gridHI As GridHitInfo = gv.CalcHitInfo(e.Location)
                    Dim row As DataRow = gv.GetDataRow(gridHI.RowHandle)
                    Dim DragDropData As String = row(KPIGroupFields.KPI_ID).ToString & "#" & row(KPIGroupFields.KPI_NAME).ToString
                    gv.GridControl.DoDragDrop(DragDropData, DragDropEffects.Copy)
                    gv.FocusedRowHandle = gridHI.RowHandle
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub treeListKPI_DragDrop(sender As Object, e As DragEventArgs) Handles treeListKPI.DragDrop
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim targetNode As TreeListNode = Nothing
            Dim pt As Point = treeListKPI.PointToClient(New Point(e.X, e.Y))
            targetNode = treeListKPI.CalcHitInfo(pt).Node

            Dim data() As String = e.Data.GetData("System.String").ToString.Split("#")
            If (targetNode IsNot Nothing AndAlso targetNode.Level = 0) Then
                Dim categoryID As String = targetNode.Tag
                Dim kpiID As String = data(0).ToString
                Try
                    Dim result As Integer = DataAccessorODBC.ExecuteScalar(connStrSandBoxServer, SQLKpiCategory.AddKpiWithCategory(categoryID, kpiID))
                    If (result = 0) Then
                        BindkpiList()
                        'SetMessage("KPI Successfully dragged.")
                    Else
                        'SetMessage("Fail : KPI already exists.")
                    End If
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                End Try
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub treeListKPI_DragOver(sender As Object, e As DragEventArgs) Handles treeListKPI.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub treeListKPI_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles treeListKPI.CellValueChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim treeNode As TreeListNode = e.Node
            If (e.Value IsNot Nothing) Then

                Try
                        'If Not (e.Value = treeNode.Item(KPIGroupFields.KPI_CATEGORY_NAME)) Then
                        Dim selectedNodeId As String = treeListKPI.FocusedNode.Tag
                            If (treeNode.Level = 0) Then
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiCategory.ModifyCategory(selectedNodeId, e.Value.ToString))
                        treeNode.Item(KPIGroupFields.KPI_CATEGORY_NAME) = e.Value.ToString
                            End If
                        'End If
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                    Finally
                    End Try

            End If
            treeListKPI.OptionsBehavior.Editable = False
            treeListKPI.OptionsBehavior.ReadOnly = True
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            LoadGridData()
            BindkpiList()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnAddCategory_Click(sender As Object, e As EventArgs) Handles btnAddCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim frmGroupInsert As New dlgSBGroupInsert()
            frmGroupInsert.SetConnectionString(connStrSandBoxServer)
            frmGroupInsert.GroupTypeInserting = GroupType.KpiCategory
            frmGroupInsert.KPIGroupID = lblKPIGroup.Tag
            frmGroupInsert.ShowDialog()
            Dim newCategoryName As String = frmGroupInsert.NewGroup
            If (newCategoryName IsNot Nothing) Then
                If (newCategoryName IsNot Nothing) Then
                    BindkpiList()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadGridData()

        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, StoreProcedurName.QRY_GET_KPI_DATA)
        IOSDevExpressGrid.PopulateDataInGrid(gcKPI, gvKPI, dt, "ALL", Nothing, "MeasurementName")
    End Sub

    Private Sub BindkpiList()
        treeListKPI.BeginUnboundLoad()
        RemoveHandler treeListKPI.FocusedNodeChanged, AddressOf TreeListKPI_FocusedNodeChanged
        RemoveHandler treeListKPI.FocusedColumnChanged, AddressOf TreeListKPI_FocusedColumnChanged
        RemoveHandler treeListKPI.ShowingEditor, AddressOf TreeListKPI_ShowingEditor


        Dim selectTechnologyPackageKPI As String = SQLTechnologyKPIs.GetByTechAndCreator(Me.teckPackValue, lblKPIGroup.Text.Trim)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectTechnologyPackageKPI)

        If (dt.IsValid) Then
            Dim colList() As String = {KPIGroupFields.KPI_CATEGORY_NAME, KPIGroupFields.KPI_CATEGORY_ID, KPIGroupFields.KPI_NAME, KPIGroupFields.KPI_ID}
            treeListKPI.Columns.Clear()
            For i As Integer = 0 To colList.Length - 1
                Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
                col1.Caption = colList(i)
                col1.VisibleIndex = i
                If colList(i) = KPIGroupFields.KPI_CATEGORY_NAME Then
                    treeListKPI.AutoFillColumn = col1
                    col1.Visible = True
                Else
                    col1.Visible = False
                End If
                treeListKPI.Columns.Add(col1)
            Next

            'Adding checkbox column
            Dim chkCol As New Columns.TreeListColumn()
            chkCol.Caption = ""
            chkCol.Name = "chk"
            chkCol.FieldName = "riChkEdit"
            chkCol.VisibleIndex = 4
            chkCol.OptionsColumn.ReadOnly = False
            Dim riChk As New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
            riChk.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard
            riChk.AllowGrayed = False
            riChk.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
            chkCol.ColumnEdit = riChk
            chkCol.Visible = True
            treeListKPI.Columns.Add(chkCol)

            treeListKPI.Nodes.Clear()
            Dim dbNode As TreeListNode = Nothing

            If lblKPIGroup.Text.Trim.ToUpper = "ALL" Then

                Dim tlNode As TreeListNode = treeListKPI.Nodes.Add(New Object() {dt.Rows(0)(KPIGroupFields.KPI_CATEGORY_NAME)})
                tlNode.Tag = dt.Rows(0)(KPIGroupFields.KPI_CATEGORY_ID)

                Dim distinctCol() As String = {KPIGroupFields.KPI_ID, KPIGroupFields.KPI_NAME, KPIGroupFields.KPI_CREATOR}
                Dim dtDistinctGroupName As DataTable = dt.DistinctCol(distinctCol)

                If (dtDistinctGroupName.IsValid) Then
                    Dim drGroupName As DataRow() = dtDistinctGroupName.Select("", KPIGroupFields.KPI_NAME & " ASC ")
                    For Each rowGroupName As DataRow In drGroupName
                        If (Not IsDBNull(rowGroupName(KPIGroupFields.KPI_NAME))) Then
                            dbNode = treeListKPI.AppendNode(New Object() {rowGroupName(KPIGroupFields.KPI_NAME), rowGroupName(KPIGroupFields.KPI_ID), rowGroupName(KPIGroupFields.KPI_CREATOR)}, tlNode)
                            dbNode.Tag = rowGroupName(KPIGroupFields.KPI_ID).ToString
                        End If
                    Next
                End If

            Else

                Dim distinctCol() As String = {KPIGroupFields.KPI_CATEGORY_ID, KPIGroupFields.KPI_CATEGORY_NAME, KPIGroupFields.KPI_CATEGORY_ORDINAL}
                Dim dtSub As DataTable = dt.DistinctCol(distinctCol)

                If (dtSub.IsValid) Then
                    Dim drCatName As DataRow() = dtSub.Select("", KPIGroupFields.KPI_CATEGORY_ORDINAL & " ASC ")

                    For Each rowCatName As DataRow In drCatName
                        If (Not IsDBNull(rowCatName(KPIGroupFields.KPI_CATEGORY_NAME))) Then
                            dbNode = treeListKPI.Nodes.Add(New Object() {rowCatName(KPIGroupFields.KPI_CATEGORY_NAME)})
                            dbNode.Tag = rowCatName(KPIGroupFields.KPI_CATEGORY_ID).ToString

                            Dim kpiFilter As String = KPIGroupFields.KPI_CATEGORY_ID & " = " & rowCatName(KPIGroupFields.KPI_CATEGORY_ID)
                            Dim dtKpi As DataTable = dt.SelectedRowsAsTable(kpiFilter)

                            Dim distinctColKPI() As String = {KPIGroupFields.KPI_ID, KPIGroupFields.KPI_NAME, KPIGroupFields.KPI_CREATOR}
                            Dim dtDistinctKPI As DataTable = dtKpi.DistinctCol(distinctColKPI)

                            If dtKpi.IsValid Then
                                Dim dr As DataRow() = dtDistinctKPI.Select("", KPIGroupFields.KPI_NAME & " ASC ")
                                For Each drow As DataRow In dr
                                    Dim rptNode As TreeListNode = treeListKPI.AppendNode(New Object() {drow.Item(KPIGroupFields.KPI_NAME).ToString, drow.Item(KPIGroupFields.KPI_ID).ToString, drow.Item(KPIGroupFields.KPI_CREATOR).ToString}, dbNode)
                                    rptNode.Tag = drow.Item(KPIGroupFields.KPI_ID).ToString
                                Next
                            End If
                        End If
                    Next
                End If

            End If
        Else
            SandBoxTreeView.Clear(treeListKPI)
        End If

        treeListKPI.EndUnboundLoad()
        If treeListKPI.Nodes.Count > 0 Then
            treeListKPI.SelectNode(treeListKPI.Nodes(0))
            treeListKPI.SetFocusedNode(treeListKPI.Nodes(0))
            treeListKPI.AutoFillColumn = treeListKPI.Columns(0)
            treeListKPI.ExpandAll()
        End If

        treeListKPI.OptionsBehavior.Editable = False
        treeListKPI.OptionsBehavior.ReadOnly = True

        AddHandler treeListKPI.FocusedNodeChanged, AddressOf TreeListKPI_FocusedNodeChanged
        AddHandler treeListKPI.FocusedColumnChanged, AddressOf TreeListKPI_FocusedColumnChanged
        AddHandler treeListKPI.ShowingEditor, AddressOf TreeListKPI_ShowingEditor
    End Sub

#End Region

#Region "Context Menu"

    Private Sub cms_KPIManager_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_KPIManager.Opening
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (treeListKPI.Nodes.Count > 0) Then
                treeListKPI.ContextMenuStrip.Show()
            Else
                e.Cancel = True
                treeListKPI.ContextMenuStrip.Hide()
                Return
            End If

            If (lblOwner.Text.Trim.ToUpper = Environment.UserName.ToUpper) Then
                If (Me.treeSelectionType = ReportSelectionType.Category) Then
                    tsmi_RenameCategory.Enabled = True
                    tsmi_DeleteCategory.Enabled = True
                    tsmi_DeleteKPI.Enabled = False
                ElseIf (Me.treeSelectionType = ReportSelectionType.Kpi) Then
                    tsmi_RenameCategory.Enabled = False
                    tsmi_DeleteCategory.Enabled = False
                    tsmi_DeleteKPI.Enabled = True
                End If
            Else
                tsmi_RenameCategory.Enabled = False
                tsmi_DeleteCategory.Enabled = False
                tsmi_DeleteKPI.Enabled = False
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_RenameCategory_Click(sender As Object, e As EventArgs) Handles tsmi_RenameCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (treeListKPI.FocusedNode.Level = 0) Then
                If treeListKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME Then
                    treeListKPI.OptionsBehavior.Editable = True
                    treeListKPI.OptionsBehavior.ReadOnly = False
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_DeleteCategory_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (treeListKPI.FocusedNode.Level = 0) Then
                If treeListKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiCategory.DeleteCategory(treeListKPI.FocusedNode.Tag, kpiGroupID))
                    treeListKPI.Nodes.Remove(treeListKPI.FocusedNode)
                    treeListKPI.Refresh()
                    'BindkpiList()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_DeleteKPI_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteKPI.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (treeListKPI.FocusedNode.Level = 1) Then
                If treeListKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiGroup.RemoveKPIFromCategory(treeListKPI.FocusedNode.ParentNode.Tag, treeListKPI.FocusedNode.Tag))
                    treeListKPI.Nodes.Remove(treeListKPI.FocusedNode)
                    treeListKPI.Refresh()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class