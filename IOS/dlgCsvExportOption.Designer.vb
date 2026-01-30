<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgCsvExportOption
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.tlpBtns = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpDelimiter = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDelimiter = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpBtns.SuspendLayout()
        Me.tlpDelimiter.SuspendLayout()
        CType(Me.cmbDelimiter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 4)
        Me.tlpMain.Controls.Add(Me.tlpBtns, 0, 3)
        Me.tlpMain.Controls.Add(Me.tlpDelimiter, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 5
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Size = New System.Drawing.Size(334, 128)
        Me.tlpMain.TabIndex = 2
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 93)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(328, 32)
        Me.lblMessage.TabIndex = 21
        '
        'tlpBtns
        '
        Me.tlpBtns.ColumnCount = 4
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.tlpBtns.Controls.Add(Me.btnOk, 1, 0)
        Me.tlpBtns.Controls.Add(Me.btnCancel, 2, 0)
        Me.tlpBtns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBtns.Location = New System.Drawing.Point(1, 56)
        Me.tlpBtns.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpBtns.Name = "tlpBtns"
        Me.tlpBtns.RowCount = 1
        Me.tlpBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBtns.Size = New System.Drawing.Size(332, 33)
        Me.tlpBtns.TabIndex = 3
        '
        'btnOk
        '
        Me.btnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOk.Location = New System.Drawing.Point(18, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(145, 27)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "Ok"
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(169, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(145, 27)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'tlpDelimiter
        '
        Me.tlpDelimiter.ColumnCount = 2
        Me.tlpDelimiter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.tlpDelimiter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDelimiter.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpDelimiter.Controls.Add(Me.cmbDelimiter, 1, 0)
        Me.tlpDelimiter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpDelimiter.Location = New System.Drawing.Point(3, 13)
        Me.tlpDelimiter.Name = "tlpDelimiter"
        Me.tlpDelimiter.RowCount = 1
        Me.tlpDelimiter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDelimiter.Size = New System.Drawing.Size(328, 29)
        Me.tlpDelimiter.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(79, 23)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Select Delimiter"
        '
        'cmbDelimiter
        '
        Me.cmbDelimiter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbDelimiter.EditValue = ";"
        Me.cmbDelimiter.Location = New System.Drawing.Point(88, 4)
        Me.cmbDelimiter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbDelimiter.Name = "cmbDelimiter"
        Me.cmbDelimiter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDelimiter.Properties.Items.AddRange(New Object() {";", ",", "TAB", "|"})
        Me.cmbDelimiter.Size = New System.Drawing.Size(237, 20)
        Me.cmbDelimiter.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'dlgCsvExportOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 128)
        Me.ControlBox = False
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(340, 160)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(340, 160)
        Me.Name = "dlgCsvExportOption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Export CSV Delimiter Options"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpBtns.ResumeLayout(False)
        Me.tlpDelimiter.ResumeLayout(False)
        Me.tlpDelimiter.PerformLayout()
        CType(Me.cmbDelimiter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpBtns As TableLayoutPanel
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpDelimiter As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbDelimiter As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Timer1 As Timer
End Class
