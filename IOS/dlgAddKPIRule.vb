Public Class dlgAddKPIRule

#Region "Variables/Properties"

    Public capCongestionRuleID As Integer = Nothing
    Public iosTech As String = Nothing
    Public counterType As String = Nothing
    Private dtKPI As DataTable = Nothing

#End Region

#Region "Methods"

    Private Sub LoadKpiList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@iosTech", Chr(39) & lbliosTech.Text.Trim & Chr(39)},
            New String() {"@counterType", Chr(39) & lblCounter.Text.Trim & Chr(39)}
        }
        strConnection = GetSQL(3010, parray)(0)
        sqlParam = GetSQL(3010, parray)(1)

        dtKPI = New DataTable()
        dtKPI = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        lstviewKPI.DataSource = Nothing
        lstviewKPI.Columns.Clear()
        lstviewKPI.DataSource = dtKPI
        lstviewKPI.BestFitColumns()
        lstviewKPI.Columns(0).Visible = False
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

    Private Sub dlgAddKPIRule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lbliosTech.Text = iosTech
            lblCounter.Text = counterType
            LoadKpiList()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearckKPI_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearckKPI.KeyUp
        Try
            If dtKPI IsNot Nothing Then
                If (txtSearckKPI.Text.Length > 0) Then
                    dtKPI.DefaultView.RowFilter = "KPI_Name Like '%" + txtSearckKPI.Text + "%'"
                Else
                    dtKPI.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If cmbOperator.SelectedIndex = 0 Then
                SetMessage("Please select operator")
                Exit Sub
            End If

            If txtValue.Text = "" Then
                SetMessage("Please enter numeric treshold value")
                Exit Sub
            End If

            Dim nd As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewKPI.FocusedNode
            Dim dataKpi As DataRowView = lstviewKPI.GetDataRecordByNode(nd)
            If dataKpi IsNot Nothing Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@capCongestionRuleID", Me.capCongestionRuleID},
                    New String() {"@sqlKpiID", dataKpi("SQLKPI_ID")},
                    New String() {"@operator", Chr(39) & cmbOperator.SelectedItem.ToString & Chr(39)},
                    New String() {"@tresholdValue", txtValue.Text.Trim}
                }
                strConnection = GetSQL(3015, parray)(0)
                sqlParam = GetSQL(3015, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                Me.Close()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValue.KeyPress
        If (Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> ".")) Then
            e.Handled = True
        End If

        ' only allow one decimal point
        If ((e.KeyChar = ".") AndAlso (TryCast(sender, DevExpress.XtraEditors.TextEdit).Text.IndexOf(".") > -1)) Then
            e.Handled = True
        End If
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