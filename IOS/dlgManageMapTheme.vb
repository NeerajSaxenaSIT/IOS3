Imports IOS.Library
Imports IOS.DataLibrary
Imports System.Text
Imports MapInfo.Mapping.Thematics
Imports DevExpress.XtraEditors

Public Class dlgManageMapTheme

#Region "Variables"

    Private thematicID As Integer = Nothing
    Private dtThemeType As DataTable = Nothing

    'theme
    Public themeType As String = Nothing
    Public themeRoundBy As Double = Nothing

    'theme bins data
    'Public dtThemeBins As DataTable = Nothing

    Public _RangedTheme As RangedTheme = Nothing
    Public _IndividualValueTheme As IndividualValueTheme = Nothing

    'theme bins variables
    'Public themeBinsCount As Integer = Nothing
    'Public regionColor As Integer = Nothing
    'Public regionTransparency As String = Nothing
    'Public regionInteriorType As String = Nothing
    'Public regionBorderLineWidth As Integer = Nothing
    'Public regionBorderLineTransparency As Integer = Nothing
    'Public symbolNumber As Integer = Nothing
    'Public symbolSize As Integer = Nothing
    'Public symbolInteriorColor As Integer = Nothing
    'Public symbolBorderColor As Integer = Nothing
    'Public symbolInteriorTransparency As Integer = Nothing
    'Public symbolBorderTransparency As Integer = Nothing
    'Public rangeMin As Double = Nothing
    'Public rangeMax As Double = Nothing
    'Public individualValue As String = Nothing

#End Region

#Region "Events"

    Private Sub dlgManageMapTheme_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetThemeType()
            LoadSavedThemes()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnCreateTheme_Click(sender As Object, e As EventArgs) Handles btnCreateTheme.Click
        Try
            If txtThemeName.Text.Trim = String.Empty Then
                SetMessage("Please enter a theme name")
                Exit Sub
            End If

            frmMapWindow.newKpiInfoThemeName = txtThemeName.Text.Trim
            SaveRangedTheme()
            SaveRangedThemeBins(thematicID)
            SetMessage("Theme saved successfully")
            LoadSavedThemes()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnUpdateTheme_Click(sender As Object, e As EventArgs) Handles btnUpdateTheme.Click
        Try
            If cmbThemeName.SelectedIndex = 0 Then
                SetMessage("Please select a theme to update")
                Exit Sub
            End If

            thematicID = CType(cmbThemeName.SelectedItem, clsComboBoxItem).Value
            Dim result = XtraMessageBox.Show("Overwrite Selected Theme?", "Update Theme", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

            If result = DialogResult.OK Then
                SaveRangedThemeBins(thematicID)
            ElseIf result = DialogResult.Cancel Then
                Exit Sub
            End If

            SetMessage("Theme updated successfully")
            frmMapWindow.ThematicID = Me.thematicID
            LoadSavedThemes()
            SetComboBox(cmbThemeName, ComboSelectBased.ValueBased, thematicID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteTheme_Click(sender As Object, e As EventArgs) Handles btnDeleteTheme.Click
        Try
            If cmbThemeName.SelectedIndex = 0 Then
                SetMessage("Please select a theme to delete")
                Exit Sub
            End If

            'delete the selected theme
            If XtraMessageBox.Show("Are you sure to delete theme: " & cmbThemeName.SelectedItem.ToString & "?", "Delete Theme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                thematicID = CType(cmbThemeName.SelectedItem, clsComboBoxItem).Value
                clsSQLCommands.DeleteTheme(connStrIOSServer, thematicID)
                LoadSavedThemes()
                SetMessage("Theme deleted successfully")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

#End Region

#Region "Methods"

    Private Sub GetThemeType()
        dtThemeType = clsSQLCommands.GetThemeType(connStrIOSServer)
    End Sub

    Private Sub SaveRangedTheme()
        Dim themeTypeID As Integer = dtThemeType.AsEnumerable().Where(Function(x) x.Field(Of String)("ThematicTypeName") = Me.themeType)(0)("ThematicTypeID")
        Dim distMethod As String = GetDistributionMethodName(_RangedTheme.Distribution)
        Dim dt As DataTable = clsSQLCommands.SaveTheme(connStrIOSServer, txtThemeName.Text.Trim, themeTypeID, _RangedTheme.RoundBy, distMethod)
        If dt IsNot Nothing Then
            thematicID = CInt(dt.Rows(0)(0))
        End If
    End Sub

    Public Sub SaveRangedThemeBins(thematicID As Integer)
        Dim kpiName As String = _RangedTheme.Alias '.Expression
        Dim binsCount As Integer = _RangedTheme.Bins.Count
        'Dim thematicID As Integer = 0
        Dim kpiID As String = "0"

        Dim iosThematic As IOSThematic = New IOSThematic()
        iosThematic.ObjectName = kpiName
        iosThematic.ThematicType = "RangedTheme"
        'iosThematic.KPIID = kpiID
        iosThematic.KPITypeID = 2
        iosThematic.TargetObject = "Region"
        iosThematic.DistributionMethod = GetDistributionMethodName(_RangedTheme.Distribution)
        iosThematic.RoundBy = _RangedTheme.RoundBy
        iosThematic.IsGraduated = True
        iosThematic.IsHalfPie = False
        iosThematic.PieDiameter = 0

        Dim lstIOSThematicBins As New List(Of IOSThematicBins)
        If (thematicID <> Nothing) Then
            If (binsCount >= 1) Then
                For binIndex As Integer = 0 To binsCount - 1
                    Dim themBin As MapInfo.Mapping.Thematics.RangedThemeBin = _RangedTheme.Bins.Item(binIndex)
                    Dim iosThematicStyle As IOSThematicBins = GetThematicStyle(themBin.Style)
                    iosThematicStyle.RangeMax = _RangedTheme.Bins.Item(binIndex).Max
                    iosThematicStyle.RangeMin = _RangedTheme.Bins.Item(binIndex).Min
                    iosThematicStyle.ThematicID = thematicID
                    lstIOSThematicBins.Add(iosThematicStyle)
                Next
            Else
                Dim iosThematicStyle As IOSThematicBins = GetThematicStyle(_RangedTheme.ModelStyle)
                iosThematicStyle.RangeMax = 0
                iosThematicStyle.RangeMin = 0
                lstIOSThematicBins.Add(iosThematicStyle)
            End If
        End If
        If (lstIOSThematicBins.Count > 0) Then
            clsSQLCommands.DeleteKPIMappingThematicBinsForThematicID(connStrIOSServer, thematicID)
            For Each thematicBinsStyle As IOSThematicBins In lstIOSThematicBins
                SaveKPiMappingThemeBins(connStrIOSServer, thematicBinsStyle, iosThematic.ThematicType)
            Next
        End If
    End Sub

    Public Function GetThematicStyle(ByVal compositStyle As MapInfo.Styles.CompositeStyle) As IOSThematicBins
        Dim iosThematicObj As IOSThematicBins = New IOSThematicBins()
        Dim areaStyle As MapInfo.Styles.AreaStyle = compositStyle.AreaStyle
        Dim areaInteriorStyle As MapInfo.Styles.SimpleInterior = areaStyle.Interior
        Dim lineStyle As MapInfo.Styles.SimpleLineStyle = compositStyle.LineStyle
        Dim symboleStyle As MapInfo.Styles.SimpleVectorPointStyle = compositStyle.SymbolStyle

        iosThematicObj.RangeMax = 0.0
        iosThematicObj.RangeMin = 0.0
        iosThematicObj.RegionBorderLineTransparancy = DirectCast(areaStyle.Border, MapInfo.Styles.SimpleLineStyle).Color.A
        iosThematicObj.RegionBorderLineWidth = DirectCast(areaStyle.Border, MapInfo.Styles.SimpleLineStyle).Width.Value
        iosThematicObj.regionColor = ColorTranslator.ToOle(DirectCast(areaStyle.Interior, MapInfo.Styles.SimpleInterior).ForeColor) 'System.Drawing.ColorTranslator.ToWin32(lineStyle.Color)
        iosThematicObj.RegionInteriorType = DirectCast(areaStyle.Interior, MapInfo.Styles.SimpleInterior).Pattern
        iosThematicObj.RegionTransparency = DirectCast(areaStyle.Interior, MapInfo.Styles.SimpleInterior).ForeColor.A 'areaInteriorStyle.ForeColor.A
        iosThematicObj.SymbolBorderColor = 0 'System.Drawing.ColorTranslator.ToWin32(symboleStyle.Color)
        iosThematicObj.SymbolBorderTransparancy = 0 'symboleStyle.Color.A
        iosThematicObj.SymbolInteriorColor = 0 'symboleStyle.Color.ToArgb
        iosThematicObj.SymbolInteriorTransparancy = 0 'symboleStyle.Color.A
        iosThematicObj.SymbolNumber = 0 'symboleStyle.Code
        iosThematicObj.SymbolSize = 0 'symboleStyle.PointSize
        Return iosThematicObj
    End Function

    Public Function GetDistributionMethodName(ByVal distributionMethod As DistributionMethod) As String
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

    Public Function SaveKPiMappingThemeBins(connStr As String, _IOSThematicBins As IOSThematicBins, _thematicType As String)
        Dim sqlQuery = New StringBuilder()
        If _thematicType = "RangedTheme" Then

            sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_KPIMapping_Theme_Bins] ([ThematicID],[RegionColor],[RegionTransparency],[RegionInteriorType],[RegionBorderLineWidth],[RegionBorderLineTransparancy],")
            sqlQuery.AppendLine("[SymbolNumber],[SymbolSize],[SymbolInteriorColor],[SymbolBorderColor],[SymbolInteriorTransparancy],[SymbolBorderTransparancy],[RangeMin],[RangeMax],[IndividualValue])")
            sqlQuery.AppendLine("VALUES (" & _IOSThematicBins.ThematicID & "," & _IOSThematicBins.regionColor & "," & Chr(39) & _IOSThematicBins.RegionTransparency & Chr(39) & "," & Chr(39) & _IOSThematicBins.RegionInteriorType & Chr(39) & "," & _IOSThematicBins.RegionBorderLineWidth & "," & _IOSThematicBins.RegionBorderLineTransparancy & ",")
            sqlQuery.AppendLine("NULL,NULL,NULL,NULL,NULL,NULL," & _IOSThematicBins.RangeMin & "," & _IOSThematicBins.RangeMax & ",NULL)")

        ElseIf _thematicType = "IndividualValueTheme" Then

            sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_KPIMapping_Theme_Bins] ([ThematicID],[RegionColor],[RegionTransparency],[RegionInteriorType],[RegionBorderLineWidth],[RegionBorderLineTransparancy],")
            sqlQuery.AppendLine("[SymbolNumber],[SymbolSize],[SymbolInteriorColor],[SymbolBorderColor],[SymbolInteriorTransparancy],[SymbolBorderTransparancy],[RangeMin],[RangeMax],[IndividualValue])")
            sqlQuery.AppendLine("VALUES (" & _IOSThematicBins.ThematicID & ",NULL,NULL,NULL,NULL,NULL,")
            sqlQuery.AppendLine(_IOSThematicBins.SymbolNumber & "," & _IOSThematicBins.SymbolSize & "," & _IOSThematicBins.SymbolInteriorColor & "," & _IOSThematicBins.SymbolBorderColor & "," & _IOSThematicBins.SymbolInteriorTransparancy & "," & _IOSThematicBins.SymbolBorderTransparancy & "," & _IOSThematicBins.RangeMin & "," & _IOSThematicBins.RangeMax & "," & Chr(39) & _IOSThematicBins.IndividualValue & Chr(39) & ")")

        End If
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    'Public Function UpdateKPiMappingThemeBins(connStr As String, thematicBinsID As Integer, _IOSThematicBins As IOSThematicBins)
    '    Dim sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("UPDATE [dbo].[IOS_KPIMapping_Theme_Bins] SET [ThematicID] = " & thematicID & ",[RegionTransparency] = " & _IOSThematicBins.RegionTransparency & ",[RegionInteriorType] = " & Chr(39) & _IOSThematicBins.RegionInteriorType & Chr(39) & ",[RegionBorderLineWidth] = " & _IOSThematicBins.RegionBorderLineWidth & ",")
    '    sqlQuery.AppendLine("[RegionBorderLineTransparancy] = " & _IOSThematicBins.RegionBorderLineTransparancy & ",[SymbolNumber] = " & _IOSThematicBins.SymbolNumber & ",[SymbolSize] = " & _IOSThematicBins.SymbolSize & ",[SymbolInteriorColor] = " & _IOSThematicBins.SymbolInteriorColor & ",[SymbolBorderColor] = " & _IOSThematicBins.SymbolBorderColor & ",")
    '    sqlQuery.AppendLine("[SymbolInteriorTransparancy] = " & _IOSThematicBins.SymbolInteriorTransparancy & ",[SymbolBorderTransparancy] = " & _IOSThematicBins.SymbolBorderTransparancy & ",[RangeMin] = " & _IOSThematicBins.RangeMin & ",[RangeMax] = " & _IOSThematicBins.RangeMax & ",[IndividualValue] = " & Chr(39) & _IOSThematicBins.IndividualValue & Chr(39) & "")
    '    sqlQuery.AppendLine("WHERE [ThematicBinsID] = " & thematicBinsID & ";")
    '    Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    'End Function

    Private Sub LoadSavedThemes()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {}
        strConnection = GetSQL(7100, parray)(0)
        sqlParam = GetSQL(7100, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt IsNot Nothing Then
            BindDevExComboBoxWithTagMember(cmbThemeName, dt, "ThematicID", "ThematicName", "Select Theme", "ThematicTypeName", False)
        End If
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

#End Region

End Class