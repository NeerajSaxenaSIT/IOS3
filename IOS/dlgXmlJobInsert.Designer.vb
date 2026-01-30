<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgXmlJobInsert
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgXmlJobInsert))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.tlpCommit = New System.Windows.Forms.TableLayoutPanel()
        Me.btnXmlJobCommit = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblXmlJob = New DevExpress.XtraEditors.LabelControl()
        Me.cmbVendor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpPaste = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl35 = New DevExpress.XtraEditors.LabelControl()
        Me.btnXmlJobPaste = New DevExpress.XtraEditors.SimpleButton()
        Me.lblRecordsCount = New DevExpress.XtraEditors.LabelControl()
        Me.gcXmlJobData = New DevExpress.XtraGrid.GridControl()
        Me.gvXmlJobData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpCommit.SuspendLayout()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpPaste.SuspendLayout()
        CType(Me.gcXmlJobData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvXmlJobData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 3)
        Me.tlpMain.Controls.Add(Me.tlpCommit, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpPaste, 0, 1)
        Me.tlpMain.Controls.Add(Me.gcXmlJobData, 0, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 4
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.Size = New System.Drawing.Size(818, 593)
        Me.tlpMain.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 566)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(812, 24)
        Me.lblMessage.TabIndex = 21
        '
        'tlpCommit
        '
        Me.tlpCommit.ColumnCount = 5
        Me.tlpCommit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpCommit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCommit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpCommit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.tlpCommit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpCommit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpCommit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpCommit.Controls.Add(Me.btnXmlJobCommit, 4, 0)
        Me.tlpCommit.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpCommit.Controls.Add(Me.lblXmlJob, 1, 0)
        Me.tlpCommit.Controls.Add(Me.cmbVendor, 3, 0)
        Me.tlpCommit.Controls.Add(Me.LabelControl3, 2, 0)
        Me.tlpCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCommit.Location = New System.Drawing.Point(1, 1)
        Me.tlpCommit.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpCommit.Name = "tlpCommit"
        Me.tlpCommit.RowCount = 1
        Me.tlpCommit.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpCommit.Size = New System.Drawing.Size(816, 28)
        Me.tlpCommit.TabIndex = 0
        '
        'btnXmlJobCommit
        '
        Me.btnXmlJobCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnXmlJobCommit.Location = New System.Drawing.Point(738, 2)
        Me.btnXmlJobCommit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnXmlJobCommit.Name = "btnXmlJobCommit"
        Me.btnXmlJobCommit.Size = New System.Drawing.Size(76, 24)
        Me.btnXmlJobCommit.TabIndex = 26
        Me.btnXmlJobCommit.Text = "Commit"
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(54, 22)
        Me.LabelControl1.TabIndex = 27
        Me.LabelControl1.Text = "XML Job:"
        '
        'lblXmlJob
        '
        Me.lblXmlJob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblXmlJob.Location = New System.Drawing.Point(63, 3)
        Me.lblXmlJob.Name = "lblXmlJob"
        Me.lblXmlJob.Size = New System.Drawing.Size(460, 22)
        Me.lblXmlJob.TabIndex = 28
        '
        'cmbVendor
        '
        Me.cmbVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbVendor.Location = New System.Drawing.Point(589, 4)
        Me.cmbVendor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbVendor.Size = New System.Drawing.Size(144, 20)
        Me.cmbVendor.TabIndex = 25
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(529, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(54, 22)
        Me.LabelControl3.TabIndex = 29
        Me.LabelControl3.Text = "Vendor:"
        '
        'tlpPaste
        '
        Me.tlpPaste.ColumnCount = 3
        Me.tlpPaste.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPaste.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.tlpPaste.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpPaste.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpPaste.Controls.Add(Me.LabelControl35, 0, 0)
        Me.tlpPaste.Controls.Add(Me.btnXmlJobPaste, 2, 0)
        Me.tlpPaste.Controls.Add(Me.lblRecordsCount, 1, 0)
        Me.tlpPaste.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpPaste.Location = New System.Drawing.Point(1, 31)
        Me.tlpPaste.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpPaste.Name = "tlpPaste"
        Me.tlpPaste.RowCount = 1
        Me.tlpPaste.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPaste.Size = New System.Drawing.Size(816, 28)
        Me.tlpPaste.TabIndex = 1
        '
        'LabelControl35
        '
        Me.LabelControl35.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl35.Appearance.Image = CType(resources.GetObject("LabelControl35.Appearance.Image"), System.Drawing.Image)
        Me.LabelControl35.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl35.Appearance.Options.UseForeColor = True
        Me.LabelControl35.Appearance.Options.UseImage = True
        Me.LabelControl35.Appearance.Options.UseImageAlign = True
        Me.LabelControl35.Appearance.Options.UseTextOptions = True
        Me.LabelControl35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl35.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl35.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl35.Name = "LabelControl35"
        Me.LabelControl35.Size = New System.Drawing.Size(580, 22)
        Me.LabelControl35.TabIndex = 28
        Me.LabelControl35.Text = "Paste Into Grid: <MO>Tab<ObjectName>Tab<ObjectConditionColumns>Tab<ParameterName>" &
    "Tab<TargetValue>"
        '
        'btnXmlJobPaste
        '
        Me.btnXmlJobPaste.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnXmlJobPaste.Location = New System.Drawing.Point(738, 2)
        Me.btnXmlJobPaste.Margin = New System.Windows.Forms.Padding(2)
        Me.btnXmlJobPaste.Name = "btnXmlJobPaste"
        Me.btnXmlJobPaste.Size = New System.Drawing.Size(76, 24)
        Me.btnXmlJobPaste.TabIndex = 27
        Me.btnXmlJobPaste.Text = "Paste"
        '
        'lblRecordsCount
        '
        Me.lblRecordsCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRecordsCount.Location = New System.Drawing.Point(589, 3)
        Me.lblRecordsCount.Name = "lblRecordsCount"
        Me.lblRecordsCount.Size = New System.Drawing.Size(144, 22)
        Me.lblRecordsCount.TabIndex = 29
        Me.lblRecordsCount.Text = "# Records:"
        '
        'gcXmlJobData
        '
        Me.gcXmlJobData.AllowDrop = True
        Me.gcXmlJobData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcXmlJobData.Location = New System.Drawing.Point(3, 63)
        Me.gcXmlJobData.MainView = Me.gvXmlJobData
        Me.gcXmlJobData.Name = "gcXmlJobData"
        Me.gcXmlJobData.Size = New System.Drawing.Size(812, 497)
        Me.gcXmlJobData.TabIndex = 22
        Me.gcXmlJobData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvXmlJobData})
        '
        'gvXmlJobData
        '
        Me.gvXmlJobData.ActiveFilterEnabled = False
        Me.gvXmlJobData.GridControl = Me.gcXmlJobData
        Me.gvXmlJobData.Name = "gvXmlJobData"
        Me.gvXmlJobData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXmlJobData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXmlJobData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlJobData.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlJobData.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlJobData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvXmlJobData.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append
        Me.gvXmlJobData.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvXmlJobData.OptionsSelection.MultiSelect = True
        Me.gvXmlJobData.OptionsView.ShowGroupPanel = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'dlgXmlJobInsert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 593)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("dlgXmlJobInsert.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(820, 625)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(820, 625)
        Me.Name = "dlgXmlJobInsert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Xml Job: Insert"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpCommit.ResumeLayout(False)
        Me.tlpCommit.PerformLayout()
        CType(Me.cmbVendor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpPaste.ResumeLayout(False)
        Me.tlpPaste.PerformLayout()
        CType(Me.gcXmlJobData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvXmlJobData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpCommit As TableLayoutPanel
    Friend WithEvents cmbVendor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnXmlJobCommit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblXmlJob As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpPaste As TableLayoutPanel
    Friend WithEvents btnXmlJobPaste As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl35 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblRecordsCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents gcXmlJobData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvXmlJobData As DevExpress.XtraGrid.Views.Grid.GridView
End Class
