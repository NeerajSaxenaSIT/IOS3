Imports IOS.My
Imports MapInfo.Data
Imports MapInfo.Engine
Imports MapInfo.Geometry
Imports MapInfo.Mapping
Imports MapInfo.Styles
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.Odbc
Imports System.Drawing
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows.Forms
Imports MapInfo.Mapping.MapLayer
Imports MapInfo.Data.Table

Public Class ThreadGetMaps

    ' Fields
    Public connstring As String
    Public csysWGS84 As CoordSys
    Public date_selected As DateTime
    Public LayerConfig As DataRow
    Public sql_total As String
    Public TableName As String

    ' Events
    Public Event Thread_GetMaps_Complete(ByVal str As String, ByVal ti As Thread)

    ' Methods
    Private Function CheckLabelLayer(ByVal lyrname As String) As Boolean
        Dim flag As Boolean = False
        Dim num2 As Integer = (frmMapWindow.MapControl1.Map.Layers.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            If (frmMapWindow.MapControl1.Map.Layers.Item(i).Alias = lyrname) Then
                flag = True
            End If
            i += 1
        Loop
        Return flag
    End Function

    Public Function ColorInt2Color(ByVal colorint As Integer) As Color
        Dim blue As Integer = CInt((CLng(colorint) Mod &H100))
        Dim green As Integer = CInt(((CLng(colorint) / &H100) Mod &H100))
        Dim red As Integer = CInt((((CLng(colorint) / &H100) / &H100) Mod &H100))
        Return Color.FromArgb(&HFF, red, green, blue)
    End Function

    Private Function ColumnString_MapinfoTable(ByVal dt As Table) As String
        Dim enumerator As IEnumerator = Nothing
        Dim str2 As String = ""
        Try
            enumerator = dt.TableInfo.Columns.GetEnumerator
            Do While enumerator.MoveNext
                Dim current As Column = DirectCast(enumerator.Current, Column)
                If ((Not current.DataType = 9) And (Not current.DataType = 13)) Then
                    str2 = (str2 & current.Alias & ",")
                End If
            Loop
        Finally
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Return str2.TrimEnd(New Char() {","c})
    End Function

    Private Function CreateCircleGeometry(ByVal cp As DPoint, ByVal size As Integer) As FeatureGeometry
        Try
            Dim point As New DPoint((Convert.ToDouble(cp.x.ToString) - (CDbl(size) / 2000)), (Convert.ToDouble(cp.y.ToString) - (CDbl(size) / 3200)))
            Dim point2 As New DPoint((Convert.ToDouble(cp.x.ToString) + (CDbl(size) / 2000)), (Convert.ToDouble(cp.y.ToString) + (CDbl(size) / 3200)))

            Dim rect As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(point, point2)

            'Dim rect As New DRECT(point, point2)
            Dim ellipse As New Ellipse(Me.csysWGS84, rect)
            Return ellipse.CopyFeatureGeometry
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            Dim geometry As FeatureGeometry = Nothing
            ProjectData.ClearProjectError()
            Return geometry
            ProjectData.ClearProjectError()
        End Try
        Return Nothing
    End Function

    Private Sub CreateNetworkLabels(ByRef tbl As Table, ByVal LabelName As String, ByVal LabelSize As Integer, ByVal LabelColorInt As Integer)
        Try
            If Me.CheckLabelLayer(("Labels_" & tbl.Alias)) Then
                frmMapWindow.MapControl1.Map.Layers.Remove(("Labels_" & tbl.Alias))
            End If
            Dim layer As New MapInfo.Mapping.LabelLayer(("Labels_" & tbl.Alias))
            layer.Name = ("Labels_" & tbl.Alias)
            layer.Alias = ("Labels_" & tbl.Alias)
            frmMapWindow.MapControl1.Map.Layers.Insert(0, layer)
            Dim source As New MapInfo.Mapping.LabelSource(tbl)
            source.DefaultLabelProperties.Caption = LabelName
            source.DefaultLabelProperties.Style.Font = New MapInfo.Styles.Font("Arial Narrow", CDbl(LabelSize))
            source.DefaultLabelProperties.Style.Font.TextEffect = &H100
            source.DefaultLabelProperties.Style.Font.FontWeight = 400
            layer.Sources.Append(source)
            Dim color As Color = Me.ColorInt2Color(LabelColorInt)
            source.DefaultLabelProperties.Style.Font.Size = CDbl(LabelSize)
            source.DefaultLabelProperties.Style.Font.ForeColor = color
            source.DefaultLabelProperties.Layout.Alignment = 0
            Dim range As New VisibleRange(0, 4, 1)
            layer.VisibleRange = range
            layer.VisibleRangeEnabled = True
            frmMapWindow.MapControl1.Tools.SelectMapToolProperties.LabelsAreEditable = False
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Interaction.MsgBox(("Error: Network Label: " & tbl.Alias & " not mapped"), MsgBoxStyle.OkOnly, Nothing)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Function CreateNetworkStyle(ByVal lnwidth As Integer, ByVal linecolor As Integer) As CompositeStyle
        Dim style3 As CompositeStyle = New StyleFactory().FromMBString("Pen(1,2,0)")
        Dim color As Color = Me.ColorInt2Color(linecolor)
        Dim width As New LineWidth(CDbl(lnwidth), 0)
        Dim style5 As New SimpleLineStyle(width, 2, color)
        Dim interior As New SimpleInterior(0, color, color, False)
        Return New CompositeStyle(New AreaStyle(style5, interior))
    End Function

    Private Function CreatePieGeometry(ByVal cp As DPoint, ByVal azimuth As Integer, ByVal size As Integer, ByVal beamwidth As Integer) As FeatureGeometry
        Try
            Dim point As New DPoint((Conversions.ToDouble(cp.x.ToString) - (CDbl(size) / 2000)), (Conversions.ToDouble(cp.y.ToString) - (CDbl(size) / 3200)))
            Dim point2 As New DPoint((Conversions.ToDouble(cp.x.ToString) + (CDbl(size) / 2000)), (Conversions.ToDouble(cp.y.ToString) + (CDbl(size) / 3200)))

            Dim rect As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(point, point2)

            'Dim rect As New DRECT = point, point2
            Dim arc As New LegacyArc(Me.csysWGS84, rect, (((azimuth * -1) + 90) - (CDbl(beamwidth) / 2)), (((azimuth * -1) + 90) + (CDbl(beamwidth) / 2)))
            Dim source As DPoint() = arc.CreateMultiCurve(20).Item(0).SamplePoints
            source = DirectCast(Utils.CopyArray(DirectCast(source, Array), New DPoint((source.Count() + 1) - 1) {}), DPoint())
            source((source.Count() - 1)) = cp
            Return New MultiPolygon(Me.csysWGS84, 0, source)
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            Dim geometry As FeatureGeometry = Nothing
            ProjectData.ClearProjectError()
            Return geometry
            ProjectData.ClearProjectError()
        End Try
        Return Nothing
    End Function

    Private Function CreateTableFromExisting(ByVal tableToIndex As Table, ByVal columnAliasToIndex As String, ByVal AliasForNewTable As String, ByVal FilePathToSaveNativeTable As String, ByVal CloseOldTable As Boolean, ByVal cStyle As CompositeStyle, ByVal cSize As Integer, ByVal cBeam As Integer) As Table
        Dim con As New MapInfo.Data.MIConnection
        Dim com As MapInfo.Data.MICommand = Nothing

        Try
            Dim ti As MapInfo.Data.TableInfoNative = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(AliasForNewTable, MapInfo.Data.TableType.Native, tableToIndex), MapInfo.Data.TableInfoNative)
            ti.Columns("CELLID").Indexed = True
            ti.Columns("SITECODE").Indexed = True
            ti.Columns("LAC").Indexed = True
            ti.Columns("RecordID").Indexed = True
            ti.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn(Me.csysWGS84))
            ti.Columns.Add(ColumnFactory.CreateStyleColumn())
            ti.Alias = AliasForNewTable
            ti.TablePath = FilePathToSaveNativeTable + AliasForNewTable + ".tab"
            ti.WriteTabFile()

            Dim nativeTable As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(ti)
            nativeTable.Close()

            nativeTable = MapInfo.Engine.Session.Current.Catalog.OpenTable(FilePathToSaveNativeTable + AliasForNewTable + ".tab")

            'Populating Native Table with data
            nativeTable.BeginAccess(TableAccessMode.Write)
            con.Open()
            com = con.CreateCommand()

            Dim totalrecords As Integer

            'populating native table with all values of datatable
            com = con.CreateCommand()
            com.CommandText = "Insert into " + nativeTable.Alias + " (" + ColumnString_MapinfoTable(nativeTable) + ") Select " & ColumnString_MapinfoTable(tableToIndex) + " from " + tableToIndex.Alias
            com.Prepare()
            totalrecords = com.ExecuteNonQuery()
            com.Dispose()

            'adding geo and style
            '++++++++++++++++++++
            'style
            Dim csys As CoordSys = Me.csysWGS84


            'Geo
            Dim gtemp As FeatureGeometry = GetLineFeature(New DPoint(4, 52), New DPoint(4, 53), csys)
            'Preparing update query
            com = con.CreateCommand()
            com.CommandText = "Update " + nativeTable.Alias + " set obj = @Obj, MI_Style = @style where RecordID = @RecordID"
            com.Parameters.Add("@Obj", gtemp)
            com.Parameters.Add("@style", cStyle)
            com.Parameters.Add("@RecordID", 1)

            Dim recordsupdated As Integer
            com.Prepare()

            Dim ftr As New Feature(nativeTable.TableInfo.Columns)
            Dim i As Integer = 1

            If nativeTable.Alias.Contains("NanoBTS") Then
                For Each ftr In nativeTable
                    If Not ftr.Item("X") Is Nothing Then
                        '    com.Parameters("@Obj").Value = MapInfo.Geometry.MultiCurve.CreateLine(csys, New DPoint(ftr.Item("X"), ftr.Item("Y")), New DPoint(ftr.Item("DX"), ftr.Item("DY")))
                        com.Parameters("@Obj").Value = CreateCircleGeometry(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), cSize * Math.Max(CInt(ftr.Item("MinOfDist")), 1))
                        com.Parameters("@style").Value = cStyle
                        com.Parameters("@RecordID").Value = ftr.Item("RecordID")
                        recordsupdated = com.ExecuteNonQuery()
                    End If

                    i = i + 1
                Next
            Else
                For Each ftr In nativeTable
                    If Not ftr.Item("X") Is Nothing Then
                        If CInt(ftr.Item("AZIMUTH")) = 360 Then
                            com.Parameters("@Obj").Value = CreateCircleGeometry(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), cSize * Math.Max(CInt(ftr.Item("MinOfDist")), 1))
                        Else
                            com.Parameters("@Obj").Value = CreatePieGeometry(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), ftr.Item("AZIMUTH"), cSize * Math.Max(CInt(ftr.Item("MinOfDist")), 1), cBeam)
                        End If
                        com.Parameters("@style").Value = cStyle
                        com.Parameters("@RecordID").Value = ftr.Item("RecordID")
                        recordsupdated = com.ExecuteNonQuery()
                    End If

                    i = i + 1
                Next
            End If

            nativeTable.EndAccess()
            com.Dispose()


            con.Close()
            con.Dispose()
            con = Nothing

            Return nativeTable
        Catch ex As Exception
            If Not com Is Nothing Then
                com.Dispose()
                com = Nothing
            End If

            con.Close()
            con.Dispose()
            con = Nothing

            Return Nothing
        End Try
        Return Nothing
    End Function

    Public Function GetData() As DataSet
        Dim cnQODBC As System.Data.Odbc.OdbcConnection = Nothing
        Dim daQODBC As System.Data.Odbc.OdbcDataAdapter = Nothing
        Dim dtQODBC As New DataTable()
        Dim ds As New DataSet

        Try
            cnQODBC = New System.Data.Odbc.OdbcConnection(connstring)
            cnQODBC.ConnectionTimeout = 5
            cnQODBC.Open()
            daQODBC = New System.Data.Odbc.OdbcDataAdapter(sql_total, cnQODBC)
            daQODBC.SelectCommand.CommandTimeout = 0
            ds = New System.Data.DataSet
            daQODBC.Fill(ds)
            cnQODBC.Close()
            daQODBC.Dispose()
            cnQODBC.Dispose()
        Catch

        Finally
            If Not cnQODBC Is Nothing Then
                cnQODBC.Close()
            End If
            If Not daQODBC Is Nothing Then
                daQODBC.Dispose()
            End If
            If Not cnQODBC Is Nothing Then
                cnQODBC.Dispose()
            End If
        End Try
        Return ds
    End Function

    Private Function GetLineFeature(ByRef p1 As DPoint, ByRef p2 As DPoint, ByVal csys2 As CoordSys) As Geometry
        Return MultiCurve.CreateLine(csys2, p1, p2)
    End Function

    Public Sub LoadNetwork()
        Try
            Dim tableName As String = Me.TableName
            Dim table As New DataTable
            table = Me.GetData.Tables.Item(0)
            Dim connection As New MIConnection
            Dim command As MICommand = connection.CreateCommand
            connection.Open()
            connection.Catalog.CloseTable(tableName)
            Dim net As New TableInfoAdoNet(("Temp_" & tableName), table)
            Dim tableToIndex As Table = connection.Catalog.OpenTable(net)
            Dim table2 As Table = Me.CreateTableFromExisting(tableToIndex, "CellID", tableName, (GetUserDataPath() & "\Data\"), True, Me.CreateNetworkStyle(Conversions.ToInteger(Me.LayerConfig.Item("LayerLineWidth").ToString), Conversions.ToInteger(Me.LayerConfig.Item("LayerLineColor").ToString)), Conversions.ToInteger(Me.LayerConfig.Item("LayerRelativeSize").ToString), Conversions.ToInteger(Me.LayerConfig.Item("LayerBeamWidth").ToString))
            tableToIndex.Close()
            connection.Catalog.CloseTable(("Labels_" & tableName))
            If (Not table2 Is Nothing) Then
                connection.Close()
                connection.Dispose()
                'handler = Me.Thread_GetMaps_CompleteEventHandler1
                RaiseEvent Thread_GetMaps_Complete(table2.Alias, Thread.CurrentThread)
                ' If (Not handler Is Nothing) Then
                'handler.Invoke(table2.Alias, Thread.CurrentThread)
                'End If
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            RaiseEvent Thread_GetMaps_Complete(Nothing, Thread.CurrentThread)
            ProjectData.ClearProjectError()
        End Try
    End Sub

End Class