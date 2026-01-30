Imports System.Data
Imports System.Data.SqlClient
Imports dotnetCHARTING.WinForms
Imports System.IO
Imports System.Globalization
Imports IOS.Library
Imports System.Linq

Public Class frmCompare3G

#Region "Variables"

    Dim rscpdata As New DataTable
    Dim econdata As New DataTable

#End Region

#Region "Form & Controls' Events"

    Private Sub frmCompare3G_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If (dlgMappingSelection.Visible) Then
            dlgMappingSelection.BringToFront()
            dlgMappingSelection.TopMost = True
        End If
    End Sub

    Private Sub frmCompare3G_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim counter As Integer = 0
        ConfigurForm(Me, "frmCompare3G", counter)
        Me.BringToFront()
        CreateCharts(Me.rscpdata, Me.econdata)
        Me.Location = New System.Drawing.Point(5, 60)
    End Sub

#End Region

#Region "Helper Methods"

    Public Sub SetChartData(ByVal rscpdata As DataTable, ByVal ecnodata As DataTable, ByVal heading As String)
        Me.rscpdata = rscpdata
        Me.econdata = ecnodata
        Me.Text = "Compare Drive Test " + heading
    End Sub

    Public Sub CreateCharts(ByVal rscpdata As DataTable, ByVal ecnodata As DataTable)
        Dim dataRSCP As DataTable = GetRSCP_Data(rscpdata)
        If (Not dataRSCP Is Nothing) Then
            If (dataRSCP.Rows.Count > 0) Then
                Dim yaxis1 As New IOSAxis
                yaxis1.Orientation = Orientation.Left
                yaxis1.ElementMarkerType = ElementMarkerType.None
                yaxis1.ElementListToApply.Add("Serving_RSCP_Before")
                yaxis1.ElementListToApply.Add("Serving_RSCP_after")
                Dim listOfYAxix As New List(Of IOSAxis)
                listOfYAxix.Add(yaxis1)

                Dim chart_elements1 As New List(Of String)
                chart_elements1.Add("RSCP")
                chart_elements1.Add("Serving_RSCP_Before")
                chart_elements1.Add("Serving_RSCP_after")
                Dim objIOSChartManager As New IOSChartManager(Chart1, dataRSCP, chart_elements1, "RSCP Compare", listOfYAxix)
                objIOSChartManager.CreateChartOnRSCPandEconCount(ChartType.Combo, SeriesType.Spline, SeriesType.Spline, 3)
                Chart1.TitleBox.Label.Text = "RSCP"
                Me.Chart1.RefreshChart()
            End If
        End If
        Dim dataEcNo As DataTable = GetEcNo_Data(ecnodata)
        If (Not dataEcNo Is Nothing) Then
            If dataEcNo.Rows.Count > 0 Then
                Dim yaxis2 As New IOSAxis
                yaxis2.Orientation = Orientation.Left
                yaxis2.ElementMarkerType = ElementMarkerType.Circle
                yaxis2.ElementListToApply.Add("Serving_Ecno_Before")
                yaxis2.ElementListToApply.Add("Serving_Ecno_after")
                Dim listOfYAxix2 As New List(Of IOSAxis)
                listOfYAxix2.Add(yaxis2)

                Dim chart_elements2 As New List(Of String)
                chart_elements2.Add("EcNo")
                chart_elements2.Add("Serving_Ecno_Before")
                chart_elements2.Add("Serving_Ecno_after")
                Dim objIOSChartManager As New IOSChartManager(Chart2, dataEcNo, chart_elements2, "EcNo Compare", listOfYAxix2)
                objIOSChartManager.CreateChartOnRSCPandEconCount(ChartType.Combo, SeriesType.Spline, SeriesType.Spline, 3)
                Chart2.TitleBox.Label.Text = "EcNo"
                Chart2.RefreshChart()
            End If
        End If
    End Sub

    Function GetRSCP_Data(ByRef dt As DataTable) As DataTable
        If (Not dt Is Nothing) Then
            If (dt.Rows.Count > 0) Then
                dt.Columns(0).ColumnName = "RSCP"
                dt.Columns(1).ColumnName = "Serving_RSCP_Before"
                dt.Columns(2).ColumnName = "Serving_RSCP_after"

                Return dt
            End If
            Return Nothing
        End If
        Return Nothing
    End Function

    Function GetEcNo_Data(ByRef dt As DataTable) As DataTable
        If Not dt Is Nothing Then
            If (dt.Rows.Count > 0) Then
                dt.Columns(0).ColumnName = "EcNo"
                dt.Columns(1).ColumnName = "Serving_EcNo_Before"
                dt.Columns(2).ColumnName = "Serving_EcNo_after"
                Dim datarows() As DataRow = dt.Select("", "Ecno")
                Return dt
            End If
            Return Nothing
        End If
        Return Nothing
    End Function

#End Region

End Class