<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgMappingSelection
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
        Dim TreeListViewColumn1 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgMappingSelection))
        Dim TreeListViewColumn2 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn3 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Dim TreeListViewColumn4 As LidorSystems.IntegralUI.Lists.TreeListViewColumn = New LidorSystems.IntegralUI.Lists.TreeListViewColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblMsg = New DevExpress.XtraEditors.LabelControl()
        Me.tlvSelection = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.stateImages = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlvDetail = New LidorSystems.IntegralUI.Lists.TreeListView()
        Me.cm_TLV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmi_description = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_mapping = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_mapping_voronoi = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_mapping_label = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmi_Copy_Value_To_Clipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Copy_Table_To_Clipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Export_Table_To_Excel = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_Show_Only_Differences = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ceShowOnlyDifference = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbTemplate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtSelInfoSearch = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.tlvSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.tlvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cm_TLV.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.ceShowOnlyDifference.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSelInfoSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblMsg, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tlvSelection, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.ForeColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(384, 511)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LblMsg
        '
        Me.LblMsg.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LblMsg.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblMsg.Appearance.Options.UseFont = True
        Me.LblMsg.Appearance.Options.UseForeColor = True
        Me.LblMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMsg.Location = New System.Drawing.Point(3, 494)
        Me.LblMsg.Name = "LblMsg"
        Me.LblMsg.Size = New System.Drawing.Size(378, 14)
        Me.LblMsg.TabIndex = 10
        '
        'tlvSelection
        '
        TreeListViewColumn1.FooterRect = CType(resources.GetObject("TreeListViewColumn1.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.HeaderRect = CType(resources.GetObject("TreeListViewColumn1.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn1.HeaderText = "Cell ID"
        TreeListViewColumn1.Width = 55
        TreeListViewColumn2.FooterRect = CType(resources.GetObject("TreeListViewColumn2.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.HeaderRect = CType(resources.GetObject("TreeListViewColumn2.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn2.HeaderText = "Tech"
        TreeListViewColumn2.Width = 44
        TreeListViewColumn3.FooterRect = CType(resources.GetObject("TreeListViewColumn3.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn3.HeaderRect = CType(resources.GetObject("TreeListViewColumn3.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn3.HeaderText = "Sector Type"
        TreeListViewColumn3.Width = 79
        TreeListViewColumn4.FooterRect = CType(resources.GetObject("TreeListViewColumn4.FooterRect"), System.Drawing.RectangleF)
        TreeListViewColumn4.HeaderRect = CType(resources.GetObject("TreeListViewColumn4.HeaderRect"), System.Drawing.RectangleF)
        TreeListViewColumn4.HeaderText = "Source Table"
        TreeListViewColumn4.Width = 102
        Me.tlvSelection.Columns.AddRange(New Object() {TreeListViewColumn1, TreeListViewColumn2, TreeListViewColumn3, TreeListViewColumn4})
        '
        '
        '
        Me.tlvSelection.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvSelection.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvSelection.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvSelection.ContentPanel.Name = ""
        Me.tlvSelection.ContentPanel.Size = New System.Drawing.Size(370, 121)
        Me.tlvSelection.ContentPanel.TabIndex = 3
        Me.tlvSelection.ContentPanel.TabStop = False
        Me.tlvSelection.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvSelection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvSelection.ExpandingColumn = TreeListViewColumn1
        Me.tlvSelection.Footer = False
        Me.tlvSelection.Location = New System.Drawing.Point(4, 29)
        Me.tlvSelection.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvSelection.Name = "tlvSelection"
        Me.tlvSelection.NodeSpacing = 0
        Me.tlvSelection.ShowDropMarker = False
        Me.tlvSelection.ShowLines = False
        Me.tlvSelection.ShowPlusMinus = False
        Me.tlvSelection.ShowStateImages = True
        Me.tlvSelection.ShowToolTips = True
        Me.tlvSelection.Size = New System.Drawing.Size(376, 127)
        Me.tlvSelection.StateImageList = Me.stateImages
        Me.tlvSelection.TabIndex = 7
        Me.tlvSelection.Text = "TreeListView2"
        '
        'stateImages
        '
        Me.stateImages.ImageStream = CType(resources.GetObject("stateImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.stateImages.TransparentColor = System.Drawing.Color.Transparent
        Me.stateImages.Images.SetKeyName(0, "")
        Me.stateImages.Images.SetKeyName(1, "")
        Me.stateImages.Images.SetKeyName(2, "")
        Me.stateImages.Images.SetKeyName(3, "")
        Me.stateImages.Images.SetKeyName(4, "")
        Me.stateImages.Images.SetKeyName(5, "")
        Me.stateImages.Images.SetKeyName(6, "square_red.bmp")
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.tlvDetail, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 163)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(378, 325)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'tlvDetail
        '
        '
        '
        '
        Me.tlvDetail.ContentPanel.BackColor = System.Drawing.Color.Transparent
        Me.tlvDetail.ContentPanel.Location = New System.Drawing.Point(3, 3)
        Me.tlvDetail.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvDetail.ContentPanel.Name = ""
        Me.tlvDetail.ContentPanel.Size = New System.Drawing.Size(364, 256)
        Me.tlvDetail.ContentPanel.TabIndex = 3
        Me.tlvDetail.ContentPanel.TabStop = False
        Me.tlvDetail.ContextMenuStrip = Me.cm_TLV
        Me.tlvDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlvDetail.Footer = False
        Me.tlvDetail.Location = New System.Drawing.Point(4, 59)
        Me.tlvDetail.Margin = New System.Windows.Forms.Padding(4)
        Me.tlvDetail.Name = "tlvDetail"
        Me.tlvDetail.ShowStateImages = True
        Me.tlvDetail.ShowToolTips = True
        Me.tlvDetail.Size = New System.Drawing.Size(370, 262)
        Me.tlvDetail.StateImageList = Me.stateImages
        Me.tlvDetail.TabIndex = 6
        Me.tlvDetail.Text = "TreeListView1"
        '
        'cm_TLV
        '
        Me.cm_TLV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmi_description, Me.ToolStripSeparator2, Me.tsmi_mapping, Me.tsmi_mapping_voronoi, Me.tsmi_mapping_label, Me.ToolStripSeparator1, Me.tsmi_Copy_Value_To_Clipboard, Me.tsmi_Copy_Table_To_Clipboard, Me.tsmi_Export_Table_To_Excel, Me.tsmi_Show_Only_Differences})
        Me.cm_TLV.Name = "cm_TLV"
        Me.cm_TLV.Size = New System.Drawing.Size(232, 192)
        '
        'tsmi_description
        '
        Me.tsmi_description.Name = "tsmi_description"
        Me.tsmi_description.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_description.Text = "Parameter Description"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(228, 6)
        '
        'tsmi_mapping
        '
        Me.tsmi_mapping.Enabled = False
        Me.tsmi_mapping.Name = "tsmi_mapping"
        Me.tsmi_mapping.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_mapping.Text = "Parameter Mapping - Cells"
        '
        'tsmi_mapping_voronoi
        '
        Me.tsmi_mapping_voronoi.Enabled = False
        Me.tsmi_mapping_voronoi.Name = "tsmi_mapping_voronoi"
        Me.tsmi_mapping_voronoi.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_mapping_voronoi.Text = "Parameter Mapping - Voronoi"
        '
        'tsmi_mapping_label
        '
        Me.tsmi_mapping_label.Enabled = False
        Me.tsmi_mapping_label.Name = "tsmi_mapping_label"
        Me.tsmi_mapping_label.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_mapping_label.Text = "Parameter Mapping - Label"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(228, 6)
        '
        'tsmi_Copy_Value_To_Clipboard
        '
        Me.tsmi_Copy_Value_To_Clipboard.Name = "tsmi_Copy_Value_To_Clipboard"
        Me.tsmi_Copy_Value_To_Clipboard.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_Copy_Value_To_Clipboard.Text = "Copy Value to Clipboard"
        '
        'tsmi_Copy_Table_To_Clipboard
        '
        Me.tsmi_Copy_Table_To_Clipboard.Name = "tsmi_Copy_Table_To_Clipboard"
        Me.tsmi_Copy_Table_To_Clipboard.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_Copy_Table_To_Clipboard.Text = "Copy Table to Clipboard"
        '
        'tsmi_Export_Table_To_Excel
        '
        Me.tsmi_Export_Table_To_Excel.Enabled = False
        Me.tsmi_Export_Table_To_Excel.Name = "tsmi_Export_Table_To_Excel"
        Me.tsmi_Export_Table_To_Excel.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_Export_Table_To_Excel.Text = "Export Table to Excel"
        '
        'tsmi_Show_Only_Differences
        '
        Me.tsmi_Show_Only_Differences.Name = "tsmi_Show_Only_Differences"
        Me.tsmi_Show_Only_Differences.Size = New System.Drawing.Size(231, 22)
        Me.tsmi_Show_Only_Differences.Text = "Show Only Differences"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl3, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl4, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ceShowOnlyDifference, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbTemplate, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtSelInfoSearch, 2, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(374, 51)
        Me.TableLayoutPanel3.TabIndex = 7
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(138, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(57, 19)
        Me.LabelControl3.TabIndex = 11
        Me.LabelControl3.Text = "Template"
        '
        'LabelControl4
        '
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl4.Location = New System.Drawing.Point(138, 28)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(57, 20)
        Me.LabelControl4.TabIndex = 12
        Me.LabelControl4.Text = "Search"
        '
        'LabelControl2
        '
        Me.LabelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl2.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(129, 19)
        Me.LabelControl2.TabIndex = 10
        Me.LabelControl2.Text = "Selection Details"
        '
        'ceShowOnlyDifference
        '
        Me.ceShowOnlyDifference.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ceShowOnlyDifference.Location = New System.Drawing.Point(3, 28)
        Me.ceShowOnlyDifference.Name = "ceShowOnlyDifference"
        Me.ceShowOnlyDifference.Properties.Caption = "Show Only Difference"
        Me.ceShowOnlyDifference.Size = New System.Drawing.Size(129, 20)
        Me.ceShowOnlyDifference.TabIndex = 13
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTemplate.EditValue = ""
        Me.cmbTemplate.Location = New System.Drawing.Point(201, 3)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTemplate.Properties.NullText = "Select Item..."
        Me.cmbTemplate.Size = New System.Drawing.Size(170, 20)
        Me.cmbTemplate.TabIndex = 16
        '
        'txtSelInfoSearch
        '
        Me.txtSelInfoSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSelInfoSearch.Location = New System.Drawing.Point(201, 28)
        Me.txtSelInfoSearch.Name = "txtSelInfoSearch"
        Me.txtSelInfoSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)})
        Me.txtSelInfoSearch.Properties.NullValuePrompt = "Search..."
        Me.txtSelInfoSearch.Size = New System.Drawing.Size(170, 20)
        Me.txtSelInfoSearch.TabIndex = 15
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(378, 19)
        Me.LabelControl1.TabIndex = 9
        Me.LabelControl1.Text = "Selected Items"
        '
        'dlgMappingSelection
        '
        Me.ClientSize = New System.Drawing.Size(384, 511)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgMappingSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Selection Info"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.tlvSelection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.tlvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cm_TLV.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.ceShowOnlyDifference.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSelInfoSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlvSelection As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlvDetail As LidorSystems.IntegralUI.Lists.TreeListView
    Friend WithEvents LblMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ceShowOnlyDifference As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents stateImages As System.Windows.Forms.ImageList
    Friend WithEvents cm_TLV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi_description As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_mapping As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_mapping_voronoi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_mapping_label As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmi_Copy_Table_To_Clipboard As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_Export_Table_To_Excel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi_Show_Only_Differences As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbTemplate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtSelInfoSearch As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents tsmi_Copy_Value_To_Clipboard As ToolStripMenuItem
End Class
