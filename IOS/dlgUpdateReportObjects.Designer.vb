<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgUpdateReportObjects
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgUpdateReportObjects))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpTop = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblSelectedReportItem = New DevExpress.XtraEditors.LabelControl()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.grpCtrlMain = New DevExpress.XtraEditors.GroupControl()
        Me.tlpUpdateSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkPeriodResolution = New System.Windows.Forms.CheckBox()
        Me.cmbResolution = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbPredefTimeStats = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.dtEditStartTime = New DevExpress.XtraEditors.DateEdit()
        Me.dtEditEndTime = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkObjects = New System.Windows.Forms.CheckBox()
        Me.gcReportObjects = New DevExpress.XtraGrid.GridControl()
        Me.gvReportObjects = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpMain.SuspendLayout()
        Me.tlpTop.SuspendLayout()
        Me.tlpBottom.SuspendLayout()
        CType(Me.grpCtrlMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCtrlMain.SuspendLayout()
        Me.tlpUpdateSettings.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPredefTimeStats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEditStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEditStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEditEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEditEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.gcReportObjects, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReportObjects, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpTop, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 3)
        Me.tlpMain.Controls.Add(Me.grpCtrlMain, 0, 2)
        Me.tlpMain.Controls.Add(Me.LabelControl6, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 4
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlpMain.Size = New System.Drawing.Size(698, 488)
        Me.tlpMain.TabIndex = 0
        '
        'tlpTop
        '
        Me.tlpTop.ColumnCount = 2
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTop.Controls.Add(Me.LabelControl1, 0, 0)
        Me.tlpTop.Controls.Add(Me.lblSelectedReportItem, 1, 0)
        Me.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTop.Location = New System.Drawing.Point(1, 1)
        Me.tlpTop.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpTop.Name = "tlpTop"
        Me.tlpTop.RowCount = 1
        Me.tlpTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTop.Size = New System.Drawing.Size(696, 28)
        Me.tlpTop.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(174, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Selected Report Item:"
        '
        'lblSelectedReportItem
        '
        Me.lblSelectedReportItem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSelectedReportItem.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSelectedReportItem.Appearance.Options.UseFont = True
        Me.lblSelectedReportItem.Appearance.Options.UseForeColor = True
        Me.lblSelectedReportItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSelectedReportItem.Location = New System.Drawing.Point(183, 3)
        Me.lblSelectedReportItem.Name = "lblSelectedReportItem"
        Me.lblSelectedReportItem.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblSelectedReportItem.Size = New System.Drawing.Size(510, 22)
        Me.lblSelectedReportItem.TabIndex = 1
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnUpdate, 1, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(1, 457)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(696, 30)
        Me.tlpBottom.TabIndex = 1
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(530, 24)
        Me.lblMessage.TabIndex = 13
        '
        'btnUpdate
        '
        Me.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUpdate.Location = New System.Drawing.Point(538, 2)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(156, 26)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "Update"
        '
        'grpCtrlMain
        '
        Me.grpCtrlMain.Controls.Add(Me.tlpUpdateSettings)
        Me.grpCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCtrlMain.Location = New System.Drawing.Point(3, 107)
        Me.grpCtrlMain.Name = "grpCtrlMain"
        Me.grpCtrlMain.Size = New System.Drawing.Size(692, 346)
        Me.grpCtrlMain.TabIndex = 2
        Me.grpCtrlMain.Text = "Update Settings"
        '
        'tlpUpdateSettings
        '
        Me.tlpUpdateSettings.ColumnCount = 1
        Me.tlpUpdateSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpUpdateSettings.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.tlpUpdateSettings.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.tlpUpdateSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpUpdateSettings.Location = New System.Drawing.Point(2, 23)
        Me.tlpUpdateSettings.Name = "tlpUpdateSettings"
        Me.tlpUpdateSettings.RowCount = 2
        Me.tlpUpdateSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.tlpUpdateSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpUpdateSettings.Size = New System.Drawing.Size(688, 321)
        Me.tlpUpdateSettings.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.chkPeriodResolution, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbResolution, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbPredefTimeStats, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl3, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.dtEditStartTime, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.dtEditEndTime, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl5, 2, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(682, 84)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'chkPeriodResolution
        '
        Me.chkPeriodResolution.AutoSize = True
        Me.chkPeriodResolution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkPeriodResolution.Location = New System.Drawing.Point(3, 3)
        Me.chkPeriodResolution.Name = "chkPeriodResolution"
        Me.chkPeriodResolution.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.chkPeriodResolution.Size = New System.Drawing.Size(164, 22)
        Me.chkPeriodResolution.TabIndex = 0
        Me.chkPeriodResolution.Text = "Update Period + Interval"
        Me.chkPeriodResolution.UseVisualStyleBackColor = True
        '
        'cmbResolution
        '
        Me.cmbResolution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbResolution.Location = New System.Drawing.Point(173, 33)
        Me.cmbResolution.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbResolution.Name = "cmbResolution"
        Me.cmbResolution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbResolution.Size = New System.Drawing.Size(176, 20)
        Me.cmbResolution.TabIndex = 5
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Select Interval"
        '
        'cmbPredefTimeStats
        '
        Me.cmbPredefTimeStats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbPredefTimeStats.Location = New System.Drawing.Point(525, 33)
        Me.cmbPredefTimeStats.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.cmbPredefTimeStats.Name = "cmbPredefTimeStats"
        Me.cmbPredefTimeStats.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPredefTimeStats.Size = New System.Drawing.Size(154, 20)
        Me.cmbPredefTimeStats.TabIndex = 4
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(355, 31)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Select Predefined Time"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl4.TabIndex = 2
        Me.LabelControl4.Text = "Select Start Time"
        '
        'dtEditStartTime
        '
        Me.dtEditStartTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtEditStartTime.EditValue = Nothing
        Me.dtEditStartTime.Location = New System.Drawing.Point(173, 61)
        Me.dtEditStartTime.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.dtEditStartTime.Name = "dtEditStartTime"
        Me.dtEditStartTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtEditStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtEditStartTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.dtEditStartTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dtEditStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dtEditStartTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dtEditStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dtEditStartTime.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.dtEditStartTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtEditStartTime.Size = New System.Drawing.Size(176, 20)
        Me.dtEditStartTime.TabIndex = 6
        '
        'dtEditEndTime
        '
        Me.dtEditEndTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtEditEndTime.EditValue = Nothing
        Me.dtEditEndTime.Location = New System.Drawing.Point(525, 61)
        Me.dtEditEndTime.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.dtEditEndTime.Name = "dtEditEndTime"
        Me.dtEditEndTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtEditEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtEditEndTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.dtEditEndTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dtEditEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dtEditEndTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dtEditEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dtEditEndTime.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.dtEditEndTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtEditEndTime.Size = New System.Drawing.Size(154, 20)
        Me.dtEditEndTime.TabIndex = 7
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(355, 59)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl5.TabIndex = 8
        Me.LabelControl5.Text = "Select End Time"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 182.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.chkObjects, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.gcReportObjects, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.btnRefresh, 3, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 93)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(682, 225)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'chkObjects
        '
        Me.chkObjects.AutoSize = True
        Me.chkObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkObjects.Location = New System.Drawing.Point(3, 3)
        Me.chkObjects.Name = "chkObjects"
        Me.chkObjects.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.chkObjects.Size = New System.Drawing.Size(164, 24)
        Me.chkObjects.TabIndex = 0
        Me.chkObjects.Text = "Update Objects"
        Me.chkObjects.UseVisualStyleBackColor = True
        '
        'gcReportObjects
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.gcReportObjects, 4)
        Me.gcReportObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcReportObjects.Location = New System.Drawing.Point(3, 33)
        Me.gcReportObjects.MainView = Me.gvReportObjects
        Me.gcReportObjects.Name = "gcReportObjects"
        Me.gcReportObjects.Size = New System.Drawing.Size(676, 189)
        Me.gcReportObjects.TabIndex = 17
        Me.gcReportObjects.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReportObjects})
        '
        'gvReportObjects
        '
        Me.gvReportObjects.GridControl = Me.gcReportObjects
        Me.gvReportObjects.Name = "gvReportObjects"
        Me.gvReportObjects.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportObjects.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvReportObjects.OptionsBehavior.Editable = False
        Me.gvReportObjects.OptionsBehavior.ReadOnly = True
        Me.gvReportObjects.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportObjects.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportObjects.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvReportObjects.OptionsSelection.MultiSelect = True
        Me.gvReportObjects.OptionsView.ColumnAutoWidth = False
        Me.gvReportObjects.OptionsView.ShowGroupPanel = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefresh.Location = New System.Drawing.Point(524, 3)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(156, 25)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "Refresh"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl6.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 33)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(692, 68)
        Me.LabelControl6.TabIndex = 3
        Me.LabelControl6.Text = resources.GetString("LabelControl6.Text")
        '
        'dlgUpdateReportObjects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 488)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Icon = CType(resources.GetObject("dlgUpdateReportObjects.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(700, 520)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(700, 520)
        Me.Name = "dlgUpdateReportObjects"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report - Update Objects"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpTop.ResumeLayout(False)
        Me.tlpTop.PerformLayout()
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        CType(Me.grpCtrlMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCtrlMain.ResumeLayout(False)
        Me.tlpUpdateSettings.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.cmbResolution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPredefTimeStats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEditStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEditStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEditEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEditEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.gcReportObjects, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReportObjects, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpTop As TableLayoutPanel
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents grpCtrlMain As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tlpUpdateSettings As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents chkPeriodResolution As CheckBox
    Friend WithEvents chkObjects As CheckBox
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSelectedReportItem As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbPredefTimeStats As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbResolution As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents dtEditStartTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtEditEndTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcReportObjects As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReportObjects As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
End Class
