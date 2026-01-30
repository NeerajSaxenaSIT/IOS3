<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTagManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTagManager))
        Dim TreeListViewColumn2 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn3 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn4 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn5 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn6 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn7 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn8 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn9 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcTagsList = New DevExpress.XtraGrid.GridControl()
        Me.cm_TagManagement = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_AddTag = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_DeleteTag = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_RenameTag = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_PreAggregration = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvTagsList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btnTagInsert = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbObjectType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbTagType = New DevExpress.XtraEditors.CheckedComboBoxEdit()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.xtcTagManager = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpListManager = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.gcTags = New DevExpress.XtraGrid.GridControl()
        Me.cm_TagPaste = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_TagPaste_Paste = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvTags = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnTagSave = New DevExpress.XtraEditors.SimpleButton()
        Me.xtpCMBased = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnInsertCMBased = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLoadParameter = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlvParameter = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFilterOnObject = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtSearchLongName = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlvCMParamenter = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.xtpRegionBased = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl3 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRefreshTabFiles = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTabFile = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbRegionColumn = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnImport = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.txtRegionName = New DevExpress.XtraEditors.TextEdit()
        Me.btnManualCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.togBtnDraw = New IOS.Library.IOSToggleButton()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.tlvRegionList = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.cms_RegionBase = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RegionEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_RegionDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cms_CMParameter = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_Edit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Delete = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.gcTagsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_TagManagement.SuspendLayout()
        CType(Me.gvTagsList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbObjectType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTagType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.xtcTagManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcTagManager.SuspendLayout()
        Me.xtpListManager.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.gcTags, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_TagPaste.SuspendLayout()
        CType(Me.gvTags, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpCMBased.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.tlvParameter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.cmbFilterOnObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchLongName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.tlvCMParamenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpRegionBased.SuspendLayout()
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl3.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl3.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl3.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl3.Panel2.SuspendLayout()
        Me.SplitContainerControl3.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        CType(Me.cmbTabFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRegionColumn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.txtRegionName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.tlvRegionList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cms_RegionBase.SuspendLayout()
        Me.cms_CMParameter.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.TableLayoutPanel4)
        Me.SplitContainerControl1.Panel1.MinSize = 400
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainerControl1.Panel2.MinSize = 600
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1394, 818)
        Me.SplitContainerControl1.SplitterPosition = 400
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.gcTagsList, 0, 8)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl2, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl3, 0, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl4, 0, 7)
        Me.TableLayoutPanel4.Controls.Add(Me.btnTagInsert, 0, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.cmbTechnology, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.cmbObjectType, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.cmbTagType, 0, 5)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 9
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(400, 818)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'gcTagsList
        '
        Me.gcTagsList.ContextMenuStrip = Me.cm_TagManagement
        Me.gcTagsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTagsList.Location = New System.Drawing.Point(3, 221)
        Me.gcTagsList.MainView = Me.gvTagsList
        Me.gcTagsList.Name = "gcTagsList"
        Me.gcTagsList.Size = New System.Drawing.Size(394, 594)
        Me.gcTagsList.TabIndex = 13
        Me.gcTagsList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTagsList})
        '
        'cm_TagManagement
        '
        Me.cm_TagManagement.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_AddTag, Me.tsmi_DeleteTag, Me.tsmi_RenameTag, Me.tsmi_PreAggregration})
        Me.cm_TagManagement.Name = "cm_TagManagement"
        Me.cm_TagManagement.Size = New System.Drawing.Size(159, 92)
        '
        'tsmi_AddTag
        '
        Me.tsmi_AddTag.Name = "tsmi_AddTag"
        Me.tsmi_AddTag.Size = New System.Drawing.Size(158, 22)
        Me.tsmi_AddTag.Text = "Tag  -  Add New"
        '
        'tsmi_DeleteTag
        '
        Me.tsmi_DeleteTag.Name = "tsmi_DeleteTag"
        Me.tsmi_DeleteTag.Size = New System.Drawing.Size(158, 22)
        Me.tsmi_DeleteTag.Text = "Tag  -  Delete"
        '
        'tsmi_RenameTag
        '
        Me.tsmi_RenameTag.Name = "tsmi_RenameTag"
        Me.tsmi_RenameTag.Size = New System.Drawing.Size(158, 22)
        Me.tsmi_RenameTag.Text = "Tag  -  Rename"
        '
        'tsmi_PreAggregration
        '
        Me.tsmi_PreAggregration.Enabled = False
        Me.tsmi_PreAggregration.Name = "tsmi_PreAggregration"
        Me.tsmi_PreAggregration.Size = New System.Drawing.Size(158, 22)
        Me.tsmi_PreAggregration.Text = "PreAggregation"
        '
        'gvTagsList
        '
        Me.gvTagsList.GridControl = Me.gcTagsList
        Me.gvTagsList.Name = "gvTagsList"
        Me.gvTagsList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTagsList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTagsList.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTagsList.OptionsBehavior.Editable = False
        Me.gvTagsList.OptionsBehavior.ReadOnly = True
        Me.gvTagsList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTagsList.OptionsView.ShowGroupPanel = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(394, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Select Technology"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 57)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(394, 22)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Select Object Type"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(394, 22)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Select Tag Type"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 195)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(394, 20)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Tag List"
        '
        'btnTagInsert
        '
        Me.btnTagInsert.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnTagInsert.Location = New System.Drawing.Point(280, 164)
        Me.btnTagInsert.Margin = New System.Windows.Forms.Padding(2)
        Me.btnTagInsert.Name = "btnTagInsert"
        Me.btnTagInsert.Size = New System.Drawing.Size(118, 26)
        Me.btnTagInsert.TabIndex = 8
        Me.btnTagInsert.Text = "Add Tag"
        '
        'cmbTechnology
        '
        Me.cmbTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTechnology.Location = New System.Drawing.Point(3, 31)
        Me.cmbTechnology.Name = "cmbTechnology"
        Me.cmbTechnology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnology.Size = New System.Drawing.Size(394, 20)
        Me.cmbTechnology.TabIndex = 10
        '
        'cmbObjectType
        '
        Me.cmbObjectType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbObjectType.Location = New System.Drawing.Point(3, 85)
        Me.cmbObjectType.Name = "cmbObjectType"
        Me.cmbObjectType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbObjectType.Size = New System.Drawing.Size(394, 20)
        Me.cmbObjectType.TabIndex = 11
        '
        'cmbTagType
        '
        Me.cmbTagType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTagType.Location = New System.Drawing.Point(3, 139)
        Me.cmbTagType.Name = "cmbTagType"
        Me.cmbTagType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTagType.Properties.PopupSizeable = False
        Me.cmbTagType.Properties.SelectAllItemVisible = False
        Me.cmbTagType.Properties.ShowButtons = False
        Me.cmbTagType.Size = New System.Drawing.Size(394, 20)
        Me.cmbTagType.TabIndex = 12
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.xtcTagManager, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(984, 818)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 796)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(978, 19)
        Me.lblMessage.TabIndex = 4
        '
        'xtcTagManager
        '
        Me.xtcTagManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcTagManager.Location = New System.Drawing.Point(3, 3)
        Me.xtcTagManager.Name = "xtcTagManager"
        Me.xtcTagManager.SelectedTabPage = Me.xtpListManager
        Me.xtcTagManager.Size = New System.Drawing.Size(978, 787)
        Me.xtcTagManager.TabIndex = 0
        Me.xtcTagManager.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpListManager, Me.xtpCMBased, Me.xtpRegionBased})
        '
        'xtpListManager
        '
        Me.xtpListManager.Controls.Add(Me.TableLayoutPanel2)
        Me.xtpListManager.Name = "xtpListManager"
        Me.xtpListManager.Size = New System.Drawing.Size(976, 762)
        Me.xtpListManager.Tag = "List"
        Me.xtpListManager.Text = "List"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl5, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(976, 762)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Options.UseTextOptions = True
        Me.LabelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 28)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(970, 74)
        Me.LabelControl5.TabIndex = 5
        Me.LabelControl5.Text = "Build a Tag based on a static List of Objects" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Method 1: Copy/Paste From Clipbo" &
    "ard to Grid below. Ensure Correct ObjectID entry!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Method 2: Right Click on Obje" &
    "ct Tree and Transfer selection to Grid"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.GroupControl1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnTagSave, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 108)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(970, 651)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.gcTags)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(814, 645)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Object Name And IDs"
        '
        'gcTags
        '
        Me.gcTags.ContextMenuStrip = Me.cm_TagPaste
        Me.gcTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTags.Location = New System.Drawing.Point(2, 23)
        Me.gcTags.MainView = Me.gvTags
        Me.gcTags.Name = "gcTags"
        Me.gcTags.Size = New System.Drawing.Size(810, 620)
        Me.gcTags.TabIndex = 6
        Me.gcTags.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTags})
        '
        'cm_TagPaste
        '
        Me.cm_TagPaste.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_TagPaste_Paste})
        Me.cm_TagPaste.Name = "cm_TagManagement"
        Me.cm_TagPaste.Size = New System.Drawing.Size(189, 26)
        '
        'tsmi_TagPaste_Paste
        '
        Me.tsmi_TagPaste_Paste.Name = "tsmi_TagPaste_Paste"
        Me.tsmi_TagPaste_Paste.Size = New System.Drawing.Size(188, 22)
        Me.tsmi_TagPaste_Paste.Text = "Paste From Clipboard"
        '
        'gvTags
        '
        Me.gvTags.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.gvTags.GridControl = Me.gcTags
        Me.gvTags.Name = "gvTags"
        Me.gvTags.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTags.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTags.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTags.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTags.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvTags.OptionsSelection.MultiSelect = True
        Me.gvTags.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gvTags.OptionsView.ShowGroupPanel = False
        '
        'btnTagSave
        '
        Me.btnTagSave.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnTagSave.Location = New System.Drawing.Point(823, 3)
        Me.btnTagSave.Name = "btnTagSave"
        Me.btnTagSave.Size = New System.Drawing.Size(144, 25)
        Me.btnTagSave.TabIndex = 1
        Me.btnTagSave.Text = "Save"
        '
        'xtpCMBased
        '
        Me.xtpCMBased.Controls.Add(Me.TableLayoutPanel5)
        Me.xtpCMBased.Name = "xtpCMBased"
        Me.xtpCMBased.Size = New System.Drawing.Size(976, 762)
        Me.xtpCMBased.Tag = "CMBased"
        Me.xtpCMBased.Text = "CM Based"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.btnInsertCMBased, 0, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.btnLoadParameter, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl6, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.SplitContainerControl2, 0, 2)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 4
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(976, 762)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'btnInsertCMBased
        '
        Me.btnInsertCMBased.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnInsertCMBased.Location = New System.Drawing.Point(856, 735)
        Me.btnInsertCMBased.Name = "btnInsertCMBased"
        Me.btnInsertCMBased.Size = New System.Drawing.Size(117, 24)
        Me.btnInsertCMBased.TabIndex = 12
        Me.btnInsertCMBased.Text = "Commit"
        '
        'btnLoadParameter
        '
        Me.btnLoadParameter.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLoadParameter.Location = New System.Drawing.Point(793, 3)
        Me.btnLoadParameter.Name = "btnLoadParameter"
        Me.btnLoadParameter.Size = New System.Drawing.Size(180, 24)
        Me.btnLoadParameter.TabIndex = 9
        Me.btnLoadParameter.Text = "Load Parameter"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 33)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(970, 24)
        Me.LabelControl6.TabIndex = 10
        Me.LabelControl6.Text = "Build a Tag based on a CM Parameter Filter"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Location = New System.Drawing.Point(3, 63)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        '
        'SplitContainerControl2.Panel1
        '
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.TableLayoutPanel6)
        Me.SplitContainerControl2.Panel1.MinSize = 300
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        '
        'SplitContainerControl2.Panel2
        '
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.GroupControl2)
        Me.SplitContainerControl2.Panel2.MinSize = 300
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(970, 666)
        Me.SplitContainerControl2.SplitterPosition = 468
        Me.SplitContainerControl2.TabIndex = 11
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.tlvParameter, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.GroupControl3, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(468, 666)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'tlvParameter
        '
        Me.tlvParameter.AllowDrag = True
        Me.tlvParameter.AllowDrop = True
        TreeListViewColumn1.FixedWidth = True
        TreeListViewColumn1.FooterRect = CType(resources.GetObject("TreeListViewColumn1.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.FooterText = "Footer 6"
        TreeListViewColumn1.HeaderRect = CType(resources.GetObject("TreeListViewColumn1.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.HeaderText = "ID"
        TreeListViewColumn2.FooterRect = CType(resources.GetObject("TreeListViewColumn2.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.FooterText = "Footer 1"
        TreeListViewColumn2.HeaderRect = CType(resources.GetObject("TreeListViewColumn2.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.HeaderText = "Long Name"
        TreeListViewColumn2.Width = 70
        TreeListViewColumn3.FooterRect = CType(resources.GetObject("TreeListViewColumn3.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn3.FooterText = "Footer 2"
        TreeListViewColumn3.HeaderRect = CType(resources.GetObject("TreeListViewColumn3.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn3.HeaderText = "DB Name"
        TreeListViewColumn3.Width = 70
        TreeListViewColumn4.FooterRect = CType(resources.GetObject("TreeListViewColumn4.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn4.FooterText = "Footer 3"
        TreeListViewColumn4.HeaderRect = CType(resources.GetObject("TreeListViewColumn4.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn4.HeaderText = "Object"
        TreeListViewColumn5.FooterRect = CType(resources.GetObject("TreeListViewColumn5.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn5.FooterText = "Footer 4"
        TreeListViewColumn5.HeaderRect = CType(resources.GetObject("TreeListViewColumn5.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn5.HeaderText = "Range Steps"
        TreeListViewColumn5.Width = 90
        TreeListViewColumn6.FooterRect = CType(resources.GetObject("TreeListViewColumn6.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn6.FooterText = "Footer 5"
        TreeListViewColumn6.HeaderRect = CType(resources.GetObject("TreeListViewColumn6.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn6.HeaderText = "Conv_Int_Val"
        TreeListViewColumn6.Width = 90
        Me.tlvParameter.Columns.AddRange(New Object() {TreeListViewColumn1, TreeListViewColumn2, TreeListViewColumn3, TreeListViewColumn4, TreeListViewColumn5, TreeListViewColumn6})
        '
        '
        '
        Me.tlvParameter.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvParameter.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvParameter.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvParameter.ContentPanel.Name = ""
        Me.tlvParameter.ContentPanel.Size = New System.Drawing.Size(454, 552)
        Me.tlvParameter.ContentPanel.TabIndex = 3
        Me.tlvParameter.ContentPanel.TabStop = False
        Me.tlvParameter.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvParameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvParameter.ExpandingColumn = TreeListViewColumn1
        Me.tlvParameter.Footer = False
        Me.tlvParameter.Location = New System.Drawing.Point(4, 104)
        Me.tlvParameter.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvParameter.Name = "tlvParameter"
        Me.tlvParameter.Size = New System.Drawing.Size(460, 558)
        Me.tlvParameter.TabIndex = 6
        Me.tlvParameter.Text = "TreeListView2"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.TableLayoutPanel8)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(462, 94)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Parameter Search"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.LabelControl9, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.cmbFilterOnObject, 1, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.txtSearchLongName, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 3
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(458, 69)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Options.UseTextOptions = True
        Me.LabelControl9.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(4, 2, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl9.TabIndex = 3
        Me.LabelControl9.Text = "Filter On Object"
        '
        'cmbFilterOnObject
        '
        Me.cmbFilterOnObject.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbFilterOnObject.Location = New System.Drawing.Point(103, 29)
        Me.cmbFilterOnObject.Name = "cmbFilterOnObject"
        Me.cmbFilterOnObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFilterOnObject.Size = New System.Drawing.Size(256, 20)
        Me.cmbFilterOnObject.TabIndex = 8
        '
        'txtSearchLongName
        '
        Me.txtSearchLongName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearchLongName.Location = New System.Drawing.Point(103, 3)
        Me.txtSearchLongName.Name = "txtSearchLongName"
        Me.txtSearchLongName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchLongName.Properties.NullValuePrompt = "Search..."
        Me.txtSearchLongName.Size = New System.Drawing.Size(256, 20)
        Me.txtSearchLongName.TabIndex = 7
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Options.UseTextOptions = True
        Me.LabelControl8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(4, 2, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(94, 20)
        Me.LabelControl8.TabIndex = 2
        Me.LabelControl8.Text = "Search Long Name"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel7)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(492, 666)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Parameter List"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.tlvCMParamenter, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.LabelControl7, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(488, 641)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'tlvCMParamenter
        '
        Me.tlvCMParamenter.AllowDrag = True
        Me.tlvCMParamenter.AllowDrop = True
        TreeListViewColumn7.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible
        TreeListViewColumn7.FooterRect = CType(resources.GetObject("TreeListViewColumn7.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn7.FooterText = "Footer 1"
        TreeListViewColumn7.HeaderRect = CType(resources.GetObject("TreeListViewColumn7.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn7.HeaderText = "Name"
        TreeListViewColumn7.Width = 70
        TreeListViewColumn8.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible
        TreeListViewColumn8.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control
        TreeListViewColumn8.FooterRect = CType(resources.GetObject("TreeListViewColumn8.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn8.FooterText = "Footer 2"
        TreeListViewColumn8.HeaderRect = CType(resources.GetObject("TreeListViewColumn8.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn8.HeaderText = "Operator"
        TreeListViewColumn8.Width = 70
        TreeListViewColumn9.ContentControlVisibility = LidorSystems.IntegralUI.Lists.ContentControlVisibility.AlwaysVisible
        TreeListViewColumn9.ContentType = LidorSystems.IntegralUI.Lists.ColumnContentType.Control
        TreeListViewColumn9.FooterRect = CType(resources.GetObject("TreeListViewColumn9.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn9.FooterText = "Footer 3"
        TreeListViewColumn9.HeaderRect = CType(resources.GetObject("TreeListViewColumn9.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn9.HeaderText = "Value"
        Me.tlvCMParamenter.Columns.AddRange(New Object() {TreeListViewColumn7, TreeListViewColumn8, TreeListViewColumn9})
        '
        '
        '
        Me.tlvCMParamenter.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvCMParamenter.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvCMParamenter.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvCMParamenter.ContentPanel.Name = ""
        Me.tlvCMParamenter.ContentPanel.Size = New System.Drawing.Size(474, 548)
        Me.tlvCMParamenter.ContentPanel.TabIndex = 3
        Me.tlvCMParamenter.ContentPanel.TabStop = False
        Me.tlvCMParamenter.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvCMParamenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvCMParamenter.DragDropMode = LidorSystems.IntegralUI.DragDropMode.Custom
        Me.tlvCMParamenter.ExpandingColumn = TreeListViewColumn7
        Me.tlvCMParamenter.Footer = False
        Me.tlvCMParamenter.LabelEdit = True
        Me.tlvCMParamenter.Location = New System.Drawing.Point(4, 83)
        Me.tlvCMParamenter.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvCMParamenter.Name = "tlvCMParamenter"
        Me.tlvCMParamenter.Size = New System.Drawing.Size(480, 554)
        Me.tlvCMParamenter.TabIndex = 12
        Me.tlvCMParamenter.Text = "TreeListView1"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl7.Appearance.Options.UseBackColor = True
        Me.LabelControl7.Appearance.Options.UseTextOptions = True
        Me.LabelControl7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(482, 73)
        Me.LabelControl7.TabIndex = 11
        Me.LabelControl7.Text = "Step 1: Search and Select CM Parameter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 2: Drag and Drop onto Parameter list" &
    "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 3: Set Operator and Value" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 4: Commit!"
        '
        'xtpRegionBased
        '
        Me.xtpRegionBased.Controls.Add(Me.SplitContainerControl3)
        Me.xtpRegionBased.Name = "xtpRegionBased"
        Me.xtpRegionBased.Size = New System.Drawing.Size(976, 762)
        Me.xtpRegionBased.Tag = "RegionBased"
        Me.xtpRegionBased.Text = "Region Based"
        '
        'SplitContainerControl3
        '
        Me.SplitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl3.Name = "SplitContainerControl3"
        '
        'SplitContainerControl3.Panel1
        '
        Me.SplitContainerControl3.Panel1.Controls.Add(Me.TableLayoutPanel9)
        Me.SplitContainerControl3.Panel1.Text = "Panel1"
        '
        'SplitContainerControl3.Panel2
        '
        Me.SplitContainerControl3.Panel2.Controls.Add(Me.GroupControl4)
        Me.SplitContainerControl3.Panel2.MinSize = 300
        Me.SplitContainerControl3.Panel2.Text = "Panel2"
        Me.SplitContainerControl3.Size = New System.Drawing.Size(976, 762)
        Me.SplitContainerControl3.SplitterPosition = 653
        Me.SplitContainerControl3.TabIndex = 0
        Me.SplitContainerControl3.Text = "SplitContainerControl3"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.GroupControl6, 0, 1)
        Me.TableLayoutPanel9.Controls.Add(Me.GroupControl5, 0, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(653, 762)
        Me.TableLayoutPanel9.TabIndex = 0
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.TableLayoutPanel11)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(3, 384)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(647, 375)
        Me.GroupControl6.TabIndex = 1
        Me.GroupControl6.Text = "Import"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel11.ColumnCount = 1
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.LabelControl12, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel13, 0, 1)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 2
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(643, 350)
        Me.TableLayoutPanel11.TabIndex = 1
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Options.UseTextOptions = True
        Me.LabelControl12.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(637, 94)
        Me.LabelControl12.TabIndex = 13
        Me.LabelControl12.Text = "Build a Tag based on a Mapinfo TAB file" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 1: Select TAB from Map" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 2: S" &
    "elect the Column holding the Polygon Name" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 3: Press ""Import"""
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 3
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.btnRefreshTabFiles, 2, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.LabelControl13, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.LabelControl14, 0, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.cmbTabFile, 1, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.cmbRegionColumn, 1, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.btnImport, 2, 1)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(3, 103)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 3
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(637, 244)
        Me.TableLayoutPanel13.TabIndex = 14
        '
        'btnRefreshTabFiles
        '
        Me.btnRefreshTabFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefreshTabFiles.Location = New System.Drawing.Point(540, 3)
        Me.btnRefreshTabFiles.Name = "btnRefreshTabFiles"
        Me.btnRefreshTabFiles.Size = New System.Drawing.Size(94, 22)
        Me.btnRefreshTabFiles.TabIndex = 14
        Me.btnRefreshTabFiles.Text = "Refresh"
        '
        'LabelControl13
        '
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(114, 22)
        Me.LabelControl13.TabIndex = 15
        Me.LabelControl13.Text = "Select TAB File"
        '
        'LabelControl14
        '
        Me.LabelControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl14.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl14.Size = New System.Drawing.Size(114, 22)
        Me.LabelControl14.TabIndex = 19
        Me.LabelControl14.Text = "Select Region Column"
        '
        'cmbTabFile
        '
        Me.cmbTabFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTabFile.Location = New System.Drawing.Point(123, 3)
        Me.cmbTabFile.Name = "cmbTabFile"
        Me.cmbTabFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTabFile.Size = New System.Drawing.Size(411, 20)
        Me.cmbTabFile.TabIndex = 20
        '
        'cmbRegionColumn
        '
        Me.cmbRegionColumn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbRegionColumn.Location = New System.Drawing.Point(123, 31)
        Me.cmbRegionColumn.Name = "cmbRegionColumn"
        Me.cmbRegionColumn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRegionColumn.Size = New System.Drawing.Size(411, 20)
        Me.cmbRegionColumn.TabIndex = 21
        '
        'btnImport
        '
        Me.btnImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnImport.Location = New System.Drawing.Point(540, 31)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(94, 22)
        Me.btnImport.TabIndex = 18
        Me.btnImport.Text = "Import"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel10)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(647, 375)
        Me.GroupControl5.TabIndex = 0
        Me.GroupControl5.Text = "Manual"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.LabelControl10, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel12, 0, 1)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 2
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(643, 350)
        Me.TableLayoutPanel10.TabIndex = 0
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Options.UseTextOptions = True
        Me.LabelControl10.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl10.Size = New System.Drawing.Size(637, 94)
        Me.LabelControl10.TabIndex = 12
        Me.LabelControl10.Text = "Build a Tag based on a Map Polygon" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 1: Name the polygon" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 2: Press ""Dr" &
    "aw!""" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 3: In Map window, draw a polygon" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 4: Commit"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 4
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.LabelControl11, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.txtRegionName, 1, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnManualCommit, 3, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.togBtnDraw, 2, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(3, 103)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 2
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(637, 244)
        Me.TableLayoutPanel12.TabIndex = 13
        '
        'LabelControl11
        '
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(114, 24)
        Me.LabelControl11.TabIndex = 4
        Me.LabelControl11.Text = "Region Name"
        '
        'txtRegionName
        '
        Me.txtRegionName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRegionName.Location = New System.Drawing.Point(123, 3)
        Me.txtRegionName.Name = "txtRegionName"
        Me.txtRegionName.Size = New System.Drawing.Size(311, 20)
        Me.txtRegionName.TabIndex = 8
        '
        'btnManualCommit
        '
        Me.btnManualCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnManualCommit.Location = New System.Drawing.Point(540, 3)
        Me.btnManualCommit.Name = "btnManualCommit"
        Me.btnManualCommit.Size = New System.Drawing.Size(94, 24)
        Me.btnManualCommit.TabIndex = 13
        Me.btnManualCommit.Text = "Commit"
        '
        'togBtnDraw
        '
        Me.togBtnDraw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.togBtnDraw.Location = New System.Drawing.Point(440, 3)
        Me.togBtnDraw.LookAndFeel.SkinName = "McSkin"
        Me.togBtnDraw.LookAndFeel.UseDefaultLookAndFeel = False
        Me.togBtnDraw.Name = "togBtnDraw"
        Me.togBtnDraw.Size = New System.Drawing.Size(94, 24)
        Me.togBtnDraw.TabIndex = 14
        Me.togBtnDraw.Text = "Draw"
        Me.togBtnDraw.ToggleState = System.Windows.Forms.CheckState.Unchecked
        '
        'GroupControl4
        '
        Me.GroupControl4.Appearance.BackColor = System.Drawing.Color.White
        Me.GroupControl4.Appearance.Options.UseBackColor = True
        Me.GroupControl4.Controls.Add(Me.tlvRegionList)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(313, 762)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "Region List"
        '
        'tlvRegionList
        '
        Me.tlvRegionList.AllowDrag = True
        '
        '
        '
        Me.tlvRegionList.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvRegionList.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvRegionList.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvRegionList.ContentPanel.Name = ""
        Me.tlvRegionList.ContentPanel.Size = New System.Drawing.Size(303, 731)
        Me.tlvRegionList.ContentPanel.TabIndex = 3
        Me.tlvRegionList.ContentPanel.TabStop = False
        Me.tlvRegionList.ContextMenuStrip = Me.cms_RegionBase
        Me.tlvRegionList.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvRegionList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvRegionList.Footer = False
        Me.tlvRegionList.Location = New System.Drawing.Point(2, 23)
        Me.tlvRegionList.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvRegionList.Name = "tlvRegionList"
        Me.tlvRegionList.Size = New System.Drawing.Size(309, 737)
        Me.tlvRegionList.TabIndex = 7
        Me.tlvRegionList.Text = "TreeListView1"
        '
        'cms_RegionBase
        '
        Me.cms_RegionBase.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RegionEdit, Me.tsmi_RegionDelete})
        Me.cms_RegionBase.Name = "cm_TagManagement"
        Me.cms_RegionBase.Size = New System.Drawing.Size(162, 48)
        '
        'tsmi_RegionEdit
        '
        Me.tsmi_RegionEdit.Name = "tsmi_RegionEdit"
        Me.tsmi_RegionEdit.Size = New System.Drawing.Size(161, 22)
        Me.tsmi_RegionEdit.Text = "Region  -  Edit"
        '
        'tsmi_RegionDelete
        '
        Me.tsmi_RegionDelete.Name = "tsmi_RegionDelete"
        Me.tsmi_RegionDelete.Size = New System.Drawing.Size(161, 22)
        Me.tsmi_RegionDelete.Text = "Region  -  Delete"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'cms_CMParameter
        '
        Me.cms_CMParameter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_Edit, Me.tsmi_Delete})
        Me.cms_CMParameter.Name = "cm_TagManagement"
        Me.cms_CMParameter.Size = New System.Drawing.Size(179, 48)
        '
        'tsmi_Edit
        '
        Me.tsmi_Edit.Name = "tsmi_Edit"
        Me.tsmi_Edit.Size = New System.Drawing.Size(178, 22)
        Me.tsmi_Edit.Text = "Parameter  -  Edit"
        '
        'tsmi_Delete
        '
        Me.tsmi_Delete.Name = "tsmi_Delete"
        Me.tsmi_Delete.Size = New System.Drawing.Size(178, 22)
        Me.tsmi_Delete.Text = "Parameter  -  Delete"
        '
        'frmTagManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1394, 818)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.Icon = CType(resources.GetObject("frmTagManager.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1396, 850)
        Me.Name = "frmTagManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tag Manager"
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.gcTagsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_TagManagement.ResumeLayout(False)
        CType(Me.gvTagsList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbObjectType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTagType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.xtcTagManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcTagManager.ResumeLayout(False)
        Me.xtpListManager.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.gcTags, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_TagPaste.ResumeLayout(False)
        CType(Me.gvTags, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpCMBased.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.tlvParameter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.cmbFilterOnObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchLongName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        CType(Me.tlvCMParamenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpRegionBased.ResumeLayout(False)
        CType(Me.SplitContainerControl3.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl3.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        CType(Me.cmbTabFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRegionColumn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        CType(Me.txtRegionName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.tlvRegionList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cms_RegionBase.ResumeLayout(False)
        Me.cms_CMParameter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnTagInsert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents xtcTagManager As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpListManager As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnTagSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xtpCMBased As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpRegionBased As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnLoadParameter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnInsertCMBased As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainerControl3 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtRegionName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnManualCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel13 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnRefreshTabFiles As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnImport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cm_TagManagement As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_AddTag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_DeleteTag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_RenameTag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_PreAggregration As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cm_TagPaste As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_TagPaste_Paste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cms_RegionBase As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_RegionEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_RegionDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cms_CMParameter As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_Edit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlvRegionList As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents tlvParameter As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbObjectType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbFilterOnObject As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTabFile As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbRegionColumn As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlvCMParamenter As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents cmbTagType As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents gcTags As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTags As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtSearchLongName As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents togBtnDraw As Library.IOSToggleButton
    Friend WithEvents gcTagsList As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTagsList As DevExpress.XtraGrid.Views.Grid.GridView
End Class
