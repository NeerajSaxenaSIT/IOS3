<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInternetExplorer
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.webView2 = New Microsoft.Web.WebView2.WinForms.WebView2()
        CType(Me.webView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'webView2
        '
        Me.webView2.CreationProperties = Nothing
        Me.webView2.DefaultBackgroundColor = System.Drawing.Color.White
        Me.webView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webView2.Location = New System.Drawing.Point(0, 0)
        Me.webView2.Name = "webView2"
        Me.webView2.Size = New System.Drawing.Size(1031, 665)
        Me.webView2.TabIndex = 0
        Me.webView2.ZoomFactor = 1.0R
        '
        'frmInternetExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1031, 665)
        Me.ControlBox = False
        Me.Controls.Add(Me.webView2)
        Me.IconOptions.ShowIcon = False
        Me.MinimumSize = New System.Drawing.Size(1033, 697)
        Me.Name = "frmInternetExplorer"
        Me.Text = "Web Client"
        CType(Me.webView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents webView2 As Microsoft.Web.WebView2.WinForms.WebView2
End Class
