<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgManageMapTheme
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgManageMapTheme))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.grpCreateTheme = New DevExpress.XtraEditors.GroupControl()
        Me.tlpCreateTheme = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtThemeName = New DevExpress.XtraEditors.TextEdit()
        Me.btnCreateTheme = New DevExpress.XtraEditors.SimpleButton()
        Me.grpEditTheme = New DevExpress.XtraEditors.GroupControl()
        Me.tlpEditTheme = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbThemeName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tlpEditBtn = New System.Windows.Forms.TableLayoutPanel()
        Me.btnUpdateTheme = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteTheme = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.grpCreateTheme, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCreateTheme.SuspendLayout()
        Me.tlpCreateTheme.SuspendLayout()
        CType(Me.txtThemeName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpEditTheme, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEditTheme.SuspendLayout()
        Me.tlpEditTheme.SuspendLayout()
        CType(Me.cmbThemeName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpEditBtn.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 2)
        Me.tlpMain.Controls.Add(Me.grpCreateTheme, 0, 0)
        Me.tlpMain.Controls.Add(Me.grpEditTheme, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpMain.Size = New System.Drawing.Size(438, 190)
        Me.tlpMain.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 153)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(432, 34)
        Me.lblMessage.TabIndex = 14
        '
        'grpCreateTheme
        '
        Me.grpCreateTheme.Controls.Add(Me.tlpCreateTheme)
        Me.grpCreateTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCreateTheme.Location = New System.Drawing.Point(3, 3)
        Me.grpCreateTheme.Name = "grpCreateTheme"
        Me.grpCreateTheme.Size = New System.Drawing.Size(432, 69)
        Me.grpCreateTheme.TabIndex = 0
        Me.grpCreateTheme.Text = "Create Theme"
        '
        'tlpCreateTheme
        '
        Me.tlpCreateTheme.ColumnCount = 3
        Me.tlpCreateTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpCreateTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCreateTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpCreateTheme.Controls.Add(Me.LabelControl1, 0, 1)
        Me.tlpCreateTheme.Controls.Add(Me.txtThemeName, 1, 1)
        Me.tlpCreateTheme.Controls.Add(Me.btnCreateTheme, 2, 1)
        Me.tlpCreateTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCreateTheme.Location = New System.Drawing.Point(2, 22)
        Me.tlpCreateTheme.Name = "tlpCreateTheme"
        Me.tlpCreateTheme.RowCount = 3
        Me.tlpCreateTheme.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpCreateTheme.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpCreateTheme.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpCreateTheme.Size = New System.Drawing.Size(428, 45)
        Me.tlpCreateTheme.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(74, 26)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Theme Name"
        '
        'txtThemeName
        '
        Me.txtThemeName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtThemeName.Location = New System.Drawing.Point(83, 12)
        Me.txtThemeName.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtThemeName.Name = "txtThemeName"
        Me.txtThemeName.Properties.MaxLength = 200
        Me.txtThemeName.Size = New System.Drawing.Size(222, 20)
        Me.txtThemeName.TabIndex = 1
        '
        'btnCreateTheme
        '
        Me.btnCreateTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCreateTheme.Location = New System.Drawing.Point(310, 8)
        Me.btnCreateTheme.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCreateTheme.Name = "btnCreateTheme"
        Me.btnCreateTheme.Size = New System.Drawing.Size(116, 28)
        Me.btnCreateTheme.TabIndex = 2
        Me.btnCreateTheme.Text = "Create"
        '
        'grpEditTheme
        '
        Me.grpEditTheme.Controls.Add(Me.tlpEditTheme)
        Me.grpEditTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpEditTheme.Location = New System.Drawing.Point(3, 78)
        Me.grpEditTheme.Name = "grpEditTheme"
        Me.grpEditTheme.Size = New System.Drawing.Size(432, 69)
        Me.grpEditTheme.TabIndex = 1
        Me.grpEditTheme.Text = "Edit Theme"
        '
        'tlpEditTheme
        '
        Me.tlpEditTheme.ColumnCount = 3
        Me.tlpEditTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpEditTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpEditTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpEditTheme.Controls.Add(Me.LabelControl2, 0, 1)
        Me.tlpEditTheme.Controls.Add(Me.cmbThemeName, 1, 1)
        Me.tlpEditTheme.Controls.Add(Me.tlpEditBtn, 2, 1)
        Me.tlpEditTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpEditTheme.Location = New System.Drawing.Point(2, 22)
        Me.tlpEditTheme.Name = "tlpEditTheme"
        Me.tlpEditTheme.RowCount = 3
        Me.tlpEditTheme.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEditTheme.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpEditTheme.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEditTheme.Size = New System.Drawing.Size(428, 45)
        Me.tlpEditTheme.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 9)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(74, 26)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Select Theme"
        '
        'cmbThemeName
        '
        Me.cmbThemeName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbThemeName.Location = New System.Drawing.Point(83, 12)
        Me.cmbThemeName.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.cmbThemeName.Name = "cmbThemeName"
        Me.cmbThemeName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbThemeName.Size = New System.Drawing.Size(222, 20)
        Me.cmbThemeName.TabIndex = 1
        '
        'tlpEditBtn
        '
        Me.tlpEditBtn.ColumnCount = 2
        Me.tlpEditBtn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEditBtn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEditBtn.Controls.Add(Me.btnUpdateTheme, 0, 0)
        Me.tlpEditBtn.Controls.Add(Me.btnDeleteTheme, 1, 0)
        Me.tlpEditBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpEditBtn.Location = New System.Drawing.Point(308, 6)
        Me.tlpEditBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpEditBtn.Name = "tlpEditBtn"
        Me.tlpEditBtn.RowCount = 1
        Me.tlpEditBtn.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpEditBtn.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpEditBtn.Size = New System.Drawing.Size(120, 32)
        Me.tlpEditBtn.TabIndex = 2
        '
        'btnUpdateTheme
        '
        Me.btnUpdateTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUpdateTheme.Location = New System.Drawing.Point(2, 2)
        Me.btnUpdateTheme.Margin = New System.Windows.Forms.Padding(2)
        Me.btnUpdateTheme.Name = "btnUpdateTheme"
        Me.btnUpdateTheme.Size = New System.Drawing.Size(56, 28)
        Me.btnUpdateTheme.TabIndex = 0
        Me.btnUpdateTheme.Text = "Update"
        '
        'btnDeleteTheme
        '
        Me.btnDeleteTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDeleteTheme.Location = New System.Drawing.Point(62, 2)
        Me.btnDeleteTheme.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDeleteTheme.Name = "btnDeleteTheme"
        Me.btnDeleteTheme.Size = New System.Drawing.Size(56, 28)
        Me.btnDeleteTheme.TabIndex = 1
        Me.btnDeleteTheme.Text = "Delete"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgManageMapTheme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 190)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("dlgManageMapTheme.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(444, 222)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(444, 222)
        Me.Name = "dlgManageMapTheme"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Theme"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.grpCreateTheme, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCreateTheme.ResumeLayout(False)
        Me.tlpCreateTheme.ResumeLayout(False)
        Me.tlpCreateTheme.PerformLayout()
        CType(Me.txtThemeName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpEditTheme, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEditTheme.ResumeLayout(False)
        Me.tlpEditTheme.ResumeLayout(False)
        Me.tlpEditTheme.PerformLayout()
        CType(Me.cmbThemeName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpEditBtn.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents grpCreateTheme As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpEditTheme As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpCreateTheme As TableLayoutPanel
    Friend WithEvents tlpEditTheme As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtThemeName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbThemeName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnCreateTheme As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpEditBtn As TableLayoutPanel
    Friend WithEvents btnUpdateTheme As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteTheme As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
End Class
