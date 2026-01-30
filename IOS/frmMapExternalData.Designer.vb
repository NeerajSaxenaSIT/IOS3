<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapExternalData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMapExternalData))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpFirstRow = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDelimiter = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tlpThirdSection = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBoxJoin = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbJoinDataGridField = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbJoinsToMapField = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupBoxThematic = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbThematicFields = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbThemticType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.ceMatchThematic = New DevExpress.XtraEditors.CheckEdit()
        Me.ceMapToVoronoi = New DevExpress.XtraEditors.CheckEdit()
        Me.btnMap = New DevExpress.XtraEditors.SimpleButton()
        Me.gcExternalData = New DevExpress.XtraGrid.GridControl()
        Me.cmTagPaste = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiTagPastePaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvExternalData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FormAssistant1 = New DevExpress.XtraBars.FormAssistant()
        Me.tlpMain.SuspendLayout()
        Me.tlpFirstRow.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.cmbDelimiter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpThirdSection.SuspendLayout()
        CType(Me.GroupBoxJoin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxJoin.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.cmbJoinDataGridField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbJoinsToMapField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupBoxThematic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxThematic.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.cmbThematicFields.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbThemticType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.ceMatchThematic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceMapToVoronoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcExternalData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmTagPaste.SuspendLayout()
        CType(Me.gvExternalData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.BackColor = System.Drawing.Color.Transparent
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.tlpFirstRow, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpThirdSection, 0, 2)
        Me.tlpMain.Controls.Add(Me.btnMap, 0, 3)
        Me.tlpMain.Controls.Add(Me.gcExternalData, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 4
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tlpMain.Size = New System.Drawing.Size(841, 645)
        Me.tlpMain.TabIndex = 0
        '
        'tlpFirstRow
        '
        Me.tlpFirstRow.ColumnCount = 2
        Me.tlpFirstRow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpFirstRow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpFirstRow.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.tlpFirstRow.Controls.Add(Me.LabelControl6, 0, 0)
        Me.tlpFirstRow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpFirstRow.Location = New System.Drawing.Point(2, 2)
        Me.tlpFirstRow.Margin = New System.Windows.Forms.Padding(2)
        Me.tlpFirstRow.Name = "tlpFirstRow"
        Me.tlpFirstRow.RowCount = 1
        Me.tlpFirstRow.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpFirstRow.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.tlpFirstRow.Size = New System.Drawing.Size(837, 66)
        Me.tlpFirstRow.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl7, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbDelimiter, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(420, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(415, 62)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'LabelControl7
        '
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl7.TabIndex = 5
        Me.LabelControl7.Text = "Delimiter"
        '
        'cmbDelimiter
        '
        Me.cmbDelimiter.EditValue = "TAB"
        Me.cmbDelimiter.Location = New System.Drawing.Point(83, 3)
        Me.cmbDelimiter.Name = "cmbDelimiter"
        Me.cmbDelimiter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDelimiter.Properties.Items.AddRange(New Object() {"TAB", ":", ","})
        Me.cmbDelimiter.Size = New System.Drawing.Size(174, 20)
        Me.cmbDelimiter.TabIndex = 6
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl6.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(4, 2, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(412, 60)
        Me.LabelControl6.TabIndex = 1
        Me.LabelControl6.Text = "Step 1: Select delimiter" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 2: Copy Paste into Datagrid, having first line as " &
    "header" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 3: set the join fields" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Step 4: set the field for the thematic"
        '
        'tlpThirdSection
        '
        Me.tlpThirdSection.ColumnCount = 2
        Me.tlpThirdSection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpThirdSection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpThirdSection.Controls.Add(Me.GroupBoxJoin, 0, 0)
        Me.tlpThirdSection.Controls.Add(Me.GroupBoxThematic, 1, 0)
        Me.tlpThirdSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpThirdSection.Location = New System.Drawing.Point(2, 492)
        Me.tlpThirdSection.Margin = New System.Windows.Forms.Padding(2)
        Me.tlpThirdSection.Name = "tlpThirdSection"
        Me.tlpThirdSection.RowCount = 1
        Me.tlpThirdSection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpThirdSection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 121.0!))
        Me.tlpThirdSection.Size = New System.Drawing.Size(837, 116)
        Me.tlpThirdSection.TabIndex = 7
        '
        'GroupBoxJoin
        '
        Me.GroupBoxJoin.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBoxJoin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBoxJoin.Location = New System.Drawing.Point(3, 3)
        Me.GroupBoxJoin.Name = "GroupBoxJoin"
        Me.GroupBoxJoin.Size = New System.Drawing.Size(412, 110)
        Me.GroupBoxJoin.TabIndex = 0
        Me.GroupBoxJoin.Text = "Joins"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbJoinDataGridField, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbJoinsToMapField, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(408, 85)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(116, 20)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Joins To Map Fields"
        '
        'cmbJoinDataGridField
        '
        Me.cmbJoinDataGridField.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbJoinDataGridField.Location = New System.Drawing.Point(125, 3)
        Me.cmbJoinDataGridField.Name = "cmbJoinDataGridField"
        Me.cmbJoinDataGridField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbJoinDataGridField.Size = New System.Drawing.Size(240, 20)
        Me.cmbJoinDataGridField.TabIndex = 0
        '
        'cmbJoinsToMapField
        '
        Me.cmbJoinsToMapField.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbJoinsToMapField.Location = New System.Drawing.Point(125, 29)
        Me.cmbJoinsToMapField.Name = "cmbJoinsToMapField"
        Me.cmbJoinsToMapField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbJoinsToMapField.Size = New System.Drawing.Size(240, 20)
        Me.cmbJoinsToMapField.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(116, 20)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "DataGrid Fields"
        '
        'GroupBoxThematic
        '
        Me.GroupBoxThematic.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBoxThematic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBoxThematic.Location = New System.Drawing.Point(421, 3)
        Me.GroupBoxThematic.Name = "GroupBoxThematic"
        Me.GroupBoxThematic.Size = New System.Drawing.Size(413, 110)
        Me.GroupBoxThematic.TabIndex = 1
        Me.GroupBoxThematic.Text = "Thematic"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl5, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl4, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl3, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbThematicFields, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbThemticType, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 1, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(409, 85)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelControl5.Location = New System.Drawing.Point(3, 55)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(84, 17)
        Me.LabelControl5.TabIndex = 6
        Me.LabelControl5.Text = "Match Thematics"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 29)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(116, 20)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "Thematic Types"
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(116, 20)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Thematic Fields"
        '
        'cmbThematicFields
        '
        Me.cmbThematicFields.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbThematicFields.Location = New System.Drawing.Point(125, 3)
        Me.cmbThematicFields.Name = "cmbThematicFields"
        Me.cmbThematicFields.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbThematicFields.Size = New System.Drawing.Size(240, 20)
        Me.cmbThematicFields.TabIndex = 0
        '
        'cmbThemticType
        '
        Me.cmbThemticType.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbThemticType.Location = New System.Drawing.Point(125, 29)
        Me.cmbThemticType.Name = "cmbThemticType"
        Me.cmbThemticType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbThemticType.Properties.Items.AddRange(New Object() {"Ranged Theme", "Individual Value Theme"})
        Me.cmbThemticType.Size = New System.Drawing.Size(240, 20)
        Me.cmbThemticType.TabIndex = 1
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.47826!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.52174!))
        Me.TableLayoutPanel4.Controls.Add(Me.ceMatchThematic, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.ceMapToVoronoi, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(124, 54)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(283, 29)
        Me.TableLayoutPanel4.TabIndex = 7
        '
        'ceMatchThematic
        '
        Me.ceMatchThematic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceMatchThematic.Location = New System.Drawing.Point(3, 3)
        Me.ceMatchThematic.Name = "ceMatchThematic"
        Me.ceMatchThematic.Properties.Caption = ""
        Me.ceMatchThematic.Size = New System.Drawing.Size(46, 23)
        Me.ceMatchThematic.TabIndex = 0
        '
        'ceMapToVoronoi
        '
        Me.ceMapToVoronoi.Dock = System.Windows.Forms.DockStyle.Top
        Me.ceMapToVoronoi.Location = New System.Drawing.Point(55, 3)
        Me.ceMapToVoronoi.Name = "ceMapToVoronoi"
        Me.ceMapToVoronoi.Properties.Caption = "Map To Voronoi"
        Me.ceMapToVoronoi.Size = New System.Drawing.Size(225, 20)
        Me.ceMapToVoronoi.TabIndex = 1
        '
        'btnMap
        '
        Me.btnMap.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMap.Location = New System.Drawing.Point(694, 613)
        Me.btnMap.Name = "btnMap"
        Me.btnMap.Size = New System.Drawing.Size(144, 29)
        Me.btnMap.TabIndex = 8
        Me.btnMap.Text = "Map"
        '
        'gcExternalData
        '
        Me.gcExternalData.ContextMenuStrip = Me.cmTagPaste
        Me.gcExternalData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcExternalData.Location = New System.Drawing.Point(3, 73)
        Me.gcExternalData.MainView = Me.gvExternalData
        Me.gcExternalData.Name = "gcExternalData"
        Me.gcExternalData.Size = New System.Drawing.Size(835, 414)
        Me.gcExternalData.TabIndex = 9
        Me.gcExternalData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvExternalData})
        '
        'cmTagPaste
        '
        Me.cmTagPaste.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmTagPaste.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiTagPastePaste})
        Me.cmTagPaste.Name = "cm_TagManagement"
        Me.cmTagPaste.Size = New System.Drawing.Size(189, 26)
        '
        'tsmiTagPastePaste
        '
        Me.tsmiTagPastePaste.Name = "tsmiTagPastePaste"
        Me.tsmiTagPastePaste.Size = New System.Drawing.Size(188, 22)
        Me.tsmiTagPastePaste.Text = "Paste From Clipboard"
        '
        'gvExternalData
        '
        Me.gvExternalData.GridControl = Me.gcExternalData
        Me.gvExternalData.Name = "gvExternalData"
        Me.gvExternalData.OptionsBehavior.Editable = False
        Me.gvExternalData.OptionsView.ColumnAutoWidth = False
        Me.gvExternalData.OptionsView.ShowGroupPanel = False
        '
        'frmMapExternalData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(841, 645)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Icon = CType(resources.GetObject("frmMapExternalData.IconOptions.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMapExternalData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IOS - Map External Data"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpFirstRow.ResumeLayout(False)
        Me.tlpFirstRow.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.cmbDelimiter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpThirdSection.ResumeLayout(False)
        CType(Me.GroupBoxJoin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxJoin.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.cmbJoinDataGridField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbJoinsToMapField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupBoxThematic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxThematic.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.cmbThematicFields.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbThemticType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.ceMatchThematic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceMapToVoronoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcExternalData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmTagPaste.ResumeLayout(False)
        CType(Me.gvExternalData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlpFirstRow As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlpThirdSection As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBoxJoin As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupBoxThematic As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnMap As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbJoinDataGridField As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbJoinsToMapField As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbThematicFields As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbThemticType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmTagPaste As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiTagPastePaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbDelimiter As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ceMatchThematic As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceMapToVoronoi As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FormAssistant1 As DevExpress.XtraBars.FormAssistant
    Friend WithEvents gcExternalData As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvExternalData As DevExpress.XtraGrid.Views.Grid.GridView
End Class
