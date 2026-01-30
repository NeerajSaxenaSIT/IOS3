<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFreqChooser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFreqChooser))
        Me.SuperTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.ListViewFreqGSM = New System.Windows.Forms.ListView()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.ListViewfreqDCS = New System.Windows.Forms.ListView()
        CType(Me.SuperTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SuperTabControl1
        '
        Me.SuperTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControl1.LookAndFeel.SkinName = "Office 2013"
        Me.SuperTabControl1.Name = "SuperTabControl1"
        Me.SuperTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.SuperTabControl1.Size = New System.Drawing.Size(624, 361)
        Me.SuperTabControl1.TabIndex = 0
        Me.SuperTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.ListViewFreqGSM)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(622, 336)
        Me.XtraTabPage1.Text = "(E)GSM"
        '
        'ListViewFreqGSM
        '
        Me.ListViewFreqGSM.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewFreqGSM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListViewFreqGSM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewFreqGSM.FullRowSelect = True
        Me.ListViewFreqGSM.Location = New System.Drawing.Point(0, 0)
        Me.ListViewFreqGSM.Margin = New System.Windows.Forms.Padding(4)
        Me.ListViewFreqGSM.Name = "ListViewFreqGSM"
        Me.ListViewFreqGSM.Size = New System.Drawing.Size(622, 336)
        Me.ListViewFreqGSM.TabIndex = 1
        Me.ListViewFreqGSM.UseCompatibleStateImageBehavior = False
        Me.ListViewFreqGSM.View = System.Windows.Forms.View.Details
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.ListViewfreqDCS)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(622, 336)
        Me.XtraTabPage2.Text = "DCS"
        '
        'ListViewfreqDCS
        '
        Me.ListViewfreqDCS.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewfreqDCS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewfreqDCS.FullRowSelect = True
        Me.ListViewfreqDCS.Location = New System.Drawing.Point(0, 0)
        Me.ListViewfreqDCS.Margin = New System.Windows.Forms.Padding(4)
        Me.ListViewfreqDCS.Name = "ListViewfreqDCS"
        Me.ListViewfreqDCS.Size = New System.Drawing.Size(622, 336)
        Me.ListViewfreqDCS.TabIndex = 1
        Me.ListViewfreqDCS.UseCompatibleStateImageBehavior = False
        Me.ListViewfreqDCS.View = System.Windows.Forms.View.Details
        '
        'frmFreqChooser
        '
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 361)
        Me.Controls.Add(Me.SuperTabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Office 2013"
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFreqChooser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frequency Resultset"
        CType(Me.SuperTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SuperTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ListViewFreqGSM As System.Windows.Forms.ListView
    Friend WithEvents ListViewfreqDCS As System.Windows.Forms.ListView
End Class
