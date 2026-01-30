Imports DevExpress.XtraEditors.Controls
Imports System.Drawing

Public Class ucProgressPanel

    Public Sub StopProgress()
        PictureEdit2.BringToFront()
        PictureEdit2.Visible = True
        PictureEdit1.Visible = False
    End Sub

    Public Sub StartProgress()
        PictureEdit1.BringToFront()
        PictureEdit1.Visible = True
        PictureEdit2.Visible = False
    End Sub

    Private Sub ucProgressPanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        StopProgress()
    End Sub

End Class
