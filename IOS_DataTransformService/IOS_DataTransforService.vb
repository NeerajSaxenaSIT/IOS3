Imports System.Configuration
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Net.Mail
Imports System.Security.AccessControl
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Oracle.ManagedDataAccess.Client
Imports Renci.SshNet
Imports Renci.SshNet.Sftp
Imports RestSharp

Module IOS_DataTransforService

    Dim conn_ios As String
    Dim conn_ios_SQL As String
    Dim JobID As Integer
    Dim HasSequenceFailed As Boolean = False
    Dim nbiMaxDeviation As Integer = Nothing
    Dim NBIReport_HyperLink As String = ""
    Dim NBIRootFolder As String = ""
    Dim NBIOutputFolder As String = ""

    Public Sub Main()
        Console.WriteLine("start")
        Try

            ' conn_ios_sql = "Data Source=PROQVP-01;Initial Catalog=IOS_Server;Persist Security Info=True;User ID=IOS_JobAgent; Password=IOS_JobAgent"
            'conn_ios_sql = "Data Source=D:\CellSens\Projects\IOS_TMNL\IOS_DataTrftransferansformService\bin\Debug\IOS_JobProcefssing.sdf"
            conn_ios_SQL = My.Settings.IOS_JobProcessingConnectionString.ToString

            'Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            'Dim protectedSection As ConfigurationSection = config.ConnectionStrings
            'config.Save(ConfigurationSaveMode.Full, True)nn
            Dim fp2 As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
            'Console.WriteLine("Number Of Arguments: " & My.Application.CommandLineArgs.Count)

            '        Try
            ' Console.WriteLine("Arguments: " & My.Application.CommandLineArgs(0))
            ' Catch ex As Exception
            ' Console.WriteLine("Arguments Error: " & ex.Message)
            ' Console.ReadKey(True)
            ' Exit Sub
            ' End Try

            'Dim settings As ConnectionStringSetting
            'settings = ConfigurationManager.ConnectionStrings("IOSServer")

            conn_ios = My.Settings.IOS_JobProcessingConnectionStringODBC.ToString

            Dim conn_ios_agent As String = conn_ios

            'get the JobID to launch from commandline

            Dim Args As String = My.Application.CommandLineArgs(0)

            If Args <> "NBI" Then

                If Args = "MANUAL" Then

                    Dim sql_job_manual As String = "Select * From [dbo].[IOS_Jobs] Where [RunManual] = 1 And [JobActive] = 1;"
                    Dim dt_job_manual As DataTable = GetTableFromODBC(conn_ios, sql_job_manual)

                    If dt_job_manual IsNot Nothing AndAlso dt_job_manual.Rows.Count > 0 Then
                        For Each dr_job_manual As DataRow In dt_job_manual.Rows
                            RunJob(CInt(dr_job_manual("JobID")), conn_ios_agent)
                        Next
                    End If

                Else

                    JobID = CInt(My.Application.CommandLineArgs(0))
                    RunJob(JobID, conn_ios_agent)

                End If

                'NBI PART
            Else

                Dim Args2 As String = ""
                Dim ReportRunType As String = ""

                Try
                    Args2 = My.Application.CommandLineArgs(1)
                Catch ex As Exception

                End Try

                Dim sql_nbi_Custom As String = ""

                If Args2 = "CUSTOM" Then
                    'load a manual table to generate files
                    sql_nbi_Custom = "select * from IOS_NBI_Reports_Custom"
                    ReportRunType = "CUSTOM"

                    Dim dt_nbi_custom As DataTable = GetTableFromODBC(conn_ios, sql_nbi_Custom)
                    If dt_nbi_custom.Rows.Count > 0 Then
                        Console.WriteLine("NBI Scheduled Reports Found: " + dt_nbi_custom.Rows.Count.ToString)
                    Else
                        Console.WriteLine("No NBI Scheduled Reports Found !")
                    End If

                    Dim NBIRootFolder2 As String = System.Configuration.ConfigurationManager.AppSettings("NBIRootFolder").ToString
                    Dim NBIQueryTimeOut2 As Int32 = System.Configuration.ConfigurationManager.AppSettings("NBI_QueryTimeOut").ToString


                    For Each dr_nbi_custom As DataRow In dt_nbi_custom.Rows

                        Dim ExportPath As String = NBIRootFolder2.TrimEnd("\") + "\" + dr_nbi_custom("OutputFolder")
                        Dim uniqueJob As String = Now.ToString("yyyyMMdd") & Now.ToString("HHmmss")
                        Dim ExportFile As String = dr_nbi_custom("ExportFile")
                        Dim EmailEnabled As String = False
                        Dim Recipient As String = ""
                        Dim EmailLinkNBI As Boolean = False
                        Dim OutputDelimiter As String = ConfigurationManager.AppSettings("OutputDelimiter").ToString
                        Dim ReportRunID As String = dr_nbi_custom("CustomReportID").ToString & "_" & Now().ToString("yyyyMMddHHmmss")
                        Dim nbiAttempts As Integer = Nothing
                        Dim sqlToFire As String = dr_nbi_custom("SQL").ToString
                        Dim success As Boolean = False
                        Dim ExpectedRowCount As Int32 = Nothing

                        If ConfigurationManager.AppSettings("NBI_NoStream").ToString = 0 Then
                            success = TransferDataToNBICSV(dr_nbi_custom("CustomReportID").ToString, conn_ios_SQL, dr_nbi_custom("SQL").ToString, ExportPath, ExportFile, NBIQueryTimeOut2, Recipient, EmailLinkNBI, OutputDelimiter, EmailEnabled, ReportRunID, 1, sqlToFire)
                        Else
                            success = TransferDataToNBICSV_NoStream(dr_nbi_custom("CustomReportID").ToString, conn_ios_SQL, dr_nbi_custom("SQL").ToString, ExportPath, ExportFile, NBIQueryTimeOut2, Recipient, EmailLinkNBI, OutputDelimiter, EmailEnabled, ReportRunID, 1, sqlToFire)
                        End If

                    Next

                End If

                ' collect all queries to be fired
                Console.WriteLine("Querying NBI Reports Table ... ")

                Dim sql_nbi As String = ""

                If Args2 = "" Then
                    sql_nbi = "select * from IOS_NBI_Reports  where IsEnabled = 1 and isScheduled = 1 and  GetDate() > ScheduleStartTime "   'And ReportID = 98;
                    ReportRunType = "SCHEDULED"
                ElseIf IsNumeric(Args2) Then
                    sql_nbi = "select * from IOS_NBI_Reports  where ReportId = " & Args2
                    ReportRunType = "ADHOC"
                Else
                    Console.WriteLine("Wrong input argument !")
                End If

                Dim nbiAttemptsConfig As Integer = CInt(ConfigurationManager.AppSettings("NBI_Attempts").ToString)
                nbiMaxDeviation = CInt(ConfigurationManager.AppSettings("NBI_MaxRecordDeviationPercentage").ToString)

                Dim dt_nbi As DataTable = GetTableFromODBC(conn_ios, sql_nbi)
                If dt_nbi.Rows.Count > 0 Then
                    Console.WriteLine("NBI Scheduled Reports Found: " + dt_nbi.Rows.Count.ToString)
                Else
                    Console.WriteLine("No NBI Scheduled Reports Found !")
                End If

                NBIRootFolder = System.Configuration.ConfigurationManager.AppSettings("NBIRootFolder").ToString
                Dim NBIQueryTimeOut As Int32 = System.Configuration.ConfigurationManager.AppSettings("NBI_QueryTimeOut").ToString


                For Each dr_nbi As DataRow In dt_nbi.Rows
                    Try
                        WriteString_Log(Now() & "    " & "NBI Scheduled ReportID: " & dr_nbi("ReportID").ToString)
                        ReportLog(conn_ios, "IOS DTE - NBI Scheduled ReportID: " & dr_nbi("ReportID").ToString, dr_nbi("ReportID").ToString, "STARTED")

                        ' Launching sp_GenerarteNBI without NBI (sp_NBI_GenerateReport)

                        Dim sSQL As String = "EXECUTE IOS_Server.dbo.sp_NBI_GenerateReport " & dr_nbi("ReportID").ToString & "," & IIf(Args2 <> "", 1, 0) & ",0"
                        NBIOutputFolder = dr_nbi("OutputFolder")
                        Dim ExportPath As String = NBIRootFolder.TrimEnd("\") + "\" + dr_nbi("OutputFolder") + "\"
                        Dim uniqueJob As String = Now.ToString("yyyyMMdd") & Now.ToString("HHmmss")
                        Dim ExportFile As String = dr_nbi("ReportName") & "_" & uniqueJob + "." + dr_nbi("OutputFormat").ToString
                        Dim EmailEnabled As String = nZ(dr_nbi("EmailEnabled"), "False").ToString
                        Dim Recipient As String = ""
                        Dim EmailLinkNBI As Boolean = False
                        Dim OutputDelimiter As String = nZ(dr_nbi("OutputDelimiter"), ";")
                        Dim ReportRunID As String = dr_nbi("ReportID").ToString & "_" & Now().ToString("yyyyMMddHHmmss")
                        Dim nbiAttempts As Integer = Nothing
                        Dim sqlToFire As String = Nothing
                        Dim success As Boolean = False
                        Dim ExpectedRowCount As Int32 = Nothing
                        Dim OutputFileTimeStamp As Int16 = 1
                        Try
                            OutputFileTimeStamp = CInt(dr_nbi("OutputFileTimeStamp").ToString)
                        Catch ex As Exception

                        End Try

                        If OutputFileTimeStamp = 0 Then
                            ExportFile = dr_nbi("ReportName") + "." + dr_nbi("OutputFormat").ToString
                        End If

                        Try
                            nbiMaxDeviation = CInt(dr_nbi("ResultCount_MaxDeviationPerc"))
                            nbiAttemptsConfig = CInt(dr_nbi("QueryAttempts"))
                            NBIQueryTimeOut = CInt(dr_nbi("QueryTimeOut"))
                        Catch ex As Exception
                            WriteString_Log(Now() & "    " & "NBI Query Configuraiton: " & dr_nbi("ReportID").ToString & " - Error: " & ex.Message)

                        End Try

                        'checking report failed status
                        Dim sql_ReportStatus As String = "Select * From [IOS_Server].[dbo].[IOS_NBI_Reports_Status] Where [ReportID] = " & CInt(dr_nbi("ReportID").ToString) & " And [ReportStatus] = 'FAILED' And [Attempts] < " & nbiAttemptsConfig
                        Dim dt_nbiStatus As DataTable = GetTableFromODBC(conn_ios, sql_ReportStatus)


                        Try


                            If dt_nbiStatus IsNot Nothing Then
                                If dt_nbiStatus.Rows.Count > 0 Then

                                    For j = 0 To dt_nbiStatus.Rows.Count - 1

                                        Dim ReportRunID_ADHoc As String = dt_nbiStatus.Rows(j)("ReportsRunID").ToString
                                        Dim ExportFile_ADHoc As String = dt_nbiStatus.Rows(j)("FileName").ToString
                                        Dim sqlToFire_ADHoc As String = dt_nbiStatus.Rows(j)("SQLQueryFired").ToString
                                        Dim nbiAttempts_ADHoc As String = dt_nbiStatus.Rows(j)("Attempts").ToString

                                        ReportLog(conn_ios, "IOS DTE - Retrying: " & ReportRunID_ADHoc & " Attempt: " & nbiAttempts_ADHoc, dr_nbi("ReportID").ToString, "INFO")

                                        If ConfigurationManager.AppSettings("NBI_NoStream").ToString = 0 Then
                                            success = TransferDataToNBICSV(dr_nbi("ReportID"), conn_ios_SQL, sSQL, ExportPath, ExportFile_ADHoc, NBIQueryTimeOut, Recipient, EmailLinkNBI, OutputDelimiter, EmailEnabled, ReportRunID_ADHoc, nbiAttempts_ADHoc, sqlToFire_ADHoc)
                                        Else
                                            success = TransferDataToNBICSV_NoStream(dr_nbi("ReportID"), conn_ios_SQL, sSQL, ExportPath, ExportFile_ADHoc, NBIQueryTimeOut, Recipient, EmailLinkNBI, OutputDelimiter, EmailEnabled, ReportRunID_ADHoc, nbiAttempts_ADHoc, sqlToFire_ADHoc)
                                        End If
                                    Next

                                End If
                            End If

                        Catch ex As Exception
                            WriteString_Log(Now() & "    " & " Retry Error: " & dr_nbi("ReportID").ToString & " - Error: " & ex.Message)
                        End Try

                        InsertNBIReportStatus(conn_ios, ReportRunID, CInt(dr_nbi("ReportID").ToString), ReportRunType, ExportFile)

                        If EmailEnabled = "True" Then
                            Recipient = nZ(dr_nbi("EmailAddresses"), "").ToString
                            EmailLinkNBI = nZ(dr_nbi("EmailLinkNBI"), False)
                            WriteString_Log(Now() & "    Email Report to:" & Recipient)
                            Console.WriteLine("Email Result to:" & Recipient)
                        End If

                        If sSQL IsNot Nothing Then
                            If ConfigurationManager.AppSettings("NBI_NoStream").ToString = 0 Then
                                success = TransferDataToNBICSV(dr_nbi("ReportID"), conn_ios_SQL, sSQL, ExportPath, ExportFile, NBIQueryTimeOut, Recipient, EmailLinkNBI, OutputDelimiter, EmailEnabled, ReportRunID, nbiAttempts, sqlToFire)
                            Else
                                success = TransferDataToNBICSV_NoStream(dr_nbi("ReportID"), conn_ios_SQL, sSQL, ExportPath, ExportFile, NBIQueryTimeOut, Recipient, EmailLinkNBI, OutputDelimiter, EmailEnabled, ReportRunID, nbiAttempts, sqlToFire)
                            End If
                        Else
                            ReportLog(conn_ios, "IOS DTE - Report Not Fired - No SQL from sp_NBI_GenerateReport", dr_nbi("ReportID").ToString, "INFO")
                        End If


                    Catch ex As Exception
                        WriteString_Log(Now() & "    Report Failed - " & dr_nbi("ReportID").ToString & ex.Message)
                        ReportLog(conn_ios, "IOS DTE - Report Failed - " & ex.Message, dr_nbi("ReportID").ToString, "ERROR")
                        Console.WriteLine(ex.Message)
                    End Try

                Next

            End If

        Catch ex As Exception
            JobLog(conn_ios, "Job Failed - " & ex.Message, JobID)
            Console.WriteLine(ex.Message)
        End Try


    End Sub

    Private Sub RunJob(jobId As Integer, conn_ios_agent As String)
        Try

            Console.WriteLine("Querying Job Table ... ")
            Console.WriteLine("ConnectionString:  " & conn_ios)

            Dim sql_job As String = "SELECT * FROM IOS_Jobs where JobID = " & jobId

            Console.WriteLine("Query:  " & sql_job)
            Dim dt_job As DataTable = GetTableFromODBC(conn_ios, sql_job)
            If dt_job.Rows.Count > 0 Then
                Console.WriteLine("Job Found !")
            Else
                Console.WriteLine("Job Not Found !")
            End If

            'get the configuration of the job
            Console.WriteLine("Querying Job Table ... ")

            Dim sql_config As String = "SELECT * FROM IOS_Jobs_Details where JobID = " & jobId & " order by SequenceNumber ASC"
            Dim dt_config As DataTable = GetTableFromODBC(conn_ios, sql_config)

            If dt_config.Rows.Count > 0 Then
                Console.WriteLine("Job Sequences Found " & dt_config.Rows.Count)
            Else
                Console.WriteLine("Job Sequences Not Found !  => EXIT")
            End If

            'loading variables of job
            Dim sql_variables As String = "SELECT * FROM IOS_Jobs_Variables where JobID = " & jobId
            Dim dt_variables As DataTable = GetTableFromODBC(conn_ios, sql_variables)
            If dt_variables.Rows.Count > 0 Then
                Console.WriteLine("Job Variables Found " & dt_variables.Rows.Count)
            Else
                Console.WriteLine("Job Variables Not Present")
            End If

            If dt_config Is Nothing Then
                End
            End If

            'execute set of queries
            '1. load the temp queries
            JobLog(conn_ios, "Job Started", jobId)
            WriteString_Log(Now() & "    " & "Job Started: " & jobId)
            Dim uniqueJob As String = jobId & "_" & Now.ToString("yyyyMMdd") & Now.ToString("HHmmss")
            'uniqueJob = "1044_20141108071135"

            For i = 0 To dt_config.Rows.Count - 1
                ' Dim dr As DataRow = dt_config.Rows(6)
                Dim dr As DataRow = dt_config.Rows(i)
                Dim sql_from_job As String = dr("SQLString").ToString

                Dim success As Boolean = True
                Dim successAsync As Threading.Tasks.Task(Of Boolean)

                Console.WriteLine("Job Started - Sequence: " & dr("SequenceNumber").ToString)

                WriteString_Log(Now() & "    " & "UniqueJob: " & uniqueJob)

                'applying variables
                If Not dt_variables Is Nothing Then
                    For Each vRow As DataRow In dt_variables.Rows
                        sql_from_job = Replace(sql_from_job, vRow("VariableName").ToString, vRow("VariableValue").ToString)
                    Next
                End If

                JobLog(conn_ios, "Job Sequence Start: " & dr("SequenceNumber").ToString, jobId)
                WriteString_Log("---------------         START SEQUENCE       ----------------------------")
                WriteString_Log(Now() & "    " & "SequenceNumber:  " & dr("SequenceNumber").ToString.Trim)
                WriteString_Log(Now() & "    " & "Type:  " & dr("JobType").ToString.Trim)
                WriteString_Log(Now() & "    " & "SourceConnString:  " & dr("ConnectionString"))
                WriteString_Log(Now() & "    " & "SQL After Variables:  " & sql_from_job)
                WriteString_Log(Now() & "    " & "DestinationConnString:  " & conn_ios_SQL)
                WriteString_Log(Now() & "    " & "DestinationTable:  " & dr("DestinationTable"))
                WriteString_Log(Now() & "    " & "QueryTimeOut:  " & dr("QueryTimeOut"))


                If dr("JobType").ToString.Trim = "Load" And success = True Then
                    success = TransferData(dr("ConnectionString"), sql_from_job, conn_ios_SQL, dr("DestinationTable"), dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Trim = "LoadSQL" And success = True Then
                    success = TransferData_FromSQLclient(dr("ConnectionString"), sql_from_job, conn_ios_SQL, dr("DestinationTable"), dr("QueryTimeOut"))
                End If

                ' If dr("JobType").ToString.Trim = "Execute" And success = True Then
                'success = ExecuteFinalQuery(conn_ios_sql, sql_from_job, dr("QueryTimeOut"))
                'End If

                If dr("JobType").ToString.Trim = "Execute" And success = True Then
                    success = ExecuteRemoteSP(dr("ConnectionString"), sql_from_job, dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Trim = "Transfer" Then
                    success = TransferDataToExistingTable(dr("ConnectionString"), sql_from_job, dr("DestinationConnString"), dr("DestinationTable"), dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Trim = "ExportCSV" Then
                    success = TransferDataToCSV(dr("ConnectionString"), sql_from_job, dr("DestinationConnString"), dr("DestinationTable"), dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Trim = "RESTAPI" Then

                    successAsync = TransferDataFromRestAPI(dr("ConnectionString"), sql_from_job, dr("DestinationConnString"), dr("DestinationTable"), dr("QueryTimeOut"))

                    While successAsync.IsCompleted = False
                        Threading.Thread.Sleep(1000)
                    End While

                End If


                If dr("JobType").ToString.Trim = "CSVandMail" Then
                    success = ExportCSVandMail(dr("ConnectionString"), sql_from_job, dr("DestinationConnString"), dr("DestinationTable"), dr("QueryTimeOut"))
                End If


                If dr("JobType").ToString.Trim = "TransferOracle" Then
                    success = TransferDataToExistingTable_Oracle(dr("ConnectionString"), sql_from_job, dr("DestinationConnString"), dr("DestinationTable"), dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Trim = "TransferOracleDataTable" Then
                    success = TransferDataToExistingTableDataTable_Oracle(dr("ConnectionString"), sql_from_job, dr("DestinationConnString"), dr("DestinationTable"), dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Trim = "TransferDataTable" Then
                    success = TransferDataToExistingTableDataTable(dr("ConnectionString"), sql_from_job, dr("DestinationConnString"), dr("DestinationTable"))
                End If

                If dr("JobType").ToString.Trim = "Filter" And success = True Then
                    success = TransferData(conn_ios_agent, sql_from_job, conn_ios_SQL, dr("DestinationTable"), dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Trim = "Final" And success = True Then
                    'adding unique identifier
                    success = ExecuteFinalQuery(conn_ios_SQL, Replace(sql_from_job, "@uniquejob", Chr(39) & uniqueJob & Chr(39)), dr("QueryTimeOut"))
                End If

                If dr("JobType").ToString.Contains("XML") And success = True Then
                    Dim dt As DataTable = GetTableFromODBC(conn_ios_agent, Replace(sql_from_job, "@uniquejob", Chr(39) & uniqueJob & Chr(39)))
                    Dim fp As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
                    Dim jobtype As String = dr("JobType").ToString.TrimEnd

                    If dt Is Nothing Then
                        success = False
                    ElseIf dt.Rows.Count = 0 Then
                        success = True
                    Else
                        Select Case jobtype
                            Case "XML_HUAWEI"
                                success = XML_Parameters_HUAWEI(Replace(fp, "file:/", ""), dt)
                            Case "XML_NSN"
                                success = XML_Parameters_NSN(Replace(fp, "file:/", ""), dt)
                        End Select

                    End If

                End If

                If dr("JobType").ToString.Contains("MML") And success = True Then
                    Dim sqlnew As String = Replace("SELECT DISTINCT res.*, Managed_Object, MML_commands from IOS_Oss_Param_Ref inner join (" + Replace(sql_from_job, "ORDER BY DN ASC", "") + ") res on IOS_Oss_Param_Ref.P_abbr_name = res.ParameterName inner join IOS_Tune_Parameter on ID = ParameterID where MML_Commands is not null order by IOS_Oss_Param_Ref.Managed_Object, res.PARENT, res.GID, res.ParameterName", "@uniquejob", Chr(39) & uniqueJob & Chr(39))
                    Dim dt As DataTable = GetTableFromODBC(conn_ios_agent, sqlnew)
                    Dim fp As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

                    If dt Is Nothing Then
                        success = False
                    ElseIf dt.Rows.Count = 0 Then
                        success = True
                    Else
                        Select Case Split(dr("JobType").ToString, "_")(1).ToUpper
                            Case "HUAWEI"
                                success = MML_Parameters_Huawei(Replace(fp, "file:/", ""), dt)
                        End Select

                    End If
                End If

                If dr("JobType").ToString.ToUpper = "OOKLA" Then
                    success = TransferDataFromRestAPI_OOKLA()
                End If

                If Not successAsync Is Nothing Then

                    If successAsync.Result = True Then
                        JobLog(conn_ios, "Sequence Finished:  " & dr("SequenceNumber").ToString & " - Succes", jobId)
                        WriteString_Log("---------------         END SEQUENCE - SUCCESS      ----------------------------")
                    Else
                        HasSequenceFailed = True
                        JobLog(conn_ios, "Sequence Finished:  " & dr("SequenceNumber").ToString & " - Failed", jobId)
                        WriteString_Log("---------------         END SEQUENCE - FAIL      ----------------------------")
                    End If

                Else

                    If success = True Then
                        JobLog(conn_ios, "Sequence Finished:  " & dr("SequenceNumber").ToString & " - Succes", jobId)
                        WriteString_Log("---------------         END SEQUENCE - SUCCESS      ----------------------------")
                    Else
                        HasSequenceFailed = True
                        JobLog(conn_ios, "Sequence Finished:  " & dr("SequenceNumber").ToString & " - Failed", jobId)
                        WriteString_Log("---------------         END SEQUENCE - FAIL      ----------------------------")
                    End If
                End If

            Next
            If HasSequenceFailed = False Then
                JobLog(conn_ios, "Job Success", jobId)
            Else
                JobLog(conn_ios, "Job Failed", jobId)
            End If

        Catch ex As Exception
            JobLog(conn_ios, "Job Failed - " & ex.Message, jobId)
            Console.WriteLine(ex.Message)
        Finally
            'update jobid run manual status after completion
            UpdateJobIDRunManualStatus(conn_ios_agent, jobId)
        End Try

    End Sub

    Private Sub UpdateJobIDRunManualStatus(connString As String, jobId As Integer)
        Try
            Using sourceConnection As OdbcConnection = New OdbcConnection(connString)
                sourceConnection.Open()

                Dim update_cmd As OdbcCommand = New OdbcCommand("Update [dbo].[IOS_Jobs] Set [RunManual] = ? Where [JobID] = ?;", sourceConnection)

                Dim p1 As OdbcParameter = New OdbcParameter("@RunManual", 0)
                Dim p2 As OdbcParameter = New OdbcParameter("@JobID", jobId)

                update_cmd.Parameters.Add(p1)
                update_cmd.Parameters.Add(p2)

                Try
                    Dim i As Integer = update_cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                sourceConnection.Close()
            End Using
        Catch ex As Exception
            WriteString_Log(Now() & "    " & "JobLog Write Error:  " & ex.Message)
        End Try
    End Sub

    Public Function GetTableFromODBC(ByVal connstring As String, ByVal sql As String) As DataTable

        If sql = "" Or connstring = "" Then
            Return Nothing
        End If

        Dim cnOSS As System.Data.Odbc.OdbcConnection = Nothing
        Dim daOSS As System.Data.Odbc.OdbcDataAdapter = Nothing
        Dim dsOSS As System.Data.DataSet = Nothing
        Dim dtOSS As New DataTable()

        Try


            cnOSS = New System.Data.Odbc.OdbcConnection(connstring)
            cnOSS.ConnectionTimeout = 60
            cnOSS.Open()
            daOSS = New System.Data.Odbc.OdbcDataAdapter(sql, cnOSS)
            dsOSS = New System.Data.DataSet
            daOSS.SelectCommand.CommandTimeout = 60
            daOSS.Fill(dsOSS)
            cnOSS.Close()
            dtOSS = dsOSS.Tables(0)

            cnOSS.Dispose()
            daOSS.Dispose()

            Return dtOSS
        Catch ex As Exception
            If Not daOSS Is Nothing Then
                daOSS.Dispose()
            End If
            If Not dsOSS Is Nothing Then
                dsOSS.Dispose()
            End If
            If Not dtOSS Is Nothing Then
                dtOSS.Dispose()
            End If

            If Not cnOSS Is Nothing Then
                cnOSS.Close()
                cnOSS.Dispose()
            End If

            Console.WriteLine("Problem getting data from server using: " & connstring & Chr(13) & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Public Sub InsertNBIReportStatus(ByVal Connstring As String, ReportRunID As String, ByVal ReportID As Integer, ReportRunType As String, FileName As String)
        Try
            Using sourceConnection As OdbcConnection = New OdbcConnection(Connstring)
                sourceConnection.Open()

                Dim strSql As String = "INSERT INTO [IOS_Server].[dbo].[IOS_NBI_Reports_Status] ([ReportsRunID],[ReportID],[ReportTimeStamp_Start],[ReportRunType],[ReportStatus],[Attempts],[FileName])
                                        VALUES(?,?,GETDATE(),?,?,?,?)"

                Dim insrt_cmd As OdbcCommand = New OdbcCommand(strSql, sourceConnection)

                Dim p0 As OdbcParameter = New OdbcParameter("@ReportsRunID", ReportRunID)
                Dim p1 As OdbcParameter = New OdbcParameter("@ReportID", ReportID)
                Dim p2 As OdbcParameter = New OdbcParameter("@ReportRunType", ReportRunType)
                Dim p3 As OdbcParameter = New OdbcParameter("@ReportStatus", "STARTED")
                Dim p4 As OdbcParameter = New OdbcParameter("@Attempts", 0)
                Dim p5 As OdbcParameter = New OdbcParameter("@FileName", FileName)

                insrt_cmd.Parameters.Add(p0)
                insrt_cmd.Parameters.Add(p1)
                insrt_cmd.Parameters.Add(p2)
                insrt_cmd.Parameters.Add(p3)
                insrt_cmd.Parameters.Add(p4)
                insrt_cmd.Parameters.Add(p5)

                Try
                    Dim j As Integer = insrt_cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                sourceConnection.Close()
            End Using
        Catch ex As Exception
            WriteString_Log(Now() & "    " & "JobLog Write Error:  " & ex.Message)
        End Try
    End Sub

    Public Function Report_ExpectedCount(reportid) As Int32
        Dim ExpectedRowCount As Int32 = 0
        Dim SampleCount As Int32 = 0

        Dim sql_ReportStatus2 As String = " select coalesce(avg([NumberOfRecords]),0) AvgOfRecords, count(*) CountOfRecords from ( select top 10 [NumberOfRecords]  from [IOS_NBI_Reports_Status] where reportid = " & reportid.ToString & " and reportstatus = 'SUCCESS' order by [ReportTimeStamp_Start] desc) x"
        Dim dt_nbiStatus2 As DataTable = GetTableFromODBC(conn_ios, sql_ReportStatus2)
        If dt_nbiStatus2 IsNot Nothing AndAlso dt_nbiStatus2.Rows.Count > 0 Then
            ExpectedRowCount = dt_nbiStatus2(0)(0).ToString
            SampleCount = dt_nbiStatus2(0)(1).ToString
        End If
        If SampleCount = 10 Then
            Return (ExpectedRowCount - (ExpectedRowCount * nbiMaxDeviation / 100))
        Else
            Return 0
        End If

    End Function

    Public Sub UpdateNBIReportStatus(ByVal Connstring As String, ByVal ReportRunID As String, ElapsedTime As String, ReportStatus As Boolean, NoOfRecords As Integer, Attempts As Integer, SQLQueryFired As String)
        Try


            Dim ExpectedRowCount As Int32 = 0
            Dim SampleCount As Int32 = 0
            If ReportStatus = "True" Then
                Dim sql_ReportStatus2 As String = " select coalesce(avg([NumberOfRecords]),0) AvgOfRecords, count(*) CountOfRecords from ( select top 10 [NumberOfRecords]  from [IOS_NBI_Reports_Status] where reportid = " & ReportRunID.Split("_")(0) & " and reportstatus = 'SUCCESS' order by [ReportTimeStamp_Start] desc) x"
                Dim dt_nbiStatus2 As DataTable = GetTableFromODBC(conn_ios, sql_ReportStatus2)
                If dt_nbiStatus2 IsNot Nothing AndAlso dt_nbiStatus2.Rows.Count > 0 Then
                    ExpectedRowCount = dt_nbiStatus2(0)(0).ToString
                    SampleCount = dt_nbiStatus2(0)(1).ToString
                End If
                If NoOfRecords < (ExpectedRowCount - (ExpectedRowCount * nbiMaxDeviation / 100)) And SampleCount = 10 Then
                    ReportStatus = False
                End If
            End If



            Using sourceConnection As OdbcConnection = New OdbcConnection(Connstring)
                sourceConnection.Open()
                Dim strSql As String = ""
                Dim insrt_cmd As OdbcCommand = Nothing

                strSql = "UPDATE [IOS_Server].[dbo].[IOS_NBI_Reports_Status] SET [ReportTimeStamp_End] = GETDATE(),[ReportStatus] = ?,[ElapsedTime] = ?,
                          [NumberOfRecords] = ?,[Attempts] = ?,[SQLQueryFired] = ?, [URL] = ? WHERE [ReportsRunID] = ?"

                insrt_cmd = New OdbcCommand(strSql, sourceConnection)

                Dim p1 As OdbcParameter = New OdbcParameter("@ReportStatus", IIf(ReportStatus = True, "SUCCESS", "FAILED"))
                Dim p2 As OdbcParameter = New OdbcParameter("@ElapsedTime", ElapsedTime)
                Dim p3 As OdbcParameter = New OdbcParameter("@NumberOfRecords", NoOfRecords)
                Dim p4 As OdbcParameter = New OdbcParameter("@Attempts", Attempts + 1)
                Dim p5 As OdbcParameter = New OdbcParameter("@SQLQueryFired", SQLQueryFired.Replace("GETDATE()", "convert(datetime," & Chr(39) & Now.ToString("yyyy-MM-dd HH:mm") & Chr(39) & ")"))
                Dim p7 As OdbcParameter = New OdbcParameter("@URL", NBIReport_HyperLink.ToString)
                Dim p6 As OdbcParameter = New OdbcParameter("@ReportsRunID", ReportRunID)


                insrt_cmd.Parameters.Add(p1)
                insrt_cmd.Parameters.Add(p2)
                insrt_cmd.Parameters.Add(p3)
                insrt_cmd.Parameters.Add(p4)
                insrt_cmd.Parameters.Add(p5)
                insrt_cmd.Parameters.Add(p7)
                insrt_cmd.Parameters.Add(p6)

                Try
                    Dim j As Integer = insrt_cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                sourceConnection.Close()
            End Using
        Catch ex As Exception
            WriteString_Log(Now() & "    " & "JobLog Write Error:  " & ex.Message)
        End Try
    End Sub

    Public Sub JobLog(ByVal Connstring As String, ByVal msg As String, ByVal JobID As Integer)
        Try

            Using sourceConnection As OdbcConnection = New OdbcConnection(Connstring)
                sourceConnection.Open()

                ' Get data from the source table as a SqlDataReader.
                'Dim sql_insert As String = "INSERT INTO IOS_Jobs_Log (JobTimeStamp, JobID, JobMsg) VALUES (" & Chr(39) & Now() & Chr(39) & ", " & JobID & ", " & Chr(39) & msg & Chr(39) & ")"
                Dim insrt_cmd As OdbcCommand = New OdbcCommand("INSERT INTO IOS_Jobs_Log (JobTimeStamp, JobID, JobMsg) VALUES (?,?,?)", sourceConnection)


                Dim p1 As OdbcParameter = New OdbcParameter("@JobTimeStamp", Now().ToString("yyyy-MM-dd HH:mm:ss"))
                Dim p2 As OdbcParameter = New OdbcParameter("@JobID", JobID)
                Dim p3 As OdbcParameter = New OdbcParameter("@JobMsg", msg)
                insrt_cmd.Parameters.Add(p1)
                insrt_cmd.Parameters.Add(p2)
                insrt_cmd.Parameters.Add(p3)

                'Dim commandSourceData As OdbcCommand = New OdbcCommand(sql_insert, sourceConnection)

                Try
                    Dim j As Integer = insrt_cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                'commandSourceData.Dispose()
                'commandSourceData = Nothing
                sourceConnection.Close()

            End Using
        Catch ex As Exception
            WriteString_Log(Now() & "    " & "JobLog Write Error:  " & ex.Message)
        End Try
    End Sub

    Public Sub ReportLog(ByVal Connstring As String, ByVal msg As String, ByVal ReportID As Integer, ByVal status As String)
        Try

            Using sourceConnection As OdbcConnection = New OdbcConnection(Connstring)
                sourceConnection.Open()

                ' Get data from the source table as a SqlDataReader.
                'Dim sql_insert As String = "INSERT INTO IOS_Jobs_Log (JobTimeStamp, JobID, JobMsg) VALUES (" & Chr(39) & Now() & Chr(39) & ", " & JobID & ", " & Chr(39) & msg & Chr(39) & ")"
                Dim insrt_cmd As OdbcCommand = New OdbcCommand("INSERT INTO IOS_NBI_Reports_Log (ReportTimeStamp, ReportID, ReportMessage, ReportStatus) VALUES (?,?,?,?)", sourceConnection)


                Dim p1 As OdbcParameter = New OdbcParameter("@ReportTimeStamp", OdbcType.DateTime)
                p1.Value = Now()
                p1.Scale = 3
                Dim p2 As OdbcParameter = New OdbcParameter("@ReportID", ReportID)
                Dim p3 As OdbcParameter = New OdbcParameter("@ReportMessage", msg)
                Dim p4 As OdbcParameter = New OdbcParameter("@ReportStatus", status)
                insrt_cmd.Parameters.Add(p1)
                insrt_cmd.Parameters.Add(p2)
                insrt_cmd.Parameters.Add(p3)
                insrt_cmd.Parameters.Add(p4)

                'Dim commandSourceData As OdbcCommand = New OdbcCommand(sql_insert, sourceConnection)

                Try
                    Dim j As Integer = insrt_cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                'commandSourceData.Dispose()
                'commandSourceData = Nothing
                sourceConnection.Close()

            End Using
        Catch ex As Exception
            WriteString_Log(Now() & "    " & "JobLog Write Error:  " & ex.Message)
        End Try
    End Sub

    Public Function XML_Parameters_NSN(ByVal fn As String, ByVal dt As DataTable) As Boolean
        Try

            Dim objDom As Xml.XmlDocument
            Dim objRaml As Xml.XmlElement

            Dim objCMdata As Xml.XmlElement
            Dim objHeader As Xml.XmlElement
            Dim objLog As Xml.XmlElement
            Dim objMO As Xml.XmlElement
            Dim objParam As Xml.XmlElement
            Dim class_name, Version, distname, id, param, objlevel As String

            objDom = New Xml.XmlDocument
            objDom.LoadXml("<?xml version=""1.0"" encoding=""UTF-8""?><raml><cmData/></raml>")

            objRaml = objDom.GetElementsByTagName("raml").Item(0)
            objRaml.SetAttribute("version", "2.0")
            objRaml.SetAttribute("xmlns", "raml20.xsd")

            objCMdata = objDom.GetElementsByTagName("cmData").Item(0)
            objCMdata.SetAttribute("xmlns", "")
            objCMdata.SetAttribute("type", "plan")
            objCMdata.SetAttribute("scope", "all")
            objCMdata.SetAttribute("name", "default")


            'create header
            objHeader = objDom.CreateElement("header")
            objCMdata.AppendChild(objHeader)

            'create logs
            objLog = objDom.CreateElement("log")
            objHeader.AppendChild(objLog)
            objLog.SetAttribute("dateTime", Now.ToString("dd-MM-yyyy_HH-mm-ss"))
            objLog.SetAttribute("action", "created")

            Dim dn_old As String = ""
            'create XML for BTS param
            For Each dr As DataRow In dt.Rows

                If Not dr("NewParamValue").ToString Is Nothing Then
                    If dr("DN").ToString.Trim <> dn_old Then
                        dn_old = dr("DN").ToString.Trim

                        Try
                            Version = dr("Version").ToString
                        Catch ex As Exception
                            Version = "S15.3"
                        End Try

                        distname = dr("DN").ToString
                        id = dr("GID").ToString
                        'If distname.Contains("/WBTS") Then
                        'objlevel = "WBTS"
                        'ElseIf distname.Contains("/BTS") Then
                        ' objlevel = "BTS"
                        'Else
                        objlevel = Split(Split(distname, "/").Last, "-").First
                        'End If



                        objMO = objDom.CreateElement("managedObject")
                        objCMdata.AppendChild(objMO)
                        objMO.SetAttribute("class", objlevel)
                        objMO.SetAttribute("version", Version)
                        objMO.SetAttribute("distName", distname)
                        objMO.SetAttribute("id", id)
                        objMO.SetAttribute("operation", "update")

                    End If

                    objParam = objDom.CreateElement("p")
                    objMO.AppendChild(objParam)
                    objParam.SetAttribute("name", dr("ParameterName").ToString.Trim)
                    objParam.InnerText = dr("NewParamValue").ToString.Trim
                Else
                End If
            Next

            'save XML file
            If Not Directory.Exists(fn & "\Jobs\" & JobID) Then
                WriteString_Log("Creating Folder ... " & fn & "\Jobs\" & JobID)
                Directory.CreateDirectory(fn & "\Jobs\" & JobID)
                WriteString_Log("Created Folder ... " & fn & "\Jobs\" & JobID)

            End If
            objDom.Save(fn & "\Jobs\" & JobID & "\Job_" & JobID & "_RunID_" & dt(0)(0) & ".xml")
            JobLog(conn_ios, "Sequence Finished:  XML_NSN - Succes", JobID)
            Return True
        Catch ex As Exception
            JobLog(conn_ios, ex.Message, JobID)

            Return False
        End Try

    End Function

    Private Function MML_Parameters_Huawei(ByVal fn As String, ByVal dt As DataTable) As Boolean
        Try

            Dim jobid As Integer = Split(dt(0)("JobRunId").ToString, "_")(0).ToString
            Dim JobRunID As String = dt(0)("JobRunId").ToString
            Dim parent As String = dt(0)("PARENT").ToString

            WriteString_Log("Writing MML ... " & JobRunID)

            Dim MML_String As String = ""


            Dim fileHeader As String = "//***********MML SCRIPT*********//" & vbCrLf &
                                        "//Generation Time: " & Now() & vbCrLf &
                                        "//Generated by CellSens Platform" & vbCrLf &
                                        "//CellSens JobID: " & jobid & vbCrLf &
                                        "//CellSens JobRunID: " & JobRunID & vbCrLf &
                                        "//Target Controller: " & parent & vbCrLf &
                                        "//*****************************//" & vbCrLf

            Dim mo_old As String = ""
            Dim gid_old As String = ""
            Dim mml_command As String = ""
            Dim parent_old As String = ""
            For Each dr As DataRow In dt.Rows
                If Not dr("NewParamValue").ToString Is Nothing Then
                    If dr("Managed_Object").ToString.Trim <> mo_old Or dr("GID").ToString <> gid_old Then
                        mo_old = dr("Managed_Object").ToString.Trim
                        gid_old = dr("GID").ToString

                        If mml_command = "" Then
                            mml_command = MML_Command_SetORMOD(dr("MML_commands").ToString) + ": IDTYPE=BYNAME, CELLNAME=" + Chr(34) + dr("GID").ToString + Chr(34) + ","
                        Else
                            mml_command = mml_command.TrimEnd(",") + "; {" + parent_old + "}" + vbCrLf & MML_Command_SetORMOD(dr("MML_commands").ToString) + ": IDTYPE=BYNAME, CELLNAME=" + Chr(34) + dr("GID").ToString + Chr(34) + ","
                        End If
                        parent_old = dr("PARENT").ToString
                    End If
                    mml_command = mml_command + dr("ParameterName").ToString & "=" & dr("NewParamValue").ToString & ","
                End If
            Next
            If mml_command.EndsWith(",") Then
                mml_command = mml_command.TrimEnd(",") + "; {" + parent_old + "}"
            End If


            If Not Directory.Exists(fn & "\Jobs\" & jobid) Then
                WriteString_Log("Creating Folder ... " & fn & "\Jobs\" & jobid)
                Directory.CreateDirectory(fn & "\Jobs\" & jobid)
                WriteString_Log("Created Folder ... " & fn & "\Jobs\" & jobid)

            End If
            Dim FILE_NAME As String = fn & "\Jobs\" & jobid & "\" & JobRunID & ".txt"

            Static LogFileLock As New Object()
            SyncLock LogFileLock
                WriteString_Log("Creating File ... " & fn & "\Jobs\" & jobid & "\" & JobRunID & ".txt")
                File.AppendAllText(FILE_NAME, fileHeader + mml_command & vbCrLf)
                WriteString_Log("Created File ... " & fn & "\Jobs\" & jobid & "\" & JobRunID & ".txt")
            End SyncLock


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MML_Command_SetORMOD(ByVal command As String) As String
        Try
            Dim cmd() As String = Split(command, ",")
            For Each Str As String In cmd
                If Str.Contains("SET") Then
                    Return Str
                End If
                If Str.Contains("MOD") Then
                    Return Str
                End If
            Next
        Catch ex As Exception
            Return ""
        End Try
        Return ""
    End Function

    Public Function XML_Parameters_HUAWEI(ByVal fn As String, ByVal dt As DataTable) As Boolean
        Try

            Dim objDom As Xml.XmlDocument
            Dim objRaml As Xml.XmlElement

            Dim objCMdata As Xml.XmlElement
            Dim objHeader As Xml.XmlElement
            Dim objSubSession As Xml.XmlElement
            Dim objNE As Xml.XmlElement
            Dim objModule As Xml.XmlElement
            Dim objMoi As Xml.XmlElement
            Dim objAtt As Xml.XmlElement = Nothing
            Dim objParam As Xml.XmlElement

            objDom = New Xml.XmlDocument
            objDom.LoadXml("<?xml version=""1.0"" encoding=""ISO-8859-1""?><cmconfigdatafile></cmconfigdatafile>")

            objRaml = objDom.GetElementsByTagName("cmconfigdatafile").Item(0)
            objRaml.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            objRaml.SetAttribute("xmlns", "http://www.huawei.com/specs/SOM")
            objRaml.SetAttribute("xsi:schemaLocation", "http://www.huawei.com/specs/SOM CMEGBSS_NRM_BSC6910_V100R015C00.xsd")


            'create header
            objHeader = objDom.CreateElement("fileheader")
            objHeader.SetAttribute("fileType", "ImportFile")

            objRaml.AppendChild(objHeader)

            'create subsession
            objSubSession = objDom.CreateElement("subsession")
            objRaml.AppendChild(objSubSession)

            'create NE
            objNE = objDom.CreateElement("NE")
            objSubSession.AppendChild(objNE)
            objNE.SetAttribute("xsi:type", "")
            objNE.SetAttribute("netype", "")
            objNE.SetAttribute("neversion", "")
            objNE.SetAttribute("neid", "")

            'create Module
            objModule = objDom.CreateElement("module")
            objNE.AppendChild(objModule)
            objModule.SetAttribute("xsi:type", "Radio")
            objModule.SetAttribute("remark", "radio mois")




            Dim dn_old As String = ""
            Dim mo_old As String = ""
            'create XML for BTS param
            For Each dr As DataRow In dt.Rows

                If Not dr("NewParamValue").ToString Is Nothing Then
                    If dr("MO").ToString.Trim <> mo_old Then

                        'create moi
                        objMoi = objDom.CreateElement("moi")
                        objModule.AppendChild(objMoi)
                        objModule.SetAttribute("xsi:type", "")
                        objModule.SetAttribute("modifier", "update")

                        objAtt = objDom.CreateElement("attributes")
                        objModule.AppendChild(objSubSession)

                        mo_old = dr("MO").ToString.Trim


                    End If

                    objParam = objDom.CreateElement(dr("ParameterName").ToString.Trim.ToUpper)
                    objAtt.AppendChild(objParam)
                    objParam.InnerText = dr("NewParamValue").ToString.Trim
                Else
                End If
            Next

            'save XML file
            objDom.Save(fn & "\Job_" & JobID & "_RunID_" & dt(0)(0) & ".xml")

            Return True
        Catch ex As Exception
            JobLog(conn_ios, ex.Message, JobID)

            Return False
        End Try

    End Function

    Public Function ExecuteFinalQuery(ByVal ConnString As String, ByVal SQLString As String, ByVal QueryTimeOut As Integer) As Boolean
        Dim success As Boolean = False

        Using destinationConnection As SqlConnection = New SqlConnection(ConnString)
            Try
                destinationConnection.Open()
            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                success = False
            End Try

            Try
                Dim commandDropData As SqlCommand = New SqlCommand(SQLString, destinationConnection)
                Dim counter As Integer = 0
                commandDropData.CommandTimeout = QueryTimeOut
                counter = commandDropData.ExecuteNonQuery()
                commandDropData.Dispose()
                JobLog(conn_ios, "Job Final - #" & counter, JobID)
                success = True
            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                WriteString_Log(Now() & "    " & "JobLog ExecuteFinalError:  " & ex.Message & vbCrLf)
                success = False
                Return success
            End Try

            Try
                Dim commandDropData As SqlCommand = New SqlCommand("EXECUTE sp_JobResults_Hash " & JobID, destinationConnection)
                Dim counter As Integer = 0
                commandDropData.CommandTimeout = QueryTimeOut
                counter = commandDropData.ExecuteNonQuery()
                commandDropData.Dispose()
                success = True
            Catch
            End Try
        End Using
        Return success

    End Function

    Public Function ExecuteRemoteSP(ByVal ConnString As String, ByVal SQLString As String, ByVal QueryTimeOut As Integer) As Boolean
        Dim success As Boolean = False

        Using sourceConnection As OdbcConnection = New OdbcConnection(ConnString)
            Try
                sourceConnection.Open()
            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                WriteString_Log(Now() & "    " & "JobLog ExecuteRemoteSP Connection Error:  " & ex.Message)

                success = False
            End Try

            Try
                Dim commandData As OdbcCommand = New OdbcCommand()
                commandData.CommandType = CommandType.StoredProcedure
                commandData.CommandText = SQLString
                commandData.Connection = sourceConnection
                commandData.CommandTimeout = QueryTimeOut

                Dim counter As Integer = 0

                counter = commandData.ExecuteNonQuery()
                commandData.Dispose()
                JobLog(conn_ios, "Job SP - #" & counter, JobID)
                success = True
            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                WriteString_Log(Now() & "    " & "JobLog ExecuteRemoteSP Error:  " & ex.Message)

                success = False
            End Try

        End Using
        Return success

    End Function

    Public Function TransferData(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, ByVal QueryTimeOut As Integer) As Boolean

        Dim success As Boolean = True

        ' Open a connection to the Source database.
        Using sourceConnection As OdbcConnection = New OdbcConnection(ConnString_Source)
            Try
                sourceConnection.Open()
                WriteString_Log(Now() & "    " & "Connection Success: " & ConnString_Source)

            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                WriteString_Log(Now() & "    " & "Connection Failed: " & ex.Message & vbCrLf)
                success = False
            End Try

            If success = True Then
                'drop destination table on server
                Dim sql_drop As String = "IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") DROP TABLE " & DestinationTable & ";"

                ' Get data from the source table as a SqlDataReader.
                Dim commandSourceData As OdbcCommand = New OdbcCommand(SQLString_Source, sourceConnection)
                commandSourceData.CommandTimeout = QueryTimeOut
                Dim schemareader As OdbcDataReader = Nothing
                Dim datareader As OdbcDataReader = Nothing

                Dim sql_create As String = Nothing
                Try
                    'get schema to create sql for table creation
                    datareader = commandSourceData.ExecuteReader()
                    sql_create = GetCreateSQL_NoPK(DestinationTable, datareader.GetSchemaTable, Nothing)
                    'schemareader.Close()
                    WriteString_Log(Now() & "    " & "Create SQL: " & sql_create)

                    'set data reader schema
                    ' datareader = commandSourceData.ExecuteReader()
                Catch ex As Exception
                    JobLog(conn_ios, ex.Message, JobID)
                    WriteString_Log(Now() & "    " & "Datareader failed: " & ex.Message)

                    success = False
                End Try


                ' Open the destination connection. In the real world you would 
                ' not use SqlBulkCopy to move data from one table to the other   
                ' in the same database. This is for demonstration purposes only.
                Using destinationConnection As SqlConnection = New SqlConnection(ConnString_Dest)
                    Try
                        destinationConnection.Open()

                        WriteString_Log(Now() & "    " & "Destination Connection Success: " & ConnString_Dest)

                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        WriteString_Log(Now() & "    " & "Destination Connection Failed: " & ex.Message & vbCrLf)

                        success = False
                    End Try

                    Try
                        'checking if table exists, if not, create

                        Dim commandDropData As SqlCommand = New SqlCommand(sql_drop, destinationConnection)
                        commandDropData.ExecuteNonQuery()
                        commandDropData.Dispose()

                        WriteString_Log(Now() & "    " & "Drop Table if Exists Success: " & sql_drop)


                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        WriteString_Log(Now() & "    " & "Drop Table if Exists Failed: " & ex.Message & vbCrLf)

                        success = False
                    End Try

                    Try
                        Dim commandCreateTable As SqlCommand = New SqlCommand(sql_create, destinationConnection)
                        commandCreateTable.ExecuteNonQuery()
                        commandCreateTable.Dispose()

                        WriteString_Log(Now() & "    " & "Table Creation Success: " & sql_create)

                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        WriteString_Log(Now() & "    " & "Table Creation Failed: " & sql_create & vbCrLf & ex.Message & vbCrLf)

                        success = False
                    End Try
                    ' Set up the bulk copy object. 
                    ' The column positions in the source data reader 
                    ' match the column positions in the destination table, 
                    ' so there is no need to map columns.
                    Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)
                        bulkCopy.DestinationTableName = DestinationTable
                        bulkCopy.BulkCopyTimeout = QueryTimeOut
                        bulkCopy.BatchSize = 500
                        bulkCopy.NotifyAfter = 500
                        AddHandler bulkCopy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                        Try
                            ' Write from the source to the destination.

                            bulkCopy.WriteToServer(datareader)
                            success = True

                            WriteString_Log(Now() & "    " & "Bulk Insert Success! ")

                        Catch ex As Exception
                            JobLog(conn_ios, ex.Message, JobID)
                            WriteString_Log(Now() & "    " & "Bulk Insert Failed: " & ex.Message & vbCrLf)

                            success = False
                        Finally
                            ' Close the SqlDataReader. The SqlBulkCopy
                            ' object is automatically closed at the end
                            ' of the Using block.
                            If Not datareader Is Nothing Then
                                datareader.Close()
                            End If
                        End Try
                    End Using

                End Using
            End If
        End Using
        Return success
    End Function

    Public Function TransferData_FromSQLclient(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, ByVal QueryTimeOut As Integer) As Boolean

        Dim success As Boolean = True

        ' Open a connection to the Source database.
        Using sourceConnection As SqlConnection = New SqlConnection(ConnString_Source)
            Try
                sourceConnection.Open()
            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                WriteString_Log(Now() & "    " & "Connection Failed: " & ex.Message)
                success = False
            End Try

            If success = True Then
                'drop destination table on server
                Dim sql_drop As String = "IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") DROP TABLE " & DestinationTable & ";"

                ' Get data from the source table as a SqlDataReader.
                Dim commandSourceData As SqlCommand = New SqlCommand(SQLString_Source, sourceConnection)
                commandSourceData.CommandTimeout = QueryTimeOut
                Dim schemareader As SqlDataReader = Nothing
                Dim datareader As SqlDataReader = Nothing

                Dim sql_create As String = Nothing
                'Dim ds As DataSet = Nothing

                Try
                    'get schema to create sql for table creation
                    datareader = commandSourceData.ExecuteReader()
                    ' datareader = commandSourceData.ExecuteReader(CommandBehavior.KeyInfo)
                    sql_create = GetCreateSQL_NoPK(DestinationTable, datareader.GetSchemaTable, Nothing)
                    'schemareader.Close()

                    'set data reader schema
                    ' datareader = commandSourceData.ExecuteReader()
                Catch ex As Exception
                    JobLog(conn_ios, ex.Message, JobID)
                    WriteString_Log(Now() & "    " & "Datareader failed: " & ex.Message)

                    success = False
                End Try


                ' Open the destination connection. In the real world you would 
                ' not use SqlBulkCopy to move data from one table to the other   
                ' in the same database. This is for demonstration purposes only.
                Using destinationConnection As SqlConnection = New SqlConnection(ConnString_Dest)
                    Try
                        destinationConnection.Open()

                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        WriteString_Log(Now() & "    " & "Destination Connection Failed: " & ex.Message)

                        success = False
                    End Try

                    Try
                        'checking if table exists, if not, create

                        Dim commandDropData As SqlCommand = New SqlCommand(sql_drop, destinationConnection)
                        commandDropData.ExecuteNonQuery()
                        commandDropData.Dispose()
                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        WriteString_Log(Now() & "    " & "Drop Table if Exists Failed: " & ex.Message)

                        success = False
                    End Try

                    Try
                        Dim commandCreateTable As SqlCommand = New SqlCommand(sql_create, destinationConnection)
                        commandCreateTable.ExecuteNonQuery()
                        commandCreateTable.Dispose()
                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        WriteString_Log(Now() & "    " & "Table Creation Failed: " & sql_create & vbCrLf & ex.Message)

                        success = False
                    End Try
                    ' Set up the bulk copy object. 
                    ' The column positions in the source data reader 
                    ' match the column positions in the destination table, 
                    ' so there is no need to map columns.
                    Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)
                        bulkCopy.DestinationTableName = DestinationTable
                        bulkCopy.BulkCopyTimeout = 500
                        bulkCopy.BatchSize = 500
                        bulkCopy.NotifyAfter = 500
                        AddHandler bulkCopy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                        Try
                            ' Write from the source to the destination.

                            bulkCopy.WriteToServer(datareader)
                            success = True
                        Catch ex As Exception
                            JobLog(conn_ios, ex.Message, JobID)
                            WriteString_Log(Now() & "    " & "Bulk Insert Failed: " & ex.Message)

                            success = False
                        Finally
                            ' Close the SqlDataReader. The SqlBulkCopy
                            ' object is automatically closed at the end
                            ' of the Using block.
                            If Not datareader Is Nothing Then
                                datareader.Close()
                            End If
                        End Try
                    End Using

                End Using
            End If
        End Using
        Return success
    End Function

    Public Function TransferDataToExistingTable(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, ByVal QueryTimeOut As Integer) As Boolean

        Dim success As Boolean = True
        Dim SourceTableCountStart As Long = -1
        ' Open a connection to the Source database.
        Using sourceConnection As OdbcConnection = New OdbcConnection(ConnString_Source)
            Try
                WriteString_Log(Now() & "    " & "Open Connection for Transfer Job: " & DestinationTable & " - " & ConnString_Source)
                sourceConnection.Open()

            Catch ex As Exception
                Console.WriteLine("open source connection: " & DestinationTable & ex.Message)
                JobLog(conn_ios, "open source connection: " & DestinationTable & ex.Message, JobID)
                WriteString_Log(Now() & "    " & "open source connection: " & DestinationTable & ex.Message)
                success = False
            End Try

            If success = True Then
                'drop destination table on server
                Dim sql_drop As String = "IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") "

                ' Get data from the source table as a SqlDataReader.
                Try

                    Dim commandSourceDataCount As OdbcCommand = New OdbcCommand("SELECT COUNT(*) FROM (" & SQLString_Source & ") a", sourceConnection)
                    SourceTableCountStart = commandSourceDataCount.ExecuteScalar
                    WriteString_Log(Now() & "    " & " Count Of SourceRows - " & DestinationTable & ": " & SourceTableCountStart)
                    commandSourceDataCount.Dispose()
                Catch ex As Exception
                    WriteString_Log(Now() & "    " & " Count Of SourceRows - Failed -  " & DestinationTable & ex.Message)
                End Try

                Dim commandSourceData As OdbcCommand = New OdbcCommand(SQLString_Source, sourceConnection)
                commandSourceData.CommandTimeout = QueryTimeOut
                Dim schemareader As OdbcDataReader = Nothing
                Dim datareader As OdbcDataReader = Nothing

                Dim sql_create As String = Nothing
                Try
                    'get schema to create sql for table creation
                    datareader = commandSourceData.ExecuteReader(CommandBehavior.KeyInfo)
                    sql_create = GetCreateSQL(DestinationTable, datareader.GetSchemaTable, Nothing)
                    'schemareader.Close()


                    WriteString_Log(Now() & "    " & " SQL Create: " & sql_create)
                    'set data reader schema
                    ' datareader = commandSourceData.ExecuteReader()
                Catch ex As Exception
                    Console.WriteLine("query reader: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "query reader: " & DestinationTable & ex.Message, JobID)
                    WriteString_Log(Now() & "    " & "query reader: " & DestinationTable & ex.Message)
                    success = False
                End Try


                ' Open the destination connection. In the real world you would 
                ' not use SqlBulkCopy to move data from one table to the other   
                ' in the same database. This is for demonstration purposes only.
                Using destinationConnection As SqlConnection = New SqlConnection(ConnString_Dest)
                    ' Dim commandRowCount As New SqlCommand("IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") SELECT Total_Rows= SUM(st.row_count) FROM sys.dm_db_partition_stats st WHERE  (index_id < 2) AND object_name(object_id) = " & Chr(39) & DestinationTable & Chr(39), destinationConnection)
                    Dim commandRowCount As New SqlCommand("IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") SELECT COUNT(*) FROM  " & DestinationTable, destinationConnection)

                    Dim countStart As Long = 0

                    Try
                        destinationConnection.Open()
                        WriteString_Log(Now() & "    " & "DestinationConnection Open: " & DestinationTable & " - " & destinationConnection.ConnectionString)

                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False
                    End Try

                    Try
                        countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar())

                        Dim commandDropData As SqlCommand = New SqlCommand(sql_drop & sql_create, destinationConnection)
                        commandDropData.ExecuteNonQuery()
                        commandDropData.Dispose()

                        WriteString_Log(Now() & "    " & "Drop Data Success: " & sql_drop & sql_create)


                    Catch ex As Exception
                        Console.WriteLine("checking existing table: " & DestinationTable & ex.Message)
                        JobLog(conn_ios, "checking existing table: " & DestinationTable & ex.Message, JobID)
                        WriteString_Log(Now() & "    " & "checking existing table: " & DestinationTable & ex.Message)
                        success = False
                    End Try

                    Dim destreader As SqlDataReader = Nothing
                    Try
                        'get schema to create sql for table creation

                        Dim commandDestData As SqlCommand = New SqlCommand("SELECT * FROM " & DestinationTable, destinationConnection)
                        commandDestData.CommandTimeout = QueryTimeOut
                        destreader = commandDestData.ExecuteReader()
                        Dim destschema As DataTable = destreader.GetSchemaTable
                        Dim sql_alter As String = Nothing

                        WriteString_Log(Now() & "    " & "Destination Table Schema Read " & DestinationTable)

                        For Each SourceCol As DataRow In datareader.GetSchemaTable.Rows

                            'does column exist?
                            If Not ColumnExists(destreader, SourceCol("ColumnName").ToString) Then
                                'add column
                                JobLog(conn_ios, "NEW COLUMN DETECTED: " & DestinationTable & " - " & SourceCol("ColumnName").ToString, JobID)
                                Console.WriteLine(DestinationTable & " - New Column Detected In Source - Column Name: " & SourceCol("ColumnName").ToString)
                                WriteString_Log(Now() & "    " & "NEW COLUMN DETECTED: " & DestinationTable & " - " & SourceCol("ColumnName").ToString)

                                sql_alter = sql_alter & "ALTER TABLE " & DestinationTable & " ADD [" & SourceCol("ColumnName").ToString & "] " & SQLGetType(SourceCol) & vbCrLf
                            End If

                        Next
                        destreader.Close()
                        commandDestData.Dispose()

                        If Not sql_alter Is Nothing Then
                            Dim commandDestAlter As SqlCommand = New SqlCommand(sql_alter, destinationConnection)
                            commandDestAlter.ExecuteNonQuery()
                            commandDestAlter.Dispose()
                            WriteString_Log(Now() & "    " & "Alter Schema Success " & sql_alter)

                        End If


                        'refresh schema table
                        'destreader = commandDestData.ExecuteReader()


                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        WriteString_Log(Now() & "    " & " Schema Detection: " & DestinationTable & ex.Message)
                        success = False

                        If Not destreader Is Nothing Then
                            destreader.Close()
                        End If

                    End Try
                    ' Set up the bulk copy object. 
                    ' The column positions in the source data reader 

                    Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)
                        bulkCopy.DestinationTableName = DestinationTable
                        bulkCopy.BulkCopyTimeout = QueryTimeOut * 2
                        bulkCopy.BatchSize = 500
                        bulkCopy.NotifyAfter = 500
                        AddHandler bulkCopy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                        'column mappings
                        For Each drow As DataRow In datareader.GetSchemaTable.Rows
                            bulkCopy.ColumnMappings.Add(drow("ColumnName").ToString, drow("ColumnName").ToString)
                        Next

                        Try
                            ' Write from the source to the destination.

                            bulkCopy.WriteToServer(datareader)


                            Dim countEnd As Long = System.Convert.ToInt32(commandRowCount.ExecuteScalar())


                            'If SourceTableCountStart <> (countEnd - countStart) Then
                            '    Console.WriteLine("WARNING - NOK: " & DestinationTable & " - Source#:" & SourceTableCountStart & " - DestStart#" & countStart & " - DestEnd#" & countEnd & " - DestInserted:#" & (countEnd - countStart).ToString)
                            '    JobLog(conn_ios, "WARNING - NOK: " & DestinationTable & " - Source#:" & SourceTableCountStart & " - DestStart#" & countStart & " - DestEnd#" & countEnd & " - DestInserted:#" & (countEnd - countStart).ToString, JobID)
                            '    WriteString_Log(Now() & "    " & "WARNING - NOK: " & DestinationTable & " - Source#:" & SourceTableCountStart & " - DestStart#" & countStart & " - DestEnd#" & countEnd & " - DestInserted:#" & (countEnd - countStart).ToString)
                            '    success = False
                            'Else
                            Console.WriteLine("OK: " & DestinationTable & " - Source#:" & SourceTableCountStart & " - DestStart#" & countStart & " - DestEnd#" & countEnd & " - DestInserted:#" & (countEnd - countStart).ToString)
                            JobLog(conn_ios, "OK: " & DestinationTable & " - Source#:" & SourceTableCountStart & " - DestStart#" & countStart & " - DestEnd#" & countEnd & " - DestInserted:#" & (countEnd - countStart).ToString, JobID)
                            WriteString_Log(Now() & "    " & "OK: " & DestinationTable & " - Source#:" & SourceTableCountStart & " - DestStart#" & countStart & " - DestEnd#" & countEnd & " - DestInserted:#" & (countEnd - countStart).ToString)
                            success = True
                            'End If


                        Catch ex As Exception
                            Console.WriteLine("bulk copy: " & DestinationTable & ex.Message)
                            JobLog(conn_ios, "bulk copy: " & DestinationTable & ex.Message, JobID)
                            WriteString_Log(Now() & "    " & "bulk copy: " & DestinationTable & ex.Message)
                            success = False
                        Finally
                            ' Close the SqlDataReader. The SqlBulkCopy
                            ' object is automatically closed at the end
                            ' of the Using block.
                            If Not datareader Is Nothing Then
                                datareader.Close()
                            End If
                        End Try
                    End Using

                End Using
            End If
        End Using
        Return success
    End Function

    Public Function TransferDataToCSV(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, ByVal QueryTimeOut As Integer) As Boolean

        Dim success As Boolean = True

        ' Open a connection to the Source database.
        Using sourceConnection As OdbcConnection = New OdbcConnection(ConnString_Source)
            Try
                sourceConnection.Open()

            Catch ex As Exception
                Console.WriteLine("open source connection: " & DestinationTable & ex.Message)
                JobLog(conn_ios, "open source connection: " & DestinationTable & ex.Message, JobID)
                success = False
            End Try

            If success = True Then
                'drop destination table on server

                ' Get data from the source table as a SqlDataReader.
                Dim commandSourceData As OdbcCommand = New OdbcCommand(SQLString_Source, sourceConnection)
                commandSourceData.CommandTimeout = QueryTimeOut
                Dim schemareader As OdbcDataReader = Nothing
                Dim datareader As OdbcDataReader = Nothing

                Dim sql_create As String = Nothing
                Try
                    'get schema to create sql for table creation
                    datareader = commandSourceData.ExecuteReader(CommandBehavior.KeyInfo)

                Catch ex As Exception
                    Console.WriteLine("query reader: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "query reader: " & DestinationTable & ex.Message, JobID)
                    success = False
                End Try

                Dim path As String = ConnString_Dest
                If Not path.EndsWith("\") Then
                    path = path + "\"
                End If

                Try
                    If Not System.IO.Directory.Exists(ConnString_Dest) Then
                        System.IO.Directory.CreateDirectory(ConnString_Dest)
                    End If
                Catch ex As Exception
                    Console.WriteLine("Directory Check Failed: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "Directory Check Failed: " & DestinationTable & ex.Message, JobID)
                    success = False
                End Try

                'replacing mask 
                If success = True Then


                    Dim nowdate As DateTime = Now
                    Dim fp As String = DestinationTable
                    fp = fp.Replace("YYYY", Now.Year)
                    fp = fp.Replace("MM", Strings.Right("0" + Now.Month.ToString, 2))
                    fp = fp.Replace("DD", Strings.Right("0" + Now.Day.ToString, 2))
                    fp = fp.Replace("HH", Strings.Right("0" + Now.Hour.ToString, 2))
                    fp = fp.Replace("mm", Strings.Right("0" + Now.Minute.ToString, 2))
                    Dim NumOfRows As Long = 0
                    Try

                        Using writer As StreamWriter = New StreamWriter(path + fp, False)
                            writer.Write(String.Format("{0}", datareader.GetName(0)))
                            For i = 1 To datareader.FieldCount - 1
                                writer.Write(";")
                                writer.Write(String.Format("{0}", datareader.GetName(i)))
                            Next
                            writer.WriteLine()
                            NumOfRows = NumOfRows + 1
                            While datareader.Read
                                writer.Write(String.Format("{0}", datareader.GetValue(0)))
                                For i = 1 To datareader.FieldCount - 1
                                    writer.Write(";")
                                    writer.Write(String.Format("{0}", datareader.GetValue(i)))
                                Next
                                writer.WriteLine()
                                NumOfRows = NumOfRows + 1
                            End While
                        End Using

                    Catch ex As Exception
                        Console.WriteLine("StreamWriter Failed: " & DestinationTable & ex.Message)
                        JobLog(conn_ios, "StreamWriter Failed: " & DestinationTable & ex.Message, JobID)
                        success = False
                    End Try
                    Console.WriteLine("StreamWriter Finished: #" & NumOfRows)
                    JobLog(conn_ios, "StreamWriter Finished: #" & NumOfRows, JobID)

                End If

            End If
        End Using
        Return success
    End Function

    Public Function ExportCSVandMail(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, ByVal QueryTimeOut As Integer) As Boolean

        Dim success As Boolean = True
        Dim sent As Boolean = True

        Dim mailData As String() = ConnString_Dest.Split("|")

        ' Open a connection to the Source database.
        Using sourceConnection As OdbcConnection = New OdbcConnection(ConnString_Source)
            Try
                sourceConnection.Open()

            Catch ex As Exception
                Console.WriteLine("open source connection: " & DestinationTable & ex.Message)
                JobLog(conn_ios, "open source connection: " & DestinationTable & ex.Message, JobID)
                success = False
            End Try

            If success = True Then
                '

                ' Get data from the source table as a SqlDataReader.
                Dim commandSourceData As OdbcCommand = New OdbcCommand(SQLString_Source, sourceConnection)
                commandSourceData.CommandTimeout = QueryTimeOut
                Dim schemareader As OdbcDataReader = Nothing
                Dim datareader As OdbcDataReader = Nothing


                Dim sql_create As String = Nothing
                Try
                    'get schema to create sql for table creation
                    datareader = commandSourceData.ExecuteReader(CommandBehavior.KeyInfo)

                Catch ex As Exception
                    Console.WriteLine("query reader: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "query reader: " & DestinationTable & ex.Message, JobID)
                    success = False
                End Try

                Dim path As String = "..\" + DestinationTable

                Try
                    If Not System.IO.Directory.Exists(DestinationTable) Then
                        System.IO.Directory.CreateDirectory(DestinationTable)
                    End If
                Catch ex As Exception
                    Console.WriteLine("Directory Check Failed: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "Directory Check Failed: " & DestinationTable & ex.Message, JobID)
                    success = False
                End Try

                'replacing mask 
                If success = True Then


                    Dim nowdate As DateTime = Now
                    Dim NumOfRows As Long = 0
                    Try

                        Using writer As StreamWriter = New StreamWriter(path, False)
                            writer.Write(String.Format("{0}", datareader.GetName(0)))
                            For i = 1 To datareader.FieldCount - 1
                                writer.Write(";")
                                writer.Write(String.Format("{0}", datareader.GetName(i)))
                            Next
                            writer.WriteLine()
                            NumOfRows = NumOfRows + 1
                            While datareader.Read
                                writer.Write(String.Format("{0}", datareader.GetValue(0)))
                                For i = 1 To datareader.FieldCount - 1
                                    writer.Write(";")
                                    writer.Write(String.Format("{0}", datareader.GetValue(i)))
                                Next
                                writer.WriteLine()
                                NumOfRows = NumOfRows + 1
                            End While
                        End Using

                    Catch ex As Exception
                        Console.WriteLine("StreamWriter Failed: " & DestinationTable & ex.Message)
                        JobLog(conn_ios, "StreamWriter Failed: " & DestinationTable & ex.Message, JobID)
                        success = False
                    End Try
                    Console.WriteLine("StreamWriter Finished: #" & NumOfRows)
                    JobLog(conn_ios, "StreamWriter Finished: #" & NumOfRows, JobID)

                End If

                Try
                    sent = SendAttachmentMail(mailData(0), mailData(1), mailData(2), path, ConfigurationManager.AppSettings.Get("displayNameSMTP").ToString, -1)
                Catch ex As Exception
                    JobLog(conn_ios, "Mail Failed To Send: " & ex.Message, JobID)
                    sent = False
                End Try


            End If


        End Using



        Return success
    End Function

    Public Function TransferDataToNBICSV_NoStream(ByVal ReportID As Integer, ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ExportPath As String, ByVal ExportFile As String, ByVal QueryTimeOut As Integer, ByVal Recipient As String, ByVal EmailLinkNBI As String, AttachedFileDelimiter As String,
                                                  ByVal emailEnabled As Boolean, Optional ReportsRunID As String = Nothing, Optional Attemps As Integer = Nothing, Optional sqlQuery As String = Nothing) As Boolean

        Dim success As Boolean = True
        Dim dsSourceData As DataSet = New DataSet
        Dim SQLtoFire As String = ""

        If sqlQuery IsNot Nothing Then
            SQLtoFire = sqlQuery
        End If

        Try
            ' Open a connection to the Source database.
            Using sourceConnection As SqlConnection = New SqlConnection(ConnString_Source)
                Try
                    sourceConnection.Open()
                Catch ex As Exception
                    Console.WriteLine("Error: open source connection: " & ExportFile & " " & ex.Message)
                    ReportLog(conn_ios, "IOS DTE - Error: open source connection: " & ExportFile & " " & ex.Message, ReportID, "ERROR")
                    success = False
                    UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: 0", success, 0, Attemps, SQLtoFire)
                End Try

                If success = True Then
                    'drop destination table on server

                    If sqlQuery IsNot Nothing Then
                        SQLtoFire = sqlQuery
                    Else

                        Dim commandSourceData1 As SqlCommand = New SqlCommand(SQLString_Source, sourceConnection)
                        commandSourceData1.CommandTimeout = QueryTimeOut
                        Dim schemareader1 As SqlDataReader = Nothing
                        Dim datareader1 As SqlDataReader = Nothing

                        Dim sql_create As String = Nothing
                        Try
                            'get schema to create sql for table creation

                            datareader1 = commandSourceData1.ExecuteReader(CommandBehavior.KeyInfo)

                        Catch ex As Exception
                            Console.WriteLine("query reader - Sql To Fire: " & ExportFile & ex.Message)
                            ReportLog(conn_ios, "IOS DTE - query reader - Sql To Fire: " & ExportFile & ex.Message, JobID, "ERROR")
                            success = False
                            UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: 0", success, 0, Attemps, SQLtoFire)
                        End Try

                        While datareader1.Read
                            SQLtoFire = datareader1.GetValue(0)
                            WriteString_Log("NBI - ReportID: " & ReportID & vbCrLf & "SQL:" & vbCrLf & SQLtoFire)
                        End While
                        datareader1.Close()
                    End If

                    If SQLtoFire <> "" Then

                        Dim sw As New Stopwatch()
                        sw.Start()

                        Dim commandSourceData As SqlCommand = New SqlCommand(SQLtoFire, sourceConnection)
                        commandSourceData.CommandTimeout = QueryTimeOut
                        Dim commandAdapter As SqlDataAdapter = New SqlDataAdapter(commandSourceData)

                        Try
                            'get schema to create sql for table creation
                            commandAdapter.Fill(dsSourceData)

                        Catch ex As Exception
                            Console.WriteLine("query reader - Sql To Fire: " & ExportFile & ex.Message)
                            ReportLog(conn_ios, "IOS DTE - query reader - Sql To Fire: " & ExportFile & ex.Message, JobID, "ERROR")
                            success = False
                            UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: " & sw.ElapsedMilliseconds.ToString, success, 0, Attemps, SQLtoFire)
                        End Try

                        Dim QueryTime As Long = sw.ElapsedMilliseconds
                        sw.Reset()

                        If dsSourceData.Tables.Count = 0 Then
                            Console.WriteLine("Query Success: Elasped Time [ms]: " & QueryTime.ToString & " -- StreamWriter Finished: #" & 0 & "  Elapsed Time [ms]:" & sw.ElapsedMilliseconds.ToString)
                            ReportLog(conn_ios, "IOS DTE - Query Success: Elasped Time [ms]: " & QueryTime.ToString & " -- StreamWriter Finished: #" & 0 & "  Elapsed Time [ms]:" & sw.ElapsedMilliseconds.ToString, ReportID, "SUCCESS")
                        Else
                            Console.WriteLine("Query Success: Elasped Time [ms]: " & QueryTime.ToString & " -- StreamWriter Finished: #" & dsSourceData.Tables(0).Rows.Count & "  Elapsed Time [ms]:" & sw.ElapsedMilliseconds.ToString)
                            ReportLog(conn_ios, "IOS DTE - Query Success: Elasped Time [ms]: " & QueryTime.ToString & " -- StreamWriter Finished: #" & dsSourceData.Tables(0).Rows.Count & "  Elapsed Time [ms]:" & sw.ElapsedMilliseconds.ToString, ReportID, "SUCCESS")
                        End If

                        Try
                            NBIReport_HyperLink = ConfigurationManager.AppSettings.Get("HyperLink").ToString
                            If NBIReport_HyperLink.Contains("https:") Then
                                NBIReport_HyperLink = NBIReport_HyperLink.Substring(InStr(NBIReport_HyperLink, "https:") - 1, InStr(NBIReport_HyperLink, "@ExportFile") - InStr(NBIReport_HyperLink, "https:")) + IIf(NBIOutputFolder <> "", NBIOutputFolder + "/", "") + ExportFile
                            Else
                                NBIReport_HyperLink = NBIReport_HyperLink.Substring(InStr(NBIReport_HyperLink, "http:") - 1, InStr(NBIReport_HyperLink, "@ExportFile") - InStr(NBIReport_HyperLink, "http:")) + IIf(NBIOutputFolder <> "", NBIOutputFolder + "/", "") + ExportFile
                            End If


                        Catch ex As Exception

                        End Try

                        If dsSourceData.Tables.Count = 0 Then
                            UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: " & QueryTime.ToString, success, 0, Attemps, SQLtoFire)
                        Else
                            UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: " & QueryTime.ToString, success, dsSourceData.Tables(0).Rows.Count, Attemps, SQLtoFire)
                        End If
                    Else
                        UpdateNBIReportStatus(conn_ios, ReportsRunID, "No SQL", False, 0, Attemps, SQLtoFire)
                    End If

                    ' Get data from the source table as a SqlDataReader.

                End If
            End Using

        Catch ex As Exception
            WriteString_Log("NBI - ReportID: " & ReportID & vbCrLf & Now() & "    " & "NBI Report Error:  " & ex.Message)
        End Try

        If Not dsSourceData Is Nothing AndAlso success = True AndAlso dsSourceData.Tables.Count <> 0 Then

            Dim ExpectedCount As Int32 = Report_ExpectedCount(ReportID)
            If dsSourceData.Tables(0).Rows.Count < ExpectedCount Then
                ReportLog(conn_ios, "IOS DTE - Query count too low: " & dsSourceData.Tables(0).Rows.Count & " < " & ExpectedCount, ReportID, "FAILED")
                Return False
            End If

            Dim path As String = ExportPath
            If Not path.EndsWith("\") Then
                path = path + "\"
            End If

            If AttachedFileDelimiter = "TAB" Then
                AttachedFileDelimiter = vbTab
            End If

            Dim reportName As String = Replace(ExportFile, ".CSV", "")

            Try
                If Not System.IO.Directory.Exists(ExportPath) Then
                    System.IO.Directory.CreateDirectory(ExportPath)
                End If
            Catch ex As Exception
                Console.WriteLine("Directory Check Failed: " & ExportFile & ex.Message)
                ReportLog(conn_ios, "IOS DTE - Directory Check Failed: " & ExportFile & ex.Message, JobID, "ERROR")
                success = False
                UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: 0", success, dsSourceData.Tables(0).Rows.Count, Attemps, SQLtoFire)
            End Try

            DataTableToCSV(dsSourceData.Tables(0), path + ExportFile, AttachedFileDelimiter)

            Dim myFile As New FileInfo(path + ExportFile)
            Dim filesize As Double = myFile.Length / 1000000

            If emailEnabled = True Then
                If Recipient <> "" And (EmailLinkNBI = True Or filesize >= 25.0) Then
                    Console.WriteLine("EmailLinkNBI = True or  Filesize > 25MB")
                    SendLinkMail(Recipient,
                             Replace(ConfigurationManager.AppSettings.Get("LinkMailSubject").ToString, "@reportName", reportName),
                             Replace(ConfigurationManager.AppSettings.Get("LinkMailBody").ToString, "@reportName", reportName),
                             NBIReport_HyperLink,
                             ConfigurationManager.AppSettings.Get("displayNameSMTP").ToString, ReportID)
                ElseIf EmailLinkNBI = False And filesize <= 25.0 Then
                    Console.WriteLine("EmailLinkNBI = False And  Filesize > 25MB")
                    SendAttachmentMail(Recipient,
                               Replace(ConfigurationManager.AppSettings.Get("NBImailSubject").ToString, "@reportName", reportName),
                               Replace(ConfigurationManager.AppSettings.Get("NBImailBody").ToString, "@reportName", reportName),
                               path + ExportFile,
                               ConfigurationManager.AppSettings.Get("displayNameSMTP").ToString, ReportID)
                Else
                    Console.WriteLine("Check the NBI report table")
                    ReportLog(conn_ios, "IOS DTE - Check report configuration table!", ReportID, "INFO")
                End If

            End If

        Else
            Console.WriteLine("No Data in query: " & ExportFile)
            ReportLog(conn_ios, "IOS DTE - No Data: " & ExportFile, JobID, "ERROR")
        End If
        Return success
    End Function

    Public Function TransferDataToNBICSV(ByVal ReportID As Integer, ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ExportPath As String, ByVal ExportFile As String, ByVal QueryTimeOut As Integer, ByVal Recipient As String,
                                         ByVal EmailLinkNBI As String, AttachedFileDelimiter As String, ByVal emailEnabled As Boolean, Optional ReportsRunID As String = Nothing, Optional Attemps As Integer = Nothing, Optional sqlQuery As String = Nothing) As Boolean

        Dim success As Boolean = True
        Dim SQLtoFire As String = ""

        If sqlQuery IsNot Nothing Then
            SQLtoFire = sqlQuery
        End If

        Try

            ' Open a connection to the Source database.
            Using sourceConnection As SqlConnection = New SqlConnection(ConnString_Source)
                Try
                    sourceConnection.Open()

                Catch ex As Exception
                    Console.WriteLine("Error: open source connection: " & ExportFile & " " & ex.Message)
                    ReportLog(conn_ios, "IOS DTE - Error: open source connection: " & ExportFile & " " & ex.Message, ReportID, "ERROR")
                    success = False
                    UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: 0", success, 0, Attemps, SQLtoFire)
                End Try

                If success = True Then
                    'drop destination table on server

                    If sqlQuery IsNot Nothing Then    ' if a re-attempt query is provided, the sp_IOS_GenerateNBIReport shouldn't be caleld
                        SQLtoFire = sqlQuery
                    Else

                        ' Get data from the source table as a SqlDataReader.
                        Dim commandSourceData1 As SqlCommand = New SqlCommand(SQLString_Source, sourceConnection)
                        commandSourceData1.CommandTimeout = QueryTimeOut
                        Dim schemareader1 As SqlDataReader = Nothing
                        Dim datareader1 As SqlDataReader = Nothing

                        Dim sql_create As String = Nothing
                        Try

                            'get schema to create sql for table creation
                            datareader1 = commandSourceData1.ExecuteReader(CommandBehavior.KeyInfo)
                            WriteString_Log("NBI - ReportID: " & ReportID & vbCrLf & "SQL:" & vbCrLf & SQLString_Source & " - DataReader Opened")

                        Catch ex As Exception
                            Console.WriteLine("query reader - Sql To Fire: " & ExportFile & ex.Message)
                            ReportLog(conn_ios, "IOS DTE - query reader - Sql To Fire: " & ExportFile & ex.Message, JobID, "ERROR")
                            success = False
                            UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: 0", success, 0, Attemps, SQLtoFire)
                        End Try


                        While datareader1.Read
                            SQLtoFire = datareader1.GetValue(0)
                            WriteString_Log("NBI - ReportID: " & ReportID & vbCrLf & "SQL:" & vbCrLf & SQLtoFire)
                        End While
                        datareader1.Close()
                    End If

                    If SQLtoFire <> "" Then
                        Dim commandSourceData As SqlCommand = New SqlCommand(SQLtoFire, sourceConnection)
                        commandSourceData.CommandTimeout = QueryTimeOut
                        Dim datareader As SqlDataReader = Nothing

                        Dim sw As New Stopwatch()
                        sw.Start()

                        Try
                            datareader = commandSourceData.ExecuteReader()

                        Catch ex As Exception
                            Console.WriteLine("query reader - PMSQL: " & ExportFile & ex.Message)
                            ReportLog(conn_ios, "IOS DTE - query reader - PMSQL:" & ExportFile & ex.Message, ReportID, "ERROR")
                            success = False
                            UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: " & sw.ElapsedMilliseconds.ToString, success, 0, Attemps, SQLtoFire)
                        End Try


                        Dim QueryTime As Long = sw.ElapsedMilliseconds
                        sw.Reset()
                        sw.Start()

                        Dim path As String = ExportPath
                        If Not path.EndsWith("\") Then
                            path = path + "\"
                        End If

                        If AttachedFileDelimiter = "TAB" Then
                            AttachedFileDelimiter = vbTab
                        End If

                        Dim reportName As String = Replace(ExportFile, ".CSV", "")
                        Dim filesize As Double

                        Try
                            If Not System.IO.Directory.Exists(ExportPath) Then
                                System.IO.Directory.CreateDirectory(ExportPath)
                            End If
                        Catch ex As Exception
                            Console.WriteLine("Directory Check Failed: " & ExportFile & ex.Message)
                            ReportLog(conn_ios, "IOS DTE - Directory Check Failed: " & ExportFile & ex.Message, JobID, "ERROR")
                            success = False
                            UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: " & sw.ElapsedMilliseconds.ToString, success, 0, Attemps, SQLtoFire)
                        End Try

                        'replacing mask 
                        If success = True Then


                            Dim SchemaTable As DataTable = datareader.GetSchemaTable

                            Dim newline As String
                            If ConfigurationManager.AppSettings.Get("NBInewLineSetting") = "vbLf" Then
                                newline = vbLf
                            Else
                                newline = vbCrLf
                            End If
                            Console.WriteLine("SchemaRows:" & SchemaTable.Rows.Count)

                            Dim NumOfRows As Long = 0

                            Try

                                Using writer As StreamWriter = New StreamWriter(path + ExportFile, False)

                                    writer.Write(String.Format("{0}", datareader.GetName(0)))
                                    For i = 1 To datareader.FieldCount - 1
                                        writer.Write(AttachedFileDelimiter)
                                        writer.Write(String.Format("{0}", datareader.GetName(i)))
                                    Next

                                    writer.Write(newline)
                                    NumOfRows = NumOfRows + 1
                                    While datareader.Read

                                        For i = 0 To datareader.FieldCount - 2
                                            Dim v As Object = datareader.GetValue(i)

                                            If Not IsDBNull(v) Then
                                                Select Case SchemaTable(i)("DataTypeName").ToString
                                                    Case "real"
                                                        writer.Write(String.Format("{0}", CDbl(v)))
                                                    Case "float"
                                                        writer.Write(String.Format("{0}", CDbl(v)))
                                                    Case "datetime"
                                                        'writer.Write(String.Format("{0}", v))
                                                        writer.Write(DirectCast(v, DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                                                    Case Else
                                                        writer.Write(String.Format("{0}", v))
                                                End Select
                                            Else
                                                writer.Write(String.Format("{0}", v))
                                            End If

                                            writer.Write(AttachedFileDelimiter)
                                        Next

                                        Dim v_end As Object = datareader.GetValue(datareader.FieldCount - 1)

                                        If Not IsDBNull(v_end) Then
                                            Select Case SchemaTable(datareader.FieldCount - 1)("DataTypeName").ToString
                                                Case "real"
                                                    writer.Write(String.Format("{0}", CDbl(v_end)))
                                                Case "float"
                                                    writer.Write(String.Format("{0}", CDbl(v_end)))
                                                Case "datetime"
                                                    'writer.Write(String.Format("{0}", v))
                                                    writer.Write(DirectCast(v_end, DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                                                Case Else
                                                    writer.Write(String.Format("{0}", v_end))
                                            End Select
                                        Else
                                            writer.Write(String.Format("{0}", v_end))
                                        End If

                                        ' writer.WriteLine()
                                        writer.Write(newline)
                                        NumOfRows = NumOfRows + 1
                                        If NumOfRows Mod 1000 = 0 Then
                                            Console.WriteLine("Number of Records StreamWritten: " & NumOfRows & " ... ")
                                        End If
                                    End While

                                    writer.Flush()
                                    filesize = (writer.BaseStream.Length / 1000000)

                                End Using
                                NumOfRows = NumOfRows - 1

                                Console.WriteLine("Query Success: Elasped Time [ms]: " & QueryTime.ToString & " -- StreamWriter Finished: #" & NumOfRows & "  Elapsed Time [ms]:" & sw.ElapsedMilliseconds.ToString)
                                ReportLog(conn_ios, "IOS DTE - Query Success: Elasped Time [ms]: " & QueryTime.ToString & " -- StreamWriter Finished: #" & NumOfRows & "  Elapsed Time [ms]:" & sw.ElapsedMilliseconds.ToString, ReportID, "SUCCESS")

                                Try
                                    NBIReport_HyperLink = ConfigurationManager.AppSettings.Get("HyperLink").ToString
                                    If NBIReport_HyperLink.Contains("https:") Then
                                        NBIReport_HyperLink = NBIReport_HyperLink.Substring(InStr(NBIReport_HyperLink, "https:") - 1, InStr(NBIReport_HyperLink, "@ExportFile") - InStr(NBIReport_HyperLink, "https:")) + IIf(NBIOutputFolder <> "", NBIOutputFolder + "/", "") + ExportFile

                                    Else
                                        NBIReport_HyperLink = NBIReport_HyperLink.Substring(InStr(NBIReport_HyperLink, "http:") - 1, InStr(NBIReport_HyperLink, "@ExportFile") - InStr(NBIReport_HyperLink, "http:")) + IIf(NBIOutputFolder <> "", NBIOutputFolder + "/", "") + ExportFile

                                    End If
                                Catch ex As Exception

                                End Try

                                UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: " & QueryTime.ToString, success, NumOfRows, Attemps, SQLtoFire)

                            Catch ex As Exception
                                Console.WriteLine("StreamWriter Failed: " & ExportFile & ex.Message)
                                ReportLog(conn_ios, "IOS DTE - StreamWriter Failed: " & ExportFile & ex.Message, JobID, "ERROR")
                                success = False
                                UpdateNBIReportStatus(conn_ios, ReportsRunID, "[ms]: " & QueryTime.ToString, success, NumOfRows, Attemps, SQLtoFire)
                            End Try

                        End If

                        datareader.Close()
                        sw.Stop()

                        If emailEnabled = True Then
                            If Recipient <> "" And (EmailLinkNBI = True Or filesize >= 25.0) Then
                                Console.WriteLine("EmailLinkNBI = True or  Filesize > 25MB")
                                SendLinkMail(Recipient,
                                     Replace(ConfigurationManager.AppSettings.Get("LinkMailSubject").ToString, "@reportName", reportName),
                                     Replace(ConfigurationManager.AppSettings.Get("LinkMailBody").ToString, "@reportName", reportName),
                                     NBIReport_HyperLink,
                                     ConfigurationManager.AppSettings.Get("displayNameSMTP").ToString, ReportID)
                            ElseIf EmailLinkNBI = False And filesize <= 25.0 Then
                                Console.WriteLine("EmailLinkNBI = False And  Filesize > 25MB")
                                SendAttachmentMail(Recipient,
                                       Replace(ConfigurationManager.AppSettings.Get("NBImailSubject").ToString, "@reportName", reportName),
                                       Replace(ConfigurationManager.AppSettings.Get("NBImailBody").ToString, "@reportName", reportName),
                                       path + ExportFile,
                                       ConfigurationManager.AppSettings.Get("displayNameSMTP").ToString, ReportID)
                            Else
                                Console.WriteLine("Check the NBI report table")
                                ReportLog(conn_ios, "IOS DTE - Check report configuration table!", ReportID, "INFO")
                            End If

                        End If
                    Else
                        Console.WriteLine("No SQL to Fire!")
                        ReportLog(conn_ios, "IOS DTE - No SQL to Fire!", ReportID, "INFO")
                    End If

                End If
            End Using

        Catch ex As Exception
            WriteString_Log("NBI - ReportID: " & ReportID & vbCrLf & Now() & "    " & "NBI Report Error:  " & ex.Message)
        End Try

        Return success

    End Function

    Public Function TransferDataToExistingTable_Oracle(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, ByVal QueryTimeOut As Integer) As Boolean

        Dim success As Boolean = True


        ' Open a connection to the Source database.
        Using sourceConnection As OracleConnection = New OracleConnection(ConnString_Source)
            Try

                Console.WriteLine("Source Connection - Opening ..." & ConnString_Source)
                sourceConnection.Open()

                Console.WriteLine("Source Connection - Opened!")
            Catch ex As Exception
                Console.WriteLine("ERROR - Source Connection: " & DestinationTable & ex.Message)
                JobLog(conn_ios, "ERROR - Source Connection: " & DestinationTable & ex.Message, JobID)
                success = False
            End Try

            If success = True Then
                'drop destination table on server
                Dim sql_drop As String = "IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") "

                ' Get data from the source table as a SqlDataReader.
                Dim commandSourceData As OracleCommand = New OracleCommand(SQLString_Source, sourceConnection)

                commandSourceData.CommandTimeout = QueryTimeOut
                Dim schemareader As OracleDataReader = Nothing
                Dim datareaderSchema As OracleDataReader = Nothing
                Dim datareader As OracleDataReader = Nothing
                Dim commandSourceDataCasted As OracleCommand = Nothing

                Dim sql_create As String = Nothing
                Try
                    'get schema to create sql for table creation
                    datareaderSchema = commandSourceData.ExecuteReader(CommandBehavior.SchemaOnly)
                    '  datareaderSchema.FetchSize = 1000

                    Console.WriteLine("Source DataReader - GetSchema Start ...")
                    Dim schemaTable As DataTable = datareaderSchema.GetSchemaTable
                    sql_create = GetCreateSQL(DestinationTable, schemaTable, Nothing)
                    'schemareader.Close()
                    commandSourceDataCasted = New OracleCommand(GetCastedSQL(DestinationTable, schemaTable, SQLString_Source), sourceConnection)
                    datareader = commandSourceDataCasted.ExecuteReader()
                    'datareader.FetchSize = 1000

                    'set data reader schema
                    ' datareader = commandSourceData.ExecuteReader()
                Catch ex As Exception
                    Console.WriteLine("ERROR - Source DataReader: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "ERROR - Source DataReader: " & DestinationTable & ex.Message, JobID)
                    success = False
                End Try


                ' Open the destination connection. In the real world you would 
                ' not use SqlBulkCopy to move data from one table to the other   
                ' in the same database. This is for demonstration purposes only.
                Using destinationConnection As SqlConnection = New SqlConnection(ConnString_Dest)
                    Dim commandRowCount As New SqlCommand("IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") SELECT Total_Rows= SUM(st.row_count) FROM sys.dm_db_partition_stats st WHERE  (index_id < 2) AND object_name(object_id) = " & Chr(39) & DestinationTable & Chr(39), destinationConnection)
                    Dim countStart As Long = 0

                    Try
                        Console.WriteLine("Destination Connection - Opening ...")
                        destinationConnection.Open()
                        Console.WriteLine("Destination Connection - Opened !")
                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False
                    End Try

                    Try

                        countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar())
                        Console.WriteLine("Destination Table " & DestinationTable & " - Start Count:" & countStart)
                        Console.WriteLine("Destination Table  " & DestinationTable & " - If Not Exists - Create Process")
                        Dim commandDropData As SqlCommand = New SqlCommand(sql_drop & sql_create, destinationConnection)
                        commandDropData.ExecuteNonQuery()
                        commandDropData.Dispose()

                    Catch ex As Exception
                        Console.WriteLine("ERROR - checking existing table: " & DestinationTable & ex.Message)
                        JobLog(conn_ios, "ERROR - checking existing table: " & DestinationTable & ex.Message, JobID)
                        success = False
                    End Try

                    Dim destreader As SqlDataReader = Nothing
                    Try
                        'get schema to create sql for table creation

                        Dim commandDestData As SqlCommand = New SqlCommand("SELECT * FROM " & DestinationTable, destinationConnection)
                        commandDestData.CommandTimeout = QueryTimeOut
                        destreader = commandDestData.ExecuteReader()
                        Dim destschema As DataTable = destreader.GetSchemaTable
                        Dim sql_alter As String = Nothing

                        For Each SourceCol As DataRow In datareader.GetSchemaTable.Rows

                            'does column exist?
                            If Not ColumnExists(destreader, SourceCol("ColumnName").ToString) Then
                                'add column
                                JobLog(conn_ios, "NEW COLUMN DETECTED: " & DestinationTable & " - " & SourceCol("ColumnName").ToString, JobID)
                                Console.WriteLine(DestinationTable & " - New Column Detected In Source - Column Name: " & SourceCol("ColumnName").ToString)
                                sql_alter = sql_alter & "ALTER TABLE " & DestinationTable & " ADD [" & SourceCol("ColumnName").ToString & "] " & SQLGetType(SourceCol) & vbCrLf
                            End If

                        Next
                        destreader.Close()
                        commandDestData.Dispose()

                        If Not sql_alter Is Nothing Then
                            Dim commandDestAlter As SqlCommand = New SqlCommand(sql_alter, destinationConnection)
                            commandDestAlter.ExecuteNonQuery()
                            commandDestAlter.Dispose()
                        End If


                        'refresh schema table
                        'destreader = commandDestData.ExecuteReader()


                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False

                        If Not destreader Is Nothing Then
                            destreader.Close()
                        End If

                    End Try
                    ' Set up the bulk copy object. 
                    ' The column positions in the source data reader 

                    Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)
                        bulkCopy.DestinationTableName = DestinationTable
                        bulkCopy.BulkCopyTimeout = 1500
                        bulkCopy.BatchSize = 1000
                        bulkCopy.NotifyAfter = 1000
                        AddHandler bulkCopy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                        'column mappings
                        For Each drow As DataRow In datareader.GetSchemaTable.Rows
                            bulkCopy.ColumnMappings.Add(drow("ColumnName").ToString, drow("ColumnName").ToString)
                        Next

                        Try
                            ' Write from the source to the destination.

                            bulkCopy.WriteToServer(datareader)

                            Dim countEnd As Long = System.Convert.ToInt32(commandRowCount.ExecuteScalar())

                            Console.WriteLine("OK: " & DestinationTable & " - #" & countEnd - countStart)
                            JobLog(conn_ios, "OK: " & DestinationTable & " - #" & countEnd - countStart, JobID)
                            success = True
                        Catch ex As Exception
                            Console.WriteLine("bulk copy: " & DestinationTable & ex.Message)
                            JobLog(conn_ios, "bulk copy: " & DestinationTable & ex.Message, JobID)
                            success = False
                        Finally
                            ' Close the SqlDataReader. The SqlBulkCopy
                            ' object is automatically closed at the end
                            ' of the Using block.
                            If Not datareader Is Nothing Then
                                datareader.Close()
                            End If
                        End Try
                    End Using

                End Using
            End If
        End Using
        Return success
    End Function

    Public Function ColumnExists(ByRef reader As IDataReader, ByVal columnName As String) As Boolean
        Dim i As Integer
        For i = 0 To reader.FieldCount - 1
            If reader.GetName(i) = columnName Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function TransferDataToExistingTableDataTable_Oracle(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, Optional ByVal QueryTimeOut As Integer = 300) As Boolean

        Dim success As Boolean = True
        Dim dsSourceData As DataSet = New DataSet

        ' Open a connection to the Source database.
        Using sourceConnection As OracleConnection = New OracleConnection(ConnString_Source)
            Try
                sourceConnection.Open()
            Catch ex As Exception
                Console.WriteLine("open source connection: " & DestinationTable & ex.Message)
                JobLog(conn_ios, "open source connection: " & DestinationTable & ex.Message, JobID)
                success = False
            End Try

            If success = True Then
                'drop destination table on server
                Dim sql_drop As String = "IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") "

                ' Get data from the source table as a SqlDataReader.



                Dim commandSourceData As OracleCommand = New OracleCommand(SQLString_Source, sourceConnection)
                Dim commandAdapter As OracleDataAdapter = New OracleDataAdapter(commandSourceData)
                commandSourceData.CommandTimeout = QueryTimeOut




                'commandAdapter.ReturnProviderSpecificTypes = True
                Dim schemareader As OracleDataReader = Nothing
                Dim datareader As OracleDataReader = Nothing
                Dim sql_CastedStatement As String = Nothing
                Dim sql_create As String = Nothing
                Try
                    'get schema to create sql for table creation

                    datareader = commandSourceData.ExecuteReader(CommandBehavior.SchemaOnly)
                    Dim schemaTable As DataTable = datareader.GetSchemaTable
                    sql_create = GetCreateSQL(DestinationTable, schemaTable, Nothing)



                    Dim commandSourceDataCasted As OracleCommand = New OracleCommand(GetCastedSQL(DestinationTable, schemaTable, SQLString_Source), sourceConnection)
                    Dim commandAdapterCasted As OracleDataAdapter = New OracleDataAdapter(commandSourceDataCasted)
                    commandSourceDataCasted.CommandTimeout = QueryTimeOut
                    'commandSourceDataCasted.FetchSize = 1000
                    datareader.Close()

                    commandAdapterCasted.Fill(dsSourceData)

                    commandAdapterCasted.Dispose()
                    commandSourceDataCasted.Dispose()

                    Console.WriteLine(" Rows Read:  " & dsSourceData.Tables(0).Rows.Count)

                Catch ex As Exception
                    Console.WriteLine("query reader: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "query reader: " & DestinationTable & ex.Message, JobID)
                    success = False
                End Try


                ' Open the destination connection. In the real world you would 
                ' not use SqlBulkCopy to move data from one table to the other   
                ' in the same database. This is for demonstration purposes only.
                Using destinationConnection As SqlConnection = New SqlConnection(ConnString_Dest)
                    Dim commandRowCount As New SqlCommand("IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") SELECT Total_Rows= SUM(st.row_count) FROM sys.dm_db_partition_stats st WHERE  (index_id < 2) AND object_name(object_id) = " & Chr(39) & DestinationTable & Chr(39), destinationConnection)
                    Dim countStart As Long = 0

                    Try
                        destinationConnection.Open()

                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False
                    End Try

                    Try
                        countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar())

                        Dim commandDropData As SqlCommand = New SqlCommand(sql_drop & sql_create, destinationConnection)
                        commandDropData.ExecuteNonQuery()
                        commandDropData.Dispose()

                    Catch ex As Exception
                        Console.WriteLine("checking existing table: " & DestinationTable & ex.Message)
                        JobLog(conn_ios, "checking existing table: " & DestinationTable & ex.Message, JobID)
                        success = False
                    End Try

                    Try
                        ' Dim commandCreateTable As SqlCommand = New SqlCommand(sql_create, destinationConnection)
                        ' commandCreateTable.ExecuteNonQuery()
                        ' commandCreateTable.Dispose()
                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False
                    End Try

                    ''
                    Dim destreader As SqlDataReader = Nothing
                    Try
                        'get schema to create sql for table creation

                        Dim commandDestData As SqlCommand = New SqlCommand("SELECT TOP 1 * FROM " & DestinationTable, destinationConnection)
                        commandDestData.CommandTimeout = QueryTimeOut
                        destreader = commandDestData.ExecuteReader()
                        Dim destschema As DataTable = destreader.GetSchemaTable
                        Dim sql_alter As String = Nothing

                        For Each SourceCol As DataColumn In dsSourceData.Tables(0).Columns

                            'does column exist?
                            If Not ColumnExists(destreader, SourceCol.ColumnName) Then
                                'add column
                                JobLog(conn_ios, "NEW COLUMN DETECTED: " & DestinationTable & " - " & SourceCol.ColumnName, JobID)
                                Console.WriteLine(DestinationTable & " - New Column Detected In Source - Column Name: " & SourceCol.ColumnName)
                                sql_alter = sql_alter & "ALTER TABLE " & DestinationTable & " ADD [" & SourceCol.ColumnName & "] " & SQLGetType(SourceCol) & vbCrLf
                            End If

                        Next
                        destreader.Close()
                        commandDestData.Dispose()

                        If Not sql_alter Is Nothing Then
                            Dim commandDestAlter As SqlCommand = New SqlCommand(sql_alter, destinationConnection)
                            commandDestAlter.ExecuteNonQuery()
                            commandDestAlter.Dispose()
                        End If


                        'refresh schema table
                        'destreader = commandDestData.ExecuteReader()


                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False

                        If Not destreader Is Nothing Then
                            destreader.Close()
                        End If

                    End Try
                    ' Set up the bulk copy object. 
                    ' The column positions in the source data reader 


                    ' Set up the bulk copy object. 
                    ' The column positions in the source data reader 
                    ' match the column positions in the destination table, 
                    ' so there is no need to map columns.
                    Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)
                        bulkCopy.DestinationTableName = DestinationTable
                        bulkCopy.BulkCopyTimeout = 100
                        bulkCopy.BatchSize = 1000
                        bulkCopy.NotifyAfter = 1000
                        AddHandler bulkCopy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                        'column mappings
                        For Each SourceCol As DataColumn In dsSourceData.Tables(0).Columns
                            bulkCopy.ColumnMappings.Add(SourceCol.ColumnName, SourceCol.ColumnName)
                        Next

                        Try
                            ' Write from the source to the destination.

                            bulkCopy.WriteToServer(dsSourceData.Tables(0))

                            Dim countEnd As Long = System.Convert.ToInt32(commandRowCount.ExecuteScalar())

                            Console.WriteLine("OK: " & DestinationTable & " - #" & countEnd - countStart)
                            JobLog(conn_ios, "OK: " & DestinationTable & " - #" & countEnd - countStart, JobID)
                            success = True
                        Catch ex As Exception
                            Console.WriteLine("bulk copy: " & DestinationTable & ex.Message)
                            JobLog(conn_ios, "bulk copy: " & DestinationTable & ex.Message, JobID)
                            success = False
                        Finally
                            ' Close the SqlDataReader. The SqlBulkCopy
                            ' object is automatically closed at the end
                            ' of the Using block.
                            If Not datareader Is Nothing Then
                                datareader.Close()
                            End If
                        End Try
                    End Using
                    destinationConnection.Close()
                End Using
            End If
            sourceConnection.Close()
        End Using
        Return success
    End Function
    Public Function TransferDataToExistingTableDataTable(ByVal ConnString_Source As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String) As Boolean

        Dim success As Boolean = True
        Dim dsSourceData As DataSet = New DataSet

        ' Open a connection to the Source database.
        Using sourceConnection As OdbcConnection = New OdbcConnection(ConnString_Source)
            Try
                sourceConnection.Open()
            Catch ex As Exception
                Console.WriteLine("open source connection: " & DestinationTable & ex.Message)
                success = False
            End Try

            If success = True Then
                'drop destination table on server
                Dim sql_drop As String = "IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") "

                ' Get data from the source table as a SqlDataReader.
                Dim commandSourceData As OdbcCommand = New OdbcCommand(SQLString_Source, sourceConnection)
                Dim commandAdapter As OdbcDataAdapter = New OdbcDataAdapter(commandSourceData)

                Dim schemareader As OdbcDataReader = Nothing
                Dim datareader As OdbcDataReader = Nothing

                Dim sql_create As String = Nothing
                Try
                    'get schema to create sql for table creation
                    commandAdapter.Fill(dsSourceData)
                    datareader = commandSourceData.ExecuteReader(CommandBehavior.KeyInfo)
                    sql_create = GetCreateSQL(DestinationTable, datareader.GetSchemaTable, Nothing)
                    datareader.Close()

                Catch ex As Exception
                    Console.WriteLine("query reader: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "query reader: " & DestinationTable & ex.Message, JobID)
                    success = False
                End Try


                ' Open the destination connection. In the real world you would 
                ' not use SqlBulkCopy to move data from one table to the other   
                ' in the same database. This is for demonstration purposes only.
                Using destinationConnection As SqlConnection = New SqlConnection(ConnString_Dest)
                    Dim commandRowCount As New SqlCommand("IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") SELECT COUNT(*) FROM " & DestinationTable, destinationConnection)
                    Dim countStart As Long = 0

                    Try
                        destinationConnection.Open()

                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False
                    End Try

                    Try
                        countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar())

                        Dim commandDropData As SqlCommand = New SqlCommand(sql_drop & sql_create, destinationConnection)
                        commandDropData.ExecuteNonQuery()
                        commandDropData.Dispose()

                    Catch ex As Exception
                        Console.WriteLine("checking existing table: " & DestinationTable & ex.Message)
                        JobLog(conn_ios, "checking existing table: " & DestinationTable & ex.Message, JobID)
                        success = False
                    End Try

                    Try
                        ' Dim commandCreateTable As SqlCommand = New SqlCommand(sql_create, destinationConnection)
                        ' commandCreateTable.ExecuteNonQuery()
                        ' commandCreateTable.Dispose()
                    Catch ex As Exception
                        JobLog(conn_ios, ex.Message, JobID)
                        success = False
                    End Try
                    ' Set up the bulk copy object. 
                    ' The column positions in the source data reader 
                    ' match the column positions in the destination table, 
                    ' so there is no need to map columns.
                    Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)
                        bulkCopy.DestinationTableName = DestinationTable
                        bulkCopy.BulkCopyTimeout = 100
                        bulkCopy.BatchSize = 100
                        bulkCopy.NotifyAfter = 100
                        AddHandler bulkCopy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                        Try
                            ' Write from the source to the destination.

                            bulkCopy.WriteToServer(dsSourceData.Tables(0))

                            Dim countEnd As Long = System.Convert.ToInt32(commandRowCount.ExecuteScalar())

                            Console.WriteLine("OK: " & DestinationTable & " - #" & countEnd - countStart)
                            JobLog(conn_ios, "OK: " & DestinationTable & " - #" & countEnd - countStart, JobID)
                            success = True
                        Catch ex As Exception
                            Console.WriteLine("bulk copy: " & DestinationTable & ex.Message)
                            JobLog(conn_ios, "bulk copy: " & DestinationTable & ex.Message, JobID)
                            success = False
                        Finally
                            ' Close the SqlDataReader. The SqlBulkCopy
                            ' object is automatically closed at the end
                            ' of the Using block.
                            If Not datareader Is Nothing Then
                                datareader.Close()
                            End If
                        End Try
                    End Using
                    destinationConnection.Close()
                End Using
            End If
            sourceConnection.Close()
        End Using
        Return success
    End Function

    Public Function TransferDataTableToSQL(ByVal dt As DataTable, ByVal ConnString_Dest As String, ByVal DestinationTable As String) As Boolean

        Dim success As Boolean = True

        ' Open a connection to the Source database.


        Dim sql_create As String = GetCreateFromDataTableSQL(DestinationTable, dt)
        Dim sql_check As String = "IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") "
        Using destinationConnection As SqlConnection = New SqlConnection(ConnString_Dest)

            Dim commandRowCount As New SqlCommand("IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " & Chr(39) & DestinationTable & Chr(39) & ") SELECT COUNT(*) FROM " & DestinationTable, destinationConnection)
            Dim countStart As Long = 0

            Try
                destinationConnection.Open()

            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                success = False
            End Try

            Try

                Dim commandCheckCreate As SqlCommand = New SqlCommand(sql_check & sql_create, destinationConnection)
                commandCheckCreate.ExecuteNonQuery()
                commandCheckCreate.Dispose()

                countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar())


            Catch ex As Exception
                Console.WriteLine("checking existing table: " & DestinationTable & ex.Message)
                JobLog(conn_ios, "checking existing table: " & DestinationTable & ex.Message, JobID)
                success = False
            End Try

            Try
                ' Dim commandCreateTable As SqlCommand = New SqlCommand(sql_create, destinationConnection)
                ' commandCreateTable.ExecuteNonQuery()
                ' commandCreateTable.Dispose()
            Catch ex As Exception
                JobLog(conn_ios, ex.Message, JobID)
                success = False

            End Try
            ' Set up the bulk copy object. 
            ' The column positions in the source data reader 
            ' match the column positions in the destination table, 
            ' so there is no need to map columns.
            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)

                bulkCopy.DestinationTableName = DestinationTable
                bulkCopy.BulkCopyTimeout = 100
                bulkCopy.BatchSize = 100
                bulkCopy.NotifyAfter = 100
                AddHandler bulkCopy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                Try
                    ' Write from the source to the destination.

                    bulkCopy.WriteToServer(dt)

                    Dim countEnd As Long = System.Convert.ToInt32(commandRowCount.ExecuteScalar())

                    Console.WriteLine("OK: " & DestinationTable & " - #" & countEnd - countStart)
                    JobLog(conn_ios, "OK: " & DestinationTable & " - #" & countEnd - countStart, JobID)
                    success = True
                Catch ex As Exception
                    Console.WriteLine("bulk copy: " & DestinationTable & ex.Message)
                    JobLog(conn_ios, "bulk copy: " & DestinationTable & ex.Message, JobID)
                    success = False
                Finally
                    ' Close the SqlDataReader. The SqlBulkCopy
                    ' object is automatically closed at the end
                    ' of the Using block.

                End Try
            End Using
            destinationConnection.Close()
        End Using


        Return success
    End Function
    Public Function GetCreateSQL(ByVal tableName As String, ByVal schema As DataTable, ByVal primaryKeys() As String) As String
        Dim sql As String = "CREATE TABLE " + tableName + " ("

        ' columns
        Dim colduplicates As Integer = 0
        For Each column As DataRow In schema.Rows
            'If (Not (schema.Columns.Contains("IsHidden") And CBool(column("IsHidden")))) Then

            If Split(sql, " " & column("ColumnName").ToString() & " ").Length > 1 Then
                sql = sql + Chr(34) + column("ColumnName").ToString() + CStr(Split(sql, column("ColumnName").ToString()).Length - 1) + Chr(34) + " " + SQLGetType(column) & ", "

            Else
                sql = sql + Chr(34) + column("ColumnName").ToString() + Chr(34) + " " + SQLGetType(column) & ", "
            End If

            'If (schema.Columns.Contains("AllowDBNull") And CBool(column("AllowDBNull") = False)) Then
            ' sql += " NOT NULL"
            ' End If
            ' sql += ",\n"
            ' End If
        Next

        sql = sql.TrimEnd(New Char() {",", "\n", " ", ", "})


        ' primary keys
        Dim primkeys As String = ""

        For Each dr As DataRow In schema.Rows
            If (nZ(dr("IsKey"), False)) Or (Not dr("AllowDBNull")) Then
                primkeys = primkeys + Chr(34) + dr("ColumnName").ToString + Chr(34) + " Asc, "
            End If
        Next
        If primkeys <> "" Then
            primkeys = primkeys.TrimEnd(" ").TrimEnd(",")
            primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED (" + primkeys + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" &
                        ") ON [PRIMARY] "
        End If

        'If (tableName.Contains("_WCEL") Or tableName.Contains("MNC1_RAW")) And Not tableName.Contains("_LCG") Then
        '    primkeys = ", CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[RNC_ID] Asc, " & _
        '    "[WBTS_ID] Asc, " & _
        '    "[WCEL_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("_LCG_") Then
        '    primkeys = ", CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[RNC_ID] Asc, " & _
        '    "[WBTS_ID] Asc, " & _
        '    "[LCG_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("_WBTS") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[RNC_ID] Asc, " & _
        '    "[WBTS_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("_RNC") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[RNC_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("_SERVT") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[MGW_ID] Asc, " & _
        '    "[SERV_TYPE_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("_UNITID") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[RNC_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("_UNIT3") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[MGW_ID] Asc, " & _
        '    "[UNIT_TYPE_ID] Asc, " & _
        '    "[UNIT_INDEX_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("RSG_PS") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[SGSN_GID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("_MSC_") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[MSC_ID] Asc, " & _
        '    "[PERIOD_START_TIME] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'ElseIf tableName.Contains("UTP_COMMON_OBJECTS") Then
        '    primkeys = " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED " & _
        '    "(" & _
        '    "[CO_GID] Asc " & _
        '    ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & _
        '    ") ON [PRIMARY] "
        'Else
        '    sql = sql + ")"
        'End If

        If primkeys <> "" Then
            sql = sql + primkeys
        Else
            sql = sql + ")"
        End If


        'Dim pk As String = " CONSTRAINT PK_" + tableName + " PRIMARY KEY CLUSTERED ("
        'Dim hasKeys As Boolean = False '(Not primaryKeys Is Nothing) And primaryKeys.Length > 0
        'If hasKeys = True Then
        ' ' user defined keys
        ' For Each key As Integer In primaryKeys
        'pk = pk + schema.Rows(key)("ColumnName").ToString() + ", "
        'Next

        'Else
        ' check schema for keys
        'Dim keys As String = String.Join(", ", GetPrimaryKeys(schema))
        'pk = pk + keys
        'hasKeys = keys.Length > 0
        'End If

        'pk = pk.TrimEnd(New Char() {",", "\n"})
        'If (hasKeys) Then
        ' sql = sql + pk + ")"
        'End If



        Return sql
    End Function
    Public Function GetCastedSQL(ByVal tableName As String, ByVal schema As DataTable, ByVal OriginalSql As String) As String
        Dim sql As String = ""

        ' columns
        Dim colduplicates As Integer = 0
        For Each column As DataRow In schema.Rows
            sql = sql + SQLCastType(column("ColumnName").ToString, column("DataType"), nZ(column("NumericScale"), 0), CBool(column("AllowDBNull"))) + " ,"
        Next
        sql = sql.TrimEnd(",")

        If Replace(OriginalSql, " ", "").StartsWith("SELECT*FROM") Then
            OriginalSql = Replace(OriginalSql, "*", sql)
        End If



        Return OriginalSql
    End Function
    Public Function GetCreateSQL_NoPK(ByVal tableName As String, ByVal schema As DataTable, ByVal primaryKeys() As String) As String
        Dim sql As String = "CREATE TABLE " + tableName + " ("

        ' columns
        Dim colduplicates As Integer = 0
        For Each column As DataRow In schema.Rows

            If Split(sql, " " & column("ColumnName").ToString() & " ").Length > 1 Then
                sql = sql + Chr(34) + column("ColumnName").ToString() + Chr(34) + CStr(Split(sql, column("ColumnName").ToString()).Length - 1) + " " + SQLGetType(column) & ", "

            Else
                sql = sql + Chr(34) + column("ColumnName").ToString() + Chr(34) + " " + SQLGetType(column) & ", "
            End If

        Next

        sql = sql.TrimEnd(New Char() {",", "\n", " ", ", "})

        sql = sql + ")"


        Return sql
    End Function
    Public Function GetCreateFromDataTableSQL(ByVal tableName As String, ByVal table As DataTable) As String
        Dim sql As String = "CREATE TABLE [" + tableName + "] (" + Chr(13)
        ' columns
        For Each column As DataColumn In table.Columns
            sql += Chr(34) + column.ColumnName + Chr(34) + " " + SQLGetType(column) + "," + Chr(13)
        Next
        sql = sql.TrimEnd(New Char() {",", Chr(13)}) + Chr(13)

        'primary(keys)
        If (table.PrimaryKey.Length > 0) Then
            sql += " CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED ("
            For Each column As DataColumn In table.PrimaryKey
                sql += Chr(34) + column.ColumnName + Chr(34) + ","
            Next
            sql = sql.TrimEnd(New Char() {","}) + "))" + Chr(13)
        End If

        If ((table.PrimaryKey.Length = 0) And (Not sql.EndsWith(")"))) Then
            sql += ")"
        End If

        Return sql

    End Function

    Public Function GetPrimaryKeys(ByVal schema As DataTable) As String()
        Dim keys As List(Of String) = New List(Of String)

        For Each column As DataRow In schema.Rows
            If (schema.Columns.Contains("IsKey") And CBool(column("IsKey"))) Then
                keys.Add(column("ColumnName").ToString())
            End If

        Next

        Return keys.ToArray()
    End Function
    Public Function SQLGetType(ByVal schemaRow As DataRow) As String
        Return SQLGetType(schemaRow("DataType"),
                            CInt(nZ(schemaRow("ColumnSize"), 0).ToString()),
                            CInt(nZ(schemaRow("NumericPrecision"), 0.ToString())),
                            CInt(nZ(schemaRow("NumericScale"), 0).ToString()), CBool(schemaRow("AllowDBNull")))

    End Function
    Private Function nZ(ByVal source As Object, ByVal defaultValue As String) As String
        'this code is meant to translate a Mapinfo NULL result into a defaultValue
        ' otherwise we get exceptions too easily

        If (source Is DBNull.Value) Then
            Return defaultValue
        End If
        Return source.ToString
    End Function
    ' Return T-SQL data type definition, based on schema definition for a column
    Public Function SQLGetType(ByVal type As Object, ByVal columnSize As Integer, ByVal numericPrecision As Integer, ByVal numericScale As Integer, Optional IsAllowDBNull As Boolean = True) As String
        Select Case (type.ToString())
            Case "System.String"
                ' return "VARCHAR(" + ((columnSize = -1) ? 255 : columnSize) + ")"
                If columnSize > 7999 Or columnSize < 1 Then
                    Return "VARCHAR(MAX)"
                Else
                    Return "VARCHAR(" & columnSize & ")"
                End If


            Case "System.Decimal"
                If IsAllowDBNull = False Then
                    Return "BIGINT"
                ElseIf (numericScale > 0) Then
                    Return "FLOAT"
                ElseIf (numericPrecision > 10) Then
                    Return "BIGINT"
                Else
                    Return "INT"
                End If

            Case "System.Double"
                Return "FLOAT"

            Case "System.Single"
                Return "FLOAT"

            Case "System.Int64"
                Return "BIGINT"

            Case "System.Int16"
                Return "INT"

            Case "System.Int32"
                Return "INT"

            Case "System.DateTime"
                Return "DATETIME"
            Case "System.Boolean"
                Return "BIT"
            Case "System.Byte"
                Return "TINYINT"
            Case "System.Guid"
                Return "UNIQUEIDENTIFIER"
            Case Else
                Return Nothing
        End Select
    End Function
    Public Function SQLCastType(ByVal columnname As String, ByVal type As Object, ByVal numericScale As Integer, Optional IsAllowDBNull As Boolean = True) As String
        Select Case (type.ToString())
            Case "System.Decimal"
                'If IsAllowDBNull = False Then
                '    Return "CAST(" & columnname & " as DECIMAL(18,0))" & columnname
                'ElseIf (numericScale > 0) Then
                '    Return "CAST(" & columnname & " as DECIMAL(12,5)) " & columnname
                'Else
                '    Return "ROUND(" & columnname & ") " & columnname
                'End If
                If IsAllowDBNull = False Then
                    Return "CAST(" & columnname & " as NUMBER(19))" & columnname
                ElseIf columnname.EndsWith("_ID") Or columnname.EndsWith("_GID") Then
                    Return "CAST(" & columnname & " as NUMBER(19))" & columnname
                ElseIf (numericScale > 0) Then
                    Return "CAST(" & columnname & " as BINARY_DOUBLE) " & columnname
                Else
                    Return "ROUND(" & columnname & ") " & columnname
                End If
                'If IsAllowDBNull = False Then
                '    Return "ROUND(" & columnname & ") " & columnname
                'ElseIf (numericScale > 0) Then
                '    Return "ROUND(" & columnname & ",5) " & columnname
                'Else
                '    Return "ROUND(" & columnname & ") " & columnname
                'End If
            Case Else
                Return columnname
        End Select
    End Function
    Public Function SQLGetType(ByVal column As DataColumn) As String
        Return SQLGetType(column.DataType, column.MaxLength, 10, 2)

    End Function

    Private Sub OnSqlRowsCopied(ByVal sender As Object,
        ByVal args As SqlRowsCopiedEventArgs)
        Console.WriteLine("Copied {0} so far...", args.RowsCopied)
    End Sub
    Public Sub WriteString_Log(ByVal text2append As String)
        Try
            Dim FILE_NAME As String = Environment.CurrentDirectory & "\session.log"

            If File.Exists(FILE_NAME) Then
                Dim myFile As New FileInfo(FILE_NAME)
                Dim sizeInBytes As Long = myFile.Length

                If sizeInBytes > 100000000 Then
                    My.Computer.FileSystem.RenameFile(FILE_NAME, "session_" & Format(Date.Now, "ddMMyy") & ".log")
                End If
            End If

            Static LogFileLock As New Object()
            SyncLock LogFileLock

                File.AppendAllText(FILE_NAME, text2append & vbCrLf)
                ' File.SetAttributes(FILE_NAME, FileAttributes.Hidden)

            End SyncLock
        Catch
        End Try
    End Sub

    Public Function SendReportMail(sourceConnection As SqlConnection, reportName As String, recipient As String, reportFilePath As String, ReportID As Integer) As Integer

        'translate path to network path


        Dim sqlQuery As System.Text.StringBuilder = New System.Text.StringBuilder()
        sqlQuery.AppendLine("EXEC [dbo].[sp_SendMail_ReportCreated] " & Chr(39) & reportName & Chr(39) & "," & Chr(39) & recipient & Chr(39) & "," & Chr(39) & reportFilePath & Chr(39) & ",1")
        Console.WriteLine("Send Mail Query: " & sqlQuery.ToString)


        Dim commandSourceData As SqlCommand = New SqlCommand(sqlQuery.ToString, sourceConnection)
        commandSourceData.CommandTimeout = 100

        Try
            commandSourceData.ExecuteNonQuery()
            ReportLog(conn_ios, "Send NBI report Email:" & reportName & " - " & recipient, ReportID, "SUCCESS")
        Catch ex As Exception
            Console.WriteLine("ERROR: Send NBI report Email: " & ex.Message)
            ReportLog(conn_ios, "ERROR: Send NBI report Email:" & reportName & " - " & ex.Message, ReportID, "ERROR")
        End Try

    End Function

    Public Function SendAttachmentMail(ByVal recipient As String, ByVal subject As String, ByVal body As String, ByVal path As String, ByVal displayNameM As String, ByVal reportid As Int16) As Boolean
        Dim sent As Boolean = True

        Try
            ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf AcceptAllCertifications)

            'Init smtp client
            Dim _smtpClient = New SmtpClient()

            'get the sender mail address
            'Dim credentials As NetworkCredential = _smtpClient.Credentials
            'Dim sender As String = credentials.UserName.ToString
            'Dim pss As String = credentials.Password.ToString

            Console.WriteLine("Start SMTP Client...")
            Console.WriteLine("recipient:" & recipient)
            Console.WriteLine("subject:" & subject)
            Console.WriteLine("body:" & body)
            Console.WriteLine("displayNameM:" & displayNameM)


            'create message
            Dim _mailMessage = New MailMessage With
                            {
                            .From = New MailAddress(ConfigurationManager.AppSettings.Get("FromEmailAddress").ToString, displayNameM),
                            .Subject = subject,
                            .Body = body,
                            .IsBodyHtml = True
                            }
            _mailMessage.Attachments.Add(New Attachment(path))
            Console.WriteLine("SMTP: Message Created, From: " & _mailMessage.From.Address.ToString)

            'add recipient

            WriteString_Log(Now() & "    " & "Email From: " & _mailMessage.From.ToString)
            WriteString_Log(Now() & "    " & "Email Subject: " & _mailMessage.Subject.ToString)
            WriteString_Log(Now() & "    " & "Email recipient: " & recipient.ToString)

            For Each r As String In recipient.Split(";")
                _mailMessage.To.Add(r)
            Next
            'send message
            _smtpClient.Send(_mailMessage)

            WriteString_Log(Now() & "    " & "Mail sent !")
            Console.WriteLine("Mail with Link Sent For Report: " & displayNameM)
            ReportLog(conn_ios, "IOS DTE - Report SMTP Sent - " & recipient.ToString, reportid, "SUCCESS")
            'disconnect
            _smtpClient.Dispose()
        Catch ex As Exception
            WriteString_Log(Now() & "    " & "Mail failed to send: " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
            ReportLog(conn_ios, "IOS DTE - Report SMTP Failed - " & ex.Message, reportid, "ERROR")
            Console.WriteLine("Mail failed to send: " & ex.Message.ToString
                              )
            sent = False
        End Try

        Return sent
    End Function

    Public Function AcceptAllCertifications(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function


    Public Function SendLinkMail(ByVal recipient As String, ByVal subject As String, ByVal body As String, ByVal bodyHyperLink As String, ByVal displayNameM As String, ByVal reportid As Int16) As Boolean
        Dim sent As Boolean = True
        Try
            'Init smtp client
            Console.WriteLine("Start SMTP Client...")
            Console.WriteLine("recipient:" & recipient)
            Console.WriteLine("subject:" & subject)
            Console.WriteLine("body:" & body)
            Console.WriteLine("bodyHyperLink:" & bodyHyperLink)
            Console.WriteLine("displayNameM:" & displayNameM)


            ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf AcceptAllCertifications)

            Dim _smtpClient = New SmtpClient()

            'get the sender mail address
            'Dim credentials As NetworkCredential = _smtpClient.Credentials
            'Dim sender As String = credentials.UserName.ToString
            'create message
            Dim _mailMessage = New MailMessage With
                            {
                            .From = New MailAddress(ConfigurationManager.AppSettings.Get("FromEmailAddress").ToString, displayNameM),
                            .Subject = subject,
                            .Body = body + vbCr + vbCrLf + bodyHyperLink,
                            .IsBodyHtml = True
                            }
            Console.WriteLine("SMTP: Message Created, From: " & _mailMessage.From.Address.ToString)

            WriteString_Log(Now() & "    " & "Email From: " & _mailMessage.From.ToString)
            WriteString_Log(Now() & "    " & "Email Subject: " & _mailMessage.Subject.ToString)
            WriteString_Log(Now() & "    " & "Email recipient: " & recipient.ToString)


            'add recipient
            For Each r As String In recipient.Split(";")
                _mailMessage.To.Add(r)
            Next

            'send message
            _smtpClient.Send(_mailMessage)
            'disconnect
            _smtpClient.Dispose()

            WriteString_Log(Now() & "    " & "Mail sent !")
            Console.WriteLine("Mail with Link Sent For Report: " & displayNameM)
            ReportLog(conn_ios, "IOS DTE - Report SMTP Sent - " & recipient.ToString, reportid, "SUCCESS")
        Catch ex As Exception
            WriteString_Log(Now() & "    " & "Mail failed to send: " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
            ReportLog(conn_ios, "IOS DTE - Report SMTP Failed - " & ex.Message, reportid, "ERROR")
            Console.WriteLine("Mail failed to send: " & ex.Message.ToString)
            sent = False
        End Try

        Return sent
    End Function

    Public Sub DataTableToCSV(ByRef dt As DataTable, ByVal fn As String, ByVal separator As String)
        Try


            If IO.File.Exists(fn) Then
                IO.File.Delete(fn)
            End If

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
            For i = 0 To dt.Columns.Count - 1
                sb.Append(dt.Columns(i).ColumnName)
                If i < dt.Columns.Count - 1 Then
                    sb.Append(separator)
                End If
            Next
            sb.AppendLine()

            Dim j As Int32 = 0
            For Each dr As DataRow In dt.Rows


                For i = 0 To dt.Columns.Count - 1
                    Dim val = dr(i)
                    If IsDBNull(val) Then
                        ' Handle NULLs: Append nothing or a placeholder like ""
                        sb.Append("")
                    ElseIf dt.Columns(i).DataType = GetType(System.DateTime) Then
                        sb.Append(DirectCast(val, DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                    Else
                        sb.Append(val.ToString)
                    End If

                    If i < dt.Columns.Count - 1 Then
                        sb.Append(separator)
                    End If
                Next
                sb.AppendLine()


                If sb.Length > 100000000 Or j = dt.Rows.Count - 1 Then

                    Using sw As StreamWriter = New StreamWriter(fn, True)
                        Const buffersize As Int32 = 1000000
                        Dim buffer(buffersize) As Char

                        For i = 0 To sb.Length - 1 Step buffersize
                            Dim cnt As Int32 = Math.Min(buffersize, sb.Length - i)
                            sb.CopyTo(i, buffer, 0, cnt)
                            sw.Write(buffer, 0, cnt)
                        Next
                        buffer = Nothing
                        GC.Collect()

                    End Using
                    sb.Clear()
                End If

                j = j + 1
            Next




        Catch ex As Exception
            Console.WriteLine(Now() & "    " & " Failed writing to CSV : " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
            WriteString_Log(Now() & "    " & " Failed writing to CSV : " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
        End Try
    End Sub


    Private Async Function TransferDataFromRestAPI(ByVal apiConfigXml As String, ByVal SQLString_Source As String, ByVal ConnString_Dest As String, ByVal DestinationTable As String, ByVal QueryTimeOut As Integer) As Threading.Tasks.Task(Of Boolean)
        Try
            Dim dsTicketsConfig As DataSet = Nothing
            Dim transferSuccess As Boolean

            Using sr As New System.IO.StringReader(apiConfigXml)
                dsTicketsConfig = New DataSet
                dsTicketsConfig.ReadXml(sr)
            End Using

            Dim dtAuth As DataTable = dsTicketsConfig.Tables(0)
            Dim authTokenUrl As String = dtAuth.Rows(0)("AuthTokenUrl")
            Dim apiBaseUrl As String = dtAuth.Rows(0)("APIBaseUrl")
            Dim userName As String = dtAuth.Rows(0)("UserName")
            Dim pswd As String = dtAuth.Rows(0)("Password")
            Dim clientID As String = dtAuth.Rows(0)("ClientID")
            Dim clientSecret As String = dtAuth.Rows(0)("ClientSecret")
            Dim methodType As String = dtAuth.Rows(0)("MethodType")
            Dim methodParams() As String = dtAuth.Rows(0)("MethodParams").ToString.Split("|")
            Dim priority As String = Nothing


            Dim actualUrl As String = Nothing
            Dim location As String = "%22MBTS%22%2C%22BBU%22"
            Dim startDate As String = Nothing
            Dim endDate As String = Nothing
            Dim accessToken As String = Nothing
            Dim refreshToken As String = Nothing

            Using client As New HttpClient()

                'POST request to get access token/refresh token
                client.BaseAddress = New Uri(authTokenUrl)
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim reqBodyContent = New FormUrlEncodedContent(
                {
                    New KeyValuePair(Of String, String)("grant_type", "password"),
                    New KeyValuePair(Of String, String)("client_id", clientID),
                    New KeyValuePair(Of String, String)("client_secret", clientSecret),
                    New KeyValuePair(Of String, String)("username", userName),
                    New KeyValuePair(Of String, String)("password", pswd)
                })
                Console.WriteLine("PostAsync")
                Dim token = Await client.PostAsync(client.BaseAddress, reqBodyContent)
                Console.WriteLine("PostAsync Finished")
                If token.StatusCode = HttpStatusCode.OK Then
                    Dim tokenContent = token.Content.ReadAsStringAsync()
                    Dim jTokenData As JObject = TryCast(JsonConvert.DeserializeObject(tokenContent.Result.ToString), JObject)
                    accessToken = jTokenData.GetValue("access_token").ToString
                    refreshToken = jTokenData.GetValue("refresh_token").ToString
                Else
                    Console.WriteLine(token.StatusCode.ToString)
                End If

                '****************************************************************************

                'POST subsequent request to get access token/refresh token
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim subReqBodyContent = New FormUrlEncodedContent(
                {
                    New KeyValuePair(Of String, String)("grant_type", "refresh_token"),
                    New KeyValuePair(Of String, String)("client_id", clientID),
                    New KeyValuePair(Of String, String)("client_secret", clientSecret),
                    New KeyValuePair(Of String, String)("refresh_token", refreshToken)
                })
                Dim subToken = Await client.PostAsync(client.BaseAddress, subReqBodyContent)
                If subToken.StatusCode = HttpStatusCode.OK Then
                    Dim subTokenContent = subToken.Content.ReadAsStringAsync()
                    Dim jTokenData As JObject = TryCast(JsonConvert.DeserializeObject(subTokenContent.Result.ToString), JObject)
                    accessToken = jTokenData.GetValue("access_token").ToString
                    refreshToken = jTokenData.GetValue("refresh_token").ToString
                End If
                '****************************************************************************
            End Using

            Dim cols() As String = dtAuth.Rows(0)("ColumnName").ToString.Split("|")
            Dim replaceCols() As String = dtAuth.Rows(0)("ReplacedColumn").ToString.Split("|")
            Dim colsOrdinal() As String = dtAuth.Rows(0)("ColumnOrdinal").ToString.Split("|")

            Using client As New HttpClient()

                startDate = CDate(DateAdd(DateInterval.Day, -100, Now())).ToString("yyyy-MM-dd")
                endDate = CDate(Now()).ToString("yyyy-MM-dd")
                actualUrl = apiBaseUrl & methodType & "?" & methodParams(0) & "=" & location & "&" & methodParams(1) & "=" & startDate & "&" & methodParams(2) & "=" & endDate & "&" & methodParams(3) & "=" & priority

                'GET request to get access tickets data
                client.BaseAddress = New Uri(apiBaseUrl)
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)
                client.DefaultRequestHeaders.Add("cis", location)
                client.DefaultRequestHeaders.Add("start_date", startDate)
                client.DefaultRequestHeaders.Add("end_date", endDate)
                client.DefaultRequestHeaders.Add("priority", priority)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/xml"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim response = Await client.GetAsync(actualUrl)
                If response.StatusCode = HttpStatusCode.OK Then
                    Dim tickesData = Await response.Content.ReadAsStringAsync()
                    Dim taskResponse As String = Await client.GetStringAsync(actualUrl)
                    Dim ds As DataSet = XMLToDataSet(tickesData, "")
                    If ds IsNot Nothing AndAlso ds.Tables.Count <> 0 Then
                        Dim dtRest = ds.Tables(0)
                        For iCntr = 0 To replaceCols.Count - 1
                            dtRest.Columns(iCntr).ColumnName = CStr(replaceCols(iCntr))
                            dtRest.AcceptChanges()
                        Next

                        transferSuccess = TransferDataTableToSQL(dtRest, ConnString_Dest, DestinationTable)

                    End If
                End If
            End Using

            Return transferSuccess
        Catch ex As Exception
            Console.WriteLine(Now() & "    " & " Failed using RestAPI : " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
            WriteString_Log(Now() & "    " & " Failed using RestAPI : " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
            Return False
        End Try

    End Function

    Private Function TransferDataFromRestAPI_OOKLA() As Boolean
        Try
            Dim apiBaseUrl As String = ConfigurationManager.AppSettings("OOKLA_API_BASEURL").ToString
            Dim username As String = ConfigurationManager.AppSettings("OOKLA_API_USERNAME").ToString
            Dim password As String = ConfigurationManager.AppSettings("OOKLA_API_PASSWORD").ToString
            Dim localPath As String = ConfigurationManager.AppSettings("OOKLA_ZIP_LOCALPATH").ToString
            Dim sshClientIP As String = ConfigurationManager.AppSettings("OOKLA_ZIP_SSHIP").ToString
            Dim sshClientUser As String = ConfigurationManager.AppSettings("OOKLA_ZIP_SSHUSER").ToString
            Dim sshClientFolder As String = ConfigurationManager.AppSettings("OOKLA_ZIP_SSHFolder").ToString

            Dim client As New RestClient(apiBaseUrl)
            Dim request As New RestRequest(Method.GET)

            Dim credentials As String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"))
            request.AddHeader("Authorization", $"Basic {credentials}")
            request.AddHeader("Content-Type", "application/json")

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True

            Console.WriteLine("OOKLA Files: Requesting API Login")
            Dim response As RestResponse = CType(client.Execute(request), RestResponse)
            Dim json As JArray = CType(JsonConvert.DeserializeObject(response.Content), JArray)

            Dim fileName As String = Nothing
            Dim zipUrl As String = Nothing

            If json IsNot Nothing Then
                If response.StatusCode = HttpStatusCode.OK Then
                    Console.WriteLine("API Response: Login Request Successful")

                    Dim responseBody As String = response.Content.ToString
                    Dim jsonArray As Linq.JArray = JsonConvert.DeserializeObject(responseBody)

                    For Each item As JToken In jsonArray
                        'Cast each item to a JObject to access its properties
                        Dim obj As JObject = CType(item, JObject)

                        fileName = obj("name").ToString()
                        zipUrl = CStr(obj("url"))
                        Dim type As String = CStr(obj("type"))
                        If zipUrl = "/AndroidSignalScan/" Then
                            Exit For
                        End If
                    Next
                Else
                    Console.WriteLine("Error: API Login Failed " & response.StatusDescription)
                End If
            End If

            'Requesting ZIP files
            client = New RestClient(apiBaseUrl & zipUrl)
            request = New RestRequest(Method.GET)

            request.AddHeader("Authorization", $"Basic {credentials}")
            request.AddHeader("Content-Type", "application/json")

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True

            Console.WriteLine("API Request: Fetching Zip Files URLs From API")

            response = CType(client.Execute(request), RestResponse)
            json = CType(JsonConvert.DeserializeObject(response.Content), JArray)

            If json IsNot Nothing Then
                If response.StatusCode = HttpStatusCode.OK Then
                    Console.WriteLine("API Response: Zip File URL Received")

                    Dim responseBody As String = response.Content.ToString
                    Dim jsonArray As Linq.JArray = JsonConvert.DeserializeObject(responseBody)

                    'Setting local directory accessibility for everyone 
                    Dim di As DirectoryInfo = Nothing
                    'Directory.CreateDirectory(localPath)
                    di = New DirectoryInfo(localPath)
                    Dim dsDR As DirectorySecurity = di.GetAccessControl()
                    dsDR.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit Or InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow))
                    di.SetAccessControl(dsDR)

                    Console.WriteLine("Iterating Through URLs To Get File Data")

                    For Each item As JToken In jsonArray
                        'Cast each item to a JObject to access its properties
                        Dim obj As JObject = CType(item, JObject)

                        fileName = obj("name").ToString()
                        zipUrl = CStr(obj("url"))
                        Dim type As String = CStr(obj("type"))

                        client = New RestClient(zipUrl)
                        request = New RestRequest(Method.GET)

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                        ServicePointManager.ServerCertificateValidationCallback = Function(sender, cert, chain, sslPolicyErrors) True

                        Console.WriteLine("API Request: Downloading Zip File Through Received URL")
                        response = CType(client.Execute(request), RestResponse)

                        If response.StatusCode = HttpStatusCode.OK Then

                            Console.WriteLine("API Resonse: Received Zip File URL, Trying To Get Zip File Data In Bytes")
                            Dim zipData As Byte() = response.RawBytes()

                            Console.WriteLine("Writing Zip File Content To Local Path")
                            Try
                                Using stream As New MemoryStream()
                                    stream.Write(zipData, 0, zipData.Length)
                                    File.WriteAllBytes(localPath & "\" & fileName, stream.ToArray())
                                End Using
                            Catch ex As Exception
                                Console.WriteLine("Writing File Content To Local Path Failed: " & ex.Message.ToString)
                            End Try

                            'copy file to SSH Remote location through SFTP
                            Using sshCl As New SftpClient(sshClientIP, sshClientUser, "C3llS3ns01")
                                Try
                                    sshCl.Connect()
                                    Console.WriteLine("Connecting To SSH Client Successful")
                                Catch ex As Exception
                                    Console.WriteLine("Connecting To SSH Client Failed: " & ex.Message.ToString)
                                End Try

                                Try
                                    Using fs As FileStream = File.OpenRead(localPath & "\" & fileName)
                                        If Not sshCl.Exists(sshClientFolder & "/" & fileName) Then
                                            sshCl.UploadFile(fs, sshClientFolder & "/" & fileName)
                                            Console.WriteLine($"Uploaded File: {fileName} {DateTime.Now()}")
                                        End If
                                    End Using
                                Catch ex As Exception
                                    Console.WriteLine("Uploading File To SSH Location Failed: " & ex.Message.ToString)
                                End Try

                            End Using
                        Else
                            Console.WriteLine("Downloading ZIP File Failed: " & response.StatusDescription)
                        End If
                    Next
                Else
                    Console.WriteLine("Error: " & response.StatusDescription)
                End If
            End If

        Catch ex As Exception
            Console.WriteLine(Now() & "    " & " Failed using RestAPI OOKLA: " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
            WriteString_Log(Now() & "    " & " Failed using RestAPI OOKLA: " & ex.Message.ToString & vbCrLf & ex.StackTrace.ToString)
            Return False
        End Try
    End Function

    Public Function XMLToDataSet(ByVal xmlStr As String, ByVal schemaFile As String) As DataSet
        'Convert the XML to a dataset
        Dim sr As New System.IO.StringReader(xmlStr)

        'Convert xmlData to a Dataset
        Dim ds As New DataSet

        If schemaFile = String.Empty Then
            ds.ReadXml(sr, XmlReadMode.InferSchema)
        Else
            ds.ReadXmlSchema(schemaFile)
            ds.ReadXml(sr, XmlReadMode.ReadSchema)
        End If

        For Each relation As DataRelation In ds.Relations
            For Each c As DataColumn In relation.ParentColumns
                If Not relation.ChildTable.Columns.Contains(c.ColumnName) Then
                    relation.ChildTable.Columns.Add(c)
                End If
                For Each dr As DataRow In relation.ChildTable.Rows
                    dr(c.ColumnName) = dr.GetParentRow(relation)(c.ColumnName)
                Next
            Next
        Next

        Return ds
    End Function

End Module

