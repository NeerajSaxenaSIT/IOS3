<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMDI
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

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
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDI))
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.Document2 = New DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(Me.components)
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.ribCon_Main = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.rbarBtn_Exit = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSON = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnPCHR = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnReports = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnTags = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnPMKPIs = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnChart = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnICM = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnVirtualAzimuthDetection = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnChangeFeatures = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSyncAll = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSyncConsoleObjects = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSyncMapObjects = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSyncClientConfig = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnMapWindow = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnConsoleWindow = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSelectionInfo = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnTicketWindow = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnPortalHelp = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnManualHelp = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnAbout = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnDrivetest = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSandBox1 = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnCustomerExp = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnCMView = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnYellowfin = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnDashboard = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnWebClient = New DevExpress.XtraBars.BarButtonItem()
        Me.SkinRibbonGalleryBarItem1 = New DevExpress.XtraBars.SkinRibbonGalleryBarItem()
        Me.bbtnDataIntegrity = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnPMView = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnAnomaly = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnNBMgmt = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnRefCheck = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnCapacity = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnTiltMngrBulk = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnTiltMngrAdHoc = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnXML = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnNBIReports = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSandBox = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSiteIntegration = New DevExpress.XtraBars.BarButtonItem()
        Me.rbPageFile = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.bbtnOpenWorkspace = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnSaveWorkspace = New DevExpress.XtraBars.BarButtonItem()
        Me.rbPageGrpMapTable = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.bbtnOpenMapTable = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnCloseMapTable = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnCloseAllMapTable = New DevExpress.XtraBars.BarButtonItem()
        Me.rbPageExit = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.bbtnExit = New DevExpress.XtraBars.BarButtonItem()
        Me.rbPagePM = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.grpPMConfig = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbPageCEM = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup8 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup9 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbPageCM = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpg_CM = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.bbtnParameterHistory = New DevExpress.XtraBars.BarButtonItem()
        Me.rbPageTools = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rbPageGroupManager = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpg_GIS = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.bbtnQueryBuilder = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnGISSearch = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnMapImport = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup6 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbPageHelp = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rbPageGroupHelp = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.rbarBtn_PCHR = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtn_PCHR = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnTraceGPEH = New DevExpress.XtraBars.BarButtonItem()
        Me.bbtnDriveTestImport = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbPageDashboard = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.dmMDI = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DocumentManager1 = New DevExpress.XtraBars.Docking2010.DocumentManager(Me.components)
        Me.TabbedView1 = New DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(Me.components)
        Me.DefaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.FormAssistant1 = New DevExpress.XtraBars.FormAssistant()
        Me.SplashScreenManager1 = New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Global.IOS.WaitForm1), True, True, True)
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Document2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ribCon_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dmMDI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'Document2
        '
        Me.Document2.Caption = "IOS Map"
        Me.Document2.ControlName = "dpMap"
        Me.Document2.FloatLocation = New System.Drawing.Point(0, 0)
        Me.Document2.FloatSize = New System.Drawing.Size(200, 200)
        Me.Document2.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.[True]
        Me.Document2.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.[True]
        Me.Document2.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.[True]
        '
        'lblVersion
        '
        Me.lblVersion.Location = New System.Drawing.Point(0, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(100, 23)
        Me.lblVersion.TabIndex = 0
        Me.lblVersion.Text = "Version {0}.{1:00}.{2}.{3}"
        '
        'lblCopyright
        '
        Me.lblCopyright.Location = New System.Drawing.Point(0, 0)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(100, 23)
        Me.lblCopyright.TabIndex = 0
        Me.lblCopyright.Text = "Copyright"
        '
        'ribCon_Main
        '
        Me.ribCon_Main.AllowCustomization = True
        Me.ribCon_Main.AutoSizeItems = True
        Me.ribCon_Main.ExpandCollapseItem.Id = 0
        Me.ribCon_Main.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribCon_Main.ExpandCollapseItem, Me.ribCon_Main.SearchEditItem, Me.rbarBtn_Exit, Me.bbtnSON, Me.bbtnPCHR, Me.bbtnReports, Me.bbtnTags, Me.bbtnPMKPIs, Me.bbtnChart, Me.bbtnICM, Me.bbtnVirtualAzimuthDetection, Me.bbtnChangeFeatures, Me.bbtnSyncAll, Me.bbtnSyncConsoleObjects, Me.bbtnSyncMapObjects, Me.bbtnSyncClientConfig, Me.bbtnMapWindow, Me.bbtnConsoleWindow, Me.bbtnSelectionInfo, Me.bbtnTicketWindow, Me.bbtnPortalHelp, Me.bbtnManualHelp, Me.bbtnAbout, Me.bbtnDrivetest, Me.bbtnSandBox1, Me.bbtnCustomerExp, Me.bbtnCMView, Me.bbtnYellowfin, Me.bbtnDashboard, Me.bbtnWebClient, Me.SkinRibbonGalleryBarItem1, Me.bbtnDataIntegrity, Me.bbtnPMView, Me.bbtnAnomaly, Me.bbtnNBMgmt, Me.bbtnRefCheck, Me.bbtnCapacity, Me.bbtnTiltMngrBulk, Me.bbtnTiltMngrAdHoc, Me.bbtnXML, Me.bbtnNBIReports, Me.bbtnSandBox, Me.bbtnSiteIntegration})
        Me.ribCon_Main.Location = New System.Drawing.Point(0, 0)
        Me.ribCon_Main.MaxItemId = 107
        Me.ribCon_Main.Name = "ribCon_Main"
        Me.ribCon_Main.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.rbPageFile, Me.rbPagePM, Me.rbPageCEM, Me.rbPageCM, Me.rbPageTools, Me.rbPageHelp})
        Me.ribCon_Main.QuickToolbarItemLinks.Add(Me.rbarBtn_Exit)
        Me.ribCon_Main.QuickToolbarItemLinks.Add(Me.bbtnDataIntegrity)
        Me.ribCon_Main.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox2})
        Me.ribCon_Main.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.ribCon_Main.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribCon_Main.Size = New System.Drawing.Size(1298, 158)
        Me.ribCon_Main.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above
        '
        'rbarBtn_Exit
        '
        Me.rbarBtn_Exit.Caption = "Exit"
        Me.rbarBtn_Exit.Id = 25
        Me.rbarBtn_Exit.ImageOptions.Image = Global.IOS.My.Resources.Resources.Exit1
        Me.rbarBtn_Exit.Name = "rbarBtn_Exit"
        '
        'bbtnSON
        '
        Me.bbtnSON.Caption = "SON"
        Me.bbtnSON.Id = 57
        Me.bbtnSON.ImageOptions.Image = Global.IOS.My.Resources.Resources.son_icon
        Me.bbtnSON.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Son
        Me.bbtnSON.Name = "bbtnSON"
        '
        'bbtnPCHR
        '
        Me.bbtnPCHR.Caption = "PCHR"
        Me.bbtnPCHR.Enabled = False
        Me.bbtnPCHR.Id = 58
        Me.bbtnPCHR.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.PCHR
        Me.bbtnPCHR.LargeWidth = 70
        Me.bbtnPCHR.Name = "bbtnPCHR"
        Me.bbtnPCHR.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Me.bbtnPCHR.VisibleInSearchMenu = False
        '
        'bbtnReports
        '
        Me.bbtnReports.Caption = "Reports"
        Me.bbtnReports.Id = 59
        Me.bbtnReports.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Report
        Me.bbtnReports.Name = "bbtnReports"
        '
        'bbtnTags
        '
        Me.bbtnTags.Caption = "Tags"
        Me.bbtnTags.Id = 60
        Me.bbtnTags.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Tags
        Me.bbtnTags.Name = "bbtnTags"
        '
        'bbtnPMKPIs
        '
        Me.bbtnPMKPIs.Caption = "PM KPIs"
        Me.bbtnPMKPIs.Id = 62
        Me.bbtnPMKPIs.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.PM_KPIs
        Me.bbtnPMKPIs.Name = "bbtnPMKPIs"
        '
        'bbtnChart
        '
        Me.bbtnChart.Caption = "Chart"
        Me.bbtnChart.Id = 63
        Me.bbtnChart.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Chart
        Me.bbtnChart.Name = "bbtnChart"
        '
        'bbtnICM
        '
        Me.bbtnICM.Caption = "ICM"
        Me.bbtnICM.Id = 64
        Me.bbtnICM.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.ICM
        Me.bbtnICM.Name = "bbtnICM"
        Me.bbtnICM.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'bbtnVirtualAzimuthDetection
        '
        Me.bbtnVirtualAzimuthDetection.Caption = "Virtual Azimuth Detection"
        Me.bbtnVirtualAzimuthDetection.Id = 65
        Me.bbtnVirtualAzimuthDetection.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Virtual_Azimuth_Detection
        Me.bbtnVirtualAzimuthDetection.Name = "bbtnVirtualAzimuthDetection"
        '
        'bbtnChangeFeatures
        '
        Me.bbtnChangeFeatures.Caption = "Change Features"
        Me.bbtnChangeFeatures.Id = 66
        Me.bbtnChangeFeatures.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.change_feature
        Me.bbtnChangeFeatures.Name = "bbtnChangeFeatures"
        '
        'bbtnSyncAll
        '
        Me.bbtnSyncAll.Caption = "Sync - All"
        Me.bbtnSyncAll.Enabled = False
        Me.bbtnSyncAll.Id = 67
        Me.bbtnSyncAll.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.sync_all
        Me.bbtnSyncAll.Name = "bbtnSyncAll"
        '
        'bbtnSyncConsoleObjects
        '
        Me.bbtnSyncConsoleObjects.Caption = "Sync - Console Objects"
        Me.bbtnSyncConsoleObjects.Enabled = False
        Me.bbtnSyncConsoleObjects.Id = 68
        Me.bbtnSyncConsoleObjects.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.partial_sync
        Me.bbtnSyncConsoleObjects.Name = "bbtnSyncConsoleObjects"
        '
        'bbtnSyncMapObjects
        '
        Me.bbtnSyncMapObjects.Caption = "Sync - Map Objects"
        Me.bbtnSyncMapObjects.Enabled = False
        Me.bbtnSyncMapObjects.Id = 69
        Me.bbtnSyncMapObjects.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.partial_sync
        Me.bbtnSyncMapObjects.Name = "bbtnSyncMapObjects"
        '
        'bbtnSyncClientConfig
        '
        Me.bbtnSyncClientConfig.Caption = "Sync - Client Configuration"
        Me.bbtnSyncClientConfig.Id = 70
        Me.bbtnSyncClientConfig.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Client_Configuration
        Me.bbtnSyncClientConfig.Name = "bbtnSyncClientConfig"
        '
        'bbtnMapWindow
        '
        Me.bbtnMapWindow.Caption = "Map Window"
        Me.bbtnMapWindow.Id = 71
        Me.bbtnMapWindow.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.map_window
        Me.bbtnMapWindow.Name = "bbtnMapWindow"
        '
        'bbtnConsoleWindow
        '
        Me.bbtnConsoleWindow.Caption = "Console Window"
        Me.bbtnConsoleWindow.Id = 72
        Me.bbtnConsoleWindow.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.console_window
        Me.bbtnConsoleWindow.Name = "bbtnConsoleWindow"
        '
        'bbtnSelectionInfo
        '
        Me.bbtnSelectionInfo.Caption = "Selection Info Window"
        Me.bbtnSelectionInfo.Id = 73
        Me.bbtnSelectionInfo.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.selection_info_win
        Me.bbtnSelectionInfo.Name = "bbtnSelectionInfo"
        '
        'bbtnTicketWindow
        '
        Me.bbtnTicketWindow.Caption = "Ticket Window"
        Me.bbtnTicketWindow.Id = 74
        Me.bbtnTicketWindow.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.ticket_window
        Me.bbtnTicketWindow.Name = "bbtnTicketWindow"
        '
        'bbtnPortalHelp
        '
        Me.bbtnPortalHelp.Caption = "Portal"
        Me.bbtnPortalHelp.Id = 75
        Me.bbtnPortalHelp.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.portal_help
        Me.bbtnPortalHelp.Name = "bbtnPortalHelp"
        '
        'bbtnManualHelp
        '
        Me.bbtnManualHelp.Caption = "Manual"
        Me.bbtnManualHelp.Id = 76
        Me.bbtnManualHelp.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Manual
        Me.bbtnManualHelp.Name = "bbtnManualHelp"
        '
        'bbtnAbout
        '
        Me.bbtnAbout.Caption = "About"
        Me.bbtnAbout.Id = 77
        Me.bbtnAbout.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.about
        Me.bbtnAbout.Name = "bbtnAbout"
        '
        'bbtnDrivetest
        '
        Me.bbtnDrivetest.Caption = "Drivetest"
        Me.bbtnDrivetest.Id = 54
        Me.bbtnDrivetest.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Direvertest
        Me.bbtnDrivetest.LargeWidth = 70
        Me.bbtnDrivetest.Name = "bbtnDrivetest"
        '
        'bbtnSandBox1
        '
        Me.bbtnSandBox1.Caption = "DataMart"
        Me.bbtnSandBox1.Id = 82
        Me.bbtnSandBox1.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.gray_Icon_Package_Final
        Me.bbtnSandBox1.Name = "bbtnSandBox1"
        Me.bbtnSandBox1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'bbtnCustomerExp
        '
        Me.bbtnCustomerExp.Caption = "Customer Experience"
        Me.bbtnCustomerExp.Id = 83
        Me.bbtnCustomerExp.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Customer_Experience
        Me.bbtnCustomerExp.Name = "bbtnCustomerExp"
        '
        'bbtnCMView
        '
        Me.bbtnCMView.Caption = "CM View"
        Me.bbtnCMView.Id = 84
        Me.bbtnCMView.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.DUMP
        Me.bbtnCMView.Name = "bbtnCMView"
        '
        'bbtnYellowfin
        '
        Me.bbtnYellowfin.Caption = "Yellow Fin"
        Me.bbtnYellowfin.Id = 85
        Me.bbtnYellowfin.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.yellowfin
        Me.bbtnYellowfin.Name = "bbtnYellowfin"
        '
        'bbtnDashboard
        '
        Me.bbtnDashboard.Caption = "Dashboard"
        Me.bbtnDashboard.Id = 86
        Me.bbtnDashboard.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Dashboard
        Me.bbtnDashboard.Name = "bbtnDashboard"
        '
        'bbtnWebClient
        '
        Me.bbtnWebClient.Caption = "Web Client"
        Me.bbtnWebClient.Id = 87
        Me.bbtnWebClient.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.blue_Icon_Package_Final
        Me.bbtnWebClient.Name = "bbtnWebClient"
        '
        'SkinRibbonGalleryBarItem1
        '
        Me.SkinRibbonGalleryBarItem1.Caption = "SkinRibbonGalleryBarItem1"
        Me.SkinRibbonGalleryBarItem1.Id = 92
        Me.SkinRibbonGalleryBarItem1.Name = "SkinRibbonGalleryBarItem1"
        '
        'bbtnDataIntegrity
        '
        Me.bbtnDataIntegrity.Caption = "Data Integrity"
        Me.bbtnDataIntegrity.Id = 95
        Me.bbtnDataIntegrity.ImageOptions.Image = Global.IOS.My.Resources.Resources.green_box
        Me.bbtnDataIntegrity.Name = "bbtnDataIntegrity"
        '
        'bbtnPMView
        '
        Me.bbtnPMView.Caption = "PM View"
        Me.bbtnPMView.Id = 96
        Me.bbtnPMView.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.DUMP
        Me.bbtnPMView.Name = "bbtnPMView"
        '
        'bbtnAnomaly
        '
        Me.bbtnAnomaly.Caption = "Anomaly Detection"
        Me.bbtnAnomaly.Id = 97
        Me.bbtnAnomaly.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Anomaly
        Me.bbtnAnomaly.Name = "bbtnAnomaly"
        '
        'bbtnNBMgmt
        '
        Me.bbtnNBMgmt.Caption = "Neighbor Management"
        Me.bbtnNBMgmt.Id = 98
        Me.bbtnNBMgmt.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.NBManagement
        Me.bbtnNBMgmt.Name = "bbtnNBMgmt"
        '
        'bbtnRefCheck
        '
        Me.bbtnRefCheck.Caption = "Ref Check"
        Me.bbtnRefCheck.Id = 99
        Me.bbtnRefCheck.ImageOptions.LargeImage = CType(resources.GetObject("bbtnRefCheck.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.bbtnRefCheck.Name = "bbtnRefCheck"
        '
        'bbtnCapacity
        '
        Me.bbtnCapacity.Caption = "Capacity"
        Me.bbtnCapacity.Id = 100
        Me.bbtnCapacity.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Capacity_Icon
        Me.bbtnCapacity.Name = "bbtnCapacity"
        '
        'bbtnTiltMngrBulk
        '
        Me.bbtnTiltMngrBulk.Caption = "Tilt Manager"
        Me.bbtnTiltMngrBulk.Id = 101
        Me.bbtnTiltMngrBulk.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.btnTiltManager
        Me.bbtnTiltMngrBulk.Name = "bbtnTiltMngrBulk"
        '
        'bbtnTiltMngrAdHoc
        '
        Me.bbtnTiltMngrAdHoc.Caption = "Tilt Manager - Ad Hoc"
        Me.bbtnTiltMngrAdHoc.Id = 102
        Me.bbtnTiltMngrAdHoc.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.btnTiltManager
        Me.bbtnTiltMngrAdHoc.Name = "bbtnTiltMngrAdHoc"
        '
        'bbtnXML
        '
        Me.bbtnXML.Caption = "CM XML"
        Me.bbtnXML.Id = 103
        Me.bbtnXML.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.btnXML
        Me.bbtnXML.Name = "bbtnXML"
        '
        'bbtnNBIReports
        '
        Me.bbtnNBIReports.Caption = "NBI Reports"
        Me.bbtnNBIReports.Id = 104
        Me.bbtnNBIReports.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.NBIReport
        Me.bbtnNBIReports.Name = "bbtnNBIReports"
        '
        'bbtnSandBox
        '
        Me.bbtnSandBox.Caption = "Datamart"
        Me.bbtnSandBox.Id = 105
        Me.bbtnSandBox.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.gray_Icon_Package_Final
        Me.bbtnSandBox.Name = "bbtnSandBox"
        '
        'bbtnSiteIntegration
        '
        Me.bbtnSiteIntegration.Caption = "Site Integration"
        Me.bbtnSiteIntegration.Id = 106
        Me.bbtnSiteIntegration.ImageOptions.LargeImage = CType(resources.GetObject("bbtnSiteIntegration.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.bbtnSiteIntegration.Name = "bbtnSiteIntegration"
        '
        'rbPageFile
        '
        Me.rbPageFile.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2, Me.rbPageGrpMapTable, Me.rbPageExit})
        Me.rbPageFile.Name = "rbPageFile"
        Me.rbPageFile.Text = "File"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.AllowTextClipping = False
        Me.RibbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup2.ItemLinks.Add(Me.bbtnOpenWorkspace)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.bbtnSaveWorkspace)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Workspace"
        '
        'bbtnOpenWorkspace
        '
        Me.bbtnOpenWorkspace.Caption = "Open"
        Me.bbtnOpenWorkspace.Id = 49
        Me.bbtnOpenWorkspace.ImageOptions.Image = CType(resources.GetObject("bbtnOpenWorkspace.ImageOptions.Image"), System.Drawing.Image)
        Me.bbtnOpenWorkspace.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Open
        Me.bbtnOpenWorkspace.Name = "bbtnOpenWorkspace"
        '
        'bbtnSaveWorkspace
        '
        Me.bbtnSaveWorkspace.Caption = "Save"
        Me.bbtnSaveWorkspace.Id = 50
        Me.bbtnSaveWorkspace.ImageOptions.Image = CType(resources.GetObject("bbtnSaveWorkspace.ImageOptions.Image"), System.Drawing.Image)
        Me.bbtnSaveWorkspace.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Save
        Me.bbtnSaveWorkspace.Name = "bbtnSaveWorkspace"
        '
        'rbPageGrpMapTable
        '
        Me.rbPageGrpMapTable.AllowTextClipping = False
        Me.rbPageGrpMapTable.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbPageGrpMapTable.ItemLinks.Add(Me.bbtnOpenMapTable)
        Me.rbPageGrpMapTable.ItemLinks.Add(Me.bbtnCloseMapTable)
        Me.rbPageGrpMapTable.ItemLinks.Add(Me.bbtnCloseAllMapTable)
        Me.rbPageGrpMapTable.Name = "rbPageGrpMapTable"
        Me.rbPageGrpMapTable.Text = "Map Table"
        '
        'bbtnOpenMapTable
        '
        Me.bbtnOpenMapTable.Caption = "Open"
        Me.bbtnOpenMapTable.Enabled = False
        Me.bbtnOpenMapTable.Id = 51
        Me.bbtnOpenMapTable.ImageOptions.Image = CType(resources.GetObject("bbtnOpenMapTable.ImageOptions.Image"), System.Drawing.Image)
        Me.bbtnOpenMapTable.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Open
        Me.bbtnOpenMapTable.Name = "bbtnOpenMapTable"
        '
        'bbtnCloseMapTable
        '
        Me.bbtnCloseMapTable.Caption = "Close"
        Me.bbtnCloseMapTable.Id = 52
        Me.bbtnCloseMapTable.ImageOptions.Image = CType(resources.GetObject("bbtnCloseMapTable.ImageOptions.Image"), System.Drawing.Image)
        Me.bbtnCloseMapTable.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Close
        Me.bbtnCloseMapTable.Name = "bbtnCloseMapTable"
        '
        'bbtnCloseAllMapTable
        '
        Me.bbtnCloseAllMapTable.Caption = "Close All"
        Me.bbtnCloseAllMapTable.Id = 53
        Me.bbtnCloseAllMapTable.ImageOptions.Image = CType(resources.GetObject("bbtnCloseAllMapTable.ImageOptions.Image"), System.Drawing.Image)
        Me.bbtnCloseAllMapTable.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Close_All
        Me.bbtnCloseAllMapTable.Name = "bbtnCloseAllMapTable"
        '
        'rbPageExit
        '
        Me.rbPageExit.AllowTextClipping = False
        Me.rbPageExit.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbPageExit.ItemLinks.Add(Me.bbtnExit)
        Me.rbPageExit.Name = "rbPageExit"
        Me.rbPageExit.Text = "Exit"
        '
        'bbtnExit
        '
        Me.bbtnExit.Caption = "Exit"
        Me.bbtnExit.Id = 55
        Me.bbtnExit.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Exit1
        Me.bbtnExit.Name = "bbtnExit"
        '
        'rbPagePM
        '
        Me.rbPagePM.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.grpPMConfig, Me.RibbonPageGroup3})
        Me.rbPagePM.Name = "rbPagePM"
        Me.rbPagePM.Text = "PM"
        '
        'grpPMConfig
        '
        Me.grpPMConfig.AllowTextClipping = False
        Me.grpPMConfig.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.grpPMConfig.ItemLinks.Add(Me.bbtnPMKPIs)
        Me.grpPMConfig.ItemLinks.Add(Me.bbtnChart)
        Me.grpPMConfig.ItemLinks.Add(Me.bbtnTags)
        Me.grpPMConfig.ItemLinks.Add(Me.bbtnPMView)
        Me.grpPMConfig.ItemLinks.Add(Me.bbtnAnomaly)
        Me.grpPMConfig.ItemLinks.Add(Me.bbtnCapacity)
        Me.grpPMConfig.Name = "grpPMConfig"
        Me.grpPMConfig.Text = "Configuration"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.bbtnSandBox)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "Clients"
        '
        'rbPageCEM
        '
        Me.rbPageCEM.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup8, Me.RibbonPageGroup9})
        Me.rbPageCEM.Name = "rbPageCEM"
        Me.rbPageCEM.Text = "CEM"
        '
        'RibbonPageGroup8
        '
        Me.RibbonPageGroup8.AllowTextClipping = False
        Me.RibbonPageGroup8.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup8.ItemLinks.Add(Me.bbtnPCHR)
        Me.RibbonPageGroup8.Name = "RibbonPageGroup8"
        Me.RibbonPageGroup8.Text = "Trace"
        '
        'RibbonPageGroup9
        '
        Me.RibbonPageGroup9.AllowTextClipping = False
        Me.RibbonPageGroup9.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup9.ItemLinks.Add(Me.bbtnDrivetest)
        Me.RibbonPageGroup9.Name = "RibbonPageGroup9"
        Me.RibbonPageGroup9.Text = "Drivetest"
        '
        'rbPageCM
        '
        Me.rbPageCM.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpg_CM})
        Me.rbPageCM.Name = "rbPageCM"
        Me.rbPageCM.Text = "CM"
        '
        'rpg_CM
        '
        Me.rpg_CM.AllowTextClipping = False
        Me.rpg_CM.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.rpg_CM.ItemLinks.Add(Me.bbtnParameterHistory)
        Me.rpg_CM.ItemLinks.Add(Me.bbtnCMView)
        Me.rpg_CM.ItemLinks.Add(Me.bbtnNBMgmt)
        Me.rpg_CM.ItemLinks.Add(Me.bbtnRefCheck)
        Me.rpg_CM.ItemLinks.Add(Me.bbtnTiltMngrBulk)
        Me.rpg_CM.ItemLinks.Add(Me.bbtnXML)
        Me.rpg_CM.ItemLinks.Add(Me.bbtnSiteIntegration)
        Me.rpg_CM.Name = "rpg_CM"
        Me.rpg_CM.Text = "CM"
        '
        'bbtnParameterHistory
        '
        Me.bbtnParameterHistory.Caption = "Parameter History"
        Me.bbtnParameterHistory.Id = 38
        Me.bbtnParameterHistory.ImageOptions.LargeImage = CType(resources.GetObject("bbtnParameterHistory.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.bbtnParameterHistory.Name = "bbtnParameterHistory"
        '
        'rbPageTools
        '
        Me.rbPageTools.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rbPageGroupManager, Me.rpg_GIS, Me.RibbonPageGroup4, Me.RibbonPageGroup5, Me.RibbonPageGroup6})
        Me.rbPageTools.Name = "rbPageTools"
        Me.rbPageTools.Text = "Tools"
        '
        'rbPageGroupManager
        '
        Me.rbPageGroupManager.AllowTextClipping = False
        Me.rbPageGroupManager.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbPageGroupManager.ItemLinks.Add(Me.bbtnDashboard)
        Me.rbPageGroupManager.ItemLinks.Add(Me.bbtnReports)
        Me.rbPageGroupManager.ItemLinks.Add(Me.bbtnICM)
        Me.rbPageGroupManager.ItemLinks.Add(Me.bbtnSON)
        Me.rbPageGroupManager.ItemLinks.Add(Me.bbtnVirtualAzimuthDetection)
        Me.rbPageGroupManager.ItemLinks.Add(Me.bbtnNBIReports)
        Me.rbPageGroupManager.Name = "rbPageGroupManager"
        Me.rbPageGroupManager.Text = "Reporting"
        '
        'rpg_GIS
        '
        Me.rpg_GIS.AllowTextClipping = False
        Me.rpg_GIS.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.rpg_GIS.ItemLinks.Add(Me.bbtnQueryBuilder)
        Me.rpg_GIS.ItemLinks.Add(Me.bbtnGISSearch, "PRESS CTRL + F TO OPEN")
        Me.rpg_GIS.ItemLinks.Add(Me.bbtnMapImport)
        Me.rpg_GIS.Name = "rpg_GIS"
        Me.rpg_GIS.Text = "GIS"
        '
        'bbtnQueryBuilder
        '
        Me.bbtnQueryBuilder.Caption = "Query Builder"
        Me.bbtnQueryBuilder.Id = 39
        Me.bbtnQueryBuilder.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Query_Builder
        Me.bbtnQueryBuilder.Name = "bbtnQueryBuilder"
        '
        'bbtnGISSearch
        '
        Me.bbtnGISSearch.Caption = "GIS Search"
        Me.bbtnGISSearch.Id = 40
        Me.bbtnGISSearch.ImageOptions.LargeImage = CType(resources.GetObject("bbtnGISSearch.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.bbtnGISSearch.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
        Me.bbtnGISSearch.Name = "bbtnGISSearch"
        Me.bbtnGISSearch.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.[True]
        '
        'bbtnMapImport
        '
        Me.bbtnMapImport.Caption = "Map External Data"
        Me.bbtnMapImport.Id = 41
        Me.bbtnMapImport.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Map_External_Data
        Me.bbtnMapImport.Name = "bbtnMapImport"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.AllowTextClipping = False
        Me.RibbonPageGroup4.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup4.ItemLinks.Add(Me.bbtnSyncClientConfig)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        Me.RibbonPageGroup4.Text = "Settings"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.AllowTextClipping = False
        Me.RibbonPageGroup5.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup5.ItemLinks.Add(Me.bbtnWebClient)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        Me.RibbonPageGroup5.Text = "Clients"
        '
        'RibbonPageGroup6
        '
        Me.RibbonPageGroup6.AllowTextClipping = False
        Me.RibbonPageGroup6.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup6.ItemLinks.Add(Me.SkinRibbonGalleryBarItem1)
        Me.RibbonPageGroup6.Name = "RibbonPageGroup6"
        Me.RibbonPageGroup6.Text = "Appearance"
        '
        'rbPageHelp
        '
        Me.rbPageHelp.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rbPageGroupHelp})
        Me.rbPageHelp.Name = "rbPageHelp"
        Me.rbPageHelp.Text = "Help"
        '
        'rbPageGroupHelp
        '
        Me.rbPageGroupHelp.AllowTextClipping = False
        Me.rbPageGroupHelp.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbPageGroupHelp.ItemLinks.Add(Me.bbtnPortalHelp)
        Me.rbPageGroupHelp.ItemLinks.Add(Me.bbtnManualHelp)
        Me.rbPageGroupHelp.ItemLinks.Add(Me.bbtnAbout)
        Me.rbPageGroupHelp.Name = "rbPageGroupHelp"
        Me.rbPageGroupHelp.Text = "IOS Help"
        '
        'RepositoryItemComboBox2
        '
        Me.RepositoryItemComboBox2.AutoHeight = False
        Me.RepositoryItemComboBox2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox2.Name = "RepositoryItemComboBox2"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 35
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'rbarBtn_PCHR
        '
        Me.rbarBtn_PCHR.Caption = "PCHR"
        Me.rbarBtn_PCHR.Id = 36
        Me.rbarBtn_PCHR.Name = "rbarBtn_PCHR"
        '
        'bbtn_PCHR
        '
        Me.bbtn_PCHR.Caption = "PCHR"
        Me.bbtn_PCHR.Id = 46
        Me.bbtn_PCHR.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.PCHR
        Me.bbtn_PCHR.Name = "bbtn_PCHR"
        '
        'bbtnTraceGPEH
        '
        Me.bbtnTraceGPEH.Caption = "GPEH"
        Me.bbtnTraceGPEH.Id = 47
        Me.bbtnTraceGPEH.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.GPEH_Menu
        Me.bbtnTraceGPEH.Name = "bbtnTraceGPEH"
        '
        'bbtnDriveTestImport
        '
        Me.bbtnDriveTestImport.Caption = "Import"
        Me.bbtnDriveTestImport.Id = 48
        Me.bbtnDriveTestImport.ImageOptions.LargeImage = Global.IOS.My.Resources.Resources.Import
        Me.bbtnDriveTestImport.Name = "bbtnDriveTestImport"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 48
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.AllowTextClipping = False
        Me.RibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "Dashboard"
        '
        'rbPageDashboard
        '
        Me.rbPageDashboard.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.rbPageDashboard.Name = "rbPageDashboard"
        Me.rbPageDashboard.Text = "Dashboard"
        '
        'dmMDI
        '
        Me.dmMDI.Form = Me
        Me.dmMDI.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl"})
        '
        'DocumentManager1
        '
        Me.DocumentManager1.ContainerControl = Me
        Me.DocumentManager1.View = Me.TabbedView1
        Me.DocumentManager1.ViewCollection.AddRange(New DevExpress.XtraBars.Docking2010.Views.BaseView() {Me.TabbedView1})
        '
        'TabbedView1
        '
        Me.TabbedView1.OptionsLayout.PropertiesRestoreMode = DevExpress.XtraBars.Docking2010.Views.PropertiesRestoreMode.All
        '
        'frmMDI
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[False]
        Me.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Appearance.Options.UseForeColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1298, 949)
        Me.Controls.Add(Me.ribCon_Main)
        Me.IconOptions.Icon = CType(resources.GetObject("frmMDI.IconOptions.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MinimumSize = New System.Drawing.Size(1300, 950)
        Me.Name = "frmMDI"
        Me.Ribbon = Me.ribCon_Main
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "MDI"
        Me.Text = "IOS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Document2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ribCon_Main, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dmMDI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents rbPageDashboard As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents ribCon_Main As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents rbarBtn_Exit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbarBtn_PCHR As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnParameterHistory As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnQueryBuilder As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnGISSearch As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnMapImport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtn_PCHR As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnTraceGPEH As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnDriveTestImport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbPagePM As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rbPageCM As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rpg_CM As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents rbPageTools As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rpg_GIS As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents rbPageCEM As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents bbtnOpenWorkspace As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnSaveWorkspace As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbPageFile As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnOpenMapTable As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnCloseMapTable As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnCloseAllMapTable As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbPageGrpMapTable As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnDrivetest As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnExit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbPageExit As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnSON As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnPCHR As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnReports As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnTags As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnPMKPIs As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnChart As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnICM As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnVirtualAzimuthDetection As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbPageGroupManager As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnChangeFeatures As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnSyncAll As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnSyncConsoleObjects As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnSyncMapObjects As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnSyncClientConfig As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnMapWindow As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnConsoleWindow As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnSelectionInfo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnTicketWindow As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnPortalHelp As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnManualHelp As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnAbout As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbPageHelp As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rbPageGroupHelp As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents dmMDI As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DocumentManager1 As DevExpress.XtraBars.Docking2010.DocumentManager
    Friend WithEvents TabbedView1 As DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView
    Friend WithEvents Document2 As DevExpress.XtraBars.Docking2010.Views.Tabbed.Document
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnSandBox1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnCustomerExp As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnCMView As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnYellowfin As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnDashboard As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpPMConfig As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup8 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup9 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnWebClient As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup6 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents SkinRibbonGalleryBarItem1 As DevExpress.XtraBars.SkinRibbonGalleryBarItem
    Friend WithEvents RepositoryItemComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents DefaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents FormAssistant1 As DevExpress.XtraBars.FormAssistant
    Public WithEvents SplashScreenManager1 As DevExpress.XtraSplashScreen.SplashScreenManager
    Friend WithEvents bbtnDataIntegrity As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnPMView As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnAnomaly As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnNBMgmt As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnRefCheck As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnCapacity As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnTiltMngrBulk As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnTiltMngrAdHoc As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnXML As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnNBIReports As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbtnSandBox As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents bbtnSiteIntegration As DevExpress.XtraBars.BarButtonItem
End Class
