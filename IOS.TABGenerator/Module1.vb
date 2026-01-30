Imports System.Configuration
Imports System.Drawing
Imports System.IO
Imports System.IO.Compression
Imports MapInfo.Data
Imports MapInfo.Engine
Imports MapInfo.Geometry
Imports MapInfo.Styles
Imports TriangleNet
Imports IOS.DataLibrary

Module Module1

    Dim isAllNetworkArea As Boolean = False
    Dim fieldName As String
    Dim sharedTabFilePath As String
    Dim localTabFilePath As String
    Dim autoSuffixAddition As Boolean = False
    Dim csysWGS84 As MapInfo.Geometry.CoordSys
    Dim dt_Map_Configuration As New DataTable
    Dim selected_date As Date

    Dim FailedAndRetry As Int16 = 0

    Sub Main()
        Try

            Console.WriteLine("Generating TAB files...")
            isAllNetworkArea = CBool(ConfigurationManager.AppSettings("IsAllNetworkArea"))
            autoSuffixAddition = CBool(ConfigurationManager.AppSettings("AutoSuffixAddition"))
            dt_Map_Configuration = DataAccessorODBC.GetDataTable(ConfigurationManager.ConnectionStrings("IOSServer").ToString(), "Select * from IOS_Map_Configuration WHERE LayerActive = 1 ORDER BY LayerOrder")
            selected_date = DateTime.Now()

            If dt_Map_Configuration Is Nothing Then
                Console.WriteLine("DT Map Config Is Not Loaded...")
                Exit Sub
            Else
                Console.WriteLine("DT Map Config Loaded...")
            End If

            'selected_date = Convert.ToDateTime("12/18/2013")       'Testing purpose only

            If dt_Map_Configuration.Rows.Count > 0 Then
                fieldName = ConfigurationManager.AppSettings("FieldName")
                sharedTabFilePath = ConfigurationManager.AppSettings("SharedTabFilePath") & "/" & selected_date.ToString("yyyyMMdd")
                localTabFilePath = ConfigurationManager.AppSettings("LocalTabFilePath") & "\" & selected_date.ToString("yyyyMMdd")
                csysWGS84 = Session.Current.CoordSysFactory.CreateLongLat(157)

                Dim cols_dist() As String = {"LayerTechnology"}
                Dim distinctLayerTechnologyView As DataView = dt_Map_Configuration.DefaultView.ToTable(True, cols_dist).DefaultView
                distinctLayerTechnologyView.Sort = "LayerTechnology DESC"
                Dim distinctLayerTechnology As DataTable = distinctLayerTechnologyView.ToTable

                Console.WriteLine("Starting Load Network Cells...")

                If isAllNetworkArea Then
                    sharedTabFilePath = sharedTabFilePath & "/ALL/"
                    localTabFilePath = localTabFilePath & "\ALL\"

                    For Each drow As DataRow In dt_Map_Configuration.Rows
                        If Not drow("LayerTechnology").ToString.StartsWith("TX") Then
                            LoadNetworks_Cells(drow, "ALL")

                            If FailedAndRetry = 1 Then
                                LoadNetworks_Cells(drow, "ALL")
                                FailedAndRetry = 0
                            End If
                        End If
                    Next

                    For Each drow As DataRow In distinctLayerTechnology.Rows
                        If Not drow("LayerTechnology").ToString.StartsWith("TX") Then
                            LoadNetworks_Sites(drow, "ALL")
                        End If
                    Next

                    ' Generating transmission network layer files
                    If dt_Map_Configuration.Select("LayerTechnology = 'TX'").Count > 0 Then
                        Dim dtMapConfigTempTx As DataTable = dt_Map_Configuration.Select("LayerTechnology = 'TX'").CopyToDataTable
                        For Each drow As DataRow In dtMapConfigTempTx.Rows
                            LoadNetworks_Links(drow, "ALL")
                        Next
                    End If
                Else
                    If dt_Map_Configuration.Columns.Contains(fieldName) Then
                        Dim cols_distinct() As String = {fieldName}
                        Dim distinctLayerField As DataTable = dt_Map_Configuration.DefaultView.ToTable(True, cols_distinct)
                        For Each row As DataRow In distinctLayerField.Select(fieldName & " IS NOT NULL")
                            sharedTabFilePath = ConfigurationManager.AppSettings("SharedTabFilePath") & selected_date.ToString("yyyyMMdd") & "/" & row.Item(fieldName) & "/"
                            localTabFilePath = ConfigurationManager.AppSettings("LocalTabFilePath") & "\" & selected_date.ToString("yyyyMMdd") & "\" & row.Item(fieldName) & "\"
                            For Each drow As DataRow In dt_Map_Configuration.Select(fieldName & "='" & row.Item(fieldName) & "' And LayerTechnology <> 'TX'")
                                LoadNetworks_Cells(drow, row.Item(fieldName))
                            Next
                            For Each drow As DataRow In distinctLayerTechnology.Select("LayerTechnology <> 'TX'")
                                LoadNetworks_Sites(drow, row.Item(fieldName))
                            Next
                            For Each drow As DataRow In dt_Map_Configuration.Select(fieldName & "='" & row.Item(fieldName) & "' And LayerTechnology = 'TX'")
                                LoadNetworks_Links(drow, row.Item(fieldName))
                            Next
                        Next
                    End If
                End If
                Console.WriteLine("TAB files generated.")
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message & vbLf & ex.StackTrace)
        End Try
    End Sub

    Public Sub LoadNetworks_Sites(ByVal layerConfig As DataRow, ByVal networkArea As String)
        Dim connection As MIConnection = New MIConnection
        Try
            Dim dateid As String
            dateid = Chr(39) + selected_date.ToString("yyyyMMdd") + Chr(39)
            'LOAD SITES
            Dim sitetable As New System.Data.DataTable

            Dim tblname As String = "Sites_" & layerConfig.Item("LayerTechnology").ToString
            Dim sql As String = ""
            If networkArea = "ALL" Then
                sql = "SELECT * FROM qry_IOS_Sites" & layerConfig.Item("LayerTechnology").ToString & " WHERE NetworkDate = " & dateid
            Else
                sql = "SELECT * FROM qry_IOS_Sites" & layerConfig.Item("LayerTechnology").ToString & " WHERE NetworkDate = " & dateid & "  and " & fieldName & " = " & Chr(39) & networkArea & Chr(39)
            End If

            sitetable = DataAccessorODBC.GetDataTable(ConfigurationManager.ConnectionStrings("IOSServer").ToString(), sql)
            If sitetable Is Nothing Then
                Exit Sub
            End If

            Dim siteData As New DataTable
            If isAllNetworkArea Then
                siteData = sitetable.Copy()
            Else
                siteData = sitetable.Select(fieldName & "='" & networkArea & "'").CopyToDataTable()
            End If

            If connection.State = ConnectionState.Closed Then
                connection.Open()
            End If
            connection.Catalog.CloseTable(tblname & "_" & networkArea)
            Dim ti2 As TableInfoAdoNet = New TableInfoAdoNet("Temp" & tblname & "_" & networkArea)
            ti2.DataTable = siteData

            Dim LayerLineColor As Integer = dt_Map_Configuration.Select("LayerTechnology='" & layerConfig("LayerTechnology").ToString & "'")(0)("LayerLinecolor")
            Dim techPointSize As Double = Nothing
            Dim dStyle As Style = Nothing
            Dim Sitecolor As Color = Color.Black

            If layerConfig.Item("LayerTechnology").ToString = "2G" Then
                techPointSize = 24
                Sitecolor = Color.Red
            ElseIf layerConfig.Item("LayerTechnology").ToString = "3G" Then
                techPointSize = 18
                Sitecolor = Color.Blue
            ElseIf layerConfig.Item("LayerTechnology").ToString = "4G" Then
                techPointSize = 12
                Sitecolor = Color.Orange
            ElseIf layerConfig.Item("LayerTechnology").ToString = "5G" Then
                techPointSize = 6
                Sitecolor = Color.DarkGreen
            End If

            dStyle = New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.FontPointStyle(35, New MapInfo.Styles.Font("MapInfo Symbols", 6), 0, Sitecolor, techPointSize))

            Dim xy As SpatialSchemaXY = New SpatialSchemaXY
            xy.XColumn = "X"
            xy.YColumn = "Y"
            xy.NullPoint = "0.0, 0.0"
            ' Any customer at 0,0 means we don't know their location.
            xy.DefaultStyle = dStyle
            xy.CoordSys = Session.Current.CoordSysFactory.CreateLongLat(csysWGS84.Datum)
            ti2.SpatialSchema = xy
            ' Now set the spatial schema

            Dim tbl2 As Table = connection.Catalog.OpenTable(ti2)
            Dim newtbl2 As MapInfo.Data.Table

            newtbl2 = CreateNetworkTable_Sites(tbl2, "SITECODE", tblname & "_" & networkArea, sharedTabFilePath, True, networkArea)
            tbl2.Close()
            Dim SharedTabFileFullName As String
            Dim LocalTabFileFullName As String
            If newtbl2 Is Nothing Then
                connection.Close()
                connection.Dispose()
                Exit Sub
            Else
                If Not Directory.Exists(localTabFilePath) Then
                    Directory.CreateDirectory(localTabFilePath)
                End If
                SharedTabFileFullName = sharedTabFilePath & tblname & "_" & networkArea & ".zip"
                LocalTabFileFullName = localTabFilePath & tblname & "_" & networkArea & ".zip"
                CreateZipFileAndInsertTABFile(selected_date.ToString("yyyyMMdd"), networkArea, SharedTabFileFullName, LocalTabFileFullName, tblname & "_" & networkArea)
            End If


            For Each sFile As String In Directory.GetFiles(localTabFilePath)
                If Not sFile.Contains("*.zip") Then
                    Try
                        'File.Delete(sFile)
                    Catch ex As Exception

                    End Try

                End If
            Next

            connection.Close()
            connection.Dispose()
            GC.Collect()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            If connection.State = ConnectionState.Open Then
                connection.Close()
                connection.Dispose()
            End If
        End Try
    End Sub

    Public Sub LoadNetworks_Cells(ByVal layerConfig As DataRow, ByVal networkArea As String)
        Dim connection As MIConnection = New MIConnection
        Try
            Dim dateid As String
            dateid = Chr(39) + selected_date.ToString("yyyyMMdd") + Chr(39)

            Dim sql As String = Replace(layerConfig.Item("LayerSQL").ToString, "@networkdate", dateid)
            Dim tblname As String = layerConfig.Item("LayerName").ToString
            Dim starttime As DateTime = Now()

            'LOAD CELLS
            Dim networktbl As New System.Data.DataTable
            networktbl = GetTableFromODBCSchema(ConfigurationManager.ConnectionStrings("IOSServer").ToString(), sql)
            If networktbl Is Nothing Then
                Exit Sub
            End If

            Dim networkData As New DataTable
            'If isAllNetworkArea Then
            networkData = networktbl.Copy()
                'Else
                '    networkData = networktbl.Select(fieldName & "='" & networkArea & "'").CopyToDataTable()
                'End If

                Dim command As MICommand = connection.CreateCommand()
            Dim tbl As MapInfo.Data.Table = Nothing
            connection.Open()
            Dim targetTableName As String = ""

            If autoSuffixAddition = False Then
                targetTableName = tblname
            Else
                targetTableName = tblname & "_" & networkArea
            End If

            Console.WriteLine("TargetTable Downloaded Cells:  " & targetTableName)
            connection.Catalog.CloseTable(targetTableName)
            Dim ti As TableInfoAdoNet = New TableInfoAdoNet("Temp_" & tblname & "_" & networkArea, networkData)
            tbl = connection.Catalog.OpenTable(ti)

            Dim newtbl As MapInfo.Data.Table = Nothing
            newtbl = CreateNetworkTable_Cells(tbl, "CellID", targetTableName, sharedTabFilePath, True, CreateNetworkStyle(CInt(layerConfig.Item("LayerLineWidth").ToString), CInt(layerConfig.Item("LayerLineColor").ToString)), CInt(layerConfig.Item("LayerRelativeSize").ToString), CInt(layerConfig.Item("LayerBeamWidth").ToString), layerConfig.Item("LayerPolygonType").ToString, networkArea)
            Console.WriteLine("TargetTable Generated Cells:  " & targetTableName)



            tbl.Close()
            tbl = Nothing
            ti.Dispose()
            ti = Nothing

            Dim SharedTabFileFullName As String
            Dim LocalTabFileFullName As String
            If newtbl Is Nothing Then
                connection.Close()
                connection.Dispose()
                Exit Sub
            Else
                If Not Directory.Exists(localTabFilePath) Then
                    Directory.CreateDirectory(sharedTabFilePath)
                End If
                SharedTabFileFullName = sharedTabFilePath & targetTableName & ".zip"
                LocalTabFileFullName = localTabFilePath & targetTableName & ".zip"
                CreateZipFileAndInsertTABFile(selected_date.ToString("yyyyMMdd"), networkArea, SharedTabFileFullName, LocalTabFileFullName, tblname)
            End If
            Console.WriteLine("TargetTable Link Uploaded:  " & targetTableName)


            'Voronoi Calculation
            If autoSuffixAddition = False Then
                targetTableName = tblname & "_Voronoi"
            Else
                targetTableName = tblname & "_Voronoi" & "_" & networkArea
            End If

            connection.Catalog.CloseTable(targetTableName)

            Dim dtVerifyVoronoiCalc As DataTable = networkData.DefaultView.ToTable(True, {"x", "y"})

            If networktbl.Rows.Count >= 9 AndAlso dtVerifyVoronoiCalc.Rows.Count >= 10 Then
                CreateNetworkTable_Voronoi(newtbl, "CellID", targetTableName, sharedTabFilePath, True, "RecordID", networkArea)
                SharedTabFileFullName = sharedTabFilePath & targetTableName & ".zip"
                LocalTabFileFullName = localTabFilePath & targetTableName & ".zip"

                Console.WriteLine("Voronoi Generated:  " & targetTableName)
                CreateZipFileAndInsertTABFile(selected_date.ToString("yyyyMMdd"), networkArea, SharedTabFileFullName, LocalTabFileFullName, targetTableName)

                Console.WriteLine("TargetTable Voronoi Generated:  " & targetTableName)

            Else
                Console.WriteLine("TargetTable Voronoi Generation Skipped due to low records:  " & targetTableName)
            End If

            networktbl.Dispose()
            networktbl = Nothing
            networkData.Dispose()
            networkData = Nothing
            dtVerifyVoronoiCalc.Dispose()
            dtVerifyVoronoiCalc = Nothing
            newtbl = Nothing

            For Each sFile As String In Directory.GetFiles(localTabFilePath)
                If Not sFile.Contains(".zip") Then
                    Try
                        'File.Delete(sFile)
                    Catch

                    End Try
                End If
            Next

            connection.Close()
            connection.Dispose()
            GC.Collect()

        Catch ex As Exception
            Console.WriteLine("LoadNetwork_Cells - " & ex.Message)
            If connection.State = ConnectionState.Open Then
                connection.Close()
                connection.Dispose()
            End If
        End Try
    End Sub

    Public Sub LoadNetworks_Links(ByVal layerConfig As DataRow, ByVal networkArea As String)
        Dim connection As MIConnection = New MIConnection
        Try
            Dim dateid As String
            dateid = Chr(39) + selected_date.ToString("yyyyMMdd") + Chr(39)

            Dim sql As String = Replace(layerConfig.Item("LayerSQL").ToString, "@networkdate", dateid)
            Dim tblname As String = layerConfig.Item("LayerName").ToString
            Dim starttime As DateTime = Now()

            'LOAD LINKS
            Dim networktbl As New System.Data.DataTable
            networktbl = GetTableFromODBCSchema(ConfigurationManager.ConnectionStrings("IOSServer").ToString(), sql)
            If networktbl Is Nothing Then
                Exit Sub
            End If

            Dim networkData As New DataTable
            If isAllNetworkArea Then
                networkData = networktbl.Copy()
            Else
                networkData = networktbl.Select(fieldName & "='" & networkArea & "'").CopyToDataTable()
            End If

            Dim command As MICommand = connection.CreateCommand()
            Dim tbl As MapInfo.Data.Table
            connection.Open()
            Dim targetTableName As String = ""

            If autoSuffixAddition = False Then
                targetTableName = tblname
            Else
                targetTableName = tblname & "_" & networkArea
            End If


            Console.WriteLine("TargetTable Downloaded Links:  " & targetTableName)

            connection.Catalog.CloseTable(targetTableName)
            Dim ti As TableInfoAdoNet = New TableInfoAdoNet("Temp_" & tblname & "_" & networkArea, networkData)
            tbl = connection.Catalog.OpenTable(ti)
            Dim newtbl As MapInfo.Data.Table = Nothing
            newtbl = CreateNetworkTable_Links(tbl, "CellID", tblname, sharedTabFilePath, True, CreateNetworkStyle(CInt(layerConfig.Item("LayerLineWidth").ToString), CInt(layerConfig.Item("LayerLineColor").ToString)), CInt(layerConfig.Item("LayerRelativeSize").ToString), CInt(layerConfig.Item("LayerBeamWidth").ToString), layerConfig.Item("LayerPolygonType").ToString)
            Console.WriteLine("TargetTable Generated Links:  " & targetTableName)

            tbl.Close()
            Dim SharedTabFileFullName As String
            Dim LocalTabFileFullName As String
            If newtbl Is Nothing Then
                connection.Close()
                connection.Dispose()
                Exit Sub
            Else
                If Not Directory.Exists(localTabFilePath) Then
                    Directory.CreateDirectory(sharedTabFilePath)
                End If
                SharedTabFileFullName = sharedTabFilePath & targetTableName & ".zip"
                LocalTabFileFullName = localTabFilePath & targetTableName & ".zip"
                CreateZipFileAndInsertTABFile(selected_date.ToString("yyyyMMdd"), networkArea, SharedTabFileFullName, LocalTabFileFullName, tblname)
            End If

            'NetworkLines
            Dim tblName_lines As String = layerConfig.Item("LayerName").ToString & "_Lines"
            Dim command_lines As MICommand = connection.CreateCommand()
            Dim tbl_lines As MapInfo.Data.Table
            connection.Catalog.CloseTable(tblName_lines)

            Dim ti_lines As TableInfoAdoNet = New TableInfoAdoNet("Temp_" & tblname & "_" & networkArea, networkData)
            tbl_lines = connection.Catalog.OpenTable(ti_lines)
            Dim newtbl_lines As MapInfo.Data.Table = Nothing
            newtbl_lines = CreateNetworkTable_LinksLines(tbl_lines, "CellID", tblName_lines, sharedTabFilePath, True, CreateNetworkStyle(1, 12237770), CInt(layerConfig.Item("LayerRelativeSize").ToString), CInt(layerConfig.Item("LayerBeamWidth").ToString), layerConfig.Item("LayerPolygonType").ToString)
            Console.WriteLine("TargetTable Generated Links Lines:  " & targetTableName & "_Lines")

            tbl_lines.Close()
            If newtbl_lines Is Nothing Then
                connection.Close()
                connection.Dispose()
                Exit Sub
            Else
                If Not Directory.Exists(localTabFilePath) Then
                    Directory.CreateDirectory(sharedTabFilePath)
                End If
                SharedTabFileFullName = sharedTabFilePath & targetTableName & "_Lines" & ".zip"
                LocalTabFileFullName = localTabFilePath & targetTableName & "_Lines" & ".zip"
                CreateZipFileAndInsertTABFile(selected_date.ToString("yyyyMMdd"), networkArea, SharedTabFileFullName, LocalTabFileFullName, tblname)
            End If

            'Voronoi Calculation
            If autoSuffixAddition = False Then
                targetTableName = tblname & "_Voronoi"
            Else
                targetTableName = tblname & "_Voronoi" & "_" & networkArea
            End If

            connection.Catalog.CloseTable(targetTableName)
            CreateNetworkTable_Voronoi(newtbl, "CellID", targetTableName, sharedTabFilePath, True, "RecordID", networkArea)
            SharedTabFileFullName = sharedTabFilePath & targetTableName & ".zip"
            LocalTabFileFullName = localTabFilePath & targetTableName & ".zip"

            CreateZipFileAndInsertTABFile(selected_date.ToString("yyyyMMdd"), networkArea, SharedTabFileFullName, LocalTabFileFullName, tblname)

            connection.Close()
            connection.Dispose()
            GC.Collect()

        Catch ex As Exception
            Console.WriteLine("LoadNetworks_Links - " & ex.Message)
            If connection.State = ConnectionState.Open Then
                connection.Close()
                connection.Dispose()
            End If
        End Try
    End Sub

    Public Function GetTableFromODBCSchema(ByVal connstring As String, ByVal sql As String) As DataTable
        Dim table2 As DataTable
        If ((sql = "") Or (connstring = "")) Then
            Return Nothing
        End If
        Dim selectConnection As System.Data.Odbc.OdbcConnection = Nothing
        Dim adapter As System.Data.Odbc.OdbcDataAdapter = Nothing
        Dim dataSet As DataSet = Nothing
        Dim table As New DataTable

        Try
            Dim enumerator As IEnumerator = Nothing
            selectConnection = New System.Data.Odbc.OdbcConnection(connstring) With {.ConnectionTimeout = 60}
            selectConnection.Open()
            adapter = New System.Data.Odbc.OdbcDataAdapter(sql, selectConnection)
            dataSet = New DataSet
            adapter.SelectCommand.CommandTimeout = 60
            adapter.FillSchema(dataSet, SchemaType.Source)
            Try
                enumerator = dataSet.Tables.Item(0).Columns.GetEnumerator
                Do While enumerator.MoveNext
                    Dim current As DataColumn = DirectCast(enumerator.Current, DataColumn)
                    '    Console.WriteLine(current.ColumnName & "  " & current.DataType.ToString)
                    If (current.DataType.ToString = "System.String") Then
                        If current.ColumnName.Contains("CELL") Then
                            current.MaxLength = Math.Min(250, current.MaxLength)
                            current.ExtendedProperties.Add("StringWidth", Math.Min(250, current.MaxLength))
                        Else
                            If (current.MaxLength = &HFF) Then
                                current.MaxLength -= 1
                                current.ExtendedProperties.Add("StringWidth", (current.MaxLength - 1))
                                Continue Do
                            End If
                            current.ExtendedProperties.Add("StringWidth", current.MaxLength)
                        End If
                    End If
                Loop
            Finally
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
            dataSet.EnforceConstraints = False
            adapter.Fill(dataSet)
            selectConnection.Close()
            table = dataSet.Tables.Item(0)
            selectConnection.Dispose()
            adapter.Dispose()
            table2 = table
        Catch exception1 As Exception
            Dim exception As Exception = exception1
            If (Not adapter Is Nothing) Then
                adapter.Dispose()
            End If
            If (Not dataSet Is Nothing) Then
                dataSet.Dispose()
            End If
            If (Not table Is Nothing) Then
                table.Dispose()
            End If
            If (Not selectConnection Is Nothing) Then
                selectConnection.Close()
                selectConnection.Dispose()
            End If
            table2 = Nothing
            Return table2
        Finally
            If (Not selectConnection Is Nothing) Then
                selectConnection.Close()
                selectConnection.Dispose()
            End If
        End Try
        Return table2
    End Function

    Private Function CreateNetworkTable_Sites(ByRef tableToIndex As MapInfo.Data.Table, ByVal columnAliasToIndex As String, ByVal AliasForNewTable As String, ByVal FilePathToSaveNativeTable As String, ByVal CloseOldTable As Boolean, ByVal networkArea As String) As MapInfo.Data.Table
        Dim con As New MapInfo.Data.MIConnection
        Dim com As MapInfo.Data.MICommand = Nothing

        Try
            Dim TabFileFullName As String = localTabFilePath + AliasForNewTable + ".tab"
            Dim ti As MapInfo.Data.TableInfoNative = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(AliasForNewTable, MapInfo.Data.TableType.Native, tableToIndex), MapInfo.Data.TableInfoNative)
            ti.Columns("SITECODE").Indexed = True
            ti.Alias = AliasForNewTable
            ti.TablePath = TabFileFullName

            If Not System.IO.Directory.Exists(localTabFilePath) Then
                System.IO.Directory.CreateDirectory(localTabFilePath)
            End If

            ti.WriteTabFile()

            Dim nativeTable As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(ti)
            nativeTable.Close()

            nativeTable = MapInfo.Engine.Session.Current.Catalog.OpenTable(TabFileFullName)

            'Populating Native Table with data
            nativeTable.BeginAccess(TableAccessMode.Write)
            con.Open()
            com = con.CreateCommand()

            Dim totalrecords As Integer

            'populating native table with all values of datatable
            com = con.CreateCommand()
            com.CommandText = "Insert into " + nativeTable.Alias + " (obj, MI_Style, " + ColumnString_MapinfoTable(nativeTable) + ") Select obj, MI_Style, " & ColumnString_MapinfoTable(tableToIndex) + " from " + tableToIndex.Alias
            com.Prepare()
            totalrecords = com.ExecuteNonQuery()
            com.Dispose()

            nativeTable.EndAccess()
            com.Dispose()

            con.Close()
            con.Dispose()
            con = Nothing
            GC.Collect()

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

    Private Sub CreateZipFileAndInsertTABFile(NetworkDate As String, NetworkArea As String, ZipFileFullName As String, LocalZipFileFullName As String, layername As String)
        Try
            Dim di As New DirectoryInfo(localTabFilePath)
            Dim fiArr As FileInfo() = Nothing
            fiArr = GetFiles(di, "*" & layername & ".*").Where(Function(x) Not x.Extension.ToString.ToLower.Contains("zip")).ToArray()

            If System.IO.File.Exists(LocalZipFileFullName) = True Then
                System.IO.File.Delete(LocalZipFileFullName)
                Console.WriteLine("Deleted Old File - " & LocalZipFileFullName)
            End If

            Using archive As ZipArchive = ZipFile.Open(LocalZipFileFullName, ZipArchiveMode.Update)
                For Each fi As FileInfo In fiArr
                    If fi.FullName <> LocalZipFileFullName Then
                        archive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal)
                    End If
                Next

            End Using
            ZipFileFullName = Replace(ZipFileFullName, "\", "/")
            Console.WriteLine("CreateZip Success - " & LocalZipFileFullName)
        Catch ex As Exception
            Console.WriteLine("CreateZip Failed - " & LocalZipFileFullName & "  - " & ex.Message)
        End Try

        Try
            DataAccessorODBC.ExecuteScalar(ConfigurationManager.ConnectionStrings("IOSServer").ToString(), "Delete from IOS_Network_TabFiles Where NetworkDate=" & NetworkDate & " And NetworkArea='" & NetworkArea & "' And TABFileName='" & ZipFileFullName & "'")
            DataAccessorODBC.ExecuteNonQuery(ConfigurationManager.ConnectionStrings("IOSServer").ToString(), "INSERT INTO IOS_Network_TabFiles(NetworkDate,NetworkArea,TABFileName) Values(" & NetworkDate & ",'" & NetworkArea & "','" & ZipFileFullName & "')")
        Catch ex As Exception
            Console.WriteLine("Insert Link Failed - " & ZipFileFullName & "  - " & ex.Message)
        End Try
    End Sub

    Private Function CreateNetworkStyle(ByVal lnwidth As Integer, ByVal linecolor As Integer) As MapInfo.Styles.CompositeStyle
        Dim sf As StyleFactory = New StyleFactory()
        Dim cs As CompositeStyle
        cs = sf.FromMBString("Pen(1,2,0)")
        Dim cStyle As CompositeStyle
        Dim networkcolor As Color = ColorInt2Color(linecolor)
        Dim lw As LineWidth = New LineWidth(lnwidth, LineWidthUnit.Pixel)
        Dim vStyle As SimpleLineStyle = New SimpleLineStyle(lw, 2, networkcolor)
        Dim iStyle As SimpleInterior = New SimpleInterior(0, networkcolor, networkcolor, False)
        Dim astyle As New MapInfo.Styles.AreaStyle(vStyle, iStyle)
        cStyle = New MapInfo.Styles.CompositeStyle(astyle)
        Return cStyle
    End Function

    Public Function ColorInt2Color(ByVal colorint As Integer) As Color
        Dim color_r, color_g, color_b As Integer
        color_b = CLng(colorint) Mod 256
        color_g = (CLng(colorint) \ 256) Mod 256
        color_r = ((CLng(colorint) \ 256) \ 256) Mod 256
        Return Color.FromArgb(255, color_r, color_g, color_b)
    End Function

    Private Function CreateNetworkTable_Voronoi(ByRef SourceTable As MapInfo.Data.Table, ByVal columnAliasToIndex As String, ByVal AliasForNewTable As String, ByVal FilePathToSaveNativeTable As String, ByVal CloseOldTable As Boolean, ByVal KeyColumnToLink As String, ByVal networkArea As String) As MapInfo.Data.Table
        Dim con As New MapInfo.Data.MIConnection
        Dim com As MapInfo.Data.MICommand = Nothing

        Try
            Dim TabFileFullName As String = localTabFilePath + AliasForNewTable + ".tab"
            Dim msh_Network As Mesh
            Dim polyPoints As New TriangleNet.Geometry.Polygon(100)

            Dim p As Integer = 0
            For Each ftr_Source As Feature In SourceTable
                Dim att(0) As Double
                att(0) = ftr_Source(KeyColumnToLink)
                polyPoints.Add(New TriangleNet.Geometry.Vertex(ftr_Source.Geometry.Centroid.x, ftr_Source.Geometry.Centroid.y), att)
            Next

            If Not System.IO.Directory.Exists(localTabFilePath) Then
                System.IO.Directory.CreateDirectory(localTabFilePath)
            End If

            Dim mesher As New TriangleNet.Meshing.GenericMesher()
            msh_Network = mesher.Triangulate(polyPoints)
            Dim msh_Voronoi As TriangleNet.Voronoi.StandardVoronoi = New TriangleNet.Voronoi.StandardVoronoi(msh_Network)

            Dim ti As MapInfo.Data.TableInfoMemTable = New MapInfo.Data.TableInfoMemTable(AliasForNewTable & "_tmp")
            ti.Columns.Add(ColumnFactory.CreateIndexedDoubleColumn(KeyColumnToLink & "_V"))
            ti.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn(csysWGS84))
            ti.Columns.Add(ColumnFactory.CreateStyleColumn())
            ti.Alias = AliasForNewTable & "_tmp"

            Dim nativeTable As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(ti)
            nativeTable.BeginAccess(TableAccessMode.Write)

            Dim j As Integer = 0

            For Each rgn In msh_Voronoi.Faces
                Try
                    Dim ftr As New Feature(nativeTable.TableInfo.Columns)
                    If Not rgn.Edge Is Nothing Then
                        Dim edg As TriangleNet.Topology.DCEL.HalfEdge = rgn.Edge
                        Dim first As Integer = edg.Origin.ID

                        Dim r = 0
                        Dim dps(0) As DPoint

                        Try
                            Do
                                ReDim Preserve dps(r)
                                dps(r) = New MapInfo.Geometry.DPoint(edg.Origin.X, edg.Origin.Y)
                                r = r + 1
                                edg = edg.Next
                            Loop While edg.Origin.ID <> first
                        Catch ex As Exception

                        End Try
                        Dim mpPie As MultiPolygon = New MultiPolygon(csysWGS84, CurveSegmentType.Linear, dps)
                        Dim vx As TriangleNet.Geometry.Vertex = msh_Network.Vertices.ElementAt(j)
                        Dim recordid As Double = (vx.Attributes)(0)
                        ftr(KeyColumnToLink & "_V") = recordid
                        If Not mpPie Is Nothing Then
                            ftr.Geometry = mpPie
                            ftr.Style = New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.AreaStyle(New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(1, MapInfo.Styles.LineWidthUnit.Pixel), 2, Color.FromArgb(128, Color.DarkGray)), New MapInfo.Styles.SimpleInterior(1, Color.FromArgb(255, Color.Black), Color.FromArgb(255, Color.Black), True)))
                            nativeTable.InsertFeature(ftr)
                        End If
                    End If
                    j = j + 1
                Catch ex As Exception

                End Try
            Next

            msh_Network = Nothing
            polyPoints = Nothing
            mesher = Nothing
            msh_Voronoi = Nothing
            nativeTable.EndAccess()
            con.Open()
            com = con.CreateCommand()
            Dim irfc As IResultSetFeatureCollection
            com.CommandText = "select " + ColumnString_MapinfoTable(SourceTable) + ", " & nativeTable.Alias & ".obj," & nativeTable.Alias & ".MI_Style from " + SourceTable.Alias + "," + nativeTable.Alias + " where " + SourceTable.Alias + "." & KeyColumnToLink & "=" + nativeTable.Alias + "." & KeyColumnToLink & "_V"
            irfc = com.ExecuteFeatureCollection()
            con.Catalog.CloseTable(AliasForNewTable)

            'creating convexHull
            con.Catalog.CloseTable("ConvexHull_tbl")
            Dim ti_convex As MapInfo.Data.TableInfoMemTable = New TableInfoMemTable("ConvexHull_tbl")
            ti_convex.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn("obj2", csysWGS84))
            Dim tbl_convexhull As MapInfo.Data.Table = Session.Current.Catalog.CreateTable(ti_convex)
            com = con.CreateCommand()
            com.CommandText = "Insert into " + tbl_convexhull.Alias + " (obj2) Select MI_BUFFER(MI_AggregateConvexHull(obj),10,'km','Spherical',5) from " + AliasForNewTable.Replace("_Voronoi", "")
            com.Prepare()
            Dim totalrecords As Integer = com.ExecuteNonQuery()
            com.Dispose()
            Dim ti_memtbl As MapInfo.Data.TableInfoNative = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(AliasForNewTable, MapInfo.Data.TableType.Native, irfc), MapInfo.Data.TableInfoNative)
            ti_memtbl.TablePath = TabFileFullName
            ti_memtbl.WriteTabFile()
            Dim tbl_map As MapInfo.Data.Table = Session.Current.Catalog.CreateTable(ti_memtbl)
            If irfc.Count <> 0 Then
                'tbl_map.InsertFeatures(irfc)
                com = con.CreateCommand()
                com.CommandText = "Insert into " + tbl_map.Alias + " (" + ColumnString_MapinfoTable(tbl_map) + ", obj, MI_STYLE) Select " & ColumnString_MapinfoTable(tbl_map) + ", MI_INTERSECTION(" + irfc.Alias + ".obj,ConvexHull_tbl.obj2), MI_STYLE from " + irfc.Alias + ", ConvexHull_tbl"
                com.Prepare()
                totalrecords = com.ExecuteNonQuery()
                com.Dispose()
            End If
            con.Catalog.CloseTable(nativeTable.Alias)

            com.Dispose()
            con.Close()
            con.Dispose()
            con = Nothing
            ti.Dispose()
            ti = Nothing
            GC.Collect()

            Return tbl_map
        Catch ex As Exception
            con.Catalog.CloseTable("ConvexHull_tbl")
            con.Catalog.CloseTable(AliasForNewTable & "_tmp")

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

    Private Function CreateNetworkTable_Cells(ByRef tableToIndex As MapInfo.Data.Table, ByVal columnAliasToIndex As String, ByVal AliasForNewTable As String, ByVal FilePathToSaveNativeTable As String,
                                              ByVal CloseOldTable As Boolean, ByVal cStyle As CompositeStyle, ByVal cSize As Integer, ByVal cBeam As Integer, ByVal polygontype As String, ByVal networkArea As String) As MapInfo.Data.Table
        Dim con As New MapInfo.Data.MIConnection
        Dim com As MapInfo.Data.MICommand = Nothing
        Try
            Dim TabFileFullName As String = localTabFilePath & AliasForNewTable & ".tab"
            Dim ti As MapInfo.Data.TableInfoNative = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(AliasForNewTable, MapInfo.Data.TableType.Native, tableToIndex), MapInfo.Data.TableInfoNative)

            For Each dc As MapInfo.Data.Column In ti.Columns
                If dc.Alias = "LAC" Then
                    ti.Columns("LAC").Indexed = True
                ElseIf dc.Alias = "TAC" Then
                    ti.Columns("TAC").Indexed = True
                ElseIf dc.Alias = "CELLID" Then
                    ti.Columns("CELLID").Indexed = True
                ElseIf dc.Alias = "CELLNAME" Then
                    ti.Columns("CELLNAME").Indexed = True
                ElseIf dc.Alias = "SITECODE" Then
                    ti.Columns("SITECODE").Indexed = True
                ElseIf dc.Alias = "RecordID" Then
                    ti.Columns("RecordID").Indexed = True
                End If
            Next

            ti.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn(csysWGS84))
            ti.Columns.Add(ColumnFactory.CreateStyleColumn())
            ti.Alias = AliasForNewTable

            If Not System.IO.Directory.Exists(localTabFilePath) Then
                System.IO.Directory.CreateDirectory(localTabFilePath)
            End If

            ti.TablePath = TabFileFullName
            ti.WriteTabFile()

            Dim nativeTable As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(ti)
            nativeTable.Close()

            nativeTable = MapInfo.Engine.Session.Current.Catalog.OpenTable(TabFileFullName)

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

            'Geo
            Dim gtemp As FeatureGeometry = GetLineFeature(New DPoint(4, 52), New DPoint(4, 53), csysWGS84)
            'Preparing update query
            com = con.CreateCommand()
            com.CommandText = "Update " + nativeTable.Alias + " set obj = @Obj, MI_Style = @style where RecordID = @RecordID"
            com.Parameters.Add("@Obj", gtemp)
            com.Parameters.Add("@style", cStyle)
            com.Parameters.Add("@RecordID", 1)

            Dim recordsupdated As Integer
            com.Prepare()

            Dim HasBeamWidthColumn As Boolean = False
            If ColumnString_MapinfoTable(nativeTable).Contains("BEAMWIDTH") And ColumnString_MapinfoTable(nativeTable).Contains("UseActualBeamWidthOnMap") Then
                HasBeamWidthColumn = True
            End If

            Dim ftr As New Feature(nativeTable.TableInfo.Columns)
            Dim i As Integer = 1

            'exception for nanobts and nano3g... if that is in the tablealias, all items will be circular

            For Each ftr In nativeTable
                If Not ftr.Item("X") Is Nothing Then
                    Dim BeamWidth As Double = 0
                    If HasBeamWidthColumn = True Then
                        If ftr.Item("UseActualBeamWidthOnMap") = 1 Then
                            If Not IsDBNull(ftr.Item("BEAMWIDTH")) Then
                                BeamWidth = ftr.Item("BEAMWIDTH")
                            End If
                        End If
                    End If
                    If BeamWidth = 0 Then
                        BeamWidth = cBeam
                    End If
                    If CInt(ftr.Item("AZIMUTH")) = 360 Then
                        com.Parameters("@Obj").Value = CreateCircleGeometry(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), cSize * Math.Max(CDbl(ftr.Item("MinOfDist")), 0.01))
                    ElseIf polygontype.ToLower = "pie" Then
                        com.Parameters("@Obj").Value = CreatePieGeometry(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), ftr.Item("AZIMUTH"), cSize * Math.Max(CDbl(ftr.Item("MinOfDist")), 0.01), BeamWidth)
                    ElseIf polygontype.ToLower = "lollypop" Then
                        com.Parameters("@Obj").Value = CreateLollyPop(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), ftr.Item("AZIMUTH"), cSize * Math.Max(CDbl(ftr.Item("MinOfDist")), 0.01), BeamWidth)
                    ElseIf polygontype.ToLower = "circle" Then
                        com.Parameters("@Obj").Value = CreateCircleGeometry(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), cSize * Math.Max(CDbl(ftr.Item("MinOfDist")), 0.01))
                    Else
                        com.Parameters("@Obj").Value = CreatePieGeometry(New DPoint(ftr.Item("X").ToString, ftr.Item("Y").ToString), ftr.Item("AZIMUTH"), cSize * Math.Max(CDbl(ftr.Item("MinOfDist")), 0.01), BeamWidth)
                    End If
                    com.Parameters("@style").Value = cStyle
                    com.Parameters("@RecordID").Value = ftr.Item("RecordID")
                    recordsupdated = com.ExecuteNonQuery()
                End If
                i = i + 1
            Next

            nativeTable.EndAccess()
            com.Dispose()

            con.Close()
            con.Dispose()
            con = Nothing
            ti.Dispose()
            ti = Nothing
            GC.Collect()

            Return nativeTable
        Catch ex As Exception
            FailedAndRetry = 1
            Console.WriteLine("Create network >  " & ex.Message)
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

    Private Function CreateNetworkTable_Links(ByRef tableToIndex As MapInfo.Data.Table, ByVal columnAliasToIndex As String, ByVal AliasForNewTable As String, ByVal FilePathToSaveNativeTable As String,
                                              ByVal CloseOldTable As Boolean, ByVal cStyle As CompositeStyle, ByVal cSize As Integer, ByVal cBeam As Integer, ByVal polygontype As String) As MapInfo.Data.Table
        Dim con As New MapInfo.Data.MIConnection
        Dim com As MapInfo.Data.MICommand = Nothing
        Try
            Dim TabFileFullName As String = localTabFilePath & AliasForNewTable & ".tab"
            Dim ti As MapInfo.Data.TableInfoNative = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(AliasForNewTable, MapInfo.Data.TableType.Native, tableToIndex), MapInfo.Data.TableInfoNative)

            For Each dc As MapInfo.Data.Column In ti.Columns
                If dc.Alias = "LINKNAME" Then
                    ti.Columns("LINKNAME").Indexed = True
                ElseIf dc.Alias = "RecordID" Then
                    ti.Columns("RecordID").Indexed = True
                ElseIf dc.Alias = "SiteName_1" Then
                    ti.Columns("SiteName_S").Indexed = True
                ElseIf dc.Alias = "SiteName_2" Then
                    ti.Columns("SiteName_T").Indexed = True
                End If
            Next

            ti.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn(csysWGS84))
            ti.Columns.Add(ColumnFactory.CreateStyleColumn())
            ti.Alias = AliasForNewTable

            If Not System.IO.Directory.Exists(localTabFilePath) Then
                System.IO.Directory.CreateDirectory(localTabFilePath)
            End If

            ti.TablePath = TabFileFullName
            ti.WriteTabFile()

            Dim nativeTable As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(ti)
            nativeTable.Close()

            nativeTable = MapInfo.Engine.Session.Current.Catalog.OpenTable(TabFileFullName)

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

            Dim gtemp As FeatureGeometry = GetLineFeature(New DPoint(4, 52), New DPoint(4, 53), csysWGS84)
            com = con.CreateCommand()
            com.CommandText = "Update " + nativeTable.Alias + " set obj = @Obj, MI_Style = @style where RecordID = @RecordID"
            com.Parameters.Add("@Obj", gtemp)
            com.Parameters.Add("@style", cStyle)
            com.Parameters.Add("@RecordID", 1)

            Dim recordsupdated As Integer
            com.Prepare()

            Dim ftr As New Feature(nativeTable.TableInfo.Columns)
            Dim i As Integer = 1

            For Each ftr In nativeTable
                If Not ftr.Item("X1") Is Nothing Then

                    If polygontype.ToLower = "link" Then
                        com.Parameters("@Obj").Value = CreateLink(New DPoint(ftr.Item("X1").ToString, ftr.Item("Y1").ToString), ftr.Item("AZIMUTH_S"), cSize, 0)
                    End If

                    com.Parameters("@style").Value = cStyle
                    com.Parameters("@RecordID").Value = ftr.Item("RecordID")
                    recordsupdated = com.ExecuteNonQuery()
                End If
                i = i + 1
            Next

            nativeTable.EndAccess()
            com.Dispose()
            con.Close()
            con.Dispose()
            con = Nothing
            GC.Collect()

            Return nativeTable
        Catch ex As Exception
            Console.WriteLine("Create network links >  " & ex.Message)
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

    Private Function CreateNetworkTable_LinksLines(ByRef tableToIndex As MapInfo.Data.Table, ByVal columnAliasToIndex As String, ByVal AliasForNewTable As String, ByVal FilePathToSaveNativeTable As String,
                                              ByVal CloseOldTable As Boolean, ByVal cStyle As CompositeStyle, ByVal cSize As Integer, ByVal cBeam As Integer, ByVal polygontype As String) As MapInfo.Data.Table
        Dim con As New MapInfo.Data.MIConnection
        Dim com As MapInfo.Data.MICommand = Nothing
        Try
            Dim TabFileFullName As String = localTabFilePath & AliasForNewTable & ".tab"
            Dim ti As MapInfo.Data.TableInfoNative = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(AliasForNewTable, MapInfo.Data.TableType.Native, tableToIndex), MapInfo.Data.TableInfoNative)

            For Each dc As MapInfo.Data.Column In ti.Columns
                If dc.Alias = "LINKNAME" Then
                    ti.Columns("LINKNAME").Indexed = True
                ElseIf dc.Alias = "RecordID" Then
                    ti.Columns("RecordID").Indexed = True
                ElseIf dc.Alias = "SiteName_1" Then
                    ti.Columns("SiteName_S").Indexed = True
                ElseIf dc.Alias = "SiteName_2" Then
                    ti.Columns("SiteName_T").Indexed = True
                End If
            Next
            ' Dim csystest As CoordSys = Session.Current.CoordSysFactory.CreateLongLat(DatumID.PopularVisualization)
            ti.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn(csysWGS84))
            'ti.Columns.Add(ColumnFactory.CreateFeatureGeometryColumn(Session.Current.CoordSysFactory.CreateNonEarth(DistanceUnit.Degree, New MapInfo.Geometry.DRect(-60, 20, -50, -20))))
            ti.Columns.Add(ColumnFactory.CreateStyleColumn())
            ti.Alias = AliasForNewTable
            ti.TablePath = TabFileFullName
            ti.WriteTabFile()

            Dim nativeTable As MapInfo.Data.Table = MapInfo.Engine.Session.Current.Catalog.CreateTable(ti)
            nativeTable.Close()

            'TODO Open network tab file from here.
            nativeTable = MapInfo.Engine.Session.Current.Catalog.OpenTable(TabFileFullName)

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

            'Geo
            Dim gtemp As FeatureGeometry = GetLineFeature(New DPoint(4, 52), New DPoint(4, 53), csysWGS84)
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

            'exception for nanobts and nano3g... if that is in the tablealias, all items will be circular

            For Each ftr In nativeTable
                If Not ftr.Item("X1") Is Nothing Then

                    If polygontype.ToLower = "link" Then
                        com.Parameters("@Obj").Value = CreateLinkLine(New DPoint(ftr.Item("X1").ToString, ftr.Item("Y1").ToString), New DPoint(ftr.Item("X2").ToString, ftr.Item("Y2").ToString), cSize * 10)
                    End If
                    com.Parameters("@style").Value = cStyle
                    com.Parameters("@RecordID").Value = ftr.Item("RecordID")
                    recordsupdated = com.ExecuteNonQuery()
                End If
                i = i + 1
            Next

            nativeTable.EndAccess()
            com.Dispose()

            con.Close()
            con.Dispose()
            con = Nothing
            Return nativeTable
        Catch ex As Exception
            Console.WriteLine("Create network links lines >  " & ex.Message)
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

    Private Function CreateLinkLine(ByVal cp1 As DPoint, ByVal cp2 As DPoint, ByVal size As Double) As FeatureGeometry
        Try
            Dim ftrPie As FeatureGeometry
            Dim pnts(1) As DPoint
            pnts(0) = cp1
            pnts(1) = cp2
            Dim mp As MultiPolygon = New MultiPolygon(csysWGS84, CurveSegmentType.Linear, pnts)
            ftrPie = mp.CopyFeatureGeometry
            Return ftrPie
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function

    Private Function ColumnString_MapinfoTable(ByRef dt As MapInfo.Data.Table) As String
        Dim str As String = ""
        For Each col As MapInfo.Data.Column In dt.TableInfo.Columns
            If col.DataType <> 9 And col.DataType <> 13 Then
                str = str & col.Alias & ","
            End If
        Next col
        str = str.TrimEnd(",")
        Return str
    End Function

    Private Function GetLineFeature(ByRef p1 As DPoint, ByRef p2 As DPoint, ByVal csys2 As CoordSys) As Geometry
        Dim g As Geometry
        g = MapInfo.Geometry.MultiCurve.CreateLine(csys2, p1, p2)
        Return g
    End Function

    Private Function CreateCircleGeometry(ByVal cp As DPoint, ByVal size As Double) As FeatureGeometry
        Try
            Dim ftrPie As FeatureGeometry
            Dim pint As MapInfo.Geometry.IPointEdit = New MapInfo.Geometry.Point(csysWGS84, cp).GetPointEditor
            pint.OffsetByXY(-1 * size * 30, -1 * size * 30, DistanceUnit.Meter, DistanceType.Spherical)

            Dim p1 As DPoint = New DPoint(pint.X, pint.Y)
            pint.OffsetByXY(2 * size * 30, 2 * size * 30, DistanceUnit.Meter, DistanceType.Spherical)
            Dim p2 As DPoint = New DPoint(pint.X, pint.Y)

            Dim rect As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(p1, p2)
            Dim crl As MapInfo.Geometry.Ellipse = New MapInfo.Geometry.Ellipse(csysWGS84, rect)
            ftrPie = crl.CopyFeatureGeometry

            Return ftrPie
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing

    End Function

    Private Function CreateLollyPop(ByVal cp As DPoint, ByVal azimuth As Integer, ByVal size As Double, ByVal beamwidth As Integer) As FeatureGeometry
        Try
            Dim ftrPie As FeatureGeometry
            Dim pint As MapInfo.Geometry.IPointEdit = New MapInfo.Geometry.Point(csysWGS84, cp).GetPointEditor
            pint.OffsetByAngle(-1 * (azimuth + 90), -1 * size * 30, DistanceUnit.Meter, DistanceType.Spherical)

            Dim p1 As DPoint = New DPoint(pint.X, pint.Y)
            Dim pnts(1) As DPoint
            pnts(0) = cp
            pnts(1) = p1

            Dim ln As MapInfo.Geometry.LineString = New MapInfo.Geometry.LineString(csysWGS84, pnts)
            Dim pint2 As MapInfo.Geometry.IPointEdit = New MapInfo.Geometry.Point(csysWGS84, p1).GetPointEditor
            pint2.OffsetByXY(-5 * size / 2, -5 * size / 2, DistanceUnit.Meter, DistanceType.Spherical)

            Dim p2 As DPoint = New DPoint(pint2.X, pint2.Y)
            pint2.OffsetByXY(2 * 5 * size / 2, 2 * 5 * size / 2, DistanceUnit.Meter, DistanceType.Spherical)
            Dim p3 As DPoint = New DPoint(pint2.X, pint2.Y)

            Dim rect As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(p2, p3)
            Dim crl As MapInfo.Geometry.Ellipse = New MapInfo.Geometry.Ellipse(csysWGS84, rect)
            Dim mpCircle As MultiPolygon = crl.CreateMultiPolygon(10)

            Dim editMP As IMultiPolygonEdit = mpCircle.GetMultiPolygonEditor
            editMP.AddPolygon(CurveSegmentType.Linear, pnts)
            mpCircle.EditingComplete()

            ftrPie = mpCircle.CopyFeatureGeometry
            Return ftrPie
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function

    Private Function CreatePieGeometry(ByVal cp As DPoint, ByVal azimuth As Integer, ByVal size As Double, ByVal beamwidth As Integer) As FeatureGeometry
        Try
            Dim ftrPie As FeatureGeometry
            Dim pint As MapInfo.Geometry.IPointEdit = New MapInfo.Geometry.Point(csysWGS84, cp).GetPointEditor
            pint.OffsetByXY(-1 * size * 30, -1 * size * 30, DistanceUnit.Meter, DistanceType.Spherical)

            Dim p1 As DPoint = New DPoint(pint.X, pint.Y)
            pint.OffsetByXY(2 * size * 30, 2 * size * 30, DistanceUnit.Meter, DistanceType.Spherical)
            Dim p2 As DPoint = New DPoint(pint.X, pint.Y)

            Dim rect As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(p1, p2)
            Dim larc As MapInfo.Geometry.LegacyArc = New MapInfo.Geometry.LegacyArc(csysWGS84, rect, azimuth * -1 + 90 - beamwidth / 2, azimuth * -1 + 90 + beamwidth / 2)
            Dim mcurve As MultiCurve = larc.CreateMultiCurve(20)
            Dim dps() As DPoint = mcurve.Item(0).SamplePoints

            ReDim Preserve dps(dps.Count)
            dps(dps.Count - 1) = cp

            Dim mpPie As MultiPolygon = New MultiPolygon(csysWGS84, CurveSegmentType.Linear, dps)
            ftrPie = mpPie
            Return ftrPie
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function

    Private Function CreateLink(ByVal cp As DPoint, ByVal azimuth As Integer, ByVal size As Double, ByVal distancekm As Double) As FeatureGeometry
        Try
            Dim ftrPie As FeatureGeometry
            Dim pint As MapInfo.Geometry.IPointEdit = New MapInfo.Geometry.Point(csysWGS84, cp).GetPointEditor
            pint.OffsetByAngle(-1 * (azimuth + 90), -1 * size * 60, DistanceUnit.Meter, DistanceType.Spherical)

            Dim p1 As DPoint = New DPoint(pint.X, pint.Y)
            Dim pnts(1) As DPoint
            pnts(0) = cp
            pnts(1) = p1

            Dim ln As MapInfo.Geometry.LineString = New MapInfo.Geometry.LineString(csysWGS84, pnts)
            Dim pint2 As MapInfo.Geometry.IPointEdit = New MapInfo.Geometry.Point(csysWGS84, p1).GetPointEditor
            pint2.OffsetByXY(-50 * size / 4, -50 * size / 4, DistanceUnit.Meter, DistanceType.Spherical)

            Dim p2 As DPoint = New DPoint(pint2.X, pint2.Y)
            pint2.OffsetByXY(2 * 50 * size / 4, 2 * 50 * size / 4, DistanceUnit.Meter, DistanceType.Spherical)
            Dim p3 As DPoint = New DPoint(pint2.X, pint2.Y)

            Dim rect As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(p2, p3)
            Dim crl As MapInfo.Geometry.Ellipse = New MapInfo.Geometry.Ellipse(csysWGS84, rect)
            Dim mpCircle As MultiPolygon = crl.CreateMultiPolygon(10)

            Dim editMP As IMultiPolygonEdit = mpCircle.GetMultiPolygonEditor
            editMP.AddPolygon(CurveSegmentType.Linear, pnts)

            mpCircle.EditingComplete()
            ftrPie = mpCircle.CopyFeatureGeometry
            Return ftrPie
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function

    Private Function GetFiles(ByVal root As DirectoryInfo, ByVal searchPattern As String) As List(Of FileInfo)
        Dim output As New List(Of FileInfo)
        Dim pending As New Stack(Of DirectoryInfo)
        pending.Push(root)
        While pending.Count <> 0

            Dim di As DirectoryInfo = New DirectoryInfo(pending.Pop.FullName)
            Dim nextpaths() As FileInfo = Nothing
            Try

                output.AddRange(di.GetFiles(searchPattern))

            Catch ex As Exception
                Console.WriteLine(di.FullName & " - " & ex.Message)
            End Try

            Try
                Dim nextdirs() As DirectoryInfo = di.GetDirectories
                For Each subdir As DirectoryInfo In nextdirs
                    pending.Push(subdir)
                Next
            Catch ex As Exception
                Console.WriteLine(di.FullName & " - " & ex.Message)
            End Try
        End While
        Return output

    End Function

End Module
