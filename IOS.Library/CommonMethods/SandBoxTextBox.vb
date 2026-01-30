Public Class SandBoxTextBox
    Public Shared Function IsNumberVal(ByVal keyCh As String) As Boolean
        If Asc(keyCh) <> 8 Then
            If Asc(keyCh) < 48 Or Asc(keyCh) > 57 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
  
End Class
