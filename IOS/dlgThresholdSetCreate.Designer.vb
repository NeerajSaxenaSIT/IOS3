<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgThresholdSetCreate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgThresholdSetCreate))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbTargetType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblName = New DevExpress.XtraEditors.LabelControl()
        Me.txtThresholdSetName = New DevExpress.XtraEditors.TextEdit()
        Me.tlpBtns = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.lblCounterType = New DevExpress.XtraEditors.LabelControl()
        Me.cmbMethod = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblIOSTech = New DevExpress.XtraEditors.LabelControl()
        Me.lblTargetType = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.cmbTargetType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtThresholdSetName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpBtns.SuspendLayout()
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.cmbTargetType, 1, 3)
        Me.tlpMain.Controls.Add(Me.lblName, 0, 1)
        Me.tlpMain.Controls.Add(Me.txtThresholdSetName, 1, 1)
        Me.tlpMain.Controls.Add(Me.tlpBtns, 1, 4)
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 5)
        Me.tlpMain.Controls.Add(Me.lblCounterType, 0, 2)
        Me.tlpMain.Controls.Add(Me.cmbMethod, 1, 2)
        Me.tlpMain.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpMain.Controls.Add(Me.lblIOSTech, 1, 0)
        Me.tlpMain.Controls.Add(Me.lblTargetType, 0, 3)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 6
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(398, 168)
        Me.tlpMain.TabIndex = 3
        '
        'cmbTargetType
        '
        Me.cmbTargetType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTargetType.Location = New System.Drawing.Point(143, 78)
        Me.cmbTargetType.Name = "cmbTargetType"
        Me.cmbTargetType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTargetType.Size = New System.Drawing.Size(252, 20)
        Me.cmbTargetType.TabIndex = 18
        Me.cmbTargetType.Visible = False
        '
        'lblName
        '
        Me.lblName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblName.Location = New System.Drawing.Point(3, 28)
        Me.lblName.Name = "lblName"
        Me.lblName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblName.Size = New System.Drawing.Size(134, 19)
        Me.lblName.TabIndex = 8
        Me.lblName.Text = "Enter Threshold Set Name"
        '
        'txtThresholdSetName
        '
        Me.txtThresholdSetName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtThresholdSetName.Location = New System.Drawing.Point(143, 28)
        Me.txtThresholdSetName.Name = "txtThresholdSetName"
        Me.txtThresholdSetName.Properties.MaxLength = 1000
        Me.txtThresholdSetName.Size = New System.Drawing.Size(252, 20)
        Me.txtThresholdSetName.TabIndex = 9
        '
        'tlpBtns
        '
        Me.tlpBtns.ColumnCount = 2
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBtns.Controls.Add(Me.btnCancel, 1, 0)
        Me.tlpBtns.Controls.Add(Me.btnOk, 0, 0)
        Me.tlpBtns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBtns.Location = New System.Drawing.Point(141, 101)
        Me.tlpBtns.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpBtns.Name = "tlpBtns"
        Me.tlpBtns.RowCount = 1
        Me.tlpBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tlpBtns.Size = New System.Drawing.Size(256, 33)
        Me.tlpBtns.TabIndex = 11
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(131, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(122, 27)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        '
        'btnOk
        '
        Me.btnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOk.Location = New System.Drawing.Point(3, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(122, 27)
        Me.btnOk.TabIndex = 10
        Me.btnOk.Text = "OK"
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.tlpMain.SetColumnSpan(Me.lblMessage, 2)
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 138)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(392, 27)
        Me.lblMessage.TabIndex = 12
        '
        'lblCounterType
        '
        Me.lblCounterType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCounterType.Location = New System.Drawing.Point(3, 53)
        Me.lblCounterType.Name = "lblCounterType"
        Me.lblCounterType.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblCounterType.Size = New System.Drawing.Size(134, 19)
        Me.lblCounterType.TabIndex = 13
        Me.lblCounterType.Text = "Select Method"
        '
        'cmbMethod
        '
        Me.cmbMethod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbMethod.Location = New System.Drawing.Point(143, 53)
        Me.cmbMethod.Name = "cmbMethod"
        Me.cmbMethod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMethod.Size = New System.Drawing.Size(252, 20)
        Me.cmbMethod.TabIndex = 14
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(134, 19)
        Me.LabelControl1.TabIndex = 15
        Me.LabelControl1.Text = "Technology"
        '
        'lblIOSTech
        '
        Me.lblIOSTech.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblIOSTech.Appearance.Options.UseForeColor = True
        Me.lblIOSTech.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIOSTech.Location = New System.Drawing.Point(143, 3)
        Me.lblIOSTech.Name = "lblIOSTech"
        Me.lblIOSTech.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblIOSTech.Size = New System.Drawing.Size(252, 19)
        Me.lblIOSTech.TabIndex = 16
        '
        'lblTargetType
        '
        Me.lblTargetType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTargetType.Location = New System.Drawing.Point(3, 78)
        Me.lblTargetType.Name = "lblTargetType"
        Me.lblTargetType.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblTargetType.Size = New System.Drawing.Size(134, 19)
        Me.lblTargetType.TabIndex = 17
        Me.lblTargetType.Text = "Select Target Type"
        Me.lblTargetType.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgThresholdSetCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 168)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Image = CType(resources.GetObject("dlgThresholdSetCreate.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(400, 200)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "dlgThresholdSetCreate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add New Threshold Set"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.cmbTargetType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtThresholdSetName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpBtns.ResumeLayout(False)
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents lblName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtThresholdSetName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tlpBtns As TableLayoutPanel
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCounterType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbMethod As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblIOSTech As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTargetType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblTargetType As DevExpress.XtraEditors.LabelControl
End Class
