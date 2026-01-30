Imports LidorSystems.IntegralUI.Lists
Imports MapInfo.Mapping
Imports MapInfo.Mapping.Thematics
Imports MapInfo.Styles
Imports DevExpress.XtraEditors

Public Class frmQuickThemeControl

#Region "Variables"

    Dim tltpScrollBar As New ToolTip()
    Dim btnRecalculateText = "Calculate Bins({0})"
    Dim customRangeText = "Custom"
    Dim trackbarMouseDown As Boolean = False
    Dim trackbarScrolling As Boolean = False
    Dim currentSelectedFeatureLayerAlias As String = String.Empty
    Dim currentSelectedModifierExpression As String = String.Empty
    Dim currentSelectedModifierTag As String = String.Empty
    Dim selectedModifiersOriginalDataList As New Dictionary(Of String, RangedThemeBackup)
    Dim selectedFeatureStypeModifier As FeatureStyleModifier = Nothing
    ReadOnly rangedTheme As String = "RangedTheme"
    ReadOnly individualValueTheme As String = "IndividualValueTheme"
    Dim IsSliderSetRequired As Boolean = False
    Dim validThemes As List(Of String) = New List(Of String)() From {rangedTheme, individualValueTheme}
    Dim kpiPrefix As String = "X"
    Delegate Sub ModifierWorkEventHandlerr(ByRef modifier As MapInfo.Mapping.FeatureStyleModifier)
    Private enableFormLevelDoubleBuffering As Boolean = True
    Private defaultEX As Integer = -1
    Dim IsAnyThemeticModifiedFoundInReset As Boolean = False
    Private removeDraggedKPIList As New List(Of String)
    Private KpiColorModifyList As New Dictionary(Of String, Color)
    Dim BinsCountOfKPI As New Dictionary(Of String, Integer)

#End Region

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams

            If defaultEX = -1 Then
                defaultEX = cp.ExStyle
            End If
            If enableFormLevelDoubleBuffering = True Then
                cp.ExStyle = cp.ExStyle Or &H2000000
            Else
                cp.ExStyle = defaultEX
            End If
            Return cp
        End Get
    End Property

    Sub ManagevgbThematic(ByVal isVisible As Boolean)
        vgbThematic.Visible = isVisible
    End Sub

    Sub SetRecalculateButtonText(ByVal bins As Integer)
        btnRecalculateBins.Text = String.Format(btnRecalculateText, bins)
    End Sub

    Private Sub UpdateModifierStyleColor(ByRef compositeStyle As MapInfo.Styles.CompositeStyle, ByVal color As System.Drawing.Color)
        compositeStyle.SymbolStyle.Color = color
        UpdateInteriorColor(compositeStyle.AreaStyle.Interior, color)
    End Sub

    Private Sub UpdateInteriorColor(ByRef binInterior As MapInfo.Styles.BaseInterior, ByVal color As System.Drawing.Color)
        Dim interior As MapInfo.Styles.SimpleInterior = TryCast(binInterior, MapInfo.Styles.SimpleInterior)
        If (interior IsNot Nothing) Then
            interior.ForeColor = color
            interior.BackColor = color
        End If
    End Sub

    Private Sub ClearSelectedModifier()
        selectedModifiersOriginalDataList.Clear()
    End Sub

    Private Sub WorkOnModifier(ByVal selectedNode As TreeListViewNode, ByVal callBack As ModifierWorkEventHandlerr)
        For Each layer As MapLayer In frmMapWindow.MapControl1.Map.Layers
            Dim featureLayer As FeatureLayer = Me.IsValidLayer(layer)
            If Not featureLayer Is Nothing Then
                If (layer.Alias = selectedNode.Text) Then
                    For Each modifier As MapInfo.Mapping.FeatureStyleModifier In featureLayer.Modifiers
                        If (modifier.Alias = selectedNode.Tag.ToString().Split("#")(1)) Then
                            callBack(modifier)
                            Exit Sub
                        End If
                    Next
                End If
            End If
        Next
    End Sub

    Private Sub UpdateAndRecomputeBins(ByRef modifier As MapInfo.Mapping.FeatureStyleModifier)
        If (modifier.IsRangesTheme()) Then
            Dim rangedThemeModifier = modifier.ToRangesTheme()
            rangedThemeModifier.Bins.Count = vtbBins.Value
            rangedThemeModifier.RecomputeBins()
            ManageThematicOnItemSelection(rangedThemeModifier)
        End If
        If (frmMapWindow.legend_ac.Visible) Then
            frmMapWindow.legend_ac.Refresh()
        End If
    End Sub

    Private Sub ManageThematicOnItemSelection(ByRef modifier As MapInfo.Mapping.FeatureStyleModifier)
        Dim isRangedThemeSelected = modifier.IsRangesTheme()
        vcmbThemticBinsDistribution.Enabled = isRangedThemeSelected
        vtbBins.Enabled = isRangedThemeSelected
        If (isRangedThemeSelected) Then
            Dim rangedThemeModifier = modifier.ToRangesTheme()
            'Skip this step if call is being made from reset button
            If (lvLayers.SelectedNode IsNot Nothing AndAlso Not IsAnyThemeticModifiedFoundInReset) Then
                AddToModifierList(lvLayers.SelectedNode.Tag.ToString(), rangedThemeModifier.Clone())
            End If
            selectedFeatureStypeModifier = rangedThemeModifier
            currentSelectedModifierExpression = rangedThemeModifier.Expression
            '' If condition is required to avoid infinite loop as method is being call on vtbBins.Value changed event
            If BinsCountOfKPI.Keys.Contains(lvLayers.SelectedNode.Tag.ToString()) Then
                vtbBins.Value = BinsCountOfKPI.Item(lvLayers.SelectedNode.Tag.ToString())
                SetRecalculateButtonText(vtbBins.Value)
                BinsCountOfKPI.Remove(lvLayers.SelectedNode.Tag.ToString())
                btnRecalculateBins_Click(Nothing, EventArgs.Empty)
                BinsCountOfKPI.Add(lvLayers.SelectedNode.Tag.ToString(), vtbBins.Value)
            Else
                If IsSliderSetRequired Then
                    If Not vtbBins.Value = rangedThemeModifier.Bins.Count Then
                        vtbBins.Value = rangedThemeModifier.Bins.Count
                        SetRecalculateButtonText(vtbBins.Value)
                    End If
                End If
            End If

            Dim rangedBins As MapInfo.Mapping.Thematics.RangedThemeBins = rangedThemeModifier.Bins
            vcmbThematicType.Text = rangedTheme
            If rangedThemeModifier.Distribution = Thematics.DistributionMethod.BIQuantile Then
                vcmbThemticBinsDistribution.Text = "Quantile"
            ElseIf rangedThemeModifier.Distribution = Thematics.DistributionMethod.CustomRanges Then
                vcmbThemticBinsDistribution.Text = customRangeText
            ElseIf rangedThemeModifier.Distribution = Thematics.DistributionMethod.EqualCountPerRange Then
                vcmbThemticBinsDistribution.Text = "Equal Count"
            ElseIf rangedThemeModifier.Distribution = Thematics.DistributionMethod.EqualRangeSize Then
                vcmbThemticBinsDistribution.Text = "Equal Ranges"
            ElseIf rangedThemeModifier.Distribution = Thematics.DistributionMethod.NaturalBreak Then
                vcmbThemticBinsDistribution.Text = "Natural Break"
            ElseIf rangedThemeModifier.Distribution = Thematics.DistributionMethod.StandardDeviation Then
                vcmbThemticBinsDistribution.Text = "Standard Deviation"
            End If
            tlpThemeBins.SuspendLayout()
            BindRangesBinsTable(rangedBins, rangedThemeModifier.Expression)
            tlpThemeBins.ResumeLayout()
            rangedThemeModifier.RecomputeBins()
            vgbThematic.Visible = True
        ElseIf (modifier.IsIndividualValueTheme()) Then
            Dim individualThemeModifier = modifier.ToIndividualValueTheme()
            If (lvLayers.SelectedNode IsNot Nothing AndAlso Not IsAnyThemeticModifiedFoundInReset) Then
                AddToModifierList(lvLayers.SelectedNode.Tag.ToString(), individualThemeModifier.Clone())
            End If
            selectedFeatureStypeModifier = individualThemeModifier
            currentSelectedModifierExpression = individualThemeModifier.Expression
            '' If condition is required to avoid infinite loop as method is being call on vtbBins.Value changed event
            If Not vtbBins.Value = individualThemeModifier.Bins.Count Then
                If (individualThemeModifier.Bins.Count > vtbBins.Properties.Maximum) Then
                    vtbBins.Value = vtbBins.Properties.Maximum
                Else
                    vtbBins.Value = individualThemeModifier.Bins.Count
                End If
                SetRecalculateButtonText(vtbBins.Value)
            End If
            Dim individualBins As MapInfo.Mapping.Thematics.IndividualValueThemeBins = individualThemeModifier.Bins
            vcmbThematicType.Text = individualValueTheme
            tlpThemeBins.SuspendLayout()
            BindIndivisualBinsTable(individualBins, individualThemeModifier.Expression)
            tlpThemeBins.ResumeLayout()
            ManagevgbThematic(True)
        Else
            currentSelectedModifierExpression = String.Empty
            selectedFeatureStypeModifier = Nothing
            ManagevgbThematic(False)
        End If
        If (frmMapWindow.legend_ac IsNot Nothing) Then
            If (frmMapWindow.legend_ac.Visible) Then
                frmMapWindow.legend_ac.Refresh()
            End If
        End If
    End Sub

    Private Sub txtCustomBins_TextChanges(sender As Object, e As EventArgs)
        Try
            Dim txt As DevExpress.XtraEditors.TextEdit = TryCast(sender, DevExpress.XtraEditors.TextEdit)
            If (txt IsNot Nothing) Then
                Dim result As Double
                If (Double.TryParse(txt.Text, result)) Then
                    Dim tagData As TextBoxTagData = TryCast(txt.Tag, TextBoxTagData)
                    If (tagData IsNot Nothing) Then 'And e.KeyCode = Keys.Enter Then
                        Dim IsMinMaxValidationFail = False
                        If (tagData.ValueType = BinValueType.Max) Then
                            IsMinMaxValidationFail = Convert.ToDouble(tagData.Bin.Min) > result
                            If (Not IsMinMaxValidationFail) Then
                                tagData.Bin.Max = result
                            End If
                        End If
                        If (tagData.ValueType = BinValueType.Min) Then
                            IsMinMaxValidationFail = Convert.ToDouble(tagData.Bin.Max) < result
                            If (Not IsMinMaxValidationFail) Then
                                tagData.Bin.Min = result
                            End If
                        End If

                        If (IsMinMaxValidationFail) Then
                            'XtraMessageBox.Show("Bin minimum value can not be more than maximum value. Please change the value")
                        End If
                    End If
                Else
                    If (Not String.IsNullOrEmpty(txt.Text)) AndAlso Not txt.Text = "-" AndAlso Not txt.Text = "+" Then
                        XtraMessageBox.Show("Please enter valid bin value")
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub vtbBins_MouseDown(sender As Object, e As MouseEventArgs) Handles vtbBins.MouseDown
        trackbarMouseDown = True
        Application.DoEvents()
    End Sub

    Private Sub vtbBins_MouseHover(sender As Object, e As EventArgs) Handles vtbBins.MouseHover
        tltpScrollBar.InitialDelay = 500
        tltpScrollBar.ShowAlways = True
        tltpScrollBar.SetToolTip(vtbBins, vtbBins.Value.ToString())
    End Sub

    Private Sub vtbBins_MouseLeave(sender As Object, e As EventArgs) Handles vtbBins.MouseLeave
        tltpScrollBar.RemoveAll()
    End Sub

    Private Sub vtbBins_MouseUp(sender As Object, e As EventArgs) Handles vtbBins.MouseUp
        If trackbarMouseDown AndAlso trackbarScrolling Then
            'vbtnRecalculateBins.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.ORANGEFRESH
            SetRecalculateButtonText(vtbBins.Value)
            If BinsCountOfKPI.Keys.Contains(lvLayers.SelectedNode.Tag.ToString()) Then
                BinsCountOfKPI.Remove(lvLayers.SelectedNode.Tag.ToString())
            End If
            btnRecalculateBins_Click(btnRecalculateBins, EventArgs.Empty)
            BinsCountOfKPI.Add(lvLayers.SelectedNode.Tag.ToString(), vtbBins.Value)
        End If
        trackbarMouseDown = False
        trackbarScrolling = False
    End Sub

    Private Sub vtbBins_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles vtbBins.EditValueChanging
        trackbarScrolling = True
    End Sub

    Private Sub vtbBins_ParseEditValue(sender As Object, e As DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs) Handles vtbBins.ParseEditValue
        trackbarScrolling = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            IsAnyThemeticModifiedFoundInReset = False
            For Each layer As MapLayer In frmMapWindow.MapControl1.Map.Layers
                Dim featureLayer As FeatureLayer = Me.IsValidLayer(layer)
                If Not featureLayer Is Nothing Then
                    For Each modifier As MapInfo.Mapping.FeatureStyleModifier In featureLayer.Modifiers
                        If (modifier.IsRangesTheme()) Then
                            Dim modifierTagKey As String = featureLayer.Alias & "#" & modifier.Alias
                            If (selectedModifiersOriginalDataList.Keys.Contains(modifierTagKey)) Then
                                Dim rangedThemeModifier = modifier.ToRangesTheme()
                                If (rangedThemeModifier IsNot Nothing) Then
                                    IsAnyThemeticModifiedFoundInReset = True
                                    Dim rangedThemeBackup As RangedThemeBackup = selectedModifiersOriginalDataList(modifierTagKey)
                                    rangedThemeModifier.Distribution = rangedThemeBackup.Distribution
                                    rangedThemeModifier.Bins.Count = rangedThemeBackup.ModifierThemeBins.Count
                                    rangedThemeModifier.Recompute()
                                    For index = 1 To rangedThemeBackup.ModifierThemeBins.Count
                                        rangedThemeModifier.Bins(index - 1).Min = rangedThemeBackup.ModifierThemeBins(index - 1).Min
                                        rangedThemeModifier.Bins(index - 1).Max = rangedThemeBackup.ModifierThemeBins(index - 1).Max
                                        UpdateModifierStyleColor(rangedThemeModifier.Bins(index - 1).Style, rangedThemeBackup.ModifierThemeBins(index - 1).Color)
                                    Next
                                    rangedThemeModifier.Recompute()
                                End If
                            End If
                        End If
                    Next
                End If
            Next
            If (IsAnyThemeticModifiedFoundInReset) Then
                BindNodeThematic(lvLayers.SelectedNode)
                IsAnyThemeticModifiedFoundInReset = False
            End If
        Catch ex As Exception
            IsAnyThemeticModifiedFoundInReset = False
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        BinsCountOfKPI.Clear()
    End Sub

    Private Sub vbtnApply_Click(sender As Object, e As EventArgs) Handles vbtnApply.Click
        Try
            For Each layer As MapLayer In frmMapWindow.MapControl1.Map.Layers
                Dim featureLayer As FeatureLayer = Me.IsValidLayer(layer)
                If Not featureLayer Is Nothing Then
                    If Not (featureLayer.Alias = currentSelectedFeatureLayerAlias) Then
                        For Each modifier As MapInfo.Mapping.FeatureStyleModifier In featureLayer.Modifiers
                            If (modifier.IsRangesTheme()) Then
                                Dim rangedThemeModifier = modifier.ToRangesTheme()
                                If (rangedThemeModifier IsNot Nothing AndAlso rangedThemeModifier.Expression = currentSelectedModifierExpression) Then
                                    Dim selectedThemeModifier = selectedFeatureStypeModifier.ToRangesTheme()
                                    rangedThemeModifier.Distribution = selectedThemeModifier.Distribution
                                    rangedThemeModifier.Bins.Count = selectedThemeModifier.Bins.Count
                                    rangedThemeModifier.Recompute()
                                    For index = 1 To selectedThemeModifier.Bins.Count
                                        rangedThemeModifier.Bins(index - 1).Min = selectedThemeModifier.Bins(index - 1).Min
                                        rangedThemeModifier.Bins(index - 1).Max = selectedThemeModifier.Bins(index - 1).Max
                                        UpdateModifierStyleColor(rangedThemeModifier.Bins(index - 1).Style, selectedThemeModifier.Bins(index - 1).Style.SymbolStyle.Color)
                                    Next
                                End If
                            End If
                        Next
                    End If
                End If
            Next
            ClearSelectedModifier()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnRecalculateBins_Click(sender As Object, e As EventArgs) Handles btnRecalculateBins.Click
        Try
            Dim selectedNode = lvLayers.SelectedNode
            If Me.IsValidNodeSelected(selectedNode) AndAlso (currentSelectedModifierTag = selectedNode.Tag) Then
                WorkOnModifier(selectedNode, AddressOf UpdateAndRecomputeBins)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lvLayers_AfterSelect(sender As Object, e As LidorSystems.IntegralUI.ObjectEventArgs) Handles lvLayers.AfterSelect
        Try
            Dim selectedNode = TryCast(e.Object, TreeListViewNode)
            If (selectedNode IsNot Nothing) Then
                If Me.IsValidNodeSelected(selectedNode) AndAlso Not (currentSelectedModifierTag = selectedNode.Tag) Then
                    BindNodeThematic(selectedNode)
                ElseIf Not validThemes.Contains(selectedNode.Key) Then
                    currentSelectedModifierTag = String.Empty
                    ManagevgbThematic(False)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lvLayers_KeyDown(sender As Object, e As KeyEventArgs) Handles lvLayers.KeyDown
        Try
            Dim modifierToDelete As MapInfo.Mapping.FeatureStyleModifier = Nothing
            Dim layerToDelete As MapInfo.Mapping.FeatureLayer = Nothing
            If e.KeyCode = Keys.Delete Then
                Dim tlv As TreeListView = DirectCast(sender, TreeListView)
                Dim node As TreeListViewNode = tlv.SelectedNode
                If Not node Is Nothing Then

                    For Each layer As MapLayer In frmMapWindow.MapControl1.Map.Layers
                        Dim featureLayer As FeatureLayer = Me.IsValidLayer(layer)
                        If Not featureLayer Is Nothing Then

                            If featureLayer.Name = node.Tag Then
                                layerToDelete = featureLayer
                                Exit For
                            Else
                                For Each modifier As MapInfo.Mapping.FeatureStyleModifier In featureLayer.Modifiers
                                    If (featureLayer.Alias & "#" & modifier.Alias = node.Tag) Then
                                        modifierToDelete = modifier
                                    End If
                                Next
                                If Not modifierToDelete Is Nothing Then
                                    featureLayer.Modifiers.Remove(modifierToDelete)
                                    removeDraggedKPIList.Add(modifierToDelete.Alias)
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If Not layerToDelete Is Nothing Then
                        frmMapWindow.MapControl1.Map.Layers.Remove(layerToDelete)
                    End If

                    frmMapWindow.MapControl1.Refresh()
                    frmMapWindow.legend_ac.Refresh()
                    frmMapWindow.RefreshQuickThemeControl()

                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub BindNodeThematic(ByVal selectedNode As TreeListViewNode)
        If ((Not selectedNode Is Nothing) AndAlso validThemes.Contains(selectedNode.Key)) Then
            IsSliderSetRequired = True
            'This line required before call ManageThematicOnItemSelection becuase currentSelectedNoteTag is being used in this method
            currentSelectedFeatureLayerAlias = selectedNode.Text
            currentSelectedModifierTag = selectedNode.Tag
            WorkOnModifier(selectedNode, AddressOf ManageThematicOnItemSelection)
            IsSliderSetRequired = False
        ElseIf Not validThemes.Contains(selectedNode.Key) Then
            currentSelectedModifierTag = String.Empty
            ManagevgbThematic(False)
        End If
    End Sub

    Private Sub vcmbThematicType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles vcmbThematicType.SelectedIndexChanged
        Try
            Dim isRangedThemeSelected = (vcmbThematicType.SelectedItem.ToString = rangedTheme)
            vcmbThemticBinsDistribution.Enabled = isRangedThemeSelected
            vtbBins.Enabled = isRangedThemeSelected
        Catch
        End Try
    End Sub

    Private Sub vcmbThemticBinsDistribution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles vcmbThemticBinsDistribution.SelectedIndexChanged
        Try
            Dim selectedNode = lvLayers.SelectedNode
            If Me.IsValidNodeSelected(selectedNode) AndAlso (currentSelectedModifierTag = selectedNode.Tag) Then
                WorkOnModifier(selectedNode, AddressOf UpdateDistributionAndRecomputeBins)
            End If
        Catch
        End Try
    End Sub

    Private Sub UpdateDistributionAndRecomputeBins(ByRef modifier As MapInfo.Mapping.FeatureStyleModifier)
        If (modifier.GetType().Name = Me.rangedTheme) Then
            Dim rangedThemeModifier = modifier.ToRangesTheme()
            Dim distributionMethod As Thematics.DistributionMethod = Thematics.DistributionMethod.CustomRanges
            If vcmbThemticBinsDistribution.SelectedItem.ToString = "Quantile" Then
                distributionMethod = Thematics.DistributionMethod.BIQuantile
            ElseIf vcmbThemticBinsDistribution.SelectedItem.ToString = "Custom" Then
                distributionMethod = Thematics.DistributionMethod.CustomRanges
            ElseIf vcmbThemticBinsDistribution.SelectedItem.ToString = "Equal Ranges" Then
                distributionMethod = Thematics.DistributionMethod.EqualRangeSize
            ElseIf vcmbThemticBinsDistribution.SelectedItem.ToString = "Equal Count" Then
                distributionMethod = Thematics.DistributionMethod.EqualCountPerRange
            ElseIf vcmbThemticBinsDistribution.SelectedItem.ToString = "Natural Break" Then
                distributionMethod = Thematics.DistributionMethod.NaturalBreak
            ElseIf vcmbThemticBinsDistribution.SelectedItem.ToString = "Standard Deviation" Then
                distributionMethod = Thematics.DistributionMethod.StandardDeviation
            End If
            rangedThemeModifier.Distribution = distributionMethod
            rangedThemeModifier.RecomputeBins()
            ManageThematicOnItemSelection(rangedThemeModifier)
        End If
        If (frmMapWindow.legend_ac.Visible) Then
            frmMapWindow.legend_ac.Refresh()
        End If
    End Sub

    Private Sub frmQuickThemeControl_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        frmMapWindow.tbl_QuickTheme.Checked = False
        currentSelectedFeatureLayerAlias = String.Empty
        currentSelectedModifierTag = String.Empty
        ClearSelectedModifier()
        removeDraggedKPIList.Clear()
        KpiColorModifyList.Clear()
    End Sub

    Sub AddToModifierList(ByVal name As String, ByVal modifier As FeatureStyleModifier)
        If Not (selectedModifiersOriginalDataList.Keys.Contains(name)) Then
            If (modifier.IsRangesTheme()) Then
                Dim rangedThemeModifier = modifier.ToRangesTheme()
                If (rangedThemeModifier IsNot Nothing) Then
                    Dim modifierBackup As New RangedThemeBackup()
                    modifierBackup.Distribution = rangedThemeModifier.Distribution
                    For index = 1 To rangedThemeModifier.Bins.Count
                        Dim binData As New ThemeBinData()
                        binData.Max = rangedThemeModifier.Bins(index - 1).Max
                        binData.Min = rangedThemeModifier.Bins(index - 1).Min
                        binData.Color = rangedThemeModifier.Bins(index - 1).Style.SymbolStyle.Color
                        modifierBackup.ModifierThemeBins.Add(binData)
                    Next
                    selectedModifiersOriginalDataList.Add(name, modifierBackup)
                End If
            End If
        End If
    End Sub

    Sub RemoveModifierFromList(ByVal name As String)
        If (selectedModifiersOriginalDataList.Keys.Contains(name)) Then
            selectedModifiersOriginalDataList.Remove(name)
        End If
    End Sub

    Sub BindRangesBinsTable(ByRef bins As MapInfo.Mapping.Thematics.RangedThemeBins, ByVal exp As String)
        Me.tlpThemeBins.Visible = False
        Me.tlpIndividualBins.Visible = False

        Me.tlpThemeBins.SuspendLayout()
        Application.DoEvents()
        tlpThemeBins.Controls.Clear()
        Me.tlpThemeBins.Dock = DockStyle.Top
        Me.tlpThemeBins.ColumnCount = 6
        Me.tlpThemeBins.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpThemeBins.Location = New System.Drawing.Point(3, 3)

        If (bins.Count > vtbBins.Properties.Maximum) Then
            Me.tlpThemeBins.Height = (vtbBins.Properties.Maximum * 20) + 10
            Me.tlpThemeBins.RowCount = vtbBins.Properties.Maximum
        Else
            Me.tlpThemeBins.RowCount = bins.Count
            Me.tlpThemeBins.Height = bins.Count * 20
        End If
        Me.tlpThemeBins.Height = bins.Count * 21
        UpdateKPILabel(exp)
        Dim counter As Integer = 0
        For index = bins.Count - 1 To 0 Step -1

            Dim bin As MapInfo.Mapping.Thematics.RangedThemeBin = bins.Item(index)
            Me.tlpThemeBins.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))

            'aligning border color with interior color
            Try
                Dim si As SimpleInterior = bin.Style.AreaStyle.Interior
                Dim frcolor As Color = Color.FromArgb(255, si.ForeColor)
                bin.Style.AreaStyle.Border = New SimpleLineStyle(New LineWidth(3, 0), 2, frcolor, False)
            Catch ex As Exception
            End Try

            Dim panel As New Panel()
            panel.Enabled = True
            panel.Tag = bin

            If (bin.Style.SymbolStyle IsNot Nothing) Then
                If KpiColorModifyList.Count = 0 Then
                    panel.BackColor = bin.Style.SymbolStyle.Color
                Else
                    For Each kvp As KeyValuePair(Of String, Color) In KpiColorModifyList
                        Dim key As String = kvp.Key
                        Dim value As Color = kvp.Value
                        Dim keyParts As String() = key.Split(",")
                        If keyParts(1) = exp AndAlso keyParts(2) = Math.Round(bin.Min, 2) AndAlso keyParts(3) = Math.Round(bin.Max, 2) Then
                            panel.BackColor = value
                            Exit For
                        Else
                            panel.BackColor = bin.Style.SymbolStyle.Color
                        End If
                    Next
                End If
                AddHandler panel.MouseDown, AddressOf ChangeBinColor
            End If
            Dim minControl As System.Windows.Forms.Control
            Dim maxControl As System.Windows.Forms.Control
            If (vcmbThemticBinsDistribution.Text = customRangeText) Then
                Dim txtMin = GetDevExpressTextBox(bin.Min)
                txtMin.Tag = New TextBoxTagData(bin, BinValueType.Min)
                minControl = txtMin
                AddHandler txtMin.TextChanged, AddressOf txtCustomBins_TextChanges
                Dim txtMax = GetDevExpressTextBox(bin.Max)
                txtMax.Tag = New TextBoxTagData(bin, BinValueType.Max)
                maxControl = txtMax
                AddHandler txtMax.TextChanged, AddressOf txtCustomBins_TextChanges
            Else
                minControl = GetDevExpressLabel(bin.Min)
                maxControl = GetDevExpressLabel(bin.Max)
            End If

            Me.tlpThemeBins.Controls.Add(panel, 5, counter)
            Me.tlpThemeBins.Controls.Add(minControl, 0, counter)
            Me.tlpThemeBins.Controls.Add(GetDevExpressLabel("<="), 1, counter)
            Me.tlpThemeBins.Controls.Add(GetDevExpressLabel(kpiPrefix), 2, counter)
            Me.tlpThemeBins.Controls.Add(GetDevExpressLabel("<"), 3, counter)
            Me.tlpThemeBins.Controls.Add(maxControl, 4, counter)
            If (counter = vtbBins.Properties.Maximum - 1) Then
                Exit For
            End If
            counter = counter + 1
        Next
        Me.tlpThemeBins.ResumeLayout()
        Me.tlpThemeBins.Visible = True
        Application.DoEvents()
    End Sub

    Sub ChangeBinColor(ByVal bin As Object, ByVal e As EventArgs)
        Dim panel As Panel = TryCast(bin, Panel)
        If (panel IsNot Nothing) Then
            Dim breakAll As Boolean = False
            Dim result As DialogResult = clpBins.ShowDialog()
            If (result = DialogResult.OK) Then
                Dim tBin As MapInfo.Mapping.Thematics.RangedThemeBin = TryCast(panel.Tag, MapInfo.Mapping.Thematics.RangedThemeBin)
                Dim iBin As MapInfo.Mapping.Thematics.IndividualValueThemeBin = Nothing
                If (tBin Is Nothing) Then
                    iBin = TryCast(panel.Tag, MapInfo.Mapping.Thematics.IndividualValueThemeBin)
                End If

                If (tBin IsNot Nothing Or iBin IsNot Nothing) Then
                    panel.BackColor = clpBins.Color
                    For Each layer As MapLayer In frmMapWindow.MapControl1.Map.Layers
                        If (breakAll) Then
                            Exit For
                        End If
                        Dim featureLayer As FeatureLayer = Me.IsValidLayer(layer)
                        If Not featureLayer Is Nothing Then
                            If (featureLayer.Alias = currentSelectedFeatureLayerAlias) Then
                                For Each modifier As MapInfo.Mapping.FeatureStyleModifier In featureLayer.Modifiers
                                    If (breakAll) Then
                                        Exit For
                                    End If
                                    If (modifier.IsRangesTheme()) Then
                                        Dim rangedThemeModifier = modifier.ToRangesTheme()
                                        If (rangedThemeModifier.Expression = currentSelectedModifierExpression) Then
                                            For index = 1 To rangedThemeModifier.Bins.Count
                                                If rangedThemeModifier.Bins(index - 1).Value = tBin.Value AndAlso rangedThemeModifier.Bins(index - 1).Min = tBin.Min AndAlso rangedThemeModifier.Bins(index - 1).Max = tBin.Max Then
                                                    UpdateModifierStyleColor(rangedThemeModifier.Bins(index - 1).Style, clpBins.Color)
                                                    If KpiColorModifyList.Keys.Contains(featureLayer.Alias & "," & currentSelectedModifierExpression & "," & tBin.Min & "," & tBin.Max) Then
                                                        KpiColorModifyList.Remove(featureLayer.Alias & "," & currentSelectedModifierExpression & "," & tBin.Min & "," & tBin.Max)
                                                    End If
                                                    KpiColorModifyList.Add(featureLayer.Alias & "," & currentSelectedModifierExpression & "," & tBin.Min & "," & tBin.Max, clpBins.Color)
                                                    breakAll = True
                                                End If
                                            Next
                                        End If
                                    End If
                                    If (modifier.IsIndividualValueTheme()) Then
                                        Dim indivisuleThemeModifier = modifier.ToIndividualValueTheme()
                                        If (indivisuleThemeModifier.Expression = currentSelectedModifierExpression) Then
                                            For index = 1 To indivisuleThemeModifier.Bins.Count
                                                If indivisuleThemeModifier.Bins(index - 1).Value = iBin.Value AndAlso indivisuleThemeModifier.Bins(index - 1).Min = iBin.Min AndAlso indivisuleThemeModifier.Bins(index - 1).Max = iBin.Max Then
                                                    UpdateModifierStyleColor(indivisuleThemeModifier.Bins(index - 1).Style, clpBins.Color)
                                                    breakAll = True
                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Sub UpdateKPILabel(ByVal exp As String)
        vlblKPI.Text = kpiPrefix & " = " & exp
    End Sub

    Sub BindIndivisualBinsTable(ByRef bins As MapInfo.Mapping.Thematics.IndividualValueThemeBins, ByVal exp As String)
        Me.tlpIndividualBins.Visible = False
        Me.tlpThemeBins.Visible = False
        Me.tlpIndividualBins.SuspendLayout()
        Application.DoEvents()
        Me.tlpIndividualBins.Controls.Clear()
        Me.tlpIndividualBins.ColumnCount = 3
        Me.tlpIndividualBins.Dock = DockStyle.Top
        Me.tlpIndividualBins.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]

        Me.tlpIndividualBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpIndividualBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpIndividualBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpIndividualBins.Location = New System.Drawing.Point(3, 3)

        If (bins.Count > vtbBins.Properties.Maximum) Then
            Me.tlpIndividualBins.Height = (vtbBins.Properties.Maximum * 20) + 10
            Me.tlpIndividualBins.RowCount = vtbBins.Properties.Maximum
        Else
            Me.tlpIndividualBins.RowCount = bins.Count
            Me.tlpIndividualBins.Height = bins.Count * 20
        End If

        UpdateKPILabel(exp)
        Dim counter As Integer = 0
        For index = bins.Count - 1 To 0 Step -1
            Dim bin As MapInfo.Mapping.Thematics.IndividualValueThemeBin = bins.Item(index)
            Me.tlpIndividualBins.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))

            Dim panel As New Panel()
            panel.Dock = DockStyle.Fill
            panel.Enabled = True
            panel.Tag = bin
            If (bin.Style.SymbolStyle IsNot Nothing) Then
                panel.BackColor = bin.Style.SymbolStyle.Color
                AddHandler panel.MouseDown, AddressOf ChangeBinColor
            End If
            Me.tlpIndividualBins.Controls.Add(panel, 2, counter)
            Me.tlpIndividualBins.Controls.Add(GetDevExpressLabel(kpiPrefix), 0, counter)
            Me.tlpIndividualBins.Controls.Add(GetDevExpressLabel(bin.Value), 1, counter)
            If (counter = vtbBins.Properties.Maximum - 1) Then
                Exit For
            End If
            counter = counter + 1
        Next
        Me.tlpIndividualBins.Visible = True
        Me.tlpIndividualBins.ResumeLayout()
        Application.DoEvents()
    End Sub

    Sub BindThemeticType()
        vcmbThematicType.SuspendLayout()
        vcmbThematicType.Properties.Items.Clear()
        For Each item As String In validThemes
            vcmbThematicType.Properties.Items.Add(item)
        Next
        vcmbThematicType.ResumeLayout()
    End Sub

    Sub BindTreeColumns()
        lvLayers.Nodes.Clear()
        lvLayers.Columns.Clear()
        Dim column As New LidorSystems.IntegralUI.Lists.TreeListViewColumn()

        column.HeaderText = "Layers"
        column.Width = 250
        Me.lvLayers.Columns.Add(column)

        column = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        column.HeaderText = "Visible"
        column.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control
        column.Fixed = LidorSystems.IntegralUI.Lists.ColumnFixedType.Right
        column.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible
        column.Width = 50
        column.StyleFromParent = False
        column.FormatStyle.ContentAlign = HorizontalAlignment.Center
        Me.lvLayers.Columns.Add(column)
    End Sub

    Function GetDevExpressLabel(ByVal text As String) As DevExpress.XtraEditors.LabelControl
        Dim label As New DevExpress.XtraEditors.LabelControl()
        label.Text = text
        Return label
    End Function

    Function GetDevExpressTextBox(ByVal text As String) As DevExpress.XtraEditors.TextEdit
        Dim txt As New DevExpress.XtraEditors.TextEdit()
        txt.BackColor = System.Drawing.Color.White
        txt.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        txt.Location = New System.Drawing.Point(70, 176)
        txt.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        txt.SelectionLength = 0
        txt.SelectionStart = 0
        txt.Size = New System.Drawing.Size(68, 23)
        txt.TabIndex = 2
        txt.Enabled = True
        txt.Text = text
        Return txt
    End Function

    Function IsValidLayer(ByRef layer As MapLayer) As FeatureLayer
        Return TryCast(layer, FeatureLayer)
    End Function

    Function IsValidNodeSelected(ByVal selectedNode As TreeListViewNode) As Boolean
        Return ((Not selectedNode Is Nothing) AndAlso validThemes.Contains(selectedNode.Key))
    End Function

    Public Sub onCheckboxVisibilityChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        For Each layer As MapLayer In frmMapWindow.MapControl1.Map.Layers
            Dim featureLayer As FeatureLayer = Me.IsValidLayer(layer)
            If Not featureLayer Is Nothing Then
                If (layer.Alias = chk.Name) Then
                    layer.Enabled = chk.Checked
                End If
                For Each modifier As MapInfo.Mapping.FeatureStyleModifier In featureLayer.Modifiers
                    If (featureLayer.Alias & "#" & modifier.Alias = chk.Name) Then
                        If TypeOf modifier Is IndividualValueTheme Then
                            Dim thm As IndividualValueTheme = CType(modifier, IndividualValueTheme)
                            thm.VisibleRangeEnabled = False
                            thm.Enabled = chk.Checked
                        End If
                        modifier.VisibleRangeEnabled = False
                        modifier.Enabled = chk.Checked
                    End If
                Next
            End If
        Next
        frmMapWindow.ShowHideLegandsWithLayer(frmMapWindow.GetLayerFilterType())
    End Sub

    Public Sub LoadQuickThemeControl(layers As MapInfo.Mapping.Layers, Optional ByVal ignoreVisibility As Boolean = False)
        If Me.Visible = True Then
            'ManageQuickThemeControlPosition(objFrmMap, ignoreVisibility)
            QuickThemeControlStart(layers, ignoreVisibility)
            currentSelectedFeatureLayerAlias = String.Empty
            currentSelectedModifierTag = String.Empty
            BindThemeticType()
            'Me.TopLevel = False
            Me.TopMost = True
            'frmMDI.Controls.Add(Me)
            'Me.Show()
            'Me.BringToFront()
        End If
    End Sub

    Public Sub ManageQuickThemeControlPosition(parent As frmMapWindow, Optional ByVal ignoreVisibility As Boolean = False)
        If Me.Visible Or ignoreVisibility Then
            Me.SuspendLayout()
            Me.Top = parent.Location.Y + 106
            Me.Left = parent.Location.X + parent.Width - Me.Width - 3 ''parent.VExplorerBar1.Width + parent.MapControl1.Width - Me.Width + 6
            Me.Height = parent.MapControl1.Height
            Me.ResumeLayout()
        End If
    End Sub

    Public Sub QuickThemeControlStart(layers As MapInfo.Mapping.Layers, Optional ByVal ignoreVisibility As Boolean = False)
        Me.DoubleBuffered = True
        _logger.SetInfo("Quick Theme Started")

        If Me.Visible = True Or ignoreVisibility Then
            lvLayers.SuspendUpdate()
            currentSelectedModifierTag = ""
            BindTreeColumns()
            lvLayers.Columns(1).FormatStyle.ContentAlign = HorizontalAlignment.Center
            lvLayers.DropMarkerType = DropMarkerType.Full
            For Each layer As MapLayer In layers
                Dim featureLayer As FeatureLayer = Me.IsValidLayer(layer)
                If Not featureLayer Is Nothing Then

                    Dim treeNode As New TreeListViewNode()
                    treeNode.ToolTip = featureLayer.Alias
                    treeNode.ImageIndex = treeNode.SelectedImageIndex = 0
                    treeNode.StyleFromParent = False
                    treeNode.Key = featureLayer.GetType().Name
                    treeNode.Tag = featureLayer.Alias
                    Dim fItem As New TreeListViewSubItem(featureLayer.Name)
                    Dim thItem As TreeListViewSubItem = GetLayerVisibleCheckBox(featureLayer.IsVisible, featureLayer.Alias)
                    treeNode.SubItems.Add(fItem)
                    treeNode.SubItems.Add(thItem)
                    For Each modifier As MapInfo.Mapping.FeatureStyleModifier In featureLayer.Modifiers
                        If Not removeDraggedKPIList.Contains(modifier.Alias) Then
                            Dim subchildNode As New TreeListViewNode(featureLayer.Alias)
                            subchildNode.ToolTip = modifier.Alias
                            subchildNode.Key = modifier.GetType().Name
                            subchildNode.Tag = featureLayer.Alias & "#" & modifier.Alias
                            subchildNode.StyleFromParent = False
                            Dim cfItem As New TreeListViewSubItem(modifier.Name)
                            Dim cthItem As TreeListViewSubItem = GetLayerVisibleCheckBox(modifier.Visible, featureLayer.Alias & "#" & modifier.Alias)
                            subchildNode.SubItems.Add(cfItem)
                            subchildNode.SubItems.Add(cthItem)
                            treeNode.Nodes.Add(subchildNode)
                        End If
                    Next
                    lvLayers.Nodes.Add(treeNode)
                End If
            Next
            lvLayers.ResumeUpdate()
            lvLayers.ExpandAll()
            ManagevgbThematic(False)
        End If
        _logger.SetInfo("Quick Theme Complete")
    End Sub

    Public Sub OnParentFormMove()
        Me.BringToFront()
    End Sub

    Public Function GetLayerVisibleCheckBox(isVisible As Boolean, name As String) As TreeListViewSubItem
        Dim subchkbox As New CheckBox()
        subchkbox.Size = New System.Drawing.Size(16, 16)
        subchkbox.Location = New System.Drawing.Point(10, 10)
        subchkbox.Checked = isVisible
        subchkbox.Name = name
        AddHandler subchkbox.CheckedChanged, AddressOf onCheckboxVisibilityChanged
        Dim cthItem As New TreeListViewSubItem("")
        cthItem.Control = subchkbox
        cthItem.UpdateLayout()
        Return cthItem
    End Function

End Class

Class TextBoxTagData

    Public Sub New(bin As MapInfo.Mapping.Thematics.RangedThemeBin, valueType As BinValueType)
        _bin = bin
        _valueType = valueType
    End Sub

    Private _bin As MapInfo.Mapping.Thematics.RangedThemeBin
    Public Property Bin() As MapInfo.Mapping.Thematics.RangedThemeBin
        Get
            Return _bin
        End Get
        Set(ByVal value As MapInfo.Mapping.Thematics.RangedThemeBin)
            _bin = value
        End Set
    End Property

    Private _valueType As BinValueType
    Public Property ValueType() As BinValueType
        Get
            Return _valueType
        End Get
        Set(ByVal value As BinValueType)
            _valueType = value
        End Set
    End Property

End Class

Enum BinValueType
    Max
    Min
End Enum

Class RangedThemeBackup

    Public Sub New()
        _modifierThemeBins = New List(Of ThemeBinData)()
    End Sub

    Private _distribution As DistributionMethod
    Public Property Distribution() As DistributionMethod
        Get
            Return _distribution
        End Get
        Set(ByVal value As DistributionMethod)
            _distribution = value
        End Set
    End Property

    Private _modifierThemeBins As List(Of ThemeBinData)
    Public Property ModifierThemeBins() As List(Of ThemeBinData)
        Get
            Return _modifierThemeBins
        End Get
        Set(ByVal value As List(Of ThemeBinData))
            _modifierThemeBins = value
        End Set
    End Property
End Class

Class ThemeBinData

    Private _min As Double
    Public Property Min() As Double
        Get
            Return _min
        End Get
        Set(ByVal value As Double)
            _min = value
        End Set
    End Property

    Private _max As Double
    Public Property Max() As Double
        Get
            Return _max
        End Get
        Set(ByVal value As Double)
            _max = value
        End Set
    End Property

    Private _color As System.Drawing.Color
    Public Property Color() As System.Drawing.Color
        Get
            Return _color
        End Get
        Set(ByVal value As System.Drawing.Color)
            _color = value
        End Set
    End Property

End Class