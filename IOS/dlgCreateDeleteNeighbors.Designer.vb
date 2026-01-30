<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgCreateDeleteNeighbors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCreateDeleteNeighbors))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gcNeighbors = New DevExpress.XtraGrid.GridControl()
        Me.gvNeighbors = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView23 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtManualCampName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ceIsPublic = New DevExpress.XtraEditors.CheckEdit()
        Me.btnCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.gcNeighbors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvNeighbors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView23, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtManualCampName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsPublic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.gcNeighbors, 0, 1)
        Me.tlpMain.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.tlpMain.Controls.Add(Me.lblStatus, 0, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0!))
        Me.tlpMain.Size = New System.Drawing.Size(784, 561)
        Me.tlpMain.TabIndex = 0
        '
        'gcNeighbors
        '
        Me.gcNeighbors.AllowDrop = True
        Me.gcNeighbors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcNeighbors.Location = New System.Drawing.Point(2, 34)
        Me.gcNeighbors.MainView = Me.gvNeighbors
        Me.gcNeighbors.Margin = New System.Windows.Forms.Padding(2)
        Me.gcNeighbors.Name = "gcNeighbors"
        Me.gcNeighbors.Size = New System.Drawing.Size(780, 525)
        Me.gcNeighbors.TabIndex = 12
        Me.gcNeighbors.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvNeighbors, Me.GridView23})
        '
        'gvNeighbors
        '
        Me.gvNeighbors.ActiveFilterEnabled = False
        Me.gvNeighbors.GridControl = Me.gcNeighbors
        Me.gvNeighbors.Name = "gvNeighbors"
        Me.gvNeighbors.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNeighbors.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvNeighbors.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNeighbors.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNeighbors.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNeighbors.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvNeighbors.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append
        Me.gvNeighbors.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvNeighbors.OptionsSelection.MultiSelect = True
        Me.gvNeighbors.OptionsView.ShowGroupPanel = False
        '
        'GridView23
        '
        Me.GridView23.GridControl = Me.gcNeighbors
        Me.GridView23.Name = "GridView23"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 5
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.txtManualCampName, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl5, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl1, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ceIsPublic, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnCommit, 4, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(782, 30)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'txtManualCampName
        '
        Me.txtManualCampName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtManualCampName.Location = New System.Drawing.Point(133, 3)
        Me.txtManualCampName.Name = "txtManualCampName"
        Me.txtManualCampName.Size = New System.Drawing.Size(184, 20)
        Me.txtManualCampName.TabIndex = 11
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 4)
        Me.LabelControl5.Size = New System.Drawing.Size(124, 24)
        Me.LabelControl5.TabIndex = 9
        Me.LabelControl5.Text = "Manual Campaign Name"
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(323, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 4)
        Me.LabelControl1.Size = New System.Drawing.Size(49, 24)
        Me.LabelControl1.TabIndex = 10
        Me.LabelControl1.Text = "Is Public"
        '
        'ceIsPublic
        '
        Me.ceIsPublic.Dock = System.Windows.Forms.DockStyle.Left
        Me.ceIsPublic.Location = New System.Drawing.Point(380, 3)
        Me.ceIsPublic.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsPublic.Name = "ceIsPublic"
        Me.ceIsPublic.Properties.Caption = ""
        Me.ceIsPublic.Size = New System.Drawing.Size(28, 24)
        Me.ceIsPublic.TabIndex = 12
        '
        'btnCommit
        '
        Me.btnCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCommit.Location = New System.Drawing.Point(678, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(101, 24)
        Me.btnCommit.TabIndex = 13
        Me.btnCommit.Text = "Commit"
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Location = New System.Drawing.Point(3, 564)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblStatus.Size = New System.Drawing.Size(778, 1)
        Me.lblStatus.TabIndex = 13
        '
        'Timer1
        '
        Me.Timer1.Interval = 8000
        '
        'dlgCreateDeleteNeighbors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "dlgCreateDeleteNeighbors"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create Delete Neighbors"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        CType(Me.gcNeighbors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvNeighbors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView23, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.txtManualCampName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsPublic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtManualCampName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ceIsPublic As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcNeighbors As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvNeighbors As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView23 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
End Class
