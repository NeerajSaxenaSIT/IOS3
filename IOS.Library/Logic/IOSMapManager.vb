Imports MapInfo.Data
Imports MapInfo.Windows.Controls
Imports MapInfo.Geometry
Imports MapInfo.Mapping
Imports MapInfo.Engine
Imports MapInfo.Tools
Imports MapInfo.Styles
Public Class IOSMapManager
    Implements IDisposable

    Private _mapControl As MapInfo.Windows.Controls.MapControl
    Public Property MapControl() As MapInfo.Windows.Controls.MapControl
        Get
            Return _mapControl
        End Get
        Set(ByVal value As MapInfo.Windows.Controls.MapControl)
            _mapControl = value
        End Set
    End Property

    Private _MIConnection As MIConnection
    Public Property MIConnection() As MIConnection
        Get
            Return _MIConnection
        End Get
        Set(ByVal value As MIConnection)
            _MIConnection = value
            If (_MIConnection.State = ConnectionState.Closed) Then
                _MIConnection.Open()
            End If
        End Set
    End Property

    Private _ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Public Property ProgressBar() As System.Windows.Forms.ToolStripProgressBar
        Get
            Return _ProgressBar
        End Get
        Set(ByVal value As System.Windows.Forms.ToolStripProgressBar)
            _ProgressBar = value
        End Set
    End Property

    Public Sub New(ByRef mapControl As MapControl)
        Me._mapControl = mapControl
    End Sub
    Public Sub New(ByRef mapControl As MapControl, ByRef connection As MIConnection)
        Me._mapControl = mapControl
        Me._MIConnection = connection
    End Sub

    Public Sub RemoveLayer(ByVal layerName As String)
        Try
            Me._mapControl.Map.Layers.Remove(layerName)
            MapInfo.Engine.Session.Current.Catalog.CloseTable(layerName)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub CloseTable(ByVal tableName As String)
        If Not (Me._MIConnection Is Nothing And Not Me._MIConnection.Catalog Is Nothing) Then
            Me._MIConnection.Catalog.CloseTable(tableName)
        End If
    End Sub
    Public Sub CreateMapLayerUsingServerTableInfo(ByRef tableInfo As IOSTableInfoServer)
        Me.RemoveLayer(tableInfo.TableName)
        Me.CloseTable(tableInfo.TableName)
        Dim ti As TableInfoServer = New TableInfoServer(tableInfo.TableName)
        ti.ConnectString = tableInfo.ConStr
        ti.Query = tableInfo.Query
        ti.Toolkit = ServerToolkit.Odbc
        ti.CacheSettings.CacheType = CacheOption.All
        Dim tbl As Table = Me._MIConnection.Catalog.OpenTable(ti)
        Dim lyr As New FeatureLayer(tbl)

        Me._mapControl.Map.Layers.Add(lyr)
        tableInfo.OutLayer = lyr
    End Sub

    Public Sub CreateMapLayerUsingAdoNetTableInfo(ByRef tableInfo As IOSTableInfoAdoNet, Optional ByVal progressStartValue As Integer = Nothing, Optional ByVal progressEndValue As Integer = Nothing)

        Me.UpdateProgerssBar(progressStartValue, progressEndValue)
        Me.RemoveLayer(tableInfo.TableName)
        Me.CloseTable(tableInfo.TableName)
        Dim data As DataTable
        If (String.IsNullOrEmpty(tableInfo.ConStr) And String.IsNullOrEmpty(tableInfo.Query)) Then
            data = tableInfo.Data
        Else
            data = IOS.DataLibrary.DataAccessorODBC.GetDataTable(tableInfo.ConStr, tableInfo.Query)
        End If
        Me.LayerWithAdoNet(tableInfo, data, progressStartValue, progressEndValue)
    End Sub
   
    Public Sub CreateMapLayerUsingAdoNetTableInfo(ByRef tableInfo As IOSTableInfoAdoNet, ByVal parameters As List(Of Odbc.OdbcParameter), Optional ByVal progressStartValue As Integer = Nothing, Optional ByVal progressEndValue As Integer = Nothing)

        Me.UpdateProgerssBar(progressStartValue, progressEndValue)
        Me.RemoveLayer(tableInfo.TableName)
        Me.CloseTable(tableInfo.TableName)
        Dim data As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(tableInfo.ConStr, tableInfo.Query, parameters)
        Me.LayerWithAdoNet(tableInfo, data, progressStartValue, progressEndValue)
    End Sub
    Public Function ExecuteMapInfoQuery(ByVal query As String, ByVal aliasName As String, ByVal ParamArray parameters() As MIParameter) As IResultSetFeatureCollection
        Dim command As MapInfo.Data.MICommand = Me._MIConnection.CreateCommand()
        command.CommandText = query
        For Each Item As MIParameter In parameters
            command.Parameters.Add(Item)
        Next
        Return command.ExecuteFeatureCollection(aliasName)
    End Function
    Private Sub LayerWithAdoNet(ByRef tableInfo As IOSTableInfoAdoNet, ByRef data As DataTable, Optional ByVal progressStartValue As Integer = Nothing, Optional ByVal progressEndValue As Integer = Nothing)
        Dim ti As TableInfoAdoNet = New TableInfoAdoNet(tableInfo.TableName)
        ti.DataTable = data
        ti.ReadOnly = False
        If (tableInfo.IsSpatial) Then
            Dim xy As SpatialSchemaXY = New SpatialSchemaXY
            xy.XColumn = tableInfo.SpatialSchemaX
            xy.YColumn = tableInfo.SpatialSchemaY
            xy.NullPoint = "0.0, 0.0"
            Me.UpdateProgerssBar(progressStartValue, progressEndValue)
            xy.DefaultStyle = tableInfo.Style
            xy.CoordSys = Session.Current.CoordSysFactory.CreateLongLat(DatumID.WGS84)
            ti.SpatialSchema = xy
        End If
        Dim tbl As Table = Me._MIConnection.Catalog.OpenTable(ti)
        Me.UpdateProgerssBar(progressStartValue, progressEndValue)
        Dim lyr As New FeatureLayer(tbl)
        ''  MapInfo.Mapping.LayerHelper.SetInsertable(lyr, True)
        Me._mapControl.Map.Layers.Add(lyr)
        Me.UpdateProgerssBar(progressStartValue, progressEndValue)
        tableInfo.OutLayer = lyr
        tableInfo.OutData = data
    End Sub
    Private Sub UpdateProgerssBar(ByRef Startvalue As Integer, ByRef EndValue As Integer)
        Dim value As Integer = 0
        If (Startvalue = Nothing Or EndValue = Nothing) Then
            Exit Sub
        Else
            value = (EndValue - Startvalue) / 5
        End If
        If Not (Me.ProgressBar Is Nothing And Me.ProgressBar.Value <= 100 And Not value = 0) Then
            If (Me.ProgressBar.Value + value > 100) Then
                Exit Sub
            End If
            Me.ProgressBar.Value += value
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If (Me._MIConnection.State = ConnectionState.Open) Then
            Me._MIConnection.Close()
        End If
    End Sub

    Overridable Sub Dispose() Implements IDisposable.Dispose
        If (Me._MIConnection.State = ConnectionState.Open) Then
            Me._MIConnection.Close()
        End If
    End Sub
End Class
