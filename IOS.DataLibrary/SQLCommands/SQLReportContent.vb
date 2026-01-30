Public Class SQLReportContent
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_REPORT_GROUPS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function SelectAll()
    '    Return "Select * from " & DataBaseTableName.TBL_REPORT_GROUPS
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String)
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean)
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_REPORT_GROUPS, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    Public Shared Function GetReport(ByVal reportID As String)
        Return "Exec " & StoreProcedurName.SP_REPORT_GET & " '" & reportID & "'"
    End Function

    Public Shared Function ReportContent_Delete(ByVal reportID As String)
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_DELETE & " '" & reportID & "'"
    End Function

    Public Shared Function ReportContent_CreateExportConnection(reportID As String, sqlDBName As String)
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_SAVEEXPORTCONNECTION & " '" & reportID & "','" & sqlDBName & "'"
    End Function

    Public Shared Function ReportContent_GetExportConnection(reportID As String)
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_GETEXPORTCONNECTION & " '" & reportID & "'"
    End Function

End Class
