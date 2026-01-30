Imports System.IO
Imports System.Net
Imports DevExpress.XtraEditors

Public Class frmImportDrivetest

#Region "Private Variable"

    Private conStr As String = IOS.Configuration.IOSAppConfigManage.IOSServer
    Private conStrDriveTest As String = IOS.Configuration.IOSAppConfigManage.DriveTest
    Private projectName As String = Nothing
    Private driveTest As String = Nothing
    Private dtFileList As New DataTable
    Private isUploading As Boolean = False
    Private ftp_FileUpload As FtpWebRequest
    Private isCancel As Boolean = False
    Private currentProjectName As String = Nothing
    Private currentDriveTest As String = Nothing
    Private currentDeviceName As String = Nothing
    Private rowAppearance As New List(Of KeyValuePair(Of Integer, Color))
    Private DriveTestFilesPath As String = Nothing
    Private ImportFileHost As String = Nothing
    Private IsImportFile As Boolean = False
    Private ImportFileHostUser As String = Nothing
    Private ImportFileHostPassword As String = Nothing

#End Region

#Region "Public Property"

    Public WriteOnly Property SetConnectionString() As String
        Set(ByVal value As String)
            conStr = value
        End Set
    End Property

    Public WriteOnly Property SetConnectionStringDriveTest() As String
        Set(ByVal value As String)
            conStrDriveTest = value
        End Set
    End Property

#End Region

#Region "Form Events"

    Private Sub frm_DrivetestImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.SuspendLayout()

            DriveTestFilesPath = GetConfigClientKeyValue("DriveTestFilesPath")
            ImportFileHost = GetConfigClientKeyValue("ImportFileHost")
            IsImportFile = GetConfigClientKeyValue("IsImportFile")
            ImportFileHostUser = GetConfigClientKeyValue("ImportFileHostUser")
            ImportFileHostPassword = GetConfigClientKeyValue("ImportFileHostPassword")

            rdoProjectExist.Checked = True

            dtFileList.Columns.Add("FileName")
            dtFileList.Columns.Add("FilePath")
            dtFileList.Columns.Add("FileSize")

            'If (rdoDeviceNew.Checked) Then
            '    rdoDeviceExist.Checked = False
            '    gcDevice.Enabled = False
            '    txtDeviceNew.Enabled = True
            'End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Common Functions"

    Private Sub SetStatusMessage(ByVal message As String)
        tsStatusLabelStatus.ForeColor = Color.Red
        tsStatusLabelStatus.Visible = True
        tsStatusLabelStatus.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        tsStatusLabelStatus.Text = "Status : "
        tsStatusLabelStatus.ForeColor = Color.Black
        'tsStatusLabelStatus.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

#End Region

#Region "Level:1 Project"

    Private Sub BindProject()
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim dt As DataTable = IOS.DataLibrary.clsSQLCommands.GetProjectList(conStrDriveTest)
            Library.IOSDevExpressGrid.PopulateDataInGrid(gcProject, gvProject, dt, "ALL", , "Project")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            gcDrivetest.ResumeLayout()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdoProjectNew_CheckedChanged(sender As Object, e As EventArgs) Handles rdoProjectNew.CheckedChanged
        If (rdoProjectNew.Checked) Then
            gcProject.Enabled = Not rdoProjectNew.Checked
            txtProjectNew.Enabled = rdoProjectNew.Checked
            rdoDriveTestNew.Checked = rdoProjectNew.Checked
            rdoDriveTestExist.Enabled = Not rdoProjectNew.Checked
        End If
    End Sub

    Private Sub rdoProjectExist_CheckedChanged(sender As Object, e As EventArgs) Handles rdoProjectExist.CheckedChanged
        Try
            If (rdoProjectExist.Checked) Then
                txtProjectNew.Enabled = Not rdoProjectExist.Checked
                gcProject.Enabled = rdoProjectExist.Checked
                rdoDriveTestExist.Enabled = rdoProjectExist.Checked
                rdoDriveTestExist.Checked = rdoProjectExist.Checked
                BindProject()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvProject_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvProject.FocusedRowChanged
        Try
            If (gvProject.GetSelectedRows().Count > 0) Then
                Dim dr As DataRow = Nothing
                dr = TryCast(gvProject.GetRow(gvProject.GetSelectedRows()(0)), DataRowView).Row
                projectName = dr.Item(0).ToString

                If (projectName IsNot Nothing) Then
                    BindDriveTest(projectName)
                End If

                If (rdoDriveTestExist.Checked) Then
                    If (projectName Is Nothing) Then
                        gcDrivetest.Enabled = True
                        SetStatusMessage("Select Project Name.")
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Level:2 Drivetest"

    Private Sub BindDriveTest(projectName As String)
        Try
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Dim dt As DataTable = IOS.DataLibrary.clsSQLCommands.GetDriveTestByProject(conStrDriveTest, projectName)
            Library.IOSDevExpressGrid.PopulateDataInGrid(gcDrivetest, gvDrivetest, dt, "ALL", , "DriveTest")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            gcDrivetest.ResumeLayout()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdoDriveTestNew_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDriveTestNew.CheckedChanged
        If (rdoDriveTestNew.Checked) Then
            gcDrivetest.Enabled = Not rdoDriveTestNew.Checked
            txtDrivetestNew.Enabled = rdoDriveTestNew.Checked
            gcDevice.Enabled = Not rdoDriveTestNew.Checked
        End If
    End Sub

    Private Sub rdoDriveTestExist_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDriveTestExist.CheckedChanged
        Try
            If (rdoDriveTestExist.Checked) Then
                txtDrivetestNew.Enabled = Not rdoDriveTestExist.Checked
                gcDrivetest.Enabled = rdoDriveTestExist.Checked
                gcDevice.Enabled = rdoDriveTestExist.Checked

                Dim dr As DataRow = Nothing
                If gvProject.RowCount > 0 Then
                    dr = TryCast(gvProject.GetRow(gvProject.GetSelectedRows()(0)), DataRowView).Row
                    projectName = dr.Item(0).ToString

                    If (projectName IsNot Nothing) Then
                        BindDriveTest(projectName)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvDrivetest_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvDrivetest.FocusedRowChanged
        Dim dtID As String = Nothing
        Dim flag As Boolean = True
        Try
            If (gvDrivetest.GetSelectedRows().Count > 0) Then
                Dim dr As DataRow = Nothing
                dr = TryCast(gvDrivetest.GetRow(gvDrivetest.GetSelectedRows()(0)), DataRowView).Row
                driveTest = dr.Item(0).ToString

                If (driveTest IsNot Nothing) Then
                    BindDevice(driveTest)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Level:3 Device"

    Private Sub BindDevice(ByVal driveTest As String)
        Dim dt As DataTable = IOS.DataLibrary.clsSQLCommands.GetDeviceListByDriveTest(conStrDriveTest, driveTest)
        Library.IOSDevExpressGrid.PopulateDataInGrid(gcDevice, gvDevice, dt, "ALL", , "Device")
    End Sub

    Private Sub rdoDeviceNew_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDeviceNew.CheckedChanged
        If (rdoDeviceNew.Checked) Then
            gcDevice.Enabled = Not rdoDeviceNew.Checked
            txtDeviceNew.Enabled = rdoDeviceNew.Checked
        End If
    End Sub

#End Region

#Region "Files : Step 3"

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim projectName As String = String.Empty
            Dim drivetestName As String = String.Empty
            Dim deviceName As String = String.Empty
            Dim deviceFileSavePath As String = Nothing

            If (btnImport.Text = "Cancel") Then
                btnImport.Text = "Import"
                'DeleteFTPDir(compName & "/" & driveTestName)
                isCancel = True
                If ftp_FileUpload IsNot Nothing Then
                    ftp_FileUpload.Abort()
                    FTPDirectoryandFilesRemove(currentProjectName, currentDriveTest, currentDeviceName)
                    SetStatusMessage("Process cancel by user.")
                    currentProjectName = Nothing
                    currentDriveTest = Nothing
                    currentDeviceName = Nothing
                End If
                Return
            Else
                btnImport.Text = "Cancel"
                isCancel = False
            End If

            If (rdoProjectNew.Checked) Then
                projectName = txtProjectNew.Text.Trim()
            Else
                If (gvProject.GetSelectedRows().Count > 0) Then
                    Dim dr As DataRow = TryCast(gvProject.GetRow(gvProject.GetSelectedRows()(0)), DataRowView).Row
                    projectName = dr.Item(0).ToString
                End If
            End If

            If (String.IsNullOrEmpty(projectName)) Then
                SetStatusMessage("Enter Project Name.")
                Exit Sub
            End If

            If (rdoDriveTestNew.Checked) Then
                drivetestName = txtDrivetestNew.Text.Trim()
            Else
                If (gvDrivetest.GetSelectedRows().Count > 0) Then
                    Dim dr As DataRow = TryCast(gvDrivetest.GetRow(gvDrivetest.GetSelectedRows()(0)), DataRowView).Row
                    drivetestName = dr.Item(0).ToString
                End If
            End If

            If (String.IsNullOrEmpty(drivetestName)) Then
                SetStatusMessage("Enter Drivetest Name.")
                Exit Sub
            End If

            If (gvDevice.GetSelectedRows().Count > 0) AndAlso rdoDriveTestExist.Checked = True Then
                Dim dr1 As DataRow = TryCast(gvDevice.GetRow(gvDevice.GetSelectedRows()(0)), DataRowView).Row
                deviceName = dr1.Item(1).ToString
            End If

            If (rdoDeviceNew.Checked) Then
                If Not (String.IsNullOrEmpty(txtDeviceNew.Text)) Then
                    deviceName = txtDeviceNew.Text.Trim()
                End If
            End If

            If (String.IsNullOrEmpty(deviceName)) Then
                SetStatusMessage("Enter Device Name.")
                Exit Sub
            End If

            currentProjectName = projectName
            currentDriveTest = drivetestName
            currentDeviceName = deviceName

            If Not (gvFiles.RowCount > 0) Then
                SetStatusMessage("No any file selected.")
                Exit Sub
            End If

            Dim IsPathExist As Boolean = False
            'Dim IsProjectPathExist As Boolean = False
            'Dim IsDrivetestPathExist As Boolean = False
            'Dim IsDevicePathExist As Boolean = False

            If (projectName IsNot Nothing AndAlso drivetestName IsNot Nothing AndAlso deviceName IsNot Nothing) Then
                ' If (IsImportFile) Then
                'IsCompaignPathExist = IsFTPDirectoryPathExist(compName)
                'IsDriveTestPathExist = IsFTPDirectoryPathExist(driveTestName)
                'If (IsCompaignPathExist AndAlso IsDriveTestPathExist) Then
                '    IsPathExist = True
                'End If
                IsPathExist = IsFTPDirectoryPathExist(projectName & "/" & drivetestName & "/" & deviceName)
                'Else
                'IsPathExist = IsFilePathExist(compName, driveTestName)
                'If (Not IsPathExist) Then
                '    CreateFilePath(compName, driveTestName)
                '    IsPathExist = True
                '    ' End If
                'End If
            End If

            'If (Not IsPathExist) Then
            '    If (IsImportFile) Then
            '        driveTestFileSavePath = String.Format("ftp://{0}/{1}/{2}", ImportFileHost, compName, driveTestName)
            '    Else
            '        driveTestFileSavePath = CreateFilePath(compName, driveTestName)
            '    End If
            'End If
            If (IsPathExist) Then
                If (IsImportFile) Then
                    deviceFileSavePath = String.Format("ftp://{0}/{1}/{2}/{3}", ImportFileHost, projectName, drivetestName, deviceName)
                Else
                    deviceFileSavePath = DriveTestFilesPath & "\" & projectName & "\" & drivetestName & "\" & deviceName & "\"
                    'CreateFilePath(compName, driveTestName)
                End If

                Dim successFiles As String = ""
                Dim successFilesCounter As Integer = 0
                Dim failFiles As String = ""
                Dim failFilesCounter As Integer = 0

                Try
                    If (deviceFileSavePath IsNot Nothing) Then
                        rowAppearance.Clear()
                        For rowIndex As Integer = 0 To gvFiles.RowCount - 1
                            Dim dr As DataRow = TryCast(gvFiles.GetRow(rowIndex), DataRowView).Row
                            Try
                                If (IsImportFile) Then
                                    isUploading = True
                                    UploadFTPFile(dr.Item(1).ToString, deviceFileSavePath)
                                Else
                                    My.Computer.FileSystem.CopyFile(dr.Item(1).ToString, deviceFileSavePath & "\" & dr.Item(0).ToString, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs)
                                End If
                                If (Not isCancel) Then
                                    successFiles = successFiles & Environment.NewLine & dr.Item(0).ToString
                                    successFilesCounter += 1
                                    rowAppearance.Add(New KeyValuePair(Of Integer, Color)(rowIndex, Color.LimeGreen))
                                Else
                                    successFiles = String.Empty
                                    successFilesCounter = 0
                                    Return
                                End If
                            Catch ex As Exception
                                failFiles = failFiles & Environment.NewLine & dr.Item(0).ToString
                                failFilesCounter += 1
                                rowAppearance.Add(New KeyValuePair(Of Integer, Color)(rowIndex, Color.OrangeRed))
                            Finally
                            End Try
                        Next
                        gvFiles.LayoutChanged()
                    End If
                    gcFiles.Refresh()
                    gcFiles.SuspendLayout()

                    StartParser()
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                Finally
                    XtraMessageBox.Show("Success Files :" & successFilesCounter & successFiles & Environment.NewLine & "Fail Files :" & failFilesCounter & failFiles, "DT Import Files", MessageBoxButtons.OK)
                    tsProgressBar.Value = 0
                    'TimerFileUpload.Stop()
                    'TimerFileUpload.Enabled = False
                    tsStatuslblFileName.Text = "File :"
                    tsStatuslblFileSize.Text = "File :"

                    If (isCancel) Then
                        XtraMessageBox.Show("Request cancel by user.", "DT Import Files", MessageBoxButtons.OK)
                    End If
                    btnImport.Text = "Import"
                End Try
            Else
                SetStatusMessage("Save file Path already exist.")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvFiles_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gvFiles.RowStyle
        Try
            If rowAppearance.Count > 0 Then
                If rowAppearance.Exists(Function(x) x.Key = e.RowHandle) Then
                    e.Appearance.BackColor = rowAppearance.Find(Function(x) x.Key = e.RowHandle).Value
                    e.Appearance.BackColor2 = Color.White
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TimerFileUpload_Tick(sender As Object, e As EventArgs) 'Handles TimerFileUpload.Tick
        'If isUploading Then
        '    tsStatuslblFileSize.Text = SummaryText
        'Else
        '    tsStatuslblFileSize.Text = SummaryText
        '    TimerFileUpload.Enabled = False
        '    TimerFileUpload.Stop()
        'End If
    End Sub

    Private Function CreateFilePath(ByVal compName As String, ByVal driveTest As String) As String
        Dim dT_DestinationPath As String = DriveTestFilesPath
        If Not Directory.Exists(dT_DestinationPath) Then
            Directory.CreateDirectory(dT_DestinationPath)
        End If
        'Create Compagin Folder
        If Not Directory.Exists(dT_DestinationPath & "\" & compName) Then
            Directory.CreateDirectory(dT_DestinationPath & "\" & compName)
        End If
        'Create DriveTest Folder
        If Not Directory.Exists(dT_DestinationPath & "\" & compName & "\" & driveTest) Then
            Directory.CreateDirectory(dT_DestinationPath & "\" & compName & "\" & driveTest)
        End If
        Return dT_DestinationPath & "\" & compName & "\" & driveTest & "\"
    End Function

    Private Function IsFilePathExist(ByVal compName As String, ByVal driveTest As String) As Boolean
        Dim destinationPath As String = DriveTestFilesPath
        If (Directory.Exists(destinationPath) AndAlso Directory.Exists(destinationPath & compName) AndAlso Directory.Exists(destinationPath & compName & "\" & driveTest)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsFTPDirectoryPathExist(ByVal directoryName As String) As Boolean
        Dim IsPathExist As Boolean = True
        Try
            Dim subDirs() As String = directoryName.Split("/")
            Dim currentDir As String = String.Format("ftp://{0}", ImportFileHost)
            For Each subDir As String In subDirs
                Try
                    currentDir = currentDir + "/" + subDir
                    IsPathExist = FTPDirectoryProcess(currentDir)
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    IsPathExist = False
                End Try
            Next
        Catch ex As WebException
            'IsExists = False
        End Try
        Return IsPathExist
    End Function

    Public Function FTPDirectoryProcess(ByVal directoryName As String) As Boolean
        Dim IsExists As Boolean = True
        Dim request As FtpWebRequest = Nothing
        Dim response As FtpWebResponse
        Try
            request = TryCast(FtpWebRequest.Create(directoryName), FtpWebRequest)
            request.Timeout = 5000
            request.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            response = TryCast(request.GetResponse(), FtpWebResponse)
            request.Abort()
            response.Close()
        Catch ex As WebException
            IsExists = False
            request.Abort()
            'XtraMessageBox.Show("Please check FTP Settings", "Import DriveTest", MessageBoxButtons.OK)
            'response.Close()
        End Try

        If (Not IsExists) Then
            Dim destinationPath As String = Nothing     'IOSConigManager.ImportFile.Host
            Dim reqFTP As FtpWebRequest = Nothing
            Dim ftpStream As Stream = Nothing

            Dim currentDir As String = directoryName    'String.Format("ftp://{0}/{1}", ImportFileHost, directoryName)
            Try
                reqFTP = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory
                reqFTP.UseBinary = True
                reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
                Dim response2 As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
                ftpStream = response2.GetResponseStream()
                ftpStream.Close()
                response2.Close()
                IsExists = True
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                IsExists = False
            End Try
        End If
        Return IsExists
    End Function

    Public Function MakeFTPDir(ByVal directoryName As String) As String
        Dim destinationPath As String = Nothing     'IOSConigManager.ImportFile.Host
        Dim reqFTP As FtpWebRequest = Nothing
        Dim ftpStream As Stream = Nothing
        Dim subDirs() As String = directoryName.Split("/")

        Dim currentDir As String = String.Format("ftp://{0}", ImportFileHost)
        For Each subDir As String In subDirs
            Try
                currentDir = currentDir + "/" + subDir
                reqFTP = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory
                reqFTP.UseBinary = True
                reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
                Dim response As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
                ftpStream = response.GetResponseStream()
                ftpStream.Close()
                response.Close()
                destinationPath = currentDir & "/"
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                ''directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
            End Try
        Next
        Return destinationPath
    End Function

    Public Function FTPDirectoryandFilesRemove(ByVal projectName As String, ByVal drivetestName As String, ByVal deviceName As String) As String
        Dim destinationPath As String = Nothing
        Dim reqFTP As FtpWebRequest = Nothing
        Dim ftpStream As Stream = Nothing
        Dim currentDir As String = String.Format("ftp://{0}", ImportFileHost)
        Try
            currentDir = currentDir & "/" & projectName & "/" & drivetestName
            Dim existDirectoryList As List(Of String) = GetFTPDirectoryList(currentDir)
            If (existDirectoryList.Count > 1) Then
                currentDir = currentDir & "/" & deviceName
                Dim existFilesList As List(Of String) = GetFTPFilesList(currentDir)
                If (existFilesList.Count > 0) Then
                    For Each existfileName As String In existFilesList
                        FTPFilesRemove(currentDir & "/" & existfileName)
                    Next
                End If
            Else
                currentDir = currentDir
            End If
            reqFTP = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
            Dim response As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
            ftpStream = response.GetResponseStream()
            ftpStream.Close()
            response.Close()
            destinationPath = currentDir & "/"
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return destinationPath
    End Function

    Public Function FTPDirectoryRemove(ByVal directoryName As String) As Boolean
        Dim reqFTP As FtpWebRequest = Nothing
        Dim ftpStream As Stream = Nothing
        Dim currentDir As String = String.Format("ftp://{0}/{1}", ImportFileHost, directoryName)
        Try
            reqFTP = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
            Dim response As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
            ftpStream = response.GetResponseStream()
            ftpStream.Close()
            response.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Public Function FTPFilesRemove(ByVal filePath As String) As Boolean
        Dim ftpStream As Stream = Nothing
        Dim currentDir As String = filePath     'String.Format("ftp://{0}/{1}", ImportFileHost, filePath)
        Dim isFileDeleted As Boolean = False
        Try
            Dim reqFTP As FtpWebRequest = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
            reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile
            reqFTP.UseBinary = True
            Dim response As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
            ftpStream = response.GetResponseStream()
            isFileDeleted = True
            ftpStream.Close()
            response.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            isFileDeleted = False
        End Try
        Return isFileDeleted
    End Function

    Public Function DeleteFTPDir(ByVal directoryName As String) As String
        Dim destinationPath As String = Nothing
        Dim reqFTP As FtpWebRequest = Nothing
        Dim ftpStream As Stream = Nothing
        Dim subDirs() As String = directoryName.Split("/")

        Dim currentDir As String = String.Format("ftp://{0}", ImportFileHost)
        'Array.Reverse(subDirs)
        For Each subDir As String In subDirs
            Try
                currentDir = currentDir + "/" + subDir
                Dim existDirectoryList As List(Of String) = GetFTPDirectoryList(currentDir)

                'Array.Reverse(subDirs)
                reqFTP = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory
                reqFTP.UseBinary = True
                reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
                Dim response As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
                ftpStream = response.GetResponseStream()
                ftpStream.Close()
                response.Close()
                destinationPath = currentDir & "/"
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                ''directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
            End Try
        Next
        Return destinationPath '& "/" ' & directoryName & "/"
    End Function

    Public Function GetFTPDirectoryList(ByVal directoryName As String) As List(Of String)
        Dim destinationPath As String = Nothing
        Dim reqFTP As FtpWebRequest = Nothing
        Dim ftpStream As StreamReader = Nothing
        Dim currentDir As String = directoryName
        Dim dirList As List(Of String) = New List(Of String)()
        Try
            reqFTP = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
            Dim response As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
            ftpStream = New StreamReader(response.GetResponseStream())
            Dim line As String = ftpStream.ReadLine()
            While line IsNot Nothing
                dirList.Add(line)
                line = ftpStream.ReadLine()
            End While
            ftpStream.Close()
            response.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return dirList
    End Function

    Public Function GetFTPFilesList(ByVal directoryName As String) As List(Of String)
        Dim destinationPath As String = Nothing
        Dim reqFTP As FtpWebRequest = Nothing
        Dim ftpStream As StreamReader = Nothing
        Dim currentDir As String = directoryName
        Dim dirList As List(Of String) = New List(Of String)()
        Try
            reqFTP = TryCast(FtpWebRequest.Create(currentDir), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword)
            Dim response As FtpWebResponse = TryCast(reqFTP.GetResponse(), FtpWebResponse)
            ftpStream = New StreamReader(response.GetResponseStream())
            Dim line As String = ftpStream.ReadLine()
            While line IsNot Nothing
                Dim fileName As String = line.Split("/")(line.Split("/").Count - 1)
                If (fileName.Contains(".")) Then
                    dirList.Add(fileName)
                End If
                line = ftpStream.ReadLine()
            End While
            ftpStream.Close()
            response.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return dirList
    End Function

    Private Sub UploadFTPFile(filename As String, ByVal url As String)
        tsProgressBar.Value = 0
        Dim ftpServerIP As String = ImportFileHost
        Dim fileInf As New FileInfo(filename)
        Dim uri As String = (url) & "/" & fileInf.Name '' ("ftp://" & ftpServerIP & "/" & compName & "/" & driveTest & "/") + fileInf.Name
        ftp_FileUpload = DirectCast(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest) ' Create FtpWebRequest object from the Uri provided
        ftp_FileUpload.Credentials = New NetworkCredential(ImportFileHostUser, ImportFileHostPassword) ' Provide the WebPermission Credintials
        ftp_FileUpload.KeepAlive = False ' By default KeepAlive is true, where the control connection is not closed after a command is executed.
        ftp_FileUpload.Method = WebRequestMethods.Ftp.UploadFile ' Specify the command to be executed.
        ftp_FileUpload.UseBinary = True  ' Specify the data transfer type.
        ftp_FileUpload.ContentLength = fileInf.Length ' Notify the server about the size of the uploaded file

        Dim buffLength As Integer = 2048  ' The buffer size is set to 2kb
        Dim buff As Byte() = New Byte(buffLength - 1) {}
        Dim contentLen As Integer
        Dim fs As FileStream = fileInf.OpenRead() ' Opens a file stream (System.IO.FileStream) to read  the file to be uploaded
        Dim strm As Stream = Nothing
        Try
            strm = ftp_FileUpload.GetRequestStream() ' Stream to which the file to be upload is written
            contentLen = fs.Read(buff, 0, buffLength) ' Read from the file stream 2kb at a time
            Dim SentBytes As Long = 0
            Dim FileSize As Long = ftp_FileUpload.ContentLength
            Dim FileSizeDescription As String = GetFileSize(FileSize) ' e.g. "2.4 Gb" instead of 240000000000000 bytes etc...

            While contentLen <> 0 ' Till Stream content ends
                strm.Write(buff, 0, contentLen) ' Write Content from the file stream to the FTP Upload Stream
                contentLen = fs.Read(buff, 0, buffLength)
                SentBytes += contentLen
                tsStatuslblFileSize.Text = String.Format(" {0} / {1} ", GetFileSize(SentBytes), FileSizeDescription)
                tsProgressBar.Value = ((SentBytes * 100) / FileSize)
                tsStatuslblFileName.Text = "File Name: " & fileInf.Name
                Application.DoEvents()
            End While
            strm.Close() ' Close the file stream and the Request Stream
            fs.Close()
            isUploading = False
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            strm.Close()
            fs.Close()
        End Try
    End Sub

    Public Shared Function GetFileSize(numBytes As Long) As String
        Dim fileSize As String = ""
        If numBytes > 1073741824 Then
            fileSize = [String].Format("{0:0.00} Gb", CDbl(numBytes) / 1073741824)
        ElseIf numBytes > 1048576 Then
            fileSize = [String].Format("{0:0.00} Mb", CDbl(numBytes) / 1048576)
        Else
            fileSize = [String].Format("{0:0} Kb", CDbl(numBytes) / 1024)
        End If
        If fileSize = "0 Kb" Then
            fileSize = "1 Kb"
        End If
        Return fileSize
    End Function

    Private Sub StartParser()
        Dim ParserAppHostedPath As String = GetConfigClientKeyValue("ParserAppHostedPath")
        Dim dataOwner As String = Environment.UserName
        'Dim info As New System.Diagnostics.ProcessStartInfo
        ''info.FileName = "iexplore.exe"
        'info.Arguments = Replace(ParserAppHostedPath, "dataowner=IOS&", "") & "&dataowner=" & dataOwner
        'info.WindowStyle = ProcessWindowStyle.Minimized
        'Dim process As New System.Diagnostics.Process
        'process.StartInfo = info

        If objDTParserWebClient Is Nothing Then
            objDTParserWebClient = New frmInternetExplorer()
        End If
        objDTParserWebClient.NavigationUrl = Replace(ParserAppHostedPath, "dataowner=IOS&", "") & "&dataowner=" & dataOwner
        objDTParserWebClient.WebRequestFrom = "DTParserRequest"
        frmMDI.OpenFormAsDockPanel("DT Parser Web Client",, objDTParserWebClient)
        Try
            'process.Start()
            'process.Close()
        Catch ex As Exception
            'process.Close()
            'Dim info2 As New System.Diagnostics.ProcessStartInfo
            'info2.FileName = "chrome.exe"
            'info2.Arguments = ParserAppHostedPath & "&dataowner=" & dataOwner
            'Dim process2 As New System.Diagnostics.Process
            'process2.StartInfo = info2
            'process2.Start()
            'process2.Close()
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim fdDriveTest As New OpenFileDialog
            'fd.InitialDirectory = GetUserDataPath() & "\Data\"
            fdDriveTest.DefaultExt = "nmf"
            fdDriveTest.Filter = "Drive Test (*.nmf)|*.nmf|All files (*.*)|*.*"
            fdDriveTest.Title = "Open the Drive Test File"
            fdDriveTest.Multiselect = True

            If fdDriveTest.ShowDialog = DialogResult.OK Then
                For Each selectedFile As String In fdDriveTest.FileNames
                    BindFilesDataTable(selectedFile)
                Next
            End If
            If (dtFileList.Rows.Count > 0) Then
                Library.IOSDevExpressGrid.PopulateDataInGrid(gcFiles, gvFiles, dtFileList, "ALL", , dtFileList.Columns(0).ColumnName)
                'IOS.Library.IOSDevExpressGrid.RefreshingGrid(gvFiles, True)
                'gvFiles.Columns(0).Width = gcFiles.Width - 65
                gvFiles.Columns(1).Visible = False
                gvFiles.Columns(2).Width = 60
                gcFiles.ResumeLayout()
                gcFiles.Refresh()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFormat.SelectedIndexChanged
        If (cmbFormat.SelectedIndex = 0) Then
            btnBrowse.Enabled = False
        Else
            btnBrowse.Enabled = True
        End If
    End Sub

    Private Sub BindFilesDataTable(ByVal fileName As String)
        Dim ext() As String = fileName.Substring(fileName.LastIndexOf("\") + 1).Split(".")
        If (ext(ext.Length - 1).ToUpper = "NMF") Then
            Dim drFile As DataRow = dtFileList.NewRow
            Dim filestream As FileStream = New FileStream(fileName, FileMode.Open)
            drFile(0) = fileName.Substring(fileName.LastIndexOf("\") + 1)
            drFile(1) = fileName
            drFile(2) = Math.Round(((filestream.Length / 1024) / 1024), 1).ToString.GetDecimalString & " MB"
            Dim duplicate() As DataRow = dtFileList.Select("FileName='" & drFile(0) & "'")
            If Not (duplicate.Length > 0) Then
                dtFileList.Rows.Add(drFile)
            End If
            filestream.Close()
        End If
    End Sub

#End Region

#Region "Files Drag & Drop Events"

    Private Sub gcFiles_DragOver(sender As Object, e As DragEventArgs) Handles gcFiles.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub gcFiles_DragDrop(sender As Object, e As DragEventArgs) Handles gcFiles.DragDrop
        Try
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If (files.Length > 0) Then
                For Each selectedFile As String In files
                    Dim fileExt() As String = selectedFile.Substring(selectedFile.LastIndexOf("\") + 1).Split(".")
                    If (fileExt.Length > 1) Then
                        BindFilesDataTable(selectedFile)
                    End If
                Next
                If (dtFileList.Rows.Count > 0) Then
                    Library.IOSDevExpressGrid.PopulateDataInGrid(gcFiles, gvFiles, dtFileList, "ALL", , dtFileList.Columns(0).ColumnName)
                    'gvFiles.Columns(0).Width = gcFiles.Width - 65
                    gvFiles.Columns(1).Visible = False
                    gvFiles.Columns(2).Width = 60
                    gcFiles.ResumeLayout()
                    gcFiles.Refresh()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

End Class