Public Class SQLCommanCommand

    Public Shared _tableName As String = Nothing
    Public Function SelectAll() As String
        Return "Select * from " & _tableName
    End Function
    Public Function SelectAll(ByVal orderByColumn As String) As String
        Return SelectAll() & " order by " & orderByColumn
    End Function
    Public Function SelectAll(ByVal withAlias As Boolean) As String
        Return SelectAll() & If(withAlias, " AS " & _tableName, "")
    End Function
    Public Function SelectAll(ByVal orderByColumn As String, ByVal withAlias As Boolean) As String
        If (withAlias) Then
            Return SelectAll(withAlias) & " order by " & orderByColumn
        Else
            Return SelectAll() & " order by " & orderByColumn
        End If
    End Function
    Public Function SelectAll(ByVal withAlias As Boolean, ByVal whereExpressionString As String) As String
        If (withAlias) Then
            Return SelectAll(withAlias) & " Where " & whereExpressionString
        Else
            Return SelectAll() & " Where " & whereExpressionString
        End If
    End Function
    Public Function SelectAll(ByVal whereExpressionString As String, ByVal orderByColumn As String, ByVal withAlias As Boolean) As String
        If (withAlias) Then
            Return SelectAll(withAlias) & " Where " & whereExpressionString & " order by " & orderByColumn
        Else
            Return SelectAll() & " Where " & whereExpressionString & " order by " & orderByColumn
        End If
    End Function

    Public Shared Function GetScopeIdentity() As String
        Return "SELECT SCOPE_IDENTITY();"
    End Function


End Class
