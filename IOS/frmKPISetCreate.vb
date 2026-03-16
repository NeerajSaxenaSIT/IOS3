Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.Utils.DragDrop
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList

Public Class frmKPISetCreate

#Region "Variables"

    Private dtPMKpiSetList As DataTable = Nothing
    Private dtKpiList As DataTable = Nothing
    Private KPIDragOrSwap As String = Nothing

    Public kpiSetTech As String = Nothing
    Public newKpiSetName As String = Nothing
    Public kpiSetID As Integer = Nothing
    Public kpiSetDeleted As Boolean = False

#End Region

#Region "Events"

    Private Sub frmKPISetCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadKPISetList()
            SetComboBox(cmbKPISets, ComboSelectBased.ValueBased, Me.kpiSetID)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim objKPISetCreate As New dlgKPISetCreate()
            objKPISetCreate.kpiSetTech = Me.kpiSetTech
            objKPISetCreate.ShowDialog()
            If objKPISetCreate.DialogResult = DialogResult.OK Then
                LoadKPISetList()
                SetComboBox(cmbKPISets, ComboSelectBased.TextBased, Me.newKpiSetName)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim _kpiSetID As Integer = Nothing
            Dim isPowerUser As Boolean = False

            If cmbKPISets.SelectedIndex > 0 Then
                _kpiSetID = CInt(TryCast(cmbKPISets.SelectedItem, clsComboBoxItem).Value)
                Dim kpiSetOwner As String = dtPMKpiSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("KPISetID") = _kpiSetID)(0)("Owner").ToString
                If kpiSetOwner.ToLower <> Environment.UserName.ToLower Then
                    If configMgr.User.IsPowerUser = True Then
                        isPowerUser = True
                    Else
                        XtraMessageBox.Show("Current user can't delete the KPI Set as the owner is a different user.", "Delete KPI Set!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    isPowerUser = True
                End If
                If isPowerUser = True Then
                    If XtraMessageBox.Show("Are you sure to delete KPI Set: " & cmbKPISets.SelectedItem.ToString & "?", "Delete KPI Set!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        DeleteKPISet(_kpiSetID)
                        kpiSetDeleted = True
                        'GetKPISetList(kpiSetTech)
                        LoadKPISetList()
                        cmbKPISets_SelectedIndexChanged(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbKPISets_SelectedIndexChanged(sender As Object, e As EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If cmbKPISets.SelectedIndex > 0 Then
                'lblKPISetName.Text = cmbKPISets.SelectedItem.ToString
                Me.newKpiSetName = cmbKPISets.SelectedItem.ToString
                LoadKPIsForKPISet()
                LoadKPIsForTechAndCounter()
                Dim isPowerUser As Boolean = False
                Dim kpiSetOwner As String = dtPMKpiSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("KPISetID") = CInt(TryCast(cmbKPISets.SelectedItem, clsComboBoxItem).Value))(0)("Owner")
                If kpiSetOwner <> Environment.UserName Then
                    If configMgr.User.IsPowerUser = True Then
                        isPowerUser = True
                    End If
                Else
                    isPowerUser = True
                End If
                If isPowerUser = True Then
                    tlKPISetKPIsList.Enabled = True
                    btnDelete.Enabled = True
                    picDrag.Enabled = True
                Else
                    tlKPISetKPIsList.Enabled = False
                    btnDelete.Enabled = False
                    picDrag.Enabled = False
                End If
            Else
                tlKPISetKPIsList.Columns.Clear()
                tlKPISetKPIsList.Nodes.Clear()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gridControl_DragOver(sender As Object, e As DragEventArgs) Handles tlKPISetKPIsList.DragOver   'gcKPIList4TechCounter.DragOver,
        e.Effect = DragDropEffects.Copy
    End Sub

    'Private downHitInfo As GridHitInfo = Nothing
    'Private Sub gvKPIList4TechCounter_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gvKPIList4TechCounter.MouseDown
    '    Dim view As GridView = TryCast(sender, GridView)
    '    downHitInfo = Nothing
    '    Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
    '    If Control.ModifierKeys <> Keys.None Then
    '        Return
    '    End If
    '    If e.Button = MouseButtons.Left AndAlso hitInfo.InRow AndAlso hitInfo.RowHandle <> GridControl.NewItemRowHandle Then
    '        downHitInfo = hitInfo
    '    End If
    'End Sub

    'Private Sub gcKPIList4TechCounter_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gcKPIList4TechCounter.MouseMove
    '    Dim gc As GridControl = TryCast(sender, GridControl)
    '    Dim view As GridView = TryCast(gc.MainView, GridView)
    '    If e.Button = MouseButtons.Left AndAlso downHitInfo IsNot Nothing Then
    '        Dim dragSize As Size = SystemInformation.DragSize
    '        Dim dragRect As New Rectangle(New Point(downHitInfo.HitPoint.X - dragSize.Width \ 2, downHitInfo.HitPoint.Y - dragSize.Height \ 2), dragSize)

    '        If (dragRect.Contains(New Point(e.X, e.Y))) Then
    '            view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.Copy)
    '            downHitInfo = Nothing
    '        End If
    '    End If
    'End Sub

    Private Sub tlKPISetKPIsList_DragDrop(sender As Object, e As DragEventArgs) Handles tlKPISetKPIsList.DragDrop
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim sourceRow As Integer = Nothing
            Dim targetRow As Integer = Nothing

            Dim tl As TreeList = TryCast(sender, TreeList)
            Dim targetNode As TreeListNode = Nothing

            Dim pt As Point = tl.PointToClient(New Point(e.X, e.Y))
            Try
                targetNode = tl.CalcHitInfo(pt).Node
                targetRow = tl.CalcHitInfo(pt).Node.Tag
            Catch
            End Try

            'Dim srcHitInfo As GridHitInfo = TryCast(e.Data.GetData(GetType(GridHitInfo)), GridHitInfo)
            'If Not srcHitInfo Is Nothing Then
            '    If srcHitInfo.View.SelectedRowsCount = 1 Then
            '        sourceRow = srcHitInfo.RowHandle
            '        KPIDragOrSwap = "KPIDrag"

            '        SaveDraggedKPI(sourceRow, targetRow, targetNode)
            '        LoadKPIsForKPISet()
            '    Else
            '        Dim selRows() As Integer = gvKPIList4TechCounter.GetSelectedRows()
            '        For i As Integer = 0 To selRows.Length - 1
            '            sourceRow = selRows(i)
            '            KPIDragOrSwap = "KPIDrag"

            '            SaveDraggedKPI(sourceRow, targetRow, targetNode)
            '            LoadKPIsForKPISet()
            '        Next
            '    End If
            'Else
            'disallowing multiple KPIs for ordinal swapping
            If tlKPISetKPIsList.Selection.Count = 1 Then
                sourceRow = e.Data.GetData(GetType(TreeListNode)).Tag
                KPIDragOrSwap = "OrdinalSwap"

                SaveDraggedKPI(sourceRow, targetRow, targetNode)
                LoadKPIsForKPISet()
            End If
            'End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            ManageTreeColumns()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub picDrag_Click(sender As Object, e As EventArgs) Handles picDrag.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim sourceRow As Integer = Nothing
            Dim targetRow As Integer = Nothing
            Dim targetNode As TreeListNode = Nothing

            targetNode = tlKPISetKPIsList.FocusedNode
            If targetNode Is Nothing Then
                targetRow = 0
            Else
                targetRow = targetNode.Tag
            End If

            If gvKPIList4TechCounter.SelectedRowsCount = 1 Then
                sourceRow = gvKPIList4TechCounter.FocusedRowHandle
                Dim kpiName As String = gvKPIList4TechCounter.GetRowCellValue(sourceRow, "KPI_Name")
                If dtKpiList.AsEnumerable().Where(Function(x) x.Field(Of String)("KPI_Name").Equals(kpiName)).Count = 0 Then
                    KPIDragOrSwap = "KPIDrag"

                    SaveDraggedKPI(sourceRow, targetRow, targetNode)
                    LoadKPIsForKPISet()
                End If
            Else
                Dim selRows() As Integer = gvKPIList4TechCounter.GetSelectedRows()
                For i As Integer = 0 To selRows.Length - 1
                    sourceRow = selRows(i)
                    Dim kpiName As String = gvKPIList4TechCounter.GetRowCellValue(sourceRow, "KPI_Name")
                    If dtKpiList.AsEnumerable().Where(Function(x) x.Field(Of String)("KPI_Name").Equals(kpiName)).Count = 0 Then
                        KPIDragOrSwap = "KPIDrag"

                        SaveDraggedKPI(sourceRow, targetRow, targetNode)
                    End If
                Next
                LoadKPIsForKPISet()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            ManageTreeColumns()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tlKPISetKPIsList_MouseMove(sender As Object, e As MouseEventArgs) Handles tlKPISetKPIsList.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim node As TreeListNode = tlKPISetKPIsList.FocusedNode
                Dim data As TreeListNode = tlKPISetKPIsList.GetNodeAt(e.Location)
                If data IsNot Nothing Then
                    tlKPISetKPIsList.DoDragDrop(data, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlKPISetKPIsList_KeyDown(sender As Object, e As KeyEventArgs) Handles tlKPISetKPIsList.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If XtraMessageBox.Show("Are you sure to delete selected KPI(s)?", "Delete KPI(s)!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()

                    For Each tlNode In tlKPISetKPIsList.Selection
                        DeleteKPIFromKPISet(CInt(tlNode.Tag))
                    Next

                    LoadKPIsForKPISet()
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

    Private Sub tlKPISetKPIsList_ColumnFilterChanged(sender As Object, e As EventArgs) Handles tlKPISetKPIsList.ColumnFilterChanged
        Try
            Dim filterString As String = tlKPISetKPIsList.FilterPanelText
            If filterString.Trim <> "" Then
                If dtKpiList.Select(filterString).Length <> 0 Then
                    Dim dt As DataTable = dtKpiList.Select(filterString).CopyToDataTable()
                    BindKPIsForKPISet(dt)
                Else
                    BindKPIsForKPISet(dtKpiList)
                End If
            Else
                BindKPIsForKPISet(dtKpiList)
            End If

            ManageTreeColumns()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub frmKPISetCreate_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            ManageTreeColumns()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim _kpiSetID As Integer = Nothing
            Dim isPowerUser As Boolean = False

            If cmbKPISets.SelectedIndex > 0 Then
                _kpiSetID = CInt(TryCast(cmbKPISets.SelectedItem, clsComboBoxItem).Value)
                Dim kpiSetOwner As String = dtPMKpiSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("KPISetID") = _kpiSetID)(0)("Owner").ToString
                If kpiSetOwner.ToLower <> Environment.UserName.ToLower Then
                    If configMgr.User.IsPowerUser = True Then
                        isPowerUser = True
                    Else
                        XtraMessageBox.Show("Current user can't rename the KPI Set as the owner is a different user.", "Rename KPI Set!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    isPowerUser = True
                End If
                If isPowerUser = True Then
                    Dim renamedKPISet As String = Nothing
                    renamedKPISet = XtraInputBox.Show("Rename KPI Set:", "Rename KPI Set", cmbKPISets.SelectedItem.ToString, MessageBoxButtons.OKCancel)
                    If renamedKPISet.Trim <> "" Then
                        RenameKPISet(_kpiSetID, renamedKPISet)
                        LoadKPISetList()
                        SetComboBox(cmbKPISets, ComboSelectBased.TextBased, renamedKPISet)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadKPISetList()
        RemoveHandler cmbKPISets.SelectedIndexChanged, AddressOf cmbKPISets_SelectedIndexChanged
        dtPMKpiSetList = GetKPISetList(Me.kpiSetTech)
        BindDevExComboBoxWithValueMember(cmbKPISets, dtPMKpiSetList, "KPISetID", "KPISetName", "Select KPI Set", False)
        AddHandler cmbKPISets.SelectedIndexChanged, AddressOf cmbKPISets_SelectedIndexChanged
    End Sub

    Private Sub LoadKPIsForKPISet()
        tlKPISetKPIsList.OptionsFilter.FilterMode = FilterMode.Matches
        Dim selectedKPISetID As Integer = CInt(TryCast(cmbKPISets.SelectedItem, clsComboBoxItem).Value)
        Dim parray()() As String = {
            New String() {"@KPISetID", selectedKPISetID},
            New String() {"@IOSTech", Chr(39) & Me.kpiSetTech & Chr(39)}
        }
        Dim strConnection As String = GetSQL(7006, parray)(0)
        Dim sqlParam As String = GetSQL(7006, parray)(1)
        dtKpiList = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim cols2Hide() As String = {"KPISetID"}
        BindKPIsForKPISet(dtKpiList)
    End Sub

    Private Sub BindKPIsForKPISet(ByRef dt As DataTable)
        Try
            If dt IsNot Nothing Then
                tlKPISetKPIsList.Columns.Clear()
                Dim colsList As New List(Of String)
                For Each col As DataColumn In dt.Columns
                    colsList.Add(col.ColumnName)
                Next

                tlKPISetKPIsList.OptionsView.AutoWidth = False

                Dim i As Integer = 0
                For Each colName In colsList
                    Dim col As Columns.TreeListColumn = New Columns.TreeListColumn()
                    col.Caption = colName
                    col.VisibleIndex = i
                    col.OptionsFilter.AllowFilter = True
                    If colName = "KPISetDetailID" Or colName = "KPISetID" Or colName = "KPIOrdinal" Then
                        col.Visible = False
                    Else
                        If colName = "KPI_Name" Then
                            tlKPISetKPIsList.AutoFillColumn = col
                        End If
                        col.Visible = True
                    End If
                    tlKPISetKPIsList.Columns.Add(col)
                    i = i + 1
                Next

                tlKPISetKPIsList.Nodes.Clear()
                tlKPISetKPIsList.OptionsView.AutoWidth = True

                Dim dbNode As TreeListNode = Nothing
                If dt.IsValid Then
                    Dim tlNode As TreeListNode = Nothing
                    Dim drKpiOrdinal As DataRow() = dt.AsEnumerable().Where(Function(x) Not IsDBNull(x.Field(Of Integer)("KPIOrdinal"))).OrderBy(Function(x) x.Field(Of Integer)("KPIOrdinal")).ToArray()
                    For Each dr As DataRow In drKpiOrdinal
                        dbNode = tlKPISetKPIsList.AppendNode(New Object() {dr("SQLKPIID"), dr("IOSTech"), dr("CounterType"), dr("KPI_Name"), dr("KPISetID"), dr("KPIOrdinal")}, tlNode)
                        dbNode.Tag = dr("KPISetDetailID")
                    Next
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            'If Not dt Is Nothing Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            ManageTreeColumns()
            tlKPISetKPIsList.EndUnboundLoad()
            If tlKPISetKPIsList.Nodes.Count > 0 Then
                tlKPISetKPIsList.AutoFillColumn = tlKPISetKPIsList.Columns("KPI_Name")
                tlKPISetKPIsList.SelectNode(tlKPISetKPIsList.Nodes(0))
                tlKPISetKPIsList.SetFocusedNode(tlKPISetKPIsList.Nodes(0))
                tlKPISetKPIsList.ExpandAll()
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub LoadKPIsForTechAndCounter()
        Dim counterType As String = dtPMKpiSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("KPISetID") = CInt(TryCast(cmbKPISets.SelectedItem, clsComboBoxItem).Value))(0)("CounterType").ToString
        Dim parray()() As String = {
            New String() {"@Tech", Chr(39) & kpiSetTech & Chr(39)},
            New String() {"@CounterType", Chr(39) & counterType & Chr(39)}
        }
        Dim strConnection As String = GetSQL(7005, parray)(0)
        Dim sqlParam As String = GetSQL(7005, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(gcKPIList4TechCounter, gvKPIList4TechCounter, dt, "ALL", Nothing, "KPI_Name")
    End Sub

    Private Sub DeleteKPISet(kpiSetID As Integer)
        Dim parray()() As String = {
            New String() {"@KPISetID", kpiSetID}
        }
        Dim strConnection As String = GetSQL(7010, parray)(0)
        Dim sqlParam As String = GetSQL(7010, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub SaveDraggedKPI(srcRow As Integer, trgtRow As Integer, Optional trgtNode As TreeListNode = Nothing)
        Select Case KPIDragOrSwap.ToLower
            Case "kpidrag"
                Dim kpiOrdinal As String = Nothing
                If (trgtNode Is Nothing) AndAlso (trgtRow = 0) Then
                    If tlKPISetKPIsList.Nodes.Count = 0 Then
                        kpiOrdinal = "NULL"
                    Else
                        kpiOrdinal = tlKPISetKPIsList.Nodes(tlKPISetKPIsList.Nodes.Count - 1)("KPIOrdinal")
                    End If
                Else
                    kpiOrdinal = CInt(trgtNode.GetDisplayText("KPIOrdinal"))
                End If

                Dim dr As DataRow = gvKPIList4TechCounter.GetDataRow(srcRow)
                Dim parray()() As String = {
                    New String() {"@KPISetID", CInt(CType(cmbKPISets.SelectedItem, clsComboBoxItem).Value)},
                    New String() {"@SqlKpiID", CInt(dr("SQLKPI_ID"))},
                    New String() {"@KpiOrdinal", kpiOrdinal}
                }
                Dim strConnection As String = GetSQL(7021, parray)(0)
                Dim sqlParam As String = GetSQL(7021, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            Case "ordinalswap"
                Dim parray()() As String = {
                    New String() {"@SrcKPISetDetailID", srcRow},
                    New String() {"@TrgKPISetDetailID", trgtRow}
                }
                Dim strConnection As String = GetSQL(7020, parray)(0)
                Dim sqlParam As String = GetSQL(7020, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        End Select
    End Sub

    Private Sub DeleteKPIFromKPISet(kpiSetDetailID As Integer)
        Dim parray()() As String = {
            New String() {"@KPISetDetailID", kpiSetDetailID}
        }
        Dim strConnection As String = GetSQL(7026, parray)(0)
        Dim sqlParam As String = GetSQL(7026, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub ManageTreeColumns()
        For Each col As Columns.TreeListColumn In tlKPISetKPIsList.Columns
            If col.Caption = "KPISetDetailID" Or col.Caption = "KPISetID" Or col.Caption = "KPIOrdinal" Then
                'do nothing
            Else
                If col.Caption = "KPI_Name" Then
                    tlKPISetKPIsList.AutoFillColumn = col
                    col.Width = CInt(Math.Round(tlKPISetKPIsList.Width * 0.6))
                Else
                    col.Width = CInt(Math.Round(tlKPISetKPIsList.Width * 0.3))
                End If
            End If
        Next
    End Sub

    Private Sub RenameKPISet(kpiSetID As Integer, kpiSetName As String)
        Dim parray()() As String = {
            New String() {"@KPISetName", Chr(39) & kpiSetName & Chr(39)},
            New String() {"@KPISetID", kpiSetID}
        }
        Dim strConnection As String = GetSQL(7042, parray)(0)
        Dim sqlParam As String = GetSQL(7042, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

#End Region

#Region "Context Menu"

    Private Sub tsmi_DeleteKPIs_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteKPIs.Click
        Try
            If XtraMessageBox.Show("Are you sure to delete selected KPI(s)?", "Delete KPI(s)!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                For Each tlNode In tlKPISetKPIsList.Selection
                    DeleteKPIFromKPISet(CInt(tlNode.Tag))
                Next

                LoadKPIsForKPISet()
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

End Class