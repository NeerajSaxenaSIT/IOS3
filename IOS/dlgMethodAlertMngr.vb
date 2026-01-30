Imports IOS.Library
Imports IOS.DataLibrary

Public Class dlgMethodAlertMngr

#Region "Variables"

    Public kpiRuleID As Integer = Nothing
    Public kpiRuleType As Integer = Nothing

#End Region

#Region "Events"

    Private Sub dlgMethodAlertMngr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadKPIRuleType()
            If kpiRuleType <> 0 Then
                SetComboBox(cmbMethod, ComboSelectBased.ValueBased, kpiRuleType)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub btnUpdateMethod_Click(sender As Object, e As EventArgs) Handles btnUpdateMethod.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbMethod.SelectedIndex > 0 Then
                UpdateKPIRuleType()
            Else
                SetMessage("Please Select Method")
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        Me.Cursor = Cursors.Default
        Application.DoEvents()
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

#Region "Methods"

    Private Sub UpdateKPIRuleType()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@KPI_RuleType", TryCast(cmbMethod.SelectedItem, clsComboBoxItem).Value},
            New String() {"@KPI_RULEID", kpiRuleID}
        }
        strConnection = GetSQL(3849, parray)(0)
        sqlParam = GetSQL(3849, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadKPIRuleType()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3819, parray)(0)
        sqlParam = GetSQL(3819, parray)(1)
        Dim dt As New DataTable()
        dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbMethod, dt, "KPI_RuleType", "KPI_RuleTypeName", "Select Method", True)
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

End Class