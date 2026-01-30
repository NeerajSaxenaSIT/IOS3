Public Class SQLTechnologySourceObjects
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_TECHNOLOGY_SOURCEOBJECTS
    End Sub

    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function GetCounterInfo(ByVal counterId As String, ByVal sourceObjectId As String)
        Return "Exec " & StoreProcedurName.SP_GET_COUNTERINFO & " " & counterId & "," & sourceObjectId
    End Function
End Class
