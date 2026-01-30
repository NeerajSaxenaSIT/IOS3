Imports System.IO
Imports System.Net
Imports System.Threading.Tasks


Public Class frm_AutoUpdate

    Private t_list As New List(Of Threading.Thread)
    'Private Call_ProcessDownloadInvoked As New MethodInvoker(AddressOf Process_Download_Invoked)
    'Private Delegate Sub Call_ProcessDownloadInvoked(ByRef lbl As Label, ByRef prg As ProgressBar)

    Private RemoteUrl As String
    Private localFilePath As String
    Private bufferSize As Integer = 81920

    Private WithEvents fileDownloader As Net.WebClient

    Private Sub frm_AutoUpdate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Visible = True
        Me.Show()
        lbl_Status2.Text = "Contacting Remote IOS Server... "
        Me.BringToFront()
        Application.DoEvents()
        'Call Main()
        bgWorker.WorkerReportsProgress = True
        bgWorker.RunWorkerAsync()

        'Me.Close()
    End Sub

    Public Sub Main()
        '  downloadbusy = False
        t_list.Clear()

        Dim ExeFile As String ' the program that called the auto update
        Dim RemoteUri As String ' the web location of the files
        Dim Files() As String ' the list of files to be updated
        Dim Key As String ' the key used by the program when called back 

        ' to know that the program was launched by the 
        ' Auto Update program
        WriteString_Log(Now() & "    " & "AutoUpdate Agent Started:")

        Dim CommandLine As String ' the command line passed to the original 

        ' program if is the case

        Dim myWebClient As New WebClient ' the web client
        'FileProgressBar2.Value = 0

        Application.DoEvents()
        Threading.Thread.Sleep(1500)

        Try
            ' Get the parameters sent by the application

            Dim param() As String = Split(Microsoft.VisualBasic.Command(), "|")
            ExeFile = param(0)
            RemoteUri = param(1)
            ' the files to be updated should be separeted by "?"

            Files = Split(param(2), "?")
            Key = param(3)
            CommandLine = param(4)

            WriteString_Log(Now() & "    " & "AutoUpdate Agent CommandLine:" & CommandLine)
        Catch ex As Exception
            ' if the parameters wasn't right just terminate the program
            ' this will happen if the program wasn't called by the system 
            ' to be updated
            WriteString_Log(Now() & "    " & "AutoUpdate Agent Error Parameters: " & vbCrLf & ex.Message)

            Exit Sub
        End Try
        Try
            ' Process each file 

            For i As Integer = 0 To Files.Length - 1

                Try
                    ' try to rename the current file before download the new one
                    ' this is a good procedure since the file can be in use
                    WriteString_Log(Now() & "    " & "AutoUpdate Agent Renaming: " & Files(i) & "-> *.old")

                    File.Move(System.IO.Path.GetDirectoryName(ExeFile) & "\" & Files(i), System.IO.Path.GetDirectoryName(ExeFile) & "\" & Now.TimeOfDay.TotalMilliseconds & ".old")
                Catch ex As Exception
                    WriteString_Log(Now() & "    " & "AutoUpdate Agent Renaming Error: " & Files(i) & "-> *.old" & vbCrLf & ex.Message)

                End Try
                ' download the new version

                Try
                    If Files(i).Contains("\") Then
                        If Not IO.Directory.Exists(System.IO.Path.GetDirectoryName(ExeFile) + "\" + Split(Files(i), "\")(0)) Then
                            IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(ExeFile) + "\" + Split(Files(i), "\")(0))
                        End If
                    End If
                Catch ex As Exception
                    WriteString_Log(Now() & "    " & "AutoUpdate Agent Directory Error : " & Files(i) & vbCrLf & ex.Message)
                End Try


                'lbl_Status2.Text = "Downloading... " & Files(i)
                bgWorker.ReportProgress(0, "Downloading... " & Files(i))
                '   downloadbusy = True
                Application.DoEvents()

                bgWorker.ReportProgress(0, "0")

                Threading.Thread.Sleep(100)
                WriteString_Log(Now() & "    " & "AutoUpdate Agent Initiate Download: " & RemoteUri & Files(i))


                'Dim t As New Task(InitiateDownloadHttpClient(RemoteUri & Files(i), System.IO.Path.GetDirectoryName(ExeFile) & "\" & Files(i)))

                'Await t.Wait(5000)

                'Dim au_t As New Thread_AutoUpdate()

                'au_t.lblDownloadPrg = Me.Downloadprogresslabel2
                'au_t.prgBarDownload = Me.Downloadprogressbar2

                'au_t.remoteUrl = RemoteUri & Files(i)
                'au_t.localFilePath = Path.GetDirectoryName(ExeFile) & "\" & Files(i)

                'AddHandler au_t.ThreadComplete, AddressOf Process_Download_ThreadEnd
                'Dim thread_x As New Threading.Thread(AddressOf au_t.InitiateDownloadHttpClient)
                'thread_x.Start()

                't_list.Add(thread_x)

                RemoteUrl = RemoteUri & Files(i)
                localFilePath = Path.GetDirectoryName(ExeFile) & "\" & Files(i)
                InitiateDownloadHttpClient(RemoteUrl, localFilePath)

                ' InitiateDownload(RemoteUri & Files(i), System.IO.Path.GetDirectoryName(ExeFile) & "\" & Files(i))
                'Dim result As String = "started"
                'DownloadFile(RemoteUri & Files(i), System.IO.Path.GetDirectoryName(ExeFile) & "\" & Files(i)).GetAwaiter().GetResult()

                'waiting for file to complete (async download)
                'While result = "started"
                '    Application.DoEvents()
                'End While

                'FileProgressBar2.Value = (i + 1) / (Files.Length) * 100
                bgWorker.ReportProgress(0, (i + 1) / (Files.Length) * 100)

                'myWebClient.DownloadFile(RemoteUri & Files(i), Application.StartupPath & "\" & Files(i))
            Next
            WriteString_Log(Now() & "    " & "AutoUpdate Agent Completed -> Restarting")

            '   downloadbusy = False
            ' Call back the system with the original command line 
            ' with the key at the end

            'lbl_Status2.Text = "Finished ! Restarting..."
            bgWorker.ReportProgress(0, "Finished ! Restarting...")
            Application.DoEvents()

            Threading.Thread.Sleep(1500)

            'System.Diagnostics.Process.Start(ExeFile, CommandLine & Key)
            MsgBox("Done ... Restart IOS Manually !")

            ' do some clean up -  delete all .old files (if possible) 
            ' in the current directory
            ' if some file stays it will be cleaned next time
            Dim S As String = Dir(System.IO.Path.GetDirectoryName(ExeFile) & "\*.old")
            Do While S <> ""
                Try
                    WriteString_Log(Now() & "    " & "AutoUpdate Agent Cleaning *.old")

                    File.Delete(System.IO.Path.GetDirectoryName(ExeFile) & "\" & S)
                Catch ex As Exception
                End Try
                S = Dir()
            Loop
        Catch ex As Exception
            ' something went wrong... 
            WriteString_Log(Now() & "    " & "AutoUpdate Agent Error: " & vbCrLf & ex.Message)

            MsgBox("There was a problem runing the Auto Update." & vbCr &
                "Please Contact Support@CellSens.com" & vbCr & ex.Message,
                MsgBoxStyle.Critical)
        End Try
    End Sub

    'Private Sub Process_Download_ThreadEnd(lbl As Label, prg As ProgressBar, ti As Threading.Thread)
    '    Try
    '        Dim threadcorrect As Boolean = False
    '        For Each tid In t_list
    '            If tid.ManagedThreadId = ti.ManagedThreadId Then
    '                threadcorrect = True
    '            End If
    '        Next
    '        If threadcorrect = False Then
    '            Exit Sub
    '        End If
    '        Dim args() As Object = {lbl, prg}
    '        Me.BeginInvoke(New Call_ProcessDownloadInvoked(AddressOf Process_Download_Invoked), args)
    '    Catch
    '    End Try
    'End Sub

    'Protected Friend Sub Process_Download_Invoked(ByRef lbl As Label, ByRef prg As ProgressBar)
    '    lbl.Text = "100%"
    '    prg.Value = Downloadprogressbar2.Maximum
    'End Sub

    'Public Async Function InitiateDownloadHttpClient(ByVal remoteUrl As String, ByVal localFilePath As String) As Task

    '    Dim fileDownloaderHttp As New System.Net.Http.HttpClient

    '    Try



    '        ' Send asynchronous request to get the file
    '        Dim response = Await fileDownloaderHttp.GetAsync(remoteUrl, Http.HttpCompletionOption.ResponseHeadersRead)



    '        If response.IsSuccessStatusCode Then
    '            ' Read the Last-Modified header from the response, if present
    '            Dim lastModifiedString = response.Content.Headers.LastModified.ToString()

    '            ' Read the content into a stream
    '            Using streamToReadFrom = Await response.Content.ReadAsStreamAsync()
    '                ' Create a new file stream to write the downloaded content
    '                Using streamToWriteTo = File.Open(localFilePath, FileMode.Create)
    '                    Await streamToReadFrom.CopyToAsync(streamToWriteTo)
    '                End Using
    '            End Using

    '            If Not String.IsNullOrEmpty(lastModifiedString) Then
    '                Dim lastModified = DateTime.Parse(lastModifiedString)
    '                ' Set the Last Modified date of the local file
    '                File.SetLastWriteTime(localFilePath, lastModified)
    '            End If

    '            ' Update progress to 100% upon successful download
    '            Me.Downloadprogresslabel2.Text = "100%"
    '            Me.Downloadprogressbar2.Value = Downloadprogressbar2.Maximum


    '        Else
    '            ' Handle the case where the HTTP request failed
    '            MessageBox.Show($"Failed to download file. HTTP status: {response.StatusCode}")
    '        End If
    '    Catch ex As Exception
    '        ' Handle potential exceptions, such as network errors
    '        MessageBox.Show($"Error downloading file: {ex.Message}")

    '    Finally
    '        fileDownloaderHttp.Dispose()

    '    End Try

    'End Function

    Private Sub InitiateDownload(ByVal remoteUrl As String, ByVal localFilePath As String)
        'Reset the progress indicators.
        Me.Downloadprogresslabel2.Text = (0).ToString & "%"
        Me.Downloadprogressbar2.Value = 0

        If Me.fileDownloader Is Nothing Then
            'This is the first download so create the web client.
            Me.fileDownloader = New Net.WebClient
        End If

        'Start downloading the file in the background.
        Me.fileDownloader.DownloadFileAsync(New Uri(remoteUrl), localFilePath)

    End Sub

    Private Sub fileDownloader_DownloadProgressChanged(ByVal sender As Object,
                                                       ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles fileDownloader.DownloadProgressChanged
        'Update the progress.
        Me.Downloadprogresslabel2.Text = e.ProgressPercentage.ToString & "%"
        Me.Downloadprogressbar2.Value = CInt(Math.Round(Me.Downloadprogressbar2.Maximum * e.ProgressPercentage / 100))
    End Sub

    Private Sub fileDownloader_DownloadFileCompleted(ByVal sender As Object,
                                                     ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles fileDownloader.DownloadFileCompleted
        'Notify the user.
        'MessageBox.Show("Download Complete")

        'Reset the progress indicators.
        '  downloadbusy = False
    End Sub

    Private Sub Form1_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Me.fileDownloader IsNot Nothing Then
            'Destroy the web client object.
            Me.fileDownloader.Dispose()
        End If
    End Sub

    Public Function GetUserDataPath() As String
        Dim basePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim dataPath As String = String.Format("{0}\{1}\{2}", basePath, "CellSens", "IOS")
        If Not Directory.Exists(dataPath) Then
            Directory.CreateDirectory(dataPath)
        End If
        Return dataPath
    End Function

    Public Sub WriteString_Log(ByVal text2append As String)
        Try
            Dim FILE_NAME As String = GetUserDataPath() & "\session_LastUpdate.log"

            Static LogFileLock As New Object()
            SyncLock LogFileLock

                File.AppendAllText(FILE_NAME, text2append & vbCrLf)
                ' File.SetAttributes(FILE_NAME, FileAttributes.Hidden)

            End SyncLock
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork
        Try
            Call Main()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub bgWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorker.ProgressChanged
        Try
            If e.UserState.ToString.Contains("Downloading") Or e.UserState.ToString.Contains("Finished") Then
                lbl_Status2.Text = e.UserState.ToString
            ElseIf e.UserState.ToString = "0" Then
                Downloadprogresslabel2.Text = "0%"
                Downloadprogressbar2.Value = Downloadprogressbar2.Minimum
            ElseIf e.UserState.ToString = "100" Then
                Downloadprogresslabel2.Text = "100%"
                Downloadprogressbar2.Value = Downloadprogressbar2.Maximum
            Else
                FileProgressBar2.Value = CInt(e.UserState)
            End If
            Application.DoEvents()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        Try
            Me.Close()
        Catch ex As Exception
        Finally
            bgWorker.CancelAsync()
            bgWorker.Dispose()
        End Try
    End Sub

    Public Async Sub InitiateDownloadHttpClient(remoteUrl As String, localFilePath As String)
        Dim fileDownloaderHttp As New System.Net.Http.HttpClient
        Try
            ' Send asynchronous request to get the file
            Dim response = Await fileDownloaderHttp.GetAsync(remoteUrl, Http.HttpCompletionOption.ResponseHeadersRead)

            If response.IsSuccessStatusCode Then
                ' Read the Last-Modified header from the response, if present
                Dim lastModifiedString = response.Content.Headers.LastModified.ToString()

                ' Read the content into a stream
                Using streamToReadFrom = Await response.Content.ReadAsStreamAsync()
                    ' Create a new file stream to write the downloaded content
                    Using streamToWriteTo = File.Open(localFilePath, FileMode.Create)
                        Await streamToReadFrom.CopyToAsync(streamToWriteTo, bufferSize)
                    End Using
                End Using

                If Not String.IsNullOrEmpty(lastModifiedString) Then
                    Dim lastModified = DateTime.Parse(lastModifiedString)
                    ' Set the Last Modified date of the local file
                    File.SetLastWriteTime(localFilePath, lastModified)
                End If

                bgWorker.ReportProgress(0, "100")

            Else
                ' Handle the case where the HTTP request failed
                MessageBox.Show($"Failed to download file. HTTP status: {response.StatusCode}" & " File:" & localFilePath)
            End If
        Catch ex As Exception
            ' Handle potential exceptions, such as network errors
            MessageBox.Show($"Error downloading file: {ex.Message}")

        Finally
            fileDownloaderHttp.Dispose()
        End Try

    End Sub

End Class

Public Class Thread_AutoUpdate

    'Public lblDownloadPrg As Label
    'Public prgBarDownload As ProgressBar

    Public remoteUrl As String
    Public localFilePath As String

    Public Event ThreadComplete(lblas As Label, prg As ProgressBar, ti As Threading.Thread)

    Public Async Sub InitiateDownloadHttpClient()
        Dim fileDownloaderHttp As New System.Net.Http.HttpClient
        Try
            ' Send asynchronous request to get the file
            Dim response = Await fileDownloaderHttp.GetAsync(remoteUrl, Http.HttpCompletionOption.ResponseHeadersRead)

            If response.IsSuccessStatusCode Then
                ' Read the Last-Modified header from the response, if present
                Dim lastModifiedString = response.Content.Headers.LastModified.ToString()

                ' Read the content into a stream
                Using streamToReadFrom = Await response.Content.ReadAsStreamAsync()
                    ' Create a new file stream to write the downloaded content
                    Using streamToWriteTo = File.Open(localFilePath, FileMode.Create)
                        Await streamToReadFrom.CopyToAsync(streamToWriteTo)
                    End Using
                End Using

                If Not String.IsNullOrEmpty(lastModifiedString) Then
                    Dim lastModified = DateTime.Parse(lastModifiedString)
                    ' Set the Last Modified date of the local file
                    File.SetLastWriteTime(localFilePath, lastModified)
                End If

                ' Update progress to 100% upon successful download
                'Me.lblDownloadPrg.Text = "100%"
                'Me.prgBarDownload.Value = prgBarDownload.Maximum

            Else
                ' Handle the case where the HTTP request failed
                MessageBox.Show($"Failed to download file. HTTP status: {response.StatusCode}")
            End If
        Catch ex As Exception
            ' Handle potential exceptions, such as network errors
            MessageBox.Show($"Error downloading file: {ex.Message}")

        Finally
            fileDownloaderHttp.Dispose()
        End Try

    End Sub

End Class
