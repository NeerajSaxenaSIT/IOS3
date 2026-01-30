Imports System.IO

Public Class clsLoggerManager
    Private logger As log4net.ILog

    Sub New()
        'Dim DeploymentName As String = IOS.Configuration.IOSAppConfigManage.DeploymentName
        'Dim basePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        'Dim dataPath As String = String.Format("{0}\{1}\{2}\{3}", basePath, Application.CompanyName, Application.ProductName, DeploymentName)
        'If Not Directory.Exists(dataPath) Then
        '    Directory.CreateDirectory(dataPath)
        'End If

        'log4net.GlobalContext.Properties("LogFileName") = dataPath & "\IOSv2.log"
        'log4net.Config.XmlConfigurator.Configure()
        log4net.Config.BasicConfigurator.Configure()
        logger = log4net.LogManager.GetLogger("IOS")
    End Sub

    Public Sub SetLogInfo(ByVal methodName As String, ByVal message As String)
        logger.Info("Info : " & methodName & " - " & message)
    End Sub

    Public Sub SetError(ByVal errorMessage As String)
        logger.Error("Error : " & errorMessage)
    End Sub

    Public Sub SetInfo(ByVal errorMessage As String)
        logger.Info("Info : " & errorMessage)
    End Sub

    Public Sub SetDebug(ByVal errorMessage As String)
        logger.Debug("Debug : " & errorMessage)
    End Sub

End Class

