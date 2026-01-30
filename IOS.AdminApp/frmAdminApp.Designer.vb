<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdminApp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdminApp))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtExcelPath = New DevExpress.XtraEditors.TextEdit()
        Me.btnTemplate = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label9 = New DevExpress.XtraEditors.LabelControl()
        Me.rtxtEncryptedString = New DevExpress.XtraEditors.MemoEdit()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New DevExpress.XtraEditors.LabelControl()
        Me.Label2 = New DevExpress.XtraEditors.LabelControl()
        Me.Label3 = New DevExpress.XtraEditors.LabelControl()
        Me.Label4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtServerName = New DevExpress.XtraEditors.TextEdit()
        Me.txtDBUserName = New DevExpress.XtraEditors.TextEdit()
        Me.txtDatabase = New DevExpress.XtraEditors.TextEdit()
        Me.txtDBPassword = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnExport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNew = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New DevExpress.XtraEditors.LabelControl()
        Me.Label6 = New DevExpress.XtraEditors.LabelControl()
        Me.Label7 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCompanyName = New DevExpress.XtraEditors.TextEdit()
        Me.txtUserName = New DevExpress.XtraEditors.TextEdit()
        Me.dtExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.chkNew = New System.Windows.Forms.CheckBox()
        Me.chkDateUpdate = New System.Windows.Forms.CheckBox()
        Me.vchkListBox = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.cmsUserNameList = New System.Windows.Forms.ContextMenuStrip()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UncheckAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.txtExcelPath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.rtxtEncryptedString.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.txtServerName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBUserName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDatabase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtCompanyName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vchkListBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsUserNameList.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel9, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel10, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel11, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(984, 462)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 5
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.306122!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.89796!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.69388!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.Label8, 1, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.txtExcelPath, 2, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.btnTemplate, 3, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(2, 259)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(980, 31)
        Me.TableLayoutPanel9.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(55, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label8.Size = New System.Drawing.Size(140, 25)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Excel Path"
        '
        'txtExcelPath
        '
        Me.txtExcelPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtExcelPath.Location = New System.Drawing.Point(201, 3)
        Me.txtExcelPath.Name = "txtExcelPath"
        Me.txtExcelPath.Size = New System.Drawing.Size(579, 20)
        Me.txtExcelPath.TabIndex = 10
        '
        'btnTemplate
        '
        Me.btnTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTemplate.Location = New System.Drawing.Point(786, 3)
        Me.btnTemplate.Name = "btnTemplate"
        Me.btnTemplate.Size = New System.Drawing.Size(141, 25)
        Me.btnTemplate.TabIndex = 11
        Me.btnTemplate.Text = "Set Default Template"
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 4
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.010225!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.03067!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.74438!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.Label9, 1, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.rtxtEncryptedString, 2, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(3, 295)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(978, 94)
        Me.TableLayoutPanel10.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Location = New System.Drawing.Point(52, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label9.Size = New System.Drawing.Size(141, 88)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Encrypted String"
        '
        'rtxtEncryptedString
        '
        Me.rtxtEncryptedString.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtEncryptedString.EditValue = ""
        Me.rtxtEncryptedString.Location = New System.Drawing.Point(199, 3)
        Me.rtxtEncryptedString.Name = "rtxtEncryptedString"
        Me.rtxtEncryptedString.Size = New System.Drawing.Size(726, 88)
        Me.rtxtEncryptedString.TabIndex = 12
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 6
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.214724!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.62168!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label3, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label4, 3, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txtServerName, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txtDBUserName, 2, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txtDatabase, 4, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txtDBPassword, 4, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 33)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(978, 49)
        Me.TableLayoutPanel4.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(54, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(137, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name"
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(54, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(137, 19)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "DB User Name"
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(441, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(140, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "DataBase Name"
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(441, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label4.Size = New System.Drawing.Size(140, 19)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "DB Password"
        '
        'txtServerName
        '
        Me.txtServerName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtServerName.Location = New System.Drawing.Point(197, 3)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(238, 20)
        Me.txtServerName.TabIndex = 0
        '
        'txtDBUserName
        '
        Me.txtDBUserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDBUserName.EditValue = "IOS"
        Me.txtDBUserName.Location = New System.Drawing.Point(197, 27)
        Me.txtDBUserName.Name = "txtDBUserName"
        Me.txtDBUserName.Size = New System.Drawing.Size(238, 20)
        Me.txtDBUserName.TabIndex = 2
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(587, 3)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(200, 20)
        Me.txtDatabase.TabIndex = 1
        '
        'txtDBPassword
        '
        Me.txtDBPassword.Location = New System.Drawing.Point(587, 27)
        Me.txtDBPassword.Name = "txtDBPassword"
        Me.txtDBPassword.Size = New System.Drawing.Size(200, 20)
        Me.txtDBPassword.TabIndex = 3
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 4
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.03067!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.84663!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.TableLayoutPanel12, 2, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(3, 395)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(978, 34)
        Me.TableLayoutPanel11.TabIndex = 11
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 5
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.btnExport, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnNew, 1, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnUpdate, 2, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnDelete, 3, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(196, 1)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(730, 32)
        Me.TableLayoutPanel12.TabIndex = 0
        '
        'btnExport
        '
        Me.btnExport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExport.Location = New System.Drawing.Point(3, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(114, 26)
        Me.btnExport.TabIndex = 13
        Me.btnExport.Text = "Exprot To Edit"
        '
        'btnNew
        '
        Me.btnNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNew.Enabled = False
        Me.btnNew.Location = New System.Drawing.Point(123, 3)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(114, 26)
        Me.btnNew.TabIndex = 14
        Me.btnNew.Text = "Create New"
        '
        'btnUpdate
        '
        Me.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUpdate.Location = New System.Drawing.Point(243, 3)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(114, 26)
        Me.btnUpdate.TabIndex = 15
        Me.btnUpdate.Text = "Update"
        '
        'btnDelete
        '
        Me.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDelete.Location = New System.Drawing.Point(363, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(114, 26)
        Me.btnDelete.TabIndex = 16
        Me.btnDelete.Text = "Delete"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 5
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.97436!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.30769!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.txtCompanyName, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtUserName, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.dtExpiryDate, 2, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.chkNew, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.chkDateUpdate, 3, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.vchkListBox, 2, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 88)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(978, 166)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(54, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label5.Size = New System.Drawing.Size(139, 19)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Company Name"
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(54, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "User Name"
        '
        'Label7
        '
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Location = New System.Drawing.Point(54, 144)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(139, 19)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Expiry Date"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCompanyName.Location = New System.Drawing.Point(199, 3)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(237, 20)
        Me.txtCompanyName.TabIndex = 4
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(587, 3)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(200, 20)
        Me.txtUserName.TabIndex = 6
        '
        'dtExpiryDate
        '
        Me.dtExpiryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtExpiryDate.Location = New System.Drawing.Point(199, 144)
        Me.dtExpiryDate.Name = "dtExpiryDate"
        Me.dtExpiryDate.Size = New System.Drawing.Size(237, 21)
        Me.dtExpiryDate.TabIndex = 8
        '
        'chkNew
        '
        Me.chkNew.AutoSize = True
        Me.chkNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkNew.Location = New System.Drawing.Point(442, 3)
        Me.chkNew.Name = "chkNew"
        Me.chkNew.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.chkNew.Size = New System.Drawing.Size(139, 19)
        Me.chkNew.TabIndex = 5
        Me.chkNew.Text = "New User"
        Me.chkNew.UseVisualStyleBackColor = True
        '
        'chkDateUpdate
        '
        Me.chkDateUpdate.AutoSize = True
        Me.chkDateUpdate.Checked = True
        Me.chkDateUpdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDateUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkDateUpdate.Location = New System.Drawing.Point(442, 144)
        Me.chkDateUpdate.Name = "chkDateUpdate"
        Me.chkDateUpdate.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.chkDateUpdate.Size = New System.Drawing.Size(139, 19)
        Me.chkDateUpdate.TabIndex = 9
        Me.chkDateUpdate.Text = "Update Date Also"
        Me.chkDateUpdate.UseVisualStyleBackColor = True
        '
        'vchkListBox
        '
        Me.vchkListBox.CheckOnClick = True
        Me.vchkListBox.ContextMenuStrip = Me.cmsUserNameList
        Me.vchkListBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.vchkListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vchkListBox.Location = New System.Drawing.Point(199, 28)
        Me.vchkListBox.Name = "vchkListBox"
        Me.vchkListBox.Size = New System.Drawing.Size(237, 110)
        Me.vchkListBox.TabIndex = 7
        '
        'cmsUserNameList
        '
        Me.cmsUserNameList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem, Me.UncheckAllToolStripMenuItem})
        Me.cmsUserNameList.Name = "cmsUserNameList"
        Me.cmsUserNameList.Size = New System.Drawing.Size(138, 48)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.SelectAllToolStripMenuItem.Text = "Check All"
        '
        'UncheckAllToolStripMenuItem
        '
        Me.UncheckAllToolStripMenuItem.Name = "UncheckAllToolStripMenuItem"
        Me.UncheckAllToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.UncheckAllToolStripMenuItem.Text = "Uncheck All"
        '
        'frmAdminApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 462)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 400)
        Me.Name = "frmAdminApp"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IOS Admin"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        CType(Me.txtExcelPath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        CType(Me.rtxtEncryptedString.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.txtServerName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBUserName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDatabase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.txtCompanyName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vchkListBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsUserNameList.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnExport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtExcelPath As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents rtxtEncryptedString As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtServerName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDBUserName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDatabase As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDBPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCompanyName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUserName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtExpiryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkNew As System.Windows.Forms.CheckBox
    Friend WithEvents chkDateUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents vchkListBox As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents cmsUserNameList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UncheckAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
