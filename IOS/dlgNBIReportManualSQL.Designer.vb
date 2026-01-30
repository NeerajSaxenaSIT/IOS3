<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgNBIReportManualSQL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgNBIReportManualSQL))
        Me.gcManualSQLQry = New DevExpress.XtraGrid.GridControl()
        Me.gvManualSQLQry = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.gcManualSQLQry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvManualSQLQry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcManualSQLQry
        '
        Me.gcManualSQLQry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcManualSQLQry.Location = New System.Drawing.Point(0, 0)
        Me.gcManualSQLQry.MainView = Me.gvManualSQLQry
        Me.gcManualSQLQry.Name = "gcManualSQLQry"
        Me.gcManualSQLQry.Size = New System.Drawing.Size(798, 448)
        Me.gcManualSQLQry.TabIndex = 17
        Me.gcManualSQLQry.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvManualSQLQry})
        '
        'gvManualSQLQry
        '
        Me.gvManualSQLQry.GridControl = Me.gcManualSQLQry
        Me.gvManualSQLQry.Name = "gvManualSQLQry"
        Me.gvManualSQLQry.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvManualSQLQry.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvManualSQLQry.OptionsBehavior.Editable = False
        Me.gvManualSQLQry.OptionsBehavior.ReadOnly = True
        Me.gvManualSQLQry.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvManualSQLQry.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvManualSQLQry.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvManualSQLQry.OptionsView.ColumnAutoWidth = False
        Me.gvManualSQLQry.OptionsView.ShowGroupPanel = False
        '
        'dlgNBIReportManualSQL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(798, 448)
        Me.Controls.Add(Me.gcManualSQLQry)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("dlgNBIReportManualSQL.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(800, 480)
        Me.MinimumSize = New System.Drawing.Size(800, 480)
        Me.Name = "dlgNBIReportManualSQL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NBI Report - Manual SQL Query"
        CType(Me.gcManualSQLQry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvManualSQLQry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gcManualSQLQry As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvManualSQLQry As DevExpress.XtraGrid.Views.Grid.GridView
End Class
