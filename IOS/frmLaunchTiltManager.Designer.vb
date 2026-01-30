<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLaunchTiltManager
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim Annotation1 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim BoxHeaderOptions1 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim BoxHeaderOptions2 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim BoxHeaderOptions3 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim View3D1 As dotnetCHARTING.WinForms.View3D = New dotnetCHARTING.WinForms.View3D()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLaunchTiltManager))
        Me.sccMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ch_TiltManager = New dotnetCHARTING.WinForms.Chart()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcSectorList = New DevExpress.XtraGrid.GridControl()
        Me.cmsSectorList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_DeleteSector = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvSectorList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnGenerateTiltCampaign = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCalculateAndSave = New DevExpress.XtraEditors.SimpleButton()
        Me.tglPlanned = New IOS.Library.IOSToggleButton()
        Me.btnClearThematics = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbManualCampaign = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAddCampaign = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteCampaign = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbResolution = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnManageTree = New DevExpress.XtraEditors.SimpleButton()
        Me.txtETiltValue = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbcETiltSlider = New DevExpress.XtraEditors.TrackBarControl()
        Me.lbl_EtiltPlanned = New DevExpress.XtraEditors.LabelControl()
        Me.sccTiltTreeValidGrid = New DevExpress.XtraEditors.SplitContainerControl()
        Me.tlTiltManager = New DevExpress.XtraTreeList.TreeList()
        Me.Antennas = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn3 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.treeListBand1 = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.TreeListColumn4 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn5 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn6 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.treeListBand2 = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.TreeListColumn7 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn8 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn10 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.treeListBand3 = New DevExpress.XtraTreeList.Columns.TreeListBand()
        Me.tlcValidation = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.TreeListColumn11 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn12 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn13 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn14 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn15 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn9 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn16 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn17 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn18 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn19 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn20 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn21 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemImageEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageEdit()
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.gcCampaignValidation = New DevExpress.XtraGrid.GridControl()
        Me.gvCampaignValidation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TreeListColumn22 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel1.SuspendLayout()
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel2.SuspendLayout()
        Me.sccMain.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ch_TiltManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.gcSectorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsSectorList.SuspendLayout()
        CType(Me.gvSectorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.cmbManualCampaign.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtETiltValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.tbcETiltSlider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbcETiltSlider.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccTiltTreeValidGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccTiltTreeValidGrid.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTiltTreeValidGrid.Panel1.SuspendLayout()
        CType(Me.sccTiltTreeValidGrid.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTiltTreeValidGrid.Panel2.SuspendLayout()
        Me.sccTiltTreeValidGrid.SuspendLayout()
        CType(Me.tlTiltManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCampaignValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCampaignValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sccMain
        '
        Me.sccMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccMain.Horizontal = False
        Me.sccMain.Location = New System.Drawing.Point(0, 0)
        Me.sccMain.Name = "sccMain"
        '
        'sccMain.Panel1
        '
        Me.sccMain.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.sccMain.Panel1.MinSize = 200
        Me.sccMain.Panel1.Text = "Panel1"
        '
        'sccMain.Panel2
        '
        Me.sccMain.Panel2.Controls.Add(Me.sccTiltTreeValidGrid)
        Me.sccMain.Panel2.MinSize = 300
        Me.sccMain.Panel2.Text = "Panel2"
        Me.sccMain.Size = New System.Drawing.Size(1271, 877)
        Me.sccMain.SplitterPosition = 429
        Me.sccMain.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ch_TiltManager, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1271, 429)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ch_TiltManager
        '
        Me.ch_TiltManager.Background.Color = System.Drawing.Color.White
        Annotation1.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        Annotation1.DynamicSize = True
        BoxHeaderOptions1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        BoxHeaderOptions1.Label.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        BoxHeaderOptions1.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions1.Shadow.Color = System.Drawing.Color.Transparent
        Annotation1.Header = BoxHeaderOptions1
        Annotation1.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation1.Line.Color = System.Drawing.Color.Gray
        Annotation1.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation1.Padding = 4
        Annotation1.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Annotation1.Shadow.Depth = 1
        Annotation1.Shadow.ExpandBy = 2.0!
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(824, 422)
        Annotation1.Visible = True
        Me.ch_TiltManager.Box = Annotation1
        Me.ch_TiltManager.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ch_TiltManager.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Me.ch_TiltManager.ChartArea.DefaultElement.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.ch_TiltManager.ChartArea.DefaultElement.DefaultSubValue.Visible = True
        Me.ch_TiltManager.ChartArea.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.ch_TiltManager.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.ch_TiltManager.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ch_TiltManager.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.ch_TiltManager.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.ch_TiltManager.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        BoxHeaderOptions2.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions2.Shadow.Color = System.Drawing.Color.Transparent
        Me.ch_TiltManager.ChartArea.LegendBox.Header = BoxHeaderOptions2
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.ch_TiltManager.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.ch_TiltManager.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ch_TiltManager.ChartArea.LegendBox.LabelStyle.Font = New System.Drawing.Font("Trebuchet MS", 8.0!)
        Me.ch_TiltManager.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.LegendBox.Padding = 4
        Me.ch_TiltManager.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.ch_TiltManager.ChartArea.LegendBox.Shadow.ExpandBy = 2.0!
        Me.ch_TiltManager.ChartArea.LegendBox.Visible = True
        Me.ch_TiltManager.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ch_TiltManager.ChartArea.Shadow.Depth = 1
        Me.ch_TiltManager.ChartArea.Shadow.ExpandBy = 2.0!
        Me.ch_TiltManager.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        BoxHeaderOptions3.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions3.Shadow.Color = System.Drawing.Color.Transparent
        Me.ch_TiltManager.ChartArea.TitleBox.Header = BoxHeaderOptions3
        Me.ch_TiltManager.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ch_TiltManager.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ch_TiltManager.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ch_TiltManager.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.ch_TiltManager.ChartArea.TitleBox.Shadow.ExpandBy = 2.0!
        Me.ch_TiltManager.ChartArea.TitleBox.Visible = True
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ch_TiltManager.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.ch_TiltManager.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.XAxis.MinorTimeIntervalAdvanced.Unit = dotnetCHARTING.WinForms.TimeInterval.None
        Me.ch_TiltManager.ChartArea.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.ch_TiltManager.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.ch_TiltManager.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.ch_TiltManager.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.ch_TiltManager.ChartArea.YAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.ch_TiltManager.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.ch_TiltManager.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.ch_TiltManager.DataGrid = Nothing
        Me.ch_TiltManager.DefaultElement.DefaultSubValue.Visible = True
        Me.ch_TiltManager.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.ch_TiltManager.DefaultShadow.ExpandBy = 2.0!
        Me.ch_TiltManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ch_TiltManager.LegacyMode = False
        Me.ch_TiltManager.Location = New System.Drawing.Point(3, 3)
        Me.ch_TiltManager.Name = "ch_TiltManager"
        Me.ch_TiltManager.NoDataLabel.Text = "No Data"
        Me.ch_TiltManager.Size = New System.Drawing.Size(825, 423)
        Me.ch_TiltManager.StartDateOfYear = New Date(CType(0, Long))
        Me.ch_TiltManager.TabIndex = 6
        Me.ch_TiltManager.TempDirectory = "C:\Users\Guy\AppData\Local\Temp\"
        Me.ch_TiltManager.View3D = View3D1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.gcSectorList, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel6, 0, 3)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(874, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(394, 423)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'gcSectorList
        '
        Me.gcSectorList.ContextMenuStrip = Me.cmsSectorList
        Me.gcSectorList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcSectorList.Location = New System.Drawing.Point(3, 31)
        Me.gcSectorList.MainView = Me.gvSectorList
        Me.gcSectorList.Name = "gcSectorList"
        Me.gcSectorList.Size = New System.Drawing.Size(388, 322)
        Me.gcSectorList.TabIndex = 10
        Me.gcSectorList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvSectorList})
        '
        'cmsSectorList
        '
        Me.cmsSectorList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_DeleteSector})
        Me.cmsSectorList.Name = "cm_SON_Incon_dgvResult"
        Me.cmsSectorList.Size = New System.Drawing.Size(144, 26)
        '
        'tsmi_DeleteSector
        '
        Me.tsmi_DeleteSector.Name = "tsmi_DeleteSector"
        Me.tsmi_DeleteSector.Size = New System.Drawing.Size(143, 22)
        Me.tsmi_DeleteSector.Text = "Delete Sector"
        '
        'gvSectorList
        '
        Me.gvSectorList.GridControl = Me.gcSectorList
        Me.gvSectorList.Name = "gvSectorList"
        Me.gvSectorList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSectorList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSectorList.OptionsBehavior.Editable = False
        Me.gvSectorList.OptionsBehavior.ReadOnly = True
        Me.gvSectorList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSectorList.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSectorList.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSectorList.OptionsView.ColumnAutoWidth = False
        Me.gvSectorList.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnGenerateTiltCampaign, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnCalculateAndSave, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.tglPlanned, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnClearThematics, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1, 357)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(392, 33)
        Me.TableLayoutPanel3.TabIndex = 11
        '
        'btnGenerateTiltCampaign
        '
        Me.btnGenerateTiltCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGenerateTiltCampaign.Location = New System.Drawing.Point(215, 2)
        Me.btnGenerateTiltCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnGenerateTiltCampaign.Name = "btnGenerateTiltCampaign"
        Me.btnGenerateTiltCampaign.Size = New System.Drawing.Size(139, 29)
        Me.btnGenerateTiltCampaign.TabIndex = 2
        Me.btnGenerateTiltCampaign.Text = "Get MML Of Campaign"
        '
        'btnCalculateAndSave
        '
        Me.btnCalculateAndSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCalculateAndSave.Location = New System.Drawing.Point(72, 2)
        Me.btnCalculateAndSave.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCalculateAndSave.Name = "btnCalculateAndSave"
        Me.btnCalculateAndSave.Size = New System.Drawing.Size(139, 29)
        Me.btnCalculateAndSave.TabIndex = 3
        Me.btnCalculateAndSave.Text = "Calculate And Save"
        '
        'tglPlanned
        '
        Me.tglPlanned.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tglPlanned.Location = New System.Drawing.Point(2, 2)
        Me.tglPlanned.LookAndFeel.SkinName = "McSkin"
        Me.tglPlanned.LookAndFeel.UseDefaultLookAndFeel = False
        Me.tglPlanned.Margin = New System.Windows.Forms.Padding(2)
        Me.tglPlanned.Name = "tglPlanned"
        Me.tglPlanned.Size = New System.Drawing.Size(66, 29)
        Me.tglPlanned.TabIndex = 4
        Me.tglPlanned.Text = "Current"
        Me.tglPlanned.ToggleState = System.Windows.Forms.CheckState.Unchecked
        '
        'btnClearThematics
        '
        Me.btnClearThematics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClearThematics.ImageOptions.Image = Global.IOS.My.Resources.Resources.Clear_all_thematic
        Me.btnClearThematics.Location = New System.Drawing.Point(358, 2)
        Me.btnClearThematics.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClearThematics.Name = "btnClearThematics"
        Me.btnClearThematics.Size = New System.Drawing.Size(32, 29)
        Me.btnClearThematics.TabIndex = 5
        Me.btnClearThematics.ToolTip = "Clear thematics on the map window"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.cmbManualCampaign, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnAddCampaign, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnDeleteCampaign, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(392, 26)
        Me.TableLayoutPanel4.TabIndex = 12
        '
        'cmbManualCampaign
        '
        Me.cmbManualCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbManualCampaign.EditValue = "Select Campaign"
        Me.cmbManualCampaign.Location = New System.Drawing.Point(3, 3)
        Me.cmbManualCampaign.Name = "cmbManualCampaign"
        Me.cmbManualCampaign.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbManualCampaign.Properties.Sorted = True
        Me.cmbManualCampaign.Size = New System.Drawing.Size(266, 20)
        Me.cmbManualCampaign.TabIndex = 10
        Me.cmbManualCampaign.ToolTip = "Select Campaign"
        '
        'btnAddCampaign
        '
        Me.btnAddCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddCampaign.Location = New System.Drawing.Point(274, 2)
        Me.btnAddCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddCampaign.Name = "btnAddCampaign"
        Me.btnAddCampaign.Size = New System.Drawing.Size(56, 22)
        Me.btnAddCampaign.TabIndex = 11
        Me.btnAddCampaign.Text = "Add"
        '
        'btnDeleteCampaign
        '
        Me.btnDeleteCampaign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteCampaign.Location = New System.Drawing.Point(334, 2)
        Me.btnDeleteCampaign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteCampaign.Name = "btnDeleteCampaign"
        Me.btnDeleteCampaign.Size = New System.Drawing.Size(56, 22)
        Me.btnDeleteCampaign.TabIndex = 12
        Me.btnDeleteCampaign.Text = "Delete"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 4
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl1, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.cmbResolution, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btnManageTree, 3, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.txtETiltValue, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(1, 392)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(392, 30)
        Me.TableLayoutPanel6.TabIndex = 13
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(53, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(64, 24)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Resolution"
        '
        'cmbResolution
        '
        Me.cmbResolution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResolution.EditValue = "Low"
        Me.cmbResolution.Location = New System.Drawing.Point(123, 5)
        Me.cmbResolution.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbResolution.Name = "cmbResolution"
        Me.cmbResolution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResolution.Properties.Items.AddRange(New Object() {"Low", "Medium", "High"})
        Me.cmbResolution.Size = New System.Drawing.Size(166, 20)
        Me.cmbResolution.TabIndex = 1
        '
        'btnManageTree
        '
        Me.btnManageTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnManageTree.Location = New System.Drawing.Point(294, 2)
        Me.btnManageTree.Margin = New System.Windows.Forms.Padding(2)
        Me.btnManageTree.Name = "btnManageTree"
        Me.btnManageTree.Size = New System.Drawing.Size(96, 26)
        Me.btnManageTree.TabIndex = 4
        Me.btnManageTree.Text = "Expand Tree"
        '
        'txtETiltValue
        '
        Me.txtETiltValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtETiltValue.EditValue = ""
        Me.txtETiltValue.Location = New System.Drawing.Point(2, 5)
        Me.txtETiltValue.Margin = New System.Windows.Forms.Padding(2, 5, 2, 2)
        Me.txtETiltValue.Name = "txtETiltValue"
        Me.txtETiltValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtETiltValue.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtETiltValue.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtETiltValue.Properties.MaskSettings.Set("autoHideDecimalSeparator", True)
        Me.txtETiltValue.Properties.MaskSettings.Set("hideInsignificantZeros", True)
        Me.txtETiltValue.Properties.MaskSettings.Set("mask", "##.#")
        Me.txtETiltValue.Properties.MaxLength = 4
        Me.txtETiltValue.Size = New System.Drawing.Size(46, 20)
        Me.txtETiltValue.TabIndex = 5
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.tbcETiltSlider, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EtiltPlanned, 0, 1)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(831, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(40, 429)
        Me.TableLayoutPanel5.TabIndex = 7
        '
        'tbcETiltSlider
        '
        Me.tbcETiltSlider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcETiltSlider.EditValue = Nothing
        Me.tbcETiltSlider.Location = New System.Drawing.Point(4, 4)
        Me.tbcETiltSlider.Name = "tbcETiltSlider"
        Me.tbcETiltSlider.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbcETiltSlider.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbcETiltSlider.Properties.LargeChange = 1
        Me.tbcETiltSlider.Properties.Maximum = 150
        Me.tbcETiltSlider.Properties.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbcETiltSlider.Size = New System.Drawing.Size(32, 387)
        Me.tbcETiltSlider.TabIndex = 7
        '
        'lbl_EtiltPlanned
        '
        Me.lbl_EtiltPlanned.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbl_EtiltPlanned.Appearance.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_EtiltPlanned.Appearance.Options.UseFont = True
        Me.lbl_EtiltPlanned.Appearance.Options.UseForeColor = True
        Me.lbl_EtiltPlanned.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_EtiltPlanned.Location = New System.Drawing.Point(3, 397)
        Me.lbl_EtiltPlanned.Margin = New System.Windows.Forms.Padding(2)
        Me.lbl_EtiltPlanned.Name = "lbl_EtiltPlanned"
        Me.lbl_EtiltPlanned.Padding = New System.Windows.Forms.Padding(7, 0, 0, 0)
        Me.lbl_EtiltPlanned.Size = New System.Drawing.Size(34, 29)
        Me.lbl_EtiltPlanned.TabIndex = 8
        '
        'sccTiltTreeValidGrid
        '
        Me.sccTiltTreeValidGrid.Collapsed = True
        Me.sccTiltTreeValidGrid.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.sccTiltTreeValidGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccTiltTreeValidGrid.Horizontal = False
        Me.sccTiltTreeValidGrid.Location = New System.Drawing.Point(0, 0)
        Me.sccTiltTreeValidGrid.Name = "sccTiltTreeValidGrid"
        '
        'sccTiltTreeValidGrid.Panel1
        '
        Me.sccTiltTreeValidGrid.Panel1.Controls.Add(Me.tlTiltManager)
        Me.sccTiltTreeValidGrid.Panel1.MinSize = 250
        Me.sccTiltTreeValidGrid.Panel1.Text = "Panel1"
        '
        'sccTiltTreeValidGrid.Panel2
        '
        Me.sccTiltTreeValidGrid.Panel2.Controls.Add(Me.gcCampaignValidation)
        Me.sccTiltTreeValidGrid.Panel2.Text = "Panel2"
        Me.sccTiltTreeValidGrid.Size = New System.Drawing.Size(1271, 438)
        Me.sccTiltTreeValidGrid.SplitterPosition = 165
        Me.sccTiltTreeValidGrid.TabIndex = 2
        '
        'tlTiltManager
        '
        Me.tlTiltManager.Bands.AddRange(New DevExpress.XtraTreeList.Columns.TreeListBand() {Me.Antennas, Me.treeListBand1, Me.treeListBand2, Me.treeListBand3})
        Me.tlTiltManager.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1, Me.TreeListColumn2, Me.TreeListColumn3, Me.TreeListColumn4, Me.TreeListColumn5, Me.TreeListColumn6, Me.TreeListColumn7, Me.TreeListColumn8, Me.TreeListColumn10, Me.tlcValidation, Me.TreeListColumn11, Me.TreeListColumn12, Me.TreeListColumn13, Me.TreeListColumn14, Me.TreeListColumn15, Me.TreeListColumn9, Me.TreeListColumn16, Me.TreeListColumn17, Me.TreeListColumn18, Me.TreeListColumn19, Me.TreeListColumn20, Me.TreeListColumn21})
        Me.tlTiltManager.CustomizationFormBounds = New System.Drawing.Rectangle(2306, 617, 254, 222)
        Me.tlTiltManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlTiltManager.Location = New System.Drawing.Point(0, 0)
        Me.tlTiltManager.Name = "tlTiltManager"
        Me.tlTiltManager.OptionsCustomization.AllowSort = False
        Me.tlTiltManager.OptionsMenu.EnableNodeMenu = False
        Me.tlTiltManager.OptionsView.ShowHorzLines = False
        Me.tlTiltManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageEdit1, Me.RepositoryItemPictureEdit1})
        Me.tlTiltManager.Size = New System.Drawing.Size(1271, 428)
        Me.tlTiltManager.TabIndex = 1
        Me.tlTiltManager.ToolTipController = Me.ToolTipController1
        '
        'Antennas
        '
        Me.Antennas.Caption = "ANTENNAS"
        Me.Antennas.Columns.Add(Me.TreeListColumn1)
        Me.Antennas.Columns.Add(Me.TreeListColumn2)
        Me.Antennas.Columns.Add(Me.TreeListColumn3)
        Me.Antennas.Name = "Antennas"
        Me.Antennas.OptionsBand.AllowMove = False
        Me.Antennas.Width = 190
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Antenna Type"
        Me.TreeListColumn1.FieldName = "AntennaType"
        Me.TreeListColumn1.MinWidth = 100
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.AllowFocus = False
        Me.TreeListColumn1.OptionsColumn.AllowMove = False
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Width = 100
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Azimuth"
        Me.TreeListColumn2.FieldName = "Azimuth"
        Me.TreeListColumn2.MinWidth = 50
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.AllowFocus = False
        Me.TreeListColumn2.OptionsColumn.AllowMove = False
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 1
        Me.TreeListColumn2.Width = 50
        '
        'TreeListColumn3
        '
        Me.TreeListColumn3.Caption = "M-Tilt"
        Me.TreeListColumn3.FieldName = "MTilt"
        Me.TreeListColumn3.MinWidth = 40
        Me.TreeListColumn3.Name = "TreeListColumn3"
        Me.TreeListColumn3.OptionsColumn.AllowFocus = False
        Me.TreeListColumn3.OptionsColumn.AllowMove = False
        Me.TreeListColumn3.Visible = True
        Me.TreeListColumn3.VisibleIndex = 2
        Me.TreeListColumn3.Width = 40
        '
        'treeListBand1
        '
        Me.treeListBand1.Caption = "RET DEVICES"
        Me.treeListBand1.Columns.Add(Me.TreeListColumn4)
        Me.treeListBand1.Columns.Add(Me.TreeListColumn5)
        Me.treeListBand1.Columns.Add(Me.TreeListColumn6)
        Me.treeListBand1.Name = "treeListBand1"
        Me.treeListBand1.OptionsBand.AllowMove = False
        Me.treeListBand1.Width = 186
        '
        'TreeListColumn4
        '
        Me.TreeListColumn4.Caption = "Device Name"
        Me.TreeListColumn4.FieldName = "DeviceName"
        Me.TreeListColumn4.MinWidth = 214
        Me.TreeListColumn4.Name = "TreeListColumn4"
        Me.TreeListColumn4.OptionsColumn.AllowFocus = False
        Me.TreeListColumn4.OptionsColumn.AllowMove = False
        Me.TreeListColumn4.Visible = True
        Me.TreeListColumn4.VisibleIndex = 3
        Me.TreeListColumn4.Width = 214
        '
        'TreeListColumn5
        '
        Me.TreeListColumn5.Caption = "E-Tilt"
        Me.TreeListColumn5.FieldName = "ETilt"
        Me.TreeListColumn5.MinWidth = 40
        Me.TreeListColumn5.Name = "TreeListColumn5"
        Me.TreeListColumn5.OptionsColumn.AllowFocus = False
        Me.TreeListColumn5.OptionsColumn.AllowMove = False
        Me.TreeListColumn5.Visible = True
        Me.TreeListColumn5.VisibleIndex = 4
        Me.TreeListColumn5.Width = 40
        '
        'TreeListColumn6
        '
        Me.TreeListColumn6.Caption = "Device No"
        Me.TreeListColumn6.FieldName = "DeviceNo"
        Me.TreeListColumn6.MinWidth = 60
        Me.TreeListColumn6.Name = "TreeListColumn6"
        Me.TreeListColumn6.OptionsColumn.AllowFocus = False
        Me.TreeListColumn6.OptionsColumn.AllowMove = False
        Me.TreeListColumn6.Visible = True
        Me.TreeListColumn6.VisibleIndex = 5
        Me.TreeListColumn6.Width = 60
        '
        'treeListBand2
        '
        Me.treeListBand2.Caption = "PLAN"
        Me.treeListBand2.Columns.Add(Me.TreeListColumn7)
        Me.treeListBand2.Columns.Add(Me.TreeListColumn8)
        Me.treeListBand2.Columns.Add(Me.TreeListColumn10)
        Me.treeListBand2.Name = "treeListBand2"
        Me.treeListBand2.OptionsBand.AllowMove = False
        Me.treeListBand2.Width = 270
        '
        'TreeListColumn7
        '
        Me.TreeListColumn7.Caption = "Include In Plan"
        Me.TreeListColumn7.FieldName = "IncludeInPlan"
        Me.TreeListColumn7.MinWidth = 129
        Me.TreeListColumn7.Name = "TreeListColumn7"
        Me.TreeListColumn7.OptionsColumn.AllowMove = False
        Me.TreeListColumn7.Visible = True
        Me.TreeListColumn7.VisibleIndex = 6
        Me.TreeListColumn7.Width = 129
        '
        'TreeListColumn8
        '
        Me.TreeListColumn8.Caption = "E-Tilt Planned"
        Me.TreeListColumn8.FieldName = "ETiltPlanned"
        Me.TreeListColumn8.MinWidth = 90
        Me.TreeListColumn8.Name = "TreeListColumn8"
        Me.TreeListColumn8.OptionsColumn.AllowMove = False
        Me.TreeListColumn8.Visible = True
        Me.TreeListColumn8.VisibleIndex = 7
        Me.TreeListColumn8.Width = 90
        '
        'TreeListColumn10
        '
        Me.TreeListColumn10.Caption = "Rule"
        Me.TreeListColumn10.FieldName = "Rule"
        Me.TreeListColumn10.MinWidth = 100
        Me.TreeListColumn10.Name = "TreeListColumn10"
        Me.TreeListColumn10.OptionsColumn.AllowMove = False
        Me.TreeListColumn10.Visible = True
        Me.TreeListColumn10.VisibleIndex = 8
        Me.TreeListColumn10.Width = 100
        '
        'treeListBand3
        '
        Me.treeListBand3.Caption = "CELLS"
        Me.treeListBand3.Columns.Add(Me.tlcValidation)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn11)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn12)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn13)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn14)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn15)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn9)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn16)
        Me.treeListBand3.Columns.Add(Me.TreeListColumn17)
        Me.treeListBand3.Name = "treeListBand3"
        Me.treeListBand3.OptionsBand.AllowMove = False
        Me.treeListBand3.Width = 413
        '
        'tlcValidation
        '
        Me.tlcValidation.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.tlcValidation.FieldName = "Validation"
        Me.tlcValidation.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.tlcValidation.ImageOptions.Image = Global.IOS.My.Resources.Resources.cellinfo16_2
        Me.tlcValidation.MinWidth = 25
        Me.tlcValidation.Name = "tlcValidation"
        Me.tlcValidation.Visible = True
        Me.tlcValidation.VisibleIndex = 9
        Me.tlcValidation.Width = 25
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        Me.RepositoryItemPictureEdit1.NullText = " "
        '
        'TreeListColumn11
        '
        Me.TreeListColumn11.Caption = "Technology"
        Me.TreeListColumn11.FieldName = "Technology"
        Me.TreeListColumn11.MinWidth = 70
        Me.TreeListColumn11.Name = "TreeListColumn11"
        Me.TreeListColumn11.OptionsColumn.AllowFocus = False
        Me.TreeListColumn11.OptionsColumn.AllowMove = False
        Me.TreeListColumn11.Visible = True
        Me.TreeListColumn11.VisibleIndex = 10
        Me.TreeListColumn11.Width = 70
        '
        'TreeListColumn12
        '
        Me.TreeListColumn12.Caption = "Location ID"
        Me.TreeListColumn12.FieldName = "LocationID"
        Me.TreeListColumn12.MinWidth = 70
        Me.TreeListColumn12.Name = "TreeListColumn12"
        Me.TreeListColumn12.OptionsColumn.AllowFocus = False
        Me.TreeListColumn12.OptionsColumn.AllowMove = False
        Me.TreeListColumn12.Visible = True
        Me.TreeListColumn12.VisibleIndex = 11
        Me.TreeListColumn12.Width = 70
        '
        'TreeListColumn13
        '
        Me.TreeListColumn13.Caption = "MBTS Name"
        Me.TreeListColumn13.FieldName = "MBTS_Name"
        Me.TreeListColumn13.MinWidth = 80
        Me.TreeListColumn13.Name = "TreeListColumn13"
        Me.TreeListColumn13.OptionsColumn.AllowFocus = False
        Me.TreeListColumn13.OptionsColumn.AllowMove = False
        Me.TreeListColumn13.Visible = True
        Me.TreeListColumn13.VisibleIndex = 12
        Me.TreeListColumn13.Width = 80
        '
        'TreeListColumn14
        '
        Me.TreeListColumn14.Caption = "Sector ID"
        Me.TreeListColumn14.FieldName = "SectorID"
        Me.TreeListColumn14.MinWidth = 70
        Me.TreeListColumn14.Name = "TreeListColumn14"
        Me.TreeListColumn14.OptionsColumn.AllowFocus = False
        Me.TreeListColumn14.OptionsColumn.AllowMove = False
        Me.TreeListColumn14.Visible = True
        Me.TreeListColumn14.VisibleIndex = 13
        Me.TreeListColumn14.Width = 70
        '
        'TreeListColumn15
        '
        Me.TreeListColumn15.Caption = "Layer"
        Me.TreeListColumn15.FieldName = "Layer"
        Me.TreeListColumn15.MinWidth = 60
        Me.TreeListColumn15.Name = "TreeListColumn15"
        Me.TreeListColumn15.OptionsColumn.AllowFocus = False
        Me.TreeListColumn15.Visible = True
        Me.TreeListColumn15.VisibleIndex = 14
        Me.TreeListColumn15.Width = 60
        '
        'TreeListColumn9
        '
        Me.TreeListColumn9.Caption = "VBeam Angle"
        Me.TreeListColumn9.FieldName = "Vangle"
        Me.TreeListColumn9.MinWidth = 100
        Me.TreeListColumn9.Name = "TreeListColumn9"
        Me.TreeListColumn9.OptionsColumn.AllowFocus = False
        Me.TreeListColumn9.OptionsColumn.AllowMove = False
        Me.TreeListColumn9.Visible = True
        Me.TreeListColumn9.VisibleIndex = 15
        Me.TreeListColumn9.Width = 100
        '
        'TreeListColumn16
        '
        Me.TreeListColumn16.Caption = "Cell Name"
        Me.TreeListColumn16.FieldName = "CellName"
        Me.TreeListColumn16.MinWidth = 171
        Me.TreeListColumn16.Name = "TreeListColumn16"
        Me.TreeListColumn16.OptionsColumn.AllowFocus = False
        Me.TreeListColumn16.OptionsColumn.AllowMove = False
        Me.TreeListColumn16.Visible = True
        Me.TreeListColumn16.VisibleIndex = 16
        Me.TreeListColumn16.Width = 171
        '
        'TreeListColumn17
        '
        Me.TreeListColumn17.Caption = "Cell ID"
        Me.TreeListColumn17.FieldName = "CellID"
        Me.TreeListColumn17.MinWidth = 50
        Me.TreeListColumn17.Name = "TreeListColumn17"
        Me.TreeListColumn17.OptionsColumn.AllowFocus = False
        Me.TreeListColumn17.OptionsColumn.AllowMove = False
        Me.TreeListColumn17.Visible = True
        Me.TreeListColumn17.VisibleIndex = 17
        Me.TreeListColumn17.Width = 50
        '
        'TreeListColumn18
        '
        Me.TreeListColumn18.Caption = "X"
        Me.TreeListColumn18.FieldName = "X"
        Me.TreeListColumn18.Name = "TreeListColumn18"
        Me.TreeListColumn18.OptionsColumn.AllowFocus = False
        '
        'TreeListColumn19
        '
        Me.TreeListColumn19.Caption = "Y"
        Me.TreeListColumn19.FieldName = "Y"
        Me.TreeListColumn19.Name = "TreeListColumn19"
        Me.TreeListColumn19.OptionsColumn.AllowFocus = False
        '
        'TreeListColumn20
        '
        Me.TreeListColumn20.Caption = "RADIATIONCENTER"
        Me.TreeListColumn20.FieldName = "RADIATIONCENTER"
        Me.TreeListColumn20.Name = "TreeListColumn20"
        Me.TreeListColumn20.OptionsColumn.AllowFocus = False
        '
        'TreeListColumn21
        '
        Me.TreeListColumn21.Caption = "DEVICELINKEDTO"
        Me.TreeListColumn21.FieldName = "DEVICELINKEDTO"
        Me.TreeListColumn21.Name = "TreeListColumn21"
        Me.TreeListColumn21.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemImageEdit1
        '
        Me.RepositoryItemImageEdit1.AutoHeight = False
        Me.RepositoryItemImageEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageEdit1.Name = "RepositoryItemImageEdit1"
        '
        'gcCampaignValidation
        '
        Me.gcCampaignValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCampaignValidation.Location = New System.Drawing.Point(0, 0)
        Me.gcCampaignValidation.MainView = Me.gvCampaignValidation
        Me.gcCampaignValidation.Name = "gcCampaignValidation"
        Me.gcCampaignValidation.Size = New System.Drawing.Size(0, 0)
        Me.gcCampaignValidation.TabIndex = 11
        Me.gcCampaignValidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCampaignValidation})
        '
        'gvCampaignValidation
        '
        Me.gvCampaignValidation.GridControl = Me.gcCampaignValidation
        Me.gvCampaignValidation.Name = "gvCampaignValidation"
        Me.gvCampaignValidation.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignValidation.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCampaignValidation.OptionsBehavior.Editable = False
        Me.gvCampaignValidation.OptionsBehavior.ReadOnly = True
        Me.gvCampaignValidation.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignValidation.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignValidation.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCampaignValidation.OptionsView.ColumnAutoWidth = False
        Me.gvCampaignValidation.OptionsView.ShowGroupPanel = False
        '
        'TreeListColumn22
        '
        Me.TreeListColumn22.Caption = "Validation"
        Me.TreeListColumn22.FieldName = "Validation"
        Me.TreeListColumn22.MinWidth = 70
        Me.TreeListColumn22.Name = "TreeListColumn22"
        Me.TreeListColumn22.OptionsColumn.AllowFocus = False
        Me.TreeListColumn22.OptionsColumn.AllowMove = False
        Me.TreeListColumn22.Visible = True
        Me.TreeListColumn22.VisibleIndex = 9
        Me.TreeListColumn22.Width = 70
        '
        'frmLaunchTiltManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1271, 877)
        Me.Controls.Add(Me.sccMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmLaunchTiltManager.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1100, 700)
        Me.Name = "frmLaunchTiltManager"
        Me.Text = "Tilt Manager"
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel1.ResumeLayout(False)
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel2.ResumeLayout(False)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.ch_TiltManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.gcSectorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsSectorList.ResumeLayout(False)
        CType(Me.gvSectorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.cmbManualCampaign.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtETiltValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.tbcETiltSlider.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbcETiltSlider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccTiltTreeValidGrid.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTiltTreeValidGrid.Panel1.ResumeLayout(False)
        CType(Me.sccTiltTreeValidGrid.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTiltTreeValidGrid.Panel2.ResumeLayout(False)
        CType(Me.sccTiltTreeValidGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTiltTreeValidGrid.ResumeLayout(False)
        CType(Me.tlTiltManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCampaignValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCampaignValidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents sccMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btnGenerateTiltCampaign As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlTiltManager As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ch_TiltManager As dotnetCHARTING.WinForms.Chart
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn3 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn4 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn5 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn6 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn7 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn8 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn10 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn11 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn12 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn13 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn14 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn15 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn16 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn17 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents gvSectorList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents btnCalculateAndSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TreeListColumn9 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents btnAddCampaign As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TreeListColumn18 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn19 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn20 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Public WithEvents gcSectorList As DevExpress.XtraGrid.GridControl

    Friend WithEvents tbcETiltSlider As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents lbl_EtiltPlanned As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tglPlanned As Library.IOSToggleButton
    Friend WithEvents btnClearThematics As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TreeListColumn21 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents sccTiltTreeValidGrid As DevExpress.XtraEditors.SplitContainerControl
    Public WithEvents gcCampaignValidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCampaignValidation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Antennas As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents treeListBand1 As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents treeListBand2 As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents treeListBand3 As DevExpress.XtraTreeList.Columns.TreeListBand
    Friend WithEvents TreeListColumn22 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents RepositoryItemImageEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemImageEdit
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents btnDeleteCampaign As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmsSectorList As ContextMenuStrip
    Friend WithEvents tsmi_DeleteSector As ToolStripMenuItem
    Friend WithEvents tlcValidation As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbResolution As DevExpress.XtraEditors.ComboBoxEdit
    Public WithEvents cmbManualCampaign As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnManageTree As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtETiltValue As DevExpress.XtraEditors.TextEdit
End Class
