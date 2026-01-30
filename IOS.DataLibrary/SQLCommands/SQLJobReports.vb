Public Class SQLJobReports
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_JOBREPORTS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function ReportsDelete(ByVal dashboardID As String, ByVal reportID As String)
    '    Return "Exec " & StoreProcedurName.SP_DASHBOARDREPORTS_DELETE & " " & dashboardID & ", " & reportID & ""
    'End Function

    Public Shared Function JobReportsInsert(ByVal jobID As String, ByVal reportID As String)
        Return "Exec " & StoreProcedurName.SP_JOBREPORT_INSERT & " " & jobID & ", " & reportID & ""
    End Function
End Class
