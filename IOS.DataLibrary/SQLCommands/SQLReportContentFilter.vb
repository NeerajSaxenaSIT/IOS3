Public Class SQLReportContentFilter

    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_REPORT_CONTENT_FILTERS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function DeleteByReportID(ByVal reportId As String) As String
        Return "DELETE " & DataBaseTableName.TBL_REPORT_CONTENT_FILTERS & " WHERE " & ReportContentFilterFields.ReportID & "=" & reportId & ";"
    End Function
    Public Shared Function InsertReportContent_Filter(ByVal reportId As String, ByVal filterDimension As String, ByVal filterOperator As String, ByVal filterValue As String, ByVal logicalLink As String, ByVal filterType As String, ByVal ObjectFilterType As String) As String
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENT_FILTERS_INSERT & " " & reportId & ",'" & filterDimension & "','" & filterOperator & "','" & filterValue & "','" & logicalLink & "','" & filterType & "'," & ObjectFilterType & ";"
    End Function
    Public Shared Function GetReportContentFilter(ByVal reportId As String) As String
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENTFILTERS_GETBY_REPORTID & " " & reportId & ""
    End Function

    Public Shared Function GetReportDimensionDistinctValues(tagValue As String, objectTableName As String) As String
        Return "Exec " & StoreProcedurName.SP_REPORTCONTENTFILTERS_DIMENSION_DISTINCT_VALUES & " " & "'" & tagValue & "','" & objectTableName & "'"
    End Function
End Class
