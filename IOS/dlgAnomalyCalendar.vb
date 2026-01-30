Public Class dlgAnomalyCalendar

    Dim clr As Color = Color.Empty
    Private Sub dtNavigator_CustomDrawDayNumberCell(sender As Object, e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs) Handles dtNavigator.CustomDrawDayNumberCell
        Dim isDisabledDate As Boolean = True
        isDisabledDate = False
       
        If e.Date < Today Then isDisabledDate = True
        If isDisabledDate Then
            e.State = DevExpress.Utils.Drawing.ObjectState.Disabled
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.Gray, New System.Drawing.Point(e.ContentBounds.Location.X, e.ContentBounds.Location.Y))
            e.Handled = True
        Else
            e.Handled = False
        End If

        If e.Disabled = False And e.Holiday = False And e.Inactive = False And e.Selected = False And e.Today = False Then
            clr = e.Style.ForeColor
        End If
        If e.Holiday = True And e.Disabled = False And e.Inactive = False And clr <> Color.Empty Then
            e.Style.ForeColor = clr
        End If
    End Sub

    Private Sub dtNavigator_SelectionChanged(sender As Object, e As EventArgs) Handles dtNavigator.SelectionChanged
        Try
            'frmMapWindow.trafficDenStartDate = dtNavigator.SelectionStart
            'frmMapWindow.trafficDenEndDate = dtNavigator.SelectionEnd.AddDays(-1)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtNavigator_MouseUp(sender As Object, e As MouseEventArgs) Handles dtNavigator.MouseUp
        Try
            'frmMapWindow.LoadMapTrafficDensityData()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub
End Class