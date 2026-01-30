Public Class SQLReportChartSeries
    Inherits SQLCommanCommand
    Sub New()
        _tableName = DataBaseTableName.TBL_REPORTCHART_SERIES
    End Sub
    Protected Overrides Sub Finalize()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    'Public Shared Function Insert(
    '                                     ByVal reportID As Integer,
    '                                     ByVal seriesName As String,
    '                                     ByVal seriesChartType As String,
    '                                     ByVal seriesAxisID As Integer,
    '                                     ByVal seriesColor As Integer,
    '                                     ByVal sortOrder As String,
    '                                     ByVal calSeriesTypeId As Integer,
    '                                     ByVal calSeriesParamValues As String
    '                             )

    '    Return "Exec " & StoreProcedurName.SP_REPORT_CHART_DATA_INSERT & " " & reportID & ",'" & chartType & "','" & chartTitle & "','" & axisLocation & "','" & axisLabel & "','" & axisAbsPerc & "','" & axisPrecision & "','" & axisScaleProp & "','" & seriesName & "','" & seriesChartType & "'," & seriesColor & ",'" & sortOrder & "'"
    'End Function
End Class
