<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCopyFromTemplate
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCopyFromTemplate))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpTemplateAndMO = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTemplate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbMOForTemplate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tlpCheckBoxes = New System.Windows.Forms.TableLayoutPanel()
        Me.ceCopyParamExclusions = New DevExpress.XtraEditors.CheckEdit()
        Me.ceCopyFilterStrings = New DevExpress.XtraEditors.CheckEdit()
        Me.ceCopyInclusionList = New DevExpress.XtraEditors.CheckEdit()
        Me.ceCopyExclusionList = New DevExpress.XtraEditors.CheckEdit()
        Me.tlpCommitBtn = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpTemplateAndMO.SuspendLayout()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMOForTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpCheckBoxes.SuspendLayout()
        CType(Me.ceCopyParamExclusions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceCopyFilterStrings.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceCopyInclusionList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceCopyExclusionList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(398, 168)
        Me.tlpMain.TabIndex = 0
        '
        'tlpTemplateAndMO
        '
        Me.tlpTemplateAndMO.ColumnCount = 2
        Me.tlpTemplateAndMO.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.tlpTemplateAndMO.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTemplateAndMO.Controls.Add(Me.LabelControl1, 0, 1)
        Me.tlpTemplateAndMO.Controls.Add(Me.LabelControl3, 0, 0)
        Me.tlpTemplateAndMO.Controls.Add(Me.cmbTemplate, 1, 0)
        Me.tlpTemplateAndMO.Controls.Add(Me.cmbMOForTemplate, 1, 1)
        Me.tlpTemplateAndMO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTemplateAndMO.Location = New System.Drawing.Point(0, 0)
        Me.tlpTemplateAndMO.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpTemplateAndMO.Name = "tlpTemplateAndMO"
        Me.tlpTemplateAndMO.RowCount = 3
        Me.tlpTemplateAndMO.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpTemplateAndMO.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpTemplateAndMO.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTemplateAndMO.Size = New System.Drawing.Size(398, 65)
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
        Me.LabelControl1.Text = "Copy Filter From MO"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(109, 22)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Select Template"
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTemplate.EditValue = "Select Template"
        Me.cmbTemplate.Location = New System.Drawing.Point(118, 5)
        Me.cmbTemplate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTemplate.Size = New System.Drawing.Size(277, 20)
        Me.cmbTemplate.TabIndex = 11
        '
        'cmbMOForTemplate
        '
        Me.cmbMOForTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbMOForTemplate.EditValue = "Select MO"
        Me.cmbMOForTemplate.Location = New System.Drawing.Point(118, 33)
        Me.cmbMOForTemplate.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbMOForTemplate.Name = "cmbMOForTemplate"
        Me.cmbMOForTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMOForTemplate.Size = New System.Drawing.Size(277, 20)
        Me.cmbMOForTemplate.TabIndex = 12
        '
        'tlpCheckBoxes
        '
        Me.tlpCheckBoxes.ColumnCount = 2
        Me.tlpCheckBoxes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpCheckBoxes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpCheckBoxes.Controls.Add(Me.ceCopyParamExclusions, 0, 1)
        Me.tlpCheckBoxes.Controls.Add(Me.ceCopyFilterStrings, 0, 0)
        Me.tlpCheckBoxes.Controls.Add(Me.ceCopyInclusionList, 1, 0)
        Me.tlpCheckBoxes.Controls.Add(Me.ceCopyExclusionList, 1, 1)
        Me.tlpCheckBoxes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCheckBoxes.Location = New System.Drawing.Point(0, 65)
        Me.tlpCheckBoxes.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpCheckBoxes.Name = "tlpCheckBoxes"
        Me.tlpCheckBoxes.RowCount = 3
        Me.tlpCheckBoxes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpCheckBoxes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpCheckBoxes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCheckBoxes.Size = New System.Drawing.Size(398, 65)
        Me.tlpCheckBoxes.TabIndex = 1
        '
        'ceCopyParamExclusions
        '
        Me.ceCopyParamExclusions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceCopyParamExclusions.Location = New System.Drawing.Point(10, 31)
        Me.ceCopyParamExclusions.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.ceCopyParamExclusions.Name = "ceCopyParamExclusions"
        Me.ceCopyParamExclusions.Properties.Caption = "Copy Parameter Exclusions"
        Me.ceCopyParamExclusions.Size = New System.Drawing.Size(186, 22)
        Me.ceCopyParamExclusions.TabIndex = 19
        '
        'ceCopyFilterStrings
        '
        Me.ceCopyFilterStrings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceCopyFilterStrings.EditValue = True
        Me.ceCopyFilterStrings.Location = New System.Drawing.Point(10, 3)
        Me.ceCopyFilterStrings.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.ceCopyFilterStrings.Name = "ceCopyFilterStrings"
        Me.ceCopyFilterStrings.Properties.Caption = "Copy Filter Strings"
        Me.ceCopyFilterStrings.Size = New System.Drawing.Size(186, 22)
        Me.ceCopyFilterStrings.TabIndex = 13
        '
        'ceCopyInclusionList
        '
        Me.ceCopyInclusionList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceCopyInclusionList.Location = New System.Drawing.Point(209, 3)
        Me.ceCopyInclusionList.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.ceCopyInclusionList.Name = "ceCopyInclusionList"
        Me.ceCopyInclusionList.Properties.Caption = "Copy Inclusion List"
        Me.ceCopyInclusionList.Size = New System.Drawing.Size(186, 22)
        Me.ceCopyInclusionList.TabIndex = 18
        '
        'ceCopyExclusionList
        '
        Me.ceCopyExclusionList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceCopyExclusionList.Location = New System.Drawing.Point(209, 31)
        Me.ceCopyExclusionList.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.ceCopyExclusionList.Name = "ceCopyExclusionList"
        Me.ceCopyExclusionList.Properties.Caption = "Copy Exclusion List"
        Me.ceCopyExclusionList.Size = New System.Drawing.Size(186, 22)
        Me.ceCopyExclusionList.TabIndex = 14
        '
        'tlpCommitBtn
        '
        Me.tlpCommitBtn.ColumnCount = 2
        Me.tlpCommitBtn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCommitBtn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpCommitBtn.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpCommitBtn.Controls.Add(Me.btnCommit, 1, 0)
        Me.tlpCommitBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCommitBtn.Location = New System.Drawing.Point(0, 130)
        Me.tlpCommitBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpCommitBtn.Name = "tlpCommitBtn"
        Me.tlpCommitBtn.RowCount = 1
        Me.tlpCommitBtn.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCommitBtn.Size = New System.Drawing.Size(398, 38)
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
        Me.lblMessage.Size = New System.Drawing.Size(292, 32)
        Me.lblMessage.TabIndex = 18
        '
        'btnCommit
        '
        Me.btnCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCommit.Location = New System.Drawing.Point(301, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(94, 32)
        Me.btnCommit.TabIndex = 0
        Me.btnCommit.Text = "Commit"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'frmCopyFromTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 168)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("frmCopyFromTemplate.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(408, 200)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(408, 200)
        Me.Name = "frmCopyFromTemplate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy From Template"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpTemplateAndMO.ResumeLayout(False)
        Me.tlpTemplateAndMO.PerformLayout()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMOForTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpCheckBoxes.ResumeLayout(False)
        CType(Me.ceCopyParamExclusions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceCopyFilterStrings.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceCopyInclusionList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceCopyExclusionList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpCommitBtn.ResumeLayout(False)
        Me.tlpCommitBtn.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpTemplateAndMO As TableLayoutPanel
    Friend WithEvents tlpCheckBoxes As TableLayoutPanel
    Friend WithEvents tlpCommitBtn As TableLayoutPanel
    Friend WithEvents btnCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTemplate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbMOForTemplate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ceCopyParamExclusions As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceCopyExclusionList As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceCopyFilterStrings As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceCopyInclusionList As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
End Class
