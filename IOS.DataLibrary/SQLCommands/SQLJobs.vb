Public Class SQLJobs
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_JOBGROUPS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Shared Function InsertJob(ByVal jobGroupID As Integer, ByVal jobName As String, ByVal jobDescription As String, ByVal jobActive As Boolean, ByVal jobStart As String, ByVal jobInterval_Hours As Integer, ByVal JobInterval_Minutes As Integer, ByVal jobInterval_Days As Integer, ByVal jobStop_TimeOut_Minutes As Integer, ByVal jobStop_End As String, ByVal jobNextRun As String, ByVal jobOutputFormatID As Integer, ByVal jobOutputFileDestination As String, ByVal jobOutputEmailDestination As String, ByVal jobThresholdBreach As Boolean, ByVal jobSNMPAlarm As Boolean, ByVal jobSNMPAlarmComment As String)
        Return "Exec " & StoreProcedurName.SP_JOB_CREATE & " " & jobGroupID & ",'" & jobName & "','" & jobDescription & "','" & System.Environment.UserName.ToString() & "', " & jobActive & ",'" & jobStart & "'," & jobInterval_Hours & "," & JobInterval_Minutes & "," & jobInterval_Days & "," & jobStop_TimeOut_Minutes & ",'" & jobStop_End & "','" & jobNextRun & "'," & jobOutputFormatID & ",'" & jobOutputFileDestination & "','" & jobOutputEmailDestination & "','" & jobThresholdBreach & "','" & jobSNMPAlarm & "','" & jobSNMPAlarmComment & "'"
    End Function
    Public Shared Function JobReportsInsert(ByVal jobID As String, ByVal reportID As String)
        Return "Exec " & StoreProcedurName.SP_JOBREPORT_INSERT & " " & jobID & ", " & reportID & ""
    End Function

    Public Shared Function GetJobReport(ByVal jobID As String, ByVal reportID As String)
        Return "SELECT * " & ViewName.VIEW_JOB_REPORT & " WHERE " & JobReportFields.JobID & "=" & jobID & " AND " & JobReportFields.ReportID & "=" & reportID & ";"
    End Function
    Public Shared Function GetJobReport(ByVal jobID As String)
        Return "SELECT [JobName],[JobType],[ReportName] FROM " & ViewName.VIEW_JOB_REPORT & " WHERE " & JobReportFields.JobID & "=" & jobID & ";"
    End Function
    Public Shared Function GetJob(ByVal jobID As String)
        Return "SELECT * FROM " & ViewName.VIEW_JOBS & " WHERE " & JobFields.JobID & "=" & jobID & ";"
    End Function

    Public Shared Function JobReportsDelete(ByVal jobID As String, ByVal reportID As String)
        Return "Exec " & StoreProcedurName.SP_JOBREPORTS_DELETE & " " & jobID & ", " & reportID & ""
    End Function

    Public Shared Function JobDelete(ByVal jobID As Integer)
        Return "Exec " & StoreProcedurName.SP_JOB_DELETE & " " & jobID & ",'" & System.Environment.UserName.ToString() & "'"
    End Function

    Public Shared Function JobRename(ByVal jobID As Integer, ByVal newJobName As String)
        Return "Exec " & StoreProcedurName.SP_JOB_RENAME & " " & jobID & ",'" & newJobName & "','" & System.Environment.UserName.ToString() & "'"
    End Function


    Public Shared Function UpdateJob(ByVal jobID As Integer, ByVal jobDescription As String, ByVal jobActive As Boolean, ByVal jobStart As String, ByVal jobInterval_Hours As Integer, ByVal JobInterval_Minutes As Integer, ByVal jobInterval_Days As Integer, ByVal jobStop_TimeOut_Minutes As Integer, ByVal jobStop_End As String, ByVal jobNextRun As String, ByVal jobOutputFormatID As Integer, ByVal jobOutputFileDestination As String, ByVal jobOutputEmailDestination As String, ByVal jobThresholdBreach As Boolean, ByVal jobSNMPAlarm As Boolean, ByVal jobSNMPAlarmComment As String)
        Return "Exec " & StoreProcedurName.SP_JOB_UPDATE & " " & jobID & ",'" & jobDescription & "','" & System.Environment.UserName.ToString() & "', " & jobActive & ",'" & jobStart & "'," & jobInterval_Hours & "," & JobInterval_Minutes & "," & jobInterval_Days & "," & jobStop_TimeOut_Minutes & ",'" & jobStop_End & "','" & jobNextRun & "'," & jobOutputFormatID & ",'" & jobOutputFileDestination & "','" & jobOutputEmailDestination & "','" & jobThresholdBreach & "','" & jobSNMPAlarm & "','" & jobSNMPAlarmComment & "'"
    End Function


    'Public Shared Function UpdateJob(ByVal jobGroupID As Integer, ByVal jobName As String, ByVal jobDescription As String, ByVal jobActive As Boolean, ByVal jobStart As String, ByVal jobInterval_Hours As Integer, ByVal JobInterval_Minutes As Integer, ByVal jobInterval_Days As Integer, ByVal jobStop_TimeOut_Minutes As Integer, ByVal jobStop_End As String, ByVal jobNextRun As String, ByVal jobOutputFormatID As Integer, ByVal jobOutputFileDestination As String, ByVal jobOutputEmailDestination As String)
    '    Return "Exec " & StoreProcedurName.SP_JOB_UPDATE & " " & jobGroupID & ",'" & jobName & "','" & jobDescription & "','" & System.Environment.UserName.ToString() & "', " & jobActive & ",'" & jobStart & "'," & jobInterval_Hours & "," & JobInterval_Minutes & "," & jobInterval_Days & "," & jobStop_TimeOut_Minutes & ",'" & jobStop_End & "','" & jobNextRun & "'," & jobOutputFormatID & ",'" & jobOutputFileDestination & "','" & jobOutputEmailDestination & "'"
    'End Function
End Class
