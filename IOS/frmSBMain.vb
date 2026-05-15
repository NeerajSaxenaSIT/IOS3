Imports dotnetCHARTING.WinForms
Imports IOS.Configuration
Imports IOS.DataLibrary
Imports IOS.Library
Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraTab
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Concurrent

Enum KPIDataBaseName
    None
    MSSQL
    ORACLE
End Enum

Public Class frmSBMain

#Region "Variables"

    Dim isModifyKPIRequest As Boolean = False
    Dim modifyKPIID As String = String.Empty
    Private defaultEX As Integer = -1
    Dim isClickedByObjectSource As Boolean = True
    Dim isDraggedCounterItem As Boolean = False
    Dim isDraggedKPIItem As Boolean = False
    Dim IsByClickJobGroup As Boolean = False

    Private ScaleDateToBeExcluded As New List(Of DateTime)
    Private enableFormLevelDoubleBuffering As Boolean = True
    Dim isReportGroupSelectedIndexChanged As Boolean = True
    Dim clickedSandBoxSourceControl As Control = Nothing
    Dim isDashboradGroupSelectedIndexChanged As Boolean = True
    Dim cm_OT_SourceControl As Control

    Dim SanboxReportTreeSelectionType As ReportSelectionType = ReportSelectionType.NotSelected
    Dim DashboardGroupReportTreeSelectionType As DashboardSelectionType = DashboardSelectionType.NotSelected
    Dim JobGroupReportTreeSelectionType As JobSelectionType = JobSelectionType.NotSelected
    Dim IsReportGroupMouseDownRight As Boolean = False
    Dim IsDashboardGroupReportMouseDownRight As Boolean = False
    Dim timeMonitor As DateTime
    Dim IsByClickReport As Boolean = False
    Dim dtChartConfigSandbox As DataTable = New DataTable
    Dim dtReportAxisData As DataTable = New DataTable
    Dim dtChartObjectsData As DataTable = New DataTable
    Dim dtReportFilterData As DataTable = New DataTable
    Dim isByClick As Boolean = True

    Dim TreeView_SearchFound As Integer
    Dim Treeview_NodeFound As Boolean = False
    Dim dragDropType As DragDropType = DragDropType.NoDragDrop
    Dim isChartSerieSelected As Boolean = False
    Dim rightMouseOnListbox As Boolean = False
    Public dt_TechPackCounter As DataTable = Nothing
    Public dt_TechnologyPackageKPI As DataTable = Nothing
    Private dt_TechnologyPackageObjects As DataTable = Nothing
    Dim checkedCounter As New List(Of String)
    Dim checkedKPI As New List(Of String)
    Dim checkedKPINameList As New List(Of String)
    Dim viewCheckedKPINameList As New List(Of String)
    Dim checkedMeasurements As New List(Of String)
    Dim sqlSelectColList As New Dictionary(Of String, List(Of String))
    Dim viewCheckedKPIsOnly As Boolean = False

    Dim list_of_used_tables As List(Of String) = New List(Of String)
    Dim lstTechCounterCheckedItems As New List(Of Object)
    Dim lstTechKPICheckedItems As New List(Of Object)
    Dim lstTechMeasurementCheckedItems As New List(Of Object)

    Dim isFirstTimeCalculatedSeriesTypes As Boolean = True
    Dim p As Point = Point.Empty
    'Dim strDenominator As String = "()" ''"<enter denominator here>"
    Dim cm_SourceControl As Control
    Dim clr As Color = Color.Empty
    Private hourList As New List(Of String)
    Private dtPredefinedPeriodSB As New DataTable
    Dim reportName As String = Nothing
    Dim ReportIDOwner As String = ""
    Dim dragDimensions As Boolean = False
    Private GridorChart As String = Nothing
    Private dtDashboardGroup As DataTable = Nothing

    'Report Export
    Private dtReportExport As DataTable = New DataTable
    Private reportConnString As String = Nothing
    Private expReportID As Integer = Nothing
    Private OutputDelimiter As String = ";"
    Private queue As New TaskQueue()
    Private ReadOnly objExportThreadLock As New Object

#End Region

#Region "Helper Methods"

    Private Sub BindTechnologyPackage()
        Try
            RemoveHandler cmbReportTechnology.SelectedIndexChanged, AddressOf cmbReportTechnology_SelectedIndexChanged
            RemoveHandler vCmb_SchedulerTechPack.SelectedIndexChanged, AddressOf vCmb_SchedulerTechPack_SelectedIndexChanged
            Dim dtTechnologyPackage As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, New SQLTechnologyPackage().SelectAll(TechnologyPackagesFields.TECHNOLOGY_PACKAGE_NAME))
            If (dtTechnologyPackage.IsValid) Then
                BindDevExComboBoxWithValueMember(cmbReportTechnology, dtTechnologyPackage, TechnologyPackagesFields.TECHNOLOGY_PACKAGE_ID, TechnologyPackagesFields.TECHNOLOGY_PACKAGE_NAME, "Select TechPack")
                BindDevExComboBoxWithValueMember(vCmb_SchedulerTechPack, dtTechnologyPackage, TechnologyPackagesFields.TECHNOLOGY_PACKAGE_ID, TechnologyPackagesFields.TECHNOLOGY_PACKAGE_NAME, "Select TechPack")
            Else
                ClearComboBox(cmbReportTechnology, "Select TechPack")
                ClearComboBox(vCmb_SchedulerTechPack, "Select TechPack")
            End If
            AddHandler cmbReportTechnology.SelectedIndexChanged, AddressOf cmbReportTechnology_SelectedIndexChanged
            AddHandler vCmb_SchedulerTechPack.SelectedIndexChanged, AddressOf vCmb_SchedulerTechPack_SelectedIndexChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindKPIGroup()
        Try
            Dim dtKPIGroup As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLKpiGroup.GetKPIGroupsByTech(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value))
            If (dtKPIGroup.IsValid) Then
                BindDevExComboBoxWithTagMember(cmbKPIGroup, dtKPIGroup, KPIGroupFields.KPI_GROUP_ID, KPIGroupFields.KPI_GROUP_NAME, Nothing, KPIGroupFields.KPI_GROUP_CREATOR, True)
                cmbKPIGroup.SelectedIndex = 0
            Else
                ClearComboBox(cmbKPIGroup, "ALL")
            End If
            btnAddCategory.Enabled = True
            btnAddKPI.Enabled = True
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindPredefinePeriod()
        Try
            dtPredefinedPeriodSB = DataAccessorODBC.GetDataTable(connStrSandBoxServer, New SQLPredefinedPeriod().SelectAll())
            If (dtPredefinedPeriodSB.IsValid) Then
                BindDevExComboBoxWithValueMember(vcmb_PredefinedPeriod, dtPredefinedPeriodSB.Select("Control='" & vcmb_PredefinedPeriod.Name & "'").CopyToDataTable, PredefinedPeriodFields.PREDEFINED_PERIOD_ID, PredefinedPeriodFields.GUI_TEXT, "None")
                vcmb_PredefinedPeriod.SelectedIndex = 4
            Else
                ClearComboBox(vcmb_PredefinedPeriod, "None")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindPredefinePeriodFilter()
        Try
            Dim dtPeriodFilter As DataTable = dtPredefinedPeriodSB.Select("Control='" & cmbPredefinedFilter.Name & "'").CopyToDataTable
            If (dtPeriodFilter.IsValid) Then
                BindDevExComboBoxWithValueMember(cmbPredefinedFilter, dtPeriodFilter, PredefinedPeriodFields.PREDEFINED_PERIOD_ID, PredefinedPeriodFields.GUI_TEXT, "None")
            Else
                ClearComboBox(cmbPredefinedFilter, "None")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindDashboardGroupCombo()
        Try
            Dim sqlCommand As String = SQLDashboardGroups.GetDashboardGroup("( " & DashBoardGroups_View.LICENSEUSER & OperatorConst.Equal & Chr(39) & System.Environment.UserName & Chr(39) & Library.AggregateConst.AND_Only & " " & DashBoardGroups_View.DASHBOARDGROUP_PRIVATE & OperatorConst.Equal & "0 ) " & Library.AggregateConst.OR_Only & " ( " & DashBoardGroups_View.DASHBOARDGROUP_CREATOR & OperatorConst.Equal & Chr(39) & System.Environment.UserName & "' " & AggregateConst.AND_Only & DashBoardGroups_View.DASHBOARDGROUP_PRIVATE & OperatorConst.Equal & "1 )", DashBoardGroups_View.DASHBOARDGROUP_NAME)
            Dim dt_DashboardGroup As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCommand)
            If (dt_DashboardGroup.IsValid) Then
                BindDevExComboBoxWithTagMember(cmbDashboardGroup, dt_DashboardGroup, DashBoardGroups_View.DASHBOARDGROUP_ID, DashBoardGroups_View.DASHBOARDGROUP_NAME, "Select Group", DashBoardGroups_View.DASHBOARDGROUP_CREATOR, True)
            Else
                ClearComboBox(cmbDashboardGroup, "Select Group")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindJobGroupCombo(Optional ByVal selectedIndex As Integer = 0)
        Try
            Dim sqlCommand As String = SQLJobGroups.GetJobGroup("( " & JobGroup_View.LICENSEUSER & OperatorConst.Equal & Chr(39) & System.Environment.UserName & Chr(39) & AggregateConst.AND_Only & " " & JobGroup_View.JOBGROUPPRIVATE & OperatorConst.Equal & "0 ) " & AggregateConst.OR_Only & " ( " & JobGroup_View.JOBGROUPCREATOR & OperatorConst.Equal & Chr(39) & System.Environment.UserName & "' " & AggregateConst.AND_Only & JobGroup_View.JOBGROUPPRIVATE & OperatorConst.Equal & "1 )", JobGroup_View.JOBGROUPNAME)
            Dim dt_JobGroup As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCommand)
            If (dt_JobGroup.IsValid) Then
                BindDevExComboBoxWithTagMember(cmbJobGroup, dt_JobGroup, JobGroup_View.JOBGROUPID, JobGroup_View.JOBGROUPNAME, "Select Group", JobGroup_View.JOBGROUPPRIVATE, True)
            Else
                ClearComboBox(cmbJobGroup, "Select Group")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindJobFormats(Optional ByVal selectedIndex As Integer = 0)
        Try
            Dim dtJobFormats As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLJobFormats.GetJobFormats(JobFormatFields.JobFormatOutput))
            If (dtJobFormats.IsValid) Then
                BindDevExComboBoxWithValueMember(vCmb_SchedulerFileFormat, dtJobFormats, JobFormatFields.JobFormatID, JobFormatFields.JobFormatOutput, "Select Job Format", True)
                SetComboBox(vCmb_SchedulerFileFormat, ComboSelectBased.TextBased, "CSV")
            Else
                ClearComboBox(vCmb_SchedulerFileFormat, "Select Formats")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindJobDropZones(Optional ByVal selectedIndex As Integer = 0)
        Try
            Dim dtJobDropZones As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLJobDropZones.GetJobFormats(JobDropZonesFields.JOB_DROP_ZONE_FOLDER))
            If (dtJobDropZones.IsValid) Then
                BindDevExComboBoxWithValueMember(vcmb_JobDropZone, dtJobDropZones, JobDropZonesFields.JOB_DROP_ZONE_ID, JobDropZonesFields.JOB_DROP_ZONE_FOLDER, "Select Job Drop Zone", True)
            Else
                ClearComboBox(vcmb_JobDropZone, "Select Job Drop Zone")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub SetNextRunTime()
        Dim nextRunTime As Date = DateTime.Parse(vDTP_SchedulerTriggerStartTime.EditValue).AddDays(vtxt_SchedulerDays.Text.Trim)
        nextRunTime = nextRunTime.AddHours(vtxt_SchedulerHours.Text.Trim)
        nextRunTime = nextRunTime.AddMinutes(vtxt_SchedulerMinutes.Text.Trim)
        lblNextRunTime.Text = nextRunTime
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If defaultEX = -1 Then
                defaultEX = cp.ExStyle
            End If
            If enableFormLevelDoubleBuffering = True Then
                cp.ExStyle = cp.ExStyle Or &H2000000
            Else
                cp.ExStyle = defaultEX
            End If
            Return cp
        End Get
    End Property

    Private Sub RefreshDashboardReport()
        'If (cmbDashboardTechPack.SelectedIndex > 0) Then
        ' Dim selectedTechPack As String = vCmb_DeashboardTechPack.SelectedItem.Value
        If (cmbDashboardReportGroup.SelectedIndex > 0) Then
            Dim selectedReportGroup As String = TryCast(cmbDashboardReportGroup.SelectedItem, clsComboBoxItem).Value
            BindDashboardReportGroup()
            SetComboBox(cmbDashboardReportGroup, ComboSelectBased.ValueBased, selectedReportGroup)
        Else
            BindDashboardReportGroup()
        End If
        'End If
    End Sub

    Private Sub RefreshSchedulerReport()
        If (vCmb_SchedulerTechPack.SelectedIndex > 0) Then
            'Dim selectedTechPack As String = vCmb_SchedulerTechPack.SelectedItem.Value
            If (vCmb_SchedulerReportGroup.SelectedIndex > 0) Then
                Dim selectedReportGroup As String = TryCast(vCmb_SchedulerReportGroup.SelectedItem, clsComboBoxItem).Value
                BindSchedulerReportGroup()
                SetComboBox(vCmb_SchedulerReportGroup, ComboSelectBased.ValueBased, selectedReportGroup)
            Else
                BindSchedulerReportGroup()
            End If
        End If
    End Sub

    Public Sub ConfigureIOSDatamart(ByVal frmName As String)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim form As EntityModel.IOSForm = configMgr.FindFormByName(frmName)
            If Not form Is Nothing Then
                Dim counter As Integer = 0
                ConfigurForm(Me, frmName, counter)
                Dim ctrl As EntityModel.Control = Nothing

                Dim formControls As List(Of Object) = New List(Of Object) From {
                    rbChart, rbGrid, rbExport
                }

                For Each frmControl As Object In formControls
                    ctrl = form.FindControlByName(frmControl.Name)
                    If Not ctrl Is Nothing Then
                        frmControl.Enabled = ctrl.DefaultEnable
                        frmControl.Visible = ctrl.DefaultVisible
                    End If
                Next

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Form Event"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '_logger = New clsLoggerManager()

        'Get the logger as named in the configuration file.
        _logger.SetInfo("DataMart_Load - Start")
        _logger.SetDebug("DataMart_Load - Code Implementation goes here......")

        'AddHandler cmbObjectSource.SelectedIndexChanged, AddressOf cmbObjectSource_SelectedIndexChanged

        cmbTimeResolution.Properties.Items.Clear()
        cmbTimeResolution.Properties.Items.Add(New clsComboBoxItem("DAY", "DAY", "DAY"))
        cmbTimeResolution.Properties.Items.Add(New clsComboBoxItem("HOUR", "HOUR", "HOUR"))
        cmbTimeResolution.Properties.Items.Add(New clsComboBoxItem("RAW", "RAW", "RAW"))
        cmbTimeResolution.Properties.Items.Add(New clsComboBoxItem("BH", "BH", "BH"))
        cmbTimeResolution.Properties.Items.Add(New clsComboBoxItem("WEEK", "WEEK", "WEEK"))
        cmbTimeResolution.SelectedIndex = 0

        AddHandler sccDashboard.SplitGroupPanelCollapsed, AddressOf SplitContainerDashboard_SplitGroupPanelCollapsed
    End Sub

    Private Sub frmSBMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
        Me.UseWaitCursor = True
        Application.DoEvents()
        Try
            'bind sandbox combos
            BindReportGroup()
            BindTechnologyPackage()
            BindPredefinePeriod()
            BindPredefinePeriodFilter()

            ClearComboBox(cmbObjectSource, "None")
            ClearComboBox(cmbObjectType, "None")
            BindTreeSource()

            'bind dashboard combos
            BindDashboardGroupCombo()
            BindDashboardReportGroup()

            'bind scheduler combos
            BindJobGroupCombo()
            BindJobFormats()
            BindJobDropZones()
            vDTP_SchedulerTriggerStartTime.EditValue = Now()
            SetNextRunTime()
            txtChartAxisFont.Text = "'Arial', 6, FontStyle.Regular"
            cmbCalculatedYAxis.SelectedIndex = 0
            accPeriodSelection.Tag = accPeriodSelection.Height

            btnAddCategory.Enabled = True
            btnAddKPIGroup.Enabled = True
            btnAddKPI.Enabled = True
            ConfigureIOSDatamart("frmSBMain")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.UseWaitCursor = False
            Application.DoEvents()
            _logger.SetInfo("DataMart_Load) - Finish")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

#End Region

#Region "SandBox"

    Private Sub BindOnLoadData()
        Try
            Dim selectTechnologyPackage As String = New SQLTechnologyPackage().SelectAll(TechnologyPackagesFields.TECHNOLOGY_PACKAGE_NAME, True)
            Dim selectPredefinedPeriod As String = New SQLPredefinedPeriod().SelectAll(PredefinedPeriodFields.PREDEFINED_PERIOD_ID, True)
            Dim totalSQL As String = selectTechnologyPackage & ";" & selectPredefinedPeriod & ";"
            Dim initializeDS As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, totalSQL)
            If (initializeDS IsNot Nothing) Then
                BindDevExComboBoxWithValueMember(cmbReportTechnology, initializeDS.Tables(DataBaseTableName.TBL_TECHNOLOGY_PACKAGES), TechnologyPackagesFields.TECHNOLOGY_PACKAGE_ID, TechnologyPackagesFields.TECHNOLOGY_PACKAGE_NAME, "Select TechPack", True)
                BindDevExComboBoxWithValueMember(vcmb_PredefinedPeriod, initializeDS.Tables(DataBaseTableName.TBL_PREDEFINED_PERIOD), PredefinedPeriodFields.PREDEFINED_PERIOD_ID, PredefinedPeriodFields.GUI_TEXT, "None", True)
            Else
                ClearComboBox(cmbReportTechnology, "Select TechPack")
                ClearComboBox(vcmb_PredefinedPeriod, "None")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub cmbReportTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cmbReportTechnology.SelectedIndexChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        'lblSelectedReport.Text = String.Empty
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If (cmbReportTechnology.SelectedIndex > 0) Then
                RemoveHandler cmbKPIGroup.SelectedIndexChanged, AddressOf cmbKPIGroup_SelectedIndexChanged
                BindKPIGroup()
                AddHandler cmbKPIGroup.SelectedIndexChanged, AddressOf cmbKPIGroup_SelectedIndexChanged
                Load_CounterObjectKPIData()

                BindObjectSource(dt_TechPackCounter)
                BindMeasurmentLST(dt_TechPackCounter)
                BindCounterLST(dt_TechPackCounter)
                BindKPITree(dt_TechnologyPackageKPI)
            Else
                dt_TechnologyPackageObjects = Nothing
                dt_TechPackCounter = Nothing
                dt_TechnologyPackageKPI = Nothing
                isClickedByObjectSource = False
                BindObjectSource(dt_TechPackCounter)
                isClickedByObjectSource = True
                BindObjectType(dt_TechnologyPackageObjects)
                BindMeasurmentLST(dt_TechPackCounter)
                BindCounterLST(dt_TechPackCounter)
                BindKPITree(dt_TechnologyPackageKPI)
                checkedCounter.Clear()
                checkedMeasurements.Clear()
                checkedKPI.Clear()
                checkedKPINameList.Clear()
                viewCheckedKPINameList.Clear()
                viewCheckedKPIsOnly = False
            End If
            'flp_ValueX.Controls.Clear()
            flp_ValueY.Controls.Clear()
            'reportChartGrid_SendBox.ClearData()
            lstTechCounterCheckedItems.Clear()
            lstTechMeasurementCheckedItems.Clear()
            lstTechKPICheckedItems.Clear()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub cmbKPIGroup_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (cmbKPIGroup.SelectedItem.ToString.ToUpper = "ALL") Then
                Dim selectTechnologyPackageKPI As String = SQLTechnologyKPIs.GetByTechAndCreator(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, cmbKPIGroup.SelectedItem.ToString)
                dt_TechnologyPackageKPI = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectTechnologyPackageKPI)
                BindKPITree(dt_TechnologyPackageKPI)

                'btnAddCategory.Enabled = False
                'btnAddKPI.Enabled = False
            Else
                Dim selectTechnologyPackageKPI As String = SQLTechnologyKPIs.GetByTechAndCreator(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, cmbKPIGroup.SelectedItem.ToString)
                dt_TechnologyPackageKPI = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectTechnologyPackageKPI)
                BindKPITree(dt_TechnologyPackageKPI)

                'btnAddCategory.Enabled = True
                'btnAddKPI.Enabled = True
            End If

            btnAddCategory.Enabled = True
            btnAddKPI.Enabled = True

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbObjectSource_SelectedIndexChanged(sender As Object, e As EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.UseWaitCursor = True
        Application.DoEvents()

        Try
            If (cmbObjectSource.SelectedIndex > 0) Then
                If (dt_TechnologyPackageObjects.IsValid) Then
                    Dim objectSourceFilter As String = TechnologyPackageObjectsFields.SOURCE_OBJECT_ID & OperatorConst.Equal & TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value

                    Try

                        Dim dt As DataTable = dt_TechnologyPackageObjects.Select(objectSourceFilter).CopyToDataTable
                        If dt(0)("DataIsPresent_Sum") > 0 Then
                            Dim ObjectTypeID As String = dt(0)("ObjectTypeID").ToString
                            Dim ObjectTypeParentID As String = dt(0)("ObjectTypeParentID").ToString

                            ' While cmbCMPM.Text = "CM" And ObjectTypeID <> 1
                            While ObjectTypeID <> 1
                                Dim dt_temp As DataTable = dt_TechnologyPackageObjects.Select("ObjectTypeID='" + ObjectTypeParentID + "'").CopyToDataTable
                                ObjectTypeID = dt_temp(0)("ObjectTypeID").ToString
                                ObjectTypeParentID = dt_temp(0)("ObjectTypeParentID").ToString
                                dt.Merge(dt_temp)
                            End While
                            BindObjectType(dt)

                        End If
                    Catch ex As Exception

                    End Try

                    If (isClickedByObjectSource) Then
                        BindListBoxBySource(TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value)
                    End If

                    RefreshKPITree()

                End If
            Else
                ClearComboBox(cmbObjectType, "None")
                lstTechMeasurement.Text = "None"
                lstTechCounter.Text = "None"
                If (dt_TechPackCounter.IsValid) Then
                    BindMeasurmentLST(dt_TechPackCounter)
                End If
                If (dt_TechPackCounter.IsValid) Then
                    BindCounterLST(dt_TechPackCounter)
                End If
                If (dt_TechnologyPackageKPI.IsValid AndAlso cmbKPIGroup.SelectedItem.ToString.ToUpper = "ALL") Then
                    BindKPITree(dt_TechnologyPackageKPI)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.UseWaitCursor = False
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub Load_CounterObjectKPIData()
        Try
            Dim selectTachPackCounter As String = New SQLTechnologyPackageCounters().SelectAll(True, TechnologyPackageCountersFields.TECHNOLOGY_PACKAGE_ID & OperatorConst.Equal & TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
            Dim selectTechnologyPackageObjects As String = New SQLTechnologyPackageObjects().SelectAll(True, TechnologyPackageCountersFields.TECHNOLOGY_PACKAGE_ID & OperatorConst.Equal & TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
            Dim selectTechnologyPackageKPI As String = SQLTechnologyKPIs.GetByTechAndCreator(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, cmbKPIGroup.SelectedItem.ToString)
            Dim totalSQL As String = selectTachPackCounter & ";" & selectTechnologyPackageObjects & ";" & selectTechnologyPackageKPI & ";"

            Dim kpiCountersObjectDS As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, totalSQL)
            If (kpiCountersObjectDS IsNot Nothing) Then
                dt_TechPackCounter = kpiCountersObjectDS.Tables(0)
                dt_TechnologyPackageObjects = kpiCountersObjectDS.Tables(1)
                dt_TechnologyPackageKPI = kpiCountersObjectDS.Tables(2)
            Else
                dt_TechPackCounter = Nothing
                dt_TechnologyPackageKPI = Nothing
                dt_TechnologyPackageObjects = Nothing
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindObjectSource(ByRef dtTachPackCounter As DataTable, Optional ByVal isSelectedFirst As Boolean = False)
        Try
            RemoveHandler cmbObjectSource.SelectedIndexChanged, AddressOf cmbObjectSource_SelectedIndexChanged
            If (dtTachPackCounter.IsValid) Then
                Dim array() As String = {TechnologyPackageCountersFields.SOURCE_OBJECT_NAME, TechnologyPackageCountersFields.SOURCE_OBJECT_ID}
                Dim distObjectSource As DataTable = dtTachPackCounter.DistinctCol(array)
                isClickedByObjectSource = False
                BindDevExComboBoxWithValueMember(cmbObjectSource, distObjectSource, TechnologyPackageCountersFields.SOURCE_OBJECT_ID, TechnologyPackageCountersFields.SOURCE_OBJECT_NAME, "None", isSelectedFirst)
                isClickedByObjectSource = True
            Else
                ClearComboBox(cmbObjectSource, "None")
            End If
            AddHandler cmbObjectSource.SelectedIndexChanged, AddressOf cmbObjectSource_SelectedIndexChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindObjectType(ByRef dtTechnologyPackageObjects As DataTable)
        Try
            If (dtTechnologyPackageObjects.IsValid) Then
                ''RemoveHandler cmbObjectType.SelectedIndexChanged, AddressOf cmbObjectType_SelectedIndexChanged
                Dim array() As String = {TechnologyPackageObjectsFields.OBJECT_TYPE_NAME, TechnologyPackageObjectsFields.OBJECT_TYPE_ID}
                Dim distObjectSource As DataTable = dtTechnologyPackageObjects.DistinctCol(array)
                BindDevExComboBoxWithValueMember(cmbObjectType, distObjectSource, TechnologyPackageObjectsFields.OBJECT_TYPE_ID, TechnologyPackageObjectsFields.OBJECT_TYPE_NAME, "None")
                If cmbObjectType.Properties.Items.Count > 0 Then
                    cmbObjectType.SelectedIndex = 1
                End If
                ''AddHandler cmbObjectType.SelectedIndexChanged, AddressOf cmbObjectType_SelectedIndexChanged
            Else
                ClearComboBox(cmbObjectType, "None")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindTreeSource()
        Dim lstItemCM As New clsComboBoxItem()
        lstItemCM.Text = "CM"
        lstItemCM.Value = 0
        cmbCMPM.Properties.Items.Add(lstItemCM)

        Dim lstItemPM As New clsComboBoxItem()
        lstItemPM.Text = "PM"
        lstItemPM.Value = 1
        cmbCMPM.Properties.Items.Add(lstItemPM)

        cmbCMPM.SelectedIndex = 0
    End Sub

    Private Sub BindMeasurmentLST(ByRef dtTachPackCounter As DataTable)
        Try
            lstTechMeasurement.SuspendLayout()
            lstTechMeasurement.Items.Clear()
            lstTechMeasurement.Refresh()
            If (dtTachPackCounter.IsValid) Then
                Dim array() As String = {TechnologyPackageCountersFields.MEASUREMENT_NAME, TechnologyPackageCountersFields.MEASUREMENT_ID, TechnologyPackageCountersFields.SQL_DATABASENAME}
                Dim distObjectSource As DataTable = dtTachPackCounter.DistinctCol(array)
                Dim distObjectShort As DataTable = distObjectSource.Select("", TechnologyPackageCountersFields.MEASUREMENT_NAME & " ASC").CopyToDataTable()

                SandBoxCheckedListBox.BindDataToCheckedListBox(lstTechMeasurement, distObjectShort, TechnologyPackageCountersFields.MEASUREMENT_ID, TechnologyPackageCountersFields.MEASUREMENT_NAME, TechnologyPackageCountersFields.SQL_DATABASENAME, checkedMeasurements)
                lstTechMeasurement.SelectedIndex = -1
            Else
                SandBoxCheckedListBox.Clear(lstTechMeasurement)
            End If
            lstTechMeasurement.Update()
            lstTechMeasurement.ResumeLayout()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindCounterLST(ByRef dtTachPackCounter As DataTable)
        Try
            If (dtTachPackCounter.IsValid) Then
                Dim array() As String = {TechnologyPackageCountersFields.COUNTER_NAME, TechnologyPackageCountersFields.COUNTER_ID}
                Dim distObjectSource As DataTable = dtTachPackCounter.DistinctCol(array)
                Dim distObjectShort As DataTable = distObjectSource.Select("", TechnologyPackageCountersFields.COUNTER_NAME & " ASC").CopyToDataTable()
                lstTechCounter.DataSource = distObjectShort
                lstTechCounter.DisplayMember = TechnologyPackageCountersFields.COUNTER_NAME
                lstTechCounter.ValueMember = TechnologyPackageCountersFields.COUNTER_ID

                'SandBoxCheckedListBox.BindData(vlst_TechCounter, distObjectSource, TechnologyPackageCountersFields.COUNTER_ID, TechnologyPackageCountersFields.COUNTER_NAME, checkedCounter)
                lstTechCounter.SortOrder = System.Windows.Forms.SortOrder.Ascending

                For Each li As Controls.ListBoxItem In lstTechCounterCheckedItems
                    lstTechCounter.Items.Remove(li)
                    lstTechCounter.Items.Insert(0, li)
                Next

                lstTechCounter.SelectedIndex = -1
                distObjectShort.Dispose()
            Else
                SandBoxCheckedListBox.Clear(lstTechCounter)
                For Each li As Controls.ListBoxItem In lstTechCounterCheckedItems
                    lstTechCounter.Items.Remove(li)
                    lstTechCounter.Items.Insert(0, li)
                Next
                lstTechCounter.SelectedIndex = -1
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BindKPITree(ByRef dtTechnologyPackageKPI As DataTable)
        Try
            checkedKPI.Clear()
            'checkedKPINameList.Clear()
            lstTechKPI.SuspendLayout()
            lstTechKPI.BeginUnboundLoad()
            RemoveHandler lstTechKPI.FocusedNodeChanged, AddressOf lstTechKPI_FocusedNodeChanged
            RemoveHandler lstTechKPI.FocusedColumnChanged, AddressOf lstTechKPI_FocusedColumnChanged
            RemoveHandler lstTechKPI.ShowingEditor, AddressOf lstTechKPI_ShowingEditor
            If (dtTechnologyPackageKPI.IsValid) Then
                Dim colList() As String = {KPIGroupFields.KPI_CATEGORY_NAME, KPIGroupFields.KPI_CATEGORY_ID, KPIGroupFields.KPI_NAME, KPIGroupFields.KPI_ID}
                lstTechKPI.Columns.Clear()
                For i As Integer = 0 To colList.Length - 1
                    Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
                    col1.Caption = colList(i)
                    col1.VisibleIndex = i
                    If colList(i) = KPIGroupFields.KPI_CATEGORY_NAME Then
                        lstTechKPI.AutoFillColumn = col1
                        col1.Visible = True
                    Else
                        col1.Visible = False
                    End If
                    lstTechKPI.Columns.Add(col1)
                Next

                'Adding checkbox column
                Dim chkCol As New Columns.TreeListColumn()
                chkCol.Caption = ""
                chkCol.Name = "chk"
                chkCol.FieldName = "riChkEdit"
                chkCol.VisibleIndex = 4
                chkCol.OptionsColumn.ReadOnly = False
                Dim riChk As New Repository.RepositoryItemCheckEdit()
                riChk.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard
                riChk.AllowGrayed = False
                riChk.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
                chkCol.ColumnEdit = riChk
                chkCol.Visible = True
                AddHandler riChk.CheckedChanged, AddressOf riChk_CheckedChanged
                lstTechKPI.Columns.Add(chkCol)

                lstTechKPI.Nodes.Clear()
                Dim dbNode As TreeListNode = Nothing

                If cmbKPIGroup.SelectedItem.ToString.ToUpper = "ALL" Then

                    Dim tlNode As TreeListNode = lstTechKPI.Nodes.Add(New Object() {dtTechnologyPackageKPI.Rows(0)(KPIGroupFields.KPI_CATEGORY_NAME)})
                    tlNode.Tag = dtTechnologyPackageKPI.Rows(0)(KPIGroupFields.KPI_CATEGORY_ID)

                    Dim distinctCol() As String = {KPIGroupFields.KPI_ID, KPIGroupFields.KPI_NAME, KPIGroupFields.KPI_CREATOR}
                    Dim dtDistinctGroupName As DataTable = dtTechnologyPackageKPI.DistinctCol(distinctCol)

                    If (dtDistinctGroupName.IsValid) Then
                        Dim drGroupName As DataRow() = dtDistinctGroupName.Select("", KPIGroupFields.KPI_NAME & " ASC ")
                        For Each rowGroupName As DataRow In drGroupName
                            If (Not IsDBNull(rowGroupName(KPIGroupFields.KPI_NAME))) Then
                                dbNode = lstTechKPI.AppendNode(New Object() {rowGroupName(KPIGroupFields.KPI_NAME), rowGroupName(KPIGroupFields.KPI_ID), rowGroupName(KPIGroupFields.KPI_CREATOR)}, tlNode)
                                dbNode.Tag = rowGroupName(KPIGroupFields.KPI_ID).ToString
                            End If
                        Next
                    End If

                Else

                    Dim distinctCol() As String = {KPIGroupFields.KPI_CATEGORY_NAME, KPIGroupFields.KPI_CATEGORY_ID, KPIGroupFields.KPI_CATEGORY_ORDINAL}
                    Dim dt As DataTable = dtTechnologyPackageKPI.DistinctCol(distinctCol)

                    If (dt.IsValid) Then
                        Dim drCatName As DataRow() = dt.Select("", KPIGroupFields.KPI_CATEGORY_ORDINAL & " ASC ")

                        For Each rowCatName As DataRow In drCatName
                            If (Not IsDBNull(rowCatName(KPIGroupFields.KPI_CATEGORY_NAME))) Then
                                dbNode = lstTechKPI.Nodes.Add(New Object() {rowCatName(KPIGroupFields.KPI_CATEGORY_NAME)})
                                dbNode.Tag = rowCatName(KPIGroupFields.KPI_CATEGORY_ID).ToString

                                Dim kpiFilter As String = KPIGroupFields.KPI_CATEGORY_ID & " = " & rowCatName(KPIGroupFields.KPI_CATEGORY_ID)
                                Dim dtKpi As DataTable = dtTechnologyPackageKPI.SelectedRowsAsTable(kpiFilter)
                                Dim distinctColKPI() As String = {KPIGroupFields.KPI_ID, KPIGroupFields.KPI_NAME, KPIGroupFields.KPI_CREATOR}
                                Dim dtDistinctKPI As DataTable = dtKpi.DistinctCol(distinctColKPI)

                                If dtDistinctKPI.IsValid Then
                                    Dim dr As DataRow() = dtDistinctKPI.Select("", KPIGroupFields.KPI_NAME & " ASC ")
                                    For Each drow As DataRow In dr
                                        Dim rptNode As TreeListNode = lstTechKPI.AppendNode(New Object() {drow.Item(KPIGroupFields.KPI_NAME).ToString, drow.Item(KPIGroupFields.KPI_ID).ToString, drow.Item(KPIGroupFields.KPI_CREATOR)}, dbNode)
                                        rptNode.Tag = drow.Item(KPIGroupFields.KPI_ID).ToString
                                    Next
                                End If
                            End If
                        Next
                    End If

                End If
            Else
                SandBoxTreeView.Clear(lstTechKPI)
            End If
            lstTechKPI.EndUnboundLoad()
            lstTechKPI.ResumeLayout()

            If lstTechKPI.Nodes.Count > 0 Then
                lstTechKPI.SelectNode(lstTechKPI.Nodes(0))
                lstTechKPI.SetFocusedNode(lstTechKPI.Nodes(0))
                lstTechKPI.AutoFillColumn = lstTechKPI.Columns(0)
                lstTechKPI.ExpandAll()
            End If

            lstTechKPI.OptionsBehavior.Editable = False
            lstTechKPI.OptionsBehavior.ReadOnly = True

            AddHandler lstTechKPI.FocusedNodeChanged, AddressOf lstTechKPI_FocusedNodeChanged
            AddHandler lstTechKPI.FocusedColumnChanged, AddressOf lstTechKPI_FocusedColumnChanged
            AddHandler lstTechKPI.ShowingEditor, AddressOf lstTechKPI_ShowingEditor
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub lstTechCounter_ItemCheck(sender As Object, e As Controls.ItemCheckEventArgs) Handles lstTechCounter.ItemCheck
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim lstRowView As System.Data.DataRowView = lstTechCounter.GetItem(e.Index)
            Dim isNeedToUpdate As Boolean = False
            If (lstRowView IsNot Nothing) Then

                If (e.State = CheckState.Checked) Then
                    If (Not checkedCounter.Contains(lstRowView.Item("CounterID").ToString)) Then
                        checkedCounter.Add(lstRowView.Row.Item("CounterID").ToString)
                        lstTechCounter.SetItemCheckState(e.Index, CheckState.Checked)
                        lstTechCounter.SetSelected(e.Index, True)
                        BindListBoxByCounter()
                    End If
                    Dim counterFilter As String = String.Empty
                    counterFilter = TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & lstRowView.Item("CounterID").ToString
                    Dim sourceObjectId As String = "0"
                    If (Not String.IsNullOrEmpty(counterFilter)) Then
                        Dim dtTachPackCounter As DataTable = dt_TechPackCounter.SelectedRowsAsTable(counterFilter)
                        If (dtTachPackCounter.IsValid) Then
                            isClickedByObjectSource = False
                            sourceObjectId = dtTachPackCounter.Rows(0)(TechnologyPackageCountersFields.SOURCE_OBJECT_ID)
                            Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(sourceObjectId, cmbObjectSource)
                            ''vcmb_ObjectSource.Properties.Items(.ToList().FindIndex(Function(c) c.Value = sourceObjectId)
                            If cmbItem IsNot Nothing Then
                                cmbObjectSource.SelectedItem = cmbItem
                            End If
                            isClickedByObjectSource = True
                        End If
                    End If
                    'add to checkitems list of vlst_techcounter
                    If Not lstTechCounterCheckedItems.Contains(lstTechCounter.GetItem(e.Index)) Then
                        lstTechCounterCheckedItems.Add(lstTechCounter.GetItem(e.Index))
                    End If
                    If (tvReportGroup.FocusedNode IsNot Nothing AndAlso tvReportGroup.FocusedNode.Level = 2) Then
                        flp_AddVsandBox(lstTechCounter.GetItemText(e.Index), lstTechCounter.GetItemValue(e.Index), DatamartFieldType.Counter, flp_ValueY)
                        GetCounterInfo(lstRowView.Item("CounterID").ToString, sourceObjectId)
                    End If

                Else
                    If (checkedCounter.Contains(lstRowView.Item("CounterID").ToString)) Then
                        checkedCounter.Remove(lstRowView.Item("CounterID").ToString)
                        lstTechCounter.SetItemCheckState(e.Index, CheckState.Unchecked)
                        lstTechCounter.SetSelected(e.Index, False)
                        If (checkedCounter.Count > 0) Then
                            BindListBoxByCounter()
                        Else
                            isNeedToUpdate = True
                        End If
                    End If
                    tlpCounterInfo.SuspendLayout()
                    tlpCounterInfo.RowCount = 1
                    tlpCounterInfo.RowStyles.Clear()
                    tlpCounterInfo.Controls.Clear()
                    tlpCounterInfo.ResumeLayout()
                    lstTechCounterCheckedItems.Remove(lstTechCounter.GetItem(e.Index))
                    flp_RemoveSandbox(lstTechCounter.GetItemText(e.Index), flp_ValueY)
                End If
            Else

            End If

            If (checkedCounter.Count <= 0 AndAlso isNeedToUpdate) Then
                'BindMeasurmentLST(dt_TachPackCounter)
                'BindKPILST(dt_TechnologyPackageKPI)
                lstTechCounter.Text = ""
                lstTechMeasurement.Text = ""
                BindListBoxBySource(TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub lstTechCounter_ItemChecking(sender As Object, e As Controls.ItemCheckingEventArgs) Handles lstTechCounter.ItemChecking
        If rightMouseOnListbox = False Then
            Dim listBox As CheckedListBoxControl = TryCast(sender, CheckedListBoxControl)
            Dim viewInfo As CheckedListBoxViewInfo = TryCast(listBox.GetViewInfo(), CheckedListBoxViewInfo)
            Dim point As Point = listBox.PointToClient(Control.MousePosition)
            Dim itemInfo As CheckedListBoxViewInfo.CheckedItemInfo = TryCast(viewInfo.GetItemInfoByPoint(point), CheckedListBoxViewInfo.CheckedItemInfo)
            If itemInfo IsNot Nothing Then
                e.Cancel = Not itemInfo.CheckArgs.Bounds.Contains(point)
            End If
        End If
    End Sub

    Private Sub GetCounterInfo(ByVal counterId As String, ByVal sourceObjectId As String)
        Try
            Dim dtCounterInfo As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLTechnologySourceObjects.GetCounterInfo(counterId, sourceObjectId))
            If (dtCounterInfo.IsValid) Then
                Dim rowIndex As Integer = 0
                tlpCounterInfo.SuspendLayout()
                tlpCounterInfo.RowCount = 1
                tlpCounterInfo.RowStyles.Clear()
                tlpCounterInfo.Controls.Clear()

                For Each col As DataColumn In dtCounterInfo.Columns
                    Dim lblHeader As LabelControl = New LabelControl
                    lblHeader.Text = col.ColumnName.ToString
                    lblHeader.BackColor = Color.LightGray
                    Dim vlblHeaderValue As LabelControl = New LabelControl
                    vlblHeaderValue.Text = dtCounterInfo.Rows(0)(col).ToString
                    If Len(vlblHeaderValue.Text) > 0 Then

                        tlpCounterInfo.RowStyles.Add(New RowStyle(SizeType.Absolute, 25.0!))
                        tlpCounterInfo.RowCount = tlpCounterInfo.RowCount + 1

                        tlpCounterInfo.Controls.Add(lblHeader, 0, tlpCounterInfo.RowCount - 2)
                        lblHeader.Dock = DockStyle.Fill
                        tlpCounterInfo.Controls.Add(vlblHeaderValue, 2, tlpCounterInfo.RowCount - 2)
                        vlblHeaderValue.Dock = DockStyle.Fill
                    End If
                Next
                tlpCounterInfo.ResumeLayout()

                ''tlp_CounterInfo
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Function CreateLable(ByVal lblName As String, ByVal lblText As String, ByVal fontColor As System.Drawing.Color) As LabelControl
        Dim vlbl_obj As New LabelControl()
        vlbl_obj.BackColor = System.Drawing.Color.Transparent
        'vlbl_obj.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        'vlbl_obj.Ellipsis = False
        'vlbl_obj.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        'vlbl_obj.Multiline = True
        vlbl_obj.Name = "vlbl" & lblName
        vlbl_obj.Size = New System.Drawing.Size(179, 14)
        vlbl_obj.Text = lblText
        'vlbl_obj.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        'vlbl_obj.UseMnemonics = True
        'vlbl_obj.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        vlbl_obj.Dock = DockStyle.Fill
        vlbl_obj.ForeColor = fontColor
        vlbl_obj.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Return vlbl_obj
    End Function

    Private Sub lstTechCounter_DragDrop(sender As Object, e As DragEventArgs) Handles lstTechCounter.DragDrop
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub lstTechCounter_DragOver(sender As Object, e As DragEventArgs) Handles lstTechCounter.DragOver
        If e.Data.GetDataPresent(GetType(System.String)) Then
            e.Effect = DragDropEffects.Move
            isDraggedCounterItem = True
        Else
            e.Effect = DragDropEffects.None
        End If

        If e.Data.GetDataPresent(GetType(System.String)) Then
            e.Effect = DragDropEffects.Move
            isDraggedCounterItem = True
        Else
            e.Effect = DragDropEffects.None
        End If
        Try
            If e.Data.GetDataPresent(GetType(System.String)) Then
                e.Effect = DragDropEffects.Move
                isDraggedCounterItem = True
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub cms_Counter_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_Counter.Opening
        Dim lstCounter As CheckedListBoxControl = CType(cms_Counter.SourceControl, CheckedListBoxControl)
        Dim countchecked As Integer = 0
        Try
            'count checked boxes
            countchecked = lstCounter.CheckedItemsCount

            'enable/disable copy
            If countchecked > 0 Then
                tsmi_CounterCopy.Text = "Copy - Objects: " & countchecked
                tsmi_CounterCopy.Enabled = True
            Else
                tsmi_CounterCopy.Text = "Copy"
                tsmi_CounterCopy.Enabled = False
            End If

            'check clipboard
            Dim s As String = Clipboard.GetText()
            Dim rows() As String = s.Split(ControlChars.NewLine)
            Dim i, j As Integer
            If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                tsmi_CounterPaste.Text = "Paste - Objects: ?"
                tsmi_CounterPaste.Enabled = True
            Else
                Dim clipboardmatches As Integer = 0
                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                    For j = 0 To bufferCell.Length - 1
                        If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                            bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                        End If
                        If bufferCell(j).ToString.Contains("'") Then
                            bufferCell(j) = bufferCell(j).ToString.Replace("'", "")
                        End If
                        If bufferCell(j).Trim <> "" Then
                            If lstCounter.FindStringExact(bufferCell(j).Trim) <> -1 Then
                                clipboardmatches = clipboardmatches + 1
                            End If
                        End If
                    Next
                Next

                'enable/disable paste
                If clipboardmatches > 0 Then
                    tsmi_CounterPaste.Text = "Paste - Objects: " & clipboardmatches
                    tsmi_CounterPaste.Enabled = True
                Else
                    tsmi_CounterPaste.Text = "Paste"
                    tsmi_CounterPaste.Enabled = False
                End If
                lstCounter.Cursor = Cursors.Arrow
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_CounterCopy_Click(sender As Object, e As EventArgs) Handles tsmi_CounterCopy.Click
        Clipboard.Clear()
        Try
            Dim copystring As String = ToStringList(lstTechCounter)
            copystring = copystring.Replace(",", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            lstTechCounter.Cursor = Cursors.Arrow
        End Try
    End Sub

    Public Function ToStringList(ByVal cbl As DevExpress.XtraEditors.CheckedListBoxControl) As String
        Dim separator As String = ","
        Dim values As New List(Of String)

        For i As Integer = 0 To cbl.CheckedItems.Count - 1
            values.Add(cbl.GetItemText(cbl.CheckedIndices(i)))
        Next

        Return String.Join(separator, values)
    End Function

    Private Sub tsmi_CounterPaste_Click(sender As Object, e As EventArgs) Handles tsmi_CounterPaste.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim s As String = Clipboard.GetText()
            Dim rows() As String = s.Split(ControlChars.NewLine)
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
                        If bufferCell(j).ToString.Contains("'") Then
                            bufferCell(j) = bufferCell(j).ToString.Replace("'", "")
                        End If
                        Dim itemIndex As Integer = lstTechCounter.FindStringExact(bufferCell(j).Trim)
                        If itemIndex <> -1 Then
                            lstTechCounter.SetItemCheckState(itemIndex, CheckState.Checked)
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub vlst_TechKPI_DragOver(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(GetType(System.String)) Then
            e.Effect = DragDropEffects.Move
            isDraggedKPIItem = True
        Else
            e.Effect = DragDropEffects.None
        End If
        If e.Data.GetDataPresent(GetType(System.String)) Then
            e.Effect = DragDropEffects.Move
            isDraggedKPIItem = True
        Else
            e.Effect = DragDropEffects.None
        End If
        Try
            If e.Data.GetDataPresent(GetType(System.String)) Then
                e.Effect = DragDropEffects.Move
                isDraggedKPIItem = True
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub lstTechKPI_CustomDrawNodeCell(sender As Object, e As CustomDrawNodeCellEventArgs) Handles lstTechKPI.CustomDrawNodeCell
        Try
            If (e.Node.Level = 0) Then
                If e.Column.FieldName = "riChkEdit" Then
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds)
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lstTechKPI_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Try
            e.Cancel = False
            If lstTechKPI.FocusedColumn.FieldName = "riChkEdit" Then
                If lstTechKPI.FocusedNode.Level = 0 Then
                    e.Cancel = True
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub lstTechKPI_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles lstTechKPI.CellValueChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim treeNode As TreeListNode = e.Node
            If (e.Value IsNot Nothing) Then
                'If Not (e.Value = True Or e.Value = False) Then
                Try
                    If (e.Value.GetType.ToString <> "System.Boolean") AndAlso Not (e.Value = treeNode.Item(KPIGroupFields.KPI_CATEGORY_NAME)) Then
                        Dim selectedNodeId As String = lstTechKPI.FocusedNode.Tag
                        If (treeNode.Level = 0) Then
                            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiCategory.ModifyCategory(selectedNodeId, e.Value.ToString))
                            treeNode.Item(KPIGroupFields.KPI_CATEGORY_NAME) = e.Value.ToString
                        End If
                    End If
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                Finally
                End Try
                'End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub BindListBoxBySource(ByVal sourceObjectID As String)
        Dim counterFilters As String = "" ''= TechnologyPackageCountersFields.COUNTER_ID & OpratorConst.Equel & selectedItem.Value
        checkedCounter.Clear()
        ' checkedMesurments.Clear()
        checkedKPI.Clear()
        counterFilters = TechnologyPackageCountersFields.SOURCE_OBJECT_ID & OperatorConst.Equal & sourceObjectID

        If (Not String.IsNullOrEmpty(counterFilters)) Then
            ''counterFilters = counterFilters.Remove(counterFilters.Length - 4, 4)
            'Try
            '    Dim dtTechPackCounter As DataTable = dt_TachPackCounter.SelectedRowsAsTable(counterFilters)
            '    BindObjectSource(dtTechPackCounter, 1)
            'Catch EX As Exception
            '    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
            'End Try

            ''Dim objectSourceFilter As String = IIf(vcmb_ObjectSource.SelectedIndex > 0, AggregateConst.AND_Only & TechnologyPackageCountersFields.SOURCE_OBJECT_ID & OperatorConst.Equal & vcmb_ObjectSource.SelectedItem.Value, "")
            ''UPDATE LISTBOX(3) COUNTER
            Try
                If lstTechMeasurement.Text <> sourceObjectID Or lstTechMeasurement.Items.Count = 0 Then
                    Dim dtTachPackCounterMeaserment As DataTable = dt_TechPackCounter.SelectedRowsAsTable(counterFilters)
                    BindMeasurmentLST(dtTachPackCounterMeaserment)
                    lstTechMeasurement.Text = sourceObjectID
                End If

            Catch EX As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", EX.Message)
            End Try

            Try
                If lstTechCounter.Text <> sourceObjectID Or lstTechCounter.Items.Count = 0 Or checkedMeasurements.Count = 0 Then
                    Dim measurementFilter As String = ""
                    For Each li As Object In lstTechMeasurementCheckedItems
                        measurementFilter = measurementFilter + "MeasurementID= '" + li.Value.ToString + "' OR "
                    Next
                    If measurementFilter.Length > 0 Then
                        measurementFilter = " (" + measurementFilter.Substring(0, measurementFilter.Count - 4) + ") "
                    End If

                    If counterFilters.Length > 0 And measurementFilter.Length > 0 Then
                        counterFilters = counterFilters + " AND " + measurementFilter
                    End If
                    Dim dtTachPackCounter As DataTable = dt_TechPackCounter.SelectedRowsAsTable(counterFilters)
                    BindCounterLST(dtTachPackCounter)
                    lstTechCounter.Text = sourceObjectID
                End If
            Catch EX As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", EX.Message)
            End Try
            ''UPDATE LISTBOX(4) KPI
            Try
                If (cmbKPIGroup.SelectedItem.ToString.ToUpper = "ALL") Then
                    If viewCheckedKPIsOnly = False Then
                        Dim dtTechnologyPackageKPI As DataTable = dt_TechnologyPackageKPI.SelectedRowsAsTable(counterFilters)
                        BindKPITree(dtTechnologyPackageKPI)
                    End If
                End If
            Catch EX As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", EX.Message)
            End Try

        End If
    End Sub

    Private Sub BindListBoxByCounter()
        Dim counterFilters As String = "" ''= TechnologyPackageCountersFields.COUNTER_ID & OpratorConst.Equel & selectedItem.Value

        If (checkedCounter.Count > 0) Then
            For Each checkedId As String In checkedCounter
                counterFilters = counterFilters & TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & checkedId & AggregateConst.OR_Only
            Next
        End If
        If (Not String.IsNullOrEmpty(counterFilters)) Then
            counterFilters = counterFilters.Remove(counterFilters.Length - 4, 4)
            Dim objectSourceFilter As String = IIf(cmbObjectSource.SelectedIndex > 0, AggregateConst.AND_Only & TechnologyPackageCountersFields.SOURCE_OBJECT_ID & OperatorConst.Equal & TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value, "")

            ''UPDATE LISTBOX(3) COUNTER
            Try
                Dim dtTachPackCounterMeasurment As DataTable = dt_TechPackCounter.SelectedRowsAsTable(counterFilters)
                BindMeasurmentLST(dtTachPackCounterMeasurment)
                '' vlst_TechMeasurement.SmartScrollEnabled = True
            Catch EX As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", EX.Message)
            End Try


            ''UPDATE LISTBOX(4) KPI
            Try
                Dim dtTechnologyPackageKPI As DataTable = dt_TechnologyPackageKPI.SelectedRowsAsTable(counterFilters & objectSourceFilter)
                If (cmbKPIGroup.SelectedItem.ToString.ToUpper = "ALL") Then
                    BindKPITree(dtTechnologyPackageKPI)
                End If
            Catch EX As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", EX.Message)
            End Try

        End If
    End Sub

    Private Sub BindListBoxByKPI()
        Dim kpiFilters As String = ""
        If (checkedKPI.Count > 0) Then
            For Each checkedId As String In checkedKPI
                kpiFilters = kpiFilters & TechnologyPackageKPIFields.KPI_ID & OperatorConst.Equal & checkedId & AggregateConst.OR_Only
            Next
        End If
        If (Not String.IsNullOrEmpty(kpiFilters)) Then
            kpiFilters = kpiFilters.Remove(kpiFilters.Length - 4, 4)

            Dim dtTachPackCounterMeaserment As DataTable = dt_TechnologyPackageKPI.SelectedRowsAsTable(kpiFilters)
            Dim dtMeasermentFiltered As DataTable = New DataTable()
            Dim dtCounterFiltered As DataTable = New DataTable()
            If (dtTachPackCounterMeaserment.IsValid) Then
                dtMeasermentFiltered = dtTachPackCounterMeaserment.DistinctCol(TechnologyPackageCountersFields.MEASUREMENT_ID)
            End If

            If (dtTachPackCounterMeaserment.IsValid) Then
                dtCounterFiltered = dtTachPackCounterMeaserment.DistinctCol(TechnologyPackageCountersFields.COUNTER_ID)
            End If

            ''UPDATE ListBox(3) Measurement
            Try
                If (dtMeasermentFiltered.IsValid) Then
                    Dim measermentFilters As String = ""
                    For Each dr As DataRow In dtMeasermentFiltered.Rows
                        measermentFilters = measermentFilters & TechnologyPackageCountersFields.MEASUREMENT_ID & OperatorConst.Equal & dr(TechnologyPackageCountersFields.MEASUREMENT_ID) & AggregateConst.OR_Only
                    Next
                    If (Not String.IsNullOrEmpty(measermentFilters)) Then
                        measermentFilters = measermentFilters.Remove(measermentFilters.Length - 4, 4)
                        BindMeasurmentLST(dt_TechPackCounter.SelectedRowsAsTable(measermentFilters))
                    End If
                End If
            Catch EX As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", EX.Message)
            End Try

            ''Update ListBox(2) Counter 
            Try
                If (dtCounterFiltered.IsValid) Then
                    Dim counterFilters As String = ""
                    For Each dr As DataRow In dtCounterFiltered.Rows
                        counterFilters = counterFilters & TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & dr(TechnologyPackageCountersFields.COUNTER_ID) & AggregateConst.OR_Only
                    Next
                    If (Not String.IsNullOrEmpty(counterFilters)) Then
                        counterFilters = counterFilters.Remove(counterFilters.Length - 4, 4)
                        BindCounterLST(dt_TechPackCounter.SelectedRowsAsTable(counterFilters))
                    End If
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try

        End If
    End Sub

    Private Sub lstTechMeasurement_ItemCheck(sender As Object, e As Controls.ItemCheckEventArgs) Handles lstTechMeasurement.ItemCheck
        Try
            If Not e Is Nothing Then
                Dim selectedItem As Controls.CheckedListBoxItem = lstTechMeasurement.GetItem(e.Index)
                'Dim selectedItem As VIBlend.WinForms.Controls.ListItem = vlst_TechMeasurement.GetSelectedItem()
                Dim isNeedToUpdate As Boolean = False

                'If (selectedItem IsNot Nothing) Then
                'If (vlst_TechMeasurement.SelectedIndex > -1) Then

                Try
                    Try
                        'Dim selectedItem As VIBlend.WinForms.Controls.ListItem = vlst_TechMeasurement.GetSelectedItem()
                        If (selectedItem IsNot Nothing) Then
                            Dim measurmentFilter As String = String.Empty
                            measurmentFilter = TechnologyPackageCountersFields.MEASUREMENT_ID & OperatorConst.Equal & selectedItem.Value
                            If (Not String.IsNullOrEmpty(measurmentFilter)) Then
                                Dim dtTachPackCounter As DataTable = dt_TechPackCounter.SelectedRowsAsTable(measurmentFilter)
                                If (dtTachPackCounter.IsValid) Then
                                    isClickedByObjectSource = False
                                    Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(dtTachPackCounter.Rows(0)(TechnologyPackageCountersFields.SOURCE_OBJECT_ID), cmbObjectSource)
                                    ''vcmb_ObjectSource.Properties.Items.ToList().FindIndex(Function(c) c.Value = dtTachPackCounter.Rows(0)(TechnologyPackageCountersFields.SOURCE_OBJECT_ID))

                                    If cmbItem IsNot Nothing Then
                                        cmbObjectSource.SelectedItem = cmbItem
                                    End If
                                    isClickedByObjectSource = True
                                End If
                                ''BindObjectSource(dtTachPackCounter, 1)
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                End Try

                If (e.State = CheckState.Checked) Then
                    If (Not checkedMeasurements.Contains(selectedItem.Value)) Then
                        checkedMeasurements.Add(selectedItem.Value)

                        If Not lstTechMeasurementCheckedItems.Contains(lstTechMeasurement.GetItem(e.Index)) Then
                            lstTechMeasurementCheckedItems.Add(lstTechMeasurement.GetItem(e.Index))
                        End If
                        selectedItem.CheckState = CheckState.Checked
                        BindListBoxByMeasurment()
                    End If
                Else
                    If (checkedMeasurements.Contains(selectedItem.Value)) Then
                        checkedMeasurements.Remove(selectedItem.Value)
                        lstTechMeasurementCheckedItems.Remove(lstTechMeasurement.GetItem(e.Index))

                        selectedItem.CheckState = CheckState.Unchecked
                        'vcmb_ObjectSource.Text = "None"
                        If (checkedMeasurements.Count > 0) Then
                            BindListBoxByMeasurment()
                        Else
                            ''vlst_TechMeasurement.Items.Clear()
                            isNeedToUpdate = True
                        End If

                    End If
                End If

                If (isNeedToUpdate) Then
                    If (checkedMeasurements.Count <= 0) Then
                        lstTechMeasurement.Text = ""
                        BindListBoxBySource(TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value)
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
        End Try
    End Sub

    Private Sub lstTechMeasurement_ItemChecking(sender As Object, e As Controls.ItemCheckingEventArgs) Handles lstTechMeasurement.ItemChecking
        Dim listBox As CheckedListBoxControl = TryCast(sender, CheckedListBoxControl)
        Dim viewInfo As CheckedListBoxViewInfo = TryCast(listBox.GetViewInfo(), CheckedListBoxViewInfo)
        Dim point As Point = listBox.PointToClient(Control.MousePosition)
        Dim itemInfo As CheckedListBoxViewInfo.CheckedItemInfo = TryCast(viewInfo.GetItemInfoByPoint(point), CheckedListBoxViewInfo.CheckedItemInfo)
        e.Cancel = Not itemInfo.CheckArgs.Bounds.Contains(point)
    End Sub

    Private Sub BindListBoxByMeasurment()
        Dim measurmentFilter As String = String.Empty  '' TechnologyPackageCountersFields.MEASUREMENT_ID & OpratorConst.Equel & selectedItem.Value

        If (checkedMeasurements.Count > 0) Then
            For Each checkedId As String In checkedMeasurements
                measurmentFilter = measurmentFilter & TechnologyPackageCountersFields.MEASUREMENT_ID & OperatorConst.Equal & checkedId & AggregateConst.OR_Only
            Next
        End If
        If (Not String.IsNullOrEmpty(measurmentFilter)) Then
            measurmentFilter = measurmentFilter.Remove(measurmentFilter.Length - 4, 4)

            Dim objectSourceFilter As String = IIf(cmbObjectSource.SelectedIndex > 0, AggregateConst.AND_Only & TechnologyPackageCountersFields.SOURCE_OBJECT_ID & OperatorConst.Equal & TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value, "")
            Dim filters As String = measurmentFilter & objectSourceFilter
            ''Update ListBox(2) Measurement 
            Try
                Dim dtTachPackCounterLst As DataTable = dt_TechPackCounter.SelectedRowsAsTable(filters)
                BindCounterLST(dtTachPackCounterLst)
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try

            ''Update ListBox(4) KPI
            Try
                If (cmbKPIGroup.SelectedItem.ToString.ToUpper = "ALL") Then
                    Dim dtTechnologyPackageKPI As DataTable = dt_TechnologyPackageKPI.SelectedRowsAsTable(filters)
                    BindKPITree(dtTechnologyPackageKPI)
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try

        End If
    End Sub

    Private Sub cmbPredefinedPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles vcmb_PredefinedPeriod.SelectedIndexChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim cmb As ComboBoxEdit = CType(sender, ComboBoxEdit)
            If cmb.SelectedIndex > 0 Then
                'Dim dtPredefinePeriod As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, New SQLPredefinedPeriod().SelectAll())
                Dim dr() As DataRow = dtPredefinedPeriodSB.Select("PredefinedPeriodID = " & TryCast(cmb.SelectedItem, clsComboBoxItem).Value & " And Control='" & cmb.Name & "'")
                If Not dr Is Nothing Then
                    If dr.Count > 0 Then
                        Dim SQL As String = " SELECT " & dr(0)("SQLStart").ToString
                        SQL = SQL & ", " & dr(0)("SQLEnd").ToString

                        Dim dtPeriod As New DataTable
                        dtPeriod = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQL)
                        If dtPeriod IsNot Nothing AndAlso dtPeriod.Rows.Count > 0 Then
                            dtEditStartTime.EditValue = dtPeriod.Rows(0)(0)
                            dtEditStartTime.EditValue = dtEditStartTime.DateTime.ToString("yyyy/MM/dd hh:" & "00")
                            dtEditEndTime.EditValue = dtPeriod.Rows(0)(1)
                            dtEditEndTime.EditValue = dtEditEndTime.DateTime.ToString("yyyy/MM/dd hh:" & "00")
                            If xtcPSFilterStats.SelectedTabPage.Text = "Days" Then
                                dtEditEndTime.EditValue = dtEditEndTime.DateTime
                                dtEditStartTime.EditValue = dtEditStartTime.DateTime
                            ElseIf xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
                                dtEditEndTime.EditValue = dtEditEndTime.DateTime.AddHours(23).AddMinutes(59)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbPredefinedFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPredefinedFilter.SelectedIndexChanged
        rdoFilterDaysInc.Checked = False
        rdoFilterDaysExc.Checked = False
        rdoFilterHrsInc.Checked = False
        rdoFilterHrsExc.Checked = False
        dateNavigator.SelectedRanges.Clear()
        hourList.Clear()
        For Each ctrl As Control In tlpFilterHoursStats.Controls
            CType(ctrl, IOSToggleButton).ToggleState = CheckState.Unchecked
        Next
        dateNavigator.Refresh()
    End Sub

    Private Sub tglButton_Click(sender As Object, e As EventArgs) Handles tgl0.Click, tgl1.Click, tgl2.Click, tgl3.Click, tgl4.Click, tgl5.Click, tgl6.Click, tgl7.Click, tgl8.Click, tgl9.Click,
                                                                          tgl10.Click, tgl11.Click, tgl12.Click, tgl13.Click, tgl14.Click, tgl15.Click, tgl16.Click, tgl17.Click, tgl18.Click,
                                                                          tgl19.Click, tgl20.Click, tgl21.Click, tgl22.Click, tgl23.Click
        Dim btnToggle As IOSToggleButton = CType(sender, IOSToggleButton)
        btnToggle.ChangeToggleState()
        If btnToggle.ToggleState = CheckState.Checked Then
            If Not hourList.Contains(btnToggle.Tag) Then
                hourList.Add(btnToggle.Tag)
            End If
        ElseIf btnToggle.ToggleState = CheckState.Unchecked Then
            If hourList.Contains(btnToggle.Tag) Then
                hourList.Remove(btnToggle.Tag)
            End If
        End If
    End Sub

    Private Sub xtcPSFilterStats_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles xtcPSFilterStats.SelectedPageChanged
        cmbPredefinedFilter.SelectedIndex = 0
        cmbPredefinedFilter_SelectedIndexChanged(Nothing, Nothing)
        If e.Page.Text = "Days" Then
            dtEditEndTime.EditValue = dtEditEndTime.DateTime.Date
            dateNavigator.SelectedRanges.Clear()
        ElseIf e.Page.Text = "Hours" Then
            dtEditEndTime.EditValue = dtEditEndTime.DateTime.AddHours(23).AddMinutes(59)
        End If
    End Sub

    Private Sub acePSFilter_Click(sender As Object, e As EventArgs) Handles acePSFilterStats.Click
        If acePSFilterStats.Expanded Then
            accPeriodSelection.Height = accPeriodSelection.Tag
        Else
            accPeriodSelection.Height = accPeriodSelection.Tag + accPSFilterStats.Height
        End If
    End Sub

    Private Sub dateNavigator_CustomDrawDayNumberCell(sender As Object, e As Calendar.CustomDrawDayNumberCellEventArgs) Handles dateNavigator.CustomDrawDayNumberCell
        Dim isDisabledDate As Boolean = True

        If e.Date >= dtEditStartTime.EditValue And e.Date <= dtEditEndTime.EditValue Then
            If cmbPredefinedFilter.SelectedIndex > 0 Then
                If cmbPredefinedFilter.SelectedItem.ToString = e.Date.DayOfWeek.ToString Then
                    isDisabledDate = False
                ElseIf cmbPredefinedFilter.SelectedItem.ToString = "Weekdays" Then
                    If e.Date.DayOfWeek = DayOfWeek.Saturday Or e.Date.DayOfWeek = DayOfWeek.Sunday Then
                        isDisabledDate = True
                    Else
                        isDisabledDate = False
                    End If
                ElseIf cmbPredefinedFilter.SelectedItem.ToString = "Weekends" Then
                    If e.Date.DayOfWeek <> DayOfWeek.Saturday And e.Date.DayOfWeek <> DayOfWeek.Sunday Then
                        isDisabledDate = True
                    Else
                        isDisabledDate = False
                    End If
                End If
            Else
                isDisabledDate = False
            End If
        End If

        If isDisabledDate Then
            e.State = DevExpress.Utils.Drawing.ObjectState.Disabled
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.Gray, New Point(e.ContentBounds.Location.X, e.ContentBounds.Location.Y))
            e.Handled = True
        Else
            e.Handled = False
        End If

        If e.Disabled = False And e.Holiday = False And e.Inactive = False And e.Selected = False And e.Today = False Then
            clr = e.Style.ForeColor
        End If
        If e.Holiday = True And e.Disabled = False And e.Inactive = False And clr <> Color.Empty Then
            e.Style.ForeColor = clr
        End If
    End Sub

    'Private Function ValidateControlsForKPIConfig() As Boolean
    '    If (cmbObjectType.SelectedIndex > 0) Then
    '        If (Not txtKPIName.Text.Trim.Length = 0) Then
    '            If Not (txtKPIFormula.Text = "") Then
    '                Return True
    '            Else
    '                SetMessage("Enter Any Formula.")
    '                Return False
    '            End If
    '        Else
    '            SetMessage("Enter any KPI Name.")
    '            Return False
    '        End If
    '    Else
    '        SetMessage("Select Object Name.")
    '        Return False
    '    End If
    'End Function

    'Private Function GetSourceTable() As String
    '    Dim from_fieldTemp As String = ""
    '    Dim stList() As String = list_of_used_tables.ToArray()
    '    Dim indexST As Integer = 0
    '    Dim stFirst As String = ""
    '    Dim pkFirst As String = ""

    '    Dim selectCMD As String = SQLTechnologyMeasurements.GetPrimaryKey(String.Join(",", list_of_used_tables), "")
    '    Dim measurementPrimaryKeyDt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)
    '    Dim isFirstTime As Boolean = True
    '    If (measurementPrimaryKeyDt.IsValid) Then

    '        Dim stCurrent As String = ""
    '        Dim pkCurrent As String = ""

    '        from_fieldTemp = String.Empty
    '        For Each stDR As DataRow In measurementPrimaryKeyDt.Rows
    '            If (isFirstTime) Then
    '                stFirst = stDR(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
    '                pkFirst = stDR(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
    '                ''from_fieldTemp = stFirst
    '            Else
    '                stCurrent = stDR(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
    '                pkCurrent = stDR(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
    '            End If
    '            Dim pkCounter As Integer = pkFirst.Split(",").Count '' If PrimaryKey has more then one 
    '            If (Not isFirstTime) Then
    '                If (pkCounter = 1) Then
    '                    from_fieldTemp = " INNER JOIN " & stCurrent & " ON " & stFirst & "." & pkFirst & " = " & stCurrent & "." & pkCurrent
    '                Else
    '                    from_fieldTemp = from_fieldTemp & " INNER JOIN " & stCurrent & " ON " & stFirst & "." & pkFirst.Split(",")(0).ToString & " = " & stCurrent & "." & pkCurrent.Split(",")(0).ToString
    '                End If
    '                For index = 1 To pkCurrent.Split(",").Count - 1
    '                    If (index > pkCounter) Then
    '                        from_fieldTemp = from_fieldTemp & " AND " & stFirst & "." & pkFirst.Split(",")(pkCounter).ToString & " = " & stCurrent & "." & pkCurrent.Split(",")(index).ToString
    '                    Else
    '                        from_fieldTemp = from_fieldTemp & " AND " & stFirst & "." & pkFirst.Split(",")(index).ToString.Trim & " = " & stCurrent & "." & pkCurrent.Split(",")(index).ToString.Trim
    '                    End If
    '                Next
    '            End If
    '            isFirstTime = False
    '        Next
    '    End If
    '    Return from_fieldTemp
    'End Function

    'Private Function TestKPI() As Boolean
    '    Dim testStr As String = ""
    '    Dim kpiName As String = txtKPIName.Text.Trim()
    '    Dim kpiFarmula As String = txtKPIFormula.Text.Trim()

    '    Dim tableNames As String = String.Join(",", list_of_used_tables.ToArray) '' GetUsingAllTableNames(tableKey, connectionName, dataBaseName, tableAlias, "ByKPITest", JoinObject, megaQuery)
    '    Dim selectCMD As String = SQLTechnologyMeasurements.GetPrimaryKey(String.Join(",", list_of_used_tables), "")
    '    Dim measurementPrimaryKeyDt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)
    '    Dim stFirst As String = measurementPrimaryKeyDt(0)(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
    '    Dim pkFirst As String = measurementPrimaryKeyDt(0)(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
    '    Dim pkfields As String = stFirst + "." + Replace(pkFirst, ",", ", " + stFirst + ".")
    '    Try
    '        If (IsKPIFormulaValid(kpiFarmula)) Then
    '            If (tableNames.Trim.Length > 1) Then
    '                Dim tableCount As Integer = tableNames.Split(",").Count
    '                Dim kpiValue As Integer = 0
    '                If (tableCount > 1) Then
    '                    'testStr = GetSourceTable()
    '                    testStr = "SELECT TOP 1 " + pkfields + ", ISNULL(" & kpiFarmula & ",0) [" & kpiName & "] FROM " & tableNames.Split(",")(0) & " " & GetSourceTable() & " GROUP BY " + pkfields
    '                Else
    '                    testStr = "SELECT TOP 1 " + pkfields + ", ISNULL(" & kpiFarmula & ",0) [" & kpiName & "] FROM " & tableNames & " GROUP BY " + pkfields
    '                End If

    '                Dim counterFilters As String = TechnologyPackageCountersFields.SQL_SOURCE_TABLE & OperatorConst.Equal & tableNames.Split(",")(0)
    '                Dim dtSourceTableCon As DataTable = dt_TechPackCounter.Select("SQL_SourceTable='" + tableNames.Split(",")(0) + "'").CopyToDataTable()

    '                If (dtSourceTableCon.IsValid) Then
    '                    Dim result As DataTable = DataAccessorODBC.GetDataTable(dtSourceTableCon.Rows(0)(TechnologyPackageCountersFields.SQL_CONNSTRING).ToString, testStr)
    '                    If (result IsNot Nothing AndAlso result.Rows.Count > 0) Then
    '                        lblKPIConfigStatus.Text = "KPI OK"
    '                        Return True
    '                    ElseIf (result Is Nothing) Then
    '                        lblKPIConfigStatus.Text = "KPI OK"
    '                        Return False
    '                    Else
    '                        lblKPIConfigStatus.Text = "KPI OK"
    '                        Return True
    '                    End If
    '                Else
    '                    SetMessage("Test Connection Not found.")
    '                    Return False
    '                End If
    '            Else
    '                SetMessage("Table is not in Table Grid so Not able to find connection string.")
    '                Return False
    '            End If
    '        End If
    '        Return Nothing
    '    Catch ex As Exception
    '        lblKPIConfigStatus.Text = "KPI Not OK"
    '        XtraMessageBox.Show("There is some problem with query. Error: " & ex.Message)
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "ERROR", testStr & " " & ex.Message)
    '    End Try
    '    Return Nothing
    'End Function

    'Private Function IsKPIFormulaValid(ByVal kpiFormula As String) As Boolean
    '    Dim kpiTestStr As String = kpiFormula
    '    Dim bracesCounter As Integer = kpiFormula.Length - kpiFormula.Replace("(", "").Length
    '    Dim isMatchAggregate As Boolean = False
    '    kpiFormula = Replace(kpiFormula.ToLower, "round", "")
    '    If (kpiFormula.IndexOf("(") > 0) Then

    '        For i As Integer = 0 To lstAggregateFunction.ItemCount - 1
    '            If kpiFormula.ToUpper.Contains(lstAggregateFunction.Items.Item(i).ToString.ToUpper().Replace("()", "(")) Then
    '                isMatchAggregate = True
    '                Exit For
    '            End If
    '        Next

    '        If (isMatchAggregate) Then
    '            If Not (kpiFormula.Length - kpiFormula.Replace("(", "").Length = kpiFormula.Length - kpiFormula.Replace(")", "").Length) Then
    '                XtraMessageBox.Show("Query does not have matching brackets.")
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Else
    '            XtraMessageBox.Show("Query does not seem to start with an aggregate function")
    '            Return False
    '        End If
    '    Else

    '        For i As Integer = 0 To lstAggregateFunction.ItemCount - 1
    '            If kpiFormula.ToUpper.Contains(Replace(lstAggregateFunction.Items.Item(i).ToString.ToUpper, "()", "")) Then
    '                isMatchAggregate = True
    '                Exit For
    '            End If
    '        Next

    '        If (isMatchAggregate) Then
    '            Return True
    '        Else
    '            XtraMessageBox.Show("Query does not seem to start with an aggregate function")
    '            Return False
    '        End If

    '    End If
    '    Return True
    'End Function

    'Private Sub btnTestKPI_Click(sender As Object, e As EventArgs) Handles btnTestKPI.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Application.DoEvents()
    '    Try
    '        Dim kpiFormula As String = txtKPIFormula.Text.Trim()
    '        If (Not String.IsNullOrEmpty(kpiFormula)) Then
    '            If IsNumeric(kpiFormula) Then
    '                lblKPIConfigStatus.Text = "Test successfully."
    '                Exit Sub
    '            End If
    '        End If

    '        If (ValidateControlsForKPIConfig()) Then
    '            If (TestKPI()) Then
    '                XtraMessageBox.Show("KPI executed successfully")
    '            Else
    '                XtraMessageBox.Show("KPI not executed successfully.")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        XtraMessageBox.Show("There is some problem with query. Error: " & ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '        Application.DoEvents()
    '    End Try
    'End Sub

    'Private Sub btnCommitKPI_Click(sender As Object, e As EventArgs) Handles btnCommitKPI.Click
    '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
    '    Try
    '        Dim isPrivate As Boolean = IIf(rbKPIConfigPublic.Checked, False, True)
    '        Dim modifyStatus As Boolean = False

    '        If cmbReportTechnology.SelectedIndex = 0 Then
    '            SetMessage("Please Select Technology")
    '            Exit Sub
    '        Else
    '            If (txtKPIName.Text.Trim = String.Empty) Or (txtKPIFormula.Text.Trim = String.Empty) Then
    '                SetMessage("Either KPI Name Or KPI Formula left empty")
    '                Exit Sub
    '            End If
    '        End If

    '        Dim objKPIModify As New dlgKPIModify(cmbReportTechnology.SelectedItem.ToString)
    '        objKPIModify.Creator = lblKPICreator.Text

    '        Dim dr() As DataRow = dt_TechnologyPackageKPI.Select("KPINAME='" & txtKPIName.Text & "'")
    '        If Not dr Is Nothing AndAlso dr.Count = 0 Then
    '            objKPIModify.kpiModifyOption = KPIModifyOption.Add
    '        Else
    '            SetMessage("KPI Name already exists in Technology Package...Rename KPI Name in Text Box")
    '            'Open dialog to confirm whether new KPI is going to be added or need to modify existing KPI.
    '            objKPIModify.ShowDialog()
    '        End If

    '        Me.UseWaitCursor = True
    '        Application.DoEvents()

    '        If objKPIModify.kpiModifyOption = KPIModifyOption.Add Then
    '            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLTechnologyKPIs.Insert(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, txtKPIName.Text, txtKPIDescription.Text, txtKPIFormula.Text, isPrivate))
    '            SetMessage("KPI Successfully Added.")
    '            modifyStatus = True
    '        ElseIf objKPIModify.kpiModifyOption = KPIModifyOption.Update Then
    '            If (lblKPICreator.Text.ToUpper = Environment.UserName.ToUpper) Then
    '                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLTechnologyKPIs.Update(lstTechKPI.FocusedNode.Tag, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, txtKPIName.Text, txtKPIDescription.Text, txtKPIFormula.Text, isPrivate))
    '                SetMessage("KPI Successfully Updated.")
    '                modifyStatus = True

    '                Try
    '                    'updating vsandboxfield if available
    '                    Dim vSandBoxFieldModel As New EntityModel.SandBoxFieldModel()
    '                    Dim vSandBoxElement As DevExSandBoxField = New DevExSandBoxField()

    '                    For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
    '                        vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
    '                        If vSandBoxElement.SQL_KPI_ID = lstTechKPI.FocusedNode.Tag Then
    '                            vSandBoxElement.SQL_KPIFormula = txtKPIFormula.Text
    '                        End If
    '                    Next
    '                Catch ex As Exception
    '                End Try
    '            Else
    '                SetMessage("Only KPI creator can modify.")
    '            End If
    '        End If
    '        If (modifyStatus = True) Then
    '            RefreshKPITree()
    '        End If
    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '    Finally
    '        Me.UseWaitCursor = False
    '        Application.DoEvents()
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    '    End Try
    'End Sub

    Private Sub SandboxTextBox_KeyPress_OnlyNumeric(sender As Object, e As KeyPressEventArgs) Handles txtQueryTimeOut.KeyPress, txtSandBoxTopX.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

#Region "Searching"

    Private Sub txtSearchMeasurement_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMeasurement.TextChanged
        Try
            lstTechMeasurement.SuspendLayout()
            Dim dtFilter As DataTable = New DataTable
            Dim totalFiler As String = ""
            Dim counterFilters As String = "" ''= TechnologyPackageCountersFields.COUNTER_ID & OpratorConst.Equel & selectedItem.Value
            Dim kpiFilterGet As String = ""
            If (checkedCounter.Count > 0) Then
                For Each checkedId As String In checkedCounter
                    counterFilters = counterFilters & TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & checkedId & AggregateConst.OR_Only
                Next
            End If

            If (Not String.IsNullOrEmpty(counterFilters)) Then
                counterFilters = counterFilters.Remove(counterFilters.Length - 4, 4)
            End If


            If (checkedKPI.Count > 0) Then
                For Each checkedId As String In checkedKPI
                    kpiFilterGet = kpiFilterGet & TechnologyPackageKPIFields.KPI_ID & OperatorConst.Equal & checkedId & AggregateConst.OR_Only
                Next
            End If
            If (Not String.IsNullOrEmpty(kpiFilterGet)) Then
                kpiFilterGet = kpiFilterGet.Remove(kpiFilterGet.Length - 4, 4)
            End If



            If (Not String.IsNullOrEmpty(counterFilters)) Then
                If (Not String.IsNullOrEmpty(kpiFilterGet)) Then
                    totalFiler = counterFilters & AggregateConst.OR_Only & kpiFilterGet
                Else
                    totalFiler = counterFilters
                End If
            Else
                If (Not String.IsNullOrEmpty(kpiFilterGet)) Then
                    totalFiler = kpiFilterGet
                End If
            End If


            If (Not String.IsNullOrEmpty(totalFiler)) Then
                dtFilter = dt_TechPackCounter.SelectedRowsAsTable(counterFilters)
            Else
                dtFilter = dt_TechPackCounter
            End If

            If (txtSearchMeasurement.Text.Trim.Length > 2) Then
                Dim dv As New DataView(dtFilter, TechnologyPackageCountersFields.MEASUREMENT_NAME & " LIKE '%" & txtSearchMeasurement.Text.Trim & "%'", "", DataViewRowState.CurrentRows)
                BindMeasurmentLST(dv.ToTable)
            Else
                BindMeasurmentLST(dtFilter)
            End If
            lstTechMeasurement.Refresh()
            lstTechMeasurement.ResumeLayout()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub txtSearchCounter_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCounter.TextChanged
        Try
            If (txtSearchCounter.Text.Trim.Length < 3) And lstTechCounter.Items.Count = dt_TechPackCounter.Rows.Count Then
                Exit Sub
            End If
            lstTechCounter.SuspendLayout()
            Dim appliedFilter As String = String.Empty
            If (checkedMeasurements.Count > 0) Then
                For Each checkedId As String In checkedMeasurements
                    appliedFilter = appliedFilter & TechnologyPackageCountersFields.MEASUREMENT_ID & OperatorConst.Equal & checkedId & AggregateConst.OR_Only
                Next
            End If

            Dim dtFilter As DataTable = New DataTable
            If (Not String.IsNullOrEmpty(appliedFilter)) Then
                appliedFilter = appliedFilter.Remove(appliedFilter.Length - 4, 4)
                dtFilter = dt_TechPackCounter.SelectedRowsAsTable(appliedFilter)
            Else
                dtFilter = dt_TechPackCounter
            End If

            If (txtSearchCounter.Text.Trim.Length > 2) Then
                Dim dv As New DataView(dtFilter, TechnologyPackageCountersFields.COUNTER_NAME & " LIKE '%" & txtSearchCounter.Text.Trim & "%'", "", DataViewRowState.CurrentRows)
                BindCounterLST(dv.ToTable)
            Else
                BindCounterLST(dtFilter)
            End If

            lstTechCounter.Refresh()

            lstTechCounter.ResumeLayout()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

#End Region

#Region "Message Label"

    Public Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Public Sub ClearMessage()
        lblMessage.Visible = False
        lblMessage.Text = String.Empty
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

#End Region

    Private Sub cmbObjectType_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cmbObjectType.SelectedValueChanged
        ''RemoveHandler cmbCMPM.SelectedIndexChanged, AddressOf cmbCMPM_SelectedIndexChanged
        ''Dim dsTreeVendor As DataSet = Nothing
        ''Try
        ''    tvObjects.SuspendLayout()
        ''    tvObjects.Nodes.Clear()
        ''    tvObjects.Refresh()
        ''    tvObjects.ResumeLayout()
        ''    Dim mapField As String = Nothing
        ''    Console.WriteLine(cmbObjectType.Text & "-" & cmbObjectType.SelectedIndex.ToString)
        ''    If (cmbObjectType.SelectedIndex > 0) Then
        ''        Me.UseWaitCursor = True
        ''        Application.DoEvents()
        ''        If (TryCast(cmbCMPM.SelectedItem, clsComboBoxItem).Value = 0) Then
        ''            For j = cmbObjectType.Properties.Items.Count - 1 To cmbObjectType.SelectedIndex Step -1
        ''                Dim item As clsComboBoxItem = cmbObjectType.Properties.Items(j)
        ''                If item.Text.ToUpper <> "NONE" Then
        ''                    Dim dsReportGroup As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLTechnologyObjectTypes.GetObjectSQLForCMTree(item.Value))
        ''                    If (dsReportGroup.Tables(0).IsValid) Then
        ''                        Dim noOfRows As Integer = dsReportGroup.Tables(0).Rows.Count
        ''                        Dim reportGroup_con As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_CM_CONNSTR).ToString
        ''                        Dim reportGroup_SQL As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_CM_TREE).ToString
        ''                        mapField = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.MAP_FIELD).ToString
        ''                        If (reportGroup_con IsNot Nothing And reportGroup_SQL IsNot Nothing) Then
        ''                            Dim dtReportGroupSQL As DataTable = DataAccessorODBC.GetDataTable(reportGroup_con, reportGroup_SQL)
        ''                            If (dtReportGroupSQL.IsValid) Then
        ''                                dtReportGroupSQL.TableName = "ObjectTree"
        ''                                If dsTreeVendor Is Nothing Then
        ''                                    dsTreeVendor = dtReportGroupSQL.DataSet
        ''                                Else
        ''                                    dsTreeVendor.Merge(dtReportGroupSQL)
        ''                                End If
        ''                            End If
        ''                        End If
        ''                    End If
        ''                End If
        ''            Next
        ''        ElseIf (TryCast(cmbCMPM.SelectedItem, clsComboBoxItem).Value = 1) Then
        ''            For j = cmbObjectType.Properties.Items.Count - 1 To cmbObjectType.SelectedIndex Step -1
        ''                Dim item As clsComboBoxItem = cmbObjectType.Properties.Items(j)
        ''                If item.Text.ToUpper <> "NONE" Then
        ''                    Dim dsReportGroup As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLTechnologyObjectTypes.GetObjectSQLForPMTree(item.Value, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value))
        ''                    If (dsReportGroup.Tables(0).IsValid) Then
        ''                        Dim noOfRows As Integer = dsReportGroup.Tables(0).Rows.Count
        ''                        Dim reportGroup_con As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_PM_CONNSTR).ToString
        ''                        Dim reportGroup_SQL As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_PM_TREE).ToString
        ''                        If (reportGroup_con IsNot Nothing And reportGroup_SQL IsNot Nothing) Then

        ''                            Dim dtReportGroupSQL As DataTable = DataAccessorODBC.GetDataTable(reportGroup_con, reportGroup_SQL)
        ''                            If (dtReportGroupSQL.IsValid) Then
        ''                                dtReportGroupSQL.TableName = "ObjectTree"
        ''                                If dsTreeVendor Is Nothing Then
        ''                                    dsTreeVendor = dtReportGroupSQL.DataSet
        ''                                Else
        ''                                    dsTreeVendor.Merge(dtReportGroupSQL)
        ''                                End If
        ''                            End If

        ''                        End If
        ''                    End If
        ''                End If
        ''            Next
        ''        End If
        ''        Me.UseWaitCursor = False
        ''        Application.DoEvents()
        ''    End If
        ''    RemoveHandler tvObjects.NodeChanged, AddressOf tvObjects_NodeChanged
        ''    If dsTreeVendor IsNot Nothing Then
        ''        FillTreeList(cmbObjectType.SelectedItem.ToString, "PLMN", dsTreeVendor, mapField)
        ''    End If

        ''    FillDimensions(cmbCMPM.SelectedItem.ToString, cmbObjectType.Text)

        ''    AddHandler tvObjects.NodeChanged, AddressOf tvObjects_NodeChanged
        ''    AddHandler cmbCMPM.SelectedIndexChanged, AddressOf cmbCMPM_SelectedIndexChanged
        ''Catch ex As Exception
        ''    Me.UseWaitCursor = False
        ''    Application.DoEvents()
        ''    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
        ''End Try
    End Sub

    Sub FillTreeList(objectType As String, rNode As String, ByRef ds As DataSet, ByVal mapField As String)
        Try
            tvObjects.BeginUnboundLoad()
            tvObjects.Tag = mapField
            tvObjects.PupulateTreeListColumn({"ObjectName", "ObjectID"})

            tvObjects.Nodes.Clear()
            Dim tlNode As TreeListNode = tvObjects.Nodes.Add(New Object() {rNode, "0", "EMPTY"})
            tlNode.Tag = rNode
            If ds IsNot Nothing Then
                PopulateTreeList(rNode, tlNode, ds, "0", objectType)
            End If
        Catch ex As Exception
        Finally
            tvObjects.EndUnboundLoad()
            If tvObjects.Nodes.Count > 0 Then
                tvObjects.SelectNode(tvObjects.Nodes(0))
                tvObjects.SetFocusedNode(tvObjects.Nodes(0))
                tvObjects.CollapseAll()
                tvObjects.ExpandToLevel(0)
            End If
        End Try
    End Sub

    Sub FillDimensions(ByVal treeSource As String, ByVal objectType As String)
        lbDimensions.Items.Clear()
        lbDimensions.DataSource = Nothing

        Dim dtDimensions As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLTechnologyObjectTypes.GetDimensionsForSource(treeSource, objectType))
        Dim sqlDBName As String = dt_TechPackCounter.Select("TechnologyPackageID=" & CInt(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value))(0)("SQL_DatabaseName")

        If dtDimensions.IsValid() Then
            lbDimensions.DisplayMember = "COLUMN_NAME"
            lbDimensions.ValueMember = "TABLE_NAME"
            lbDimensions.Tag = sqlDBName
            lbDimensions.DataSource = dtDimensions
        End If
    End Sub

    Sub PopulateTreeList(ParentID As String, rNode As TreeListNode, ds As DataSet, tblname As String, ByVal objectType As String)
        Dim foundRows() As DataRow = Nothing
        If tblname = "0" Then
            foundRows = ds.Tables(0).Select("ParentID = " & Chr(39) & ParentID & Chr(39))
        Else
            If ds.Tables.Contains(tblname) = False Then Exit Sub
            foundRows = ds.Tables(tblname).Select("ParentID = " & Chr(39) & ParentID & Chr(39))
        End If

        'Dim dsObjectTree As New DataSet
        If foundRows.Length > 0 Then
            For Each row As DataRow In foundRows
                If row.Item("ObjectID").ToString <> "" Then
                    Dim parentnode As TreeListNode = tvObjects.AppendNode(New Object() {row.Item("ObjectName"), row.Item("ObjectID")}, rNode)
                    parentnode.Tag = row.Item("ObjectID").ToString.Trim
                    PopulateTreeList(row.Item(2), parentnode, ds, tblname, objectType)
                End If
            Next row
        End If
    End Sub

    Private Sub tvObjects_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        RemoveHandler tvObjects.NodeChanged, AddressOf tvObjects_NodeChanged
        If e.ChangeType = NodeChangeTypeEnum.CheckedState Then
            If e.Node.CheckState = CheckState.Checked Then
                e.Node.CheckAll()
            Else
                e.Node.UncheckAll()
            End If
            tvObjects.CheckParentNode(e.Node)
        End If
        AddHandler tvObjects.NodeChanged, AddressOf tvObjects_NodeChanged
    End Sub

    Private Sub cmbCMPM_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            tvObjects.Columns.Clear()
            tvObjects.Nodes.Clear()

            If (dt_TechnologyPackageObjects.IsValid) Then
                Dim objectSourceFilter As String = TechnologyPackageObjectsFields.SOURCE_OBJECT_ID & OperatorConst.Equal & TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value
                Dim dt As DataTable = dt_TechnologyPackageObjects.Select(objectSourceFilter).CopyToDataTable
                If dt(0)("DataIsPresent_Sum") > 0 Then
                    Dim ObjectTypeID As String = dt(0)("ObjectTypeID").ToString
                    Dim ObjectTypeParentID As String = dt(0)("ObjectTypeParentID").ToString

                    While ObjectTypeID <> "1"
                        Dim dt_temp As DataTable = dt_TechnologyPackageObjects.Select("ObjectTypeID='" + ObjectTypeParentID + "'").CopyToDataTable
                        ObjectTypeID = dt_temp(0)("ObjectTypeID").ToString
                        ObjectTypeParentID = dt_temp(0)("ObjectTypeParentID").ToString
                        dt.Merge(dt_temp)
                    End While

                    BindObjectType(dt)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnLoadTreeSource_Click(sender As Object, e As EventArgs) Handles btnLoadTreeSource.Click
        RemoveHandler cmbCMPM.SelectedIndexChanged, AddressOf cmbCMPM_SelectedIndexChanged
        Dim dsTreeVendor As DataSet = Nothing
        Try
            tvObjects.SuspendLayout()
            tvObjects.Nodes.Clear()
            Dim mapField As String = Nothing
            Console.WriteLine(cmbObjectType.Text & "-" & cmbObjectType.SelectedIndex.ToString)
            If (cmbObjectType.SelectedIndex > 0) Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                If (TryCast(cmbCMPM.SelectedItem, clsComboBoxItem).Value = 0) Then
                    For j = cmbObjectType.Properties.Items.Count - 1 To cmbObjectType.SelectedIndex Step -1
                        Dim item As clsComboBoxItem = cmbObjectType.Properties.Items(j)
                        If item.Text.ToUpper <> "NONE" Then
                            Dim dsReportGroup As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLTechnologyObjectTypes.GetObjectSQLForCMTree(item.Value))
                            If (dsReportGroup.Tables(0).IsValid) Then
                                Dim noOfRows As Integer = dsReportGroup.Tables(0).Rows.Count
                                Dim reportGroup_con As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_CM_CONNSTR).ToString
                                Dim reportGroup_SQL As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_CM_TREE).ToString
                                mapField = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.MAP_FIELD).ToString
                                If (reportGroup_con IsNot Nothing And reportGroup_SQL IsNot Nothing) Then
                                    Dim dtReportGroupSQL As DataTable = DataAccessorODBC.GetDataTable(reportGroup_con, reportGroup_SQL)
                                    If (dtReportGroupSQL.IsValid) Then
                                        dtReportGroupSQL.TableName = "ObjectTree"
                                        If dsTreeVendor Is Nothing Then
                                            dsTreeVendor = dtReportGroupSQL.DataSet
                                        Else
                                            dsTreeVendor.Merge(dtReportGroupSQL)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                ElseIf (TryCast(cmbCMPM.SelectedItem, clsComboBoxItem).Value = 1) Then
                    For j = cmbObjectType.Properties.Items.Count - 1 To cmbObjectType.SelectedIndex Step -1
                        Dim item As clsComboBoxItem = cmbObjectType.Properties.Items(j)
                        If item.Text.ToUpper <> "NONE" Then
                            Dim dsReportGroup As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLTechnologyObjectTypes.GetObjectSQLForPMTree(item.Value, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value))
                            If (dsReportGroup.Tables(0).IsValid) Then
                                Dim noOfRows As Integer = dsReportGroup.Tables(0).Rows.Count
                                Dim reportGroup_con As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_PM_CONNSTR).ToString
                                Dim reportGroup_SQL As String = dsReportGroup.Tables(0).Rows(noOfRows - 1)(TechnologyObjectTypesFields.OBJECT_SQLFOR_PM_TREE).ToString
                                If (reportGroup_con IsNot Nothing And reportGroup_SQL IsNot Nothing) Then

                                    Dim dtReportGroupSQL As DataTable = DataAccessorODBC.GetDataTable(reportGroup_con, reportGroup_SQL)
                                    If (dtReportGroupSQL.IsValid) Then
                                        dtReportGroupSQL.TableName = "ObjectTree"
                                        If dsTreeVendor Is Nothing Then
                                            dsTreeVendor = dtReportGroupSQL.DataSet
                                        Else
                                            dsTreeVendor.Merge(dtReportGroupSQL)
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    Next
                End If
                'tvObjects.Cursor = Cursors.Default
                'Application.DoEvents()
            End If
            RemoveHandler tvObjects.NodeChanged, AddressOf tvObjects_NodeChanged
            If dsTreeVendor IsNot Nothing Or cmbObjectType.SelectedItem.ToString = "PLMN" Then
                'Try
                '    Dim dtTreeNode3 As DataTable = dsTreeVendor.Tables(0).Select("ObjectID Like '%" & cmbObjectType.SelectedItem.ToString & "%'").CopyToDataTable()
                '    Dim dtTreeNode2 As DataTable = dtTreeNode3.Clone()
                '    Dim dtTreeNode1 As DataTable = dtTreeNode3.Clone()
                '    Dim foundRow As DataRow() = Nothing

                '    For Each dr As DataRow In dtTreeNode3.Rows
                '        foundRow = dsTreeVendor.Tables(0).Select("ObjectID = '" & dr("ParentID") & "'")
                '        If foundRow.Length > 0 Then
                '            Dim drParentNode As DataRow = foundRow(0)
                '            dtTreeNode2.ImportRow(drParentNode)
                '            dtTreeNode2.AcceptChanges()
                '        End If
                '    Next

                '    For Each dr As DataRow In dtTreeNode2.Rows
                '        foundRow = dsTreeVendor.Tables(0).Select("ObjectID = '" & dr("ParentID") & "'")
                '        If foundRow.Length > 0 Then
                '            Dim drParentNode As DataRow = foundRow(0)
                '            dtTreeNode1.ImportRow(drParentNode)
                '            dtTreeNode1.AcceptChanges()
                '        End If
                '    Next

                '    dtTreeNode3.Merge(dtTreeNode2)
                '    dtTreeNode3.Merge(dtTreeNode1)
                '    dsTreeVendor = New DataSet
                '    dsTreeVendor.Tables.Add(dtTreeNode3)
                'Catch
                'End Try
                FillTreeList(cmbObjectType.SelectedItem.ToString, "PLMN", dsTreeVendor, mapField)
            Else
                SetMessage("Object Type/Tree source combination is not configured")
            End If

            If cmbObjectType.Properties.Items.Count > 0 Then
                FillDimensions(cmbCMPM.SelectedItem.ToString, cmbObjectType.Properties.Items(1).Text)
            End If

            AddHandler tvObjects.NodeChanged, AddressOf tvObjects_NodeChanged
            AddHandler cmbCMPM.SelectedIndexChanged, AddressOf cmbCMPM_SelectedIndexChanged

            'select chart objects in the objects tree
            Dim reportID As Integer = CInt(tvReportGroup.FocusedNode.Tag.ToString)
            Dim dtObjectID As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContentObjects.GetReportChartObjects(reportID))
            ObjectTreeChecked(dtObjectID)

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tvObjects.Refresh()
            tvObjects.ResumeLayout()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub TreeView_KPI_AfterCheck(ByVal nd As TreeNode)
        Try
            SandBoxTreeView.TreeView_AfterCheck(nd)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnAddReportGroup_Click(sender As Object, e As EventArgs) Handles btnAddReportGroup.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim frmGroupInsert As New dlgSBGroupInsert()
            frmGroupInsert.SetConnectionString(connStrSandBoxServer)
            frmGroupInsert.GroupTypeInserting = GroupType.SandboxGroup
            frmGroupInsert.ShowDialog()
            Dim newGroupName As String = frmGroupInsert.NewGroup
            Dim RetrunData As Boolean = frmGroupInsert.IsGroupPrivate
            If (newGroupName IsNot Nothing) Then
                If (newGroupName IsNot Nothing) Then
                    BindReportGroup()
                    Dim cmbItem As clsComboBoxItem = GetComboItemFromText(newGroupName, cmbReportGroup)
                    ''vcmb_ReportGroup.Properties.Items.ToList().FindIndex(Function(c) c.ToString() = newGroupName)
                    cmbReportGroup.SelectedItem = cmbItem
                    RefreshDashboardReport()
                    RefreshSchedulerReport()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub cmbReportGroup_Properties_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles cmbReportGroup.Properties.ButtonClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (e.Button.Tag IsNot Nothing) AndAlso (e.Button.Tag.ToString.ToUpper = "REFRESH") Then
                BindReportGroup()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Public Sub BindReportGroup()
        Dim initializeDT As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportGroups.GetReportGroups(Environment.UserName.ToString))
        If (initializeDT.IsValid) Then
            BindDevExComboBoxWithTagMember(cmbReportGroup, initializeDT, ReportGroupsFields.REPORT_GROUP_ID, ReportGroupsFields.REPORT_GROUP_NAME, "None", ReportGroupsFields.LICENSE_USER)
        Else
            ClearComboBox(cmbReportGroup, "None")
        End If
    End Sub

    Private Sub cmbReportGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReportGroup.SelectedIndexChanged
        Try
            'RemoveHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
            If (isReportGroupSelectedIndexChanged) Then
                flp_ValueX.Controls.Clear()
                flp_ValueY.Controls.Clear()
                tlvSandboxChartsSeries.Nodes.Clear()
                If (cmbReportGroup.SelectedIndex > 0) Then
                    Me.UseWaitCursor = True
                    RefreshReportGroup_TreeList()
                    btnAddReport.Enabled = True
                    Me.UseWaitCursor = False
                Else
                    tvReportGroup.Nodes.Clear()
                    btnAddReport.Enabled = False
                End If
            End If
            flp_ValueX.Controls.Clear()
            flp_ValueY.Controls.Clear()
            lbDimensions.DataSource = Nothing
            If (reportChartGrid_SendBox IsNot Nothing) Then
                reportChartGrid_SendBox.ClearData()
            End If
            'AddHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
        Catch ex As Exception
            Me.UseWaitCursor = False
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub DragReportToCategory(ByVal reportID As Integer, ByVal reportCategoryID As Integer)
        Dim iQuery As Integer = DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportGroups.UpdateReportCategory(reportID, reportCategoryID))
    End Sub

    Public Sub RefreshReportGroup_TreeList()
        'Update tlv
        RemoveHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
        tvReportGroup.BeginUnboundLoad()
        If cmbReportGroup.SelectedItem Is Nothing Then
            Exit Sub
        End If
        'Dim sqlCommand As String = New SQLReportTree().SelectAll(False, "( " & ReportTreeFields.REPORT_GROUP_USERS & OperatorConst.Equal & Chr(39) & System.Environment.UserName & Chr(39) & AggregateConst.AND_Only & " ( " & ReportTreeFields.REPORT_GROUP_PRIVATE & OperatorConst.Equal & "0" & AggregateConst.OR_Only & ReportTreeFields.REPORT_GROUP_CREATOR & OperatorConst.Equal & Chr(39) & System.Environment.UserName & "' )) " & AggregateConst.AND_Only & ReportTreeFields.REPORT_GROUP_ID & OperatorConst.Equal & TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value & AggregateConst.AND_Only & JobReportFields.ReportID & " IS NOT NULL")
        Dim sqlCommand As String = New SQLReportTree().SelectAll(False, "( " & ReportTreeFields.REPORT_GROUP_USERS & OperatorConst.Equal & Chr(39) & System.Environment.UserName & Chr(39) & AggregateConst.AND_Only & " ( " & ReportTreeFields.REPORT_GROUP_PRIVATE & OperatorConst.Equal & "0" & AggregateConst.OR_Only & ReportTreeFields.REPORT_GROUP_CREATOR & OperatorConst.Equal & Chr(39) & System.Environment.UserName & "' )) " & AggregateConst.AND_Only & ReportTreeFields.REPORT_GROUP_ID & OperatorConst.Equal & TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value)
        Dim dtQODBC As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCommand)

        Dim colList() As String = {ReportTreeFields.REPORT_GROUP_ID, ReportTreeFields.REPORT_GROUP_NAME,
                                   ReportTreeFields.REPORT_CATEGORY_ID, ReportTreeFields.REPORT_CATEGORY_NAME, ReportTreeFields.REPORT_CATEGORY_ORDINAL,
                                   ReportTreeFields.REPORT_ID, ReportTreeFields.REPORT_NAME, "ReportCreatorName"}

        tvReportGroup.Columns.Clear()
        For i As Integer = 0 To colList.Length - 1
            Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
            col1.Caption = colList(i)
            col1.VisibleIndex = i
            If colList(i) = ReportTreeFields.REPORT_GROUP_NAME Then 'Or colList(i) = "ReportCreatorName" Then
                tvReportGroup.AutoFillColumn = col1
                col1.Visible = True
            Else
                col1.Visible = False
            End If
            tvReportGroup.Columns.Add(col1)
        Next
        tvReportGroup.Nodes.Clear()
        tvReportGroup.OptionsView.AutoWidth = True

        Try
            Dim dbNode As TreeListNode = Nothing
            If (dtQODBC.IsValid) Then

                Dim tlNode As TreeListNode = tvReportGroup.Nodes.Add(New Object() {dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_ID), dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_NAME), 0, "", -1, 0, "", -1})
                Dim groupName As String = dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_NAME).ToString
                Dim groupID As String = dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_ID).ToString

                ToolTipController1.SetToolTip(tvReportGroup, "Group" & "_" & IIf(dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_PRIVATE).ToString.ToUpper = "TRUE", 1, 0))
                tlNode.Tag = groupID
                Dim distinctCol() As String = {ReportTreeFields.REPORT_CATEGORY_NAME, ReportTreeFields.REPORT_GROUP_ORDINAL, ReportTreeFields.REPORT_CATEGORY_ID}
                Dim dtDistinctGroupName As DataTable = dtQODBC.DistinctCol(distinctCol)

                If (dtDistinctGroupName.IsValid) Then
                    Dim nodeIndex As Integer = 0
                    Dim drGroupName As DataRow() = dtDistinctGroupName.Select("", ReportTreeFields.REPORT_GROUP_ORDINAL & " ASC ")
                    For Each rowGroupName As DataRow In drGroupName
                        If (Not IsDBNull(rowGroupName(ReportTreeFields.REPORT_CATEGORY_NAME))) Then
                            dbNode = tvReportGroup.AppendNode(New Object() {rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID), rowGroupName(ReportTreeFields.REPORT_CATEGORY_NAME), rowGroupName(ReportTreeFields.REPORT_GROUP_ORDINAL)}, tlNode)
                            dbNode.Tag = rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID).ToString

                            Dim reportFilter As String = ReportTreeFields.REPORT_CATEGORY_ID & OperatorConst.Equal & rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID)
                            Dim dtDistinctReport As DataTable = dtQODBC.SelectedRowsAsTable(reportFilter)

                            If dtDistinctReport.IsValid Then
                                Dim dr As DataRow() = dtDistinctReport.Select("", ReportTreeFields.REPORT_CATEGORY_ORDINAL & " ASC ")
                                For Each drow As DataRow In dr
                                    Dim rptNode As TreeListNode = tvReportGroup.AppendNode(New Object() {drow.Item(ReportTreeFields.REPORT_ID).ToString, drow.Item(ReportTreeFields.REPORT_NAME).ToString, rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID),
                                                                                           rowGroupName(ReportTreeFields.REPORT_CATEGORY_NAME), rowGroupName(ReportTreeFields.REPORT_GROUP_ORDINAL), drow.Item("ReportCreatorName").ToString}, dbNode)
                                    rptNode.Tag = drow.Item(ReportTreeFields.REPORT_ID).ToString
                                Next
                            End If
                            nodeIndex = nodeIndex + 1
                        End If
                    Next
                End If
            Else
                Dim tlNode As TreeListNode = tvReportGroup.Nodes.Add(New Object() {TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value, cmbReportGroup.SelectedItem.ToString.Trim, 0, "", -1, 0, "", -1})
                tlNode.Tag = TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
            tvReportGroup.EndUnboundLoad()
            If tvReportGroup.Nodes.Count > 0 Then
                tvReportGroup.SelectNode(tvReportGroup.Nodes(0))
                tvReportGroup.SetFocusedNode(tvReportGroup.Nodes(0))
                tvReportGroup.ExpandAll()
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
            AddHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
        End Try
    End Sub

    Private Sub tvReportGroup_MouseUp(sender As Object, e As MouseEventArgs)
        If (Not IsReportGroupMouseDownRight) Then
            If Not tvReportGroup.Selection Is Nothing Then
                Dim treeNode As TreeListNode = tvReportGroup.FocusedNode
                If treeNode IsNot Nothing Then
                    lbDimensions.DataSource = Nothing
                    'Disallow repeatedly report loading
                    If treeNode.GetDisplayText("ReportGroupName").ToLower = lblSelectedReport.Text.ToLower Then
                        Exit Sub
                    End If

                    If (dtChartConfigSandbox IsNot Nothing) Then
                        dtChartConfigSandbox.Clear()
                        dtChartConfigSandbox = Nothing
                    End If

                    Try
                        flp_ValueX.Controls.Clear()
                        flp_ValueY.Controls.Clear()
                        tlvSandboxChartsSeries.Nodes.Clear()
                        reportChartGrid_SendBox.ClearData()

                        Dim tv As TreeList = tvObjects
                        If (treeNode.Level = 0) Then  'Report Group Node
                            lblSelectedReport.Text = String.Empty
                        ElseIf (treeNode.Level = 1) Then  'Category Node
                            lblSelectedReport.Text = String.Empty
                        ElseIf (treeNode.Level = 2) Then  'Report Node
                            cmbReportTechnology.SelectedIndex = 0
                            IOS.Library.ReportChartGrid.reportAbort = False
                            dmWaitScreen.ShowDataMartWaitScreen("Report loading")
                            lblSelectedReport.Text = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
                            lblReportMode.Text = "READ MODE"
                            lblReportMode.ForeColor = Color.DarkBlue
                            btnEdit.Appearance.ForeColor = Color.Maroon
                            btnEdit.Appearance.BackColor = Color.Yellow

                            Dim reportId As String = tvReportGroup.FocusedNode.Tag.ToString
                            GetFieldByReportId(reportId)
                            EnableDisableReportControls(False)

                            If rbExport.Checked = False Then
                                dtChartConfigSandbox = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportChart.GetReportChartData(reportId))
                                Dim dsReportAxisData As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLReportChart.GetReportAxisData(reportId) & SQLReportContentFilter.GetReportContentFilter(reportId))
                                dtReportAxisData = dsReportAxisData.Tables(0)
                                dtReportFilterData = dsReportAxisData.Tables(1)

                                If (dtChartConfigSandbox.IsValid) Then
                                    If (dtReportAxisData.IsValid) Then
                                        BindChart(dtChartConfigSandbox, True)
                                        tsmi_DashboardReportHideAndShowTitle_Click(cms_ReportChartGrid.SourceControl, Nothing)
                                    End If
                                    If dtChartConfigSandbox.Rows(0)("CompareTime").ToString.Trim = 0 Then
                                        ceAlignIntervalAll.Checked = False
                                        'ceAlignIntervalMatch.Checked = False
                                    ElseIf dtChartConfigSandbox.Rows(0)("CompareTime").ToString.Trim = 1 Then
                                        ceAlignIntervalAll.Checked = True
                                        'ceAlignIntervalMatch.Checked = False
                                    ElseIf dtChartConfigSandbox.Rows(0)("CompareTime").ToString.Trim = 2 Then
                                        ceAlignIntervalAll.Checked = False
                                        'ceAlignIntervalMatch.Checked = True
                                    End If
                                End If

                                Dim sqlCommand As String = SQLReportContent.GetReport(reportId)
                                Dim dtReportObjects As DataTable = DataAccessorODBC.GetDataSet(connStrSandBoxServer, sqlCommand).Tables(6)
                                If dtReportObjects.IsValid Then
                                    If (dtReportObjects(0)("ReportSQL").ToString <> "") Then
                                        ' disabling report edit controls (read only report)
                                        grpCountersAndKPI.Enabled = False
                                    Else
                                        lblReportMode.Text = "EDIT MODE"
                                        lblReportMode.ForeColor = Color.Red
                                        'btnEdit.Appearance.ForeColor = Color.DarkRed
                                        ' enabling report edit controls (empty report)
                                        grpCountersAndKPI.Enabled = True
                                        EnableDisableReportControls(True)
                                    End If
                                End If
                            End If
                        Else
                            lblSelectedReport.Text = String.Empty
                        End If
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                    Finally
                        dmWaitScreen.CloseDataMartWaitScreen()
                    End Try

                    tvObjects.Nodes.Clear()
                    tvObjects.Refresh()

                End If
            End If
        End If
        Me.UseWaitCursor = False
    End Sub

    Private Sub EnableDisableReportControls(ByVal enable As Boolean)
        ' enable/diable controls as report loads in read/edit mode
        flp_ValueX.Enabled = enable
        flp_ValueY.Enabled = enable
        btnTime.Enabled = enable
        btnFilter.Enabled = enable
        txtSandBoxTopX.Enabled = enable
        btnCommit.Enabled = enable
        btnCommitAs.Enabled = enable
        xtcSandboxChartConfigure.Enabled = enable
        rbChart.Enabled = enable
        rbGrid.Enabled = enable
        rbExport.Enabled = enable
    End Sub

    Private Sub tvReportGroup_EditorKeyDown(sender As Object, e As KeyEventArgs) Handles tvReportGroup.EditorKeyDown
        If ((e.KeyCode = Keys.Enter) Or (e.KeyCode = Keys.Escape)) Then
            tvReportGroup.OptionsBehavior.Editable = False
            tvReportGroup.OptionsBehavior.ReadOnly = True
        End If
    End Sub

    Private Sub tvReportGroup_MouseDown(sender As Object, e As MouseEventArgs) Handles tvReportGroup.MouseDown
        If e.Button = MouseButtons.Right Then
            IsReportGroupMouseDownRight = True

            Dim treeList As TreeList = TryCast(sender, TreeList)
            Dim info As TreeListHitInfo = treeList.CalcHitInfo(e.Location)
            If info.Node IsNot Nothing Then
                If info.Node.Level = 0 Then
                    Me.SanboxReportTreeSelectionType = ReportSelectionType.Group
                ElseIf info.Node.Level = 1 Then
                    Me.SanboxReportTreeSelectionType = ReportSelectionType.Category
                ElseIf info.Node.Level = 2 Then
                    Me.SanboxReportTreeSelectionType = ReportSelectionType.Report
                End If
            Else
                Me.SanboxReportTreeSelectionType = ReportSelectionType.NotSelected
            End If
        Else
            IsReportGroupMouseDownRight = False
        End If
    End Sub

    Private Sub tvReportGroup_MouseMove(sender As Object, e As MouseEventArgs) Handles tvReportGroup.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim node As Nodes.TreeListNode = tvReportGroup.FocusedNode
                Dim data As TreeListNode = tvReportGroup.GetNodeAt(e.Location)
                If data IsNot Nothing Then
                    tvReportGroup.DoDragDrop(data, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tvReportGroup_DragDrop(sender As Object, e As DragEventArgs) Handles tvReportGroup.DragDrop
        RemoveHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
        tvReportGroup.SuspendLayout()
        Dim treeNodePath As String = Nothing
        Dim targetNode As TreeListNode = Nothing
        Dim pt As Point = tvReportGroup.PointToClient(New Point(e.X, e.Y))
        targetNode = tvReportGroup.CalcHitInfo(pt).Node

        Dim sourceNode As TreeListNode = e.Data.GetData(GetType(TreeListNode))
        Dim sourceNodeText As String = sourceNode.GetDisplayText("ReportGroupName")
        Try
            Me.UseWaitCursor = True
            If (sourceNode.Level = 2 AndAlso targetNode.Level = 2) AndAlso (sourceNode.ParentNode.Tag = targetNode.ParentNode.Tag) Then
                'Move report with same category (report ordinal swap)
                treeNodePath = targetNode.ParentNode.ParentNode.GetDisplayText("ReportGroupName") & "\" & targetNode.ParentNode.GetDisplayText("ReportGroupName") & "\" & targetNode.GetDisplayText("ReportGroupName") & "\" & sourceNode(1).ToString
                Dim sourceReportId As Integer = 0
                Dim targatReportId As Integer = 0
                Dim sourceCategoryId As Integer = 0
                Dim targatCategoryId As Integer = 0
                sourceReportId = sourceNode.Tag
                targatReportId = targetNode.Tag
                sourceCategoryId = sourceNode(2)
                targatCategoryId = targetNode.ParentNode.Tag
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.SwapReportOrdinal(sourceReportId, targatReportId))
                RefreshReportGroup_TreeList()
                RefreshDashboardReport()
                RefreshSchedulerReport()
            ElseIf (sourceNode.Level = 1 AndAlso targetNode.Level = 1) Then
                'Move category over another category (category ordinal swap)
                Dim targetNodeID As Integer = CInt(targetNode.Tag)
                Dim sourceNodeID As Integer = CInt(sourceNode(0))
                Dim sourceNodeOrdinal As Integer = CInt(sourceNode(2))
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.SwapCategoryOrdinal(targetNodeID, sourceNodeID, sourceNodeOrdinal))
                RefreshReportGroup_TreeList()
            ElseIf (sourceNode.Level = 2 AndAlso targetNode.Level = 1) Then
                'Drag a report to another category (set report ordinal highest)
                DragReportToCategory(sourceNode.Tag, targetNode.Tag)
                RefreshReportGroup_TreeList()
            ElseIf (sourceNode.ParentNode.Tag <> targetNode.ParentNode.Tag) Then
                SetMessage("Sorry : You can drag report to category only.")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.UseWaitCursor = False
        End Try
        targetNode = GetNodeFromPath(tvReportGroup.Nodes, treeNodePath)
        If (targetNode IsNot Nothing) Then
            targetNode.Visible = True
        End If

        tvReportGroup.Refresh()
        tvReportGroup.ResumeLayout()
        AddHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
        tvReportGroup.SetFocusedNode(tvReportGroup.FindNodeByFieldValue("ReportGroupName", sourceNodeText))
        tvReportGroup_MouseUp(Nothing, Nothing)
    End Sub

    Private Sub tvReportGroup_Click(sender As Object, e As EventArgs) Handles tvReportGroup.Click
        'IsByClickReport = True
    End Sub

    Private Sub GetFieldByReportId(ByVal reportId As Integer)

        Dim sqlCommand As String = SQLReportContent.GetReport(reportId)
        Dim dsReportObjects As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, sqlCommand)
        ReportIDOwner = ""
        If (dsReportObjects IsNot Nothing) Then
            Try
                If lblReportMode.Text.ToUpper.Contains("EDIT MODE") Then
                    If dsReportObjects.Tables(3).IsValid Then

                        Dim isCMorPMObjects As Boolean = IIf(dsReportObjects.Tables(3)(0)("CMorPM_Objects").ToString.ToLower = "false", True, False)
                        Dim techPackID As Integer = dsReportObjects.Tables(3)(0)("TechnologyPackageID").ToString
                        SetComboBox(cmbReportTechnology, ComboSelectBased.ValueBased, techPackID)
                        Dim isCMorPMObjectsSelected As Boolean = IIf(cmbCMPM.SelectedItem.ToString.ToUpper = "CM", True, False)
                        If (Not (isCMorPMObjects AndAlso isCMorPMObjectsSelected)) Then
                            If isCMorPMObjects Then
                                SetComboBox(cmbCMPM, ComboSelectBased.TextBased, "CM")
                            Else
                                SetComboBox(cmbCMPM, ComboSelectBased.TextBased, "PM")
                            End If
                        End If

                        Dim sourceObjectID As String = dsReportObjects.Tables(3)(0)("SourceObjectID").ToString

                        If (Not (sourceObjectID = TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value)) Then
                            'RemoveHandler cmbObjectSource.SelectedIndexChanged, AddressOf cmbObjectSource_SelectedIndexChanged
                            SetComboBox(cmbObjectSource, ComboSelectBased.ValueBased, sourceObjectID)
                            'AddHandler cmbObjectSource.SelectedIndexChanged, AddressOf cmbObjectSource_SelectedIndexChanged
                        End If

                        Dim objectTypeID As String = dsReportObjects.Tables(3)(0)("ObjectTypeID").ToString
                        If (Not (objectTypeID = TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value)) Then
                            SetComboBox(cmbObjectType, ComboSelectBased.ValueBased, objectTypeID)
                        End If

                        Dim timeResolution As String = dsReportObjects.Tables(3)(0)("TimeResolution").ToString
                        If (Not (timeResolution = cmbTimeResolution.SelectedItem.ToString)) Then
                            SetComboBox(cmbTimeResolution, ComboSelectBased.TextBased, timeResolution)
                        End If

                        'clearing objecttree

                        If dsReportObjects.Tables(3)(0)("PreDefinedID") = 0 Then
                            dtEditStartTime.EditValue = CDate(dsReportObjects.Tables(3)(0)("TimeManualStart").ToString)
                            dtEditEndTime.EditValue = CDate(dsReportObjects.Tables(3)(0)("TimeManualStop").ToString)
                        Else
                            SetComboBox(vcmb_PredefinedPeriod, ComboSelectBased.ValueBased, dsReportObjects.Tables(3)(0)("PredefinedID").ToString)
                        End If

                        ReportIDOwner = dsReportObjects.Tables(6)(0)("LicenseUser").ToString

                    Else
                        SetMessage("Data Not found : Report has no any axis control.")
                    End If
                End If
            Catch ex As Exception
            End Try

            Try
                If dsReportObjects.Tables(0).IsValid Then
                    AddCounterAndKPIObject(dsReportObjects.Tables(0), DatamartFieldType.Counter)
                End If

            Catch ex As Exception
            End Try

            Try
                If dsReportObjects.Tables(1).IsValid Then
                    AddCounterAndKPIObject(dsReportObjects.Tables(1), DatamartFieldType.Kpi)
                End If

            Catch ex As Exception
            End Try

            Try
                If dsReportObjects.Tables(2).IsValid Then
                    AddCounterAndKPIObject(dsReportObjects.Tables(2), DatamartFieldType.ObjectFld)
                End If

                If lblReportMode.Text.ToUpper.Contains("EDIT MODE") Then
                    If tvObjects.Nodes.Count > 0 Then
                        TreeView_ClearChecks(tvObjects.Nodes(0))
                    End If

                    If dsReportObjects.Tables(4).IsValid Then
                        ObjectTreeChecked(dsReportObjects.Tables(4))
                    End If
                End If
            Catch ex As Exception
            End Try

            Try
                If dsReportObjects.Tables(5).IsValid Then
                    AddCounterAndKPIObject(dsReportObjects.Tables(5), DatamartFieldType.Time)
                End If

            Catch ex As Exception
            End Try

            Try
                If dsReportObjects.Tables(6).IsValid Then
                    txtSQLStatement.Text = dsReportObjects.Tables(6)(0)("ReportSQL").ToString
                    txtSandBoxTopX.Text = dsReportObjects.Tables(6)(0)("TopX").ToString
                    Me.GridorChart = dsReportObjects.Tables(6)(0)("GridOrChart").ToString
                    ReportIDOwner = dsReportObjects.Tables(6)(0)("LicenseUser").ToString
                End If

                If Me.GridorChart.ToUpper = "GRID" Then
                    rbChart.Checked = False
                    rbGrid.Checked = True
                    rbExport.Checked = False
                ElseIf Me.GridorChart.ToUpper = "CHART" Then
                    rbChart.Checked = True
                    rbGrid.Checked = False
                    rbExport.Checked = False
                ElseIf Me.GridorChart.ToUpper = "EXPORT" Then
                    rbChart.Checked = False
                    rbGrid.Checked = False
                    rbExport.Checked = True
                End If

            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub ObjectTreeChecked(ByRef dt As DataTable)
        Dim tv As TreeList = tvObjects
        Dim aggr_to As String = cmbObjectType.SelectedItem.ToString
        tv.Cursor = Cursors.WaitCursor
        Try
            Dim ExactMatch As Boolean = True

            If (dt.IsValid) Then
                For Each dr As DataRow In dt.Rows
                    Dim objectId As String = dr(ReportContentObjectsFields.OBJECT_ID)
                    objectId = objectId.Replace(vbLf, "").Replace(vbCrLf, "").Replace(Environment.NewLine, "")
                    If (Not String.IsNullOrEmpty(objectId)) Then
                        Dim tv_result As TreeListNode = Treeview_TagSearch(objectId.Trim, tv.Nodes, ExactMatch)
                        If Not tv_result Is Nothing Then
                            tv_result.Checked = True
                        End If
                    End If
                Next
            End If
            tv.Cursor = Cursors.Arrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            tv.Cursor = Cursors.Arrow
        End Try
    End Sub

    Public Function Treeview_TagSearch(ByVal SearchString As String, ByVal Nodes As TreeListNodes, Optional ByVal ExactMatch As Boolean = False) As TreeListNode
        Dim ret As TreeListNode
        Try
            For Each tn As TreeListNode In Nodes
                If ExactMatch = True Then
                    If tn.Tag IsNot Nothing Then
                        If tn.Tag.ToLower = SearchString.ToLower Then
                            Return tn
                        End If
                    End If
                Else
                    If tn.Tag.IndexOf(SearchString) <> -1 Then Return tn
                End If

                If tn.Nodes.Count > 0 Then
                    ret = Treeview_TagSearch(SearchString, tn.Nodes, ExactMatch)
                    If Not ret Is Nothing Then Return ret
                End If
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
        Return Nothing
    End Function

    Private Sub AddCounterAndKPIObject(ByRef dt As DataTable, ByVal fieldType As DatamartFieldType)
        Try
            If (dt.IsValid) Then
                Dim vSandBoxField As DevExSandBoxField
                If (fieldType = DatamartFieldType.Counter) Then

                    For Each dr As DataRow In dt.Rows
                        vSandBoxField = New DevExSandBoxField()
                        Dim isXAxisObject As Boolean = IIf(dr(ReportContentDimensionsFields.DIMENSION_AXIS).ToString = "0", True, False)
                        Dim sqlsourcetable As New List(Of String)
                        vSandBoxField.VSandBoxType = DatamartFieldType.Counter
                        vSandBoxField.Name = "vSandBoxCounter" & IIf(isXAxisObject, "X_", "Y_") & "_" & dr(ReportContentDimensionsFields.COUNTER_ID).ToString
                        vSandBoxField.Text = dr(ReportContentDimensionsFields.COUNTER_NAME).ToString ''& IIf(isXAxisObject, "X", "Y")
                        vSandBoxField.CounterID = dr(ReportContentDimensionsFields.COUNTER_ID).ToString
                        vSandBoxField.SourceObjectID = dr(ReportContentDimensionsFields.SOURCE_OBJECTID).ToString
                        sqlsourcetable.Add(dr(ReportContentDimensionsFields.SQL_SOURCETABLE).ToString)
                        vSandBoxField.SQL_SourceTable = sqlsourcetable
                        vSandBoxField.TimeAggregation = dr(ReportContentDimensionsFields.TIMEAGGREGATIONFORMULA).ToString
                        vSandBoxField.ObjectAggregation = dr(ReportContentDimensionsFields.OBJECTAGGREGATIONFORMULA).ToString
                        vSandBoxField.SQL_KPI_ID = "0"
                        vSandBoxField.ObjectTypeID = 0 ''dr(ReportContentDimensionsFields.SOURCE_OBJECTID).ToString
                        vSandBoxField.SortValue = dr(ReportContentDimensionsFields.SORT_ORDER).ToString
                        AttachedObjectWithXY(vSandBoxField, False, isXAxisObject)
                    Next

                ElseIf (fieldType = DatamartFieldType.Kpi) Then
                    For Each dr As DataRow In dt.Rows
                        vSandBoxField = New DevExSandBoxField()
                        Dim isXAxisObject As Boolean = IIf(dr(ReportContentDimensionsFields.DIMENSION_AXIS).ToString = "0", True, False)

                        vSandBoxField.VSandBoxType = DatamartFieldType.Kpi
                        vSandBoxField.Name = "vSandBoxKPI" & IIf(isXAxisObject, "X_", "Y_") & "_" & dr(ReportContentDimensionsFields.KPI_ID).ToString
                        vSandBoxField.Text = dr(ReportContentDimensionsFields.KPI_NAME).ToString ''& IIf(isXAxisObject, "X", "Y")

                        If Not flp_ValueY.Controls.ContainsKey(vSandBoxField.Name) Then

                            Dim dtfilter As DataTable = dt.SelectedRowsAsTable("KPIID='" + dr("KPIID").ToString + "'")
                            Dim sqlSourceTable As List(Of String) = New List(Of String)
                            If (dtfilter.Rows.Count > 0) Then
                                For Each dr2 As DataRow In dtfilter.Rows
                                    Dim sqlST As String = dr2(TechnologyPackageKPIFields.SQL_SOURCE_TABLE)
                                    If (sqlST.Length > 0 AndAlso sqlST IsNot Nothing AndAlso Not sqlSourceTable.Contains(sqlST)) Then
                                        sqlSourceTable.Add(sqlST)
                                    End If

                                Next
                            End If
                            vSandBoxField.SQL_KPIFormula = dr(ReportContentDimensionsFields.KPI_SQL).ToString
                            vSandBoxField.CounterID = "0" ''dr(ReportContentDimensionsFields.COUNTER_ID).ToString
                            vSandBoxField.SQL_SourceTable = sqlSourceTable

                            vSandBoxField.ObjectTypeID = "0"
                            vSandBoxField.SQL_KPI_ID = dr(ReportContentDimensionsFields.KPI_ID).ToString
                            vSandBoxField.SortValue = dr(ReportContentDimensionsFields.SORT_ORDER).ToString
                            AttachedObjectWithXY(vSandBoxField, False, isXAxisObject)
                        End If
                    Next
                ElseIf (fieldType = DatamartFieldType.ObjectFld) Then
                    For Each dr As DataRow In dt.Rows
                        vSandBoxField = New DevExSandBoxField()
                        Dim isXAxisObject As Boolean = IIf(dr(ReportContentDimensionsFields.DIMENSION_AXIS).ToString = "0", True, False)
                        vSandBoxField.VSandBoxType = DatamartFieldType.ObjectFld
                        vSandBoxField.SourceObjectID = dr(ReportContentDimensionsFields.OBJECTTYPE_ID).ToString
                        vSandBoxField.Text = dr(ReportContentDimensionsFields.DIMENSIONNAME).ToString
                        vSandBoxField.Name = "vSandBoxObject" & IIf(isXAxisObject, "X_", "Y_") & dr(ReportContentDimensionsFields.OBJECTTYPE_ID).ToString
                        vSandBoxField.SortValue = dr(ReportContentDimensionsFields.SORT_ORDER).ToString
                        ' vSandBoxField.SourceObjectID = dr(ReportContentDimensionsFields.OBJECTTYPE_PARENTID).ToString
                        vSandBoxField.ObjectTypeID = dr(ReportContentDimensionsFields.OBJECTTYPE_ID).ToString
                        AttachedObjectWithXY(vSandBoxField, False, isXAxisObject)
                        ' SelectObjectSourceByVSandBoxField(vSandBoxField.SourceObjectID, vSandBoxField.ObjectTypeID)
                    Next
                ElseIf (fieldType = DatamartFieldType.Time) Then
                    For Each dr As DataRow In dt.Rows
                        Dim isXAxisObject As Boolean = IIf(dr(ReportContentDimensionsFields.DIMENSION_AXIS).ToString = "0", True, False)
                        vSandBoxField = New DevExSandBoxField()
                        vSandBoxField.Name = "vSandBoxTimerX"
                        vSandBoxField.Text = "PERIOD_START_TIME"
                        vSandBoxField.VSandBoxType = DatamartFieldType.Time
                        vSandBoxField.SortValue = "None"
                        AttachedObjectWithXY(vSandBoxField, False, isXAxisObject)
                    Next
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub SelectObjectSourceByVSandBoxField(ByVal sourceObjectID As String, ByVal objectTypeID As String)
        Try
            If (sourceObjectID IsNot Nothing) Then
                Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(objectTypeID, cmbObjectSource)
                'vcmb_ObjectSource.Properties.Items.ToList().FindIndex(Function(c) c.Value = objectTypeID)
                If (cmbItem IsNot Nothing) Then
                    cmbObjectSource.SelectedItem = cmbItem
                End If
            End If
            If (objectTypeID IsNot Nothing) Then
                Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(objectTypeID, cmbObjectType)
                'vcmb_ObjectType.Properties.Items.ToList().FindIndex(Function(c) c.Value = objectTypeID)
                If (cmbItem IsNot Nothing) Then
                    cmbObjectType.SelectedItem = cmbItem
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub AttachedObjectWithXY(ByRef sandBoxField As DevExSandBoxField, ByVal isNew As Boolean, ByVal isXAxisObject As Boolean)
        sandBoxField.Size = New Size(sandBoxField.Text.Length * 8, 20)
        sandBoxField.AutoSize = True
        sandBoxField.Margin = New Padding(0, 0, 5, 0)
        sandBoxField.ContextMenuStrip = cm_FLPValueXY
        AddHandler sandBoxField.MouseDown, AddressOf SanboxField_MouseDown
        sandBoxField.LookAndFeel.SetSkinStyle("Office 2010 Black")
        Try
            If (isXAxisObject) Then
                flp_ValueX.Controls.Add(sandBoxField)
                flp_ValueX.Controls.SetChildIndex(sandBoxField, 0)
            ElseIf (Not dragDimensions) Then
                flp_ValueY.Controls.Add(sandBoxField)
                flp_ValueY.Controls.SetChildIndex(sandBoxField, 0)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            dragDimensions = False
        End Try
    End Sub

    Private Sub SanboxField_MouseDown(sender As Object, e As MouseEventArgs)

        If e.Button = MouseButtons.Right Then
            Dim sandBoxFieldSelected As DevExSandBoxField = TryCast(sender, DevExSandBoxField)
            Try
                If (sandBoxFieldSelected IsNot Nothing) Then
                    tsmi_FLPValueXY_SelectField.Text = "Select Field : " & sandBoxFieldSelected.Text
                    tsmi_FLPValueXY_FieldType.Text = "Field Type : " & IIf(sandBoxFieldSelected.VSandBoxType = 1, "Counter", IIf(sandBoxFieldSelected.VSandBoxType = 2, "KPI", IIf(sandBoxFieldSelected.VSandBoxType = 3, "Object", IIf(sandBoxFieldSelected.VSandBoxType = 4, "Timer", "None"))))
                    tsmi_FLPValueXY_TimeAggregation.Text = "Time Aggregation : " & sandBoxFieldSelected.TimeAggregation
                    tsmi_FLPValueXY_ObjectAggregation.Text = "Object Aggregation : " & sandBoxFieldSelected.ObjectAggregation

                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            Finally
            End Try
        End If
    End Sub

    Private Sub SetChartConfigurationDefaultValueCalculatedSeries(ByVal seriesName As String, ByVal calculatedSeriesTypeName As String, ByVal calculatedSeriesTypeID As String, ByVal calculatedSeriesTypeParamValues As String)
        Dim rnd As Random = New Random()
        Dim cl As Color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))
        colEditChartConfig_SeriesColor.Color = cl
        CustomCharts_Serie_Insert(
            seriesName & "_" & calculatedSeriesTypeName.Replace(" ", ""),
            ColorTranslator.ToOle(cl), "Line",
                                          "",
                                          GetAxisLocationOfSeries(seriesName),
                                          "",
                                          "",
                                          "",
                                          calculatedSeriesTypeID, calculatedSeriesTypeName, calculatedSeriesTypeParamValues, spinEdit_LineThickness.Value, cmbCalculatedYAxis.SelectedItem.ToString, ""
            )
    End Sub

    Private Function GetAxisLocationOfSeries(ByVal seriesName As String) As String
        For Each nd As TreeListViewNode In tlvSandboxChartsSeries.Nodes
            If nd.SubItems(0).Text.ToString.ToUpper = seriesName.ToUpper Then
                Return nd.SubItems(4).Text.ToString
            End If
        Next
        Return "Left"
    End Function

    'Private Sub tsmi_FLPValueXY_RemoveAllFields_Click_1(sender As Object, e As EventArgs) Handles tsmi_FLPValueXY_RemoveAllFields.Click
    '    Dim tsmiTemp As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
    '    Dim cmsTemp As ContextMenuStrip = CType(tsmiTemp.Owner, ContextMenuStrip)
    '    Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(cmsTemp.SourceControl, DevExSandBoxField)
    '    If (vSandBoxFieldSelected IsNot Nothing) Then
    '        Dim parentFlowLayoutPanel As FlowLayoutPanel = TryCast(vSandBoxFieldSelected.Parent, FlowLayoutPanel)
    '        If (parentFlowLayoutPanel IsNot Nothing) Then
    '            If (parentFlowLayoutPanel.Controls.Count > 0) Then

    '                lstTechCounter.Items.Clear()
    '                'lstTechKPI.Nodes.Clear()
    '                'vlst_TechCounter.UnCheckAllItems()
    '                'vlst_TechCounter_CheckedItems.Clear()
    '                'vlst_TechKPI.UnCheckAllItems()
    '                'vlst_TechKPI_CheckedItems.Clear()
    '                parentFlowLayoutPanel.Controls.Clear()
    '                tlvSandboxChartsSeries.Nodes.Clear()
    '                reportChartGrid_SendBox.ClearData()
    '                If dtChartConfigSandbox IsNot Nothing Then dtChartConfigSandbox.Clear()
    '                SetMessage("Removed All VSandbox")

    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub lstOperators_MouseDown(sender As Object, e As MouseEventArgs)
    '    Dim listControl As ListBoxControl = TryCast(sender, ListBoxControl)
    '    p = New Point(e.X, e.Y)
    '    Dim selectedIndex As Integer = listControl.IndexFromPoint(p)
    '    If selectedIndex = -1 Then
    '        p = Point.Empty
    '    End If
    'End Sub

    'Private Sub lstOperators_MouseMove(sender As Object, e As MouseEventArgs)
    '    If e.Button = MouseButtons.Left Then
    '        If (p <> Point.Empty) Then
    '            Dim listControl As ListBoxControl = TryCast(sender, ListBoxControl)
    '            If (listControl IsNot Nothing) Then
    '                Dim index As Integer = listControl.IndexFromPoint(p)
    '                If (index > -1) Then
    '                    Me.dragDropType = DragDropType.ByOprators
    '                    listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub txtKPIFormula_DragDrop(sender As Object, e As DragEventArgs) Handles txtKPIFormula.DragDrop
    '    Dim text As String = e.Data.GetData("System.String")
    '    Try
    '        If txtKPIFormula.SelectedText.Length = 0 AndAlso txtKPIFormula.SelectionStart = 0 Then
    '            If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then
    '                If (text = "/") Then
    '                    '' If (kpiDataBaseName = SandBoxApp.KPIDataBaseName.MSSQL) Then
    '                    text = "/ NULLIF((),0)"
    '                    'ElseIf (kpiDataBaseName = SandBoxApp.KPIDataBaseName.ORACLE) Then
    '                    '    text = "/ NULLIF(" & strDenominator & ",0)"
    '                    'ElseIf (kpiDataBaseName = SandBoxApp.KPIDataBaseName.None) Then
    '                    '    text = "/"
    '                    'End If
    '                End If
    '                If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
    '                    txtKPIFormula.Text = text
    '                Else
    '                    If (txtKPIFormula.Text.EndsWith("()")) Then
    '                        txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 1, text)
    '                    ElseIf txtKPIFormula.Text.Contains("/ NULLIF((),0)") Then
    '                        txtKPIFormula.Text = txtKPIFormula.Text.Replace("/ NULLIF((),0)", "/ NULLIF(" + text + ",0)")
    '                    Else
    '                        txtKPIFormula.Text += " " & text
    '                    End If
    '                End If
    '            End If
    '            If dragDropType = DragDropType.ByCounter Then
    '                ''Dim items() As VIBlend.WinForms.Controls.ListItem = e.Data.GetData("VIBlend.WinForms.Controls.vCheckedListBox.Items[])")
    '                Dim items As List(Of String) = text.Split("#").ToList
    '                If (items.Count >= 1) Then
    '                    ' Dim selectedTableCounterRows() As DataRow = dt_TechnologyPackageKPI.Select("TableName='" & items(9).Value.ToString() & "' and CounterName='" & items(10).Value.ToString() & "' ")

    '                    'If Not (IsItemExist(tabeleName, tlv_UsingTableName)) Then
    '                    '    ' InsertItemInUsingTableTLV(tabeleName, tableAlias)
    '                    '    'SetRowInDTUsingTable(tabeleName, tableKey, connectionName, dataBaseName, tableAlias, megaQuery)
    '                    '    'tlv_UsingTableName.Refresh()
    '                    '    'tlv_UsingTableName.UpdateLayout()
    '                    '    If (dataBaseName = dbMSSQL) Then
    '                    '        kpiDataBaseName = IOS.KPIDataBaseName.MSSQL
    '                    '    ElseIf (dataBaseName = dbORACLE) Then
    '                    '        kpiDataBaseName = IOS.KPIDataBaseName.ORACLE
    '                    '    Else
    '                    '        kpiDataBaseName = IOS.KPIDataBaseName.None
    '                    '    End If
    '                    'End If
    '                    Dim sourceTableAsTableAlias As String = GetSourceTableIdByCounterId(items(1)) '' items(0)
    '                    If (sourceTableAsTableAlias Is Nothing) Then
    '                        SetMessage("No Source Table found.")
    '                        Exit Sub
    '                    End If

    '                    'Dim counterNameAsTableCounter As String = items(1)
    '                    If Not list_of_used_tables.Contains(sourceTableAsTableAlias) Then
    '                        'Dim guiTimeResolution As String = cmbTimeResolution.SelectedItem.ToString
    '                        'Dim timeaggregationSuffix As String = String.Empty

    '                        'Dim guiObjectTableType As String = cmbObjectType.SelectedItem.ToString
    '                        'Dim suffixTimeAndObject As List(Of String) = New List(Of String)


    '                        'Dim suffixTime As String = ""
    '                        'Dim suffixObject As New List(Of String)

    '                        'suffixTime = GetTimeSuffix(sourceTableAsTableAlias, guiTimeResolution, guiObjectTableType)(0)
    '                        'suffixObject = GetObjectSuffix(sourceTableAsTableAlias, guiTimeResolution, guiObjectTableType)

    '                        'Dim st As String = ""
    '                        'If suffixTime <> "" And suffixObject(0) <> "_" + guiObjectTableType Then
    '                        '    st = "[" + sourceTableAsTableAlias.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
    '                        'Else
    '                        '    st = "[" + sourceTableAsTableAlias.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
    '                        'End If
    '                        list_of_used_tables.Add(sourceTableAsTableAlias)
    '                    End If

    '                    Dim tableAliasAndTableCounter = sourceTableAsTableAlias & ".[" & items(0) & "]"
    '                    If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
    '                        txtKPIFormula.Text = tableAliasAndTableCounter
    '                    Else

    '                        If (txtKPIFormula.Text.Contains("()")) Then

    '                            'Dim CharNo As New Integer
    '                            'CharNo = vtxt_KPIFormula.Text.IndexOf("[]")
    '                            Dim indexOfBrackets As Integer = txtKPIFormula.Text.IndexOf("()")
    '                            txtKPIFormula.Text = txtKPIFormula.Text.Remove(indexOfBrackets, 2)
    '                            txtKPIFormula.Text = txtKPIFormula.Text.Insert(indexOfBrackets, "(" & tableAliasAndTableCounter & ")")

    '                            'vtxt_KPIFormula.Text = vtxt_KPIFormula.Text.Remove(vtxt_KPIFormula.Text.IndexOf("[]"), 2)
    '                            'vtxt_KPIFormula.Text = vtxt_KPIFormula.Text.Insert(vtxt_KPIFormula.Text.Length - 1, tableAliasAndTableCounter)
    '                        ElseIf (txtKPIFormula.Text.EndsWith(",0)")) Then
    '                            Dim endIndex As Integer = txtKPIFormula.Text.IndexOf(",0)")
    '                            Dim listOfIndex As List(Of Integer) = GetMatchingIndexCollection(txtKPIFormula.Text, "(")
    '                            Dim startIndex As Integer = (From w In listOfIndex
    '                                                         Where w < endIndex
    '                                                         Select w).Max()
    '                            Dim sSubString As String = txtKPIFormula.Text.Substring(startIndex + 1, (endIndex - (startIndex + 1)))
    '                            If (strDenominator = sSubString) Then
    '                                txtKPIFormula.Text = txtKPIFormula.Text.Substring(0, txtKPIFormula.Text.IndexOf(strDenominator)) + tableAliasAndTableCounter & ",0)"
    '                            ElseIf (String.IsNullOrEmpty(sSubString)) Then
    '                                txtKPIFormula.Text = txtKPIFormula.Text.Insert(startIndex + 1, tableAliasAndTableCounter)
    '                            Else
    '                                txtKPIFormula.Text += tableAliasAndTableCounter
    '                                ' vtxt_KPIFormula.Text = vtxt_KPIFormula.Text.Insert(vtxt_KPIFormula.Text.Length - 3, tableAlias & "." & tabeleCounter)
    '                            End If
    '                        ElseIf txtKPIFormula.Text.EndsWith("()") Then
    '                            txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 1, tableAliasAndTableCounter)
    '                        Else
    '                            txtKPIFormula.Text += tableAliasAndTableCounter
    '                        End If

    '                    End If
    '                End If
    '            End If
    '        ElseIf txtKPIFormula.SelectedText.Length > 0 Then
    '            If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then
    '                If text = "Avg()" OrElse text = "Sum()" OrElse text = "Count()" OrElse text = "Min()" OrElse text = "Max()" Then
    '                    txtKPIFormula.Text = txtKPIFormula.Text.Replace(txtKPIFormula.SelectedText, text.Substring(0, text.IndexOf("(")))
    '                Else
    '                    txtKPIFormula.Text = txtKPIFormula.Text.Replace(txtKPIFormula.SelectedText, text)
    '                End If
    '            ElseIf dragDropType = DragDropType.ByCounter Then
    '                Dim items As List(Of String) = text.Split("#").ToList
    '                If (items.Count >= 1) Then
    '                    Dim sourceTableAsTableAlias As String = GetSourceTableIdByCounterId(items(1))
    '                    txtKPIFormula.Text = txtKPIFormula.Text.Replace(txtKPIFormula.SelectedText, sourceTableAsTableAlias & ".[" & items(0) & "]")
    '                End If
    '            End If
    '        ElseIf txtKPIFormula.SelectionStart > 0 Then
    '            If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then

    '                If (text = "/") Then
    '                    text = "/ NULLIF((),0)"
    '                End If
    '                If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
    '                    txtKPIFormula.Text = text
    '                Else
    '                    If (txtKPIFormula.Text.EndsWith("()")) Then
    '                        txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 1, text)
    '                    ElseIf txtKPIFormula.Text.Contains("/ NULLIF((),0)") Then
    '                        txtKPIFormula.Text = txtKPIFormula.Text.Replace("/ NULLIF((),0)", "/ NULLIF(" + text + ",0)")
    '                    Else
    '                        txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.SelectionStart, text)
    '                    End If
    '                End If
    '            ElseIf dragDropType = DragDropType.ByCounter Then
    '                Dim items As List(Of String) = text.Split("#").ToList
    '                If (items.Count >= 1) Then
    '                    Dim sourceTableAsTableAlias As String = GetSourceTableIdByCounterId(items(1))
    '                    If (sourceTableAsTableAlias Is Nothing) Then
    '                        SetMessage("No Source Table found.")
    '                        Exit Sub
    '                    Else

    '                        txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.SelectionStart, sourceTableAsTableAlias & ".[" & items(0) & "]")
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
    '    End Try
    'End Sub

    'Private Sub txtKPIFormula_DragOver(sender As Object, e As DragEventArgs) Handles txtKPIFormula.DragOver
    '    e.Effect = DragDropEffects.Copy
    'End Sub

    'Private Sub lstAggregateFunction_MouseMove(sender As Object, e As MouseEventArgs) Handles lstAggregateFunction.MouseMove
    '    Try
    '        If e.Button = MouseButtons.Left Then
    '            If (p <> Point.Empty) Then
    '                Dim listControl As ListBoxControl = TryCast(sender, ListBoxControl)
    '                If (listControl IsNot Nothing) Then
    '                    Dim index As Integer = listControl.IndexFromPoint(p)
    '                    If (index > -1) Then
    '                        Me.dragDropType = DragDropType.ByAggregrate
    '                        listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch
    '    End Try
    'End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            If (tvReportGroup.FocusedNode.Level = 2) Then

                Try
                    Dim frmReportContentFilters As New frmSBReportContentFilters()
                    frmReportContentFilters.ReportId = tvReportGroup.FocusedNode.Tag
                    frmReportContentFilters.reportConnString = dt_TechPackCounter.Rows(0)("SQL_ConnString").ToString 'dtChartConfigSandbox.Rows(0)("ReportConnString").ToString
                    frmReportContentFilters.ShowDialog()
                    If (frmReportContentFilters.IsFilterInserted) Then
                        SetMessage("Filter applied.")
                    Else
                        SetMessage("Filter not applied.")
                    End If

                    dtReportFilterData = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContentFilter.GetReportContentFilter(tvReportGroup.FocusedNode.Tag))

                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                Finally
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
                End Try
            Else
                SetMessage("Please select report")
            End If
        Else
            SetMessage("Please select report")
        End If
    End Sub

    Private Sub btnChartConfigFont_Click(sender As Object, e As EventArgs) Handles btnChartConfigFont.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim fdlg_font As New FontDialog
            If fdlg_font.ShowDialog = DialogResult.OK Then
                If (fdlg_font.Font.Style = FontStyle.Bold) Then
                    txtChartAxisFont.Text = fdlg_font.Font.Name & ", " & fdlg_font.Font.Size & ",FontStyle.Bold"
                ElseIf (fdlg_font.Font.Style = FontStyle.Italic) Then
                    txtChartAxisFont.Text = fdlg_font.Font.Name & ", " & fdlg_font.Font.Size & ", FontStyle.Italic"
                ElseIf (fdlg_font.Font.Style = FontStyle.Regular) Then
                    txtChartAxisFont.Text = fdlg_font.Font.Name & ", " & fdlg_font.Font.Size & ", FontStyle.Regular"
                ElseIf (fdlg_font.Font.Style = FontStyle.Strikeout) Then
                    txtChartAxisFont.Text = fdlg_font.Font.Name & ", " & fdlg_font.Font.Size & ", FontStyle.Strikeout"
                ElseIf (fdlg_font.Font.Style = FontStyle.Underline) Then
                    txtChartAxisFont.Text = fdlg_font.Font.Name & ", " & fdlg_font.Font.Size & ", FontStyle.Underline"
                End If

                If isChartSerieSelected = True Then
                    Try
                        If (tlvSandboxChartsSeries.SelectedNode IsNot Nothing) Then
                            Dim nd As TreeListViewNode = tlvSandboxChartsSeries.SelectedNode
                            nd.SubItems(13).Text = txtChartAxisFont.Text
                            Dim oRowsInTarget As DataRow() = dtChartConfigSandbox.Select(ReportChartFields.SeriesName & " = '" & nd.SubItems(0).Text & "'")
                            If oRowsInTarget.Count > 0 Then
                                For Each dr As DataRow In oRowsInTarget
                                    dr(ReportChartFields.LineSize) = txtChartAxisFont.Text
                                Next
                            End If
                            tlvSandboxChartsSeries.Refresh()
                        End If
                    Catch
                    End Try
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cms_ReportChartGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_ReportChartGrid.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            cm_SourceControl = cmsTemp.SourceControl
            Dim tempSplitCont As SplitContainer = GetSplitControl(cm_SourceControl)
            If (tempSplitCont IsNot Nothing) Then
                Dim ch As Chart = TryCast(tempSplitCont.Panel1.Controls(0), Chart)
                If (ch IsNot Nothing) Then
                    tsmi_DashboardReportName.Text = "Report Name : " & ch.TitleBox.HeaderLabel.Text
                    tsmi_DashboardReportTechnology.Text = "Technology : " & ch.Tag
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_DashboardReportHideAndShowGrid_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardReportHideAndShowGrid.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim tempSplitCont As SplitContainer = GetSplitControl(cm_SourceControl)
        Try
            If (tempSplitCont IsNot Nothing) Then
                If (tempSplitCont.Panel2Collapsed.Equals(True)) Then
                    tempSplitCont.Panel2Collapsed = False
                    tempSplitCont.Panel1Collapsed = True
                    tempSplitCont.Panel1.Hide()
                    DataMartGridView.RefreshingGrid(CType(tempSplitCont.Panel2.Controls(0), DevExpress.XtraGrid.GridControl), CType(tempSplitCont.Panel2.Controls(0), DevExpress.XtraGrid.GridControl).DefaultView, True, False)
                Else
                    tempSplitCont.Panel1Collapsed = False
                    tempSplitCont.Panel2Collapsed = True
                    tempSplitCont.Panel2.Hide()
                End If
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

    Private Sub tsmi_ExportToExcel_Click(sender As Object, e As EventArgs) Handles tsmi_ExportToExcel.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim dtExport As New DataTable
        Try
            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Excel Workbook |*.xlsx"
            objFileDlg.Title = "Save an excel File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then
                    WaitScreen.ShowWaitScreen("Exporting to excel...")
                    Application.DoEvents()
                    dtExport = reportChartGrid_SendBox.gcReportChartGrid.DataSource
                    'IOS.Library.IOSDevExpressGrid.DataTable2Excel(dtExport, objFileDlg.FileName)
                    ExportDataTableToExcel_Stream(dtExport, objFileDlg.FileName)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            dtExport.Dispose()
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ExportGridToCSV_Click(sender As Object, e As EventArgs) Handles tsmi_ExportGridToCSV.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim dtExport As New DataTable
        Try
            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Comma Delimited|*.csv"
            objFileDlg.Title = "Save a CSV File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then
                    WaitScreen.ShowWaitScreen("Exporting to CSV...")
                    Application.DoEvents()
                    dtExport = reportChartGrid_SendBox.gcReportChartGrid.DataSource
                    IOS.Library.IOSDevExpressGrid.DataTable2CSV(dtExport, objFileDlg.FileName)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            dtExport.Dispose()
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_DashboardReportCopyDataToClipboard_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardReportCopyDataToClipboard.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim tempSplitCont As SplitContainer = GetSplitControl(cm_SourceControl)
        Try
            If (tempSplitCont IsNot Nothing) Then
                DataMartGridView.SelectAllAndCopyGridData(CType(tempSplitCont.Panel2.Controls(0), DevExpress.XtraGrid.GridControl), CType(tempSplitCont.Panel2.Controls(0), DevExpress.XtraGrid.GridControl).DefaultView)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_DashboardReportCopyChartToClipboard_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardReportCopyChartToClipboard.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tempSplitCont As SplitContainer = GetSplitControl(cm_SourceControl)
            If (tempSplitCont IsNot Nothing) Then
                Dim ch As Chart = TryCast(tempSplitCont.Panel1.Controls(0), Chart)
                If (ch IsNot Nothing) Then
                    Clipboard.Clear()
                    'adding logo
                    ch.MarginTop = 0

                    ' Next we place the logo on the chart using an annotation.
                    ''Dim a As New Annotation(New Background(Application.StartupPath & "\IOS_Logo_Chart.bmp"))
                    ''a.DynamicSize = False
                    ''a.Position = New System.Drawing.Point(ch.Width - 100, 10)
                    ''a.Shadow.Visible = False
                    ''ch.Annotations.Add(a)
                    Clipboard.SetImage(ch.GetChartBitmap)
                    ''ch.Annotations.Remove(a)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_DashboardReportGetSQL_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardReportGetSQL.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim tempSplitCont As SplitContainer = GetSplitControl(cm_SourceControl)
        Try
            If (tempSplitCont IsNot Nothing) Then
                Dim rcg As ReportChartGrid = TryCast(tempSplitCont.Parent, ReportChartGrid)
                If (rcg IsNot Nothing) Then
                    Clipboard.SetText(rcg.reportSQL)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cms_ReportTLV_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_ReportTLV.Opening
        isByClick = True
        If (tvReportGroup.Nodes.Count > 0) Then
            tvReportGroup.ContextMenuStrip.Show()
            tvReportGroup.OptionsBehavior.Editable = False
            tvReportGroup.OptionsBehavior.ReadOnly = True
        Else
            e.Cancel = True
            tvReportGroup.ContextMenuStrip.Hide()
            Return
        End If
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            If (tvReportGroup.FocusedNode.Level = 0) Then
                isByClick = False
                If (ToolTipController1.GetToolTip(tvReportGroup) IsNot Nothing AndAlso ToolTipController1.GetToolTip(tvReportGroup) <> "") Then
                    Dim isPrivate As Boolean = IIf(ToolTipController1.GetToolTip(tvReportGroup).Split("_")(1).ToString.ToUpper = "1", True, False)
                    tsmi_ReportGroupStatusPrivate.Checked = isPrivate
                    tsmi_ReportGroupStatusPublic.Checked = IIf(isPrivate, False, True)
                    isByClick = True
                End If
            End If
        Else
            tvReportGroup.ContextMenuStrip.Hide()
            Return
        End If
        Try
            If (Me.SanboxReportTreeSelectionType = ReportSelectionType.Group) Then
                tsmi_ReportGroupRename.Enabled = True
                tsmi_ReportGroupModify.Enabled = True
                tsmi_ReportGroupDelete.Enabled = True
                tsmi_ReportGroupRename.Enabled = True
                tsmi_ReportGroupStatus.Enabled = True
                tsmi_CategoryDelete.Enabled = False
                tsmi_CategoryInsert.Enabled = True
                tsmi_CategoryRename.Enabled = False
                tsmi_ReportDelete.Enabled = False
                tsmi_ReportInsert.Enabled = False
                tsmi_ReportRename.Enabled = False
                tsmi_ReportEdit.Enabled = False
                tsmi_ReportCopy.Enabled = False
            ElseIf (Me.SanboxReportTreeSelectionType = ReportSelectionType.Category) Then
                tsmi_ReportGroupRename.Enabled = False
                tsmi_ReportGroupModify.Enabled = False
                tsmi_ReportGroupRename.Enabled = False
                tsmi_ReportGroupDelete.Enabled = False
                tsmi_ReportGroupStatus.Enabled = False

                If TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Tag.ToUpper = System.Environment.UserName.ToUpper Then
                    tsmi_CategoryDelete.Enabled = True
                    tsmi_CategoryInsert.Enabled = True
                    tsmi_CategoryRename.Enabled = True
                Else
                    tsmi_CategoryDelete.Enabled = False
                    tsmi_CategoryInsert.Enabled = False
                    tsmi_CategoryRename.Enabled = False
                End If

                tsmi_ReportDelete.Enabled = False
                tsmi_ReportInsert.Enabled = True
                tsmi_ReportRename.Enabled = False
                tsmi_ReportEdit.Enabled = False
                tsmi_ReportCopy.Enabled = False
            ElseIf (Me.SanboxReportTreeSelectionType = ReportSelectionType.Report) Then
                tsmi_ReportGroupRename.Enabled = False
                tsmi_ReportGroupDelete.Enabled = False
                tsmi_ReportGroupModify.Enabled = False
                tsmi_ReportGroupRename.Enabled = False
                tsmi_ReportGroupStatus.Enabled = False
                tsmi_CategoryDelete.Enabled = False
                tsmi_CategoryInsert.Enabled = False
                tsmi_CategoryRename.Enabled = False
                tsmi_ReportDelete.Enabled = False
                tsmi_ReportInsert.Enabled = True
                tsmi_ReportRename.Enabled = False
                tsmi_ReportEdit.Enabled = True
                tsmi_ReportCopy.Enabled = True

                If Environment.UserName.ToString = tvReportGroup.FocusedNode.GetValue(5).ToString Then
                    tsmi_ReportDelete.Enabled = True
                    tsmi_ReportRename.Enabled = True
                End If

            Else
                tsmi_ReportGroupDelete.Enabled = False
                tsmi_ReportGroupRename.Enabled = False
                tsmi_ReportGroupModify.Enabled = False
                tsmi_ReportGroupRename.Enabled = False
                tsmi_ReportGroupStatus.Enabled = False
                tsmi_CategoryDelete.Enabled = False
                tsmi_CategoryInsert.Enabled = False
                tsmi_CategoryRename.Enabled = False
                tsmi_ReportDelete.Enabled = False
                tsmi_ReportInsert.Enabled = False
                tsmi_ReportRename.Enabled = False
                tsmi_ReportEdit.Enabled = False
                tsmi_ReportCopy.Enabled = False
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub cms_KPIManage_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_KPIManage.Opening
        Try
            If (lstTechKPI.Nodes.Count > 0) Then
                lstTechKPI.ContextMenuStrip.Show()
            Else
                e.Cancel = True
                lstTechKPI.ContextMenuStrip.Hide()
                Return
            End If
            If (Not lstTechKPI.FocusedNode Is Nothing) Then
                'If cmbKPIGroup.SelectedItem.ToString.ToUpper = "ALL" Then
                If (lstTechKPI.FocusedNode.Level = 0) Then
                    tsmi_RenameCategory.Enabled = True
                    tsmi_DeleteCategory.Enabled = True
                    tsmi_ViewKPI.Enabled = False
                    tsmi_ModifyKPI.Enabled = False
                    tsmi_KPIRemoveCategory.Enabled = False
                ElseIf (lstTechKPI.FocusedNode.Level = 1) Then
                    Dim kpiGroupCreator As String = TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Tag.ToString
                    Dim kpiCreator As String = IIf(IsDBNull(lstTechKPI.GetFocusedRow(2)), "", lstTechKPI.GetFocusedRow(2))
                    If (kpiGroupCreator.ToUpper = Environment.UserName.ToUpper Or kpiCreator.ToUpper = Environment.UserName.ToUpper) Then   'Or kpiCreator = 
                        tsmi_KPIDeleteDatabase.Enabled = True
                    Else
                        tsmi_KPIDeleteDatabase.Enabled = False
                    End If
                    tsmi_ViewKPI.Enabled = True
                    tsmi_ModifyKPI.Enabled = True
                    tsmi_KPIRemoveCategory.Enabled = True
                    tsmi_RenameCategory.Enabled = False
                    tsmi_DeleteCategory.Enabled = False
                End If

                'enable/disable copy
                If checkedKPINameList.Count > 0 Then
                    tsmi_KPICopy.Text = "Copy - Objects: " & checkedKPINameList.Count.ToString
                    tsmi_KPICopy.Enabled = True
                Else
                    tsmi_KPICopy.Text = "Copy"
                    tsmi_KPICopy.Enabled = False
                End If

                'check clipboard
                Dim s As String = Clipboard.GetText()
                Dim rows() As String = s.Split(ControlChars.NewLine)
                Dim i, j As Integer
                If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                    tsmi_ObjectPaste.Text = "Paste - Objects: ?"
                    tsmi_ObjectPaste.Enabled = True
                Else
                    Dim clipboardmatches As Integer = 0
                    For i = 0 To rows.Length - 1
                        'Split row into cells
                        Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                        For j = 0 To bufferCell.Length - 1
                            If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                                bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                            End If
                            If bufferCell(j).ToString.Contains("'") Then
                                bufferCell(j) = bufferCell(j).ToString.Replace("'", "")
                            End If
                            If bufferCell(j).Trim <> "" Then
                                If Not lstTechKPI.FindNodeByFieldValue("KPICategoryName", bufferCell(j).Trim) Is Nothing Then
                                    clipboardmatches = clipboardmatches + 1
                                End If
                            End If
                        Next
                    Next

                    'enable/disable paste
                    If clipboardmatches > 0 Then
                        tsmi_KPIPaste.Text = "Paste - Objects: " & clipboardmatches
                        tsmi_KPIPaste.Enabled = True
                    Else
                        tsmi_KPIPaste.Text = "Paste"
                        tsmi_KPIPaste.Enabled = False
                    End If
                    lstTechKPI.Cursor = Cursors.Arrow
                End If

                'Else
                '    Dim CreatorName As String = TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Tag
                '    If (CreatorName.ToUpper = System.Environment.UserName.ToUpper) Then
                '        cms_KPIManage.Enabled = True
                '    Else
                '        cms_KPIManage.Enabled = False
                '    End If
                'End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tsmi_KPICopy_Click(sender As Object, e As EventArgs) Handles tsmi_KPICopy.Click
        Clipboard.Clear()
        Try
            Dim copystring As String = String.Join(",", checkedKPINameList)
            copystring = copystring.Replace(",", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            lstTechKPI.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub tsmi_KPIPaste_Click(sender As Object, e As EventArgs) Handles tsmi_KPIPaste.Click
        lstTechKPI.Cursor = Cursors.WaitCursor
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim s As String = Clipboard.GetText()
            Dim rows() As String = s.Split(ControlChars.NewLine)
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
                        If bufferCell(j).ToString.Contains("'") Then
                            bufferCell(j) = bufferCell(j).ToString.Replace("'", "")
                        End If
                        Dim tv_result As TreeListNode = lstTechKPI.FindNodeByFieldValue("KPICategoryName", bufferCell(j).Trim)
                        If Not tv_result Is Nothing Then
                            tv_result.SetValue("riChkEdit", True)
                            checkedKPINameList.Add(bufferCell(j).Trim)
                        End If
                    Next
                Next
            End If
            lstTechKPI.Cursor = Cursors.Arrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            lstTechKPI.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            lstTechKPI.Cursor = Cursors.Arrow
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub cmbTimeResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTimeResolution.SelectedIndexChanged
        If cmbTimeResolution.Text.ToUpper() = "HOUR" OrElse cmbTimeResolution.Text.ToUpper() = "RAW" Then
            vcmb_PredefinedPeriod.SelectedIndex = 1
            Dim val As Integer = vcmb_PredefinedPeriod.SelectedIndex
        ElseIf cmbTimeResolution.Text.ToUpper() = "DAY" OrElse cmbTimeResolution.Text.ToUpper() = "BH" Then
            vcmb_PredefinedPeriod.SelectedIndex = 4
        Else
            vcmb_PredefinedPeriod.SelectedIndex = 0
        End If
    End Sub

    Private Sub lstTechKPI_DragOver(sender As Object, e As DragEventArgs) 'Handles lstTechKPI.DragOver
        If e.Data.GetDataPresent(GetType(System.String)) Then
            e.Effect = DragDropEffects.Move
            isDraggedKPIItem = True
        Else
            e.Effect = DragDropEffects.None
        End If
        If e.Data.GetDataPresent(GetType(System.String)) Then
            e.Effect = DragDropEffects.Move
            isDraggedKPIItem = True
        Else
            e.Effect = DragDropEffects.None
        End If
        Try
            If e.Data.GetDataPresent(GetType(System.String)) Then
                e.Effect = DragDropEffects.Move
                isDraggedKPIItem = True
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub lstTechKPI_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstTechKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME) Then
                lstTechKPI.OptionsBehavior.Editable = False
                lstTechKPI.OptionsBehavior.ReadOnly = True
            ElseIf (lstTechKPI.FocusedColumn.FieldName = "riChkEdit") Then
                lstTechKPI.OptionsBehavior.Editable = True
                lstTechKPI.OptionsBehavior.ReadOnly = False
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub lstTechKPI_FocusedColumnChanged(sender As Object, e As FocusedColumnChangedEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (e.Column.FieldName = KPIGroupFields.KPI_CATEGORY_NAME) Then
                lstTechKPI.OptionsBehavior.Editable = False
                lstTechKPI.OptionsBehavior.ReadOnly = True
            ElseIf (e.Column.FieldName = "riChkEdit") Then
                lstTechKPI.OptionsBehavior.Editable = True
                lstTechKPI.OptionsBehavior.ReadOnly = False
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub riChk_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            Dim selectedTreeNode As TreeListNode = lstTechKPI.FocusedNode
            Dim selectedNodeValue As Object = lstTechKPI.GetDataRecordByNode(lstTechKPI.FocusedNode)
            Dim isNeedToUpdate As Boolean = False

            If chkBox.Checked = True Then
                If Not checkedKPINameList.Contains(selectedNodeValue(0).ToString()) Then
                    checkedKPINameList.Add(selectedNodeValue(0).ToString())
                End If
            Else
                checkedKPINameList.Remove(selectedNodeValue(0).ToString())
                viewCheckedKPINameList.Remove(selectedNodeValue(0).ToString())
            End If

            If (chkBox.Checked = True) AndAlso (Not checkedKPI.Contains(selectedTreeNode.Tag)) Then
                If (tvReportGroup.FocusedNode IsNot Nothing AndAlso tvReportGroup.FocusedNode.Level = 2) Then
                    checkedKPI.Add(selectedTreeNode.Tag)
                    flp_AddVsandBox(selectedNodeValue(0).ToString(), selectedTreeNode.Tag, DatamartFieldType.Kpi, flp_ValueY)
                End If
                BindListBoxByKPI()
            ElseIf (chkBox.Checked = False) Or checkedKPI.Contains(selectedTreeNode.Tag) Then
                checkedKPI.Remove(selectedTreeNode.Tag)
                lstTechKPICheckedItems.Remove(selectedTreeNode.Tag)
                flp_RemoveSandbox(selectedNodeValue(0).ToString(), flp_ValueY)
                If (checkedKPI.Count > 0) Then
                    BindListBoxByKPI()
                Else
                    BindListBoxByKPI()
                    isNeedToUpdate = True
                End If
            End If
            Dim KPIFilter As String = String.Empty
            KPIFilter = TechnologyPackageKPIFields.KPI_ID & OperatorConst.Equal & selectedTreeNode.Tag
            If (Not String.IsNullOrEmpty(KPIFilter)) Then
                Dim dtTachPackCounter As DataTable = dt_TechnologyPackageKPI.SelectedRowsAsTable(KPIFilter)
                If (dtTachPackCounter.IsValid) Then
                    isClickedByObjectSource = False
                    Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(dtTachPackCounter.Rows(0)(TechnologyPackageKPIFields.SOURCE_OBJECT_ID), cmbObjectSource)
                    If (cmbItem IsNot Nothing) Then
                        cmbObjectSource.SelectedItem = cmbItem
                    End If
                    isClickedByObjectSource = True
                End If
            End If
            If chkBox.Checked AndAlso Not lstTechKPICheckedItems.Contains(selectedTreeNode.Tag) Then
                lstTechKPICheckedItems.Add(selectedTreeNode.Tag)
            End If
            If (checkedKPI.Count <= 0 AndAlso isNeedToUpdate) Then
                lstTechCounter.Text = ""
                lstTechMeasurement.Text = ""
                BindListBoxBySource(TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnAddKPIGroup_Click(sender As Object, e As EventArgs) Handles btnAddKPIGroup.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim frmGroupInsert As New dlgSBGroupInsert()
            frmGroupInsert.SetConnectionString(connStrSandBoxServer)
            frmGroupInsert.GroupTypeInserting = GroupType.KpiGroup
            frmGroupInsert.ShowDialog()
            Dim newGroupName As String = frmGroupInsert.NewGroup
            Dim RetrunData As Boolean = frmGroupInsert.IsGroupPrivate
            If (newGroupName IsNot Nothing) Then
                If (newGroupName IsNot Nothing) Then
                    BindKPIGroup()
                    Dim cmbItem As clsComboBoxItem = GetComboItemFromText(newGroupName, cmbKPIGroup)
                    cmbKPIGroup.SelectedItem = cmbItem
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnAddKPI_Click(sender As Object, e As EventArgs) Handles btnAddKPI.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Dim objSBKPIManager As New frmSBKPIManage()
            If (cmbKPIGroup.SelectedItem.ToString.ToUpper <> "ALL") Then
                objSBKPIManager.kpiGroupID = TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Value
                objSBKPIManager.kpiGroupName = cmbKPIGroup.SelectedItem.ToString()
                objSBKPIManager.kpiGroupOwner = TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Tag
                objSBKPIManager.teckPackValue = TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value
            Else
                objSBKPIManager.kpiGroupID = 0
                objSBKPIManager.kpiGroupName = cmbKPIGroup.SelectedItem.ToString()
                objSBKPIManager.kpiGroupOwner = ""
                objSBKPIManager.teckPackValue = TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value
            End If
            objSBKPIManager.ShowDialog()
            RefreshKPITree()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub lstTechKPI_MouseDown(sender As Object, e As MouseEventArgs) 'Handles lstTechKPI.MouseDown
        Try
            Dim selectedTreeNode As TreeListNode = lstTechKPI.FocusedNode
            Dim selectedCheckBox As Repository.RepositoryItemCheckEdit = lstTechKPI.FocusedColumn.ColumnEdit
            Dim vSandBoxFieldType As DatamartFieldType = New DatamartFieldType()
            vSandBoxFieldType = DatamartFieldType.None
            If (selectedTreeNode IsNot Nothing) Then
                Dim itemindex As Integer = selectedTreeNode.Level
                If e.Button = MouseButtons.Left Then
                    rightMouseOnListbox = False

                    If (selectedTreeNode.Level = "1") Then
                        vSandBoxFieldType = DatamartFieldType.Counter
                        dragDropType = DragDropType.ByCounter
                    ElseIf (selectedTreeNode.Level = "2") Then
                        vSandBoxFieldType = DatamartFieldType.Kpi
                        dragDropType = DragDropType.ByCounter
                    End If

                    If (itemindex > -1) Then
                        Dim item As Object = itemindex
                        If item IsNot Nothing Then
                            Dim counterdragdroptext As String = selectedTreeNode.TreeList.FocusedValue & "#" & itemindex & "#" & vSandBoxFieldType
                            lstTechKPI.DoDragDrop(counterdragdroptext, DragDropEffects.Copy)
                            selectedTreeNode.SelectImageIndex = selectedTreeNode.Level
                        End If
                    End If
                Else
                    rightMouseOnListbox = True
                End If

                If e.Button = MouseButtons.Right Then
                    Dim item As Object = Nothing
                    If itemindex > -1 Then
                        item = itemindex
                    End If

                    If item IsNot Nothing Then
                        If (selectedTreeNode.Level = "2") Then
                            cms_KPIManage.Show(MousePosition)
                        End If

                        If lstTechKPICheckedItems.Contains(itemindex) Then
                            lstTechKPI.SetNodeCheckState(lstTechKPI.FocusedNode, CheckState.Checked)
                        Else
                            lstTechKPI.SetNodeCheckState(lstTechKPI.FocusedNode, CheckState.Unchecked)
                        End If

                    End If
                End If
                If Not lstTechKPI.RepositoryItems.Count = 0 Then
                    lstTechKPI.SetNodeCheckState(lstTechKPI.FocusedNode, Not selectedTreeNode(itemindex))
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnAddCategory_Click(sender As Object, e As EventArgs) Handles btnAddCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim frmGroupInsert As New dlgSBGroupInsert()
            frmGroupInsert.SetConnectionString(connStrSandBoxServer)
            frmGroupInsert.GroupTypeInserting = GroupType.KpiCategory
            frmGroupInsert.KPIGroupID = TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Value
            frmGroupInsert.ShowDialog()
            Dim newCategoryName As String = frmGroupInsert.NewGroup
            If (newCategoryName IsNot Nothing) Then
                If (newCategoryName IsNot Nothing) Then
                    RefreshKPITree()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnCreateKPI_Click(sender As Object, e As EventArgs) Handles btnCreateKPI.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If Not Application.OpenForms().OfType(Of frmDatamartKpiConfig).Any Then
                objDMKpiConfig = New frmDatamartKpiConfig()
                objDMKpiConfig.txtKPIName.Text = String.Empty
                objDMKpiConfig.txtValueIfNull.Text = String.Empty
                objDMKpiConfig.txtKPIFormula.Text = String.Empty
                objDMKpiConfig.txtKPIDescription.Text = String.Empty
                If cmbObjectType.SelectedIndex > 0 Then
                    objDMKpiConfig.kpiConfigObjectType = cmbObjectType.Properties.Items(1).ToString
                    objDMKpiConfig.lblKPIConfigObjectType.Text = cmbObjectType.Properties.Items(1).ToString
                End If
                objDMKpiConfig.btnCommitKPI.Enabled = True
                objDMKpiConfig.btnTestKPI.Enabled = True
                objDMKpiConfig.list_of_used_tables.Clear()
                objDMKpiConfig.kpiGroupID = CInt(TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Value)
                objDMKpiConfig.Show()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    'Private Function GetMatchingIndexCollection(ByVal str As String, ByVal pattern As String) As List(Of Integer)
    '    Dim list As New List(Of Integer)
    '    For index As Integer = 0 To str.Length - 1
    '        If (str(index) = pattern) Then
    '            list.Add(index)
    '        End If
    '    Next
    '    Return list
    'End Function

    Private Function GetSourceTableIdByCounterId(ByVal counterId As String) As String
        '' Dim objectSourceFilter As String = IIf(vcmb_ObjectSource.SelectedIndex > 0, AggregateConst.AND_Only & TechnologyPackageCountersFields.SOURCE_OBJECT_ID & OperatorConst.Equal & vcmb_ObjectSource.SelectedItem.Value, "")

        ''UPDATE LISTBOX(3) COUNTER

        Try
            If (Not String.IsNullOrEmpty(counterId)) Then
                Dim dtTachPackCounterMeasurment As DataTable = dt_TechPackCounter.SelectedRowsAsTable(TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & counterId)
                If (dtTachPackCounterMeasurment.IsValid) Then
                    Return dtTachPackCounterMeasurment.Rows(0)(TechnologyPackageCountersFields.SQL_SOURCE_TABLE).ToString
                End If

            End If
        Catch EX As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", EX.Message)
        End Try
        Return Nothing
    End Function

#Region "ToolStripMenu"

#Region "Report TreeView ContextMenu"

    Private Sub tsmi_CategoryInsert_Click(sender As Object, e As EventArgs) Handles tsmi_CategoryInsert.Click
        If (cmbReportGroup.SelectedIndex > 0) Then
            Me.UseWaitCursor = True
            Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportCategory.InsertReportCategory(TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value, "New Category"))
                SetMessage("A New Category Inserted")
                RefreshReportGroup_TreeList()
                RefreshDashboardReport()
                RefreshSchedulerReport()
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            Finally
                Me.UseWaitCursor = False
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End Try
        End If
    End Sub

    Private Sub tsmi_CategoryDelete_Click(sender As Object, e As EventArgs) Handles tsmi_CategoryDelete.Click
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            Me.UseWaitCursor = True
            Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                If (tvReportGroup.FocusedNode.Level = 1) Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportCategory.DeleteReportCategory(tvReportGroup.FocusedNode.Tag.ToString, tvReportGroup.FocusedNode.ParentNode.Tag.ToString))
                    tvReportGroup.Nodes.Clear()
                    RefreshReportGroup_TreeList()
                    RefreshDashboardReport()
                    RefreshSchedulerReport()
                Else
                    SetMessage("Select any Node from tree")
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            Finally
                Me.UseWaitCursor = False
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End Try
        End If
    End Sub

    Private Sub tsmi_CategoryRename_Click(sender As Object, e As EventArgs) Handles tsmi_CategoryRename.Click
        If (tvReportGroup.FocusedNode.Level = 1) Then
            tvReportGroup.OptionsBehavior.Editable = True
            tvReportGroup.OptionsBehavior.ReadOnly = False
        End If
    End Sub

    Private Sub tsmi_ReportInsert_Click(sender As Object, e As EventArgs) Handles tsmi_ReportInsert.Click
        If (cmbReportGroup.SelectedIndex > 0) Then
            tvReportGroup.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim reportCategoryID As String = "0"
            Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                If (tvReportGroup.FocusedNode IsNot Nothing) Then
                    If (tvReportGroup.FocusedNode.Level = 1) Then
                        reportCategoryID = tvReportGroup.FocusedNode.Tag.ToString
                    ElseIf (tvReportGroup.FocusedNode.Level = 2) Then
                        reportCategoryID = tvReportGroup.FocusedNode.ParentNode.Tag.ToString
                    End If
                End If

                Dim reportGroupID As String = TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value

                Dim frmNewReport As New dlgSBGroupInsert()
                frmNewReport.GroupTypeInserting = GroupType.SandboxReport
                frmNewReport.reportCategoryID = reportCategoryID
                frmNewReport.reportGroupID = reportGroupID
                frmNewReport.SetConnectionString(connStrSandBoxServer)
                frmNewReport.ShowDialog()

                RefreshReportGroup_TreeList()
                RefreshDashboardReport()
                RefreshSchedulerReport()
                tvReportGroup.SetFocusedNode(tvReportGroup.FindNodeByFieldValue("ReportGroupName", frmNewReport.reportName))
                IsReportGroupMouseDownRight = False
                tvReportGroup_MouseUp(Nothing, Nothing)
                EnableDisableReportControls(True)

            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            Finally
                tvReportGroup.Cursor = Cursors.Default
                Application.DoEvents()
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End Try
        End If
    End Sub

    Private Sub tsmi_ReportDelete_Click(sender As Object, e As EventArgs) Handles tsmi_ReportDelete.Click
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            Me.UseWaitCursor = True
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Try
                If (tvReportGroup.FocusedNode.Level = 2) Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.DeleteReports(tvReportGroup.FocusedNode.Tag.ToString, tvReportGroup.FocusedNode.ParentNode.Tag.ToString))
                    RefreshReportGroup_TreeList()
                    RefreshDashboardReport()
                Else
                    SetMessage("Select any Node from tree")
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            Finally
                Me.UseWaitCursor = False
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End Try
        End If
    End Sub

    Private Sub tsmi_ReportRename_Click(sender As Object, e As EventArgs) Handles tsmi_ReportRename.Click
        If (tvReportGroup.FocusedNode.Level = 2) Then
            reportName = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
            tvReportGroup.OptionsBehavior.Editable = True
            tvReportGroup.OptionsBehavior.ReadOnly = False
        End If
    End Sub

    Private Sub tsmi_ReportEdit_Click(sender As Object, e As EventArgs) Handles tsmi_ReportEdit.Click
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            RemoveHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Try
                If (tvReportGroup.FocusedNode.Level = 2) Then
                    'If rbExport.Checked = True Then
                    '    Dim reportID As String = tvReportGroup.FocusedNode.Tag.ToString
                    '    dtReportExport = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportChart.GetReportChartData(reportID))
                    '    If dtReportExport IsNot Nothing AndAlso dtReportExport.Rows.Count > 0 Then
                    '        reportConnString = dtReportExport.Rows(0)("ReportConnString").ToString
                    '        expReportID = CInt(dtReportExport.Rows(0)("ReportID"))
                    '        reportSQL = dtReportExport.Rows(0)("ReportSQL").ToString
                    '        Dim objFileDlg As New SaveFileDialog()
                    '        objFileDlg.Filter = "Comma Delimited|*.csv"
                    '        objFileDlg.Title = "Save a CSV File"

                    '        If objFileDlg.ShowDialog() = DialogResult.OK Then
                    '            If objFileDlg.FileName <> "" Then
                    '                reportExportFile = objFileDlg.FileName
                    '                Process_ReportExportAppend()
                    '            End If
                    '        End If
                    '    End If
                    'Else
                    ' clear already loaded report data
                    IOS.Library.ReportChartGrid.reportAbort = False
                    flp_ValueX.Controls.Clear()
                    flp_ValueY.Controls.Clear()
                    RefreshChartSeriesTLV(tlvSandboxChartsSeries)
                    'cmbReportTechnology.SelectedIndex = 0

                    ' start loading report
                    dmWaitScreen.ShowDataMartWaitScreen("Report Getting data")
                    Me.SanboxReportTreeSelectionType = ReportSelectionType.Report
                    btnFilter.Enabled = True
                    Dim reportId As String = tvReportGroup.FocusedNode.Tag.ToString
                    lblSelectedReport.Text = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
                    lblReportMode.Text = "EDIT MODE (Report Locked by Owner)"
                    lblReportMode.ForeColor = Color.Red

                    ' enabling report edit controls
                    grpCountersAndKPI.Enabled = True

                    GetFieldByReportId(reportId)
                    EnableDisableReportControls(True)

                    If ReportIDOwner = Environment.UserName.ToString Then
                        btnCommit.Enabled = True
                    Else
                        btnCommit.Enabled = False
                    End If

                    ' clear KPI Configuration
                    'If objDMKpiConfig Is Nothing Then
                    '    objDMKpiConfig = New frmDatamartKpiConfig()
                    'End If
                    'objDMKpiConfig.txtKPIFormula.Text = ""
                    'objDMKpiConfig.txtKPIName.Text = ""
                    'objDMKpiConfig.btnCommitKPI.Enabled = True
                    'objDMKpiConfig.Show()

                    dtChartConfigSandbox = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportChart.GetReportChartData(reportId))
                    Dim dsReportAxisData As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLReportChart.GetReportAxisData(reportId) & SQLReportContentFilter.GetReportContentFilter(reportId))
                    dtReportAxisData = dsReportAxisData.Tables(0)
                    dtReportFilterData = dsReportAxisData.Tables(1)
                    If (dsReportAxisData.Tables(1).Rows.Count > 0) Then
                        btnFilter.LookAndFeel.SetSkinStyle("Caramel")
                    Else
                        btnFilter.LookAndFeel.SetSkinStyle("Office 2010 Black")
                    End If

                    If (dtChartConfigSandbox.IsValid) Then
                        GetChartConfigData(dtChartConfigSandbox)
                        'If (dtReportAxisData.IsValid) Then
                        '    BindChart(dtChartConfigSandbox, True)
                        'End If
                    End If
                    tvObjects.Nodes.Clear()
                    tvObjects.Refresh()
                    'End If
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            Finally
                dmWaitScreen.CloseDataMartWaitScreen()
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End Try
            AddHandler tvReportGroup.MouseUp, AddressOf tvReportGroup_MouseUp
        End If
    End Sub

    Private Sub tsmi_ReportCopy_Click(sender As Object, e As EventArgs) Handles tsmi_ReportCopy.Click
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            Me.UseWaitCursor = True
            Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                Dim treenode As TreeListNode = tvReportGroup.FocusedNode
                If (tvReportGroup.FocusedNode.Level = 2) Then
                    If (treenode.Level = 2) Then
                        Dim reportId As String = tvReportGroup.FocusedNode.Tag.ToString
                        Dim reportName As String = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
                        Dim reportGroupID As String = TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.CopyReport(reportGroupID, reportId, reportName & "_Copy"))
                        RefreshReportGroup_TreeList()
                        RefreshDashboardReport()
                        RefreshSchedulerReport()
                    Else
                        SetMessage("Select any Node from tree")
                    End If
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            Finally
                Me.UseWaitCursor = False
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End Try
        End If
    End Sub

    Private Sub tsmi_ExpandAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_ExpandAll.Click
        tvReportGroup.ExpandAll()
    End Sub

    Private Sub tsmi_CollapseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_CollapseAll.Click
        If (tvReportGroup.Nodes.Count > 0 AndAlso tvReportGroup.Nodes(0).Nodes.Count > 0) Then
            tvReportGroup.CollapseAll()
        End If
    End Sub

    Private Sub tsmi_ReportGroupDelete_Click(sender As Object, e As EventArgs) Handles tsmi_ReportGroupDelete.Click
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            Try
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                If (tvReportGroup.FocusedNode.Level = 0) Then
                    Dim reportGroupId As String = tvReportGroup.FocusedNode.Tag
                    Dim recordAffected As Integer = DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportGroups.DeleteGroup(reportGroupId, System.Environment.UserName))
                    If recordAffected > 0 Then
                        BindReportGroup()
                        RefreshReportGroup_TreeList()
                        RefreshDashboardReport()
                    Else
                        SetMessage("Only group owner can delete the group.")
                    End If
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            Finally
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            End Try
        End If
    End Sub

    Private Sub tsmi_ReportGroupRename_Click(sender As Object, e As EventArgs) Handles tsmi_ReportGroupRename.Click
        If (tvReportGroup.FocusedNode.Level = 0) Then
            tvReportGroup.OptionsBehavior.Editable = True
            tvReportGroup.OptionsBehavior.ReadOnly = False
        End If
    End Sub

    Private Sub tsmi_ReportGroupStatusPublic_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_ReportGroupStatusPublic.CheckedChanged
        If (isByClick) Then
            Me.UseWaitCursor = True
            If (tvReportGroup.FocusedNode IsNot Nothing) Then
                Try
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                    If (tvReportGroup.FocusedNode.Level = 0) Then
                        Dim reportGroupId As String = tvReportGroup.FocusedNode.Tag
                        Dim reportGroupName As String = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
                        Dim isPrivate As Boolean = IIf(tsmi_ReportGroupStatusPublic.Checked, False, True)
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportGroups.ModifyGroup(reportGroupId, reportGroupName, isPrivate, System.Environment.UserName))
                        isByClick = False
                        If (tsmi_ReportGroupStatusPublic.Checked) Then
                            tsmi_ReportGroupStatusPrivate.Checked = False
                        Else
                            tsmi_ReportGroupStatusPrivate.Checked = True
                            tsmi_ReportGroupStatusPublic.Checked = False
                        End If
                        isByClick = True
                        RefreshReportGroup_TreeList()
                    End If
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                Finally
                    Me.UseWaitCursor = False
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
                End Try
            End If
        End If
    End Sub

    Private Sub tsmi_ReportGroupStatusPrivate_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_ReportGroupStatusPrivate.CheckedChanged
        If (isByClick) Then
            Me.UseWaitCursor = True
            If (tvReportGroup.FocusedNode IsNot Nothing) Then
                Try
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                    If (tvReportGroup.FocusedNode.Level = 0) Then
                        Dim reportGroupId As String = tvReportGroup.FocusedNode.Tag
                        Dim reportGroupName As String = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
                        Dim isPrivate As Boolean = IIf(tsmi_ReportGroupStatusPrivate.Checked, True, False)
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportGroups.ModifyGroup(reportGroupId, reportGroupName, isPrivate, System.Environment.UserName))
                        isByClick = False
                        If (tsmi_ReportGroupStatusPrivate.Checked) Then
                            tsmi_ReportGroupStatusPublic.Checked = False
                        Else
                            tsmi_ReportGroupStatusPrivate.Checked = False
                            tsmi_ReportGroupStatusPublic.Checked = True
                        End If
                        isByClick = True
                        RefreshReportGroup_TreeList()
                    End If
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                Finally
                    Me.UseWaitCursor = False
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
                End Try
            End If
        End If
    End Sub

    Private Sub tvReportGroup_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles tvReportGroup.CellValueChanged
        Dim reportNode As TreeListNode = e.Node
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        If (e.Value IsNot Nothing) Then
            Try
                Dim selectedNodeId As String = tvReportGroup.FocusedNode.Tag
                If (reportNode.Level = 0) Then
                    Dim isPrivate As Boolean = IIf(tsmi_ReportGroupStatusPrivate.Checked, True, False)
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportGroups.ModifyGroup(selectedNodeId, e.Value, isPrivate, System.Environment.UserName))
                    isReportGroupSelectedIndexChanged = False
                    BindReportGroup()

                    Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(selectedNodeId, cmbReportGroup)
                    cmbReportGroup.SelectedItem = cmbItem
                    isReportGroupSelectedIndexChanged = True
                    RefreshDashboardReport()
                    RefreshSchedulerReport()
                    reportNode.Item("ReportGroupName") = e.Value.ToString
                ElseIf (reportNode.Level = 1) Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportCategory.ModifyReportCategory(selectedNodeId, e.Value))
                    RefreshDashboardReport()
                    RefreshSchedulerReport()
                    reportNode.Item("ReportGroupName") = e.Value.ToString
                ElseIf (reportNode.Level = 2) Then
                    Dim reportGroupID As String = TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value
                    Dim countAffected As Integer = DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.ModifyReports(reportGroupID, selectedNodeId, e.Value, System.Environment.UserName))
                    If countAffected > 0 Then
                        lblSelectedReport.Text = e.Value.ToString
                        RefreshDashboardReport()
                        RefreshSchedulerReport()
                        reportNode.Item("ReportGroupName") = e.Value.ToString
                    Else
                        SetMessage("Report name already exists, try another name.")
                        reportNode.Item("ReportGroupName") = reportName
                    End If
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            Finally
                tvReportGroup.OptionsBehavior.Editable = False
                tvReportGroup.OptionsBehavior.ReadOnly = True
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            End Try
        End If
    End Sub

#End Region

#Region "Object Tree Context Menu"

    Private Sub tsmi_ObjectCopy_Click(sender As Object, e As EventArgs) Handles tsmi_ObjectCopy.Click
        Clipboard.Clear()
        Dim tv As TreeList = cm_OT_SourceControl
        Dim tech As String = cmbReportTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbObjectType.SelectedItem.ToString
        Try
            Dim copystring As String = GetChecked2String(tvObjects, tech, aggr_to, "ObjectCopy")
            copystring = copystring.Replace(",,", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            tv.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Function GetChecked2String(ByRef tree As TreeList, ByVal tech As String, ByVal aggr_to As String, ByVal outputtype As String) As String
        Dim nodelevel As Integer
        Dim outputstr As New System.Text.StringBuilder()
        If outputtype = "ObjectNameWild" Then
            outputstr.Append(" LIKE ")
        ElseIf outputtype = "Naked" Then
            outputstr.Append("")
        ElseIf outputtype = "TAGS_CM" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectType" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectCopy" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectSQL" Then
            outputstr.Append("")
        Else
            outputstr.Append("IN (")
        End If
        nodelevel = 3

        nodelevel = cmbObjectType.Properties.Items.Count - cmbObjectType.SelectedIndex - 1 ''GetNodeLevelByObjectType(tech, aggr_to)
        For Each nd As TreeListNode In tree.Nodes
            outputstr.Append(GetChecked_2_StringByLevel(tree, nd, nodelevel, outputtype))
        Next

        Dim outputfinal As String = Nothing
        If outputtype = "ObjectNameWild" Then
            outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
        ElseIf outputtype = "Naked" Or outputtype = "ObjectType" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "TAGS_CM" Then
            outputfinal = outputstr.ToString.Substring(0, Len(outputstr.ToString) - 4)
        ElseIf outputtype = "ObjectCopy" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "ObjectSQL" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        Else
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
        End If

        Return outputfinal
    End Function

    Private Function GetChecked_2_StringByLevel(ByRef tree As TreeList, ByVal nd As TreeListNode, ByVal level As Integer, ByVal outputtype As String) As String
        Dim Result As String = ""

        If nd.Checked = True And nd.Level = level And outputtype = "ObjectName" Then
            Result = Result & Chr(39) & nd.Item("ObjectName") & Chr(39) & ","
        ElseIf nd.Checked And nd.Level = level And outputtype = "ObjectNameSplit" Then
            Result = Result & Chr(39) & Split(nd.Item("ObjectName"), "-")(0).Trim.Substring(0, 5) & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectID" Then
            Result = Result & Chr(39) & nd.Item("ObjectID").ToString.Replace("'", "''") & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectCopy" Then
            Result = Result & Chr(39) & nd.Item("ObjectName") & ControlChars.Tab & nd.Tag.ToString.Replace("'", "''") & Chr(39) & ",,"
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectSQL" Then
            Result = Result & Chr(39) & nd.Tag.ToString.Replace("'", "''") & Chr(39) & ",,"
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectType" Then
            Result = Result & nd.Item("ObjectType") & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "TAGS_CM" Then
            Result = Result & nd.Item("ObjectName") & " OR "
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "Naked" Then
            Result = Result & nd.Item("ObjectName") & ","
        End If

        Dim N As TreeListNode
        For Each N In nd.Nodes
            Result = Result & GetChecked_2_StringByLevel(tree, N, level, outputtype)
        Next
        N = Nothing
        Return Result
    End Function

    Private Sub tsmi_ObjectPaste_Click(sender As Object, e As EventArgs) Handles tsmi_ObjectPaste.Click
        Dim tv As TreeList = cm_OT_SourceControl
        Dim tech As String = cmbReportTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbObjectType.SelectedItem.ToString

        tv.Cursor = Cursors.WaitCursor
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim ExactMatch As Boolean = True
            If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                ExactMatch = False
            Else
                ExactMatch = True
            End If

            Dim s As String = Clipboard.GetText()
            Dim rows() As String = s.Split(ControlChars.NewLine)
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
                        If bufferCell(j).ToString.Contains("'") Then
                            bufferCell(j) = bufferCell(j).ToString.Replace("'", "")
                        End If
                        Dim tv_result As TreeListNode = tv.FindNodeByFieldValue("ObjectName", bufferCell(j).Trim) 'Treeview_TextSearch(bufferCell(j).Trim, tv.Nodes, ExactMatch)
                        If Not tv_result Is Nothing Then
                            tv_result.Checked = True
                            tv.CheckParentNode(tv_result)
                        End If
                    Next
                Next
            End If
            tv.Cursor = Cursors.Arrow
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            tv.Cursor = Cursors.Arrow
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            tv.Cursor = Cursors.Arrow
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub tsmi_UncheckedAll_Click(sender As Object, e As EventArgs) Handles tsmi_UncheckedAll.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
        Try
            Dim tv As TreeList = cm_OT_SourceControl
            tv.UncheckAll()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_CheckAllFiltered_Click(sender As Object, e As EventArgs) Handles tsmi_CheckAllFiltered.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
        Try
            Dim tv As TreeList = cm_OT_SourceControl
            Dim filterText As String = tv.FindFilterText
            Dim nds As List(Of TreeListNode) = tv.GetNodeList()

            For Each nod As TreeListNode In nds
                If nod.GetDisplayText("ObjectName").ToLower.Contains(filterText) Then
                    nod.Checked = True
                Else
                    nod.Checked = False
                End If
            Next
            If tv.GetAllCheckedNodes().Count > 1000 Then
                SetMessage("Objects in a report cannot go beyond 1000")
                tv.UncheckAll()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_MapSelection_Click(sender As Object, e As EventArgs) Handles tsmi_MapSelection.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            frmMapWindow.Cells_SearchAndDisplay_DataMart(GetChecked2String(tvObjects, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, cmbObjectType.SelectedItem.ToString, "Naked"), tvObjects.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Public Sub TreeView_ClearChecks(ByVal nd As TreeListNode)
        For Each node As TreeListNode In nd.Nodes
            Try
                If node.Checked = True Then
                    node.Checked = False
                    TreeView_ClearChecks(node)
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try
        Next
    End Sub

    Public Function TreeView_Checked2String(ByVal tech As String, ByVal aggr_to As String, ByVal outputtype As String, Optional ByVal isFromParameterpage As Boolean = False) As String
        Dim nodelevel As Integer
        Dim outputstr As New System.Text.StringBuilder()

        If outputtype = "ObjectNameWild" Then
            outputstr.Append(" LIKE ")
        ElseIf outputtype = "Naked" Then
            outputstr.Append("")
        ElseIf outputtype = "TAGS_CM" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectType" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectCopy" Then
            outputstr.Append("")
        ElseIf outputtype = "ObjectSQL" Then
            outputstr.Append("")
        Else
            outputstr.Append("IN (")
        End If
        nodelevel = 3
        nodelevel = cmbObjectType.Properties.Items.Count - cmbObjectType.SelectedIndex '' - 1
        For Each nd As TreeNode In tvObjects.Nodes
            outputstr.Append(TreeView_Checked2String_Level(nd, nodelevel, outputtype))
        Next
        Dim outputfinal As String = Nothing
        If outputtype = "ObjectNameWild" Then
            outputfinal = Mid(outputstr.ToString, 1, outputstr.ToString.Length - 9)
        ElseIf outputtype = "Naked" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "TAGS_CM" Then
            outputfinal = outputstr.ToString.Substring(0, Len(outputstr.ToString) - 5)
        ElseIf outputtype = "ObjectCopy" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        ElseIf outputtype = "ObjectSQL" Then
            outputfinal = outputstr.ToString.TrimEnd(",")
        Else
            outputfinal = outputstr.ToString.TrimEnd(",") + ")"
        End If

        Return outputfinal
    End Function

    Public Function TreeView_Checked2String_Level(ByVal nd As TreeNode, ByVal level As Integer, ByVal outputtype As String) As String

        Dim Result As String = ""
        If nd.Checked = True And nd.Level = level And outputtype = "ObjectName" Then
            Result = Result & Chr(39) & nd.Text & Chr(39) & ", "
        ElseIf nd.Checked And nd.Level = level And outputtype = "ObjectNameSplit" Then
            Result = Result & Chr(39) & Split(nd.Text, "-")(0).Trim.Substring(0, 5) & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectID" Then
            Result = Result & Chr(39) & nd.Tag.ToString.Replace("'", "''") & Chr(39) & ","
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectCopy" Then
            Result = Result & Chr(39) & nd.Text & ControlChars.Tab & nd.Tag.ToString.Replace("'", "''") & Chr(39) & ",,"
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectSQL" Then
            Result = Result & Chr(39) & nd.Tag.ToString.Replace("'", "''") & Chr(39) & ",,"
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "ObjectType" Then
            Result = Result & nd.ImageKey & ", "

        ElseIf nd.Checked = True And nd.Level = level And outputtype = "TAGS_CM" Then
            Result = Result & nd.Text & " AND "
        ElseIf nd.Checked = True And nd.Level = level And outputtype = "Naked" Then
            Result = Result & nd.Tag & ","
        End If

        Dim N As TreeNode
        For Each N In nd.Nodes
            Result = Result & TreeView_Checked2String_Level(N, level, outputtype)
        Next
        N = Nothing
        Return Result

    End Function

#End Region

    Private Sub cm_FLPValueXY_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cm_FLPValueXY.Opening
        Try
            Dim cmsTemp As ContextMenuStrip = CType(sender, ContextMenuStrip)
            Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(cmsTemp.SourceControl, DevExSandBoxField)
            If (vSandBoxFieldSelected IsNot Nothing) Then
                clickedSandBoxSourceControl = vSandBoxFieldSelected

                If (vSandBoxFieldSelected.SortValue.ToUpper = "NONE") Then
                    tsmi_FLPValueXY_SortOrderNone.Checked = True
                ElseIf (vSandBoxFieldSelected.SortValue.ToUpper = "ASC") Then
                    tsmi_FLPValueXY_SortOrderASC.Checked = True
                ElseIf (vSandBoxFieldSelected.SortValue.ToUpper = "DESC") Then
                    tsmi_FLPValueXY_SortOrderDESC.Checked = True
                End If

                tsmi_FLPValueXY_TimeAggregation.Visible = False
                tsmi_FLPValueXY_ObjectAggregation.Visible = False
                tsmi_FLPValueXY_AddStatistics.Visible = False
                tsmi_FLPValueXY_AddThreshold.Visible = False
                tsmi_FLPValueXY_SortOrder.Visible = False

                If (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Counter) Then
                    tsmi_FLPValueXY_AddStatistics.Visible = True
                    tsmi_FLPValueXY_TimeAggregation.Visible = True
                    tsmi_FLPValueXY_ObjectAggregation.Visible = True
                    tsmi_FLPValueXY_AddThreshold.Visible = True
                    tsmi_FLPValueXY_SortOrder.Visible = True
                ElseIf (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Kpi) Then
                    tsmi_FLPValueXY_AddStatistics.Visible = True
                    tsmi_FLPValueXY_AddThreshold.Visible = True
                    tsmi_FLPValueXY_SortOrder.Visible = True
                ElseIf (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.ObjectFld) Then
                    tsmi_FLPValueXY_SortOrder.Visible = True
                    tsmi_FLPValueXY_SortOrder.Visible = True
                ElseIf (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Time) Then
                    ''tsmi_FLPValueXY_SortOrder.Visible = True
                    ''tsmi_FLPValueXY_AddStatistics.Visible = False
                    ''tsmi_FLPValueXY_AddThreshold.Visible = False
                End If
            End If

            ''If (tsmi_FLPValueXY_AddStatistics.Enabled) Then
            If (isFirstTimeCalculatedSeriesTypes) Then
                'Dim SQLThresholdType As String = New SQLThresholdTypes().SelectAll()
                Dim SQLCalculatedSeriesType As String = New SQLCalculatedSeriesTypes().SelectAll()
                Dim dtTypes As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLCalculatedSeriesType)

                Dim calculatedSeriesTypesDt As DataTable = dtTypes.SelectedRowsAsTable(CalculatedSeriesTypesFields.StatisticsOrThreshold, OperatorConst.Equal, StatisticsOrThreshold.Statistics.ToString)
                Dim thresholdTypeDt As DataTable = dtTypes.SelectedRowsAsTable(CalculatedSeriesTypesFields.StatisticsOrThreshold, OperatorConst.Equal, StatisticsOrThreshold.Threshold.ToString)
                Dim tsmiCalculatedSeriesTypes As ToolStripMenuItem
                For Each dr As DataRow In calculatedSeriesTypesDt.Rows
                    tsmiCalculatedSeriesTypes = New ToolStripMenuItem()
                    tsmiCalculatedSeriesTypes.Text = dr(CalculatedSeriesTypesFields.Calculated_Series_Type_Name)
                    tsmiCalculatedSeriesTypes.ToolTipText = IsDBNull(dr(CalculatedSeriesTypesFields.Calculated_Series_Type_Parameters))
                    tsmiCalculatedSeriesTypes.Tag = dr(CalculatedSeriesTypesFields.Calculated_Series_Type_ID)
                    AddHandler tsmiCalculatedSeriesTypes.Click, AddressOf tsmiCalculatedSeriesTypes_Click
                    tsmi_FLPValueXY_AddStatistics.DropDownItems.Add(tsmiCalculatedSeriesTypes)
                Next

                Dim tsmiThresholdType As ToolStripMenuItem
                For Each dr As DataRow In thresholdTypeDt.Rows
                    tsmiThresholdType = New ToolStripMenuItem()
                    tsmiThresholdType.Text = dr(CalculatedSeriesTypesFields.Calculated_Series_Type_Name)
                    tsmiThresholdType.ToolTipText = IsDBNull(dr(CalculatedSeriesTypesFields.Calculated_Series_Type_Parameters))
                    tsmiThresholdType.Tag = dr(CalculatedSeriesTypesFields.Calculated_Series_Type_ID)
                    AddHandler tsmiThresholdType.Click, AddressOf tsmiThresholdType_Click
                    tsmi_FLPValueXY_AddThreshold.DropDownItems.Add(tsmiThresholdType)
                Next

                isFirstTimeCalculatedSeriesTypes = False
            End If
            ''End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tsmiCalculatedSeriesTypes_Click(sender As Object, e As EventArgs)
        Try
            Dim tsmiTemp As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim isCalculatedSeriesTypeParameters As Boolean = False
            If (clickedSandBoxSourceControl IsNot Nothing) Then

                Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(clickedSandBoxSourceControl, DevExSandBoxField)
                If (vSandBoxFieldSelected IsNot Nothing) Then
                    If (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Counter Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Kpi) Then
                        If (tsmiTemp.ToolTipText = "False") Then
                            isCalculatedSeriesTypeParameters = True
                        End If
                        Dim calculatedSeriesTypeParameters As String = String.Empty
                        If (isCalculatedSeriesTypeParameters) Then

                            Dim dialogCalculatedSeriesNew As New dlgSBCalculatedSeriesNew()
                            dialogCalculatedSeriesNew.SetConnectionString(connStrSandBoxServer)
                            dialogCalculatedSeriesNew.StatisticsOrThresholdType = StatisticsOrThreshold.Statistics
                            dialogCalculatedSeriesNew.CalculatedSeriesTypeID = tsmiTemp.Tag
                            dialogCalculatedSeriesNew.ShowDialog()

                            If dialogCalculatedSeriesNew.DialogResult = DialogResult.Abort Or dialogCalculatedSeriesNew.DialogResult = DialogResult.Cancel Then
                                Exit Sub
                            End If

                            calculatedSeriesTypeParameters = dialogCalculatedSeriesNew.CalculatedSeriesTypeParameters

                            If (calculatedSeriesTypeParameters.Length > 0) Then
                                SetChartConfigurationDefaultValueCalculatedSeries(vSandBoxFieldSelected.Text, tsmiTemp.Text, tsmiTemp.Tag, calculatedSeriesTypeParameters)
                            Else
                                SetMessage("No Calculated Series Type Parameters Value.")
                                Return
                            End If
                        Else
                            SetChartConfigurationDefaultValueCalculatedSeries(vSandBoxFieldSelected.Text, tsmiTemp.Text, tsmiTemp.Tag, calculatedSeriesTypeParameters)
                        End If
                        RefreshChartSeriesTLV(tlvSandboxChartsSeries)

                        If ceAutoRefreshChart.Checked = True Then
                            RefreshChartAndGrid(vSandBoxFieldSelected.Text & "_" & tsmiTemp.Text, False)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tsmi_FLPValueXY_RemoveField_Click(sender As Object, e As EventArgs) Handles tsmi_FLPValueXY_RemoveField.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tsmiTemp As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim cmsTemp As ContextMenuStrip = CType(tsmiTemp.Owner, ContextMenuStrip)
            Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(cmsTemp.SourceControl, DevExSandBoxField)
            If (vSandBoxFieldSelected IsNot Nothing) Then
                Dim parentFlowLayoutPanel As FlowLayoutPanel = TryCast(vSandBoxFieldSelected.Parent, FlowLayoutPanel)
                If (parentFlowLayoutPanel IsNot Nothing) Then
                    If (parentFlowLayoutPanel.Controls.Count > 0) Then
                        flp_RemoveSandbox(vSandBoxFieldSelected.Text, parentFlowLayoutPanel)

                        If vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Counter Then
                            For Each clbItm As DataRowView In lstTechCounter.CheckedItems
                                If clbItm.Item("CounterName") = vSandBoxFieldSelected.Text Then
                                    Dim index = lstTechCounter.FindStringExact(vSandBoxFieldSelected.Text)
                                    lstTechCounter.SetItemCheckState(index, CheckState.Unchecked)
                                    lstTechCounter.SetSelected(index, False)
                                    lstTechCounterCheckedItems.Remove(clbItm)
                                End If
                            Next
                        ElseIf vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Kpi Then
                            Dim nd = lstTechKPI.FindNodeByFieldValue(KPIGroupFields.KPI_CATEGORY_NAME, vSandBoxFieldSelected.Text)
                            If nd IsNot Nothing Then
                                nd.SetValue("riChkEdit", False)
                                viewCheckedKPINameList.Remove(vSandBoxFieldSelected.Text)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiThresholdType_Click(sender As Object, e As EventArgs)
        Try
            Dim tsmiTemp As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim isThresholdTypeParameters As Boolean = False
            If (clickedSandBoxSourceControl IsNot Nothing) Then

                Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(clickedSandBoxSourceControl, DevExSandBoxField)
                If (vSandBoxFieldSelected IsNot Nothing) Then
                    If (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Counter Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Kpi) Then
                        If (tsmiTemp.ToolTipText = "False") Then
                            isThresholdTypeParameters = True
                        End If
                        Dim thresholdTypeParameters As String = String.Empty
                        If (isThresholdTypeParameters) Then

                            Dim dialogCalculatedSeriesNew As New dlgSBCalculatedSeriesNew()
                            dialogCalculatedSeriesNew.SetConnectionString(connStrSandBoxServer)
                            dialogCalculatedSeriesNew.StatisticsOrThresholdType = StatisticsOrThreshold.Threshold
                            dialogCalculatedSeriesNew.CalculatedSeriesTypeID = tsmiTemp.Tag
                            dialogCalculatedSeriesNew.ShowDialog()
                            thresholdTypeParameters = dialogCalculatedSeriesNew.CalculatedSeriesTypeParameters

                            If (thresholdTypeParameters.Length > 0) Then
                                SetChartConfigurationDefaultValueCalculatedSeries(vSandBoxFieldSelected.Text, tsmiTemp.Text, tsmiTemp.Tag, thresholdTypeParameters)
                            Else
                                SetMessage("No Calculated Series Type Parameters Value.")
                                Return
                            End If
                        Else
                            ''   SetChartConfigurationDefaultValueCalculatedSeries(vSandBoxFieldSelected.Text, tsmiTemp.Text, tsmiTemp.Tag, calculatedSeriesTypeParameters)
                        End If
                        RefreshChartSeriesTLV(tlvSandboxChartsSeries)

                        If ceAutoRefreshChart.Checked = True Then
                            RefreshChartAndGrid(vSandBoxFieldSelected.Text & "_" & tsmiTemp.Text, False)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tsmi_FLPValueXY_RemoveAllFields_Click(sender As Object, e As EventArgs) Handles tsmi_FLPValueXY_RemoveAllFields.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tsmiTemp As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim cmsTemp As ContextMenuStrip = CType(tsmiTemp.Owner, ContextMenuStrip)
            Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(cmsTemp.SourceControl, DevExSandBoxField)
            If (vSandBoxFieldSelected IsNot Nothing) Then
                Dim parentFlowLayoutPanel As FlowLayoutPanel = TryCast(vSandBoxFieldSelected.Parent, FlowLayoutPanel)
                If (parentFlowLayoutPanel IsNot Nothing) Then
                    If (parentFlowLayoutPanel.Controls.Count > 0) Then

                        lstTechCounter.UnCheckAll()
                        lstTechCounterCheckedItems.Clear()
                        checkedCounter.Clear()
                        lstTechKPI.UncheckAll()
                        checkedKPI.Clear()
                        checkedKPINameList.Clear()
                        lstTechKPICheckedItems.Clear()
                        viewCheckedKPINameList.Clear()
                        viewCheckedKPIsOnly = False
                        RefreshKPITree()
                        parentFlowLayoutPanel.Controls.Clear()
                        tlvSandboxChartsSeries.Nodes.Clear()
                        reportChartGrid_SendBox.ClearData()
                        dtChartConfigSandbox.Clear()
                        SetMessage("Removed All VSandbox")

                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmit_FLPValueXY_SortOrderNone_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_FLPValueXY_SortOrderNone.CheckedChanged
        Try
            If (tsmi_FLPValueXY_SortOrderNone.Checked) Then
                tsmi_FLPValueXY_SortOrderASC.Checked = False
                tsmi_FLPValueXY_SortOrderDESC.Checked = False
                If (clickedSandBoxSourceControl IsNot Nothing) Then
                    Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(clickedSandBoxSourceControl, DevExSandBoxField)
                    If (vSandBoxFieldSelected IsNot Nothing) Then
                        If (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Counter Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Kpi Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.ObjectFld) Then
                            vSandBoxFieldSelected.SortValue = tsmi_FLPValueXY_SortOrderNone.Tag
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tsmit_FLPValueXY_SortOrderASC_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_FLPValueXY_SortOrderASC.CheckedChanged
        Try
            If (tsmi_FLPValueXY_SortOrderASC.Checked) Then
                tsmi_FLPValueXY_SortOrderDESC.Checked = False
                tsmi_FLPValueXY_SortOrderNone.Checked = False
                If (clickedSandBoxSourceControl IsNot Nothing) Then
                    Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(clickedSandBoxSourceControl, DevExSandBoxField)
                    If (vSandBoxFieldSelected IsNot Nothing) Then
                        If (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Counter Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Kpi Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.ObjectFld) Then
                            vSandBoxFieldSelected.SortValue = tsmi_FLPValueXY_SortOrderASC.Tag
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tsmit_FLPValueXY_SortOrderDESC_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_FLPValueXY_SortOrderDESC.CheckedChanged
        Try
            If (tsmi_FLPValueXY_SortOrderDESC.Checked) Then
                tsmi_FLPValueXY_SortOrderASC.Checked = False
                tsmi_FLPValueXY_SortOrderNone.Checked = False
                If (clickedSandBoxSourceControl IsNot Nothing) Then
                    Dim vSandBoxFieldSelected As DevExSandBoxField = TryCast(clickedSandBoxSourceControl, DevExSandBoxField)
                    If (vSandBoxFieldSelected IsNot Nothing) Then
                        If (vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Counter Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.Kpi Or vSandBoxFieldSelected.VSandBoxType = DatamartFieldType.ObjectFld) Then
                            vSandBoxFieldSelected.SortValue = tsmi_FLPValueXY_SortOrderDESC.Tag
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

    Private Sub btnAddReport_Click(sender As Object, e As EventArgs) Handles btnAddReport.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            tsmi_ReportInsert_Click(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
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

    Function GetNodeFromPath(ByVal nodes As Nodes.TreeListNodes, ByVal path As String) As TreeListNode
        Dim foundNode As TreeListNode = Nothing
        If (nodes Is Nothing Or String.IsNullOrEmpty(path)) Then
            Return foundNode
        End If
        For Each tn As TreeListNode In nodes
            'If (tn.FullPath = path) Then
            '    tvReportGroup.FocusedNode = tn
            '    tvReportGroup.FocusedNode.Visible = True
            '    tvReportGroup.Focus()
            '    Return tn
            'ElseIf (tn.Nodes.Count > 0) Then
            '    foundNode = GetNodeFromPath(tn.Nodes, path)
            'End If
            If (foundNode IsNot Nothing) Then
                Return foundNode
            End If
        Next
        Return Nothing
    End Function

#Region "DragDrop"

    Private Sub flp_ValueX_GiveFeedback(sender As Object, e As GiveFeedbackEventArgs)
        e.UseDefaultCursors = False
        Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub flp_RemoveSandbox(ByVal objectText As String, ByRef parentFlowLayoutPanel As FlowLayoutPanel)
        For Each vSanBox As DevExSandBoxField In parentFlowLayoutPanel.Controls
            If (vSanBox.Text.ToUpper = objectText.ToUpper) Then
                Dim seriesName As String = objectText.ToUpper
                parentFlowLayoutPanel.Controls.Remove(vSanBox)
                RemoveChartConfigSetting(seriesName)

                Try
                    Dim dt As DataTable = dtChartConfigSandbox.AsEnumerable().Where(Function(a) a(ReportChartFields.SeriesName).ToString.ToUpper <> seriesName.ToUpper).CopyToDataTable()
                    dtChartConfigSandbox = dt
                Catch ex As Exception
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                End Try
                If (vSanBox.VSandBoxType = "4" Or vSanBox.VSandBoxType = "3") Then
                    dtReportAxisData.Rows.Clear()
                End If

                If ceAutoRefreshChart.Checked = True Then
                    reportChartGrid_SendBox.ClearData()
                    RefreshChartAndGrid(objectText, True)
                End If

                SetMessage(" Removed Selected VSandbox ")
                Return
            End If
        Next
    End Sub

    Private Sub flp_AddVsandBox(ByVal dragedObjectText As String, ByVal dragedObjectID As String, ByVal dragedObjectType As String, ByRef flowLayout As FlowLayoutPanel, Optional ByVal flpCtrlTag As String = Nothing)

        Dim vSandBoxFieldModel As New EntityModel.SandBoxFieldModel()
        Dim vSandBoxField As DevExSandBoxField = New DevExSandBoxField()
        vSandBoxField.LookAndFeel.SetSkinStyle("Office 2010 Black")

        If (dragedObjectType = "1") Then
            vSandBoxFieldModel = SandBoxTable.GetVSandBoxObjectForCounter(dt_TechPackCounter, TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & dragedObjectID)
            vSandBoxField.VSandBoxType = DatamartFieldType.Counter
            vSandBoxField.Name = "vSandBoxCounterX_" & vSandBoxFieldModel.SourceObjectID
            vSandBoxField.CounterID = dragedObjectID
            vSandBoxField.SourceObjectID = vSandBoxFieldModel.SourceObjectID
            vSandBoxField.SQL_SourceTable = vSandBoxFieldModel.SQL_SourceTable
            vSandBoxField.TimeAggregation = vSandBoxFieldModel.TimeAggregation
            vSandBoxField.ObjectAggregation = vSandBoxFieldModel.ObjectAggregation
            vSandBoxField.SortValue = "None"
        ElseIf (dragedObjectType = "2") Then
            vSandBoxFieldModel = SandBoxTable.GetVSandBoxObjectForKPI(dt_TechnologyPackageKPI, TechnologyPackageKPIFields.KPI_ID & OperatorConst.Equal & Chr(39) & dragedObjectID & Chr(39))
            vSandBoxField.VSandBoxType = DatamartFieldType.Kpi
            vSandBoxField.Name = "vSandBoxKPIX_" & vSandBoxFieldModel.SourceObjectID
            vSandBoxField.SourceObjectID = vSandBoxFieldModel.SourceObjectID
            vSandBoxField.SQL_SourceTable = vSandBoxFieldModel.SQL_SourceTable
            vSandBoxField.SQL_KPI_ID = vSandBoxFieldModel.SQL_KPI_ID
            vSandBoxField.SQL_KPIFormula = vSandBoxFieldModel.SQL_KPIFormula
            vSandBoxField.SortValue = "None"
        ElseIf (dragedObjectType = "3") Then
            vSandBoxFieldModel = SandBoxTable.GetVSandBoxObjectForObject(dt_TechPackCounter, TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & dragedObjectID)
            vSandBoxField.VSandBoxType = DatamartFieldType.ObjectFld
            vSandBoxField.Name = "vSandBoxObjectX_" & dragedObjectText ''vSandBoxFieldModel.SourceObjectID
            vSandBoxField.SourceObjectID = dragedObjectID ''vSandBoxFieldModel.SourceObjectID
            vSandBoxField.Tag = flpCtrlTag
            vSandBoxField.SQL_KPI_ID = "0"
            vSandBoxField.CounterID = "0"
            vSandBoxField.SQL_KPIFormula = ""
            vSandBoxField.ObjectTypeID = TryCast(cmbObjectType.Properties.Items(1), clsComboBoxItem).Value
            IsAxisDataValid(tvReportGroup.FocusedNode.Tag, "3", "0", "0", vSandBoxField.ObjectTypeID, cmbObjectType.SelectedItem.ToString, "", vSandBoxField.Text)
            vSandBoxField.SortValue = "None"
            If (txtSandBoxTopX.Text = "0") And Not (IsVSandBoxFieldExistX("PERIOD_START_TIME")) Then
                txtSandBoxTopX.Text = "50"
            End If
        End If
        vSandBoxField.Text = dragedObjectText

        If (IsExistVSendBoxField_Object(vSandBoxField)) Then
            SetMessage("Fail : [ " & dragedObjectText & " ] object already added.")
            Exit Sub
        End If
        ' Dim flowLayout As System.Windows.Forms.FlowLayoutPanel = TryCast(sender, System.Windows.Forms.FlowLayoutPanel)

        ''IsAxisDataValid(tvReportGroup.FocusedNode.Tag, dragedObjectType, )
        If (flowLayout.Tag.ToString.ToUpper = "X") Then
            AttachedObjectWithXY(vSandBoxField, True, True)
        ElseIf (flowLayout.Tag.ToString.ToUpper = "Y") Then
            AttachedObjectWithXY(vSandBoxField, True, False)
            If (dragedObjectType = "1" Or dragedObjectType = "2") Then
                SetChartConfigurationDefaultValue(dragedObjectText)
                If (IsVSandBoxFieldExistX("PERIOD_START_TIME")) Then
                    If ceAutoRefreshChart.Checked = True Then
                        reportChartGrid_SendBox.ClearData()
                        RefreshChartAndGrid(dragedObjectText, True)
                    End If
                Else
                    SetMessage("Chart not generated, PERIOD_START_TIME not found.")
                End If
            End If
        End If
    End Sub

    Private Sub flp_ValueX_DragDrop(sender As Object, e As DragEventArgs) Handles flp_ValueX.DragDrop, flp_ValueY.DragDrop
        If (IsValidForDragDrop()) Then

            Dim items As String = e.Data.GetData(DataFormats.Text).ToString
            Dim dragedObjectText As String = items.Split("#")(0).ToString
            Dim dragedObjectID As String = items.Split("#")(1).ToString
            Dim dragedObjectType As String = items.Split("#")(2).ToString

            Dim flowLayout As FlowLayoutPanel = TryCast(sender, FlowLayoutPanel)
            If flowLayout IsNot Nothing Then
                flp_AddVsandBox(dragedObjectText, dragedObjectID, dragedObjectType, flowLayout)
            End If
        End If
    End Sub

    Private Sub IsAxisDataValid(ByVal reportID As String, ByVal sandBoxType As String, ByVal counterID As String, ByVal KPIID As String, ByVal objectTypeId As String, ByVal objectTypeName As String, ByVal objectViewFroCM As String, ByVal dimensionName As String)
        If (Not dtReportAxisData.IsValid) Then
            Dim drNew As DataRow = dtReportAxisData.NewRow
            drNew(ReportContentDimensionsFields.REPORT_ID) = reportID
            drNew(ReportContentDimensionsFields.DIMENSION_AXIS) = "0"
            drNew(ReportContentDimensionsFields.SANDBOX_FIELD_TYPE) = sandBoxType
            drNew(ReportContentDimensionsFields.COUNTER_ID) = counterID
            drNew(ReportContentDimensionsFields.KPI_ID) = KPIID
            drNew(ReportContentDimensionsFields.OBJECTTYPE_ID) = objectTypeId
            drNew(ReportContentDimensionsFields.OBJECTTYPE_NAME) = objectTypeName
            drNew(TechnologyObjectTypesFields.OBJECT_VIEW_FOR_CM) = objectViewFroCM
            drNew(ReportContentDimensionsFields.DIMENSIONNAME) = dimensionName
            dtReportAxisData.Rows.Add(drNew)
        Else
            If (sandBoxType = dtReportAxisData.Rows(0)(ReportContentDimensionsFields.SANDBOX_FIELD_TYPE)) Then
                dtReportAxisData.Rows.Clear()
                Dim drNew As DataRow = dtReportAxisData.NewRow
                drNew(ReportContentDimensionsFields.REPORT_ID) = reportID
                drNew(ReportContentDimensionsFields.DIMENSION_AXIS) = "0"
                drNew(ReportContentDimensionsFields.SANDBOX_FIELD_TYPE) = sandBoxType
                drNew(ReportContentDimensionsFields.COUNTER_ID) = counterID
                drNew(ReportContentDimensionsFields.KPI_ID) = KPIID
                drNew(ReportContentDimensionsFields.OBJECTTYPE_ID) = objectTypeId
                drNew(ReportContentDimensionsFields.OBJECTTYPE_NAME) = objectTypeName
                drNew(TechnologyObjectTypesFields.OBJECT_VIEW_FOR_CM) = objectViewFroCM
                drNew(ReportContentDimensionsFields.DIMENSIONNAME) = dimensionName
                dtReportAxisData.Rows.Add(drNew)
            End If
        End If
    End Sub

    Private Function IsVSandBoxFieldExistX(ByVal objectText As String) As Boolean
        If (flp_ValueX.Controls.Count > 0) Then
            For Each contSandBox As DevExSandBoxField In flp_ValueX.Controls
                If (contSandBox.VSandBoxType = "3" Or contSandBox.VSandBoxType = "4") And contSandBox.Text = objectText Then
                    Return True
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function

    Private Function IsVSandBoxFieldAnObjectType(ByVal objectText As String) As Boolean
        If (flp_ValueX.Controls.Count > 0) Then
            For Each ctr As clsComboBoxItem In cmbObjectType.Properties.Items
                If ctr.Text.ToString.ToUpper = objectText.ToUpper Then
                    Return True
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function

    Private Sub SetChartConfigurationDefaultValue(ByVal seriesName As String)
        isChartSerieSelected = False
        cmbChartConfig_SerieType.SelectedItem = cmbChartConfig_SerieType.Properties.Items(1)
        cmbChartConfig_SeriesAxisType.SelectedItem = cmbChartConfig_SeriesAxisType.Properties.Items(0)
        Dim rnd As Random = New Random()
        Dim cl As Color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))
        colEditChartConfig_SeriesColor.Color = cl
        cmbChartConfig_SeriesAxis.SelectedItem = cmbChartConfig_SeriesAxis.Properties.Items(0)
        spinEditChartConfig_ChartPrecision.Text = "0"
        cmbChartConfig_AbsPerc.SelectedItem = cmbChartConfig_AbsPerc.Properties.Items(0)
        txtChartConfig_AxisLabel.Text = ""
        cmbChartConfig_SeriesOrder.SelectedItem = cmbChartConfig_SeriesOrder.Properties.Items(0)

        CustomCharts_Serie_Insert(
            seriesName,
            ColorTranslator.ToOle(cl),
            cmbChartConfig_SerieType.SelectedItem.ToString,
            cmbChartConfig_SeriesAxisType.SelectedItem.ToString,
            cmbChartConfig_SeriesAxis.SelectedItem.ToString,
            txtChartConfig_AxisLabel.Text,
            spinEditChartConfig_ChartPrecision.EditValue,
            cmbChartConfig_AbsPerc.SelectedItem.ToString,
            "0", "", "", spinEdit_LineThickness.EditValue, cmbCalculatedYAxis.SelectedItem.ToString, txtChartAxisFont.Text.Trim,
            IIf(cmbChartConfig_SeriesOrder.SelectedItem.ToString.ToLower = "none", "", cmbChartConfig_SeriesOrder.SelectedItem.ToString)
            )
        RefreshChartSeriesTLV(tlvSandboxChartsSeries)
        isChartSerieSelected = True

    End Sub

    Private Function CounterObjectIsValid(ByRef vSandBoxFieldCounter As DevExSandBoxField) As Boolean
        Dim vSandBoxFieldTemp As DevExSandBoxField = New DevExSandBoxField()
        Dim isValid As Boolean = False
        Try
            If (vSandBoxFieldCounter.SourceObjectID = TryCast(cmbObjectSource.SelectedItem, clsComboBoxItem).Value) Then
                isValid = True
            Else
                If (cmbObjectSource.SelectedIndex <= 1) Then
                    Dim selectedSourceId As String = vSandBoxFieldCounter.SourceObjectID.Trim.ToString
                    Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(selectedSourceId, cmbObjectSource)
                    ''vcmb_ObjectSource.Properties.Items.ToList().FindIndex(Function(c) c.Value = selectedSourceId)
                    If (cmbItem IsNot Nothing) Then
                        cmbObjectSource.SelectedItem = cmbItem
                        isValid = True
                    End If
                Else
                    If (IsAnyObjectKPIOrCounterInVSendBoxField()) Then
                        isValid = False
                    Else
                        isValid = False
                    End If
                End If
            End If
        Catch ex As Exception
            isValid = False
        End Try
        Return isValid
    End Function

    Private Function IsAnyObjectKPIOrCounterInVSendBoxField() As Boolean
        Dim vSandBoxFieldTemp As DevExSandBoxField = New DevExSandBoxField()
        Dim isExist As Boolean = False
        For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
            vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxFieldTemp IsNot Nothing) Then
                If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.ObjectFld) Then
                    isExist = True
                    Exit For
                ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Kpi) Then
                    isExist = True
                    Exit For
                ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Counter) Then
                    isExist = True
                    Exit For
                End If
            End If
        Next
        If (isExist) Then
            Return isExist
            ''Exit Function
        End If
        For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
            vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxFieldTemp IsNot Nothing) Then
                If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.ObjectFld) Then
                    If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.ObjectFld) Then
                        isExist = True
                        Exit For
                    ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Kpi) Then
                        isExist = True
                        Exit For
                    ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Counter) Then
                        isExist = True
                        Exit For
                    End If
                End If
            End If
        Next

        Return isExist
    End Function

    Private Function IsExistVSendBoxField_Object(ByRef vSandBoxFieldObject As DevExSandBoxField) As Boolean
        Dim vSandBoxFieldTemp As DevExSandBoxField = New DevExSandBoxField()
        Dim isExist As Boolean = False
        For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
            vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxFieldTemp IsNot Nothing) Then
                If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.ObjectFld) Then
                    If (vSandBoxFieldTemp.Text.ToUpper = vSandBoxFieldObject.Text.ToUpper) Then
                        isExist = True
                        Exit For
                    End If
                End If
            End If
        Next
        If (isExist) Then
            Return isExist
            Exit Function
        End If
        For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
            vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxFieldTemp IsNot Nothing) Then
                If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Counter) Then
                    If (vSandBoxFieldTemp.Text.ToUpper = vSandBoxFieldObject.Text.ToUpper) Then
                        isExist = True
                        Exit For
                    End If
                End If
            End If
        Next

        Return isExist
    End Function

    Private Sub btnTime_Click(sender As Object, e As EventArgs) Handles btnTime.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            For Each ctrl As Control In flp_ValueX.Controls
                If ctrl.Name = "vSandBoxTimerX" Then
                    Exit Sub
                End If
            Next
            Dim vSandBoxField As DevExSandBoxField = New DevExSandBoxField()
            vSandBoxField.Name = "vSandBoxTimerX"
            vSandBoxField.Text = "PERIOD_START_TIME"
            vSandBoxField.VSandBoxType = DatamartFieldType.Time
            vSandBoxField.SortValue = "None"
            If (tvReportGroup.FocusedNode IsNot Nothing AndAlso tvReportGroup.FocusedNode.Level = 2) Then
                IsAxisDataValid(tvReportGroup.FocusedNode.Tag, "4", "0", "0", "0", "NULL", "NULL", vSandBoxField.Text)
                AttachedObjectWithXY(vSandBoxField, True, True)
                txtSandBoxTopX.Text = "0"
            Else
                SetMessage("Fail: Select a report!")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Function IsControlExist(ByRef flp As FlowLayoutPanel, ByRef vSandBoxFieldObject As Library.DevExSandBoxField) As Boolean
        Dim isExist As Boolean = False
        If (flp.Controls.Count > 0) Then
            For Each vsbfObject As Object In flp.Controls
                Dim vsbf As DevExSandBoxField = TryCast(vsbfObject, DevExSandBoxField)
                If (vsbf IsNot Nothing) Then
                    If (vsbf.Name.ToLower = vSandBoxFieldObject.Name.ToLower) Then
                        Return True
                    End If
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function

    Private Sub vlst_TechKPI_ItemChecking(sender As Object, e As Controls.ItemCheckingEventArgs)
        Try

            Dim listBox As CheckedListBoxControl = TryCast(sender, CheckedListBoxControl)
            Dim viewInfo As CheckedListBoxViewInfo = TryCast(listBox.GetViewInfo(), CheckedListBoxViewInfo)
            Dim point As Point = listBox.PointToClient(Control.MousePosition)
            Dim itemInfo As CheckedListBoxViewInfo.CheckedItemInfo = TryCast(viewInfo.GetItemInfoByPoint(point), CheckedListBoxViewInfo.CheckedItemInfo)
            e.Cancel = Not itemInfo.CheckArgs.Bounds.Contains(point)

            'If MouseButtons.Left = MouseButtons.Left Then
            '    If Not e.Index >= -1 Then
            '        'e.Item.IsChecked = Not e.Item.IsChecked
            '        ' vlst_TechKPI_ItemChecked(e.Item, New ItemCheckChangedEventArgs(e.Item))
            '    End If
            'Else
            '    If e.Index > -1 Then
            '        If (vlst_TechKPI.GetItemCheckState(e.Index) = CheckState.Checked) Then
            '            vlst_TechKPI.SetItemCheckState(e.Index, CheckState.Unchecked)
            '        Else
            '            vlst_TechKPI.SetItemCheckState(e.Index, CheckState.Checked)
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub lstTechCounter_MouseDown(sender As Object, e As MouseEventArgs) Handles lstTechCounter.MouseDown ', vlst_TechKPI.MouseDown
        Try
            Dim listControl As CheckedListBoxControl = TryCast(sender, CheckedListBoxControl)
            Dim vSandBoxFieldType As DatamartFieldType = New DatamartFieldType()
            vSandBoxFieldType = DatamartFieldType.None
            If (listControl IsNot Nothing) Then
                Dim itemIndex As Integer = listControl.IndexFromPoint(e.Location)
                If e.Button = MouseButtons.Left Then
                    rightMouseOnListbox = False

                    If (listControl.Tag = "1") Then
                        vSandBoxFieldType = DatamartFieldType.Counter
                        If Not objDMKpiConfig Is Nothing Then
                            objDMKpiConfig.dragDropType = DragDropType.ByCounter
                        End If
                    ElseIf (listControl.Tag = "2") Then
                        vSandBoxFieldType = DatamartFieldType.Kpi
                        If Not objDMKpiConfig Is Nothing Then
                            objDMKpiConfig.dragDropType = DragDropType.ByCounter
                        End If
                    End If

                    If (itemIndex > -1) Then
                        Dim item As Object = listControl.GetItem(itemIndex)
                        If item IsNot Nothing Then
                            Dim counterDragDropText As String = listControl.GetItemText(itemIndex) & "#" & listControl.GetItemValue(itemIndex) & "#" & vSandBoxFieldType
                            listControl.DoDragDrop(counterDragDropText, DragDropEffects.Copy)
                            listControl.SelectedIndex = listControl.IndexFromPoint(e.Location)
                        End If
                    End If
                Else
                    rightMouseOnListbox = True
                End If

                If e.Button = MouseButtons.Right Then
                    'Dim itemIndex As Integer = listControl.IndexFromPoint(e.Location)
                    Dim item As Object = Nothing
                    If itemIndex > -1 Then
                        item = listControl.GetItem(itemIndex)
                    End If

                    If item IsNot Nothing Then
                        If (listControl.Tag = "2") Then
                            cms_KPIManage.Show(MousePosition)
                        End If

                        If listControl.Name = "lstTechKPI" Then
                            If lstTechKPICheckedItems.Contains(listControl.GetItem(itemIndex)) Then
                                listControl.SetItemCheckState(itemIndex, CheckState.Checked)
                            Else
                                listControl.SetItemCheckState(itemIndex, CheckState.Unchecked)
                            End If
                        End If
                    End If
                End If
                If e.Button = MouseButtons.Left Then
                    If Not listControl.ItemCount = 0 Then
                        listControl.SetItemChecked(itemIndex, Not listControl.GetItemChecked(itemIndex))
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub flp_Value_DragEnter(sender As Object, e As DragEventArgs) Handles flp_ValueX.DragEnter, flp_ValueY.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub flp_ValueX_DragOver(sender As Object, e As DragEventArgs) Handles flp_ValueX.DragOver, flp_ValueY.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            tsmi_ReportEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            btnEdit.Appearance.BackColor = btnCommit.Appearance.BackColor
        End Try
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Try
                sqlSelectColList.Clear()
                reportChartGrid_SendBox.ClearData()
            Catch ex As Exception
                MsgBox("An error has occured while clearing chart/grid data!", MsgBoxStyle.OkOnly)
            End Try

            If flp_ValueX.Controls.Item("vSandBoxTimerX") IsNot Nothing AndAlso flp_ValueX.Controls.Item("vSandBoxTimerX").Text = "PERIOD_START_TIME" AndAlso flp_ValueX.Controls.Count > 1 Then
                If tvObjects.GetAllCheckedNodes().Count() > 1000 And tvObjects.AllNodesCount > 500 And IsVSandBoxFieldExistX(cmbObjectType.SelectedItem.ToString) = True Then
                    MsgBox("Please select less than 1000 objects from object tree!")
                    Exit Sub
                End If
            End If

            If (IsValidForCommit()) Then
                Dim reportID As String = tvReportGroup.FocusedNode.Tag
                Dim _sqlGenerateParameters As SQLGenerateParameters = GenarateSQL()
                txtSQLStatement.Text = String.Join(";", _sqlGenerateParameters.SQLCommands.ToArray())
                If (_sqlGenerateParameters IsNot Nothing) Then

                    CommitSandBoxFields(reportID, _sqlGenerateParameters.SQLCommands, _sqlGenerateParameters.ConnectionString)

                    'querying results
                    ' Dim ds_result As DataSet = QueryData(lst_TotalSQL, lst_connstring)
                    'assigning to grid

                    'If (ds_result IsNot Nothing) Then
                    '    If (ds_result.Tables.Count >= 1) Then
                    '        ''VDataGrid.SetData(vdgv_ChartControl, ds_result.Tables(0), "ALL")
                    '    End If
                    'End If

                    If flp_ValueX.Controls.Item("vSandBoxTimerX") IsNot Nothing AndAlso flp_ValueX.Controls.Item("vSandBoxTimerX").Text = "PERIOD_START_TIME" AndAlso flp_ValueX.Controls.Count > 1 Then
                        If tvObjects.GetAllCheckedNodes().Count() > 1000 And tvObjects.AllNodesCount > 500 And IsVSandBoxFieldExistX(cmbObjectType.SelectedItem.ToString) = True Then
                            MsgBox("Please select less than 1000 objects from object tree!")
                            Exit Sub
                        End If
                    End If

                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
                    Dim tempStr As String = TryCast(sender, String)
                    Dim isTempReport As Boolean = False
                    If Not tempStr Is Nothing Then
                        If tempStr.StartsWith("TempReportID=") Then
                            isTempReport = True
                        End If
                    End If

                    If (tvReportGroup.FocusedNode.Level = 2) And isTempReport = False Then
                        reportID = tvReportGroup.FocusedNode.Tag
                        reportName = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
                    ElseIf isTempReport = True Then
                        reportID = tempStr.Split(",")(0).Replace("TempReportID=", "")
                        reportName = tempStr.Split(",")(1)
                    End If

                    ChartConfigData_Delete(reportID)
                    For Each nd As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                        ChartConfigData_Insert(reportID, cmbChartType.SelectedItem.ToString, reportName, nd.SubItems(4).Text, nd.SubItems(7).Text, nd.SubItems(6).Text, nd.SubItems(5).Text, nd.SubItems(2).Text,
                                               nd.SubItems(0).Text, nd.SubItems(1).Text, CInt(nd.SubItems(3).Text), nd.SubItems(8).Text, IIf(nd.SubItems(9).Tag.ToString = "", "0", nd.SubItems(9).Tag.ToString), nd.SubItems(10).Text,
                                               nd.SubItems(11).Text, nd.SubItems(12).Text, nd.SubItems(13).Text)
                    Next

                    If (rbChart.Checked = True) Then
                        Me.GridorChart = "chart"
                    ElseIf (rbGrid.Checked = True) Then
                        Me.GridorChart = "grid"
                    ElseIf (rbExport.Checked = True) Then
                        Me.GridorChart = "export"
                    End If

                    If Me.GridorChart.ToLower = "chart" Or Me.GridorChart.ToLower = "grid" Then
                        '-- RELOADING CHART
                        dtChartConfigSandbox = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportChart.GetReportChartData(reportID))
                        Dim dsReportAxisData As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLReportChart.GetReportAxisData(reportID) & SQLReportContentFilter.GetReportContentFilter(reportID))
                        dtReportAxisData = dsReportAxisData.Tables(0)
                        dtReportFilterData = dsReportAxisData.Tables(1)
                        If (dsReportAxisData.Tables(1).Rows.Count > 0) Then
                            btnFilter.LookAndFeel.SetSkinStyle("Caramel")
                        Else
                            btnFilter.LookAndFeel.SetSkinStyle("Office 2010 Black")
                        End If

                        If (dtChartConfigSandbox.IsValid) Then
                            GetChartConfigData(dtChartConfigSandbox)
                            If (dtReportAxisData.IsValid) Then
                                BindChart(dtChartConfigSandbox, True)
                            End If
                        End If
                    ElseIf Me.GridorChart.ToLower = "export" Then
                        aceSndBxExportReport.Expanded = True
                        CommitSandBoxFieldsExport(reportID, _sqlGenerateParameters.ExpConnectionString)
                        dtReportExport = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportChart.GetReportChartData(reportID))
                        If dtReportExport IsNot Nothing AndAlso dtReportExport.Rows.Count > 0 Then
                            reportConnString = dtReportExport.Rows(0)("ReportConnString").ToString
                            expReportID = CInt(dtReportExport.Rows(0)("ReportID"))
                            reportName = dtReportExport.Rows(0)("ReportName").ToString
                            Dim reportSQL = dtReportExport.Rows(0)("ReportSQL").ToString

                            Dim objExpDel As New dlgExportDelimiter()
                            If objExpDel.ShowDialog = DialogResult.OK Then
                                OutputDelimiter = objExpDel.fileDelimiter

                                Dim objFileDlg As New SaveFileDialog()
                                objFileDlg.Filter = "Comma Delimited|*.csv"
                                objFileDlg.Title = "Save a CSV File"

                                If objFileDlg.ShowDialog() = DialogResult.OK Then
                                    If objFileDlg.FileName <> "" Then
                                        queue.Enqueue(Function()
                                                          Process_ReportExportAppend(reportName, reportSQL, objFileDlg.FileName, OutputDelimiter)
                                                      End Function)
                                        'Task.Run(Sub() Process_ReportExportAppend(reportName, reportSQL, objFileDlg.FileName, OutputDelimiter))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    SetMessage("Fail Commit : No Any source table.")
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            SetMessage("Committed Successfully")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub btnCommitAs_Click(sender As Object, e As EventArgs) Handles btnCommitAs.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Try
                sqlSelectColList.Clear()
                reportChartGrid_SendBox.ClearData()
            Catch ex As Exception
                MsgBox("An error has occured while clearing chart/grid data!", MsgBoxStyle.OkOnly)
            End Try

            If flp_ValueX.Controls.Item("vSandBoxTimerX") IsNot Nothing AndAlso flp_ValueX.Controls.Item("vSandBoxTimerX").Text = "PERIOD_START_TIME" AndAlso flp_ValueX.Controls.Count > 1 Then
                If tvObjects.GetAllCheckedNodes().Count() > 1000 Then
                    SetMessage("Please select less than 1000 objects from object tree!")
                    Exit Sub
                End If
            End If

            IsReportGroupMouseDownRight = False
            Dim newReport As New dlgNewReport()
            newReport.connString = connStrSandBoxServer
            newReport.ShowDialog()
            If newReport.newReportName IsNot Nothing AndAlso newReport.newReportGroupID IsNot Nothing Then
                Me.UseWaitCursor = True
                Application.DoEvents()
                'Adding new report with report group
                If (cmbReportGroup.SelectedIndex > 0) Then
                    'Dim reportCategoryID As String = "0"
                    'If (tvReportGroup.FocusedNode IsNot Nothing) Then
                    '    If (tvReportGroup.FocusedNode.Level = 1) Then
                    '        reportCategoryID = tvReportGroup.FocusedNode.Tag.ToString
                    '    ElseIf (tvReportGroup.FocusedNode.Level = 2) Then
                    '        reportCategoryID = tvReportGroup.FocusedNode.ParentNode.Tag.ToString
                    '    End If
                    'End If
                    'Dim reportGroupID As String = TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.InsertReports(newReport.newReportGroupID, "0", newReport.newReportName))

                    'Getting newly added reportId
                    Dim reportId As String = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReports.GetNewRepotId()).Rows(0)(0).ToString()
                    Dim _sqlGenerateParameters As SQLGenerateParameters = GenarateSQL()
                    txtSQLStatement.Text = String.Join(";", _sqlGenerateParameters.SQLCommands.ToArray())
                    If (_sqlGenerateParameters IsNot Nothing) Then
                        CommitSandBoxFields(reportId, _sqlGenerateParameters.SQLCommands, _sqlGenerateParameters.ConnectionString)

                        'Adding Chart configuration for new report
                        ChartConfigData_Delete(reportId)
                        For Each nd As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                            ChartConfigData_Insert(reportId, cmbChartType.SelectedItem.ToString, newReport.newReportName, nd.SubItems(4).Text, nd.SubItems(7).Text, nd.SubItems(6).Text, nd.SubItems(5).Text, nd.SubItems(2).Text, nd.SubItems(0).Text, nd.SubItems(1).Text, CInt(nd.SubItems(3).Text), nd.SubItems(8).Text, IIf(nd.SubItems(9).Tag.ToString = "", "0", nd.SubItems(9).Tag.ToString), nd.SubItems(10).Text, nd.SubItems(11).Text, nd.SubItems(12).Text, nd.SubItems(13).Text)
                        Next
                        'SetMessage("Chart Configuration Successfully Committed.")
                        Dim dtChartConfig As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportChart.GetReportChartData(reportId))

                        If (dtChartConfig.IsValid) Then
                            GetChartConfigData(dtChartConfig)
                            Dim dsReportAxisData As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLReportChart.GetReportAxisData(reportId) & SQLReportContentFilter.GetReportContentFilter(reportId))
                            dtReportAxisData = dsReportAxisData.Tables(0)
                            dtReportFilterData = dsReportAxisData.Tables(1)
                        End If
                        RefreshReportGroup_TreeList()
                        RefreshDashboardReport()
                        tvReportGroup.SetFocusedNode(tvReportGroup.FindNodeByFieldValue("ReportGroupName", newReport.newReportName))
                        tvReportGroup_MouseUp(Nothing, Nothing)
                    Else
                        SetMessage("Fail : No Any source table.")
                    End If
                Else
                    SetMessage("Fail : No Any source table.")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.UseWaitCursor = False
            Application.DoEvents()
            SetMessage("Committed Successfully.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Function GenarateSQL() As SQLGenerateParameters
        Dim sqlGenerateParameters As SQLGenerateParameters = New SQLGenerateParameters()
        Dim reportID As String = tvReportGroup.FocusedNode.Tag
        Dim sourcetable_ListTemp As List(Of String) = Make_SourceTable()
        Dim lst_connstring As List(Of String) = Make_ConnString(sourcetable_ListTemp)
        Dim lst_ExpConnString As List(Of String) = Make_ExportConnString(sourcetable_ListTemp)
        Dim select_fields As List(Of String) = New List(Of String)
        Dim from_fields As List(Of String) = New List(Of String)
        Dim where_fields As List(Of String) = New List(Of String)
        Dim groupBy_fields As List(Of String) = New List(Of String)
        Dim orderBy_fields As List(Of String) = New List(Of String)
        Dim lst_TotalSQL As List(Of String) = New List(Of String)
        Dim TotalSQL As String = String.Empty
        If (sourcetable_ListTemp.Count > 0) Then
            Dim sqlResult As String = String.Empty
            Dim sourcetable_List As List(Of String) = sourcetable_ListTemp.Distinct().ToList
            For Each st As String In sourcetable_List
                TotalSQL = String.Empty
                sqlResult = SQL_SelectPart(st)
                If (String.IsNullOrEmpty(sqlResult)) Then
                    SetMessage("Fail Commit : SQL SELECT PART.")
                    Return Nothing
                Else
                    select_fields.Add(sqlResult)
                    If (txtSandBoxTopX.Text.Trim IsNot String.Empty AndAlso IsNumeric(txtSandBoxTopX.Text.Trim)) Then
                        If (Integer.Parse(txtSandBoxTopX.Text.Trim) > 0) Then
                            TotalSQL = TotalSQL + vbLf + "SELECT TOP " & txtSandBoxTopX.Text.Trim & " " & sqlResult
                        Else
                            TotalSQL = TotalSQL + vbLf + "SELECT " + sqlResult
                        End If
                    Else
                        TotalSQL = TotalSQL + vbLf + "SELECT " + sqlResult
                    End If
                    ''TotalSQL = TotalSQL + vbLf + "SELECT " + sqlResult
                End If

                sqlResult = SQL_FromPart(st)
                If (String.IsNullOrEmpty(sqlResult)) Then
                    SetMessage("Fail Commit : SQL FROM PART.")
                    Return Nothing
                Else
                    from_fields.Add(sqlResult)
                    TotalSQL = TotalSQL + vbLf + " FROM " + sqlResult
                End If

                sqlResult = SQL_WherePart(st)
                If (String.IsNullOrEmpty(sqlResult)) Then
                    SetMessage("Fail Commit : SQL WHERE PART.")
                    Return Nothing
                Else
                    where_fields.Add(sqlResult)
                    TotalSQL = TotalSQL + vbLf + " WHERE " + sqlResult
                End If

                sqlResult = SQL_GroupPart(st)
                If (String.IsNullOrEmpty(sqlResult)) Then
                    SetMessage("Fail Commit : SQL GROUP PART.")
                    Return Nothing
                Else
                    groupBy_fields.Add(sqlResult)
                    TotalSQL = TotalSQL + vbLf + " GROUP BY " + sqlResult
                End If

                If dtReportFilterData.IsValid() Then
                    sqlResult = SQL_HavingPart(dtReportFilterData)
                    If Not String.IsNullOrEmpty(sqlResult) Then
                        TotalSQL = TotalSQL + " HAVING " + sqlResult
                    End If
                End If

                sqlResult = SQL_OrderByPart(st)
                If (Not String.IsNullOrEmpty(sqlResult)) Then
                    orderBy_fields.Add(sqlResult)
                    TotalSQL = TotalSQL + vbLf + " ORDER BY " + sqlResult
                End If

                lst_TotalSQL.Add(TotalSQL)

                sqlResult = String.Empty
            Next

        End If
        sqlGenerateParameters.SQLCommands = lst_TotalSQL
        sqlGenerateParameters.ConnectionString = lst_connstring
        sqlGenerateParameters.ExpConnectionString = lst_ExpConnString
        Return sqlGenerateParameters
    End Function

    Private Sub RefreshSandBoxChartConfig()
        Dim sc As New SeriesCollection()
        reportChartGrid_SendBox.RefreshChartConfig()
    End Sub

    Private Sub RefreshChartAndGrid(ByVal seriesName As String, ByVal isNeedSQLRegenerate As Boolean)
        Dim dtChartData As DataTable = New DataTable()

        Application.UseWaitCursor = True
        Cursor.Current = Cursors.WaitCursor
        Application.DoEvents()

        Dim sqlTotal As String = String.Empty
        Dim sqlConnection As String = String.Empty
        If tvReportGroup.FocusedNode Is Nothing Then
            SetMessage("No Report Selected")
            Exit Sub
        End If
        Dim reportID As String = tvReportGroup.FocusedNode.Tag
        If (isNeedSQLRegenerate) Then
            Dim _sqlGenerateParameters As SQLGenerateParameters = GenarateSQL()
            If (_sqlGenerateParameters IsNot Nothing) Then
                If _sqlGenerateParameters.SQLCommands.Count > 0 Then
                    sqlTotal = String.Join(";", _sqlGenerateParameters.SQLCommands.ToArray())
                    sqlConnection = _sqlGenerateParameters.ConnectionString.First
                Else
                    SetMessage("No SQL Command")
                    Exit Sub
                End If

            Else
                SetMessage("Error : SQL Generate.")
                Exit Sub
            End If

        Else
            If (dtChartConfigSandbox.IsValid) Then
                sqlTotal = dtChartConfigSandbox.Rows(0)(ReportChartFields.ReportSQL).ToString
                sqlConnection = dtChartConfigSandbox.Rows(0)(ReportChartFields.ReportConnString).ToString
            End If

        End If
        ''Dim _sqlGenerateParameters As SQLGenerateParameters = GenarateSQL()
        If (sqlTotal IsNot Nothing) Then
            ''Dim sqlTotal As String = String.Join(";", _sqlGenerateParameters.SQLCommands.ToArray())

            If (dtChartConfigSandbox.IsValid) Then
                If (isNeedSQLRegenerate) Then
                    For Each dr As DataRow In dtChartConfigSandbox.Rows
                        dr(ReportChartFields.ReportSQL) = sqlTotal
                    Next
                End If

                Dim drExist As DataRow = dtChartConfigSandbox.Rows(0)
                Dim drNew As DataRow = dtChartConfigSandbox.NewRow
                For Each nodeTLV As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                    If (nodeTLV.Text.ToUpper = seriesName.Replace(" ", "").ToUpper) Then
                        drNew(ReportChartFields.ReportChartID) = "0"
                        drNew(ReportChartFields.ReportID) = drExist(ReportChartFields.ReportID)
                        drNew(ReportChartFields.ChartType) = drExist(ReportChartFields.ChartType)
                        drNew(ReportChartFields.ChartTitle) = drExist(ReportChartFields.ChartTitle)
                        drNew(ReportChartFields.SeriesName) = seriesName.Replace(" ", "")
                        drNew(ReportChartFields.SeriesChartType) = nodeTLV.SubItems(1).Text
                        drNew(ReportChartFields.AxisLocation) = nodeTLV.SubItems(4).Text
                        drNew(ReportChartFields.AxisLabel) = nodeTLV.SubItems(7).Text
                        drNew(ReportChartFields.AxisAbsPerc) = nodeTLV.SubItems(6).Text
                        drNew(ReportChartFields.AxisPrecision) = nodeTLV.SubItems(5).Text
                        drNew(ReportChartFields.AxisScaleProp) = nodeTLV.SubItems(2).Text
                        drNew(ReportChartFields.SeriesColor) = nodeTLV.SubItems(3).Text
                        drNew(ReportChartFields.SortOrder) = nodeTLV.SubItems(8).Text
                        drNew(ReportChartFields.ReportName) = drExist(ReportChartFields.ReportName)
                        drNew(ReportChartFields.ReportSQL) = sqlTotal
                        drNew(ReportChartFields.ReportConnString) = sqlConnection
                        drNew(ReportChartFields.TimeResolution) = drExist(ReportChartFields.TimeResolution)
                        drNew(ReportChartFields.TechnologyPackageName) = drExist(ReportChartFields.TechnologyPackageName)
                        drNew(ReportChartFields.Calculated_Series_Type_ID) = nodeTLV.SubItems(9).Tag
                        drNew(ReportChartFields.Calculated_Series_Type_Name) = nodeTLV.SubItems(9).Text
                        drNew(ReportChartFields.Calculated_Series_Type_Parameters) = nodeTLV.SubItems(10).Text
                        drNew(ReportChartFields.StatisticsOrThreshold) = StatisticsOrThreshold.Threshold.ToString
                        Exit For

                    End If
                Next
                dtChartConfigSandbox.Rows.Add(drNew)
                BindChart(dtChartConfigSandbox, isNeedSQLRegenerate)
            Else
                'SetConfigDataInDT()
                Dim drNew As DataRow = dtChartConfigSandbox.NewRow
                For Each nodeTLV As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                    If (nodeTLV.Text.ToUpper = seriesName.ToUpper) Then
                        drNew(ReportChartFields.ReportChartID) = "0"
                        drNew(ReportChartFields.ReportID) = tvReportGroup.FocusedNode.Tag
                        drNew(ReportChartFields.ChartType) = "Combo"
                        drNew(ReportChartFields.ChartTitle) = tvReportGroup.FocusedNode.ParentNode.GetDisplayText("ReportGroupName")
                        drNew(ReportChartFields.SeriesName) = seriesName
                        drNew(ReportChartFields.SeriesChartType) = nodeTLV.SubItems(1).Text
                        drNew(ReportChartFields.AxisLocation) = nodeTLV.SubItems(4).Text
                        drNew(ReportChartFields.AxisLabel) = nodeTLV.SubItems(7).Text
                        drNew(ReportChartFields.AxisAbsPerc) = nodeTLV.SubItems(6).Text
                        drNew(ReportChartFields.AxisPrecision) = nodeTLV.SubItems(5).Text
                        drNew(ReportChartFields.AxisScaleProp) = nodeTLV.SubItems(2).Text
                        drNew(ReportChartFields.SeriesColor) = nodeTLV.SubItems(3).Text
                        drNew(ReportChartFields.SortOrder) = nodeTLV.SubItems(8).Text
                        drNew(ReportChartFields.ReportName) = tvReportGroup.FocusedNode.GetDisplayText("ReportGroupName")
                        drNew(ReportChartFields.ReportSQL) = sqlTotal
                        drNew(ReportChartFields.ReportConnString) = sqlConnection
                        drNew(ReportChartFields.TimeResolution) = cmbTimeResolution.SelectedItem.ToString
                        drNew(ReportChartFields.TechnologyPackageName) = cmbReportTechnology.SelectedItem.ToString
                        drNew(ReportChartFields.Calculated_Series_Type_ID) = nodeTLV.SubItems(9).Tag
                        drNew(ReportChartFields.Calculated_Series_Type_Name) = nodeTLV.SubItems(9).Text
                        drNew(ReportChartFields.Calculated_Series_Type_Parameters) = nodeTLV.SubItems(10).Text
                    End If
                Next
                dtChartConfigSandbox.Rows.Add(drNew)
                BindChart(dtChartConfigSandbox, isNeedSQLRegenerate)
            End If
        Else
            SetMessage("Error :  Found Some Error in SQL Part.")
        End If
        Application.UseWaitCursor = False
    End Sub

    Private Sub RefreshChartAndGrid(ByVal isNeedSQLRegenerate As Boolean)
        Dim dtChartData As DataTable = New DataTable()

        Application.UseWaitCursor = True
        Cursor.Current = Cursors.WaitCursor
        Application.DoEvents()

        Dim sqlTotal As String = String.Empty
        Dim sqlConnection As String = String.Empty
        Dim reportID As String = tvReportGroup.FocusedNode.Tag
        If (isNeedSQLRegenerate) Then
            Dim _sqlGenerateParameters As SQLGenerateParameters = GenarateSQL()
            If (_sqlGenerateParameters IsNot Nothing) Then
                sqlTotal = String.Join(";", _sqlGenerateParameters.SQLCommands.ToArray())
                sqlConnection = _sqlGenerateParameters.ConnectionString.First
            Else
                SetMessage("Error : SQL Generate.")
                Exit Sub
            End If

        Else
            If (dtChartConfigSandbox.IsValid) Then
                sqlTotal = dtChartConfigSandbox.Rows(0)(ReportChartFields.ReportSQL).ToString
                sqlConnection = dtChartConfigSandbox.Rows(0)(ReportChartFields.ReportConnString).ToString
            End If

        End If
        If (sqlTotal IsNot Nothing) Then
            If (dtChartConfigSandbox.IsValid) Then
                If (isNeedSQLRegenerate) Then
                    For Each dr As DataRow In dtChartConfigSandbox.Rows
                        dr(ReportChartFields.ReportSQL) = sqlTotal
                    Next
                End If
                If (Not isNeedSQLRegenerate) Then
                    For Each nodeTLV As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                        Dim drExist As DataRow = dtChartConfigSandbox.Select(ReportChartFields.SeriesName & "='" & nodeTLV.Text & "'").FirstOrDefault()
                        If (drExist IsNot Nothing) Then
                            If (drExist(ReportChartFields.SeriesName).ToString.ToUpper = nodeTLV.Text.ToUpper) Then
                                drExist(ReportChartFields.SeriesChartType) = nodeTLV.SubItems(1).Text
                                drExist(ReportChartFields.AxisLocation) = nodeTLV.SubItems(4).Text
                                drExist(ReportChartFields.AxisLabel) = nodeTLV.SubItems(7).Text
                                drExist(ReportChartFields.AxisAbsPerc) = nodeTLV.SubItems(6).Text
                                drExist(ReportChartFields.AxisPrecision) = nodeTLV.SubItems(5).Text
                                drExist(ReportChartFields.AxisScaleProp) = nodeTLV.SubItems(2).Text
                                drExist(ReportChartFields.SeriesColor) = nodeTLV.SubItems(3).Text
                                drExist(ReportChartFields.SortOrder) = nodeTLV.SubItems(8).Text
                                drExist(ReportChartFields.Calculated_Series_Type_ID) = nodeTLV.SubItems(9).Tag
                                drExist(ReportChartFields.Calculated_Series_Type_Name) = nodeTLV.SubItems(9).Text
                                drExist(ReportChartFields.Calculated_Series_Type_Parameters) = nodeTLV.SubItems(10).Text
                            End If
                        End If
                    Next
                End If
                BindChart(dtChartConfigSandbox, isNeedSQLRegenerate)
            End If
        Else
            SetMessage("Error :  Found Some Error in SQL Part.")
        End If
        Application.UseWaitCursor = False
    End Sub

    Private Sub SetConfigDataInDT()
        dtChartConfigSandbox.Columns.Add(ReportChartFields.ReportChartID)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.ReportID)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.ChartType)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.ChartTitle)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.SeriesName)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.SeriesChartType)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.AxisLocation)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.AxisLabel)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.AxisAbsPerc)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.AxisPrecision)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.AxisScaleProp)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.SeriesColor)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.SortOrder)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.ReportName)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.ReportSQL)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.ReportConnString)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.TimeResolution)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.TechnologyPackageName)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.Calculated_Series_Type_ID)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.Calculated_Series_Type_Name)
        dtChartConfigSandbox.Columns.Add(ReportChartFields.Calculated_Series_Type_Parameters)
        'Dim frmCategoryManager As New frm_CategoryManagerDialog()
    End Sub

    Private Function QueryData(ByVal lst_TotalSQL As List(Of String), lst_connstring As List(Of String)) As DataSet
        Dim ds_result As New DataSet
        For i = 0 To lst_TotalSQL.Count - 1
            Try

                Dim dt As DataTable = DataAccessorODBC.GetDataTable(lst_connstring(i), lst_TotalSQL(i))
                If (dt IsNot Nothing) Then
                    'identifying primary keys (take all columns excluding counters/kpis)
                    Dim pkcols() As DataColumn = Nothing
                    Dim pkcolsindex As Integer = 0
                    For Each col As DataColumn In dt.Columns
                        If GetTypeOfSandboxField(col.ColumnName) = DatamartFieldType.Time Or GetTypeOfSandboxField(col.ColumnName) = DatamartFieldType.ObjectFld Then
                            ReDim Preserve pkcols(pkcolsindex)
                            pkcols(pkcolsindex) = col
                            pkcolsindex = pkcolsindex + 1
                        End If
                    Next
                    dt.PrimaryKey = pkcols
                    If ds_result.Tables.Count = 0 Then
                        ds_result.Tables.Add(dt.Copy)
                    Else
                        ds_result.Tables(0).Merge(dt.Copy)
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
        Return ds_result
    End Function

    Private Function IsValidForDragDrop() As Boolean
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            If (tvReportGroup.FocusedNode.Level = 2) Then
                If (cmbTimeResolution.SelectedItem IsNot Nothing) Then
                    If (cmbObjectSource.SelectedIndex >= 1 AndAlso cmbObjectType.SelectedIndex >= 1) Then
                        Return True
                    Else
                        SetMessage("Fail DragDrop : Please Select Object Source OR Object Type.")
                        Return False
                    End If
                Else
                    SetMessage("Fail DragDrop : Please Select Time Resolution.")
                    Return False
                End If
            Else
                SetMessage("Fail DragDrop : Select Report.")
                Return False
            End If
        Else
            SetMessage("Fail DragDrop : Select Report.")
            Return False
        End If
    End Function

    Private Function IsValidForCommit() As Boolean
        If (tvReportGroup.FocusedNode IsNot Nothing) Then
            If (tvReportGroup.FocusedNode.Level = 2) Then
                If (flp_ValueX.Controls.Count > 0 AndAlso flp_ValueY.Controls.Count > 0) Then
                    If (cmbTimeResolution.SelectedItem IsNot Nothing) Then

                        If (cmbObjectSource.SelectedIndex >= 1 AndAlso cmbObjectType.SelectedIndex >= 1) Then
                            Return True
                        Else
                            SetMessage("Fail Commit : Please Select Object Source OR Object Type.")
                            Return False
                        End If
                    Else
                        SetMessage("Fail Commit : Please Select Time Resolution")
                        Return False
                    End If

                Else
                    SetMessage("Fail Commit : No any object in X-Value or Y-Value")
                    Return False
                End If
            Else
                SetMessage("Commit Fail : Select Report.")
                Return False
            End If
        Else
            SetMessage("Commit Fail : Select Report.")
            Return False
        End If
    End Function

    Private Sub CommitSandBoxFields(ByVal reportId As String, ByVal lstSQL As List(Of String), ByVal connstring As List(Of String))
        Try
            DeleteReportObjects(reportId)
            InsertReportSourceTime(reportId)
            GetSandBoxFieldsControls(flp_ValueX, reportId)
            GetSandBoxFieldsControls(flp_ValueY, reportId)
            InsertTreeObjects(reportId)
            InsertReportSQL(reportId, lstSQL, connstring)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CommitSandBoxFieldsExport(ByVal reportID As String, ByVal expConnstring As List(Of String))
        Try
            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportContent.ReportContent_CreateExportConnection(reportID, expConnstring.First))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub InsertReportSQL(ByVal reportId As Integer, ByVal lstSQL As List(Of String), ByVal connstring As List(Of String))
        Dim sqlUpdate As String = "UPDATE [dbo].[tbl_Reports] SET [ReportSQL]= ? , [ReportConnString]= ? , [ObjectNamesInReport]= ?, [TopX]= ?, [GridOrChart]= ?, [CompareTime]= ? WHERE [ReportID]=" & reportId

        'Update object string from objects tree into tbl_reports
        Dim CompareTime As Int16 = 0
        If ceAlignIntervalAll.Checked Then
            CompareTime = 1
            'ElseIf ceAlignIntervalMatch.Checked Then
            '    CompareTime = 2
        End If
        Dim tech As String = cmbReportTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbObjectType.SelectedItem.ToString
        Dim objectString As String = GetChecked2String(tvObjects, tech, aggr_to, "Naked")

        Dim odbcParam1 As Odbc.OdbcParameter = New Odbc.OdbcParameter
        odbcParam1.DbType = DbType.String
        odbcParam1.Value = String.Join(";", lstSQL.ToArray())

        Dim odbcParam2 As Odbc.OdbcParameter = New Odbc.OdbcParameter
        odbcParam2.DbType = DbType.String
        odbcParam2.Value = connstring.First

        Dim odbcParam3 As Odbc.OdbcParameter = New Odbc.OdbcParameter
        odbcParam3.DbType = DbType.String
        odbcParam3.Value = objectString

        Dim odbcParam4 As Odbc.OdbcParameter = New Odbc.OdbcParameter
        odbcParam4.DbType = DbType.String
        odbcParam4.Value = txtSandBoxTopX.Text.Trim

        Dim odbcParam5 As Odbc.OdbcParameter = New Odbc.OdbcParameter
        odbcParam5.DbType = DbType.String
        odbcParam5.Value = IIf(rbChart.Checked, "CHART", IIf(rbGrid.Checked, "GRID", "EXPORT"))

        Dim odbcParam6 As Odbc.OdbcParameter = New Odbc.OdbcParameter
        odbcParam6.DbType = DbType.String
        odbcParam6.Value = CompareTime.ToString


        Dim lst_odbcparams As New List(Of Odbc.OdbcParameter)
        lst_odbcparams.Add(odbcParam1)
        lst_odbcparams.Add(odbcParam2)
        lst_odbcparams.Add(odbcParam3)
        lst_odbcparams.Add(odbcParam4)
        lst_odbcparams.Add(odbcParam5)
        lst_odbcparams.Add(odbcParam6)

        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlUpdate, lst_odbcparams)
    End Sub

    Private Sub DeleteReportObjects(ByVal reportId As Integer)
        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportContent.ReportContent_Delete(reportId))
    End Sub

    Private Sub InsertTreeObjects(ByVal reportId As Integer)
        Dim tech As String = cmbReportTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbObjectType.SelectedItem.ToString
        Try
            Dim copystring As String = GetChecked2String(tvObjects, tech, aggr_to, "ObjectSQL")
            copystring = copystring.Replace(",,", ControlChars.NewLine)
            If Not copystring Is Nothing Or copystring <> "" Then
                Dim sqlSourcTime As String = String.Empty
                Dim objectIds As List(Of String) = New List(Of String)
                objectIds = copystring.Split(ControlChars.NewLine).ToList
                If (objectIds.Count >= 1) Then
                    Dim counterNo As Integer = 0
                    For Each objectId As String In objectIds
                        counterNo = counterNo + 1
                        sqlSourcTime = sqlSourcTime + SQLReportContentObjects.InsertReportContent_Objects(objectId.Replace("'", ""), reportId)
                        If (counterNo > 100) Then
                            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlSourcTime)
                            counterNo = 0
                            sqlSourcTime = String.Empty
                        End If
                    Next
                    If (Not String.IsNullOrEmpty(sqlSourcTime)) Then
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlSourcTime)
                    End If

                End If
                '' Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
        End Try
    End Sub

    Private Sub InsertReportObjects(ByRef vSandBoxFieldTemp As Library.DevExSandBoxField, ByVal reportId As Integer, ByVal dimensionAxis As Integer)
        Dim sqlDimensions As String = SQLReportContentDimensions.InsertReportContent_Dimensions(reportId, dimensionAxis, vSandBoxFieldTemp.VSandBoxType, vSandBoxFieldTemp.CounterID, vSandBoxFieldTemp.SQL_KPI_ID,
                                                                                                vSandBoxFieldTemp.ObjectTypeID, vSandBoxFieldTemp.SortValue, vSandBoxFieldTemp.Text)
        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlDimensions)
    End Sub

    Private Sub InsertReportSourceTime(ByVal reportId As String)
        'Dim isAggregrationOrSplit As Integer = IIf(rbAggregateObjects.Checked, 0, 1)
        Dim topXValue As Integer = 0
        If (txtSandBoxTopX.Text.Trim.Length > 0) Then
            topXValue = txtSandBoxTopX.Text
        End If
        Dim sqlSourcTime As String = SQLReportContentSourceTime.InsertReportContent_SourceTime(reportId, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, cmbObjectSource.SelectedItem.Value, TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value,
                                                                                               cmbTimeResolution.SelectedItem.ToString, vcmb_PredefinedPeriod.SelectedItem.Value, dtEditStartTime.EditValue, dtEditEndTime.EditValue, cmbCMPM.SelectedItem.Value,
                                                                                               1, topXValue)
        If vcmb_PredefinedPeriod.SelectedIndex = -1 Or vcmb_PredefinedPeriod.SelectedIndex = 0 Then

            Dim truncatedDateTime1 As DateTime = New DateTime(dtEditStartTime.EditValue.Ticks - (dtEditStartTime.EditValue.Ticks Mod TimeSpan.TicksPerSecond), dtEditStartTime.EditValue.Kind)
            Dim truncatedDateTime2 As DateTime = New DateTime(dtEditEndTime.EditValue.Ticks - (dtEditEndTime.EditValue.Ticks Mod TimeSpan.TicksPerSecond), dtEditEndTime.EditValue.Kind)

            Dim odbcParam1 As New Odbc.OdbcParameter
            odbcParam1.DbType = DbType.DateTime
            odbcParam1.Value = truncatedDateTime1
            Dim odbcParam2 As New Odbc.OdbcParameter
            odbcParam2.DbType = DbType.DateTime
            odbcParam2.Value = truncatedDateTime2

            Dim lst_OdbcParams As New List(Of Odbc.OdbcParameter)
            lst_OdbcParams.Add(odbcParam1)
            lst_OdbcParams.Add(odbcParam2)

            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlSourcTime, lst_OdbcParams)
        Else
            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlSourcTime.Replace("?", "NULL"))
        End If
    End Sub

    Private Sub InsertReportDimensions(ByRef vSandBoxFieldTemp As Library.DevExSandBoxField, ByVal reportId As String, ByVal dimensionAxis As Integer)
        Dim sqlSourcTime As String = SQLReportContentDimensions.InsertReportContent_Dimensions(reportId, dimensionAxis, vSandBoxFieldTemp.VSandBoxType, vSandBoxFieldTemp.CounterID, vSandBoxFieldTemp.SQL_KPI_ID,
                                                                                            vSandBoxFieldTemp.ObjectTypeID, vSandBoxFieldTemp.SortValue, vSandBoxFieldTemp.Text)
        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlSourcTime)
    End Sub

    Private Sub GetSandBoxFieldsControls(ByRef flowLayoutPanelXY As FlowLayoutPanel, ByVal reportId As String)
        Dim vSandBoxFieldTemp As DevExSandBoxField = New DevExSandBoxField()
        If (flowLayoutPanelXY.Controls.Count > 0) Then
            For Each flowLayoutPanelXYControls As Object In flowLayoutPanelXY.Controls
                vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxFieldTemp IsNot Nothing) Then
                    '  If (vSandBoxFieldTemp.IsNew) Then
                    If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Counter) Then
                        InsertReportDimensions(vSandBoxFieldTemp, reportId, IIf(flowLayoutPanelXY.Tag.ToString.ToUpper = "X", 0, 1))
                    ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Kpi) Then
                        InsertReportDimensions(vSandBoxFieldTemp, reportId, IIf(flowLayoutPanelXY.Tag.ToString.ToUpper = "X", 0, 1))
                    ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.ObjectFld) Then
                        InsertReportObjects(vSandBoxFieldTemp, reportId, IIf(flowLayoutPanelXY.Tag.ToString.ToUpper = "X", 0, 1))
                    ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Time) Then
                        InsertReportObjects(vSandBoxFieldTemp, reportId, IIf(flowLayoutPanelXY.Tag.ToString.ToUpper = "X", 0, 1))
                    End If
                    'End If
                Else

                End If
            Next
        End If
    End Sub

    Private Function GetTypeOfSandboxField(ByVal sandboxfieldname As String) As DatamartFieldType
        Dim vSandBoxFieldTemp As DevExSandBoxField = New DevExSandBoxField()
        If (flp_ValueX.Controls.Count > 0) Then
            For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
                vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxFieldTemp IsNot Nothing) Then
                    If vSandBoxFieldTemp.Text.ToUpper = sandboxfieldname.ToUpper Then
                        Return vSandBoxFieldTemp.VSandBoxType
                    End If
                End If
            Next
        End If
        If (flp_ValueY.Controls.Count > 0) Then
            For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
                vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxFieldTemp IsNot Nothing) Then
                    If vSandBoxFieldTemp.Text.ToUpper = sandboxfieldname.ToUpper Then
                        Return vSandBoxFieldTemp.VSandBoxType
                    End If
                End If
            Next
        End If
        Return Nothing

    End Function

    Private Sub Process_ReportExportAppend(_reportName As String, _reportSql As String, _fileName As String, _fileDelimiter As String)
        SyncLock objExportThreadLock
            ExportDataToCSV(_reportName, _reportSql, _fileName, _fileDelimiter)
        End SyncLock

        'If AbortExport(reportAbort) = True Then
        '    Exit Sub
        'End If
    End Sub

    Private Sub ExportDataToCSV(reportName As String, sqlQuery As String, fileName As String, fileDelimiter As String)
        Try
            Threading.Thread.Sleep(3000)

            Me.Invoke(Sub()
                          txtExpReportStatus.Text = txtExpReportStatus.Text & vbCrLf & reportName & ": Report is being exported in the background."
                      End Sub)

            SyncLock objExportThreadLock
                Dim connArr As String = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContent.ReportContent_GetExportConnection(expReportID)).Rows(0)(0).ToString
                Dim connString As String = GetDecryptedConnectionString(connArr)

                Using sourceConnection As SqlConnection = New SqlConnection(connString)
                    sourceConnection.Open()
                    Dim commandSourceData As SqlCommand = New SqlCommand(sqlQuery, sourceConnection)
                    commandSourceData.CommandTimeout = 1000

                    Dim bufferSize = 1024 * 1024 '1Mb

                    If File.Exists(fileName) Then
                        File.Delete(fileName)
                    End If

                    Using FileObject As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bufferSize)
                        Using StreamWriterObj As New StreamWriter(FileObject)
                            Using dataReader As SqlDataReader = commandSourceData.ExecuteReader()
                                Dim FieldCount As Integer = dataReader.FieldCount - 1

                                StreamWriterObj.Write(String.Format("{0}", dataReader.GetName(0)))
                                For i = 1 To FieldCount
                                    StreamWriterObj.Write(fileDelimiter)
                                    StreamWriterObj.Write(String.Format("{0}", dataReader.GetName(i)))
                                Next
                                StreamWriterObj.WriteLine()

                                Do While dataReader.Read()
                                    StreamWriterObj.Write(dataReader.Item(0))
                                    For i = 1 To FieldCount
                                        StreamWriterObj.Write(fileDelimiter)
                                        If dataReader.GetFieldType(i) Is GetType(String) Then
                                            StreamWriterObj.Write($""" & {dataReader.Item(i)} & """)
                                        Else
                                            StreamWriterObj.Write(dataReader.Item(i))
                                        End If

                                    Next
                                    StreamWriterObj.WriteLine()
                                Loop
                            End Using
                        End Using
                    End Using
                End Using
            End SyncLock

            Me.Invoke(Sub()
                          txtExpReportStatus.Text = txtExpReportStatus.Text & vbCrLf & reportName & ": Report Export Is Completed."
                      End Sub)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            Me.Invoke(Sub()
                          txtExpReportStatus.Text = txtExpReportStatus.Text & vbCrLf & reportName & ": Report Export Is Failed."
                      End Sub)
        End Try
    End Sub

#End Region

    Public Sub txtObject_TextChanged(ByRef tree As TreeView, ByVal text As String)
        Dim tn As TreeNode = tree.SelectedNode
        TreeView_SearchFound = 0
        Treeview_NodeFound = False
        Try
            If tree.Nodes.Count <> 0 Then
                If Not tn Is Nothing Then
                    tn.BackColor = Color.White
                End If
                Dim tns() As TreeNode = tree.Nodes(0).Nodes.Find(text, True)

                If tns.Length > 0 Then
                    tns(0).EnsureVisible()
                    tns(0).TreeView.SelectedNode = tns(0)
                    tns(0).BackColor = Color.White
                    TreeView_SearchFound = 1
                Else
                    TreeView_SearchWildCard(tree.Nodes(0), text, 0)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub txtObject_KeyDown(ByRef tree As TreeView, ByVal text As String, ByRef e As KeyEventArgs)
        Dim tn As TreeNode = tree.SelectedNode

        If Not tn Is Nothing Then
            Try
                If e.KeyCode = Keys.Enter Then
                    Treeview_NodeFound = False

                    If TreeView_SearchFound = 0 Then
                        TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn), True)
                        If Treeview_NodeFound = False Then
                            TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn))
                        End If
                    Else
                        TreeView_SearchFound = 0
                        TreeView_SearchWildCard(tree.Nodes(0), text, 0, False)
                    End If

                    If tn.Index = tree.SelectedNode.Index Then
                        tn.EnsureVisible()
                        tn.BackColor = Color.Coral
                    Else
                        tn.BackColor = Color.White
                    End If
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            End Try
        End If
    End Sub

    Private Sub TreeView_SearchWildCard(ByVal nd As TreeNode, ByVal str As String, ByVal startindex As Integer, Optional ByVal ExactMatch As Boolean = False)
        nd.TreeView.SuspendLayout()
        Try
            If str.Length < 3 Then
                For Each nd In nd.Nodes
                    If Treeview_NodeFound = True Then
                        nd.TreeView.ResumeLayout(True)
                        Exit Sub
                    End If
                    If nd.Text.ToUpper = str.ToUpper Then
                        If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                            nd.EnsureVisible()
                            nd.TreeView.SelectedNode = nd

                            nd.BackColor = Color.Coral
                            Treeview_NodeFound = True
                            nd.TreeView.ResumeLayout(True)
                            Exit Sub
                        End If

                    Else
                        nd.BackColor = Color.White
                    End If
                    TreeView_SearchWildCard(nd, str, startindex)
                Next
            Else
                For Each nd In nd.Nodes
                    If Treeview_NodeFound = True Then
                        nd.TreeView.ResumeLayout(True)
                        Exit Sub
                    End If
                    If ExactMatch = False Then
                        If nd.Text.ToUpper.StartsWith(str.ToUpper) Then
                            If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                nd.EnsureVisible()
                                nd.TreeView.SelectedNode = nd
                                nd.BackColor = Color.Coral
                                Treeview_NodeFound = True
                                nd.TreeView.ResumeLayout(True)
                                Exit Sub
                            End If
                        Else
                            nd.BackColor = Color.White
                        End If
                        TreeView_SearchWildCard(nd, str, startindex)
                    Else
                        If nd.Text.ToUpper = str.ToUpper Then
                            If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                                nd.EnsureVisible()
                                nd.TreeView.SelectedNode = nd
                                nd.BackColor = Color.Coral
                                Treeview_NodeFound = True
                                nd.TreeView.ResumeLayout(True)
                                Exit Sub
                            End If
                        Else
                            nd.BackColor = Color.White
                        End If
                        TreeView_SearchWildCard(nd, str, startindex, True)
                    End If

                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        nd.TreeView.ResumeLayout(True)
    End Sub

    Private Function Treeview_NodePosition(ByVal oTreeView As TreeView, ByVal oNode As TreeNode)
        Dim iPosInTree As Integer = 0
        Do
            Dim iNodeIndex As Integer = oNode.Index
            iPosInTree = iPosInTree + iNodeIndex + 1

            'Get the Parent Node or the TreeView if at the top.
            Dim oParentNode As Object = oNode.Parent
            If oParentNode Is Nothing Then
                oParentNode = oTreeView
            End If

            'Count the Nodes precding this one on the current level.
            Dim I As Integer
            For I = 0 To iNodeIndex - 1
                iPosInTree = iPosInTree + Treeview_NumberOfChildren(oParentNode.Nodes(I))
            Next

            'Go up to the next level.
            oNode = oNode.Parent
        Loop Until oNode Is Nothing
        Return iPosInTree
    End Function

    Function Treeview_NumberOfChildren(ByVal oNode As TreeNode)
        If oNode.LastNode Is Nothing Then
            Return 0 'No children
        End If
        Dim iNumChildren = oNode.LastNode.Index + 1
        Dim oSubNode As TreeNode
        For Each oSubNode In oNode.Nodes
            iNumChildren = iNumChildren + Treeview_NumberOfChildren(oSubNode)
        Next
        Return iNumChildren
    End Function

    'Private Sub rbAggregateObjects_CheckedChanged(sender As Object, e As EventArgs) Handles rbAggregateObjects.CheckedChanged
    '    If (rbAggregateObjects.Checked) Then
    '        rbSplitObjects.Checked = False
    '    Else
    '        rbSplitObjects.Checked = True
    '    End If
    'End Sub

    'Private Sub rbSplitObjects_CheckedChanged(sender As Object, e As EventArgs) Handles rbSplitObjects.CheckedChanged
    '    Try
    '        If (rbSplitObjects.Checked) Then
    '            rbAggregateObjects.Checked = False
    '        Else
    '            rbAggregateObjects.Checked = True
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

#Region "Object Tree"

    Private Sub tvObjects_MouseDown(sender As Object, e As MouseEventArgs) Handles tvObjects.MouseDown
        Try
            Dim tree As TreeList = TryCast(sender, TreeList)
            If (tree IsNot Nothing AndAlso cmbObjectType.SelectedIndex > 0) Then
                Dim item As TreeListHitInfo = tree.CalcHitInfo(e.Location)
                If (e.Button = MouseButtons.Left) Then
                    Dim objectDragDropText As String = cmbObjectType.SelectedItem.ToString.Trim & "#" & TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value & "#" & DatamartFieldType.ObjectFld
                    tree.DoDragDrop(objectDragDropText, DragDropEffects.Copy)
                    tree.FocusedNode = item.Node
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tvObjects_AfterCheckNode(sender As Object, e As NodeEventArgs) Handles tvObjects.AfterCheckNode
        TreeView_CheckAndCount(e.Node, 0)
        If (tvObjects.GetEndCheckedNodes().Count > 1000) Then
            SetMessage("Objects in a report cannot go beyond 1000")
            tvObjects.UncheckAll()
        End If
    End Sub

    Public Sub TreeView_CheckAndCount(ByRef nd As TreeListNode, ByVal cnt As Integer)
        Try
            If nd.Checked = True Then
                If nd.Level > 1 Then
                    If nd.ParentNode.Checked = False Then
                        nd.ParentNode.Checked = True
                    End If
                End If

                If nd.Nodes.Count > 0 And SandBoxTreeView.TreeListNodes_GetCheck(nd.Nodes).Count = 0 Then
                    For Each nde As TreeListNode In nd.Nodes
                        If nde.Checked = False Then
                            nde.Checked = True
                        End If
                    Next
                End If
            Else
                If nd.Nodes.Count > 0 Then
                    For Each nde As TreeListNode In nd.Nodes
                        If nde.Checked = True Then
                            nde.Checked = False
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

    Private Sub cms_ObjectTLV_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_ObjectTLV.Opening
        cm_OT_SourceControl = cms_ObjectTLV.SourceControl
        Dim tv As TreeList = CType(cms_ObjectTLV.SourceControl, TreeList)
        Dim tech As String = cmbReportTechnology.SelectedItem.ToString
        Dim aggr_to As String = cmbObjectType.SelectedItem.ToString ''       
        Dim countchecked As Integer = 0
        Try
            Dim ExactMatch As Boolean = True
            If aggr_to = "WBTS" Or aggr_to = "BCF" Then
                ExactMatch = False
            Else
                ExactMatch = True
            End If
            Dim nodelevel As Integer = cmbObjectType.Properties.Items.Count - cmbObjectType.SelectedIndex '' - 1

            'count checked boxes
            countchecked = tv.GetEndCheckedNodes().Count

            'enable/disable copy
            If countchecked > 0 Then
                tsmi_ObjectCopy.Text = "Copy - Objects: " & countchecked
                tsmi_ObjectCopy.Enabled = True
            Else
                tsmi_ObjectCopy.Text = "Copy"
                tsmi_ObjectCopy.Enabled = False
            End If

            'check clipboard
            Dim s As String = Clipboard.GetText()                  'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim i, j As Integer
            If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                tsmi_ObjectPaste.Text = "Paste - Objects: ?"
                tsmi_ObjectPaste.Enabled = True
            Else
                Dim clipboardmatches As Integer = 0
                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                    For j = 0 To bufferCell.Length - 1
                        If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                            bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                        End If
                        If bufferCell(j).ToString.Contains("'") Then
                            bufferCell(j) = bufferCell(j).ToString.Replace("'", "")
                        End If
                        If bufferCell(j).Trim <> "" Then
                            If Not tv.FindNodeByFieldValue("ObjectName", bufferCell(j).Trim) Is Nothing Then
                                clipboardmatches = clipboardmatches + 1
                            End If
                        End If
                    Next
                Next

                'enable/disable paste
                If clipboardmatches > 0 Then
                    tsmi_ObjectPaste.Text = "Paste - Objects: " & clipboardmatches
                    tsmi_ObjectPaste.Enabled = True
                Else
                    tsmi_ObjectPaste.Text = "Paste"
                    tsmi_ObjectPaste.Enabled = False
                End If
                tv.Cursor = Cursors.Arrow
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

        'tags
        '----
        'get all tags
        Dim sql As String = Nothing
        Dim connstring As String = Nothing
        '''''''''''''''cm_OT_tsmi_CopyToTag.Enabled = False
    End Sub

    Public Function TreeView_CountCheckedNodes(ByVal rootNode As TreeNode, ByVal level As Integer) As Integer
        Dim count As Integer = 0
        ' count the root node, if checked
        If rootNode.Checked And rootNode.Level = level Then count = 1
        ' check the child nodes, by recursively calling this function for all of 
        ' them
        Dim tvn As TreeNode = Nothing
        For Each tvn In rootNode.Nodes
            count += TreeView_CountCheckedNodes(tvn, level)
        Next
        tvn = Nothing
        Return count
    End Function

    Public Function TreeView_CountCheckedAll(ByVal rootNode As TreeNode) As Integer
        Dim count As Integer = 0
        ' count the root node, if checked

        If rootNode.Checked Then count = 1
        ' check the child nodes, by recursively calling this function for all of 
        ' them
        Dim tvn As TreeNode = Nothing
        For Each tvn In rootNode.Nodes
            count += TreeView_CountCheckedAll(tvn)
        Next
        tvn = Nothing
        Return count
    End Function

    Public Function Treeview_TextSearch(ByVal SearchString As String, ByVal Nodes As TreeNodeCollection, Optional ByVal ExactMatch As Boolean = False) As TreeNode

        Dim ret As TreeNode
        For Each tn As TreeNode In Nodes
            If ExactMatch = True Then
                If tn.Text.ToLower = SearchString.ToLower Then Return tn
            Else
                If tn.Text.IndexOf(SearchString) <> -1 Then Return tn
            End If

            If tn.Nodes.Count > 0 Then
                ret = Treeview_TextSearch(SearchString, tn.Nodes, ExactMatch)
                If Not ret Is Nothing Then Return ret
            End If
        Next
        Return Nothing

    End Function

#Region "Chart Configuration"

    Private Sub CustomCharts_Serie_Insert(ByVal SeriesName As String, ByVal SerieColor As Integer, ByVal SerieType As String, ByVal SerieForm As String, ByVal yaxis_leftright As String,
                                          ByVal yaxis_left_label As String, ByVal yaxis_precision As String, ByVal yaxis_ABdPerc As String, ByVal calculatedSeriesTypeID As String, ByVal calculatedSeriesTypeName As String,
                                          ByVal calculatedSeriesParamValues As String, ByVal lineThickness As String, ByVal isYaxisAutoCalculated As String, ByVal axisFont As String, Optional ByVal serieorder As String = "")
        Try

            Dim tlvnode As TreeListViewNode = New TreeListViewNode(SeriesName)
            Dim tlvnode_sub0 As TreeListViewSubItem = New TreeListViewSubItem(SeriesName)
            tlvnode_sub0.Tag = SeriesName
            Dim tlvnode_sub1 As TreeListViewSubItem = New TreeListViewSubItem(SerieType)
            Dim tlvnode_sub2 As TreeListViewSubItem = New TreeListViewSubItem(SerieForm)
            Dim tlvnode_sub3 As TreeListViewSubItem = New TreeListViewSubItem(SerieColor)
            Dim tlvnode_sub4 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_leftright)
            Dim tlvnode_sub5 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_precision)
            Dim tlvnode_sub6 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_ABdPerc)
            Dim tlvnode_sub7 As TreeListViewSubItem = New TreeListViewSubItem(yaxis_left_label)
            Dim tlvnode_sub8 As TreeListViewSubItem = New TreeListViewSubItem(serieorder)
            Dim tlvnode_sub9 As TreeListViewSubItem = New TreeListViewSubItem(calculatedSeriesTypeName)
            tlvnode_sub9.Tag = IIf(calculatedSeriesTypeID = "", "0", calculatedSeriesTypeID)
            Dim tlvnode_sub10 As TreeListViewSubItem = New TreeListViewSubItem(calculatedSeriesParamValues)
            Dim tlvnode_sub11 As TreeListViewSubItem = New TreeListViewSubItem(lineThickness)
            Dim tlvnode_sub12 As TreeListViewSubItem = New TreeListViewSubItem(isYaxisAutoCalculated)
            Dim tlvnode_sub13 As TreeListViewSubItem = New TreeListViewSubItem(axisFont)
            '' tlvnode_sub10.Tag = New TreeListViewSubItem(calculatedSeriesParamValues)

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
            tlvnode.Selected = True

            tlvSandboxChartsSeries.Nodes.Add(tlvnode)

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

    End Sub

    Private Sub RefreshChartSeriesTLV(ByRef tlv As TreeListView)
        tlv.UpdateCurrentView()
        For Each col As TreeListViewColumn In tlv.Columns
            tlv.AutoSizeColumn(col)
        Next
        tlv.Columns(0).Width = tlv.Columns(0).Width + 10
        tlv.ResumeUpdate()
    End Sub

    Private Sub spinEdit_DashboradReportPerRow_ValueChanged(sender As Object, e As EventArgs) Handles spinEdit_DashboradReportPerRow.ValueChanged
        Try
            ResizeDashboardCharts()
        Catch
        End Try
    End Sub

    Private Sub ResizeDashboardCharts()
        If (spinEdit_DashboradReportPerRow.Value > 0) Then
            If (xtcDashboards.TabPages.Count > 0) Then
                For Each xtabPage As XtraTabPage In xtcDashboards.TabPages
                    If (xtabPage IsNot Nothing) Then
                        If (xtabPage.Controls.Count > 0) Then
                            Dim flpDashboardChartGrid As FlowLayoutPanel = TryCast(xtabPage.Controls(0), FlowLayoutPanel)
                            If (flpDashboardChartGrid IsNot Nothing) Then
                                If (flpDashboardChartGrid.Controls.Count > 0) Then
                                    For Each rcgControl As Control In flpDashboardChartGrid.Controls
                                        Dim rcg As ReportChartGrid = TryCast(rcgControl, ReportChartGrid)
                                        If (rcg IsNot Nothing) Then
                                            Dim widthValue As Integer = (flpDashboardChartGrid.Width - 20) / spinEdit_DashboradReportPerRow.Value - ((spinEdit_DashboradReportPerRow.Value - 1) * 2) - 5
                                            'Dim heightValue As Integer = widthValue / 2
                                            Dim heightValue As Integer = rcg.Size.Height
                                            rcg.Size = New Size(widthValue, heightValue)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnSandBoxChartSeriesApply_Click(sender As Object, e As EventArgs) Handles btnSandBoxChartSeriesApply.Click
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If (dtChartConfigSandbox.IsValid) Then
                Dim dtConfig As DataTable = dtChartConfigSandbox.Clone()

                Dim tech As String = cmbReportTechnology.SelectedItem.ToString
                Dim aggr_to As String = cmbObjectType.SelectedItem.ToString
                Dim objectString As String = GetChecked2String(tvObjects, tech, aggr_to, "Naked")

                ' Set Chart Config Data
                For Each nodeTLV As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                    Dim drow As DataRow = dtConfig.NewRow()
                    drow("ReportID") = dtChartConfigSandbox.Rows(0)("ReportID").ToString
                    drow("ChartType") = cmbChartType.SelectedItem.ToString
                    drow("ChartTitle") = lblSelectedReport.Text.Trim
                    drow(ReportChartFields.SeriesName) = nodeTLV.Text.ToUpper
                    drow(ReportChartFields.SeriesChartType) = nodeTLV.SubItems(1).Text
                    drow(ReportChartFields.AxisLocation) = nodeTLV.SubItems(4).Text
                    drow(ReportChartFields.AxisLabel) = nodeTLV.SubItems(7).Text
                    drow(ReportChartFields.AxisAbsPerc) = nodeTLV.SubItems(6).Text
                    drow(ReportChartFields.AxisPrecision) = nodeTLV.SubItems(5).Text
                    drow(ReportChartFields.AxisScaleProp) = nodeTLV.SubItems(2).Text
                    drow(ReportChartFields.SeriesColor) = nodeTLV.SubItems(3).Text
                    drow(ReportChartFields.SortOrder) = nodeTLV.SubItems(8).Text
                    drow("ReportName") = dtChartConfigSandbox.Rows(0)("ReportName").ToString
                    drow("ReportSQL") = dtChartConfigSandbox.Rows(0)("ReportSQL").ToString
                    drow("ReportConnString") = dtChartConfigSandbox.Rows(0)("ReportConnString").ToString
                    drow("TimeResolution") = dtChartConfigSandbox.Rows(0)("TimeResolution").ToString
                    drow("TechnologyPackageName") = dtChartConfigSandbox.Rows(0)("TechnologyPackageName").ToString
                    drow(ReportChartFields.Calculated_Series_Type_ID) = nodeTLV.SubItems(9).Tag
                    drow(ReportChartFields.Calculated_Series_Type_Name) = nodeTLV.SubItems(9).Text
                    drow(ReportChartFields.Calculated_Series_Type_Parameters) = nodeTLV.SubItems(10).Text
                    drow("StatisticsOrThreshold") = dtChartConfigSandbox.Rows(0)("StatisticsOrThreshold").ToString
                    drow("AxisFont") = nodeTLV.SubItems(13).Text.Trim
                    drow("LineSize") = nodeTLV.SubItems(11).Text.Trim
                    drow("IsAutoScale") = nodeTLV.SubItems(12).Text.Trim
                    drow("ObjectNamesInReport") = objectString

                    dtConfig.Rows.Add(drow)
                Next

                If (dtReportAxisData.IsValid) Then
                    BindChart(dtConfig, True)
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub ChartConfigData_Insert(ByVal reportId As String, ByVal chartType As String, ByVal chartName As String, ByVal serieAxisLocation As String, ByVal axisLable As String, ByVal absPerc As String,
                                       ByVal chartPrecision As String, ByVal serieScaleProp As String, ByVal seriesName As String, ByVal serieAxisType As String, ByVal chartElementsColor As Integer,
                                       ByVal serieOrder As String, ByVal calSeriesTypeId As Integer, ByVal calSeriesParamValues As String, ByVal lineThickness As String, ByVal calculatedYAxis As String, ByVal chartAxisFont As String)
        Try
            ''ModuleWaitDialog
            Dim sqlCommand As String = SQLReportChart.ReportChartData_Insert(reportId, chartType, chartName, serieAxisLocation, axisLable, absPerc, chartPrecision, serieScaleProp, seriesName, serieAxisType, chartElementsColor,
                                                                             serieOrder, calSeriesTypeId, calSeriesParamValues, lineThickness, calculatedYAxis, chartAxisFont)
            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlCommand)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub ChartConfigData_Delete(ByVal reportId As String)
        Try
            DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReportChart.ReportChartData_Delete(reportId))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub GetChartConfigData(ByRef dtChartConfig As DataTable)
        Try
            tlvSandboxChartsSeries.Nodes.Clear()

            If (dtChartConfig.IsValid) Then
                For Each dr As DataRow In dtChartConfig.Rows
                    CustomCharts_Serie_Insert(dr(ReportChartFields.SeriesName).ToString, dr(ReportChartFields.SeriesColor).ToString, dr(ReportChartFields.SeriesChartType).ToString,
                    dr(ReportChartFields.AxisScaleProp).ToString, dr(ReportChartFields.AxisLocation).ToString, dr(ReportChartFields.AxisLabel).ToString, dr(ReportChartFields.AxisPrecision).ToString,
                    dr(ReportChartFields.AxisAbsPerc).ToString, dr(ReportChartFields.Calculated_Series_Type_ID).ToString, dr(ReportChartFields.Calculated_Series_Type_Name).ToString,
                    dr(ReportChartFields.Calculated_Series_Type_ParamValues).ToString, dr(ReportChartFields.LineSize).ToString, dr(ReportChartFields.IsAutoScale).ToString,
                    dr(ReportChartFields.AxisFont).ToString, dr(ReportChartFields.SortOrder).ToString)
                Next
            Else
                tlvSandboxChartsSeries.Nodes.Clear()
            End If
            RefreshChartSeriesTLV(tlvSandboxChartsSeries)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub GetChartConfigData(ByVal reportId As String)
        Try
            tlvSandboxChartsSeries.Nodes.Clear()
            Dim dtChartConfig As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportChart.GetReportChartData(reportId))
            If (dtChartConfig.IsValid) Then
                For Each dr As DataRow In dtChartConfig.Rows
                    CustomCharts_Serie_Insert(dr(ReportChartFields.SeriesName).ToString, dr(ReportChartFields.SeriesColor).ToString, dr(ReportChartFields.SeriesChartType).ToString,
                    dr(ReportChartFields.AxisScaleProp).ToString, dr(ReportChartFields.AxisLocation).ToString, dr(ReportChartFields.AxisLabel).ToString, dr(ReportChartFields.AxisPrecision).ToString,
                    dr(ReportChartFields.AxisAbsPerc).ToString, dr(ReportChartFields.Calculated_Series_Type_ID).ToString, dr(ReportChartFields.Calculated_Series_Type_Name).ToString,
                    dr(ReportChartFields.Calculated_Series_Type_ParamValues).ToString, dr(ReportChartFields.LineSize).ToString, dr(ReportChartFields.IsAutoScale).ToString, dr(ReportChartFields.AxisFont).ToString,
                    dr(ReportChartFields.SortOrder).ToString)
                Next
            Else
                tlvSandboxChartsSeries.Nodes.Clear()
            End If
            RefreshChartSeriesTLV(tlvSandboxChartsSeries)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnSandboxChartSeriesRemove_Click(sender As Object, e As EventArgs) Handles btnSandBoxChartSeriesRemove.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If Not tlvSandboxChartsSeries.SelectedNode Is Nothing Then
                tlvSandboxChartsSeries.SelectedNode.Remove()
            End If

            RefreshChartSeriesTLV(tlvSandboxChartsSeries)

            If ceAutoRefreshChart.Checked = True Then
                RefreshChartAndGrid(False)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnSandBoxChartSeriesUp_Click(sender As Object, e As EventArgs) Handles btnSandBoxChartSeriesUp.Click
        If Not tlvSandboxChartsSeries.SelectedNode Is Nothing Then
            tlvSandboxChartsSeries.SelectedNode.Move(TreeNodeMoveDirection.Up)
        End If
    End Sub

    Private Sub btnSandBoxChartSeriesDown_Click(sender As Object, e As EventArgs) Handles btnSandBoxChartSeriesDown.Click
        If Not tlvSandboxChartsSeries.SelectedNode Is Nothing Then
            tlvSandboxChartsSeries.SelectedNode.Move(TreeNodeMoveDirection.Down)
        End If
    End Sub

    Private Sub tlvSandboxChartsSeries_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvSandboxChartsSeries.SubItemSelectionChanged
        isChartSerieSelected = False
        Try
            Select Case tlvSandboxChartsSeries.SelectedNode.SubItems(1).Text.Trim.ToLower
                Case "bar"
                    cmbChartConfig_SerieType.SelectedItem = cmbChartConfig_SerieType.Properties.Items(0)
                Case "line"
                    cmbChartConfig_SerieType.SelectedItem = cmbChartConfig_SerieType.Properties.Items(1)
            End Select
            Select Case tlvSandboxChartsSeries.SelectedNode.SubItems(2).Text.Trim.ToLower
                Case "normal"
                    cmbChartConfig_SeriesAxisType.SelectedItem = cmbChartConfig_SeriesAxisType.Properties.Items(0)
                Case "stacked"
                    cmbChartConfig_SeriesAxisType.SelectedItem = cmbChartConfig_SeriesAxisType.Properties.Items(1)
                Case "fullstacked"
                    cmbChartConfig_SeriesAxisType.SelectedItem = cmbChartConfig_SeriesAxisType.Properties.Items(2)
            End Select

            colEditChartConfig_SeriesColor.Color = ColorTranslator.FromOle(CInt(tlvSandboxChartsSeries.SelectedNode.SubItems(3).Text))
            Select Case tlvSandboxChartsSeries.SelectedNode.SubItems(4).Text.Trim.ToLower
                Case "left"
                    cmbChartConfig_SeriesAxis.SelectedItem = cmbChartConfig_SeriesAxis.Properties.Items(0)
                Case "right"
                    cmbChartConfig_SeriesAxis.SelectedItem = cmbChartConfig_SeriesAxis.Properties.Items(1)
            End Select

            spinEditChartConfig_ChartPrecision.Text = CInt(tlvSandboxChartsSeries.SelectedNode.SubItems(5).Text.Trim)

            Select Case tlvSandboxChartsSeries.SelectedNode.SubItems(6).Text.Trim.ToLower
                Case "abs"
                    cmbChartConfig_AbsPerc.SelectedItem = cmbChartConfig_AbsPerc.Properties.Items(0)
                Case "perc"
                    cmbChartConfig_AbsPerc.SelectedItem = cmbChartConfig_AbsPerc.Properties.Items(1)
            End Select

            txtChartConfig_AxisLabel.Text = tlvSandboxChartsSeries.SelectedNode.SubItems(7).Text.Trim

            Select Case tlvSandboxChartsSeries.SelectedNode.SubItems(8).Text.Trim.ToLower
                Case "asc"
                    cmbChartConfig_SeriesOrder.SelectedItem = cmbChartConfig_SeriesOrder.Properties.Items(1)
                Case "desc"
                    cmbChartConfig_SeriesOrder.SelectedItem = cmbChartConfig_SeriesOrder.Properties.Items(2)
                Case ""
                    cmbChartConfig_SeriesOrder.SelectedItem = cmbChartConfig_SeriesOrder.Properties.Items(0)
            End Select

            txtChartAxisFont.Text = tlvSandboxChartsSeries.SelectedNode.SubItems(13).Text.Trim

            spinEdit_LineThickness.Value = tlvSandboxChartsSeries.SelectedNode.SubItems(11).Text.Trim
            If (tlvSandboxChartsSeries.SelectedNode.SubItems(12).Text.Trim.ToLower = "true") Then
                cmbCalculatedYAxis.SelectedItem = cmbCalculatedYAxis.Properties.Items(0)
            Else
                cmbCalculatedYAxis.SelectedItem = cmbCalculatedYAxis.Properties.Items(1)
            End If

        Catch
        End Try
        isChartSerieSelected = True
    End Sub

    Private Sub cmbChartConfig_SerieType_SelectedItemChanged(sender As Object, e As EventArgs) Handles cmbChartConfig_SerieType.SelectedValueChanged
        If isChartSerieSelected = True Then
            Try
                Dim colorIntValue As Integer = ColorTranslator.ToOle(colEditChartConfig_SeriesColor.Color)
                ChartConfigSeries_Update(colorIntValue,
                                          cmbChartConfig_SerieType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxisType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxis.SelectedItem.ToString,
                                          txtChartConfig_AxisLabel.Text,
                                          spinEditChartConfig_ChartPrecision.Value,
                                          cmbChartConfig_AbsPerc.SelectedItem.ToString,
                                          IIf(cmbChartConfig_SeriesOrder.SelectedItem.ToString.ToLower = "none", "", cmbChartConfig_SeriesOrder.SelectedItem.ToString)
                                        )
            Catch
            End Try
        End If
    End Sub

    Private Sub cmbChartConfig_SeriesOrder_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbChartConfig_SeriesOrder.SelectedValueChanged
        If isChartSerieSelected = True Then
            Try
                Dim colorIntValue As Integer = ColorTranslator.ToOle(colEditChartConfig_SeriesColor.Color)
                ChartConfigSeries_Update(colorIntValue,
                                          cmbChartConfig_SerieType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxisType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxis.SelectedItem.ToString,
                                          txtChartConfig_AxisLabel.Text,
                                          spinEditChartConfig_ChartPrecision.Value,
                                          cmbChartConfig_AbsPerc.SelectedItem.ToString,
                                          IIf(cmbChartConfig_SeriesOrder.SelectedItem.ToString.ToLower = "none", "", cmbChartConfig_SeriesOrder.SelectedItem.ToString)
                                        )
            Catch
            End Try
        End If
    End Sub

    Private Sub cmbChartConfig_SeriesAxisType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbChartConfig_SeriesAxisType.SelectedValueChanged
        If isChartSerieSelected = True Then
            ChartConfigSeries_UpdateAxis(2, cmbChartConfig_SeriesAxisType.SelectedItem.ToString)
            tlvSandboxChartsSeries.Refresh()
        End If
    End Sub

    Private Sub txtChartConfig_AxisLabel_TextChanged(sender As Object, e As EventArgs) Handles txtChartConfig_AxisLabel.TextChanged
        If isChartSerieSelected = True Then
            Try
                ChartConfigSeries_UpdateAxis(7, txtChartConfig_AxisLabel.Text)
                tlvSandboxChartsSeries.Refresh()
            Catch
            End Try
        End If
    End Sub

    Private Sub colEditChartConfig_SeriesColor_SelectedColorChanged(sender As Object, e As EventArgs) Handles colEditChartConfig_SeriesColor.ColorChanged
        If isChartSerieSelected = True Then
            Try
                Dim colorIntValue As Integer = ColorTranslator.ToOle(colEditChartConfig_SeriesColor.Color)
                ChartConfigSeries_Update(colorIntValue,
                                          cmbChartConfig_SerieType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxisType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxis.SelectedItem.ToString,
                                          txtChartConfig_AxisLabel.Text,
                                          spinEditChartConfig_ChartPrecision.Value,
                                          cmbChartConfig_AbsPerc.SelectedItem.ToString,
                                          IIf(cmbChartConfig_SeriesOrder.SelectedItem.ToString.ToLower = "none", "", cmbChartConfig_SeriesOrder.SelectedItem.ToString)
                                        )
            Catch
            End Try
        End If
    End Sub

    Private Sub spinEditChartConfig_ChartPrecision_ValueChanged(sender As Object, e As EventArgs) Handles spinEditChartConfig_ChartPrecision.ValueChanged
        If isChartSerieSelected = True Then
            Try
                ChartConfigSeries_UpdateAxis(5, spinEditChartConfig_ChartPrecision.Value)
                tlvSandboxChartsSeries.Refresh()
            Catch
            End Try
        End If
    End Sub

    Private Sub cmbChartConfig_AbsPerc_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbChartConfig_AbsPerc.SelectedValueChanged
        If isChartSerieSelected = True Then
            Try
                ChartConfigSeries_UpdateAxis(6, cmbChartConfig_AbsPerc.SelectedItem.ToString)
                tlvSandboxChartsSeries.Refresh()
            Catch
            End Try
        End If
    End Sub

    Private Sub cmbChartConfig_SeriesAxis_TextChanged(sender As Object, e As EventArgs) Handles cmbChartConfig_SeriesAxis.TextChanged
        If isChartSerieSelected = True Then
            Try
                Dim colorIntValue As Integer = ColorTranslator.ToOle(colEditChartConfig_SeriesColor.Color)
                ChartConfigSeries_Update(colorIntValue,
                                          cmbChartConfig_SerieType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxisType.SelectedItem.ToString,
                                          cmbChartConfig_SeriesAxis.SelectedItem.ToString,
                                          txtChartConfig_AxisLabel.Text,
                                          spinEditChartConfig_ChartPrecision.Value,
                                          cmbChartConfig_AbsPerc.SelectedItem.ToString,
                                          IIf(cmbChartConfig_SeriesOrder.SelectedItem.ToString.ToLower = "none", "", cmbChartConfig_SeriesOrder.SelectedItem.ToString)
                                        )
            Catch
            End Try
        End If
    End Sub

    Private Sub ChartConfigSeries_Update(ByVal SerieColor As Integer, ByVal SerieType As String, ByVal SerieForm As String, ByVal yaxis_leftright As String, ByVal yaxis_left_label As String, ByVal yaxis_precision As String,
                                         ByVal yaxis_ABdPerc As String, Optional ByVal serieorder As String = "")
        Try

            If (tlvSandboxChartsSeries.SelectedNodes IsNot Nothing) Then
                For Each tlvnode As TreeListViewNode In tlvSandboxChartsSeries.SelectedNodes

                    tlvnode.SubItems(1).Text = SerieType
                    tlvnode.SubItems(2).Text = SerieForm
                    tlvnode.SubItems(3).Text = SerieColor
                    tlvnode.SubItems(4).Text = yaxis_leftright
                    tlvnode.SubItems(5).Text = yaxis_precision
                    tlvnode.SubItems(6).Text = yaxis_ABdPerc
                    tlvnode.SubItems(7).Text = yaxis_left_label
                    tlvnode.SubItems(8).Text = serieorder
                    tlvSandboxChartsSeries.Refresh()

                Next
                If ceAutoRefreshChart.Checked = True Then
                    RefreshChartAndGrid(False)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub ChartConfigSeries_UpdateAxis(ByVal colindex As Integer, ByVal replaceto As String)
        Try
            Dim colName As String = "0"
            If (colindex = 2) Then
                colName = ReportChartFields.SeriesChartType
            ElseIf (colindex = 5) Then
                colName = ReportChartFields.AxisPrecision
            ElseIf (colindex = 6) Then
                colName = ReportChartFields.AxisAbsPerc
            ElseIf (colindex = 7) Then
                colName = ReportChartFields.AxisLabel
            ElseIf (colindex = 10) Then
                colName = ReportChartFields.LineSize
            ElseIf (colindex = 11) Then
                colName = ReportChartFields.IsAutoScale
            ElseIf (colindex = 12) Then
                colName = ReportChartFields.AxisFont
            End If
            For Each nd As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                If nd.SubItems(4).Text.Trim.ToUpper = cmbChartConfig_SeriesAxis.Text.ToUpper Then
                    nd.SubItems(colindex).Text = replaceto
                    If (Not colName = "0") Then
                        Dim oRowsInTarget As DataRow() = dtChartConfigSandbox.Select(ReportChartFields.SeriesName & " = '" & nd.SubItems(0).Text & "'")
                        If oRowsInTarget.Count > 0 Then
                            For Each dr As DataRow In oRowsInTarget
                                dr(colName) = replaceto
                            Next
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub RemoveChartConfigSetting(ByVal chartTitle As String)
        Try
            If (tlvSandboxChartsSeries.Nodes.Count > 0) Then
                Dim removeableNode As TreeListViewNode = New TreeListViewNode()
                For Each tlvnode As TreeListViewNode In tlvSandboxChartsSeries.Nodes
                    If (tlvnode.SubItems(0).Text.ToUpper = chartTitle.ToUpper) Then
                        removeableNode = tlvnode
                        Exit For
                    End If
                Next
                If removeableNode IsNot Nothing Then
                    tlvSandboxChartsSeries.Nodes.Remove(removeableNode)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub spinEdit_LineThickness_ValueChanged(sender As Object, e As EventArgs) Handles spinEdit_LineThickness.ValueChanged
        If isChartSerieSelected = True Then
            Try
                ''ChartConfigSeries_UpdateAxis(11, vNUD_LineThickness.Value)
                If (tlvSandboxChartsSeries.SelectedNode IsNot Nothing) Then

                    Dim nd As TreeListViewNode = tlvSandboxChartsSeries.SelectedNode
                    If nd.SubItems(4).Text.Trim.ToUpper = cmbChartConfig_SeriesAxis.Text.ToUpper Then
                        nd.SubItems(11).Text = spinEdit_LineThickness.Value
                        Dim oRowsInTarget As DataRow() = dtChartConfigSandbox.Select(ReportChartFields.SeriesName & " = '" & nd.SubItems(0).Text & "'")
                        If oRowsInTarget.Count > 0 Then
                            For Each dr As DataRow In oRowsInTarget
                                dr(ReportChartFields.LineSize) = spinEdit_LineThickness.Value
                            Next
                        End If
                    End If
                    tlvSandboxChartsSeries.Refresh()
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub cmbCalculatedYAxis_TextChanged(sender As Object, e As EventArgs) Handles cmbCalculatedYAxis.TextChanged
        If isChartSerieSelected = True Then
            Try
                If (tlvSandboxChartsSeries.SelectedNode IsNot Nothing) Then
                    Dim nd As TreeListViewNode = tlvSandboxChartsSeries.SelectedNode
                    nd.SubItems(12).Text = cmbCalculatedYAxis.SelectedItem.ToString
                    Dim oRowsInTarget As DataRow() = dtChartConfigSandbox.Select(ReportChartFields.SeriesName & " = '" & nd.SubItems(0).Text & "'")
                    If oRowsInTarget.Count > 0 Then
                        For Each dr As DataRow In oRowsInTarget
                            dr(ReportChartFields.IsAutoScale) = cmbCalculatedYAxis.SelectedItem.ToString
                        Next
                    End If
                    tlvSandboxChartsSeries.Refresh()
                End If
            Catch
            End Try
        End If
    End Sub

#End Region

#Region "Build SQL Query"

    Private Function Make_SourceTable() As List(Of String)
        Dim sourceTables_list As List(Of String) = New List(Of String)
        Dim vSandBoxFieldTemp As DevExSandBoxField = New DevExSandBoxField()
        For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
            vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxFieldTemp IsNot Nothing) Then
                If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Counter) Then
                    If (vSandBoxFieldTemp.SQL_SourceTable.Count = 1) Then
                        SandBoxListOfString.InsertIntoList(sourceTables_list, vSandBoxFieldTemp.SQL_SourceTable.ToArray)
                    Else
                        sourceTables_list.Add(String.Join(",", vSandBoxFieldTemp.SQL_SourceTable))
                    End If

                ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Kpi) Then
                    If (vSandBoxFieldTemp.SQL_SourceTable.Count = 1) Then
                        SandBoxListOfString.InsertIntoList(sourceTables_list, vSandBoxFieldTemp.SQL_SourceTable.ToArray)
                    Else
                        sourceTables_list.Add(String.Join(",", vSandBoxFieldTemp.SQL_SourceTable))
                    End If

                ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.ObjectFld) Then

                End If
            End If
        Next

        '   Dim dict_SourceObjects As New Dictionary(Of String, List(Of String))
        Dim lst_OfUniqueMeas As New List(Of String)

        For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
            vSandBoxFieldTemp = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxFieldTemp IsNot Nothing) Then
                If (vSandBoxFieldTemp.SQL_SourceTable IsNot Nothing) Then
                    If (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Counter) Then
                        'If (vSandBoxFieldTemp.SQL_SourceTable.Count = 1) Then
                        '    SandBoxListOfString.InsertIntoList(sourceTables_list, vSandBoxFieldTemp.SQL_SourceTable.ToArray)
                        'Else
                        '    sourceTables_list.Add(String.Join(",", vSandBoxFieldTemp.SQL_SourceTable))
                        'End If
                        For Each meas As String In vSandBoxFieldTemp.SQL_SourceTable
                            If Not lst_OfUniqueMeas.Contains(meas) Then
                                lst_OfUniqueMeas.Add(meas)
                            End If
                        Next
                    ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.Kpi) Then
                        'dicttionary was tried to build only new select statement when other sourceobjectID is detected. HOwever sourceobjectid is not available in KPI... perhaps later
                        'If dict_SourceObjects.ContainsKey(vSandBoxFieldTemp.SourceObjectID) Then
                        '    For Each meas As String In vSandBoxFieldTemp.SQL_SourceTable
                        '        If Not dict_SourceObjects(vSandBoxFieldTemp.SourceObjectID).Contains(meas) Then
                        '            Dim lst As List(Of String) = dict_SourceObjects(vSandBoxFieldTemp.SourceObjectID)
                        '            lst.Add(meas)
                        '            dict_SourceObjects.Remove(vSandBoxFieldTemp.SourceObjectID)
                        '            dict_SourceObjects.Add(vSandBoxFieldTemp.SourceObjectID, lst)
                        '        End If
                        '    Next

                        'Else
                        '    dict_SourceObjects.Add(vSandBoxFieldTemp.SourceObjectID, vSandBoxFieldTemp.SQL_SourceTable)
                        'End If

                        'this is commented out as we don't want new select statements built when KPIs are coming from same sourceobject. we assume now reports are only built using same sourceobject.
                        'If (vSandBoxFieldTemp.SQL_SourceTable.Count = 1) Then
                        '    SandBoxListOfString.InsertIntoList(sourceTables_list, vSandBoxFieldTemp.SQL_SourceTable.ToArray)
                        'Else
                        '    sourceTables_list.Add(String.Join(",", vSandBoxFieldTemp.SQL_SourceTable))
                        'End If

                        For Each meas As String In vSandBoxFieldTemp.SQL_SourceTable
                            If Not lst_OfUniqueMeas.Contains(meas) Then
                                lst_OfUniqueMeas.Add(meas)
                            End If
                        Next
                    ElseIf (vSandBoxFieldTemp.VSandBoxType = DatamartFieldType.ObjectFld) Then

                    End If
                End If
            End If
        Next
        sourceTables_list.Add(String.Join(",", lst_OfUniqueMeas))
        Return sourceTables_list.ToList
    End Function

    Private Function Make_ConnString(ByVal lst_sourcetables As List(Of String)) As List(Of String)
        Dim connstring As New List(Of String)
        Try
            For Each st In lst_sourcetables
                Dim dr() As DataRow = dt_TechPackCounter.Select("SQL_SourceTable = '" + st.Split(",")(0) + "'")
                If Not dr Is Nothing Then
                    If dr.Count >= 1 Then
                        connstring.Add(dr(0)("SQL_ConnString"))
                    End If
                End If
            Next
        Catch ex As Exception
            Return Nothing
        End Try

        Return connstring

    End Function

    Private Function Make_ExportConnString(ByVal lst_sourcetables As List(Of String)) As List(Of String)
        Dim connstring As New List(Of String)
        Try
            For Each st In lst_sourcetables
                Dim dr() As DataRow = dt_TechPackCounter.Select("SQL_SourceTable = '" + st.Split(",")(0) + "'")
                If Not dr Is Nothing Then
                    If dr.Count >= 1 Then
                        connstring.Add(dr(0)("SQL_DatabaseName"))
                    End If
                End If
            Next
        Catch ex As Exception
            Return Nothing
        End Try

        Return connstring

    End Function

    Private Function SQL_SelectPart(ByVal sourceTable As String) As String

        Dim select_fieldTemp As String = String.Empty
        Dim vSandBoxElement As DevExSandBoxField = New DevExSandBoxField()
        Dim isXPeriodStartTime As Boolean = False
        For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
            vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxElement IsNot Nothing) Then
                If (vSandBoxElement.VSandBoxType = DatamartFieldType.Time) Then
                    isXPeriodStartTime = True
                    Exit For
                End If
            End If
        Next
        ''For SQL Select X
        For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
            vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxElement IsNot Nothing) Then
                If (isXPeriodStartTime) Then
                    If Not select_fieldTemp.Contains("PERIOD_START_TIME") Then
                        Dim guiTimeResolution As String = cmbTimeResolution.SelectedItem.ToString
                        Dim guiObjectTableType As String = cmbObjectType.SelectedItem.ToString
                        Dim PeriodStarTimeWithTimeagg As String = GetTimeSuffix(sourceTable, guiTimeResolution, guiObjectTableType)(1)
                        If PeriodStarTimeWithTimeagg IsNot "" Then
                            select_fieldTemp = select_fieldTemp + Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable.Split(",")(0) & ".PERIOD_START_TIME") & " AS PERIOD_START_TIME, " & vbLf
                            sqlSelectColList.Add(Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable.Split(",")(0) & ".PERIOD_START_TIME"), New List(Of String)(New String() {"PERIOD_START_TIME", "X"}))
                        End If
                        'select_fieldTemp = select_fieldTemp + Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable.Split(",")(0) & ".PERIOD_START_TIME") & " AS PERIOD_START_TIME, " & vbLf
                    End If
                End If
                If (vSandBoxElement.SQL_SourceTable IsNot Nothing) Then
                    If (String.Join(",", vSandBoxElement.SQL_SourceTable) = sourceTable) Then
                        If (vSandBoxElement.VSandBoxType = DatamartFieldType.Counter) Then
                            If vSandBoxElement.ObjectAggregation.Length > 3 Then
                                select_fieldTemp = select_fieldTemp & vSandBoxElement.ObjectAggregation & " AS [" & vSandBoxElement.Text & "]," & vbLf
                                sqlSelectColList.Add(vSandBoxElement.ObjectAggregation, New List(Of String)(New String() {vSandBoxElement.Text, "X"}))
                            Else
                                select_fieldTemp = select_fieldTemp & vSandBoxElement.ObjectAggregation & "(" & sourceTable.Split(",")(0) & ".[" & vSandBoxElement.Text & "]) AS [" & vSandBoxElement.Text & "]," & vbLf
                                sqlSelectColList.Add(vSandBoxElement.ObjectAggregation & "(" & sourceTable.Split(",")(0) & ".[" & vSandBoxElement.Text & "])", New List(Of String)(New String() {vSandBoxElement.Text, "X"}))
                            End If
                        ElseIf (vSandBoxElement.VSandBoxType = DatamartFieldType.Kpi) Then
                            select_fieldTemp = select_fieldTemp & vSandBoxElement.SQL_KPIFormula + " AS " + Chr(34) + vSandBoxElement.Text + Chr(34) + "," & vbLf
                            sqlSelectColList.Add(vSandBoxElement.SQL_KPIFormula, New List(Of String)(New String() {vSandBoxElement.Text, "X"}))
                        End If
                    End If
                End If
                If vSandBoxElement.VSandBoxType = DatamartFieldType.ObjectFld Then
                    'Object is by default mapped to Fielname 'Objectname'. What needs to be known is the viewname that relates to the requested objectname, to avoid ambuigity.
                    If (cmbCMPM.SelectedItem.ToString.ToUpper = "CM") Then
                        Dim selectCMD As String = SQLTechnologyObjectTypes.GetObjectViewCMorPM(vSandBoxElement.SourceObjectID, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
                        Dim dtObjectViewCMorPM As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)

                        If IsVSandBoxFieldExistX(cmbObjectType.SelectedItem.ToString) = True And IsVSandBoxFieldAnObjectType(vSandBoxElement.Text) Then
                            select_fieldTemp = select_fieldTemp & dtObjectViewCMorPM(0)("ObjectViewForCM") & ".ObjectName AS [" & vSandBoxElement.Text & "]," & vbLf
                            sqlSelectColList.Add(dtObjectViewCMorPM(0)("ObjectViewForCM") & ".ObjectName", New List(Of String)(New String() {vSandBoxElement.Text, "X"}))
                        Else
                            select_fieldTemp = select_fieldTemp & dtObjectViewCMorPM(0)("ObjectViewForCM") & ".[" & vSandBoxElement.Text & "]," & vbLf
                            sqlSelectColList.Add(dtObjectViewCMorPM(0)("ObjectViewForCM") & "." + vSandBoxElement.Text, New List(Of String)(New String() {vSandBoxElement.Text, "X"}))
                        End If

                    Else
                        Dim sqlCMD As String = New SQLTechnologyMeasurements().SelectAll(False, TechnologyMeasurementsFields.SQL_SOURCE_TABLE & OperatorConst.Equal & Chr(39) & sourceTable & Chr(39))
                        Dim dtObjectType As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
                        select_fieldTemp = select_fieldTemp & " " & sourceTable.Split(",")(0) & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID) & " AS [" & vSandBoxElement.Text & "]," & vbLf
                        sqlSelectColList.Add(sourceTable & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID), New List(Of String)(New String() {vSandBoxElement.Text, "X"}))
                    End If

                End If
            End If

        Next

        ''For SQL Select Y
        For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
            vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxElement IsNot Nothing) Then
                If (vSandBoxElement.SQL_SourceTable IsNot Nothing) Then
                    If (isXPeriodStartTime) Then
                        If Not select_fieldTemp.Contains("PERIOD_START_TIME") Then
                            Dim guiTimeResolution As String = cmbTimeResolution.SelectedItem.ToString
                            Dim guiObjectTableType As String = cmbObjectType.SelectedItem.ToString
                            Dim PeriodStarTimeWithTimeagg As String = GetTimeSuffix(sourceTable, guiTimeResolution, guiObjectTableType)(1)
                            If PeriodStarTimeWithTimeagg IsNot "" Then
                                select_fieldTemp = select_fieldTemp + Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable.Split(",")(0) & ".PERIOD_START_TIME") & " AS PERIOD_START_TIME, " & vbLf
                                sqlSelectColList.Add(Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable.Split(",")(0) & ".PERIOD_START_TIME"), New List(Of String)(New String() {"PERIOD_START_TIME", "Y"}))
                            End If
                            'select_fieldTemp = select_fieldTemp + Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable.Split(",")(0) & ".PERIOD_START_TIME") & " AS PERIOD_START_TIME, " & vbLf
                        End If
                    End If
                    ' If String.Join(",", vSandBoxElement.SQL_SourceTable) = sourceTable Then
                    'If (sourceTable.Contains(String.Join(",", vSandBoxElement.SQL_SourceTable))) Then
                    If vSandBoxElement.SQL_SourceTable.Except(sourceTable.Split(",")).Count = 0 Then
                        If (vSandBoxElement.VSandBoxType = DatamartFieldType.Counter) Then
                            If vSandBoxElement.ObjectAggregation.Length > 3 Then
                                select_fieldTemp = select_fieldTemp & vSandBoxElement.ObjectAggregation & " AS [" & vSandBoxElement.Text & "]," & vbLf
                                sqlSelectColList.Add(vSandBoxElement.ObjectAggregation, New List(Of String)(New String() {vSandBoxElement.Text, "Y"}))
                            Else
                                select_fieldTemp = select_fieldTemp & vSandBoxElement.ObjectAggregation & "(" & vSandBoxElement.SQL_SourceTable(0) & ".[" & vSandBoxElement.Text & "]) AS [" & vSandBoxElement.Text & "]," & vbLf
                                sqlSelectColList.Add(vSandBoxElement.ObjectAggregation & "(" & sourceTable.Split(",")(0) & ".[" & vSandBoxElement.Text & "])", New List(Of String)(New String() {vSandBoxElement.Text, "Y"}))
                            End If

                        ElseIf (vSandBoxElement.VSandBoxType = DatamartFieldType.Kpi) Then
                            select_fieldTemp = select_fieldTemp & vSandBoxElement.SQL_KPIFormula + " AS " + Chr(34) + vSandBoxElement.Text + Chr(34) + "," & vbLf
                            ' extracting kpi formula
                            Dim temp As String = vSandBoxElement.SQL_KPIFormula.Substring(0, vSandBoxElement.SQL_KPIFormula.LastIndexOf(")") + 1)
                            Dim strAlias As String = vSandBoxElement.Text
                            sqlSelectColList.Add(Replace(temp, " ", "", 1), New List(Of String)(New String() {strAlias, "Y"}))
                        End If
                    End If
                End If
                If vSandBoxElement.VSandBoxType = DatamartFieldType.ObjectFld Then
                    If (cmbCMPM.SelectedItem.ToString.ToUpper = "CM") Then

                        Dim selectCMD As String = SQLTechnologyObjectTypes.GetObjectViewCMorPM(vSandBoxElement.SourceObjectID, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
                        Dim dtObjectViewCMorPM As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)

                        select_fieldTemp = select_fieldTemp & dtObjectViewCMorPM(0)("ObjectViewForCM") & ".ObjectName AS [" & vSandBoxElement.Text & "]," & vbLf
                        sqlSelectColList.Add(dtObjectViewCMorPM(0)("ObjectViewForCM") & ".ObjectName", New List(Of String)(New String() {vSandBoxElement.Text, "Y"}))
                    Else
                        Dim sqlCMD As String = New SQLTechnologyMeasurements().SelectAll(False, TechnologyMeasurementsFields.SQL_SOURCE_TABLE & OperatorConst.Equal & Chr(39) & sourceTable & Chr(39))
                        Dim dtObjectType As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
                        select_fieldTemp = sourceTable & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID) & " AS [" & vSandBoxElement.Text & "]," & vbLf
                        sqlSelectColList.Add(sourceTable & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID), New List(Of String)(New String() {vSandBoxElement.Text, "Y"}))
                    End If
                End If

            End If

        Next
        '' Next
        Return select_fieldTemp.Remove(select_fieldTemp.Count - 2, 2)
    End Function

    Private Function SQL_FromPart(ByVal sourceTable As String) As String
        Dim from_fieldTemp As String = String.Empty

        Dim guiTimeResolution As String = cmbTimeResolution.SelectedItem.ToString
        Dim timeaggregationSuffix As String = String.Empty

        Dim guiObjectTableType As String = cmbObjectType.SelectedItem.ToString
        Dim objectTypeTableSuffix As String = String.Empty
        Dim selectCMD As String = String.Empty
        Dim suffixTimeAndObject As List(Of String) = New List(Of String)

        ''if st is single table 
        Dim stFirst As String = ""
        Dim pkFirst As String = ""
        Dim suffixTimeAndObjectFrom_Field As String = String.Empty


        Dim suffixTime As String = ""
        Dim suffixObject As New List(Of String)

        If (sourceTable.Split(",").Count = 1) Then

            suffixTime = GetTimeSuffix(sourceTable, guiTimeResolution, guiObjectTableType)(0)
            suffixObject = GetObjectSuffix(sourceTable, guiTimeResolution, guiObjectTableType)

            If suffixTime <> "" And suffixObject(0) <> "_" + guiObjectTableType Then
                from_fieldTemp = "[" + sourceTable.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
            Else
                from_fieldTemp = "[" + sourceTable.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
            End If

            selectCMD = SQLTechnologyMeasurements.GetPrimaryKey(String.Join(",", sourceTable.ToArray).TrimEnd(", "), "")
            Dim measurementPrimaryKeyDt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)
            If Not measurementPrimaryKeyDt Is Nothing Then
                If measurementPrimaryKeyDt.Rows.Count > 0 Then
                    pkFirst = measurementPrimaryKeyDt(0)(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
                End If
                measurementPrimaryKeyDt.Dispose()
            End If
            stFirst = sourceTable
            suffixTimeAndObjectFrom_Field = from_fieldTemp + " " + sourceTable
            from_fieldTemp = suffixTimeAndObjectFrom_Field & " With(NOLOCK) "

        Else ''else if multiple tables

            suffixTime = ""

            Dim dict_suffixTimeAndObjectFrom As New Dictionary(Of String, String)

            For Each st As String In sourceTable.Split(",").ToArray
                suffixTime = GetTimeSuffix(st, guiTimeResolution, guiObjectTableType)(0)
                suffixObject = GetObjectSuffix(st, guiTimeResolution, guiObjectTableType)

                If suffixTime <> "" And suffixObject(0) <> "_" + guiObjectTableType Then
                    from_fieldTemp = "[" + st.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
                Else
                    from_fieldTemp = "[" + st.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
                End If
                dict_suffixTimeAndObjectFrom.Add(st, from_fieldTemp)
            Next

            Dim stList() As String = sourceTable.Split(",").ToArray
            Dim indexST As Integer = 0

            selectCMD = SQLTechnologyMeasurements.GetPrimaryKey(String.Join(",", sourceTable.ToArray), String.Join(",", dict_suffixTimeAndObjectFrom.Values.ToArray))
            Dim measurementPrimaryKeyDt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)
            Dim isFirstTime As Boolean = True
            If (measurementPrimaryKeyDt.IsValid) Then

                Dim stCurrent As String = ""
                Dim pkCurrent As String = ""

                from_fieldTemp = String.Empty
                For Each stDR As DataRow In measurementPrimaryKeyDt.Rows
                    If (isFirstTime) Then
                        stFirst = stDR(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
                        pkFirst = stDR(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
                        ''from_fieldTemp = stFirst
                    Else
                        stCurrent = stDR(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
                        pkCurrent = stDR(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
                    End If
                    Dim pkCounter As Integer = pkFirst.Split(",").Count '' If PrimaryKey has more then one 
                    If (Not isFirstTime) Then
                        If (pkCounter = 1) Then
                            from_fieldTemp = " INNER JOIN " & dict_suffixTimeAndObjectFrom.Item(stCurrent) & " " & stCurrent & " With(NOLOCK) On " & stFirst & "." & pkFirst & " = " & stCurrent & "." & pkCurrent
                        Else
                            from_fieldTemp = from_fieldTemp & " INNER JOIN " & dict_suffixTimeAndObjectFrom.Item(stCurrent) & " " & stCurrent & " With(NOLOCK) On " & stFirst & "." & pkFirst.Split(",")(0).ToString & " = " & stCurrent & "." & pkCurrent.Split(",")(0).ToString
                        End If
                        For index = 1 To pkCurrent.Split(",").Count - 1
                            If (index > pkCounter) Then
                                from_fieldTemp = from_fieldTemp & " AND " & stFirst & "." & pkFirst.Split(",")(pkCounter).ToString & " = " & stCurrent & "." & pkCurrent.Split(",")(index).ToString
                            Else
                                from_fieldTemp = from_fieldTemp & " AND " & stFirst & "." & pkFirst.Split(",")(index).ToString.Trim & " = " & stCurrent & "." & pkCurrent.Split(",")(index).ToString.Trim
                            End If
                        Next
                    End If
                    isFirstTime = False
                Next
                from_fieldTemp = dict_suffixTimeAndObjectFrom.Item(stFirst) & " " & stFirst & " " & from_fieldTemp
            End If
        End If

        Dim listOfObjectType As List(Of String) = New List(Of String)

        Dim StartFromSuffix As Boolean = False

        If (suffixObject(0) = "_" + guiObjectTableType) Then
            listOfObjectType.Add(cmbObjectType.SelectedItem.Value)
        Else

            For i = 0 To cmbObjectType.Properties.Items.Count - 1

                If (cmbObjectType.Properties.Items(i).Value IsNot Nothing) AndAlso "_" + cmbObjectType.Properties.Items(i).ToString = suffixObject(0) AndAlso cmbObjectType.Properties.Items(i).ToString <> "PLMN" Then
                    listOfObjectType.Add(cmbObjectType.Properties.Items(i).Value)
                    StartFromSuffix = True
                ElseIf (cmbObjectType.Properties.Items(i).Value IsNot Nothing) AndAlso "_" + cmbObjectType.Properties.Items(i).ToString <> suffixObject(0) AndAlso cmbObjectType.Properties.Items(i).ToString <> "PLMN" And StartFromSuffix = True Then
                    listOfObjectType.Add(cmbObjectType.Properties.Items(i).Value)
                    If cmbObjectType.SelectedItem.Value = cmbObjectType.Properties.Items(i).value Then
                        Exit For
                    End If
                ElseIf (cmbObjectType.Properties.Items(i).Value IsNot Nothing) AndAlso suffixObject(0) = "" AndAlso cmbObjectType.Properties.Items(i).ToString <> "PLMN" Then
                    listOfObjectType.Add(cmbObjectType.Properties.Items(i).Value)
                End If

            Next

        End If

        selectCMD = SQLTechnologyObjectTypes.GetObjectViewCMorPM(String.Join(",", listOfObjectType.ToArray), TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
        Dim dtObjectViewCMorPM As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)
        Dim from_fieldObjectViewTemp As String = String.Empty
        If (dtObjectViewCMorPM.IsValid) Then
            If (cmbCMPM.SelectedItem.ToString.ToUpper = "CM") Then

                Dim firstObjectView As String = ""
                Dim firstObjectID As String = ""
                Dim firstObjectParentID As String = ""
                Dim currentObjectView As String = ""
                Dim currentObjectID As String = ""
                Dim currentObjectParentID As String = ""
                Dim isFirstTime As Boolean = True
                For Each selectedObjectId As String In listOfObjectType
                    Dim dtObjectView As DataTable = dtObjectViewCMorPM.SelectedRowsAsTable(TechnologyObjectTypesFields.OBJECT_TYPE_ID & OperatorConst.Equal & selectedObjectId)
                    If (dtObjectView.IsValid) Then

                        If (isFirstTime) Then
                            firstObjectView = dtObjectView(0)(TechnologyObjectTypesFields.OBJECT_VIEW_FOR_CM).ToString
                            firstObjectID = dtObjectView(0)(TechnologyObjectTypesFields.OBJECT_TYPE_ID).ToString
                            firstObjectParentID = dtObjectView(0)(TechnologyObjectTypesFields.OBJECT_TYPE_PARENT_ID).ToString
                            from_fieldObjectViewTemp = firstObjectView
                        Else
                            currentObjectView = dtObjectView(0)(TechnologyObjectTypesFields.OBJECT_VIEW_FOR_CM).ToString
                            currentObjectID = dtObjectView(0)(TechnologyObjectTypesFields.OBJECT_TYPE_ID).ToString
                            currentObjectParentID = dtObjectView(0)(TechnologyObjectTypesFields.OBJECT_TYPE_PARENT_ID).ToString
                        End If
                        If (Not isFirstTime) Then
                            from_fieldObjectViewTemp = from_fieldObjectViewTemp & vbLf & " INNER JOIN " & currentObjectView & " ON " & firstObjectView & ".ParentID = " & currentObjectView & ".ObjectID"

                            firstObjectView = currentObjectView    'if currentobjectview has a parent, then currentobjectview will need to be joined to that parent 
                        Else
                            from_fieldObjectViewTemp = firstObjectView
                        End If
                        isFirstTime = False
                    End If
                Next
                Dim dtObjectsView As DataTable = dtObjectViewCMorPM.SelectedRowsAsTable(TechnologyObjectTypesFields.OBJECT_TYPE_ID & OperatorConst.Equal & TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value)
                If suffixObject(0) = "_" + guiObjectTableType Then
                    'If suffixObject <> vcmb_ObjectType.SelectedItem.Text Then
                    '    dtObjectsView = dtObjectViewCMorPM.SelectedRowsAsTable(TechnologyObjectTypesFields.OBJECT_TYPE_ID & OperatorConst.Equal & vcmb_ObjectType.Items(1).Value)
                    'End If
                Else
                    dtObjectsView = dtObjectViewCMorPM.SelectedRowsAsTable(TechnologyObjectTypesFields.OBJECT_TYPE_ID & OperatorConst.Equal & cmbObjectType.Properties.Items(1).Value)
                End If

                Dim ObjectFieldPM As String = "ObjectID"
                'If suffixObject <> "" Then
                '    ObjectFieldPM = "ObjectName"
                'End If
                If (dtObjectsView.IsValid) Then
                    If suffixObject(1) = "" And suffixObject(0) <> "_PLMN" Then
                        from_fieldObjectViewTemp = from_fieldObjectViewTemp & vbLf & " INNER JOIN " & from_fieldTemp & " ON " & dtObjectsView(0)(TechnologyObjectTypesFields.OBJECT_VIEW_FOR_CM).ToString & "." & ObjectFieldPM & " = " & stFirst & "." & IIf(pkFirst.Split(",").Count = 1, pkFirst, pkFirst.Trim.Split(",")(1).ToString.Trim) & " "

                        from_fieldTemp = from_fieldObjectViewTemp
                    ElseIf suffixObject(0) <> "_PLMN" Then
                        from_fieldObjectViewTemp = from_fieldObjectViewTemp & vbLf & " INNER JOIN " & from_fieldTemp & " ON " & dtObjectsView(0)(TechnologyObjectTypesFields.OBJECT_VIEW_FOR_CM).ToString & "." & ObjectFieldPM & " = " & stFirst & "." & suffixObject(1) & " "
                        from_fieldTemp = from_fieldObjectViewTemp
                    Else
                        from_fieldTemp = from_fieldObjectViewTemp
                    End If
                End If

            ElseIf (cmbCMPM.SelectedItem.ToString.ToUpper = "PM") Then

                from_fieldTemp = from_fieldTemp
            End If
        End If
        Return from_fieldTemp
    End Function

    Private Function GetFilterPeriodStats(ByRef cmb As ComboBoxEdit, ByRef dNavigator As DevExpress.XtraScheduler.DateNavigator) As String
        Try
            Dim SQL As String = ""
            If cmb.SelectedIndex > 0 Then
                Dim dr() As DataRow = dtPredefinedPeriodSB.Select("PredefinedPeriodID = " & TryCast(cmb.SelectedItem, clsComboBoxItem).Value)
                If Not dr Is Nothing Then
                    If dr.Count > 0 Then
                        SQL = dr(0)("SQLStart").ToString
                    Else
                        SQL = "@filter"
                    End If
                Else
                    SQL = "@filter"
                End If
            Else
                SQL = "@filter"
            End If

            Dim filterPeriod As DevExpress.XtraScheduler.SchedulerDateRangeCollection
            filterPeriod = dNavigator.SelectedRanges
            If xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
                filterPeriod.Clear()
                Dim endDate As DateTime = dtEditEndTime.DateTime.AddDays(1)
                filterPeriod.Add(New Controls.DateRange(dtEditStartTime.EditValue, endDate))
            End If

            Dim flag As Boolean = False
            If xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
                If rdoFilterHrsInc.Checked Or rdoFilterHrsExc.Checked Then
                    flag = True
                End If
            Else
                If rdoFilterDaysInc.Checked Or rdoFilterDaysExc.Checked Then
                    flag = True
                End If
            End If

            ScaleDateToBeExcluded.Clear()
            Dim filterQry As String = ""
            If flag Then
                For Each dt As Controls.DateRange In filterPeriod
                    If dt.StartDate.Date.CompareTo(dt.EndDate.Date.AddDays(-1)) = 0 Then
                        If xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
                            For Each tString As String In hourList
                                filterQry = filterQry & "'" & dt.StartDate.ToString("yyyy-MM-dd " & tString & ":00") & "',"
                            Next
                        Else
                            filterQry = filterQry & "'" & dt.StartDate.ToString("yyyy-MM-dd 00:00") & "',"
                        End If
                        Continue For
                    Else
                        Dim sDate As Date = dt.StartDate
                        Dim eDate As Date = dt.EndDate.AddDays(-1)
                        While sDate <= eDate
                            If xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
                                For Each tString As String In hourList
                                    filterQry = filterQry & "'" & sDate.ToString("yyyy-MM-dd " & tString & ":00") & "',"
                                Next
                            Else
                                filterQry = filterQry & "'" & sDate.ToString("yyyy-MM-dd 00:00") & "',"
                            End If
                            sDate = sDate.AddDays(1)
                        End While
                    End If
                Next
            End If
            filterQry = filterQry.TrimEnd(",") & ""
            If filterQry = "" Then
                SQL = SQL.Replace("@filter", "")
            Else
                If xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
                    If rdoFilterHrsInc.Checked Then
                        filterQry = " IN (" & filterQry & ")"
                    ElseIf rdoFilterHrsExc.Checked Then
                        filterQry = " NOT IN (" & filterQry & ")"
                    End If
                Else
                    If rdoFilterDaysInc.Checked Then
                        filterQry = " IN (" & filterQry & ")"
                    ElseIf rdoFilterDaysExc.Checked Then
                        filterQry = " NOT IN (" & filterQry & ")"
                    End If
                End If

                SQL = SQL.Replace("@filter", " AND PERIOD_START_TIME " & filterQry)
            End If
            If xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
                SQL = SQL.Replace("PERIOD_START_TIME", "DATEADD(hh, DATEDIFF(hh, 0, @alias.PERIOD_START_TIME ), 0) ")
            Else
                SQL = SQL.Replace("PERIOD_START_TIME", "DATEADD(dd, DATEDIFF(dd, 0, @alias.PERIOD_START_TIME ), 0) ")
            End If

            Return SQL

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function SQL_WherePart(ByVal sourceTable As String) As String
        Dim where_fieldTemp As String = String.Empty

        Dim sqlCMD As String = String.Empty

        Dim checkedObjectStr As String = String.Empty

        Dim guiTimeResolution As String = cmbTimeResolution.SelectedItem.ToString
        Dim guiObjectTableType As String = cmbObjectType.SelectedItem.ToString
        Dim suffixObject As List(Of String)
        suffixObject = GetObjectSuffix(sourceTable, guiTimeResolution, guiObjectTableType)

        Dim ObjectFieldPM As String = "ObjectID"
        If suffixObject(0) <> "" Or cmbObjectType.SelectedItem.ToString = "PLMN" Then
            ObjectFieldPM = "ObjectName"
        End If

        Try
            ''"Naked"
            Dim checkedObjects As String = GetChecked2String(tvObjects, cmbReportTechnology.SelectedItem.ToString, cmbObjectType.SelectedItem.ToString, ObjectFieldPM)

            If checkedObjects Is Nothing AndAlso checkedObjects = "" Then
                SetMessage("Fail Commit : Please select atlest one object in object tree.")
                Return where_fieldTemp
            End If
            checkedObjectStr = checkedObjects
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

        ''If PM based radiobutton selected
        If (cmbCMPM.SelectedItem.ToString.ToUpper = "PM") Then
            ''if st is single table
            If (sourceTable.Split(",").Count = 1) Then

                ''Get the objectfield in tbl_Technology_Measurement of st record
                sqlCMD = New SQLTechnologyMeasurements().SelectAll(False, TechnologyMeasurementsFields.SQL_SOURCE_TABLE & OperatorConst.Equal & Chr(39) & sourceTable & Chr(39))
                Dim dtObjectType As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
                If (dtObjectType.IsValid) And checkedObjectStr <> "IN ()" Then
                    ''insert into where_fields: <objectfield> + <selected_objects>
                    where_fieldTemp = sourceTable & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID) & " " & checkedObjectStr
                End If

            Else ''if st is multiple tables
                ''get first table of st
                Dim stFirst As String = sourceTable.Split(",")(0)
                If (Not String.IsNullOrEmpty(stFirst)) Then
                    ''Get the objectfield in tbl_Technology_Measurement of st record
                    sqlCMD = New SQLTechnologyMeasurements().SelectAll(False, TechnologyMeasurementsFields.SQL_SOURCE_TABLE & OperatorConst.Equal & Chr(39) & stFirst & Chr(39))
                    Dim dtObjectType As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
                    If (dtObjectType.IsValid) Then
                        where_fieldTemp = stFirst & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID) & " " & checkedObjectStr
                    End If
                Else
                    Return where_fieldTemp
                End If

            End If

            ''If CM based radiobutton selected
        ElseIf (cmbCMPM.SelectedItem.ToString.ToUpper = "CM") Then
            sqlCMD = SQLTechnologyObjectTypes.GetObjectViewCMorPM(TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value) ''String.Join(",", sourceTable.ToArray))
            Dim dtObjectViewCMorPM As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
            Dim from_fieldObjectViewTemp As String = String.Empty
            If (dtObjectViewCMorPM.IsValid) And checkedObjectStr <> "IN ()" Then
                where_fieldTemp = dtObjectViewCMorPM(0)(TechnologyObjectTypesFields.OBJECT_VIEW_FOR_CM).ToString & "." & ObjectFieldPM & " " & checkedObjectStr '''' & vcmb_ObjectSource.SelectedItem.Value

            End If
        End If

        ''if predefined period is selected:
        If (Not vcmb_PredefinedPeriod.SelectedItem.ToString.ToUpper = "NONE") Then

            'sqlCMD = New SQLPredefinedPeriod().SelectAll(False, PredefinedPeriodFields.PREDEFINED_PERIOD_ID & OperatorConst.Equal & vcmb_PredefinedPeriod.SelectedItem.Value)
            Dim dt As DataTable = dtPredefinedPeriodSB.Select(PredefinedPeriodFields.PREDEFINED_PERIOD_ID & OperatorConst.Equal & TryCast(vcmb_PredefinedPeriod.SelectedItem, clsComboBoxItem).Value).CopyToDataTable
            Dim from_fieldObjectViewTemp As String = String.Empty
            If (dt.IsValid) Then
                where_fieldTemp = where_fieldTemp & vbLf & IIf(where_fieldTemp.Length = 0, "", " AND ") & "@sourcetable.PERIOD_START_TIME BETWEEN " & dt(0)(PredefinedPeriodFields.SQL_START).ToString & " AND " & dt(0)(PredefinedPeriodFields.SQL_END).ToString
                where_fieldTemp = where_fieldTemp.Replace("@sourcetable", sourceTable.Split(",")(0))
            End If
        Else ''if predefined period is not selected, take the manual start and end dates:

            Dim startdate As Date = Nothing
            Dim enddate As Date = Nothing
            startdate = Convert.ToDateTime(dtEditStartTime.EditValue)
            enddate = Convert.ToDateTime(dtEditEndTime.EditValue)

            Dim startdate_string As String = Chr(39) & startdate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
            Dim enddate_string As String = Chr(39) & enddate.ToString("yyyy-MM-dd HH:mm") & Chr(39)

            where_fieldTemp = where_fieldTemp & vbLf & IIf(where_fieldTemp.Length = 0, "", " AND ") + sourceTable.Split(",")(0) + ".PERIOD_START_TIME  >= " & startdate_string & vbLf & " AND " + sourceTable.Split(",")(0) + ".PERIOD_START_TIME  < " & enddate_string

        End If

        Dim filterPeriodstring As String = GetFilterPeriodStats(cmbPredefinedFilter, dateNavigator)
        If filterPeriodstring <> "" Then
            where_fieldTemp = where_fieldTemp & " " & filterPeriodstring.Replace("@alias", sourceTable)
        End If

        Dim DimensionFilter As String = ""
        If dtReportFilterData.IsValid() Then
            DimensionFilter = SQL_FilterPart(dtReportFilterData)

            If DimensionFilter.Trim <> "" Then
                where_fieldTemp = where_fieldTemp & " AND " & DimensionFilter
            End If

        End If


        Return where_fieldTemp
    End Function

    Private Function SQL_GroupPart(ByVal sourceTable As String) As String
        Dim groupBy_fieldsTemp As String = ""
        Dim vSandBoxElement As DevExSandBoxField = New DevExSandBoxField()

        ''if PERIOD_START_TIME is in X axis:
        Dim isXPeriodStartTime As Boolean = False
        Dim isObjectTypeExist As Boolean = False
        Dim objectTypeExist As String = String.Empty
        For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
            vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxElement IsNot Nothing) Then
                If (vSandBoxElement.VSandBoxType = DatamartFieldType.Time) Then
                    isXPeriodStartTime = True
                ElseIf (vSandBoxElement.VSandBoxType = DatamartFieldType.ObjectFld) Then
                    isObjectTypeExist = True
                    ''Exit For

                    If (cmbCMPM.SelectedItem.ToString.ToUpper = "CM") Then
                        Dim selectCMD As String = SQLTechnologyObjectTypes.GetObjectViewCMorPM(vSandBoxElement.SourceObjectID, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
                        Dim dtObjectViewCMorPM As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)

                        If IsVSandBoxFieldExistX(cmbObjectType.SelectedItem.ToString) = True And IsVSandBoxFieldAnObjectType(vSandBoxElement.Text) Then
                            objectTypeExist = objectTypeExist & dtObjectViewCMorPM(0)("ObjectViewForCM") & ".ObjectName,"
                        Else
                            objectTypeExist = objectTypeExist & dtObjectViewCMorPM(0)("ObjectViewForCM") & ".[" & vSandBoxElement.Text & "],"
                        End If
                    Else
                        Dim sqlCMD As String = New SQLTechnologyMeasurements().SelectAll(False, TechnologyMeasurementsFields.SQL_SOURCE_TABLE & OperatorConst.Equal & Chr(39) & sourceTable & Chr(39))
                        Dim dtObjectType As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
                        objectTypeExist = dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID)
                    End If

                End If
            End If
        Next
        If (Not isObjectTypeExist) Then
            For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
                vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxElement IsNot Nothing) Then
                    If (vSandBoxElement.VSandBoxType = DatamartFieldType.ObjectFld) Then
                        isObjectTypeExist = True
                        objectTypeExist = objectTypeExist & vSandBoxElement.Text & ","
                    End If
                End If
            Next
        End If

        ''insert into group by array: period_start_time
        If (isXPeriodStartTime) Then
            Dim guiTimeResolution As String = cmbTimeResolution.SelectedItem.ToString
            Dim guiObjectTableType As String = cmbObjectType.SelectedItem.ToString
            Dim PeriodStarTimeWithTimeagg As String = GetTimeSuffix(sourceTable, guiTimeResolution, guiObjectTableType)(1)
            If sourceTable.Split(",").Count > 1 Then
                groupBy_fieldsTemp = Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable.Split(",")(0) & ".PERIOD_START_TIME")
            Else
                groupBy_fieldsTemp = Replace(PeriodStarTimeWithTimeagg, "@PERIOD_START_TIME", sourceTable & ".PERIOD_START_TIME ")
            End If

        End If
        ''insert into group by array: objecttype
        If (isObjectTypeExist) Then
            If (isXPeriodStartTime) AndAlso groupBy_fieldsTemp IsNot Nothing Then
                groupBy_fieldsTemp = groupBy_fieldsTemp & "," & objectTypeExist.TrimEnd(",")
            Else
                groupBy_fieldsTemp = objectTypeExist.TrimEnd(",")
            End If
        End If
        ''ORDER BY PART
        Return groupBy_fieldsTemp
    End Function

    Private Function SQL_HavingPart(ByRef dtFilter As DataTable) As String
        Dim having_fieldsTemp As String = String.Empty
        Dim valParam As String = String.Empty
        Dim sqlHavingPart As String = String.Empty
        Dim applyFilter As Boolean = False

        For i As Integer = 0 To dtFilter.Rows.Count - 1

            If dtFilter.Rows(i)(ReportContentFilterFields.ObjectFieldType).ToString.Trim = DatamartFieldType.Kpi Then


                For Each selectCol As KeyValuePair(Of String, List(Of String)) In sqlSelectColList
                    If (dtFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString.Trim = selectCol.Value(0).ToString.Trim) AndAlso (selectCol.Value(1).ToUpper = "Y") Then
                        sqlHavingPart = selectCol.Key.ToString
                        applyFilter = True
                        Exit For
                    End If
                Next

                If applyFilter = True Then
                    valParam = (If(IsString(dtFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()),
                                "'" & dtFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString() & "'",
                                dtFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()))

                    If (Not having_fieldsTemp.Contains(sqlHavingPart)) Then
                        having_fieldsTemp &= " " & sqlHavingPart & Chr(32) & dtFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32) & valParam & " AND "
                    End If
                End If
                applyFilter = False
            End If

        Next

        If having_fieldsTemp.Contains(" AND ") Then
            having_fieldsTemp = having_fieldsTemp.Remove(having_fieldsTemp.Count - 4, 4)
        End If
        Return having_fieldsTemp
    End Function


    Private Function SQL_FilterPart(ByRef dtFilter As DataTable) As String
        Dim having_fieldsTemp As String = String.Empty
        Dim valParam As String = String.Empty
        Dim sqlHavingPart As String = String.Empty
        Dim SkipCounterFilter As Boolean = False

        For i As Integer = 0 To dtFilter.Rows.Count - 1

            If dtFilter.Rows(i)(ReportContentFilterFields.ObjectFieldType).ToString.Trim <> DatamartFieldType.Kpi Then


                sqlHavingPart = dtFilter.Rows(i)(ReportContentFilterFields.FilterDimension).ToString()

                    valParam = (If(IsString(dtFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()),
                                "'" & dtFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString() & "'",
                                dtFilter.Rows(i)(ReportContentFilterFields.FilterValue).ToString()))

                    If (Not having_fieldsTemp.Contains(sqlHavingPart)) Then
                        having_fieldsTemp &= " " & sqlHavingPart & Chr(32) & dtFilter.Rows(i)(ReportContentFilterFields.FilterOperator).ToString() & Chr(32) & Chr(39) & Replace(valParam.Trim, Chr(39), "") & Chr(39) & " AND "
                    End If

            End If

        Next

        If having_fieldsTemp.Contains(" AND ") Then
            having_fieldsTemp = having_fieldsTemp.Remove(having_fieldsTemp.Count - 4, 4)
        End If
        Return having_fieldsTemp
    End Function


    Private Function SQL_OrderByPart(ByVal sourceTable As String) As String
        Dim orderBy_fieldsTemp As String = ""
        Dim vSandBoxElement As DevExSandBoxField = New DevExSandBoxField()

        ''if PERIOD_START_TIME is in X axis:
        Dim isXPeriodStartTime As Boolean = False
        Dim isObjectTypeExist As Boolean = False
        Dim objectTypeExist As String = String.Empty
        For Each flowLayoutPanelXYControls As Object In flp_ValueX.Controls
            vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxElement IsNot Nothing) Then
                If (vSandBoxElement.VSandBoxType = DatamartFieldType.Time) Then
                    ' isXPeriodStartTime = True
                    If sourceTable.Split(",").Count > 1 Then
                        ' orderBy_fieldsTemp = orderBy_fieldsTemp & sourceTable.Split(",")(0) + ".PERIOD_START_TIME ,"
                        orderBy_fieldsTemp = "1 ,"
                    Else
                        ' orderBy_fieldsTemp = orderBy_fieldsTemp & sourceTable & ".PERIOD_START_TIME ,"
                        orderBy_fieldsTemp = "1 ,"
                    End If
                ElseIf (vSandBoxElement.VSandBoxType = DatamartFieldType.ObjectFld) Then
                    isObjectTypeExist = True
                    If (Not vSandBoxElement.SortValue.ToUpper = "NONE") Then
                        If (cmbCMPM.SelectedItem.ToString.ToUpper = "CM") Then
                            Dim selectCMD As String = SQLTechnologyObjectTypes.GetObjectViewCMorPM(vSandBoxElement.SourceObjectID, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
                            Dim dtObjectViewCMorPM As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)

                            orderBy_fieldsTemp = orderBy_fieldsTemp & dtObjectViewCMorPM(0)("ObjectViewForCM") & ".ObjectName  " & vSandBoxElement.SortValue & ","
                        Else
                            Dim sqlCMD As String = New SQLTechnologyMeasurements().SelectAll(False, TechnologyMeasurementsFields.SQL_SOURCE_TABLE & OperatorConst.Equal & Chr(39) & sourceTable & Chr(39))
                            Dim dtObjectType As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
                            orderBy_fieldsTemp = orderBy_fieldsTemp & sourceTable & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID) & " " & vSandBoxElement.SortValue & ","
                        End If

                        'If sourceTable.Split(",").Count > 1 Then
                        '    orderBy_fieldsTemp = orderBy_fieldsTemp & sourceTable.Split(",")(0) & vSandBoxElement.Text & " " & vSandBoxElement.SortValue & ","
                        'Else
                        '    orderBy_fieldsTemp = orderBy_fieldsTemp & sourceTable & "." & vSandBoxElement.Text & " " & vSandBoxElement.SortValue & ","
                        'End If
                    End If
                ElseIf (vSandBoxElement.VSandBoxType = DatamartFieldType.Counter) Or (vSandBoxElement.VSandBoxType = DatamartFieldType.Kpi) Then
                    If (vSandBoxElement.SQL_SourceTable.Contains(sourceTable)) Then
                        orderBy_fieldsTemp = orderBy_fieldsTemp & Chr(34) & vSandBoxElement.Text & Chr(34) & " " & vSandBoxElement.SortValue & ","
                    End If


                End If
            End If
        Next
        '  If (Not isObjectTypeExist) Then
        For Each flowLayoutPanelXYControls As Object In flp_ValueY.Controls
            vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
            If (vSandBoxElement IsNot Nothing) Then
                If (vSandBoxElement.VSandBoxType = DatamartFieldType.ObjectFld) Then
                    isObjectTypeExist = True
                    If (Not vSandBoxElement.SortValue.ToUpper = "NONE") Then
                        If (cmbCMPM.SelectedItem.ToString.ToUpper = "CM") Then
                            Dim selectCMD As String = SQLTechnologyObjectTypes.GetObjectViewCMorPM(vSandBoxElement.SourceObjectID, TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value)
                            Dim dtObjectViewCMorPM As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)

                            orderBy_fieldsTemp = orderBy_fieldsTemp & dtObjectViewCMorPM(0)("ObjectViewForCM") & ".ObjectName  " & vSandBoxElement.SortValue & ","
                        Else
                            Dim sqlCMD As String = New SQLTechnologyMeasurements().SelectAll(False, TechnologyMeasurementsFields.SQL_SOURCE_TABLE & OperatorConst.Equal & Chr(39) & sourceTable & Chr(39))
                            Dim dtObjectType As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCMD)
                            orderBy_fieldsTemp = orderBy_fieldsTemp & sourceTable & "." & dtObjectType(0)(TechnologyMeasurementsFields.OBJECT_FIELD_ID) & " " & vSandBoxElement.SortValue & ","
                        End If


                    End If
                ElseIf (vSandBoxElement.VSandBoxType = DatamartFieldType.Counter Or vSandBoxElement.VSandBoxType = DatamartFieldType.Kpi) Then
                    If (Not vSandBoxElement.SortValue.ToUpper = "NONE") Then
                        ' If (vSandBoxElement.SQL_SourceTable.Contains(sourceTable)) Then
                        If (sourceTable.Contains(String.Join(",", vSandBoxElement.SQL_SourceTable))) Then
                            orderBy_fieldsTemp = orderBy_fieldsTemp & Chr(34) & vSandBoxElement.Text & Chr(34) & " " & vSandBoxElement.SortValue & ","
                        End If

                    End If

                End If
            End If

        Next
        'End If

        ''insert into group by array: period_start_time
        'If (isXPeriodStartTime) Then
        '    If sourceTable.Split(",").Count > 1 Then
        '        orderBy_fieldsTemp = sourceTable.Split(",")(0) + ".PERIOD_START_TIME "
        '    Else
        '        orderBy_fieldsTemp = sourceTable & ".PERIOD_START_TIME "
        '    End If
        'End If

        Return orderBy_fieldsTemp.TrimEnd(",")
    End Function

    Private Function GetTimeSuffix(ByVal sourceTable As String, ByVal guiTimeResolution As String, ByVal guiObjectTableType As String) As String()
        Dim suffixTime As String() = {"", ""}
        Dim selectCMD As String = String.Empty
        selectCMD = SQLAggregationsTime.GetTimeAggregationSuffix(sourceTable.Split(",")(0), guiTimeResolution)

        Dim timeaAndObjectAgregationDs As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, selectCMD)
        If (timeaAndObjectAgregationDs.Tables(0).IsValid) Then
            suffixTime(0) = (timeaAndObjectAgregationDs.Tables(0).Rows(0)(AggregationsTimeFields.TIME_AGGREGATION_TABLE_SUFFIX).ToString())
            suffixTime(1) = (timeaAndObjectAgregationDs.Tables(0).Rows(0)("PeriodStartTime").ToString())
        End If

        Return suffixTime
    End Function

    Private Function GetObjectSuffix(ByVal sourceTable As String, ByVal guiTimeResolution As String, ByVal guiObjectTableType As String) As List(Of String)
        Dim suffixObject As New List(Of String)
        Dim selectCMD As String = String.Empty

        selectCMD = SQLAggregationsObjects.GetObjectAggregationSuffix(sourceTable, guiObjectTableType)

        Dim timeaAndObjectAgregationDs As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, selectCMD)
        If (timeaAndObjectAgregationDs.Tables(0).IsValid) Then
            suffixObject.Add((timeaAndObjectAgregationDs.Tables(0).Rows(0)(AggregationsObjectsFields.OBJECT_AGGREGATION_TABLE_SUFFIX).ToString()))
            suffixObject.Add(nZ(timeaAndObjectAgregationDs.Tables(0).Rows(0)("ObjectAggregationTablePMObjectField"), ""))
        Else
            suffixObject.Add("")
            suffixObject.Add("")
        End If


        Return suffixObject
    End Function

    Private Function GetTimeAndObjectSuffix(ByVal sourceTable As String, ByVal guiTimeResolution As String, ByVal guiObjectTableType As String) As List(Of String)
        Dim suffixTimeAndObject As List(Of String) = New List(Of String)
        Dim selectCMD As String = String.Empty
        selectCMD = SQLAggregationsTime.GetTimeAggregationSuffix(sourceTable, guiTimeResolution)
        selectCMD = selectCMD & SQLAggregationsObjects.GetObjectAggregationSuffix(sourceTable, guiObjectTableType)
        Dim timeaAndObjectAgregationDs As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, selectCMD)
        If (timeaAndObjectAgregationDs.Tables(0).IsValid) Then
            suffixTimeAndObject.Add(timeaAndObjectAgregationDs.Tables(0).Rows(0)(AggregationsTimeFields.TIME_AGGREGATION_TABLE_SUFFIX).ToString())
        End If

        If (timeaAndObjectAgregationDs.Tables(1).IsValid) Then
            suffixTimeAndObject.Add(timeaAndObjectAgregationDs.Tables(1).Rows(0)(AggregationsTimeFields.TIME_AGGREGATION_TABLE_SUFFIX).ToString())
        End If

        Return suffixTimeAndObject

    End Function

#End Region

    Private Sub Combobox_MouseWheel(sender As Object, e As MouseEventArgs) Handles cmbReportTechnology.Properties.MouseWheel, cmbObjectSource.Properties.MouseWheel,
                                                                                   cmbObjectType.Properties.MouseWheel, cmbCMPM.Properties.MouseWheel
        ' disallowing combobox items scrolling through mouse wheel unless clicked/expanded
        If e.Clicks <= 0 Then
            Dim cmb As ComboBoxEdit = DirectCast(sender, ComboBoxEdit)
            cmb.Properties.AllowMouseWheel = False
        End If
    End Sub

    Private Sub txtDimensionSearch_TextChanged(sender As Object, e As EventArgs) Handles txtDimensionSearch.TextChanged
        Try
            lbDimensions.DataSource.DefaultView.RowFilter = "[COLUMN_NAME] like '%" & txtDimensionSearch.Text.Trim() & "%'"
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub lbDimensions_DragDrop(sender As Object, e As DragEventArgs) Handles lbDimensions.DragDrop
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub lbDimensions_DragOver(sender As Object, e As DragEventArgs) Handles lbDimensions.DragOver
        Try
            e.Effect = DragDropEffects.Move
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub lbDimensions_MouseMove(sender As Object, e As MouseEventArgs) Handles lbDimensions.MouseMove
        If (e.Button AndAlso MouseButtons.Left = MouseButtons.Left) Then
            Dim drop_effect As DragDropEffects = Nothing
            dragDimensions = True
            Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
            If (listControl IsNot Nothing) Then
                Dim index As Integer = listControl.IndexFromPoint(e.Location)
                Dim objectDragDropText As String = listControl.GetItemText(index) & "#" & TryCast(cmbObjectType.Properties.Items(1), clsComboBoxItem).Value & "#" & DatamartFieldType.ObjectFld
                drop_effect = listControl.DoDragDrop(objectDragDropText, DragDropEffects.All)
            End If
        End If
    End Sub

#End Region

#Region "Dashboard"

    Private Function GetTreeViewReportObjects() As String
        Dim checkedNodes As String = String.Empty
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (cmbReportTechnology.SelectedIndex > 0 AndAlso cmbObjectType.SelectedIndex > 0) Then
                checkedNodes = GetChecked2String(tvObjects, cmbReportTechnology.SelectedItem.ToString, cmbObjectType.SelectedItem.ToString, "ObjectName")
            End If
            If checkedNodes <> "IN ()" Then
                checkedNodes = checkedNodes.Replace("IN ('", "").Replace("', )", "").Replace("'", "")
            Else
                checkedNodes = checkedNodes.Replace("IN ()", "")
            End If
            checkedNodes = checkedNodes.Replace(")", "")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
        Return checkedNodes
    End Function

    Private Sub GetReportChartGridDashboardId(ByVal dashboardId As String, ByVal dashboardName As String)
        Try
            Dim vTabPageNew As XtraTabPage = CreateReportChartGrid_Tab(dashboardId, dashboardName)
            If (vTabPageNew IsNot Nothing) Then
                Dim flpDashboard As FlowLayoutPanel = CreateReportChartGrid_Flowlayout(dashboardName & "_" & dashboardId)
                If (flpDashboard IsNot Nothing) Then
                    flpDashboard.Tag = dashboardId
                    flpDashboard.Dock = DockStyle.Fill
                    vTabPageNew.Controls.Add(flpDashboard)
                    'xtcDashboards.TabPages.Clear()
                    xtcDashboards.TabPages.Add(vTabPageNew)
                    xtcDashboards.SelectedTabPage = vTabPageNew
                    ''flpDashboard.AutoSize
                    Dim dt_DashboardReportCharts As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLDashboards.GetDashBoardReportChart(dashboardId))
                    If (dt_DashboardReportCharts.IsValid) Then
                        Dim dtDistictReportId As DataTable = dt_DashboardReportCharts.Select("", DashboardReportsFields.REPORT_ORDINAL & " ASC").CopyToDataTable().DistinctCol(ReportChartFields.ReportID)
                        If (dtDistictReportId.IsValid) Then
                            Dim dtDashboardReports As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLDashboardReports.GetDashBoardReport(dashboardId))

                            For Each dr As DataRow In dtDistictReportId.Rows
                                Dim dtChConfig As DataTable = dt_DashboardReportCharts.SelectedRowsAsTable(ReportChartFields.ReportID, OperatorConst.Equal, dr(ReportChartFields.ReportID))
                                Dim dtDashboardReport As DataTable = dtDashboardReports.SelectedRowsAsTable(ReportChartFields.ReportID, OperatorConst.Equal, dr(ReportChartFields.ReportID))
                                AppendReportChartGrid(dtDashboardReport, flpDashboard, dtChConfig)
                            Next
                        End If
                    Else
                        SetMessage("Chart not found corresponding to selected Dashboard.")
                    End If
                End If
            End If
        Catch ex As Exception
            SetMessage("Error : Not able to get Dashboard Report Charts ")
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub AppendReportChartGrid(ByVal dtDashboardReport As DataTable, ByRef flpDashboardReportChartGrid As FlowLayoutPanel, ByVal dtChartConfig As DataTable)

        Dim tabText = dtChartConfig.Rows(0)(ReportChartFields.ChartTitle).ToString
        Dim reportId = dtChartConfig.Rows(0)(ReportChartFields.ReportID).ToString
        Try
            Dim reportChartGrid As ReportChartGrid = New ReportChartGrid
            reportChartGrid.Name = tabText.Replace(" ", "_") & reportId
            reportChartGrid.SetGridContextMenu = cms_ReportChartGrid
            reportChartGrid.SetChartContextMenu = cms_ReportChartGrid
            Dim widthValue As Integer = (flpDashboardReportChartGrid.Width - 25) / (IIf(spinEdit_DashboradReportPerRow.Value = 0, 1, spinEdit_DashboradReportPerRow.Value)) - 5
            Dim heightValue As Integer = trackBar_DashboardChartSize.Value

            reportChartGrid.Size = New Size(widthValue, heightValue)
            reportChartGrid.reportSQL = IIf(IsDBNull(dtChartConfig.Rows(0)(ReportChartFields.ReportSQL)), "", dtChartConfig.Rows(0)(ReportChartFields.ReportSQL))
            reportChartGrid.reportConnString = IIf(IsDBNull(dtChartConfig.Rows(0)(ReportChartFields.ReportConnString)), "", dtChartConfig.Rows(0)(ReportChartFields.ReportConnString))
            reportChartGrid.ChartObjectsData = IIf(IsDBNull(dtChartConfig.Rows(0)(ReportChartFields.ObjectNamesInReport)), "", dtChartConfig.Rows(0)(ReportChartFields.ObjectNamesInReport))
            reportChartGrid.dtChartConfig = dtChartConfig
            Dim dsReportAxisData As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLReportChart.GetReportAxisData(reportId) & SQLReportContentFilter.GetReportContentFilter(reportId))

            reportChartGrid.ReportAxisData = dsReportAxisData.Tables(0)
            reportChartGrid.ReportFilter = dsReportAxisData.Tables(1)
            reportChartGrid.GridorChart = IIf(IsDBNull(dtChartConfig.Rows(0)("GridOrChart")), "", dtChartConfig.Rows(0)("GridOrChart"))

            'Dim dsReportContentObjects As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLReportContentObjects.InsertReportContent_GetObjects(reportId))
            'Dim objs As String = ""
            'For Each dr As DataRow In dsReportContentObjects.Tables(0).Rows
            '    objs = objs + dr("ObjectID").ToString
            'Next

            'reportChartGrid.ChartObjectsData = GetTreeViewReportObjects()
            ' reportChartGrid.ChartObjectsData = ""
            If (dtDashboardReport.IsValid) Then
                reportChartGrid.ReportId = reportId
                reportChartGrid.ReportOrdinale = dtDashboardReport(0)(DashboardReportsFields.REPORT_ORDINAL).ToString
                reportChartGrid.DashboardID = dtDashboardReport(0)(DashboardReportsFields.DASHBOARD_ID).ToString
            Else
                reportChartGrid.ReportId = reportId
                reportChartGrid.ReportOrdinale = 0
                reportChartGrid.DashboardID = 0
            End If

            'AddHandler reportChartGrid.ChartDragDropEvent, AddressOf reportChartGridSendBox_DragDropEvent
            'AddHandler reportChartGrid.ChartClickEvent, AddressOf reportChartGridSendBox_ClickEvent
            flpDashboardReportChartGrid.Controls.Add(reportChartGrid)

            Dim dedateFormat As String = ""
            'If (cmbTimeResolution.SelectedItem.ToString.ToUpper = "RAW" Or cmbTimeResolution.SelectedItem.ToString.ToUpper = "HOUR") And ((rdoFilterDaysExc.Checked = True Or rdoFilterDaysInc.Checked = True) Or (rdoFilterHrsExc.Checked = True Or rdoFilterHrsInc.Checked = True) Or (cmbPredefinedFilter.SelectedIndex > 0)) Then
            '    dedateFormat = "dd/MM/yy HH:mm"
            'ElseIf (cmbTimeResolution.SelectedItem.ToString.ToUpper = "DAY") Or (rdoFilterDaysExc.Checked = True Or rdoFilterDaysInc.Checked = True) Or (rdoFilterHrsExc.Checked = True Or rdoFilterHrsInc.Checked = True) Or (cmbPredefinedFilter.SelectedIndex > 0) Then
            '    dedateFormat = "dd/MM/yy"
            'ElseIf (cmbTimeResolution.SelectedItem.ToString.ToUpper = "MONTH") And (rdoFilterDaysExc.Checked = True Or rdoFilterDaysInc.Checked = True) Or (rdoFilterHrsExc.Checked = True Or rdoFilterHrsInc.Checked = True) Or (cmbPredefinedFilter.SelectedIndex > 0) Then
            '    dedateFormat = "MMMM"
            'End If
            Dim timeResolution As String = dtChartConfig(0)("TimeResolution").ToString
            If timeResolution = "RAW" Or timeResolution = "HOUR" Then
                dedateFormat = "dd/MM/yy HH:mm"
            ElseIf timeResolution = "DAY" Then
                dedateFormat = "dd/MM/yy"
            ElseIf timeResolution = "MONTH" Then
                dedateFormat = "MMMM"
            End If

            reportChartGrid.RefreshSetting = False
            reportChartGrid.GenerateSQL(dedateFormat, CInt(txtQueryTimeOut.Text))
            reportChartGrid.ShowGridChartPanel()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            mm.ReleaseMemory()
        End Try
    End Sub

    Private Function GetSplitControl(ByRef tempControl As Control) As SplitContainer
        Try
            If (tempControl IsNot Nothing AndAlso tempControl.Parent IsNot Nothing) Then
                If tempControl.Parent.GetType() Is GetType(SplitContainer) Then
                    Return tempControl.Parent
                Else
                    Return GetSplitControl(tempControl.Parent)
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Private Function CreateReportChartGrid_Tab(ByVal tabTag As String, ByVal tabText As String) As DevExpress.XtraTab.XtraTabPage
        Try
            Dim vTabPageNew As New DevExpress.XtraTab.XtraTabPage()
            vTabPageNew.Name = "vTabPage" & tabText
            vTabPageNew.Text = tabText
            vTabPageNew.Tag = tabTag
            vTabPageNew.Dock = DockStyle.Fill
            vTabPageNew.Tooltip = tabText

            Return vTabPageNew
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Private Function CreateReportChartGrid_Flowlayout(ByVal flpName As String) As FlowLayoutPanel

        Dim flp_Dashboard As FlowLayoutPanel = New FlowLayoutPanel
        flp_Dashboard.AllowDrop = True
        flp_Dashboard.AutoScroll = True
        flp_Dashboard.AutoSize = True
        flp_Dashboard.BackColor = System.Drawing.Color.WhiteSmoke
        flp_Dashboard.Dock = DockStyle.Fill
        flp_Dashboard.ForeColor = System.Drawing.Color.DimGray
        flp_Dashboard.Location = New System.Drawing.Point(60, 3)
        flp_Dashboard.Name = "flp_" & flpName
        flp_Dashboard.Size = New System.Drawing.Size(310, 29)
        flp_Dashboard.Dock = DockStyle.Fill
        flp_Dashboard.Tag = flpName.ToUpper
        AddHandler flp_Dashboard.DragEnter, AddressOf flp_Dashboard_DragEnter
        AddHandler flp_Dashboard.DragDrop, AddressOf flp_Dashboard_DragDrop
        Return flp_Dashboard

    End Function

    Private Sub flp_Dashboard_DragDrop(sender As Object, e As DragEventArgs)

        Dim data As ReportChartGrid = DirectCast(e.Data.GetData(GetType(ReportChartGrid)), ReportChartGrid)
        If (data IsNot Nothing) Then
            Dim _destination As FlowLayoutPanel = TryCast(sender, FlowLayoutPanel)
            If (_destination IsNot Nothing) Then
                Dim p As System.Drawing.Point = _destination.PointToClient(New System.Drawing.Point(e.X, e.Y))
                Dim item = _destination.GetChildAtPoint(p)
                Dim index As Integer = _destination.Controls.GetChildIndex(item, False)
                _destination.Controls.SetChildIndex(data, index)
                _destination.Invalidate()
                Dim controlIndex As Integer = 0
                Dim strReportOrderCommand As String = String.Empty
                For Each con As Control In _destination.Controls
                    Dim cmdStr As String = ""
                    Dim reportChartGrid As ReportChartGrid = TryCast(con, ReportChartGrid)
                    If (reportChartGrid IsNot Nothing) Then
                        reportChartGrid.ReportOrdinale = controlIndex
                        cmdStr = "UPDATE [tbl_Dashboard_Reports] SET " & DashboardReportsFields.REPORT_ORDINAL & "=" & controlIndex & " WHERE DashboardID=" & reportChartGrid.DashboardID & " AND ReportID=" & reportChartGrid.ReportId & ";"
                        strReportOrderCommand = strReportOrderCommand & cmdStr
                        controlIndex = controlIndex + 1
                    End If
                Next

                If (Not String.IsNullOrEmpty(strReportOrderCommand)) Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, strReportOrderCommand)
                    Dim selectedDashboardNode As TreeListNode = tvDashboardGroupReport.FocusedNode ''.Tag.ToString
                    RefreshDashboardGroupReportTLV()
                    tvDashboardGroupReport.FocusedNode = selectedDashboardNode
                End If
            End If
        End If

    End Sub

    Private Sub flp_Dashboard_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Move
    End Sub

    'Private Sub BindDashboardGroupCombo(Optional ByVal isFirstItemSelected As Boolean = False)
    '    Try
    '        Dim sqlCommand As String = SQLDashboardGroups.GetDashboardGroup("( " & DashBoardGroups_View.LICENSEUSER & OperatorConst.Equal & Chr(39) & System.Environment.UserName & Chr(39) & AggregateConst.AND_Only & " " & DashBoardGroups_View.DASHBOARDGROUP_PRIVATE & OperatorConst.Equal & "0 ) " & AggregateConst.OR_Only & " ( " & DashBoardGroups_View.DASHBOARDGROUP_CREATOR & OperatorConst.Equal & Chr(39) & System.Environment.UserName & "' " & AggregateConst.AND_Only & DashBoardGroups_View.DASHBOARDGROUP_PRIVATE & OperatorConst.Equal & "1 )", DashBoardGroups_View.DASHBOARDGROUP_NAME)
    '        Dim dt_DashboardGroup As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCommand)
    '        If (dt_DashboardGroup.IsValid) Then
    '            BindDevExComboBoxWithTagMember(cmbDashboardGroup, dt_DashboardGroup, DashBoardGroups_View.DASHBOARDGROUP_ID, DashBoardGroups_View.DASHBOARDGROUP_NAME, "Select Group", DashBoardGroups_View.DASHBOARDGROUP_PRIVATE, isFirstItemSelected)
    '        Else
    '            ClearComboBox(cmbDashboardGroup, "Select Group")
    '        End If
    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '    End Try
    'End Sub

    Private Sub cmbDashboardGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDashboardGroup.SelectedIndexChanged
        Try
            If (isDashboradGroupSelectedIndexChanged) Then
                If (cmbDashboardGroup.SelectedIndex > 0) Then
                    btnAddDashborad.Enabled = True
                    xtcDashboards.TabPages.Clear()
                    RefreshDashboardGroupReportTLV()
                Else
                    tvDashboardGroupReport.Nodes.Clear()
                    btnAddDashborad.Enabled = False
                    xtcDashboards.TabPages.Clear()
                End If
            End If
            reportChartGrid_SendBox.ClearData()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Sub RefreshDashboardGroupReportTLV()
        tvDashboardGroupReport.Cursor = Cursors.WaitCursor
        tvDashboardGroupReport.BeginUnboundLoad()
        Application.DoEvents()
        If cmbDashboardGroup.SelectedItem Is Nothing Then
            Exit Sub
        End If

        RemoveHandler tvDashboardGroupReport.FocusedNodeChanged, AddressOf tvDashboardGroupReport_FocusedNodeChanged
        RemoveHandler tvDashboardGroupReport.NodeChanged, AddressOf tvDashboardGroupReport_NodeChanged

        Dim dtQODBC As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLDashboardGroups.GetDashBoardGroupReportTree(TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Value))

        Dim colList() As String = {DashBoardTreeFields.DASHBOARDGROUPID, DashBoardTreeFields.DASHBOARDGROUPNAME,
                                           DashBoardTreeFields.DASHBOARDID, DashBoardTreeFields.DASHBOARDNAME, DashBoardTreeFields.DASHBOARDORDINAL,
                                           DashBoardTreeFields.REPORTID, DashBoardTreeFields.REPORTNAME, DashBoardTreeFields.REPORTORDINAL}

        tvDashboardGroupReport.Columns.Clear()
        For i As Integer = 0 To colList.Length - 1
            Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
            col1.Caption = colList(i)
            col1.VisibleIndex = i
            If colList(i) = DashBoardTreeFields.DASHBOARDGROUPNAME Then
                tvDashboardGroupReport.AutoFillColumn = col1
                col1.Visible = True
            Else
                col1.Visible = False
            End If
            tvDashboardGroupReport.Columns.Add(col1)
        Next
        tvDashboardGroupReport.Nodes.Clear()

        Try
            Dim dbNode As TreeListNode = Nothing

            If (dtQODBC.IsValid) Then
                Dim tlNode As TreeListNode = tvDashboardGroupReport.Nodes.Add(New Object() {dtQODBC.Rows(0)(DashBoardTreeFields.DASHBOARDGROUPID), dtQODBC.Rows(0)(DashBoardTreeFields.DASHBOARDGROUPNAME), 0, "", -1, 0, "", -1})

                Dim groupName As String = dtQODBC.Rows(0)(DashBoardTreeFields.DASHBOARDGROUPNAME).ToString
                Dim groupID As String = dtQODBC.Rows(0)(DashBoardTreeFields.DASHBOARDGROUPID).ToString

                ToolTipController1.SetToolTip(tvDashboardGroupReport, "Group" & "_" & IIf(dtQODBC.Rows(0)(DashBoardTreeFields.DASHBOARDGROUPPRIVATE).ToString.ToUpper = "TRUE", 1, 0))
                tlNode.Tag = groupID

                Dim distinctCol() As String = {DashBoardTreeFields.DASHBOARDNAME, DashBoardTreeFields.DASHBOARDORDINAL, DashBoardTreeFields.DASHBOARDID}
                Dim dtDistinctGroupName As DataTable = dtQODBC.DistinctCol(distinctCol)
                If (dtDistinctGroupName.IsValid) Then
                    Dim nodeIndex As Integer = 0
                    Dim drGroupName As DataRow() = dtDistinctGroupName.Select("", DashBoardTreeFields.DASHBOARDORDINAL & " ASC ")
                    For Each rowGroupName As DataRow In drGroupName
                        If (Not IsDBNull(rowGroupName(DashBoardTreeFields.DASHBOARDNAME))) Then
                            dbNode = tvDashboardGroupReport.AppendNode(New Object() {rowGroupName(DashBoardTreeFields.DASHBOARDID), rowGroupName(DashBoardTreeFields.DASHBOARDNAME), rowGroupName(DashBoardTreeFields.DASHBOARDORDINAL)}, tlNode)
                            dbNode.Tag = rowGroupName(DashBoardTreeFields.DASHBOARDID).ToString

                            Dim reportFilter As String = DashBoardTreeFields.DASHBOARDID & OperatorConst.Equal & rowGroupName(DashBoardTreeFields.DASHBOARDID)
                            Dim dtDistinctReport As DataTable = dtQODBC.SelectedRowsAsTable(reportFilter)

                            If dtDistinctReport.IsValid Then
                                Dim dr As DataRow() = dtDistinctReport.Select("", DashBoardTreeFields.REPORTORDINAL & " ASC ")
                                For Each drow As DataRow In dr
                                    Dim rptNode As TreeListNode = tvDashboardGroupReport.AppendNode(New Object() {drow.Item(DashBoardTreeFields.REPORTID).ToString, drow.Item(DashBoardTreeFields.REPORTNAME).ToString, drow.Item(DashBoardTreeFields.REPORTORDINAL).ToString, rowGroupName(DashBoardTreeFields.DASHBOARDID), rowGroupName(DashBoardTreeFields.DASHBOARDNAME)}, dbNode)
                                    rptNode.Tag = drow.Item(DashBoardTreeFields.REPORTID).ToString
                                Next
                            End If
                            nodeIndex = nodeIndex + 1
                        End If
                    Next
                End If
            Else
                Dim tlNode As TreeListNode = tvDashboardGroupReport.Nodes.Add(New Object() {TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Value, cmbDashboardGroup.SelectedItem.ToString.Trim, 0, "", -1, 0, "", -1})
                tlNode.Tag = TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Value
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If

            tvDashboardGroupReport.EndUnboundLoad()
            If tvDashboardGroupReport.Nodes.Count > 0 Then
                tvDashboardGroupReport.SelectNode(tvDashboardGroupReport.Nodes(0))
                tvDashboardGroupReport.SetFocusedNode(tvDashboardGroupReport.Nodes(0))
                tvDashboardGroupReport.ExpandAll()
            End If
            tvDashboardGroupReport.Cursor = Cursors.Default
            Application.DoEvents()

            AddHandler tvDashboardGroupReport.NodeChanged, AddressOf tvDashboardGroupReport_NodeChanged
            AddHandler tvDashboardGroupReport.FocusedNodeChanged, AddressOf tvDashboardGroupReport_FocusedNodeChanged

            GC.Collect()
            GC.WaitForPendingFinalizers()

            tvDashboardGroupReport_FocusedNodeChanged(Nothing, Nothing)
        End Try
    End Sub

    Private Sub tvDashboardGroupReport_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        RemoveHandler tvDashboardGroupReport.NodeChanged, AddressOf tvDashboardGroupReport_NodeChanged
        If e.ChangeType = NodeChangeTypeEnum.CheckedState Then
            If e.Node.CheckState = CheckState.Checked Then
                e.Node.CheckAll()
            Else
                e.Node.UncheckAll()
            End If
            tvDashboardGroupReport.CheckParentNode(e.Node)
        End If
        AddHandler tvDashboardGroupReport.NodeChanged, AddressOf tvDashboardGroupReport_NodeChanged
    End Sub

    Private Sub btnDashboradGroup_Click(sender As Object, e As EventArgs) Handles btnDashboradGroup.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim frmGroupInsert As New dlgSBGroupInsert()
            frmGroupInsert.SetConnectionString(connStrSandBoxServer)
            frmGroupInsert.GroupTypeInserting = GroupType.DashboardGroup

            frmGroupInsert.ShowDialog()
            Dim newGroupName As String = frmGroupInsert.NewGroup

            Dim RetrunData As Boolean = frmGroupInsert.IsGroupPrivate
            If (newGroupName IsNot Nothing) Then
                BindDashboardGroupCombo()
                'Dim index = vCmb_DashboradGroup.Items.ToList().FindIndex(Function(c) c.ToString() = newGroupName)
                cmbDashboardGroup.SelectedItem = GetComboItemFromText(newGroupName, cmbDashboardGroup)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub tvDashboardGroupReport_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs)
        Try
            If (Not IsDashboardGroupReportMouseDownRight) Then

                tvDashboardGroupReport.OptionsBehavior.Editable = False
                tvDashboardGroupReport.OptionsBehavior.ReadOnly = True

                If Not tvDashboardGroupReport.FocusedNode Is Nothing Then
                    WaitScreen.ShowWaitScreen("Dashboard reports getting data")

                    Dim treeNode As TreeListNode = tvDashboardGroupReport.FocusedNode
                    If (treeNode.Level = 0) Then
                        If xtcDashboards.TabPages.Count = 0 Then
                            dtDashboardGroup = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLDashboards.GetDashboardsForDashboardGroup(TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Value))
                            If dtDashboardGroup.IsValid() AndAlso Not dtDashboardGroup Is Nothing Then
                                xtcDashboards.TabPages.Clear()
                                For Each drDashboard As DataRow In dtDashboardGroup.Rows
                                    GetReportChartGridDashboardId(drDashboard("DashboardID").ToString, drDashboard("DashboardName").ToString)
                                Next
                            End If
                        End If
                    ElseIf (treeNode.Level = 1) Then
                        If xtcDashboards.TabPages.Count > 0 Then
                            Dim dashboardID = treeNode.Tag
                            Dim dashboardName = treeNode.GetDisplayText("DashboardGroupName").ToString
                            Dim vTabPageNew As XtraTabPage = xtcDashboards.TabPages.First(Function(x) x.Text = dashboardName AndAlso x.Tag = dashboardID)
                            xtcDashboards.SelectedTabPage = vTabPageNew
                        End If
                    ElseIf (treeNode.Level = 2) Then
                        If xtcDashboards.TabPages.Count > 0 Then
                            Dim dashboardID = treeNode.ParentNode.Tag
                            Dim dashboardName = treeNode.ParentNode.GetDisplayText("DashboardGroupName").ToString
                            Dim vTabPageNew As XtraTabPage = xtcDashboards.TabPages.First(Function(x) x.Text = dashboardName AndAlso x.Tag = dashboardID)
                            xtcDashboards.SelectedTabPage = vTabPageNew

                            Dim tlist As TreeListNode = treeNode.ParentNode
                            Dim childList() As TreeListNode = tlist.Nodes.ToArray()
                            Dim index As Integer = Array.FindIndex(childList, Function(x) x.Item("DashboardGroupName") = treeNode.Item("DashboardGroupName"))
                            vTabPageNew.Controls(0).Controls(index).Focus()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
        End Try
    End Sub

    Private Sub tvDashboardGroupReport_MouseDown(sender As Object, e As MouseEventArgs) Handles tvDashboardGroupReport.MouseDown
        If e.Button = MouseButtons.Right Then
            IsDashboardGroupReportMouseDownRight = True

            Dim treeList As TreeList = TryCast(sender, TreeList)
            Dim info As TreeListHitInfo = treeList.CalcHitInfo(e.Location)
            If info.Node IsNot Nothing Then
                If info.Node.Level = 0 Then
                    Me.DashboardGroupReportTreeSelectionType = DashboardSelectionType.DashboardGroup
                ElseIf info.Node.Level = 1 Then
                    Me.DashboardGroupReportTreeSelectionType = DashboardSelectionType.Dashboard
                ElseIf info.Node.Level = 2 Then
                    Me.DashboardGroupReportTreeSelectionType = DashboardSelectionType.DashboardReport
                End If
            Else
                Me.DashboardGroupReportTreeSelectionType = DashboardSelectionType.NotSelected
            End If
        ElseIf e.Button = MouseButtons.Left Then
            IsDashboardGroupReportMouseDownRight = False
            Dim data As TreeListNode = tvDashboardGroupReport.GetNodeAt(e.Location)
            If data IsNot Nothing Then
                tvDashboardGroupReport.DoDragDrop(data, DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub btnAddDashborad_Click(sender As Object, e As EventArgs) Handles btnAddDashborad.Click
        tsmi_Dashboard_Insert_Click(Nothing, Nothing)
    End Sub

    Private Sub cmbDashboardReportGroup_Properties_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles cmbDashboardReportGroup.Properties.ButtonClick
        Try
            If (e.Button.Tag.ToString.ToUpper = "REFRESH") Then
                BindDashboardReportGroup()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tvDashboardGroupReport_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles tvDashboardGroupReport.CellValueChanged
        Dim dashboardTreeViewNode As TreeListNode = e.Node
        Try
            If (e.Value IsNot Nothing) Then
                If Not (e.Value = dashboardTreeViewNode.Item(DashBoardTreeFields.DASHBOARDNAME)) Then
                    Dim selectedNodeId As String = tvDashboardGroupReport.FocusedNode.Tag
                    If (dashboardTreeViewNode.Level = 0) Then
                        Dim isPrivate As Boolean = IIf(tsmi_ReportGroupStatusPrivate.Checked, True, False)
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboardGroups.Modify(selectedNodeId, e.Value, isPrivate, System.Environment.UserName))
                        isDashboradGroupSelectedIndexChanged = False
                        BindDashboardGroupCombo()
                        'Dim index = vCmb_DashboradGroup.Items.ToList().FindIndex(Function(c) c.ToString() = e.Label)
                        cmbDashboardGroup.SelectedItem = GetComboItemFromValue(e.Value, cmbDashboardGroup)
                        isDashboradGroupSelectedIndexChanged = True

                    ElseIf (dashboardTreeViewNode.Level = 1) Then
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboards.Modify(selectedNodeId, e.Value, System.Environment.UserName))

                    ElseIf (dashboardTreeViewNode.Level = 2) Then
                        Dim reportGroupID As String = TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Value
                        Dim countAffected As Integer = DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.ModifyReports(reportGroupID, selectedNodeId, e.Value, System.Environment.UserName))
                        If countAffected <> 1 Then
                            SetMessage("Report name already exists, try another name.")
                        End If
                    End If
                    dashboardTreeViewNode.Item(DashBoardTreeFields.DASHBOARDNAME) = e.Value.ToString
                End If
            End If
            tvDashboardGroupReport.OptionsBehavior.Editable = False
            tvDashboardGroupReport.OptionsBehavior.ReadOnly = True
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub BindDashboardReportGroup()
        Dim selectReportGroups As String = New SQLReportGroups().SelectAll(ReportGroupsFields.REPORT_GROUP_NAME, True)
        Dim initializeDT As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportGroups.GetReportGroups(Environment.UserName.ToString))
        If (initializeDT.IsValid) Then
            BindDevExComboBoxWithTagMember(cmbDashboardReportGroup, initializeDT, ReportGroupsFields.REPORT_GROUP_ID, ReportGroupsFields.REPORT_GROUP_NAME, "None", ReportGroupsFields.REPORT_GROUP_PRIVATE, True)
        Else
            ClearComboBox(cmbDashboardReportGroup, "None")
        End If
    End Sub

    Private Sub cmbDashboardReportGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDashboardReportGroup.SelectedIndexChanged
        Try
            If (cmbDashboardReportGroup.SelectedIndex > 0) Then
                RefreshReportTLV("DashboradReports", cmbDashboardReportGroup)
            Else
                tvDashboardReports.Nodes.Clear()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Sub RefreshReportTLV(ByVal treeType As String, ByRef cmbGroup As ComboBoxEdit)
        'Update tlv
        If cmbGroup.SelectedItem Is Nothing Then
            Exit Sub
        End If
        Dim rptTreeList As New TreeList
        If treeType = "DashboradReports" Then
            rptTreeList = tvDashboardReports
        ElseIf treeType = "SchedulerReports" Then
            rptTreeList = tvSchedulerReports
        End If

        rptTreeList.Cursor = Cursors.WaitCursor
        rptTreeList.BeginUnboundLoad()
        Application.DoEvents()

        Dim sqlCommand As String = New SQLReportTree().SelectAll(False, "( " & ReportTreeFields.REPORT_GROUP_USERS & OperatorConst.Equal & Chr(39) & System.Environment.UserName & Chr(39) & AggregateConst.AND_Only &
                                                                 " ( " & ReportTreeFields.REPORT_GROUP_PRIVATE & OperatorConst.Equal & "0" & AggregateConst.OR_Only & ReportTreeFields.REPORT_GROUP_CREATOR & OperatorConst.Equal & Chr(39) &
                                                                 System.Environment.UserName & "' )) " & AggregateConst.AND_Only & ReportTreeFields.REPORT_GROUP_ID & OperatorConst.Equal & TryCast(cmbGroup.SelectedItem, clsComboBoxItem).Value)
        Dim dtQODBC As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCommand)

        Dim colList() As String = {ReportTreeFields.REPORT_GROUP_ID, ReportTreeFields.REPORT_GROUP_NAME,
                                           ReportTreeFields.REPORT_CATEGORY_ID, ReportTreeFields.REPORT_CATEGORY_NAME, ReportTreeFields.REPORT_CATEGORY_ORDINAL,
                                           ReportTreeFields.REPORT_ID, ReportTreeFields.REPORT_NAME}

        rptTreeList.Columns.Clear()
        For i As Integer = 0 To colList.Length - 1
            Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
            col1.Caption = colList(i)
            col1.VisibleIndex = i
            If colList(i) = ReportTreeFields.REPORT_GROUP_NAME Then
                rptTreeList.AutoFillColumn = col1
                col1.Visible = True
            Else
                col1.Visible = False
            End If
            rptTreeList.Columns.Add(col1)
        Next
        rptTreeList.Nodes.Clear()

        Try
            Dim node As TreeListNode = Nothing

            If (dtQODBC.IsValid) Then
                Dim tlNode As TreeListNode = rptTreeList.Nodes.Add(New Object() {dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_ID), dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_NAME), 0, "", -1, 0, ""})
                Dim groupName As String = dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_NAME)
                Dim groupID As String = dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_ID).ToString

                ToolTipController1.SetToolTip(rptTreeList, "Group" & "_" & IIf(dtQODBC.Rows(0)(ReportTreeFields.REPORT_GROUP_PRIVATE).ToString.ToUpper = "TRUE", 1, 0))
                tlNode.Tag = groupID

                Dim distinctCol() As String = {ReportTreeFields.REPORT_CATEGORY_NAME, ReportTreeFields.REPORT_GROUP_ORDINAL, ReportTreeFields.REPORT_CATEGORY_ID}
                Dim dtView As DataView = dtQODBC.DistinctCol(distinctCol).DefaultView
                dtView.Sort = ReportTreeFields.REPORT_GROUP_ORDINAL & " ASC"
                Dim dtDistinctGroupName As DataTable = dtView.ToTable()
                If (dtDistinctGroupName.IsValid) Then
                    Dim nodeIndex As Integer = 0
                    For Each rowGroupName As DataRow In dtDistinctGroupName.Rows

                        node = rptTreeList.AppendNode(New Object() {rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID), rowGroupName(ReportTreeFields.REPORT_CATEGORY_NAME), rowGroupName(ReportTreeFields.REPORT_GROUP_ORDINAL)}, tlNode)
                        node.Tag = rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID)

                        Dim reportFilter As String = ReportTreeFields.REPORT_CATEGORY_ID & OperatorConst.Equal & rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID)
                        Dim dtDistinctReport As DataTable = dtQODBC.SelectedRowsAsTable(reportFilter)
                        If dtDistinctReport.IsValid Then
                            Dim dr As DataRow() = dtDistinctReport.Select("", ReportTreeFields.REPORT_CATEGORY_ORDINAL & " ASC ")
                            For Each drow As DataRow In dr

                                Dim rptNode As TreeListNode = rptTreeList.AppendNode(New Object() {drow.Item(ReportTreeFields.REPORT_ID).ToString, drow.Item(ReportTreeFields.REPORT_NAME).ToString, drow.Item(ReportTreeFields.REPORT_CATEGORY_ORDINAL).ToString,
                                                                                  rowGroupName(ReportTreeFields.REPORT_CATEGORY_ID), ReportTreeFields.REPORT_CATEGORY_NAME}, node)
                                rptNode.Tag = drow.Item(DashBoardTreeFields.REPORTID).ToString
                            Next
                        End If
                        nodeIndex = nodeIndex + 1
                    Next
                End If
                rptTreeList.ExpandAll()
            Else
                ToolTipController1.SetToolTip(rptTreeList, "Group" & "_" & IIf(TryCast(cmbGroup.SelectedItem, clsComboBoxItem).Tag.ToString.ToUpper = "TRUE", 1, 0))
                Dim tlNode As TreeListNode = rptTreeList.Nodes.Add(New Object() {TryCast(cmbGroup.SelectedItem, clsComboBoxItem).Value, cmbGroup.SelectedItem.ToString.Trim, 0, "", -1, 0, ""})
                tlNode.Tag = TryCast(cmbGroup.SelectedItem, clsComboBoxItem).Value
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If

            rptTreeList.EndUnboundLoad()
            If rptTreeList.Nodes.Count > 0 Then
                rptTreeList.SelectNode(rptTreeList.Nodes(0))
                rptTreeList.SetFocusedNode(rptTreeList.Nodes(0))
                rptTreeList.CollapseAll()
                rptTreeList.ExpandAll()
            End If
            rptTreeList.Cursor = Cursors.Default
            Application.DoEvents()

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub cmsDashBoardGroupTree_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsDashBoardGroupTree.Opening
        isByClick = True
        Try
            If (tvDashboardGroupReport.Nodes.Count > 0) Then
                tvDashboardGroupReport.ContextMenuStrip.Show()
                tvDashboardGroupReport_FocusedNodeChanged(Nothing, Nothing)
            Else
                e.Cancel = True
                tvDashboardGroupReport.ContextMenuStrip.Hide()
                Return
            End If
            If (tvDashboardGroupReport.FocusedNode IsNot Nothing) Then
                If (tvDashboardGroupReport.FocusedNode.Level = 0) Then
                    isByClick = False
                    Dim isPrivate As Boolean = IIf(ToolTipController1.GetToolTip(tvDashboardGroupReport).Split("_")(1).ToString.ToUpper = "1", True, False)
                    tsmi_DashboardGroup_ShareStatusPrivate.Checked = isPrivate
                    tsmi_DashboardGroup_ShareStatusPublic.Checked = IIf(isPrivate, False, True)

                    isByClick = True
                End If
            Else
                tvDashboardGroupReport.ContextMenuStrip.Hide()
                Return
            End If
        Catch ex As Exception
        End Try

        Try
            Dim ColumnDep As String = tvDashboardGroupReport.Nodes(0).GetValue(tvDashboardGroupReport.Columns("DashboardGroupName"))
            If (Me.DashboardGroupReportTreeSelectionType = DashboardSelectionType.DashboardGroup) Then
                If TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Tag.ToUpper = Environment.UserName.ToUpper Then
                    tsmi_DashboardGroup_Delete.Enabled = True
                    tsmi_DashboardGroup_Modify.Enabled = True
                    tsmi_DashboardGroup_Rename.Enabled = True
                    tsmi_DashboardGroup_ShareStatus.Enabled = True
                    tsmi_DashboardGroup_ShareStatusPrivate.Enabled = True
                    tsmi_DashboardGroup_ShareStatusPublic.Enabled = True
                Else
                    tsmi_DashboardGroup_Delete.Enabled = False
                    tsmi_DashboardGroup_Modify.Enabled = False
                    tsmi_DashboardGroup_Rename.Enabled = False
                    tsmi_DashboardGroup_ShareStatus.Enabled = False
                    tsmi_DashboardGroup_ShareStatusPrivate.Enabled = False
                    tsmi_DashboardGroup_ShareStatusPublic.Enabled = False
                End If
                tsmi_Dashboard_Delete.Enabled = False
                tsmi_Dashboard_Insert.Enabled = False
                tsmi_Dashboard_Rename.Enabled = False
                tsmi_DashboardReport_Delete.Enabled = False

            ElseIf (Me.DashboardGroupReportTreeSelectionType = DashboardSelectionType.Dashboard) Then
                tsmi_DashboardGroup_Delete.Enabled = False
                tsmi_DashboardGroup_Modify.Enabled = False
                tsmi_DashboardGroup_Rename.Enabled = False
                tsmi_DashboardGroup_ShareStatus.Enabled = False
                tsmi_Dashboard_Delete.Enabled = True
                tsmi_Dashboard_Insert.Enabled = True
                tsmi_Dashboard_Rename.Enabled = True
                tsmi_DashboardReport_Delete.Enabled = False

            ElseIf (Me.DashboardGroupReportTreeSelectionType = DashboardSelectionType.DashboardReport) Then
                tsmi_DashboardGroup_Delete.Enabled = False
                tsmi_DashboardGroup_Modify.Enabled = False
                tsmi_DashboardGroup_Rename.Enabled = False
                tsmi_DashboardGroup_ShareStatus.Enabled = False
                tsmi_Dashboard_Delete.Enabled = False
                tsmi_Dashboard_Insert.Enabled = False
                tsmi_Dashboard_Rename.Enabled = False
                tsmi_DashboardReport_Delete.Enabled = True

            Else
                tsmi_DashboardGroup_Delete.Enabled = False
                tsmi_DashboardGroup_Modify.Enabled = False
                tsmi_DashboardGroup_Rename.Enabled = False
                tsmi_DashboardGroup_ShareStatus.Enabled = False
                tsmi_Dashboard_Delete.Enabled = False
                tsmi_Dashboard_Insert.Enabled = False
                tsmi_Dashboard_Rename.Enabled = False
                tsmi_DashboardReport_Delete.Enabled = False

            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub tvDashboardGroupReport_MouseMove(sender As Object, e As MouseEventArgs) Handles tvDashboardGroupReport.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim data As TreeListNode = tvDashboardGroupReport.GetNodeAt(e.Location)
                If data IsNot Nothing Then
                    tvDashboardGroupReport.DoDragDrop(data, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Control_DragOver(sender As Object, e As DragEventArgs) Handles tvDashboardGroupReport.DragOver, tvReportGroup.DragOver, tvObjects.DragOver, tvSchedulerJob.DragOver, lbDimensions.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub tvDashboardGroupReport_DragDrop(sender As Object, e As DragEventArgs) Handles tvDashboardGroupReport.DragDrop
        Try
            Dim args As DXDragEventArgs = tvDashboardGroupReport.GetDXDragEventArgs(e)
            Dim targetNode As TreeListNode = args.TargetNode
            Dim sourceNode As TreeListNode = e.Data.GetData(GetType(TreeListNode))
            Dim sourcetext As String = sourceNode.GetDisplayText("DashboardGroupName")
            If (targetNode IsNot Nothing AndAlso targetNode.Level = 1 AndAlso sourceNode.Level = 2 AndAlso sourceNode.TreeList.Name = "tvDashboardReports") Then
                Dim dashboardID As String = targetNode.Tag
                Dim reportDragDropId As String = sourceNode.Tag
                Try
                    Dim result As Integer = DataAccessorODBC.ExecuteScalar(connStrSandBoxServer, SQLDashboardReports.DashboardReportsInsert(dashboardID, reportDragDropId))
                    If (result = 1) Then
                        RefreshDashboardGroupReportTLV()
                        SetMessage("Report Successfully dragged.")
                    Else
                        SetMessage("Fail : Report already exist.")
                    End If
                    tvDashboardGroupReport.SetFocusedNode(tvDashboardGroupReport.FindNodeByFieldValue("DashboardGroupName", sourcetext))
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                End Try
            ElseIf (sourceNode.ParentNode.Tag = targetNode.ParentNode.Tag) Then
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboardReports.SwapDashboardReportOrdinal(CInt(sourceNode(0)), CInt(targetNode.Tag), sourceNode.ParentNode.Tag))
                RefreshDashboardGroupReportTLV()
                tvDashboardGroupReport.SetFocusedNode(tvDashboardGroupReport.FindNodeByFieldValue("DashboardGroupName", sourcetext))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub TvDashboardReports_MouseMove(sender As Object, e As MouseEventArgs) Handles tvDashboardReports.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim node As Nodes.TreeListNode = tvDashboardReports.FocusedNode
                Dim data As TreeListNode = tvDashboardReports.GetNodeAt(e.Location)
                If data IsNot Nothing Then
                    tvDashboardReports.DoDragDrop(data, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tvDashboardReports_DragDrop(sender As Object, e As DragEventArgs) Handles tvDashboardReports.DragDrop
        tvDashboardReports.SuspendLayout()
        Dim treeNodePath As String = Nothing
        Dim targetNode As TreeListNode = Nothing
        Dim pt As Point = tvDashboardReports.PointToClient(New Point(e.X, e.Y))
        targetNode = tvDashboardReports.CalcHitInfo(pt).Node

        Dim sourceNode As TreeListNode = e.Data.GetData(GetType(TreeListNode))
        Dim sourcetext As String = sourceNode.GetDisplayText("ReportGroupName")
        Try
            Me.UseWaitCursor = True
            If (sourceNode.Level = 2 AndAlso targetNode.Level = 2) AndAlso (sourceNode.ParentNode.Tag = targetNode.ParentNode.Tag) Then
                treeNodePath = targetNode.ParentNode.ParentNode.GetDisplayText("ReportGroupName") & "\" & targetNode.ParentNode.GetDisplayText("ReportGroupName") & "\" & targetNode.GetDisplayText("ReportGroupName") & "\" & sourceNode(1).ToString
                Dim sourceReportId As Integer = 0
                Dim targatReportId As Integer = 0
                Dim sourceCategoryId As Integer = 0
                Dim targatCategoryId As Integer = 0
                sourceReportId = sourceNode.Tag
                targatReportId = targetNode.Tag
                sourceCategoryId = sourceNode(2)
                targatCategoryId = targetNode.ParentNode.Tag
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.SwapReportOrdinal(sourceReportId, targatReportId))
                RefreshReportTLV("DashboradReports", cmbDashboardReportGroup)
            ElseIf (sourceNode.Level = 1 AndAlso targetNode.Level = 1) Then
                Dim targetNodeID As Integer = CInt(targetNode.Tag)
                Dim sourceNodeID As Integer = CInt(sourceNode(0))
                Dim sourceNodeOrdinal As Integer = CInt(sourceNode(2))
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.SwapCategoryOrdinal(targetNodeID, sourceNodeID, sourceNodeOrdinal))
                RefreshReportTLV("DashboradReports", cmbDashboardReportGroup)
            ElseIf (sourceNode.Level = 2 AndAlso targetNode.Level = 1) Then
                'Drag a report to another category
                DragReportToCategory(sourceNode.Tag, targetNode.Tag)
                RefreshReportTLV("DashboradReports", cmbDashboardReportGroup)
            ElseIf (sourceNode.ParentNode.Tag <> targetNode.ParentNode.Tag) Then
                SetMessage("Sorry : You can drag report to category only.")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.UseWaitCursor = False
        End Try
        targetNode = GetNodeFromPath(tvDashboardReports.Nodes, treeNodePath)
        tvDashboardReports.SetFocusedNode(tvDashboardReports.FindNodeByFieldValue("ReportGroupName", sourcetext))
        If (targetNode IsNot Nothing) Then
            targetNode.Visible = True
        End If
        tvDashboardReports.Refresh()
        tvDashboardReports.ResumeLayout()
    End Sub

    Private Sub TvDashboardReports_DragOver(sender As Object, e As DragEventArgs) Handles tvDashboardReports.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub tsmi_DashboardGroup_Delete_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardGroup_Delete.Click
        If (tvDashboardGroupReport.FocusedNode IsNot Nothing) Then
            Try
                If (tvDashboardGroupReport.FocusedNode.Level = 0) Then
                    Dim dashboardGroupID As String = tvDashboardGroupReport.FocusedNode.Tag
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboardGroups.Delete(dashboardGroupID))
                    BindDashboardGroupCombo()
                    RefreshDashboardGroupReportTLV()
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub tsmi_Dashboard_Insert_Click(sender As Object, e As EventArgs) Handles tsmi_Dashboard_Insert.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (cmbDashboardGroup.SelectedIndex > 0) Then
                Dim dashBoardID As String = "0"
                If (tvDashboardGroupReport.FocusedNode IsNot Nothing) Then
                    If (tvDashboardGroupReport.FocusedNode.Level = 1) Then
                        dashBoardID = tvDashboardGroupReport.FocusedNode.Tag.ToString
                    ElseIf (tvDashboardGroupReport.FocusedNode.Level = 2) Then
                        dashBoardID = tvDashboardGroupReport.FocusedNode.ParentNode.Tag.ToString
                    End If
                End If
                Dim dashBoardGroupID As Integer = TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Value

                Dim dashBoardName As String = "Add DashBoard"
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboards.Insert(dashBoardName, dashBoardGroupID, System.Environment.UserName.ToString))
                RefreshDashboardGroupReportTLV()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Dashboard_Delete_Click(sender As Object, e As EventArgs) Handles tsmi_Dashboard_Delete.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tvDashboardGroupReport.FocusedNode IsNot Nothing) Then
                If (tvDashboardGroupReport.FocusedNode.Level = 1) Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboards.Delete(tvDashboardGroupReport.FocusedNode.Tag.ToString))
                    tvDashboardGroupReport.Nodes.Clear()
                    RefreshDashboardGroupReportTLV()
                Else
                    SetMessage("Select any Node from tree")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Dashboard_Rename_Click(sender As Object, e As EventArgs) Handles tsmi_Dashboard_Rename.Click
        If (tvDashboardGroupReport.FocusedNode.Level = 1) Then
            tvDashboardGroupReport.OptionsBehavior.Editable = True
            tvDashboardGroupReport.OptionsBehavior.ReadOnly = False
        End If
    End Sub

    Private Sub tsmi_DashboardGroup_Rename_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardGroup_Rename.Click
        If (tvDashboardGroupReport.FocusedNode.Level = 0) Then
            tvDashboardGroupReport.OptionsBehavior.Editable = True
            tvDashboardGroupReport.OptionsBehavior.ReadOnly = False
        End If
    End Sub

    Private Sub tsmi_DashboardReport_Delete_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardReport_Delete.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tvDashboardGroupReport.FocusedNode IsNot Nothing) Then
                If (tvDashboardGroupReport.FocusedNode.Level = 2) Then
                    Dim dashboardID As String = tvDashboardGroupReport.FocusedNode.ParentNode.Tag.ToString
                    Dim reportID As String = tvDashboardGroupReport.FocusedNode.Tag.ToString
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboardReports.DashboardReportsDelete(dashboardID, reportID))
                    tvDashboardGroupReport.Nodes.Clear()
                    RefreshDashboardGroupReportTLV()
                    SetMessage("Dashboard Report Successfully Deleted.")
                End If

            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_DashboardGroup_Collapse_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardGroup_Collapse.Click
        If (tvDashboardGroupReport.Nodes.Count > 0) Then
            tvDashboardGroupReport.CollapseAll()
        End If
    End Sub

    Private Sub tsmi_DashboardGroup_Expand_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardGroup_Expand.Click
        tvDashboardGroupReport.ExpandAll()
    End Sub

    Private Sub tsmi_DashboardGroup_ShareStatusPublic_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_DashboardGroup_ShareStatusPublic.CheckedChanged
        Try
            If (tvDashboardGroupReport.FocusedNode IsNot Nothing) Then
                If (tvDashboardGroupReport.FocusedNode.Level = 0) Then
                    Dim reportGroupId As String = tvDashboardGroupReport.FocusedNode.Tag
                    Dim reportGroupName As String = tvDashboardGroupReport.FocusedNode.GetDisplayText("DashboardGroupName")
                    Dim isPrivate As Boolean = IIf(tsmi_DashboardGroup_ShareStatusPublic.Checked, False, True)
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboardGroups.Modify(reportGroupId, reportGroupName, isPrivate, System.Environment.UserName))
                    If (tsmi_DashboardGroup_ShareStatusPublic.Checked) Then
                        tsmi_DashboardGroup_ShareStatusPrivate.Checked = False
                    Else
                        tsmi_DashboardGroup_ShareStatusPrivate.Checked = True
                        tsmi_DashboardGroup_ShareStatusPublic.Checked = False
                    End If
                    RefreshDashboardGroupReportTLV()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_DashboardGroup_ShareStatusPrivate_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_DashboardGroup_ShareStatusPrivate.CheckedChanged
        Try
            If (tvDashboardGroupReport.FocusedNode IsNot Nothing) Then
                If (tvDashboardGroupReport.FocusedNode.Level = 0) Then
                    Dim reportGroupId As String = tvDashboardGroupReport.FocusedNode.Tag
                    Dim reportGroupName As String = tvDashboardGroupReport.FocusedNode.GetDisplayText("")
                    Dim isPrivate As Boolean = IIf(tsmi_DashboardGroup_ShareStatusPrivate.Checked, True, False)
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLDashboardGroups.Modify(reportGroupId, reportGroupName, isPrivate, System.Environment.UserName))
                    If (tsmi_DashboardGroup_ShareStatusPrivate.Checked) Then
                        tsmi_DashboardGroup_ShareStatusPublic.Checked = False
                    Else
                        tsmi_DashboardGroup_ShareStatusPrivate.Checked = False
                        tsmi_DashboardGroup_ShareStatusPublic.Checked = True
                    End If
                    RefreshDashboardGroupReportTLV()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub BindChart(ByRef dtChartConfig As DataTable, ByVal isNeedSQLRegenerate As Boolean)
        Try
            If (tvReportGroup.FocusedNode IsNot Nothing) Then
                Dim reportId As String = tvReportGroup.FocusedNode.Tag.ToString
                If (isNeedSQLRegenerate) Then
                    reportChartGrid_SendBox.SetChartContextMenu = cms_ReportChartGrid
                    reportChartGrid_SendBox.SetGridContextMenu = cms_ReportChartGrid
                    reportChartGrid_SendBox.ReportAxisData = dtReportAxisData
                    reportChartGrid_SendBox.ReportFilter = dtReportFilterData
                    reportChartGrid_SendBox.dtChartConfig = dtChartConfig
                    reportChartGrid_SendBox.ChartObjectsData = IIf(IsDBNull(dtChartConfig.Rows(0)(ReportChartFields.ObjectNamesInReport)), "", dtChartConfig.Rows(0)(ReportChartFields.ObjectNamesInReport)) 'GetTreeViewReportObjects()
                    ''Process_Top_3G(dtChartConfig, reportChartGrid_SendBox)
                    reportChartGrid_SendBox.reportSQL = IIf(IsDBNull(dtChartConfig.Rows(0)(ReportChartFields.ReportSQL)), "", dtChartConfig.Rows(0)(ReportChartFields.ReportSQL))
                    reportChartGrid_SendBox.reportConnString = IIf(IsDBNull(dtChartConfig.Rows(0)(ReportChartFields.ReportConnString)), "", dtChartConfig.Rows(0)(ReportChartFields.ReportConnString))
                    'AddHandler reportChartGrid_SendBox.ChartDragDropEvent, AddressOf reportChartGridSendBox_DragDropEvent

                    Dim dedateFormat As String = ""
                    If (cmbTimeResolution.SelectedItem.ToString.ToUpper = "RAW" Or cmbTimeResolution.SelectedItem.ToString.ToUpper = "HOUR") Or ((rdoFilterDaysExc.Checked = True Or rdoFilterDaysInc.Checked = True) Or (rdoFilterHrsExc.Checked = True Or rdoFilterHrsInc.Checked = True) Or (cmbPredefinedFilter.SelectedIndex > 0)) Then
                        dedateFormat = "dd/MM/yy HH:mm"
                    ElseIf (cmbTimeResolution.SelectedItem.ToString.ToUpper = "DAY") Or (rdoFilterDaysExc.Checked = True Or rdoFilterDaysInc.Checked = True) Or (rdoFilterHrsExc.Checked = True Or rdoFilterHrsInc.Checked = True) Or (cmbPredefinedFilter.SelectedIndex > 0) Then
                        dedateFormat = "dd/MM/yy"
                    ElseIf (cmbTimeResolution.SelectedItem.ToString.ToUpper = "MONTH") And (rdoFilterDaysExc.Checked = True Or rdoFilterDaysInc.Checked = True) Or (rdoFilterHrsExc.Checked = True Or rdoFilterHrsInc.Checked = True) Or (cmbPredefinedFilter.SelectedIndex > 0) Then
                        dedateFormat = "MMMM"
                    End If

                    reportChartGrid_SendBox.GridorChart = Me.GridorChart
                    reportChartGrid_SendBox.GenerateSQL(dedateFormat, CInt(txtQueryTimeOut.Text))
                Else
                    reportChartGrid_SendBox.ReportAxisData = dtReportAxisData
                    reportChartGrid_SendBox.ReportFilter = dtReportFilterData
                    reportChartGrid_SendBox.dtChartConfig = dtChartConfig
                    reportChartGrid_SendBox.ChartObjectsData = dtChartConfig.Rows(0)(ReportChartFields.ObjectNamesInReport) 'GetTreeViewReportObjects()
                    reportChartGrid_SendBox.RefreshChartConfig()

                    ''Me.reportChartGrid_SendBox.ButtonClickEvent += new System.EventHandler(Me.myCustomControl_ButtonClickEvent);
                End If
            End If

            'If flp_ValueX.Controls.Count > 1 And reportChartGrid_SendBox.ChartObjectsData = "" Then
            '    Dim tempSplitCont As System.Windows.Forms.SplitContainer = GetSplitControl(cm_SourceControl)

            '    Try
            '        If (tempSplitCont IsNot Nothing) Then
            '            If (tempSplitCont.Panel2Collapsed.Equals(True)) Then

            '                tempSplitCont.Panel2Collapsed = False
            '                tempSplitCont.Panel1Collapsed = True
            '                tempSplitCont.Panel1.Hide()
            '            End If
            '        End If
            '        Application.DoEvents()

            '    Catch
            '    End Try
            'Else
            '    Dim tempSplitCont As System.Windows.Forms.SplitContainer = GetSplitControl(cm_SourceControl)

            '    Try
            '        If (tempSplitCont IsNot Nothing) Then
            '            If (tempSplitCont.Panel1Collapsed.Equals(True)) Then

            '                tempSplitCont.Panel1Collapsed = False
            '                tempSplitCont.Panel2Collapsed = True
            '                tempSplitCont.Panel2.Hide()
            '            End If
            '        End If
            '        Application.DoEvents()

            '    Catch
            '    End Try
            'End If

            ' show chart/grid as per report config settings
            reportChartGrid_SendBox.ShowGridChartPanel()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            mm.ReleaseMemory()
        End Try
    End Sub

    Private Sub spinEdit_DashboradReportPerRow_EditValueChanged(sender As Object, e As EventArgs) Handles spinEdit_DashboradReportPerRow.EditValueChanged
        Try
            If (spinEdit_DashboradReportPerRow.Value > 0) Then
                If (xtcDashboards.TabPages.Count > 0) Then
                    Dim tabPage As DevExpress.XtraTab.XtraTabPage = xtcDashboards.TabPages(0)
                    If (tabPage IsNot Nothing) Then
                        If (tabPage.Controls.Count > 0) Then
                            Dim flpDashboardChartGrid As FlowLayoutPanel = TryCast(tabPage.Controls(0), FlowLayoutPanel)
                            If (flpDashboardChartGrid IsNot Nothing) Then
                                If (flpDashboardChartGrid.Controls.Count > 0) Then
                                    For Each rcgControl As Control In flpDashboardChartGrid.Controls
                                        Dim rcg As ReportChartGrid = TryCast(rcgControl, ReportChartGrid)
                                        If (rcg IsNot Nothing) Then
                                            Dim widthValue As Integer = (flpDashboardChartGrid.Width - 20) / spinEdit_DashboradReportPerRow.Value - ((spinEdit_DashboradReportPerRow.Value - 1) * 2) - 5
                                            'Dim heightValue As Integer = widthValue / 2
                                            Dim heightValue As Integer = rcg.Size.Height
                                            rcg.Size = New Size(widthValue, heightValue)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub trackBar_DashboardChartSize_EditValueChanged(sender As Object, e As EventArgs) Handles trackBar_DashboardChartSize.EditValueChanged
        Try
            If (xtcDashboards.TabPages.Count > 0) Then
                Dim tabPage As DevExpress.XtraTab.XtraTabPage = xtcDashboards.TabPages(0)
                If (tabPage IsNot Nothing) Then
                    If (tabPage.Controls.Count > 0) Then
                        Dim flpDashboardChartGrid As FlowLayoutPanel = TryCast(tabPage.Controls(0), FlowLayoutPanel)
                        If (flpDashboardChartGrid IsNot Nothing) Then
                            If (flpDashboardChartGrid.Controls.Count > 0) Then
                                For Each rcgControl As Control In flpDashboardChartGrid.Controls
                                    Dim rcg As ReportChartGrid = TryCast(rcgControl, ReportChartGrid)
                                    If (rcg IsNot Nothing) Then
                                        Dim widthValue As Integer = rcg.Size.Width
                                        'Dim heightValue As Integer = widthValue / 2
                                        Dim heightValue As Integer = trackBar_DashboardChartSize.Value
                                        rcg.Size = New Size(widthValue, heightValue)
                                    End If

                                Next
                            End If
                        End If
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub tglBtnMonitorMode_Click(sender As Object, e As EventArgs) Handles tglBtnMonitorMode.Click
        Try
            tglBtnMonitorMode.ChangeToggleState()
            If tglBtnMonitorMode.ToggleState = CheckState.Checked Then
                timerCountdown.Enabled = True
                tglBtnMonitorMode.Text = "Stop"
                timeMonitor = TimeSerial(0, CInt(SpinEdit4.Value), 0)
                timerCountdown.Start()
            Else
                timeMonitor = TimeSerial(0, 0, 0)
                tglBtnMonitorMode.Text = "Start"
                timerCountdown.Stop()
                timerCountdown.Enabled = False
                lblDashboardTimer.Text = "Not Set"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub timerCountdown_Tick(sender As Object, e As EventArgs) Handles timerCountdown.Tick
        Try
            If timeMonitor.Hour + timeMonitor.Minute + timeMonitor.Second > 0 Then
                ' Display the new time left
                ' by updating the Time Left label.
                timeMonitor = DateAdd(DateInterval.Second, -1, timeMonitor)
                lblDashboardTimer.Text = timeMonitor.ToString("HH:mm:ss")
            Else
                ' If the user ran out of time, stop the timer, show
                ' a XtraMessageBox, and fill in the answers.
                timeMonitor = TimeSerial(0, CInt(SpinEdit4.Value), 0)
                lblDashboardTimer.Text = "REFRESHING"
                Application.DoEvents()
                WaitScreen.ShowWaitScreen("Dashboard reports refreshing data")
                If dtDashboardGroup.IsValid() AndAlso Not dtDashboardGroup Is Nothing Then
                    For Each drDashboard As DataRow In dtDashboardGroup.Rows
                        RefreshReportChartGridDashboardId(drDashboard("DashboardID").ToString, drDashboard("DashboardName").ToString)
                    Next
                End If
            End If
        Catch
        Finally
            WaitScreen.CloseWaitScreen()
        End Try
    End Sub

    Private Sub RefreshReportChartGridDashboardId(ByVal dashboardId As String, ByVal dashboardName As String)
        Try
            Dim vTabPageNew As XtraTabPage = xtcDashboards.TabPages.First(Function(x) x.Text = dashboardName AndAlso x.Tag = dashboardId)
            If (vTabPageNew IsNot Nothing) Then
                Dim flpDashboard As FlowLayoutPanel = TryCast(vTabPageNew.Controls(0), FlowLayoutPanel)
                If (flpDashboard IsNot Nothing) Then
                    Dim dt_DashboardReportCharts As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLDashboards.GetDashBoardReportChart(dashboardId))
                    If (dt_DashboardReportCharts.IsValid) Then
                        Dim dtDistictReportId As DataTable = dt_DashboardReportCharts.Select("", DashboardReportsFields.REPORT_ORDINAL & " ASC").CopyToDataTable().DistinctCol(ReportChartFields.ReportID)
                        If (dtDistictReportId.IsValid) Then
                            Dim dtDashboardReports As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLDashboardReports.GetDashBoardReport(dashboardId))

                            For Each dr As DataRow In dtDistictReportId.Rows
                                Dim dtChConfig As DataTable = dt_DashboardReportCharts.SelectedRowsAsTable(ReportChartFields.ReportID, OperatorConst.Equal, dr(ReportChartFields.ReportID))
                                Dim tabText = dtChConfig.Rows(0)(ReportChartFields.ChartTitle).ToString
                                Dim reportId = dtChConfig.Rows(0)(ReportChartFields.ReportID).ToString
                                Dim reportChartGrid As ReportChartGrid = TryCast(flpDashboard.Controls.Find(tabText.Replace(" ", "_") & reportId, True)(0), ReportChartGrid)
                                Dim dtDashboardReport As DataTable = dtDashboardReports.SelectedRowsAsTable(ReportChartFields.ReportID, OperatorConst.Equal, dr(ReportChartFields.ReportID))

                                Dim dsReportAxisData As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLReportChart.GetReportAxisData(reportId) & SQLReportContentFilter.GetReportContentFilter(reportId))
                                reportChartGrid.ReportAxisData = dsReportAxisData.Tables(0)
                                reportChartGrid.ReportFilter = dsReportAxisData.Tables(1)

                                Dim dedateFormat As String = ""
                                Dim timeResolution As String = dtChConfig(0)("TimeResolution").ToString
                                If timeResolution = "RAW" Or timeResolution = "HOUR" Then
                                    dedateFormat = "dd/MM/yy HH:mm"
                                ElseIf timeResolution = "DAY" Then
                                    dedateFormat = "dd/MM/yy"
                                ElseIf timeResolution = "MONTH" Then
                                    dedateFormat = "MMMM"
                                End If

                                If dtChConfig.Rows(0)("PredefinedID").ToString = "0" Then
                                    reportChartGrid.DTPredefinedReportID = Nothing
                                Else
                                    reportChartGrid.DTPredefinedReportID = dtPredefinedPeriodSB.Select("PredefinedPeriodID=" & CInt(dtChConfig.Rows(0)("PredefinedID").ToString)).CopyToDataTable
                                End If
                                reportChartGrid.RefreshSetting = True
                                reportChartGrid.GenerateSQL(dedateFormat, CInt(txtQueryTimeOut.Text))
                                reportChartGrid.ShowGridChartPanel()
                            Next
                        End If
                    Else
                        SetMessage("Chart not found corresponding to selected Dashboard.")
                    End If
                End If
            End If
        Catch ex As Exception
            SetMessage("Error : Not able to get Dashboard Report Charts ")
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub SplitContainerDashboard_SplitGroupPanelCollapsed(sender As Object, e As EventArgs)
        Try
            ResizeDashboardCharts()
        Catch
        End Try
    End Sub

    Private Sub btnRefreshDashbaord_Click(sender As Object, e As EventArgs) Handles btnRefreshDashbaord.Click
        Try
            xtcDashboards.TabPages.Clear()
            tvDashboardGroupReport.FocusedNode = tvDashboardGroupReport.Nodes(0)
            tvDashboardGroupReport_FocusedNodeChanged(tvDashboardGroupReport, Nothing)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

#Region "Scheduler"

    Private Sub cmbJobGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJobGroup.SelectedIndexChanged
        Try
            If (cmbJobGroup.SelectedIndex > 0) Then
                vBtn_AddJob.Enabled = True
                RefreshJobGroupReportTLV()
            Else
                tvSchedulerJob.Nodes.Clear()
                vBtn_AddJob.Enabled = False
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub RefreshJobGroupReportTLV()
        If cmbJobGroup.SelectedItem Is Nothing Then
            Exit Sub
        End If

        tvSchedulerJob.BeginUnboundLoad()
        Application.DoEvents()

        Dim sqlCommand As String = New SQLJobTree().SelectAll(False, "( " & JobTree_View.JOBGROUPCREATORNAME & OperatorConst.Equal & Chr(39) & System.Environment.UserName & Chr(39) & AggregateConst.AND_Only &
                                                              " ( " & JobTree_View.JOBGROUPPRIVATE & OperatorConst.Equal & "0" & AggregateConst.OR_Only & JobTree_View.JOBGROUPCREATORNAME & OperatorConst.Equal & Chr(39) & System.Environment.UserName & "' )) " &
                                                              AggregateConst.AND_Only & JobTree_View.JOBGROUPID & OperatorConst.Equal & TryCast(cmbJobGroup.SelectedItem, clsComboBoxItem).Value)
        Dim dtQODBC As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, sqlCommand)
        tvSchedulerJob.Nodes.Clear()
        Try
            Dim node As TreeListNode = Nothing
            Dim colList() As String = {JobTree_View.JOBGROUPID, JobTree_View.JOBGROUPNAME,
                                       JobTree_View.JOBID, JobTree_View.JOBNAME,
                                       DashBoardTreeFields.REPORTID, DashBoardTreeFields.REPORTNAME}

            tvSchedulerJob.Columns.Clear()
            For i As Integer = 0 To colList.Length - 1
                Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
                col1.Caption = colList(i)
                col1.VisibleIndex = i
                If colList(i) = JobTree_View.JOBGROUPNAME Then
                    tvSchedulerJob.AutoFillColumn = col1
                    col1.Visible = True
                Else
                    col1.Visible = False
                End If
                tvSchedulerJob.Columns.Add(col1)
            Next
            tvSchedulerJob.Nodes.Clear()

            If (dtQODBC.IsValid) Then
                Dim tlNode As TreeListNode = tvSchedulerJob.Nodes.Add(New Object() {dtQODBC.Rows(0)(JobTree_View.JOBGROUPID), dtQODBC.Rows(0)(JobTree_View.JOBGROUPNAME), 0, "", 0, ""})
                tlNode.Tag = dtQODBC.Rows(0)(JobTree_View.JOBGROUPID).ToString

                Dim distinctCol() As String = {JobTree_View.JOBNAME, JobTree_View.JOBID}
                Dim dtDistinctGroupName As DataTable = dtQODBC.DistinctCol(distinctCol)
                If (dtDistinctGroupName.IsValid) Then
                    For Each rowGroupName As DataRow In dtDistinctGroupName.Rows
                        If (Not IsDBNull(rowGroupName(JobTree_View.JOBNAME))) Then
                            node = tvSchedulerJob.AppendNode(New Object() {rowGroupName(JobTree_View.JOBID), rowGroupName(JobTree_View.JOBNAME)}, tlNode)
                            node.Tag = rowGroupName(JobTree_View.JOBID)

                            Dim reportFilter As String = JobTree_View.JOBID & OperatorConst.Equal & rowGroupName(JobTree_View.JOBID)
                            Dim dtDistinctReport As DataTable = dtQODBC.SelectedRowsAsTable(reportFilter)
                            If dtDistinctReport.IsValid Then

                                For Each drow As DataRow In dtDistinctReport.Rows
                                    Dim rptNode As TreeListNode = tvSchedulerJob.AppendNode(New Object() {drow.Item(DashBoardTreeFields.REPORTID).ToString, drow.Item(DashBoardTreeFields.REPORTNAME).ToString,
                                                                                  rowGroupName(JobTree_View.JOBID), JobTree_View.JOBNAME}, node)
                                    rptNode.Tag = drow.Item(DashBoardTreeFields.REPORTID).ToString
                                Next
                            End If
                        End If
                    Next
                End If
                tvSchedulerJob.ExpandAll()
            Else
                ToolTipController1.SetToolTip(tvSchedulerJob, "Group" & "_" & IIf(cmbJobGroup.SelectedItem.Tag.ToString.ToUpper = "TRUE", 1, 0))
                Dim tlNode As TreeListNode = tvSchedulerJob.Nodes.Add(New Object() {TryCast(cmbJobGroup.SelectedItem, clsComboBoxItem).Value, cmbJobGroup.SelectedItem.ToString.Trim, 0, "", 0, ""})
                tlNode.Tag = TryCast(cmbJobGroup.SelectedItem, clsComboBoxItem).Value
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
            tvSchedulerJob.EndUnboundLoad()
            If tvSchedulerJob.Nodes.Count > 0 Then
                tvSchedulerJob.SelectNode(tvSchedulerJob.Nodes(0))
                tvSchedulerJob.SetFocusedNode(tvSchedulerJob.Nodes(0))
                tvSchedulerJob.ExpandAll()
            End If
            tvSchedulerJob.Cursor = Cursors.Default
            Application.DoEvents()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub vBtn_AddJobGroup_Click(sender As Object, e As EventArgs) Handles vBtn_AddJobGroup.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim frmGroupInsert As New dlgSBGroupInsert()
            frmGroupInsert.SetConnectionString(connStrSandBoxServer)
            frmGroupInsert.GroupTypeInserting = GroupType.JobGroup

            frmGroupInsert.ShowDialog()
            Dim newGroupName As String = frmGroupInsert.NewGroup
            Dim RetrunData As Boolean = frmGroupInsert.IsGroupPrivate
            If (newGroupName IsNot Nothing) Then
                BindJobGroupCombo()
                Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(newGroupName, cmbJobGroup)    '.Properties.Items.ToList().FindIndex(Function(c) c.ToString() = newGroupName)
                cmbJobGroup.SelectedItem = cmbItem
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vBtn_AddJob_Click(sender As Object, e As EventArgs) Handles vBtn_AddJob.Click
        tsmi_JobInsert_Click(Nothing, Nothing)
    End Sub

    Private Sub tvSchedulerJob_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles tvSchedulerJob.CellValueChanged
        Dim jobTreeViewNode As TreeListNode = e.Node
        Try
            If (e.Value IsNot Nothing) Then
                If Not (e.Value = jobTreeViewNode.Item("ReportName")) Then
                    Dim selectedNodeId As String = tvSchedulerJob.FocusedNode.Tag
                    If (jobTreeViewNode.Level = 0) Then
                        Dim isPrivate As Boolean = IIf(tsmi_JobGroupShareStatus_Private.Checked, True, False)
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLJobGroups.ModifyJobGroup(selectedNodeId, e.Value, isPrivate, System.Environment.UserName))
                        BindJobGroupCombo()
                        Dim cmbItem As clsComboBoxItem = GetComboItemFromValue(e.Value, cmbJobGroup) '.Items.ToList().FindIndex(Function(c) c.ToString() = e.Label)
                        cmbJobGroup.SelectedItem = cmbItem
                    ElseIf (jobTreeViewNode.Level = 1) Then
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLJobs.JobRename(selectedNodeId, e.Value))
                    ElseIf (jobTreeViewNode.Level = 2) Then
                        Dim reportGroupID As String = TryCast(cmbDashboardGroup.SelectedItem, clsComboBoxItem).Value
                        Dim countAffected As Integer = DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLReports.ModifyReports(reportGroupID, selectedNodeId, e.Value, System.Environment.UserName))
                        If countAffected <> 1 Then
                            SetMessage("Report name already exists, try another name.")
                        End If
                    End If
                    jobTreeViewNode.Item("ReportName") = e.Value.ToString
                    ''e.CancelEdit = True
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tvSchedulerJob_AfterFocusNode(sender As Object, e As NodeEventArgs) Handles tvSchedulerJob.AfterFocusNode
        Try
            Me.UseWaitCursor = True
            Application.DoEvents()
            If Not tvSchedulerJob.FocusedNode Is Nothing Then
                DataMartGridView.ClearGrid(gcSchedulerJobReports, gvSchedulerJobReports)

                Dim treeNode As TreeListNode = tvSchedulerJob.FocusedNode
                If (treeNode.Level = 0) Then  'treeNode.ToolTipText.Split("_")(0) = "Group"
                    Me.JobGroupReportTreeSelectionType = JobSelectionType.JobGroup
                    vLbl_SelectedJob.Text = String.Empty
                    vBtn_JobCommit.Enabled = False
                ElseIf (treeNode.Level = 1) Then  'treeNode.ToolTipText = "Job"
                    vBtn_JobCommit.Enabled = True
                    Me.JobGroupReportTreeSelectionType = JobSelectionType.Job
                    vLbl_SelectedJob.Text = tvSchedulerJob.FocusedNode.GetDisplayText("JobGroupName")
                    GetJobAndReportData(tvSchedulerJob.FocusedNode.Tag.ToString) '', tvSchedulerJob.FocusedNode.Text.ToString)
                ElseIf (treeNode.Level = 2) Then  'treeNode.ToolTipText = "Report"
                    vBtn_JobCommit.Enabled = False
                    Me.JobGroupReportTreeSelectionType = JobSelectionType.JobReport
                    vLbl_SelectedJob.Text = String.Empty
                Else
                    Me.JobGroupReportTreeSelectionType = JobSelectionType.NotSelected
                    vLbl_SelectedJob.Text = String.Empty
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.UseWaitCursor = False
            Application.DoEvents()
        End Try
    End Sub

    Private Sub GetJobAndReportData(ByVal jobId As String)
        Dim dsJobData As DataSet = DataAccessorODBC.GetDataSet(connStrSandBoxServer, SQLJobs.GetJob(jobId) & SQLJobs.GetJobReport(jobId))
        If (dsJobData IsNot Nothing) Then
            If (dsJobData.Tables(1).IsValid) Then
                DataMartGridView.SetData(gcSchedulerJobReports, gvSchedulerJobReports, dsJobData.Tables(1), "ALL")
            Else
                DataMartGridView.ClearGrid(gcSchedulerJobReports, gvSchedulerJobReports)
                SetMessage("Sorry ! Job report data not found.")
            End If

            If (dsJobData.Tables(0).IsValid) Then
                GetJobData(dsJobData.Tables(0))
            End If
        End If
    End Sub

    Private Sub GetJobData(ByVal dtJobData As DataTable)
        If (dtJobData IsNot Nothing) Then
            If (dtJobData.IsValid) Then
                lblNextRunTime.Text = dtJobData.Rows(0)(JobFields.JobNextRun).ToString
                vtxt_SchedulerMinutes.Text = dtJobData.Rows(0)(JobFields.JobInterval_Minutes).ToString
                vtxt_SchedulerHours.Text = dtJobData.Rows(0)(JobFields.JobInterval_Hours).ToString
                vtxt_SchedulerDays.Text = dtJobData.Rows(0)(JobFields.JobInterval_Days).ToString
                cb_JobEnabled.Checked = dtJobData.Rows(0)(JobFields.JobActive)
                If (String.IsNullOrEmpty(dtJobData.Rows(0)(JobFields.JobStop_TimeOut_Minutes).ToString.Trim)) Then
                    cb_TimeOutEnabled.Checked = True
                    vtxt_SchedulerTimeOut.Text = "1000"
                Else
                    cb_TimeOutEnabled.Checked = False
                    vtxt_SchedulerTimeOut.Text = dtJobData.Rows(0)(JobFields.JobStop_TimeOut_Minutes).ToString
                End If

                If (String.IsNullOrEmpty(dtJobData.Rows(0)(JobFields.JobStop_End).ToString.Trim)) Then
                    cb_StopJobEnabled.Checked = True
                    vDTP_SchedulerJobEndTime.Enabled = False
                Else
                    cb_StopJobEnabled.Checked = False
                    vDTP_SchedulerJobEndTime.Enabled = True
                    vDTP_SchedulerJobEndTime.EditValue = dtJobData.Rows(0)(JobFields.JobStop_End).ToString
                End If

                SetComboBox(vCmb_SchedulerFileFormat, ComboSelectBased.TextBased, dtJobData.Rows(0)(JobFormatFields.JobFormatOutput))

                If (String.IsNullOrEmpty(dtJobData.Rows(0)(JobFields.JobOutputFileDestination).ToString.Trim)) Then
                    cb_JobDropFileToDropzone.Checked = True
                Else
                    cb_JobDropFileToDropzone.Checked = False
                    SetComboBox(vcmb_JobDropZone, ComboSelectBased.TextBased, dtJobData.Rows(0)(JobFields.JobOutputFileDestination))
                End If

                If (String.IsNullOrEmpty(dtJobData.Rows(0)(JobFields.JobOutputEmailDestination).ToString.Trim)) Then
                    cb_JobEmail.Checked = True
                    vTxt_SchedulerOutputEmail.Text = String.Empty
                Else
                    cb_JobEmail.Checked = False
                    vTxt_SchedulerOutputEmail.Text = dtJobData.Rows(0)(JobFields.JobOutputEmailDestination)
                End If

                'vcbJobThresholdBreach.Checked = dtJobData.Rows(0)(JobFields.JobThresholdOuputOnly)
                vcbJobSNMPAlarm.Checked = dtJobData.Rows(0)(JobFields.JobSNMPAlarm)
                vtxtSNMPAlarmComment.Text = dtJobData.Rows(0)(JobFields.JobSNMPAlarmComment)

                If (String.IsNullOrEmpty(dtJobData.Rows(0)(JobFields.JobSNMPAlarmComment))) Then
                    vtxtSNMPAlarmComment.Text = String.Empty
                Else
                    vtxtSNMPAlarmComment.Text = dtJobData.Rows(0)(JobFields.JobSNMPAlarmComment)
                End If
            End If
        End If
    End Sub

    Private Sub vCmb_SchedulerTechPack_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles vCmb_SchedulerTechPack.SelectedIndexChanged
        Application.UseWaitCursor = True
        Cursor.Current = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If (vCmb_SchedulerTechPack.SelectedIndex > 0) Then
                BindSchedulerReportGroup()
            Else
                ClearComboBox(vCmb_SchedulerReportGroup, "None")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Cursor.Current = Cursors.Default
            Application.UseWaitCursor = False
        End Try
    End Sub

    Private Sub BindSchedulerReportGroup()
        Dim selectReportGroups As String = New SQLReportGroups().SelectAll(ReportGroupsFields.REPORT_GROUP_NAME, True)
        Dim initializeDT As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, New SQLReportGroups().SelectAll(ReportGroupsFields.REPORT_GROUP_NAME))
        If (initializeDT.IsValid) Then
            BindDevExComboBoxWithTagMember(vCmb_SchedulerReportGroup, initializeDT, ReportGroupsFields.REPORT_GROUP_ID, ReportGroupsFields.REPORT_GROUP_NAME, "None", ReportGroupsFields.REPORT_GROUP_PRIVATE, True)
        Else
            ClearComboBox(vCmb_SchedulerReportGroup, "None")
        End If
    End Sub

    Private Sub vCmb_SchedulerReportGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles vCmb_SchedulerReportGroup.SelectedIndexChanged
        If (vCmb_SchedulerReportGroup.SelectedIndex > 0) Then
            RefreshReportTLV("SchedulerReports", vCmb_SchedulerReportGroup)
        Else
            tvSchedulerReports.Nodes.Clear()
        End If
    End Sub

    Private Sub cTxt_SchedulerSearchReport_KeyDown(sender As Object, e As KeyEventArgs)
        ''txtObject_KeyDown(tvSchedulerReports, cTxt_SchedulerSearchReport.Text, e)
    End Sub

    Private Sub cTxt_SchedulerSearchReport_TextChanged(sender As Object, e As EventArgs)
        ''txtObject_TextChanged(tvSchedulerReports, cTxt_SchedulerSearchReport.Text)
    End Sub

    Private Sub cms_JobGroupTLV_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_JobGroupTLV.Opening
        IsByClickJobGroup = True
        If (tvSchedulerJob.Nodes.Count > 0) Then
            tvSchedulerJob.ContextMenuStrip.Show()
        Else
            e.Cancel = True
            tvSchedulerJob.ContextMenuStrip.Hide()
            Return
        End If
        If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
            If (tvSchedulerJob.FocusedNode.Level = 0) Then
                IsByClickJobGroup = False
                Dim isPrivate As Boolean = IIf(ToolTipController1.GetToolTip(tvSchedulerJob).Split("_")(1).ToString.ToUpper = "1", True, False)
                tsmi_JobGroupShareStatus_Private.Checked = isPrivate
                tsmi_JobGroupShareStatus_Public.Checked = IIf(isPrivate, False, True)
                IsByClickJobGroup = True
            End If
        Else
            tvSchedulerJob.ContextMenuStrip.Hide()
            Return
        End If
        Try
            If (Me.JobGroupReportTreeSelectionType = JobSelectionType.JobGroup) Then
                tsmi_JobGroupDelete.Enabled = True
                tsmi_JobGroupModify.Enabled = True
                tsmi_JobGroupRename.Enabled = True
                tsmi_JobGroupShareStatus.Enabled = True
                tsmi_JobGroupShareStatus_Private.Enabled = True
                tsmi_JobGroupShareStatus_Public.Enabled = True
                tsmi_JobDelete.Enabled = False
                tsmi_JobInsert.Enabled = False
                ''tsmi_JobGroupRename.Enabled = False
                tsmi_JobReport_Delete.Enabled = False
                tsmi_JobRename.Enabled = False

            ElseIf (Me.JobGroupReportTreeSelectionType = JobSelectionType.Job) Then
                tsmi_JobGroupDelete.Enabled = False
                tsmi_JobGroupModify.Enabled = False
                tsmi_JobGroupRename.Enabled = False
                tsmi_JobGroupShareStatus.Enabled = False
                tsmi_JobDelete.Enabled = True
                tsmi_JobInsert.Enabled = True
                tsmi_JobRename.Enabled = True
                tsmi_JobReport_Delete.Enabled = False

            ElseIf (Me.JobGroupReportTreeSelectionType = JobSelectionType.JobReport) Then
                tsmi_JobGroupDelete.Enabled = False
                tsmi_JobGroupModify.Enabled = False
                tsmi_JobGroupRename.Enabled = False
                tsmi_JobGroupShareStatus.Enabled = False
                tsmi_JobDelete.Enabled = False
                tsmi_JobInsert.Enabled = False
                tsmi_JobRename.Enabled = False
                tsmi_JobReport_Delete.Enabled = True

            Else
                tsmi_JobGroupDelete.Enabled = False
                tsmi_JobGroupModify.Enabled = False
                tsmi_JobGroupRename.Enabled = False
                tsmi_JobGroupShareStatus.Enabled = False
                tsmi_JobDelete.Enabled = False
                tsmi_JobInsert.Enabled = False
                tsmi_JobRename.Enabled = False
                tsmi_JobReport_Delete.Enabled = False

            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
        End Try
    End Sub

    Private Sub tsmi_JobGroupDelete_Click(sender As Object, e As EventArgs) Handles tsmi_JobGroupDelete.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
                If (tvSchedulerJob.FocusedNode.Level = 0) Then
                    Dim JobGroupID As String = tvSchedulerJob.FocusedNode.Tag
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLJobGroups.DeleteJobGroup(JobGroupID))
                    BindJobGroupCombo()
                    'Dim index = vCmb_JobGroup.Items.ToList().FindIndex(Function(c) c.ToString() = e.Label)
                    'vCmb_JobGroup.SelectedIndex = index

                    ''RefreshJobGroupReportTLV()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_JobGroupRename_Click(sender As Object, e As EventArgs) Handles tsmi_JobGroupRename.Click
        If (tvSchedulerJob.FocusedNode.Level = 0) Then
            tvSchedulerJob.OptionsBehavior.Editable = True
            tvSchedulerJob.OptionsBehavior.ReadOnly = False
        End If
    End Sub

    Private Sub tsmi_JobInsert_Click(sender As Object, e As EventArgs) Handles tsmi_JobInsert.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (cmbJobGroup.SelectedIndex > 0) Then
                Dim jobGroupID As String = "0"
                If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
                    If (tvSchedulerJob.FocusedNode.Level = 0) Then
                        jobGroupID = tvSchedulerJob.FocusedNode.Tag.ToString
                    End If
                    If (tvSchedulerJob.FocusedNode.Level = 1) Then
                        jobGroupID = tvSchedulerJob.FocusedNode.ParentNode.Tag.ToString
                    End If

                    If (vCmb_SchedulerFileFormat.SelectedIndex > 0) Then
                        ''If (vcmb_JobDropZone.SelectedIndex > 0) Then
                        ''jobGroupID = tvSchedulerJob.FocusedNode.Parent.Tag.ToString
                        'Dim endtime As Date = Date.Now().AddDays(vtxt_SchedulerDays.Text.Trim)
                        'endtime = endtime.AddHours(vtxt_SchedulerHours.Text.Trim)
                        'endtime = endtime.AddMinutes(vtxt_SchedulerMinutes.Text.Trim)
                        Dim sqlCommand As String = SQLJobs.InsertJob(jobGroupID, "New Job", "", True, Date.Now(), 0, 0, 1, 1000, "", lblSchedulerNextRunTime.Text.Trim, TryCast(vCmb_SchedulerFileFormat.SelectedItem, clsComboBoxItem).Value, "", "", False, False, "")
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlCommand)
                        SetMessage("A Job Successfully created.")
                        RefreshJobGroupReportTLV()
                        'Else
                        '    SetMessage("Select Drop File to Zone")
                        'End If
                    Else
                        SetMessage("Please Select File Format.")
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_JobRename_Click(sender As Object, e As EventArgs) Handles tsmi_JobRename.Click
        Try
            If (tvSchedulerJob.FocusedNode.Level = 1) Then
                tvSchedulerJob.OptionsBehavior.Editable = True
                tvSchedulerJob.OptionsBehavior.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tsmi_JobDelete_Click(sender As Object, e As EventArgs) Handles tsmi_JobDelete.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
                If (tvSchedulerJob.FocusedNode.Level = 1) Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLJobs.JobDelete(tvSchedulerJob.FocusedNode.Tag))
                    SetMessage("Job Successfully Deleted.")
                    RefreshJobGroupReportTLV()
                Else
                    SetMessage("Please select any group or job from tree")
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_JobReport_Delete_Click(sender As Object, e As EventArgs) Handles tsmi_JobReport_Delete.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
                If (tvSchedulerJob.FocusedNode.Level = 2) Then
                    Dim jobID As String = tvSchedulerJob.FocusedNode.ParentNode.Tag.ToString
                    Dim reportID As String = tvSchedulerJob.FocusedNode.Tag.ToString
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLJobs.JobReportsDelete(jobID, reportID))
                    ''tvSchedulerJob.Nodes.Clear()
                    RefreshJobGroupReportTLV()
                    SetMessage("Job Report Successfully Deleted.")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_JobGroup_Expand_Click(sender As Object, e As EventArgs) Handles tsmi_JobGroup_Expand.Click
        tvSchedulerJob.ExpandAll()
    End Sub

    Private Sub tsmi_JobGroupCollapse_Click(sender As Object, e As EventArgs) Handles tsmi_JobGroupCollapse.Click
        If (tvSchedulerJob.Nodes.Count > 0 AndAlso tvSchedulerJob.Nodes(0).Nodes.Count > 0) Then
            tvSchedulerJob.CollapseAll()
        End If
    End Sub

    Private Sub tsmi_JobGroupShareStatus_Public_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_JobGroupShareStatus_Public.CheckedChanged
        Try
            If (IsByClickJobGroup) Then
                If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
                    If (tvSchedulerJob.FocusedNode.Level = 0) Then
                        Dim jobGroupId As String = tvSchedulerJob.FocusedNode.Tag
                        Dim jobGroupName As String = tvSchedulerJob.FocusedNode.GetDisplayText("JobGroupName")
                        Dim isPrivate As Boolean = IIf(tsmi_JobGroupShareStatus_Public.Checked, False, True)
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLJobGroups.ModifyJobGroup(jobGroupId, jobGroupName, isPrivate, System.Environment.UserName))
                        IsByClickJobGroup = False
                        If (tsmi_JobGroupShareStatus_Public.Checked) Then
                            tsmi_JobGroupShareStatus_Private.Checked = False
                        Else
                            tsmi_JobGroupShareStatus_Private.Checked = True
                            tsmi_JobGroupShareStatus_Public.Checked = False
                        End If
                        IsByClickJobGroup = True
                        RefreshJobGroupReportTLV()
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tsmi_JobGroupShareStatus_Private_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_JobGroupShareStatus_Private.CheckedChanged
        Try
            If (IsByClickJobGroup) Then
                If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
                    If (tvSchedulerJob.FocusedNode.Level = 0) Then
                        Dim jobGroupId As String = tvSchedulerJob.FocusedNode.Tag
                        Dim jobGroupName As String = tvSchedulerJob.FocusedNode.GetDisplayText("JobGroupName")
                        Dim isPrivate As Boolean = IIf(tsmi_JobGroupShareStatus_Private.Checked, True, False)
                        DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLJobGroups.ModifyJobGroup(jobGroupId, jobGroupName, isPrivate, System.Environment.UserName))
                        IsByClickJobGroup = False
                        If (tsmi_JobGroupShareStatus_Private.Checked) Then
                            tsmi_JobGroupShareStatus_Public.Checked = False
                        Else
                            tsmi_JobGroupShareStatus_Private.Checked = False
                            tsmi_JobGroupShareStatus_Public.Checked = True
                        End If
                        IsByClickJobGroup = True
                        RefreshJobGroupReportTLV()
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btn_JobHistory_Click(sender As Object, e As EventArgs) Handles btn_JobHistory.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (tvSchedulerJob.FocusedNode IsNot Nothing) Then
                If (tvSchedulerJob.FocusedNode.Level = 1) Then
                    Dim dtJobHistory As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLJobHistory.GetJobHistoryByJobId(tvSchedulerJob.FocusedNode.Tag.ToString))
                    If (dtJobHistory.IsValid) Then
                        DataMartGridView.SetData(gcSchedulerHistory, gvSchedulerHistory, dtJobHistory, "ALL")
                    Else
                        DataMartGridView.ClearGrid(gcSchedulerHistory, gvSchedulerHistory)
                        SetMessage("Sorry no job history to selected job.")
                    End If
                Else
                    DataMartGridView.ClearGrid(gcSchedulerHistory, gvSchedulerHistory)
                    SetMessage("Please select job.")
                End If
            Else
                DataMartGridView.ClearGrid(gcSchedulerHistory, gvSchedulerHistory)
                SetMessage("Please select job.")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vtxt_SchedulerMinutes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles vtxt_SchedulerMinutes.KeyPress
        e.Handled = SandBoxTextBox.IsNumberVal(e.KeyChar)

        If (vtxt_SchedulerMinutes.Text.Length = 0) Then
            vtxt_SchedulerMinutes.Text = 0
        End If
        SetNextRunTime()
    End Sub

    Private Sub vtxt_SchedulerHours_KeyPress(sender As Object, e As KeyPressEventArgs) Handles vtxt_SchedulerHours.KeyPress
        e.Handled = SandBoxTextBox.IsNumberVal(e.KeyChar)
        If (vtxt_SchedulerHours.Text.Length = 0) Then
            vtxt_SchedulerHours.Text = 0
        End If
        SetNextRunTime()
    End Sub

    Private Sub vtxt_SchedulerDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles vtxt_SchedulerDays.KeyPress
        e.Handled = SandBoxTextBox.IsNumberVal(e.KeyChar)
        If (vtxt_SchedulerDays.Text.Length = 0) Then
            vtxt_SchedulerDays.Text = 0
        End If
        SetNextRunTime()
    End Sub

    Private Sub vtxt_SchedulerTimeOut_KeyPress(sender As Object, e As KeyPressEventArgs) Handles vtxt_SchedulerTimeOut.KeyPress
        e.Handled = SandBoxTextBox.IsNumberVal(e.KeyChar)
        If (vtxt_SchedulerTimeOut.Text.Length = 0) Then
            vtxt_SchedulerTimeOut.Text = 1000
        End If
        SetNextRunTime()
    End Sub

    Private Sub tvSchedulerJob_MouseMove(sender As Object, e As MouseEventArgs) Handles tvSchedulerJob.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim node As Nodes.TreeListNode = tvSchedulerJob.FocusedNode
                Dim data As TreeListNode = tvSchedulerJob.GetNodeAt(e.Location)
                If data IsNot Nothing Then
                    'Dim obj() As Object = {data.Item(0), data.Item(1), data.Item(2), data.Item(3)}
                    tvSchedulerJob.DoDragDrop(data, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tvSchedulerJob_DragDrop(sender As Object, e As DragEventArgs) Handles tvSchedulerJob.DragDrop
        Try
            Dim targetNode As TreeListNode = Nothing
            Dim pt As Point = tvSchedulerJob.PointToClient(New Point(e.X, e.Y))
            targetNode = tvSchedulerJob.CalcHitInfo(pt).Node

            Dim data() As Object = e.Data.GetData("System.Object[]")
            If (targetNode.Level = 1) Then
                Dim jobID As String = targetNode.Tag
                Dim reportDragDropID As String = data(0)
                Try
                    Dim result As Integer = DataAccessorODBC.ExecuteScalar(connStrSandBoxServer, SQLJobs.JobReportsInsert(jobID, reportDragDropID))
                    If (result = 1) Then
                        RefreshJobGroupReportTLV()
                        SetMessage("Report Successfully dragged.")
                    Else
                        SetMessage("Fail : Report already exist.")
                    End If
                    vCmb_SchedulerReportGroup_SelectedIndexChanged(Nothing, Nothing)
                Catch ex As Exception
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
                End Try
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub tvSchedulerJob_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub vBtn_JobCommit_Click(sender As Object, e As EventArgs) Handles vBtn_JobCommit.Click
        Try
            If (vCmb_SchedulerFileFormat.SelectedIndex > 0 AndAlso tvSchedulerJob.FocusedNode.Level = 1) Then
                If (cmbJobGroup.SelectedIndex > 0) Then
                    '' Dim jobGroupID As String = vCmb_JobGroup.SelectedItem.Value & "," & tvSchedulerJob.FocusedNode.Text.ToString
                    'If (vcmb_JobDropZone.SelectedIndex > 0) Then

                    'End If

                    Dim commandParm As String = tvSchedulerJob.FocusedNode.Tag & ",'NULL'"
                    Dim jobStopTimeOut As String = String.Empty
                    Dim jobStopEnd As String = String.Empty
                    If (Not cb_TimeOutEnabled.Checked) Then
                        jobStopTimeOut = vtxt_SchedulerTimeOut.Text
                    Else
                        ' jobStopTimeOut = "NULL"
                    End If
                    If (Not cb_StopJobEnabled.Checked) Then
                        jobStopEnd = vDTP_SchedulerJobEndTime.EditValue
                    Else
                        'jobStopEnd = "NULL"
                    End If


                    '' SetComboBox(vCmb_SchedulerFileFormat, ComboSelectBased.TextBased, dtJobData.Rows(0)(JobFormatFields.JobFormatOutput))
                    Dim jobOutputFileDestination As String = String.Empty
                    If (Not cb_JobDropFileToDropzone.Checked) Then
                        jobOutputFileDestination = vcmb_JobDropZone.SelectedItem.ToString
                    End If

                    Dim jobOutputEmailDestination As String = String.Empty
                    If (Not cb_JobEmail.Checked) Then
                        jobOutputEmailDestination = vTxt_SchedulerOutputEmail.Text
                    End If


                    Dim endtime As Date = Date.Now().AddDays(vtxt_SchedulerDays.Text.Trim)
                    endtime = endtime.AddHours(vtxt_SchedulerHours.Text.Trim)
                    endtime = endtime.AddMinutes(vtxt_SchedulerMinutes.Text.Trim)
                    Dim sqlCommand As String = SQLJobs.UpdateJob(tvSchedulerJob.FocusedNode.Tag, "", cb_JobEnabled.Checked, vDTP_SchedulerTriggerStartTime.EditValue, vtxt_SchedulerHours.Text.Trim, vtxt_SchedulerMinutes.Text.Trim, vtxt_SchedulerDays.Text.Trim, jobStopTimeOut, jobStopEnd, lblNextRunTime.Text.Trim, TryCast(vCmb_SchedulerFileFormat.SelectedItem, clsComboBoxItem).Value, jobOutputFileDestination, jobOutputEmailDestination, False, vcbJobSNMPAlarm.Checked, vtxtSNMPAlarmComment.Text)

                    ''Dim sqlCommand As String = SQLJobs.UpdateJob(tvSchedulerJob.FocusedNode.Tag, "'NULL'", ", True, Date.Now(), vtxt_SchedulerHours.Text.Trim, vtxt_SchedulerMinutes.Text.Trim, vtxt_SchedulerDays.Text.Trim, vtxt_SchedulerTimeOut.Text.Trim, "", endtime, vCmb_SchedulerFileFormat.SelectedItem.Value, "", "")
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlCommand)
                    SetMessage("A job Successfully committed .")
                    RefreshJobGroupReportTLV()
                Else
                    SetMessage("Select Drop File to Zone")
                End If
            Else
                SetMessage("Select File Format")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub vbtn_CreateKPI_Click(sender As Object, e As EventArgs)
        Dim frmCreateKPI As New frmSBKPIManage()
        frmCreateKPI.ShowDialog()
    End Sub

    Private Sub cb_JobEmail_CheckedChanged(sender As Object, e As EventArgs) Handles cb_JobEmail.CheckedChanged
        If (cb_JobEmail.Checked) Then
            vTxt_SchedulerOutputEmail.Text = String.Empty
            vTxt_SchedulerOutputEmail.Enabled = False
        Else
            vTxt_SchedulerOutputEmail.Enabled = True
        End If
    End Sub

    Private Sub cb_JobDropFileToDropzone_CheckedChanged(sender As Object, e As EventArgs) Handles cb_JobDropFileToDropzone.CheckedChanged
        If (cb_JobDropFileToDropzone.Checked) Then
            vcmb_JobDropZone.SelectedIndex = 0
            vcmb_JobDropZone.Enabled = False
        Else
            vcmb_JobDropZone.Enabled = True
        End If
    End Sub

    Private Sub cb_StopJobEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles cb_StopJobEnabled.CheckedChanged
        If (cb_StopJobEnabled.Checked) Then
            ''vDTP_SchedulerJobEndTime.Text = String.Empty
            vDTP_SchedulerJobEndTime.Enabled = False
        Else
            vDTP_SchedulerJobEndTime.Enabled = True
        End If
    End Sub

    Private Sub cb_TimeOutEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles cb_TimeOutEnabled.CheckedChanged
        If (cb_TimeOutEnabled.Checked) Then
            ''vtxt_SchedulerTimeOut.Text = String.Empty
            vtxt_SchedulerTimeOut.Enabled = False
        Else
            vtxt_SchedulerTimeOut.Enabled = True
        End If
    End Sub

#End Region

#Region "Context Menu - KPI Manage"

    Private Sub tsmi_KPIAdd_Click(sender As Object, e As EventArgs)
        Try
            'xtcSandboxChartConfigure.SelectedTabPage() = xtpSandboxKPIConfig
            objDMKpiConfig = New frmDatamartKpiConfig()
            objDMKpiConfig.txtKPIName.Text = String.Empty
            objDMKpiConfig.txtKPIDescription.Text = String.Empty
            objDMKpiConfig.txtKPIFormula.Text = String.Empty
            'vlbl_ObType.Text = String.Empty
            'vlbl_ObType.Text = vcmb_ObjectType.SelectedItem.Text
            objDMKpiConfig.isModifyKpiRequest = False

            If (cmbObjectType.SelectedIndex > 0) Then
                objDMKpiConfig.lblKPIConfigObjectType.Text = cmbObjectType.Properties.Items(1).ToString
                objDMKpiConfig.btnCommitKPI.Enabled = True
                objDMKpiConfig.Show()
            Else
                SetMessage("Please select Object Type.")
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub GetKPIFormulaAndDescription(ByVal kpiId As String)
    '    Try
    '        '     If (cmbObjectType.SelectedIndex > 0) Then
    '        txtKPIFormula.Text = String.Empty
    '        txtKPIDescription.Text = String.Empty
    '        ' Dim counterFilters As String = TechnologyPackageCountersFields.SOURCE_OBJECT_ID & OperatorConst.Equal & TryCast(cmbObjectType.SelectedItem, clsComboBoxItem).Value & AggregateConst.AND_Only & TechnologyPackageKPIFields.KPI_ID & OperatorConst.Equal & kpiId
    '        Dim counterFilters As String = TechnologyPackageKPIFields.KPI_ID & OperatorConst.Equal & kpiId

    '        Dim dtTechnologyPackageKPI As DataTable = dt_TechnologyPackageKPI.SelectedRowsAsTable(counterFilters)
    '        If (dtTechnologyPackageKPI.IsValid) Then
    '            modifyKPIID = dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_ID)
    '            txtKPIFormula.Text = dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_SQL)
    '            txtKPIDescription.Text = dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_DESCRIPTION)
    '            lblKPICreator.Text = IIf(IsDBNull(dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_CREATOR)), "", dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_CREATOR))
    '            If (dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.IS_PRIVATE) = True) Then
    '                rbKPIConfigPrivate.Checked = True
    '                rbKPIConfigPublic.Checked = False
    '            Else
    '                rbKPIConfigPrivate.Checked = False
    '                rbKPIConfigPublic.Checked = True
    '            End If
    '            RefreshUsedTableInKPI()
    '        Else
    '            ''SetMessage("Sorry ! No KPI Formula and Description.")
    '        End If
    '        '    End If
    '    Catch ex As Exception
    '        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
    '    End Try
    'End Sub

    'Private Sub RefreshUsedTableInKPI()
    '    list_of_used_tables.Clear()
    '    If (txtKPIFormula.Text.Trim().Length >= 0) Then
    '        Dim usingTables As String() = txtKPIFormula.Text.Split(".")
    '        If (usingTables.Count > 1) Then
    '            For Each sTable As String In usingTables
    '                If (sTable(sTable.Length - 1) = "]") Then
    '                    Dim startIndex As Integer = sTable.LastIndexOf("[")
    '                    Dim endIndex As Integer = sTable.LastIndexOf("]")
    '                    Dim strTable As String = sTable.Substring(startIndex, endIndex - startIndex + 1)
    '                    If Not list_of_used_tables.Contains(strTable) Then
    '                        list_of_used_tables.Add(strTable)
    '                    End If
    '                End If
    '            Next
    '        End If
    '    End If
    'End Sub

    Private Sub tsmi_DeleteCategory_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstTechKPI.FocusedNode.Level = 0) Then
                If lstTechKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiCategory.DeleteCategory(lstTechKPI.FocusedNode.Tag, TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Value))
                    lstTechKPI.Nodes.Remove(lstTechKPI.FocusedNode)
                    lstTechKPI.Refresh()
                End If
            End If
        Catch ex As Exception
            SetMessage("Delete KPI Category : Found Error.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_RenameCategory_Click(sender As Object, e As EventArgs) Handles tsmi_RenameCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstTechKPI.FocusedNode.Level = 0) Then
                If lstTechKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME Then
                    lstTechKPI.OptionsBehavior.Editable = True
                    lstTechKPI.OptionsBehavior.ReadOnly = False
                End If
            End If
        Catch ex As Exception
            SetMessage("Rename Category : Found Error.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_RenameKPIGroup_Click(sender As Object, e As EventArgs) Handles tsmi_RenameKPIGroup.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim frmGroupInsert As New dlgSBGroupInsert()
            frmGroupInsert.SetConnectionString(connStrSandBoxServer)
            frmGroupInsert.GroupTypeInserting = GroupType.KpiGroup
            frmGroupInsert.KPIGroupID = TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Value
            frmGroupInsert.ShowDialog()
            Dim newGroupName As String = frmGroupInsert.NewGroup
            Dim RetrunData As Boolean = frmGroupInsert.IsGroupPrivate
            If (newGroupName IsNot Nothing) Then
                If (newGroupName IsNot Nothing) Then
                    BindKPIGroup()
                    Dim cmbItem As clsComboBoxItem = GetComboItemFromText(newGroupName, cmbKPIGroup)
                    cmbKPIGroup.SelectedItem = cmbItem
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub tsmi_DeleteKPIGroup_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteKPIGroup.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstTechKPI.FocusedNode.Level = 1) Then
                If lstTechKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_GROUP_ID Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiGroup.DeleteKPIGroup(TryCast(cmbKPIGroup.SelectedItem, clsComboBoxItem).Value))
                    lstTechKPI.Nodes.Remove(lstTechKPI.FocusedNode)
                    lstTechKPI.Refresh()
                    SetMessage("KPI Group Successfully Deleted.")
                End If
            End If
        Catch ex As Exception
            SetMessage("Delete KPI Group : Found Error.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ViewKPI_Click(sender As Object, e As EventArgs) Handles tsmi_ViewKPI.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If Not Application.OpenForms().OfType(Of frmDatamartKpiConfig).Any Then
                objDMKpiConfig = New frmDatamartKpiConfig()
                objDMKpiConfig.kpiNameToModify = lstTechKPI.FocusedNode.GetDisplayText(KPIGroupFields.KPI_CATEGORY_NAME).ToString
                objDMKpiConfig.kpiConfigObjectType = cmbObjectType.Properties.Items(1).ToString
                objDMKpiConfig.txtKPIName.Text = lstTechKPI.FocusedNode.GetDisplayText(KPIGroupFields.KPI_CATEGORY_NAME).ToString
                objDMKpiConfig.lblKPIConfigObjectType.Text = cmbObjectType.Properties.Items(1).ToString
                objDMKpiConfig.isModifyKpiRequest = False
                objDMKpiConfig.GetKPIFormulaAndDescription(lstTechKPI.FocusedNode.Tag)
                objDMKpiConfig.Show()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ModifyKPI_Click(sender As Object, e As EventArgs) Handles tsmi_ModifyKPI.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (cmbObjectType.SelectedIndex > 0) Then
                If Not Application.OpenForms().OfType(Of frmDatamartKpiConfig).Any Then
                    objDMKpiConfig = New frmDatamartKpiConfig()
                    objDMKpiConfig.kpiNameToModify = lstTechKPI.FocusedNode.GetDisplayText(KPIGroupFields.KPI_CATEGORY_NAME).ToString
                    objDMKpiConfig.kpiConfigObjectType = cmbObjectType.Properties.Items(1).ToString
                    objDMKpiConfig.txtKPIName.Text = lstTechKPI.FocusedNode.GetDisplayText(KPIGroupFields.KPI_CATEGORY_NAME).ToString
                    objDMKpiConfig.lblKPIConfigObjectType.Text = cmbObjectType.Properties.Items(1).ToString
                    objDMKpiConfig.isModifyKpiRequest = True
                    objDMKpiConfig.GetKPIFormulaAndDescription(lstTechKPI.FocusedNode.Tag)
                    objDMKpiConfig.Show()
                End If
            Else
                SetMessage("Please Select Object Type.")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_KPIRemoveCategory_Click(sender As Object, e As EventArgs) Handles tsmi_KPIRemoveCategory.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstTechKPI.FocusedNode.Level = 1) Then
                If lstTechKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiGroup.RemoveKPIFromCategory(lstTechKPI.FocusedNode.ParentNode.Tag, lstTechKPI.FocusedNode.Tag))
                    lstTechKPI.Nodes.Remove(lstTechKPI.FocusedNode)
                    lstTechKPI.Refresh()
                    SetMessage("KPI Successfully Removed From Category.")
                End If
            End If
        Catch ex As Exception
            SetMessage("KPI Remove From Category : Found Error.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_KPIDeleteDatabase_Click(sender As Object, e As EventArgs) Handles tsmi_KPIDeleteDatabase.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (lstTechKPI.FocusedNode.Level = 1) Then
                If lstTechKPI.FocusedColumn.FieldName = KPIGroupFields.KPI_CATEGORY_NAME Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLKpiGroup.DeleteKPIFromDB(lstTechKPI.FocusedNode.Tag))
                    RefreshKPITree()
                    SetMessage("KPI Successfully Deleted From Database.")
                End If
            End If
        Catch ex As Exception
            SetMessage("KPI Delete From Database: Found Error.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Public Sub RefreshKPITree()
        Dim selectTechnologyPackageKPI As String = SQLTechnologyKPIs.GetByTechAndCreator(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, cmbKPIGroup.SelectedItem.ToString)
        dt_TechnologyPackageKPI = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectTechnologyPackageKPI)
        If cmbObjectSource.SelectedIndex = 0 Then
            BindKPITree(dt_TechnologyPackageKPI)
        Else
            Dim filterSourceObject = dt_TechnologyPackageKPI.AsEnumerable().Where(Function(n) n.Field(Of Integer)("SourceObjectID") = CInt(CType(cmbObjectSource.SelectedItem, clsComboBoxItem).Value))
            If filterSourceObject.Any() Then
                Dim dtSourceFilter As DataTable = filterSourceObject.CopyToDataTable
                BindKPITree(dtSourceFilter)
            End If
        End If
    End Sub

    Private Sub tsmi_DashboardReportHideAndShowTitle_Click(sender As Object, e As EventArgs) Handles tsmi_DashboardReportHideAndShowTitle.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tempSplitCont As SplitContainer = GetSplitControl(cm_SourceControl)
            If (tempSplitCont IsNot Nothing) Then
                Dim ch As Chart = TryCast(tempSplitCont.Panel1.Controls(0), Chart)
                If (ch IsNot Nothing) Then
                    If ch.Title = "   " Then
                        If (Len(dtChartConfigSandbox(0)("ObjectNamesInReport").ToString) > 150) Then
                            ch.Title = dtChartConfigSandbox(0)("ObjectNamesInReport").ToString.Substring(0, 150) + IIf(Len(dtChartConfigSandbox(0)("ObjectNamesInReport").ToString) > 150, "...", "")
                        Else
                            ch.Title = dtChartConfigSandbox(0)("ObjectNamesInReport").ToString
                        End If
                    Else
                        ch.Title = "   "
                    End If
                End If
                ch.RefreshChart()
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub ceAlignIntervalAll_CheckedChanged(sender As Object, e As EventArgs) Handles ceAlignIntervalAll.CheckedChanged
        If ceAlignIntervalAll.CheckState = CheckState.Checked Then
            'ceAlignIntervalMatch.Checked = False
            txtSandBoxTopX.Enabled = False
        Else
            txtSandBoxTopX.Enabled = True
        End If
    End Sub

    Private Sub ceAlignIntervalMatch_CheckedChanged(sender As Object, e As EventArgs)
        'If ceAlignIntervalMatch.CheckState = CheckState.Checked Then
        '    ceAlignIntervalAll.Checked = False
        '    txtSandBoxTopX.Enabled = False
        'Else
        '    txtSandBoxTopX.Enabled = True
        'End If
    End Sub

    Private Function GetKPITreeData() As DataTable
        Dim selectTechnologyPackageKPI As String = SQLTechnologyKPIs.GetByTechAndCreator(TryCast(cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, cmbKPIGroup.SelectedItem.ToString)
        Return DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectTechnologyPackageKPI)
    End Function

    Private Sub tsmi_ViewCheckedKPIs_Click(sender As Object, e As EventArgs) Handles tsmi_ViewCheckedKPIs.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lstTechKPI.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dt As DataTable = GetKPITreeData()
            Dim tvKPIFilterString As String = ""
            viewCheckedKPIsOnly = True

            If tsmi_ViewCheckedKPIs.Text.Trim.ToLower = "view checked" Then
                If checkedKPINameList.Count <> 0 Then
                    For Each str As String In checkedKPINameList
                        If Not viewCheckedKPINameList.Contains(str) Then
                            viewCheckedKPINameList.Add(str)
                        End If
                        tvKPIFilterString = tvKPIFilterString & "'" & str & "',"
                    Next

                    tvKPIFilterString = tvKPIFilterString.TrimEnd(",")
                    Dim dtFilter As DataTable = dt.Select("KPIName In(" & tvKPIFilterString & ")").CopyToDataTable()
                    BindKPITree(dtFilter)

                    Dim strClear As String = tvKPIFilterString.Replace("'", "")
                    Dim ndText() As String = strClear.Split(",")

                    For Each str As String In ndText
                        Dim nd = lstTechKPI.FindNodeByFieldValue(KPIGroupFields.KPI_CATEGORY_NAME, str)
                        nd.SetValue("riChkEdit", True)
                    Next
                    tsmi_ViewCheckedKPIs.Text = "View All"
                End If
            Else
                BindKPITree(dt)
                For Each str As String In viewCheckedKPINameList
                    Dim nd = lstTechKPI.FindNodeByFieldValue(KPIGroupFields.KPI_CATEGORY_NAME, str)
                    If nd IsNot Nothing Then
                        nd.SetValue("riChkEdit", True)
                    End If
                Next
                tsmi_ViewCheckedKPIs.Text = "View Checked"
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            lstTechKPI.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class

Public Class TaskQueue
    Private ReadOnly taskQueue As New ConcurrentQueue(Of Func(Of Task))()
    Private isProcessing As Boolean = False
    Private ReadOnly lockObj As New Object()

    ' Enqueue a task
    Public Sub Enqueue(task As Func(Of Task))
        taskQueue.Enqueue(task)
        ProcessQueue()
    End Sub

    ' Process the queue asynchronously
    Private Sub ProcessQueue()
        SyncLock lockObj
            If isProcessing Then Return
            isProcessing = True
        End SyncLock

        Task.Run(Function()
                     Dim dequeuedTask As Func(Of Task) = Nothing
                     While taskQueue.TryDequeue(dequeuedTask)
                         Try
                             dequeuedTask.Invoke()
                         Catch ex As Exception
                             ' Handle task exceptions
                             Console.WriteLine($"Task error: {ex.Message}")
                         End Try
                     End While

                     ' Mark processing as complete
                     SyncLock lockObj
                         isProcessing = False
                     End SyncLock
                 End Function)
    End Sub
End Class