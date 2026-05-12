Imports IOS.DataLibrary

Public Class frmEvalRptChangeKPI

    Public Function LoadChangeKpiGridData(ByRef dtGrid As DataTable) As DevExpress.XtraGrid.GridControl
        Try
            GridControl1.DataSource = Nothing
            BandedGridView1.Columns.Clear()
            BandedGridView1.OptionsBehavior.AutoPopulateColumns = True
            GridControl1.DataSource = dtGrid

            BandedGridView1.Bands.Clear()

            Dim gcBandPrdComp As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandPrdComp.Caption = "Period Comparison"
            gcBandPrdComp.AppearanceHeader.Options.UseTextOptions = True
            gcBandPrdComp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandPrdComp.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandPrdCalc As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandPrdCalc.Caption = "Period Calculation"
            gcBandPrdCalc.AppearanceHeader.Options.UseTextOptions = True
            gcBandPrdCalc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandPrdCalc.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandAvg As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandAvg.Caption = "AVG"
            gcBandAvg.AppearanceHeader.Options.UseTextOptions = True
            gcBandAvg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandAvg.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandP10 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandP10.Caption = "P10"
            gcBandP10.AppearanceHeader.Options.UseTextOptions = True
            gcBandP10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandP10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandP90 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandP90.Caption = "P90"
            gcBandP90.AppearanceHeader.Options.UseTextOptions = True
            gcBandP90.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandP90.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In BandedGridView1.Columns
                If col.AbsoluteIndex > -1 AndAlso col.AbsoluteIndex <= 1 Then
                    gcBandPrdComp.Columns.Add(col)
                ElseIf col.AbsoluteIndex = 2 Then
                    gcBandPrdCalc.Columns.Add(col)
                ElseIf col.AbsoluteIndex > 2 AndAlso col.AbsoluteIndex <= 6 Then
                    gcBandAvg.Columns.Add(col)
                ElseIf col.AbsoluteIndex > 6 AndAlso col.AbsoluteIndex <= 10 Then
                    gcBandP10.Columns.Add(col)
                ElseIf col.AbsoluteIndex > 10 AndAlso col.AbsoluteIndex <= 14 Then
                    gcBandP90.Columns.Add(col)
                End If
                col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            Next
            If BandedGridView1.Columns.Count > 0 Then
                BandedGridView1.Columns(0).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            End If
            BandedGridView1.OptionsView.ColumnAutoWidth = True
            BandedGridView1.Bands.Add(gcBandPrdComp)
            BandedGridView1.Bands.Add(gcBandPrdCalc)
            BandedGridView1.Bands.Add(gcBandAvg)
            BandedGridView1.Bands.Add(gcBandP10)
            BandedGridView1.Bands.Add(gcBandP90)

            Return GridControl1
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function LoadSingleKpiGridData(ByRef dtGrid As DataTable) As DevExpress.XtraGrid.GridControl
        Try
            GridControl1.DataSource = Nothing
            BandedGridView1.Columns.Clear()
            BandedGridView1.OptionsBehavior.AutoPopulateColumns = True
            GridControl1.DataSource = dtGrid

            BandedGridView1.Bands.Clear()

            'Dim gcBandPrdComp As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            'gcBandPrdComp.Caption = "Period Comparison"
            'gcBandPrdComp.AppearanceHeader.Options.UseTextOptions = True
            'gcBandPrdComp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            'gcBandPrdComp.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            'Dim gcBandPrdCalc As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            'gcBandPrdCalc.Caption = "Period Calculation"
            'gcBandPrdCalc.AppearanceHeader.Options.UseTextOptions = True
            'gcBandPrdCalc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            'gcBandPrdCalc.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandAvg As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandAvg.Caption = "AVG"
            gcBandAvg.AppearanceHeader.Options.UseTextOptions = True
            gcBandAvg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandAvg.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandP10 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandP10.Caption = "P10"
            gcBandP10.AppearanceHeader.Options.UseTextOptions = True
            gcBandP10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandP10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim gcBandP90 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            gcBandP90.Caption = "P90"
            gcBandP90.AppearanceHeader.Options.UseTextOptions = True
            gcBandP90.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gcBandP90.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In BandedGridView1.Columns
                If col.AbsoluteIndex > -1 AndAlso col.AbsoluteIndex <= 1 Then
                    'gcBandPrdComp.Columns.Add(col)
                ElseIf col.AbsoluteIndex = 2 Then
                    'gcBandPrdCalc.Columns.Add(col)
                ElseIf col.AbsoluteIndex > 2 AndAlso col.AbsoluteIndex <= 6 Then
                    gcBandAvg.Columns.Add(col)
                ElseIf col.AbsoluteIndex > 6 AndAlso col.AbsoluteIndex <= 10 Then
                    gcBandP10.Columns.Add(col)
                ElseIf col.AbsoluteIndex > 10 AndAlso col.AbsoluteIndex <= 14 Then
                    gcBandP90.Columns.Add(col)
                End If
                col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            Next
            If BandedGridView1.Columns.Count > 0 Then
                BandedGridView1.Columns(0).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            End If
            BandedGridView1.OptionsView.ColumnAutoWidth = True
            'BandedGridView1.Bands.Add(gcBandPrdComp)
            'BandedGridView1.Bands.Add(gcBandPrdCalc)
            BandedGridView1.Bands.Add(gcBandAvg)
            BandedGridView1.Bands.Add(gcBandP10)
            BandedGridView1.Bands.Add(gcBandP90)

            Return GridControl1
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function LoadSingleKpiTopXGridData(ByRef dtGrid As DataTable) As DevExpress.XtraGrid.GridControl
        Try
            'GridControl2.DataSource = Nothing
            'GridControl2.MainView = GridView2
            'GridView2.Columns.Clear()
            'GridView2.OptionsBehavior.AutoPopulateColumns = True
            'GridControl2.DataSource = dtGrid
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(GridControl2, GridView2, dtGrid, "ALL")
            Return GridControl2
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function SetupBandedVerticalGrid(ByRef dtGrid As DataTable) As DevExpress.XtraVerticalGrid.VGridControl
        VGridControl1.DataSource = dtGrid

        ' IMPORTANT: Enable BandsView to show record columns grouped by bands
        VGridControl1.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.BandsView

        ' Optional: Auto-size record columns to fit the grid width
        VGridControl1.OptionsView.AutoScaleBands = True

        ' Create Categories (Bands)
        Dim catPrdCalc As New DevExpress.XtraVerticalGrid.Rows.CategoryRow("Period Calculation")
        Dim catAvg As New DevExpress.XtraVerticalGrid.Rows.CategoryRow("AVG")
        Dim catP10 As New DevExpress.XtraVerticalGrid.Rows.CategoryRow("P10")
        Dim catP90 As New DevExpress.XtraVerticalGrid.Rows.CategoryRow("P90")

        ' Create Data Rows
        Dim rowName As New DevExpress.XtraVerticalGrid.Rows.EditorRow("KPIName")
        rowName.Properties.Caption = "KPI"

        Dim rowAvgBefore As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_Before")
        rowAvgBefore.Properties.Caption = "Before"

        Dim rowAvgAfter As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_After")
        rowAvgAfter.Properties.Caption = "After"

        Dim rowAvgDelta As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_Delta")
        rowAvgDelta.Properties.Caption = "Delta"

        Dim rowAvgPercDelta As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_%Delta")
        rowAvgPercDelta.Properties.Caption = "%Delta"

        Dim rowP10Before As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_Before")
        rowP10Before.Properties.Caption = "Before"

        Dim rowP10After As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_After")
        rowP10After.Properties.Caption = "After"

        Dim rowP10Delta As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_Delta")
        rowP10Delta.Properties.Caption = "Delta"

        Dim rowP10PercDelta As New DevExpress.XtraVerticalGrid.Rows.EditorRow("AVG_%Delta")
        rowP10PercDelta.Properties.Caption = "%Delta"

        Dim rowP90Before As New DevExpress.XtraVerticalGrid.Rows.EditorRow("P90_Before")
        rowP90Before.Properties.Caption = "Before"

        Dim rowP90After As New DevExpress.XtraVerticalGrid.Rows.EditorRow("P90_After")
        rowP90After.Properties.Caption = "After"

        Dim rowP90Delta As New DevExpress.XtraVerticalGrid.Rows.EditorRow("P90_Delta")
        rowP90Delta.Properties.Caption = "Delta"

        Dim rowP90PercDelta As New DevExpress.XtraVerticalGrid.Rows.EditorRow("P90_%Delta")
        rowP90PercDelta.Properties.Caption = "%Delta"

        ' Clear auto-generated rows and build hierarchy
        VGridControl1.Rows.Clear()
        VGridControl1.Rows.AddRange(New DevExpress.XtraVerticalGrid.Rows.BaseRow() {catPrdCalc, catAvg, catP10, catP90})

        ' Nest the fields under their respective category/band
        catPrdCalc.ChildRows.Add(rowName)

        catAvg.ChildRows.Add(rowAvgBefore)
        catAvg.ChildRows.Add(rowAvgAfter)
        catAvg.ChildRows.Add(rowAvgDelta)
        catAvg.ChildRows.Add(rowAvgPercDelta)

        catP10.ChildRows.Add(rowP10Before)
        catP10.ChildRows.Add(rowP10After)
        catP10.ChildRows.Add(rowP10Delta)
        catP10.ChildRows.Add(rowP10PercDelta)

        catP90.ChildRows.Add(rowP90Before)
        catP90.ChildRows.Add(rowP90After)
        catP90.ChildRows.Add(rowP90Delta)
        catP90.ChildRows.Add(rowP90PercDelta)

        Return VGridControl1
    End Function

End Class