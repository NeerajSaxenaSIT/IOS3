Imports DevExpress.XtraScheduler
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors

Public Class dlgTechPeriodCalculation

#Region "variables"

    Public procType As String = Nothing
    Public periodName As String = Nothing
    Public dateStart As Date = Nothing
    Public dateEnd As Date = Nothing
    Public dtRange As String = Nothing

#End Region

#Region "Events"

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub dlgTechPeriodCalculation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.procType.ToUpper = "STATS" Then
            Me.Text = "Technology - Period Calculation"
        ElseIf Me.procType.ToUpper = "EVAL" Then
            Me.Text = "Technology - Period Comparison"
        End If
        dateNavigator.ResetState(Now().AddDays(-30), Now().AddDays(-30))
        dateNavigator.SetSelection(Now.AddDays(-7), Now())
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            If txtPeriodName.Text.Trim = String.Empty Then
                SetMessage("Period Name Required !!!")
                Exit Sub
            End If

            periodName = txtPeriodName.Text.Trim
            dtRange = dateNavigator.SelectedRanges(0).ToString
            DialogResult = DialogResult.OK
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            DialogResult = DialogResult.Cancel
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub dateNavigator_CustomDrawDayNumberCell(sender As Object, e As Calendar.CustomDrawDayNumberCellEventArgs) Handles dateNavigator.CustomDrawDayNumberCell
        Dim isDisabledDate As Boolean = True

        If (e.Date >= dateStart And e.Date <= dateEnd) Or (e.Date.ToString("yyyyMMdd") = CDate(dateEnd).ToString("yyyyMMdd")) Then
            isDisabledDate = False
        End If

        If isDisabledDate Then
            e.State = DevExpress.Utils.Drawing.ObjectState.Disabled
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.Gray, New Point(e.ContentBounds.Location.X, e.ContentBounds.Location.Y))
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

#End Region

#Region "Private Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

#End Region

End Class