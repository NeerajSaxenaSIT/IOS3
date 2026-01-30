<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
    Inherits DevExpress.XtraSplashScreen.SplashScreen

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
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Copyright As System.Windows.Forms.Label
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DetailsLayoutPanel As System.Windows.Forms.TableLayoutPanel

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
		Me.DetailsLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
		Me.Copyright = New System.Windows.Forms.Label()
		Me.Version = New System.Windows.Forms.Label()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.lblVersionCheck = New System.Windows.Forms.Label()
		Me.Splash_ProgressBar = New System.Windows.Forms.ProgressBar()
		Me.lblDataSourceVerfication = New System.Windows.Forms.Label()
		Me.lblIOSConfiguration = New System.Windows.Forms.Label()
		Me.lblIOSConnection = New System.Windows.Forms.Label()
		Me.lblLicenseCheck = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.lblLicServer = New System.Windows.Forms.Label()
		Me.lblLicenseServer = New System.Windows.Forms.Label()
		Me.lbl_License = New System.Windows.Forms.Label()
		Me.MainLayoutPanel.SuspendLayout()
		Me.DetailsLayoutPanel.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.TableLayoutPanel2.SuspendLayout()
		Me.TableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'MainLayoutPanel
		'
		Me.MainLayoutPanel.BackgroundImage = Global.IOS.My.Resources.Resources.ÌOS_SplashScreen
		Me.MainLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.MainLayoutPanel.ColumnCount = 2
		Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 506.0!))
		Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
		Me.MainLayoutPanel.Controls.Add(Me.DetailsLayoutPanel, 1, 1)
		Me.MainLayoutPanel.Controls.Add(Me.Panel1, 1, 0)
		Me.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.MainLayoutPanel.Location = New System.Drawing.Point(0, 0)
		Me.MainLayoutPanel.Margin = New System.Windows.Forms.Padding(4)
		Me.MainLayoutPanel.Name = "MainLayoutPanel"
		Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 306.0!))
		Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17.0!))
		Me.MainLayoutPanel.Size = New System.Drawing.Size(867, 423)
		Me.MainLayoutPanel.TabIndex = 0
		'
		'DetailsLayoutPanel
		'
		Me.DetailsLayoutPanel.BackColor = System.Drawing.Color.Transparent
		Me.DetailsLayoutPanel.ColumnCount = 1
		Me.DetailsLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.DetailsLayoutPanel.Controls.Add(Me.Copyright, 0, 1)
		Me.DetailsLayoutPanel.Controls.Add(Me.Version, 0, 0)
		Me.DetailsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DetailsLayoutPanel.Location = New System.Drawing.Point(510, 310)
		Me.DetailsLayoutPanel.Margin = New System.Windows.Forms.Padding(4)
		Me.DetailsLayoutPanel.Name = "DetailsLayoutPanel"
		Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.46067!))
		Me.DetailsLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.53933!))
		Me.DetailsLayoutPanel.Size = New System.Drawing.Size(353, 109)
		Me.DetailsLayoutPanel.TabIndex = 1
		'
		'Copyright
		'
		Me.Copyright.BackColor = System.Drawing.Color.Transparent
		Me.Copyright.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Copyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.Copyright.Location = New System.Drawing.Point(4, 89)
		Me.Copyright.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Copyright.Name = "Copyright"
		Me.Copyright.Size = New System.Drawing.Size(345, 20)
		Me.Copyright.TabIndex = 2
		Me.Copyright.Text = "Copyright"
		'
		'Version
		'
		Me.Version.BackColor = System.Drawing.Color.Transparent
		Me.Version.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.Version.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.Version.Location = New System.Drawing.Point(4, 0)
		Me.Version.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Version.Name = "Version"
		Me.Version.Size = New System.Drawing.Size(345, 34)
		Me.Version.TabIndex = 1
		Me.Version.Text = "Version {0}.{1:00}.{2}.{3}"
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.Color.Transparent
		Me.Panel1.Controls.Add(Me.TableLayoutPanel2)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(510, 4)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(353, 298)
		Me.Panel1.TabIndex = 2
		'
		'TableLayoutPanel2
		'
		Me.TableLayoutPanel2.ColumnCount = 1
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 0, 0)
		Me.TableLayoutPanel2.Controls.Add(Me.lbl_License, 0, 1)
		Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
		Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
		Me.TableLayoutPanel2.RowCount = 2
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
		Me.TableLayoutPanel2.Size = New System.Drawing.Size(353, 298)
		Me.TableLayoutPanel2.TabIndex = 9
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.ColumnCount = 2
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.TableLayoutPanel1.Controls.Add(Me.lblVersionCheck, 1, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.Splash_ProgressBar, 1, 6)
		Me.TableLayoutPanel1.Controls.Add(Me.lblDataSourceVerfication, 1, 5)
		Me.TableLayoutPanel1.Controls.Add(Me.lblIOSConfiguration, 1, 4)
		Me.TableLayoutPanel1.Controls.Add(Me.lblIOSConnection, 1, 3)
		Me.TableLayoutPanel1.Controls.Add(Me.lblLicenseCheck, 1, 2)
		Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 2)
		Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 3)
		Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 4)
		Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 5)
		Me.TableLayoutPanel1.Controls.Add(Me.Label11, 0, 6)
		Me.TableLayoutPanel1.Controls.Add(Me.lblLicServer, 0, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.lblLicenseServer, 1, 1)
		Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 2)
		Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 7
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(347, 234)
		Me.TableLayoutPanel1.TabIndex = 8
		'
		'lblVersionCheck
		'
		Me.lblVersionCheck.AutoSize = True
		Me.lblVersionCheck.BackColor = System.Drawing.Color.Transparent
		Me.lblVersionCheck.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblVersionCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.lblVersionCheck.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.lblVersionCheck.Location = New System.Drawing.Point(54, 0)
		Me.lblVersionCheck.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblVersionCheck.Name = "lblVersionCheck"
		Me.lblVersionCheck.Size = New System.Drawing.Size(289, 33)
		Me.lblVersionCheck.TabIndex = 6
		Me.lblVersionCheck.Text = "Version Check"
		Me.lblVersionCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Splash_ProgressBar
		'
		Me.Splash_ProgressBar.BackColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.Splash_ProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Splash_ProgressBar.ForeColor = System.Drawing.Color.Turquoise
		Me.Splash_ProgressBar.Location = New System.Drawing.Point(61, 211)
		Me.Splash_ProgressBar.Margin = New System.Windows.Forms.Padding(11, 13, 11, 13)
		Me.Splash_ProgressBar.Name = "Splash_ProgressBar"
		Me.Splash_ProgressBar.Size = New System.Drawing.Size(275, 10)
		Me.Splash_ProgressBar.TabIndex = 5
		'
		'lblDataSourceVerfication
		'
		Me.lblDataSourceVerfication.AutoSize = True
		Me.lblDataSourceVerfication.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblDataSourceVerfication.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.lblDataSourceVerfication.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.lblDataSourceVerfication.Location = New System.Drawing.Point(54, 165)
		Me.lblDataSourceVerfication.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblDataSourceVerfication.Name = "lblDataSourceVerfication"
		Me.lblDataSourceVerfication.Size = New System.Drawing.Size(289, 33)
		Me.lblDataSourceVerfication.TabIndex = 3
		Me.lblDataSourceVerfication.Text = "Datasources Verification"
		Me.lblDataSourceVerfication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblIOSConfiguration
		'
		Me.lblIOSConfiguration.AutoSize = True
		Me.lblIOSConfiguration.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblIOSConfiguration.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.lblIOSConfiguration.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.lblIOSConfiguration.Location = New System.Drawing.Point(54, 132)
		Me.lblIOSConfiguration.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblIOSConfiguration.Name = "lblIOSConfiguration"
		Me.lblIOSConfiguration.Size = New System.Drawing.Size(289, 33)
		Me.lblIOSConfiguration.TabIndex = 2
		Me.lblIOSConfiguration.Text = "IOS Configuration"
		Me.lblIOSConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblIOSConnection
		'
		Me.lblIOSConnection.AutoSize = True
		Me.lblIOSConnection.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblIOSConnection.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.lblIOSConnection.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.lblIOSConnection.Location = New System.Drawing.Point(54, 99)
		Me.lblIOSConnection.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblIOSConnection.Name = "lblIOSConnection"
		Me.lblIOSConnection.Size = New System.Drawing.Size(289, 33)
		Me.lblIOSConnection.TabIndex = 1
		Me.lblIOSConnection.Text = "IOS Server Connection"
		Me.lblIOSConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblLicenseCheck
		'
		Me.lblLicenseCheck.AutoSize = True
		Me.lblLicenseCheck.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblLicenseCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.lblLicenseCheck.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.lblLicenseCheck.Location = New System.Drawing.Point(54, 66)
		Me.lblLicenseCheck.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblLicenseCheck.Name = "lblLicenseCheck"
		Me.lblLicenseCheck.Size = New System.Drawing.Size(289, 33)
		Me.lblLicenseCheck.TabIndex = 12
		Me.lblLicenseCheck.Text = "License Check"
		Me.lblLicenseCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label6.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.Label6.Location = New System.Drawing.Point(4, 2)
		Me.Label6.Margin = New System.Windows.Forms.Padding(4, 2, 4, 0)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(42, 31)
		Me.Label6.TabIndex = 13
		Me.Label6.Text = "l"
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label7.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.Label7.Location = New System.Drawing.Point(4, 68)
		Me.Label7.Margin = New System.Windows.Forms.Padding(4, 2, 4, 0)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(42, 31)
		Me.Label7.TabIndex = 14
		Me.Label7.Text = "l"
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label8.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.Label8.Location = New System.Drawing.Point(4, 101)
		Me.Label8.Margin = New System.Windows.Forms.Padding(4, 2, 4, 0)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(42, 31)
		Me.Label8.TabIndex = 15
		Me.Label8.Text = "l"
		Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label9.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.Label9.Location = New System.Drawing.Point(4, 134)
		Me.Label9.Margin = New System.Windows.Forms.Padding(4, 2, 4, 0)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(42, 31)
		Me.Label9.TabIndex = 16
		Me.Label9.Text = "l"
		Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label10.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.Label10.Location = New System.Drawing.Point(4, 167)
		Me.Label10.Margin = New System.Windows.Forms.Padding(4, 2, 4, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(42, 31)
		Me.Label10.TabIndex = 17
		Me.Label10.Text = "l"
		Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label11.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.Label11.Location = New System.Drawing.Point(4, 200)
		Me.Label11.Margin = New System.Windows.Forms.Padding(4, 2, 4, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(42, 34)
		Me.Label11.TabIndex = 18
		Me.Label11.Text = "l"
		Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblLicServer
		'
		Me.lblLicServer.AutoSize = True
		Me.lblLicServer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblLicServer.Font = New System.Drawing.Font("Wingdings", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
		Me.lblLicServer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.lblLicServer.Location = New System.Drawing.Point(4, 35)
		Me.lblLicServer.Margin = New System.Windows.Forms.Padding(4, 2, 4, 0)
		Me.lblLicServer.Name = "lblLicServer"
		Me.lblLicServer.Size = New System.Drawing.Size(42, 31)
		Me.lblLicServer.TabIndex = 19
		Me.lblLicServer.Text = "l"
		Me.lblLicServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblLicenseServer
		'
		Me.lblLicenseServer.AutoSize = True
		Me.lblLicenseServer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblLicenseServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
		Me.lblLicenseServer.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.lblLicenseServer.Location = New System.Drawing.Point(54, 33)
		Me.lblLicenseServer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblLicenseServer.Name = "lblLicenseServer"
		Me.lblLicenseServer.Size = New System.Drawing.Size(289, 33)
		Me.lblLicenseServer.TabIndex = 20
		Me.lblLicenseServer.Text = "License Server"
		Me.lblLicenseServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lbl_License
		'
		Me.lbl_License.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.lbl_License.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.lbl_License.Location = New System.Drawing.Point(4, 276)
		Me.lbl_License.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lbl_License.Name = "lbl_License"
		Me.lbl_License.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl_License.Size = New System.Drawing.Size(345, 22)
		Me.lbl_License.TabIndex = 7
		Me.lbl_License.Text = "Licensed To: "
		'
		'SplashScreen
		'
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		Me.AutoSize = True
		Me.ClientSize = New System.Drawing.Size(867, 423)
		Me.Controls.Add(Me.MainLayoutPanel)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "SplashScreen"
		Me.MainLayoutPanel.ResumeLayout(False)
		Me.DetailsLayoutPanel.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.TableLayoutPanel2.ResumeLayout(False)
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.TableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblDataSourceVerfication As System.Windows.Forms.Label
    Friend WithEvents lblIOSConfiguration As System.Windows.Forms.Label
    Friend WithEvents lblIOSConnection As System.Windows.Forms.Label
    Friend WithEvents Splash_ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblVersionCheck As System.Windows.Forms.Label
    Friend WithEvents lbl_License As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblLicenseCheck As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents lblLicServer As Label
	Friend WithEvents lblLicenseServer As Label
End Class
