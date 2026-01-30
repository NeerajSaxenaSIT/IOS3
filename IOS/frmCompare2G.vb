Imports System.Data
Imports System.Data.SqlClient
Imports dotnetCHARTING.WinForms
Imports System.IO
Imports System.Globalization
Imports IOS.Library
Imports System.Linq

Public Class frmCompare2G

#Region "Variables"

    Dim RxLevAvgdata As New DataTable
    Dim RxQualdata As New DataTable
    Dim isScan As Boolean = False

#End Region

#Region "Form & Controls' Events"

    Private Sub frmCompare2G_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim counter As Integer = 0
        ConfigurForm(Me, "frmCompare2G", counter)
        Me.BringToFront()
        CreateCharts(Me.RxLevAvgdata, Me.RxQualdata)
        If (frmCompare3G.Visible) Then
            Me.Location = New System.Drawing.Point(5, 555)
        Else
            Me.Location = New System.Drawing.Point(5, 60)
        End If
    End Sub

    Private Sub frmCompare2G_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If (dlgMappingSelection.Visible) Then
            dlgMappingSelection.BringToFront()
            dlgMappingSelection.TopMost = True
        End If
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

#End Region

#Region "Helper Methods"

    Public Sub SetChartData(ByVal RxLevAvgdata As DataTable, ByVal RxQualdata As DataTable, ByVal heading As String, ByVal IsScan As String)
        Dim counter As Integer = 0
        ConfigurForm(Me, "frmCompare2G", counter)
        Me.RxLevAvgdata = RxLevAvgdata
        Me.RxQualdata = RxQualdata
        Me.Text = "Compare Drive Test " + heading
        If (IsScan.ToLower() = "scan") Then
            Me.isScan = True
        Else
            Me.isScan = False
        End If
    End Sub

    Public Sub CreateCharts(ByVal RxLevAvgdata As DataTable, ByVal RxQualdata As DataTable)
        Dim dataRSCP As DataTable = GetRxLevAvg_Data(RxLevAvgdata)
        If (Not dataRSCP Is Nothing) Then
            Dim yaxis1 As New IOSAxis
            yaxis1.Orientation = Orientation.Left
            yaxis1.ElementMarkerType = ElementMarkerType.None
            yaxis1.ElementListToApply.Add("RxLevAvg_Before")
            yaxis1.ElementListToApply.Add("RxLevAvg_After")
            Dim listOfYAxix As New List(Of IOSAxis)
            listOfYAxix.Add(yaxis1)

            Dim chart_elements1 As New List(Of String)
            chart_elements1.Add("RxLevAvg")
            chart_elements1.Add("RxLevAvg_Before")
            chart_elements1.Add("RxLevAvg_After")
            Dim objIOSChartManager As New IOSChartManager(Chart1, dataRSCP, chart_elements1, "RxLevAvg Compare", listOfYAxix)
            objIOSChartManager.CreateChartOnRSCPandEconCount(ChartType.Combo, SeriesType.Spline, SeriesType.Spline, 3)
            Chart1.TitleBox.Label.Text = "RxLevAvg"
            Me.Chart1.RefreshChart()
        End If
        If Not Me.isScan Then
            Dim dataEcNo As DataTable = GetRxQual_Data(RxQualdata)
            If Not (dataEcNo Is Nothing) Then

                Dim yaxis2 As New IOSAxis
                yaxis2.Orientation = Orientation.Left
                yaxis2.ElementMarkerType = ElementMarkerType.Circle
                yaxis2.ElementListToApply.Add("RxQual_Before")
                yaxis2.ElementListToApply.Add("RxQual_after")
                Dim listOfYAxix2 As New List(Of IOSAxis)
                listOfYAxix2.Add(yaxis2)

                Dim chart_elements2 As New List(Of String)
                chart_elements2.Add("RxQual")
                chart_elements2.Add("RxQual_Before")
                chart_elements2.Add("RxQual_after")
                Dim objIOSChartManager As New IOSChartManager(Chart2, dataEcNo, chart_elements2, "RxQual Compare", listOfYAxix2)
                objIOSChartManager.CreateChartOnRSCPandEconCount(ChartType.Combo, SeriesType.Spline, SeriesType.Spline, 3)
                Chart2.TitleBox.Label.Text = "RxQual"
                Chart2.RefreshChart()
            End If
        End If
    End Sub

    Function GetRxLevAvg_Data(ByRef dt As DataTable) As DataTable
        If (Not dt Is Nothing) Then
            If (dt.Rows.Count > 0) Then
                dt.Columns(0).ColumnName = "RxLevAvg"
                dt.Columns(1).ColumnName = "RxLevAvg_Before"
                dt.Columns(2).ColumnName = "RxLevAvg_After"
                Return dt
            End If
        End If
        Return Nothing
    End Function

    Function GetRxQual_Data(ByRef dt As DataTable) As DataTable
        If (Not dt Is Nothing) Then
            If (dt.Rows.Count > 0) Then
                dt.Columns(0).ColumnName = "RxQual"
                dt.Columns(1).ColumnName = "RxQual_Before"
                dt.Columns(2).ColumnName = "RxQual_after"
                Return dt
            End If
            Return Nothing
        End If
        Return Nothing
    End Function

#End Region

End Class