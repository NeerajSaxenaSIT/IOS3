Public Class SandBoxCheckedListBox

    Public Shared Sub BindDataWithoutItem(ByRef lst As DevExpress.XtraEditors.CheckedListBoxControl, ByRef data As DataTable, ByVal valueField As String, ByVal textField As String, ByVal checkedIds As List(Of String))
        lst.SuspendLayout()
        lst.Items.Clear()
        lst.Refresh()
        If (data.IsValid) Then
            lst.DataSource = data
            lst.DisplayMember = textField
            lst.ValueMember = valueField
        End If
        lst.Update()
        lst.ResumeLayout()
    End Sub

    Public Shared Sub BindDataToCheckedListBox(ByRef lst As DevExpress.XtraEditors.CheckedListBoxControl, ByRef data As DataTable, ByVal valueField As String, ByVal textField As String, ByVal tagField As String, ByVal checkedIds As List(Of String))
        lst.SuspendLayout()
        lst.Items.Clear()
        lst.Refresh()
        If (data.IsValid) Then
            For Each Item As DataRow In data.Rows
                Dim objItem As New DevExpress.XtraEditors.Controls.CheckedListBoxItem()
                objItem.Description = Item(textField)
                objItem.Value = Item(valueField)
                objItem.Tag = Item(tagField)
                If (checkedIds.Contains(Item(valueField))) Then
                    objItem.CheckState = System.Windows.Forms.CheckState.Checked
                    lst.Items.Insert(0, objItem)
                Else
                    objItem.CheckState = System.Windows.Forms.CheckState.Unchecked
                    lst.Items.Add(objItem)
                End If
                'lst.Items.Add(objItem)
            Next
        End If
        lst.Update()
        lst.ResumeLayout()
    End Sub

    Public Shared Sub Clear(ByRef lst As DevExpress.XtraEditors.CheckedListBoxControl)
        lst.SuspendLayout()
        lst.Items.Clear()
        lst.DataSource = Nothing
        lst.Refresh()
        lst.Update()
        lst.ResumeLayout()
    End Sub

End Class
