<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReportEdit
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportEdit))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.sccRepEditor = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccReportTree = New DevExpress.XtraEditors.SplitContainerControl()
        Me.tlvReports = New DevExpress.XtraTreeList.TreeList()
        Me.cmsReport = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_ReportSlideAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_txtReportSlideAdd = New System.Windows.Forms.ToolStripTextBox()
        Me.tsmi_ReportRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ReportDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_ReportObjects = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjectAddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjectRemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjectMoveUpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjectMoveDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.tsmi_ReportRunCurrent = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ReportRunConfigured = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ReportLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ReportCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmt_SlideRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmt_SlideDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_SlideMoveUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SlideMoveDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_SlideObjectAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmitxt_NewTextbox = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_ObjectChartMoveUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ObjectChartMoveDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ObjectRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ObjectChartDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.gcReportHistory = New DevExpress.XtraGrid.GridControl()
        Me.gvReportHistory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpProperties = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpPropButtonsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSaveStyle = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCreateStyle = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpPropButtonsBotttom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.btnApplyStyle = New DevExpress.XtraEditors.SimpleButton()
        Me.propertyGridreport = New System.Windows.Forms.PropertyGrid()
        Me.VLabel3 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpStyleName = New System.Windows.Forms.TableLayoutPanel()
        Me.lblStyleObject = New DevExpress.XtraEditors.LabelControl()
        Me.cmbStyleName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblMSG = New DevExpress.XtraEditors.LabelControl()
        Me.sccTop = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcKPIName = New DevExpress.XtraEditors.GroupControl()
        Me.tlpReportGroup = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.VLabel2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbReportGroup = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtSearchReport = New DevExpress.XtraEditors.ButtonEdit()
        Me.VGroupBox1 = New DevExpress.XtraEditors.GroupControl()
        Me.tlpCreateReport = New System.Windows.Forms.TableLayoutPanel()
        Me.btnReportAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.sccRepEditor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccRepEditor.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccRepEditor.Panel1.SuspendLayout()
        CType(Me.sccRepEditor.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccRepEditor.Panel2.SuspendLayout()
        Me.sccRepEditor.SuspendLayout()
        CType(Me.sccReportTree, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccReportTree.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccReportTree.Panel1.SuspendLayout()
        CType(Me.sccReportTree.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccReportTree.Panel2.SuspendLayout()
        Me.sccReportTree.SuspendLayout()
        CType(Me.tlvReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsReport.SuspendLayout()
        CType(Me.gcReportHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReportHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpProperties.SuspendLayout()
        Me.tlpPropButtonsTop.SuspendLayout()
        Me.tlpPropButtonsBotttom.SuspendLayout()
        Me.tlpStyleName.SuspendLayout()
        CType(Me.cmbStyleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccTop.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTop.Panel1.SuspendLayout()
        CType(Me.sccTop.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTop.Panel2.SuspendLayout()
        Me.sccTop.SuspendLayout()
        CType(Me.gcKPIName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcKPIName.SuspendLayout()
        Me.tlpReportGroup.SuspendLayout()
        CType(Me.cmbReportGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VGroupBox1.SuspendLayout()
        Me.tlpCreateReport.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.BackColor = System.Drawing.Color.Transparent
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.sccRepEditor, 0, 1)
        Me.tlpMain.Controls.Add(Me.lblMSG, 0, 2)
        Me.tlpMain.Controls.Add(Me.sccTop, 0, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpMain.Size = New System.Drawing.Size(1305, 900)
        Me.tlpMain.TabIndex = 5
        '
        'sccRepEditor
        '
        Me.sccRepEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccRepEditor.Location = New System.Drawing.Point(4, 79)
        Me.sccRepEditor.Margin = New System.Windows.Forms.Padding(4)
        Me.sccRepEditor.Name = "sccRepEditor"
        '
        'sccRepEditor.Panel1
        '
        Me.sccRepEditor.Panel1.Controls.Add(Me.sccReportTree)
        Me.sccRepEditor.Panel1.MinSize = 500
        '
        'sccRepEditor.Panel2
        '
        Me.sccRepEditor.Panel2.Controls.Add(Me.tlpProperties)
        Me.sccRepEditor.Panel2.MinSize = 300
        Me.sccRepEditor.Size = New System.Drawing.Size(1297, 777)
        Me.sccRepEditor.SplitterPosition = 987
        Me.sccRepEditor.TabIndex = 12
        '
        'sccReportTree
        '
        Me.sccReportTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccReportTree.Horizontal = False
        Me.sccReportTree.Location = New System.Drawing.Point(0, 0)
        Me.sccReportTree.Name = "sccReportTree"
        '
        'sccReportTree.Panel1
        '
        Me.sccReportTree.Panel1.Controls.Add(Me.tlvReports)
        Me.sccReportTree.Panel1.MinSize = 300
        Me.sccReportTree.Panel1.Text = "Panel1"
        '
        'sccReportTree.Panel2
        '
        Me.sccReportTree.Panel2.Controls.Add(Me.gcReportHistory)
        Me.sccReportTree.Panel2.MinSize = 200
        Me.sccReportTree.Panel2.Text = "Panel2"
        Me.sccReportTree.Size = New System.Drawing.Size(987, 777)
        Me.sccReportTree.SplitterPosition = 567
        Me.sccReportTree.TabIndex = 0
        '
        'tlvReports
        '
        Me.tlvReports.ContextMenuStrip = Me.cmsReport
        Me.tlvReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvReports.Location = New System.Drawing.Point(0, 0)
        Me.tlvReports.Name = "tlvReports"
        Me.tlvReports.OptionsBehavior.Editable = False
        Me.tlvReports.OptionsBehavior.ReadOnly = True
        Me.tlvReports.OptionsCustomization.AllowSort = False
        Me.tlvReports.OptionsMenu.EnableNodeMenu = False
        Me.tlvReports.OptionsView.ShowHorzLines = False
        Me.tlvReports.Size = New System.Drawing.Size(987, 567)
        Me.tlvReports.TabIndex = 0
        '
        'cmsReport
        '
        Me.cmsReport.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_ReportSlideAdd, Me.tsmi_ReportRename, Me.tsmi_ReportDelete, Me.ToolStripSeparator5, Me.tsmi_ReportObjects, Me.tsmi_ReportRunCurrent, Me.tsmi_ReportRunConfigured, Me.tsmi_ReportLock, Me.tsmi_ReportCopy, Me.ToolStripSeparator6, Me.tsmt_SlideRename, Me.tsmt_SlideDelete, Me.ToolStripSeparator7, Me.tsmi_SlideMoveUp, Me.tsmi_SlideMoveDown, Me.tsmi_SlideObjectAdd, Me.ToolStripSeparator4, Me.tsmi_ObjectChartMoveUp, Me.tsmi_ObjectChartMoveDown, Me.tsmi_ObjectRename, Me.tsmi_ObjectChartDelete})
        Me.cmsReport.Name = "cm_ReportEditor"
        Me.cmsReport.Size = New System.Drawing.Size(205, 424)
        '
        'tsmi_ReportSlideAdd
        '
        Me.tsmi_ReportSlideAdd.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_txtReportSlideAdd})
        Me.tsmi_ReportSlideAdd.Name = "tsmi_ReportSlideAdd"
        Me.tsmi_ReportSlideAdd.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportSlideAdd.Text = "Slide - Add"
        '
        'tsmi_txtReportSlideAdd
        '
        Me.tsmi_txtReportSlideAdd.Name = "tsmi_txtReportSlideAdd"
        Me.tsmi_txtReportSlideAdd.Size = New System.Drawing.Size(100, 23)
        '
        'tsmi_ReportRename
        '
        Me.tsmi_ReportRename.Name = "tsmi_ReportRename"
        Me.tsmi_ReportRename.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportRename.Text = "Report - Rename"
        '
        'tsmi_ReportDelete
        '
        Me.tsmi_ReportDelete.Name = "tsmi_ReportDelete"
        Me.tsmi_ReportDelete.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportDelete.Text = "Report - Delete"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(201, 6)
        '
        'tsmi_ReportObjects
        '
        Me.tsmi_ReportObjects.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ObjectAddToolStripMenuItem, Me.ObjectRemoveToolStripMenuItem, Me.ObjectMoveUpToolStripMenuItem, Me.ObjectMoveDownToolStripMenuItem, Me.ToolStripMenuItem1, Me.ToolStripMenuItem2})
        Me.tsmi_ReportObjects.Name = "tsmi_ReportObjects"
        Me.tsmi_ReportObjects.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportObjects.Text = "Report - Objects"
        '
        'ObjectAddToolStripMenuItem
        '
        Me.ObjectAddToolStripMenuItem.Name = "ObjectAddToolStripMenuItem"
        Me.ObjectAddToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ObjectAddToolStripMenuItem.Text = "Object Add"
        '
        'ObjectRemoveToolStripMenuItem
        '
        Me.ObjectRemoveToolStripMenuItem.Name = "ObjectRemoveToolStripMenuItem"
        Me.ObjectRemoveToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ObjectRemoveToolStripMenuItem.Text = "Object Remove"
        '
        'ObjectMoveUpToolStripMenuItem
        '
        Me.ObjectMoveUpToolStripMenuItem.Name = "ObjectMoveUpToolStripMenuItem"
        Me.ObjectMoveUpToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ObjectMoveUpToolStripMenuItem.Text = "Object Move Up"
        '
        'ObjectMoveDownToolStripMenuItem
        '
        Me.ObjectMoveDownToolStripMenuItem.Name = "ObjectMoveDownToolStripMenuItem"
        Me.ObjectMoveDownToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ObjectMoveDownToolStripMenuItem.Text = "Object Move Down"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox1})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem1.Text = "Edit Slide Title"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 23)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox2})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem2.Text = "Edit Slide Text"
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(100, 23)
        '
        'tsmi_ReportRunCurrent
        '
        Me.tsmi_ReportRunCurrent.Name = "tsmi_ReportRunCurrent"
        Me.tsmi_ReportRunCurrent.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportRunCurrent.Text = "Report - Run Current"
        '
        'tsmi_ReportRunConfigured
        '
        Me.tsmi_ReportRunConfigured.Name = "tsmi_ReportRunConfigured"
        Me.tsmi_ReportRunConfigured.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportRunConfigured.Text = "Report - Run Configured"
        '
        'tsmi_ReportLock
        '
        Me.tsmi_ReportLock.Name = "tsmi_ReportLock"
        Me.tsmi_ReportLock.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportLock.Text = "Report - Lock"
        '
        'tsmi_ReportCopy
        '
        Me.tsmi_ReportCopy.Name = "tsmi_ReportCopy"
        Me.tsmi_ReportCopy.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ReportCopy.Text = "Report - Copy"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(201, 6)
        '
        'tsmt_SlideRename
        '
        Me.tsmt_SlideRename.Name = "tsmt_SlideRename"
        Me.tsmt_SlideRename.Size = New System.Drawing.Size(204, 22)
        Me.tsmt_SlideRename.Text = "Slide - Rename"
        '
        'tsmt_SlideDelete
        '
        Me.tsmt_SlideDelete.Name = "tsmt_SlideDelete"
        Me.tsmt_SlideDelete.Size = New System.Drawing.Size(204, 22)
        Me.tsmt_SlideDelete.Text = "Slide - Delete"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(201, 6)
        '
        'tsmi_SlideMoveUp
        '
        Me.tsmi_SlideMoveUp.Name = "tsmi_SlideMoveUp"
        Me.tsmi_SlideMoveUp.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_SlideMoveUp.Text = "Slide - Move Up"
        '
        'tsmi_SlideMoveDown
        '
        Me.tsmi_SlideMoveDown.Name = "tsmi_SlideMoveDown"
        Me.tsmi_SlideMoveDown.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_SlideMoveDown.Text = "Slide - Move Down"
        '
        'tsmi_SlideObjectAdd
        '
        Me.tsmi_SlideObjectAdd.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmitxt_NewTextbox})
        Me.tsmi_SlideObjectAdd.Name = "tsmi_SlideObjectAdd"
        Me.tsmi_SlideObjectAdd.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_SlideObjectAdd.Text = "Object - Add Textbox"
        '
        'tsmitxt_NewTextbox
        '
        Me.tsmitxt_NewTextbox.Name = "tsmitxt_NewTextbox"
        Me.tsmitxt_NewTextbox.Size = New System.Drawing.Size(100, 23)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(201, 6)
        '
        'tsmi_ObjectChartMoveUp
        '
        Me.tsmi_ObjectChartMoveUp.Name = "tsmi_ObjectChartMoveUp"
        Me.tsmi_ObjectChartMoveUp.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ObjectChartMoveUp.Text = "Object - Move Up"
        '
        'tsmi_ObjectChartMoveDown
        '
        Me.tsmi_ObjectChartMoveDown.Name = "tsmi_ObjectChartMoveDown"
        Me.tsmi_ObjectChartMoveDown.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ObjectChartMoveDown.Text = "Object - Move Down"
        '
        'tsmi_ObjectRename
        '
        Me.tsmi_ObjectRename.Name = "tsmi_ObjectRename"
        Me.tsmi_ObjectRename.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ObjectRename.Text = "Object - Rename"
        '
        'tsmi_ObjectChartDelete
        '
        Me.tsmi_ObjectChartDelete.Name = "tsmi_ObjectChartDelete"
        Me.tsmi_ObjectChartDelete.Size = New System.Drawing.Size(204, 22)
        Me.tsmi_ObjectChartDelete.Text = "Object - Delete"
        '
        'gcReportHistory
        '
        Me.gcReportHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcReportHistory.Location = New System.Drawing.Point(0, 0)
        Me.gcReportHistory.MainView = Me.gvReportHistory
        Me.gcReportHistory.Name = "gcReportHistory"
        Me.gcReportHistory.Size = New System.Drawing.Size(987, 200)
        Me.gcReportHistory.TabIndex = 16
        Me.gcReportHistory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReportHistory})
        '
        'gvReportHistory
        '
        Me.gvReportHistory.GridControl = Me.gcReportHistory
        Me.gvReportHistory.Name = "gvReportHistory"
        Me.gvReportHistory.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportHistory.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportHistory.OptionsBehavior.Editable = False
        Me.gvReportHistory.OptionsBehavior.ReadOnly = True
        Me.gvReportHistory.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportHistory.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportHistory.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportHistory.OptionsView.ColumnAutoWidth = False
        Me.gvReportHistory.OptionsView.ShowGroupPanel = False
        '
        'tlpProperties
        '
        Me.tlpProperties.ColumnCount = 1
        Me.tlpProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpProperties.Controls.Add(Me.tlpPropButtonsTop, 0, 2)
        Me.tlpProperties.Controls.Add(Me.tlpPropButtonsBotttom, 0, 4)
        Me.tlpProperties.Controls.Add(Me.propertyGridreport, 0, 3)
        Me.tlpProperties.Controls.Add(Me.VLabel3, 0, 0)
        Me.tlpProperties.Controls.Add(Me.tlpStyleName, 0, 1)
        Me.tlpProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpProperties.Location = New System.Drawing.Point(0, 0)
        Me.tlpProperties.Name = "tlpProperties"
        Me.tlpProperties.RowCount = 5
        Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpProperties.Size = New System.Drawing.Size(300, 777)
        Me.tlpProperties.TabIndex = 3
        '
        'tlpPropButtonsTop
        '
        Me.tlpPropButtonsTop.BackColor = System.Drawing.Color.Transparent
        Me.tlpPropButtonsTop.ColumnCount = 2
        Me.tlpPropButtonsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpPropButtonsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpPropButtonsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPropButtonsTop.Controls.Add(Me.btnSaveStyle, 1, 0)
        Me.tlpPropButtonsTop.Controls.Add(Me.btnCreateStyle, 0, 0)
        Me.tlpPropButtonsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpPropButtonsTop.Location = New System.Drawing.Point(1, 56)
        Me.tlpPropButtonsTop.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpPropButtonsTop.Name = "tlpPropButtonsTop"
        Me.tlpPropButtonsTop.RowCount = 1
        Me.tlpPropButtonsTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPropButtonsTop.Size = New System.Drawing.Size(298, 33)
        Me.tlpPropButtonsTop.TabIndex = 9
        '
        'btnSaveStyle
        '
        Me.btnSaveStyle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSaveStyle.Enabled = False
        Me.btnSaveStyle.Location = New System.Drawing.Point(152, 3)
        Me.btnSaveStyle.Name = "btnSaveStyle"
        Me.btnSaveStyle.Size = New System.Drawing.Size(143, 27)
        Me.btnSaveStyle.TabIndex = 6
        Me.btnSaveStyle.Text = "Save Style"
        '
        'btnCreateStyle
        '
        Me.btnCreateStyle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCreateStyle.Enabled = False
        Me.btnCreateStyle.Location = New System.Drawing.Point(3, 3)
        Me.btnCreateStyle.Name = "btnCreateStyle"
        Me.btnCreateStyle.Size = New System.Drawing.Size(143, 27)
        Me.btnCreateStyle.TabIndex = 5
        Me.btnCreateStyle.Text = "Create Style"
        '
        'tlpPropButtonsBotttom
        '
        Me.tlpPropButtonsBotttom.BackColor = System.Drawing.Color.Transparent
        Me.tlpPropButtonsBotttom.ColumnCount = 2
        Me.tlpPropButtonsBotttom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpPropButtonsBotttom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpPropButtonsBotttom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPropButtonsBotttom.Controls.Add(Me.btnPreview, 0, 0)
        Me.tlpPropButtonsBotttom.Controls.Add(Me.btnApplyStyle, 1, 0)
        Me.tlpPropButtonsBotttom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpPropButtonsBotttom.Location = New System.Drawing.Point(1, 743)
        Me.tlpPropButtonsBotttom.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpPropButtonsBotttom.Name = "tlpPropButtonsBotttom"
        Me.tlpPropButtonsBotttom.RowCount = 1
        Me.tlpPropButtonsBotttom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPropButtonsBotttom.Size = New System.Drawing.Size(298, 33)
        Me.tlpPropButtonsBotttom.TabIndex = 8
        '
        'btnPreview
        '
        Me.btnPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPreview.Enabled = False
        Me.btnPreview.Location = New System.Drawing.Point(3, 3)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(143, 27)
        Me.btnPreview.TabIndex = 4
        Me.btnPreview.Text = "Preview"
        '
        'btnApplyStyle
        '
        Me.btnApplyStyle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnApplyStyle.Enabled = False
        Me.btnApplyStyle.Location = New System.Drawing.Point(152, 3)
        Me.btnApplyStyle.Name = "btnApplyStyle"
        Me.btnApplyStyle.Size = New System.Drawing.Size(143, 27)
        Me.btnApplyStyle.TabIndex = 5
        Me.btnApplyStyle.Text = "Apply Style"
        '
        'propertyGridreport
        '
        Me.propertyGridreport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propertyGridreport.LineColor = System.Drawing.SystemColors.ControlDark
        Me.propertyGridreport.Location = New System.Drawing.Point(4, 94)
        Me.propertyGridreport.Margin = New System.Windows.Forms.Padding(4)
        Me.propertyGridreport.Name = "propertyGridreport"
        Me.propertyGridreport.PropertySort = System.Windows.Forms.PropertySort.Categorized
        Me.propertyGridreport.Size = New System.Drawing.Size(292, 644)
        Me.propertyGridreport.TabIndex = 0
        '
        'VLabel3
        '
        Me.VLabel3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VLabel3.Appearance.Options.UseBackColor = True
        Me.VLabel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel3.Location = New System.Drawing.Point(3, 3)
        Me.VLabel3.Name = "VLabel3"
        Me.VLabel3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.VLabel3.Size = New System.Drawing.Size(294, 19)
        Me.VLabel3.TabIndex = 6
        Me.VLabel3.Text = "Properties"
        '
        'tlpStyleName
        '
        Me.tlpStyleName.BackColor = System.Drawing.Color.Transparent
        Me.tlpStyleName.ColumnCount = 2
        Me.tlpStyleName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.tlpStyleName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpStyleName.Controls.Add(Me.lblStyleObject, 0, 0)
        Me.tlpStyleName.Controls.Add(Me.cmbStyleName, 1, 0)
        Me.tlpStyleName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpStyleName.Location = New System.Drawing.Point(1, 26)
        Me.tlpStyleName.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpStyleName.Name = "tlpStyleName"
        Me.tlpStyleName.RowCount = 1
        Me.tlpStyleName.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpStyleName.Size = New System.Drawing.Size(298, 28)
        Me.tlpStyleName.TabIndex = 7
        '
        'lblStyleObject
        '
        Me.lblStyleObject.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lblStyleObject.Appearance.Options.UseBackColor = True
        Me.lblStyleObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStyleObject.Location = New System.Drawing.Point(4, 4)
        Me.lblStyleObject.Margin = New System.Windows.Forms.Padding(4)
        Me.lblStyleObject.Name = "lblStyleObject"
        Me.lblStyleObject.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblStyleObject.Size = New System.Drawing.Size(97, 20)
        Me.lblStyleObject.TabIndex = 7
        Me.lblStyleObject.Text = "Select Object Style"
        '
        'cmbStyleName
        '
        Me.cmbStyleName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbStyleName.Location = New System.Drawing.Point(108, 4)
        Me.cmbStyleName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbStyleName.Name = "cmbStyleName"
        Me.cmbStyleName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbStyleName.Size = New System.Drawing.Size(187, 20)
        Me.cmbStyleName.TabIndex = 8
        '
        'lblMSG
        '
        Me.lblMSG.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG.Appearance.Options.UseBackColor = True
        Me.lblMSG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMSG.Location = New System.Drawing.Point(3, 863)
        Me.lblMSG.Name = "lblMSG"
        Me.lblMSG.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMSG.Size = New System.Drawing.Size(1299, 34)
        Me.lblMSG.TabIndex = 13
        Me.lblMSG.Text = " "
        '
        'sccTop
        '
        Me.sccTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccTop.Location = New System.Drawing.Point(3, 3)
        Me.sccTop.Name = "sccTop"
        '
        'sccTop.Panel1
        '
        Me.sccTop.Panel1.Controls.Add(Me.gcKPIName)
        Me.sccTop.Panel1.MinSize = 500
        Me.sccTop.Panel1.Text = "Panel1"
        '
        'sccTop.Panel2
        '
        Me.sccTop.Panel2.Controls.Add(Me.VGroupBox1)
        Me.sccTop.Panel2.MinSize = 300
        Me.sccTop.Panel2.Text = "Panel2"
        Me.sccTop.Size = New System.Drawing.Size(1299, 69)
        Me.sccTop.SplitterPosition = 989
        Me.sccTop.TabIndex = 14
        '
        'gcKPIName
        '
        Me.gcKPIName.AutoSize = True
        Me.gcKPIName.Controls.Add(Me.tlpReportGroup)
        Me.gcKPIName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPIName.Location = New System.Drawing.Point(0, 0)
        Me.gcKPIName.Name = "gcKPIName"
        Me.gcKPIName.Padding = New System.Windows.Forms.Padding(2)
        Me.gcKPIName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gcKPIName.Size = New System.Drawing.Size(989, 69)
        Me.gcKPIName.TabIndex = 20
        Me.gcKPIName.Text = "Search Report"
        '
        'tlpReportGroup
        '
        Me.tlpReportGroup.ColumnCount = 4
        Me.tlpReportGroup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.tlpReportGroup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpReportGroup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReportGroup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpReportGroup.Controls.Add(Me.btnRefresh, 3, 1)
        Me.tlpReportGroup.Controls.Add(Me.VLabel2, 1, 1)
        Me.tlpReportGroup.Controls.Add(Me.cmbReportGroup, 2, 1)
        Me.tlpReportGroup.Controls.Add(Me.txtSearchReport, 0, 1)
        Me.tlpReportGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpReportGroup.Location = New System.Drawing.Point(4, 25)
        Me.tlpReportGroup.Name = "tlpReportGroup"
        Me.tlpReportGroup.RowCount = 3
        Me.tlpReportGroup.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpReportGroup.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpReportGroup.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpReportGroup.Size = New System.Drawing.Size(981, 40)
        Me.tlpReportGroup.TabIndex = 7
        '
        'btnRefresh
        '
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefresh.Location = New System.Drawing.Point(884, 7)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(94, 26)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refresh"
        '
        'VLabel2
        '
        Me.VLabel2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.Appearance.Options.UseBackColor = True
        Me.VLabel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel2.Location = New System.Drawing.Point(203, 7)
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.VLabel2.Size = New System.Drawing.Size(74, 26)
        Me.VLabel2.TabIndex = 5
        Me.VLabel2.Text = "Report Group"
        '
        'cmbReportGroup
        '
        Me.cmbReportGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbReportGroup.Location = New System.Drawing.Point(283, 10)
        Me.cmbReportGroup.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.cmbReportGroup.Name = "cmbReportGroup"
        Me.cmbReportGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbReportGroup.Size = New System.Drawing.Size(595, 20)
        Me.cmbReportGroup.TabIndex = 15
        '
        'txtSearchReport
        '
        Me.txtSearchReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchReport.Location = New System.Drawing.Point(3, 10)
        Me.txtSearchReport.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtSearchReport.Name = "txtSearchReport"
        Me.txtSearchReport.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtSearchReport.Properties.Appearance.Options.UseBackColor = True
        Me.txtSearchReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchReport.Properties.NullValuePrompt = "Search..."
        Me.txtSearchReport.Size = New System.Drawing.Size(194, 20)
        Me.txtSearchReport.TabIndex = 14
        '
        'VGroupBox1
        '
        Me.VGroupBox1.Controls.Add(Me.tlpCreateReport)
        Me.VGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.VGroupBox1.Name = "VGroupBox1"
        Me.VGroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.VGroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.VGroupBox1.Size = New System.Drawing.Size(300, 69)
        Me.VGroupBox1.TabIndex = 21
        Me.VGroupBox1.Text = "Create Report"
        '
        'tlpCreateReport
        '
        Me.tlpCreateReport.BackColor = System.Drawing.Color.Transparent
        Me.tlpCreateReport.ColumnCount = 2
        Me.tlpCreateReport.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.tlpCreateReport.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCreateReport.Controls.Add(Me.btnReportAdd, 0, 1)
        Me.tlpCreateReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCreateReport.Location = New System.Drawing.Point(4, 25)
        Me.tlpCreateReport.Name = "tlpCreateReport"
        Me.tlpCreateReport.RowCount = 3
        Me.tlpCreateReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpCreateReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpCreateReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpCreateReport.Size = New System.Drawing.Size(292, 40)
        Me.tlpCreateReport.TabIndex = 14
        '
        'btnReportAdd
        '
        Me.btnReportAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReportAdd.Location = New System.Drawing.Point(3, 7)
        Me.btnReportAdd.Name = "btnReportAdd"
        Me.btnReportAdd.Size = New System.Drawing.Size(99, 26)
        Me.btnReportAdd.TabIndex = 0
        Me.btnReportAdd.Text = "Add"
        '
        'Timer1
        '
        Me.Timer1.Interval = 3000
        '
        'frmReportEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1305, 900)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmReportEdit.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(992, 549)
        Me.Name = "frmReportEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Editor"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.sccRepEditor.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccRepEditor.Panel1.ResumeLayout(False)
        CType(Me.sccRepEditor.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccRepEditor.Panel2.ResumeLayout(False)
        CType(Me.sccRepEditor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccRepEditor.ResumeLayout(False)
        CType(Me.sccReportTree.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccReportTree.Panel1.ResumeLayout(False)
        CType(Me.sccReportTree.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccReportTree.Panel2.ResumeLayout(False)
        CType(Me.sccReportTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccReportTree.ResumeLayout(False)
        CType(Me.tlvReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsReport.ResumeLayout(False)
        CType(Me.gcReportHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReportHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpProperties.ResumeLayout(False)
        Me.tlpProperties.PerformLayout()
        Me.tlpPropButtonsTop.ResumeLayout(False)
        Me.tlpPropButtonsBotttom.ResumeLayout(False)
        Me.tlpStyleName.ResumeLayout(False)
        Me.tlpStyleName.PerformLayout()
        CType(Me.cmbStyleName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccTop.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTop.Panel1.ResumeLayout(False)
        Me.sccTop.Panel1.PerformLayout()
        CType(Me.sccTop.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTop.Panel2.ResumeLayout(False)
        CType(Me.sccTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTop.ResumeLayout(False)
        CType(Me.gcKPIName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcKPIName.ResumeLayout(False)
        Me.tlpReportGroup.ResumeLayout(False)
        Me.tlpReportGroup.PerformLayout()
        CType(Me.cmbReportGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VGroupBox1.ResumeLayout(False)
        Me.tlpCreateReport.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcKPIName As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpReportGroup As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents VLabel2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VGroupBox1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpCreateReport As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents sccRepEditor As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents tlpProperties As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlpPropButtonsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSaveStyle As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCreateStyle As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpPropButtonsBotttom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnApplyStyle As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents propertyGridreport As System.Windows.Forms.PropertyGrid
    Friend WithEvents VLabel3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpStyleName As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblStyleObject As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMSG As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmsReport As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_ReportSlideAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_txtReportSlideAdd As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tsmi_ReportRename As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_ReportDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_ReportRunConfigured As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_ReportLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbReportGroup As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbStyleName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtSearchReport As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents tlvReports As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents tsmt_SlideRename As ToolStripMenuItem
    Friend WithEvents tsmt_SlideDelete As ToolStripMenuItem
    Friend WithEvents tsmi_SlideMoveUp As ToolStripMenuItem
    Friend WithEvents tsmi_SlideMoveDown As ToolStripMenuItem
    Friend WithEvents tsmi_SlideObjectAdd As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents tsmitxt_NewTextbox As ToolStripTextBox
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents tsmi_ObjectRename As ToolStripMenuItem
    Friend WithEvents tsmi_ObjectChartDelete As ToolStripMenuItem
    Friend WithEvents tsmi_ReportObjects As ToolStripMenuItem
    Friend WithEvents ObjectAddToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectRemoveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectMoveUpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectMoveDownToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox2 As ToolStripTextBox
    Friend WithEvents tsmi_ReportRunCurrent As ToolStripMenuItem
    Friend WithEvents tsmi_ReportCopy As ToolStripMenuItem
    Friend WithEvents sccTop As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents btnReportAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tsmi_ObjectChartMoveUp As ToolStripMenuItem
    Friend WithEvents tsmi_ObjectChartMoveDown As ToolStripMenuItem
    Friend WithEvents gcReportHistory As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReportHistory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sccReportTree As DevExpress.XtraEditors.SplitContainerControl
End Class
