<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportDrivetest
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportDrivetest))
        Me.IosTableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.IosTableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.rdoProjectNew = New System.Windows.Forms.RadioButton()
        Me.rdoProjectExist = New System.Windows.Forms.RadioButton()
        Me.gcProject = New DevExpress.XtraGrid.GridControl()
        Me.gvProject = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtProjectNew = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.IosTableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.rdoDriveTestExist = New System.Windows.Forms.RadioButton()
        Me.gcDrivetest = New DevExpress.XtraGrid.GridControl()
        Me.gvDrivetest = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.rdoDriveTestNew = New System.Windows.Forms.RadioButton()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.IosTableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.rdoDeviceNew = New System.Windows.Forms.RadioButton()
        Me.txtDeviceNew = New DevExpress.XtraEditors.TextEdit()
        Me.gcDevice = New DevExpress.XtraGrid.GridControl()
        Me.gvDevice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtDrivetestNew = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.IosTableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcFiles = New DevExpress.XtraGrid.GridControl()
        Me.gvFiles = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IosTableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFormat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.IosTableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.IosTableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.IosTableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnImport = New DevExpress.XtraEditors.SimpleButton()
        Me.StatusStripFileUpload = New System.Windows.Forms.StatusStrip()
        Me.tsStatusLabelStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsStatuslblFileName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsStatuslblFileSize = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.IosTableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.IosTableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.gcProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProjectNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.IosTableLayoutPanel5.SuspendLayout()
        CType(Me.gcDrivetest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDrivetest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        Me.IosTableLayoutPanel6.SuspendLayout()
        CType(Me.txtDeviceNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDrivetestNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.IosTableLayoutPanel8.SuspendLayout()
        CType(Me.gcFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.IosTableLayoutPanel9.SuspendLayout()
        CType(Me.cmbFormat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.IosTableLayoutPanel10.SuspendLayout()
        Me.IosTableLayoutPanel11.SuspendLayout()
        Me.IosTableLayoutPanel12.SuspendLayout()
        Me.StatusStripFileUpload.SuspendLayout()
        Me.SuspendLayout()
        '
        'IosTableLayoutPanel1
        '
        Me.IosTableLayoutPanel1.ColumnCount = 5
        Me.IosTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.IosTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.IosTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.IosTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.IosTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.IosTableLayoutPanel1.Controls.Add(Me.GroupControl1, 0, 0)
        Me.IosTableLayoutPanel1.Controls.Add(Me.GroupControl2, 2, 0)
        Me.IosTableLayoutPanel1.Controls.Add(Me.GroupControl3, 4, 0)
        Me.IosTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.IosTableLayoutPanel1.Name = "IosTableLayoutPanel1"
        Me.IosTableLayoutPanel1.RowCount = 1
        Me.IosTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel1.Size = New System.Drawing.Size(1000, 597)
        Me.IosTableLayoutPanel1.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.IosTableLayoutPanel2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(324, 591)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Step 1: Select Project"
        '
        'IosTableLayoutPanel2
        '
        Me.IosTableLayoutPanel2.ColumnCount = 1
        Me.IosTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.IosTableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.IosTableLayoutPanel2.Controls.Add(Me.gcProject, 0, 2)
        Me.IosTableLayoutPanel2.Controls.Add(Me.txtProjectNew, 0, 1)
        Me.IosTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel2.Location = New System.Drawing.Point(2, 20)
        Me.IosTableLayoutPanel2.Name = "IosTableLayoutPanel2"
        Me.IosTableLayoutPanel2.RowCount = 3
        Me.IosTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68.0!))
        Me.IosTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.IosTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel2.Size = New System.Drawing.Size(320, 569)
        Me.IosTableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.rdoProjectNew, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rdoProjectExist, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(314, 62)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'rdoProjectNew
        '
        Me.rdoProjectNew.AutoSize = True
        Me.rdoProjectNew.Checked = True
        Me.rdoProjectNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoProjectNew.Location = New System.Drawing.Point(3, 3)
        Me.rdoProjectNew.Name = "rdoProjectNew"
        Me.rdoProjectNew.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rdoProjectNew.Size = New System.Drawing.Size(308, 25)
        Me.rdoProjectNew.TabIndex = 0
        Me.rdoProjectNew.TabStop = True
        Me.rdoProjectNew.Text = "New Project"
        Me.rdoProjectNew.UseVisualStyleBackColor = True
        '
        'rdoProjectExist
        '
        Me.rdoProjectExist.AutoSize = True
        Me.rdoProjectExist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoProjectExist.Location = New System.Drawing.Point(3, 34)
        Me.rdoProjectExist.Name = "rdoProjectExist"
        Me.rdoProjectExist.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rdoProjectExist.Size = New System.Drawing.Size(308, 25)
        Me.rdoProjectExist.TabIndex = 1
        Me.rdoProjectExist.Text = "Choose Existing Project"
        Me.rdoProjectExist.UseVisualStyleBackColor = True
        '
        'gcProject
        '
        Me.gcProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcProject.Location = New System.Drawing.Point(3, 99)
        Me.gcProject.MainView = Me.gvProject
        Me.gcProject.Name = "gcProject"
        Me.gcProject.Size = New System.Drawing.Size(314, 467)
        Me.gcProject.TabIndex = 5
        Me.gcProject.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvProject})
        '
        'gvProject
        '
        Me.gvProject.GridControl = Me.gcProject
        Me.gvProject.Name = "gvProject"
        Me.gvProject.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvProject.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvProject.OptionsBehavior.Editable = False
        Me.gvProject.OptionsBehavior.ReadOnly = True
        Me.gvProject.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvProject.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvProject.OptionsView.ColumnAutoWidth = False
        Me.gvProject.OptionsView.ShowGroupPanel = False
        '
        'txtProjectNew
        '
        Me.txtProjectNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtProjectNew.Location = New System.Drawing.Point(3, 71)
        Me.txtProjectNew.Name = "txtProjectNew"
        Me.txtProjectNew.Properties.NullValuePrompt = "Enter new project"
        Me.txtProjectNew.Properties.NullValuePromptShowForEmptyValue = True
        Me.txtProjectNew.Properties.ShowNullValuePromptWhenFocused = True
        Me.txtProjectNew.Size = New System.Drawing.Size(314, 20)
        Me.txtProjectNew.TabIndex = 6
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.IosTableLayoutPanel5)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(337, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(324, 591)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Step 2: Select Drivetest"
        '
        'IosTableLayoutPanel5
        '
        Me.IosTableLayoutPanel5.ColumnCount = 1
        Me.IosTableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel5.Controls.Add(Me.rdoDriveTestExist, 0, 1)
        Me.IosTableLayoutPanel5.Controls.Add(Me.gcDrivetest, 0, 3)
        Me.IosTableLayoutPanel5.Controls.Add(Me.rdoDriveTestNew, 0, 0)
        Me.IosTableLayoutPanel5.Controls.Add(Me.GroupControl4, 0, 4)
        Me.IosTableLayoutPanel5.Controls.Add(Me.gcDevice, 0, 5)
        Me.IosTableLayoutPanel5.Controls.Add(Me.txtDrivetestNew, 0, 2)
        Me.IosTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel5.Location = New System.Drawing.Point(2, 20)
        Me.IosTableLayoutPanel5.Name = "IosTableLayoutPanel5"
        Me.IosTableLayoutPanel5.RowCount = 6
        Me.IosTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.IosTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.IosTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.IosTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.IosTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.IosTableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.IosTableLayoutPanel5.Size = New System.Drawing.Size(320, 569)
        Me.IosTableLayoutPanel5.TabIndex = 1
        '
        'rdoDriveTestExist
        '
        Me.rdoDriveTestExist.AutoSize = True
        Me.rdoDriveTestExist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoDriveTestExist.Location = New System.Drawing.Point(3, 33)
        Me.rdoDriveTestExist.Name = "rdoDriveTestExist"
        Me.rdoDriveTestExist.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rdoDriveTestExist.Size = New System.Drawing.Size(314, 24)
        Me.rdoDriveTestExist.TabIndex = 1
        Me.rdoDriveTestExist.TabStop = True
        Me.rdoDriveTestExist.Text = "Choose Existing Drivetest"
        Me.rdoDriveTestExist.UseVisualStyleBackColor = True
        '
        'gcDrivetest
        '
        Me.gcDrivetest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDrivetest.Location = New System.Drawing.Point(3, 91)
        Me.gcDrivetest.MainView = Me.gvDrivetest
        Me.gcDrivetest.Name = "gcDrivetest"
        Me.gcDrivetest.Size = New System.Drawing.Size(314, 204)
        Me.gcDrivetest.TabIndex = 2
        Me.gcDrivetest.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDrivetest})
        '
        'gvDrivetest
        '
        Me.gvDrivetest.GridControl = Me.gcDrivetest
        Me.gvDrivetest.Name = "gvDrivetest"
        Me.gvDrivetest.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDrivetest.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDrivetest.OptionsBehavior.Editable = False
        Me.gvDrivetest.OptionsBehavior.ReadOnly = True
        Me.gvDrivetest.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDrivetest.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvDrivetest.OptionsView.ColumnAutoWidth = False
        Me.gvDrivetest.OptionsView.ShowGroupPanel = False
        '
        'rdoDriveTestNew
        '
        Me.rdoDriveTestNew.AutoSize = True
        Me.rdoDriveTestNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoDriveTestNew.Location = New System.Drawing.Point(3, 3)
        Me.rdoDriveTestNew.Name = "rdoDriveTestNew"
        Me.rdoDriveTestNew.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rdoDriveTestNew.Size = New System.Drawing.Size(314, 24)
        Me.rdoDriveTestNew.TabIndex = 0
        Me.rdoDriveTestNew.TabStop = True
        Me.rdoDriveTestNew.Text = "New Drivetest"
        Me.rdoDriveTestNew.UseVisualStyleBackColor = True
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.IosTableLayoutPanel6)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(3, 301)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(314, 54)
        Me.GroupControl4.TabIndex = 4
        Me.GroupControl4.Text = "Device Measurements in DriveTest"
        '
        'IosTableLayoutPanel6
        '
        Me.IosTableLayoutPanel6.ColumnCount = 2
        Me.IosTableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 216.0!))
        Me.IosTableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.IosTableLayoutPanel6.Controls.Add(Me.rdoDeviceNew, 0, 0)
        Me.IosTableLayoutPanel6.Controls.Add(Me.txtDeviceNew, 1, 0)
        Me.IosTableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel6.Location = New System.Drawing.Point(2, 20)
        Me.IosTableLayoutPanel6.Name = "IosTableLayoutPanel6"
        Me.IosTableLayoutPanel6.RowCount = 2
        Me.IosTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.IosTableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.IosTableLayoutPanel6.Size = New System.Drawing.Size(310, 32)
        Me.IosTableLayoutPanel6.TabIndex = 0
        '
        'rdoDeviceNew
        '
        Me.rdoDeviceNew.AutoSize = True
        Me.rdoDeviceNew.Checked = True
        Me.rdoDeviceNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdoDeviceNew.Location = New System.Drawing.Point(3, 3)
        Me.rdoDeviceNew.Name = "rdoDeviceNew"
        Me.rdoDeviceNew.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.rdoDeviceNew.Size = New System.Drawing.Size(88, 25)
        Me.rdoDeviceNew.TabIndex = 0
        Me.rdoDeviceNew.TabStop = True
        Me.rdoDeviceNew.Text = "New Device"
        Me.rdoDeviceNew.UseVisualStyleBackColor = True
        '
        'txtDeviceNew
        '
        Me.txtDeviceNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDeviceNew.Location = New System.Drawing.Point(99, 5)
        Me.txtDeviceNew.Margin = New System.Windows.Forms.Padding(5)
        Me.txtDeviceNew.Name = "txtDeviceNew"
        Me.txtDeviceNew.Properties.NullValuePrompt = "Enter new device"
        Me.txtDeviceNew.Properties.NullValuePromptShowForEmptyValue = True
        Me.txtDeviceNew.Properties.ShowNullValuePromptWhenFocused = True
        Me.txtDeviceNew.Size = New System.Drawing.Size(206, 20)
        Me.txtDeviceNew.TabIndex = 1
        '
        'gcDevice
        '
        Me.gcDevice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDevice.Location = New System.Drawing.Point(3, 361)
        Me.gcDevice.MainView = Me.gvDevice
        Me.gcDevice.Name = "gcDevice"
        Me.gcDevice.Size = New System.Drawing.Size(314, 205)
        Me.gcDevice.TabIndex = 3
        Me.gcDevice.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDevice})
        '
        'gvDevice
        '
        Me.gvDevice.GridControl = Me.gcDevice
        Me.gvDevice.Name = "gvDevice"
        Me.gvDevice.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDevice.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDevice.OptionsBehavior.Editable = False
        Me.gvDevice.OptionsBehavior.ReadOnly = True
        Me.gvDevice.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDevice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvDevice.OptionsView.ColumnAutoWidth = False
        Me.gvDevice.OptionsView.ShowGroupPanel = False
        '
        'txtDrivetestNew
        '
        Me.txtDrivetestNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDrivetestNew.Location = New System.Drawing.Point(5, 65)
        Me.txtDrivetestNew.Margin = New System.Windows.Forms.Padding(5)
        Me.txtDrivetestNew.Name = "txtDrivetestNew"
        Me.txtDrivetestNew.Properties.NullValuePrompt = "Enter new drivetest"
        Me.txtDrivetestNew.Properties.NullValuePromptShowForEmptyValue = True
        Me.txtDrivetestNew.Properties.ShowNullValuePromptWhenFocused = True
        Me.txtDrivetestNew.Size = New System.Drawing.Size(310, 20)
        Me.txtDrivetestNew.TabIndex = 1
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.IosTableLayoutPanel8)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(671, 3)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(326, 591)
        Me.GroupControl3.TabIndex = 2
        Me.GroupControl3.Text = "Step 3: Select Files to add to selected Device"
        '
        'IosTableLayoutPanel8
        '
        Me.IosTableLayoutPanel8.ColumnCount = 1
        Me.IosTableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.IosTableLayoutPanel8.Controls.Add(Me.gcFiles, 0, 2)
        Me.IosTableLayoutPanel8.Controls.Add(Me.IosTableLayoutPanel9, 0, 0)
        Me.IosTableLayoutPanel8.Controls.Add(Me.IosTableLayoutPanel10, 0, 1)
        Me.IosTableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel8.Location = New System.Drawing.Point(2, 20)
        Me.IosTableLayoutPanel8.Name = "IosTableLayoutPanel8"
        Me.IosTableLayoutPanel8.RowCount = 3
        Me.IosTableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63.0!))
        Me.IosTableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.IosTableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel8.Size = New System.Drawing.Size(322, 569)
        Me.IosTableLayoutPanel8.TabIndex = 0
        '
        'gcFiles
        '
        Me.gcFiles.AllowDrop = True
        Me.gcFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcFiles.Location = New System.Drawing.Point(3, 102)
        Me.gcFiles.MainView = Me.gvFiles
        Me.gcFiles.Name = "gcFiles"
        Me.gcFiles.Size = New System.Drawing.Size(316, 464)
        Me.gcFiles.TabIndex = 4
        Me.gcFiles.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvFiles})
        '
        'gvFiles
        '
        Me.gvFiles.GridControl = Me.gcFiles
        Me.gvFiles.Name = "gvFiles"
        Me.gvFiles.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvFiles.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvFiles.OptionsBehavior.Editable = False
        Me.gvFiles.OptionsBehavior.ReadOnly = True
        Me.gvFiles.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvFiles.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvFiles.OptionsView.ColumnAutoWidth = False
        Me.gvFiles.OptionsView.ShowGroupPanel = False
        '
        'IosTableLayoutPanel9
        '
        Me.IosTableLayoutPanel9.ColumnCount = 2
        Me.IosTableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.IosTableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.IosTableLayoutPanel9.Controls.Add(Me.LabelControl1, 0, 0)
        Me.IosTableLayoutPanel9.Controls.Add(Me.cmbFormat, 1, 0)
        Me.IosTableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel9.Location = New System.Drawing.Point(3, 3)
        Me.IosTableLayoutPanel9.Name = "IosTableLayoutPanel9"
        Me.IosTableLayoutPanel9.RowCount = 3
        Me.IosTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.IosTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.IosTableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel9.Size = New System.Drawing.Size(316, 57)
        Me.IosTableLayoutPanel9.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(101, 21)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Format"
        '
        'cmbFormat
        '
        Me.cmbFormat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbFormat.EditValue = "Select Format"
        Me.cmbFormat.Location = New System.Drawing.Point(110, 3)
        Me.cmbFormat.Name = "cmbFormat"
        Me.cmbFormat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFormat.Properties.Items.AddRange(New Object() {"Select Format", "Nemo"})
        Me.cmbFormat.Size = New System.Drawing.Size(203, 20)
        Me.cmbFormat.TabIndex = 1
        '
        'IosTableLayoutPanel10
        '
        Me.IosTableLayoutPanel10.ColumnCount = 2
        Me.IosTableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.IosTableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.IosTableLayoutPanel10.Controls.Add(Me.LabelControl2, 0, 0)
        Me.IosTableLayoutPanel10.Controls.Add(Me.btnBrowse, 1, 0)
        Me.IosTableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel10.Location = New System.Drawing.Point(3, 66)
        Me.IosTableLayoutPanel10.Name = "IosTableLayoutPanel10"
        Me.IosTableLayoutPanel10.RowCount = 2
        Me.IosTableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.IosTableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel10.Size = New System.Drawing.Size(316, 30)
        Me.IosTableLayoutPanel10.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(101, 25)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Drag - Drop - Or"
        '
        'btnBrowse
        '
        Me.btnBrowse.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnBrowse.Location = New System.Drawing.Point(110, 3)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(97, 25)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "Browse"
        '
        'IosTableLayoutPanel11
        '
        Me.IosTableLayoutPanel11.ColumnCount = 1
        Me.IosTableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.IosTableLayoutPanel11.Controls.Add(Me.IosTableLayoutPanel1, 0, 0)
        Me.IosTableLayoutPanel11.Controls.Add(Me.IosTableLayoutPanel12, 0, 1)
        Me.IosTableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel11.Location = New System.Drawing.Point(0, 0)
        Me.IosTableLayoutPanel11.Name = "IosTableLayoutPanel11"
        Me.IosTableLayoutPanel11.RowCount = 2
        Me.IosTableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.IosTableLayoutPanel11.Size = New System.Drawing.Size(1006, 640)
        Me.IosTableLayoutPanel11.TabIndex = 1
        '
        'IosTableLayoutPanel12
        '
        Me.IosTableLayoutPanel12.ColumnCount = 3
        Me.IosTableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7.0!))
        Me.IosTableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133.0!))
        Me.IosTableLayoutPanel12.Controls.Add(Me.btnImport, 2, 0)
        Me.IosTableLayoutPanel12.Controls.Add(Me.StatusStripFileUpload, 1, 0)
        Me.IosTableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel12.Location = New System.Drawing.Point(3, 606)
        Me.IosTableLayoutPanel12.Name = "IosTableLayoutPanel12"
        Me.IosTableLayoutPanel12.RowCount = 2
        Me.IosTableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.IosTableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel12.Size = New System.Drawing.Size(1000, 31)
        Me.IosTableLayoutPanel12.TabIndex = 1
        '
        'btnImport
        '
        Me.btnImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnImport.Location = New System.Drawing.Point(870, 3)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(127, 25)
        Me.btnImport.TabIndex = 0
        Me.btnImport.Text = "Import"
        '
        'StatusStripFileUpload
        '
        Me.StatusStripFileUpload.BackColor = System.Drawing.Color.Transparent
        Me.StatusStripFileUpload.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatusStripFileUpload.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsStatusLabelStatus, Me.tsStatuslblFileName, Me.tsStatuslblFileSize, Me.tsProgressBar})
        Me.StatusStripFileUpload.Location = New System.Drawing.Point(7, 0)
        Me.StatusStripFileUpload.Name = "StatusStripFileUpload"
        Me.StatusStripFileUpload.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStripFileUpload.Size = New System.Drawing.Size(860, 31)
        Me.StatusStripFileUpload.TabIndex = 12
        '
        'tsStatusLabelStatus
        '
        Me.tsStatusLabelStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsStatusLabelStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tsStatusLabelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsStatusLabelStatus.Name = "tsStatusLabelStatus"
        Me.tsStatusLabelStatus.Size = New System.Drawing.Size(168, 26)
        Me.tsStatusLabelStatus.Spring = True
        Me.tsStatusLabelStatus.Text = "Status : "
        Me.tsStatusLabelStatus.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'tsStatuslblFileName
        '
        Me.tsStatuslblFileName.BackColor = System.Drawing.Color.Transparent
        Me.tsStatuslblFileName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsStatuslblFileName.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tsStatuslblFileName.Name = "tsStatuslblFileName"
        Me.tsStatuslblFileName.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.tsStatuslblFileName.Size = New System.Drawing.Size(168, 26)
        Me.tsStatuslblFileName.Spring = True
        Me.tsStatuslblFileName.Text = "File :"
        Me.tsStatuslblFileName.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'tsStatuslblFileSize
        '
        Me.tsStatuslblFileSize.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsStatuslblFileSize.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tsStatuslblFileSize.Name = "tsStatuslblFileSize"
        Me.tsStatuslblFileSize.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tsStatuslblFileSize.Size = New System.Drawing.Size(168, 26)
        Me.tsStatuslblFileSize.Spring = True
        Me.tsStatuslblFileSize.Text = " Size :"
        Me.tsStatuslblFileSize.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'tsProgressBar
        '
        Me.tsProgressBar.Name = "tsProgressBar"
        Me.tsProgressBar.Size = New System.Drawing.Size(333, 25)
        '
        'Timer1
        '
        '
        'frmImportDrivetest
        '
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 640)
        Me.Controls.Add(Me.IosTableLayoutPanel11)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Seven"
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportDrivetest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import DriveTest"
        Me.IosTableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.IosTableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.gcProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProjectNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.IosTableLayoutPanel5.ResumeLayout(False)
        Me.IosTableLayoutPanel5.PerformLayout()
        CType(Me.gcDrivetest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDrivetest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.IosTableLayoutPanel6.ResumeLayout(False)
        Me.IosTableLayoutPanel6.PerformLayout()
        CType(Me.txtDeviceNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcDevice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDevice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDrivetestNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.IosTableLayoutPanel8.ResumeLayout(False)
        CType(Me.gcFiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.IosTableLayoutPanel9.ResumeLayout(False)
        Me.IosTableLayoutPanel9.PerformLayout()
        CType(Me.cmbFormat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.IosTableLayoutPanel10.ResumeLayout(False)
        Me.IosTableLayoutPanel10.PerformLayout()
        Me.IosTableLayoutPanel11.ResumeLayout(False)
        Me.IosTableLayoutPanel12.ResumeLayout(False)
        Me.IosTableLayoutPanel12.PerformLayout()
        Me.StatusStripFileUpload.ResumeLayout(False)
        Me.StatusStripFileUpload.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IosTableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents IosTableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents rdoDriveTestNew As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDriveTestExist As System.Windows.Forms.RadioButton
    Friend WithEvents txtDrivetestNew As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IosTableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents IosTableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents rdoDeviceNew As System.Windows.Forms.RadioButton
    Friend WithEvents txtDeviceNew As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IosTableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents IosTableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbFormat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents IosTableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IosTableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents IosTableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents btnImport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents StatusStripFileUpload As System.Windows.Forms.StatusStrip
    Friend WithEvents tsStatusLabelStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsStatuslblFileName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsStatuslblFileSize As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents gcDrivetest As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDrivetest As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcDevice As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDevice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcFiles As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvFiles As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
	Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
	Friend WithEvents rdoProjectNew As RadioButton
	Friend WithEvents rdoProjectExist As RadioButton
	Friend WithEvents gcProject As DevExpress.XtraGrid.GridControl
	Friend WithEvents gvProject As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtProjectNew As DevExpress.XtraEditors.TextEdit
End Class
