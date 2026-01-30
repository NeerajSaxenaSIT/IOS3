Imports System.Globalization
Imports System.Threading
Imports IOS.Configuration
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            ' If the user clicks No, then exit.
            _logger.SetError("Unhandled Exception - " & e.Exception.Message & vbCrLf & "STACKTRACE: " & e.Exception.StackTrace)

            Dim tohide As Boolean = False
            Try
                If My.Forms.SplashScreen.Visible = True Then
                    My.Forms.SplashScreen.Hide()
                    tohide = True
                End If

            Catch ex As Exception

            End Try

            e.ExitApplication = MessageBox.Show(e.Exception.Message & vbCrLf & "Continue?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No
            Try
                If tohide = True Then
                    My.Forms.SplashScreen.Show()
                End If
            Catch
            End Try
        End Sub

        'Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        'Try
        'Dim serverLocation As String = IOSAppConfigManage.AppServerLocation '"en-IN"
        'New CultureInfo(serverLocation)

        'CultureInfoDefault = CultureInfo.CurrentCulture
        'CultureUIDefault = CultureInfo.CurrentUICulture
        'Dim installedCulture As CultureInfo = CultureInfo.InstalledUICulture

        'Thread.CurrentThread.CurrentCulture = CultureInfoDefault
        'Thread.CurrentThread.CurrentUICulture = CultureUIDefault

        'CultureInfo.DefaultThreadCurrentCulture = CultureInfoDefault
        'CultureInfo.DefaultThreadCurrentUICulture = CultureUIDefault

        'currCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy"

        'Console.WriteLine("Current Culture: " & currCulture.Name)
        'Console.WriteLine("Current UI Culture: " & uiCulture.Name)
        'Console.WriteLine("Current UI Culture: " & installedCulture.Name)
        'Catch ex As Exception
        'End Try
        'End Sub

    End Class


End Namespace

