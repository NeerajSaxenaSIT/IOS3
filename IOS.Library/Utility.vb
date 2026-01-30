Public Class Utility
    Public Shared Function nZ(ByVal source As Object, ByVal defaultValue As String) As String
        If source Is DBNull.Value Then
            Return defaultValue
        Else
            Return source.ToString
        End If
    End Function

    'Public Function MergeDataSet2DataTable(ByRef ds As DataSet, ByRef JoinColumn As DataColumn) As DataTable
    '    If ds.Tables.Count = 1 Then
    '        Return ds.Tables(0)
    '    End If

    '    Dim dtNew As New DataTable
    '    dtNew.TableName = "ParentTable"

    '    If ds.Tables.Contains(dtNew.TableName) Then
    '        ds.Tables.Remove(dtNew.TableName)
    '    End If

    '    Dim dc As DataColumn = New DataColumn(JoinColumn.ColumnName, JoinColumn.DataType)
    '    Dim dc2(0) As DataColumn

    '    dtNew.Columns.Add(dc)
    '    dc2(0) = dc
    '    dtNew.PrimaryKey = dc2

    '    Dim lst As New List(Of DataTable)
    '    lst.Add(dtNew)
    '    For Each dts As DataTable In ds.Tables
    '        Dim result = (From row In dts.AsEnumerable Select Col1 = row.Field(Of DateTime)(dc.ColumnName)).ToList()
    '        For Each dr In result
    '            Dim dr_result() As DataRow = dtNew.Select(JoinColumn.ColumnName & "=" & Chr(39) & dr.ToString & Chr(39))
    '            If dr_result.Count = 0 Then
    '                dtNew.Rows.Add(dr)
    '            End If

    '        Next
    '        lst.Add(dts)
    '    Next
    '    For Each dts As DataTable In ds.Tables
    '        For Each dc4 As DataColumn In dts.Columns
    '            If Not dtNew.Columns.Contains(dc4.ColumnName) Then
    '                dtNew.Columns.Add(New DataColumn(dc4.ColumnName, dc4.DataType))
    '            End If
    '        Next

    '    Next


    '    dtNew = MergeData2(lst, JoinColumn.ColumnName)

    '    Return dtNew

    'End Function

    'Public Function MergeDataSet22DataTable(ByRef ds As DataSet, ByRef JoinColumn As DataColumn) As DataTable
    '    If ds.Tables.Count = 1 Then
    '        Return ds.Tables(0)
    '    End If

    '    Dim dt As New DataTable
    '    dt.TableName = "ParentTable"

    '    If ds.Tables.Contains(dt.TableName) Then
    '        ds.Tables.Remove(dt.TableName)
    '    End If

    '    Dim dc As DataColumn = New DataColumn(JoinColumn.ColumnName, JoinColumn.DataType)
    '    Dim dc2(0) As DataColumn

    '    dt.Columns.Add(dc)
    '    dc2(0) = dc
    '    dt.PrimaryKey = dc2

    '    Dim lst As New List(Of DataTable)
    '    lst.Add(dt)
    '    For Each dts As DataTable In ds.Tables
    '        Dim result = (From row In dts.AsEnumerable Select Col1 = row.Field(Of DateTime)(dc.ColumnName)).ToList()
    '        For Each dr In result
    '            Dim dr_result() As DataRow = dt.Select(JoinColumn.ColumnName & "=" & Chr(39) & dr.ToString & Chr(39))
    '            If dr_result.Count = 0 Then
    '                dt.Rows.Add(dr)
    '            End If

    '        Next
    '        lst.Add(dts)
    '    Next
    '    For Each dts As DataTable In ds.Tables
    '        For Each dc4 As DataColumn In dts.Columns
    '            If Not dt.Columns.Contains(dc4.ColumnName) Then
    '                dt.Columns.Add(New DataColumn(dc4.ColumnName, dc4.DataType))
    '            End If
    '        Next

    '    Next


    '    dt = MergeData2(lst, JoinColumn.ColumnName)

    '    Return dt

    'End Function

    Private Function MergeData2(ByVal SourceTables As List(Of DataTable), ByVal joinfield As String) As DataTable
        'Determine the number of rows the final table will have'
        Try
            Dim nMaxRowNew = 0
            Dim nMaxRowCount = 0
            Dim TargetTable As DataTable = SourceTables(0)

            For Each dt As DataTable In SourceTables
                If dt.Rows.Count > nMaxRowCount Then
                    nMaxRowCount = dt.Rows.Count
                End If
                For Each dr As DataRow In dt.Rows
                    nMaxRowNew = nMaxRowNew + 1
                Next
            Next
            'Array.Sort(a)
            ' TargetTable.Columns.Clear()
            ' TargetTable.Columns.Add(New DataColumn("Date", a(0).GetType))

            'For Each d As Date In a.Distinct
            '' TargetTable.Rows.Add(d)
            'Next
            TargetTable = SourceTables(0)

            Dim i As Integer = 1

            For i = 1 To SourceTables.Count - 1
                TargetTable = myJoinMethod(TargetTable, SourceTables(i), joinfield, joinfield)
            Next i

            If joinfield = "PERIOD_START_TIME" Then
                Dim SortedTargetTable As DataTable = TargetTable.Clone
                For Each drow As DataRow In TargetTable.Select("", "PERIOD_START_TIME ASC")
                    SortedTargetTable.ImportRow(drow)
                Next

                TargetTable.Dispose()
                TargetTable = Nothing

                Return SortedTargetTable
            Else
                Return TargetTable
            End If


        Catch ex As Exception
            ''  frm_IOS_MDI.logger.Error(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)

        End Try
        Return Nothing
    End Function

    Public Function myJoinMethod(ByVal LeftTable As DataTable, ByVal RightTable As DataTable, ByVal LeftPrimaryColumn As String, ByVal RightPrimaryColumn As String) As DataTable
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
                    If dcol.ColumnName.ToUpper = "PERIOD_START_TIME" Then
                        myDataTable.PrimaryKey = New DataColumn() {myDataTable.Columns("PERIOD_START_TIME")}
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
                        myDataTable.Columns.Add(RightTableColumn.ToString())
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
            ''frm_IOS_MDI.logger.Error(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function DataSetToArrayList(ByVal ColumnIndex As Integer, ByVal dataTable As DataTable) As ArrayList

        Dim output As ArrayList = New ArrayList()

        For Each row As DataRow In dataTable.Rows
            output.Add(row(ColumnIndex))
        Next

        Return output
    End Function


    Public Shared Function QueryData(ByVal connString As String, totalSQL As String) As DataSet
        Dim ds_result As New DataSet
        Try
            Dim ds As DataSet = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connString, totalSQL)
            If (ds Is Nothing) Then
                Return Nothing
            End If

            For Each dt As DataTable In ds.Tables
                Dim pkcols() As DataColumn = Nothing
                Dim pkcolsindex As Integer = 0

                For Each dc As DataColumn In dt.Columns
                    If dc.DataType <> GetType(Single) And dc.DataType <> GetType(Double) Then
                        ReDim Preserve pkcols(pkcolsindex)
                        pkcols(pkcolsindex) = dc
                        pkcolsindex = pkcolsindex + 1
                    End If
                Next

                dt.PrimaryKey = pkcols

                If ds_result.Tables.Count = 0 Then
                    ds_result.Tables.Add(dt.Copy)
                Else
                    ds_result.Tables(0).Merge(dt.Copy)
                End If

            Next

        Catch ex As Exception
            '' JobLog(conn_ios, "Job Failed - " & ex.Message, JobID)
        End Try


        Return ds_result

    End Function
End Class
