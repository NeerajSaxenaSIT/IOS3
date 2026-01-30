Imports IOS.Library

Public Class frmCategoryManagerDialog

#Region "Variables"

    Dim conStr As String = IOS.Configuration.IOSAppConfigManage.IOSServer
    Dim dtCategoryData As DataTable = Nothing

#End Region

#Region "Properties"

    Private _RetrunData As IOS.Library.IOSCategoryManager
    Public ReadOnly Property ReturnData() As IOS.Library.IOSCategoryManager
        Get
            Return _RetrunData
        End Get
    End Property

#End Region

#Region "Form & Controls Events"

    Private Sub frmCategoryManagerDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            TableLayoutPanel2.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(1).Height = 1
            TableLayoutPanel2.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(2).Height = 1
            Me.Height = 140
            BindCategory()
            lblMsg.Visible = False
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnApplyToHighLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyToHighLight.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (cmbCategoryList.SelectedIndex > 0) Then
                Dim categoryManage As New IOSCategoryManager()
                categoryManage.GetCategoryID = Convert.ToInt32(TryCast(cmbCategoryList.SelectedItem, clsComboBoxItem).Value)
                categoryManage.GetCategoryName = cmbCategoryList.SelectedItem.ToString
                categoryManage.IsApplyTo = IOSCategoryManager.BY_SELECTION
                If rbBtnSaveSchedule.Checked Or rbBtnSaveWithRollback.Checked Then
                    If (dtpStartDate.Text.Length >= 10) Then
                        categoryManage.IsSchdule = True
                        categoryManage.GetStartDate = CDate(dtpStartDate.EditValue)

                        If rbBtnSaveWithRollback.Checked Then
                            If (IsEndDateValid(CDate(dtpStartDate.EditValue), CDate(dtpDateEnd.EditValue))) Then
                                categoryManage.GetEndDate = CDate(dtpDateEnd.EditValue)
                                categoryManage.GetSchduleType = 3 'As ScheduleType SaveStartStop 
                                Me._RetrunData = categoryManage
                                Me.Hide()
                            Else
                                lblMsg.Text = "End date should be greater then start date."
                                lblMsg.Visible = True
                            End If
                        Else
                            categoryManage.GetEndDate = CDate(dtpDateEnd.Properties.MinValue) 'Nothing
                            categoryManage.GetSchduleType = 2 'As ScheduleType SaveStart
                            Me._RetrunData = categoryManage
                            Me.Hide()
                        End If
                    Else
                        lblMsg.Text = "Select Start Date"
                        lblMsg.Visible = True
                    End If
                Else
                    categoryManage.IsSchdule = False
                    categoryManage.GetEndDate = CDate(dtpDateEnd.Properties.MinValue) 'Nothing
                    categoryManage.GetSchduleType = 1 'As ScheduleType SaveNow 
                    Me._RetrunData = categoryManage
                    Me.Hide()
                End If
            Else
                lblMsg.Text = "Select Category"
                lblMsg.Visible = True
                Me._RetrunData = Nothing
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (cmbCategoryList.SelectedIndex > 0) Then
                Dim categoryManage As New IOSCategoryManager()
                categoryManage.GetCategoryID = Convert.ToInt32(TryCast(cmbCategoryList.SelectedItem, clsComboBoxItem).Value)
                categoryManage.GetCategoryName = cmbCategoryList.SelectedItem.ToString
                categoryManage.IsApplyTo = IOSCategoryManager.BY_GRID
                If rbBtnSaveSchedule.Checked Or rbBtnSaveWithRollback.Checked Then
                    If (dtpStartDate.Text.Length >= 10) Then
                        categoryManage.IsSchdule = True
                        categoryManage.GetStartDate = CDate(dtpStartDate.EditValue)

                        If rbBtnSaveWithRollback.Checked Then
                            If (IsEndDateValid(CDate(dtpStartDate.EditValue), CDate(dtpDateEnd.EditValue))) Then
                                categoryManage.GetEndDate = CDate(dtpDateEnd.EditValue)
                                categoryManage.GetSchduleType = 3 'As ScheduleType SaveStartStop 
                                Me._RetrunData = categoryManage
                                Me.Hide()
                            Else
                                lblMsg.Text = "End date should be greater then start date."
                                lblMsg.Visible = True
                            End If
                        Else
                            categoryManage.GetEndDate = CDate(dtpDateEnd.Properties.MinValue) 'Nothing
                            categoryManage.GetSchduleType = 2 'As ScheduleType SaveStart
                            Me._RetrunData = categoryManage
                            Me.Hide()
                        End If
                    Else
                        lblMsg.Text = "Select Start Date"
                        lblMsg.Visible = True

                    End If
                Else
                    categoryManage.IsSchdule = False
                    categoryManage.GetEndDate = CDate(dtpDateEnd.Properties.MinValue) 'Nothing
                    categoryManage.GetSchduleType = 1 'As ScheduleType SaveNow 
                    Me._RetrunData = categoryManage
                    Me.Hide()
                End If
            Else
                lblMsg.Text = "Select Category"
                lblMsg.Visible = True
                Me._RetrunData = Nothing
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnApplyToSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyToSelected.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (cmbCategoryList.SelectedIndex > 0) Then
                Dim categoryManage As New IOSCategoryManager()
                categoryManage.GetCategoryID = Convert.ToInt32(TryCast(cmbCategoryList.SelectedItem, clsComboBoxItem).Value)
                categoryManage.GetCategoryName = cmbCategoryList.SelectedItem.ToString
                categoryManage.IsApplyTo = IOSCategoryManager.BY_OBJECT
                If rbBtnSaveSchedule.Checked Or rbBtnSaveWithRollback.Checked Then
                    If (dtpStartDate.Text.Length >= 10) Then
                        categoryManage.IsSchdule = True
                        categoryManage.GetStartDate = CDate(dtpStartDate.EditValue)

                        If rbBtnSaveWithRollback.Checked Then
                            If (IsEndDateValid(CDate(dtpStartDate.EditValue), CDate(dtpDateEnd.EditValue))) Then
                                categoryManage.GetEndDate = CDate(dtpDateEnd.EditValue)
                                categoryManage.GetSchduleType = 3 'As ScheduleType SaveStartStop 
                                Me._RetrunData = categoryManage
                                Me.Hide()
                            Else
                                lblMsg.Text = "End date should be greater then start date."
                                lblMsg.Visible = True
                            End If
                        Else
                            categoryManage.GetEndDate = CDate(dtpDateEnd.Properties.MinValue) 'Nothing
                            categoryManage.GetSchduleType = 2 'As ScheduleType SaveStart
                            Me._RetrunData = categoryManage
                            Me.Hide()
                        End If
                    Else
                        lblMsg.Text = "Select Start Date"
                        lblMsg.Visible = True
                    End If
                Else
                    categoryManage.IsSchdule = False
                    categoryManage.GetEndDate = CDate(dtpDateEnd.Properties.MinValue) 'Nothing
                    categoryManage.GetSchduleType = 1 'As ScheduleType SaveNow 
                    Me._RetrunData = categoryManage
                    Me.Hide()
                End If
            Else
                lblMsg.Text = "Select Category"
                lblMsg.Visible = True
                Me._RetrunData = Nothing
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
    End Sub

    Private Sub rbBtnSaveNow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbBtnSaveNow.CheckedChanged
        If (rbBtnSaveNow.Checked) Then
            rbBtnSaveSchedule.Checked = False
            rbBtnSaveWithRollback.Checked = False
            TableLayoutPanel2.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(1).Height = 1
            TableLayoutPanel2.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(2).Height = 1
            Me.Height = 140
            dtpStartDate.Visible = False
            dtpDateEnd.Visible = False
            vlblEndDate.Visible = False
            vlblStartDate.Visible = False
            'vdtpDateEnd.Value = Nothing
            lblMsg.Text = String.Empty
            dtpStartDate.Text = Nothing
            dtpDateEnd.Text = "(Null)"
        End If
    End Sub

    Private Sub rbBtnSaveSchedule_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbBtnSaveSchedule.CheckedChanged
        If (rbBtnSaveSchedule.Checked) Then
            rbBtnSaveNow.Checked = False
            rbBtnSaveWithRollback.Checked = False
            TableLayoutPanel2.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(1).Height = 30
            TableLayoutPanel2.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(2).Height = 1
            Me.Height = 170
            dtpStartDate.Visible = rbBtnSaveSchedule.Checked
            dtpDateEnd.Visible = False
            vlblEndDate.Visible = False
            vlblStartDate.Visible = rbBtnSaveSchedule.Checked
            'vdtpDateEnd.Value = Nothing
            lblMsg.Text = String.Empty
            dtpStartDate.EditValue = System.DateTime.Now()
            dtpDateEnd.Text = "(Null)"
        End If
    End Sub

    Private Sub rbBtnSaveWithRollback_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbBtnSaveWithRollback.CheckedChanged
        If (rbBtnSaveWithRollback.Checked) Then
            rbBtnSaveNow.Checked = False
            rbBtnSaveSchedule.Checked = False
            TableLayoutPanel2.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(1).Height = 30
            TableLayoutPanel2.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(2).Height = 30
            Me.Height = 190
            dtpStartDate.Visible = rbBtnSaveWithRollback.Checked
            dtpDateEnd.Visible = rbBtnSaveWithRollback.Checked
            vlblEndDate.Visible = rbBtnSaveWithRollback.Checked
            vlblStartDate.Visible = rbBtnSaveWithRollback.Checked
            'vdtpDateEnd.Value = Nothing
            lblMsg.Text = String.Empty
            dtpStartDate.EditValue = System.DateTime.Now()
            dtpDateEnd.EditValue = System.DateTime.Now().AddDays(1).AddSeconds(-1)
        End If
    End Sub

#End Region

#Region "Helper"

    Sub BindCategory()
        cmbCategoryList.SuspendLayout()
        cmbCategoryList.Properties.Items.Clear()
        cmbCategoryList.Properties.Items.Insert(0, "Select Category")
        If (Me.dtCategoryData IsNot Nothing) Then
            BindDevExComboBoxWithValueMember(cmbCategoryList, dtCategoryData, "CategoryID", "CategoryName")
        End If
        cmbCategoryList.Refresh()
        cmbCategoryList.ResumeLayout()
    End Sub

    Private Function IsEndDateValid(ByVal startDate As DateTime, ByVal endDate As DateTime) As Boolean
        If (startDate < endDate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub SetCategoryData(ByRef data As DataTable)
        Me.dtCategoryData = data
    End Sub

#End Region

End Class