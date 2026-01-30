Public Class SQLReportCategory

    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_REPORT_CATEGORY
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function SelectAll()
    '    Return "Select * from " & DataBaseTableName.TBL_REPORT_CATEGORY
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String)
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean)
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_REPORT_CATEGORY, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    Public Shared Function InsertReportCategory(ByVal reportGroupID As String, ByVal reportCategoryName As String)
        Return "Exec " & StoreProcedurName.SP_REPORTCATEGORY_CREATE & " '" & reportGroupID & "','" & reportCategoryName & "'"
    End Function
    Public Shared Function DeleteReportCategory(ByVal reportCategoryID As String, ByVal reportGroupID As String)
        Return "Exec " & StoreProcedurName.SP_REPORTCATEGORY_DELETE & " " & reportCategoryID & "," & reportGroupID
    End Function
    Public Shared Function ModifyReportCategory(ByVal reportCategoryID As String, ByVal reportCategoryName As String)
        Return "Exec " & StoreProcedurName.SP_REPORTCATEGORY_MODIFY & " " & reportCategoryID & ",'" & reportCategoryName & "'"
    End Function

End Class
