Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports dotnetCHARTING.WinForms
Imports System.Data.SqlClient
Imports System.Data.DataTableExtensions
Imports System.Linq
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports IOS.Library
Imports IOS.DataLibrary
Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraEditors

Public Class frmChartCustomization

#Region "Variables"

    Dim bl_CustomSerieSelected As Boolean = False
    Dim ObjectsCharted_Custom As String = ""
    Dim customtabindex2g As Integer = 0
    Dim customtabindex3g As Integer = 0
    Dim customtabindex2g3 As Integer = 0
    Dim customtabindex3g3 As Integer = 0
    Dim customtabindexNanoBTS As Integer = 0
    Dim customtabindexNano3G As Integer = 0
    Dim customtabindexBSC As Integer = 0
    Dim customtabindextopx2g As Integer = 0
    Dim customtabindextopx3g As Integer = 0
    Dim customtabindextopxNanoBTS As Integer = 0
    Dim customtabindextopxNano3G As Integer = 0
    Dim TreeSelectionType As TreeSelectionType = TreeSelectionType.NotSelected
    Dim objfrmTechnology As frmTechnology = Nothing
    Dim dtCustomizeSerieKPI As System.Data.DataTable = Nothing
    Dim rndCntr As Integer = 10

    Private dsStats As DataSet
    Private dsTopx As New DataSet

#End Region

#Region "Properties"

    Private _vendorTech As String
    Public Property VendorTech() As String
        Get
            Return _vendorTech
        End Get
        Set(ByVal value As String)
            _vendorTech = value
        End Set
    End Property

#End Region

#Region "Helper"

    Public Sub New()
        Me.SuspendLayout()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.ResumeLayout()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub ConfigurChartCustomizationForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                 cm_CustomChart_Add, cm_CustomChart_Delete, cm_CustomChart_Rename, cm_ChartCopy, cm_ChartPaste,
                 cm_InserCategory, cm_DeleteCategory, cm_RenameCategory, cm_ExpandAll, cm_CollapseAll
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

    Private Sub BindTechCombo()
        'get list of technologies
        Dim dtQODBC As System.Data.DataTable = Nothing
        Try
            Dim parray()() As String = Nothing
            Dim connstring As String = GetSQL(8700, parray)(0)
            Dim sql As String = GetSQL(8700, parray)(1)
            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)
            BindDevExComboBoxWithValueMember(cmbTechnology, dtQODBC, dtQODBC.Columns(0).Caption, dtQODBC.Columns(0).Caption, "Select Tech")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try
    End Sub

    Private Sub BindObjectType()
        'get list of ObjectTypes
        Try
            cmbObjectType.Properties.Items.Clear()
            Dim purpose As String = "Charts"
            If cmbTechnology.Text.Contains("TopX") Then purpose = "TopX"

            Dim dsObject As DataSet = clsSQLCommands.GetObjectTabByTechTab(connStrIOSServer, cmbTechnology.Text)

            If dsObject.Tables(0).Rows.Count > 0 Then
                BindDevExComboBoxWithValueMember(cmbObjectType, dsObject.Tables(0), "ObjectTabIndex", "ObjectTab")
                cmbObjectType.SelectedItem = cmbObjectType.Properties.Items(0)
                ''cmbObjectType_SelectedValueChanged(Nothing, Nothing)
            Else
                cmbObjectType.Text = "Select"
            End If
            Application.DoEvents()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally

        End Try
    End Sub

    Private Sub BindChartType()
        'get list of ChartTypes
        Try
            Dim dtChartType As DataTable = clsSQLCommands.GetChartType(connStrIOSServer)
            BindDevExComboBoxWithTagMember(cmbChartType, dtChartType, "TypeIndex", "TypeName", , "KPICount", True)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindChartSetName()
        'get list of ChartSetName
        RemoveHandler cmbChartSetName.SelectedIndexChanged, AddressOf cmbChartSetName_SelectedItemChanged
        Dim userFound = False
        Dim dtQODBC As System.Data.DataTable = Nothing
        Try
            Dim parray()() As String = {
                New String() {"@TechTab", Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39)},
                New String() {"@LicenseUser", Chr(39) & Environment.UserName & Chr(39)}
            }
            Dim connstring As String = GetSQL(8720, parray)(0)
            Dim sqlChartSetName As String = GetSQL(8720, parray)(1)
            Dim currentUser = Environment.UserName.ToString
            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sqlChartSetName)
            Dim foundUser() As DataRow = dtQODBC.AsEnumerable.Where(Function(x) x.Field(Of String)("ChartSetName") = currentUser).ToArray()

            If foundUser.Length = 0 Then
                Dim dr As DataRow = dtQODBC.NewRow()
                dr("ChartSetName") = currentUser
                dr("IsDefault") = "0"  'Accessibility
                dtQODBC.Rows.Add(dr)
                dtQODBC.AcceptChanges()
            End If

            BindDevExComboBoxWithTagMember(cmbChartSetName, dtQODBC, dtQODBC.Columns(0).Caption, dtQODBC.Columns(0).Caption, Nothing, dtQODBC.Columns(1).Caption)
            cmbChartSetName.SelectedIndex = 0
            SetComboBox(cmbChartSetName, ComboSelectBased.TextBased, currentUser)

            AddHandler cmbChartSetName.SelectedIndexChanged, AddressOf cmbChartSetName_SelectedItemChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try
    End Sub

    Private Sub GetCustomizeSerieKPI()
        Dim tech As String = Nothing
        If cmbTechnology.Text = "" Then
            Exit Sub
        End If
        tech = cmbTechnology.Text
        bl_CustomSerieSelected = True
        Dim parray()() As String = {
            New String() {"@tech", Chr(39) & Replace(tech, "TopX_", "") & Chr(39)},
            New String() {"@objecttype", Chr(39) & cmbObjectType.Text & Chr(39)}
        }
        Dim connstring As String = GetSQL(8702, parray)(0)
        Dim sql As String = GetSQL(8702, parray)(1)
        dtCustomizeSerieKPI = DataAccessorODBC.GetDataTable(connstring, sql)
    End Sub

    Private Sub UpdateKPITable(Optional ByVal IsNeedUpdateTree As Boolean = True)
        tp_Customize_Series.Text = "Chart Series "
        Dim ch As Chart = CType(tlp_CustomChart.Controls(0), Chart)
        ch.SeriesCollection.Clear()
        ch.Refresh()
        ch.Title = ""

        Dim dtQODBC As System.Data.DataTable = Nothing
        Try
            'Update cmb_kpi
            If cmbTechnology.Text = "" Then
                Exit Sub
            End If
            bl_CustomSerieSelected = True
            gcCustomizeSerieKPI.DataSource = Nothing
            If (dtCustomizeSerieKPI Is Nothing) Then
                GetCustomizeSerieKPI()
            End If

            If (txtSearchKPI.Text.Length > 2) Then
                Dim dv As New DataView(dtCustomizeSerieKPI, "KPI_Name LIKE '%" & txtSearchKPI.Text.Trim & "%'", "", DataViewRowState.CurrentRows)
                dtQODBC = dv.ToTable()
            Else
                dtQODBC = dtCustomizeSerieKPI.Copy()
            End If

            If Not dtQODBC Is Nothing Then
                gcCustomizeSerieKPI.AllowDrop = True
                gcCustomizeSerieKPI.DataSource = dtQODBC
                If gvCustomizeSerieKPI.Columns.Count > 0 Then
                    gvCustomizeSerieKPI.Columns(0).Visible = False
                End If
                For k As Integer = 1 To gvCustomizeSerieKPI.Columns.Count - 1
                    gvCustomizeSerieKPI.Columns(k).OptionsColumn.AllowSize = True
                    gvCustomizeSerieKPI.Columns(k).Resize(gvCustomizeSerieKPI.Columns(0).GetBestWidth())
                    gvCustomizeSerieKPI.Columns(k).Width = 235
                    gvCustomizeSerieKPI.Columns(k).OptionsFilter.AllowFilter = True
                Next
                gcCustomizeSerieKPI.Refresh()
                gcCustomizeSerieKPI.ResumeLayout()
                dtCustomChartKPI = dtQODBC.Copy
            End If

            If VendorTech IsNot Nothing Then
                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(VendorTech.Replace("TopX_", "").ToUpper)).LastOrDefault()
                If objfrmTechnology IsNot Nothing Then
                    Select Case cmbTechnology.SelectedItem.ToString.ToUpper.TrimEnd(" ")
                        Case objfrmTechnology.Network.ToUpper
                            If objfrmTechnology.dsStats Is Nothing Then
                                lblCustomizeInfo.Text = "Data for " & objfrmTechnology.Network & " Charts - Not Available " & vbLf & "Run " & objfrmTechnology.Network & " Statistics"
                                lblCustomizeInfo.ForeColor = Color.Red
                            Else
                                lblCustomizeInfo.Text = "Data for " & objfrmTechnology.Network & " Charts - Available"
                                lblCustomizeInfo.ForeColor = Color.Black
                            End If
                            cmbCustomizeSeriesOrder.SelectedItem = cmbCustomizeSeriesOrder.Properties.Items(0)
                            cmbCustomizeSeriesOrder.Enabled = False
                        Case "TOPX_" & objfrmTechnology.Network.ToUpper
                            If objfrmTechnology.dsTopX Is Nothing Then
                                lblCustomizeInfo.Text = "Data for TopX " & objfrmTechnology.Network & " Charts - Not Available " & vbLf & "Run " & objfrmTechnology.Network & " TopX"
                                lblCustomizeInfo.ForeColor = Color.Red
                            Else
                                lblCustomizeInfo.Text = "Data for TopX " & objfrmTechnology.Network & " Charts - Available"
                                lblCustomizeInfo.ForeColor = Color.Black
                            End If
                            cmbCustomizeSeriesOrder.Enabled = True
                    End Select
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try

        bl_CustomSerieSelected = False
        'Update tlv
        If (IsNeedUpdateTree) Then
            tlvCustomChartsSeries.Nodes.Clear()
            tv_CustomCharts_Refresh()
        End If
    End Sub

    Public Sub tv_CustomCharts_Refresh()
        'Update tlv
        Me.TreeSelectionType = IOS.Library.TreeSelectionType.NotSelected
        Dim dtQODBC As System.Data.DataTable = Nothing
        If cmbChartSetName.SelectedItem Is Nothing Then
            Exit Sub
        End If
        Dim parray2()() As String = {
            New String() {"@tech", Chr(39) & cmbTechnology.SelectedItem.ToString.TrimEnd(" ") & Chr(39)},
            New String() {"@ChartSetName", Chr(39) & cmbChartSetName.SelectedItem.ToString & Chr(39)},
            New String() {"@ObjectType", Chr(39) & cmbObjectType.Text & Chr(39)},
            New String() {"@Department", Chr(39) & chartSetName & Chr(39)},
            New String() {"@userid", Chr(39) & Environment.UserName.ToString & Chr(39)}
        }
        tvCustomChartsCustom.Nodes.Clear()
        Try
            Dim connstring As String = GetSQL(8714, parray2)(0)
            Dim Sql As String = GetSQL(8714, parray2)(1)

            dtQODBC = DataAccessorODBC.GetDataTable(connstring, Sql)
            If Not dtQODBC Is Nothing AndAlso dtQODBC.Rows.Count > 0 Then
                tvCustomChartsCustom.Nodes.Add(dtQODBC.Rows(0)("TechTab"))
                tvCustomChartsCustom.Nodes(0).Nodes.Add(dtQODBC.Rows(0)("ChartSetName"))
                tvCustomChartsCustom.Nodes(0).Nodes(0).ToolTipText = "ChartSetName"

                If (cmbChartSetName.SelectedItem.ToString = chartSetName) Or (cmbChartSetName.SelectedItem.ToString <> Environment.UserName.ToString) Then
                    Dim dtDistCategoryTabAndIndex As DataTable = dtQODBC.DefaultView.ToTable(True, "CategoryTab", "CategoryTabIndex")

                    If (cmbChartSetName.SelectedItem.ToString = chartSetName) Or (cmbChartSetName.SelectedItem.ToString <> Environment.UserName.ToString) Then
                        If (dtDistCategoryTabAndIndex.Rows.Count > 0) Then
                            Dim dr As DataRow = dtDistCategoryTabAndIndex.AsEnumerable().Where(Function(w) Not (w.Field(Of Int32)("CategoryTabIndex") = 99)).OrderBy(Function(w) w.Field(Of Int32)("CategoryTabIndex")).LastOrDefault()
                            If dr IsNot Nothing Then
                                lblCategoryTab.Text = dr("CategoryTab")
                                lblCategoryTabIndex.Text = dr("CategoryTabIndex")
                            End If
                        End If
                    Else
                        lblCategoryTab.Text = "Custom"
                        lblCategoryTabIndex.Text = "99"
                    End If

                    For Each categoryTab As DataRow In dtDistCategoryTabAndIndex.Rows
                        Dim category As String = categoryTab.Item(0).ToString()
                        Dim subNode As New TreeNode(category)
                        Dim categoryIndex As String = categoryTab.Item(1).ToString()
                        subNode.Tag = categoryIndex
                        subNode.ToolTipText = "Category"
                        Try
                            Dim SelectedCharts As IEnumerable(Of DataRow) = From w In dtQODBC.AsEnumerable()
                                                                            Where w.Field(Of String)("CategoryTab") = category AndAlso w.Field(Of System.Int32)("CategoryTabIndex") = categoryIndex
                                                                            Order By w.Field(Of System.Int32)("ChartIndex") Select w

                            For Each chart As DataRow In SelectedCharts
                                If Not (String.IsNullOrEmpty(chart.Item(1).ToString)) Then
                                    Dim subsubNode As New TreeNode(chart.Item(1).ToString)
                                    subsubNode.Tag = chart.Item(0).ToString
                                    subsubNode.Name = chart.Item(6).ToString
                                    subsubNode.ToolTipText = "Chart"
                                    subNode.Nodes.Add(subsubNode)
                                End If
                            Next
                        Catch ex As Exception
                        End Try
                        tvCustomChartsCustom.Nodes(0).Nodes(0).Nodes.Add(subNode)
                    Next
                Else
                    For Each drow As DataRow In dtQODBC.Rows
                        Dim subsubNode As New TreeNode(drow.Item(1).ToString)
                        subsubNode.Tag = drow.Item(0).ToString
                        subsubNode.Name = drow.Item(6).ToString
                        subsubNode.ToolTipText = "Chart"
                        tvCustomChartsCustom.Nodes(0).Nodes(0).Nodes.Add(subsubNode)
                    Next
                End If
            End If
            tvCustomChartsCustom.ExpandAll()
            tvCustomChartsCustom.Refresh()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try
    End Sub

    Private Sub CustomCharts_FillData(ByVal chname As String)
        Dim parray()() As String = {
            New String() {"@tech", Chr(39) & cmbTechnology.SelectedItem.ToString.TrimEnd(" ") & Chr(39)},
            New String() {"@chartname", Chr(39) & chname & Chr(39)},
            New String() {"@userid", Chr(39) & cmbChartSetName.SelectedItem.ToString & Chr(39)}
        }

        Dim connstring As String = GetSQL(8705, parray)(0)
        Dim sql As String = GetSQL(8705, parray)(1)
        Dim dtQODBC As System.Data.DataTable = Nothing

        Try
            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)
            For Each drow As DataRow In dtQODBC.Rows
                txt_Customize_Chart_Title.Text = drow(3).ToString.Trim
                Dim foundRows() As DataRow

                'Use the Select method to find all rows matching the filter.
                foundRows = dtCustomChartKPI.Select("SQLKPI_ID = " & drow(15).ToString.Trim)

                If drow(6).ToString.Trim.ToLower = "left" Then
                    CustomCharts_Serie_Insert(drow(4).ToString.Trim, drow(15).ToString.Trim, CInt(drow(14).ToString.Trim), drow(5).ToString.Trim, drow(7).ToString.Trim, drow(6).ToString.Trim, drow(8).ToString.Trim, drow(12).ToString.Trim,
                                              drow(10).ToString.Trim, drow(16).ToString.Trim, drow(19).ToString.Trim, drow(20).ToString.Trim, CBool(drow(21).ToString.Trim), nZ(CBool(drow(22).ToString.Trim), True), nZ(CBool(drow(23).ToString.Trim), True), nZ(CStr(drow(24).ToString.Trim), ""), nZ(drow(25), "False"))
                Else
                    CustomCharts_Serie_Insert(drow(4).ToString.Trim, drow(15).ToString.Trim, CInt(drow(14).ToString.Trim), drow(5).ToString.Trim, drow(7).ToString.Trim, drow(6).ToString.Trim, drow(9).ToString.Trim, drow(13).ToString.Trim,
                                              drow(11).ToString.Trim, drow(16).ToString.Trim, drow(19).ToString.Trim, drow(20).ToString.Trim, CBool(drow(21).ToString.Trim), nZ(CBool(drow(22).ToString.Trim), True), nZ(CBool(drow(23).ToString.Trim), True), nZ(CStr(drow(24).ToString.Trim), ""), nZ(drow(25), "False"))
                End If
            Next
            RefrashChartSeriesTLV(tlvCustomChartsSeries)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try
    End Sub

    Private Sub SetDefaultValue()
        cmbCustomizeSerieType.SelectedItem = cmbCustomizeSerieType.Properties.Items(1)
        cmbCustomizeSerieAxisType.SelectedItem = cmbCustomizeSerieAxisType.Properties.Items(0)
        cpCustomizeSerieColor.Color = Color.Black
        cmbCustomizeSerieAxis.SelectedItem = cmbCustomizeSerieAxis.Properties.Items(0)
        nudCustomizeChartPrecision.Text = "0"
        cmbCustomChartsAbsPerc.SelectedItem = cmbCustomChartsAbsPerc.Properties.Items(0)
        txtCustomChartsAxisLabel.Text = ""
        'If cmbCustomizeSerieType.SelectedItem.ToString.ToLower = "line" Then
        '    spEdLineThickness.Enabled = True
        '    cmbShowDatapoints.Enabled = True
        '    spEdLineThickness.EditValue = 3
        '    cmbShowDatapoints.SelectedIndex = 0
        'ElseIf cmbCustomizeSerieType.SelectedItem.ToString.ToLower = "bar" Then
        '    spEdLineThickness.Enabled = False
        '    cmbShowDatapoints.Enabled = False
        'End If
        spEdLineThickness.EditValue = 3
        cmbShowDatapoints.SelectedIndex = 0
        chkSeriesVisible.Checked = True
        cmbCustomChartAutoScale.SelectedIndex = 0
        cmbGroupByAttribute.SelectedIndex = 0
        chkEnablePeriodCalc.Checked = False
    End Sub

    Private Sub CustomCharts_Serie_Insert(ByVal KPI As String, ByVal KPI_ID As Integer, ByVal SerieColor As Integer, ByVal SerieType As String, ByVal SerieForm As String, ByVal yaxis_leftright As String, ByVal yaxis_left_label As String,
                                          ByVal yaxis_precision As String, ByVal yaxis_ABdPerc As String, Optional ByVal seriesorder As String = "", Optional ByVal elementAxis As String = "X", Optional lineThickness As String = "0",
                                          Optional showDataPoints As Boolean = False, Optional SeriesVisible As Boolean = True, Optional AutoScale As Boolean = True, Optional CrossTab As String = "", Optional enablePeriodCalc As Boolean = False)
        Try
            If (tlvCustomChartsSeries.Nodes.Count > 0 AndAlso tlvCustomChartsSeries.Nodes(0).SubItems.Count > 0 AndAlso String.IsNullOrEmpty(tlvCustomChartsSeries.Nodes(0).SubItems(0).Text)) Then
                tlvCustomChartsSeries.Nodes(0).Remove()
            End If
            Dim tlvnode As TreeListViewNode = New TreeListViewNode(KPI)
            Dim tlvnode_sub0 As TreeListViewSubItem = New TreeListViewSubItem(KPI)
            Dim tlvnode_sub1 As TreeListViewSubItem = New TreeListViewSubItem(KPI_ID)
            Dim tlvnode_sub2 As TreeListViewSubItem = New TreeListViewSubItem(SerieType)
            Dim tlvnode_sub3 As TreeListViewSubItem = New TreeListViewSubItem(SerieForm)
            Dim tlvnode_sub4 As TreeListViewSubItem = New TreeListViewSubItem(SerieColor)
            Dim tlvnode_sub5 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_leftright)
            Dim tlvnode_sub6 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_precision)
            Dim tlvnode_sub7 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_ABdPerc)
            Dim tlvnode_sub8 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_left_label)
            Dim tlvnode_sub9 As TreeListViewSubItem = New TreeListViewSubItem(seriesorder)
            Dim tlvnode_sub10 As TreeListViewSubItem = New TreeListViewSubItem(elementAxis)
            Dim tlvnode_sub11 As TreeListViewSubItem = New TreeListViewSubItem(lineThickness)
            Dim tlvnode_sub12 As TreeListViewSubItem = New TreeListViewSubItem(showDataPoints)
            Dim tlvnode_sub13 As TreeListViewSubItem = New TreeListViewSubItem(SeriesVisible)
            Dim tlvnode_sub14 As TreeListViewSubItem = New TreeListViewSubItem(AutoScale)
            Dim tlvnode_sub15 As TreeListViewSubItem = New TreeListViewSubItem(CrossTab)
            Dim tlvnode_sub16 As TreeListViewSubItem = New TreeListViewSubItem(enablePeriodCalc)

            tlvnode.SubItems.Add(tlvnode_sub0)
            tlvnode.SubItems.Add(tlvnode_sub1)
            tlvnode.SubItems.Add(tlvnode_sub2)
            tlvnode.SubItems.Add(tlvnode_sub3)
            tlvnode.SubItems.Add(tlvnode_sub4)
            tlvnode.SubItems.Add(tlvnode_sub5)
            tlvnode.SubItems.Add(tlvnode_sub6)
            tlvnode.SubItems.Add(tlvnode_sub7)
            tlvnode.SubItems.Add(tlvnode_sub8)
            tlvnode.SubItems.Add(tlvnode_sub9)
            tlvnode.SubItems.Add(tlvnode_sub10)
            tlvnode.SubItems.Add(tlvnode_sub11)
            tlvnode.SubItems.Add(tlvnode_sub12)
            tlvnode.SubItems.Add(tlvnode_sub13)
            tlvnode.SubItems.Add(tlvnode_sub14)
            tlvnode.SubItems.Add(tlvnode_sub15)
            tlvnode.SubItems.Add(tlvnode_sub16)

            tlvnode.Selected = True
            tlvCustomChartsSeries.Nodes.Add(tlvnode)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub Chart_Reinitialize()
        Chart1.Dispose()

        Chart1 = New Chart()

        Me.tlp_CustomChart.ColumnCount = 1
        Me.tlp_CustomChart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_CustomChart.Controls.Add(Me.Chart1, 0, 0)
        Me.tlp_CustomChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_CustomChart.Location = New System.Drawing.Point(0, 0)
        Me.tlp_CustomChart.Name = "tlp_CustomChart"
        Me.tlp_CustomChart.RowCount = 1
        Me.tlp_CustomChart.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_CustomChart.Size = New System.Drawing.Size(993, 330)
        Me.tlp_CustomChart.TabIndex = 2
        '
        'Chart1
        '
        Me.Chart1.AllowDrop = True
        Me.Chart1.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Me.Chart1.ApplicationDNC = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Me.Chart1.Background.Color = System.Drawing.Color.White

        Me.Chart1.Background.Color = System.Drawing.Color.White
        Me.Chart1.ChartArea.Background.Color = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.Chart1.ChartArea.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Square
        Me.Chart1.ChartArea.InteriorLine.Color = System.Drawing.Color.LightGray
        Me.Chart1.ChartArea.Label.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Chart1.ChartArea.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.Label.Width = -2147483648
        Me.Chart1.ChartArea.LegendBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Chart1.ChartArea.LegendBox.CornerBottomRight = dotnetCHARTING.WinForms.BoxCorner.Cut
        Me.Chart1.ChartArea.LegendBox.DefaultEntry.DividerLine.Color = System.Drawing.Color.Empty
        Me.Chart1.ChartArea.LegendBox.DefaultEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.Chart1.ChartArea.LegendBox.DefaultEntry.LabelStyle.Font = New System.Drawing.Font("Trebuchet MS", 8.0!)
        Me.Chart1.ChartArea.LegendBox.DefaultEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.LegendBox.DefaultEntry.LabelStyle.Width = -2147483648

        Me.Chart1.ChartArea.LegendBox.HeaderEntry.DividerLine.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.LabelStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.LabelStyle.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.LabelStyle.Width = -2147483648
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.Name = "Name"
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.SortOrder = -1
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.Value = "Value"
        Me.Chart1.ChartArea.LegendBox.HeaderEntry.Visible = False
        Me.Chart1.ChartArea.LegendBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Chart1.ChartArea.LegendBox.Line.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.LegendBox.Padding = 4
        Me.Chart1.ChartArea.LegendBox.Position = dotnetCHARTING.WinForms.LegendBoxPosition.Top
        Me.Chart1.ChartArea.LegendBox.Shadow.ExpandBy = 2.0!
        Me.Chart1.ChartArea.LegendBox.Visible = True
        Me.Chart1.ChartArea.Line.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.Shadow.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.Chart1.ChartArea.Shadow.Depth = 1
        Me.Chart1.ChartArea.Shadow.ExpandBy = 2.0!
        Me.Chart1.ChartArea.Shadow.Visible = False
        Me.Chart1.ChartArea.StartDateOfYear = New Date(CType(0, Long))
        Me.Chart1.ChartArea.TitleBox.Background.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer))

        Me.Chart1.ChartArea.TitleBox.InteriorLine.Color = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Chart1.ChartArea.TitleBox.Label.Color = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Chart1.ChartArea.TitleBox.Label.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Chart1.ChartArea.TitleBox.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.TitleBox.Label.Width = -2147483648
        Me.Chart1.ChartArea.TitleBox.Line.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.TitleBox.Shadow.ExpandBy = 2.0!
        Me.Chart1.ChartArea.TitleBox.Visible = True
        Me.Chart1.ChartArea.XAxis.Crosshair = Nothing
        Me.Chart1.ChartArea.XAxis.DefaultTick.AxisID = ""
        Me.Chart1.ChartArea.XAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.Chart1.ChartArea.XAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.Chart1.ChartArea.XAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.XAxis.DefaultTick.Label.Width = -2147483648
        Me.Chart1.ChartArea.XAxis.DefaultTick.Line.Length = 3
        Me.Chart1.ChartArea.XAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.XAxis.Label.Width = -2147483648
        Me.Chart1.ChartArea.XAxis.MinorTimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart1.ChartArea.XAxis.MinorTimeIntervalAdvanced.Unit = dotnetCHARTING.WinForms.TimeInterval.None
        Me.Chart1.ChartArea.XAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.XAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.Chart1.ChartArea.XAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart1.ChartArea.XAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.Chart1.ChartArea.XAxis.ZeroTick.AxisID = ""
        Me.Chart1.ChartArea.XAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.Chart1.ChartArea.XAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.Chart1.ChartArea.XAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.XAxis.ZeroTick.Label.Width = -2147483648
        Me.Chart1.ChartArea.XAxis.ZeroTick.Line.Length = 3
        Me.Chart1.ChartArea.YAxis.Crosshair = Nothing
        Me.Chart1.ChartArea.YAxis.DefaultTick.AxisID = ""
        Me.Chart1.ChartArea.YAxis.DefaultTick.GridLine.Color = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.Chart1.ChartArea.YAxis.DefaultTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.Chart1.ChartArea.YAxis.DefaultTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.YAxis.DefaultTick.Label.Width = -2147483648
        Me.Chart1.ChartArea.YAxis.DefaultTick.Line.Length = 3
        Me.Chart1.ChartArea.YAxis.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.YAxis.Label.Width = -2147483648
        Me.Chart1.ChartArea.YAxis.ScaleBreakLine.Color = System.Drawing.Color.Gray
        Me.Chart1.ChartArea.YAxis.TimeInterval = dotnetCHARTING.WinForms.TimeInterval.Hours
        Me.Chart1.ChartArea.YAxis.TimeIntervalAdvanced.Start = New Date(CType(0, Long))
        Me.Chart1.ChartArea.YAxis.TimeScaleLabels.MaximumRangeRows = 4
        Me.Chart1.ChartArea.YAxis.ZeroTick.AxisID = ""
        Me.Chart1.ChartArea.YAxis.ZeroTick.GridLine.Color = System.Drawing.Color.Red
        Me.Chart1.ChartArea.YAxis.ZeroTick.HoverAction = dotnetCHARTING.WinForms.HoverAction.None
        Me.Chart1.ChartArea.YAxis.ZeroTick.Label.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.ChartArea.YAxis.ZeroTick.Label.Width = -2147483648
        Me.Chart1.ChartArea.YAxis.ZeroTick.Line.Length = 3
        Me.Chart1.DataGrid = Nothing
        Me.Chart1.DefaultShadow.ExpandBy = 2.0!
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Chart1.LegacyMode = False
        Me.Chart1.Location = New System.Drawing.Point(3, 3)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.NoDataLabel.Offset = New System.Drawing.Point(0, 0)
        Me.Chart1.NoDataLabel.Text = "No Data"
        Me.Chart1.NoDataLabel.Width = -2147483648
        Me.Chart1.Size = New System.Drawing.Size(987, 324)
        Me.Chart1.StartDateOfYear = New Date(CType(0, Long))
        Me.Chart1.TabIndex = 1
        Me.Chart1.TempDirectory = "C:\Windows\TEMP\"

    End Sub

    Private Sub CustomCharts_Update()
        Chart_Reinitialize()
        Dim ch As Chart = CType(tlp_CustomChart.Controls(0), Chart)
        ch.SeriesCollection.Clear()

        If Not cmbTechnology.SelectedItem.ToString.Trim.ToUpper.Contains("TOPX") Then
            Dim chartSql As String = Nothing
            Using conn_el As New Odbc.OdbcConnection(connStrIOSServer)
                conn_el.ConnectionTimeout = 5
                conn_el.Open()
                Using comm_Element As New Odbc.OdbcCommand("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable,COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects,COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj
                                                            FROM IOS_Chart_Configuration INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID  
                                                            WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39) & ")
                                                            AND ChartName = " & Chr(39) & txt_CustomChartName.Text.Trim & Chr(39) & " AND (IOS_SQL_KPI.Object = " & Chr(39) & cmbObjectType.SelectedItem.ToString & Chr(39) & ")
                                                            AND (sourcetable Is Not null)", conn_el)

                    Using dr As Odbc.OdbcDataReader = comm_Element.ExecuteReader
                        While dr.Read
                            chartSql = Me.SQL_Construct(cmbObjectType.SelectedItem.ToString, nZ(dr.Item("sourcetable").ToString.Trim, ""), txt_CustomChartName.Text.Trim, nZ(dr.Item("CrossTabObj"), ""))
                        End While
                    End Using
                End Using
            End Using

            dsStats = DataAccessorODBC.GetDataSet(connStrIOSServer, chartSql)

            If Not dsStats Is Nothing Then

                'Default chart settings...
                ch.Type = ChartType.Combo
                ch.TempDirectory = "temp"
                ch.Annotations.Clear()
                ch.Annotations.Add(New Annotation(cmbTechnology.SelectedItem.ToString.ToUpper))
                ch.Annotations(0).Size = New Size(50, 30)
                Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                ch.Annotations(0).Label.Font = fnt
                ch.MarginBottom = 5
                ch.LegendBox.Orientation = Orientation.TopRight
                ch.ExtraChartAreas.Clear()
                ch.TitleBox.Label.Alignment = StringAlignment.Near
                ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                ch.XAxis.Label.Text = ""
                ch.LegendBox.Visible = True
                ch.DefaultSeries.DefaultElement.Marker.Visible = False
                ch.SeriesCollection.Clear()
                ch.MarginTop = 0
                ch.DefaultSeries.Type = SeriesType.Line
                ch.SmartPalette = Nothing
                ch.XAxis.ScaleBreaks.Clear()

                If CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.AlignInterval Then
                    Me.ProcessStatsCompareTime_Custom(dsStats, Chart1)
                ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Surface Then
                    Me.ProcessSurfaceChart(dsStats, Chart1)
                ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.IndexedCombo Then
                    Me.ProcessStatsIndexedCombo_Custom(dsStats, Chart1)
                ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.AlignIntervalCompare Then
                    Me.ProcessStatsCompareOverlapTime_Custom(dsStats, Chart1)
                Else
                    Me.AssignDataToCustomChart(dsStats.Tables(0), cmbTechnology.SelectedItem.ToString)
                End If
            Else
                ch.SeriesCollection.Clear()
                ch.Annotations.Clear()
                ch.XAxis.Label.Text = ""
                ch.ExtraChartAreas.Clear()
                ch.RefreshChart()
            End If
        Else
            Dim chartSql As String = Nothing
            Try
                chartSql = Me.SQL_Construct_TopX(cmbObjectType.SelectedItem.ToString, txt_CustomChartName.Text.Trim)
            Catch
            End Try

            If chartSql IsNot Nothing Then
                dsTopx = DataAccessorODBC.GetDataSet(connStrIOSServer, chartSql)
                If Not dsTopx Is Nothing Then
                    AssignDataToCustomChartTopX(dsTopx.Tables(0), cmbTechnology.SelectedItem.ToString)
                Else
                    ch.SeriesCollection.Clear()
                    ch.Annotations.Clear()
                    ch.XAxis.Label.Text = ""
                    ch.ExtraChartAreas.Clear()
                    ch.RefreshChart()
                End If
            End If
        End If

    End Sub

    Private Sub ProcessStatsCompareTime_Custom(ds As DataSet, ByRef ch As Chart)
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfoDefault
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureUIDefault

            Dim dt_new As New DataTable
            Dim dt_new_sort As New DataTable
            Dim pvt_Pivot As Pivot = Nothing
            Dim dt_pivot As DataTable = Nothing
            Dim KPIname As String = Nothing

            'Dim ds_chart As DataSet = clsSQLCommands.GetDataAssignToCustomChart(constr, tech, chart_name, chart_set_name, objectType)
            'Dim dt_chart As DataTable = ds_chart.Tables(0)
            KPIname = tlvCustomChartsSeries.Nodes(0).SubItems(0).Text.Trim      'dt_chart.Rows(0).Item("ChartElements")

            For Each dt As DataTable In ds.Tables
                If dt.Columns.Contains(KPIname) Then
                    Dim displayView = New DataView(dt)
                    dt_new = displayView.ToTable(False, "Date", KPIname).Copy
                End If
            Next

            If Not dt_new Is Nothing Then
                'add time columns for pivot
                dt_new.Columns.Add(New DataColumn("RowsSortField", GetType(Integer)))
                dt_new.Columns.Add(New DataColumn("RowsField", GetType(String)))
                dt_new.Columns.Add(New DataColumn("ColumnsField", GetType(String)))

                If dt_new.Rows.Count > 1 Then
                    Dim d1 As DateTime = dt_new(0)("Date")
                    Dim d2 As DateTime = dt_new(1)("Date")
                    Dim dlast As DateTime = dt_new(dt_new.Rows.Count - 1)("Date")
                    Dim dd As Long = DateDiff(DateInterval.Minute, d1, d2)

                    If dd < 60 Then
                        For Each dr As DataRow In dt_new.Rows
                            Dim hourpart As String = "0" + DatePart(DateInterval.Hour, dr("Date")).ToString
                            Dim minpart As String = "0" + DatePart(DateInterval.Minute, dr("Date")).ToString
                            dr("RowsSortField") = hourpart.Substring(hourpart.Length - 2, 2) + minpart.Substring(minpart.Length - 2, 2)
                            dr("RowsField") = hourpart.Substring(hourpart.Length - 2, 2) + ":" + minpart.Substring(minpart.Length - 2, 2)
                            dr("ColumnsField") = "Day " & (DateDiff(DateInterval.Day, DateSerial(dlast.Year, dlast.Month, dlast.Day), DateSerial(dr("Date").Year, dr("Date").Month, dr("Date").Day), FirstDayOfWeek.System, FirstWeekOfYear.System))
                        Next
                    ElseIf dd = 60 Then
                        For Each dr As DataRow In dt_new.Rows
                            dr("RowsSortField") = DatePart(DateInterval.Hour, dr("Date"))
                            dr("RowsField") = DatePart(DateInterval.Hour, dr("Date"))
                            dr("ColumnsField") = "Day " & (DateDiff(DateInterval.Day, DateSerial(dlast.Year, dlast.Month, dlast.Day), DateSerial(dr("Date").Year, dr("Date").Month, dr("Date").Day), FirstDayOfWeek.System, FirstWeekOfYear.System))
                        Next
                    ElseIf dd = 60 * 24 Then
                        For Each dr As DataRow In dt_new.Rows
                            dr("RowsSortField") = DatePart(DateInterval.Weekday, dr("Date"), FirstDayOfWeek.System)
                            dr("RowsField") = WeekdayName(DatePart(DateInterval.Weekday, dr("Date"), FirstDayOfWeek.System), False, FirstDayOfWeek.System)
                            dr("ColumnsField") = "Week " & (DateDiff(DateInterval.WeekOfYear, DateSerial(dlast.Year, dlast.Month, dlast.Day), DateSerial(dr("Date").Year, dr("Date").Month, dr("Date").Day), FirstDayOfWeek.System, FirstWeekOfYear.System))
                        Next
                    End If
                    'remove date column
                    dt_new.Columns.Remove("Date")
                    Dim displayViewSort = New DataView(dt_new)
                    displayViewSort.Sort = "RowsSortField ASC"
                    dt_new_sort = displayViewSort.ToTable.Copy
                    'pivot
                    'pvt_Pivot = New Pivot(dt_new)
                    pvt_Pivot = New Pivot(dt_new_sort)
                    dt_pivot = pvt_Pivot.PivotData("RowsField", KPIname, AggregateFunction.Average, "ColumnsField")

                    'create chart
                    Me.AssignDataToCompareTime_Custom(dt_pivot, ch)
                End If
            End If
        Catch ex As Exception
        End Try
        'System.Threading.Thread.CurrentThread.CurrentUICulture = Globalization.CultureInfo.GetCultureInfo("en-US")
        'System.Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.GetCultureInfo("en-US")
    End Sub

    Private Sub ProcessStatsCompareOverlapTime_Custom(ds As DataSet, ByRef ch As Chart)
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfoDefault
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureUIDefault

            Dim dt_new As New DataTable
            Dim dtOverlapSet As New DataTable
            Dim KPIname As String = Nothing

            KPIname = tlvCustomChartsSeries.Nodes(0).SubItems(0).Text.Trim

            For Each dt As DataTable In ds.Tables
                If dt.Columns.Contains(KPIname) Then
                    Dim displayView = New DataView(dt)
                    dt_new = displayView.ToTable(False, "Date", KPIname).Copy
                End If
            Next

            dtOverlapSet.Columns.Add(New DataColumn("Date", GetType(DateTime)))
            dtOverlapSet.Columns.Add(New DataColumn(KPIname & "_CurrentWeek", GetType(Double)))
            dtOverlapSet.Columns.Add(New DataColumn(KPIname & "_LastWeek", GetType(Double)))

            If Not dt_new Is Nothing Then
                If dt_new.Rows.Count > 1 Then
                    Dim dtReverse As DataTable = dt_new.AsEnumerable().Reverse().CopyToDataTable

                    For Each dr As DataRow In dtReverse.Rows
                        If dtReverse.Select("Date='" & CDate(dr("Date")).AddDays(-7) & "'").Length > 0 Then
                            Dim dr1stSet As DataRow = dtOverlapSet.NewRow()
                            dr1stSet("Date") = dr("Date")
                            dr1stSet(KPIname & "_CurrentWeek") = dr(KPIname)

                            Dim lastWeekKPIVal As Double = dtReverse.Select("Date='" & CDate(dr("Date")).AddDays(-7) & "'")(0)(KPIname)
                            dr1stSet(KPIname & "_LastWeek") = lastWeekKPIVal
                            dtOverlapSet.Rows.Add(dr1stSet)
                        End If
                    Next
                    'End If
                End If
            End If
            Me.AssignDataToCompareOverlapTime_Custom(dtOverlapSet, ch)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetChartXAxis(ByRef ch As Chart)
        Try
            Dim TimeResolution As String = "Daily"
            If Not ch Is Nothing Then
                ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                ch.XAxis.TimeInterval = TimeInterval.Days
                ch.XAxis.FormatString = "dd/MM/yy"
                ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                ch.XAxis.TimeInterval = TimeInterval.Days
                ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
            End If
        Catch
        End Try
    End Sub

    Private Sub AssignDataToCompareTime_Custom(ByRef dt As DataTable, ByRef ch As Chart)

        'this is to get objectcharted filled. 
        SetChartXAxis(ch)

        'Assign data to all charts
        '*************************
        Dim i As Integer
        Dim Y1axislabel As String = ""
        Dim Y2axislabel As String = ""
        Dim Y1axisAbsorPerc = "", Y2axisAbsOrPerc As String = ""
        Dim Y1axisPrecision = 0, Y2axisPrecision As Integer = 0
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim sp As New SmartPalette()
        Dim sc As New SeriesCollection
        'Dim color_R, color_B, color_G As Integer

        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0

        ch.XAxis.Label.Text = ""
        ch.ExtraChartAreas.Clear()
        ch.DefaultSeries.Type = SeriesType.Line

        For rownum = 0 To 0
            Try
                'collecting elements from chart confguration
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.Nodes(0)

                'configures individual chart when new chartline is detected
                If lastchart = "" Or lastchart <> txt_CustomChartName.Text.Trim Then
                    lastchart = txt_CustomChartName.Text.Trim.Trim
                    sp.Clear()

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisAbsorPerc = nd.SubItems(7).Text.Trim
                    Else
                        Y2axisAbsOrPerc = nZ(nd.SubItems(7).Text.Trim, "Abs")
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                        Y2axisPrecision = 0
                    Else
                        Y1axisPrecision = 0
                        Y2axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        If nZ(nd.SubItems(8).Text.Trim, "").Length > 0 Then
                            Y1axislabel = nd.SubItems(8).Text.Trim
                        End If
                    Else
                        If nZ(nd.SubItems(8).Text, "").Length > 0 Then
                            Y2axislabel = nd.SubItems(8).Text.Trim
                        End If
                    End If

                    chart_elementsYAxis(0) = nZ(nd.SubItems(5).Text.Trim, " ")

                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(cmbTechnology.SelectedItem.ToString.ToUpper))
                    ch.TitleBox.HeaderLabel.Text = txt_Customize_Chart_Title.Text.Trim

                    ch.TitleBox.Label.Text = "Objects: PLMN"

                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near

                    ch.DefaultElement.Hotspot.ToolTip = "DateTimeElement: %XValue" & Chr(13) & "%SeriesName: %Value "
                    Dim charttitle As String = txt_Customize_Chart_Title.Text.Trim

                    'Y-Axis Settingso   
                    If chart_elementsYAxis(i).Trim.ToUpper = "LEFT" Then
                        yaxis1 = New Axis
                        yaxis1.Orientation = Orientation.Left
                        yaxis1.Label.Text = Y1axislabel
                        If UCase(Y1axisAbsorPerc) = "ABS" Then
                            yaxis1.Percent = False
                            yaxis1.NumberPrecision = Y1axisPrecision
                        End If
                        If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                            yaxis1.MinimumInterval = 1
                        End If
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    Else
                        yaxis2 = New Axis
                        yaxis2.Orientation = Orientation.Left
                        yaxis2.Label.Text = Y2axislabel
                        If UCase(Y2axisAbsOrPerc) = "PERC" Then
                            yaxis2.Percent = True
                            yaxis2.NumberPrecision = Y2axisPrecision
                        End If
                        If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                            yaxis2.MinimumInterval = 1
                        End If
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    '+++++++++++++++++++++++++++++++++++++++
                    j = 0

                    For Each col As DataColumn In dt.Columns
                        ReDim Preserve chart_elements(j)
                        If col.ColumnName.ToUpper <> "ROWSFIELD" Then
                            chart_elements(j) = col.ColumnName.ToUpper
                            j = j + 1
                        End If
                    Next

                    Dim de As DataEngine = New DataEngine(dt)
                    de.DataFields = String2DataFields(chart_elements, "RowsField")
                    sc = de.GetSeries()

                    Dim rnd As Random = New Random(10)

                    For i = 0 To sc.Count() - 1
                        sc(i).Type = SeriesType.Line

                        If sc(i).Name = "DAY 0" Or sc(i).Name = "WEEK 0" Then
                            sc(i).Line.Width = 10
                        Else
                            sc(i).Line.Width = CInt(nZ(nd.SubItems(11).Text, 0))
                        End If

                        If yaxis1 IsNot Nothing Then
                            sc(i).YAxis = yaxis1
                        Else
                            sc(i).YAxis = yaxis2
                        End If
                        sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
                        sc(i).DefaultElement.Marker.Type = i
                    Next
                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    Me.HideChartScaleIfNoDataStats(ch, dt)

                    sc = Nothing
                    de = Nothing

                    ch.XAxis.Markers.Clear()
                    ch.RefreshChart()
                    ch.Visible = True

                    Dim tn As TreeNode = New TreeNode()
                    tn.Text = charttitle
                    tn.Tag = ch.Name

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    j = 0

                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                Console.WriteLine(ex.Message.ToString)
            End Try
        Next

        If Not ch Is Nothing Then
            ch.Series.Data = dt.Copy
        End If

        If Not dt Is Nothing Then
            dt.Dispose()
        End If
    End Sub

    Private Sub AssignDataToCompareOverlapTime_Custom(ByRef dt As DataTable, ByRef ch As Chart)

        'this is to get objectcharted filled. 
        SetChartXAxis(ch)

        'Assign data to all charts
        '*************************
        Dim i As Integer
        Dim Y1axislabel As String = ""
        Dim Y2axislabel As String = ""
        Dim Y1axisAbsorPerc = "", Y2axisAbsOrPerc As String = ""
        Dim Y1axisPrecision = 0, Y2axisPrecision As Integer = 0
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim sp As New SmartPalette()
        Dim sc As New SeriesCollection
        'Dim color_R, color_B, color_G As Integer

        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        'Dim chart_ElLineSize() As Integer = {0}
        'Dim chart_ElShowDatapoints() As Boolean = {False}
        'Dim chart_SeriesVisible() As Boolean = {True}
        Dim chart_SeriesAutoScale() As Boolean = {True}

        Dim j As Integer = 0
        Dim rownum As Integer = 0

        ch.XAxis.Label.Text = ""
        ch.ExtraChartAreas.Clear()
        For rownum = 0 To 0
            Try
                'collecting elements from chart confguration
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.Nodes(0)

                'configures individual chart when new chartline is detected
                If lastchart = "" Or lastchart <> txt_CustomChartName.Text.Trim Then
                    lastchart = txt_CustomChartName.Text.Trim
                    sp.Clear()

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisAbsorPerc = nd.SubItems(7).Text.Trim
                    Else
                        Y2axisAbsOrPerc = nZ(nd.SubItems(7).Text.Trim, "Abs")
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                        Y2axisPrecision = 0
                    Else
                        Y1axisPrecision = 0
                        Y2axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        If nZ(nd.SubItems(8).Text.Trim, "").Length > 0 Then
                            Y1axislabel = nd.SubItems(8).Text.Trim
                        End If
                    Else
                        If nZ(nd.SubItems(8).Text, "").Length > 0 Then
                            Y2axislabel = nd.SubItems(8).Text.Trim
                        End If
                    End If

                    chart_elementsYAxis(0) = nZ(nd.SubItems(5).Text.Trim, " ")

                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(cmbTechnology.SelectedItem.ToString.ToUpper))
                    ch.TitleBox.HeaderLabel.Text = txt_Customize_Chart_Title.Text.Trim

                    ch.TitleBox.Label.Text = "Objects: PLMN"

                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near

                    ch.DefaultElement.Hotspot.ToolTip = "DateTimeElement: %XValue" & Chr(13) & "%SeriesName: %Value "
                    Dim charttitle As String = txt_Customize_Chart_Title.Text.Trim

                    'Y-Axis Settings
                    If chart_elementsYAxis(i).Trim.ToUpper = "LEFT" Then
                        yaxis1 = New Axis
                        yaxis1.Orientation = Orientation.Left
                        yaxis1.Label.Text = Y1axislabel
                        If UCase(Y1axisAbsorPerc) = "ABS" Then
                            yaxis1.Percent = False
                            yaxis1.NumberPrecision = Y1axisPrecision
                        End If
                        If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                            yaxis1.MinimumInterval = 1
                        End If
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    Else
                        yaxis2 = New Axis
                        yaxis2.Orientation = Orientation.Left
                        yaxis2.Label.Text = Y2axislabel
                        If UCase(Y2axisAbsOrPerc) = "PERC" Then
                            yaxis2.Percent = True
                            yaxis2.NumberPrecision = Y2axisPrecision
                        End If
                        If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                            yaxis2.MinimumInterval = 1
                        End If
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    '+++++++++++++++++++++++++++++++++++++++
                    j = 0

                    For Each col As DataColumn In dt.Columns
                        ReDim Preserve chart_elements(j)
                        If col.ColumnName.ToUpper <> "DATE" Then
                            chart_elements(j) = col.ColumnName
                            j = j + 1
                        End If
                    Next

                    Dim de As DataEngine = New DataEngine(dt)
                    de.DataFields = String2DataFields(chart_elements, "Date")
                    sc = de.GetSeries()

                    Dim rnd As Random = New Random(10)

                    For i = 0 To sc.Count() - 1
                        sc(i).Type = SeriesType.Line

                        If sc(i).Name = "DAY 0" Or sc(i).Name = "WEEK 0" Then
                            sc(i).Line.Width = 10
                        Else
                            sc(i).Line.Width = CInt(nZ(nd.SubItems(11).Text, 0))
                        End If

                        If yaxis1 IsNot Nothing Then
                            sc(i).YAxis = yaxis1
                        Else
                            sc(i).YAxis = yaxis2
                        End If

                        sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
                    Next
                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    HideChartScaleIfNoDataStats(ch, dt)

                    sc = Nothing
                    de = Nothing

                    ch.XAxis.Markers.Clear()
                    ch.RefreshChart()
                    ch.Visible = True

                    Dim tn As TreeNode = New TreeNode()
                    tn.Text = charttitle
                    tn.Tag = ch.Name

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim chart_SeriesAutoScale(True)
                    j = 0
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                Console.WriteLine(ex.Message.ToString)
            End Try
        Next

        If Not ch Is Nothing Then
            ch.Series.Data = dt.Copy
        End If

        If Not dt Is Nothing Then
            dt.Dispose()
        End If
    End Sub

    Private Sub HideChartScaleIfNoDataStats(ByRef ch As Chart, ByRef dtData As DataTable)
        '-------Hide Date if there is no data in dtChild-------
        ch.XAxis.ScaleBreakStyle = ScaleBreakStyle.None
        ch.XAxis.ScaleBreaks.Clear()
        Dim sDate As Date = dtpStartTime.EditValue
        Dim eDate As Date = dtpEndTime.EditValue
        Dim dtAxis As DataTable = dtData.DefaultView.ToTable(True, dtData.Columns(0).ColumnName)
        While sDate <= eDate
            Dim dr() As DataRow = dtAxis.Select(dtData.Columns(0).ColumnName & "='" & sDate & "'")
            If dr.Length = 0 Then
                ch.XAxis.ScaleBreaks.Add(New dotnetCHARTING.WinForms.ScaleRange(sDate, sDate))
            End If
            sDate = sDate.AddDays(1)
        End While
    End Sub

    Private Function SQL_Construct(ByVal objtype As String, ByVal aliastable As String, ByVal chartname As String, ByVal CrossTabObj As String) As String

        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim sql_select As String = Nothing
        Dim sql_where_misc As String = ""
        Dim sql_where_object As String = ""
        Dim sql_where_tables As String = ""
        Dim sql_where_period As String = ""
        Dim sql_groupby As String = Nothing
        Dim sql_orderby As String = Nothing
        Dim sql_from_time As String = Nothing
        Dim sql_kpi As String = Nothing
        Dim sql_total As String = Nothing
        Dim connectionString As String = ""

        Dim startdate As Date = CDate(dtpStartTime.EditValue)
        Dim enddate As Date = CDate(dtpEndTime.EditValue)

        Dim startdate_string As String = Chr(39) & startdate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
        Dim enddate_string As String = Chr(39) & enddate.ToString("yyyy-MM-dd HH:mm") & Chr(39)

        Dim conn_el As Odbc.OdbcConnection = Nothing
        Dim comm_sql As Odbc.OdbcCommand = Nothing
        Dim comm_Element As Odbc.OdbcCommand = Nothing
        Dim dr_sql As Odbc.OdbcDataReader = Nothing
        Dim dr_element As Odbc.OdbcDataReader = Nothing
        Dim sqlelement As String = Nothing
        Dim sql_sql As String = Nothing

        Try
            'Open connection to server
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()

            'if aliastable is nothing and chartname is not, then get the aliastable (used for sql building based on chart)
            If aliastable Is Nothing And Not chartname Is Nothing Then
                Dim dt_alias As DataTable = clsSQLCommands.GetConfigChartSourceTableData(connStrIOSServer, tech, chartname)
                aliastable = dt_alias(0)(0).ToString
                chartname = Nothing
            End If

            'objecttree selection to string
            Dim objectsel As String = "IN('PLMN')"
            Dim ObjectsCharted As String = "IN ('PLMN')"

            Dim aggr_to As String = "PLMN"
            Dim aggr_from As String = Nothing

            Dim CMFilter As String = Nothing
            Dim RegionFilter As String = objectsel
            Dim tagid As String = Nothing

            'set purpose
            Dim purpose As String = "Charts"
            'If IsObjectAggregated = False Then
            '    purpose = "ObjectTime"
            'End If

            Dim StringForSourceTable As String = ""
            sql_sql = "SELECT * FROM qry_IOS_ConstructStatSQL WHERE (((tech)=" & Chr(39) & tech & Chr(39) & ") AND ((Purpose)=" & Chr(39) & purpose & Chr(39) & ") AND ((Aggregate_to)=" & Chr(39) & aggr_to & Chr(39) & ") AND ((ObjectType)=" & Chr(39) & objtype & Chr(39) & "));"
            comm_sql = New Odbc.OdbcCommand(sql_sql, conn_el)
            dr_sql = comm_sql.ExecuteReader
            sql_from_time = ""

            dr_sql.Read()
            If Not dr_sql.HasRows = 0 Then

                StringForSourceTable = "_DAY"

                For Each c As String In Split(dr_sql.GetValue(3).ToString.Trim, " ")
                    If c.ToUpper.Contains("PERIOD_START_TIME") Then
                        Dim aliasInPeriodStartTime As String = c.Split(".")(0)
                    End If
                Next

                sql_select = dr_sql("sql_select").ToString.Trim
                aggr_from = dr_sql("Aggregate_From").ToString.Trim

                sql_from_time = " " & dr_sql("sql_time_day").ToString.Trim
                connectionString = dr_sql("sql_time_day_connStr").ToString.Trim

                sql_where_misc = " " & dr_sql("sql_where_misc").ToString.Trim
                If cmbObjectType.SelectedItem.ToString = "PLMN" Then
                    'to complete
                Else
                    sql_where_object = " " & Replace(dr_sql("sql_where_object"), "@object", ObjectsCharted).ToString.Trim
                End If
                sql_where_tables = " " & dr_sql("sql_where_tables").ToString.Trim
                sql_where_period = " " & Replace(Replace(dr_sql("sql_where_period"), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim

                sql_groupby = " " & dr_sql("sql_groupby").ToString.Trim
                sql_orderby = " " & dr_sql("sql_orderby").ToString.Trim
            Else
                'Closing and dereferencing
                dr_sql.Close()
                dr_sql = Nothing
                comm_sql.Dispose()
                comm_sql = Nothing
                conn_el.Close()
                conn_el.Dispose()
                conn_el = Nothing
                Return ""
            End If
            'Closing and dereferencing
            dr_sql.Close()
            dr_sql = Nothing
            comm_sql.Dispose()
            comm_sql = Nothing
            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            Dim supportcode As Integer = 0
            'get KPI sql
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()
            If aggr_from.Contains("BSC") Then supportcode = 1

            Dim crosstabkpisql As String = ""
            If Not CrossTabObj Is Nothing And Not CrossTabObj = "" Then
                crosstabkpisql = " AND IOS_Chart_Configuration.CrossTabObj='" & CrossTabObj & Chr(39)
            End If
            If chartname = Nothing Then
                sqlelement = "SELECT DISTINCT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object FROM IOS_Chart_Configuration " &
                             " INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID " &
                             " WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & tech & Chr(39) & ")  AND (IOS_SQL_KPI.sourcetable = " & Chr(39) & aliastable & Chr(39) & ") AND supportcode >= " & supportcode & ";"
            Else
                sqlelement = "SELECT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object FROM IOS_Chart_Configuration " &
                             " INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID " &
                             " WHERE (IOS_Chart_Configuration.ChartName = " & Chr(39) & chartname & Chr(39) & " ) AND (IOS_Chart_Configuration.TechTab = " & Chr(39) & tech & Chr(39) & ")  AND " &
                             " (IOS_SQL_KPI.sourcetable= " & Chr(39) & aliastable & Chr(39) & ") AND (IOS_Chart_Configuration.ObjectTab = " & Chr(39) & objtype & Chr(39) & ") " &
                             " GROUP BY IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object, IOS_Chart_Configuration.CategoryTabIndex, IOS_Chart_Configuration.ChartIndex " &
                             " ORDER BY IOS_Chart_Configuration.CategoryTabIndex, IOS_Chart_Configuration.ChartIndex ;"
            End If

            '- add identification of primary keys if sourcetable has multiple tables and agg_from is diff from objtype
            If aggr_from <> objtype And aliastable.Split(",").Count > 1 Then
                Dim tableToIdentifyPrimKey As String = Replace(Replace(Split(Split(aliastable, ",")(0), ".").Last, "<AggregatedObject>", aggr_from).Trim, "_RAW", "_DAY")
                Dim dbToIdentifyPrimKey As String = Split(Split(aliastable, ",")(0), ".").First.Trim

                sqlelement = sqlelement & vbCrLf & "Select schema_name(tab.schema_id) as [schema_name],  tab.[name] as table_name,   pk.[name] as pk_name,   substring(column_names, 1, len(column_names)-1) as [columns]
                                                    from " + dbToIdentifyPrimKey + ".sys.tables tab
                                                    left outer join " + dbToIdentifyPrimKey + ".sys.indexes pk on tab.object_id = pk.object_id  and pk.is_primary_key = 1
                                                    cross apply (select col.[name] + ', '
                                                    from " + dbToIdentifyPrimKey + ".sys.index_columns ic
                                                    inner join " + dbToIdentifyPrimKey + ".sys.columns col on ic.object_id = col.object_id and ic.column_id = col.column_id
                                                    where ic.object_id = tab.object_id and ic.index_id = pk.index_id
                                                    order by col.column_id for xml path ('') ) D (column_names) where tab.[name] = '" + tableToIdentifyPrimKey + "' 
                                                    order by schema_name(tab.schema_id),
                                                    tab.[name];"
            End If

            comm_Element = New Odbc.OdbcCommand(sqlelement, conn_el)
            dr_element = comm_Element.ExecuteReader
            sql_kpi = ""
            Dim sourcetable As String = ""
            Dim joinobjs As String = ""
            Dim sql_kpi_builder As New System.Text.StringBuilder

            While dr_element.Read
                sql_kpi_builder.Append(" " + dr_element.GetValue(0).trim + ", ")
                'sql_kpi = sql_kpi + " " + dr_element.GetValue(0).trim + ", "
                sourcetable = dr_element.GetValue(1).trim
                sourcetable = Replace(sourcetable, "_RAW", StringForSourceTable)
                sourcetable = Replace(sourcetable, "<AggregatedObject>", aggr_from)
                aliastable = dr_element.GetValue(2).ToString.Trim
                joinobjs = dr_element.GetValue(3).ToString.Trim
            End While
            sql_kpi = sql_kpi_builder.ToString
            sql_kpi = sql_kpi.TrimEnd(" ")
            sql_kpi = sql_kpi.TrimEnd(",")

            If dr_element.NextResult AndAlso dr_element.HasRows Then
                joinobjs = Replace(dr_element.GetValue(3), " ", "")
            End If
            'building sourcetable for multi
            Dim sourcetable_final As String = ""
            'If Not sourcetable Is Nothing Then
            '	If sourcetable.Contains(",") Then
            '		For i As Integer = 0 To Split(sourcetable, ",").Count - 1
            '			sourcetable_final = sourcetable_final + Split(sourcetable, ",")(i) & " " & Split(aliastable, ",")(i) + ", "
            '		Next
            '		sourcetable_final = sourcetable_final.Substring(0, Len(sourcetable_final) - 2)
            '	Else
            '		sourcetable_final = sourcetable + " " + aliastable
            '	End If
            'End If
            If Not sourcetable Is Nothing Then
                If sourcetable.Contains(",") Then
                    'get first table name and first alias
                    Dim firstTable As String = Split(sourcetable, ",")(0)
                    Dim firstAlias As String = Split(aliastable, ",")(0)
                    sourcetable_final = firstTable + " " + firstAlias
                    For i As Integer = 1 To Split(sourcetable, ",").Count - 1
                        sourcetable_final = sourcetable_final + " inner join "
                        sourcetable_final = sourcetable_final + Split(sourcetable, ",")(i) & " " & Split(aliastable, ",")(i) + " ON "
                        For j = 0 To joinobjs.Split(",").Count - 1
                            sourcetable_final = sourcetable_final + firstAlias + "." + joinobjs.Split(",")(j) + "=" + aliastable.Split(",")(i) + "." + joinobjs.Split(",")(j)
                            If j < joinobjs.Split(",").Count - 1 Then
                                sourcetable_final = sourcetable_final + " AND "
                            End If
                        Next
                    Next
                    aliastable = Split(aliastable, ",")(0)
                Else
                    sourcetable_final = sourcetable + " " + aliastable
                End If
            End If

            'building jointable for multi
            Dim jointable As String = " "
            'If joinobjs.Contains(",") Then
            '	Dim firsttable As String = Split(aliastable, ",")(0)
            '	For Each obj As String In Split(joinobjs, ",")
            '		For i As Integer = 1 To Split(aliastable, ",").Count - 1
            '			jointable = jointable + firsttable + "." + obj + " = " + Split(aliastable, ",")(i) + "." + obj + " AND "
            '		Next
            '	Next
            '	jointable = " AND " & jointable.Substring(0, Len(jointable) - 4)
            '	aliastable = Split(aliastable, ",")(0)
            'End If

            Dim sql_crosstab_select As String = ""
            Dim sql_crosstab_groupby As String = ""
            If CrossTabObj <> "" Then
                sql_crosstab_select = "@alias." + CrossTabObj + ","
                sql_crosstab_groupby = ",@alias." + CrossTabObj
            End If

            'Closing and dereferencing
            comm_Element.Dispose()
            comm_Element = Nothing
            dr_element.Close()
            dr_element = Nothing
            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            sql_total = sql_select + sql_crosstab_select + sql_kpi + " " + sql_from_time + sql_where_misc + sql_where_object + sql_where_tables + sql_where_period + jointable + sql_groupby + sql_crosstab_groupby + sql_orderby

            sql_total = Replace(sql_total, "@sourcetable", sourcetable_final)
            sql_total = Replace(sql_total, "@alias", aliastable)
            sql_total = Replace(sql_total, "@object", ObjectsCharted)
            sql_total = Replace(sql_total, "@CrossTabObj", CrossTabObj)
            sql_total = Replace(sql_total, "@tablejoin", jointable)
            sql_total = Replace(sql_total, "@CMFilter", CMFilter)
            sql_total = Replace(sql_total, "@RegionFilter", RegionFilter)
            sql_total = Replace(sql_total, "= @TagID", tagid)
            sql_total = Replace(sql_total, "@TagID", tagid)

            Return sql_total

        Catch ex As Exception

            If Not dr_sql Is Nothing Then
                dr_sql.Close()
                dr_sql = Nothing
            End If
            If Not comm_sql Is Nothing Then
                comm_sql.Dispose()
                comm_sql = Nothing
            End If
            If Not dr_element Is Nothing Then
                dr_element.Close()
                dr_element = Nothing
            End If
            If Not comm_Element Is Nothing Then
                comm_Element.Dispose()
                comm_Element = Nothing
            End If
            If Not conn_el Is Nothing Then
                conn_el.Close()
                conn_el.Dispose()
                conn_el = Nothing
            End If
            Return Nothing
        End Try
    End Function

    Private Sub AssignDataToCustomChart(ByRef dt As DataTable, ByVal tech As String)

        'Assign data to all charts
        Dim ch As Chart = CType(tlp_CustomChart.Controls(0), Chart)
        Dim i As Integer
        Dim Y1AxisLabel As String = "", Y2AxisLabel As String = ""
        Dim X1AxisLabel As String = "Date"
        Dim Y1axisAbsorPerc = "", Y2axisAbsOrPerc As String = ""
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim color_R, color_B, color_G As Integer
        Dim lastchart As String = ""
        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim chart_ElLineSize() As Integer = {0}
        Dim chart_ElShowDatapoints() As Boolean = {False}
        Dim chart_SeriesVisible() As Boolean = {True}
        Dim chart_SeriesAutoScale() As Boolean = {True}

        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim histChartArea As ChartArea = Nothing
        ch.DefaultSeries.DefaultElement.ShowValue = False

        '**************** Common Chart Type Settings ***********************
        'objectscharted = ObjectsCharted_Custom
        ch.TitleBox.Label.Text = "Objects: PLMN"
        ch.TitleBox.Position = TitleBoxPosition.Full
        ch.TitleBox.CornerTopLeft = BoxCorner.Round
        ch.TitleBox.CornerTopRight = BoxCorner.Round
        ch.XAxis.Minimum = Nothing
        ch.XAxis.Maximum = Nothing

        If CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Combo Then
            ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
            ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
            ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
            ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
            ch.XAxis.TimeInterval = TimeInterval.Days
            ch.XAxis.FormatString = "dd/MM/yy"
            ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
            ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
            ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
            ch.DefaultSeries.Type = SeriesType.Line

        ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Scatter Then
            ch.Type = ChartType.Scatter
            ch.Use3D = False
            ch.DefaultSeries.Type = SeriesType.Marker
            ch.DefaultSeries.DefaultElement.Transparency = 20
            ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
            ch.XAxis.FormatString = ""
            If tlvCustomChartsSeries.Nodes(0).SubItems(10).Text.ToUpper = "Y" Then
                Y1AxisLabel = tlvCustomChartsSeries.Nodes(0).SubItems(0).Text
            Else
                X1AxisLabel = tlvCustomChartsSeries.Nodes(0).SubItems(0).Text
            End If
            If tlvCustomChartsSeries.Nodes(1).SubItems(10).Text.ToUpper = "Y" Then
                Y1AxisLabel = tlvCustomChartsSeries.Nodes(1).SubItems(0).Text
            Else
                X1AxisLabel = tlvCustomChartsSeries.Nodes(1).SubItems(0).Text
            End If
            ch.XAxis.Label.Text = X1AxisLabel.Trim
            ch.DefaultSeries.DefaultElement.ShowValue = False
            ch.DefaultSeries.DefaultElement.Marker.Visible = True
            ch.LegendBox.Visible = False

        ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Histogram Then
            ch.Type = ChartType.Combo
            ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Vertical
            ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
            histChartArea = New ChartArea()
            histChartArea.HeightPercentage = 40
            histChartArea.YAxis.Label.Text = "Frequency"
            histChartArea.XAxis.Label.Text = "Bins"
            histChartArea.YAxis.Interval = 1
            ch.ExtraChartAreas.Add(histChartArea)

        ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Radar Then
            ch.Type = ChartType.Radar
            ch.XAxis.RadarMode = RadarMode.Polar
            ch.RadarLabelMode = RadarLabelMode.Outside
            ch.DefaultSeries.Type = SeriesType.AreaLine
            ch.DefaultSeries.DefaultElement.Transparency = 35

        ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Pie Then
            ch.Type = ChartType.Pie
            ch.PieLabelMode = PieLabelMode.Outside
            ch.DefaultSeries.DefaultElement.ShowValue = True
        End If

        For rownum = 0 To tlvCustomChartsSeries.Nodes.Count - 1
            Try
                'collecting elements from chart confguration
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.Nodes(rownum)

                'configures individual chart when new chartline is detected
                If lastchart = "" Or lastchart <> txt_CustomChartName.Text.Trim Then
                    lastchart = txt_CustomChartName.Text.Trim

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisAbsorPerc = nd.SubItems(7).Text.Trim
                    Else
                        Y2axisAbsOrPerc = nZ(nd.SubItems(7).Text.Trim, "Abs")
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                        Y2axisPrecision = 0
                    Else
                        Y1axisPrecision = 0
                        Y2axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        If nZ(nd.SubItems(8).Text.Trim, "").Length > 0 Then
                            Y1AxisLabel = nd.SubItems(8).Text.Trim
                        End If
                    Else
                        If nZ(nd.SubItems(8).Text, "").Length > 0 Then
                            Y2AxisLabel = nd.SubItems(8).Text.Trim
                        End If
                    End If

                    ch.TitleBox.HeaderLabel.Text = txt_Customize_Chart_Title.Text.Trim
                    If CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Scatter Then
                        ch.DefaultElement.Hotspot.ToolTip = X1AxisLabel & ": %XValue" & Chr(13) & Y1AxisLabel & ": %YValue "
                    ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Histogram Then
                        ch.DefaultElement.Hotspot.ToolTip = "%SeriesName: %Value "
                    Else
                        ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
                    End If

                    'Y-Axis Settings   
                    If ch.SeriesCollection.Count > 0 Then
                        For Each s As Series In ch.SeriesCollection
                            If s.YAxis.Orientation = Orientation.Left Then
                                yaxis1 = s.YAxis
                            End If
                            If s.YAxis.Orientation = Orientation.Right Then
                                yaxis2 = s.YAxis
                            End If
                        Next
                    End If

                    If yaxis1 Is Nothing Then
                        yaxis1 = New Axis()
                        yaxis1.Orientation = Orientation.Left
                    End If
                    yaxis1.Label.Text = Y1AxisLabel

                    If UCase(Y1axisAbsorPerc) = "PERC" Then
                        yaxis1.Percent = True
                        yaxis1.NumberPrecision = Y1axisPrecision
                    ElseIf UCase(Y1axisAbsorPerc) = "ABS" Then
                        yaxis1.NumberPrecision = Y1axisPrecision
                    End If

                    If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                        yaxis1.MinimumInterval = 1
                    End If

                    If yaxis2 Is Nothing Then
                        yaxis2 = New Axis()
                        yaxis2.Orientation = Orientation.Right
                    End If
                    yaxis2.Label.Text = Y2AxisLabel

                    If UCase(Y2axisAbsOrPerc) = "PERC" Then
                        yaxis2.Percent = True
                        yaxis2.NumberPrecision = Y2axisPrecision
                    ElseIf UCase(Y2axisAbsOrPerc) = "ABS" Then
                        yaxis2.NumberPrecision = Y2axisPrecision
                    End If

                    If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True And nd.SubItems(5).Text.Trim = "Right" Then
                        yaxis2.MinimumInterval = 1
                    End If

                    Do
                        If ColumnInDataTable(nd.SubItems(0).Text.Trim, dt) Then
                            ReDim Preserve chart_elements(j)
                            ReDim Preserve chart_elementsYAxis(j)
                            ReDim Preserve chart_Eltype(j)
                            ReDim Preserve chart_ElColor(j)
                            ReDim Preserve chart_ElLineSize(j)
                            ReDim Preserve chart_ElShowDatapoints(j)
                            ReDim Preserve chart_SeriesVisible(j)
                            ReDim Preserve chart_SeriesAutoScale(j)

                            chart_elements(j) = nd.SubItems(0).Text.Trim
                            chart_elementsYAxis(j) = nd.SubItems(5).Text.Trim
                            chart_Eltype(j) = nd.SubItems(2).Text
                            chart_ElColor(j) = CInt(nd.SubItems(4).Text.Trim)
                            chart_ElLineSize(j) = CInt(nd.SubItems(11).Text.Trim)
                            chart_ElShowDatapoints(j) = CBool(nd.SubItems(12).Text.Trim)
                            chart_SeriesVisible(j) = CBool(nZ(nd.SubItems(13).Text.Trim, True))
                            chart_SeriesAutoScale(j) = CBool(nZ(nd.SubItems(14).Text.Trim, True))

                            If UCase(chart_elementsYAxis(j)) = "LEFT" Then
                                chart_YaxisScale(0) = nd.SubItems(3).Text.Trim
                            ElseIf UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                                chart_YaxisScale(1) = nd.SubItems(3).Text.Trim
                            End If

                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > tlvCustomChartsSeries.Nodes.Count - 1 Then
                            Exit Do
                        Else
                            nd = tlvCustomChartsSeries.Nodes(rownum)
                        End If
                    Loop Until txt_CustomChartName.Text.Trim <> lastchart
                    rownum = rownum - 1

                    If UCase(chart_YaxisScale(0)) = "STACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    If UCase(chart_YaxisScale(1)) = "STACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    Dim de As DataEngine = New DataEngine(dt)
                    If ch.Type = ChartType.Scatter Then
                        de.DataFields = "XValue=" & X1AxisLabel & ",YValue=" & Y1AxisLabel.Replace(",", "\,")
                    Else
                        de.DataFields = String2DataFields(chart_elements, X1AxisLabel)
                    End If

                    de.DataGridFormatString = "N2"
                    de.FormatString = "dd/MM/yy"

                    Dim boundaries As String = Nothing
                    Dim sc As New SeriesCollection
                    sc = de.GetSeries()

                    If sc.Count > 0 Then

                        Dim LeftAxisDivisor As Int32 = 1
                        Dim RightAxisDivisor As Int32 = 1
                        Dim LeftAxisLabelAddition As String = ""
                        Dim RightAxisLabelAddition As String = ""

                        For i = 0 To sc.Count - 1
                            Dim MaxValueOfSeries As Double = sc(i).Calculate("test", Calculation.Maximum).YValue
                            If MaxValueOfSeries > 1000000000 Then
                                If MaxValueOfSeries > 1000000000000 Then
                                    Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                                        Case "LEFT"
                                            LeftAxisDivisor = 1000000
                                            LeftAxisLabelAddition = " Million"
                                            Exit Select
                                        Case "RIGHT"
                                            RightAxisDivisor = 1000000
                                            RightAxisLabelAddition = " Million"
                                            Exit Select
                                    End Select
                                Else
                                    Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                                        Case "LEFT"
                                            If LeftAxisDivisor < 1000 Then
                                                LeftAxisDivisor = 1000
                                                LeftAxisLabelAddition = " Thousand"
                                            End If
                                            Exit Select
                                        Case "RIGHT"
                                            If RightAxisDivisor < 1000 Then
                                                RightAxisDivisor = 1000
                                                RightAxisLabelAddition = " Thousand"
                                            End If
                                            Exit Select
                                    End Select
                                End If

                            End If
                        Next

                        For i = 0 To sc.Count() - 1
                            If ch.Type = ChartType.Combo Then
                                Select Case UCase(chart_Eltype(i).Trim)
                                    Case "LINE"
                                        sc(i).Type = SeriesType.Line
                                        sc(i).Line.Width = chart_ElLineSize(i)
                                    Case "BAR"
                                        sc(i).Type = SeriesType.Bar
                                    Case "AREALINE"
                                        sc(i).Type = SeriesType.AreaLine
                                End Select
                            ElseIf ch.Type = ChartType.Scatter Then
                                yaxis1.Label.Text = Y1AxisLabel
                                sc(i).Type = SeriesType.Marker
                            End If

                            Select Case chart_elementsYAxis(i).ToString().Trim().ToUpper()
                                Case "LEFT"
                                    If LeftAxisDivisor > 1 Then
                                        sc(i) = Series.Divide(sc(i), LeftAxisDivisor)
                                    End If
                                    If Not yaxis1.Label.Text.Contains(LeftAxisLabelAddition) Then
                                        yaxis1.Label.Text = yaxis1.Label.Text + LeftAxisLabelAddition
                                    End If
                                    sc(i).YAxis = yaxis1
                                    If chart_SeriesAutoScale(i) = False Then
                                        yaxis1.Minimum = 0
                                    End If
                                    Exit Select
                                Case "RIGHT"
                                    If RightAxisDivisor > 1 Then
                                        sc(i) = Series.Divide(sc(i), RightAxisDivisor)
                                    End If

                                    If Not yaxis2.Label.Text.Contains(RightAxisLabelAddition) Then
                                        yaxis2.Label.Text = yaxis2.Label.Text + RightAxisLabelAddition
                                    End If
                                    sc(i).YAxis = yaxis2
                                    If chart_SeriesAutoScale(i) = False Then
                                        yaxis2.Minimum = 0
                                    End If
                                    Exit Select
                            End Select

                            color_R = CLng(chart_ElColor(i)) Mod 256
                            color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                            color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                            sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)

                            If chart_ElShowDatapoints(i) = True Then
                                sc(i).DefaultElement.Marker.Type = ElementMarkerType.Circle
                                sc(i).DefaultElement.Marker.Size = 5
                                sc(i).EmptyElement.Mode = EmptyElementMode.None
                                sc(i).DefaultElement.Marker.Visible = True
                            Else
                                sc(i).DefaultElement.Marker.Type = ElementMarkerType.None
                                sc(i).DefaultElement.Marker.Visible = False
                            End If

                            If CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Histogram Then
                                If boundaries IsNot Nothing Then
                                    boundaries = boundaries & "," & sc(i).GetYValueList()
                                Else
                                    boundaries = sc(i).GetYValueList()
                                End If
                            End If

                            If chart_SeriesVisible(i) = True Then
                                sc(i).Visible = True
                            Else
                                sc(i).Visible = False
                            End If
                        Next

                        If CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Histogram Then
                            Dim boundariesArr As Double() = StringToDoubleArray(boundaries, New String() {","})
                            Dim minValue As Double = boundariesArr.Min()
                            Dim maxValue As Double = boundariesArr.Max()
                            Dim binsGap As Double = Math.Round((maxValue - minValue) / 30.0, 2)

                            Dim bins(29) As Double
                            For index As Integer = 0 To bins.Length - 1
                                bins(index) = minValue + (binsGap * (index + 1))
                            Next
                            histChartArea.XAxis.Minimum = minValue
                            histChartArea.XAxis.Interval = binsGap

                            For i = 0 To sc.Count() - 1
                                Dim freqTable As Series = StatisticalEngine.CFrequencyTableBOL(sc(i), bins)
                                'Dim freqTable As Series = StatisticalEngine.RFrequencyTableOL(sc(i), bins)
                                freqTable.Name = "Freq_" & sc(i).Name
                                freqTable.Type = SeriesType.Line
                                histChartArea.SeriesCollection.Add(freqTable)
                            Next
                        End If

                        ch.SeriesCollection.Add(sc)
                    End If

                    sc = Nothing
                    de = Nothing
                    ch.XAxis.Markers.Clear()
                    ch.RefreshChart()

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim chart_ElShowDatapoints(True)
                    ReDim chart_SeriesVisible(True)
                    ReDim chart_SeriesAutoScale(True)
                    j = 0
                End If

            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                Console.WriteLine(ex.Message.ToString)
            End Try
        Next

        System.GC.Collect()
    End Sub

    Private Function SQL_Construct_TopX(ByVal aggr_from As String, ByVal chartName As String) As String

        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim sql_select As String = Nothing
        Dim sql_where_misc As String = Nothing
        Dim sql_where_object As String = Nothing
        Dim sql_where_tables As String = Nothing
        Dim sql_where_period As String = Nothing
        Dim sql_groupby As String = Nothing
        Dim sql_orderby As String = Nothing
        Dim sql_from_time As String = Nothing
        Dim sql_kpi As String = Nothing
        Dim sql_total As String = Nothing
        Dim connectionString As String = Nothing

        Dim startdate As Date = CDate(dtpStartTime.EditValue)
        Dim enddate As Date = CDate(dtpEndTime.EditValue)
        Dim startdate_string As String = Nothing
        Dim enddate_string As String = Nothing
        'Dim DeltaDate1 As Date = Nothing
        'Dim DeltaDate2 As Date = Nothing

        startdate_string = Chr(39) & startdate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
        enddate_string = Chr(39) & enddate.ToString("yyyy-MM-dd HH:mm") & Chr(39)

        Dim conn_el As Odbc.OdbcConnection = Nothing
        Dim comm_sql As Odbc.OdbcCommand = Nothing
        Dim dr_sql As Odbc.OdbcDataReader = Nothing
        'Dim sql_sql As String

        Dim comm_Element As Odbc.OdbcCommand = Nothing
        Dim dr_element As Odbc.OdbcDataReader = Nothing
        Dim sqlelement As String = Nothing

        Try
            'Open connection to server
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()

            'objecttree selection to string
            Dim aggr_to As String = "PLMN"
            Dim objectsel As String = "IN ('PLMN')"

            If objectsel = "IN ()" Then
                conn_el.Close()
                conn_el.Dispose()
                Return Nothing
            End If

            'Dim CMFilter As String = Nothing
            'Dim tagid As String = Nothing
            'Dim RegionFilter As String = objectsel
            'If aggr_to = "TAGS" Then
            '    tagid = dtChartStyleProperties.Rows(0)("TagID").ToString
            '    aggr_to = dtChartStyleProperties.Rows(0)("AggregateTo").ToString
            '    If aggr_to.Contains("CM") Then
            '        CMFilter = dtChartStyleProperties.Rows(0)("Tags_Filter").ToString
            '    End If
            '    If aggr_to.Contains("Region") Then
            '        RegionFilter = dtChartStyleProperties.Rows(0)("RegionFilter").ToString
            '    End If
            'End If

            ' TG change
            'If rdoHourlyTopX.Checked = True Then
            'aggr_to = aggr_to & "_Hourly"
            'End If
            'set purpose

            Dim purpose As String = "TopX"
            comm_sql = New Odbc.OdbcCommand(clsSQLCommands.GetConstructStatsSQL(tech.Replace("TopX_", ""), purpose, aggr_to, aggr_from), conn_el)
            dr_sql = comm_sql.ExecuteReader
            sql_from_time = ""

            dr_sql.Read()
            If Not dr_sql.HasRows = 0 Then
                sql_select = dr_sql("sql_select").ToString.Trim
                'Dim deltaInterval As String = IIf(IsNumeric(dtChartStyleProperties.Rows(0)("TopX_DeltaInterval")), dtChartStyleProperties.Rows(0)("TopX_DeltaInterval"), "0")
                'If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                '    sql_from_time = " " & dr_sql("sql_time_hour").ToString.Trim
                '    connectionString = dr_sql("sql_time_hour_connStr").ToString.Trim
                '    DeltaDate1 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), startdate)
                '    DeltaDate2 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), enddate)

                'ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                sql_from_time = " " & dr_sql("sql_time_day").ToString.Trim
                connectionString = dr_sql("sql_time_day_connStr").ToString.Trim
                '    DeltaDate1 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), startdate)
                '    DeltaDate2 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), enddate)

                'ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH" Then
                '    sql_from_time = " " & dr_sql("sql_time_bh").ToString.Trim
                '    connectionString = dr_sql("sql_time_bh_connStr").ToString.Trim
                '    DeltaDate1 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), startdate)
                '    DeltaDate2 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), enddate)

                'ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly" Then
                '    sql_from_time = " " & dr_sql("sql_time_week").ToString.Trim
                '    connectionString = dr_sql("sql_time_week_connStr").ToString.Trim
                '    DeltaDate1 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), startdate)
                '    DeltaDate2 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), enddate)

                'ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH" Then
                '    sql_from_time = " " & dr_sql("sql_time_weekbh").ToString.Trim
                '    connectionString = dr_sql("sql_time_weekbh_connStr").ToString.Trim
                '    DeltaDate1 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), startdate)
                '    DeltaDate2 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), enddate)

                'ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                '    sql_from_time = " " & dr_sql("sql_time_raw").ToString.Trim
                '    connectionString = dr_sql("sql_time_raw_connStr").ToString.Trim
                '    DeltaDate1 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), startdate)
                '    DeltaDate2 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), enddate)

                'ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                '    sql_from_time = " " & dr_sql("sql_time_month").ToString.Trim
                '    connectionString = dr_sql("sql_time_month_connStr").ToString.Trim
                '    DeltaDate1 = DateAdd(DateInterval.Month, -1 * CInt(deltaInterval), startdate)
                '    DeltaDate2 = DateAdd(DateInterval.Month, -1 * CInt(deltaInterval), enddate)

                'End If

                sql_where_misc = " " & dr_sql("sql_where_misc").ToString.Trim
                If cmbObjectType.SelectedItem.ToString = "PLMN" Then
                    'to complete
                Else
                    sql_where_object = " " & Replace(dr_sql("sql_where_object"), "@object", objectsel).ToString.Trim
                End If
                sql_where_tables = " " & dr_sql("sql_where_tables").ToString.Trim

                'If chkEnableComparisonTopX.Checked = False Then
                sql_where_period = " " & Replace(Replace(dr_sql("sql_where_period"), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim
                Dim talias() As String = sql_where_period.Split(".")
                Dim table_alias As String = Replace(talias(0).Replace("AND", "").Trim(), "WHERE ", "")
                'sql_where_period = sql_where_period & filterPeriodstring.Replace("@alias", table_alias)
                'Else
                'sql_where_period = dr_sql("sql_where_period").ToString.Trim
                'End If

                sql_groupby = " " & dr_sql("sql_groupby").ToString.Trim
                sql_orderby = " " & dr_sql("sql_orderby").ToString.Trim
            Else
                dr_sql.Close()
                dr_sql = Nothing
                comm_sql.Dispose()
                comm_sql = Nothing
                conn_el.Close()
                conn_el.Dispose()
                conn_el = Nothing
                Return Nothing
            End If
            dr_sql.Close()
            dr_sql = Nothing
            comm_sql.Dispose()
            comm_sql = Nothing
            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            Dim supportcode As Integer = 0
            If aggr_from.Contains("RNC") Then
                supportcode = 1
            End If

            'KPI Tree Selection -> get string of selection
            'Dim selected_tabs As String = tvKPITopX.GetKPIChecked2String(1, "ObjectName")
            'Dim selected_charts As String = chartname
            'Dim selected_kpi As String = tvKPITopX.GetKPIChecked2String(3, "ObjectName")

            'get KPI sql
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()
            sqlelement = clsSQLCommands.GetChartKPISQL(tech.Replace("TopX_", ""), supportcode, chartName)
            comm_Element = New Odbc.OdbcCommand(sqlelement, conn_el)
            dr_element = comm_Element.ExecuteReader
            sql_kpi = ""

            While dr_element.Read
                sql_kpi = sql_kpi + " " + dr_element.GetValue(0).trim + ", "
            End While
            sql_kpi = sql_kpi.TrimEnd(" ")
            sql_kpi = sql_kpi.TrimEnd(",")

            conn_el.Close()
            comm_Element.Dispose()
            comm_Element = Nothing
            dr_element.Close()
            dr_element = Nothing
            conn_el.Dispose()
            conn_el = Nothing

            sql_total = sql_select + sql_kpi + " " + sql_from_time + sql_where_misc + sql_where_object + sql_where_tables + sql_where_period + sql_groupby + sql_orderby
            'sql_total = Replace(sql_total, "@CMFilter", CMFilter)
            'sql_total = Replace(sql_total, "@RegionFilter", RegionFilter)
            'sql_total = Replace(sql_total, "= @TagID", tagid)
            'sql_total = Replace(sql_total, "@TagID", tagid)
            'sql_total = Replace(sql_total, "@RegionFilter", RegionFilter)

            'If chkEnableComparisonTopX.Checked = True Then
            '    Dim delta1date_string As String = Chr(39) & DeltaDate1.ToString("yyyy-MM-dd HH:mm") & Chr(39)
            '    Dim delta2date_string As String = Chr(39) & DeltaDate2.ToString("yyyy-MM-dd HH:mm") & Chr(39)
            '    sql_total = Replace(Replace(sql_total, "@starttime", delta1date_string), "@endtime", delta2date_string) + ";" + Replace(Replace(sql_total, "@starttime", startdate_string), "@endtime", enddate_string)
            'End If

            Return sql_total
        Catch ex As Exception
            'Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)

            If Not dr_sql Is Nothing Then
                dr_sql.Close()
                dr_sql = Nothing
            End If
            If Not comm_sql Is Nothing Then
                comm_sql.Dispose()
                comm_sql = Nothing
            End If
            If Not dr_element Is Nothing Then
                dr_element.Close()
                dr_element = Nothing
            End If
            If Not comm_Element Is Nothing Then
                comm_Element.Dispose()
                comm_Element = Nothing
            End If
            If Not conn_el Is Nothing Then
                conn_el.Close()
                conn_el.Dispose()
                conn_el = Nothing
            End If

            Return Nothing
        End Try
    End Function

    Private Sub AssignDataToCustomChartTopX(ByRef dt As DataTable, ByVal tech As String)
        Dim connstringconfig As String = Nothing
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""

        'Dim ds_chart As DataSet = IOS.DataLibrary.clsSQLCommands.GetDataAssignToCustomChart(connStrIOSServer, tech, txt_CustomChartName.Text.ToString, cmbChartSetName.SelectedItem.ToString)
        'Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to chart
        Dim ch As Chart = CType(tlp_CustomChart.Controls(0), Chart)
        Dim X1AxisLabel As String = ""
        Dim i As Integer
        Dim Y1axislabel As String = "", Y2axislabel As String = ""
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis
        Dim yaxis2 As Axis
        Dim sp As New SmartPalette()
        Dim sc As New SeriesCollection
        Dim color_R, color_B, color_G As Integer
        Dim tblayout As TableLayoutPanel = Nothing
        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim chart_elsort() As String = {"0", "0"}
        Dim chart_elvis() As String = {"0"}
        Dim chart_SeriesVisible() As Boolean = {True}
        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim xval As String = ""

        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1


        If CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Combo Then
            ch.Type = ChartType.Combo
            ch.Use3D = False
            ch.XAxis.Label.Text = ""
            ch.LegendBox.Visible = True
            ch.DefaultSeries.Type = SeriesType.Bar
        ElseIf CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Scatter Then
            ch.Type = ChartType.Scatter
            ch.Use3D = False
            ch.DefaultSeries.Type = SeriesType.Marker
            ch.DefaultSeries.DefaultElement.Transparency = 20
            ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
            ch.XAxis.FormatString = ""
            If tlvCustomChartsSeries.Nodes(0).SubItems(10).Text.Trim = "Y" Then
                Y1axislabel = tlvCustomChartsSeries.Nodes(0).SubItems(0).Text.Trim
            Else
                X1AxisLabel = tlvCustomChartsSeries.Nodes(0).SubItems(0).Text.Trim
            End If
            If tlvCustomChartsSeries.Nodes(1).SubItems(10).Text.Trim = "Y" Then
                Y1axislabel = tlvCustomChartsSeries.Nodes(1).SubItems(0).Text.Trim
            Else
                X1AxisLabel = tlvCustomChartsSeries.Nodes(1).SubItems(0).Text.Trim
            End If
            ch.XAxis.Label.Text = X1AxisLabel.Trim
            ch.DefaultSeries.DefaultElement.ShowValue = False
            ch.DefaultSeries.DefaultElement.Marker.Visible = True
            ch.LegendBox.Visible = False
        End If

        For rownum = 0 To tlvCustomChartsSeries.Nodes.Count - 1
            Try
                'collecting elements from chart confguration
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.Nodes(rownum)

                'TESTING
                Dim tabindex_new As Integer = 99

                If tabindex_old <> tabindex_new Then
                    chartindex = 0
                    tabindex_old = tabindex_new
                Else
                    chartindex = chartindex + 1
                End If
                'configures individual chart when new chartline is detected
                If lastchart = "" Or lastchart <> txt_CustomChartName.Text.Trim Then
                    lastchart = txt_CustomChartName.Text.Trim
                    sp.Clear()

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisAbsorPerc = nd.SubItems(7).Text.Trim
                    Else
                        Y2axisAbsOrPerc = nZ(nd.SubItems(7).Text.Trim, "Abs")
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        Y1axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                        Y2axisPrecision = 0
                    Else
                        Y1axisPrecision = 0
                        Y2axisPrecision = CInt(nZ(nd.SubItems(6).Text, 0))
                    End If

                    If nd.SubItems(5).Text.ToLower = "left" Then
                        If nZ(nd.SubItems(8).Text.Trim, "").Length > 0 Then
                            Y1axislabel = nd.SubItems(8).Text.Trim
                        End If
                    Else
                        If nZ(nd.SubItems(8).Text, "").Length > 0 Then
                            Y2axislabel = nd.SubItems(8).Text.Trim
                        End If
                    End If

                    'If objTech IsNot Nothing Then

                    '    objTech.BindKPITreeViewTopX(objTech.Network)

                    '    'Dim tabindex As Integer = 0
                    '    If tech = networkAll.Network3G1 Then
                    '        TabIndex = customtabindex3g
                    '    ElseIf tech = networkAll.Network2G1 Then
                    '        TabIndex = customtabindex2g
                    '    ElseIf tech = networkAll.Network3G2 Then
                    '        TabIndex = customtabindexNano3G
                    '    ElseIf tech = networkAll.Network2G2 Then
                    '        TabIndex = customtabindexNanoBTS
                    '    ElseIf tech = networkAll.Network3G3 Then
                    '        TabIndex = customtabindex3g3
                    '    ElseIf tech = networkAll.Network2G3 Then
                    '        TabIndex = customtabindex2g3
                    '    Else
                    '        TabIndex = 99
                    '    End If

                    'Select Case tech.ToLower
                    '    Case "topx_" & objTech.Network.ToLower
                    '        xval = flpSourceBtn_GetChecked("topx_" & objTech.Network.ToLower, objTech.flpCounterTypeTopX)(0).SourceButtonText
                    '    Case Else
                    '        MsgBox("AssignData2Charts: problem in tech selection")
                    '        Exit Sub
                    'End Select

                    'End If

                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(tech))
                    ch.Annotations(0).DefaultCorner = BoxCorner.Square
                    ch.Annotations(0).Size = New Size(50, 25)
                    Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                    ch.Annotations(0).Label.Font = fnt
                    ch.TitleBox.HeaderLabel.Text = ""
                    ch.TitleBox.Label.Text = txt_Customize_Chart_Title.Text.Trim
                    ch.DefaultElement.Hotspot.ToolTip = "%SeriesName = %Value"

                    If CType(cmbChartType.SelectedItem, clsComboBoxItem).Value = IOSChartType.Scatter Then
                        ch.DefaultElement.Hotspot.ToolTip = X1AxisLabel & ": %XValue" & Chr(13) & Y1axislabel & ": %YValue "
                    End If

                    'Y-Axis Settingso   
                    yaxis1 = New Axis
                    yaxis1.Orientation = Orientation.Left
                    yaxis1.Label.Text = Y1axislabel

                    yaxis2 = New Axis
                    yaxis2.Orientation = Orientation.Right

                    'element based
                    Do
                        If ColumnInDataTable(nd.SubItems(0).Text.Trim, dt) Then
                            ReDim Preserve chart_elements(j)
                            ReDim Preserve chart_elementsYAxis(j)
                            ReDim Preserve chart_Eltype(j)
                            ReDim Preserve chart_ElColor(j)
                            ReDim Preserve chart_elvis(j)
                            ReDim Preserve chart_SeriesVisible(j)

                            chart_elements(j) = nd.SubItems(0).Text.Trim
                            chart_elementsYAxis(j) = nd.SubItems(5).Text.Trim
                            chart_Eltype(j) = nd.SubItems(2).Text.Trim
                            chart_ElColor(j) = CInt(nd.SubItems(4).Text.Trim)
                            chart_SeriesVisible(j) = CBool(nZ(nd.SubItems(13).Text.Trim, True))

                            If UCase(chart_elementsYAxis(j)) = "LEFT" Then
                                chart_YaxisScale(0) = nd.SubItems(3).Text.Trim
                            ElseIf UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                                chart_YaxisScale(1) = nd.SubItems(3).Text.Trim
                            End If
                            If nd.SubItems(9).Text.Trim <> "" Then
                                chart_elsort(0) = nd.SubItems(0).Text.Trim
                                chart_elsort(1) = nd.SubItems(9).Text.Trim.ToUpper()
                            End If
                            ''chart_elvis(j) = drow(20).ToString.Trim

                            If nd.SubItems(5).Text.ToLower = "left" Then
                                If nZ(nd.SubItems(8).Text.Trim, "").Length > 0 Then
                                    yaxis1.Label.Text = nd.SubItems(8).Text.Trim
                                End If
                            Else
                                If nZ(nd.SubItems(8).Text, "").Length > 0 Then
                                    yaxis2.Label.Text = nd.SubItems(8).Text.Trim
                                End If
                            End If

                            If nd.SubItems(5).Text.Trim = "Left" Then
                                If nZ(nd.SubItems(7).Text.Trim, " ").Length > 1 Then
                                    If nd.SubItems(7).Text.Trim.ToUpper = "PERC" Then
                                        yaxis1.Percent = True
                                    End If
                                End If
                            End If

                            If nd.SubItems(5).Text.Trim = "Right" Then
                                If nZ(nd.SubItems(7).Text.Trim, " ").Length > 1 Then
                                    If nd.SubItems(7).Text.Trim.ToUpper = "PERC" Then
                                        yaxis2.Percent = True
                                    End If
                                End If
                            End If

                            yaxis1.NumberPrecision = CInt(nZ(nd.SubItems(6).Text.Trim, 0))
                            yaxis2.NumberPrecision = CInt(nZ(nd.SubItems(6).Text.Trim, 0))

                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > tlvCustomChartsSeries.Nodes.Count - 1 Then
                            Exit Do
                        Else
                            nd = tlvCustomChartsSeries.Nodes(rownum)
                        End If
                    Loop Until txt_CustomChartName.Text.Trim <> lastchart
                    rownum = rownum - 1
                    nd = tlvCustomChartsSeries.Nodes(rownum)
                    'datagrid filling
                    'comment: need to skip element if not available in datatable!!
                    If chart_elsort(0) <> "0" Then
                        dt.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                    End If

                    'construct filter

                    Dim dt_topx As DataTable = Nothing
                    Dim cellcolumn As String = ""

                    'take only top 50 records for the chart data
                    dt_topx = GetTopXFromDataTable(dt.DefaultView.ToTable, 50, chart_elements)
                    cellcolumn = xval

                    If UCase(chart_YaxisScale(0)) = "STACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Normal
                    End If

                    If UCase(chart_YaxisScale(1)) = "STACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    ElseIf UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                    Else
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Normal
                    End If

                    If ch.Type = ChartType.Scatter Then
                        Try
                            ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
                            Dim minValue As Double = dt.Compute("Min(" & X1AxisLabel & ")", "")
                            Dim maxValue As Double = dt.Compute("Max(" & X1AxisLabel & ")", "")
                            ch.XAxis.ScaleRange.ValueLow = IIf(minValue < 0, Math.Floor(minValue), Math.Ceiling(minValue))
                            ch.XAxis.ScaleRange.ValueHigh = IIf(maxValue < 0, Math.Floor(maxValue), Math.Ceiling(maxValue))
                        Catch
                        End Try
                    End If

                    'chart filling
                    Dim de As DataEngine = New DataEngine(dt_topx)
                    If ch.Type = ChartType.Scatter Then
                        de.DataFields = "XValue=" & X1AxisLabel & ",YValue=" & Y1axislabel.Replace(",", "\,")
                    Else
                        de.DataFields = String2DataFields_TopX(chart_elements, xval, chart_elvis)
                    End If

                    sc = de.GetSeries()

                    For i = 0 To sc.Count() - 1
                        If ch.Type = ChartType.Combo Then
                            Select Case UCase(chart_Eltype(i).Trim)
                                Case "LINE"
                                    sc(i).Type = SeriesType.Line
                                    sc(i).Line.Width = CInt(nZ(nd.SubItems(11).Text, 0))
                                Case "BAR"
                                    sc(i).Type = SeriesType.Bar
                                Case "AREALINE"
                                    sc(i).Type = SeriesType.AreaLine
                            End Select
                        ElseIf ch.Type = ChartType.Scatter Then
                            yaxis1.Label.Text = Y1axislabel
                            sc(i).Type = SeriesType.Marker
                        End If

                        Select Case UCase(chart_elementsYAxis(i).Trim)
                            Case "LEFT"
                                sc(i).YAxis = yaxis1
                            Case "RIGHT"
                                sc(i).YAxis = yaxis2
                        End Select

                        If chart_SeriesVisible(i) = True Then
                            sc(i).Visible = True
                        Else
                            sc(i).Visible = False
                        End If

                        color_R = CLng(chart_ElColor(i)) Mod 256
                        color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                        color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                        sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
                    Next

                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    dt_topx.Dispose()
                    dt_topx = Nothing
                    sc = Nothing
                    de = Nothing

                    ch.RefreshChart()

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim Preserve chart_elvis(0)
                    ReDim Preserve chart_SeriesVisible(True)
                    j = 0
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                Console.WriteLine(ex.Message.ToString)
            End Try
        Next

    End Sub

    Private Function String2DataFields_TopX(ByVal str() As String, ByVal xval As String, ByVal elvis() As String) As String
        Dim stroutput As String
        Dim i As Integer
        stroutput = "XValue=" & xval.Replace(",", "\,") ' a(0)
        For i = 0 To UBound(str)
            stroutput = stroutput & "," & " Yvalue=" & str(i).Replace(",", "\,")
        Next
        String2DataFields_TopX = stroutput
    End Function

    Private Function GetTopXFromDataTable(ByVal dv As DataTable, ByVal topx As Integer, ByRef chartelements() As String) As DataTable
        Try
            Dim dt As DataTable = New DataTable()
            dt = dv.Clone

            Dim i As Integer
            Dim rowcount As Integer = dv.Rows.Count
            Dim rowinterate As Integer = topx
            Dim partofelements As Boolean = False
            Dim cols2del As New List(Of DataColumn)

            For Each col As DataColumn In dt.Columns
                partofelements = False
                For Each dr As DataRow In dt_IOS_ObjectConfig.Rows
                    If dr("Object").ToString.ToUpper = col.Caption.ToUpper Then
                        partofelements = True
                    End If
                Next
                Select Case col.Caption.ToUpper
                    Case "WBTS"
                        partofelements = True
                    Case "SITE"
                        partofelements = True
                    Case "CELL"
                        partofelements = True
                    Case "WCEL"
                        partofelements = True
                    Case "RNC"
                        partofelements = True
                    Case "BSC"
                        partofelements = True
                    Case "OPT_ZONE"
                        partofelements = True
                    Case "ZONE"
                        partofelements = True
                    Case Else
                        For i = 0 To UBound(chartelements)
                            If col.Caption.ToUpper = chartelements(i).ToUpper Then
                                partofelements = True
                            End If
                        Next
                End Select
                If partofelements = False Then
                    cols2del.Add(col)
                End If
            Next

            For Each el As DataColumn In cols2del
                dt.Columns.Remove(el)
            Next

            If rowcount > 0 Then
                If rowcount < topx Then
                    rowinterate = rowcount
                End If

                For i = 0 To rowinterate - 1
                    dt.ImportRow(dv.DefaultView.Table.Rows(i))
                Next
            End If

            dv.Dispose()
            dv = Nothing

            Return dt
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Sub SetMsgStatus(ByVal message As String)
        lblMsg.ForeColor = Color.Red
        lblMsg.Visible = True
        lblMsg.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Function GetPathFromTree(ByRef treeView As TreeView) As String
        If (treeView IsNot Nothing) Then
            If (treeView.SelectedNode.PrevNode IsNot Nothing) Then
                Return treeView.SelectedNode.PrevNode.FullPath
            ElseIf (treeView.SelectedNode.NextNode IsNot Nothing) Then
                Return treeView.SelectedNode.NextNode.FullPath
            Else
                Return treeView.SelectedNode.Parent.FullPath
            End If
        Else
            Return ""
        End If
    End Function

    Function GetNodeFromPath(ByVal nodes As TreeNodeCollection, ByVal path As String) As TreeNode
        Dim foundNode As TreeNode = Nothing
        If (nodes Is Nothing Or String.IsNullOrEmpty(path)) Then
            Return foundNode
        End If
        For Each tn As TreeNode In nodes
            If (tn.FullPath = path) Then
                tvCustomChartsCustom.SelectedNode = tn
                tvCustomChartsCustom.SelectedNode.EnsureVisible()
                tvCustomChartsCustom.Focus()
                Return tn
            ElseIf (tn.Nodes.Count > 0) Then
                foundNode = GetNodeFromPath(tn.Nodes, path)
            End If
            If (foundNode IsNot Nothing) Then
                Return foundNode
            End If
        Next
        Return Nothing
    End Function

    Private Function ValidateControls() As Boolean
        If (cmbTechnology.SelectedIndex > 0) Then
            Return True
        ElseIf cmbTechnology.Text = "" Or cmbTechnology.Text.Contains("Select Tech") Then
            SetMsgStatus("Select Technology Name.")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function String2DataFields(ByRef str() As String, ByRef xval As String) As String
        Dim stroutput As String
        Dim i As Integer

        stroutput = "XValue=" & xval ' a(0)
        For i = 0 To UBound(str)
            stroutput = stroutput & "," & " Yvalue=" & str(i).Replace(",", "\,")
        Next
        String2DataFields = stroutput
    End Function

    Public Sub CustomCharts_Chart_Commit(ByVal tech As String, ByVal chartname As String, ByVal charttitle As String, ByVal chartelement As String, ByVal chartelementtype As String, ByVal chartelementaxis As String,
                                         ByVal chartyaxisscaleprop As String, ByVal chartY1axisLabels As String, ByVal chartY2axisLabels As String, ByVal chartY1AbsPerc As String, ByVal chartY2AbsPerc As String, ByVal chartY1axisPrecision As Integer,
                                         ByVal chartY2axisPrecision As Integer, ByVal ChartElementsColor As Integer, ByVal SQLKPI_ID As Integer, ByVal serieOrder As String, ByVal categoryTab As String, ByVal categoryIndexTab As String,
                                         ByVal userid As String, ByVal chartIndex As String, ByVal ObjectTabIndex As String, ByVal ObjectTab As String, ByVal chartType As Integer, ByVal elementAxis As String, ByVal lineThickness As String,
                                         ByVal showDataPoints As Boolean, ByVal SeriesVisible As Boolean, ByVal AutoScale As Boolean, ByVal CrossTabObj As String, ByVal EnablePrdCalc As Boolean)
        Dim parray()() As String = {
            New String() {"@TechTab", Chr(39) & tech & Chr(39)},
            New String() {"@chartname", Chr(39) & chartname & Chr(39)},
            New String() {"@ChartTitle", Chr(39) & charttitle & Chr(39)},
            New String() {"@ChartElementName", Chr(39) & chartelement & Chr(39) & Chr(32)},
            New String() {"@ChartElementType", Chr(39) & chartelementtype & Chr(39)},
            New String() {"@ChartElementYAxis", Chr(39) & chartelementaxis & Chr(39)},
            New String() {"@chartYaxisScaleProp", Chr(39) & chartyaxisscaleprop & Chr(39)},
            New String() {"@chartY1axisLabels", Chr(39) & chartY1axisLabels & Chr(39)},
            New String() {"@chartY2axisLabels", Chr(39) & chartY2axisLabels & Chr(39)},
            New String() {"@chartY1AbsPerc", Chr(39) & chartY1AbsPerc & Chr(39)},
            New String() {"@chartY2AbsPerc", Chr(39) & chartY2AbsPerc & Chr(39)},
            New String() {"@chartY1axisPrecision", CInt(chartY1axisPrecision)},
            New String() {"@chartY2axisPrecision", CInt(chartY2axisPrecision)},
            New String() {"@ChartElementsColor", CInt(ChartElementsColor)},
            New String() {"@SQLKPI_ID", CInt(SQLKPI_ID)},
            New String() {"@serieorder", IIf(serieOrder <> "No Order", Chr(39) & serieOrder & Chr(39), "''")},
            New String() {"@userid", Chr(39) & userid & Chr(39)},
            New String() {"@categoryTab", Chr(39) & categoryTab & Chr(39)},
            New String() {"@categoryIndexTab", Chr(39) & categoryIndexTab & Chr(39)},
            New String() {"@ObjectIndex", Chr(39) & ObjectTabIndex & Chr(39)},
            New String() {"@ObjectTab", Chr(39) & ObjectTab & Chr(39)},
            New String() {"@chartIndex", Chr(39) & chartIndex & Chr(39)},
            New String() {"@chartType", CInt(chartType)},
            New String() {"@elementAxis", Chr(39) & elementAxis & Chr(39)},
            New String() {"@lineThickness", CInt(lineThickness)},
            New String() {"@showDataPoints", showDataPoints},
            New String() {"@SeriesVisible", SeriesVisible},
            New String() {"@AutoScale", AutoScale},
            New String() {"@CrossTabObj", CrossTabObj},
            New String() {"@EnablePeiodCalc", EnablePrdCalc}
        }

        Dim connstring As String = GetSQL(8704, parray)(0)
        Dim sql As String = GetSQL(8704, parray)(1)
        Dim dtQODBC As System.Data.DataTable = Nothing
        Try
            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)
        Catch ex As Exception
            SetMsgStatus(ex.Message.ToString)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try
    End Sub

    Public Sub CustomCharts_Chart_Delete(ByVal chartname As String, ByVal techtab As String, ByVal user As String, ByVal categoryTabIndex As String)
        Dim parray()() As String = {
            New String() {"@techTab", Chr(39) & techtab & Chr(39)},
            New String() {"@chartSetName", Chr(39) & user & Chr(39)},
            New String() {"@chartname", Chr(39) & chartname & Chr(39)},
            New String() {"@categoryTabName", Chr(39) & categoryTabIndex & Chr(39)}
        }

        Dim connstring As String = GetSQL(8706, parray)(0)
        Dim sql As String = GetSQL(8706, parray)(1)

        Dim dtQODBC As System.Data.DataTable = Nothing
        Try
            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try
    End Sub

    Public Function IsCategoryExist(ByVal newCategory As String) As Boolean
        Dim tech As String = cmbTechnology.SelectedItem.ToString
        Dim chartSetName As String = cmbChartSetName.SelectedItem.ToString
        Dim objecttab As String = cmbObjectType.SelectedItem.ToString
        If (tech IsNot Nothing AndAlso chartSetName IsNot Nothing AndAlso objecttab IsNot Nothing) Then
            Dim dtCategory As DataTable = clsSQLCommands.GetDistinctCategoryTab(connStrIOSServer, tech, chartSetName, objecttab, newCategory)
            If (dtCategory.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
        End If
        Return Nothing
    End Function

    Private Sub RefrashChartSeriesTLV(ByRef tlv As TreeListView)
        tlv.UpdateCurrentView()
        For Each col As TreeListViewColumn In tlv.Columns
            tlv.AutoSizeColumn(col)
        Next
        tlv.Columns(0).Width = tlv.Columns(0).Width + 10
        tlv.ResumeUpdate()
    End Sub

    Sub SetChartAxisColumnsWidth()
        tlvCustomChartsSeries.SuspendUpdate()
        For Each col As TreeListViewColumn In tlvCustomChartsSeries.Columns
            col.FixedWidth = False
            tlvCustomChartsSeries.AutoSizeColumn(col)
        Next
        tlvCustomChartsSeries.Refresh()
        tlvCustomChartsSeries.ResumeUpdate()
    End Sub

    Private Sub CustomCharts_Serie_Update_KPI(ByRef tlvnode As LidorSystems.IntegralUI.Lists.TreeListViewNode, ByVal KPI_Name As String, ByVal KPI_ID As Integer)
        Try
            tlvnode.SubItems(0).Text = KPI_Name
            tlvnode.SubItems(1).Text = KPI_ID
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub CustomCharts_Serie_Update(ByVal SerieColor As Integer, ByVal SerieType As String, ByVal SerieForm As String, ByVal yaxis_leftright As String, ByVal yaxis_left_label As String, ByVal yaxis_precision As String, ByVal yaxis_ABdPerc As String, Optional ByVal serieOrder As String = "")
        Try
            For Each tlvnode As TreeListViewNode In tlvCustomChartsSeries.Nodes
                tlvnode.SubItems(9).Text = ""
                tlvCustomChartsSeries.Refresh()
            Next

            For Each tlvnode As TreeListViewNode In tlvCustomChartsSeries.SelectedNodes
                tlvnode.SubItems(2).Text = SerieType
                tlvnode.SubItems(3).Text = SerieForm
                tlvnode.SubItems(4).Text = SerieColor
                tlvnode.SubItems(5).Text = yaxis_leftright
                tlvnode.SubItems(6).Text = yaxis_precision
                tlvnode.SubItems(7).Text = yaxis_ABdPerc
                tlvnode.SubItems(8).Text = yaxis_left_label
                tlvnode.SubItems(9).Text = serieOrder
                tlvCustomChartsSeries.Refresh()
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CustomCharts_Serie_UpdateAxis(ByVal colindex As Integer, ByVal replaceto As String)
        Try
            For Each nd As TreeListViewNode In tlvCustomChartsSeries.Nodes
                'If nd.Selected = True Then
                If nd.SubItems(5).Text.Trim.ToUpper = cmbCustomizeSerieAxis.Text.ToUpper Then
                    nd.SubItems(colindex).Text = replaceto
                End If
                'End If
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ProcessSurfaceChart(ByRef ds As DataSet, ByRef ch As Chart)
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfoDefault
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureUIDefault

            ch.TempDirectory = "temp"
            ch.Debug = True
            ch.Use3D = False
            ch.Type = ChartType.Surface
            ch.LegendBox.Visible = False
            ch.MarginTop = 60
            ch.DefaultSeries.Type = SeriesTypeSurface.Surface
            ch.XAxis.FormatString = ""
            ch.TitleBox.HeaderLabel.Text = txt_Customize_Chart_Title.Text.Trim

            ch.SeriesCollection.Clear()

            Dim chSC As SeriesCollection = GetSurfaceChartSeries(ds)
            ch.SmartPalette = chSC(0).GetSmartPalette(ElementValue.ZValue, Color.Blue, Color.Aqua, Color.LightGreen, Color.Yellow, Color.Orange, Color.Crimson)

            Dim an As Annotation = New Annotation(getSwatch(510, ch.SmartPalette, 10, True))
            an.Header.Label.Text = "<Chart:Scale min='" & ch.SmartPalette.GetScaleRange("*").ValueLow & "' max='" & ch.SmartPalette.GetScaleRange("*").ValueHigh & "' width='590' >"
            an.ClearColors()
            ch.Annotations.Add(an)
            an.Position = New Point(25, 10)
            an.DynamicSize = False

            ch.SeriesCollection.Add(chSC)
            chSC(0).Line.Transparency = 80

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim at As New AxisTick()
                at.Value = CInt(CDate(dr("Date")).ToOADate())
                at.Label.Text = CDate(dr("Date")).ToShortDateString
                ch.YAxis.ExtraTicks.Add(at)
            Next

            ch.RefreshChart()

        Catch ex As Exception
        End Try
        'System.Threading.Thread.CurrentThread.CurrentUICulture = Globalization.CultureInfo.GetCultureInfo("en-US")
        'System.Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.GetCultureInfo("en-US")
    End Sub

    Private Function GetSurfaceChartSeries(ByRef ds As DataSet) As SeriesCollection
        Dim PRBCols As New List(Of Integer)

        For Each col As DataColumn In ds.Tables(0).Columns
            If col.ColumnName.Contains("PRB") Then
                Dim colParts() As String = col.ColumnName.Split("_")
                PRBCols.Add(CInt(colParts(colParts.Count - 1)))
            End If
        Next

        Dim size As Integer = ds.Tables(0).Rows.Count
        Dim yAxis As New Axis()
        yAxis.Scale = dotnetCHARTING.WinForms.Scale.Range

        Dim xVals As Double() = New Double(PRBCols.Count - 1) {}
        Dim yVals As Double() = New Double(ds.Tables(0).Rows.Count - 1) {}
        Dim zVals As Double()() = New Double(PRBCols.Count - 1)() {}

        For i As Integer = 0 To PRBCols.Count - 1

            zVals(i) = New Double(ds.Tables(0).Rows.Count - 1) {}

            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim x As Double = CDbl(i)
                xVals(i) = x

                Dim y As Double = CDate(ds.Tables(0).Rows(j)("Date")).ToOADate()
                yVals(j) = y

                yAxis.Label.Text = ds.Tables(0).Rows(j)("Date").ToString

                zVals(i)(j) = CDbl(ds.Tables(0).Rows(j)("L_UL_Interference_Avg_PRB_" & CStr(i).PadLeft(2, "0")))

            Next j

        Next i

        Dim SC As SeriesCollection = New SeriesCollection()
        Dim s As Series = Series.FromSurfaceData("", xVals, yVals, zVals)

        SC.Add(s)

        Chart1.XAxis.Minimum = xVals(0)
        Chart1.XAxis.Maximum = xVals(xVals.Count - 1)
        Chart1.XAxis.Interval = 1

        Chart1.YAxis.Minimum = yVals(0)

        Return SC
    End Function

    Function getSwatch(ByVal width As Integer, ByVal sp As SmartPalette, ByVal divisions As Integer, ByVal withValues As Boolean) As String
        ' Get Maximum Value of the smart palette range
        Dim max As Double = CDbl(sp.GetScaleRange("*").ValueHigh)
        Dim min As Double = CDbl(sp.GetScaleRange("*").ValueLow)
        Dim swatch As String = "", spacers As String = ""
        Dim [step] As Double = (max - min) / divisions
        ' Width of each division.
        Dim boxWidth As Integer = width / divisions
        ' Generate swatch string for each division.
        For i As Integer = 0 To divisions
            spacers &= "<Chart:Spacer size='" & boxWidth & "x1'>"
            ' Get the color of the current division.
            Dim color As String = getHTMLColor(sp.GetValueColor("", min + (i * ([step]))))
            If withValues Then
                swatch &= "<block hAlignment='Center' bgColor='" & color & "'>" & Math.Round((min + (i * ([step]))), 1)
            Else
                swatch &= "<block bgColor='" & color & "' fColor='" & color & "'>_"
            End If
        Next i
        'return the swatch string.
        Return spacers & "<row>" & swatch
    End Function

    Function getHTMLColor(ByVal c As Color) As String
        Return "#" & c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2")
    End Function

    Public Sub ProcessStatsIndexedCombo_Custom(ds As DataSet, ch As Chart)
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfoDefault
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureUIDefault

            Dim dt_new As New DataTable
            Dim dt_new_sort As New DataTable
            Dim pvt_Pivot As Pivot = Nothing
            Dim dt_pivot As DataTable = Nothing
            Dim KPIname As String = Nothing

            Dim ds_chart As DataSet = clsSQLCommands.GetDataAssignToCustomChart(connStrIOSServer, cmbTechnology.SelectedItem.ToString, txt_CustomChartName.Text.Trim, cmbChartSetName.SelectedItem.ToString)
            Dim dt_chart As DataTable = ds_chart.Tables(0)
            dt_chart.DefaultView.Sort = "ChartElementID ASC"
            'KPIname = dt_chart.Rows(0).Item("ChartElements")

            Dim kpiColsCollection As New List(Of String)
            If Not dt_chart Is Nothing Then
                For Each drCE As DataRow In dt_chart.Rows
                    kpiColsCollection.Add(drCE("ChartElements"))
                Next
            End If

            For Each dt As DataTable In ds.Tables
                Dim displayView = New DataView(dt)
                dt_new = displayView.ToTable().Copy
            Next

            pvt_Pivot = New Pivot(dt_new)
            dt_pivot = pvt_Pivot.UnPivotData(AggregateFunction.Sum, kpiColsCollection.ToArray)

            'create chart
            AssignDataToIndexedChart_Custom(dt_pivot, ch)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AssignDataToIndexedChart_Custom(ByRef dt As DataTable, ByRef ch As Chart)
        Dim sqlchart As String
        Dim objectscharted As String = ""

        'this is to get objectcharted filled. 
        SetChartXAxis(ch)

        sqlchart = "SELECT * from qry_IOS_Configuration_Charts_1Table WHERE ChartName = " & Chr(39) & txt_CustomChartName.Text.Trim & Chr(39) & " And techtab = " & Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39)
        Dim ds_chart As DataSet = DataAccessorODBC.GetDataSet(connStrIOSServer, sqlchart)
        Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to all charts
        '*************************
        Dim i As Integer
        Dim Y1axislabel As String
        Dim Y2axislabel As String
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim sp As New SmartPalette()
        Dim sc As New SeriesCollection
        'Dim color_R, color_B, color_G As Integer

        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim chart_ElLineSize() As Integer = {0}
        Dim chart_ElShowDatapoints() As Boolean = {False}
        Dim chart_SeriesVisible() As Boolean = {True}

        Dim j As Integer = 0
        Dim rownum As Integer = 0

        ch.XAxis.Label.Text = ""
        ch.ExtraChartAreas.Clear()
        For rownum = 0 To 0
            Try
                'collecting elements from chart confguration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                'configures individual chart when new chartline is detected
                If lastchart = "" Or lastchart <> drow(5).ToString Then
                    lastchart = drow(5).ToString.Trim
                    sp.Clear()

                    Y1axisAbsorPerc = nZ(drow(13), "Abs")
                    Y2axisAbsOrPerc = nZ(drow(14), "Abs")

                    Y1axisPrecision = CInt(nZ(drow(15), "0"))
                    Y2axisPrecision = CInt(nZ(drow(16), "0"))

                    Y1axislabel = nZ(drow(11), " ")
                    Y2axislabel = nZ(drow(12), " ")

                    chart_elementsYAxis(0) = nZ(drow(9), " ")
                    chart_ElLineSize(0) = nZ(drow("LineSize"), 3)
                    chart_ElShowDatapoints(0) = nZ(drow("ShowDatapoints"), False)
                    chart_SeriesVisible(0) = nZ(drow("IsVisible"), True)

                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(cmbTechnology.SelectedItem.ToString.ToUpper))
                    ch.TitleBox.HeaderLabel.Text = drow("ChartTitle").Trim

                    ch.TitleBox.Label.Text = "Objects: " & objectscharted

                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near

                    ch.DefaultElement.Hotspot.ToolTip = "DateTimeElement: %XValue" & Chr(13) & "%SeriesName: %Value "
                    Dim charttitle As String = drow(6).Trim

                    'Y-Axis Settingso   
                    If chart_elementsYAxis(i).Trim.ToUpper = "LEFT" Then
                        yaxis1 = New Axis
                        yaxis1.Orientation = Orientation.Left
                        yaxis1.Label.Text = Y1axislabel
                        If UCase(Y1axisAbsorPerc) = "ABS" Then
                            yaxis1.Percent = False
                            yaxis1.NumberPrecision = Y1axisPrecision
                        End If
                        If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                            yaxis1.MinimumInterval = 1
                        End If
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    Else
                        yaxis2 = New Axis
                        yaxis2.Orientation = Orientation.Left
                        yaxis2.Label.Text = Y2axislabel
                        If UCase(Y2axisAbsOrPerc) = "PERC" Then
                            yaxis2.Percent = True
                            yaxis2.NumberPrecision = Y2axisPrecision
                        End If
                        If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                            yaxis2.MinimumInterval = 1
                        End If
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    j = 0

                    For Each col As DataColumn In dt.Columns
                        ReDim Preserve chart_elements(j)
                        If col.ColumnName.ToUpper <> "KPINAME" Then
                            chart_elements(j) = col.ColumnName.ToUpper
                            j = j + 1
                        End If
                    Next

                    Dim de As DataEngine = New DataEngine(dt)
                    de.DataFields = String2DataFields(chart_elements, "KPIName")
                    sc = de.GetSeries()

                    Dim rnd As Random = New Random(10)

                    For i = 0 To sc.Count() - 1
                        sc(i).Type = SeriesType.Line

                        If sc(i).Name = "DAY 0" Or sc(i).Name = "WEEK 0" Then
                            sc(i).Line.Width = 10
                        Else
                            sc(i).Line.Width = CInt(chart_ElLineSize(0))
                        End If

                        If yaxis1 IsNot Nothing Then
                            sc(i).YAxis = yaxis1
                        Else
                            sc(i).YAxis = yaxis2
                        End If

                        sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))

                        If chart_ElShowDatapoints(0) = True Then
                            sc(i).DefaultElement.Marker.Type = ElementMarkerType.Circle
                            sc(i).DefaultElement.Marker.Size = 5
                            sc(i).EmptyElement.Mode = EmptyElementMode.None
                            sc(i).DefaultElement.Marker.Visible = True
                        Else
                            sc(i).DefaultElement.Marker.Type = ElementMarkerType.None
                            sc(i).DefaultElement.Marker.Visible = False
                        End If

                        If chart_SeriesVisible(0) = True Then
                            sc(i).Visible = True
                        Else
                            sc(i).Visible = False
                        End If
                    Next

                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    sc = Nothing
                    de = Nothing

                    ch.XAxis.Markers.Clear()
                    ch.RefreshChart()
                    ch.Visible = True

                    Dim tn As TreeNode = New TreeNode()
                    tn.Text = charttitle
                    tn.Tag = ch.Name

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim chart_ElLineSize(0)
                    ReDim chart_ElShowDatapoints(False)
                    ReDim chart_SeriesVisible(True)

                    j = 0

                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                Console.WriteLine(ex.Message.ToString)
            End Try
        Next

        If Not ch Is Nothing Then
            ch.Series.Data = dt.Copy
        End If

        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing
        If Not dt Is Nothing Then
            dt.Dispose()
        End If
    End Sub

#End Region

#Region "Form Event"

    Private Sub frm_IOS_ChartCustomization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.SuspendLayout()
            BindTechCombo()
            ''BindObjectType()
            BindChartType()
            Me.ConfigurChartCustomizationForm("frmChartCustomization")
            Me.cmbGroupByAttribute.Enabled = False
            Me.ResumeLayout()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frm_IOS_ChartCustomization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.BringToFront()
            Me.TopMost = True
            If Me.WindowState = FormWindowState.Minimized Then
                Me.ShowInTaskbar = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frm_IOS_ChartCustomization_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ResizeBegin
        Me.SuspendLayout()
    End Sub

    Private Sub frm_IOS_ChartCustomization_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        Me.ResumeLayout()
    End Sub

#End Region

#Region "Control Events"

    Private Sub cmbChartSetName_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles cmbChartSetName.SelectedIndexChanged
        'clear chart
        If ValidateControls() Then
            UpdateKPITable()
        End If
    End Sub

    Private Sub tv_CustomCharts_Custom_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvCustomChartsCustom.AfterSelect
        Try
            If Not tvCustomChartsCustom.SelectedNode Is Nothing Then

                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                tlvCustomChartsSeries.Nodes.Clear()
                Dim treeNode As TreeNode = tvCustomChartsCustom.SelectedNode
                If (treeNode.Tag IsNot Nothing) Then
                    CustomCharts_FillData(treeNode.Tag.Trim)
                    tlvCustomChartsSeries.ResumeLayout()
                    If (treeNode.ToolTipText = "Category") Then
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.Category
                        If (cmbChartSetName.SelectedItem.ToString = chartSetName) Or (cmbChartSetName.SelectedItem.ToString <> Environment.UserName.ToString) Then
                            lblCategoryTab.Text = treeNode.Text
                            lblCategoryTabIndex.Text = treeNode.Tag
                        Else
                            lblCategoryTab.Text = "Custom"
                            lblCategoryTabIndex.Text = "99"
                        End If
                        txt_CustomChartName.Text = ""
                        txt_Customize_Chart_Title.Text = ""
                    ElseIf (treeNode.ToolTipText = "Chart") Then
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.Chart
                        txt_Customize_Chart_Title.Text = treeNode.Text.Trim
                        txt_CustomChartName.Text = treeNode.Tag.Trim
                        Dim dtChartData As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, "Select Top 1 * From IOS_Chart_Configuration Where ChartName = '" & treeNode.Tag.trim.ToString & "' And TechTab = '" & cmbTechnology.SelectedItem.ToString & "' And ChartSetName = '" & cmbChartSetName.SelectedItem.ToString & "';")
                        If dtChartData IsNot Nothing Then
                            Clipboard.SetText(dtChartData.Rows(0)("TechTab") & vbNewLine & dtChartData.Rows(0)("ObjectTab") & vbNewLine & Environment.UserName.ToString & vbNewLine & dtChartData.Rows(0)("CategoryTab") _
                                              & vbNewLine & dtChartData.Rows(0)("CategoryTabIndex") & vbNewLine & treeNode.Tag.Trim & vbNewLine & dtChartData.Rows(0)("ChartIndex") & vbNewLine & dtChartData.Rows(0)("ChartSetName") _
                                              & vbNewLine & dtChartData.Rows(0)("ObjectTabIndex"))

                            RemoveHandler cmbChartType.SelectedIndexChanged, AddressOf cmbChartType_SelectedIndexChanged

                            Dim itemValue As Integer = 0
                            If Not IsDBNull(dtChartData.Rows(0)("ChartType")) Then
                                itemValue = dtChartData.Rows(0)("ChartType")
                            End If
                            Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(itemValue, cmbChartType)
                            cmbChartType.SelectedItem = cmbItem

                            If itemValue = IOSChartType.Scatter Then
                                cmbElementAxis.Enabled = True
                            Else
                                cmbElementAxis.Enabled = False
                            End If

                            cmbGroupByAttribute.SelectedIndex = 0
                            cmbGroupByAttribute.Enabled = False
                            If cmbChartType.SelectedItem.ToString.ToLower = "groupperattribute" Then
                                cmbGroupByAttribute.Enabled = True
                                SetComboBox(cmbGroupByAttribute, ComboSelectBased.TextBased, dtChartData.Rows(0)("CrossTabObj").ToString)
                            End If

                            AddHandler cmbChartType.SelectedIndexChanged, AddressOf cmbChartType_SelectedIndexChanged
                        End If
                        If ((treeNode.Parent IsNot Nothing) AndAlso (treeNode.Parent.Tag IsNot Nothing) AndAlso (cmbChartSetName.SelectedItem.ToString = chartSetName) Or (cmbChartSetName.SelectedItem.ToString <> Environment.UserName.ToString)) Then
                            lblCategoryTab.Text = treeNode.Parent.Text
                            lblCategoryTabIndex.Text = treeNode.Parent.Tag
                        Else
                            lblCategoryTab.Text = "Custom"
                            lblCategoryTabIndex.Text = "99"
                        End If
                    Else
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.NotSelected
                        txt_CustomChartName.Text = ""
                        txt_Customize_Chart_Title.Text = ""
                    End If
                Else
                    Me.TreeSelectionType = IOS.Library.TreeSelectionType.NotSelected
                    txt_CustomChartName.Text = ""
                    txt_Customize_Chart_Title.Text = ""
                End If
            End If
            If tlvCustomChartsSeries.Nodes.Count > 0 Then
                tlvCustomChartsSeries.FocusedNode = tlvCustomChartsSeries.Nodes(0)
                tlvCustomChartsSeries.Nodes(0).Selected = True
                tlvCustomChartsSeries_SubItemSelectionChanged(Nothing, Nothing)
            Else
                SetDefaultValue()
            End If

            If cmbCustomizeSerieType.SelectedIndex = -1 Then
                SetDefaultValue()
            End If
            CustomCharts_Update()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tlvCustomChartsSeries_DragDrop(sender As Object, e As DragEventArgs) Handles tlvCustomChartsSeries.DragDrop, Chart1.DragDrop
        Dim items As System.Data.DataTable = e.Data.GetData("System.Data.DataTable")
        Try
            If (items IsNot Nothing) Then
                Dim serieorder As String = ""
                'Dim colorint As Integer = ColorTranslator.ToOle(cpCustomizeSerieColor.Color)

                Dim pnt As System.Drawing.Point = tlvCustomChartsSeries.PointToClient(New System.Drawing.Point(e.X, e.Y))
                Dim tlvnodetest As TreeListViewNode = tlvCustomChartsSeries.GetNodeAt(pnt)

                If cmbChartType.SelectedIndex = -1 Then
                    MsgBox("Select Chart Type", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                If cmbCustomizeSerieType.SelectedIndex = -1 Then
                    MsgBox("Select Series Type", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If cmbCustomizeSerieAxisType.SelectedIndex = -1 Then
                    MsgBox("Select Axis Type", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If cmbCustomizeSerieAxis.SelectedIndex = -1 Then
                    MsgBox("Select Axis Location", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If txtCustomChartsAxisLabel Is Nothing Then
                    txtCustomChartsAxisLabel.Text = " "
                    ' MsgBox("Add Axis Label", MsgBoxStyle.Exclamation)
                    ' Exit Sub
                End If
                'txt_CustomCharts_AxisLabel.Text = IIf(String.IsNullOrEmpty(txt_CustomCharts_AxisLabel.Text), " ", txt_CustomCharts_AxisLabel.Text)
                If nudCustomizeChartPrecision Is Nothing Then
                    MsgBox("Select Series Precision", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If cmbCustomChartsAbsPerc.SelectedIndex = -1 Then
                    MsgBox("Select Abs/Perc", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                If tlvnodetest Is Nothing Then
                    For Each Item As DataRow In items.Rows
                        Dim rnd As Random = New Random(rndCntr)
                        Dim clr As Color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))
                        CustomCharts_Serie_Insert(Item(1).ToString, Item(0), ColorTranslator.ToOle(clr), cmbCustomizeSerieType.SelectedItem.ToString, cmbCustomizeSerieAxisType.SelectedItem.ToString, cmbCustomizeSerieAxis.SelectedItem.ToString,
                                                  txtCustomChartsAxisLabel.Text, nudCustomizeChartPrecision.EditValue, cmbCustomChartsAbsPerc.SelectedItem.ToString, "", cmbElementAxis.Text.Trim, spEdLineThickness.EditValue,
                                                  cmbShowDatapoints.SelectedItem.ToString, chkSeriesVisible.Checked, cmbCustomChartAutoScale.SelectedItem.ToString, IIf(cmbGroupByAttribute.SelectedIndex = 0, "", cmbGroupByAttribute.SelectedItem.ToString), chkEnablePeriodCalc.Checked)
                        rndCntr = rndCntr + 1
                    Next
                Else
                    For Each Item As DataRow In items.Rows
                        CustomCharts_Serie_Update_KPI(tlvnodetest, Item(1).ToString, Item(0))
                    Next
                End If

                ''tlv_CustomCharts_Series.SuspendUpdate()
                'tlv_CustomCharts_Series.UpdateCurrentView()
                'For Each col As TreeListViewColumn In tlv_CustomCharts_Series.Columns
                '    'col.FixedWidth = False

                '    tlv_CustomCharts_Series.AutoSizeColumn(col)
                '    'tlv_CustomCharts_Series.Refresh()
                'Next

                ''tlv_CustomCharts_Series.Columns(0).Width = tlv_CustomCharts_Series.Columns(0).Width + 10
                ''tlv_CustomCharts_Series.Refresh()
                'tlv_CustomCharts_Series.ResumeUpdate()

                RefrashChartSeriesTLV(tlvCustomChartsSeries)
                tlvCustomChartsSeries_SubItemSelectionChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tlvCustomChartsSeries_SubItemSelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlvCustomChartsSeries.SubItemSelectionChanged
        bl_CustomSerieSelected = True
        Try
            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(2).Text.Trim.ToLower
                Case "bar"
                    cmbCustomizeSerieType.SelectedItem = cmbCustomizeSerieType.Properties.Items(0)
                Case "line"
                    cmbCustomizeSerieType.SelectedItem = cmbCustomizeSerieType.Properties.Items(1)
            End Select

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(3).Text.Trim.ToLower
                Case "normal"
                    cmbCustomizeSerieAxisType.SelectedItem = cmbCustomizeSerieAxisType.Properties.Items(0)
                Case "stacked"
                    cmbCustomizeSerieAxisType.SelectedItem = cmbCustomizeSerieAxisType.Properties.Items(1)
                Case "fullstacked"
                    cmbCustomizeSerieAxisType.SelectedItem = cmbCustomizeSerieAxisType.Properties.Items(2)
            End Select

            If (gvCustomizeSerieKPI.RowCount > 0) Then
                gcCustomizeSerieKPI.SuspendLayout()
                gvCustomizeSerieKPI.ClearSelection()
                Dim dtItem As DataTable = gcCustomizeSerieKPI.DataSource

                For Each dr As DataRow In dtItem.Select("" & dtItem.Columns(0).Caption & "='" & tlvCustomChartsSeries.SelectedNode.SubItems(1).Text.ToString & "'")
                    gvCustomizeSerieKPI.SelectRow(gvCustomizeSerieKPI.FindRow(dr))
                Next

                ''Dim Items As IEnumerable(Of HierarchyItem) = From w In grvCustomizeSerieKPI.HierarchyItem.SelectedItems _
                ''       Where w.Cells(0).Value = tlvCustomChartsSeries.SelectedNode.SubItems(1).Text.ToString _
                ''       Select w

                ''For Each Item As HierarchyItem In Items
                ''    grvCustomizeSerieKPI.RowsHierarchy.SelectItem(Item)
                ''Next
                gcCustomizeSerieKPI.Refresh()
                gcCustomizeSerieKPI.ResumeLayout()
            End If

            cpCustomizeSerieColor.Color = ColorTranslator.FromOle(CInt(tlvCustomChartsSeries.SelectedNode.SubItems(4).Text))
            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(5).Text.Trim.ToLower
                Case "left"
                    cmbCustomizeSerieAxis.SelectedItem = cmbCustomizeSerieAxis.Properties.Items(0)
                Case "right"
                    cmbCustomizeSerieAxis.SelectedItem = cmbCustomizeSerieAxis.Properties.Items(1)
            End Select

            nudCustomizeChartPrecision.Text = CInt(tlvCustomChartsSeries.SelectedNode.SubItems(6).Text.Trim)

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(7).Text.Trim.ToLower
                Case "abs"
                    cmbCustomChartsAbsPerc.SelectedItem = cmbCustomChartsAbsPerc.Properties.Items(0)
                Case "perc"
                    cmbCustomChartsAbsPerc.SelectedItem = cmbCustomChartsAbsPerc.Properties.Items(1)
            End Select

            txtCustomChartsAxisLabel.Text = tlvCustomChartsSeries.SelectedNode.SubItems(8).Text.Trim

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(9).Text.Trim.ToLower
                Case "asc"
                    cmbCustomizeSeriesOrder.SelectedItem = cmbCustomizeSeriesOrder.Properties.Items(1)
                Case "desc"
                    cmbCustomizeSeriesOrder.SelectedItem = cmbCustomizeSeriesOrder.Properties.Items(2)
                Case ""
                    cmbCustomizeSeriesOrder.SelectedItem = cmbCustomizeSeriesOrder.Properties.Items(0)
            End Select

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(10).Text.Trim.ToLower
                Case "x"
                    cmbElementAxis.SelectedItem = cmbElementAxis.Properties.Items(0)
                Case "y"
                    cmbElementAxis.SelectedItem = cmbElementAxis.Properties.Items(1)
            End Select

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(11).Text.Trim.ToLower
                Case "0"
                    spEdLineThickness.EditValue = 0
                Case "1"
                    spEdLineThickness.EditValue = 1
                Case "2"
                    spEdLineThickness.EditValue = 2
                Case "3"
                    spEdLineThickness.EditValue = 3
            End Select

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(12).Text.Trim.ToLower
                Case "true"
                    cmbShowDatapoints.SelectedIndex = 0
                Case "false"
                    cmbShowDatapoints.SelectedIndex = 1
            End Select

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(13).Text.Trim.ToLower
                Case "true"
                    chkSeriesVisible.Checked = True
                Case "false"
                    chkSeriesVisible.Checked = False
            End Select

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(14).Text.Trim.ToLower
                Case "true"
                    cmbCustomChartAutoScale.SelectedItem = 0
                Case "false"
                    cmbCustomChartAutoScale.SelectedItem = 1
            End Select

            Select Case tlvCustomChartsSeries.SelectedNode.SubItems(16).Text.Trim.ToLower
                Case "true"
                    chkEnablePeriodCalc.Checked = True
                Case "false"
                    chkEnablePeriodCalc.Checked = False
            End Select

        Catch
        End Try
        bl_CustomSerieSelected = False
    End Sub

    Private Sub btn_Customize_Chart_SeriesRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomizeChartSeriesRemove.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim lstNodes As New List(Of TreeListViewNode)
            Dim lstSelectedNodes As New List(Of TreeListViewNode)

            For Each nd As TreeListViewNode In tlvCustomChartsSeries.Nodes
                If nd.Selected = True Then
                    lstSelectedNodes.Add(nd)
                Else
                    lstNodes.Add(nd)
                End If
            Next

            tlvCustomChartsSeries.Nodes.Clear()

            For Each tlvn As TreeListViewNode In lstNodes
                tlvCustomChartsSeries.Nodes.Add(tlvn)
            Next

            For Each tlvn As TreeListViewNode In lstSelectedNodes
                tlvn.Remove()
            Next

            For Each col As TreeListViewColumn In tlvCustomChartsSeries.Columns
                If col.HeaderText.ToUpper = "KPI" Then
                    col.Width = 200
                End If
                tlvCustomChartsSeries.AutoSizeColumn(col)
            Next
            tlvCustomChartsSeries.UpdateLayout()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btn_CustomChartSeries_Commit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustomChartSeriesCommit.Click

        'check constraints
        If txt_CustomChartName.Text = "" Or txt_Customize_Chart_Title.Text = "" Then
            MsgBox("Please define a Chart Name and Chart Title before committing")
            Exit Sub
        End If
        If cmbChartType.SelectedIndex = -1 Then
            MsgBox("Please select a Chart Type")
            Exit Sub
        End If

        'Checking whether no rows or KPI-Id = 0 i.e. No KPI is dragged
        If tlvCustomChartsSeries.Nodes.Count = 0 OrElse tlvCustomChartsSeries.Nodes(0).SubItems(1).Text = "0" Then
            MsgBox("Chart must have at least one KPI", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Allow configurable no. of KPIs for a chart type (-1 means no limitation)
        If CType(cmbChartType.SelectedItem, clsComboBoxItem).Tag <> -1 Then
            If CType(cmbChartType.SelectedItem, clsComboBoxItem).Tag <> tlvCustomChartsSeries.Nodes.Count Then
                MsgBox(cmbChartType.Text.Trim & " chart type cannot have more than " & CType(cmbChartType.SelectedItem, clsComboBoxItem).Tag & " KPI(s)", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        'If cmbChartType.Text.Trim.ToLower = "groupperattribute" Then
        '    If cmbCustomizeSerieAxisType.Text.Trim.ToLower <> "normal" Then
        '        MsgBox(cmbChartType.Text.Trim & " chart type can only have axis type normal", MsgBoxStyle.Exclamation)
        '        Exit Sub
        '    End If
        'End If

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        'delete chart
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            'loop through nodes
            Dim dtQODBC As System.Data.DataTable = Nothing
            dtQODBC = clsSQLCommands.GetCustomChartIndex(connStrIOSServer, cmbTechnology.SelectedItem.ToString, cmbChartSetName.SelectedItem.ToString, txt_CustomChartName.Text.Trim, lblCategoryTab.Text.Trim)
            Dim chartIndex As String = "0"
            If (dtQODBC.Rows.Count > 0) Then
                chartIndex = dtQODBC.Rows(0)(0).ToString()
            Else
                chartIndex = 0
            End If
            For Each nd As TreeListViewNode In tlvCustomChartsSeries.Nodes
                If nd.SubItems(5).Text.ToLower = "left" Then
                    CustomCharts_Chart_Commit(cmbTechnology.SelectedItem.ToString, txt_CustomChartName.Text, txt_Customize_Chart_Title.Text.Trim, nd.SubItems(0).Text, nd.SubItems(2).Text, nd.SubItems(5).Text, nd.SubItems(3).Text,
                                              nd.SubItems(8).Text, "", nd.SubItems(7).Text, "", CInt(nd.SubItems(6).Text), 0, CInt(nd.SubItems(4).Text), CInt(nd.SubItems(1).Text), nd.SubItems(9).Text, lblCategoryTab.Text, lblCategoryTabIndex.Text,
                                              cmbChartSetName.SelectedItem.ToString, chartIndex, TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value, cmbObjectType.Text, CType(cmbChartType.SelectedItem, clsComboBoxItem).Value, nd.SubItems(10).Text,
                                              nd.SubItems(11).Text, nd.SubItems(12).Text, nd.SubItems(13).Text, nd.SubItems(14).Text, IIf(cmbGroupByAttribute.SelectedIndex = 0, "", cmbGroupByAttribute.SelectedItem.ToString), nd.SubItems(16).Text)
                Else
                    CustomCharts_Chart_Commit(cmbTechnology.SelectedItem.ToString, txt_CustomChartName.Text, txt_Customize_Chart_Title.Text.Trim, nd.SubItems(0).Text, nd.SubItems(2).Text, nd.SubItems(5).Text, nd.SubItems(3).Text, "",
                                              nd.SubItems(8).Text, "", nd.SubItems(7).Text, 0, CInt(nd.SubItems(6).Text), CInt(nd.SubItems(4).Text), CInt(nd.SubItems(1).Text), nd.SubItems(9).Text, lblCategoryTab.Text, lblCategoryTabIndex.Text,
                                              cmbChartSetName.SelectedItem.ToString, chartIndex, TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value, cmbObjectType.Text, CType(cmbChartType.SelectedItem, clsComboBoxItem).Value, nd.SubItems(10).Text,
                                              nd.SubItems(11).Text, nd.SubItems(12).Text, nd.SubItems(13).Text, nd.SubItems(14).Text, IIf(cmbGroupByAttribute.SelectedIndex = 0, "", cmbGroupByAttribute.SelectedItem.ToString), nd.SubItems(16).Text)
                End If
            Next
            SetMsgStatus("Chart committed successfully")
            Application.DoEvents()
            CustomCharts_Update()
            'reload kpitree
            ReloadKPITree(VendorTech)
        Catch ex As Exception
            SetMsgStatus(ex.Message.ToString)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            Me.Focus()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub ReloadKPITree(ByVal vTech As String)
        Try

            objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(VendorTech.Replace("TopX_", "").ToUpper)).LastOrDefault()

            If objfrmTechnology IsNot Nothing Then
                If cmbTechnology.SelectedItem.ToString.ToLower.Contains("topx") Then
                    IOS_KPITrees_Load(vTech, objfrmTechnology.cmbChartSetNameTopX.Text)
                    objfrmTechnology.KPITreeTopXDS = frmMDI.GetKPITreeByTecnology(objfrmTechnology.BaseTechnology, EnumStatsOrTopX.TOPX)
                    objfrmTechnology.KPITreeDataTopX = objfrmTechnology.GetKPIDS(vTech, objfrmTechnology.TechInternal, EnumStatsOrTopX.TOPX)
                    objfrmTechnology.BindKPITreeViewTopX(vTech)
                Else
                    IOS_KPITrees_Load(vTech, objfrmTechnology.cmbChartSetNameStats.Text)
                    objfrmTechnology.KPITreeStatsDS = frmMDI.GetKPITreeByTecnology(objfrmTechnology.BaseTechnology, EnumStatsOrTopX.STATS)
                    objfrmTechnology.KPITreeDataStats = objfrmTechnology.GetKPIDS(vTech, objfrmTechnology.TechInternal, EnumStatsOrTopX.STATS)
                    objfrmTechnology.BindKPITreeViewStats(vTech)
                    For Each tblBtn As IOS.Library.IOSToggleButton In objfrmTechnology.flpCounterTypeStats.Controls
                        If tblBtn.ToggleState = CheckState.Checked Then
                            objfrmTechnology.flpSourceBtn_ToggleChanges(tblBtn, Nothing)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            SetMsgStatus(ex.Message.ToString)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMsg.Text = ""
        lblMsg.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub tv_CustomCharts_Custom_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvCustomChartsCustom.AfterLabelEdit
        Try
            Dim node As TreeNode = e.Node
            If (e.Label IsNot Nothing) Then
                If Not (e.Label = node.Text) Then
                    Dim connstring As String = Nothing
                    Dim sql As String = Nothing
                    If (node.ToolTipText = "Chart") Then
                        Dim parray()() As String = {
                            New String() {"@newTitle", Chr(39) & e.Label.TrimEnd(" ") & Chr(39)},
                            New String() {"@chartname", Chr(39) & e.Node.Tag.TrimEnd(" ") & Chr(39)},
                            New String() {"@techtab", Chr(39) & cmbTechnology.SelectedItem.ToString.TrimEnd(" ") & Chr(39)},
                            New String() {"@userid", Chr(39) & cmbChartSetName.SelectedItem.ToString & Chr(39)},
                            New String() {"@objecttab", Chr(39) & cmbObjectType.SelectedItem.ToString.TrimEnd(" ") & Chr(39)}
                        }

                        connstring = GetSQL(8708, parray)(0)
                        sql = GetSQL(8708, parray)(1)
                        node.Text = e.Label.ToString.Trim
                    ElseIf (node.ToolTipText = "Category") Then
                        If (e.Label.TrimEnd(" ").Length <= 20) Then
                            If Not (e.Label.TrimEnd(" ").ToUpper = "CUSTOM") Then
                                If (IsCategoryExist(e.Label)) Then
                                    Dim parray()() As String = {
                                        New String() {"@categoryNew", Chr(39) & e.Label.TrimEnd(" ") & Chr(39)},
                                        New String() {"@categoryTab", Chr(39) & e.Node.Text.TrimEnd(" ") & Chr(39)},
                                        New String() {"@categoryIndexTab", Chr(39) & e.Node.Tag.TrimEnd(" ") & Chr(39)},
                                        New String() {"@TechTab", Chr(39) & cmbTechnology.SelectedItem.ToString.TrimEnd(" ") & Chr(39)},
                                        New String() {"@userid", Chr(39) & cmbChartSetName.SelectedItem.ToString & Chr(39)},
                                        New String() {"@objecttab", Chr(39) & cmbObjectType.SelectedItem.ToString.TrimEnd(" ") & Chr(39)}
                                    }

                                    connstring = GetSQL(8710, parray)(0)
                                    sql = GetSQL(8710, parray)(1)
                                    DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                                    node.Text = e.Label.ToString.Trim
                                Else
                                    MsgBox("Category already exist.")
                                    e.CancelEdit = True
                                End If
                            Else
                                MsgBox("The name Custom can't be use.")
                                e.CancelEdit = True
                            End If
                        Else
                            MsgBox("Category Must be lessthen 20 charactors")
                            e.CancelEdit = True
                            Exit Sub
                        End If
                    ElseIf (node.ToolTipText = "ChartSetName") Then
                        Dim parray()() As String = {
                            New String() {"@TechTab", Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39)},
                            New String() {"@ObjectTab", Chr(39) & cmbObjectType.SelectedItem.ToString & Chr(39)},
                            New String() {"@NewChartSetName", Chr(39) & e.Label.ToString.Trim & Chr(39)},
                            New String() {"@OldChartSetName", Chr(39) & cmbChartSetName.SelectedItem.ToString & Chr(39)}
                        }
                        connstring = GetSQL(8715, parray)(0)
                        sql = GetSQL(8715, parray)(1)
                        Dim dt As DataTable = DataAccessorODBC.GetDataTable(connstring, sql)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)(0).ToString.ToLower = "updated" Then
                                node.Text = e.Label.ToString.Trim
                                BindChartSetName()
                                SetComboBox(cmbChartSetName, ComboSelectBased.ValueBased, e.Label.ToString.Trim)
                                XtraMessageBox.Show("ChartSet updated successfully", "Rename ChartSet", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ElseIf dt.Rows(0)(0).ToString.ToLower = "alreadyexists" Then
                                XtraMessageBox.Show("ChartSet already exists", "Rename ChartSet", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                e.CancelEdit = True
                                Exit Sub
                            ElseIf dt.Rows(0)(0).ToString.ToLower = "csnasuser" Then
                                XtraMessageBox.Show("Any username cannot be set as ChartSet", "Rename ChartSet", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                e.CancelEdit = True
                                Exit Sub
                            End If
                        End If
                    End If

                    If (node.ToolTipText = "Chart") Then
                        Dim dtQODBC As System.Data.DataTable = Nothing
                        Try
                            Dim k As Integer = DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                            txt_Customize_Chart_Title.Text = e.Label.TrimEnd(" ")
                            txt_CustomChartName.Text = e.Node.Tag.TrimEnd(" ")
                        Catch ex As Exception
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                        Finally
                            If Not dtQODBC Is Nothing Then
                                dtQODBC.Dispose()
                                dtQODBC = Nothing
                            End If
                            'reload kpitree
                            ReloadKPITree(VendorTech)
                        End Try
                        Return
                    End If
                End If
            End If
            e.CancelEdit = True
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub vgrv_Customize_Serie_KPI_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs)
        Dim pnt As System.Drawing.Point = tlvCustomChartsSeries.PointToClient(New System.Drawing.Point(e.X, e.Y))
        Dim tlvnodetest As TreeListViewNode = tlvCustomChartsSeries.GetNodeAt(pnt)

        If tlvnodetest Is Nothing Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub cmb_Customize_Serie_Order_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomizeSeriesOrder.SelectedIndexChanged
        If bl_CustomSerieSelected = False Then
            Try
                Dim colorint As Integer = ColorTranslator.ToOle(cpCustomizeSerieColor.Color)
                CustomCharts_Serie_Update(colorint, cmbCustomizeSerieType.SelectedItem.ToString, cmbCustomizeSerieAxisType.SelectedItem.ToString, cmbCustomizeSerieAxis.SelectedItem.ToString, txtCustomChartsAxisLabel.Text, nudCustomizeChartPrecision.Value, cmbCustomChartsAbsPerc.SelectedItem.ToString, cmbCustomizeSeriesOrder.Text)
            Catch
            End Try
        End If
    End Sub

    Private Sub cmb_Customize_Serie_Axis_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomizeSerieAxis.SelectedIndexChanged
        If bl_CustomSerieSelected = False Then
            Try
                Dim colorint As Integer = ColorTranslator.ToOle(cpCustomizeSerieColor.Color)
                CustomCharts_Serie_Update(colorint, cmbCustomizeSerieType.SelectedItem.ToString, cmbCustomizeSerieAxisType.SelectedItem.ToString, cmbCustomizeSerieAxis.SelectedItem.ToString, txtCustomChartsAxisLabel.Text,
                                          nudCustomizeChartPrecision.Value, cmbCustomChartsAbsPerc.SelectedItem.ToString, cmbCustomizeSeriesOrder.Text)
            Catch
            End Try
        End If
    End Sub

    Private Sub cmb_Customize_Serie_AxisType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomizeSerieAxisType.SelectedValueChanged
        If bl_CustomSerieSelected = False Then
            CustomCharts_Serie_UpdateAxis(3, cmbCustomizeSerieAxisType.SelectedItem.ToString)
            tlvCustomChartsSeries.Refresh()
        End If
    End Sub

    Private Sub cp_Customize_Serie_Color_SelectedColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cpCustomizeSerieColor.ColorChanged
        If bl_CustomSerieSelected = False Then
            Try
                Dim colorint As Integer = ColorTranslator.ToOle(cpCustomizeSerieColor.Color)
                CustomCharts_Serie_Update(colorint, cmbCustomizeSerieType.SelectedItem.ToString, cmbCustomizeSerieAxisType.SelectedItem.ToString, cmbCustomizeSerieAxis.SelectedItem.ToString, txtCustomChartsAxisLabel.Text, nudCustomizeChartPrecision.Value, cmbCustomChartsAbsPerc.SelectedItem.ToString, cmbCustomizeSeriesOrder.Text)
            Catch
            End Try
        End If
    End Sub

    Private Sub cmb_Customize_Serie_Type_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomizeSerieType.SelectedIndexChanged
        If bl_CustomSerieSelected = False Then
            Try
                Dim colorint As Integer = ColorTranslator.ToOle(cpCustomizeSerieColor.Color)
                CustomCharts_Serie_Update(colorint, cmbCustomizeSerieType.SelectedItem.ToString, cmbCustomizeSerieAxisType.SelectedItem.ToString, cmbCustomizeSerieAxis.SelectedItem.ToString, txtCustomChartsAxisLabel.Text, nudCustomizeChartPrecision.Value, cmbCustomChartsAbsPerc.SelectedItem.ToString, cmbCustomizeSeriesOrder.Text)
            Catch
            End Try
        End If
        If cmbCustomizeSerieType.SelectedItem.ToString.ToLower = "line" Then
            spEdLineThickness.Enabled = True
            cmbShowDatapoints.Enabled = True
        ElseIf cmbCustomizeSerieType.SelectedItem.ToString.ToLower = "bar" Then
            spEdLineThickness.Enabled = False
            cmbShowDatapoints.Enabled = False
        End If
    End Sub

    Private Sub cmb_CustomCharts_AbsPerc_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomChartsAbsPerc.SelectedValueChanged
        If bl_CustomSerieSelected = False Then
            Try
                CustomCharts_Serie_UpdateAxis(7, cmbCustomChartsAbsPerc.SelectedItem.ToString)
                tlvCustomChartsSeries.Refresh()
            Catch
            End Try
        End If
    End Sub

    Private Sub cmbElementAxis_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbElementAxis.SelectedIndexChanged
        If bl_CustomSerieSelected = False Then
            Try
                If cmbElementAxis.SelectedIndex > -1 Then
                    For Each nd As TreeListViewNode In tlvCustomChartsSeries.Nodes
                        If nd.SubItems(10).Text.Trim.ToUpper = cmbElementAxis.Text.ToUpper Then
                            MsgBox("Both elements cannot be on the same axis", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If
                    Next
                    tlvCustomChartsSeries.SelectedNode.SubItems(10).Text = cmbElementAxis.Text.Trim
                    tlvCustomChartsSeries.Refresh()
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub txt_CustomCharts_AxisLabel_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomChartsAxisLabel.TextChanged
        If bl_CustomSerieSelected = False Then
            Try
                CustomCharts_Serie_UpdateAxis(8, txtCustomChartsAxisLabel.Text)
                tlvCustomChartsSeries.Refresh()
            Catch
            End Try
        End If
    End Sub

    Private Sub nud_Customize_Chart_Precision_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nudCustomizeChartPrecision.ValueChanged
        If bl_CustomSerieSelected = False Then
            Try
                CustomCharts_Serie_UpdateAxis(6, nudCustomizeChartPrecision.Value)
                tlvCustomChartsSeries.Refresh()
            Catch
            End Try
        End If
    End Sub

    Private Sub tv_CustomCharts_Custom_DragComplete(ByVal sender As System.Object, ByVal e As IOS.Configuration.TreeControl.DragCompleteEventArgs) Handles tvCustomChartsCustom.DragComplete
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        tvCustomChartsCustom.SuspendLayout()
        Dim treeNodePath As String = Nothing
        Dim treeNode As TreeNode = Nothing
        Try
            If (e.TargetNode.ToolTipText = "Chart") Then
                treeNodePath = e.SourceNodeFullPath
                Dim targateCategoryTab As String = ""
                Dim targateCategoryTabIndex As String = ""
                Dim sourceCategoryTab As String = ""
                Dim sourceCategoryTabIndex As String = ""
                If (cmbChartSetName.SelectedItem.ToString = Environment.UserName.ToString.Trim) Then
                    targateCategoryTab = "Custom"
                    targateCategoryTabIndex = "99"
                    sourceCategoryTab = "Custom"
                    sourceCategoryTabIndex = "99"
                Else
                    targateCategoryTab = e.TargetNodeParent.Text
                    targateCategoryTabIndex = e.TargetNodeParent.Tag
                    sourceCategoryTab = e.SourceNodeParent.Text
                    sourceCategoryTabIndex = e.SourceNodeParent.Tag
                End If
                clsSQLCommands.ExecuteSwapCustomChartIndex(connStrIOSServer, e.SourceNode.Tag, sourceCategoryTab, sourceCategoryTabIndex, e.TargetNode.Tag, targateCategoryTab, targateCategoryTabIndex, cmbTechnology.SelectedItem.ToString, cmbChartSetName.SelectedItem.ToString)
            ElseIf (e.TargetNode.ToolTipText = "Category") Then
                treeNodePath = e.SourceNodeFullPath
                Dim targateCategoryTab As String = e.TargetNode.Text
                Dim targateCategoryTabIndex As String = e.TargetNode.Tag
                Dim sourceCategoryTab As String = e.SourceNode.Text
                Dim sourceCategoryTabIndex As String = e.SourceNode.Tag
                clsSQLCommands.ExecuteSwapCustomChartIndex(connStrIOSServer, e.SourceNode.Text, e.SourceNode.Tag, e.TargetNode.Text, e.TargetNode.Tag, cmbTechnology.SelectedItem.ToString, cmbChartSetName.SelectedItem.ToString)
                tv_CustomCharts_Refresh()
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        GetNodeFromPath(tvCustomChartsCustom.Nodes, treeNodePath)
        If (treeNode IsNot Nothing) Then
            treeNode.EnsureVisible()
        End If

        tvCustomChartsCustom.Refresh()
        tvCustomChartsCustom.ResumeLayout()
        ReloadKPITree(VendorTech)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btn_CustomChart_Serie_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomChartSerieUp.Click
        If Not tlvCustomChartsSeries.SelectedNode Is Nothing Then
            tlvCustomChartsSeries.SelectedNode.Move(TreeNodeMoveDirection.Up)
        End If
    End Sub

    Private Sub btn_CustomChart_Serie_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomChartSerieDown.Click
        If Not tlvCustomChartsSeries.SelectedNode Is Nothing Then
            tlvCustomChartsSeries.SelectedNode.Move(TreeNodeMoveDirection.Down)
        End If
    End Sub

    Private Sub tv_CustomCharts_Custom_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvCustomChartsCustom.MouseDown
        If (e.Button = MouseButtons.Right) Then
            Dim tree As TreeView = TryCast(sender, TreeView)
            If (tree IsNot Nothing) Then
                Dim item As TreeViewHitTestInfo = tree.HitTest(e.Location)
                If item.Node IsNot Nothing Then
                    tree.SelectedNode = item.Node
                    If (item.Node.ToolTipText = "Chart") Then
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.Chart
                    ElseIf (item.Node.ToolTipText = "Category") Then
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.Category
                    ElseIf (item.Node.ToolTipText = "ChartSetName") Then
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.ChartSetName
                    Else
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.NotSelected
                    End If
                End If
            End If
        Else
            Dim tree As TreeView = TryCast(sender, TreeView)
            If (tree IsNot Nothing) Then
                Dim item As TreeViewHitTestInfo = tree.HitTest(e.Location)
                If item.Node IsNot Nothing Then
                    If (item.Node.ToolTipText = "Chart") Then
                        tp_Customize_Series.Text = "Chart : " & item.Node.Text
                        Me.TreeSelectionType = IOS.Library.TreeSelectionType.Chart
                    ElseIf (item.Node.ToolTipText = "Category") Then
                        tp_Customize_Series.Text = "Chart Empty"
                    Else
                        tp_Customize_Series.Text = "Chart Empty"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btn_AddChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddChart.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            cm_CustomChart_Add_Click(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbObjectType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbObjectType.SelectedValueChanged
        Try
            dtCustomizeSerieKPI = Nothing
            'BindChartSetName()

            If cmbTechnology.SelectedIndex > 0 Then
                btnAddChartSetName.Enabled = True
                btnDelChartSetName.Enabled = True
            Else
                btnAddChartSetName.Enabled = False
                btnDelChartSetName.Enabled = False
            End If

            BindGroupByAttributeCombo()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UpdateKPITable()
    End Sub

    Private Sub BindGroupByAttributeCombo()
        Try
            Dim parray()() As String = {
                New String() {"@IOSTech", Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39)},
                New String() {"@ObjectType", Chr(39) & cmbObjectType.SelectedItem.ToString & Chr(39)}
            }

            Dim connstring As String = GetSQL(8722, parray)(0)
            Dim sql As String = GetSQL(8722, parray)(1)

            Dim dt = DataAccessorODBC.GetDataTable(connstring, sql)
            Dim colsToSkip() As String = {"ConfigDate", "Latest_PM_Date"}
            For Each row As DataRow In dt.Select("ColumnName IN ('" & String.Join("','", colsToSkip) & "')")
                dt.Rows.Remove(row)
            Next
            BindDevExComboBoxWithValueMember(cmbGroupByAttribute, dt, "ColumnName", "ColumnName", "Select")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            If cmbTechnology.SelectedIndex > 0 Then
                VendorTech = cmbTechnology.SelectedItem.ToString
                If VendorTech.Contains("TopX") Then
                    VendorTech = VendorTech.Replace("TopX_", "")
                    dtpStartTime.EditValue = DateSerial(Now().Year, Now().Month, Now.Day - 1)
                    dtpEndTime.EditValue = DateSerial(Now().Year, Now().Month, Now.Day - 1)
                Else
                    dtpStartTime.EditValue = Today.AddDays(-60)
                    dtpEndTime.EditValue = New DateTime(Now().Year, Now().Month, Now.Day, 0, 0, 0)
                End If
                BindChartSetName()
                BindObjectType()
            Else
                'If cmbObjectType.SelectedIndex > 0 Then
                '    btnAddChartSetName.Enabled = True
                '    btnDelChartSetName.Enabled = True
                'Else
                btnAddChartSetName.Enabled = False
                btnDelChartSetName.Enabled = False
                'End If
                ClearComboBox(cmbObjectType, "Select")
                ClearComboBox(cmbChartSetName, "Select")
            End If
            If VendorTech IsNot Nothing Then
                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(VendorTech.Replace("TopX_", "").ToUpper)).LastOrDefault()
            End If
        Catch
        End Try
    End Sub

    Private Sub cm_ChartPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            tvCustomChartsCustom.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (Me.TreeSelectionType = TreeSelectionType.Chart) Then
                cm_ChartCopy_Click(Nothing, Nothing)
                CopyCustomChart(sender)
            ElseIf (Me.TreeSelectionType = TreeSelectionType.Category) Then
                Dim chartCount = tvCustomChartsCustom.SelectedNode.Nodes.Count
                For iCntr = 0 To chartCount - 1
                    CopyCategoryChart(iCntr)
                    CopyCategoryChartToChartSet(sender)
                Next
            ElseIf (Me.TreeSelectionType = TreeSelectionType.ChartSetName) Then
                Dim categoryCount As Integer = tvCustomChartsCustom.SelectedNode.Nodes.Count
                For iCntr = 0 To categoryCount - 1
                    Dim chartCount As Integer = tvCustomChartsCustom.SelectedNode.Nodes(iCntr).Nodes.Count
                    For jCntr = 0 To chartCount - 1
                        CopyCategoryChart(jCntr, iCntr)
                        CopyCategoryChartToChartSet(sender)
                    Next
                Next
            End If

            tv_CustomCharts_Refresh()
            tvCustomChartsCustom.ResumeLayout()
            ReloadKPITree(VendorTech)

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tvCustomChartsCustom.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub CopyCategoryChart(chartNodeIndex As Integer, Optional categoryNodeIndex As Integer = Nothing)
        Clipboard.Clear()
        Dim categoryTab As String = "Custom"
        Dim categoryTabIndex As String = "99"
        Dim chartName As String = ""
        Dim chartIndex As String = ""
        If (tvCustomChartsCustom.SelectedNode.ToolTipText = "Category") Then
            categoryTab = tvCustomChartsCustom.SelectedNode.Text
            categoryTabIndex = tvCustomChartsCustom.SelectedNode.Tag
            chartName = tvCustomChartsCustom.SelectedNode.Nodes(chartNodeIndex).Tag
            chartIndex = tvCustomChartsCustom.SelectedNode.Nodes(chartNodeIndex).Name
        ElseIf (tvCustomChartsCustom.SelectedNode.ToolTipText = "ChartSetName") Then
            categoryTab = tvCustomChartsCustom.SelectedNode.Nodes(categoryNodeIndex).Text
            categoryTabIndex = tvCustomChartsCustom.SelectedNode.Nodes(categoryNodeIndex).Tag
            chartName = tvCustomChartsCustom.SelectedNode.Nodes(categoryNodeIndex).Nodes(chartNodeIndex).Tag
            chartIndex = tvCustomChartsCustom.SelectedNode.Nodes(categoryNodeIndex).Nodes(chartNodeIndex).Name
        End If
        Dim copystring As String = cmbTechnology.SelectedItem.ToString & "," & cmbObjectType.SelectedItem.ToString & "," & cmbChartSetName.SelectedItem.ToString & "," & categoryTab & "," & categoryTabIndex & "," & chartName & "," & chartIndex & "," & cmbObjectType.Text & "," & TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value.ToString
        copystring = copystring.Replace(",", ControlChars.NewLine)
        If Not copystring Is Nothing Or copystring <> "" Then
            Clipboard.SetText(copystring)
        End If
        copystring = Nothing
    End Sub

    Private Sub CopyCustomChart(sender)
        Dim tsmiCh As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim chartSetName As String = tsmiCh.Text
        If (Clipboard.GetText IsNot Nothing) Then
            Dim copiedText As String = Clipboard.GetText()
            Dim copiedTxt() As String = copiedText.Split(ControlChars.NewLine)

            Dim parray()() As String = {
                New String() {"@techtab", Chr(39) & copiedTxt(0).Trim & Chr(39)},
                New String() {"@userid", Chr(39) & copiedTxt(2).Trim & Chr(39)},
                New String() {"@categoryTab", Chr(39) & copiedTxt(3).Trim & Chr(39)},
                New String() {"@categoryIndexTab", CInt(copiedTxt(4).Trim)},
                New String() {"@ObjectTab", Chr(39) & copiedTxt(1).Trim & Chr(39)},
                New String() {"@ObjectIndex", CInt(copiedTxt(8).Trim)},
                New String() {"@chartSetName", Chr(39) & chartSetName.Trim & Chr(39)},
                New String() {"@chartName", Chr(39) & copiedTxt(5).Trim & Chr(39)},
                New String() {"@chartIndex", CInt(copiedTxt(6).Trim)}
            }

            Dim connstring As String = GetSQL(8711, parray)(0)
            Dim sql As String = GetSQL(8711, parray)(1)
            Try
                DataAccessorODBC.ExecuteNonQuery(connstring, sql)
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try
        End If
    End Sub

    Private Sub CopyCategoryChartToChartSet(sender)
        Dim tsmiCh As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim chartSetName As String = tsmiCh.Text
        If (Clipboard.GetText IsNot Nothing) Then
            Dim copiedText As String = Clipboard.GetText()
            Dim copiedTxt() As String = copiedText.Split(ControlChars.NewLine)

            Dim parray()() As String = {
                New String() {"@TechTab", Chr(39) & copiedTxt(0).Trim & Chr(39)},
                New String() {"@ChartSetCopyFrom", Chr(39) & copiedTxt(2).Trim & Chr(39)},
                New String() {"@CategoryTab", Chr(39) & copiedTxt(3).Trim & Chr(39)},
                New String() {"@CategoryIndex", CInt(copiedTxt(4).Trim)},
                New String() {"@ObjectTab", Chr(39) & copiedTxt(1).Trim & Chr(39)},
                New String() {"@ObjectIndex", CInt(copiedTxt(8).Trim)},
                New String() {"@ChartSetCopyTo", Chr(39) & chartSetName.Trim & Chr(39)},
                New String() {"@ChartName", Chr(39) & copiedTxt(5).Trim & Chr(39)},
                New String() {"@ChartIndex", CInt(copiedTxt(6).Trim)}
            }

            Dim connstring As String = GetSQL(8721, parray)(0)
            Dim sql As String = GetSQL(8721, parray)(1)
            Try
                DataAccessorODBC.ExecuteNonQuery(connstring, sql)
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try
        End If
    End Sub

    Private Sub cm_ChartCopy_Click(sender As Object, e As EventArgs) Handles cm_ChartCopy.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Clipboard.Clear()
        Try
            Dim categoryTab As String = "Custom"
            Dim categoryTabIndex As String = "99"
            Dim chartName As String = ""
            Dim chartIndex As String = ""
            If (tvCustomChartsCustom.SelectedNode.Parent.ToolTipText = "Category") Then
                categoryTab = tvCustomChartsCustom.SelectedNode.Parent.Text
                categoryTabIndex = tvCustomChartsCustom.SelectedNode.Parent.Tag
            ElseIf (tvCustomChartsCustom.SelectedNode.Parent.ToolTipText = "Chart" Or tvCustomChartsCustom.SelectedNode.ToolTipText = "Chart") Then
                If (cmbChartSetName.SelectedItem.ToString = chartSetName) Then
                    categoryTab = tvCustomChartsCustom.SelectedNode.Text
                    categoryTabIndex = tvCustomChartsCustom.SelectedNode.Tag
                End If
            End If
            chartName = tvCustomChartsCustom.SelectedNode.Tag
            chartIndex = tvCustomChartsCustom.SelectedNode.Name
            Dim copystring As String = cmbTechnology.SelectedItem.ToString & "," & cmbObjectType.SelectedItem.ToString & "," & cmbChartSetName.SelectedItem.ToString & "," & categoryTab & "," & categoryTabIndex & "," & chartName & "," & chartIndex & "," & cmbObjectType.Text & "," & TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value.ToString
            copystring = copystring.Replace(",", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub txtSearchKPI_TextChanged(sender As Object, e As EventArgs) Handles txtSearchKPI.TextChanged
        Try
            UpdateKPITable(False)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally

        End Try
    End Sub

    Private Sub grvCustomizeSerieKPI_DragOver(sender As Object, e As DragEventArgs) Handles tlvCustomChartsSeries.DragOver, Chart1.DragOver, gcCustomizeSerieKPI.DragOver
        Dim pnt As Point = tlvCustomChartsSeries.PointToClient(New Point(e.X, e.Y))
        Dim tlvnodetest As TreeListViewNode = tlvCustomChartsSeries.GetNodeAt(pnt)
        If tlvnodetest Is Nothing Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub grvCustomizeSerieKPI_MouseMove(sender As Object, e As MouseEventArgs) Handles gcCustomizeSerieKPI.MouseMove
        If (e.Button AndAlso MouseButtons.Left = MouseButtons.Left) Then
            Dim dtData As DataTable = DirectCast(gcCustomizeSerieKPI.DataSource, DataTable).Clone()
            If dtData IsNot Nothing Then
                For i As Integer = 0 To gvCustomizeSerieKPI.GetSelectedRows().Count - 1
                    dtData.ImportRow(DirectCast(gvCustomizeSerieKPI.GetRow(gvCustomizeSerieKPI.GetSelectedRows()(i)), DataRowView).Row)
                Next
                dtData.AcceptChanges()
                Dim dropEffect As DragDropEffects = gcCustomizeSerieKPI.DoDragDrop(dtData, DragDropEffects.All)
            End If
        End If
    End Sub

    Private Sub cmbChartType_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cmbChartType.SelectedIndexChanged
        Try
            If cmbChartType.SelectedIndex > -1 Then
                If cmbChartType.Text.Trim.ToLower = "scatter" Then
                    cmbElementAxis.Enabled = True
                Else
                    cmbElementAxis.Enabled = False
                End If
                cmbGroupByAttribute.SelectedIndex = 0
                cmbGroupByAttribute.Enabled = False
                If cmbChartType.Text.Trim.ToLower = "groupperattribute" Then
                    cmbGroupByAttribute.Enabled = True
                End If
                CustomCharts_Update()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAddChartSetName_Click(sender As Object, e As EventArgs) Handles btnAddChartSetName.Click
        Try
            Dim objAddChartSet As New dlgAddChartSet
            objAddChartSet.ShowDialog()

            If objAddChartSet.DialogResult = DialogResult.OK Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim parray()() As String = {
                    New String() {"@TechTab", Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39)},
                    New String() {"@ObjectTab", Chr(39) & cmbObjectType.SelectedItem.ToString & Chr(39)},
                    New String() {"@ChartSetName", Chr(39) & objAddChartSet.ChartSetName & Chr(39)},
                    New String() {"@Accessibility", Chr(39) & objAddChartSet.AccessType & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName & Chr(39)}
                }
                Dim connstring As String = GetSQL(8716, parray)(0)
                Dim sql As String = GetSQL(8716, parray)(1)
                Dim dt As DataTable = DataAccessorODBC.GetDataTable(connstring, sql)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)(0).ToString.ToLower = "saved" Then
                        BindChartSetName()
                        SetComboBox(cmbChartSetName, ComboSelectBased.ValueBased, objAddChartSet.ChartSetName)
                        XtraMessageBox.Show("ChartSet saved successfully", "Add New ChartSet", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf dt.Rows(0)(0).ToString.ToLower = "alreadyexists" Then
                        XtraMessageBox.Show("ChartSet already exists", "Add New ChartSet", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ElseIf dt.Rows(0)(0).ToString.ToLower = "csnasuser" Then
                        XtraMessageBox.Show("Any username cannot be set as ChartSet", "Add New ChartSet", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
                tv_CustomCharts_Refresh()
                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(VendorTech.Replace("TopX_", "").ToUpper)).LastOrDefault()
                If objfrmTechnology IsNot Nothing Then
                    Select Case cmbTechnology.SelectedItem.ToString.ToUpper.TrimEnd(" ")
                        Case objfrmTechnology.Network.ToUpper
                            objfrmTechnology.dtChartSetName = objfrmTechnology.GetTechChartSetName()
                            BindComboWithChartSetName(objfrmTechnology.dtChartSetName, objfrmTechnology.cmbChartSetNameStats)
                            BindComboWithChartSetName(objfrmTechnology.dtChartSetName, objfrmTechnology.cmbChartSetNameEval)
                        Case "TOPX_" & objfrmTechnology.Network.ToUpper
                            objfrmTechnology.dtChartSetNameTopX = objfrmTechnology.GetTechChartSetName("TopX_")
                            BindComboWithChartSetName(objfrmTechnology.dtChartSetNameTopX, objfrmTechnology.cmbChartSetNameTopX)
                    End Select
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

    Private Sub btnDelChartSetName_Click(sender As Object, e As EventArgs) Handles btnDelChartSetName.Click
        Try
            If cmbChartSetName.SelectedIndex >= 0 Then
                If cmbChartSetName.SelectedItem.ToString.Trim = "RF" Then
                    XtraMessageBox.Show("RF ChartSetName cannot be deleted", "Delete ChartSetName", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    Dim isPowerUser As Boolean = False
                    If XtraMessageBox.Show("Are you sure to delete ChartSetName: " & cmbChartSetName.SelectedItem.ToString & "?", "Delete ChartSetName", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        Me.Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        Dim ChartSetOwner As String = clsSQLCommands.GetChartSetOwner(connStrIOSServer, cmbTechnology.SelectedItem.ToString, cmbChartSetName.SelectedItem.ToString)
                        If (ChartSetOwner.ToUpper = Environment.UserName.ToUpper) Or (configMgr.User.IsPowerUser = True) Then
                            isPowerUser = True
                        Else
                            XtraMessageBox.Show("Only a power user can delete ChartSetName", "Delete ChartSetName", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            isPowerUser = False
                            Exit Sub
                        End If

                        If (isPowerUser = True) Then
                            Dim parray()() As String = {
                                New String() {"@TechTab", Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39)},
                                New String() {"@ObjectTab", Chr(39) & cmbObjectType.SelectedItem.ToString & Chr(39)},
                                New String() {"@ChartSetName", Chr(39) & cmbChartSetName.SelectedItem.ToString & Chr(39)}
                            }
                            Dim connstring As String = GetSQL(8713, parray)(0)
                            Dim sql As String = GetSQL(8713, parray)(1)
                            DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                            BindChartSetName()
                            cmbChartSetName_SelectedItemChanged(Nothing, Nothing)
                        End If
                    End If
                End If
                ReloadKPITree(VendorTech)
                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(VendorTech.Replace("TopX_", "").ToUpper)).LastOrDefault()
                If objfrmTechnology IsNot Nothing Then
                    Select Case cmbTechnology.SelectedItem.ToString.ToUpper.TrimEnd(" ")
                        Case objfrmTechnology.Network.ToUpper
                            objfrmTechnology.dtChartSetName = objfrmTechnology.GetTechChartSetName()
                            BindComboWithChartSetName(objfrmTechnology.dtChartSetName, objfrmTechnology.cmbChartSetNameStats)
                        Case "TOPX_" & objfrmTechnology.Network.ToUpper
                            objfrmTechnology.dtChartSetNameTopX = objfrmTechnology.GetTechChartSetName("TopX_")
                            BindComboWithChartSetName(objfrmTechnology.dtChartSetNameTopX, objfrmTechnology.cmbChartSetNameTopX)
                    End Select
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

    Private Sub spinEdit_LineThickness_ValueChanged(sender As Object, e As EventArgs) Handles spEdLineThickness.ValueChanged
        'If isChartSerieSelected = True Then
        Try
            If (tlvCustomChartsSeries.SelectedNode IsNot Nothing) Then
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.SelectedNode
                If nd.SubItems(5).Text.Trim.ToUpper = cmbCustomizeSerieAxis.Text.ToUpper Then
                    nd.SubItems(11).Text = spEdLineThickness.Value
                Else
                    SetMsgStatus("Series axis is not set")
                End If
                tlvCustomChartsSeries.Refresh()
            End If
        Catch
        End Try
        'End If
    End Sub

    Private Sub cmbShowDatapoints_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShowDatapoints.SelectedIndexChanged
        Try
            If (tlvCustomChartsSeries.SelectedNode IsNot Nothing) Then
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.SelectedNode
                If nd.SubItems(5).Text.Trim.ToUpper = cmbCustomizeSerieAxis.Text.ToUpper Then
                    nd.SubItems(12).Text = cmbShowDatapoints.SelectedItem.ToString
                Else
                    SetMsgStatus("Series axis is not set")
                End If
                tlvCustomChartsSeries.Refresh()
            End If
        Catch
        End Try
    End Sub

    Private Sub chkSeriesVisible_CheckedChanged(sender As Object, e As EventArgs) Handles chkSeriesVisible.CheckedChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If tlvCustomChartsSeries.Nodes.Count > 1 Then
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.SelectedNode
                If chkSeriesVisible.Checked = True Then
                    nd.SubItems(13).Text = "True"
                Else
                    nd.SubItems(13).Text = "False"
                End If
                tlvCustomChartsSeries.Refresh()
            Else
                chkSeriesVisible.Checked = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbCustomChartAutoScale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomChartAutoScale.SelectedIndexChanged
        If bl_CustomSerieSelected = False Then
            Try
                CustomCharts_Serie_UpdateAxis(14, cmbCustomChartAutoScale.SelectedItem.ToString)
                tlvCustomChartsSeries.Refresh()
            Catch
            End Try
        End If
    End Sub

    Private Sub cmbGroupByAttribute_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroupByAttribute.SelectedIndexChanged
        Try
            If (tlvCustomChartsSeries.SelectedNode IsNot Nothing) Then
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.SelectedNode
                If nd.SubItems(5).Text.Trim.ToUpper = cmbCustomizeSerieAxis.Text.ToUpper Then
                    nd.SubItems(15).Text = cmbGroupByAttribute.SelectedItem.ToString
                    'Else
                    'SetMsgStatus("Series axis is not set")
                End If
                tlvCustomChartsSeries.Refresh()
            End If
        Catch
        End Try
    End Sub

    Private Sub chkEnablePeriodCalc_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnablePeriodCalc.CheckedChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If tlvCustomChartsSeries.Nodes.Count > 0 Then
                Dim nd As TreeListViewNode = tlvCustomChartsSeries.SelectedNode
                If chkEnablePeriodCalc.Checked = True Then
                    nd.SubItems(16).Text = "True"
                Else
                    nd.SubItems(16).Text = "False"
                End If
                tlvCustomChartsSeries.Refresh()
            Else
                chkEnablePeriodCalc.Checked = False
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Context Menu"

    Private Sub cm_InserCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_InserCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim frmChartCategoryDialog As New dlgChartCategory()
            frmChartCategoryDialog.ShowDialog()
            If (frmChartCategoryDialog.CategoryTab IsNot Nothing AndAlso frmChartCategoryDialog.CategoryTabIndex) Then
                clsSQLCommands.InsertNewChartCategory(connStrIOSServer, cmbTechnology.SelectedItem.ToString, frmChartCategoryDialog.CategoryTabIndex, frmChartCategoryDialog.CategoryTab, cmbChartSetName.SelectedItem.ToString, cmbObjectType.Text, TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value)
                tv_CustomCharts_Refresh()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_ExpandAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_ExpandAll.Click
        tvCustomChartsCustom.ExpandAll()
    End Sub

    Private Sub cm_CollapseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_CollapseAll.Click
        If (tvCustomChartsCustom.Nodes.Count > 0 AndAlso tvCustomChartsCustom.Nodes(0).Nodes.Count > 0) Then
            tvCustomChartsCustom.Nodes(0).Nodes(0).Collapse(False)
            tvCustomChartsCustom.Nodes(0).Nodes(0).Expand()
        End If
    End Sub

    Private Sub cm_CustomChart_Rename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_CustomChart_Rename.Click
        If (tvCustomChartsCustom.SelectedNode IsNot Nothing) Then
            tvCustomChartsCustom.SelectedNode.BeginEdit()
            ReloadKPITree(VendorTech)
        End If
    End Sub

    Private Sub cm_CustomChart_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_CustomChart_Add.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (ValidateControls()) Then
                Dim treen As TreeNode = tvCustomChartsCustom.SelectedNode
                Dim treeNodePath As String = String.Empty
                If (treen IsNot Nothing) Then
                    treeNodePath = tvCustomChartsCustom.SelectedNode.FullPath
                End If
                Dim techName As String = cmbTechnology.SelectedItem.ToString.TrimEnd(" ")
                Dim categoryTab As String = Nothing
                Dim categoryIndexTab As String = Nothing
                Dim _chartSetName As String = cmbChartSetName.SelectedItem.ToString

                If (cmbChartSetName.SelectedItem.ToString.ToUpper = Environment.UserName.ToString.ToUpper) Then
                    categoryTab = "Custom"
                    categoryIndexTab = "99"
                Else
                    If lblCategoryTab.Text = "Custom" Or lblCategoryTab.Text = "" Then
                        MsgBox("Select a Category first!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    Else
                        categoryTab = lblCategoryTab.Text
                        categoryIndexTab = lblCategoryTabIndex.Text
                    End If
                End If

                Dim parray()() As String = {
                    New String() {"@techtab", Chr(39) & techName & Chr(39)},
                    New String() {"@userid", Chr(39) & _chartSetName & Chr(39)},
                    New String() {"@categoryTab", Chr(39) & categoryTab & Chr(39)},
                    New String() {"@ObjectTab", Chr(39) & cmbObjectType.Text & Chr(39)},
                    New String() {"@ObjectIndex", Chr(39) & TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value.ToString & Chr(39)},
                    New String() {"@categoryIndexTab", Chr(39) & categoryIndexTab & Chr(39)}
                }
                Dim connstring As String = GetSQL(8703, parray)(0)
                Dim sql As String = GetSQL(8703, parray)(1)
                Dim dtQODBC As System.Data.DataTable = Nothing

                Try
                    dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                Finally
                    If Not dtQODBC Is Nothing Then
                        dtQODBC.Dispose()
                        dtQODBC = Nothing
                    End If
                End Try

                tv_CustomCharts_Refresh()
                GetNodeFromPath(tvCustomChartsCustom.Nodes, treeNodePath)

                'clear chart area for a newly added chart name
                Chart1.SeriesCollection.Clear()
                Chart1.Annotations.Clear()
                Chart1.XAxis.Label.Text = ""
                Chart1.ExtraChartAreas.Clear()
                Chart1.RefreshChart()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_CustomChart_Delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cm_CustomChart_Delete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim treeNodePath As String = GetPathFromTree(tvCustomChartsCustom)
            tvCustomChartsCustom.SelectedNode.Tag.Trim()
            If (tvCustomChartsCustom.SelectedNode.Parent.ToolTipText = "Category") Then
                Dim categoryTab As String = tvCustomChartsCustom.SelectedNode.Parent.Text
                Dim categoryTabIndex As String = tvCustomChartsCustom.SelectedNode.Parent.Tag
                CustomCharts_Chart_Delete(tvCustomChartsCustom.SelectedNode.Tag.Trim, cmbTechnology.Text.Trim, cmbChartSetName.SelectedItem.ToString, categoryTab)
            ElseIf (tvCustomChartsCustom.SelectedNode.Parent.ToolTipText = "Chart" Or tvCustomChartsCustom.SelectedNode.ToolTipText = "Chart") Then
                Dim categoryTab As String = "Custom"
                Dim categoryTabIndex As String = "99"

                If (cmbChartSetName.SelectedItem.ToString = chartSetName) Then
                    categoryTab = tvCustomChartsCustom.SelectedNode.Text
                    categoryTabIndex = tvCustomChartsCustom.SelectedNode.Tag
                End If
                CustomCharts_Chart_Delete(tvCustomChartsCustom.SelectedNode.Tag.Trim, cmbTechnology.Text.Trim, cmbChartSetName.SelectedItem.ToString, categoryTab)

            End If
            'reload kpitree
            ReloadKPITree(VendorTech)
            Dim ch As Chart = CType(tlp_CustomChart.Controls(0), Chart)
            ch.SeriesCollection.Clear()
            ch.Annotations.Clear()
            ch.XAxis.Label.Text = ""
            ch.ExtraChartAreas.Clear()
            ch.RefreshChart()
            tlvCustomChartsSeries.Nodes.Clear()

            tv_CustomCharts_Refresh()
            GetNodeFromPath(tvCustomChartsCustom.Nodes, treeNodePath)
            tlvCustomChartsSeries.UpdateLayout()
            ReloadKPITree(VendorTech)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_tlv_CustomCharts_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cm_tlv_CustomCharts.Opening
        Try
            If (Me.TreeSelectionType = IOS.Library.TreeSelectionType.Chart) Then
                cm_CustomChart_Delete.Enabled = True
                cm_CustomChart_Add.Enabled = True
                cm_ChartCopy.Enabled = True
                cm_CustomChart_Rename.Enabled = True
                cm_DeleteCategory.Enabled = False
                cm_RenameCategory.Enabled = False
                cm_ChartSetChangeAccessType.Enabled = False
                cm_ChartPaste.Enabled = True
                cm_ChartPaste.Text = "Chart - Copy To"
            ElseIf (Me.TreeSelectionType = IOS.Library.TreeSelectionType.Category) Then
                cm_CustomChart_Delete.Enabled = False
                cm_CustomChart_Add.Enabled = True
                cm_CustomChart_Rename.Enabled = False
                cm_ChartCopy.Enabled = False
                cm_DeleteCategory.Enabled = True
                cm_RenameCategory.Enabled = True
                cm_ChartSetChangeAccessType.Enabled = False
                cm_ChartPaste.Enabled = True
                cm_ChartPaste.Text = "Category - Copy To"
            ElseIf (Me.TreeSelectionType = IOS.Library.TreeSelectionType.ChartSetName) Then
                ToolStripSeparator3.Visible = True
                Dim chartSetAccessibility As String = clsSQLCommands.GetChartSetAccessibility(connStrIOSServer, cmbTechnology.SelectedItem.ToString, cmbChartSetName.SelectedItem.ToString)
                cm_ChartSetChangeAccessType.Text = "ChartSet - Change Access Type To: " & IIf(chartSetAccessibility = "Public", "Private", "Public")
                cm_ChartSetChangeAccessType.Visible = True
                cm_ChartSetChangeAccessType.Enabled = True
                cm_CustomChart_Add.Enabled = False
                cm_CustomChart_Delete.Enabled = False
                cm_ChartCopy.Enabled = False
                cm_CustomChart_Rename.Enabled = False
                cm_ChartPaste.Enabled = True
                cm_ChartPaste.Text = "ChartSet - Copy To"
                cm_ChartPaste.DropDownItems.Clear()
                For Each item As Object In cmbChartSetName.Properties.Items
                    If (item.ToString.ToLower <> cmbChartSetName.SelectedItem.ToString.ToLower) AndAlso (item.ToString.ToLower <> Environment.UserName.ToString.ToLower) Then
                        Dim tsmi As ToolStripMenuItem = New ToolStripMenuItem(item.ToString)
                        AddHandler tsmi.Click, AddressOf cm_ChartPaste_Click
                        cm_ChartPaste.DropDownItems.Add(tsmi)
                    End If
                Next
                Dim s As String = Clipboard.GetText()
                Dim rows() As String = s.Split(ControlChars.NewLine)
                cm_ChartPaste.Enabled = (rows.Count > 0)
                If cmbChartSetName.SelectedItem.ToString.ToLower = Environment.UserName.ToString.ToLower Then
                    cm_InserCategory.Enabled = False
                    cm_DeleteCategory.Enabled = False
                    cm_RenameCategory.Enabled = False
                Else
                    cm_InserCategory.Enabled = True
                    cm_DeleteCategory.Enabled = True
                    cm_RenameCategory.Enabled = True
                End If
            Else
                cm_CustomChart_Delete.Enabled = False
                cm_CustomChart_Add.Enabled = False
                cm_CustomChart_Rename.Enabled = False
                cm_ChartCopy.Enabled = False
                cm_DeleteCategory.Enabled = False
                cm_RenameCategory.Enabled = False
                cm_ChartSetChangeAccessType.Enabled = False
                cm_ChartPaste.Enabled = False
            End If

            If (Me.TreeSelectionType = IOS.Library.TreeSelectionType.Chart) Or (Me.TreeSelectionType = IOS.Library.TreeSelectionType.Category) Then
                Dim b As Boolean = (cmbChartSetName.Text = chartSetName) Or (cmbChartSetName.Text <> Environment.UserName.ToString)
                cm_CollapseAll.Visible = b
                cm_ExpandAll.Visible = b
                cm_InserCategory.Visible = b
                cm_DeleteCategory.Visible = b
                cm_RenameCategory.Visible = b
                'cm_ChartSetChangeAccessType.Visible = b
                'cm_ChartSetChangeAccessType.Enabled = b
                ToolStripSeparator1.Visible = b
                ToolStripSeparator2.Visible = b
                ToolStripSeparator3.Visible = b
                Dim i As Integer = 0
                cm_ChartPaste.DropDownItems.Clear()
                If (Me.TreeSelectionType = IOS.Library.TreeSelectionType.Chart) Then
                    Dim tsmi1 As ToolStripMenuItem = New ToolStripMenuItem(Environment.UserName.ToString)
                    cm_ChartPaste.DropDownItems.Add(tsmi1)
                    AddHandler tsmi1.Click, AddressOf cm_ChartPaste_Click
                End If
                For Each item As Object In cmbChartSetName.Properties.Items
                    If (item.ToString.ToLower <> cmbChartSetName.SelectedItem.ToString.ToLower) AndAlso (item.ToString.ToLower <> Environment.UserName.ToString.ToLower) Then
                        Dim tsmi As ToolStripMenuItem = New ToolStripMenuItem(item.ToString)
                        'If (i = 0) Or (item.ToString = Environment.UserName.ToString) Then
                        '    i = i + 1
                        '    Continue For
                        'End If
                        AddHandler tsmi.Click, AddressOf cm_ChartPaste_Click
                        cm_ChartPaste.DropDownItems.Add(tsmi)
                        'i = i + 1
                    End If
                Next
                Dim s As String = Clipboard.GetText()                  'Get clipboard data as a string
                Dim rows() As String = s.Split(ControlChars.NewLine)
                cm_ChartPaste.Enabled = (rows.Count > 0)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cm_DeleteCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_DeleteCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (cmbChartSetName.SelectedItem.ToString = chartSetName) Or (cmbChartSetName.SelectedItem.ToString <> Environment.UserName.ToString) Then
                Dim treeNodePath As String = GetPathFromTree(tvCustomChartsCustom)
                Dim categoryIndexTab As String = tvCustomChartsCustom.SelectedNode.Tag.Trim
                Dim categoryTab As String = tvCustomChartsCustom.SelectedNode.Text.Trim
                Dim techTab As String = cmbTechnology.Text.Trim
                Dim objecttab As String = cmbObjectType.Text.Trim

                If (categoryIndexTab IsNot Nothing) Then
                    Dim parray()() As String = {
                        New String() {"@techTab", Chr(39) & techTab & Chr(39)},
                        New String() {"@chartSetName", Chr(39) & cmbChartSetName.SelectedItem.ToString & Chr(39)},
                        New String() {"@categoryTabName", Chr(39) & categoryTab & Chr(39)},
                        New String() {"@categoryIndexTab", Chr(39) & categoryIndexTab & Chr(39)},
                        New String() {"@objecttab", Chr(39) & objecttab & Chr(39)}
                    }

                    Dim connstring As String = GetSQL(8709, parray)(0)
                    Dim sql As String = GetSQL(8709, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(connstring, sql)

                    tlvCustomChartsSeries.Nodes.Clear()
                    tv_CustomCharts_Refresh()
                    tlvCustomChartsSeries.UpdateLayout()
                End If
                GetNodeFromPath(tvCustomChartsCustom.Nodes, treeNodePath)
                ReloadKPITree(VendorTech)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub cm_RenameCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_RenameCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (tvCustomChartsCustom.SelectedNode IsNot Nothing) Then
                tvCustomChartsCustom.SelectedNode.BeginEdit()
                ReloadKPITree(VendorTech)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_ChartSetChangeAccessType_Click(sender As Object, e As EventArgs) Handles cm_ChartSetChangeAccessType.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim parray()() As String = {
                New String() {"@TechTab", Chr(39) & cmbTechnology.SelectedItem.ToString & Chr(39)},
                New String() {"@ChartSetName", Chr(39) & tvCustomChartsCustom.SelectedNode.Text.Trim & Chr(39)},
                New String() {"@UserName", Chr(39) & Environment.UserName.ToString & Chr(39)}
            }
            Dim connstring As String = GetSQL(8717, parray)(0)
            Dim sql As String = GetSQL(8717, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(connstring, sql)
            If dt.Rows(0)(0).ToString.ToUpper = "ACCESSMODIFIED" Then
                SetMsgStatus("ChartSet Access Modified")
            ElseIf dt.Rows(0)(0).ToString.ToUpper = "NOTMODIFIED" Then
                SetMsgStatus("Only PowerUser Can Modify Access")
            End If
            BindChartSetName()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class