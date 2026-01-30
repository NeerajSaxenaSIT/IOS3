<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSBKPIManage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSBKPIManage))
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblOwner = New DevExpress.XtraEditors.LabelControl()
        Me.treeListKPI = New DevExpress.XtraTreeList.TreeList()
        Me.cms_KPIManager = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_RenameCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_DeleteCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_DeleteKPI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddCategory = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.gcKPI = New DevExpress.XtraGrid.GridControl()
        Me.gvKPI = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblKPIGroup = New DevExpress.XtraEditors.LabelControl()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.treeListKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cms_KPIManager.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.gcKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel7, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.treeListKPI, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.btnAddCategory, 0, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(346, 582)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.LabelControl4, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(1, 28)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(344, 25)
        Me.TableLayoutPanel7.TabIndex = 19
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(338, 19)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Drop KPIs on Category"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl3, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.lblOwner, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(344, 25)
        Me.TableLayoutPanel6.TabIndex = 18
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(64, 19)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Owner :"
        '
        'lblOwner
        '
        Me.lblOwner.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOwner.Appearance.Options.UseFont = True
        Me.lblOwner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOwner.Location = New System.Drawing.Point(73, 3)
        Me.lblOwner.Name = "lblOwner"
        Me.lblOwner.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblOwner.Size = New System.Drawing.Size(268, 19)
        Me.lblOwner.TabIndex = 1
        '
        'treeListKPI
        '
        Me.treeListKPI.AllowDrop = True
        Me.treeListKPI.ContextMenuStrip = Me.cms_KPIManager
        Me.treeListKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeListKPI.KeyFieldName = "KPIID"
        Me.treeListKPI.Location = New System.Drawing.Point(3, 87)
        Me.treeListKPI.Name = "treeListKPI"
        Me.treeListKPI.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.MouseDown
        Me.treeListKPI.OptionsBehavior.ResizeNodes = False
        Me.treeListKPI.OptionsCustomization.AllowBandMoving = False
        Me.treeListKPI.OptionsCustomization.AllowBandResizing = False
        Me.treeListKPI.OptionsCustomization.AllowColumnMoving = False
        Me.treeListKPI.OptionsCustomization.AllowColumnResizing = False
        Me.treeListKPI.OptionsCustomization.AllowQuickHideColumns = False
        Me.treeListKPI.OptionsCustomization.AllowSort = False
        Me.treeListKPI.OptionsCustomization.ShowBandsInCustomizationForm = False
        Me.treeListKPI.OptionsFilter.ExpandNodesOnFiltering = True
        Me.treeListKPI.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.treeListKPI.OptionsFilter.ShowAllValuesInFilterPopup = True
        Me.treeListKPI.OptionsFind.AllowIncrementalSearch = True
        Me.treeListKPI.OptionsFind.AlwaysVisible = True
        Me.treeListKPI.OptionsFind.ExpandNodesOnIncrementalSearch = True
        Me.treeListKPI.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always
        Me.treeListKPI.OptionsFind.ShowCloseButton = False
        Me.treeListKPI.OptionsFind.ShowFindButton = False
        Me.treeListKPI.OptionsLayout.AddNewColumns = False
        Me.treeListKPI.OptionsMenu.EnableColumnMenu = False
        Me.treeListKPI.OptionsMenu.EnableFooterMenu = False
        Me.treeListKPI.OptionsNavigation.AutoFocusNewNode = True
        Me.treeListKPI.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.treeListKPI.OptionsSelection.MultiSelectMode = DevExpress.XtraTreeList.TreeListMultiSelectMode.CellSelect
        Me.treeListKPI.OptionsSelection.SelectNodesOnRightClick = True
        Me.treeListKPI.OptionsView.AutoWidth = False
        Me.treeListKPI.OptionsView.BestFitMode = DevExpress.XtraTreeList.TreeListBestFitMode.Fast
        Me.treeListKPI.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible
        Me.treeListKPI.Size = New System.Drawing.Size(340, 492)
        Me.treeListKPI.TabIndex = 17
        Me.treeListKPI.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView
        '
        'cms_KPIManager
        '
        Me.cms_KPIManager.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_RenameCategory, Me.tsmi_DeleteCategory, Me.ToolStripMenuItem7, Me.tsmi_DeleteKPI, Me.ToolStripMenuItem8})
        Me.cms_KPIManager.Name = "cm_tlv_CustomCharts"
        Me.cms_KPIManager.Size = New System.Drawing.Size(220, 104)
        '
        'tsmi_RenameCategory
        '
        Me.tsmi_RenameCategory.Name = "tsmi_RenameCategory"
        Me.tsmi_RenameCategory.Size = New System.Drawing.Size(219, 22)
        Me.tsmi_RenameCategory.Text = "Rename Category"
        '
        'tsmi_DeleteCategory
        '
        Me.tsmi_DeleteCategory.Name = "tsmi_DeleteCategory"
        Me.tsmi_DeleteCategory.Size = New System.Drawing.Size(219, 22)
        Me.tsmi_DeleteCategory.Text = "Delete Category"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(216, 6)
        '
        'tsmi_DeleteKPI
        '
        Me.tsmi_DeleteKPI.Name = "tsmi_DeleteKPI"
        Me.tsmi_DeleteKPI.Size = New System.Drawing.Size(219, 22)
        Me.tsmi_DeleteKPI.Text = "Remove KPI From Category"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(216, 6)
        '
        'btnAddCategory
        '
        Me.btnAddCategory.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddCategory.Location = New System.Drawing.Point(223, 57)
        Me.btnAddCategory.Name = "btnAddCategory"
        Me.btnAddCategory.Size = New System.Drawing.Size(120, 24)
        Me.btnAddCategory.TabIndex = 20
        Me.btnAddCategory.Text = "Add Category"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.gcKPI, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnRefresh, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(609, 582)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl2, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(1, 28)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(607, 25)
        Me.TableLayoutPanel5.TabIndex = 5
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(601, 19)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Select KPIs"
        '
        'gcKPI
        '
        Me.gcKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPI.Location = New System.Drawing.Point(3, 87)
        Me.gcKPI.MainView = Me.gvKPI
        Me.gcKPI.Name = "gcKPI"
        Me.gcKPI.Size = New System.Drawing.Size(603, 492)
        Me.gcKPI.TabIndex = 3
        Me.gcKPI.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKPI, Me.GridView6})
        '
        'gvKPI
        '
        Me.gvKPI.ActiveFilterEnabled = False
        Me.gvKPI.GridControl = Me.gcKPI
        Me.gvKPI.Name = "gvKPI"
        Me.gvKPI.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPI.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPI.OptionsBehavior.Editable = False
        Me.gvKPI.OptionsBehavior.ReadOnly = True
        Me.gvKPI.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvKPI.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPI.OptionsFilter.ShowAllTableValuesInFilterPopup = True
        Me.gvKPI.OptionsFilter.UseNewCustomFilterDialog = True
        Me.gvKPI.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvKPI.OptionsSelection.MultiSelect = True
        Me.gvKPI.OptionsView.ShowAutoFilterRow = True
        Me.gvKPI.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.gcKPI
        Me.GridView6.Name = "GridView6"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.lblKPIGroup, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(607, 25)
        Me.TableLayoutPanel4.TabIndex = 4
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(64, 19)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "KPI Group :"
        '
        'lblKPIGroup
        '
        Me.lblKPIGroup.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKPIGroup.Appearance.Options.UseFont = True
        Me.lblKPIGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKPIGroup.Location = New System.Drawing.Point(73, 3)
        Me.lblKPIGroup.Name = "lblKPIGroup"
        Me.lblKPIGroup.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblKPIGroup.Size = New System.Drawing.Size(531, 19)
        Me.lblKPIGroup.TabIndex = 1
        '
        'btnRefresh
        '
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRefresh.Location = New System.Drawing.Point(486, 57)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(120, 24)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainerControl1.Panel1.MinSize = 300
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TableLayoutPanel3)
        Me.SplitContainerControl1.Panel2.MinSize = 300
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(960, 582)
        Me.SplitContainerControl1.SplitterPosition = 609
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'frmSBKPIManage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 582)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(720, 460)
        Me.Name = "frmSBKPIManage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DataMart KPI Manager"
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.treeListKPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cms_KPIManager.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.gcKPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvKPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents gcKPI As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKPI As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents treeListKPI As DevExpress.XtraTreeList.TreeList
    Friend WithEvents cms_KPIManager As ContextMenuStrip
    Friend WithEvents tsmi_RenameCategory As ToolStripMenuItem
    Friend WithEvents tsmi_DeleteCategory As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As ToolStripSeparator
    Friend WithEvents tsmi_DeleteKPI As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As ToolStripSeparator
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOwner As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAddCategory As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKPIGroup As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
End Class
