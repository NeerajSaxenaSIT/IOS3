<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPMView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPMView))
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.tlpLeft = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbVendor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblVendor = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTargetObject = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbPmViewPreDef = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.dePMViewStart = New DevExpress.XtraEditors.DateEdit()
        Me.dePMViewEnd = New DevExpress.XtraEditors.DateEdit()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcTemp = New DevExpress.XtraGrid.GridControl()
        Me.gvTemp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnGetData = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTreeObjectsCount = New DevExpress.XtraEditors.LabelControl()
        Me.sccObjects = New DevExpress.XtraEditors.SplitContainerControl()
        Me.tvObjectTree = New DevExpress.XtraTreeList.TreeList()
        Me.cmObjectTree = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cm_OT_tsmi_copy = New System.Windows.Forms.ToolStripMenuItem()
        Me.cm_OT_tsmi_paste = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cm_OT_tsmi_CheckChilds = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_OT_UnCheck = New System.Windows.Forms.ToolStripMenuItem()
        Me.grpCheckedObjs = New DevExpress.XtraEditors.GroupControl()
        Me.lstTreeObjects = New DevExpress.XtraEditors.ListBoxControl()
        Me.cmSelectedObjs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_ClearAllObjs = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_DeleteObjs = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlCounterList = New DevExpress.XtraTreeList.TreeList()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSearchAllParameter = New DevExpress.XtraEditors.CheckEdit()
        Me.txtSearchPH = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.lstViewMeasurement = New DevExpress.XtraTreeList.TreeList()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceLoadObjectTree = New DevExpress.XtraEditors.CheckEdit()
        Me.txtSearchMM = New DevExpress.XtraEditors.ButtonEdit()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.tlvFilters = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcPMView = New DevExpress.XtraGrid.GridControl()
        Me.cmsCurrentData = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiRecordCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_AllowCellCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_CopySelectionWOHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_CopySelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_CopyFilteredToClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvPMView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDump2Xls = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMeasurementName = New DevExpress.XtraEditors.LabelControl()
        Me.lblQueryBatchSize = New DevExpress.XtraEditors.LabelControl()
        Me.btnDump2Csv = New DevExpress.XtraEditors.SimpleButton()
        Me.txtQueryBatchSize = New DevExpress.XtraEditors.TextEdit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        Me.tlpLeft.SuspendLayout()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        Me.TableLayoutPanel14.SuspendLayout()
        CType(Me.cmbPmViewPreDef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePMViewStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePMViewStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePMViewEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePMViewEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.gcTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.sccObjects, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccObjects.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccObjects.Panel1.SuspendLayout()
        CType(Me.sccObjects.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccObjects.Panel2.SuspendLayout()
        Me.sccObjects.SuspendLayout()
        CType(Me.tvObjectTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmObjectTree.SuspendLayout()
        CType(Me.grpCheckedObjs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCheckedObjs.SuspendLayout()
        CType(Me.lstTreeObjects, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmSelectedObjs.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.tlCounterList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.chkSearchAllParameter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchPH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.lstViewMeasurement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel15.SuspendLayout()
        CType(Me.ceLoadObjectTree.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchMM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.tlvFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel13.SuspendLayout()
        CType(Me.gcPMView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsCurrentData.SuspendLayout()
        CType(Me.gvPMView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.txtQueryBatchSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainerControl1.Appearance.Options.UseBackColor = True
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.tlpLeft)
        Me.SplitContainerControl1.Panel1.MinSize = 250
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.SplitContainerControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1329, 733)
        Me.SplitContainerControl1.SplitterPosition = 230
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'tlpLeft
        '
        Me.tlpLeft.BackColor = System.Drawing.Color.Transparent
        Me.tlpLeft.ColumnCount = 1
        Me.tlpLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpLeft.Controls.Add(Me.cmbTechnology, 0, 3)
        Me.tlpLeft.Controls.Add(Me.cmbVendor, 0, 1)
        Me.tlpLeft.Controls.Add(Me.lblVendor, 0, 0)
        Me.tlpLeft.Controls.Add(Me.LabelControl1, 0, 2)
        Me.tlpLeft.Controls.Add(Me.cmbTargetObject, 0, 7)
        Me.tlpLeft.Controls.Add(Me.LabelControl2, 0, 6)
        Me.tlpLeft.Controls.Add(Me.GroupControl7, 0, 4)
        Me.tlpLeft.Controls.Add(Me.TableLayoutPanel11, 0, 5)
        Me.tlpLeft.Controls.Add(Me.TableLayoutPanel1, 0, 8)
        Me.tlpLeft.Controls.Add(Me.sccObjects, 0, 9)
        Me.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpLeft.Location = New System.Drawing.Point(0, 0)
        Me.tlpLeft.Name = "tlpLeft"
        Me.tlpLeft.RowCount = 10
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpLeft.Size = New System.Drawing.Size(250, 733)
        Me.tlpLeft.TabIndex = 0
        '
        'cmbTechnology
        '
        Me.cmbTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTechnology.EditValue = "Select Technology"
        Me.cmbTechnology.Location = New System.Drawing.Point(3, 84)
        Me.cmbTechnology.Name = "cmbTechnology"
        Me.cmbTechnology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnology.Properties.Sorted = True
        Me.cmbTechnology.Size = New System.Drawing.Size(244, 20)
        Me.cmbTechnology.TabIndex = 10
        '
        'cmbVendor
        '
        Me.cmbVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbVendor.EditValue = "Select Vendor"
        Me.cmbVendor.Location = New System.Drawing.Point(3, 30)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbVendor.Properties.Items.AddRange(New Object() {"2G", "3G", "4G", "Select Technology"})
        Me.cmbVendor.Properties.Sorted = True
        Me.cmbVendor.Size = New System.Drawing.Size(244, 20)
        Me.cmbVendor.TabIndex = 9
        '
        'lblVendor
        '
        Me.lblVendor.Appearance.Options.UseTextOptions = True
        Me.lblVendor.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVendor.Location = New System.Drawing.Point(3, 3)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblVendor.Size = New System.Drawing.Size(244, 21)
        Me.lblVendor.TabIndex = 3
        Me.lblVendor.Text = "Select Vendor"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 57)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(244, 21)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Select Technology"
        '
        'cmbTargetObject
        '
        Me.cmbTargetObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTargetObject.Enabled = False
        Me.cmbTargetObject.Location = New System.Drawing.Point(3, 283)
        Me.cmbTargetObject.Name = "cmbTargetObject"
        Me.cmbTargetObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTargetObject.Size = New System.Drawing.Size(244, 20)
        Me.cmbTargetObject.TabIndex = 11
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 258)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(244, 19)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Select Target Object Type"
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.TableLayoutPanel14)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(3, 111)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(244, 108)
        Me.GroupControl7.TabIndex = 17
        Me.GroupControl7.Text = "Period Selection"
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 2
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.0!))
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.0!))
        Me.TableLayoutPanel14.Controls.Add(Me.LabelControl11, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.LabelControl12, 0, 1)
        Me.TableLayoutPanel14.Controls.Add(Me.LabelControl13, 0, 2)
        Me.TableLayoutPanel14.Controls.Add(Me.cmbPmViewPreDef, 1, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.dePMViewStart, 1, 1)
        Me.TableLayoutPanel14.Controls.Add(Me.dePMViewEnd, 1, 2)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel14.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 3
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(240, 83)
        Me.TableLayoutPanel14.TabIndex = 16
        '
        'LabelControl11
        '
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(94, 21)
        Me.LabelControl11.TabIndex = 0
        Me.LabelControl11.Text = "Predefined Time"
        '
        'LabelControl12
        '
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 30)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(94, 21)
        Me.LabelControl12.TabIndex = 1
        Me.LabelControl12.Text = "Manual Start Time"
        '
        'LabelControl13
        '
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(3, 57)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(94, 23)
        Me.LabelControl13.TabIndex = 2
        Me.LabelControl13.Text = "Manual End Time"
        '
        'cmbPmViewPreDef
        '
        Me.cmbPmViewPreDef.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbPmViewPreDef.Location = New System.Drawing.Point(103, 3)
        Me.cmbPmViewPreDef.Name = "cmbPmViewPreDef"
        Me.cmbPmViewPreDef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPmViewPreDef.Size = New System.Drawing.Size(134, 20)
        Me.cmbPmViewPreDef.TabIndex = 4
        '
        'dePMViewStart
        '
        Me.dePMViewStart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dePMViewStart.EditValue = New Date(2017, 12, 6, 0, 0, 0, 0)
        Me.dePMViewStart.Location = New System.Drawing.Point(103, 30)
        Me.dePMViewStart.Name = "dePMViewStart"
        Me.dePMViewStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePMViewStart.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePMViewStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePMViewStart.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.dePMViewStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePMViewStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dePMViewStart.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePMViewStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dePMViewStart.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.dePMViewStart.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(47)
        Me.dePMViewStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dePMViewStart.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePMViewStart.Size = New System.Drawing.Size(134, 20)
        Me.dePMViewStart.TabIndex = 5
        '
        'dePMViewEnd
        '
        Me.dePMViewEnd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dePMViewEnd.EditValue = New Date(2017, 12, 6, 0, 0, 0, 0)
        Me.dePMViewEnd.EnterMoveNextControl = True
        Me.dePMViewEnd.Location = New System.Drawing.Point(103, 57)
        Me.dePMViewEnd.Name = "dePMViewEnd"
        Me.dePMViewEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePMViewEnd.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePMViewEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePMViewEnd.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.dePMViewEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePMViewEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dePMViewEnd.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dePMViewEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dePMViewEnd.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.dePMViewEnd.Properties.Mask.PlaceHolder = Global.Microsoft.VisualBasic.ChrW(47)
        Me.dePMViewEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dePMViewEnd.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dePMViewEnd.Size = New System.Drawing.Size(134, 20)
        Me.dePMViewEnd.TabIndex = 6
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 3
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.gcTemp, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.btnGetData, 2, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.btnClear, 1, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(1, 223)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(248, 31)
        Me.TableLayoutPanel11.TabIndex = 18
        '
        'gcTemp
        '
        Me.gcTemp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTemp.Location = New System.Drawing.Point(3, 3)
        Me.gcTemp.MainView = Me.gvTemp
        Me.gcTemp.Name = "gcTemp"
        Me.gcTemp.Size = New System.Drawing.Size(98, 25)
        Me.gcTemp.TabIndex = 3
        Me.gcTemp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTemp})
        Me.gcTemp.Visible = False
        '
        'gvTemp
        '
        Me.gvTemp.GridControl = Me.gcTemp
        Me.gvTemp.Name = "gvTemp"
        '
        'btnGetData
        '
        Me.btnGetData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGetData.Location = New System.Drawing.Point(179, 3)
        Me.btnGetData.Name = "btnGetData"
        Me.btnGetData.Size = New System.Drawing.Size(66, 25)
        Me.btnGetData.TabIndex = 1
        Me.btnGetData.Text = "Get Data"
        '
        'btnClear
        '
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClear.Location = New System.Drawing.Point(107, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(66, 25)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "Clear"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblTreeObjectsCount, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 307)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(250, 27)
        Me.TableLayoutPanel1.TabIndex = 20
        '
        'lblTreeObjectsCount
        '
        Me.lblTreeObjectsCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTreeObjectsCount.Location = New System.Drawing.Point(183, 3)
        Me.lblTreeObjectsCount.Name = "lblTreeObjectsCount"
        Me.lblTreeObjectsCount.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblTreeObjectsCount.Size = New System.Drawing.Size(64, 21)
        Me.lblTreeObjectsCount.TabIndex = 1
        Me.lblTreeObjectsCount.Text = "#:"
        '
        'sccObjects
        '
        Me.sccObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccObjects.Horizontal = False
        Me.sccObjects.Location = New System.Drawing.Point(3, 337)
        Me.sccObjects.Name = "sccObjects"
        '
        'sccObjects.Panel1
        '
        Me.sccObjects.Panel1.Controls.Add(Me.tvObjectTree)
        Me.sccObjects.Panel1.MinSize = 200
        Me.sccObjects.Panel1.Text = "Panel1"
        '
        'sccObjects.Panel2
        '
        Me.sccObjects.Panel2.Controls.Add(Me.grpCheckedObjs)
        Me.sccObjects.Panel2.MinSize = 100
        Me.sccObjects.Panel2.Text = "Panel2"
        Me.sccObjects.Size = New System.Drawing.Size(244, 393)
        Me.sccObjects.SplitterPosition = 291
        Me.sccObjects.TabIndex = 21
        '
        'tvObjectTree
        '
        Me.tvObjectTree.ContextMenuStrip = Me.cmObjectTree
        Me.tvObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvObjectTree.Location = New System.Drawing.Point(0, 0)
        Me.tvObjectTree.Name = "tvObjectTree"
        Me.tvObjectTree.OptionsBehavior.Editable = False
        Me.tvObjectTree.OptionsBehavior.ReadOnly = True
        Me.tvObjectTree.OptionsBehavior.ResizeNodes = False
        Me.tvObjectTree.OptionsCustomization.AllowBandMoving = False
        Me.tvObjectTree.OptionsCustomization.AllowBandResizing = False
        Me.tvObjectTree.OptionsCustomization.AllowColumnMoving = False
        Me.tvObjectTree.OptionsCustomization.AllowColumnResizing = False
        Me.tvObjectTree.OptionsCustomization.AllowQuickHideColumns = False
        Me.tvObjectTree.OptionsCustomization.AllowSort = False
        Me.tvObjectTree.OptionsCustomization.ShowBandsInCustomizationForm = False
        Me.tvObjectTree.OptionsFilter.ExpandNodesOnFiltering = True
        Me.tvObjectTree.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.EntireBranch
        Me.tvObjectTree.OptionsFilter.ShowAllValuesInFilterPopup = True
        Me.tvObjectTree.OptionsFind.AllowIncrementalSearch = True
        Me.tvObjectTree.OptionsFind.AlwaysVisible = True
        Me.tvObjectTree.OptionsFind.ExpandNodesOnIncrementalSearch = True
        Me.tvObjectTree.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always
        Me.tvObjectTree.OptionsFind.ShowCloseButton = False
        Me.tvObjectTree.OptionsFind.ShowFindButton = False
        Me.tvObjectTree.OptionsLayout.AddNewColumns = False
        Me.tvObjectTree.OptionsMenu.EnableColumnMenu = False
        Me.tvObjectTree.OptionsMenu.EnableFooterMenu = False
        Me.tvObjectTree.OptionsMenu.EnableNodeMenu = False
        Me.tvObjectTree.OptionsNavigation.AutoFocusNewNode = True
        Me.tvObjectTree.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.tvObjectTree.OptionsSelection.MultiSelectMode = DevExpress.XtraTreeList.TreeListMultiSelectMode.CellSelect
        Me.tvObjectTree.OptionsSelection.SelectNodesOnRightClick = True
        Me.tvObjectTree.OptionsView.AutoWidth = False
        Me.tvObjectTree.OptionsView.BestFitMode = DevExpress.XtraTreeList.TreeListBestFitMode.Fast
        Me.tvObjectTree.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible
        Me.tvObjectTree.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check
        Me.tvObjectTree.Size = New System.Drawing.Size(244, 283)
        Me.tvObjectTree.TabIndex = 12
        Me.tvObjectTree.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView
        '
        'cmObjectTree
        '
        Me.cmObjectTree.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cm_OT_tsmi_copy, Me.cm_OT_tsmi_paste, Me.ToolStripSeparator5, Me.cm_OT_tsmi_CheckChilds, Me.tsmi_OT_UnCheck})
        Me.cmObjectTree.Name = "cm_ObjectTree"
        Me.cmObjectTree.Size = New System.Drawing.Size(156, 98)
        '
        'cm_OT_tsmi_copy
        '
        Me.cm_OT_tsmi_copy.Enabled = False
        Me.cm_OT_tsmi_copy.Name = "cm_OT_tsmi_copy"
        Me.cm_OT_tsmi_copy.Size = New System.Drawing.Size(155, 22)
        Me.cm_OT_tsmi_copy.Text = "Copy"
        '
        'cm_OT_tsmi_paste
        '
        Me.cm_OT_tsmi_paste.Enabled = False
        Me.cm_OT_tsmi_paste.Name = "cm_OT_tsmi_paste"
        Me.cm_OT_tsmi_paste.Size = New System.Drawing.Size(155, 22)
        Me.cm_OT_tsmi_paste.Text = "Paste"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(152, 6)
        '
        'cm_OT_tsmi_CheckChilds
        '
        Me.cm_OT_tsmi_CheckChilds.Enabled = False
        Me.cm_OT_tsmi_CheckChilds.Name = "cm_OT_tsmi_CheckChilds"
        Me.cm_OT_tsmi_CheckChilds.Size = New System.Drawing.Size(155, 22)
        Me.cm_OT_tsmi_CheckChilds.Text = "Check Children"
        '
        'tsmi_OT_UnCheck
        '
        Me.tsmi_OT_UnCheck.Enabled = False
        Me.tsmi_OT_UnCheck.Name = "tsmi_OT_UnCheck"
        Me.tsmi_OT_UnCheck.Size = New System.Drawing.Size(155, 22)
        Me.tsmi_OT_UnCheck.Text = "UnCheck All"
        '
        'grpCheckedObjs
        '
        Me.grpCheckedObjs.Controls.Add(Me.lstTreeObjects)
        Me.grpCheckedObjs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCheckedObjs.Location = New System.Drawing.Point(0, 0)
        Me.grpCheckedObjs.Name = "grpCheckedObjs"
        Me.grpCheckedObjs.Size = New System.Drawing.Size(244, 100)
        Me.grpCheckedObjs.TabIndex = 1
        Me.grpCheckedObjs.Text = "Selected Objects"
        '
        'lstTreeObjects
        '
        Me.lstTreeObjects.ContextMenuStrip = Me.cmSelectedObjs
        Me.lstTreeObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTreeObjects.HorizontalScrollbar = True
        Me.lstTreeObjects.Location = New System.Drawing.Point(2, 23)
        Me.lstTreeObjects.Name = "lstTreeObjects"
        Me.lstTreeObjects.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstTreeObjects.Size = New System.Drawing.Size(240, 75)
        Me.lstTreeObjects.TabIndex = 0
        '
        'cmSelectedObjs
        '
        Me.cmSelectedObjs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_ClearAllObjs, Me.tsmi_DeleteObjs})
        Me.cmSelectedObjs.Name = "cm_ObjectTree"
        Me.cmSelectedObjs.Size = New System.Drawing.Size(119, 48)
        '
        'tsmi_ClearAllObjs
        '
        Me.tsmi_ClearAllObjs.Name = "tsmi_ClearAllObjs"
        Me.tsmi_ClearAllObjs.Size = New System.Drawing.Size(118, 22)
        Me.tsmi_ClearAllObjs.Text = "Clear All"
        '
        'tsmi_DeleteObjs
        '
        Me.tsmi_DeleteObjs.Name = "tsmi_DeleteObjs"
        Me.tsmi_DeleteObjs.Size = New System.Drawing.Size(118, 22)
        Me.tsmi_DeleteObjs.Text = "Delete"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Horizontal = False
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        '
        'SplitContainerControl2.Panel1
        '
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.TableLayoutPanel4)
        Me.SplitContainerControl2.Panel1.MinSize = 350
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        '
        'SplitContainerControl2.Panel2
        '
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.TableLayoutPanel13)
        Me.SplitContainerControl2.Panel2.MinSize = 300
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(1069, 733)
        Me.SplitContainerControl2.SplitterPosition = 431
        Me.SplitContainerControl2.TabIndex = 1
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.GroupControl2, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.GroupControl1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel2, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1069, 423)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel10)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(398, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(389, 417)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "Counters"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.56291!))
        Me.TableLayoutPanel10.Controls.Add(Me.tlCounterList, 0, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 2
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(385, 392)
        Me.TableLayoutPanel10.TabIndex = 2
        '
        'tlCounterList
        '
        Me.tlCounterList.AllowDrop = True
        Me.tlCounterList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlCounterList.Location = New System.Drawing.Point(3, 30)
        Me.tlCounterList.Name = "tlCounterList"
        Me.tlCounterList.OptionsBehavior.AllowExpandOnDblClick = False
        Me.tlCounterList.OptionsBehavior.Editable = False
        Me.tlCounterList.OptionsBehavior.ReadOnly = True
        Me.tlCounterList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[False]
        Me.tlCounterList.OptionsCustomization.AllowBandMoving = False
        Me.tlCounterList.OptionsCustomization.AllowColumnMoving = False
        Me.tlCounterList.OptionsCustomization.AllowQuickHideColumns = False
        Me.tlCounterList.OptionsMenu.EnableColumnMenu = False
        Me.tlCounterList.OptionsMenu.EnableFooterMenu = False
        Me.tlCounterList.OptionsMenu.ShowAutoFilterRowItem = False
        Me.tlCounterList.OptionsNavigation.MoveOnEdit = False
        Me.tlCounterList.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.tlCounterList.OptionsSelection.MultiSelect = True
        Me.tlCounterList.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible
        Me.tlCounterList.OptionsView.ExpandButtonCentered = False
        Me.tlCounterList.OptionsView.ShowButtons = False
        Me.tlCounterList.OptionsView.ShowHorzLines = False
        Me.tlCounterList.OptionsView.ShowIndicator = False
        Me.tlCounterList.OptionsView.ShowRoot = False
        Me.tlCounterList.OptionsView.ShowVertLines = False
        Me.tlCounterList.Size = New System.Drawing.Size(379, 359)
        Me.tlCounterList.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.chkSearchAllParameter, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtSearchPH, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(385, 27)
        Me.TableLayoutPanel3.TabIndex = 5
        '
        'chkSearchAllParameter
        '
        Me.chkSearchAllParameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkSearchAllParameter.Location = New System.Drawing.Point(265, 3)
        Me.chkSearchAllParameter.Name = "chkSearchAllParameter"
        Me.chkSearchAllParameter.Properties.Caption = "Search All Counters"
        Me.chkSearchAllParameter.Size = New System.Drawing.Size(117, 21)
        Me.chkSearchAllParameter.TabIndex = 1
        '
        'txtSearchPH
        '
        Me.txtSearchPH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchPH.Location = New System.Drawing.Point(3, 3)
        Me.txtSearchPH.Name = "txtSearchPH"
        Me.txtSearchPH.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchPH.Properties.NullValuePrompt = "Search..."
        Me.txtSearchPH.Size = New System.Drawing.Size(256, 20)
        Me.txtSearchPH.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.Appearance.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel8)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(389, 417)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Measurement Selection"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.lstViewMeasurement, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.TableLayoutPanel15, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(385, 392)
        Me.TableLayoutPanel8.TabIndex = 1
        '
        'lstViewMeasurement
        '
        Me.lstViewMeasurement.AllowDrop = True
        Me.lstViewMeasurement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstViewMeasurement.Location = New System.Drawing.Point(3, 30)
        Me.lstViewMeasurement.Name = "lstViewMeasurement"
        Me.lstViewMeasurement.OptionsBehavior.AllowExpandOnDblClick = False
        Me.lstViewMeasurement.OptionsBehavior.Editable = False
        Me.lstViewMeasurement.OptionsBehavior.ReadOnly = True
        Me.lstViewMeasurement.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[False]
        Me.lstViewMeasurement.OptionsCustomization.AllowBandMoving = False
        Me.lstViewMeasurement.OptionsCustomization.AllowBandResizing = False
        Me.lstViewMeasurement.OptionsCustomization.AllowColumnMoving = False
        Me.lstViewMeasurement.OptionsCustomization.AllowColumnResizing = False
        Me.lstViewMeasurement.OptionsCustomization.AllowQuickHideColumns = False
        Me.lstViewMeasurement.OptionsMenu.EnableColumnMenu = False
        Me.lstViewMeasurement.OptionsMenu.EnableFooterMenu = False
        Me.lstViewMeasurement.OptionsMenu.ShowAutoFilterRowItem = False
        Me.lstViewMeasurement.OptionsNavigation.MoveOnEdit = False
        Me.lstViewMeasurement.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.lstViewMeasurement.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible
        Me.lstViewMeasurement.OptionsView.ExpandButtonCentered = False
        Me.lstViewMeasurement.OptionsView.ShowButtons = False
        Me.lstViewMeasurement.OptionsView.ShowHorzLines = False
        Me.lstViewMeasurement.OptionsView.ShowIndicator = False
        Me.lstViewMeasurement.OptionsView.ShowRoot = False
        Me.lstViewMeasurement.OptionsView.ShowVertLines = False
        Me.lstViewMeasurement.Size = New System.Drawing.Size(379, 359)
        Me.lstViewMeasurement.TabIndex = 3
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 2
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.ceLoadObjectTree, 1, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.txtSearchMM, 0, 0)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 1
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(385, 27)
        Me.TableLayoutPanel15.TabIndex = 4
        '
        'ceLoadObjectTree
        '
        Me.ceLoadObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceLoadObjectTree.EditValue = True
        Me.ceLoadObjectTree.Location = New System.Drawing.Point(278, 3)
        Me.ceLoadObjectTree.Name = "ceLoadObjectTree"
        Me.ceLoadObjectTree.Properties.Caption = "Load Object Tree"
        Me.ceLoadObjectTree.Size = New System.Drawing.Size(104, 21)
        Me.ceLoadObjectTree.TabIndex = 2
        '
        'txtSearchMM
        '
        Me.txtSearchMM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchMM.Location = New System.Drawing.Point(3, 3)
        Me.txtSearchMM.Name = "txtSearchMM"
        Me.txtSearchMM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchMM.Properties.NullValuePrompt = "Search..."
        Me.txtSearchMM.Size = New System.Drawing.Size(269, 20)
        Me.txtSearchMM.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupControl6, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(790, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 423.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(279, 423)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.TableLayoutPanel12)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(273, 417)
        Me.GroupControl6.TabIndex = 5
        Me.GroupControl6.Text = "Counter Filter"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 1
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.tlvFilters, 0, 1)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 2
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(269, 392)
        Me.TableLayoutPanel12.TabIndex = 0
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(263, 28)
        Me.LabelControl8.TabIndex = 3
        Me.LabelControl8.Text = "1. Drag counter to filter at query time" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2. Set operators like (>, <, =, <>) and " &
    "value"
        '
        'tlvFilters
        '
        Me.tlvFilters.AllowDrag = True
        Me.tlvFilters.AllowDrop = True
        '
        '
        '
        Me.tlvFilters.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvFilters.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvFilters.ContentPanel.Name = ""
        Me.tlvFilters.ContentPanel.Size = New System.Drawing.Size(257, 346)
        Me.tlvFilters.ContentPanel.TabIndex = 3
        Me.tlvFilters.ContentPanel.TabStop = False
        Me.tlvFilters.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvFilters.Footer = False
        Me.tlvFilters.Location = New System.Drawing.Point(3, 37)
        Me.tlvFilters.Name = "tlvFilters"
        Me.tlvFilters.Size = New System.Drawing.Size(263, 352)
        Me.tlvFilters.TabIndex = 4
        Me.tlvFilters.Text = "TreeListView1"
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 1
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.gcPMView, 0, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.TableLayoutPanel7, 0, 0)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 2
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(1069, 300)
        Me.TableLayoutPanel13.TabIndex = 0
        '
        'gcPMView
        '
        Me.gcPMView.AllowDrop = True
        Me.gcPMView.ContextMenuStrip = Me.cmsCurrentData
        Me.gcPMView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcPMView.Location = New System.Drawing.Point(3, 35)
        Me.gcPMView.MainView = Me.gvPMView
        Me.gcPMView.Name = "gcPMView"
        Me.gcPMView.Size = New System.Drawing.Size(1063, 262)
        Me.gcPMView.TabIndex = 1
        Me.gcPMView.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvPMView, Me.GridView1})
        '
        'cmsCurrentData
        '
        Me.cmsCurrentData.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiRecordCount, Me.ToolStripSeparator1, Me.tsmi_AllowCellCopy, Me.tsmi_CopySelectionWOHeader, Me.tsmi_CopySelectionWithHeader, Me.tsmi_CopyFilteredToClipboard})
        Me.cmsCurrentData.Name = "cmsHistoryChanges"
        Me.cmsCurrentData.Size = New System.Drawing.Size(249, 120)
        '
        'tsmiRecordCount
        '
        Me.tsmiRecordCount.Name = "tsmiRecordCount"
        Me.tsmiRecordCount.Size = New System.Drawing.Size(248, 22)
        Me.tsmiRecordCount.Text = "Record Count: "
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(245, 6)
        '
        'tsmi_AllowCellCopy
        '
        Me.tsmi_AllowCellCopy.CheckOnClick = True
        Me.tsmi_AllowCellCopy.Name = "tsmi_AllowCellCopy"
        Me.tsmi_AllowCellCopy.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_AllowCellCopy.Text = "Allow Cell Copy"
        '
        'tsmi_CopySelectionWOHeader
        '
        Me.tsmi_CopySelectionWOHeader.Name = "tsmi_CopySelectionWOHeader"
        Me.tsmi_CopySelectionWOHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_CopySelectionWOHeader.Text = "Copy - Selection Without Header"
        '
        'tsmi_CopySelectionWithHeader
        '
        Me.tsmi_CopySelectionWithHeader.Name = "tsmi_CopySelectionWithHeader"
        Me.tsmi_CopySelectionWithHeader.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_CopySelectionWithHeader.Text = "Copy - Selection With Header"
        '
        'tsmi_CopyFilteredToClipboard
        '
        Me.tsmi_CopyFilteredToClipboard.Name = "tsmi_CopyFilteredToClipboard"
        Me.tsmi_CopyFilteredToClipboard.Size = New System.Drawing.Size(248, 22)
        Me.tsmi_CopyFilteredToClipboard.Text = "Copy - Filtered to Clipboard"
        '
        'gvPMView
        '
        Me.gvPMView.ActiveFilterEnabled = False
        Me.gvPMView.GridControl = Me.gcPMView
        Me.gvPMView.Name = "gvPMView"
        Me.gvPMView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvPMView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvPMView.OptionsBehavior.Editable = False
        Me.gvPMView.OptionsBehavior.ReadOnly = True
        Me.gvPMView.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPMView.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPMView.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPMView.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvPMView.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPMView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvPMView.OptionsFilter.ShowAllTableValuesInFilterPopup = True
        Me.gvPMView.OptionsFilter.UseNewCustomFilterDialog = True
        Me.gvPMView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvPMView.OptionsSelection.MultiSelect = True
        Me.gvPMView.OptionsView.ShowAutoFilterRow = True
        Me.gvPMView.OptionsView.ShowGroupPanel = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcPMView
        Me.GridView1.Name = "GridView1"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 5
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.btnDump2Xls, 4, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.lblMeasurementName, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.lblQueryBatchSize, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.btnDump2Csv, 3, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtQueryBatchSize, 2, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(1067, 30)
        Me.TableLayoutPanel7.TabIndex = 2
        '
        'btnDump2Xls
        '
        Me.btnDump2Xls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDump2Xls.ImageOptions.Image = CType(resources.GetObject("btnDump2Xls.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDump2Xls.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnDump2Xls.Location = New System.Drawing.Point(1010, 3)
        Me.btnDump2Xls.Name = "btnDump2Xls"
        Me.btnDump2Xls.Size = New System.Drawing.Size(54, 24)
        Me.btnDump2Xls.TabIndex = 2
        Me.btnDump2Xls.Text = "XLS"
        '
        'lblMeasurementName
        '
        Me.lblMeasurementName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMeasurementName.Location = New System.Drawing.Point(3, 3)
        Me.lblMeasurementName.Name = "lblMeasurementName"
        Me.lblMeasurementName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMeasurementName.Size = New System.Drawing.Size(782, 24)
        Me.lblMeasurementName.TabIndex = 0
        Me.lblMeasurementName.Text = "Current Data:"
        '
        'lblQueryBatchSize
        '
        Me.lblQueryBatchSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblQueryBatchSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblQueryBatchSize.Location = New System.Drawing.Point(791, 3)
        Me.lblQueryBatchSize.Name = "lblQueryBatchSize"
        Me.lblQueryBatchSize.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblQueryBatchSize.Size = New System.Drawing.Size(91, 24)
        Me.lblQueryBatchSize.TabIndex = 0
        Me.lblQueryBatchSize.Text = "Query batch size:"
        '
        'btnDump2Csv
        '
        Me.btnDump2Csv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDump2Csv.ImageOptions.Image = CType(resources.GetObject("btnDump2Csv.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDump2Csv.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnDump2Csv.Location = New System.Drawing.Point(950, 3)
        Me.btnDump2Csv.Name = "btnDump2Csv"
        Me.btnDump2Csv.Size = New System.Drawing.Size(54, 24)
        Me.btnDump2Csv.TabIndex = 1
        Me.btnDump2Csv.Text = "CSV"
        '
        'txtQueryBatchSize
        '
        Me.txtQueryBatchSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtQueryBatchSize.EditValue = "1000"
        Me.txtQueryBatchSize.Location = New System.Drawing.Point(888, 5)
        Me.txtQueryBatchSize.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.txtQueryBatchSize.Name = "txtQueryBatchSize"
        Me.txtQueryBatchSize.Size = New System.Drawing.Size(56, 20)
        Me.txtQueryBatchSize.TabIndex = 1
        '
        'frmPMView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1329, 733)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.MinimumSize = New System.Drawing.Size(1188, 599)
        Me.Name = "frmPMView"
        Me.Text = "PM View"
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        Me.tlpLeft.ResumeLayout(False)
        Me.tlpLeft.PerformLayout()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.TableLayoutPanel14.ResumeLayout(False)
        Me.TableLayoutPanel14.PerformLayout()
        CType(Me.cmbPmViewPreDef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePMViewStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePMViewStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePMViewEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePMViewEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel11.ResumeLayout(False)
        CType(Me.gcTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.sccObjects.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccObjects.Panel1.ResumeLayout(False)
        CType(Me.sccObjects.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccObjects.Panel2.ResumeLayout(False)
        CType(Me.sccObjects, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccObjects.ResumeLayout(False)
        CType(Me.tvObjectTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmObjectTree.ResumeLayout(False)
        CType(Me.grpCheckedObjs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCheckedObjs.ResumeLayout(False)
        CType(Me.lstTreeObjects, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmSelectedObjs.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        CType(Me.tlCounterList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.chkSearchAllParameter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchPH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        CType(Me.lstViewMeasurement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel15.ResumeLayout(False)
        CType(Me.ceLoadObjectTree.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchMM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        CType(Me.tlvFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel13.ResumeLayout(False)
        CType(Me.gcPMView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsCurrentData.ResumeLayout(False)
        CType(Me.gvPMView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        CType(Me.txtQueryBatchSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpLeft As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmObjectTree As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cm_OT_tsmi_copy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cm_OT_tsmi_paste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cm_OT_tsmi_CheckChilds As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_OT_UnCheck As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlvFilters As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents TableLayoutPanel15 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel13 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel14 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmsCurrentData As ContextMenuStrip
    Friend WithEvents tsmi_AllowCellCopy As ToolStripMenuItem
    Friend WithEvents tsmi_CopySelectionWOHeader As ToolStripMenuItem
    Friend WithEvents tsmiRecordCount As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsmi_CopyFilteredToClipboard As ToolStripMenuItem
    Friend WithEvents tsmi_CopySelectionWithHeader As ToolStripMenuItem
    Private WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Private WithEvents cmbTargetObject As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents lblVendor As DevExpress.XtraEditors.LabelControl
    Private WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Private WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Private WithEvents cmbVendor As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Private WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Private WithEvents btnDump2Csv As DevExpress.XtraEditors.SimpleButton
    Private WithEvents lblQueryBatchSize As DevExpress.XtraEditors.LabelControl
    Private WithEvents txtQueryBatchSize As DevExpress.XtraEditors.TextEdit
    Private WithEvents btnDump2Xls As DevExpress.XtraEditors.SimpleButton
    Private WithEvents chkSearchAllParameter As DevExpress.XtraEditors.CheckEdit
    Private WithEvents tlCounterList As DevExpress.XtraTreeList.TreeList
    Private WithEvents lstViewMeasurement As DevExpress.XtraTreeList.TreeList
    Private WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Private WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Private WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Private WithEvents ceLoadObjectTree As DevExpress.XtraEditors.CheckEdit
    Private WithEvents gcPMView As DevExpress.XtraGrid.GridControl
    Private WithEvents gvPMView As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Private WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Private WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Private WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Private WithEvents cmbPmViewPreDef As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents dePMViewStart As DevExpress.XtraEditors.DateEdit
    Private WithEvents dePMViewEnd As DevExpress.XtraEditors.DateEdit
    Private WithEvents txtSearchPH As DevExpress.XtraEditors.ButtonEdit
    Private WithEvents txtSearchMM As DevExpress.XtraEditors.ButtonEdit
    Private WithEvents lblMeasurementName As DevExpress.XtraEditors.LabelControl
    Private WithEvents btnGetData As DevExpress.XtraEditors.SimpleButton
    Private WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Private WithEvents gcTemp As DevExpress.XtraGrid.GridControl
    Private WithEvents gvTemp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblTreeObjectsCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents sccObjects As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents lstTreeObjects As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents grpCheckedObjs As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmSelectedObjs As ContextMenuStrip
    Friend WithEvents tsmi_ClearAllObjs As ToolStripMenuItem
    Friend WithEvents tsmi_DeleteObjs As ToolStripMenuItem
    Friend WithEvents tvObjectTree As DevExpress.XtraTreeList.TreeList
End Class
