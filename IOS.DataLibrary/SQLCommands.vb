Imports System.Text

Public Class clsSQLCommands

#Region "Variables"

    Public Shared sqlQuery As StringBuilder

#End Region

#Region "Splash Screen"

    Public Shared Function CheckLicenseUserExists(ByVal connStr As String, ByVal _companyName As String, ByVal _userName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select * From [IOS_Licenses] Where ([LicenseCompany] = " & Chr(39) & _companyName & Chr(39) & " AND [LicenseUser] = " & Chr(39) & _userName & Chr(39) & ")")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetLicenseData(ByVal connStr As String, ByVal _companyName As String, ByVal _userName As String) As DataTable
        sqlQuery = New StringBuilder()
        If ColumnExistsInSqlTable(connStr, "IOS_Licenses", "IsLocked") AndAlso ColumnExistsInSqlTable(connStr, "IOS_Licenses", "IsEnabled") Then
            sqlQuery.AppendLine("Select * FROM IOS_Licenses WHERE (LicenseCompany = " & Chr(39) & _companyName & Chr(39) & " And LicenseUser = " & Chr(39) & _userName & Chr(39) & " And ExpirationDate >= GETDATE() And (IsLocked = 0) And (IsEnabled = 1))")
        Else
            sqlQuery.AppendLine("Select * FROM IOS_Licenses WHERE (LicenseCompany = " & Chr(39) & _companyName & Chr(39) & " And LicenseUser = " & Chr(39) & _userName & Chr(39) & " And ExpirationDate >= GETDATE())")
        End If
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetUserConfigClient(ByVal connStr As String, ByVal _userName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [dbo].[IOS_License_ConfigClient].* FROM [dbo].[IOS_License_ConfigClient] INNER JOIN [dbo].[IOS_Licenses] On [dbo].[IOS_License_ConfigClient].[ConfigClientID] = [dbo].[IOS_Licenses].[ConfigClientID]")
        sqlQuery.AppendLine("WHERE [dbo].[IOS_Licenses].[LicenseUser] = " & Chr(39) & _userName & Chr(39) & ";")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Sub InsertIOSAccessLog(connStr As String, ByVal _companyName As String, ByVal _Username As String, ByVal _Version As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO IOS_Usage (LastDate, UserID, Company, Version) VALUES (GETDATE(), " & Chr(39) & _Username & Chr(39) & ", " & Chr(39) & _companyName & Chr(39) & ", " & Chr(39) & Replace(_Version, "Version ", "") & Chr(39) & ")")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function GetLicenseConfigTemplateDetail(ByVal connStr As String, TemplateID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select TemplateDetailId, TemplateID, RTRIM(LTRIM(FormName)) As FormName, RTRIM(LTRIM(CategoryName)) As CategoryName, RTRIM(LTRIM(ControlName)) As ControlName, IsEnabled, IsVisible ")
        sqlQuery.AppendLine("From IOS_License_ConfigTemplateDetails Where TemplateID = " & TemplateID)
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function ColumnExistsInSqlTable(connStr As String, tableName As String, columnName As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & tableName & "' AND COLUMN_NAME = '" & columnName & "'"
        Return Convert.ToInt32(DataAccessorODBC.ExecuteScalar(connStr, query)) > 0
    End Function

#End Region

#Region "Import Drive Test"

    Public Shared Function GetProjectList(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select DISTINCT Project FROM dbo.DT_List")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDriveTestByProject(ByVal connStr As String, projectName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select DISTINCT DriveTest FROM dbo.DT_List WHERE Project = '" & projectName & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDeviceListByDriveTest(ByVal connStr As String, ByVal driveTest As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select DISTINCT Dtid, Device FROM dbo.DT_List WHERE DriveTest = '" & driveTest & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Jobs SQL"

    Public Shared Function AddJobs(ByVal sql_conn, ByVal JobName, ByVal JobDescription, ByVal JobOwner, ByVal JobInterval, ByVal JobNextRun, ByVal JobActive, ByVal JobTimeOut, ByVal JobProtectionLimit, ByVal jobType)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Jobs_Add '" & JobName & "', '" & JobDescription & "', '" & JobOwner & "', '" & JobInterval & "', '" & JobInterval & "', " & JobActive & ", " & JobTimeOut & ", " & JobProtectionLimit & "," & jobType)
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function UpdateJobs(ByVal sql_conn, ByVal jobId, ByVal JobName, ByVal JobDescription, ByVal JobOwner, ByVal JobInterval, ByVal JobNextRun, ByVal JobActive, ByVal JobTimeOut, ByVal JobProtectionLimit, ByVal jobType)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Jobs_Update " & jobId & ", '" & JobName & "', '" & JobDescription & "', '" & JobOwner & "', '" & JobInterval & "', '" & JobInterval & "', " & JobActive & ", " & JobTimeOut & ", " & JobProtectionLimit & "," & jobType)
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetJobDetails(ByVal connStr As String, ByVal jobID As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT JobDetailID, JobType, SequenceNumber, ConnectionString, SQLString, DestinationTable FROM dbo.IOS_Jobs_Details WHERE JobID = " & jobID & " ORDER BY SequenceNumber ASC")
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Map Window"

    Public Shared Function GetMapConfigurationData(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.IOS_Map_Configuration WHERE LayerActive = 1 ORDER BY LayerOrder")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function CheckDriveTestOwner(ByVal connStr As String, ByVal dtID As String, ByVal userName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.DT_List WHERE DtID ='" & dtID & "' AND ImportOwner ='" & userName & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetMapLayerTechnologyCount(ByVal connStr As String, market As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select dt.LayerTechnology, max(layercount) as TechCount from (SELECT [LayerVendor], [LayerTechnology], count(*) as layercount FROM [dbo].[IOS_Map_Configuration] Where [Market] = '" & market & "' group by [LayerVendor], [LayerTechnology], [LayerTech]) dt group by dt.LayerTechnology order by dt.LayerTechnology")
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function Get3G4G5GLayer(ByVal connStr As String, Optional market As String = "ALL") As DataSet
        sqlQuery = New StringBuilder()
        If market.ToUpper = "ALL" Then
            sqlQuery.AppendLine("SELECT [Layer] FROM [data_Common].[dbo].[C_OSS_CELLS] WHERE [TECH] in('3G','4G','5G') GROUP BY [LAYER] ORDER BY [LAYER]")
        Else
            sqlQuery.AppendLine("SELECT [Layer] FROM [data_Common].[dbo].[C_OSS_CELLS] WHERE [TECH] in('3G','4G','5G') AND [MARKET] = '" & market & "' GROUP BY [LAYER] ORDER BY [LAYER]")
        End If
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    'Public Shared Function Get4GUarfcn(ByVal connStr As String) As DataSet
    '    sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("SELECT DISTINCT RTRIM(LTRIM(STR(LayerUARFCN))) AS LayerUARFCN FROM dbo.IOS_Map_Configuration where LayerUARFCN is not null and LayerTech like '%4G%'")
    '    Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    'End Function

    Public Shared Function GetMapCategory(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT Category FROM dbo.DT_POI")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDateTimeAndCoordinatesOnDriveTestSelection(ByVal connStr As String, ByVal cellEvent As String, ByVal isWithMinMaxXY As Boolean) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT CampaignStart,CampaignStop FROM dbo.DT_List WHERE DtId IN(" & cellEvent & ");")
        If (isWithMinMaxXY) Then
            sqlQuery.AppendLine("SELECT MIN(px) AS px1,MAX(px) as px2,MIN(py) AS py1,MAX(py) as py2 FROM dbo.DT_Scan2G_Parallel WHERE dtid IN(" & cellEvent & ");")
            sqlQuery.AppendLine("SELECT MIN(px) AS px1,MAX(px) as px2,MIN(py) AS py1,MAX(py) as py2 FROM dbo.DT_Scan3G_Parallel WHERE dtid IN(" & cellEvent & ");")
            sqlQuery.AppendLine("SELECT MIN(px) AS px1,MAX(px) as px2,MIN(py) AS py1,MAX(py) as py2 FROM dbo.DT_UE3G_Parallel WHERE dtid IN(" & cellEvent & ");")
            sqlQuery.AppendLine("SELECT MIN(px) AS px1,MAX(px) as px2,MIN(py) AS py1,MAX(py) as py2 FROM dbo.DT_UE2G_Parallel WHERE dtid IN(" & cellEvent & ");")
            sqlQuery.AppendLine("SELECT MIN(px) AS px1,MAX(px) as px2,MIN(py) AS py1,MAX(py) as py2 FROM dbo.DT_UE4G_Parallel WHERE dtid IN(" & cellEvent & ");")
            sqlQuery.AppendLine("SELECT MIN(px) AS px1,MAX(px) as px2,MIN(py) AS py1,MAX(py) as py2 FROM dbo.DT_Scan4G_Parallel WHERE dtid IN(" & cellEvent & ");")
        End If
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function UpdatePageViewStatus(ByVal connStr As String, ByVal pageRequestCount As String, ByVal userName As String, ByVal timeOut As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_Usage SET MapPageViews =  " + pageRequestCount + " WHERE userID = '" + userName + "' AND")
        sqlQuery.AppendLine("LastDate = (SELECT MAX(lastdate) FROM IOS_Usage WHERE userId = '" + userName + "')")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString, timeOut)
    End Function

    Public Shared Function GetDefultFootPrintTimeStamp(ByVal connStr As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT MIN(CampaignStart) AS CampaignStart, MAX(CampaignStop) AS CampaignStop FROM DT_List WHERE (CampaignStart IS NOT NULL AND CampaignStart <> '') AND (CampaignStop IS NOT NULL AND CampaignStop <> '')")
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetCneDataSourceComboBox(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.PM_CNE_DataSources")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetPredefinedPeriodComboBox(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_PredefinedPeriod ")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetIOSSql(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.IOS_SQL")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetNetworkStatus(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.IOS_Network_Status ORDER BY IOS_Network_Status.NetworkDate DESC")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetNetworkStatusFromTabFiles(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT NetworkDate FROM dbo.IOS_Network_TABFiles")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function Get2GFrequencies(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_Frequencies WHERE TECH = '2G' ORDER BY ARFCN")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetCampaignName(ByVal connStr As String, ByVal source As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT DriveTest FROM dbo.DT_List WHERE Equipment = '" & source & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDTPOIFromCategory(ByVal connStr As String, ByVal category As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.DT_POI WHERE Category = '" & category & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function UpdateDTList(ByVal connStr As String, ByVal newDT As String, ByVal dtID As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Update dbo.DT_List SET Device='" & newDT & "' WHERE Dtid='" & dtID & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteDriveTestID(ByVal connStr As String, ByVal dtID As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC DeleteDTID " & dtID)
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function ManageDTProject(ByVal connStr As String, opType As String, projectID As String, Optional projectName As String = Nothing) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC sp_ManageProject " & "'" & opType & "','" & projectID & "','" & projectName & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function ManageDriveTest(ByVal connStr As String, opType As String, driveTestID As String, Optional driveTestName As String = Nothing) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC sp_ManageDriveTest " & "'" & opType & "','" & driveTestID & "','" & driveTestName & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function ManageDevice(ByVal connStr As String, opType As String, deviceID As String, Optional deviceName As String = Nothing) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC sp_ManageDevice " & "'" & opType & "'," & deviceID & ",'" & deviceName & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "MDI SQL"

    Public Shared Function Get_IOS_SQL_Data(ByVal sql_conn) As DataTable
        sqlQuery = New StringBuilder
        sqlQuery.AppendLine("SELECT * FROM dbo.IOS_SQL")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_ChartConfig_Data(ByVal sql_conn) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT TechTab FROM dbo.IOS_Chart_Configuration ")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_ChartConfig_Data_By_Tech(ByVal sql_conn, ByVal tech) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT TechTab FROM dbo.IOS_Chart_Configuration where Lower(TechTab) = '" & tech.ToString.ToLower & "' Or Lower(TechTab) = 'topx_" & tech.ToString.ToLower & "'")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_ObjectConfig_Data(ByVal sql_conn) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT InternalObjectName, loadorder 
                             FROM dbo.IOS_Object_Configuration 
                             INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                             Where loadorder is not null  and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "
                             Order By loadorder asc")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_ObjectConfig_Data_By_tech(ByVal sql_conn, ByVal tech) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT InternalObjectName, loadorder, tech 
                            FROM dbo.IOS_Object_Configuration 
                            INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                            WHERE IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "
                            ")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_IOS_ObjectConfig_Active_Data(ByVal sql_conn) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT distinct a.tech, a.Purpose, a.ObjectType, b.loadorder from  dbo.[IOS_SQL_Create] a")
        sqlQuery.AppendLine("inner join dbo.[IOS_Object_Configuration] b on a.tech=b.tech and a.ObjectType = b.[Object] 
                            INNER JOIN IOS_Licenses on b.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                            where a.purpose IN('Charts','TopX')  and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_ObjectConfig_New_Data(ByVal sql_conn) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT a.*,b.[object] AS ParentObject FROM dbo.[IOS_Object_Configuration] a ")
        sqlQuery.AppendLine("LEFT OUTER JOIN dbo.[IOS_Object_Configuration] b ON b.id = a.parentid  and a.ObjectConfigProfile=b.ObjectConfigProfile
                             INNER JOIN IOS_Licenses on a.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                             where a.loadorder is not null   and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "
                             Order By a.loadorder")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_ObjectConfig_New_Data_By_Tech(ByVal sql_conn, ByVal tech) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT a.*,b.[object] AS ParentObject FROM dbo.[IOS_Object_Configuration] a LEFT OUTER JOIN")
        sqlQuery.AppendLine("dbo.[IOS_Object_Configuration] b ON b.id = a.parentid and a.ObjectConfigProfile=b.ObjectConfigProfile
                            INNER JOIN IOS_Licenses on a.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                            Where a.loadorder is not null and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & " and ")
        sqlQuery.AppendLine("UPPER(a.tech) = '" & tech.ToUpper & "' order by a.loadorder")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

#End Region

#Region "SON"

    Public Shared Function Get_IOS_Jobs_Charts(ByVal sql_conn, ByVal jobid) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT ChartCategory from dbo.IOS_Jobs_Charts WHERE JobID=" & jobid)

        Return DataAccessorSQL.ExecuteDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function JobResults(ByVal sql_conn As String, ByVal jobid As String, ByVal outputType As Integer, Optional jobRunId As String = Nothing, Optional _offset As Integer = 0, Optional _batchSize As Integer = 0, Optional filter As String = Nothing, Optional sortExpression As String = Nothing) As DataTable
        sqlQuery = New StringBuilder()
        If String.IsNullOrEmpty(filter) Then
            If String.IsNullOrEmpty(sortExpression) Then
                sqlQuery.AppendLine("EXECUTE sp_JobResults " & jobid & "," & outputType & "," & jobRunId & ", " & _offset & "," & _batchSize)
            Else
                sqlQuery.AppendLine("EXECUTE sp_JobResults " & jobid & "," & outputType & "," & jobRunId & ", " & _offset & "," & _batchSize & "," & "NULL" & ",'" & sortExpression & "'")
            End If
        Else
            filter = filter.Replace("'", "''")
            If String.IsNullOrEmpty(sortExpression) Then
                sqlQuery.AppendLine("EXECUTE sp_JobResults " & jobid & "," & outputType & "," & jobRunId & ", " & _offset & "," & _batchSize & ",'" & filter & "'")
            Else
                sqlQuery.AppendLine("EXECUTE sp_JobResults " & jobid & "," & outputType & "," & jobRunId & ", " & _offset & "," & _batchSize & ",'" & filter & "'" & ",'" & sortExpression & "'")
            End If
        End If
        Return DataAccessorSQL.ExecuteDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_IOS_Jobs_Charts(ByVal sql_conn, ByVal jobid, ByVal chartCategory) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.IOS_Jobs_Charts WHERE jobid = " & jobid)
        sqlQuery.AppendLine("And chartCategory = '" & chartCategory & "' ORDER BY ChartIndex ASC")

        Return DataAccessorSQL.ExecuteDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Delete_Tune_Objects(ByVal sql_conn, ByVal jobid)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Tune_Objects_Delete " & jobid)
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Add_Tune_Objects(ByVal sql_conn, ByVal JobID, ByVal ObjectTech, ByVal ObjectType, ByVal ObjectName)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Tune_Objects_Add " & JobID & ", '" & ObjectTech & "','" & ObjectType & "','" & ObjectName & "'")
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Delete_Tune_ObjectsExceptions(ByVal sql_conn, ByVal jobid)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Tune_ObjectsExceptions_Delete " & jobid)
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Add_Tune_ObjectsExceptions(ByVal sql_conn, ByVal JobID, ByVal ObjectID, ByVal ObjectName)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Tune_ObjectsExceptions_Add " & JobID & ", " & ObjectID & ",'" & ObjectName & "'")
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Delete_Tune_KPI(ByVal sql_conn, ByVal jobid)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Tune_KPI_Delete " & jobid)
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Delete_Jobs_Details(ByVal sql_conn, ByVal jobid)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Jobs_Details_Delete " & jobid)
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_CounterTables(ByVal sql_conn, ByVal technology) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT top 1 ConnectionString from dbo.IOS_CounterTables where UPPER(Technology) = '" & technology & "'")
        Return DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Delete_Tune_Parameter(ByVal sql_conn, ByVal jobid)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Tune_Parameter_Delete" & jobid)
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Add_Jobs_Details(ByVal sql_conn, ByVal JobID, ByVal JobType, ByVal SequenceNumber, ByVal ConnectionString, ByVal SQLString, ByVal DestinationTable) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Jobs_Details_Add " & JobID & ", '" & JobType & "'," & SequenceNumber & ",'" & ConnectionString & "', '" & SQLString & "','" & DestinationTable & " ' ")
        Return DataAccessorSQL.ExecuteDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Add_Tune_Parameter(ByVal sql_conn, ByVal JobID, ByVal ParentParamID, ByVal ParameterID, ByVal ParameterName, ByVal ObjectType, ByVal Stepsize, ByVal ActionGreen, ByVal ActionRed, ByVal UpperLimit, ByVal LowerLimit) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Tune_Parameter_Add " & JobID & ", " & ParentParamID & "," & ParameterID & ",'" & ParameterName & "', '" & ObjectType & "','" & Stepsize & " ','" & ActionGreen & "','" & ActionRed & "','" & UpperLimit & "','" & LowerLimit & "' ")
        Return DataAccessorSQL.ExecuteDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_IOS_OSS_Param_Ref_Data(ByVal sql_conn, ByVal id) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_OSS_Param_Ref WHERE ID = " & id)
        Return DataAccessorSQL.ExecuteDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_IOS_Tune_Parameter_Data(ByVal sql_conn, ByVal jobid) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_Tune_Parameter WHERE JobID = " & jobid & " ORDER BY RecordID ASC ")
        Return DataAccessorSQL.ExecuteDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_IOS_Jobs_Data(ByVal sql_conn) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT JobID, JobName from dbo.IOS_Jobs where JobTab = 'Param' order by JobID")
        Return DataAccessorSQL.ExecuteDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_IOS_Tune_Result_Data(ByVal sql_conn, ByVal tuneAnalysisJob, ByVal jobRunId) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("  SELECT IOS_Jobs.JobName, IOS_Tune_Result.* from IOS_Tune_Result inner join IOS_Jobs on IOS_Tune_Result.Jobid = IOS_Jobs.JobID")
        sqlQuery.AppendLine(" where  IOS_Jobs.JobName = '" & tuneAnalysisJob & "' and JobRunID like '%" & jobRunId & "%' order by PARENT, GID")
        Return DataAccessorSQL.ExecuteDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Get_IOS_Tune_Result_Export_Data(ByVal sql_conn, ByVal tuneAnalysisJob) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT IOS_Jobs.JobID, JobRunID, UserImportDate, [UserComment], [User], [Path] from dbo.IOS_Tune_Result_Tracking inner join")
        sqlQuery.AppendLine("IOS_Jobs on IOS_Tune_Result_Tracking.JobID = IOS_Jobs.JobID where JobName = '" & tuneAnalysisJob & "' order by JobRunID")
        Return DataAccessorSQL.ExecuteDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function Delete_IOS_Jobs(ByVal sql_conn, ByVal jobid, ByVal jobOwner)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Jobs_Delete " & jobid & ", " & Environment.UserName.ToString)
        Return DataAccessorSQL.ExecuteNonQuery(sql_conn, sqlQuery.ToString)
    End Function

#End Region

#Region "Technology"

    'Public Shared Function Get_IOS_ObjectTypes_Data(ByVal sqlCon As String) As DataSet
    '    sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("SELECT DISTINCT InternalObjectName, parentid, tech, Object, loadorder FROM")
    '    sqlQuery.AppendLine("dbo.IOS_Object_Configuration where sqlid is not null order by tech asc, loadorder asc")
    '    Return DataAccessorODBC.GetDataSet(sqlCon, sqlQuery.ToString)
    'End Function

    'Public Shared Function Get_IOS_ChartConfig_Sourcetable(sql_conn, tech, chartname) As DataTable
    '    sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("SELECT TOP 1 sourcetable from qry_IOS_Configuration_Charts_1Table where techtab = '" & tech & "' AND chartname = '" & chartname & "'")
    '    sqlQuery.AppendLine("")
    '    Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    'End Function

    'Public Shared Function Get_IOS_ObjectConfig_Data(ByVal sqlCon) As DataSet
    '    sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("SELECT   a.[Object], a.Aggr_from_btn, a.Tech, a.InternalObjectName, Order_TimeBased FROM dbo.IOS_Object_Configuration a")
    '    sqlQuery.AppendLine("where a.Aggr_from_btn is not null and a.Order_TimeBased is not null group by  a.[Object], a.Aggr_from_btn, a.Tech,")
    '    sqlQuery.AppendLine("a.InternalObjectName,a.parentid, Order_TimeBased order by a.tech asc, Order_TimeBased asc")
    '    Return DataAccessorODBC.GetDataSet(sqlCon, sqlQuery.ToString)
    'End Function

    'Public Shared Function Get_Object_Configuration_Data(ByVal sql_conn) As DataSet
    '    sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("SELECT DISTINCT InternalObjectName, parentid, tech, Object, loadorder FROM")
    '    sqlQuery.AppendLine("dbo.IOS_Object_Configuration where sqlid is not null order by tech asc, loadorder asc")
    '    Return DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    'End Function

    'Public Shared Function Get_Chart_Config_KPI_Data(ByVal sql_conn, ByVal tech, ByVal chartname, ByVal chartsetname) As DataTable
    '    sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("SELECT TechTab,SQLKPI_ID,ObjectTab,ChartElements from IOS_Chart_Configuration WHERE (((TechTab = '" & tech & "')")
    '    sqlQuery.AppendLine("And (ChartName ='" & chartname & "') AND ((ChartSetName = '" & chartsetname & "') OR (ChartSetName = '" & Environment.UserName.ToString & "'))))")
    '    sqlQuery.AppendLine("ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
    '    Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    'End Function

    Public Shared Function GetCounterTypeForTopX(ByVal sql_conn As String, ByVal tech As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select tbl1.Objecttab,tbl1.ObjectCount,tbl2.ObjectTreeEnabled 
                            From (SELECT Objecttab, COUNT(*) As ObjectCount 
                            FROM IOS_Chart_Configuration 
                            WHERE techtab = '" & tech & "' 
                            GROUP BY ObjectTab) tbl1 
                            INNER JOIN IOS_Object_Configuration tbl2 ON tbl1.Objecttab=tbl2.[Object] 
                            INNER JOIN IOS_Licenses on tbl2.ObjectConfigProfile=IOS_Licenses.ObjectConfigProfile
                            WHERE IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "
                            ORDER BY tbl1.ObjectCount DESC")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetObjectConfigurationData(ByVal sqlCon As String, Optional techName As String = Nothing, Optional vendorName As String = Nothing) As DataSet
        'sqlQuery = New StringBuilder()
        'sqlQuery.AppendLine("SELECT DISTINCT InternalObjectName, parentid, tech, Object, loadorder, COALESCE(ObjectTreeEnabled,1) As ObjectTreeEnabled FROM")
        'sqlQuery.AppendLine("dbo.IOS_Object_Configuration WHERE sqlid IS NOT NULL ")
        'If (techName IsNot Nothing) Then
        '    sqlQuery.AppendLine(" AND tech='" & techName & "' ")
        'End If
        'If (vendorName IsNot Nothing) Then
        '    sqlQuery.AppendLine(" AND Vendor='" & vendorName & "' ")
        'End If
        'sqlQuery.AppendLine(" ORDER BY tech ASC, loadorder ASC")
        If (techName IsNot Nothing) Then
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("EXEC [dbo].[sp_IOS_ObjectConfigAll_Get] '" & techName & "',Null,'" & Environment.UserName.ToString & "'")
        End If
        If (vendorName IsNot Nothing) Then
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("EXEC [dbo].[sp_IOS_ObjectConfigAll_Get] Null,'" & vendorName & "','" & Environment.UserName.ToString & "'")
        End If
        If (techName IsNot Nothing) AndAlso (vendorName IsNot Nothing) Then
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("EXEC [dbo].[sp_IOS_ObjectConfigAll_Get] '" & techName & "','" & vendorName & "','" & Environment.UserName.ToString & "'")
        End If
        Return DataAccessorODBC.GetDataSet(sqlCon, sqlQuery.ToString)
    End Function

    Public Shared Function GetObjectConfigData(ByVal sql_conn As String, Optional ByVal tech As String = Nothing) As DataSet
        sqlQuery = New StringBuilder()
        'sqlQuery.AppendLine("SELECT a.[Object], a.Aggr_from_btn, a.Tech, a.InternalObjectName, Order_TimeBased FROM dbo.IOS_Object_Configuration a ")
        'sqlQuery.AppendLine("where a.Aggr_from_btn is not null and a.Order_TimeBased is not null ")
        'If tech IsNot Nothing Then
        '    sqlQuery.AppendLine(" AND a.tech='" & tech & "'")
        'End If
        'sqlQuery.AppendLine("group by  a.[Object], a.Aggr_from_btn, a.Tech, a.InternalObjectName,a.parentid, Order_TimeBased order by a.tech asc, Order_TimeBased asc")
        sqlQuery.AppendLine("EXEC [dbo].[sp_IOS_ObjectConfigCounterTypes_Get] 'stats','" & tech & "','" & Environment.UserName.ToString & "'")
        Return DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetTopXObjectConfigData(ByVal sql_conn As String, ByVal tech As String) As DataSet
        sqlQuery = New StringBuilder()
        'sqlQuery.AppendLine("SELECT   a.[Object], a.Aggr_from_btn, a.Tech, a.InternalObjectName, Order_TopX FROM dbo.IOS_Object_Configuration a ")
        'sqlQuery.AppendLine(" where a.Aggr_from_btn is not null and a.Order_TopX is not null AND a.tech='" & tech & "'")
        'sqlQuery.AppendLine("group by  a.[Object], a.Aggr_from_btn, a.Tech, a.InternalObjectName,a.parentid, Order_TopX order by a.tech asc, Order_TopX asc")
        sqlQuery.AppendLine("EXEC [dbo].[sp_IOS_ObjectConfigCounterTypes_Get] 'topx','" & tech & "','" & Environment.UserName.ToString & "'")
        Return DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetEvalObjectConfigData(ByVal sql_conn As String, ByVal tech As String) As DataSet
        sqlQuery = New StringBuilder()
        'sqlQuery.AppendLine("SELECT   a.[Object], a.Aggr_from_btn, a.Tech, a.InternalObjectName, Order_Eval FROM dbo.IOS_Object_Configuration a ")
        'sqlQuery.AppendLine(" where a.Aggr_from_btn is not null and a.Order_Eval is not null AND a.tech='" & tech & "'")
        'sqlQuery.AppendLine("group by  a.[Object], a.Aggr_from_btn, a.Tech, a.InternalObjectName,a.parentid, Order_Eval order by a.tech asc, Order_Eval asc")
        sqlQuery.AppendLine("EXEC [dbo].[sp_IOS_ObjectConfigCounterTypes_Get] 'eval','" & tech & "','" & Environment.UserName.ToString & "'")
        Return DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetTechnologyNotesData(ByVal sql_conn As String, ByVal objName As String, ByVal tech As String, ByVal noteObject As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT NotesID, Timestamp, Username, Tech, NoteType, Department, ShortDescription, LongDescription, NoteTypeID")
        If objName = "IN ()" Then
            sqlQuery.AppendLine(" from (SELECT * FROM qry_IOS_Notes WHERE ")
        Else
            sqlQuery.AppendLine(" from (SELECT * FROM qry_IOS_Notes WHERE NoteObjectName " & objName & " AND")
        End If
        sqlQuery.AppendLine(" Tech = '" & tech & "' AND NoteObject " & noteObject & ") DERIVEDTBL Order By [Timestamp] Desc")
        Return DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartConfigurationData(ByVal sql_conn As String, ByVal tech As String, ByVal chartname As String, ByVal chartsetname As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT TechTab,SQLKPI_ID,ObjectTab,ChartElements from IOS_Chart_Configuration WHERE (((TechTab = '" & tech & "')")
        sqlQuery.AppendLine("And (ChartName ='" & chartname & "') AND ((ChartSetName = '" & chartsetname & "') OR (ChartSetName = '" & Environment.UserName.ToString & "'))))")
        sqlQuery.AppendLine("ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetKPINotesData(ByVal sql_conn As String, ByVal strKPI As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("select SQLKPI_ID,KPI_Name,NoteID,NoteTimeStamp,NoteDescription,NoteOwner from [IOS_SQL_KPI] inner ")
        sqlQuery.AppendLine("join IOS_KPI_Notes on IOS_KPI_Notes.KPIid =[IOS_SQL_KPI].SQLKPI_ID where KPI_Name IN(" & strKPI & ")")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetIOSTags(ByVal sql_conn As String, ByVal tech As String, ByVal tagOwner As String, ByVal objType As String) As DataTable
        If tech.ToLower.Contains("topx") Then
            tech = tech.Replace("TopX_", "")
        End If
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT TagID, TagName, EnablePreAggregation From IOS_Tags Where TagType = 'Static List' And Technology = " & Chr(39) & tech & Chr(39) & "")
        sqlQuery.AppendLine("And TagOwner = " & Chr(39) & tagOwner & Chr(39) & " And ObjectType = " & Chr(39) & objType & Chr(39) & " And (([IsPrivate] = 0) Or ([TagOwner] = " & Chr(39) & tagOwner & Chr(39) & " And IsPrivate = 1)) Order By [TagName]")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetConfigChartSourceTableData(ByVal sql_conn As String, ByVal tech As String, ByVal chartName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT TOP 1 sourcetable from qry_IOS_Configuration_Charts_2Table where techtab = '" & tech & "' AND chartname = '" & chartName & "'")
        sqlQuery.AppendLine("ORDER BY CategoryTabIndex, ChartIndex, ChartElementID")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartConfigurationByElement(ByVal sql_conn As String, ByVal _tech As String, ByVal _kpiName As String, ByVal _ObjectTab As String, Optional _chartName As String = "%") As DataSet
        sqlQuery = New StringBuilder()
        If _ObjectTab <> "" Then
            sqlQuery.AppendLine("SELECT * from qry_IOS_Configuration_Charts_2Table WHERE ChartElements = " & Chr(39) & _kpiName & Chr(39) & " AND techtab = " & Chr(39) & _tech & Chr(39) & " AND ObjectTab = " & Chr(39) & _ObjectTab & Chr(39) & " AND ChartName LIKE " & Chr(39) & _chartName & Chr(39))
            sqlQuery.AppendLine("ORDER BY CategoryTabIndex, ChartIndex, ChartElementID")
        Else
            sqlQuery.AppendLine("SELECT * from qry_IOS_Configuration_Charts_2Table WHERE ChartElements = " & Chr(39) & _kpiName & Chr(39) & " AND techtab = " & Chr(39) & _tech & Chr(39) & " AND ChartName LIKE " & Chr(39) & _chartName & Chr(39))
            sqlQuery.AppendLine("ORDER BY CategoryTabIndex, ChartIndex, ChartElementID")
        End If
        Return IOS.DataLibrary.DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartConfigurationByElementAndChart(ByVal sql_conn As String, ByVal _tech As String, ByVal _kpiName As String, ByVal _ChartName As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * from qry_IOS_Configuration_Charts_2Table WHERE ChartElements = " & Chr(39) & _kpiName & Chr(39) & " AND techtab = " & Chr(39) & _tech & Chr(39) & " AND ChartName = " & Chr(39) & _ChartName & Chr(39))
        sqlQuery.AppendLine("ORDER BY CategoryTabIndex, ChartIndex, ChartElementID")
        Return IOS.DataLibrary.DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetTopXChartConfigData(ByVal sql_conn, ByVal chartsetname, ByVal tech, ByVal kpi) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT techtab, Categorytabindex, Categorytab, Chartindex, ChartName FROM IOS_Chart_Configuration WHERE ")
        sqlQuery.AppendLine("(((ChartSetName = '" & chartsetname & "') OR (ChartSetName = '" & Environment.UserName.ToString & "'))  AND")
        sqlQuery.AppendLine("TechTab = '" & "TopX_" & tech & "'  AND ChartElements = '" & kpi & "') ORDER BY techtab, categorytabindex, chartindex")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartConfigCustomData(ByVal sql_conn, ByVal chartsetname, ByVal tech, ByVal chartname) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT *, (SELECT count(ChartIndex) + 1 from IOS_Chart_configuration)")
        sqlQuery.AppendLine("as MaxOfCustomChartIndex from IOS_Chart_Configuration WHERE (((TechTab = '" & tech & "') AND (ChartName = '" & chartname & "')")
        sqlQuery.AppendLine("AND ((ChartSetName = '" & chartsetname & "') OR (ChartSetName = '" & Environment.UserName.ToString & "'))))")
        sqlQuery.AppendLine(" ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
        Return DataAccessorODBC.GetDataSet(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetObjectConfigurationByObject(ByVal sql_conn As String, ByVal _object As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT [Object] 
                            from IOS_Object_Configuration a 
                            inner join (
                            select ID from IOS_Object_Configuration   
                            INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                            where [Object] = '" + _object + "' and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "
                            ) b on a.ParentID = b.ID and a.sqlID is not null")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartConfigReaderQuery(ByVal sql_conn As String, _tech As String, _chartname As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.JoinObjects, IOS_Chart_Configuration.CrossTabObj, IOS_Chart_Configuration.ObjectTab FROM IOS_Chart_Configuration")
        sqlQuery.AppendLine("INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
        sqlQuery.AppendLine("WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > " & -1 & ") AND (sourcetable is not null) ")
        sqlQuery.AppendLine(" AND (IOS_Chart_Configuration.ChartName = " & Chr(39) & _chartname & Chr(39) & ")")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetConstructStatsSQL(_tech As String, _purpose As String, _aggr_to As String, _aggr_from As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM qry_IOS_ConstructStatSQL WHERE (((tech)=" & Chr(39) & _tech & Chr(39) & ") AND ((Purpose)=" & Chr(39) & _purpose & Chr(39) & ") AND ")
        sqlQuery.AppendLine(" ((Aggregate_to)=" & Chr(39) & _aggr_to & Chr(39) & ") AND ((Aggregate_From) LIKE " & Chr(39) & _aggr_from & Chr(39) & "))")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetConstructStatsSQL(_tech As String, _purpose As String, _aggr_to As String, _aggr_from As String, _objtype As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM qry_IOS_ConstructStatSQL WHERE (((tech)=" & Chr(39) & _tech & Chr(39) & ") AND ((Purpose)=" & Chr(39) & _purpose & Chr(39) & ") AND ")
        sqlQuery.AppendLine(" ((Aggregate_to)=" & Chr(39) & _aggr_to & Chr(39) & ") AND ((Aggregate_From) LIKE " & Chr(39) & _aggr_from & Chr(39) & ") AND ((ObjectType)=" & Chr(39) & _objtype & Chr(39) & "))")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetChartKPISQL(_tech As String, _supportcode As String, _selected_tabs As String, _selected_charts As String, _selected_kpis As String, Optional ByVal _chartname As String = Nothing) As String
        sqlQuery = New StringBuilder()
        If _chartname = Nothing Then
            sqlQuery.AppendLine("SELECT DISTINCT Cast(IOS_SQL_KPI.KPI_SQL as NVarchar(Max)), IOS_SQL_KPI.KPI_NAME FROM IOS_Chart_Configuration ")
            sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID WHERE (IOS_Chart_Configuration.TechTab = '" & "TopX_" & _tech & "') AND ")
            sqlQuery.AppendLine(" (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & " AND CategoryTab " & _selected_tabs & " AND ChartTitle " & _selected_charts & " AND ChartElements " & _selected_kpis & ") ")
        Else
            sqlQuery.AppendLine("SELECT DISTINCT Cast(IOS_SQL_KPI.KPI_SQL as NVarchar(Max)), IOS_SQL_KPI.KPI_NAME FROM IOS_Chart_Configuration ")
            sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
            sqlQuery.AppendLine(" WHERE (IOS_Chart_Configuration.ChartName = " & Chr(39) & _chartname & Chr(39) & " ) AND (IOS_Chart_Configuration.TechTab = '" & "TopX" & _tech & "') AND ")
            sqlQuery.AppendLine(" (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & ") ")
        End If
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetTopXChartConfigurationSQL(_tech As String, _chartSetName As String, _selected_tabs As String, _selected_charts As String, _selected_kpis As String, _username As String, Optional ByVal _chartname_original As String = Nothing) As String
        sqlQuery = New StringBuilder()
        If _chartname_original Is Nothing Then
            sqlQuery.AppendLine("SELECT * from IOS_Chart_Configuration ")
            sqlQuery.AppendLine("WHERE (((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & "))  AND TechTab = " & Chr(39) & _tech & Chr(39) & " AND ")
            sqlQuery.AppendLine("CategoryTab " & _selected_tabs & " AND ChartTitle " & _selected_charts & " AND ChartElements " & _selected_kpis & " OR (ChartSetName = " & _username & " AND TechTab = " & Chr(39) & _tech & Chr(39) & " AND ChartTitle " & _selected_charts & " )) ")
            sqlQuery.AppendLine("ORDER BY TechTab, CategoryTabIndex, ChartIndex, ChartElementID ASC;")
        Else
            sqlQuery.AppendLine("SELECT * from IOS_Chart_Configuration ")
            sqlQuery.AppendLine("WHERE (TechTab = " & Chr(39) & _tech & Chr(39) & " AND ChartName = " & Chr(39) & _chartname_original & Chr(39) & " AND ChartElements " & _selected_kpis & ") ")
            sqlQuery.AppendLine("ORDER BY TechTab, CategoryTabIndex, ChartIndex, ChartElementID ASC;")
        End If
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetTopXChartConfigurationDeltaSQL(_tech As String, _chartSetName As String, _selected_tabs As String, _selected_charts As String, _selected_kpis As String, _username As String, Optional ByVal _chartname_original As String = Nothing) As String
        sqlQuery = New StringBuilder()
        If _chartname_original Is Nothing Then
            sqlQuery.AppendLine("SELECT * from IOS_Chart_Configuration ")
            sqlQuery.AppendLine("WHERE (((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & "))  AND TechTab = " & Chr(39) & _tech & Chr(39) & " AND CategoryTab " & _selected_tabs & " AND ChartTitle " & _selected_charts & " AND ChartElements " & _selected_kpis & " OR (ChartSetName = " & _username & " AND TechTab = " & Chr(39) & _tech & Chr(39) & " AND ChartTitle " & _selected_charts & " )) ORDER BY TechTab, CategoryTabIndex, ChartIndex, ChartElementID ASC;")
        Else
            sqlQuery.AppendLine("SELECT * from IOS_Chart_Configuration ")
            sqlQuery.AppendLine("WHERE (TechTab = " & Chr(39) & _tech & Chr(39) & " AND ChartName = " & Chr(39) & _chartname_original & Chr(39) & " AND ChartElements " & _selected_kpis & ") ORDER BY TechTab, CategoryTabIndex, ChartIndex, ChartElementID ASC;")
        End If
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetProcessStatsQuery(_tech As String, kpi_name As String, Optional chName As String = Nothing) As String
        sqlQuery = New StringBuilder()
        If chName Is Nothing Then
            sqlQuery.AppendLine("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable, COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects, COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj, COALESCE(IOS_SQL_KPI.Object,'') AS [Object] FROM IOS_Chart_Configuration ")
            sqlQuery.AppendLine("INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
            sqlQuery.AppendLine("WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > -1) AND (sourcetable is not null) AND (KPI_Name = " & Chr(39) & kpi_name & Chr(39) & ")")
        Else
            sqlQuery.AppendLine("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable, COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects, COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj, COALESCE(IOS_SQL_KPI.Object,'') AS [Object] FROM IOS_Chart_Configuration ")
            sqlQuery.AppendLine("INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
            sqlQuery.AppendLine("WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > -1) AND (sourcetable is not null) AND (ChartName = " & Chr(39) & chName & Chr(39) & ")")
        End If
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetProcessStatsQueryExport2Excel(_tech As String, _chartSetName As String, _UserName As String) As String
        sqlQuery = New StringBuilder()
        'sqlQuery.AppendLine("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable, COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects, COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj, COALESCE(IOS_SQL_KPI.Object,'') AS [Object] FROM IOS_Chart_Configuration ")
        'sqlQuery.AppendLine("INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
        'sqlQuery.AppendLine("WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") And (IOS_Chart_Configuration.ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > -1) AND (sourcetable is not null) ")
        'Return sqlQuery.ToString

        '     If _chartSetName = _UserName Then
        '         sqlQuery.AppendLine("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable, COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects, COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj, COALESCE(IOS_SQL_KPI.Object,'') AS [Object] FROM IOS_Chart_Configuration ")
        '         sqlQuery.AppendLine("INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
        '         sqlQuery.AppendLine("WHERE 
        '             (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") 
        '             And ((IOS_Chart_Configuration.ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") or (IOS_Chart_Configuration.ChartSetName = " & Chr(39) & _UserName & Chr(39) & "))
        '             AND (IOS_SQL_KPI.supportcode > -1) AND (sourcetable is not null) ")
        '     Else
        '         sqlQuery.AppendLine("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable, COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects, COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj, COALESCE(IOS_SQL_KPI.Object,'') AS [Object] FROM IOS_Chart_Configuration ")
        '         sqlQuery.AppendLine("INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
        '         sqlQuery.AppendLine("WHERE 
        '(IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") And 
        '((IOS_Chart_Configuration.ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") OR (Chr(39) & _chartSetName & Chr(39) = @DefaultChartSetNameOfUSer and chartsetname = " & Chr(39) & _UserName & Chr(39) & " )) AND 
        '(IOS_SQL_KPI.supportcode > -1) AND (sourcetable is not null) ")
        '     End If

        sqlQuery.AppendLine("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable, COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects, COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj, COALESCE(IOS_SQL_KPI.Object,'') AS [Object] FROM IOS_Chart_Configuration ")
        sqlQuery.AppendLine("INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
        sqlQuery.AppendLine("WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") And ((IOS_Chart_Configuration.ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") OR ")
        'sqlQuery.AppendLine("(" & Chr(39) & _chartSetName & Chr(39) & " = (Select ChartSetName From IOS_Licenses Where LicenseUser = " & Chr(39) & _UserName & Chr(39) & ") 
        sqlQuery.AppendLine("(ChartSetName = " & Chr(39) & _UserName & Chr(39) & " )) AND (IOS_SQL_KPI.supportcode > -1) AND (sourcetable is not null) ")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetObjectSQLByTag(_tagid As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT TagID, ObjectID, ObjectName from dbo.IOS_Tags_Details_List where tagid = " & _tagid)
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetChartConfigByUsernameAndChartSet(ByVal _chartSetName As String, _username As String, _tabname As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT techtab, Categorytabindex, Categorytab FROM IOS_Chart_Configuration WHERE (((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") OR (ChartSetName = " & _username & "))  AND ")
        sqlQuery.AppendLine("TechTab = " & Chr(39) & _tabname & Chr(39) & ") GROUP BY techtab, Categorytab, categorytabindex ORDER BY techtab, categorytabindex")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetChartConfigByUsernameAndChartSet(ByVal _chartSetName As String, _tabname As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT techtab, Categorytabindex, Categorytab FROM IOS_Chart_Configuration WHERE ((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ")  AND ")
        sqlQuery.AppendLine("TechTab = " & Chr(39) & _tabname & Chr(39) & ") GROUP BY techtab, Categorytab, categorytabindex ORDER BY techtab, categorytabindex")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetAddChartQuery(ByVal _chartSetName As String, _username As String, _tabname As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT techtab, categorytabindex, categorytab, chartindex, chartname FROM IOS_Chart_Configuration ")
        sqlQuery.AppendLine(" WHERE (((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") OR (ChartSetName = " & _username & ")) AND TechTab = " & Chr(39) & _tabname & Chr(39) & ")")
        sqlQuery.AppendLine(" GROUP BY techtab, categorytabindex,categorytab, chartindex, chartname ORDER BY techtab, categorytabindex, chartindex ASC")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetAddChartQuery(ByVal _chartSetName As String, _tabname As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT techtab, categorytabindex, categorytab, chartindex, chartname FROM IOS_Chart_Configuration ")
        sqlQuery.AppendLine(" WHERE ((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") AND TechTab = " & Chr(39) & _tabname & Chr(39) & ")")
        sqlQuery.AppendLine(" GROUP BY techtab, categorytabindex,categorytab, chartindex, chartname ORDER BY techtab, categorytabindex, chartindex ASC")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetTopXCustomChartQuery(ByVal _chartSetName As String, _tech As String, _chartName As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT *, (SELECT max(ChartIndex) + 1 from IOS_Chart_configuration where ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") as MaxOfCustomChartIndex from IOS_Chart_Configuration ")
        sqlQuery.AppendLine(" WHERE (((TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (ChartName = " & Chr(39) & _chartName & Chr(39) & ") AND ((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") OR ")
        sqlQuery.AppendLine(" (ChartSetName = " & Chr(39) & Environment.UserName.ToString & Chr(39) & ")))) ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetSqlElementQueryReport(ByVal _supportcode As Integer, _tech As String, _aliastable As String, Optional ByVal _kpiname As String = Nothing) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object,IOS_SQL_KPI.KPI_Name FROM IOS_Chart_Configuration ")
        sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
        sqlQuery.AppendLine(" WHERE  (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & ") ")
        sqlQuery.AppendLine(" AND (IOS_SQL_KPI.sourcetable = " & Chr(39) & _aliastable & Chr(39) & ") AND (UPPER(IOS_SQL_KPI.KPI_Name) = UPPER(" & Chr(39) & _kpiname & Chr(39) & ")) ")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetSqlElementQuery(ByVal _supportcode As Integer, _tech As String, _aliastable As String, Optional ByVal _kpiname As String = Nothing, Optional _chartTitle As String = Nothing,
                                              Optional _chartSetName As String = "RF", Optional _chartObjectType As String = Nothing, Optional _chartCategory As String = Nothing, Optional _UserName As String = Nothing) As String
        sqlQuery = New StringBuilder()
        If Not _kpiname Is Nothing Then
            sqlQuery.AppendLine("SELECT DISTINCT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object, IOS_SQL_KPI.KPI_Name FROM IOS_Chart_Configuration ")
            sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
            sqlQuery.AppendLine(" WHERE  (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & ") ")
            sqlQuery.AppendLine(" AND (IOS_SQL_KPI.sourcetable = " & Chr(39) & _aliastable & Chr(39) & ") AND (UPPER(IOS_SQL_KPI.KPI_Name) = UPPER(" & Chr(39) & _kpiname & Chr(39) & ")) ")
            'sqlQuery.AppendLine(" And (IOS_Chart_Configuration.ChartSetName = '" & _chartSetName & "') AND (IOS_Chart_Configuration.ObjectTab " & _chartObjectType & ") AND (IOS_Chart_Configuration.CategoryTab " & _chartCategory & ")")
        Else
            'sqlQuery.AppendLine("SELECT DISTINCT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object,IOS_SQL_KPI.KPI_Name FROM IOS_Chart_Configuration ")
            'sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
            'sqlQuery.AppendLine(" WHERE  (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & ") AND (IOS_SQL_KPI.sourcetable = " & Chr(39) & _aliastable & Chr(39) & ") ")
            'sqlQuery.AppendLine(" AND (IOS_Chart_Configuration.ChartTitle " & _chartTitle & ") AND (IOS_Chart_Configuration.ChartSetName = '" & _chartSetName & "') AND (IOS_Chart_Configuration.ObjectTab " & _chartObjectType & ") AND (IOS_Chart_Configuration.CategoryTab " & _chartCategory & ")")
            sqlQuery.AppendLine("SELECT DISTINCT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object,IOS_SQL_KPI.KPI_Name FROM IOS_Chart_Configuration ")
            sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
            sqlQuery.AppendLine(" WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & _tech & Chr(39) & ") AND (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & ") AND (IOS_SQL_KPI.sourcetable = " & Chr(39) & _aliastable & Chr(39) & ") ")
            sqlQuery.AppendLine(" AND (IOS_Chart_Configuration.ChartTitle " & _chartTitle & ") AND ((IOS_Chart_Configuration.ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ")) ")
            'sqlQuery.AppendLine(" OR (" & Chr(39) & _chartSetName & Chr(39) & " = (Select ChartSetName From IOS_Licenses Where LicenseUser = " & Chr(39) & _UserName & Chr(39) & ")")
            sqlQuery.AppendLine(" OR (ChartSetName = " & Chr(39) & _UserName & Chr(39) & " )")
            sqlQuery.AppendLine(" AND (IOS_Chart_Configuration.ObjectTab " & _chartObjectType & ") AND (IOS_Chart_Configuration.CategoryTab " & _chartCategory & ")")
        End If
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetObjectConfigChanges(ByVal sql_conn As String, ByVal _tech As String) As DataTable
        sqlQuery = New StringBuilder()
        If _tech = "ALL" Then
            sqlQuery.AppendLine("SELECT tech, Object, ChangesSQLID 
                                    from dbo.[IOS_Object_Configuration] 
                                    INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                                    where ChangesSQLID is not null and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39))
            'Updated dbo.[IOS_Object_Configuration] table to check Changes tab dynamic buttons appearance. The update queary is below.
            'update dbo.[IOS_Object_Configuration] set ChangesSQLID = 1 Where Tech = 'NSN 3G' and [Object] in ('RNC','WBTS','WCEL')
        Else
            sqlQuery.AppendLine("SELECT tech, Object, ChangesSQLID 
                                    from dbo.[IOS_Object_Configuration] 
                                    INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                                    where ChangesSQLID is not null and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & " and  tech = " & Chr(39) & _tech & Chr(39))
        End If
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetObjectConfigurationNewQuery(ByVal _chartSetName As String, _tech As String, _username As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT techtab,  Categorytabindex, Categorytab, ObjectTab 
                            FROM IOS_Chart_Configuration 
                            left outer join IOS_Object_Configuration on tech=techtab and [Object]=ObjectTab
                            INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile")
        sqlQuery.AppendLine(" WHERE (((IOS_Chart_Configuration.ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") OR (IOS_Chart_Configuration.ChartSetName = " & _username & "))  AND TechTab = " & Chr(39) & _tech & Chr(39) & ") and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39))
        sqlQuery.AppendLine(" GROUP BY techtab, Categorytab, categorytabindex, ObjectTab, loadorder ORDER BY techtab, loadorder desc, categorytabindex")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetAddChartNewQuery(ByVal _chartSetName As String, _tech As String, _username As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT techtab, categorytabindex, categorytab, chartindex, chartname, objecttab, objecttabindex, ChartTitle FROM IOS_Chart_Configuration ")
        sqlQuery.AppendLine(" WHERE (((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & ") OR (ChartSetName = " & _username & ")) AND TechTab = " & Chr(39) & _tech & Chr(39) & ") ")
        sqlQuery.AppendLine(" GROUP BY techtab, categorytabindex,categorytab, chartindex, chartname, objecttab, objecttabindex,ChartTitle ORDER BY techtab, objecttab, objecttabindex, categorytabindex, chartindex,ChartTitle ASC")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetKmlExportFields(ByVal sql_conn As String, _tech As String, _aggr_from As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT [EXPORTFIELDS] FROM [dbo].[IOS_Map_KMLExportConfig] WHERE [IOS_TECH]='" & _tech & "' AND [OBJECTTYPE]='" & _aggr_from & "' ORDER BY [Ordinal]")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartSelectedElementsGroupBy(ByVal sql_conn As String, tech As String, objectType As String, targetType As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT GroupBy FROM  [dbo].[IOS_Chart_GroupBy_Configuration] WHERE IOS_TECH = '" & tech & "' AND ObjectType = '" & objectType & "' AND TargetType= '" & targetType & "' ORDER BY 1")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetKPINameListForKPISet(ByVal sql_conn As String, kpiSetID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select a.[SQLKPIID],b.[KPI_Name] From [dbo].[IOS_CPE_KPISet_Details] a inner join [dbo].[IOS_SQL_KPI] b on a.SQLKPIID = b.SQLKPI_ID where a.KPISetID = " & kpiSetID)
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartAlignIntervalSet(sql_conn As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT [IntervalName],[IntervalValue] FROM [dbo].[IOS_Chart_Configuration_AlignInterval]")
        Return DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
    End Function

#End Region

#Region "PCHR"

    Public Shared Function Get_PCHRProjects_Data(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [ProjectId],[ProjectName],CONVERT(varchar(10),CASE WHEN ParsedFiles IS NULL THEN '0'")
        sqlQuery.AppendLine("ELSE ParsedFiles END ) AS ParsedFiles ,CONVERT(varchar(10),case When TotalFiles Is NULL then")
        sqlQuery.AppendLine("'0' else TotalFiles End) AS TotalFiles,ProjectOwner,ParseStatus from [IOS_PCHR_Projects]")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Sub DeleteProjectPCHR(connStr As String, projectId As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DELETE IOS_PCHR_Projects WHERE ProjectId =" & projectId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

#End Region

#Region "Dialog Chart Category"

    Public Shared Function GetMaxCategoryIndex(ByVal connStr As String, ByVal tech As String, ByVal chartSetName As String, ByVal objectTab As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT MAX(CategoryTabIndex) AS CategoryTabIndex from IOS_Chart_Configuration where TechTab='" & tech & "' and ChartSetName='" & chartSetName & "' and ObjectTab='" & objectTab & "'")
        Return DataAccessorODBC.ExecuteScalar(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Dialog Mapping Selection"

    Public Shared Function GetMapSelectionnOSSUserParams(ByVal connStr As String, ByVal templateID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.qry_IOS_Parameter_Template WHERE TemplateID = " & templateID & " ORDER BY GROUP_NAME ASC, OBJECT ASC, PARAM ASC")
        Return DataAccessorSQL.ExecuteDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetParameterTemplates(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.IOS_Parameters_Templates")
        Return DataAccessorSQL.ExecuteDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Dialog Project Info PCHR"

    Public Shared Function GetPCHRProjectData(ByVal connStr As String, ByVal projectID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM [IOS_PCHR_Projects] WHERE ProjectId=" & projectID)
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Dialog Project Manager PCHR"

    Public Shared Function CreatePCHRProject(ByVal connStr As String, ByVal projectName As String, ByVal isRNCorIMSI As String, ByVal rncValue As String, ByVal imsiValue As String, ByVal rncidfilterValue As String, ByValcellidfilterValue As String,
                                             ByVal startDate As String, ByVal endDate As String, ByVal projectOwner As String, ByVal userLogChecked As Boolean, ByVal SpecLogChecked As Boolean, ByVal CellLogChecked As Boolean) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_PCHR_Projects]([ProjectName],[RNCorIMSI],[RNCValue],[IMSIValue],[RNCIDFilter],[CELLIDFilter],[StartDateTime],[EndDateTime],[ProjectOwner],[ParseStatus],[ParsedFiles],[TotalFiles],[userLogChecked],[SpecLogChecked],[CellLogChecked])")
        sqlQuery.AppendLine("VALUES ('" & projectName & "','" & isRNCorIMSI & "','" & rncValue & "','" & imsiValue & "','" & rncidfilterValue & "','" & ByValcellidfilterValue & "','" & startDate & "','" & endDate & "','" & projectOwner & "',0,0,0,'" & IIf(userLogChecked, 1, 0) & "','" & IIf(SpecLogChecked, 1, 0) & "','" & IIf(CellLogChecked, 1, 0) & "')")
        Return DataAccessorODBC.ExecuteScalar(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "SON Exceptions"

    Public Shared Function InsertJobsExceptions(ByVal connStr As String, ByVal parameters As List(Of Odbc.OdbcParameter)) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO IOS_Jobs_Exceptions (ExceptionTimeStamp, ExceptionExpiryDate, JobId, ExceptionString, rowHash) VALUES (?,?,?,?,?)")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString, parameters)
    End Function

#End Region

#Region "Tags"

    Public Shared Function GetTagsData(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT distinct IOST.TagID,IOST.TagName,IOST.Technology FROM [dbo].[IOS_Tags] IOST INNER JOIN IOS_Tags_Details_Region IOSTD ON IOSTD.TagID = IOST.TagID ORDER BY IOST.TagName ASC")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTagDetailsRegion(ByVal connStr As String, ByVal tagID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT TagDetailsID,IOSTD.TagID,RegionName,CAST(RegionPoly as Geometry).STAsText() as RegionPoly FROM IOS_Tags_Details_Region IOSTD INNER JOIN")
        sqlQuery.AppendLine("(SELECT TagId FROM IOS_Tags WHERE TagId = '" & tagID & "') IOST ON IOSTD.TagID = IOST.TagID")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Tag Manager"

    Public Shared Function GetDistinctObject(ByVal connStr As String, tech As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT object 
                                FROM dbo.IOS_Object_Configuration a 
                                inner join IOS_SQL_Create on Object=Aggregate_From  
                                 INNER JOIN IOS_Licenses on a.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                                where a.tech='" & tech & "' and SQLID is Not NULL  and Purpose = 'Charts'  and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "
                                order by object ")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function RenameTag(ByVal connStr As String, newName As String, tagId As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("update IOS_Tags set TagName = '" & newName & "' where TagID = " & tagId)
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteTag(ByVal connStr As String, tagId As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DELETE FROM IOS_Tags WHERE TagID=" & tagId)
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function RenameTagRegionDetail(ByVal connStr As String, newName As String, id As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("update IOS_Tags_Details_Region set RegionName='" & newName & "' where TagDetailsID='" & id & "';")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteTagRegionDetail(ByVal connStr As String, id As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DELETE FROM [dbo].[IOS_Tags_Details_Region] WHERE TagDetailsID='" & id & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteTagCMDetail(ByVal connStr As String, id As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("delete IOS_Tags_Details_CM where TagDetailsID='" & id & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function InsertTag(ByVal connStr As String, tagName As String, tech As String, tagType As String, tagDescription As String, objType As String, tagIsPrivate As Boolean) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXECUTE [dbo].[sp_Technology_Tag_Insert] '" & tagName & "','" & tech & "','" & tagType & "','" & tagDescription & "','" & objType & "','" & Environment.UserName & "'," & IIf(tagIsPrivate, 1, 0) & "")
        Return CInt(DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString).Rows(0)("TagID"))
    End Function

    Public Shared Function GetInsertTagDetailsCMSQL(_TagID As String, _ParameterID As String, _ParameterName As String, _ParameterOperator As String, _ParameterValue As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_Tags_Details_CM]([TagID],[ParameterID],[ParameterName],[ParameterOperator],[ParameterValue])")
        sqlQuery.AppendLine("VALUES('" & _TagID & "','" & _ParameterID & "','" & _ParameterName & "','" & _ParameterOperator & "','" & _ParameterValue & "');")
        Return sqlQuery.ToString()
    End Function

    Public Shared Function GetUpdateTagDetailsCMSQL(_TagID As String, _ParameterID As String, _ParameterName As String, _ParameterOperator As String, _ParameterValue As String, _TagDetailsID As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Update [dbo].[IOS_Tags_Details_CM] set [TagID]='" & _TagID & "',[ParameterID]='" & _ParameterID & "',[ParameterName]='" & _ParameterName & "',[ParameterOperator]='" & _ParameterOperator & "',[ParameterValue]='" & _ParameterValue & "'")
        sqlQuery.AppendLine("Where TagDetailsID= '" & _TagDetailsID & "';")
        Return sqlQuery.ToString()
    End Function

    Public Shared Function GetDistinctTechnology(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT tech 
                             FROM dbo.IOS_Object_Configuration 
                             INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                             Where SQLID is Not NULL and tech<>'PLMN' and tech<>'Tags' and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39))
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTagList(ByVal connStr As String, chkState As Boolean, _tech As String, _objectType As String, _TagType As String) As DataTable
        sqlQuery = New StringBuilder()
        If chkState = False Then
            sqlQuery.AppendLine("SELECT TagID, TagName, TagDescription,EnablePreAggregation, TagOwner,TagType FROM IOS_Tags where Technology ='" & _tech & "' and ObjectType='" & _objectType & "' and TagType='" & _TagType & "'  order by TagID")
        Else
            sqlQuery.AppendLine("SELECT TagID, TagName, TagDescription,EnablePreAggregation, TagOwner,TagType FROM IOS_Tags where Technology ='" & _tech & "' and ObjectType='" & _objectType & "'  order by TagID")
        End If
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTagListByOwner(ByVal connStr As String) As DataTable
        Return DataAccessorODBC.GetDataTable(connStr, "SELECT TagID, TagName, TagDescription, TagOwner FROM IOS_Tags WHERE TagOwner = '" & Environment.UserName.ToString & "'")
    End Function

    Public Shared Sub InsertTagsDetailsList(ByVal connStr As String, ByVal tagID As Integer, ByVal objectID As String, objectName As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_Tags_Details_List] ([TagID],[ObjectID],[ObjectName])")
        sqlQuery.AppendLine("VALUES (" & tagID & ",'" & objectID & "','" & objectName & "');")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

#End Region

#Region "Parameter Manager"

    Public Shared Function GetParameterCategory(ByVal connStr As String, vendor As String, tech As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("select * from IOS_Parameters_Categories where Vender='" & vendor & "' and Technology='" & tech & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Parameter History"

    Public Shared Function GetTemplateManager(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT ID, P_name,P_abbr_name,Managed_object,Range_Step,Conv_Int_Val,LTRIM(RTRIM(techn)) as techn,LTRIM(RTRIM(NE_release)) as NE_release,EnabledInTemplate,EnabledInCategory,Technology,Vendor FROM dbo.qry_IOS_Parameters where EnabledInTemplate = 1 ORDER BY Techn, P_Abbr_Name")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Parameter Description"

    Public Shared Function GetOSSParamRef(ByVal connStr As String, praramName As String, objName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_OSS_Param_Ref WHERE P_abbr_name = " & Chr(39) & praramName & Chr(39) & " AND Managed_Object = " & Chr(39) & objName & Chr(39))
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Form Notes"

    Public Shared Function DeleteNote(ByVal connStr As String, ByVal NoteID As Integer) As Integer
        Try
            Return DataAccessorODBC.ExecuteNonQuery(connStr, "DELETE FROM dbo.IOS_Note WHERE NotesID = " & NoteID)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Shared Function GetIOSNote(ByVal connStr As String, ByVal noteID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * from dbo.qry_IOS_Notes WHERE NotesID = " & noteID)
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetNoteIdByUsername(ByVal connStr As String, ByVal _username As String) As Integer
        Try
            Return DataAccessorODBC.ExecuteScalar(connStr, "SELECT TOP 1 NotesID FROM dbo.IOS_Note WHERE Username = " & Chr(39) & _username & Chr(39) & " ORDER BY NotesID DESC")
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Shared Function GetNoteDepartments(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_Note_Departments")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetNoteTypes(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_Note_Types")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Chart Customization"

    Public Shared Function GetObjectTabByTechTab(ByVal connStr As String, ByVal techTab As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select distinct ObjectTab, ObjectTabIndex From IOS_Chart_Configuration Where TechTab=" & Chr(39) & techTab & Chr(39) & " And ChartSetName = 'RF' Group By ObjectTab, ObjectTabIndex Order By ObjectTabIndex")
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDataAssignToCustomChart(ByVal connStr As String, ByVal tech As String, ByVal customChartName As String, ByVal chartSetName As String, Optional objectType As String = "") As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * from IOS_Chart_Configuration WHERE ((techtab = " & Chr(39) & tech & Chr(39) & ") AND (Chartname = " & Chr(39) & customChartName & Chr(39) & ") AND ((ChartSetName = " & Chr(39) & chartSetName & Chr(39) & ") OR (ChartSetName = " & Chr(39) & Environment.UserName.ToString & Chr(39) & "))")
        If objectType <> "" Then
            sqlQuery.AppendLine("AND (ObjectTab = " & Chr(39) & objectType & Chr(39) & ")) ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
        Else
            sqlQuery.AppendLine(") ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
        End If
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDistinctCategoryTab(ByVal connStr As String, ByVal tech As String, ByVal chartSetName As String, ByVal objectTab As String, ByVal newCategory As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT CategoryTab FROM IOS_Chart_Configuration WHERE TechTab='" & tech & "' AND ChartSetName='" & chartSetName & "' AND ObjectTab='" & objectTab & "' AND CategoryTab='" & newCategory & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetCustomChartIndex(ByVal connStr As String, ByVal technology As String, ByVal chartSetName As String, ByVal customChartName As String, ByVal category As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DECLARE @Chart_Index AS INTEGER = 0;")
        'sqlQuery.AppendLine("SELECT @Chart_Index = ChartIndex FROM IOS_Chart_Configuration WHERE (TechTab = '" & technology & "') AND (ChartSetName = '" & chartSetName & "') AND")
        'sqlQuery.AppendLine("(ChartName = '" & customChartName & "') AND CategoryTab = '" & category & "';")
        'sqlQuery.AppendLine("DELETE FROM IOS_Chart_Configuration WHERE (TechTab = '" & technology & "') AND (ChartSetName = '" & chartSetName & "') AND")
        'sqlQuery.AppendLine("(ChartName = '" & customChartName & "') AND CategoryTab = '" & category & "';")
        sqlQuery.AppendLine("If ('" & chartSetName & "' <> '" & Environment.UserName.ToString & "')")
        sqlQuery.AppendLine("Begin")
        sqlQuery.AppendLine("Select @Chart_Index = ChartIndex FROM IOS_Chart_Configuration WHERE (TechTab = '" & technology & "') AND (ChartSetName = '" & chartSetName & "') AND")
        sqlQuery.AppendLine("(ChartName = '" & customChartName & "');")
        sqlQuery.AppendLine("DELETE FROM IOS_Chart_Configuration WHERE (TechTab = '" & technology & "') AND (ChartSetName = '" & chartSetName & "') AND")
        sqlQuery.AppendLine("(ChartName = '" & customChartName & "');")
        sqlQuery.AppendLine("End")
        sqlQuery.AppendLine("Else")
        sqlQuery.AppendLine("Begin")
        sqlQuery.AppendLine("Select @Chart_Index = ChartIndex FROM IOS_Chart_Configuration WHERE (TechTab = '" & technology & "') AND (ChartSetName = '" & chartSetName & "') AND")
        sqlQuery.AppendLine("(ChartName = '" & customChartName & "') AND CategoryTab = '" & category & "';")
        sqlQuery.AppendLine("DELETE FROM IOS_Chart_Configuration WHERE (TechTab = '" & technology & "') AND (ChartSetName = '" & chartSetName & "') AND")
        sqlQuery.AppendLine("(ChartName = '" & customChartName & "') AND CategoryTab = '" & category & "';")
        sqlQuery.AppendLine("End")
        sqlQuery.AppendLine("SELECT @Chart_Index;")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function ExecuteSwapCustomChartIndex(ByVal connStr As String, ByVal sourceNodeTag As String, ByVal sourceCategoryTab As String, ByVal sourceCategoryTabIndex As String, ByVal targetNodeTag As String,
                                                       ByVal targateCategoryTab As String, ByVal targateCategoryTabIndex As String, ByVal technology As String, ByVal chartSetName As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Swap_Custom_ChartIndex '" & sourceNodeTag & "','" & sourceCategoryTab & "','" & sourceCategoryTabIndex & "','" & targetNodeTag & "','" & targateCategoryTab & "','" & targateCategoryTabIndex & "','" & technology & "','" & chartSetName & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function ExecuteSwapCustomChartIndex(ByVal connStr As String, ByVal sourceNodeText As String, ByVal sourceNodeTag As String, ByVal targetNodeText As String, ByVal targetNodeTag As String,
                                                       ByVal technology As String, ByVal chartSetName As String) As Integer
        sqlQuery = New StringBuilder()
        'sqlQuery.AppendLine("EXEC IOS_Swap_Custom_CategoryIndex'" & sourceNodeText & "','" & sourceNodeTag & "','" & targetNodeText & "','" & targetNodeTag & "','" & technology & "','" & chartSetName & "'")
        sqlQuery.AppendLine("Update [dbo].[IOS_Chart_Configuration] Set [CategoryTabIndex] = " & targetNodeTag & " Where [TechTab] = '" & technology & "' And [ChartSetName] = '" & chartSetName & "' And [CategoryTab] = '" & sourceNodeText & "';")
        sqlQuery.AppendLine("Update [dbo].[IOS_Chart_Configuration] Set [CategoryTabIndex] = " & sourceNodeTag & " Where [TechTab] = '" & technology & "' And [ChartSetName] = '" & chartSetName & "' And [CategoryTab] = '" & targetNodeText & "';")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function InsertNewChartCategory(ByVal connStr As String, ByVal technology As String, ByVal categoryTabIndex As String, ByVal categoryTab As String,
                                                  ByVal chartSetName As String, ByVal objectTab As String, ByVal objectTabIndex As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO IOS_Chart_Configuration (TechTab, CategoryTabIndex, CategoryTab, ChartIndex, ChartName, ChartTitle, ChartElements, chartElementsType, chartElementsYAxis, chartYaxisScaleProp,")
        sqlQuery.AppendLine("chartY1axisLabels, chartY2axisLabels, chartY1AbsPerc, chartY2AbsPerc, chartY1axisPrecision, chartY2axisPrecision, ChartElementsColor, SQLKPI_ID, Sort_dir, ElmntDisplay, ChartSetName, ObjectTab, ObjectTabIndex)")
        sqlQuery.AppendLine("VALUES ('" & technology & "', '" & categoryTabIndex & "', '" & categoryTab & "', 0, '" & technology & Now.ToString("yyMMddHHmmss") & "',")
        sqlQuery.AppendLine("'Add Chart','','','','','','','','',0,0,0,0,NULL,NULL,'" & chartSetName & "','" & objectTab & "'," & objectTabIndex & ")")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartType(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select TypeIndex,TypeName,KPICount From IOS_ChartType Where IsVisible=1")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetChartSetOwner(ByVal connStr As String, techTab As String, chartSetName As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [Owner] From [dbo].[IOS_Chart_Configuration_ChartSet] Where TechTab = '" & techTab & "' And ChartSetName = '" & chartSetName & "';")
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
        Return dt.Rows(0)("Owner").ToString
    End Function

    Public Shared Function GetChartSetAccessibility(connStr As String, tech As String, chartSetName As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [Accessibility]  FROM [dbo].[IOS_Chart_Configuration_ChartSet]")
        sqlQuery.AppendLine("Where TechTab = '" & tech & "' and ChartSetName = '" & chartSetName & "'")
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
        If dt.Rows.Count > 0 Then Return dt.Rows(0)("Accessibility").ToString Else Return ""
    End Function

#End Region

#Region "Report Edit"

    Public Shared Sub InsertReport(connStr As String, ByVal reportName As String, reportOwner As String, reportGroupId As Integer, reportType As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [IOS_Reports] ([ReportName],[ReportOwner],[ReportLocked],[ReportGroupID],[IsEnabled],[ReportType])")
        sqlQuery.AppendLine("VALUES(" & Chr(39) & reportName.Trim.Substring(0, Math.Min(49, reportName.Trim.Length)) & Chr(39) & "," & Chr(39) & reportOwner.Substring(0, Math.Min(50, reportOwner.Length)).ToLower & Chr(39) & ",'1'," & Chr(39) & reportGroupId & Chr(39) & ",0," & Chr(39) & reportType & Chr(39) & ")")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub UpdateReportSlide(connStr As String, ByVal updateCommand As String, slideId As String, reportId As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_Reports_Slides " & updateCommand & " WHERE SlideID = " & slideId & " AND ReportID=" & reportId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub UpdateReportObject(connStr As String, ByVal updateCommand As String, objectId As String, reportId As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_Reports_Objects " & updateCommand & " WHERE ObjectID = " & objectId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub UpdateReportDetails(connStr As String, ByVal updateCommand As String, reportId As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE [IOS_Reports] " & updateCommand & " WHERE ReportID=" & reportId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function GetReportGroups(connStr As String, ByVal uname As String) As DataTable
        Try
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("SELECT DISTINCT * FROM qry_IOS_ReportGroups WHERE LicenseUser = " & Chr(39) & uname & Chr(39) & " Order by 1")
            Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetAllReport(connStr As String, ByVal uname As String) As DataTable
        Try
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("SELECT DISTINCT * FROM qry_IOS_ReportAll WHERE LicenseUser = " & Chr(39) & uname & Chr(39) & " Order By ReportID, SlideOrdinal")
            Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub InsertReportsObject(connStr As String, ByVal slideID As Integer, objectName As String, objectStyleID As Integer, objecttech As String, objectNameGUI As String,
                                          targetType As String, predefinedTime As String, manualStartTime As Date, manualEndTime As Date, resolution As String, objectsSelected As String,
                                          topXShowObjects As String, topXDeltaInterval As String, counterType As String, aggregateTo As String, tagid As String, tags_Filter As String, purpose As String, topXRowCount As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select (ISNULL(Max(ObjectOrdinal),0) + 1) As ObjectOrdinal From [dbo].[IOS_Reports_Objects] Where [SlideID] = " & slideID & ";")
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)

        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [IOS_Reports_Objects]([SlideID],[ObjectName],[ObjectStyleID],[Technology],[ObjectNameGUI],[TargetType],[PredefinedTime],[ManualStartTime],")
        sqlQuery.AppendLine("[ManualEndTime], [Resolution], [ObjectsSelected], [TopX_ShowObjects], [TopX_DeltaInterval], [CounterType], [AggregateTo], [TagID], [Tags_Filter], [TopXRowCount], [Purpose], [ObjectOrdinal])")
        sqlQuery.AppendLine("VALUES(" & slideID & "," & Chr(39) & objectName & Chr(39) & "," & objectStyleID & "," & Chr(39) & objecttech & Chr(39) & "," & Chr(39) & objectNameGUI & Chr(39) & ",")
        sqlQuery.AppendLine(Chr(39) & targetType & Chr(39) & "," & Chr(39) & predefinedTime & Chr(39) & "," & Chr(39) & manualStartTime & Chr(39) & "," & Chr(39) & manualEndTime & Chr(39) & ",")
        sqlQuery.AppendLine(Chr(39) & resolution & Chr(39) & "," & Chr(39) & objectsSelected & Chr(39) & "," & Chr(39) & topXShowObjects & Chr(39) & "," & Chr(39) & topXDeltaInterval & Chr(39) & "," & Chr(39) & counterType & Chr(39) & ",")
        sqlQuery.AppendLine(Chr(39) & aggregateTo & Chr(39) & "," & Chr(39) & tagid & Chr(39) & "," & Chr(39) & tags_Filter & Chr(39) & "," & topXRowCount & "," & Chr(39) & purpose & Chr(39) & "," & CInt(dt.Rows(0)("ObjectOrdinal")) & ")")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function InsertReportSlide(connStr As String, ByVal reportID As Integer, slideName As String, slideOrdinal As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [IOS_Reports_Slides]([ReportID],[SlideName],[SlideTitle],[SlideText],[SlideStyleID],[SlideOrdinal])")
        sqlQuery.AppendLine("VALUES(" & reportID & "," & Chr(39) & slideName & Chr(39) & "," & Chr(39) & slideName & Chr(39) & ",'',1," & slideOrdinal & ");")
        sqlQuery.AppendLine("Select IDENT_CURRENT('[dbo].[IOS_Reports_Slides]')")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function InsertDashboardSlide(connStr As String, reportID As Integer, ByVal dashboardID As Integer, slideName As String, dbTabPages As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select (ISNULL(Max(SlideOrdinal),0) + 1) As SlideOrdinal From [dbo].[IOS_Reports_Slides] Where [ReportID] = " & reportID & ";")
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)

        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_Reports_Slides] ([ReportID],[SlideName],[SlideStyleID],[SlideOrdinal],[DashboardID],[DashboardTabPages])")
        sqlQuery.AppendLine("VALUES (" & reportID & "," & Chr(39) & slideName & Chr(39) & ",1," & CInt(dt.Rows(0)("SlideOrdinal")) & "," & dashboardID & "," & Chr(39) & dbTabPages & Chr(39) & ");")
        sqlQuery.AppendLine("Select IDENT_CURRENT('[dbo].[IOS_Reports_Slides]')")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetMaxReportSlideID(connStr As String, ByVal reportID As Integer) As Integer
        Return DataAccessorODBC.ExecuteScalar(connStr, "Select MAX(SlideID) as SlideID from IOS_Reports_Slides WHERE ReportID= " & reportID)
    End Function

    Public Shared Function GetSlideOrdinal(connStr As String, ByVal reportID As Integer) As Integer
        Return DataAccessorODBC.ExecuteScalar(connStr, "Select Count(SlideID) As SlideOrdinal from IOS_Reports_Slides Where ReportID=" & reportID) + 1
    End Function

    Public Shared Sub ManageStyle(connStr As String, ByVal slideID As String)
        Try
            DataAccessorODBC.ExecuteNonQuery(connStr, "EXEC IOS_Report_ManageStyle " & slideID)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function GetSlidesWithChartObject(connStr As String, ByVal reportID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select ObjectName,SlideName,SL.SlideID,ObjectType From ")
        sqlQuery.AppendLine("(Select ObjectID,ObjectName,IRS.SlideID,ObjectStyleID from IOS_Reports_Objects IRO RIGHT JOIN ")
        sqlQuery.AppendLine("(Select SlideID from IOS_Reports_Slides Where ReportID=" & reportID & ") IRS On IRS.SlideID=IRO.SlideID")
        sqlQuery.AppendLine(") OBJ ")
        sqlQuery.AppendLine("Left JOIN (Select ObjectStyleID,ObjectType FROM IOS_Reports_ObjectStyles) ")
        sqlQuery.AppendLine("OSL On OSL.ObjectStyleID=OBJ.ObjectStyleID  ")
        sqlQuery.AppendLine("LEFT JOIN IOS_Reports_Slides SL On SL.SlideID=OBJ.SlideID")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetSlidesWithChartObject(connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        'sqlQuery.AppendLine("Select OBJ.ReportName,OBJ.ReportID, ObjectName,SlideName,SL.SlideID,ObjectType From ")
        'sqlQuery.AppendLine("(Select IRS.ReportName,IRS.ReportID,ObjectID,ObjectName,IRS.SlideID,ObjectStyleID from IOS_Reports_Objects IRO RIGHT JOIN ")
        'sqlQuery.AppendLine("(Select tbl1.SlideID,tbl2.ReportName,tbl2.ReportID from IOS_Reports_Slides tbl1 ")
        'sqlQuery.AppendLine("INNER JOIN IOS_Reports tbl2 On tbl1.ReportID=tbl2.ReportID And (ReportOwner = '" & Environment.UserName & "' or ReportLocked = 0)) IRS ON IRS.SlideID=IRO.SlideID) OBJ ")
        'sqlQuery.AppendLine("Left JOIN (Select ObjectStyleID,ObjectType FROM IOS_Reports_ObjectStyles) OSL ON OSL.ObjectStyleID=OBJ.ObjectStyleID  ")
        'sqlQuery.AppendLine("LEFT JOIN IOS_Reports_Slides SL ON SL.SlideID=OBJ.SlideID")
        sqlQuery.AppendLine("SELECT rpt.ReportName,rpt.ReportID, rptobj.ObjectName,rptobj.ObjectNameGUI,SlideName,SL.SlideID,ObjectType,rpt.ReportType From (Select ReportID,ReportName,ReportType From IOS_Reports Where (ReportOwner = '" & Environment.UserName & "' or ReportLocked = 0)) rpt")
        sqlQuery.AppendLine("LEFT JOIN IOS_Reports_Slides SL ON rpt.ReportID = SL.ReportID")
        sqlQuery.AppendLine("LEFT JOIN IOS_Reports_Objects rptobj ON SL.SlideID = rptobj.SlideID")
        sqlQuery.AppendLine("LEFT JOIN IOS_Reports_ObjectStyles objstyle ON rptobj.ObjectStyleId = objstyle.ObjectStyleID Where rpt.ReportType <> 'DashboardPDF';")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Sub UpdateStyleOnObject(connStr As String, ByVal objectID As Integer, ByVal objectStyleID As Integer)
        Try
            DataAccessorODBC.ExecuteNonQuery(connStr, "UPDATE IOS_Reports_Objects SET ObjectStyleID=" & Chr(39) & objectStyleID & Chr(39) & " WHERE ObjectID=" & Chr(39) & objectID & Chr(39) & "")
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub UpdateStyleOnSlide(connStr As String, ByVal reportID As Integer, ByVal slideID As Integer, ByVal objectStyleID As Integer)
        Try
            DataAccessorODBC.ExecuteNonQuery(connStr, "UPDATE IOS_Reports_Slides SET SlideStyleID=" & Chr(39) & objectStyleID & Chr(39) & " WHERE ReportID=" & Chr(39) & reportID & Chr(39) & " AND SlideID=" & Chr(39) & slideID & Chr(39) & "")
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function GetReportProperties(connStr As String, ByVal reportId As Integer) As DataTable
        Try
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("select distinct ReportID,ReportName,ReportOwner,ReportLocked,ReportGroupName,EmailAddress,Interval,StartTime,IsEnabled,ReportType from qry_IOS_ReportAll where ReportID=" & reportId)
            Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetSlideStylePropetiesQuery(ByVal styleID As String, ByVal isBySlide As Boolean) As String
        Try
            sqlQuery = New StringBuilder()
            If (isBySlide) Then
                sqlQuery.AppendLine("Select SlideHeight,SlideWidth,SlideOrientation,SlideOrdinal,SlideText,SlideTitle,RS.[SlideStyleID],RS.SlideID,StyleOwner,RS.SlideName,RS.DashboardID,RS.DashboardTabPages,RS.SelectedPages FROM IOS_Reports_SlideStyles RSS RIGHT JOIN")
                sqlQuery.AppendLine("(Select SlideID,SlideName,SlideOrdinal,SlideText,SlideTitle,[SlideStyleID],[DashboardID],[DashboardTabPages],[SelectedPages] from [IOS_Reports_Slides] WHERE SlideID='" & styleID & "') AS RS on RSS.SlideStyleID=RS.SlideStyleID")
            Else
                sqlQuery.AppendLine("SELECT [SlideStyleID],SlideHeight,SlideWidth,SlideOrientation,StyleOwner FROM [IOS_Reports_SlideStyles] WHERE SlideStyleID=" & styleID)
            End If
            Return sqlQuery.ToString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetChartStylePropetiesQuery(ByVal styleID As String, ByVal isByObject As Boolean) As String
        Try
            sqlQuery = New StringBuilder()
            If (isByObject) Then
                sqlQuery.AppendLine("SELECT RO.[ObjectStyleID], RO.TargetType, RO.PredefinedTime, RO.ManualStartTime, RO.ManualEndTime, RO.Resolution, RO.CounterType, RO.ObjectsSelected, RO.TopX_ShowObjects, RO.TopX_DeltaInterval,")
                sqlQuery.AppendLine("RO.AggregateTo, RO.TagID, RO.Tags_Filter, RO.TopxRowCount, RO.Purpose, [ObjectType], [ObjectStyleName], [ObjectTopMargin], [ObjectLeftMargin], [ObjectScale], Technology, Objectwidth, ObjectHeight, StyleOwner From IOS_Reports_ObjectStyles ROS")
                sqlQuery.AppendLine("RIGHT JOIN (Select ObjectID, Technology, ObjectStyleID, TargetType, PredefinedTime, ManualStartTime, ManualEndTime, Resolution, ObjectsSelected, CounterType, TopX_ShowObjects, TopX_DeltaInterval,")
                sqlQuery.AppendLine("AggregateTo, TagID, Tags_Filter, TopXRowCount, Purpose, ObjectOrdinal From IOS_Reports_Objects Where ObjectID = " & styleID & ") RO ON RO.ObjectStyleID=ROS.ObjectStyleID")
            Else
                sqlQuery.AppendLine("SELECT [ObjectStyleID],[ObjectType],[ObjectStyleName],[ObjectTopMargin],[ObjectLeftMargin],[ObjectScale],Objectwidth,ObjectHeight,StyleOwner From IOS_Reports_ObjectStyles WHERE ObjectType='Chart' AND ObjectStyleID=" & styleID)
            End If
            Return sqlQuery.ToString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetTextStylePropetiesQuery(ByVal styleID As String, ByVal isByObject As Boolean) As String
        Try
            sqlQuery = New StringBuilder()
            If (isByObject) Then
                sqlQuery.AppendLine("SELECT RO.[ObjectStyleID],[ObjectType],[ObjectStyleName],[ObjectTopMargin],[ObjectLeftMargin],[TextBoxBoderColor],[TextBoxBorderSize],[TextBoxText],[TextBoxFontColor],[TextBoxFontSize],[TextBoxFontIsBold],[TextBoxFontIsItalic],[TextBoxFontIsUnderline],[TextBoxFontName],Technology,ObjectWidth,ObjectHeight,StyleOwner From IOS_Reports_ObjectStyles ROS")
                sqlQuery.AppendLine(" RIGHT JOIN (Select ObjectID,Technology,ObjectStyleID From IOS_Reports_Objects WHERE ObjectID='" & styleID & "') RO ON RO.ObjectStyleID=ROS.ObjectStyleID")
            Else
                sqlQuery.AppendLine("SELECT [ObjectStyleID],[ObjectType],[ObjectStyleName],[ObjectTopMargin],[ObjectLeftMargin],[TextBoxBoderColor],[TextBoxBorderSize],[TextBoxText],[TextBoxFontColor],[TextBoxFontSize],[TextBoxFontIsBold],[TextBoxFontIsItalic],[TextBoxFontIsUnderline],[TextBoxFontName],ObjectWidth,ObjectHeight,StyleOwner From IOS_Reports_ObjectStyles WHERE ObjectType='TextBox' AND ObjectStyleID=" & styleID)
            End If
            Return sqlQuery.ToString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetReportsSlideStyles(connStr As String) As DataTable
        Try
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("Select SlideStyleID,SlideStyleName,StyleOwner from IOS_Reports_SlideStyles order by SlideStyleID")
            Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetReportsObjectStyles(connStr As String, objectType As String) As DataTable
        Try
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("Select ObjectStyleID,ObjectStyleName,StyleOwner from [IOS_Reports_ObjectStyles] WHERE ObjectType='" & objectType & "' order by ObjectStyleID")
            Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub InsertSlideStyle(connStr As String, ByVal sytleName As String, _height As Integer, _width As Integer, _orientation As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [IOS_Reports_SlideStyles]([SlideStyleName],[SlideHeight],[SlideWidth],[SlideOrientation],StyleOwner)")
        sqlQuery.AppendLine("VALUES(" & Chr(39) & sytleName & Chr(39) & "," & Chr(39) & _height & Chr(39) & "," & Chr(39) & _width & Chr(39) & "," & Chr(39) & _orientation & Chr(39) & "," & Chr(39) & System.Environment.UserName & Chr(39) & " )")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub UpdateSlideStyle(connStr As String, ByVal sytleId As Integer, _height As Integer, _width As Integer, _orientation As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE [IOS_Reports_SlideStyles] SET [SlideHeight] = " & Chr(39) & _height & Chr(39) & ",[SlideWidth] = " & Chr(39) & _width & Chr(39) & " ,[SlideOrientation] = " & Chr(39) & _orientation & Chr(39) & " WHERE SlideStyleID = " & sytleId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function IsValidSlideStyle(connStr As String, ByVal sytleName As String) As Boolean
        Try
            If (DataAccessorODBC.ExecuteScalar(connStr, "Select Count(*) FROM [IOS_Reports_SlideStyles] WHERE [SlideStyleName] = " & Chr(39) & sytleName & Chr(39) & "") > 0) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Sub InsertChartStyle(connStr As String, ByVal sytleName As String, _ObjectType As String, _Top As Integer, _Left As Integer, _ObjectScale As String, _Width As Integer, _Height As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [IOS_Reports_ObjectStyles]([ObjectStyleName],[ObjectType],[ObjectTopMargin],[ObjectLeftMargin],[ObjectScale],ObjectWidth,ObjectHeight,StyleOwner)")
        sqlQuery.AppendLine("VALUES(" & Chr(39) & sytleName & Chr(39) & "," & Chr(39) & _ObjectType & Chr(39) & "," & Chr(39) & _Top & Chr(39) & "," & Chr(39) & _Left & Chr(39) & "," & Chr(39) & _ObjectScale & Chr(39) & "," & Chr(39) & _Width & Chr(39) & "," & Chr(39) & _Height & Chr(39) & "," & Chr(39) & System.Environment.UserName & Chr(39) & ")")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub UpdateChartStyle(connStr As String, ByVal sytleId As Integer, _ObjectType As String, _Top As Integer, _Left As Integer, _ObjectScale As String, _Width As Integer, _Height As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE [IOS_Reports_ObjectStyles] SET ObjectTopMargin=" & Chr(39) & _Top & Chr(39) & ",[ObjectLeftMargin]=" & Chr(39) & _Left & Chr(39) & ",[ObjectScale]=" & Chr(39) & _ObjectScale & Chr(39) & ",[ObjectWidth]=" & Chr(39) & _Width & Chr(39) & ",[ObjectHeight]=" & Chr(39) & _Height & Chr(39) & " WHERE ObjectStyleID=" & sytleId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function IsObjectStyleValid(connStr As String, ByVal sytleName As String, ByVal objectType As String) As Boolean
        Try
            If (DataAccessorODBC.ExecuteScalar(connStr, "Select count(*) From  [IOS_Reports_ObjectStyles] WHERE [ObjectStyleName]=" & Chr(39) & sytleName & Chr(39) & " AND [ObjectType]=" & Chr(39) & objectType & Chr(39) & "") > 0) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Sub InsertTextBoxStyle(connStr As String, ByVal sytleName As String, _ObjectType As String, _Top As Integer, _Left As Integer, _BorderColorName As String, _BorderSize As String, _TextBoxText As String, _FontColorName As String, _FontSize As Integer, _IsBold As Boolean, _IsItalic As Boolean, _IsUnderline As Boolean, _FontName As String, _Width As Integer, _Height As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [IOS_Reports_ObjectStyles]([ObjectStyleName],[ObjectType],[ObjectTopMargin],[ObjectLeftMargin],[TextBoxBoderColor],[TextBoxBorderSize],[TextBoxText],[TextBoxFontColor],[TextBoxFontSize],[TextBoxFontIsBold],[TextBoxFontIsItalic],[TextBoxFontIsUnderline],[TextBoxFontName],ObjectWidth,ObjectHeight,StyleOwner)")
        sqlQuery.AppendLine("VALUES(" & Chr(39) & sytleName & Chr(39) & "," & Chr(39) & _ObjectType & Chr(39) & "," & Chr(39) & _Top & Chr(39) & "," & Chr(39) & _Left & Chr(39) & "," & Chr(39) & _BorderColorName & Chr(39) & "," & Chr(39) & _BorderSize & Chr(39) & "," & Chr(39) & _TextBoxText & Chr(39) & "," & Chr(39) & _FontColorName & Chr(39) & "," & Chr(39) & _FontSize & Chr(39) & "," & Chr(39) & _IsBold & Chr(39) & "," & Chr(39) & _IsItalic & Chr(39) & "," & Chr(39) & _IsUnderline & Chr(39) & "," & Chr(39) & _FontName & Chr(39) & "," & Chr(39) & _Width & Chr(39) & "," & Chr(39) & _Height & Chr(39) & "," & Chr(39) & System.Environment.UserName & Chr(39) & ")")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub UpdateTextBoxStyle(connStr As String, ByVal sytleId As Integer, _Top As Integer, _Left As Integer, _BorderColorName As String, _BorderSize As String, _TextBoxText As String, _FontColorName As String, _FontSize As Integer, _IsBold As Boolean, _IsItalic As Boolean, _IsUnderline As Boolean, _FontName As String, _Width As Integer, _Height As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE [IOS_Reports_ObjectStyles] SET ObjectTopMargin=" & Chr(39) & _Top & Chr(39) & ",[ObjectLeftMargin]=" & Chr(39) & _Left & Chr(39) & ",[ObjectWidth]=" & Chr(39) & _Width & Chr(39) & ",[ObjectHeight]=" & Chr(39) & _Height & Chr(39) & ",")
        sqlQuery.AppendLine("[TextBoxBoderColor]=" & Chr(39) & _BorderColorName & Chr(39) & ",[TextBoxBorderSize]=" & Chr(39) & _BorderSize & Chr(39) & ",[TextBoxText]=" & Chr(39) & _TextBoxText & Chr(39) & ",[TextBoxFontColor]=" & Chr(39) & _FontColorName & Chr(39) & ",[TextBoxFontSize]=" & Chr(39) & _FontSize & Chr(39) & ",[TextBoxFontIsBold]=" & Chr(39) & _IsBold & Chr(39) & ",[TextBoxFontIsItalic]=" & Chr(39) & _IsItalic & Chr(39) & ",[TextBoxFontIsUnderline]=" & Chr(39) & _IsUnderline & Chr(39) & ",[TextBoxFontName]=" & Chr(39) & _FontName & Chr(39) & "")
        sqlQuery.AppendLine(" WHERE ObjectStyleID=" & sytleId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function GetSlidesByReportID(connStr As String, ByVal reportID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select * from [IOS_Reports_ObjectStyles] ROS RIGHT Join(")
        sqlQuery.AppendLine(" Select ObjectID,ObjectName,ObjectStyleID,Technology,Purpose,ObjectOrdinal,TopX_DeltaInterval, RSSS.SlideID,ReportID,SlideName,SlideTitle,SlideHeight,SlideText,SlideWidth,SlideOrdinal,SlideOrientation from [IOS_Reports_Objects] RO ")
        sqlQuery.AppendLine(" INNER Join")
        sqlQuery.AppendLine(" ( Select SlideID,ReportID,SlideName,SlideTitle,SlideHeight,SlideText,SlideWidth,SlideOrdinal,SlideOrientation from IOS_Reports_SlideStyles RSS LEFT JOIN ")
        sqlQuery.AppendLine(" [IOS_Reports_Slides] RS ON RS.SlideStyleID = RSS.SlideStyleID where ReportId=" & reportID & " )")
        sqlQuery.AppendLine(" RSSS ON RSSS.SlideID = RO.SlideID")
        sqlQuery.AppendLine(" ) ROSSS ON ROSSS.ObjectStyleID=ROS.ObjectStyleID order by SlideOrdinal")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Sub UpdateReportSlideName(connStr As String, ByVal slideId As String, _slideName As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_Reports_Slides SET SlideName = '" & _slideName & "' WHERE SlideID =" & slideId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub DeleteReportSlideName(connStr As String, ByVal slideId As String, slideParentId As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Reports_SlideDelete " & slideId & "," & slideParentId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub MoveReportSlide(connStr As String, ByVal slideId As String, slideParentId As String, nodeId As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Slide_MoveUpDown '" & slideParentId & "','" & slideId & "','" & nodeId & "'")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub RenameReportObject(connStr As String, ByVal _NewName As String, _ObjectID As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE [IOS_Reports_Objects] SET [ObjectName] ='" & _NewName & "' WHERE ObjectID =" & _ObjectID)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub DeleteChartObject(connStr As String, _slideId As Integer, _objectID As Integer)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC [dbo].[IOS_Reports_ObjectDelete] " & _slideId & "," & _objectID)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Sub DeleteReport(connStr As String, _ReportId As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC IOS_Reports_Delete " & _ReportId)
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function GetPredefinedPeriodForChart(ByVal sql_conn As String, ByVal preDefinedTime As String) As DataTable
        Try
            sqlQuery = New StringBuilder()
            sqlQuery.AppendLine("Select [SQL] From dbo.IOS_PreDefinedPeriod Where [GUIText]='" & preDefinedTime & "' And [Control]='cmbPredefTimeStats'")
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(sql_conn, sqlQuery.ToString)
            Return DataAccessorODBC.GetDataTable(sql_conn, dt.Rows(0)("SQL").ToString)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetChartKPISQL(_tech As String, _supportcode As String, Optional ByVal _chartname As String = Nothing) As String
        sqlQuery = New StringBuilder()
        'If _chartname = Nothing Then
        '    sqlQuery.AppendLine("SELECT DISTINCT Cast(IOS_SQL_KPI.KPI_SQL as NVarchar(Max)) FROM IOS_Chart_Configuration ")
        '    sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID WHERE (IOS_Chart_Configuration.TechTab = '" & "TopX_" & _tech & "') AND ")
        '    sqlQuery.AppendLine(" (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & " AND CategoryTab " & _selected_tabs & " AND ChartTitle " & _selected_charts & " ) ")
        'Else
        sqlQuery.AppendLine("SELECT DISTINCT Cast(IOS_SQL_KPI.KPI_SQL as NVarchar(Max)) FROM IOS_Chart_Configuration ")
        sqlQuery.AppendLine(" INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID ")
        sqlQuery.AppendLine(" WHERE (IOS_Chart_Configuration.ChartName = " & Chr(39) & _chartname & Chr(39) & " ) AND (IOS_Chart_Configuration.TechTab = '" & "TopX_" & _tech & "') AND ")
        sqlQuery.AppendLine(" (IOS_SQL_KPI.supportcode > " & _supportcode - 1 & ") ")
        'End If
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetConstructStatsSQLObjectTime(_tech As String, _purpose As String, _aggr_to As String, _aggr_from As String, _objtype As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM qry_IOS_ConstructStatSQL WHERE (((tech)=" & Chr(39) & _tech & Chr(39) & ") AND ((Purpose)=" & Chr(39) & _purpose & Chr(39) & ") AND ")
        sqlQuery.AppendLine(" ((Aggregate_to)=" & Chr(39) & _aggr_to & Chr(39) & ") AND ((Aggregate_From) LIKE " & Chr(39) & _aggr_from & Chr(39) & ") AND ((ObjectType)=" & Chr(39) & _objtype & Chr(39) & "))")
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetTopXChartConfigurationSQL(_tech As String, _chartSetName As String, _chartName As String, _username As String, Optional ByVal _chartname_original As String = Nothing) As String
        sqlQuery = New StringBuilder()
        If _chartname_original Is Nothing Then
            sqlQuery.AppendLine("SELECT * from IOS_Chart_Configuration ")
            'sqlQuery.AppendLine("WHERE (((ChartSetName = " & Chr(39) & _chartSetName & Chr(39) & "))  AND TechTab = " & Chr(39) & _tech & Chr(39) & " AND ")
            sqlQuery.AppendLine("WHERE ChartName ='" & _chartName & "' AND TechTab = " & Chr(39) & _tech & Chr(39) & "  ")   'OR (ChartSetName = " & _username & "  AND ChartTitle ='" & _chartName & "' ))
            sqlQuery.AppendLine("ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
        Else
            sqlQuery.AppendLine("SELECT * from IOS_Chart_Configuration ")
            'sqlQuery.AppendLine("WHERE (TechTab = " & Chr(39) & _tech & Chr(39) & " AND ChartName = " & Chr(39) & _chartname_original.Substring(0, Len(_chartname_original) - Len("_" & Replace(_tech, "TopX_", ""))) & Chr(39) & ") ")
            sqlQuery.AppendLine("WHERE (TechTab = " & Chr(39) & _tech & Chr(39) & " AND ChartName = " & Chr(39) & _chartName & Chr(39) & ") ")
            sqlQuery.AppendLine("ORDER BY techtab, categorytabindex, chartindex, chartelementid ASC")
        End If
        Return sqlQuery.ToString
    End Function

    Public Shared Function GetObjectStyleIDForExcelReport(connStr As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [ObjectStyleID] From [dbo].[IOS_Reports_ObjectStyles] Where [ObjectStyleName] = 'DefaultChartStyleExcel'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString).Rows(0)("ObjectStyleID")
    End Function

    Public Shared Sub MoveSlideObject(connStr As String, ByVal slideId As String, objectId As String, nodeId As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXEC [dbo].[IOS_SlideObject_MoveUpDown] '" & slideId & "','" & objectId & "','" & nodeId & "'")
        DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Sub

    Public Shared Function GetDashboardFileFromID(connStr As String, dashboardID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [DashboardFile]")
        sqlQuery.AppendLine("From [dbo].[IOS_Dashboards] Where [DashboardID] = " & dashboardID & ";")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDashboardFromID(connStr As String, reportID As Integer, dashboardID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select DB.[DashboardName],DB.[DashboardFile], RS.SelectedPages ")
        sqlQuery.AppendLine("From [dbo].[IOS_Dashboards] DB Inner Join [dbo].[IOS_Reports_Slides] RS On DB.DashboardID = RS.DashboardID")
        sqlQuery.AppendLine("Where RS.ReportID = " & reportID & " And DB.[DashboardID] = " & dashboardID & ";")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetReportObjects(connStr As String, reportID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select Distinct o.Technology, o.TargetType, o.Resolution, o.ObjectsSelected")
        sqlQuery.AppendLine("From [IOS_Server].[dbo].[IOS_Reports_Objects] o")
        sqlQuery.AppendLine("inner join [IOS_Server].[dbo].[IOS_Reports_Slides] s on o.SlideID = s.SlideID")
        sqlQuery.AppendLine("inner join [IOS_Server].[dbo].[IOS_Reports] r on s.ReportID = r.ReportID")
        sqlQuery.AppendLine("Where r.ReportID = '" & reportID & "';")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Events"

    Public Shared Function GetEventMessageList(ByVal connStr As String, ByVal eventID As String, ByVal dtID As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT RTRIM(LTRIM(MSG)) AS MSG, TableName FROM (SELECT '2G' AS TableName, MsgL3 AS Msg FROM dbo.DT_Events_Raw2G_Msg INNER JOIN dbo.DT_Events ON dbo.DT_Events.SessionID = dbo.DT_Events_Raw2G_Msg.SessionID")
        sqlQuery.AppendLine("WHERE dbo.DT_Events.eventid = " & eventID & " AND dbo.DT_Events_Raw2G_Msg.dtid = " & dtID & " UNION ALL SELECT '3G' AS TableName, MsgRRC AS Msg FROM dbo.DT_Events_Raw3G_Msg INNER JOIN dbo.DT_Events on")
        sqlQuery.AppendLine("dbo.DT_Events.SessionID = dbo.DT_Events_Raw3G_Msg.SessionID where dbo.DT_Events.eventid = " & eventID & " AND dbo.DT_Events_Raw3G_Msg.dtid = " & dtID & " UNION All SELECT '4G' AS TableName, MsgRRC AS Msg FROM")
        sqlQuery.AppendLine("dbo.DT_Events_Raw4G_Msg INNER JOIN dbo.DT_Events ON dbo.DT_Events.SessionID = dbo.DT_Events_Raw4G_Msg.SessionID WHERE dbo.DT_Events.eventid = " & eventID & " AND dbo.DT_Events_Raw4G_Msg.dtid = " & dtID & ") AS T1")
        sqlQuery.AppendLine("WHERE Msg IS NOT NULL ORDER BY MSG")
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetDriveTestData(ByVal connStr As String, ByVal dtID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DtID, DriveTestName, DriveTest FROM dbo.DT_List WHERE Dtid = " & dtID & "")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "ICM"

    Public Shared Function SaveFeedback(ByVal connStr As String, ByVal comment As String, ByVal approveStatus As String, ByVal cellName As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_ICM SET Comment='" & comment & "', Approved='" & approveStatus & "' WHERE cellName='" & cellName & "'")
        Return DataAccessorSQL.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetICMData(ByVal connStr As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM dbo.IOS_ICM;")
        sqlQuery.AppendLine("SELECT ICMCon.ID_ICMConfig,ICMCon.GUIColumn,ICMCon.DBColumn,ICMCon.CategoryID,ICMCat.Category,ICMCat.ShortedName,ICMCon.SortBy,ICMCat.IsActive FROM dbo.IOS_ICM_Configuration ICMCon INNER JOIN")
        sqlQuery.AppendLine("IOS_ICM_Category ICMCat ON ICMCat.CategoryID = ICMCon.CategoryID WHERE ICMCat.IsActive = 1")
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTop1LayerColumn2Tree(ByVal connStr As String, ByVal vendor As String, ByVal technology As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT TOP 1 LayerColumn2Tree FROM dbo.IOS_Map_Configuration where LayerVendor = '" & vendor & "' and LayerTechnology = '" & technology & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetKPIFilterTemplateData(ByVal connStr As String, ByVal tech As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_ICM_Filters_Templates WHERE (Tech ='" & tech & "')")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function InsertIcmKpiFilter(ByVal connStr As String, ByVal templateID As String, ByVal icmConfigID As String, ByVal guiColumn As String,
                                              ByVal icmOperator As String, ByVal icmValue As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO IOS_ICM_Filters(ICMFilterTemplateID, ID_ICMConfig, GUIColumn, ICM_Operator, ICM_Value)")
        sqlQuery.AppendLine("VALUES(" & templateID & "," & icmConfigID & ", '" & guiColumn & "', '" & icmOperator & "','" & icmValue & "')")
        Return DataAccessorSQL.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function UpdateIcmKpiFilter(ByVal connStr As String, ByVal templateID As String, ByVal icmConfigID As String, ByVal guiColumn As String,
                                              ByVal icmOperator As String, ByVal icmValue As String, ByVal icmFilterID As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_ICM_Filters SET ICMFilterTemplateID =" & templateID & ", ID_ICMConfig =" & icmConfigID & ", GUIColumn ='" & guiColumn & "', ICM_Operator ='" & icmOperator & "',")
        sqlQuery.AppendLine("ICM_Value ='" & icmValue & "' WHERE ICMFilterID = " & icmFilterID)
        Return DataAccessorSQL.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteIcmKpiFilter(ByVal connStr As String, ByVal icmFilterID As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DELETE FROM IOS_ICM_Filters WHERE ICMFilterID = " & icmFilterID)
        Return DataAccessorSQL.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetIcmFilters(ByVal connStr As String, ByVal tech As String, Optional ByVal icmFilterTemplateID As String = Nothing) As DataTable
        sqlQuery = New StringBuilder()
        If icmFilterTemplateID Is Nothing Then
            sqlQuery.AppendLine("SELECT * FROM IOS_ICM_Filters WHERE ICMFilterTemplateID in (SELECT ICMFilterTemplateID FROM IOS_ICM_Filters_Templates where Tech='" & tech & "')")
        Else
            sqlQuery.AppendLine("SELECT * FROM IOS_ICM_Filters WHERE ICMFilterTemplateID in (SELECT ICMFilterTemplateID FROM IOS_ICM_Filters_Templates where Tech='" & tech & "') AND ICMFilterTemplateID=" & icmFilterTemplateID)
        End If
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function UpdateRecommendation(ByVal connStr As String, ByVal recommendation As String, ByVal cellName As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_ICM SET Recommendation='" & recommendation & "' WHERE CellName='" & cellName & "'")
        Return DataAccessorSQL.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function UpdateIcmFilters(ByVal connStr As String, ByVal sqlUpdate As String, ByVal icmFilterID As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_ICM_Filters SET " & sqlUpdate & " WHERE ICMFilterID =" & icmFilterID)
        Return DataAccessorSQL.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "KPI Manage"

    Public Shared Function DeleteSqlKPI(ByVal connStr As String, ByVal sqlKpiID As String, ByVal userName As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DELETE IOS_SQL_KPI WHERE SQLKPI_ID='" & sqlKpiID & "' AND Creator='" & userName & "'")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetSqlQueryToAddNewKpi(ByVal techName As String, ByVal newKPIName As String, objectName As String, userName As String, technology As String, objectItem As String, kpiDesc As String) As String
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO IOS_SQL_KPI (tech,sourcetable,tablealias,supportcode,KPI_Name,KPI_SQL,JoinObjects,Object,Creator,Active,Description)")
        sqlQuery.AppendLine("VALUES ('" & techName & "','','',0,'" & newKPIName & "','','','" & objectName & "','" & userName & "',1,'" & Replace(kpiDesc, "'", "`") & "');")
        sqlQuery.AppendLine("SELECT DISTINCT KPI_Name,SQLKPI_ID,Creator FROM IOS_SQL_KPI WHERE Tech ='" & technology & "' AND Object='" & objectItem & "'")
        Return sqlQuery.ToString
    End Function

    Public Shared Function AddNewKpiAndGetList(ByVal connStr As String, ByVal sqlQuery As String) As DataTable
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery)
    End Function

    Public Shared Function UpdateSqlKpi(ByVal connStr As String, ByVal newKPI As String, ByVal formula As String, ByVal kpiID As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_SQL_KPI SET KPI_Name='" & newKPI & "', KPI_SQL='" & formula & "' WHERE SQLKPI_ID='" & kpiID & "';")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetSqlKpiListFromTech(ByVal connStr As String, ByVal tech As String, ByVal obj As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT sourcetable, tablealias, JoinObjects FROM [dbo].[IOS_SQL_KPI] WHERE tech = '" & tech & "' AND Object = '" & obj & "' AND sourcetable <> ''")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function UpdateSqlKpiAsCommitted(ByVal connStr As String, ByVal tableCount As String, ByVal technology As String, ByVal tableNamesOriginal As String, ByVal tableAlias As String,
                                                   ByVal kpiName As String, ByVal KpiSQL As String, ByVal JoinObject As String, ByVal objectItem As String, ByVal userName As String,
                                                   ByVal kpiID As String, ByVal kpiDesc As String) As Integer
        sqlQuery = New StringBuilder()
        If (tableCount > 1) Then
            sqlQuery.AppendLine("UPDATE [dbo].[IOS_SQL_KPI] SET [tech]='" & technology & "',[sourcetable]='" & tableNamesOriginal & "',[tablealias]='" & tableAlias.TrimEnd(",") & "',[supportcode]=1,")
            sqlQuery.AppendLine("[KPI_Name]='" & kpiName & "',[KPI_SQL]='" & Replace(KpiSQL, "'", "''") & "',[JoinObjects]='" & JoinObject & "',[Object]='" & objectItem & "',")
            sqlQuery.AppendLine("[Creator]='" & userName & "',[Active]=1,[Description]='" & Replace(kpiDesc, "'", "`") & "' WHERE [tech]='" & technology & "' AND [SQLKPI_ID]='" & kpiID & "'")
        Else
            sqlQuery.AppendLine("UPDATE [dbo].[IOS_SQL_KPI] SET [tech]='" & technology & "',[sourcetable]='" & tableNamesOriginal & "',[tablealias]='" & tableAlias.TrimEnd(",") & "',[supportcode]=1,")
            sqlQuery.AppendLine("[KPI_Name]='" & kpiName & "',[KPI_SQL]='" & Replace(KpiSQL, "'", "''") & "',[JoinObjects]='',[Object]='" & objectItem & "',")
            sqlQuery.AppendLine("[Creator]='" & userName & "',[Active]=1,[Description]='" & Replace(kpiDesc, "'", "`") & "' WHERE [tech]='" & technology & "' AND [SQLKPI_ID]='" & kpiID & "'")
        End If
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTechOtherThanPLMN(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT Tech 
                             FROM IOS_Object_Configuration 
                             INNER JOIN IOS_Licenses on IOS_Object_Configuration.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile
                             WHERE Tech != 'PLMN' and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39))
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetObjectTypeFromTech(ByVal connStr As String, ByVal techName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT a.ObjectType object 
                             FROM dbo.[IOS_SQL_Create] a 
                             INNER JOIN dbo.[IOS_Object_Configuration] b ON a.tech=b.tech AND a.ObjectType = b.[Object]
                             INNER JOIN IOS_Licenses on b.ObjectConfigProfile = IOS_Licenses.ObjectConfigProfile")
        sqlQuery.AppendLine("WHERE a.purpose IN('Charts','TopX') and IOS_Licenses.LicenseUser = " & Chr(39) & Environment.UserName.ToString & Chr(39) & " AND a.tech = '" & techName & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetKpiOnSelectObject(ByVal connStr As String, ByVal techName As String, ByVal objectName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT KPI_Name,SQLKPI_ID,Creator FROM IOS_SQL_KPI WHERE (Tech ='" & techName & "' AND object='" & objectName & "') AND (KPI_Name IS NOT NULL AND SQLKPI_ID IS NOT NULL) ORDER BY 1")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTableCounterOnSelectObject(ByVal connStr As String, ByVal techName As String, ByVal objectName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT CT.InternalTableID AS InternalTableID,TableName,CounterName,TableKeys,ConnectionString,[DataBase],TableAlias,megaQuery,VendorID FROM IOS_CounterTables CT LEFT JOIN IOS_Countertables_Details CTD")
        sqlQuery.AppendLine("ON CTD.InternalTableID = CT.InternalTableID WHERE Technology='" & techName & "' AND object='" & objectName & "' ORDER BY TableName DESC,CounterName DESC")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetKpiSqlFormula(ByVal connStr As String, ByVal sqlKpiID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT KPI_SQL,TableName AS sourcetable, TableAlias, [Description] FROM qry_KPItoCountersNoCharts WHERE SQLKPI_ID = '" & sqlKpiID & "' ")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "KPI Note"

    Public Shared Function GetKpiInfo(ByVal connStr As String, ByVal kpiID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT [SQLKPI_ID],[tech],[sourcetable],[tablealias],[KPI_Name],[KPI_SQL],[Object],[description] FROM [dbo].[IOS_SQL_KPI] WHERE [SQLKPI_ID]='" & kpiID & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetKpiUsageForChartsAndCounter(ByVal connStr As String, ByVal kpiID As String) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT categorytab, charttitle FROM IOS_Chart_configuration CC INNER JOIN IOS_SQL_KPI SK ON CC.[SQLKPI_ID] = SK.[SQLKPI_ID] WHERE SK.[SQLKPI_ID] = " & kpiID & ";")
        sqlQuery.AppendLine("SELECT DISTINCT tablename, countername FROM qry_KPItoCounters WHERE [SQLKPI_ID] = " & kpiID & ";")
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteKpiNotes(ByVal connStr As String, ByVal noteID As String, ByVal userName As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DELETE FROM IOS_KPI_Notes_Relations WHERE NoteID = (SELECT NoteID FROM IOS_KPI_Notes WHERE NoteID = " & noteID & " AND NoteOwner = '" & userName & "');")
        sqlQuery.AppendLine("DELETE FROM IOS_KPI_Notes WHERE NoteID = " & noteID & " AND NoteOwner = '" & userName & "';")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetKpiOnSelectObject(ByVal connStr As String, ByVal techName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT DISTINCT KPI_Name, SQLKPI_ID, Creator FROM IOS_SQL_KPI WHERE Tech ='" & techName & "'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function IsNoteValidForKPI(ByVal connStr As String, ByVal kpiID As String, ByVal noteID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM [dbo].[IOS_KPI_Notes] WHERE KPIID=" & kpiID & " AND  NoteID=" & noteID)
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function UpdateKPIRelationType(ByVal connStr As String, ByVal relationType As String, ByVal relationID As String, ByVal kpiIdRelation As String, ByVal kpiID As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("UPDATE IOS_KPI_Notes_Relations SET RelationType='" & relationType & "' WHERE RelationID=" & relationID & " AND kPIID_Relation=" & kpiIdRelation & " AND KPIID=" & kpiID)
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteKPIRelationType(ByVal connStr As String, ByVal relationID As String, ByVal kpiIdRelation As String, ByVal kpiID As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("DELETE IOS_KPI_Notes_Relations WHERE RelationID=" & relationID & " AND KPIID=" & kpiID & " AND kPIID_Relation=" & kpiIdRelation)
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function AddKPIRelationType(ByVal connStr As String, ByVal relationType As String, ByVal kpiIdRelation As String, ByVal kpiID As String, ByVal noteID As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_KPI_Notes_Relations] ([KPIID],[KPIID_Relation],[RelationType],[NoteID]) VALUES(" & kpiID & "," & kpiIdRelation & ",'" & relationType & "','" & noteID & "')")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetKPIRelationByKpiID(ByVal connStr As String, ByVal kpiID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT [KPIID],[KPIID_Relation],[RelationType] FROM [dbo].[IOS_KPI_Notes_Relations] WHERE KPIID=" & kpiID)
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetKPIRelationDataToBind(ByVal connStr As String, ByVal kpiID As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT RelationID,KPIID,KPIID_Relation,RelationType,KPI.KPI_Name FROM IOS_KPI_Notes_Relations KPIRel")
        sqlQuery.AppendLine("LEFT JOIN [IOS_SQL_KPI] KPI ON KPI.SQLKPI_ID = KPIRel.KPIID_Relation WHERE KPIRel.KPIID = " & kpiID)
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Anomaly Detection"

    Public Shared Function GetAlertObjectMappingToPM(ByVal connStr As String, ByVal alertIds As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select T5.VendorTech As Tech,T5.VendorObject As CounterType,T5.VendorAggFrom,T5.VendorAggTo As TargetType,T4.OBJECTNAME")
        sqlQuery.AppendLine("From [data_Common].[dbo].ANO_ALERTS_DETECTED T1")
        sqlQuery.AppendLine("INNER Join [data_Common].[dbo].ANO_ALERTS_RULESDETAILS T2 ON T1.ALERT_RULEID=T2.ALERT_RULEID")
        sqlQuery.AppendLine("INNER Join [data_Common].[dbo].ANO_KPI_RULES T3 ON T3.KPI_RULEID = T2.KPI_RULEID")
        sqlQuery.AppendLine("INNER Join [data_Common].[dbo].ANO_KPI_DETECTED T4 ON T3.KPI_RULEID = T4.KPI_RULEID")
        sqlQuery.AppendLine("INNER Join [data_Common].[dbo].[ANO_VendorTech_Mapping] T5 ON T5.CommonObject=T3.ObjectType And T5.CommonReportedObject = T3.ObjectReported And Replace(T5.CommonTech, ' ', '_') = Replace(T3.Technology,' ', '_')")
        sqlQuery.AppendLine("Where T1.ALERT_RULEID IN " & alertIds & "")
        sqlQuery.AppendLine("GROUP BY T5.VendorTech, T5.VendorObject, T5.VendorAggFrom, T5.VendorAggTo, T4.OBJECTNAME")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Capacity"

    Public Shared Function GetMaxEvalWindowForCongestionJob(ByVal connStr As String, ByVal _jobID As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT MAX(EvalWindowDays) MaxEvalWindow FROM [dbo].[IOS_Capacity_CongestionRules] WHERE [CapJobiD] = " & _jobID & "  GROUP BY [CapJobiD]")
        Return CInt(DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString).Rows(0)("MaxEvalWindow"))
    End Function

    Public Shared Function GetMaxEvalWindowForCongestionRule(ByVal connStr As String, ByVal _ruleID As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select MAX(EvalWindowDays) MaxEvalWindow FROM [dbo].[IOS_Capacity_CongestionRules] WHERE [CapCongestionRuleID] = " & _ruleID & " GROUP BY [CapCongestionRuleID]")
        Return CInt(DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString).Rows(0)("MaxEvalWindow"))
    End Function

    Public Shared Function GetCapacityJobList(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [CapJobID],[CapJobName] From [dbo].[IOS_Capacity_Jobs];")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Tilt Manager"

    Public Shared Function GetSectorListForSelectedCells(ByVal connStr As String, ByVal selectedCells As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select Distinct [MBTSNAME],[SectorID] From dbo.IOS_Network_All Where CELLNAME " & selectedCells & " Order By [MBTSNAME],[SectorID]")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function LoadData_Into_Tilt_Manual_Input(ByVal connStr As String, ByVal campaignID As String, ByVal technology As String, ByVal locationID As String, ByVal neName As String, ByVal MBTSNAME As String, ByVal sectorID As String,
                                                           ByVal antennaType As String, ByVal antennaBand As String, ByVal azimuth As String, ByVal mTilt As String, ByVal deviceName As String, ByVal deviceNo As String, ByVal iosLayer As String, ByVal cellName As String, ByVal cellID As String,
                                                           ByVal includeInPlan As String, ByVal tiltRule As String, ByVal vBeamAngle As String, ByVal eTiltCurrent As String, ByVal eTiltPlanned As String, ByVal x As String, ByVal y As String, ByVal radiationCenter As String, ByVal devicelinkedto As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Insert Into data_Common.dbo.TILT_Manual_Input")
        sqlQuery.AppendLine("([CampaignID],[TECHNOLOGY],[LOCATIONID],[NENAME],[MBTSNAME],[SECTORID],[ANTENNATYPE],[ANTENNABAND],[AZIMUTH],[MTILT],[DEVICENAME],[DEVICENO],[IOS_LAYER],[CELLNAME],[CELLID],[IncludeInPlan],[TiltRule],[VBeamAngle],[ETILT_Current],[ETILT_Planned],[X],[Y],[RADIATIONCENTER],[devicelinkedto])")
        sqlQuery.AppendLine("Values")
        sqlQuery.AppendLine("(" & campaignID & ",'" & technology & "','" & locationID & "','" & neName & "','" & MBTSNAME & "'," & sectorID & ",'" & antennaType & "','" & antennaBand & "'," & azimuth & "," & mTilt & ",'" & deviceName & "'," & deviceNo & ",'" & iosLayer & "','" & cellName & "','" & cellID & "','" & includeInPlan & "','" & tiltRule & "'," & vBeamAngle & "," & eTiltCurrent & "," & eTiltPlanned & "," & x & "," & y & "," & radiationCenter & ",'" & devicelinkedto & "')")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTiltCampaigns(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select [CampaignID],[CampaignName] From [data_Common].[dbo].[Tilt_Campaigns] Where ([campaignOwner] = '" & Environment.UserName & "' Or [Ispublic] = 1) And [CampaignEnabled] = 1 And CampaignType = 'MANUAL';")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function AddManualTiltCampaign(ByVal connStr As String, ByVal campaignName As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("INSERT INTO [data_Common].[dbo].[TILT_Campaigns]")
        sqlQuery.AppendLine("([CampaignName],[CampaignType],[CampaignOwner],[CampaignEnabled],[IsPublic])")
        sqlQuery.AppendLine("VALUES")
        sqlQuery.AppendLine("('" & campaignName & "','MANUAL','" & Environment.UserName & "',1,0)")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function DeleteTiltManual(ByVal connStr As String, ByVal campaignID As Integer, ByVal neName As String, ByVal sectorID As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXECUTE [data_Common].[dbo].[sp_TILT_Manual_Delete] " & campaignID & ",'" & neName & "'," & sectorID)
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function ExecuteCalculateETilt(ByVal connStr As String, ByVal campaignID As Integer, ByVal MBTSName As String, ByVal sectorID As String) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXECUTE [data_Common].[dbo].[sp_TILT_Manual_CalculateETILT] " & campaignID & ",'MANUAL','" & MBTSName & "'," & sectorID & "")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetMMLDataForTiltCampaign(ByVal connStr As String, ByVal campaignID As Integer) As DataSet
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXECUTE [data_Common].[dbo].[sp_TILT_Manual_MML] " & campaignID)
        Return DataAccessorODBC.GetDataSet(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetTiltMMLCampaigns(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select NCR.ToolId, NCR.CampaignID, NCR.ResultSetID, NCR.ResultsCreated,NC.CampaignName, NC.CampaignType, NC.CampaignOwner")
        sqlQuery.AppendLine("From [data_Common].[dbo].[TILT_Campaigns_Results] NCR INNER Join [data_Common].[dbo].[TILT_Campaigns] NC ON NCR.CampaignID = NC.CampaignID Order By NCR.ResultsCreated DESC;")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetCoordinatesForSelectedSector(ByVal connStr As String, ByVal mbtsName As String, ByVal sectorID As Integer) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select Top 1 X,Y From dbo.IOS_Network_All Where [MBTSNAME] = '" & mbtsName & "' And [SectorID] = " & sectorID & ";")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "XML"

    Public Shared Function GetVendorsList(connStr As String) As DataTable
        Dim sqlQuery As New StringBuilder()
        sqlQuery.AppendLine("Select Distinct [IOS_Vendor] From [dbo].[IOS_CM_MO2Obj] Order By 1;")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "NBI Reports"

    Public Shared Function GetPredefinedPeriodComboBoxNBIReports(ByVal connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT * FROM IOS_PredefinedPeriod Where [Control] = 'cmbPredefTimeStats'")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Ref Check - Param Mapping"

    Public Shared Function GetMODetailsForParameter(ByVal connStr As String, ByVal paramName As String, ByVal MO As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select * From [dbo].[IOS_OSS_Param_Ref] Where [P_abbr_name] = " & Chr(39) & paramName & Chr(39) & "  And  [Managed_Object] = " & Chr(39) & MO & Chr(39))
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function GetMappingDataForTemplateMOParam(ByVal connStr As String, ByVal templateID As String, ByVal MO As String, ByVal paramName As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Select * From [dbo].[qry_IOS_CM_Template_Param_Mapping] Where [TemplateID] = " & templateID & "  And [MO] = " & Chr(39) & MO & Chr(39) & " And [ParameterName] = " & Chr(39) & paramName & Chr(39))
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "KPI Mapping - Manage Theme"

    Public Shared Function GetThemeType(connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT [ThematicTypeID],[ThematicTypeName] FROM [dbo].[IOS_KPIMapping_ThemeTypes];")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function SaveTheme(connStr As String, themeName As String, themeTypeID As Integer, roundBy As Double, distributionMethod As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Insert Into [dbo].[IOS_KPIMapping_Theme] ([ThematicName],[ThematicTypeID],[RoundBy],[DistributionMethod])")
        sqlQuery.AppendLine("Values (" & Chr(39) & themeName & Chr(39) & "," & themeTypeID & "," & roundBy & "," & Chr(39) & distributionMethod & Chr(39) & ");")
        sqlQuery.AppendLine("Select IDENT_CURRENT('[dbo].[IOS_KPIMapping_Theme]')")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    'Public Shared Function SaveThemeBins(connStr As String, themeID As Integer, regionColor As Integer, regionTransparency As String, regionInteriorType As String, regionBorderLineWidth As Integer, regionBorderLineTransparency As Integer,
    '                                     symbolNumber As Integer, symbolSize As Integer, symbolInteriorColor As Integer, symbolBorderColor As Integer, symbolInteriorTransparency As Integer, symbolBorderTransparency As Integer, rangeMin As Double, rangeMax As Double, individualValue As String)
    '    sqlQuery = New StringBuilder()
    '    sqlQuery.AppendLine("INSERT INTO [dbo].[IOS_KPIMapping_Theme_Bins] ([ThematicID],[RegionColor],[RegionTransparency],[RegionInteriorType],[RegionBorderLineWidth],[RegionBorderLineTransparancy],")
    '    sqlQuery.AppendLine("[SymbolNumber],[SymbolSize],[SymbolInteriorColor],[SymbolBorderColor],[SymbolInteriorTransparancy],[SymbolBorderTransparancy],[RangeMin],[RangeMax],[IndividualValue])")
    '    sqlQuery.AppendLine("VALUES (" & themeID & "," & regionColor & "," & Chr(39) & regionTransparency & Chr(39) & "," & Chr(39) & regionInteriorType & Chr(39) & "," & regionBorderLineWidth & "," & regionBorderLineTransparency & ",")
    '    sqlQuery.AppendLine(symbolNumber & "," & symbolSize & "," & symbolInteriorColor & "," & symbolBorderColor & "," & symbolInteriorTransparency & "," & symbolBorderTransparency & "," & rangeMin & "," & rangeMax & "," & Chr(39) & individualValue & Chr(39) & ")")
    '    Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    'End Function

    Public Shared Function DeleteKPIMappingThematicBinsForThematicID(conStr As String, thematicID As Integer) As Integer
        Try
            sqlQuery = New StringBuilder
            sqlQuery.AppendLine("DELETE [dbo].[IOS_KPIMapping_Theme_Bins] WHERE [ThematicID] = " & thematicID)
            Return DataAccessorODBC.ExecuteScalar(conStr, sqlQuery.ToString)
        Catch
        End Try
        Return Nothing
    End Function

    Public Shared Function DeleteTheme(connStr As String, thematicID As Integer) As Integer
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("Delete From [dbo].[IOS_KPIMapping_Theme] Where [ThematicID] = " & thematicID & ";")
        sqlQuery.AppendLine("Delete From [dbo].[IOS_KPIMapping_Theme_Bins] Where [ThematicID] = " & thematicID & ";")
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

#End Region

#Region "Web Report Server"

    Public Shared Function GetDashboardCategories(connStr As String) As DataTable
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("SELECT [OID],[Name] FROM [DevExpressReportServer].[dbo].[DocumentCategory]")
        Return DataAccessorODBC.GetDataTable(connStr, sqlQuery.ToString)
    End Function

    Public Shared Function SaveDashboardToReportServer(connStr As String, DashboardName As String, Category As String)
        sqlQuery = New StringBuilder()
        sqlQuery.AppendLine("EXECUTE [DevExpressReportServer].[dbo].[sp_AddNewDashboard] " & Chr(39) & DashboardName & Chr(39) & "," & Chr(39) & Category & Chr(39))
        Return DataAccessorODBC.ExecuteNonQuery(connStr, sqlQuery.ToString)
    End Function

#End Region

End Class
