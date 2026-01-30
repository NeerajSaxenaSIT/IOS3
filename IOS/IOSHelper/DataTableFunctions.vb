Module DataTableFunctions

    Public Function DataSetToArrayList(ByVal ColumnIndex As Integer, ByVal dataTable As DataTable) As ArrayList
        Dim output As ArrayList = New ArrayList()
        For Each row As DataRow In dataTable.Rows
            output.Add(row(ColumnIndex))
        Next
        Return output
    End Function

    Public Function PivotTable(oldTable As DataTable, Optional pivotColumnOrdinal As Integer = 0) As DataTable
        Dim newTable As New DataTable
        Dim dr As DataRow

        ' add pivot column name
        newTable.Columns.Add(oldTable.Columns(pivotColumnOrdinal).ColumnName)

        ' add pivot column values in each row as column headers to new Table
        For Each row In oldTable.Rows
            newTable.Columns.Add(row(pivotColumnOrdinal))
        Next

        ' loop through columns
        For col = 0 To oldTable.Columns.Count - 1
            'pivot column doen't get it's own row (it is already a header)
            If col = pivotColumnOrdinal Then Continue For

            ' each column becomes a new row
            dr = newTable.NewRow()

            ' add the Column Name in the first Column
            dr(0) = oldTable.Columns(col).ColumnName

            ' add data from every row to the pivoted row
            For row = 0 To oldTable.Rows.Count - 1
                dr(row + 1) = oldTable.Rows(row)(col)
            Next

            'add the DataRow to the new table
            newTable.Rows.Add(dr)
        Next

        Return newTable
    End Function

    Public Function MyJoinMethod_Old(ByVal LeftTable As DataTable, ByVal RightTable As DataTable, ByVal LeftPrimaryColumn As String, ByVal RightPrimaryColumn As String) As DataTable
        'first create the datatable columns 
        Try
            Dim mydataSet As DataSet = New DataSet()
            mydataSet.Tables.Add("  ")
            Dim myDataTable As DataTable = mydataSet.Tables(0)

            'add left table columns 

            Dim dcLeftTableColumns(LeftTable.Columns.Count - 1) As DataColumn
            LeftTable.Columns.CopyTo(dcLeftTableColumns, 0)
            '  Console.WriteLine("LeftTable 1:  " & LeftTable.Columns(0).DataType.ToString)
            For Each LeftTableColumn As DataColumn In dcLeftTableColumns
                If Not myDataTable.Columns.Contains(LeftTableColumn.ColumnName.ToString()) Then
                    Dim dcol As DataColumn = New DataColumn(LeftTableColumn.ColumnName.ToString, LeftTableColumn.DataType)
                    myDataTable.Columns.Add(dcol)
                    If dcol.ColumnName.ToUpper = "DATE" Then
                        myDataTable.PrimaryKey = New DataColumn() {myDataTable.Columns("Date")}
                    End If
                End If
            Next
            '  Console.WriteLine("myTable:  " & myDataTable.Columns(0).DataType.ToString)

            'now add right table columns 
            Dim dcRightTableColumns(RightTable.Columns.Count - 1) As DataColumn
            RightTable.Columns.CopyTo(dcRightTableColumns, 0)

            For Each RightTableColumn As DataColumn In dcRightTableColumns
                If Not myDataTable.Columns.Contains(RightTableColumn.ToString()) Then
                    If (RightTableColumn.ToString() <> RightPrimaryColumn) Then
                        myDataTable.Columns.Add(RightTableColumn.ToString, RightTableColumn.DataType)
                    End If
                End If
            Next

            'add left-table data to mytable 
            ' Console.WriteLine("LeftTable:  " & LeftTable.Columns(0).DataType.ToString)
            For Each LeftTableDataRows As DataRow In LeftTable.Rows
                myDataTable.ImportRow(LeftTableDataRows)
            Next

            Dim var As ArrayList = New ArrayList() 'this variable holds the id's which have joined 
            ' Console.WriteLine(myDataTable.Columns(0).DataType.ToString)

            ' Dim myTableIDs As ArrayList = New ArrayList()
            ' myTableIDs = DataSetToArrayList(0, myDataTable)
            Dim LeftTableIDs As ArrayList = New ArrayList()
            LeftTableIDs = DataSetToArrayList(0, LeftTable)
            'Dim RightTableIDs As ArrayList = New ArrayList()
            ' RightTableIDs = DataSetToArrayList(0, RightTable)

            'import righttable which having not equal Id's with lefttable 

            For Each rightTableDataRows As DataRow In RightTable.Rows
                If (LeftTableIDs.Contains(rightTableDataRows(0))) Then

                    Dim wherecondition As String = "[" + myDataTable.Columns(0).ColumnName + "]='#" + rightTableDataRows(0).ToString() + "#'"
                    'Dim dr() As DataRow = myDataTable.Select(wherecondition)
                    Dim dr As DataRow = myDataTable.Rows.Find(rightTableDataRows(0))
                    Dim iIndex As Integer = myDataTable.Rows.IndexOf(dr)

                    For Each dc As DataColumn In RightTable.Columns
                        If dc.Ordinal <> 0 Then
                            myDataTable.Rows(iIndex)(dc.ColumnName.ToString().Trim()) = rightTableDataRows(dc.ColumnName.ToString().Trim())
                        End If
                    Next
                Else

                    Dim count As Integer = myDataTable.Rows.Count
                    Dim row As DataRow = myDataTable.NewRow()
                    row(0) = rightTableDataRows(0).ToString()
                    myDataTable.Rows.Add(row)

                    For Each dc As DataColumn In RightTable.Columns
                        If dc.Ordinal <> 0 Then
                            myDataTable.Rows(count)(dc.ColumnName.ToString().Trim()) = rightTableDataRows(dc.ColumnName.ToString().Trim()).ToString()
                        End If
                    Next
                End If
            Next

            Return myDataTable
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function MyJoinMethod(ByVal LeftTable As DataTable, ByVal RightTable As DataTable, ByVal LeftPrimaryColumn As String, ByVal RightPrimaryColumn As String) As DataTable
        Try
            ' Validate input
            If LeftTable Is Nothing OrElse RightTable Is Nothing Then Return Nothing
            If String.IsNullOrEmpty(LeftPrimaryColumn) OrElse String.IsNullOrEmpty(RightPrimaryColumn) Then
                Return Nothing
            End If

            ' Clone the left table structure
            Dim resultTable As DataTable = LeftTable.Clone()

            ' Add missing columns from right table
            For Each col As DataColumn In RightTable.Columns
                If Not resultTable.Columns.Contains(col.ColumnName) AndAlso
               Not col.ColumnName.Equals(RightPrimaryColumn, StringComparison.OrdinalIgnoreCase) Then
                    resultTable.Columns.Add(col.ColumnName, col.DataType)
                End If
            Next

            ' Create lookup dictionary for RightTable (O(1) joins)
            Dim rightLookup As New Dictionary(Of Object, DataRow)
            For Each row As DataRow In RightTable.Rows
                Dim key = row(RightPrimaryColumn)
                If key IsNot Nothing AndAlso Not rightLookup.ContainsKey(key) Then
                    rightLookup.Add(key, row)
                End If
            Next

            ' Merge matching rows
            For Each leftRow As DataRow In LeftTable.Rows
                Dim key = leftRow(LeftPrimaryColumn)
                Dim newRow As DataRow = resultTable.NewRow()

                ' Copy left table data
                For Each col As DataColumn In LeftTable.Columns
                    newRow(col.ColumnName) = leftRow(col)
                Next

                ' Merge right table data if match found
                If key IsNot Nothing AndAlso rightLookup.ContainsKey(key) Then
                    Dim rightRow As DataRow = rightLookup(key)
                    For Each col As DataColumn In RightTable.Columns
                        If Not col.ColumnName.Equals(RightPrimaryColumn, StringComparison.OrdinalIgnoreCase) Then
                            If resultTable.Columns.Contains(col.ColumnName) Then
                                newRow(col.ColumnName) = rightRow(col)
                            End If
                        End If
                    Next
                End If

                resultTable.Rows.Add(newRow)
            Next

            ' Add non-matching right table rows (right outer join)
            For Each rightRow As DataRow In RightTable.Rows
                Dim key = rightRow(RightPrimaryColumn)
                Dim found As Boolean = LeftTable.AsEnumerable().Any(Function(r) Equals(r(LeftPrimaryColumn), key))
                If Not found Then
                    Dim newRow As DataRow = resultTable.NewRow()
                    newRow(LeftPrimaryColumn) = key
                    For Each col As DataColumn In RightTable.Columns
                        If resultTable.Columns.Contains(col.ColumnName) Then
                            newRow(col.ColumnName) = rightRow(col)
                        End If
                    Next
                    resultTable.Rows.Add(newRow)
                End If
            Next

            Return resultTable

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Return Nothing
        End Try
    End Function

End Module
