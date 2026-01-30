Public Class SQLDashboardReports
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_DASHBOARD_REPORTS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function DashboardReportsDelete(ByVal dashboardID As String, ByVal reportID As String)
        Return "Exec " & StoreProcedurName.SP_DASHBOARDREPORTS_DELETE & " " & dashboardID & ", " & reportID & ""
    End Function

    Public Shared Function DashboardReportsInsert(ByVal dashboardID As String, ByVal reportID As String)
        Return "Exec " & StoreProcedurName.SP_DASHBOARDREPORTS_INSERT & " " & dashboardID & ", " & reportID & ""
    End Function
    Public Shared Function GetDashBoardReport(ByVal dashboardID As String)
        Return "SELECT * FROM " & DataBaseTableName.TBL_DASHBOARD_REPORTS & " WHERE DashboardID=" & dashboardID & " Order by ReportOrdinal"
    End Function
    Public Shared Function SwapDashboardReportOrdinal(ByVal sourceReportId As Int32, ByVal targatReportId As Int32, ByVal dashboardId As Int32)
        Return "Exec " & StoreProcedurName.SP_DASHBOARDREPORTS_SWAPORDINAL & " " & sourceReportId & "," & targatReportId & "," & dashboardId
    End Function
End Class
