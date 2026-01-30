<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgObjFilter
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgObjFilter))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcField = New DevExpress.XtraGrid.GridControl()
        Me.gvField = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtFieldSearch = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSearch = New DevExpress.XtraEditors.ButtonEdit()
        Me.gcColumnValue = New DevExpress.XtraGrid.GridControl()
        Me.gvColumnValue = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcDragValue = New DevExpress.XtraGrid.GridControl()
        Me.gvDragValue = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbOperator = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddFilter2AllMO = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddFilterAllKpiRules = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.gcField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcColumnValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvColumnValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcDragValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDragValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperator.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl3, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnAddFilterAllKpiRules, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(884, 462)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel4)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(285, 421)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "1. Choose Field"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.gcField, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txtFieldSearch, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(281, 396)
        Me.TableLayoutPanel4.TabIndex = 4
        '
        'gcField
        '
        Me.gcField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcField.Location = New System.Drawing.Point(3, 28)
        Me.gcField.MainView = Me.gvField
        Me.gcField.Name = "gcField"
        Me.gcField.Size = New System.Drawing.Size(275, 365)
        Me.gcField.TabIndex = 10
        Me.gcField.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvField})
        '
        'gvField
        '
        Me.gvField.GridControl = Me.gcField
        Me.gvField.Name = "gvField"
        Me.gvField.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvField.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvField.OptionsBehavior.Editable = False
        Me.gvField.OptionsBehavior.ReadOnly = True
        Me.gvField.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvField.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvField.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvField.OptionsView.ColumnAutoWidth = False
        Me.gvField.OptionsView.ShowGroupPanel = False
        '
        'txtFieldSearch
        '
        Me.txtFieldSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFieldSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtFieldSearch.Name = "txtFieldSearch"
        Me.txtFieldSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtFieldSearch.Properties.NullValuePrompt = "Search..."
        Me.txtFieldSearch.Size = New System.Drawing.Size(275, 20)
        Me.txtFieldSearch.TabIndex = 2
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(294, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(294, 421)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "2. Choose Value"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.txtSearch, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.gcColumnValue, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(290, 396)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearch.Properties.NullValuePrompt = "Search..."
        Me.txtSearch.Size = New System.Drawing.Size(284, 20)
        Me.txtSearch.TabIndex = 2
        '
        'gcColumnValue
        '
        Me.gcColumnValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcColumnValue.Location = New System.Drawing.Point(3, 28)
        Me.gcColumnValue.MainView = Me.gvColumnValue
        Me.gcColumnValue.Name = "gcColumnValue"
        Me.gcColumnValue.Size = New System.Drawing.Size(284, 365)
        Me.gcColumnValue.TabIndex = 9
        Me.gcColumnValue.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvColumnValue})
        '
        'gvColumnValue
        '
        Me.gvColumnValue.GridControl = Me.gcColumnValue
        Me.gvColumnValue.Name = "gvColumnValue"
        Me.gvColumnValue.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvColumnValue.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvColumnValue.OptionsBehavior.Editable = False
        Me.gvColumnValue.OptionsBehavior.ReadOnly = True
        Me.gvColumnValue.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvColumnValue.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvColumnValue.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvColumnValue.OptionsView.ColumnAutoWidth = False
        Me.gvColumnValue.OptionsView.ShowGroupPanel = False
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(594, 3)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(287, 421)
        Me.GroupControl3.TabIndex = 2
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
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(283, 396)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'gcDragValue
        '
        Me.gcDragValue.AllowDrop = True
        Me.gcDragValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDragValue.Location = New System.Drawing.Point(3, 28)
        Me.gcDragValue.MainView = Me.gvDragValue
        Me.gcDragValue.Name = "gcDragValue"
        Me.gcDragValue.Size = New System.Drawing.Size(277, 365)
        Me.gcDragValue.TabIndex = 9
        Me.gcDragValue.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDragValue})
        '
        'gvDragValue
        '
        Me.gvDragValue.GridControl = Me.gcDragValue
        Me.gvDragValue.Name = "gvDragValue"
        Me.gvDragValue.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDragValue.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
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
        Me.cmbOperator.Size = New System.Drawing.Size(277, 20)
        Me.cmbOperator.TabIndex = 0
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.btnAddFilter2AllMO, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.btnAddFilter, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(591, 427)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(293, 35)
        Me.TableLayoutPanel5.TabIndex = 4
        '
        'btnAddFilter2AllMO
        '
        Me.btnAddFilter2AllMO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddFilter2AllMO.Location = New System.Drawing.Point(3, 3)
        Me.btnAddFilter2AllMO.Name = "btnAddFilter2AllMO"
        Me.btnAddFilter2AllMO.Size = New System.Drawing.Size(140, 29)
        Me.btnAddFilter2AllMO.TabIndex = 4
        Me.btnAddFilter2AllMO.Text = "Add Filter to ALL MO"
        '
        'btnAddFilter
        '
        Me.btnAddFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddFilter.Location = New System.Drawing.Point(149, 3)
        Me.btnAddFilter.Name = "btnAddFilter"
        Me.btnAddFilter.Size = New System.Drawing.Size(141, 29)
        Me.btnAddFilter.TabIndex = 3
        Me.btnAddFilter.Text = "Add Filter"
        '
        'btnAddFilterAllKpiRules
        '
        Me.btnAddFilterAllKpiRules.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddFilterAllKpiRules.Location = New System.Drawing.Point(443, 430)
        Me.btnAddFilterAllKpiRules.Name = "btnAddFilterAllKpiRules"
        Me.btnAddFilterAllKpiRules.Size = New System.Drawing.Size(145, 29)
        Me.btnAddFilterAllKpiRules.TabIndex = 5
        Me.btnAddFilterAllKpiRules.Text = "Add Filter to ALL KPI Rules"
        '
        'dlgObjFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 462)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 500)
        Me.Name = "dlgObjFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Object Filter Dialog"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.gcField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.txtSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcColumnValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvColumnValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.gcDragValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDragValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperator.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnAddFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents txtSearch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents cmbOperator As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents gcColumnValue As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvColumnValue As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcDragValue As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDragValue As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents txtFieldSearch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents gcField As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvField As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents btnAddFilter2AllMO As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddFilterAllKpiRules As DevExpress.XtraEditors.SimpleButton
End Class
