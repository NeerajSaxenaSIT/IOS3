<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_AutoUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_AutoUpdate))
        Me.lbl_Status2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Downloadprogresslabel2 = New System.Windows.Forms.Label()
        Me.FileProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.Downloadprogressbar2 = New System.Windows.Forms.ProgressBar()
        Me.bgWorker = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'lbl_Status2
        '
        Me.lbl_Status2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lbl_Status2, "lbl_Status2")
        Me.lbl_Status2.Name = "lbl_Status2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Downloadprogresslabel2
        '
        Me.Downloadprogresslabel2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Downloadprogresslabel2, "Downloadprogresslabel2")
        Me.Downloadprogresslabel2.Name = "Downloadprogresslabel2"
        '
        'FileProgressBar2
        '
        resources.ApplyResources(Me.FileProgressBar2, "FileProgressBar2")
        Me.FileProgressBar2.Name = "FileProgressBar2"
        '
        'Downloadprogressbar2
        '
        resources.ApplyResources(Me.Downloadprogressbar2, "Downloadprogressbar2")
        Me.Downloadprogressbar2.Maximum = 21
        Me.Downloadprogressbar2.Name = "Downloadprogressbar2"
        '
        'bgWorker
        '
        Me.bgWorker.WorkerSupportsCancellation = True
        '
        'frm_AutoUpdate
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lbl_Status2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Downloadprogresslabel2)
        Me.Controls.Add(Me.FileProgressBar2)
        Me.Controls.Add(Me.Downloadprogressbar2)
        Me.Name = "frm_AutoUpdate"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbl_Status2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Downloadprogresslabel2 As System.Windows.Forms.Label
    Friend WithEvents FileProgressBar2 As System.Windows.Forms.ProgressBar
    Friend WithEvents Downloadprogressbar2 As System.Windows.Forms.ProgressBar
    Friend WithEvents bgWorker As System.ComponentModel.BackgroundWorker
End Class
