Public Class dlgSettingsNetworkFeatures 

#Region "Variables"

    Dim dtClone As DataTable

#End Region

#Region "Form & Control Events"

    Private Sub dlgSettingsNetworkFeatures_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'clear listbox
            lbcNetworkTables.Items.Clear()

            'clone
            dtClone = GetDataTable("dt_Map_Configuration").Copy

            'fill listbox
            For Each drow As DataRow In dtClone.Rows
                lbcNetworkTables.Items.Add(drow.Item("LayerName").ToString)
            Next

            'set first record
            lbcNetworkTables.SelectedItem = lbcNetworkTables.Items(0)

            'handlers...
            AddHandler txtLineWidth.KeyPress, AddressOf txtLineWidth_KeyPress
            AddHandler txtLineWidth.Leave, AddressOf txtLineWidth_Leave
            AddHandler txtBeamWidth.Leave, AddressOf txtBeamWidth_Leave
            AddHandler ColorPickerEdit1.Leave, AddressOf SuperColorPicker_Leave
            AddHandler txtRelativeSize.Leave, AddressOf txtRelativeSize_Leave
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub txtLineWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            'cancel keys
            e.Handled = True
        End If
    End Sub

    Private Sub txtLineWidth_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtLineWidth.Text <> "" Then
            dtClone.Rows(lbcNetworkTables.SelectedIndex)("LayerLineWidth") = txtLineWidth.Text
        End If
    End Sub

    Private Sub txtBeamWidth_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtBeamWidth.Text <> "" Then
            dtClone.Rows(lbcNetworkTables.SelectedIndex)("LayerBeamWidth") = txtBeamWidth.Text
        End If
    End Sub

    Private Sub SuperColorPicker_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not ColorPickerEdit1.Color.ToString Is Nothing Then
            dtClone.Rows(lbcNetworkTables.SelectedIndex)("LayerLineColor") = ColorPickerEdit1.Color.ToArgb.ToString
        End If
    End Sub

    Private Sub txtRelativeSize_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtRelativeSize.Text <> "" Then
            dtClone.Rows(lbcNetworkTables.SelectedIndex)("LayerRelativeSize") = txtRelativeSize.Text
        End If
    End Sub

    Private Sub lbcNetworkTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbcNetworkTables.SelectedIndexChanged
        If lbcNetworkTables.SelectedIndex = -1 Then
            Exit Sub
        End If

        For Each drow As DataRow In dtClone.Rows
            Dim teststr As String = lbcNetworkTables.Items(lbcNetworkTables.SelectedIndex).ToString
            If teststr = drow.Item("LayerName").ToString Then
                txtLineWidth.Text = drow.Item("LayerLineWidth").ToString
                ColorPickerEdit1.Color = ColorInt2Color(CInt(drow.Item("LayerLineColor").ToString))
                txtBeamWidth.Text = drow.Item("LayerBeamWidth").ToString
                txtRelativeSize.Text = drow.Item("LayerRelativeSize").ToString
            End If
        Next
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            frmMapWindow.ChangeNetworkFeatures(dtClone)
            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class