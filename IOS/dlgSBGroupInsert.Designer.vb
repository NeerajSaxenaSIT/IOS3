<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSBGroupInsert
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
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblGroupName = New DevExpress.XtraEditors.LabelControl()
        Me.txtGroupName = New DevExpress.XtraEditors.TextEdit()
        Me.gcPublicPrivate = New DevExpress.XtraEditors.GroupControl()
        Me.rbPublic = New System.Windows.Forms.RadioButton()
        Me.rbPrivate = New System.Windows.Forms.RadioButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtGroupName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcPublicPrivate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcPublicPrivate.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.gcPublicPrivate, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(384, 160)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblGroupName, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtGroupName, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(380, 26)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'lblGroupName
        '
        Me.lblGroupName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGroupName.Location = New System.Drawing.Point(3, 3)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblGroupName.Size = New System.Drawing.Size(127, 20)
        Me.lblGroupName.TabIndex = 0
        Me.lblGroupName.Text = "Report Group Name"
        '
        'txtGroupName
        '
        Me.txtGroupName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGroupName.Location = New System.Drawing.Point(136, 3)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(241, 20)
        Me.txtGroupName.TabIndex = 1
        '
        'gcPublicPrivate
        '
        Me.gcPublicPrivate.Controls.Add(Me.rbPublic)
        Me.gcPublicPrivate.Controls.Add(Me.rbPrivate)
        Me.gcPublicPrivate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcPublicPrivate.Location = New System.Drawing.Point(3, 33)
        Me.gcPublicPrivate.Name = "gcPublicPrivate"
        Me.gcPublicPrivate.Size = New System.Drawing.Size(378, 54)
        Me.gcPublicPrivate.TabIndex = 1
        Me.gcPublicPrivate.Text = "Private / Public"
        '
        'rbPublic
        '
        Me.rbPublic.AutoSize = True
        Me.rbPublic.Location = New System.Drawing.Point(218, 25)
        Me.rbPublic.Name = "rbPublic"
        Me.rbPublic.Size = New System.Drawing.Size(52, 17)
        Me.rbPublic.TabIndex = 1
        Me.rbPublic.TabStop = True
        Me.rbPublic.Text = "Public"
        Me.rbPublic.UseVisualStyleBackColor = True
        '
        'rbPrivate
        '
        Me.rbPrivate.AutoSize = True
        Me.rbPrivate.Location = New System.Drawing.Point(97, 25)
        Me.rbPrivate.Name = "rbPrivate"
        Me.rbPrivate.Size = New System.Drawing.Size(59, 17)
        Me.rbPrivate.TabIndex = 0
        Me.rbPrivate.TabStop = True
        Me.rbPrivate.Text = "Private"
        Me.rbPrivate.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 125)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(378, 32)
        Me.lblMessage.TabIndex = 2
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnAddGroup, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1, 91)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(382, 30)
        Me.TableLayoutPanel3.TabIndex = 3
        '
        'btnAddGroup
        '
        Me.btnAddGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddGroup.Location = New System.Drawing.Point(3, 3)
        Me.btnAddGroup.Name = "btnAddGroup"
        Me.btnAddGroup.Size = New System.Drawing.Size(185, 24)
        Me.btnAddGroup.TabIndex = 0
        Me.btnAddGroup.Text = "Add Group"
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(194, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(185, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgSBGroupInsert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 160)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(400, 250)
        Me.MinimumSize = New System.Drawing.Size(400, 39)
        Me.Name = "dlgSBGroupInsert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add New Group"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.txtGroupName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcPublicPrivate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcPublicPrivate.ResumeLayout(False)
        Me.gcPublicPrivate.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblGroupName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGroupName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents gcPublicPrivate As DevExpress.XtraEditors.GroupControl
    Friend WithEvents rbPublic As System.Windows.Forms.RadioButton
    Friend WithEvents rbPrivate As System.Windows.Forms.RadioButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnAddGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
