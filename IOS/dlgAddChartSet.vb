Public Class dlgAddChartSet

    Public ChartSetName As String = Nothing
    Public AccessType As String = Nothing

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtChartSetName.Text.Trim = String.Empty Then
                SetMessage("Please Enter ChartSet Name")
                Exit Sub
            End If

            Me.ChartSetName = txtChartSetName.Text.Trim
            If rbPublic.Checked Then
                Me.AccessType = "Public"
            Else
                Me.AccessType = "Private"
            End If

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.ChartSetName = Nothing
        Me.AccessType = Nothing
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#Region "Messages"

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

#End Region

End Class