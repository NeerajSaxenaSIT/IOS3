Imports IOS.DataLibrary
Imports System.ComponentModel
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base

Public Class dlgXmlJobInsert

#Region "Variables"

    Public xmlJobID As Integer = Nothing
    Public xmlJobName As String = Nothing
    Public xmlJobVendor As String = Nothing

    Private IsErrorInCopy As Boolean = False

#End Region

#Region "Events"

    Private Sub dlgXmlJobInsert_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblXmlJob.Text = xmlJobName
            LoadVendors()
            PopulateColumnsToGrid()
            SetComboBox(cmbVendor, ComboSelectBased.TextBased, xmlJobVendor)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnXmlJobPaste_Click(sender As Object, e As EventArgs) Handles btnXmlJobPaste.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            IsErrorInCopy = False
            If cmbVendor.SelectedIndex = 0 Then
                SetMessage("Please Select Vendor")
                Exit Sub
            End If
            gvXmlJobData.PasteFromClipboard()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvXmlJobData_ClipboardRowPasting(sender As Object, e As ClipboardRowPastingEventArgs) Handles gvXmlJobData.ClipboardRowPasting
        Try
            If IsErrorInCopy = True Then
                e.Cancel = True
                Clipboard.Clear()
                Exit Sub
            End If

            'Dim view As GridView = TryCast(sender, GridView)
            If e.OriginalValues.Count > 0 Then
                Dim dt As DataTable = Nothing
                dt = gcXmlJobData.DataSource
                If e.OriginalValues.Count = 5 Then
                    Dim drData As DataRow
                    drData = dt.NewRow()
                    drData(0) = xmlJobID
                    drData(1) = cmbVendor.SelectedItem.ToString
                    drData(2) = If(e.OriginalValues(0) IsNot Nothing, e.OriginalValues(0).ToString(), DBNull.Value)
                    drData(3) = If(e.OriginalValues(1) IsNot Nothing, e.OriginalValues(1).ToString(), "")
                    drData(4) = If(e.OriginalValues(2) IsNot Nothing, e.OriginalValues(2).ToString(), "")
                    drData(5) = If(e.OriginalValues(3) IsNot Nothing, e.OriginalValues(3).ToString(), "")
                    drData(6) = If(e.OriginalValues(4) IsNot Nothing, e.OriginalValues(4).ToString(), DBNull.Value)
                    dt.Rows.Add(drData)
                    lblRecordsCount.Text = "# Records: " & dt.Rows.Count.ToString
                ElseIf e.OriginalValues(0).ToString() <> "" Then
                    XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<MO>Tab<ObjectName>Tab<ObjectConditionColumns>Tab<ParameterName>Tab<TargetValue>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    e.Cancel = True
                    Clipboard.Clear()
                    IsErrorInCopy = True
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<MO>Tab<ObjectName>Tab<ObjectConditionColumns>Tab<ParameterName>Tab<TargetValue>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Clipboard.Clear()
            IsErrorInCopy = True
        End Try
    End Sub

    Private Sub gcManual_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles gcXmlJobData.KeyDown
        Try
            If (e.KeyCode = Keys.Delete) Then
                Dim grid As GridControl = CType(sender, GridControl)
                Dim view As GridView = DirectCast(grid.MainView, GridView)

                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                If (XtraMessageBox.Show("Do you want to delete rows?", "Delete Confirmation", MessageBoxButtons.YesNo) <> DialogResult.Yes) Then Return
                Dim rIndex() As Integer = view.GetSelectedRows()
                For i As Integer = 0 To rIndex.Count - 1
                    Application.DoEvents()
                    DeleteXmlJobRow(rIndex(i))
                Next
                LoadXmlJobSavedData()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DeleteXmlJobRow(index As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", CInt(gvXmlJobData.GetRowCellValue(index, "XMLJobID"))},
            New String() {"@Vendor", Chr(39) & gvXmlJobData.GetRowCellValue(index, "Vendor") & Chr(39)},
            New String() {"@MO", Chr(39) & gvXmlJobData.GetRowCellValue(index, "MO") & Chr(39)},
            New String() {"@ObjectName", Chr(39) & gvXmlJobData.GetRowCellValue(index, "ObjectName") & Chr(39)},
            New String() {"@ParameterName", Chr(39) & gvXmlJobData.GetRowCellValue(index, "ParameterName") & Chr(39)},
            New String() {"@TargetValue", Chr(39) & gvXmlJobData.GetRowCellValue(index, "TargetValue") & Chr(39)}
        }
        strConnection = GetSQL(6538, parray)(0)
        sqlParam = GetSQL(6538, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub gvManual_RowDeleted(ByVal sender As Object, ByVal e As DevExpress.Data.RowDeletedEventArgs) Handles gvXmlJobData.RowDeleted
        Try
            lblRecordsCount.Text = "# Records: " & gvXmlJobData.RowCount.ToString
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvXmlJobData_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvXmlJobData.ShowingEditor
        Try
            If gvXmlJobData.FocusedColumn.FieldName.ToLower <> "targetvalue" Then
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnXmlJobCommit_Click(sender As Object, e As EventArgs) Handles btnXmlJobCommit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                Dim dtAddedRecords As DataTable = CType(gcXmlJobData.DataSource, DataTable).GetChanges(DataRowState.Added)
                If dtAddedRecords IsNot Nothing Then
                    InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[XML_InputManual]", dtAddedRecords)
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing

                    Dim parray()() As String = {
                        New String() {"@XmlJobID", xmlJobID}
                    }

                    strConnection = GetSQL(6536, parray)(0)
                    sqlParam = GetSQL(6536, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    SetMessage(dtAddedRecords.Rows.Count.ToString & " Rows Inserted")
                Else
                    Dim changedRecordsTable As DataTable = CType(gcXmlJobData.DataSource, DataTable).GetChanges(DataRowState.Modified)
                    If changedRecordsTable IsNot Nothing Then
                        InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[XML_InputManual_Temp]", changedRecordsTable)
                        Dim strConnection As String = Nothing
                        Dim sqlParam As String = Nothing

                        strConnection = GetSQL(6534, Nothing)(0)
                        sqlParam = GetSQL(6534, Nothing)(1)
                        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                        SetMessage(changedRecordsTable.Rows.Count.ToString & " Rows Updated")
                    End If
                End If
            End If
            lblRecordsCount.Text = "# Records: " & gvXmlJobData.RowCount.ToString
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Public Sub InsertBulkDataToServer(ConnString As String, DestinationTable As String, dtData As DataTable)
        Try
            Using cn As New System.Data.SqlClient.SqlConnection(ConnString)
                cn.Open()
                Using copy As New System.Data.SqlClient.SqlBulkCopy(cn)

                    copy.DestinationTableName = DestinationTable
                    copy.NotifyAfter = 1000
                    AddHandler copy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                    copy.ColumnMappings.Add("XMLJobID", "XMLJobID")
                    copy.ColumnMappings.Add("Vendor", "Vendor")
                    copy.ColumnMappings.Add("MO", "MO")
                    copy.ColumnMappings.Add("ObjectName", "ObjectName")
                    copy.ColumnMappings.Add("ObjectConditionColumns", "ObjectConditionColumns")
                    copy.ColumnMappings.Add("ParameterName", "ParameterName")
                    copy.ColumnMappings.Add("TargetValue", "TargetValue")

                    copy.WriteToServer(dtData)
                End Using
            End Using
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlClient.SqlRowsCopiedEventArgs)
        lblRecordsCount.Text = "Completed - Count: " & args.RowsCopied.ToString
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadVendors()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(6535, parray)(0)
        sqlParam = GetSQL(6535, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbVendor, dt, "Vendor", "Vendor", "Select")
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
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

    Private Sub PopulateColumnsToGrid()
        Dim dt As New DataTable
        dt.Columns.Add("XMLJobID", GetType(Integer))
        dt.Columns.Add("Vendor", GetType(String))
        dt.Columns.Add("MO", GetType(String))
        dt.Columns.Add("ObjectName", GetType(String))
        dt.Columns.Add("ObjectConditionColumns", GetType(String))
        dt.Columns.Add("ParameterName", GetType(String))
        dt.Columns.Add("TargetValue", GetType(String))

        'Load previously inserted data into the grid for Xml Job ID
        LoadXmlJobSavedData()
    End Sub

    Private Sub LoadXmlJobSavedData()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6537, parray)(0)
        sqlParam = GetSQL(6537, parray)(1)
        Dim dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        gcXmlJobData.DataSource = dt
        lblRecordsCount.Text = "# Records: " & gvXmlJobData.RowCount.ToString
    End Sub

#End Region

End Class