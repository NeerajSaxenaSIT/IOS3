<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTerrainProfile
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTerrainProfile))
        Me.ch_TerrainProfile = New dotnetCHARTING.WinForms.Chart()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpTrackbar = New System.Windows.Forms.TableLayoutPanel()
        Me.tbcETiltSlider = New DevExpress.XtraEditors.TrackBarControl()
        Me.lbl_EtiltPlanned = New DevExpress.XtraEditors.LabelControl()
        Me.lblTilt = New DevExpress.XtraEditors.LabelControl()
        Me.txtTilt = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpResolution = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbResolution = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tlpAzimuth = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Azimuth = New DevExpress.XtraEditors.LabelControl()
        Me.tbcAzimuth = New DevExpress.XtraEditors.TrackBarControl()
        Me.lblAzimuth = New DevExpress.XtraEditors.LabelControl()
        Me.txtAzimuth = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ch_TerrainProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMain.SuspendLayout()
        Me.tlpTrackbar.SuspendLayout()
        CType(Me.tbcETiltSlider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbcETiltSlider.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTilt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.tlpResolution.SuspendLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpAzimuth.SuspendLayout()
        CType(Me.tbcAzimuth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbcAzimuth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAzimuth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ch_TerrainProfile
        '
        Me.ch_TerrainProfile.Background.Color = System.Drawing.Color.White
        Annotation1.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        Annotation1.DynamicSize = True
        BoxHeaderOptions1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        BoxHeaderOptions1.Label.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
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
        Annotation1.Padding = 4
        Annotation1.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Annotation1.Shadow.Depth = 1
        Annotation1.Shadow.ExpandBy = 2.0!
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(489, 231)
        Annotation1.Visible = True
        Me.ch_TerrainProfile.Box = Annotation1
        Me.ch_TerrainProfile.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
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
        Me.ch_TerrainProfile.ChartArea.DefaultElement = Element1
        Me.ch_TerrainProfile.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.ch_TerrainProfile.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ch_TerrainProfile.ChartArea.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.ch_TerrainProfile.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.ch_TerrainProfile.ChartArea.LegendBox.DefaultEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TerrainProfile.ChartArea.LegendBox.DefaultEntry.LabelStyle.Font = New System.Drawing.Font("Trebuchet MS", 8.0!)
        Me.ch_TerrainProfile.ChartArea.LegendBox.DefaultEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.LegendBox.DefaultEntry.LabelStyle.Width = -2147483648
        BoxHeaderOptions2.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions2.Label.Width = -2147483648
        BoxHeaderOptions2.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions2.Shadow.Color = System.Drawing.Color.Transparent
        Me.ch_TerrainProfile.ChartArea.LegendBox.Header = BoxHeaderOptions2
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.LabelStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.LabelStyle.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.ch_TerrainProfile.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.ch_TerrainProfile.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.ch_TerrainProfile.ChartArea.LegendBox.Padding = 4
        Me.ch_TerrainProfile.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.ch_TerrainProfile.ChartArea.LegendBox.Shadow.ExpandBy = 2.0!
        Me.ch_TerrainProfile.ChartArea.LegendBox.Visible = True
        Me.ch_TerrainProfile.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.ch_TerrainProfile.ChartArea.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.Shadow.Depth = 1
        Me.ch_TerrainProfile.ChartArea.Shadow.ExpandBy = 2.0!
        Me.ch_TerrainProfile.ChartArea.Shadow.Visible = False
        Me.ch_TerrainProfile.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.ch_TerrainProfile.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        BoxHeaderOptions3.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions3.Label.Width = -2147483648
        BoxHeaderOptions3.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions3.Shadow.Color = System.Drawing.Color.Transparent
        Me.ch_TerrainProfile.ChartArea.TitleBox.Header = BoxHeaderOptions3
        Me.ch_TerrainProfile.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ch_TerrainProfile.ChartArea.TitleBox.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.TitleBox.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.ch_TerrainProfile.ChartArea.TitleBox.Shadow.ExpandBy = 2.0!
        Me.ch_TerrainProfile.ChartArea.TitleBox.Visible = True
        Me.ch_TerrainProfile.ChartArea.XAxis.Crosshair = Nothing
        Me.ch_TerrainProfile.ChartArea.XAxis.DefaultTick.AxisID = ""
        Me.ch_TerrainProfile.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.XAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TerrainProfile.ChartArea.XAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.XAxis.DefaultTick.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.ch_TerrainProfile.ChartArea.XAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.XAxis.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TerrainProfile.ChartArea.XAxis.MinorTimeIntervalAdvanced.Unit = dotnetCHARTING.WinForms.TimeInterval.None
        Me.ch_TerrainProfile.ChartArea.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.ch_TerrainProfile.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TerrainProfile.ChartArea.XAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.ch_TerrainProfile.ChartArea.XAxis.ZeroTick.AxisID = ""
        Me.ch_TerrainProfile.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.ch_TerrainProfile.ChartArea.XAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TerrainProfile.ChartArea.XAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.XAxis.ZeroTick.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.ch_TerrainProfile.ChartArea.YAxis.Crosshair = Nothing
        Me.ch_TerrainProfile.ChartArea.YAxis.DefaultTick.AxisID = ""
        Me.ch_TerrainProfile.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.ch_TerrainProfile.ChartArea.YAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TerrainProfile.ChartArea.YAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.YAxis.DefaultTick.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.ch_TerrainProfile.ChartArea.YAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.YAxis.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.YAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.ch_TerrainProfile.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TerrainProfile.ChartArea.YAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.ch_TerrainProfile.ChartArea.YAxis.ZeroTick.AxisID = ""
        Me.ch_TerrainProfile.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.ch_TerrainProfile.ChartArea.YAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.ch_TerrainProfile.ChartArea.YAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.ChartArea.YAxis.ZeroTick.Label.Width = -2147483648
        Me.ch_TerrainProfile.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.ch_TerrainProfile.DataGrid = Nothing
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
        Me.ch_TerrainProfile.DefaultElement = Element2
        Me.ch_TerrainProfile.DefaultShadow.ExpandBy = 2.0!
        Me.ch_TerrainProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ch_TerrainProfile.LegacyMode = False
        Me.ch_TerrainProfile.Location = New System.Drawing.Point(3, 31)
        Me.ch_TerrainProfile.Name = "ch_TerrainProfile"
        Me.ch_TerrainProfile.NoDataLabel.Offset = New System.Drawing.Point(0, 0)
        Me.ch_TerrainProfile.NoDataLabel.Text = "No Data"
        Me.ch_TerrainProfile.NoDataLabel.Width = -2147483648
        Me.ch_TerrainProfile.Size = New System.Drawing.Size(490, 232)
        Me.ch_TerrainProfile.StartDateOfYear = New Date(CType(0, Long))
        Me.ch_TerrainProfile.TabIndex = 5
        Me.ch_TerrainProfile.TempDirectory = "C:\Users\Guy\AppData\Local\Temp\"
        Me.ch_TerrainProfile.View3D = View3D1
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 3
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpMain.Controls.Add(Me.tlpTrackbar, 1, 0)
        Me.tlpMain.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpAzimuth, 2, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 1
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(578, 268)
        Me.tlpMain.TabIndex = 6
        '
        'tlpTrackbar
        '
        Me.tlpTrackbar.ColumnCount = 1
        Me.tlpTrackbar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTrackbar.Controls.Add(Me.tbcETiltSlider, 0, 2)
        Me.tlpTrackbar.Controls.Add(Me.lbl_EtiltPlanned, 0, 3)
        Me.tlpTrackbar.Controls.Add(Me.lblTilt, 0, 0)
        Me.tlpTrackbar.Controls.Add(Me.txtTilt, 0, 1)
        Me.tlpTrackbar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTrackbar.Location = New System.Drawing.Point(498, 0)
        Me.tlpTrackbar.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpTrackbar.Name = "tlpTrackbar"
        Me.tlpTrackbar.RowCount = 4
        Me.tlpTrackbar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpTrackbar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpTrackbar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTrackbar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpTrackbar.Size = New System.Drawing.Size(40, 268)
        Me.tlpTrackbar.TabIndex = 8
        '
        'tbcETiltSlider
        '
        Me.tbcETiltSlider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcETiltSlider.EditValue = Nothing
        Me.tbcETiltSlider.Location = New System.Drawing.Point(3, 48)
        Me.tbcETiltSlider.Name = "tbcETiltSlider"
        Me.tbcETiltSlider.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbcETiltSlider.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbcETiltSlider.Properties.LargeChange = 1
        Me.tbcETiltSlider.Properties.Maximum = 150
        Me.tbcETiltSlider.Properties.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbcETiltSlider.Size = New System.Drawing.Size(34, 187)
        Me.tbcETiltSlider.TabIndex = 7
        '
        'lbl_EtiltPlanned
        '
        Me.lbl_EtiltPlanned.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbl_EtiltPlanned.Appearance.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_EtiltPlanned.Appearance.Options.UseFont = True
        Me.lbl_EtiltPlanned.Appearance.Options.UseForeColor = True
        Me.lbl_EtiltPlanned.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_EtiltPlanned.Location = New System.Drawing.Point(2, 240)
        Me.lbl_EtiltPlanned.Margin = New System.Windows.Forms.Padding(2)
        Me.lbl_EtiltPlanned.Name = "lbl_EtiltPlanned"
        Me.lbl_EtiltPlanned.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lbl_EtiltPlanned.Size = New System.Drawing.Size(36, 26)
        Me.lbl_EtiltPlanned.TabIndex = 8
        '
        'lblTilt
        '
        Me.lblTilt.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTilt.Appearance.ForeColor = System.Drawing.Color.DarkRed
        Me.lblTilt.Appearance.Options.UseFont = True
        Me.lblTilt.Appearance.Options.UseForeColor = True
        Me.lblTilt.Appearance.Options.UseTextOptions = True
        Me.lblTilt.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTilt.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblTilt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTilt.Location = New System.Drawing.Point(3, 3)
        Me.lblTilt.Name = "lblTilt"
        Me.lblTilt.Size = New System.Drawing.Size(34, 14)
        Me.lblTilt.TabIndex = 9
        Me.lblTilt.Text = "Tilt"
        Me.lblTilt.ToolTip = "Tilt"
        '
        'txtTilt
        '
        Me.txtTilt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTilt.Location = New System.Drawing.Point(2, 22)
        Me.txtTilt.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTilt.Name = "txtTilt"
        Me.txtTilt.Size = New System.Drawing.Size(36, 20)
        Me.txtTilt.TabIndex = 10
        Me.txtTilt.ToolTip = "Enter a value between 0 and 150 and press enter"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.tlpResolution, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ch_TerrainProfile, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(496, 266)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'tlpResolution
        '
        Me.tlpResolution.ColumnCount = 3
        Me.tlpResolution.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.tlpResolution.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.tlpResolution.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpResolution.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpResolution.Controls.Add(Me.cmbResolution, 1, 0)
        Me.tlpResolution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpResolution.Location = New System.Drawing.Point(1, 1)
        Me.tlpResolution.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpResolution.Name = "tlpResolution"
        Me.tlpResolution.RowCount = 1
        Me.tlpResolution.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpResolution.Size = New System.Drawing.Size(494, 26)
        Me.tlpResolution.TabIndex = 14
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(84, 20)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Set Resolution"
        '
        'cmbResolution
        '
        Me.cmbResolution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResolution.EditValue = "Low"
        Me.cmbResolution.Location = New System.Drawing.Point(93, 3)
        Me.cmbResolution.Name = "cmbResolution"
        Me.cmbResolution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResolution.Properties.Items.AddRange(New Object() {"Low", "Medium", "High"})
        Me.cmbResolution.Size = New System.Drawing.Size(194, 20)
        Me.cmbResolution.TabIndex = 1
        '
        'tlpAzimuth
        '
        Me.tlpAzimuth.ColumnCount = 1
        Me.tlpAzimuth.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAzimuth.Controls.Add(Me.lbl_Azimuth, 0, 3)
        Me.tlpAzimuth.Controls.Add(Me.tbcAzimuth, 0, 2)
        Me.tlpAzimuth.Controls.Add(Me.lblAzimuth, 0, 0)
        Me.tlpAzimuth.Controls.Add(Me.txtAzimuth, 0, 1)
        Me.tlpAzimuth.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAzimuth.Location = New System.Drawing.Point(538, 0)
        Me.tlpAzimuth.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpAzimuth.Name = "tlpAzimuth"
        Me.tlpAzimuth.RowCount = 4
        Me.tlpAzimuth.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpAzimuth.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpAzimuth.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAzimuth.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpAzimuth.Size = New System.Drawing.Size(40, 268)
        Me.tlpAzimuth.TabIndex = 9
        '
        'lbl_Azimuth
        '
        Me.lbl_Azimuth.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbl_Azimuth.Appearance.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Azimuth.Appearance.Options.UseFont = True
        Me.lbl_Azimuth.Appearance.Options.UseForeColor = True
        Me.lbl_Azimuth.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Azimuth.Location = New System.Drawing.Point(2, 240)
        Me.lbl_Azimuth.Margin = New System.Windows.Forms.Padding(2)
        Me.lbl_Azimuth.Name = "lbl_Azimuth"
        Me.lbl_Azimuth.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lbl_Azimuth.Size = New System.Drawing.Size(36, 26)
        Me.lbl_Azimuth.TabIndex = 9
        '
        'tbcAzimuth
        '
        Me.tbcAzimuth.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcAzimuth.EditValue = Nothing
        Me.tbcAzimuth.Location = New System.Drawing.Point(3, 48)
        Me.tbcAzimuth.Name = "tbcAzimuth"
        Me.tbcAzimuth.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbcAzimuth.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbcAzimuth.Properties.LargeChange = 1
        Me.tbcAzimuth.Properties.Maximum = 359
        Me.tbcAzimuth.Properties.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbcAzimuth.Size = New System.Drawing.Size(34, 187)
        Me.tbcAzimuth.TabIndex = 8
        '
        'lblAzimuth
        '
        Me.lblAzimuth.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAzimuth.Appearance.ForeColor = System.Drawing.Color.DarkRed
        Me.lblAzimuth.Appearance.Options.UseFont = True
        Me.lblAzimuth.Appearance.Options.UseForeColor = True
        Me.lblAzimuth.Appearance.Options.UseTextOptions = True
        Me.lblAzimuth.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblAzimuth.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblAzimuth.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAzimuth.Location = New System.Drawing.Point(3, 3)
        Me.lblAzimuth.Name = "lblAzimuth"
        Me.lblAzimuth.Size = New System.Drawing.Size(34, 14)
        Me.lblAzimuth.TabIndex = 10
        Me.lblAzimuth.Text = "Azim"
        Me.lblAzimuth.ToolTip = "Azimuth"
        '
        'txtAzimuth
        '
        Me.txtAzimuth.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAzimuth.Location = New System.Drawing.Point(2, 22)
        Me.txtAzimuth.Margin = New System.Windows.Forms.Padding(2)
        Me.txtAzimuth.Name = "txtAzimuth"
        Me.txtAzimuth.Size = New System.Drawing.Size(36, 20)
        Me.txtAzimuth.TabIndex = 11
        Me.txtAzimuth.ToolTip = "Enter a value between 0 and 359 and press enter"
        '
        'frmTerrainProfile
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(578, 268)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmTerrainProfile.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(500, 200)
        Me.Name = "frmTerrainProfile"
        Me.Opacity = 0.65R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Terrain Profile"
        CType(Me.ch_TerrainProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMain.ResumeLayout(False)
        Me.tlpTrackbar.ResumeLayout(False)
        Me.tlpTrackbar.PerformLayout()
        CType(Me.tbcETiltSlider.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbcETiltSlider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTilt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.tlpResolution.ResumeLayout(False)
        Me.tlpResolution.PerformLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpAzimuth.ResumeLayout(False)
        Me.tlpAzimuth.PerformLayout()
        CType(Me.tbcAzimuth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbcAzimuth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAzimuth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ch_TerrainProfile As dotnetCHARTING.WinForms.Chart
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents tlpTrackbar As TableLayoutPanel
    Friend WithEvents tbcETiltSlider As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents lbl_EtiltPlanned As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpResolution As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbResolution As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpAzimuth As TableLayoutPanel
    Friend WithEvents lbl_Azimuth As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbcAzimuth As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents lblTilt As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAzimuth As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTilt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAzimuth As DevExpress.XtraEditors.TextEdit
End Class
