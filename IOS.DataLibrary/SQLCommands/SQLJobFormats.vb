Public Class SQLJobFormats
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_JOBFORMATS
    End Sub

    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function GetJobFormats(ByVal orderByColumn As String) As String
        Return "SELECT * FROM " & DataBaseTableName.TBL_JOBFORMATS & "  order by " & orderByColumn
    End Function
End Class
