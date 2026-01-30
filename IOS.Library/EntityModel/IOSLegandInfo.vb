<Serializable()> _
Public Class IOSLegandInfo

    Private _LayerName As String
    Public Property LayerName() As String
        Get
            Return _LayerName
        End Get
        Set(ByVal value As String)
            _LayerName = value
        End Set
    End Property

    Private _ThemeName As String
    Public Property ThemeName() As String
        Get
            Return _ThemeName
        End Get
        Set(ByVal value As String)
            _ThemeName = value
        End Set
    End Property

    Private _Header As String
    Public Property Header() As String
        Get
            Return _Header
        End Get
        Set(ByVal value As String)
            _Header = value
        End Set
    End Property

    Private _SubHeader As String
    Public Property SubHeader() As String
        Get
            Return _SubHeader
        End Get
        Set(ByVal value As String)
            _SubHeader = value
        End Set
    End Property

End Class
