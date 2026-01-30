Imports dotnetCHARTING.WinForms
Imports System.Windows.Forms
Public Class ChartComman
    Public Shared Sub ChartDataClear(ByRef _chPCHR As dotnetCHARTING.WinForms.Chart)
        _chPCHR.ResumeLayout()
        _chPCHR.SeriesCollection.Clear()
        _chPCHR.Series = Nothing
        _chPCHR.Refresh()
        _chPCHR.SuspendLayout()
    End Sub

    Public Function GetNewChart() As dotnetCHARTING.WinForms.Chart
        Dim chart As New dotnetCHARTING.WinForms.Chart()
        chart.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        chart.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Return chart
    End Function

    Public Function GetChart(ByVal chartName As String, ByVal chartTag As String) As dotnetCHARTING.WinForms.Chart
        Dim chartA As dotnetCHARTING.WinForms.Chart = GetNewChart()
        chartA.AutoScroll = True
        'chartA.ContextMenuStrip = cm_SourceControlSubCatagory
        chartA.Tag = chartTag

        Dim Annotation3 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
        Dim Label4 As dotnetCHARTING.WinForms.Label = New dotnetCHARTING.WinForms.Label()
        chartA.Background.Color = System.Drawing.Color.White
        Annotation3.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Annotation3.DynamicSize = True
        Annotation3.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Annotation3.InteriorLine.Visible = True
        Annotation3.Line.Color = System.Drawing.Color.Gray
        Annotation3.Line.Visible = True
        Annotation3.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
        Annotation3.Padding = 2
        Annotation3.Shadow.Visible = False
        Annotation3.Size = New System.Drawing.Size(314, 288)
        Annotation3.Visible = True
        chartA.Box = Annotation3
        chartA.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        chartA.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        chartA.ChartArea.DefaultElement.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
        chartA.ChartArea.DefaultElement.DefaultSubValue.Line.Visible = True
        chartA.ChartArea.DefaultElement.DefaultSubValue.Visible = True
        chartA.ChartArea.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        chartA.ChartArea.DefaultElement.LegendEntry.DividerLine.Visible = True
        chartA.ChartArea.DefaultElement.Outline.Visible = True
        chartA.ChartArea.DefaultElement.SmartLabel.Color = System.Drawing.Color.Empty
        chartA.ChartArea.DefaultElement.SmartLabel.Line.Visible = True
        chartA.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        chartA.ChartArea.InteriorLine.Visible = True
        chartA.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        chartA.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        chartA.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        chartA.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        chartA.ChartArea.LegendBox.DefaultEntry.DividerLine.Visible = True
        chartA.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        chartA.ChartArea.LegendBox.HeaderEntry.DividerLine.Visible = True
        chartA.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        chartA.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        chartA.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        chartA.ChartArea.LegendBox.HeaderEntry.Visible = False
        chartA.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        chartA.ChartArea.LegendBox.InteriorLine.Visible = True
        chartA.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        chartA.ChartArea.LegendBox.Line.Visible = True
        chartA.ChartArea.LegendBox.Padding = 4
        chartA.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        chartA.ChartArea.LegendBox.Visible = True
        chartA.ChartArea.Line.Color = System.Drawing.Color.Gray
        chartA.ChartArea.Line.Visible = True
        chartA.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        chartA.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        chartA.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        chartA.ChartArea.TitleBox.InteriorLine.Visible = True
        chartA.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        chartA.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        chartA.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        chartA.ChartArea.TitleBox.Line.Visible = True
        chartA.ChartArea.TitleBox.Visible = True
        chartA.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        chartA.ChartArea.XAxis.DefaultTick.GridLine.Visible = True
        chartA.ChartArea.XAxis.DefaultTick.Line.Length = 3
        chartA.ChartArea.XAxis.DefaultTick.Line.Visible = True
        chartA.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        chartA.ChartArea.XAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        chartA.ChartArea.XAxis.ScaleBreakLine.Visible = True
        chartA.ChartArea.XAxis.TickLabelSeparatorLine.Visible = True
        chartA.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        chartA.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        chartA.ChartArea.XAxis.ZeroTick.GridLine.Visible = True
        chartA.ChartArea.XAxis.ZeroTick.Line.Length = 3
        chartA.ChartArea.XAxis.ZeroTick.Line.Visible = True
        chartA.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
        chartA.ChartArea.YAxis.DefaultTick.GridLine.Visible = True
        chartA.ChartArea.YAxis.DefaultTick.Line.Length = 3
        chartA.ChartArea.YAxis.DefaultTick.Line.Visible = True
        chartA.ChartArea.YAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        chartA.ChartArea.YAxis.ScaleBreakLine.Visible = True
        chartA.ChartArea.YAxis.TickLabelSeparatorLine.Visible = True
        chartA.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        chartA.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        chartA.ChartArea.YAxis.ZeroTick.GridLine.Visible = True
        chartA.ChartArea.YAxis.ZeroTick.Line.Length = 3
        chartA.ChartArea.YAxis.ZeroTick.Line.Visible = True
        chartA.DataGrid = Nothing
        chartA.DefaultElement.DefaultSubValue.Line.Visible = True
        chartA.DefaultElement.DefaultSubValue.Visible = True
        chartA.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
        chartA.DefaultElement.LegendEntry.DividerLine.Visible = True
        chartA.DefaultElement.Outline.Visible = True
        chartA.Dock = System.Windows.Forms.DockStyle.Fill
        chartA.Location = New System.Drawing.Point(3, 3)
        chartA.MinimumSize = New System.Drawing.Size(100, 50)
        chartA.Name = chartName
        chartA.NoDataLabel.Text = "No Data"
        chartA.ObjectChart = Label4
        chartA.Size = New System.Drawing.Size(315, 289)
        chartA.SmartLabelLine.Visible = True
        chartA.StartDateOfYear = New Date(CType(0, Long))
        chartA.TabIndex = 8
        chartA.TempDirectory = "C:\Users\IOS\AppData\Local\Temp\"
        Return chartA
    End Function
End Class
