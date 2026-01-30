<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuickThemeControl
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
        Dim TreeListViewColumn1 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuickThemeControl))
        Dim TreeListViewColumn2 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim ListColumnFormatStyle1 As LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle = New LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lvLayers = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.vgbThematic = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.vcmbThematicType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.vcmbThemticBinsDistribution = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.vbtnApply = New DevExpress.XtraEditors.SimpleButton()
        Me.btnReset = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRecalculateBins = New DevExpress.XtraEditors.SimpleButton()
        Me.vlblKPI = New DevExpress.XtraEditors.LabelControl()
        Me.tblBottomPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlThematicBins = New System.Windows.Forms.Panel()
        Me.tlpIndividualBins = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpThemeBins = New System.Windows.Forms.TableLayoutPanel()
        Me.vtbBins = New DevExpress.XtraEditors.TrackBarControl()
        Me.clpBins = New System.Windows.Forms.ColorDialog()
        Me.tlpMain.SuspendLayout()
        CType(Me.lvLayers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vgbThematic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vgbThematic.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.vcmbThematicType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vcmbThemticBinsDistribution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.tblBottomPanel.SuspendLayout()
        Me.pnlThematicBins.SuspendLayout()
        CType(Me.vtbBins, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vtbBins.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.BackColor = System.Drawing.Color.Transparent
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpMain.Controls.Add(Me.lvLayers, 0, 1)
        Me.tlpMain.Controls.Add(Me.vgbThematic, 0, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.Size = New System.Drawing.Size(309, 687)
        Me.tlpMain.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(303, 19)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Feature Layers"
        '
        'lvLayers
        '
        Me.lvLayers.AllowSelectionCheck = True
        Me.lvLayers.AllowSubItemSelection = False
        TreeListViewColumn1.FooterRect = CType(resources.GetObject("TreeListViewColumn1.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.FooterText = "Footer 1"
        TreeListViewColumn1.HeaderRect = CType(resources.GetObject("TreeListViewColumn1.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.HeaderText = "Layers"
        TreeListViewColumn1.Width = 230
        TreeListViewColumn2.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible
        TreeListViewColumn2.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control
        TreeListViewColumn2.Fixed = LidorSystems.IntegralUI.Lists.ColumnFixedType.Right
        TreeListViewColumn2.FixedWidth = True
        TreeListViewColumn2.FooterRect = CType(resources.GetObject("TreeListViewColumn2.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.FooterText = "Footer 3"
        ListColumnFormatStyle1.ContentAlign = System.Windows.Forms.HorizontalAlignment.Center
        TreeListViewColumn2.FormatStyle = ListColumnFormatStyle1
        TreeListViewColumn2.HeaderRect = CType(resources.GetObject("TreeListViewColumn2.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.HeaderText = "Visible"
        Me.lvLayers.Columns.AddRange(New Object() {TreeListViewColumn1, TreeListViewColumn2})
        '
        '
        '
        Me.lvLayers.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.lvLayers.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.lvLayers.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.lvLayers.ContentPanel.Name = ""
        Me.lvLayers.ContentPanel.Size = New System.Drawing.Size(295, 317)
        Me.lvLayers.ContentPanel.TabIndex = 3
        Me.lvLayers.ContentPanel.TabStop = False
        Me.lvLayers.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvLayers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvLayers.DragDropMode = LidorSystems.IntegralUI.DragDropMode.Custom
        Me.lvLayers.DropMarkerType = LidorSystems.IntegralUI.Lists.DropMarkerType.[Partial]
        Me.lvLayers.ExpandingColumn = TreeListViewColumn1
        Me.lvLayers.Footer = False
        Me.lvLayers.Location = New System.Drawing.Point(4, 29)
        Me.lvLayers.Margin = New System.Windows.Forms.Padding(4)
        Me.lvLayers.Name = "lvLayers"
        Me.lvLayers.Size = New System.Drawing.Size(301, 323)
        Me.lvLayers.TabIndex = 5
        '
        'vgbThematic
        '
        Me.vgbThematic.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.vgbThematic.Appearance.Options.UseBackColor = True
        Me.vgbThematic.Controls.Add(Me.TableLayoutPanel1)
        Me.vgbThematic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vgbThematic.Location = New System.Drawing.Point(3, 359)
        Me.vgbThematic.Name = "vgbThematic"
        Me.vgbThematic.Size = New System.Drawing.Size(303, 325)
        Me.vgbThematic.TabIndex = 6
        Me.vgbThematic.Text = "Thematics"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 22)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(299, 301)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.03509!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.96491!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl2, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.vcmbThematicType, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.vcmbThemticBinsDistribution, 1, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(293, 55)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 30)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(108, 22)
        Me.LabelControl3.TabIndex = 6
        Me.LabelControl3.Text = "Bins Distribution"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(108, 21)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Thematic Type"
        '
        'vcmbThematicType
        '
        Me.vcmbThematicType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vcmbThematicType.Location = New System.Drawing.Point(117, 3)
        Me.vcmbThematicType.Name = "vcmbThematicType"
        Me.vcmbThematicType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.vcmbThematicType.Size = New System.Drawing.Size(173, 20)
        Me.vcmbThematicType.TabIndex = 7
        '
        'vcmbThemticBinsDistribution
        '
        Me.vcmbThemticBinsDistribution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vcmbThemticBinsDistribution.EditValue = "Select"
        Me.vcmbThemticBinsDistribution.Location = New System.Drawing.Point(117, 30)
        Me.vcmbThemticBinsDistribution.Name = "vcmbThemticBinsDistribution"
        Me.vcmbThemticBinsDistribution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.vcmbThemticBinsDistribution.Properties.Items.AddRange(New Object() {"Equal Count", "Equal Ranges", "Natural Break", "Standard Deviation", "Custom"})
        Me.vcmbThemticBinsDistribution.Size = New System.Drawing.Size(173, 20)
        Me.vcmbThemticBinsDistribution.TabIndex = 8
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.vbtnApply, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnReset, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 64)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(293, 31)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'vbtnApply
        '
        Me.vbtnApply.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vbtnApply.Location = New System.Drawing.Point(114, 3)
        Me.vbtnApply.Name = "vbtnApply"
        Me.vbtnApply.Size = New System.Drawing.Size(176, 26)
        Me.vbtnApply.TabIndex = 2
        Me.vbtnApply.Text = "Apply to all cell layers"
        '
        'btnReset
        '
        Me.btnReset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReset.Enabled = False
        Me.btnReset.Location = New System.Drawing.Point(3, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(105, 26)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.tblBottomPanel, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 101)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(293, 197)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.btnRecalculateBins, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.vlblKPI, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(287, 30)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'btnRecalculateBins
        '
        Me.btnRecalculateBins.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRecalculateBins.Location = New System.Drawing.Point(112, 3)
        Me.btnRecalculateBins.Name = "btnRecalculateBins"
        Me.btnRecalculateBins.Size = New System.Drawing.Size(172, 25)
        Me.btnRecalculateBins.TabIndex = 8
        Me.btnRecalculateBins.Text = "Calculate Bins"
        '
        'vlblKPI
        '
        Me.vlblKPI.Appearance.Options.UseTextOptions = True
        Me.vlblKPI.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.vlblKPI.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.vlblKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vlblKPI.Location = New System.Drawing.Point(3, 3)
        Me.vlblKPI.Name = "vlblKPI"
        Me.vlblKPI.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.vlblKPI.Size = New System.Drawing.Size(103, 25)
        Me.vlblKPI.TabIndex = 7
        Me.vlblKPI.Text = "X=KPI"
        '
        'tblBottomPanel
        '
        Me.tblBottomPanel.ColumnCount = 2
        Me.tblBottomPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottomPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tblBottomPanel.Controls.Add(Me.pnlThematicBins, 0, 0)
        Me.tblBottomPanel.Controls.Add(Me.vtbBins, 1, 0)
        Me.tblBottomPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottomPanel.Location = New System.Drawing.Point(3, 39)
        Me.tblBottomPanel.Name = "tblBottomPanel"
        Me.tblBottomPanel.RowCount = 1
        Me.tblBottomPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottomPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156.0!))
        Me.tblBottomPanel.Size = New System.Drawing.Size(287, 155)
        Me.tblBottomPanel.TabIndex = 1
        '
        'pnlThematicBins
        '
        Me.pnlThematicBins.AutoScroll = True
        Me.pnlThematicBins.Controls.Add(Me.tlpIndividualBins)
        Me.pnlThematicBins.Controls.Add(Me.tlpThemeBins)
        Me.pnlThematicBins.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlThematicBins.Location = New System.Drawing.Point(3, 3)
        Me.pnlThematicBins.Name = "pnlThematicBins"
        Me.pnlThematicBins.Size = New System.Drawing.Size(241, 149)
        Me.pnlThematicBins.TabIndex = 3
        '
        'tlpIndividualBins
        '
        Me.tlpIndividualBins.ColumnCount = 3
        Me.tlpIndividualBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpIndividualBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpIndividualBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpIndividualBins.Location = New System.Drawing.Point(3, 21)
        Me.tlpIndividualBins.Name = "tlpIndividualBins"
        Me.tlpIndividualBins.RowCount = 1
        Me.tlpIndividualBins.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpIndividualBins.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpIndividualBins.Size = New System.Drawing.Size(185, 100)
        Me.tlpIndividualBins.TabIndex = 1
        Me.tlpIndividualBins.Visible = False
        '
        'tlpThemeBins
        '
        Me.tlpThemeBins.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpThemeBins.ColumnCount = 6
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpThemeBins.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.tlpThemeBins.Location = New System.Drawing.Point(0, 0)
        Me.tlpThemeBins.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpThemeBins.Name = "tlpThemeBins"
        Me.tlpThemeBins.RowCount = 1
        Me.tlpThemeBins.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpThemeBins.Size = New System.Drawing.Size(169, 10)
        Me.tlpThemeBins.TabIndex = 0
        '
        'vtbBins
        '
        Me.vtbBins.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vtbBins.EditValue = 1
        Me.vtbBins.Location = New System.Drawing.Point(250, 3)
        Me.vtbBins.Name = "vtbBins"
        Me.vtbBins.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.vtbBins.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.vtbBins.Properties.Minimum = 1
        Me.vtbBins.Properties.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.vtbBins.Size = New System.Drawing.Size(34, 149)
        Me.vtbBins.TabIndex = 4
        Me.vtbBins.Value = 1
        '
        'frmQuickThemeControl
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(309, 687)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmQuickThemeControl.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 688)
        Me.Name = "frmQuickThemeControl"
        Me.Opacity = 0.75R
        Me.Text = "Quick Layer Control"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.lvLayers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vgbThematic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vgbThematic.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.vcmbThematicType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vcmbThemticBinsDistribution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.tblBottomPanel.ResumeLayout(False)
        Me.tblBottomPanel.PerformLayout()
        Me.pnlThematicBins.ResumeLayout(False)
        CType(Me.vtbBins.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vtbBins, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lvLayers As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents vgbThematic As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents vcmbThematicType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents vcmbThemticBinsDistribution As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vbtnApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnReset As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnRecalculateBins As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents vlblKPI As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tblBottomPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlThematicBins As System.Windows.Forms.Panel
    Friend WithEvents tlpIndividualBins As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlpThemeBins As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vtbBins As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents clpBins As System.Windows.Forms.ColorDialog
End Class
