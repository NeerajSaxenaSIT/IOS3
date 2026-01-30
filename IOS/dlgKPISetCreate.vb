Imports IOS.DataLibrary

Public Class dlgKPISetCreate

#Region "Variables"

    Public kpiSetTech As String = Nothing

#End Region

#Region "Form Events"

    Private Sub dlgKPISetCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblIOSTech.Text = Me.kpiSetTech
            txtKPISetName.Text = String.Empty
            LoadCounterType()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
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

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtKPISetName.Text = String.Empty Then
                SetMessage("Enter KPI Set Name")
                Exit Sub
            ElseIf cmbCounterType.SelectedIndex = 0 Then
                SetMessage("Select Counter")
                Exit Sub
            End If
            AddKPISetName()
            objKPISetCreate.newKpiSetName = txtKPISetName.Text.Trim
            GetKPISetList(Me.kpiSetTech)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            SetMessage(ex.Message.ToString)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "Private Methods"

    Private Sub LoadCounterType()
        Dim parray()() As String = {
            New String() {"@IOSTECH", Chr(39) & Me.kpiSetTech & Chr(39)}
        }
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(GetSQL(7009, parray)(0), GetSQL(7009, parray)(1))
        BindDevExComboBoxWithValueMember(cmbCounterType, dt, "Object", "Object", "Select Counter")
    End Sub

    Private Sub AddKPISetName()
        Try
            Dim parray()() As String = {
                New String() {"@KPISetName", Chr(39) & txtKPISetName.Text.Trim & Chr(39)},
                New String() {"@IOSTech", Chr(39) & Me.kpiSetTech & Chr(39)},
                New String() {"@CounterType", Chr(39) & cmbCounterType.SelectedItem.ToString & Chr(39)},
                New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)}
            }
            DataAccessorODBC.ExecuteNonQuery(GetSQL(7002, parray)(0), GetSQL(7002, parray)(1))
        Catch ex As Exception
            SetMessage(ex.Message.ToString)
        End Try
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
