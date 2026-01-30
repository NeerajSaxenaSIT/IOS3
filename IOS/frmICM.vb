Imports DevExpress.XtraGrid.Views.Base.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports dotnetCHARTING.WinForms
Imports IOS.Configuration
Imports IOS.DataLibrary
Imports IOS.Library
Imports LidorSystems.IntegralUI.Lists
Imports MapInfo.Geometry
Imports DevExpress.XtraEditors

Public Class frmICM

#Region "Variables"

    Dim dtIOS_ICM As DataTable = Nothing
    Dim dtIOS_ICM_Config As DataTable = Nothing
    Dim dt_IOS_Template As DataTable = Nothing
    Dim dtIOS_ICM_Filters As DataTable = Nothing
    Dim cellName As String = String.Empty
    Dim subCategoryData As New Dictionary(Of String, DataTable)
    Dim TopXMapType As Integer = 0
    Dim cm_SourceControl As Control
    Dim cm_SourceControlSubCatagory As Control
    Dim cm_SourceControlOverView As Control
    Dim clickBy As EnumChartGridClick
    Public contextFlag As Boolean = True
    Dim IsFromSendToICM As Boolean = False
    Dim cellcolumnname As String = "CELLID"
    Dim dtTheamaticBins As DataTable = Nothing
    Dim MapToVoronoi As Boolean = False
    Dim MapToSite As Boolean = False
    ''' <summary>
    '''  TODO: Make id dynamic
    ''' </summary>
    ''' <remarks></remarks>
    Dim tech As String = "3G"
    Dim vendor As String = "Huawei"
    Dim isFirstTime As Boolean = True

#End Region

#Region "Helper Methods"

    Private Sub ConfigurICMForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                tsmi_MapAllWithThematic, tsmi_ShowHideOverviewForecast, tsmi_sendAlltoConsoletee, tsmi_sendconsoletree, tsmi_SendToMap, tsmi_EnableVoronoi, tsmi_SendToMapAllGraduatedTheme, tsmi_SendToMapAllRangedTheme, tsmi_UsingPieTheme,
                tsmi_SendToMapAllGeoAggregation, tsmi_SendToMapAllGeoAggregationFunction, CirclePresentationToolStripMenuItem, BufferPresentationToolStripMenuItem, tsmi_SendToMapAllHeatMap, tsmi_UsingPreconfigured, tsmi_SendToMapSelect,
                tsmi_HideAndShowGrid, tsmi_ShowHideForecastHistogramChart, tsmi_HideAndShowGridSubCategory, tsmi_ShowHideForecastSubCategoryChart
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

    Public Sub SetIsFromSendToICM(ByVal fromMap As Boolean)
        Me.IsFromSendToICM = fromMap
    End Sub

    Private Sub frmICM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.SuspendLayout()
            tsmi_SendToMapAllGeoAggregationFunction.SelectedIndex = 0
            tsmi_SendToMapSelectedGeoAggregationFunction.SelectedIndex = 0

            Fill_ICM_Data()
            BindDevExComboBoxWithValueMember(cmbReport, Me.GetIOS_ICM_Data().DefaultView.ToTable(True, "ReportName", "ReportDate"), "ReportName", "ReportName", "Select Report")
            BindTechnology()

            Filters_Initialize()

            txtXY.Text = 20

            ClearAllGrid()
            CreateCategoryTabAndChart()

            dtTheamaticBins = IOSThematicKPI.GetThenaticBins(connStrIOSServer, IOSKPIType.ICMKPI)
            SetICMKPIListControl()
            isFirstTime = False
            Me.ResumeLayout()
            ConfigurICMForm("frmICM")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Public Function GetCategoryDataTableCount(Optional ByRef refreshSubCharts As Boolean = False) As DataTable
        Dim distCategory As DataTable = Me.GetIOS_ICM_Config_Data().DefaultView.ToTable(True, "Category", "ShortedName")
        Dim distCategoryCount As DataTable = distCategory.Clone()
        distCategoryCount.Columns.Add("Count", GetType(Integer))
        For Each Category As DataRow In distCategory.Rows
            Dim row As DataRow = distCategoryCount.NewRow()
            row("Category") = Category("Category")
            Dim subCategoryCount = Me.GetSubCategoryDataTableCount(Category("Category").ToString())
            If (refreshSubCharts) Then
                Me.SubCategoryCharts(GetChartA(GetTabPage(vtabICM, Category("ShortedName").ToString().ToUpper)), Category("Category").ToString())
            End If
            row("Count") = subCategoryCount.AsEnumerable().Select(Function(w) w.Field(Of Integer)("count")).Sum()
            distCategoryCount.Rows.Add(row)
        Next
        Return distCategoryCount
    End Function

    Public Function GetSubCategoryDataTableCount(ByVal category As String) As DataTable
        Dim distSubCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "Category='" & category & "'", "", DataViewRowState.CurrentRows).ToTable(True, "ID_ICMConfig", "GUIColumn", "DBColumn", "Category")
        Dim distSubCategoryCount As DataTable = distSubCategory.Clone()
        distSubCategoryCount.Columns.Add("Count", GetType(Integer))
        Dim dt As DataTable = Me.GetIOS_ICM_Data()
        For Each subCategory As DataRow In distSubCategory.Rows
            Dim row As DataRow = distSubCategoryCount.NewRow()
            row("ID_ICMConfig") = subCategory("ID_ICMConfig")
            row("GUIColumn") = subCategory("GUIColumn")
            row("DBColumn") = subCategory("DBColumn")
            row("Category") = subCategory("Category")
            Dim DBColumn = subCategory("DBColumn")
            DBColumn = "[" & DBColumn & "]"
            If dtIOS_ICM_Filters IsNot Nothing Then
                Dim filterRow() As DataRow = Me.dtIOS_ICM_Filters.Select("ID_ICMConfig='" & subCategory("ID_ICMConfig") & "'")
                If (filterRow.Length > 0) Then
                    Dim filterSign As String = filterRow(0)("ICM_Operator")
                    Dim filterValue As String = filterRow(0)("ICM_Value")
                    Dim isDouble As Boolean = Double.TryParse(filterValue.GetDecimalString(), 0)
                    If Not (isDouble) Then
                        filterValue = "'" & filterValue & "'"
                    End If
                    Try
                        Dim tst As String = DBColumn.ToString() & filterSign.ToString() & filterValue & " AND  CONVERT(ISNULL(" & DBColumn.ToString() & ",''),'System.String')<>'' AND CONVERT(" & DBColumn.ToString() & ",'System.String')<>" & Chr(39) & Chr(39)
                        row("Count") = dt.Select(tst).Length
                    Catch ex As Exception
                        row("Count") = 0
                    End Try
                Else
                    Try
                        Dim rows = dt.Select("CONVERT(ISNULL(" & DBColumn.ToString() & ",''),'System.String')<>''")
                        row("Count") = IIf(rows.Length > 0, rows.Length, 0)
                    Catch ex As Exception
                        row("Count") = 0
                    End Try
                End If
            Else
                Try
                    Dim rows = dt.Select("CONVERT(ISNULL(" & DBColumn.ToString() & ",''),'System.String')<>''")
                    row("Count") = IIf(rows.Length > 0, rows.Length, 0)
                Catch ex As Exception
                    row("Count") = 0
                End Try
            End If

            distSubCategoryCount.Rows.Add(row)
        Next
        Return distSubCategoryCount
    End Function

    Public Function GetCategoryDataTable() As DataTable
        Dim distCategory As DataTable = Me.GetIOS_ICM_Config_Data().DefaultView.ToTable(True, "Category", "ShortedName")
        Return distCategory
    End Function

    Public Function GetShortedName(ByVal category) As String
        Dim shortedName As String = Nothing
        Dim distCategory As DataTable = GetCategoryDataTable()
        If (distCategory.Rows.Count > 0) Then
            Dim shortName = From w In distCategory.AsEnumerable()
                            Where w("Category") = category
                            Select w

            If (shortName IsNot Nothing) Then
                shortedName = shortName(0)("ShortedName").ToString
            End If
        End If
        Return shortedName
    End Function

    Public Function GetSubCategoryByCategory(ByVal category As String) As String()
        Dim distSubCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "Category='" & category & "'", "", DataViewRowState.CurrentRows).ToTable(True, "DBColumn")
        Dim subCategory(distSubCategory.Rows.Count + 2) As String
        subCategory(0) = "Cellid"
        subCategory(1) = "CellName"
        subCategory(2) = "UNodeBName"
        If (distSubCategory.Rows.Count > 0) Then
            For value As Integer = 3 To distSubCategory.Rows.Count + 2
                Dim asd As String = distSubCategory.Rows(value - 3)("DBColumn").ToString()
                subCategory(value) = asd
            Next
        End If
        Return subCategory
    End Function

    Public Function GetSubCategoryDataTable(ByVal category As String, ByVal scategory As String) As DataTable
        Dim distSubCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "Category='" & category & "' AND GUIColumn='" & scategory & "'", "", DataViewRowState.CurrentRows).ToTable(True, "ID_ICMConfig", "GUIColumn", "DBColumn", "Category", "SortBy", "IsActive")
        Dim distSubCategoryData As DataTable = New DataTable()
        distSubCategoryData.Columns.Add("Cellid")
        distSubCategoryData.Columns.Add("CellName")
        distSubCategoryData.Columns.Add("UNodeBName")
        distSubCategoryData.Columns.Add(distSubCategory(0)("DBColumn"))
        Dim dt As DataTable = Me.GetIOS_ICM_Data()
        Dim orderBy As String = String.Empty
        For Each subCategory As DataRow In distSubCategory.Rows
            orderBy = subCategory("SortBy")

            If dtIOS_ICM_Filters IsNot Nothing Then
                Dim filterRow() As DataRow = Me.dtIOS_ICM_Filters.Select("ID_ICMConfig='" & subCategory("ID_ICMConfig") & "'")
                If (filterRow.Length > 0) Then
                    Dim DBColumn = subCategory("DBColumn")
                    DBColumn = "[" & DBColumn & "]"
                    Dim filterSign As String = filterRow(0)("ICM_Operator")
                    Dim filterValue As String = filterRow(0)("ICM_Value")
                    Dim isDouble As Boolean = Double.TryParse(filterValue.GetDecimalString(), 0)
                    If Not (isDouble) Then
                        filterValue = "'" & filterValue & "'"
                    End If
                    Dim rows() As DataRow = dt.Select(DBColumn.ToString() & filterSign.ToString() & filterValue & " AND  CONVERT(ISNULL(" & DBColumn.ToString() & ",''),'System.String')<>'' AND CONVERT(" & DBColumn.ToString() & ",'System.String')<>" & Chr(39) & Chr(39))
                    If (rows.Length > 0) Then
                        For Each datarow As DataRow In rows
                            Dim row As DataRow = distSubCategoryData.NewRow()
                            row("Cellid") = datarow("Cellid")
                            row("CellName") = datarow("CellName")
                            row("UNodeBName") = datarow("UNodeBName")
                            row(subCategory("DBColumn")) = datarow(subCategory("DBColumn"))
                            distSubCategoryData.Rows.Add(row)
                        Next
                    End If
                Else
                    Dim DBColumn = subCategory("DBColumn")
                    DBColumn = "[" & DBColumn & "]"
                    Dim rows() As DataRow = dt.Select("CONVERT(ISNULL(" & DBColumn.ToString() & ",''),'System.String')<>'' AND CONVERT(" & DBColumn.ToString() & ",'System.String')<>" & Chr(39) & Chr(39))

                    For Each datarow As DataRow In rows
                        Dim row As DataRow = distSubCategoryData.NewRow()
                        row("Cellid") = datarow("Cellid")
                        row("CellName") = datarow("CellName")
                        row("UNodeBName") = datarow("UNodeBName")
                        row(subCategory("DBColumn")) = datarow(subCategory("DBColumn"))
                        distSubCategoryData.Rows.Add(row)
                    Next
                End If
            Else
                Dim DBColumn = subCategory("DBColumn")
                DBColumn = "[" & DBColumn & "]"
                Dim rows() As DataRow = dt.Select("CONVERT(ISNULL(" & DBColumn.ToString() & ",''),'System.String')<>'' AND CONVERT(" & DBColumn.ToString() & ",'System.String')<>" & Chr(39) & Chr(39))

                For Each datarow As DataRow In rows
                    Dim row As DataRow = distSubCategoryData.NewRow()
                    row("Cellid") = datarow("Cellid")
                    row("CellName") = datarow("CellName")
                    row("UNodeBName") = datarow("UNodeBName")
                    row(subCategory("DBColumn")) = datarow(subCategory("DBColumn"))
                    distSubCategoryData.Rows.Add(row)
                Next
            End If
        Next
        Return ShortData(distSubCategoryData, category, distSubCategory(0)("DBColumn"), orderBy)
    End Function

    Private Function ShortData(ByVal dtSubCategoryData As DataTable, ByVal category As String, ByVal subCategory As String, Optional ByVal sortingOrder As String = "DESC") As DataTable
        Try
            Dim sortExpression As String = subCategory + " " + sortingOrder
            Dim dt As DataTable = dtSubCategoryData.Clone()
            dt.TableName = subCategory
            dt.Columns(subCategory).DataType = GetType(Single)
            For Each item As DataRow In dtSubCategoryData.Rows
                Dim r As DataRow = dt.NewRow()
                For Each item1 As DataColumn In dtSubCategoryData.Columns
                    Try
                        If item(item1.ColumnName).ToString.ToUpper = "YES" Then
                            r(item1.ColumnName) = 1
                        Else
                            r(item1.ColumnName) = item(item1.ColumnName)
                        End If
                    Catch ex As Exception
                        r(item1.ColumnName) = 0
                    End Try
                Next
                dt.Rows.Add(r)
            Next
            Return New DataView(dt, "", sortExpression, DataViewRowState.CurrentRows).ToTable()
        Catch ex As Exception
            Return dtSubCategoryData
        End Try
    End Function

    Public Function GetIOS_ICM_Config_Data() As DataTable
        If (dtIOS_ICM_Config Is Nothing) Then
            Me.Fill_ICM_Data()
        End If
        Return Me.dtIOS_ICM_Config
    End Function

    Public Function GetIOS_ICM_Data() As DataTable
        Dim filterString As String = String.Empty
        Dim objectStr As New System.Text.StringBuilder()
        If (cmbReport.SelectedIndex > 0) Then
            filterString = filterString & " ReportName='" & cmbReport.SelectedItem.ToString & "' "
        End If

        If (cmbTechnology.SelectedIndex > 0) Then
            If (Not String.IsNullOrEmpty(filterString)) Then
                filterString = filterString & " AND"
            End If
            filterString = filterString & " Technology='" & cmbTechnology.SelectedItem.ToString & "' "
        End If
        If (cmbVendor.SelectedIndex > 0) Then
            If (Not String.IsNullOrEmpty(filterString)) Then
                filterString = filterString & " AND"
            End If
            filterString = filterString & " Vendor='" & cmbVendor.SelectedItem.ToString & "' "
        End If

        If (cmbVendor.SelectedIndex > 0 AndAlso cmbTargetObject.SelectedIndex > 0) Then
            Dim checkedObejct = TreeView_Checked2String(cmbVendor.SelectedItem.ToString & " " & cmbTechnology.SelectedItem.ToString, cmbTargetObject.SelectedItem.ToString, "ObjectID", tvObjectTree, cmbTargetObject)
            If Not (checkedObejct = "IN ()") Then
                filterString = filterString & " and " & cellcolumnname & " " & checkedObejct
            End If
        End If

        If (dtIOS_ICM Is Nothing) Then
            Me.Fill_ICM_Data()
        End If
        If Not (String.IsNullOrEmpty(filterString)) Then

            Dim rows() As DataRow = Me.dtIOS_ICM.Select(filterString)
            If (rows.Length > 0) Then
                Return rows.CopyToDataTable()
            Else
                Return Me.dtIOS_ICM.Clone()
            End If
        End If
        Return Me.dtIOS_ICM
    End Function

    'Private Function TreeView_CheckedNode2String(ByVal tech As String, ByVal aggr_to As String, ByVal outputtype As String) As String
    '    Dim nodelevel As Integer
    '    Dim outputstr As New System.Text.StringBuilder()

    '    If outputtype = "ObjectNameWild" Then
    '        outputstr.Append(" LIKE ")
    '    ElseIf outputtype = "Naked" Then
    '        outputstr.Append("")
    '    ElseIf outputtype = "TAGS_CM" Then
    '        outputstr.Append("")
    '    ElseIf outputtype = "ObjectType" Then
    '        outputstr.Append("")
    '    Else
    '        outputstr.Append("IN (")
    '    End If
    '    nodelevel = 3
    '    nodelevel = Treeview_GetNodeLevel(tech, aggr_to, cmbTargetObject, cmbVendor.SelectedItem.ToString)

    '    For Each nd As TreeNode In tvObjectTree.Nodes
    '        outputstr.Append(TreeView_Checked2String_Level(nd, nodelevel, "ObjectID"))
    '    Next
    '    Dim outputfinal As String = Nothing
    '    If outputtype = "ObjectNameWild" Then
    '        outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
    '    ElseIf outputtype = "Naked" Then
    '        outputfinal = outputstr.ToString.TrimEnd(",")
    '    ElseIf outputtype = "TAGS_CM" Then
    '        outputfinal = outputstr.ToString.Substring(0, Len(outputstr.ToString) - 5)
    '    Else
    '        outputfinal = outputstr.ToString.TrimEnd(",") + ")"
    '    End If

    '    Return outputfinal
    'End Function

    Private Sub Fill_ICM_Data()
        ''Dim sqlCom As String = "Select * from dbo.IOS_ICM;"
        ''sqlCom = sqlCom & "Select ICMCon.ID_ICMConfig,ICMCon.GUIColumn,ICMCon.DBColumn,ICMCon.CategoryID,ICMCat.Category,ICMCat.ShortedName,ICMCon.SortBy,ICMCat.IsActive from dbo.IOS_ICM_Configuration ICMCon inner join IOS_ICM_Category ICMCat ON ICMCat.CategoryID=ICMCon.CategoryID where ICMCat.IsActive=1"
        Dim ds As DataSet = Nothing
        ds = IOS.DataLibrary.clsSQLCommands.GetICMData(connStrIOSServer)
        If (ds IsNot Nothing AndAlso ds.Tables.Count > 1) Then
            Me.dtIOS_ICM = GetICMDataAfterFilter(ds.Tables(0))

            Me.dtIOS_ICM_Config = ds.Tables(1)
        End If
    End Sub

    Function GetICMDataAfterFilter(ByRef data As DataTable) As DataTable
        Try
            If (chkFilterCriteriaCombine.Checked) Then
                Dim filterString As String = String.Empty
                Dim distSubCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "", "", DataViewRowState.CurrentRows).ToTable(True, "ID_ICMConfig", "GUIColumn", "DBColumn", "Category")
                For Each subCategory As DataRow In distSubCategory.Rows
                    Dim filterRow() As DataRow = Me.dtIOS_ICM_Filters.Select("ID_ICMConfig='" & subCategory("ID_ICMConfig") & "'")
                    If (filterRow.Length > 0) Then
                        Dim DBColumn = subCategory("DBColumn")
                        DBColumn = "[" & DBColumn & "]"
                        Dim filterSign As String = filterRow(0)("ICM_Operator")
                        Dim filterValue As String = filterRow(0)("ICM_Value")
                        Dim isDouble As Boolean = Double.TryParse(filterValue.ToString(), 0)
                        If Not (isDouble) Then
                            filterValue = "'" & filterValue & "'"
                        End If
                        filterString = filterString & (DBColumn.ToString() & filterSign.ToString() & filterValue) & " AND "
                    End If
                Next
                filterString = filterString.Remove(filterString.Length - 4, 4)
                Dim rows = data.Select(filterString)
                If (rows.Length > 0) Then
                    Return rows.CopyToDataTable()
                Else
                    Return data.Clone()
                End If
            End If
        Catch ex As Exception
        End Try
        Return data
    End Function

    Private Sub ClearAllGrid()
        For Each oTab As ICMTab In LoopAllTabs()
            If (oTab IsNot Nothing) Then
                If (oTab.vdgv IsNot Nothing) Then
                    oTab.vdgv.DataSource = Nothing
                End If
            End If
        Next
        ICMGridInfo.ClearAllGrid()
    End Sub

#End Region

#Region "Create CategoryTab and Chart"

    Private Sub CreateCategoryTabAndChart()
        Dim distCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "", "", DataViewRowState.CurrentRows).ToTable(True, "Category", "ShortedName")
        Dim tableLayoutGrid As System.Windows.Forms.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Dim col As Integer = 4
        Dim row As Integer
        Dim colCounter As Integer = 0
        Dim rowCounter As Integer = 0
        If (distCategory.Rows.Count > 0) Then
            row = (distCategory.Rows.Count \ col) * 2
            If ((distCategory.Rows.Count Mod col) > 0) Then
                row += 2
            End If
            CreateCategoryBottomTable(tableLayoutGrid, col, row)
            Dim vdgv As DevExpress.XtraGrid.GridControl
            For Each item As DataRow In distCategory.Rows
                vdgv = Nothing
                CreateCategoryTab(item("Category"), item("ShortedName"), vtabICM)
                If (colCounter < col) Then
                    tableLayoutGrid.Controls.Add(CreateGridLabel(item("Category"), item("ShortedName")), colCounter, rowCounter)
                    vdgv = CreateBottomGrid(item("Category"), item("ShortedName"))
                    tableLayoutGrid.Controls.Add(vdgv, colCounter, rowCounter + 1)
                Else
                    colCounter = 0
                    rowCounter = rowCounter + 2
                    tableLayoutGrid.Controls.Add(CreateGridLabel(item("Category"), item("ShortedName")), colCounter, rowCounter)
                    vdgv = CreateBottomGrid(item("Category"), item("ShortedName"))
                    tableLayoutGrid.Controls.Add(vdgv, colCounter, rowCounter + 1)
                End If
                colCounter = colCounter + 1
                ICMGridInfo.SetGrid(item("ShortedName"), vdgv)
            Next
            VPanel1.Controls.Add(tableLayoutGrid)
        End If
    End Sub

    Private Sub CreateCategoryBottomTable(ByVal tableLayoutGrid As System.Windows.Forms.TableLayoutPanel, ByVal col As Integer, ByVal rows As Integer)
        tableLayoutGrid.ColumnCount = col
        tableLayoutGrid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        tableLayoutGrid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330.0!))
        tableLayoutGrid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        tableLayoutGrid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320.0!))
        tableLayoutGrid.Location = New System.Drawing.Point(0, 0)
        tableLayoutGrid.Name = "exTableLayoutPanelGrid"
        tableLayoutGrid.RowCount = rows
        For rowCounter As Integer = 1 To tableLayoutGrid.RowCount
            If (rowCounter Mod 2 = 0) Then
                tableLayoutGrid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320.0!))
            Else
                tableLayoutGrid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            End If
        Next
        tableLayoutGrid.Size = New System.Drawing.Size(1250, (340 * (rows / 2)))
        tableLayoutGrid.TabIndex = 2
    End Sub

    Private Function CreateBottomGrid(ByVal category As String, ByVal shortName As String) As DevExpress.XtraGrid.GridControl
        Dim gcBottom As New DevExpress.XtraGrid.GridControl()
        Dim gvBottom As New DevExpress.XtraGrid.Views.Grid.GridView()

        gcBottom.ViewCollection.Add(gvBottom)
        gcBottom.MainView = gvBottom
        gvBottom.GridControl = gcBottom

        gvBottom.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        gvBottom.OptionsView.AllowCellMerge = False
        gvBottom.OptionsView.ShowGroupPanel = False
        gvBottom.OptionsView.ColumnAutoWidth = False
        gvBottom.OptionsView.ShowColumnHeaders = True

        gvBottom.OptionsCustomization.AllowFilter = True
        gvBottom.OptionsCustomization.AllowSort = True

        gvBottom.OptionsSelection.MultiSelect = False
        gvBottom.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        gvBottom.OptionsSelection.EnableAppearanceFocusedCell = False

        gvBottom.OptionsBehavior.Editable = False

        gvBottom.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True

        gvBottom.BestFitColumns()
        gcBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
        gcBottom.Dock = System.Windows.Forms.DockStyle.Fill
        gcBottom.Location = New System.Drawing.Point(633, 23)
        gcBottom.Name = "vdgv_" + shortName
        gcBottom.Size = New System.Drawing.Size(294, 314)
        gcBottom.TabIndex = 27
        gcBottom.Text = category
        Return gcBottom
    End Function

    Private Function CreateGridLabel(ByVal category As String, ByVal shortName As String) As DevExpress.XtraEditors.LabelControl
        Dim vLabel As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl()
        vLabel.BackColor = System.Drawing.Color.Transparent
        vLabel.Dock = System.Windows.Forms.DockStyle.Fill
        vLabel.ForeColor = System.Drawing.SystemColors.ControlText
        vLabel.Location = New System.Drawing.Point(3, 3)
        vLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        vLabel.Name = "vLabel" + shortName
        vLabel.Size = New System.Drawing.Size(294, 14)
        vLabel.TabIndex = 18
        vLabel.Text = category
        Return vLabel
    End Function

    Iterator Function LoopAllTabs() As IEnumerable(Of ICMTab)
        Dim distCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "", "", DataViewRowState.CurrentRows).ToTable(True, "Category", "ShortedName")
        If (distCategory.Rows.Count > 0) Then
            For Each item As DataRow In distCategory.Rows
                Dim searchedTab As ICMTab = Nothing
                Dim sortedName As String = item("ShortedName")
                If (vtabICM.TabPages.Count > 0) Then
                    For Each pageTab As DevExpress.XtraTab.XtraTabPage In vtabICM.TabPages
                        If (pageTab.Tag.ToUpper = sortedName.ToUpper) Then
                            searchedTab = TryCast(pageTab, ICMTab)
                            Exit For
                        End If
                    Next
                End If
                Yield searchedTab
            Next
        End If
    End Function

    Private Function GetTabPage(ByRef icmTabControl As DevExpress.XtraTab.XtraTabControl, ByVal sortedName As String) As ICMTab
        Dim searchedTab As ICMTab = Nothing
        If (icmTabControl.TabPages.Count > 0) Then
            For Each pageTab As DevExpress.XtraTab.XtraTabPage In icmTabControl.TabPages
                If (pageTab.Tag.ToUpper = sortedName.ToUpper) Then
                    searchedTab = TryCast(pageTab, ICMTab)
                    Exit For
                End If
            Next
        End If
        Return searchedTab
    End Function

    Private Function GetChartA(ByRef icmTab As ICMTab) As Chart
        Dim searchedChart As Chart = Nothing
        If (icmTab IsNot Nothing) Then
            searchedChart = icmTab.ChartA
        End If
        Return searchedChart
    End Function

    Private Function GetChartB(ByRef icmTab As ICMTab) As Chart
        Dim searchedChart As Chart = Nothing
        If (icmTab IsNot Nothing) Then
            searchedChart = icmTab.ChartB
        End If
        Return searchedChart
    End Function

    Private Function GetChartGrid(ByRef icmTab As ICMTab) As DevExpress.XtraGrid.GridControl
        Dim searchedGrid As DevExpress.XtraGrid.GridControl = Nothing
        If (icmTab IsNot Nothing) Then
            searchedGrid = icmTab.vdgv
        End If
        Return searchedGrid
    End Function

    Private Sub CreateCategoryTab(ByVal categoryName As String, ByVal shortedName As String, ByRef icmTabControl As DevExpress.XtraTab.XtraTabControl)
        Try
            ''Create a New Tab
            Dim vTabPageNew As ICMTab = New ICMTab()
            vTabPageNew.Name = "vTabPageICM" & shortedName
            vTabPageNew.Text = categoryName
            vTabPageNew.Tag = shortedName
            vTabPageNew.Tooltip = categoryName
            Dim IsTabAlready As Boolean = False
            Dim IsNewTab As Boolean = False

            vTabPageNew.Controls.Add(GetSplitWithChildControl(shortedName, vTabPageNew))
            icmTabControl.TabPages.Add(vTabPageNew)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function GetSplitWithChildControl(ByVal shortedName As String, ByRef tab As ICMTab) As System.Windows.Forms.SplitContainer
        Try
            Dim splitContNew As System.Windows.Forms.SplitContainer = New System.Windows.Forms.SplitContainer()
            splitContNew.Name = "SplitConChartGrid" & shortedName
            splitContNew.Dock = DockStyle.Fill
            splitContNew.Location = New System.Drawing.Point(3, 3)
            splitContNew.Orientation = System.Windows.Forms.Orientation.Horizontal

            Dim tblCharts As New System.Windows.Forms.TableLayoutPanel()
            Dim chartA As dotnetCHARTING.WinForms.Chart = GetNewChart()
            Dim chartB As dotnetCHARTING.WinForms.Chart = GetNewChart()
            Dim Annotation2 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
            Dim Label3 As dotnetCHARTING.WinForms.Label = New dotnetCHARTING.WinForms.Label()

            Dim Annotation3 As dotnetCHARTING.WinForms.Annotation = New dotnetCHARTING.WinForms.Annotation()
            Dim Label4 As dotnetCHARTING.WinForms.Label = New dotnetCHARTING.WinForms.Label()

            Dim vdgv As DevExpress.XtraGrid.GridControl = BindChartDataGrid(shortedName)

            chartA.AutoScroll = True
            chartA.ContextMenuStrip = cm_SourceControlSubCatagory
            chartA.Tag = shortedName

            AddHandler chartA.Click, AddressOf chartBL_A_Click
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
            chartA.Name = "ChartA_" & shortedName
            chartA.NoDataLabel.Text = "No Data"
            chartA.ObjectChart = Label4
            chartA.Size = New System.Drawing.Size(315, 289)
            chartA.SmartLabelLine.Visible = True
            chartA.StartDateOfYear = New Date(CType(0, Long))
            chartA.TabIndex = 8
            chartA.TempDirectory = "C:\Users\IOS\AppData\Local\Temp\"

            chartB.AutoScroll = True
            chartB.ContextMenuStrip = cms_HistogramChart
            AddHandler chartB.Click, AddressOf chart_B_Click
            AddHandler chartB.MouseDown, AddressOf chartBL_B_MouseDown
            chartB.Background.Color = System.Drawing.Color.White
            Annotation2.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
            Annotation2.DynamicSize = True
            Annotation2.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
            Annotation2.InteriorLine.Visible = True
            Annotation2.Line.Color = System.Drawing.Color.Gray
            Annotation2.Line.Visible = True
            Annotation2.Orientation = dotnetCHARTING.WinForms.Orientation.TopRight
            Annotation2.Padding = 2
            Annotation2.Shadow.Visible = False
            Annotation2.Size = New System.Drawing.Size(314, 288)
            Annotation2.Visible = True
            chartB.Box = Annotation2
            chartB.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
            chartB.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
            chartB.ChartArea.DefaultElement.DefaultSubValue.Line.Color = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(59, Byte), Integer))
            chartB.ChartArea.DefaultElement.DefaultSubValue.Line.Visible = True
            chartB.ChartArea.DefaultElement.DefaultSubValue.Visible = True
            chartB.ChartArea.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
            chartB.ChartArea.DefaultElement.LegendEntry.DividerLine.Visible = True
            chartB.ChartArea.DefaultElement.Outline.Visible = True
            chartB.ChartArea.DefaultElement.SmartLabel.Color = System.Drawing.Color.Empty
            chartB.ChartArea.DefaultElement.SmartLabel.Line.Visible = True
            chartB.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
            chartB.ChartArea.InteriorLine.Visible = True
            chartB.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
            chartB.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
            chartB.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
            chartB.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
            chartB.ChartArea.LegendBox.DefaultEntry.DividerLine.Visible = True
            chartB.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
            chartB.ChartArea.LegendBox.HeaderEntry.DividerLine.Visible = True
            chartB.ChartArea.LegendBox.HeaderEntry.Name = "Name"
            chartB.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
            chartB.ChartArea.LegendBox.HeaderEntry.Value = "Value"
            chartB.ChartArea.LegendBox.HeaderEntry.Visible = False
            chartB.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
            chartB.ChartArea.LegendBox.InteriorLine.Visible = True
            chartB.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
            chartB.ChartArea.LegendBox.Line.Visible = True
            chartB.ChartArea.LegendBox.Padding = 4
            chartB.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
            chartB.ChartArea.LegendBox.Visible = True
            chartB.ChartArea.Line.Color = System.Drawing.Color.Gray
            chartB.ChartArea.Line.Visible = True
            chartB.ChartArea.StartDateOfYear = New Date(CType(0, Long))
            chartB.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
            chartB.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
            chartB.ChartArea.TitleBox.InteriorLine.Visible = True
            chartB.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
            chartB.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
            chartB.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
            chartB.ChartArea.TitleBox.Line.Visible = True
            chartB.ChartArea.TitleBox.Visible = True
            chartB.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
            chartB.ChartArea.XAxis.DefaultTick.GridLine.Visible = True
            chartB.ChartArea.XAxis.DefaultTick.Line.Length = 3
            chartB.ChartArea.XAxis.DefaultTick.Line.Visible = True
            chartB.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
            chartB.ChartArea.XAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
            chartB.ChartArea.XAxis.ScaleBreakLine.Visible = True
            chartB.ChartArea.XAxis.TickLabelSeparatorLine.Visible = True
            chartB.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
            chartB.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
            chartB.ChartArea.XAxis.ZeroTick.GridLine.Visible = True
            chartB.ChartArea.XAxis.ZeroTick.Line.Length = 3
            chartB.ChartArea.XAxis.ZeroTick.Line.Visible = True
            chartB.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.LightGray
            chartB.ChartArea.YAxis.DefaultTick.GridLine.Visible = True
            chartB.ChartArea.YAxis.DefaultTick.Line.Length = 3
            chartB.ChartArea.YAxis.DefaultTick.Line.Visible = True
            chartB.ChartArea.YAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
            chartB.ChartArea.YAxis.ScaleBreakLine.Visible = True
            chartB.ChartArea.YAxis.TickLabelSeparatorLine.Visible = True
            chartB.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
            chartB.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
            chartB.ChartArea.YAxis.ZeroTick.GridLine.Visible = True
            chartB.ChartArea.YAxis.ZeroTick.Line.Length = 3
            chartB.ChartArea.YAxis.ZeroTick.Line.Visible = True
            chartB.DataGrid = Nothing
            chartB.DefaultElement.DefaultSubValue.Line.Visible = True
            chartB.DefaultElement.DefaultSubValue.Visible = True
            chartB.DefaultElement.LegendEntry.DividerLine.Color = System.Drawing.Color.Empty
            chartB.DefaultElement.LegendEntry.DividerLine.Visible = True
            chartB.DefaultElement.Outline.Visible = True
            chartB.Dock = System.Windows.Forms.DockStyle.Fill
            chartB.Location = New System.Drawing.Point(324, 3)
            chartB.MinimumSize = New System.Drawing.Size(100, 50)
            chartB.Name = "ChartB_" & shortedName
            chartB.NoDataLabel.Text = "No Data"
            chartB.ObjectChart = Label3
            chartB.Size = New System.Drawing.Size(315, 289)
            chartB.SmartLabelLine.Visible = True
            chartB.StartDateOfYear = New Date(CType(0, Long))
            chartB.TabIndex = 9
            chartB.TempDirectory = "C:\Users\IOS\AppData\Local\Temp\"

            tblCharts.BackColor = System.Drawing.SystemColors.Control
            tblCharts.ColumnCount = 2
            tblCharts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            tblCharts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            tblCharts.Controls.Add(chartB, 0, 0)
            tblCharts.Controls.Add(chartA, 0, 0)
            tblCharts.Dock = System.Windows.Forms.DockStyle.Fill
            tblCharts.Location = New System.Drawing.Point(0, 0)
            tblCharts.Name = "tblCharts"
            tblCharts.RowCount = 1
            tblCharts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            tblCharts.Size = New System.Drawing.Size(642, 295)
            tblCharts.TabIndex = 2S

            splitContNew.Panel1.Controls.Add(tblCharts)

            Dim dView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(vdgv.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView)
            dView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False
            dView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False
            dView.OptionsBehavior.Editable = False
            dView.OptionsCustomization.AllowSort = True
            dView.OptionsCustomization.AllowFilter = True
            dView.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True
            dView.OptionsCustomization.AllowColumnResizing = True
            dView.OptionsCustomization.AllowRowSizing = True
            dView.OptionsSelection.MultiSelect = False
            dView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            dView.OptionsView.AllowCellMerge = False
            dView.OptionsView.ShowGroupPanel = False
            dView.BestFitColumns()
            dView.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.Default
            vdgv.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
            vdgv.Dock = System.Windows.Forms.DockStyle.Fill
            vdgv.Location = New System.Drawing.Point(0, 0)
            vdgv.Name = "vdgvTRU_All"
            vdgv.Size = New System.Drawing.Size(642, 140)
            vdgv.TabIndex = 6
            vdgv.Text = "VDataGridView1"
            AddHandler vdgv.MouseDown, AddressOf vdgvBL_All_MouseDown
            AddHandler dView.RowCellClick, AddressOf dgvCellChart_CellMouseClick
            splitContNew.Panel2Collapsed = True
            splitContNew.Panel2.Controls.Add(vdgv)
            splitContNew.Size = New System.Drawing.Size(642, 439)
            splitContNew.SplitterDistance = 295
            splitContNew.TabIndex = 8

            tab.ChartA = chartA
            tab.ChartB = chartB
            tab.vdgv = vdgv
            Return splitContNew

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        Return Nothing
    End Function

    Function GetNewChart() As dotnetCHARTING.WinForms.Chart
        Dim chart As New dotnetCHARTING.WinForms.Chart()
        chart.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        chart.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Return chart
    End Function

    Private Function BindChartDataGrid(ByVal shortedName As String) As DevExpress.XtraGrid.GridControl
        Dim newGVMap As New DevExpress.XtraGrid.GridControl()
        Dim newGridView As New DevExpress.XtraGrid.Views.Grid.GridView()
        newGVMap.ViewCollection.Add(newGridView)
        newGVMap.MainView = newGridView
        newGVMap.Name = "gvMap_" & shortedName
        newGVMap.Tag = shortedName
        newGridView.Tag = shortedName
        newGVMap.Dock = DockStyle.Fill

        newGridView.OptionsBehavior.Editable = False
        newGridView.OptionsCustomization.AllowSort = True
        newGridView.OptionsCustomization.AllowFilter = True
        newGridView.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True
        newGridView.OptionsCustomization.AllowColumnResizing = True
        newGridView.OptionsCustomization.AllowRowSizing = True
        newGridView.OptionsSelection.MultiSelect = False
        newGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        newGridView.OptionsView.AllowCellMerge = False
        newGridView.OptionsView.ShowGroupPanel = False
        newGridView.BestFitColumns()

        Return newGVMap
    End Function

#End Region

#Region "Left Region select controls AND tab controls"

    Private Sub ClearCombo(ByRef c As DevExpress.XtraEditors.ComboBoxEdit, ByVal defaultText As String)
        c.SuspendLayout()
        c.Properties.Items.Clear()
        c.Refresh()
        c.Properties.Items.Add(defaultText)
        If (Not isFirstTime) Then
            c.SelectedIndex = 0
        End If
        c.Update()
        c.ResumeLayout()
    End Sub

    Private Sub BindTechnology()
        Dim dtTech As DataTable = Me.GetDistinctValue("ParamHistory=1", "Technology")
        If (dtTech.Rows.Count > 0) Then
            BindDevExComboBoxWithValueMember(cmbTechnology, dtTech, "Technology", "Technology", "Select Technology")
        End If
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            ClearCombo(cmbTargetObject, "Select Type")
            ClearCombo(cmbVendor, "Select Vendor")
            If cmbTechnology.SelectedIndex > 0 Then
                BindVendor()
                If (cmbReport.SelectedIndex > 0) Then
                    Reload()
                End If
            Else
                If (Not isFirstTime) Then
                    SubCatageryChartsClear()
                    HistogramChartsClear()
                    chart_Overview.ClearAll()
                    ClearAllGrid()
                End If
            End If
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Function GetDistinctValue(ByVal filterCondition As String, ByVal ParamArray distinctColumns() As String) As DataTable
        Dim dt As DataTable = Nothing
        If (dt_IOS_ObjectConfig IsNot Nothing) Then
            dt = New DataView(dt_IOS_ObjectConfig, filterCondition, "", DataViewRowState.CurrentRows).ToTable(True, distinctColumns)
        End If
        Return dt
    End Function

    Private Sub cmbVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVendor.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Not (cmbVendor.SelectedIndex = 0) Then
                Dim dt As DataTable = GetDistinctValue("ParamHistory=1 AND Vendor='" & cmbVendor.SelectedItem.ToString & "' AND Technology='" & cmbTechnology.SelectedItem.ToString & "'", "Object")
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    BindDevExComboBoxWithValueMember(cmbTargetObject, dt, "Object", "Object", "Select Type")
                Else
                    ClearCombo(cmbTargetObject, "Select Type")
                End If

                dt = IOS.DataLibrary.clsSQLCommands.GetTop1LayerColumn2Tree(connStrIOSServer, cmbVendor.SelectedItem.ToString, cmbTechnology.SelectedItem.ToString)
                Try
                    If Not IsDBNull(dt(0)(0)) Then
                        cellcolumnname = dt(0)(0).ToString
                        cellcolumnname = "CELLNAME"
                    End If
                Catch
                End Try
            Else
                ClearCombo(cmbTargetObject, "Select Type")
            End If
            Reload()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub BindVendor()
        Dim dtVendor As DataTable = Me.GetDistinctValue("ParamHistory=1 AND Technology='" & cmbTechnology.SelectedItem.ToString & "'", "Vendor")
        cmbVendor.Properties.Items.Clear()
        If (dtVendor IsNot Nothing AndAlso dtVendor.Rows.Count > 0) Then
            BindDevExComboBoxWithValueMember(cmbVendor, dtVendor, "Vendor", "Vendor", "Select Vendor")
        End If
    End Sub

    Private Sub TreeViewStats_AfterCheck(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvObjectTree.AfterCheck
        CheckTreeNodeAndCount(e.Node, 0, Nothing)
    End Sub

    Private Sub TreeViewStats_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvObjectTree.NodeMouseClick
        Reload()
    End Sub

    Private Sub txtSearchObject_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchObject.TextChanged
        txtObjectSearch_TextChanged(tvObjectTree, txtSearchObject.Text)
    End Sub

    Private Sub cmbTargetObject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTargetObject.SelectedIndexChanged
        Try
            tvObjectTree.SuspendLayout()
            tvObjectTree.Nodes.Clear()

            If (cmbTargetObject.SelectedIndex > 0) Then
                If Not cmbTargetObject.EditValue Is Nothing Then
                    Dim strNetwork As String = ""
                    Try
                        Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("Technology='" & cmbTechnology.SelectedItem.ToString & "' AND Vendor='" & cmbVendor.SelectedItem.ToString & "' AND Object='" & cmbTargetObject.Text & "'")
                        If dr.Count > 0 Then
                            strNetwork = dr(0)("Tech").ToString
                        End If
                    Catch
                    End Try

                    FillObjectTreeData(tvObjectTree, strNetwork, cmbTargetObject.SelectedItem.ToString)

                End If
            End If

            chart_Overview.SuspendLayout()
            chart_Overview.Title = "Template: "
            chart_Overview.Refresh()
            chart_Overview.Update()
            chart_Overview.ResumeLayout()

            tvObjectTree.Refresh()
            tvObjectTree.ResumeLayout()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub KPI_FilterTemplate_Reload(ByVal tech As String, ByVal currentselection As Integer)
        Dim sql_insert As String = Nothing
        Dim connstring As String = Nothing
        Try
            ''sql_insert = "SELECT * FROM IOS_ICM_Filters_Templates WHERE (Tech ='" & tech & "')"
            dt_IOS_Template = IOS.DataLibrary.clsSQLCommands.GetKPIFilterTemplateData(connStrIOSServer, tech)
            Dim cmb As DevExpress.XtraEditors.ComboBoxEdit = Nothing
            If Not dt_IOS_Template Is Nothing Then
                If dt_IOS_Template.Rows.Count = 0 Then
                    KPI_FilterTemplate_Add("TestFilter", tech)
                End If
                BindDevExComboBoxWithValueMember(cmbTemplate, dt_IOS_Template, dt_IOS_Template.Columns(0).Caption, dt_IOS_Template.Columns(1).Caption)
                If cmbTemplate.Properties.Items.Count > 0 Then
                    cmbTemplate.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally

        End Try
    End Sub

    Private Sub KPI_FilterTemplate_Add(ByVal Templatename As String, ByVal tech As String)
        Dim sql_insert As String = Nothing
        Dim connstring As String = Nothing

        Try
            Dim parray()() As String = {New String() {"@TemplateName", Chr(39) & Templatename & Chr(39)},
                                        New String() {"@Tech", Chr(39) & tech & Chr(39)},
                                        New String() {"@userid", Chr(39) & System.Environment.UserName.ToString.Substring(0, Math.Min(10, System.Environment.UserName.ToString.Length)) & Chr(39)}}

            sql_insert = GetSQL(8800, parray)(1)
            connstring = GetSQL(8800, parray)(0)
            DataAccessorODBC.GetDataTable(connstring, sql_insert)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally

        End Try
    End Sub

    Private Sub KPI_Filter_Add(ByVal templateid As Integer, ByVal icmConfigID As Integer, ByVal guiColumn As String, ByVal icm_operator As String, ByVal icm_value As String)
        Dim sql As String = Nothing
        Dim connstring As String = Nothing
        Dim dt_filters As DataTable = Nothing

        Try
            ''sql = "INSERT INTO IOS_ICM_Filters(ICMFilterTemplateID, ID_ICMConfig, GUIColumn, ICM_Operator, ICM_Value) "
            ''sql = sql & "VALUES(" & templateid & "," & icmConfigID & ", '" & guiColumn & "', '" & icm_operator & "','" & icm_value & "')"
            IOS.DataLibrary.clsSQLCommands.InsertIcmKpiFilter(connStrIOSServer, templateid, icmConfigID, guiColumn, icm_operator, icm_value)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Sub KPI_Filter_Update(ByVal icmfilterid As Integer, ByVal templateid As Integer, ByVal icmConfigID As Integer, ByVal guiColumn As String, ByVal icm_operator As String, ByVal icm_value As String)
        Dim sql As String = Nothing
        Dim connstring As String = Nothing
        Dim dt_filters As DataTable = Nothing
        Try
            ''sql = "update IOS_ICM_Filters SET ICMFilterTemplateID =" & templateid & ",ID_ICMConfig =" & icmConfigID & ",GUIColumn ='" & guiColumn & "',ICM_Operator ='" & icm_operator & "', ICM_Value ='" & icm_value & "' WHERE ICMFilterID = " & icmfilterid
            IOS.DataLibrary.clsSQLCommands.UpdateIcmKpiFilter(connStrIOSServer, templateid, icmConfigID, guiColumn, icm_operator, icm_value, icmfilterid)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub KPI_Filter_Delete(ByVal icmfilterid As Integer)
        Dim sql As String = Nothing
        Dim connstring As String = Nothing
        Dim dt_filters As DataTable = Nothing
        Try
            ''sql = "DELETE FROM IOS_ICM_Filters WHERE ICMFilterID = " & icmfilterid
            IOS.DataLibrary.clsSQLCommands.DeleteIcmKpiFilter(connStrIOSServer, icmfilterid)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbTemplate_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbTemplate.SelectedValueChanged
        TreeListView_Update(tech)
    End Sub

    Public Sub Filters_Initialize()
        Dim col1 As TreeListViewColumn = New TreeListViewColumn("GUIColumn", "")
        Dim col2 As TreeListViewColumn = New TreeListViewColumn("Op", "")
        Dim col3 As TreeListViewColumn = New TreeListViewColumn("Value", "")
        col3.ContentControlVisibility = ContentControlVisibility.AlwaysVisible
        col1.Width = 160
        col2.Width = 30
        col3.Width = 40
        Try
            If tlvFilters.Columns.Count = 0 Then
                tlvFilters.Columns.Clear()
                tlvFilters.Columns.Add(col1)
                tlvFilters.Columns.Add(col2)
                tlvFilters.Columns.Add(col3)
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try

        'Filling combobox
        KPI_FilterTemplate_Reload(tech, 0)
        Dim dsFilter As DataTable = New System.Data.DataTable
        dsFilter = Me.GetIOS_ICM_Config_Data()
        If dsFilter Is Nothing Then
            Exit Sub
        End If
        Try
            BindDevExComboBoxWithValueMember(cmbFilterKPI, dsFilter, "ID_ICMConfig", "GUIColumn", "Select ICM")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        dsFilter.Dispose()
        dsFilter = Nothing
    End Sub

    Public Function IsValidTemplate(ByRef templateId As String) As Boolean
        If dt_IOS_Template IsNot Nothing Then
            Dim rows() As DataRow = dt_IOS_Template.Select("ICMFilterTemplateID='" & templateId & "' AND TemplateOwner='" & System.Environment.UserName.ToString.Substring(0, Math.Min(10, System.Environment.UserName.ToString.Length)) & "'")
            If (rows.Length > 0) Then
                Return True
            End If
            Return False
        End If
        Return False
    End Function

    Public Sub TreeListView_Update(ByVal tech As String)
        Dim sql As String = Nothing
        Dim connstring As String = Nothing
        Try
            Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
            If (cmbTemplate.EditValue Is Nothing) Then
                ''sql = "SELECT * FROM IOS_ICM_Filters WHERE ICMFilterTemplateID in (SELECT ICMFilterTemplateID FROM IOS_ICM_Filters_Templates where Tech='" & tech & "')"
                dtIOS_ICM_Filters = IOS.DataLibrary.clsSQLCommands.GetIcmFilters(connStrIOSServer, tech)
            Else
                ''sql = "SELECT * FROM IOS_ICM_Filters WHERE ICMFilterTemplateID in (SELECT ICMFilterTemplateID FROM IOS_ICM_Filters_Templates where Tech='" & tech & "') AND ICMFilterTemplateID=" & TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value
                dtIOS_ICM_Filters = IOS.DataLibrary.clsSQLCommands.GetIcmFilters(connStrIOSServer, tech, TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)
            End If
            btnFilterAdd.Enabled = Me.IsValidTemplate(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)
            btnFilterDel.Enabled = Me.IsValidTemplate(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)

            Dim tlv As TreeListView = Nothing
            tlv = tlvFilters
            tlv.Nodes.Clear()
            For Each drow As DataRow In dtIOS_ICM_Filters.Rows
                Dim newnode As TreeListViewNode = New TreeListViewNode(drow(3).ToString)
                newnode.Tag = drow(0).ToString
                Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(drow(3).ToString)
                newnode.SubItems.Add(si1)
                Dim si2 As TreeListViewSubItem = New TreeListViewSubItem(drow(4).ToString)
                si2.Name = "KPIOperator"

                newnode.SubItems.Add(si2)
                Dim si3 As TreeListViewSubItem = New TreeListViewSubItem(drow(5).ToString.GetDecimalString)
                si3.Name = "KPIValue"
                newnode.SubItems.Add(si3)
                tlv.Nodes.Add(newnode)
            Next

            tlv.Columns(0).Width = 150
            tlv.Refresh()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
        End Try
    End Sub

    Private Function BindValueTextBox(ByVal rowIndex As String, ByVal value As String) As DevExpress.XtraEditors.TextEdit
        Dim txtBox As New DevExpress.XtraEditors.TextEdit()
        txtBox.Tag = rowIndex
        txtBox.Text = value
        txtBox.ForeColor = Color.DarkGray
        txtBox.Size = New System.Drawing.Size(82, 16)
        Return txtBox
    End Function

    Private Sub btnFilterDel_Click(sender As Object, e As EventArgs) Handles btnFilterDel.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        If Not tlvFilters.SelectedSubItem Is Nothing Then
            Try
                KPI_Filter_Delete(CInt(tlvFilters.SelectedSubItem.Parent.Tag))
                TreeListView_Update(tech)
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try
        End If
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnFilterAdd_Click(sender As Object, e As EventArgs) Handles btnFilterAdd.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Try
                If cmbTemplate.EditValue Is Nothing Then
                    Exit Sub
                End If

                If cmbFilterKPI.EditValue Is Nothing Then
                    Exit Sub
                End If
                If cmbFilterKPI.SelectedIndex = 0 Then
                    Exit Sub
                End If
                If cmbFilterOp.EditValue Is Nothing Then
                    Exit Sub
                End If
                If txtFilterValue.Text Is Nothing Then
                    Exit Sub
                End If
                If String.IsNullOrEmpty(txtFilterValue.Text) Then
                    Exit Sub
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                Exit Sub
            End Try
            ' inserting into db
            KPI_Filter_Add(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value, TryCast(cmbFilterKPI.SelectedItem, clsComboBoxItem).Value, cmbFilterKPI.SelectedItem.ToString, cmbFilterOp.SelectedItem.ToString, txtFilterValue.Text.Trim)
            TreeListView_Update(tech)
            txtFilterValue.Text = ""
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tlvFilters_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvFilters.SubItemSelectionChanged
        Try
            If Not tlvFilters.SelectedSubItem Is Nothing Then
                btnFilterAdd.Enabled = Me.IsValidTemplate(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)
                btnFilterDel.Enabled = Me.IsValidTemplate(TryCast(cmbTemplate.SelectedItem, clsComboBoxItem).Value)
                For Each it As clsComboBoxItem In cmbFilterKPI.Properties.Items
                    If it.Text = tlvFilters.SelectedNode.SubItems(0).Text Then
                        cmbFilterKPI.SelectedItem = it
                        For Each opItem As clsComboBoxItem In cmbFilterOp.Properties.Items
                            If opItem.Text = tlvFilters.SelectedNode.SubItems(1).Text Then
                                cmbFilterOp.SelectedItem = opItem
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
                txtFilterValue.Text = tlvFilters.SelectedNode.SubItems(2).Text
            Else
                btnFilterDel.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Chart Code"

    Public Sub Reload()
        If (Not IsFromSendToICM) Then
            OverViewChart()
            HistogramChartsClear()
            BindGridAll()
            lblMSG.Text = ""
            txtComment.Text = ""
            lblForcastStatistics.Text = "Overview Forecast statistics of clicked cell :"
            lblRecommendation.Text = ""
            ICMGridInfo.ClearAllGrid()
        End If
    End Sub

    Private Sub OverViewChart()
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If cmbReport.SelectedIndex > 0 AndAlso cmbTechnology.SelectedIndex > 0 Then
                Dim yaxis As New IOSAxis()
                yaxis.Orientation = Orientation.Left
                yaxis.MinimumInterval = 1
                yaxis.NumberPrecision = 0
                yaxis.ElementMarkerType = ElementMarkerType.Circle
                Dim ChartElements As New List(Of String)
                ChartElements.Add("Category")
                ChartElements.Add("Count")
                yaxis.ElementListToApply.Add("Count")
                Dim listOfYAxis As New List(Of IOSAxis)
                listOfYAxis.Add(yaxis)
                chart_Overview.SuspendLayout()
                Dim checkedObejct = TreeView_Checked2String(cmbTechnology.SelectedItem.ToString, cmbTargetObject.SelectedItem.ToString, "ObjectID", tvObjectTree, cmbTargetObject).Replace("IN (", "").Replace(")", "")

                chart_Overview.LegendBox.Visible = False

                Dim objIOSChartManager As New IOSChartManager(chart_Overview, Me.GetCategoryDataTableCount(True), ChartElements, "Count of Cells per Category", listOfYAxis)
                objIOSChartManager.CreateChartOnTimeStamp(ChartType.Combo, SeriesType.Bar, SeriesType.Bar, 6)
                chart_Overview.Refresh()
                chart_Overview.Update()
                chart_Overview.ResumeLayout()
            Else
                SubCatageryChartsClear()
                HistogramChartsClear()
                chart_Overview.ClearAll()
                ClearAllGrid()
            End If
        Catch ex As Exception
            Dim a = ""
        Finally
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub SubCategoryCharts(ByRef chart As dotnetCHARTING.WinForms.Chart, ByVal category As String)
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Dim yaxis As New IOSAxis()
            yaxis.Orientation = Orientation.Left
            yaxis.MinimumInterval = 1
            yaxis.NumberPrecision = 0
            yaxis.ElementMarkerType = ElementMarkerType.Circle
            Dim ChartElements As New List(Of String)
            ChartElements.Add("GUIColumn")
            ChartElements.Add("Count")
            yaxis.ElementListToApply.Add("Count")
            Dim listOfYAxis As New List(Of IOSAxis)
            listOfYAxis.Add(yaxis)
            chart.SuspendLayout()
            chart.Title = "Count of cells per subcategory"
            chart.LegendBox.Visible = False
            chart.Tag = category
            Dim objIOSChartManager As New IOSChartManager(chart, Me.GetSubCategoryDataTableCount(category), ChartElements, category, listOfYAxis)
            objIOSChartManager.CreateChartOnTimeStamp(ChartType.Combo, SeriesType.Bar, SeriesType.Bar, 6)
            chart.ContextMenuStrip = cms_SubCategoryChart
            chart.Refresh()
            chart.Update()
            chart.ResumeLayout()
        Catch ex As Exception
            Dim a = ex.Message
        Finally
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub HistogramCharts(ByRef chart As dotnetCHARTING.WinForms.Chart, ByVal category As String, ByVal SubCategory As String)
        Try
            Dim distSubCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "Category='" & category & "' AND GUIColumn='" & SubCategory & "'", "", DataViewRowState.CurrentRows).ToTable(True, "ID_ICMConfig", "GUIColumn", "DBColumn", "Category", "SortBy", "IsActive")

            Dim yaxis As New IOSAxis()
            yaxis.Orientation = Orientation.Left
            yaxis.MinimumInterval = 1
            yaxis.NumberPrecision = 0
            yaxis.ElementMarkerType = ElementMarkerType.Circle
            Dim ChartElements As New List(Of String)
            If MapToSite = False Then
                ChartElements.Add("CellName")
            Else
                ChartElements.Add("UNodeBName")
            End If

            ChartElements.Add(distSubCategory(0)("DBColumn"))
            yaxis.ElementListToApply.Add(distSubCategory(0)("DBColumn"))
            Dim listOfYAxis As New List(Of IOSAxis)
            listOfYAxis.Add(yaxis)
            chart.SuspendLayout()
            chart.Title = SubCategory
            chart.LegendBox.Visible = False
            chart.Tag = SubCategory
            Dim objIOSChartManager As New IOSChartManager(chart, Me.GetSubCategoryDataTable(category, SubCategory), ChartElements, category, listOfYAxis)
            objIOSChartManager.CreateChartOnTimeStamp(ChartType.Combo, SeriesType.Bar, SeriesType.Bar, 6)
            subCategoryData.Remove(SubCategory)
            subCategoryData.Add(SubCategory, objIOSChartManager.ChartData)
            chart.ContextMenuStrip = cms_HistogramChart
            chart.Refresh()
            chart.Update()
            chart.ResumeLayout()
        Catch ex As Exception
            Dim a = ex.Message
        Finally
        End Try
    End Sub

    Private Sub HistogramChartsClear()
        For Each oTab As ICMTab In LoopAllTabs()
            If (oTab IsNot Nothing) Then
                If (oTab.ChartB IsNot Nothing) Then
                    oTab.ChartB.ClearAll()
                End If
            End If
        Next
    End Sub

    Private Sub SubCatageryChartsClear()
        For Each oTab As ICMTab In LoopAllTabs()
            If (oTab IsNot Nothing) Then
                If (oTab.ChartA IsNot Nothing) Then
                    oTab.ChartA.ClearAll()
                End If
            End If
        Next
    End Sub

#End Region

#Region "Chart_A Click Code"

    Private Sub chart_Overview_Click(sender As Object, e As EventArgs) Handles chart_Overview.Click
        Dim hit As HitTestInfo = Nothing
        Try
            hit = chart_Overview.HitTest()
        Catch ex As Exception
        End Try

        If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
            Dim el As Element = CType(hit.Object, Element)
            Dim categoryName As String = el.Name
            SelectTabByTag(GetTabPageByCategory(categoryName))
        End If
    End Sub

    Private Sub chartBL_A_Click(sender As Object, e As EventArgs)
        lblMSG.Text = ""

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim hit As HitTestInfo = Nothing
        Dim chart1 As dotnetCHARTING.WinForms.Chart = TryCast(sender, dotnetCHARTING.WinForms.Chart)
        If (chart1 IsNot Nothing) Then
            Try
                hit = chart1.HitTest(TryCast(e, MouseEventArgs).Location)
            Catch ex As Exception
            End Try
            Dim subCategoryName As String = Nothing
            '
            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                subCategoryName = el.Name
                chart1.SuspendLayout()
                Dim category As String = chart1.Tag
                BindHistogramChart(subCategoryName, category)
                chart1.RefreshChart()
                chart1.ResumeLayout()
            End If
            If (chart1.Tag IsNot Nothing) Then
                Dim gvTemp As DevExpress.XtraGrid.GridControl = GetChartGrid(GetTabPage(vtabICM, chart1.Tag.ToString.ToUpper))
                If subCategoryName IsNot Nothing Then
                    HideGridColumn(gvTemp, chart1.Tag.ToString, subCategoryName)
                Else
                    HideGridColumn(gvTemp, chart1.Tag.ToString)
                End If
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub HideGridColumn(ByRef gvTemp As DevExpress.XtraGrid.GridControl, ByVal categoryName As String, Optional ByVal subCategoryName As String = Nothing)
        Dim dt As DataTable = Me.GetIOS_ICM_Data()
        If subCategoryName IsNot Nothing Then
            If Me.dtIOS_ICM_Config Is Nothing Then
                Fill_ICM_Data()
            End If
            Dim dtCategary As DataTable = Me.dtIOS_ICM_Config.Select("GUIColumn='" & subCategoryName & "'").CopyToDataTable
            If (dtCategary.Rows.Count > 0) Then
                Dim dbColumn(3) As String
                dbColumn(0) = "CellID"
                dbColumn(1) = "CellName"
                dbColumn(2) = "UNodeBName"
                dbColumn(3) = dtCategary(0)("DBColumn").ToString
                Dim dtSubCatagory As DataTable = dt.DefaultView.ToTable(True, dbColumn)
                If gvTemp IsNot Nothing Then
                    IOSDevExpressGrid.PopulateDataInGrid(gvTemp, gvTemp.DefaultView, dtSubCatagory, "ALL")
                    'IOSDevExpressGrid.RefreshingGrid(gvTemp.DefaultView, True)
                End If
            End If
        Else
            Dim subCategory() As String = GetSubCategoryByCategory(categoryName)
            Dim dtSubCatagory As DataTable = dt.DefaultView.ToTable(True, subCategory)
            If gvTemp IsNot Nothing Then
                IOSDevExpressGrid.PopulateDataInGrid(gvTemp, gvTemp.DefaultView, dtSubCatagory, "ALL")
                'IOSDevExpressGrid.RefreshingGrid(gvTemp.DefaultView, True)
            End If
        End If
    End Sub

    Private Sub BindHistogramChart(ByVal subCategoryName As String, ByVal category As String)
        Dim selectedCategory As String = GetCategoryBySubCategory(subCategoryName, category)
        If Not (selectedCategory Is Nothing) Then
            Dim categoryName As String = selectedCategory.Split(",")(0)
            Dim shortName As String = selectedCategory.Split(",")(1)
            Me.HistogramCharts(Me.GetChartB(GetTabPage(vtabICM, shortName.ToUpper)), categoryName, subCategoryName)
        End If
    End Sub

    Private Sub SelectTabByTag(ByVal tabPage As DevExpress.XtraTab.XtraTabPage)
        Try
            vtabICM.SelectedTabPage = tabPage
        Catch ex As Exception
            Dim a = ex.Message
        Finally
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()
        End Try
    End Sub

    Private Function GetTabPageByCategory(ByVal category As String) As DevExpress.XtraTab.XtraTabPage
        Dim tabp As DevExpress.XtraTab.XtraTabPage = Nothing
        Try
            For Each tp As DevExpress.XtraTab.XtraTabPage In vtabICM.TabPages
                If (tp.Tooltip.ToLower.Trim = category.ToLower.Trim) Then
                    tabp = tp
                    Exit For
                End If
            Next
        Catch ex As Exception
            Dim a = ex.Message
        Finally
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()
        End Try
        Return tabp
    End Function

    Private Function GetCategoryBySubCategory(ByVal subCategory As String, ByVal category As String) As String
        Dim categoryName As String = Nothing
        Dim distSubCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "Category='" & category & "'", "", DataViewRowState.CurrentRows).ToTable(True, "Category", "ShortedName")
        If (distSubCategory.Rows.Count > 0) Then
            categoryName = distSubCategory.Rows(0)("Category").ToString.Trim & "," & distSubCategory.Rows(0)("ShortedName").ToString.Trim

        End If
        Return categoryName
    End Function

    Private Function GetSubCategoryFromCharA(ByRef htInfo As HitTestInfo) As String
        Dim subCategoryName As String = Nothing
        If htInfo IsNot Nothing AndAlso TypeOf (htInfo.Object) Is Element Then
            Dim el As Element = CType(htInfo.Object, Element)
            subCategoryName = el.Name
        End If
        Return subCategoryName
    End Function

#End Region

#Region "Histogram Chart Click Code"

    Private Sub chart_B_Click(sender As Object, e As EventArgs)
        lblMSG.Text = ""

        Dim hit As HitTestInfo = Nothing

        Dim chart1 As dotnetCHARTING.WinForms.Chart = TryCast(sender, dotnetCHARTING.WinForms.Chart)
        If (chart1 IsNot Nothing) Then
            Try
                hit = chart1.HitTest(TryCast(e, MouseEventArgs).Location)
            Catch ex As Exception
            End Try

            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                BindCellChartGrid(el.Name)
            End If
        End If
    End Sub

    Public Sub BindCellChartGrid(ByVal cellName As String, Optional isFromMap As Boolean = False)
        If (isFromMap) Then
            If MapToSite = False Then
                Dim selectedCellName = Me.GetIOS_ICM_Data().Select("Cellid='" & cellName & "'")
                If (selectedCellName.Length > 0) Then
                    cellName = selectedCellName(0)("CellName")
                Else
                    cellName = ""
                    ICMGridInfo.ClearAllGrid()
                End If
            Else
                Dim selectedsiteName = Me.GetIOS_ICM_Data().Select("UNodeBName='" & cellName & "'")
                If (selectedsiteName.Length > 0) Then
                    cellName = selectedsiteName(0)("UNodeBName")
                Else
                    cellName = ""
                    ICMGridInfo.ClearAllGrid()
                End If
            End If

        End If
        lblForcastStatistics.Text = "Overview Forecast statistics of clicked cell :" & cellName

        SetRecmmendation(cellName)
        Dim dataCategory As DataTable = Me.GetCategoryDataTable()
        For Each item As DataRow In dataCategory.Rows
            Dim Category As String = item("Category").ToString()
            Dim shortName As String = item("ShortedName").ToString()

            Dim data = Me.GetStatisticsData(Category, cellName)
            Dim gvTemp As DevExpress.XtraGrid.GridControl = ICMGridInfo.GetGrid(shortName.ToUpper.Trim)
            If (gvTemp IsNot Nothing) Then
                IOSDevExpressGrid.PopulateDataInGrid(gvTemp, gvTemp.DefaultView, data, "ALL")
                'IOSDevExpressGrid.RefreshingGrid(gvTemp.DefaultView, False)
            End If
        Next
    End Sub

    Private Sub SetRecmmendation(ByVal celName As String)
        Dim fltr As String = ""

        Dim cellData() As DataRow = Me.GetIOS_ICM_Data().Select(fltr)
        If (cellData.Count > 0) Then
            Dim recmmendationText As String = cellData(0)("Recommendation").ToString()
            lblRecommendation.Text = recmmendationText
        Else
            lblRecommendation.Text = ""
        End If
    End Sub

    Private Sub chartBL_B_MouseDown(sender As Object, e As MouseEventArgs)
        Dim hit As HitTestInfo = Nothing
        Dim chart1 As dotnetCHARTING.WinForms.Chart = TryCast(sender, dotnetCHARTING.WinForms.Chart)
        If (chart1 IsNot Nothing) Then
            Try
                hit = chart1.HitTest()
            Catch ex As Exception
            End Try

            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                cellName = el.Name
            End If
        End If
    End Sub

    Public Function GetStatisticsData(ByVal Category As String, ByVal cellName As String) As DataTable
        Dim fltr As String = ""
        If MapToSite = False Then
            fltr = "CellName='" & cellName & "'"
        Else
            fltr = "UNodeBName='" & cellName & "'"
        End If

        Dim data As DataTable = New DataTable()
        data.Columns.Add("Category")
        data.Columns.Add("Value")
        Dim distSubCategory As DataTable = New DataView(Me.GetIOS_ICM_Config_Data(), "Category='" & Category & "'", "", DataViewRowState.CurrentRows).ToTable(True, "ID_ICMConfig", "GUIColumn", "DBColumn", "Category")
        Dim cellData() As DataRow = Me.GetIOS_ICM_Data().Select(fltr)
        If (cellData.Length > 0) Then
            For Each subCategory As DataRow In distSubCategory.Rows
                If (cellData(0)("Comment") IsNot DBNull.Value) Then
                    txtComment.Text = cellData(0)("Comment")
                Else
                    txtComment.Text = ""
                End If

                If (cellData(0)("Approved") IsNot DBNull.Value) Then
                    chkApproved.Checked = cellData(0)("Approved")
                Else
                    chkApproved.Checked = False
                End If
                Dim row As DataRow = data.NewRow()
                row("Category") = subCategory("GUIColumn")
                Try
                    row("Value") = cellData(0)(subCategory("DBColumn"))
                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message)
                    row("Value") = 0
                End Try

                data.Rows.Add(row)
            Next
        End If
        Return data
    End Function

#End Region

#Region "Fill Grid "

    Private Sub BindGridAll()
        Dim distCategory As DataTable = Me.GetIOS_ICM_Config_Data().DefaultView.ToTable(True, "Category", "ShortedName")
        If (distCategory.Rows.Count > 0) Then
            Dim dt As DataTable = Me.GetIOS_ICM_Data()
            For Each dr As DataRow In distCategory.Rows
                Dim categoryName As String = dr("Category")
                Dim shortedName As String = dr("ShortedName")
                Dim subCategory() As String = GetSubCategoryByCategory(categoryName)
                If (subCategory.Length > 0 AndAlso subCategory IsNot Nothing) Then
                    Dim dtSubCatagory As DataTable = Nothing
                    Try
                        dtSubCatagory = dt.DefaultView.ToTable(True, subCategory)

                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message)
                        Continue For
                    End Try
                    If (dtSubCatagory IsNot Nothing) Then
                        Dim gvTemp As DevExpress.XtraGrid.GridControl = GetChartGrid(GetTabPage(vtabICM, shortedName.ToUpper))
                        If (gvTemp IsNot Nothing) Then
                            gvTemp.ContextMenuStrip = cms_HistogramChart
                            IOSDevExpressGrid.PopulateDataInGrid(gvTemp, gvTemp.DefaultView, dtSubCatagory, "ALL")
                            'IOSDevExpressGrid.RefreshingGrid(gvTemp.DefaultView, True)
                        End If
                    End If
                End If
            Next
        End If
    End Sub

#End Region

#Region "Grid All Click Code"

    Private Sub vdgvBL_All_MouseDown(sender As Object, e As MouseEventArgs)
        If (e.Button = MouseButtons.Right) Then
            Dim gvtemp As DevExpress.XtraGrid.GridControl = TryCast(sender, DevExpress.XtraGrid.GridControl)
            If (gvtemp IsNot Nothing) Then
                Dim gVIew As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(gvtemp.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView)
                Dim baseHI As BaseHitInfo = gVIew.CalcHitInfo(e.Location)
                Dim gridHI As GridHitInfo = TryCast(baseHI, GridHitInfo)

                If (gVIew IsNot Nothing) Then
                    gVIew.SelectRow(gridHI.RowHandle)
                    cellName = gridHI.Column.FieldName
                Else
                    gvtemp.ContextMenuStrip.Hide()
                End If
            End If
        End If
    End Sub

    Private Sub dgvCellChart_CellMouseClick(ByVal sender As System.Object, ByVal args As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs)
        Try
            Dim cellName As String = args.CellValue.ToString()
            BindCellChartGrid(cellName)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Feedback Code"

    Private Sub btnFeedbackSave_Click(sender As Object, e As EventArgs) Handles btnFeedbackSave.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (Not String.IsNullOrEmpty(Me.cellName)) Then
                If (Not String.IsNullOrEmpty(txtComment.Text)) Then
                    ''Dim sqlCMD As String = "UPDATE IOS_ICM SET Comment='" & txtComment.Text.Trim & "', Approved='" & chkApproved.Checked & "' WHERE cellName='" & Me.cellName & "'"
                    IOS.DataLibrary.clsSQLCommands.SaveFeedback(connStrIOSServer, txtComment.Text.Trim, IIf(chkApproved.Checked, "True", "False"), Me.cellName)
                    lblMSG.Text = "Feedback is updated."
                    Me.Fill_ICM_Data()
                Else
                    lblMSG.Text = "Enter comment text."
                End If
            Else
                lblMSG.Text = "Cell Name is not avilable."
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Context Menu Code"

    Private Sub childMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_SendToMapAllHeatMap.CheckedChanged
        contextFlag = False
    End Sub

    Private Sub cms_HistogramChart_Closing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripDropDownClosingEventArgs) Handles cms_HistogramChart.Closing
        e.Cancel = Not contextFlag
        contextFlag = True
    End Sub

    Private Sub cms_HistogramChart_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_HistogramChart.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            cm_SourceControl = cmsTemp.SourceControl
            Dim data As DataTable
            Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cmsTemp.SourceControl, dotnetCHARTING.WinForms.Chart)

            If (tempChart Is Nothing) Then
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
                clickBy = EnumChartGridClick.FromGrid
            Else
                data = subCategoryData.Item(tempChart.Tag.ToString())
                clickBy = EnumChartGridClick.FromChart
            End If

            tsmi_ObjectCount.Text = "Object Count: " & data.Rows.Count
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_sendconsoletree_Click(sender As Object, e As EventArgs) Handles tsmi_sendconsoletree.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim data As DataTable
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
            End If

            If (data IsNot Nothing AndAlso tech IsNot Nothing AndAlso cellName IsNot Nothing) Then
                Dim dt As DataTable = data.Copy()
                Dim rows() As DataRow = dt.Select("CellName='" & cellName & "'")
                If (rows.Count > 0) Then
                    SendToConsoleTree(tech, rows.CopyToDataTable().DefaultView.ToTable(True, "Cellid").AsEnumerable().ToArray())
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
            Dim data As DataTable
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
            End If

            If (data IsNot Nothing AndAlso data IsNot Nothing) Then
                Dim dt As DataTable = data.Copy()
                SendToConsoleTree(tech, dt.DefaultView.ToTable(True, "Cellid").AsEnumerable().ToArray())
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SendToMapAllGraduatedTheme_Click(sender As Object, e As EventArgs) Handles tsmi_SendToMapAllGraduatedTheme.Click, tsmi_SendToMapAllRangedTheme.Click
        Try
            Dim control As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            If (control IsNot Nothing) Then
                If (control.Text.ToLower.Contains("ranged")) Then
                    TopXMapType = 0
                Else
                    TopXMapType = 1
                End If
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            cms_HistogramChart.Visible = False
            contextFlag = False
            Dim data As DataTable
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
                Dim isColumnExit As Boolean = False
                For Each dtcol As DataColumn In data.Columns
                    If (dtcol.ColumnName.ToUpper = "VALUE") Then
                        isColumnExit = True
                    End If
                Next

                If (isColumnExit = False) Then
                    Dim newColumn As DataColumn = New DataColumn
                    With newColumn
                        .ColumnName = "Value"
                        .DataType = GetType(Integer)
                        .DefaultValue = 5
                    End With
                    data.Columns.Add(newColumn)
                End If
            End If

            If (data IsNot Nothing AndAlso data IsNot Nothing) Then
                Dim dt As DataTable = data.Copy()
                SendToMap_ICM(dt.AsEnumerable().ToArray(), dt, False, tsmi_SendToMapAllGeoAggregationFunction.Text.ToLower.Trim, False)
            End If
            TopXMapType = 0
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Public Sub SendToConsoleTree(ByVal tech As String, ByVal rows() As DataRow)
        Dim techn As String = Me.GetTechnologyName(tech, vendor, "Tech")
        frmMapWindow.SelectionToTreeStep1(techn, rows.Count, False, New IOS.Library.SelectionToTreeFlags())
        For Each item As DataRow In rows
            frmMapWindow.SelectionToTreeStep2(techn, item("cellid").ToString(), False)
        Next
    End Sub

    Private Function GetTechnologyName(ByVal tech As String, ByVal vendor As String, ByVal returnObjectColumnsName As String) As String
        Dim rows() As DataRow = dt_IOS_ObjectConfig.Select("Vendor='" & vendor & "' AND Technology='" & tech & "' AND ParamHistory=1")
        If (rows.Count > 0) Then
            Return rows(0)(returnObjectColumnsName).ToString
        End If
        Return ""
    End Function

    Private Sub SendToMap_ICM(ByVal rows() As DataRow, ByVal originalTable As DataTable, ByVal isEnableGeoAggregation As Boolean, ByVal agregateFunction As String, ByVal isHeatMap As Boolean, Optional ByVal dtThemBins As DataTable = Nothing)
        Dim objectName As String = String.Empty
        If (cmbTargetObject.SelectedIndex > 0) Then
            objectName = cmbTargetObject.SelectedItem.ToString
        Else
            objectName = Me.GetTechnologyName(tech, vendor, "Object")
        End If
        Dim dt_filtered As DataTable = rows.CopyToDataTable.Clone()
        dt_filtered.TableName = originalTable.TableName
        If MapToSite = False Then
            dt_filtered.Columns("Cellid").DataType = GetType(String)
            dt_filtered.Columns("Cellid").ColumnName = objectName
        Else
            dt_filtered.Columns("UNodeBName").DataType = GetType(String)
            dt_filtered.Columns("UNodeBName").ColumnName = "UNODEB"
        End If

        dt_filtered.Columns(originalTable.TableName).DataType = GetType(Double)
        dt_filtered.Columns(originalTable.TableName).ColumnName = originalTable.TableName

        For Each item As DataRow In rows
            Dim r As DataRow = dt_filtered.NewRow()
            Dim index As Integer = 0
            For Each item1 As DataColumn In originalTable.Columns
                r(index) = item(index)
                index = index + 1
            Next
            dt_filtered.Rows.Add(r)
        Next

        Dim currentview As DRect = MapInfo.Engine.Session.Current.MapFactory(0).Bounds

        'construct filter
        Dim techn As String = Me.GetTechnologyName(tech, vendor, "Tech")

        SendToMap(techn, originalTable.TableName, dt_filtered, TopXMapType, EnumSendToMap.ICMFromCategory, dtThemBins, Nothing, MapToVoronoi, MapToSite)

        Dim zoomAfterSetView As MapInfo.Geometry.Distance = MapInfo.Engine.Session.Current.MapFactory(0).Zoom


        Dim layerName As String = "ICM_" & Replace(techn, " ", "_") & "_" & originalTable.TableName & "_Map"
        Dim geoData As New GeoAggregationData(layerName, originalTable.TableName, agregateFunction)
        geoData.ZoomRange.Add("", New MapInfo.Mapping.VisibleRange(0, True, 900, True, MapInfo.Geometry.DistanceUnit.Kilometer))

        If (isEnableGeoAggregation) Then
            geoData.GeoDataType = GeoDataType.GeoAggregation
            GeoAggregationManager.RemoveByTableNameAndType(geoData)
            GeoAggregationManager.Add(geoData)
        Else
            geoData.GeoDataType = GeoDataType.GeoAggregation
            GeoAggregationManager.RemoveByTableNameAndType(geoData)
        End If
        Dim geoHeatMapData As New GeoAggregationData(layerName, originalTable.TableName, agregateFunction)
        If (isHeatMap) Then
            geoHeatMapData.GeoDataType = GeoDataType.HeatMap
            GeoAggregationManager.RemoveByTableNameAndType(geoHeatMapData)
            GeoAggregationManager.Add(geoHeatMapData)
        Else
            geoHeatMapData.GeoDataType = GeoDataType.HeatMap
            GeoAggregationManager.RemoveByTableNameAndType(geoHeatMapData)
        End If
        If (isEnableGeoAggregation) Then
            Try
                Dim lyr As MapInfo.Mapping.ObjectThemeLayer = CType(frmMapWindow.MapControl1.Map.Layers(0), MapInfo.Mapping.ObjectThemeLayer)
                If Not lyr Is Nothing And lyr.Name.ToString.Contains("ICM_Map") Then
                    lyr.Enabled = False
                End If
            Catch ex As Exception
                Dim a As String = ""
            End Try
            TopXMapType = 0
            MapInfo.Engine.Session.Current.MapFactory(0).SetView(currentview, csysWGS84)
            GeoAggregationManager.GenerateGeoAggregation(AddressOf frmMapWindow.GenerateGeoAggregation, GeoDataType.GeoAggregation)
        End If
        If (isHeatMap) Then
            Try
                Dim lyr As MapInfo.Mapping.ObjectThemeLayer = CType(frmMapWindow.MapControl1.Map.Layers(0), MapInfo.Mapping.ObjectThemeLayer)
                If Not lyr Is Nothing And lyr.Name.ToString.Contains("ICM_Map") Then
                    lyr.Enabled = False
                End If

            Catch ex As Exception

            End Try
            TopXMapType = 0
            MapInfo.Engine.Session.Current.MapFactory(0).SetView(currentview, csysWGS84)
            GeoAggregationManager.GenerateGeoAggregation(AddressOf frmMapWindow.CreateGridFromFeatures, GeoDataType.HeatMap)
        End If
        dt_filtered.Dispose()
        dt_filtered = Nothing
    End Sub

    Private Sub cms_SubCategoryChart_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_SubCategoryChart.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            cm_SourceControlSubCatagory = cmsTemp.SourceControl
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_HideAndShowGridSubCategory_Click(sender As Object, e As EventArgs) Handles tsmi_HideAndShowGridSubCategory.Click
        Dim tempSplitCont As System.Windows.Forms.SplitContainer = GetSplitControl(cm_SourceControlSubCatagory)
        If (tempSplitCont IsNot Nothing) Then
            If (tempSplitCont.Panel2Collapsed.Equals(True)) Then
                tempSplitCont.Panel2Collapsed = False
            Else
                tempSplitCont.Panel2Collapsed = True
            End If
        End If
    End Sub

    Private Sub tsmi_HideAndShowGrid_Click(sender As Object, e As EventArgs) Handles tsmi_HideAndShowGrid.Click
        Dim tempSplitCont As System.Windows.Forms.SplitContainer = GetSplitControl(cm_SourceControl)
        If (tempSplitCont IsNot Nothing) Then
            If (tempSplitCont.Panel2Collapsed.Equals(True)) Then
                tempSplitCont.Panel2Collapsed = False
            Else
                tempSplitCont.Panel2Collapsed = True
            End If
        End If
    End Sub

    Private Function GetSplitControl(ByRef tempControl As Control) As System.Windows.Forms.SplitContainer
        If (tempControl.Parent IsNot Nothing) Then
            If tempControl.Parent.GetType() Is GetType(SplitContainer) Then
                Return tempControl.Parent
            Else
                Return GetSplitControl(tempControl.Parent)
            End If
        Else
            Return Nothing
        End If
    End Function

    Private Function GetValidColumnsName(ByVal column As String) As String
        Dim temp As String = New String(column.TakeWhile(Function(w) Char.IsNumber(w)).ToArray())
        If (temp.Length > 0) Then
            column = "_" + column
        End If
        Dim s As String = column
        If (column.Length > 30) Then
            s = column.Replace("_", "")

            If (temp.Length > 0) Then
                s = "_" + s
            End If

            If (s.Length > 30) Then
                s = column.Substring(0, 30)
            End If
        End If
        Return s
    End Function

    Private Sub tsmi_MapAllWithThematic_Click(sender As Object, e As EventArgs) Handles tsmi_MapAllWithThematic.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Try
            Dim techn As String = Me.GetTechnologyName(tech, vendor, "Tech")
            Dim distCategory As DataTable = Me.GetIOS_ICM_Config_Data().DefaultView.ToTable(True, "Category")
            For Each drCategory As DataRow In distCategory.Rows
                Dim category As String = drCategory("Category").ToString.Trim
                Dim dtSubCategory As DataTable = GetCategoryData(category)
                For Each dc As DataColumn In dtSubCategory.Columns
                    dc.ColumnName = GetValidColumnsName(dc.ColumnName)
                Next
                If (dtSubCategory IsNot Nothing) Then
                    SendToMap(techn, category.Replace(" ", "_"), dtSubCategory, TopXMapType, EnumSendToMap.ICMFromOverview)
                End If
            Next
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Function GetCategoryData(categoryName) As DataTable
        Dim dt As DataTable = Me.GetIOS_ICM_Data()
        Dim subCategory() As String = GetSubCategoryByCategory(categoryName)
        Dim dtSubCatagory As DataTable = Nothing
        dtSubCatagory = dt.DefaultView.ToTable(True, subCategory)
        Dim newColumn As DataColumn = New DataColumn
        With newColumn
            .ColumnName = "TempThemValue"
            .DataType = GetType(Integer)
            .DefaultValue = 5
        End With
        dtSubCatagory.Columns.Add(newColumn)
        Return dtSubCatagory
    End Function

    Private Sub cms_OverviewChart_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_OverviewChart.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            cm_SourceControlOverView = cmsTemp.SourceControl
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_ShowHideForecastSubCategoryChart_Click(sender As Object, e As EventArgs) Handles tsmi_ShowHideForecastSubCategoryChart.Click, tsmi_ShowHideForecastHistogramChart.Click, tsmi_ShowHideOverviewForecast.Click
        If (spltConAllChartGrid.Panel2Collapsed.Equals(True)) Then
            spltConAllChartGrid.Panel2Collapsed = False
        Else
            spltConAllChartGrid.Panel2Collapsed = True
        End If
    End Sub

#End Region

#Region "Recommendation"

    Private Sub vbtnSaveRecommendation_Click(sender As Object, e As EventArgs)
        If Not (String.IsNullOrEmpty(lblRecommendation.Text) AndAlso cellName IsNot Nothing) Then
            IOS.DataLibrary.clsSQLCommands.UpdateRecommendation(connStrIOSServer, lblRecommendation.Text.Trim, cellName)
            lblMSG.Text = "Cell is updated."
            lblMSG.Visible = True
            Fill_ICM_Data()
        End If
    End Sub

#End Region

#Region "Form & Controls' Events"

    Private Sub cmbReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReport.SelectedIndexChanged

        If cmbReport.SelectedIndex > 0 Then
            MapToSite = False
            If cmbReport.SelectedItem.ToString.Contains("_SITE") Then
                MapToSite = True
            End If
        End If

        If cmbReport.SelectedIndex > 0 AndAlso cmbTechnology.SelectedIndex > 0 Then
            Reload()
        Else
            cmbTechnology.SelectedIndex = 0
            SubCatageryChartsClear()
            HistogramChartsClear()
            chart_Overview.ClearAll()
            ClearAllGrid()
        End If
    End Sub

    Private Sub vchkFilterCriteriaCombine_CheckedChanged(sender As Object, e As EventArgs) Handles chkFilterCriteriaCombine.CheckedChanged
        Me.Fill_ICM_Data()
        Me.Reload()
    End Sub

    Private Sub vtlv_Filters_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles tlvFilters.MouseDoubleClick
        Try
            If (tlvFilters.SelectedNode IsNot Nothing) Then
                Dim treeSubnode = tlvFilters.GetSubItem(tlvFilters.SelectedNode, e.Location)
                If (treeSubnode IsNot Nothing) Then
                    Dim editSubnode As LidorSystems.IntegralUI.Lists.TreeListViewSubItem = TryCast(treeSubnode, LidorSystems.IntegralUI.Lists.TreeListViewSubItem)
                    If (editSubnode IsNot Nothing) Then
                        If (editSubnode.Index = 1 Or editSubnode.Index = 2) Then
                            editSubnode.BeginEdit()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub vtlv_Filters_AfterLabelEdit(sender As Object, e As LidorSystems.IntegralUI.ObjectEditEventArgs) Handles tlvFilters.AfterLabelEdit
        Dim tlvFilter As New LidorSystems.IntegralUI.Lists.TreeListViewSubItem
        tlvFilter = e.Object
        Dim ICMFilterID As String
        Dim sql As String = Nothing
        Dim sqlUpdate As String = Nothing
        Try
            If (e.Label IsNot Nothing AndAlso tlvFilter IsNot Nothing) Then
                If Not (e.Label = "") AndAlso Not e.Label = tlvFilter.Text Then
                    ICMFilterID = tlvFilter.Parent.Tag
                    If (tlvFilter.Name = "KPIValue") Then
                        sqlUpdate = "ICM_Value ='" & e.Label & "'"
                    ElseIf (tlvFilter.Name = "KPIOperator") Then
                        If (IsOperator(e.Label)) Then
                            sqlUpdate = "ICM_Operator ='" & e.Label & "'"
                        End If
                    End If
                    If (sqlUpdate IsNot Nothing) Then
                        ''sql = "UPDATE IOS_ICM_Filters SET " & sqlUpdate & " WHERE ICMFilterID =" & ICMFilterID
                        IOS.DataLibrary.clsSQLCommands.UpdateIcmFilters(connStrIOSServer, sqlUpdate, ICMFilterID)
                        TreeListView_Update(tech)
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        e.Cancel = True
    End Sub

    Private Function IsOperator(ByVal KPIOperator As String) As Boolean
        Dim isOp As Boolean = False
        For Each opItem As clsComboBoxItem In cmbFilterOp.Properties.Items
            If opItem.Text = KPIOperator Then
                isOp = True
                Exit For
            End If
        Next
        Return isOp
    End Function

    Private Sub tsmi_SendToMapSelect_Click(sender As Object, e As EventArgs) Handles tsmi_SendToMapSelect.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim cellID As String = Nothing
            Dim data As DataTable
            'contextFlag = True
            cms_HistogramChart.Visible = False
            Dim rows() As DataRow
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
                If (data.Rows.Count > 0 AndAlso data IsNot Nothing) Then
                    If MapToSite = False Then
                        rows = data.Select("CellName='" & Me.cellName & "'")
                        If (rows.Length > 0) Then
                            cellID = rows(0)("cellID").ToString
                        End If
                    Else
                        rows = data.Select("UNodeBName='" & Me.cellName & "'")
                        If (rows.Length > 0) Then
                            cellID = rows(0)("UNodeBName").ToString
                        End If

                    End If

                End If
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                Dim dat As DataTable = New DataTable()
                dat.Columns.Add("CellID")
                dat.Columns.Add("CellName")
                dat.Columns.Add("UNodeBName")
                dat.Columns.Add("Value")

                Dim gView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(gvTemp.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView)
                Dim rowIndex() As Integer = gView.GetSelectedRows()
                If (rowIndex.Length > 0) Then
                    For a = 0 To rowIndex.Length - 1
                        Dim dr As DataRow = TryCast(gView.GetRow(a), DataRowView).Row
                        Dim celId As String = dr.Item(0).ToString
                        Dim cellName As String = dr.Item(1).ToString
                        Dim UNodeBName As String = dr.Item(2).ToString
                        dat.Rows.Add(celId, cellName, UNodeBName, "5")
                        If MapToSite = False Then
                            cellID = celId
                        Else
                            cellID = UNodeBName
                        End If
                    Next
                    data = dat.Copy()
                End If
            End If
            If (cellID IsNot Nothing) Then
                If MapToSite = False Then
                    frmMapWindow.Cell_SearchAndDisplay(cellID, Nothing, "CELLID")
                Else
                    frmMapWindow.Cell_SearchAndDisplay(cellID, Nothing, "SITECODE")
                End If

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub tsmi_SendToMapAllHeatMap_Click(sender As Object, e As EventArgs) Handles tsmi_SendToMapAllHeatMap.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            cms_HistogramChart.Visible = False
            contextFlag = False
            Dim data As DataTable
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
                Dim isColumnExit As Boolean = False
                For Each dtcol As DataColumn In data.Columns
                    If (dtcol.ColumnName.ToUpper = "VALUE") Then
                        isColumnExit = True
                    End If
                Next

                If (isColumnExit = False) Then
                    Dim newColumn As DataColumn = New DataColumn
                    With newColumn
                        .ColumnName = "Value"
                        .DataType = GetType(Integer)
                        .DefaultValue = 5
                    End With
                    data.Columns.Add(newColumn)
                End If
            End If

            If (data IsNot Nothing AndAlso data IsNot Nothing) Then
                Dim dt As DataTable = data.Copy()
                SendToMap_ICM(dt.AsEnumerable().ToArray(), dt, False, tsmi_SendToMapAllGeoAggregationFunction.Text.ToLower.Trim, True)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tsmi_SendToMapAllGeoAggregation_Click(sender As Object, e As EventArgs) Handles tsmi_SendToMapAllGeoAggregation.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            cms_HistogramChart.Visible = False
            contextFlag = False
            Dim data As DataTable
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
                Dim isColumnExit As Boolean = False
                For Each dtcol As DataColumn In data.Columns
                    If (dtcol.ColumnName.ToUpper = "VALUE") Then
                        isColumnExit = True
                    End If
                Next

                If (isColumnExit = False) Then
                    Dim newColumn As DataColumn = New DataColumn
                    With newColumn
                        .ColumnName = "Value"
                        .DataType = GetType(Integer)
                        .DefaultValue = 5
                    End With
                    data.Columns.Add(newColumn)
                End If
            End If

            If (data IsNot Nothing AndAlso data IsNot Nothing) Then
                Dim dt As DataTable = data.Copy()
                SendToMap_ICM(dt.AsEnumerable().ToArray(), dt, True, tsmi_SendToMapAllGeoAggregationFunction.Text.ToLower.Trim, False)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tsmi_UsingPreconfigured_Click(sender As Object, e As EventArgs) Handles tsmi_UsingPreconfigured.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            dtTheamaticBins = IOSThematicKPI.GetThenaticBins(connStrIOSServer, IOSKPIType.ICMKPI)
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            cms_HistogramChart.Visible = False
            contextFlag = False
            Dim selectedKPIName As String = ""
            Dim data As DataTable
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
                selectedKPIName = data.TableName
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
                Dim isColumnExit As Boolean = False
                For Each dtcol As DataColumn In data.Columns
                    If (dtcol.ColumnName.ToUpper = "VALUE") Then
                        isColumnExit = True
                    End If
                Next

                If (isColumnExit = False) Then
                    Dim newColumn As DataColumn = New DataColumn
                    With newColumn
                        .ColumnName = "Value"
                        .DataType = GetType(Integer)
                        .DefaultValue = 5
                    End With
                    data.Columns.Add(newColumn)
                End If
            End If

            Dim dt As DataTable
            If (data IsNot Nothing AndAlso data IsNot Nothing) Then
                dt = data.Copy()

                Dim drThematic() As DataRow
                Dim dtThematicTmp As DataTable
                If (dtTheamaticBins IsNot Nothing) Then
                    drThematic = dtTheamaticBins.Select("KPI_Name='" & data.TableName & "'")
                    If (drThematic.Count > 0) Then
                        dtThematicTmp = drThematic.CopyToDataTable()
                        Dim distSubCategory As DataTable
                        If (dtThematicTmp.Rows.Count > 0) Then
                            If (dtThematicTmp.Rows(0)(IOSThematicKeys.THEMATIC_TYPE).ToString.ToUpper = "PIETHEME") Then

                                Dim dtKPISets As DataTable = New DataView(dtTheamaticBins, IOSThematicKeys.OBJECT_NAME & "='" & dtThematicTmp.Rows(0)(IOSThematicKeys.OBJECT_NAME).ToString & "'", "", DataViewRowState.CurrentRows).ToTable(True, IOSThematicKeys.KPI_ID)
                                If (dtKPISets.Rows.Count > 0) Then
                                    Dim myColumn = (From row In dtKPISets.AsEnumerable() Select row.Field(Of Integer)("KPI_ID")).Distinct().ToList()
                                    If (myColumn.Count > 0) Then
                                        Dim dts2 = dtTheamaticBins.AsEnumerable.Where(Function(r) myColumn.Contains(r.Field(Of Integer)("KPI_ID") And r.Field(Of String)(IOSThematicKeys.THEMATIC_TYPE) = "PieTheme")).ToList()
                                        If (dts2.Count > 0) Then
                                            Dim dts As DataTable = dts2.CopyToDataTable()
                                            distSubCategory = New DataView(dts, IOSThematicKeys.THEMATIC_TYPE & "='PieTheme'", "", DataViewRowState.CurrentRows).ToTable(True, IOSThematicKeys.KPI_ID, "KPI_Name")
                                            dtThematicTmp = dts.Copy()
                                        Else
                                            distSubCategory = New DataView(dtThematicTmp, IOSThematicKeys.THEMATIC_TYPE & "='PieTheme'", "", DataViewRowState.CurrentRows).ToTable(True, IOSThematicKeys.KPI_ID, "KPI_Name")
                                        End If
                                    Else
                                        distSubCategory = New DataView(dtThematicTmp, IOSThematicKeys.THEMATIC_TYPE & "='PieTheme'", "", DataViewRowState.CurrentRows).ToTable(True, IOSThematicKeys.KPI_ID, "KPI_Name")
                                    End If
                                Else
                                    distSubCategory = New DataView(dtThematicTmp, IOSThematicKeys.THEMATIC_TYPE & "='PieTheme'", "", DataViewRowState.CurrentRows).ToTable(True, IOSThematicKeys.KPI_ID, "KPI_Name")
                                End If

                                Dim subCat(distSubCategory.Rows.Count + 2) As String
                                subCat(0) = "Cellid"
                                subCat(1) = "CellName"
                                subCat(2) = "UNodeBName"
                                If (distSubCategory.Rows.Count > 0) Then
                                    For value As Integer = 3 To distSubCategory.Rows.Count + 2
                                        Dim asd As String = distSubCategory.Rows(value - 3)("KPI_Name").ToString()
                                        subCat(value) = asd
                                    Next
                                End If
                                Dim dtICM As DataTable = Me.GetIOS_ICM_Data()
                                Dim dtSubCatagory As DataTable = dtICM.DefaultView.ToTable(True, subCat)
                                dt = dtSubCatagory.Copy()
                            End If
                        End If
                    Else
                        dtThematicTmp = IOSThematicKPI.GetDefaultThenaticBins(connStrIOSServer, IOSKPIThemeType.PIE)
                    End If
                    Dim subCategory(dt.Columns.Count - 1) As String
                    Dim rowvalue As Integer = 0
                    For Each colName As DataColumn In dt.Columns
                        subCategory(rowvalue) = colName.ColumnName
                        rowvalue += 1
                    Next

                    Dim distCategory As DataTable = Me.GetIOS_ICM_Config_Data().Select("DBColumn='" & subCategory(3) & "'").CopyToDataTable().DefaultView.ToTable(True, "DBColumn", "Category")
                    dt.TableName = distCategory.Rows(0)("Category")
                    If (Not dtThematicTmp Is Nothing) Then
                        If (dtThematicTmp.Rows.Count > 0) Then
                            SendToMap_Preconfigured(dt.AsEnumerable().ToArray(), dt, False, tsmi_UsingPreconfigured.Text.ToLower.Trim, False, subCategory, dtThematicTmp)
                        Else
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - Thimatic bins not found.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SendToMap_Preconfigured(ByVal rows() As DataRow, ByVal originalTable As DataTable, ByVal isEnableGeoAggregation As Boolean, ByVal agregateFunction As String, ByVal isHeatMap As Boolean, ByVal subCategory() As String, Optional ByVal dtThemBins As DataTable = Nothing)
        Dim objectName As String = String.Empty
        If (cmbTargetObject.SelectedIndex > 0) Then
            objectName = cmbTargetObject.SelectedItem.ToString
        Else
            objectName = Me.GetTechnologyName(tech, vendor, "Object")
        End If
        Dim dt_filtered As DataTable = rows.CopyToDataTable.Clone()
        If MapToSite = False Then
            dt_filtered.Columns("Cellid").DataType = GetType(String)
            dt_filtered.Columns("Cellid").ColumnName = objectName
        Else
            dt_filtered.Columns("UNodeBName").DataType = GetType(String)
            dt_filtered.Columns("UNodeBName").ColumnName = "UNODEB"
        End If

        Dim mappingColumns(subCategory.Length - 4) As String
        Dim rowCount As Integer = 0
        For Each colName As String In subCategory
            If (Not (colName.ToLower = "cellname" Or colName.ToLower = "cellid" Or colName.ToLower = "unodebname")) Then
                dt_filtered.Columns(colName).DataType = GetType(Double)
                mappingColumns(rowCount) = colName
                rowCount += 1
            End If
        Next

        For Each item As DataRow In rows
            Dim r As DataRow = dt_filtered.NewRow()
            Dim index As Integer = 0
            For Each item1 As DataColumn In originalTable.Columns
                r(index) = item(index)
                index = index + 1
            Next
            dt_filtered.Rows.Add(r)
        Next

        Dim currentview As DRect = MapInfo.Engine.Session.Current.MapFactory(0).Bounds

        'construct filter
        Dim techn As String = Me.GetTechnologyName(tech, vendor, "Tech")
        Dim themExpre As String = IIf(mappingColumns.Length = 1, mappingColumns(0), mappingColumns(0))
        SendToMap(techn, themExpre, dt_filtered, TopXMapType, EnumSendToMap.ICMFromPreconfigured, dtThemBins, mappingColumns, MapToVoronoi, MapToSite)

        dt_filtered.Dispose()
        dt_filtered = Nothing
    End Sub

    Private Sub tsmi_UsingPieTheme_Click(sender As Object, e As EventArgs) Handles tsmi_UsingPieTheme.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            cms_HistogramChart.Visible = False
            Dim selectedKPIName As String = ""

            Dim data As DataTable
            If (clickBy = EnumChartGridClick.FromChart) Then
                Dim tempChart As dotnetCHARTING.WinForms.Chart = TryCast(cm_SourceControl, dotnetCHARTING.WinForms.Chart)
                data = subCategoryData.Item(tempChart.Tag.ToString())
                selectedKPIName = data.TableName
            Else
                Dim gvTemp As DevExpress.XtraGrid.GridControl = TryCast(cm_SourceControl, DevExpress.XtraGrid.GridControl)
                data = TryCast(gvTemp.DataSource, DataTable)
                Dim isColumnExit As Boolean = False
                For Each dtcol As DataColumn In data.Columns
                    If (dtcol.ColumnName.ToUpper = "VALUE") Then
                        isColumnExit = True
                    End If
                Next

                If (isColumnExit = False) Then
                    Dim newColumn As DataColumn = New DataColumn
                    With newColumn
                        .ColumnName = "Value"
                        .DataType = GetType(Integer)
                        .DefaultValue = 5
                    End With
                    data.Columns.Add(newColumn)
                End If
            End If
            Dim selectedListBoxKPI As List(Of String) = New List(Of String)
            selectedListBoxKPI = GetKPIFromListBox(selectedKPIName)
            If (selectedListBoxKPI.Count >= 1) Then
                Dim dtThematicTmp As DataTable = IOSThematicKPI.GetDefaultThenaticBins(connStrIOSServer, IOSKPIThemeType.PIE)
                Dim dt As DataTable
                If (data IsNot Nothing AndAlso data IsNot Nothing) Then
                    dt = data.Copy()

                    'Dim dtKPIListBox As DataTable
                    If (dtIOS_ICM Is Nothing) Then
                        Me.Fill_ICM_Data()
                    End If

                    Dim filtterKPI As String = ""
                    Dim itemIndex As Integer = 0
                    For Each item As String In selectedListBoxKPI
                        If (selectedListBoxKPI.Count - 1 = itemIndex) Then
                            filtterKPI = filtterKPI & "DBColumn='" & item & "'"
                        Else
                            filtterKPI = filtterKPI & "DBColumn='" & item & "' OR "
                        End If
                        itemIndex = itemIndex + 1
                    Next

                    Dim dtICMKPI As DataTable = Me.dtIOS_ICM_Config.DefaultView.ToTable(True, "ID_ICMConfig", "DBColumn").Select(filtterKPI).CopyToDataTable()
                    'Dim distSubCategory As DataTable
                    Dim subCat(selectedListBoxKPI.Count + 2) As String
                    subCat(0) = "Cellid"
                    subCat(1) = "CellName"
                    subCat(2) = "UNodeBName"
                    If (selectedListBoxKPI.Count > 0) Then
                        For value As Integer = 3 To selectedListBoxKPI.Count + 2
                            Dim asd As String = selectedListBoxKPI(value - 3).ToString()
                            subCat(value) = asd
                        Next
                    End If
                    Dim dtICM As DataTable = Me.GetIOS_ICM_Data()
                    Dim dtSubCatagory As DataTable = dtICM.DefaultView.ToTable(True, subCat)
                    dt = dtSubCatagory.Copy()

                    Dim subCategory(dt.Columns.Count - 1) As String
                    Dim rowvalue As Integer = 0
                    For Each colName As DataColumn In dt.Columns
                        subCategory(rowvalue) = colName.ColumnName
                        rowvalue += 1
                    Next

                    Dim distCategory As DataTable = Me.GetIOS_ICM_Config_Data().Select("DBColumn='" & subCategory(3) & "'").CopyToDataTable().DefaultView.ToTable(True, "DBColumn", "Category")
                    dt.TableName = distCategory.Rows(0)("Category")
                    If (Not dtThematicTmp Is Nothing) Then
                        If (dtThematicTmp.Rows.Count > 0) Then
                            SendToMap_Preconfigured(dt.AsEnumerable().ToArray(), dt, False, tsmi_UsingPreconfigured.Text.ToLower.Trim, False, subCategory, dtThematicTmp)
                        Else
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - Thimatic bins not found.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub SetComboValue(ByVal selectItem As String, ByRef comb As DevExpress.XtraEditors.ComboBoxEdit, Optional ByVal IsNeedToRefresh As Boolean = False)
        If (comb.EditValue IsNot Nothing AndAlso Not IsNeedToRefresh) Then
            If (comb.SelectedItem.ToString.ToUpper = selectItem.ToUpper) Then
                Exit Sub
            End If
        End If
        For Each opItem As clsComboBoxItem In comb.Properties.Items
            If opItem.Text.ToUpper = selectItem.ToUpper Then
                comb.SelectedItem = opItem
                Exit For
            End If
        Next
    End Sub

    Public Sub SelectTreeNode(ByVal ndCellID As String, Optional ByVal follow As Boolean = True)
        Try
            Dim tn() As TreeNode = tvObjectTree.Nodes.Find(ndCellID, True)
            Dim i As Integer = 0
            For i = 0 To tn.Length - 1
                If tn(i).Checked = False Then
                    tn(i).Checked = True
                    If follow = True Then
                        tvObjectTree.SelectedNode = tn(i)

                    End If
                End If
            Next i
        Catch
            'do nothing
        End Try
    End Sub

    Private Sub CirclePresentationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CirclePresentationToolStripMenuItem.Click
        If CirclePresentationToolStripMenuItem.Checked = False Then
            BufferPresentationToolStripMenuItem.Checked = False
            CirclePresentationToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub BufferPresentationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BufferPresentationToolStripMenuItem.Click
        If BufferPresentationToolStripMenuItem.Checked = False Then
            CirclePresentationToolStripMenuItem.Checked = False
            BufferPresentationToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub tsmi_EnableVoronoi_Click(sender As Object, e As EventArgs) Handles tsmi_EnableVoronoi.Click
        If tsmi_EnableVoronoi.CheckState = CheckState.Checked Then
            MapToVoronoi = True
        Else
            MapToVoronoi = False
        End If
    End Sub

#End Region

#Region "Pie Theme KPI ContextMenu Control"

    Private Function GetKPIFromListBox(ByVal selectedKIP As String) As List(Of String)
        Dim dtKPIListBox As DataTable = New DataTable()
        Dim selectedLstItems As New List(Of String)
        Dim lstControlHost As ToolStripControlHost = TryCast(tsmi_UsingPieTheme.DropDownItems.Item(0), ToolStripControlHost)
        If (lstControlHost IsNot Nothing) Then
            Dim lstControl As ucICMKPIList = DirectCast(lstControlHost.Control, ucICMKPIList)
            If (lstControl IsNot Nothing) Then
                If (lstControl.GetKPI.Count > 0) Then
                    For Each item As DevExpress.XtraEditors.Controls.ListBoxItemCollection In lstControl.GetKPI
                        selectedLstItems.Add(item.ToString)
                    Next
                End If
            End If
        End If
        Return selectedLstItems
    End Function

    Private Sub SetICMKPIListControl()
        Dim objLstKPI As New ucICMKPIList()
        AddHandler objLstKPI.ItemKeyDown, AddressOf objLstKPI_ItemKeyDown
        If (dtIOS_ICM Is Nothing) Then
            Me.Fill_ICM_Data()
        End If
        Dim dt As DataTable = Me.dtIOS_ICM_Config.DefaultView.ToTable(True, "ID_ICMConfig", "DBColumn").Select("", "DBColumn").CopyToDataTable()
        If (dt.Rows.Count > 0) Then
            objLstKPI.SetKPIComboData = dt
        End If

        Dim tsch_ICMKPI As New ToolStripControlHost(objLstKPI)
        tsch_ICMKPI.Margin = Padding.Empty
        tsch_ICMKPI.Padding = Padding.Empty
        tsch_ICMKPI.AutoSize = False
        tsch_ICMKPI.Size = objLstKPI.Size
        tsch_ICMKPI.Name = "tschICMKPI"
        tsch_ICMKPI.Tag = "ICMKPI"
        tsmi_UsingPieTheme.DropDownItems.Add(tsch_ICMKPI)
    End Sub

    Public Sub objLstKPI_ItemKeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If (e.KeyCode = Keys.Delete) Then
            Dim vlstKPI As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
            If (vlstKPI IsNot Nothing) Then
                vlstKPI.SuspendLayout()
                vlstKPI.Refresh()
                vlstKPI.Items.Remove(vlstKPI.SelectedItem)
                vlstKPI.Update()
                vlstKPI.ResumeLayout()
            End If
        End If
    End Sub

#End Region

End Class