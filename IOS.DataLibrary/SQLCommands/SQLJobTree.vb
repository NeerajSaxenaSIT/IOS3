Public Class SQLJobTree
    Inherits SQLCommanCommand
    Sub New()
        _tableName = ViewName.VIEW_JOBTREE
    End Sub
    Protected Overrides Sub Finalize()
        ''GC.SuppressFinalize(Me)
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Shared Function GetJobTree(ByVal whereExpressionString As String, ByVal orderByColumn As String) As String
        Return "SELECT * FROM " & ViewName.VIEW_JOBTREE & " Where " & whereExpressionString & " order by " & orderByColumn
    End Function
End Class
