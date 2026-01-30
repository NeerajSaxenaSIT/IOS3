Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgNewReport

    Public connString As String = Nothing
    Public newReportName As String = Nothing
    Public newReportGroupID As String = Nothing

    Private Sub btnAddReport_Click(sender As Object, e As EventArgs) Handles btnAddReport.Click
        If cmbReportGroup.SelectedIndex = 0 Then
            SetMessage("Please select report group")
            Exit Sub
        End If

        If txtReportName.Text = String.Empty Then
            SetMessage("Please enter report name")
            Exit Sub
        End If

        newReportGroupID = TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value
        newReportName = txtReportName.Text.Trim()
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

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub BindReportGroup()
        Dim dtReportGroup As DataTable = DataAccessorODBC.GetDataTable(connString, SQLReportGroups.GetReportGroups(Environment.UserName.ToString))
        If (dtReportGroup.IsValid) Then
            BindDevExComboBoxWithTagMember(cmbReportGroup, dtReportGroup, ReportGroupsFields.REPORT_GROUP_ID, ReportGroupsFields.REPORT_GROUP_NAME, "None", ReportGroupsFields.LICENSE_USER)
        Else
            ClearComboBox(cmbReportGroup, "None")
        End If
    End Sub

    Private Sub dlgNewReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            BindReportGroup()
        Catch ex As Exception

        End Try
    End Sub
End Class