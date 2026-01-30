Public Class clsWaitScreen

    Public Sub ShowWaitScreen(Optional _msg As String = "Loading...", Optional ByVal ClosingDelay As Int16 = 500)
        Try
            If frmMDI.SplashScreenManager1.IsSplashFormVisible = False Then
                frmMDI.SplashScreenManager1.Properties.ClosingDelay = ClosingDelay
                frmMDI.SplashScreenManager1.ShowWaitForm()
            End If
            frmMDI.SplashScreenManager1.SetWaitFormDescription(_msg)
        Catch ex As Exception
        End Try
    End Sub


    Public Sub CloseWaitScreen()
        Try
            If frmMDI.SplashScreenManager1.IsSplashFormVisible = True Then
                frmMDI.SplashScreenManager1.CloseWaitForm()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ShowDataMartWaitScreen(Optional _msg As String = "Loading...", Optional ByVal ClosingDelay As Int16 = 500)
        Try
            If frmSBMain.SplashScreenManager1.IsSplashFormVisible = False Then
                frmMDI.SplashScreenManager1.Properties.ClosingDelay = ClosingDelay
                frmSBMain.SplashScreenManager1.ShowWaitForm()
            End If
            frmSBMain.SplashScreenManager1.SetWaitFormDescription(_msg)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub CloseDataMartWaitScreen()
        Try
            If frmSBMain.SplashScreenManager1.IsSplashFormVisible = True Then
                frmSBMain.SplashScreenManager1.CloseWaitForm()
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
