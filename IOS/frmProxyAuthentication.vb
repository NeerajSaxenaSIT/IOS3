Public Class frmProxyAuthentication 
    Private _proxyUsername As String
    Public Property ProxyUsername() As String
        Get
            Return _proxyUsername
        End Get
        Set(ByVal value As String)
            _proxyUsername = value
        End Set
    End Property

    Private _proxyPassword As String
    Public Property ProxyPassword() As String
        Get
            Return _proxyPassword
        End Get
        Set(ByVal value As String)
            _proxyPassword = value
        End Set
    End Property

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.ProxyUsername = Nothing
        Me.ProxyPassword = Nothing
        Me.Close()
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Try
            If Me.txtProxyUsername.Text = "" Then
                MsgBox("Proxy User Name is required.")
                Me.txtProxyUsername.Focus()
            ElseIf Me.txtProxyPassword.Text = "" Then
                MsgBox("Proxy Password is required.")
                Me.txtProxyUsername.Focus()
            Else
                Me.ProxyUsername = txtProxyUsername.Text.Trim()
                Me.ProxyPassword = txtProxyPassword.Text.Trim()
                Me.Close()
                DialogResult = DialogResult.OK
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class