Public Class frmTicketDetail

#Region "Form Load"

    Private Sub frmTicketDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BringToFront()
        Me.Show()
    End Sub

#End Region

#Region "Helper"

    Public Sub FetchTicket(ByVal ticketnumber As String, Optional ByVal link As String = "")
        If link = "" Then
            If System.Configuration.ConfigurationManager.AppSettings("DeploymentName").ToString = "ODIDO" Then
                WebBrowser_Ticket.Navigate("http://pwimrep/sitelogview/tt_info.php?id=" & ticketnumber)
            ElseIf System.Configuration.ConfigurationManager.AppSettings("DeploymentName").ToString = "TMUS" Then
                WebBrowser_Ticket.Navigate("http://natweb.eng.t-mobile.com/sites/Reporting/Reports/Homer/TTDetailReport.aspx?lsSttid=" & ticketnumber)
            ElseIf System.Configuration.ConfigurationManager.AppSettings("DeploymentName").ToString = "TDC" Then
                'TODO
            End If
        Else
            WebBrowser_Ticket.Navigate(link)
        End If
    End Sub

    Public Sub FetchTicketData(ByVal ticketDetailUrl As String)
        WebBrowser_Ticket.Navigate(ticketDetailUrl)
    End Sub

#End Region

End Class