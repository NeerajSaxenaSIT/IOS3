<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgStartup
	Inherits System.Windows.Forms.Form

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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgStartup))
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
		Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
		Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
		Me.TableLayoutPanel1.SuspendLayout()
		Me.TableLayoutPanel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.ColumnCount = 1
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
		Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 2
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(184, 68)
		Me.TableLayoutPanel1.TabIndex = 0
		'
		'LabelControl1
		'
		Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
		Me.LabelControl1.Appearance.Image = CType(resources.GetObject("LabelControl1.Appearance.Image"), System.Drawing.Image)
		Me.LabelControl1.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.LabelControl1.Appearance.Options.UseFont = True
		Me.LabelControl1.Appearance.Options.UseImage = True
		Me.LabelControl1.Appearance.Options.UseImageAlign = True
		Me.LabelControl1.Appearance.Options.UseTextOptions = True
		Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
		Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
		Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
		Me.LabelControl1.Name = "LabelControl1"
		Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
		Me.LabelControl1.Size = New System.Drawing.Size(178, 32)
		Me.LabelControl1.TabIndex = 0
		Me.LabelControl1.Text = "Exiting CIOS. . ."
		'
		'TableLayoutPanel2
		'
		Me.TableLayoutPanel2.ColumnCount = 3
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel2.Controls.Add(Me.btnOk, 1, 0)
		Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 38)
		Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
		Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
		Me.TableLayoutPanel2.RowCount = 1
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel2.Size = New System.Drawing.Size(184, 30)
		Me.TableLayoutPanel2.TabIndex = 1
		'
		'btnOk
		'
		Me.btnOk.Dock = System.Windows.Forms.DockStyle.Fill
		Me.btnOk.Location = New System.Drawing.Point(70, 3)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(44, 24)
		Me.btnOk.TabIndex = 0
		Me.btnOk.Text = "OK"
		'
		'dlgStartup
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(184, 68)
		Me.ControlBox = False
		Me.Controls.Add(Me.TableLayoutPanel1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(200, 107)
		Me.Name = "dlgStartup"
		Me.Text = "CIOS Startup"
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.TableLayoutPanel1.PerformLayout()
		Me.TableLayoutPanel2.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
	Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
	Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
	Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
End Class
