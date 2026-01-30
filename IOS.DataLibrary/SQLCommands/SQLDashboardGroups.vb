Public Class SQLDashboardGroups
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_DASHBOARDGROUPS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    
    Public Shared Function GetDashboardGroup(ByVal whereExpressionString As String, ByVal orderByColumn As String) As String
        Return "SELECT * FROM " & ViewName.VIEW_DASHBOARDSGROUPS & " Where " & whereExpressionString & " order by " & orderByColumn
    End Function

    Public Shared Function Insert(ByVal dashBoardGroupName As String, ByVal isDashBoardGroupPrivate As Boolean) As String
        Return "Exec " & StoreProcedurName.SP_DASHBOARDGROUPS_CREATE & " '" & dashBoardGroupName & "'," & isDashBoardGroupPrivate & ",'" & System.Environment.UserName.ToString() & "'"
    End Function
    Public Shared Function GetDashBoardGroupReportTree(ByVal dashboardGroupID As Integer) As String
        Return "EXEC " & StoreProcedurName.SP_GET_DASHBOARD_GROUP_REPORT_TREE & " " & dashboardGroupID & ", '" & System.Environment.UserName.ToString() & "'"
    End Function

    Public Shared Function Delete(ByVal dashboardGroupID As String)
        Return "Exec " & StoreProcedurName.SP_DASHBOARDGROUPS_DELETE & " '" & dashboardGroupID & "','" & System.Environment.UserName.ToString() & "'"
    End Function

    Public Shared Function Modify(ByVal dashboardGroupID As String, ByVal dashboardGroupName_NewName As String, ByVal dashboardGroupPrivate As String, ByVal Creator As String)
        Return "Exec " & StoreProcedurName.SP_DASHBOARDGROUPS_MODIFY & " " & dashboardGroupID & ",'" & dashboardGroupName_NewName & "'," & dashboardGroupPrivate & ",'" & Creator & "'"
    End Function
   
End Class
