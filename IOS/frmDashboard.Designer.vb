<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDashboard))
        Me.ce_TicketOpen = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.gc_IssueStatistics = New DevExpress.XtraEditors.GroupControl()
        Me.lbl_LongOpen = New DevExpress.XtraEditors.LabelControl()
        Me.lbl_TimeOpen = New DevExpress.XtraEditors.LabelControl()
        Me.lbl_TicketsClosed = New DevExpress.XtraEditors.LabelControl()
        Me.lbl_TicketsAssigned = New DevExpress.XtraEditors.LabelControl()
        Me.lbl_TicketsOpen = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.gc_TicketScore = New DevExpress.XtraEditors.GroupControl()
        Me.sccAnalytics = New DevExpress.XtraEditors.SplitContainerControl()
        Me.dgvIOS_Tickets = New DevExpress.XtraGrid.GridControl()
        Me.cm_IOS_Tickets = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_TicketMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_TicketMapAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_TicketObjectTree = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_TicketLaunch = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ticketremedy = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_ManualUpdateStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvIOS_Tickets = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xTabControl_Tickets = New DevExpress.XtraTab.XtraTabControl()
        Me.xTabPage_CellHistory = New DevExpress.XtraTab.XtraTabPage()
        Me.xTabPage_TicketDetails = New DevExpress.XtraTab.XtraTabPage()
        Me.tlv_TicketDetails = New DevExpress.XtraTreeList.TreeList()
        Me.xTabPage_TicketHistory = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlv_TicketHistory = New DevExpress.XtraTreeList.TreeList()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_TicketUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.txt_TicketComment = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.dcmb_TicketStatusUpdate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.xtcMain = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpAnalytics = New DevExpress.XtraTab.XtraTabPage()
        Me.tlpAnalytics = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpSubAnalytics = New System.Windows.Forms.TableLayoutPanel()
        Me.gc_TicketRoot = New DevExpress.XtraEditors.GroupControl()
        Me.btnRefreshDashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.xtpEvents = New DevExpress.XtraTab.XtraTabPage()
        Me.gcEvents = New DevExpress.XtraGrid.GridControl()
        Me.gvEvents = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtpReports = New DevExpress.XtraTab.XtraTabPage()
        Me.tlpDashboardMain = New System.Windows.Forms.TableLayoutPanel()
        Me.ReportViewer = New DevExpress.DashboardWin.DashboardViewer(Me.components)
        Me.tlpDashTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDesignDashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCreateDashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbDashboards = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnDeleteReport = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnSendToWebRptSrvr = New DevExpress.XtraEditors.SimpleButton()
        Me.grpRefreshDashboard = New DevExpress.XtraEditors.GroupControl()
        Me.tlpRefresh = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.SpinEditMonMode = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl42 = New DevExpress.XtraEditors.LabelControl()
        Me.rbAutomatic = New System.Windows.Forms.RadioButton()
        Me.rbManual = New System.Windows.Forms.RadioButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.timerCountdown = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.bgWorker = New System.ComponentModel.BackgroundWorker()
        CType(Me.ce_TicketOpen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_IssueStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gc_IssueStatistics.SuspendLayout()
        CType(Me.gc_TicketScore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccAnalytics, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccAnalytics.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccAnalytics.Panel1.SuspendLayout()
        CType(Me.sccAnalytics.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccAnalytics.Panel2.SuspendLayout()
        Me.sccAnalytics.SuspendLayout()
        CType(Me.dgvIOS_Tickets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_IOS_Tickets.SuspendLayout()
        CType(Me.gvIOS_Tickets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xTabControl_Tickets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xTabControl_Tickets.SuspendLayout()
        Me.xTabPage_TicketDetails.SuspendLayout()
        CType(Me.tlv_TicketDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xTabPage_TicketHistory.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.tlv_TicketHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.txt_TicketComment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dcmb_TicketStatusUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcMain.SuspendLayout()
        Me.xtpAnalytics.SuspendLayout()
        Me.tlpAnalytics.SuspendLayout()
        Me.tlpSubAnalytics.SuspendLayout()
        CType(Me.gc_TicketRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpEvents.SuspendLayout()
        CType(Me.gcEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpReports.SuspendLayout()
        Me.tlpDashboardMain.SuspendLayout()
        CType(Me.ReportViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpDashTop.SuspendLayout()
        CType(Me.cmbDashboards.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpRefreshDashboard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRefreshDashboard.SuspendLayout()
        Me.tlpRefresh.SuspendLayout()
        CType(Me.SpinEditMonMode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ce_TicketOpen
        '
        Me.ce_TicketOpen.EditValue = True
        Me.ce_TicketOpen.Location = New System.Drawing.Point(249, 33)
        Me.ce_TicketOpen.Name = "ce_TicketOpen"
        Me.ce_TicketOpen.Properties.Caption = ""
        Me.ce_TicketOpen.Size = New System.Drawing.Size(27, 20)
        Me.ce_TicketOpen.TabIndex = 16
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(167, 36)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl7.TabIndex = 14
        Me.LabelControl7.Text = "Open Isues"
        '
        'gc_IssueStatistics
        '
        Me.gc_IssueStatistics.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gc_IssueStatistics.Appearance.Options.UseBackColor = True
        Me.gc_IssueStatistics.Controls.Add(Me.ce_TicketOpen)
        Me.gc_IssueStatistics.Controls.Add(Me.LabelControl7)
        Me.gc_IssueStatistics.Controls.Add(Me.lbl_LongOpen)
        Me.gc_IssueStatistics.Controls.Add(Me.lbl_TimeOpen)
        Me.gc_IssueStatistics.Controls.Add(Me.lbl_TicketsClosed)
        Me.gc_IssueStatistics.Controls.Add(Me.lbl_TicketsAssigned)
        Me.gc_IssueStatistics.Controls.Add(Me.lbl_TicketsOpen)
        Me.gc_IssueStatistics.Controls.Add(Me.LabelControl9)
        Me.gc_IssueStatistics.Controls.Add(Me.LabelControl11)
        Me.gc_IssueStatistics.Controls.Add(Me.LabelControl12)
        Me.gc_IssueStatistics.Controls.Add(Me.LabelControl13)
        Me.gc_IssueStatistics.Controls.Add(Me.LabelControl14)
        Me.gc_IssueStatistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_IssueStatistics.Location = New System.Drawing.Point(3, 29)
        Me.gc_IssueStatistics.Name = "gc_IssueStatistics"
        Me.gc_IssueStatistics.Size = New System.Drawing.Size(362, 169)
        Me.gc_IssueStatistics.TabIndex = 1
        Me.gc_IssueStatistics.Text = "Issues Statistics"
        '
        'lbl_LongOpen
        '
        Me.lbl_LongOpen.Location = New System.Drawing.Point(119, 135)
        Me.lbl_LongOpen.Name = "lbl_LongOpen"
        Me.lbl_LongOpen.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lbl_LongOpen.Size = New System.Drawing.Size(10, 13)
        Me.lbl_LongOpen.TabIndex = 24
        Me.lbl_LongOpen.Text = "?"
        '
        'lbl_TimeOpen
        '
        Me.lbl_TimeOpen.Location = New System.Drawing.Point(119, 109)
        Me.lbl_TimeOpen.Name = "lbl_TimeOpen"
        Me.lbl_TimeOpen.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lbl_TimeOpen.Size = New System.Drawing.Size(10, 13)
        Me.lbl_TimeOpen.TabIndex = 23
        Me.lbl_TimeOpen.Text = "?"
        '
        'lbl_TicketsClosed
        '
        Me.lbl_TicketsClosed.Location = New System.Drawing.Point(119, 84)
        Me.lbl_TicketsClosed.Name = "lbl_TicketsClosed"
        Me.lbl_TicketsClosed.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lbl_TicketsClosed.Size = New System.Drawing.Size(10, 13)
        Me.lbl_TicketsClosed.TabIndex = 22
        Me.lbl_TicketsClosed.Text = "?"
        '
        'lbl_TicketsAssigned
        '
        Me.lbl_TicketsAssigned.Location = New System.Drawing.Point(119, 60)
        Me.lbl_TicketsAssigned.Name = "lbl_TicketsAssigned"
        Me.lbl_TicketsAssigned.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lbl_TicketsAssigned.Size = New System.Drawing.Size(10, 13)
        Me.lbl_TicketsAssigned.TabIndex = 21
        Me.lbl_TicketsAssigned.Text = "?"
        '
        'lbl_TicketsOpen
        '
        Me.lbl_TicketsOpen.Location = New System.Drawing.Point(119, 36)
        Me.lbl_TicketsOpen.Name = "lbl_TicketsOpen"
        Me.lbl_TicketsOpen.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lbl_TicketsOpen.Size = New System.Drawing.Size(10, 13)
        Me.lbl_TicketsOpen.TabIndex = 20
        Me.lbl_TicketsOpen.Text = "?"
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(26, 135)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(72, 13)
        Me.LabelControl9.TabIndex = 19
        Me.LabelControl9.Text = "Longest Open"
        '
        'LabelControl11
        '
        Me.LabelControl11.Location = New System.Drawing.Point(26, 109)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(79, 13)
        Me.LabelControl11.TabIndex = 18
        Me.LabelControl11.Text = "AVG Time Open"
        '
        'LabelControl12
        '
        Me.LabelControl12.Location = New System.Drawing.Point(26, 84)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(71, 13)
        Me.LabelControl12.TabIndex = 17
        Me.LabelControl12.Text = "Issues Closed"
        '
        'LabelControl13
        '
        Me.LabelControl13.Location = New System.Drawing.Point(26, 60)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(82, 13)
        Me.LabelControl13.TabIndex = 16
        Me.LabelControl13.Text = "Issues Assigned"
        '
        'LabelControl14
        '
        Me.LabelControl14.Location = New System.Drawing.Point(26, 36)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl14.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl14.TabIndex = 15
        Me.LabelControl14.Text = "Issues Open"
        '
        'gc_TicketScore
        '
        Me.gc_TicketScore.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gc_TicketScore.Appearance.Options.UseBackColor = True
        Me.gc_TicketScore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_TicketScore.Location = New System.Drawing.Point(3, 204)
        Me.gc_TicketScore.Name = "gc_TicketScore"
        Me.gc_TicketScore.Size = New System.Drawing.Size(362, 317)
        Me.gc_TicketScore.TabIndex = 1
        Me.gc_TicketScore.Text = "Issues Score Details"
        '
        'sccAnalytics
        '
        Me.sccAnalytics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccAnalytics.Horizontal = False
        Me.sccAnalytics.Location = New System.Drawing.Point(3, 3)
        Me.sccAnalytics.Name = "sccAnalytics"
        '
        'sccAnalytics.Panel1
        '
        Me.sccAnalytics.Panel1.Controls.Add(Me.dgvIOS_Tickets)
        Me.sccAnalytics.Panel1.Text = "Panel1"
        '
        'sccAnalytics.Panel2
        '
        Me.sccAnalytics.Panel2.Controls.Add(Me.xTabControl_Tickets)
        Me.sccAnalytics.Panel2.Text = "Panel2"
        Me.sccAnalytics.Size = New System.Drawing.Size(866, 817)
        Me.sccAnalytics.SplitterPosition = 386
        Me.sccAnalytics.TabIndex = 3
        Me.sccAnalytics.Text = "SplitContainerControl1"
        '
        'dgvIOS_Tickets
        '
        Me.dgvIOS_Tickets.ContextMenuStrip = Me.cm_IOS_Tickets
        Me.dgvIOS_Tickets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvIOS_Tickets.Location = New System.Drawing.Point(0, 0)
        Me.dgvIOS_Tickets.MainView = Me.gvIOS_Tickets
        Me.dgvIOS_Tickets.Name = "dgvIOS_Tickets"
        Me.dgvIOS_Tickets.Size = New System.Drawing.Size(866, 386)
        Me.dgvIOS_Tickets.TabIndex = 8
        Me.dgvIOS_Tickets.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvIOS_Tickets})
        '
        'cm_IOS_Tickets
        '
        Me.cm_IOS_Tickets.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_TicketMap, Me.tsmi_TicketMapAll, Me.tsmi_TicketObjectTree, Me.tsmi_TicketLaunch, Me.tsmi_ticketremedy, Me.ToolStripSeparator1, Me.tsmi_ManualUpdateStatus})
        Me.cm_IOS_Tickets.Name = "cm_IOS_Tickets"
        Me.cm_IOS_Tickets.Size = New System.Drawing.Size(191, 142)
        '
        'tsmi_TicketMap
        '
        Me.tsmi_TicketMap.Name = "tsmi_TicketMap"
        Me.tsmi_TicketMap.Size = New System.Drawing.Size(190, 22)
        Me.tsmi_TicketMap.Text = "Ticket - Map Cell"
        '
        'tsmi_TicketMapAll
        '
        Me.tsmi_TicketMapAll.Name = "tsmi_TicketMapAll"
        Me.tsmi_TicketMapAll.Size = New System.Drawing.Size(190, 22)
        Me.tsmi_TicketMapAll.Text = "Ticket - Map All Cells"
        '
        'tsmi_TicketObjectTree
        '
        Me.tsmi_TicketObjectTree.Name = "tsmi_TicketObjectTree"
        Me.tsmi_TicketObjectTree.Size = New System.Drawing.Size(190, 22)
        Me.tsmi_TicketObjectTree.Text = "Ticket - ObjectTree"
        '
        'tsmi_TicketLaunch
        '
        Me.tsmi_TicketLaunch.Name = "tsmi_TicketLaunch"
        Me.tsmi_TicketLaunch.Size = New System.Drawing.Size(190, 22)
        Me.tsmi_TicketLaunch.Text = "Ticket - Launch Stats"
        '
        'tsmi_ticketremedy
        '
        Me.tsmi_ticketremedy.Name = "tsmi_ticketremedy"
        Me.tsmi_ticketremedy.Size = New System.Drawing.Size(190, 22)
        Me.tsmi_ticketremedy.Text = "Ticket - View Remedy"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(187, 6)
        '
        'tsmi_ManualUpdateStatus
        '
        Me.tsmi_ManualUpdateStatus.Name = "tsmi_ManualUpdateStatus"
        Me.tsmi_ManualUpdateStatus.Size = New System.Drawing.Size(190, 22)
        Me.tsmi_ManualUpdateStatus.Text = "Manual Update Status"
        '
        'gvIOS_Tickets
        '
        Me.gvIOS_Tickets.GridControl = Me.dgvIOS_Tickets
        Me.gvIOS_Tickets.Name = "gvIOS_Tickets"
        Me.gvIOS_Tickets.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvIOS_Tickets.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvIOS_Tickets.OptionsBehavior.Editable = False
        Me.gvIOS_Tickets.OptionsBehavior.ReadOnly = True
        Me.gvIOS_Tickets.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvIOS_Tickets.OptionsView.ColumnAutoWidth = False
        Me.gvIOS_Tickets.OptionsView.ShowGroupPanel = False
        '
        'xTabControl_Tickets
        '
        Me.xTabControl_Tickets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xTabControl_Tickets.Location = New System.Drawing.Point(0, 0)
        Me.xTabControl_Tickets.Name = "xTabControl_Tickets"
        Me.xTabControl_Tickets.SelectedTabPage = Me.xTabPage_CellHistory
        Me.xTabControl_Tickets.Size = New System.Drawing.Size(866, 421)
        Me.xTabControl_Tickets.TabIndex = 0
        Me.xTabControl_Tickets.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xTabPage_CellHistory, Me.xTabPage_TicketDetails, Me.xTabPage_TicketHistory})
        '
        'xTabPage_CellHistory
        '
        Me.xTabPage_CellHistory.Name = "xTabPage_CellHistory"
        Me.xTabPage_CellHistory.Size = New System.Drawing.Size(864, 396)
        Me.xTabPage_CellHistory.Text = "Site History"
        '
        'xTabPage_TicketDetails
        '
        Me.xTabPage_TicketDetails.Controls.Add(Me.tlv_TicketDetails)
        Me.xTabPage_TicketDetails.Name = "xTabPage_TicketDetails"
        Me.xTabPage_TicketDetails.Size = New System.Drawing.Size(864, 396)
        Me.xTabPage_TicketDetails.Text = "Issue Details"
        '
        'tlv_TicketDetails
        '
        Me.tlv_TicketDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlv_TicketDetails.Location = New System.Drawing.Point(0, 0)
        Me.tlv_TicketDetails.Name = "tlv_TicketDetails"
        Me.tlv_TicketDetails.OptionsBehavior.Editable = False
        Me.tlv_TicketDetails.OptionsBehavior.ReadOnly = True
        Me.tlv_TicketDetails.OptionsCustomization.AllowSort = False
        Me.tlv_TicketDetails.OptionsMenu.EnableNodeMenu = False
        Me.tlv_TicketDetails.OptionsView.ShowHorzLines = False
        Me.tlv_TicketDetails.Size = New System.Drawing.Size(864, 396)
        Me.tlv_TicketDetails.TabIndex = 1
        '
        'xTabPage_TicketHistory
        '
        Me.xTabPage_TicketHistory.Controls.Add(Me.TableLayoutPanel4)
        Me.xTabPage_TicketHistory.Name = "xTabPage_TicketHistory"
        Me.xTabPage_TicketHistory.Size = New System.Drawing.Size(864, 396)
        Me.xTabPage_TicketHistory.Text = "Issue History"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.tlv_TicketHistory, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(864, 396)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'tlv_TicketHistory
        '
        Me.tlv_TicketHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlv_TicketHistory.Location = New System.Drawing.Point(3, 3)
        Me.tlv_TicketHistory.Name = "tlv_TicketHistory"
        Me.tlv_TicketHistory.OptionsBehavior.Editable = False
        Me.tlv_TicketHistory.OptionsBehavior.ReadOnly = True
        Me.tlv_TicketHistory.OptionsCustomization.AllowSort = False
        Me.tlv_TicketHistory.OptionsMenu.EnableNodeMenu = False
        Me.tlv_TicketHistory.OptionsView.ShowHorzLines = False
        Me.tlv_TicketHistory.Size = New System.Drawing.Size(858, 355)
        Me.tlv_TicketHistory.TabIndex = 6
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 4
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 272.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.btn_TicketUpdate, 3, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txt_TicketComment, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl2, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 364)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(858, 29)
        Me.TableLayoutPanel5.TabIndex = 5
        '
        'btn_TicketUpdate
        '
        Me.btn_TicketUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_TicketUpdate.Location = New System.Drawing.Point(768, 3)
        Me.btn_TicketUpdate.Name = "btn_TicketUpdate"
        Me.btn_TicketUpdate.Size = New System.Drawing.Size(87, 23)
        Me.btn_TicketUpdate.TabIndex = 0
        Me.btn_TicketUpdate.Text = "Update"
        '
        'txt_TicketComment
        '
        Me.txt_TicketComment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_TicketComment.Location = New System.Drawing.Point(496, 4)
        Me.txt_TicketComment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txt_TicketComment.Name = "txt_TicketComment"
        Me.txt_TicketComment.Size = New System.Drawing.Size(266, 20)
        Me.txt_TicketComment.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(438, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(52, 23)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Comment"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LabelControl3)
        Me.Panel1.Controls.Add(Me.dcmb_TicketStatusUpdate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(435, 29)
        Me.Panel1.TabIndex = 4
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(5, 8)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(103, 13)
        Me.LabelControl3.TabIndex = 3
        Me.LabelControl3.Text = "Update Issues Status"
        '
        'dcmb_TicketStatusUpdate
        '
        Me.dcmb_TicketStatusUpdate.EditValue = "Assigned"
        Me.dcmb_TicketStatusUpdate.Location = New System.Drawing.Point(112, 4)
        Me.dcmb_TicketStatusUpdate.Name = "dcmb_TicketStatusUpdate"
        Me.dcmb_TicketStatusUpdate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dcmb_TicketStatusUpdate.Properties.Items.AddRange(New Object() {"Assigned", "Closed"})
        Me.dcmb_TicketStatusUpdate.Size = New System.Drawing.Size(143, 20)
        Me.dcmb_TicketStatusUpdate.TabIndex = 2
        '
        'xtcMain
        '
        Me.xtcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcMain.Location = New System.Drawing.Point(0, 0)
        Me.xtcMain.Name = "xtcMain"
        Me.xtcMain.SelectedTabPage = Me.xtpAnalytics
        Me.xtcMain.Size = New System.Drawing.Size(1248, 848)
        Me.xtcMain.TabIndex = 9
        Me.xtcMain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpAnalytics, Me.xtpEvents, Me.xtpReports})
        '
        'xtpAnalytics
        '
        Me.xtpAnalytics.Controls.Add(Me.tlpAnalytics)
        Me.xtpAnalytics.Name = "xtpAnalytics"
        Me.xtpAnalytics.Size = New System.Drawing.Size(1246, 823)
        Me.xtpAnalytics.Text = "Analytics"
        '
        'tlpAnalytics
        '
        Me.tlpAnalytics.ColumnCount = 2
        Me.tlpAnalytics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tlpAnalytics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tlpAnalytics.Controls.Add(Me.tlpSubAnalytics, 1, 0)
        Me.tlpAnalytics.Controls.Add(Me.sccAnalytics, 0, 0)
        Me.tlpAnalytics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAnalytics.Location = New System.Drawing.Point(0, 0)
        Me.tlpAnalytics.Name = "tlpAnalytics"
        Me.tlpAnalytics.RowCount = 1
        Me.tlpAnalytics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAnalytics.Size = New System.Drawing.Size(1246, 823)
        Me.tlpAnalytics.TabIndex = 0
        '
        'tlpSubAnalytics
        '
        Me.tlpSubAnalytics.ColumnCount = 1
        Me.tlpSubAnalytics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpSubAnalytics.Controls.Add(Me.gc_TicketRoot, 0, 3)
        Me.tlpSubAnalytics.Controls.Add(Me.gc_TicketScore, 0, 2)
        Me.tlpSubAnalytics.Controls.Add(Me.gc_IssueStatistics, 0, 1)
        Me.tlpSubAnalytics.Controls.Add(Me.btnRefreshDashboard, 0, 0)
        Me.tlpSubAnalytics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpSubAnalytics.Location = New System.Drawing.Point(875, 3)
        Me.tlpSubAnalytics.Name = "tlpSubAnalytics"
        Me.tlpSubAnalytics.RowCount = 3
        Me.tlpSubAnalytics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpSubAnalytics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175.0!))
        Me.tlpSubAnalytics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.53863!))
        Me.tlpSubAnalytics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.46137!))
        Me.tlpSubAnalytics.Size = New System.Drawing.Size(368, 817)
        Me.tlpSubAnalytics.TabIndex = 1
        '
        'gc_TicketRoot
        '
        Me.gc_TicketRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_TicketRoot.Location = New System.Drawing.Point(3, 527)
        Me.gc_TicketRoot.Name = "gc_TicketRoot"
        Me.gc_TicketRoot.Size = New System.Drawing.Size(362, 287)
        Me.gc_TicketRoot.TabIndex = 2
        Me.gc_TicketRoot.Text = "Issues Root Cause"
        '
        'btnRefreshDashboard
        '
        Me.btnRefreshDashboard.Appearance.BackColor = System.Drawing.Color.White
        Me.btnRefreshDashboard.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshDashboard.Appearance.Options.UseBackColor = True
        Me.btnRefreshDashboard.Appearance.Options.UseForeColor = True
        Me.btnRefreshDashboard.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRefreshDashboard.Location = New System.Drawing.Point(290, 2)
        Me.btnRefreshDashboard.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefreshDashboard.Name = "btnRefreshDashboard"
        Me.btnRefreshDashboard.Size = New System.Drawing.Size(76, 22)
        Me.btnRefreshDashboard.TabIndex = 15
        Me.btnRefreshDashboard.Text = "Refresh"
        '
        'xtpEvents
        '
        Me.xtpEvents.Controls.Add(Me.gcEvents)
        Me.xtpEvents.Name = "xtpEvents"
        Me.xtpEvents.Size = New System.Drawing.Size(1246, 823)
        Me.xtpEvents.Text = "Events"
        '
        'gcEvents
        '
        Me.gcEvents.ContextMenuStrip = Me.cm_IOS_Tickets
        Me.gcEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcEvents.Location = New System.Drawing.Point(0, 0)
        Me.gcEvents.MainView = Me.gvEvents
        Me.gcEvents.Name = "gcEvents"
        Me.gcEvents.Size = New System.Drawing.Size(1246, 823)
        Me.gcEvents.TabIndex = 9
        Me.gcEvents.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvEvents})
        '
        'gvEvents
        '
        Me.gvEvents.GridControl = Me.gcEvents
        Me.gvEvents.Name = "gvEvents"
        Me.gvEvents.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvEvents.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvEvents.OptionsBehavior.Editable = False
        Me.gvEvents.OptionsBehavior.ReadOnly = True
        Me.gvEvents.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvEvents.OptionsView.ColumnAutoWidth = False
        Me.gvEvents.OptionsView.ShowGroupPanel = False
        '
        'xtpReports
        '
        Me.xtpReports.Controls.Add(Me.tlpDashboardMain)
        Me.xtpReports.Name = "xtpReports"
        Me.xtpReports.Size = New System.Drawing.Size(1246, 823)
        Me.xtpReports.Text = "Reports"
        '
        'tlpDashboardMain
        '
        Me.tlpDashboardMain.ColumnCount = 1
        Me.tlpDashboardMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDashboardMain.Controls.Add(Me.ReportViewer, 0, 1)
        Me.tlpDashboardMain.Controls.Add(Me.tlpDashTop, 0, 0)
        Me.tlpDashboardMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpDashboardMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpDashboardMain.Name = "tlpDashboardMain"
        Me.tlpDashboardMain.RowCount = 2
        Me.tlpDashboardMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.tlpDashboardMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDashboardMain.Size = New System.Drawing.Size(1246, 823)
        Me.tlpDashboardMain.TabIndex = 2
        '
        'ReportViewer
        '
        Me.ReportViewer.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ReportViewer.Appearance.Options.UseBackColor = True
        Me.ReportViewer.AsyncMode = True
        Me.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer.Location = New System.Drawing.Point(3, 93)
        Me.ReportViewer.Name = "ReportViewer"
        Me.ReportViewer.Size = New System.Drawing.Size(1240, 727)
        Me.ReportViewer.TabIndex = 1
        '
        'tlpDashTop
        '
        Me.tlpDashTop.ColumnCount = 12
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47.0!))
        Me.tlpDashTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpDashTop.Controls.Add(Me.btnDesignDashboard, 4, 0)
        Me.tlpDashTop.Controls.Add(Me.btnCreateDashboard, 3, 0)
        Me.tlpDashTop.Controls.Add(Me.cmbDashboards, 2, 0)
        Me.tlpDashTop.Controls.Add(Me.LabelControl1, 1, 0)
        Me.tlpDashTop.Controls.Add(Me.btnDeleteReport, 5, 0)
        Me.tlpDashTop.Controls.Add(Me.lblMessage, 7, 0)
        Me.tlpDashTop.Controls.Add(Me.btnSendToWebRptSrvr, 6, 0)
        Me.tlpDashTop.Controls.Add(Me.grpRefreshDashboard, 8, 0)
        Me.tlpDashTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpDashTop.Location = New System.Drawing.Point(0, 0)
        Me.tlpDashTop.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpDashTop.Name = "tlpDashTop"
        Me.tlpDashTop.RowCount = 2
        Me.tlpDashTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpDashTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDashTop.Size = New System.Drawing.Size(1246, 90)
        Me.tlpDashTop.TabIndex = 2
        '
        'btnDesignDashboard
        '
        Me.btnDesignDashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDesignDashboard.Location = New System.Drawing.Point(532, 2)
        Me.btnDesignDashboard.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDesignDashboard.Name = "btnDesignDashboard"
        Me.btnDesignDashboard.Size = New System.Drawing.Size(66, 26)
        Me.btnDesignDashboard.TabIndex = 16
        Me.btnDesignDashboard.Text = "Design"
        '
        'btnCreateDashboard
        '
        Me.btnCreateDashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCreateDashboard.Location = New System.Drawing.Point(462, 2)
        Me.btnCreateDashboard.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCreateDashboard.Name = "btnCreateDashboard"
        Me.btnCreateDashboard.Size = New System.Drawing.Size(66, 26)
        Me.btnCreateDashboard.TabIndex = 15
        Me.btnCreateDashboard.Text = "Create"
        '
        'cmbDashboards
        '
        Me.cmbDashboards.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbDashboards.Location = New System.Drawing.Point(243, 5)
        Me.cmbDashboards.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbDashboards.Name = "cmbDashboards"
        Me.cmbDashboards.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDashboards.Size = New System.Drawing.Size(214, 20)
        Me.cmbDashboards.TabIndex = 12
        Me.cmbDashboards.Tag = "Stats"
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(163, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(74, 24)
        Me.LabelControl1.TabIndex = 11
        Me.LabelControl1.Text = "Select Report"
        '
        'btnDeleteReport
        '
        Me.btnDeleteReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteReport.Location = New System.Drawing.Point(602, 2)
        Me.btnDeleteReport.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteReport.Name = "btnDeleteReport"
        Me.btnDeleteReport.Size = New System.Drawing.Size(66, 26)
        Me.btnDeleteReport.TabIndex = 17
        Me.btnDeleteReport.Text = "Delete"
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(763, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(154, 24)
        Me.lblMessage.TabIndex = 20
        '
        'btnSendToWebRptSrvr
        '
        Me.btnSendToWebRptSrvr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSendToWebRptSrvr.Location = New System.Drawing.Point(672, 2)
        Me.btnSendToWebRptSrvr.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSendToWebRptSrvr.Name = "btnSendToWebRptSrvr"
        Me.btnSendToWebRptSrvr.Size = New System.Drawing.Size(86, 26)
        Me.btnSendToWebRptSrvr.TabIndex = 21
        Me.btnSendToWebRptSrvr.Text = "Send To Web"
        '
        'grpRefreshDashboard
        '
        Me.tlpDashTop.SetColumnSpan(Me.grpRefreshDashboard, 4)
        Me.grpRefreshDashboard.Controls.Add(Me.tlpRefresh)
        Me.grpRefreshDashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpRefreshDashboard.Location = New System.Drawing.Point(923, 3)
        Me.grpRefreshDashboard.Name = "grpRefreshDashboard"
        Me.tlpDashTop.SetRowSpan(Me.grpRefreshDashboard, 2)
        Me.grpRefreshDashboard.Size = New System.Drawing.Size(320, 84)
        Me.grpRefreshDashboard.TabIndex = 28
        Me.grpRefreshDashboard.Text = "Refresh Dashboard"
        '
        'tlpRefresh
        '
        Me.tlpRefresh.ColumnCount = 4
        Me.tlpRefresh.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRefresh.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.tlpRefresh.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.tlpRefresh.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.tlpRefresh.Controls.Add(Me.lblTimer, 3, 1)
        Me.tlpRefresh.Controls.Add(Me.SpinEditMonMode, 2, 1)
        Me.tlpRefresh.Controls.Add(Me.LabelControl42, 1, 1)
        Me.tlpRefresh.Controls.Add(Me.rbAutomatic, 0, 1)
        Me.tlpRefresh.Controls.Add(Me.rbManual, 0, 0)
        Me.tlpRefresh.Controls.Add(Me.btnRefresh, 1, 0)
        Me.tlpRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpRefresh.Location = New System.Drawing.Point(2, 23)
        Me.tlpRefresh.Name = "tlpRefresh"
        Me.tlpRefresh.RowCount = 2
        Me.tlpRefresh.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpRefresh.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpRefresh.Size = New System.Drawing.Size(316, 59)
        Me.tlpRefresh.TabIndex = 0
        '
        'lblTimer
        '
        Me.lblTimer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTimer.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimer.ForeColor = System.Drawing.Color.Firebrick
        Me.lblTimer.Location = New System.Drawing.Point(234, 32)
        Me.lblTimer.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTimer.Name = "lblTimer"
        Me.lblTimer.Size = New System.Drawing.Size(79, 24)
        Me.lblTimer.TabIndex = 26
        Me.lblTimer.Text = "Not Set"
        Me.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SpinEditMonMode
        '
        Me.SpinEditMonMode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpinEditMonMode.EditValue = New Decimal(New Integer() {15, 0, 0, 0})
        Me.SpinEditMonMode.Location = New System.Drawing.Point(189, 33)
        Me.SpinEditMonMode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.SpinEditMonMode.Name = "SpinEditMonMode"
        Me.SpinEditMonMode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SpinEditMonMode.Properties.IsFloatValue = False
        Me.SpinEditMonMode.Properties.MaskSettings.Set("mask", "N00")
        Me.SpinEditMonMode.Properties.MaxValue = New Decimal(New Integer() {30, 0, 0, 0})
        Me.SpinEditMonMode.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.SpinEditMonMode.Size = New System.Drawing.Size(39, 20)
        Me.SpinEditMonMode.TabIndex = 25
        '
        'LabelControl42
        '
        Me.LabelControl42.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl42.Location = New System.Drawing.Point(84, 32)
        Me.LabelControl42.Name = "LabelControl42"
        Me.LabelControl42.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl42.Size = New System.Drawing.Size(99, 24)
        Me.LabelControl42.TabIndex = 24
        Me.LabelControl42.Text = "Refresh Rate [Mins]"
        '
        'rbAutomatic
        '
        Me.rbAutomatic.AutoSize = True
        Me.rbAutomatic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbAutomatic.Location = New System.Drawing.Point(3, 33)
        Me.rbAutomatic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.rbAutomatic.Name = "rbAutomatic"
        Me.rbAutomatic.Size = New System.Drawing.Size(75, 23)
        Me.rbAutomatic.TabIndex = 27
        Me.rbAutomatic.Text = "Automatic"
        Me.rbAutomatic.UseVisualStyleBackColor = True
        '
        'rbManual
        '
        Me.rbManual.AutoSize = True
        Me.rbManual.Checked = True
        Me.rbManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbManual.Location = New System.Drawing.Point(3, 4)
        Me.rbManual.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.rbManual.Name = "rbManual"
        Me.rbManual.Size = New System.Drawing.Size(75, 22)
        Me.rbManual.TabIndex = 28
        Me.rbManual.TabStop = True
        Me.rbManual.Text = "Manual"
        Me.rbManual.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefresh.Location = New System.Drawing.Point(83, 2)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(101, 25)
        Me.btnRefresh.TabIndex = 27
        Me.btnRefresh.Text = "Refresh"
        '
        'timerCountdown
        '
        Me.timerCountdown.Interval = 1000
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'bgWorker
        '
        Me.bgWorker.WorkerSupportsCancellation = True
        '
        'frmDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1248, 848)
        Me.Controls.Add(Me.xtcMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmDashboard.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1070, 719)
        Me.Name = "frmDashboard"
        Me.ShowMdiChildCaptionInParentTitle = True
        Me.Text = "Dashboard"
        CType(Me.ce_TicketOpen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_IssueStatistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gc_IssueStatistics.ResumeLayout(False)
        Me.gc_IssueStatistics.PerformLayout()
        CType(Me.gc_TicketScore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccAnalytics.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAnalytics.Panel1.ResumeLayout(False)
        CType(Me.sccAnalytics.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAnalytics.Panel2.ResumeLayout(False)
        CType(Me.sccAnalytics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAnalytics.ResumeLayout(False)
        CType(Me.dgvIOS_Tickets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_IOS_Tickets.ResumeLayout(False)
        CType(Me.gvIOS_Tickets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xTabControl_Tickets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xTabControl_Tickets.ResumeLayout(False)
        Me.xTabPage_TicketDetails.ResumeLayout(False)
        CType(Me.tlv_TicketDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xTabPage_TicketHistory.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.tlv_TicketHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.txt_TicketComment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dcmb_TicketStatusUpdate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcMain.ResumeLayout(False)
        Me.xtpAnalytics.ResumeLayout(False)
        Me.tlpAnalytics.ResumeLayout(False)
        Me.tlpSubAnalytics.ResumeLayout(False)
        CType(Me.gc_TicketRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpEvents.ResumeLayout(False)
        CType(Me.gcEvents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpReports.ResumeLayout(False)
        Me.tlpDashboardMain.ResumeLayout(False)
        CType(Me.ReportViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpDashTop.ResumeLayout(False)
        Me.tlpDashTop.PerformLayout()
        CType(Me.cmbDashboards.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpRefreshDashboard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRefreshDashboard.ResumeLayout(False)
        Me.tlpRefresh.ResumeLayout(False)
        Me.tlpRefresh.PerformLayout()
        CType(Me.SpinEditMonMode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_TicketScore As DevExpress.XtraEditors.GroupControl
    Friend WithEvents sccAnalytics As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents xTabControl_Tickets As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xTabPage_TicketHistory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xTabPage_TicketDetails As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xTabPage_CellHistory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_TicketUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txt_TicketComment As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dcmb_TicketStatusUpdate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ce_TicketOpen As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cm_IOS_Tickets As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_TicketMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_TicketMapAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_TicketObjectTree As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_TicketLaunch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_ticketremedy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvIOS_Tickets As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvIOS_Tickets As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gc_IssueStatistics As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lbl_LongOpen As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_TimeOpen As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_TicketsClosed As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_TicketsAssigned As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_TicketsOpen As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gc_TicketRoot As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Panel1 As Panel
    Friend WithEvents tlpSubAnalytics As TableLayoutPanel
    Friend WithEvents tlv_TicketDetails As DevExpress.XtraTreeList.TreeList
    Friend WithEvents tlv_TicketHistory As DevExpress.XtraTreeList.TreeList
    Friend WithEvents xtcMain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpAnalytics As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpEvents As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcEvents As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvEvents As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents timerCountdown As Timer
    Friend WithEvents xtpReports As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tlpAnalytics As TableLayoutPanel
    Friend WithEvents ReportViewer As DevExpress.DashboardWin.DashboardViewer
    Friend WithEvents tlpDashboardMain As TableLayoutPanel
    Friend WithEvents tlpDashTop As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbDashboards As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnCreateDashboard As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefreshDashboard As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDesignDashboard As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsmi_ManualUpdateStatus As ToolStripMenuItem
    Friend WithEvents btnDeleteReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents bgWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnSendToWebRptSrvr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl42 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SpinEditMonMode As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents lblTimer As Label
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpRefreshDashboard As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpRefresh As TableLayoutPanel
    Friend WithEvents rbAutomatic As RadioButton
    Friend WithEvents rbManual As RadioButton
End Class
