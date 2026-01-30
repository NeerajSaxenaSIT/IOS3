Imports System.IO
Imports IOS.DataLibrary
Imports DevExpress.XtraReports.UI
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraReports.UserDesigner
Imports DevExpress.DataAccess.Wizard.Services
Imports System.ComponentModel.Design
Imports DevExpress.DataAccess.Wizard.Model
Imports DevExpress.DataAccess.Wizard.Native
Imports DevExpress.DataAccess.Native

Public Class frmReportDesigner

    Public reportID As Integer = Nothing
    Public reportName As String = Nothing
    Dim rpt As XtraReport = Nothing

    Private Sub frmReportDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AddHandler reportDesigner.DesignPanelLoaded, AddressOf reportDesigner_DesignPanelLoaded

            rpt = New XtraReport()
            Dim str = dtCrystalReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ReportID") = reportID)(0)("ReportFile").ToString
            str = str.Replace("''", "'")
            Dim ms As New MemoryStream()
            ms = StringToStream(str)

            If ms.Length <> 0 Then
                rpt.LoadLayoutFromXml(ms, False)
            End If

            rpt.Name = reportName
            rpt.DataSource = GetReportSqlDataSource(rpt)

            reportDesigner.OpenReport(rpt)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function GetReportSqlDataSource(ByRef rpt As XtraReport) As SqlDataSource
        Dim sqlDS As SqlDataSource = Nothing
        Dim connArr() As String = GetIOSConnection(2000)
        connStrCrystalReport = GetDecryptedConnectionString(connArr(1))
        Dim connectionString As String = connStrCrystalReport
        sqlDS = New SqlDataSource()
        sqlDS = TryCast(rpt.DataSource, SqlDataSource)
        sqlDS.ConnectionParameters = New MsSqlConnectionParameters(connStrCrystalReport.Split(";")(0).Split("=")(1), connStrCrystalReport.Split(";")(1).Split("=")(1), connStrCrystalReport.Split(";")(2).Split("=")(1), connStrCrystalReport.Split(";")(3).Split("=")(1), MsSqlAuthorizationType.SqlServer)
        sqlDS.ConnectionName = connArr(0)

        sqlDS.Connection.Open()
        Return sqlDS
    End Function

    Private Sub ReportConfigureDataConnection(sender As Object, e As ConfigureDataConnectionEventArgs)
        Dim connArr() As String = GetIOSConnection(2000)
        connStrCrystalReport = GetDecryptedConnectionString(connArr(1))
        Dim connectionString As String = connStrCrystalReport
        e.ConnectionParameters = New MsSqlConnectionParameters()
    End Sub

    Private Sub reportDesigner_DesignPanelLoaded(sender As Object, e As DesignerLoadedEventArgs)
        Dim panel As XRDesignPanel = DirectCast(sender, XRDesignPanel)
        panel.AddCommandHandler(New SaveCommandHandler(panel))
    End Sub

    Private Sub ReplaceService(ByVal container As IServiceContainer, ByVal serviceType As Type, ByVal serviceInstance As Object)
        If container.GetService(serviceType) IsNot Nothing Then
            container.RemoveService(serviceType)
        End If
        container.AddService(serviceType, serviceInstance)
    End Sub

    Private Sub bbtnSaveReport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbtnSaveReport.ItemClick
        Try
            Dim pnl As XRDesignPanel = reportDesigner.ActiveDesignPanel
            pnl.ReportState = ReportState.Saved

            Dim rpt As XtraReport = reportDesigner.ActiveDesignPanel.Report
            Dim mstrm As MemoryStream = New MemoryStream()
            rpt.SaveLayoutToXml(mstrm)
            mstrm.Position = 0

            Dim strFile As String = Nothing
            Using sr As New StreamReader(mstrm)
                strFile = sr.ReadToEnd()
            End Using
            strFile = strFile.Replace("'", "''")
            Dim connStr As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ReportFile", Chr(39) & strFile & Chr(39)},
                New String() {"@ReportID", Me.reportID}
            }
            connStr = GetSQL(7037, parray)(0)
            sqlParam = GetSQL(7037, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(connStr, sqlParam)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub frmReportDesigner_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        Try
            Dim sqlDS = TryCast(rpt.DataSource, SqlDataSource)
            If sqlDS IsNot Nothing Then
                If sqlDS.Connection.IsConnected Then
                    sqlDS.Connection.Close()
                    sqlDS = Nothing
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Class SaveCommandHandler
        Implements ICommandHandler

        Private panel As XRDesignPanel

        Public Sub New(ByVal panel As XRDesignPanel)
            Me.panel = panel
        End Sub

        Public Sub HandleCommand(ByVal command As ReportCommand, ByVal args() As Object) Implements ICommandHandler.HandleCommand
            Save()
        End Sub

        Public Function CanHandleCommand(ByVal command As ReportCommand, ByRef useNextHandler As Boolean) As Boolean Implements ICommandHandler.CanHandleCommand
            useNextHandler = Not (command = ReportCommand.SaveFile OrElse command = ReportCommand.SaveFileAs OrElse command = ReportCommand.Closing)
            Return Not useNextHandler
        End Function

        Private Sub Save()
            panel.ReportState = ReportState.Saved
        End Sub
    End Class

End Class