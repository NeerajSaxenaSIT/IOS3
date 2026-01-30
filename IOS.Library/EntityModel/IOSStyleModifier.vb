Imports MapInfo.Mapping
Imports MapInfo.Mapping.Thematics

<Serializable()> _
Public Class IOSStyleModifier
    Private _FeatureLayer As FeatureLayer
    Public Property FeatureLayer() As FeatureLayer
        Get
            Return _FeatureLayer
        End Get
        Set(ByVal value As FeatureLayer)
            _FeatureLayer = value
        End Set
    End Property
    Private _LayerName As String
    Public Property LayerName() As String
        Get
            Return _LayerName
        End Get
        Set(ByVal value As String)
            _LayerName = value
        End Set
    End Property
    Private _ILegandList As List(Of IOSLegandInfo)
    Public Property ILegendList() As List(Of IOSLegandInfo)
        Get
            Return _ILegandList
        End Get
        Set(ByVal value As List(Of IOSLegandInfo))
            _ILegandList = value
        End Set
    End Property
    Public Sub New()
        _ILegandList = New List(Of IOSLegandInfo)
    End Sub
End Class
