Imports IOS.DataLibrary
Imports DevExpress.XtraPrinting

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
            'IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(GridControl3, GridView3, dtGrid, "ALL")
            GridControl3.MainView = GridView3
            GridView3.Columns.Clear()

            GridControl3.ViewCollection.Add(GridView3)
            GridControl3.DataSource = dtGrid

            GridView3.OptionsView.ShowGroupPanel = False
            GridView3.OptionsView.ShowIndicator = False
            GridView3.OptionsView.ColumnAutoWidth = True

            GridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView3.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView3.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
            GridView3.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never

            GridView3.AutoFillColumn = GridView3.Columns("ObjectName")

            GridView3.Columns(0).Width = GridControl3.Width / 5
            GridView3.Columns(1).Width = GridControl3.Width / 5
            GridView3.Columns(2).Width = GridControl3.Width / 5
            GridView3.Columns(3).Width = GridControl3.Width / 5
            GridView3.Columns(4).Width = GridControl3.Width / 5

            GridControl3.ForceInitialize()
            Return GridControl3
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function BuildSingleKpiGridData(ByVal dtSrc As DataTable) As DevExpress.XtraGrid.GridControl
        Dim dt As New DataTable()

        dt.Columns.Add("KPI Changes")
        dt.Columns.Add("Before")
        dt.Columns.Add("After")
        dt.Columns.Add("Delta")
        dt.Columns.Add("Delta%")

        Dim dr As DataRow = dtSrc.Rows(0)
        dt.Rows.Add("AVG", dr("AVG_Before"), dr("AVG_After"), dr("AVG_Delta"), dr("AVG_%Delta"))
        dt.Rows.Add("P10", dr("P10_Before"), dr("P10_After"), dr("P10_Delta"), dr("P10_%Delta"))
        dt.Rows.Add("P90", dr("P90_Before"), dr("P90_After"), dr("P90_Delta"), dr("P90_%Delta"))

        GridControl2.MainView = GridView2
        GridView2.Columns.Clear()

        GridControl2.ViewCollection.Add(GridView2)
        GridControl2.DataSource = dt

        GridView2.OptionsView.ShowGroupPanel = False
        GridView2.OptionsView.ShowIndicator = False
        GridView2.OptionsView.ColumnAutoWidth = False

        GridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GridControl2.ForceInitialize()
        Return GridControl2
    End Function

End Class