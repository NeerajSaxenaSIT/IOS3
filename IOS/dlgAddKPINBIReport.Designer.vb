<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgAddKPINBIReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgAddKPINBIReport))
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSearckKPI = New DevExpress.XtraEditors.ButtonEdit()
        Me.lstviewKPI = New DevExpress.XtraTreeList.TreeList()
        Me.cmKPITreeList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_PasteKPI = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_ViewCheckedItems = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_UncheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTechnology = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.lblObjectType = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.txtSearckKPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstviewKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmKPITreeList.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel15.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblMessage, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.btnAdd, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupControl5, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(334, 461)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'lblMessage
        '
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.lblMessage, 2)
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Location = New System.Drawing.Point(3, 434)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMessage.Size = New System.Drawing.Size(328, 24)
        Me.lblMessage.TabIndex = 13
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAdd.Location = New System.Drawing.Point(3, 399)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(328, 29)
        Me.btnAdd.TabIndex = 11
        Me.btnAdd.Text = "Add"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.TableLayoutPanel11)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(330, 392)
        Me.GroupControl5.TabIndex = 0
        Me.GroupControl5.Text = "Search And Add KPI"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 1
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.txtSearckKPI, 0, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.lstviewKPI, 0, 3)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel10, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel15, 0, 1)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(2, 23)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 4
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(326, 367)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'txtSearckKPI
        '
        Me.txtSearckKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearckKPI.Location = New System.Drawing.Point(3, 55)
        Me.txtSearckKPI.Name = "txtSearckKPI"
        Me.txtSearckKPI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSearckKPI.Properties.NullValuePrompt = "Search..."
        Me.txtSearckKPI.Size = New System.Drawing.Size(320, 20)
        Me.txtSearckKPI.TabIndex = 5
        '
        'lstviewKPI
        '
        Me.lstviewKPI.ContextMenuStrip = Me.cmKPITreeList
        Me.lstviewKPI.CustomizationFormBounds = New System.Drawing.Rectangle(1116, 239, 250, 200)
        Me.lstviewKPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstviewKPI.Location = New System.Drawing.Point(3, 80)
        Me.lstviewKPI.Name = "lstviewKPI"
        Me.lstviewKPI.OptionsBehavior.Editable = False
        Me.lstviewKPI.OptionsBehavior.ReadOnly = True
        Me.lstviewKPI.OptionsCustomization.AllowSort = False
        Me.lstviewKPI.OptionsMenu.EnableColumnMenu = False
        Me.lstviewKPI.OptionsMenu.EnableFooterMenu = False
        Me.lstviewKPI.OptionsMenu.ShowAutoFilterRowItem = False
        Me.lstviewKPI.OptionsMenu.ShowExpandCollapseItems = False
        Me.lstviewKPI.OptionsNavigation.MoveOnEdit = False
        Me.lstviewKPI.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.lstviewKPI.OptionsSelection.MultiSelect = True
        Me.lstviewKPI.OptionsView.BestFitMode = DevExpress.XtraTreeList.TreeListBestFitMode.Full
        Me.lstviewKPI.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.All
        Me.lstviewKPI.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check
        Me.lstviewKPI.OptionsView.ExpandButtonCentered = False
        Me.lstviewKPI.OptionsView.ShowButtons = False
        Me.lstviewKPI.OptionsView.ShowHorzLines = False
        Me.lstviewKPI.OptionsView.ShowIndicator = False
        Me.lstviewKPI.OptionsView.ShowRoot = False
        Me.lstviewKPI.Size = New System.Drawing.Size(320, 284)
        Me.lstviewKPI.TabIndex = 7
        '
        'cmKPITreeList
        '
        Me.cmKPITreeList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_PasteKPI, Me.tsmi_ViewCheckedItems, Me.tsmi_UncheckAll})
        Me.cmKPITreeList.Name = "cmsConfigurationSummary"
        Me.cmKPITreeList.Size = New System.Drawing.Size(181, 70)
        '
        'tsmi_PasteKPI
        '
        Me.tsmi_PasteKPI.Name = "tsmi_PasteKPI"
        Me.tsmi_PasteKPI.Size = New System.Drawing.Size(180, 22)
        Me.tsmi_PasteKPI.Text = "Paste KPIs"
        '
        'tsmi_ViewCheckedItems
        '
        Me.tsmi_ViewCheckedItems.Name = "tsmi_ViewCheckedItems"
        Me.tsmi_ViewCheckedItems.Size = New System.Drawing.Size(180, 22)
        Me.tsmi_ViewCheckedItems.Text = "View Checked Items"
        '
        'tsmi_UncheckAll
        '
        Me.tsmi_UncheckAll.Name = "tsmi_UncheckAll"
        Me.tsmi_UncheckAll.Size = New System.Drawing.Size(180, 22)
        Me.tsmi_UncheckAll.Text = "Uncheck All"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 3
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.LabelControl11, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.lblTechnology, 2, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(326, 26)
        Me.TableLayoutPanel10.TabIndex = 10
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(93, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(4, 20)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = ":"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl11.Appearance.Options.UseForeColor = True
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(84, 20)
        Me.LabelControl11.TabIndex = 4
        Me.LabelControl11.Text = "Technology"
        '
        'lblTechnology
        '
        Me.lblTechnology.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblTechnology.Appearance.Options.UseForeColor = True
        Me.lblTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTechnology.Location = New System.Drawing.Point(103, 3)
        Me.lblTechnology.Name = "lblTechnology"
        Me.lblTechnology.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblTechnology.Size = New System.Drawing.Size(220, 20)
        Me.lblTechnology.TabIndex = 5
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 3
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.LabelControl2, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.LabelControl12, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.lblObjectType, 2, 0)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 1
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(326, 26)
        Me.TableLayoutPanel15.TabIndex = 11
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(93, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(4, 20)
        Me.LabelControl2.TabIndex = 7
        Me.LabelControl2.Text = ":"
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.LabelControl12.Appearance.Options.UseForeColor = True
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(84, 20)
        Me.LabelControl12.TabIndex = 5
        Me.LabelControl12.Text = "Object Type"
        '
        'lblObjectType
        '
        Me.lblObjectType.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.lblObjectType.Appearance.Options.UseForeColor = True
        Me.lblObjectType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblObjectType.Location = New System.Drawing.Point(103, 3)
        Me.lblObjectType.Name = "lblObjectType"
        Me.lblObjectType.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblObjectType.Size = New System.Drawing.Size(220, 20)
        Me.lblObjectType.TabIndex = 6
        '
        'Timer1
        '
        Me.Timer1.Interval = 6000
        '
        'dlgAddKPINBIReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 461)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(350, 500)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(350, 500)
        Me.Name = "dlgAddKPINBIReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NBI Report - Add KPI "
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        CType(Me.txtSearckKPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstviewKPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmKPITreeList.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.TableLayoutPanel15.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents txtSearckKPI As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents lstviewKPI As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTechnology As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblObjectType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmKPITreeList As ContextMenuStrip
    Friend WithEvents tsmi_PasteKPI As ToolStripMenuItem
    Friend WithEvents tsmi_UncheckAll As ToolStripMenuItem
    Friend WithEvents tsmi_ViewCheckedItems As ToolStripMenuItem
End Class
