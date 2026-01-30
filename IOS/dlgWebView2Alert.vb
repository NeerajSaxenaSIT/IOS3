Public Class dlgWebView2Alert

    Private Sub hlcOpenWebLink_HyperlinkClick(sender As Object, e As DevExpress.Utils.HyperlinkClickEventArgs) Handles hlcOpenWebLink.HyperlinkClick
        hlcOpenWebLink.LinkVisited = True
        webview2Alert = True
        Process.Start(e.Link)
        Me.Close()
    End Sub
End Class