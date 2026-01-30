Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports dotnetCHARTING.WinForms
Imports IOS.Library
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class frmParameterHistory

#Region "Variables Declaration"

    Private connStr As String = "Dsn=IOS_Server;uid=sa;pwd=1234"

    Private dtChart_1 As DataTable = Nothing
    Private dtChart_2A As DataTable = Nothing
    Private dtChartAndGridData As New DataTable
    Private dtChart_2B As DataTable = Nothing

    Private techChart2a As String = Nothing
    Private cellIdChart2a As String = Nothing
    Private TopXMapType As Integer = 1
    Private sc As New SeriesCollection
    Private chartTech As String = Nothing
    Private isChartRightClick As Boolean = False
    Private timeStampChart1 As DateTime = Nothing
    Private cellOrParameterClickOnChart2aOrChart2b As String = Nothing
    Private ParameterFilterName As String = Nothing
    Private dtparameterFileter As DataTable
    Private parameterFilterColumnName As String = "P_abbr_Name"
    Private dtTemplateManagerData As DataTable = Nothing
    Private p As Point = Point.Empty

#End Region

#Region "Common Helper"

    Private Sub ConfigurParameterHistoryForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                cm_OT_tsmi_copy, cm_OT_tsmi_paste, cm_OT_tsmi_CopyToTag, cm_OT_tsmi_CheckChild, cm_OT_tsmi_UnCheck, tsmi_OT_MapCell, tsmi_ReloadTree, tsmi_OT_Exception, tsmi_PH_Copy, tsmi_PH_Paste,
                tsmi_AddParameterFilter, tsmi_SendObjectToTree, tsmi_SendObjectToMap, tsmi_sendconsoletree, tsmi_sendAlltoConsoletee, tsmi_SendToMap, tsmi_SendSelectedToMap, tsmi_TopXMapType,
                tsmi_dgv_SelectAll, tsmi_dgv_CopyClipboard, tsmi_dgv_ExportExcel
            }

            For Each frmControl As Object In formControls
                winCtrl = form.FindControlByName(frmControl.Name)
                If Not winCtrl Is Nothing Then
                    frmControl.Enabled = winCtrl.DefaultEnable
                    frmControl.Visible = winCtrl.DefaultVisible
                End If
            Next

        End If
    End Sub

    Public Sub SetConnectionString(ByVal connStr As String)
        Me.connStr = connStr
    End Sub

    Private Sub SendToMap(ByVal rows() As DataRow)
        Dim objectName As String = String.Empty
        If (cmbTargetObject.SelectedIndex > 0) Then
            objectName = cmbTargetObject.SelectedItem.ToString
        Else
            objectName = Me.GetTechnologyName(techChart2a, cmbVendor.SelectedItem.ToString, "Object")
        End If

        Dim dt_filtered As DataTable = rows.CopyToDataTable().Clone()
        dt_filtered.Columns("Cellid").DataType = GetType(String)

        For Each item As DataRow In rows
            Dim r As DataRow = dt_filtered.NewRow()
            Dim index As Integer = 0
            For Each item1 As DataColumn In dtChart_2A.Columns
                If dt_filtered.Columns.Contains(item1.ColumnName) Then
                    r(index) = item(index)
                    index = index + 1
                End If
            Next
            dt_filtered.Rows.Add(r)
        Next

        'Exit Sub

        dt_filtered.Columns("Cellid").ColumnName = objectName
        dt_filtered.Columns("Counter").ColumnName = "ParamHistoryMap"

        'construct filter
        Dim techn As String = Me.GetTechnologyName(techChart2a, cmbVendor.SelectedItem.ToString, "Tech")
        mdlCommonModule.SendToMap(techn, objectName, dt_filtered, TopXMapType, EnumSendToMap.FromPH, , , , , , , techn)

        dt_filtered.Dispose()
        dt_filtered = Nothing
    End Sub

    Private Function GetTemplateManagerData(ByVal tech As String, ByVal vendor As String, ByVal columnName As String) As DataTable
        Dim temMangData As DataTable = BindTemplateManagerData()
        Dim tempDataRow As DataRow()
        Dim tempManagerTb As DataTable = Nothing
        If (temMangData.Rows.Count > 0) Then
            If (columnName = "") Then
                tempDataRow = temMangData.Select("Technology='" & tech & "' And Vendor='" & vendor & "' ")
            Else
                tempDataRow = temMangData.Select("Technology='" & tech & "' And Vendor='" & vendor & "' and " & columnName & "=1")
            End If

            If (tempDataRow.Count > 0) Then
                tempManagerTb = tempDataRow.CopyToDataTable()
            End If
            If Not (tempManagerTb Is Nothing) Then
                tempManagerTb.Columns.Remove("Technology")
                tempManagerTb.Columns.Remove("Vendor")
                tempManagerTb.Columns.Remove("techn")
                tempManagerTb.Columns.Remove("NE_release")
                tempManagerTb.Columns.Remove("EnabledInTemplate")
                tempManagerTb.Columns.Remove("EnabledInCategory")
            End If
        End If
        Return tempManagerTb
    End Function

    Private Function BindTemplateManagerData() As DataTable
        If (dtTemplateManagerData Is Nothing) Then
            dtTemplateManagerData = IOS.DataLibrary.clsSQLCommands.GetTemplateManager(connStr)
        End If
        Return dtTemplateManagerData
    End Function

    Private Function GetDistinctFilterData(ByVal SelectedColumn As String, ByVal pmData As DataTable) As DataTable
        Try
            If Not (pmData Is Nothing) Then
                Dim distObject As DataTable = pmData.DefaultView.ToTable(True, SelectedColumn)
                Return distObject
            End If
            Return pmData
        Catch
        End Try
        Return Nothing
    End Function

    Sub clearComboBox(ByRef ctrl As DevExpress.XtraEditors.ComboBoxEdit, ByVal firstItem As String)
        ctrl.SuspendLayout()
        ctrl.Properties.Items.Clear()
        ctrl.Properties.Items.Insert(0, firstItem)
        ctrl.SelectedIndex = 0
        ctrl.Refresh()
        ctrl.ResumeLayout()
    End Sub

    Sub SetParameterHistoryTableWidthOnPanelWidthChange(Optional onlyOnPageLoad As Boolean = False)
        tlpCharts.Width = IosTableLayoutPanel3.Width - 30
        If (onlyOnPageLoad) Then
            Chart_2A.ResetToParent()
            Chart_2B.ResetToParent()
        End If
    End Sub

    Private Function GetTechnologyName(ByVal tech As String, ByVal vendor As String, ByVal returnObjectColumnsName As String) As String
        Dim rows() As DataRow = dt_IOS_ObjectConfig.Select("Vendor='" & vendor & "' AND Technology='" & tech & "' AND ParamHistory=1")
        If (rows.Count > 0) Then
            Return rows(0)(returnObjectColumnsName).ToString
        End If
        Return ""
    End Function

    Public Sub SendToConsoleTree(ByVal tech As String, ByVal rows() As DataRow)
        Dim techn As String = Me.GetTechnologyName(tech, cmbVendor.SelectedItem.ToString, "Tech")
        frmMapWindow.SelectionToTreeStep1(techn, rows.Count, False, New IOS.Library.SelectionToTreeFlags())
        For Each item As DataRow In rows
            frmMapWindow.SelectionToTreeStep2(techn, item("cellid").ToString(), False)
        Next
    End Sub

    Private Function String2DataFields2(ByRef str() As String, ByRef xval As String) As String
        Dim stroutput As String
        Dim i As Integer

        stroutput = "XValue=" & xval ' a(0)
        For i = 1 To UBound(str)
            stroutput = stroutput & "," & " Yvalue=" & str(i)
        Next
        String2DataFields2 = stroutput
    End Function

    Public Sub BindParameterToList(ByRef v As DevExpress.XtraEditors.ListBoxControl, ByVal pmData As DataTable)
        v.Items.Clear()
        For Each item As DataRow In pmData.Rows
            If (item IsNot DBNull.Value) Then
                v.Items.Add(item(parameterFilterColumnName).ToString())
            End If
        Next
    End Sub

    Private Function GetHMFilterIncludedValue() As String
        Dim filters As String = String.Empty
        If Not String.IsNullOrEmpty(txtParamFilterInclude.Text) Then
            Dim items() As String = txtParamFilterInclude.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            For Each item As String In items
                filters = "'" & item & "'," & filters
            Next
        End If
        Return filters.TrimEnd(",")
    End Function

    Private Function GetHMFilterExcludedValue() As String
        Dim filters As String = String.Empty
        If Not String.IsNullOrEmpty(txtParamFilterExclude.Text) Then
            Dim items() As String = txtParamFilterExclude.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            For Each item As String In items
                filters = "'" & item & "'," & filters
            Next
        End If
        Return filters.TrimEnd(",")
    End Function

    Function GetFilterString(Optional ByVal IsWithInnerJoinOfAlias As Boolean = False) As String
        Dim filterString As String = ""
        Dim HMFiltersInc As String = GetHMFilterIncludedValue()
        Dim HMFiltersExc As String = GetHMFilterExcludedValue()

        If Not (String.IsNullOrEmpty(HMFiltersInc)) Then
            filterString = " and ParameterName in (" & HMFiltersInc & ") "
        End If

        If Not (String.IsNullOrEmpty(HMFiltersExc)) Then
            filterString &= " and ParameterName not in (" & HMFiltersExc & ") "
        End If

        Dim tech As String = Me.GetTechnologyName(cmbTechnology.SelectedItem.ToString, cmbVendor.SelectedItem.ToString, "Tech")
        If (cmbTechnology.SelectedIndex > 0 AndAlso cmbTargetObject.SelectedIndex > 0) Then
            Try
                Dim checkedObejct = TreeList_Checked2String(tech, cmbTargetObject.SelectedItem.ToString, "ObjectName", tvObjectTree, cmbTargetObject, "ObjectName")
                If Not (checkedObejct = "IN ()") Then
                    If (IsWithInnerJoinOfAlias) Then
                        filterString = filterString & " and a.CELLNAME " & checkedObejct
                    Else
                        filterString = filterString & " and CELLNAME " & checkedObejct
                    End If
                End If
            Catch ex As Exception

            End Try

        End If
        Return filterString.Replace("'", "''")
    End Function

    Private Function CheckItemIsExist(ByVal item As String, ByRef checkeBoxList As DevExpress.XtraEditors.ListBoxControl) As Boolean
        Dim isResult As Boolean = False
        For Each listItem As DevExpress.XtraEditors.Controls.ListBoxItem In checkeBoxList.Items
            If (listItem.Value.ToUpper.Trim = item.ToUpper.Trim) Then
                checkeBoxList.SetSelected(checkeBoxList.Items.IndexOf(listItem), True)
                isResult = True
                Exit For
            End If
        Next
        Return isResult
    End Function

#End Region

#Region "Chart & Grid Helper"

    Private Sub BindChart1()
        dtChart_1 = Nothing
        dtChart_1 = GetDataForChart1()
        Chart_1.SuspendLayout()
        UpdateChart_1(Chart_1, dotnetCHARTING.WinForms.Scale.Stacked)
        Chart_1.RefreshChart()
        Chart_1.ResumeLayout()

        Chart_2A.ClearAll()
        Chart_2B.ClearAll()
        Chart_3.ClearAll()
        vdgvChart2A.DataSource = Nothing
        gvChart2A.Columns.Clear()
        lblGrid2A.Text = ""
        vdgvChart3.DataSource = Nothing
        gvChart3.Columns.Clear()
        lblGrid2B.Text = ""
    End Sub

    Private Function GetDataForChart1() As DataTable
        Dim objType As String = "%"
        If cmbTargetObject.SelectedIndex > 0 Then
            objType = cmbTargetObject.SelectedItem.ToString()
        End If

        Dim parray()() As String = {
            New String() {"@StartDate", Chr(39) & Convert.ToDateTime(tmpPHStartDate.EditValue).ToString("yyyy-MM-dd") & Chr(39)},
            New String() {"@EndDate", Chr(39) & Convert.ToDateTime(tmpPHEndDate.EditValue).AddDays(1).ToString("yyyy-MM-dd") & Chr(39)},
            New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString() & Chr(39)},
            New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
            New String() {"@ObjectType", Chr(39) & objType & Chr(39)}
                                   }

        Dim sqlQueryData() As String = GetSQL(7701, parray)
        Return IOS.DataLibrary.DataAccessorODBC.GetDataTable(sqlQueryData(0), sqlQueryData(1))
    End Function

    Private Function AddMinMaxDateToDataTable(ByVal dt As DataTable) As DataTable
        Try
            'check start date
            Dim mindate As DateTime = dt.AsEnumerable().Min(Function(w) w.Field(Of DateTime)(dt.Columns(0).ColumnName))
            Dim maxdate As DateTime = dt.AsEnumerable().Max(Function(w) w.Field(Of DateTime)(dt.Columns(0).ColumnName))

            If mindate > tmpPHStartDate.EditValue Then
                Dim newrow As DataRow = dt.NewRow
                newrow(0) = tmpPHStartDate.EditValue
                For i = 1 To dt.Columns.Count - 1
                    newrow(i) = 0
                Next
                dt.Rows.Add(newrow)
                newrow = dt.NewRow
                newrow(0) = DateAdd(DateInterval.Day, -1, CType(tmpPHStartDate.EditValue, Date))
                For i = 1 To dt.Columns.Count - 1
                    newrow(i) = 0
                Next
                dt.Rows.Add(newrow)
            End If

            If maxdate < tmpPHEndDate.EditValue Then
                Dim newrow As DataRow = dt.NewRow
                newrow(0) = tmpPHEndDate.EditValue
                For i = 1 To dt.Columns.Count - 1
                    newrow(i) = 0
                Next
                dt.Rows.Add(newrow)
            End If
            Return dt
        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Private Sub UpdateChart_1(ByRef chartObj As dotnetCHARTING.WinForms.Chart, ByVal scale As dotnetCHARTING.WinForms.Scale)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            chartObj.SuspendLayout()
            chartObj.Title = "Changes Per Technology"
            chartObj.DefaultSeries.Type = SeriesType.Marker

            tsmiShowGridChart_1.Text = "Show Grid"
            sccChart_1.Collapsed = True

            Dim yaxis2 As New IOSAxis()
            yaxis2.Orientation = Orientation.Left
            Dim ChartElements As New List(Of String)
            ChartElements.Add("Date")
            If (cmbTechnology.SelectedIndex = 0 Or cmbTechnology.SelectedIndex = -1) Then
                ChartElements.Add("2G")
                ChartElements.Add("3G")
                ChartElements.Add("4G")
                ChartElements.Add("5G")
                yaxis2.ElementListToApply.Add("2G")
                yaxis2.ElementListToApply.Add("3G")
                yaxis2.ElementListToApply.Add("4G")
                yaxis2.ElementListToApply.Add("5G")
            Else
                chartObj.Title = "Changes for " & cmbTechnology.SelectedItem.ToString
                ChartElements.Add(cmbTechnology.SelectedItem.ToString)
                yaxis2.ElementListToApply.Add(cmbTechnology.SelectedItem.ToString)
            End If

            yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
            SetChartXAxis(chartObj, "Date")

            Dim de As DataEngine = New DataEngine(AddMinMaxDateToDataTable(dtChart_1.Copy()))
            de.DataFields = String2DataFields2(ChartElements.ToArray, ChartElements(0))
            sc = de.GetSeries()
            chartObj.LegendBox.Orientation = Orientation.Bottom
            Dim i As Integer
            Dim color As System.Drawing.Color = Color.Red
            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Bar
                If (sc(i).Name.ToLower = "3g") Then
                    color = System.Drawing.Color.Blue
                End If
                If (sc(i).Name.ToLower = "4g") Then
                    color = System.Drawing.Color.Orange
                End If
                If (sc(i).Name.ToLower = "5g") Then
                    color = System.Drawing.Color.MediumPurple
                End If
                sc(i).DefaultElement.Color = color
                sc(i).DefaultElement.Marker.Type = i
                sc(i).YAxis = yaxis2
            Next
            chartObj.SeriesCollection.Clear()
            chartObj.SeriesCollection.Add(sc)
            chartObj.Series.Data = dtChart_1

            chartObj.RefreshChart()
            chartObj.ResumeLayout()
        Catch ex As Exception
            frmMapWindow.SetStatus(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub CreateChart2A(ByVal configDate As Date, ByVal tech As String, ByRef chartObj As dotnetCHARTING.WinForms.Chart)
        Try
            tsmiShowGrid_Chart_2A.Text = "Show Grid"
            sccChart_2A.Collapsed = True

            chartObj.SuspendLayout()

            chartObj.Title = tech & " changes per Object on " & configDate.ToString()
            chartObj.DefaultSeries.Type = SeriesType.Marker
            Dim yaxis2 As New IOSAxis()
            yaxis2.Orientation = Orientation.Left
            yaxis2.NumberPrecision = 0
            yaxis2.MinimumInterval = 1

            Dim parray()() As String = {
                New String() {"@ConfigDate", Chr(39) & configDate.ToString("yyyyMMdd") & Chr(39)},
                New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString(True) & Chr(39)},
                New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
                New String() {"@Technology", Chr(39) & tech & Chr(39)},
                New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex > 0, Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39), "NULL")}
            }

            Dim sqlQueryData() As String = GetSQL(7702, parray)
            If Not (sqlQueryData Is Nothing) Then
                techChart2a = tech
                cellIdChart2a = Nothing
                dtChart_2A = IOS.DataLibrary.DataAccessorODBC.GetDataTable(sqlQueryData(0), sqlQueryData(1))
                If (dtChart_2A.Rows.Count > 400) Then
                    dtChart_2A = dtChart_2A.AsEnumerable().Take(400).CopyToDataTable()
                    chartObj.Title = "Top 400 " & chartObj.Title
                End If
                If (dtChart_2A IsNot Nothing AndAlso dtChart_2A.Rows.Count > 20) Then
                    chartObj.Dock = DockStyle.Left
                    chartObj.XAxis.StaticColumnWidth = 0
                    chartObj.Width = (dtChart_2A.Rows.Count * 40)
                Else
                    chartObj.Dock = DockStyle.Fill
                    chartObj.XAxis.StaticColumnWidth = 25
                    chartObj.ResetToParent()
                End If
                Dim ChartElements As New List(Of String)
                ChartElements.Add("CELLID")
                ChartElements.Add("Counter")

                yaxis2.ElementListToApply.Add("Counter")
                yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked

                SetChartXAxis(chartObj, "CELLID")
                Dim de As DataEngine = New DataEngine(dtChart_2A)
                de.DataFields = String2DataFields2(ChartElements.ToArray, ChartElements(0))
                sc = de.GetSeries()
                chartObj.LegendBox.Orientation = Orientation.Bottom
                Dim i As Integer
                Dim rnd As Random = New Random(10)
                For i = 0 To sc.Count() - 1
                    sc(i).Type = SeriesType.Bar
                    Select Case tech.ToUpper
                        Case "2G"
                            sc(i).DefaultElement.Color = Color.Red
                        Case "3G"
                            sc(i).DefaultElement.Color = Color.Blue
                        Case "4G"
                            sc(i).DefaultElement.Color = Color.Orange
                        Case "5G"
                            sc(i).DefaultElement.Color = Color.MediumPurple
                    End Select

                    sc(i).DefaultElement.Marker.Type = i
                    sc(i).YAxis = yaxis2
                Next
                chartObj.SeriesCollection.Clear()
                chartObj.SeriesCollection.Add(sc)
                chartObj.Series.Data = dtChart_2A

                chartObj.RefreshChart()
                chartObj.ResumeLayout()
            Else
                lblMsg.Text = "Chart 2A data Not found"
                lblMsg.Visible = False
            End If
        Catch ex As Exception
            frmMapWindow.SetStatus(ex.Message)
        End Try
    End Sub

    Private Sub CreateChart2B(ByVal configDate As Date, ByVal tech As String, ByRef chartObj As dotnetCHARTING.WinForms.Chart)
        Try
            tsmiShowGrid_Chart_2B.Text = "Show Grid"
            sccChart_2B.Collapsed = True
            chartObj.SuspendLayout()

            chartObj.Title = tech & " changes per Parameter on " & configDate.ToString()
            chartObj.DefaultSeries.Type = SeriesType.Marker
            Dim yaxis2 As New IOSAxis()
            yaxis2.Orientation = Orientation.Left
            yaxis2.NumberPrecision = 0
            yaxis2.MinimumInterval = 1

            Dim parray()() As String = {
                New String() {"@ConfigDate", Chr(39) & configDate.ToString("yyyyMMdd") & Chr(39)},
                New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString(True) & Chr(39)},
                New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
                New String() {"@Technology", Chr(39) & IIf(cmbTechnology.SelectedIndex = 0, tech, cmbTechnology.SelectedItem.ToString()) & Chr(39)},
               New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex > 0, Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39), "NULL")}
            }

            Dim sqlQueryData() As String = GetSQL(7704, parray)
            If Not (sqlQueryData Is Nothing) Then
                dtChart_2B = IOS.DataLibrary.DataAccessorODBC.GetDataTable(sqlQueryData(0), sqlQueryData(1))
                If (dtChart_2B.Rows.Count > 400) Then
                    dtChart_2B = dtChart_2B.AsEnumerable().Take(400).CopyToDataTable()
                    chartObj.Title = "Top 400 " & chartObj.Title
                End If
                If (dtChart_2B IsNot Nothing AndAlso dtChart_2B.Rows.Count > 20) Then
                    chartObj.Dock = DockStyle.Fill
                    chartObj.XAxis.StaticColumnWidth = 0
                    chartObj.Width = (dtChart_2B.Rows.Count * 40)
                Else
                    chartObj.Dock = DockStyle.Fill
                    chartObj.XAxis.StaticColumnWidth = 25
                    chartObj.ResetToParent()
                End If
                Dim ChartElements As New List(Of String)
                ChartElements.Add("ParameterName")
                ChartElements.Add("Counter")
                yaxis2.ElementListToApply.Add("Counter")
                yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked

                SetChartXAxis(chartObj, "ParameterName")

                Dim de As DataEngine = New DataEngine(dtChart_2B)
                de.DataFields = String2DataFields2(ChartElements.ToArray, ChartElements(0))
                sc = de.GetSeries()
                chartObj.LegendBox.Orientation = Orientation.Bottom
                Dim i As Integer
                Dim rnd As Random = New Random(10)
                For i = 0 To sc.Count() - 1
                    sc(i).Type = SeriesType.Bar
                    Select Case tech
                        Case "2G"
                            sc(i).DefaultElement.Color = Color.Red
                        Case "3G"
                            sc(i).DefaultElement.Color = Color.Blue
                        Case "4G"
                            sc(i).DefaultElement.Color = Color.Orange
                        Case "5G"
                            sc(i).DefaultElement.Color = Color.MediumPurple
                    End Select
                    sc(i).DefaultElement.Marker.Type = i
                    sc(i).YAxis = yaxis2
                Next
                chartObj.SeriesCollection.Clear()
                chartObj.SeriesCollection.Add(sc)
                chartObj.Series.Data = dtChart_2B

                chartObj.RefreshChart()
                chartObj.ResumeLayout()
            Else
                lblMsg.Text = "Chart 2B data Not found"
                lblMsg.Visible = False
            End If
        Catch ex As Exception
            frmMapWindow.SetStatus(ex.Message)
        End Try
    End Sub

    Private Sub CreateChart3(ByVal name As String, ByRef chartObj As dotnetCHARTING.WinForms.Chart, ByVal chartFrom As ParameterHistoryChart)
        Try
            tsmiShowGrid_Chart_3.Text = "Show Grid"
            sccChart_3.Collapsed = True
            vdgvChart3.DataSource = Nothing
            gvChart3.Columns.Clear()
            chartObj.SuspendLayout()
            chartObj.Title = "Changes of Selected Object"

            chartObj.DefaultSeries.Type = SeriesType.Marker
            Dim yaxis2 As New IOSAxis()
            yaxis2.Orientation = Orientation.Left
            yaxis2.NumberPrecision = 0
            yaxis2.MinimumInterval = 1
            '' yaxis2.Interval = 10
            chartObj.Tag = chartFrom

            Dim parray()() As String = {
                New String() {"@CellName", Chr(39) & name & Chr(39)},
                New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString() & Chr(39)},
                New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
                New String() {"@Technology", Chr(39) & chartTech & Chr(39)},
                New String() {"@StartDate", Chr(39) & Convert.ToDateTime(tmpPHStartDate.EditValue).ToString("yyyy-MM-dd") & Chr(39)},
                New String() {"@EndDate", Chr(39) & Convert.ToDateTime(tmpPHEndDate.EditValue).AddDays(1).ToString("yyyy-MM-dd") & Chr(39)},
                New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex = 0, "NULL", Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39))}
            }
            Dim sqlQueryData() As String
            If chartFrom = ParameterHistoryChart.Chart3_2A_Clicked Then
                sqlQueryData = GetSQL(7703, parray)
            Else
                sqlQueryData = GetSQL(7705, parray)
            End If

            If Not (sqlQueryData Is Nothing) Then
                dtChartAndGridData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(sqlQueryData(0), sqlQueryData(1))
                Dim ChartElements As New List(Of String)
                If (chartFrom = ParameterHistoryChart.Chart3_2B_Clicked) Then
                    chartObj.Title = "Changes of Selected Parameter"
                End If
                chartObj.Title = chartObj.Title & " : " & name
                ChartElements.Add("ConfigDate")
                ChartElements.Add("Counter")
                yaxis2.ElementListToApply.Add("Counter")
                yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                SetChartXAxis(chartObj, "ConfigDate")
                chartObj.XAxis.TimePadding = New TimeSpan(200, 0, 0, 0)

                Dim de As DataEngine = New DataEngine(AddMinMaxDateToDataTable(dtChartAndGridData.Copy()))
                de.DataFields = String2DataFields2(ChartElements.ToArray, ChartElements(0))
                sc = de.GetSeries()
                chartObj.LegendBox.Orientation = Orientation.Bottom
                Dim i As Integer
                Dim rnd As Random = New Random(10)
                For i = 0 To sc.Count() - 1
                    sc(i).Type = SeriesType.Bar
                    Select Case chartTech.ToUpper
                        Case "2G"
                            sc(i).DefaultElement.Color = Color.Red
                        Case "3G"
                            sc(i).DefaultElement.Color = Color.Blue
                        Case "4G"
                            sc(i).DefaultElement.Color = Color.Orange
                        Case "5G"
                            sc(i).DefaultElement.Color = Color.MediumPurple
                    End Select
                    sc(i).DefaultElement.Marker.Type = i
                    sc(i).YAxis = yaxis2
                Next
                chartObj.SeriesCollection.Clear()
                chartObj.SeriesCollection.Add(sc)
                chartObj.Series.Data = dtChartAndGridData
                chartObj.RefreshChart()
                chartObj.ResumeLayout()
            Else
                lblMsg.Text = "Chart 3 data Not found"
                lblMsg.Visible = False
            End If
        Catch ex As Exception
            frmMapWindow.SetStatus(ex.Message)
        End Try
    End Sub

    Private Sub SetChartXAxis(ByRef chartObj As dotnetCHARTING.WinForms.Chart, ByVal chartElements As String)
        Try
            chartObj.XAxis.TickLabelMode = TickLabelMode.Angled
            chartObj.XAxis.TickLabelAngle = 45
            If chartElements = "ConfigDate" Or chartElements = "Date" Then
                chartObj.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                chartObj.XAxis.ScaleRange = New ScaleRange(tmpPHStartDate.EditValue, tmpPHEndDate.EditValue)
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

    Private Sub BindChartGrid(ByRef gridCtrl As GridControl, ByRef gridView As GridView, ByVal cellName As String, ByVal tech As String, ByVal phChart As ParameterHistoryChart, Optional ByVal configdate As Date = Nothing)
        Try
            Dim parray()() As String
            Dim sqlQueryData() As String = Nothing
            Select Case phChart
                Case ParameterHistoryChart.GridChart2A
                    parray = {
                        New String() {"@CellName", Chr(39) & cellName & Chr(39)},
                        New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString() & Chr(39)},
                        New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
                        New String() {"@Technology", Chr(39) & chartTech & Chr(39)},
                        New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex = 0, "NULL", Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39))}
                    }
                    sqlQueryData = GetSQL(7707, parray)
                    lblGrid2A.Text = "Grid- Changes of Selected Object : " & cellName
                Case ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked
                    parray = {
                            New String() {"@CellName", Chr(39) & cellName & Chr(39)},
                            New String() {"@ConfigDate", Chr(39) & configdate.ToString("yyyy-MM-dd HH:mm:ss") & Chr(39)},
                            New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString() & Chr(39)},
                            New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
                            New String() {"@Technology", Chr(39) & chartTech & Chr(39)},
                            New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex = 0, "NULL", Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39))}
                            }
                    sqlQueryData = GetSQL(7708, parray)
                    lblGrid2B.Text = "Grid- Changes of Selected Object : " & cellName & "; on " & configdate
                Case ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked
                    parray = {
                            New String() {"@CellName", Chr(39) & cellName & Chr(39)},
                            New String() {"@ConfigDate", Chr(39) & configdate.ToString("yyyy-MM-dd HH:mm:ss") & Chr(39)},
                            New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString() & Chr(39)},
                            New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
                            New String() {"@Technology", Chr(39) & chartTech & Chr(39)},
                            New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex = 0, "NULL", Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39))}
                            }
                    sqlQueryData = GetSQL(7709, parray)
                    lblGrid2B.Text = "Grid- Changes of Selected Object : " & cellName & "; on " & configdate
            End Select

            If Not (sqlQueryData Is Nothing) Then
                dtChartAndGridData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(sqlQueryData(0), sqlQueryData(1))
                If Not (dtChartAndGridData Is Nothing) Then
                    'RemoveHandler gridCtrl.MouseDown, AddressOf gridCtrl_MouseDown
                    'AddHandler gridCtrl.MouseDown, AddressOf gridCtrl_MouseDown
                    IOSDevExpressGrid.PopulateDataInGrid(gridCtrl, gridView, dtChartAndGridData, "ALL")
                End If
            Else
                lblMsg.Text = "Chart 3 Grid Not Filled"
                lblMsg.Visible = False
            End If
        Catch ex As Exception
            frmMapWindow.SetStatus(ex.Message)
        End Try
    End Sub

#End Region

#Region "Chart's Event"

    Private Sub Chart_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chart_1.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If (isChartRightClick) Then
                isChartRightClick = False
                Exit Sub
            End If

            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name)
            Dim hit As HitTestInfo = Nothing
            Try
                hit = Chart_1.HitTest()
            Catch ex As Exception
            End Try

            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                Dim techName As String = hit.Series.Name
                Dim xValue As DateTime = el.XDateTime
                Dim yValue As Double = el.YValue
                chartTech = techName
                timeStampChart1 = xValue
                CreateChart2A(xValue, techName, Chart_2A)
                CreateChart2B(xValue, techName, Chart_2B)

                Chart_3.ClearAll()
                vdgvChart2A.DataSource = Nothing
                gvChart2A.Columns.Clear()
                lblGrid2A.Text = ""
                vdgvChart3.DataSource = Nothing
                gvChart3.Columns.Clear()
                lblGrid2B.Text = ""
            End If
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Exit Chart_1_Click")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub Chart_2A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chart_2A.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If (isChartRightClick) Then
                isChartRightClick = False
                Exit Sub
            End If
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Start Chart_2A_Click")
            Dim hit As HitTestInfo = Nothing
            Try
                hit = Chart_2A.HitTest()
            Catch ex As Exception
            End Try

            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                Dim columnName As String = hit.Series.Name
                Dim xValue As String = el.Name
                Dim yValue As Double = el.YValue

                cellOrParameterClickOnChart2aOrChart2b = xValue
                CreateChart3(xValue, Chart_3, ParameterHistoryChart.Chart3_2A_Clicked)
                BindChartGrid(vdgvChart2A, gvChart2A, xValue, chartTech, ParameterHistoryChart.GridChart2A)
                vdgvChart3.DataSource = Nothing
                gvChart3.Columns.Clear()
            End If
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Exit Chart_2A_Click")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub Chart_2B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chart_2B.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If (isChartRightClick) Then
                isChartRightClick = False
                Exit Sub
            End If
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Start Chart_2B_Click")
            Dim hit As HitTestInfo = Nothing
            Try
                hit = Chart_2B.HitTest()
            Catch ex As Exception
            End Try

            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                Dim columnName As String = hit.Series.Name
                ParameterFilterName = el.Name
                Dim xValue As String = el.Name
                Dim yValue As Double = el.YValue
                cellOrParameterClickOnChart2aOrChart2b = xValue
                CreateChart3(xValue, Chart_3, ParameterHistoryChart.Chart3_2B_Clicked)
                vdgvChart3.DataSource = Nothing
                gvChart3.Columns.Clear()
            End If
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Exit Chart_2B_Click")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub Chart_3_Click(sender As System.Object, e As System.EventArgs) Handles Chart_3.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If (isChartRightClick) Then
                isChartRightClick = False
                Exit Sub
            End If
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Start Chart_3_Click")
            Dim hit As HitTestInfo = Nothing
            Try
                hit = Chart_3.HitTest()
            Catch ex As Exception
            End Try

            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                Dim columnName As String = hit.Series.Name
                Dim xValue As DateTime = el.XDateTime
                Dim yValue As Double = el.YValue
                Dim gridCreatedOnChart2Click As ParameterHistoryChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked
                If (Chart_3.Tag = ParameterHistoryChart.Chart3_2B_Clicked) Then
                    gridCreatedOnChart2Click = ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked
                End If
                BindChartGrid(vdgvChart3, gvChart3, cellOrParameterClickOnChart2aOrChart2b, chartTech, gridCreatedOnChart2Click, xValue)
            End If
            _logger.SetInfo(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Exit Chart_3_Click")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub Chart_2A_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart_2A.MouseDown
        Try
            If (e.Button = MouseButtons.Right) Then
                Dim hit As HitTestInfo = Nothing
                Try
                    hit = Chart_2A.HitTest()
                Catch ex As Exception
                End Try

                If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                    Dim el As Element = CType(hit.Object, Element)
                    Dim columnName As String = hit.Series.Name
                    Dim xValue As String = el.Name
                    Dim yValue As Double = el.YValue
                    cellIdChart2a = xValue
                End If
                isChartRightClick = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Chart_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart_1.MouseDown, Chart_3.MouseDown, Chart_2B.MouseDown
        Try
            isChartRightClick = False
            If (e.Button = MouseButtons.Right) Then
                isChartRightClick = True
            End If
            Dim hit As HitTestInfo = Nothing
            Dim chart1 As dotnetCHARTING.WinForms.Chart = TryCast(sender, dotnetCHARTING.WinForms.Chart)
            If (chart1 IsNot Nothing) Then
                Try
                    hit = chart1.HitTest()
                Catch ex As Exception
                End Try

                If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                    Dim el As Element = CType(hit.Object, Element)
                    ParameterFilterName = el.Name
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Form & Left Control's Event"

    Private Sub frmParameterHistory_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Chart_1.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            Chart_2A.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            Chart_2B.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            Chart_3.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"

            lblMsg.Text = ""
            If (dt_IOS_ObjectConfig IsNot Nothing) Then
                Dim dtVendorPH As DataTable = Nothing
                dtVendorPH = New DataView(dt_IOS_ObjectConfig, "ParamHistory=1", "", DataViewRowState.CurrentRows).ToTable(True, "Vendor")
                BindDevExComboBoxWithValueMember(cmbVendor, dtVendorPH, "Vendor", "Vendor", "Select Vendor")
            End If

            clearComboBox(cmbTechnology, "Select Technology")
            clearComboBox(cmbTargetObject, "Select Object")

            tmpPHEndDate.EditValue = Now()
            tmpPHStartDate.EditValue = DateSerial(Now().Year, Now().Month, Now.Day - 60)

            ConfigurParameterHistoryForm(Me.Name)
            SetParameterHistoryTableWidthOnPanelWidthChange(True)
            sccChart_1.Collapsed = True
            AddHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVendor.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If cmbVendor.SelectedIndex > 0 Then
                BindChart1()

                If (dt_IOS_ObjectConfig IsNot Nothing) Then
                    Dim dtTechPH As DataTable = Nothing
                    dtTechPH = New DataView(dt_IOS_ObjectConfig, " Vendor='" & cmbVendor.SelectedItem.ToString & "' and ParamHistory=1", "", DataViewRowState.CurrentRows).ToTable(True, "Technology")
                    BindDevExComboBoxWithValueMember(cmbTechnology, dtTechPH, "Technology", "Technology", "Select Technology")
                End If
            Else
                Chart_1.ClearAll()
                Chart_2A.ClearAll()
                Chart_2B.ClearAll()
                Chart_3.ClearAll()
                vdgvChart2A.DataSource = Nothing
                gvChart2A.Columns.Clear()
                lblGrid2A.Text = ""
                vdgvChart3.DataSource = Nothing
                gvChart3.Columns.Clear()
                lblGrid2B.Text = ""
                dtChart_1 = Nothing
                clearComboBox(cmbTechnology, "Select Technology")
            End If
            clearComboBox(cmbTargetObject, "Select Object")
            tvObjectTree.Nodes.Clear()
            tvObjectTree.Columns.Clear()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If (cmbTechnology.SelectedIndex > 0) Then
                lblMsg.Text = ""

                If (dt_IOS_ObjectConfig IsNot Nothing) Then
                    Dim dtobject As DataTable = Nothing
                    dtobject = New DataView(dt_IOS_ObjectConfig, "Vendor='" & cmbVendor.SelectedItem.ToString & "' and  Technology='" & cmbTechnology.SelectedItem.ToString & "' and ParamHistory=1", "", DataViewRowState.CurrentRows).ToTable(True, "Object")
                    BindDevExComboBoxWithValueMember(cmbTargetObject, dtobject, "Object", "Object", "Select Object")
                End If

                Dim pmDataMain As DataTable = GetTemplateManagerData(cmbTechnology.SelectedItem.ToString.Trim, cmbVendor.SelectedItem.ToString.Trim(), "EnabledInTemplate")
                Dim pmData As DataTable = GetDistinctFilterData(parameterFilterColumnName, pmDataMain)
                If (pmData IsNot Nothing) Then
                    BindParameterToList(lstParameterFilter, pmData)
                End If
                dtparameterFileter = pmData
            Else
                clearComboBox(cmbTargetObject, "Select Object")
            End If
            tvObjectTree.Nodes.Clear()
            tvObjectTree.Columns.Clear()
            BindChart1()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbTargetObject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTargetObject.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If cmbTargetObject.SelectedIndex > 0 Then
                tvObjectTree.SuspendLayout()
                tvObjectTree.Nodes.Clear()

                Dim imgListObject As New ImageList
                tvObjectTree.SelectImageList = Nothing
                clsIOSImageList.SetImages(imgListObject, cmbTechnology.SelectedItem.ToString())
                tvObjectTree.SelectImageList = imgListObject

                'Dim roottn As TreeNode = New TreeNode()
                'roottn.Text = "PLMN"
                'roottn.ImageKey = "EMPTY"
                'roottn.SelectedImageKey = "EMPTY"
                'tvObjectTree.Nodes.Clear()
                'tvObjectTree.Nodes.Add(roottn)
                'Dim tNode As New TreeNode
                'tNode = tvObjectTree.Nodes(0)

                Dim parray()() As String = {
                    New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)},
                    New String() {"@Technology", Chr(39) & cmbTechnology.SelectedItem.ToString() & Chr(39)},
                    New String() {"@ObjectType", Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39)}
                }

                Dim sqlAndConnStr() As String = GetSQL(7710, parray, dt_IOS_SQL)
                Dim dsObjectTree As New DataSet
                dsObjectTree = IOS.DataLibrary.DataAccessorODBC.GetDataSet(sqlAndConnStr(0), sqlAndConnStr(1))
                'PopulateObjectTree(roottn.Text, tNode, dsObjectTree, cmbTechnology.SelectedItem.ToString())
                Dim tech As String = cmbVendor.SelectedItem.ToString & " " & cmbTechnology.SelectedItem.ToString
                FillTreeList(tvObjectTree, dsObjectTree, cmbTargetObject.SelectedItem.ToString, "PLMN", tech)
                'tNode.Expand()
                'tNode = Nothing

                tvObjectTree.Refresh()
                tvObjectTree.ResumeLayout()
                BindChart1()

            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Sub FillTreeList(ByRef tl As TreeList, ds As DataSet, objectType As String, rNode As String, tech As String, Optional filterObject As String = Nothing, Optional ObjectTreeEnabled As Boolean = True)
        Try
            tl.Cursor = Cursors.WaitCursor
            tl.BeginUnboundLoad()
            Application.DoEvents()

            tl.PupulateTreeListColumn({"ObjectID", "ParentID", "ObjectName", "ObjectType", "Band", "ImageIndex"})

            tl.Nodes.Clear()
            Dim tlNode As TreeListNode = tl.Nodes.Add(New Object() {rNode, "0", rNode, "EMPTY", "-1", 1})

            If ObjectTreeEnabled Then
                Select Case objectType.ToLower
                    Case "tags"
                        Dim ds_tag As New DataSet
                        Try
                            'Dim parray()() As String = {New String() {"@Tech", Chr(39) & _strNetwork & Chr(39)}}
                            'Dim sqlAndConnectionStr() As String = GetSQL(IOSSqlIds.TAGS_OBJECT_TREE, parray, dt_IOS_SQL)
                            'ds_tag = DataAccessorODBC.GetDataSet(sqlAndConnectionStr(0), sqlAndConnectionStr(1))
                            'tl.PopulateTreeList("PLMN", tlNode, ds_tag, "0")
                        Catch ex As Exception
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                        Finally
                            If ds_tag IsNot Nothing Then
                                ds_tag.Dispose()
                                ds_tag = Nothing
                            End If
                        End Try
                    Case Else
                        Dim tblname As String = Nothing
                        tblname = "dsTree" & "_" & objectType
                        ds.Tables(0).TableName = tblname
                        tl.PopulateTreeList(rNode, tlNode, ds, tblname, filterObject, objectType, tech)
                End Select
            End If
        Catch ex As Exception
        Finally
            tl.EndUnboundLoad()
            If filterObject Is Nothing Then
                If tl.Nodes.Count > 0 Then
                    tl.SelectNode(tl.Nodes(0))
                    tl.SetFocusedNode(tl.Nodes(0))
                    tl.CollapseAll()
                    tl.ExpandToLevel(0)
                End If
            Else
                tl.SetColumnWidth()
            End If
            tl.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    'Private Sub txtSearchOuter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If txtSearchOuter.Text.Length >= 3 Then
    '            txtObjectsearch_KeyDown(tvObjectTree1, txtSearchOuter.Text, e)
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub txtSearchOuter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If txtSearchOuter.Text.Length >= 3 Then
    '            txtObjectSearch_TextChanged(tvObjectTree1, txtSearchOuter.Text)
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub btnRefreshChart_Click(sender As Object, e As EventArgs) Handles btnRefreshChart.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            lblMsg.Text = ""
            lblMsg.Visible = False
            BindChart1()
        Catch ex As Exception
            frmMapWindow.SetStatus(ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub txtSearchPH_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchPH.KeyUp
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If (dtparameterFileter IsNot Nothing) Then
                If (txtSearchPH.Text.Length > 2) Then
                    Dim rows = dtparameterFileter.AsEnumerable().Where(Function(w) w.Field(Of String)(parameterFilterColumnName).StartsWith(txtSearchPH.Text, StringComparison.OrdinalIgnoreCase))
                    If (rows.Count() > 0) Then
                        BindParameterToList(lstParameterFilter, rows.CopyToDataTable())
                    Else
                        lstParameterFilter.Items.Clear()
                    End If
                Else
                    BindParameterToList(lstParameterFilter, dtparameterFileter)
                End If
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Parameter Filter"

    Private Sub lstParameterFilter_MouseMove(sender As Object, e As MouseEventArgs) Handles lstParameterFilter.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                If (p <> Point.Empty) Then
                    Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
                    If (listControl IsNot Nothing) Then
                        Dim index As Integer = listControl.IndexFromPoint(p)
                        If (index > -1) Then
                            listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lstParameterFilter_MouseDown(sender As Object, e As MouseEventArgs) Handles lstParameterFilter.MouseDown
        Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
        p = New Point(e.X, e.Y)
        Dim selectedIndex As Integer = listControl.IndexFromPoint(p)
        If selectedIndex = -1 Then
            p = Point.Empty
        End If
    End Sub

    Private Sub txtParamFilterInclude_DragDrop(sender As Object, e As DragEventArgs) Handles txtParamFilterInclude.DragDrop
        Try
            Dim text As String = e.Data.GetData("System.String")
            If (lstParameterFilter.Items.Contains(text)) Then
                Dim IsNewTable As Boolean = True
                If String.IsNullOrEmpty(txtParamFilterInclude.Text.Trim) Then
                    txtParamFilterInclude.Text = text
                Else
                    Dim exitingTables() As String = txtParamFilterInclude.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    If Not (exitingTables.Contains(text)) Then
                        txtParamFilterInclude.Text += "," & text
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtParamFilterExclude_DragDrop(sender As Object, e As DragEventArgs) Handles txtParamFilterExclude.DragDrop
        Try
            Dim text As String = e.Data.GetData("System.String")
            If (lstParameterFilter.Items.Contains(text)) Then
                Dim IsNewTable As Boolean = True
                If String.IsNullOrEmpty(txtParamFilterExclude.Text.Trim) Then
                    txtParamFilterExclude.Text = text
                Else
                    Dim exitingTables() As String = txtParamFilterExclude.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    If Not (exitingTables.Contains(text)) Then
                        txtParamFilterExclude.Text += "," & text
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtParameterFilter_DragOver(sender As Object, e As DragEventArgs) Handles txtParamFilterInclude.DragOver, txtParamFilterExclude.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

#End Region

#Region "Parameter Filter Context Menu"

    Private Sub cm_ParameterFilter_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cm_ParameterFilter.Opening
        Try
            cm_ParameterFilter.Items(0).Enabled = False
            Dim copyedText As String = Clipboard.GetText()
            If (copyedText = String.Empty) Then
                cm_ParameterFilter.Items(1).Enabled = False
                cm_ParameterFilter.Items(1).Text = "Paste"
            Else
                cm_ParameterFilter.Items(1).Enabled = True
                cm_ParameterFilter.Items(1).Text = "Paste (" & copyedText.Split(ControlChars.NewLine).Length & ")"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_PH_Copy_Click(sender As Object, e As EventArgs) Handles tsmi_PH_Copy.Click
        Clipboard.Clear()
        Dim outputstr As New System.Text.StringBuilder()
        Try
            'Dim copystring As String = DropDown_Checked2String(vcmParFilter)
            'copystring = copystring.Replace(",", ControlChars.NewLine)
            'If Not copystring Is Nothing Or copystring <> "" Then
            '    Clipboard.SetText(copystring)
            'End If
            'copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
        End Try
    End Sub

    Private Sub tsmi_PH_Paste_Click(sender As Object, e As EventArgs) Handles tsmi_PH_Paste.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim vcmParFilter As DevExpress.XtraEditors.ListBoxControl = lstParameterFilter
            Dim copyedText As String = Clipboard.GetText()                   'Get clipboard data as a string
            Dim rows() As String = copyedText.Split(ControlChars.NewLine)    'Split into rows
            Dim i As Integer
            Dim clipboardmatches As Integer = 0
            Dim mbresult As MsgBoxResult = MsgBoxResult.Ok

            If copyedText.Split(ControlChars.Tab).Length * copyedText.Split(ControlChars.NewLine).Length > 100 Then
                mbresult = MsgBox("An estimated " & copyedText.Split(ControlChars.Tab).Length * copyedText.Split(ControlChars.NewLine).Length & " strings on clipboard are detected. Selection can take long. Do you wish to continue selection?", MsgBoxStyle.OkCancel)
            End If

            If mbresult = MsgBoxResult.Ok Then
                For i = 0 To rows.Length - 1
                    If (CheckItemIsExist(rows(i), vcmParFilter) = False) Then
                        vcmParFilter.Items.Add(rows(i).ToString.Trim)
                        vcmParFilter.SetSelected(vcmParFilter.Items.IndexOf(rows(i).ToString.Trim), True)
                    End If
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_TopXMapType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tsmi_TopXMapType.SelectedIndexChanged
        Dim cmb As ToolStripComboBox = CType(sender, ToolStripComboBox)
        TopXMapType = cmb.SelectedIndex
    End Sub

#End Region

#Region "Object Tree Context Menu"

    Private Sub cm_ObjectTree_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cm_ObjectTree.Opening
        Try
            tvObjectTree.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tech As String = Me.GetTechnologyName(cmbTechnology.SelectedItem.ToString, cmbVendor.SelectedItem.ToString, "Tech")
            Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
            Dim countchecked As Integer = 0
            Try
                Dim ExactMatch As Boolean = True
                If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                    ExactMatch = False
                Else
                    ExactMatch = True
                End If

                'count checked boxes
                countchecked = tvObjectTree.GetEndCheckedNodes().Count    'TreeView_CountCheckedNodes(tvObjectTree1.Nodes(0))
                tsmi_OT_Exception.Visible = False

                'enable/disable copy
                If countchecked > 0 Then
                    cm_OT_tsmi_copy.Text = "Copy - Objects: " & countchecked
                    cm_OT_tsmi_copy.Enabled = True
                Else
                    cm_OT_tsmi_copy.Text = "Copy"
                    cm_OT_tsmi_copy.Enabled = False
                End If

                'check clipboard
                Dim s As String = Clipboard.GetText()                  'Get clipboard data as a string
                Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
                Dim i, j As Integer
                If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                    cm_OT_tsmi_paste.Text = "Paste - Objects: ?"
                    cm_OT_tsmi_paste.Enabled = True
                Else

                    Dim clipboardmatches As Integer = 0
                    For i = 0 To rows.Length - 1
                        'Split row into cells
                        Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                        For j = 0 To bufferCell.Length - 1
                            If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                                bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                            End If
                            If bufferCell(j).Trim <> "" Then
                                If Not Treelist_TextSearch(bufferCell(j).Trim, tvObjectTree.Nodes, ExactMatch, "ObjectName") Is Nothing Then
                                    clipboardmatches = clipboardmatches + 1
                                End If
                            End If
                        Next
                    Next

                    'enable/disable paste
                    If clipboardmatches > 0 Then
                        cm_OT_tsmi_paste.Text = "Paste - Objects: " & clipboardmatches
                        cm_OT_tsmi_paste.Enabled = True
                    Else
                        cm_OT_tsmi_paste.Text = "Paste"
                        cm_OT_tsmi_paste.Enabled = False
                    End If
                    tvObjectTree.Cursor = Cursors.Arrow
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            End Try

            'get all tags
            Dim sql As String = Nothing
            Dim connstring As String = Nothing
            Dim ds_tag As DataSet = Nothing
            cm_OT_tsmi_CopyToTag.Enabled = False

            Try
                sql = GetSQL(8601, Nothing)(1)
                connstring = GetSQL(8601, Nothing)(0)
                ds_tag = IOS.DataLibrary.DataAccessorODBC.GetDataSet(connstring, sql)

                For Each drow As DataRow In ds_tag.Tables(0).Rows
                    Dim tsmi As ToolStripMenuItem = New ToolStripMenuItem(drow(1).ToString.Trim)

                    AddHandler tsmi.Click, AddressOf cm_OT_CopyToTag_ItemClick
                    cm_OT_tsmi_CopyToTag.DropDownItems.Add(tsmi)

                Next
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                ds_tag.Dispose()
                ds_tag = Nothing
            End Try

            'exception list
            If tvObjectTree.Name = "TreeView_Tuning_Objects" And countchecked > 0 Then
                tsmi_OT_Exception.Visible = True
            End If

            cm_OT_tsmi_CheckChild.Enabled = False
            cm_OT_tsmi_UnCheck.Enabled = False

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tvObjectTree.Cursor = Cursors.Arrow
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cm_OT_CopyToTag_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cm_OT_tsmi_copy_Click(sender As Object, e As EventArgs) Handles cm_OT_tsmi_copy.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Clipboard.Clear()

        Dim tech As String = Me.GetTechnologyName(cmbTechnology.SelectedItem.ToString, cmbVendor.SelectedItem.ToString, "Tech")
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString
        Try
            Dim copystring As String = TreeList_Checked2String(tech, aggr_to, "Naked", tvObjectTree, cmbTargetObject, "ObjectName")
            copystring = copystring.Replace(",", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tvObjectTree.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub cm_OT_tsmi_paste_Click(sender As Object, e As EventArgs) Handles cm_OT_tsmi_paste.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbTargetObject.SelectedItem.ToString

        tvObjectTree.Cursor = Cursors.WaitCursor
        Try
            Dim ExactMatch As Boolean = True
            If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                ExactMatch = False
            Else
                ExactMatch = True
            End If

            Dim s As String = Clipboard.GetText()                   'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim i, j As Integer
            Dim clipboardmatches As Integer = 0
            Dim mbresult As MsgBoxResult = MsgBoxResult.Ok

            If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                mbresult = MsgBox("An estimated " & s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length & " strings on clipboard are detected. Selection can take long. Do you wish to continue selection?", MsgBoxStyle.OkCancel)
            End If

            If mbresult = MsgBoxResult.Ok Then
                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                    For j = 0 To bufferCell.Length - 1
                        If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                            bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                        End If
                        Dim tv_result As TreeListNode = Treelist_TextSearch(bufferCell(j).Trim, tvObjectTree.Nodes, ExactMatch, "ObjectName")
                        If Not tv_result Is Nothing Then
                            tv_result.Checked = True
                        End If
                    Next
                Next
            End If

            tvObjectTree.Cursor = Cursors.Arrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            tvObjectTree.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_OT_tsmi_CheckChilds_Click(sender As Object, e As EventArgs) Handles cm_OT_tsmi_CheckChild.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            DevEx_ObjectTree_CheckChild(tvObjectTree.FocusedNode)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_OT_tsmi_UnCheck_Click(sender As Object, e As EventArgs) Handles cm_OT_tsmi_UnCheck.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            DevEx_TreeView_ClearChecks(tvObjectTree.FocusedNode)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_OT_MapCell_Click(sender As Object, e As EventArgs) Handles tsmi_OT_MapCell.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tech As String = ""
            tech = Me.GetTechnologyName(cmbTechnology.SelectedItem.ToString, cmbVendor.SelectedItem.ToString, "Tech")
            Select Case tech.ToUpper
                Case cmbVendor.SelectedItem.ToString & " " & "3G"
                    If cmbTargetObject.SelectedItem.ToString = "WCEL" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "WCEL", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "WBTS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "WBTS", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    End If
                Case cmbVendor.SelectedItem.ToString & " " & "2G"
                    If cmbTargetObject.SelectedItem.ToString = "CELL" Or cmbTargetObject.SelectedItem.ToString = "BTS" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "CELL", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "BCF" Or cmbTargetObject.SelectedItem.ToString = "SITE" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "BCF", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    End If
                Case cmbVendor.SelectedItem.ToString & " " & "4G"
                    If cmbTargetObject.SelectedItem.ToString = "LCELL" Or cmbTargetObject.SelectedItem.ToString = "ENODEB" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "LCELL", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "ENODEB" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "ENODEB", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "EUTRANCELL" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "EUTRANCELL", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    End If
                Case cmbVendor.SelectedItem.ToString & " " & "5G"
                    If cmbTargetObject.SelectedItem.ToString = "NRCELL" Or cmbTargetObject.SelectedItem.ToString = "GNODEB" Or cmbTargetObject.SelectedItem.ToString = "TAGS" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "NRCELL", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    ElseIf cmbTargetObject.SelectedItem.ToString = "GNODEB" Then
                        frmMapWindow.Cells_SearchAndDisplay(TreeList_Checked2String(tech, "GNODEB", "Naked", tvObjectTree, cmbTargetObject, "ObjectName"), tech, Nothing, True)
                    End If
            End Select
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ReloadTree_Click(sender As Object, e As EventArgs) Handles tsmi_ReloadTree.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        cmbTargetObject_SelectedIndexChanged(Nothing, Nothing)
        Try
            frmMapWindow.Calendar_GetNetworks_Fill()
            frmMapWindow.Calendar_GetNetwork_Fill_From_DB()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Chart 1 Context Menu"

    Private Sub tsmiShowGridChart_1_Click(sender As Object, e As EventArgs) Handles tsmiShowGridChart_1.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If tsmiShowGridChart_1.Text = "Show Grid" Then
                tsmiShowGridChart_1.Text = "Hide Grid"
                sccChart_1.Collapsed = False
                IOSDevExpressGrid.PopulateDataInGrid(gcChart_1, gvChart_1, dtChart_1, "ALL")
            Else
                tsmiShowGridChart_1.Text = "Show Grid"
                sccChart_1.Collapsed = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub


#End Region

#Region "Chart 2A Context Menu"

    Private Sub Chart2a_ContextMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Chart2a_ContextMenu.Opening
        Try
            If (dtChart_2A IsNot Nothing) Then
                tsmi_ObjectCount.Text = "Total Object : " & dtChart_2A.Rows.Count
                tsmi_TopXMapType.Items.Clear()
                AddThemeType(tsmi_TopXMapType)
                tsmi_TopXMapType.SelectedIndex = TopXMapType
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_sendconsoletree_Click(sender As Object, e As EventArgs) Handles tsmi_sendconsoletree.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (dtChart_2A IsNot Nothing AndAlso techChart2a IsNot Nothing AndAlso cellIdChart2a IsNot Nothing) Then
                Dim rows() As DataRow = dtChart_2A.Select("Cellid='" & cellIdChart2a & "'")
                If (rows.Count > 0) Then
                    SendToConsoleTree(techChart2a, rows.CopyToDataTable().DefaultView.ToTable(True, "Cellid").AsEnumerable().ToArray())
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_sendAlltoConsoletee_Click(sender As Object, e As EventArgs) Handles tsmi_sendAlltoConsoletee.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (dtChart_2A IsNot Nothing AndAlso techChart2a IsNot Nothing) Then
                SendToConsoleTree(techChart2a, dtChart_2A.DefaultView.ToTable(True, "Cellid").AsEnumerable().ToArray())
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_Topx_MapSel_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_SendToMap.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (dtChart_2A IsNot Nothing AndAlso techChart2a IsNot Nothing) Then
                SendToMap(dtChart_2A.AsEnumerable().ToArray())
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SendSelectedToMap_Click(sender As Object, e As EventArgs) Handles tsmi_SendSelectedToMap.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (Me.dtChart_2A IsNot Nothing AndAlso Me.techChart2a IsNot Nothing AndAlso Me.cellIdChart2a IsNot Nothing) Then
                Dim rows() As DataRow = dtChart_2A.Select("Cellid='" & cellIdChart2a & "'")
                If (rows.Count > 0) Then
                    Me.SendToMap(rows)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiShowGrid_Chart_2A_Click(sender As Object, e As EventArgs) Handles tsmiShowGrid_Chart_2A.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If tsmiShowGrid_Chart_2A.Text = "Show Grid" Then
                tsmiShowGrid_Chart_2A.Text = "Hide Grid"
                sccChart_2A.Collapsed = False
                IOSDevExpressGrid.PopulateDataInGrid(gcChart_2A, gvChart_2A, DirectCast(Chart_2A.Series.Data, DataTable), "ALL")
            Else
                tsmiShowGrid_Chart_2A.Text = "Show Grid"
                sccChart_2A.Collapsed = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Chart 2B Context Menu"

    Private Sub tsmi_AddParameterFilter_Click(sender As Object, e As EventArgs) Handles tsmi_AddParameterFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (ParameterFilterName IsNot Nothing) Then
                If Not (lstParameterFilter.Items.Contains(ParameterFilterName)) Then
                    If (dtparameterFileter IsNot Nothing) Then
                        Dim rows = dtparameterFileter.AsEnumerable().Where(Function(w) w.Field(Of String)(parameterFilterColumnName).StartsWith(txtSearchPH.Text, StringComparison.OrdinalIgnoreCase))
                        If Not (rows.Count() > 0) Then
                            txtParamFilterInclude.Text += "," & ParameterFilterName
                        Else
                            dtparameterFileter.Rows.Add(ParameterFilterName)
                            lstParameterFilter.Items.Clear()
                            BindParameterToList(lstParameterFilter, dtparameterFileter)
                        End If
                    End If
                End If
                If (lstParameterFilter.Items.Contains(ParameterFilterName)) Then
                    Dim IsNewTable As Boolean = True
                    If String.IsNullOrEmpty(txtParamFilterInclude.Text.Trim) Then
                        txtParamFilterInclude.Text = ParameterFilterName
                    Else
                        Dim exitingTables() As String = txtParamFilterInclude.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        If Not (exitingTables.Contains(ParameterFilterName)) Then
                            txtParamFilterInclude.Text += "," & ParameterFilterName
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SendObjectToTree_Click(sender As Object, e As EventArgs) Handles tsmi_SendObjectToTree.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim startDate As String = timeStampChart1.ToString("yyyy-MM-dd HH:mm:ss")
            Dim endDate As String = timeStampChart1.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
            Dim data As DataTable = Nothing

            Dim parray()() As String = {
                New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString() & Chr(39)},
                New String() {"@ConfigStartDate", Chr(39) & startDate & Chr(39)},
                New String() {"@ConfigEndDate", Chr(39) & endDate & Chr(39)},
                New String() {"@CellName", Chr(39) & ParameterFilterName & Chr(39)},
                New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex = 0, "NULL", Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39))},
                New String() {"@Technology", Chr(39) & techChart2a & Chr(39)},
                New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)}
            }

            Dim sqlQueryData() As String = GetSQL(7706, parray)
            If Not (sqlQueryData Is Nothing) Then
                data = IOS.DataLibrary.DataAccessorODBC.GetDataTable(sqlQueryData(0), sqlQueryData(1))
                If (data.Rows.Count > 0) Then
                    SendToConsoleTree(techChart2a, data.AsEnumerable().ToArray())
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SendObjectToMap_Click(sender As Object, e As EventArgs) Handles tsmi_SendObjectToMap.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim startDate As String = timeStampChart1.ToString("yyyy-MM-dd HH:mm:ss")
            Dim endDate As String = timeStampChart1.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
            Dim data As DataTable = Nothing
            Dim username As String = Environment.UserName
            Dim parray()() As String = {
                New String() {"@ParametersFilter", Chr(39) & Me.GetFilterString() & Chr(39)},
                New String() {"@ConfigStartDate", Chr(39) & startDate & Chr(39)},
                New String() {"@ConfigEndDate", Chr(39) & endDate & Chr(39)},
                New String() {"@CellName", Chr(39) & ParameterFilterName & Chr(39)},
                New String() {"@ObjectType", IIf(cmbTargetObject.SelectedIndex = 0, "NULL", Chr(39) & cmbTargetObject.SelectedItem.ToString() & Chr(39))},
                New String() {"@Technology", Chr(39) & techChart2a & Chr(39)},
                New String() {"@Vendor", Chr(39) & cmbVendor.SelectedItem.ToString() & Chr(39)}
            }

            Dim sqlQueryData() As String = GetSQL(7706, parray)
            If Not (sqlQueryData Is Nothing) Then
                data = IOS.DataLibrary.DataAccessorODBC.GetDataTable(sqlQueryData(0), sqlQueryData(1))
                If (data.Rows.Count > 0) Then
                    Dim colStattic As DataColumn = New System.Data.DataColumn("Counter") 'StatticValue
                    With colStattic
                        .DataType = System.Type.GetType("System.Int32")
                        .DefaultValue = 5
                    End With

                    data.Columns.Add(colStattic)
                    SendToMap(data.AsEnumerable().ToArray())
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiShowGrid_Chart_2B_Click(sender As Object, e As EventArgs) Handles tsmiShowGrid_Chart_2B.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If tsmiShowGrid_Chart_2B.Text = "Show Grid" Then
                tsmiShowGrid_Chart_2B.Text = "Hide Grid"
                sccChart_2B.Collapsed = False
                IOSDevExpressGrid.PopulateDataInGrid(gcChart_2B, gvChart_2B, DirectCast(Chart_2B.Series.Data, DataTable), "ALL")
            Else
                tsmiShowGrid_Chart_2B.Text = "Show Grid"
                sccChart_2B.Collapsed = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Chart 3 Context Menu"

    Private Sub tsmiShowGrid_Chart_3_Click(sender As Object, e As EventArgs) Handles tsmiShowGrid_Chart_3.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If tsmiShowGrid_Chart_3.Text = "Show Grid" Then
                tsmiShowGrid_Chart_3.Text = "Hide Grid"
                sccChart_3.Collapsed = False
                IOSDevExpressGrid.PopulateDataInGrid(gcChart_3, gvChart_3, DirectCast(Chart_3.Series.Data, DataTable), "ALL")
            Else
                tsmiShowGrid_Chart_3.Text = "Show Grid"
                sccChart_3.Collapsed = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "GridView Context Menu"

    Private Sub cm_GridViewMap_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cm_GridViewMap.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            Dim mapGridTmp As DevExpress.XtraGrid.GridControl = Nothing
            mapGridTmp = TryCast(cmsTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            tsmi_RecordCount.Text = "Record Count: " & mapGridTmp.DefaultView.RowCount.ToString
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_dgv_SelectAll_Click(sender As Object, e As EventArgs) Handles tsmi_dgv_SelectAll.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim grvTemp As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.DefaultView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_dgv_CopyClipboard_Click(sender As Object, e As EventArgs) Handles tsmi_dgv_CopyClipboard.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.DefaultView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_dgv_ExportExcel_Click(sender As Object, e As EventArgs) Handles tsmi_dgv_ExportExcel.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            IOSDevExpressGrid.ExportDataGridToExcel(tempGrid)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "ObjectTree Events"

    Private Sub tvObjectTree_FilterCheckVisible(ByRef nds As TreeListNodes)
        For Each nd As TreeListNode In nds
            If nd.HasChildren Then
                tvObjectTree_FilterCheckVisible(nd.Nodes)
            End If
            If nd.Visible = True Then
                nd.Checked = True
            Else
                nd.Checked = False
            End If
        Next
    End Sub

    Private Sub tvObjectTree_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        RemoveHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
        If e.ChangeType = DevExpress.XtraTreeList.NodeChangeTypeEnum.CheckedState Then
            If e.Node.CheckState = CheckState.Checked Then
                If tvObjectTree.FindFilterText <> "" Then
                    tvObjectTree_FilterCheckVisible(e.Node.Nodes)
                Else
                    e.Node.CheckAll()
                End If
            Else
                e.Node.UncheckAll()
            End If

            tvObjectTree.CheckParentNode(e.Node)
            Dim Count_Checked As Integer = tvObjectTree.GetEndCheckedNodes().Count
        End If
        AddHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
    End Sub

    'Private Sub tvObjectTree_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvObjectTree1.AfterCheck
    '    Try
    '        CheckTreeNodeAndCount(e.Node, 0, Nothing)
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub tvObjectTree_DragOver(sender As Object, e As DragEventArgs) Handles tvObjectTree.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub tvObjectTree_MouseDown(sender As Object, e As MouseEventArgs) Handles tvObjectTree.MouseDown
        Try
            Dim tl As TreeList = TryCast(sender, TreeList)
            If (tl IsNot Nothing) Then
                Dim item As TreeListHitInfo = tl.CalcHitInfo(e.Location)
                If item.Node IsNot Nothing Then
                    If (e.Button = MouseButtons.Left) Then
                        tl.DoDragDrop(item.Node, DragDropEffects.Copy)
                    Else
                        tl.FocusedNode = item.Node
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Commented Code"

    'Private Function GetSQLIDByTechnogloy(ByVal tech As String, ByVal phChart As ParameterHistoryChart) As Integer
    '    Dim resultSQLID As Integer = 0
    '    Dim vendor As String = cmbVendor.SelectedItem.ToString.Trim()
    '    Select Case tech
    '        Case "2G"
    '            If (vendor.ToUpper = "HUAWEI") Then
    '                If (phChart = ParameterHistoryChart.Chart2A) Then
    '                    resultSQLID = 20050
    '                ElseIf (phChart = ParameterHistoryChart.Chart2B) Then
    '                    resultSQLID = 20051
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2A_Clicked) Then
    '                    resultSQLID = 20052
    '                ElseIf (phChart = ParameterHistoryChart.GridChart2A) Then
    '                    resultSQLID = 20053
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2B_Clicked) Then
    '                    resultSQLID = 20054
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked) Then
    '                    resultSQLID = 20055
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked) Then
    '                    resultSQLID = 20056
    '                ElseIf (phChart = ParameterHistoryChart.Chart2BCellData) Then
    '                    resultSQLID = 20057
    '                Else
    '                    resultSQLID = 0
    '                End If
    '            ElseIf (vendor.ToUpper = "NSN") Or (vendor.ToUpper = "ERICSSON") Then
    '                If (phChart = ParameterHistoryChart.Chart2A) Then
    '                    resultSQLID = 7150
    '                ElseIf (phChart = ParameterHistoryChart.Chart2B) Then
    '                    resultSQLID = 7151
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2A_Clicked) Then
    '                    resultSQLID = 7152
    '                ElseIf (phChart = ParameterHistoryChart.GridChart2A) Then
    '                    resultSQLID = 7153
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2B_Clicked) Then
    '                    resultSQLID = 7154
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked) Then
    '                    resultSQLID = 7155
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked) Then
    '                    resultSQLID = 7156
    '                ElseIf (phChart = ParameterHistoryChart.Chart2BCellData) Then
    '                    resultSQLID = 7157
    '                Else
    '                    resultSQLID = 0
    '                End If
    '            End If

    '        Case "3G"
    '            If (vendor.ToUpper = "HUAWEI") Then
    '                If (phChart = ParameterHistoryChart.Chart2A) Then
    '                    resultSQLID = 30050
    '                ElseIf (phChart = ParameterHistoryChart.Chart2B) Then
    '                    resultSQLID = 30051
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2A_Clicked) Then
    '                    resultSQLID = 30052
    '                ElseIf (phChart = ParameterHistoryChart.GridChart2A) Then
    '                    resultSQLID = 30053
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2B_Clicked) Then
    '                    resultSQLID = 30054
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked) Then
    '                    resultSQLID = 30055
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked) Then
    '                    resultSQLID = 30056
    '                ElseIf (phChart = ParameterHistoryChart.Chart2BCellData) Then
    '                    resultSQLID = 30057
    '                Else
    '                    resultSQLID = 0
    '                End If
    '            ElseIf (vendor.ToUpper = "NSN") Or (vendor.ToUpper = "ERICSSON") Then
    '                If (phChart = ParameterHistoryChart.Chart2A) Then
    '                    resultSQLID = 7250
    '                ElseIf (phChart = ParameterHistoryChart.Chart2B) Then
    '                    resultSQLID = 7251
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2A_Clicked) Then
    '                    resultSQLID = 7252
    '                ElseIf (phChart = ParameterHistoryChart.GridChart2A) Then
    '                    resultSQLID = 7253
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2B_Clicked) Then
    '                    resultSQLID = 7254
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked) Then
    '                    resultSQLID = 7255
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked) Then
    '                    resultSQLID = 7256
    '                ElseIf (phChart = ParameterHistoryChart.Chart2BCellData) Then
    '                    resultSQLID = 7257
    '                Else
    '                    resultSQLID = 0
    '                End If

    '            End If
    '        Case "4G"
    '            If (vendor.ToUpper = "HUAWEI") Then
    '                If (phChart = ParameterHistoryChart.Chart2A) Then
    '                    resultSQLID = 10050
    '                ElseIf (phChart = ParameterHistoryChart.Chart2B) Then
    '                    resultSQLID = 10051
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2A_Clicked) Then
    '                    resultSQLID = 10052
    '                ElseIf (phChart = ParameterHistoryChart.GridChart2A) Then
    '                    resultSQLID = 10053
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2B_Clicked) Then
    '                    resultSQLID = 10054
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked) Then
    '                    resultSQLID = 10055
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked) Then
    '                    resultSQLID = 10056
    '                ElseIf (phChart = ParameterHistoryChart.Chart2BCellData) Then
    '                    resultSQLID = 10057
    '                Else
    '                    resultSQLID = 0
    '                End If
    '            ElseIf (vendor.ToUpper = "NSN") Or (vendor.ToUpper = "ERICSSON") Then
    '                If (phChart = ParameterHistoryChart.Chart2A) Then
    '                    resultSQLID = 7350
    '                ElseIf (phChart = ParameterHistoryChart.Chart2B) Then
    '                    resultSQLID = 7351
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2A_Clicked) Then
    '                    resultSQLID = 7352
    '                ElseIf (phChart = ParameterHistoryChart.GridChart2A) Then
    '                    resultSQLID = 7353
    '                ElseIf (phChart = ParameterHistoryChart.Chart3_2B_Clicked) Then
    '                    resultSQLID = 7354
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2bClicked) Then
    '                    resultSQLID = 7355
    '                ElseIf (phChart = ParameterHistoryChart.GridChart3_WithChart3OnChart2aClicked) Then
    '                    resultSQLID = 7356
    '                ElseIf (phChart = ParameterHistoryChart.Chart2BCellData) Then
    '                    resultSQLID = 7357
    '                Else
    '                    resultSQLID = 0
    '                End If
    '            End If
    '        Case Else
    '            resultSQLID = 0
    '    End Select
    '    Return resultSQLID
    'End Function

    'Private Function GetSQLIDByVendor(ByVal vendor As String) As Integer
    '    Dim resultSQLID As Integer = 0
    '    Select Case vendor.ToLower
    '        Case "huawei"
    '            resultSQLID = 40001
    '        Case "nsn"
    '            resultSQLID = 40002
    '        Case "ericsson"
    '            resultSQLID = 40002
    '        Case Else
    '            resultSQLID = 0
    '    End Select
    '    Return resultSQLID
    'End Function

#End Region

End Class