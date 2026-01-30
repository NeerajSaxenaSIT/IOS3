<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgAddKPI2ThresholdSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgAddKPI2ThresholdSet))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnInsert = New DevExpress.XtraEditors.SimpleButton()
        Me.chkListKPIs = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpMain.SuspendLayout()
        Me.tlpBottom.SuspendLayout()
        CType(Me.chkListKPIs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 2)
        Me.tlpMain.Controls.Add(Me.chkListKPIs, 0, 1)
        Me.tlpMain.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.Size = New System.Drawing.Size(394, 368)
        Me.tlpMain.TabIndex = 0
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnInsert, 1, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(1, 334)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(392, 33)
        Me.tlpBottom.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(311, 27)
        Me.lblMessage.TabIndex = 19
        '
        'btnInsert
        '
        Me.btnInsert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnInsert.Location = New System.Drawing.Point(320, 3)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(69, 27)
        Me.btnInsert.TabIndex = 0
        Me.btnInsert.Text = "Add"
        '
        'chkListKPIs
        '
        Me.chkListKPIs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkListKPIs.Location = New System.Drawing.Point(3, 28)
        Me.chkListKPIs.Name = "chkListKPIs"
        Me.chkListKPIs.Size = New System.Drawing.Size(388, 302)
        Me.chkListKPIs.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(388, 19)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Select one or more KPIs from the list to add"
        '
        'dlgAddKPI2ThresholdSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 368)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("dlgAddKPI2ThresholdSet.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(400, 400)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 400)
        Me.Name = "dlgAddKPI2ThresholdSet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add KPIs To Threshold Set"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        CType(Me.chkListKPIs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents btnInsert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkListKPIs As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
