Imports IOS.Library
Imports IOS.DataLibrary

Public Class dlgAddNBIReport

    Private parray()() As String = Nothing
    Private strConnAndSqlParam() As String = Nothing
    Private dtTechAndCounter As New DataTable

    Public reportID As Integer = 0
    Public reportName As String = Nothing
    Public reportDesc As String = Nothing
    Public technology As String = Nothing
    Public objectType As String = Nothing

#Region "Form Events"

    Private Sub dlgAddNBIReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Text = "Add NBI Report"
            Me.btnAdd.Text = "Add"

            newNBIReportName = Nothing
            LoadIOSTech()

            Me.cmbIOSTech.Enabled = True
            Me.cmbCounter.Enabled = True

            If Me.reportID <> 0 Then
                Me.Text = "Modify NBI Report"
                Me.btnAdd.Text = "Modify"

                TryCast(cmbIOSTech.SelectedItem, clsComboBoxItem).Text = Me.technology
                LoadObjectType(Me.technology, False)
                cmbCounter.Text = Me.objectType
                txtReportName.Text = Me.reportName
                txtReportDescription.Text = Me.reportDesc

                Me.cmbIOSTech.Enabled = False
                Me.cmbCounter.Enabled = False
            End If

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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtReportName.Text = "" Then
                SetMessage("Please enter Report Name")
                Exit Sub
            ElseIf txtReportDescription.Text = "" Then
                SetMessage("Please enter Report Description")
                Exit Sub
            ElseIf cmbIOSTech.Text.Trim = "Select Technology" Then
                SetMessage("Please select technology")
                Exit Sub
            ElseIf cmbCounter.Text.Trim = "Select Object Type" Then
                SetMessage("Please select object type")
                Exit Sub
            Else
                Add_Modify_NBIReport()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        newNBIReportName = Nothing
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmbIOSTech_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbIOSTech.SelectedIndex > 0 Then
                LoadObjectType(cmbIOSTech.SelectedItem.ToString)
            Else
                ClearComboBox(cmbCounter, "Select Object Type")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Private Methods"

    Private Sub LoadIOSTech()
        RemoveHandler cmbIOSTech.SelectedIndexChanged, AddressOf cmbIOSTech_SelectedIndexChanged

        parray = Nothing
        strConnAndSqlParam = Nothing
        strConnAndSqlParam = GetSQL(3020, parray)

        dtTechAndCounter = DataAccessorODBC.GetDataTable(strConnAndSqlParam(0), strConnAndSqlParam(1))
        Dim dtIOSTech As DataTable = dtTechAndCounter.DefaultView.ToTable(True, "Tech")
        BindDevExComboBoxWithValueMember(cmbIOSTech, dtIOSTech, "Tech", "Tech", "Select Technology", True)

        AddHandler cmbIOSTech.SelectedIndexChanged, AddressOf cmbIOSTech_SelectedIndexChanged
    End Sub

    Private Sub LoadObjectType(ByVal iosTech As String, Optional isFirstItemSelected As Boolean = True)
        Dim dtCounter As DataTable = dtTechAndCounter.Select("Tech='" & iosTech & "'").CopyToDataTable.DefaultView.ToTable(True, "ObjectType")
        BindDevExComboBoxWithValueMember(cmbCounter, dtCounter, "ObjectType", "ObjectType", "Select Object Type", isFirstItemSelected)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Add_Modify_NBIReport()
        Try
            parray = Nothing
            strConnAndSqlParam = Nothing
            newNBIReportName = txtReportName.Text.Trim

            If Me.reportID = 0 Then
                parray = {
                    New String() {"@ReportName", Chr(39) & newNBIReportName & Chr(39)},
                    New String() {"@ReportDescription", Chr(39) & txtReportDescription.Text.Trim.Replace("'", "`") & Chr(39)},
                    New String() {"@iosTECH", Chr(39) & cmbIOSTech.SelectedItem.ToString & Chr(39)},
                    New String() {"@ObjectType", Chr(39) & cmbCounter.SelectedItem.ToString & Chr(39)},
                    New String() {"@ReportOwner", Chr(39) & Environment.UserName & Chr(39)}
                }
                strConnAndSqlParam = GetSQL(8523, parray)
                DataAccessorODBC.ExecuteNonQuery(strConnAndSqlParam(0), strConnAndSqlParam(1))
                SetMessage("NBI Report added successfully")
            Else
                parray = {
                    New String() {"@ReportName", Chr(39) & newNBIReportName & Chr(39)},
                    New String() {"@ReportDescription", Chr(39) & txtReportDescription.Text.Trim.Replace("'", "`") & Chr(39)},
                    New String() {"@ReportID", CInt(Me.reportID)}
                }
                strConnAndSqlParam = GetSQL(8526, parray)
                DataAccessorODBC.ExecuteNonQuery(strConnAndSqlParam(0), strConnAndSqlParam(1))
                SetMessage("NBI Report modified successfully")
            End If
            Me.Hide()
        Catch ex As Exception
            SetMessage("Error : NBI Report Addition/Modification Failed")
        End Try
    End Sub

#End Region

End Class