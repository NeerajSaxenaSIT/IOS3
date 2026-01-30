<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTechExportOption
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
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.tlpBtns = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpDelimiter = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDelimiter = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tlpFolderSelection = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSelectPath = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpRadioBtns = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.rbTargetType = New System.Windows.Forms.RadioButton()
        Me.rbCellBased = New System.Windows.Forms.RadioButton()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.rbPeriodSelectionAggr = New System.Windows.Forms.RadioButton()
        Me.rbWholePeriodAggr = New System.Windows.Forms.RadioButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpBtns.SuspendLayout()
        Me.tlpDelimiter.SuspendLayout()
        CType(Me.cmbDelimiter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpFolderSelection.SuspendLayout()
        CType(Me.txtSelectPath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpRadioBtns.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 8)
        Me.tlpMain.Controls.Add(Me.tlpBtns, 0, 7)
        Me.tlpMain.Controls.Add(Me.tlpDelimiter, 0, 3)
        Me.tlpMain.Controls.Add(Me.tlpFolderSelection, 0, 5)
        Me.tlpMain.Controls.Add(Me.tlpRadioBtns, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 9
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(334, 261)
        Me.tlpMain.TabIndex = 1
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 208)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(328, 50)
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
        Me.tlpBtns.Location = New System.Drawing.Point(1, 171)
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
        Me.tlpDelimiter.Location = New System.Drawing.Point(3, 83)
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
        'tlpFolderSelection
        '
        Me.tlpFolderSelection.ColumnCount = 2
        Me.tlpFolderSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.tlpFolderSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpFolderSelection.Controls.Add(Me.txtSelectPath, 0, 0)
        Me.tlpFolderSelection.Controls.Add(Me.LabelControl2, 0, 0)
        Me.tlpFolderSelection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpFolderSelection.Location = New System.Drawing.Point(3, 128)
        Me.tlpFolderSelection.Name = "tlpFolderSelection"
        Me.tlpFolderSelection.RowCount = 1
        Me.tlpFolderSelection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpFolderSelection.Size = New System.Drawing.Size(328, 29)
        Me.tlpFolderSelection.TabIndex = 6
        '
        'txtSelectPath
        '
        Me.txtSelectPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSelectPath.Location = New System.Drawing.Point(88, 4)
        Me.txtSelectPath.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txtSelectPath.Name = "txtSelectPath"
        Me.txtSelectPath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSelectPath.Properties.NullValuePrompt = "Search..."
        Me.txtSelectPath.Size = New System.Drawing.Size(237, 20)
        Me.txtSelectPath.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(79, 23)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Select Folder"
        '
        'tlpRadioBtns
        '
        Me.tlpRadioBtns.ColumnCount = 1
        Me.tlpRadioBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRadioBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpRadioBtns.Controls.Add(Me.pnlTop, 0, 0)
        Me.tlpRadioBtns.Controls.Add(Me.pnlBottom, 0, 1)
        Me.tlpRadioBtns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpRadioBtns.Location = New System.Drawing.Point(0, 10)
        Me.tlpRadioBtns.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpRadioBtns.Name = "tlpRadioBtns"
        Me.tlpRadioBtns.RowCount = 2
        Me.tlpRadioBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.45454!))
        Me.tlpRadioBtns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.54546!))
        Me.tlpRadioBtns.Size = New System.Drawing.Size(334, 60)
        Me.tlpRadioBtns.TabIndex = 22
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.rbTargetType)
        Me.pnlTop.Controls.Add(Me.rbCellBased)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(334, 27)
        Me.pnlTop.TabIndex = 0
        '
        'rbTargetType
        '
        Me.rbTargetType.AutoSize = True
        Me.rbTargetType.Checked = True
        Me.rbTargetType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTargetType.Location = New System.Drawing.Point(6, 5)
        Me.rbTargetType.Name = "rbTargetType"
        Me.rbTargetType.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rbTargetType.Size = New System.Drawing.Size(124, 17)
        Me.rbTargetType.TabIndex = 0
        Me.rbTargetType.TabStop = True
        Me.rbTargetType.Text = "Target Type Export"
        Me.rbTargetType.UseVisualStyleBackColor = True
        '
        'rbCellBased
        '
        Me.rbCellBased.AutoSize = True
        Me.rbCellBased.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCellBased.Location = New System.Drawing.Point(174, 5)
        Me.rbCellBased.Name = "rbCellBased"
        Me.rbCellBased.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rbCellBased.Size = New System.Drawing.Size(114, 17)
        Me.rbCellBased.TabIndex = 1
        Me.rbCellBased.Text = "Cell Based Export"
        Me.rbCellBased.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.rbPeriodSelectionAggr)
        Me.pnlBottom.Controls.Add(Me.rbWholePeriodAggr)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBottom.Location = New System.Drawing.Point(0, 27)
        Me.pnlBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(334, 33)
        Me.pnlBottom.TabIndex = 1
        '
        'rbPeriodSelectionAggr
        '
        Me.rbPeriodSelectionAggr.AutoSize = True
        Me.rbPeriodSelectionAggr.Checked = True
        Me.rbPeriodSelectionAggr.Location = New System.Drawing.Point(6, 8)
        Me.rbPeriodSelectionAggr.Name = "rbPeriodSelectionAggr"
        Me.rbPeriodSelectionAggr.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rbPeriodSelectionAggr.Size = New System.Drawing.Size(166, 17)
        Me.rbPeriodSelectionAggr.TabIndex = 4
        Me.rbPeriodSelectionAggr.TabStop = True
        Me.rbPeriodSelectionAggr.Text = "Period Selection Aggregated"
        Me.rbPeriodSelectionAggr.UseVisualStyleBackColor = True
        '
        'rbWholePeriodAggr
        '
        Me.rbWholePeriodAggr.AutoSize = True
        Me.rbWholePeriodAggr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbWholePeriodAggr.Location = New System.Drawing.Point(174, 8)
        Me.rbWholePeriodAggr.Name = "rbWholePeriodAggr"
        Me.rbWholePeriodAggr.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rbWholePeriodAggr.Size = New System.Drawing.Size(153, 17)
        Me.rbWholePeriodAggr.TabIndex = 3
        Me.rbWholePeriodAggr.Text = "Whole Period Aggregated"
        Me.rbWholePeriodAggr.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'dlgTechExportOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 261)
        Me.ControlBox = False
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(340, 293)
        Me.MinimumSize = New System.Drawing.Size(340, 293)
        Me.Name = "dlgTechExportOption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Export CSV Options"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpBtns.ResumeLayout(False)
        Me.tlpDelimiter.ResumeLayout(False)
        Me.tlpDelimiter.PerformLayout()
        CType(Me.cmbDelimiter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpFolderSelection.ResumeLayout(False)
        Me.tlpFolderSelection.PerformLayout()
        CType(Me.txtSelectPath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpRadioBtns.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents rbCellBased As RadioButton
    Friend WithEvents rbTargetType As RadioButton
    Friend WithEvents tlpBtns As TableLayoutPanel
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpDelimiter As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbDelimiter As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpFolderSelection As TableLayoutPanel
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSelectPath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents rbWholePeriodAggr As RadioButton
    Friend WithEvents rbPeriodSelectionAggr As RadioButton
    Friend WithEvents tlpRadioBtns As TableLayoutPanel
    Friend WithEvents pnlTop As Panel
    Friend WithEvents pnlBottom As Panel
End Class
