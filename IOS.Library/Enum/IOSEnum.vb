Public Enum EnumStatsOrTopX
    STATS
    TOPX
End Enum

Public Enum EnumSendToMap
    FromDefault
    FromPH
    ICMFromCategory
    ICMFromOverview
    ICMFromPreconfigured
    PolygonMap
    FromPCHR
    FromGeoID
    FromCapacity
End Enum

Public Enum EnumChartGridClick
    FromChart
    FromGrid
End Enum

Public Enum CellFootPrintType
    None
    Scanner
    UE
    CNE
End Enum

Public Enum TableNames
    DT_Scan2G_Parallel
    DT_Scan3G_Parallel
    DT_Scan4G_Parallel
    DT_Scan5G_Parallel
    DT_UE2G_Parallel
    DT_UE3G_Parallel
    DT_UE4G_Parallel
    DT_UE5G_Parallel
    DT_Compare
    DT_Events_GetEvents
    DT_Events
    DT_EventGrid
    CellFootPrint
    CNE_RAW_Map_2G
    CNE_RAW_Map_3G
    CNE_RAW_Map_4G
    CNE_RAW_Map_5G
End Enum

Public Enum IOSKPIType
    PMKPI = 1
    ICMKPI = 2
End Enum

Public Enum IOSKPIThemeType
    RANGED = 1
    PIE = 2
    INDIVIDUAL = 3
End Enum

Public Enum PCHTType
    PS
    CS
End Enum

Public Enum RadioValue
    IMSI
    CellSetup
    CellRelease
    CallRecordNum
    CellChart
End Enum

Public Enum ParameterHistoryChart
    Chart1
    Chart2A
    Chart2B
    Chart3_2A_Clicked
    Chart3_2B_Clicked
    GridChart2A
    GridChart3_WithChart3OnChart2aClicked
    GridChart3_WithChart3OnChart2bClicked
    Chart2BCellData
End Enum

Public Enum DragDropType
    NoDragDrop
    ByOprators
    ByAggregrate
    ByTableCounter
    ByCounter
    ByKPI
End Enum
Public Enum KPIDataBaseName
    None
    MSSQL
    ORACLE
End Enum

Public Enum EnumSelectBy
    None
    FromField
    FromValue
    FromKeyPress
End Enum

Public Enum TreeSelectionType
    NotSelected
    ChartSetName
    Category
    Chart
End Enum
'===================================================SandBox==========================
Public Enum OperatorEnum3
    Equel
    LessThen
    GraterThen
    NotEquel
End Enum

Public Enum ReportSelectionType
    NotSelected
    Group
    Category
    Report
    Kpi
End Enum

Public Enum DashboardSelectionType
    NotSelected
    DashboardGroup
    Dashboard
    DashboardReport

End Enum

Public Enum JobSelectionType
    NotSelected
    JobGroup
    Job
    JobReport
End Enum

Public Enum IOSChartType
	Combo = 0
	Pie = 1
	Scatter = 2
	Bubbles = 3
	Radar = 4
	Gauges = 5
	Histogram = 6
    AlignInterval = 7
    Surface = 8
    IndexedCombo = 9
    AlignIntervalCompare = 10
    GroupPerAttribute = 11
End Enum

Public Enum StatisticsOrThreshold
    Statistics
    Threshold
End Enum

Public Enum DatamartFieldType
    None = 0
    Counter = 1
    Kpi = 2
    ObjectFld = 3
    Time = 4
End Enum

'Public Enum IOSChartType
'	Combo = 0
'	Histogram = 1
'	Scatter = 2
'	AlignHour = 3
'	AlignWeekDay = 4
'End Enum