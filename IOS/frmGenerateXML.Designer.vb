<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGenerateXML
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGenerateXML))
        Me.sccMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.sccSubTop = New DevExpress.XtraEditors.SplitContainerControl()
        Me.tlpXmlJobList = New System.Windows.Forms.TableLayoutPanel()
        Me.grpXmlJobs = New DevExpress.XtraEditors.GroupControl()
        Me.tlpXmlJobs = New System.Windows.Forms.TableLayoutPanel()
        Me.gcXmlJobs = New DevExpress.XtraGrid.GridControl()
        Me.cmXmlJobs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RenameXmlJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvXmlJobs = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpXmlJobButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnXmlJobsRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnXmlJobsAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnXmlJobsDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnXmlJobsInsert = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcOption2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnProvision = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRollbackProvision = New DevExpress.XtraEditors.SimpleButton()
        Me.btnKillProvision = New DevExpress.XtraEditors.SimpleButton()
        Me.gcOption1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSaveXml = New DevExpress.XtraEditors.SimpleButton()
        Me.gcStep1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnValidate = New DevExpress.XtraEditors.SimpleButton()
        Me.grpPartialProvision = New DevExpress.XtraEditors.GroupControl()
        Me.tlpPartialProvision = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCreateValidate = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCreateValidateRollback = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExecute = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExecuteRollback = New DevExpress.XtraEditors.SimpleButton()
        Me.xtcSubTop = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpValidation = New DevExpress.XtraTab.XtraTabPage()
        Me.gcValidation = New DevExpress.XtraGrid.GridControl()
        Me.gvValidation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.xtpInputData = New DevExpress.XtraTab.XtraTabPage()
        Me.gcInputData = New DevExpress.XtraGrid.GridControl()
        Me.cmInputDataGrid = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_PasteDataFromClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_DeleteSelectedRows = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvInputData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtpOutputData = New DevExpress.XtraTab.XtraTabPage()
        Me.gcOutputData = New DevExpress.XtraGrid.GridControl()
        Me.gvOutputData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtpErrors = New DevExpress.XtraTab.XtraTabPage()
        Me.gcErrors = New DevExpress.XtraGrid.GridControl()
        Me.gvErrors = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtpProvisionResult = New DevExpress.XtraTab.XtraTabPage()
        Me.gcProvisionResult = New DevExpress.XtraGrid.GridControl()
        Me.gvProvisionResult = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpLogMsgs = New DevExpress.XtraEditors.GroupControl()
        Me.gcXmlLogMsgs = New DevExpress.XtraGrid.GridControl()
        Me.gvXmlLogMsgs = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.bgWorker = New System.ComponentModel.BackgroundWorker()
        Me.bgWorkerRollBack = New System.ComponentModel.BackgroundWorker()
        Me.bgWorkerPartial = New System.ComponentModel.BackgroundWorker()
        Me.bgWorkerPartialRollback = New System.ComponentModel.BackgroundWorker()
        Me.bgWorkerExecute = New System.ComponentModel.BackgroundWorker()
        Me.bgWorkerExecuteRollback = New System.ComponentModel.BackgroundWorker()
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel1.SuspendLayout()
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel2.SuspendLayout()
        Me.sccMain.SuspendLayout()
        CType(Me.sccSubTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccSubTop.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccSubTop.Panel1.SuspendLayout()
        CType(Me.sccSubTop.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccSubTop.Panel2.SuspendLayout()
        Me.sccSubTop.SuspendLayout()
        Me.tlpXmlJobList.SuspendLayout()
        CType(Me.grpXmlJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpXmlJobs.SuspendLayout()
        Me.tlpXmlJobs.SuspendLayout()
        CType(Me.gcXmlJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmXmlJobs.SuspendLayout()
        CType(Me.gvXmlJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpXmlJobButtons.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.gcOption2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcOption2.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.gcOption1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcOption1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcStep1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcStep1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grpPartialProvision, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPartialProvision.SuspendLayout()
        Me.tlpPartialProvision.SuspendLayout()
        CType(Me.xtcSubTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcSubTop.SuspendLayout()
        Me.xtpValidation.SuspendLayout()
        CType(Me.gcValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpInputData.SuspendLayout()
        CType(Me.gcInputData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmInputDataGrid.SuspendLayout()
        CType(Me.gvInputData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpOutputData.SuspendLayout()
        CType(Me.gcOutputData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOutputData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpErrors.SuspendLayout()
        CType(Me.gcErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpProvisionResult.SuspendLayout()
        CType(Me.gcProvisionResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProvisionResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLogMsgs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLogMsgs.SuspendLayout()
        CType(Me.gcXmlLogMsgs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvXmlLogMsgs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.sccMain.Panel1.Controls.Add(Me.sccSubTop)
        Me.sccMain.Panel1.MinSize = 400
        Me.sccMain.Panel1.Text = "Panel1"
        '
        'sccMain.Panel2
        '
        Me.sccMain.Panel2.Controls.Add(Me.grpLogMsgs)
        Me.sccMain.Panel2.MinSize = 300
        Me.sccMain.Panel2.Text = "Panel2"
        Me.sccMain.Size = New System.Drawing.Size(1302, 868)
        Me.sccMain.SplitterPosition = 473
        Me.sccMain.TabIndex = 0
        '
        'sccSubTop
        '
        Me.sccSubTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccSubTop.Location = New System.Drawing.Point(0, 0)
        Me.sccSubTop.Name = "sccSubTop"
        '
        'sccSubTop.Panel1
        '
        Me.sccSubTop.Panel1.Controls.Add(Me.tlpXmlJobList)
        Me.sccSubTop.Panel1.MinSize = 900
        Me.sccSubTop.Panel1.Text = "Panel1"
        '
        'sccSubTop.Panel2
        '
        Me.sccSubTop.Panel2.Controls.Add(Me.xtcSubTop)
        Me.sccSubTop.Panel2.MinSize = 400
        Me.sccSubTop.Panel2.Text = "Panel2"
        Me.sccSubTop.Size = New System.Drawing.Size(1302, 473)
        Me.sccSubTop.SplitterPosition = 788
        Me.sccSubTop.TabIndex = 0
        '
        'tlpXmlJobList
        '
        Me.tlpXmlJobList.ColumnCount = 1
        Me.tlpXmlJobList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpXmlJobList.Controls.Add(Me.grpXmlJobs, 0, 0)
        Me.tlpXmlJobList.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.tlpXmlJobList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpXmlJobList.Location = New System.Drawing.Point(0, 0)
        Me.tlpXmlJobList.Name = "tlpXmlJobList"
        Me.tlpXmlJobList.RowCount = 2
        Me.tlpXmlJobList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpXmlJobList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpXmlJobList.Size = New System.Drawing.Size(900, 473)
        Me.tlpXmlJobList.TabIndex = 0
        '
        'grpXmlJobs
        '
        Me.grpXmlJobs.Controls.Add(Me.tlpXmlJobs)
        Me.grpXmlJobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpXmlJobs.Location = New System.Drawing.Point(3, 3)
        Me.grpXmlJobs.Name = "grpXmlJobs"
        Me.grpXmlJobs.Size = New System.Drawing.Size(894, 397)
        Me.grpXmlJobs.TabIndex = 3
        Me.grpXmlJobs.Text = "XML Jobs List"
        '
        'tlpXmlJobs
        '
        Me.tlpXmlJobs.ColumnCount = 1
        Me.tlpXmlJobs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpXmlJobs.Controls.Add(Me.gcXmlJobs, 0, 1)
        Me.tlpXmlJobs.Controls.Add(Me.tlpXmlJobButtons, 0, 0)
        Me.tlpXmlJobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpXmlJobs.Location = New System.Drawing.Point(2, 23)
        Me.tlpXmlJobs.Name = "tlpXmlJobs"
        Me.tlpXmlJobs.RowCount = 2
        Me.tlpXmlJobs.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpXmlJobs.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpXmlJobs.Size = New System.Drawing.Size(890, 372)
        Me.tlpXmlJobs.TabIndex = 6
        '
        'gcXmlJobs
        '
        Me.gcXmlJobs.AllowDrop = True
        Me.gcXmlJobs.ContextMenuStrip = Me.cmXmlJobs
        Me.gcXmlJobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcXmlJobs.Location = New System.Drawing.Point(2, 32)
        Me.gcXmlJobs.MainView = Me.gvXmlJobs
        Me.gcXmlJobs.Margin = New System.Windows.Forms.Padding(2)
        Me.gcXmlJobs.Name = "gcXmlJobs"
        Me.gcXmlJobs.Size = New System.Drawing.Size(886, 338)
        Me.gcXmlJobs.TabIndex = 5
        Me.gcXmlJobs.Tag = "TM_Bulk"
        Me.gcXmlJobs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvXmlJobs, Me.GridView2})
        '
        'cmXmlJobs
        '
        Me.cmXmlJobs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RenameXmlJob})
        Me.cmXmlJobs.Name = "cmsConfigurationSummary"
        Me.cmXmlJobs.Size = New System.Drawing.Size(166, 26)
        '
        'tsmi_RenameXmlJob
        '
        Me.tsmi_RenameXmlJob.Name = "tsmi_RenameXmlJob"
        Me.tsmi_RenameXmlJob.Size = New System.Drawing.Size(165, 22)
        Me.tsmi_RenameXmlJob.Text = "Rename XML Job"
        '
        'gvXmlJobs
        '
        Me.gvXmlJobs.ActiveFilterEnabled = False
        Me.gvXmlJobs.GridControl = Me.gcXmlJobs
        Me.gvXmlJobs.Name = "gvXmlJobs"
        Me.gvXmlJobs.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXmlJobs.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXmlJobs.OptionsBehavior.Editable = False
        Me.gvXmlJobs.OptionsBehavior.ReadOnly = True
        Me.gvXmlJobs.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlJobs.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlJobs.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlJobs.OptionsCustomization.AllowSort = False
        Me.gvXmlJobs.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvXmlJobs.OptionsSelection.MultiSelect = True
        Me.gvXmlJobs.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gcXmlJobs
        Me.GridView2.Name = "GridView2"
        '
        'tlpXmlJobButtons
        '
        Me.tlpXmlJobButtons.ColumnCount = 5
        Me.tlpXmlJobButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpXmlJobButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpXmlJobButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpXmlJobButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpXmlJobButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpXmlJobButtons.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpXmlJobButtons.Controls.Add(Me.btnXmlJobsRefresh, 3, 0)
        Me.tlpXmlJobButtons.Controls.Add(Me.btnXmlJobsAdd, 1, 0)
        Me.tlpXmlJobButtons.Controls.Add(Me.btnXmlJobsDelete, 2, 0)
        Me.tlpXmlJobButtons.Controls.Add(Me.btnXmlJobsInsert, 4, 0)
        Me.tlpXmlJobButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpXmlJobButtons.Location = New System.Drawing.Point(0, 0)
        Me.tlpXmlJobButtons.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpXmlJobButtons.Name = "tlpXmlJobButtons"
        Me.tlpXmlJobButtons.RowCount = 1
        Me.tlpXmlJobButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpXmlJobButtons.Size = New System.Drawing.Size(890, 30)
        Me.tlpXmlJobButtons.TabIndex = 6
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(564, 24)
        Me.lblMessage.TabIndex = 20
        '
        'btnXmlJobsRefresh
        '
        Me.btnXmlJobsRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnXmlJobsRefresh.Location = New System.Drawing.Point(732, 2)
        Me.btnXmlJobsRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnXmlJobsRefresh.Name = "btnXmlJobsRefresh"
        Me.btnXmlJobsRefresh.Size = New System.Drawing.Size(76, 26)
        Me.btnXmlJobsRefresh.TabIndex = 8
        Me.btnXmlJobsRefresh.Text = "Refresh"
        '
        'btnXmlJobsAdd
        '
        Me.btnXmlJobsAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnXmlJobsAdd.Location = New System.Drawing.Point(572, 2)
        Me.btnXmlJobsAdd.Margin = New System.Windows.Forms.Padding(2)
        Me.btnXmlJobsAdd.Name = "btnXmlJobsAdd"
        Me.btnXmlJobsAdd.Size = New System.Drawing.Size(76, 26)
        Me.btnXmlJobsAdd.TabIndex = 9
        Me.btnXmlJobsAdd.Text = "Add"
        '
        'btnXmlJobsDelete
        '
        Me.btnXmlJobsDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnXmlJobsDelete.Location = New System.Drawing.Point(652, 2)
        Me.btnXmlJobsDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnXmlJobsDelete.Name = "btnXmlJobsDelete"
        Me.btnXmlJobsDelete.Size = New System.Drawing.Size(76, 26)
        Me.btnXmlJobsDelete.TabIndex = 10
        Me.btnXmlJobsDelete.Text = "Delete"
        '
        'btnXmlJobsInsert
        '
        Me.btnXmlJobsInsert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnXmlJobsInsert.Location = New System.Drawing.Point(812, 2)
        Me.btnXmlJobsInsert.Margin = New System.Windows.Forms.Padding(2)
        Me.btnXmlJobsInsert.Name = "btnXmlJobsInsert"
        Me.btnXmlJobsInsert.Size = New System.Drawing.Size(76, 26)
        Me.btnXmlJobsInsert.TabIndex = 21
        Me.btnXmlJobsInsert.Text = "Insert"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 4
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.gcOption2, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.gcOption1, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.gcStep1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.grpPartialProvision, 3, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(1, 404)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(898, 68)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'gcOption2
        '
        Me.gcOption2.Controls.Add(Me.TableLayoutPanel5)
        Me.gcOption2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcOption2.Location = New System.Drawing.Point(219, 3)
        Me.gcOption2.Name = "gcOption2"
        Me.gcOption2.Size = New System.Drawing.Size(214, 62)
        Me.gcOption2.TabIndex = 10
        Me.gcOption2.Text = "Option 2: Direct Provision"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.btnProvision, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.btnRollbackProvision, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.btnKillProvision, 2, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(210, 37)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'btnProvision
        '
        Me.btnProvision.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProvision.Location = New System.Drawing.Point(2, 2)
        Me.btnProvision.Margin = New System.Windows.Forms.Padding(2)
        Me.btnProvision.Name = "btnProvision"
        Me.btnProvision.Size = New System.Drawing.Size(65, 33)
        Me.btnProvision.TabIndex = 5
        Me.btnProvision.Text = "Provision"
        '
        'btnRollbackProvision
        '
        Me.btnRollbackProvision.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRollbackProvision.Location = New System.Drawing.Point(71, 2)
        Me.btnRollbackProvision.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRollbackProvision.Name = "btnRollbackProvision"
        Me.btnRollbackProvision.Size = New System.Drawing.Size(67, 33)
        Me.btnRollbackProvision.TabIndex = 7
        Me.btnRollbackProvision.Text = "RollBack"
        '
        'btnKillProvision
        '
        Me.btnKillProvision.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnKillProvision.Location = New System.Drawing.Point(143, 3)
        Me.btnKillProvision.Name = "btnKillProvision"
        Me.btnKillProvision.Size = New System.Drawing.Size(64, 31)
        Me.btnKillProvision.TabIndex = 21
        Me.btnKillProvision.Text = "Kill"
        '
        'gcOption1
        '
        Me.gcOption1.Controls.Add(Me.TableLayoutPanel3)
        Me.gcOption1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcOption1.Location = New System.Drawing.Point(109, 3)
        Me.gcOption1.Name = "gcOption1"
        Me.gcOption1.Size = New System.Drawing.Size(104, 62)
        Me.gcOption1.TabIndex = 9
        Me.gcOption1.Text = "Option 1: Manual"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnSaveXml, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(100, 37)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'btnSaveXml
        '
        Me.btnSaveXml.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSaveXml.Location = New System.Drawing.Point(2, 2)
        Me.btnSaveXml.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSaveXml.Name = "btnSaveXml"
        Me.btnSaveXml.Size = New System.Drawing.Size(96, 33)
        Me.btnSaveXml.TabIndex = 4
        Me.btnSaveXml.Text = "Save XML"
        '
        'gcStep1
        '
        Me.gcStep1.Controls.Add(Me.TableLayoutPanel1)
        Me.gcStep1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcStep1.Location = New System.Drawing.Point(3, 3)
        Me.gcStep1.Name = "gcStep1"
        Me.gcStep1.Size = New System.Drawing.Size(100, 62)
        Me.gcStep1.TabIndex = 8
        Me.gcStep1.Text = "Step 1: Validate"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnValidate, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(96, 37)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btnValidate
        '
        Me.btnValidate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnValidate.Location = New System.Drawing.Point(2, 2)
        Me.btnValidate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(92, 33)
        Me.btnValidate.TabIndex = 7
        Me.btnValidate.Text = "Validate"
        '
        'grpPartialProvision
        '
        Me.grpPartialProvision.Controls.Add(Me.tlpPartialProvision)
        Me.grpPartialProvision.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPartialProvision.Location = New System.Drawing.Point(439, 3)
        Me.grpPartialProvision.Name = "grpPartialProvision"
        Me.grpPartialProvision.Size = New System.Drawing.Size(456, 62)
        Me.grpPartialProvision.TabIndex = 11
        Me.grpPartialProvision.Text = "Option 3: Partial Provision"
        '
        'tlpPartialProvision
        '
        Me.tlpPartialProvision.ColumnCount = 4
        Me.tlpPartialProvision.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.0!))
        Me.tlpPartialProvision.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.tlpPartialProvision.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tlpPartialProvision.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.0!))
        Me.tlpPartialProvision.Controls.Add(Me.btnCreateValidate, 0, 0)
        Me.tlpPartialProvision.Controls.Add(Me.btnCreateValidateRollback, 1, 0)
        Me.tlpPartialProvision.Controls.Add(Me.btnExecute, 2, 0)
        Me.tlpPartialProvision.Controls.Add(Me.btnExecuteRollback, 3, 0)
        Me.tlpPartialProvision.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpPartialProvision.Location = New System.Drawing.Point(2, 23)
        Me.tlpPartialProvision.Name = "tlpPartialProvision"
        Me.tlpPartialProvision.RowCount = 1
        Me.tlpPartialProvision.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPartialProvision.Size = New System.Drawing.Size(452, 37)
        Me.tlpPartialProvision.TabIndex = 1
        '
        'btnCreateValidate
        '
        Me.btnCreateValidate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCreateValidate.Location = New System.Drawing.Point(2, 2)
        Me.btnCreateValidate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCreateValidate.Name = "btnCreateValidate"
        Me.btnCreateValidate.Size = New System.Drawing.Size(99, 33)
        Me.btnCreateValidate.TabIndex = 5
        Me.btnCreateValidate.Text = "Create + Validate"
        '
        'btnCreateValidateRollback
        '
        Me.btnCreateValidateRollback.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCreateValidateRollback.Location = New System.Drawing.Point(106, 3)
        Me.btnCreateValidateRollback.Name = "btnCreateValidateRollback"
        Me.btnCreateValidateRollback.Size = New System.Drawing.Size(152, 31)
        Me.btnCreateValidateRollback.TabIndex = 21
        Me.btnCreateValidateRollback.Text = "Create + Validate (Rollback)"
        '
        'btnExecute
        '
        Me.btnExecute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExecute.Location = New System.Drawing.Point(263, 2)
        Me.btnExecute.Margin = New System.Windows.Forms.Padding(2)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(86, 33)
        Me.btnExecute.TabIndex = 7
        Me.btnExecute.Text = "Execute"
        '
        'btnExecuteRollback
        '
        Me.btnExecuteRollback.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExecuteRollback.Location = New System.Drawing.Point(354, 3)
        Me.btnExecuteRollback.Name = "btnExecuteRollback"
        Me.btnExecuteRollback.Size = New System.Drawing.Size(95, 31)
        Me.btnExecuteRollback.TabIndex = 22
        Me.btnExecuteRollback.Text = "Execute Rollback"
        Me.btnExecuteRollback.Visible = False
        '
        'xtcSubTop
        '
        Me.xtcSubTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcSubTop.Location = New System.Drawing.Point(0, 0)
        Me.xtcSubTop.Name = "xtcSubTop"
        Me.xtcSubTop.SelectedTabPage = Me.xtpValidation
        Me.xtcSubTop.Size = New System.Drawing.Size(392, 473)
        Me.xtcSubTop.TabIndex = 0
        Me.xtcSubTop.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpValidation, Me.xtpInputData, Me.xtpOutputData, Me.xtpErrors, Me.xtpProvisionResult})
        '
        'xtpValidation
        '
        Me.xtpValidation.Controls.Add(Me.gcValidation)
        Me.xtpValidation.Name = "xtpValidation"
        Me.xtpValidation.Size = New System.Drawing.Size(390, 448)
        Me.xtpValidation.Text = "Validation"
        '
        'gcValidation
        '
        Me.gcValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcValidation.Location = New System.Drawing.Point(0, 0)
        Me.gcValidation.MainView = Me.gvValidation
        Me.gcValidation.Name = "gcValidation"
        Me.gcValidation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit1})
        Me.gcValidation.Size = New System.Drawing.Size(390, 448)
        Me.gcValidation.TabIndex = 11
        Me.gcValidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvValidation})
        '
        'gvValidation
        '
        Me.gvValidation.GridControl = Me.gcValidation
        Me.gvValidation.Name = "gvValidation"
        Me.gvValidation.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidation.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValidation.OptionsBehavior.Editable = False
        Me.gvValidation.OptionsBehavior.ReadOnly = True
        Me.gvValidation.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidation.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidation.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValidation.OptionsCustomization.AllowSort = False
        Me.gvValidation.OptionsView.ColumnAutoWidth = False
        Me.gvValidation.OptionsView.ShowGroupPanel = False
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)})
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        Me.RepositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'xtpInputData
        '
        Me.xtpInputData.Controls.Add(Me.gcInputData)
        Me.xtpInputData.Name = "xtpInputData"
        Me.xtpInputData.Size = New System.Drawing.Size(390, 448)
        Me.xtpInputData.Text = "Input Data"
        '
        'gcInputData
        '
        Me.gcInputData.ContextMenuStrip = Me.cmInputDataGrid
        Me.gcInputData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcInputData.Location = New System.Drawing.Point(0, 0)
        Me.gcInputData.MainView = Me.gvInputData
        Me.gcInputData.Name = "gcInputData"
        Me.gcInputData.Size = New System.Drawing.Size(390, 448)
        Me.gcInputData.TabIndex = 12
        Me.gcInputData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvInputData})
        '
        'cmInputDataGrid
        '
        Me.cmInputDataGrid.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmInputDataGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_PasteDataFromClipboard, Me.tsmi_DeleteSelectedRows})
        Me.cmInputDataGrid.Name = "cmBulkPaste"
        Me.cmInputDataGrid.Size = New System.Drawing.Size(189, 48)
        '
        'tsmi_PasteDataFromClipboard
        '
        Me.tsmi_PasteDataFromClipboard.Name = "tsmi_PasteDataFromClipboard"
        Me.tsmi_PasteDataFromClipboard.Size = New System.Drawing.Size(188, 22)
        Me.tsmi_PasteDataFromClipboard.Text = "Paste From Clipboard"
        '
        'tsmi_DeleteSelectedRows
        '
        Me.tsmi_DeleteSelectedRows.Name = "tsmi_DeleteSelectedRows"
        Me.tsmi_DeleteSelectedRows.Size = New System.Drawing.Size(188, 22)
        Me.tsmi_DeleteSelectedRows.Text = "Delete Selected Rows"
        '
        'gvInputData
        '
        Me.gvInputData.ActiveFilterEnabled = False
        Me.gvInputData.GridControl = Me.gcInputData
        Me.gvInputData.Name = "gvInputData"
        Me.gvInputData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvInputData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvInputData.OptionsBehavior.Editable = False
        Me.gvInputData.OptionsBehavior.ReadOnly = True
        Me.gvInputData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvInputData.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvInputData.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvInputData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvInputData.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append
        Me.gvInputData.OptionsCustomization.AllowSort = False
        Me.gvInputData.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvInputData.OptionsSelection.MultiSelect = True
        Me.gvInputData.OptionsView.ColumnAutoWidth = False
        Me.gvInputData.OptionsView.ShowGroupPanel = False
        '
        'xtpOutputData
        '
        Me.xtpOutputData.Controls.Add(Me.gcOutputData)
        Me.xtpOutputData.Name = "xtpOutputData"
        Me.xtpOutputData.Size = New System.Drawing.Size(390, 448)
        Me.xtpOutputData.Text = "Output Data"
        '
        'gcOutputData
        '
        Me.gcOutputData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcOutputData.Location = New System.Drawing.Point(0, 0)
        Me.gcOutputData.MainView = Me.gvOutputData
        Me.gcOutputData.Name = "gcOutputData"
        Me.gcOutputData.Size = New System.Drawing.Size(390, 448)
        Me.gcOutputData.TabIndex = 12
        Me.gcOutputData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvOutputData})
        '
        'gvOutputData
        '
        Me.gvOutputData.GridControl = Me.gcOutputData
        Me.gvOutputData.Name = "gvOutputData"
        Me.gvOutputData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvOutputData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvOutputData.OptionsBehavior.Editable = False
        Me.gvOutputData.OptionsBehavior.ReadOnly = True
        Me.gvOutputData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputData.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvOutputData.OptionsCustomization.AllowSort = False
        Me.gvOutputData.OptionsView.ColumnAutoWidth = False
        Me.gvOutputData.OptionsView.ShowGroupPanel = False
        '
        'xtpErrors
        '
        Me.xtpErrors.Controls.Add(Me.gcErrors)
        Me.xtpErrors.Name = "xtpErrors"
        Me.xtpErrors.Size = New System.Drawing.Size(390, 448)
        Me.xtpErrors.Text = "Errors"
        '
        'gcErrors
        '
        Me.gcErrors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcErrors.Location = New System.Drawing.Point(0, 0)
        Me.gcErrors.MainView = Me.gvErrors
        Me.gcErrors.Name = "gcErrors"
        Me.gcErrors.Size = New System.Drawing.Size(390, 448)
        Me.gcErrors.TabIndex = 12
        Me.gcErrors.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvErrors})
        '
        'gvErrors
        '
        Me.gvErrors.GridControl = Me.gcErrors
        Me.gvErrors.Name = "gvErrors"
        Me.gvErrors.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvErrors.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvErrors.OptionsBehavior.Editable = False
        Me.gvErrors.OptionsBehavior.ReadOnly = True
        Me.gvErrors.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvErrors.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvErrors.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvErrors.OptionsCustomization.AllowSort = False
        Me.gvErrors.OptionsView.ColumnAutoWidth = False
        Me.gvErrors.OptionsView.ShowGroupPanel = False
        '
        'xtpProvisionResult
        '
        Me.xtpProvisionResult.Controls.Add(Me.gcProvisionResult)
        Me.xtpProvisionResult.Name = "xtpProvisionResult"
        Me.xtpProvisionResult.Size = New System.Drawing.Size(390, 448)
        Me.xtpProvisionResult.Text = "Provision Result"
        '
        'gcProvisionResult
        '
        Me.gcProvisionResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcProvisionResult.Location = New System.Drawing.Point(0, 0)
        Me.gcProvisionResult.MainView = Me.gvProvisionResult
        Me.gcProvisionResult.Name = "gcProvisionResult"
        Me.gcProvisionResult.Size = New System.Drawing.Size(390, 448)
        Me.gcProvisionResult.TabIndex = 13
        Me.gcProvisionResult.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvProvisionResult})
        '
        'gvProvisionResult
        '
        Me.gvProvisionResult.GridControl = Me.gcProvisionResult
        Me.gvProvisionResult.Name = "gvProvisionResult"
        Me.gvProvisionResult.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvProvisionResult.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvProvisionResult.OptionsBehavior.Editable = False
        Me.gvProvisionResult.OptionsBehavior.ReadOnly = True
        Me.gvProvisionResult.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvProvisionResult.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvProvisionResult.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvProvisionResult.OptionsCustomization.AllowSort = False
        Me.gvProvisionResult.OptionsView.ColumnAutoWidth = False
        Me.gvProvisionResult.OptionsView.RowAutoHeight = True
        Me.gvProvisionResult.OptionsView.ShowGroupPanel = False
        '
        'grpLogMsgs
        '
        Me.grpLogMsgs.Controls.Add(Me.gcXmlLogMsgs)
        Me.grpLogMsgs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLogMsgs.Location = New System.Drawing.Point(0, 0)
        Me.grpLogMsgs.Name = "grpLogMsgs"
        Me.grpLogMsgs.Size = New System.Drawing.Size(1302, 385)
        Me.grpLogMsgs.TabIndex = 14
        Me.grpLogMsgs.Text = "Log Messages"
        '
        'gcXmlLogMsgs
        '
        Me.gcXmlLogMsgs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcXmlLogMsgs.Location = New System.Drawing.Point(2, 23)
        Me.gcXmlLogMsgs.MainView = Me.gvXmlLogMsgs
        Me.gcXmlLogMsgs.Name = "gcXmlLogMsgs"
        Me.gcXmlLogMsgs.Size = New System.Drawing.Size(1298, 360)
        Me.gcXmlLogMsgs.TabIndex = 13
        Me.gcXmlLogMsgs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvXmlLogMsgs})
        '
        'gvXmlLogMsgs
        '
        Me.gvXmlLogMsgs.GridControl = Me.gcXmlLogMsgs
        Me.gvXmlLogMsgs.Name = "gvXmlLogMsgs"
        Me.gvXmlLogMsgs.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXmlLogMsgs.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXmlLogMsgs.OptionsBehavior.Editable = False
        Me.gvXmlLogMsgs.OptionsBehavior.ReadOnly = True
        Me.gvXmlLogMsgs.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlLogMsgs.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlLogMsgs.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlLogMsgs.OptionsCustomization.AllowSort = False
        Me.gvXmlLogMsgs.OptionsView.ColumnAutoWidth = False
        Me.gvXmlLogMsgs.OptionsView.ShowGroupPanel = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'bgWorker
        '
        Me.bgWorker.WorkerSupportsCancellation = True
        '
        'bgWorkerRollBack
        '
        Me.bgWorkerRollBack.WorkerSupportsCancellation = True
        '
        'bgWorkerPartial
        '
        Me.bgWorkerPartial.WorkerSupportsCancellation = True
        '
        'bgWorkerPartialRollback
        '
        Me.bgWorkerPartialRollback.WorkerSupportsCancellation = True
        '
        'bgWorkerExecute
        '
        Me.bgWorkerExecute.WorkerSupportsCancellation = True
        '
        'bgWorkerExecuteRollback
        '
        Me.bgWorkerExecuteRollback.WorkerSupportsCancellation = True
        '
        'frmGenerateXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1302, 868)
        Me.Controls.Add(Me.sccMain)
        Me.IconOptions.Icon = CType(resources.GetObject("frmGenerateXML.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1296, 900)
        Me.Name = "frmGenerateXML"
        Me.Text = "Generate XML"
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel1.ResumeLayout(False)
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel2.ResumeLayout(False)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.ResumeLayout(False)
        CType(Me.sccSubTop.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccSubTop.Panel1.ResumeLayout(False)
        CType(Me.sccSubTop.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccSubTop.Panel2.ResumeLayout(False)
        CType(Me.sccSubTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccSubTop.ResumeLayout(False)
        Me.tlpXmlJobList.ResumeLayout(False)
        CType(Me.grpXmlJobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpXmlJobs.ResumeLayout(False)
        Me.tlpXmlJobs.ResumeLayout(False)
        CType(Me.gcXmlJobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmXmlJobs.ResumeLayout(False)
        CType(Me.gvXmlJobs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpXmlJobButtons.ResumeLayout(False)
        Me.tlpXmlJobButtons.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.gcOption2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcOption2.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.gcOption1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcOption1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.gcStep1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcStep1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grpPartialProvision, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPartialProvision.ResumeLayout(False)
        Me.tlpPartialProvision.ResumeLayout(False)
        CType(Me.xtcSubTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcSubTop.ResumeLayout(False)
        Me.xtpValidation.ResumeLayout(False)
        CType(Me.gcValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpInputData.ResumeLayout(False)
        CType(Me.gcInputData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmInputDataGrid.ResumeLayout(False)
        CType(Me.gvInputData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpOutputData.ResumeLayout(False)
        CType(Me.gcOutputData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOutputData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpErrors.ResumeLayout(False)
        CType(Me.gcErrors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvErrors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpProvisionResult.ResumeLayout(False)
        CType(Me.gcProvisionResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProvisionResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLogMsgs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLogMsgs.ResumeLayout(False)
        CType(Me.gcXmlLogMsgs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvXmlLogMsgs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents sccMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents sccSubTop As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents xtcSubTop As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpValidation As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpInputData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpOutputData As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpErrors As DevExpress.XtraTab.XtraTabPage
    Public WithEvents gcValidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvValidation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpXmlJobList As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents grpXmlJobs As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcXmlJobs As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvXmlJobs As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnValidate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSaveXml As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnProvision As DevExpress.XtraEditors.SimpleButton
    Public WithEvents gcInputData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvInputData As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents gcOutputData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvOutputData As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents gcErrors As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvErrors As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnRollbackProvision As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpLogMsgs As DevExpress.XtraEditors.GroupControl
    Public WithEvents gcXmlLogMsgs As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvXmlLogMsgs As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpXmlJobs As TableLayoutPanel
    Friend WithEvents tlpXmlJobButtons As TableLayoutPanel
    Friend WithEvents btnXmlJobsRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnXmlJobsAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnXmlJobsDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnKillProvision As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xtpProvisionResult As DevExpress.XtraTab.XtraTabPage
    Public WithEvents gcProvisionResult As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvProvisionResult As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcOption1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents gcStep1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents gcOption2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents cmInputDataGrid As ContextMenuStrip
    Friend WithEvents tsmi_PasteDataFromClipboard As ToolStripMenuItem
    Friend WithEvents cmXmlJobs As ContextMenuStrip
    Friend WithEvents tsmi_RenameXmlJob As ToolStripMenuItem
    Friend WithEvents tsmi_DeleteSelectedRows As ToolStripMenuItem
    Friend WithEvents bgWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgWorkerRollBack As System.ComponentModel.BackgroundWorker
    Friend WithEvents grpPartialProvision As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpPartialProvision As TableLayoutPanel
    Friend WithEvents btnCreateValidate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExecute As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCreateValidateRollback As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bgWorkerPartial As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgWorkerPartialRollback As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgWorkerExecute As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnExecuteRollback As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bgWorkerExecuteRollback As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnXmlJobsInsert As DevExpress.XtraEditors.SimpleButton
End Class
