Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class IOSDevExpressGrid

    Public Shared Sub ClearGrid(ByRef grd As GridControl)
        grd.SuspendLayout()
        grd.DataSource = Nothing
        CType(grd.MainView, DevExpress.XtraGrid.Views.Grid.GridView).Columns.Clear()
        grd.Refresh()
        grd.ResumeLayout()
    End Sub

    Public Shared Sub PopulateDataInGrid(ByRef gridCtrl As GridControl, ByRef grdView As GridView, ByVal dt As DataTable, ByVal fit As String, Optional ColumnHiddenList() As String = Nothing, Optional ColumnToAutoFill As String = Nothing, Optional dateFormat As String = Nothing,
                                         Optional ByRef dsColmnConfig As DataSet = Nothing)
        Try
            Dim gvActiveFilterString As String = Nothing
            With gridCtrl
                .SuspendLayout()
                grdView.OptionsView.ColumnAutoWidth = False
                grdView.OptionsBehavior.AutoPopulateColumns = True
                gridCtrl.DataSource = Nothing
                grdView.Columns.Clear()
                gridCtrl.DataSource = dt
                grdView.BestFitMaxRowCount = 10

                If dt IsNot Nothing Then
                    For Each dtCol As DataColumn In dt.Columns
                        If grdView.Columns(dtCol.ColumnName) IsNot Nothing Then
                            If grdView.Columns(dtCol.ColumnName).Visible = True Then

                                If ColumnHiddenList IsNot Nothing Then
                                    If ColumnHiddenList.Contains(dtCol.ColumnName) Then
                                        grdView.Columns(dtCol.ColumnName).Visible = False
                                    End If
                                End If

                                If dtCol.DataType = GetType(DateTime) Then
                                    grdView.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                    If dateFormat IsNot Nothing Then
                                        grdView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = dateFormat
                                    Else
                                        grdView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
                                    End If
                                End If

                                grdView.Columns(dtCol.ColumnName).BestFit()
                                grdView.Columns(dtCol.ColumnName).FieldName = dtCol.ColumnName
                                grdView.Columns(dtCol.ColumnName).Caption = dtCol.ColumnName
                            End If

                            If dsColmnConfig IsNot Nothing Then
                                If dsColmnConfig.Tables.Contains(grdView.Name) Then
                                    Dim dtColmnConfig As DataTable = dsColmnConfig.Tables(grdView.Name)
                                    grdView.Columns(dtCol.ColumnName).VisibleIndex = CInt(dtColmnConfig.Select("ColumnName='" & dtCol.ColumnName & "'")(0)("ColumnVisibleIndex"))
                                    grdView.Columns(dtCol.ColumnName).Width = CInt(dtColmnConfig.Select("ColumnName='" & dtCol.ColumnName & "'")(0)("ColumnWidth"))
                                    If dtColmnConfig.Columns.Contains("ColumnFilter") Then
                                        If CStr(dtColmnConfig.Select("ColumnName='" & dtCol.ColumnName & "'")(0)("ColumnFilter")) <> "" Then
                                            gvActiveFilterString &= CStr(dtColmnConfig.Select("ColumnName='" & dtCol.ColumnName & "'")(0)("ColumnFilter")) & " AND "
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    Next
                End If

                If ColumnToAutoFill IsNot Nothing Then
                    If grdView.Columns(ColumnToAutoFill) IsNot Nothing Then
                        grdView.AutoFillColumn = grdView.Columns(ColumnToAutoFill)
                    End If
                End If

                If dsColmnConfig IsNot Nothing Then
                    If dsColmnConfig.Tables.Contains(grdView.Name) Then
                        Dim dtColmnConfig As DataTable = dsColmnConfig.Tables(grdView.Name)
                        If dtColmnConfig.Columns.Contains("ColumnFilter") Then
                            If gvActiveFilterString IsNot Nothing Then
                                gvActiveFilterString = gvActiveFilterString.Substring(0, gvActiveFilterString.Length - 4)
                                grdView.ActiveFilterString = gvActiveFilterString
                            End If
                        End If
                    End If
                End If

                .ResumeLayout()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub CopyGridDataToClipBoard(ByRef gControl As GridControl, ByRef gView As GridView, Optional IsCopyAll As Boolean = True, Optional ByVal IsIncludeHeader As Boolean = True, Optional ColumnListToExclude() As String = Nothing)
        If IsIncludeHeader = True Then
            gView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.True
        Else
            gView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False
        End If
        Clipboard.Clear()
        If ColumnListToExclude IsNot Nothing Then
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
                                stringToCopy = stringToCopy & Col.Caption & vbTab
                            End If
                        Next
                        If stringToCopy = "" Then Exit Sub
                        stringToCopy = stringToCopy.TrimEnd(vbTab)
                        stringToCopy = stringToCopy & vbCr
                    End If

                    For Each row As DataRow In dtData.Rows
                        For Each Col As DataColumn In dtData.Columns
                            If Not colList.Exists(Function(x) x.ToLower = Col.Caption.ToLower) Then
                                If Col.Caption.ToLower = "date" Then
                                    stringToCopy = stringToCopy & row.Item(Col) & vbTab
                                Else
                                    stringToCopy = stringToCopy & row.Item(Col).ToString & vbTab
                                End If
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
                                    stringToCopy = stringToCopy & Col.FieldName & vbTab
                                End If
                            Next
                            If stringToCopy = "" Then Exit Sub
                            stringToCopy = stringToCopy.TrimEnd(vbTab)
                            stringToCopy = stringToCopy & vbCr
                        End If

                        Dim rowIndex() As Integer = gView.GetSelectedRows()
                        gView.CopyToClipboard()
                        For i As Integer = 0 To rowIndex.Length - 1
                            For Each Col As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                                If Not colList.Exists(Function(x) x.ToLower = Col.FieldName.ToLower) Then
                                    stringToCopy = stringToCopy & IIf(IsDBNull(gView.GetRowCellValue(rowIndex(i), Col.FieldName)), "", gView.GetRowCellValue(rowIndex(i), Col.FieldName)) & vbTab
                                End If
                            Next
                            stringToCopy = stringToCopy & vbCr
                        Next
                        stringToCopy = stringToCopy.TrimEnd(vbTab)
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
                            stringToCopy = stringToCopy & vbCr
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
                                stringToCopy = stringToCopy & gView.GetRowCellValue(cellObj(k).RowHandle, cellObj(k).Column.FieldName) & vbTab
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
        Else
            If IsCopyAll = True Then
                gView.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.True
                gView.OptionsSelection.MultiSelect = True
                gView.SelectAll()
            End If
            gView.CopyToClipboard()
        End If
    End Sub

    Public Shared Function CopyAllDataFromGridToArray(ByVal grvTemp As GridControl, ByVal gView As GridView) As Object
        Dim DataArray(gView.RowCount, gView.Columns.Count - 1) As Object
        Try
            If (grvTemp IsNot Nothing) Then

                'Header text
                For Each col As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                    DataArray(0, col.AbsoluteIndex) = col.FieldName
                Next

                'cell values
                For i As Integer = 0 To gView.RowCount - 1
                    Dim data As Object = gView.GetRow(i)
                    If data IsNot Nothing Then
                        For Each col As DevExpress.XtraGrid.Columns.GridColumn In gView.Columns
                            DataArray(i + 1, col.AbsoluteIndex) = data.Item(col.AbsoluteIndex).ToString()
                        Next
                    End If
                Next
                Return DataArray
            Else
                Throw New Exception("Did not find associated Grid view")
            End If
        Catch ex As Exception
            Dim errorMsg As String = System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace
        End Try
        Return Nothing
    End Function

    Public Shared Function CreateGrid(ByVal gridName As String, Optional ByRef dtSourse As DataTable = Nothing) As GridControl
        Dim gControl As New GridControl()
        Dim gView As New GridView(gControl)
        gControl.MainView = gView
        gView.GridControl = gControl
        gView.OptionsView.ShowGroupPanel = False
        gView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False
        gView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False
        gView.OptionsBehavior.Editable = False
        gView.OptionsView.AnimationType = Views.Base.GridAnimationType.Default
        gView.OptionsView.AllowCellMerge = False
        gView.OptionsClipboard.AllowCopy = True
        gControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
        gView.OptionsCustomization.AllowColumnMoving = True
        gView.OptionsCustomization.AllowColumnResizing = True
        gControl.Dock = System.Windows.Forms.DockStyle.Fill
        gControl.Location = New System.Drawing.Point(633, 23)
        gView.OptionsSelection.MultiSelect = True
        gControl.Name = gridName
        gView.OptionsCustomization.AllowRowSizing = True
        gView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
        gControl.Size = New System.Drawing.Size(294, 314)
        gControl.TabIndex = 27
        gControl.LookAndFeel.UseDefaultLookAndFeel = True

        If dtSourse IsNot Nothing Then
            PopulateDataInGrid(gControl, gView, dtSourse, "All")
        End If

        Return gControl
    End Function

    Public Shared Sub ExportDataGridToCSV(ByRef gridCtrl As DevExpress.XtraGrid.GridControl)
        Dim savefiledialog1 As New SaveFileDialog()
        savefiledialog1.FileName = ""
        savefiledialog1.Filter = "Comma delimited |*.csv"
        If savefiledialog1.ShowDialog <> DialogResult.OK Then
            Exit Sub
        End If
        Dim fp As String = savefiledialog1.FileName
        Try
            'gridCtrl.ExportToCsv(fp)
            'MsgBox("Export Completed !", MsgBoxStyle.Information)
            Dim dt As DataTable = CType(gridCtrl.DataSource, DataTable)
            DataTable2CSV(dt, fp)
        Catch
            MsgBox("Export Failed !", MsgBoxStyle.Information)
        End Try
    End Sub

    Public Shared Sub ExportDataGridToExcel(ByRef gridCtrl As DevExpress.XtraGrid.GridControl)
        Dim savefiledialog1 As New SaveFileDialog()
        savefiledialog1.FileName = ""
        savefiledialog1.Filter = "Excel Workbook |*.xlsx"
        If savefiledialog1.ShowDialog <> DialogResult.OK Then
            Exit Sub
        End If
        Dim fp As String = savefiledialog1.FileName
        Try
            gridCtrl.ExportToXlsx(fp)
            'Dim dt As DataTable = CType(gridCtrl.DataSource, DataTable)
            'DataTable2Excel(dt, fp)
            MsgBox("Export Completed !", MsgBoxStyle.Information, "CIOS - Excel Export")
        Catch
            Try
                gridCtrl.ExportToXls(fp)
                'Dim dt As DataTable = CType(gridCtrl.DataSource, DataTable)
                'DataTable2Excel(dt, fp)
                MsgBox("Export Completed !", MsgBoxStyle.Information, "CIOS - Excel Export")
            Catch ex As Exception
                MsgBox("Export Failed !", MsgBoxStyle.Information, "CIOS - Excel Export")
            End Try
        End Try
    End Sub

    Public Shared Sub DataTable2CSV(ByRef dt As DataTable, ByVal fileName As String, Optional ByVal delim As String = ",")
        ' Standardize the delimiter
        Dim separator As String = If(delim.ToUpper() = "TAB", vbTab, delim)

        ' Use a Using block to ensure the file is closed correctly even if an error occurs
        Using writer As New System.IO.StreamWriter(fileName, False, System.Text.Encoding.UTF8)
            Try
                ' 1. Write Header Row
                Dim headerLine As New System.Text.StringBuilder()
                For i As Integer = 0 To dt.Columns.Count - 1
                    headerLine.Append(dt.Columns(i).ColumnName)
                    If i < dt.Columns.Count - 1 Then headerLine.Append(separator)
                Next
                writer.WriteLine(headerLine.ToString())

                ' 2. Write Data Rows
                For Each row As DataRow In dt.Rows
                    Dim rowLine As New System.Text.StringBuilder()

                    For i As Integer = 0 To dt.Columns.Count - 1
                        Dim cellValue As Object = row(i)
                        Dim formattedValue As String = ""

                        If cellValue IsNot DBNull.Value AndAlso cellValue IsNot Nothing Then
                            ' CHECK FOR DATETIME HERE
                            If TypeOf cellValue Is DateTime Then
                                formattedValue = DirectCast(cellValue, DateTime).ToString("yyyy-MM-dd HH:mm:ss")
                            Else
                                ' Handle commas in text (wrap in quotes) to avoid breaking CSV structure
                                formattedValue = cellValue.ToString()
                                If formattedValue.Contains(separator) Then
                                    formattedValue = String.Format("""{0}""", formattedValue.Replace("""", """"""))
                                End If
                            End If
                        End If

                        rowLine.Append(formattedValue)
                        If i < dt.Columns.Count - 1 Then rowLine.Append(separator)
                    Next
                    writer.WriteLine(rowLine.ToString())
                Next

                MsgBox("Export Completed!", MsgBoxStyle.Information)

            Catch ex As Exception
                MsgBox("Error during export: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End Using
    End Sub

    'Public Shared Sub DataTable2CLF(ByRef dt As DataTable, ByVal fileName As String)
    '    Dim writer As New System.IO.StreamWriter(fileName)
    '    Try
    '        ' first write a line with the columns name
    '        Dim sep As String = ""
    '        Dim builder As New System.Text.StringBuilder
    '        For Each col As DataColumn In dt.Columns
    '            builder.Append(sep).Append(col.ColumnName)
    '            sep = ";"
    '        Next
    '        writer.WriteLine(builder.ToString())

    '        ' then write all the rows
    '        For Each row As DataRow In dt.Rows
    '            sep = ""
    '            builder = New System.Text.StringBuilder

    '            For Each col As DataColumn In dt.Columns
    '                builder.Append(sep).Append(row(col.ColumnName))
    '                sep = ";"
    '            Next
    '            writer.WriteLine(builder.ToString())
    '        Next
    '    Finally
    '        If Not writer Is Nothing Then writer.Close()
    '        MsgBox("Export Completed !", MsgBoxStyle.Information)
    '    End Try
    'End Sub

    Public Shared Sub DataTable2Excel(ByRef dt As DataTable, ByVal fileName As String, Optional delimiter As String = Nothing)
        Dim writer As New System.IO.StreamWriter(fileName)
        Try
            ' Write Columns to excel file
            For i As Integer = 0 To dt.Columns.Count - 1
                If delimiter Is Nothing OrElse delimiter.ToUpper = "TAB" Then
                    writer.Write(dt.Columns(i).ToString().ToUpper() & vbTab)
                Else
                    writer.Write(dt.Columns(i).ToString().ToUpper() & delimiter)
                End If
            Next

            writer.WriteLine()

            'write rows to excel file
            For i As Integer = 0 To (dt.Rows.Count) - 1

                For j As Integer = 0 To dt.Columns.Count - 1

                    If delimiter Is Nothing OrElse delimiter.ToUpper = "TAB" Then
                        If dt.Rows(i)(j) IsNot Nothing Then
                            If dt.Rows(i)(j).GetType().Name.ToString.ToLower = "datetime" Then
                                writer.Write(Convert.ToString(dt.Rows(i)(j)) & vbTab)
                                'writer.Write(Convert.ToString(CType(dt.Rows(i)(j), DateTime).ToString("yyyy-MM-dd HH:mm:ss")) & vbTab)
                            Else
                                writer.Write(Convert.ToString(dt.Rows(i)(j)) & vbTab)
                            End If
                        Else
                            writer.Write(vbTab)
                        End If
                    Else
                        If dt.Rows(i)(j) IsNot Nothing Then
                            If dt.Rows(i)(j).GetType().Name.ToString.ToLower = "datetime" Then
                                writer.Write(Convert.ToString(dt.Rows(i)(j)) & delimiter)
                                'writer.Write(Convert.ToString(CType(dt.Rows(i)(j), DateTime).ToString("yyyy-MM-dd HH:mm:ss")) & delimiter)
                            Else
                                writer.Write(Convert.ToString(dt.Rows(i)(j)) & delimiter)
                            End If
                        Else
                            writer.Write(vbTab)
                        End If
                    End If

                Next

                writer.WriteLine()
            Next

        Finally
            If Not writer Is Nothing Then writer.Close()
        End Try
    End Sub

End Class
