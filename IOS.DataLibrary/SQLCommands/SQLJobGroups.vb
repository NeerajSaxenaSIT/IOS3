Public Class SQLJobGroups
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_JOBGROUPS
    End Sub

    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Shared Function GetJobGroup(ByVal whereExpressionString As String, ByVal orderByColumn As String) As String
        Return "SELECT * FROM " & ViewName.VIEW_JOBGROUPS & " Where " & whereExpressionString & " order by " & orderByColumn
    End Function


    Public Shared Function GetJobGroupReportTree(ByVal jobGroupID As Integer) As String
        Return "EXEC " & StoreProcedurName.SP_GETJOBGROUPREPORTTREE & " " & jobGroupID & ", '" & System.Environment.UserName.ToString() & "'"
    End Function

    Public Shared Function InsertJobGroup(ByVal jobGroupName As String, ByVal isJobGroupPrivate As Boolean) As String
        Return "Exec " & StoreProcedurName.SP_JOBGROUP_CREATE & " '" & jobGroupName & "'," & isJobGroupPrivate & ",'" & System.Environment.UserName.ToString() & "'"
    End Function

    Public Shared Function DeleteJobGroup(ByVal jobGroupID As String)
        Return "Exec " & StoreProcedurName.SP_JOBGROUP_DELETE & " '" & jobGroupID & "','" & System.Environment.UserName.ToString() & "'"
    End Function

    Public Shared Function ModifyJobGroup(ByVal jobGroupID As String, ByVal jobGroupName_NewName As String, ByVal jobGroupPrivate As String, ByVal Creator As String)
        Return "Exec " & StoreProcedurName.SP_JOBGROUP_MODIFY & " " & jobGroupID & ",'" & jobGroupName_NewName & "'," & jobGroupPrivate & ",'" & Creator & "'"
    End Function
End Class
