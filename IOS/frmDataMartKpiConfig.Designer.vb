<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDatamartKpiConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDatamartKpiConfig))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.tlpKpiDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.gcKPIDetails = New DevExpress.XtraEditors.GroupControl()
        Me.tlpKpiDetailsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblKPIConfigObjectType = New DevExpress.XtraEditors.LabelControl()
        Me.tlpObjectType = New System.Windows.Forms.TableLayoutPanel()
        Me.lblObjType = New DevExpress.XtraEditors.LabelControl()
        Me.lblKPICreator = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.tlpKpiShare = New System.Windows.Forms.TableLayoutPanel()
        Me.rbKPIConfigPublic = New System.Windows.Forms.RadioButton()
        Me.rbKPIConfigPrivate = New System.Windows.Forms.RadioButton()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.lblKPIConfigStatus = New DevExpress.XtraEditors.LabelControl()
        Me.tlpKpiButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCommitKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTestKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.tlpKPI = New System.Windows.Forms.TableLayoutPanel()
        Me.vgb_KPIDesc = New DevExpress.XtraEditors.GroupControl()
        Me.txtKPIDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.tlpKpiName = New System.Windows.Forms.TableLayoutPanel()
        Me.txtValueIfNull = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl34 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl33 = New DevExpress.XtraEditors.LabelControl()
        Me.txtKPIName = New DevExpress.XtraEditors.TextEdit()
        Me.vgp_KPIList = New DevExpress.XtraEditors.GroupControl()
        Me.txtKPIFormula = New DevExpress.XtraEditors.MemoEdit()
        Me.tlpRight = New System.Windows.Forms.TableLayoutPanel()
        Me.vgb_AggrFunc = New DevExpress.XtraEditors.GroupControl()
        Me.lstAggregateFunction = New DevExpress.XtraEditors.ListBoxControl()
        Me.vgb_OperatorList = New DevExpress.XtraEditors.GroupControl()
        Me.lstOperators = New DevExpress.XtraEditors.ListBoxControl()
        Me.tlpLeftInfo = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl36 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl37 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl38 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl45 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl48 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl61 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl62 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl63 = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.tlpKpiDetails.SuspendLayout()
        CType(Me.gcKPIDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcKPIDetails.SuspendLayout()
        Me.tlpKpiDetailsTop.SuspendLayout()
        Me.tlpObjectType.SuspendLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        Me.tlpKpiShare.SuspendLayout()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        Me.tlpKpiButtons.SuspendLayout()
        Me.tlpKPI.SuspendLayout()
        CType(Me.vgb_KPIDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vgb_KPIDesc.SuspendLayout()
        CType(Me.txtKPIDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpKpiName.SuspendLayout()
        CType(Me.txtValueIfNull.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKPIName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vgp_KPIList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vgp_KPIList.SuspendLayout()
        CType(Me.txtKPIFormula.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpRight.SuspendLayout()
        CType(Me.vgb_AggrFunc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vgb_AggrFunc.SuspendLayout()
        CType(Me.lstAggregateFunction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vgb_OperatorList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vgb_OperatorList.SuspendLayout()
        CType(Me.lstOperators, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpLeftInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.BackColor = System.Drawing.Color.Transparent
        Me.tlpMain.ColumnCount = 4
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 265.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127.0!))
        Me.tlpMain.Controls.Add(Me.lblMessage, 0, 1)
        Me.tlpMain.Controls.Add(Me.tlpKpiDetails, 1, 0)
        Me.tlpMain.Controls.Add(Me.tlpKPI, 2, 0)
        Me.tlpMain.Controls.Add(Me.tlpRight, 3, 0)
        Me.tlpMain.Controls.Add(Me.tlpLeftInfo, 0, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.Size = New System.Drawing.Size(1060, 408)
        Me.tlpMain.TabIndex = 1
        '
        'lblMessage
        '
        Me.tlpMain.SetColumnSpan(Me.lblMessage, 4)
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 381)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(1054, 24)
        Me.lblMessage.TabIndex = 4
        '
        'tlpKpiDetails
        '
        Me.tlpKpiDetails.ColumnCount = 1
        Me.tlpKpiDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiDetails.Controls.Add(Me.gcKPIDetails, 0, 0)
        Me.tlpKpiDetails.Controls.Add(Me.GroupControl7, 0, 1)
        Me.tlpKpiDetails.Controls.Add(Me.GroupControl8, 0, 2)
        Me.tlpKpiDetails.Controls.Add(Me.tlpKpiButtons, 0, 3)
        Me.tlpKpiDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKpiDetails.Location = New System.Drawing.Point(265, 0)
        Me.tlpKpiDetails.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpKpiDetails.Name = "tlpKpiDetails"
        Me.tlpKpiDetails.RowCount = 4
        Me.tlpKpiDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpKpiDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.tlpKpiDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpKpiDetails.Size = New System.Drawing.Size(150, 378)
        Me.tlpKpiDetails.TabIndex = 0
        '
        'gcKPIDetails
        '
        Me.gcKPIDetails.Controls.Add(Me.tlpKpiDetailsTop)
        Me.gcKPIDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKPIDetails.Location = New System.Drawing.Point(3, 3)
        Me.gcKPIDetails.Name = "gcKPIDetails"
        Me.gcKPIDetails.Size = New System.Drawing.Size(144, 74)
        Me.gcKPIDetails.TabIndex = 0
        Me.gcKPIDetails.Text = "KPI Details"
        '
        'tlpKpiDetailsTop
        '
        Me.tlpKpiDetailsTop.ColumnCount = 1
        Me.tlpKpiDetailsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKpiDetailsTop.Controls.Add(Me.lblKPIConfigObjectType, 0, 1)
        Me.tlpKpiDetailsTop.Controls.Add(Me.tlpObjectType, 0, 0)
        Me.tlpKpiDetailsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKpiDetailsTop.Location = New System.Drawing.Point(2, 23)
        Me.tlpKpiDetailsTop.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpKpiDetailsTop.Name = "tlpKpiDetailsTop"
        Me.tlpKpiDetailsTop.RowCount = 2
        Me.tlpKpiDetailsTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKpiDetailsTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKpiDetailsTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpKpiDetailsTop.Size = New System.Drawing.Size(140, 49)
        Me.tlpKpiDetailsTop.TabIndex = 0
        '
        'lblKPIConfigObjectType
        '
        Me.lblKPIConfigObjectType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKPIConfigObjectType.Location = New System.Drawing.Point(3, 27)
        Me.lblKPIConfigObjectType.Name = "lblKPIConfigObjectType"
        Me.lblKPIConfigObjectType.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblKPIConfigObjectType.Size = New System.Drawing.Size(134, 19)
        Me.lblKPIConfigObjectType.TabIndex = 1
        '
        'tlpObjectType
        '
        Me.tlpObjectType.ColumnCount = 2
        Me.tlpObjectType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.tlpObjectType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.tlpObjectType.Controls.Add(Me.lblObjType, 0, 0)
        Me.tlpObjectType.Controls.Add(Me.lblKPICreator, 1, 0)
        Me.tlpObjectType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpObjectType.Location = New System.Drawing.Point(1, 1)
        Me.tlpObjectType.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpObjectType.Name = "tlpObjectType"
        Me.tlpObjectType.RowCount = 1
        Me.tlpObjectType.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpObjectType.Size = New System.Drawing.Size(138, 22)
        Me.tlpObjectType.TabIndex = 2
        '
        'lblObjType
        '
        Me.lblObjType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblObjType.Location = New System.Drawing.Point(3, 3)
        Me.lblObjType.Name = "lblObjType"
        Me.lblObjType.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblObjType.Size = New System.Drawing.Size(125, 16)
        Me.lblObjType.TabIndex = 0
        Me.lblObjType.Text = "Object Type"
        '
        'lblKPICreator
        '
        Me.lblKPICreator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKPICreator.Location = New System.Drawing.Point(134, 3)
        Me.lblKPICreator.Name = "lblKPICreator"
        Me.lblKPICreator.Size = New System.Drawing.Size(1, 16)
        Me.lblKPICreator.TabIndex = 1
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.tlpKpiShare)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(3, 83)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(144, 49)
        Me.GroupControl7.TabIndex = 1
        Me.GroupControl7.Text = "Share"
        '
        'tlpKpiShare
        '
        Me.tlpKpiShare.ColumnCount = 2
        Me.tlpKpiShare.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKpiShare.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKpiShare.Controls.Add(Me.rbKPIConfigPublic, 1, 0)
        Me.tlpKpiShare.Controls.Add(Me.rbKPIConfigPrivate, 0, 0)
        Me.tlpKpiShare.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKpiShare.Location = New System.Drawing.Point(2, 23)
        Me.tlpKpiShare.Margin = New System.Windows.Forms.Padding(2)
        Me.tlpKpiShare.Name = "tlpKpiShare"
        Me.tlpKpiShare.RowCount = 1
        Me.tlpKpiShare.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiShare.Size = New System.Drawing.Size(140, 24)
        Me.tlpKpiShare.TabIndex = 0
        '
        'rbKPIConfigPublic
        '
        Me.rbKPIConfigPublic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbKPIConfigPublic.Location = New System.Drawing.Point(73, 3)
        Me.rbKPIConfigPublic.Name = "rbKPIConfigPublic"
        Me.rbKPIConfigPublic.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.rbKPIConfigPublic.Size = New System.Drawing.Size(64, 18)
        Me.rbKPIConfigPublic.TabIndex = 1
        Me.rbKPIConfigPublic.Text = "Public"
        Me.rbKPIConfigPublic.UseVisualStyleBackColor = True
        '
        'rbKPIConfigPrivate
        '
        Me.rbKPIConfigPrivate.Checked = True
        Me.rbKPIConfigPrivate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbKPIConfigPrivate.Location = New System.Drawing.Point(3, 3)
        Me.rbKPIConfigPrivate.Name = "rbKPIConfigPrivate"
        Me.rbKPIConfigPrivate.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.rbKPIConfigPrivate.Size = New System.Drawing.Size(64, 18)
        Me.rbKPIConfigPrivate.TabIndex = 0
        Me.rbKPIConfigPrivate.TabStop = True
        Me.rbKPIConfigPrivate.Text = "Private"
        Me.rbKPIConfigPrivate.UseVisualStyleBackColor = True
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.lblKPIConfigStatus)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(3, 138)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(144, 202)
        Me.GroupControl8.TabIndex = 2
        Me.GroupControl8.Text = "Status"
        '
        'lblKPIConfigStatus
        '
        Me.lblKPIConfigStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblKPIConfigStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblKPIConfigStatus.Location = New System.Drawing.Point(2, 23)
        Me.lblKPIConfigStatus.Name = "lblKPIConfigStatus"
        Me.lblKPIConfigStatus.Size = New System.Drawing.Size(140, 177)
        Me.lblKPIConfigStatus.TabIndex = 2
        '
        'tlpKpiButtons
        '
        Me.tlpKpiButtons.ColumnCount = 2
        Me.tlpKpiButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKpiButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKpiButtons.Controls.Add(Me.btnCommitKPI, 1, 0)
        Me.tlpKpiButtons.Controls.Add(Me.btnTestKPI, 0, 0)
        Me.tlpKpiButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKpiButtons.Location = New System.Drawing.Point(1, 344)
        Me.tlpKpiButtons.Margin = New System.Windows.Forms.Padding(1)
        Me.tlpKpiButtons.Name = "tlpKpiButtons"
        Me.tlpKpiButtons.RowCount = 1
        Me.tlpKpiButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiButtons.Size = New System.Drawing.Size(148, 33)
        Me.tlpKpiButtons.TabIndex = 3
        '
        'btnCommitKPI
        '
        Me.btnCommitKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCommitKPI.Location = New System.Drawing.Point(77, 3)
        Me.btnCommitKPI.Name = "btnCommitKPI"
        Me.btnCommitKPI.Size = New System.Drawing.Size(68, 27)
        Me.btnCommitKPI.TabIndex = 1
        Me.btnCommitKPI.Text = "Commit KPI"
        '
        'btnTestKPI
        '
        Me.btnTestKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTestKPI.Location = New System.Drawing.Point(3, 3)
        Me.btnTestKPI.Name = "btnTestKPI"
        Me.btnTestKPI.Size = New System.Drawing.Size(68, 27)
        Me.btnTestKPI.TabIndex = 0
        Me.btnTestKPI.Text = "Test KPI"
        '
        'tlpKPI
        '
        Me.tlpKPI.BackColor = System.Drawing.Color.Transparent
        Me.tlpKPI.ColumnCount = 1
        Me.tlpKPI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKPI.Controls.Add(Me.vgb_KPIDesc, 0, 2)
        Me.tlpKPI.Controls.Add(Me.tlpKpiName, 0, 0)
        Me.tlpKPI.Controls.Add(Me.vgp_KPIList, 0, 1)
        Me.tlpKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKPI.Location = New System.Drawing.Point(418, 3)
        Me.tlpKPI.Name = "tlpKPI"
        Me.tlpKPI.RowCount = 3
        Me.tlpKPI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpKPI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKPI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpKPI.Size = New System.Drawing.Size(512, 372)
        Me.tlpKPI.TabIndex = 1
        '
        'vgb_KPIDesc
        '
        Me.vgb_KPIDesc.Controls.Add(Me.txtKPIDescription)
        Me.vgb_KPIDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vgb_KPIDesc.Location = New System.Drawing.Point(3, 204)
        Me.vgb_KPIDesc.Name = "vgb_KPIDesc"
        Me.vgb_KPIDesc.Size = New System.Drawing.Size(506, 165)
        Me.vgb_KPIDesc.TabIndex = 2
        Me.vgb_KPIDesc.Text = "KPI Description"
        '
        'txtKPIDescription
        '
        Me.txtKPIDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtKPIDescription.Location = New System.Drawing.Point(2, 23)
        Me.txtKPIDescription.Name = "txtKPIDescription"
        Me.txtKPIDescription.Size = New System.Drawing.Size(502, 140)
        Me.txtKPIDescription.TabIndex = 0
        '
        'tlpKpiName
        '
        Me.tlpKpiName.ColumnCount = 4
        Me.tlpKpiName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpKpiName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpKpiName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpKpiName.Controls.Add(Me.txtValueIfNull, 3, 0)
        Me.tlpKpiName.Controls.Add(Me.LabelControl34, 2, 0)
        Me.tlpKpiName.Controls.Add(Me.LabelControl33, 0, 0)
        Me.tlpKpiName.Controls.Add(Me.txtKPIName, 1, 0)
        Me.tlpKpiName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpKpiName.Location = New System.Drawing.Point(2, 2)
        Me.tlpKpiName.Margin = New System.Windows.Forms.Padding(2)
        Me.tlpKpiName.Name = "tlpKpiName"
        Me.tlpKpiName.RowCount = 1
        Me.tlpKpiName.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpKpiName.Size = New System.Drawing.Size(508, 26)
        Me.tlpKpiName.TabIndex = 0
        '
        'txtValueIfNull
        '
        Me.txtValueIfNull.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValueIfNull.Location = New System.Drawing.Point(471, 3)
        Me.txtValueIfNull.Name = "txtValueIfNull"
        Me.txtValueIfNull.Size = New System.Drawing.Size(34, 20)
        Me.txtValueIfNull.TabIndex = 4
        '
        'LabelControl34
        '
        Me.LabelControl34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl34.Location = New System.Drawing.Point(401, 3)
        Me.LabelControl34.Name = "LabelControl34"
        Me.LabelControl34.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl34.Size = New System.Drawing.Size(64, 20)
        Me.LabelControl34.TabIndex = 3
        Me.LabelControl34.Text = "Value If Null"
        '
        'LabelControl33
        '
        Me.LabelControl33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl33.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl33.Name = "LabelControl33"
        Me.LabelControl33.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl33.Size = New System.Drawing.Size(54, 20)
        Me.LabelControl33.TabIndex = 1
        Me.LabelControl33.Text = "KPI Name"
        '
        'txtKPIName
        '
        Me.txtKPIName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtKPIName.Location = New System.Drawing.Point(63, 3)
        Me.txtKPIName.Name = "txtKPIName"
        Me.txtKPIName.Size = New System.Drawing.Size(332, 20)
        Me.txtKPIName.TabIndex = 2
        '
        'vgp_KPIList
        '
        Me.vgp_KPIList.Controls.Add(Me.txtKPIFormula)
        Me.vgp_KPIList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vgp_KPIList.Location = New System.Drawing.Point(3, 33)
        Me.vgp_KPIList.Name = "vgp_KPIList"
        Me.vgp_KPIList.Size = New System.Drawing.Size(506, 165)
        Me.vgp_KPIList.TabIndex = 1
        Me.vgp_KPIList.Text = "KPI Formula"
        '
        'txtKPIFormula
        '
        Me.txtKPIFormula.AllowDrop = True
        Me.txtKPIFormula.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtKPIFormula.Location = New System.Drawing.Point(2, 23)
        Me.txtKPIFormula.Name = "txtKPIFormula"
        Me.txtKPIFormula.Size = New System.Drawing.Size(502, 140)
        Me.txtKPIFormula.TabIndex = 0
        '
        'tlpRight
        '
        Me.tlpRight.ColumnCount = 1
        Me.tlpRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRight.Controls.Add(Me.vgb_AggrFunc, 0, 1)
        Me.tlpRight.Controls.Add(Me.vgb_OperatorList, 0, 0)
        Me.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpRight.Location = New System.Drawing.Point(936, 3)
        Me.tlpRight.Name = "tlpRight"
        Me.tlpRight.RowCount = 2
        Me.tlpRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpRight.Size = New System.Drawing.Size(121, 372)
        Me.tlpRight.TabIndex = 2
        '
        'vgb_AggrFunc
        '
        Me.vgb_AggrFunc.Controls.Add(Me.lstAggregateFunction)
        Me.vgb_AggrFunc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vgb_AggrFunc.Location = New System.Drawing.Point(3, 189)
        Me.vgb_AggrFunc.Name = "vgb_AggrFunc"
        Me.vgb_AggrFunc.Size = New System.Drawing.Size(115, 180)
        Me.vgb_AggrFunc.TabIndex = 1
        Me.vgb_AggrFunc.Text = "Aggregation"
        '
        'lstAggregateFunction
        '
        Me.lstAggregateFunction.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstAggregateFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstAggregateFunction.Items.AddRange(New Object() {"Avg()", "Sum()", "Count()", "Min()", "Max()"})
        Me.lstAggregateFunction.Location = New System.Drawing.Point(2, 23)
        Me.lstAggregateFunction.Name = "lstAggregateFunction"
        Me.lstAggregateFunction.Size = New System.Drawing.Size(111, 155)
        Me.lstAggregateFunction.TabIndex = 4
        '
        'vgb_OperatorList
        '
        Me.vgb_OperatorList.Controls.Add(Me.lstOperators)
        Me.vgb_OperatorList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vgb_OperatorList.Location = New System.Drawing.Point(3, 3)
        Me.vgb_OperatorList.Name = "vgb_OperatorList"
        Me.vgb_OperatorList.Size = New System.Drawing.Size(115, 180)
        Me.vgb_OperatorList.TabIndex = 0
        Me.vgb_OperatorList.Text = "Operator List"
        '
        'lstOperators
        '
        Me.lstOperators.AllowDrop = True
        Me.lstOperators.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstOperators.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOperators.Items.AddRange(New Object() {"=", "<>", ">", "<", "<=", ">=", "+", "-", "*", "/", "^", "()", "AND", "OR", "NOT", "LIKE", "Contains", "Contains Entire", "Within", "Entirely Within", "Intersects"})
        Me.lstOperators.Location = New System.Drawing.Point(2, 23)
        Me.lstOperators.Name = "lstOperators"
        Me.lstOperators.Size = New System.Drawing.Size(111, 155)
        Me.lstOperators.TabIndex = 2
        '
        'tlpLeftInfo
        '
        Me.tlpLeftInfo.ColumnCount = 1
        Me.tlpLeftInfo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl9, 0, 0)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl36, 0, 1)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl37, 0, 2)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl38, 0, 3)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl45, 0, 4)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl48, 0, 5)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl61, 0, 6)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl62, 0, 7)
        Me.tlpLeftInfo.Controls.Add(Me.LabelControl63, 0, 8)
        Me.tlpLeftInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpLeftInfo.Location = New System.Drawing.Point(3, 3)
        Me.tlpLeftInfo.Name = "tlpLeftInfo"
        Me.tlpLeftInfo.RowCount = 10
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLeftInfo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpLeftInfo.Size = New System.Drawing.Size(259, 372)
        Me.tlpLeftInfo.TabIndex = 3
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(253, 19)
        Me.LabelControl9.TabIndex = 0
        Me.LabelControl9.Text = "Steps:"
        '
        'LabelControl36
        '
        Me.LabelControl36.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl36.Appearance.Options.UseFont = True
        Me.LabelControl36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl36.Location = New System.Drawing.Point(3, 28)
        Me.LabelControl36.Name = "LabelControl36"
        Me.LabelControl36.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl36.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl36.TabIndex = 1
        Me.LabelControl36.Text = "Step1: Enter new KPI Name"
        '
        'LabelControl37
        '
        Me.LabelControl37.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl37.Appearance.Options.UseFont = True
        Me.LabelControl37.Appearance.Options.UseTextOptions = True
        Me.LabelControl37.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl37.Location = New System.Drawing.Point(3, 50)
        Me.LabelControl37.Name = "LabelControl37"
        Me.LabelControl37.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl37.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl37.TabIndex = 2
        Me.LabelControl37.Text = "Step2: Optionally set value in case result is NULL"
        '
        'LabelControl38
        '
        Me.LabelControl38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl38.Location = New System.Drawing.Point(3, 72)
        Me.LabelControl38.Name = "LabelControl38"
        Me.LabelControl38.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl38.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl38.TabIndex = 3
        Me.LabelControl38.Text = "Step3: Drag aggregation function"
        '
        'LabelControl45
        '
        Me.LabelControl45.Appearance.Options.UseTextOptions = True
        Me.LabelControl45.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl45.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl45.Location = New System.Drawing.Point(3, 94)
        Me.LabelControl45.Name = "LabelControl45"
        Me.LabelControl45.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl45.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl45.TabIndex = 4
        Me.LabelControl45.Text = "Step4: Drag from Counter List a counter of interest"
        '
        'LabelControl48
        '
        Me.LabelControl48.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl48.Location = New System.Drawing.Point(3, 116)
        Me.LabelControl48.Name = "LabelControl48"
        Me.LabelControl48.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl48.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl48.TabIndex = 5
        Me.LabelControl48.Text = "Step5: Add description"
        '
        'LabelControl61
        '
        Me.LabelControl61.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl61.Location = New System.Drawing.Point(3, 138)
        Me.LabelControl61.Name = "LabelControl61"
        Me.LabelControl61.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl61.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl61.TabIndex = 6
        Me.LabelControl61.Text = "Step6: Test KPI"
        '
        'LabelControl62
        '
        Me.LabelControl62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl62.Location = New System.Drawing.Point(3, 160)
        Me.LabelControl62.Name = "LabelControl62"
        Me.LabelControl62.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl62.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl62.TabIndex = 7
        Me.LabelControl62.Text = "Strep7: Commit KPI"
        '
        'LabelControl63
        '
        Me.LabelControl63.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl63.Location = New System.Drawing.Point(3, 182)
        Me.LabelControl63.Name = "LabelControl63"
        Me.LabelControl63.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl63.Size = New System.Drawing.Size(253, 16)
        Me.LabelControl63.TabIndex = 8
        Me.LabelControl63.Text = "Step8: Add KPI to KPI Group"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'frmDatamartKpiConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1060, 408)
        Me.Controls.Add(Me.tlpMain)
        Me.IconOptions.Image = CType(resources.GetObject("frmDatamartKpiConfig.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1070, 440)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1062, 440)
        Me.Name = "frmDatamartKpiConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datamart: KPI Configuration"
        Me.TopMost = True
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpKpiDetails.ResumeLayout(False)
        CType(Me.gcKPIDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcKPIDetails.ResumeLayout(False)
        Me.tlpKpiDetailsTop.ResumeLayout(False)
        Me.tlpKpiDetailsTop.PerformLayout()
        Me.tlpObjectType.ResumeLayout(False)
        Me.tlpObjectType.PerformLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.tlpKpiShare.ResumeLayout(False)
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.tlpKpiButtons.ResumeLayout(False)
        Me.tlpKPI.ResumeLayout(False)
        CType(Me.vgb_KPIDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vgb_KPIDesc.ResumeLayout(False)
        CType(Me.txtKPIDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpKpiName.ResumeLayout(False)
        Me.tlpKpiName.PerformLayout()
        CType(Me.txtValueIfNull.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKPIName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vgp_KPIList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vgp_KPIList.ResumeLayout(False)
        CType(Me.txtKPIFormula.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpRight.ResumeLayout(False)
        CType(Me.vgb_AggrFunc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vgb_AggrFunc.ResumeLayout(False)
        CType(Me.lstAggregateFunction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vgb_OperatorList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vgb_OperatorList.ResumeLayout(False)
        CType(Me.lstOperators, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpLeftInfo.ResumeLayout(False)
        Me.tlpLeftInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcKPIDetails As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblKPIConfigObjectType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents rbKPIConfigPublic As RadioButton
    Friend WithEvents rbKPIConfigPrivate As RadioButton
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblKPIConfigStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCommitKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTestKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents vgb_KPIDesc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtKPIDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ExTableLayoutPanel8 As Library.ExTableLayoutPanel
    Friend WithEvents txtValueIfNull As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl34 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl33 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtKPIName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents vgp_KPIList As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtKPIFormula As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents vgb_AggrFunc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lstAggregateFunction As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents vgb_OperatorList As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lstOperators As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents tlpLeftInfo As TableLayoutPanel
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl36 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl37 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl38 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl45 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl48 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl61 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl62 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl63 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents tlpObjectType As TableLayoutPanel
    Friend WithEvents lblObjType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKPICreator As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tlpKpiName As TableLayoutPanel
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpKpiDetails As TableLayoutPanel
    Friend WithEvents tlpKpiDetailsTop As TableLayoutPanel
    Friend WithEvents tlpKpiShare As TableLayoutPanel
    Friend WithEvents tlpKpiButtons As TableLayoutPanel
    Friend WithEvents tlpKPI As TableLayoutPanel
    Friend WithEvents tlpRight As TableLayoutPanel
End Class
