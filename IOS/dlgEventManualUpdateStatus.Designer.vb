<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEventManualUpdateStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEventManualUpdateStatus))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.txtEventDesc = New DevExpress.XtraEditors.MemoEdit()
        Me.cmbEventStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblEventID = New DevExpress.XtraEditors.LabelControl()
        Me.lblEventConfigID = New DevExpress.XtraEditors.LabelControl()
        Me.lblEventName = New DevExpress.XtraEditors.LabelControl()
        Me.lblEventStatus = New DevExpress.XtraEditors.LabelControl()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnSubmit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.txtEventDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEventStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.tlpMain.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpMain.Controls.Add(Me.LabelControl2, 0, 1)
        Me.tlpMain.Controls.Add(Me.LabelControl3, 0, 2)
        Me.tlpMain.Controls.Add(Me.LabelControl4, 0, 3)
        Me.tlpMain.Controls.Add(Me.LabelControl5, 0, 4)
        Me.tlpMain.Controls.Add(Me.LabelControl6, 0, 5)
        Me.tlpMain.Controls.Add(Me.txtEventDesc, 1, 5)
        Me.tlpMain.Controls.Add(Me.cmbEventStatus, 1, 4)
        Me.tlpMain.Controls.Add(Me.lblEventID, 1, 0)
        Me.tlpMain.Controls.Add(Me.lblEventConfigID, 1, 1)
        Me.tlpMain.Controls.Add(Me.lblEventName, 1, 2)
        Me.tlpMain.Controls.Add(Me.lblEventStatus, 1, 3)
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 6)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 7
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.Size = New System.Drawing.Size(549, 414)
        Me.tlpMain.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(131, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Event ID"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(131, 22)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Event Configuration ID"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(131, 22)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Event Name"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 87)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(131, 22)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Currest Status"
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 115)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(131, 22)
        Me.LabelControl5.TabIndex = 4
        Me.LabelControl5.Text = "Select Event Status"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 150)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(131, 226)
        Me.LabelControl6.TabIndex = 5
        Me.LabelControl6.Text = "Description"
        '
        'txtEventDesc
        '
        Me.txtEventDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEventDesc.Location = New System.Drawing.Point(140, 143)
        Me.txtEventDesc.Name = "txtEventDesc"
        Me.txtEventDesc.Properties.MaxLength = 4000
        Me.txtEventDesc.Size = New System.Drawing.Size(406, 233)
        Me.txtEventDesc.TabIndex = 6
        '
        'cmbEventStatus
        '
        Me.cmbEventStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbEventStatus.Location = New System.Drawing.Point(140, 116)
        Me.cmbEventStatus.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbEventStatus.Name = "cmbEventStatus"
        Me.cmbEventStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEventStatus.Size = New System.Drawing.Size(406, 20)
        Me.cmbEventStatus.TabIndex = 8
        '
        'lblEventID
        '
        Me.lblEventID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblEventID.Location = New System.Drawing.Point(140, 3)
        Me.lblEventID.Name = "lblEventID"
        Me.lblEventID.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblEventID.Size = New System.Drawing.Size(406, 22)
        Me.lblEventID.TabIndex = 9
        '
        'lblEventConfigID
        '
        Me.lblEventConfigID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblEventConfigID.Location = New System.Drawing.Point(140, 31)
        Me.lblEventConfigID.Name = "lblEventConfigID"
        Me.lblEventConfigID.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblEventConfigID.Size = New System.Drawing.Size(406, 22)
        Me.lblEventConfigID.TabIndex = 10
        '
        'lblEventName
        '
        Me.lblEventName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblEventName.Location = New System.Drawing.Point(140, 59)
        Me.lblEventName.Name = "lblEventName"
        Me.lblEventName.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblEventName.Size = New System.Drawing.Size(406, 22)
        Me.lblEventName.TabIndex = 11
        '
        'lblEventStatus
        '
        Me.lblEventStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblEventStatus.Location = New System.Drawing.Point(140, 87)
        Me.lblEventStatus.Name = "lblEventStatus"
        Me.lblEventStatus.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblEventStatus.Size = New System.Drawing.Size(406, 22)
        Me.lblEventStatus.TabIndex = 12
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 3
        Me.tlpMain.SetColumnSpan(Me.tlpBottom, 2)
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnSubmit, 1, 0)
        Me.tlpBottom.Controls.Add(Me.btnClose, 2, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(0, 379)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(549, 35)
        Me.tlpBottom.TabIndex = 13
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(383, 29)
        Me.lblMessage.TabIndex = 22
        '
        'btnSubmit
        '
        Me.btnSubmit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSubmit.Location = New System.Drawing.Point(392, 3)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(74, 29)
        Me.btnSubmit.TabIndex = 7
        Me.btnSubmit.Text = "Submit"
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClose.Location = New System.Drawing.Point(472, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 29)
        Me.btnClose.TabIndex = 23
        Me.btnClose.Text = "Close"
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'dlgEventManualUpdateStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 414)
        Me.ControlBox = False
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("dlgEventManualUpdateStatus.IconOptions.Image"), System.Drawing.Image)
        Me.Name = "dlgEventManualUpdateStatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Event - Manual Update Status"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.txtEventDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEventStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtEventDesc As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnSubmit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbEventStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblEventID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEventConfigID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEventName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEventStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
End Class
