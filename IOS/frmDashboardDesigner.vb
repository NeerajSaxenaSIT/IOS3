Imports System.IO
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DashboardWin
Imports IOS.DataLibrary
Imports DevExpress.XtraPrinting
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.DataFederation
Imports DevExpress.DataAccess

Partial Public Class frmDashboardDesigner

    Public dashboardID As Integer = Nothing
    Public dashboardName As String = Nothing

    Private dashboardRpt As Dashboard = Nothing

    'Private ticketID As String = Nothing
    'Private cellName As String = Nothing
    'Private tech As String = Nothing

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmDashboardDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dashboardXmlFile As String = Nothing
            Dim str = dtDashboardReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("DashboardID") = dashboardID)(0)("DashboardFile").ToString
            str = str.Replace("''", "'")

            If str.Trim.Contains("<?xml") Then
                dashboardXmlFile = str
            Else
                dashboardXmlFile = GetDecryptedConnectionString(str)
            End If

            Dim ms As New MemoryStream()
            ms = StringToStream(dashboardXmlFile)

            If ms.Length <> 0 Then
                DashDesigner.Dashboard.LoadFromXml(ms)
            End If

            'DashDesigner.Dashboard.LayoutOptions.Width.Mode = LayoutDimensionMode.Auto
            'DashDesigner.Dashboard.LayoutOptions.Height.Mode = LayoutDimensionMode.Auto

            'DashDesigner.Dashboard = dashboardRpt
            'dashboardRpt = DashDesigner.Dashboard
            'AddHandler dashboardRpt.DataSourceCollectionChanged, AddressOf dashboardRpt_DataSourceCollectionChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub DashDesigner_CustomParameters(ByVal sender As Object, ByVal e As CustomParametersEventArgs) Handles DashDesigner.CustomParameters
        Try
            Dim grid As GridDashboardItem = CType(DashDesigner.Dashboard.Items(0), GridDashboardItem)
            Dim customParameter = e.Parameters.FirstOrDefault(Function(p) p.Name = "SQL_ID")
            If customParameter IsNot Nothing Then
                grid.FilterString = "SQL_ID in (?" & customParameter.Name & ")"
            Else
                grid.FilterString = Nothing
            End If
        Catch
        End Try
    End Sub

    'Private Sub dashboardRpt_DataSourceCollectionChanged(sender As Object, e As NotifyingCollectionChangedEventArgs(Of IDashboardDataSource))
    '    Try
    '        For Each src As DashboardSqlDataSource In e.AddedItems
    '            DevExpress.XtraEditors.XtraMessageBox.Show(TryCast(src.ConnectionParameters, MsSqlConnectionParameters).DatabaseName)
    '            DevExpress.XtraEditors.XtraMessageBox.Show(TryCast(src.ConnectionParameters, MsSqlConnectionParameters).ServerName)
    '            DevExpress.XtraEditors.XtraMessageBox.Show(TryCast(src.ConnectionParameters, MsSqlConnectionParameters).UserName)
    '            DevExpress.XtraEditors.XtraMessageBox.Show(TryCast(src.ConnectionParameters, MsSqlConnectionParameters).Password)
    '            DevExpress.XtraEditors.XtraMessageBox.Show(TryCast(src.ConnectionParameters, MsSqlConnectionParameters).AuthorizationType)
    '        Next
    '    Catch ex As Exception
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '    End Try
    'End Sub

    'Private Sub DashDesigner_Load(sender As Object, e As EventArgs) Handles DashDesigner.Load
    '    Dim panel As DashboardDesigner = DirectCast(sender, DashboardDesigner)
    '    panel.AddCommandHandler(New SaveCommandHandler(panel))
    'End Sub

    Private Sub DashDesigner_DashboardItemClick(sender As Object, e As DashboardItemMouseActionEventArgs) Handles DashDesigner.DashboardItemClick
        'MessageBox.Show(e.DashboardItemName & " clicked")

        'For Each axis As String In e.Data.GetAxisNames()
        '    Dim axisPoint As AxisPoint = e.GetAxisPoint(axis)
        '    If axisPoint Is Nothing Then
        '        Continue For
        '    End If

        '    For Each dimension In e.Data.GetDimensions(axis)
        '        Dim dimValue As DimensionValue = axisPoint.GetDimensionValue(dimension)
        '        If dimValue Is Nothing Then
        '            Continue For
        '        End If

        '        If dimension.Name.ToLower = "ticketid" Then
        '            ticketID = dimValue.DisplayText
        '        End If

        '        If dimension.Name.ToLower = "site" Then
        '            cellName = dimValue.DisplayText
        '        End If

        '        If dimension.Name.ToLower = "tech" Then
        '            tech = dimValue.DisplayText
        '        End If

        '    Next dimension
        'Next axis

        'DashboardDesigner1.Dashboard.BeginUpdate()

        'DashboardDesigner1.Parameters(0).SelectedValue = cellName
        'DashboardDesigner1.Parameters(1).SelectedValue = tech

        'DashboardDesigner1.Dashboard.EndUpdate()

    End Sub

    Private Sub GenerateChart()
        'DashboardDesigner1.Dashboard.BeginUpdate()

        'DashboardDesigner1.Dashboard.Parameters.Remove("@Cell")
        'DashboardDesigner1.Dashboard.Parameters.Remove("@Tech")

        'Dim cellNameParam As New DashboardParameter()
        'Dim techParam As New DashboardParameter()

        'DashboardDesigner1.Dashboard.Parameters.Add(cellNameParam)
        'DashboardDesigner1.Dashboard.Parameters.Add(techParam)

        'If cellName IsNot Nothing AndAlso tech IsNot Nothing Then

        '    Dim dataSource As DashboardSqlDataSource = CType(DashboardDesigner1.Dashboard.DataSources(0), DashboardSqlDataSource)
        '    Dim cellHistChartQry As StoredProcQuery = CType(dataSource.Queries(0), StoredProcQuery)

        '    cellHistChartQry.Parameters(0).Value = cellName
        '    cellHistChartQry.Parameters(1).Value = tech

        'cellHistChartQry.Parameters.Add(New QueryParameter("cellName", GetType(System.String), cellName))
        'cellHistChartQry.Parameters.Add(New QueryParameter("tech", GetType(System.String), tech))

        'cellHistChartQry..Sql = "SELECT  CAST(CAST(IA_score.Date_Day as nchar(8)) as datetime) as Date, IA_score.Voice_Access, IA_score.Voice_Drop, IA_score.Data_Access, IA_score.Data_Drop, IA_score.Signaling  FROM IA_score    
        'WHERE ((IA_score.WCEL = @cellName) AND (IA_Score.Tech = @tech))  ORDER BY IA_score.Date_Day"

        '_dashboard.cellHistoryChart = CreateCellHistoryChart(SqlDataSource)
        'DashboardDesigner1.Dashboard.Items("Site Score - History")


        'End If

        'DashboardDesigner1.Dashboard.EndUpdate()
    End Sub

    Private Sub btnSaveDashboard_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSaveDashboard.ItemClick
        Try
            Dim dash As Dashboard = DashDesigner.Dashboard

            Dim mstrm As MemoryStream = New MemoryStream()
            dash.SaveToXml(mstrm)
            mstrm.Position = 0

            Dim strFile As String = Nothing
            Dim encryptedStr As String = Nothing

            Using sr As New StreamReader(mstrm)
                strFile = sr.ReadToEnd()
            End Using

            'strFile = strFile.Replace("'", "''")
            encryptedStr = GetEncryptedString(strFile)

            Dim connStr As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@DashboardFile", Chr(39) & encryptedStr & Chr(39)},
                New String() {"@DashboardID", Me.dashboardID}
            }
            connStr = GetSQL(8103, parray)(0)
            sqlParam = GetSQL(8103, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(connStr, sqlParam)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub RemoveDahsboardDataSourcesParametrs()
        For Each ds As DashboardSqlDataSource In DashDesigner.Dashboard.DataSources
            If ds.Connection.ProviderKey.ToUpper = "POSTGRES" Then
                ds.ConnectionParameters = RemoveConnectionParametersPostGreSql()
            ElseIf ds.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then
                ds.ConnectionParameters = RemoveConnectionParametersSql()
            ElseIf ds.Connection.ProviderKey.ToUpper = "ORACLESERVER" Then
                ds.ConnectionParameters = RemoveConnectionParametersOracle()
            End If
        Next
    End Sub

    Private Function RemoveConnectionParametersSql() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(2000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New MsSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .DatabaseName = connString.Split(";")(1).Split("=")(1),
            .AuthorizationType = MsSqlAuthorizationType.SqlServer,
            .UserName = "",
            .Password = ""
        }
    End Function

    Private Function RemoveConnectionParametersPostGreSql() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(3000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New PostgreSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .PortNumber = connString.Split(";")(1).Split("=")(1),
            .UserName = "",
            .Password = "",
            .DatabaseName = connString.Split(";")(4).Split("=")(1)
        }
    End Function

    Private Function RemoveConnectionParametersOracle() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(4000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New OracleConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .UserName = "",
            .Password = "",
            .ProviderType = OracleProviderType.ODPManaged
        }
    End Function

    Private Sub frmDashboardDesigner_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        Try
            Dim sqlDS = TryCast(dashboardRpt.DataSources, DataSourceCollection)
            If sqlDS IsNot Nothing Then
                sqlDS = Nothing
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub DashDesigner_DashboardSaving(sender As Object, e As DashboardSavingEventArgs) Handles DashDesigner.DashboardSaving
        If e.Command = DashboardSaveCommand.Save OrElse e.Command = DashboardSaveCommand.SaveAs Then
            e.Handled = True
        End If
    End Sub

#Region "Backward Compatibility"

    Private Sub DashDesigner_ConfigureDataConnection(sender As Object, e As DashboardConfigureDataConnectionEventArgs) Handles DashDesigner.ConfigureDataConnection
        Try
            If DashDesigner.Dashboard.DataSources.Count > 1 Then
                'multi data sources of diff kind
                For Each ds As DashboardSqlDataSource In DashDesigner.Dashboard.DataSources
                    ds.ConnectionOptions.CommandTimeout = 300
                    If ds.Connection.ProviderKey.ToUpper = "POSTGRES" Then
                        'ds.ConnectionParameters = CreateConnectionParametersPostGreSql()
                        Dim params As PostgreSqlConnectionParameters = TryCast(e.ConnectionParameters, PostgreSqlConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(3000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.PortNumber = connString.Split(";")(1).Split("=")(1)
                            params.UserName = connString.Split(";")(2).Split("=")(1)
                            params.Password = connString.Split(";")(3).Split("=")(1)
                            params.DatabaseName = connString.Split(";")(4).Split("=")(1)
                        End If
                    ElseIf ds.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then
                        'ds.ConnectionParameters = CreateConnectionParametersSql()
                        Dim params As MsSqlConnectionParameters = TryCast(e.ConnectionParameters, MsSqlConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(2000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.DatabaseName = connString.Split(";")(1).Split("=")(1)
                            params.AuthorizationType = MsSqlAuthorizationType.SqlServer
                            params.UserName = connString.Split(";")(2).Split("=")(1)
                            params.Password = connString.Split(";")(3).Split("=")(1)
                        End If
                    ElseIf ds.Connection.ProviderKey.ToUpper = "ORACLESERVER" Then
                        'ds.ConnectionParameters = CreateConnectionParametersOracle()
                        Dim params As OracleConnectionParameters = TryCast(e.ConnectionParameters, OracleConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(4000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.UserName = connString.Split(";")(1).Split("=")(1)
                            params.Password = connString.Split(";")(2).Split("=")(1)
                            params.ProviderType = OracleProviderType.ODPManaged
                        End If
                    End If
                Next
            Else
                'single data source of a kind
                Dim ds As DashboardSqlDataSource = DashDesigner.Dashboard.DataSources(0)
                If ds.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then
                    e.ConnectionParameters = CreateConnectionParametersSql()
                ElseIf ds.Connection.ProviderKey.ToUpper = "POSTGRES" Then
                    e.ConnectionParameters = CreateConnectionParametersPostGreSql()
                ElseIf ds.Connection.ProviderKey.ToUpper = "ORACLESERVER" Then
                    e.ConnectionParameters = CreateConnectionParametersOracle()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Function CreateConnectionParametersSql() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(2000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New MsSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .DatabaseName = connString.Split(";")(1).Split("=")(1),
            .AuthorizationType = MsSqlAuthorizationType.SqlServer,
            .UserName = connString.Split(";")(2).Split("=")(1),
            .Password = connString.Split(";")(3).Split("=")(1)
        }
    End Function

    Private Function CreateConnectionParametersPostGreSql() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(3000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New PostgreSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .PortNumber = connString.Split(";")(1).Split("=")(1),
            .UserName = connString.Split(";")(2).Split("=")(1),
            .Password = connString.Split(";")(3).Split("=")(1),
            .DatabaseName = connString.Split(";")(4).Split("=")(1)
        }
    End Function

    Private Function CreateConnectionParametersOracle() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(4000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New OracleConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .UserName = connString.Split(";")(1).Split("=")(1),
            .Password = connString.Split(";")(2).Split("=")(1),
            .ProviderType = OracleProviderType.ODPManaged
        }
    End Function

    '    Private Shared Function CreateFederatedDataSourceJoin(ByVal sqliteDataSource As DashboardSqlDataSource, ByVal exceldataSource As DashboardExcelDataSource, ByVal objectDataSource As DashboardObjectDataSource) As DashboardFederationDataSource
    '        Dim federationDS As New DashboardFederationDataSource("Federated Data Source (JOIN)")
    '        Dim sqlSource As New Source("sqlite", sqliteDataSource, "SQLite Orders")
    '        Dim excelSource As New Source("excel", exceldataSource, "")
    '        Dim objectSource As New Source("object", objectDataSource, "")

    '#Region "Use API to join SQL, Excel, and Object Data Sources in a Query"
    '        Dim mainQueryCreatedByApi As New SelectNode()

    '        mainQueryCreatedByApi.Alias = "FDS-Created-by-API"
    '        Dim sqlSourceNode As New SourceNode(sqlSource, "SQLite Orders")
    '        Dim excelSourceNode As New SourceNode(excelSource, "ExcelDS")
    '        Dim objectSourceNode As New SourceNode(objectSource, "ObjectDS")

    '        mainQueryCreatedByApi.Root = sqlSourceNode
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "SalesPerson", .Node = objectSourceNode})
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "Weight", .Node = objectSourceNode})
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "CategoryName", .Node = excelSourceNode})
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "ProductName", .Node = excelSourceNode})
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "OrderDate", .Node = sqlSourceNode})
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "ShipCity", .Node = sqlSourceNode})
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "ShipCountry", .Node = sqlSourceNode})
    '        mainQueryCreatedByApi.Expressions.Add(New SelectColumnExpression() With {.Name = "Extended Price", .Node = excelSourceNode})
    '        mainQueryCreatedByApi.SubNodes.Add(New JoinElement(excelSourceNode, JoinType.Inner, "[ExcelDS.OrderID] = [SQLite Orders.OrderID]"))
    '        mainQueryCreatedByApi.SubNodes.Add(New JoinElement(objectSourceNode, JoinType.Inner, "[ObjectDS.SalesPerson] = [ExcelDS.Sales Person]"))
    '#End Region

    '#Region "Use NodedBuilder to join SQL, Excel, and Object Data Sources in a Query"
    '        Dim mainQueryCreatedByNodeBuilder As SelectNode = sqlSource.From().Select("OrderDate", "ShipCity", "ShipCountry").Join(excelSource, "[excel.OrderID] = [sqlite.OrderID]").Select("CategoryName", "ProductName", "Extended Price").Join(objectSource, "[object.SalesPerson] = [excel.Sales Person]").Select("SalesPerson", "Weight").Build("FDS-Created-by-NodeBulder")
    '#End Region

    '        federationDS.Queries.Add(mainQueryCreatedByApi)
    '        federationDS.Queries.Add(mainQueryCreatedByNodeBuilder)

    '        federationDS.CalculatedFields.Add("FDS-Created-by-NodeBulder", "[Weight] * [Extended Price] / 100", "Score")

    '        federationDS.Fill(New DevExpress.Data.IParameter() {})
    '        Return federationDS
    '    End Function

#End Region

    Public Class SaveCommandHandler
        Implements ICommandHandler

        Private panel As DashboardDesigner

        Public Sub New(ByVal panel As DashboardDesigner)
            Me.panel = panel
        End Sub

        Public Sub HandleCommand(command As PrintingSystemCommand, args() As Object, printControl As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
            handled = True
        End Sub

        Public Function CanHandleCommand(command As PrintingSystemCommand, printControl As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
            Throw New NotImplementedException()
        End Function

    End Class

End Class
