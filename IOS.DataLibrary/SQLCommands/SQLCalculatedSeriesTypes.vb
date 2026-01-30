Public Class SQLCalculatedSeriesTypes
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_CALCULATED_SERIES_TYPES
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Shared Function GetByID(ByVal calculatedSeriesTypeID As String)
        Return "SELECT * FROM  " & DataBaseTableName.TBL_CALCULATED_SERIES_TYPES & " WHERE CalculatedSeriesTypeID=" & calculatedSeriesTypeID
    End Function

End Class
