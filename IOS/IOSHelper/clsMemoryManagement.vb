Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Public Class clsMemoryManagement
    ' Methods
    Public Sub ReleaseMemory()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
                clsMemoryManagement.SetProcessWorkingSetSize(Process.GetCurrentProcess.Handle, -1, -1)
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function SetProcessWorkingSetSize(ByVal procHandle As IntPtr, ByVal min As Integer, ByVal max As Integer) As Boolean
    End Function

End Class


