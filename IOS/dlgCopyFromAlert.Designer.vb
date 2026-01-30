<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgCopyFromAlert
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCopyFromAlert))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpTemplateAndMO = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbAlert = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbKPIRuleForAlert = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tlpCheckBoxes = New System.Windows.Forms.TableLayoutPanel()
        Me.ceCopyFilterStrings = New DevExpress.XtraEditors.CheckEdit()
        Me.tlpCommitBtn = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpTemplateAndMO.SuspendLayout()
        CType(Me.cmbAlert.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbKPIRuleForAlert.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpCheckBoxes.SuspendLayout()
        CType(Me.ceCopyFilterStrings.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpCommitBtn.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpTemplateAndMO, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpCheckBoxes, 0, 1)
        Me.tlpMain.Controls.Add(Me.tlpCommitBtn, 0, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(398, 148)
        Me.tlpMain.TabIndex = 1
        '
        'tlpTemplateAndMO
        '
        Me.tlpTemplateAndMO.ColumnCount = 2
        Me.tlpTemplateAndMO.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.tlpTemplateAndMO.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTemplateAndMO.Controls.Add(Me.LabelControl1, 0, 1)
        Me.tlpTemplateAndMO.Controls.Add(Me.LabelControl3, 0, 0)
        Me.tlpTemplateAndMO.Controls.Add(Me.cmbAlert, 1, 0)
        Me.tlpTemplateAndMO.Controls.Add(Me.cmbKPIRuleForAlert, 1, 1)
        Me.tlpTemplateAndMO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTemplateAndMO.Location = New System.Drawing.Point(0, 0)
        Me.tlpTemplateAndMO.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpTemplateAndMO.Name = "tlpTemplateAndMO"
        Me.tlpTemplateAndMO.RowCount = 3
        Me.tlpTemplateAndMO.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpTemplateAndMO.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpTemplateAndMO.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTemplateAndMO.Size = New System.Drawing.Size(398, 66)
        Me.tlpTemplateAndMO.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(109, 22)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Copy From KPI Rule"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(109, 22)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Select Alert"
        '
        'cmbAlert
        '
        Me.cmbAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbAlert.EditValue = ""
        Me.cmbAlert.Location = New System.Drawing.Point(118, 5)
        Me.cmbAlert.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbAlert.Name = "cmbAlert"
        Me.cmbAlert.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAlert.Size = New System.Drawing.Size(277, 20)
        Me.cmbAlert.TabIndex = 11
        '
        'cmbKPIRuleForAlert
        '
        Me.cmbKPIRuleForAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbKPIRuleForAlert.EditValue = ""
        Me.cmbKPIRuleForAlert.Location = New System.Drawing.Point(118, 33)
        Me.cmbKPIRuleForAlert.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbKPIRuleForAlert.Name = "cmbKPIRuleForAlert"
        Me.cmbKPIRuleForAlert.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbKPIRuleForAlert.Size = New System.Drawing.Size(277, 20)
        Me.cmbKPIRuleForAlert.TabIndex = 12
        '
        'tlpCheckBoxes
        '
        Me.tlpCheckBoxes.ColumnCount = 2
        Me.tlpCheckBoxes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.tlpCheckBoxes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCheckBoxes.Controls.Add(Me.ceCopyFilterStrings, 1, 0)
        Me.tlpCheckBoxes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCheckBoxes.Location = New System.Drawing.Point(0, 66)
        Me.tlpCheckBoxes.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpCheckBoxes.Name = "tlpCheckBoxes"
        Me.tlpCheckBoxes.RowCount = 2
        Me.tlpCheckBoxes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpCheckBoxes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCheckBoxes.Size = New System.Drawing.Size(398, 45)
        Me.tlpCheckBoxes.TabIndex = 1
        '
        'ceCopyFilterStrings
        '
        Me.ceCopyFilterStrings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceCopyFilterStrings.EditValue = True
        Me.ceCopyFilterStrings.Location = New System.Drawing.Point(120, 3)
        Me.ceCopyFilterStrings.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceCopyFilterStrings.Name = "ceCopyFilterStrings"
        Me.ceCopyFilterStrings.Properties.Caption = "Copy Filter Strings"
        Me.ceCopyFilterStrings.Size = New System.Drawing.Size(275, 29)
        Me.ceCopyFilterStrings.TabIndex = 13
        '
        'tlpCommitBtn
        '
        Me.tlpCommitBtn.ColumnCount = 2
        Me.tlpCommitBtn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCommitBtn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpCommitBtn.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpCommitBtn.Controls.Add(Me.btnCommit, 1, 0)
        Me.tlpCommitBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCommitBtn.Location = New System.Drawing.Point(0, 111)
        Me.tlpCommitBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpCommitBtn.Name = "tlpCommitBtn"
        Me.tlpCommitBtn.RowCount = 1
        Me.tlpCommitBtn.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCommitBtn.Size = New System.Drawing.Size(398, 37)
        Me.tlpCommitBtn.TabIndex = 2
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
        Me.lblMessage.Size = New System.Drawing.Size(292, 31)
        Me.lblMessage.TabIndex = 18
        '
        'btnCommit
        '
        Me.btnCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCommit.Location = New System.Drawing.Point(301, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(94, 31)
        Me.btnCommit.TabIndex = 0
        Me.btnCommit.Text = "Commit"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgCopyFromAlert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 148)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Image = CType(resources.GetObject("dlgCopyFromAlert.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(408, 180)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(408, 180)
        Me.Name = "dlgCopyFromAlert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy From Alert"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpTemplateAndMO.ResumeLayout(False)
        Me.tlpTemplateAndMO.PerformLayout()
        CType(Me.cmbAlert.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbKPIRuleForAlert.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpCheckBoxes.ResumeLayout(False)
        CType(Me.ceCopyFilterStrings.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpCommitBtn.ResumeLayout(False)
        Me.tlpCommitBtn.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpTemplateAndMO As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbAlert As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbKPIRuleForAlert As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpCheckBoxes As TableLayoutPanel
    Friend WithEvents ceCopyFilterStrings As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tlpCommitBtn As TableLayoutPanel
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
End Class
