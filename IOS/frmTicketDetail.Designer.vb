<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTicketDetail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTicketDetail))
        Me.WebBrowser_Ticket = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'WebBrowser_Ticket
        '
        Me.WebBrowser_Ticket.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser_Ticket.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser_Ticket.Margin = New System.Windows.Forms.Padding(4)
        Me.WebBrowser_Ticket.MinimumSize = New System.Drawing.Size(27, 25)
        Me.WebBrowser_Ticket.Name = "WebBrowser_Ticket"
        Me.WebBrowser_Ticket.Size = New System.Drawing.Size(1202, 749)
        Me.WebBrowser_Ticket.TabIndex = 5
        '
        'frmTicketDetail
        '
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1202, 749)
        Me.Controls.Add(Me.WebBrowser_Ticket)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTicketDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ticket Details"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WebBrowser_Ticket As System.Windows.Forms.WebBrowser
End Class
