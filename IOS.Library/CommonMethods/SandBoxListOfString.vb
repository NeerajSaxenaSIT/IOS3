Public Class SandBoxListOfString
    Public Shared Sub InsertIntoList(ByRef lst As List(Of String), ByVal newValues As String())
        For Each newValue As String In newValues
            If (Not String.IsNullOrEmpty(newValue)) Then
                lst.Add(newValue)
            End If
        Next
    End Sub
End Class
