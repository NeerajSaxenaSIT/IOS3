Imports LidorSystems.IntegralUI.Lists
Imports IOS.Library

Public Class dlgTagInsert

    Private _tagName As String = Nothing
    Public ReadOnly Property TagName() As String
        Get
            Return _tagName
        End Get
    End Property

    Private _IsValid As Boolean = Nothing
    Public ReadOnly Property IsValid() As Boolean
        Get
            Return _IsValid
        End Get
    End Property

    Private _tagDescription As String = Nothing
    Public ReadOnly Property TagDescription() As String
        Get
            Return _tagDescription
        End Get
    End Property

    Private _tagIsPrivate As Boolean = False
    Public ReadOnly Property TagIsPrivate() As Boolean
        Get
            Return _tagIsPrivate
        End Get
    End Property

    Private Sub SetMessage(ByVal message As String)
        lblMsg.ForeColor = Color.Red
        lblMsg.Visible = True
        lblMsg.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblMsg.Text = ""
        lblMsg.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        _IsValid = False
        Me.Hide()
    End Sub

    Private Sub btnTagInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTagInsert.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If Not (txtTagDescription.Text.Trim = "") AndAlso Not (txtTagName.Text.Trim = "") Then
                _IsValid = True
                _tagName = txtTagName.Text
                _tagDescription = txtTagDescription.Text
                _tagIsPrivate = IIf(rdoPrivate.Checked, True, False)
                'lblMsg.Visible = False
                Me.Close()
            Else
                SetMessage("Please Enter Tag Name And Description")
                'lblMsg.Visible = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BringToFront()
        _IsValid = False
        txtTagName.Focus()
    End Sub

End Class
