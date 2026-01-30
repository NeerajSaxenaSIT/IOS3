Imports IOS.DataLibrary

Public Class dlgEventManualUpdateStatus

    Public eventID As Integer = Nothing
    Public eventName As String = Nothing
    Public eventStatus As String = Nothing

#Region "Messages"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
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

#Region "Control Events"

    Private Sub dlgEventManualUpdateStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim sqlParam As String = Nothing
            Dim connstring As String = Nothing

            Dim parray()() As String = {
                New String() {"@EventID", Me.eventID}
            }
            sqlParam = GetSQL(8104, parray, dt_IOS_SQL)(1)
            connstring = GetSQL(8104, parray, dt_IOS_SQL)(0)
            Dim dtData As DataTable = DataAccessorODBC.GetDataTable(connstring, sqlParam)

            sqlParam = Nothing
            connstring = Nothing

            parray = {
                New String() {"@EventName", Chr(39) & Me.eventName & Chr(39)}
            }
            sqlParam = GetSQL(8105, parray, dt_IOS_SQL)(1)
            connstring = GetSQL(8105, parray, dt_IOS_SQL)(0)
            Dim dtStatus As DataTable = DataAccessorODBC.GetDataTable(connstring, sqlParam)

            If dtData.Rows.Count > 0 Then
                lblEventID.Text = dtData.Rows(0)("EventID")
                lblEventConfigID.Text = dtData.Rows(0)("EventConfigurationID")
                lblEventName.Text = dtData.Rows(0)("EventID")
                lblEventStatus.Text = dtData.Rows(0)("EventStatus")
            End If

            If dtStatus.Rows.Count > 0 Then
                BindDevExComboBoxWithValueMember(cmbEventStatus, dtStatus, "EventStatus", "EventStatus", "Select")
                SetComboBox(cmbEventStatus, ComboSelectBased.TextBased, eventStatus)
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If cmbEventStatus.SelectedIndex = 0 Then
                SetMessage("Please Select Event Status")
                Exit Sub
            ElseIf txtEventDesc.Text.Trim = String.Empty Then
                SetMessage("Please Select Event Description")
                Exit Sub
            End If

            Dim sqlParam As String = Nothing
            Dim connstring As String = Nothing

            Dim parray()() As String = {
                New String() {"@EventConfigurationID", lblEventConfigID.Text.Trim},
                New String() {"@EventLogMessage", Chr(39) & Environment.UserName & ": " & txtEventDesc.Text.Trim & Chr(39)},
                New String() {"@EventStatus", Chr(39) & cmbEventStatus.SelectedItem.ToString & Chr(39)},
                New String() {"@EventID", Me.eventID}
            }
            sqlParam = GetSQL(8106, parray, dt_IOS_SQL)(1)
            connstring = GetSQL(8106, parray, dt_IOS_SQL)(0)
            DataAccessorODBC.ExecuteNonQuery(connstring, sqlParam,, iQryTimeOut)

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

End Class