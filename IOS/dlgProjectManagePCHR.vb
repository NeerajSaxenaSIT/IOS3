Imports IOS.DataLibrary
Imports IOS.Library
Imports DevExpress.XtraEditors

Public Class dlgProjectManagePCHR
    Private _conStr As String = IOS.Configuration.IOSAppConfigManage.IOSServer

#Region "Properties"

    Public Property IOSConnectionString() As String
        Get
            Return _conStr
        End Get
        Set(ByVal value As String)
            _conStr = value
        End Set
    End Property

#End Region

#Region "Helper Methods"

    Private Sub BindRNCList()
        Try
            Dim parray()() As String = Nothing
            Dim sqlCommand As String = GetSQL(IOSSqlIds.PCHR_RNCNAME, parray, dt_IOS_SQL)(1)
            Dim connstring As String = GetSQL(IOSSqlIds.PCHR_RNCNAME, parray, dt_IOS_SQL)(0)
            Dim dtRNC As DataTable = DataAccessorSQL.ExecuteDataTable(connstring, sqlCommand)
            If (dtRNC.Rows.Count >= 1) Then
                For Each dr As DataRow In dtRNC.Rows
                    lstRNC.Items.Add(dr("BSCName").ToString)
                Next
            Else
                lstRNC.Items.Clear()
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Form & Control Events"

    Private Sub frmProjectManagerPCHR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lstRNC.Enabled = True
            Dim startdate As DateTime = DateAdd(DateInterval.Hour, -2, Now())
            dtpStartDate.EditValue = startdate
            dtpEndDate.EditValue = Now()
            BindRNCList()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub txtSelectIMSI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSelectIMSI.KeyPress
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If DateDiff(DateInterval.Hour, CDate(dtpStartDate.EditValue), CDate(dtpEndDate.EditValue), FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) > 24 Then
                If cb_UserLogs.Checked = True Then
                    Dim res As DialogResult = XtraMessageBox.Show("Warning: A large timeframe has been chosen. It can take a while before all files are processed... Recommended timeframe for an RNC is a few hours. It takes about 10 minutes of parsing for 1 hour of RNC data. Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    If res = DialogResult.No Then
                        Exit Sub
                    End If
                End If
            End If

            lblMSG.Text = ""
            If (txtProjectName.Text.Length > 1) Then
                Dim isRNCorIMSI As String = IIf(txtSelectIMSI.Text.Length >= 1, "IMSI", "RNC")
                Dim rncValue As String = ""
                Dim imsiValue As String = ""
                Dim rncidfilterValue As String = ""
                Dim cellidfilterValue As String = ""
                If (txtSelectIMSI.Text.Length >= 1) Then
                    imsiValue = txtSelectIMSI.Text.Trim
                End If
                If (txtRNCID.Text.Length >= 1) Then
                    rncidfilterValue = txtRNCID.Text.Trim
                End If
                If (txtCELLID.Text.Length >= 1) Then
                    cellidfilterValue = txtCELLID.Text.Trim
                End If
                If (lstRNC.SelectedItem IsNot Nothing) Then
                    rncValue = lstRNC.SelectedItem.ToString
                Else
                    lblMSG.Text = "Please Select RNC"
                    Exit Sub
                End If

                IOS.DataLibrary.clsSQLCommands.CreatePCHRProject(_conStr, txtProjectName.Text.Trim, isRNCorIMSI, rncValue, imsiValue, rncidfilterValue, cellidfilterValue, dtpStartDate.EditValue, dtpEndDate.EditValue, Environment.UserName.ToString, cb_UserLogs.Checked, cb_SpecLogs.Checked, cb_CellLogs.Checked)
                lblMSG.Text = "Project Created"
                txtProjectName.Text = ""
                txtSelectIMSI.Text = ""
                If objFrmPCHR IsNot Nothing Then
                    objFrmPCHR.RefrashProjectTreeListView()
                End If
            Else
                lblMSG.Text = "Please Enter Project Name."
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnChoseRNCbasedIMSI_Click(sender As Object, e As EventArgs) Handles btnChoseRNCbasedIMSI.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (txtSelectIMSI.Text.Length >= 1) Then
                Dim imsiValue = txtSelectIMSI.Text.Trim()
                Dim parray()() As String = {New String() {"@IMSI", Chr(39) & imsiValue & Chr(39)}, New String() {"@starttime", Chr(39) & dtpStartDate.EditValue & Chr(39)}, New String() {"@endtime", Chr(39) & dtpEndDate.EditValue & Chr(39)}}

                Dim sqlCommandAndConnection() As String = GetSQL(IOSSqlIds.PCHR_IMPORT_IMSI2RNC, parray, dt_IOS_SQL)
                Dim dtRNC As DataTable = DataAccessorSQL.ExecuteDataTable(sqlCommandAndConnection(0), sqlCommandAndConnection(1))
                lstRNC.Items.Clear()
                If (dtRNC Is Nothing) Then
                    lblMSG.Text = "No any RNC"
                    Exit Sub
                End If

                If (dtRNC.Rows.Count >= 1) Then
                    For Each dr As DataRow In dtRNC.Rows
                        lstRNC.Items.Add(dr("BSCNAME").ToString)
                    Next
                Else
                    lstRNC.Items.Clear()
                    lblMSG.Text = "No any RNC"
                End If
            Else
                lblMSG.Text = "Please Enter IMSI"
                Exit Sub
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class