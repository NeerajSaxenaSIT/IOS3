Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class DataMartGridView

    Public Shared Sub ClearGrid(ByRef gridControl As GridControl, ByRef gridView As DevExpress.XtraGrid.Views.Grid.GridView)
        gridControl.SuspendLayout()
        gridView.Columns.Clear()
        gridControl.DataSource = Nothing
        gridControl.Refresh()
        gridControl.ResumeLayout()
    End Sub

    Public Shared Sub SetData(ByRef gridControl As GridControl, ByRef gridView As Views.Grid.GridView, ByVal dt As DataTable, ByVal fit As String)
        Try
            gridView.OptionsBehavior.AutoPopulateColumns = True
            gridView.Columns.Clear()
            With gridControl
                .ResumeLayout(False)
                .DataSource = Nothing
                .Refresh()
                .DataSource = dt
                .ResumeLayout(True)
                .Refresh()
                For k = 0 To gridView.Columns.Count - 1
                    gridView.Columns(k).OptionsFilter.AllowFilter = True
                    If fit = "ALL" Then
                        gridView.Columns(k).Resize(gridView.Columns(k).GetBestWidth())
                    Else
                        gridView.Columns(k).BestFit()
                    End If
                Next
                .ResumeLayout(True)
                .Refresh()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub RefreshingGrid(ByRef gvTemp As GridControl, ByRef gView As Views.Grid.GridView, ByVal isHeader As Boolean)
        If (gvTemp IsNot Nothing) Then
            'gView.RowsHierarchy.Visible = False
            'gvTemp.AllowContextMenuFiltering = True
            For Each Item As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                Item.OptionsColumn.AllowSize = True
                Item.Resize(10)
                'Item.AllowFiltering = True
            Next
        End If
    End Sub

    Public Shared Sub RefreshingGrid(ByRef gcTemp As GridControl, ByRef gvTemp As Views.Grid.GridView, ByVal isHeader As Boolean, ByVal isRowsVisible As Boolean, ByVal isMultipleSelection As Boolean)
        If (gcTemp IsNot Nothing) Then
            gvTemp.OptionsSelection.MultiSelect = isMultipleSelection
            gvTemp.OptionsFilter.AllowFilterEditor = True
            For Each Item As Columns.GridColumn In gvTemp.Columns
                Item.OptionsColumn.AllowSize = True
                Item.BestFit()
                Item.OptionsFilter.AllowFilter = True
            Next
            'gvTemp.ColumnsHierarchy.Visible = isHeader
        End If
    End Sub

    Public Shared Sub RefreshingGrid(ByRef gcTemp As GridControl, ByRef gvTemp As Views.Grid.GridView, ByVal isHeader As Boolean, ByVal isRowsVisible As Boolean)
        If (gvTemp IsNot Nothing) Then
            'gvTemp.RowsHierarchy.Visible = isRowsVisible
            'gcTemp.AllowContextMenuFiltering = True
            For Each Item As Columns.GridColumn In gvTemp.Columns
                Item.OptionsColumn.AllowSize = True
                Item.BestFit()
                Item.OptionsFilter.AllowFilter = True
            Next
            'gvTemp.ColumnsHierarchy.Visible = isHeader
        End If
    End Sub

    Public Shared Sub SelectAllAndCopyGridData(ByRef gControl As GridControl, ByVal gView As GridView, Optional IsCopyAll As Boolean = True, Optional ByVal IsIncludeHeader As Boolean = True, Optional ColumnListToExclude() As String = Nothing)
        Dim stringToCopy As String = ""
        Try
            Dim colList As New List(Of String)
            If ColumnListToExclude IsNot Nothing Then
                colList = ColumnListToExclude.ToList
            End If
            If IsCopyAll Then
                Dim dtData As DataTable = TryCast(gControl.DataSource, DataTable)
                If dtData Is Nothing Then Exit Sub
                If IsIncludeHeader = True Then
                    For Each Col As DataColumn In dtData.Columns
                        If Not colList.Exists(Function(x) x.ToLower = Col.Caption.ToLower) Then
                            stringToCopy = stringToCopy + Col.Caption + vbTab
                        End If
                    Next
                    If stringToCopy = "" Then Exit Sub
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy + vbCr
                End If

                For Each row As DataRow In dtData.Rows
                    For Each Col As DataColumn In dtData.Columns
                        If Not colList.Exists(Function(x) x.ToLower = Col.Caption.ToLower) Then
                            stringToCopy = stringToCopy + row.Item(Col).ToString + vbTab
                        End If
                    Next
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy & vbCr
                Next
            Else
                If gView.IsCellSelect = False Then
                    If IsIncludeHeader Then
                        For Each Col As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                            If Not colList.Exists(Function(x) x.ToLower = Col.FieldName.ToLower) Then
                                stringToCopy = stringToCopy + Col.FieldName + vbTab
                            End If
                        Next
                        If stringToCopy = "" Then Exit Sub
                        stringToCopy = stringToCopy.TrimEnd(vbTab)
                        stringToCopy = stringToCopy + vbCr
                    End If

                    Dim rowIndex() As Integer = gView.GetSelectedRows()
                    For i As Integer = 0 To rowIndex.Length - 1
                        For Each Col As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                            If Not colList.Exists(Function(x) x.ToLower = Col.FieldName.ToLower) Then
                                stringToCopy = stringToCopy & gView.GetRowCellValue(rowIndex(i), Col) & vbTab
                            End If
                        Next
                    Next
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy + vbCr
                Else
                    Dim cellObj() As DevExpress.XtraGrid.Views.Base.GridCell = gView.GetSelectedCells()

                    If IsIncludeHeader Then
                        For Each cell As DevExpress.XtraGrid.Views.Base.GridCell In cellObj
                            If Not stringToCopy.Contains(cell.Column.FieldName) Then
                                If Not colList.Exists(Function(x) x.ToLower = cell.Column.FieldName.ToLower) Then
                                    stringToCopy = stringToCopy & cell.Column.FieldName & vbTab
                                End If
                            End If
                        Next
                        If stringToCopy = "" Then Exit Sub
                        stringToCopy = stringToCopy.TrimEnd(vbTab)
                        stringToCopy = stringToCopy + vbCr
                    End If

                    Dim rowIndex As Integer = 0
                    If cellObj.Length > 0 Then
                        rowIndex = cellObj(0).RowHandle
                    End If
                    Dim k As Integer = 0
                    For k = 0 To cellObj.Length - 1
                        If Not colList.Exists(Function(x) x.ToLower = cellObj(k).Column.FieldName.ToLower) Then
                            If rowIndex <> cellObj(k).RowHandle Then
                                stringToCopy = stringToCopy & vbCr
                            End If
                            stringToCopy = stringToCopy & gView.GetRowCellValue(cellObj(k).RowHandle, cellObj(k).Column) & vbTab
                            rowIndex = cellObj(k).RowHandle
                        End If
                    Next
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy & vbCr
                End If
            End If
            Clipboard.SetText(stringToCopy)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub CopyGridDataToClipBoard(ByRef gControl As GridControl, ByVal gView As Views.Grid.GridView, Optional IsCopyAll As Boolean = True, Optional ByVal IsIncludeHeader As Boolean = True,
                                              Optional ColumnListToExclude() As String = Nothing)
        Dim stringToCopy As String = ""
        Try
            Dim colList As New List(Of String)
            If ColumnListToExclude IsNot Nothing Then
                colList = ColumnListToExclude.ToList
            End If
            If IsCopyAll Then
                Dim dtData As DataTable = TryCast(gControl.DataSource, DataTable)
                If dtData Is Nothing Then Exit Sub
                If IsIncludeHeader = True Then
                    For Each Col As DataColumn In dtData.Columns
                        If Not colList.Exists(Function(x) x.ToLower = Col.Caption.ToLower) Then
                            stringToCopy = stringToCopy + Col.Caption + vbTab
                        End If
                    Next
                    If stringToCopy = "" Then Exit Sub
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy + vbCr
                End If

                For Each row As DataRow In dtData.Rows
                    For Each Col As DataColumn In dtData.Columns
                        If Not colList.Exists(Function(x) x.ToLower = Col.Caption.ToLower) Then
                            stringToCopy = stringToCopy + row.Item(Col).ToString + vbTab
                        End If
                    Next
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy & vbCr
                Next
            Else
                If gView.IsCellSelect = False Then
                    If IsIncludeHeader Then
                        For Each Col As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                            If Not colList.Exists(Function(x) x.ToLower = Col.FieldName.ToLower) Then
                                stringToCopy = stringToCopy + Col.FieldName + vbTab
                            End If
                        Next
                        If stringToCopy = "" Then Exit Sub
                        stringToCopy = stringToCopy.TrimEnd(vbTab)
                        stringToCopy = stringToCopy + vbCr
                    End If

                    Dim rowIndex() As Integer = gView.GetSelectedRows()
                    For i As Integer = 0 To rowIndex.Length - 1
                        For Each Col As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                            If Not colList.Exists(Function(x) x.ToLower = Col.FieldName.ToLower) Then
                                stringToCopy = stringToCopy & gView.GetRowCellValue(rowIndex(i), Col) & vbTab
                            End If
                        Next
                    Next
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy + vbCr
                Else
                    Dim cellObj() As DevExpress.XtraGrid.Views.Base.GridCell = gView.GetSelectedCells()

                    If IsIncludeHeader Then
                        For Each cell As DevExpress.XtraGrid.Views.Base.GridCell In cellObj
                            If Not stringToCopy.Contains(cell.Column.FieldName) Then
                                If Not colList.Exists(Function(x) x.ToLower = cell.Column.FieldName.ToLower) Then
                                    stringToCopy = stringToCopy & cell.Column.FieldName & vbTab
                                End If
                            End If
                        Next
                        If stringToCopy = "" Then Exit Sub
                        stringToCopy = stringToCopy.TrimEnd(vbTab)
                        stringToCopy = stringToCopy + vbCr
                    End If

                    Dim rowIndex As Integer = 0
                    If cellObj.Length > 0 Then
                        rowIndex = cellObj(0).RowHandle
                    End If
                    Dim k As Integer = 0
                    For k = 0 To cellObj.Length - 1
                        If Not colList.Exists(Function(x) x.ToLower = cellObj(k).Column.FieldName.ToLower) Then
                            If rowIndex <> cellObj(k).RowHandle Then
                                stringToCopy = stringToCopy & vbCr
                            End If
                            stringToCopy = stringToCopy & gView.GetRowCellValue(cellObj(k).RowHandle, cellObj(k).Column) & vbTab
                            rowIndex = cellObj(k).RowHandle
                        End If
                    Next
                    stringToCopy = stringToCopy.TrimEnd(vbTab)
                    stringToCopy = stringToCopy & vbCr
                End If
            End If
            Clipboard.SetText(stringToCopy)
        Catch ex As Exception
        End Try
    End Sub

End Class
