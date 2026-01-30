<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportChartGrid
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Annotation1 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim BoxHeaderOptions1 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim Element1 As dotnetCHARTING.WinForms.Element = New dotnetCHARTING.WinForms.Element()
        Dim Line1 As dotnetCHARTING.WinForms.Line = New dotnetCHARTING.WinForms.Line()
        Dim BoxHeaderOptions2 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim BoxHeaderOptions3 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim Element2 As dotnetCHARTING.WinForms.Element = New dotnetCHARTING.WinForms.Element()
        Dim Line2 As dotnetCHARTING.WinForms.Line = New dotnetCHARTING.WinForms.Line()
        Dim View3D1 As dotnetCHARTING.WinForms.View3D = New dotnetCHARTING.WinForms.View3D()
        Me.splitC_ReportChartGrid = New System.Windows.Forms.SplitContainer()
        Me.chart_ReportChartGrid = New dotnetCHARTING.WinForms.Chart()
        Me.gcReportChartGrid = New DevExpress.XtraGrid.GridControl()
        Me.gvReportChartGrid = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.splitC_ReportChartGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitC_ReportChartGrid.Panel1.SuspendLayout()
        Me.splitC_ReportChartGrid.Panel2.SuspendLayout()
        Me.splitC_ReportChartGrid.SuspendLayout()
        CType(Me.chart_ReportChartGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcReportChartGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReportChartGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'splitC_ReportChartGrid
        '
        Me.splitC_ReportChartGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitC_ReportChartGrid.Location = New System.Drawing.Point(0, 0)
        Me.splitC_ReportChartGrid.Name = "splitC_ReportChartGrid"
        Me.splitC_ReportChartGrid.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitC_ReportChartGrid.Panel1
        '
        Me.splitC_ReportChartGrid.Panel1.Controls.Add(Me.chart_ReportChartGrid)
        '
        'splitC_ReportChartGrid.Panel2
        '
        Me.splitC_ReportChartGrid.Panel2.Controls.Add(Me.gcReportChartGrid)
        Me.splitC_ReportChartGrid.Panel2Collapsed = True
        Me.splitC_ReportChartGrid.Size = New System.Drawing.Size(756, 518)
        Me.splitC_ReportChartGrid.SplitterDistance = 250
        Me.splitC_ReportChartGrid.TabIndex = 3
        '
        'chart_ReportChartGrid
        '
        Me.chart_ReportChartGrid.AllowDrop = True
        Me.chart_ReportChartGrid.Application = "gQzI2MXojPIgHq0nSVxaGkDnjJ5mpGQhDVaFskyiEpJuan0E08iqQMF1Ct16hWyK"
        Me.chart_ReportChartGrid.ApplicationDNC = "gQzI2MXojPIgHq0nSVxaGkDnjJ5mpGQhDVaFskyiEpJuan0E08iqQMF1Ct16hWyK"
        Me.chart_ReportChartGrid.AutoSize = True
        Annotation1.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        Annotation1.DynamicSize = True
        BoxHeaderOptions1.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions1.Label.Width = -2147483648
        BoxHeaderOptions1.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions1.Shadow.Color = System.Drawing.Color.Transparent
        Annotation1.Header = BoxHeaderOptions1
        Annotation1.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation1.Label.Offset = New System.Drawing.Point(0, 0)
        Annotation1.Label.Width = -2147483648
        Annotation1.Line.Color = System.Drawing.Color.Gray
        Annotation1.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(755, 517)
        Annotation1.Visible = True
        Me.chart_ReportChartGrid.Box = Annotation1
        Me.chart_ReportChartGrid.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.chart_ReportChartGrid.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Element1.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        Element1.DefaultSubValue.Visible = True
        Element1.FocusGlow = Line1
        Element1.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element1.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Element1.LegendEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element1.LegendEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Element1.LegendEntry.LabelStyle.Width = -2147483648
        Element1.SmartLabel.Color = System.Drawing.Color.Empty
        Element1.SmartLabel.Offset = New System.Drawing.Point(0, 0)
        Element1.SmartLabel.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.DefaultElement = Element1
        Me.chart_ReportChartGrid.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.chart_ReportChartGrid.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.chart_ReportChartGrid.ChartArea.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.chart_ReportChartGrid.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.chart_ReportChartGrid.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chart_ReportChartGrid.ChartArea.LegendBox.DefaultEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chart_ReportChartGrid.ChartArea.LegendBox.DefaultEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.LegendBox.DefaultEntry.LabelStyle.Width = -2147483648
        BoxHeaderOptions2.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions2.Label.Width = -2147483648
        BoxHeaderOptions2.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions2.Shadow.Color = System.Drawing.Color.Transparent
        Me.chart_ReportChartGrid.ChartArea.LegendBox.Header = BoxHeaderOptions2
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.LabelStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.LabelStyle.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.chart_ReportChartGrid.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.chart_ReportChartGrid.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chart_ReportChartGrid.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.chart_ReportChartGrid.ChartArea.LegendBox.Padding = 4
        Me.chart_ReportChartGrid.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.chart_ReportChartGrid.ChartArea.LegendBox.Visible = True
        Me.chart_ReportChartGrid.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.chart_ReportChartGrid.ChartArea.Shadow.Visible = False
        Me.chart_ReportChartGrid.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        BoxHeaderOptions3.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions3.Label.Width = -2147483648
        BoxHeaderOptions3.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions3.Shadow.Color = System.Drawing.Color.Transparent
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Header = BoxHeaderOptions3
        Me.chart_ReportChartGrid.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.chart_ReportChartGrid.ChartArea.TitleBox.Visible = True
        Me.chart_ReportChartGrid.ChartArea.XAxis.Crosshair = Nothing
        Me.chart_ReportChartGrid.ChartArea.XAxis.DefaultTick.AxisID = ""
        Me.chart_ReportChartGrid.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.chart_ReportChartGrid.ChartArea.XAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chart_ReportChartGrid.ChartArea.XAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.XAxis.DefaultTick.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.chart_ReportChartGrid.ChartArea.XAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.XAxis.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chart_ReportChartGrid.ChartArea.XAxis.MinorTimeIntervalAdvanced.Unit = dotnetCHARTING.WinForms.TimeInterval.None
        Me.chart_ReportChartGrid.ChartArea.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.chart_ReportChartGrid.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chart_ReportChartGrid.ChartArea.XAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.chart_ReportChartGrid.ChartArea.XAxis.ZeroTick.AxisID = ""
        Me.chart_ReportChartGrid.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chart_ReportChartGrid.ChartArea.XAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chart_ReportChartGrid.ChartArea.XAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.XAxis.ZeroTick.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.chart_ReportChartGrid.ChartArea.YAxis.Crosshair = Nothing
        Me.chart_ReportChartGrid.ChartArea.YAxis.DefaultTick.AxisID = ""
        Me.chart_ReportChartGrid.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.chart_ReportChartGrid.ChartArea.YAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chart_ReportChartGrid.ChartArea.YAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.YAxis.DefaultTick.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.chart_ReportChartGrid.ChartArea.YAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.YAxis.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.YAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.chart_ReportChartGrid.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chart_ReportChartGrid.ChartArea.YAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.chart_ReportChartGrid.ChartArea.YAxis.ZeroTick.AxisID = ""
        Me.chart_ReportChartGrid.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chart_ReportChartGrid.ChartArea.YAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chart_ReportChartGrid.ChartArea.YAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.ChartArea.YAxis.ZeroTick.Label.Width = -2147483648
        Me.chart_ReportChartGrid.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.chart_ReportChartGrid.DataGrid = Nothing
        Element2.DefaultSubValue.Visible = True
        Element2.FocusGlow = Line2
        Element2.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element2.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Element2.LegendEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Element2.LegendEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Element2.LegendEntry.LabelStyle.Width = -2147483648
        Element2.SmartLabel.Color = System.Drawing.Color.Empty
        Element2.SmartLabel.Offset = New System.Drawing.Point(0, 0)
        Element2.SmartLabel.Width = -2147483648
        Me.chart_ReportChartGrid.DefaultElement = Element2
        Me.chart_ReportChartGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chart_ReportChartGrid.LegacyMode = False
        Me.chart_ReportChartGrid.Location = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.Name = "chart_ReportChartGrid"
        Me.chart_ReportChartGrid.NoDataLabel.Offset = New System.Drawing.Point(0, 0)
        Me.chart_ReportChartGrid.NoDataLabel.Width = -2147483648
        Me.chart_ReportChartGrid.Size = New System.Drawing.Size(756, 518)
        Me.chart_ReportChartGrid.StartDateOfYear = New Date(CType(0, Long))
        Me.chart_ReportChartGrid.TabIndex = 0
        Me.chart_ReportChartGrid.TempDirectory = "C:\Users\Neeraj Saxena\AppData\Local\Temp\"
        Me.chart_ReportChartGrid.View3D = View3D1
        '
        'gcReportChartGrid
        '
        Me.gcReportChartGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcReportChartGrid.Location = New System.Drawing.Point(0, 0)
        Me.gcReportChartGrid.MainView = Me.gvReportChartGrid
        Me.gcReportChartGrid.Name = "gcReportChartGrid"
        Me.gcReportChartGrid.Size = New System.Drawing.Size(150, 46)
        Me.gcReportChartGrid.TabIndex = 0
        Me.gcReportChartGrid.Tag = "GRID"
        Me.gcReportChartGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReportChartGrid})
        '
        'gvReportChartGrid
        '
        Me.gvReportChartGrid.GridControl = Me.gcReportChartGrid
        Me.gvReportChartGrid.Name = "gvReportChartGrid"
        Me.gvReportChartGrid.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportChartGrid.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportChartGrid.OptionsBehavior.Editable = False
        Me.gvReportChartGrid.OptionsBehavior.ReadOnly = True
        Me.gvReportChartGrid.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvReportChartGrid.OptionsView.ColumnAutoWidth = False
        Me.gvReportChartGrid.OptionsView.ShowGroupPanel = False
        '
        'ReportChartGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.DimGray
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.splitC_ReportChartGrid)
        Me.Name = "ReportChartGrid"
        Me.Size = New System.Drawing.Size(756, 518)
        Me.splitC_ReportChartGrid.Panel1.ResumeLayout(False)
        Me.splitC_ReportChartGrid.Panel1.PerformLayout()
        Me.splitC_ReportChartGrid.Panel2.ResumeLayout(False)
        CType(Me.splitC_ReportChartGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitC_ReportChartGrid.ResumeLayout(False)
        CType(Me.chart_ReportChartGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcReportChartGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReportChartGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents splitC_ReportChartGrid As System.Windows.Forms.SplitContainer
    Private WithEvents chart_ReportChartGrid As dotnetCHARTING.WinForms.Chart
    Public WithEvents gcReportChartGrid As DevExpress.XtraGrid.GridControl
    Public WithEvents gvReportChartGrid As DevExpress.XtraGrid.Views.Grid.GridView

End Class
