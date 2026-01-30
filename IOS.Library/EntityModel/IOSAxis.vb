Imports dotnetCHARTING.WinForms

Public Class IOSAxis
    Inherits dotnetCHARTING.WinForms.Axis

    Private _ElementListToApply As New List(Of String)
    Public Property ElementListToApply() As List(Of String)
        Get
            Return _ElementListToApply
        End Get
        Set(ByVal value As List(Of String))
            _ElementListToApply = value
        End Set
    End Property


    Private _ElementMarkerType As dotnetCHARTING.WinForms.ElementMarkerType = ElementMarkerType.Circle
    Public Property ElementMarkerType() As dotnetCHARTING.WinForms.ElementMarkerType
        Get
            Return _ElementMarkerType
        End Get
        Set(ByVal value As dotnetCHARTING.WinForms.ElementMarkerType)
            _ElementMarkerType = value
        End Set
    End Property

End Class
