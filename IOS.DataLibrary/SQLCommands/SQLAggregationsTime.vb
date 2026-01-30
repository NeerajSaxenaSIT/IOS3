Public Class SQLAggregationsTime
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_AGGREGATIONS_TIME
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function SelectAll()
    '    Return "Select * from " & DataBaseTableName.TBL_AGGREGATIONS_TIME
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String)
    '    Return SelectAll() & " order by " & orderByColumn
    'End Function

    'Public Shared Function SelectAll(ByVal withAlias As Boolean)
    '    Return SelectAll() & If(withAlias, " AS " & DataBaseTableName.TBL_AGGREGATIONS_TIME, "")
    'End Function

    'Public Shared Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean)
    '    If (withAlias) Then
    '        Return SelectAll(withAlias) & " order by " & orderByColumn
    '    Else
    '        Return SelectAll() & " order by " & orderByColumn
    '    End If
    'End Function

    Public Shared Function GetTimeAggregationSuffix(ByVal sourcetable As String, ByVal timeAggregation As String)
        Return "Exec " & StoreProcedurName.SP_GET_TIME_AGGREGATION_SUFFIX & " '" & sourcetable & "','" & timeAggregation & "'; "
    End Function

End Class
