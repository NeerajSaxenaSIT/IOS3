<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEvalReportConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEvalReportConfig))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gcReportFilter = New DevExpress.XtraGrid.GridControl()
        Me.gvReportFilter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbCategory = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbField = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtValue = New DevExpress.XtraEditors.TextEdit()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRun = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout
        CType(Me.gcReportFilter,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.gvReportFilter,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tlpTop.SuspendLayout
        CType(Me.cmbCategory.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cmbField.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtValue.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tlpBottom.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tlpMain.Controls.Add(Me.gcReportFilter, 0, 2)
        Me.tlpMain.Controls.Add(Me.tlpTop, 0, 1)
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 3)
        Me.tlpMain.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 4
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpMain.Size = New System.Drawing.Size(598, 268)
        Me.tlpMain.TabIndex = 0
        '
        'gcReportFilter
        '
        Me.gcReportFilter.AllowDrop = True
        Me.gcReportFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcReportFilter.Location = New System.Drawing.Point(3, 63)
        Me.gcReportFilter.MainView = Me.gvReportFilter
        Me.gcReportFilter.Name = "gcReportFilter"
        Me.gcReportFilter.Size = New System.Drawing.Size(592, 170)
        Me.gcReportFilter.TabIndex = 4
        Me.gcReportFilter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReportFilter, Me.GridView1})
        '
        'gvReportFilter
        '
        Me.gvReportFilter.ActiveFilterEnabled = False
        Me.gvReportFilter.GridControl = Me.gcReportFilter
        Me.gvReportFilter.Name = "gvReportFilter"
        Me.gvReportFilter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportFilter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportFilter.OptionsBehavior.Editable = False
        Me.gvReportFilter.OptionsBehavior.ReadOnly = True
        Me.gvReportFilter.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportFilter.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportFilter.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportFilter.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvReportFilter.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportFilter.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportFilter.OptionsCustomization.AllowSort = False
        Me.gvReportFilter.OptionsMenu.EnableColumnMenu = False
        Me.gvReportFilter.OptionsMenu.EnableFooterMenu = False
        Me.gvReportFilter.OptionsView.ShowGroupPanel = False
        Me.gvReportFilter.OptionsView.ShowIndicator = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcReportFilter
        Me.GridView1.Name = "GridView1"
        '
        'tlpTop
        '
        Me.tlpTop.ColumnCount = 3
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpTop.Controls.Add(Me.cmbCategory, 0, 0)
        Me.tlpTop.Controls.Add(Me.cmbField, 1, 0)
        Me.tlpTop.Controls.Add(Me.txtValue, 2, 0)
        Me.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTop.Location = New System.Drawing.Point(1, 31)
        Me.tlpTop.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpTop.Name = "tlpTop"
        Me.tlpTop.RowCount = 1
        Me.tlpTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTop.Size = New System.Drawing.Size(596, 28)
        Me.tlpTop.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDelete.Location = New System.Drawing.Point(538, 2)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(56, 24)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAdd.Location = New System.Drawing.Point(478, 2)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(56, 24)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add"
        '
        'cmbCategory
        '
        Me.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbCategory.EditValue = "Select"
        Me.cmbCategory.Location = New System.Drawing.Point(3, 4)
        Me.cmbCategory.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCategory.Properties.Items.AddRange(New Object() {"Select", "KPITable", "HistogramTopX", "TimebasedCharts"})
        Me.cmbCategory.Size = New System.Drawing.Size(114, 20)
        Me.cmbCategory.TabIndex = 0
        '
        'cmbField
        '
        Me.cmbField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbField.Location = New System.Drawing.Point(123, 4)
        Me.cmbField.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbField.Name = "cmbField"
        Me.cmbField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbField.Size = New System.Drawing.Size(114, 20)
        Me.cmbField.TabIndex = 1
        '
        'txtValue
        '
        Me.txtValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValue.Location = New System.Drawing.Point(243, 4)
        Me.txtValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(350, 20)
        Me.txtValue.TabIndex = 2
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpBottom.Controls.Add(Me.btnRun, 1, 0)
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(1, 237)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(596, 30)
        Me.tlpBottom.TabIndex = 1
        '
        'btnRun
        '
        Me.btnRun.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRun.Location = New System.Drawing.Point(479, 3)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(114, 24)
        Me.btnRun.TabIndex = 2
        Me.btnRun.Text = "Run"
        '
        'lblMessage
        '
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(470, 24)
        Me.lblMessage.TabIndex = 3
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnAdd, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnDelete, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl3, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(596, 28)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LabelControl1.ImageOptions.Image = CType(resources.GetObject("LabelControl1.ImageOptions.Image"), System.Drawing.Image)
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(114, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Category"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LabelControl2.ImageOptions.Image = CType(resources.GetObject("LabelControl2.ImageOptions.Image"), System.Drawing.Image)
        Me.LabelControl2.Location = New System.Drawing.Point(125, 3)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(112, 22)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Field"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LabelControl3.ImageOptions.Image = CType(resources.GetObject("LabelControl3.ImageOptions.Image"), System.Drawing.Image)
        Me.LabelControl3.Location = New System.Drawing.Point(245, 3)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(228, 22)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Value"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgEvalReportConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(598, 268)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("dlgEvalReportConfig.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(600, 300)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 300)
        Me.Name = "dlgEvalReportConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Evaluate Report Filter"
        Me.tlpMain.ResumeLayout(false)
        CType(Me.gcReportFilter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.gvReportFilter,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        Me.tlpTop.ResumeLayout(false)
        CType(Me.cmbCategory.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbField.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtValue.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.tlpBottom.ResumeLayout(false)
        Me.tlpBottom.PerformLayout
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpTop As TableLayoutPanel
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents gcReportFilter As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReportFilter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbCategory As devexpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbField As devexpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtValue As devexpress.XtraEditors.TextEdit
    Friend WithEvents btnAdd As devexpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As devexpress.XtraEditors.SimpleButton
    Friend WithEvents btnRun As devexpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LabelControl1 As devexpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As devexpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As devexpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
End Class
