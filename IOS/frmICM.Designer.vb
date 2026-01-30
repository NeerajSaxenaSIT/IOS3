<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmICM
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
        Dim ListItemColorStyle1 As LidorSystems.IntegralUI.Lists.Style.ListItemColorStyle = New LidorSystems.IntegralUI.Lists.Style.ListItemColorStyle()
        Dim ListItemColorStyle2 As LidorSystems.IntegralUI.Lists.Style.ListItemColorStyle = New LidorSystems.IntegralUI.Lists.Style.ListItemColorStyle()
        Dim Annotation1 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim Label2 As dotnetCHARTING.WinForms.Label = New dotnetCHARTING.WinForms.Label()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmICM))
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.VGroupBox1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel16 = New System.Windows.Forms.Label()
        Me.VLabel1 = New System.Windows.Forms.Label()
        Me.VLabel7 = New System.Windows.Forms.Label()
        Me.txtSearchObject = New DevExpress.XtraEditors.ButtonEdit()
        Me.VLabel5 = New System.Windows.Forms.Label()
        Me.VLabel8 = New System.Windows.Forms.Label()
        Me.AccordionControl1 = New DevExpress.XtraBars.Navigation.AccordionControl()
        Me.AccordionContentContainer2 = New DevExpress.XtraBars.Navigation.AccordionContentContainer()
        Me.ExTableLayoutPanel35 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel19 = New System.Windows.Forms.Label()
        Me.chkFilterCriteriaCombine = New DevExpress.XtraEditors.CheckEdit()
        Me.ExTableLayoutPanel36 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel12 = New System.Windows.Forms.Label()
        Me.VLabel14 = New System.Windows.Forms.Label()
        Me.txtXY = New DevExpress.XtraEditors.TextEdit()
        Me.cmbTemplate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ExTableLayoutPanel37 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtFilterValue = New DevExpress.XtraEditors.TextEdit()
        Me.cmbFilterKPI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbFilterOp = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ExTableLayoutPanel38 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFilterAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDel = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tlvFilters = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.AccordionContentContainer1 = New DevExpress.XtraBars.Navigation.AccordionContentContainer()
        Me.tvObjectTree = New System.Windows.Forms.TreeView()
        Me.aceObjectTree = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.aceFilters = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.cmbReport = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbVendor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbTargetObject = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ExTableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.spltConAllChartGrid = New System.Windows.Forms.SplitContainer()
        Me.vtabICM = New DevExpress.XtraTab.XtraTabControl()
        Me.vtpICMOverview = New DevExpress.XtraTab.XtraTabPage()
        Me.ExTableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.chart_Overview = New dotnetCHARTING.WinForms.Chart()
        Me.cms_OverviewChart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_MapAllWithThematic = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ShowHideOverviewForecast = New System.Windows.Forms.ToolStripMenuItem()
        Me.VGroupBox8 = New DevExpress.XtraEditors.GroupControl()
        Me.ExTableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.ExTableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel4 = New System.Windows.Forms.Label()
        Me.btnFeedbackSave = New DevExpress.XtraEditors.SimpleButton()
        Me.chkApproved = New DevExpress.XtraEditors.CheckEdit()
        Me.lblMSG = New System.Windows.Forms.Label()
        Me.txtComment = New DevExpress.XtraEditors.MemoEdit()
        Me.ExTableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel11 = New System.Windows.Forms.Label()
        Me.lblRecommendation = New System.Windows.Forms.Label()
        Me.VPanel1 = New System.Windows.Forms.Panel()
        Me.ExTableLayoutPanel39 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblForcastStatistics = New System.Windows.Forms.Label()
        Me.VGroupBox4 = New DevExpress.XtraEditors.GroupControl()
        Me.cms_HistogramChart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_ObjectCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_sendAlltoConsoletee = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_sendconsoletree = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_SendToMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_EnableVoronoi = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_SendToMapAllGraduatedTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SendToMapAllRangedTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_UsingPieTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SendToMapAllGeoAggregation = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SendToMapAllGeoAggregationFunction = New System.Windows.Forms.ToolStripComboBox()
        Me.CirclePresentationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BufferPresentationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SendToMapAllHeatMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_UsingPreconfigured = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SendToMapSelect = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_HideAndShowGrid = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ShowHideForecastHistogramChart = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SendToMapSelectedGraduatedTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SendToMapSelectedGeoAggregationFunction = New System.Windows.Forms.ToolStripComboBox()
        Me.tsmi_HeatMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_SubCategoryChart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_HideAndShowGridSubCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ShowHideForecastSubCategoryChart = New System.Windows.Forms.ToolStripMenuItem()
        Me.Content = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel13 = New System.Windows.Forms.Label()
        Me.VLabel15 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel19 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.VGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VGroupBox1.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.txtSearchObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AccordionControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AccordionControl1.SuspendLayout()
        Me.AccordionContentContainer2.SuspendLayout()
        Me.ExTableLayoutPanel35.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.chkFilterCriteriaCombine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel36.SuspendLayout()
        CType(Me.txtXY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel37.SuspendLayout()
        CType(Me.txtFilterValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFilterKPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFilterOp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel38.SuspendLayout()
        CType(Me.tlvFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AccordionContentContainer1.SuspendLayout()
        CType(Me.cmbReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel1.SuspendLayout()
        CType(Me.spltConAllChartGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spltConAllChartGrid.Panel1.SuspendLayout()
        Me.spltConAllChartGrid.Panel2.SuspendLayout()
        Me.spltConAllChartGrid.SuspendLayout()
        CType(Me.vtabICM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vtabICM.SuspendLayout()
        Me.vtpICMOverview.SuspendLayout()
        Me.ExTableLayoutPanel7.SuspendLayout()
        CType(Me.chart_Overview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cms_OverviewChart.SuspendLayout()
        CType(Me.VGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VGroupBox8.SuspendLayout()
        Me.ExTableLayoutPanel34.SuspendLayout()
        Me.ExTableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.chkApproved.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExTableLayoutPanel5.SuspendLayout()
        Me.ExTableLayoutPanel39.SuspendLayout()
        CType(Me.VGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cms_HistogramChart.SuspendLayout()
        Me.cms_SubCategoryChart.SuspendLayout()
        Me.TableLayoutPanel15.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(2, 2)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.VGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.ExTableLayoutPanel1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1249, 738)
        Me.SplitContainer2.SplitterDistance = 270
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 9
        '
        'VGroupBox1
        '
        Me.VGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VGroupBox1.Appearance.Options.UseBackColor = True
        Me.VGroupBox1.Controls.Add(Me.TableLayoutPanel7)
        Me.VGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.VGroupBox1.Name = "VGroupBox1"
        Me.VGroupBox1.Size = New System.Drawing.Size(270, 738)
        Me.VGroupBox1.TabIndex = 1
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.VLabel16, 0, 4)
        Me.TableLayoutPanel7.Controls.Add(Me.VLabel1, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.VLabel7, 0, 8)
        Me.TableLayoutPanel7.Controls.Add(Me.txtSearchObject, 0, 9)
        Me.TableLayoutPanel7.Controls.Add(Me.VLabel5, 0, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.VLabel8, 0, 6)
        Me.TableLayoutPanel7.Controls.Add(Me.AccordionControl1, 0, 10)
        Me.TableLayoutPanel7.Controls.Add(Me.cmbReport, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.cmbTechnology, 0, 3)
        Me.TableLayoutPanel7.Controls.Add(Me.cmbVendor, 0, 5)
        Me.TableLayoutPanel7.Controls.Add(Me.cmbTargetObject, 0, 7)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 11
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(266, 716)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'VLabel16
        '
        Me.VLabel16.BackColor = System.Drawing.Color.Transparent
        Me.VLabel16.Dock = System.Windows.Forms.DockStyle.Left
        Me.VLabel16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel16.Location = New System.Drawing.Point(4, 108)
        Me.VLabel16.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel16.Name = "VLabel16"
        Me.VLabel16.Size = New System.Drawing.Size(172, 17)
        Me.VLabel16.TabIndex = 13
        Me.VLabel16.Text = "Vendor Selection:"
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.VLabel1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel1.Location = New System.Drawing.Point(4, 4)
        Me.VLabel1.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(172, 17)
        Me.VLabel1.TabIndex = 0
        Me.VLabel1.Text = "Report Selection:"
        '
        'VLabel7
        '
        Me.VLabel7.BackColor = System.Drawing.Color.Transparent
        Me.VLabel7.Dock = System.Windows.Forms.DockStyle.Left
        Me.VLabel7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel7.Location = New System.Drawing.Point(4, 212)
        Me.VLabel7.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel7.Name = "VLabel7"
        Me.VLabel7.Size = New System.Drawing.Size(160, 17)
        Me.VLabel7.TabIndex = 4
        Me.VLabel7.Text = "Search Object Text:"
        '
        'txtSearchObject
        '
        Me.txtSearchObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchObject.Location = New System.Drawing.Point(3, 236)
        Me.txtSearchObject.Name = "txtSearchObject"
        Me.txtSearchObject.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtSearchObject.Properties.Appearance.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtSearchObject.Properties.Appearance.Options.UseBackColor = True
        Me.txtSearchObject.Properties.Appearance.Options.UseForeColor = True
        Me.txtSearchObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchObject.Properties.NullValuePrompt = "Search..."
        Me.txtSearchObject.Properties.NullValuePromptShowForEmptyValue = True
        Me.txtSearchObject.Size = New System.Drawing.Size(260, 20)
        Me.txtSearchObject.TabIndex = 6
        '
        'VLabel5
        '
        Me.VLabel5.BackColor = System.Drawing.Color.Transparent
        Me.VLabel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.VLabel5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel5.Location = New System.Drawing.Point(4, 56)
        Me.VLabel5.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel5.Name = "VLabel5"
        Me.VLabel5.Size = New System.Drawing.Size(172, 17)
        Me.VLabel5.TabIndex = 3
        Me.VLabel5.Text = "Technology Selection:"
        '
        'VLabel8
        '
        Me.VLabel8.BackColor = System.Drawing.Color.Transparent
        Me.VLabel8.Dock = System.Windows.Forms.DockStyle.Left
        Me.VLabel8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel8.Location = New System.Drawing.Point(4, 160)
        Me.VLabel8.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel8.Name = "VLabel8"
        Me.VLabel8.Size = New System.Drawing.Size(172, 17)
        Me.VLabel8.TabIndex = 11
        Me.VLabel8.Text = "Object Type:"
        '
        'AccordionControl1
        '
        Me.AccordionControl1.Controls.Add(Me.AccordionContentContainer2)
        Me.AccordionControl1.Controls.Add(Me.AccordionContentContainer1)
        Me.AccordionControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccordionControl1.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.aceObjectTree, Me.aceFilters})
        Me.AccordionControl1.Location = New System.Drawing.Point(3, 263)
        Me.AccordionControl1.LookAndFeel.SkinName = "Office 2013"
        Me.AccordionControl1.Name = "AccordionControl1"
        Me.AccordionControl1.Size = New System.Drawing.Size(260, 450)
        Me.AccordionControl1.TabIndex = 15
        Me.AccordionControl1.Text = "AccordionControl1"
        '
        'AccordionContentContainer2
        '
        Me.AccordionContentContainer2.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.AccordionContentContainer2.Appearance.Options.UseBackColor = True
        Me.AccordionContentContainer2.Controls.Add(Me.ExTableLayoutPanel35)
        Me.AccordionContentContainer2.Name = "AccordionContentContainer2"
        Me.AccordionContentContainer2.Size = New System.Drawing.Size(243, 385)
        Me.AccordionContentContainer2.TabIndex = 2
        '
        'ExTableLayoutPanel35
        '
        Me.ExTableLayoutPanel35.ColumnCount = 1
        Me.ExTableLayoutPanel35.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel35.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.ExTableLayoutPanel35.Controls.Add(Me.ExTableLayoutPanel36, 0, 0)
        Me.ExTableLayoutPanel35.Controls.Add(Me.ExTableLayoutPanel37, 0, 3)
        Me.ExTableLayoutPanel35.Controls.Add(Me.ExTableLayoutPanel38, 0, 2)
        Me.ExTableLayoutPanel35.Controls.Add(Me.tlvFilters, 0, 4)
        Me.ExTableLayoutPanel35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel35.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel35.Name = "ExTableLayoutPanel35"
        Me.ExTableLayoutPanel35.RowCount = 5
        Me.ExTableLayoutPanel35.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.ExTableLayoutPanel35.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel35.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.ExTableLayoutPanel35.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.ExTableLayoutPanel35.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel35.Size = New System.Drawing.Size(243, 385)
        Me.ExTableLayoutPanel35.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel19, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.chkFilterCriteriaCombine, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 64)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(239, 26)
        Me.TableLayoutPanel1.TabIndex = 12
        '
        'VLabel19
        '
        Me.VLabel19.BackColor = System.Drawing.Color.Transparent
        Me.VLabel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.VLabel19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel19.Location = New System.Drawing.Point(1, 4)
        Me.VLabel19.Margin = New System.Windows.Forms.Padding(1, 4, 0, 4)
        Me.VLabel19.Name = "VLabel19"
        Me.VLabel19.Size = New System.Drawing.Size(214, 18)
        Me.VLabel19.TabIndex = 4
        Me.VLabel19.Text = "Combine All Filter Criteria"
        '
        'chkFilterCriteriaCombine
        '
        Me.chkFilterCriteriaCombine.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkFilterCriteriaCombine.Location = New System.Drawing.Point(219, 4)
        Me.chkFilterCriteriaCombine.Margin = New System.Windows.Forms.Padding(4)
        Me.chkFilterCriteriaCombine.Name = "chkFilterCriteriaCombine"
        Me.chkFilterCriteriaCombine.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.chkFilterCriteriaCombine.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.chkFilterCriteriaCombine.Properties.Appearance.Options.UseBackColor = True
        Me.chkFilterCriteriaCombine.Properties.Appearance.Options.UseFont = True
        Me.chkFilterCriteriaCombine.Size = New System.Drawing.Size(16, 18)
        Me.chkFilterCriteriaCombine.TabIndex = 5
        '
        'ExTableLayoutPanel36
        '
        Me.ExTableLayoutPanel36.ColumnCount = 2
        Me.ExTableLayoutPanel36.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.16736!))
        Me.ExTableLayoutPanel36.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.83264!))
        Me.ExTableLayoutPanel36.Controls.Add(Me.VLabel12, 0, 1)
        Me.ExTableLayoutPanel36.Controls.Add(Me.VLabel14, 0, 0)
        Me.ExTableLayoutPanel36.Controls.Add(Me.txtXY, 1, 0)
        Me.ExTableLayoutPanel36.Controls.Add(Me.cmbTemplate, 1, 1)
        Me.ExTableLayoutPanel36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel36.Location = New System.Drawing.Point(2, 2)
        Me.ExTableLayoutPanel36.Margin = New System.Windows.Forms.Padding(2)
        Me.ExTableLayoutPanel36.Name = "ExTableLayoutPanel36"
        Me.ExTableLayoutPanel36.RowCount = 2
        Me.ExTableLayoutPanel36.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel36.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.ExTableLayoutPanel36.Size = New System.Drawing.Size(239, 58)
        Me.ExTableLayoutPanel36.TabIndex = 6
        '
        'VLabel12
        '
        Me.VLabel12.BackColor = System.Drawing.Color.Transparent
        Me.VLabel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel12.Location = New System.Drawing.Point(4, 33)
        Me.VLabel12.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel12.Name = "VLabel12"
        Me.VLabel12.Size = New System.Drawing.Size(87, 21)
        Me.VLabel12.TabIndex = 10
        Me.VLabel12.Text = "Select Template"
        '
        'VLabel14
        '
        Me.VLabel14.BackColor = System.Drawing.Color.Transparent
        Me.VLabel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel14.ForeColor = System.Drawing.Color.Black
        Me.VLabel14.Location = New System.Drawing.Point(4, 4)
        Me.VLabel14.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel14.Name = "VLabel14"
        Me.VLabel14.Size = New System.Drawing.Size(87, 21)
        Me.VLabel14.TabIndex = 6
        Me.VLabel14.Text = "Select X in Top X"
        '
        'txtXY
        '
        Me.txtXY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtXY.Location = New System.Drawing.Point(99, 4)
        Me.txtXY.Margin = New System.Windows.Forms.Padding(4)
        Me.txtXY.Name = "txtXY"
        Me.txtXY.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtXY.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.txtXY.Properties.Appearance.Options.UseBackColor = True
        Me.txtXY.Properties.Appearance.Options.UseForeColor = True
        Me.txtXY.Size = New System.Drawing.Size(136, 20)
        Me.txtXY.TabIndex = 9
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTemplate.Location = New System.Drawing.Point(98, 32)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTemplate.Size = New System.Drawing.Size(138, 20)
        Me.cmbTemplate.TabIndex = 11
        '
        'ExTableLayoutPanel37
        '
        Me.ExTableLayoutPanel37.ColumnCount = 3
        Me.ExTableLayoutPanel37.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel37.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59.0!))
        Me.ExTableLayoutPanel37.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.ExTableLayoutPanel37.Controls.Add(Me.txtFilterValue, 2, 0)
        Me.ExTableLayoutPanel37.Controls.Add(Me.cmbFilterKPI, 0, 0)
        Me.ExTableLayoutPanel37.Controls.Add(Me.cmbFilterOp, 1, 0)
        Me.ExTableLayoutPanel37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel37.Location = New System.Drawing.Point(3, 133)
        Me.ExTableLayoutPanel37.Name = "ExTableLayoutPanel37"
        Me.ExTableLayoutPanel37.RowCount = 1
        Me.ExTableLayoutPanel37.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel37.Size = New System.Drawing.Size(237, 29)
        Me.ExTableLayoutPanel37.TabIndex = 10
        '
        'txtFilterValue
        '
        Me.txtFilterValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFilterValue.Location = New System.Drawing.Point(161, 4)
        Me.txtFilterValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFilterValue.Name = "txtFilterValue"
        Me.txtFilterValue.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtFilterValue.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.txtFilterValue.Properties.Appearance.Options.UseBackColor = True
        Me.txtFilterValue.Properties.Appearance.Options.UseForeColor = True
        Me.txtFilterValue.Size = New System.Drawing.Size(72, 20)
        Me.txtFilterValue.TabIndex = 5
        '
        'cmbFilterKPI
        '
        Me.cmbFilterKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbFilterKPI.Location = New System.Drawing.Point(3, 3)
        Me.cmbFilterKPI.Name = "cmbFilterKPI"
        Me.cmbFilterKPI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFilterKPI.Size = New System.Drawing.Size(92, 20)
        Me.cmbFilterKPI.TabIndex = 6
        '
        'cmbFilterOp
        '
        Me.cmbFilterOp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbFilterOp.EditValue = "Select Item..."
        Me.cmbFilterOp.Location = New System.Drawing.Point(101, 3)
        Me.cmbFilterOp.Name = "cmbFilterOp"
        Me.cmbFilterOp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFilterOp.Properties.Items.AddRange(New Object() {"<", ">", "="})
        Me.cmbFilterOp.Size = New System.Drawing.Size(53, 20)
        Me.cmbFilterOp.TabIndex = 7
        '
        'ExTableLayoutPanel38
        '
        Me.ExTableLayoutPanel38.ColumnCount = 3
        Me.ExTableLayoutPanel38.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel38.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.ExTableLayoutPanel38.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.ExTableLayoutPanel38.Controls.Add(Me.btnFilterAdd, 0, 0)
        Me.ExTableLayoutPanel38.Controls.Add(Me.btnFilterDel, 0, 0)
        Me.ExTableLayoutPanel38.Controls.Add(Me.Label1, 0, 0)
        Me.ExTableLayoutPanel38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel38.Location = New System.Drawing.Point(4, 96)
        Me.ExTableLayoutPanel38.Margin = New System.Windows.Forms.Padding(4)
        Me.ExTableLayoutPanel38.Name = "ExTableLayoutPanel38"
        Me.ExTableLayoutPanel38.RowCount = 1
        Me.ExTableLayoutPanel38.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel38.Size = New System.Drawing.Size(235, 30)
        Me.ExTableLayoutPanel38.TabIndex = 11
        '
        'btnFilterAdd
        '
        Me.btnFilterAdd.Location = New System.Drawing.Point(161, 4)
        Me.btnFilterAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFilterAdd.Name = "btnFilterAdd"
        Me.btnFilterAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnFilterAdd.TabIndex = 10
        Me.btnFilterAdd.Text = "Add"
        '
        'btnFilterDel
        '
        Me.btnFilterDel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterDel.Enabled = False
        Me.btnFilterDel.Location = New System.Drawing.Point(86, 4)
        Me.btnFilterDel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFilterDel.Name = "btnFilterDel"
        Me.btnFilterDel.Size = New System.Drawing.Size(67, 22)
        Me.btnFilterDel.TabIndex = 9
        Me.btnFilterDel.Text = "Delete"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 30)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Select KPI to filter"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tlvFilters
        '
        '
        '
        '
        Me.tlvFilters.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvFilters.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvFilters.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvFilters.ContentPanel.Name = ""
        Me.tlvFilters.ContentPanel.Size = New System.Drawing.Size(229, 206)
        Me.tlvFilters.ContentPanel.TabIndex = 3
        Me.tlvFilters.ContentPanel.TabStop = False
        Me.tlvFilters.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvFilters.Footer = False
        Me.tlvFilters.Location = New System.Drawing.Point(4, 169)
        Me.tlvFilters.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvFilters.Name = "tlvFilters"
        ListItemColorStyle1.TextColor = System.Drawing.Color.DimGray
        Me.tlvFilters.NormalNodeStyle = ListItemColorStyle1
        ListItemColorStyle2.TextColor = System.Drawing.Color.DimGray
        Me.tlvFilters.NormalSubItemStyle = ListItemColorStyle2
        Me.tlvFilters.Size = New System.Drawing.Size(235, 212)
        Me.tlvFilters.TabIndex = 9
        Me.tlvFilters.Text = "TreeListView1"
        '
        'AccordionContentContainer1
        '
        Me.AccordionContentContainer1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.AccordionContentContainer1.Appearance.Options.UseBackColor = True
        Me.AccordionContentContainer1.Controls.Add(Me.tvObjectTree)
        Me.AccordionContentContainer1.Name = "AccordionContentContainer1"
        Me.AccordionContentContainer1.Size = New System.Drawing.Size(243, 260)
        Me.AccordionContentContainer1.TabIndex = 1
        '
        'tvObjectTree
        '
        Me.tvObjectTree.CheckBoxes = True
        Me.tvObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvObjectTree.ForeColor = System.Drawing.Color.DimGray
        Me.tvObjectTree.Location = New System.Drawing.Point(0, 0)
        Me.tvObjectTree.Margin = New System.Windows.Forms.Padding(4, 4, 4, 25)
        Me.tvObjectTree.Name = "tvObjectTree"
        Me.tvObjectTree.ShowNodeToolTips = True
        Me.tvObjectTree.Size = New System.Drawing.Size(243, 260)
        Me.tvObjectTree.TabIndex = 8
        '
        'aceObjectTree
        '
        Me.aceObjectTree.ContentContainer = Me.AccordionContentContainer1
        Me.aceObjectTree.Name = "aceObjectTree"
        Me.aceObjectTree.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.aceObjectTree.Text = "Object Tree"
        '
        'aceFilters
        '
        Me.aceFilters.ContentContainer = Me.AccordionContentContainer2
        Me.aceFilters.Name = "aceFilters"
        Me.aceFilters.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        Me.aceFilters.Text = "Filters"
        '
        'cmbReport
        '
        Me.cmbReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbReport.Location = New System.Drawing.Point(3, 28)
        Me.cmbReport.Name = "cmbReport"
        Me.cmbReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbReport.Size = New System.Drawing.Size(260, 20)
        Me.cmbReport.TabIndex = 16
        '
        'cmbTechnology
        '
        Me.cmbTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTechnology.Location = New System.Drawing.Point(3, 80)
        Me.cmbTechnology.Name = "cmbTechnology"
        Me.cmbTechnology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnology.Size = New System.Drawing.Size(260, 20)
        Me.cmbTechnology.TabIndex = 17
        '
        'cmbVendor
        '
        Me.cmbVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbVendor.Location = New System.Drawing.Point(3, 132)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbVendor.Size = New System.Drawing.Size(260, 20)
        Me.cmbVendor.TabIndex = 18
        '
        'cmbTargetObject
        '
        Me.cmbTargetObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTargetObject.Location = New System.Drawing.Point(3, 184)
        Me.cmbTargetObject.Name = "cmbTargetObject"
        Me.cmbTargetObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTargetObject.Size = New System.Drawing.Size(260, 20)
        Me.cmbTargetObject.TabIndex = 19
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 1
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.spltConAllChartGrid, 0, 0)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 2
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6.0!))
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(974, 738)
        Me.ExTableLayoutPanel1.TabIndex = 9
        '
        'spltConAllChartGrid
        '
        Me.spltConAllChartGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spltConAllChartGrid.Location = New System.Drawing.Point(4, 4)
        Me.spltConAllChartGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.spltConAllChartGrid.Name = "spltConAllChartGrid"
        Me.spltConAllChartGrid.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'spltConAllChartGrid.Panel1
        '
        Me.spltConAllChartGrid.Panel1.Controls.Add(Me.vtabICM)
        '
        'spltConAllChartGrid.Panel2
        '
        Me.spltConAllChartGrid.Panel2.Controls.Add(Me.VGroupBox8)
        Me.spltConAllChartGrid.Size = New System.Drawing.Size(966, 724)
        Me.spltConAllChartGrid.SplitterDistance = 415
        Me.spltConAllChartGrid.SplitterWidth = 5
        Me.spltConAllChartGrid.TabIndex = 9
        '
        'vtabICM
        '
        Me.vtabICM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vtabICM.Location = New System.Drawing.Point(0, 0)
        Me.vtabICM.Margin = New System.Windows.Forms.Padding(4)
        Me.vtabICM.Name = "vtabICM"
        Me.vtabICM.Padding = New System.Windows.Forms.Padding(0, 45, 0, 0)
        Me.vtabICM.SelectedTabPage = Me.vtpICMOverview
        Me.vtabICM.Size = New System.Drawing.Size(966, 415)
        Me.vtabICM.TabIndex = 5
        Me.vtabICM.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.vtpICMOverview})
        '
        'vtpICMOverview
        '
        Me.vtpICMOverview.Controls.Add(Me.ExTableLayoutPanel7)
        Me.vtpICMOverview.Margin = New System.Windows.Forms.Padding(4)
        Me.vtpICMOverview.Name = "vtpICMOverview"
        Me.vtpICMOverview.Size = New System.Drawing.Size(960, 387)
        Me.vtpICMOverview.Tag = "ICMOverview"
        Me.vtpICMOverview.Text = "Overview"
        '
        'ExTableLayoutPanel7
        '
        Me.ExTableLayoutPanel7.BackColor = System.Drawing.SystemColors.Control
        Me.ExTableLayoutPanel7.ColumnCount = 1
        Me.ExTableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.ExTableLayoutPanel7.Controls.Add(Me.chart_Overview, 0, 0)
        Me.ExTableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel7.Margin = New System.Windows.Forms.Padding(4)
        Me.ExTableLayoutPanel7.Name = "ExTableLayoutPanel7"
        Me.ExTableLayoutPanel7.RowCount = 1
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel7.Size = New System.Drawing.Size(960, 387)
        Me.ExTableLayoutPanel7.TabIndex = 3
		'
		'chart_Overview
		'
		Me.chart_Overview.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
		Me.chart_Overview.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
		Me.chart_Overview.AutoScroll = True
        Me.chart_Overview.Background.Color = System.Drawing.Color.White
        Annotation1.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation1.DynamicSize = True
        Annotation1.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation1.InteriorLine.Visible = True
        Annotation1.Line.Color = System.Drawing.Color.Gray
        Annotation1.Line.Visible = True
        Annotation1.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation1.Padding = 2
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(953, 380)
        Annotation1.Visible = True
        Me.chart_Overview.Box = Annotation1
        Me.chart_Overview.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.chart_Overview.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Me.chart_Overview.ChartArea.DefaultElement.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.chart_Overview.ChartArea.DefaultElement.DefaultSubValue.Line.Visible = True
        Me.chart_Overview.ChartArea.DefaultElement.DefaultSubValue.Visible = True
        Me.chart_Overview.ChartArea.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chart_Overview.ChartArea.DefaultElement.LegendEntry.DividerLine.Visible = True
        Me.chart_Overview.ChartArea.DefaultElement.Outline.Visible = True
        Me.chart_Overview.ChartArea.DefaultElement.SmartLabel.Color = System.Drawing.Color.Empty
        Me.chart_Overview.ChartArea.DefaultElement.SmartLabel.Line.Visible = True
        Me.chart_Overview.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.chart_Overview.ChartArea.InteriorLine.Visible = True
        Me.chart_Overview.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.chart_Overview.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.chart_Overview.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.chart_Overview.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chart_Overview.ChartArea.LegendBox.DefaultEntry.DividerLine.Visible = True
        Me.chart_Overview.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.chart_Overview.ChartArea.LegendBox.HeaderEntry.DividerLine.Visible = True
        Me.chart_Overview.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.chart_Overview.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.chart_Overview.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.chart_Overview.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.chart_Overview.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chart_Overview.ChartArea.LegendBox.InteriorLine.Visible = True
        Me.chart_Overview.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.chart_Overview.ChartArea.LegendBox.Line.Visible = True
        Me.chart_Overview.ChartArea.LegendBox.Padding = 4
        Me.chart_Overview.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.chart_Overview.ChartArea.LegendBox.Visible = True
        Me.chart_Overview.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.chart_Overview.ChartArea.Line.Visible = True
        Me.chart_Overview.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.chart_Overview.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.chart_Overview.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chart_Overview.ChartArea.TitleBox.InteriorLine.Visible = True
        Me.chart_Overview.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.chart_Overview.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.chart_Overview.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.chart_Overview.ChartArea.TitleBox.Line.Visible = True
        Me.chart_Overview.ChartArea.TitleBox.Visible = True
        Me.chart_Overview.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        Me.chart_Overview.ChartArea.XAxis.DefaultTick.GridLine.Visible = True
        Me.chart_Overview.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.chart_Overview.ChartArea.XAxis.DefaultTick.Line.Visible = True
        Me.chart_Overview.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chart_Overview.ChartArea.XAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.chart_Overview.ChartArea.XAxis.ScaleBreakLine.Visible = True
        Me.chart_Overview.ChartArea.XAxis.TickLabelSeparatorLine.Visible = True
        Me.chart_Overview.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chart_Overview.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chart_Overview.ChartArea.XAxis.ZeroTick.GridLine.Visible = True
        Me.chart_Overview.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.chart_Overview.ChartArea.XAxis.ZeroTick.Line.Visible = True
        Me.chart_Overview.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        Me.chart_Overview.ChartArea.YAxis.DefaultTick.GridLine.Visible = True
        Me.chart_Overview.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.chart_Overview.ChartArea.YAxis.DefaultTick.Line.Visible = True
        Me.chart_Overview.ChartArea.YAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.chart_Overview.ChartArea.YAxis.ScaleBreakLine.Visible = True
        Me.chart_Overview.ChartArea.YAxis.TickLabelSeparatorLine.Visible = True
        Me.chart_Overview.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chart_Overview.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chart_Overview.ChartArea.YAxis.ZeroTick.GridLine.Visible = True
        Me.chart_Overview.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.chart_Overview.ChartArea.YAxis.ZeroTick.Line.Visible = True
        Me.chart_Overview.ContextMenuStrip = Me.cms_OverviewChart
        Me.chart_Overview.DataGrid = Nothing
        Me.chart_Overview.DefaultElement.DefaultSubValue.Line.Visible = True
        Me.chart_Overview.DefaultElement.DefaultSubValue.Visible = True
        Me.chart_Overview.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chart_Overview.DefaultElement.LegendEntry.DividerLine.Visible = True
        Me.chart_Overview.DefaultElement.Outline.Visible = True
        Me.chart_Overview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chart_Overview.Location = New System.Drawing.Point(3, 3)
        Me.chart_Overview.MinimumSize = New System.Drawing.Size(133, 62)
        Me.chart_Overview.Name = "chart_Overview"
        Me.chart_Overview.NoDataLabel.Text = "No Data"
        Me.chart_Overview.ObjectChart = Label2
        Me.chart_Overview.Size = New System.Drawing.Size(954, 381)
        Me.chart_Overview.SmartLabelLine.Visible = True
        Me.chart_Overview.StartDateOfYear = New Date(CType(0, Long))
        Me.chart_Overview.TabIndex = 9
        Me.chart_Overview.TempDirectory = "C:\Users\Charul\AppData\Local\Temp\"
        '
        'cms_OverviewChart
        '
        Me.cms_OverviewChart.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_MapAllWithThematic, Me.tsmi_ShowHideOverviewForecast})
        Me.cms_OverviewChart.Name = "Map_ContextMenu"
        Me.cms_OverviewChart.Size = New System.Drawing.Size(255, 48)
        '
        'tsmi_MapAllWithThematic
        '
        Me.tsmi_MapAllWithThematic.Name = "tsmi_MapAllWithThematic"
        Me.tsmi_MapAllWithThematic.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_MapAllWithThematic.Text = "Map All with Thematic"
        '
        'tsmi_ShowHideOverviewForecast
        '
        Me.tsmi_ShowHideOverviewForecast.Name = "tsmi_ShowHideOverviewForecast"
        Me.tsmi_ShowHideOverviewForecast.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_ShowHideOverviewForecast.Text = "Show/Hide Overview for Selection"
        '
        'VGroupBox8
        '
        Me.VGroupBox8.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VGroupBox8.Appearance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VGroupBox8.Appearance.Options.UseBackColor = True
        Me.VGroupBox8.Appearance.Options.UseForeColor = True
        Me.VGroupBox8.Controls.Add(Me.ExTableLayoutPanel34)
        Me.VGroupBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VGroupBox8.Location = New System.Drawing.Point(0, 0)
        Me.VGroupBox8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 1)
        Me.VGroupBox8.Name = "VGroupBox8"
        Me.VGroupBox8.Padding = New System.Windows.Forms.Padding(4)
        Me.VGroupBox8.Size = New System.Drawing.Size(966, 304)
        Me.VGroupBox8.TabIndex = 8
        Me.VGroupBox8.Text = "Overview Forecast statistics :"
        '
        'ExTableLayoutPanel34
        '
        Me.ExTableLayoutPanel34.ColumnCount = 1
        Me.ExTableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel34.Controls.Add(Me.ExTableLayoutPanel4, 0, 2)
        Me.ExTableLayoutPanel34.Controls.Add(Me.VPanel1, 0, 1)
        Me.ExTableLayoutPanel34.Controls.Add(Me.ExTableLayoutPanel39, 0, 0)
        Me.ExTableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel34.Location = New System.Drawing.Point(6, 24)
        Me.ExTableLayoutPanel34.Margin = New System.Windows.Forms.Padding(4)
        Me.ExTableLayoutPanel34.Name = "ExTableLayoutPanel34"
        Me.ExTableLayoutPanel34.RowCount = 3
        Me.ExTableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.ExTableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123.0!))
        Me.ExTableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel34.Size = New System.Drawing.Size(954, 274)
        Me.ExTableLayoutPanel34.TabIndex = 2
        '
        'ExTableLayoutPanel4
        '
        Me.ExTableLayoutPanel4.ColumnCount = 1
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel4.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.ExTableLayoutPanel4.Controls.Add(Me.ExTableLayoutPanel5, 0, 0)
        Me.ExTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel4.Location = New System.Drawing.Point(3, 154)
        Me.ExTableLayoutPanel4.Name = "ExTableLayoutPanel4"
        Me.ExTableLayoutPanel4.RowCount = 2
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.ExTableLayoutPanel4.Size = New System.Drawing.Size(948, 117)
        Me.ExTableLayoutPanel4.TabIndex = 3
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 5
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.VLabel4, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnFeedbackSave, 4, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.chkApproved, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.lblMSG, 2, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txtComment, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 45)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(944, 70)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'VLabel4
        '
        Me.VLabel4.BackColor = System.Drawing.Color.Transparent
        Me.VLabel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel4.Location = New System.Drawing.Point(11, 4)
        Me.VLabel4.Margin = New System.Windows.Forms.Padding(4)
        Me.VLabel4.Name = "VLabel4"
        Me.VLabel4.Size = New System.Drawing.Size(125, 23)
        Me.VLabel4.TabIndex = 0
        Me.VLabel4.Text = "Comment :"
        '
        'btnFeedbackSave
        '
        Me.btnFeedbackSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnFeedbackSave.Location = New System.Drawing.Point(748, 4)
        Me.btnFeedbackSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFeedbackSave.Name = "btnFeedbackSave"
        Me.btnFeedbackSave.Size = New System.Drawing.Size(192, 23)
        Me.btnFeedbackSave.TabIndex = 1
        Me.btnFeedbackSave.Text = "Save Comment"
        '
        'chkApproved
        '
        Me.chkApproved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkApproved.Location = New System.Drawing.Point(628, 4)
        Me.chkApproved.Margin = New System.Windows.Forms.Padding(4)
        Me.chkApproved.Name = "chkApproved"
        Me.chkApproved.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.chkApproved.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.chkApproved.Properties.Appearance.Options.UseBackColor = True
        Me.chkApproved.Properties.Appearance.Options.UseFont = True
        Me.chkApproved.Properties.Caption = "Approved"
        Me.chkApproved.Size = New System.Drawing.Size(112, 23)
        Me.chkApproved.TabIndex = 4
        '
        'lblMSG
        '
        Me.lblMSG.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMSG.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.lblMSG.ForeColor = System.Drawing.Color.Maroon
        Me.lblMSG.Location = New System.Drawing.Point(144, 35)
        Me.lblMSG.Margin = New System.Windows.Forms.Padding(4)
        Me.lblMSG.Name = "lblMSG"
        Me.lblMSG.Size = New System.Drawing.Size(476, 31)
        Me.lblMSG.TabIndex = 15
        Me.lblMSG.Text = "Label"
        '
        'txtComment
        '
        Me.txtComment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtComment.Location = New System.Drawing.Point(143, 3)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(478, 25)
        Me.txtComment.TabIndex = 16
        '
        'ExTableLayoutPanel5
        '
        Me.ExTableLayoutPanel5.ColumnCount = 5
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.ExTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ExTableLayoutPanel5.Controls.Add(Me.VLabel11, 1, 0)
        Me.ExTableLayoutPanel5.Controls.Add(Me.lblRecommendation, 2, 0)
        Me.ExTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel5.Location = New System.Drawing.Point(2, 2)
        Me.ExTableLayoutPanel5.Margin = New System.Windows.Forms.Padding(2)
        Me.ExTableLayoutPanel5.Name = "ExTableLayoutPanel5"
        Me.ExTableLayoutPanel5.RowCount = 2
        Me.ExTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.ExTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel5.Size = New System.Drawing.Size(944, 39)
        Me.ExTableLayoutPanel5.TabIndex = 16
        '
        'VLabel11
        '
        Me.VLabel11.BackColor = System.Drawing.Color.Transparent
        Me.VLabel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.VLabel11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VLabel11.Location = New System.Drawing.Point(8, 4)
        Me.VLabel11.Margin = New System.Windows.Forms.Padding(1, 4, 4, 4)
        Me.VLabel11.Name = "VLabel11"
        Me.VLabel11.Size = New System.Drawing.Size(128, 29)
        Me.VLabel11.TabIndex = 17
        Me.VLabel11.Text = "Recommendation"
        '
        'lblRecommendation
        '
        Me.lblRecommendation.BackColor = System.Drawing.Color.Transparent
        Me.lblRecommendation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRecommendation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRecommendation.Location = New System.Drawing.Point(144, 4)
        Me.lblRecommendation.Margin = New System.Windows.Forms.Padding(4)
        Me.lblRecommendation.Name = "lblRecommendation"
        Me.lblRecommendation.Size = New System.Drawing.Size(476, 29)
        Me.lblRecommendation.TabIndex = 19
        Me.lblRecommendation.Text = "  "
        '
        'VPanel1
        '
        Me.VPanel1.AutoScroll = True
        Me.VPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VPanel1.Location = New System.Drawing.Point(4, 37)
        Me.VPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.VPanel1.Name = "VPanel1"
        Me.VPanel1.Size = New System.Drawing.Size(946, 110)
        Me.VPanel1.TabIndex = 17
        Me.VPanel1.Text = "VPanel1"
        '
        'ExTableLayoutPanel39
        '
        Me.ExTableLayoutPanel39.ColumnCount = 4
        Me.ExTableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7.0!))
        Me.ExTableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 667.0!))
        Me.ExTableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.ExTableLayoutPanel39.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel39.Controls.Add(Me.lblForcastStatistics, 1, 0)
        Me.ExTableLayoutPanel39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel39.Location = New System.Drawing.Point(3, 3)
        Me.ExTableLayoutPanel39.Name = "ExTableLayoutPanel39"
        Me.ExTableLayoutPanel39.RowCount = 1
        Me.ExTableLayoutPanel39.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel39.Size = New System.Drawing.Size(948, 27)
        Me.ExTableLayoutPanel39.TabIndex = 15
        '
        'lblForcastStatistics
        '
        Me.lblForcastStatistics.BackColor = System.Drawing.Color.Transparent
        Me.lblForcastStatistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblForcastStatistics.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblForcastStatistics.Location = New System.Drawing.Point(11, 4)
        Me.lblForcastStatistics.Margin = New System.Windows.Forms.Padding(4)
        Me.lblForcastStatistics.Name = "lblForcastStatistics"
        Me.lblForcastStatistics.Size = New System.Drawing.Size(659, 19)
        Me.lblForcastStatistics.TabIndex = 14
        Me.lblForcastStatistics.Text = "Overview Forecast statistics of clicked cell :"
        '
        'VGroupBox4
        '
        Me.VGroupBox4.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VGroupBox4.Appearance.Options.UseBackColor = True
        Me.VGroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.VGroupBox4.Name = "VGroupBox4"
        Me.VGroupBox4.Size = New System.Drawing.Size(200, 100)
        Me.VGroupBox4.TabIndex = 0
        '
        'cms_HistogramChart
        '
        Me.cms_HistogramChart.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_ObjectCount, Me.ToolStripSeparator4, Me.tsmi_sendAlltoConsoletee, Me.tsmi_sendconsoletree, Me.ToolStripSeparator3, Me.tsmi_SendToMap, Me.tsmi_SendToMapSelect, Me.ToolStripSeparator2, Me.tsmi_HideAndShowGrid, Me.tsmi_ShowHideForecastHistogramChart})
        Me.cms_HistogramChart.Name = "Map_ContextMenu"
        Me.cms_HistogramChart.Size = New System.Drawing.Size(255, 176)
        '
        'tsmi_ObjectCount
        '
        Me.tsmi_ObjectCount.Name = "tsmi_ObjectCount"
        Me.tsmi_ObjectCount.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_ObjectCount.Text = "Total Object :"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(251, 6)
        '
        'tsmi_sendAlltoConsoletee
        '
        Me.tsmi_sendAlltoConsoletee.Name = "tsmi_sendAlltoConsoletee"
        Me.tsmi_sendAlltoConsoletee.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_sendAlltoConsoletee.Text = "Send To Console - All"
        '
        'tsmi_sendconsoletree
        '
        Me.tsmi_sendconsoletree.Name = "tsmi_sendconsoletree"
        Me.tsmi_sendconsoletree.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_sendconsoletree.Text = "Send To Console - Selected"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(251, 6)
        '
        'tsmi_SendToMap
        '
        Me.tsmi_SendToMap.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_EnableVoronoi, Me.ToolStripSeparator1, Me.tsmi_SendToMapAllGraduatedTheme, Me.tsmi_SendToMapAllRangedTheme, Me.tsmi_UsingPieTheme, Me.tsmi_SendToMapAllGeoAggregation, Me.tsmi_SendToMapAllHeatMap, Me.tsmi_UsingPreconfigured})
        Me.tsmi_SendToMap.Name = "tsmi_SendToMap"
        Me.tsmi_SendToMap.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_SendToMap.Text = "Send To Map - All"
        '
        'tsmi_EnableVoronoi
        '
        Me.tsmi_EnableVoronoi.CheckOnClick = True
        Me.tsmi_EnableVoronoi.Name = "tsmi_EnableVoronoi"
        Me.tsmi_EnableVoronoi.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_EnableVoronoi.Text = "Enable Voronoi"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(199, 6)
        '
        'tsmi_SendToMapAllGraduatedTheme
        '
        Me.tsmi_SendToMapAllGraduatedTheme.Name = "tsmi_SendToMapAllGraduatedTheme"
        Me.tsmi_SendToMapAllGraduatedTheme.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_SendToMapAllGraduatedTheme.Text = "Using Graduated Theme"
        '
        'tsmi_SendToMapAllRangedTheme
        '
        Me.tsmi_SendToMapAllRangedTheme.Name = "tsmi_SendToMapAllRangedTheme"
        Me.tsmi_SendToMapAllRangedTheme.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_SendToMapAllRangedTheme.Text = "Using Ranged Theme"
        '
        'tsmi_UsingPieTheme
        '
        Me.tsmi_UsingPieTheme.Name = "tsmi_UsingPieTheme"
        Me.tsmi_UsingPieTheme.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_UsingPieTheme.Text = "Using PieTheme"
        '
        'tsmi_SendToMapAllGeoAggregation
        '
        Me.tsmi_SendToMapAllGeoAggregation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_SendToMapAllGeoAggregationFunction, Me.CirclePresentationToolStripMenuItem, Me.BufferPresentationToolStripMenuItem})
        Me.tsmi_SendToMapAllGeoAggregation.Name = "tsmi_SendToMapAllGeoAggregation"
        Me.tsmi_SendToMapAllGeoAggregation.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_SendToMapAllGeoAggregation.Text = "Using GeoAggregation"
        '
        'tsmi_SendToMapAllGeoAggregationFunction
        '
        Me.tsmi_SendToMapAllGeoAggregationFunction.Items.AddRange(New Object() {"SUM", "AVG", "COUNT", "MIN", "MAX"})
        Me.tsmi_SendToMapAllGeoAggregationFunction.Name = "tsmi_SendToMapAllGeoAggregationFunction"
        Me.tsmi_SendToMapAllGeoAggregationFunction.Size = New System.Drawing.Size(121, 23)
        '
        'CirclePresentationToolStripMenuItem
        '
        Me.CirclePresentationToolStripMenuItem.Checked = True
        Me.CirclePresentationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CirclePresentationToolStripMenuItem.Name = "CirclePresentationToolStripMenuItem"
        Me.CirclePresentationToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CirclePresentationToolStripMenuItem.Text = "Circle Presentation"
        '
        'BufferPresentationToolStripMenuItem
        '
        Me.BufferPresentationToolStripMenuItem.Name = "BufferPresentationToolStripMenuItem"
        Me.BufferPresentationToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.BufferPresentationToolStripMenuItem.Text = "Buffer Presentation"
        '
        'tsmi_SendToMapAllHeatMap
        '
        Me.tsmi_SendToMapAllHeatMap.Name = "tsmi_SendToMapAllHeatMap"
        Me.tsmi_SendToMapAllHeatMap.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_SendToMapAllHeatMap.Text = "Using HeatMap"
        '
        'tsmi_UsingPreconfigured
        '
        Me.tsmi_UsingPreconfigured.Name = "tsmi_UsingPreconfigured"
        Me.tsmi_UsingPreconfigured.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_UsingPreconfigured.Text = "Using Preconfigured"
        '
        'tsmi_SendToMapSelect
        '
        Me.tsmi_SendToMapSelect.Name = "tsmi_SendToMapSelect"
        Me.tsmi_SendToMapSelect.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_SendToMapSelect.Text = "Send To Map - Selected"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(251, 6)
        '
        'tsmi_HideAndShowGrid
        '
        Me.tsmi_HideAndShowGrid.Name = "tsmi_HideAndShowGrid"
        Me.tsmi_HideAndShowGrid.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_HideAndShowGrid.Text = "Hide/Show Grid"
        '
        'tsmi_ShowHideForecastHistogramChart
        '
        Me.tsmi_ShowHideForecastHistogramChart.Name = "tsmi_ShowHideForecastHistogramChart"
        Me.tsmi_ShowHideForecastHistogramChart.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_ShowHideForecastHistogramChart.Text = "Show/Hide Overview for Selection"
        '
        'tsmi_SendToMapSelectedGraduatedTheme
        '
        Me.tsmi_SendToMapSelectedGraduatedTheme.Name = "tsmi_SendToMapSelectedGraduatedTheme"
        Me.tsmi_SendToMapSelectedGraduatedTheme.Size = New System.Drawing.Size(202, 22)
        Me.tsmi_SendToMapSelectedGraduatedTheme.Text = "Using Graduated Theme"
        '
        'tsmi_SendToMapSelectedGeoAggregationFunction
        '
        Me.tsmi_SendToMapSelectedGeoAggregationFunction.Items.AddRange(New Object() {"SUM", "AVG", "COUNT", "MIN", "MAX"})
        Me.tsmi_SendToMapSelectedGeoAggregationFunction.Name = "tsmi_SendToMapSelectedGeoAggregationFunction"
        Me.tsmi_SendToMapSelectedGeoAggregationFunction.Size = New System.Drawing.Size(121, 23)
        '
        'tsmi_HeatMap
        '
        Me.tsmi_HeatMap.Name = "tsmi_HeatMap"
        Me.tsmi_HeatMap.Size = New System.Drawing.Size(250, 22)
        Me.tsmi_HeatMap.Text = "Heat Map"
        '
        'cms_SubCategoryChart
        '
        Me.cms_SubCategoryChart.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_HideAndShowGridSubCategory, Me.tsmi_ShowHideForecastSubCategoryChart})
        Me.cms_SubCategoryChart.Name = "Map_ContextMenu"
        Me.cms_SubCategoryChart.Size = New System.Drawing.Size(255, 48)
        '
        'tsmi_HideAndShowGridSubCategory
        '
        Me.tsmi_HideAndShowGridSubCategory.Name = "tsmi_HideAndShowGridSubCategory"
        Me.tsmi_HideAndShowGridSubCategory.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_HideAndShowGridSubCategory.Text = "Hide/Show Grid"
        '
        'tsmi_ShowHideForecastSubCategoryChart
        '
        Me.tsmi_ShowHideForecastSubCategoryChart.Name = "tsmi_ShowHideForecastSubCategoryChart"
        Me.tsmi_ShowHideForecastSubCategoryChart.Size = New System.Drawing.Size(254, 22)
        Me.tsmi_ShowHideForecastSubCategoryChart.Text = "Show/Hide Overview for Selection"
        '
        'Content
        '
        Me.Content.BackColor = System.Drawing.Color.Transparent
        Me.Content.Location = New System.Drawing.Point(1, 1)
        Me.Content.Name = "Content"
        Me.Content.Size = New System.Drawing.Size(0, 0)
        Me.Content.TabIndex = 3
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 1
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel14.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 2
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(200, 100)
        Me.TableLayoutPanel14.TabIndex = 0
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 3
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.VLabel13, 0, 3)
        Me.TableLayoutPanel15.Controls.Add(Me.VLabel15, 0, 1)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 4
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(219, 59)
        Me.TableLayoutPanel15.TabIndex = 1
        '
        'VLabel13
        '
        Me.VLabel13.BackColor = System.Drawing.Color.Transparent
        Me.VLabel13.Dock = System.Windows.Forms.DockStyle.Left
        Me.VLabel13.Location = New System.Drawing.Point(3, 34)
        Me.VLabel13.Name = "VLabel13"
        Me.VLabel13.Size = New System.Drawing.Size(74, 25)
        Me.VLabel13.TabIndex = 2
        Me.VLabel13.Text = "End Date :"
        '
        'VLabel15
        '
        Me.VLabel15.BackColor = System.Drawing.Color.Transparent
        Me.VLabel15.Dock = System.Windows.Forms.DockStyle.Left
        Me.VLabel15.Location = New System.Drawing.Point(3, 5)
        Me.VLabel15.Name = "VLabel15"
        Me.VLabel15.Size = New System.Drawing.Size(74, 24)
        Me.VLabel15.TabIndex = 1
        Me.VLabel15.Text = "Start Date :"
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.ColumnCount = 2
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 1
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(200, 100)
        Me.TableLayoutPanel17.TabIndex = 0
        '
        'TableLayoutPanel18
        '
        Me.TableLayoutPanel18.ColumnCount = 2
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel18.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        Me.TableLayoutPanel18.RowCount = 1
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel18.Size = New System.Drawing.Size(200, 100)
        Me.TableLayoutPanel18.TabIndex = 0
        '
        'TableLayoutPanel19
        '
        Me.TableLayoutPanel19.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TableLayoutPanel19.ColumnCount = 2
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel19.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel19.Name = "TableLayoutPanel19"
        Me.TableLayoutPanel19.RowCount = 1
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(200, 100)
        Me.TableLayoutPanel19.TabIndex = 0
        '
        'frmICM
        '
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1253, 742)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1269, 726)
        Me.Name = "frmICM"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ICM"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.VGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VGroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        CType(Me.txtSearchObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AccordionControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AccordionControl1.ResumeLayout(False)
        Me.AccordionContentContainer2.ResumeLayout(False)
        Me.ExTableLayoutPanel35.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.chkFilterCriteriaCombine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel36.ResumeLayout(False)
        CType(Me.txtXY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel37.ResumeLayout(False)
        CType(Me.txtFilterValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFilterKPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFilterOp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel38.ResumeLayout(False)
        Me.ExTableLayoutPanel38.PerformLayout()
        CType(Me.tlvFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AccordionContentContainer1.ResumeLayout(False)
        CType(Me.cmbReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        Me.spltConAllChartGrid.Panel1.ResumeLayout(False)
        Me.spltConAllChartGrid.Panel2.ResumeLayout(False)
        CType(Me.spltConAllChartGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spltConAllChartGrid.ResumeLayout(False)
        CType(Me.vtabICM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vtabICM.ResumeLayout(False)
        Me.vtpICMOverview.ResumeLayout(False)
        Me.ExTableLayoutPanel7.ResumeLayout(False)
        CType(Me.chart_Overview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cms_OverviewChart.ResumeLayout(False)
        CType(Me.VGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VGroupBox8.ResumeLayout(False)
        Me.ExTableLayoutPanel34.ResumeLayout(False)
        Me.ExTableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.chkApproved.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExTableLayoutPanel5.ResumeLayout(False)
        Me.ExTableLayoutPanel39.ResumeLayout(False)
        CType(Me.VGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cms_HistogramChart.ResumeLayout(False)
        Me.cms_SubCategoryChart.ResumeLayout(False)
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents VGroupBox1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel1 As System.Windows.Forms.Label
    Friend WithEvents VLabel5 As System.Windows.Forms.Label
    Friend WithEvents VLabel7 As System.Windows.Forms.Label
    Friend WithEvents VGroupBox4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel14 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel15 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel13 As System.Windows.Forms.Label
    Friend WithEvents VLabel15 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel17 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel18 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel19 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ExTableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vtabICM As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents vtpICMOverview As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ExTableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chart_Overview As dotnetCHARTING.WinForms.Chart
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel4 As System.Windows.Forms.Label
    Friend WithEvents btnFeedbackSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkApproved As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents VGroupBox8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ExTableLayoutPanel34 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblForcastStatistics As System.Windows.Forms.Label
    Friend WithEvents tvObjectTree As System.Windows.Forms.TreeView
    Friend WithEvents ExTableLayoutPanel35 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ExTableLayoutPanel36 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel12 As System.Windows.Forms.Label
    Friend WithEvents VLabel14 As System.Windows.Forms.Label
    Friend WithEvents txtXY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tlvFilters As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents ExTableLayoutPanel37 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtFilterValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ExTableLayoutPanel38 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnFilterAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ExTableLayoutPanel39 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel8 As System.Windows.Forms.Label
    Friend WithEvents VLabel16 As System.Windows.Forms.Label
    Friend WithEvents lblMSG As System.Windows.Forms.Label
    Friend WithEvents cms_HistogramChart As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_ObjectCount As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_sendconsoletree As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_sendAlltoConsoletee As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_SendToMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_HideAndShowGrid As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cms_SubCategoryChart As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_HideAndShowGridSubCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cms_OverviewChart As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_MapAllWithThematic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VLabel11 As System.Windows.Forms.Label
    Friend WithEvents ExTableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ExTableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents spltConAllChartGrid As System.Windows.Forms.SplitContainer
    Friend WithEvents lblRecommendation As System.Windows.Forms.Label
    Friend WithEvents tsmi_ShowHideOverviewForecast As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_ShowHideForecastSubCategoryChart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_ShowHideForecastHistogramChart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VPanel1 As System.Windows.Forms.Panel
    Friend WithEvents Content As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel19 As System.Windows.Forms.Label
    Friend WithEvents chkFilterCriteriaCombine As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tsmi_HeatMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_SendToMapAllGraduatedTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_SendToMapAllGeoAggregation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_SendToMapAllHeatMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_SendToMapSelect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_SendToMapSelectedGraduatedTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_SendToMapSelectedGeoAggregationFunction As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_SendToMapAllGeoAggregationFunction As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tsmi_SendToMapAllRangedTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CirclePresentationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BufferPresentationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_UsingPreconfigured As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_EnableVoronoi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_UsingPieTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AccordionControl1 As DevExpress.XtraBars.Navigation.AccordionControl
    Friend WithEvents AccordionContentContainer1 As DevExpress.XtraBars.Navigation.AccordionContentContainer
    Friend WithEvents AccordionContentContainer2 As DevExpress.XtraBars.Navigation.AccordionContentContainer
    Friend WithEvents aceObjectTree As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents aceFilters As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents cmbReport As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbFilterKPI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbFilterOp As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbVendor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTargetObject As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtComment As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents cmbTemplate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtSearchObject As DevExpress.XtraEditors.ButtonEdit
End Class
