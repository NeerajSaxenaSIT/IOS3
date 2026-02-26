Imports System.Configuration
Imports System.IO
Imports System.Threading
Imports DevExpress.XtraBars
Imports IOS.Configuration
Imports IOS.Library
Imports DevExpress.XtraBars.Ribbon

Public Class frmMDI

#Region "Variables"

    '****************** Private Variables ******************'

    Private t_Network_CountAlive As Integer
    Private t_Network_list As New List(Of Thread)
    Private t_Network_TableFinished As New List(Of String)
    Private Call_ProcessNetworks As New MethodInvoker(AddressOf Process_Networks_Invoked)
    Private dsFromODBCThreadCount As Integer = 0
    Private dsFromODBCThreadCountStart As Integer = 0
    Private t_list_ot As New List(Of Thread)

    '****************** Public Variables ******************'

    Public dt_IOS_Sources As DataTable

    '******************** Alert - Add KPI **************************
    Public Delegate Sub CallThreadInvokedAddKPI(ByRef lbl As DevExpress.XtraEditors.LabelControl, Status As Integer)

#End Region

#Region "Theme's Method"

    Sub SaveCurrentSkin()
        Try
            Dim config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            If config.AppSettings.Settings("ApplicationSkinName") Is Nothing Then
                config.AppSettings.Settings.Add("ApplicationSkinName", DefaultLookAndFeel1.LookAndFeel.SkinName)
            Else
                config.AppSettings.Settings("ApplicationSkinName").Value = DefaultLookAndFeel1.LookAndFeel.SkinName
            End If
            config.Save()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Sub LoadCurrentSkin()
        Dim config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        If config.AppSettings.Settings("ApplicationSkinName") Is Nothing Then
            Return
        Else
            DefaultLookAndFeel1.LookAndFeel.SkinName = config.AppSettings.Settings("ApplicationSkinName").Value
        End If
    End Sub

#End Region

#Region "Form Events"

    Private Sub MDIMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadCurrentSkin()
            Me.Hide()
            Me.WindowState = FormWindowState.Minimized
            Me.ShowInTaskbar = False
            Application.DoEvents()
            _logger.SetInfo("MDILoad - Start")
            _logger.SetDebug("MDILoad - Code Implementation goes here......")

            Control.CheckForIllegalCrossThreadCalls = False
            GetProxyIfAvailable()
            Dim startupsuccess As Boolean = False
            startupsuccess = SplashScreen.StartUpPhase()
            If startupsuccess = False Then
                WriteString_Log(Now() & "    " & "MDI -> Close")
                'Threading.Thread.Sleep(4000)
                SplashScreen.Close()
                GC.Collect()
                End
            End If

            ribCon_Main.Minimized = True
            ribCon_Main.Images = imgListVendors
            Try
                If Not dt_IOS_ObjectConfig Is Nothing Then
                    BindLink(dt_IOS_ObjectConfig)
                End If
            Catch
            End Try

            AddHandler Me.LocationChanged, AddressOf OnMDILocationChanged
            Me.Show()
            Me.WindowState = FormWindowState.Maximized
            Me.ShowInTaskbar = True
            WaitScreen.ShowWaitScreen("Map window loading...")
            OpenFormInDockPanel("IOS Map", "IOS Map", frmMapWindow)
            IsThreshHoldBreached()

            bbtnPCHR.Enabled = False
            bbtnPCHR.Visibility = False
            '   bbtnSiteIntegration.Enabled = False

            LoadGridColumnsConfig()

            'CreateTicketsWebAPIConfigXML()
            'CreateMapTicketsWebAPIConfigXML()

            ManageRibbonMenuDynamicButtons()
            IsInternetAvailable()

            WaitScreen.CloseWaitScreen()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            _logger.SetInfo("MDILoad - Finish")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub ManageRibbonMenuDynamicButtons()
        Dim dtMenuDynamicBtns As DataTable = Nothing
        Try
            dtMenuDynamicBtns = configData.AsEnumerable().Where(Function(x) x.Field(Of String)("ControlName").Contains("rBarBtnItemObj")).CopyToDataTable()
            For Each dr As DataRow In dtMenuDynamicBtns.Rows
                For iCntr = 0 To ribCon_Main.Manager.Items.Count - 1
                    If dr("ControlName").ToString = ribCon_Main.Items(iCntr).Name Then
                        CType(ribCon_Main.Items(iCntr), BarButtonItem).Enabled = CBool(dr("IsEnabled"))
                        CType(ribCon_Main.Items(iCntr), BarButtonItem).Visibility = IIf(CBool(dr("IsVisible")) = True, BarItemVisibility.Always, BarItemVisibility.Never)
                        Exit For
                    End If
                Next
            Next
        Catch
        Finally
            dtMenuDynamicBtns = Nothing
            'dtMenuDynamicBtns.Dispose()
        End Try
    End Sub

    Public Sub OnMDILocationChanged(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Try
            If frmTerrainProfile.Visible Then
                frmTerrainProfile.OnParentFormMove()
            End If
            If frmServiceCheck.Visible Then
                frmServiceCheck.OnParentFormMove()
            End If
            If frmQuickThemeControl.Visible Then
                frmQuickThemeControl.OnParentFormMove()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub frm_IOS_MDI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            SaveCurrentSkin()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

        Try
            SaveWorkspace()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

        Try
            SaveGridColumnsConfig()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

        Try
            ExitApplication(Nothing, Nothing)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub TabbedView1_ShowingDockGuides(sender As Object, e As Docking2010.Views.ShowingDockGuidesEventArgs) Handles TabbedView1.ShowingDockGuides
        e.Configuration.Disable(Docking2010.Customization.DockGuide.CenterDock)
    End Sub

#End Region

#Region "Technology Button Binding"

    Private Sub BindLink(ByRef dtConf As DataTable)
        Dim dtDistVendor As DataTable
        dtDistVendor = dtConf.DistinctCol({"Vendor"}).Select("Vendor<>''", "Vendor").CopyToDataTable()
        Dim lastVendor As String = ""
        Dim isTXExist As Boolean = False
        Dim isTransportExist As Boolean = False

        For Each dr As DataRow In dtDistVendor.Rows
            If lastVendor.ToLower <> dr("Vendor").ToString().ToLower Then
                lastVendor = dr("Vendor").ToString()
                Dim dtMenuObject As DataTable = dtConf.Select("Vendor='" & dr("Vendor") & "'").CopyToDataTable()
                Dim dtTech As DataTable = dtMenuObject.DistinctCol({"SubGroups"}).Select("", "SubGroups").CopyToDataTable()

                Dim isRanExist As Boolean = False

                For Each drGroup As DataRow In dtTech.Rows
                    Select Case drGroup("SubGroups").ToString.ToLower
                        Case "cem"
                            ribCon_Main.Pages.GetPageByName("rbPageCEM").Groups.Insert(0, GetRibbonPageGroup(dtConf, dr("Vendor").ToString, dr("Vendor")))
                        Case "core"
                            ribCon_Main.Pages.GetPageByName("rbPagePM").Groups.Insert(0, GetRibbonPageGroup(dtConf, drGroup("SubGroups").ToString, dr("Vendor")))
                        Case "ran 2g", "ran 3g", "ran 4g", "ran 5g", "ran node"
                            If drGroup("SubGroups").ToString.ToLower.Contains("ran") And isRanExist = False Then
                                isRanExist = True
                                ribCon_Main.Pages.GetPageByName("rbPagePM").Groups.Insert(0, GetRibbonPageGroup(dtConf, "RAN", dr("Vendor")))
                            End If
                        Case "common"
                            If drGroup("SubGroups").ToString.ToLower.Contains("common") Then
                                ribCon_Main.Pages.GetPageByName("rbPagePM").Groups.Insert(0, GetRibbonPageGroup(dtConf, "COMMON", dr("Vendor")))
                            End If
                        Case "tx"
                            If drGroup("SubGroups").ToString.ToLower.Contains("tx") And isTXExist = False Then
                                If isTXExist = False Then
                                    isTXExist = True
                                    ribCon_Main.Pages.GetPageByName("rbPagePM").Groups.Insert(0, GetRibbonPageGroup(dtConf, "TX", dr("Vendor")))
                                End If
                            End If
                        Case "transport"
                            If drGroup("SubGroups").ToString.ToLower.Contains("transport") And isTransportExist = False Then
                                If isTransportExist = False Then
                                    isTransportExist = True
                                    ribCon_Main.Pages.GetPageByName("rbPagePM").Groups.Insert(0, GetRibbonPageGroup(dtConf, "TRANSPORT", dr("Vendor")))
                                End If
                            End If
                        Case Else
                            ribCon_Main.Pages.GetPageByName("rbPagePM").Groups.Insert(0, GetRibbonPageGroup(dtConf, drGroup("SubGroups").ToString, dr("Vendor")))
                    End Select
                Next
            End If
        Next
    End Sub

    Private Function GetRibbonPageGroup(ByRef dtConf As DataTable, ByVal subGroupName As String, ByVal _vendor As String) As DevExpress.XtraBars.Ribbon.RibbonPageGroup
        Dim rbnPageGroupObj As DevExpress.XtraBars.Ribbon.RibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Dim arryVandor() As String = {"Tech", "Vendor", "TechInternal", "Technology"}
        Dim Filter As String = "Vendor='" & _vendor & "' And SubGroups='" & subGroupName & "'"

        If subGroupName.ToLower.Contains("ran") Then
            Filter = "Vendor='" & _vendor & "' And SubGroups IN('RAN 2G','RAN 3G','RAN 4G','RAN 5G','RAN NODE')"
        ElseIf subGroupName.ToLower.Contains("common") Then
            Filter = "Vendor='" & _vendor & "' And SubGroups = 'COMMON'"
        ElseIf subGroupName.ToLower.Contains("core") Then
            Filter = "Vendor='" & _vendor & "' And SubGroups = '" & subGroupName & "'"
        ElseIf subGroupName.ToLower = _vendor.ToLower Then
            Filter = "Vendor='" & _vendor & "' And SubGroups = 'CEM'"
        ElseIf subGroupName.ToLower.Contains("tx") Then
            Filter = "SubGroups = 'TX'"
        ElseIf subGroupName.ToLower.Contains("transport") Then
            Filter = "SubGroups= 'TRANSPORT'"
        ElseIf subGroupName = "" Then
            Filter = "Vendor='" & _vendor & "'"
        End If

        'If subGroupName.ToLower = _vendor.ToLower Then
        '    Filter = "Vendor='" & _vendor & "' And SubGroups = 'CEM'"
        'ElseIf subGroupName.ToLower.Contains("core") Then
        '    Filter = "Vendor='" & _vendor & "' And SubGroups = '" & subGroupName & "'"
        'ElseIf subGroupName.ToLower.Contains("ran") Then
        '    Filter = "Vendor='" & _vendor & "' And SubGroups IN('RAN 2G','RAN 3G','RAN 4G')"
        'ElseIf subGroupName = "" Then
        '    Filter = "Vendor='" & _vendor & "'"
        'End If
        Dim dtMenuObject As DataTable = dtConf.Select(Filter).CopyToDataTable()

        Dim dtTech As DataTable = dtMenuObject.DistinctCol(arryVandor).AsEnumerable().OrderBy(Function(x) x.Field(Of String)("Vendor")).ThenBy(Function(x) x.Field(Of String)("Technology")).CopyToDataTable()

        If (dtTech.IsValid) Then
            Dim rBarBtnItemObj As DevExpress.XtraBars.BarButtonItem
            For Each dr As DataRow In dtTech.Rows
                If (Not dr("Vendor").ToString = "") Then
                    rBarBtnItemObj = GetBarButtonItem(subGroupName, dr("Vendor"), dr("TechInternal"), dr("Tech"))
                    rbnPageGroupObj.ItemLinks.Add(rBarBtnItemObj)
                    Me.ribCon_Main.Items.AddRange(New DevExpress.XtraBars.BarItem() {rBarBtnItemObj})
                End If
            Next
        End If
        rbnPageGroupObj.AllowTextClipping = False
        rbnPageGroupObj.Name = "rbnPageGroupObj" & subGroupName & _vendor
        rbnPageGroupObj.ShowCaptionButton = False
        rbnPageGroupObj.Text = subGroupName
        Return rbnPageGroupObj
    End Function

    Private Function GetBarButtonItem(ByVal subGroupName As String, ByVal vendorName As String, ByVal _techInternal As String, bbtnCaption As String) As DevExpress.XtraBars.BarButtonItem
        Dim RBarBtnItemObj As DevExpress.XtraBars.BarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        RBarBtnItemObj.Caption = bbtnCaption
        RBarBtnItemObj.Description = vendorName & "_" & subGroupName
        RBarBtnItemObj.Tag = _techInternal
        RBarBtnItemObj.LargeGlyph = GetVendorImage(vendorName)
        RBarBtnItemObj.Name = "rBarBtnItemObj" & subGroupName & vendorName & _techInternal

        AddHandler RBarBtnItemObj.ItemClick, AddressOf TechnologyBtn_ItemClick
        Return RBarBtnItemObj
    End Function

    Private Sub TechnologyBtn_ItemClick(sender As Object, e As ItemClickEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim rBarBtnItemObj As DevExpress.XtraBars.BarButtonItem = TryCast(e.Item, DevExpress.XtraBars.BarButtonItem)
            If (rBarBtnItemObj IsNot Nothing) Then
                Dim strNetwork As String = rBarBtnItemObj.Caption.ToString
                OpenTechFormDynamically(strNetwork)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Function LaunchTechForm(ByVal _vendorName As String, ByVal _network As String, _baseTechnology As String, _internalTech As String, dsStats As DataSet, dsTopX As DataSet, Optional ByRef dpWindow As DevExpress.XtraBars.Docking.DockPanel = Nothing) As frmTechnology
        Dim ReloadOnStartup As Boolean = IIf(GetConfigClientKeyValue("ReloadOnStartup") = Nothing, True, GetConfigClientKeyValue("ReloadOnStartup"))

        IOS_ObjectConfig_Load_New(_network, ReloadOnStartup)
        IOS_KPITrees_Load(_network, Nothing)
        Dim activeSkinName As String = Me.DefaultLookAndFeel1.LookAndFeel.ActiveSkinName
        Dim frmTechObj As New frmTechnology()
        Me.DefaultLookAndFeel1.LookAndFeel.SetSkinStyle(activeSkinName)
        frmTechObj.Network = _network
        frmTechObj.VendorName = _vendorName
        frmTechObj.BaseTechnology = _baseTechnology
        frmTechObj.TechInternal = _internalTech
        frmTechObj.VendorStatsDS = dsStats
        frmTechObj.VendorTopXDS = dsTopX
        frmTechObj.KPITreeStatsDS = GetKPITreeByTecnology(_baseTechnology, EnumStatsOrTopX.STATS)
        frmTechObj.KPITreeTopXDS = GetKPITreeByTecnology(_baseTechnology, EnumStatsOrTopX.TOPX)
        OpenFormInDockPanel(_vendorName & "_" & _network, _network, frmTechObj, dpWindow)
        Return frmTechObj
        'objFrmTechList.Add(frmTechObj)
    End Function

    Public Sub OpenTechFormDynamically(ByVal _network As String, Optional ByRef objTech As frmTechnology = Nothing, Optional ShowDockPanelIfInstanceIsCreated As Boolean = False, Optional ByRef dpWindow As DevExpress.XtraBars.Docking.DockPanel = Nothing, Optional ByVal CloseWaitScreen As Boolean = True)
        Try
            Dim dr() As DataRow
            dr = dt_IOS_ObjectConfig.AsEnumerable().Where(Function(x) x.Field(Of String)("Tech") = _network).ToArray()
            If dr.Length > 0 Then
                Dim baseTech As String = ""
                If dr(0).Item("SubGroups") IsNot DBNull.Value Then
                    If (dr(0).Item("SubGroups").ToUpper = "CEM" Or dr(0).Item("SubGroups").ToUpper = "CORE" Or dr(0).Item("SubGroups").ToUpper = "OTX") Then
                        baseTech = dr(0).Item("TechInternal").ToString
                    Else
                        baseTech = GetBaseTechnology(_network)
                    End If
                Else
                    baseTech = GetBaseTechnology(_network)
                End If

                'If Not objFrmTechList.Exists(Function(x) x.VendorName.Equals(dr(0).Item("Vendor")) AndAlso x.BaseTechnology.Equals(baseTech)) Then
                '    WaitScreen.ShowWaitScreen(_network & " loading...")
                '    frmMapWindow.SetStatus(_network & " loading...")
                '    Application.DoEvents()
                '    Dim ObjTreeDS As DataSet = Nothing
                '    ObjTreeDS = GetObjectTreeDSByTechInternal(dr(0).Item("TechInternal").ToString)
                '    LaunchTechForm(dr(0).Item("Vendor"), _network, baseTech.ToUpper, dr(0).Item("TechInternal").ToString, ObjTreeDS, ObjTreeDS, dpWindow)
                'End If
                'objFrmTechList.OfType(Of frmTechnology)().Count(Function(f) f.VendorName.Equals(dr(0).Item("Vendor")) AndAlso f.BaseTechnology.Equals(baseTech))

                Dim existingCount = dicFrmTechInstances.Values.OfType(Of frmTechnology).Count(Function(m) m.VendorName.Equals(dr(0).Item("Vendor")) AndAlso m.BaseTechnology.Equals(baseTech))
                If existingCount < 2 Then
                    WaitScreen.ShowWaitScreen(_network & " loading...")
                    frmMapWindow.SetStatus(_network & " loading...")
                    Application.DoEvents()
                    Dim ObjTreeDS As DataSet = Nothing
                    ObjTreeDS = GetObjectTreeDSByTechInternal(dr(0).Item("TechInternal").ToString)
                    objTech = LaunchTechForm(dr(0).Item("Vendor"), _network, baseTech.ToUpper, dr(0).Item("TechInternal").ToString, ObjTreeDS, ObjTreeDS, dpWindow)

                    If (existingCount = 0) AndAlso (Not dicFrmTechInstances.Keys.Contains(existingCount.ToString & ";" & dr(0).Item("Vendor") & baseTech.ToUpper)) Then
                        objTech.InstanceKey = existingCount.ToString & ";" & dr(0).Item("Vendor").ToString.ToUpper & baseTech.ToUpper
                        dicFrmTechInstances.Add(objTech.InstanceKey, objTech)
                        objTech = dicFrmTechInstances.Values.OfType(Of frmTechnology).Where(Function(x) x.VendorName.Equals(dr(0).Item("Vendor")) AndAlso x.BaseTechnology.Equals(baseTech)).LastOrDefault()
                        objFrmTechList.Add(objTech)
                    ElseIf (existingCount = 1) AndAlso (Not dicFrmTechInstances.Keys.Contains(existingCount.ToString & ";" & dr(0).Item("Vendor") & baseTech.ToUpper)) Then
                        Dim objTechTemp = objFrmTechList.Where(Function(x) x.VendorName.Equals(dr(0).Item("Vendor")) AndAlso x.BaseTechnology.Equals(baseTech)).LastOrDefault()
                        objFrmTechList.Remove(objTechTemp)
                        objTech.InstanceKey = existingCount.ToString & ";" & dr(0).Item("Vendor").ToString.ToUpper & baseTech.ToUpper
                        dicFrmTechInstances.Add(objTech.InstanceKey, objTech)
                        objTech = dicFrmTechInstances.Values.OfType(Of frmTechnology).Where(Function(x) x.VendorName.Equals(dr(0).Item("Vendor")) AndAlso x.BaseTechnology.Equals(baseTech)).LastOrDefault()
                        objFrmTechList.Add(objTech)
                    End If
                End If

                If ShowDockPanelIfInstanceIsCreated Then
                    ShowFormIfInstanceIsAlreadyCreated(dr(0).Item("Vendor") & "_" & _network)
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

            If CloseWaitScreen = True Then
                WaitScreen.CloseWaitScreen()
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            End If

        End Try
    End Sub

#End Region

#Region "Bar Button Events"

    Private Sub bbtnDriveTestImport_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnDriveTestImport.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmImportDrivetest.Show()
            frmImportDrivetest.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnQueryBuilder_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnQueryBuilder.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmSQLQuery.Show()
            frmSQLQuery.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub bbtnGISSearch_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnGISSearch.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmGISSearch.Show()
            frmGISSearch.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub bbtnMapImport_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnMapImport.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmMapExternalData.Show()
            frmMapExternalData.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnParameterHistory_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnParameterHistory.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmParameterHistory.SetConnectionString(connStrIOSServer)
            frmParameterHistory.Show()
            frmParameterHistory.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Public Sub bbtnOpenWorkspace_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnOpenWorkspace.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Using fd As New OpenFileDialog
                fd.InitialDirectory = GetUserDataPath() & "\Data\"
                fd.DefaultExt = "mws"
                fd.Filter = "IOS Workspace|*.mws"
                fd.Title = "Open the workspace"
                fd.ShowDialog()
                If fd.FileName <> "" Then
                    Dim actualDirectory As String = fd.FileName.Replace(fd.SafeFileName, "")
                    WaitScreen.ShowWaitScreen("Workspace & layout loading...")
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    OpenLayoutAndWorkspace(actualDirectory, fd.SafeFileName.Split(".")(0).ToString)
                End If
            End Using
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub bbtnSaveWorkspace_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSaveWorkspace.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Using fd As New SaveFileDialog
                fd.InitialDirectory = GetUserDataPath() & "\Data\"
                fd.DefaultExt = "mws"
                fd.Filter = "IOS Workspace|*.mws"
                fd.Title = "Save the workspace"
                fd.ShowDialog()

                If fd.FileName <> "" Then
                    Dim wsp As New MapInfo.Persistence.WorkSpacePersistence
                    'Call the save method and supply a file path/name
                    For Each tbl In MapInfo.Engine.Session.Current.Catalog
                        If tbl.TableInfo.TableType = MapInfo.Data.TableType.AdoNet Or tbl.Alias.StartsWith("CNE_") Then
                            MapInfo.Engine.Session.Current.Catalog.CloseTable(tbl.Alias)
                        End If
                    Next
                    wsp.ApplicationName = "IOS"
                    wsp.Save(fd.FileName)
                    Dim layoutFileName As String = fd.FileName.Substring(0, fd.FileName.Length() - 4)
                    dmMDI.SaveLayoutToXml(layoutFileName & "1.xml")
                    frmMapWindow.dmMap.SaveLayoutToXml(layoutFileName & "2.xml")
                    File.WriteAllText(layoutFileName & ".skin", DefaultLookAndFeel1.LookAndFeel.ActiveSkinName)

                    'Writing each technology window (opened as the dock panel) controls values to the xml file
                    Dim itemCntr As Integer = 1
                    Dim xDoc As XDocument = XDocument.Load(layoutFileName & "1.xml")
                    For Each docPanel As Docking.DockPanel In dmMDI.Panels

                        If docPanel.Text = "IOS Map" Then Continue For

                        Dim objFrmTech As frmTechnology = dicFrmTechInstances.Values.OfType(Of frmTechnology).First(Function(x) x.Network = docPanel.Text) 'objFrmTechList.Find(Function(x) x.Network = docPanel.Text)

                        If objFrmTech IsNot Nothing Then
                            Dim xePanelsNode As XElement = xDoc.Descendants("property").Where(Function(x) x.Attribute("name").Value = "Panels" AndAlso x.Attribute("iskey").Value = "true")(0)
                            Dim xeTechNode As XElement = xePanelsNode.Nodes(itemCntr)
                            '.Descendants("property").Where(Function(x) x.Attribute("name").Value = "Item" & itemCntr.ToString AndAlso x.Attribute("isnull").Value = "true" AndAlso x.Attribute("iskey").Value = "true")(0)

                            If xeTechNode IsNot Nothing Then
                                'Stats nodes
                                Dim xeCSNStats As New XElement("property", New XAttribute("name", "ChartSetNameStats"), objFrmTech.cmbChartSetNameStats.SelectedItem.ToString)
                                Dim xeTargetTypeStats As New XElement("property", New XAttribute("name", "TargetTypeStats"), objFrmTech.cmbObjectTreeStats.SelectedItem.ToString)
                                Dim xeSelectedObjectsStats As New XElement("property", New XAttribute("name", "SelectedObjectsStats"), objFrmTech.tvObjectsTreeStats.GetChecked2String(objFrmTech._strNetwork, objFrmTech.cmbObjectTreeStats.Text, "ObjectName", objFrmTech.strTreeFilter).Substring(4).TrimEnd(")"))
                                Dim xeFilterTemplateStats As New XElement("property", New XAttribute("name", "FilterTemplateStats"), objFrmTech.cmbFilterTemplateStats.SelectedItem.ToString)
                                Dim xeFilterStringStats As New XElement("property", New XAttribute("name", "FilterStringStats"), objFrmTech.GetParamFilterStringTemplate(docPanel.Text, "stats"))
                                Dim xePSPredefTimeStats As New XElement("property", New XAttribute("name", "PSPredefTimeStats"), objFrmTech.cmbPredefTimeStats.SelectedItem.ToString)

                                Dim PSResolutionStats As String = Nothing
                                If objFrmTech.rdoHourlyStats.Checked Then
                                    PSResolutionStats = "Hourly"
                                ElseIf objFrmTech.rdoRawStats.Checked Then
                                    PSResolutionStats = "Raw"
                                ElseIf objFrmTech.rdoDailyStats.Checked Then
                                    PSResolutionStats = "Daily"
                                ElseIf objFrmTech.rdoDailyBHStats.Checked Then
                                    PSResolutionStats = "DailyBH"
                                ElseIf objFrmTech.rdoWeeklyStats.Checked Then
                                    PSResolutionStats = "Weekly"
                                ElseIf objFrmTech.rdoDailyBH2Stats.Checked Then
                                    PSResolutionStats = "DailyBH2"
                                ElseIf objFrmTech.rdoMonthlyStats.Checked Then
                                    PSResolutionStats = "Monthly"
                                End If

                                Dim xePSResolutionStats As New XElement("property", New XAttribute("name", "PSResolutionStats"), PSResolutionStats.ToString)
                                Dim xeCounterTypeStats As New XElement("property", New XAttribute("name", "CounterTypeStats"), flpSourceBtn_GetChecked(objFrmTech.Network, objFrmTech.flpCounterTypeStats)(0).SourceButtonText)
                                Dim xeShowPrdCalcLegend As New XElement("property", New XAttribute("name", "ShowPrdCalcLegend"), IIf(objFrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(0).CheckState = CheckState.Checked, "True", "False"))
                                Dim xeShowPrdCalcSeries As New XElement("property", New XAttribute("name", "ShowPrdCalcSeries"), IIf(objFrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(1).CheckState = CheckState.Checked, "True", "False"))
                                Dim xeShowPrdCalcBands As New XElement("property", New XAttribute("name", "ShowPrdCalcBands"), IIf(objFrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(2).CheckState = CheckState.Checked, "True", "False"))
                                Dim xeShowPrdCalcWeekendBands As New XElement("property", New XAttribute("name", "ShowPrdCalcWeekendBands"), IIf(objFrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(3).CheckState = CheckState.Checked, "True", "False"))
                                Dim xeShowPrdCalcHolidayBands As New XElement("property", New XAttribute("name", "ShowPrdCalcHolidayBands"), IIf(objFrmTech.prdCalcChkCmbVisuals.Properties.Items.Item(4).CheckState = CheckState.Checked, "True", "False"))

                                Dim xePrdCalcPeriodDataStats As XElement = Nothing
                                Dim prdCalcPeriodRowStats As String = Nothing
                                If objFrmTech.gvPrdCalcStats.RowCount > 0 Then
                                    For iCntr = 0 To objFrmTech.gvPrdCalcStats.RowCount - 1
                                        prdCalcPeriodRowStats &= objFrmTech.gvPrdCalcStats.GetRowCellValue(iCntr, "PeriodName") & "," & objFrmTech.gvPrdCalcStats.GetRowCellValue(iCntr, "PeriodStart") & "," & objFrmTech.gvPrdCalcStats.GetRowCellValue(iCntr, "PeriodEnd") & ";"
                                    Next
                                    prdCalcPeriodRowStats = prdCalcPeriodRowStats.TrimEnd(";")
                                    xePrdCalcPeriodDataStats = New XElement("property", New XAttribute("name", "PrdCalcPeriodDataStats"), prdCalcPeriodRowStats)
                                End If

                                xeTechNode.Add(xeCSNStats)
                                xeTechNode.Add(xeTargetTypeStats)
                                xeTechNode.Add(xeSelectedObjectsStats)
                                xeTechNode.Add(xeFilterTemplateStats)
                                xeTechNode.Add(xeFilterStringStats)
                                xeTechNode.Add(xePSPredefTimeStats)
                                xeTechNode.Add(xePSResolutionStats)
                                xeTechNode.Add(xeCounterTypeStats)
                                xeTechNode.Add(xeShowPrdCalcLegend)
                                xeTechNode.Add(xeShowPrdCalcSeries)
                                xeTechNode.Add(xeShowPrdCalcBands)
                                xeTechNode.Add(xeShowPrdCalcWeekendBands)
                                xeTechNode.Add(xeShowPrdCalcHolidayBands)
                                xeTechNode.Add(xePrdCalcPeriodDataStats)

                                'TopX nodes
                                Dim xeCSNTopX As New XElement("property", New XAttribute("name", "ChartSetNameTopX"), objFrmTech.cmbChartSetNameTopX.SelectedItem.ToString)
                                Dim xeTargetTypeTopX As New XElement("property", New XAttribute("name", "TargetTypeTopX"), objFrmTech.cmbObjectTreeTopX.SelectedItem.ToString)
                                Dim xeSelectedObjectsTopX As New XElement("property", New XAttribute("name", "SelectedObjectsTopX"), objFrmTech.tvObjectsTreeTopX.GetChecked2String("TopX_" & objFrmTech.Network, objFrmTech.cmbObjectTreeTopX.Text, "ObjectName"))
                                Dim xeFilterTemplateTopX As New XElement("property", New XAttribute("name", "FilterTemplateTopX"), objFrmTech.cmbFilterTemplateTopX.SelectedItem.ToString)
                                Dim xeFilterStringTopX As New XElement("property", New XAttribute("name", "FilterStringTopX"), objFrmTech.GetParamFilterStringTemplate(docPanel.Text, "topx"))
                                Dim xePSPredefTimeTopX As New XElement("property", New XAttribute("name", "PSPredefTimeTopX"), objFrmTech.cmbPredefTimeTopX.SelectedItem.ToString)

                                Dim PSResolutionTopX As String = Nothing
                                If objFrmTech.rdoHourlyTopX.Checked Then
                                    PSResolutionTopX = "Hourly"
                                ElseIf objFrmTech.rdoRawTopX.Checked Then
                                    PSResolutionTopX = "Raw"
                                ElseIf objFrmTech.rdoDailyTopX.Checked Then
                                    PSResolutionTopX = "Daily"
                                ElseIf objFrmTech.rdoDailyBHTopX.Checked Then
                                    PSResolutionTopX = "DailyBH"
                                ElseIf objFrmTech.rdoWeeklyTopX.Checked Then
                                    PSResolutionTopX = "Weekly"
                                ElseIf objFrmTech.rdoDailyBH2TopX.Checked Then
                                    PSResolutionTopX = "DailyBH2"
                                ElseIf objFrmTech.rdoMonthlyTopX.Checked Then
                                    PSResolutionTopX = "Monthly"
                                End If

                                Dim xePSResolutionTopX As New XElement("property", New XAttribute("name", "PSResolutionTopX"), PSResolutionTopX.ToString)
                                Dim xeShowObjectsTopX As New XElement("property", New XAttribute("name", "ShowObjectsTopX"), flpSourceBtn_GetChecked(objFrmTech.Network, objFrmTech.flpCounterTypeTopX)(0).SourceButtonText)
                                Dim xeNoOfTopX As New XElement("property", New XAttribute("name", "NoOfTopX"), objFrmTech.txtSelectXTopX.Text.Trim)
                                Dim xeTagsExcListTopXID As New XElement("property", New XAttribute("name", "TagsExcListTopXID"), objFrmTech.GetTagsExcListTopX("ID").ToString)
                                Dim xeTagsExcListTopXName As New XElement("property", New XAttribute("name", "TagsExcListTopXName"), objFrmTech.GetTagsExcListTopX("Name").ToString)
                                Dim xeChkTagsExcListEnable As New XElement("property", New XAttribute("name", "TagsExcListEnable"), objFrmTech.chkTagsExcListEnable.Checked.ToString)
                                Dim xeBTopXHideGridCols As New XElement("property", New XAttribute("name", "BTopXHideGridCols"), objFrmTech.bTopXHideGridCols.ToString)

                                xeTechNode.Add(xeCSNTopX)
                                xeTechNode.Add(xeTargetTypeTopX)
                                xeTechNode.Add(xeSelectedObjectsTopX)
                                xeTechNode.Add(xeFilterTemplateTopX)
                                xeTechNode.Add(xeFilterStringTopX)
                                xeTechNode.Add(xePSPredefTimeTopX)
                                xeTechNode.Add(xePSResolutionTopX)
                                xeTechNode.Add(xeShowObjectsTopX)
                                xeTechNode.Add(xeNoOfTopX)
                                xeTechNode.Add(xeTagsExcListTopXID)
                                xeTechNode.Add(xeTagsExcListTopXName)
                                xeTechNode.Add(xeChkTagsExcListEnable)
                                xeTechNode.Add(xeBTopXHideGridCols)

                                'Eval Nodes
                                Dim xeCSNEval As New XElement("property", New XAttribute("name", "ChartSetNameEval"), objFrmTech.cmbChartSetNameEval.SelectedItem.ToString)
                                Dim xeTargetTypeEval As New XElement("property", New XAttribute("name", "TargetTypeEval"), objFrmTech.cmbObjectTreeEval.SelectedItem.ToString)
                                Dim xeSelectedObjectsEval As New XElement("property", New XAttribute("name", "SelectedObjectsEval"), objFrmTech.tvObjectsTreeEval.GetChecked2String(objFrmTech._strNetwork, objFrmTech.cmbObjectTreeEval.Text, "ObjectName", objFrmTech.strTreeFilter).Substring(4).TrimEnd(")"))

                                Dim xePrdCalcPeriodDataEval As XElement = Nothing
                                Dim prdCalcPeriodRowEval As String = Nothing
                                If objFrmTech.gvPrdCalcEval.RowCount > 0 Then
                                    For iCntr = 0 To objFrmTech.gvPrdCalcEval.RowCount - 1
                                        prdCalcPeriodRowEval &= objFrmTech.gvPrdCalcEval.GetRowCellValue(iCntr, "PeriodName") & "," & objFrmTech.gvPrdCalcEval.GetRowCellValue(iCntr, "PeriodStart") & "," & objFrmTech.gvPrdCalcEval.GetRowCellValue(iCntr, "PeriodEnd") & ";"
                                    Next
                                    prdCalcPeriodRowEval = prdCalcPeriodRowEval.TrimEnd(";")
                                    xePrdCalcPeriodDataEval = New XElement("property", New XAttribute("name", "PrdCalcPeriodDataEval"), prdCalcPeriodRowEval)
                                End If

                                xeTechNode.Add(xeCSNEval)
                                xeTechNode.Add(xeTargetTypeEval)
                                xeTechNode.Add(xeSelectedObjectsEval)
                                xeTechNode.Add(xePrdCalcPeriodDataEval)
                            End If
                            itemCntr = itemCntr + 1
                        End If
                    Next
                    xDoc.Save(layoutFileName & "1.xml")
                End If
            End Using
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub bbtnCloseMapTable_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnCloseMapTable.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Using objDlgMapTables As New dlgMapTables()
                objDlgMapTables.StartPosition = FormStartPosition.CenterScreen
                objDlgMapTables.dlgMapTables_Setting("Close")
                objDlgMapTables.ShowDialog()
                frmMapWindow.cmb_FieldSearch_Refresh()
            End Using
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnSON_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSON.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("SON")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnPCHR_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnPCHR.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("PCHR")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnCloseAllMapTable_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnCloseAllMapTable.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            For Each mn As ToolStripMenuItem In frmMapWindow.tlb_Layer_GM.DropDownItems
                mn.Checked = False
            Next
            CType(frmMapWindow.tlb_Layer_GM.DropDownItems("TileMapType_None"), ToolStripMenuItem).Checked = True
            frmMapWindow.Mapcontrol_ActivateMapViewChange(False)
            frmTileMapping.TileMapping_None()
            frmMapWindow.Map_StatusStrip.Visible = False
            frmMapWindow.ToolStripProgressBar1.Visible = False
            MapInfo.Engine.Session.Current.Catalog.CloseAll()
            frmMapWindow.MapControl1.Map.Layers.Clear()
            frmMapWindow.vtcMapGridTable.TabPages.Clear()
            frmMapWindow.cmb_FieldSearch_Refresh()

            Dim dpList As New List(Of Docking.DockPanel)
            For Each dp As Docking.DockPanel In dmMDI.Panels
                If dp.Tag.ToString.ToLower <> "ios map" Then
                    dpList.Add(dp)
                End If
            Next
            For Each dp In dpList
                CloseDockPanelAndDisposeObject(dp)
                dp.Close()
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnDrivetest_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnDrivetest.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmImportDrivetest.Show()
            frmImportDrivetest.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub ExitApplication(sender As Object, e As EventArgs)
        Try
            frmMapWindow.PageViewsStatsUpdate()
            Try
                'If frmMapWindow.tilemenu.MenuItems IsNot Nothing Then
                '    frmMapWindow.tilemenu.MenuItems("TileMapType_None").Checked = True
                'End If
            Catch
            End Try
            Try
                frmMapWindow.Mapcontrol_ActivateMapViewChange(False)
            Catch
            End Try
            Try
                frmTileMapping.TileMapping_None()
                frmTileMapping.Close()
                frmTileMapping.Dispose()
            Catch
            End Try
            Dim openForm As Form = Nothing
            For index As Integer = My.Application.OpenForms.Count - 1 To 0 Step -1
                openForm = My.Application.OpenForms.Item(index)
                If openForm IsNot Me And openForm IsNot sender Then
                    openForm.Close()
                    openForm.Dispose()
                    openForm = Nothing
                End If
            Next
            GC.Collect()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub Exit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles rbarBtn_Exit.ItemClick, bbtnExit.ItemClick
        Me.Close()
    End Sub

    Private Sub bbtnDashboard_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnDashboard.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Dashboard")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnReports_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnReports.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmReportEdit.Show()
            frmReportEdit.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnTags_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnTags.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmTagManager.Show()
            frmTagManager.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnCM_ItemClick(sender As Object, e As ItemClickEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Application.DoEvents()
            frmCMTemplate.Show()
            frmCMTemplate.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnPMKPIs_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnPMKPIs.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("KPI Manager")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnChart_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnChart.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Chart Customization")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnICM_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnICM.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmICM.Show()
            frmICM.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnVirtualAzimuthDetection_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnVirtualAzimuthDetection.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Virtual Azimuth")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnChangeFeatures_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnChangeFeatures.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            dlgSettingsNetworkFeatures.Show()
            dlgSettingsNetworkFeatures.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnSyncAll_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSyncAll.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (Interaction.MsgBox("This will take a minute ...Press Ok", MsgBoxStyle.OkCancel, Nothing) = MsgBoxResult.Ok) Then
                frmMapWindow.lbl_GetNetworks_Status.Text = " Synchronizing Tree Objects "
                Application.DoEvents()
                frmMapWindow.Refresh_IOSSQL()
                Me.bbtnSyncConsoleObjects_ItemClick(Nothing, Nothing)
                frmMapWindow.lbl_GetNetworks_Status.Text = " Synchronizing Map Objects "
                Me.bbtnSyncMapObjects_ItemClick(Nothing, Nothing)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnSyncConsoleObjects_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSyncConsoleObjects.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            IOS_ObjectConfig_Load("ALL", True)
            IOS_KPITrees_Load("ALL")

            Application.DoEvents()
            Exit Sub
            ' ''--------------
            ' ''Below is a multithreaded unmanaged way of getting the objecttree. Only used in tmnl.
            ' ''----------------
            Me.dsFromODBCThreadCount = 0
            Me.dsFromODBCThreadCountStart = 0
            Me.t_list_ot.Clear()
            Dim parray As String()() = Nothing
            Dim strArray2 As String()() = Nothing

            Me.GetDataSetFromODBC_Threaded(GetSQL(1009, parray)(0), GetSQL(&H3F1, strArray2)(1), "dsTree3G_wcel")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1017, strArray2)(0), GetSQL(&H3F9, parray)(1), "dsTree3G_wbts")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1018, strArray2)(0), GetSQL(&H3FA, parray)(1), "dsTree3G_rnc")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1019, strArray2)(0), GetSQL(&H3FB, parray)(1), "dsTree2G_bts")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1016, strArray2)(0), GetSQL(&H3F8, parray)(1), "dsTree2G_cel")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1024, strArray2)(0), GetSQL(&H400, parray)(1), "dsTree2G_bcf")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1020, strArray2)(0), GetSQL(&H3FC, parray)(1), "dsTree2G_bsc")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1026, strArray2)(0), GetSQL(&H402, parray)(1), "dsTree2G_zone")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1026, strArray2)(0), GetSQL(&H402, parray)(1), "dsTree3G_zone")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1025, strArray2)(0), GetSQL(&H401, parray)(1), "dsTree2G_region")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1025, strArray2)(0), GetSQL(&H401, parray)(1), "dsTree3G_region")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1053, strArray2)(0), GetSQL(&H41D, parray)(1), "dsTree3G_VPI")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1052, strArray2)(0), GetSQL(&H41C, parray)(1), "dsTree3G_VCI")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1073, strArray2)(0), GetSQL(&H431, parray)(1), "dsTree2G_mr")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1066, strArray2)(0), GetSQL(&H42A, parray)(1), "dsTree3G_mr")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(9000, strArray2)(0), GetSQL(&H2328, parray)(1), "dsTreeNanoBTS_cel")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(9001, strArray2)(0), GetSQL(&H2329, parray)(1), "dsTreeNanoBTS_site")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(9002, strArray2)(0), GetSQL(&H232A, parray)(1), "dsTreeNanoBTS_bsc")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(6001, strArray2)(0), GetSQL(&H1771, parray)(1), "dt_IOS_OSSParams")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1027, strArray2)(0), GetSQL(&H403, parray)(1), "ds_OSS_3G_UserParams")
            strArray2 = Nothing
            parray = Nothing
            Me.GetDataSetFromODBC_Threaded(GetSQL(1030, strArray2)(0), GetSQL(&H406, parray)(1), "ds_OSS_2G_UserParams")
            Dim now As DateTime = DateAndTime.Now
            Dim flag As Boolean = True
            Do While flag
                flag = False
                Dim thread As Thread
                For Each thread In Me.t_list_ot
                    If thread.IsAlive Then
                        flag = True
                    End If
                Next
                If (DateAndTime.Now.Subtract(now).TotalSeconds < 180) Then
                    Application.DoEvents()
                Else
                    Interaction.MsgBox("Sync Failed due to TimeOut", MsgBoxStyle.OkOnly, Nothing)
                    Dim thread2 As Thread
                    For Each thread2 In Me.t_list_ot
                        thread2.Abort()
                    Next
                    Return
                End If
            Loop

            ObjectTree_DataSet_Load("sgsn", False, dt_IOS_Sources)
            ObjectTree_DataSet_Load("mss", False, dt_IOS_Sources)
            ObjectTree_DataSet_Load("rnc", False)
            ObjectTree_DataSet_Load("wbts", False)
            ObjectTree_DataSet_Load("wcel", False)
            ObjectTree_DataSet_Load("mr", False)
            ObjectTree_DataSet_Load("vpi", False)
            ObjectTree_DataSet_Load("vci", False)
            ObjectTree_DataSet_Load("bsc", False)
            ObjectTree_DataSet_Load("bcf", False)
            ObjectTree_DataSet_Load("bts", False)
            ObjectTree_DataSet_Load("cell", False)
            ObjectTree_DataSet_Load("zone", False)
            ObjectTree_DataSet_Load("nanobts_bsc", False)
            ObjectTree_DataSet_Load("nanobts_site", False)
            ObjectTree_DataSet_Load("nanobts_cell", False)
            frmMapWindow.Preparation_Parameters_OSS(False)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub GetDataSetFromODBC_Threaded(ByVal connstring As String, ByVal sql As String, ByVal datasetName As String)
        Try
            Me.dsFromODBCThreadCountStart += 1
            Dim now As DateTime = DateAndTime.Now
            Do While ((Me.dsFromODBCThreadCountStart > 5) And (DateAndTime.Now.Subtract(now).Seconds < 30))
                Application.DoEvents()
            Loop
            Dim stats As New Thread_Stats
            Dim item As New Thread(New ThreadStart(AddressOf stats.GetData))
            stats.connstring = connstring
            stats.sql_total = sql

            AddHandler stats.ThreadComplete, AddressOf Process_GetDataSetFromODBC_ThreadEnd
            item.Name = datasetName
            item.Start()
            Me.t_list_ot.Add(item)
        Catch exception1 As Exception

        End Try
    End Sub

    Private Sub Process_GetDataSetFromODBC_ThreadEnd(ByVal ds As DataSet, ByVal ti As Thread)
        Try
            If (Not ds Is Nothing) Then
                ds.WriteXml((GetUserDataPath() & "\" & ti.Name & ".xml"), XmlWriteMode.WriteSchema)
                Me.dsFromODBCThreadCount += 1
                Me.dsFromODBCThreadCountStart -= 1
            End If
            If (Not ds Is Nothing) Then
                ds.Dispose()
                ds = Nothing
            End If
        Catch exception1 As Exception
        End Try
    End Sub

    Private Sub bbtnSyncMapObjects_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSyncMapObjects.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If Not t_Network_list Is Nothing Then
                Me.t_Network_list.Clear()
                Me.t_Network_TableFinished.Clear()
            End If

            Me.t_Network_CountAlive = 0
            frmMapWindow.lbl_GetNetworks_Status.Text = ""
            Application.DoEvents()
            Dim boldedDates() As Date = Nothing  '= objFrmMap.Calendar_GetNetworks.BoldedDates
            Dim time As Date = boldedDates(1)
            Dim index As Integer = Array.IndexOf(Of DateTime)(boldedDates, time)
            Dim strings2 As New System.Collections.Specialized.StringCollection
            If (index > -1) Then
                Dim enumerator As IEnumerator = Nothing
                Try
                    enumerator = dt_Map_Configuration.Rows.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        frmMapWindow.lbl_GetNetworks_Status.Text = ("status: " & current.Item("layername").ToString & ": fetching data...")
                        frmMapWindow.pbcGetNetworks.EditValue = 0
                        Application.DoEvents()
                        Dim replacement As String = ("'" & time.ToString("yyyymmdd") & "'")
                        Dim str2 As String = Replace(current.Item("layersql").ToString, "@networkdate", replacement, 1, -1, CompareMethod.Binary)
                        strings2.Add(current.Item("layername").ToString)
                        Dim maps As ThreadGetMaps = New ThreadGetMaps
                        Dim item As New Thread(New ThreadStart(AddressOf maps.LoadNetwork))
                        maps.connstring = connStrIOSServer
                        maps.sql_total = str2
                        maps.LayerConfig = current
                        maps.date_selected = time
                        maps.csysWGS84 = frmMapWindow.MapControl1.Map.GetDisplayCoordSys
                        maps.TableName = current.Item("layername").ToString

                        AddHandler maps.Thread_GetMaps_Complete, AddressOf Process_Networks_ThreadEnd

                        item.Name = current.Item("layername").ToString
                        item.Start()
                        Me.t_Network_list.Add(item)
                        Me.t_Network_CountAlive += 1
                    Loop
                Catch
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
            Else
                Interaction.MsgBox("Select BOLD date only", MsgBoxStyle.OkOnly, Nothing)
                Return
            End If
            frmMapWindow.lbl_GetNetworks_Status.Text = "Query Started..." & Convert.ToString(Me.t_Network_list.Count) & " Threads"
            frmMapWindow.lbl_GetNetworks_Status.ForeColor = Color.Black
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Public Sub Process_Networks_ThreadEnd(ByVal tblname As String, ByVal tid As Thread)
        Try
            Application.DoEvents()
            Me.t_Network_TableFinished.Add(tblname)
            Me.BeginInvoke(Me.Call_ProcessNetworks)
        Catch exception1 As Exception
        End Try
    End Sub

    Public Sub Process_Networks_Invoked()
        Me.t_Network_CountAlive -= 1
        frmMapWindow.lbl_GetNetworks_Status.Text = "Threads Remaining: " & Convert.ToString(Me.t_Network_CountAlive)
        Application.DoEvents()
        frmMapWindow.pbcGetNetworks.EditValue = CInt(Math.Round(CDbl((frmMapWindow.pbcGetNetworks.EditValue + (100 / CDbl(Me.t_Network_list.Count))))))
        frmMapWindow.LoadNetwork_Add2Map(Me.t_Network_TableFinished.Item((Me.t_Network_TableFinished.Count - 1)))
        If (Me.t_Network_CountAlive = 0) Then
            frmMapWindow.lbl_GetNetworks_Status.Text = "Finished"
            Application.DoEvents()
        End If
    End Sub

    Private Sub bbtnSyncClientConfig_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSyncClientConfig.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            frmMapWindow.Refresh_IOSSQL()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnMapWindow_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnMapWindow.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            frmMapWindow.Show()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnSelectionInfo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSelectionInfo.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Selection Info")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnTicketWindow_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnTicketWindow.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            frmTicketDetail.Show()
            frmTicketDetail.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnPortalHelp_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnPortalHelp.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim webAddress As String = "http://www.cellsens.com/FlySpray"
            Process.Start(webAddress)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnManualHelp_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnManualHelp.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            System.Diagnostics.Process.Start(Application.StartupPath & "\" & "CIOS_UserManual.chm")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnAbout_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnAbout.ItemClick
        AboutBox.Show()
    End Sub

    Private Sub bbtnOpenInternet_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnWebClient.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Web Client")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnSandBox_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSandBox.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("DataMart")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnCMSchedule_ItemClick(sender As Object, e As ItemClickEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmCMScheduler.Show()
            frmCMScheduler.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnCMView_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnCMView.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("CM View")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnPMView_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnPMView.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("PM View")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnNBMgmt_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnNBMgmt.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Neighbor Management")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnRefCheck_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnRefCheck.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Ref Check")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnTiltMngrBulk_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnTiltMngrBulk.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            tiltMngrType = "TMBULK"
            selectedTiltCampaignID = 0
            OpenFormAsDockPanel("Tilt Manager")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnDataIntegrity_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnDataIntegrity.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            frmDataIntegrity.Show()
            frmDataIntegrity.BringToFront()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnAnomaly_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnAnomaly.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Anomaly Detection")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnCapacity_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnCapacity.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Capacity")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnXML_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnXML.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("XML")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub bbtnNBIReports_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnNBIReports.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("NBI Reports")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "MDI Helper"

    Public Sub OpenFormAsDockPanel(ByVal formKey As String, Optional ByRef dpWindow As DevExpress.XtraBars.Docking.DockPanel = Nothing, Optional formObject As Form = Nothing, Optional navigateUrl As String = Nothing)
        Try
            If ShowFormIfInstanceIsAlreadyCreated(formKey) Then Exit Sub
            Select Case formKey.ToUpper
                Case "IOS Map".ToUpper
                    InilializeMapGetNetworkCalendar()
                    OpenFormInDockPanel("IOS Map", "IOS Map", frmMapWindow, dpWindow)
                Case "SON".ToUpper
                    WaitScreen.ShowWaitScreen("SON loading...")
                    objfrmSON = New frmSON()
                    objfrmSON.networksAll = networkAll
                    objfrmSON.Jobs_Load_Param()
                    objfrmSON.Jobs_Load_Inconsist()
                    OpenFormInDockPanel("SON", "SON", objfrmSON, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "PCHR".ToUpper
                    WaitScreen.ShowWaitScreen("PCHR loading...")
                    objFrmPCHR = New frmTracePCHR()
                    OpenFormInDockPanel("PCHR", "PCHR", objFrmPCHR, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "DataMart".ToUpper
                    WaitScreen.ShowWaitScreen("DataMart loading...")
                    objSandbox = New frmSBMain()
                    OpenFormInDockPanel("DataMart", "DataMart", objSandbox, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Web Client".ToUpper
                    Dim frmObj As New frmInternetExplorer()
                    OpenFormInDockPanel("Web Client", "Web Client", frmObj, dpWindow)
                Case "DT Parser Web Client".ToUpper
                    WaitScreen.ShowWaitScreen("DT Parser Web Client loading...")
                    OpenFormInDockPanel("DT Parser Web Client", "DT Parser Web Client", formObject, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Chart Customization".ToUpper
                    WaitScreen.ShowWaitScreen("Chart Customization loading...")
                    OpenFormInDockPanel("Chart Customization", "Chart Customization", frmChartCustomization, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "KPI Manager".ToUpper
                    WaitScreen.ShowWaitScreen("KPI Manager loading...")
                    Dim objfrm As New frmKPIManage()
                    OpenFormInDockPanel("KPI Manager", "KPI Manager", objfrm, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "CM View".ToUpper
                    WaitScreen.ShowWaitScreen("CM View loading...")
                    Dim objfrm As New frmCMView()
                    OpenFormInDockPanel("CM View", "CM View", objfrm, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Neighbor Management".ToUpper
                    WaitScreen.ShowWaitScreen("Neighbor Management loading...")
                    Dim objfrm As New frmNBManagement()
                    OpenFormInDockPanel("Neighbor Management", "Neighbor Management", objfrm, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "PM View".ToUpper
                    WaitScreen.ShowWaitScreen("PM View loading...")
                    Dim objfrm As New frmPMView()
                    OpenFormInDockPanel("PM View", "PM View", objfrm, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Selection Info".ToUpper
                    WaitScreen.ShowWaitScreen("Selection Info loading...")
                    OpenFormInDockPanel("Selection Info", "Selection Info", dlgMappingSelection, dpWindow, False)
                    WaitScreen.CloseWaitScreen()
                Case "Quick Layer Control".ToUpper
                    WaitScreen.ShowWaitScreen("Quick layer control loading...")
                    OpenFormInDockPanel("Quick Layer Control", "Quick Layer Control", frmQuickThemeControl, dpWindow, False)
                    WaitScreen.CloseWaitScreen()
                Case "Virtual Azimuth".ToUpper
                    WaitScreen.ShowWaitScreen("Virtual Azimuth loading...")
                    OpenFormInDockPanel("Virtual Azimuth", "Virtual Azimuth", frmVirtualAzimuth, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Dashboard".ToUpper
                    WaitScreen.ShowWaitScreen("Dashboard loading...")
                    Dim objfrm As New frmDashboard()
                    OpenFormInDockPanel("Dashboard", "Dashboard", objfrm, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Data Integrity".ToUpper
                    WaitScreen.ShowWaitScreen("Data integrity loading...")
                    OpenFormInDockPanel("Data Integrity", "Data Integrity", frmDataIntegrity, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Anomaly Detection".ToUpper
                    WaitScreen.ShowWaitScreen("Anomaly Detection loading...")
                    OpenFormInDockPanel("Anomaly Detection", "Anomaly Detection", frmAnomaly, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Ref Check".ToUpper
                    WaitScreen.ShowWaitScreen("Ref Check loading...")
                    OpenFormInDockPanel("Ref Check", "Ref Check", frmRefCheck, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Capacity".ToUpper
                    WaitScreen.ShowWaitScreen("Capacity Management loading...")
                    Dim objfrm As New frmCapacity()
                    OpenFormInDockPanel("Capacity", "Capacity", objfrm, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Tilt Manager".ToUpper
                    WaitScreen.ShowWaitScreen("Tilt Manager loading...")
                    If objFrmAdHocTiltMngr Is Nothing Then
                        objFrmAdHocTiltMngr = New frmTiltManagement()
                    End If
                    OpenFormInDockPanel("Tilt Manager", "Tilt Manager", objFrmAdHocTiltMngr, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Xml".ToUpper
                    WaitScreen.ShowWaitScreen("CM XML loading...")
                    OpenFormInDockPanel("XML", "XML", frmGenerateXML, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "NBI Reports".ToUpper
                    WaitScreen.ShowWaitScreen("NBI Reports loading...")
                    OpenFormInDockPanel("NBI Reports", "NBI Reports", frmNBIReports, dpWindow)
                    WaitScreen.CloseWaitScreen()
                Case "Tickets Web Url".ToUpper
                    Dim frmObj As New frmInternetExplorer("tickets", navigateUrl)
                    OpenFormInDockPanel("Tickets Web Url", "Tickets Web Url", frmObj, dpWindow)
                Case "NBIReports Web Url".ToUpper
                    Dim frmObj As New frmInternetExplorer("NBIReports", navigateUrl)
                    OpenFormInDockPanel("NBIReports Web Url", "NBIReports Web Url", frmObj, dpWindow)
                Case "PM TopX Ticket Url".ToUpper
                    Dim frmObj As New frmInternetExplorer("PMTopX", navigateUrl)
                    OpenFormInDockPanel("PM TopX Ticket Url", "PM TopX Ticket Url", frmObj, dpWindow)
                Case "Report Editor WebLink".ToUpper
                    Dim frmObj As New frmInternetExplorer("ReportEditor", navigateUrl)
                    OpenFormInDockPanel("Report Editor WebLink", "Report Editor WebLink", frmObj, dpWindow)
                Case "Site Integration".ToUpper
                    WaitScreen.ShowWaitScreen("Site Integration Loading...")
                    OpenFormInDockPanel("Site Integration", "Site Integration", frmSiteIntegration, dpWindow)
                    WaitScreen.CloseWaitScreen()
            End Select
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub ConfigurIOSMDI(ByVal frmName As String)
        Try
            Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
            If Not form Is Nothing Then
                Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing

                Dim formControls As List(Of BarButtonItem) = New List(Of BarButtonItem) From {
                    bbtnOpenWorkspace, bbtnSaveWorkspace, bbtnOpenMapTable, bbtnCloseMapTable, bbtnCloseAllMapTable, bbtnExit, bbtnPMKPIs, bbtnChart, bbtnTags, bbtnPCHR, bbtnDrivetest,
                    bbtnParameterHistory, bbtnCMView, bbtnDashboard, bbtnReports, bbtnICM, bbtnSON, bbtnVirtualAzimuthDetection, bbtnQueryBuilder, bbtnGISSearch, bbtnMapImport, bbtnSyncClientConfig,
                    bbtnWebClient, bbtnSandBox, bbtnPortalHelp, bbtnManualHelp, bbtnAbout, bbtnNBMgmt, bbtnAnomaly, bbtnRefCheck, bbtnPMView, bbtnCapacity, bbtnTiltMngrBulk, bbtnTiltMngrAdHoc, bbtnXML,
                    bbtnNBIReports, bbtnSiteIntegration
                }

                For Each frmControl As BarButtonItem In formControls
                    winCtrl = form.FindControlByName(frmControl.Name)
                    If Not winCtrl Is Nothing Then
                        frmControl.Enabled = winCtrl.DefaultEnable
                        frmControl.Visibility = IIf(winCtrl.DefaultVisible, BarItemVisibility.Always, BarItemVisibility.Never)
                    End If
                Next

                Dim formControlsRB As List(Of RibbonPage) = New List(Of RibbonPage) From {
                    rbPageFile, rbPagePM, rbPageCEM, rbPageCM, rbPageTools, rbPageHelp
                }

                For Each frmControl As RibbonPage In formControlsRB
                    winCtrl = form.FindControlByName(frmControl.Name)
                    If Not winCtrl Is Nothing Then
                        'frmControl.enabled = winCtrl.DefaultEnable
                        frmControl.Visible = winCtrl.DefaultVisible
                    End If
                Next

            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Function ShowFormIfInstanceIsAlreadyCreated(keys As String) As Boolean
        Try
            For Each dp As DevExpress.XtraBars.Docking.DockPanel In dmMDI.Panels
                If dp.Tag Is Nothing Then Continue For
                If dp.Tag.ToString.ToUpper = keys.ToUpper Then
                    If dp.Tag = "Web Client" Then
                        dp.Dispose()
                        dp.Close()
                        Return False
                    End If
                    dp.Show()
                    Application.DoEvents()
                    Return True
                End If
            Next
            For Each dp As DevExpress.XtraBars.Docking.DockPanel In frmMapWindow.dmMap.Panels
                If dp.Tag Is Nothing Then Continue For
                If dp.Tag.ToString.ToUpper = keys.ToUpper Then
                    dp.Show()
                    Application.DoEvents()
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Sub InilializeMapGetNetworkCalendar()
        frmMapWindow.Calendar_GetNetwork_Fill_From_DB()
        frmMapWindow.Calendar_GetNetworks_Fill()
    End Sub

    Public Sub CloseDockPanelAndDisposeObject(ByRef dpWindow As DevExpress.XtraBars.Docking.DockPanel)
        Try
            RemoveHandler dpWindow.ClosingPanel, AddressOf DockManager1_ClosingPanel

            If dpWindow.ControlContainer IsNot Nothing Then
                For Each obj As Object In dpWindow.ControlContainer.Controls.OfType(Of Form)()
                    If TypeOf (obj) Is frmTechnology Then
                        If objFrmTechList.Exists(Function(x) x.VendorName.Equals(obj.VendorName) AndAlso x.BaseTechnology.Equals(obj.BaseTechnology)) Then
                            DirectCast(obj, frmTechnology).ReleaseMemoryOfTechnologyStatsInstance()
                            Dim key As String = TryCast(obj, frmTechnology).InstanceKey
                            dicFrmTechInstances.Remove(key)
                            objFrmTechList.Remove(obj)
                            ResetNetworkAll(obj.TechInternal)
                            obj.Dispose()
                        End If
                    ElseIf TypeOf (obj) Is frmICM Then
                        objFrmICM.Dispose()
                        objFrmICM = Nothing
                    ElseIf TypeOf (obj) Is frmSON Then
                        objfrmSON.Dispose()
                        objfrmSON = Nothing
                    ElseIf TypeOf (obj) Is frmTracePCHR Then
                        objFrmPCHR.Dispose()
                        objFrmPCHR = Nothing
                    ElseIf TypeOf (obj) Is frmParameterHistory Then
                        objFrmParamHistory.Dispose()
                        objFrmParamHistory = Nothing
                    ElseIf TypeOf (obj) Is dlgMappingSelection Then
                        frmMapWindow.tlb_SelInfo.Checked = False
                    ElseIf TypeOf (obj) Is frmQuickThemeControl Then
                        frmMapWindow.tbl_QuickTheme.Checked = False
                    ElseIf TypeOf (obj) Is frmLegend Then
                        frmMapWindow.tlb_Legend.Checked = False
                    ElseIf TypeOf (obj) Is frmInternetExplorer Then
                        If objDTParserWebClient IsNot Nothing Then
                            objDTParserWebClient.Dispose()
                            objDTParserWebClient = Nothing
                        End If
                        If objTechTicketsUrlWC IsNot Nothing Then
                            objTechTicketsUrlWC.Dispose()
                            objTechTicketsUrlWC = Nothing
                        End If
                        If objReportEditWebLinkWC IsNot Nothing Then
                            objReportEditWebLinkWC.Dispose()
                            objReportEditWebLinkWC = Nothing
                        End If
                    ElseIf TypeOf (obj) Is frmTiltManagement Then
                        objFrmAdHocTiltMngr.Dispose()
                        objFrmAdHocTiltMngr = Nothing
                    ElseIf TypeOf (obj) Is frmNBManagement Then
                        obj = Nothing
                    End If
                Next
            Else
                For Each ctrl As Object In dpWindow.Controls
                    If ctrl.ControlContainer IsNot Nothing Then
                        For Each objFrm As Object In ctrl.ControlContainer.Controls
                            If TypeOf (objFrm) Is frmTechnology Then
                                If objFrmTechList.Exists(Function(x) x.VendorName.Equals(TryCast(objFrm, frmTechnology).VendorName) AndAlso x.BaseTechnology.Equals(TryCast(objFrm, frmTechnology).BaseTechnology)) Then
                                    DirectCast(objFrm, frmTechnology).ReleaseMemoryOfTechnologyStatsInstance()
                                    Dim key As String = TryCast(objFrm, frmTechnology).InstanceKey
                                    dicFrmTechInstances.Remove(key)
                                    objFrmTechList.Remove(objFrm)
                                    ResetNetworkAll(objFrm.TechInternal)
                                    objFrm.Dispose()
                                End If
                            ElseIf TypeOf (objFrm) Is frmICM Then
                                objFrmICM.Dispose()
                                objFrmICM = Nothing
                            ElseIf TypeOf (objFrm) Is frmSON Then
                                objfrmSON.Dispose()
                                objfrmSON = Nothing
                            ElseIf TypeOf (objFrm) Is frmTracePCHR Then
                                objFrmPCHR.Dispose()
                                objFrmPCHR = Nothing
                            ElseIf TypeOf (objFrm) Is frmParameterHistory Then
                                objFrmParamHistory.Dispose()
                                objFrmParamHistory = Nothing
                            ElseIf TypeOf (objFrm) Is frmInternetExplorer Then
                                If objDTParserWebClient IsNot Nothing Then
                                    objDTParserWebClient.Dispose()
                                    objDTParserWebClient = Nothing
                                End If
                                If objTechTicketsUrlWC IsNot Nothing Then
                                    objTechTicketsUrlWC.Dispose()
                                    objTechTicketsUrlWC = Nothing
                                End If
                                If objReportEditWebLinkWC IsNot Nothing Then
                                    objReportEditWebLinkWC.Dispose()
                                    objReportEditWebLinkWC = Nothing
                                End If
                            ElseIf TypeOf (objFrm) Is dlgMappingSelection Then
                                frmMapWindow.tlb_SelInfo.Checked = False
                            ElseIf TypeOf (objFrm) Is frmQuickThemeControl Then
                                frmMapWindow.tbl_QuickTheme.Checked = False
                            ElseIf TypeOf (objFrm) Is frmLegend Then
                                frmMapWindow.tlb_Legend.Checked = False
                            End If
                        Next
                    Else
                        CloseDockPanelAndDisposeObject(ctrl)
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Error")
        End Try
    End Sub

    Public Sub OpenDockPanelWithFormObjectForMDI()
        objFrmTechList.Clear()
        dicFrmTechInstances.Clear()
        Dim panelCount As Integer = dmMDI.Panels.Count
        Dim dpList(panelCount - 1) As Object

        For i As Integer = 0 To panelCount - 1
            dpList(i) = dmMDI.Panels.Item(i)
        Next

        For i As Integer = 0 To dpList.Length - 1
            If dpList(i).ControlContainer IsNot Nothing Then
                If dt_IOS_ObjectConfig.Select("Tech='" & dpList(i).Text & "'").Count > 0 Then
                    OpenTechFormDynamically(dpList(i).Text, , , dpList(i))
                Else
                    OpenFormAsDockPanel(dpList(i).Text, dpList(i))
                End If
            End If
        Next
    End Sub

    Public Sub OpenDockPanelWithFormObjectForMap()
        Dim panelCount As Integer = frmMapWindow.dmMap.Panels.Count
        Dim dpList(panelCount - 1) As Object

        For i As Integer = 0 To panelCount - 1
            dpList(i) = frmMapWindow.dmMap.Panels.Item(i)
        Next

        For i As Integer = 0 To dpList.Length - 1
            If dpList(i).ControlContainer IsNot Nothing Then
                If dt_IOS_ObjectConfig.Select("Tech='" & dpList(i).Text & "'").Count > 0 Then
                    OpenTechFormDynamically(dpList(i).Text, , , dpList(i))
                Else
                    OpenFormAsDockPanel(dpList(i).Text, dpList(i))
                End If
            End If
        Next
    End Sub

    Private Sub DockPanel_DockChanged(sender As Object, e As EventArgs)
        Try
            Dim dpWindow As DevExpress.XtraBars.Docking.DockPanel = CType(sender, DevExpress.XtraBars.Docking.DockPanel)
            Dim obj As Form = dpWindow.ControlContainer.Controls(0)
            If dpWindow.FloatForm IsNot Nothing Then
                dpWindow.FloatForm.MinimumSize = New Size(obj.MinimumSize.Width + 20, obj.MinimumSize.Height + 40)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub DockManager1_ShowingDockGuides(sender As Object, e As Docking.ShowingDockGuidesEventArgs) Handles dmMDI.ShowingDockGuides
        e.Configuration.Disable(Docking2010.Customization.DockGuide.CenterDock)
    End Sub

    Private Sub DockManager1_ClosingPanel(sender As Object, e As Docking.DockPanelCancelEventArgs) Handles dmMDI.ClosingPanel
        Try
            Dim dpWindow As Docking.DockPanel = CType(e.Panel, Docking.DockPanel)
            CloseDockPanelAndDisposeObject(dpWindow)
            dpWindow.Dispose()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Function OpenFormInDockPanel(keys As String, sText As String, ByRef frm As System.Windows.Forms.Form, Optional ByRef dpWindow As DevExpress.XtraBars.Docking.DockPanel = Nothing, Optional IsPanelFill As Boolean = True) As DevExpress.XtraBars.Docking.DockPanel
        Try
            If dpWindow Is Nothing Then
                If IsPanelFill = True Then
                    dpWindow = dmMDI.AddPanel(Docking.DockingStyle.Float)
                    dpWindow.DockAsMdiDocument()
                    dpWindow.DockedAsTabbedDocument = True

                    dmMDI.AutoHideSpeed = 100
                    dmMDI.DockingOptions.HideImmediatelyOnAutoHide = True
                Else
                    dpWindow = frmMapWindow.dmMap.AddPanel(Docking.DockingStyle.Right)
                    If keys = "Quick Layer Control" Then
                        dpWindow.MinimumSize = New Size(350, 688)
                        dpWindow.Size = New Size(350, 688)
                        dpWindow.FloatSize = New Size(350, 688)
                    ElseIf keys = "Selection Info" Then
                        dpWindow.MinimumSize = New Size(450, 550)
                        dpWindow.Size = New Size(500, 550)
                        dpWindow.FloatSize = New Size(450, 550)
                    End If

                    frmMapWindow.dmMap.AutoHideSpeed = 30
                    frmMapWindow.dmMap.DockingOptions.HideImmediatelyOnAutoHide = True
                End If
            End If
            dpWindow.SuspendLayout()
            dpWindow.Text = sText
            dpWindow.Tag = keys
            dpWindow.AccessibleDescription = keys
            dpWindow.Options.AllowFloating = True

            If keys = "IOS Map" Then
                dpWindow.Options.ShowCloseButton = False
            Else
                dpWindow.Options.ShowCloseButton = True
            End If

            AddHandler dpWindow.DockChanged, AddressOf DockPanel_DockChanged
            AddHandler dpWindow.ClosingPanel, AddressOf DockManager1_ClosingPanel

            frm.MdiParent = Me
            frm.Dock = DockStyle.Fill
            frm.FormBorderStyle = FormBorderStyle.None
            frm.Tag = keys
            dpWindow.ControlContainer.Controls.Add(frm)
            frm.Show()
            dpWindow.ResumeLayout()
            Return dpWindow
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Error")
        End Try
        Return Nothing
    End Function

    Public Function GetKPITreeByTecnology(ByVal baseTech As String, ByVal statsOrTopX As EnumStatsOrTopX) As Dictionary(Of String, DataSet)
        Dim dicKPI As Dictionary(Of String, DataSet) = New Dictionary(Of String, DataSet)
        If (statsOrTopX = EnumStatsOrTopX.STATS) Then
            If (baseTech = BaseTechnology.Tech2G) Then
                dicKPI.Add(IOSInternalTechnology.Tech2G1, dsTree2G_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech2G2, dsTreeNanoBTS_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech2G3, dsTree2G3_Kpi)
            ElseIf (baseTech = BaseTechnology.Tech3G) Then
                dicKPI.Add(IOSInternalTechnology.Tech3G1, dsTree3G_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech3G2, dsTreeNano3G_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech3G3, dsTree3G3_Kpi)
            ElseIf (baseTech = BaseTechnology.Tech4G) Then
                dicKPI.Add(IOSInternalTechnology.Tech4G1, dsTree4G1_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech4G2, dsTree4G2_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech4G3, dsTree4G3_Kpi)
            ElseIf (baseTech = BaseTechnology.Tech5G) Then
                dicKPI.Add(IOSInternalTechnology.Tech5G1, dsTree5G1_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech5G2, dsTree5G2_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech5G3, dsTree5G3_Kpi)
            ElseIf (baseTech = BaseTechnology.TechNode) Then
                dicKPI.Add(IOSInternalTechnology.TechNode1, dsTreeNode1_Kpi)
                dicKPI.Add(IOSInternalTechnology.TechNode2, dsTreeNode2_Kpi)
                dicKPI.Add(IOSInternalTechnology.TechNode3, dsTreeNode3_Kpi)
            ElseIf (baseTech = BaseTechnology.TechCOMMON) Then
                dicKPI.Add(IOSInternalTechnology.COMMON, dsTreeCommon_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMSC) Then
                'dicKPI.Add(IOSInternalTechnology., dsTreeGGSN_Kpi)
            ElseIf (baseTech = BaseTechnology.TechGGSN) Then
                dicKPI.Add(IOSInternalTechnology.GGSN, dsTreeGGSN_Kpi)
            ElseIf (baseTech = BaseTechnology.TechIMS) Then
                dicKPI.Add(IOSInternalTechnology.IMS, dsTreeIMS_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMGW) Then
                dicKPI.Add(IOSInternalTechnology.MGW, dsTreeMGW_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMSS) Then
                dicKPI.Add(IOSInternalTechnology.MSS, dsTreeMSS_Kpi)
            ElseIf (baseTech = BaseTechnology.TechSGSN) Then
                dicKPI.Add(IOSInternalTechnology.SGSN, dsTreeSGSN_Kpi)
            ElseIf (baseTech = BaseTechnology.TechPGW) Then
                dicKPI.Add(IOSInternalTechnology.PGW, dsTreePGW_Kpi)
            ElseIf (baseTech = BaseTechnology.TechSGW) Then
                dicKPI.Add(IOSInternalTechnology.SGW, dsTreeSGW_Kpi)
            ElseIf (baseTech = BaseTechnology.TechTX) Then
                dicKPI.Add(IOSInternalTechnology.TX, dsTreeTM_Kpi)
            ElseIf (baseTech = BaseTechnology.TechTX2) Then
                dicKPI.Add(IOSInternalTechnology.TX2, dsTreeTM2_Kpi)
            ElseIf (baseTech = BaseTechnology.TechSAPC) Then
                dicKPI.Add(IOSInternalTechnology.SAPC, dsTreeSAPC_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMEE) Then
                dicKPI.Add(IOSInternalTechnology.MEE, dsTreeMEE_Kpi)
            ElseIf (baseTech = BaseTechnology.TechCDRMSC) Then
                dicKPI.Add(IOSInternalTechnology.CDRMSC, dsTreeMSC_Kpi_CDR)
            ElseIf (baseTech = BaseTechnology.TechCDRSGSN) Then
                dicKPI.Add(IOSInternalTechnology.CDRSGSN, dsTreeSGSN_Kpi_CDR)
            ElseIf (baseTech = BaseTechnology.TechCDRGGSN) Then
                dicKPI.Add(IOSInternalTechnology.CDRGGSN, dsTreeGGSN_Kpi_CDR)
            ElseIf (baseTech = BaseTechnology.TechEPC1) Then
                dicKPI.Add(IOSInternalTechnology.EPC1, dsTreeMME_Kpi)
            ElseIf (baseTech = BaseTechnology.TechEPC2) Then
                dicKPI.Add(IOSInternalTechnology.EPC2, dsTreeSGW_Kpi)
            ElseIf (baseTech = BaseTechnology.TechEPC3) Then
                dicKPI.Add(IOSInternalTechnology.EPC3, dsTreeMME_Kpi)
            ElseIf (baseTech = BaseTechnology.TechTransport) Then
                dicKPI.Add(IOSInternalTechnology.TRANSPORT, dsTreeTransport_kpi)
            ElseIf (baseTech = BaseTechnology.TechPDUM) Then
                dicKPI.Add(IOSInternalTechnology.PDUM, dsTreePDUM_kpi)
            ElseIf (baseTech = BaseTechnology.TechTwamp) Then
                dicKPI.Add(IOSInternalTechnology.TWAMP, dsTreeTwamp_kpi)
            ElseIf (baseTech = BaseTechnology.TechHLR) Then
                dicKPI.Add(IOSInternalTechnology.HLR, dsTreeHLR_kpi)
            ElseIf (baseTech = BaseTechnology.TechDWDM) Then
                dicKPI.Add(IOSInternalTechnology.DWDM, dsTreeDwdm_kpi)
            ElseIf (baseTech = BaseTechnology.TechHSS) Then
                dicKPI.Add(IOSInternalTechnology.HSS, dsTreeHSS_kpi)
            ElseIf (baseTech = BaseTechnology.TechUDR) Then
                dicKPI.Add(IOSInternalTechnology.UDR, dsTreeUDR_kpi)
            End If
        Else
            If (baseTech = BaseTechnology.Tech2G) Then
                dicKPI.Add(IOSInternalTechnology.Tech2G1, dsTreeTopX2G_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech2G2, dsTreeTopXNanoBTS_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech2G3, dsTreeTopX2G3_Kpi)
            ElseIf (baseTech = BaseTechnology.Tech3G) Then
                dicKPI.Add(IOSInternalTechnology.Tech3G1, dsTreeTopX3G_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech3G2, dsTreeTopXNano3G_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech3G3, dsTreeTopX3G3_Kpi)
            ElseIf (baseTech = BaseTechnology.Tech4G) Then
                dicKPI.Add(IOSInternalTechnology.Tech4G1, dsTreeTopX4G1_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech4G2, dsTreeTopX4G2_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech4G3, dsTreeTopX4G3_Kpi)
            ElseIf (baseTech = BaseTechnology.Tech5G) Then
                dicKPI.Add(IOSInternalTechnology.Tech5G1, dsTreeTopX5G1_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech5G2, dsTreeTopX5G2_Kpi)
                dicKPI.Add(IOSInternalTechnology.Tech5G3, dsTreeTopX5G3_Kpi)
            ElseIf (baseTech = BaseTechnology.TechNode) Then
                dicKPI.Add(IOSInternalTechnology.TechNode1, dsTreeTopXNode1_Kpi)
                dicKPI.Add(IOSInternalTechnology.TechNode2, dsTreeTopXNode2_Kpi)
                dicKPI.Add(IOSInternalTechnology.TechNode3, dsTreeTopXNode3_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMSC) Then
                'do nothing
            ElseIf (baseTech = BaseTechnology.TechCOMMON) Then
                dicKPI.Add(IOSInternalTechnology.COMMON, dsTreeTopXCommon_Kpi)
            ElseIf (baseTech = BaseTechnology.TechGGSN) Then
                dicKPI.Add(IOSInternalTechnology.GGSN, dsTreeGGSN_Kpi)
            ElseIf (baseTech = BaseTechnology.TechIMS) Then
                dicKPI.Add(IOSInternalTechnology.IMS, dsTreeIMS_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMGW) Then
                dicKPI.Add(IOSInternalTechnology.MGW, dsTreeMGW_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMSS) Then
                dicKPI.Add(IOSInternalTechnology.MSS, dsTreeMSS_Kpi)
            ElseIf (baseTech = BaseTechnology.TechSGSN) Then
                dicKPI.Add(IOSInternalTechnology.SGSN, dsTreeSGSN_Kpi)
            ElseIf (baseTech = BaseTechnology.TechPGW) Then
                dicKPI.Add(IOSInternalTechnology.PGW, dsTreePGW_Kpi)
            ElseIf (baseTech = BaseTechnology.TechSGW) Then
                dicKPI.Add(IOSInternalTechnology.SGW, dsTreeSGW_Kpi)
            ElseIf (baseTech = BaseTechnology.TechTX) Then
                dicKPI.Add(IOSInternalTechnology.TX, dsTreeTopXTM_Kpi)
            ElseIf (baseTech = BaseTechnology.TechTX2) Then
                dicKPI.Add(IOSInternalTechnology.TX2, dsTreeTopXTM2_Kpi)
            ElseIf (baseTech = BaseTechnology.TechSAPC) Then
                dicKPI.Add(IOSInternalTechnology.SAPC, dsTreeSAPC_Kpi)
            ElseIf (baseTech = BaseTechnology.TechMEE) Then
                dicKPI.Add(IOSInternalTechnology.MEE, dsTreeMEE_Kpi)
            ElseIf (baseTech = BaseTechnology.TechCDRMSC) Then
                dicKPI.Add(IOSInternalTechnology.CDRMSC, dsTreeTopXMSC_Kpi_CDR)
            ElseIf (baseTech = BaseTechnology.TechCDRSGSN) Then
                dicKPI.Add(IOSInternalTechnology.CDRSGSN, dsTreeTopXSGSN_Kpi_CDR)
            ElseIf (baseTech = BaseTechnology.TechCDRGGSN) Then
                dicKPI.Add(IOSInternalTechnology.CDRGGSN, dsTreeTopXGGSN_Kpi_CDR)
            ElseIf (baseTech = BaseTechnology.TechEPC1) Then
                dicKPI.Add(IOSInternalTechnology.EPC1, dsTreeTopXMME_Kpi)
            ElseIf (baseTech = BaseTechnology.TechEPC2) Then
                dicKPI.Add(IOSInternalTechnology.EPC2, dsTreeTopXMME_Kpi)
            ElseIf (baseTech = BaseTechnology.TechEPC3) Then
                dicKPI.Add(IOSInternalTechnology.EPC3, dsTreeTopXMME_Kpi)
            ElseIf (baseTech = BaseTechnology.TechTransport) Then
                dicKPI.Add(IOSInternalTechnology.TRANSPORT, dsTreeTopXTransport_kpi)
            ElseIf (baseTech = BaseTechnology.TechPDUM) Then
                dicKPI.Add(IOSInternalTechnology.PDUM, dsTreeTopXPDUM_kpi)
            ElseIf (baseTech = BaseTechnology.TechTwamp) Then
                dicKPI.Add(IOSInternalTechnology.TWAMP, dsTreeTopXTwamp_kpi)
            ElseIf (baseTech = BaseTechnology.TechHLR) Then
                dicKPI.Add(IOSInternalTechnology.HLR, dsTreeTopXHLR_kpi)
            ElseIf (baseTech = BaseTechnology.TechDWDM) Then
                dicKPI.Add(IOSInternalTechnology.DWDM, dsTreeTopXDwdm_kpi)
            ElseIf (baseTech = BaseTechnology.Techhss) Then
                dicKPI.Add(IOSInternalTechnology.HSS, dsTreeTopXHSS_kpi)
            ElseIf (baseTech = BaseTechnology.TechUDR) Then
                dicKPI.Add(IOSInternalTechnology.UDR, dsTreeTopXUDR_kpi)
            End If
        End If

        Return dicKPI
    End Function

    Private Function GetVendorImageIndex(ByVal vendor As String) As Integer
        Dim imgindex As Integer = Nothing
        Try
            imgindex = imgListVendors.Images.IndexOfKey(vendor)
            Return imgindex
        Catch ex As Exception
            Return imgindex
        End Try
    End Function

    Private Function GetVendorImage(ByVal vendorName As String) As System.Drawing.Image
        Try
            If (vendorName IsNot Nothing) Then
                If (vendorName.ToLower = IOSVendors.HUAWEI) Then
                    Return EmbeddedImage(IOSVendorImages.HUAWEI)
                ElseIf (vendorName.ToLower = IOSVendors.NORTEL) Then
                    Return EmbeddedImage(IOSVendorImages.NORTEL)
                ElseIf (vendorName.ToLower = IOSVendors.ERICSSON) Then
                    Return EmbeddedImage(IOSVendorImages.ERICSSON)
                ElseIf (vendorName.ToLower = IOSVendors.NOKIA) Then
                    Return EmbeddedImage(IOSVendorImages.NOKIA)
                ElseIf (vendorName.ToLower = IOSVendors.CNE) Then
                    Return EmbeddedImage(IOSVendorImages.CNE)
                ElseIf (vendorName.ToLower = IOSVendors.IPACCESS) Then
                    Return EmbeddedImage(IOSVendorImages.IPACCESS)
                ElseIf (vendorName.ToLower = IOSVendors.SMALL) Then
                    Return EmbeddedImage(IOSVendorImages.SMALL)
                ElseIf (vendorName.ToLower = IOSVendors.ZTE) Then
                    Return EmbeddedImage(IOSVendorImages.ZTE)
                ElseIf (vendorName.ToLower = IOSVendors.COMMON) Then
                    Return EmbeddedImage(IOSVendorImages.COMMON)
                Else
                    Return EmbeddedImage(IOSVendorImages.NONE)
                End If
            Else
                Return EmbeddedImage(IOSVendorImages.NONE)
            End If
        Catch ex As Exception
            Return EmbeddedImage(IOSVendorImages.NONE)
        End Try
    End Function

    Private Function GetIndexByKey(ByVal keyName As String, ByRef imglist As ImageList) As Integer
        Dim imgindex As Integer = Nothing
        Try
            imgindex = imglist.Images.IndexOfKey(keyName)
            Return imgindex
        Catch ex As Exception
            Return imgindex
        End Try
    End Function

    Private Function GetBaseTechnology(ByVal techName As String) As String
        Try
            If (techName.ToUpper().Contains(BaseTechnology.Tech2G) Or techName.ToUpper().Contains("BTS")) Then
                Return BaseTechnology.Tech2G
            ElseIf (techName.ToUpper().Contains(BaseTechnology.Tech3G)) Then
                Return BaseTechnology.Tech3G
            ElseIf (techName.ToUpper().Contains(BaseTechnology.Tech4G)) Then
                Return BaseTechnology.Tech4G
            ElseIf (techName.ToUpper().Contains(BaseTechnology.Tech5G)) Then
                Return BaseTechnology.Tech5G
            ElseIf (techName.ToUpper().Contains(BaseTechnology.TechNode)) Then
                Return BaseTechnology.TechNode
            ElseIf (techName.ToUpper().Contains(BaseTechnology.TechCOMMON)) Then
                Return BaseTechnology.TechCOMMON

                'ElseIf (techName.ToUpper().Contains(BaseTechnology.TechTX)) Then
                '    Return BaseTechnology.TechTX

            ElseIf (techName.ToUpper().Trim = "ERICSSON TX") Then
                Return BaseTechnology.TechTX
            ElseIf (techName.ToUpper().Trim = "HUAWEI TX") Then
                Return BaseTechnology.TechTX2

            ElseIf (techName.ToUpper().Trim = "ERICSSON TRANSPORT") Then
                Return BaseTechnology.TechTransport
            ElseIf (techName.ToUpper().Trim = "HUAWEI TRANSPORT") Then
                'Return BaseTechnology.TechTransport2
            ElseIf (techName.ToUpper().Trim = "PDUM") Then
                Return BaseTechnology.TechPDUM
            ElseIf (techName.ToUpper.Trim = "TWAMP") Then
                Return BaseTechnology.TechTwamp
            ElseIf (techName.ToUpper.Trim = "HLR") Then
                Return BaseTechnology.TechHLR
            ElseIf (techName.ToUpper.Trim = "DWDM") Then
                Return BaseTechnology.TechDWDM
            ElseIf (techName.ToUpper.Trim = "HSS") Then
                Return BaseTechnology.TechHSS
            ElseIf (techName.ToUpper.Trim = "UDR") Then
                Return BaseTechnology.TechUDR
            End If
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function

    Private Function GetTechImage(ByVal techName As String) As System.Drawing.Image
        Try
            If (techName IsNot Nothing) Then
                If (techName.ToUpper().Contains(BaseTechnology.Tech2G)) Then
                    Return EmbeddedImage("2G.png")
                ElseIf (techName.ToUpper().Contains(BaseTechnology.Tech3G)) Then
                    Return EmbeddedImage("3G.png")
                ElseIf (techName.ToUpper().Contains(BaseTechnology.Tech4G)) Then
                    Return EmbeddedImage("4G.png")
                ElseIf (techName.ToUpper().Contains(BaseTechnology.Tech5G)) Then
                    Return EmbeddedImage("5G.png")
                ElseIf (techName.ToUpper().Contains(BaseTechnology.TechNode)) Then
                    Return EmbeddedImage("Node.png")
                Else
                    Return EmbeddedImage("G.png")
                End If
            Else
                Return EmbeddedImage("G.png")
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub LoadGridColumnsConfig()
        If File.Exists(String.Format("{0}\{1}.xml", GetUserDataPath(), "GridColumnsConfig")) Then
            dsGridColumnsConfig.ReadXml(String.Format("{0}\{1}.xml", GetUserDataPath(), "GridColumnsConfig"), XmlReadMode.ReadSchema)
        End If
    End Sub

    Private Sub SaveGridColumnsConfig()
        Try
            If dsGridColumnsConfig IsNot Nothing Then
                dsGridColumnsConfig.WriteXml(String.Format("{0}\{1}.xml", GetUserDataPath(), "GridColumnsConfig"), XmlWriteMode.WriteSchema)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CreateTicketsWebAPIConfigXML()
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dr As DataRow = Nothing

            dt.Columns.Add("AuthTokenUrl", GetType(String))
            dt.Columns.Add("ClientID", GetType(String))
            dt.Columns.Add("ClientSecret", GetType(String))
            dt.Columns.Add("UserName", GetType(String))
            dt.Columns.Add("Password", GetType(String))
            dt.Columns.Add("APIBaseUrl", GetType(String))
            dt.Columns.Add("MethodType", GetType(String))
            dt.Columns.Add("MethodParams", GetType(String))
            dt.Columns.Add("ColumnName", GetType(String))
            dt.Columns.Add("ReplacedColumn", GetType(String))
            dt.Columns.Add("ColumnOrdinal", GetType(String))

            dr = dt.NewRow()
            dr("AuthTokenUrl") = "https://tmobilenl.service-now.com/oauth_token.do"
            dr("ClientID") = "1f935b8a48f645503bfb5f79c0b2a8f6"
            dr("ClientSecret") = "&zP:bro_l+?!sB"
            dr("UserName") = "cellsens.integration"
            dr("Password") = "m@j1pHoml*r=zita@IbIstAbro_l+?!3"
            dr("APIBaseUrl") = "https://tmobilenl.service-now.com/api/tmnrb/cellsensintegration/"
            dr("MethodType") = "createdAfter"
            dr("MethodParams") = "cis|start_date"
            dr("ColumnName") = "short_description|assignment_group|ci|created|impact|description|priority|url|number|closed|state|updated|assigned_to"
            dr("ReplacedColumn") = "short_description|assignment_group|ci|CREATED|impact|description|PRIO|url|number|closed|state|updated|assigned_to"
            dr("ColumnOrdinal") = "3|7|1|0|5|4|2|12|8|9|10|11|6"

            dt.Rows.Add(dr)
            ds.Tables.Add(dt)
            ds.WriteXml(String.Format("{0}\{1}.xml", GetUserDataPath(), "TicketsWebAPIConfig"), XmlWriteMode.WriteSchema)
        Catch
        End Try
    End Sub

    Private Sub CreateMapTicketsWebAPIConfigXML()
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dr As DataRow = Nothing

            dt.Columns.Add("AuthTokenUrl", GetType(String))
            dt.Columns.Add("ClientID", GetType(String))
            dt.Columns.Add("ClientSecret", GetType(String))
            dt.Columns.Add("UserName", GetType(String))
            dt.Columns.Add("Password", GetType(String))
            dt.Columns.Add("APIBaseUrl", GetType(String))
            dt.Columns.Add("MethodType", GetType(String))
            dt.Columns.Add("MethodParams", GetType(String))
            dt.Columns.Add("ColumnName", GetType(String))
            dt.Columns.Add("ReplacedColumn", GetType(String))
            dt.Columns.Add("ColumnOrdinal", GetType(String))

            dr = dt.NewRow()
            dr("AuthTokenUrl") = "https://tmobilenl.service-now.com/oauth_token.do"
            dr("ClientID") = "1f935b8a48f645503bfb5f79c0b2a8f6"
            dr("ClientSecret") = "&zP:bro_l+?!sB"
            dr("UserName") = "cellsens.integration"
            dr("Password") = "m@j1pHoml*r=zita@IbIstAbro_l+?!3"
            dr("APIBaseUrl") = "https://tmobilenl.service-now.com/api/tmnrb/cellsensintegration/"
            dr("MethodType") = "createdBetween"
            dr("MethodParams") = "cis|start_date|end_date|priority"
            dr("ColumnName") = "short_description|assignment_group|ci|created|impact|description|priority|url|number|closed|state|updated|assigned_to"
            dr("ReplacedColumn") = "short_description|assignment_group|SITECODE|CREATED|impact|description|PRIO_IOS|url|number|closed|state|updated|assigned_to"
            dr("ColumnOrdinal") = "3|7|1|0|5|4|2|12|8|9|10|11|6"

            dt.Rows.Add(dr)
            ds.Tables.Add(dt)
            ds.WriteXml(String.Format("{0}\{1}.xml", GetUserDataPath(), "MapTicketsWebAPIConfig"), XmlWriteMode.WriteSchema)
        Catch
        End Try
    End Sub

#End Region

#Region "Alert - Add KPI"

    Public Sub ExecuteAfteAddKPiThreadComplete(lc As DevExpress.XtraEditors.LabelControl, Status As Integer, ti As Threading.Thread)
        SyncLock objAddKPIThreadLock
            Dim arg() As Object = {lc, Status}
            Me.BeginInvoke(New CallThreadInvokedAddKPI(AddressOf SetAddKPIStatus), arg)
        End SyncLock
    End Sub

    Public Sub SetAddKPIStatus(ByRef lc As DevExpress.XtraEditors.LabelControl, Status As Integer)
        Try
            SyncLock objAddKPIThreadLock
                If lc IsNot Nothing Then
                    If Status = 0 Or Status = -1 Then
                        lc.Text = "Adding KPI Failed."
                    ElseIf Status = 1 Then
                        lc.Text = "KPI Added Successfully."
                    End If
                    Application.DoEvents()
                End If
            End SyncLock
        Catch
        Finally
            'If Status = 1 Then
            '	lc.Text = "KPI added successfully."
            'End If
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub bbtnSiteIntegration_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbtnSiteIntegration.ItemClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            OpenFormAsDockPanel("Site Integration")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class

Public Class AddKPIClass

    Public kpiRuleID As Integer = Nothing
    Public kpiRuleDataAvailable As Boolean = Nothing
    Public AlertRuleID As Integer = Nothing
    Public AddKpiStatus As Integer = Nothing
    Public kpiSqlID As Integer = Nothing
    Public kpiRuleType As Integer = Nothing
    Public technology As String = Nothing
    Public objectType As String = Nothing
    Public objectReported As String = Nothing
    Public lc As DevExpress.XtraEditors.LabelControl
    Public Interval As String = Nothing
    Private dtKpiRuleTypeFields As DataTable = Nothing
    Public Event ThreadComplete(lc As DevExpress.XtraEditors.LabelControl, Status As Integer, ti As Thread)

    Sub AddKPI()
        Try
            'AddKpiStatus = 1
            AnomalyAddKPI(AlertRuleID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            RaiseEvent ThreadComplete(lc, AddKpiStatus, Thread.CurrentThread)
        End Try
    End Sub

    Sub AnomalyAddKPI(ALertRuleID As Integer)
        If dtKpiRuleTypeFields Is Nothing Then
            GetKpiRuleTypeFields(kpiRuleType)
        End If

        Dim propNames As New List(Of String())

        propNames.Add(New String() {"@AlertRuleID", ALertRuleID})
        propNames.Add(New String() {"@KPISQLID", kpiSqlID})
        propNames.Add(New String() {"@KPIRuleType", kpiRuleType})
        propNames.Add(New String() {"@Technology", Chr(39) & technology & Chr(39)})
        propNames.Add(New String() {"@ObjectType", Chr(39) & objectType & Chr(39)})
        propNames.Add(New String() {"@ObjectReported", Chr(39) & objectReported & Chr(39)})
        propNames.Add(New String() {"@InputDataPeriodInterval", Chr(39) & Interval.ToUpper & Chr(39)})

        For Each dr As DataRow In dtKpiRuleTypeFields.Rows
            propNames.Add(New String() {"@" & dr("KPI_RuleProperties").ToString, IIf(IsDBNull(dr("DefaultValue")), "NULL", Chr(39) & dr("DefaultValue").ToString & Chr(39))})
        Next

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        strConnection = GetSQL(3822, propNames.ToArray)(0)
        sqlParam = GetSQL(3822, propNames.ToArray)(1)

        sqlParam = sqlParam.Replace("@InputDataPeriodInterval", Interval.ToUpper)
        sqlParam = sqlParam.Replace("@InputDataSlidingWindow", "NULL")
        sqlParam = sqlParam.Replace("@InputdataMatchDays", "NULL")
        sqlParam = sqlParam.Replace("@InputdataMatchHours", "NULL")
        sqlParam = sqlParam.Replace("@FixedLowerThreshold", "NULL")
        sqlParam = sqlParam.Replace("@FixedUpperTreshold", "NULL")
        sqlParam = sqlParam.Replace("@SigmaFilterOutliers", "NULL")
        sqlParam = sqlParam.Replace("@PercLowerTreshold", "NULL")
        sqlParam = sqlParam.Replace("@PercUpperTreshold", "NULL")
        sqlParam = sqlParam.Replace("@ZScoreLowerTreshold", "NULL")
        sqlParam = sqlParam.Replace("@ZScoreUpperTreshold", "NULL")
        sqlParam = sqlParam.Replace("@OccurencesThreshold", "NULL")
        sqlParam = sqlParam.Replace("@OccurencesSlidingWindow", "NULL")

        kpiRuleID = DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut).Rows(0)(0)
        If kpiRuleID > 0 Then
            lc.Text = "KPI Added, Background Calculation Started."
            'Execute KPI Calculation Procedure...
            ExecuteKPICalculationProcess(kpiRuleID)
        End If
    End Sub

    Private Sub ExecuteKPICalculationProcess(kpiRuleID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@kpiRuleID", kpiRuleID}
        }
        strConnection = GetSQL(3853, parray)(0)
        sqlParam = GetSQL(3853, parray)(1)
        kpiRuleDataAvailable = CBool(DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, 600).Rows(0)("DataAvailable"))
        If kpiRuleDataAvailable = True Then
            AddKpiStatus = 1
        ElseIf kpiRuleDataAvailable = False Then
            AddKpiStatus = 0
        End If
    End Sub

    Private Sub GetKpiRuleTypeFields(ByVal kpiRuleType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@kpiRuleType", kpiRuleType}
        }
        strConnection = GetSQL(3818, parray)(0)
        sqlParam = GetSQL(3818, parray)(1)
        dtKpiRuleTypeFields = New DataTable()
        dtKpiRuleTypeFields = DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

End Class