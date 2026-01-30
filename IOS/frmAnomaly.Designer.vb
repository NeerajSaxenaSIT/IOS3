<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAnomaly
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
        Dim TrackBarLabel1 As DevExpress.XtraEditors.Repository.TrackBarLabel = New DevExpress.XtraEditors.Repository.TrackBarLabel()
        Dim TrackBarLabel2 As DevExpress.XtraEditors.Repository.TrackBarLabel = New DevExpress.XtraEditors.Repository.TrackBarLabel()
        Dim TrackBarLabel3 As DevExpress.XtraEditors.Repository.TrackBarLabel = New DevExpress.XtraEditors.Repository.TrackBarLabel()
        Dim Annotation1 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim BoxHeaderOptions1 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim BoxHeaderOptions2 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim BoxHeaderOptions3 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim View3D1 As dotnetCHARTING.WinForms.View3D = New dotnetCHARTING.WinForms.View3D()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnomaly))
        Me.sccMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccTop = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcNotes = New DevExpress.XtraEditors.GroupControl()
        Me.grdNotes = New DevExpress.XtraGrid.GridControl()
        Me.cm_CopyGridData = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RecordCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_Copy_All = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Copy_SelectionWOHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Copy_SelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvNotes = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcAlerts = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.xtcAlerts = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpAlert = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdAlerts = New DevExpress.XtraGrid.GridControl()
        Me.cmsGridAlerts = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiModifyAlert = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSuppressAlertAllObjects = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSuppressAlertSelectedObject = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSendObjectToPM = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAddNote = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_Alerts_Copy_All = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Alerts_Copy_SelectionWOHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Alerts_Copy_SelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvAlerts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtpSuppressed = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdSuppressed = New DevExpress.XtraGrid.GridControl()
        Me.cmsGridSuppressed = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiDeleteSuppression = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiExtentSuppression = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_Suppressed_Copy_All = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Suppressed_Copy_SelectionWOHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Suppressed_Copy_SelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvSuppressed = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtpScore = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdScore = New DevExpress.XtraGrid.GridControl()
        Me.cms_ScoreGrid = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_ScoreRecordCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_Send2Alerts = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Send2PM = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvScore = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAlertManager = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcCorrelation = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcNegative = New DevExpress.XtraEditors.GroupControl()
        Me.grdNegative = New DevExpress.XtraGrid.GridControl()
        Me.gvNegative = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCorrelationFilter = New DevExpress.XtraEditors.LabelControl()
        Me.cmbCorrType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbcCorrelationFilter = New DevExpress.XtraEditors.TrackBarControl()
        Me.gcPositive = New DevExpress.XtraEditors.GroupControl()
        Me.grdPositive = New DevExpress.XtraGrid.GridControl()
        Me.gvPositive = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcRules = New DevExpress.XtraEditors.GroupControl()
        Me.grdRulesDetected = New DevExpress.XtraGrid.GridControl()
        Me.gvRulesDetected = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.sccBottom = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcChartSettings = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.tglAlertTest = New IOS.Library.IOSToggleButton()
        Me.lblDataPoint = New DevExpress.XtraEditors.LabelControl()
        Me.seDataPoint = New DevExpress.XtraEditors.SpinEdit()
        Me.btnClearChart = New DevExpress.XtraEditors.SimpleButton()
        Me.ceShowAlertDetected = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowAlertTriggered = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowKPIBreach = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowSlidingWindow = New DevExpress.XtraEditors.CheckEdit()
        Me.sccChart = New DevExpress.XtraEditors.SplitContainerControl()
        Me.chAnomaly = New dotnetCHARTING.WinForms.Chart()
        Me.gcChartData = New DevExpress.XtraGrid.GridControl()
        Me.gvChartData = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel1.SuspendLayout()
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel2.SuspendLayout()
        Me.sccMain.SuspendLayout()
        CType(Me.sccTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccTop.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTop.Panel1.SuspendLayout()
        CType(Me.sccTop.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTop.Panel2.SuspendLayout()
        Me.sccTop.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.gcNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcNotes.SuspendLayout()
        CType(Me.grdNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_CopyGridData.SuspendLayout()
        CType(Me.gvNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcAlerts.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.xtcAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcAlerts.SuspendLayout()
        Me.xtpAlert.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.grdAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsGridAlerts.SuspendLayout()
        CType(Me.gvAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpSuppressed.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.grdSuppressed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsGridSuppressed.SuspendLayout()
        CType(Me.gvSuppressed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpScore.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.grdScore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cms_ScoreGrid.SuspendLayout()
        CType(Me.gvScore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcCorrelation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcCorrelation.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.gcNegative, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcNegative.SuspendLayout()
        CType(Me.grdNegative, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvNegative, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.cmbCorrType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.tbcCorrelationFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbcCorrelationFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcPositive, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcPositive.SuspendLayout()
        CType(Me.grdPositive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPositive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcRules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcRules.SuspendLayout()
        CType(Me.grdRulesDetected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvRulesDetected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccBottom.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccBottom.Panel1.SuspendLayout()
        CType(Me.sccBottom.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccBottom.Panel2.SuspendLayout()
        Me.sccBottom.SuspendLayout()
        CType(Me.gcChartSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcChartSettings.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.seDataPoint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowAlertDetected.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowAlertTriggered.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowKPIBreach.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowSlidingWindow.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccChart.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccChart.Panel1.SuspendLayout()
        CType(Me.sccChart.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccChart.Panel2.SuspendLayout()
        Me.sccChart.SuspendLayout()
        CType(Me.chAnomaly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcChartData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvChartData, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.sccMain.Panel1.Controls.Add(Me.sccTop)
        Me.sccMain.Panel1.MinSize = 300
        Me.sccMain.Panel1.Text = "Panel1"
        '
        'sccMain.Panel2
        '
        Me.sccMain.Panel2.Controls.Add(Me.sccBottom)
        Me.sccMain.Panel2.MinSize = 250
        Me.sccMain.Panel2.Text = "Panel2"
        Me.sccMain.Size = New System.Drawing.Size(1302, 736)
        Me.sccMain.SplitterPosition = 500
        Me.sccMain.TabIndex = 0
        Me.sccMain.Text = "SplitContainer"
        '
        'sccTop
        '
        Me.sccTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccTop.Location = New System.Drawing.Point(0, 0)
        Me.sccTop.Name = "sccTop"
        '
        'sccTop.Panel1
        '
        Me.sccTop.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.sccTop.Panel1.MinSize = 450
        Me.sccTop.Panel1.Text = "Panel1"
        '
        'sccTop.Panel2
        '
        Me.sccTop.Panel2.Controls.Add(Me.TableLayoutPanel3)
        Me.sccTop.Panel2.MinSize = 450
        Me.sccTop.Panel2.Text = "Panel2"
        Me.sccTop.Size = New System.Drawing.Size(1302, 445)
        Me.sccTop.SplitterPosition = 650
        Me.sccTop.TabIndex = 1
        Me.sccTop.Text = "SplitContainerControl1"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.gcNotes, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.gcAlerts, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(450, 445)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'gcNotes
        '
        Me.gcNotes.Controls.Add(Me.grdNotes)
        Me.gcNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcNotes.Location = New System.Drawing.Point(3, 314)
        Me.gcNotes.Name = "gcNotes"
        Me.gcNotes.Size = New System.Drawing.Size(444, 128)
        Me.gcNotes.TabIndex = 2
        Me.gcNotes.Text = "Notes"
        '
        'grdNotes
        '
        Me.grdNotes.AllowDrop = True
        Me.grdNotes.ContextMenuStrip = Me.cm_CopyGridData
        Me.grdNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdNotes.Location = New System.Drawing.Point(2, 23)
        Me.grdNotes.MainView = Me.gvNotes
        Me.grdNotes.Name = "grdNotes"
        Me.grdNotes.Size = New System.Drawing.Size(440, 103)
        Me.grdNotes.TabIndex = 2
        Me.grdNotes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvNotes})
        '
        'cm_CopyGridData
        '
        Me.cm_CopyGridData.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RecordCount, Me.ToolStripSeparator11, Me.tsmi_Copy_All, Me.tsmi_Copy_SelectionWOHeader, Me.tsmi_Copy_SelectionWithHeader})
        Me.cm_CopyGridData.Name = "cm_GridViewMap"
        Me.cm_CopyGridData.Size = New System.Drawing.Size(249, 120)
        '
        'tsmi_RecordCount
        '
        Me.tsmi_RecordCount.Enabled = False
        Me.tsmi_RecordCount.Name = "tsmi_RecordCount"
        Me.tsmi_RecordCount.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_RecordCount.Text = "Record Count: "
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(245, 6)
        '
        'tsmi_Copy_All
        '
        Me.tsmi_Copy_All.Name = "tsmi_Copy_All"
        Me.tsmi_Copy_All.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Copy_All.Text = "Copy to Clipboard  - All"
        '
        'tsmi_Copy_SelectionWOHeader
        '
        Me.tsmi_Copy_SelectionWOHeader.Name = "tsmi_Copy_SelectionWOHeader"
        Me.tsmi_Copy_SelectionWOHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Copy_SelectionWOHeader.Text = "Copy - Selection Without Header"
        '
        'tsmi_Copy_SelectionWithHeader
        '
        Me.tsmi_Copy_SelectionWithHeader.Name = "tsmi_Copy_SelectionWithHeader"
        Me.tsmi_Copy_SelectionWithHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Copy_SelectionWithHeader.Text = "Copy - Selection With Header"
        '
        'gvNotes
        '
        Me.gvNotes.GridControl = Me.grdNotes
        Me.gvNotes.Name = "gvNotes"
        Me.gvNotes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNotes.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNotes.OptionsBehavior.Editable = False
        Me.gvNotes.OptionsBehavior.ReadOnly = True
        Me.gvNotes.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNotes.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNotes.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNotes.OptionsView.ColumnAutoWidth = False
        Me.gvNotes.OptionsView.ShowGroupPanel = False
        '
        'gcAlerts
        '
        Me.gcAlerts.Controls.Add(Me.TableLayoutPanel2)
        Me.gcAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcAlerts.Location = New System.Drawing.Point(3, 3)
        Me.gcAlerts.Name = "gcAlerts"
        Me.gcAlerts.Size = New System.Drawing.Size(444, 305)
        Me.gcAlerts.TabIndex = 1
        Me.gcAlerts.Text = "Alerts"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.xtcAlerts, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel11, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(440, 280)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'xtcAlerts
        '
        Me.xtcAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcAlerts.Location = New System.Drawing.Point(3, 36)
        Me.xtcAlerts.Name = "xtcAlerts"
        Me.xtcAlerts.SelectedTabPage = Me.xtpAlert
        Me.xtcAlerts.Size = New System.Drawing.Size(434, 241)
        Me.xtcAlerts.TabIndex = 1
        Me.xtcAlerts.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpAlert, Me.xtpSuppressed, Me.xtpScore})
        '
        'xtpAlert
        '
        Me.xtpAlert.Controls.Add(Me.TableLayoutPanel9)
        Me.xtpAlert.Name = "xtpAlert"
        Me.xtpAlert.Size = New System.Drawing.Size(432, 216)
        Me.xtpAlert.Text = "Alerts"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.grdAlerts, 0, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(432, 216)
        Me.TableLayoutPanel9.TabIndex = 0
        '
        'grdAlerts
        '
        Me.grdAlerts.ContextMenuStrip = Me.cmsGridAlerts
        Me.grdAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAlerts.Location = New System.Drawing.Point(3, 3)
        Me.grdAlerts.MainView = Me.gvAlerts
        Me.grdAlerts.Name = "grdAlerts"
        Me.grdAlerts.Size = New System.Drawing.Size(426, 210)
        Me.grdAlerts.TabIndex = 1
        Me.grdAlerts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvAlerts})
        '
        'cmsGridAlerts
        '
        Me.cmsGridAlerts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiModifyAlert, Me.tsmiSuppressAlertAllObjects, Me.tsmiSuppressAlertSelectedObject, Me.tsmiSendObjectToPM, Me.tsmiAddNote, Me.ToolStripMenuItem1, Me.tsmi_Alerts_Copy_All, Me.tsmi_Alerts_Copy_SelectionWOHeader, Me.tsmi_Alerts_Copy_SelectionWithHeader})
        Me.cmsGridAlerts.Name = "cmsGridAlerts"
        Me.cmsGridAlerts.Size = New System.Drawing.Size(249, 186)
        '
        'tsmiModifyAlert
        '
        Me.tsmiModifyAlert.Name = "tsmiModifyAlert"
        Me.tsmiModifyAlert.Size = New System.Drawing.Size(248, 22)
        Me.tsmiModifyAlert.Text = "Modify Alert"
        '
        'tsmiSuppressAlertAllObjects
        '
        Me.tsmiSuppressAlertAllObjects.Name = "tsmiSuppressAlertAllObjects"
        Me.tsmiSuppressAlertAllObjects.Size = New System.Drawing.Size(248, 22)
        Me.tsmiSuppressAlertAllObjects.Text = "Suppress Alert - All Objects"
        '
        'tsmiSuppressAlertSelectedObject
        '
        Me.tsmiSuppressAlertSelectedObject.Name = "tsmiSuppressAlertSelectedObject"
        Me.tsmiSuppressAlertSelectedObject.Size = New System.Drawing.Size(248, 22)
        Me.tsmiSuppressAlertSelectedObject.Text = "Suppress Alert - Selected Object"
        '
        'tsmiSendObjectToPM
        '
        Me.tsmiSendObjectToPM.Name = "tsmiSendObjectToPM"
        Me.tsmiSendObjectToPM.Size = New System.Drawing.Size(248, 22)
        Me.tsmiSendObjectToPM.Text = "Send Object to PM"
        '
        'tsmiAddNote
        '
        Me.tsmiAddNote.Name = "tsmiAddNote"
        Me.tsmiAddNote.Size = New System.Drawing.Size(248, 22)
        Me.tsmiAddNote.Text = "Add Note"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(245, 6)
        '
        'tsmi_Alerts_Copy_All
        '
        Me.tsmi_Alerts_Copy_All.Name = "tsmi_Alerts_Copy_All"
        Me.tsmi_Alerts_Copy_All.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Alerts_Copy_All.Text = "Copy to Clipboard - All"
        '
        'tsmi_Alerts_Copy_SelectionWOHeader
        '
        Me.tsmi_Alerts_Copy_SelectionWOHeader.Name = "tsmi_Alerts_Copy_SelectionWOHeader"
        Me.tsmi_Alerts_Copy_SelectionWOHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Alerts_Copy_SelectionWOHeader.Text = "Copy - Selection Without Header"
        '
        'tsmi_Alerts_Copy_SelectionWithHeader
        '
        Me.tsmi_Alerts_Copy_SelectionWithHeader.Name = "tsmi_Alerts_Copy_SelectionWithHeader"
        Me.tsmi_Alerts_Copy_SelectionWithHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Alerts_Copy_SelectionWithHeader.Text = "Copy - Selection With Header"
        '
        'gvAlerts
        '
        Me.gvAlerts.GridControl = Me.grdAlerts
        Me.gvAlerts.Name = "gvAlerts"
        Me.gvAlerts.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvAlerts.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvAlerts.OptionsBehavior.Editable = False
        Me.gvAlerts.OptionsBehavior.ReadOnly = True
        Me.gvAlerts.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvAlerts.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvAlerts.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvAlerts.OptionsView.ColumnAutoWidth = False
        Me.gvAlerts.OptionsView.ShowGroupPanel = False
        '
        'xtpSuppressed
        '
        Me.xtpSuppressed.Controls.Add(Me.TableLayoutPanel10)
        Me.xtpSuppressed.Name = "xtpSuppressed"
        Me.xtpSuppressed.Size = New System.Drawing.Size(432, 216)
        Me.xtpSuppressed.Text = "Suppressed"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.grdSuppressed, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(432, 216)
        Me.TableLayoutPanel10.TabIndex = 0
        '
        'grdSuppressed
        '
        Me.grdSuppressed.ContextMenuStrip = Me.cmsGridSuppressed
        Me.grdSuppressed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSuppressed.Location = New System.Drawing.Point(3, 3)
        Me.grdSuppressed.MainView = Me.gvSuppressed
        Me.grdSuppressed.Name = "grdSuppressed"
        Me.grdSuppressed.Size = New System.Drawing.Size(426, 210)
        Me.grdSuppressed.TabIndex = 2
        Me.grdSuppressed.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvSuppressed})
        '
        'cmsGridSuppressed
        '
        Me.cmsGridSuppressed.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiDeleteSuppression, Me.tsmiExtentSuppression, Me.ToolStripMenuItem2, Me.tsmi_Suppressed_Copy_All, Me.tsmi_Suppressed_Copy_SelectionWOHeader, Me.tsmi_Suppressed_Copy_SelectionWithHeader})
        Me.cmsGridSuppressed.Name = "cmsGridSuppressed"
        Me.cmsGridSuppressed.Size = New System.Drawing.Size(249, 120)
        '
        'tsmiDeleteSuppression
        '
        Me.tsmiDeleteSuppression.Name = "tsmiDeleteSuppression"
        Me.tsmiDeleteSuppression.Size = New System.Drawing.Size(248, 22)
        Me.tsmiDeleteSuppression.Text = "Delete Suppression"
        '
        'tsmiExtentSuppression
        '
        Me.tsmiExtentSuppression.Name = "tsmiExtentSuppression"
        Me.tsmiExtentSuppression.Size = New System.Drawing.Size(248, 22)
        Me.tsmiExtentSuppression.Text = "Extent Suppression"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(245, 6)
        '
        'tsmi_Suppressed_Copy_All
        '
        Me.tsmi_Suppressed_Copy_All.Name = "tsmi_Suppressed_Copy_All"
        Me.tsmi_Suppressed_Copy_All.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Suppressed_Copy_All.Text = "Copy to Clipboard - All"
        '
        'tsmi_Suppressed_Copy_SelectionWOHeader
        '
        Me.tsmi_Suppressed_Copy_SelectionWOHeader.Name = "tsmi_Suppressed_Copy_SelectionWOHeader"
        Me.tsmi_Suppressed_Copy_SelectionWOHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Suppressed_Copy_SelectionWOHeader.Text = "Copy - Selection Without Header"
        '
        'tsmi_Suppressed_Copy_SelectionWithHeader
        '
        Me.tsmi_Suppressed_Copy_SelectionWithHeader.Name = "tsmi_Suppressed_Copy_SelectionWithHeader"
        Me.tsmi_Suppressed_Copy_SelectionWithHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_Suppressed_Copy_SelectionWithHeader.Text = "Copy - Selection With Header"
        '
        'gvSuppressed
        '
        Me.gvSuppressed.GridControl = Me.grdSuppressed
        Me.gvSuppressed.Name = "gvSuppressed"
        Me.gvSuppressed.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSuppressed.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSuppressed.OptionsBehavior.Editable = False
        Me.gvSuppressed.OptionsBehavior.ReadOnly = True
        Me.gvSuppressed.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSuppressed.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSuppressed.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvSuppressed.OptionsSelection.MultiSelect = True
        Me.gvSuppressed.OptionsView.ShowGroupPanel = False
        '
        'xtpScore
        '
        Me.xtpScore.Controls.Add(Me.TableLayoutPanel12)
        Me.xtpScore.Name = "xtpScore"
        Me.xtpScore.Size = New System.Drawing.Size(432, 216)
        Me.xtpScore.Text = "Score"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 1
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.grdScore, 0, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(432, 216)
        Me.TableLayoutPanel12.TabIndex = 1
        '
        'grdScore
        '
        Me.grdScore.ContextMenuStrip = Me.cms_ScoreGrid
        Me.grdScore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdScore.Location = New System.Drawing.Point(3, 3)
        Me.grdScore.MainView = Me.gvScore
        Me.grdScore.Name = "grdScore"
        Me.grdScore.Size = New System.Drawing.Size(426, 210)
        Me.grdScore.TabIndex = 1
        Me.grdScore.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvScore})
        '
        'cms_ScoreGrid
        '
        Me.cms_ScoreGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_ScoreRecordCount, Me.ToolStripSeparator1, Me.tsmi_Send2Alerts, Me.tsmi_Send2PM})
        Me.cms_ScoreGrid.Name = "cm_GridViewMap"
        Me.cms_ScoreGrid.Size = New System.Drawing.Size(154, 76)
        '
        'tsmi_ScoreRecordCount
        '
        Me.tsmi_ScoreRecordCount.Enabled = False
        Me.tsmi_ScoreRecordCount.Name = "tsmi_ScoreRecordCount"
        Me.tsmi_ScoreRecordCount.Size = New System.Drawing.Size(153, 22)
        Me.tsmi_ScoreRecordCount.Text = "Record Count: "
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(150, 6)
        '
        'tsmi_Send2Alerts
        '
        Me.tsmi_Send2Alerts.Name = "tsmi_Send2Alerts"
        Me.tsmi_Send2Alerts.Size = New System.Drawing.Size(153, 22)
        Me.tsmi_Send2Alerts.Text = "Send to Alerts"
        '
        'tsmi_Send2PM
        '
        Me.tsmi_Send2PM.Name = "tsmi_Send2PM"
        Me.tsmi_Send2PM.Size = New System.Drawing.Size(153, 22)
        Me.tsmi_Send2PM.Text = "Send to PM"
        '
        'gvScore
        '
        Me.gvScore.GridControl = Me.grdScore
        Me.gvScore.Name = "gvScore"
        Me.gvScore.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScore.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScore.OptionsBehavior.Editable = False
        Me.gvScore.OptionsBehavior.ReadOnly = True
        Me.gvScore.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScore.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScore.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScore.OptionsView.ColumnAutoWidth = False
        Me.gvScore.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 3
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.btnAlertManager, 1, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.btnRefresh, 2, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(440, 33)
        Me.TableLayoutPanel11.TabIndex = 2
        '
        'btnAlertManager
        '
        Me.btnAlertManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAlertManager.Location = New System.Drawing.Point(263, 3)
        Me.btnAlertManager.Name = "btnAlertManager"
        Me.btnAlertManager.Size = New System.Drawing.Size(84, 27)
        Me.btnAlertManager.TabIndex = 0
        Me.btnAlertManager.Text = "Alert Manager"
        '
        'btnRefresh
        '
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefresh.Location = New System.Drawing.Point(353, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(84, 27)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.gcCorrelation, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.gcRules, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(842, 445)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'gcCorrelation
        '
        Me.gcCorrelation.Controls.Add(Me.TableLayoutPanel4)
        Me.gcCorrelation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCorrelation.Location = New System.Drawing.Point(3, 225)
        Me.gcCorrelation.Name = "gcCorrelation"
        Me.gcCorrelation.Size = New System.Drawing.Size(836, 217)
        Me.gcCorrelation.TabIndex = 3
        Me.gcCorrelation.Text = "Correlation"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.gcNegative, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.gcPositive, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(832, 192)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'gcNegative
        '
        Me.gcNegative.Controls.Add(Me.grdNegative)
        Me.gcNegative.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcNegative.Location = New System.Drawing.Point(499, 3)
        Me.gcNegative.Name = "gcNegative"
        Me.gcNegative.Size = New System.Drawing.Size(330, 186)
        Me.gcNegative.TabIndex = 2
        Me.gcNegative.Text = "Negative"
        '
        'grdNegative
        '
        Me.grdNegative.AllowDrop = True
        Me.grdNegative.ContextMenuStrip = Me.cm_CopyGridData
        Me.grdNegative.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdNegative.Location = New System.Drawing.Point(2, 23)
        Me.grdNegative.MainView = Me.gvNegative
        Me.grdNegative.Name = "grdNegative"
        Me.grdNegative.Size = New System.Drawing.Size(326, 161)
        Me.grdNegative.TabIndex = 4
        Me.grdNegative.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvNegative})
        '
        'gvNegative
        '
        Me.gvNegative.GridControl = Me.grdNegative
        Me.gvNegative.Name = "gvNegative"
        Me.gvNegative.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNegative.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNegative.OptionsBehavior.Editable = False
        Me.gvNegative.OptionsBehavior.ReadOnly = True
        Me.gvNegative.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNegative.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNegative.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNegative.OptionsSelection.MultiSelect = True
        Me.gvNegative.OptionsView.ColumnAutoWidth = False
        Me.gvNegative.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.lblCorrelationFilter, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.cmbCorrType, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel6, 0, 2)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 3
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(154, 186)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'lblCorrelationFilter
        '
        Me.lblCorrelationFilter.Appearance.Options.UseTextOptions = True
        Me.lblCorrelationFilter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblCorrelationFilter.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblCorrelationFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCorrelationFilter.Location = New System.Drawing.Point(3, 30)
        Me.lblCorrelationFilter.Name = "lblCorrelationFilter"
        Me.lblCorrelationFilter.Size = New System.Drawing.Size(148, 21)
        Me.lblCorrelationFilter.TabIndex = 0
        Me.lblCorrelationFilter.Text = "Correlation Filter"
        '
        'cmbCorrType
        '
        Me.cmbCorrType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbCorrType.Location = New System.Drawing.Point(3, 3)
        Me.cmbCorrType.Name = "cmbCorrType"
        Me.cmbCorrType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCorrType.Size = New System.Drawing.Size(148, 20)
        Me.cmbCorrType.TabIndex = 1
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.tbcCorrelationFilter, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 57)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(148, 126)
        Me.TableLayoutPanel6.TabIndex = 2
        '
        'tbcCorrelationFilter
        '
        Me.tbcCorrelationFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcCorrelationFilter.EditValue = Nothing
        Me.tbcCorrelationFilter.Location = New System.Drawing.Point(3, 3)
        Me.tbcCorrelationFilter.Name = "tbcCorrelationFilter"
        Me.tbcCorrelationFilter.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbcCorrelationFilter.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        TrackBarLabel1.Label = "Weak Correlation"
        TrackBarLabel2.Label = "Medium Correlation"
        TrackBarLabel2.Value = 1
        TrackBarLabel3.Label = "Strong Correlation"
        TrackBarLabel3.Value = 2
        Me.tbcCorrelationFilter.Properties.Labels.AddRange(New DevExpress.XtraEditors.Repository.TrackBarLabel() {TrackBarLabel1, TrackBarLabel2, TrackBarLabel3})
        Me.tbcCorrelationFilter.Properties.LargeChange = 1
        Me.tbcCorrelationFilter.Properties.Maximum = 2
        Me.tbcCorrelationFilter.Properties.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbcCorrelationFilter.Properties.ShowLabels = True
        Me.tbcCorrelationFilter.Size = New System.Drawing.Size(142, 120)
        Me.tbcCorrelationFilter.TabIndex = 2
        '
        'gcPositive
        '
        Me.gcPositive.Controls.Add(Me.grdPositive)
        Me.gcPositive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcPositive.Location = New System.Drawing.Point(163, 3)
        Me.gcPositive.Name = "gcPositive"
        Me.gcPositive.Size = New System.Drawing.Size(330, 186)
        Me.gcPositive.TabIndex = 1
        Me.gcPositive.Text = "Positive"
        '
        'grdPositive
        '
        Me.grdPositive.AllowDrop = True
        Me.grdPositive.ContextMenuStrip = Me.cm_CopyGridData
        Me.grdPositive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPositive.Location = New System.Drawing.Point(2, 23)
        Me.grdPositive.MainView = Me.gvPositive
        Me.grdPositive.Name = "grdPositive"
        Me.grdPositive.Size = New System.Drawing.Size(326, 161)
        Me.grdPositive.TabIndex = 3
        Me.grdPositive.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvPositive})
        '
        'gvPositive
        '
        Me.gvPositive.GridControl = Me.grdPositive
        Me.gvPositive.Name = "gvPositive"
        Me.gvPositive.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvPositive.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvPositive.OptionsBehavior.Editable = False
        Me.gvPositive.OptionsBehavior.ReadOnly = True
        Me.gvPositive.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPositive.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPositive.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPositive.OptionsSelection.MultiSelect = True
        Me.gvPositive.OptionsView.ColumnAutoWidth = False
        Me.gvPositive.OptionsView.ShowGroupPanel = False
        '
        'gcRules
        '
        Me.gcRules.Controls.Add(Me.grdRulesDetected)
        Me.gcRules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcRules.Location = New System.Drawing.Point(3, 3)
        Me.gcRules.Name = "gcRules"
        Me.gcRules.Size = New System.Drawing.Size(836, 216)
        Me.gcRules.TabIndex = 2
        Me.gcRules.Text = "Rules"
        '
        'grdRulesDetected
        '
        Me.grdRulesDetected.ContextMenuStrip = Me.cm_CopyGridData
        Me.grdRulesDetected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRulesDetected.Location = New System.Drawing.Point(2, 23)
        Me.grdRulesDetected.MainView = Me.gvRulesDetected
        Me.grdRulesDetected.Name = "grdRulesDetected"
        Me.grdRulesDetected.Size = New System.Drawing.Size(832, 191)
        Me.grdRulesDetected.TabIndex = 2
        Me.grdRulesDetected.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvRulesDetected})
        '
        'gvRulesDetected
        '
        Me.gvRulesDetected.GridControl = Me.grdRulesDetected
        Me.gvRulesDetected.Name = "gvRulesDetected"
        Me.gvRulesDetected.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvRulesDetected.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvRulesDetected.OptionsBehavior.Editable = False
        Me.gvRulesDetected.OptionsBehavior.ReadOnly = True
        Me.gvRulesDetected.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvRulesDetected.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvRulesDetected.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvRulesDetected.OptionsView.ColumnAutoWidth = False
        Me.gvRulesDetected.OptionsView.ShowGroupPanel = False
        '
        'sccBottom
        '
        Me.sccBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccBottom.Location = New System.Drawing.Point(0, 0)
        Me.sccBottom.Name = "sccBottom"
        '
        'sccBottom.Panel1
        '
        Me.sccBottom.Panel1.Controls.Add(Me.gcChartSettings)
        Me.sccBottom.Panel1.MinSize = 170
        Me.sccBottom.Panel1.Text = "Chart Settings"
        '
        'sccBottom.Panel2
        '
        Me.sccBottom.Panel2.Controls.Add(Me.sccChart)
        Me.sccBottom.Panel2.MinSize = 500
        Me.sccBottom.Panel2.Text = "Chart"
        Me.sccBottom.Size = New System.Drawing.Size(1302, 281)
        Me.sccBottom.SplitterPosition = 170
        Me.sccBottom.TabIndex = 0
        Me.sccBottom.Text = "SplitContainerControl1"
        '
        'gcChartSettings
        '
        Me.gcChartSettings.Controls.Add(Me.TableLayoutPanel7)
        Me.gcChartSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcChartSettings.Location = New System.Drawing.Point(0, 0)
        Me.gcChartSettings.Name = "gcChartSettings"
        Me.gcChartSettings.Size = New System.Drawing.Size(170, 281)
        Me.gcChartSettings.TabIndex = 0
        Me.gcChartSettings.Text = "Chart Settings"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel8, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(166, 256)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.42857!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.57143!))
        Me.TableLayoutPanel8.Controls.Add(Me.tglAlertTest, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.lblDataPoint, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.seDataPoint, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btnClearChart, 1, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.ceShowAlertDetected, 0, 3)
        Me.TableLayoutPanel8.Controls.Add(Me.ceShowAlertTriggered, 0, 4)
        Me.TableLayoutPanel8.Controls.Add(Me.ceShowKPIBreach, 0, 5)
        Me.TableLayoutPanel8.Controls.Add(Me.ceShowSlidingWindow, 0, 6)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 7
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(160, 179)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'tglAlertTest
        '
        Me.tglAlertTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tglAlertTest.Location = New System.Drawing.Point(3, 29)
        Me.tglAlertTest.LookAndFeel.SkinName = "McSkin"
        Me.tglAlertTest.LookAndFeel.UseDefaultLookAndFeel = False
        Me.tglAlertTest.Name = "tglAlertTest"
        Me.tglAlertTest.Size = New System.Drawing.Size(76, 24)
        Me.tglAlertTest.TabIndex = 11
        Me.tglAlertTest.Text = "Show Grid"
        Me.tglAlertTest.ToggleState = System.Windows.Forms.CheckState.Unchecked
        '
        'lblDataPoint
        '
        Me.lblDataPoint.Appearance.Options.UseTextOptions = True
        Me.lblDataPoint.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblDataPoint.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblDataPoint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDataPoint.Location = New System.Drawing.Point(3, 3)
        Me.lblDataPoint.Name = "lblDataPoint"
        Me.lblDataPoint.Size = New System.Drawing.Size(76, 20)
        Me.lblDataPoint.TabIndex = 1
        Me.lblDataPoint.Text = "#Data Point"
        '
        'seDataPoint
        '
        Me.seDataPoint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.seDataPoint.EditValue = New Decimal(New Integer() {180, 0, 0, 0})
        Me.seDataPoint.Location = New System.Drawing.Point(85, 3)
        Me.seDataPoint.Name = "seDataPoint"
        Me.seDataPoint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seDataPoint.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.seDataPoint.Properties.Mask.EditMask = "N00"
        Me.seDataPoint.Size = New System.Drawing.Size(72, 20)
        Me.seDataPoint.TabIndex = 2
        '
        'btnClearChart
        '
        Me.btnClearChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClearChart.Location = New System.Drawing.Point(85, 29)
        Me.btnClearChart.Name = "btnClearChart"
        Me.btnClearChart.Size = New System.Drawing.Size(72, 24)
        Me.btnClearChart.TabIndex = 3
        Me.btnClearChart.Text = "Clear Chart"
        '
        'ceShowAlertDetected
        '
        Me.TableLayoutPanel8.SetColumnSpan(Me.ceShowAlertDetected, 2)
        Me.ceShowAlertDetected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceShowAlertDetected.EditValue = True
        Me.ceShowAlertDetected.Location = New System.Drawing.Point(5, 79)
        Me.ceShowAlertDetected.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceShowAlertDetected.Name = "ceShowAlertDetected"
        Me.ceShowAlertDetected.Properties.Caption = "Show Alert Detected"
        Me.ceShowAlertDetected.Size = New System.Drawing.Size(152, 19)
        Me.ceShowAlertDetected.TabIndex = 12
        '
        'ceShowAlertTriggered
        '
        Me.TableLayoutPanel8.SetColumnSpan(Me.ceShowAlertTriggered, 2)
        Me.ceShowAlertTriggered.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceShowAlertTriggered.EditValue = True
        Me.ceShowAlertTriggered.Location = New System.Drawing.Point(5, 104)
        Me.ceShowAlertTriggered.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceShowAlertTriggered.Name = "ceShowAlertTriggered"
        Me.ceShowAlertTriggered.Properties.Caption = "Show Alert Triggered"
        Me.ceShowAlertTriggered.Size = New System.Drawing.Size(152, 19)
        Me.ceShowAlertTriggered.TabIndex = 13
        '
        'ceShowKPIBreach
        '
        Me.TableLayoutPanel8.SetColumnSpan(Me.ceShowKPIBreach, 2)
        Me.ceShowKPIBreach.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceShowKPIBreach.EditValue = True
        Me.ceShowKPIBreach.Location = New System.Drawing.Point(5, 129)
        Me.ceShowKPIBreach.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceShowKPIBreach.Name = "ceShowKPIBreach"
        Me.ceShowKPIBreach.Properties.Caption = "Show KPI Breach"
        Me.ceShowKPIBreach.Size = New System.Drawing.Size(152, 19)
        Me.ceShowKPIBreach.TabIndex = 14
        '
        'ceShowSlidingWindow
        '
        Me.TableLayoutPanel8.SetColumnSpan(Me.ceShowSlidingWindow, 2)
        Me.ceShowSlidingWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceShowSlidingWindow.EditValue = True
        Me.ceShowSlidingWindow.Location = New System.Drawing.Point(5, 154)
        Me.ceShowSlidingWindow.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceShowSlidingWindow.Name = "ceShowSlidingWindow"
        Me.ceShowSlidingWindow.Properties.Caption = "Show Alert Sliding Window"
        Me.ceShowSlidingWindow.Size = New System.Drawing.Size(152, 22)
        Me.ceShowSlidingWindow.TabIndex = 15
        '
        'sccChart
        '
        Me.sccChart.Collapsed = True
        Me.sccChart.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.sccChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccChart.Horizontal = False
        Me.sccChart.Location = New System.Drawing.Point(0, 0)
        Me.sccChart.Name = "sccChart"
        '
        'sccChart.Panel1
        '
        Me.sccChart.Panel1.Controls.Add(Me.chAnomaly)
        Me.sccChart.Panel1.Text = "Panel1"
        '
        'sccChart.Panel2
        '
        Me.sccChart.Panel2.Controls.Add(Me.gcChartData)
        Me.sccChart.Panel2.MinSize = 100
        Me.sccChart.Panel2.Text = "Panel2"
        Me.sccChart.Size = New System.Drawing.Size(1122, 281)
        Me.sccChart.SplitterPosition = 122
        Me.sccChart.TabIndex = 13
        Me.sccChart.Text = "SplitContainerControl1"
        '
        'chAnomaly
        '
        Me.chAnomaly.AllowDrop = True
        Me.chAnomaly.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Me.chAnomaly.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Me.chAnomaly.AutoScroll = True
        Me.chAnomaly.Background.Color = System.Drawing.Color.White
        Annotation1.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        Annotation1.DynamicSize = True
        BoxHeaderOptions1.Background.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.[Default]
        BoxHeaderOptions1.Label.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        BoxHeaderOptions1.Label.Shadow.Color = System.Drawing.Color.Transparent
        BoxHeaderOptions1.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions1.Shadow.Color = System.Drawing.Color.Transparent
        Annotation1.Header = BoxHeaderOptions1
        Annotation1.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation1.Line.Color = System.Drawing.Color.Gray
        Annotation1.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation1.Padding = 2
        Annotation1.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Annotation1.Shadow.Depth = 1
        Annotation1.Shadow.ExpandBy = 2.0!
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(1121, 270)
        Annotation1.Visible = True
        Me.chAnomaly.Box = Annotation1
        Me.chAnomaly.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.chAnomaly.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Me.chAnomaly.ChartArea.DefaultElement.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.chAnomaly.ChartArea.DefaultElement.DefaultSubValue.Visible = True
        Me.chAnomaly.ChartArea.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chAnomaly.ChartArea.DefaultElement.SmartLabel.Color = System.Drawing.Color.Empty
        Me.chAnomaly.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.chAnomaly.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.chAnomaly.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.chAnomaly.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.chAnomaly.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        BoxHeaderOptions2.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions2.Shadow.Color = System.Drawing.Color.Transparent
        Me.chAnomaly.ChartArea.LegendBox.Header = BoxHeaderOptions2
        Me.chAnomaly.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.chAnomaly.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.chAnomaly.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.chAnomaly.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.chAnomaly.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.chAnomaly.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chAnomaly.ChartArea.LegendBox.LabelStyle.Font = New System.Drawing.Font("Trebuchet MS", 8.0!)
        Me.chAnomaly.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.chAnomaly.ChartArea.LegendBox.Padding = 4
        Me.chAnomaly.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.chAnomaly.ChartArea.LegendBox.Shadow.ExpandBy = 2.0!
        Me.chAnomaly.ChartArea.LegendBox.Visible = True
        Me.chAnomaly.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.chAnomaly.ChartArea.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.chAnomaly.ChartArea.Shadow.Depth = 1
        Me.chAnomaly.ChartArea.Shadow.ExpandBy = 2.0!
        Me.chAnomaly.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.chAnomaly.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        BoxHeaderOptions3.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions3.Shadow.Color = System.Drawing.Color.Transparent
        Me.chAnomaly.ChartArea.TitleBox.Header = BoxHeaderOptions3
        Me.chAnomaly.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chAnomaly.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.chAnomaly.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.chAnomaly.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.chAnomaly.ChartArea.TitleBox.Shadow.ExpandBy = 2.0!
        Me.chAnomaly.ChartArea.TitleBox.Visible = True
        Me.chAnomaly.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.chAnomaly.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.chAnomaly.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chAnomaly.ChartArea.XAxis.MinorTimeIntervalAdvanced.Unit = dotnetCHARTING.WinForms.TimeInterval.None
        Me.chAnomaly.ChartArea.XAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.chAnomaly.ChartArea.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.chAnomaly.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chAnomaly.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chAnomaly.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.chAnomaly.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.chAnomaly.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.chAnomaly.ChartArea.YAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.chAnomaly.ChartArea.YAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.chAnomaly.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chAnomaly.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chAnomaly.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.chAnomaly.DataGrid = Nothing
        Me.chAnomaly.DefaultElement.DefaultSubValue.Visible = True
        Me.chAnomaly.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chAnomaly.DefaultShadow.ExpandBy = 2.0!
        Me.chAnomaly.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chAnomaly.LegacyMode = False
        Me.chAnomaly.Location = New System.Drawing.Point(0, 0)
        Me.chAnomaly.MinimumSize = New System.Drawing.Size(133, 62)
        Me.chAnomaly.Name = "chAnomaly"
        Me.chAnomaly.NoDataLabel.Text = "No Data"
        Me.chAnomaly.Size = New System.Drawing.Size(1122, 271)
        Me.chAnomaly.StartDateOfYear = New Date(CType(0, Long))
        Me.chAnomaly.TabIndex = 12
        Me.chAnomaly.TempDirectory = "C:\Users\Charul\AppData\Local\Temp\"
        Me.chAnomaly.View3D = View3D1
        '
        'gcChartData
        '
        Me.gcChartData.ContextMenuStrip = Me.cm_CopyGridData
        Me.gcChartData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcChartData.Location = New System.Drawing.Point(0, 0)
        Me.gcChartData.MainView = Me.gvChartData
        Me.gcChartData.Name = "gcChartData"
        Me.gcChartData.Size = New System.Drawing.Size(0, 0)
        Me.gcChartData.TabIndex = 3
        Me.gcChartData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvChartData})
        '
        'gvChartData
        '
        Me.gvChartData.GridControl = Me.gcChartData
        Me.gvChartData.Name = "gvChartData"
        Me.gvChartData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvChartData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvChartData.OptionsBehavior.Editable = False
        Me.gvChartData.OptionsBehavior.ReadOnly = True
        Me.gvChartData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvChartData.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvChartData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvChartData.OptionsView.ColumnAutoWidth = False
        Me.gvChartData.OptionsView.ShowGroupPanel = False
        '
        'frmAnomaly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1302, 736)
        Me.Controls.Add(Me.sccMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmAnomaly.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1000, 650)
        Me.Name = "frmAnomaly"
        Me.Text = "Anomaly Detection"
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel1.ResumeLayout(False)
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel2.ResumeLayout(False)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.ResumeLayout(False)
        CType(Me.sccTop.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTop.Panel1.ResumeLayout(False)
        CType(Me.sccTop.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTop.Panel2.ResumeLayout(False)
        CType(Me.sccTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTop.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.gcNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcNotes.ResumeLayout(False)
        CType(Me.grdNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_CopyGridData.ResumeLayout(False)
        CType(Me.gvNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcAlerts.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.xtcAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcAlerts.ResumeLayout(False)
        Me.xtpAlert.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        CType(Me.grdAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsGridAlerts.ResumeLayout(False)
        CType(Me.gvAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpSuppressed.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        CType(Me.grdSuppressed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsGridSuppressed.ResumeLayout(False)
        CType(Me.gvSuppressed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpScore.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        CType(Me.grdScore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cms_ScoreGrid.ResumeLayout(False)
        CType(Me.gvScore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.gcCorrelation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcCorrelation.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.gcNegative, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcNegative.ResumeLayout(False)
        CType(Me.grdNegative, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvNegative, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.cmbCorrType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.tbcCorrelationFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbcCorrelationFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcPositive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcPositive.ResumeLayout(False)
        CType(Me.grdPositive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPositive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcRules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcRules.ResumeLayout(False)
        CType(Me.grdRulesDetected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvRulesDetected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccBottom.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccBottom.Panel1.ResumeLayout(False)
        CType(Me.sccBottom.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccBottom.Panel2.ResumeLayout(False)
        CType(Me.sccBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccBottom.ResumeLayout(False)
        CType(Me.gcChartSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcChartSettings.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.seDataPoint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowAlertDetected.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowAlertTriggered.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowKPIBreach.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowSlidingWindow.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccChart.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccChart.Panel1.ResumeLayout(False)
        CType(Me.sccChart.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccChart.Panel2.ResumeLayout(False)
        CType(Me.sccChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccChart.ResumeLayout(False)
        CType(Me.chAnomaly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcChartData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvChartData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sccMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccBottom As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcCorrelation As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcRules As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcAlerts As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnAlertManager As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grdAlerts As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvAlerts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcNegative As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdNegative As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvNegative As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCorrelationFilter As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbCorrType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tbcCorrelationFilter As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents gcPositive As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdPositive As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvPositive As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdRulesDetected As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvRulesDetected As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcChartSettings As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblDataPoint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seDataPoint As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents sccTop As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents cmsGridAlerts As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiModifyAlert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiSuppressAlertAllObjects As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiSendObjectToPM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gcNotes As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdNotes As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvNotes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tsmiSuppressAlertSelectedObject As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiAddNote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chAnomaly As dotnetCHARTING.WinForms.Chart
    Friend WithEvents btnClearChart As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xtcAlerts As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpAlert As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents xtpSuppressed As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdSuppressed As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvSuppressed As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmsGridSuppressed As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiDeleteSuppression As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiExtentSuppression As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sccChart As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gcChartData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvChartData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tglAlertTest As IOS.Library.IOSToggleButton
    Friend WithEvents ceShowAlertDetected As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceShowAlertTriggered As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceShowKPIBreach As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceShowSlidingWindow As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmi_Alerts_Copy_All As ToolStripMenuItem
    Friend WithEvents tsmi_Alerts_Copy_SelectionWOHeader As ToolStripMenuItem
    Friend WithEvents cm_CopyGridData As ContextMenuStrip
    Friend WithEvents tsmi_RecordCount As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents tsmi_Copy_All As ToolStripMenuItem
    Friend WithEvents tsmi_Copy_SelectionWOHeader As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents tsmi_Suppressed_Copy_All As ToolStripMenuItem
    Friend WithEvents tsmi_Suppressed_Copy_SelectionWOHeader As ToolStripMenuItem
    Friend WithEvents xtpScore As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents grdScore As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvScore As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cms_ScoreGrid As ContextMenuStrip
    Friend WithEvents tsmi_ScoreRecordCount As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsmi_Send2Alerts As ToolStripMenuItem
    Friend WithEvents tsmi_Send2PM As ToolStripMenuItem
    Friend WithEvents tsmi_Alerts_Copy_SelectionWithHeader As ToolStripMenuItem
    Friend WithEvents tsmi_Copy_SelectionWithHeader As ToolStripMenuItem
    Friend WithEvents tsmi_Suppressed_Copy_SelectionWithHeader As ToolStripMenuItem
End Class
