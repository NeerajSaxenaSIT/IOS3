Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgManualCampaign

#Region "Private Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub InsertNewManualCampaign()
        Try
            Dim parray()() As String = {
                New String() {"@CampaignNameNew", Chr(39) & txtCampaignName.Text.Trim & Chr(39)},
                New String() {"@CampaignDescription", Chr(39) & txtDescription.Text.Trim & Chr(39)},
                New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)}
            }
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4510, parray)(0), GetSQL(4510, parray)(1))
            SetMessage("Manual campaign name saved successfully")
        Catch ex As Exception
            SetMessage("Error : Manual Campaign Saving Fail")
        End Try
    End Sub

#End Region

#Region "Form Events"

    Private Sub dlgManualCampaign_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtCampaignName.Text = String.Empty
            txtDescription.Text = String.Empty
        Catch ex As Exception
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

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtCampaignName.Text = "" Then
                SetMessage("Please Enter Campaign Name")
            Else
                InsertNewManualCampaign()
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class