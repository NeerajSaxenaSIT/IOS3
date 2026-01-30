<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCMView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCMView))
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tvObjectTree = New DevExpress.XtraTreeList.TreeList()
        Me.cmObjectTree = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cm_OT_tsmi_copy = New System.Windows.Forms.ToolStripMenuItem()
        Me.cm_OT_tsmi_paste = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cm_OT_tsmi_CheckChilds = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_OT_UnCheck = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbVendor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblVendor = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTargetObject = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbTemplate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnGetTemplate = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.lstViewMO = New DevExpress.XtraTreeList.TreeList()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceLoadObjectTree = New DevExpress.XtraEditors.CheckEdit()
        Me.txtSearchMO = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcParamFilter = New DevExpress.XtraGrid.GridControl()
        Me.cmsGrid = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RecordCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_CompareVsSelection = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_FreezeColumn = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_FreezeRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_GetChangesForMO = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_GetChangesForSelection = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_AllowCellCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_CopySelection = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_CopySelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvParamFilter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.tlvFilters = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlParameterList = New DevExpress.XtraTreeList.TreeList()
        Me.cmsParamList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_ParamDescParam = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSearchAllParameter = New DevExpress.XtraEditors.CheckEdit()
        Me.txtSearchPH = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.gcObject = New DevExpress.XtraGrid.GridControl()
        Me.gvObject = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.sccGrids = New DevExpress.XtraEditors.SplitContainerControl()
        Me.xtcBottom = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpCurrSettings = New DevExpress.XtraTab.XtraTabPage()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.gcCMView = New DevExpress.XtraGrid.GridControl()
        Me.gvCMView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpCurrentSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDump2Xls = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.btnDump2Csv = New DevExpress.XtraEditors.SimpleButton()
        Me.lblQueryBatchSize = New DevExpress.XtraEditors.LabelControl()
        Me.txtQueryBatchSize = New DevExpress.XtraEditors.TextEdit()
        Me.xtpParamDesc = New DevExpress.XtraTab.XtraTabPage()
        Me.gcParamDesc = New DevExpress.XtraGrid.GridControl()
        Me.gvParamDesc = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.gcHistChanges = New DevExpress.XtraGrid.GridControl()
        Me.cmsHistoryChanges = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_histChangesRecordCount = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_HistChangesAllowCellCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_HistChangesCopySelection = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_HistChangesCopySelectionWithHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvHistChanges = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ImgList = New DevExpress.Utils.ImageCollection(Me.components)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.tvObjectTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmObjectTree.SuspendLayout()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.lstViewMO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel15.SuspendLayout()
        CType(Me.ceLoadObjectTree.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.gcParamFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsGrid.SuspendLayout()
        CType(Me.gvParamFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.tlvFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.tlParameterList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsParamList.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.chkSearchAllParameter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchPH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.gcObject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvObject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccGrids, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccGrids.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccGrids.Panel1.SuspendLayout()
        CType(Me.sccGrids.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccGrids.Panel2.SuspendLayout()
        Me.sccGrids.SuspendLayout()
        CType(Me.xtcBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtcBottom.SuspendLayout()
        Me.xtpCurrSettings.SuspendLayout()
        Me.tlpBottom.SuspendLayout()
        CType(Me.gcCMView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCMView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpCurrentSettings.SuspendLayout()
        CType(Me.txtQueryBatchSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpParamDesc.SuspendLayout()
        CType(Me.gcParamDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParamDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel14.SuspendLayout()
        CType(Me.gcHistChanges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsHistoryChanges.SuspendLayout()
        CType(Me.gvHistChanges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgList, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainerControl1.Panel1.MinSize = 250
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.SplitContainerControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1240, 733)
        Me.SplitContainerControl1.SplitterPosition = 230
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tvObjectTree, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbTechnology, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbVendor, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblVendor, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbTargetObject, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl2, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl5, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl4, 0, 8)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(250, 733)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'tvObjectTree
        '
        Me.tvObjectTree.ActiveFilterEnabled = False
        Me.tvObjectTree.ContextMenuStrip = Me.cmObjectTree
        Me.tvObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvObjectTree.KeyFieldName = "NENAME"
        Me.tvObjectTree.Location = New System.Drawing.Point(3, 246)
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
        Me.tvObjectTree.OptionsFilter.ShowAllValuesInFilterPopup = True
        Me.tvObjectTree.OptionsFind.AllowIncrementalSearch = True
        Me.tvObjectTree.OptionsFind.AlwaysVisible = True
        Me.tvObjectTree.OptionsFind.ExpandNodesOnIncrementalSearch = True
        Me.tvObjectTree.OptionsFind.FindFilterColumns = "NENAME"
        Me.tvObjectTree.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always
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
        Me.tvObjectTree.Size = New System.Drawing.Size(244, 484)
        Me.tvObjectTree.TabIndex = 16
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
        Me.cm_OT_tsmi_CheckChilds.Name = "cm_OT_tsmi_CheckChilds"
        Me.cm_OT_tsmi_CheckChilds.Size = New System.Drawing.Size(155, 22)
        Me.cm_OT_tsmi_CheckChilds.Text = "Check Children"
        '
        'tsmi_OT_UnCheck
        '
        Me.tsmi_OT_UnCheck.Name = "tsmi_OT_UnCheck"
        Me.tsmi_OT_UnCheck.Size = New System.Drawing.Size(155, 22)
        Me.tsmi_OT_UnCheck.Text = "UnCheck All"
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
        Me.cmbTargetObject.Location = New System.Drawing.Point(3, 192)
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
        Me.LabelControl2.Location = New System.Drawing.Point(3, 167)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(244, 19)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Select Target Object Type"
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 111)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(244, 21)
        Me.LabelControl5.TabIndex = 13
        Me.LabelControl5.Text = "Template Selection"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.cmbTemplate, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnGetTemplate, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(1, 136)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(248, 27)
        Me.TableLayoutPanel2.TabIndex = 14
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTemplate.EditValue = "Select Template"
        Me.cmbTemplate.Location = New System.Drawing.Point(3, 3)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTemplate.Properties.Sorted = True
        Me.cmbTemplate.Size = New System.Drawing.Size(192, 20)
        Me.cmbTemplate.TabIndex = 0
        '
        'btnGetTemplate
        '
        Me.btnGetTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGetTemplate.Location = New System.Drawing.Point(201, 3)
        Me.btnGetTemplate.Name = "btnGetTemplate"
        Me.btnGetTemplate.Size = New System.Drawing.Size(44, 21)
        Me.btnGetTemplate.TabIndex = 1
        Me.btnGetTemplate.Text = "Get"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 219)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(244, 21)
        Me.LabelControl4.TabIndex = 7
        Me.LabelControl4.Text = "Object Tree"
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
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.sccGrids)
        Me.SplitContainerControl2.Panel2.MinSize = 300
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(980, 733)
        Me.SplitContainerControl2.SplitterPosition = 431
        Me.SplitContainerControl2.TabIndex = 1
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.GroupControl1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.GroupControl5, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(980, 423)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.Appearance.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel8)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(317, 417)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "MO Selection"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.lstViewMO, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.TableLayoutPanel15, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(313, 392)
        Me.TableLayoutPanel8.TabIndex = 1
        '
        'lstViewMO
        '
        Me.lstViewMO.AllowDrop = True
        Me.lstViewMO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstViewMO.Location = New System.Drawing.Point(3, 30)
        Me.lstViewMO.Name = "lstViewMO"
        Me.lstViewMO.OptionsBehavior.AllowExpandOnDblClick = False
        Me.lstViewMO.OptionsBehavior.Editable = False
        Me.lstViewMO.OptionsBehavior.ReadOnly = True
        Me.lstViewMO.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[False]
        Me.lstViewMO.OptionsCustomization.AllowBandMoving = False
        Me.lstViewMO.OptionsCustomization.AllowBandResizing = False
        Me.lstViewMO.OptionsCustomization.AllowColumnMoving = False
        Me.lstViewMO.OptionsCustomization.AllowColumnResizing = False
        Me.lstViewMO.OptionsCustomization.AllowQuickHideColumns = False
        Me.lstViewMO.OptionsMenu.EnableColumnMenu = False
        Me.lstViewMO.OptionsMenu.EnableFooterMenu = False
        Me.lstViewMO.OptionsMenu.ShowAutoFilterRowItem = False
        Me.lstViewMO.OptionsNavigation.MoveOnEdit = False
        Me.lstViewMO.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.lstViewMO.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible
        Me.lstViewMO.OptionsView.ExpandButtonCentered = False
        Me.lstViewMO.OptionsView.ShowButtons = False
        Me.lstViewMO.OptionsView.ShowHorzLines = False
        Me.lstViewMO.OptionsView.ShowIndicator = False
        Me.lstViewMO.OptionsView.ShowRoot = False
        Me.lstViewMO.OptionsView.ShowVertLines = False
        Me.lstViewMO.Size = New System.Drawing.Size(307, 359)
        Me.lstViewMO.TabIndex = 3
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 2
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.ceLoadObjectTree, 1, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.txtSearchMO, 0, 0)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 1
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(313, 27)
        Me.TableLayoutPanel15.TabIndex = 4
        '
        'ceLoadObjectTree
        '
        Me.ceLoadObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceLoadObjectTree.EditValue = True
        Me.ceLoadObjectTree.Location = New System.Drawing.Point(206, 3)
        Me.ceLoadObjectTree.Name = "ceLoadObjectTree"
        Me.ceLoadObjectTree.Properties.Caption = "Load Object Tree"
        Me.ceLoadObjectTree.Size = New System.Drawing.Size(104, 21)
        Me.ceLoadObjectTree.TabIndex = 2
        '
        'txtSearchMO
        '
        Me.txtSearchMO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchMO.Location = New System.Drawing.Point(3, 3)
        Me.txtSearchMO.Name = "txtSearchMO"
        Me.txtSearchMO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchMO.Properties.NullValuePrompt = "Search..."
        Me.txtSearchMO.Size = New System.Drawing.Size(197, 20)
        Me.txtSearchMO.TabIndex = 1
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel11)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(649, 3)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(328, 417)
        Me.GroupControl5.TabIndex = 3
        Me.GroupControl5.Text = "Parameter Selection"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 1
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.gcParamFilter, 0, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel7, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.GroupControl6, 0, 2)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 3
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(324, 392)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'gcParamFilter
        '
        Me.gcParamFilter.AllowDrop = True
        Me.gcParamFilter.ContextMenuStrip = Me.cmsGrid
        Me.gcParamFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcParamFilter.Location = New System.Drawing.Point(1, 33)
        Me.gcParamFilter.MainView = Me.gvParamFilter
        Me.gcParamFilter.Margin = New System.Windows.Forms.Padding(1)
        Me.gcParamFilter.Name = "gcParamFilter"
        Me.gcParamFilter.Size = New System.Drawing.Size(322, 178)
        Me.gcParamFilter.TabIndex = 6
        Me.gcParamFilter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvParamFilter, Me.GridView3})
        '
        'cmsGrid
        '
        Me.cmsGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RecordCount, Me.ToolStripSeparator4, Me.tsmi_CompareVsSelection, Me.ToolStripSeparator1, Me.tsmi_FreezeColumn, Me.tsmi_FreezeRow, Me.ToolStripSeparator2, Me.tsmi_GetChangesForMO, Me.tsmi_GetChangesForSelection, Me.ToolStripSeparator3, Me.tsmi_AllowCellCopy, Me.tsmi_CopySelection, Me.tsmi_CopySelectionWithHeader})
        Me.cmsGrid.Name = "cmsGrid"
        Me.cmsGrid.Size = New System.Drawing.Size(241, 226)
        '
        'tsmi_RecordCount
        '
        Me.tsmi_RecordCount.Name = "tsmi_RecordCount"
        Me.tsmi_RecordCount.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_RecordCount.Text = "Record Count:"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(237, 6)
        '
        'tsmi_CompareVsSelection
        '
        Me.tsmi_CompareVsSelection.Name = "tsmi_CompareVsSelection"
        Me.tsmi_CompareVsSelection.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_CompareVsSelection.Text = "Compare vs Selection"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(237, 6)
        '
        'tsmi_FreezeColumn
        '
        Me.tsmi_FreezeColumn.Name = "tsmi_FreezeColumn"
        Me.tsmi_FreezeColumn.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_FreezeColumn.Text = "Freeze Column"
        '
        'tsmi_FreezeRow
        '
        Me.tsmi_FreezeRow.Name = "tsmi_FreezeRow"
        Me.tsmi_FreezeRow.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_FreezeRow.Text = "Freeze Row"
        Me.tsmi_FreezeRow.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(237, 6)
        '
        'tsmi_GetChangesForMO
        '
        Me.tsmi_GetChangesForMO.Name = "tsmi_GetChangesForMO"
        Me.tsmi_GetChangesForMO.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_GetChangesForMO.Text = "Get Changes for MO"
        '
        'tsmi_GetChangesForSelection
        '
        Me.tsmi_GetChangesForSelection.Name = "tsmi_GetChangesForSelection"
        Me.tsmi_GetChangesForSelection.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_GetChangesForSelection.Text = "Get Changes for Selection"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(237, 6)
        '
        'tsmi_AllowCellCopy
        '
        Me.tsmi_AllowCellCopy.CheckOnClick = True
        Me.tsmi_AllowCellCopy.Name = "tsmi_AllowCellCopy"
        Me.tsmi_AllowCellCopy.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_AllowCellCopy.Text = "Allow Cell Copy"
        '
        'tsmi_CopySelection
        '
        Me.tsmi_CopySelection.Name = "tsmi_CopySelection"
        Me.tsmi_CopySelection.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_CopySelection.Text = "Copy Selection Without Header"
        '
        'tsmi_CopySelectionWithHeader
        '
        Me.tsmi_CopySelectionWithHeader.Name = "tsmi_CopySelectionWithHeader"
        Me.tsmi_CopySelectionWithHeader.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_CopySelectionWithHeader.Text = "Copy Selection With Header"
        '
        'gvParamFilter
        '
        Me.gvParamFilter.ActiveFilterEnabled = False
        Me.gvParamFilter.GridControl = Me.gcParamFilter
        Me.gvParamFilter.Name = "gvParamFilter"
        Me.gvParamFilter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvParamFilter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvParamFilter.OptionsBehavior.Editable = False
        Me.gvParamFilter.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamFilter.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamFilter.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamFilter.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvParamFilter.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamFilter.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamFilter.OptionsCustomization.AllowColumnMoving = False
        Me.gvParamFilter.OptionsCustomization.AllowColumnResizing = False
        Me.gvParamFilter.OptionsCustomization.AllowFilter = False
        Me.gvParamFilter.OptionsCustomization.AllowGroup = False
        Me.gvParamFilter.OptionsCustomization.AllowQuickHideColumns = False
        Me.gvParamFilter.OptionsCustomization.AllowSort = False
        Me.gvParamFilter.OptionsMenu.EnableColumnMenu = False
        Me.gvParamFilter.OptionsMenu.EnableFooterMenu = False
        Me.gvParamFilter.OptionsMenu.EnableGroupPanelMenu = False
        Me.gvParamFilter.OptionsMenu.ShowAutoFilterRowItem = False
        Me.gvParamFilter.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.gvParamFilter.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.gvParamFilter.OptionsMenu.ShowSplitItem = False
        Me.gvParamFilter.OptionsNavigation.AutoMoveRowFocus = False
        Me.gvParamFilter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvParamFilter.OptionsSelection.UseIndicatorForSelection = False
        Me.gvParamFilter.OptionsView.AllowHtmlDrawGroups = False
        Me.gvParamFilter.OptionsView.ColumnAutoWidth = False
        Me.gvParamFilter.OptionsView.ShowColumnHeaders = False
        Me.gvParamFilter.OptionsView.ShowDetailButtons = False
        Me.gvParamFilter.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.gvParamFilter.OptionsView.ShowGroupPanel = False
        Me.gvParamFilter.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvParamFilter.OptionsView.ShowIndicator = False
        Me.gvParamFilter.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvParamFilter.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.gcParamFilter
        Me.GridView3.Name = "GridView3"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.btnClear, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.LabelControl6, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(324, 32)
        Me.TableLayoutPanel7.TabIndex = 4
        '
        'btnClear
        '
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClear.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnClear.Location = New System.Drawing.Point(276, 2)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(46, 28)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "Clear"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 7.7!)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(268, 26)
        Me.LabelControl6.TabIndex = 3
        Me.LabelControl6.Text = "1. Drag parameter to include in grid" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2. Check parameter to include in comparison" &
    ""
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.TableLayoutPanel12)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(1, 213)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(322, 178)
        Me.GroupControl6.TabIndex = 5
        Me.GroupControl6.Text = "Parameter Filter"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 1
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.tlvFilters, 0, 1)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 2
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(318, 153)
        Me.TableLayoutPanel12.TabIndex = 0
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 7.7!)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(312, 28)
        Me.LabelControl8.TabIndex = 3
        Me.LabelControl8.Text = "1. Drag parameter to filter at query time" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2. Set operators like (>, <, =, <>) an" &
    "d value"
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
        Me.tlvFilters.ContentPanel.Size = New System.Drawing.Size(310, 111)
        Me.tlvFilters.ContentPanel.TabIndex = 3
        Me.tlvFilters.ContentPanel.TabStop = False
        Me.tlvFilters.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvFilters.Footer = False
        Me.tlvFilters.Location = New System.Drawing.Point(1, 35)
        Me.tlvFilters.Margin = New System.Windows.Forms.Padding(1)
        Me.tlvFilters.Name = "tlvFilters"
        Me.tlvFilters.Size = New System.Drawing.Size(316, 117)
        Me.tlvFilters.TabIndex = 4
        Me.tlvFilters.Text = "TreeListView1"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.GroupControl2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.GroupControl3, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(323, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(323, 423)
        Me.TableLayoutPanel3.TabIndex = 4
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel10)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(317, 247)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Parameter Settings"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.56291!))
        Me.TableLayoutPanel10.Controls.Add(Me.tlParameterList, 0, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 2
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(313, 222)
        Me.TableLayoutPanel10.TabIndex = 2
        '
        'tlParameterList
        '
        Me.tlParameterList.AllowDrop = True
        Me.tlParameterList.ContextMenuStrip = Me.cmsParamList
        Me.tlParameterList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlParameterList.Location = New System.Drawing.Point(3, 30)
        Me.tlParameterList.Name = "tlParameterList"
        Me.tlParameterList.OptionsBehavior.AllowExpandOnDblClick = False
        Me.tlParameterList.OptionsBehavior.Editable = False
        Me.tlParameterList.OptionsBehavior.ReadOnly = True
        Me.tlParameterList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[False]
        Me.tlParameterList.OptionsCustomization.AllowBandMoving = False
        Me.tlParameterList.OptionsCustomization.AllowBandResizing = False
        Me.tlParameterList.OptionsCustomization.AllowColumnMoving = False
        Me.tlParameterList.OptionsCustomization.AllowQuickHideColumns = False
        Me.tlParameterList.OptionsMenu.EnableColumnMenu = False
        Me.tlParameterList.OptionsMenu.EnableFooterMenu = False
        Me.tlParameterList.OptionsMenu.ShowAutoFilterRowItem = False
        Me.tlParameterList.OptionsNavigation.MoveOnEdit = False
        Me.tlParameterList.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.tlParameterList.OptionsSelection.MultiSelect = True
        Me.tlParameterList.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible
        Me.tlParameterList.OptionsView.ExpandButtonCentered = False
        Me.tlParameterList.OptionsView.ShowButtons = False
        Me.tlParameterList.OptionsView.ShowHorzLines = False
        Me.tlParameterList.OptionsView.ShowIndicator = False
        Me.tlParameterList.OptionsView.ShowRoot = False
        Me.tlParameterList.OptionsView.ShowVertLines = False
        Me.tlParameterList.Size = New System.Drawing.Size(307, 189)
        Me.tlParameterList.TabIndex = 0
        '
        'cmsParamList
        '
        Me.cmsParamList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_ParamDescParam})
        Me.cmsParamList.Name = "cm_ObjectTree"
        Me.cmsParamList.Size = New System.Drawing.Size(192, 26)
        '
        'tsmi_ParamDescParam
        '
        Me.tsmi_ParamDescParam.Name = "tsmi_ParamDescParam"
        Me.tsmi_ParamDescParam.Size = New System.Drawing.Size(191, 22)
        Me.tsmi_ParamDescParam.Text = "Parameter Description"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.chkSearchAllParameter, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txtSearchPH, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(313, 27)
        Me.TableLayoutPanel5.TabIndex = 5
        '
        'chkSearchAllParameter
        '
        Me.chkSearchAllParameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkSearchAllParameter.Location = New System.Drawing.Point(182, 3)
        Me.chkSearchAllParameter.Name = "chkSearchAllParameter"
        Me.chkSearchAllParameter.Properties.Caption = "Search All Parameters"
        Me.chkSearchAllParameter.Size = New System.Drawing.Size(128, 21)
        Me.chkSearchAllParameter.TabIndex = 1
        '
        'txtSearchPH
        '
        Me.txtSearchPH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchPH.Location = New System.Drawing.Point(3, 3)
        Me.txtSearchPH.Name = "txtSearchPH"
        Me.txtSearchPH.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchPH.Properties.NullValuePrompt = "Search..."
        Me.txtSearchPH.Size = New System.Drawing.Size(173, 20)
        Me.txtSearchPH.TabIndex = 0
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.gcObject)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(3, 256)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(317, 164)
        Me.GroupControl3.TabIndex = 5
        Me.GroupControl3.Text = "Object Identifiers"
        '
        'gcObject
        '
        Me.gcObject.AllowDrop = True
        Me.gcObject.ContextMenuStrip = Me.cmsGrid
        Me.gcObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcObject.Location = New System.Drawing.Point(2, 23)
        Me.gcObject.MainView = Me.gvObject
        Me.gcObject.Name = "gcObject"
        Me.gcObject.Size = New System.Drawing.Size(313, 139)
        Me.gcObject.TabIndex = 2
        Me.gcObject.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvObject, Me.GridView4})
        '
        'gvObject
        '
        Me.gvObject.ActiveFilterEnabled = False
        Me.gvObject.GridControl = Me.gcObject
        Me.gvObject.Name = "gvObject"
        Me.gvObject.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvObject.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvObject.OptionsBehavior.Editable = False
        Me.gvObject.OptionsBehavior.ReadOnly = True
        Me.gvObject.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObject.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObject.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObject.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvObject.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObject.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvObject.OptionsFilter.ShowAllTableValuesInFilterPopup = True
        Me.gvObject.OptionsFilter.UseNewCustomFilterDialog = True
        Me.gvObject.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvObject.OptionsSelection.MultiSelect = True
        Me.gvObject.OptionsView.ShowAutoFilterRow = True
        Me.gvObject.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.gcObject
        Me.GridView4.Name = "GridView4"
        '
        'sccGrids
        '
        Me.sccGrids.Collapsed = True
        Me.sccGrids.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.sccGrids.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccGrids.Horizontal = False
        Me.sccGrids.Location = New System.Drawing.Point(0, 0)
        Me.sccGrids.Name = "sccGrids"
        '
        'sccGrids.Panel1
        '
        Me.sccGrids.Panel1.Controls.Add(Me.xtcBottom)
        Me.sccGrids.Panel1.Text = "Panel1"
        '
        'sccGrids.Panel2
        '
        Me.sccGrids.Panel2.Controls.Add(Me.TableLayoutPanel14)
        Me.sccGrids.Panel2.MinSize = 150
        Me.sccGrids.Panel2.Text = "Panel2"
        Me.sccGrids.Size = New System.Drawing.Size(980, 300)
        Me.sccGrids.SplitterPosition = 145
        Me.sccGrids.TabIndex = 2
        Me.sccGrids.Text = "SplitContainerControl3"
        '
        'xtcBottom
        '
        Me.xtcBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtcBottom.Location = New System.Drawing.Point(0, 0)
        Me.xtcBottom.Name = "xtcBottom"
        Me.xtcBottom.SelectedTabPage = Me.xtpCurrSettings
        Me.xtcBottom.Size = New System.Drawing.Size(980, 290)
        Me.xtcBottom.TabIndex = 1
        Me.xtcBottom.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpCurrSettings, Me.xtpParamDesc})
        '
        'xtpCurrSettings
        '
        Me.xtpCurrSettings.Controls.Add(Me.tlpBottom)
        Me.xtpCurrSettings.Name = "xtpCurrSettings"
        Me.xtpCurrSettings.Size = New System.Drawing.Size(978, 265)
        Me.xtpCurrSettings.Text = "Current Settings"
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 1
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Controls.Add(Me.gcCMView, 0, 1)
        Me.tlpBottom.Controls.Add(Me.tlpCurrentSettings, 0, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(0, 0)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 2
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(978, 265)
        Me.tlpBottom.TabIndex = 0
        '
        'gcCMView
        '
        Me.gcCMView.AllowDrop = True
        Me.gcCMView.ContextMenuStrip = Me.cmsGrid
        Me.gcCMView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCMView.Location = New System.Drawing.Point(3, 35)
        Me.gcCMView.MainView = Me.gvCMView
        Me.gcCMView.Name = "gcCMView"
        Me.gcCMView.Size = New System.Drawing.Size(972, 227)
        Me.gcCMView.TabIndex = 1
        Me.gcCMView.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCMView, Me.GridView1})
        '
        'gvCMView
        '
        Me.gvCMView.ActiveFilterEnabled = False
        Me.gvCMView.GridControl = Me.gcCMView
        Me.gvCMView.Name = "gvCMView"
        Me.gvCMView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCMView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCMView.OptionsBehavior.Editable = False
        Me.gvCMView.OptionsBehavior.ReadOnly = True
        Me.gvCMView.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCMView.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCMView.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCMView.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvCMView.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCMView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCMView.OptionsFilter.ShowAllTableValuesInFilterPopup = True
        Me.gvCMView.OptionsFilter.UseNewCustomFilterDialog = True
        Me.gvCMView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvCMView.OptionsSelection.MultiSelect = True
        Me.gvCMView.OptionsView.ShowAutoFilterRow = True
        Me.gvCMView.OptionsView.ShowGroupPanel = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcCMView
        Me.GridView1.Name = "GridView1"
        '
        'tlpCurrentSettings
        '
        Me.tlpCurrentSettings.ColumnCount = 5
        Me.tlpCurrentSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCurrentSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpCurrentSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpCurrentSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpCurrentSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpCurrentSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpCurrentSettings.Controls.Add(Me.btnDump2Xls, 4, 0)
        Me.tlpCurrentSettings.Controls.Add(Me.LabelControl9, 0, 0)
        Me.tlpCurrentSettings.Controls.Add(Me.btnDump2Csv, 3, 0)
        Me.tlpCurrentSettings.Controls.Add(Me.lblQueryBatchSize, 1, 0)
        Me.tlpCurrentSettings.Controls.Add(Me.txtQueryBatchSize, 2, 0)
        Me.tlpCurrentSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCurrentSettings.Location = New System.Drawing.Point(1, 1)
        Me.tlpCurrentSettings.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpCurrentSettings.Name = "tlpCurrentSettings"
        Me.tlpCurrentSettings.RowCount = 1
        Me.tlpCurrentSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCurrentSettings.Size = New System.Drawing.Size(976, 30)
        Me.tlpCurrentSettings.TabIndex = 2
        '
        'btnDump2Xls
        '
        Me.btnDump2Xls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDump2Xls.ImageOptions.Image = CType(resources.GetObject("btnDump2Xls.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDump2Xls.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnDump2Xls.Location = New System.Drawing.Point(908, 2)
        Me.btnDump2Xls.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDump2Xls.Name = "btnDump2Xls"
        Me.btnDump2Xls.Size = New System.Drawing.Size(66, 26)
        Me.btnDump2Xls.TabIndex = 2
        Me.btnDump2Xls.Text = "XLSX"
        '
        'LabelControl9
        '
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(650, 24)
        Me.LabelControl9.TabIndex = 2
        Me.LabelControl9.Text = "Current Settings"
        '
        'btnDump2Csv
        '
        Me.btnDump2Csv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDump2Csv.ImageOptions.Image = CType(resources.GetObject("btnDump2Csv.ImageOptions.Image"), System.Drawing.Image)
        Me.btnDump2Csv.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnDump2Csv.Location = New System.Drawing.Point(838, 2)
        Me.btnDump2Csv.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDump2Csv.Name = "btnDump2Csv"
        Me.btnDump2Csv.Size = New System.Drawing.Size(66, 26)
        Me.btnDump2Csv.TabIndex = 1
        Me.btnDump2Csv.Text = "CSV"
        '
        'lblQueryBatchSize
        '
        Me.lblQueryBatchSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblQueryBatchSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblQueryBatchSize.Location = New System.Drawing.Point(659, 3)
        Me.lblQueryBatchSize.Name = "lblQueryBatchSize"
        Me.lblQueryBatchSize.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblQueryBatchSize.Size = New System.Drawing.Size(94, 24)
        Me.lblQueryBatchSize.TabIndex = 0
        Me.lblQueryBatchSize.Text = "Query batch size:"
        '
        'txtQueryBatchSize
        '
        Me.txtQueryBatchSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtQueryBatchSize.EditValue = "1000"
        Me.txtQueryBatchSize.Location = New System.Drawing.Point(759, 5)
        Me.txtQueryBatchSize.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.txtQueryBatchSize.Name = "txtQueryBatchSize"
        Me.txtQueryBatchSize.Size = New System.Drawing.Size(74, 20)
        Me.txtQueryBatchSize.TabIndex = 1
        '
        'xtpParamDesc
        '
        Me.xtpParamDesc.Controls.Add(Me.gcParamDesc)
        Me.xtpParamDesc.Name = "xtpParamDesc"
        Me.xtpParamDesc.Size = New System.Drawing.Size(984, 271)
        Me.xtpParamDesc.Text = "Parameter Description"
        '
        'gcParamDesc
        '
        Me.gcParamDesc.AllowDrop = True
        Me.gcParamDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcParamDesc.Location = New System.Drawing.Point(0, 0)
        Me.gcParamDesc.MainView = Me.gvParamDesc
        Me.gcParamDesc.Name = "gcParamDesc"
        Me.gcParamDesc.Size = New System.Drawing.Size(984, 271)
        Me.gcParamDesc.TabIndex = 2
        Me.gcParamDesc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvParamDesc, Me.GridView5})
        '
        'gvParamDesc
        '
        Me.gvParamDesc.ActiveFilterEnabled = False
        Me.gvParamDesc.GridControl = Me.gcParamDesc
        Me.gvParamDesc.Name = "gvParamDesc"
        Me.gvParamDesc.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvParamDesc.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvParamDesc.OptionsBehavior.Editable = False
        Me.gvParamDesc.OptionsBehavior.ReadOnly = True
        Me.gvParamDesc.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamDesc.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamDesc.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamDesc.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvParamDesc.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamDesc.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvParamDesc.OptionsFilter.ShowAllTableValuesInFilterPopup = True
        Me.gvParamDesc.OptionsFilter.UseNewCustomFilterDialog = True
        Me.gvParamDesc.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvParamDesc.OptionsSelection.MultiSelect = True
        Me.gvParamDesc.OptionsView.ShowAutoFilterRow = True
        Me.gvParamDesc.OptionsView.ShowGroupPanel = False
        '
        'GridView5
        '
        Me.GridView5.GridControl = Me.gcParamDesc
        Me.GridView5.Name = "GridView5"
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 1
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Controls.Add(Me.LabelControl10, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.gcHistChanges, 0, 1)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 2
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(0, 0)
        Me.TableLayoutPanel14.TabIndex = 0
        '
        'LabelControl10
        '
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl10.Size = New System.Drawing.Size(1, 17)
        Me.LabelControl10.TabIndex = 0
        Me.LabelControl10.Text = "Historic Changes"
        '
        'gcHistChanges
        '
        Me.gcHistChanges.ContextMenuStrip = Me.cmsHistoryChanges
        Me.gcHistChanges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcHistChanges.Location = New System.Drawing.Point(3, 26)
        Me.gcHistChanges.MainView = Me.gvHistChanges
        Me.gcHistChanges.Name = "gcHistChanges"
        Me.gcHistChanges.Size = New System.Drawing.Size(1, 1)
        Me.gcHistChanges.TabIndex = 1
        Me.gcHistChanges.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvHistChanges})
        '
        'cmsHistoryChanges
        '
        Me.cmsHistoryChanges.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_histChangesRecordCount, Me.ToolStripMenuItem1, Me.tsmi_HistChangesAllowCellCopy, Me.tsmi_HistChangesCopySelection, Me.tsmi_HistChangesCopySelectionWithHeader})
        Me.cmsHistoryChanges.Name = "cmsHistoryChanges"
        Me.cmsHistoryChanges.Size = New System.Drawing.Size(241, 98)
        '
        'tsmi_histChangesRecordCount
        '
        Me.tsmi_histChangesRecordCount.Name = "tsmi_histChangesRecordCount"
        Me.tsmi_histChangesRecordCount.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_histChangesRecordCount.Text = "Record Count:"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(237, 6)
        '
        'tsmi_HistChangesAllowCellCopy
        '
        Me.tsmi_HistChangesAllowCellCopy.CheckOnClick = True
        Me.tsmi_HistChangesAllowCellCopy.Name = "tsmi_HistChangesAllowCellCopy"
        Me.tsmi_HistChangesAllowCellCopy.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_HistChangesAllowCellCopy.Text = "Allow Cell Copy"
        '
        'tsmi_HistChangesCopySelection
        '
        Me.tsmi_HistChangesCopySelection.Name = "tsmi_HistChangesCopySelection"
        Me.tsmi_HistChangesCopySelection.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_HistChangesCopySelection.Text = "Copy Selection Without Header"
        '
        'tsmi_HistChangesCopySelectionWithHeader
        '
        Me.tsmi_HistChangesCopySelectionWithHeader.Name = "tsmi_HistChangesCopySelectionWithHeader"
        Me.tsmi_HistChangesCopySelectionWithHeader.Size = New System.Drawing.Size(240, 22)
        Me.tsmi_HistChangesCopySelectionWithHeader.Text = "Copy Selection With Header"
        '
        'gvHistChanges
        '
        Me.gvHistChanges.ActiveFilterEnabled = False
        Me.gvHistChanges.GridControl = Me.gcHistChanges
        Me.gvHistChanges.Name = "gvHistChanges"
        Me.gvHistChanges.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvHistChanges.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvHistChanges.OptionsBehavior.Editable = False
        Me.gvHistChanges.OptionsBehavior.ReadOnly = True
        Me.gvHistChanges.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvHistChanges.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvHistChanges.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvHistChanges.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvHistChanges.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvHistChanges.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvHistChanges.OptionsMenu.EnableColumnMenu = False
        Me.gvHistChanges.OptionsMenu.EnableFooterMenu = False
        Me.gvHistChanges.OptionsMenu.EnableGroupPanelMenu = False
        Me.gvHistChanges.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvHistChanges.OptionsSelection.MultiSelect = True
        Me.gvHistChanges.OptionsView.ShowAutoFilterRow = True
        Me.gvHistChanges.OptionsView.ShowGroupPanel = False
        '
        'ImgList
        '
        Me.ImgList.ImageStream = CType(resources.GetObject("ImgList.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        '
        'frmCMView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1240, 733)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.IconOptions.Icon = CType(resources.GetObject("frmCMView.IconOptions.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1200, 599)
        Me.Name = "frmCMView"
        Me.Text = "CM View"
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.tvObjectTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmObjectTree.ResumeLayout(False)
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTargetObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        CType(Me.lstViewMO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel15.ResumeLayout(False)
        CType(Me.ceLoadObjectTree.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        CType(Me.gcParamFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsGrid.ResumeLayout(False)
        CType(Me.gvParamFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        CType(Me.tlvFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        CType(Me.tlParameterList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsParamList.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.chkSearchAllParameter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchPH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.gcObject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvObject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sccGrids.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccGrids.Panel1.ResumeLayout(False)
        CType(Me.sccGrids.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccGrids.Panel2.ResumeLayout(False)
        CType(Me.sccGrids, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccGrids.ResumeLayout(False)
        CType(Me.xtcBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtcBottom.ResumeLayout(False)
        Me.xtpCurrSettings.ResumeLayout(False)
        Me.tlpBottom.ResumeLayout(False)
        CType(Me.gcCMView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCMView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpCurrentSettings.ResumeLayout(False)
        Me.tlpCurrentSettings.PerformLayout()
        CType(Me.txtQueryBatchSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpParamDesc.ResumeLayout(False)
        CType(Me.gcParamDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParamDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel14.ResumeLayout(False)
        Me.TableLayoutPanel14.PerformLayout()
        CType(Me.gcHistChanges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsHistoryChanges.ResumeLayout(False)
        CType(Me.gvHistChanges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmbTargetObject As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblVendor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
	Friend WithEvents cmObjectTree As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents cm_OT_tsmi_copy As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents cm_OT_tsmi_paste As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents cm_OT_tsmi_CheckChilds As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents tsmi_OT_UnCheck As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents cmbVendor As DevExpress.XtraEditors.ComboBoxEdit
	Private WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
	Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents cmbTemplate As DevExpress.XtraEditors.ComboBoxEdit
	Friend WithEvents btnGetTemplate As DevExpress.XtraEditors.SimpleButton
	Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnDump2Csv As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblQueryBatchSize As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtQueryBatchSize As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnDump2Xls As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmsGrid As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_CompareVsSelection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gcCMView As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCMView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lstViewMO As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents tsmi_FreezeColumn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_FreezeRow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlvFilters As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents sccGrids As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents tlpBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel14 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcHistChanges As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvHistChanges As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel15 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ceLoadObjectTree As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tsmi_GetChangesForMO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_GetChangesForSelection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtSearchMO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents gcParamFilter As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvParamFilter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tsmi_AllowCellCopy As ToolStripMenuItem
    Friend WithEvents tsmi_CopySelection As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents cmsHistoryChanges As ContextMenuStrip
    Friend WithEvents tsmi_HistChangesAllowCellCopy As ToolStripMenuItem
    Friend WithEvents tsmi_HistChangesCopySelection As ToolStripMenuItem
    Friend WithEvents tlpCurrentSettings As TableLayoutPanel
    Friend WithEvents tsmi_RecordCount As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tsmi_histChangesRecordCount As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tvObjectTree As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ImgList As DevExpress.Utils.ImageCollection
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents tlParameterList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents chkSearchAllParameter As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtSearchPH As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcObject As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvObject As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tsmi_CopySelectionWithHeader As ToolStripMenuItem
    Friend WithEvents tsmi_HistChangesCopySelectionWithHeader As ToolStripMenuItem
    Friend WithEvents xtcBottom As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpCurrSettings As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpParamDesc As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gcParamDesc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvParamDesc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmsParamList As ContextMenuStrip
    Friend WithEvents tsmi_ParamDescParam As ToolStripMenuItem
End Class
