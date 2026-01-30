Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraEditors

Public Class dlgParameter
    Private _RetrunData As String

    Public ReadOnly Property ReturnData() As String
        Get
            Return _RetrunData
        End Get
    End Property

    Private Sub frmParameter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Left = (frmCMTemplate.Left + frmCMTemplate.Width) - 200
            Me.Top = frmCMTemplate.Top + 150
            TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(3).Height = 1
            Me.Height = 120
            Me.BringToFront()

            BindDevExComboBoxWithValueMember(cmbTemplate, frmCMTemplate.dsVenderData.Tables(0), "TemplateID", "TemplateName", "Select Template")
            txtNewTemplate.Focus()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim li As DevExpress.XtraEditors.ListBoxControl = New DevExpress.XtraEditors.ListBoxControl
            li.Text = txtNewTemplate.Text.Trim.ToLower
            If (String.IsNullOrEmpty(txtNewTemplate.Text.Trim)) Then
                XtraMessageBox.Show("Please enter template name.")
                _RetrunData = "NoData"
            ElseIf (chkCopyTemplate.Checked AndAlso cmbTemplate.SelectedIndex = 0) Then
                XtraMessageBox.Show("Please Select Template To Copy from.")
                _RetrunData = "NoData"
            ElseIf FindItemInvCombobox(txtNewTemplate.Text.Trim, cmbTemplate) > -1 Then
                cmbTemplate.Focus()
                XtraMessageBox.Show("New tempalate name can not be same as existing template.")
                _RetrunData = "NoData"
            Else
                _RetrunData = txtNewTemplate.Text.Trim() & "#" & TryCast(cmbTemplate.SelectedItem, IOS.Library.clsComboBoxItem).Value
                Me.Close()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Function FindItemInvCombobox(ByVal str As String, ByRef cmb As DevExpress.XtraEditors.ComboBoxEdit) As Integer
        For j = 0 To cmb.Properties.Items.Count - 1
            If cmb.Properties.Items(j).Text.ToLower = str.ToLower Then
                Return j
            End If
        Next
        Return -1
    End Function

    Private Sub chkCopyTemplate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCopyTemplate.CheckedChanged
        Try
            cmbTemplate.Visible = chkCopyTemplate.Checked
            If (chkCopyTemplate.Checked) Then
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(3).Height = 50
                Me.Height = 250
            Else
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(3).Height = 1
                Me.Height = 250
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class