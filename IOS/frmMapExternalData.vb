Imports IOS.Library
Imports MapInfo.Data
Imports MapInfo.Engine
Imports MapInfo.Mapping
Imports DevExpress.XtraEditors

Public Class frmMapExternalData

    Dim conStr As String = IOS.Configuration.IOSAppConfigManage.IOSServer
    Private defaultEX As Integer = -1
    Private defaultSelectionColumn As String = "CELLNAME"
    Private enableFormLevelDoubleBuffering As Boolean = True
    Private externalData As DataTable = Nothing

    Public Sub SetConnectionString(ByVal conStr As String)
        Me.conStr = conStr
    End Sub

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

    Private Sub frmMapExternalData_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub frmMapExternalData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowInTaskbar = True
        frmMapWindow.BindCombowithThematicData(cmbJoinsToMapField)
        cmbThemticType.SelectedIndex = 1
        Me.ConfigurIOSMapForm("frmMapExternalData")
    End Sub

    Private Sub frmMapExternalData_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        frmMDI.bbtnMapImport.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default
    End Sub

    Private Sub cmbJoinDataGridField_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJoinDataGridField.SelectedIndexChanged
        Dim itemList As New List(Of clsComboBoxItem)
        For Each itm As clsComboBoxItem In cmbJoinsToMapField.Properties.Items
            itemList.Add(itm)
        Next
        If (cmbJoinDataGridField.Properties.Items.Count > 0) Then
            If (itemList.Where(Function(w) w.Text.ToUpper = cmbJoinDataGridField.Text.ToUpper).Any()) Then
                cmbJoinsToMapField.Text = cmbJoinDataGridField.Text
            End If
        End If
    End Sub

    Public Sub ConfigurIOSMapForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)
        End If
    End Sub

    Sub clearComboBox(ByRef control As DevExpress.XtraEditors.ComboBoxEdit, ByVal firstItem As String)
        control.SuspendLayout()
        control.Properties.Items.Clear()
        control.SelectedIndex = 0
        control.Refresh()
        control.ResumeLayout()
    End Sub

    Private Sub vbtnMap_Click(sender As Object, e As EventArgs) Handles btnMap.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If ceMatchThematic.Checked = False Then
                MapToIndividualLayers()
            Else
                MapToSingleLayer()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub MapToIndividualLayers()
        Try
            Dim connection As New MIConnection
            Dim tableNameAlias = "ExternalMapData"

            If (externalData IsNot Nothing) Then
                Dim cloneData As DataTable = externalData.Clone()
                If (cmbThemticType.Text = "Ranged Theme") Then
                    '' Ranged theme require numeric type column
                    cloneData.Columns(cmbThematicFields.Text).DataType = System.Type.GetType("System.Decimal")
                    For Each row As DataRow In externalData.Rows
                        If (IsDBNull(row(cmbThematicFields.Text))) Then
                            Continue For
                        ElseIf (row(cmbThematicFields.Text).ToString().Trim = "") Then
                            Continue For
                        Else
                            Dim result As Decimal = 0
                            If (Not Decimal.TryParse(row(cmbThematicFields.Text), result)) Then
                                XtraMessageBox.Show("Data '" & row(cmbThematicFields.Text) & "' in thematic column is not a valid number", "Please correct you data.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                        End If
                        cloneData.ImportRow(row)
                    Next
                End If
                connection.Open()
                connection.Catalog.CloseTable(tableNameAlias)
                Dim ti As TableInfoAdoNet = New TableInfoAdoNet(tableNameAlias)
                ti.ReadOnly = True
                ti.DataTable = IIf((cmbThemticType.Text = "Ranged Theme"), cloneData, externalData)
                Dim tbl_ExternalData As Table = connection.Catalog.OpenTable(ti)
                For Each drow As DataRow In dt_Map_Configuration.Rows
                    If drow("LayerActive").ToString = True Then
                        Dim tbl As MapInfo.Data.Table = Session.Current.Catalog.GetTable(drow("LayerName").ToString.Trim)
                        If ceMapToVoronoi.Checked = True Then
                            tbl = Session.Current.Catalog.GetTable(drow("LayerName").ToString.Trim & "_Voronoi")
                        End If
                        If tbl IsNot Nothing AndAlso (tbl.TableInfo.TableType = TableType.Native And tbl.Alias.Contains("Cells_")) Then
                            Dim Sql = "Select " & tbl_ExternalData.Alias & ".*," & tbl.Alias & ".obj from " & tbl_ExternalData.Alias & ", " & tbl.Alias & "  WHERE " & tbl_ExternalData.Alias & "." & cmbJoinDataGridField.Text.Trim.Replace(" ", "_") & " = " & tbl.Alias & "." & cmbJoinsToMapField.Text
                            Dim command As MICommand = connection.CreateCommand()
                            command.CommandText = Sql

                            connection.Catalog.CloseTable(tbl.Alias & "_" & tableNameAlias)
                            Dim irfc As IResultSetFeatureCollection = command.ExecuteFeatureCollection(tbl.Alias & "_" & tableNameAlias)
                            command.Dispose()

                            If irfc.Count <> 0 Then

                                Dim lyr As New FeatureLayer(irfc.Table)
                                frmMapWindow.MapControl1.Map.Layers.Insert(0, lyr)
                                Dim lyr2 As FeatureLayer = CType(frmMapWindow.MapControl1.Map.Layers(tbl.Alias), FeatureLayer)
                                If lyr2 IsNot Nothing Then
                                    frmMapWindow.Layer_View(irfc.Alias, lyr2.Enabled)
                                End If
                                MapInfo.Mapping.LayerHelper.SetSelectable(lyr, False)

                                If (cmbThemticType.Text = "Ranged Theme") Then
                                    Dim rangedThematic As MapInfo.Mapping.Thematics.RangedTheme = New MapInfo.Mapping.Thematics.RangedTheme(lyr, cmbThematicFields.Text, tbl.Alias & "_" & tableNameAlias, 6, Thematics.DistributionMethod.EqualRangeSize)

                                    'starting color
                                    rangedThematic.Bins(0).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, Color.DarkBlue, False)
                                    rangedThematic.Bins(0).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(2, Color.DarkBlue)

                                    rangedThematic.InflectionColor = Color.Yellow
                                    rangedThematic.InflectionIndex = 3
                                    rangedThematic.Inflected = True

                                    rangedThematic.Recompute()

                                    For i = 0 To rangedThematic.Bins.Count - 1
                                        Dim si As MapInfo.Styles.SimpleInterior = rangedThematic.Bins(i).Style.AreaStyle.Interior
                                        Dim frcolor As Color = Color.FromArgb(128, si.ForeColor)
                                        Dim frcolorbrdr As Color = Color.FromArgb(255, si.ForeColor)

                                        rangedThematic.Bins(i).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, frcolorbrdr, False)
                                        rangedThematic.Bins(i).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(si.Pattern, frcolor)

                                    Next

                                    rangedThematic.Bins(rangedThematic.InflectionIndex).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, rangedThematic.InflectionColor, False)
                                    rangedThematic.Bins(rangedThematic.InflectionIndex).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(2, rangedThematic.InflectionColor)
                                    lyr.Modifiers.Append(rangedThematic)

                                    frmMapWindow.Legend_CreateThematic(rangedThematic, lyr.Alias, rangedThematic.Expression, lyr)
                                ElseIf (cmbThemticType.Text = "Individual Value Theme") Then
                                    Dim individualValueTheme As MapInfo.Mapping.Thematics.IndividualValueTheme = New MapInfo.Mapping.Thematics.IndividualValueTheme(lyr, cmbThematicFields.Text, tbl.Alias & "_" & tableNameAlias)

                                    For i = 0 To individualValueTheme.Bins.Count - 1
                                        Dim si As MapInfo.Styles.SimpleInterior = individualValueTheme.Bins(i).Style.AreaStyle.Interior
                                        Dim frcolor As Color = Color.FromArgb(128, si.ForeColor)
                                        Dim frcolorbrdr As Color = Color.FromArgb(255, si.ForeColor)
                                        individualValueTheme.Bins(i).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, frcolorbrdr, False)
                                        individualValueTheme.Bins(i).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(si.Pattern, frcolor)
                                    Next
                                    individualValueTheme.Name = cmbThematicFields.Text
                                    individualValueTheme.RecomputeBins()

                                    lyr.Modifiers.Append(individualValueTheme)
                                    frmMapWindow.Legend_CreateThematic(individualValueTheme, lyr.Alias, individualValueTheme.Expression, lyr)

                                End If
                                MapInfo.Tools.MapTool.SetInfoTipExpression(frmMapWindow.MapControl1.Tools.MapToolProperties, lyr, frmMapWindow.ColumnsInfoTip_MapinfoTable(tbl_ExternalData))
                                frmMapWindow.RefreshQuickThemeControl()
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
        End Try
    End Sub

    Private Sub MapToSingleLayer()
        Try
            Dim connection As New MIConnection
            Dim tableNameAlias = "ExternalMapData"

            If (externalData IsNot Nothing) Then
                Dim cloneData As DataTable = externalData.Clone()
                If (cmbThemticType.Text = "Ranged Theme") Then
                    '' Ranged theme require numeric type column
                    cloneData.Columns(cmbThematicFields.Text).DataType = System.Type.GetType("System.Decimal")
                    For Each row As DataRow In externalData.Rows
                        If (IsDBNull(row(cmbThematicFields.Text))) Then
                            Continue For
                        ElseIf (row(cmbThematicFields.Text).ToString().Trim = "") Then
                            Continue For
                        Else
                            Dim result As Decimal = 0
                            If (Not Decimal.TryParse(row(cmbThematicFields.Text), result)) Then
                                XtraMessageBox.Show("Data '" & row(cmbThematicFields.Text) & "' in thematic column is not a valid number", "Please correct you data.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                        End If
                        cloneData.ImportRow(row)
                    Next
                End If
                connection.Open()
                connection.Catalog.CloseTable(tableNameAlias)
                Dim ti As TableInfoAdoNet = New TableInfoAdoNet(tableNameAlias)
                ti.ReadOnly = True
                ti.DataTable = IIf((cmbThemticType.Text = "Ranged Theme"), cloneData, externalData)
                Dim tbl_ExternalData As Table = connection.Catalog.OpenTable(ti)
                Dim tbl_map As MapInfo.Data.Table = Nothing
                Dim MaptableName = tableNameAlias & "_Map"

                For Each drow As DataRow In dt_Map_Configuration.Rows
                    If drow("LayerActive").ToString = True Then
                        Dim tbl As MapInfo.Data.Table = Session.Current.Catalog.GetTable(drow("LayerName").ToString.Trim)

                        If ceMapToVoronoi.Checked = True Then
                            tbl = Session.Current.Catalog.GetTable(drow("LayerName").ToString.Trim & "_Voronoi")
                        End If
                        If tbl IsNot Nothing AndAlso (tbl.TableInfo.TableType = TableType.Native And tbl.Alias.Contains("Cells_")) Then
                            Dim Sql = "Select " & tbl_ExternalData.Alias & ".*," & tbl.Alias & ".obj from " & tbl_ExternalData.Alias & ", " & tbl.Alias & "  WHERE " & tbl_ExternalData.Alias & "." & cmbJoinDataGridField.Text.Trim.Replace(" ", "_") & " = " & tbl.Alias & "." & cmbJoinsToMapField.Text
                            Dim command As MICommand = connection.CreateCommand()
                            command.CommandText = Sql
                            Dim irfc As IResultSetFeatureCollection = command.ExecuteFeatureCollection()

                            command.Dispose()
                            If irfc.Count <> 0 Then
                                If connection.Catalog.GetTable(MaptableName) Is Nothing Then
                                    Dim ti_memtbl As MapInfo.Data.TableInfoMemTable = CType(MapInfo.Data.TableInfoFactory.CreateFromFeatureCollection(MaptableName, MapInfo.Data.TableType.MemTable, irfc), MapInfo.Data.TableInfoMemTable)
                                    tbl_map = Session.Current.Catalog.CreateTable(ti_memtbl)
                                    tbl_map.InsertFeatures(irfc)
                                Else
                                    tbl_map = Session.Current.Catalog.GetTable(MaptableName)
                                    tbl_map.InsertFeatures(irfc)
                                End If
                                command.Dispose()
                            End If
                        End If
                    End If
                Next

                If Not tbl_map Is Nothing Then
                    Dim lyr As New FeatureLayer(tbl_map)
                    frmMapWindow.MapControl1.Map.Layers.Insert(0, lyr)

                    If (cmbThemticType.Text = "Ranged Theme") Then
                        Dim rangedThematic As MapInfo.Mapping.Thematics.RangedTheme = New MapInfo.Mapping.Thematics.RangedTheme(lyr, cmbThematicFields.Text, MaptableName, 6, Thematics.DistributionMethod.EqualRangeSize)

                        'starting color
                        rangedThematic.Bins(0).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, Color.DarkBlue, False)
                        rangedThematic.Bins(0).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(2, Color.DarkBlue)

                        rangedThematic.InflectionColor = Color.Yellow
                        rangedThematic.InflectionIndex = 3
                        rangedThematic.Inflected = True

                        rangedThematic.Recompute()

                        For i = 0 To rangedThematic.Bins.Count - 1
                            Dim si As MapInfo.Styles.SimpleInterior = rangedThematic.Bins(i).Style.AreaStyle.Interior
                            Dim frcolor As Color = Color.FromArgb(128, si.ForeColor)
                            Dim frcolorbrdr As Color = Color.FromArgb(255, si.ForeColor)

                            rangedThematic.Bins(i).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, frcolorbrdr, False)
                            rangedThematic.Bins(i).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(si.Pattern, frcolor)

                        Next

                        rangedThematic.Bins(rangedThematic.InflectionIndex).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, rangedThematic.InflectionColor, False)
                        rangedThematic.Bins(rangedThematic.InflectionIndex).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(2, rangedThematic.InflectionColor)
                        lyr.Modifiers.Append(rangedThematic)
                        frmMapWindow.Legend_CreateThematic(rangedThematic, lyr.Alias, rangedThematic.Expression, Nothing)
                    ElseIf (cmbThemticType.Text = "Individual Value Theme") Then
                        Dim individualValueTheme As MapInfo.Mapping.Thematics.IndividualValueTheme = New MapInfo.Mapping.Thematics.IndividualValueTheme(lyr, cmbThematicFields.Text, MaptableName)

                        For i = 0 To individualValueTheme.Bins.Count - 1
                            Dim si As MapInfo.Styles.SimpleInterior = individualValueTheme.Bins(i).Style.AreaStyle.Interior
                            Dim frcolor As Color = Color.FromArgb(128, si.ForeColor)
                            Dim frcolorbrdr As Color = Color.FromArgb(255, si.ForeColor)
                            individualValueTheme.Bins(i).Style.AreaStyle.Border = New MapInfo.Styles.SimpleLineStyle(New MapInfo.Styles.LineWidth(3, 0), 2, frcolorbrdr, False)
                            individualValueTheme.Bins(i).Style.AreaStyle.Interior = New MapInfo.Styles.SimpleInterior(si.Pattern, frcolor)
                        Next
                        individualValueTheme.Name = cmbThematicFields.Text
                        individualValueTheme.RecomputeBins()

                        lyr.Modifiers.Append(individualValueTheme)
                        frmMapWindow.Legend_CreateThematic(individualValueTheme, lyr.Alias, individualValueTheme.Expression, Nothing)

                    End If
                    MapInfo.Tools.MapTool.SetInfoTipExpression(frmMapWindow.MapControl1.Tools.MapToolProperties, lyr, frmMapWindow.ColumnsInfoTip_MapinfoTable(tbl_ExternalData))
                    frmMapWindow.RefreshQuickThemeControl()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
        End Try
    End Sub

    Private Sub tsmi_TagPaste_Paste_Click(sender As Object, e As EventArgs) Handles tsmiTagPastePaste.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim s As String = Clipboard.GetText()                   'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim i As Integer
            externalData = New DataTable()
            Dim clipboardmatches As Integer = 0
            Try
                gcExternalData.SuspendLayout()
                gvExternalData.Columns.Clear()
                Dim itemList As New List(Of IOS.Library.clsComboBoxItem)
                For Each itm As IOS.Library.clsComboBoxItem In cmbJoinsToMapField.Properties.Items
                    itemList.Add(itm)
                Next

                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim delimeter As String = cmbDelimiter.Text
                    Dim bufferCell() As String = rows(i).Split(IIf(delimeter.ToUpper() = "TAB", ControlChars.Tab, delimeter))
                    '' Bind headers if i=0 (For first row)
                    If (i = 0) Then
                        Dim columns = bufferCell.Select(Function(w) New DataColumn(Replace(w, " ", "_"))).ToArray()
                        externalData.Columns.AddRange(columns)

                        BindCombowithValues(cmbJoinDataGridField, bufferCell)
                        If (bufferCell.Where(Function(w) w.Trim.ToUpper = defaultSelectionColumn.ToUpper).Any() AndAlso itemList.Where(Function(w) w.Text.ToUpper = defaultSelectionColumn.ToUpper).Any()) Then
                            cmbJoinDataGridField.Text = defaultSelectionColumn
                            cmbJoinsToMapField.Text = defaultSelectionColumn
                        Else
                            If (cmbJoinDataGridField.Properties.Items.Count > 0) Then
                                cmbJoinDataGridField.SelectedIndex = 0
                                If (itemList.Where(Function(w) w.Text.ToUpper = cmbJoinDataGridField.Text.ToUpper).Any()) Then
                                    cmbJoinsToMapField.Text = cmbJoinDataGridField.Text
                                End If
                            End If
                        End If
                        BindCombowithValues(cmbThematicFields, bufferCell, 1)
                        If cmbThematicFields.Text = cmbJoinDataGridField.Text Then
                            If cmbThematicFields.SelectedIndex < cmbThematicFields.Properties.Items.Count Then
                                cmbThematicFields.SelectedIndex = cmbThematicFields.SelectedIndex + 1
                            Else
                                cmbThematicFields.SelectedIndex = cmbThematicFields.SelectedIndex - 1
                            End If
                        End If
                    Else
                        Dim row As DataRow = externalData.NewRow()
                        '' Starting from 1 as we have added id column on 0th index
                        For index = 0 To bufferCell.Length - 1
                            row(index) = bufferCell(index).Trim()
                        Next
                        externalData.Rows.Add(row)
                    End If
                Next
                gcExternalData.DataSource = externalData
                gcExternalData.ResumeLayout()
            Catch ex As Exception
                Dim a As String = ex.Message
            End Try
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Public Sub BindCombowithValues(ByRef combo As DevExpress.XtraEditors.ComboBoxEdit, values() As String, Optional ByVal selectedIndex As Integer = 0)
        Try
            clearComboBox(combo, "")
            If combo.Properties.Items.Count = 0 Then
                'collect all unique fields of all open Cells_tables
                For Each item As String In values
                    Dim li As New IOS.Library.clsComboBoxItem()
                    li.Text = item
                    li.Value = item
                    If Not combo.Properties.Items.Contains(li) Then
                        combo.Properties.Items.Add(li)
                    End If
                Next
            End If
            combo.Properties.Sorted = True
            If (selectedIndex > 0 AndAlso values.Length > selectedIndex) Then
                combo.SelectedIndex = selectedIndex
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindGridColumns(bufferCell() As String)
        gcExternalData.SuspendLayout()
        gcExternalData.DataSource = Nothing
        gvExternalData.Columns.Clear()
        For Each item As String In bufferCell
            Dim columns As New DevExpress.XtraGrid.Columns.GridColumn
            columns.Caption = item
            gvExternalData.Columns.Add(columns)
        Next
        gcExternalData.ResumeLayout()
    End Sub

    Private Function GetStringFromCell(bufferCell As String) As String
        If bufferCell.ToString.Contains(ControlChars.Lf) Then
            bufferCell = bufferCell.ToString.Replace(ControlChars.Lf, "")
        End If
        Return bufferCell.ToString.Trim
    End Function

    Private Sub dgv_ExternalData_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.V Then
            tsmi_TagPaste_Paste_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub gvExternalData_KeyDown(sender As Object, e As KeyEventArgs) Handles gvExternalData.KeyDown
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.V Then
            tsmi_TagPaste_Paste_Click(Nothing, Nothing)
        End If
    End Sub

End Class