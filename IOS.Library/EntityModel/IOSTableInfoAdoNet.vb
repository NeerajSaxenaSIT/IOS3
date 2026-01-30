Public Class IOSTableInfoAdoNet
    Implements ITableInfo

    Private _IsSpatial As Boolean
    Public ReadOnly Property IsSpatial() As Boolean
        Get
            Return _IsSpatial
        End Get
    End Property

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
    Private _Data As DataTable
    Public ReadOnly Property Data() As DataTable
        Get
            Return _Data
        End Get
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
    Private _Style As MapInfo.Styles.Style
    Public Property Style() As MapInfo.Styles.Style
        Get
            Return _Style
        End Get
        Set(ByVal value As MapInfo.Styles.Style)
            _Style = value
        End Set
    End Property
    Private _SpatialSchemaX As String
    Public Property SpatialSchemaX() As String
        Get
            Return _SpatialSchemaX
        End Get
        Set(ByVal value As String)
            _SpatialSchemaX = value
        End Set
    End Property
    Private _SpatialSchemaY As String
    Public Property SpatialSchemaY() As String
        Get
            Return _SpatialSchemaY
        End Get
        Set(ByVal value As String)
            _SpatialSchemaY = value
        End Set
    End Property

    Private _OutData As DataTable
    Public Property OutData() As DataTable
        Get
            Return _OutData
        End Get
        Set(ByVal value As DataTable)
            _OutData = value
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
    Public Sub New(ByRef TableName As String, ByVal data As DataTable, ByVal Style As MapInfo.Styles.Style, ByVal SpatialX As String, ByVal SpatialY As String)
        Me._TableName = TableName
        Me._Data = data
        Me._Style = Style
        Me.SpatialSchemaX = SpatialX
        Me.SpatialSchemaY = SpatialY
        Me._IsSpatial = True
    End Sub
    Public Sub New(ByRef TableName As String, ByRef ConStr As String, ByVal Query As String, ByVal Style As MapInfo.Styles.Style, ByVal SpatialX As String, ByVal SpatialY As String)
        Me._TableName = TableName
        Me._ConStr = ConStr
        Me._Query = Query
        Me._Style = Style
        Me.SpatialSchemaX = SpatialX
        Me.SpatialSchemaY = SpatialY
        Me._IsSpatial = True
    End Sub
    Public Sub New(ByRef TableName As String, ByRef ConStr As String, ByVal Query As String, ByVal Style As MapInfo.Styles.Style)
        Me._TableName = TableName
        Me._ConStr = ConStr
        Me._Query = Query
        Me._Style = Style
        Me._IsSpatial = False
    End Sub
    Public Sub New(ByRef TableName As String, ByRef ConStr As String, ByVal Query As String)
        Me._TableName = TableName
        Me._ConStr = ConStr
        Me._Query = Query
        Me._IsSpatial = False
    End Sub
    Public Sub New(ByRef TableName As String, ByRef ConStr As String, ByVal Query As String, ByVal Style As MapInfo.Styles.Style, ByVal SpatialX As String, ByVal SpatialY As String, ByRef parameters As List(Of Odbc.OdbcParameter))
        Me._TableName = TableName
        Me._ConStr = ConStr
        Me._Query = Query
        Me._Style = Style
        Me.SpatialSchemaX = SpatialX
        Me.SpatialSchemaY = SpatialY

        Me._IsSpatial = True
    End Sub
End Class
