Imports System.IO
Imports IOS.Configuration
Imports System.Threading
Imports System.Globalization

Public NotInheritable Class SplashScreen

#Region "Variable Declaration"

    Public licenselabel As String
    Dim pbvalue As Integer
    Public loadmodules As Integer
    Delegate Sub UpdateStatusLabel()
	Dim IosLicenseServerOdbc As String = ""
	Dim IosServerOdbc As String = ""
	Dim licenseUser As String = ""
	Dim licenseExpiry As DateTime

#End Region

#Region "Form Event"

	Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        licenselabel = ""
        Splash_ProgressBar.Value = 0
        Splash_ProgressBar.Maximum = 100
        pbvalue = 0
        loadmodules = 6

        IosLicenseServerOdbc = connStrIOSLicenseServer.Split(";")(0).Split("=")(1)
        IosServerOdbc = connStrIOSServer.Split(";")(0).Split("=")(1)

        Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
        Copyright.Text = "Copyright © 2025 CellSens B.V."

        Dim CompanyName As String = System.Configuration.ConfigurationManager.AppSettings("Company").ToString
        If CompanyName.Contains("Omnitele") Then
            Me.MainLayoutPanel.BackgroundImage = My.Resources.ìos_splashscreen_omni
        Else
            Me.MainLayoutPanel.BackgroundImage = My.Resources.ÌOS_SplashScreen
        End If
    End Sub

#End Region

#Region "Splash Screen Update"

    Public Sub Update_LabelVersion_Failed()
        Label6.ForeColor = Color.Red
		lblVersionCheck.Text = "Version - FAILED (Version: " & Application.ProductVersion & ")"
	End Sub

    Public Sub Update_LabelVersion_Disabled()
        Label6.ForeColor = Color.LightGray
		lblVersionCheck.Text = "Version - DISABLED (Version: " & Application.ProductVersion & ")"
	End Sub

    Public Sub Update_LabelVersion_NoUpdate()
        Label6.ForeColor = Color.LimeGreen
		lblVersionCheck.Text = "Version - OK (Version: " & Application.ProductVersion & ")"
	End Sub

    Public Sub Update_LabelVersion_Update()
        Label6.ForeColor = Color.Red
        lblVersionCheck.Text = "Version - New Available!"
        Threading.Thread.Sleep(2000)
        Me.Close()
    End Sub

	Public Sub Update_LabelLicense_Failed(user As String, licenseExpDate As String)
		Label7.ForeColor = Color.Red
		lblLicenseCheck.Text = "License Not Available/Expired !"
	End Sub

	Public Sub Update_UserLicense_Success(user As String, licenseExpDate As String)
		Label7.ForeColor = Color.LimeGreen
		lblLicenseCheck.Text = "License Check - OK (Found ! " & user & ":" & licenseExpDate & ")"
	End Sub

	Public Sub Update_UserLicense_NotReached()
        Label7.ForeColor = Color.Red
        lblLicenseCheck.Text = "License Check - Not Reached"
	End Sub

	Public Sub Update_UserLicense_NotValid(user As String, licenseExpDate As String)
        Label7.ForeColor = Color.Red
        lblLicenseCheck.Text = "License Check - (Not Found ! " & user & ":" & licenseExpDate & ")"
	End Sub

	Public Sub Update_LabelLicense_NotReached()
        Label7.ForeColor = Color.Red
        lblLicenseCheck.Text = "License Check. User Not Found !"
	End Sub

	Public Sub Update_LabelLicServer_Found()
		lblLicServer.ForeColor = Color.LimeGreen
        lblLicenseServer.Text = "License Server (Connected ! " & IosLicenseServerOdbc & ")"
    End Sub

	Public Sub Update_LabelLicServer_NotReached()
		lblLicServer.ForeColor = Color.Red
        lblLicenseServer.Text = "License Server (Not Reached ! " & IosLicenseServerOdbc & ")"
        dlgStartup.OpenExitDialog(Me.Left + (Me.Width / 2) - (dlgStartup.Width / 2), Me.Top + Me.Height)
	End Sub

	Public Sub Update_LabelLicServer_NotValid()
		lblLicServer.ForeColor = Color.Red
        lblLicenseCheck.Text = "Not Reached ! " & IosLicenseServerOdbc
    End Sub

	Public Sub Update_LicenseText()
        lbl_License.Text = licenselabel
    End Sub

	Public Sub Update_ProgressBar()
		Try
			pbvalue = pbvalue + 100 / loadmodules
			Splash_ProgressBar.Value = pbvalue
		Catch
		End Try
	End Sub

	Public Sub Update_LabelServer_NotFound()
		Label8.ForeColor = Color.Red
		lblIOSConnection.Text = "IOS Server Connection (Failed ! " & IosServerOdbc & ")"
		dlgStartup.OpenExitDialog(Me.Left + (Me.Width / 2) - (dlgStartup.Width / 2), Me.Top + Me.Height)
	End Sub

	Public Sub Update_LabelServer_Found()
		Label8.ForeColor = Color.LimeGreen
		lblIOSConnection.Text = "IOS Server Connection (Connected ! " & IosServerOdbc & ")"
	End Sub

	Public Sub Update_LabelConfig_Found()
        Label9.ForeColor = Color.LimeGreen
    End Sub

    Public Sub Update_LabelConfig_NotFound()
        Label9.ForeColor = Color.Red
    End Sub

    Public Sub Update_LabelDataSource_Found()
        Label10.ForeColor = Color.LimeGreen
    End Sub

    Public Sub Update_LabelDataSource_NotFound()
        Label10.ForeColor = Color.Red
    End Sub

    Public Sub Update_LabelProgressFinished()
        Label11.ForeColor = Color.LimeGreen
    End Sub

    Public Sub Update_UserLicense_IsLocked()
        Label7.ForeColor = Color.Red
        lblLicenseCheck.Text = "License Check - User Is Locked"
    End Sub

    Public Sub Update_UserLicense_IsDisabled()
        Label7.ForeColor = Color.Red
        lblLicenseCheck.Text = "License Check - User Is Disabled"
    End Sub

#End Region

#Region "Splash Screen Helper"

    Public Function AutoUpdate(ByRef CommandLine As String) As String
        'Return "NoUpdate"
        Dim Key As String = "&**#@!" ' any unique sequence of characters

        ' the file with the update information
        Dim sfile As String = "update.dat"
        ' the Assembly name 

        Dim AssemblyName As String = "IOS"

        'Here you need to change the web address
        Dim RemotePath As String = ""
        If IOSAppConfigManage.AutoUpdateServer.ToLower.Contains("https") Then
            RemotePath = IOSAppConfigManage.AutoUpdateServer
            System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(senderX, certificate, chain, sslPolicyErrors) True
        Else
            RemotePath = "http://" & IOSAppConfigManage.AutoUpdateServer
        End If

        Try
            ''ProxyServer = ConfigurationManager.AppSettings("Proxy Server").ToString
        Catch
        End Try

        WriteString_Log(Now() & "    " & "Auto Update - Server:" & RemotePath)
        ' where are the files for a specific system

        Dim RemoteUri As String = RemotePath
        ' clean up the command line getting rid of the key

        CommandLine = Replace(Microsoft.VisualBasic.Command(), Key, "")
        ' Verify if was called by the auto update
        WriteString_Log(Now() & "    " & "Auto Update - Commandline:" & CommandLine)

        If InStr(Microsoft.VisualBasic.Command(), Key) > 0 Then
            Try
                ' try to delete the AutoUpdate program, 

                ' since it is not needed anymore
                WriteString_Log(Now() & "    " & "Auto Update - Delete File:" & GetUserDataPath() & "\IOS_AutoUpdate.exe")
                System.IO.File.Delete(GetUserDataPath() & "\IOS_AutoUpdate.exe")
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                WriteString_Log(Now() & "    " & "Auto Update - Delete File Error:" & GetUserDataPath() & "\IOS_AutoUpdate.exe" & vbCrLf & ex.Message)
            End Try
            ' return false means that no update is needed
            WriteString_Log(Now() & "    " & "Auto Update - Return NoUpdate")
            Return "NoUpdate"
        Else
            ' was called by the user
            Dim ret As String = "NoUpdate" ' Default - no update needed
            Try
                Dim pol As System.Net.Cache.RequestCachePolicy = New System.Net.Cache.RequestCachePolicy(Net.Cache.RequestCacheLevel.Reload)
                Dim myWebClient As New System.Net.WebClient 'the webclient
                If IOSAppConfigManage.UseProxyForAutoUpdate = True Then
                    If objProxy IsNot Nothing Then
                        myWebClient.Proxy = objProxy
                        WriteString_Log(Now() & "")
                    End If
                End If
                myWebClient.CachePolicy = pol
                ' Download the update info file to the memory, 

                ' read and close the stream
                WriteString_Log(Now() & "    " & "Auto Update - Download InfoFile:" & RemoteUri & sfile)

                Dim file As New System.IO.StreamReader(myWebClient.OpenRead(RemoteUri & sfile))
                Dim Contents As String = file.ReadToEnd()
                file.Close()
                ' if something was read
                WriteString_Log(Now() & "    " & "Auto Update - InfoFile Contents:" & Contents)

                If Contents <> "" Then
                    ' Break the contents 
                    Dim x() As String = Split(Contents, "|")
                    ' the first parameter is the version. if it's 

                    ' greater then the current version starts the 

                    ' update process
                    If x(0) > Application.ProductVersion Then
                        ' assembly the parameter to be passed to the auto 

                        ' update program

                        ' x(1) is the files that need to be 

                        ' updated separated by "?"
                        WriteString_Log(Now() & "    " & "Auto Update - Version:" & x(0) & ">" & Application.ProductVersion & " -> UPDATE")

                        Dim arg As String = Application.ExecutablePath & "|" &
                                    RemoteUri & "|" & x(1) & "|" & Key & "|" &
                                    Microsoft.VisualBasic.Command()
                        ' Download the auto update program to the application 
                        WriteString_Log(Now() & "    " & "Auto Update - Start Download AutoUpdate Agent:" & RemoteUri & "IOS_AutoUpdate.exe")

                        ' path, so you always have the last version runing


                        myWebClient.DownloadFile(RemoteUri & "IOS_AutoUpdate.exe",
                            GetUserDataPath() & "\IOS_AutoUpdate.exe")
                        ' Call the auto update program with all the parameters
                        WriteString_Log(Now() & "    " & "Auto Update - Start AutoUpdate Agent:" & GetUserDataPath() & "\IOS_AutoUpdate.exe" & " " & arg)

                        System.Diagnostics.Process.Start(
                            GetUserDataPath() & "\IOS_AutoUpdate.exe", arg)
                        ' return true - auto update in progress

                        ret = "Update"
                        WriteString_Log(Now() & "    " & "Auto Update - Return Update")

                    End If
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                ' if there is an error return true, 
                ' what means that the application

                ' should be closed
                WriteString_Log(Now() & "    " & "Auto Update - Return Failed " & vbCrLf & ex.Message)

                ret = "Failed"
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                ' something went wrong... 
            End Try
            Return ret
        End If
    End Function

    Private Function SplashScreen_Progress() As Boolean
        'Testing IOS_Server Connection
        WriteString_Log(Now() & "    " & "Start Configuration Process")
        If TestConnection(connStrIOSServer) = False Then
            WriteString_Log(Now() & "    " & "Configuration: Connection to IOS Server Failed")
            MsgBox("IOS Server not found: " & Chr(13) & "Check ODBC connection ! ", MsgBoxStyle.ApplicationModal)
            Update_LabelServer_NotFound()
            Threading.Thread.Sleep(3000)
            Return False
        Else
            WriteString_Log(Now() & "    " & "Configuration: Connection to IOS Server Success")
            Update_LabelServer_Found()
        End If

        If Check_IOS_Table() = False Then
            WriteString_Log(Now() & "    " & "Configuration: Connection to IOS Table Set Corrupt")
            MsgBox("IOS Query-Set not found: " & Chr(13) & "Contact Administrator! ", MsgBoxStyle.ApplicationModal)
            Update_LabelConfig_NotFound()
            Return False
        Else
            WriteString_Log(Now() & "    " & "Configuration: Connection to IOS Table Set Valid")
            Update_LabelConfig_Found()
        End If

        'Checking Directories
        WriteString_Log(Now() & "    " & "Configuration: Checking User Directories")
        Try
            If Not System.IO.Directory.Exists(GetUserDataPath() & "\Data\") Then
                WriteString_Log(Now() & "    " & "Configuration: Creating " & GetUserDataPath() & "\Data\")
                System.IO.Directory.CreateDirectory(GetUserDataPath() & "\Data\")
            End If

            'Now part of setup
            If Not System.IO.Directory.Exists(GetUserDataPath() & "\Cache\") Then
                WriteString_Log(Now() & "    " & "Configuration: Creating " & GetUserDataPath() & "\Cache\")
                System.IO.Directory.CreateDirectory(GetUserDataPath() & "\Cache\")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            WriteString_Log(Now() & "    " & "Configuration: Error Checking/Creating Paths " & GetUserDataPath() & vbCrLf & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

        Update_LabelDataSource_Found()
        Update_ProgressBar()
        Application.DoEvents()
        Try
            'Map Calendar
            frmMDI.InilializeMapGetNetworkCalendar()

            'Loading Data Object Tree data ORDER IS IMPORTANT! High hierarchy to low
            'object tree, download configuration
            dt_IOS_ObjectConfig = IOS.DataLibrary.clsSQLCommands.Get_ObjectConfig_New_Data(connStrIOSServer)
            IOS_ObjectConfig_Tech(dt_IOS_ObjectConfig)
            Update_ProgressBar()
            Application.DoEvents()

            'assign technologies
            Try
                imgListVendors.Images.Add("HUAWEI", EmbeddedImage("Logo_Huawei_Short.png"))
                imgListVendors.Images.Add("NORTEL", EmbeddedImage("Logo_Nortel_Short.png"))
                imgListVendors.Images.Add("ERICSSON", EmbeddedImage("Logo_Ericsson_Short.png"))
                imgListVendors.Images.Add("NOKIA", EmbeddedImage("Logo_NSN_Short.jpg"))
                imgListVendors.Images.Add("IPACCESS", EmbeddedImage("Logo_Ipaccess_Short.png"))
                imgListVendors.Images.Add("SMALL", EmbeddedImage("Logo_Small_Cell_Short.png"))
                imgListVendors.Images.Add("COMMON", EmbeddedImage("Common.png"))
                imgListVendors.Images.Add("SMALL H", EmbeddedImage("Logo_Small_Cell_Short_Huawei.png"))
                imgListVendors.Images.Add("SMALL E", EmbeddedImage("Logo_Small_Cell_Short_Ericsson.png"))
                imgListVendors.Images.Add("SMALL N", EmbeddedImage("Logo_Small_Cell_Short_Nokia.png"))
                imgListVendors.Images.Add("ZTE", EmbeddedImage("Logo_ZTE_Short.png"))
                imgListVendors.Images.Add("CNE", EmbeddedImage("Logo_CNE_Short.png"))
            Catch
            End Try
            Update_ProgressBar()
            Application.DoEvents()

            Update_LabelProgressFinished()
            WriteString_Log(Now() & "    " & "Configuration: Success")
            Threading.Thread.Sleep(2000)
            Return True
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            WriteString_Log(Now() & "    " & "Configuration: Failure: " & ex.Message.ToString)
            MsgBox("Failure during objects loading... Closing IOS. Contact Admin. " & Chr(13) & ex.Message.ToString, MsgBoxStyle.ApplicationModal)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            Return False
            Exit Function
        End Try
    End Function

    Public Function StartUpPhase() As Boolean
        'session.queries file is a log file used for tracking/debugging queries fired by IOS
        Dim FILE_NAME As String = GetUserDataPath() & "\session.queries"
        If File.Exists(FILE_NAME) = True Then
            File.Delete(FILE_NAME)
        End If

        If AppVersionCheck() = False Then
            Return False
        End If

        ' Connection string encryption
        ' TG SPECIFIC: PROBLEM RUNNING THIS ON VMWARE machine
        Dim progressstatus As Boolean = False
        WriteString_Log(Now() & "    " & "Configuration IOS Server")
        'Connection String for Drive Test Module

        'License Server Check
        WriteString_Log(Now() & "    " & "Start License Server Process")
        If TestConnection(connStrIOSLicenseServer) = False Then
            WriteString_Log(Now() & "    " & "Configuration: Connection to IOS License Server Failed")
            Update_LabelLicServer_NotReached()
            Threading.Thread.Sleep(3000)
            Return False
        Else
            WriteString_Log(Now() & "    " & "Configuration: Connection to IOS License Server Success")
			Update_LabelLicServer_Found()
		End If

		Update_ProgressBar()
		Application.DoEvents()

		Dim lastaccess As String = Nothing

        'Copyright inf
        Copyright.Text = "Copyright © 2025 CellSens B.V."
        Application.DoEvents()
        WriteString_Log(Now() & "    " & "Start LicenseCheck Process:")
        Dim IOS_License_Result As String = Check_IOS_License_Local()

        If Not System.IO.File.Exists(GetUserDataPath() & "\last.access") Then
            Dim fs As FileStream = File.Create(GetUserDataPath() & "\last.access")
            fs.Close()
            File.SetAttributes(GetUserDataPath() & "\last.access", FileAttributes.Hidden)
        End If
        lastaccess = File.GetLastWriteTime(GetUserDataPath() & "\last.access").ToString("yyyyMMdd")

        If IOS_License_Result = "Valid" Then
            Try
                Update_UserLicense_Success(licenseUser, licenseExpiry.ToString("dd/MM/yyyy"))
                Update_ProgressBar()
                Application.DoEvents()
                Threading.Thread.Sleep(1000)

                WriteString_Log(Now() & "    " & "LicenseCheck: Register Valid Access:" & lastaccess)
                File.SetLastWriteTime(GetUserDataPath() & "\last.access", Now)
                progressstatus = SplashScreen_Progress()
                If progressstatus = False Then
                    Return False
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                WriteString_Log(Now() & "    " & "LicenseCheck: Register Valid Access - Error:" & lastaccess & vbCrLf & ex.Message)
            End Try
        ElseIf IOS_License_Result = "NotReached" Then
            Update_UserLicense_NotReached()
            Threading.Thread.Sleep(3000)
            Return False
        ElseIf IOS_License_Result = "NotValid" Then
            Update_UserLicense_NotValid(licenseUser, licenseExpiry)
            Threading.Thread.Sleep(3000)
            Return False
        ElseIf IOS_License_Result = "IsLocked" Then
            Update_UserLicense_IsLocked()
            Threading.Thread.Sleep(3000)
            Return False
        ElseIf IOS_License_Result = "IsDisabled" Then
            Update_UserLicense_IsDisabled()
            Threading.Thread.Sleep(3000)
            Return False
        Else
            Threading.Thread.Sleep(1000)
            Return False
        End If
        Return True
    End Function

    Private Function AppVersionCheck() As Boolean
        Dim verChkResult As Boolean = True
        Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
        WriteString_Log(Now() & "    " & Version.Text)
        Try
            Dim VersionCheckValue As Boolean = IOSAppConfigManage.VersionCheck
            If VersionCheckValue Then

                'auto update
                Dim commandline As String = Nothing
                WriteString_Log(Now() & "    " & "Start AutoUpdate Process:")

                Dim autoupdate_result As String = AutoUpdate(commandline)
                WriteString_Log(Now() & "    " & "AutoUpdate Result: " & autoupdate_result)

                Application.DoEvents()
                If autoupdate_result = "Failed" Then
                    Update_LabelVersion_Failed()
                ElseIf autoupdate_result = "NoUpdate" Then
                    Update_LabelVersion_NoUpdate()
                ElseIf autoupdate_result = "Update" Then
                    Try
                        File.Copy(GetUserDataPath() & "\session.log", GetUserDataPath() & "\session_LastUpdate.log", True)
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                        WriteString_Log(Now() & "    " & "AutoUpdate Log Error: " & ex.Message)
                    End Try
                    Update_LabelVersion_Update()
                    Threading.Thread.Sleep(3000)
                    verChkResult = False
                End If
            Else
                Update_LabelVersion_Disabled()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            WriteString_Log(Now() & "    " & "Start AutoUpdate Process Failed:" & vbCrLf & ex.Message)
            Update_LabelVersion_Failed()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Update_ProgressBar()
        Application.DoEvents()
        Return verChkResult
    End Function

    Private Function TestConnection(ByVal constr As String) As Boolean
        Try
            Using cnOSS As New System.Data.Odbc.OdbcConnection(constr)
                cnOSS.ConnectionTimeout = 5
                cnOSS.Open()
                Return True
            End Using
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            WriteString_Log(Now() & "    " & "Configuration: TestConnection Failed " & vbCrLf & ex.Message)
            Return False
        End Try
        Return False
    End Function

    Private Function Check_IOS_License_Local() As String
        Dim myData As New DataTable()
        Try
            'Dim host As String = IOSAppConfigManage.HostServer
            'WriteString_Log(Now() & "    " & "LicenseCheck: License Server:" & host)

            'Create MySql connection
            Dim CompanyName As String = IOSAppConfigManage.Company
            Dim strUser As String = Environment.UserName.ToString
            Dim LicenseType As String = ""


            WriteString_Log(Now() & "    " & "LicenseCheck: Register Access")
            IOS.DataLibrary.clsSQLCommands.InsertIOSAccessLog(connStrIOSServer, CompanyName, strUser, Version.Text)


            WriteString_Log(Now() & "    " & "LicenseCheck: Company: " & CompanyName)
            WriteString_Log(Now() & "    " & "LicenseCheck: User: " & strUser)

            myData = IOS.DataLibrary.clsSQLCommands.GetLicenseData(connStrIOSServer, CompanyName, strUser)

            If (myData IsNot Nothing) Then
                If (myData.Rows.Count > 0) Then
                    WriteString_Log(Now() & "    " & "LicenseCheck: Result Available - QueryResult:" & myData.Rows.Count)
                    'Fetch user configuration
                    dtUserConfigClient = IOS.DataLibrary.clsSQLCommands.GetUserConfigClient(connStrIOSServer, strUser)

                    Dim configDT As New DataTable
                    configDT = IOS.DataLibrary.clsSQLCommands.GetLicenseConfigTemplateDetail(connStrIOSServer, myData.Rows(0).Item("configTemplateID"))
                    If (configDT.Rows.Count > 0) Then
                        configData = configDT
                        configMgr.SetConfiguration(configData)
                        configMgr.User.LicenseID = CInt(myData.Rows(0)("LicenseID"))
                        configMgr.User.LicenseType = CStr(myData.Rows(0)("LicenseType"))
                        configMgr.User.LicenseUser = strUser
                        configMgr.User.IsValidUser = True
                        configMgr.User.LicenseCompany = CStr(myData.Rows(0)("LicenseCompany"))
                        configMgr.User.ExpirationDate = CDate(myData.Rows(0)("ExpirationDate"))
                        configMgr.User.WebClientUserName = CStr(IIf(IsDBNull(myData.Rows(0)("Web_GUI_username")), "", myData.Rows(0)("Web_GUI_username")))
                        configMgr.User.WebClientPassword = CStr(IIf(IsDBNull(myData.Rows(0)("Web_GUI_password")), "", myData.Rows(0)("Web_GUI_password")))
                        configMgr.User.IsPowerUser = CBool(IIf(IsDBNull(myData.Rows(0)("IsPowerUser")), False, myData.Rows(0)("IsPowerUser")))
                        If myData.Columns.Contains("IsLocked") AndAlso myData.Columns.Contains("IsEnabled") Then
                            configMgr.User.IsUserLocked = CBool(IIf(IsDBNull(myData.Rows(0)("IsLocked")), False, myData.Rows(0)("IsLocked")))
                            configMgr.User.IsUserEnabled = CBool(IIf(IsDBNull(myData.Rows(0)("IsEnabled")), False, myData.Rows(0)("IsEnabled")))
                        End If
                        If myData.Columns.Contains("Market") Then
                            configMgr.User.UserMarket = CStr(IIf(IsDBNull(myData.Rows(0)("Market")), "", myData.Rows(0)("Market")))
                        Else
                            configMgr.User.UserMarket = ""
                        End If

                        licenseUser = strUser
                        licenseExpiry = CDate(myData.Rows(0)("ExpirationDate"))
                        Try
                            UserTracking = myData.Rows(0)("Tracking")
                        Catch
                        End Try
                    End If
                    WriteString_Log(Now() & "    " & "LicenseCheck: Result Available - Configure Settings ")
                    frmMDI.ConfigurIOSMDI("frmMDI")
                End If
            End If

            'regional settings as per configuration
            ConfigureRegionalSettings()

            If myData Is Nothing Then
				Update_LabelLicense_NotReached()
				WriteString_Log(Now() & "    " & "LicenseCheck: Empty Result - Return of Query - Empty")
				Return "NotReached"
			End If

            If configMgr.User.IsUserLocked = True Then
                licenselabel = "Licensed User: Is Locked !"
                Update_LicenseText()
                Return "IsLocked"
            End If

            If configMgr.User.IsUserEnabled = False Then
                licenselabel = "Licensed User: Is Disabled !"
                Update_LicenseText()
                Return "IsDisabled"
            End If

            Dim IsExpired As Boolean = False
            If (myData.Rows.Count > 0) Then
                Dim dbDate As DateTime = CDate(myData.Rows(0)("ExpirationDate"))
                WriteString_Log(Now() & "    " & "LicenseCheck: Result Available - " & configMgr.User.ExpirationDate.ToShortDateString() & " =? " & dbDate.ToShortDateString())
                If (configMgr.User.IsValidUser) Then
                    IsExpired = Not configMgr.User.ExpirationDate.ToShortDateString() = dbDate.ToShortDateString()
                End If
                chartSetName = myData.Rows(0)("ChartSetName").ToString.ToUpper
            Else
                IsExpired = True
            End If

            'TG:
            IsExpired = False
            If IsExpired Then
                WriteString_Log(Now() & "    " & "LicenseCheck: Result Available - Return NotValid")
                myData.Dispose()
                licenselabel = "Licensed To: LICENSE NOT AVAILABLE !"
				Update_LicenseText()
				Return "NotValid"
			Else
                WriteString_Log(Now() & "    " & "LicenseCheck: Result Available - Return Valid")
                LicenseType = myData.Rows(0)(1).ToString
                If LicenseType = "Trial" Then
                    licenselabel = "Licensed To: TRIAL LICENSE, Expiration:  " & myData.Rows(0)(4).ToString.Substring(0, 10)
                Else
                    licenselabel = "Licensed To: " & CompanyName
                End If
                Update_LicenseText()
				myData.Dispose()
				Return "Valid"
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & ex.StackTrace)
            WriteString_Log(Now() & "    " & "LicenseCheck: Failure - Return NotReached" & vbCrLf & ex.Message & ex.StackTrace)
            Return "NotReached"
        Finally
            myData.Dispose()
            myData = Nothing
        End Try
    End Function

    Private Sub ConfigureRegionalSettings()
        Try
            regionalSettings = GetConfigClientKeyValue("RegionalSettings")
            If regionalSettings = True Then
                CultureInfoDefault = CultureInfo.CurrentCulture
                CultureUIDefault = CultureInfo.CurrentUICulture
                Dim installedCulture As CultureInfo = CultureInfo.InstalledUICulture

                Thread.CurrentThread.CurrentCulture = CultureInfoDefault
                Thread.CurrentThread.CurrentUICulture = CultureUIDefault

                CultureInfo.DefaultThreadCurrentCulture = CultureInfoDefault
                CultureInfo.DefaultThreadCurrentUICulture = CultureUIDefault
            Else
                CultureUIDefault = Globalization.CultureInfo.GetCultureInfo("en-US")
                CultureInfoDefault = Globalization.CultureInfo.GetCultureInfo("en-US")

                Thread.CurrentThread.CurrentCulture = CultureInfoDefault
                Thread.CurrentThread.CurrentUICulture = CultureUIDefault

                CultureInfo.DefaultThreadCurrentCulture = CultureInfoDefault
                CultureInfo.DefaultThreadCurrentUICulture = CultureUIDefault
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & ex.StackTrace)
        End Try
    End Sub

#End Region

End Class