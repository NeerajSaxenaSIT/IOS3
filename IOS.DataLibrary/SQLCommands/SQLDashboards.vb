Imports System.Text

Public Class SQLDashboards
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_DASHBOARDS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function Insert(ByVal dashBoardName As String, ByVal dashBoardGroupID As Integer, ByVal Creator As String)
        Return "Exec " & StoreProcedurName.SP_DASHBOARD_CREATE & " '" & dashBoardName & "'," & dashBoardGroupID & ",'" & System.Environment.UserName.ToString() & "'"
    End Function
    Public Shared Function Modify(ByVal dashboardId As String, ByVal dashboardName_NewName As String, ByVal Creator As String)
        Return "Exec " & StoreProcedurName.SP_DASHBOARD_MODIFY & " " & dashboardId & ",'" & dashboardName_NewName & "','" & System.Environment.UserName.ToString() & "'"
    End Function
    Public Shared Function Delete(ByVal dashboardId As String)
        Return "Exec " & StoreProcedurName.SP_DASHBOARD_DELETE & " " & dashboardId & ",'" & System.Environment.UserName.ToString() & "'"
    End Function

    Public Shared Function GetReportChartGrid(ByVal dashboardId As String)
        Return "Exec " & StoreProcedurName.SP_GETREPORT_CHART_GRID & " " & dashboardId
    End Function

    Public Shared Function GetDashBoardReportChart(ByVal dashboardID As String)
        Return "Exec " & StoreProcedurName.SP_GETDASHBOARD_REPORTCHART & " " & dashboardID
    End Function

    Public Shared Function GetDashboardsForDashboardGroup(dashboardGroupID As String)
        Dim sbQuery As New StringBuilder()
        sbQuery.AppendLine("Select d.[DashboardID],d.[DashboardName] From [dbo].[tbl_Dashboards] d ")
        sbQuery.AppendLine("Inner Join [dbo].[tbl_DashboardGroup_Dashboards] dg on d.[DashboardID] = dg.[DashboardID]")
        sbQuery.AppendLine("Inner Join [dbo].[tbl_DashboardGroups] g on dg.[DashboardGroupID] = g.[DashboardGroupID]")
        sbQuery.AppendLine("Where g.[DashboardGroupID] = " & dashboardGroupID & ";")
        Return sbQuery.ToString
    End Function

End Class
