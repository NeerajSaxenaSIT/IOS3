Imports System.Text

Public Class IOSThematicKPI

    Public Shared sqlQuery As StringBuilder

    Public Shared Function GetThenaticBins(ByVal conStr As String, ByVal kpiType As IOSKPIType) As DataTable
        Dim dtThematic As DataTable = Nothing
        sqlQuery = New StringBuilder
        Try
            If (kpiType = IOSKPIType.ICMKPI) Then
                sqlQuery.AppendLine("SELECT TheKPI.ThematicID,TheKPI.ObjectName,TheKPI.ThematicType,TheKPI.KPI_ID,TheKPI.KPI_Name,TheKPI.KPITypeID,TheKPI.KPIType,TheKPI.RoundBy,")
                sqlQuery.AppendLine("TheKPI.DistributionMethod,TheKPI.TargetObject,TheKPI.IsGraduated,TheKPI.IsHalfPie,TheKPI.PieDiameter,")
                sqlQuery.AppendLine("IOSTB.RegionColor, IOSTB.RegionTransparency, IOSTB.RegionInteriorType, IOSTB.RegionBorderLineWidth, IOSTB.SymbolNumber, IOSTB.SymbolSize,")
                sqlQuery.AppendLine(" IOSTB.RangeMin,IOSTB.RangeMax,IOSTB.RegionBorderLineTransparancy,IOSTB.SymbolInteriorColor,IOSTB.SymbolBorderColor,IOSTB.SymbolInteriorTransparancy,IOSTB.SymbolBorderTransparancy ")
                sqlQuery.AppendLine("FROM (")
                sqlQuery.AppendLine("SELECT The.ThematicID,The.ObjectName,The.ThematicType,The.KPI_ID,The.KPI_Name,The.KPITypeID,KPIT.KPIType,The.RoundBy,The.DistributionMethod,The.TargetObject,The.IsGraduated,The.IsHalfPie,The.PieDiameter FROM (")
                sqlQuery.AppendLine("SELECT IOST.ThematicID,IOST.ObjectName,IOST.ThematicType,IOST.KPI_ID,IOST.KPITypeID,IOST.RoundBy,IOST.DistributionMethod,IOST.TargetObject,IOST.IsGraduated,IOST.IsHalfPie,IOST.PieDiameter,ICMCon.DBColumn AS KPI_Name ")
                sqlQuery.AppendLine("FROM [dbo].[IOS_Thematics] IOST ")
                sqlQuery.AppendLine("INNER JOIN IOS_ICM_Configuration ICMCon ON ICMCon.ID_ICMConfig =IOST.KPI_ID WHERE IOST.KPITypeID=2 ) The ")
                sqlQuery.AppendLine("LEFT JOIN IOS_KPIType KPIT ON KPIT.KPITypeID=The.KPITypeID) TheKPI ")
                sqlQuery.AppendLine("INNER JOIN IOS_Thematic_Bins IOSTB ON IOSTB.ThematicID=TheKPI.ThematicID")
                dtThematic = IOS.DataLibrary.DataAccessorODBC.GetDataTable(conStr, sqlQuery.ToString)
            Else
            End If
        Catch
        End Try
        Return dtThematic
    End Function

    Public Shared Function GetDefaultThenaticBins(ByVal conStr As String, ByVal themeType As IOSKPIThemeType) As DataTable
        Dim dtTheamaticBins As DataTable = Nothing
        Dim defaulTheme As String = IIf(themeType = IOSKPIThemeType.PIE, "DefaultPie", "DefaultThematic")
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine("SELECT TheKPI.ThematicID,TheKPI.ObjectName,TheKPI.ThematicType,TheKPI.KPI_ID,TheKPI.KPITypeID,TheKPI.KPIType,TheKPI.RoundBy,TheKPI.DistributionMethod,TheKPI.TargetObject,")
            sqlQuery.AppendLine("TheKPI.IsGraduated, TheKPI.IsHalfPie, TheKPI.PieDiameter, ")
            sqlQuery.AppendLine("IOSTB.RegionColor, IOSTB.RegionTransparency, IOSTB.RegionInteriorType, IOSTB.RegionBorderLineWidth, IOSTB.SymbolNumber, IOSTB.SymbolSize,")
            sqlQuery.AppendLine(" IOSTB.RangeMin,IOSTB.RangeMax,IOSTB.RegionBorderLineTransparancy,IOSTB.SymbolInteriorColor,IOSTB.SymbolBorderColor,IOSTB.SymbolInteriorTransparancy,IOSTB.SymbolBorderTransparancy ")
            sqlQuery.AppendLine("FROM (")
            sqlQuery.AppendLine("SELECT The.ThematicID,The.ObjectName,The.ThematicType,The.KPI_ID,The.KPITypeID,The.RoundBy,The.DistributionMethod,The.TargetObject,The.IsGraduated,The.IsHalfPie,The.PieDiameter,KPIT.KPIType FROM (")
            sqlQuery.AppendLine("SELECT IOST.ThematicID,IOST.ObjectName,IOST.ThematicType,IOST.KPI_ID,IOST.KPITypeID,IOST.RoundBy,IOST.DistributionMethod,IOST.TargetObject,IOST.IsGraduated,IOST.IsHalfPie,IOST.PieDiameter ")
            sqlQuery.AppendLine("FROM [dbo].[IOS_Thematics] IOST ")
            sqlQuery.AppendLine(" WHERE IOST.ObjectName='" & defaulTheme & "'  AND KPI_ID=0 ) The ")
            sqlQuery.AppendLine("LEFT JOIN IOS_KPIType KPIT ON KPIT.KPITypeID=The.KPITypeID) TheKPI ")
            sqlQuery.AppendLine("INNER JOIN IOS_Thematic_Bins IOSTB ON IOSTB.ThematicID=TheKPI.ThematicID")
            dtTheamaticBins = IOS.DataLibrary.DataAccessorODBC.GetDataTable(conStr, sqlQuery.ToString)
        Catch
        End Try
        Return dtTheamaticBins
    End Function

    Public Shared Sub DeleteThenatic(ByVal conStr As String, ByVal icmKPIName As String)
        sqlQuery = New StringBuilder
        Try
            sqlQuery.AppendLine("DECLARE @KPIID as int;")
            sqlQuery.AppendLine("SELECT @KPIID=ID_ICMConfig FROM IOS_ICM_Configuration WHERE DBColumn='" & icmKPIName & "'")
            sqlQuery.AppendLine("DECLARE @ObjectName as nvarchar(MAX);")
            sqlQuery.AppendLine("Select @ObjectName=ObjectName from IOS_Thematics WHERE KPI_ID=@KPIID AND ThematicType='PieTheme'")
            sqlQuery.AppendLine("DECLARE @ColourList VARCHAR(100)")
            sqlQuery.AppendLine("SELECT @ColourList=''")
            sqlQuery.AppendLine("Select @ColourList=@ColourList +''''+ CONVERT(Varchar(200),ThematicID) + ''',' from IOS_Thematics WHERE (ObjectName=@ObjectName OR KPI_ID=@KPIID) AND ThematicType='PieTheme'")
            sqlQuery.AppendLine("DECLARE @SQLstr Varchar(200)")
            sqlQuery.AppendLine("SET @SQLstr='DELETE IOS_Thematics WHERE ThematicID in('+@ColourList+'''0'');DELETE IOS_Thematic_Bins WHERE ThematicID in('+@ColourList+'''0'');';")
            sqlQuery.AppendLine("EXEC (@SQLstr)")
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(conStr, sqlQuery.ToString)
        Catch
        End Try
    End Sub

    Public Shared Function IsExistThemForICMKPI(ByVal conStr As String, ByVal icmKPIName As String) As Boolean
        Dim dtTheamaticBins As DataTable = Nothing
        Dim isExist As Boolean = False
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine(" SELECT * FROM IOS_Thematics WHERE ObjectName='" & icmKPIName & "'")
            dtTheamaticBins = IOS.DataLibrary.DataAccessorODBC.GetDataTable(conStr, sqlQuery.ToString)
            If (dtTheamaticBins.Rows.Count > 0) Then
                isExist = True
            End If
        Catch
        End Try
        Return isExist
    End Function

    Public Shared Function GetThemticIdByICMKPI(ByVal conStr As String, ByVal icmKPIName As String) As String
        Dim dtTheamaticBins As DataTable = Nothing
        Dim thematicId As String = "0"
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine(" SELECT * FROM IOS_Thematics WHERE ObjectName='" & icmKPIName & "'")
            dtTheamaticBins = IOS.DataLibrary.DataAccessorODBC.GetDataTable(conStr, sqlQuery.ToString)
            If (dtTheamaticBins.Rows.Count > 0) Then
                thematicId = dtTheamaticBins.Rows(0)(IOSThematicKeys.THEMATIC_ID)
            End If
        Catch
        End Try
        Return thematicId
    End Function

    Public Shared Function GetKPIIdByICMKPI(ByVal conStr As String, ByVal icmKPIName As String) As String
        Dim dtTheamaticBins As DataTable = Nothing
        Dim kpiId As String = Nothing
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine("SELECT * FROM IOS_ICM_Configuration WHERE DBColumn='" & icmKPIName & "'")
            dtTheamaticBins = IOS.DataLibrary.DataAccessorODBC.GetDataTable(conStr, sqlQuery.ToString)
            If (dtTheamaticBins.Rows.Count > 0) Then
                kpiId = dtTheamaticBins.Rows(0)("ID_ICMConfig")
            End If
        Catch
        End Try
        Return kpiId
    End Function

    Public Shared Function SetThematicStyle(ByVal conStr As String, ByVal iosThematicStyle As IOSThematic) As String
        Dim thematicId As String = Nothing
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine("INSERT INTO [IOS_Thematics] ([ObjectName],[ThematicType],[KPI_ID],[KPITypeID],[TargetObject],[DistributionMethod],[RoundBy],[IsGraduated],[IsHalfPie],[PieDiameter])")
            sqlQuery.AppendLine("VALUES('" & iosThematicStyle.ObjectName & "','" & iosThematicStyle.ThematicType & "','" & iosThematicStyle.KPIID & "','" & iosThematicStyle.KPITypeID & "','" & iosThematicStyle.TargetObject & "',")
            sqlQuery.AppendLine("'" & iosThematicStyle.DistributionMethod & "','" & Convert.ToDouble(iosThematicStyle.RoundBy) & "','" & iosThematicStyle.IsGraduated & "','" & iosThematicStyle.IsHalfPie & "','" & Convert.ToDouble(iosThematicStyle.PieDiameter) & "');")
            sqlQuery.AppendLine("SELECT SCOPE_IDENTITY();")
            thematicId = IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(conStr, sqlQuery.ToString)
        Catch
        End Try
        Return thematicId
    End Function

    Public Shared Sub UpdateThematicStyle(ByVal conStr As String, ByVal iosThematicStyle As IOSThematic)
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine("UPDATE [IOS_Thematics]  SET [ObjectName]='" & iosThematicStyle.ObjectName & "',[ThematicType]='" & iosThematicStyle.ThematicType & "',[KPI_ID]='" & iosThematicStyle.KPIID & "',")
            sqlQuery.AppendLine("[KPITypeID]='" & iosThematicStyle.KPITypeID & "',[TargetObject]='" & iosThematicStyle.TargetObject & "',[DistributionMethod]='" & iosThematicStyle.DistributionMethod & "',")
            sqlQuery.AppendLine("[RoundBy]='" & Convert.ToDouble(iosThematicStyle.RoundBy) & "',[IsGraduated]='" & iosThematicStyle.IsGraduated & "',[IsHalfPie]='" & iosThematicStyle.IsHalfPie & "',[PieDiameter]='" & Convert.ToDouble(iosThematicStyle.PieDiameter) & "'")
            sqlQuery.AppendLine("WHERE ThematicID ='" & iosThematicStyle.ThematicID & "'")
            IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(conStr, sqlQuery.ToString)
        Catch
        End Try
    End Sub

    Public Shared Sub SetThematicBinsStyle(ByVal conStr As String, ByVal iosThematicBinsStyle As IOSThematicBins)
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_Thematic_Bins]([ThematicID],[RegionColor],[RegionTransparency],[RegionInteriorType],[RegionBorderLineWidth],[SymbolNumber],[SymbolSize],[RangeMin],[RangeMax],")
            sqlQuery.AppendLine("[RegionBorderLineTransparancy],[SymbolInteriorColor],[SymbolBorderColor],[SymbolInteriorTransparancy],[SymbolBorderTransparancy])")
            sqlQuery.AppendLine("VALUES('" & iosThematicBinsStyle.ThematicID & "','" & iosThematicBinsStyle.regionColor & "','" & iosThematicBinsStyle.RegionTransparency & "','" & iosThematicBinsStyle.RegionInteriorType & "',")
            sqlQuery.AppendLine("'" & iosThematicBinsStyle.RegionBorderLineWidth & "','" & iosThematicBinsStyle.SymbolNumber & "','" & iosThematicBinsStyle.SymbolSize & "',")
            sqlQuery.AppendLine("'" & iosThematicBinsStyle.RangeMin & "','" & iosThematicBinsStyle.RangeMax & "','" & iosThematicBinsStyle.RegionBorderLineTransparancy & "','" & iosThematicBinsStyle.SymbolInteriorColor & "',")
            sqlQuery.AppendLine("'" & iosThematicBinsStyle.SymbolBorderColor & "','" & iosThematicBinsStyle.SymbolInteriorTransparancy & "','" & iosThematicBinsStyle.SymbolBorderTransparancy & "')")
            IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(conStr, sqlQuery.ToString)
        Catch
        End Try
    End Sub

    Public Shared Sub DeleteThematicBinsByThematicID(ByVal conStr As String, ByVal thematicID As String)
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine("DELETE [IOS_Thematic_Bins] WHERE [ThematicID]='" & thematicID & "'")
            IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(conStr, sqlQuery.ToString)
        Catch
        End Try
    End Sub

    Public Shared Function GetThematicStyle(ByVal compositStyle As MapInfo.Styles.CompositeStyle) As IOSThematicBins
        Dim iosThematicObj As IOSThematicBins = New IOSThematicBins()
        Dim areaStyle As MapInfo.Styles.AreaStyle = compositStyle.AreaStyle
        Dim areaInteriorStyle As MapInfo.Styles.SimpleInterior = areaStyle.Interior
        Dim lineStyle As MapInfo.Styles.SimpleLineStyle = compositStyle.LineStyle
        Dim symboleStyle As MapInfo.Styles.SimpleVectorPointStyle = compositStyle.SymbolStyle

        iosThematicObj.RangeMax = 0.0
        iosThematicObj.RangeMin = 0.0
        iosThematicObj.RegionBorderLineTransparancy = lineStyle.Color.A
        iosThematicObj.RegionBorderLineWidth = lineStyle.Width.Value
        iosThematicObj.regionColor = Drawing.ColorTranslator.ToWin32(lineStyle.Color)
        iosThematicObj.RegionInteriorType = areaInteriorStyle.Pattern
        iosThematicObj.RegionTransparency = areaInteriorStyle.ForeColor.A
        iosThematicObj.SymbolBorderColor = Drawing.ColorTranslator.ToWin32(symboleStyle.Color)
        iosThematicObj.SymbolBorderTransparancy = symboleStyle.Color.A
        iosThematicObj.SymbolInteriorColor = symboleStyle.Color.ToArgb
        iosThematicObj.SymbolInteriorTransparancy = symboleStyle.Color.A
        iosThematicObj.SymbolNumber = symboleStyle.Code
        iosThematicObj.SymbolSize = symboleStyle.PointSize
        Return iosThematicObj
    End Function

    Public Shared Function GetDistibutionMethodName(ByVal distributionMethod As MapInfo.Mapping.Thematics.DistributionMethod) As String
        If (distributionMethod = MapInfo.Mapping.Thematics.DistributionMethod.BIQuantile) Then
            Return "BIQuantile"
        ElseIf (distributionMethod = MapInfo.Mapping.Thematics.DistributionMethod.CustomRanges) Then
            Return "CustomRanges"
        ElseIf (distributionMethod = MapInfo.Mapping.Thematics.DistributionMethod.EqualCountPerRange) Then
            Return "EqualCountPerRange"
        ElseIf (distributionMethod = MapInfo.Mapping.Thematics.DistributionMethod.EqualRangeSize) Then
            Return "EqualRangeSize"
        ElseIf (distributionMethod = MapInfo.Mapping.Thematics.DistributionMethod.NaturalBreak) Then
            Return "NaturalBreak"
        ElseIf (distributionMethod = MapInfo.Mapping.Thematics.DistributionMethod.StandardDeviation) Then
            Return "StandardDeviation"
        End If
        Return Nothing
    End Function

    'Public Shared Function GetThenaticBins(ByVal conStr As String, ByVal kpiType As IOSKPIType) As DataTable
    '    Dim dtThematic As DataTable = Nothing
    '    Dim sqlCom As String = Nothing
    '    Try
    '        If (kpiType = IOSKPIType.ICMKPI) Then
    '            sqlCom = "SELECT TheKPI.ThematicID,TheKPI.ObjectName,TheKPI.ThematicType,TheKPI.KPI_ID,TheKPI.KPI_Name,TheKPI.KPITypeID,TheKPI.KPIType,TheKPI.RoundBy,TheKPI.DistributionMethod,TheKPI.TargetObject,TheKPI.KPISetID,"
    '            sqlCom = sqlCom & "IOSTB.RegionColor, IOSTB.RegionTransparency, IOSTB.RegionInteriorType, IOSTB.RegionBorderLineWidth, IOSTB.SymbolNumber, IOSTB.SymbolSize, IOSTB.RangeMin,IOSTB.RangeMax,IOSTB.RegionBorderLineTransparancy,IOSTB.SymbolInteriorColor,IOSTB.SymbolBorderColor,IOSTB.SymbolInteriorTransparancy,IOSTB.SymbolBorderTransparancy "
    '            sqlCom = sqlCom & "FROM ("
    '            sqlCom = sqlCom & "SELECT The.ThematicID,The.ObjectName,The.ThematicType,The.KPI_ID,The.KPI_Name,The.KPITypeID,KPIT.KPIType,The.RoundBy,The.DistributionMethod,The.TargetObject,The.KPISetID FROM ("
    '            sqlCom = sqlCom & "SELECT IOST.ThematicID,IOST.ObjectName,IOST.ThematicType,IOST.KPI_ID,IOST.KPITypeID,IOST.RoundBy,IOST.DistributionMethod,IOST.TargetObject,IOST.KPISetID,ICMCon.DBColumn AS KPI_Name "
    '            sqlCom = sqlCom & "FROM [dbo].[IOS_Thematics] IOST "
    '            sqlCom = sqlCom & "INNER JOIN IOS_ICM_Configuration ICMCon ON ICMCon.ID_ICMConfig =IOST.KPI_ID WHERE IOST.KPITypeID=2 ) The "
    '            sqlCom = sqlCom & "LEFT JOIN IOS_KPIType KPIT ON KPIT.KPITypeID=The.KPITypeID) TheKPI "
    '            sqlCom = sqlCom & "LEFT JOIN IOS_Thematic_Bins IOSTB ON IOSTB.ThematicID=TheKPI.ThematicID"
    '            dtThematic = IOS.DataLibrary.DataAccessorODBC.GetDataTable(conStr, sqlCom)
    '        Else
    '        End If
    '    Catch
    '    End Try
    '    Return dtThematic
    'End Function

    Public Shared Sub SetPieTheme(ByVal themPie As MapInfo.Mapping.Thematics.PieTheme, ByVal pieLayerName As String, ByVal conStr As String)
        Dim pieThemKpi() As String = pieLayerName.Substring(pieLayerName.IndexOf("by") + 3, (pieLayerName.Length - 1) - pieLayerName.IndexOf("by") - 2).Split(",")
        If (pieThemKpi.Count <= 0) Then
            Exit Sub
        End If
        Dim kpiName As String = pieThemKpi(0) 'themPie.Expression
        Dim binsCount As Integer = themPie.Categories.Count
        Dim thematicID As String = "0"
        Dim kpiID As String = "0"
        Dim objectName As String = pieThemKpi(0) '
        Dim isNextKPI As Boolean = True
        For Each nextKPI As String In pieThemKpi
            nextKPI = nextKPI.Replace(" ", "")
            kpiID = "0"
            Dim dr() As DataRow = GetThenaticBins(conStr, IOSKPIType.ICMKPI).Select(IOSThematicKeys.OBJECT_NAME & "='" & nextKPI & "'")
            If (dr.Count > 0) Then
                thematicID = dr(0)(IOSThematicKeys.THEMATIC_ID)
                kpiID = dr(0)(IOSThematicKeys.KPI_ID)
            Else
                kpiID = GetKPIIdByICMKPI(conStr, nextKPI)
                If (kpiID = "0") Then
                    Exit Sub
                End If
            End If
            Dim iosThematic As IOSThematic = New IOSThematic()

            If (isNextKPI) Then
                iosThematic.ObjectName = nextKPI
                isNextKPI = False
            Else
                iosThematic.ObjectName = pieThemKpi(0)
                Dim drObj() As DataRow = GetThenaticBins(conStr, IOSKPIType.ICMKPI).Select(IOSThematicKeys.OBJECT_NAME & "='" & pieThemKpi(0) & "' AND " & IOSThematicKeys.KPI_ID & "='" & kpiID & "'")
                If (drObj.Count > 0) Then
                    thematicID = drObj(0)(IOSThematicKeys.THEMATIC_ID)
                    'kpiID = dr(0)(IOSThematicKeys.KPI_ID)
                Else
                    thematicID = "0"
                End If
            End If

            iosThematic.ThematicType = "PieTheme"
            iosThematic.KPIID = kpiID
            iosThematic.KPITypeID = 2
            iosThematic.TargetObject = "Region"
            iosThematic.DistributionMethod = "" 'GetDistibutionMethodName(themPie.Distribution)
            iosThematic.RoundBy = 0.0 'themPie.RoundBy
            iosThematic.IsGraduated = themPie.Graduated
            iosThematic.IsHalfPie = themPie.Half
            iosThematic.PieDiameter = 0
            If (thematicID = "0") Then
                thematicID = SetThematicStyle(conStr, iosThematic)
            Else
                iosThematic.ThematicID = thematicID
                UpdateThematicStyle(conStr, iosThematic)
            End If
            Dim lstIOSThematicBins As List(Of IOSThematicBins) = New List(Of IOSThematicBins)
            If (thematicID IsNot Nothing) Then
                If (binsCount >= 1) Then
                    For binIndex As Integer = 0 To binsCount - 1
                        Dim sInterior As MapInfo.Styles.SimpleInterior = themPie.Categories.Item(binIndex).FillStyle
                        'Dim themBin As MapInfo.Mapping.Thematics.PieTheme = themPie.Categories.Item(binIndex).FillStyle
                        'Dim iosThematicStyle As IOSThematicBins = GetThematicStyle(themBin.Style)

                        Dim iosThematicObj As IOSThematicBins = New IOSThematicBins()
                        'Dim areaStyle As MapInfo.Styles.AreaStyle = compositStyle.AreaStyle
                        'Dim areaInteriorStyle As MapInfo.Styles.SimpleInterior = areaStyle.Interior
                        'Dim lineStyle As MapInfo.Styles.SimpleLineStyle = compositStyle.LineStyle
                        'Dim symboleStyle As MapInfo.Styles.SimpleVectorPointStyle = compositStyle.SymbolStyle

                        iosThematicObj.RangeMax = 0.0
                        iosThematicObj.RangeMin = 0.0
                        iosThematicObj.RegionBorderLineTransparancy = sInterior.ForeColor.A
                        iosThematicObj.RegionBorderLineWidth = 0
                        iosThematicObj.regionColor = Drawing.ColorTranslator.ToWin32(sInterior.ForeColor)
                        iosThematicObj.RegionInteriorType = sInterior.Pattern
                        iosThematicObj.RegionTransparency = sInterior.ForeColor.A
                        iosThematicObj.SymbolBorderColor = Drawing.ColorTranslator.ToWin32(sInterior.ForeColor)
                        iosThematicObj.SymbolBorderTransparancy = sInterior.ForeColor.A
                        iosThematicObj.SymbolInteriorColor = Drawing.ColorTranslator.ToWin32(sInterior.ForeColor)
                        iosThematicObj.SymbolInteriorTransparancy = sInterior.ForeColor.A
                        iosThematicObj.SymbolNumber = 0
                        iosThematicObj.SymbolSize = 0
                        iosThematicObj.ThematicID = thematicID

                        lstIOSThematicBins.Add(iosThematicObj)
                    Next
                Else
                    Dim sInterior As MapInfo.Styles.SimpleInterior = themPie.Categories.Item(0).FillStyle
                    Dim iosThematicObj As IOSThematicBins = New IOSThematicBins()
                    iosThematicObj.RangeMax = 0.0
                    iosThematicObj.RangeMin = 0.0
                    iosThematicObj.RegionBorderLineTransparancy = sInterior.ForeColor.A
                    iosThematicObj.RegionBorderLineWidth = 0
                    iosThematicObj.regionColor = Drawing.ColorTranslator.ToWin32(sInterior.ForeColor)
                    iosThematicObj.RegionInteriorType = sInterior.Pattern
                    iosThematicObj.RegionTransparency = sInterior.ForeColor.A
                    iosThematicObj.SymbolBorderColor = Drawing.ColorTranslator.ToWin32(sInterior.ForeColor)
                    iosThematicObj.SymbolBorderTransparancy = sInterior.ForeColor.A
                    iosThematicObj.SymbolInteriorColor = sInterior.ForeColor.ToArgb
                    iosThematicObj.SymbolInteriorTransparancy = sInterior.ForeColor.A
                    iosThematicObj.SymbolNumber = 0
                    iosThematicObj.SymbolSize = 0
                    iosThematicObj.ThematicID = thematicID
                    lstIOSThematicBins.Add(iosThematicObj)
                End If
            End If
            If (lstIOSThematicBins.Count > 0) Then
                DeleteThematicBinsByThematicID(conStr, thematicID)
                For Each thematicBinsStyle As IOSThematicBins In lstIOSThematicBins
                    SetThematicBinsStyle(conStr, thematicBinsStyle)
                Next
            End If
        Next
    End Sub

    Public Shared Sub SetRangedTheme(ByVal themRange As MapInfo.Mapping.Thematics.RangedTheme, ByVal conStr As String)
        Dim kpiName As String = themRange.Alias '.Expression
        Dim binsCount As Integer = themRange.Bins.Count
        Dim thematicID As String = "0"
        Dim kpiID As String = "0"
        Dim dr() As DataRow = GetThenaticBins(conStr, IOSKPIType.ICMKPI).Select(IOSThematicKeys.OBJECT_NAME & "='" & kpiName & "'")
        If (dr.Count > 0) Then
            thematicID = dr(0)(IOSThematicKeys.THEMATIC_ID)
            kpiID = dr(0)(IOSThematicKeys.KPI_ID)
        Else
            kpiID = GetKPIIdByICMKPI(conStr, kpiName)
            If (kpiID = "0") Then
                Exit Sub
            End If
        End If
        Dim iosThematic As IOSThematic = New IOSThematic()
        iosThematic.ObjectName = kpiName
        iosThematic.ThematicType = "RangedTheme"
        iosThematic.KPIID = kpiID
        iosThematic.KPITypeID = 2
        iosThematic.TargetObject = "Region"
        iosThematic.DistributionMethod = GetDistibutionMethodName(themRange.Distribution)
        iosThematic.RoundBy = themRange.RoundBy
        iosThematic.IsGraduated = True
        iosThematic.IsHalfPie = False
        iosThematic.PieDiameter = 0
        If (thematicID = "0") Then
            thematicID = SetThematicStyle(conStr, iosThematic)
        Else
            UpdateThematicStyle(conStr, iosThematic)
        End If
        Dim lstIOSThematicBins As List(Of IOSThematicBins) = New List(Of IOSThematicBins)
        If (thematicID IsNot Nothing) Then
            If (binsCount >= 1) Then
                For binIndex As Integer = 0 To binsCount - 1
                    Dim themBin As MapInfo.Mapping.Thematics.RangedThemeBin = themRange.Bins.Item(binIndex)
                    Dim iosThematicStyle As IOSThematicBins = GetThematicStyle(themBin.Style)
                    iosThematicStyle.RangeMax = themRange.Bins.Item(binIndex).Max
                    iosThematicStyle.RangeMin = themRange.Bins.Item(binIndex).Min
                    iosThematicStyle.ThematicID = thematicID
                    lstIOSThematicBins.Add(iosThematicStyle)
                Next
            Else
                Dim iosThematicStyle As IOSThematicBins = GetThematicStyle(themRange.ModelStyle)
                iosThematicStyle.RangeMax = 0
                iosThematicStyle.RangeMin = 0
                lstIOSThematicBins.Add(iosThematicStyle)
            End If
        End If
        If (lstIOSThematicBins.Count > 0) Then
            DeleteThematicBinsByThematicID(conStr, thematicID)
            For Each thematicBinsStyle As IOSThematicBins In lstIOSThematicBins
                SetThematicBinsStyle(conStr, thematicBinsStyle)
            Next
        End If
    End Sub

    Public Shared Sub SetIndividualTheme(ByVal themIndividual As MapInfo.Mapping.Thematics.IndividualValueTheme, ByVal conStr As String)
        Dim kpiName As String = themIndividual.Expression
        Dim binsCount As Integer = themIndividual.Bins.Count
        Dim thematicID As String = "0"
        Dim kpiID As String = "0"
        Dim dr() As DataRow = GetThenaticBins(conStr, IOSKPIType.ICMKPI).Select(IOSThematicKeys.OBJECT_NAME & "='" & kpiName & "'")
        If (dr.Count > 0) Then
            thematicID = dr(0)(IOSThematicKeys.THEMATIC_ID)
            kpiID = dr(0)(IOSThematicKeys.KPI_ID)
        Else
            kpiID = IOSThematicKPI.GetKPIIdByICMKPI(conStr, kpiName)
            If (kpiID = "0") Then
                Exit Sub
            End If
        End If

        Dim iosThematic As IOSThematic = New IOSThematic()
        iosThematic.ObjectName = kpiName
        iosThematic.ThematicType = "IndividualTheme"
        iosThematic.KPIID = kpiID
        iosThematic.KPITypeID = 2
        iosThematic.TargetObject = "Region"
        iosThematic.DistributionMethod = "" ' IOSThematicKPI.GetDistibutionMethodName(themIndividual.Distribution)
        iosThematic.RoundBy = 0 'themIndividual.RoundBy
        iosThematic.IsGraduated = True
        iosThematic.IsHalfPie = False
        iosThematic.PieDiameter = 0
        If (thematicID = "0") Then
            thematicID = SetThematicStyle(conStr, iosThematic)
        Else
            UpdateThematicStyle(conStr, iosThematic)
        End If
        Dim lstIOSThematicBins As List(Of IOSThematicBins) = New List(Of IOSThematicBins)
        If (thematicID IsNot Nothing) Then
            If (binsCount >= 1) Then
                For binIndex As Integer = 0 To binsCount - 1
                    Dim themBin As MapInfo.Mapping.Thematics.IndividualValueThemeBin = themIndividual.Bins.Item(binIndex)
                    Dim iosThematicStyle As IOSThematicBins = GetThematicStyle(themBin.Style)
                    iosThematicStyle.RangeMax = themIndividual.Bins.Item(binIndex).Max
                    iosThematicStyle.RangeMin = themIndividual.Bins.Item(binIndex).Min
                    iosThematicStyle.ThematicID = thematicID
                    lstIOSThematicBins.Add(iosThematicStyle)
                Next
            Else
                Dim iosThematicStyle As IOSThematicBins = GetThematicStyle(themIndividual.ModelStyle)
                iosThematicStyle.RangeMax = 0
                iosThematicStyle.RangeMin = 0
                lstIOSThematicBins.Add(iosThematicStyle)
            End If
        End If
        If (lstIOSThematicBins.Count > 0) Then
            DeleteThematicBinsByThematicID(conStr, thematicID)
            For Each thematicBinsStyle As IOSThematicBins In lstIOSThematicBins
                SetThematicBinsStyle(conStr, thematicBinsStyle)
            Next
        End If
    End Sub

End Class