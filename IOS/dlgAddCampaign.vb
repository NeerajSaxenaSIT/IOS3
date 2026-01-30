Public Class dlgAddCampaign

#Region "Variables/Properties"

    Dim connStr As String
    Public Sub SetConnectionString(ByVal conn As String)
        connStr = connStr
    End Sub

    Private _campaignType As String
    Public Property CampaignType() As String
        Get
            Return _campaignType
        End Get
        Set(ByVal value As String)
            _campaignType = value
        End Set
    End Property

    Private _isPublic As Boolean
    Public Property IsPublic() As Boolean
        Get
            Return _isPublic
        End Get
        Set(ByVal value As Boolean)
            _isPublic = value
        End Set
    End Property

    Public newManualCampaignAdded As String = Nothing
    Public newAuditCampaignAdded As String = Nothing

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

    Private Sub AddNewCampaign()
        Try
            If _campaignType.ToLower = "manual" Then
                Dim parray()() As String = {
                    New String() {"@CampaignNameNew", Chr(39) & txtCampaignName.Text.Trim & Chr(39)},
                    New String() {"@CampaignDescription", Chr(39) & txtDescription.Text.Trim & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)},
                    New String() {"@IsPublic", IIf(IsPublic = True, 1, 0)}
                }
                Me.newManualCampaignAdded = txtCampaignName.Text.Trim
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4510, parray)(0), GetSQL(4510, parray)(1))
            ElseIf _campaignType.ToLower = "bulkimport" Then
                Dim parray()() As String = {
                    New String() {"@CampaignNameNew", Chr(39) & txtCampaignName.Text.Trim & Chr(39)},
                    New String() {"@CampaignDescription", Chr(39) & txtDescription.Text.Trim & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)},
                    New String() {"@IsPublic", IIf(IsPublic = True, 1, 0)}
                }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4911, parray)(0), GetSQL(4911, parray)(1))
            ElseIf _campaignType.ToLower = "nb_audit" Then
                Dim parray()() As String = {
                    New String() {"@CampaignName", Chr(39) & txtCampaignName.Text.Trim & Chr(39)},
                    New String() {"@CampaignDescription", Chr(39) & txtDescription.Text.Trim & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)},
                    New String() {"@IsPublic", IIf(IsPublic = True, 1, 0)}
                }
                Me.newAuditCampaignAdded = txtCampaignName.Text.Trim
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4563, parray)(0), GetSQL(4563, parray)(1))
            End If
            SetMessage(txtCampaignName.Text.Trim & " Campaign saved successfully")
        Catch ex As Exception
            SetMessage("Error : " & _campaignType & " Campaign Saving Fail")
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
                AddNewCampaign()
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
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class