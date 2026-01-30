<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgCondition
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCondition))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcConditionFields = New DevExpress.XtraGrid.GridControl()
        Me.gvConditionFields = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtSearchField = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcDragValue = New DevExpress.XtraGrid.GridControl()
        Me.gvDragValue = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbOperator = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcValue = New DevExpress.XtraGrid.GridControl()
        Me.gvValue = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtSearchValue = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddThen = New DevExpress.XtraEditors.SimpleButton()
        Me.txtParamSetValue = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcCondition = New DevExpress.XtraGrid.GridControl()
        Me.gvCondition = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddElse = New DevExpress.XtraEditors.SimpleButton()
        Me.txtParamSetValueElse = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.gcConditionFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConditionFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcDragValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDragValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperator.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.gcValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.txtParamSetValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.gcCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.txtParamSetValueElse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl7, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(884, 611)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(878, 322)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "IF"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupControl5, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupControl3, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupControl4, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(874, 297)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel5)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(282, 291)
        Me.GroupControl5.TabIndex = 5
        Me.GroupControl5.Text = "1. Choose Table Field"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.gcConditionFields, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.txtSearchField, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(278, 266)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'gcConditionFields
        '
        Me.gcConditionFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcConditionFields.Location = New System.Drawing.Point(3, 28)
        Me.gcConditionFields.MainView = Me.gvConditionFields
        Me.gcConditionFields.Name = "gcConditionFields"
        Me.gcConditionFields.Size = New System.Drawing.Size(272, 235)
        Me.gcConditionFields.TabIndex = 16
        Me.gcConditionFields.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvConditionFields})
        '
        'gvConditionFields
        '
        Me.gvConditionFields.GridControl = Me.gcConditionFields
        Me.gvConditionFields.Name = "gvConditionFields"
        Me.gvConditionFields.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConditionFields.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvConditionFields.OptionsBehavior.Editable = False
        Me.gvConditionFields.OptionsBehavior.ReadOnly = True
        Me.gvConditionFields.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConditionFields.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConditionFields.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvConditionFields.OptionsView.ColumnAutoWidth = False
        Me.gvConditionFields.OptionsView.ShowGroupPanel = False
        '
        'txtSearchField
        '
        Me.txtSearchField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchField.Location = New System.Drawing.Point(3, 3)
        Me.txtSearchField.Name = "txtSearchField"
        Me.txtSearchField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchField.Properties.NullValuePrompt = "Search..."
        Me.txtSearchField.Size = New System.Drawing.Size(272, 20)
        Me.txtSearchField.TabIndex = 2
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(588, 3)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(283, 291)
        Me.GroupControl3.TabIndex = 3
        Me.GroupControl3.Text = "3. Choose Operator"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.gcDragValue, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbOperator, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(279, 266)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'gcDragValue
        '
        Me.gcDragValue.AllowDrop = True
        Me.gcDragValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDragValue.Location = New System.Drawing.Point(3, 28)
        Me.gcDragValue.MainView = Me.gvDragValue
        Me.gcDragValue.Name = "gcDragValue"
        Me.gcDragValue.Size = New System.Drawing.Size(273, 235)
        Me.gcDragValue.TabIndex = 17
        Me.gcDragValue.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDragValue})
        '
        'gvDragValue
        '
        Me.gvDragValue.GridControl = Me.gcDragValue
        Me.gvDragValue.Name = "gvDragValue"
        Me.gvDragValue.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDragValue.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDragValue.OptionsBehavior.Editable = False
        Me.gvDragValue.OptionsBehavior.ReadOnly = True
        Me.gvDragValue.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDragValue.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDragValue.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDragValue.OptionsView.ColumnAutoWidth = False
        Me.gvDragValue.OptionsView.ShowGroupPanel = False
        '
        'cmbOperator
        '
        Me.cmbOperator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbOperator.EditValue = "Select Operator"
        Me.cmbOperator.Location = New System.Drawing.Point(3, 3)
        Me.cmbOperator.Name = "cmbOperator"
        Me.cmbOperator.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperator.Properties.Items.AddRange(New Object() {"Select Operator", "=", "<>", "<", ">", "<=", ">=", "like", "not like", "range", "in", "not in"})
        Me.cmbOperator.Size = New System.Drawing.Size(273, 20)
        Me.cmbOperator.TabIndex = 0
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.TableLayoutPanel4)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(291, 3)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(291, 291)
        Me.GroupControl4.TabIndex = 4
        Me.GroupControl4.Text = "2. Choose Value"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.gcValue, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txtSearchValue, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(287, 266)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'gcValue
        '
        Me.gcValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcValue.Location = New System.Drawing.Point(3, 28)
        Me.gcValue.MainView = Me.gvValue
        Me.gcValue.Name = "gcValue"
        Me.gcValue.Size = New System.Drawing.Size(281, 235)
        Me.gcValue.TabIndex = 17
        Me.gcValue.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvValue})
        '
        'gvValue
        '
        Me.gvValue.GridControl = Me.gcValue
        Me.gvValue.Name = "gvValue"
        Me.gvValue.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValue.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvValue.OptionsBehavior.Editable = False
        Me.gvValue.OptionsBehavior.ReadOnly = True
        Me.gvValue.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValue.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValue.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvValue.OptionsView.ColumnAutoWidth = False
        Me.gvValue.OptionsView.ShowGroupPanel = False
        '
        'txtSearchValue
        '
        Me.txtSearchValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchValue.Location = New System.Drawing.Point(3, 3)
        Me.txtSearchValue.Name = "txtSearchValue"
        Me.txtSearchValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearchValue.Properties.NullValuePrompt = "Search..."
        Me.txtSearchValue.Size = New System.Drawing.Size(281, 20)
        Me.txtSearchValue.TabIndex = 2
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.GroupControl6)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(3, 331)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(878, 88)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "THEN"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.TableLayoutPanel7)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(2, 23)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(874, 63)
        Me.GroupControl6.TabIndex = 0
        Me.GroupControl6.Text = "4. On true set parameter value"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.btnAddThen, 1, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.txtParamSetValue, 0, 1)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(870, 38)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'btnAddThen
        '
        Me.btnAddThen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddThen.Location = New System.Drawing.Point(772, 8)
        Me.btnAddThen.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddThen.Name = "btnAddThen"
        Me.btnAddThen.Size = New System.Drawing.Size(96, 21)
        Me.btnAddThen.TabIndex = 2
        Me.btnAddThen.Text = "Add"
        '
        'txtParamSetValue
        '
        Me.txtParamSetValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtParamSetValue.Location = New System.Drawing.Point(3, 9)
        Me.txtParamSetValue.Name = "txtParamSetValue"
        Me.txtParamSetValue.Size = New System.Drawing.Size(764, 20)
        Me.txtParamSetValue.TabIndex = 3
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.gcCondition, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btnCommit, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 492)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(878, 116)
        Me.TableLayoutPanel6.TabIndex = 3
        '
        'gcCondition
        '
        Me.gcCondition.AllowDrop = True
        Me.gcCondition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCondition.Location = New System.Drawing.Point(3, 3)
        Me.gcCondition.MainView = Me.gvCondition
        Me.gcCondition.Name = "gcCondition"
        Me.gcCondition.Size = New System.Drawing.Size(772, 110)
        Me.gcCondition.TabIndex = 18
        Me.gcCondition.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCondition})
        '
        'gvCondition
        '
        Me.gvCondition.GridControl = Me.gcCondition
        Me.gvCondition.Name = "gvCondition"
        Me.gvCondition.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCondition.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCondition.OptionsBehavior.Editable = False
        Me.gvCondition.OptionsBehavior.ReadOnly = True
        Me.gvCondition.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCondition.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCondition.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvCondition.OptionsView.ColumnAutoWidth = False
        Me.gvCondition.OptionsView.ShowGroupPanel = False
        '
        'btnCommit
        '
        Me.btnCommit.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnCommit.Location = New System.Drawing.Point(781, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(94, 25)
        Me.btnCommit.TabIndex = 0
        Me.btnCommit.Text = "Commit"
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.TableLayoutPanel8)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(3, 425)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(878, 61)
        Me.GroupControl7.TabIndex = 4
        Me.GroupControl7.Text = "ELSE"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.btnAddElse, 1, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.txtParamSetValueElse, 0, 1)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 3
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(874, 36)
        Me.TableLayoutPanel8.TabIndex = 1
        '
        'btnAddElse
        '
        Me.btnAddElse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddElse.Location = New System.Drawing.Point(776, 7)
        Me.btnAddElse.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddElse.Name = "btnAddElse"
        Me.btnAddElse.Size = New System.Drawing.Size(96, 21)
        Me.btnAddElse.TabIndex = 2
        Me.btnAddElse.Text = "Add"
        '
        'txtParamSetValueElse
        '
        Me.txtParamSetValueElse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtParamSetValueElse.Location = New System.Drawing.Point(3, 8)
        Me.txtParamSetValueElse.Name = "txtParamSetValueElse"
        Me.txtParamSetValueElse.Size = New System.Drawing.Size(768, 20)
        Me.txtParamSetValueElse.TabIndex = 3
        '
        'dlgCondition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 611)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 650)
        Me.Name = "dlgCondition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Condition Dialog"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.gcConditionFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConditionFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.gcDragValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDragValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperator.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.gcValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        CType(Me.txtParamSetValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.gcCondition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCondition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        CType(Me.txtParamSetValueElse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents cmbOperator As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents txtSearchField As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents txtSearchValue As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents btnCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents btnAddThen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcConditionFields As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvConditionFields As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcDragValue As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDragValue As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcValue As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvValue As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcCondition As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCondition As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtParamSetValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents btnAddElse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtParamSetValueElse As DevExpress.XtraEditors.TextEdit
End Class
