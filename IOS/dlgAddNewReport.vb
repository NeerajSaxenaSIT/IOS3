Imports IOS.DataLibrary

Public Class dlgAddNewReport

    Public reportGroupId As Integer = Nothing

#Region "Form Events"

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

#Region "Private Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtReportName.Text.Trim = String.Empty Then
                SetMessage("Enter Report Name")
                Exit Sub
            ElseIf cmbReportType.SelectedIndex = 0 Then
                SetMessage("Select Report Type")
                Exit Sub
            End If
            NewReportName = txtReportName.Text.Trim
            clsSQLCommands.InsertReport(connStrIOSServer, NewReportName, System.Environment.UserName.ToString, reportGroupId, cmbReportType.SelectedItem.ToString)
            Me.DialogResult = DialogResult.OK
            Me.Hide()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class