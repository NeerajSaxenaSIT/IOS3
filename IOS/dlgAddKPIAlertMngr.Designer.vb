<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgAddKPIAlertMngr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgAddKPIAlertMngr))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSearckKPI = New DevExpress.XtraEditors.ButtonEdit()
        Me.lstviewKPI = New DevExpress.XtraTreeList.TreeList()
        Me.TableLayoutPanel20 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbTechnology = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbObject = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbInterval = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbTarget = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbMethod = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tlpBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnAddKPI = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tlpMain.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.txtSearckKPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstviewKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel20.SuspendLayout()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel15.SuspendLayout()
        CType(Me.cmbObject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.cmbInterval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel16.SuspendLayout()
        CType(Me.cmbTarget.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.GroupControl5, 0, 0)
        Me.tlpMain.Controls.Add(Me.GroupControl6, 0, 1)
        Me.tlpMain.Controls.Add(Me.tlpBottom, 0, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpMain.Size = New System.Drawing.Size(364, 668)
        Me.tlpMain.TabIndex = 0
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel11)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(360, 574)
        Me.GroupControl5.TabIndex = 1
        Me.GroupControl5.Text = "Search And Add KPI"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 1
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.txtSearckKPI, 0, 4)
        Me.TableLayoutPanel11.Controls.Add(Me.lstviewKPI, 0, 5)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel20, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel15, 0, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel10, 0, 3)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel16, 0, 2)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 6
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(356, 552)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'txtSearckKPI
        '
        Me.txtSearckKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearckKPI.Location = New System.Drawing.Point(2, 106)
        Me.txtSearckKPI.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearckKPI.Name = "txtSearckKPI"
        Me.txtSearckKPI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearckKPI.Properties.NullValuePrompt = "Search..."
        Me.txtSearckKPI.Size = New System.Drawing.Size(352, 20)
        Me.txtSearckKPI.TabIndex = 5
        '
        'lstviewKPI
        '
        Me.lstviewKPI.CustomizationFormBounds = New System.Drawing.Rectangle(1116, 239, 250, 200)
        Me.lstviewKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstviewKPI.Location = New System.Drawing.Point(2, 126)
        Me.lstviewKPI.Margin = New System.Windows.Forms.Padding(2)
        Me.lstviewKPI.Name = "lstviewKPI"
        Me.lstviewKPI.OptionsBehavior.Editable = False
        Me.lstviewKPI.OptionsBehavior.ReadOnly = True
        Me.lstviewKPI.OptionsMenu.EnableColumnMenu = False
        Me.lstviewKPI.OptionsMenu.EnableFooterMenu = False
        Me.lstviewKPI.OptionsMenu.EnableNodeMenu = False
        Me.lstviewKPI.OptionsMenu.ShowAutoFilterRowItem = False
        Me.lstviewKPI.OptionsNavigation.MoveOnEdit = False
        Me.lstviewKPI.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.lstviewKPI.OptionsView.BestFitMode = DevExpress.XtraTreeList.TreeListBestFitMode.Full
        Me.lstviewKPI.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.All
        Me.lstviewKPI.OptionsView.ExpandButtonCentered = False
        Me.lstviewKPI.OptionsView.ShowButtons = False
        Me.lstviewKPI.OptionsView.ShowHorzLines = False
        Me.lstviewKPI.OptionsView.ShowIndicator = False
        Me.lstviewKPI.OptionsView.ShowRoot = False
        Me.lstviewKPI.Size = New System.Drawing.Size(352, 424)
        Me.lstviewKPI.TabIndex = 7
        '
        'TableLayoutPanel20
        '
        Me.TableLayoutPanel20.ColumnCount = 2
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.Controls.Add(Me.cmbTechnology, 1, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.LabelControl11, 0, 0)
        Me.TableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel20.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel20.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel20.Name = "TableLayoutPanel20"
        Me.TableLayoutPanel20.RowCount = 1
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel20.Size = New System.Drawing.Size(356, 26)
        Me.TableLayoutPanel20.TabIndex = 10
        '
        'cmbTechnology
        '
        Me.cmbTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTechnology.Location = New System.Drawing.Point(82, 2)
        Me.cmbTechnology.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbTechnology.Name = "cmbTechnology"
        Me.cmbTechnology.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTechnology.Size = New System.Drawing.Size(272, 20)
        Me.cmbTechnology.TabIndex = 3
        '
        'LabelControl11
        '
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl11.TabIndex = 4
        Me.LabelControl11.Text = "Technology"
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 2
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.cmbObject, 1, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.LabelControl12, 0, 0)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 1
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(356, 26)
        Me.TableLayoutPanel15.TabIndex = 11
        '
        'cmbObject
        '
        Me.cmbObject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbObject.Location = New System.Drawing.Point(82, 2)
        Me.cmbObject.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbObject.Name = "cmbObject"
        Me.cmbObject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbObject.Size = New System.Drawing.Size(272, 20)
        Me.cmbObject.TabIndex = 4
        '
        'LabelControl12
        '
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl12.TabIndex = 5
        Me.LabelControl12.Text = "Counter Type"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.cmbInterval, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 78)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(356, 26)
        Me.TableLayoutPanel10.TabIndex = 10
        '
        'cmbInterval
        '
        Me.cmbInterval.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbInterval.Location = New System.Drawing.Point(82, 2)
        Me.cmbInterval.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbInterval.Name = "cmbInterval"
        Me.cmbInterval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbInterval.Properties.DropDownRows = 2
        Me.cmbInterval.Properties.Items.AddRange(New Object() {"DAY", "HOUR"})
        Me.cmbInterval.Size = New System.Drawing.Size(272, 20)
        Me.cmbInterval.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "Interval"
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 2
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.cmbTarget, 1, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.LabelControl13, 0, 0)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(0, 52)
        Me.TableLayoutPanel16.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 1
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(356, 26)
        Me.TableLayoutPanel16.TabIndex = 12
        '
        'cmbTarget
        '
        Me.cmbTarget.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTarget.Location = New System.Drawing.Point(83, 3)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTarget.Size = New System.Drawing.Size(270, 20)
        Me.cmbTarget.TabIndex = 9
        '
        'LabelControl13
        '
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(74, 20)
        Me.LabelControl13.TabIndex = 10
        Me.LabelControl13.Text = "Target Type"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(2, 580)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(360, 56)
        Me.GroupControl6.TabIndex = 2
        Me.GroupControl6.Text = "Select Method"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.cmbMethod, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(356, 34)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'cmbMethod
        '
        Me.cmbMethod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbMethod.Location = New System.Drawing.Point(3, 4)
        Me.cmbMethod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.cmbMethod.Name = "cmbMethod"
        Me.cmbMethod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMethod.Size = New System.Drawing.Size(350, 20)
        Me.cmbMethod.TabIndex = 0
        '
        'tlpBottom
        '
        Me.tlpBottom.ColumnCount = 2
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.tlpBottom.Controls.Add(Me.lblMessage, 0, 0)
        Me.tlpBottom.Controls.Add(Me.btnAddKPI, 1, 0)
        Me.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpBottom.Location = New System.Drawing.Point(0, 638)
        Me.tlpBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpBottom.Name = "tlpBottom"
        Me.tlpBottom.RowCount = 1
        Me.tlpBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpBottom.Size = New System.Drawing.Size(364, 30)
        Me.tlpBottom.TabIndex = 3
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(288, 24)
        Me.lblMessage.TabIndex = 20
        '
        'btnAddKPI
        '
        Me.btnAddKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddKPI.Location = New System.Drawing.Point(296, 2)
        Me.btnAddKPI.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAddKPI.Name = "btnAddKPI"
        Me.btnAddKPI.Size = New System.Drawing.Size(66, 26)
        Me.btnAddKPI.TabIndex = 1
        Me.btnAddKPI.Text = "Add"
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'dlgAddKPIAlertMngr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(364, 668)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.Image = CType(resources.GetObject("dlgAddKPIAlertMngr.IconOptions.Image"), System.Drawing.Image)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(374, 700)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(366, 700)
        Me.Name = "dlgAddKPIAlertMngr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add KPI - Alert Manager"
        Me.tlpMain.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        CType(Me.txtSearckKPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstviewKPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel20.ResumeLayout(False)
        Me.TableLayoutPanel20.PerformLayout()
        CType(Me.cmbTechnology.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.TableLayoutPanel15.PerformLayout()
        CType(Me.cmbObject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        CType(Me.cmbInterval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel16.ResumeLayout(False)
        Me.TableLayoutPanel16.PerformLayout()
        CType(Me.cmbTarget.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.cmbMethod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpBottom.ResumeLayout(False)
        Me.tlpBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents txtSearckKPI As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents lstviewKPI As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
    Friend WithEvents cmbTechnology As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents cmbObject As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel16 As TableLayoutPanel
    Friend WithEvents cmbTarget As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents cmbMethod As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents btnAddKPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbInterval As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
End Class
