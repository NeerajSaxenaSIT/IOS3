Imports System.Data
Imports System.Data.SqlClient
Imports dotnetCHARTING.WinForms
Imports System.IO
Imports System.Globalization
Imports IOS.Library
Imports System.Linq

Public Class frmCompare4G

#Region "Variables"

    Dim rsrpdata As New DataTable
    Dim rsrqdata As New DataTable

#End Region

#Region "Form & Controls' Events"

    Private Sub frmCompare4G_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If (dlgMappingSelection.Visible) Then
            dlgMappingSelection.BringToFront()
            dlgMappingSelection.TopMost = True
        End If
    End Sub

    Private Sub frmCompare4G_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim counter As Integer = 0
        ConfigurForm(Me, "frmCompare4G", counter)
        Me.Location = New System.Drawing.Point(555, 60)
        Me.BringToFront()
        CreateCharts(Me.rsrpdata, Me.rsrqdata)
        If (frmCompare3G.Visible And frmCompare2G.Visible) Then
            Me.Location = New System.Drawing.Point(frmCompare3G.Width + 10, 60)
        ElseIf (frmCompare3G.Visible And Not frmCompare2G.Visible) Then
            Me.Location = New System.Drawing.Point(5, 555)
        ElseIf (Not frmCompare3G.Visible And frmCompare2G.Visible) Then
            Me.Location = New System.Drawing.Point(5, 555)
        Else
            Me.Location = New System.Drawing.Point(5, 60)
        End If
    End Sub

#End Region

#Region "Helper Methods"

    Public Sub SetChartData(ByVal rsrpData As DataTable, ByVal rsrqData As DataTable, ByVal heading As String)
        Me.rsrpdata = rsrpData
        Me.rsrqdata = rsrqData
        Me.Text = "Compare Drive Test " + heading
    End Sub

    Function GetRSRP_Data(ByRef dt As DataTable) As DataTable
        If (Not dt Is Nothing) Then
            If (dt.Rows.Count > 0) Then
                dt.Columns(0).ColumnName = "RSRP"
                dt.Columns(1).ColumnName = "Serving_RSRP_Before"
                dt.Columns(2).ColumnName = "Serving_RSRP_After"
                Return dt
            End If
            Return Nothing
        End If
        Return Nothing
    End Function

    Function GetRSRQ_Data(ByRef dt As DataTable) As DataTable
        If Not dt Is Nothing Then
            If (dt.Rows.Count > 0) Then
                dt.Columns(0).ColumnName = "RSRQ"
                dt.Columns(1).ColumnName = "Serving_RSRQ_Before"
                dt.Columns(2).ColumnName = "Serving_RSRQ_After"
                Dim datarows() As DataRow = dt.Select("", "RSRQ")
                Return dt
            End If
            Return Nothing
        End If
        Return Nothing
    End Function

    Public Sub CreateCharts(ByVal rsrpData As DataTable, ByVal rsrqData As DataTable)
        Dim dataRSRP As DataTable = GetRSRP_Data(rsrpData)
        If (Not dataRSRP Is Nothing) Then
            If (dataRSRP.Rows.Count > 0) Then
                Dim yaxis1 As New IOSAxis
                yaxis1.Orientation = Orientation.Left
                yaxis1.ElementMarkerType = ElementMarkerType.None
                yaxis1.ElementListToApply.Add("Serving_RSRP_Before")
                yaxis1.ElementListToApply.Add("Serving_RSRP_After")
                Dim listOfYAxix As New List(Of IOSAxis)
                listOfYAxix.Add(yaxis1)

                Dim chart_elements1 As New List(Of String)
                chart_elements1.Add("RSRP")
                chart_elements1.Add("Serving_RSRP_Before")
                chart_elements1.Add("Serving_RSRP_After")
                Dim objIOSChartManager As New IOSChartManager(Chart1, dataRSRP, chart_elements1, "RSRP Compare", listOfYAxix)
                objIOSChartManager.CreateChartOnRSCPandEconCount(ChartType.Combo, SeriesType.Spline, SeriesType.Spline, 3)
                Chart1.TitleBox.Label.Text = "RSRP"
                Me.Chart1.RefreshChart()
            End If
        End If
        Dim dataRSRQ As DataTable = GetRSRQ_Data(rsrqData)
        If (Not dataRSRQ Is Nothing) Then
            If dataRSRQ.Rows.Count > 0 Then
                Dim yaxis2 As New IOSAxis
                yaxis2.Orientation = Orientation.Left
                yaxis2.ElementMarkerType = ElementMarkerType.Circle
                yaxis2.ElementListToApply.Add("Serving_RSRQ_Before")
                yaxis2.ElementListToApply.Add("Serving_RSRQ_After")
                Dim listOfYAxix2 As New List(Of IOSAxis)
                listOfYAxix2.Add(yaxis2)

                Dim chart_elements2 As New List(Of String)
                chart_elements2.Add("RSRQ")
                chart_elements2.Add("Serving_RSRQ_Before")
                chart_elements2.Add("Serving_RSRQ_After")
                Dim objIOSChartManager As New IOSChartManager(Chart2, dataRSRQ, chart_elements2, "RSRQ Compare", listOfYAxix2)
                objIOSChartManager.CreateChartOnRSCPandEconCount(ChartType.Combo, SeriesType.Spline, SeriesType.Spline, 3)
                Chart2.TitleBox.Label.Text = "RSRQ"
                Chart2.RefreshChart()
            End If
        End If
    End Sub

#End Region

End Class