Public Class IOSDevExTab

    Public Shared Sub CreateTab(ByVal tabTitle As String, ByVal tabName As String, ByVal tabTag As String, ByRef tabControl As DevExpress.XtraTab.XtraTabControl)
        Try
            Dim vTabPageNew As DevExpress.XtraTab.XtraTabPage = New DevExpress.XtraTab.XtraTabPage()
            vTabPageNew.Name = tabName
            vTabPageNew.Text = tabTitle
            vTabPageNew.Tag = tabTag
            vTabPageNew.Tooltip = tabTitle
            vTabPageNew.Dock = System.Windows.Forms.DockStyle.Fill
            Dim IsTabAlready As Boolean = False
            Dim IsNewTab As Boolean = False

            Dim ExistingTab As DevExpress.XtraTab.XtraTabPage = Nothing
            If (tabControl.TabPages.Count > 0) Then
                For Each pageTab As DevExpress.XtraTab.XtraTabPage In tabControl.TabPages

                    If (pageTab.Text.ToUpper = tabTitle.ToUpper) Then
                        IsTabAlready = True
                        ExistingTab = pageTab
                        Exit For
                    End If
                Next
            End If

            If Not (IsTabAlready) Then
                tabControl.TabPages.Add(vTabPageNew)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function CreateTab(ByVal tabName As String, ByVal tabTitle As String, ByVal tabTag As String) As DevExpress.XtraTab.XtraTabPage
        Dim vTabPageObj As DevExpress.XtraTab.XtraTabPage = New DevExpress.XtraTab.XtraTabPage()
        Try
            vTabPageObj.Name = tabName
            vTabPageObj.Text = tabTitle
            vTabPageObj.Tag = tabTag
            vTabPageObj.Tooltip = tabTitle
            vTabPageObj.ShowCloseButton = True
            vTabPageObj.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
            vTabPageObj.Dock = System.Windows.Forms.DockStyle.Fill
        Catch ex As Exception

        End Try
        Return vTabPageObj
    End Function

    Public Shared Sub RemoveTab(ByVal tabTag As String, ByRef vtabControl As DevExpress.XtraTab.XtraTabControl)
        Try
            Dim deletableTabPage As List(Of DevExpress.XtraTab.XtraTabPage) = New List(Of DevExpress.XtraTab.XtraTabPage)
            If (vtabControl.TabPages.Count > 2) Then
                For Each pageTab As DevExpress.XtraTab.XtraTabPage In vtabControl.TabPages
                    If (pageTab.Tag.ToString = tabTag) Then
                        deletableTabPage.Add(pageTab)
                    End If
                Next
            End If
            If (deletableTabPage.Count > 0) Then
                For Each pageTab As DevExpress.XtraTab.XtraTabPage In deletableTabPage
                    vtabControl.TabPages.Remove(pageTab)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
