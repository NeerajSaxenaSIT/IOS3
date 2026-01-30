Imports Microsoft.Win32
Imports Microsoft.Web.WebView2.Core

Public Class frmInternetExplorer

    Sub New()
        SetBrowserFeatureControl()

        webview2Alert = False
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'AddHandler webView2.NavigationCompleted, AddressOf webView2_NavigationCompleted
        InitializeAsync()

    End Sub

    Sub New(calledFrom As String, nvgtUrl As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeAsync(calledFrom, nvgtUrl)

    End Sub

    Private Async Sub InitializeAsync()
        'check whether webview2 runtime is installed on the client machine.
        Dim wv2rtVersion As String = Nothing
        Dim keyExists As Boolean = False
        Dim webViewVersion As String = ""

        Try
            wv2rtVersion = CoreWebView2Environment.GetAvailableBrowserVersionString()
        Catch
        End Try

        keyExists = WebViewIsInstalled()
        'webViewVersion = GetWebView2RuntimeBitness()

        If (wv2rtVersion Is Nothing Or keyExists = False) Then
            If keyExists = False Then
                Dim objAlert As New dlgWebView2Alert()
                objAlert.ShowDialog()
                Exit Sub
            End If
        End If

        If (webViewVersion = "32-bit") Or (webViewVersion = "Not Installed") Then
            Dim objAlert As New dlgWebView2Alert()
            objAlert.ShowDialog()
            Exit Sub
        End If

        WebRequestFrom = "SupportRequest"
        NavigationUrl = GetConfigClientKeyValue("SupportWebURL")
        Dim userDataFolder As String = GetUserDataPath()
        Dim env = Await CoreWebView2Environment.CreateAsync(Nothing, userDataFolder, Nothing)
        Await webView2.EnsureCoreWebView2Async(env)
        webView2.Source = New Uri(NavigationUrl)

    End Sub

    Private Async Sub InitializeAsync(requestFrom As String, url As String)
        'check whether webview2 runtime is installed on the client machine.
        Dim wv2rtVersion As String = Nothing
        Dim keyExists As Boolean = False
        Dim webViewVersion As String = ""
        Try
            wv2rtVersion = CoreWebView2Environment.GetAvailableBrowserVersionString()
        Catch
        End Try

        keyExists = WebViewIsInstalled()
        'webViewVersion = GetWebView2RuntimeBitness()

        If (wv2rtVersion Is Nothing Or keyExists = False) Then
            If keyExists = False Then
                Dim objAlert As New dlgWebView2Alert()
                objAlert.ShowDialog()
                Exit Sub
            End If
        End If

        If (webViewVersion = "32-bit") Or (webViewVersion = "Not Installed") Then
            Dim objAlert As New dlgWebView2Alert()
            objAlert.ShowDialog()
            Exit Sub
        End If

        WebRequestFrom = requestFrom
        NavigationUrl = url
        Dim userDataFolder As String = GetUserDataPath()
        Dim env = Await CoreWebView2Environment.CreateAsync(Nothing, userDataFolder, Nothing)
        Await webView2.EnsureCoreWebView2Async(env)
        webView2.Source = New Uri(NavigationUrl)

    End Sub

    Private Function WebViewIsInstalled() As Boolean
        Try
            Dim regKey As String = "SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients"
            Using edgeKey As RegistryKey = Registry.LocalMachine.OpenSubKey(regKey)
                If edgeKey IsNot Nothing Then
                    Dim productKeys As String() = edgeKey.GetSubKeyNames()
                    If productKeys.Any() Then
                        Return True
                    End If
                End If
            End Using
        Catch
            Return False
        End Try
        Return Nothing
    End Function

    Public Function GetWebView2RuntimeBitness() As String
        Dim key32 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients")
        Dim key64 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\EdgeUpdate\Clients")

        Dim found32 As Boolean = False
        Dim found64 As Boolean = False

        If key32 IsNot Nothing Then
            For Each subkeyName As String In key32.GetSubKeyNames()
                Dim subkey = key32.OpenSubKey(subkeyName)
                If subkey.GetValue("name", "").ToString().Contains("WebView2") Then
                    found32 = True
                    Exit For
                End If
            Next
        End If

        If key64 IsNot Nothing Then
            For Each subkeyName As String In key64.GetSubKeyNames()
                Dim subkey = key64.OpenSubKey(subkeyName)
                If subkey.GetValue("name", "").ToString().Contains("WebView2") Then
                    found64 = True
                    Exit For
                End If
            Next
        End If

        If found64 Then
            Return "64-bit"
        ElseIf found32 Then
            Return "32-bit"
        Else
            Return "Not Installed"
        End If
    End Function

    Private _webRequestFrom As String
    Public Property WebRequestFrom() As String
        Get
            Return _webRequestFrom
        End Get
        Set(ByVal value As String)
            _webRequestFrom = value
        End Set
    End Property

    Private _navigationUrl As String
    Public Property NavigationUrl() As String
        Get
            'webView2.NavigateToString(_navigationUrl)
            Return _navigationUrl
        End Get
        Set(ByVal value As String)
            _navigationUrl = value
        End Set
    End Property

    Private Sub frmInternetExplorer_Load(sender As Object, e As EventArgs) Handles Me.Load
        'webView2.AllowNavigation = True
        Dim SupportWebURL As String = NavigationUrl
        'webView2.NavigateToString(SupportWebURL)
        If (WebRequestFrom = "SupportRequest") Then
            Timer1.Interval = 1000
            Timer1.Enabled = True
            Timer1.Start()
        End If
    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            'Dim doc As HtmlDocument
            'doc = WebBrowser1.Document
            'If webView2.ReadyState = WebBrowserReadyState.Complete Then
            'Dim html As String = Await webView2.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHtml")
            Dim logout As String = Await webView2.CoreWebView2.ExecuteScriptAsync("document.getElementById('logoffBtn')")
            Dim username As String = Await webView2.CoreWebView2.ExecuteScriptAsync("document.getElementById('email')")
            Dim password As String = Await webView2.CoreWebView2.ExecuteScriptAsync("document.getElementById('password')")
            Dim login As String = Await webView2.CoreWebView2.ExecuteScriptAsync("document.getElementById('logonButton')")

            'username.SetAttribute("value", configMgr.User.WebClientUserName)
            'password.SetAttribute("value", configMgr.User.WebClientPassword)
            username = configMgr.User.WebClientUserName
            password = configMgr.User.WebClientPassword

            If logout IsNot Nothing Then
                'Do nothing
            Else
                'login.InvokeMember("click")
                Await webView2.CoreWebView2.ExecuteScriptAsync("document.getElementById('logonButton').click();")
                Timer1.Enabled = False
                Timer1.Stop()
            End If
            'End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
        End Try
    End Sub

    Private Sub SetBrowserFeatureControlKey(feature As String, appName As String, value As UInteger)
        Using key = Registry.CurrentUser.CreateSubKey([String].Concat("Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature), RegistryKeyPermissionCheck.ReadWriteSubTree)
            key.SetValue(appName, CType(value, UInt32), RegistryValueKind.DWord)
        End Using
    End Sub

    Private Sub SetBrowserFeatureControl()
        ' FeatureControl settings are per-process
        Dim fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName)

        ' make the control is not running inside Visual Studio Designer
        If [String].Compare(fileName, "devenv.exe", True) = 0 OrElse [String].Compare(fileName, "XDesProc.exe", True) = 0 Then
            Return
        End If

        SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode())
        ' Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
        SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1)
        SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0)
        SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1)
    End Sub

    Private Function GetBrowserEmulationMode() As UInt32
        Dim browserVersion As Integer = 7
        Using ieKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.QueryValues)
            Dim version = ieKey.GetValue("svcVersion")
            If version Is Nothing Then
                version = ieKey.GetValue("Version")
                If version Is Nothing Then
                    Throw New ApplicationException("Microsoft Internet Explorer is required!")
                End If
            End If
            Integer.TryParse(version.ToString().Split("."c)(0), browserVersion)
        End Using

        Dim mode As UInt32 = 11000
        ' Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
        Select Case browserVersion
            Case 7
                mode = 7000
                ' Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                Exit Select
            Case 8
                mode = 8000
                ' Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                Exit Select
            Case 9
                mode = 9000
                ' Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                Exit Select
            Case 10
                mode = 10000
                ' Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                Exit Select
            Case 11
                mode = 11000
                ' Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 mode. Default value for Internet Explorer 11.
                Exit Select
            Case Else
                ' use IE11 mode by default
                Exit Select
        End Select

        Return mode
    End Function

End Class