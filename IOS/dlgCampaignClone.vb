Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgCampaignClone

#Region "Variables/Properties"

    Public campaignID As Integer = 0
    Public campaignType As String = Nothing

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

    Private Sub InsertNewCampaignClone()
        Try
            If campaignType.ToUpper = "BULK IMPORT" Then

                Dim parray()() As String = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)},
                    New String() {"@CampaignNameNew", Chr(39) & txtCampaignName.Text.Trim & Chr(39)},
                    New String() {"@CampaignDecsriptionNew", Chr(39) & txtDescription.Text.Trim & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)}
                }
                DataAccessorODBC.ExecuteNonQuery(GetSQL(4931, parray)(0), GetSQL(4931, parray)(1))

            ElseIf campaignType.ToUpper = "AUDIT" Then

                Dim parray()() As String = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)},
                    New String() {"@CampaignNameNew", Chr(39) & txtCampaignName.Text.Trim & Chr(39)},
                    New String() {"@CampaignDecsriptionNew", Chr(39) & txtDescription.Text.Trim & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)}
                }
                DataAccessorODBC.ExecuteNonQuery(GetSQL(4942, parray)(0), GetSQL(4942, parray)(1))

            Else

                Dim parray()() As String = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)},
                    New String() {"@CampaignNameNew", Chr(39) & txtCampaignName.Text.Trim & Chr(39)},
                    New String() {"@CampaignDecsriptionNew", Chr(39) & txtDescription.Text.Trim & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)}
                }
                DataAccessorODBC.ExecuteNonQuery(GetSQL(4507, parray)(0), GetSQL(4507, parray)(1))

            End If

            SetMessage("Campaign clone name saved successfully")
        Catch ex As Exception
            SetMessage("Error : Campaign clone Saving Fail")
        End Try
    End Sub

#End Region

#Region "Form Events"

    Private Sub dlgCampaignClone_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                SetMessage("Please Enter Campaign Clone Name")
            Else
                InsertNewCampaignClone()
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
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