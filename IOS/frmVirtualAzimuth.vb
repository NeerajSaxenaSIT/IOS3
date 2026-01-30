Imports dotnetCHARTING.WinForms
Imports IOS.Library

Public Class frmVirtualAzimuth

#Region "Variables"

    Dim CheckedNodes As New List(Of TreeNode)
    Dim dsTree_Sites As DataSet = New System.Data.DataSet
    Dim dsPM_Sites As DataSet = New System.Data.DataSet
    Dim ds_predef As DataSet = New System.Data.DataSet
    Dim flp_VA_Charts As FlowLayoutPanel = Nothing
    Dim BandPlotted As New List(Of PlottedNode)

    Structure PlottedNode
        Dim tech As String
        Dim site As String
        Dim band As String
        Dim techtag As String
        Dim sitetag As String
        Dim bandtag As String
    End Structure

#End Region

#Region "Form & Controls Event"

    Private Sub frmVirtualAzimuth_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        ResizeChartWidth()
    End Sub

    Private Sub frmVirtualAzimuth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            lblRadarDate.Text = ""
            txtSite.Text = ""
            dateEditStart.EditValue = DateAdd(DateInterval.Day, -30, Now).Date
            dateEditEnd.EditValue = Now.Date
            tbcChartHeight.Value = 230

            'load predefined combo box
            vcmb_VA_PreDef.Enabled = False
            dateEditStart.Enabled = True
            dateEditEnd.Enabled = True

            BindComboWithPredefinedPeriod(vcmb_VA_PreDef)
            Dim flp As FlowLayoutPanel = New FlowLayoutPanel()
            flp.FlowDirection = FlowDirection.TopDown
            flp.AutoScroll = True
            TableLayoutPanel1.Controls.Add(flp, 1, 1)
            flp.Dock = DockStyle.Fill
            flp_VA_Charts = flp
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub txtSite_TextChanged(sender As Object, e As EventArgs) Handles txtSite.TextChanged
        If txtSite.Text.Length = 0 Then
            tvObjectTree.Nodes.Clear()
        ElseIf txtSite.Text.Length > 1 Then
            ReloadSiteTree()
        End If
        BandPlotted.Clear()
    End Sub

    Private Sub tvObjectTree_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvObjectTree.AfterCheck
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        If BandPlotted.Count = 0 Then
            flp_VA_Charts.Controls.Clear()
        End If

        Try
            If GetPeriod() Is Nothing Then
                Exit Sub
            End If
            TreeView_CheckAndCount(e.Node, "SITE")
            If e.Node.Checked = True Then
                If e.Node.Level = 3 Then
                    'run pm queries, build charts
                    Dim site As String = e.Node.Parent.Parent.Text.ToString
                    Dim tech As String = e.Node.Parent.Text.ToString
                    Dim band As String = e.Node.Text.ToString

                    Dim sitetag As String = e.Node.Parent.Parent.Tag.ToString
                    Dim techtag As String = Replace(e.Node.Parent.Tag.ToString, sitetag + "_", "")
                    Dim bandtag As String = Replace(e.Node.Tag.ToString, sitetag + "_", "")
                    Dim period As String = GetPeriod()
                    Dim newnode As New PlottedNode
                    newnode.tech = tech
                    newnode.site = site
                    newnode.band = band
                    newnode.techtag = techtag
                    newnode.sitetag = sitetag
                    newnode.bandtag = bandtag

                    If Not BandPlotted.Contains(newnode) Then

                        Dim parray()() As String = {
                            New String() {"@site", "IN('" & sitetag & "')"},
                            New String() {"@tech", "IN('" & techtag & "')"},
                            New String() {"@band", "IN('" & bandtag & "')"},
                            New String() {"@period", period}
                        }

                        dsPM_Sites = IOS.DataLibrary.DataAccessorODBC.GetDataSet(GetSQL(3101, parray)(0), GetSQL(3101, parray)(1))
                        'set radar control
                        If dsPM_Sites.Tables.Count > 0 Then
                            tbRadarDate.Properties.Maximum = dsPM_Sites.Tables(0).Rows.Count
                            RadarChart(dsPM_Sites, site, band)
                            RealAndVirtualChart(dsPM_Sites, site, band)
                            VerdictChart(dsPM_Sites, site, band)
                        End If
                    End If
                    BandPlotted.Add(newnode)
                    ResizeChartWidth()
                End If
            Else
                If e.Node.Level = 3 Then
                    'run pm queries, build charts
                    Dim site As String = e.Node.Parent.Parent.Text.ToString
                    Dim band As String = e.Node.Text.ToString
                    Dim tech As String = e.Node.Parent.Text.ToString

                    Dim sitetag As String = e.Node.Parent.Parent.Tag.ToString
                    Dim techtag As String = Replace(e.Node.Parent.Tag.ToString, sitetag + "_", "")
                    Dim bandtag As String = Replace(e.Node.Tag.ToString, sitetag + "_", "")

                    Dim newnode As New PlottedNode
                    newnode.tech = tech
                    newnode.site = site
                    newnode.band = band
                    newnode.techtag = techtag
                    newnode.sitetag = sitetag
                    newnode.bandtag = bandtag

                    If BandPlotted.Contains(newnode) Then
                        'remove the charts 
                        Dim lstToRemove As New List(Of Chart)
                        For j = 0 To flp_VA_Charts.Controls.Count - 1
                            Dim ch As Chart = TryCast(flp_VA_Charts.Controls(j), Chart)
                            If Not ch Is Nothing Then
                                If ch.Title = "SITE - BAND: " + site & " - " + band Then
                                    lstToRemove.Add(ch)
                                End If
                            End If

                        Next
                        For Each ch As Chart In lstToRemove
                            flp_VA_Charts.Controls.Remove(ch)
                        Next
                        BandPlotted.Remove(newnode)
                        ResizeChartWidth()
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub tbcChartHeight_ValueChanged(sender As Object, e As EventArgs) Handles tbcChartHeight.ValueChanged
        ResizeChartHeights(tbcChartHeight.Value)
    End Sub

    Private Sub dateEditStart_EditValueChanged(sender As Object, e As EventArgs) Handles dateEditStart.EditValueChanged
        vcmb_VA_PreDef.SelectedIndex = 0
        RefreshCharts()
    End Sub

    Private Sub dateEditEnd_EditValueChanged(sender As Object, e As EventArgs) Handles dateEditEnd.EditValueChanged
        vcmb_VA_PreDef.SelectedIndex = 0
        RefreshCharts()
    End Sub

    Private Sub tbRadarDate_ValueChanged(sender As Object, e As EventArgs) Handles tbRadarDate.ValueChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If Not flp_VA_Charts Is Nothing Then
                For Each ch As Chart In flp_VA_Charts.Controls
                    If ch.Type = ChartType.Radar Then
                        RadarChart(ch.Series.Data, Nothing, Nothing, ch)
                    End If
                    If ch.Type = ChartType.Combo Then
                        Dim rdrdate As DateTime = CType(ch.Series.Data, DataSet).Tables(0)(tbRadarDate.EditValue - 1)("PERIOD_START_TIME")
                        lblRadarDate.Text = rdrdate.ToString("yyyy-MM-dd")
                        AddAxisMarker(ch, rdrdate)
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Helper"

    Private Sub ReloadSiteTree()
        Dim parray()() As String = {New String() {"@site", Chr(39) & txtSite.Text & "%" & Chr(39)}}
        dsTree_Sites = IOS.DataLibrary.DataAccessorODBC.GetDataSet(GetSQL(3100, parray)(0), GetSQL(3100, parray)(1))
        Fill_TreeviewStatsVA("PLMN", tvObjectTree)
    End Sub

    Public Sub Fill_TreeviewStatsVA(ByVal node As String, ByRef tree As TreeView)
        Dim roottn As TreeNode = New TreeNode()
        roottn.Text = "PLMN"
        roottn.ImageKey = "EMPTY"
        roottn.SelectedImageKey = "EMPTY"
        tree.Nodes.Clear()
        tree.Nodes.Add(roottn)
        Dim tNode As New TreeNode()
        tNode = tree.Nodes(0)

        PopulateObjectTree(roottn.Text, tNode, dsTree_Sites, tNode.TreeView.Tag)
        tNode.ExpandAll()
    End Sub

    Private Function GetPeriod() As String
        Try
            If vcmb_VA_PreDef.Enabled = True And vcmb_VA_PreDef.SelectedIndex > 0 Then
                Dim dr() As DataRow = dtPredefinePeriod.Select("PredefinedPeriodID = " & TryCast(vcmb_VA_PreDef.SelectedItem, clsComboBoxItem).Value)
                If Not dr Is Nothing Then
                    If dr.Count > 0 Then
                        Return dr(0)("SQL").ToString
                    End If
                End If
            Else
                Dim startdate As Date = Nothing
                Dim enddate As Date = Nothing
                startdate = dateEditStart.EditValue
                enddate = dateEditEnd.EditValue
                Dim startdate_string As String = Chr(39) & startdate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
                Dim enddate_string As String = Chr(39) & enddate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
                Return "A.PERIOD_START_TIME >=" + startdate_string + " AND A.PERIOD_START_TIME < " + enddate_string
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Sub TreeView_CheckAndCount(ByRef nd As TreeNode, ByVal targetobj As String)
        Try
            If nd.Checked = True Then
                If nd.Level > 1 Then
                    If nd.Parent.Checked = False Then
                        nd.Parent.Checked = True
                    End If
                End If
                Dim ln As List(Of TreeNode) = Treeview_GetCheck(nd.Nodes)
                If nd.Nodes.Count > 0 And ln.Count = 0 Then
                    For Each nde As TreeNode In nd.Nodes
                        If nde.Checked = False Then
                            nde.Checked = True
                        End If
                    Next
                End If

                If nd.Level = Treeview_GetNodeLevel_VA(targetobj) Then
                    ' Treeview_Count(cnt, lbl, 1)
                End If
            Else
                If nd.Nodes.Count > 0 Then
                    For Each nde As TreeNode In nd.Nodes
                        If nde.Checked = True Then
                            nde.Checked = False
                        End If
                    Next
                End If
                If nd.Level = Treeview_GetNodeLevel_VA(targetobj) Then
                    ' Treeview_Count(cnt, lbl, -1)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Function GetColor(ByVal col As String) As Color
        Select Case col.Substring(Len(col) - 2, 2)
            Case "S1"
                Return Color.Blue
            Case "S2"
                Return Color.Red
            Case "S3"
                Return Color.Green
            Case "S4"
                Return Color.Yellow
        End Select
        Return Nothing
    End Function

    Private Function Treeview_GetNodeLevel_VA(ByVal obj As String) As Integer
        Select Case obj
            Case "SITE"
                Return 1
            Case "BAND"
                Return 3
            Case "TECH"
                Return 2
        End Select
        Return 0
    End Function

    Private Sub ResizeChartWidth()
        Try
            TableLayoutPanel1.Width = Me.Width - 10
            If Not flp_VA_Charts Is Nothing Then
                Me.SuspendLayout()
                flp_VA_Charts.Width = Me.Width
                For Each ch As Chart In flp_VA_Charts.Controls
                    ch.Width = (flp_VA_Charts.Width - 25) / BandPlotted.Count - ((BandPlotted.Count - 1) * 2)
                    ch.RefreshChart()
                Next
                Me.ResumeLayout()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadarChart(ByRef ds As DataSet, ByVal Site As String, ByVal band As String, Optional nc As Chart = Nothing)
        Dim recalc As Boolean = False
        Try
            If nc Is Nothing Then
                nc = New Chart

                If Not nc Is Nothing Then
                    nc.Tag = Site
                    nc.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
                End If
                nc.Type = ChartType.Radar
                ' Set the size
                nc.AutoSize = False

                ' Set the temp directory
                nc.TempDirectory = "temp"
                nc.Title = "SITE - BAND: " + Site & " - " + band

                nc.Width = flp_VA_Charts.Width - 25
                nc.Height = tbcChartHeight.Value

                ' Specify a series type.
                nc.DefaultSeries.Type = SeriesType.Marker

                ' Setup the x axis.
                nc.XAxis.ScaleRange = New ScaleRange(0, 360)
                nc.XAxis.DefaultTick.Label.Text = "%Value"
                nc.XAxis.AlternateGridBackground.Color = Color.FromArgb(20, Color.Gray)
                nc.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Right
                nc.LegendBox.DefaultEntry.Value = ""
                nc.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
                nc.LegendBox.Visible = True

                nc.Tag = Site
                flp_VA_Charts.Controls.Add(nc)
            Else
                recalc = True
                nc.SeriesCollection.Clear()
            End If

            Dim lst_Colors As New List(Of Color)
            Dim sc As New SeriesCollection
            Dim j As Integer = 0

            For i = 1 To 4
                Dim s As New Series
                s.Name = "V_Angle_S" + i.ToString

                If ds.Tables(0).Rows.Count > 0 Then
                    If Not ds.Tables(0)(tbRadarDate.Value - 1)(s.Name) Is Nothing Then
                        If Not IsDBNull(ds.Tables(0)(tbRadarDate.Value - 1)(s.Name)) Then
                            Dim xvalue As Double
                            xvalue = ds.Tables(0)(tbRadarDate.Value - 1)(s.Name)
                            Dim e As New Element
                            e.XValue = xvalue
                            e.YValue = 1
                            s.Elements.Add(e)
                            s.DefaultElement.Color = Color.FromArgb(255, GetColor(s.Name))
                            s.DefaultElement.Marker.Type = ElementMarkerType.Square

                            lst_Colors.Add(s.DefaultElement.Color)
                            sc.Add(s)
                        End If
                    End If
                End If
            Next
            For i = 1 To 4
                Dim s As New Series
                s.Name = "AZIMUTH_S" + i.ToString
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not ds.Tables(0)(tbRadarDate.Value - 1)(s.Name) Is Nothing Then
                        If Not IsDBNull(ds.Tables(0)(tbRadarDate.Value - 1)(s.Name)) Then
                            Dim xvalue As Double
                            xvalue = ds.Tables(0)(tbRadarDate.Value - 1)(s.Name)

                            Dim e As New Element
                            e.XValue = xvalue
                            e.YValue = 1
                            s.Elements.Add(e)
                            s.DefaultElement.Marker.Type = ElementMarkerType.Triangle
                            s.DefaultElement.Color = lst_Colors(j)
                            sc.Add(s)
                            j = j + 1
                        Else
                            j = i - 1
                        End If
                    End If
                End If
            Next
            nc.SeriesCollection.Add(sc)
            nc.RefreshChart()
            nc.Visible = True
            nc.Series.Data = ds
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub RefreshCharts()
        Try
            'clearing charts
            If Not flp_VA_Charts Is Nothing Then
                flp_VA_Charts.Controls.Clear()
                Dim period As String = GetPeriod()
                'adding charts
                For Each nd As PlottedNode In BandPlotted
                    Dim parray()() As String = {
                        New String() {"@site", "IN('" & nd.sitetag & "')"},
                        New String() {"@tech", "IN('" & nd.techtag & "')"},
                        New String() {"@band", "IN('" & nd.bandtag & "')"},
                        New String() {"@period", period}
                    }

                    dsPM_Sites = IOS.DataLibrary.DataAccessorODBC.GetDataSet(GetSQL(3101, parray)(0), GetSQL(3101, parray)(1))
                    'set radar control
                    If dsPM_Sites.Tables.Count > 0 Then
                        tbRadarDate.Properties.Maximum = dsPM_Sites.Tables(0).Rows.Count
                        RadarChart(dsPM_Sites, nd.site, nd.band)
                        RealAndVirtualChart(dsPM_Sites, nd.site, nd.band)
                        VerdictChart(dsPM_Sites, nd.site, nd.band)
                    End If
                    ResizeChartWidth()
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub ResizeChartHeights(ByVal chartheight As Integer)
        Try
            If Not flp_VA_Charts Is Nothing Then
                For Each ch As Chart In flp_VA_Charts.Controls
                    ch.Height = chartheight
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub VerdictChart(ByRef ds As DataSet, ByVal Site As String, ByVal band As String)
        Try
            Dim nc As New Chart
            If Not nc Is Nothing Then
                nc.Tag = Site
                nc.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            End If
            nc.Title = "SITE - BAND: " + Site & " - " + band
            nc.DefaultElement.Marker.Visible = False
            nc.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Right
            nc.LegendBox.DefaultEntry.Value = ""
            nc.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
            nc.LegendBox.Visible = True
            nc.XAxis.TickLabelMode = TickLabelMode.Angled
            nc.XAxis.TickLabelAngle = 45
            nc.XAxis.Minimum = 0
            nc.XAxis.Maximum = 0

            nc.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
            nc.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

            nc.ToolTip.InitialDelay = 1
            nc.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
            nc.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
            nc.CleanupPeriod = 1

            nc.XAxis.TimeScaleLabels.RangeIntervals.Clear()
            nc.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
            nc.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
            nc.XAxis.TimeInterval = TimeInterval.Days
            nc.XAxis.FormatString = "dd/MM/yy"
            nc.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
            nc.XAxis.TimeInterval = TimeInterval.Days
            nc.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
            nc.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"

            nc.Width = flp_VA_Charts.Width - 25
            nc.Height = tbcChartHeight.Value

            flp_VA_Charts.Controls.Add(nc)
            flp_VA_Charts.VerticalScroll.Visible = True
            flp_VA_Charts.VerticalScroll.Enabled = True

            Dim chart_elements() As String = {"0"}
            ReDim Preserve chart_elements(0)
            chart_elements(0) = "Verdict"

            Dim de As DataEngine = New DataEngine(ds.Tables(0))
            de.DataFields = String2DataFields(chart_elements, "PERIOD_START_TIME")
            Dim sc As SeriesCollection = de.GetSeries()
            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Line
                sc(i).Line.Width = 3
                sc(i).DefaultElement.Color = Color.FromArgb(255, Color.Red)
            Next
            nc.SeriesCollection.Clear()
            nc.SeriesCollection.Add(sc)
            nc.Series.Data = ds
            flp_VA_Charts.SetFlowBreak(nc, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub RealAndVirtualChart(ByRef ds As DataSet, ByVal Site As String, ByVal band As String)
        Try
            Dim nc As New Chart
            If Not nc Is Nothing Then
                nc.Tag = Site
                nc.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            End If

            nc.Title = "SITE - BAND: " + Site & " - " + band
            nc.DefaultElement.Marker.Visible = False
            nc.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Right
            nc.LegendBox.DefaultEntry.Value = ""
            nc.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
            nc.LegendBox.Visible = True
            nc.XAxis.TickLabelMode = TickLabelMode.Angled
            nc.XAxis.TickLabelAngle = 45
            nc.XAxis.Minimum = 0
            nc.XAxis.Maximum = 0

            nc.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
            nc.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart


            nc.ToolTip.InitialDelay = 1
            nc.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
            nc.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
            nc.CleanupPeriod = 1

            nc.XAxis.TimeScaleLabels.RangeIntervals.Clear()
            nc.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
            nc.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
            nc.XAxis.TimeInterval = TimeInterval.Days
            nc.XAxis.FormatString = "dd/MM/yy"
            nc.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
            nc.XAxis.TimeInterval = TimeInterval.Days
            nc.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
            nc.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"

            nc.Width = flp_VA_Charts.Width - 25
            nc.Height = tbcChartHeight.Value

            flp_VA_Charts.Controls.Add(nc)
            flp_VA_Charts.VerticalScroll.Visible = True
            flp_VA_Charts.VerticalScroll.Enabled = True

            Dim chart_elements() As String = {"0"}
            ReDim Preserve chart_elements(7)
            chart_elements(0) = "V_Angle_S1"
            chart_elements(1) = "V_Angle_S2"
            chart_elements(2) = "V_Angle_S3"
            chart_elements(3) = "V_Angle_S4"
            chart_elements(4) = "AZIMUTH_S1"
            chart_elements(5) = "AZIMUTH_S2"
            chart_elements(6) = "AZIMUTH_S3"
            chart_elements(7) = "AZIMUTH_S4"

            Dim de As DataEngine = New DataEngine(ds.Tables(0))
            de.DataFields = String2DataFields(chart_elements, "PERIOD_START_TIME")
            Dim sc As SeriesCollection = de.GetSeries()
            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Line
                sc(i).Line.Width = 3
                sc(i).DefaultElement.Color = Color.FromArgb(255, GetColor(chart_elements(i)))
                If chart_elements(i).Contains("AZIMUTH") Then
                    sc(i).DefaultElement.Marker.Type = ElementMarkerType.Triangle
                    sc(i).DefaultElement.Marker.Visible = True
                End If
            Next
            nc.SeriesCollection.Clear()
            nc.SeriesCollection.Add(sc)
            nc.Series.Data = ds
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub LoadFromMapSelection(ByVal locationid As String, ByVal carrier As String, ByVal tech As String)
        'set text field:
        txtSite.Text = locationid
        Application.DoEvents()
        Dim objectidtocheck As String = locationid + "_" + carrier
        Dim parentidtocheck As String = locationid + "_" + tech.ToUpper
        Dim dr() As DataRow = dsTree_Sites.Tables(0).Select("ObjectID='" & objectidtocheck & "' AND ParentID='" & parentidtocheck & "'")

        If dr.Count > 0 Then
            Dim band As String = dr(0)("ObjectName")
            Dim nd As TreeNode() = tvObjectTree.Nodes.Find(band, True)
            If nd.Count > 0 Then
                For Each n As TreeNode In nd
                    If n.Parent.Tag = parentidtocheck Then
                        If n.Checked = False Then
                            n.Checked = True
                        End If
                    End If
                Next
                Application.DoEvents()
            End If
        End If
    End Sub

    Private Sub AddAxisMarker(ByRef ch As Chart, ByVal coldate As DateTime)
        ch.XAxis.Markers.Clear()
        Dim datestart As DateTime = coldate
        Dim cl As Color = Color.Black
        Dim am4 As New AxisMarker("", New Line(cl, 4), datestart)
        am4.LegendEntry.Visible = False

        am4.BringToFront = True
        ch.XAxis.Markers.Add(am4)
        ch.RefreshChart()
    End Sub

#End Region

End Class