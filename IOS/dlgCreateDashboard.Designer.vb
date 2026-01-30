<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgCreateDashboard
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
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.tlpBtns = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpDelimiter = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtReportName = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.pnlAccess = New DevExpress.XtraEditors.PanelControl()
        Me.rbPublic = New System.Windows.Forms.RadioButton()
        Me.rbPrivate = New System.Windows.Forms.RadioButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpBtns.SuspendLayout()
        Me.tlpDelimiter.SuspendLayout()
        CType(Me.txtReportName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.pnlAccess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAccess.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 6)
        Me.tlpMain.Controls.Add(Me.tlpBtns, 0, 5)
        Me.tlpMain.Controls.Add(Me.tlpDelimiter, 0, 3)
        Me.tlpMain.Controls.Add(Me.GroupControl1, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 7
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(334, 178)
        Me.tlpMain.TabIndex = 3
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 148)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(328, 27)
        Me.lblMessage.TabIndex = 21
        '
        'tlpBtns
        '
        Me.tlpBtns.ColumnCount = 4
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBtns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpBtns.Controls.Add(Me.btnOk, 1, 0)
        Me.tlpBtns.Controls.Add(Me.btnCancel, 2, 0)
        Me.tlpBtns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBtns.Location = New System.Drawing.Point(1, 111)
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
        Me.btnOk.Location = New System.Drawing.Point(33, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(130, 27)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "Ok"
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(169, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(130, 27)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'tlpDelimiter
        '
        Me.tlpDelimiter.ColumnCount = 2
        Me.tlpDelimiter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpDelimiter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDelimiter.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpDelimiter.Controls.Add(Me.txtReportName, 1, 0)
        Me.tlpDelimiter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpDelimiter.Location = New System.Drawing.Point(3, 73)
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
        Me.LabelControl1.Size = New System.Drawing.Size(74, 23)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Report Name"
        '
        'txtReportName
        '
        Me.txtReportName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReportName.Location = New System.Drawing.Point(83, 4)
        Me.txtReportName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.Properties.MaxLength = 100
        Me.txtReportName.Size = New System.Drawing.Size(242, 20)
        Me.txtReportName.TabIndex = 1
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.pnlAccess)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 8)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(328, 54)
        Me.GroupControl1.TabIndex = 22
        Me.GroupControl1.Text = "Accessibility"
        '
        'pnlAccess
        '
        Me.pnlAccess.Controls.Add(Me.rbPublic)
        Me.pnlAccess.Controls.Add(Me.rbPrivate)
        Me.pnlAccess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAccess.Location = New System.Drawing.Point(2, 22)
        Me.pnlAccess.Name = "pnlAccess"
        Me.pnlAccess.Size = New System.Drawing.Size(324, 30)
        Me.pnlAccess.TabIndex = 22
        '
        'rbPublic
        '
        Me.rbPublic.AutoSize = True
        Me.rbPublic.Checked = True
        Me.rbPublic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPublic.Location = New System.Drawing.Point(78, 6)
        Me.rbPublic.Name = "rbPublic"
        Me.rbPublic.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rbPublic.Size = New System.Drawing.Size(57, 17)
        Me.rbPublic.TabIndex = 2
        Me.rbPublic.TabStop = True
        Me.rbPublic.Text = "Public"
        Me.rbPublic.UseVisualStyleBackColor = True
        '
        'rbPrivate
        '
        Me.rbPrivate.AutoSize = True
        Me.rbPrivate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPrivate.Location = New System.Drawing.Point(167, 6)
        Me.rbPrivate.Name = "rbPrivate"
        Me.rbPrivate.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rbPrivate.Size = New System.Drawing.Size(64, 17)
        Me.rbPrivate.TabIndex = 3
        Me.rbPrivate.Text = "Private"
        Me.rbPrivate.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'dlgCreateDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 178)
        Me.ControlBox = False
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(340, 210)
        Me.MinimumSize = New System.Drawing.Size(340, 210)
        Me.Name = "dlgCreateDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard - Create Report"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpBtns.ResumeLayout(False)
        Me.tlpDelimiter.ResumeLayout(False)
        Me.tlpDelimiter.PerformLayout()
        CType(Me.txtReportName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.pnlAccess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAccess.ResumeLayout(False)
        Me.pnlAccess.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpBtns As TableLayoutPanel
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlpDelimiter As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtReportName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents pnlAccess As DevExpress.XtraEditors.PanelControl
    Friend WithEvents rbPublic As RadioButton
    Friend WithEvents rbPrivate As RadioButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
End Class
