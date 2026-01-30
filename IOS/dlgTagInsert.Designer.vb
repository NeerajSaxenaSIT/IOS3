<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTagInsert
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTagInsert))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpContent = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtTagName = New DevExpress.XtraEditors.TextEdit()
        Me.txtTagDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTagInsert = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpAccess = New System.Windows.Forms.TableLayoutPanel()
        Me.rdoPrivate = New System.Windows.Forms.RadioButton()
        Me.rdoPublic = New System.Windows.Forms.RadioButton()
        Me.lblMsg = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpContent.SuspendLayout()
        CType(Me.txtTagName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTagDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.tlpAccess.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.BackColor = System.Drawing.Color.Transparent
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpContent, 0, 0)
        Me.tlpMain.Controls.Add(Me.lblMsg, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(463, 218)
        Me.tlpMain.TabIndex = 0
        '
        'tlpContent
        '
        Me.tlpContent.ColumnCount = 2
        Me.tlpContent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpContent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpContent.Controls.Add(Me.LabelControl2, 0, 1)
        Me.tlpContent.Controls.Add(Me.LabelControl3, 0, 2)
        Me.tlpContent.Controls.Add(Me.txtTagName, 1, 1)
        Me.tlpContent.Controls.Add(Me.txtTagDescription, 1, 2)
        Me.tlpContent.Controls.Add(Me.TableLayoutPanel3, 1, 3)
        Me.tlpContent.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpContent.Controls.Add(Me.tlpAccess, 1, 0)
        Me.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpContent.Location = New System.Drawing.Point(2, 2)
        Me.tlpContent.Margin = New System.Windows.Forms.Padding(2)
        Me.tlpContent.Name = "tlpContent"
        Me.tlpContent.RowCount = 4
        Me.tlpContent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpContent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tlpContent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpContent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpContent.Size = New System.Drawing.Size(459, 186)
        Me.tlpContent.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 30)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(94, 21)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Tag Name"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 70)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(94, 25)
        Me.LabelControl3.TabIndex = 3
        Me.LabelControl3.Text = "Tag Description"
        '
        'txtTagName
        '
        Me.txtTagName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTagName.Location = New System.Drawing.Point(103, 31)
        Me.txtTagName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txtTagName.Name = "txtTagName"
        Me.txtTagName.Properties.MaxLength = 50
        Me.txtTagName.Size = New System.Drawing.Size(353, 20)
        Me.txtTagName.TabIndex = 4
        '
        'txtTagDescription
        '
        Me.txtTagDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTagDescription.Location = New System.Drawing.Point(103, 57)
        Me.txtTagDescription.Name = "txtTagDescription"
        Me.txtTagDescription.Properties.MaxLength = 200
        Me.txtTagDescription.Size = New System.Drawing.Size(353, 94)
        Me.txtTagDescription.TabIndex = 5
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnTagInsert, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(100, 154)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(359, 32)
        Me.TableLayoutPanel3.TabIndex = 8
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCancel.Location = New System.Drawing.Point(183, 2)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 2, 2, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 28)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        '
        'btnTagInsert
        '
        Me.btnTagInsert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTagInsert.Location = New System.Drawing.Point(2, 2)
        Me.btnTagInsert.Margin = New System.Windows.Forms.Padding(2)
        Me.btnTagInsert.Name = "btnTagInsert"
        Me.btnTagInsert.Size = New System.Drawing.Size(175, 28)
        Me.btnTagInsert.TabIndex = 6
        Me.btnTagInsert.Text = "Insert Tag"
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(94, 21)
        Me.LabelControl1.TabIndex = 9
        Me.LabelControl1.Text = "Private/Public"
        '
        'tlpAccess
        '
        Me.tlpAccess.ColumnCount = 2
        Me.tlpAccess.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpAccess.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpAccess.Controls.Add(Me.rdoPrivate, 0, 0)
        Me.tlpAccess.Controls.Add(Me.rdoPublic, 1, 0)
        Me.tlpAccess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAccess.Location = New System.Drawing.Point(100, 0)
        Me.tlpAccess.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpAccess.Name = "tlpAccess"
        Me.tlpAccess.RowCount = 1
        Me.tlpAccess.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAccess.Size = New System.Drawing.Size(359, 27)
        Me.tlpAccess.TabIndex = 10
        '
        'rdoPrivate
        '
        Me.rdoPrivate.AutoSize = True
        Me.rdoPrivate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoPrivate.Location = New System.Drawing.Point(3, 3)
        Me.rdoPrivate.Name = "rdoPrivate"
        Me.rdoPrivate.Padding = New System.Windows.Forms.Padding(60, 0, 0, 0)
        Me.rdoPrivate.Size = New System.Drawing.Size(173, 21)
        Me.rdoPrivate.TabIndex = 0
        Me.rdoPrivate.Text = "Private"
        Me.rdoPrivate.UseVisualStyleBackColor = True
        '
        'rdoPublic
        '
        Me.rdoPublic.AutoSize = True
        Me.rdoPublic.Checked = True
        Me.rdoPublic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoPublic.Location = New System.Drawing.Point(182, 3)
        Me.rdoPublic.Name = "rdoPublic"
        Me.rdoPublic.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rdoPublic.Size = New System.Drawing.Size(174, 21)
        Me.rdoPublic.TabIndex = 1
        Me.rdoPublic.TabStop = True
        Me.rdoPublic.Text = "Public"
        Me.rdoPublic.UseVisualStyleBackColor = True
        '
        'lblMsg
        '
        Me.lblMsg.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsg.Appearance.Options.UseForeColor = True
        Me.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMsg.Location = New System.Drawing.Point(3, 193)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMsg.Size = New System.Drawing.Size(457, 22)
        Me.lblMsg.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgTagInsert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(463, 218)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("dlgTagInsert.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(465, 250)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(465, 250)
        Me.Name = "dlgTagInsert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tag Insert"
        Me.TopMost = True
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpContent.ResumeLayout(False)
        Me.tlpContent.PerformLayout()
        CType(Me.txtTagName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTagDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.tlpAccess.ResumeLayout(False)
        Me.tlpAccess.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlpContent As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTagName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTagDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnTagInsert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpAccess As TableLayoutPanel
    Friend WithEvents rdoPrivate As RadioButton
    Friend WithEvents rdoPublic As RadioButton
    Friend WithEvents Timer1 As Timer
End Class
