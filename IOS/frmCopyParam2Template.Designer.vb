<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCopyParam2Template
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCopyParam2Template))
        Me.sccMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcMOParam = New DevExpress.XtraGrid.GridControl()
        Me.gvMOParam = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcTemplateList = New DevExpress.XtraGrid.GridControl()
        Me.gvTemplateList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSelectAllParams = New DevExpress.XtraEditors.CheckEdit()
        Me.chkSelectAllTemplates = New DevExpress.XtraEditors.CheckEdit()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel1.SuspendLayout()
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sccMain.Panel2.SuspendLayout()
        Me.sccMain.SuspendLayout()
        CType(Me.gcMOParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMOParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcTemplateList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTemplateList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMain.SuspendLayout()
        Me.tlpBottom.SuspendLayout()
        Me.tlpTop.SuspendLayout()
        CType(Me.chkSelectAllParams.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSelectAllTemplates.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sccMain
        '
        Me.sccMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sccMain.Location = New System.Drawing.Point(3, 33)
        Me.sccMain.Name = "sccMain"
        '
        'sccMain.Panel1
        '
        Me.sccMain.Panel1.Controls.Add(Me.gcMOParam)
        Me.sccMain.Panel1.MinSize = 400
        Me.sccMain.Panel1.Text = "Panel1"
        '
        'sccMain.Panel2
        '
        Me.sccMain.Panel2.Controls.Add(Me.gcTemplateList)
        Me.sccMain.Panel2.MinSize = 400
        Me.sccMain.Panel2.Text = "Panel2"
        Me.sccMain.Size = New System.Drawing.Size(884, 545)
        Me.sccMain.SplitterPosition = 439
        Me.sccMain.TabIndex = 0
        '
        'gcMOParam
        '
        Me.gcMOParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcMOParam.Location = New System.Drawing.Point(0, 0)
        Me.gcMOParam.MainView = Me.gvMOParam
        Me.gcMOParam.Name = "gcMOParam"
        Me.gcMOParam.Size = New System.Drawing.Size(439, 545)
        Me.gcMOParam.TabIndex = 10
        Me.gcMOParam.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvMOParam})
        '
        'gvMOParam
        '
        Me.gvMOParam.GridControl = Me.gcMOParam
        Me.gvMOParam.Name = "gvMOParam"
        Me.gvMOParam.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvMOParam.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvMOParam.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMOParam.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMOParam.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvMOParam.OptionsView.ColumnAutoWidth = False
        Me.gvMOParam.OptionsView.ShowAutoFilterRow = True
        Me.gvMOParam.OptionsView.ShowGroupPanel = False
        '
        'gcTemplateList
        '
        Me.gcTemplateList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTemplateList.Location = New System.Drawing.Point(0, 0)
        Me.gcTemplateList.MainView = Me.gvTemplateList
        Me.gcTemplateList.Name = "gcTemplateList"
        Me.gcTemplateList.Size = New System.Drawing.Size(440, 545)
        Me.gcTemplateList.TabIndex = 10
        Me.gcTemplateList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTemplateList})
        '
        'gvTemplateList
        '
        Me.gvTemplateList.GridControl = Me.gcTemplateList
        Me.gvTemplateList.Name = "gvTemplateList"
        Me.gvTemplateList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTemplateList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvTemplateList.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTemplateList.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTemplateList.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvTemplateList.OptionsView.ColumnAutoWidth = False
        Me.gvTemplateList.OptionsView.ShowAutoFilterRow = True
        Me.gvTemplateList.OptionsView.ShowGroupPanel = False
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 2)
        Me.tlpMain.Controls.Add(Me.tlpTop, 0, 0)
        Me.tlpMain.Controls.Add(Me.sccMain, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.tlpMain.Size = New System.Drawing.Size(890, 618)
        Me.tlpMain.TabIndex = 1
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnCommit, 1, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(1, 582)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(888, 35)
        Me.tlpBottom.TabIndex = 1
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(782, 29)
        Me.lblMessage.TabIndex = 18
        '
        'btnCommit
        '
        Me.btnCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCommit.Location = New System.Drawing.Point(791, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(94, 29)
        Me.btnCommit.TabIndex = 0
        Me.btnCommit.Text = "Commit"
        '
        'tlpTop
        '
        Me.tlpTop.ColumnCount = 4
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpTop.Controls.Add(Me.chkSelectAllParams, 0, 0)
        Me.tlpTop.Controls.Add(Me.chkSelectAllTemplates, 2, 0)
        Me.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTop.Location = New System.Drawing.Point(1, 1)
        Me.tlpTop.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpTop.Name = "tlpTop"
        Me.tlpTop.RowCount = 1
        Me.tlpTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTop.Size = New System.Drawing.Size(888, 28)
        Me.tlpTop.TabIndex = 2
        '
        'chkSelectAllParams
        '
        Me.chkSelectAllParams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkSelectAllParams.Location = New System.Drawing.Point(10, 3)
        Me.chkSelectAllParams.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.chkSelectAllParams.Name = "chkSelectAllParams"
        Me.chkSelectAllParams.Properties.Caption = "Select All Params"
        Me.chkSelectAllParams.Size = New System.Drawing.Size(137, 22)
        Me.chkSelectAllParams.TabIndex = 0
        '
        'chkSelectAllTemplates
        '
        Me.chkSelectAllTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkSelectAllTemplates.Location = New System.Drawing.Point(454, 3)
        Me.chkSelectAllTemplates.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.chkSelectAllTemplates.Name = "chkSelectAllTemplates"
        Me.chkSelectAllTemplates.Properties.Caption = "Select All Templates"
        Me.chkSelectAllTemplates.Size = New System.Drawing.Size(137, 22)
        Me.chkSelectAllTemplates.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'frmCopyParam2Template
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 618)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("frmCopyParam2Template.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 650)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 650)
        Me.Name = "frmCopyParam2Template"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy Param(s) To Template(s)"
        CType(Me.sccMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel1.ResumeLayout(False)
        CType(Me.sccMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.Panel2.ResumeLayout(False)
        CType(Me.sccMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sccMain.ResumeLayout(False)
        CType(Me.gcMOParam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMOParam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcTemplateList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTemplateList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMain.ResumeLayout(False)
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        Me.tlpTop.ResumeLayout(False)
        CType(Me.chkSelectAllParams.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSelectAllTemplates.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents sccMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gcMOParam As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMOParam As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcTemplateList As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTemplateList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents btnCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents tlpTop As TableLayoutPanel
    Friend WithEvents chkSelectAllParams As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkSelectAllTemplates As DevExpress.XtraEditors.CheckEdit
End Class
