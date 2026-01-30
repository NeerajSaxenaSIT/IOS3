Imports IOS.DataLibrary

Public Class dlgAddXMlJob

#Region "Events"

    Private Sub dlgAddXMlJob_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            newXMLJob = Nothing
            LoadVendors()
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
            If cmbVendor.SelectedIndex = 0 Then
                SetMessage("Please Select Vendor")
                Exit Sub
            ElseIf txtXmlJobName.Text.Trim = String.Empty Then
                SetMessage("Please Enter XML Job Name")
                Exit Sub
            Else
                AddNewXMLJob()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "Private Methods"

    Private Sub LoadVendors()
        Dim dt As DataTable = clsSQLCommands.GetVendorsList(connStrIOSServer)
        BindDevExComboBoxWithValueMember(cmbVendor, dt, "IOS_Vendor", "IOS_Vendor", "Select Vendor", True)
    End Sub

    Private Sub AddNewXMLJob()
        newXMLJob = txtXmlJobName.Text.Trim
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobName", Chr(39) & newXMLJob & Chr(39)},
            New String() {"@XMLJobOwner", Chr(39) & Environment.UserName & Chr(39)},
            New String() {"@XMLJobType", Chr(39) & "RefCheck" & Chr(39)},
            New String() {"@XMLVendor", Chr(39) & cmbVendor.SelectedItem.ToString & Chr(39)},
            New String() {"@DeleteStatus", 0}
        }
        strConnection = GetSQL(6513, parray)(0)
        sqlParam = GetSQL(6513, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        SetMessage("New XML Job added successfully")
        Me.Close()
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