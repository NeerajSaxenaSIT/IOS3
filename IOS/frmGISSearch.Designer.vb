<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGISSearch
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGISSearch))
        Me.IosTableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.IosTableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTable = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbField = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbValue = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.IosTableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSearchGIS = New DevExpress.XtraEditors.SimpleButton()
        Me.gcMapTableData = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lblMsg = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFieldSearch = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtFieldSearch = New DevExpress.XtraEditors.ButtonEdit()
        Me.IosTableLayoutPanel1.SuspendLayout()
        Me.IosTableLayoutPanel2.SuspendLayout()
        CType(Me.cmbTable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.IosTableLayoutPanel3.SuspendLayout()
        CType(Me.gcMapTableData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.cmbFieldSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IosTableLayoutPanel1
        '
        Me.IosTableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.IosTableLayoutPanel1.ColumnCount = 1
        Me.IosTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel1.Controls.Add(Me.IosTableLayoutPanel2, 0, 0)
        Me.IosTableLayoutPanel1.Controls.Add(Me.IosTableLayoutPanel3, 0, 1)
        Me.IosTableLayoutPanel1.Controls.Add(Me.gcMapTableData, 0, 2)
        Me.IosTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel1.Location = New System.Drawing.Point(2, 20)
        Me.IosTableLayoutPanel1.Name = "IosTableLayoutPanel1"
        Me.IosTableLayoutPanel1.RowCount = 3
        Me.IosTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.IosTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.IosTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel1.Size = New System.Drawing.Size(745, 453)
        Me.IosTableLayoutPanel1.TabIndex = 0
        '
        'IosTableLayoutPanel2
        '
        Me.IosTableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.IosTableLayoutPanel2.ColumnCount = 3
        Me.IosTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.IosTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.IosTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.IosTableLayoutPanel2.Controls.Add(Me.LabelControl3, 2, 0)
        Me.IosTableLayoutPanel2.Controls.Add(Me.LabelControl2, 1, 0)
        Me.IosTableLayoutPanel2.Controls.Add(Me.LabelControl1, 0, 0)
        Me.IosTableLayoutPanel2.Controls.Add(Me.cmbTable, 0, 1)
        Me.IosTableLayoutPanel2.Controls.Add(Me.cmbField, 1, 1)
        Me.IosTableLayoutPanel2.Controls.Add(Me.cmbValue, 2, 1)
        Me.IosTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.IosTableLayoutPanel2.Name = "IosTableLayoutPanel2"
        Me.IosTableLayoutPanel2.RowCount = 2
        Me.IosTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.IosTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.IosTableLayoutPanel2.Size = New System.Drawing.Size(739, 56)
        Me.IosTableLayoutPanel2.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(489, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(247, 22)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Set Value"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(246, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(237, 22)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Select Field"
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(237, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Select Table"
        '
        'cmbTable
        '
        Me.cmbTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTable.EditValue = "Select Item..."
        Me.cmbTable.Location = New System.Drawing.Point(3, 31)
        Me.cmbTable.Name = "cmbTable"
        Me.cmbTable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTable.Size = New System.Drawing.Size(237, 20)
        Me.cmbTable.TabIndex = 3
        '
        'cmbField
        '
        Me.cmbField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbField.EditValue = "Select Item..."
        Me.cmbField.Location = New System.Drawing.Point(246, 31)
        Me.cmbField.Name = "cmbField"
        Me.cmbField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbField.Size = New System.Drawing.Size(237, 20)
        Me.cmbField.TabIndex = 4
        '
        'cmbValue
        '
        Me.cmbValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbValue.EditValue = "Select Item..."
        Me.cmbValue.Location = New System.Drawing.Point(489, 31)
        Me.cmbValue.Name = "cmbValue"
        Me.cmbValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbValue.Size = New System.Drawing.Size(247, 20)
        Me.cmbValue.TabIndex = 5
        '
        'IosTableLayoutPanel3
        '
        Me.IosTableLayoutPanel3.BackColor = System.Drawing.Color.Transparent
        Me.IosTableLayoutPanel3.ColumnCount = 2
        Me.IosTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.IosTableLayoutPanel3.Controls.Add(Me.btnSearchGIS, 1, 0)
        Me.IosTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IosTableLayoutPanel3.Location = New System.Drawing.Point(3, 65)
        Me.IosTableLayoutPanel3.Name = "IosTableLayoutPanel3"
        Me.IosTableLayoutPanel3.RowCount = 1
        Me.IosTableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.IosTableLayoutPanel3.Size = New System.Drawing.Size(739, 33)
        Me.IosTableLayoutPanel3.TabIndex = 1
        '
        'btnSearchGIS
        '
        Me.btnSearchGIS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSearchGIS.Location = New System.Drawing.Point(592, 3)
        Me.btnSearchGIS.Name = "btnSearchGIS"
        Me.btnSearchGIS.Size = New System.Drawing.Size(144, 27)
        Me.btnSearchGIS.TabIndex = 0
        Me.btnSearchGIS.Text = "Search GIS"
        '
        'gcMapTableData
        '
        Me.gcMapTableData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcMapTableData.Location = New System.Drawing.Point(3, 104)
        Me.gcMapTableData.MainView = Me.GridView1
        Me.gcMapTableData.Name = "gcMapTableData"
        Me.gcMapTableData.Size = New System.Drawing.Size(739, 346)
        Me.gcMapTableData.TabIndex = 2
        Me.gcMapTableData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcMapTableData
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'lblMsg
        '
        Me.lblMsg.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMsg.Location = New System.Drawing.Point(3, 559)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMsg.Size = New System.Drawing.Size(749, 21)
        Me.lblMsg.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblMsg, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.53791!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.4621!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(755, 583)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.IosTableLayoutPanel1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 78)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(749, 475)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Search in a specific table"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(749, 69)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Search in all visible cell tables"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 5
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 197.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 288.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl4, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbFieldSearch, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl5, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtFieldSearch, 3, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(745, 47)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(3, 10)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 8)
        Me.LabelControl4.Size = New System.Drawing.Size(44, 27)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Field"
        '
        'cmbFieldSearch
        '
        Me.cmbFieldSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbFieldSearch.Location = New System.Drawing.Point(53, 10)
        Me.cmbFieldSearch.Name = "cmbFieldSearch"
        Me.cmbFieldSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFieldSearch.Size = New System.Drawing.Size(191, 20)
        Me.cmbFieldSearch.TabIndex = 24
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(250, 10)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(4, 0, 0, 8)
        Me.LabelControl5.Size = New System.Drawing.Size(51, 27)
        Me.LabelControl5.TabIndex = 25
        Me.LabelControl5.Text = "Set Value"
        '
        'txtFieldSearch
        '
        Me.txtFieldSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFieldSearch.Location = New System.Drawing.Point(307, 10)
        Me.txtFieldSearch.Name = "txtFieldSearch"
        ToolTipItem1.Text = "Enter search text and click search icon."
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.txtFieldSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, SuperToolTip1, True)})
        Me.txtFieldSearch.Properties.NullValuePrompt = "Search..."
        Me.txtFieldSearch.Size = New System.Drawing.Size(282, 20)
        Me.txtFieldSearch.TabIndex = 26
        '
        'frmGISSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 583)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Office 2013"
        Me.MinimizeBox = False
        Me.Name = "frmGISSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GIS Search"
        Me.IosTableLayoutPanel1.ResumeLayout(False)
        Me.IosTableLayoutPanel2.ResumeLayout(False)
        Me.IosTableLayoutPanel2.PerformLayout()
        CType(Me.cmbTable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.IosTableLayoutPanel3.ResumeLayout(False)
        CType(Me.gcMapTableData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.cmbFieldSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IosTableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents IosTableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTable As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbField As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbValue As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents IosTableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents btnSearchGIS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gcMapTableData As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbFieldSearch As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtFieldSearch As DevExpress.XtraEditors.ButtonEdit
End Class
