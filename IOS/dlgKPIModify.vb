Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgKPIModify
    Public Creator As String
    Public fromLeft As Integer
    Public fromTop As Integer

    Sub New(ByVal techPack As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        lblTechPack.Text = techPack
    End Sub

    Public kpiModifyOption As Integer = -1

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If rbAdd.Checked = True Then
            KPIModifyOption = 0
        ElseIf rbUpdate.Checked = True Then
            KPIModifyOption = 1
        End If
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgKPIModify_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.BringToFront()
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(fromLeft, fromTop)
        If (Creator.ToUpper = Environment.UserName.ToUpper) Then
            rbUpdate.Enabled = True
            rbUpdate.Checked = True
            rbAdd.Checked = False
        Else
            rbUpdate.Enabled = False
            rbUpdate.Checked = False
            rbAdd.Checked = True
        End If
    End Sub

End Class