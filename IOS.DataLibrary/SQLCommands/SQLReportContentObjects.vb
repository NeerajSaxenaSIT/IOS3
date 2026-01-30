Public Class SQLReportContentObjects
    Inherits SQLCommanCommand
    Sub New()
        _tableName = StoreProcedurName.SP_REPORTCONTENT_OBJECTS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function SelectAll() As String
    '    Return "Select * from " & DataBaseTableName.TBL_REPORTCONTENT_OBJECTS_SP
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String) As String
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean) As String
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_REPORTCONTENT_OBJECTS_SP, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean) As String
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    Public Shared Function InsertReportContent_Objects() As String
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_OBJECTS & ""
    End Function
    Public Shared Function InsertReportContent_Objects(ByVal objectID As String, ByVal reportId As Integer) As String
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_OBJECTS & " " & reportId & ",'" & objectID & "' ; "
    End Function
    Public Shared Function InsertReportContent_GetObjects(ByVal reportId As Integer) As String
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_GETOBJECTS & " " & reportId & "; "
    End Function
    Public Shared Function GetReportChartObjects(reportID As Integer) As String
        Return "Select [ObjectID] From " & DataBaseTableName.TBL_REPORT_CONTENT_OBJECTS & " Where [ReportID] = " & reportID & ";"
    End Function

End Class
