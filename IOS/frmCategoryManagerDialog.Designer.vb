<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCategoryManagerDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCategoryManagerDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMsg = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.vlblEndDate = New DevExpress.XtraEditors.LabelControl()
        Me.dtpDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.vlblStartDate = New DevExpress.XtraEditors.LabelControl()
        Me.dtpStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.rbBtnSaveSchedule = New System.Windows.Forms.RadioButton()
        Me.rbBtnSaveNow = New System.Windows.Forms.RadioButton()
        Me.rbBtnSaveWithRollback = New System.Windows.Forms.RadioButton()
        Me.cmbCategoryList = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnApplyToSelected = New DevExpress.XtraEditors.SimpleButton()
        Me.btnApply = New DevExpress.XtraEditors.SimpleButton()
        Me.btnApplyToHighLight = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.dtpDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.dtpStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.cmbCategoryList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblMsg, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(759, 236)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblMsg
        '
        Me.lblMsg.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsg.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMsg.Location = New System.Drawing.Point(3, 166)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblMsg.Size = New System.Drawing.Size(753, 31)
        Me.lblMsg.TabIndex = 4
        Me.lblMsg.Text = "Label"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.vlblEndDate, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.dtpDateEnd, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2, 98)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(755, 29)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'vlblEndDate
        '
        Me.vlblEndDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vlblEndDate.Location = New System.Drawing.Point(3, 3)
        Me.vlblEndDate.Name = "vlblEndDate"
        Me.vlblEndDate.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.vlblEndDate.Size = New System.Drawing.Size(159, 23)
        Me.vlblEndDate.TabIndex = 1
        Me.vlblEndDate.Text = "End Date"
        '
        'dtpDateEnd
        '
        Me.dtpDateEnd.EditValue = "2013/02/25 04:08:28"
        Me.dtpDateEnd.Location = New System.Drawing.Point(168, 3)
        Me.dtpDateEnd.Name = "dtpDateEnd"
        Me.dtpDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpDateEnd.Properties.DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss"
        Me.dtpDateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpDateEnd.Size = New System.Drawing.Size(193, 20)
        Me.dtpDateEnd.TabIndex = 2
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.vlblStartDate, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.dtpStartDate, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 66)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(755, 28)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'vlblStartDate
        '
        Me.vlblStartDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vlblStartDate.Location = New System.Drawing.Point(3, 3)
        Me.vlblStartDate.Name = "vlblStartDate"
        Me.vlblStartDate.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.vlblStartDate.Size = New System.Drawing.Size(159, 22)
        Me.vlblStartDate.TabIndex = 1
        Me.vlblStartDate.Text = "Start Date"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.EditValue = "2013/02/25 04:08:00"
        Me.dtpStartDate.Location = New System.Drawing.Point(168, 3)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpStartDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss"
        Me.dtpStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpStartDate.Size = New System.Drawing.Size(193, 20)
        Me.dtpStartDate.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel6, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbCategoryList, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(755, 60)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(159, 21)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Category List"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.rbBtnSaveSchedule, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.rbBtnSaveNow, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.rbBtnSaveWithRollback, 2, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(167, 29)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(586, 29)
        Me.TableLayoutPanel6.TabIndex = 1
        '
        'rbBtnSaveSchedule
        '
        Me.rbBtnSaveSchedule.AutoSize = True
        Me.rbBtnSaveSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbBtnSaveSchedule.Location = New System.Drawing.Point(196, 3)
        Me.rbBtnSaveSchedule.Name = "rbBtnSaveSchedule"
        Me.rbBtnSaveSchedule.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.rbBtnSaveSchedule.Size = New System.Drawing.Size(187, 23)
        Me.rbBtnSaveSchedule.TabIndex = 1
        Me.rbBtnSaveSchedule.TabStop = True
        Me.rbBtnSaveSchedule.Text = "Save Schedule"
        Me.rbBtnSaveSchedule.UseVisualStyleBackColor = True
        '
        'rbBtnSaveNow
        '
        Me.rbBtnSaveNow.AutoSize = True
        Me.rbBtnSaveNow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbBtnSaveNow.Location = New System.Drawing.Point(3, 3)
        Me.rbBtnSaveNow.Name = "rbBtnSaveNow"
        Me.rbBtnSaveNow.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.rbBtnSaveNow.Size = New System.Drawing.Size(187, 23)
        Me.rbBtnSaveNow.TabIndex = 0
        Me.rbBtnSaveNow.TabStop = True
        Me.rbBtnSaveNow.Text = "Save Now"
        Me.rbBtnSaveNow.UseVisualStyleBackColor = True
        '
        'rbBtnSaveWithRollback
        '
        Me.rbBtnSaveWithRollback.AutoSize = True
        Me.rbBtnSaveWithRollback.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbBtnSaveWithRollback.Location = New System.Drawing.Point(389, 3)
        Me.rbBtnSaveWithRollback.Name = "rbBtnSaveWithRollback"
        Me.rbBtnSaveWithRollback.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.rbBtnSaveWithRollback.Size = New System.Drawing.Size(194, 23)
        Me.rbBtnSaveWithRollback.TabIndex = 2
        Me.rbBtnSaveWithRollback.TabStop = True
        Me.rbBtnSaveWithRollback.Text = "Save Schedule With Rollback"
        Me.rbBtnSaveWithRollback.UseVisualStyleBackColor = True
        '
        'cmbCategoryList
        '
        Me.cmbCategoryList.Location = New System.Drawing.Point(168, 3)
        Me.cmbCategoryList.Name = "cmbCategoryList"
        Me.cmbCategoryList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCategoryList.Size = New System.Drawing.Size(193, 20)
        Me.cmbCategoryList.TabIndex = 2
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnApplyToSelected, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnApply, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnApplyToHighLight, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnCancel, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 131)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(755, 30)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'btnApplyToSelected
        '
        Me.btnApplyToSelected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnApplyToSelected.Location = New System.Drawing.Point(3, 3)
        Me.btnApplyToSelected.Name = "btnApplyToSelected"
        Me.btnApplyToSelected.Size = New System.Drawing.Size(182, 24)
        Me.btnApplyToSelected.TabIndex = 0
        Me.btnApplyToSelected.Text = "Apply to Tree Selection"
        '
        'btnApply
        '
        Me.btnApply.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnApply.Location = New System.Drawing.Point(191, 3)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(182, 24)
        Me.btnApply.TabIndex = 1
        Me.btnApply.Text = "Apply To Grid"
        '
        'btnApplyToHighLight
        '
        Me.btnApplyToHighLight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnApplyToHighLight.Location = New System.Drawing.Point(379, 3)
        Me.btnApplyToHighLight.Name = "btnApplyToHighLight"
        Me.btnApplyToHighLight.Size = New System.Drawing.Size(182, 24)
        Me.btnApplyToHighLight.TabIndex = 2
        Me.btnApplyToHighLight.Text = "Apply to Highlights"
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(567, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(185, 24)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'frmCategoryManagerDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 236)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Seven"
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(775, 274)
        Me.Name = "frmCategoryManagerDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Category"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.dtpDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.dtpStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.cmbCategoryList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vlblEndDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents vlblStartDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents rbBtnSaveSchedule As System.Windows.Forms.RadioButton
    Friend WithEvents rbBtnSaveNow As System.Windows.Forms.RadioButton
    Friend WithEvents rbBtnSaveWithRollback As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCategoryList As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnApplyToSelected As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnApplyToHighLight As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
