Public Class SandBoxListBox

    'Public Shared Sub BindDataWithOutItem(ByRef lst As VIBlend.WinForms.Controls.vListBox, ByRef data As DataTable, ByVal valueField As String, ByVal textField As String)
    '    lst.SuspendLayout()
    '    lst.Items.Clear()
    '    lst.Refresh()
    '    If (data.IsValid) Then
    '        lst.DataSource = data
    '        lst.DisplayMember = textField
    '        lst.ValueMember = valueField
    '    End If
    '    lst.Update()
    '    lst.ResumeLayout()
    'End Sub
    'Public Shared Sub BindData(ByRef lst As VIBlend.WinForms.Controls.vListBox, ByRef data As DataTable, ByVal valueField As String, ByVal textField As String)
    '    lst.SuspendLayout()
    '    lst.Items.Clear()
    '    lst.Refresh()
    '    If (data.IsValid) Then
    '        For Each Item As DataRow In data.Rows
    '            Dim objItem As New VIBlend.WinForms.Controls.ListItem()
    '            objItem.Text = Item(textField)
    '            objItem.Value = Item(valueField)
    '            lst.Items.Add(objItem)
    '        Next
    '    End If
    '    lst.Update()
    '    lst.ResumeLayout()
    'End Sub
    'Public Shared Sub Clear(ByRef lst As VIBlend.WinForms.Controls.vListBox)
    '    lst.SuspendLayout()
    '    lst.Items.Clear()
    '    lst.Refresh()
    '    lst.Update()
    '    lst.ResumeLayout()
    'End Sub

End Class
