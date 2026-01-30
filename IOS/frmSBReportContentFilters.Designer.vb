<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSBReportContentFilters
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
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New IOS.Library.ExTableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.flp_DimensionsResult = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanel2 = New IOS.Library.ExTableLayoutPanel()
        Me.vGBox_FilterStatementResult = New DevExpress.XtraEditors.GroupControl()
        Me.exTLP_FilterStatmentResult = New IOS.Library.ExTableLayoutPanel()
        Me.exTLP_FilterHeader = New IOS.Library.ExTableLayoutPanel()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.RichTextBoxResult = New DevExpress.XtraEditors.MemoEdit()
        Me.exTPL_DragdropAndFiltersResult = New IOS.Library.ExTableLayoutPanel()
        Me.lbl_ResultDrag = New DevExpress.XtraEditors.LabelControl()
        Me.tlp_ReportContentFilterResult = New IOS.Library.ExTableLayoutPanel()
        Me.TableLayoutPanel3 = New IOS.Library.ExTableLayoutPanel()
        Me.btn_CancelResult = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_ReportContentFilterCommitResult = New DevExpress.XtraEditors.SimpleButton()
        Me.lbl_MessageResult = New DevExpress.XtraEditors.LabelControl()
        Me.xtcReportFilter = New DevExpress.XtraTab.XtraTabControl()
        Me.xtbQuery = New DevExpress.XtraTab.XtraTabPage()
        Me.ExTableLayoutPanel1 = New IOS.Library.ExTableLayoutPanel()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.flp_DimensionsQuery = New System.Windows.Forms.FlowLayoutPanel()
        Me.ExTableLayoutPanel2 = New IOS.Library.ExTableLayoutPanel()
        Me.vGBox_FilterStatementQuery = New DevExpress.XtraEditors.GroupControl()
        Me.exTLP_FilterStatmentQuery = New IOS.Library.ExTableLayoutPanel()
        Me.ExTableLayoutPanel4 = New IOS.Library.ExTableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.RichTextBoxQuery = New DevExpress.XtraEditors.MemoEdit()
        Me.exTPL_DragdropAndFiltersQuery = New IOS.Library.ExTableLayoutPanel()
        Me.lbl_QueryDrag = New DevExpress.XtraEditors.LabelControl()
        Me.tlp_ReportContentFilterQuery = New IOS.Library.ExTableLayoutPanel()
        Me.ExTableLayoutPanel7 = New IOS.Library.ExTableLayoutPanel()
        Me.btn_CancelQuery = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_ReportContentFilterCommitQuery = New DevExpress.XtraEditors.SimpleButton()
        Me.lbl_MessageQuery = New DevExpress.XtraEditors.LabelControl()
        Me.xtbResult = New DevExpress.XtraTab.XtraTabPage()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.vGBox_FilterStatementResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vGBox_FilterStatementResult.SuspendLayout()
        Me.exTLP_FilterStatmentResult.SuspendLayout()
        Me.exTLP_FilterHeader.SuspendLayout()
        CType(Me.RichTextBoxResult.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.exTPL_DragdropAndFiltersResult.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.xtcReportFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcReportFilter.SuspendLayout()
        Me.xtbQuery.SuspendLayout()
        Me.ExTableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.ExTableLayoutPanel2.SuspendLayout()
        CType(Me.vGBox_FilterStatementQuery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vGBox_FilterStatementQuery.SuspendLayout()
        Me.exTLP_FilterStatmentQuery.SuspendLayout()
        Me.ExTableLayoutPanel4.SuspendLayout()
        CType(Me.RichTextBoxQuery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.exTPL_DragdropAndFiltersQuery.SuspendLayout()
        Me.ExTableLayoutPanel7.SuspendLayout()
        Me.xtbResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 3000
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(778, 383)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.flp_DimensionsResult)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(227, 377)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Drag Dimensions"
        '
        'flp_DimensionsResult
        '
        Me.flp_DimensionsResult.AllowDrop = True
        Me.flp_DimensionsResult.AutoScroll = True
        Me.flp_DimensionsResult.AutoSize = True
        Me.flp_DimensionsResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flp_DimensionsResult.Location = New System.Drawing.Point(2, 20)
        Me.flp_DimensionsResult.Name = "flp_DimensionsResult"
        Me.flp_DimensionsResult.Size = New System.Drawing.Size(223, 355)
        Me.flp_DimensionsResult.TabIndex = 0
        Me.flp_DimensionsResult.Tag = "Y"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.vGBox_FilterStatementResult, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(236, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(539, 377)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'vGBox_FilterStatementResult
        '
        Me.vGBox_FilterStatementResult.Controls.Add(Me.exTLP_FilterStatmentResult)
        Me.vGBox_FilterStatementResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vGBox_FilterStatementResult.Location = New System.Drawing.Point(3, 3)
        Me.vGBox_FilterStatementResult.Name = "vGBox_FilterStatementResult"
        Me.vGBox_FilterStatementResult.Padding = New System.Windows.Forms.Padding(10)
        Me.vGBox_FilterStatementResult.Size = New System.Drawing.Size(533, 334)
        Me.vGBox_FilterStatementResult.TabIndex = 0
        Me.vGBox_FilterStatementResult.Text = "Filter Statement"
        '
        'exTLP_FilterStatmentResult
        '
        Me.exTLP_FilterStatmentResult.ColumnCount = 1
        Me.exTLP_FilterStatmentResult.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTLP_FilterStatmentResult.Controls.Add(Me.exTLP_FilterHeader, 0, 0)
        Me.exTLP_FilterStatmentResult.Controls.Add(Me.RichTextBoxResult, 0, 2)
        Me.exTLP_FilterStatmentResult.Controls.Add(Me.exTPL_DragdropAndFiltersResult, 0, 1)
        Me.exTLP_FilterStatmentResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.exTLP_FilterStatmentResult.Location = New System.Drawing.Point(12, 30)
        Me.exTLP_FilterStatmentResult.Name = "exTLP_FilterStatmentResult"
        Me.exTLP_FilterStatmentResult.RowCount = 3
        Me.exTLP_FilterStatmentResult.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTLP_FilterStatmentResult.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTLP_FilterStatmentResult.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.exTLP_FilterStatmentResult.Size = New System.Drawing.Size(509, 292)
        Me.exTLP_FilterStatmentResult.TabIndex = 0
        '
        'exTLP_FilterHeader
        '
        Me.exTLP_FilterHeader.BackColor = System.Drawing.Color.Silver
        Me.exTLP_FilterHeader.ColumnCount = 5
        Me.exTLP_FilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.72917!))
        Me.exTLP_FilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.10417!))
        Me.exTLP_FilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.3125!))
        Me.exTLP_FilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.10417!))
        Me.exTLP_FilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.57639!))
        Me.exTLP_FilterHeader.Controls.Add(Me.LabelControl5, 3, 0)
        Me.exTLP_FilterHeader.Controls.Add(Me.LabelControl4, 2, 0)
        Me.exTLP_FilterHeader.Controls.Add(Me.LabelControl3, 1, 0)
        Me.exTLP_FilterHeader.Controls.Add(Me.LabelControl2, 0, 0)
        Me.exTLP_FilterHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.exTLP_FilterHeader.Location = New System.Drawing.Point(1, 1)
        Me.exTLP_FilterHeader.Margin = New System.Windows.Forms.Padding(1)
        Me.exTLP_FilterHeader.Name = "exTLP_FilterHeader"
        Me.exTLP_FilterHeader.RowCount = 1
        Me.exTLP_FilterHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTLP_FilterHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTLP_FilterHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTLP_FilterHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTLP_FilterHeader.Size = New System.Drawing.Size(507, 28)
        Me.exTLP_FilterHeader.TabIndex = 0
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(338, 3)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(70, 24)
        Me.LabelControl5.TabIndex = 3
        Me.LabelControl5.Text = "Logical Link"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(235, 3)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(97, 24)
        Me.LabelControl4.TabIndex = 2
        Me.LabelControl4.Text = "Value"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(159, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(70, 24)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Operator"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(150, 24)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Dimension"
        '
        'RichTextBoxResult
        '
        Me.RichTextBoxResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxResult.Location = New System.Drawing.Point(3, 260)
        Me.RichTextBoxResult.Name = "RichTextBoxResult"
        Me.RichTextBoxResult.Size = New System.Drawing.Size(503, 29)
        Me.RichTextBoxResult.TabIndex = 1
        '
        'exTPL_DragdropAndFiltersResult
        '
        Me.exTPL_DragdropAndFiltersResult.ColumnCount = 1
        Me.exTPL_DragdropAndFiltersResult.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTPL_DragdropAndFiltersResult.Controls.Add(Me.lbl_ResultDrag, 0, 1)
        Me.exTPL_DragdropAndFiltersResult.Controls.Add(Me.tlp_ReportContentFilterResult, 0, 0)
        Me.exTPL_DragdropAndFiltersResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.exTPL_DragdropAndFiltersResult.Location = New System.Drawing.Point(3, 33)
        Me.exTPL_DragdropAndFiltersResult.Name = "exTPL_DragdropAndFiltersResult"
        Me.exTPL_DragdropAndFiltersResult.RowCount = 2
        Me.exTPL_DragdropAndFiltersResult.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTPL_DragdropAndFiltersResult.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.exTPL_DragdropAndFiltersResult.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.exTPL_DragdropAndFiltersResult.Size = New System.Drawing.Size(503, 221)
        Me.exTPL_DragdropAndFiltersResult.TabIndex = 2
        '
        'lbl_ResultDrag
        '
        Me.lbl_ResultDrag.AllowDrop = True
        Me.lbl_ResultDrag.Appearance.BackColor = System.Drawing.Color.White
        Me.lbl_ResultDrag.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ResultDrag.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lbl_ResultDrag.Appearance.Options.UseBackColor = True
        Me.lbl_ResultDrag.Appearance.Options.UseFont = True
        Me.lbl_ResultDrag.Appearance.Options.UseForeColor = True
        Me.lbl_ResultDrag.Appearance.Options.UseTextOptions = True
        Me.lbl_ResultDrag.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbl_ResultDrag.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbl_ResultDrag.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbl_ResultDrag.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_ResultDrag.Location = New System.Drawing.Point(3, 33)
        Me.lbl_ResultDrag.Name = "lbl_ResultDrag"
        Me.lbl_ResultDrag.Padding = New System.Windows.Forms.Padding(5)
        Me.lbl_ResultDrag.Size = New System.Drawing.Size(497, 25)
        Me.lbl_ResultDrag.TabIndex = 2
        Me.lbl_ResultDrag.Text = "< Drag Here >"
        '
        'tlp_ReportContentFilterResult
        '
        Me.tlp_ReportContentFilterResult.AutoScroll = True
        Me.tlp_ReportContentFilterResult.ColumnCount = 5
        Me.tlp_ReportContentFilterResult.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895!))
        Me.tlp_ReportContentFilterResult.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.tlp_ReportContentFilterResult.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.05263!))
        Me.tlp_ReportContentFilterResult.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.tlp_ReportContentFilterResult.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.tlp_ReportContentFilterResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_ReportContentFilterResult.Location = New System.Drawing.Point(1, 1)
        Me.tlp_ReportContentFilterResult.Margin = New System.Windows.Forms.Padding(1)
        Me.tlp_ReportContentFilterResult.Name = "tlp_ReportContentFilterResult"
        Me.tlp_ReportContentFilterResult.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.tlp_ReportContentFilterResult.RowCount = 1
        Me.tlp_ReportContentFilterResult.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlp_ReportContentFilterResult.Size = New System.Drawing.Size(501, 28)
        Me.tlp_ReportContentFilterResult.TabIndex = 3
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btn_CancelResult, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_ReportContentFilterCommitResult, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lbl_MessageResult, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 343)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(533, 31)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'btn_CancelResult
        '
        Me.btn_CancelResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_CancelResult.Location = New System.Drawing.Point(436, 3)
        Me.btn_CancelResult.Name = "btn_CancelResult"
        Me.btn_CancelResult.Size = New System.Drawing.Size(94, 25)
        Me.btn_CancelResult.TabIndex = 0
        Me.btn_CancelResult.Text = "Cancel"
        '
        'btn_ReportContentFilterCommitResult
        '
        Me.btn_ReportContentFilterCommitResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ReportContentFilterCommitResult.Location = New System.Drawing.Point(336, 3)
        Me.btn_ReportContentFilterCommitResult.Name = "btn_ReportContentFilterCommitResult"
        Me.btn_ReportContentFilterCommitResult.Size = New System.Drawing.Size(94, 25)
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
        Me.lbl_MessageResult.Size = New System.Drawing.Size(327, 25)
        Me.lbl_MessageResult.TabIndex = 2
        '
        'xtcReportFilter
        '
        Me.xtcReportFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcReportFilter.Location = New System.Drawing.Point(0, 0)
        Me.xtcReportFilter.Name = "xtcReportFilter"
        Me.xtcReportFilter.SelectedTabPage = Me.xtbQuery
        Me.xtcReportFilter.Size = New System.Drawing.Size(784, 411)
        Me.xtcReportFilter.TabIndex = 1
        Me.xtcReportFilter.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtbQuery, Me.xtbResult})
        '
        'xtbQuery
        '
        Me.xtbQuery.Controls.Add(Me.ExTableLayoutPanel1)
        Me.xtbQuery.Name = "xtbQuery"
        Me.xtbQuery.Size = New System.Drawing.Size(778, 383)
        Me.xtbQuery.Tag = "QUERY"
        Me.xtbQuery.Text = "In Query"
        '
        'ExTableLayoutPanel1
        '
        Me.ExTableLayoutPanel1.ColumnCount = 2
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.ExTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.ExTableLayoutPanel1.Controls.Add(Me.GroupControl2, 0, 0)
        Me.ExTableLayoutPanel1.Controls.Add(Me.ExTableLayoutPanel2, 1, 0)
        Me.ExTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExTableLayoutPanel1.Name = "ExTableLayoutPanel1"
        Me.ExTableLayoutPanel1.RowCount = 1
        Me.ExTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel1.Size = New System.Drawing.Size(778, 383)
        Me.ExTableLayoutPanel1.TabIndex = 1
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.flp_DimensionsQuery)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(227, 377)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Drag Dimensions"
        '
        'flp_DimensionsQuery
        '
        Me.flp_DimensionsQuery.AllowDrop = True
        Me.flp_DimensionsQuery.AutoScroll = True
        Me.flp_DimensionsQuery.AutoSize = True
        Me.flp_DimensionsQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flp_DimensionsQuery.Location = New System.Drawing.Point(2, 20)
        Me.flp_DimensionsQuery.Name = "flp_DimensionsQuery"
        Me.flp_DimensionsQuery.Size = New System.Drawing.Size(223, 355)
        Me.flp_DimensionsQuery.TabIndex = 0
        Me.flp_DimensionsQuery.Tag = "Y"
        '
        'ExTableLayoutPanel2
        '
        Me.ExTableLayoutPanel2.ColumnCount = 1
        Me.ExTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.Controls.Add(Me.vGBox_FilterStatementQuery, 0, 0)
        Me.ExTableLayoutPanel2.Controls.Add(Me.ExTableLayoutPanel7, 0, 1)
        Me.ExTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel2.Location = New System.Drawing.Point(236, 3)
        Me.ExTableLayoutPanel2.Name = "ExTableLayoutPanel2"
        Me.ExTableLayoutPanel2.RowCount = 2
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.ExTableLayoutPanel2.Size = New System.Drawing.Size(539, 377)
        Me.ExTableLayoutPanel2.TabIndex = 1
        '
        'vGBox_FilterStatementQuery
        '
        Me.vGBox_FilterStatementQuery.Controls.Add(Me.exTLP_FilterStatmentQuery)
        Me.vGBox_FilterStatementQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vGBox_FilterStatementQuery.Location = New System.Drawing.Point(3, 3)
        Me.vGBox_FilterStatementQuery.Name = "vGBox_FilterStatementQuery"
        Me.vGBox_FilterStatementQuery.Padding = New System.Windows.Forms.Padding(10)
        Me.vGBox_FilterStatementQuery.Size = New System.Drawing.Size(533, 334)
        Me.vGBox_FilterStatementQuery.TabIndex = 0
        Me.vGBox_FilterStatementQuery.Text = "Filter Statement"
        '
        'exTLP_FilterStatmentQuery
        '
        Me.exTLP_FilterStatmentQuery.ColumnCount = 1
        Me.exTLP_FilterStatmentQuery.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTLP_FilterStatmentQuery.Controls.Add(Me.ExTableLayoutPanel4, 0, 0)
        Me.exTLP_FilterStatmentQuery.Controls.Add(Me.RichTextBoxQuery, 0, 2)
        Me.exTLP_FilterStatmentQuery.Controls.Add(Me.exTPL_DragdropAndFiltersQuery, 0, 1)
        Me.exTLP_FilterStatmentQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.exTLP_FilterStatmentQuery.Location = New System.Drawing.Point(12, 30)
        Me.exTLP_FilterStatmentQuery.Name = "exTLP_FilterStatmentQuery"
        Me.exTLP_FilterStatmentQuery.RowCount = 3
        Me.exTLP_FilterStatmentQuery.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTLP_FilterStatmentQuery.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTLP_FilterStatmentQuery.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.exTLP_FilterStatmentQuery.Size = New System.Drawing.Size(509, 292)
        Me.exTLP_FilterStatmentQuery.TabIndex = 0
        '
        'ExTableLayoutPanel4
        '
        Me.ExTableLayoutPanel4.BackColor = System.Drawing.Color.Silver
        Me.ExTableLayoutPanel4.ColumnCount = 5
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.72917!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.10417!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.3125!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.10417!))
        Me.ExTableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.57639!))
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelControl1, 3, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelControl6, 2, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelControl7, 1, 0)
        Me.ExTableLayoutPanel4.Controls.Add(Me.LabelControl8, 0, 0)
        Me.ExTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel4.Location = New System.Drawing.Point(1, 1)
        Me.ExTableLayoutPanel4.Margin = New System.Windows.Forms.Padding(1)
        Me.ExTableLayoutPanel4.Name = "ExTableLayoutPanel4"
        Me.ExTableLayoutPanel4.RowCount = 1
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.ExTableLayoutPanel4.Size = New System.Drawing.Size(507, 28)
        Me.ExTableLayoutPanel4.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(338, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(70, 24)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Logical Link"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(235, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(97, 24)
        Me.LabelControl6.TabIndex = 2
        Me.LabelControl6.Text = "Value"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(159, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(70, 24)
        Me.LabelControl7.TabIndex = 1
        Me.LabelControl7.Text = "Operator"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Appearance.Options.UseTextOptions = True
        Me.LabelControl8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(150, 24)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "Dimension"
        '
        'RichTextBoxQuery
        '
        Me.RichTextBoxQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxQuery.Location = New System.Drawing.Point(3, 260)
        Me.RichTextBoxQuery.Name = "RichTextBoxQuery"
        Me.RichTextBoxQuery.Size = New System.Drawing.Size(503, 29)
        Me.RichTextBoxQuery.TabIndex = 1
        '
        'exTPL_DragdropAndFiltersQuery
        '
        Me.exTPL_DragdropAndFiltersQuery.ColumnCount = 1
        Me.exTPL_DragdropAndFiltersQuery.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTPL_DragdropAndFiltersQuery.Controls.Add(Me.lbl_QueryDrag, 0, 1)
        Me.exTPL_DragdropAndFiltersQuery.Controls.Add(Me.tlp_ReportContentFilterQuery, 0, 0)
        Me.exTPL_DragdropAndFiltersQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.exTPL_DragdropAndFiltersQuery.Location = New System.Drawing.Point(3, 33)
        Me.exTPL_DragdropAndFiltersQuery.Name = "exTPL_DragdropAndFiltersQuery"
        Me.exTPL_DragdropAndFiltersQuery.RowCount = 2
        Me.exTPL_DragdropAndFiltersQuery.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.exTPL_DragdropAndFiltersQuery.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.exTPL_DragdropAndFiltersQuery.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.exTPL_DragdropAndFiltersQuery.Size = New System.Drawing.Size(503, 221)
        Me.exTPL_DragdropAndFiltersQuery.TabIndex = 2
        '
        'lbl_QueryDrag
        '
        Me.lbl_QueryDrag.AllowDrop = True
        Me.lbl_QueryDrag.Appearance.BackColor = System.Drawing.Color.White
        Me.lbl_QueryDrag.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_QueryDrag.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lbl_QueryDrag.Appearance.Options.UseBackColor = True
        Me.lbl_QueryDrag.Appearance.Options.UseFont = True
        Me.lbl_QueryDrag.Appearance.Options.UseForeColor = True
        Me.lbl_QueryDrag.Appearance.Options.UseTextOptions = True
        Me.lbl_QueryDrag.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lbl_QueryDrag.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbl_QueryDrag.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbl_QueryDrag.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_QueryDrag.Location = New System.Drawing.Point(3, 33)
        Me.lbl_QueryDrag.Name = "lbl_QueryDrag"
        Me.lbl_QueryDrag.Padding = New System.Windows.Forms.Padding(5)
        Me.lbl_QueryDrag.Size = New System.Drawing.Size(497, 25)
        Me.lbl_QueryDrag.TabIndex = 2
        Me.lbl_QueryDrag.Text = "< Drag Here >"
        '
        'tlp_ReportContentFilterQuery
        '
        Me.tlp_ReportContentFilterQuery.AutoScroll = True
        Me.tlp_ReportContentFilterQuery.ColumnCount = 5
        Me.tlp_ReportContentFilterQuery.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895!))
        Me.tlp_ReportContentFilterQuery.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.tlp_ReportContentFilterQuery.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.05263!))
        Me.tlp_ReportContentFilterQuery.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.tlp_ReportContentFilterQuery.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.tlp_ReportContentFilterQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_ReportContentFilterQuery.Location = New System.Drawing.Point(1, 1)
        Me.tlp_ReportContentFilterQuery.Margin = New System.Windows.Forms.Padding(1)
        Me.tlp_ReportContentFilterQuery.Name = "tlp_ReportContentFilterQuery"
        Me.tlp_ReportContentFilterQuery.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.tlp_ReportContentFilterQuery.RowCount = 1
        Me.tlp_ReportContentFilterQuery.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlp_ReportContentFilterQuery.Size = New System.Drawing.Size(501, 28)
        Me.tlp_ReportContentFilterQuery.TabIndex = 3
        '
        'ExTableLayoutPanel7
        '
        Me.ExTableLayoutPanel7.ColumnCount = 3
        Me.ExTableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.ExTableLayoutPanel7.Controls.Add(Me.btn_CancelQuery, 2, 0)
        Me.ExTableLayoutPanel7.Controls.Add(Me.btn_ReportContentFilterCommitQuery, 1, 0)
        Me.ExTableLayoutPanel7.Controls.Add(Me.lbl_MessageQuery, 0, 0)
        Me.ExTableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExTableLayoutPanel7.Location = New System.Drawing.Point(3, 343)
        Me.ExTableLayoutPanel7.Name = "ExTableLayoutPanel7"
        Me.ExTableLayoutPanel7.RowCount = 1
        Me.ExTableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ExTableLayoutPanel7.Size = New System.Drawing.Size(533, 31)
        Me.ExTableLayoutPanel7.TabIndex = 1
        '
        'btn_CancelQuery
        '
        Me.btn_CancelQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_CancelQuery.Location = New System.Drawing.Point(436, 3)
        Me.btn_CancelQuery.Name = "btn_CancelQuery"
        Me.btn_CancelQuery.Size = New System.Drawing.Size(94, 25)
        Me.btn_CancelQuery.TabIndex = 0
        Me.btn_CancelQuery.Text = "Cancel"
        '
        'btn_ReportContentFilterCommitQuery
        '
        Me.btn_ReportContentFilterCommitQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ReportContentFilterCommitQuery.Location = New System.Drawing.Point(336, 3)
        Me.btn_ReportContentFilterCommitQuery.Name = "btn_ReportContentFilterCommitQuery"
        Me.btn_ReportContentFilterCommitQuery.Size = New System.Drawing.Size(94, 25)
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
        Me.lbl_MessageQuery.Size = New System.Drawing.Size(327, 25)
        Me.lbl_MessageQuery.TabIndex = 2
        '
        'xtbResult
        '
        Me.xtbResult.Controls.Add(Me.TableLayoutPanel1)
        Me.xtbResult.Name = "xtbResult"
        Me.xtbResult.Size = New System.Drawing.Size(778, 383)
        Me.xtbResult.Tag = "RESULT"
        Me.xtbResult.Text = "In Result"
        '
        'Timer2
        '
        Me.Timer2.Interval = 3000
        '
        'frmSBReportContentFilters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 411)
        Me.Controls.Add(Me.xtcReportFilter)
        Me.IconOptions.ShowIcon = False
        Me.MinimumSize = New System.Drawing.Size(794, 443)
        Me.Name = "frmSBReportContentFilters"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datamart: Filter"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.vGBox_FilterStatementResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vGBox_FilterStatementResult.ResumeLayout(False)
        Me.exTLP_FilterStatmentResult.ResumeLayout(False)
        Me.exTLP_FilterHeader.ResumeLayout(False)
        Me.exTLP_FilterHeader.PerformLayout()
        CType(Me.RichTextBoxResult.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.exTPL_DragdropAndFiltersResult.ResumeLayout(False)
        Me.exTPL_DragdropAndFiltersResult.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.xtcReportFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcReportFilter.ResumeLayout(False)
        Me.xtbQuery.ResumeLayout(False)
        Me.ExTableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.ExTableLayoutPanel2.ResumeLayout(False)
        CType(Me.vGBox_FilterStatementQuery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vGBox_FilterStatementQuery.ResumeLayout(False)
        Me.exTLP_FilterStatmentQuery.ResumeLayout(False)
        Me.ExTableLayoutPanel4.ResumeLayout(False)
        Me.ExTableLayoutPanel4.PerformLayout()
        CType(Me.RichTextBoxQuery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.exTPL_DragdropAndFiltersQuery.ResumeLayout(False)
        Me.exTPL_DragdropAndFiltersQuery.PerformLayout()
        Me.ExTableLayoutPanel7.ResumeLayout(False)
        Me.ExTableLayoutPanel7.PerformLayout()
        Me.xtbResult.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As IOS.Library.ExTableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flp_DimensionsResult As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel2 As IOS.Library.ExTableLayoutPanel
    Friend WithEvents vGBox_FilterStatementResult As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As IOS.Library.ExTableLayoutPanel
    Friend WithEvents btn_CancelResult As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_ReportContentFilterCommitResult As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents exTLP_FilterStatmentResult As IOS.Library.ExTableLayoutPanel
    Friend WithEvents exTLP_FilterHeader As IOS.Library.ExTableLayoutPanel
    Friend WithEvents RichTextBoxResult As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents exTPL_DragdropAndFiltersResult As IOS.Library.ExTableLayoutPanel
    Friend WithEvents lbl_ResultDrag As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_MessageResult As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents tlp_ReportContentFilterResult As IOS.Library.ExTableLayoutPanel
    Friend WithEvents xtcReportFilter As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtbQuery As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ExTableLayoutPanel1 As Library.ExTableLayoutPanel
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flp_DimensionsQuery As FlowLayoutPanel
    Friend WithEvents ExTableLayoutPanel2 As Library.ExTableLayoutPanel
    Friend WithEvents vGBox_FilterStatementQuery As DevExpress.XtraEditors.GroupControl
    Friend WithEvents exTLP_FilterStatmentQuery As Library.ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel4 As Library.ExTableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RichTextBoxQuery As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents exTPL_DragdropAndFiltersQuery As Library.ExTableLayoutPanel
    Friend WithEvents lbl_QueryDrag As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlp_ReportContentFilterQuery As Library.ExTableLayoutPanel
    Friend WithEvents ExTableLayoutPanel7 As Library.ExTableLayoutPanel
    Friend WithEvents btn_CancelQuery As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_ReportContentFilterCommitQuery As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lbl_MessageQuery As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtbResult As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Timer2 As Timer
End Class
