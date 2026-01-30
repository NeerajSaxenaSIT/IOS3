<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgThresholdSetDateListDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgThresholdSetDateListDetails))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.gcTSDateListDetail = New DevExpress.XtraGrid.GridControl()
        Me.cmTSDetail = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiPasteFromClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDeleteSelectedRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvTSDateListDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.tlpMain.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.gcTSDateListDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmTSDetail.SuspendLayout()
        CType(Me.gvTSDateListDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.tlpMain)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(594, 468)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Add/Update Thresold Set Date List Details"
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.LabelControl10, 0, 0)
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 3)
        Me.tlpMain.Controls.Add(Me.TableLayoutPanel12, 0, 1)
        Me.tlpMain.Controls.Add(Me.gcTSDateListDetail, 0, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(2, 23)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 4
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.Size = New System.Drawing.Size(590, 443)
        Me.tlpMain.TabIndex = 5
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Image = CType(resources.GetObject("LabelControl10.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl10.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl10.Appearance.Options.UseImage = True
        Me.LabelControl10.Appearance.Options.UseImageAlign = True
        Me.LabelControl10.Appearance.Options.UseTextOptions = True
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl10.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(584, 26)
        Me.LabelControl10.TabIndex = 1
        Me.LabelControl10.Text = "Paste From clipboard, dates should be in ""dd-MM-yyyy"" Format "
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Appearance.Options.UseFont = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 416)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(584, 24)
        Me.lblMessage.TabIndex = 14
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 2
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.btnCommit, 1, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(1, 33)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(588, 33)
        Me.TableLayoutPanel12.TabIndex = 5
        '
        'btnCommit
        '
        Me.btnCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCommit.Location = New System.Drawing.Point(510, 2)
        Me.btnCommit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(76, 29)
        Me.btnCommit.TabIndex = 2
        Me.btnCommit.Text = "Commit"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Image = CType(resources.GetObject("LabelControl1.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl1.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl1.Appearance.Options.UseImage = True
        Me.LabelControl1.Appearance.Options.UseImageAlign = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(502, 27)
        Me.LabelControl1.TabIndex = 15
        Me.LabelControl1.Text = "Copy Paste from Excel (delimiter either ""comma"" or ""tab"")"
        '
        'gcTSDateListDetail
        '
        Me.gcTSDateListDetail.AllowDrop = True
        Me.gcTSDateListDetail.ContextMenuStrip = Me.cmTSDetail
        Me.gcTSDateListDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTSDateListDetail.Location = New System.Drawing.Point(3, 70)
        Me.gcTSDateListDetail.MainView = Me.gvTSDateListDetail
        Me.gcTSDateListDetail.Name = "gcTSDateListDetail"
        Me.gcTSDateListDetail.Size = New System.Drawing.Size(584, 340)
        Me.gcTSDateListDetail.TabIndex = 4
        Me.gcTSDateListDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTSDateListDetail, Me.GridView3})
        '
        'cmTSDetail
        '
        Me.cmTSDetail.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmTSDetail.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiPasteFromClipboard, Me.tsmiDeleteSelectedRow})
        Me.cmTSDetail.Name = "cm_TagManagement"
        Me.cmTSDetail.Size = New System.Drawing.Size(189, 48)
        '
        'tsmiPasteFromClipboard
        '
        Me.tsmiPasteFromClipboard.Name = "tsmiPasteFromClipboard"
        Me.tsmiPasteFromClipboard.Size = New System.Drawing.Size(188, 22)
        Me.tsmiPasteFromClipboard.Text = "Paste From Clipboard"
        '
        'tsmiDeleteSelectedRow
        '
        Me.tsmiDeleteSelectedRow.Name = "tsmiDeleteSelectedRow"
        Me.tsmiDeleteSelectedRow.Size = New System.Drawing.Size(188, 22)
        Me.tsmiDeleteSelectedRow.Text = "Delete Selected Row"
        '
        'gvTSDateListDetail
        '
        Me.gvTSDateListDetail.ActiveFilterEnabled = False
        Me.gvTSDateListDetail.GridControl = Me.gcTSDateListDetail
        Me.gvTSDateListDetail.Name = "gvTSDateListDetail"
        Me.gvTSDateListDetail.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTSDateListDetail.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTSDateListDetail.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTSDateListDetail.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTSDateListDetail.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTSDateListDetail.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted
        Me.gvTSDateListDetail.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTSDateListDetail.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTSDateListDetail.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvTSDateListDetail.OptionsSelection.MultiSelect = True
        Me.gvTSDateListDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gvTSDateListDetail.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.gcTSDateListDetail
        Me.GridView3.Name = "GridView3"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgThresholdSetDateListDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 468)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("dlgThresholdSetDateListDetails.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(596, 500)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(596, 500)
        Me.Name = "dlgThresholdSetDateListDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thresold Set Date List Details"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        CType(Me.gcTSDateListDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmTSDetail.ResumeLayout(False)
        CType(Me.gvTSDateListDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcTSDateListDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTSDateListDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmTSDetail As ContextMenuStrip
    Friend WithEvents tsmiPasteFromClipboard As ToolStripMenuItem
    Friend WithEvents tsmiDeleteSelectedRow As ToolStripMenuItem
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
