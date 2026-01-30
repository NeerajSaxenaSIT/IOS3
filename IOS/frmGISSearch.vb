Imports LidorSystems.IntegralUI.Lists
Imports IOS.Library
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO.IsolatedStorage
Imports System.Data.Odbc
Imports dotnetCHARTING.WinForms
Imports MapInfo.Data
Imports MapInfo.Engine
Imports MapInfo.Mapping
Imports MapInfo.Geometry

Public Class frmGISSearch

#Region "Variables"

    Dim conStr As String = IOS.Configuration.IOSAppConfigManage.IOSServer
    Dim _enumSelectBy As EnumSelectBy = IOS.Library.EnumSelectBy.None

#End Region

#Region "Form & Controls' Events"

    Private Sub frmGISSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmMapWindow.BindCombowithThematicData(cmbFieldSearch)
        BindCmbTables()
    End Sub

    Private Sub btnSearchGIS_Click(sender As Object, e As EventArgs) Handles btnSearchGIS.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            lblMsg.Text = ""
            If (cmbTable.SelectedIndex > 0) Then
                If (cmbField.SelectedIndex > 0) Then
                    Dim datatype As MapInfo.Data.MIDbType = TryCast(cmbField.SelectedItem, clsComboBoxItem).Value
                    If (cmbValue.SelectedIndex >= 0) Then
                        GetTableFrom(cmbTable.SelectedItem.ToString, cmbField.SelectedItem.ToString, cmbValue.Text, datatype)
                    Else
                        lblMsg.Text = "Enter any value."
                    End If
                Else
                    lblMsg.Text = "Select Field Name."
                End If
            Else
                lblMsg.Text = "Select All Fields"
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frmGISSearch_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub cmbField_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbField.SelectedIndexChanged
        If (cmbField.SelectedIndex > 0) Then
            _enumSelectBy = EnumSelectBy.FromField
            BindValueCombo(cmbTable.SelectedItem.ToString, cmbField.SelectedItem.ToString)
        End If
    End Sub

    Private Sub cmbValue_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValue.SelectedIndexChanged
        If (_enumSelectBy = EnumSelectBy.FromKeyPress) Then
            _enumSelectBy = EnumSelectBy.None
            Exit Sub
        End If
        gcMapTableData.SuspendLayout()
        gcMapTableData.DataSource = Nothing
        gcMapTableData.RefreshDataSource()

        If (cmbValue.SelectedIndex >= 0) Then
            gcMapTableData.SuspendLayout()
            gcMapTableData.DataSource = Nothing
            gcMapTableData.RefreshDataSource()
            btnSearchGIS_Click(Nothing, Nothing)
        End If
        gcMapTableData.ResumeLayout()
    End Sub

    Private Sub cmbValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbValue.KeyPress
        Dim tmp As System.Windows.Forms.KeyPressEventArgs = e
        If tmp.KeyChar = ChrW(Keys.Enter) Then
            _enumSelectBy = EnumSelectBy.FromKeyPress
            If (cmbTable.SelectedIndex > 0) Then
                If (cmbField.SelectedIndex > 0) Then
                    Dim datatype As MapInfo.Data.MIDbType = TryCast(cmbField.SelectedItem, clsComboBoxItem).Value
                    GetTableFrom(cmbTable.SelectedItem.ToString, cmbField.SelectedItem.ToString, cmbValue.Text, datatype)
                Else
                    lblMsg.Text = "Select Field Name."
                End If
            Else
                lblMsg.Text = "Select Table Name."
            End If
        End If
    End Sub

    Private Sub gcMapTableData_CellMouseClick(sender As Object, args As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name)
        Me.Cursor = Cursors.WaitCursor
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim recordID As String = Nothing
            Dim cellID As String = Nothing
            Dim siteCode As String = Nothing
            For k As Integer = 0 To GridView1.GetSelectedRows().Count - 1
                Dim row As DataRow = DirectCast(GridView1.GetRow(GridView1.GetSelectedRows()(k)), DataRowView).Row
                For Each col As DataColumn In row.Table.Columns
                    If (col.Caption.ToUpper = "RECORDID") Then
                        recordID = row(col.ColumnName).ToString
                        Exit For
                    ElseIf (col.Caption.ToUpper = "CELLID") Then
                        cellID = row(col.ColumnName).ToString
                        Exit For
                    ElseIf (col.Caption.ToUpper = "SITECODE") Then
                        siteCode = row(col.ColumnName).ToString
                        Exit For
                    End If
                Next
            Next

            Dim tableName As String = cmbTable.SelectedItem.ToString
            Dim tbl_map As MapInfo.Data.Table = Nothing
            Dim sqlCmd As String
            sqlCmd = "Select * from " & tableName
            If (recordID IsNot Nothing) Then
                sqlCmd = sqlCmd & " WHERE recordID=" & recordID
            ElseIf (cellID IsNot Nothing) Then
                sqlCmd = sqlCmd & " WHERE CellID='" & cellID & "'"
            ElseIf (siteCode IsNot Nothing) Then
                sqlCmd = sqlCmd & " WHERE SiteCode='" & siteCode & "'"
            End If

            Dim connection As New MIConnection
            connection.Open()
            Dim found As Boolean = False
            Dim fGeometory As FeatureGeometry = Nothing
            Dim command As MICommand = connection.CreateCommand()
            Dim irfc As IResultSetFeatureCollection
            command.CommandText = sqlCmd
            irfc = command.ExecuteFeatureCollection()
            connection.Catalog.CloseTable(tableName & "_Map")

            If irfc.Count <> 0 Then
                For Each f As Feature In irfc
                    If Not (found) Then
                        fGeometory = f.Geometry
                        MapInfo.Engine.Session.Current.Selections.DefaultSelection.Clear()
                        MapInfo.Engine.Session.Current.Selections.DefaultSelection.Add(irfc)
                        found = True
                    End If
                Next
            End If
            If found Then
                frmMapWindow.MapControl1.Map.SetView(fGeometory)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus("There is an error. Not able to draw complete data.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub txtFieldSearch_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtFieldSearch.Properties.ButtonClick
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If Not cmbFieldSearch.Text.Contains("Select Item") Then
                'RemoveHandler MapInfo.Engine.Session.Current.Selections.DefaultSelection.SelectionChangedEvent, AddressOf frmMapWindow.Default_SelectionChangedEvent
                frmMapWindow.Cells_SearchAndDisplay(cmbFieldSearch.Text, txtFieldSearch.Text)
                'AddHandler MapInfo.Engine.Session.Current.Selections.DefaultSelection.SelectionChangedEvent, AddressOf frmMapWindow.Default_SelectionChangedEvent
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Helper Methods"

    Public Sub SetConnectionString(ByVal conStr As String)
        Me.conStr = conStr
    End Sub

    Private Sub BindCmbTables()
        cmbTable.SuspendLayout()
        cmbTable.Properties.Items.Clear()
        cmbTable.SelectedText = ""
        cmbTable.Properties.Items.Insert(0, "Select Item")
        Dim tables As List(Of String) = frmMapWindow.GetMapTable()
        If (tables IsNot Nothing) Then
            For Each tableName As String In tables
                cmbTable.Properties.Items.Add(tableName)
            Next
        End If
        cmbTable.ResumeLayout()
        cmbTable.SelectedIndex = 0
    End Sub

    Private Sub BindUsageInChartListView(ByRef lst As LidorSystems.IntegralUI.Lists.TreeListView, ByVal dt As DataTable)
        If (dt.Rows.Count > 0) Then
            lst.Nodes.Clear()
            For Each Item As DataRow In dt.Rows
                Dim node As New TreeListViewNode()
                For Each Item1 As DataColumn In dt.Columns
                    Dim str As String = String.Empty
                    Try
                        str = Convert.ToString(Item(Item1))
                    Catch ex As Exception
                        str = ""
                    End Try

                    Dim nodeItem As New TreeListViewSubItem(str)
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

    Private Sub cmbTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTable.SelectedIndexChanged
        lblMsg.Text = ""
        cmbField.SuspendLayout()
        cmbField.Properties.Items.Clear()
        cmbField.Properties.Items.Insert(0, "Select Item")
        If cmbTable.SelectedIndex > 0 Then
            Dim tableText As String = cmbTable.SelectedItem.ToString
            Dim filteredTable = From w In MapInfo.Engine.Session.Current.Catalog.Cast(Of MapInfo.Data.Table)() _
                                Where w.Alias = tableText _
                                Select w
            Dim table As MapInfo.Data.Table = filteredTable.FirstOrDefault()
            If (table IsNot Nothing) Then
                For Each column As MapInfo.Data.Column In table.TableInfo.Columns
                    Dim lItem As New clsComboBoxItem()
                    lItem.Text = tableText & "." & column.Alias
                    lItem.Value = column.DataType
                    cmbField.Properties.Items.Add(lItem)
                Next
            End If
        End If
        cmbField.SelectedIndex = 0
        cmbField.ResumeLayout()
        gcMapTableData.DataSource = Nothing
        gcMapTableData.RefreshDataSource()
        cmbValue.Properties.Items.Clear()
    End Sub

    Private Sub GetTableFrom(ByVal tableName As String, ByVal fieldName As String, ByVal fieldVale As String, ByVal selectedValueDateType As MapInfo.Data.MIDbType)
        _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name)
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim connection As New MIConnection
            Dim tbl_map As MapInfo.Data.Table = Nothing
            Dim sqlCmd As String
            sqlCmd = "Select * from " & tableName & " "
            If (fieldName IsNot Nothing) Then
                If selectedValueDateType = MapInfo.Data.MIDbType.SmallInt Or selectedValueDateType = MapInfo.Data.MIDbType.Int Or selectedValueDateType = MapInfo.Data.MIDbType.Double Or selectedValueDateType = MapInfo.Data.MIDbType.dBaseDecimal Then
                    sqlCmd = sqlCmd + " Where " & fieldName & "=" & fieldVale
                Else
                    sqlCmd = sqlCmd + " Where " & fieldName & "='" & fieldVale & "'"
                End If
            End If

            connection.Open()
            Dim command As MICommand = connection.CreateCommand()
            Dim irfc As IResultSetFeatureCollection
            command.CommandText = sqlCmd
            irfc = command.ExecuteFeatureCollection()
            connection.Catalog.CloseTable(tableName & "_Map")
            Dim ti_memtbl As MapInfo.Data.TableInfoMemTable = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(tableName & "_Map", MapInfo.Data.TableType.MemTable, irfc), MapInfo.Data.TableInfoMemTable)
            tbl_map = Session.Current.Catalog.CreateTable(ti_memtbl)
            If irfc.Count <> 0 Then
                tbl_map.InsertFeatures(irfc)
                MapInfo.Engine.Session.Current.Selections.DefaultSelection.Clear()
                MapInfo.Engine.Session.Current.Selections.DefaultSelection.Add(irfc)
                frmMapWindow.SetFocusToResultSet(irfc)
            Else
                lblMsg.Text = "No Matching record found"
            End If
            GridTable(tbl_map, tableName)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub

    Private Sub GridTable(ByRef maptbl As MapInfo.Data.Table, ByVal tableName As String)
        Try
            Dim dataTab As System.Data.DataTable = GetTableFromMap(maptbl, tableName)
            gcMapTableData.DataSource = Nothing
            gcMapTableData.RefreshDataSource()
            If (dataTab.Rows.Count > 0) Then
                gcMapTableData.SuspendLayout()
                IOSDevExpressGrid.PopulateDataInGrid(gcMapTableData, GridView1, dataTab, "ALL")
                gcMapTableData.Refresh()
                gcMapTableData.ResumeLayout()
            End If
            dataTab.Dispose()
            dataTab = Nothing
        Catch ex As Exception
            Dim a As String = ex.Message
        End Try
    End Sub

    Private Function GetTableFromMap(ByRef maptbl As MapInfo.Data.Table, ByVal tableName As String) As DataTable
        Try
            Dim dataTab As System.Data.DataTable = Nothing
            dataTab = New System.Data.DataTable(tableName)

            Dim i As Integer
            Dim j As Integer = 0
            For i = 0 To maptbl.TableInfo.Columns.Count - 1
                If maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.FeatureGeometry And maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.Style Then
                    Dim dc As System.Data.DataColumn = dataTab.Columns.Add(maptbl.TableInfo.Columns(i).Alias)
                End If
            Next
            Dim f As MapInfo.Data.Feature
            For Each f In maptbl
                j = 0
                Dim dr As System.Data.DataRow = dataTab.NewRow()
                For i = 0 To maptbl.TableInfo.Columns.Count - 1
                    If maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.FeatureGeometry And maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.Style Then
                        dr(j) = f(i).ToString()
                        j = j + 1
                    End If
                Next
                dataTab.Rows.Add(dr)
            Next
            Return dataTab
        Catch ex As Exception
            Dim a As String = ex.Message
        End Try
        Return Nothing
    End Function

    Private Sub BindValueCombo(ByVal tableName As String, ByVal fieldName As String)
        _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name)
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim connection As New MIConnection
            Dim tbl_map As MapInfo.Data.Table = Nothing
            Dim sqlCmd As String
            sqlCmd = "Select " & fieldName & " from " & tableName & " "
            connection.Open()
            Dim command As MICommand = connection.CreateCommand()
            Dim irfc As IResultSetFeatureCollection
            command.CommandText = sqlCmd
            irfc = command.ExecuteFeatureCollection()
            connection.Catalog.CloseTable(tableName & "_Map")
            Dim ti_memtbl As MapInfo.Data.TableInfoMemTable = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(tableName & "_Map", MapInfo.Data.TableType.MemTable, irfc), MapInfo.Data.TableInfoMemTable)
            tbl_map = Session.Current.Catalog.CreateTable(ti_memtbl)
            If irfc.Count <> 0 Then
                tbl_map.InsertFeatures(irfc)
            Else
                lblMsg.Text = "No Matching record found"
            End If
            GetDataForValueCombo(tbl_map, tableName, fieldName)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try

    End Sub

    Private Sub GetDataForValueCombo(ByRef maptbl As MapInfo.Data.Table, ByVal tableName As String, ByVal fieldName As String)
        Try
            Dim dataTab As System.Data.DataTable = Nothing
            dataTab = New System.Data.DataTable(tableName)

            Dim i As Integer
            Dim j As Integer = 0
            For i = 0 To maptbl.TableInfo.Columns.Count - 1
                If maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.FeatureGeometry And maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.Style Then
                    Dim dc As System.Data.DataColumn = dataTab.Columns.Add(maptbl.TableInfo.Columns(i).Alias)
                End If
            Next
            Dim f As MapInfo.Data.Feature
            For Each f In maptbl
                j = 0
                Dim dr As System.Data.DataRow = dataTab.NewRow()
                For i = 0 To maptbl.TableInfo.Columns.Count - 1
                    If maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.FeatureGeometry And maptbl.TableInfo.Columns(i).DataType <> MapInfo.Data.MIDbType.Style Then
                        dr(j) = f(i).ToString()
                        j = j + 1
                    End If
                Next
                dataTab.Rows.Add(dr)
            Next

            dataTab = GetTableFromMap(maptbl, tableName)
            MapInfo.Engine.Session.Current.Catalog.CloseTable(maptbl.Alias)
            cmbValue.Properties.Items.Clear()

            If (dataTab.Rows.Count > 0) Then
                Dim dtUniqRecords As DataTable = New DataTable()
                dtUniqRecords = dataTab.DefaultView.ToTable(True, dataTab.Columns(0).ColumnName)

                dtUniqRecords.DefaultView.Sort = dataTab.Columns(0).ColumnName & " ASC"
                dtUniqRecords = dtUniqRecords.DefaultView.ToTable

                cmbValue.SuspendLayout()
                For Each dr As DataRow In dtUniqRecords.Rows
                    cmbValue.Properties.Items.Add(dr(0))
                Next
                cmbValue.Properties.Items.Insert("Select Item", 0)
                cmbValue.Refresh()
                cmbValue.ResumeLayout()
                cmbValue.SelectedIndex = 0
            End If

            dataTab.Dispose()
            dataTab = Nothing
        Catch ex As Exception
            Dim a As String = ex.Message
        End Try
    End Sub

    Private Sub cmbFieldSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFieldSearch.SelectedIndexChanged
        If cmbFieldSearch.SelectedIndex > -1 Then
            AttachAutoCompleteWithTextBox(txtFieldSearch, cmbFieldSearch.SelectedItem.ToString, frmMapWindow.cmbNetworkArea.SelectedItem.ToString)
        End If
    End Sub

#End Region

End Class