<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgJobsAdd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgJobsAdd))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.rbMonthly = New System.Windows.Forms.RadioButton()
        Me.rbWeekly = New System.Windows.Forms.RadioButton()
        Me.rbDaily = New System.Windows.Forms.RadioButton()
        Me.rbHourly = New System.Windows.Forms.RadioButton()
        Me.rb15Mins = New System.Windows.Forms.RadioButton()
        Me.chkJobActive = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.txtJobProtectionLimit = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtTimeout = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.dtpJob = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtJobDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtJobName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcQueries = New DevExpress.XtraGrid.GridControl()
        Me.gvQueries = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkJobActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJobProtectionLimit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTimeout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpJob.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpJob.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJobDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJobName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.gcQueries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvQueries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 560.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.ForeColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1057, 371)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupControl1)
        Me.Panel2.Controls.Add(Me.chkJobActive)
        Me.Panel2.Controls.Add(Me.LabelControl7)
        Me.Panel2.Controls.Add(Me.txtJobProtectionLimit)
        Me.Panel2.Controls.Add(Me.LabelControl6)
        Me.Panel2.Controls.Add(Me.LabelControl5)
        Me.Panel2.Controls.Add(Me.txtTimeout)
        Me.Panel2.Controls.Add(Me.LabelControl4)
        Me.Panel2.Controls.Add(Me.dtpJob)
        Me.Panel2.Controls.Add(Me.LabelControl2)
        Me.Panel2.Controls.Add(Me.txtJobDescription)
        Me.Panel2.Controls.Add(Me.LabelControl1)
        Me.Panel2.Controls.Add(Me.txtJobName)
        Me.Panel2.Controls.Add(Me.LabelControl9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(554, 324)
        Me.Panel2.TabIndex = 5
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.rbMonthly)
        Me.GroupControl1.Controls.Add(Me.rbWeekly)
        Me.GroupControl1.Controls.Add(Me.rbDaily)
        Me.GroupControl1.Controls.Add(Me.rbHourly)
        Me.GroupControl1.Controls.Add(Me.rb15Mins)
        Me.GroupControl1.Location = New System.Drawing.Point(345, 143)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(200, 170)
        Me.GroupControl1.TabIndex = 19
        Me.GroupControl1.Text = "Interval"
        '
        'rbMonthly
        '
        Me.rbMonthly.AutoSize = True
        Me.rbMonthly.Location = New System.Drawing.Point(31, 141)
        Me.rbMonthly.Name = "rbMonthly"
        Me.rbMonthly.Size = New System.Drawing.Size(63, 17)
        Me.rbMonthly.TabIndex = 4
        Me.rbMonthly.TabStop = True
        Me.rbMonthly.Text = "Monthly"
        Me.rbMonthly.UseVisualStyleBackColor = True
        '
        'rbWeekly
        '
        Me.rbWeekly.AutoSize = True
        Me.rbWeekly.Location = New System.Drawing.Point(31, 112)
        Me.rbWeekly.Name = "rbWeekly"
        Me.rbWeekly.Size = New System.Drawing.Size(60, 17)
        Me.rbWeekly.TabIndex = 3
        Me.rbWeekly.TabStop = True
        Me.rbWeekly.Text = "Weekly"
        Me.rbWeekly.UseVisualStyleBackColor = True
        '
        'rbDaily
        '
        Me.rbDaily.AutoSize = True
        Me.rbDaily.Location = New System.Drawing.Point(31, 83)
        Me.rbDaily.Name = "rbDaily"
        Me.rbDaily.Size = New System.Drawing.Size(48, 17)
        Me.rbDaily.TabIndex = 2
        Me.rbDaily.TabStop = True
        Me.rbDaily.Text = "Daily"
        Me.rbDaily.UseVisualStyleBackColor = True
        '
        'rbHourly
        '
        Me.rbHourly.AutoSize = True
        Me.rbHourly.Location = New System.Drawing.Point(31, 54)
        Me.rbHourly.Name = "rbHourly"
        Me.rbHourly.Size = New System.Drawing.Size(56, 17)
        Me.rbHourly.TabIndex = 1
        Me.rbHourly.TabStop = True
        Me.rbHourly.Text = "Hourly"
        Me.rbHourly.UseVisualStyleBackColor = True
        '
        'rb15Mins
        '
        Me.rb15Mins.AutoSize = True
        Me.rb15Mins.Location = New System.Drawing.Point(31, 25)
        Me.rb15Mins.Name = "rb15Mins"
        Me.rb15Mins.Size = New System.Drawing.Size(77, 17)
        Me.rb15Mins.TabIndex = 0
        Me.rb15Mins.TabStop = True
        Me.rb15Mins.Text = "15 Minutes"
        Me.rb15Mins.UseVisualStyleBackColor = True
        '
        'chkJobActive
        '
        Me.chkJobActive.Location = New System.Drawing.Point(122, 233)
        Me.chkJobActive.Name = "chkJobActive"
        Me.chkJobActive.Properties.Caption = ""
        Me.chkJobActive.Size = New System.Drawing.Size(23, 19)
        Me.chkJobActive.TabIndex = 18
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(20, 236)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl7.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl7.TabIndex = 17
        Me.LabelControl7.Text = "Job Active"
        '
        'txtJobProtectionLimit
        '
        Me.txtJobProtectionLimit.Location = New System.Drawing.Point(122, 203)
        Me.txtJobProtectionLimit.Name = "txtJobProtectionLimit"
        Me.txtJobProtectionLimit.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobProtectionLimit.Properties.Appearance.Options.UseFont = True
        Me.txtJobProtectionLimit.Size = New System.Drawing.Size(140, 22)
        Me.txtJobProtectionLimit.TabIndex = 5
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(20, 207)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl6.Size = New System.Drawing.Size(78, 13)
        Me.LabelControl6.TabIndex = 15
        Me.LabelControl6.Text = "Protection Limit"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabelControl5.Location = New System.Drawing.Point(268, 180)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl5.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl5.TabIndex = 14
        Me.LabelControl5.Text = "Seconds"
        '
        'txtTimeout
        '
        Me.txtTimeout.Location = New System.Drawing.Point(122, 173)
        Me.txtTimeout.Name = "txtTimeout"
        Me.txtTimeout.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTimeout.Properties.Appearance.Options.UseFont = True
        Me.txtTimeout.Size = New System.Drawing.Size(140, 22)
        Me.txtTimeout.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(20, 178)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl4.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl4.TabIndex = 12
        Me.LabelControl4.Text = "Job TimeOut"
        '
        'dtpJob
        '
        Me.dtpJob.EditValue = New Date(2016, 8, 11, 11, 59, 10, 756)
        Me.dtpJob.Location = New System.Drawing.Point(122, 146)
        Me.dtpJob.Name = "dtpJob"
        Me.dtpJob.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpJob.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpJob.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm"
        Me.dtpJob.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dtpJob.Properties.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm"
        Me.dtpJob.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.dtpJob.Size = New System.Drawing.Size(140, 20)
        Me.dtpJob.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(20, 149)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl2.TabIndex = 10
        Me.LabelControl2.Text = "Start Time"
        '
        'txtJobDescription
        '
        Me.txtJobDescription.Location = New System.Drawing.Point(122, 45)
        Me.txtJobDescription.Name = "txtJobDescription"
        Me.txtJobDescription.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobDescription.Properties.Appearance.Options.UseFont = True
        Me.txtJobDescription.Size = New System.Drawing.Size(423, 94)
        Me.txtJobDescription.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(20, 50)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(78, 13)
        Me.LabelControl1.TabIndex = 8
        Me.LabelControl1.Text = "Job Description"
        '
        'txtJobName
        '
        Me.txtJobName.Location = New System.Drawing.Point(122, 12)
        Me.txtJobName.Name = "txtJobName"
        Me.txtJobName.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobName.Properties.Appearance.Options.UseFont = True
        Me.txtJobName.Size = New System.Drawing.Size(233, 22)
        Me.txtJobName.TabIndex = 1
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(20, 17)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl9.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl9.TabIndex = 3
        Me.LabelControl9.Text = "Job Name"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnOK, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(860, 332)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(195, 37)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Location = New System.Drawing.Point(3, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(91, 31)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(100, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(92, 31)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.gcQueries, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(563, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(491, 324)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'gcQueries
        '
        Me.gcQueries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcQueries.Location = New System.Drawing.Point(4, 39)
        Me.gcQueries.MainView = Me.gvQueries
        Me.gcQueries.Margin = New System.Windows.Forms.Padding(4)
        Me.gcQueries.Name = "gcQueries"
        Me.gcQueries.Size = New System.Drawing.Size(483, 281)
        Me.gcQueries.TabIndex = 4
        Me.gcQueries.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvQueries})
        '
        'gvQueries
        '
        Me.gvQueries.GridControl = Me.gcQueries
        Me.gvQueries.Name = "gvQueries"
        Me.gvQueries.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvQueries.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvQueries.OptionsBehavior.Editable = False
        Me.gvQueries.OptionsCustomization.AllowColumnMoving = False
        Me.gvQueries.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvQueries.OptionsView.ColumnAutoWidth = False
        Me.gvQueries.OptionsView.ShowGroupPanel = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.LabelControl3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(485, 29)
        Me.Panel1.TabIndex = 3
        '
        'btnSave
        '
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(408, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 25)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'LabelControl3
        '
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.LabelControl3.Size = New System.Drawing.Size(481, 25)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Queries of Job"
        '
        'dlgJobsAdd
        '
        Me.Appearance.ForeColor = System.Drawing.Color.Black
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1057, 371)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgJobsAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Job Information"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkJobActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJobProtectionLimit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTimeout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpJob.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpJob.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJobDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJobName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.gcQueries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvQueries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Panel2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents gcQueries As DevExpress.XtraGrid.GridControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtJobDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtJobName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkJobActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtJobProtectionLimit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTimeout As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpJob As DevExpress.XtraEditors.DateEdit
    Friend WithEvents rbMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents rbWeekly As System.Windows.Forms.RadioButton
    Friend WithEvents rbDaily As System.Windows.Forms.RadioButton
    Friend WithEvents rbHourly As System.Windows.Forms.RadioButton
    Friend WithEvents rb15Mins As System.Windows.Forms.RadioButton
    Friend WithEvents gvQueries As DevExpress.XtraGrid.Views.Grid.GridView
End Class
