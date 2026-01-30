<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNBIReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNBIReports))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.sccReports = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpReportsList = New DevExpress.XtraEditors.GroupControl()
        Me.tlpReportsList = New System.Windows.Forms.TableLayoutPanel()
        Me.gcReportsList = New DevExpress.XtraGrid.GridControl()
        Me.gvReportsList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDeleteReport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnModifyReport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddReport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCopyReport = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpReports = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpReportConfig = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblReportName = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTechnology = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.lblObjectType = New DevExpress.XtraEditors.LabelControl()
        Me.grpReportProperties = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl30 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblReportOwner = New DevExpress.XtraEditors.LabelControl()
        Me.lblReportLockedMsg = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsScheduled = New DevExpress.XtraEditors.CheckEdit()
        Me.ceIsLocked = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl27 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.lblLastRunTime = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsEnabled = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl31 = New DevExpress.XtraEditors.LabelControl()
        Me.lblReportDescription = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl32 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl33 = New DevExpress.XtraEditors.LabelControl()
        Me.txtTimeout = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTestStatus = New DevExpress.XtraEditors.LabelControl()
        Me.btnSaveReport = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpConfig1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpOutput = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbOutputFormat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ceIsOutputPivotted = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.txtOutputFolder = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl29 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceEmailEnabled = New DevExpress.XtraEditors.CheckEdit()
        Me.txtEmailAddresses = New DevExpress.XtraEditors.TextEdit()
        Me.grpScheduleReport = New DevExpress.XtraEditors.GroupControl()
        Me.tlpScheduleReport = New System.Windows.Forms.TableLayoutPanel()
        Me.deScheduleStartTime = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbScheduleInterval = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.grpPeriod = New DevExpress.XtraEditors.GroupControl()
        Me.tlpPeriodSelection = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbPredefTimeStats = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.dePeriodStartTime = New DevExpress.XtraEditors.DateEdit()
        Me.dePeriodEndTime = New DevExpress.XtraEditors.DateEdit()
        Me.xtcSQL = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpAutoSQL = New DevExpress.XtraTab.XtraTabPage()
        Me.tlpAutoSQL = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpConfig2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl28 = New DevExpress.XtraEditors.LabelControl()
        Me.grpAggr = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbObjectAggr = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl24 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTimeAggr = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl25 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpKPIandFilter = New System.Windows.Forms.TableLayoutPanel()
        Me.grpKPIs = New DevExpress.XtraEditors.GroupControl()
        Me.tlpKPI = New System.Windows.Forms.TableLayoutPanel()
        Me.grdKPI = New DevExpress.XtraGrid.GridControl()
        Me.gvKPI = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpKPIBtns = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDeleteKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpAliases = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdAliases = New DevExpress.XtraGrid.GridControl()
        Me.gvAliases = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddAlises = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteAliases = New DevExpress.XtraEditors.SimpleButton()
        Me.grpObjectFilter = New DevExpress.XtraEditors.GroupControl()
        Me.tlpObjFilter = New System.Windows.Forms.TableLayoutPanel()
        Me.grdObjFilter = New DevExpress.XtraGrid.GridControl()
        Me.gvObjFilter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpObjFilterBtns = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddObjFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteObjFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.xtpManualSQL = New DevExpress.XtraTab.XtraTabPage()
        Me.sccManualSQL = New DevExpress.XtraEditors.SplitContainerControl()
        Me.tlpManualSQLText = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTest = New DevExpress.XtraEditors.SimpleButton()
        Me.btnStartTime = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEndTime = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl26 = New DevExpress.XtraEditors.LabelControl()
        Me.txtManualSQL = New DevExpress.XtraEditors.MemoEdit()
        Me.tlpManualSQLGrids = New System.Windows.Forms.TableLayoutPanel()
        Me.gcKPIs = New DevExpress.XtraGrid.GridControl()
        Me.gvKPIs = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcTables = New DevExpress.XtraGrid.GridControl()
        Me.gvTables = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcColumns = New DevExpress.XtraGrid.GridControl()
        Me.gvColumns = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtcViewReport = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpViewReport = New DevExpress.XtraTab.XtraTabPage()
        Me.grpViewReport = New DevExpress.XtraEditors.GroupControl()
        Me.tlpReportView = New System.Windows.Forms.TableLayoutPanel()
        Me.gcViewReport = New DevExpress.XtraGrid.GridControl()
        Me.gvViewReport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpViewReport = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnLoadReport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExport2CSV = New DevExpress.XtraEditors.SimpleButton()
        Me.xtpReportStatus = New DevExpress.XtraTab.XtraTabPage()
        Me.gcReportStatus = New DevExpress.XtraGrid.GridControl()
        Me.gvReportStatus = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.sccReports, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccReports.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccReports.Panel1.SuspendLayout()
        CType(Me.sccReports.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccReports.Panel2.SuspendLayout()
        Me.sccReports.SuspendLayout()
        CType(Me.grpReportsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReportsList.SuspendLayout()
        Me.tlpReportsList.SuspendLayout()
        CType(Me.gcReportsList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReportsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel17.SuspendLayout()
        Me.tlpReports.SuspendLayout()
        Me.tlpReportConfig.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.grpReportProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReportProperties.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.ceIsScheduled.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsLocked.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsEnabled.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTimeout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel16.SuspendLayout()
        Me.tlpConfig1.SuspendLayout()
        CType(Me.grpOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOutput.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.cmbOutputFormat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsOutputPivotted.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOutputFolder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ceEmailEnabled.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailAddresses.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpScheduleReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScheduleReport.SuspendLayout()
        Me.tlpScheduleReport.SuspendLayout()
        CType(Me.deScheduleStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deScheduleStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbScheduleInterval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPeriod.SuspendLayout()
        Me.tlpPeriodSelection.SuspendLayout()
        CType(Me.cmbPredefTimeStats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePeriodStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePeriodStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePeriodEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePeriodEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcSQL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcSQL.SuspendLayout()
        Me.xtpAutoSQL.SuspendLayout()
        Me.tlpAutoSQL.SuspendLayout()
        Me.tlpConfig2.SuspendLayout()
        CType(Me.grpAggr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAggr.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.cmbObjectAggr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTimeAggr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpKPIandFilter.SuspendLayout()
        CType(Me.grpKPIs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpKPIs.SuspendLayout()
        Me.tlpKPI.SuspendLayout()
        CType(Me.grdKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpKPIBtns.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.grpAliases, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAliases.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.grdAliases, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAliases, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.grpObjectFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpObjectFilter.SuspendLayout()
        Me.tlpObjFilter.SuspendLayout()
        CType(Me.grdObjFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvObjFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpObjFilterBtns.SuspendLayout()
        Me.xtpManualSQL.SuspendLayout()
        CType(Me.sccManualSQL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccManualSQL.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccManualSQL.Panel1.SuspendLayout()
        CType(Me.sccManualSQL.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccManualSQL.Panel2.SuspendLayout()
        Me.sccManualSQL.SuspendLayout()
        Me.tlpManualSQLText.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.txtManualSQL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpManualSQLGrids.SuspendLayout()
        CType(Me.gcKPIs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvKPIs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcViewReport.SuspendLayout()
        Me.xtpViewReport.SuspendLayout()
        CType(Me.grpViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpViewReport.SuspendLayout()
        Me.tlpReportView.SuspendLayout()
        CType(Me.gcViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpViewReport.SuspendLayout()
        Me.xtpReportStatus.SuspendLayout()
        CType(Me.gcReportStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReportStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.AutoScroll = True
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.sccReports, 0, 0)
        Me.tlpMain.Controls.Add(Me.xtcViewReport, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 670.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(1248, 868)
        Me.tlpMain.TabIndex = 0
        '
        'sccReports
        '
        Me.sccReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccReports.Location = New System.Drawing.Point(3, 3)
        Me.sccReports.Name = "sccReports"
        '
        'sccReports.Panel1
        '
        Me.sccReports.Panel1.Controls.Add(Me.grpReportsList)
        Me.sccReports.Panel1.MinSize = 600
        Me.sccReports.Panel1.Text = "Panel1"
        '
        'sccReports.Panel2
        '
        Me.sccReports.Panel2.Controls.Add(Me.tlpReports)
        Me.sccReports.Panel2.MinSize = 600
        Me.sccReports.Panel2.Text = "Panel2"
        Me.sccReports.Size = New System.Drawing.Size(1242, 664)
        Me.sccReports.SplitterPosition = 632
        Me.sccReports.TabIndex = 2
        '
        'grpReportsList
        '
        Me.grpReportsList.Controls.Add(Me.tlpReportsList)
        Me.grpReportsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpReportsList.Location = New System.Drawing.Point(0, 0)
        Me.grpReportsList.Name = "grpReportsList"
        Me.grpReportsList.Size = New System.Drawing.Size(632, 664)
        Me.grpReportsList.TabIndex = 2
        Me.grpReportsList.Text = "Reports List"
        '
        'tlpReportsList
        '
        Me.tlpReportsList.ColumnCount = 1
        Me.tlpReportsList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReportsList.Controls.Add(Me.gcReportsList, 0, 1)
        Me.tlpReportsList.Controls.Add(Me.TableLayoutPanel17, 0, 0)
        Me.tlpReportsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpReportsList.Location = New System.Drawing.Point(2, 23)
        Me.tlpReportsList.Name = "tlpReportsList"
        Me.tlpReportsList.RowCount = 2
        Me.tlpReportsList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpReportsList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReportsList.Size = New System.Drawing.Size(628, 639)
        Me.tlpReportsList.TabIndex = 2
        '
        'gcReportsList
        '
        Me.gcReportsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcReportsList.Location = New System.Drawing.Point(3, 38)
        Me.gcReportsList.MainView = Me.gvReportsList
        Me.gcReportsList.Name = "gcReportsList"
        Me.gcReportsList.Size = New System.Drawing.Size(622, 598)
        Me.gcReportsList.TabIndex = 16
        Me.gcReportsList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReportsList})
        '
        'gvReportsList
        '
        Me.gvReportsList.GridControl = Me.gcReportsList
        Me.gvReportsList.Name = "gvReportsList"
        Me.gvReportsList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportsList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportsList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportsList.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportsList.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportsList.OptionsView.ColumnAutoWidth = False
        Me.gvReportsList.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.ColumnCount = 5
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel17.Controls.Add(Me.btnDeleteReport, 3, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.btnModifyReport, 2, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.btnAddReport, 1, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.btnCopyReport, 4, 0)
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel17.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 1
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(628, 35)
        Me.TableLayoutPanel17.TabIndex = 0
        '
        'btnDeleteReport
        '
        Me.btnDeleteReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteReport.Location = New System.Drawing.Point(471, 3)
        Me.btnDeleteReport.Name = "btnDeleteReport"
        Me.btnDeleteReport.Size = New System.Drawing.Size(74, 29)
        Me.btnDeleteReport.TabIndex = 3
        Me.btnDeleteReport.Text = "Delete"
        '
        'btnModifyReport
        '
        Me.btnModifyReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnModifyReport.Location = New System.Drawing.Point(391, 3)
        Me.btnModifyReport.Name = "btnModifyReport"
        Me.btnModifyReport.Size = New System.Drawing.Size(74, 29)
        Me.btnModifyReport.TabIndex = 2
        Me.btnModifyReport.Text = "Modify"
        '
        'btnAddReport
        '
        Me.btnAddReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddReport.Location = New System.Drawing.Point(311, 3)
        Me.btnAddReport.Name = "btnAddReport"
        Me.btnAddReport.Size = New System.Drawing.Size(74, 29)
        Me.btnAddReport.TabIndex = 1
        Me.btnAddReport.Text = "Add"
        '
        'btnCopyReport
        '
        Me.btnCopyReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCopyReport.Location = New System.Drawing.Point(551, 3)
        Me.btnCopyReport.Name = "btnCopyReport"
        Me.btnCopyReport.Size = New System.Drawing.Size(74, 29)
        Me.btnCopyReport.TabIndex = 4
        Me.btnCopyReport.Text = "Copy"
        '
        'tlpReports
        '
        Me.tlpReports.ColumnCount = 1
        Me.tlpReports.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReports.Controls.Add(Me.tlpReportConfig, 0, 0)
        Me.tlpReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpReports.Location = New System.Drawing.Point(0, 0)
        Me.tlpReports.Name = "tlpReports"
        Me.tlpReports.RowCount = 1
        Me.tlpReports.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReports.Size = New System.Drawing.Size(600, 664)
        Me.tlpReports.TabIndex = 0
        '
        'tlpReportConfig
        '
        Me.tlpReportConfig.ColumnCount = 1
        Me.tlpReportConfig.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReportConfig.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.tlpReportConfig.Controls.Add(Me.grpReportProperties, 0, 1)
        Me.tlpReportConfig.Controls.Add(Me.TableLayoutPanel16, 0, 5)
        Me.tlpReportConfig.Controls.Add(Me.tlpConfig1, 0, 2)
        Me.tlpReportConfig.Controls.Add(Me.xtcSQL, 0, 3)
        Me.tlpReportConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpReportConfig.Location = New System.Drawing.Point(3, 3)
        Me.tlpReportConfig.Name = "tlpReportConfig"
        Me.tlpReportConfig.RowCount = 6
        Me.tlpReportConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpReportConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111.0!))
        Me.tlpReportConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.tlpReportConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49.0!))
        Me.tlpReportConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReportConfig.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.tlpReportConfig.Size = New System.Drawing.Size(594, 658)
        Me.tlpReportConfig.TabIndex = 1
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 9
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl14, 6, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl12, 4, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl5, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl6, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lblReportName, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl11, 3, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lblTechnology, 5, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl13, 7, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lblObjectType, 8, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(594, 28)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl14.Appearance.Options.UseForeColor = True
        Me.LabelControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl14.Location = New System.Drawing.Point(401, 3)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl14.Size = New System.Drawing.Size(74, 22)
        Me.LabelControl14.TabIndex = 12
        Me.LabelControl14.Text = "Object Type"
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl12.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Appearance.Options.UseForeColor = True
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(286, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(4, 22)
        Me.LabelControl12.TabIndex = 9
        Me.LabelControl12.Text = ":"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl5.Appearance.Options.UseForeColor = True
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(79, 22)
        Me.LabelControl5.TabIndex = 3
        Me.LabelControl5.Text = "Report Name"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(88, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(4, 22)
        Me.LabelControl6.TabIndex = 7
        Me.LabelControl6.Text = ":"
        '
        'lblReportName
        '
        Me.lblReportName.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblReportName.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblReportName.Appearance.Options.UseFont = True
        Me.lblReportName.Appearance.Options.UseForeColor = True
        Me.lblReportName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReportName.Location = New System.Drawing.Point(98, 3)
        Me.lblReportName.Name = "lblReportName"
        Me.lblReportName.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblReportName.Size = New System.Drawing.Size(102, 22)
        Me.lblReportName.TabIndex = 4
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl11.Appearance.Options.UseForeColor = True
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(206, 3)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(74, 22)
        Me.LabelControl11.TabIndex = 8
        Me.LabelControl11.Text = "Technology"
        '
        'lblTechnology
        '
        Me.lblTechnology.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTechnology.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblTechnology.Appearance.Options.UseFont = True
        Me.lblTechnology.Appearance.Options.UseForeColor = True
        Me.lblTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTechnology.Location = New System.Drawing.Point(296, 3)
        Me.lblTechnology.Name = "lblTechnology"
        Me.lblTechnology.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblTechnology.Size = New System.Drawing.Size(99, 22)
        Me.lblTechnology.TabIndex = 11
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl13.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Appearance.Options.UseForeColor = True
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(481, 3)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(4, 22)
        Me.LabelControl13.TabIndex = 10
        Me.LabelControl13.Text = ":"
        '
        'lblObjectType
        '
        Me.lblObjectType.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblObjectType.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblObjectType.Appearance.Options.UseFont = True
        Me.lblObjectType.Appearance.Options.UseForeColor = True
        Me.lblObjectType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblObjectType.Location = New System.Drawing.Point(491, 3)
        Me.lblObjectType.Name = "lblObjectType"
        Me.lblObjectType.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblObjectType.Size = New System.Drawing.Size(100, 22)
        Me.lblObjectType.TabIndex = 13
        '
        'grpReportProperties
        '
        Me.grpReportProperties.Controls.Add(Me.TableLayoutPanel6)
        Me.grpReportProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpReportProperties.Location = New System.Drawing.Point(3, 31)
        Me.grpReportProperties.Name = "grpReportProperties"
        Me.grpReportProperties.Size = New System.Drawing.Size(588, 105)
        Me.grpReportProperties.TabIndex = 2
        Me.grpReportProperties.Text = "Report Proprties"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 9
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl30, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl1, 3, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl3, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl4, 3, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.lblReportOwner, 5, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.lblReportLockedMsg, 6, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.ceIsScheduled, 2, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.ceIsLocked, 5, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl8, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl9, 4, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl7, 4, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl10, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl27, 7, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl23, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.lblLastRunTime, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.ceIsEnabled, 8, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl2, 6, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl31, 1, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.lblReportDescription, 2, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl32, 6, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl33, 7, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.txtTimeout, 8, 2)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 3
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(584, 80)
        Me.TableLayoutPanel6.TabIndex = 1
        '
        'LabelControl30
        '
        Me.LabelControl30.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl30.Appearance.Options.UseForeColor = True
        Me.LabelControl30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl30.Location = New System.Drawing.Point(3, 56)
        Me.LabelControl30.Name = "LabelControl30"
        Me.LabelControl30.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl30.Size = New System.Drawing.Size(74, 21)
        Me.LabelControl30.TabIndex = 17
        Me.LabelControl30.Text = "Description"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(199, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Owner"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(74, 21)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Is Scheduled"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(199, 29)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(74, 21)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "Is Locked"
        '
        'lblReportOwner
        '
        Me.lblReportOwner.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblReportOwner.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblReportOwner.Appearance.Options.UseFont = True
        Me.lblReportOwner.Appearance.Options.UseForeColor = True
        Me.lblReportOwner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReportOwner.Location = New System.Drawing.Point(289, 3)
        Me.lblReportOwner.Name = "lblReportOwner"
        Me.lblReportOwner.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblReportOwner.Size = New System.Drawing.Size(97, 20)
        Me.lblReportOwner.TabIndex = 7
        '
        'lblReportLockedMsg
        '
        Me.lblReportLockedMsg.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblReportLockedMsg.Appearance.Options.UseForeColor = True
        Me.TableLayoutPanel6.SetColumnSpan(Me.lblReportLockedMsg, 3)
        Me.lblReportLockedMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReportLockedMsg.Location = New System.Drawing.Point(392, 3)
        Me.lblReportLockedMsg.Name = "lblReportLockedMsg"
        Me.lblReportLockedMsg.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblReportLockedMsg.Size = New System.Drawing.Size(189, 20)
        Me.lblReportLockedMsg.TabIndex = 9
        '
        'ceIsScheduled
        '
        Me.ceIsScheduled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsScheduled.Location = New System.Drawing.Point(93, 29)
        Me.ceIsScheduled.Name = "ceIsScheduled"
        Me.ceIsScheduled.Properties.Caption = ""
        Me.ceIsScheduled.Size = New System.Drawing.Size(100, 21)
        Me.ceIsScheduled.TabIndex = 1
        '
        'ceIsLocked
        '
        Me.ceIsLocked.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsLocked.Location = New System.Drawing.Point(289, 29)
        Me.ceIsLocked.Name = "ceIsLocked"
        Me.ceIsLocked.Properties.Caption = ""
        Me.ceIsLocked.Size = New System.Drawing.Size(97, 21)
        Me.ceIsLocked.TabIndex = 2
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Appearance.Options.UseForeColor = True
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(83, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(4, 20)
        Me.LabelControl8.TabIndex = 11
        Me.LabelControl8.Text = ":"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl9.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Appearance.Options.UseForeColor = True
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(279, 3)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(4, 20)
        Me.LabelControl9.TabIndex = 12
        Me.LabelControl9.Text = ":"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Appearance.Options.UseForeColor = True
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(279, 29)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(4, 21)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = ":"
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl10.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Appearance.Options.UseForeColor = True
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(83, 29)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(4, 21)
        Me.LabelControl10.TabIndex = 13
        Me.LabelControl10.Text = ":"
        '
        'LabelControl27
        '
        Me.LabelControl27.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl27.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl27.Appearance.Options.UseFont = True
        Me.LabelControl27.Appearance.Options.UseForeColor = True
        Me.LabelControl27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl27.Location = New System.Drawing.Point(472, 29)
        Me.LabelControl27.Name = "LabelControl27"
        Me.LabelControl27.Size = New System.Drawing.Size(4, 21)
        Me.LabelControl27.TabIndex = 16
        Me.LabelControl27.Text = ":"
        '
        'LabelControl23
        '
        Me.LabelControl23.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl23.Appearance.Options.UseForeColor = True
        Me.LabelControl23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl23.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl23.Name = "LabelControl23"
        Me.LabelControl23.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl23.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl23.TabIndex = 14
        Me.LabelControl23.Text = "Last Run Time"
        '
        'lblLastRunTime
        '
        Me.lblLastRunTime.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLastRunTime.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblLastRunTime.Appearance.Options.UseFont = True
        Me.lblLastRunTime.Appearance.Options.UseForeColor = True
        Me.lblLastRunTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLastRunTime.Location = New System.Drawing.Point(93, 3)
        Me.lblLastRunTime.Name = "lblLastRunTime"
        Me.lblLastRunTime.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblLastRunTime.Size = New System.Drawing.Size(100, 20)
        Me.lblLastRunTime.TabIndex = 15
        '
        'ceIsEnabled
        '
        Me.ceIsEnabled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsEnabled.Location = New System.Drawing.Point(482, 29)
        Me.ceIsEnabled.Name = "ceIsEnabled"
        Me.ceIsEnabled.Properties.Caption = ""
        Me.ceIsEnabled.Size = New System.Drawing.Size(99, 21)
        Me.ceIsEnabled.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(392, 29)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(74, 21)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Is Enabled"
        '
        'LabelControl31
        '
        Me.LabelControl31.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl31.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl31.Appearance.Options.UseFont = True
        Me.LabelControl31.Appearance.Options.UseForeColor = True
        Me.LabelControl31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl31.Location = New System.Drawing.Point(83, 56)
        Me.LabelControl31.Name = "LabelControl31"
        Me.LabelControl31.Size = New System.Drawing.Size(4, 21)
        Me.LabelControl31.TabIndex = 18
        Me.LabelControl31.Text = ":"
        '
        'lblReportDescription
        '
        Me.lblReportDescription.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblReportDescription.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblReportDescription.Appearance.Options.UseFont = True
        Me.lblReportDescription.Appearance.Options.UseForeColor = True
        Me.TableLayoutPanel6.SetColumnSpan(Me.lblReportDescription, 4)
        Me.lblReportDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReportDescription.Location = New System.Drawing.Point(93, 56)
        Me.lblReportDescription.Name = "lblReportDescription"
        Me.lblReportDescription.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblReportDescription.Size = New System.Drawing.Size(293, 21)
        Me.lblReportDescription.TabIndex = 19
        '
        'LabelControl32
        '
        Me.LabelControl32.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl32.Appearance.Options.UseForeColor = True
        Me.LabelControl32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl32.Location = New System.Drawing.Point(392, 56)
        Me.LabelControl32.Name = "LabelControl32"
        Me.LabelControl32.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl32.Size = New System.Drawing.Size(74, 21)
        Me.LabelControl32.TabIndex = 20
        Me.LabelControl32.Text = "Timeout"
        '
        'LabelControl33
        '
        Me.LabelControl33.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl33.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl33.Appearance.Options.UseFont = True
        Me.LabelControl33.Appearance.Options.UseForeColor = True
        Me.LabelControl33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl33.Location = New System.Drawing.Point(472, 56)
        Me.LabelControl33.Name = "LabelControl33"
        Me.LabelControl33.Size = New System.Drawing.Size(4, 21)
        Me.LabelControl33.TabIndex = 21
        Me.LabelControl33.Text = ":"
        '
        'txtTimeout
        '
        Me.txtTimeout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTimeout.EditValue = ""
        Me.txtTimeout.Location = New System.Drawing.Point(482, 57)
        Me.txtTimeout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txtTimeout.Name = "txtTimeout"
        Me.txtTimeout.Size = New System.Drawing.Size(99, 20)
        Me.txtTimeout.TabIndex = 22
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 2
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.lblTestStatus, 0, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.btnSaveReport, 1, 0)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(3, 616)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 1
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(588, 39)
        Me.TableLayoutPanel16.TabIndex = 6
        '
        'lblTestStatus
        '
        Me.lblTestStatus.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblTestStatus.Appearance.Options.UseForeColor = True
        Me.lblTestStatus.Appearance.Options.UseTextOptions = True
        Me.lblTestStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblTestStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTestStatus.Location = New System.Drawing.Point(3, 3)
        Me.lblTestStatus.Name = "lblTestStatus"
        Me.lblTestStatus.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblTestStatus.Size = New System.Drawing.Size(482, 33)
        Me.lblTestStatus.TabIndex = 19
        '
        'btnSaveReport
        '
        Me.btnSaveReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSaveReport.Location = New System.Drawing.Point(491, 3)
        Me.btnSaveReport.Name = "btnSaveReport"
        Me.btnSaveReport.Size = New System.Drawing.Size(94, 33)
        Me.btnSaveReport.TabIndex = 18
        Me.btnSaveReport.Text = "Save Report"
        '
        'tlpConfig1
        '
        Me.tlpConfig1.ColumnCount = 3
        Me.tlpConfig1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpConfig1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.tlpConfig1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpConfig1.Controls.Add(Me.grpOutput, 2, 0)
        Me.tlpConfig1.Controls.Add(Me.grpScheduleReport, 1, 0)
        Me.tlpConfig1.Controls.Add(Me.grpPeriod, 0, 0)
        Me.tlpConfig1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpConfig1.Location = New System.Drawing.Point(3, 142)
        Me.tlpConfig1.Name = "tlpConfig1"
        Me.tlpConfig1.RowCount = 1
        Me.tlpConfig1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpConfig1.Size = New System.Drawing.Size(588, 144)
        Me.tlpConfig1.TabIndex = 3
        '
        'grpOutput
        '
        Me.grpOutput.Controls.Add(Me.TableLayoutPanel2)
        Me.grpOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpOutput.Location = New System.Drawing.Point(396, 3)
        Me.grpOutput.Name = "grpOutput"
        Me.grpOutput.Size = New System.Drawing.Size(189, 138)
        Me.grpOutput.TabIndex = 1
        Me.grpOutput.Text = "Output"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl21, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl22, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbOutputFormat, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ceIsOutputPivotted, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl20, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.txtOutputFolder, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl29, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 1, 3)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(185, 113)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'LabelControl21
        '
        Me.LabelControl21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl21.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl21.Name = "LabelControl21"
        Me.LabelControl21.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl21.Size = New System.Drawing.Size(84, 20)
        Me.LabelControl21.TabIndex = 0
        Me.LabelControl21.Text = "Output Format"
        '
        'LabelControl22
        '
        Me.LabelControl22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl22.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl22.Size = New System.Drawing.Size(84, 20)
        Me.LabelControl22.TabIndex = 1
        Me.LabelControl22.Text = "Output Pivotted"
        '
        'cmbOutputFormat
        '
        Me.cmbOutputFormat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbOutputFormat.EditValue = "CSV"
        Me.cmbOutputFormat.Location = New System.Drawing.Point(93, 3)
        Me.cmbOutputFormat.Name = "cmbOutputFormat"
        Me.cmbOutputFormat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOutputFormat.Properties.Items.AddRange(New Object() {"CSV", "CLF"})
        Me.cmbOutputFormat.Size = New System.Drawing.Size(89, 20)
        Me.cmbOutputFormat.TabIndex = 12
        '
        'ceIsOutputPivotted
        '
        Me.ceIsOutputPivotted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsOutputPivotted.Location = New System.Drawing.Point(93, 29)
        Me.ceIsOutputPivotted.Name = "ceIsOutputPivotted"
        Me.ceIsOutputPivotted.Properties.Caption = ""
        Me.ceIsOutputPivotted.Size = New System.Drawing.Size(89, 20)
        Me.ceIsOutputPivotted.TabIndex = 13
        '
        'LabelControl20
        '
        Me.LabelControl20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl20.Location = New System.Drawing.Point(3, 55)
        Me.LabelControl20.Name = "LabelControl20"
        Me.LabelControl20.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl20.Size = New System.Drawing.Size(84, 20)
        Me.LabelControl20.TabIndex = 14
        Me.LabelControl20.Text = "Output Folder"
        '
        'txtOutputFolder
        '
        Me.txtOutputFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOutputFolder.Location = New System.Drawing.Point(93, 55)
        Me.txtOutputFolder.Name = "txtOutputFolder"
        Me.txtOutputFolder.Size = New System.Drawing.Size(89, 20)
        Me.txtOutputFolder.TabIndex = 15
        '
        'LabelControl29
        '
        Me.LabelControl29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl29.Location = New System.Drawing.Point(3, 81)
        Me.LabelControl29.Name = "LabelControl29"
        Me.LabelControl29.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl29.Size = New System.Drawing.Size(84, 20)
        Me.LabelControl29.TabIndex = 16
        Me.LabelControl29.Text = "Email"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ceEmailEnabled, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtEmailAddresses, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(90, 78)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(95, 26)
        Me.TableLayoutPanel1.TabIndex = 17
        '
        'ceEmailEnabled
        '
        Me.ceEmailEnabled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceEmailEnabled.Location = New System.Drawing.Point(3, 3)
        Me.ceEmailEnabled.Name = "ceEmailEnabled"
        Me.ceEmailEnabled.Properties.Caption = ""
        Me.ceEmailEnabled.Size = New System.Drawing.Size(17, 20)
        Me.ceEmailEnabled.TabIndex = 0
        '
        'txtEmailAddresses
        '
        Me.txtEmailAddresses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEmailAddresses.Location = New System.Drawing.Point(26, 3)
        Me.txtEmailAddresses.Name = "txtEmailAddresses"
        Me.txtEmailAddresses.Size = New System.Drawing.Size(66, 20)
        Me.txtEmailAddresses.TabIndex = 1
        '
        'grpScheduleReport
        '
        Me.grpScheduleReport.Controls.Add(Me.tlpScheduleReport)
        Me.grpScheduleReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpScheduleReport.Location = New System.Drawing.Point(197, 3)
        Me.grpScheduleReport.Name = "grpScheduleReport"
        Me.grpScheduleReport.Size = New System.Drawing.Size(193, 138)
        Me.grpScheduleReport.TabIndex = 1
        Me.grpScheduleReport.Text = "Schedule Report"
        '
        'tlpScheduleReport
        '
        Me.tlpScheduleReport.ColumnCount = 2
        Me.tlpScheduleReport.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.tlpScheduleReport.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpScheduleReport.Controls.Add(Me.deScheduleStartTime, 1, 0)
        Me.tlpScheduleReport.Controls.Add(Me.LabelControl18, 0, 0)
        Me.tlpScheduleReport.Controls.Add(Me.LabelControl19, 0, 1)
        Me.tlpScheduleReport.Controls.Add(Me.cmbScheduleInterval, 1, 1)
        Me.tlpScheduleReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpScheduleReport.Location = New System.Drawing.Point(2, 23)
        Me.tlpScheduleReport.Name = "tlpScheduleReport"
        Me.tlpScheduleReport.RowCount = 4
        Me.tlpScheduleReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpScheduleReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpScheduleReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpScheduleReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpScheduleReport.Size = New System.Drawing.Size(189, 113)
        Me.tlpScheduleReport.TabIndex = 1
        '
        'deScheduleStartTime
        '
        Me.deScheduleStartTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deScheduleStartTime.EditValue = New Date(2016, 8, 11, 11, 0, 0, 0)
        Me.deScheduleStartTime.Location = New System.Drawing.Point(113, 3)
        Me.deScheduleStartTime.Name = "deScheduleStartTime"
        Me.deScheduleStartTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deScheduleStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.deScheduleStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deScheduleStartTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.deScheduleStartTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.deScheduleStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deScheduleStartTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.deScheduleStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deScheduleStartTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deScheduleStartTime.Properties.MaskSettings.Set("mask", "dd/MM/yyyy HH:mm")
        Me.deScheduleStartTime.Properties.MaskSettings.Set("placeholder", Global.Microsoft.VisualBasic.ChrW(47))
        Me.deScheduleStartTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.deScheduleStartTime.Size = New System.Drawing.Size(73, 20)
        Me.deScheduleStartTime.TabIndex = 7
        '
        'LabelControl18
        '
        Me.LabelControl18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl18.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl18.Size = New System.Drawing.Size(104, 20)
        Me.LabelControl18.TabIndex = 0
        Me.LabelControl18.Text = "Schedule Start Time"
        '
        'LabelControl19
        '
        Me.LabelControl19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl19.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl19.Name = "LabelControl19"
        Me.LabelControl19.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl19.Size = New System.Drawing.Size(104, 20)
        Me.LabelControl19.TabIndex = 1
        Me.LabelControl19.Text = "Schedule Interval"
        '
        'cmbScheduleInterval
        '
        Me.cmbScheduleInterval.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbScheduleInterval.EditValue = "Select"
        Me.cmbScheduleInterval.Location = New System.Drawing.Point(113, 29)
        Me.cmbScheduleInterval.Name = "cmbScheduleInterval"
        Me.cmbScheduleInterval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbScheduleInterval.Properties.Items.AddRange(New Object() {"Select", "RAW", "HOUR", "DAY", "WEEK", "MONTH", "QUARTER", "YEAR"})
        Me.cmbScheduleInterval.Size = New System.Drawing.Size(73, 20)
        Me.cmbScheduleInterval.TabIndex = 8
        '
        'grpPeriod
        '
        Me.grpPeriod.Controls.Add(Me.tlpPeriodSelection)
        Me.grpPeriod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPeriod.Location = New System.Drawing.Point(3, 3)
        Me.grpPeriod.Name = "grpPeriod"
        Me.grpPeriod.Size = New System.Drawing.Size(188, 138)
        Me.grpPeriod.TabIndex = 0
        Me.grpPeriod.Text = "Period Selection"
        '
        'tlpPeriodSelection
        '
        Me.tlpPeriodSelection.ColumnCount = 2
        Me.tlpPeriodSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpPeriodSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPeriodSelection.Controls.Add(Me.cmbPredefTimeStats, 1, 0)
        Me.tlpPeriodSelection.Controls.Add(Me.LabelControl15, 0, 0)
        Me.tlpPeriodSelection.Controls.Add(Me.LabelControl16, 0, 1)
        Me.tlpPeriodSelection.Controls.Add(Me.LabelControl17, 0, 2)
        Me.tlpPeriodSelection.Controls.Add(Me.dePeriodStartTime, 1, 1)
        Me.tlpPeriodSelection.Controls.Add(Me.dePeriodEndTime, 1, 2)
        Me.tlpPeriodSelection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpPeriodSelection.Location = New System.Drawing.Point(2, 23)
        Me.tlpPeriodSelection.Name = "tlpPeriodSelection"
        Me.tlpPeriodSelection.RowCount = 4
        Me.tlpPeriodSelection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpPeriodSelection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpPeriodSelection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpPeriodSelection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPeriodSelection.Size = New System.Drawing.Size(184, 113)
        Me.tlpPeriodSelection.TabIndex = 0
        '
        'cmbPredefTimeStats
        '
        Me.cmbPredefTimeStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbPredefTimeStats.Location = New System.Drawing.Point(103, 3)
        Me.cmbPredefTimeStats.Name = "cmbPredefTimeStats"
        Me.cmbPredefTimeStats.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPredefTimeStats.Size = New System.Drawing.Size(78, 20)
        Me.cmbPredefTimeStats.TabIndex = 4
        '
        'LabelControl15
        '
        Me.LabelControl15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl15.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl15.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl15.TabIndex = 0
        Me.LabelControl15.Text = "Predefined Period"
        '
        'LabelControl16
        '
        Me.LabelControl16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl16.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl16.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl16.TabIndex = 1
        Me.LabelControl16.Text = "Period Start Time"
        '
        'LabelControl17
        '
        Me.LabelControl17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl17.Location = New System.Drawing.Point(3, 55)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl17.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl17.TabIndex = 2
        Me.LabelControl17.Text = "Period End Time"
        '
        'dePeriodStartTime
        '
        Me.dePeriodStartTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dePeriodStartTime.EditValue = New Date(2016, 8, 11, 11, 0, 0, 0)
        Me.dePeriodStartTime.Location = New System.Drawing.Point(103, 29)
        Me.dePeriodStartTime.Name = "dePeriodStartTime"
        Me.dePeriodStartTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePeriodStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePeriodStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePeriodStartTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.dePeriodStartTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePeriodStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dePeriodStartTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePeriodStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dePeriodStartTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dePeriodStartTime.Properties.MaskSettings.Set("mask", "dd/MM/yyyy HH:mm")
        Me.dePeriodStartTime.Properties.MaskSettings.Set("placeholder", Global.Microsoft.VisualBasic.ChrW(47))
        Me.dePeriodStartTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePeriodStartTime.Size = New System.Drawing.Size(78, 20)
        Me.dePeriodStartTime.TabIndex = 5
        '
        'dePeriodEndTime
        '
        Me.dePeriodEndTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dePeriodEndTime.EditValue = New Date(2016, 8, 11, 11, 0, 0, 0)
        Me.dePeriodEndTime.Location = New System.Drawing.Point(103, 55)
        Me.dePeriodEndTime.Name = "dePeriodEndTime"
        Me.dePeriodEndTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePeriodEndTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePeriodEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePeriodEndTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.dePeriodEndTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePeriodEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dePeriodEndTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePeriodEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dePeriodEndTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dePeriodEndTime.Properties.MaskSettings.Set("mask", "dd/MM/yyyy HH:mm")
        Me.dePeriodEndTime.Properties.MaskSettings.Set("placeholder", Global.Microsoft.VisualBasic.ChrW(47))
        Me.dePeriodEndTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePeriodEndTime.Size = New System.Drawing.Size(78, 20)
        Me.dePeriodEndTime.TabIndex = 6
        '
        'xtcSQL
        '
        Me.xtcSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcSQL.Location = New System.Drawing.Point(3, 292)
        Me.xtcSQL.Name = "xtcSQL"
        Me.tlpReportConfig.SetRowSpan(Me.xtcSQL, 2)
        Me.xtcSQL.SelectedTabPage = Me.xtpAutoSQL
        Me.xtcSQL.Size = New System.Drawing.Size(588, 318)
        Me.xtcSQL.TabIndex = 7
        Me.xtcSQL.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpAutoSQL, Me.xtpManualSQL})
        '
        'xtpAutoSQL
        '
        Me.xtpAutoSQL.Controls.Add(Me.tlpAutoSQL)
        Me.xtpAutoSQL.Name = "xtpAutoSQL"
        Me.xtpAutoSQL.Size = New System.Drawing.Size(586, 293)
        Me.xtpAutoSQL.Text = "Auto SQL"
        '
        'tlpAutoSQL
        '
        Me.tlpAutoSQL.ColumnCount = 1
        Me.tlpAutoSQL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAutoSQL.Controls.Add(Me.tlpConfig2, 0, 0)
        Me.tlpAutoSQL.Controls.Add(Me.tlpKPIandFilter, 0, 1)
        Me.tlpAutoSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAutoSQL.Location = New System.Drawing.Point(0, 0)
        Me.tlpAutoSQL.Name = "tlpAutoSQL"
        Me.tlpAutoSQL.RowCount = 2
        Me.tlpAutoSQL.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.tlpAutoSQL.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAutoSQL.Size = New System.Drawing.Size(586, 293)
        Me.tlpAutoSQL.TabIndex = 0
        '
        'tlpConfig2
        '
        Me.tlpConfig2.ColumnCount = 3
        Me.tlpConfig2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpConfig2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.tlpConfig2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpConfig2.Controls.Add(Me.LabelControl28, 0, 0)
        Me.tlpConfig2.Controls.Add(Me.grpAggr, 0, 0)
        Me.tlpConfig2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpConfig2.Location = New System.Drawing.Point(3, 3)
        Me.tlpConfig2.Name = "tlpConfig2"
        Me.tlpConfig2.RowCount = 1
        Me.tlpConfig2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpConfig2.Size = New System.Drawing.Size(580, 60)
        Me.tlpConfig2.TabIndex = 4
        '
        'LabelControl28
        '
        Me.LabelControl28.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl28.Appearance.Options.UseForeColor = True
        Me.LabelControl28.Appearance.Options.UseTextOptions = True
        Me.LabelControl28.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl28.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl28.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.LabelControl28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl28.Location = New System.Drawing.Point(391, 3)
        Me.LabelControl28.Name = "LabelControl28"
        Me.LabelControl28.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl28.Size = New System.Drawing.Size(186, 54)
        Me.LabelControl28.TabIndex = 23
        Me.LabelControl28.Text = "NOTE: When using ""Auto SQL"", leave ""Manual SQL"" empty."
        Me.LabelControl28.ToolTipTitle = "NOTE: When using ""Auto SQL"", leave ""Manual SQL"" empty."
        '
        'grpAggr
        '
        Me.tlpConfig2.SetColumnSpan(Me.grpAggr, 2)
        Me.grpAggr.Controls.Add(Me.TableLayoutPanel3)
        Me.grpAggr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpAggr.Location = New System.Drawing.Point(3, 3)
        Me.grpAggr.Name = "grpAggr"
        Me.grpAggr.Size = New System.Drawing.Size(382, 54)
        Me.grpAggr.TabIndex = 0
        Me.grpAggr.Text = "Aggregation"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.cmbObjectAggr, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl24, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbTimeAggr, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl25, 2, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(378, 29)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'cmbObjectAggr
        '
        Me.cmbObjectAggr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbObjectAggr.Location = New System.Drawing.Point(93, 5)
        Me.cmbObjectAggr.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbObjectAggr.Name = "cmbObjectAggr"
        Me.cmbObjectAggr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbObjectAggr.Size = New System.Drawing.Size(93, 20)
        Me.cmbObjectAggr.TabIndex = 10
        '
        'LabelControl24
        '
        Me.LabelControl24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl24.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl24.Name = "LabelControl24"
        Me.LabelControl24.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl24.Size = New System.Drawing.Size(84, 23)
        Me.LabelControl24.TabIndex = 0
        Me.LabelControl24.Text = "Object Aggr."
        '
        'cmbTimeAggr
        '
        Me.cmbTimeAggr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTimeAggr.EditValue = "Select"
        Me.cmbTimeAggr.Location = New System.Drawing.Point(282, 5)
        Me.cmbTimeAggr.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbTimeAggr.Name = "cmbTimeAggr"
        Me.cmbTimeAggr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTimeAggr.Properties.Items.AddRange(New Object() {"Select", "RAW", "HOUR", "DAY", "BH", "WEEK", "MONTH"})
        Me.cmbTimeAggr.Size = New System.Drawing.Size(93, 20)
        Me.cmbTimeAggr.TabIndex = 11
        '
        'LabelControl25
        '
        Me.LabelControl25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl25.Location = New System.Drawing.Point(192, 3)
        Me.LabelControl25.Name = "LabelControl25"
        Me.LabelControl25.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl25.Size = New System.Drawing.Size(84, 23)
        Me.LabelControl25.TabIndex = 1
        Me.LabelControl25.Text = "Time Aggr."
        '
        'tlpKPIandFilter
        '
        Me.tlpKPIandFilter.ColumnCount = 2
        Me.tlpKPIandFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKPIandFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKPIandFilter.Controls.Add(Me.grpKPIs, 0, 0)
        Me.tlpKPIandFilter.Controls.Add(Me.TableLayoutPanel9, 1, 0)
        Me.tlpKPIandFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKPIandFilter.Location = New System.Drawing.Point(3, 69)
        Me.tlpKPIandFilter.Name = "tlpKPIandFilter"
        Me.tlpKPIandFilter.RowCount = 1
        Me.tlpKPIandFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPIandFilter.Size = New System.Drawing.Size(580, 221)
        Me.tlpKPIandFilter.TabIndex = 5
        '
        'grpKPIs
        '
        Me.grpKPIs.Controls.Add(Me.tlpKPI)
        Me.grpKPIs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpKPIs.Location = New System.Drawing.Point(3, 3)
        Me.grpKPIs.Name = "grpKPIs"
        Me.grpKPIs.Size = New System.Drawing.Size(284, 215)
        Me.grpKPIs.TabIndex = 2
        Me.grpKPIs.Text = "KPIs"
        '
        'tlpKPI
        '
        Me.tlpKPI.ColumnCount = 1
        Me.tlpKPI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPI.Controls.Add(Me.grdKPI, 0, 1)
        Me.tlpKPI.Controls.Add(Me.tlpKPIBtns, 0, 0)
        Me.tlpKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKPI.Location = New System.Drawing.Point(2, 23)
        Me.tlpKPI.Name = "tlpKPI"
        Me.tlpKPI.RowCount = 2
        Me.tlpKPI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpKPI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPI.Size = New System.Drawing.Size(280, 190)
        Me.tlpKPI.TabIndex = 0
        '
        'grdKPI
        '
        Me.grdKPI.AllowDrop = True
        Me.grdKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdKPI.Location = New System.Drawing.Point(3, 38)
        Me.grdKPI.MainView = Me.gvKPI
        Me.grdKPI.Name = "grdKPI"
        Me.grdKPI.Size = New System.Drawing.Size(274, 149)
        Me.grdKPI.TabIndex = 4
        Me.grdKPI.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKPI})
        '
        'gvKPI
        '
        Me.gvKPI.GridControl = Me.grdKPI
        Me.gvKPI.Name = "gvKPI"
        Me.gvKPI.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPI.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPI.OptionsBehavior.Editable = False
        Me.gvKPI.OptionsBehavior.ReadOnly = True
        Me.gvKPI.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsCustomization.AllowSort = False
        Me.gvKPI.OptionsView.ColumnAutoWidth = False
        Me.gvKPI.OptionsView.ShowGroupPanel = False
        '
        'tlpKPIBtns
        '
        Me.tlpKPIBtns.ColumnCount = 3
        Me.tlpKPIBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPIBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpKPIBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpKPIBtns.Controls.Add(Me.btnDeleteKPI, 2, 0)
        Me.tlpKPIBtns.Controls.Add(Me.btnAddKPI, 1, 0)
        Me.tlpKPIBtns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKPIBtns.Location = New System.Drawing.Point(0, 0)
        Me.tlpKPIBtns.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpKPIBtns.Name = "tlpKPIBtns"
        Me.tlpKPIBtns.RowCount = 1
        Me.tlpKPIBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPIBtns.Size = New System.Drawing.Size(280, 35)
        Me.tlpKPIBtns.TabIndex = 0
        '
        'btnDeleteKPI
        '
        Me.btnDeleteKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteKPI.Location = New System.Drawing.Point(213, 3)
        Me.btnDeleteKPI.Name = "btnDeleteKPI"
        Me.btnDeleteKPI.Size = New System.Drawing.Size(64, 29)
        Me.btnDeleteKPI.TabIndex = 15
        Me.btnDeleteKPI.Text = "Delete"
        '
        'btnAddKPI
        '
        Me.btnAddKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddKPI.Location = New System.Drawing.Point(143, 3)
        Me.btnAddKPI.Name = "btnAddKPI"
        Me.btnAddKPI.Size = New System.Drawing.Size(64, 29)
        Me.btnAddKPI.TabIndex = 14
        Me.btnAddKPI.Text = "Add"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.grpAliases, 0, 1)
        Me.TableLayoutPanel9.Controls.Add(Me.grpObjectFilter, 0, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(293, 3)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(284, 215)
        Me.TableLayoutPanel9.TabIndex = 3
        '
        'grpAliases
        '
        Me.grpAliases.Controls.Add(Me.TableLayoutPanel10)
        Me.grpAliases.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpAliases.Location = New System.Drawing.Point(3, 110)
        Me.grpAliases.Name = "grpAliases"
        Me.grpAliases.Size = New System.Drawing.Size(278, 102)
        Me.grpAliases.TabIndex = 4
        Me.grpAliases.Text = "Aliases"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.grdAliases, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel12, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(274, 77)
        Me.TableLayoutPanel10.TabIndex = 1
        '
        'grdAliases
        '
        Me.grdAliases.AllowDrop = True
        Me.grdAliases.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAliases.Location = New System.Drawing.Point(3, 3)
        Me.grdAliases.MainView = Me.gvAliases
        Me.grdAliases.Name = "grdAliases"
        Me.grdAliases.Size = New System.Drawing.Size(188, 71)
        Me.grdAliases.TabIndex = 4
        Me.grdAliases.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvAliases})
        '
        'gvAliases
        '
        Me.gvAliases.GridControl = Me.grdAliases
        Me.gvAliases.Name = "gvAliases"
        Me.gvAliases.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvAliases.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvAliases.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvAliases.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvAliases.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvAliases.OptionsCustomization.AllowSort = False
        Me.gvAliases.OptionsView.ColumnAutoWidth = False
        Me.gvAliases.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 1
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.btnAddAlises, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnDeleteAliases, 0, 1)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(194, 0)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 3
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(80, 77)
        Me.TableLayoutPanel12.TabIndex = 1
        '
        'btnAddAlises
        '
        Me.btnAddAlises.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddAlises.Location = New System.Drawing.Point(3, 3)
        Me.btnAddAlises.Name = "btnAddAlises"
        Me.btnAddAlises.Size = New System.Drawing.Size(74, 29)
        Me.btnAddAlises.TabIndex = 16
        Me.btnAddAlises.Text = "Add"
        '
        'btnDeleteAliases
        '
        Me.btnDeleteAliases.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteAliases.Location = New System.Drawing.Point(3, 38)
        Me.btnDeleteAliases.Name = "btnDeleteAliases"
        Me.btnDeleteAliases.Size = New System.Drawing.Size(74, 29)
        Me.btnDeleteAliases.TabIndex = 17
        Me.btnDeleteAliases.Text = "Delete"
        '
        'grpObjectFilter
        '
        Me.grpObjectFilter.Controls.Add(Me.tlpObjFilter)
        Me.grpObjectFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpObjectFilter.Location = New System.Drawing.Point(3, 3)
        Me.grpObjectFilter.Name = "grpObjectFilter"
        Me.grpObjectFilter.Size = New System.Drawing.Size(278, 101)
        Me.grpObjectFilter.TabIndex = 3
        Me.grpObjectFilter.Text = "Object Filter"
        '
        'tlpObjFilter
        '
        Me.tlpObjFilter.ColumnCount = 2
        Me.tlpObjFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpObjFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpObjFilter.Controls.Add(Me.grdObjFilter, 0, 0)
        Me.tlpObjFilter.Controls.Add(Me.tlpObjFilterBtns, 1, 0)
        Me.tlpObjFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpObjFilter.Location = New System.Drawing.Point(2, 23)
        Me.tlpObjFilter.Name = "tlpObjFilter"
        Me.tlpObjFilter.RowCount = 1
        Me.tlpObjFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpObjFilter.Size = New System.Drawing.Size(274, 76)
        Me.tlpObjFilter.TabIndex = 1
        '
        'grdObjFilter
        '
        Me.grdObjFilter.AllowDrop = True
        Me.grdObjFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdObjFilter.Location = New System.Drawing.Point(3, 3)
        Me.grdObjFilter.MainView = Me.gvObjFilter
        Me.grdObjFilter.Name = "grdObjFilter"
        Me.grdObjFilter.Size = New System.Drawing.Size(188, 70)
        Me.grdObjFilter.TabIndex = 4
        Me.grdObjFilter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvObjFilter})
        '
        'gvObjFilter
        '
        Me.gvObjFilter.GridControl = Me.grdObjFilter
        Me.gvObjFilter.Name = "gvObjFilter"
        Me.gvObjFilter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvObjFilter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvObjFilter.OptionsBehavior.Editable = False
        Me.gvObjFilter.OptionsBehavior.ReadOnly = True
        Me.gvObjFilter.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObjFilter.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObjFilter.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObjFilter.OptionsCustomization.AllowSort = False
        Me.gvObjFilter.OptionsView.ColumnAutoWidth = False
        Me.gvObjFilter.OptionsView.ShowGroupPanel = False
        '
        'tlpObjFilterBtns
        '
        Me.tlpObjFilterBtns.ColumnCount = 1
        Me.tlpObjFilterBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpObjFilterBtns.Controls.Add(Me.btnAddObjFilter, 0, 0)
        Me.tlpObjFilterBtns.Controls.Add(Me.btnDeleteObjFilter, 0, 1)
        Me.tlpObjFilterBtns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpObjFilterBtns.Location = New System.Drawing.Point(194, 0)
        Me.tlpObjFilterBtns.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpObjFilterBtns.Name = "tlpObjFilterBtns"
        Me.tlpObjFilterBtns.RowCount = 3
        Me.tlpObjFilterBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpObjFilterBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpObjFilterBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpObjFilterBtns.Size = New System.Drawing.Size(80, 76)
        Me.tlpObjFilterBtns.TabIndex = 1
        '
        'btnAddObjFilter
        '
        Me.btnAddObjFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddObjFilter.Location = New System.Drawing.Point(3, 3)
        Me.btnAddObjFilter.Name = "btnAddObjFilter"
        Me.btnAddObjFilter.Size = New System.Drawing.Size(74, 29)
        Me.btnAddObjFilter.TabIndex = 16
        Me.btnAddObjFilter.Text = "Add"
        '
        'btnDeleteObjFilter
        '
        Me.btnDeleteObjFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteObjFilter.Location = New System.Drawing.Point(3, 38)
        Me.btnDeleteObjFilter.Name = "btnDeleteObjFilter"
        Me.btnDeleteObjFilter.Size = New System.Drawing.Size(74, 29)
        Me.btnDeleteObjFilter.TabIndex = 17
        Me.btnDeleteObjFilter.Text = "Delete"
        '
        'xtpManualSQL
        '
        Me.xtpManualSQL.Controls.Add(Me.sccManualSQL)
        Me.xtpManualSQL.Name = "xtpManualSQL"
        Me.xtpManualSQL.Size = New System.Drawing.Size(586, 293)
        Me.xtpManualSQL.Text = "Manual SQL"
        '
        'sccManualSQL
        '
        Me.sccManualSQL.Collapsed = True
        Me.sccManualSQL.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.sccManualSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccManualSQL.Location = New System.Drawing.Point(0, 0)
        Me.sccManualSQL.Name = "sccManualSQL"
        '
        'sccManualSQL.Panel1
        '
        Me.sccManualSQL.Panel1.Controls.Add(Me.tlpManualSQLText)
        Me.sccManualSQL.Panel1.MinSize = 300
        Me.sccManualSQL.Panel1.Text = "Panel1"
        '
        'sccManualSQL.Panel2
        '
        Me.sccManualSQL.Panel2.Controls.Add(Me.tlpManualSQLGrids)
        Me.sccManualSQL.Panel2.MinSize = 300
        Me.sccManualSQL.Panel2.Text = "Panel2"
        Me.sccManualSQL.Size = New System.Drawing.Size(586, 293)
        Me.sccManualSQL.SplitterPosition = 300
        Me.sccManualSQL.TabIndex = 0
        '
        'tlpManualSQLText
        '
        Me.tlpManualSQLText.ColumnCount = 1
        Me.tlpManualSQLText.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpManualSQLText.Controls.Add(Me.TableLayoutPanel8, 0, 1)
        Me.tlpManualSQLText.Controls.Add(Me.txtManualSQL, 0, 0)
        Me.tlpManualSQLText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpManualSQLText.Location = New System.Drawing.Point(0, 0)
        Me.tlpManualSQLText.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpManualSQLText.Name = "tlpManualSQLText"
        Me.tlpManualSQLText.RowCount = 2
        Me.tlpManualSQLText.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpManualSQLText.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpManualSQLText.Size = New System.Drawing.Size(576, 293)
        Me.tlpManualSQLText.TabIndex = 0
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 4
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.btnTest, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btnStartTime, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btnEndTime, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.LabelControl26, 3, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 258)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(576, 35)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'btnTest
        '
        Me.btnTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTest.Location = New System.Drawing.Point(2, 2)
        Me.btnTest.Margin = New System.Windows.Forms.Padding(2)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(66, 31)
        Me.btnTest.TabIndex = 0
        Me.btnTest.Text = "Test"
        '
        'btnStartTime
        '
        Me.btnStartTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnStartTime.Location = New System.Drawing.Point(73, 3)
        Me.btnStartTime.Name = "btnStartTime"
        Me.btnStartTime.Size = New System.Drawing.Size(64, 29)
        Me.btnStartTime.TabIndex = 20
        Me.btnStartTime.Text = "Start Time"
        '
        'btnEndTime
        '
        Me.btnEndTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnEndTime.Location = New System.Drawing.Point(143, 3)
        Me.btnEndTime.Name = "btnEndTime"
        Me.btnEndTime.Size = New System.Drawing.Size(64, 29)
        Me.btnEndTime.TabIndex = 21
        Me.btnEndTime.Text = "End Time"
        '
        'LabelControl26
        '
        Me.LabelControl26.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl26.Appearance.Options.UseForeColor = True
        Me.LabelControl26.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.LabelControl26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl26.Location = New System.Drawing.Point(213, 3)
        Me.LabelControl26.Name = "LabelControl26"
        Me.LabelControl26.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl26.Size = New System.Drawing.Size(360, 29)
        Me.LabelControl26.TabIndex = 22
        Me.LabelControl26.Text = "NOTE: When using ""Auto SQL"", leave ""Manual SQL"" empty."
        Me.LabelControl26.ToolTipTitle = "NOTE: When using ""Auto SQL"", leave ""Manual SQL"" empty."
        '
        'txtManualSQL
        '
        Me.txtManualSQL.AllowDrop = True
        Me.txtManualSQL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtManualSQL.Location = New System.Drawing.Point(3, 3)
        Me.txtManualSQL.Name = "txtManualSQL"
        Me.txtManualSQL.Size = New System.Drawing.Size(570, 252)
        Me.txtManualSQL.TabIndex = 1
        '
        'tlpManualSQLGrids
        '
        Me.tlpManualSQLGrids.ColumnCount = 1
        Me.tlpManualSQLGrids.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpManualSQLGrids.Controls.Add(Me.gcKPIs, 0, 2)
        Me.tlpManualSQLGrids.Controls.Add(Me.gcTables, 0, 0)
        Me.tlpManualSQLGrids.Controls.Add(Me.gcColumns, 0, 1)
        Me.tlpManualSQLGrids.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpManualSQLGrids.Location = New System.Drawing.Point(0, 0)
        Me.tlpManualSQLGrids.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpManualSQLGrids.Name = "tlpManualSQLGrids"
        Me.tlpManualSQLGrids.RowCount = 3
        Me.tlpManualSQLGrids.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpManualSQLGrids.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.tlpManualSQLGrids.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.tlpManualSQLGrids.Size = New System.Drawing.Size(0, 0)
        Me.tlpManualSQLGrids.TabIndex = 1
        '
        'gcKPIs
        '
        Me.gcKPIs.AllowDrop = True
        Me.gcKPIs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPIs.Location = New System.Drawing.Point(3, 3)
        Me.gcKPIs.MainView = Me.gvKPIs
        Me.gcKPIs.Name = "gcKPIs"
        Me.gcKPIs.Size = New System.Drawing.Size(1, 1)
        Me.gcKPIs.TabIndex = 7
        Me.gcKPIs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKPIs})
        '
        'gvKPIs
        '
        Me.gvKPIs.GridControl = Me.gcKPIs
        Me.gvKPIs.Name = "gvKPIs"
        Me.gvKPIs.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPIs.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPIs.OptionsBehavior.Editable = False
        Me.gvKPIs.OptionsBehavior.ReadOnly = True
        Me.gvKPIs.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPIs.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPIs.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPIs.OptionsCustomization.AllowSort = False
        Me.gvKPIs.OptionsView.ColumnAutoWidth = False
        Me.gvKPIs.OptionsView.ShowGroupPanel = False
        '
        'gcTables
        '
        Me.gcTables.AllowDrop = True
        Me.gcTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTables.Location = New System.Drawing.Point(3, 3)
        Me.gcTables.MainView = Me.gvTables
        Me.gcTables.Name = "gcTables"
        Me.gcTables.Size = New System.Drawing.Size(1, 1)
        Me.gcTables.TabIndex = 5
        Me.gcTables.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTables})
        '
        'gvTables
        '
        Me.gvTables.GridControl = Me.gcTables
        Me.gvTables.Name = "gvTables"
        Me.gvTables.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTables.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTables.OptionsBehavior.Editable = False
        Me.gvTables.OptionsBehavior.ReadOnly = True
        Me.gvTables.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTables.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTables.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTables.OptionsCustomization.AllowSort = False
        Me.gvTables.OptionsView.ColumnAutoWidth = False
        Me.gvTables.OptionsView.ShowGroupPanel = False
        '
        'gcColumns
        '
        Me.gcColumns.AllowDrop = True
        Me.gcColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcColumns.Location = New System.Drawing.Point(3, 3)
        Me.gcColumns.MainView = Me.gvColumns
        Me.gcColumns.Name = "gcColumns"
        Me.gcColumns.Size = New System.Drawing.Size(1, 1)
        Me.gcColumns.TabIndex = 6
        Me.gcColumns.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvColumns})
        '
        'gvColumns
        '
        Me.gvColumns.GridControl = Me.gcColumns
        Me.gvColumns.Name = "gvColumns"
        Me.gvColumns.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvColumns.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvColumns.OptionsBehavior.Editable = False
        Me.gvColumns.OptionsBehavior.ReadOnly = True
        Me.gvColumns.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvColumns.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvColumns.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvColumns.OptionsCustomization.AllowSort = False
        Me.gvColumns.OptionsView.ColumnAutoWidth = False
        Me.gvColumns.OptionsView.ShowGroupPanel = False
        '
        'xtcViewReport
        '
        Me.xtcViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcViewReport.Location = New System.Drawing.Point(3, 673)
        Me.xtcViewReport.Name = "xtcViewReport"
        Me.xtcViewReport.SelectedTabPage = Me.xtpViewReport
        Me.xtcViewReport.Size = New System.Drawing.Size(1242, 192)
        Me.xtcViewReport.TabIndex = 3
        Me.xtcViewReport.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpViewReport, Me.xtpReportStatus})
        '
        'xtpViewReport
        '
        Me.xtpViewReport.Controls.Add(Me.grpViewReport)
        Me.xtpViewReport.Name = "xtpViewReport"
        Me.xtpViewReport.Size = New System.Drawing.Size(1240, 167)
        Me.xtpViewReport.Text = "View Report"
        '
        'grpViewReport
        '
        Me.grpViewReport.Controls.Add(Me.tlpReportView)
        Me.grpViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpViewReport.Location = New System.Drawing.Point(0, 0)
        Me.grpViewReport.Name = "grpViewReport"
        Me.grpViewReport.Size = New System.Drawing.Size(1240, 167)
        Me.grpViewReport.TabIndex = 1
        Me.grpViewReport.Text = "View Report"
        '
        'tlpReportView
        '
        Me.tlpReportView.ColumnCount = 1
        Me.tlpReportView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReportView.Controls.Add(Me.gcViewReport, 0, 1)
        Me.tlpReportView.Controls.Add(Me.tlpViewReport, 0, 0)
        Me.tlpReportView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpReportView.Location = New System.Drawing.Point(2, 23)
        Me.tlpReportView.Name = "tlpReportView"
        Me.tlpReportView.RowCount = 2
        Me.tlpReportView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpReportView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpReportView.Size = New System.Drawing.Size(1236, 142)
        Me.tlpReportView.TabIndex = 1
        '
        'gcViewReport
        '
        Me.gcViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcViewReport.Location = New System.Drawing.Point(3, 43)
        Me.gcViewReport.MainView = Me.gvViewReport
        Me.gcViewReport.Name = "gcViewReport"
        Me.gcViewReport.Size = New System.Drawing.Size(1230, 96)
        Me.gcViewReport.TabIndex = 17
        Me.gcViewReport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvViewReport})
        '
        'gvViewReport
        '
        Me.gvViewReport.GridControl = Me.gcViewReport
        Me.gvViewReport.Name = "gvViewReport"
        Me.gvViewReport.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvViewReport.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvViewReport.OptionsBehavior.Editable = False
        Me.gvViewReport.OptionsBehavior.ReadOnly = True
        Me.gvViewReport.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvViewReport.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvViewReport.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvViewReport.OptionsSelection.MultiSelect = True
        Me.gvViewReport.OptionsView.ColumnAutoWidth = False
        Me.gvViewReport.OptionsView.ShowGroupPanel = False
        '
        'tlpViewReport
        '
        Me.tlpViewReport.ColumnCount = 3
        Me.tlpViewReport.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpViewReport.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpViewReport.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpViewReport.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpViewReport.Controls.Add(Me.btnLoadReport, 1, 0)
        Me.tlpViewReport.Controls.Add(Me.btnExport2CSV, 2, 0)
        Me.tlpViewReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpViewReport.Location = New System.Drawing.Point(0, 0)
        Me.tlpViewReport.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpViewReport.Name = "tlpViewReport"
        Me.tlpViewReport.RowCount = 1
        Me.tlpViewReport.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpViewReport.Size = New System.Drawing.Size(1236, 40)
        Me.tlpViewReport.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(1030, 34)
        Me.lblMessage.TabIndex = 18
        '
        'btnLoadReport
        '
        Me.btnLoadReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLoadReport.Location = New System.Drawing.Point(1039, 3)
        Me.btnLoadReport.Name = "btnLoadReport"
        Me.btnLoadReport.Size = New System.Drawing.Size(94, 34)
        Me.btnLoadReport.TabIndex = 19
        Me.btnLoadReport.Text = "Load"
        '
        'btnExport2CSV
        '
        Me.btnExport2CSV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExport2CSV.Location = New System.Drawing.Point(1139, 3)
        Me.btnExport2CSV.Name = "btnExport2CSV"
        Me.btnExport2CSV.Size = New System.Drawing.Size(94, 34)
        Me.btnExport2CSV.TabIndex = 20
        Me.btnExport2CSV.Text = "Export to File"
        '
        'xtpReportStatus
        '
        Me.xtpReportStatus.Controls.Add(Me.gcReportStatus)
        Me.xtpReportStatus.Name = "xtpReportStatus"
        Me.xtpReportStatus.Size = New System.Drawing.Size(1240, 167)
        Me.xtpReportStatus.Text = "Report Status"
        '
        'gcReportStatus
        '
        Me.gcReportStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcReportStatus.Location = New System.Drawing.Point(0, 0)
        Me.gcReportStatus.MainView = Me.gvReportStatus
        Me.gcReportStatus.Name = "gcReportStatus"
        Me.gcReportStatus.Size = New System.Drawing.Size(1240, 167)
        Me.gcReportStatus.TabIndex = 18
        Me.gcReportStatus.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReportStatus})
        '
        'gvReportStatus
        '
        Me.gvReportStatus.GridControl = Me.gcReportStatus
        Me.gvReportStatus.Name = "gvReportStatus"
        Me.gvReportStatus.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportStatus.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportStatus.OptionsBehavior.Editable = False
        Me.gvReportStatus.OptionsBehavior.ReadOnly = True
        Me.gvReportStatus.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportStatus.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportStatus.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportStatus.OptionsView.ColumnAutoWidth = False
        Me.gvReportStatus.OptionsView.ShowGroupPanel = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'frmNBIReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1248, 868)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmNBIReports.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1250, 900)
        Me.Name = "frmNBIReports"
        Me.Text = "NBI Reports"
        Me.tlpMain.ResumeLayout(False)
        CType(Me.sccReports.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccReports.Panel1.ResumeLayout(False)
        CType(Me.sccReports.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccReports.Panel2.ResumeLayout(False)
        CType(Me.sccReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccReports.ResumeLayout(False)
        CType(Me.grpReportsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReportsList.ResumeLayout(False)
        Me.tlpReportsList.ResumeLayout(False)
        CType(Me.gcReportsList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReportsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.tlpReports.ResumeLayout(False)
        Me.tlpReportConfig.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.grpReportProperties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReportProperties.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.ceIsScheduled.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsLocked.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsEnabled.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTimeout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel16.ResumeLayout(False)
        Me.TableLayoutPanel16.PerformLayout()
        Me.tlpConfig1.ResumeLayout(False)
        CType(Me.grpOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOutput.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.cmbOutputFormat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsOutputPivotted.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOutputFolder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.ceEmailEnabled.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailAddresses.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpScheduleReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScheduleReport.ResumeLayout(False)
        Me.tlpScheduleReport.ResumeLayout(False)
        Me.tlpScheduleReport.PerformLayout()
        CType(Me.deScheduleStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deScheduleStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbScheduleInterval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPeriod.ResumeLayout(False)
        Me.tlpPeriodSelection.ResumeLayout(False)
        Me.tlpPeriodSelection.PerformLayout()
        CType(Me.cmbPredefTimeStats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePeriodStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePeriodStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePeriodEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePeriodEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcSQL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcSQL.ResumeLayout(False)
        Me.xtpAutoSQL.ResumeLayout(False)
        Me.tlpAutoSQL.ResumeLayout(False)
        Me.tlpConfig2.ResumeLayout(False)
        Me.tlpConfig2.PerformLayout()
        CType(Me.grpAggr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAggr.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.cmbObjectAggr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTimeAggr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpKPIandFilter.ResumeLayout(False)
        CType(Me.grpKPIs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpKPIs.ResumeLayout(False)
        Me.tlpKPI.ResumeLayout(False)
        CType(Me.grdKPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvKPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpKPIBtns.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        CType(Me.grpAliases, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAliases.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        CType(Me.grdAliases, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAliases, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel12.ResumeLayout(False)
        CType(Me.grpObjectFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpObjectFilter.ResumeLayout(False)
        Me.tlpObjFilter.ResumeLayout(False)
        CType(Me.grdObjFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvObjFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpObjFilterBtns.ResumeLayout(False)
        Me.xtpManualSQL.ResumeLayout(False)
        CType(Me.sccManualSQL.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccManualSQL.Panel1.ResumeLayout(False)
        CType(Me.sccManualSQL.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccManualSQL.Panel2.ResumeLayout(False)
        CType(Me.sccManualSQL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccManualSQL.ResumeLayout(False)
        Me.tlpManualSQLText.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.txtManualSQL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpManualSQLGrids.ResumeLayout(False)
        CType(Me.gcKPIs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvKPIs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcTables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcColumns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvColumns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcViewReport.ResumeLayout(False)
        Me.xtpViewReport.ResumeLayout(False)
        CType(Me.grpViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpViewReport.ResumeLayout(False)
        Me.tlpReportView.ResumeLayout(False)
        CType(Me.gcViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpViewReport.ResumeLayout(False)
        Me.tlpViewReport.PerformLayout()
        Me.xtpReportStatus.ResumeLayout(False)
        CType(Me.gcReportStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReportStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpReports As TableLayoutPanel
    Friend WithEvents tlpReportConfig As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblReportName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpReportProperties As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsEnabled As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblReportOwner As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceIsScheduled As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceIsLocked As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel14 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTechnology As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblObjectType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpConfig1 As TableLayoutPanel
    Friend WithEvents grpScheduleReport As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpScheduleReport As TableLayoutPanel
    Friend WithEvents grpPeriod As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpPeriodSelection As TableLayoutPanel
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dePeriodStartTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dePeriodEndTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbPredefTimeStats As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents deScheduleStartTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbScheduleInterval As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpConfig2 As TableLayoutPanel
    Friend WithEvents grpOutput As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbOutputFormat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ceIsOutputPivotted As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents grpAggr As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents cmbObjectAggr As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl24 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl25 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTimeAggr As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpKPIandFilter As TableLayoutPanel
    Friend WithEvents grpKPIs As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpKPI As TableLayoutPanel
    Friend WithEvents grdKPI As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKPI As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpKPIBtns As TableLayoutPanel
    Friend WithEvents btnAddKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpObjectFilter As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpObjFilter As TableLayoutPanel
    Friend WithEvents grdObjFilter As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvObjFilter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpObjFilterBtns As TableLayoutPanel
    Friend WithEvents btnAddObjFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteObjFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel16 As TableLayoutPanel
    Friend WithEvents btnSaveReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpReportsList As TableLayoutPanel
    Friend WithEvents grpReportsList As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel17 As TableLayoutPanel
    Friend WithEvents btnDeleteReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnModifyReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcReportsList As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReportsList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpReportView As TableLayoutPanel
    Friend WithEvents gcViewReport As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvViewReport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpViewReport As TableLayoutPanel
    Friend WithEvents btnLoadReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExport2CSV As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblLastRunTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl27 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpViewReport As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtcSQL As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpAutoSQL As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpManualSQL As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tlpAutoSQL As TableLayoutPanel
    Friend WithEvents tlpManualSQLText As TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents lblTestStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnTest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtManualSQL As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents tlpManualSQLGrids As TableLayoutPanel
    Friend WithEvents gcKPIs As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKPIs As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcTables As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTables As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcColumns As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvColumns As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnStartTime As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEndTime As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents grpAliases As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents grdAliases As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvAliases As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents btnAddAlises As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteAliases As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sccManualSQL As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccReports As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtOutputFolder As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblReportLockedMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl26 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl28 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl29 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ceEmailEnabled As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtEmailAddresses As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl30 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl31 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblReportDescription As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCopyReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xtcViewReport As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpViewReport As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpReportStatus As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcReportStatus As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReportStatus As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl32 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl33 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTimeout As DevExpress.XtraEditors.TextEdit
End Class
