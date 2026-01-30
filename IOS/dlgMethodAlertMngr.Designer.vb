<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgMethodAlertMngr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgMethodAlertMngr))
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnUpdateMethod = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbMethod = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.tlpBottom.SuspendLayout()
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(314, 88)
        Me.GroupControl6.TabIndex = 3
        Me.GroupControl6.Text = "Select Method"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.tlpBottom, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbMethod, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 22)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(310, 64)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnUpdateMethod, 1, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(0, 28)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(310, 30)
        Me.tlpBottom.TabIndex = 4
        '
        'btnUpdateMethod
        '
        Me.btnUpdateMethod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUpdateMethod.Location = New System.Drawing.Point(242, 2)
        Me.btnUpdateMethod.Margin = New System.Windows.Forms.Padding(2)
        Me.btnUpdateMethod.Name = "btnUpdateMethod"
        Me.btnUpdateMethod.Size = New System.Drawing.Size(66, 26)
        Me.btnUpdateMethod.TabIndex = 1
        Me.btnUpdateMethod.Text = "Update"
        '
        'cmbMethod
        '
        Me.cmbMethod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbMethod.Location = New System.Drawing.Point(3, 4)
        Me.cmbMethod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbMethod.Name = "cmbMethod"
        Me.cmbMethod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMethod.Size = New System.Drawing.Size(304, 20)
        Me.cmbMethod.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'lblMessage
        '
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(234, 24)
        Me.lblMessage.TabIndex = 13
        '
        'dlgMethodAlertMngr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 88)
        Me.Controls.Add(Me.GroupControl6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("dlgMethodAlertMngr.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(320, 120)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(320, 120)
        Me.Name = "dlgMethodAlertMngr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Method - Alert Manager"
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents cmbMethod As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents btnUpdateMethod As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
End Class
