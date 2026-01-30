<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgRefChkAddParam
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgRefChkAddParam))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbParam = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblMOName = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.lblVendor = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTemplateName = New DevExpress.XtraEditors.LabelControl()
        Me.grpOption1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpManualSetValue = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblParameterName = New DevExpress.XtraEditors.LabelControl()
        Me.gcDistinctValues = New DevExpress.XtraGrid.GridControl()
        Me.gvDistinctValues = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtParamValues = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbOperator = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ceIsEnabled = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.grpAutoSetValue = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.ceSetAutoValue = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCommonalityValue = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnAddParam = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.cmbParam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.grpOption1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOption1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.grpManualSetValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpManualSetValue.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        CType(Me.gcDistinctValues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDistinctValues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtParamValues.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperator.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsEnabled.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpAutoSetValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAutoSetValue.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.ceSetAutoValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommonalityValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpOption1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(554, 511)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.cmbParam, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblMOName, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl10, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl12, 2, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 28)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(554, 28)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'cmbParam
        '
        Me.cmbParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbParam.EditValue = ""
        Me.cmbParam.Location = New System.Drawing.Point(367, 4)
        Me.cmbParam.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbParam.Name = "cmbParam"
        Me.cmbParam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbParam.Size = New System.Drawing.Size(184, 20)
        Me.cmbParam.TabIndex = 34
        '
        'lblMOName
        '
        Me.lblMOName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMOName.Location = New System.Drawing.Point(88, 3)
        Me.lblMOName.Name = "lblMOName"
        Me.lblMOName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMOName.Size = New System.Drawing.Size(183, 22)
        Me.lblMOName.TabIndex = 17
        '
        'LabelControl10
        '
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl10.Size = New System.Drawing.Size(79, 22)
        Me.LabelControl10.TabIndex = 18
        Me.LabelControl10.Text = "MO"
        '
        'LabelControl12
        '
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(277, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(84, 22)
        Me.LabelControl12.TabIndex = 20
        Me.LabelControl12.Text = "Parameter Name"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl7, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblVendor, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl5, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblTemplateName, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(554, 28)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'LabelControl7
        '
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(277, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(84, 22)
        Me.LabelControl7.TabIndex = 20
        Me.LabelControl7.Text = "Vendor"
        '
        'lblVendor
        '
        Me.lblVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVendor.Location = New System.Drawing.Point(367, 3)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblVendor.Size = New System.Drawing.Size(184, 22)
        Me.lblVendor.TabIndex = 19
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(79, 22)
        Me.LabelControl5.TabIndex = 18
        Me.LabelControl5.Text = "Template Name"
        '
        'lblTemplateName
        '
        Me.lblTemplateName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTemplateName.Location = New System.Drawing.Point(88, 3)
        Me.lblTemplateName.Name = "lblTemplateName"
        Me.lblTemplateName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblTemplateName.Size = New System.Drawing.Size(183, 22)
        Me.lblTemplateName.TabIndex = 17
        '
        'grpOption1
        '
        Me.grpOption1.Controls.Add(Me.TableLayoutPanel4)
        Me.grpOption1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpOption1.Location = New System.Drawing.Point(3, 59)
        Me.grpOption1.Name = "grpOption1"
        Me.grpOption1.Size = New System.Drawing.Size(548, 442)
        Me.grpOption1.TabIndex = 2
        Me.grpOption1.Text = "Option 1: Add Single Parameter"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.48529!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.51471!))
        Me.TableLayoutPanel4.Controls.Add(Me.grpManualSetValue, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.ceIsEnabled, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.grpAutoSetValue, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 3)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 4
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.2687!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.7313!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(544, 417)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'grpManualSetValue
        '
        Me.TableLayoutPanel4.SetColumnSpan(Me.grpManualSetValue, 2)
        Me.grpManualSetValue.Controls.Add(Me.TableLayoutPanel7)
        Me.grpManualSetValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpManualSetValue.Location = New System.Drawing.Point(3, 114)
        Me.grpManualSetValue.Name = "grpManualSetValue"
        Me.grpManualSetValue.Size = New System.Drawing.Size(538, 267)
        Me.grpManualSetValue.TabIndex = 24
        Me.grpManualSetValue.Text = "Option 1.2: Manual Set Value"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 3
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.lblParameterName, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.gcDistinctValues, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.txtParamValues, 2, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.LabelControl6, 2, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.LabelControl4, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.LabelControl13, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.cmbOperator, 1, 1)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(534, 242)
        Me.TableLayoutPanel7.TabIndex = 1
        '
        'lblParameterName
        '
        Me.lblParameterName.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblParameterName.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblParameterName.Appearance.Options.UseFont = True
        Me.lblParameterName.Appearance.Options.UseForeColor = True
        Me.lblParameterName.Appearance.Options.UseTextOptions = True
        Me.lblParameterName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblParameterName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblParameterName.Location = New System.Drawing.Point(3, 3)
        Me.lblParameterName.Name = "lblParameterName"
        Me.lblParameterName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblParameterName.Size = New System.Drawing.Size(206, 22)
        Me.lblParameterName.TabIndex = 26
        Me.lblParameterName.Text = "Parameter Name"
        '
        'gcDistinctValues
        '
        Me.gcDistinctValues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDistinctValues.Location = New System.Drawing.Point(3, 31)
        Me.gcDistinctValues.MainView = Me.gvDistinctValues
        Me.gcDistinctValues.Name = "gcDistinctValues"
        Me.gcDistinctValues.Size = New System.Drawing.Size(206, 176)
        Me.gcDistinctValues.TabIndex = 27
        Me.gcDistinctValues.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDistinctValues})
        '
        'gvDistinctValues
        '
        Me.gvDistinctValues.GridControl = Me.gcDistinctValues
        Me.gvDistinctValues.Name = "gvDistinctValues"
        Me.gvDistinctValues.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDistinctValues.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvDistinctValues.OptionsBehavior.Editable = False
        Me.gvDistinctValues.OptionsBehavior.ReadOnly = True
        Me.gvDistinctValues.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDistinctValues.OptionsClipboard.CopyCollapsedData = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDistinctValues.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDistinctValues.OptionsView.ColumnAutoWidth = False
        Me.gvDistinctValues.OptionsView.ShowGroupPanel = False
        '
        'txtParamValues
        '
        Me.txtParamValues.AllowDrop = True
        Me.txtParamValues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtParamValues.Location = New System.Drawing.Point(325, 31)
        Me.txtParamValues.Name = "txtParamValues"
        Me.txtParamValues.Size = New System.Drawing.Size(206, 176)
        Me.txtParamValues.TabIndex = 29
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.DarkRed
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(325, 215)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 5, 12, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(197, 24)
        Me.LabelControl6.TabIndex = 30
        Me.LabelControl6.Text = "?"
        Me.LabelControl6.ToolTip = resources.GetString("LabelControl6.ToolTip")
        Me.LabelControl6.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.LabelControl6.ToolTipTitle = "Syntax Rules"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(325, 3)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(206, 22)
        Me.LabelControl4.TabIndex = 31
        Me.LabelControl4.Text = "Drag or Type value"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl13.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Appearance.Options.UseForeColor = True
        Me.LabelControl13.Appearance.Options.UseTextOptions = True
        Me.LabelControl13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(215, 3)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(104, 22)
        Me.LabelControl13.TabIndex = 32
        Me.LabelControl13.Text = "Operator"
        '
        'cmbOperator
        '
        Me.cmbOperator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbOperator.EditValue = "Select Operator"
        Me.cmbOperator.Location = New System.Drawing.Point(215, 31)
        Me.cmbOperator.Name = "cmbOperator"
        Me.cmbOperator.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperator.Properties.Items.AddRange(New Object() {"Select Operator", "=", "<>", "<", ">", "<=", ">=", "Like", "Range", "IN"})
        Me.cmbOperator.Size = New System.Drawing.Size(104, 20)
        Me.cmbOperator.TabIndex = 33
        '
        'ceIsEnabled
        '
        Me.ceIsEnabled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceIsEnabled.Location = New System.Drawing.Point(110, 3)
        Me.ceIsEnabled.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceIsEnabled.Name = "ceIsEnabled"
        Me.ceIsEnabled.Properties.Caption = ""
        Me.ceIsEnabled.Size = New System.Drawing.Size(431, 22)
        Me.ceIsEnabled.TabIndex = 21
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(99, 22)
        Me.LabelControl1.TabIndex = 15
        Me.LabelControl1.Text = "Is Enabled"
        '
        'grpAutoSetValue
        '
        Me.TableLayoutPanel4.SetColumnSpan(Me.grpAutoSetValue, 2)
        Me.grpAutoSetValue.Controls.Add(Me.TableLayoutPanel6)
        Me.grpAutoSetValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpAutoSetValue.Location = New System.Drawing.Point(3, 31)
        Me.grpAutoSetValue.Name = "grpAutoSetValue"
        Me.grpAutoSetValue.Size = New System.Drawing.Size(538, 77)
        Me.grpAutoSetValue.TabIndex = 23
        Me.grpAutoSetValue.Text = "Option 1.1: Auto Set Value"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.10112!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.89888!))
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl11, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LabelControl9, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.ceSetAutoValue, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.txtCommonalityValue, 1, 1)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(534, 52)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'LabelControl11
        '
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(95, 20)
        Me.LabelControl11.TabIndex = 21
        Me.LabelControl11.Text = "Commonality Value"
        '
        'LabelControl9
        '
        Me.LabelControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl9.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(95, 20)
        Me.LabelControl9.TabIndex = 18
        Me.LabelControl9.Text = "Set Auto Value"
        '
        'ceSetAutoValue
        '
        Me.ceSetAutoValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceSetAutoValue.Location = New System.Drawing.Point(106, 3)
        Me.ceSetAutoValue.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.ceSetAutoValue.Name = "ceSetAutoValue"
        Me.ceSetAutoValue.Properties.Caption = ""
        Me.ceSetAutoValue.Size = New System.Drawing.Size(425, 20)
        Me.ceSetAutoValue.TabIndex = 20
        '
        'txtCommonalityValue
        '
        Me.txtCommonalityValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCommonalityValue.Location = New System.Drawing.Point(104, 30)
        Me.txtCommonalityValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.txtCommonalityValue.Name = "txtCommonalityValue"
        Me.txtCommonalityValue.Properties.MaxLength = 2
        Me.txtCommonalityValue.Size = New System.Drawing.Size(427, 20)
        Me.txtCommonalityValue.TabIndex = 25
        Me.txtCommonalityValue.ToolTipTitle = "Enter Numeric Value between 0 and 100"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel4.SetColumnSpan(Me.TableLayoutPanel5, 2)
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.42647!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.57353!))
        Me.TableLayoutPanel5.Controls.Add(Me.lblMessage, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.btnAddParam, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 384)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(544, 33)
        Me.TableLayoutPanel5.TabIndex = 25
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(387, 27)
        Me.lblMessage.TabIndex = 26
        '
        'btnAddParam
        '
        Me.btnAddParam.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAddParam.Location = New System.Drawing.Point(397, 3)
        Me.btnAddParam.Name = "btnAddParam"
        Me.btnAddParam.Size = New System.Drawing.Size(144, 27)
        Me.btnAddParam.TabIndex = 22
        Me.btnAddParam.Text = "Add Parameter"
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgRefChkAddParam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 511)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(570, 550)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(570, 550)
        Me.Name = "dlgRefChkAddParam"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ref Check - Add Parameter"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.cmbParam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.grpOption1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOption1.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.grpManualSetValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpManualSetValue.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        CType(Me.gcDistinctValues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDistinctValues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtParamValues.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperator.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsEnabled.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpAutoSetValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAutoSetValue.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.ceSetAutoValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommonalityValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents grpOption1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAddParam As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMOName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblVendor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTemplateName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpManualSetValue As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents lblParameterName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpAutoSetValue As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents gcDistinctValues As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDistinctValues As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Public WithEvents ceIsEnabled As DevExpress.XtraEditors.CheckEdit
    Public WithEvents txtParamValues As DevExpress.XtraEditors.MemoEdit
    Public WithEvents ceSetAutoValue As DevExpress.XtraEditors.CheckEdit
    Public WithEvents txtCommonalityValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbOperator As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbParam As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
End Class
