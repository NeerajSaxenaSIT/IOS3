<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCMScheduler
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCMScheduler))
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbTargetObject = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbVendor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lbl_GetNetworks_Status = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tvObjectTreeStats = New System.Windows.Forms.TreeView()
        Me.cm_ObjectTree = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cm_OT_tsmi_copy = New System.Windows.Forms.ToolStripMenuItem()
        Me.cm_OT_tsmi_paste = New System.Windows.Forms.ToolStripMenuItem()
        Me.cm_OT_tsmi_CopyToTag = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cm_OT_tsmi_CheckChilds = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_OT_UnCheck = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_OT_MapCell = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_ReloadTree = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_OT_Exception = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtSearchOuter = New DevExpress.XtraEditors.ButtonEdit()
        Me.xtcTabParameters = New DevExpress.XtraTab.XtraTabControl()
        Me.vtpCategoryManager = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.gcCategoryManager = New DevExpress.XtraGrid.GridControl()
        Me.cmsCategorManager = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectScheduleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvCategoryManager = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.btnClearGrid = New DevExpress.XtraEditors.SimpleButton()
        Me.btnModifyCellType = New DevExpress.XtraEditors.SimpleButton()
        Me.btnShowCellType = New DevExpress.XtraEditors.SimpleButton()
        Me.btnGetObjects = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbCategory = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.vtbCategoryScheduler = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcScheduleData = New DevExpress.XtraGrid.GridControl()
        Me.gvScheduleData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcCategorySchedulor = New DevExpress.XtraGrid.GridControl()
        Me.cmsSchedule = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemSheduleDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSheduleUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_Export2XML = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Export2XML_NSN = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Export2Clipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvCategorySchedulor = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmsdgvScheduleData = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.cm_GridViewMap = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RecordCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_dgv_SelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_dgv_CopyClipboardWOHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_dgv_CopyClipboardWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_dgv_ExportExcel = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_ObjectTree.SuspendLayout()
        CType(Me.txtSearchOuter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcTabParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcTabParameters.SuspendLayout()
        Me.vtpCategoryManager.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcCategoryManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsCategorManager.SuspendLayout()
        CType(Me.gvCategoryManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.cmbCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vtbCategoryScheduler.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.gcScheduleData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvScheduleData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcCategorySchedulor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsSchedule.SuspendLayout()
        CType(Me.gvCategorySchedulor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsdgvScheduleData.SuspendLayout()
        Me.cm_GridViewMap.SuspendLayout()
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
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainerControl1.Panel1.MinSize = 300
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.xtcTabParameters)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1184, 687)
        Me.SplitContainerControl1.SplitterPosition = 300
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmbTargetObject, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbVendor, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbTechnology, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_GetNetworks_Status, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl2, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl3, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl4, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.tvObjectTreeStats, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.txtSearchOuter, 0, 7)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(300, 687)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'cmbTargetObject
        '
        Me.cmbTargetObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTargetObject.Location = New System.Drawing.Point(3, 138)
        Me.cmbTargetObject.Name = "cmbTargetObject"
        Me.cmbTargetObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTargetObject.Size = New System.Drawing.Size(294, 20)
        Me.cmbTargetObject.TabIndex = 11
        '
        'cmbVendor
        '
        Me.cmbVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbVendor.Location = New System.Drawing.Point(3, 84)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbVendor.Size = New System.Drawing.Size(294, 20)
        Me.cmbVendor.TabIndex = 10
        '
        'cmbTechnology
        '
        Me.cmbTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTechnology.EditValue = "Select Technology"
        Me.cmbTechnology.Location = New System.Drawing.Point(3, 30)
        Me.cmbTechnology.Name = "cmbTechnology"
        Me.cmbTechnology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnology.Properties.Items.AddRange(New Object() {"Select Technology", "2G", "3G", "4G"})
        Me.cmbTechnology.Size = New System.Drawing.Size(294, 20)
        Me.cmbTechnology.TabIndex = 9
        '
        'lbl_GetNetworks_Status
        '
        Me.lbl_GetNetworks_Status.Appearance.Options.UseTextOptions = True
        Me.lbl_GetNetworks_Status.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lbl_GetNetworks_Status.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_GetNetworks_Status.Location = New System.Drawing.Point(3, 3)
        Me.lbl_GetNetworks_Status.Name = "lbl_GetNetworks_Status"
        Me.lbl_GetNetworks_Status.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lbl_GetNetworks_Status.Size = New System.Drawing.Size(294, 21)
        Me.lbl_GetNetworks_Status.TabIndex = 3
        Me.lbl_GetNetworks_Status.Text = "Select Technology"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 57)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(294, 21)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Select Vendor"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(294, 21)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Select Target Object Type"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 165)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(294, 21)
        Me.LabelControl3.TabIndex = 6
        Me.LabelControl3.Text = "Search Text"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 219)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(294, 21)
        Me.LabelControl4.TabIndex = 7
        Me.LabelControl4.Text = "Object Tree"
        '
        'tvObjectTreeStats
        '
        Me.tvObjectTreeStats.CheckBoxes = True
        Me.tvObjectTreeStats.ContextMenuStrip = Me.cm_ObjectTree
        Me.tvObjectTreeStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvObjectTreeStats.Location = New System.Drawing.Point(3, 246)
        Me.tvObjectTreeStats.Name = "tvObjectTreeStats"
        Me.tvObjectTreeStats.Size = New System.Drawing.Size(294, 438)
        Me.tvObjectTreeStats.TabIndex = 8
        '
        'cm_ObjectTree
        '
        Me.cm_ObjectTree.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cm_OT_tsmi_copy, Me.cm_OT_tsmi_paste, Me.cm_OT_tsmi_CopyToTag, Me.ToolStripSeparator5, Me.cm_OT_tsmi_CheckChilds, Me.tsmi_OT_UnCheck, Me.ToolStripSeparator12, Me.tsmi_OT_MapCell, Me.ToolStripSeparator14, Me.tsmi_ReloadTree, Me.ToolStripSeparator15, Me.tsmi_OT_Exception})
        Me.cm_ObjectTree.Name = "cm_ObjectTree"
        Me.cm_ObjectTree.Size = New System.Drawing.Size(187, 204)
        '
        'cm_OT_tsmi_copy
        '
        Me.cm_OT_tsmi_copy.Enabled = False
        Me.cm_OT_tsmi_copy.Name = "cm_OT_tsmi_copy"
        Me.cm_OT_tsmi_copy.Size = New System.Drawing.Size(186, 22)
        Me.cm_OT_tsmi_copy.Text = "Copy"
        '
        'cm_OT_tsmi_paste
        '
        Me.cm_OT_tsmi_paste.Enabled = False
        Me.cm_OT_tsmi_paste.Name = "cm_OT_tsmi_paste"
        Me.cm_OT_tsmi_paste.Size = New System.Drawing.Size(186, 22)
        Me.cm_OT_tsmi_paste.Text = "Paste"
        '
        'cm_OT_tsmi_CopyToTag
        '
        Me.cm_OT_tsmi_CopyToTag.Name = "cm_OT_tsmi_CopyToTag"
        Me.cm_OT_tsmi_CopyToTag.Size = New System.Drawing.Size(186, 22)
        Me.cm_OT_tsmi_CopyToTag.Text = "Copy To Tag"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(183, 6)
        '
        'cm_OT_tsmi_CheckChilds
        '
        Me.cm_OT_tsmi_CheckChilds.Name = "cm_OT_tsmi_CheckChilds"
        Me.cm_OT_tsmi_CheckChilds.Size = New System.Drawing.Size(186, 22)
        Me.cm_OT_tsmi_CheckChilds.Text = "Check Children"
        '
        'tsmi_OT_UnCheck
        '
        Me.tsmi_OT_UnCheck.Name = "tsmi_OT_UnCheck"
        Me.tsmi_OT_UnCheck.Size = New System.Drawing.Size(186, 22)
        Me.tsmi_OT_UnCheck.Text = "UnCheck All"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(183, 6)
        '
        'tsmi_OT_MapCell
        '
        Me.tsmi_OT_MapCell.Name = "tsmi_OT_MapCell"
        Me.tsmi_OT_MapCell.Size = New System.Drawing.Size(186, 22)
        Me.tsmi_OT_MapCell.Text = "Map Checked Cells"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(183, 6)
        '
        'tsmi_ReloadTree
        '
        Me.tsmi_ReloadTree.Name = "tsmi_ReloadTree"
        Me.tsmi_ReloadTree.Size = New System.Drawing.Size(186, 22)
        Me.tsmi_ReloadTree.Text = "Reload ObjectTree"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(183, 6)
        '
        'tsmi_OT_Exception
        '
        Me.tsmi_OT_Exception.Name = "tsmi_OT_Exception"
        Me.tsmi_OT_Exception.Size = New System.Drawing.Size(186, 22)
        Me.tsmi_OT_Exception.Text = "Add to Exception List"
        Me.tsmi_OT_Exception.Visible = False
        '
        'txtSearchOuter
        '
        Me.txtSearchOuter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchOuter.Location = New System.Drawing.Point(3, 192)
        Me.txtSearchOuter.Name = "txtSearchOuter"
        Me.txtSearchOuter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchOuter.Properties.NullValuePrompt = "Search..."
        Me.txtSearchOuter.Size = New System.Drawing.Size(294, 20)
        Me.txtSearchOuter.TabIndex = 12
        '
        'xtcTabParameters
        '
        Me.xtcTabParameters.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.xtcTabParameters.Appearance.Options.UseBackColor = True
        Me.xtcTabParameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcTabParameters.Location = New System.Drawing.Point(0, 0)
        Me.xtcTabParameters.Name = "xtcTabParameters"
        Me.xtcTabParameters.SelectedTabPage = Me.vtpCategoryManager
        Me.xtcTabParameters.Size = New System.Drawing.Size(874, 687)
        Me.xtcTabParameters.TabIndex = 0
        Me.xtcTabParameters.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.vtpCategoryManager, Me.vtbCategoryScheduler})
        '
        'vtpCategoryManager
        '
        Me.vtpCategoryManager.Controls.Add(Me.TableLayoutPanel3)
        Me.vtpCategoryManager.Name = "vtpCategoryManager"
        Me.vtpCategoryManager.Size = New System.Drawing.Size(872, 662)
        Me.vtpCategoryManager.Tag = "CM"
        Me.vtpCategoryManager.Text = "Category Manager"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.lblMessage, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.gcCategoryManager, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(872, 662)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblMessage.Appearance.Options.UseBackColor = True
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 629)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(866, 30)
        Me.lblMessage.TabIndex = 8
        Me.lblMessage.Text = "Message"
        '
        'gcCategoryManager
        '
        Me.gcCategoryManager.AllowDrop = True
        Me.gcCategoryManager.ContextMenuStrip = Me.cmsCategorManager
        Me.gcCategoryManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCategoryManager.Location = New System.Drawing.Point(4, 4)
        Me.gcCategoryManager.MainView = Me.gvCategoryManager
        Me.gcCategoryManager.Margin = New System.Windows.Forms.Padding(4)
        Me.gcCategoryManager.Name = "gcCategoryManager"
        Me.gcCategoryManager.Size = New System.Drawing.Size(864, 584)
        Me.gcCategoryManager.TabIndex = 2
        Me.gcCategoryManager.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCategoryManager})
        '
        'cmsCategorManager
        '
        Me.cmsCategorManager.AllowMerge = False
        Me.cmsCategorManager.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectScheduleToolStripMenuItem})
        Me.cmsCategorManager.Name = "cmsCategorManager"
        Me.cmsCategorManager.Size = New System.Drawing.Size(157, 26)
        '
        'SelectScheduleToolStripMenuItem
        '
        Me.SelectScheduleToolStripMenuItem.Name = "SelectScheduleToolStripMenuItem"
        Me.SelectScheduleToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.SelectScheduleToolStripMenuItem.Text = "Select Schedule"
        '
        'gvCategoryManager
        '
        Me.gvCategoryManager.GridControl = Me.gcCategoryManager
        Me.gvCategoryManager.Name = "gvCategoryManager"
        Me.gvCategoryManager.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCategoryManager.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCategoryManager.OptionsBehavior.AutoUpdateTotalSummary = False
        Me.gvCategoryManager.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCategoryManager.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvCategoryManager.OptionsCustomization.AllowColumnMoving = False
        Me.gvCategoryManager.OptionsFilter.UseNewCustomFilterDialog = True
        Me.gvCategoryManager.OptionsSelection.InvertSelection = True
        Me.gvCategoryManager.OptionsView.ColumnAutoWidth = False
        Me.gvCategoryManager.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 6
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl5, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnClearGrid, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnModifyCellType, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnShowCellType, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnGetObjects, 5, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.cmbCategory, 4, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 594)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(868, 30)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Options.UseTextOptions = True
        Me.LabelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(363, 3)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 5, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(182, 24)
        Me.LabelControl5.TabIndex = 8
        Me.LabelControl5.Text = "Get Objects of Category"
        '
        'btnClearGrid
        '
        Me.btnClearGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClearGrid.Location = New System.Drawing.Point(243, 3)
        Me.btnClearGrid.Name = "btnClearGrid"
        Me.btnClearGrid.Size = New System.Drawing.Size(114, 24)
        Me.btnClearGrid.TabIndex = 2
        Me.btnClearGrid.Text = "Clear Grid"
        '
        'btnModifyCellType
        '
        Me.btnModifyCellType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnModifyCellType.Location = New System.Drawing.Point(123, 3)
        Me.btnModifyCellType.Name = "btnModifyCellType"
        Me.btnModifyCellType.Size = New System.Drawing.Size(114, 24)
        Me.btnModifyCellType.TabIndex = 1
        Me.btnModifyCellType.Text = "Modify Cell Type"
        '
        'btnShowCellType
        '
        Me.btnShowCellType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnShowCellType.Location = New System.Drawing.Point(3, 3)
        Me.btnShowCellType.Name = "btnShowCellType"
        Me.btnShowCellType.Size = New System.Drawing.Size(114, 24)
        Me.btnShowCellType.TabIndex = 0
        Me.btnShowCellType.Text = "Show Cell Type"
        '
        'btnGetObjects
        '
        Me.btnGetObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGetObjects.Location = New System.Drawing.Point(751, 3)
        Me.btnGetObjects.Name = "btnGetObjects"
        Me.btnGetObjects.Size = New System.Drawing.Size(114, 24)
        Me.btnGetObjects.TabIndex = 3
        Me.btnGetObjects.Text = "Get Objects"
        '
        'cmbCategory
        '
        Me.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbCategory.EditValue = "No Category"
        Me.cmbCategory.Location = New System.Drawing.Point(551, 3)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCategory.Properties.Items.AddRange(New Object() {"No Category"})
        Me.cmbCategory.Size = New System.Drawing.Size(194, 20)
        Me.cmbCategory.TabIndex = 9
        '
        'vtbCategoryScheduler
        '
        Me.vtbCategoryScheduler.Controls.Add(Me.TableLayoutPanel2)
        Me.vtbCategoryScheduler.Name = "vtbCategoryScheduler"
        Me.vtbCategoryScheduler.PageEnabled = False
        Me.vtbCategoryScheduler.Size = New System.Drawing.Size(872, 662)
        Me.vtbCategoryScheduler.Tag = "CS"
        Me.vtbCategoryScheduler.Text = "Category Scheduler"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.gcScheduleData, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.gcCategorySchedulor, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(872, 662)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'gcScheduleData
        '
        Me.gcScheduleData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcScheduleData.Location = New System.Drawing.Point(4, 335)
        Me.gcScheduleData.MainView = Me.gvScheduleData
        Me.gcScheduleData.Margin = New System.Windows.Forms.Padding(4)
        Me.gcScheduleData.Name = "gcScheduleData"
        Me.gcScheduleData.Size = New System.Drawing.Size(864, 323)
        Me.gcScheduleData.TabIndex = 6
        Me.gcScheduleData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvScheduleData})
        '
        'gvScheduleData
        '
        Me.gvScheduleData.GridControl = Me.gcScheduleData
        Me.gvScheduleData.Name = "gvScheduleData"
        Me.gvScheduleData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScheduleData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvScheduleData.OptionsBehavior.Editable = False
        Me.gvScheduleData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvScheduleData.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvScheduleData.OptionsView.ColumnAutoWidth = False
        Me.gvScheduleData.OptionsView.ShowGroupPanel = False
        '
        'gcCategorySchedulor
        '
        Me.gcCategorySchedulor.ContextMenuStrip = Me.cmsSchedule
        Me.gcCategorySchedulor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCategorySchedulor.Location = New System.Drawing.Point(4, 4)
        Me.gcCategorySchedulor.MainView = Me.gvCategorySchedulor
        Me.gcCategorySchedulor.Margin = New System.Windows.Forms.Padding(4)
        Me.gcCategorySchedulor.Name = "gcCategorySchedulor"
        Me.gcCategorySchedulor.Size = New System.Drawing.Size(864, 323)
        Me.gcCategorySchedulor.TabIndex = 5
        Me.gcCategorySchedulor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCategorySchedulor})
        '
        'cmsSchedule
        '
        Me.cmsSchedule.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemSheduleDelete, Me.ToolStripMenuItemSheduleUpdate, Me.ToolStripSeparator2, Me.tsmi_Export2XML, Me.tsmi_Export2Clipboard})
        Me.cmsSchedule.Name = "cmsdgvScheduleData"
        Me.cmsSchedule.Size = New System.Drawing.Size(178, 98)
        '
        'ToolStripMenuItemSheduleDelete
        '
        Me.ToolStripMenuItemSheduleDelete.Name = "ToolStripMenuItemSheduleDelete"
        Me.ToolStripMenuItemSheduleDelete.Size = New System.Drawing.Size(177, 22)
        Me.ToolStripMenuItemSheduleDelete.Text = "Delete Schedule"
        '
        'ToolStripMenuItemSheduleUpdate
        '
        Me.ToolStripMenuItemSheduleUpdate.Name = "ToolStripMenuItemSheduleUpdate"
        Me.ToolStripMenuItemSheduleUpdate.Size = New System.Drawing.Size(177, 22)
        Me.ToolStripMenuItemSheduleUpdate.Text = "Update Schedule"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(174, 6)
        '
        'tsmi_Export2XML
        '
        Me.tsmi_Export2XML.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_Export2XML_NSN})
        Me.tsmi_Export2XML.Name = "tsmi_Export2XML"
        Me.tsmi_Export2XML.Size = New System.Drawing.Size(177, 22)
        Me.tsmi_Export2XML.Text = "Export to XML"
        '
        'tsmi_Export2XML_NSN
        '
        Me.tsmi_Export2XML_NSN.Name = "tsmi_Export2XML_NSN"
        Me.tsmi_Export2XML_NSN.Size = New System.Drawing.Size(156, 22)
        Me.tsmi_Export2XML_NSN.Text = "NSN - RAML2.0"
        '
        'tsmi_Export2Clipboard
        '
        Me.tsmi_Export2Clipboard.Name = "tsmi_Export2Clipboard"
        Me.tsmi_Export2Clipboard.Size = New System.Drawing.Size(177, 22)
        Me.tsmi_Export2Clipboard.Text = "Export to Clipboard"
        '
        'gvCategorySchedulor
        '
        Me.gvCategorySchedulor.GridControl = Me.gcCategorySchedulor
        Me.gvCategorySchedulor.Name = "gvCategorySchedulor"
        Me.gvCategorySchedulor.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCategorySchedulor.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCategorySchedulor.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCategorySchedulor.OptionsCustomization.AllowColumnMoving = False
        Me.gvCategorySchedulor.OptionsView.ColumnAutoWidth = False
        Me.gvCategorySchedulor.OptionsView.ShowGroupPanel = False
        '
        'cmsdgvScheduleData
        '
        Me.cmsdgvScheduleData.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemDelete})
        Me.cmsdgvScheduleData.Name = "cmsdgvScheduleData"
        Me.cmsdgvScheduleData.Size = New System.Drawing.Size(148, 26)
        '
        'ToolStripMenuItemDelete
        '
        Me.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete"
        Me.ToolStripMenuItemDelete.Size = New System.Drawing.Size(147, 22)
        Me.ToolStripMenuItemDelete.Text = "Delete Record"
        '
        'cm_GridViewMap
        '
        Me.cm_GridViewMap.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RecordCount, Me.ToolStripSeparator8, Me.tsmi_dgv_SelectAll, Me.tsmi_dgv_CopyClipboardWOHeader, Me.tsmi_dgv_CopyClipboardWithHeader, Me.tsmi_dgv_ExportExcel})
        Me.cm_GridViewMap.Name = "cm_GridViewMap"
        Me.cm_GridViewMap.Size = New System.Drawing.Size(241, 120)
        '
        'tsmi_RecordCount
        '
        Me.tsmi_RecordCount.Enabled = False
        Me.tsmi_RecordCount.Name = "tsmi_RecordCount"
        Me.tsmi_RecordCount.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_RecordCount.Text = "Record Count: "
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(237, 6)
        '
        'tsmi_dgv_SelectAll
        '
        Me.tsmi_dgv_SelectAll.Name = "tsmi_dgv_SelectAll"
        Me.tsmi_dgv_SelectAll.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_dgv_SelectAll.Text = "Copy All"
        '
        'tsmi_dgv_CopyClipboardWOHeader
        '
        Me.tsmi_dgv_CopyClipboardWOHeader.Name = "tsmi_dgv_CopyClipboardWOHeader"
        Me.tsmi_dgv_CopyClipboardWOHeader.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_dgv_CopyClipboardWOHeader.Text = "Copy Selection Without Header"
        '
        'tsmi_dgv_CopyClipboardWithHeader
        '
        Me.tsmi_dgv_CopyClipboardWithHeader.Name = "tsmi_dgv_CopyClipboardWithHeader"
        Me.tsmi_dgv_CopyClipboardWithHeader.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_dgv_CopyClipboardWithHeader.Text = "Copy Selection With Header"
        '
        'tsmi_dgv_ExportExcel
        '
        Me.tsmi_dgv_ExportExcel.Name = "tsmi_dgv_ExportExcel"
        Me.tsmi_dgv_ExportExcel.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_dgv_ExportExcel.Text = "Export All - Excel"
        '
        'frmCMScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 687)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.IconOptions.Icon = CType(resources.GetObject("frmCMScheduler.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1186, 719)
        Me.Name = "frmCMScheduler"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CM Scheduler"
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_ObjectTree.ResumeLayout(False)
        CType(Me.txtSearchOuter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcTabParameters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcTabParameters.ResumeLayout(False)
        Me.vtpCategoryManager.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.gcCategoryManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsCategorManager.ResumeLayout(False)
        CType(Me.gvCategoryManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.cmbCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vtbCategoryScheduler.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.gcScheduleData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvScheduleData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcCategorySchedulor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsSchedule.ResumeLayout(False)
        CType(Me.gvCategorySchedulor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsdgvScheduleData.ResumeLayout(False)
        Me.cm_GridViewMap.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmbTargetObject As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbVendor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lbl_GetNetworks_Status As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tvObjectTreeStats As System.Windows.Forms.TreeView
    Friend WithEvents xtcTabParameters As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents vtpCategoryManager As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcCategoryManager As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCategoryManager As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnClearGrid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnModifyCellType As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnShowCellType As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnGetObjects As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbCategory As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents vtbCategoryScheduler As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcScheduleData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvScheduleData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcCategorySchedulor As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCategorySchedulor As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmsCategorManager As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectScheduleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsdgvScheduleData As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItemDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsSchedule As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItemSheduleDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemSheduleUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_Export2XML As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_Export2XML_NSN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_Export2Clipboard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cm_GridViewMap As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_RecordCount As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_dgv_SelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_dgv_CopyClipboardWOHeader As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_dgv_ExportExcel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cm_ObjectTree As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cm_OT_tsmi_copy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cm_OT_tsmi_paste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cm_OT_tsmi_CopyToTag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cm_OT_tsmi_CheckChilds As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_OT_UnCheck As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_OT_MapCell As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_ReloadTree As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_OT_Exception As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtSearchOuter As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents tsmi_dgv_CopyClipboardWithHeader As ToolStripMenuItem
End Class
