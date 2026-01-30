<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTempObjects
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTempObjects))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcTempObjects = New DevExpress.XtraGrid.GridControl()
        Me.cmTempObjects = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiCopyClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvTempObjects = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView23 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.gcTempObjects, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmTempObjects.SuspendLayout()
        CType(Me.gvTempObjects, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView23, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(984, 611)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "NB"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.gcTempObjects, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(980, 589)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'gcTempObjects
        '
        Me.gcTempObjects.AllowDrop = True
        Me.gcTempObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTempObjects.Location = New System.Drawing.Point(2, 28)
        Me.gcTempObjects.MainView = Me.gvTempObjects
        Me.gcTempObjects.Margin = New System.Windows.Forms.Padding(2)
        Me.gcTempObjects.Name = "gcTempObjects"
        Me.gcTempObjects.Size = New System.Drawing.Size(976, 559)
        Me.gcTempObjects.TabIndex = 12
        Me.gcTempObjects.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTempObjects, Me.GridView23})
        '
        'cmTempObjects
        '
        Me.cmTempObjects.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmTempObjects.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiCopyClipboard})
        Me.cmTempObjects.Name = "cm_TagManagement"
        Me.cmTempObjects.Size = New System.Drawing.Size(189, 26)
        '
        'tsmiCopyClipboard
        '
        Me.tsmiCopyClipboard.Name = "tsmiCopyClipboard"
        Me.tsmiCopyClipboard.Size = New System.Drawing.Size(188, 22)
        Me.tsmiCopyClipboard.Text = "Paste From Clipboard"
        '
        'gvTempObjects
        '
        Me.gvTempObjects.ActiveFilterEnabled = False
        Me.gvTempObjects.GridControl = Me.gcTempObjects
        Me.gvTempObjects.Name = "gvTempObjects"
        Me.gvTempObjects.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTempObjects.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTempObjects.OptionsBehavior.Editable = False
        Me.gvTempObjects.OptionsBehavior.ReadOnly = True
        Me.gvTempObjects.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTempObjects.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTempObjects.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTempObjects.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTempObjects.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.None
        Me.gvTempObjects.OptionsSelection.MultiSelect = True
        Me.gvTempObjects.OptionsView.ShowGroupPanel = False
        '
        'GridView23
        '
        Me.GridView23.GridControl = Me.gcTempObjects
        Me.GridView23.Name = "GridView23"
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(974, 20)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Grid showing all current temporary NB objects in the CellSens database"
        '
        'dlgTempObjects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.GroupControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 500)
        Me.Name = "dlgTempObjects"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Temporary Objects"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.gcTempObjects, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmTempObjects.ResumeLayout(False)
        CType(Me.gvTempObjects, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView23, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcTempObjects As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTempObjects As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView23 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmTempObjects As ContextMenuStrip
    Friend WithEvents tsmiCopyClipboard As ToolStripMenuItem
End Class
