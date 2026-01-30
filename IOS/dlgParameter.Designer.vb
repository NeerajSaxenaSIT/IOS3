<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgParameter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgParameter))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNewTemplate = New DevExpress.XtraEditors.TextEdit()
        Me.chkCopyTemplate = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbTemplate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnSubmit = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.txtNewTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCopyTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtNewTemplate, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.chkCopyTemplate, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbTemplate, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSubmit, 1, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(414, 158)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 23)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(128, 19)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "Enter Template Name:"
        '
        'txtNewTemplate
        '
        Me.txtNewTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNewTemplate.Location = New System.Drawing.Point(137, 23)
        Me.txtNewTemplate.Name = "txtNewTemplate"
        Me.txtNewTemplate.Size = New System.Drawing.Size(274, 20)
        Me.txtNewTemplate.TabIndex = 6
        '
        'chkCopyTemplate
        '
        Me.chkCopyTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkCopyTemplate.Location = New System.Drawing.Point(137, 48)
        Me.chkCopyTemplate.Name = "chkCopyTemplate"
        Me.chkCopyTemplate.Properties.Caption = "Copy From Existing"
        Me.chkCopyTemplate.Size = New System.Drawing.Size(274, 25)
        Me.chkCopyTemplate.TabIndex = 7
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTemplate.EditValue = "Select Template"
        Me.cmbTemplate.Location = New System.Drawing.Point(137, 79)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTemplate.Size = New System.Drawing.Size(274, 20)
        Me.cmbTemplate.TabIndex = 8
        '
        'btnSubmit
        '
        Me.btnSubmit.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSubmit.Location = New System.Drawing.Point(137, 105)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(104, 26)
        Me.btnSubmit.TabIndex = 9
        Me.btnSubmit.Text = "Submit"
        '
        'dlgParameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 158)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgParameter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add New Template"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.txtNewTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCopyTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNewTemplate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkCopyTemplate As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmbTemplate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnSubmit As DevExpress.XtraEditors.SimpleButton
End Class
