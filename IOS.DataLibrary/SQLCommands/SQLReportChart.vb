Public Class SQLReportChart
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_REPORTCHART
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function ReportChartData_Insert(
                                         ByVal reportID As Integer,
                                         ByVal chartType As String,
                                         ByVal chartTitle As String,
                                         ByVal axisLocation As String,
                                         ByVal axisLabel As String,
                                         ByVal axisAbsPerc As String,
                                         ByVal axisPrecision As String,
                                         ByVal axisScaleProp As String,
                                         ByVal seriesName As String,
                                         ByVal seriesChartType As String,
                                         ByVal seriesColor As Integer,
                                         ByVal sortOrder As String,
                                         ByVal calSeriesTypeId As Integer,
                                         ByVal calSeriesParamValues As String,
                                         ByVal lineThickness As String,
                                         ByVal calculatedYAxis As String,
                                         ByVal chartAxisFont As String)
        chartAxisFont = chartAxisFont.Replace("'", "''")

        Return "Exec " & StoreProcedurName.SP_REPORT_CHART_DATA_INSERT & " " & reportID & ",'" & chartType & "','" & chartTitle & "','" & axisLocation & "','" & axisLabel & "','" & axisAbsPerc & "','" & axisPrecision & "','" & axisScaleProp & "','" & seriesName & "','" & seriesChartType & "'," & seriesColor & ",'" & sortOrder & "','" & calSeriesTypeId & "','" & calSeriesParamValues & "','" & calculatedYAxis & "','" & lineThickness & "', '" & chartAxisFont & "'"
    End Function


    Public Shared Function GetReportChartData(ByVal reportID As Integer)
        Return "SELECT * FROM  " & ViewName.VIEW_REPORTCHART & " WHERE " & ReportChartFields.ReportID & "=" & reportID & ""
    End Function
    Public Shared Function GetReportAxisData(ByVal reportID As Integer)
        Return "SELECT * FROM  " & ViewName.VIEW_REPORTAXISDATA & " WHERE " & ReportChartFields.ReportID & "=" & reportID & ";"
    End Function
    Public Shared Function ReportChartData_Delete(ByVal reportID As Integer)
        Return "Exec " & StoreProcedurName.SP_REPORT_CHART_DATA_DELETE & " " & reportID & ""
    End Function

End Class
