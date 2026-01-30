Public Class frmScheduleManagerDialog

#Region "Variables"

    Dim conStr As String = "Dsn=IOS_Server;uid=sa;pwd=1234"
    Dim dtCategoryData As DataTable = Nothing
    Dim lastStartTime As DateTime = Nothing

#End Region

#Region "Properties"

    Private _RetrunData As IOS.Library.IOSCategoryManager
    Public ReadOnly Property ReturnData() As IOS.Library.IOSCategoryManager
        Get
            Return _RetrunData
        End Get
    End Property

#End Region

#Region "Helper Methods"

    Public Sub SetCategoryData(ByRef data As DataTable)
        Me.dtCategoryData = data
    End Sub

    Public Sub SetLastStartTime(ByRef lStsrtTime As DateTime)
        Me.lastStartTime = lStsrtTime
    End Sub

    Private Function IsNewDateValid(ByVal oldStartDate As DateTime, ByVal newStartDate As DateTime) As Boolean
        If (oldStartDate < newStartDate) Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Form & Control Events"

    Private Sub frmScheduleManagerDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (lastStartTime = Nothing) Then
            btnApplySchedule.Enabled = False
            lblStartTime.Text = "Sorry, No last Schedule time, try again"
        Else
            lblStartTime.Text = lastStartTime.ToString()
            dtpStartDate.EditValue = lblStartTime.Text
            btnApplySchedule.Enabled = True
        End If
        lblMsg.Visible = False
    End Sub

    Private Sub btnApplySchedule_Click(sender As Object, e As EventArgs) Handles btnApplySchedule.Click
        Try
            Dim categoryManage As New IOS.Library.IOSCategoryManager()
            If (IsNewDateValid(CDate(lblStartTime.Text), CDate(dtpStartDate.EditValue))) Then
                categoryManage.IsSchdule = True
                categoryManage.GetStartDate = CDate(dtpStartDate.EditValue)
                categoryManage.IsApplyTo = IOS.Library.IOSCategoryManager.BYSCHEDULE_UPDATE
                Me._RetrunData = categoryManage
                Me.Hide()
            Else
                lblMsg.Text = "New date should be greater then current start date."
                lblMsg.ForeColor = Color.Red
                lblMsg.Visible = True
                Me._RetrunData = Nothing
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Hide()
    End Sub

#End Region

End Class