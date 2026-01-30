<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKPISetCreate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKPISetCreate))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpDragDrop = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcKPIList4TechCounter = New DevExpress.XtraGrid.GridControl()
        Me.gvKPIList4TechCounter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpKPIsTree = New System.Windows.Forms.TableLayoutPanel()
        Me.lblKPISetName = New DevExpress.XtraEditors.LabelControl()
        Me.tlKPISetKPIsList = New DevExpress.XtraTreeList.TreeList()
        Me.tlpDragDropSymbol = New System.Windows.Forms.TableLayoutPanel()
        Me.picDrag = New DevExpress.XtraEditors.PictureEdit()
        Me.grpManageKPISet = New DevExpress.XtraEditors.GroupControl()
        Me.tlpManageKPISet = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbKPISets = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.btnCreate = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRename = New DevExpress.XtraEditors.SimpleButton()
        Me.DisabledCellEvents1 = New DevExpress.Utils.Behaviors.Common.DisabledCellEvents(Me.components)
        Me.DragDropEvents1 = New DevExpress.Utils.DragDrop.DragDropEvents(Me.components)
        Me.DragDropEvents2 = New DevExpress.Utils.DragDrop.DragDropEvents(Me.components)
        Me.cm_KPITreeList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_DeleteKPIs = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlpMain.SuspendLayout()
        Me.tlpDragDrop.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcKPIList4TechCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvKPIList4TechCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpKPIsTree.SuspendLayout()
        CType(Me.tlKPISetKPIsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpDragDropSymbol.SuspendLayout()
        CType(Me.picDrag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpManageKPISet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpManageKPISet.SuspendLayout()
        Me.tlpManageKPISet.SuspendLayout()
        CType(Me.cmbKPISets.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_KPITreeList.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpDragDrop, 0, 1)
        Me.tlpMain.Controls.Add(Me.grpManageKPISet, 0, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(1089, 675)
        Me.tlpMain.TabIndex = 0
        '
        'tlpDragDrop
        '
        Me.tlpDragDrop.ColumnCount = 3
        Me.tlpDragDrop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.tlpDragDrop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpDragDrop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.tlpDragDrop.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.tlpDragDrop.Controls.Add(Me.tlpKPIsTree, 2, 0)
        Me.tlpDragDrop.Controls.Add(Me.tlpDragDropSymbol, 1, 0)
        Me.tlpDragDrop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpDragDrop.Location = New System.Drawing.Point(3, 63)
        Me.tlpDragDrop.Name = "tlpDragDrop"
        Me.tlpDragDrop.RowCount = 1
        Me.tlpDragDrop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDragDrop.Size = New System.Drawing.Size(1083, 609)
        Me.tlpDragDrop.TabIndex = 1
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.gcKPIList4TechCounter, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(417, 609)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'gcKPIList4TechCounter
        '
        Me.gcKPIList4TechCounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPIList4TechCounter.Location = New System.Drawing.Point(3, 33)
        Me.gcKPIList4TechCounter.MainView = Me.gvKPIList4TechCounter
        Me.gcKPIList4TechCounter.Name = "gcKPIList4TechCounter"
        Me.gcKPIList4TechCounter.Size = New System.Drawing.Size(411, 573)
        Me.gcKPIList4TechCounter.TabIndex = 6
        Me.gcKPIList4TechCounter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKPIList4TechCounter})
        '
        'gvKPIList4TechCounter
        '
        Me.gvKPIList4TechCounter.GridControl = Me.gcKPIList4TechCounter
        Me.gvKPIList4TechCounter.Name = "gvKPIList4TechCounter"
        Me.gvKPIList4TechCounter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPIList4TechCounter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPIList4TechCounter.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKPIList4TechCounter.OptionsBehavior.Editable = False
        Me.gvKPIList4TechCounter.OptionsBehavior.ReadOnly = True
        Me.gvKPIList4TechCounter.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvKPIList4TechCounter.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvKPIList4TechCounter.OptionsSelection.MultiSelect = True
        Me.gvKPIList4TechCounter.OptionsView.ColumnAutoWidth = False
        Me.gvKPIList4TechCounter.OptionsView.ShowAutoFilterRow = True
        Me.gvKPIList4TechCounter.OptionsView.ShowGroupPanel = False
        Me.gvKPIList4TechCounter.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(413, 26)
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "All KPIs"
        '
        'tlpKPIsTree
        '
        Me.tlpKPIsTree.ColumnCount = 1
        Me.tlpKPIsTree.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPIsTree.Controls.Add(Me.lblKPISetName, 0, 0)
        Me.tlpKPIsTree.Controls.Add(Me.tlKPISetKPIsList, 0, 1)
        Me.tlpKPIsTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKPIsTree.Location = New System.Drawing.Point(457, 0)
        Me.tlpKPIsTree.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpKPIsTree.Name = "tlpKPIsTree"
        Me.tlpKPIsTree.RowCount = 2
        Me.tlpKPIsTree.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpKPIsTree.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPIsTree.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpKPIsTree.Size = New System.Drawing.Size(626, 609)
        Me.tlpKPIsTree.TabIndex = 1
        '
        'lblKPISetName
        '
        Me.lblKPISetName.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKPISetName.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblKPISetName.Appearance.Options.UseFont = True
        Me.lblKPISetName.Appearance.Options.UseForeColor = True
        Me.lblKPISetName.Appearance.Options.UseTextOptions = True
        Me.lblKPISetName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblKPISetName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblKPISetName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblKPISetName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKPISetName.Location = New System.Drawing.Point(2, 2)
        Me.lblKPISetName.Margin = New System.Windows.Forms.Padding(2)
        Me.lblKPISetName.Name = "lblKPISetName"
        Me.lblKPISetName.Size = New System.Drawing.Size(622, 26)
        Me.lblKPISetName.TabIndex = 6
        Me.lblKPISetName.Text = "Select KPIs from left list and click arrow button to save and shuffle within the " &
    "list to alter KPI ordinal"
        '
        'tlKPISetKPIsList
        '
        Me.tlKPISetKPIsList.AllowDrop = True
        Me.tlKPISetKPIsList.ContextMenuStrip = Me.cm_KPITreeList
        Me.tlKPISetKPIsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlKPISetKPIsList.KeyFieldName = "KPI_Name"
        Me.tlKPISetKPIsList.Location = New System.Drawing.Point(3, 33)
        Me.tlKPISetKPIsList.Name = "tlKPISetKPIsList"
        Me.tlKPISetKPIsList.OptionsBehavior.Editable = False
        Me.tlKPISetKPIsList.OptionsBehavior.ReadOnly = True
        Me.tlKPISetKPIsList.OptionsFind.AllowFindPanel = False
        Me.tlKPISetKPIsList.OptionsFind.ExpandNodesOnIncrementalSearch = True
        Me.tlKPISetKPIsList.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always
        Me.tlKPISetKPIsList.OptionsFind.ShowCloseButton = False
        Me.tlKPISetKPIsList.OptionsFind.ShowFindButton = False
        Me.tlKPISetKPIsList.OptionsMenu.EnableColumnMenu = False
        Me.tlKPISetKPIsList.OptionsMenu.EnableFooterMenu = False
        Me.tlKPISetKPIsList.OptionsMenu.EnableNodeMenu = False
        Me.tlKPISetKPIsList.OptionsSelection.MultiSelect = True
        Me.tlKPISetKPIsList.OptionsView.AutoWidth = False
        Me.tlKPISetKPIsList.OptionsView.BestFitMode = DevExpress.XtraTreeList.TreeListBestFitMode.Fast
        Me.tlKPISetKPIsList.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible
        Me.tlKPISetKPIsList.Size = New System.Drawing.Size(620, 573)
        Me.tlKPISetKPIsList.TabIndex = 7
        '
        'tlpDragDropSymbol
        '
        Me.tlpDragDropSymbol.ColumnCount = 1
        Me.tlpDragDropSymbol.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDragDropSymbol.Controls.Add(Me.picDrag, 0, 1)
        Me.tlpDragDropSymbol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpDragDropSymbol.Location = New System.Drawing.Point(417, 0)
        Me.tlpDragDropSymbol.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpDragDropSymbol.Name = "tlpDragDropSymbol"
        Me.tlpDragDropSymbol.RowCount = 3
        Me.tlpDragDropSymbol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpDragDropSymbol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpDragDropSymbol.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpDragDropSymbol.Size = New System.Drawing.Size(40, 609)
        Me.tlpDragDropSymbol.TabIndex = 2
        '
        'picDrag
        '
        Me.picDrag.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picDrag.EditValue = CType(resources.GetObject("picDrag.EditValue"), Object)
        Me.picDrag.Location = New System.Drawing.Point(0, 289)
        Me.picDrag.Margin = New System.Windows.Forms.Padding(0)
        Me.picDrag.Name = "picDrag"
        Me.picDrag.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.picDrag.Size = New System.Drawing.Size(40, 30)
        Me.picDrag.TabIndex = 0
        Me.picDrag.ToolTip = "Select KPI(s) from the left grid and click arrow button to save"
        Me.picDrag.ToolTipTitle = "Select KPI Set"
        '
        'grpManageKPISet
        '
        Me.grpManageKPISet.Controls.Add(Me.tlpManageKPISet)
        Me.grpManageKPISet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpManageKPISet.Location = New System.Drawing.Point(3, 3)
        Me.grpManageKPISet.Name = "grpManageKPISet"
        Me.grpManageKPISet.Size = New System.Drawing.Size(1083, 54)
        Me.grpManageKPISet.TabIndex = 2
        Me.grpManageKPISet.Text = "Manage KPI Set"
        '
        'tlpManageKPISet
        '
        Me.tlpManageKPISet.ColumnCount = 6
        Me.tlpManageKPISet.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpManageKPISet.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260.0!))
        Me.tlpManageKPISet.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.tlpManageKPISet.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.tlpManageKPISet.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.tlpManageKPISet.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpManageKPISet.Controls.Add(Me.cmbKPISets, 0, 0)
        Me.tlpManageKPISet.Controls.Add(Me.LabelControl9, 0, 0)
        Me.tlpManageKPISet.Controls.Add(Me.btnCreate, 2, 0)
        Me.tlpManageKPISet.Controls.Add(Me.btnDelete, 3, 0)
        Me.tlpManageKPISet.Controls.Add(Me.btnRename, 4, 0)
        Me.tlpManageKPISet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpManageKPISet.Location = New System.Drawing.Point(2, 23)
        Me.tlpManageKPISet.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpManageKPISet.Name = "tlpManageKPISet"
        Me.tlpManageKPISet.RowCount = 1
        Me.tlpManageKPISet.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpManageKPISet.Size = New System.Drawing.Size(1079, 29)
        Me.tlpManageKPISet.TabIndex = 0
        '
        'cmbKPISets
        '
        Me.cmbKPISets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbKPISets.Location = New System.Drawing.Point(83, 5)
        Me.cmbKPISets.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbKPISets.Name = "cmbKPISets"
        Me.cmbKPISets.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbKPISets.Size = New System.Drawing.Size(254, 20)
        Me.cmbKPISets.TabIndex = 10
        Me.cmbKPISets.Tag = "Stats"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(74, 23)
        Me.LabelControl9.TabIndex = 2
        Me.LabelControl9.Text = "Select KPI Set"
        '
        'btnCreate
        '
        Me.btnCreate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCreate.Location = New System.Drawing.Point(342, 2)
        Me.btnCreate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(71, 25)
        Me.btnCreate.TabIndex = 11
        Me.btnCreate.Text = "Create"
        '
        'btnDelete
        '
        Me.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDelete.Location = New System.Drawing.Point(417, 2)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(71, 25)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Delete"
        '
        'btnRename
        '
        Me.btnRename.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRename.Location = New System.Drawing.Point(492, 2)
        Me.btnRename.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(71, 25)
        Me.btnRename.TabIndex = 13
        Me.btnRename.Text = "Rename"
        '
        'cm_KPITreeList
        '
        Me.cm_KPITreeList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_DeleteKPIs})
        Me.cm_KPITreeList.Name = "cm_KPITreeList"
        Me.cm_KPITreeList.Size = New System.Drawing.Size(141, 26)
        '
        'tsmi_DeleteKPIs
        '
        Me.tsmi_DeleteKPIs.Name = "tsmi_DeleteKPIs"
        Me.tsmi_DeleteKPIs.Size = New System.Drawing.Size(140, 22)
        Me.tsmi_DeleteKPIs.Text = "Delete KPI(s)"
        '
        'frmKPISetCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1089, 675)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Image = CType(resources.GetObject("frmKPISetCreate.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1400, 900)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1000, 550)
        Me.Name = "frmKPISetCreate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create KPI Set"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpDragDrop.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.gcKPIList4TechCounter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvKPIList4TechCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpKPIsTree.ResumeLayout(False)
        Me.tlpKPIsTree.PerformLayout()
        CType(Me.tlKPISetKPIsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpDragDropSymbol.ResumeLayout(False)
        CType(Me.picDrag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpManageKPISet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpManageKPISet.ResumeLayout(False)
        Me.tlpManageKPISet.ResumeLayout(False)
        Me.tlpManageKPISet.PerformLayout()
        CType(Me.cmbKPISets.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_KPITreeList.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpManageKPISet As TableLayoutPanel
    Friend WithEvents tlpDragDrop As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents tlpKPIsTree As TableLayoutPanel
    Friend WithEvents tlpDragDropSymbol As TableLayoutPanel
    Friend WithEvents gcKPIList4TechCounter As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKPIList4TechCounter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbKPISets As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCreate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents picDrag As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKPISetName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DisabledCellEvents1 As DevExpress.Utils.Behaviors.Common.DisabledCellEvents
    Friend WithEvents DragDropEvents2 As DevExpress.Utils.DragDrop.DragDropEvents
    Friend WithEvents DragDropEvents1 As DevExpress.Utils.DragDrop.DragDropEvents
    Friend WithEvents tlKPISetKPIsList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents grpManageKPISet As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnRename As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cm_KPITreeList As ContextMenuStrip
    Friend WithEvents tsmi_DeleteKPIs As ToolStripMenuItem
End Class
