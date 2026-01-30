<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRefChkBulkDelete
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRefChkBulkDelete))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbItem = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnBulkDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDeleteInfo = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.cmbItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.cmbItem, 1, 0)
        Me.tlpMain.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 2)
        Me.tlpMain.Controls.Add(Me.lblDeleteInfo, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Size = New System.Drawing.Size(665, 318)
        Me.tlpMain.TabIndex = 1
        '
        'cmbItem
        '
        Me.cmbItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbItem.Location = New System.Drawing.Point(123, 4)
        Me.cmbItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbItem.Size = New System.Drawing.Size(539, 20)
        Me.cmbItem.TabIndex = 8
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(114, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Select Item To Delete"
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpMain.SetColumnSpan(Me.tlpBottom, 2)
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnBulkDelete, 1, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(0, 283)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(665, 35)
        Me.tlpBottom.TabIndex = 9
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(559, 29)
        Me.lblMessage.TabIndex = 18
        '
        'btnBulkDelete
        '
        Me.btnBulkDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnBulkDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnBulkDelete.Location = New System.Drawing.Point(567, 2)
        Me.btnBulkDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnBulkDelete.Name = "btnBulkDelete"
        Me.btnBulkDelete.Size = New System.Drawing.Size(96, 31)
        Me.btnBulkDelete.TabIndex = 5
        Me.btnBulkDelete.Text = "Delete"
        '
        'lblDeleteInfo
        '
        Me.lblDeleteInfo.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblDeleteInfo.Appearance.Options.UseForeColor = True
        Me.lblDeleteInfo.Appearance.Options.UseTextOptions = True
        Me.lblDeleteInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblDeleteInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblDeleteInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.tlpMain.SetColumnSpan(Me.lblDeleteInfo, 2)
        Me.lblDeleteInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDeleteInfo.Location = New System.Drawing.Point(3, 31)
        Me.lblDeleteInfo.Name = "lblDeleteInfo"
        Me.lblDeleteInfo.Padding = New System.Windows.Forms.Padding(4, 5, 0, 0)
        Me.lblDeleteInfo.Size = New System.Drawing.Size(659, 249)
        Me.lblDeleteInfo.TabIndex = 1
        Me.lblDeleteInfo.Text = "Bulk Delete Info:"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'frmRefChkBulkDelete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 318)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("frmRefChkBulkDelete.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(675, 350)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(675, 350)
        Me.Name = "frmRefChkBulkDelete"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ref Check - Bulk Delete"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.cmbItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents cmbItem As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDeleteInfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnBulkDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
End Class
