<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSQLQuery
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSQLQuery))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSaveTemplate = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLoadTemplate = New DevExpress.XtraEditors.SimpleButton()
        Me.txtDestinationTable = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.txtGroupBy = New DevExpress.XtraEditors.TextEdit()
        Me.txtOrderBy = New DevExpress.XtraEditors.TextEdit()
        Me.txtSelect = New DevExpress.XtraEditors.MemoEdit()
        Me.txtWhere = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lstTables = New DevExpress.XtraEditors.ListBoxControl()
        Me.lstOperators = New DevExpress.XtraEditors.ListBoxControl()
        Me.lstFields = New DevExpress.XtraEditors.ListBoxControl()
        Me.lstAggregateFunction = New DevExpress.XtraEditors.ListBoxControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExecute = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTest = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtDestinationTable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.txtGroupBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSelect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhere.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.lstTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstOperators, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstAggregateFunction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.SplitContainerControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelControl1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1059, 636)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 354.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnSaveTemplate, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnLoadTemplate, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtDestinationTable, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl6, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 567)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1055, 32)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'btnSaveTemplate
        '
        Me.btnSaveTemplate.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSaveTemplate.Location = New System.Drawing.Point(744, 3)
        Me.btnSaveTemplate.Name = "btnSaveTemplate"
        Me.btnSaveTemplate.Size = New System.Drawing.Size(150, 26)
        Me.btnSaveTemplate.TabIndex = 0
        Me.btnSaveTemplate.Text = "&Save Template"
        '
        'btnLoadTemplate
        '
        Me.btnLoadTemplate.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLoadTemplate.Location = New System.Drawing.Point(902, 3)
        Me.btnLoadTemplate.Name = "btnLoadTemplate"
        Me.btnLoadTemplate.Size = New System.Drawing.Size(150, 26)
        Me.btnLoadTemplate.TabIndex = 1
        Me.btnLoadTemplate.Text = "&Load Template"
        '
        'txtDestinationTable
        '
        Me.txtDestinationTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDestinationTable.Location = New System.Drawing.Point(131, 3)
        Me.txtDestinationTable.Name = "txtDestinationTable"
        Me.txtDestinationTable.Size = New System.Drawing.Size(409, 20)
        Me.txtDestinationTable.TabIndex = 2
        '
        'LabelControl6
        '
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(122, 26)
        Me.LabelControl6.TabIndex = 3
        Me.LabelControl6.Text = "Destination Table:"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.TableLayoutPanel3)
        Me.SplitContainerControl1.Panel1.MinSize = 500
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TableLayoutPanel4)
        Me.SplitContainerControl1.Panel2.MinSize = 300
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1053, 559)
        Me.SplitContainerControl1.SplitterPosition = 500
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.txtFrom, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.txtGroupBy, 0, 7)
        Me.TableLayoutPanel3.Controls.Add(Me.txtOrderBy, 0, 9)
        Me.TableLayoutPanel3.Controls.Add(Me.txtSelect, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.txtWhere, 0, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl2, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl3, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl4, 0, 6)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl5, 0, 8)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 10
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(500, 559)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'txtFrom
        '
        Me.txtFrom.AllowDrop = True
        Me.txtFrom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFrom.Location = New System.Drawing.Point(3, 31)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(494, 21)
        Me.txtFrom.TabIndex = 0
        '
        'txtGroupBy
        '
        Me.txtGroupBy.AllowDrop = True
        Me.txtGroupBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGroupBy.Location = New System.Drawing.Point(3, 477)
        Me.txtGroupBy.Name = "txtGroupBy"
        Me.txtGroupBy.Size = New System.Drawing.Size(494, 20)
        Me.txtGroupBy.TabIndex = 1
        '
        'txtOrderBy
        '
        Me.txtOrderBy.AllowDrop = True
        Me.txtOrderBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOrderBy.Location = New System.Drawing.Point(3, 533)
        Me.txtOrderBy.Name = "txtOrderBy"
        Me.txtOrderBy.Size = New System.Drawing.Size(494, 20)
        Me.txtOrderBy.TabIndex = 2
        '
        'txtSelect
        '
        Me.txtSelect.AllowDrop = True
        Me.txtSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSelect.Location = New System.Drawing.Point(3, 87)
        Me.txtSelect.Name = "txtSelect"
        Me.txtSelect.Size = New System.Drawing.Size(494, 161)
        Me.txtSelect.TabIndex = 3
        '
        'txtWhere
        '
        Me.txtWhere.AllowDrop = True
        Me.txtWhere.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtWhere.Location = New System.Drawing.Point(3, 282)
        Me.txtWhere.Name = "txtWhere"
        Me.txtWhere.Size = New System.Drawing.Size(494, 161)
        Me.txtWhere.TabIndex = 4
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(494, 22)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "From"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(494, 22)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Select"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 254)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(494, 22)
        Me.LabelControl3.TabIndex = 7
        Me.LabelControl3.Text = "Where"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 449)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(494, 22)
        Me.LabelControl4.TabIndex = 8
        Me.LabelControl4.Text = "Group By"
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 505)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(494, 22)
        Me.LabelControl5.TabIndex = 9
        Me.LabelControl5.Text = "Order By"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.lstTables, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.lstOperators, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.lstFields, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.lstAggregateFunction, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl7, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl8, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl9, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl10, 1, 2)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 5
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(548, 559)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'lstTables
        '
        Me.lstTables.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTables.Location = New System.Drawing.Point(3, 31)
        Me.lstTables.Name = "lstTables"
        Me.lstTables.Size = New System.Drawing.Size(372, 232)
        Me.lstTables.TabIndex = 0
        '
        'lstOperators
        '
        Me.lstOperators.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstOperators.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOperators.Items.AddRange(New Object() {"=", "<>", ">", "<", "<=", ">=", "+", "-", "*", "/", "^", "()", "AND", "OR", "NOT", "LIKE", "Contains", "Contains Entire", "Within", "Entirely Within", "Intersects"})
        Me.lstOperators.Location = New System.Drawing.Point(381, 31)
        Me.lstOperators.Name = "lstOperators"
        Me.lstOperators.Size = New System.Drawing.Size(164, 232)
        Me.lstOperators.TabIndex = 1
        '
        'lstFields
        '
        Me.lstFields.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstFields.Location = New System.Drawing.Point(3, 297)
        Me.lstFields.Name = "lstFields"
        Me.lstFields.Size = New System.Drawing.Size(372, 232)
        Me.lstFields.TabIndex = 2
        '
        'lstAggregateFunction
        '
        Me.lstAggregateFunction.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstAggregateFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstAggregateFunction.Items.AddRange(New Object() {"Avg()", "Sum()", "Count()", "Min()", "Max()"})
        Me.lstAggregateFunction.Location = New System.Drawing.Point(381, 297)
        Me.lstAggregateFunction.Name = "lstAggregateFunction"
        Me.lstAggregateFunction.Size = New System.Drawing.Size(164, 232)
        Me.lstAggregateFunction.TabIndex = 3
        '
        'LabelControl7
        '
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(372, 22)
        Me.LabelControl7.TabIndex = 4
        Me.LabelControl7.Text = "List of Tables"
        '
        'LabelControl8
        '
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(381, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl8.TabIndex = 5
        Me.LabelControl8.Text = "List of Operators"
        '
        'LabelControl9
        '
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 269)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(372, 22)
        Me.LabelControl9.TabIndex = 6
        Me.LabelControl9.Text = "List of Fields"
        '
        'LabelControl10
        '
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(381, 269)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl10.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl10.TabIndex = 7
        Me.LabelControl10.Text = "Aggregate Funtions"
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.btnCancel)
        Me.PanelControl1.Controls.Add(Me.btnExecute)
        Me.PanelControl1.Controls.Add(Me.btnTest)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(3, 604)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1053, 29)
        Me.PanelControl1.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(633, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(150, 26)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Clear"
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(450, 0)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(150, 26)
        Me.btnExecute.TabIndex = 1
        Me.btnExecute.Text = "&Execute"
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(267, 0)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(150, 26)
        Me.btnTest.TabIndex = 0
        Me.btnTest.Text = "&Verify"
        '
        'frmSQLQuery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1059, 636)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "frmSQLQuery"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GIS - Query Builder"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.txtDestinationTable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.txtGroupBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSelect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhere.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.lstTables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstOperators, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstAggregateFunction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSaveTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLoadTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtDestinationTable As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtGroupBy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtOrderBy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSelect As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtWhere As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExecute As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lstTables As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents lstOperators As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents lstFields As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents lstAggregateFunction As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
End Class
