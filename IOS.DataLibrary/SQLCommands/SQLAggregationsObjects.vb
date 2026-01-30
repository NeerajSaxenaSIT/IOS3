Public Class SQLAggregationsObjects
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_AGGREGATIONS_OBJECTS
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Shared Function GetObjectAggregationSuffix(ByVal sourcetable As String, ByVal objectAggregation As String)
        Return "Exec " & StoreProcedurName.SP_GET_OBJECT_AGGREGATION_SUFFIX & " '" & sourcetable & "','" & objectAggregation & "'; "
    End Function

End Class
