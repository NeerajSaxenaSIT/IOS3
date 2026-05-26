<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgDataMartContentFilter
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgDataMartContentFilter))
        Me.xtcReportFilter = New DevExpress.XtraTab.XtraTabControl()
        Me.xtbQuery = New DevExpress.XtraTab.XtraTabPage()
        Me.tlpQueryMain = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.flp_DimensionsQuery = New System.Windows.Forms.FlowLayoutPanel()
        Me.ExTableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GC_FilterStatementQuery = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.RichTextBoxQuery = New DevExpress.XtraEditors.MemoEdit()
        Me.gcQuery = New DevExpress.XtraGrid.GridControl()
        Me.gvQuery = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpQueryBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_CancelQuery = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_ReportContentFilterCommitQuery = New DevExpress.XtraEditors.SimpleButton()
        Me.lbl_MessageQuery = New DevExpress.XtraEditors.LabelControl()
        Me.xtbResult = New DevExpress.XtraTab.XtraTabPage()
        Me.tlpRresultMain = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.flp_DimensionsResult = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GC_FilterStatementResult = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.RichTextBoxResult = New DevExpress.XtraEditors.MemoEdit()
        Me.gcResult = New DevExpress.XtraGrid.GridControl()
        Me.gvResult = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpResultBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_CancelResult = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_ReportContentFilterCommitResult = New DevExpress.XtraEditors.SimpleButton()
        Me.lbl_MessageResult = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.xtcReportFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcReportFilter.SuspendLayout()
        Me.xtbQuery.SuspendLayout()
        Me.tlpQueryMain.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.ExTableLayoutPanel2.SuspendLayout()
        CType(Me.GC_FilterStatementQuery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GC_FilterStatementQuery.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.RichTextBoxQuery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcQuery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvQuery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpQueryBottom.SuspendLayout()
        Me.xtbResult.SuspendLayout()
        Me.tlpRresultMain.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.GC_FilterStatementResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GC_FilterStatementResult.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.RichTextBoxResult.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpResultBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'xtcReportFilter
        '
        Me.xtcReportFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcReportFilter.Location = New System.Drawing.Point(0, 0)
        Me.xtcReportFilter.Name = "xtcReportFilter"
        Me.xtcReportFilter.SelectedTabPage = Me.xtbQuery
        Me.xtcReportFilter.Size = New System.Drawing.Size(898, 468)
        Me.xtcReportFilter.TabIndex = 2
        Me.xtcReportFilter.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtbQuery, Me.xtbResult})
        '
        'xtbQuery
        '
        Me.xtbQuery.Controls.Add(Me.tlpQueryMain)
        Me.xtbQuery.Name = "xtbQuery"
        Me.xtbQuery.Size = New System.Drawing.Size(896, 443)
        Me.xtbQuery.Tag = "QUERY"
        Me.xtbQuery.Text = "In Query"
        '
        'tlpQueryMain
        '
        Me.tlpQueryMain.ColumnCount = 2
        Me.tlpQueryMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tlpQueryMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tlpQueryMain.Controls.Add(Me.GroupControl2, 0, 0)
        Me.tlpQueryMain.Controls.Add(Me.ExTableLayoutPanel2, 1, 0)
        Me.tlpQueryMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpQueryMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpQueryMain.Name = "tlpQueryMain"
        Me.tlpQueryMain.RowCount = 1
        Me.tlpQueryMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpQueryMain.Size = New System.Drawing.Size(896, 443)
        Me.tlpQueryMain.TabIndex = 1
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.flp_DimensionsQuery)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(262, 437)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Drag Dimensions"
        '
        'flp_DimensionsQuery
        '
        Me.flp_DimensionsQuery.AllowDrop = True
        Me.flp_DimensionsQuery.AutoScroll = True
        Me.flp_DimensionsQuery.AutoSize = True
        Me.flp_DimensionsQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flp_DimensionsQuery.Location = New System.Drawing.Point(2, 23)
        Me.flp_DimensionsQuery.Name = "flp_DimensionsQuery"
        Me.flp_DimensionsQuery.Size = New System.Drawing.Size(258, 412)
        Me.flp_DimensionsQuery.TabIndex = 0
        Me.flp_DimensionsQuery.Tag = "Y"
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.ColumnCount = 1
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.GC_FilterStatementQuery, 0, 0)
        Me.ExTableLayoutPanel2.Controls.Add(Me.tlpQueryBottom, 0, 1)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(269, 1)
        Me.ExTableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 2
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(626, 441)
        Me.ExTableLayoutPanel2.TabIndex = 1
        '
        'GC_FilterStatementQuery
        '
        Me.GC_FilterStatementQuery.Controls.Add(Me.TableLayoutPanel1)
        Me.GC_FilterStatementQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GC_FilterStatementQuery.Location = New System.Drawing.Point(3, 3)
        Me.GC_FilterStatementQuery.Name = "GC_FilterStatementQuery"
        Me.GC_FilterStatementQuery.Padding = New System.Windows.Forms.Padding(1)
        Me.GC_FilterStatementQuery.Size = New System.Drawing.Size(620, 398)
        Me.GC_FilterStatementQuery.TabIndex = 0
        Me.GC_FilterStatementQuery.Text = "Filter Statement"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.RichTextBoxQuery, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.gcQuery, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 24)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(614, 371)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'RichTextBoxQuery
        '
        Me.RichTextBoxQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxQuery.Location = New System.Drawing.Point(3, 318)
        Me.RichTextBoxQuery.Name = "RichTextBoxQuery"
        Me.RichTextBoxQuery.Size = New System.Drawing.Size(608, 50)
        Me.RichTextBoxQuery.TabIndex = 4
        '
        'gcQuery
        '
        Me.gcQuery.AllowDrop = True
        Me.gcQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcQuery.Location = New System.Drawing.Point(3, 3)
        Me.gcQuery.MainView = Me.gvQuery
        Me.gcQuery.Name = "gcQuery"
        Me.gcQuery.Size = New System.Drawing.Size(608, 309)
        Me.gcQuery.TabIndex = 3
        Me.gcQuery.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvQuery, Me.GridView1})
        '
        'gvQuery
        '
        Me.gvQuery.ActiveFilterEnabled = False
        Me.gvQuery.GridControl = Me.gcQuery
        Me.gvQuery.Name = "gvQuery"
        Me.gvQuery.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvQuery.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvQuery.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvQuery.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvQuery.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvQuery.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvQuery.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvQuery.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvQuery.OptionsCustomization.AllowSort = False
        Me.gvQuery.OptionsMenu.EnableColumnMenu = False
        Me.gvQuery.OptionsMenu.EnableFooterMenu = False
        Me.gvQuery.OptionsView.ShowGroupPanel = False
        Me.gvQuery.OptionsView.ShowIndicator = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcQuery
        Me.GridView1.Name = "GridView1"
        '
        'tlpQueryBottom
        '
        Me.tlpQueryBottom.ColumnCount = 3
        Me.tlpQueryBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpQueryBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpQueryBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpQueryBottom.Controls.Add(Me.btn_CancelQuery, 2, 0)
        Me.tlpQueryBottom.Controls.Add(Me.btn_ReportContentFilterCommitQuery, 1, 0)
        Me.tlpQueryBottom.Controls.Add(Me.lbl_MessageQuery, 0, 0)
        Me.tlpQueryBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpQueryBottom.Location = New System.Drawing.Point(1, 405)
        Me.tlpQueryBottom.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpQueryBottom.Name = "tlpQueryBottom"
        Me.tlpQueryBottom.RowCount = 1
        Me.tlpQueryBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpQueryBottom.Size = New System.Drawing.Size(624, 35)
        Me.tlpQueryBottom.TabIndex = 1
        '
        'btn_CancelQuery
        '
        Me.btn_CancelQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_CancelQuery.Location = New System.Drawing.Point(527, 3)
        Me.btn_CancelQuery.Name = "btn_CancelQuery"
        Me.btn_CancelQuery.Size = New System.Drawing.Size(94, 29)
        Me.btn_CancelQuery.TabIndex = 0
        Me.btn_CancelQuery.Text = "Cancel"
        '
        'btn_ReportContentFilterCommitQuery
        '
        Me.btn_ReportContentFilterCommitQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ReportContentFilterCommitQuery.Location = New System.Drawing.Point(427, 3)
        Me.btn_ReportContentFilterCommitQuery.Name = "btn_ReportContentFilterCommitQuery"
        Me.btn_ReportContentFilterCommitQuery.Size = New System.Drawing.Size(94, 29)
        Me.btn_ReportContentFilterCommitQuery.TabIndex = 1
        Me.btn_ReportContentFilterCommitQuery.Text = "Commit"
        '
        'lbl_MessageQuery
        '
        Me.lbl_MessageQuery.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbl_MessageQuery.Appearance.Options.UseFont = True
        Me.lbl_MessageQuery.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbl_MessageQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_MessageQuery.Location = New System.Drawing.Point(3, 3)
        Me.lbl_MessageQuery.Name = "lbl_MessageQuery"
        Me.lbl_MessageQuery.Size = New System.Drawing.Size(418, 29)
        Me.lbl_MessageQuery.TabIndex = 2
        '
        'xtbResult
        '
        Me.xtbResult.Controls.Add(Me.tlpRresultMain)
        Me.xtbResult.Name = "xtbResult"
        Me.xtbResult.Size = New System.Drawing.Size(896, 443)
        Me.xtbResult.Tag = "RESULT"
        Me.xtbResult.Text = "In Result"
        '
        'tlpRresultMain
        '
        Me.tlpRresultMain.ColumnCount = 2
        Me.tlpRresultMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tlpRresultMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tlpRresultMain.Controls.Add(Me.GroupControl1, 0, 0)
        Me.tlpRresultMain.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.tlpRresultMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpRresultMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpRresultMain.Name = "tlpRresultMain"
        Me.tlpRresultMain.RowCount = 1
        Me.tlpRresultMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRresultMain.Size = New System.Drawing.Size(896, 443)
        Me.tlpRresultMain.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.flp_DimensionsResult)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(262, 437)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Drag Dimensions"
        '
        'flp_DimensionsResult
        '
        Me.flp_DimensionsResult.AllowDrop = True
        Me.flp_DimensionsResult.AutoScroll = True
        Me.flp_DimensionsResult.AutoSize = True
        Me.flp_DimensionsResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flp_DimensionsResult.Location = New System.Drawing.Point(2, 23)
        Me.flp_DimensionsResult.Name = "flp_DimensionsResult"
        Me.flp_DimensionsResult.Size = New System.Drawing.Size(258, 412)
        Me.flp_DimensionsResult.TabIndex = 0
        Me.flp_DimensionsResult.Tag = "Y"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GC_FilterStatementResult, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.tlpResultBottom, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(269, 1)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(626, 441)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'GC_FilterStatementResult
        '
        Me.GC_FilterStatementResult.Controls.Add(Me.TableLayoutPanel3)
        Me.GC_FilterStatementResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GC_FilterStatementResult.Location = New System.Drawing.Point(3, 3)
        Me.GC_FilterStatementResult.Name = "GC_FilterStatementResult"
        Me.GC_FilterStatementResult.Padding = New System.Windows.Forms.Padding(1)
        Me.GC_FilterStatementResult.Size = New System.Drawing.Size(620, 398)
        Me.GC_FilterStatementResult.TabIndex = 0
        Me.GC_FilterStatementResult.Text = "Filter Statement"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.RichTextBoxResult, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.gcResult, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 24)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(614, 371)
        Me.TableLayoutPanel3.TabIndex = 4
        '
        'RichTextBoxResult
        '
        Me.RichTextBoxResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxResult.Location = New System.Drawing.Point(3, 318)
        Me.RichTextBoxResult.Name = "RichTextBoxResult"
        Me.RichTextBoxResult.Size = New System.Drawing.Size(608, 50)
        Me.RichTextBoxResult.TabIndex = 4
        '
        'gcResult
        '
        Me.gcResult.AllowDrop = True
        Me.gcResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcResult.Location = New System.Drawing.Point(3, 3)
        Me.gcResult.MainView = Me.gvResult
        Me.gcResult.Name = "gcResult"
        Me.gcResult.Size = New System.Drawing.Size(608, 309)
        Me.gcResult.TabIndex = 3
        Me.gcResult.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvResult, Me.GridView3})
        '
        'gvResult
        '
        Me.gvResult.ActiveFilterEnabled = False
        Me.gvResult.GridControl = Me.gcResult
        Me.gvResult.Name = "gvResult"
        Me.gvResult.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvResult.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvResult.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResult.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResult.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResult.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvResult.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResult.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvResult.OptionsCustomization.AllowSort = False
        Me.gvResult.OptionsMenu.EnableColumnMenu = False
        Me.gvResult.OptionsMenu.EnableFooterMenu = False
        Me.gvResult.OptionsView.ShowGroupPanel = False
        Me.gvResult.OptionsView.ShowIndicator = False
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.gcResult
        Me.GridView3.Name = "GridView3"
        '
        'tlpResultBottom
        '
        Me.tlpResultBottom.ColumnCount = 3
        Me.tlpResultBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpResultBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpResultBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpResultBottom.Controls.Add(Me.btn_CancelResult, 2, 0)
        Me.tlpResultBottom.Controls.Add(Me.btn_ReportContentFilterCommitResult, 1, 0)
        Me.tlpResultBottom.Controls.Add(Me.lbl_MessageResult, 0, 0)
        Me.tlpResultBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpResultBottom.Location = New System.Drawing.Point(1, 405)
        Me.tlpResultBottom.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpResultBottom.Name = "tlpResultBottom"
        Me.tlpResultBottom.RowCount = 1
        Me.tlpResultBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpResultBottom.Size = New System.Drawing.Size(624, 35)
        Me.tlpResultBottom.TabIndex = 1
        '
        'btn_CancelResult
        '
        Me.btn_CancelResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_CancelResult.Location = New System.Drawing.Point(527, 3)
        Me.btn_CancelResult.Name = "btn_CancelResult"
        Me.btn_CancelResult.Size = New System.Drawing.Size(94, 29)
        Me.btn_CancelResult.TabIndex = 0
        Me.btn_CancelResult.Text = "Cancel"
        '
        'btn_ReportContentFilterCommitResult
        '
        Me.btn_ReportContentFilterCommitResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ReportContentFilterCommitResult.Location = New System.Drawing.Point(427, 3)
        Me.btn_ReportContentFilterCommitResult.Name = "btn_ReportContentFilterCommitResult"
        Me.btn_ReportContentFilterCommitResult.Size = New System.Drawing.Size(94, 29)
        Me.btn_ReportContentFilterCommitResult.TabIndex = 1
        Me.btn_ReportContentFilterCommitResult.Text = "Commit"
        '
        'lbl_MessageResult
        '
        Me.lbl_MessageResult.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbl_MessageResult.Appearance.Options.UseFont = True
        Me.lbl_MessageResult.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbl_MessageResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_MessageResult.Location = New System.Drawing.Point(3, 3)
        Me.lbl_MessageResult.Name = "lbl_MessageResult"
        Me.lbl_MessageResult.Size = New System.Drawing.Size(418, 29)
        Me.lbl_MessageResult.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Interval = 3000
        '
        'Timer2
        '
        Me.Timer2.Interval = 3000
        '
        'dlgDataMartContentFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 468)
        Me.Controls.Add(Me.xtcReportFilter)
        Me.IconOptions.Icon = CType(resources.GetObject("dlgDataMartContentFilter.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 500)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 500)
        Me.Name = "dlgDataMartContentFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DataMart: Filter"
        CType(Me.xtcReportFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcReportFilter.ResumeLayout(False)
        Me.xtbQuery.ResumeLayout(False)
        Me.tlpQueryMain.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        CType(Me.GC_FilterStatementQuery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GC_FilterStatementQuery.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.RichTextBoxQuery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcQuery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvQuery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpQueryBottom.ResumeLayout(False)
        Me.tlpQueryBottom.PerformLayout()
        Me.xtbResult.ResumeLayout(False)
        Me.tlpRresultMain.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.GC_FilterStatementResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GC_FilterStatementResult.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.RichTextBoxResult.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpResultBottom.ResumeLayout(False)
        Me.tlpResultBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents xtcReportFilter As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtbQuery As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tlpQueryMain As TableLayoutPanel
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flp_DimensionsQuery As FlowLayoutPanel
    Friend WithEvents ExTableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GC_FilterStatementQuery As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpQueryBottom As TableLayoutPanel
    Friend WithEvents btn_CancelQuery As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_ReportContentFilterCommitQuery As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lbl_MessageQuery As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtbResult As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tlpRresultMain As TableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flp_DimensionsResult As FlowLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GC_FilterStatementResult As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpResultBottom As TableLayoutPanel
    Friend WithEvents btn_CancelResult As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_ReportContentFilterCommitResult As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lbl_MessageResult As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcQuery As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvQuery As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcResult As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvResult As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents RichTextBoxQuery As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents RichTextBoxResult As DevExpress.XtraEditors.MemoEdit
End Class
