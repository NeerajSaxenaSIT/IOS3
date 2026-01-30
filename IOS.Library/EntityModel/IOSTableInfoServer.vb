Public Class IOSTableInfoServer
    Implements ITableInfo
    Private _TableName As String
    Public Property TableName() As String Implements ITableInfo.TableName
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property
    Private _ConStr As String
    Public Property ConStr() As String Implements ITableInfo.ConStr
        Get
            Return _ConStr
        End Get
        Set(ByVal value As String)
            _ConStr = value
        End Set
    End Property
    Private _Query As String
    Public Property Query() As String Implements ITableInfo.Query
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
        End Set
    End Property
    Private _Layer As MapInfo.Mapping.FeatureLayer
    Public Property OutLayer() As MapInfo.Mapping.FeatureLayer Implements ITableInfo.OutLayer
        Get
            Return _Layer
        End Get
        Set(ByVal value As MapInfo.Mapping.FeatureLayer)
            _Layer = value
        End Set
    End Property
    Public Sub New(ByRef TableName As String, ByRef ConStr As String, ByVal Query As String)
        Me._TableName = TableName
        Me._ConStr = ConStr
        Me._Query = Query
    End Sub
End Class
