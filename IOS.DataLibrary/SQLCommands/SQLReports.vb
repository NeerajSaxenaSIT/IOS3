Public Class SQLReports
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

    Public Shared Function InsertReports(reportGroupID As String, ByVal reportCategoryID As String, ByVal reportName As String)
        Return "Exec " & StoreProcedurName.SP_REPORT_CREATE & " " & reportGroupID & "," & reportCategoryID & ",'" & reportName & "' , '" & System.Environment.UserName & "'"
    End Function
    Public Shared Function CopyReport(ByVal reportGroupID As String, ByVal reportID As String, ByVal reportName As String)
        Return "Exec " & StoreProcedurName.SP_REPORT_COPY & " " & reportGroupID & "," & reportID & ",'" & reportName & "' , '" & System.Environment.UserName & "'"
    End Function
    Public Shared Function DeleteReports(ByVal reportID As String, ByVal reportCategoryID As String)
        Return "Exec " & StoreProcedurName.SP_REPORT_DELETE & " " & reportID & "," & reportCategoryID & ""
    End Function
    Public Shared Function ModifyReports(ByVal reportGroupID As String, ByVal reportID As String, ByVal reportName As String, ByVal creatorID As String)
        Return $"Exec {StoreProcedurName.SP_REPORT_MODIFY} {reportGroupID},{reportID},'{reportName}','{creatorID}'"
    End Function
    Public Shared Function SwapReportOrdinal(ByVal sourceReportId As Integer, ByVal targatReportId As Integer)
        Return "Exec " & StoreProcedurName.SP_REPORT_SWAPORDINAL & " " & sourceReportId & "," & targatReportId
    End Function
    Public Shared Function SwapCategoryOrdinal(ByVal targetCategoryID As Integer, ByVal sourceCategoryID As Integer, ByVal sourceCategoryOrdinal As Integer)
        Return "Exec " & StoreProcedurName.SP_CATEGORY_SWAPORDINAL & " " & targetCategoryID & "," & sourceCategoryID & "," & sourceCategoryOrdinal
    End Function
    Public Shared Function InsertReportsTemp(ByVal reportName As String)
        Return "Exec " & StoreProcedurName.SP_REPORT_CREATE_TEMP & " '" & reportName & "'"
    End Function
    Public Shared Function GetNewRepotId()
        Return "Exec " & StoreProcedurName.SP_GETNEWREPORTID
    End Function
End Class
