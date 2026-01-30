Public Class SQLThresholdTypes
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_THRESHOLD_TYPES
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Shared Function GetByID(ByVal thresholdTypeID As String)
        Return "SELECT * FROM  " & DataBaseTableName.TBL_THRESHOLD_TYPES & " WHERE ThresholdTypeID=" & thresholdTypeID
    End Function
End Class
