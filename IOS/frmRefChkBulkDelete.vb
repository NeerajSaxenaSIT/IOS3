Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraEditors

Public Class frmRefChkBulkDelete

#Region "Variables"

    Private dtSearchFiltered As DataTable = Nothing

    Public itemType As String = Nothing
    Public strSearchGridFilter As String = Nothing

#End Region

#Region "Events"

    Private Sub frmRefChkBulkDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtTemp As DataTable = Nothing
            If itemType.ToLower = "filterstring" Then
                lblDeleteInfo.Text = lblDeleteInfo.Text & " Filter String" & vbCrLf & vbCrLf & strSearchGridFilter.Replace(" And ", vbCrLf)
                If dtFilterStrings IsNot Nothing AndAlso dtFilterStrings.Rows.Count > 0 Then
                    dtSearchFiltered = dtFilterStrings.Select(strSearchGridFilter).CopyToDataTable
                    dtTemp = dtSearchFiltered.DistinctCol({"FilterString"})
                    BindDevExComboBoxWithValueMember(cmbItem, dtTemp, "FilterString", "FilterString", "Select")
                End If
            ElseIf itemType.ToLower = "incexcobject" Then
                lblDeleteInfo.Text = lblDeleteInfo.Text & " Inclusion/Exclusion Objects" & vbCrLf & vbCrLf & strSearchGridFilter.Replace(" And ", vbCrLf)
                If dtIncExcObjects IsNot Nothing AndAlso dtIncExcObjects.Rows.Count > 0 Then
                    dtSearchFiltered = dtIncExcObjects.Select(strSearchGridFilter).CopyToDataTable
                    dtTemp = dtSearchFiltered.DistinctCol({"ListID", "ListName"})
                    BindDevExComboBoxWithValueMember(cmbItem, dtTemp, "ListID", "ListName", "Select")
                End If
            ElseIf itemType.ToLower = "exclusionparam" Then
                lblDeleteInfo.Text = lblDeleteInfo.Text & " Excluded Parameters" & vbCrLf & vbCrLf & strSearchGridFilter.Replace(" And ", vbCrLf)
                If dtExcludedParams IsNot Nothing AndAlso dtExcludedParams.Rows.Count > 0 Then
                    dtSearchFiltered = dtExcludedParams.Select(strSearchGridFilter).CopyToDataTable
                    dtTemp = dtSearchFiltered.DistinctCol({"ParameterName"})
                    BindDevExComboBoxWithValueMember(cmbItem, dtTemp, "ParameterName", "ParameterName", "Select")
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnbulkDelete_Click(sender As Object, e As EventArgs) Handles btnBulkDelete.Click
        Try
            If cmbItem.SelectedIndex = 0 Then
                SetMessage("Please Select Item To Delete")
                cmbItem.Focus()
                Exit Sub
            End If

            If XtraMessageBox.Show("Are you sure to delete selected item?", "Delete Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                BulkDeleteItem()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
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

#End Region

#Region "Methods"

    Private Sub BulkDeleteItem()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim Rows2Delete As DataRow() = Nothing

        If itemType.ToLower = "filterstring" Then

            Dim dtTemp As DataTable = dtSearchFiltered.DistinctCol("TemplateID")
            If dtTemp.Rows.Count > 0 Then
                For Each drTemp As DataRow In dtTemp.Rows
                    Dim parray()() As String = {
                        New String() {"@templateID", CInt(drTemp("TemplateID"))},
                        New String() {"@filterString", Chr(39) & cmbItem.SelectedItem.ToString.Replace("'", "''") & Chr(39)}
                    }
                    strConnection = GetSQL(4193, parray)(0)
                    sqlParam = GetSQL(4193, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    cmbItem.Properties.Items.Remove(cmbItem.SelectedItem)
                Next
                cmbItem.SelectedIndex = 0
            End If

        ElseIf itemType.ToLower = "incexcobject" Then

            Dim dtTemp As DataTable = dtSearchFiltered.DistinctCol("TemplateID")
            If dtTemp.Rows.Count > 0 Then
                For Each drTemp As DataRow In dtTemp.Rows
                    Dim parray()() As String = {
                        New String() {"@TemplateID", CInt(drTemp("TemplateID"))},
                        New String() {"@ListID", CInt(CType(cmbItem.SelectedItem, clsComboBoxItem).Value)}
                    }
                    strConnection = GetSQL(4211, parray)(0)
                    sqlParam = GetSQL(4211, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    cmbItem.Properties.Items.Remove(cmbItem.SelectedItem)
                Next
                cmbItem.SelectedIndex = 0
            End If

        ElseIf itemType.ToLower = "exclusionparam" Then

            Dim dtTemp As DataTable = dtSearchFiltered.DistinctCol("TemplateID")

            If dtTemp.Rows.Count > 0 Then
                For Each drTemp As DataRow In dtTemp.Rows
                    Dim parray()() As String = {
                        New String() {"@templateID", CInt(drTemp("TemplateID"))},
                        New String() {"@parameterName", Chr(39) & cmbItem.SelectedItem.ToString & Chr(39)}
                    }
                    strConnection = GetSQL(4168, parray)(0)
                    sqlParam = GetSQL(4168, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    cmbItem.Properties.Items.Remove(cmbItem.SelectedItem)
                Next
                cmbItem.SelectedIndex = 0
            End If

        End If
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