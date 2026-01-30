<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgKPISetCreate
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblName = New DevExpress.XtraEditors.LabelControl()
        Me.txtKPISetName = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.lblCounterType = New DevExpress.XtraEditors.LabelControl()
        Me.cmbCounterType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblIOSTech = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.txtKPISetName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.cmbCounterType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblName, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtKPISetName, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblCounterType, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbCounterType, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblIOSTech, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(398, 168)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'lblName
        '
        Me.lblName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblName.Location = New System.Drawing.Point(3, 28)
        Me.lblName.Name = "lblName"
        Me.lblName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblName.Size = New System.Drawing.Size(114, 19)
        Me.lblName.TabIndex = 8
        Me.lblName.Text = "Enter KPI Set Name"
        '
        'txtKPISetName
        '
        Me.txtKPISetName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtKPISetName.Location = New System.Drawing.Point(123, 28)
        Me.txtKPISetName.Name = "txtKPISetName"
        Me.txtKPISetName.Properties.MaxLength = 1000
        Me.txtKPISetName.Size = New System.Drawing.Size(272, 20)
        Me.txtKPISetName.TabIndex = 9
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOk, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(121, 76)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(276, 33)
        Me.TableLayoutPanel2.TabIndex = 11
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(141, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(132, 27)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        '
        'btnOk
        '
        Me.btnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOk.Location = New System.Drawing.Point(3, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(132, 27)
        Me.btnOk.TabIndex = 10
        Me.btnOk.Text = "OK"
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblMessage, 2)
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 113)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(392, 52)
        Me.lblMessage.TabIndex = 12
        '
        'lblCounterType
        '
        Me.lblCounterType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCounterType.Location = New System.Drawing.Point(3, 53)
        Me.lblCounterType.Name = "lblCounterType"
        Me.lblCounterType.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblCounterType.Size = New System.Drawing.Size(114, 19)
        Me.lblCounterType.TabIndex = 13
        Me.lblCounterType.Text = "Select Counter Type"
        '
        'cmbCounterType
        '
        Me.cmbCounterType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbCounterType.Location = New System.Drawing.Point(123, 53)
        Me.cmbCounterType.Name = "cmbCounterType"
        Me.cmbCounterType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCounterType.Size = New System.Drawing.Size(272, 20)
        Me.cmbCounterType.TabIndex = 14
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(114, 19)
        Me.LabelControl1.TabIndex = 15
        Me.LabelControl1.Text = "Technology"
        '
        'lblIOSTech
        '
        Me.lblIOSTech.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblIOSTech.Appearance.Options.UseForeColor = True
        Me.lblIOSTech.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIOSTech.Location = New System.Drawing.Point(123, 3)
        Me.lblIOSTech.Name = "lblIOSTech"
        Me.lblIOSTech.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblIOSTech.Size = New System.Drawing.Size(272, 19)
        Me.lblIOSTech.TabIndex = 16
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgKPISetCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 168)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(404, 200)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(404, 200)
        Me.Name = "dlgKPISetCreate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add New KPI Set"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.txtKPISetName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.cmbCounterType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtKPISetName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCounterType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbCounterType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblIOSTech As DevExpress.XtraEditors.LabelControl
End Class
