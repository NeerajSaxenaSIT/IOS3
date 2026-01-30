Imports System
Imports System.Net

Public Class MyCustomWebClient
    Inherits Net.WebClient
    Private _TimeoutMS As Integer = 0
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal TimeoutMS As Integer)
        MyBase.New()
        _TimeoutMS = TimeoutMS
    End Sub
    Protected Overrides Sub Dispose(ByVal Disposing As Boolean)
        On Error Resume Next
        MyBase.Dispose(Disposing)
    End Sub
    Public WriteOnly Property SetTimeout() As Integer
        Set(ByVal Value As Integer)
            _TimeoutMS = Value
        End Set
    End Property
    Protected Overrides Function GetWebRequest(ByVal Address As System.Uri) As Net.WebRequest
        On Error Resume Next
        Dim MyWR As Net.WebRequest = MyBase.GetWebRequest(Address)
        If _TimeoutMS <> 0 Then MyWR.Timeout = _TimeoutMS
        Return MyWR
    End Function
End Class
