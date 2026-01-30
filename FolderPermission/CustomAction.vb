Imports System.Security

Public Class CustomActions

    <CustomAction()> _
    Public Shared Function SetFolderPermission(ses As Session) As ActionResult
        Try
            Dim folder As String = ses.GetTargetPath("INSTALLFOLDER")
            Dim sid As Security.Principal.SecurityIdentifier = New Security.Principal.SecurityIdentifier(Security.Principal.WellKnownSidType.AuthenticatedUserSid, Nothing)
            Dim writerule As Security.AccessControl.FileSystemAccessRule = New Security.AccessControl.FileSystemAccessRule(sid, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow)

            If Not String.IsNullOrEmpty(folder) And IO.Directory.Exists(folder) Then
                Dim fSecurity As AccessControl.DirectorySecurity = IO.Directory.GetAccessControl(folder)
                fSecurity.AddAccessRule(writerule)
                IO.Directory.SetAccessControl(folder, fSecurity)
            End If
            Return ActionResult.Success
        Catch ex As Exception
            Return ActionResult.Success
        End Try
    End Function

End Class
