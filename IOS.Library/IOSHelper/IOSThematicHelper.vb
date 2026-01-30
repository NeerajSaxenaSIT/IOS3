Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports MapInfo.Mapping

Public Class IOSThematicHelper

    Private _FeatureLayre As FeatureLayer
    Public Property FeatureLayre() As FeatureLayer
        Get
            Return _FeatureLayre
        End Get
        Set(ByVal value As FeatureLayer)
            _FeatureLayre = value
        End Set
    End Property

    Private _IThemeList As List(Of FeatureStyleModifier)
    Public ReadOnly Property IThemeList() As List(Of FeatureStyleModifier)
        Get
            Return _IThemeList
        End Get
    End Property
    Private _ILegandList As List(Of IOSLegandInfo)
    Public ReadOnly Property ILegendList() As List(Of IOSLegandInfo)
        Get
            Return _ILegandList
        End Get
    End Property
    Private _Exception As System.Exception
    Public ReadOnly Property OutException() As System.Exception
        Get
            Return _Exception
        End Get
    End Property

    Public Sub New(ByRef FeatureLayer As FeatureLayer)
        Me._FeatureLayre = FeatureLayer
        _IThemeList = New List(Of FeatureStyleModifier)
    End Sub
    Public Sub ApplyIndividualValueTheme(ByVal expression As String, ByVal themealias As String, ByVal StylePart As Thematics.StylePart, Optional ByVal IsNumericNull As Boolean = False)
        Try
            Dim thm As MapInfo.Mapping.Thematics.IndividualValueTheme = New MapInfo.Mapping.Thematics.IndividualValueTheme(Me.FeatureLayre, expression, themealias)
            thm.Name = themealias
            If (IsNumericNull) Then
                thm.HasNumericNull = True
            End If
            thm.ApplyStylePart = StylePart
            thm.RecomputeBins()
            Me.FeatureLayre.Modifiers.Append(thm)
            Me._Exception = Nothing
        Catch ex As Exception
            Me._Exception = ex
        End Try

    End Sub

    Public Sub ApplyRangedTheme(ByVal expression As String, ByVal themealias As String)
        Dim thm As MapInfo.Mapping.Thematics.RangedTheme = New MapInfo.Mapping.Thematics.RangedTheme(Me.FeatureLayre, expression, themealias, 5, Thematics.DistributionMethod.CustomRanges)
        'Dim cl As Color

        'set bins
        thm.ApplyStylePart = Thematics.StylePart.Color
        thm.SpreadBy = Thematics.SpreadByPart.None
    End Sub
    Public Function ApplyThematicSettings(ByVal settingpath As String) As Boolean
        Dim st As IOSStyleModifier
        Dim p As String = settingpath + "\\" + Me.FeatureLayre.Alias + ".data"
        If IO.File.Exists(p) Then
            Dim stream As FileStream = File.OpenRead(p)
            Try
                Dim formatter As New BinaryFormatter()
                st = formatter.Deserialize(stream)
                stream.Close()
            Catch ex As Exception
                stream.Close()
                File.Delete(p)
                Return False
            End Try

            Dim index As Integer = 0
            While (st.FeatureLayer.Modifiers.Count > index)
                Dim Item As FeatureStyleModifier = st.FeatureLayer.Modifiers(0)
                Me.FeatureLayre.Modifiers.Remove(Item)
                Me.FeatureLayre.Modifiers.Append(Item)
                index = index + 1
                Me._IThemeList.Add(Item)
            End While
            _ILegandList = st.ILegendList
            If (Not Me.FeatureLayre.IsVisible) Then
                Me.FeatureLayre.Enabled = True
            End If

            Return True
        End If
        Return False
    End Function
    Public Function GenerateThematicXML(ByVal path As String) As DataTable
        Dim dt As New DataTable("Thematic")
        dt.Columns.Add("LayerName")
        dt.Columns.Add("ThemeName")
        dt.Columns.Add("Header")
        dt.Columns.Add("SubHeader")
        dt.WriteXml(path)
        Return dt
    End Function

End Class
