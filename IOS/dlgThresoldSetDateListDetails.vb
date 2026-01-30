Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class dlgThresholdSetDateListDetails

    Private dtNotSaved As DataTable = Nothing
    Public thresholdSetDateListID As Integer = Nothing

#Region "Events"

    Private Sub dlgThresoldSetDateListDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadThresholdSetDateListsDetails()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvTSDateListDetail_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvTSDateListDetail.CellValueChanged
        Try
            Dim data As DataRow = gvTSDateListDetail.GetFocusedDataRow()
            If data IsNot Nothing Then
                If IsDBNull(data.Item("ThresholdSetDateListID")) Then
                    AddNewThresholdSetDateListDetails(IIf(IsDBNull(data.Item("ObjectName")), "", data.Item("ObjectName")), IIf(IsDBNull(data.Item("ThresholdCalc_StartDate")), "", data.Item("ThresholdCalc_StartDate")), IIf(IsDBNull(data.Item("ThresholdCalc_EndDate")), "", data.Item("ThresholdCalc_EndDate")))
                Else
                    UpdateThresholdSetDateListDetails(e.Column.FieldName, e.Value)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gcTSDateListDetail_KeyUp(sender As Object, e As KeyEventArgs) Handles gcTSDateListDetail.KeyUp
        If e.KeyCode = Keys.Delete Then
            DeleteObject()
        End If
    End Sub

    Private Sub tsmiPasteFromClipboard_Click(sender As Object, e As EventArgs) Handles tsmiPasteFromClipboard.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim s As String = Clipboard.GetText()                   'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim delimeter1 As String = ","
            Dim delimeter2 As String = vbTab
            Dim dtContext As DataTable
            dtContext = CType(gcTSDateListDetail.DataSource, DataTable)

            If dtNotSaved Is Nothing Then
                dtNotSaved = New DataTable()
                dtNotSaved = dtContext.Clone()
            End If

            Dim hasError As Boolean = False

            For i As Integer = 0 To rows.Length - 1
                Dim str_comma() As String = rows(i).Split(delimeter1)
                Dim str_tab() As String = rows(i).Split(delimeter2)

                Dim str() As String = Nothing
                If str_comma.Length = 3 Then
                    str = str_comma
                Else
                    str = str_tab
                End If

                If str.Length = 3 Then
                    Dim drContext As DataRow
                    drContext = dtContext.NewRow
                    drContext("ThresholdSetDateListID") = thresholdSetDateListID
                    drContext("ObjectName") = str(0).Replace(vbLf, "")
                    drContext("ThresholdCalc_StartDate") = str(1).Replace(vbLf, "")
                    drContext("ThresholdCalc_EndDate") = str(2).Replace(vbLf, "")
                    dtContext.Rows.Add(drContext)

                    Dim drNotSaved As DataRow
                    drNotSaved = dtNotSaved.NewRow
                    drNotSaved("ThresholdSetDateListID") = thresholdSetDateListID
                    drNotSaved("ObjectName") = str(0).Replace(vbLf, "")
                    drNotSaved("ThresholdCalc_StartDate") = str(1).Replace(vbLf, "")
                    drNotSaved("ThresholdCalc_EndDate") = str(2).Replace(vbLf, "")
                    dtNotSaved.Rows.Add(drNotSaved)
                Else
                    If Not str(0) = vbLf And Not i = rows.Length - 1 Then
                        hasError = True
                    End If

                End If
            Next

            If hasError = True Then
                XtraMessageBox.Show("Columns mismatch, column must be: " & vbNewLine & "<ObjectName>,<ThresholdCalc_StartDate>,<ThresholdCalc_EndDate>")
                Exit Sub
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiDeleteSelectedRow_Click(sender As Object, e As EventArgs) Handles tsmiDeleteSelectedRow.Click
        DeleteObject()
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                InsertBulkDataToServer(connArr(1), ".[dbo].[IOS_CPE_ThresholdSet_DateLists_Details]", dtNotSaved)
            End If

            SetMessage("Data Committed Successfully")
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

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadThresholdSetDateListsDetails()
        Dim parray()() As String = {
            New String() {"@ThresholdSetDateListID", thresholdSetDateListID}
        }
        Dim strConnection As String = GetSQL(7030, parray)(0)
        Dim sqlParam As String = GetSQL(7030, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim cols2Hide() As String = {"ThresholdSetDateListDetailsID", "ThresholdSetDateListID"}
        IOSDevExpressGrid.PopulateDataInGrid(gcTSDateListDetail, gvTSDateListDetail, dt, "ALL", cols2Hide, "ObjectName")
    End Sub

    Private Sub AddNewThresholdSetDateListDetails(objName As String, startDate As String, endDate As String)
        Dim parray()() As String = {
            New String() {"@ThresholdSetDateListID", thresholdSetDateListID},
            New String() {"@ObjectName", IIf(objName = "", "NULL", Chr(39) & objName & Chr(39))},
            New String() {"@ThresholdCalc_StartDate", IIf(startDate = "", "NULL", Chr(39) & startDate & Chr(39))},
            New String() {"@ThresholdCalc_EndDate", IIf(endDate = "", "NULL", Chr(39) & endDate & Chr(39))}
        }
        Dim strConnection As String = GetSQL(7032, parray)(0)
        Dim sqlParam As String = GetSQL(7032, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub UpdateThresholdSetDateListDetails(fieldName As String, fieldValue As String)
        Dim parray()() As String = {
            New String() {"@ThresholdSetDateListDetailsID", CInt(gvTSDateListDetail.GetFocusedRowCellValue("ThresholdSetDateListDetailsID"))},
            New String() {"@ColName", Chr(39) & fieldName & Chr(39)},
            New String() {"@ColValue", Chr(39) & fieldValue & Chr(39)}
        }
        Dim strConnection As String = GetSQL(7029, parray)(0)
        Dim sqlParam As String = GetSQL(7029, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub DeleteObject()
        Try
            If XtraMessageBox.Show("Are you sure to delete selected object(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim strConnection As String, sqlParam As String
                Dim parray()() As String = Nothing

                Dim rowIndex() As Integer = gvTSDateListDetail.GetSelectedRows()
                For i As Integer = 0 To rowIndex.Length - 1
                    Dim drContext As DataRowView = gvTSDateListDetail.GetRow(rowIndex(i))
                    If drContext IsNot Nothing Then
                        parray = {
                            New String() {"@ThresholdSetDateListDetailsID", drContext.Item(0)}
                        }
                        strConnection = GetSQL(7031, parray)(0)
                        sqlParam = GetSQL(7031, parray)(1)
                        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                        parray = Nothing
                    End If
                Next
                LoadThresholdSetDateListsDetails()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
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
                copy.NotifyAfter = 1000
                AddHandler copy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                copy.ColumnMappings.Add("ThresholdSetDateListID", "ThresholdSetDateListID")
                copy.ColumnMappings.Add("ObjectName", "ObjectName")
                copy.ColumnMappings.Add("ThresholdCalc_StartDate", "ThresholdCalc_StartDate")
                copy.ColumnMappings.Add("ThresholdCalc_EndDate", "ThresholdCalc_EndDate")

                copy.WriteToServer(dtData, DataRowState.Added)

            End Using
        End Using
    End Sub

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlClient.SqlRowsCopiedEventArgs)
        lblMessage.Text = "Copied " & args.RowsCopied & " so far..."
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

#End Region

End Class