<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgCalendar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCalendar))
        Me.dtNavigator = New DevExpress.XtraScheduler.DateNavigator()
        CType(Me.dtNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtNavigator.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtNavigator
        '
        Me.dtNavigator.AllowAnimatedContentChange = True
        Me.dtNavigator.CalendarAppearance.DayCellSpecial.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.dtNavigator.CalendarAppearance.DayCellSpecial.Options.UseFont = True
        Me.dtNavigator.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtNavigator.Cursor = System.Windows.Forms.Cursors.Default
        Me.dtNavigator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtNavigator.FirstDayOfWeek = System.DayOfWeek.Sunday
        Me.dtNavigator.Location = New System.Drawing.Point(0, 0)
        Me.dtNavigator.Name = "dtNavigator"
        Me.dtNavigator.ShowFooter = False
        Me.dtNavigator.ShowTodayButton = False
        Me.dtNavigator.ShowWeekNumbers = False
        Me.dtNavigator.Size = New System.Drawing.Size(234, 231)
        Me.dtNavigator.TabIndex = 0
        '
        'dlgCalendar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 231)
        Me.Controls.Add(Me.dtNavigator)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.Icon = CType(resources.GetObject("dlgCalendar.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgCalendar"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        CType(Me.dtNavigator.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtNavigator As DevExpress.XtraScheduler.DateNavigator
End Class
