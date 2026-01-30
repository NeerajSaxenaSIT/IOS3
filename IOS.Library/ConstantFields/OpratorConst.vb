Public Class OperatorConst
    Public Const Equal As String = "="
    Public Const LessThan As String = "<"
    Public Const LessThanEqual As String = "<="
    Public Const GreaterThan As String = ">"
    Public Const GreaterThanEqual As String = ">="
    Public Const NotEqual As String = "<>"

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function

    'Shared Function Equals() As String
    '    Throw New NotImplementedException
    'End Function

End Class
