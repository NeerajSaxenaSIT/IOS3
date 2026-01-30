Imports IOS.Library
Imports IOS.DataLibrary

Public Class dlgCopyFromAlert

#Region "Variables"

    Private parray()() As String = Nothing
    Private ConnStringAndSqlParam() As String = Nothing
    Private dtAlertConfig As DataTable = Nothing

#End Region

#Region "Methods"

    Private Sub LoadAlertList()
        RemoveHandler cmbAlert.SelectedIndexChanged, AddressOf cmbAlert_SelectedIndexChanged
        parray = Nothing
        ConnStringAndSqlParam = Nothing
        ConnStringAndSqlParam = GetSQL(3811, parray)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(ConnStringAndSqlParam(0), ConnStringAndSqlParam(1))
        BindDevExComboBoxWithValueMember(cmbAlert, dt, "ALERT_RULEID", "AlertName", "Select Alert")
        AddHandler cmbAlert.SelectedIndexChanged, AddressOf cmbAlert_SelectedIndexChanged
    End Sub

    Private Sub GetAlertConfigurationDetails(ByVal alertRuleID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        parray = {
            New String() {"@AlertRuleID", alertRuleID}
        }
        strConnection = GetSQL(3812, parray)(0)
        sqlParam = GetSQL(3812, parray)(1)
        dtAlertConfig = New DataTable()
        dtAlertConfig = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadKPIRulesForAlert()
        GetAlertConfigurationDetails(CType(cmbAlert.SelectedItem, clsComboBoxItem).Value)
        If dtAlertConfig.Rows.Count > 0 Then
            Dim dtTemp As DataTable = dtAlertConfig.DefaultView.ToTable(True, {"Technology", "ObjectType", "ObjectReported", "KPI_Name", "KPI_RuleTypeName", "KPI_RULEID", "ALERT_RULEID", "KPI_RuleTypeName_Short", "KPI_RuleType", "DataAvailable"})
            BindDevExComboBoxWithValueMember(cmbKPIRuleForAlert, dtTemp, "KPI_RULEID", "KPI_Name", "Select KPI Rule")
        End If
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

#End Region

#Region "Events"

    Private Sub frmCopyFromAlert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadAlertList()
            AlertCopyFromCommitted = False

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbAlert_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbAlert.SelectedIndex <> 0 Then
                LoadKPIRulesForAlert()
            Else
                ClearComboBox(cmbKPIRuleForAlert, "Select KPI Rule")
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If cmbAlert.SelectedIndex = 0 Then
                SetMessage("Please Select Alert Name")
                Exit Sub
            ElseIf cmbKPIRuleForAlert.SelectedIndex = 0 Then
                SetMessage("Please Select KPI Rule")
                Exit Sub
            ElseIf (ceCopyFilterStrings.Checked = False) Then
                SetMessage("Please Select Copy Filters Check Box")
                Exit Sub
            End If

            AlertCopyFromCommitted = True
            frmAlertManager.copyFromSrcAlertRuleID = TryCast(cmbAlert.SelectedItem, clsComboBoxItem).Value
            frmAlertManager.copyFromSrcKpiRuleID = TryCast(cmbKPIRuleForAlert.SelectedItem, clsComboBoxItem).Value
            frmAlertManager.copyFilterStringsFromKpiRule = ceCopyFilterStrings.Checked

            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

#End Region

End Class