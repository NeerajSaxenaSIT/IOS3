<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSBCalculatedSeriesNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgSBCalculatedSeriesNew))
        Me.exTLP_main = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.vbtn_AddSeries = New DevExpress.XtraEditors.SimpleButton()
        Me.vbtn_Cancel = New DevExpress.XtraEditors.SimpleButton()
        Me.vlblMSG = New DevExpress.XtraEditors.LabelControl()
        Me.exTLP_main.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'exTLP_main
        '
        Me.exTLP_main.ColumnCount = 1
        Me.exTLP_main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTLP_main.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.exTLP_main.Controls.Add(Me.vlblMSG, 0, 1)
        Me.exTLP_main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.exTLP_main.Location = New System.Drawing.Point(0, 0)
        Me.exTLP_main.Name = "exTLP_main"
        Me.exTLP_main.RowCount = 3
        Me.exTLP_main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.exTLP_main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.exTLP_main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.exTLP_main.Size = New System.Drawing.Size(282, 162)
        Me.exTLP_main.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.vbtn_AddSeries, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.vbtn_Cancel, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 130)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(276, 29)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'vbtn_AddSeries
        '
        Me.vbtn_AddSeries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vbtn_AddSeries.Location = New System.Drawing.Point(3, 3)
        Me.vbtn_AddSeries.Name = "vbtn_AddSeries"
        Me.vbtn_AddSeries.Size = New System.Drawing.Size(132, 23)
        Me.vbtn_AddSeries.TabIndex = 0
        Me.vbtn_AddSeries.Text = "Add Series"
        '
        'vbtn_Cancel
        '
        Me.vbtn_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vbtn_Cancel.Location = New System.Drawing.Point(141, 3)
        Me.vbtn_Cancel.Name = "vbtn_Cancel"
        Me.vbtn_Cancel.Size = New System.Drawing.Size(132, 23)
        Me.vbtn_Cancel.TabIndex = 1
        Me.vbtn_Cancel.Text = "Cancel"
        '
        'vlblMSG
        '
        Me.vlblMSG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vlblMSG.Location = New System.Drawing.Point(3, 110)
        Me.vlblMSG.Name = "vlblMSG"
        Me.vlblMSG.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.vlblMSG.Size = New System.Drawing.Size(276, 14)
        Me.vlblMSG.TabIndex = 1
        '
        'dlgSBCalculatedSeriesNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 162)
        Me.ControlBox = False
        Me.Controls.Add(Me.exTLP_main)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Office 2013"
        Me.Name = "dlgSBCalculatedSeriesNew"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Calculated Series"
        Me.exTLP_main.ResumeLayout(False)
        Me.exTLP_main.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents exTLP_main As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vbtn_AddSeries As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents vbtn_Cancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents vlblMSG As DevExpress.XtraEditors.LabelControl
End Class
