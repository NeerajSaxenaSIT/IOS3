Imports System
Imports System.IO
Imports System.Configuration

Public Module CommonModule

    Public Sub WriteString_Query(ByVal text2append As String)
        Try
            Dim FILE_NAME As String = GetUserDataPath() & "\session.queries"
            Static LogFileLock As New Object()
            SyncLock LogFileLock
                File.AppendAllText(FILE_NAME, text2append)
                File.SetAttributes(FILE_NAME, FileAttributes.Hidden)
            End SyncLock
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetUserDataPath() As String
        Dim basePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim dataPath As String = String.Format("{0}\{1}\{2}\{3}", basePath, "CellSens", "CIOS", ConfigurationManager.AppSettings("DeploymentName").ToString())
        If Not Directory.Exists(dataPath) Then
            Directory.CreateDirectory(dataPath)
        End If
        Return dataPath
    End Function

End Module
