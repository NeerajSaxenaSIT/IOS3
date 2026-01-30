<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgCongestionRule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCongestionRule))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCongestionRule = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.vbtn_Cancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbIOSTech = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbCounter = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.txtCongestionRule.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.cmbIOSTech.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCounter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl5, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCongestionRule, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbIOSTech, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbCounter, 1, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(384, 191)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 23)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(114, 22)
        Me.LabelControl5.TabIndex = 8
        Me.LabelControl5.Text = "Congestion Rule Name"
        '
        'txtCongestionRule
        '
        Me.txtCongestionRule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCongestionRule.Location = New System.Drawing.Point(123, 25)
        Me.txtCongestionRule.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.txtCongestionRule.Name = "txtCongestionRule"
        Me.txtCongestionRule.Size = New System.Drawing.Size(258, 20)
        Me.txtCongestionRule.TabIndex = 9
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.vbtn_Cancel, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOk, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(121, 125)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(262, 30)
        Me.TableLayoutPanel2.TabIndex = 11
        '
        'vbtn_Cancel
        '
        Me.vbtn_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vbtn_Cancel.Location = New System.Drawing.Point(134, 3)
        Me.vbtn_Cancel.Name = "vbtn_Cancel"
        Me.vbtn_Cancel.Size = New System.Drawing.Size(125, 24)
        Me.vbtn_Cancel.TabIndex = 11
        Me.vbtn_Cancel.Text = "Cancel"
        '
        'btnOk
        '
        Me.btnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOk.Location = New System.Drawing.Point(3, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(125, 24)
        Me.btnOk.TabIndex = 10
        Me.btnOk.Text = "OK"
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblMessage, 2)
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 159)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(378, 29)
        Me.lblMessage.TabIndex = 12
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 51)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(114, 22)
        Me.LabelControl1.TabIndex = 13
        Me.LabelControl1.Text = "Technology"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 79)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(114, 22)
        Me.LabelControl2.TabIndex = 14
        Me.LabelControl2.Text = "Counter Type"
        '
        'cmbIOSTech
        '
        Me.cmbIOSTech.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbIOSTech.Location = New System.Drawing.Point(123, 53)
        Me.cmbIOSTech.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbIOSTech.Name = "cmbIOSTech"
        Me.cmbIOSTech.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIOSTech.Size = New System.Drawing.Size(258, 20)
        Me.cmbIOSTech.TabIndex = 15
        '
        'cmbCounter
        '
        Me.cmbCounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbCounter.Location = New System.Drawing.Point(123, 81)
        Me.cmbCounter.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbCounter.Name = "cmbCounter"
        Me.cmbCounter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCounter.Size = New System.Drawing.Size(258, 20)
        Me.cmbCounter.TabIndex = 16
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgCongestionRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 191)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(400, 230)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 230)
        Me.Name = "dlgCongestionRule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Congestion Rule"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.txtCongestionRule.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.cmbIOSTech.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCounter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCongestionRule As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents vbtn_Cancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbIOSTech As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbCounter As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Timer1 As Timer
End Class
