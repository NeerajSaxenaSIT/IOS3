<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNotes))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTechnology = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblObjectType = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.lblObjectsSelected = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lvNoteObjects = New System.Windows.Forms.ListView()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtLongDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.txtShortDescription = New DevExpress.XtraEditors.TextEdit()
        Me.deChangeDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblUserName = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbChangeType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbNoteDepartment = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnSubmit = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.txtLongDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShortDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deChangeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deChangeDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbChangeType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbNoteDepartment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSubmit, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(584, 531)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(578, 242)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Current Object Selection"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(574, 220)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.86926!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.13074!))
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.lblTechnology, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl3, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.lblObjectType, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelControl5, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.lblObjectsSelected, 1, 2)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 5
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(283, 216)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(104, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Technology"
        '
        'lblTechnology
        '
        Me.lblTechnology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTechnology.Location = New System.Drawing.Point(113, 3)
        Me.lblTechnology.Name = "lblTechnology"
        Me.lblTechnology.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblTechnology.Size = New System.Drawing.Size(167, 22)
        Me.lblTechnology.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(104, 22)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Object Type"
        '
        'lblObjectType
        '
        Me.lblObjectType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblObjectType.Location = New System.Drawing.Point(113, 31)
        Me.lblObjectType.Name = "lblObjectType"
        Me.lblObjectType.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblObjectType.Size = New System.Drawing.Size(167, 22)
        Me.lblObjectType.TabIndex = 3
        '
        'LabelControl5
        '
        Me.LabelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl5.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(104, 22)
        Me.LabelControl5.TabIndex = 4
        Me.LabelControl5.Text = "Object Selected"
        '
        'lblObjectsSelected
        '
        Me.lblObjectsSelected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblObjectsSelected.Location = New System.Drawing.Point(113, 59)
        Me.lblObjectsSelected.Name = "lblObjectsSelected"
        Me.lblObjectsSelected.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblObjectsSelected.Size = New System.Drawing.Size(167, 22)
        Me.lblObjectsSelected.TabIndex = 5
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.lvNoteObjects, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.LabelControl7, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(289, 2)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(283, 216)
        Me.TableLayoutPanel5.TabIndex = 1
        '
        'lvNoteObjects
        '
        Me.lvNoteObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvNoteObjects.Location = New System.Drawing.Point(3, 31)
        Me.lvNoteObjects.Name = "lvNoteObjects"
        Me.lvNoteObjects.Size = New System.Drawing.Size(277, 182)
        Me.lvNoteObjects.TabIndex = 0
        Me.lvNoteObjects.UseCompatibleStateImageBehavior = False
        '
        'LabelControl7
        '
        Me.LabelControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl7.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(277, 22)
        Me.LabelControl7.TabIndex = 1
        Me.LabelControl7.Text = "Objects Affected"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(3, 251)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(578, 242)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Note Details"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.txtLongDescription, 1, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.txtShortDescription, 1, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.deChangeDate, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl8, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblUserName, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl10, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl11, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl12, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl13, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelControl14, 0, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbChangeType, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbNoteDepartment, 1, 3)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 20)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 6
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(574, 220)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'txtLongDescription
        '
        Me.txtLongDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLongDescription.Location = New System.Drawing.Point(146, 143)
        Me.txtLongDescription.Name = "txtLongDescription"
        Me.txtLongDescription.Size = New System.Drawing.Size(425, 74)
        Me.txtLongDescription.TabIndex = 0
        '
        'txtShortDescription
        '
        Me.txtShortDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtShortDescription.Location = New System.Drawing.Point(146, 115)
        Me.txtShortDescription.Name = "txtShortDescription"
        Me.txtShortDescription.Size = New System.Drawing.Size(425, 20)
        Me.txtShortDescription.TabIndex = 1
        '
        'deChangeDate
        '
        Me.deChangeDate.EditValue = "05-02-2013 13:59"
        Me.deChangeDate.Location = New System.Drawing.Point(146, 3)
        Me.deChangeDate.Name = "deChangeDate"
        Me.deChangeDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deChangeDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.deChangeDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deChangeDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.deChangeDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.deChangeDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deChangeDate.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.deChangeDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.deChangeDate.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.deChangeDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.deChangeDate.Size = New System.Drawing.Size(246, 20)
        Me.deChangeDate.TabIndex = 4
        '
        'LabelControl8
        '
        Me.LabelControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl8.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl8.Size = New System.Drawing.Size(137, 22)
        Me.LabelControl8.TabIndex = 5
        Me.LabelControl8.Text = "Date Of Change"
        '
        'lblUserName
        '
        Me.lblUserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUserName.Location = New System.Drawing.Point(146, 31)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblUserName.Size = New System.Drawing.Size(425, 22)
        Me.lblUserName.TabIndex = 6
        '
        'LabelControl10
        '
        Me.LabelControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl10.Location = New System.Drawing.Point(3, 31)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl10.Size = New System.Drawing.Size(137, 22)
        Me.LabelControl10.TabIndex = 7
        Me.LabelControl10.Text = "User Name"
        '
        'LabelControl11
        '
        Me.LabelControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl11.Location = New System.Drawing.Point(3, 59)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl11.Size = New System.Drawing.Size(137, 22)
        Me.LabelControl11.TabIndex = 8
        Me.LabelControl11.Text = "Type Of Change"
        '
        'LabelControl12
        '
        Me.LabelControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl12.Location = New System.Drawing.Point(3, 87)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl12.Size = New System.Drawing.Size(137, 22)
        Me.LabelControl12.TabIndex = 9
        Me.LabelControl12.Text = "Department"
        '
        'LabelControl13
        '
        Me.LabelControl13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl13.Location = New System.Drawing.Point(3, 115)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl13.Size = New System.Drawing.Size(137, 22)
        Me.LabelControl13.TabIndex = 10
        Me.LabelControl13.Text = "Short Description"
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl14.Location = New System.Drawing.Point(3, 146)
        Me.LabelControl14.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LabelControl14.Size = New System.Drawing.Size(137, 71)
        Me.LabelControl14.TabIndex = 11
        Me.LabelControl14.Text = "Long Description"
        '
        'cmbChangeType
        '
        Me.cmbChangeType.Location = New System.Drawing.Point(146, 59)
        Me.cmbChangeType.Name = "cmbChangeType"
        Me.cmbChangeType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbChangeType.Size = New System.Drawing.Size(246, 20)
        Me.cmbChangeType.TabIndex = 14
        '
        'cmbNoteDepartment
        '
        Me.cmbNoteDepartment.Location = New System.Drawing.Point(146, 87)
        Me.cmbNoteDepartment.Name = "cmbNoteDepartment"
        Me.cmbNoteDepartment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbNoteDepartment.Size = New System.Drawing.Size(246, 20)
        Me.cmbNoteDepartment.TabIndex = 14
        '
        'btnSubmit
        '
        Me.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSubmit.Location = New System.Drawing.Point(470, 499)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(111, 29)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        '
        'frmNotes
        '
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 531)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.LookAndFeel.SkinName = "Seven"
        Me.MinimumSize = New System.Drawing.Size(600, 569)
        Me.Name = "frmNotes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Notes"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.txtLongDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShortDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deChangeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deChangeDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbChangeType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbNoteDepartment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTechnology As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblObjectType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblObjectsSelected As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lvNoteObjects As System.Windows.Forms.ListView
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtLongDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtShortDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents deChangeDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUserName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSubmit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbChangeType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbNoteDepartment As DevExpress.XtraEditors.ComboBoxEdit
End Class
