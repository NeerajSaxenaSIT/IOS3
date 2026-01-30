<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRefChkBulkUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRefChkBulkUpdate))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbItem = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblCombo = New DevExpress.XtraEditors.LabelControl()
        Me.lblSetNewValue = New DevExpress.XtraEditors.LabelControl()
        Me.txtNewValue = New DevExpress.XtraEditors.TextEdit()
        Me.lblUpdateInfo = New DevExpress.XtraEditors.LabelControl()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnBulkUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.lblListType = New DevExpress.XtraEditors.LabelControl()
        Me.tlpListType = New System.Windows.Forms.TableLayoutPanel()
        Me.rdoInclusion = New System.Windows.Forms.RadioButton()
        Me.rdoExclusion = New System.Windows.Forms.RadioButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.cmbItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpBottom.SuspendLayout()
        Me.tlpListType.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.cmbItem, 1, 0)
        Me.tlpMain.Controls.Add(Me.lblCombo, 0, 0)
        Me.tlpMain.Controls.Add(Me.lblSetNewValue, 0, 1)
        Me.tlpMain.Controls.Add(Me.txtNewValue, 1, 1)
        Me.tlpMain.Controls.Add(Me.lblUpdateInfo, 0, 3)
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 4)
        Me.tlpMain.Controls.Add(Me.lblListType, 0, 2)
        Me.tlpMain.Controls.Add(Me.tlpListType, 1, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 5
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.Size = New System.Drawing.Size(665, 318)
        Me.tlpMain.TabIndex = 0
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
        'lblCombo
        '
        Me.lblCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCombo.Location = New System.Drawing.Point(3, 3)
        Me.lblCombo.Name = "lblCombo"
        Me.lblCombo.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblCombo.Size = New System.Drawing.Size(114, 22)
        Me.lblCombo.TabIndex = 0
        Me.lblCombo.Text = "Select Item"
        '
        'lblSetNewValue
        '
        Me.lblSetNewValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSetNewValue.Location = New System.Drawing.Point(3, 31)
        Me.lblSetNewValue.Name = "lblSetNewValue"
        Me.lblSetNewValue.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblSetNewValue.Size = New System.Drawing.Size(114, 22)
        Me.lblSetNewValue.TabIndex = 6
        Me.lblSetNewValue.Text = "Set New Value"
        '
        'txtNewValue
        '
        Me.txtNewValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNewValue.Location = New System.Drawing.Point(123, 32)
        Me.txtNewValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txtNewValue.Name = "txtNewValue"
        Me.txtNewValue.Size = New System.Drawing.Size(539, 20)
        Me.txtNewValue.TabIndex = 7
        '
        'lblUpdateInfo
        '
        Me.lblUpdateInfo.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblUpdateInfo.Appearance.Options.UseForeColor = True
        Me.lblUpdateInfo.Appearance.Options.UseTextOptions = True
        Me.lblUpdateInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblUpdateInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblUpdateInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.tlpMain.SetColumnSpan(Me.lblUpdateInfo, 2)
        Me.lblUpdateInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUpdateInfo.Location = New System.Drawing.Point(3, 87)
        Me.lblUpdateInfo.Name = "lblUpdateInfo"
        Me.lblUpdateInfo.Padding = New System.Windows.Forms.Padding(4, 5, 0, 0)
        Me.lblUpdateInfo.Size = New System.Drawing.Size(659, 193)
        Me.lblUpdateInfo.TabIndex = 1
        Me.lblUpdateInfo.Text = "Bulk Update Info:"
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpMain.SetColumnSpan(Me.tlpBottom, 2)
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnBulkUpdate, 1, 0)
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
        'btnBulkUpdate
        '
        Me.btnBulkUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnBulkUpdate.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnBulkUpdate.Location = New System.Drawing.Point(567, 2)
        Me.btnBulkUpdate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnBulkUpdate.Name = "btnBulkUpdate"
        Me.btnBulkUpdate.Size = New System.Drawing.Size(96, 31)
        Me.btnBulkUpdate.TabIndex = 5
        Me.btnBulkUpdate.Text = "Update"
        '
        'lblListType
        '
        Me.lblListType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblListType.Location = New System.Drawing.Point(3, 59)
        Me.lblListType.Name = "lblListType"
        Me.lblListType.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblListType.Size = New System.Drawing.Size(114, 22)
        Me.lblListType.TabIndex = 12
        Me.lblListType.Text = "Select List Type"
        '
        'tlpListType
        '
        Me.tlpListType.ColumnCount = 3
        Me.tlpListType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpListType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpListType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpListType.Controls.Add(Me.rdoInclusion, 0, 0)
        Me.tlpListType.Controls.Add(Me.rdoExclusion, 1, 0)
        Me.tlpListType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpListType.Location = New System.Drawing.Point(120, 56)
        Me.tlpListType.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpListType.Name = "tlpListType"
        Me.tlpListType.RowCount = 1
        Me.tlpListType.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpListType.Size = New System.Drawing.Size(545, 28)
        Me.tlpListType.TabIndex = 13
        '
        'rdoInclusion
        '
        Me.rdoInclusion.AutoSize = True
        Me.rdoInclusion.Checked = True
        Me.rdoInclusion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoInclusion.Location = New System.Drawing.Point(5, 3)
        Me.rdoInclusion.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.rdoInclusion.Name = "rdoInclusion"
        Me.rdoInclusion.Size = New System.Drawing.Size(92, 22)
        Me.rdoInclusion.TabIndex = 0
        Me.rdoInclusion.TabStop = True
        Me.rdoInclusion.Text = "Inclusion"
        Me.rdoInclusion.UseVisualStyleBackColor = True
        '
        'rdoExclusion
        '
        Me.rdoExclusion.AutoSize = True
        Me.rdoExclusion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoExclusion.Location = New System.Drawing.Point(103, 3)
        Me.rdoExclusion.Name = "rdoExclusion"
        Me.rdoExclusion.Size = New System.Drawing.Size(114, 22)
        Me.rdoExclusion.TabIndex = 1
        Me.rdoExclusion.Text = "Exclusion"
        Me.rdoExclusion.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'frmRefChkBulkUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 318)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("frmRefChkBulkUpdate.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(675, 350)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(675, 350)
        Me.Name = "frmRefChkBulkUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ref Check - Bulk Update"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.cmbItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        Me.tlpListType.ResumeLayout(False)
        Me.tlpListType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents lblCombo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUpdateInfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnBulkUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblSetNewValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNewValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbItem As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblListType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpListType As TableLayoutPanel
    Friend WithEvents rdoInclusion As RadioButton
    Friend WithEvents rdoExclusion As RadioButton
End Class
