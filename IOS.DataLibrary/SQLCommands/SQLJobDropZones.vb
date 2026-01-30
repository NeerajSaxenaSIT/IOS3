Public Class SQLJobDropZones
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_JOBDROPZONES
    End Sub

    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Shared Function GetJobFormats(ByVal orderByColumn As String) As String
        Return "SELECT * FROM " & DataBaseTableName.TBL_JOBDROPZONES & "  order by " & orderByColumn
    End Function
End Class
