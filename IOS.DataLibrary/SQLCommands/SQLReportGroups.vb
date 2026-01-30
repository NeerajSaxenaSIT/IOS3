Public Class SQLReportGroups
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_REPORT_GROUPS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function InsertGroup(ByVal reportGroupName As String, ByVal isReportGroupPrivate As Boolean)
        Return "Exec " & StoreProcedurName.SP_REPORTGROUP_CREATE & " '" & reportGroupName & "'," & isReportGroupPrivate & ",'" & System.Environment.UserName.ToString() & "'"
    End Function
    Public Shared Function ModifyGroup(ByVal reportGroupID As String, ByVal ReportGroupName_NewName As String, ByVal ReportGroupPrivate As String, ByVal Creator As String)
        Return "Exec " & StoreProcedurName.SP_REPORTGROUP_MODIFY & " " & reportGroupID & ",'" & ReportGroupName_NewName & "'," & ReportGroupPrivate & ",'" & Creator & "'"
    End Function
    Public Shared Function DeleteGroup(ByVal reportGroupID As String, ByVal Creator As String)
        Return "Exec " & StoreProcedurName.SP_REPORTGROUP_DELETE & " '" & reportGroupID & "','" & Creator & "'"
    End Function
    Public Shared Function GetReportGroups(ByVal licenseUser As String)
        Return "Exec " & StoreProcedurName.SP_REPORTGROUP_GET & " '" & licenseUser & "'"
    End Function
    Public Shared Function UpdateReportCategory(ByVal reportID As Integer, ByVal reportCategoryID As Integer)
        Return "UPDATE [tbl_Reports] SET ReportCategoryID = " & reportCategoryID & ",ReportCategoryOrdinal=(SELECT  ISNULL(MAX(ReportCategoryOrdinal) + 1,0) from [tbl_Reports] where ReportCategoryID=" & reportCategoryID & ")" & " Where ReportID = " & reportID & ""
    End Function

End Class
