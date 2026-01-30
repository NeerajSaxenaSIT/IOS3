<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKPIManage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKPIManage))
        Dim TreeListViewColumn2 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Me.cmKPIManagerStripMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmKPIDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmKPIRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcKPIName = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdKpiList = New DevExpress.XtraGrid.GridControl()
        Me.gvKpiList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSearchKPI = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnAddKpi = New DevExpress.XtraEditors.SimpleButton()
        Me.gcOpratorList = New DevExpress.XtraEditors.GroupControl()
        Me.lstOperators = New DevExpress.XtraEditors.ListBoxControl()
        Me.tlpKPIFormDesc = New System.Windows.Forms.TableLayoutPanel()
        Me.gcKPIFormula = New DevExpress.XtraEditors.GroupControl()
        Me.txtKPIFormula = New DevExpress.XtraEditors.MemoEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtKPIDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbObjectList = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.VLabel29 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcTablesCounter = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSearchCounterName = New DevExpress.XtraEditors.ButtonEdit()
        Me.dgvTableCounter = New DevExpress.XtraGrid.GridControl()
        Me.gvTableCounter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcAggregateFunctions = New DevExpress.XtraEditors.GroupControl()
        Me.lstAggregateFunction = New DevExpress.XtraEditors.ListBoxControl()
        Me.gcTableInUse = New DevExpress.XtraEditors.GroupControl()
        Me.tlvUsingTableName = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnKPICommit = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_KPITest = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.txtValueIfNull = New DevExpress.XtraEditors.TextEdit()
        Me.VLabel4 = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmKPIManagerStripMenu.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcKPIName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcKPIName.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.grdKpiList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvKpiList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.txtSearchKPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcOpratorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcOpratorList.SuspendLayout()
        CType(Me.lstOperators, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpKPIFormDesc.SuspendLayout()
        CType(Me.gcKPIFormula, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcKPIFormula.SuspendLayout()
        CType(Me.txtKPIFormula.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtKPIDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.cmbObjectList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.gcTablesCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcTablesCounter.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel15.SuspendLayout()
        CType(Me.txtSearchCounterName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTableCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTableCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.gcAggregateFunctions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcAggregateFunctions.SuspendLayout()
        CType(Me.lstAggregateFunction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcTableInUse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcTableInUse.SuspendLayout()
        CType(Me.tlvUsingTableName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.txtValueIfNull.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmKPIManagerStripMenu
        '
        Me.cmKPIManagerStripMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmKPIDelete, Me.cmKPIRename})
        Me.cmKPIManagerStripMenu.Name = "cm_tlv_CustomCharts"
        Me.cmKPIManagerStripMenu.Size = New System.Drawing.Size(146, 48)
        '
        'cmKPIDelete
        '
        Me.cmKPIDelete.Name = "cmKPIDelete"
        Me.cmKPIDelete.Size = New System.Drawing.Size(145, 22)
        Me.cmKPIDelete.Text = "KPI - Delete"
        '
        'cmKPIRename
        '
        Me.cmKPIRename.Name = "cmKPIRename"
        Me.cmKPIRename.Size = New System.Drawing.Size(145, 22)
        Me.cmKPIRename.Text = "KPI - Rename"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 650.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1184, 611)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.85072!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.14928!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1180, 607)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.gcKPIName, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.gcOpratorList, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.tlpKPIFormDesc, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 33)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1176, 323)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'gcKPIName
        '
        Me.gcKPIName.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gcKPIName.Appearance.Options.UseBackColor = True
        Me.gcKPIName.Controls.Add(Me.TableLayoutPanel6)
        Me.gcKPIName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPIName.Location = New System.Drawing.Point(3, 3)
        Me.gcKPIName.Name = "gcKPIName"
        Me.gcKPIName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gcKPIName.Size = New System.Drawing.Size(394, 317)
        Me.gcKPIName.TabIndex = 18
        Me.gcKPIName.Text = "KPI Name"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.grdKpiList, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel11, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(4, 25, 4, 4)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(390, 292)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'grdKpiList
        '
        Me.grdKpiList.AllowDrop = True
        Me.grdKpiList.ContextMenuStrip = Me.cmKPIManagerStripMenu
        Me.grdKpiList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdKpiList.Location = New System.Drawing.Point(3, 37)
        Me.grdKpiList.MainView = Me.gvKpiList
        Me.grdKpiList.Name = "grdKpiList"
        Me.grdKpiList.Size = New System.Drawing.Size(384, 252)
        Me.grdKpiList.TabIndex = 8
        Me.grdKpiList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKpiList})
        '
        'gvKpiList
        '
        Me.gvKpiList.GridControl = Me.grdKpiList
        Me.gvKpiList.Name = "gvKpiList"
        Me.gvKpiList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKpiList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvKpiList.OptionsBehavior.Editable = False
        Me.gvKpiList.OptionsBehavior.ReadOnly = True
        Me.gvKpiList.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvKpiList.OptionsView.ColumnAutoWidth = False
        Me.gvKpiList.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 2
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.txtSearchKPI, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.btnAddKpi, 1, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(386, 30)
        Me.TableLayoutPanel11.TabIndex = 6
        '
        'txtSearchKPI
        '
        Me.txtSearchKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchKPI.Location = New System.Drawing.Point(3, 5)
        Me.txtSearchKPI.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.txtSearchKPI.Name = "txtSearchKPI"
        Me.txtSearchKPI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchKPI.Properties.NullValuePrompt = "Search..."
        Me.txtSearchKPI.Size = New System.Drawing.Size(280, 20)
        Me.txtSearchKPI.TabIndex = 8
        '
        'btnAddKpi
        '
        Me.btnAddKpi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddKpi.Location = New System.Drawing.Point(289, 3)
        Me.btnAddKpi.Name = "btnAddKpi"
        Me.btnAddKpi.Size = New System.Drawing.Size(94, 24)
        Me.btnAddKpi.TabIndex = 7
        Me.btnAddKpi.Text = "Add KPI"
        '
        'gcOpratorList
        '
        Me.gcOpratorList.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gcOpratorList.Appearance.Options.UseBackColor = True
        Me.gcOpratorList.Controls.Add(Me.lstOperators)
        Me.gcOpratorList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcOpratorList.Location = New System.Drawing.Point(966, 3)
        Me.gcOpratorList.Name = "gcOpratorList"
        Me.gcOpratorList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gcOpratorList.Size = New System.Drawing.Size(207, 317)
        Me.gcOpratorList.TabIndex = 17
        Me.gcOpratorList.Text = "Operator List"
        '
        'lstOperators
        '
        Me.lstOperators.AllowDrop = True
        Me.lstOperators.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstOperators.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOperators.Items.AddRange(New Object() {"=", "<>", ">", "<", "<=", ">=", "+", "-", "*", "/", "^", "()", "AND", "OR", "NOT", "LIKE", "Round(,)"})
        Me.lstOperators.Location = New System.Drawing.Point(2, 23)
        Me.lstOperators.Name = "lstOperators"
        Me.lstOperators.Size = New System.Drawing.Size(203, 292)
        Me.lstOperators.TabIndex = 0
        '
        'tlpKPIFormDesc
        '
        Me.tlpKPIFormDesc.ColumnCount = 1
        Me.tlpKPIFormDesc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPIFormDesc.Controls.Add(Me.gcKPIFormula, 0, 0)
        Me.tlpKPIFormDesc.Controls.Add(Me.GroupControl1, 0, 1)
        Me.tlpKPIFormDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKPIFormDesc.Location = New System.Drawing.Point(403, 3)
        Me.tlpKPIFormDesc.Name = "tlpKPIFormDesc"
        Me.tlpKPIFormDesc.RowCount = 2
        Me.tlpKPIFormDesc.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.tlpKPIFormDesc.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.tlpKPIFormDesc.Size = New System.Drawing.Size(557, 317)
        Me.tlpKPIFormDesc.TabIndex = 19
        '
        'gcKPIFormula
        '
        Me.gcKPIFormula.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gcKPIFormula.Appearance.Options.UseBackColor = True
        Me.gcKPIFormula.Controls.Add(Me.txtKPIFormula)
        Me.gcKPIFormula.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPIFormula.Location = New System.Drawing.Point(2, 2)
        Me.gcKPIFormula.Margin = New System.Windows.Forms.Padding(2)
        Me.gcKPIFormula.Name = "gcKPIFormula"
        Me.gcKPIFormula.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gcKPIFormula.Size = New System.Drawing.Size(553, 186)
        Me.gcKPIFormula.TabIndex = 16
        Me.gcKPIFormula.Text = "KPI Formula"
        '
        'txtKPIFormula
        '
        Me.txtKPIFormula.AllowDrop = True
        Me.txtKPIFormula.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtKPIFormula.Location = New System.Drawing.Point(2, 23)
        Me.txtKPIFormula.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtKPIFormula.Name = "txtKPIFormula"
        Me.txtKPIFormula.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtKPIFormula.Properties.Appearance.Options.UseBackColor = True
        Me.txtKPIFormula.Size = New System.Drawing.Size(549, 161)
        Me.txtKPIFormula.TabIndex = 14
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtKPIDescription)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(2, 192)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(553, 123)
        Me.GroupControl1.TabIndex = 17
        Me.GroupControl1.Text = "KPI Description"
        '
        'txtKPIDescription
        '
        Me.txtKPIDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtKPIDescription.Location = New System.Drawing.Point(2, 23)
        Me.txtKPIDescription.Margin = New System.Windows.Forms.Padding(2)
        Me.txtKPIDescription.Name = "txtKPIDescription"
        Me.txtKPIDescription.Size = New System.Drawing.Size(549, 98)
        Me.txtKPIDescription.TabIndex = 17
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 6
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 293.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 293.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.VLabel2, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.cmbObjectList, 4, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.cmbTechnology, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.VLabel29, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1176, 27)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'VLabel2
        '
        Me.VLabel2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.Appearance.Options.UseBackColor = True
        Me.VLabel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel2.Location = New System.Drawing.Point(401, 3)
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.VLabel2.Size = New System.Drawing.Size(94, 21)
        Me.VLabel2.TabIndex = 3
        Me.VLabel2.Text = "Object"
        '
        'cmbObjectList
        '
        Me.cmbObjectList.Location = New System.Drawing.Point(501, 3)
        Me.cmbObjectList.Name = "cmbObjectList"
        Me.cmbObjectList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbObjectList.Size = New System.Drawing.Size(287, 20)
        Me.cmbObjectList.TabIndex = 4
        '
        'cmbTechnology
        '
        Me.cmbTechnology.Location = New System.Drawing.Point(103, 3)
        Me.cmbTechnology.Name = "cmbTechnology"
        Me.cmbTechnology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnology.Size = New System.Drawing.Size(287, 20)
        Me.cmbTechnology.TabIndex = 5
        '
        'VLabel29
        '
        Me.VLabel29.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VLabel29.Appearance.Options.UseBackColor = True
        Me.VLabel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel29.Location = New System.Drawing.Point(3, 3)
        Me.VLabel29.Name = "VLabel29"
        Me.VLabel29.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.VLabel29.Size = New System.Drawing.Size(94, 21)
        Me.VLabel29.TabIndex = 1
        Me.VLabel29.Text = "Technology"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 828.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.gcTablesCounter, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel9, 1, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 360)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(1176, 245)
        Me.TableLayoutPanel7.TabIndex = 20
        '
        'gcTablesCounter
        '
        Me.gcTablesCounter.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gcTablesCounter.Appearance.Options.UseBackColor = True
        Me.gcTablesCounter.Controls.Add(Me.TableLayoutPanel12)
        Me.gcTablesCounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTablesCounter.Location = New System.Drawing.Point(3, 3)
        Me.gcTablesCounter.Name = "gcTablesCounter"
        Me.gcTablesCounter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gcTablesCounter.Size = New System.Drawing.Size(342, 239)
        Me.gcTablesCounter.TabIndex = 19
        Me.gcTablesCounter.Text = "Tables and TableCounter"
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 1
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.TableLayoutPanel15, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.dgvTableCounter, 0, 1)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 2
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(338, 214)
        Me.TableLayoutPanel12.TabIndex = 1
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 3
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 216.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.VLabel5, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.txtSearchCounterName, 1, 0)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 1
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(332, 28)
        Me.TableLayoutPanel15.TabIndex = 6
        '
        'VLabel5
        '
        Me.VLabel5.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VLabel5.Appearance.Options.UseBackColor = True
        Me.VLabel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel5.Location = New System.Drawing.Point(3, 3)
        Me.VLabel5.Name = "VLabel5"
        Me.VLabel5.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.VLabel5.Size = New System.Drawing.Size(111, 22)
        Me.VLabel5.TabIndex = 5
        Me.VLabel5.Text = "Search Counter Name"
        '
        'txtSearchCounterName
        '
        Me.txtSearchCounterName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchCounterName.Location = New System.Drawing.Point(120, 2)
        Me.txtSearchCounterName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtSearchCounterName.Name = "txtSearchCounterName"
        Me.txtSearchCounterName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtSearchCounterName.Properties.Appearance.Options.UseBackColor = True
        Me.txtSearchCounterName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchCounterName.Properties.NullValuePrompt = "Search..."
        Me.txtSearchCounterName.Size = New System.Drawing.Size(210, 20)
        Me.txtSearchCounterName.TabIndex = 14
        '
        'dgvTableCounter
        '
        Me.dgvTableCounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTableCounter.Location = New System.Drawing.Point(3, 37)
        Me.dgvTableCounter.MainView = Me.gvTableCounter
        Me.dgvTableCounter.Name = "dgvTableCounter"
        Me.dgvTableCounter.Size = New System.Drawing.Size(332, 174)
        Me.dgvTableCounter.TabIndex = 7
        Me.dgvTableCounter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTableCounter})
        '
        'gvTableCounter
        '
        Me.gvTableCounter.GridControl = Me.dgvTableCounter
        Me.gvTableCounter.Name = "gvTableCounter"
        Me.gvTableCounter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTableCounter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTableCounter.OptionsBehavior.Editable = False
        Me.gvTableCounter.OptionsBehavior.ReadOnly = True
        Me.gvTableCounter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvTableCounter.OptionsView.ColumnAutoWidth = False
        Me.gvTableCounter.OptionsView.ShowGroupPanel = False
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.TableLayoutPanel10, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.TableLayoutPanel8, 0, 1)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(350, 2)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(824, 241)
        Me.TableLayoutPanel9.TabIndex = 21
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.gcAggregateFunctions, 1, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.gcTableInUse, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(820, 200)
        Me.TableLayoutPanel10.TabIndex = 20
        '
        'gcAggregateFunctions
        '
        Me.gcAggregateFunctions.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gcAggregateFunctions.Appearance.Options.UseBackColor = True
        Me.gcAggregateFunctions.Controls.Add(Me.lstAggregateFunction)
        Me.gcAggregateFunctions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcAggregateFunctions.Location = New System.Drawing.Point(615, 3)
        Me.gcAggregateFunctions.Name = "gcAggregateFunctions"
        Me.gcAggregateFunctions.Size = New System.Drawing.Size(202, 194)
        Me.gcAggregateFunctions.TabIndex = 18
        Me.gcAggregateFunctions.Text = "Aggregate Functions"
        '
        'lstAggregateFunction
        '
        Me.lstAggregateFunction.AllowDrop = True
        Me.lstAggregateFunction.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstAggregateFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstAggregateFunction.Items.AddRange(New Object() {"Avg()", "Sum()", "Count()", "Min()", "Max()"})
        Me.lstAggregateFunction.Location = New System.Drawing.Point(2, 23)
        Me.lstAggregateFunction.Name = "lstAggregateFunction"
        Me.lstAggregateFunction.Size = New System.Drawing.Size(198, 169)
        Me.lstAggregateFunction.TabIndex = 0
        '
        'gcTableInUse
        '
        Me.gcTableInUse.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.gcTableInUse.Appearance.Options.UseBackColor = True
        Me.gcTableInUse.Controls.Add(Me.tlvUsingTableName)
        Me.gcTableInUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTableInUse.Location = New System.Drawing.Point(3, 3)
        Me.gcTableInUse.Name = "gcTableInUse"
        Me.gcTableInUse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gcTableInUse.Size = New System.Drawing.Size(606, 194)
        Me.gcTableInUse.TabIndex = 17
        Me.gcTableInUse.Text = "Tables in use"
        '
        'tlvUsingTableName
        '
        Me.tlvUsingTableName.AllowDrag = True
        Me.tlvUsingTableName.AllowDrop = True
        TreeListViewColumn1.FooterRect = CType(resources.GetObject("TreeListViewColumn1.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.HeaderRect = CType(resources.GetObject("TreeListViewColumn1.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.HeaderText = "Table Name"
        TreeListViewColumn1.Width = 310
        TreeListViewColumn2.FooterRect = CType(resources.GetObject("TreeListViewColumn2.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.HeaderRect = CType(resources.GetObject("TreeListViewColumn2.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.HeaderText = "TableAlias"
        TreeListViewColumn2.Width = 120
        Me.tlvUsingTableName.Columns.AddRange(New Object() {TreeListViewColumn1, TreeListViewColumn2})
        '
        '
        '
        Me.tlvUsingTableName.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvUsingTableName.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvUsingTableName.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvUsingTableName.ContentPanel.Name = ""
        Me.tlvUsingTableName.ContentPanel.Size = New System.Drawing.Size(596, 163)
        Me.tlvUsingTableName.ContentPanel.TabIndex = 3
        Me.tlvUsingTableName.ContentPanel.TabStop = False
        Me.tlvUsingTableName.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvUsingTableName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvUsingTableName.DragDropMode = LidorSystems.IntegralUI.DragDropMode.Custom
        Me.tlvUsingTableName.ExpandingColumn = TreeListViewColumn1
        Me.tlvUsingTableName.Location = New System.Drawing.Point(2, 23)
        Me.tlvUsingTableName.Name = "tlvUsingTableName"
        Me.tlvUsingTableName.ShowWholeHeader = False
        Me.tlvUsingTableName.Size = New System.Drawing.Size(602, 169)
        Me.tlvUsingTableName.TabIndex = 5
        Me.tlvUsingTableName.Text = "tlv_Tickets"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 5
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.btnKPICommit, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btn_KPITest, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.lblMessage, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtValueIfNull, 4, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.VLabel4, 3, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 207)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(818, 31)
        Me.TableLayoutPanel8.TabIndex = 18
        '
        'btnKPICommit
        '
        Me.btnKPICommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnKPICommit.Location = New System.Drawing.Point(110, 3)
        Me.btnKPICommit.Name = "btnKPICommit"
        Me.btnKPICommit.Size = New System.Drawing.Size(101, 26)
        Me.btnKPICommit.TabIndex = 6
        Me.btnKPICommit.Text = "Commit KPI"
        '
        'btn_KPITest
        '
        Me.btn_KPITest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_KPITest.Location = New System.Drawing.Point(3, 3)
        Me.btn_KPITest.Name = "btn_KPITest"
        Me.btn_KPITest.Size = New System.Drawing.Size(101, 26)
        Me.btn_KPITest.TabIndex = 5
        Me.btn_KPITest.Text = "Test KPI"
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(217, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(391, 26)
        Me.lblMessage.TabIndex = 2
        '
        'txtValueIfNull
        '
        Me.txtValueIfNull.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValueIfNull.EditValue = "0"
        Me.txtValueIfNull.Location = New System.Drawing.Point(726, 4)
        Me.txtValueIfNull.Margin = New System.Windows.Forms.Padding(4)
        Me.txtValueIfNull.Name = "txtValueIfNull"
        Me.txtValueIfNull.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtValueIfNull.Properties.Appearance.Options.UseBackColor = True
        Me.txtValueIfNull.Size = New System.Drawing.Size(88, 20)
        Me.txtValueIfNull.TabIndex = 7
        '
        'VLabel4
        '
        Me.VLabel4.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VLabel4.Appearance.Options.UseBackColor = True
        Me.VLabel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel4.Location = New System.Drawing.Point(614, 3)
        Me.VLabel4.Name = "VLabel4"
        Me.VLabel4.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.VLabel4.Size = New System.Drawing.Size(105, 26)
        Me.VLabel4.TabIndex = 8
        Me.VLabel4.Text = "Value If ""No Data"""
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'frmKPIManage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.IconOptions.Icon = CType(resources.GetObject("frmKPIManage.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1186, 643)
        Me.Name = "frmKPIManage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KPI Manager"
        Me.cmKPIManagerStripMenu.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.gcKPIName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcKPIName.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.grdKpiList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvKpiList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel11.ResumeLayout(False)
        CType(Me.txtSearchKPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcOpratorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcOpratorList.ResumeLayout(False)
        CType(Me.lstOperators, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpKPIFormDesc.ResumeLayout(False)
        CType(Me.gcKPIFormula, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcKPIFormula.ResumeLayout(False)
        CType(Me.txtKPIFormula.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.txtKPIDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.cmbObjectList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel7.ResumeLayout(False)
        CType(Me.gcTablesCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcTablesCounter.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.TableLayoutPanel15.PerformLayout()
        CType(Me.txtSearchCounterName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTableCounter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTableCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        CType(Me.gcAggregateFunctions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcAggregateFunctions.ResumeLayout(False)
        CType(Me.lstAggregateFunction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcTableInUse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcTableInUse.ResumeLayout(False)
        CType(Me.tlvUsingTableName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.txtValueIfNull.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmKPIManagerStripMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmKPIDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmKPIRename As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcKPIName As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnAddKpi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcOpratorList As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcKPIFormula As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel29 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VLabel2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcTablesCounter As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel15 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gcAggregateFunctions As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcTableInUse As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlvUsingTableName As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnKPICommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_KPITest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtValueIfNull As DevExpress.XtraEditors.TextEdit
    Friend WithEvents VLabel4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lstOperators As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents lstAggregateFunction As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents txtKPIFormula As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents cmbObjectList As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents dgvTableCounter As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTableCounter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtSearchCounterName As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents tlpKPIFormDesc As TableLayoutPanel
    Friend WithEvents txtKPIDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdKpiList As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKpiList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents txtSearchKPI As DevExpress.XtraEditors.ButtonEdit
End Class
