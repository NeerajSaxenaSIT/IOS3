<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlertManager
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
        Dim Annotation1 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim BoxHeaderOptions1 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim Element1 As dotnetCHARTING.WinForms.Element = New dotnetCHARTING.WinForms.Element()
        Dim Line1 As dotnetCHARTING.WinForms.Line = New dotnetCHARTING.WinForms.Line()
        Dim BoxHeaderOptions2 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim BoxHeaderOptions3 As dotnetCHARTING.WinForms.BoxHeaderOptions = New dotnetCHARTING.WinForms.BoxHeaderOptions()
        Dim Element2 As dotnetCHARTING.WinForms.Element = New dotnetCHARTING.WinForms.Element()
        Dim Line2 As dotnetCHARTING.WinForms.Line = New dotnetCHARTING.WinForms.Line()
        Dim View3D1 As dotnetCHARTING.WinForms.View3D = New dotnetCHARTING.WinForms.View3D()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAlertManager))
        Me.sccMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccKpiRules = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccAlerts = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.tlpAlertMain = New System.Windows.Forms.TableLayoutPanel()
        Me.grpAlertProperties = New DevExpress.XtraEditors.GroupControl()
        Me.tlpAlertProperties = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.seAlertWindow = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.ceEventEmail = New DevExpress.XtraEditors.CheckEdit()
        Me.txtEventEmail = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.ceEventSNMP = New DevExpress.XtraEditors.CheckEdit()
        Me.txtEventSNMP = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.lblAlertOwner = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.seAlertOccurence = New DevExpress.XtraEditors.SpinEdit()
        Me.ceAlertEnabled = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel20 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceDashboardScore = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDashboardScore = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel19 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbKPIFailureColumn = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceEventReport = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl28 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbEventReport = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lstviewAlerts = New DevExpress.XtraTreeList.TreeList()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDeleteAlert = New DevExpress.XtraEditors.SimpleButton()
        Me.txtAlertSearch = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnAddNewAlert = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.propGrid = New System.Windows.Forms.PropertyGrid()
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.lblCountBreach = New DevExpress.XtraEditors.LabelControl()
        Me.deTestKPIRule = New DevExpress.XtraEditors.DateEdit()
        Me.btnTestKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.gcKPIRules = New DevExpress.XtraGrid.GridControl()
        Me.cmsKPIRule = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteKPIRuleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_KPI_Rules_Copy_All = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_KPI_Rules_Copy_SelectionWOHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_KPI_Rules_Copy_SelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvKPIRules = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.btnMethod = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.tlpConfigDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl24 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl25 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl26 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl27 = New DevExpress.XtraEditors.LabelControl()
        Me.lblConfigProcess = New DevExpress.XtraEditors.LabelControl()
        Me.tlpKpiRulesFilter = New System.Windows.Forms.TableLayoutPanel()
        Me.gcKpiRulesFilter = New DevExpress.XtraGrid.GridControl()
        Me.gvKpiRulesFilter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDeleteFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCopyFromFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.sccChart = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.tlpAlertTest = New System.Windows.Forms.TableLayoutPanel()
        Me.seDataPoints = New DevExpress.XtraEditors.SpinEdit()
        Me.btnLoad = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtObjectNameFilter = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tglAlertTest = New IOS.Library.IOSToggleButton()
        Me.btnClearChart = New DevExpress.XtraEditors.SimpleButton()
        Me.ceShowHideBreached = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowHideOutlier = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.btnAlertProcess = New DevExpress.XtraEditors.SimpleButton()
        Me.deAlertProcessDate = New DevExpress.XtraEditors.DateEdit()
        Me.sccAlertChart = New DevExpress.XtraEditors.SplitContainerControl()
        Me.chAlert = New dotnetCHARTING.WinForms.Chart()
        Me.gcChartAlert = New DevExpress.XtraGrid.GridControl()
        Me.cm_CopyGridData = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RecordCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_Copy_All = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Copy_SelectionWOHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Copy_SelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvChartAlert = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel1.SuspendLayout()
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel2.SuspendLayout()
        Me.sccMain.SuspendLayout()
        CType(Me.sccKpiRules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccKpiRules.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccKpiRules.Panel1.SuspendLayout()
        CType(Me.sccKpiRules.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccKpiRules.Panel2.SuspendLayout()
        Me.sccKpiRules.SuspendLayout()
        CType(Me.sccAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccAlerts.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccAlerts.Panel1.SuspendLayout()
        CType(Me.sccAlerts.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccAlerts.Panel2.SuspendLayout()
        Me.sccAlerts.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.tlpAlertMain.SuspendLayout()
        CType(Me.grpAlertProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAlertProperties.SuspendLayout()
        Me.tlpAlertProperties.SuspendLayout()
        CType(Me.seAlertWindow.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.ceEventEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEventEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel14.SuspendLayout()
        CType(Me.ceEventSNMP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEventSNMP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.seAlertOccurence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceAlertEnabled.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel20.SuspendLayout()
        CType(Me.ceDashboardScore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDashboardScore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel19.SuspendLayout()
        CType(Me.cmbKPIFailureColumn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.ceEventReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEventReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstviewAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel13.SuspendLayout()
        CType(Me.txtAlertSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        Me.TableLayoutPanel18.SuspendLayout()
        CType(Me.deTestKPIRule.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTestKPIRule.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcKPIRules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsKPIRule.SuspendLayout()
        CType(Me.gvKPIRules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.tlpConfigDetails.SuspendLayout()
        Me.tlpKpiRulesFilter.SuspendLayout()
        CType(Me.gcKpiRulesFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvKpiRulesFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.sccChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccChart.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccChart.Panel1.SuspendLayout()
        CType(Me.sccChart.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccChart.Panel2.SuspendLayout()
        Me.sccChart.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        Me.tlpAlertTest.SuspendLayout()
        CType(Me.seDataPoints.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObjectNameFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ceShowHideBreached.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowHideOutlier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        Me.TableLayoutPanel17.SuspendLayout()
        CType(Me.deAlertProcessDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deAlertProcessDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccAlertChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccAlertChart.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccAlertChart.Panel1.SuspendLayout()
        CType(Me.sccAlertChart.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccAlertChart.Panel2.SuspendLayout()
        Me.sccAlertChart.SuspendLayout()
        CType(Me.chAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcChartAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_CopyGridData.SuspendLayout()
        CType(Me.gvChartAlert, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.sccMain.Panel1.Controls.Add(Me.sccKpiRules)
        Me.sccMain.Panel1.MinSize = 350
        Me.sccMain.Panel1.Text = "Panel1"
        '
        'sccMain.Panel2
        '
        Me.sccMain.Panel2.Controls.Add(Me.sccChart)
        Me.sccMain.Panel2.MinSize = 250
        Me.sccMain.Panel2.Text = "Panel2"
        Me.sccMain.Size = New System.Drawing.Size(1310, 868)
        Me.sccMain.SplitterPosition = 514
        Me.sccMain.TabIndex = 0
        Me.sccMain.Text = "SplitContainerControl1"
        '
        'sccKpiRules
        '
        Me.sccKpiRules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccKpiRules.Location = New System.Drawing.Point(0, 0)
        Me.sccKpiRules.Name = "sccKpiRules"
        '
        'sccKpiRules.Panel1
        '
        Me.sccKpiRules.Panel1.Controls.Add(Me.sccAlerts)
        Me.sccKpiRules.Panel1.MinSize = 900
        Me.sccKpiRules.Panel1.Text = "Panel1"
        '
        'sccKpiRules.Panel2
        '
        Me.sccKpiRules.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.sccKpiRules.Panel2.MinSize = 350
        Me.sccKpiRules.Panel2.Text = "Panel2"
        Me.sccKpiRules.Size = New System.Drawing.Size(1310, 514)
        Me.sccKpiRules.SplitterPosition = 955
        Me.sccKpiRules.TabIndex = 0
        Me.sccKpiRules.Text = "SplitContainerControl1"
        '
        'sccAlerts
        '
        Me.sccAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccAlerts.Location = New System.Drawing.Point(0, 0)
        Me.sccAlerts.Name = "sccAlerts"
        '
        'sccAlerts.Panel1
        '
        Me.sccAlerts.Panel1.Controls.Add(Me.GroupControl1)
        Me.sccAlerts.Panel1.MinSize = 300
        Me.sccAlerts.Panel1.Text = "Panel1"
        '
        'sccAlerts.Panel2
        '
        Me.sccAlerts.Panel2.Controls.Add(Me.GroupControl2)
        Me.sccAlerts.Panel2.MinSize = 500
        Me.sccAlerts.Panel2.Text = "Panel2"
        Me.sccAlerts.Size = New System.Drawing.Size(950, 514)
        Me.sccAlerts.SplitterPosition = 358
        Me.sccAlerts.TabIndex = 1
        Me.sccAlerts.Text = "SplitContainerControl2"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.tlpAlertMain)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(358, 514)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Alert"
        '
        'tlpAlertMain
        '
        Me.tlpAlertMain.ColumnCount = 1
        Me.tlpAlertMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAlertMain.Controls.Add(Me.grpAlertProperties, 0, 2)
        Me.tlpAlertMain.Controls.Add(Me.lstviewAlerts, 0, 1)
        Me.tlpAlertMain.Controls.Add(Me.TableLayoutPanel13, 0, 0)
        Me.tlpAlertMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAlertMain.Location = New System.Drawing.Point(2, 23)
        Me.tlpAlertMain.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpAlertMain.Name = "tlpAlertMain"
        Me.tlpAlertMain.RowCount = 3
        Me.tlpAlertMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpAlertMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAlertMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 280.0!))
        Me.tlpAlertMain.Size = New System.Drawing.Size(354, 489)
        Me.tlpAlertMain.TabIndex = 0
        '
        'grpAlertProperties
        '
        Me.grpAlertProperties.Controls.Add(Me.tlpAlertProperties)
        Me.grpAlertProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpAlertProperties.Location = New System.Drawing.Point(2, 211)
        Me.grpAlertProperties.Margin = New System.Windows.Forms.Padding(2)
        Me.grpAlertProperties.Name = "grpAlertProperties"
        Me.grpAlertProperties.Size = New System.Drawing.Size(350, 276)
        Me.grpAlertProperties.TabIndex = 3
        Me.grpAlertProperties.Text = "Alert Properties"
        '
        'tlpAlertProperties
        '
        Me.tlpAlertProperties.ColumnCount = 2
        Me.tlpAlertProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpAlertProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAlertProperties.Controls.Add(Me.lblMessage, 0, 8)
        Me.tlpAlertProperties.Controls.Add(Me.seAlertWindow, 1, 1)
        Me.tlpAlertProperties.Controls.Add(Me.LabelControl2, 0, 1)
        Me.tlpAlertProperties.Controls.Add(Me.TableLayoutPanel6, 0, 2)
        Me.tlpAlertProperties.Controls.Add(Me.TableLayoutPanel14, 0, 3)
        Me.tlpAlertProperties.Controls.Add(Me.LabelControl10, 0, 7)
        Me.tlpAlertProperties.Controls.Add(Me.lblAlertOwner, 1, 7)
        Me.tlpAlertProperties.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpAlertProperties.Controls.Add(Me.TableLayoutPanel5, 1, 0)
        Me.tlpAlertProperties.Controls.Add(Me.LabelControl17, 0, 5)
        Me.tlpAlertProperties.Controls.Add(Me.TableLayoutPanel20, 1, 5)
        Me.tlpAlertProperties.Controls.Add(Me.TableLayoutPanel19, 0, 6)
        Me.tlpAlertProperties.Controls.Add(Me.TableLayoutPanel3, 0, 4)
        Me.tlpAlertProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAlertProperties.Location = New System.Drawing.Point(2, 23)
        Me.tlpAlertProperties.Name = "tlpAlertProperties"
        Me.tlpAlertProperties.RowCount = 9
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertProperties.Size = New System.Drawing.Size(346, 251)
        Me.tlpAlertProperties.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.tlpAlertProperties.SetColumnSpan(Me.lblMessage, 2)
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 227)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(340, 22)
        Me.lblMessage.TabIndex = 19
        '
        'seAlertWindow
        '
        Me.seAlertWindow.Dock = System.Windows.Forms.DockStyle.Left
        Me.seAlertWindow.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seAlertWindow.Location = New System.Drawing.Point(103, 33)
        Me.seAlertWindow.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.seAlertWindow.Name = "seAlertWindow"
        Me.seAlertWindow.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seAlertWindow.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.seAlertWindow.Properties.IsFloatValue = False
        Me.seAlertWindow.Properties.Mask.EditMask = "N00"
        Me.seAlertWindow.Properties.MaxLength = 1
        Me.seAlertWindow.Properties.MaxValue = New Decimal(New Integer() {8, 0, 0, 0})
        Me.seAlertWindow.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seAlertWindow.Properties.UseReadOnlyAppearance = False
        Me.seAlertWindow.Size = New System.Drawing.Size(40, 20)
        Me.seAlertWindow.TabIndex = 8
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(94, 22)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Window"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.tlpAlertProperties.SetColumnSpan(Me.TableLayoutPanel6, 2)
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.ceEventEmail, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.txtEventEmail, 2, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(1, 57)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(344, 26)
        Me.TableLayoutPanel6.TabIndex = 10
        '
        'LabelControl8
        '
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "Event - Email"
        '
        'ceEventEmail
        '
        Me.ceEventEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceEventEmail.Location = New System.Drawing.Point(106, 3)
        Me.ceEventEmail.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.ceEventEmail.Name = "ceEventEmail"
        Me.ceEventEmail.Properties.Caption = ""
        Me.ceEventEmail.Size = New System.Drawing.Size(21, 20)
        Me.ceEventEmail.TabIndex = 1
        '
        'txtEventEmail
        '
        Me.txtEventEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEventEmail.Location = New System.Drawing.Point(133, 3)
        Me.txtEventEmail.Name = "txtEventEmail"
        Me.txtEventEmail.Size = New System.Drawing.Size(208, 20)
        Me.txtEventEmail.TabIndex = 2
        Me.txtEventEmail.ToolTip = "Add event email and hit enter to save."
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 3
        Me.tlpAlertProperties.SetColumnSpan(Me.TableLayoutPanel14, 2)
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Controls.Add(Me.LabelControl9, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.ceEventSNMP, 1, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.txtEventSNMP, 2, 0)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(1, 85)
        Me.TableLayoutPanel14.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 1
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(344, 26)
        Me.TableLayoutPanel14.TabIndex = 11
        '
        'LabelControl9
        '
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl9.TabIndex = 0
        Me.LabelControl9.Text = "Event - SNMP Trap"
        '
        'ceEventSNMP
        '
        Me.ceEventSNMP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceEventSNMP.Location = New System.Drawing.Point(106, 3)
        Me.ceEventSNMP.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.ceEventSNMP.Name = "ceEventSNMP"
        Me.ceEventSNMP.Properties.Caption = ""
        Me.ceEventSNMP.Size = New System.Drawing.Size(21, 20)
        Me.ceEventSNMP.TabIndex = 1
        '
        'txtEventSNMP
        '
        Me.txtEventSNMP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEventSNMP.Location = New System.Drawing.Point(133, 3)
        Me.txtEventSNMP.Name = "txtEventSNMP"
        Me.txtEventSNMP.Size = New System.Drawing.Size(208, 20)
        Me.txtEventSNMP.TabIndex = 2
        Me.txtEventSNMP.ToolTip = "Add event snmp description and hit enter to save."
        '
        'LabelControl10
        '
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(3, 199)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl10.Size = New System.Drawing.Size(94, 22)
        Me.LabelControl10.TabIndex = 12
        Me.LabelControl10.Text = "Alert Owner"
        '
        'lblAlertOwner
        '
        Me.lblAlertOwner.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.lblAlertOwner.Appearance.Options.UseForeColor = True
        Me.lblAlertOwner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAlertOwner.Location = New System.Drawing.Point(103, 199)
        Me.lblAlertOwner.Name = "lblAlertOwner"
        Me.lblAlertOwner.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblAlertOwner.Size = New System.Drawing.Size(240, 22)
        Me.lblAlertOwner.TabIndex = 13
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(94, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Occurence"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.seAlertOccurence, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.ceAlertEnabled, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(101, 1)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(244, 26)
        Me.TableLayoutPanel5.TabIndex = 14
        '
        'seAlertOccurence
        '
        Me.seAlertOccurence.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seAlertOccurence.Location = New System.Drawing.Point(3, 3)
        Me.seAlertOccurence.Name = "seAlertOccurence"
        Me.seAlertOccurence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seAlertOccurence.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.seAlertOccurence.Properties.IsFloatValue = False
        Me.seAlertOccurence.Properties.Mask.EditMask = "N00"
        Me.seAlertOccurence.Properties.MaxLength = 1
        Me.seAlertOccurence.Properties.MaxValue = New Decimal(New Integer() {8, 0, 0, 0})
        Me.seAlertOccurence.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seAlertOccurence.Properties.UseReadOnlyAppearance = False
        Me.seAlertOccurence.Size = New System.Drawing.Size(40, 20)
        Me.seAlertOccurence.TabIndex = 7
        '
        'ceAlertEnabled
        '
        Me.ceAlertEnabled.Dock = System.Windows.Forms.DockStyle.Right
        Me.ceAlertEnabled.EditValue = True
        Me.ceAlertEnabled.Location = New System.Drawing.Point(150, 3)
        Me.ceAlertEnabled.Name = "ceAlertEnabled"
        Me.ceAlertEnabled.Properties.Caption = "Alert Enabled"
        Me.ceAlertEnabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ceAlertEnabled.Size = New System.Drawing.Size(91, 20)
        Me.ceAlertEnabled.TabIndex = 8
        '
        'LabelControl17
        '
        Me.LabelControl17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl17.Location = New System.Drawing.Point(3, 143)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl17.Size = New System.Drawing.Size(94, 22)
        Me.LabelControl17.TabIndex = 16
        Me.LabelControl17.Text = "Dashboard Score"
        '
        'TableLayoutPanel20
        '
        Me.TableLayoutPanel20.ColumnCount = 2
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.Controls.Add(Me.ceDashboardScore, 0, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.txtDashboardScore, 1, 0)
        Me.TableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel20.Location = New System.Drawing.Point(101, 141)
        Me.TableLayoutPanel20.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel20.Name = "TableLayoutPanel20"
        Me.TableLayoutPanel20.RowCount = 1
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.Size = New System.Drawing.Size(244, 26)
        Me.TableLayoutPanel20.TabIndex = 17
        '
        'ceDashboardScore
        '
        Me.ceDashboardScore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceDashboardScore.Location = New System.Drawing.Point(3, 3)
        Me.ceDashboardScore.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.ceDashboardScore.Name = "ceDashboardScore"
        Me.ceDashboardScore.Properties.Caption = ""
        Me.ceDashboardScore.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ceDashboardScore.Size = New System.Drawing.Size(21, 20)
        Me.ceDashboardScore.TabIndex = 9
        '
        'txtDashboardScore
        '
        Me.txtDashboardScore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDashboardScore.Location = New System.Drawing.Point(33, 3)
        Me.txtDashboardScore.Name = "txtDashboardScore"
        Me.txtDashboardScore.Size = New System.Drawing.Size(208, 20)
        Me.txtDashboardScore.TabIndex = 10
        Me.txtDashboardScore.ToolTip = "Add dashboard score (numeric value only) and hit enter to save."
        '
        'TableLayoutPanel19
        '
        Me.TableLayoutPanel19.ColumnCount = 2
        Me.tlpAlertProperties.SetColumnSpan(Me.TableLayoutPanel19, 2)
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.Controls.Add(Me.LabelControl18, 0, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.cmbKPIFailureColumn, 1, 0)
        Me.TableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel19.Location = New System.Drawing.Point(0, 168)
        Me.TableLayoutPanel19.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel19.Name = "TableLayoutPanel19"
        Me.TableLayoutPanel19.RowCount = 1
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(346, 28)
        Me.TableLayoutPanel19.TabIndex = 18
        '
        'LabelControl18
        '
        Me.LabelControl18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl18.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl18.Size = New System.Drawing.Size(124, 22)
        Me.LabelControl18.TabIndex = 18
        Me.LabelControl18.Text = "KPI in Failure Column"
        '
        'cmbKPIFailureColumn
        '
        Me.cmbKPIFailureColumn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbKPIFailureColumn.Location = New System.Drawing.Point(133, 3)
        Me.cmbKPIFailureColumn.Name = "cmbKPIFailureColumn"
        Me.cmbKPIFailureColumn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbKPIFailureColumn.Size = New System.Drawing.Size(210, 20)
        Me.cmbKPIFailureColumn.TabIndex = 19
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.tlpAlertProperties.SetColumnSpan(Me.TableLayoutPanel3, 2)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.ceEventReport, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl28, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbEventReport, 2, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1, 113)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(344, 26)
        Me.TableLayoutPanel3.TabIndex = 20
        '
        'ceEventReport
        '
        Me.ceEventReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceEventReport.Location = New System.Drawing.Point(106, 3)
        Me.ceEventReport.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.ceEventReport.Name = "ceEventReport"
        Me.ceEventReport.Properties.Caption = ""
        Me.ceEventReport.Size = New System.Drawing.Size(21, 20)
        Me.ceEventReport.TabIndex = 2
        '
        'LabelControl28
        '
        Me.LabelControl28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl28.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl28.Name = "LabelControl28"
        Me.LabelControl28.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl28.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl28.TabIndex = 0
        Me.LabelControl28.Text = "Event - Report"
        '
        'cmbEventReport
        '
        Me.cmbEventReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbEventReport.Location = New System.Drawing.Point(133, 3)
        Me.cmbEventReport.Name = "cmbEventReport"
        Me.cmbEventReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEventReport.Size = New System.Drawing.Size(208, 20)
        Me.cmbEventReport.TabIndex = 3
        '
        'lstviewAlerts
        '
        Me.lstviewAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstviewAlerts.Location = New System.Drawing.Point(2, 32)
        Me.lstviewAlerts.Margin = New System.Windows.Forms.Padding(2)
        Me.lstviewAlerts.Name = "lstviewAlerts"
        Me.lstviewAlerts.OptionsBehavior.AllowExpandOnDblClick = False
        Me.lstviewAlerts.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.DoubleClick
        Me.lstviewAlerts.OptionsCustomization.AllowBandMoving = False
        Me.lstviewAlerts.OptionsCustomization.AllowBandResizing = False
        Me.lstviewAlerts.OptionsCustomization.AllowColumnMoving = False
        Me.lstviewAlerts.OptionsCustomization.AllowColumnResizing = False
        Me.lstviewAlerts.OptionsCustomization.AllowQuickHideColumns = False
        Me.lstviewAlerts.OptionsMenu.EnableColumnMenu = False
        Me.lstviewAlerts.OptionsMenu.EnableFooterMenu = False
        Me.lstviewAlerts.OptionsMenu.ShowAutoFilterRowItem = False
        Me.lstviewAlerts.OptionsNavigation.MoveOnEdit = False
        Me.lstviewAlerts.OptionsView.AutoWidth = False
        Me.lstviewAlerts.OptionsView.BestFitMode = DevExpress.XtraTreeList.TreeListBestFitMode.Full
        Me.lstviewAlerts.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.All
        Me.lstviewAlerts.OptionsView.ShowButtons = False
        Me.lstviewAlerts.OptionsView.ShowHorzLines = False
        Me.lstviewAlerts.OptionsView.ShowIndicator = False
        Me.lstviewAlerts.OptionsView.ShowRoot = False
        Me.lstviewAlerts.Size = New System.Drawing.Size(350, 175)
        Me.lstviewAlerts.TabIndex = 5
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 3
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.btnDeleteAlert, 2, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.txtAlertSearch, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.btnAddNewAlert, 1, 0)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 1
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(352, 28)
        Me.TableLayoutPanel13.TabIndex = 6
        '
        'btnDeleteAlert
        '
        Me.btnDeleteAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteAlert.Location = New System.Drawing.Point(304, 2)
        Me.btnDeleteAlert.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteAlert.Name = "btnDeleteAlert"
        Me.btnDeleteAlert.Size = New System.Drawing.Size(46, 24)
        Me.btnDeleteAlert.TabIndex = 1
        Me.btnDeleteAlert.Text = "Delete"
        '
        'txtAlertSearch
        '
        Me.txtAlertSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAlertSearch.Location = New System.Drawing.Point(2, 3)
        Me.txtAlertSearch.Margin = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.txtAlertSearch.Name = "txtAlertSearch"
        Me.txtAlertSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtAlertSearch.Properties.NullValuePrompt = "Search..."
        Me.txtAlertSearch.Size = New System.Drawing.Size(248, 20)
        Me.txtAlertSearch.TabIndex = 2
        '
        'btnAddNewAlert
        '
        Me.btnAddNewAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddNewAlert.Location = New System.Drawing.Point(254, 2)
        Me.btnAddNewAlert.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddNewAlert.Name = "btnAddNewAlert"
        Me.btnAddNewAlert.Size = New System.Drawing.Size(46, 24)
        Me.btnAddNewAlert.TabIndex = 0
        Me.btnAddNewAlert.Text = "Add"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel12)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(582, 514)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "KPI Rules"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 1
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel8, 0, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.gcKPIRules, 0, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 2
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 157.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(578, 489)
        Me.TableLayoutPanel12.TabIndex = 4
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.GroupControl7, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.GroupControl10, 0, 1)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 160)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(572, 326)
        Me.TableLayoutPanel8.TabIndex = 1
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.TableLayoutPanel9)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(568, 265)
        Me.GroupControl7.TabIndex = 1
        Me.GroupControl7.Text = "Properties"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.propGrid, 0, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(564, 240)
        Me.TableLayoutPanel9.TabIndex = 0
        '
        'propGrid
        '
        Me.propGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propGrid.LineColor = System.Drawing.SystemColors.ControlDark
        Me.propGrid.Location = New System.Drawing.Point(3, 3)
        Me.propGrid.Name = "propGrid"
        Me.propGrid.Size = New System.Drawing.Size(558, 234)
        Me.propGrid.TabIndex = 1
        Me.propGrid.ToolbarVisible = False
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Me.TableLayoutPanel18)
        Me.GroupControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl10.Location = New System.Drawing.Point(3, 272)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.Size = New System.Drawing.Size(566, 51)
        Me.GroupControl10.TabIndex = 2
        Me.GroupControl10.Text = "Test KPI Rule"
        '
        'TableLayoutPanel18
        '
        Me.TableLayoutPanel18.ColumnCount = 6
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel18.Controls.Add(Me.LabelControl15, 0, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.LabelControl16, 4, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.lblCountBreach, 5, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.deTestKPIRule, 1, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.btnTestKPI, 3, 0)
        Me.TableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel18.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        Me.TableLayoutPanel18.RowCount = 1
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel18.Size = New System.Drawing.Size(562, 26)
        Me.TableLayoutPanel18.TabIndex = 0
        '
        'LabelControl15
        '
        Me.LabelControl15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl15.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl15.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl15.TabIndex = 0
        Me.LabelControl15.Text = "Process Date"
        '
        'LabelControl16
        '
        Me.LabelControl16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl16.Location = New System.Drawing.Point(385, 3)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl16.Size = New System.Drawing.Size(124, 20)
        Me.LabelControl16.TabIndex = 1
        Me.LabelControl16.Text = "No. Of Objects Breached"
        '
        'lblCountBreach
        '
        Me.lblCountBreach.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCountBreach.Location = New System.Drawing.Point(515, 3)
        Me.lblCountBreach.Name = "lblCountBreach"
        Me.lblCountBreach.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblCountBreach.Size = New System.Drawing.Size(44, 20)
        Me.lblCountBreach.TabIndex = 2
        '
        'deTestKPIRule
        '
        Me.deTestKPIRule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deTestKPIRule.EditValue = New Date(2019, 1, 15, 0, 0, 0, 0)
        Me.deTestKPIRule.Location = New System.Drawing.Point(83, 4)
        Me.deTestKPIRule.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.deTestKPIRule.Name = "deTestKPIRule"
        Me.deTestKPIRule.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deTestKPIRule.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deTestKPIRule.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.deTestKPIRule.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deTestKPIRule.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.deTestKPIRule.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deTestKPIRule.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deTestKPIRule.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(45)
        Me.deTestKPIRule.Size = New System.Drawing.Size(94, 20)
        Me.deTestKPIRule.TabIndex = 3
        '
        'btnTestKPI
        '
        Me.btnTestKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTestKPI.Location = New System.Drawing.Point(264, 2)
        Me.btnTestKPI.Margin = New System.Windows.Forms.Padding(2)
        Me.btnTestKPI.Name = "btnTestKPI"
        Me.btnTestKPI.Size = New System.Drawing.Size(116, 22)
        Me.btnTestKPI.TabIndex = 4
        Me.btnTestKPI.Text = "Test KPI Rule"
        '
        'gcKPIRules
        '
        Me.gcKPIRules.AllowDrop = True
        Me.gcKPIRules.ContextMenuStrip = Me.cmsKPIRule
        Me.gcKPIRules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPIRules.Location = New System.Drawing.Point(3, 3)
        Me.gcKPIRules.MainView = Me.gvKPIRules
        Me.gcKPIRules.Name = "gcKPIRules"
        Me.gcKPIRules.Size = New System.Drawing.Size(572, 151)
        Me.gcKPIRules.TabIndex = 2
        Me.gcKPIRules.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKPIRules})
        '
        'cmsKPIRule
        '
        Me.cmsKPIRule.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteKPIRuleToolStripMenuItem, Me.ToolStripMenuItem1, Me.tsmi_KPI_Rules_Copy_All, Me.tsmi_KPI_Rules_Copy_SelectionWOHeader, Me.tsmi_KPI_Rules_Copy_SelectionWithHeader})
        Me.cmsKPIRule.Name = "cmsKPIRule"
        Me.cmsKPIRule.Size = New System.Drawing.Size(249, 98)
        '
        'DeleteKPIRuleToolStripMenuItem
        '
        Me.DeleteKPIRuleToolStripMenuItem.Name = "DeleteKPIRuleToolStripMenuItem"
        Me.DeleteKPIRuleToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.DeleteKPIRuleToolStripMenuItem.Text = "Delete KPI Rule"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(245, 6)
        '
        'tsmi_KPI_Rules_Copy_All
        '
        Me.tsmi_KPI_Rules_Copy_All.Name = "tsmi_KPI_Rules_Copy_All"
        Me.tsmi_KPI_Rules_Copy_All.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_KPI_Rules_Copy_All.Text = "Copy to Clipboard - All"
        '
        'tsmi_KPI_Rules_Copy_SelectionWOHeader
        '
        Me.tsmi_KPI_Rules_Copy_SelectionWOHeader.Name = "tsmi_KPI_Rules_Copy_SelectionWOHeader"
        Me.tsmi_KPI_Rules_Copy_SelectionWOHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_KPI_Rules_Copy_SelectionWOHeader.Text = "Copy - Selection Without Header"
        '
        'tsmi_KPI_Rules_Copy_SelectionWithHeader
        '
        Me.tsmi_KPI_Rules_Copy_SelectionWithHeader.Name = "tsmi_KPI_Rules_Copy_SelectionWithHeader"
        Me.tsmi_KPI_Rules_Copy_SelectionWithHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_KPI_Rules_Copy_SelectionWithHeader.Text = "Copy - Selection With Header"
        '
        'gvKPIRules
        '
        Me.gvKPIRules.GridControl = Me.gcKPIRules
        Me.gvKPIRules.Name = "gvKPIRules"
        Me.gvKPIRules.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPIRules.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPIRules.OptionsBehavior.Editable = False
        Me.gvKPIRules.OptionsBehavior.ReadOnly = True
        Me.gvKPIRules.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPIRules.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPIRules.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPIRules.OptionsView.ColumnAutoWidth = False
        Me.gvKPIRules.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupControl5, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(350, 514)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel11)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(346, 510)
        Me.GroupControl5.TabIndex = 0
        Me.GroupControl5.Text = "Search And Add KPI"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 1
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel10, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.GroupControl3, 0, 1)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 2
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(342, 485)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 3
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.btnAddKPI, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.btnDeleteKPI, 1, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.btnMethod, 2, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(342, 32)
        Me.TableLayoutPanel10.TabIndex = 0
        '
        'btnAddKPI
        '
        Me.btnAddKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddKPI.Location = New System.Drawing.Point(3, 3)
        Me.btnAddKPI.Name = "btnAddKPI"
        Me.btnAddKPI.Size = New System.Drawing.Size(106, 26)
        Me.btnAddKPI.TabIndex = 0
        Me.btnAddKPI.Text = "Add"
        '
        'btnDeleteKPI
        '
        Me.btnDeleteKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteKPI.Location = New System.Drawing.Point(115, 3)
        Me.btnDeleteKPI.Name = "btnDeleteKPI"
        Me.btnDeleteKPI.Size = New System.Drawing.Size(110, 26)
        Me.btnDeleteKPI.TabIndex = 1
        Me.btnDeleteKPI.Text = "Delete"
        '
        'btnMethod
        '
        Me.btnMethod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMethod.Location = New System.Drawing.Point(231, 3)
        Me.btnMethod.Name = "btnMethod"
        Me.btnMethod.Size = New System.Drawing.Size(108, 26)
        Me.btnMethod.TabIndex = 2
        Me.btnMethod.Text = "Method"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.tlpConfigDetails)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(3, 35)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(336, 447)
        Me.GroupControl3.TabIndex = 1
        Me.GroupControl3.Text = "Configuration Process"
        '
        'tlpConfigDetails
        '
        Me.tlpConfigDetails.ColumnCount = 2
        Me.tlpConfigDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.tlpConfigDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl22, 1, 3)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl21, 0, 3)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl20, 1, 2)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl19, 0, 2)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl13, 1, 1)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl12, 0, 1)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl11, 1, 0)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl7, 0, 0)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl23, 1, 4)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl24, 0, 5)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl25, 1, 5)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl26, 1, 6)
        Me.tlpConfigDetails.Controls.Add(Me.LabelControl27, 1, 7)
        Me.tlpConfigDetails.Controls.Add(Me.lblConfigProcess, 1, 8)
        Me.tlpConfigDetails.Controls.Add(Me.tlpKpiRulesFilter, 0, 9)
        Me.tlpConfigDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpConfigDetails.Location = New System.Drawing.Point(2, 23)
        Me.tlpConfigDetails.Name = "tlpConfigDetails"
        Me.tlpConfigDetails.RowCount = 10
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.tlpConfigDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpConfigDetails.Size = New System.Drawing.Size(332, 422)
        Me.tlpConfigDetails.TabIndex = 0
        '
        'LabelControl22
        '
        Me.LabelControl22.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl22.Appearance.Options.UseForeColor = True
        Me.LabelControl22.Appearance.Options.UseTextOptions = True
        Me.LabelControl22.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl22.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.LabelControl22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl22.Location = New System.Drawing.Point(53, 75)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl22.Size = New System.Drawing.Size(276, 14)
        Me.LabelControl22.TabIndex = 9
        Me.LabelControl22.Text = "Adjust Method Properties" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'LabelControl21
        '
        Me.LabelControl21.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl21.Appearance.Options.UseForeColor = True
        Me.LabelControl21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl21.Location = New System.Drawing.Point(3, 75)
        Me.LabelControl21.Name = "LabelControl21"
        Me.LabelControl21.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl21.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl21.TabIndex = 8
        Me.LabelControl21.Text = "Step 4:"
        '
        'LabelControl20
        '
        Me.LabelControl20.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl20.Appearance.Options.UseForeColor = True
        Me.LabelControl20.Appearance.Options.UseTextOptions = True
        Me.LabelControl20.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl20.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.LabelControl20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl20.Location = New System.Drawing.Point(53, 43)
        Me.LabelControl20.Name = "LabelControl20"
        Me.LabelControl20.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl20.Size = New System.Drawing.Size(276, 26)
        Me.LabelControl20.TabIndex = 7
        Me.LabelControl20.Text = "Visualize using Alert Test. Select an object and hit Load."
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl19.Appearance.Options.UseForeColor = True
        Me.LabelControl19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl19.Location = New System.Drawing.Point(3, 43)
        Me.LabelControl19.Name = "LabelControl19"
        Me.LabelControl19.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl19.Size = New System.Drawing.Size(44, 26)
        Me.LabelControl19.TabIndex = 6
        Me.LabelControl19.Text = "Step 3:"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl13.Appearance.Options.UseForeColor = True
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(53, 23)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(276, 14)
        Me.LabelControl13.TabIndex = 5
        Me.LabelControl13.Text = "Add KPI and set Method"
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl12.Appearance.Options.UseForeColor = True
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 23)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl12.TabIndex = 4
        Me.LabelControl12.Text = "Step 2:"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl11.Appearance.Options.UseForeColor = True
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(53, 3)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(276, 14)
        Me.LabelControl11.TabIndex = 3
        Me.LabelControl11.Text = "Add Alert"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl7.Appearance.Options.UseForeColor = True
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl7.TabIndex = 2
        Me.LabelControl7.Text = "Step 1:"
        '
        'LabelControl23
        '
        Me.LabelControl23.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl23.Appearance.Options.UseForeColor = True
        Me.LabelControl23.Appearance.Options.UseTextOptions = True
        Me.LabelControl23.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl23.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.LabelControl23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl23.Location = New System.Drawing.Point(53, 95)
        Me.LabelControl23.Name = "LabelControl23"
        Me.LabelControl23.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl23.Size = New System.Drawing.Size(276, 26)
        Me.LabelControl23.TabIndex = 10
        Me.LabelControl23.Text = "Loop through 3 and 4 until satisfied with number of triggers"
        '
        'LabelControl24
        '
        Me.LabelControl24.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl24.Appearance.Options.UseForeColor = True
        Me.LabelControl24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl24.Location = New System.Drawing.Point(3, 127)
        Me.LabelControl24.Name = "LabelControl24"
        Me.LabelControl24.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl24.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl24.TabIndex = 11
        Me.LabelControl24.Text = "Step 5:"
        '
        'LabelControl25
        '
        Me.LabelControl25.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl25.Appearance.Options.UseForeColor = True
        Me.LabelControl25.Appearance.Options.UseTextOptions = True
        Me.LabelControl25.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl25.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.LabelControl25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl25.Location = New System.Drawing.Point(53, 127)
        Me.LabelControl25.Name = "LabelControl25"
        Me.LabelControl25.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl25.Size = New System.Drawing.Size(276, 14)
        Me.LabelControl25.TabIndex = 12
        Me.LabelControl25.Text = "To know how many breaches of a KPI rule:"
        '
        'LabelControl26
        '
        Me.LabelControl26.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl26.Appearance.Options.UseForeColor = True
        Me.LabelControl26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl26.Location = New System.Drawing.Point(53, 147)
        Me.LabelControl26.Name = "LabelControl26"
        Me.LabelControl26.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl26.Size = New System.Drawing.Size(276, 14)
        Me.LabelControl26.TabIndex = 13
        Me.LabelControl26.Text = "Click Test KPI Rule for a certain day."
        '
        'LabelControl27
        '
        Me.LabelControl27.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl27.Appearance.Options.UseForeColor = True
        Me.LabelControl27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl27.Location = New System.Drawing.Point(53, 167)
        Me.LabelControl27.Name = "LabelControl27"
        Me.LabelControl27.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl27.Size = New System.Drawing.Size(276, 14)
        Me.LabelControl27.TabIndex = 14
        Me.LabelControl27.Text = "Tune Method properties accordingly."
        '
        'lblConfigProcess
        '
        Me.lblConfigProcess.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblConfigProcess.Appearance.Options.UseForeColor = True
        Me.lblConfigProcess.Appearance.Options.UseTextOptions = True
        Me.lblConfigProcess.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblConfigProcess.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblConfigProcess.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.lblConfigProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblConfigProcess.Location = New System.Drawing.Point(53, 187)
        Me.lblConfigProcess.Name = "lblConfigProcess"
        Me.lblConfigProcess.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConfigProcess.Size = New System.Drawing.Size(276, 56)
        Me.lblConfigProcess.TabIndex = 1
        Me.lblConfigProcess.Text = "To process a defined Alert, set the start date for which the Alert needs to be ca" &
    "lculate and hit Process button." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For every interval between the set date and now" &
    ", Alert will be calculated." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'tlpKpiRulesFilter
        '
        Me.tlpKpiRulesFilter.ColumnCount = 1
        Me.tlpConfigDetails.SetColumnSpan(Me.tlpKpiRulesFilter, 2)
        Me.tlpKpiRulesFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiRulesFilter.Controls.Add(Me.gcKpiRulesFilter, 0, 1)
        Me.tlpKpiRulesFilter.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.tlpKpiRulesFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKpiRulesFilter.Location = New System.Drawing.Point(3, 249)
        Me.tlpKpiRulesFilter.Name = "tlpKpiRulesFilter"
        Me.tlpKpiRulesFilter.RowCount = 2
        Me.tlpKpiRulesFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpKpiRulesFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiRulesFilter.Size = New System.Drawing.Size(326, 170)
        Me.tlpKpiRulesFilter.TabIndex = 15
        '
        'gcKpiRulesFilter
        '
        Me.gcKpiRulesFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKpiRulesFilter.Location = New System.Drawing.Point(3, 35)
        Me.gcKpiRulesFilter.MainView = Me.gvKpiRulesFilter
        Me.gcKpiRulesFilter.Name = "gcKpiRulesFilter"
        Me.gcKpiRulesFilter.Size = New System.Drawing.Size(320, 132)
        Me.gcKpiRulesFilter.TabIndex = 9
        Me.gcKpiRulesFilter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKpiRulesFilter})
        '
        'gvKpiRulesFilter
        '
        Me.gvKpiRulesFilter.GridControl = Me.gcKpiRulesFilter
        Me.gvKpiRulesFilter.Name = "gvKpiRulesFilter"
        Me.gvKpiRulesFilter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKpiRulesFilter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKpiRulesFilter.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKpiRulesFilter.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKpiRulesFilter.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKpiRulesFilter.OptionsView.ColumnAutoWidth = False
        Me.gvKpiRulesFilter.OptionsView.ShowAutoFilterRow = True
        Me.gvKpiRulesFilter.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.btnDeleteFilter, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnAddFilter, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnCopyFromFilter, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(326, 32)
        Me.TableLayoutPanel4.TabIndex = 8
        '
        'btnDeleteFilter
        '
        Me.btnDeleteFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteFilter.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnDeleteFilter.Location = New System.Drawing.Point(110, 3)
        Me.btnDeleteFilter.Name = "btnDeleteFilter"
        Me.btnDeleteFilter.Size = New System.Drawing.Size(104, 26)
        Me.btnDeleteFilter.TabIndex = 16
        Me.btnDeleteFilter.Text = "Delete Filter"
        '
        'btnAddFilter
        '
        Me.btnAddFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddFilter.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnAddFilter.Location = New System.Drawing.Point(3, 3)
        Me.btnAddFilter.Name = "btnAddFilter"
        Me.btnAddFilter.Size = New System.Drawing.Size(101, 26)
        Me.btnAddFilter.TabIndex = 15
        Me.btnAddFilter.Text = "Add Filter"
        '
        'btnCopyFromFilter
        '
        Me.btnCopyFromFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCopyFromFilter.Location = New System.Drawing.Point(220, 3)
        Me.btnCopyFromFilter.Name = "btnCopyFromFilter"
        Me.btnCopyFromFilter.Size = New System.Drawing.Size(103, 26)
        Me.btnCopyFromFilter.TabIndex = 18
        Me.btnCopyFromFilter.Text = "Copy From"
        '
        'sccChart
        '
        Me.sccChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccChart.Location = New System.Drawing.Point(0, 0)
        Me.sccChart.Name = "sccChart"
        '
        'sccChart.Panel1
        '
        Me.sccChart.Panel1.Controls.Add(Me.GroupControl4)
        Me.sccChart.Panel1.MinSize = 250
        Me.sccChart.Panel1.Text = "Panel1"
        '
        'sccChart.Panel2
        '
        Me.sccChart.Panel2.Controls.Add(Me.sccAlertChart)
        Me.sccChart.Panel2.MinSize = 750
        Me.sccChart.Panel2.Text = "Panel2"
        Me.sccChart.Size = New System.Drawing.Size(1310, 344)
        Me.sccChart.SplitterPosition = 250
        Me.sccChart.TabIndex = 7
        Me.sccChart.Text = "SplitContainerControl2"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.tlpAlertTest)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(250, 344)
        Me.GroupControl4.TabIndex = 1
        Me.GroupControl4.Text = "Alert Test"
        '
        'tlpAlertTest
        '
        Me.tlpAlertTest.ColumnCount = 3
        Me.tlpAlertTest.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpAlertTest.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAlertTest.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpAlertTest.Controls.Add(Me.seDataPoints, 1, 0)
        Me.tlpAlertTest.Controls.Add(Me.btnLoad, 2, 0)
        Me.tlpAlertTest.Controls.Add(Me.LabelControl4, 0, 0)
        Me.tlpAlertTest.Controls.Add(Me.LabelControl5, 0, 1)
        Me.tlpAlertTest.Controls.Add(Me.txtObjectNameFilter, 1, 1)
        Me.tlpAlertTest.Controls.Add(Me.GroupControl8, 0, 5)
        Me.tlpAlertTest.Controls.Add(Me.tglAlertTest, 0, 2)
        Me.tlpAlertTest.Controls.Add(Me.btnClearChart, 2, 2)
        Me.tlpAlertTest.Controls.Add(Me.ceShowHideBreached, 0, 3)
        Me.tlpAlertTest.Controls.Add(Me.ceShowHideOutlier, 0, 4)
        Me.tlpAlertTest.Controls.Add(Me.GroupControl9, 0, 6)
        Me.tlpAlertTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAlertTest.Location = New System.Drawing.Point(2, 23)
        Me.tlpAlertTest.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpAlertTest.Name = "tlpAlertTest"
        Me.tlpAlertTest.RowCount = 7
        Me.tlpAlertTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpAlertTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpAlertTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAlertTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpAlertTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpAlertTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpAlertTest.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.tlpAlertTest.Size = New System.Drawing.Size(246, 319)
        Me.tlpAlertTest.TabIndex = 1
        '
        'seDataPoints
        '
        Me.seDataPoints.Dock = System.Windows.Forms.DockStyle.Left
        Me.seDataPoints.EditValue = New Decimal(New Integer() {180, 0, 0, 0})
        Me.seDataPoints.Location = New System.Drawing.Point(83, 3)
        Me.seDataPoints.Name = "seDataPoints"
        Me.seDataPoints.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seDataPoints.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.seDataPoints.Properties.Mask.EditMask = "N00"
        Me.seDataPoints.Size = New System.Drawing.Size(60, 20)
        Me.seDataPoints.TabIndex = 4
        '
        'btnLoad
        '
        Me.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLoad.Location = New System.Drawing.Point(168, 2)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(76, 24)
        Me.btnLoad.TabIndex = 5
        Me.btnLoad.Text = "Load"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(74, 22)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "#Data Points"
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(74, 19)
        Me.LabelControl5.TabIndex = 7
        Me.LabelControl5.Text = "Objects"
        '
        'txtObjectNameFilter
        '
        Me.tlpAlertTest.SetColumnSpan(Me.txtObjectNameFilter, 2)
        Me.txtObjectNameFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtObjectNameFilter.Location = New System.Drawing.Point(82, 30)
        Me.txtObjectNameFilter.Margin = New System.Windows.Forms.Padding(2)
        Me.txtObjectNameFilter.Name = "txtObjectNameFilter"
        Me.txtObjectNameFilter.Size = New System.Drawing.Size(162, 20)
        Me.txtObjectNameFilter.TabIndex = 8
        '
        'GroupControl8
        '
        Me.tlpAlertTest.SetColumnSpan(Me.GroupControl8, 3)
        Me.GroupControl8.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(2, 196)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(242, 66)
        Me.GroupControl8.TabIndex = 9
        Me.GroupControl8.Text = "Tip"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl3, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl6, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(238, 41)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(234, 16)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "* Drag Alert to Chart to Test"
        '
        'LabelControl6
        '
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(2, 22)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(234, 17)
        Me.LabelControl6.TabIndex = 1
        Me.LabelControl6.Text = "* Drag KPI Rule to Chart to Test"
        '
        'tglAlertTest
        '
        Me.tglAlertTest.Dock = System.Windows.Forms.DockStyle.Top
        Me.tglAlertTest.Location = New System.Drawing.Point(3, 56)
        Me.tglAlertTest.LookAndFeel.SkinName = "McSkin"
        Me.tglAlertTest.LookAndFeel.UseDefaultLookAndFeel = False
        Me.tglAlertTest.Name = "tglAlertTest"
        Me.tglAlertTest.Size = New System.Drawing.Size(74, 23)
        Me.tglAlertTest.TabIndex = 10
        Me.tglAlertTest.Text = "Show Grid"
        Me.tglAlertTest.ToggleState = System.Windows.Forms.CheckState.Unchecked
        '
        'btnClearChart
        '
        Me.btnClearChart.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnClearChart.Location = New System.Drawing.Point(169, 56)
        Me.btnClearChart.Name = "btnClearChart"
        Me.btnClearChart.Size = New System.Drawing.Size(74, 23)
        Me.btnClearChart.TabIndex = 10
        Me.btnClearChart.Text = "Clear Chart"
        '
        'ceShowHideBreached
        '
        Me.tlpAlertTest.SetColumnSpan(Me.ceShowHideBreached, 2)
        Me.ceShowHideBreached.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceShowHideBreached.EditValue = True
        Me.ceShowHideBreached.Location = New System.Drawing.Point(5, 147)
        Me.ceShowHideBreached.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceShowHideBreached.Name = "ceShowHideBreached"
        Me.ceShowHideBreached.Properties.Caption = "Show KPI Breach"
        Me.ceShowHideBreached.Size = New System.Drawing.Size(158, 19)
        Me.ceShowHideBreached.TabIndex = 11
        '
        'ceShowHideOutlier
        '
        Me.tlpAlertTest.SetColumnSpan(Me.ceShowHideOutlier, 2)
        Me.ceShowHideOutlier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceShowHideOutlier.EditValue = True
        Me.ceShowHideOutlier.Location = New System.Drawing.Point(5, 172)
        Me.ceShowHideOutlier.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceShowHideOutlier.Name = "ceShowHideOutlier"
        Me.ceShowHideOutlier.Properties.Caption = "Show Outliers"
        Me.ceShowHideOutlier.Size = New System.Drawing.Size(158, 19)
        Me.ceShowHideOutlier.TabIndex = 12
        '
        'GroupControl9
        '
        Me.tlpAlertTest.SetColumnSpan(Me.GroupControl9, 3)
        Me.GroupControl9.Controls.Add(Me.TableLayoutPanel17)
        Me.GroupControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl9.Location = New System.Drawing.Point(2, 266)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(242, 51)
        Me.GroupControl9.TabIndex = 13
        Me.GroupControl9.Text = "Alert Process"
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.ColumnCount = 3
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel17.Controls.Add(Me.LabelControl14, 0, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.btnAlertProcess, 2, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.deAlertProcessDate, 1, 0)
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel17.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 1
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(238, 26)
        Me.TableLayoutPanel17.TabIndex = 0
        '
        'LabelControl14
        '
        Me.LabelControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl14.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl14.Size = New System.Drawing.Size(69, 20)
        Me.LabelControl14.TabIndex = 0
        Me.LabelControl14.Text = "Process Date"
        '
        'btnAlertProcess
        '
        Me.btnAlertProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAlertProcess.Location = New System.Drawing.Point(170, 2)
        Me.btnAlertProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAlertProcess.Name = "btnAlertProcess"
        Me.btnAlertProcess.Size = New System.Drawing.Size(66, 22)
        Me.btnAlertProcess.TabIndex = 2
        Me.btnAlertProcess.Text = "Process"
        '
        'deAlertProcessDate
        '
        Me.deAlertProcessDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deAlertProcessDate.EditValue = New Date(2019, 1, 13, 0, 0, 0, 0)
        Me.deAlertProcessDate.Location = New System.Drawing.Point(78, 3)
        Me.deAlertProcessDate.Name = "deAlertProcessDate"
        Me.deAlertProcessDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deAlertProcessDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deAlertProcessDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.deAlertProcessDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deAlertProcessDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.deAlertProcessDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deAlertProcessDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deAlertProcessDate.Size = New System.Drawing.Size(87, 20)
        Me.deAlertProcessDate.TabIndex = 3
        '
        'sccAlertChart
        '
        Me.sccAlertChart.Collapsed = True
        Me.sccAlertChart.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.sccAlertChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccAlertChart.Horizontal = False
        Me.sccAlertChart.Location = New System.Drawing.Point(0, 0)
        Me.sccAlertChart.Name = "sccAlertChart"
        '
        'sccAlertChart.Panel1
        '
        Me.sccAlertChart.Panel1.Controls.Add(Me.chAlert)
        Me.sccAlertChart.Panel1.Text = "Panel1"
        '
        'sccAlertChart.Panel2
        '
        Me.sccAlertChart.Panel2.Controls.Add(Me.gcChartAlert)
        Me.sccAlertChart.Panel2.MinSize = 100
        Me.sccAlertChart.Panel2.Text = "Panel2"
        Me.sccAlertChart.Size = New System.Drawing.Size(1050, 344)
        Me.sccAlertChart.SplitterPosition = 195
        Me.sccAlertChart.TabIndex = 7
        Me.sccAlertChart.Text = "SplitContainerControl3"
        '
        'chAlert
        '
        Me.chAlert.AllowDrop = True
        Me.chAlert.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Me.chAlert.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
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
        Annotation1.Padding = 2
        Annotation1.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Annotation1.Shadow.Depth = 1
        Annotation1.Shadow.ExpandBy = 2.0!
        Annotation1.Shadow.Visible = False
        Annotation1.Size = New System.Drawing.Size(1049, 333)
        Annotation1.Visible = True
        Me.chAlert.Box = Annotation1
        Me.chAlert.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.chAlert.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
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
        Me.chAlert.ChartArea.DefaultElement = Element1
        Me.chAlert.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.chAlert.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.chAlert.ChartArea.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.Label.Width = -2147483648
        Me.chAlert.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.chAlert.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.chAlert.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chAlert.ChartArea.LegendBox.DefaultEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chAlert.ChartArea.LegendBox.DefaultEntry.LabelStyle.Font = New System.Drawing.Font("Trebuchet MS", 8.0!)
        Me.chAlert.ChartArea.LegendBox.DefaultEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.LegendBox.DefaultEntry.LabelStyle.Width = -2147483648
        BoxHeaderOptions2.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions2.Label.Width = -2147483648
        BoxHeaderOptions2.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions2.Shadow.Color = System.Drawing.Color.Transparent
        Me.chAlert.ChartArea.LegendBox.Header = BoxHeaderOptions2
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.LabelStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.LabelStyle.Width = -2147483648
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.chAlert.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.chAlert.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chAlert.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.chAlert.ChartArea.LegendBox.Padding = 4
        Me.chAlert.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.chAlert.ChartArea.LegendBox.Shadow.ExpandBy = 2.0!
        Me.chAlert.ChartArea.LegendBox.Visible = True
        Me.chAlert.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.chAlert.ChartArea.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.chAlert.ChartArea.Shadow.Depth = 1
        Me.chAlert.ChartArea.Shadow.ExpandBy = 2.0!
        Me.chAlert.ChartArea.Shadow.Visible = False
        Me.chAlert.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.chAlert.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        BoxHeaderOptions3.Label.Offset = New System.Drawing.Point(0, 0)
        BoxHeaderOptions3.Label.Width = -2147483648
        BoxHeaderOptions3.Line.Color = System.Drawing.Color.Gray
        BoxHeaderOptions3.Shadow.Color = System.Drawing.Color.Transparent
        Me.chAlert.ChartArea.TitleBox.Header = BoxHeaderOptions3
        Me.chAlert.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chAlert.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.chAlert.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.chAlert.ChartArea.TitleBox.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.TitleBox.Label.Width = -2147483648
        Me.chAlert.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.chAlert.ChartArea.TitleBox.Shadow.ExpandBy = 2.0!
        Me.chAlert.ChartArea.TitleBox.Visible = True
        Me.chAlert.ChartArea.XAxis.Crosshair = Nothing
        Me.chAlert.ChartArea.XAxis.DefaultTick.AxisID = ""
        Me.chAlert.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.chAlert.ChartArea.XAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chAlert.ChartArea.XAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.XAxis.DefaultTick.Label.Width = -2147483648
        Me.chAlert.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.chAlert.ChartArea.XAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.XAxis.Label.Width = -2147483648
        Me.chAlert.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chAlert.ChartArea.XAxis.MinorTimeIntervalAdvanced.Unit = dotnetCHARTING.WinForms.TimeInterval.None
        Me.chAlert.ChartArea.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.chAlert.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chAlert.ChartArea.XAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.chAlert.ChartArea.XAxis.ZeroTick.AxisID = ""
        Me.chAlert.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chAlert.ChartArea.XAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chAlert.ChartArea.XAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.XAxis.ZeroTick.Label.Width = -2147483648
        Me.chAlert.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.chAlert.ChartArea.YAxis.Crosshair = Nothing
        Me.chAlert.ChartArea.YAxis.DefaultTick.AxisID = ""
        Me.chAlert.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.chAlert.ChartArea.YAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chAlert.ChartArea.YAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.YAxis.DefaultTick.Label.Width = -2147483648
        Me.chAlert.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.chAlert.ChartArea.YAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.YAxis.Label.Width = -2147483648
        Me.chAlert.ChartArea.YAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.chAlert.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.chAlert.ChartArea.YAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.chAlert.ChartArea.YAxis.ZeroTick.AxisID = ""
        Me.chAlert.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.chAlert.ChartArea.YAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.chAlert.ChartArea.YAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.ChartArea.YAxis.ZeroTick.Label.Width = -2147483648
        Me.chAlert.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.chAlert.DataGrid = Nothing
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
        Me.chAlert.DefaultElement = Element2
        Me.chAlert.DefaultShadow.ExpandBy = 2.0!
        Me.chAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chAlert.LegacyMode = False
        Me.chAlert.Location = New System.Drawing.Point(0, 0)
        Me.chAlert.Name = "chAlert"
        Me.chAlert.NoDataLabel.Offset = New System.Drawing.Point(0, 0)
        Me.chAlert.NoDataLabel.Width = -2147483648
        Me.chAlert.Size = New System.Drawing.Size(1050, 334)
        Me.chAlert.StartDateOfYear = New Date(CType(0, Long))
        Me.chAlert.TabIndex = 6
        Me.chAlert.TempDirectory = "C:\Users\Guy\AppData\Local\Temp\"
        Me.chAlert.View3D = View3D1
        '
        'gcChartAlert
        '
        Me.gcChartAlert.ContextMenuStrip = Me.cm_CopyGridData
        Me.gcChartAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcChartAlert.Location = New System.Drawing.Point(0, 0)
        Me.gcChartAlert.MainView = Me.gvChartAlert
        Me.gcChartAlert.Name = "gcChartAlert"
        Me.gcChartAlert.Size = New System.Drawing.Size(0, 0)
        Me.gcChartAlert.TabIndex = 4
        Me.gcChartAlert.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvChartAlert})
        '
        'cm_CopyGridData
        '
        Me.cm_CopyGridData.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RecordCount, Me.ToolStripSeparator11, Me.tsmi_Copy_All, Me.tsmi_Copy_SelectionWOHeader, Me.tsmi_Copy_SelectionWithHeader})
        Me.cm_CopyGridData.Name = "cm_GridViewMap"
        Me.cm_CopyGridData.Size = New System.Drawing.Size(249, 98)
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
        'gvChartAlert
        '
        Me.gvChartAlert.GridControl = Me.gcChartAlert
        Me.gvChartAlert.Name = "gvChartAlert"
        Me.gvChartAlert.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvChartAlert.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvChartAlert.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvChartAlert.OptionsBehavior.Editable = False
        Me.gvChartAlert.OptionsBehavior.ReadOnly = True
        Me.gvChartAlert.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvChartAlert.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvChartAlert.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvChartAlert.OptionsSelection.MultiSelect = True
        Me.gvChartAlert.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.gvChartAlert.OptionsView.ShowGroupPanel = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'frmAlertManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1310, 868)
        Me.Controls.Add(Me.sccMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmAlertManager.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1312, 900)
        Me.Name = "frmAlertManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Alert Manager"
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel1.ResumeLayout(False)
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel2.ResumeLayout(False)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.ResumeLayout(False)
        CType(Me.sccKpiRules.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccKpiRules.Panel1.ResumeLayout(False)
        CType(Me.sccKpiRules.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccKpiRules.Panel2.ResumeLayout(False)
        CType(Me.sccKpiRules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccKpiRules.ResumeLayout(False)
        CType(Me.sccAlerts.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAlerts.Panel1.ResumeLayout(False)
        CType(Me.sccAlerts.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAlerts.Panel2.ResumeLayout(False)
        CType(Me.sccAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAlerts.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.tlpAlertMain.ResumeLayout(False)
        CType(Me.grpAlertProperties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAlertProperties.ResumeLayout(False)
        Me.tlpAlertProperties.ResumeLayout(False)
        Me.tlpAlertProperties.PerformLayout()
        CType(Me.seAlertWindow.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.ceEventEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEventEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel14.ResumeLayout(False)
        Me.TableLayoutPanel14.PerformLayout()
        CType(Me.ceEventSNMP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEventSNMP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.seAlertOccurence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceAlertEnabled.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel20.ResumeLayout(False)
        CType(Me.ceDashboardScore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDashboardScore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel19.ResumeLayout(False)
        Me.TableLayoutPanel19.PerformLayout()
        CType(Me.cmbKPIFailureColumn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.ceEventReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEventReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstviewAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel13.ResumeLayout(False)
        CType(Me.txtAlertSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl10.ResumeLayout(False)
        Me.TableLayoutPanel18.ResumeLayout(False)
        Me.TableLayoutPanel18.PerformLayout()
        CType(Me.deTestKPIRule.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTestKPIRule.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcKPIRules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsKPIRule.ResumeLayout(False)
        CType(Me.gvKPIRules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.tlpConfigDetails.ResumeLayout(False)
        Me.tlpConfigDetails.PerformLayout()
        Me.tlpKpiRulesFilter.ResumeLayout(False)
        CType(Me.gcKpiRulesFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvKpiRulesFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.sccChart.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccChart.Panel1.ResumeLayout(False)
        CType(Me.sccChart.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccChart.Panel2.ResumeLayout(False)
        CType(Me.sccChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccChart.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.tlpAlertTest.ResumeLayout(False)
        Me.tlpAlertTest.PerformLayout()
        CType(Me.seDataPoints.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObjectNameFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.ceShowHideBreached.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowHideOutlier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.TableLayoutPanel17.PerformLayout()
        CType(Me.deAlertProcessDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deAlertProcessDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccAlertChart.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAlertChart.Panel1.ResumeLayout(False)
        CType(Me.sccAlertChart.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAlertChart.Panel2.ResumeLayout(False)
        CType(Me.sccAlertChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccAlertChart.ResumeLayout(False)
        CType(Me.chAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcChartAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_CopyGridData.ResumeLayout(False)
        CType(Me.gvChartAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sccMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccKpiRules As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents chAlert As dotnetCHARTING.WinForms.Chart
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpAlertMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpAlertProperties As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlpAlertTest As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtAlertSearch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnAddNewAlert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteAlert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpAlertProperties As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents seDataPoints As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents btnLoad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtObjectNameFilter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lstviewAlerts As DevExpress.XtraTreeList.TreeList
    Friend WithEvents seAlertOccurence As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents seAlertWindow As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents propGrid As System.Windows.Forms.PropertyGrid
    Friend WithEvents TableLayoutPanel13 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents sccChart As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccAlerts As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceEventEmail As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TableLayoutPanel14 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceEventSNMP As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtEventEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEventSNMP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmsKPIRule As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteKPIRuleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnClearChart As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tglAlertTest As IOS.Library.IOSToggleButton
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAlertOwner As DevExpress.XtraEditors.LabelControl
    Friend WithEvents sccAlertChart As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gcChartAlert As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvChartAlert As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcKPIRules As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKPIRules As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ceShowHideBreached As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceShowHideOutlier As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ceAlertEnabled As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel17 As TableLayoutPanel
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAlertProcess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents deAlertProcessDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents GroupControl10 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel18 As TableLayoutPanel
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCountBreach As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deTestKPIRule As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnTestKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmi_KPI_Rules_Copy_All As ToolStripMenuItem
    Friend WithEvents tsmi_KPI_Rules_Copy_SelectionWOHeader As ToolStripMenuItem
    Friend WithEvents cm_CopyGridData As ContextMenuStrip
    Friend WithEvents tsmi_RecordCount As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents tsmi_Copy_All As ToolStripMenuItem
    Friend WithEvents tsmi_Copy_SelectionWOHeader As ToolStripMenuItem
    Friend WithEvents ceDashboardScore As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDashboardScore As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbKPIFailureColumn As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TableLayoutPanel19 As TableLayoutPanel
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tsmi_KPI_Rules_Copy_SelectionWithHeader As ToolStripMenuItem
    Friend WithEvents tsmi_Copy_SelectionWithHeader As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents btnAddKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnMethod As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblConfigProcess As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpConfigDetails As TableLayoutPanel
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl24 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl25 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl26 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl27 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents ceEventReport As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl28 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbEventReport As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpKpiRulesFilter As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents btnDeleteFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCopyFromFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcKpiRulesFilter As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKpiRulesFilter As DevExpress.XtraGrid.Views.Grid.GridView
End Class
