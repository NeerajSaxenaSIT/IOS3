Imports IOS.Library
Imports IOS.DataLibrary

Public Class dlgJobsAdd

#Region "Variables"

    Public SelectedJobID As Integer
    Public AddOrUpdate As String
    'Dim cn As Odbc.OdbcConnection
    'Dim sCommand As Odbc.OdbcCommand
    Dim sAdapter As Odbc.OdbcDataAdapter
    Dim sDs As DataSet
    Dim sTable As DataTable
    Public JobType As String = Nothing

#End Region

#Region "Form Events"

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim counter As Integer = 0
        ConfigurForm(Me, "dlgJobsAdd", counter)
        If AddOrUpdate = "Update" Then
            gcQueries.Enabled = True
            btnSave.Enabled = True
            LoadJobSelected()
        Else
            'clear
            txtJobProtectionLimit.Text = ""
            txtJobDescription.Text = ""
            txtTimeout.Text = ""
            txtJobName.Text = ""
            chkJobActive.Checked = False
            gcQueries.DataSource = Nothing
            gcQueries.Refresh()
            gcQueries.Enabled = False
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub frm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ''cn.Close()
        Catch
        End Try
    End Sub

#End Region

#Region "Helper"

    Private Sub LoadJobSelected()
        Try
            Dim jobid As Integer = CInt(SelectedJobID)
            Dim parray()() As String = {New String() {"@Nothing", ""}}
            Dim sql As String = "SELECT * FROM dbo.IOS_Jobs"

            Dim dr() As DataRow = DataAccessorODBC.GetDataTable(connStrIOSServer, sql).Select("JobID = " & jobid)

            If dr.Count > 0 Then
                txtJobName.Text = dr(0)(1).ToString.Trim
                txtJobDescription.Text = dr(0)(2).ToString.Trim
                Select Case dr(0)(4).ToString.Trim
                    Case "H"
                        rbHourly.Checked = True
                    Case "D"
                        rbDaily.Checked = True
                    Case "Q"
                        rb15Mins.Checked = True
                    Case "W"
                        rbWeekly.Checked = True
                    Case "M"
                        rbMonthly.Checked = True
                End Select
                dtpJob.EditValue = CDate(dr(0)(5).ToString)

                If dr(0)(6).ToString.ToUpper = "TRUE" Then
                    chkJobActive.Checked = True
                Else
                    chkJobActive.Checked = False
                End If
                txtTimeout.Text = dr(0)(7).ToString.Trim
            End If

            'Job Details
            ''sql = "SELECT JobDetailID, JobType, SequenceNumber, ConnectionString, SQLString, DestinationTable FROM dbo.IOS_Jobs_Details WHERE JobID = " & jobid & " ORDER BY SequenceNumber ASC"
            ''cn = New Odbc.OdbcConnection(connStrIOSServer)
            ''cn.Open()
            ''sDs = New DataSet

            ''sCommand = New Odbc.OdbcCommand(sql, cn)
            ''sAdapter = New Odbc.OdbcDataAdapter(sCommand)
            ''sAdapter.Fill(sDs, "JobDetails")

            sDs = New DataSet
            sDs = IOS.DataLibrary.clsSQLCommands.GetJobDetails(connStrIOSServer, jobid)

            gcQueries.DataSource = Nothing
            gcQueries.Refresh()
            gcQueries.DataSource = sDs
            gcQueries.DataMember = "JobDetails"
            gvQueries.Columns(0).Visible = False
            gcQueries.Refresh()
        Catch ex As Exception
        Finally
            ''If cn.State = ConnectionState.Open Then
            ''    cn.Close()
            ''End If
        End Try
    End Sub

#End Region

#Region "Control Events"

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cmdbuilder As New Odbc.OdbcCommandBuilder(sAdapter)
        Dim i As Integer
        Try
            i = sAdapter.Update(sDs, "JobDetails")
            MsgBox("Query Records Updated= " & i)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            btnSave.ForeColor = Color.Black
        End Try
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            'Validate
            Dim interval As String = ""
            If txtJobName.Text = "" Then
                Exit Sub
            End If
            If txtJobDescription.Text = "" Then
                Exit Sub
            End If
            If txtTimeout.Text = "" Then
                Exit Sub
            End If
            If dtpJob.EditValue < Now() Then
                'Exit Sub
            End If

            Try
                If CInt(txtJobProtectionLimit.Text) < 1 Then
                    MsgBox("Protection Limit Invalid")
                    Exit Sub
                End If
            Catch ex As Exception
                MsgBox("Protection Limit Invalid")
                Exit Sub
            End Try

            Select Case True
                Case rb15Mins.Checked
                    interval = "Q"
                Case rbHourly.Checked
                    interval = "H"
                Case rbDaily.Checked
                    interval = "D"
                Case rbWeekly.Checked
                    interval = "W"
                Case rbMonthly.Checked
                    interval = "M"
                Case Else
                    Exit Sub
            End Select

            Dim jobactive As Integer = 0
            If chkJobActive.Checked = True Then
                jobactive = 1
            Else
                jobactive = 0
            End If

            Try
                If AddOrUpdate = "Add" Then
                    'Inserting...
                    Dim startdate As Date = dtpJob.EditValue
                    IOS.DataLibrary.clsSQLCommands.AddJobs(connStrIOSServer, txtJobName.Text.Trim, txtJobDescription.Text.Trim, Environment.UserName.ToString, interval, startdate.ToString("yyyy-MM-dd HH:mm:ss"), jobactive, CInt(txtTimeout.Text), CInt(txtJobProtectionLimit.Text), JobType)
                Else
                    'Updating...
                    Dim startdate As Date = dtpJob.EditValue
                    IOS.DataLibrary.clsSQLCommands.UpdateJobs(connStrIOSServer, CInt(SelectedJobID), txtJobName.Text.Trim, txtJobDescription.Text.Trim, Environment.UserName.ToString, interval, startdate.ToString("yyyy-MM-dd HH:mm:ss"), jobactive, CInt(txtTimeout.Text), CInt(txtJobProtectionLimit.Text), JobType)
                    btnSave_Click(Nothing, Nothing)
                End If
            Catch ex As Exception
                MsgBox("Problem Saving Data...")
                Exit Sub
            End Try

            Me.DialogResult = DialogResult.OK
            Me.Close()
            objfrmSON.Jobs_Load_Inconsist()
            objfrmSON.Jobs_Load_Param()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class