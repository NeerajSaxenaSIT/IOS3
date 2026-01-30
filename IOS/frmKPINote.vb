Imports LidorSystems.IntegralUI.Lists
Imports IOS.Library
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO.IsolatedStorage
Imports System.Data.Odbc
Imports dotnetCHARTING.WinForms
Imports IOS.DataLibrary

Public Class frmKPINote

    Dim kpi_ID As Integer = 0
    Dim p As Point = Point.Empty
    Dim chart As dotnetCHARTING.WinForms.Chart
    Dim dtKPI As System.Data.DataTable = Nothing
    Dim defaultNotesData As String = Nothing

    Public Sub SetChart(ByRef chart As dotnetCHARTING.WinForms.Chart)
        Me.chart = chart
    End Sub

    Public Sub SetStatus(ByVal message As String)
        lblMsg.ForeColor = Color.Red
        lblMsg.Visible = True
        lblMsg.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Public Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMsg.Text = ""
        lblMsg.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Public Sub SetKPIInfo(ByVal kpiID As Integer)
        kpi_ID = kpiID
        'Dim tlTipTlv As New ToolTip()
        'tlTipTlv.AutoPopDelay = 1000
        'tlTipTlv.InitialDelay = 1000
        'tlTipTlv.ReshowDelay = 500
        '' Force the ToolTip text to be displayed whether or not the form is active.
        'tlTipTlv.ShowAlways = True
        'tlTipTlv.SetToolTip(lstKPI, "Drag and Drop to Relation Box")

        'dtpNoteDate.EditValue = System.DateTime.Today
        ''Dim sqlCmd As String = "SELECT [SQLKPI_ID],[tech],[sourcetable],[tablealias],[KPI_Name],[KPI_SQL],[Object],[description] FROM [dbo].[IOS_SQL_KPI] WHERE [SQLKPI_ID]='" & kpiID & "'"
        Dim dtKPI As DataTable = clsSQLCommands.GetKpiInfo(connStrIOSServer, kpiID)
        If (dtKPI.Rows.Count > 0) Then
            txtKPIName.Text = dtKPI.Rows(0)("KPI_Name").ToString
            lblTechnology.Text = dtKPI.Rows(0)("tech").ToString
            Dim kpiFormula As String = dtKPI.Rows(0)("KPI_SQL")
            txtKPIFormula.Text = kpiFormula.Substring(0, kpiFormula.Length - txtKPIName.Text.Length)
            lblObjectType.Text = dtKPI.Rows(0)("Object").ToString
            lblKPIDescription.Text = dtKPI.Rows(0)("description").ToString
        End If

        Dim dtTech As DataTable = Me.GetDistinctValue("tech='" & lblTechnology.Text & "'", "Vendor")
        If (dtTech IsNot Nothing AndAlso dtTech.Rows.Count > 0) Then
            lblVendor.Text = dtTech.Rows(0)(0).ToString()
        End If
        SetUsageInChartsAndCounter(kpiID)
        'BindKPINoteGrid(kpiID)

        If ((Not String.IsNullOrEmpty(lblVendor.Text)) AndAlso (Not String.IsNullOrEmpty(lblTechnology.Text))) Then
            GetKPI_onSelectObject(lblTechnology.Text.Trim, lblVendor.Text.Trim) ''"HUAWEI"
        End If
        'BindKPI_ToTLV(kpiID)
        Dim KPINotesFilePath As String = GetConfigClientKeyValue("KPINotesFilePath")
        defaultNotesData = IOSUtility.GetNotesDataFromFile(GetUserDataPath(), KPINotesFilePath)
        'txtNoteDescription.Text = defaultNotesData
    End Sub

    Private Function GetDistinctValue(ByVal filterCondition As String, ByVal ParamArray distinctColumns() As String) As DataTable
        Dim dt As DataTable = Nothing
        If (dt_IOS_ObjectConfig IsNot Nothing) Then
            dt = New DataView(dt_IOS_ObjectConfig, filterCondition, "", DataViewRowState.CurrentRows).ToTable(True, distinctColumns)
        End If
        Return dt
    End Function

    Private Sub SetUsageInChartsAndCounter(ByVal kpiID As String)
        Dim dsKPIUses As DataSet = clsSQLCommands.GetKpiUsageForChartsAndCounter(connStrIOSServer, kpiID)
        If (dsKPIUses.Tables(0).Rows.Count > 0) Then
            tlvUsageInChart.Nodes.Clear()
            tlvUsageInChart.SuspendLayout()
            BindUsageInChartListView(tlvUsageInChart, dsKPIUses.Tables(0))
        End If

        If (dsKPIUses.Tables(1).Rows.Count > 0) Then
            tlvUsageOfCounters.Nodes.Clear()
            BindUsageInChartListView(tlvUsageOfCounters, SetTableName(dsKPIUses.Tables(1)))
        End If
    End Sub

    Private Function SetTableName(ByRef dt As DataTable) As DataTable
        Dim kpiFormula As String
        If (dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                kpiFormula = dr("tablename").ToString
                If (kpiFormula.IndexOf("[dbo]") <> -1) Then
                    dr("tablename") = kpiFormula.ToString
                    Try
                        dr("tablename") = kpiFormula.Substring(kpiFormula.IndexOf("[dbo]") + 6, kpiFormula.IndexOf("<AggregatedObject>") - 21)
                    Catch ex As Exception
                    End Try
                End If
            Next
        End If
        Return dt
    End Function

    Private Sub BindUsageInChartListView(ByRef lst As LidorSystems.IntegralUI.Lists.TreeListView, ByVal dt As DataTable)
        If (dt.Rows.Count > 0) Then
            lst.Nodes.Clear()
            For Each Item As DataRow In dt.Rows
                Dim node As New TreeListViewNode()
                For Each Item1 As DataColumn In dt.Columns
                    Dim s As String = String.Empty
                    Try
                        s = Convert.ToString(Item(Item1))
                    Catch ex As Exception
                        s = ""
                    End Try

                    Dim nodeItem As New TreeListViewSubItem(s)
                    node.SubItems.Add(nodeItem)
                Next
                lst.Nodes.Add(node)
            Next

            lst.UpdateCurrentView()
            For Each col As TreeListViewColumn In lst.Columns
                lst.AutoSizeColumn(col)
            Next
            lst.Refresh()
            lst.ResumeUpdate()
        End If
    End Sub

    Public Sub AddSubNodes(ByRef treenode As TreeListViewNode, ByVal subNode As String)
        Dim childNode As New TreeListViewNode()
        childNode.StyleFromParent = True
        Dim fItem As New TreeListViewSubItem(subNode)
        childNode.SubItems.Add(fItem)
        treenode.Nodes.Add(childNode)
    End Sub

    Public Sub BindParameterToList(ByRef v As DevExpress.XtraEditors.ListBoxControl, ByVal pmData As DataTable, ByVal columnShow As String)
        v.Items.Clear()
        For Each item As DataRow In pmData.Rows
            If (item IsNot DBNull.Value) Then
                v.Items.Add(item(columnShow).ToString())
            End If
        Next
    End Sub

    'Private Sub BindKPINoteGrid(ByVal kpiID As Integer)
    '    Dim dtKPIUses As DataTable = IOSUtility.GetKPINote(kpiID, connStrIOSServer)
    '    If (dtKPIUses.Rows.Count > 0) Then
    '        dtKPIUses.Columns.Remove("KPIName")
    '        IOSDevExpressGrid.PopulateDataInGrid(gcNote, gvNote, dtKPIUses, "ALL")
    '        IOSUtility.KPINotesGridRefreshing(gcNote, gvNote, True, 2)
    '    End If
    'End Sub

    Private Sub RefrashingDTGrid(ByRef gvTemp As DevExpress.XtraGrid.Views.Grid.GridView, ByVal isHeader As Boolean)
        If (gvTemp IsNot Nothing) Then
            gvTemp.OptionsSelection.MultiSelect = True
            gvTemp.OptionsFilter.AllowFilterEditor = True

            Dim gvWidth As Integer = Me.Width - 395

            For Each Item As DevExpress.XtraGrid.Columns.GridColumn In gvTemp.Columns
                If ((Item.Caption).ToUpper = ("NoteTimeStamp").ToUpper) Or ((Item.Caption).ToUpper = ("NoteOwner").ToUpper) Or ((Item.Caption).ToUpper = ("RelatedKPI").ToUpper) Or ((Item.Caption).ToUpper = ("NoteID").ToUpper) Then
                    Item.OptionsColumn.AllowSize = True
                    Item.BestFit()
                ElseIf ((Item.Caption).ToUpper = ("NoteDescription").ToUpper) Then
                    Item.AppearanceCell.TextOptions.WordWrap = True
                    Item.AppearanceHeader.TextOptions.WordWrap = True
                    Item.OptionsColumn.AllowSize = True
                ElseIf ((Item.Caption).ToUpper = ("RelationType").ToUpper) Then
                    Item.Visible = False
                End If
                Item.OptionsFilter.AllowFilter = True
            Next
            gvTemp.OptionsView.ShowColumnHeaders = isHeader
            gvTemp.RefreshData()
        End If
    End Sub

    'Private Sub vbtnSaveNote_Click(sender As Object, e As EventArgs)
    '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
    '    Try
    '        lblMsg.Text = ""
    '        If (Not kpi_ID = 0) Then
    '            If (Not String.IsNullOrEmpty(txtNoteDescription.Text)) Then
    '                If (Not defaultNotesData = txtNoteDescription.Text) Then
    '                    Dim sqlCmd As String = "INSERT INTO [dbo].[IOS_KPI_Notes] ([KPIid],[NoteTimeStamp],[NoteDescription],[NoteOwner])"
    '                    sqlCmd = sqlCmd + " VALUES(?,?,?,?)"
    '                    Dim list As New List(Of System.Data.Odbc.OdbcParameter)
    '                    list.Add(New Odbc.OdbcParameter("kpiid", kpi_ID))
    '                    Dim timeStamp As New System.Data.Odbc.OdbcParameter("date", OdbcType.DateTime)
    '                    timeStamp.Value = dtpNoteDate.EditValue.ToString("yyyy-MM-dd")
    '                    list.Add(timeStamp)
    '                    list.Add(New Odbc.OdbcParameter("Desc", txtNoteDescription.Text.Trim))
    '                    list.Add(New Odbc.OdbcParameter("owner", System.Environment.UserName.ToString.Substring(0, Math.Min(10, System.Environment.UserName.ToString.Length))))
    '                    DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sqlCmd) ', list)
    '                    'BindKPINoteGrid(kpi_ID)
    '                    Dim objFrmTechnology As frmTechnology = Nothing
    '                    frmMDI.OpenTechFormDynamically(lblTechnology.Text, objFrmTechnology, False)
    '                    Dim dtKPI As DataTable = objFrmTechnology.GetChartsKPI(chart)
    '                    objFrmTechnology.CreateXaxisMarkerForKPINotes(chart, dtKPI)
    '                    SetStatus("KPI Note successfully inserted.")
    '                Else
    '                    SetStatus("No Change in Note Text, note not saved...")
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '    End Try
    '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    'End Sub

    Private Sub frm_IOS_KPI_Note_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            'IOSUtility.KPINotesGridRefreshing(gcNote, gvNote, True, 2)
            txtKPIFormula.Width = MyBase.Width
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    'Private Sub vbtnDeleteNote_Click(sender As Object, e As EventArgs)
    '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
    '    Try
    '        If (Not kpi_ID = 0) Then
    '            If (gvNote.GetSelectedRows.Count > 0 AndAlso gvNote.GetRowCellValue(gvNote.GetSelectedRows()(0), gvNote.Columns("NoteOwner")).ToString = Environment.UserName) Then
    '                Dim itemIndex As Integer = gvNote.GetSelectedRows()(0)
    '                If (itemIndex >= 0) Then
    '                    Dim note_ID As String = gvNote.GetRowCellValue(itemIndex, gvNote.Columns(0)).ToString
    '                    ''Dim sqlCmd As String =
    '                    ''        "DELETE FROM IOS_KPI_Notes_Relations where NoteID = (SELECT NoteID from IOS_KPI_Notes where NoteID =  " & note_ID & " AND NoteOwner='" & Environment.UserName & "'); " & _
    '                    ''        "DELETE FROM IOS_KPI_Notes where NoteID =  " & note_ID & " AND NoteOwner='" & Environment.UserName & "';"
    '                    clsSQLCommands.DeleteKpiNotes(connStrIOSServer, note_ID, Environment.UserName)
    '                    BindKPINoteGrid(kpi_ID)
    '                    SetStatus("KPI Note successfully Deleted.")
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '    End Try
    '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    'End Sub

    Private Sub GetKPI_onSelectObject(ByVal techName As String, ByVal vendorName As String)
        ''Dim sql As String = "select distinct KPI_Name,SQLKPI_ID,Creator from IOS_SQL_KPI where Tech ='" & techName & "'"
        dtKPI = clsSQLCommands.GetKpiOnSelectObject(connStrIOSServer, techName)
        'tlvKPIList.Nodes.Clear()
        If Not dtKPI Is Nothing Then
            If (dtKPI.Rows.Count > 0) Then
                'BindKPIListBox(lstKPI, dtKPI, "SQLKPI_ID", "KPI_Name")
            Else
                SetStatus("Selected Tech and Vendor does not have any KPI.")
            End If
        End If
    End Sub

    Private Sub BindKPIListBox(ByRef c As DevExpress.XtraEditors.ListBoxControl, ByVal data As DataTable, ByVal valueField As String, ByVal textField As String)
        c.SuspendLayout()
        c.Items.Clear()
        c.Refresh()
        For Each item As DataRow In data.Rows
            Dim li As New clsComboBoxItem()
            li.Text = item(textField)
            li.Value = item(valueField)
            c.Items.Add(li)
        Next
        c.SelectedIndex = 0
        c.Update()
        c.ResumeLayout()
    End Sub

    'Private Sub vtxtKPISearch_TextChanged(sender As Object, e As EventArgs)
    '    If (String.IsNullOrEmpty(txtKPISearch.Text)) Then
    '        Exit Sub
    '    End If
    '    If (txtKPISearch.Text.Trim.Length > 2) Then
    '        If Not dtKPI Is Nothing Then
    '            If (dtKPI.Rows.Count > 0) Then
    '                Dim dv As New DataView(dtKPI, "KPI_Name LIKE '%" & txtKPISearch.Text.Trim & "%'", "", DataViewRowState.CurrentRows)
    '                BindKPIListBox(lstKPI, dv.ToTable, "SQLKPI_ID", "KPI_Name")
    '            End If
    '        End If
    '    Else
    '        BindKPIListBox(lstKPI, dtKPI, "SQLKPI_ID", "KPI_Name")
    '    End If
    'End Sub

    'Private Sub tlv_KPI_List_DragDrop(sender As Object, e As DragEventArgs)
    '    Dim text As String = e.Data.GetData("System.String")
    '    If Not (String.IsNullOrEmpty(text)) Then
    '        Dim KpiNameAndID As String() = text.Split("/")
    '        If Not (IsItemExist(KpiNameAndID(0), tlvKPIList)) Then
    '            If (Not kpi_ID = 0) Then
    '                If (gvNote.GetSelectedRows.Length = 1) Then
    '                    Dim noteID As String = GetNoteIDFromGrid()
    '                    If (Not noteID = "0") Then
    '                        If (IsNoteValidForKPI(noteID, kpi_ID)) Then
    '                            InsertKPI_Relation(kpi_ID, CType(KpiNameAndID(1), Integer), "Sibling", noteID)
    '                            'BindKPI_ToTLV(kpi_ID)
    '                            'BindKPINoteGrid(kpi_ID)
    '                        Else
    '                            SetStatus("KPI Note not valid.")
    '                        End If
    '                    End If
    '                Else
    '                    SetStatus("KPI Note not selected.")
    '                End If
    '            End If
    '        Else
    '            SetStatus("KPI already have relation.")
    '        End If
    '    End If
    'End Sub

    'Private Function GetNoteIDFromGrid() As String
    '    Dim note_ID As String = "0"
    '    If (gvNote.GetSelectedRows.Length = 1) Then
    '        note_ID = gvNote.GetRowCellValue(gvNote.GetSelectedRows()(0), gvNote.Columns(0)).ToString
    '    End If
    '    Return note_ID
    'End Function

    Private Function IsNoteValidForKPI(ByVal noteId As String, ByVal kpiId As String) As Boolean
        Dim sql As String = Nothing
        ''sql = "SELECT * FROM [dbo].[IOS_KPI_Notes]  Where KPIID=" & kpiId & " AND  NoteID=" & noteId
        Dim data As DataTable = clsSQLCommands.IsNoteValidForKPI(connStrIOSServer, kpiId, noteId)
        If (data.Rows.Count = 1) Then
            Return True
        End If
        Return False
    End Function

    Private Function IsItemExist(ByVal newItem As String, ByRef treeControl As LidorSystems.IntegralUI.Lists.TreeListView) As Boolean
        Dim isKPI As Boolean = False
        For Each tlvNode As TreeListViewNode In treeControl.Nodes
            If (tlvNode.SubItems(0).Text.ToUpper() = newItem.ToUpper()) Then
                isKPI = True
                Exit For
            End If
        Next
        Return isKPI
    End Function

    'Private Sub tlv_KPI_List_DragOver(sender As Object, e As DragEventArgs)
    '    e.Effect = DragDropEffects.Copy
    'End Sub

    'Private Sub tlv_KPI_List_MouseDoubleClick(sender As Object, e As MouseEventArgs)
    '    Try
    '        Application.UseWaitCursor = True
    '        Me.Cursor = Cursors.WaitCursor
    '        Application.DoEvents()
    '        Dim relationType As String = Nothing
    '        Dim relationID As String = Nothing
    '        Dim kpiId As String = Nothing
    '        Dim kpiId_Relation As String = Nothing
    '        If (tlvKPIList.SelectedNode IsNot Nothing) Then
    '            Dim treeSubnode = tlvKPIList.GetSubItem(tlvKPIList.SelectedNode, e.Location)
    '            If (treeSubnode IsNot Nothing) Then
    '                relationID = tlvKPIList.SelectedNode.Tag
    '                kpiId = tlvKPIList.SelectedNode.Key
    '                Dim editSubnode As LidorSystems.IntegralUI.Lists.TreeListViewSubItem = TryCast(treeSubnode, LidorSystems.IntegralUI.Lists.TreeListViewSubItem)
    '                If (editSubnode IsNot Nothing) Then
    '                    If (editSubnode.Index = 1) Then
    '                        kpiId_Relation = editSubnode.Tag
    '                        If (editSubnode.Text.ToUpper = ("Parent").ToUpper) Then
    '                            relationType = "Sibling"
    '                        ElseIf ((editSubnode.Text).ToUpper = ("Sibling").ToUpper) Then
    '                            relationType = "Parent"
    '                        End If

    '                        editSubnode.Text = relationType
    '                        UpdateKPI_RelationType(relationID, kpiId, kpiId_Relation, relationType)
    '                        'BindKPINoteGrid(kpi_ID)
    '                        SetStatus("KPI relation updated")

    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '    Finally
    '        Application.UseWaitCursor = False
    '        Me.Cursor = Cursors.Default
    '        Application.DoEvents()
    '    End Try
    'End Sub

    Private Sub UpdateKPI_RelationType(ByVal relationID As Integer, ByVal kpiID As Integer, ByVal kPIID_Relation As Integer, ByVal relationType As String)
        clsSQLCommands.UpdateKPIRelationType(connStrIOSServer, relationType, relationID, kPIID_Relation, kpiID)
    End Sub

    Private Sub DeleteKPI_Relation(ByVal relationID As Integer, ByVal kpiID As Integer, ByVal kPIID_Relation As Integer)
        clsSQLCommands.DeleteKPIRelationType(connStrIOSServer, relationID, kPIID_Relation, kpiID)
    End Sub

    Private Sub InsertKPI_Relation(ByVal kpiID As Integer, ByVal KPIID_Relation As Integer, ByVal relationType As String, ByVal noteID As Integer)
        clsSQLCommands.AddKPIRelationType(connStrIOSServer, relationType, KPIID_Relation, kpiID, noteID)
    End Sub

    Private Sub GetKPI_RelationByKPIID(ByVal kpiID As Integer)
        clsSQLCommands.GetKPIRelationByKpiID(connStrIOSServer, kpiID)
    End Sub

    'Private Sub tlv_KPI_List_KeyDown(sender As Object, e As KeyEventArgs)
    '    Try
    '        Application.UseWaitCursor = True
    '        Me.Cursor = Cursors.WaitCursor
    '        Application.DoEvents()
    '        If (e.KeyCode = Keys.Delete) Then
    '            If (tlvKPIList.SelectedNode IsNot Nothing) Then
    '                Dim relationID As String = Nothing
    '                Dim kpiId As String = Nothing
    '                Dim kpiId_Relation As String = Nothing
    '                relationID = tlvKPIList.SelectedNode.Tag
    '                kpiId = tlvKPIList.SelectedNode.Key
    '                kpiId_Relation = tlvKPIList.SelectedSubItem.Tag
    '                If (relationID IsNot Nothing AndAlso kpiId IsNot Nothing AndAlso kpiId_Relation IsNot Nothing) Then
    '                    DeleteKPI_Relation(relationID, kpiId, kpiId_Relation)
    '                    tlvKPIList.SelectedNode.Remove()
    '                    SetStatus("KPI Relation successfully Deleted.")
    '                    'BindKPINoteGrid(kpi_ID)
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '    Finally
    '        Application.UseWaitCursor = False
    '        Me.Cursor = Cursors.Default
    '        Application.DoEvents()
    '    End Try
    'End Sub

    'Private Sub BindKPI_ToTLV(ByVal kpiID As Integer)
    '    Dim data As DataTable
    '    data = clsSQLCommands.GetKPIRelationDataToBind(connStrIOSServer, kpiID)

    '    tlvKPIList.Nodes.Clear()
    '    For Each dtRow As DataRow In data.Rows
    '        InsertKPIItemInTLV(dtRow("RelationID"), dtRow("KPIID"), dtRow("KPIID_Relation"), dtRow("RelationType"), dtRow("KPI_Name"))
    '    Next
    '    tlvKPIList.UpdateCurrentView()
    '    tlvKPIList.Refresh()
    '    tlvKPIList.UpdateLayout()
    'End Sub

    'Private Sub InsertKPIItemInTLV(ByVal relationID As String, ByVal KPIID As String, ByVal kPIID_Relation As String, ByVal relationType As String, ByVal kPIName As String)
    '    Try
    '        Dim tlvnode As TreeListViewNode = New TreeListViewNode()
    '        tlvnode.Tag = relationID
    '        tlvnode.Key = KPIID
    '        tlvnode.ToolTip = "Double Click to change KPI relation, Press Del to remove."
    '        Dim tlvnode_KPIName As TreeListViewSubItem = New TreeListViewSubItem(kPIName)
    '        tlvnode_KPIName.Tag = kPIID_Relation
    '        tlvnode.SubItems.Add(tlvnode_KPIName)
    '        Dim tlvnode_RelationType As TreeListViewSubItem = New TreeListViewSubItem(relationType)
    '        tlvnode_RelationType.Tag = kPIID_Relation
    '        tlvnode.SubItems.Add(tlvnode_RelationType)
    '        tlvKPIList.Nodes.Add(tlvnode)
    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '    End Try
    'End Sub

    'Private Sub vlstKPI_MouseDown(sender As Object, e As MouseEventArgs)
    '    Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
    '    p = New Point(e.X, e.Y)
    '    Dim selectedIndex As Integer = listControl.IndexFromPoint(p)
    '    If selectedIndex = -1 Then
    '        p = Point.Empty
    '    End If
    'End Sub

    'Private Sub lstKPI_MouseMove(sender As Object, e As MouseEventArgs)
    '    If e.Button = MouseButtons.Left Then
    '        If (p <> Point.Empty) Then
    '            Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
    '            If (listControl IsNot Nothing) Then
    '                Dim index As Integer = listControl.IndexFromPoint(p)
    '                If (index > -1) Then
    '                    Dim item As clsComboBoxItem = listControl.Items(index)
    '                    If item IsNot Nothing Then
    '                        listControl.DoDragDrop(item.Text & "/" & item.Value, DragDropEffects.Copy)
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub GridView1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
    '    Try
    '        Dim _view As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
    '        If e.Column.AbsoluteIndex = 2 Then
    '            e.Column.AppearanceCell.TextOptions.WordWrap = True
    '            e.Column.AppearanceHeader.TextOptions.WordWrap = True
    '            If ((e.CellValue.ToString()).Contains(vbLf)) Then
    '                Dim countRows As Integer = (e.CellValue.ToString()).Split(vbLf).Count()
    '                _view.SetRowExpanded(e.RowHandle, True)
    '                _view.OptionsView.RowAutoHeight = True
    '                '_view.RowHeight = 20 * countRows
    '            End If
    '            If (gvNote.GetSelectedRows.Count > 0 AndAlso gvNote.GetRowCellValue(e.RowHandle, gvNote.Columns("NoteOwner")).ToString = Environment.UserName) Then
    '                e.Column.OptionsColumn.AllowEdit = True
    '            Else
    '                e.Column.OptionsColumn.AllowEdit = False
    '            End If
    '        Else
    '            e.Column.OptionsColumn.AllowEdit = False
    '        End If

    '        If e.Column.AbsoluteIndex = 3 Then
    '            If (_view.GetRowCellValue(e.RowHandle, _view.Columns(5)).ToString().Contains("Sibling")) Then
    '                e.Appearance.BackColor = Color.Orange
    '                e.Appearance.BackColor2 = Color.White
    '            ElseIf (_view.GetRowCellValue(e.RowHandle, _view.Columns(5)).ToString().Contains("Parent")) Then
    '                e.Appearance.BackColor = Color.Yellow
    '                e.Appearance.BackColor2 = Color.White
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub gvNote_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
    '    'Dim gvEditCell As DevExpress.XtraGrid.Views.Base.GridCell = e.Column.ColumnHandle
    '    If e.RowHandle > -1 AndAlso e.Column.ColumnHandle = 2 Then
    '        Dim newNoteDescription As String = gvNote.GetRowCellValue(e.RowHandle, gvNote.Columns(e.Column.ColumnHandle))
    '        Dim colName As String = e.Column.FieldName
    '        Dim noteID As String = gvNote.GetRowCellValue(e.RowHandle, gvNote.Columns(0))

    '        Dim listPara As New List(Of System.Data.Odbc.OdbcParameter)
    '        listPara.Add(New Odbc.OdbcParameter("noteDescription", newNoteDescription))
    '        listPara.Add(New Odbc.OdbcParameter("noteID", noteID))
    '        Dim sqlCmd As String = "UPDATE [IOS_KPI_Notes] SET [NoteDescription] = ? WHERE NoteID=?"
    '        DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sqlCmd, listPara)
    '    End If
    'End Sub

End Class
