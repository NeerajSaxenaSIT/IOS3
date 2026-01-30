Public Class frmFreqChooser

    Private Sub frmFreqChooser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Refresh()
        'Me.SuspendLayout()
        'Me.MdiParent = frmMDI
        'Me.WindowState = FormWindowState.Normal
        'Me.ResumeLayout()
    End Sub

    Private Sub ListViewFreqGSM_DoubleClick(sender As Object, e As EventArgs) Handles ListViewFreqGSM.DoubleClick
        Application.UseWaitCursor = True
        Application.DoEvents()
        frmMapWindow.txtSearch2GChannel.Text = ListViewFreqGSM.SelectedItems(0).Text
        frmMapWindow.rbSearch2GChannelAll.Checked = True
        Call frmMapWindow.btnSearchChannel2G_Click(Nothing, Nothing)
        Application.UseWaitCursor = False
    End Sub

    Private Sub ListViewfreqDCS_DoubleClick(sender As Object, e As EventArgs) Handles ListViewfreqDCS.DoubleClick
        Application.UseWaitCursor = True
        Application.DoEvents()
        frmMapWindow.txtSearch2GChannel.Text = ListViewfreqDCS.SelectedItems(0).Text
        frmMapWindow.rbSearch2GChannelAll.Checked = True
        Call frmMapWindow.btnSearchChannel2G_Click(Nothing, Nothing)
        Application.UseWaitCursor = False
    End Sub

End Class