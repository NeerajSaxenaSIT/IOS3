Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports dotnetCHARTING.WinForms
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraVerticalGrid
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraVerticalGrid.Events
Imports DevExpress.XtraGrid.Views.Base

Public Class frmCapacity

#Region "Variables"

    Private dtCongestionJob As DataTable = Nothing
    Private dtCongJobDetails As DataTable = Nothing
    Private dtCongObjTree As DataTable = Nothing
    Public selectedJobID As Integer = Nothing
    Private selectedJobName As String = Nothing
    Public selectedRuleID As Integer = Nothing
    Private selectedCongRuleID As Integer = Nothing
    Private iosTech As String = Nothing
    Private counterType As String = Nothing
    Private selectedChartDate As String = Nothing
    Private objfrmTech As frmTechnology = Nothing
    Public congestionJobRunDate As Date = Nothing
    Public congestionRuleRunDate As Date = Nothing
    Private _isResizing As Boolean = False
    Private imgListJob As New ImageList
    Private jobNodeChanged As Boolean = False
    Private dtCapChart3 As New DataTable
    Private techChart3 As String = Nothing
    Private targetTypeChart3 As String = Nothing
    Private objectNameChart3 As String = Nothing
    Private clickedCongDate_Chart1 As DateTime = Nothing
    Private clickedCongDate_Chart2 As DateTime = Nothing
    Private jobConfigChangeDate() As DateTime = {}
    Private congRuleIdsInCategory As String = Nothing

    Private riCmb As RepositoryItemComboBox
    Private riEvalPeriodInterval As RepositoryItemComboBox
    Private riExcDays As RepositoryItemCheckedComboBoxEdit
    Private riseOcc As RepositoryItemSpinEdit
    Private riseEvalWinDays As RepositoryItemSpinEdit
    Private riseHrlyMinOcc As RepositoryItemSpinEdit
    Private rimeChangesSettings As RepositoryItemMemoEdit
    Private rideStartDate As RepositoryItemDateEdit
    Private rideEndDate As RepositoryItemDateEdit
    Private riExcHours As RepositoryItemCheckedComboBoxEdit

    Private ExtraLegendEntryCollectionC2 As New Dictionary(Of String, LegendEntryCollection)
    Private DefaultSeriesCollectionC2 As New Dictionary(Of String, SeriesCollection)
    Private ExtraLegendEntryCollectionC3 As New Dictionary(Of String, LegendEntryCollection)
    Private DefaultSeriesCollectionC3 As New Dictionary(Of String, SeriesCollection)
    Private ExtraLegendEntryCollectionC4 As New Dictionary(Of String, LegendEntryCollection)
    Private DefaultSeriesCollectionC4 As New Dictionary(Of String, SeriesCollection)

#End Region

#Region "Methods"

    Private Sub LoadObjectTree()
        tlObjectTree.SuspendLayout()

        RemoveHandler tlObjectTree.NodeChanged, AddressOf tlObjectTree_NodeChanged
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3001, parray)(0)
        sqlParam = GetSQL(3001, parray)(1)

        dtCongObjTree = New DataTable()
        dtCongObjTree = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        FillObjectTreeList(tlObjectTree)
        AddHandler tlObjectTree.NodeChanged, AddressOf tlObjectTree_NodeChanged

        tlObjectTree.ResumeLayout()
    End Sub

    Private Sub FillObjectTreeList(ByRef tl As TreeList)
        Try
            tl.Cursor = Cursors.WaitCursor
            tl.BeginUnboundLoad()
            Application.DoEvents()

            tl.PupulateTreeListColumn({"ObjectID", "ParentID", "ObjectName", "ObjectType", "ImageIndex"})
            tl.Nodes.Clear()

            Dim tlNode As TreeListNode = tl.Nodes.Add(New Object() {"PLMN", "0", "PLMN", "PLMN", 0})

            Dim rootNodes As DataRow() = dtCongObjTree.Select("ParentID='PLMN'")
            For Each dr As DataRow In rootNodes
                Dim subNode As TreeListNode = tl.AppendNode(New Object() {dr("ObjectID"), dr("ParentID"), dr("ObjectName"), dr("ObjectType")}, tlNode)
                PopulateObjectTreeList(tl, dr("ObjectID"), subNode, dtCongObjTree)
            Next

        Catch ex As Exception
        Finally
            tl.EndUnboundLoad()
            If tl.Nodes.Count > 0 Then
                tl.SelectNode(tl.Nodes(0))
                tl.SetFocusedNode(tl.Nodes(0))
                tl.CollapseAll()
                tl.ExpandToLevel(0)
            End If
            tl.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Public Sub PopulateObjectTreeList(ByRef tl As TreeList, ParentID As String, rNode As TreeListNode, dt As DataTable)
        Dim foundRows() As DataRow = Nothing
        foundRows = dtCongObjTree.Select("ParentID = " & Chr(39) & ParentID & Chr(39))
        Dim dsObjectTree As New DataSet

        If foundRows.Length > 0 Then
            For Each row As DataRow In foundRows
                If row.Item(0).ToString <> "" Then
                    Dim parentnode As TreeListNode = tl.AppendNode(New Object() {row.Item("ObjectID"), row.Item("ParentID"), row.Item("ObjectName"), row.Item("ObjectType")}, rNode)
                    PopulateObjectTreeList(tl, row.Item("ObjectID"), parentnode, dtCongObjTree)
                    parentnode.ExpandAll()
                End If
            Next row
        End If
    End Sub

    Private Sub LoadCongestionJobs()
        tlCongestionJobs.SuspendLayout()

        RemoveHandler tlCongestionJobs.FocusedNodeChanged, AddressOf tlCongestionJobs_FocusedNodeChanged
        RemoveHandler tlCongestionJobs.NodeChanged, AddressOf tlCongestionJobs_NodeChanged
        RemoveHandler tlCongestionJobs.CellValueChanged, AddressOf tlCongestionJobs_CellValueChanged
        RemoveHandler tlCongestionJobs.DoubleClick, AddressOf tlCongestionJobs_DoubleClick

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3000, parray)(0)
        sqlParam = GetSQL(3000, parray)(1)

        dtCongestionJob = New DataTable()
        dtCongestionJob = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        FillCongestionJobTreeList(tlCongestionJobs)

        AddHandler tlCongestionJobs.CellValueChanged, AddressOf tlCongestionJobs_CellValueChanged
        AddHandler tlCongestionJobs.NodeChanged, AddressOf tlCongestionJobs_NodeChanged
        AddHandler tlCongestionJobs.FocusedNodeChanged, AddressOf tlCongestionJobs_FocusedNodeChanged
        AddHandler tlCongestionJobs.DoubleClick, AddressOf tlCongestionJobs_DoubleClick

        tlCongestionJobs.ResumeLayout()
    End Sub

    Private Sub DeleteCongestionJob(jobID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", CInt(jobID)}
        }

        strConnection = GetSQL(3032, parray)(0)
        sqlParam = GetSQL(3032, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub FillCongestionJobTreeList(ByRef tl As TreeList)
        Try
            tl.Cursor = Cursors.WaitCursor
            tl.BeginUnboundLoad()
            Application.DoEvents()

            tl.PupulateTreeListColumn({"ObjectID", "ParentID", "ObjectName", "ObjectType", "ImageIndex"})
            tl.Nodes.Clear()

            Dim rootNodes As DataRow() = dtCongestionJob.Select("ParentID='0'")
            dtCongestionJobsList.Rows.Clear()
            For Each dr As DataRow In rootNodes
                Dim drow As DataRow = dtCongestionJobsList.NewRow()
                drow("CapJobName") = dr("ObjectName")
                dtCongestionJobsList.Rows.Add(drow)

                Dim tlNode As TreeListNode = tl.Nodes.Add(New Object() {dr("ObjectID"), "0", dr("ObjectName"), dr("ObjectType"), 0})
                'set congestion job node (root node) id as tag value
                tlNode.Tag = dr("ObjectID").ToString.Split("_")(0)
                PopulateTreeList(tl, dr("ObjectID"), tlNode, dtCongestionJob)
            Next

        Catch ex As Exception
        Finally
            tl.EndUnboundLoad()
            If tl.Nodes.Count > 0 Then
                tl.SelectNode(tl.Nodes(0))
                tl.SetFocusedNode(tl.Nodes(0))
                tl.CollapseAll()
                tl.ExpandToLevel(0)
            End If
            tl.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Public Sub PopulateTreeList(ByRef tl As TreeList, ParentID As String, rNode As TreeListNode, dt As DataTable)
        Dim foundRows() As DataRow = Nothing
        foundRows = dtCongestionJob.Select("ParentID = " & Chr(39) & ParentID & Chr(39))
        Dim dsObjectTree As New DataSet

        If foundRows.Length > 0 Then
            Dim imgList As ImageList = tl.SelectImageList
            Dim index As Integer = imgList.Images.IndexOfKey("EMPTY")
            For Each row As DataRow In foundRows
                If row.Item(0).ToString <> "" Then
                    If imgList IsNot Nothing Then
                        index = imgList.Images.IndexOfKey(row.Item(3).ToString)
                    End If
                    Dim parentnode As TreeListNode = tl.AppendNode(New Object() {row.Item("ObjectID"), row.Item("ParentID"), row.Item("ObjectName"), row.Item("ObjectType"), index}, rNode)
                    'set tag id for congestion rule
                    If row.Item("ObjectType").ToString = "CapCongRule" Then
                        parentnode.Tag = row("ObjectID").ToString.Split("_")(0)
                    End If
                    PopulateTreeList(tl, row.Item(0), parentnode, dtCongestionJob)
                    parentnode.ExpandAll()
                End If
            Next row
        End If
    End Sub

    Private Sub LoadCategories()
        RemoveHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3002, parray)(0)
        sqlParam = GetSQL(3002, parray)(1)

        Dim dt As New DataTable()
        dt = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbCategory, dt, "CapJobCategoryID", "CapJobCategoryName", "Select Category", True)

        AddHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
    End Sub

    Private Sub GetSelectedJobDetails()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", Chr(39) & selectedJobID & Chr(39)}
        }

        strConnection = GetSQL(3008, parray)(0)
        sqlParam = GetSQL(3008, parray)(1)
        dtCongJobDetails = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadCongestionJobDetails()
        GetSelectedJobDetails()

        RemoveHandler ceIsLocked.CheckedChanged, AddressOf ceIsLocked_CheckedChanged
        RemoveHandler ceIsEnabled.CheckedChanged, AddressOf ceIsEnabled_CheckedChanged
        RemoveHandler ceIsScheduled.CheckedChanged, AddressOf ceIsScheduled_CheckedChanged

        If dtCongJobDetails IsNot Nothing Then
            lblSelectedJob.Text = dtCongJobDetails.Rows(0)("CapJobName").ToString
            lblOwner.Text = IIf(IsDBNull(dtCongJobDetails.Rows(0)("Owner")), "", dtCongJobDetails.Rows(0)("Owner").ToString)
            ceIsEnabled.Checked = IIf(IsDBNull(dtCongJobDetails.Rows(0)("IsEnabled")), False, CBool(dtCongJobDetails.Rows(0)("IsEnabled")))
            ceIsLocked.Checked = IIf(IsDBNull(dtCongJobDetails.Rows(0)("IsLocked")), False, CBool(dtCongJobDetails.Rows(0)("IsLocked")))
            ceIsScheduled.Checked = IIf(IsDBNull(dtCongJobDetails.Rows(0)("IsScheduled")), False, CBool(dtCongJobDetails.Rows(0)("IsScheduled")))

            'congestion job is locked
            If ceIsLocked.Checked = True Then
                'job owner is logged in user
                If lblOwner.Text.Trim.ToUpper = Environment.UserName.ToUpper Then
                    tlpConfig.Enabled = True
                    ceIsLocked.Enabled = True
                    ceIsEnabled.Enabled = True
                    ceIsScheduled.Enabled = True
                End If
                'logged in user is power user
                If configMgr.User.IsPowerUser = True Then
                    tlpConfig.Enabled = True
                    ceIsLocked.Enabled = True
                    ceIsEnabled.Enabled = True
                    ceIsScheduled.Enabled = True
                Else
                    'logged in user is not power user
                    tlpConfig.Enabled = False
                    tlpConfig.Enabled = False
                    ceIsLocked.Enabled = False
                    ceIsEnabled.Enabled = False
                    ceIsScheduled.Enabled = False
                End If
            Else
                'congestion job is not locked
                tlpConfig.Enabled = True
                ceIsLocked.Enabled = True
                ceIsEnabled.Enabled = True
                ceIsScheduled.Enabled = True
            End If
        End If

        AddHandler ceIsLocked.CheckedChanged, AddressOf ceIsLocked_CheckedChanged
        AddHandler ceIsEnabled.CheckedChanged, AddressOf ceIsEnabled_CheckedChanged
        AddHandler ceIsScheduled.CheckedChanged, AddressOf ceIsScheduled_CheckedChanged
    End Sub

    Private Sub ClearChart3AndChart4()
        'Remove chart 3 & clear chart 3 grid
        chartCapacity3.SeriesCollection.Clear()
        chartCapacity3.RefreshChart()
        IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject3)
        'Remove chart 4 & clear chart 4 grid
        chartCapacity4.SeriesCollection.Clear()
        chartCapacity4.RefreshChart()
        IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject4)
    End Sub

    Private Sub LoadReportingChart1()
        Dim k As Integer = 0
        Dim filterQry As String = GetFilterQueryFromObjectTree()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        Dim congRuleID As String = Nothing
        If selectedRuleID <> Nothing Then
            congRuleID = "IN (" & selectedRuleID.ToString & ")"
        Else
            If congRuleIdsInCategory Is Nothing Then
                congRuleID = Nothing
            Else
                congRuleID = congRuleIdsInCategory
            End If
        End If

        Dim parray()() As String = {
            New String() {"@capJobID", CInt(dtCongJobDetails.Rows(0)("CapJobID"))},
            New String() {"@capCongRuleID", IIf(Not congRuleID Is Nothing, Chr(39) & congRuleID & Chr(39), "NULL")},
            New String() {"@filter", IIf(filterQry IsNot Nothing, Chr(39) & filterQry & Chr(39), "NULL")}
        }

        strConnection = GetSQL(3022, parray)(0)
        sqlParam = GetSQL(3022, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdObject1, gvObject1, dt, "ALL")

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            clickedCongDate_Chart1 = dt.Select("CongestionDate=MAX(CongestionDate)")(0)("CongestionDate")

            chartCapacity1.Height = tbChartHeightStats.Value
            chartCapacity1.SuspendLayout()
            SetDefaultSettingsForChart(chartCapacity1)

            chartCapacity1.TitleBox.Label.Text = "Objects: " & selectedJobName
            chartCapacity1.TitleBox.HeaderLabel.Text = "Capacity - Number of Congested Objects In Job"
            chartCapacity1.TitleBox.Label.Alignment = StringAlignment.Near
            chartCapacity1.TitleBox.Label.LineAlignment = StringAlignment.Near
            chartCapacity1.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
            chartCapacity1.Annotations.Clear()
            chartCapacity1.Annotations.Add(New Annotation(""))
            chartCapacity1.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
            'chartCapacity1.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
            'chartCapacity1.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
            chartCapacity1.Dock = DockStyle.Fill

            Dim chart_elements() As String = Nothing
            k = 0
            For Each dcol As DataColumn In dt.Columns
                If dcol.ColumnName.ToUpper.Trim <> "CONGESTIONDATE" Then
                    ReDim Preserve chart_elements(k)
                    chart_elements(k) = dcol.ColumnName
                    k = k + 1
                End If
            Next

            Dim de As DataEngine = New DataEngine(dt)
            de.DataFields = String2DataFields(chart_elements, "CongestionDate")
            de.DataGridFormatString = "N2"
            de.FormatString = "dd/MM/yyyy"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()

            Dim i As Integer = 0
            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Bar
                sc(i).DefaultElement.Color = Color.DarkOrange
            Next

            chartCapacity1.SeriesCollection.Clear()
            chartCapacity1.SeriesCollection.Add(sc)
            chartCapacity1.Series.Data = dt

            dt.Dispose()
            dt = Nothing

            chartCapacity1.XAxis.Markers.Clear()
            'add configuration changes on the chart with axis marker
            LoadCapJobChanges(False)

            chartCapacity1.RefreshChart()
            chartCapacity1.ResumeLayout()
        Else
            chartCapacity1.SeriesCollection.Clear()
            chartCapacity1.RefreshChart()

            chartCapacity3.SeriesCollection.Clear()
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject3)
            chartCapacity4.SeriesCollection.Clear()
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject4)
        End If
    End Sub

    Private Sub LoadReportingChart2()
        DefaultSeriesCollectionC2.Clear()
        ExtraLegendEntryCollectionC2.Clear()

        DefaultSeriesCollectionC3.Clear()
        ExtraLegendEntryCollectionC3.Clear()

        DefaultSeriesCollectionC4.Clear()
        ExtraLegendEntryCollectionC4.Clear()

        Dim k As Integer = 0
        Dim color_R As Integer = 0, color_B As Integer = 0, color_G As Integer = 0
        Dim filterQry As String = GetFilterQueryFromObjectTree()

        Dim congRuleID As String = Nothing
        If selectedRuleID <> Nothing Then
            congRuleID = "IN (" & selectedRuleID.ToString & ")"
        Else
            If congRuleIdsInCategory Is Nothing Then
                congRuleID = Nothing
            Else
                congRuleID = congRuleIdsInCategory
            End If
        End If

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", CInt(dtCongJobDetails.Rows(0)("CapJobID"))},
            New String() {"@capCongRuleID", IIf(Not congRuleID Is Nothing, Chr(39) & congRuleID & Chr(39), "NULL")},
            New String() {"@capCongestionDate", IIf(clickedCongDate_Chart1 <> Nothing, Chr(39) & clickedCongDate_Chart1.ToString("yyyy-MM-dd") & Chr(39), "NULL")},
            New String() {"@filter", IIf(filterQry IsNot Nothing, Chr(39) & filterQry & Chr(39), "NULL")}
        }

        strConnection = GetSQL(3023, parray)(0)
        sqlParam = GetSQL(3023, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdObject2, gvObject2, dt, "ALL")

        If dt.Rows.Count > 0 Then
            Dim dtDistinctRules As DataTable = dt.DefaultView.ToTable(True, "CapCongestionRuleName")
            Dim dtDistinctDate As DataTable = dt.DefaultView.ToTable(True, "CongestionDate")

            Dim dtCopy As New DataTable
            dtCopy.Columns.Add("CongestionDate", GetType(System.DateTime))

            For Each dr As DataRow In dtDistinctRules.Rows
                dtCopy.Columns.Add(dr("CapCongestionRuleName"), GetType(String))
            Next

            For Each dr As DataRow In dtDistinctDate.Rows
                Dim drw() As DataRow = dt.Select("CongestionDate='" & dr("CongestionDate") & "'")
                Dim drow As DataRow = dtCopy.NewRow()
                For Each dr1 As DataRow In drw
                    drow(dr1("CapCongestionRuleName")) = dr1("TriggeredCount").ToString
                Next
                drow("CongestionDate") = drw(0)("CongestionDate").ToString
                dtCopy.Rows.Add(drow)
            Next

            chartCapacity2.XAxis.Markers.Clear()
            chartCapacity2.Height = tbChartHeightStats.Value

            chartCapacity2.SuspendLayout()
            SetDefaultSettingsForChart(chartCapacity2)

            If dtCopy IsNot Nothing AndAlso dtCopy.Rows.Count > 0 Then

                chartCapacity2.TitleBox.Label.Text = "Objects: " & selectedJobName
                chartCapacity2.TitleBox.HeaderLabel.Text = "Capacity - Number of Congested Objects In Job Per Day in Evaluation Window"
                chartCapacity2.TitleBox.Label.Alignment = StringAlignment.Near
                chartCapacity2.TitleBox.Label.LineAlignment = StringAlignment.Near
                chartCapacity2.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
                chartCapacity2.Annotations.Clear()
                chartCapacity2.Annotations.Add(New Annotation(""))
                chartCapacity2.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                chartCapacity2.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                chartCapacity2.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
                chartCapacity2.Dock = DockStyle.Fill

                Dim chart_elements() As String = Nothing
                k = 0
                For Each dcol As DataColumn In dtCopy.Columns
                    If dcol.ColumnName.ToUpper.Trim <> "CONGESTIONDATE" Then
                        ReDim Preserve chart_elements(k)
                        chart_elements(k) = dcol.ColumnName
                        k = k + 1
                    End If
                Next

                Dim de As DataEngine = New DataEngine(dtCopy)
                de.DataFields = String2DataFields(chart_elements, "CongestionDate")
                de.DataGridFormatString = "N2"
                de.FormatString = "dd/MM/yyyy"

                Dim sc As New SeriesCollection
                sc = de.GetSeries()
                sc.Sort(ElementValue.YValue, "ASC")

                Dim rnd As Random = New Random(15)
                Dim i As Integer = 0
                For i = 0 To sc.Count() - 1
                    sc(i).Type = SeriesType.Bar
                    sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
                Next

                chartCapacity2.SeriesCollection.Clear()
                chartCapacity2.SeriesCollection.Add(sc)
                chartCapacity2.Series.Data = dtCopy

                dtCopy.Dispose()
                dtCopy = Nothing

                chartCapacity2.RefreshChart()
                chartCapacity2.ResumeLayout()
            End If
        Else
            chartCapacity2.SeriesCollection.Clear()
            chartCapacity2.RefreshChart()

            chartCapacity3.SeriesCollection.Clear()
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject3)
            chartCapacity4.SeriesCollection.Clear()
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject4)
        End If
    End Sub

    Private Sub LoadReportingChart3(ByVal congestionDate_Chart1 As DateTime, ByVal congestionDate_Chart2 As DateTime)
        DefaultSeriesCollectionC3.Clear()
        ExtraLegendEntryCollectionC3.Clear()

        DefaultSeriesCollectionC4.Clear()
        ExtraLegendEntryCollectionC4.Clear()

        Dim k As Integer = 0
        Dim color_R As Integer = 0, color_B As Integer = 0, color_G As Integer = 0
        Dim filterQry As String = GetFilterQueryFromObjectTree()

        Dim congRuleID As String = Nothing
        If selectedRuleID <> Nothing Then
            congRuleID = "IN (" & selectedRuleID.ToString & ")"
        Else
            If congRuleIdsInCategory Is Nothing Then
                congRuleID = Nothing
            Else
                congRuleID = congRuleIdsInCategory
            End If
        End If

        Dim dtTemp As DataTable = Nothing
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", CInt(dtCongJobDetails.Rows(0)("CapJobID"))},
            New String() {"@capCongestionDate_Chart1", Chr(39) & congestionDate_Chart1.ToString("yyyy-MM-dd") & Chr(39)},
            New String() {"@capCongestionDate_Chart2", IIf(congestionDate_Chart2 <> Nothing, Chr(39) & congestionDate_Chart2.ToString("yyyy-MM-dd") & Chr(39), "NULL")},
            New String() {"@capCongRuleID", IIf(Not congRuleID Is Nothing, Chr(39) & congRuleID & Chr(39), "NULL")},
            New String() {"@filter", IIf(filterQry IsNot Nothing, Chr(39) & filterQry & Chr(39), "NULL")}
        }

        strConnection = GetSQL(3024, parray)(0)
        sqlParam = GetSQL(3024, parray)(1)
        dtCapChart3 = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdObject3, gvObject3, dtCapChart3, "ALL")

        techChart3 = dtCapChart3.Rows(0)("IOS_TECH").ToString
        targetTypeChart3 = dtCapChart3.Rows(0)("CounterType").ToString

        'Apply objects filter
        Dim ObjectsCharted As String = "'"
        For Each tln As TreeListNode In tlObjectTree.GetAllCheckedNodes().Where(Function(nd) nd.Level = 2).ToList()
            ObjectsCharted = ObjectsCharted + tln.Item(2) + "','"
        Next

        dtTemp = dtCapChart3

        If ObjectsCharted <> "'" Then
            ObjectsCharted = ObjectsCharted.Substring(0, ObjectsCharted.Length - 2)
            If dtCapChart3.Select("ObjectName IN (" & ObjectsCharted & ")").Count > 0 Then
                dtTemp = dtCapChart3.Select("ObjectName In (" & ObjectsCharted & ")").CopyToDataTable()
            End If
        End If

        If dtTemp.Rows.Count > 0 Then
            Dim dtDistinctRules As DataTable = dtTemp.DefaultView.ToTable(True, "CapCongestionRuleName")
            Dim dtDistinctObjects As DataTable = dtCapChart3.DefaultView.ToTable(True, "ObjectName")

            Dim dtCopy As New DataTable
            dtCopy.Columns.Add("ObjectName", GetType(String))

            For Each dr As DataRow In dtDistinctRules.Rows
                dtCopy.Columns.Add(dr("CapCongestionRuleName"), GetType(String))
            Next
            dtCopy.Columns.Add("TotalOccurrencesInDay", GetType(Int32))

            For Each dr As DataRow In dtDistinctObjects.Rows
                Dim drw() As DataRow = dtCapChart3.Select("ObjectName='" & dr("ObjectName") & "'")
                Dim drow As DataRow = dtCopy.NewRow()
                Dim TotalOcc As Integer = 0
                For Each dr1 As DataRow In drw
                    drow(dr1("CapCongestionRuleName")) = dr1("OccurrencesInDay").ToString
                    TotalOcc = TotalOcc + CInt(dr1("OccurrencesInDay"))
                Next
                drow("ObjectName") = drw(0)("ObjectName").ToString
                drow("TotalOccurrencesInDay") = TotalOcc
                dtCopy.Rows.Add(drow)
            Next

            dtCopy.DefaultView.Sort = "TotalOccurrencesInDay DESC"
            Dim dtCopySorted As DataTable = dtCopy.DefaultView.ToTable()

            chartCapacity3.Height = tbChartHeightStats.Value
            chartCapacity3.SuspendLayout()
            SetDefaultSettingsForChart(chartCapacity3)
            If dtCopySorted IsNot Nothing Then

                chartCapacity3.TitleBox.Label.Text = "Objects: " & selectedJobName
                chartCapacity3.TitleBox.HeaderLabel.Text = "Capacity - Count of congestion rule breach occurred per object on " + IIf(congestionDate_Chart2 <> Nothing, congestionDate_Chart2.ToString("yyyy-MM-dd"), congestionDate_Chart1.ToString("yyyy-MM-dd"))
                chartCapacity3.TitleBox.Label.Alignment = StringAlignment.Near
                chartCapacity3.TitleBox.Label.LineAlignment = StringAlignment.Near
                chartCapacity3.DefaultElement.Hotspot.ToolTip = "ObjectName: %XValue" & Chr(13) & "%SeriesName: %Value "
                chartCapacity3.Annotations.Clear()
                chartCapacity3.Annotations.Add(New Annotation(""))
                chartCapacity3.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                chartCapacity3.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                chartCapacity3.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
                chartCapacity3.Dock = DockStyle.Fill

                Dim chart_elements() As String = Nothing
                k = 0
                For Each dcol As DataColumn In dtCopySorted.Columns
                    If dcol.ColumnName.ToUpper.Trim <> "OBJECTNAME" And dcol.ColumnName.Trim <> "TotalOccurrencesInDay" Then
                        ReDim Preserve chart_elements(k)
                        chart_elements(k) = dcol.ColumnName
                        k = k + 1
                    End If
                Next

                Dim de As DataEngine = New DataEngine(dtCopySorted)
                de.DataFields = String2DataFields(chart_elements, "ObjectName")
                de.DataGridFormatString = "N2"

                Dim sc As New SeriesCollection
                sc = de.GetSeries()
                'sc.Sort(ElementValue.YValue, "ASC")

                Dim rnd As Random = New Random(10)
                Dim i As Integer = 0
                For i = 0 To sc.Count() - 1
                    sc(i).Type = SeriesType.Bar
                    sc(i).EmptyElement.Mode = EmptyElementMode.TreatAsZero
                    sc(i).DefaultElement.Color = chartCapacity2.SeriesCollection.GetSeries(sc(i).Name).DefaultElement.Color
                Next

                chartCapacity3.SeriesCollection.Clear()
                chartCapacity3.SeriesCollection.Add(sc)
                chartCapacity3.Series.Data = dtCopy

                dtCopy.Dispose()
                dtCopy = Nothing
                dtCopySorted.Dispose()
                dtCopySorted = Nothing

                chartCapacity3.RefreshChart()
                chartCapacity3.ResumeLayout()
            End If
        Else
            chartCapacity3.SeriesCollection.Clear()
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject3)
        End If
    End Sub

    Private Sub LoadReportingChart4(ByVal objectName As String)
        DefaultSeriesCollectionC4.Clear()
        ExtraLegendEntryCollectionC4.Clear()

        Dim k As Integer = 0
        Dim color_R As Integer = 0, color_B As Integer = 0, color_G As Integer = 0

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim clickedCongDate As Date
        clickedCongDate = clickedCongDate_Chart1

        Dim parray()() As String = {
            New String() {"@CapJobId", Chr(39) & CInt(dtCongJobDetails.Rows(0)("CapJobID")) & Chr(39)},
            New String() {"@objectName", Chr(39) & objectName & Chr(39)},
            New String() {"@congestionDate", Chr(39) & clickedCongDate.ToString("yyyy-MM-dd") & Chr(39)}
        }

        strConnection = GetSQL(3026, parray)(0)
        sqlParam = GetSQL(3026, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dt.Rows.Count > 0 Then
            Dim dtDistinctKPIs As DataTable = dt.DefaultView.ToTable(True, "KPIName")
            Dim dtDistinctDate As DataTable = dt.DefaultView.ToTable(True, "CongestionDate")
            Dim dtCopy As New DataTable
            dtCopy.Columns.Add("CongestionDate", GetType(DateTime))

            For Each dr As DataRow In dtDistinctKPIs.Rows
                dtCopy.Columns.Add(dr("KPIName"), GetType(String))
            Next

            For Each dr As DataRow In dtDistinctDate.Rows
                Dim drw() As DataRow = dt.Select("CongestionDate='" & dr("CongestionDate") & "'")
                Dim drow As DataRow = dtCopy.NewRow()
                For Each dr1 As DataRow In drw
                    drow(dr1("KPIName")) = dr1("KPIValue").ToString
                Next
                drow("CongestionDate") = drw(0)("CongestionDate")
                dtCopy.Rows.Add(drow)
            Next

            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdObject4, gvObject4, dtCopy, "ALL")

            chartCapacity4.Height = tbChartHeightStats.Value
            chartCapacity4.SuspendLayout()
            SetDefaultSettingsForChart(chartCapacity4)

            chartCapacity4.XAxis.TimeInterval = TimeInterval.Hours
            chartCapacity4.XAxis.FormatString = "dd/MM/yyyy HH:mm"
            chartCapacity4.XAxis.TimeScaleLabels.HourFormatString = "dd/MM/yyyy HH:mm"
            chartCapacity4.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yyyy HH:mm"
            chartCapacity4.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None

            chartCapacity4.TitleBox.Label.Text = "Objects: " & objectName
            chartCapacity4.TitleBox.HeaderLabel.Text = "KPIs of " & objectName
            chartCapacity4.TitleBox.Label.Alignment = StringAlignment.Near
            chartCapacity4.TitleBox.Label.LineAlignment = StringAlignment.Near
            chartCapacity4.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
            chartCapacity4.Annotations.Clear()
            chartCapacity4.Annotations.Add(New Annotation(""))
            chartCapacity4.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
            chartCapacity4.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
            chartCapacity4.Dock = DockStyle.Fill
            chartCapacity4.DefaultSeries.DefaultElement.Marker.Visible = True

            Dim chart_elements() As String = Nothing
            k = 0
            For Each dcol As DataColumn In dtCopy.Columns
                If dcol.ColumnName.ToUpper.Trim <> "CONGESTIONDATE" Then
                    ReDim Preserve chart_elements(k)
                    chart_elements(k) = dcol.ColumnName
                    k = k + 1
                End If
            Next

            Dim de As DataEngine = New DataEngine(dtCopy)
            de.DataFields = String2DataFields(chart_elements, "CongestionDate")
            de.DataGridFormatString = "N2"
            de.FormatString = "dd/MM/yyyy HH:mm"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()

            Dim rnd As Random = New Random(20)
            Dim i As Integer = 0
            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Line
                sc(i).Line.Width = 3
                sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
                sc(i).DefaultElement.Marker.Type = i + 1
                sc(i).EmptyElement.Mode = EmptyElementMode.None
            Next

            chartCapacity4.SeriesCollection.Clear()
            chartCapacity4.SeriesCollection.Add(sc)
            chartCapacity4.Series.Data = dtCopy

            dtCopy.Dispose()
            dtCopy = Nothing

            chartCapacity4.RefreshChart()
            chartCapacity4.ResumeLayout()
        Else
            chartCapacity4.SeriesCollection.Clear()
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdObject4)
        End If
    End Sub

    Private Sub SetDefaultSettingsForChart(ByRef ch As Chart)
        ch.DefaultElement.Marker.Visible = False
        ch.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        ch.LegendBox.DefaultEntry.Value = ""
        ch.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        ch.LegendBox.Visible = True

        ch.XAxis.TickLabelMode = TickLabelMode.Angled
        ch.XAxis.TickLabelAngle = 45
        ch.XAxis.Minimum = 0
        ch.XAxis.Maximum = 0

        ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

        ch.ToolTip.InitialDelay = 1
        ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        ch.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        ch.CleanupPeriod = 1

        ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
        ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
        ch.XAxis.TimeInterval = TimeInterval.Days
        ch.XAxis.FormatString = "dd/MM/yyyy"
        ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yyyy"
        'ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
        'ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
        ch.LegendBox.Orientation = Orientation.Bottom
        ch.LegendBox.DefaultCorner = BoxCorner.Round
        ch.LegendBox.ExtraEntries.Clear()

        ch.TitleBox.Position = TitleBoxPosition.Full
        ch.TitleBox.CornerTopLeft = BoxCorner.Round
        ch.TitleBox.CornerTopRight = BoxCorner.Round
        ch.TitleBox.Label.AutoWrap = True
        ch.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
    End Sub

    Private Sub SetChartXAxis(ByRef chartObj As dotnetCHARTING.WinForms.Chart, ByVal chartElements As String)
        Try
            chartObj.XAxis.TickLabelMode = TickLabelMode.Angled
            chartObj.XAxis.TickLabelAngle = 45
            If chartElements = "CongestionDate" Or chartElements = "Date" Then
                chartObj.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                chartObj.XAxis.ScaleRange = New ScaleRange()
                chartObj.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                chartObj.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                chartObj.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                chartObj.XAxis.TimeInterval = TimeInterval.Days
                chartObj.XAxis.FormatString = "dd/MM/yy"
                chartObj.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                chartObj.XAxis.TimeInterval = TimeInterval.Days
                chartObj.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                chartObj.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
                chartObj.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
            Else
                chartObj.DefaultElement.Hotspot.ToolTip = "%XValue" & Chr(13) & "%SeriesName: %Value "
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetCongestionRules()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", Chr(39) & selectedJobID & Chr(39)}
        }
        strConnection = GetSQL(3009, parray)(0)
        sqlParam = GetSQL(3009, parray)(1)
        dtCongRule = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadCongestionRuleGrid()
        RemoveHandler gvCongRules.FocusedRowChanged, AddressOf gvCongRules_FocusedRowChanged
        GetCongestionRules()
        Dim hiddenColList() As String = {
            "CapCongestionRuleID", "CapJobID", "CapJobCategoryID", "Occurences", "EvalWindowDays", "IsEnabled", "Score", "ScoreEnabled", "EmailEnabled", "EmailAddresses",
            "ExcludeDays", "ExcludedHours", "RootCauseSelection", "ServiceCategory", "EvalPeriodInterval", "HourlyMinOcc", "HourlyOccConsecutive", "CounterType", "IOS_TECH", "StartDate", "EndDate"
        }

        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdCongRules, gvCongRules, dtCongRule, "ALL", hiddenColList, "CapCongestionRuleName")
        If selectedRuleID <> Nothing Then
            gvCongRules.FocusedRowHandle = gvCongRules.LocateByValue("CapCongestionRuleID", selectedRuleID)
        End If
        AddHandler gvCongRules.FocusedRowChanged, AddressOf gvCongRules_FocusedRowChanged
        gvCongRules_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub UpdateCongRuleProperties(changedPropertyName As String, changedPropertyValue As String)
        Try
            Dim congRuleID As Integer = -1
            Dim drCongRule As DataRow = gvCongRules.GetDataRow(gvCongRules.FocusedRowHandle)
            congRuleID = drCongRule.Item("CapCongestionRuleID")

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@CapCongestionRuleID", congRuleID},
                New String() {"@PropertyName", Chr(39) & changedPropertyName & Chr(39)},
                New String() {"@PropertyValue", Chr(39) & changedPropertyValue & Chr(39)}
            }
            strConnection = GetSQL(3007, parray)(0)
            sqlParam = GetSQL(3007, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ResizeChartHeights(ByVal chartheight As Integer)
        Try
            tlpCharts.AutoScroll = False
            For iCnt As Integer = 0 To tlpCharts.RowCount - 1
                tlpCharts.RowStyles.Item(iCnt).SizeType = SizeType.Absolute
                tlpCharts.RowStyles.Item(iCnt).Height = chartheight
            Next
            tlpCharts.Size = New Size(tlpCharts.Width, chartheight * 4)
            tlpCharts.AutoScroll = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadKPIGrid(ByVal capCongRuleID As Integer)
        RemoveHandler gvKPI.CustomRowCellEdit, AddressOf gvKPI_CustomRowCellEdit
        RemoveHandler gvKPI.ValidatingEditor, AddressOf gvKPI_ValidatingEditor
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CapCongestionRuleID", capCongRuleID}
        }
        strConnection = GetSQL(3014, parray)(0)
        sqlParam = GetSQL(3014, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdKPI, gvKPI, dt, "ALL", {"CapKPIRuleID"}, "KPI_Name")

        riCmb = New Repository.RepositoryItemComboBox()
        Dim items As String() = {"Select Operator", "=", "<>", "<", "<=", ">", ">=", "like"}
        riCmb.Items.AddRange(items)
        AddHandler gvKPI.ValidatingEditor, AddressOf gvKPI_ValidatingEditor
        AddHandler gvKPI.CustomRowCellEdit, AddressOf gvKPI_CustomRowCellEdit
    End Sub

    Private Sub LoadObjectFilterGrid(ByVal capCongRuleID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CapCongestionRuleID", capCongRuleID}
        }
        strConnection = GetSQL(3013, parray)(0)
        sqlParam = GetSQL(3013, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdObjFilter, gvObjFilter, dt, "ALL", {"CapCongestionRuleFilterID"}, "FilterString")
    End Sub

    Private Sub DeleteKPI(ByVal kpiID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capKPIRuleID", kpiID}
        }
        strConnection = GetSQL(3017, parray)(0)
        sqlParam = GetSQL(3017, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub DeleteObjFilter(ByVal filterID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capCongestionRuleFilterID", filterID}
        }
        strConnection = GetSQL(3018, parray)(0)
        sqlParam = GetSQL(3018, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub DeleteCongestionRule(ByVal congRuleID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capCongestionRuleID", congRuleID}
        }
        strConnection = GetSQL(3025, parray)(0)
        sqlParam = GetSQL(3025, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub AddSlidingWindowAxisMarker(Optional startDate As Date = Nothing, Optional endDate As Date = Nothing)
        Try
            Dim chartCapacity2 As Chart = TryCast(sccObject2.Panel1.Controls(0), Chart)
            chartCapacity2.XAxis.Markers.Clear()
            Dim shade As AxisMarker = Nothing
            If startDate <> Nothing AndAlso endDate <> Nothing Then
                shade = New AxisMarker(Format(startDate, "dd/MM/yy") & " - " & Format(endDate, "dd/MM/yy"), New Background(Color.FromArgb(100, Color.Green)), startDate, endDate)
            End If
            shade.LegendEntry.Visible = False
            shade.Label.LineAlignment = StringAlignment.Near
            shade.Label.Alignment = StringAlignment.Center
            shade.LegendEntry.Value = ""
            chartCapacity2.XAxis.Markers.Add(shade)
            chartCapacity2.RefreshChart()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub RunCongestionJob()
        'execute job run sql
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capCongestionJobID", CInt(selectedJobID)},
            New String() {"@runDate", Chr(39) + congestionJobRunDate.ToString("yyyyMMdd") + Chr(39)}
        }
        strConnection = GetSQL(3033, parray)(0)
        sqlParam = GetSQL(3033, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, 1800)
    End Sub

    Public Sub RunCongestionRule()
        'execute rule run sql
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capCongestionJobID", CInt(selectedJobID)},
            New String() {"@capCongestionRuleID", CInt(selectedRuleID)},
            New String() {"@runDate", Chr(39) + congestionRuleRunDate.ToString("yyyyMMdd") + Chr(39)}
        }
        strConnection = GetSQL(3034, parray)(0)
        sqlParam = GetSQL(3034, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, 200)
    End Sub

    Public Sub Charts_ResizeWidth()
        Me.SuspendLayout()
        Dim ch As Chart = Nothing
        Try
            tlpCharts.AutoScroll = False
            IOS.Configuration.ManageResizingControl.DisableHorizontalScrollBar(tlpCharts)
            tlpCharts.AutoScroll = True
        Catch ex As Exception
        End Try
        Me.ResumeLayout()
    End Sub

    Private Function GetTreeImages(ByRef imglistkpi As ImageList) As ImageList
        imglistkpi.Images.Add("CapCat", EmbeddedImage("icon_Category1.png"))
        imglistkpi.Images.Add("CapCongRule", EmbeddedImage("icon_chart.jpg"))
        imglistkpi.Images.Add("CapKPIRule", EmbeddedImage("icon_Element1.png"))
        Return imglistkpi
    End Function

    Private Sub CreateExcludeDaysCombo(ByRef vGridControl As VGridControl, ByVal name As String, ByVal value As String)
        riExcDays = New Repository.RepositoryItemCheckedComboBoxEdit()
        riExcDays.AllowMultiSelect = True
        riExcDays.AutoHeight = False
        Dim items As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        For Each x As String In items
            Dim y As New CheckedListBoxItem()
            y.Value = x.ToString
            If value.Contains(x) Then
                riExcDays.Items.Add(y, CheckState.Checked)
            Else
                riExcDays.Items.Add(y, CheckState.Unchecked)
            End If
        Next
        vGridControl.RepositoryItems.Add(riExcDays)
        AddHandler riExcDays.Closed, AddressOf riExcDays_Closed
    End Sub

    Private Sub CreateExcludeHoursCombo(ByRef vGridControl As VGridControl, ByVal name As String, ByVal value As String)
        riExcHours = New Repository.RepositoryItemCheckedComboBoxEdit()
        riExcHours.AllowMultiSelect = True
        riExcHours.AutoHeight = False
        Dim items As String() = {"00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"}
        For Each x As String In items
            Dim y As New CheckedListBoxItem()
            y.Value = x.ToString
            If value.Contains(x) Then
                riExcHours.Items.Add(y, CheckState.Checked)
            Else
                riExcHours.Items.Add(y, CheckState.Unchecked)
            End If
        Next
        vGridControl.RepositoryItems.Add(riExcHours)
        AddHandler riExcHours.Closed, AddressOf riExcHours_Closed
    End Sub

    Private Sub CreateCombo(ByRef vGridControl As VGridControl, ByVal name As String)
        riEvalPeriodInterval = New Repository.RepositoryItemComboBox()
        RemoveHandler riEvalPeriodInterval.SelectedIndexChanged, AddressOf riEvalPeriodInterval_SelectedIndexChanged
        vGridControl.RepositoryItems.Add(riEvalPeriodInterval)
        riEvalPeriodInterval.AutoHeight = False
        Dim items As String() = {"DAY", "HOUR", "BUSY HOUR"}
        riEvalPeriodInterval.Items.AddRange(items)
        riEvalPeriodInterval.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        AddHandler riEvalPeriodInterval.SelectedIndexChanged, AddressOf riEvalPeriodInterval_SelectedIndexChanged
    End Sub

    Private Sub riEvalPeriodInterval_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim riEVICombo As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
        Dim hh As Rows.EditorRow = propGridCongRule.GetRowByFieldName("HourlyMinOcc")
        Dim hh2 As Rows.EditorRow = propGridCongRule.GetRowByFieldName("HourlyOccConsecutive")

        If riEVICombo.SelectedItem.ToString.ToUpper = "DAY" Or riEVICombo.SelectedItem.ToString.ToUpper = "BUSY HOUR" Then
            hh.Properties.Value = 1
            hh2.Properties.Value = False
            hh.Enabled = False
            hh2.Enabled = False
        ElseIf riEVICombo.SelectedItem.ToString.ToUpper = "HOUR" Then
            hh.Enabled = True
            hh2.Enabled = True
        End If

    End Sub

    Private Sub CreateEvalWinDays(ByRef vGridControl As VGridControl, ByVal name As String, ByVal maxValue As Integer, ByVal minValue As Integer)
        riseEvalWinDays = New RepositoryItemSpinEdit()
        vGridControl.RepositoryItems.Add(riseEvalWinDays)
        riseEvalWinDays.AutoHeight = False
        riseEvalWinDays.IsFloatValue = False
        riseEvalWinDays.Mask.EditMask = "N00"
        riseEvalWinDays.MaxValue = maxValue
        riseEvalWinDays.MinValue = minValue
        riseEvalWinDays.Name = name
        riseEvalWinDays.Appearance.Options.UseTextOptions = True
        riseEvalWinDays.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        AddHandler riseEvalWinDays.EditValueChanged, AddressOf riseEvalWinDays_EditValueChanged
    End Sub

    Private Sub CreateHourlyMinOcc(ByRef vGridControl As VGridControl, ByVal name As String, ByVal maxValue As Integer, ByVal minValue As Integer)
        riseHrlyMinOcc = New RepositoryItemSpinEdit()
        vGridControl.RepositoryItems.Add(riseHrlyMinOcc)
        riseHrlyMinOcc.AutoHeight = False
        riseHrlyMinOcc.IsFloatValue = False
        riseHrlyMinOcc.Mask.EditMask = "N00"
        riseHrlyMinOcc.MaxValue = maxValue
        riseHrlyMinOcc.MinValue = minValue
        riseHrlyMinOcc.Name = name
        riseHrlyMinOcc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        AddHandler riseHrlyMinOcc.EditValueChanged, AddressOf riseHrlyMinOcc_EditValueChanged
    End Sub

    Private Sub CreateOccurences(ByRef vGridControl As VGridControl, ByVal name As String, ByVal maxValue As Integer, ByVal minValue As Integer)
        riseOcc = New RepositoryItemSpinEdit()
        vGridControl.RepositoryItems.Add(riseOcc)
        riseOcc.AutoHeight = False
        riseOcc.IsFloatValue = False
        riseOcc.Mask.EditMask = "N00"
        riseOcc.MaxValue = maxValue
        riseOcc.MinValue = minValue
        riseOcc.Name = name
        riseOcc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        AddHandler riseOcc.EditValueChanged, AddressOf riseOcc_EditValueChanged
    End Sub

    Private Sub RenameCongestionJobName(newJobName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", CInt(selectedJobID)},
            New String() {"@capJobName", Chr(39) & newJobName & Chr(39)}
        }
        strConnection = GetSQL(3030, parray)(0)
        sqlParam = GetSQL(3030, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub RenameCongestionRuleName(newCongRuleName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capCongRuleID", CInt(selectedCongRuleID)},
            New String() {"@capCongRuleName", Chr(39) & newCongRuleName & Chr(39)}
        }
        strConnection = GetSQL(3031, parray)(0)
        sqlParam = GetSQL(3031, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadCongRuleProperties(ByRef dr As DataRow)
        If dr IsNot Nothing Then
            grpCtrlCongProperties.Enabled = True

            Dim dt As DataTable = dtCongRule.Select("CapCongestionRuleID=" & dr("CapCongestionRuleID")).CopyToDataTable

            iosTech = dt.Rows(0)("IOS_TECH").ToString
            counterType = dt.Rows(0)("CounterType").ToString

            Dim dtProps As DataTable = dt.DefaultView.ToTable(True, {"IsEnabled", "EvalPeriodInterval", "HourlyMinOcc", "HourlyOccConsecutive", "Occurences", "EvalWindowDays", "StartDate", "EndDate", "ExcludeDays", "ExcludedHours", "ScoreEnabled", "Score", "EmailEnabled", "EmailAddresses", "RootCauseSelection", "ServiceCategory"})

            RemoveHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            SetComboBox(cmbCategory, ComboSelectBased.ValueBased, CInt(dt.Rows(0)("CapJobCategoryID")))
            AddHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged

            RemoveHandler propGridCongRule.CellValueChanged, AddressOf propGridCongRule_CellValueChanged
            RemoveHandler propGridCongRule.CustomRecordCellEdit, AddressOf propGridCongRule_CustomRecordCellEdit

            propGridCongRule.Rows.Clear()
            propGridCongRule.DataSource = dtProps
            propGridCongRule.LayoutStyle = LayoutViewStyle.SingleRecordView

            propGridCongRule.Rows.RemoveAt(2)
            propGridCongRule.Rows.RemoveAt(2)

            CreateCombo(propGridCongRule, "EvalPeriodInterval")

            If dtProps.Rows(0)("EvalPeriodInterval").ToString.ToUpper = "HOUR" Then
                Dim hh As New DevExpress.XtraVerticalGrid.Rows.EditorRow("HourlyMinOcc")
                hh.Properties.Value = IIf(IsDBNull(dtProps.Rows(0)("HourlyMinOcc")), 0, dtProps.Rows(0)("HourlyMinOcc"))
                hh.Properties.Caption = "Minimum # of congested hours in one day"
                hh.Enabled = True
                propGridCongRule.Rows.Insert(hh, 2)

                Dim hh2 As New DevExpress.XtraVerticalGrid.Rows.EditorRow("HourlyOccConsecutive")
                hh2.Properties.Value = IIf(IsDBNull(dtProps.Rows(0)("HourlyOccConsecutive")), False, dtProps.Rows(0)("HourlyOccConsecutive"))
                hh2.Properties.Caption = "Congested hours must be consecutive"
                hh2.Enabled = True
                propGridCongRule.Rows.Insert(hh2, 3)

            ElseIf dtProps.Rows(0)("EvalPeriodInterval").ToString.ToUpper = "DAY" Or dtProps.Rows(0)("EvalPeriodInterval").ToString.ToUpper = "BUSY HOUR" Or dtProps.Rows(0)("EvalPeriodInterval").ToString.ToUpper = "" Then
                Dim hh As New DevExpress.XtraVerticalGrid.Rows.EditorRow("HourlyMinOcc")
                hh.Properties.Value = IIf(IsDBNull(dtProps.Rows(0)("HourlyMinOcc")), 0, dtProps.Rows(0)("HourlyMinOcc"))
                hh.Properties.Caption = "Minimum # of congested hours in one day"
                hh.Enabled = False
                propGridCongRule.Rows.Insert(hh, 2)

                Dim hh2 As New DevExpress.XtraVerticalGrid.Rows.EditorRow("HourlyOccConsecutive")
                hh2.Properties.Value = IIf(IsDBNull(dtProps.Rows(0)("HourlyOccConsecutive")), False, dtProps.Rows(0)("HourlyOccConsecutive"))
                hh2.Properties.Caption = "Congested hours must be consecutive"
                hh2.Enabled = False
                propGridCongRule.Rows.Insert(hh2, 3)
            End If

            CreateOccurences(propGridCongRule, "Occurences", 10, 1)
            propGridCongRule.Rows.ElementAt(4).Properties.Caption = "Minimum Days with Congestion during sliding window"

            CreateEvalWinDays(propGridCongRule, "EvalWindowDays", 10, 1)
            propGridCongRule.Rows.ElementAt(5).Properties.Caption = "Evaluation sliding window - Days back from now"

            CreateHourlyMinOcc(propGridCongRule, "HourlyMinOcc", 24, 1)

            CreateExcludeDaysCombo(propGridCongRule, "ExcludeDays", IIf(IsDBNull(dtProps.Rows(0)("ExcludeDays")), "", dtProps.Rows(0)("ExcludeDays")))
            CreateExcludeHoursCombo(propGridCongRule, "ExcludedHours", IIf(IsDBNull(dtProps.Rows(0)("ExcludedHours")), "", dtProps.Rows(0)("ExcludedHours")))

            AddHandler propGridCongRule.CustomRecordCellEdit, AddressOf propGridCongRule_CustomRecordCellEdit
            AddHandler propGridCongRule.CellValueChanged, AddressOf propGridCongRule_CellValueChanged
        End If
    End Sub

    Private Sub ShowHideChartSeries(ByRef chart As Chart, ByVal SeriesName As String)
        Try
            Dim DefaultSeries As Dictionary(Of String, SeriesCollection) = Nothing
            Dim ExtraSeries As Dictionary(Of String, LegendEntryCollection) = Nothing

            Dim chartObject As String = Nothing
            Dim chartNameKey As String = Nothing

            If chart.Name.Contains("2") Then
                DefaultSeries = DefaultSeriesCollectionC2
                ExtraSeries = ExtraLegendEntryCollectionC2
            ElseIf chart.Name.Contains("3") Then
                DefaultSeries = DefaultSeriesCollectionC3
                ExtraSeries = ExtraLegendEntryCollectionC3
            ElseIf chart.Name.Contains("4") Then
                DefaultSeries = DefaultSeriesCollectionC4
                ExtraSeries = ExtraLegendEntryCollectionC4
            End If

            chartNameKey = chart.Name

            If chart.SeriesCollection.GetSeries(SeriesName) IsNot Nothing Then
                If chart.SeriesCollection.Count > 1 Then

                    If ExtraSeries.ContainsKey(chartNameKey) = False Then
                        If DefaultSeries.ContainsKey(chartNameKey) Then
                            DefaultSeries.Remove(chartNameKey)
                        End If
                        Dim sc As New SeriesCollection()
                        For Each ser As Series In chart.SeriesCollection
                            sc.Add(ser)
                        Next
                        DefaultSeries.Add(chartNameKey, sc)
                    End If

                    chart.SeriesCollection.Remove(chart.SeriesCollection.GetSeries(SeriesName))

                    Dim entry As New LegendEntry()
                    entry.Name = SeriesName
                    entry.Hotspot.ToolTip = SeriesName
                    entry.LabelStyle.Color = Color.Gray
                    chart.LegendBox.ExtraEntries.Add(entry)

                    If ExtraSeries.ContainsKey(chartNameKey) Then
                        ExtraSeries.Remove(chartNameKey)
                    End If
                    Dim tempLegendCol As New LegendEntryCollection
                    For Each kvp As LegendEntry In chart.LegendBox.ExtraEntries
                        tempLegendCol.Add(kvp)
                    Next
                    ExtraSeries.Add(chartNameKey, tempLegendCol)
                End If
            Else
                If ExtraSeries.ContainsKey(chartNameKey) Then

                    Dim LegendColl As LegendEntryCollection = Nothing
                    LegendColl = ExtraSeries.Item(chartNameKey)

                    For Each kvp As LegendEntry In LegendColl
                        If kvp.Name = SeriesName Then
                            chart.LegendBox.ExtraEntries.Remove(kvp)
                            Exit For
                        End If
                    Next

                    ExtraSeries.Remove(chartNameKey)

                    Dim tempLegendCol As New LegendEntryCollection
                    For Each kvp As LegendEntry In chart.LegendBox.ExtraEntries
                        tempLegendCol.Add(kvp)
                    Next
                    If tempLegendCol.Count > 0 Then
                        ExtraSeries.Add(chartNameKey, tempLegendCol)
                    End If

                    chart.SeriesCollection.Clear()
                    Dim sc As SeriesCollection = Nothing
                    If DefaultSeries.ContainsKey(chartNameKey) Then
                        sc = DefaultSeries.Item(chartNameKey)
                        If ExtraSeries.ContainsKey(chartNameKey) = False Then
                            DefaultSeries.Remove(chartNameKey)
                        End If
                    End If

                    Dim flag As Boolean = False
                    LegendColl.Clear()
                    If ExtraSeries.ContainsKey(chartNameKey) Then
                        LegendColl = ExtraSeries.Item(chartNameKey)
                    End If
                    For Each objSeries As Series In sc
                        flag = False
                        For Each kvp As LegendEntry In LegendColl
                            If kvp.Name = objSeries.Name Then
                                flag = True
                                Exit For
                            End If
                        Next

                        If flag = False Then
                            chart.SeriesCollection.Add(objSeries)
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
        Finally
            chart.Refresh()
        End Try
    End Sub

    Private Function GetFilterQueryFromObjectTree() As String
        Dim filter As String = Nothing
        Dim objList As New List(Of KeyValuePair(Of String, List(Of String)))

        GetSelectedNodes(tlObjectTree.Nodes, objList)

        For Each itm As KeyValuePair(Of String, List(Of String)) In objList
            filter = filter & itm.Key & " IN ("
            For Each itm1 As String In itm.Value
                filter = filter & "''" & itm1 & "'',"
            Next
            filter = filter.TrimEnd(",") & ")"
        Next
        Return filter
    End Function

    Private Sub GetSelectedNodes(ByVal nodes As TreeListNodes, ByRef list As List(Of KeyValuePair(Of String, List(Of String))))
        Dim obj As KeyValuePair(Of String, List(Of String))
        For Each node As TreeListNode In nodes
            If node.Checked Then
                Dim ndText As String = node.GetDisplayText("ObjectName")
                If ndText.ToUpper <> "PLMN" Then
                    obj = Nothing
                    If list.Exists(Function(x) x.Key = node.Tag) Then
                        obj = list.FirstOrDefault(Function(x) x.Key = node.Tag)
                        obj.Value.Add(node.GetDisplayText("ObjectName"))
                    Else
                        Dim value As New List(Of String)
                        value.Add(node.GetDisplayText("ObjectName"))
                        obj = New KeyValuePair(Of String, List(Of String))(node.Tag, value)
                        list.Add(obj)
                    End If
                End If
            End If
            GetSelectedNodes(node.Nodes, list)
        Next
    End Sub

    Private Function GetDataToExportResults() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capRunJobID", Chr(39) + CInt(selectedJobID).ToString + "_" + clickedCongDate_Chart1.ToString("yyyyMMdd") + Chr(39)}
        }
        strConnection = GetSQL(3038, parray)(0)
        sqlParam = GetSQL(3038, parray)(1)
        Return IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Function GetDataToExportConfig() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", Chr(39) & CInt(selectedJobID) & Chr(39)}
        }
        strConnection = GetSQL(3039, parray)(0)
        sqlParam = GetSQL(3039, parray)(1)
        Return IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub LoadCapJobStatus()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", Chr(39) & CInt(selectedJobID) & Chr(39)}
        }
        strConnection = GetSQL(3040, parray)(0)
        sqlParam = GetSQL(3040, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        For Each dr As DataRow In dt.Rows
            dr("CongestionRuleSettings") = dr("CongestionRuleSettings").ToString.Replace("KPI Settings:", vbCrLf & "KPI Settings:").Replace("Filter Settings:", vbCrLf & "Filter Settings:")
        Next

        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcStatus, gvStatus, dt, "ALL", Nothing, "CongestionRuleSettings")

        rimeChangesSettings = New RepositoryItemMemoEdit()
        rimeChangesSettings.ReadOnly = True
        rimeChangesSettings.Appearance.Options.UseTextOptions = True
        rimeChangesSettings.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        gcStatus.RepositoryItems.Add(rimeChangesSettings)
        gvStatus.Columns("CongestionRuleSettings").ColumnEdit = rimeChangesSettings
        gvStatus.OptionsView.RowAutoHeight = True
    End Sub

    Private Sub LoadCapJobChanges(ByVal gridOnly As Boolean)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@capJobID", Chr(39) & CInt(selectedJobID) & Chr(39)}
        }
        strConnection = GetSQL(3041, parray)(0)
        sqlParam = GetSQL(3041, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        For Each dr As DataRow In dt.Rows
            dr("CongestionRuleSettings") = dr("CongestionRuleSettings").ToString.Replace("KPI Settings:", vbCrLf & "KPI Settings:").Replace("Filter Settings:", vbCrLf & "Filter Settings:")
        Next

        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(grdChanges, gvChanges, dt, "ALL", Nothing, "CongestionRuleSettings")

        rimeChangesSettings = New RepositoryItemMemoEdit()
        rimeChangesSettings.ReadOnly = True
        rimeChangesSettings.Appearance.Options.UseTextOptions = True
        rimeChangesSettings.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        grdChanges.RepositoryItems.Add(rimeChangesSettings)
        gvChanges.Columns("CongestionRuleSettings").ColumnEdit = rimeChangesSettings
        gvChanges.OptionsView.RowAutoHeight = True

        'Add black axis marker as config settings on chart1
        If dt IsNot Nothing Then
            Dim i As Integer = 0
            For Each dr As DataRow In dt.Rows
                ReDim Preserve jobConfigChangeDate(i)
                Dim cl As Color = Color.Black
                Dim axisMarkerObj As AxisMarker

                jobConfigChangeDate(i) = Convert.ToDateTime(dr("ChangeDate"))
                axisMarkerObj = New AxisMarker("", New Line(cl, 4), jobConfigChangeDate(i))    'dr("CapCongestionRuleName")

                axisMarkerObj.LegendEntry.Visible = False
                axisMarkerObj.Label.Alignment = StringAlignment.Near
                axisMarkerObj.Label.LineAlignment = StringAlignment.Far
                axisMarkerObj.BringToFront = True
                chartCapacity1.XAxis.Markers.Add(axisMarkerObj)
                i = i + 1
            Next

        End If
    End Sub

    Private Sub ConfigureCapacityForm(frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)
            Dim modelControl As IOS.Configuration.EntityModel.Control = Nothing

            Dim formControls As List(Of Object) = New List(Of Object) From {
                tlpConfig, btnRun, btnAddJob, btnDeleteJob, btnRefresh
            }

            For Each frmControl As Object In formControls
                modelControl = form.FindControlByName(frmControl.Name)
                If Not modelControl Is Nothing Then
                    frmControl.Enabled = modelControl.DefaultEnable
                    frmControl.Visible = modelControl.DefaultVisible
                End If
            Next
        End If
    End Sub

#End Region

#Region "All Events"

    Private Sub gvKPI_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs)
        If e.Value.ToString.ToLower = "select operator" Then
            e.Valid = False
        Else
            e.Valid = True
        End If
    End Sub

    Private Sub gvKPI_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        Try
            If e.Column.FieldName = "Operator" Then
                e.RepositoryItem = riCmb
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvKPI_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvKPI.ShowingEditor
        Try
            If gvKPI.FocusedColumn().FieldName = "Operator" Or gvKPI.FocusedColumn().FieldName = "TresholdValue" Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvKPI_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvKPI.CellValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim templateMOParamConfigID As Integer = 0
            Dim rIndex() As Integer = gvKPI.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvKPI.GetRow(rIndex(0)).Row
                templateMOParamConfigID = drow.Item(0)
            End If

            Dim data As DataRow = gvKPI.GetFocusedDataRow()
            If data IsNot Nothing Then

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@capKPIRuleID", CInt(data.Item("CapKPIRuleID"))},
                    New String() {"@operator", Chr(39) & data.Item("Operator") & Chr(39)},
                    New String() {"@tresholdValue", data.Item("TresholdValue")}
                }
                strConnection = GetSQL(3028, parray)(0)
                sqlParam = GetSQL(3028, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                grdKPI.Refresh()
                gvKPI.SelectRow(rIndex(0))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tlObjectTree_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        RemoveHandler tlObjectTree.NodeChanged, AddressOf tlObjectTree_NodeChanged
        If e.ChangeType = DevExpress.XtraTreeList.NodeChangeTypeEnum.CheckedState Then
            If e.Node.CheckState = CheckState.Checked Then
                e.Node.CheckAll()
            Else
                e.Node.UncheckAll()
            End If
            tlObjectTree.CheckParentNode(e.Node)
        End If
        AddHandler tlObjectTree.NodeChanged, AddressOf tlObjectTree_NodeChanged

        ClearChart3AndChart4()
        LoadReportingChart1()
        LoadReportingChart2()

    End Sub

    Private Sub tlCongestionJobs_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        RemoveHandler tlCongestionJobs.NodeChanged, AddressOf tlCongestionJobs_NodeChanged
        If e.ChangeType = DevExpress.XtraTreeList.NodeChangeTypeEnum.CheckedState Then
            If e.Node.CheckState = CheckState.Checked Then
                e.Node.CheckAll()
            Else
                e.Node.UncheckAll()
            End If
            tlCongestionJobs.CheckParentNode(e.Node)
        End If
        AddHandler tlCongestionJobs.NodeChanged, AddressOf tlCongestionJobs_NodeChanged
    End Sub

    Private Sub tlCongestionJobs_DoubleClick(sender As Object, e As EventArgs)
        If tlCongestionJobs.FocusedNode.Level = 0 Or tlCongestionJobs.FocusedNode.Level = 2 Then
            tlCongestionJobs.OptionsBehavior.Editable = True
            tlCongestionJobs.OptionsBehavior.ReadOnly = False

            If tlCongestionJobs.FocusedNode.Level = 0 Then
                selectedJobID = CInt(tlCongestionJobs.FocusedNode.Tag)
            ElseIf tlCongestionJobs.FocusedNode.Level = 2 Then
                selectedCongRuleID = CInt(tlCongestionJobs.FocusedNode.Tag)
            End If
        Else
            tlCongestionJobs.OptionsBehavior.Editable = False
            tlCongestionJobs.OptionsBehavior.ReadOnly = True
        End If
    End Sub

    Private Sub tlCongestionJobs_CellValueChanged(sender As Object, e As DevExpress.XtraTreeList.CellValueChangedEventArgs)
        Dim jobNode As TreeListNode = e.Node
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (e.Value IsNot Nothing) Then
                If (jobNode.Level = 0) Then
                    RenameCongestionJobName(e.Value)
                    jobNode.Item("ObjectName") = e.Value.ToString
                ElseIf (jobNode.Level = 2) Then
                    RenameCongestionRuleName(e.Value)
                    jobNode.Item("ObjectName") = e.Value.ToString
                End If
            End If

            tlCongestionJobs.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            tlCongestionJobs.OptionsBehavior.Editable = False
            tlCongestionJobs.OptionsBehavior.ReadOnly = True

            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tlCongestionJobs_CustomRowFilter(sender As Object, e As FilterNodeEventArgs) Handles tlCongestionJobs.CustomRowFilter, tlObjectTree.CustomRowFilter
        Try
            Dim parentNode As TreeListNode = e.Node.ParentNode
            If parentNode IsNot Nothing Then
                If e.Node.ParentNode.Visible = True And (e.Node.Item("ObjectName").ToString().ToUpper().Contains(e.Node.TreeList.FindFilterText.ToUpper()) Or e.Node.ParentNode.Item("ObjectName").ToString().ToUpper().Contains(e.Node.TreeList.FindFilterText.ToUpper())) Then
                    e.Node.Visible = e.Node.Visible OrElse e.Node.ParentNode.Visible
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtb_ChartHeight_EditValueChanged(sender As Object, e As EventArgs)
        ResizeChartHeights(tbChartHeightStats.EditValue)
    End Sub

    Private Sub frmCapacity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.GetTreeImages(imgListJob)
            tlCongestionJobs.SelectImageList = imgListJob
            dtCongestionJobsList = New DataTable
            dtCongestionJobsList.Columns.Add("CapJobName", GetType(String))
            LoadCategories()
            LoadObjectTree()
            LoadCongestionJobs()
            AddHandler tbChartHeightStats.EditValueChanged, AddressOf dtb_ChartHeight_EditValueChanged
            tlCongestionJobs_FocusedNodeChanged(Nothing, Nothing)
            ResizeChartHeights(tbChartHeightStats.EditValue)
            ConfigureCapacityForm("frmCapacity")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Update Congestion Rule Category
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@capCongestionRuleID", CInt(gvCongRules.GetRowCellValue(gvCongRules.FocusedRowHandle, "CapCongestionRuleID"))},
                New String() {"@capJobCategoryID", CInt(TryCast(cmbCategory.SelectedItem, IOS.Library.clsComboBoxItem).Value)}
            }
            strConnection = GetSQL(3027, parray)(0)
            sqlParam = GetSQL(3027, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            GetCongestionRules()
            LoadCongestionJobs()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If btnRun.Text.ToUpper = "RUN JOB" Then

                If (ceIsEnabled.Checked = False) Then
                    SetMessage("Please Note: Congestion job is not enabled")
                    Exit Sub
                End If

                congestionJobRunDate = Now.AddDays(-1)

                WaitScreen.ShowWaitScreen("Running Job for date:" & congestionJobRunDate.ToString("yyyy-MM-dd") & vbCrLf & "  This can take a few minutes...")
                RunCongestionJob()
                WaitScreen.CloseWaitScreen()

            ElseIf btnRun.Text.ToUpper = "RUN RULE" Then

                congestionRuleRunDate = Now.AddDays(-1)

                WaitScreen.ShowWaitScreen("Running Rule for date:" & congestionRuleRunDate.ToString("yyyy-MM-dd") & vbCrLf & "  This can take a few minutes...")
                RunCongestionRule()
                WaitScreen.CloseWaitScreen()

            End If

            ClearChart3AndChart4()
            LoadReportingChart1()
            LoadReportingChart2()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadCongestionJobs()
            tlCongestionJobs_FocusedNodeChanged(Nothing, Nothing)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAddJob_Click(sender As Object, e As EventArgs) Handles btnAddJob.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objdlgCongJob As New dlgCongestionJob()
            objdlgCongJob.addNewJob = True
            objdlgCongJob.ShowDialog()

            If (newCongestionJob IsNot Nothing) Then
                'RemoveHandler tlCongestionJobs.FocusedNodeChanged, AddressOf tlCongestionJobs_FocusedNodeChanged
                LoadCongestionJobs()
                'AddHandler tlCongestionJobs.FocusedNodeChanged, AddressOf tlCongestionJobs_FocusedNodeChanged
                tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNodeByFieldValue("ObjectName", newCongestionJob))
                tlCongestionJobs_FocusedNodeChanged(Nothing, Nothing)
                xtcRight.SelectedTabPageIndex = 1
            Else
                xtcRight.SelectedTabPageIndex = 0
            End If
            'tlCongestionJobs.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteJob_Click(sender As Object, e As EventArgs) Handles btnDeleteJob.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If tlCongestionJobs.FocusedNode.Level = 0 Then
                If XtraMessageBox.Show("Are you sure to delete job: " & tlCongestionJobs.FocusedNode.GetDisplayText("ObjectName").ToString & " and all it's history?", "Delete Congestion Job & History", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim jobID As Integer = CInt(tlCongestionJobs.FocusedNode.Tag)
                    DeleteCongestionJob(jobID)
                End If
                LoadCongestionJobs()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tlCongestionJobs_FocusedNodeChanged(sender As Object, e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            selectedRuleID = Nothing
            congRuleIdsInCategory = Nothing

            Dim node As TreeListNode = tlCongestionJobs.FocusedNode
            If node.Level = 0 Then
                'job node
                selectedJobID = CInt(node.Tag)
                selectedJobName = tlCongestionJobs.FocusedNode.GetDisplayText("ObjectName")
                btnRun.Text = "Run Job"
                'clear chart3 and chart4 on rule selection change
                ClearChart3AndChart4()
            Else
                If node.Level = 1 Then
                    'category node
                    selectedJobID = CInt(node.ParentNode.Tag)
                    selectedJobName = node.ParentNode.GetDisplayText("ObjectName")

                    'getting all the cong rule ids under the selected category
                    If node.HasChildren = True Then
                        congRuleIdsInCategory = "IN ("
                        For Each ruleNode As TreeListNode In node.Nodes
                            congRuleIdsInCategory = congRuleIdsInCategory + ruleNode.Tag & ","
                        Next
                        congRuleIdsInCategory = congRuleIdsInCategory.TrimEnd(",")
                        congRuleIdsInCategory = congRuleIdsInCategory & ")"
                    End If

                    btnRun.Text = "Run"
                ElseIf node.Level = 2 Then
                    'rule node
                    selectedJobID = CInt(node.ParentNode.ParentNode.Tag)
                    selectedJobName = node.ParentNode.ParentNode.GetDisplayText("ObjectName")
                    selectedRuleID = CInt(node.Tag)
                    btnRun.Text = "Run Rule"
                    'clear chart3 and chart4 on rule selection change
                    ClearChart3AndChart4()
                ElseIf node.Level = 3 Then
                    'kpi node
                    selectedJobID = CInt(node.ParentNode.ParentNode.ParentNode.Tag)
                    selectedRuleID = CInt(node.ParentNode.Tag)
                    selectedJobName = node.ParentNode.ParentNode.ParentNode.GetDisplayText("ObjectName")
                    btnRun.Text = "Run"
                End If
            End If

            If tlCongestionJobs.FocusedNode IsNot Nothing Then
                LoadCongestionJobDetails()
            End If

            If xtcRight.SelectedTabPageIndex = 0 Then
                LoadReportingChart1()
                LoadReportingChart2()

            ElseIf xtcRight.SelectedTabPageIndex = 1 Then
                jobNodeChanged = True
                LoadCongestionRuleGrid()

            ElseIf xtcRight.SelectedTabPageIndex = 2 Then
                LoadCapJobStatus()

            ElseIf xtcRight.SelectedTabPageIndex = 3 Then
                LoadCapJobChanges(True)

            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsScheduled_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@capJobID", CInt(selectedJobID)},
                New String() {"@isScheduled", IIf(ceIsScheduled.Checked, 1, 0)}
            }
            strConnection = GetSQL(3035, parray)(0)
            sqlParam = GetSQL(3035, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsEnabled_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@capJobID", CInt(selectedJobID)},
                New String() {"@isEnabled", IIf(ceIsEnabled.Checked, 1, 0)}
            }
            strConnection = GetSQL(3036, parray)(0)
            sqlParam = GetSQL(3036, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsLocked_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@capJobID", CInt(selectedJobID)},
                New String() {"@isLocked", IIf(ceIsLocked.Checked, 1, 0)}
            }
            strConnection = GetSQL(3037, parray)(0)
            sqlParam = GetSQL(3037, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAddCongRule_Click(sender As Object, e As EventArgs) Handles btnAddCongRule.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbCategory.SelectedIndex = 0 Then
                SetMessage("Please select category")
                Exit Sub
            End If

            Dim objCongestionRule As New dlgCongestionRule()
            objCongestionRule.capJobID = CInt(dtCongJobDetails.Rows(0)("CapJobID"))
            objCongestionRule.categoryID = CInt(TryCast(cmbCategory.SelectedItem, IOS.Library.clsComboBoxItem).Value)
            objCongestionRule.ShowDialog()

            LoadCongestionJobs()
            tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNodeByFieldValue("ObjectName", newCapCongRuleName))

            LoadCongestionRuleGrid()
            If newCapCongRuleName IsNot Nothing Then
                Dim dr() As DataRow = dtCongRule.Select("CapCongestionRuleName='" & newCapCongRuleName & "'", "CapCongestionRuleID DESC")
                If dr.Length > 1 Then
                    gvCongRules.FocusedRowHandle = gvCongRules.LocateByValue("CapCongestionRuleID", dr(0)("CapCongestionRuleID"))
                Else
                    gvCongRules.FocusedRowHandle = gvCongRules.LocateByValue("CapCongestionRuleName", newCapCongRuleName)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAddKPI_Click(sender As Object, e As EventArgs) Handles btnAddKPI.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim capCongRuleID As Integer = gvCongRules.GetRowCellValue(gvCongRules.FocusedRowHandle, "CapCongestionRuleID")

            Dim objDlgAddKPIRule As New dlgAddKPIRule()
            objDlgAddKPIRule.capCongestionRuleID = capCongRuleID
            objDlgAddKPIRule.iosTech = Me.iosTech
            objDlgAddKPIRule.counterType = Me.counterType
            objDlgAddKPIRule.ShowDialog()

            'reload KPI grid to show newly added kpi
            LoadKPIGrid(capCongRuleID)

            'reload congestion job tree and set focus the cong rule
            LoadCongestionJobs()
            tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNode(Function(x) x.Tag = capCongRuleID))

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteKPI_Click(sender As Object, e As EventArgs) Handles btnDeleteKPI.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim capCongRuleID As Integer = gvCongRules.GetRowCellValue(gvCongRules.FocusedRowHandle, "CapCongestionRuleID")
            If (gvKPI.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvKPI.FocusedRowHandle
                Dim kpiName As String = gvKPI.GetRowCellValue(selectedRowHandle, "KPI_Name")
                Dim kpiID As Integer = gvKPI.GetRowCellValue(selectedRowHandle, "CapKPIRuleID")
                If XtraMessageBox.Show("Are you sure to delete kpi: " & kpiName & "?", "Delete KPI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteKPI(kpiID)
                    LoadKPIGrid(capCongRuleID)
                End If
                'reload congestion job tree and set focus the cong rule
                LoadCongestionJobs()
                tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNode(Function(x) x.Tag = capCongRuleID))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAddObjFilter_Click(sender As Object, e As EventArgs) Handles btnAddObjFilter.Click
        Try
            Dim capCongRuleID As Integer = gvCongRules.GetRowCellValue(gvCongRules.FocusedRowHandle, "CapCongestionRuleID")
            Dim objFilter As New dlgObjFilter("CongestionRule", capCongRuleID)
            objFilter.ShowDialog()

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadObjectFilterGrid(capCongRuleID)
            grdObjFilter.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteObjFilter_Click(sender As Object, e As EventArgs) Handles btnDeleteObjFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim capCongRuleID As Integer = gvCongRules.GetRowCellValue(gvCongRules.FocusedRowHandle, "CapCongestionRuleID")
            If (gvObjFilter.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvObjFilter.FocusedRowHandle
                Dim filterSting As String = gvObjFilter.GetRowCellValue(selectedRowHandle, "FilterString")
                Dim filterID As Integer = gvObjFilter.GetRowCellValue(selectedRowHandle, "CapCongestionRuleFilterID")
                If XtraMessageBox.Show("Are you sure to delete filter: " & filterSting & "?", "Delete Filter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteObjFilter(filterID)
                    LoadObjectFilterGrid(capCongRuleID)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAddNewCategory_Click(sender As Object, e As EventArgs) Handles btnAddNewCategory.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim selectedCategoryIndex As Integer = cmbCategory.SelectedIndex
            Dim objAddCategory As New dlgAddCategory()
            objAddCategory.ShowDialog()

            'RemoveHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            LoadCategories()
            If newCapCategory IsNot Nothing Then
                cmbCategory.SelectedItem = newCapCategory
            Else
                cmbCategory.SelectedIndex = selectedCategoryIndex
            End If
            'AddHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            LoadCongestionJobs()
            tlCongestionJobs.SelectNode(tlCongestionJobs.FindNode(Function(x) x.Tag = selectedJobID))
            tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNode(Function(x) x.Tag = selectedJobID))
            tlCongestionJobs.ExpandAll()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteCategory_Click(sender As Object, e As EventArgs) Handles btnDeleteCategory.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'RemoveHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            If cmbCategory.SelectedItem.ToString.ToUpper = "NO_CATEGORY" Then
                SetMessage("NO_CATEGORY cannot be deleted")
                Exit Sub
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@capJobCategoryID", CInt(TryCast(cmbCategory.SelectedItem, IOS.Library.clsComboBoxItem).Value)}
            }

            strConnection = GetSQL(3029, parray)(0)
            sqlParam = GetSQL(3029, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            LoadCategories()
            SetComboBox(cmbCategory, ComboSelectBased.TextBased, "NO_CATEGORY")
            'AddHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            LoadCongestionJobs()
            tlCongestionJobs.SelectNode(tlCongestionJobs.FindNode(Function(x) x.Tag = selectedJobID))
            tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNode(Function(x) x.Tag = selectedJobID))
            tlCongestionJobs.ExpandAll()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub xtcRight_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcRight.SelectedPageChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If xtcRight.SelectedTabPageIndex = 0 Then
                'Reporting tab page is focused
                If jobNodeChanged = True Then
                    LoadReportingChart1()
                    LoadReportingChart2()
                End If
                jobNodeChanged = False
            ElseIf xtcRight.SelectedTabPageIndex = 1 Then
                'Config tab page is focused
                LoadCongestionJobDetails()
                LoadCongestionRuleGrid()
            ElseIf xtcRight.SelectedTabPageIndex = 2 Then
                LoadCapJobStatus()
            ElseIf xtcRight.SelectedTabPageIndex = 3 Then
                LoadCapJobChanges(True)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvCongRules_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim data As DataRow = gvCongRules.GetDataRow(gvCongRules.FocusedRowHandle)

            If data IsNot Nothing Then
                LoadCongRuleProperties(data)
                LoadKPIGrid(data("CapCongestionRuleID"))
                LoadObjectFilterGrid(data("CapCongestionRuleID"))
            Else
                propGridCongRule.Rows.Clear()
                propGridCongRule.DataSource = Nothing
                grpCtrlCongProperties.Enabled = False
                IOS.Library.IOSDevExpressGrid.ClearGrid(grdKPI)
                IOS.Library.IOSDevExpressGrid.ClearGrid(grdObjFilter)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riseOcc_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim se As SpinEdit = TryCast(sender, SpinEdit)
            If se.Properties.Name = "Occurences" Then
                UpdateCongRuleProperties("Occurences", se.EditValue)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riseHrlyMinOcc_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim se As SpinEdit = TryCast(sender, SpinEdit)
            If se.Properties.Name = "HourlyMinOcc" Then
                UpdateCongRuleProperties("HourlyMinOcc", se.EditValue)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riseEvalWinDays_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim se As SpinEdit = TryCast(sender, SpinEdit)
            If se.Properties.Name = "EvalWindowDays" Then
                UpdateCongRuleProperties("EvalWindowDays", se.EditValue)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riExcDays_Closed(sender As Object, e As ClosedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim edit As CheckedComboBoxEdit = TryCast(sender, CheckedComboBoxEdit)
            If (e.CloseMode = PopupCloseMode.Normal) Then
                UpdateCongRuleProperties("ExcludeDays", edit.EditValue)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub riExcHours_Closed(sender As Object, e As ClosedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim edit As CheckedComboBoxEdit = TryCast(sender, CheckedComboBoxEdit)
            If (e.CloseMode = PopupCloseMode.Normal) Then
                UpdateCongRuleProperties("ExcludedHours", edit.EditValue)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub propGridCongRule_CustomRecordCellEdit(sender As Object, e As GetCustomRowCellEditEventArgs)
        Try
            If e.Row.Name.Contains("Occurences") Then
                e.RepositoryItem = riseOcc
                e.RepositoryItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ElseIf e.Row.Name.Contains("EvalWindowDays") Then
                e.RepositoryItem = riseEvalWinDays
                e.RepositoryItem.Appearance.Options.UseTextOptions = True
                e.RepositoryItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ElseIf e.Row.Name.Contains("HourlyMinOcc") Then
                e.RepositoryItem = riseHrlyMinOcc
                e.RepositoryItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ElseIf e.Row.Name.Contains("EvalPeriodInterval") Then
                e.RepositoryItem = riEvalPeriodInterval
                e.RepositoryItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ElseIf e.Row.Name.Contains("ExcludeDays") Then
                e.RepositoryItem = riExcDays
            ElseIf e.Row.Name.Contains("ExcludedHours") Then
                e.RepositoryItem = riExcHours
            ElseIf e.Row.Name.Contains("StartDate") Then
                rideStartDate = New RepositoryItemDateEdit()
                rideStartDate.EditMask = "yyyy/MM/dd"
                rideStartDate.UseMaskAsDisplayFormat = True
                e.RepositoryItem = rideStartDate
                e.RepositoryItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ElseIf e.Row.Name.Contains("EndDate") Then
                rideEndDate = New RepositoryItemDateEdit()
                rideEndDate.EditMask = "yyyy/MM/dd"
                rideEndDate.UseMaskAsDisplayFormat = True
                e.RepositoryItem = rideEndDate
                e.RepositoryItem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub propGridCongRule_CellValueChanged(sender As Object, e As DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs)
        Try
            propGridCongRule.SuspendLayout()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gridRow As Rows.BaseRow = e.Row
            If (Not gridRow Is Nothing) Then
                If gridRow.Properties.FieldName = "IsEnabled" Then
                    UpdateCongRuleProperties("IsEnabled", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "EvalPeriodInterval" Then
                    UpdateCongRuleProperties("EvalPeriodInterval", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "HourlyOccConsecutive" Then
                    UpdateCongRuleProperties("HourlyOccConsecutive", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "ScoreEnabled" Then
                    UpdateCongRuleProperties("ScoreEnabled", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "Score" Then
                    UpdateCongRuleProperties("Score", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "EmailEnabled" Then
                    UpdateCongRuleProperties("EmailEnabled", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "EmailAddresses" Then
                    UpdateCongRuleProperties("EmailAddresses", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "RootCauseSelection" Then
                    UpdateCongRuleProperties("RootCauseSelection", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "ServiceCategory" Then
                    UpdateCongRuleProperties("ServiceCategory", gridRow.Properties.Value)
                ElseIf gridRow.Properties.FieldName = "StartDate" Then
                    UpdateCongRuleProperties("StartDate", IIf(IsDBNull(gridRow.Properties.Value), "", gridRow.Properties.Value))
                ElseIf gridRow.Properties.FieldName = "EndDate" Then
                    UpdateCongRuleProperties("EndDate", IIf(IsDBNull(gridRow.Properties.Value), "", gridRow.Properties.Value))
                End If
            End If
            'Refresh changed values in <dtCongRule>
            GetCongestionRules()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            propGridCongRule.ResumeLayout()
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub btnDeleteCongRule_Click(sender As Object, e As EventArgs) Handles btnDeleteCongRule.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (gvCongRules.SelectedRowsCount > 0) Then

                Dim node As TreeListNode = tlCongestionJobs.FocusedNode
                If node.Level = 0 Then
                    selectedJobID = CInt(node.Tag)
                ElseIf node.Level = 1 Then
                    selectedJobID = CInt(node.ParentNode.Tag)
                ElseIf node.Level = 2 Then
                    selectedJobID = CInt(node.ParentNode.ParentNode.Tag)
                ElseIf node.Level = 3 Then
                    selectedJobID = CInt(node.ParentNode.ParentNode.ParentNode.Tag)
                End If

                Dim selectedRowHandle As Integer = gvCongRules.FocusedRowHandle
                Dim capCongRuleID As Integer = gvCongRules.GetRowCellValue(gvCongRules.FocusedRowHandle, "CapCongestionRuleID")
                Dim congRuleName As String = gvCongRules.GetRowCellValue(selectedRowHandle, "CapCongestionRuleName")
                If XtraMessageBox.Show("Are you sure to delete congestion rule: " & congRuleName & "?", "Delete Congestion Rule", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteCongestionRule(capCongRuleID)
                    LoadCongestionJobs()
                    tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNode(Function(x) x.Tag = selectedJobID))
                    LoadCongestionRuleGrid()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub chart_MouseDown(sender As System.Object, e As MouseEventArgs) Handles chartCapacity3.MouseDown
        Try
            If (e.Button = MouseButtons.Right) Then
                Dim hit As HitTestInfo = Nothing
                Dim myChart As dotnetCHARTING.WinForms.Chart = TryCast(sender, dotnetCHARTING.WinForms.Chart)
                'If (chart1 IsNot Nothing) Then
                Try
                    hit = myChart.HitTest()
                Catch ex As Exception
                End Try

                If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                    Dim el As Element = CType(hit.Object, Element)
                    Dim clickedItem As String = el.Name
                    objectNameChart3 = el.Name
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub chart_Click(sender As Object, e As MouseEventArgs) Handles chartCapacity1.Click, chartCapacity2.Click, chartCapacity3.Click, chartCapacity4.Click
        Try
            If e.Button = MouseButtons.Left Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim myChart As Chart = CType(sender, Chart)
                Dim chartName As String = myChart.Name
                Dim hit As HitTestInfo = myChart.HitTest(e.X, e.Y)

                If (myChart IsNot Nothing) Then
                    Try
                        hit = myChart.HitTest()
                    Catch ex As Exception
                    End Try

                    'chartCapacity1.XAxis.Markers.Clear()
                    chartCapacity2.XAxis.Markers.Clear()
                    chartCapacity3.XAxis.Markers.Clear()

                    If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                        Dim el As Element = CType(hit.Object, Element)
                        Dim clickedDate As DateTime = Nothing
                        Dim clickedObject As String = Nothing

                        If chartName = "chartCapacity1" Then
                            clickedDate = CType(el.XDateTime, DateTime)
                            clickedCongDate_Chart1 = clickedDate
                            clickedCongDate_Chart2 = Nothing
                            If chartName = "chartCapacity1" Then
                                AddAxisMarkerX(chartCapacity1, clickedDate)
                            ElseIf chartName = "chartCapacity2" Then
                                AddAxisMarkerX(chartCapacity2, clickedDate)
                            End If
                        Else
                            clickedObject = CType(el.Name, String)
                            AddAxisMarkerX(chartCapacity3, clickedObject)
                        End If

                        If chartName = "chartCapacity2" Then
                            clickedDate = CType(el.XDateTime, DateTime)
                            clickedCongDate_Chart2 = clickedDate
                            AddAxisMarkerX(chartCapacity2, clickedDate)
                        Else
                            clickedObject = CType(el.Name, String)
                        End If

                        'Add green sliding window marker on chart 2
                        If chartName = "chartCapacity1" Then
                            Dim sDate As Date = DateAdd(DateInterval.Day, -7, clickedDate)
                            Dim eDate As Date = clickedDate
                            AddSlidingWindowAxisMarker(sDate, eDate)
                        End If

                        'Draw Chart 3 for clicked congestion date on chart 1
                        If chartName = "chartCapacity1" Then
                            ClearChart3AndChart4()
                            'Remove chart 2
                            chartCapacity2.SeriesCollection.Clear()
                            chartCapacity2.RefreshChart()

                            LoadReportingChart2()
                            'Draw chart 3
                            LoadReportingChart3(clickedCongDate_Chart1, Nothing)
                        End If

                        'Draw Chart 3 for clicked congestion date on chart 2
                        If chartName = "chartCapacity2" Then
                            ClearChart3AndChart4()
                            'Draw chart 3
                            LoadReportingChart3(clickedCongDate_Chart1, clickedCongDate_Chart2)
                        End If

                        'Draw chart 4 for clicked object on chart 3
                        If chartName = "chartCapacity3" Then
                            LoadReportingChart4(clickedObject)
                        End If
                    Else
                        'If chartName = "chartCapacity1" AndAlso myChart.XAxis.Markers.Count > 0 Then
                        '    Dim clickedMarkerDate As Object = myChart.XAxis.GetValueAtX(e.X.ToString + "," + e.Y.ToString)
                        '    For Each obj As DateTime In jobConfigChangeDate
                        '        If CType(clickedMarkerDate, DateTime) = obj Then
                        '            xtcRight.SelectedTabPageIndex = 4
                        '        End If
                        '    Next
                        'End If
                    End If
                End If

                Dim ChartLegendRect As System.Drawing.Rectangle = myChart.LegendBox.GetRectangle()
                If ChartLegendRect.Contains(e.X, e.Y) Then
                    If TypeOf hit.Object Is LegendEntry Then
                        Dim chartLegendEntry As LegendEntry = CType(hit.Object, LegendEntry)
                        ShowHideChartSeries(myChart, chartLegendEntry.Name)
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub AddAxisMarkerX(ByRef chart As dotnetCHARTING.WinForms.Chart, ByVal xAxisValue As String)
        Dim datestart As DateTime = Nothing
        Dim cl As Color = Color.Yellow
        Dim axisMarkerObj As AxisMarker

        If chart.Name = "chartCapacity3" Then
            axisMarkerObj = New AxisMarker("", New Line(cl, 3), xAxisValue)
        Else
            If chart.Name = "chartCapacity1" Then
                For Each am As AxisMarker In chart.XAxis.Markers
                    If am.Label.Text = "" AndAlso am.Line.Color = Color.Yellow Then
                        chart.XAxis.Markers.Remove(am)
                        Exit For
                    End If
                Next
            End If
            datestart = Convert.ToDateTime(xAxisValue)
            axisMarkerObj = New AxisMarker("", New Line(cl, 3), datestart)
        End If

        axisMarkerObj.LegendEntry.Visible = False
        axisMarkerObj.Label.Alignment = StringAlignment.Near
        axisMarkerObj.Label.LineAlignment = StringAlignment.Far
        axisMarkerObj.BringToFront = True
        chart.XAxis.Markers.Add(axisMarkerObj)
        chart.RefreshChart()
    End Sub

    Private Sub cmChart_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmChart.Opening
        Dim myChart As Chart = DirectCast(cmChart.SourceControl, Chart)
        Dim hitchart As HitTestInfo = myChart.HitTest()
        If TypeOf hitchart.Object Is Element Then
            Dim el As Element = CType(hitchart.Object, Element)
            tsmi_Chart_SelectedObject.Text = "Selected Cell: " & el.Name

            tsmi_Chart_Send2Map.Enabled = True
            tsmi_Chart_Send2Stats.Enabled = True
        Else
            tsmi_Chart_SelectedObject.Text = "Selected Cell: "
            tsmi_Chart_Send2Map.Enabled = False
            tsmi_Chart_Send2Stats.Enabled = False
        End If
        myChart = Nothing
    End Sub

    Private Sub tsmi_Chart_Send2Stats_Click(sender As Object, e As EventArgs) Handles tsmi_Chart_Send2Stats.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim cell As String = Replace(cmChart.Items(0).Text, "Selected Cell: ", "").Trim
            Dim ch As Chart = DirectCast(cmChart.SourceControl, Chart)
            Dim tech As String = TryCast(grdObject3.DataSource, DataTable).Select("ObjectName='" & cell & "'")(0)("IOS_Tech")
            Dim targetType As String = TryCast(grdObject3.DataSource, DataTable).Select("ObjectName='" & cell & "'")(0)("CounterType")
            If tech IsNot Nothing AndAlso targetType IsNot Nothing Then

                objfrmTech = Nothing
                If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                    frmMDI.OpenTechFormDynamically(tech, objfrmTech, False)
                Else
                    objfrmTech = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
                End If

                'set stat parameters correctly
                objfrmTech.Network = tech
                    objfrmTech.SetButtonsForLaunch(tech, targetType, False)
                    objfrmTech.FindNodeTreeviewStats(cell.ToString.Trim)
                    'launch
                    Call frmTechnology.btnApplyStats_Click(frmTechnology.btnApplyStats, Nothing)
                    objfrmTech.tcTabControlHighTopX.SelectedTabPageIndex = 0
                End If
                ch = Nothing
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_Chart_Send2Map_Click(sender As Object, e As EventArgs) Handles tsmi_Chart_Send2Map.Click
        Dim cell As String = Replace(cmChart.Items(0).Text, "Selected Cell: ", "").Trim
        Dim ch As Chart = DirectCast(cmChart.SourceControl, Chart)
        Dim tech As String = TryCast(grdObject3.DataSource, DataTable).Select("ObjectName='" & cell & "'")(0)("IOS_Tech")
        Dim targetType As String = TryCast(grdObject3.DataSource, DataTable).Select("ObjectName='" & cell & "'")(0)("CounterType")

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            objfrmTech = Nothing
            If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech)) Then
                frmMDI.OpenTechFormDynamically(tech, objfrmTech, False)
            Else
                objfrmTech = objFrmTechList.Where(Function(x) x.Network.Equals(tech)).LastOrDefault()
            End If

            If cell IsNot Nothing Then
                If flpSourceBtn_GetChecked(tech, objfrmTech.flpCounterTypeTopX)(0).SourceButtonTag = "CELL" Then
                    frmMapWindow.Cell_SearchAndDisplay(cell, Nothing, "CELLID")
                ElseIf flpSourceBtn_GetChecked(tech, objfrmTech.flpCounterTypeTopX)(0).SourceButtonTag = "SITE" Then
                    frmMapWindow.Cell_SearchAndDisplay(cell, Nothing, "SiteCode")
                End If
            End If
            ch = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub frmCapacity_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If _isResizing Then
            _isResizing = False
            Me.xtcRight.Refresh()
            Me.Refresh()
        End If
    End Sub

    Private Sub frmCapacity_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        _isResizing = True
        Charts_ResizeWidth()
        If xtcRight.SelectedTabPageIndex = 1 Then
            LoadCongRuleProperties(gvCongRules.GetDataRow(gvCongRules.FocusedRowHandle))
        End If
        xtcRight.Refresh()
        Me.Refresh()
    End Sub

    Private Sub tsmi_SendSelected2Console_Click(sender As Object, e As EventArgs) Handles tsmi_SendSelected2Console.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If ((dtCapChart3 IsNot Nothing) AndAlso (techChart3 IsNot Nothing) AndAlso (targetTypeChart3 IsNot Nothing) AndAlso (objectNameChart3 IsNot Nothing)) Then
                Dim row() As DataRow = dtCapChart3.Select("ObjectName='" & objectNameChart3 & "'")
                If row.Count > 0 Then
                    SendToConsoleTree(techChart3, row.CopyToDataTable().DefaultView.ToTable(True, "ObjectName").AsEnumerable().ToArray())
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub SendToConsoleTree(ByVal tech As String, ByVal rows() As DataRow)
        'Dim techn As String = Me.GetTechnologyName(tech, cmbVendor.SelectedItem.ToString, "Tech")
        frmMapWindow.SelectionToTreeStep1(tech, rows.Count, False, New IOS.Library.SelectionToTreeFlags())
        For Each item As DataRow In rows
            frmMapWindow.SelectionToTreeStep2(tech, item("ObjectName").ToString(), False)
        Next
    End Sub

    Private Sub tsmi_SendSelected2Map_Click(sender As Object, e As EventArgs) Handles tsmi_SendSelected2Map.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (dtCapChart3 IsNot Nothing) Then
                Dim dt As DataTable = dtCapChart3.Select("ObjectName='" & objectNameChart3 & "'").CopyToDataTable
                frmMapWindow.MapDataToSingleLayer(dt, "CapModule", "ObjectName", "CELLNAME", "Individual Theme", "CapCongestionRuleName", "ObjectName,CapCongestionRuleName")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SendAll2Map_Click(sender As Object, e As EventArgs) Handles tsmi_SendAll2Map.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (dtCapChart3 IsNot Nothing) Then
                frmMapWindow.MapDataToSingleLayer(dtCapChart3, "CapModule", "ObjectName", "CELLNAME", "Individual Theme", "CapCongestionRuleName", "ObjectName,CapCongestionRuleName")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnDump2Csv_Click(sender As Object, e As EventArgs) Handles btnDump2Csv.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtExport As New DataTable
            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Comma Delimited|*.csv"
            objFileDlg.Title = "Save a CSV File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Exporting Results to CSV...")
                    Application.DoEvents()

                    dtExport = GetDataToExportResults()

                    IOS.Library.IOSDevExpressGrid.DataTable2CSV(dtExport, objFileDlg.FileName)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnExportConfig2Csv_Click(sender As Object, e As EventArgs) Handles btnExportConfig2Csv.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtExport As New DataTable
            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Comma Delimited|*.csv"
            objFileDlg.Title = "Save a CSV File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Exporting Config to CSV...")
                    Application.DoEvents()

                    dtExport = GetDataToExportConfig()

                    IOS.Library.IOSDevExpressGrid.DataTable2CSV(dtExport, objFileDlg.FileName)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmJobTree_Opening(sender As Object, e As CancelEventArgs) Handles cmJobTree.Opening
        Try
            tsmi_CopyJob.Enabled = False
            tsmi_CopyRule.Enabled = False

            Dim node As TreeListNode = tlCongestionJobs.FocusedNode
            If node.Level = 0 Then
                tsmi_CopyJob.Enabled = True
                tsmi_CopyRule.Enabled = False
            ElseIf node.Level = 2 Then
                tsmi_CopyJob.Enabled = False
                tsmi_CopyRule.Enabled = True
            End If
        Catch
        End Try
    End Sub

    Private Sub tsmi_CopyJob_Click(sender As Object, e As EventArgs) Handles tsmi_CopyJob.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim node As TreeListNode = tlCongestionJobs.FocusedNode
            If node.Level = 0 Then
                Dim objdlgCongJob As New dlgCongestionJob()
                objdlgCongJob.tobeCopiedJobID = selectedJobID
                objdlgCongJob.tobeCopiedJobName = node.GetDisplayText("ObjectName").ToString
                objdlgCongJob.copyJob = True
                objdlgCongJob.ShowDialog()

                If (copyJobName IsNot Nothing) Then
                    'RemoveHandler tlCongestionJobs.FocusedNodeChanged, AddressOf tlCongestionJobs_FocusedNodeChanged
                    LoadCongestionJobs()
                    'AddHandler tlCongestionJobs.FocusedNodeChanged, AddressOf tlCongestionJobs_FocusedNodeChanged
                    tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNodeByFieldValue("ObjectName", copyJobName))
                    tlCongestionJobs_FocusedNodeChanged(Nothing, Nothing)
                    xtcRight.SelectedTabPageIndex = 1
                Else
                    xtcRight.SelectedTabPageIndex = 0
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_CopyRule_Click(sender As Object, e As EventArgs) Handles tsmi_CopyRule.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim node As TreeListNode = tlCongestionJobs.FocusedNode
            If node.Level = 2 Then
                Dim objdlgCongJob As New dlgCongestionJob()
                objdlgCongJob.tobeCopiedRuleID = selectedRuleID
                objdlgCongJob.tobeCopiedRuleName = node.GetDisplayText("ObjectName").ToString
                objdlgCongJob.copyRule = True
                objdlgCongJob.ShowDialog()

                If (copyRuleName IsNot Nothing) Then
                    LoadCongestionJobs()
                    tlCongestionJobs.SetFocusedNode(tlCongestionJobs.FindNodeByFieldValue("ObjectName", copyRuleName))
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class