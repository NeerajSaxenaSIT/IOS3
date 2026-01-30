<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgChartCategory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgChartCategory))
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel1 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCharCategoryInsert = New DevExpress.XtraEditors.SimpleButton()
        Me.txtChartCategoryName = New DevExpress.XtraEditors.TextEdit()
        Me.lblCategoryIndex = New System.Windows.Forms.Label()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.txtChartCategoryName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblMessage, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(365, 108)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtChartCategoryName, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblCategoryIndex, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(361, 65)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'VLabel1
        '
        Me.VLabel1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.Appearance.Options.UseBackColor = True
        Me.VLabel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel1.Location = New System.Drawing.Point(3, 3)
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.VLabel1.Size = New System.Drawing.Size(87, 22)
        Me.VLabel1.TabIndex = 6
        Me.VLabel1.Text = "Category Tab"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btnCharCategoryInsert, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(95, 30)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(264, 34)
        Me.TableLayoutPanel4.TabIndex = 11
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(161, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 28)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnCharCategoryInsert
        '
        Me.btnCharCategoryInsert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCharCategoryInsert.Location = New System.Drawing.Point(3, 3)
        Me.btnCharCategoryInsert.Name = "btnCharCategoryInsert"
        Me.btnCharCategoryInsert.Size = New System.Drawing.Size(152, 28)
        Me.btnCharCategoryInsert.TabIndex = 0
        Me.btnCharCategoryInsert.Text = "Insert Category"
        '
        'txtChartCategoryName
        '
        Me.txtChartCategoryName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtChartCategoryName.Location = New System.Drawing.Point(96, 4)
        Me.txtChartCategoryName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 2)
        Me.txtChartCategoryName.Name = "txtChartCategoryName"
        Me.txtChartCategoryName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtChartCategoryName.Properties.Appearance.Options.UseBackColor = True
        Me.txtChartCategoryName.Size = New System.Drawing.Size(262, 20)
        Me.txtChartCategoryName.TabIndex = 14
        '
        'lblCategoryIndex
        '
        Me.lblCategoryIndex.AutoSize = True
        Me.lblCategoryIndex.Location = New System.Drawing.Point(4, 28)
        Me.lblCategoryIndex.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCategoryIndex.Name = "lblCategoryIndex"
        Me.lblCategoryIndex.Size = New System.Drawing.Size(0, 13)
        Me.lblCategoryIndex.TabIndex = 15
        Me.lblCategoryIndex.Visible = False
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMessage.Appearance.Options.UseBackColor = True
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 72)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(359, 33)
        Me.lblMessage.TabIndex = 13
        Me.lblMessage.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgChartCategory
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(365, 108)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("dlgChartCategory.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(375, 140)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(375, 140)
        Me.Name = "dlgChartCategory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Insert Chart Category"
        Me.TopMost = True
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.txtChartCategoryName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtChartCategoryName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCategoryIndex As System.Windows.Forms.Label
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCharCategoryInsert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
End Class
