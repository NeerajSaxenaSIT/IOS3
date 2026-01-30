Public Class dlgCongestionRule

#Region "Variables/Properties"

    Private dtTechAndCounter As New DataTable
    Public capJobID As Integer = Nothing
    Public categoryID As Integer = Nothing

#End Region

#Region "Form Events"

    Private Sub dlgCongestionRule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadIOSTech()
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
            If txtCongestionRule.Text = "" Then
                SetMessage("Please enter Congestion Rule Name")
                Exit Sub
            ElseIf cmbIOSTech.SelectedIndex = 0 Then
                SetMessage("Please select technology")
                Exit Sub
            ElseIf cmbCounter.SelectedIndex = 0 Then
                SetMessage("Please select counter")
                Exit Sub
            Else
                AddCongestionRule()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vbtn_Cancel_Click(sender As Object, e As EventArgs) Handles vbtn_Cancel.Click
        newCapCongRuleName = Nothing
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmbIOSTech_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbIOSTech.SelectedIndex > 0 Then
                LoadCounterType(cmbIOSTech.SelectedItem.ToString)
            Else
                ClearComboBox(cmbCounter, "Select Counter Type")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

#Region "Private Methods"

    Private Sub LoadIOSTech()
        RemoveHandler cmbIOSTech.SelectedIndexChanged, AddressOf cmbIOSTech_SelectedIndexChanged

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3020, parray)(0)
        sqlParam = GetSQL(3020, parray)(1)

        dtTechAndCounter = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim dtIOSTech As DataTable = dtTechAndCounter.DefaultView.ToTable(True, "Tech")
        BindDevExComboBoxWithValueMember(cmbIOSTech, dtIOSTech, "Tech", "Tech", "Select Technology", True)

        AddHandler cmbIOSTech.SelectedIndexChanged, AddressOf cmbIOSTech_SelectedIndexChanged
    End Sub

    Private Sub LoadCounterType(ByVal iosTech As String)
        Dim dtCounter As DataTable = dtTechAndCounter.Select("Tech='" & iosTech & "'").CopyToDataTable.DefaultView.ToTable(True, "ObjectType")
        BindDevExComboBoxWithValueMember(cmbCounter, dtCounter, "ObjectType", "ObjectType", "Select Counter Type", True)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub AddCongestionRule()
        Try
            Dim dtRow() As DataRow = Nothing
            'If dtCongRule IsNot Nothing Then
            '    dtRow = dtCongRule.Select("CapCongestionRuleName = '" & txtCongestionRule.Text.Trim & "'")
            'End If
            'If (dtRow.Length > 0) Then
            '    SetMessage("Fail : Congestion Rule Name already exists.")
            '    txtCongestionRule.Focus()
            'Else
            newCapCongRuleName = txtCongestionRule.Text.Trim
            Dim parray()() As String = {
                New String() {"@capJobID", capJobID},
                New String() {"@capCongestionRuleName", Chr(39) & newCapCongRuleName & Chr(39)},
                New String() {"@capJobCategoryID", categoryID},
                New String() {"@counterType", Chr(39) & cmbCounter.SelectedItem.ToString & Chr(39)},
                New String() {"@iosTECH", Chr(39) & cmbIOSTech.SelectedItem.ToString & Chr(39)}
            }
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(3021, parray)(0), GetSQL(3021, parray)(1))
            SetMessage("Congestion Rule added successfully")
            Me.Hide()
            'End If
        Catch ex As Exception
            SetMessage("Error : Congestion Rule Insertion Fail")
        End Try
    End Sub

#End Region

#End Region

End Class