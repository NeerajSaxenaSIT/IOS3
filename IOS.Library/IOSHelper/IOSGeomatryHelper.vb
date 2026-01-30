Imports MapInfo.Geometry
Imports MapInfo.Ogc

Public Class IOSGeomatryHelper
    Public Shared Function GetGeomatryFromGeomatryString(ByVal featureGeomatryValue As String, ByVal csysWGS84 As MapInfo.Geometry.CoordSys) As Geometry
        Dim geometry As Geometry = Nothing
        geometry = New MapInfo.Ogc.FeatureGeometryFactory(csysWGS84).FeatureGeometryFromWKT(featureGeomatryValue)
        Return geometry
    End Function
    Public Shared Function GetGeomatryStringFromGeomatry(ByVal featureGeometry As FeatureGeometry) As String
        Dim wktValue As String = Nothing
        Dim feaGemfactory As FeatureGeometryFactory = New FeatureGeometryFactory(featureGeometry.CoordSys)
        If (feaGemfactory IsNot Nothing) Then
            If (featureGeometry.Type = GeometryType.Ellipse) Then
                featureGeometry = CType(featureGeometry, MapInfo.Geometry.Ellipse).CreateMultiPolygon(20)
            End If
            wktValue = feaGemfactory.FeatureGeometryToWKT(featureGeometry)
        End If
        Return wktValue
    End Function
End Class
