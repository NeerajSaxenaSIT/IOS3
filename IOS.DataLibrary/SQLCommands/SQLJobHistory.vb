Public Class SQLJobHistory
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_JOBHISTORY
    End Sub

    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function GetJobHistoryByJobId(ByVal jobId As String)
        Return "Exec " & StoreProcedurName.SP_GET_JOB_HISTORYBY_JOBID & " " & jobId
    End Function

    Public Shared Function GetJobFormats(ByVal orderByColumn As String) As String
        Return "SELECT * FROM " & DataBaseTableName.TBL_JOBDROPZONES & "  order by " & orderByColumn
    End Function
End Class
