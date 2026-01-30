<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCMTemplate
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
        Dim TreeListViewColumn1 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCMTemplate))
        Dim TreeListViewColumn2 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn3 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn4 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn5 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn6 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn7 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn8 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn9 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
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
        Me.TreeViewStats = New System.Windows.Forms.TreeView()
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
        Me.vtpTemplateManager = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.sccTemplateMngr = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TreeListView1 = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.cmsTreeListView1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiDelParameter = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDeleteGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAddNewGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtAddNewGroup = New System.Windows.Forms.ToolStripTextBox()
        Me.tsmiAddExistingGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiComboBoxGroup = New System.Windows.Forms.ToolStripComboBox()
        Me.tsmiParameterDescTLV1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiCurrentTemplate = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAllTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSelectedTemplate = New System.Windows.Forms.ToolStripMenuItem()
        Me.TreeListView2 = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.cmsTreeListView2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiParmeterDescTLV2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.vgbTempSelection = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.vlblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTemplate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAddTemplate = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteTemplate = New DevExpress.XtraEditors.SimpleButton()
        Me.vgbParameterSearch = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFilterOnObject = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtSearchLongName = New DevExpress.XtraEditors.TextEdit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_ObjectTree.SuspendLayout()
        CType(Me.txtSearchOuter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcTabParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcTabParameters.SuspendLayout()
        Me.vtpTemplateManager.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.sccTemplateMngr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccTemplateMngr.SuspendLayout()
        CType(Me.TreeListView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsTreeListView1.SuspendLayout()
        CType(Me.TreeListView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsTreeListView2.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.vgbTempSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vgbTempSelection.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vgbParameterSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vgbParameterSearch.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.cmbFilterOnObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchLongName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainerControl1.Appearance.Options.UseBackColor = True
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainerControl1.Panel1.MinSize = 300
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.xtcTabParameters)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1290, 725)
        Me.SplitContainerControl1.SplitterPosition = 300
        Me.SplitContainerControl1.TabIndex = 0
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
        Me.TableLayoutPanel1.Controls.Add(Me.TreeViewStats, 0, 9)
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(300, 725)
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
        'TreeViewStats
        '
        Me.TreeViewStats.CheckBoxes = True
        Me.TreeViewStats.ContextMenuStrip = Me.cm_ObjectTree
        Me.TreeViewStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewStats.Location = New System.Drawing.Point(3, 246)
        Me.TreeViewStats.Name = "TreeViewStats"
        Me.TreeViewStats.Size = New System.Drawing.Size(294, 476)
        Me.TreeViewStats.TabIndex = 8
        '
        'cm_ObjectTree
        '
        Me.cm_ObjectTree.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cm_OT_tsmi_copy, Me.cm_OT_tsmi_paste, Me.cm_OT_tsmi_CopyToTag, Me.ToolStripSeparator5, Me.cm_OT_tsmi_CheckChilds, Me.tsmi_OT_UnCheck, Me.ToolStripSeparator12, Me.tsmi_OT_MapCell, Me.ToolStripSeparator14, Me.tsmi_ReloadTree, Me.ToolStripSeparator15, Me.tsmi_OT_Exception})
        Me.cm_ObjectTree.Name = "cm_ObjectTree"
        Me.cm_ObjectTree.Size = New System.Drawing.Size(186, 204)
        '
        'cm_OT_tsmi_copy
        '
        Me.cm_OT_tsmi_copy.Enabled = False
        Me.cm_OT_tsmi_copy.Name = "cm_OT_tsmi_copy"
        Me.cm_OT_tsmi_copy.Size = New System.Drawing.Size(185, 22)
        Me.cm_OT_tsmi_copy.Text = "Copy"
        '
        'cm_OT_tsmi_paste
        '
        Me.cm_OT_tsmi_paste.Enabled = False
        Me.cm_OT_tsmi_paste.Name = "cm_OT_tsmi_paste"
        Me.cm_OT_tsmi_paste.Size = New System.Drawing.Size(185, 22)
        Me.cm_OT_tsmi_paste.Text = "Paste"
        '
        'cm_OT_tsmi_CopyToTag
        '
        Me.cm_OT_tsmi_CopyToTag.Name = "cm_OT_tsmi_CopyToTag"
        Me.cm_OT_tsmi_CopyToTag.Size = New System.Drawing.Size(185, 22)
        Me.cm_OT_tsmi_CopyToTag.Text = "Copy To Tag"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(182, 6)
        '
        'cm_OT_tsmi_CheckChilds
        '
        Me.cm_OT_tsmi_CheckChilds.Name = "cm_OT_tsmi_CheckChilds"
        Me.cm_OT_tsmi_CheckChilds.Size = New System.Drawing.Size(185, 22)
        Me.cm_OT_tsmi_CheckChilds.Text = "Check Children"
        '
        'tsmi_OT_UnCheck
        '
        Me.tsmi_OT_UnCheck.Name = "tsmi_OT_UnCheck"
        Me.tsmi_OT_UnCheck.Size = New System.Drawing.Size(185, 22)
        Me.tsmi_OT_UnCheck.Text = "UnCheck All"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(182, 6)
        '
        'tsmi_OT_MapCell
        '
        Me.tsmi_OT_MapCell.Name = "tsmi_OT_MapCell"
        Me.tsmi_OT_MapCell.Size = New System.Drawing.Size(185, 22)
        Me.tsmi_OT_MapCell.Text = "Map Checked Cells"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(182, 6)
        '
        'tsmi_ReloadTree
        '
        Me.tsmi_ReloadTree.Name = "tsmi_ReloadTree"
        Me.tsmi_ReloadTree.Size = New System.Drawing.Size(185, 22)
        Me.tsmi_ReloadTree.Text = "Reload ObjectTree"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(182, 6)
        '
        'tsmi_OT_Exception
        '
        Me.tsmi_OT_Exception.Name = "tsmi_OT_Exception"
        Me.tsmi_OT_Exception.Size = New System.Drawing.Size(185, 22)
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
        Me.xtcTabParameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcTabParameters.Location = New System.Drawing.Point(0, 0)
        Me.xtcTabParameters.LookAndFeel.SkinName = "Office 2013"
        Me.xtcTabParameters.Name = "xtcTabParameters"
        Me.xtcTabParameters.SelectedTabPage = Me.vtpTemplateManager
        Me.xtcTabParameters.Size = New System.Drawing.Size(985, 725)
        Me.xtcTabParameters.TabIndex = 0
        Me.xtcTabParameters.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.vtpTemplateManager})
        '
        'vtpTemplateManager
        '
        Me.vtpTemplateManager.Controls.Add(Me.TableLayoutPanel5)
        Me.vtpTemplateManager.Name = "vtpTemplateManager"
        Me.vtpTemplateManager.Size = New System.Drawing.Size(979, 697)
        Me.vtpTemplateManager.Tag = "TM"
        Me.vtpTemplateManager.Text = "Template Manager"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.sccTemplateMngr, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(979, 697)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'sccTemplateMngr
        '
        Me.sccTemplateMngr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccTemplateMngr.Location = New System.Drawing.Point(3, 153)
        Me.sccTemplateMngr.Name = "sccTemplateMngr"
        Me.sccTemplateMngr.Panel1.Controls.Add(Me.TreeListView1)
        Me.sccTemplateMngr.Panel1.MinSize = 250
        Me.sccTemplateMngr.Panel1.Text = "Panel1"
        Me.sccTemplateMngr.Panel2.Controls.Add(Me.TreeListView2)
        Me.sccTemplateMngr.Panel2.Text = "Panel2"
        Me.sccTemplateMngr.Size = New System.Drawing.Size(973, 541)
        Me.sccTemplateMngr.SplitterPosition = 300
        Me.sccTemplateMngr.TabIndex = 0
        Me.sccTemplateMngr.Text = "SplitContainerControl2"
        '
        'TreeListView1
        '
        Me.TreeListView1.AllowDrop = True
        Me.TreeListView1.AllowSelectionCheck = True
        TreeListViewColumn1.FixedWidth = True
        TreeListViewColumn1.FooterRect = CType(resources.GetObject("TreeListViewColumn1.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.FooterText = "Footer 1"
        TreeListViewColumn1.HeaderRect = CType(resources.GetObject("TreeListViewColumn1.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.HeaderText = "Group Names"
        TreeListViewColumn2.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible
        TreeListViewColumn2.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control
        TreeListViewColumn2.FixedWidth = True
        TreeListViewColumn2.FooterRect = CType(resources.GetObject("TreeListViewColumn2.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.FooterText = "Footer 2"
        TreeListViewColumn2.HeaderRect = CType(resources.GetObject("TreeListViewColumn2.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.HeaderText = "Default Values"
        TreeListViewColumn3.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible
        TreeListViewColumn3.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control
        TreeListViewColumn3.FixedWidth = True
        TreeListViewColumn3.FooterRect = CType(resources.GetObject("TreeListViewColumn3.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn3.FooterText = "Footer 3"
        TreeListViewColumn3.HeaderRect = CType(resources.GetObject("TreeListViewColumn3.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn3.HeaderText = "Active"
        Me.TreeListView1.Columns.AddRange(New Object() {TreeListViewColumn1, TreeListViewColumn2, TreeListViewColumn3})
        '
        '
        '
        Me.TreeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.TreeListView1.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.TreeListView1.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeListView1.ContentPanel.Name = ""
        Me.TreeListView1.ContentPanel.Size = New System.Drawing.Size(294, 535)
        Me.TreeListView1.ContentPanel.TabIndex = 3
        Me.TreeListView1.ContentPanel.TabStop = False
        Me.TreeListView1.ContextMenuStrip = Me.cmsTreeListView1
        Me.TreeListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.TreeListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeListView1.DragDropMode = LidorSystems.IntegralUI.DragDropMode.Custom
        Me.TreeListView1.DropMarkerType = LidorSystems.IntegralUI.Lists.DropMarkerType.[Partial]
        Me.TreeListView1.ExpandingColumn = TreeListViewColumn1
        Me.TreeListView1.Footer = False
        Me.TreeListView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeListView1.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeListView1.Name = "TreeListView1"
        Me.TreeListView1.Size = New System.Drawing.Size(300, 541)
        Me.TreeListView1.TabIndex = 1
        Me.TreeListView1.Text = "TreeListView1"
        '
        'cmsTreeListView1
        '
        Me.cmsTreeListView1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiDelParameter, Me.tsmiDeleteGroup, Me.tsmiAddNewGroup, Me.tsmiAddExistingGroup, Me.tsmiParameterDescTLV1, Me.ToolStripSeparator1, Me.tsmiCurrentTemplate, Me.tsmiAllTemplates, Me.tsmiSelectedTemplate})
        Me.cmsTreeListView1.Name = "cmsTreeListView1"
        Me.cmsTreeListView1.Size = New System.Drawing.Size(213, 186)
        '
        'tsmiDelParameter
        '
        Me.tsmiDelParameter.Name = "tsmiDelParameter"
        Me.tsmiDelParameter.Size = New System.Drawing.Size(212, 22)
        Me.tsmiDelParameter.Text = "Delete Parameter"
        '
        'tsmiDeleteGroup
        '
        Me.tsmiDeleteGroup.Name = "tsmiDeleteGroup"
        Me.tsmiDeleteGroup.Size = New System.Drawing.Size(212, 22)
        Me.tsmiDeleteGroup.Text = "Delete Group"
        '
        'tsmiAddNewGroup
        '
        Me.tsmiAddNewGroup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtAddNewGroup})
        Me.tsmiAddNewGroup.Name = "tsmiAddNewGroup"
        Me.tsmiAddNewGroup.Size = New System.Drawing.Size(212, 22)
        Me.tsmiAddNewGroup.Text = "Add New Group"
        '
        'txtAddNewGroup
        '
        Me.txtAddNewGroup.Name = "txtAddNewGroup"
        Me.txtAddNewGroup.Size = New System.Drawing.Size(100, 23)
        '
        'tsmiAddExistingGroup
        '
        Me.tsmiAddExistingGroup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiComboBoxGroup})
        Me.tsmiAddExistingGroup.Name = "tsmiAddExistingGroup"
        Me.tsmiAddExistingGroup.Size = New System.Drawing.Size(212, 22)
        Me.tsmiAddExistingGroup.Text = "Add Existing Group"
        '
        'tsmiComboBoxGroup
        '
        Me.tsmiComboBoxGroup.Name = "tsmiComboBoxGroup"
        Me.tsmiComboBoxGroup.Size = New System.Drawing.Size(140, 23)
        '
        'tsmiParameterDescTLV1
        '
        Me.tsmiParameterDescTLV1.Name = "tsmiParameterDescTLV1"
        Me.tsmiParameterDescTLV1.Size = New System.Drawing.Size(212, 22)
        Me.tsmiParameterDescTLV1.Text = "Parameter Description"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(209, 6)
        '
        'tsmiCurrentTemplate
        '
        Me.tsmiCurrentTemplate.CheckOnClick = True
        Me.tsmiCurrentTemplate.Name = "tsmiCurrentTemplate"
        Me.tsmiCurrentTemplate.Size = New System.Drawing.Size(212, 22)
        Me.tsmiCurrentTemplate.Text = "Update Current Template"
        '
        'tsmiAllTemplates
        '
        Me.tsmiAllTemplates.CheckOnClick = True
        Me.tsmiAllTemplates.Name = "tsmiAllTemplates"
        Me.tsmiAllTemplates.Size = New System.Drawing.Size(212, 22)
        Me.tsmiAllTemplates.Text = "Update All Template"
        '
        'tsmiSelectedTemplate
        '
        Me.tsmiSelectedTemplate.CheckOnClick = True
        Me.tsmiSelectedTemplate.Name = "tsmiSelectedTemplate"
        Me.tsmiSelectedTemplate.Size = New System.Drawing.Size(212, 22)
        Me.tsmiSelectedTemplate.Text = "Update Selected Template"
        '
        'TreeListView2
        '
        Me.TreeListView2.AllowDrag = True
        TreeListViewColumn4.FixedWidth = True
        TreeListViewColumn4.FooterRect = CType(resources.GetObject("TreeListViewColumn4.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn4.FooterText = "Footer 6"
        TreeListViewColumn4.HeaderRect = CType(resources.GetObject("TreeListViewColumn4.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn4.HeaderText = "ID"
        TreeListViewColumn5.FooterRect = CType(resources.GetObject("TreeListViewColumn5.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn5.FooterText = "Footer 1"
        TreeListViewColumn5.HeaderRect = CType(resources.GetObject("TreeListViewColumn5.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn5.HeaderText = "Long Name"
        TreeListViewColumn5.Width = 70
        TreeListViewColumn6.FooterRect = CType(resources.GetObject("TreeListViewColumn6.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn6.FooterText = "Footer 2"
        TreeListViewColumn6.HeaderRect = CType(resources.GetObject("TreeListViewColumn6.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn6.HeaderText = "DB Name"
        TreeListViewColumn6.Width = 70
        TreeListViewColumn7.FooterRect = CType(resources.GetObject("TreeListViewColumn7.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn7.FooterText = "Footer 3"
        TreeListViewColumn7.HeaderRect = CType(resources.GetObject("TreeListViewColumn7.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn7.HeaderText = "Object"
        TreeListViewColumn8.FooterRect = CType(resources.GetObject("TreeListViewColumn8.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn8.FooterText = "Footer 4"
        TreeListViewColumn8.HeaderRect = CType(resources.GetObject("TreeListViewColumn8.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn8.HeaderText = "Range Steps"
        TreeListViewColumn8.Width = 90
        TreeListViewColumn9.FooterRect = CType(resources.GetObject("TreeListViewColumn9.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn9.FooterText = "Footer 5"
        TreeListViewColumn9.HeaderRect = CType(resources.GetObject("TreeListViewColumn9.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn9.HeaderText = "Conv_Int_Val"
        TreeListViewColumn9.Width = 90
        Me.TreeListView2.Columns.AddRange(New Object() {TreeListViewColumn4, TreeListViewColumn5, TreeListViewColumn6, TreeListViewColumn7, TreeListViewColumn8, TreeListViewColumn9})
        '
        '
        '
        Me.TreeListView2.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.TreeListView2.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.TreeListView2.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeListView2.ContentPanel.Name = ""
        Me.TreeListView2.ContentPanel.Size = New System.Drawing.Size(662, 535)
        Me.TreeListView2.ContentPanel.TabIndex = 3
        Me.TreeListView2.ContentPanel.TabStop = False
        Me.TreeListView2.ContextMenuStrip = Me.cmsTreeListView2
        Me.TreeListView2.Cursor = System.Windows.Forms.Cursors.Default
        Me.TreeListView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeListView2.ExpandingColumn = TreeListViewColumn4
        Me.TreeListView2.Footer = False
        Me.TreeListView2.Location = New System.Drawing.Point(0, 0)
        Me.TreeListView2.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeListView2.Name = "TreeListView2"
        Me.TreeListView2.Size = New System.Drawing.Size(668, 541)
        Me.TreeListView2.TabIndex = 1
        Me.TreeListView2.Text = "TreeListView2"
        '
        'cmsTreeListView2
        '
        Me.cmsTreeListView2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiParmeterDescTLV2})
        Me.cmsTreeListView2.Name = "cmsTreeListView2"
        Me.cmsTreeListView2.Size = New System.Drawing.Size(192, 26)
        '
        'tsmiParmeterDescTLV2
        '
        Me.tsmiParmeterDescTLV2.Name = "tsmiParmeterDescTLV2"
        Me.tsmiParmeterDescTLV2.Size = New System.Drawing.Size(191, 22)
        Me.tsmiParmeterDescTLV2.Text = "Parameter Description"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.vgbTempSelection, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.vgbParameterSearch, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(973, 144)
        Me.TableLayoutPanel6.TabIndex = 1
        '
        'vgbTempSelection
        '
        Me.vgbTempSelection.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.vgbTempSelection.Appearance.Options.UseBackColor = True
        Me.vgbTempSelection.Controls.Add(Me.TableLayoutPanel7)
        Me.vgbTempSelection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vgbTempSelection.Location = New System.Drawing.Point(3, 3)
        Me.vgbTempSelection.Name = "vgbTempSelection"
        Me.vgbTempSelection.Size = New System.Drawing.Size(480, 138)
        Me.vgbTempSelection.TabIndex = 0
        Me.vgbTempSelection.Text = "Template Selection"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.vlblMessage, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel8, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(476, 116)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'vlblMessage
        '
        Me.vlblMessage.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.vlblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.vlblMessage.Appearance.Options.UseFont = True
        Me.vlblMessage.Appearance.Options.UseForeColor = True
        Me.vlblMessage.Appearance.Options.UseTextOptions = True
        Me.vlblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.vlblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vlblMessage.Location = New System.Drawing.Point(3, 90)
        Me.vlblMessage.Name = "vlblMessage"
        Me.vlblMessage.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.vlblMessage.Size = New System.Drawing.Size(470, 23)
        Me.vlblMessage.TabIndex = 8
        Me.vlblMessage.Text = "Template is locked."
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.55932!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.44068!))
        Me.TableLayoutPanel8.Controls.Add(Me.LabelControl6, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.cmbTemplate, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btnAddTemplate, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.btnDeleteTemplate, 1, 1)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(472, 83)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(175, 21)
        Me.LabelControl6.TabIndex = 4
        Me.LabelControl6.Text = "Template Selection"
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Location = New System.Drawing.Point(184, 3)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTemplate.Size = New System.Drawing.Size(250, 20)
        Me.cmbTemplate.TabIndex = 5
        '
        'btnAddTemplate
        '
        Me.btnAddTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddTemplate.Location = New System.Drawing.Point(76, 57)
        Me.btnAddTemplate.Margin = New System.Windows.Forms.Padding(3, 3, 5, 3)
        Me.btnAddTemplate.Name = "btnAddTemplate"
        Me.btnAddTemplate.Size = New System.Drawing.Size(100, 23)
        Me.btnAddTemplate.TabIndex = 6
        Me.btnAddTemplate.Text = "Add"
        '
        'btnDeleteTemplate
        '
        Me.btnDeleteTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteTemplate.Location = New System.Drawing.Point(186, 57)
        Me.btnDeleteTemplate.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.btnDeleteTemplate.Name = "btnDeleteTemplate"
        Me.btnDeleteTemplate.Size = New System.Drawing.Size(100, 23)
        Me.btnDeleteTemplate.TabIndex = 7
        Me.btnDeleteTemplate.Text = "Delete"
        '
        'vgbParameterSearch
        '
        Me.vgbParameterSearch.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.vgbParameterSearch.Appearance.Options.UseBackColor = True
        Me.vgbParameterSearch.Controls.Add(Me.TableLayoutPanel9)
        Me.vgbParameterSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vgbParameterSearch.Location = New System.Drawing.Point(489, 3)
        Me.vgbParameterSearch.Name = "vgbParameterSearch"
        Me.vgbParameterSearch.Size = New System.Drawing.Size(481, 138)
        Me.vgbParameterSearch.TabIndex = 1
        Me.vgbParameterSearch.Text = "Parameter Search"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.TableLayoutPanel10, 0, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(477, 116)
        Me.TableLayoutPanel9.TabIndex = 0
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.92373!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.07627!))
        Me.TableLayoutPanel10.Controls.Add(Me.LabelControl8, 0, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.LabelControl7, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.cmbFilterOnObject, 1, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.txtSearchLongName, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 2
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(473, 83)
        Me.TableLayoutPanel10.TabIndex = 1
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Options.UseTextOptions = True
        Me.LabelControl8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(4, 5, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(173, 51)
        Me.LabelControl8.TabIndex = 6
        Me.LabelControl8.Text = "Filter on Object"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Options.UseTextOptions = True
        Me.LabelControl7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(173, 20)
        Me.LabelControl7.TabIndex = 5
        Me.LabelControl7.Text = "Search Long Name"
        '
        'cmbFilterOnObject
        '
        Me.cmbFilterOnObject.Location = New System.Drawing.Point(182, 29)
        Me.cmbFilterOnObject.Name = "cmbFilterOnObject"
        Me.cmbFilterOnObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFilterOnObject.Size = New System.Drawing.Size(250, 20)
        Me.cmbFilterOnObject.TabIndex = 7
        '
        'txtSearchLongName
        '
        Me.txtSearchLongName.Location = New System.Drawing.Point(182, 3)
        Me.txtSearchLongName.Name = "txtSearchLongName"
        Me.txtSearchLongName.Properties.NullValuePrompt = "Search..."
        Me.txtSearchLongName.Size = New System.Drawing.Size(250, 20)
        Me.txtSearchLongName.TabIndex = 8
        '
        'frmCMTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1290, 725)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Seven"
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1300, 726)
        Me.Name = "frmCMTemplate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parameter Manager"
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
        Me.vtpTemplateManager.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.sccTemplateMngr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccTemplateMngr.ResumeLayout(False)
        CType(Me.TreeListView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsTreeListView1.ResumeLayout(False)
        CType(Me.TreeListView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsTreeListView2.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.vgbTempSelection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vgbTempSelection.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vgbParameterSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vgbParameterSearch.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        CType(Me.cmbFilterOnObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchLongName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_GetNetworks_Status As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TreeViewStats As System.Windows.Forms.TreeView
    Friend WithEvents cmbTargetObject As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbVendor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents xtcTabParameters As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents vtpTemplateManager As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents sccTemplateMngr As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TreeListView1 As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents TreeListView2 As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vgbTempSelection As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vlblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTemplate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents vgbParameterSearch As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbFilterOnObject As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtSearchLongName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAddTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmsTreeListView1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiDelParameter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiDeleteGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiAddNewGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtAddNewGroup As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tsmiAddExistingGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiComboBoxGroup As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tsmiParameterDescTLV1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmiCurrentTemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiAllTemplates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiSelectedTemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTreeListView2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiParmeterDescTLV2 As System.Windows.Forms.ToolStripMenuItem
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
End Class
