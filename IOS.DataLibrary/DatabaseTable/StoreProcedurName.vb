Public Class StoreProcedurName
    Public Const SP_REPORTGROUP_CREATE As String = "sp_ReportGroup_Create"
    Public Const SP_REPORTGROUP_MODIFY As String = "sp_ReportGroup_Modify"
    Public Const SP_REPORTGROUP_DELETE As String = "sp_ReportGroup_Delete"
    Public Const SP_REPORTGROUP_GET As String = "sp_GetReportGroups"
    Public Const SP_REPORT_CREATE As String = "sp_Report_Create"
    Public Const SP_REPORT_COPY As String = "sp_Report_Copy"
    Public Const SP_REPORT_CREATE_TEMP As String = "sp_Report_Create_Temp"
    Public Const SP_REPORT_DELETE As String = "sp_Report_Delete"
    Public Const SP_REPORT_MODIFY As String = "sp_Report_Modify"
    Public Const SP_REPORT_SWAPORDINAL As String = "sp_Report_SwapOrdinal"
    Public Const SP_CATEGORY_SWAPORDINAL As String = "sp_Category_SwapOrdinal"
    Public Const SP_REPORTCATEGORY_CREATE As String = "sp_ReportCategory_Create"
    Public Const SP_REPORTCATEGORY_DELETE As String = "sp_ReportCategory_Delete"
    Public Const SP_REPORTCATEGORY_MODIFY As String = "sp_ReportCategory_Modify"
    Public Const SP_REPORTCONTENT_DIMENSIONS As String = "sp_ReportContent_Dimensions"
    Public Const SP_REPORTCONTENT_OBJECTS As String = "sp_ReportContent_Objects"
    Public Const SP_REPORTCONTENT_GETOBJECTS As String = "sp_ReportContent_GetObjects"
    Public Const SP_GETNEWREPORTID As String = "sp_GetNewReportId"

    Public Const SP_REPORTCONTENT_SOURCETIME As String = "sp_ReportContent_SourceTime"
    Public Const SP_REPORT_SWAP As String = "sp_Report_Swap"
    Public Const SP_OBJECTTREE_CM As String = "sp_GetObjectTreeCM "
    Public Const SP_OBJECTTREE_PM As String = "sp_GetObjectTreePM "

    

    Public Const SP_REPORT_GET As String = "sp_Report_Get"
    Public Const SP_GET_TIME_AGGREGATION_SUFFIX As String = "sp_GetTimeAggregationSuffix"
    Public Const SP_GET_OBJECT_AGGREGATION_SUFFIX As String = "sp_GetObjectAggregationSuffix"
    Public Const SP_GET_MEASUREMENT_PRIMARYKEY As String = "sp_GetMeasurementPrimaryKey"
    Public Const SP_GETOBJECTVIEW_CMORPM As String = "sp_GetObjectViewCMorPM"
    Public Const SP_GETDIMENSIONS_CMORPM As String = "sp_GetDimensionsForSource"
    Public Const SP_REPORTCONTENT_DELETE As String = "sp_ReportContent_Delete"
    Public Const SP_DASHBOARDGROUPS_CREATE As String = "sp_DashBoardGroup_Create"
    Public Const SP_DASHBOARDGROUPS_MODIFY As String = "sp_DashBoardGroup_Modify"
    Public Const SP_DASHBOARDGROUPS_DELETE As String = "sp_DashBoardGroup_Delete"
    Public Const SP_DASHBOARD_CREATE As String = "sp_DashBoard_Create"
    Public Const SP_DASHBOARD_MODIFY As String = "sp_Dashboard_Modify"
    Public Const SP_DASHBOARD_DELETE As String = "sp_Dashboard_Delete"
    Public Const SP_GET_DASHBOARD_GROUP_REPORT_TREE As String = "sp_GetDashBoardGroupReportTree"
    Public Const SP_GETREPORT_CHART_GRID As String = "sp_GetReportChartGrid"
    Public Const SP_REPORT_CHART_DATA_INSERT As String = "sp_ReportChartData_Insert"

    Public Const SP_GETJOBGROUPREPORTTREE As String = "sp_GetJobGroupReportTree"
    Public Const SP_JOBGROUP_CREATE As String = "sp_JobGroup_Create"
    Public Const SP_JOBGROUP_MODIFY As String = "sp_JobGroup_Modify"
    Public Const SP_JOBGROUP_DELETE As String = "sp_JobGroup_Delete"
    Public Const SP_JOB_CREATE As String = "sp_Job_Create"
    Public Const SP_JOBREPORT_INSERT As String = "sp_JobReports_Insert"
    Public Const SP_DASHBOARDREPORTS_INSERT As String = "sp_DashboardReports_Insert"
    Public Const SP_DASHBOARDREPORTS_SWAPORDINAL As String = "sp_DashboardReport_SwapOrdinal"
    Public Const SP_GETDASHBOARD_REPORTCHART As String = "sp_GetDashBoardReportChart"
    Public Const SP_DASHBOARDREPORTS_DELETE As String = "sp_DashboardReports_Delete"
    Public Const SP_REPORT_CHART_DATA_DELETE As String = "sp_ReportChartData_Delete"
    Public Const SP_GET_JOB_HISTORYBY_JOBID As String = "sp_GetJobHistoryByJobId"
    Public Const SP_JOBREPORTS_DELETE As String = "sp_JobReports_Delete"
    Public Const SP_JOB_UPDATE As String = "sp_Job_Modify"
    Public Const SP_JOB_RENAME As String = "sp_JobRename"
    Public Const SP_JOB_DELETE As String = "sp_JobDelete"

    Public Const SP_TECH_KPI_UPDATE As String = "sp_TechKPIUpdate "
    Public Const SP_TECH_KPI_INSERT As String = "sp_TechKPIInsert "
    Public Const SP_TECH_KPI_DELETE As String = "sp_TechKPIDelete "
    Public Const SP_TECH_KPI_GETBY_TECHANDCREATOR As String = "sp_TechKPIGetByTechAndCreator "


    Public Const SP_REPORT_CHART_SERIES_INSERT As String = "sp_ReportChartSeries_Insert "

    Public Const SP_REPORTCONTENT_FILTERS_INSERT As String = "sp_ReportContent_FiltersInsert "

    Public Const SP_REPORTCONTENTFILTERS_GETBY_REPORTID As String = "sp_ReportContentFiltersGetByReportID "
    Public Const SP_REPORTCONTENTFILTERS_DIMENSION_DISTINCT_VALUES As String = "sp_ReportContent_Dimensions_DistinctValues"
    Public Const SP_GET_COUNTERINFO As String = "sp_Get_Counterinfo "

    Public Const SP_GET_KPIGROUP As String = "sp_KPIGroup_Get "
    Public Const SP_KPIGROUP_DELETE As String = "sp_KPIGroup_Delete "
    Public Const SP_KPIGROUP_CREATE As String = "sp_KPIGroup_Create "
    Public Const SP_KPIGROUP_MODIFY As String = "sp_KPIGroup_Modify "
    Public Const SP_KPICATEGORY_CREATE As String = "sp_KPICategory_Create "
    Public Const SP_KPICATEGORY_MODIFY As String = "sp_KPICategory_Modify "
    Public Const SP_KPICATEGORY_DELETE As String = "sp_KPICategory_Delete "
    Public Const SP_KPICATEGORY_REMOVE_KPI As String = "sp_KPICategory_DeleteKPI "
    Public Const SP_KPICATEGORY_DELETE_KPI_DB As String = "sp_KPI_Delete "
    Public Const SP_KPICATEGORY_ADD_KPI As String = "sp_KPICategory_AddKPI "
    Public Const SP_REPORTCONTENT_SAVEEXPORTCONNECTION = "sp_ReportContent_ExportConnection"
    Public Const SP_REPORTCONTENT_GETEXPORTCONNECTION = "sp_ReportContent_GetExportConnection"

    Public Const QRY_GET_KPI_DATA As String = "SELECT TechnologyPackageName, MeasurementName, KPIName, KPIID FROM
                                                [qry_Technology_Package_KPI] a INNER JOIN tbl_Technology_Measurements b ON 
                                                a.measurementid=b.measurementid
                                                GROUP BY TechnologyPackageName, MeasurementName, KPIname, KPIID
                                                ORDER BY TechnologyPackageName, MeasurementName, KPIname, KPIID"

End Class
