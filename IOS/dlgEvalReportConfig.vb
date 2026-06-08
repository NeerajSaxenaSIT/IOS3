Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgEvalReportConfig

    Private Sub dlgEvalReportConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadFieldCombo()
            LoadReportFilterForUser(GetReportFilterForUser())
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If cmbCategory.SelectedIndex = 0 Then
                SetMessage("Please Select Filter Category")
                Exit Sub
            ElseIf cmbField.SelectedIndex = 0 Then
                SetMessage("Please Select Filter Field")
                Exit Sub
            ElseIf txtValue.Text.Trim = String.Empty Then
                SetMessage("Please Enter Filter Value")
                Exit Sub
            Else
                Dim sqlParam As String = Nothing
                Dim connstring As String = Nothing
                Dim parray()() As String = {
                    New String() {"@UserName", Chr(39) & Environment.UserName.ToString.Trim & Chr(39)},
                    New String() {"@FilterCategory", Chr(39) & cmbCategory.SelectedItem.ToString.Trim & Chr(39)},
                    New String() {"@FilterField", Chr(39) & cmbField.SelectedItem.ToString.Trim & Chr(39)},
                    New String() {"@FilterValue", Chr(39) & Replace(txtValue.Text.Trim, "'", "''") & Chr(39)}
                }
                sqlParam = GetSQL(8830, parray)(1)
                connstring = GetSQL(8830, parray)(0)
                DataAccessorODBC.ExecuteNonQuery(connstring, sqlParam, iQryTimeOut)
                LoadReportFilterForUser(GetReportFilterForUser())
                ClearSelection()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub ClearSelection()
        cmbCategory.SelectedIndex = 0
        cmbField.SelectedIndex = 0
        txtValue.Text = String.Empty
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If gvReportFilter.RowCount > 0 Then
                Dim sqlParam As String = Nothing
                Dim connstring As String = Nothing
                Dim parray()() As String = {
                    New String() {"@ReportConfigID", CInt(gvReportFilter.GetFocusedRowCellValue("ReportConfigID"))}
                }
                sqlParam = GetSQL(8831, parray)(1)
                connstring = GetSQL(8831, parray)(0)
                DataAccessorODBC.ExecuteNonQuery(connstring, sqlParam, iQryTimeOut)
                LoadReportFilterForUser(GetReportFilterForUser())
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        Try
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub LoadFieldCombo()
        Dim sqlParam As String = Nothing
        Dim connstring As String = Nothing
        Dim parray()() As String = Nothing
        sqlParam = GetSQL(8829, parray)(1)
        connstring = GetSQL(8829, parray)(0)
        Dim dt = DataAccessorODBC.GetDataTable(connstring, sqlParam, 300)
        BindDevExComboBoxWithValueMember(cmbField, dt, "Column_Name", "Column_Name", "Select")
    End Sub

    Public Function GetReportFilterForUser() As DataTable
        Dim sqlParam As String = Nothing
        Dim connstring As String = Nothing
        Dim parray()() As String = {
            New String() {"@UserName", Chr(39) & Environment.UserName.ToString.Trim & Chr(39)}
        }
        sqlParam = GetSQL(8832, parray)(1)
        connstring = GetSQL(8832, parray)(0)
        Return DataAccessorODBC.GetDataTable(connstring, sqlParam, 300)
    End Function

    Private Sub LoadReportFilterForUser(ByRef dt As DataTable)
        IOSDevExpressGrid.PopulateDataInGrid(gcReportFilter, gvReportFilter, dt, "ALL", {"ReportConfigID"}, "FilterValue")
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

End Class