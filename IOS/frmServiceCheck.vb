Imports DevExpress.XtraGauges.Core.Drawing
Imports DevExpress.XtraGauges.Core.Model
Imports DevExpress.XtraGauges.Win.Gauges.State
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraVerticalGrid
Imports IOS.DataLibrary
Imports IOS.Library
Imports MapInfo.Data

Public Class frmServiceCheck

#Region "Variables"

    Public networkArea As String = Nothing
    Private dtSettings As DataTable = Nothing
    Private dtGrid As DataTable = Nothing
    Private dtMapData As DataTable = Nothing
    Private colsToColor As New List(Of String)

    Dim sig2G As StateIndicatorGauge = Nothing
    Dim sic2G As StateIndicatorComponent = Nothing
    Dim sig3G As StateIndicatorGauge = Nothing
    Dim sic3G As StateIndicatorComponent = Nothing
    Dim sig4G As StateIndicatorGauge = Nothing
    Dim sic4G As StateIndicatorComponent = Nothing
    Dim sig5G As StateIndicatorGauge = Nothing
    Dim sic5G As StateIndicatorComponent = Nothing

#End Region

#Region "Properties"

    Private _poslat As Double
    Public Property posLat() As Double
        Get
            Return _poslat
        End Get
        Set(ByVal value As Double)
            _poslat = value
        End Set
    End Property

    Private _poslng As Double
    Public Property posLng() As Double
        Get
            Return _poslng
        End Get
        Set(ByVal value As Double)
            _poslng = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Sub OnParentFormMove()
        Me.BringToFront()
    End Sub

    Public Sub frmServiceCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadSettings()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnGeocoder_Click(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtGeoCoderSearch.Properties.ButtonClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'clear map lines/markers
            frmMapWindow.CloseMapTables_RemoveLayerModifiers()
            frmMapWindow.RemoveLabelLayers()
            frmMapWindow.MapControl1.Map.Legends.Clear()

            LoadNetworkServiceDetails()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frmServiceCheck_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Dim btnSC As ToolStripButton = frmMapWindow.map_ToolStrip.Items("btnSrvCheck")
            If btnSC IsNot Nothing Then
                btnSC.CheckState = CheckState.Unchecked
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tglbtnGetMapCoords_Click(sender As Object, e As EventArgs) Handles tglbtnGetMapCoords.Click
        Try
            tglbtnGetMapCoords.ChangeToggleState()
            If tglbtnGetMapCoords.ToggleState = CheckState.Checked Then
                frmMapWindow.MapControl1.Cursor = Cursors.Cross
                Application.DoEvents()
                getMapCoords = True
            Else
                getMapCoords = False
                frmMapWindow.MapControl1.Cursor = Cursors.Default
                Application.DoEvents()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub gvDetails_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles gvDetails.CellMerge
        Try
            Dim view As GridView = CType(sender, GridView)
            Dim val1 As String = view.GetRowCellValue(e.RowHandle1, e.Column)
            Dim val2 As String = view.GetRowCellValue(e.RowHandle2, e.Column)
            e.Merge = (val1.ToString = val2.ToString)
            e.Handled = True
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub gvDetails_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gvDetails.RowCellStyle
        Try
            If colsToColor.Contains(e.Column.FieldName) AndAlso e.Column.FieldName <> "KPIName" AndAlso dtGrid.Rows(e.RowHandle)("KPIName") = "Availability" Then
                If CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 0 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < 50 Then
                    e.Appearance.BackColor = Color.Red
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 50 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < 100 Then
                    e.Appearance.BackColor = Color.Orange
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 100 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) <= 101 Then
                    e.Appearance.BackColor = Color.LightGreen
                End If
            ElseIf colsToColor.Contains(e.Column.FieldName) AndAlso e.Column.FieldName <> "KPIName" AndAlso dtGrid.Rows(e.RowHandle)("KPIName") = "CongestionRatio" Then
                If CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 0 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < 2 Then
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 2 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < 10 Then
                    e.Appearance.BackColor = Color.Orange
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 10 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) <= 101 Then
                    e.Appearance.BackColor = Color.Red
                End If
            ElseIf colsToColor.Contains(e.Column.FieldName) AndAlso e.Column.FieldName <> "KPIName" AndAlso dtGrid.Rows(e.RowHandle)("KPIName") = "UtilizationRatio" Then
                If CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 0 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < 25 Then
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 25 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < 75 Then
                    e.Appearance.BackColor = Color.Orange
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= 75 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) <= 101 Then
                    e.Appearance.BackColor = Color.Red
                End If
            ElseIf e.Column.FieldName = "CoverageAsPlanned" Then
                If CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= -150 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < -90 Then
                    e.Appearance.BackColor = Color.Red
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= -90 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) < -80 Then
                    e.Appearance.BackColor = Color.Orange
                ElseIf CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) >= -80 And CDbl(dtGrid.Rows(e.RowHandle)(e.Column.FieldName)) <= 0 Then
                    e.Appearance.BackColor = Color.LightGreen
                End If
            ElseIf e.Column.FieldName = "Last24Hours" Then
                If dtGrid.Rows(e.RowHandle)("KPIName") = "Availability" Then
                    If CDbl(e.CellValue) >= 0 And CDbl(e.CellValue) < 50 Then
                        e.Appearance.BackColor = Color.Red
                    ElseIf CDbl(e.CellValue) >= 50 And CDbl(e.CellValue) < 100 Then
                        e.Appearance.BackColor = Color.Orange
                    ElseIf CDbl(e.CellValue) >= 100 And CDbl(e.CellValue) <= 101 Then
                        e.Appearance.BackColor = Color.LightGreen
                    End If
                ElseIf dtGrid.Rows(e.RowHandle)("KPIName") = "CongestionRatio" Then
                    If CDbl(e.CellValue) >= 0 And CDbl(e.CellValue) < 2 Then
                        e.Appearance.BackColor = Color.LightGreen
                    ElseIf CDbl(e.CellValue) >= 2 And CDbl(e.CellValue) < 10 Then
                        e.Appearance.BackColor = Color.Orange
                    ElseIf CDbl(e.CellValue) >= 10 And CDbl(e.CellValue) <= 101 Then
                        e.Appearance.BackColor = Color.Red
                    End If
                ElseIf dtGrid.Rows(e.RowHandle)("KPIName") = "UtilizationRatio" Then
                    If CDbl(e.CellValue) >= 0 And CDbl(e.CellValue) < 25 Then
                        e.Appearance.BackColor = Color.LightGreen
                    ElseIf CDbl(e.CellValue) >= 25 And CDbl(e.CellValue) < 75 Then
                        e.Appearance.BackColor = Color.Orange
                    ElseIf CDbl(e.CellValue) >= 75 And CDbl(e.CellValue) <= 101 Then
                        e.Appearance.BackColor = Color.Red
                    End If
                End If
            End If
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            e.Appearance.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tglbtnHistory_Click(sender As Object, e As EventArgs) Handles tglbtnHistory.Click
        Try
            tglbtnHistory.ChangeToggleState()
            If tglbtnHistory.ToggleState = CheckState.Checked Then
                ShowHideDateColumns(True)
            Else
                ShowHideDateColumns(False)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub ShowHideDateColumns(bVal As Boolean)
        For Each colName As String In colsToColor
            If colName <> "KPIName" Then
                gvDetails.Columns(colName).Visible = bVal
            End If
        Next
        gvDetails.Columns("Last24Hours").VisibleIndex = gvDetails.VisibleColumns.Count
    End Sub

    Private Sub LoadSettings()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(5501, parray)(0)
        sqlParam = GetSQL(5501, parray)(1)
        dtSettings = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub ManagTechSummaryIndicator(tech As String, clrRank As Integer)
        Dim shapes As StateIndicatorShapeType() = New StateIndicatorShapeType() {
            StateIndicatorShapeType.ElectricLight1,
            StateIndicatorShapeType.ElectricLight2,
            StateIndicatorShapeType.ElectricLight3,
            StateIndicatorShapeType.ElectricLight4
        }

        'ElectricLight1 = 0 - Gray (rank N/A)
        'ElectricLight2 = 1 - Red (rank 3)
        'ElectricLight3 = 2 - Yellow (rank 2)
        'ElectricLight4 = 3 - Green (rank 1)

        Select Case tech
            Case "2G"
                If sig2G IsNot Nothing Then
                    state2G.Gauges.Remove(sig2G)
                End If
                sig2G = state2G.AddStateIndicatorGauge()
                sic2G = sig2G.AddIndicator()
                Dim is2G As IndicatorState = New IndicatorState()
                Select Case clrRank
                    Case 1
                        is2G.ShapeType = shapes(3)
                    Case 2
                        is2G.ShapeType = shapes(2)
                    Case 3
                        is2G.ShapeType = shapes(1)
                    Case 0
                        is2G.ShapeType = shapes(0)
                End Select
                sic2G.States.Add(is2G)
                sic2G.Size = New SizeF(250, 250)
                sic2G.StateIndex = 1
            Case "3G"
                If sig3G IsNot Nothing Then
                    state3G.Gauges.Remove(sig3G)
                End If
                sig3G = state3G.AddStateIndicatorGauge()
                sic3G = sig3G.AddIndicator()
                Dim is3G As IndicatorState = New IndicatorState()
                Select Case clrRank
                    Case 1
                        is3G.ShapeType = shapes(3)
                    Case 2
                        is3G.ShapeType = shapes(2)
                    Case 3
                        is3G.ShapeType = shapes(1)
                    Case 0
                        is3G.ShapeType = shapes(0)
                End Select
                sic3G.States.Add(is3G)
                sic3G.Size = New SizeF(250, 250)
                sic3G.StateIndex = 1
            Case "4G"
                If sig4G IsNot Nothing Then
                    state4G.Gauges.Remove(sig4G)
                End If
                sig4G = state4G.AddStateIndicatorGauge()
                sic4G = sig4G.AddIndicator()
                Dim is4G As IndicatorState = New IndicatorState()
                Select Case clrRank
                    Case 1
                        is4G.ShapeType = shapes(3)
                    Case 2
                        is4G.ShapeType = shapes(2)
                    Case 3
                        is4G.ShapeType = shapes(1)
                    Case 0
                        is4G.ShapeType = shapes(0)
                End Select
                sic4G.States.Add(is4G)
                sic4G.Size = New SizeF(250, 250)
                sic4G.StateIndex = 1
            Case "5G"
                If sig5G IsNot Nothing Then
                    state5G.Gauges.Remove(sig5G)
                End If
                sig5G = state5G.AddStateIndicatorGauge()
                sic5G = sig5G.AddIndicator()
                Dim is5G As IndicatorState = New IndicatorState()
                Select Case clrRank
                    Case 1
                        is5G.ShapeType = shapes(3)
                    Case 2
                        is5G.ShapeType = shapes(2)
                    Case 3
                        is5G.ShapeType = shapes(1)
                    Case 0
                        is5G.ShapeType = shapes(0)
                End Select
                sic5G.States.Add(is5G)
                sic5G.Size = New SizeF(250, 250)
                sic5G.StateIndex = 1
        End Select
    End Sub

    Private Sub GetNetworkServiceData()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        Dim parray()() As String = {
            New String() {"@x", posLng},
            New String() {"@y", posLat},
            New String() {"@networkArea", Chr(39) & networkArea & Chr(39)}
        }
        strConnection = GetSQL(5500, parray)(0)
        sqlParam = GetSQL(5500, parray)(1)
        dtGrid = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dtGrid.Rows.Count > 0 Then

            LoadTechSummaryData(dtGrid, vGrid2GSumm, "2G")
            LoadTechSummaryData(dtGrid, vGrid3GSumm, "3G")
            LoadTechSummaryData(dtGrid, vGrid4GSumm, "4G")
            LoadTechSummaryData(dtGrid, vGrid5GSumm, "5G")

            Dim colsToHide() As String = {"IOS_TECH", "LAYER", "IOS_CELL_GID", "SiteX", "SiteY"}
            IOSDevExpressGrid.PopulateDataInGrid(gcDetails, gvDetails, dtGrid, "ALL", colsToHide, Nothing)

            Dim colsToLeave() As String = {"TECHNOLOGY", "IOS_TECH", "LAYER", "SITENAME", "IOS_CELL_GID", "SiteX", "SiteY", "S_CELLNAME", "DistanceToSite", "CoverageAsPlanned", "Last24Hours"}
            For Each col As DataColumn In dtGrid.Columns
                If Not colsToLeave.Contains(col.ColumnName) Then
                    colsToColor.Add(col.ColumnName)
                End If
            Next

            If tglbtnHistory.ToggleState = CheckState.Checked Then
                ShowHideDateColumns(True)
            Else
                ShowHideDateColumns(False)
            End If

            Dim colsToMap() As String = {"TECHNOLOGY", "IOS_TECH", "LAYER", "SITENAME", "IOS_CELL_GID", "SiteX", "SiteY", "S_CELLNAME", "DistanceToSite", "CoverageAsPlanned"}
            'Get data for map lines
            colsToLeave.ToList().Remove("Last24Hours")
            dtMapData = dtGrid.AsDataView.ToTable(True, colsToMap)
            frmMapWindow.ServiceCheckCreateMapInfo(dtMapData, posLng, posLat)
        Else
            vGrid2GSumm.Rows.Clear()
            vGrid2GSumm.DataSource = Nothing
            vGrid3GSumm.Rows.Clear()
            vGrid3GSumm.DataSource = Nothing
            vGrid4GSumm.Rows.Clear()
            vGrid4GSumm.DataSource = Nothing
            vGrid5GSumm.Rows.Clear()
            vGrid5GSumm.DataSource = Nothing

            If sig2G IsNot Nothing Then
                state2G.Gauges.Remove(sig2G)
            End If
            If sig3G IsNot Nothing Then
                state3G.Gauges.Remove(sig3G)
            End If
            If sig4G IsNot Nothing Then
                state4G.Gauges.Remove(sig4G)
            End If
            If sig5G IsNot Nothing Then
                state5G.Gauges.Remove(sig5G)
            End If

            IOSDevExpressGrid.ClearGrid(gcDetails)
        End If
    End Sub

    Private Sub LoadTechSummaryData(ByRef dt As DataTable, ByRef vgSumm As VGridControl, ByVal tech As String)
        Try
            Dim dvTech As DataView = New DataView(dt.Select("TECHNOLOGY='" & tech.Trim & "'").CopyToDataTable())
            Dim cols(2) As String
            cols(0) = "KPIName"
            cols(1) = "Last24Hours"
            cols(2) = "CoverageAsPlanned"
            Dim dtTech As DataTable = dvTech.ToTable(True, cols)

            Dim dtTechVGrid As New DataTable
            For i = 0 To dtTech.Rows.Count - 1
                dtTechVGrid.Columns.Add(dtTech.Rows(i)("KPIName"), GetType(String))
            Next

            Dim dr As DataRow = dtTechVGrid.NewRow()
            For i = 0 To dtTechVGrid.Columns.Count - 1
                dr(dtTech.Rows(i)("KPIName")) = dtTech.Rows(i)("Last24Hours")
            Next
            dtTechVGrid.Rows.Add(dr)

            dtTechVGrid.Columns.Add("CoverageAsPlanned", GetType(String))
            For Each dr2 As DataRow In dtTechVGrid.Rows
                dr2("CoverageAsPlanned") = dtTech.Rows(0)("CoverageAsPlanned")
                dtTechVGrid.AcceptChanges()
            Next

            vgSumm.Rows.Clear()
            vgSumm.DataSource = dtTechVGrid
            vgSumm.LayoutStyle = LayoutViewStyle.SingleRecordView

            'Tech indicator color coding
            Dim lstClrRank As New List(Of Integer)
            Dim dtTechClr As DataTable = dtSettings.Select("TECHNOLOGY='" & tech.Trim & "'").CopyToDataTable
            For i = 0 To dtTechVGrid.Columns.Count - 1
                Dim dtKPI As DataTable = dtTechClr.Select("KPIName='" & dtTechVGrid.Columns(i).ColumnName & "'").CopyToDataTable
                For Each drKPI As DataRow In dtKPI.Rows
                    If CDbl(dtTechVGrid.Rows(0)(dtTechVGrid.Columns(i).ColumnName)) >= CDbl(drKPI("RangeFrom")) And CDbl(dtTechVGrid.Rows(0)(dtTechVGrid.Columns(i).ColumnName)) < CDbl(drKPI("RangeTo")) Then
                        lstClrRank.Add(CInt(drKPI("Rank")))
                        Exit For
                    End If
                Next
            Next

            ManagTechSummaryIndicator(tech, lstClrRank.Max())

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Sub LoadNetworkServiceDetails()
        Try
            Dim latLng() As String = Nothing
            Dim inputIsCoordinate As Boolean = False

            If regionalSettings = True Then
                Dim temp = txtGeoCoderSearch.Text.Replace(".", "#")
                temp = temp.Replace(",", ".")
                txtGeoCoderSearch.Text = temp.Replace("#", ",")

                latLng = txtGeoCoderSearch.Text.Trim().Split({"."}, StringSplitOptions.RemoveEmptyEntries)
            Else
                If txtGeoCoderSearch.Text.Trim.IndexOf(",") <> -1 Then
                    latLng = txtGeoCoderSearch.Text.Trim().Split({","}, StringSplitOptions.RemoveEmptyEntries)
                ElseIf txtGeoCoderSearch.Text.Trim.IndexOf(";") <> -1 Then
                    latLng = txtGeoCoderSearch.Text.Trim().Split({";"}, StringSplitOptions.RemoveEmptyEntries)
                Else
                    latLng = txtGeoCoderSearch.Text.Trim().Split({" "}, StringSplitOptions.RemoveEmptyEntries)
                End If
            End If

            If latLng IsNot Nothing Then
                If latLng.Length = 2 Then
                    If (IsNumeric(latLng(0).Trim()) AndAlso IsNumeric(latLng(1).Trim())) Then
                        posLat = IIf(regionalSettings = True, CDbl(latLng(0).Replace(",", ".").Trim), CDbl(latLng(0).Trim))
                        posLng = IIf(regionalSettings = True, CDbl(latLng(1).Replace(",", ".").Trim), CDbl(latLng(1).Trim))
                        inputIsCoordinate = True
                    End If
                End If
            End If

            If (inputIsCoordinate = True) Then
                frmMapWindow.Location_Map(txtGeoCoderSearch.Text.Trim(), posLng, posLat, "ServiceCheck_Location", False)
            Else
                frmTileMapping.TileMapping_GeoCoder(txtGeoCoderSearch.Text.Trim)
            End If

            'get network service data
            GetNetworkServiceData()
            'change the state of the toggle button

            'If tglbtnGetMapCoords.ToggleState = CheckState.Checked Then
            '    tglbtnGetMapCoords.ChangeToggleState()
            '    getMapCoords = False
            'End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

End Class