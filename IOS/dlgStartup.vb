Public Class dlgStartup

	Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
		Try
			DialogResult = DialogResult.OK
		Catch
		End Try
	End Sub

	Public Sub OpenExitDialog(ByVal fromLeft As Integer, ByVal fromTop As Integer)
		Me.BringToFront()
		Me.StartPosition = FormStartPosition.Manual
		Me.Location = New Point(fromLeft, fromTop)
		If Me.ShowDialog() = DialogResult.OK Then
			End
		End If
	End Sub

End Class