<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucICMKPIList
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstICMKPI = New DevExpress.XtraEditors.ListBoxControl()
        Me.cmbKPI = New System.Windows.Forms.ComboBox()
        CType(Me.lstICMKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.Label1.Size = New System.Drawing.Size(154, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select KPI"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(3, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.Label2.Size = New System.Drawing.Size(154, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Selected KPI"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lstICMKPI
        '
        Me.lstICMKPI.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstICMKPI.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstICMKPI.Location = New System.Drawing.Point(6, 79)
        Me.lstICMKPI.Name = "lstICMKPI"
        Me.lstICMKPI.Size = New System.Drawing.Size(154, 151)
        Me.lstICMKPI.TabIndex = 3
        '
        'cmbKPI
        '
        Me.cmbKPI.DropDownHeight = 100
        Me.cmbKPI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbKPI.FormattingEnabled = True
        Me.cmbKPI.IntegralHeight = False
        Me.cmbKPI.ItemHeight = 13
        Me.cmbKPI.Location = New System.Drawing.Point(6, 28)
        Me.cmbKPI.Name = "cmbKPI"
        Me.cmbKPI.Size = New System.Drawing.Size(151, 21)
        Me.cmbKPI.TabIndex = 4
        '
        'ucICMKPIList
        '
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmbKPI)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstICMKPI)
        Me.LookAndFeel.SkinName = "Office 2013"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "ucICMKPIList"
        Me.Size = New System.Drawing.Size(164, 233)
        CType(Me.lstICMKPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstICMKPI As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents cmbKPI As System.Windows.Forms.ComboBox

End Class
