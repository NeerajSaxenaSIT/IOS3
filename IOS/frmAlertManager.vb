Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports dotnetCHARTING.WinForms
Imports IOS.DataLibrary
Imports IOS.Library

Public Class frmAlertManager

#Region "Variables"

    Private dtKPI As DataTable = Nothing
    Private dtAlertConfig As DataTable = Nothing
    Private dtChartData As New DataTable
    Private objKpiRuleProp As New KPIRuleProperties()
    Private dtObj As DataTable = Nothing
    Private seriesColl() As String = Nothing

    Private ExtraLegendEntryCollection As New Dictionary(Of String, LegendEntry)
    Private DefaultSeriesCollection As New Dictionary(Of String, Series)
    Private dtKpiRuleTypeFields As DataTable = Nothing

    Public copyFromSrcAlertRuleID As Integer = 0
    Public copyFromSrcKpiRuleID As Integer = 0
    Public copyFilterStringsFromKpiRule As Boolean = False

#End Region

#Region "Form Load Event"

    Private Sub frmAlertManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblAlertOwner.Text = Environment.UserName
            Me.BringToFront()

            LoadAlertList()
            SetChartProperties()
            AttachAutoCompleteWithTextBox(txtObjectNameFilter)
            LoadEventReport()

            ConfigurAlertManagerForm(Me.Name)
            deAlertProcessDate.EditValue = Now.AddDays(-1)
            deTestKPIRule.EditValue = Now.AddDays(-1)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Public Sub ConfigurAlertManagerForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim ctrl As IOS.Configuration.EntityModel.Control = Nothing
            'Context Menu
            ctrl = form.FindControlByName(DeleteKPIRuleToolStripMenuItem.Name)
            If Not ctrl Is Nothing Then
                DeleteKPIRuleToolStripMenuItem.Enabled = ctrl.DefaultEnable
                DeleteKPIRuleToolStripMenuItem.Visible = ctrl.DefaultVisible
            End If

        End If
    End Sub

    Private Sub frmAlertManager_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            sccKpiRules.SplitterPosition = Math.Abs(sccKpiRules.Width / 3) * 3
            sccKpiRules.Panel2.Width = Math.Abs(sccKpiRules.Width / 3)
        Catch
        End Try
    End Sub

#End Region

#Region "Private Methods"

    Private Sub SetChartProperties()
        'Chart Default Properties
        chAlert.DefaultElement.Marker.Visible = False
        chAlert.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        chAlert.LegendBox.DefaultEntry.Value = ""
        chAlert.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        chAlert.LegendBox.Visible = True

        chAlert.XAxis.TickLabelMode = TickLabelMode.Angled
        chAlert.XAxis.TickLabelAngle = 45
        chAlert.XAxis.Minimum = 0
        chAlert.XAxis.Maximum = 0

        chAlert.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        chAlert.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

        chAlert.ToolTip.InitialDelay = 1
        chAlert.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        chAlert.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        chAlert.CleanupPeriod = 1
    End Sub

    Private Sub LoadAlertList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3811, parray)(0)
        sqlParam = GetSQL(3811, parray)(1)

        dtAlertName = New DataTable()
        dtAlertName = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        lstviewAlerts.Columns.Clear()
        lstviewAlerts.DataSource = dtAlertName
        lstviewAlerts.Columns(0).Width = 80
        lstviewAlerts.Columns(0).Caption = "ID"
        lstviewAlerts.Columns(0).OptionsColumn.AllowEdit = False
        lstviewAlerts.Columns(2).Visible = False
        lstviewAlerts.AutoFillColumn = lstviewAlerts.Columns(1)
        lstviewAlerts.Refresh()
    End Sub

    Private Sub LoadAlertDataByID(alertRuleId As Integer)
        Dim dtAlertDetails As New DataTable
        Dim parray()() As String = {
            New String() {"@alertRuleID", alertRuleId}
        }

        dtAlertDetails = IOS.DataLibrary.DataAccessorODBC.GetDataTable(GetSQL(3839, parray)(0), GetSQL(3839, parray)(1))

        RemoveHandler seAlertOccurence.EditValueChanged, AddressOf seAlertOccurence_EditValueChanged
        RemoveHandler seAlertWindow.EditValueChanged, AddressOf seAlertWindow_EditValueChanged
        RemoveHandler ceEventEmail.CheckedChanged, AddressOf ceEventEmail_CheckedChanged
        RemoveHandler ceEventSNMP.CheckedChanged, AddressOf ceEventSNMP_CheckedChanged
        RemoveHandler ceAlertEnabled.CheckedChanged, AddressOf ceAlertEnabled_CheckedChanged
        RemoveHandler ceDashboardScore.CheckedChanged, AddressOf ceDashboardScore_CheckedChanged
        RemoveHandler ceEventReport.CheckedChanged, AddressOf ceEventReport_CheckedChanged

        If dtAlertDetails.Rows.Count > 0 Then
            Dim dr As DataRow = Nothing
            dr = dtAlertDetails.Rows(0)
            seAlertOccurence.EditValue = dr.Item("AlertOccurences")
            seAlertWindow.EditValue = dr.Item("AlertSlidingWindowDays")
            ceEventEmail.Checked = IIf(dr.Item("AlertTriggerEmail") = True, True, False)

            If IsDBNull(dr.Item("AlertTriggerSNMP")) Then
                ceEventSNMP.Checked = False
            Else
                ceEventSNMP.Checked = IIf(dr.Item("AlertTriggerSNMP") = True, True, False)
            End If

            If IsDBNull(dr.Item("AlertEnabled")) Then
                ceAlertEnabled.Checked = False
            Else
                ceAlertEnabled.Checked = IIf(dr.Item("AlertEnabled") = True, True, False)
            End If

            If IsDBNull(dr.Item("DashboardScoreIsActive")) Then
                ceDashboardScore.Checked = False
            Else
                ceDashboardScore.Checked = IIf(dr.Item("DashboardScoreIsActive") = True, True, False)
            End If

            If IsDBNull(dr.Item("DashboardScoreValue")) Then
                txtDashboardScore.Text = ""
            Else
                txtDashboardScore.Text = dr.Item("DashboardScoreValue").ToString
            End If

            txtEventEmail.Text = dr.Item("AlertEmailAddresses").ToString
            If IsDBNull(dr.Item("AlertSNMPDescription")) Then
                txtEventSNMP.Text = ""
            Else
                txtEventSNMP.Text = dr.Item("AlertSNMPDescription").ToString
            End If

            RemoveHandler cmbKPIFailureColumn.SelectedIndexChanged, AddressOf cmbKPIFailureColumn_SelectedIndexChanged
            If IsDBNull(dr.Item("FailuresColumn_KPIRULEID")) Then
                cmbKPIFailureColumn.SelectedIndex = 0
            Else
                SetComboBox(cmbKPIFailureColumn, ComboSelectBased.ValueBased, CInt(dr.Item("FailuresColumn_KPIRULEID")))
            End If
            AddHandler cmbKPIFailureColumn.SelectedIndexChanged, AddressOf cmbKPIFailureColumn_SelectedIndexChanged

            If IsDBNull(dr.Item("AlertOwner")) Then
                lblAlertOwner.Text = ""
            Else
                lblAlertOwner.Text = dr.Item("AlertOwner").ToString
            End If

            If IsDBNull(dr.Item("AlertEventReportEnabled")) Then
                ceEventReport.Checked = False
            Else
                ceEventReport.Checked = IIf(dr.Item("AlertEventReportEnabled") = True, True, False)
            End If

            RemoveHandler cmbEventReport.SelectedIndexChanged, AddressOf cmbEventReport_SelectedIndexChanged
            If IsDBNull(dr.Item("AlertEventReportID")) Then
                cmbEventReport.SelectedIndex = 0
            Else
                SetComboBox(cmbEventReport, ComboSelectBased.ValueBased, CInt(dr.Item("AlertEventReportID")))
            End If
            AddHandler cmbEventReport.SelectedIndexChanged, AddressOf cmbEventReport_SelectedIndexChanged
        Else
            seAlertOccurence.EditValue = 1
            seAlertWindow.EditValue = 1
            ceEventEmail.Checked = False
            ceEventSNMP.Checked = False
            ceAlertEnabled.Checked = False
            ceDashboardScore.Checked = False
            txtDashboardScore.Text = String.Empty
            txtEventEmail.Text = String.Empty
            txtEventSNMP.Text = String.Empty
            lblAlertOwner.Text = String.Empty
        End If

        AddHandler seAlertOccurence.EditValueChanged, AddressOf seAlertOccurence_EditValueChanged
        AddHandler seAlertWindow.EditValueChanged, AddressOf seAlertWindow_EditValueChanged
        AddHandler ceEventEmail.CheckedChanged, AddressOf ceEventEmail_CheckedChanged
        AddHandler ceEventSNMP.CheckedChanged, AddressOf ceEventSNMP_CheckedChanged
        AddHandler ceAlertEnabled.CheckedChanged, AddressOf ceAlertEnabled_CheckedChanged
        AddHandler ceDashboardScore.CheckedChanged, AddressOf ceDashboardScore_CheckedChanged
        AddHandler ceEventReport.CheckedChanged, AddressOf ceEventReport_CheckedChanged
    End Sub

    Private Sub DeleteAlerName(ByVal AlertRuleID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@AlertRuleID", AlertRuleID}}
        strConnection = GetSQL(3816, parray)(0)
        sqlParam = GetSQL(3816, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadKPIRuleTypeFields(ByVal kpiRuleType As String)
        GetKpiRuleTypeFields(kpiRuleType)
        Dim data As DataRow = gvKPIRules.GetDataRow(gvKPIRules.FocusedRowHandle)

        Dim dr() As DataRow = Nothing
        If data IsNot Nothing Then
            dr = dtAlertConfig.Select("[KPI_RULEID]=" & data.Item(5))
        End If

        If Not dr Is Nothing AndAlso dr.Length > 0 Then
            If dtKpiRuleTypeFields.Rows.Count > 0 Then
                propGrid.SelectedObject = Nothing
                objKpiRuleProp = New KPIRuleProperties()
                For Each dtRow As DataRow In dtKpiRuleTypeFields.Rows
                    Select Case dtRow(1)
                        Case "InputdataMatchDays"
                            objKpiRuleProp.InputdataMatchDays = IIf(IsDBNull(dr(0)("InputdataMatchDays")), 0, dr(0)("InputdataMatchDays"))
                        Case "InputdataMatchHours"
                            objKpiRuleProp.InputdataMatchHours = IIf(IsDBNull(dr(0)("InputdataMatchHours")), 0, dr(0)("InputdataMatchHours"))
                        Case "InputDataSlidingWindow"
                            objKpiRuleProp.InputDataSlidingWindow = IIf(IsDBNull(dr(0)("InputDataSlidingWindow")), 0, dr(0)("InputDataSlidingWindow"))
                        Case "InputDataPeriodInterval"
                            objKpiRuleProp.InputDataPeriodInterval = IIf(IsDBNull(dr(0)("InputDataPeriodInterval")), "", dr(0)("InputDataPeriodInterval"))
                        Case "FixedLowerThreshold"
                            objKpiRuleProp.FixedLowerThreshold = IIf(IsDBNull(dr(0)("FixedLowerThreshold")), "", dr(0)("FixedLowerThreshold"))
                        Case "FixedUpperTreshold"
                            objKpiRuleProp.FixedUpperTreshold = IIf(IsDBNull(dr(0)("FixedUpperTreshold")), "", dr(0)("FixedUpperTreshold"))
                        Case "OccurencesSlidingWindow"
                            objKpiRuleProp.OccurencesSlidingWindow = IIf(IsDBNull(dr(0)("OccurencesSlidingWindow")), 0, dr(0)("OccurencesSlidingWindow"))
                        Case "OccurencesThreshold"
                            objKpiRuleProp.OccurencesThreshold = IIf(IsDBNull(dr(0)("OccurencesThreshold")), 0, dr(0)("OccurencesThreshold"))
                        Case "PercLowerTreshold"
                            objKpiRuleProp.PercLowerTreshold = IIf(IsDBNull(dr(0)("PercLowerTreshold")), "", dr(0)("PercLowerTreshold"))
                        Case "PercUpperTreshold"
                            objKpiRuleProp.PercUpperTreshold = IIf(IsDBNull(dr(0)("PercUpperTreshold")), "", dr(0)("PercUpperTreshold"))
                        Case "ZScoreLowerTreshold"
                            objKpiRuleProp.ZScoreLowerTreshold = IIf(IsDBNull(dr(0)("ZScoreLowerTreshold")), "", dr(0)("ZScoreLowerTreshold"))
                        Case "ZScoreUpperTreshold"
                            objKpiRuleProp.ZScoreUpperTreshold = IIf(IsDBNull(dr(0)("ZScoreUpperTreshold")), "", dr(0)("ZScoreUpperTreshold"))
                        Case "SigmaFilterOutliers"
                            objKpiRuleProp.SigmaFilterOutliers = IIf(IsDBNull(dr(0)("SigmaFilterOutliers")), 0, dr(0)("SigmaFilterOutliers"))
                        Case "Px"
                            objKpiRuleProp.Px = IIf(IsDBNull(dr(0)("Px")), "", dr(0)("Px"))
                        Case "PxOperator"
                            objKpiRuleProp.PxOperator = IIf(IsDBNull(dr(0)("PxOperator")), "", dr(0)("PxOperator"))
                        Case "ExcludeNightTimes"
                            objKpiRuleProp.ExcludeNightTimes = IIf(IsDBNull(dr(0)("ExcludeNightTimes")), 0, dr(0)("ExcludeNightTimes"))
                    End Select
                Next
                SetPropertyAsBrowsable(dtKpiRuleTypeFields)
                propGrid.SelectedObject = objKpiRuleProp
            End If
        Else
            If dtKpiRuleTypeFields.Rows.Count > 0 Then
                propGrid.SelectedObject = Nothing
                objKpiRuleProp = New KPIRuleProperties()
                For Each dtRow As DataRow In dtKpiRuleTypeFields.Rows
                    Select Case dtRow(1)
                        Case "InputdataMatchDays"
                            objKpiRuleProp.InputdataMatchDays = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "InputdataMatchHours"
                            objKpiRuleProp.InputdataMatchHours = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "InputDataSlidingWindow"
                            objKpiRuleProp.InputDataSlidingWindow = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "InputDataPeriodInterval"
                            objKpiRuleProp.InputDataPeriodInterval = IIf(IsDBNull(dtRow("DefaultValue")), "", dtRow("DefaultValue"))
                        Case "FixedLowerThreshold"
                            objKpiRuleProp.FixedLowerThreshold = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "FixedUpperTreshold"
                            objKpiRuleProp.FixedUpperTreshold = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "OccurencesSlidingWindow"
                            objKpiRuleProp.OccurencesSlidingWindow = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "OccurencesThreshold"
                            objKpiRuleProp.OccurencesThreshold = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "PercLowerTreshold"
                            objKpiRuleProp.PercLowerTreshold = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "PercUpperTreshold"
                            objKpiRuleProp.PercUpperTreshold = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "ZScoreLowerTreshold"
                            objKpiRuleProp.ZScoreLowerTreshold = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "ZScoreUpperTreshold"
                            objKpiRuleProp.ZScoreUpperTreshold = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "SigmaFilterOutliers"
                            objKpiRuleProp.SigmaFilterOutliers = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "Px"
                            objKpiRuleProp.Px = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "PxOperator"
                            objKpiRuleProp.PxOperator = IIf(IsDBNull(dtRow("DefaultValue")), 0, dtRow("DefaultValue"))
                        Case "ExcludeNightTimes"
                            objKpiRuleProp.ExcludeNightTimes = IIf(IsDBNull(dr(0)("DefaultValue")), 0, dr(0)("DefaultValue"))
                    End Select
                Next
                SetPropertyAsBrowsable(dtKpiRuleTypeFields)
                propGrid.SelectedObject = objKpiRuleProp
            End If
        End If
    End Sub

    Private Sub SetPropertyAsBrowsable(ByVal dt As DataTable)
        Dim oDescriptor As System.ComponentModel.PropertyDescriptor = Nothing
        Dim oBrowsableAttribute As System.ComponentModel.BrowsableAttribute = Nothing
        Dim oDescAttr As System.ComponentModel.DescriptionAttribute = Nothing
        Dim oField As Reflection.FieldInfo = Nothing
        Dim oField2 As Reflection.FieldInfo = Nothing

        For Each oProperty As Reflection.PropertyInfo In objKpiRuleProp.GetType.GetProperties
            oDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(GetType(KPIRuleProperties))(oProperty.Name)
            oBrowsableAttribute = DirectCast(oDescriptor.Attributes(GetType(System.ComponentModel.BrowsableAttribute)), System.ComponentModel.BrowsableAttribute)
            If (oDescriptor IsNot Nothing) Then
                If (oBrowsableAttribute IsNot Nothing) Then
                    oField = oBrowsableAttribute.[GetType]().GetField("BROWSABLE", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.IgnoreCase)
                    oField.SetValue(oBrowsableAttribute, False)
                End If
            End If
        Next oProperty

        For Each dtRow As DataRow In dt.Rows

            For Each oProperty As Reflection.PropertyInfo In objKpiRuleProp.GetType.GetProperties
                oDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(GetType(KPIRuleProperties))(oProperty.Name)
                oBrowsableAttribute = DirectCast(oDescriptor.Attributes(GetType(System.ComponentModel.BrowsableAttribute)), System.ComponentModel.BrowsableAttribute)
                oDescAttr = DirectCast(oDescriptor.Attributes(GetType(System.ComponentModel.DescriptionAttribute)), System.ComponentModel.DescriptionAttribute)
                If (oDescriptor IsNot Nothing) Then
                    If (oBrowsableAttribute IsNot Nothing) Then
                        oField = oBrowsableAttribute.[GetType]().GetField("BROWSABLE", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.IgnoreCase)
                        oField2 = oDescAttr.[GetType]().GetField("DESCRIPTION", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.IgnoreCase)

                        If dtRow("KPI_RuleProperties").ToString.ToLower = oProperty.Name.ToLower Then
                            oField.SetValue(oBrowsableAttribute, True)
                            oField2.SetValue(oDescAttr, dtRow(3).ToString)
                        End If
                    End If
                End If
            Next oProperty
        Next

    End Sub

    Private Sub GetAlertConfigurationDetails(ByVal alertRuleID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@AlertRuleID", alertRuleID}
        }
        strConnection = GetSQL(3812, parray)(0)
        sqlParam = GetSQL(3812, parray)(1)
        dtAlertConfig = New DataTable()
        dtAlertConfig = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Public Sub LoadKPIRules(ByVal alertRuleID As Integer)
        GetAlertConfigurationDetails(alertRuleID)

        gcKPIRules.DataSource = Nothing
        gvKPIRules.Columns.Clear()

        If dtAlertConfig.Rows.Count > 0 Then
            Dim dtTemp As DataTable = dtAlertConfig.DefaultView.ToTable(True, {"Technology", "ObjectType", "ObjectReported", "KPI_Name", "KPI_RuleTypeName", "KPI_RULEID", "ALERT_RULEID", "KPI_RuleTypeName_Short", "KPI_RuleType", "DataAvailable"})

            'Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "CampaignEnabled", "ScheduleNextStartDate", "ScheduleRepeatInterval", "LastRunTime", "LastEndTime", "LastStatus"}
            IOSDevExpressGrid.PopulateDataInGrid(gcKPIRules, gvKPIRules, dtTemp, "ALL", {"KPI_RuleType"})

            gvKPIRules.Columns(5).Visible = False
            gvKPIRules.Columns(6).Visible = False
            gvKPIRules.Columns(7).Visible = False
            gvKPIRules.AutoFillColumn = gvKPIRules.Columns(3)

            seAlertOccurence.EditValue = dtAlertConfig.Rows(0)("AlertOccurences")
            seAlertWindow.EditValue = dtAlertConfig.Rows(0)("AlertSlidingWindowDays")
        End If
    End Sub

    Private Sub LoadKPIFailureColumn()
        RemoveHandler cmbKPIFailureColumn.SelectedIndexChanged, AddressOf cmbKPIFailureColumn_SelectedIndexChanged
        If dtAlertConfig.Rows.Count > 0 Then
            Dim dtTemp As DataTable = dtAlertConfig.DefaultView.ToTable(True, {"KPI_RULEID", "KPI_Name"})
            Dim dataview As New DataView(dtTemp)
            dataview.Sort = "KPI_Name ASC"
            Dim dtSorted As DataTable = dataview.ToTable()
            BindDevExComboBoxWithValueMember(cmbKPIFailureColumn, dtSorted, "KPI_RULEID", "KPI_Name", "Select", False)
        End If
        AddHandler cmbKPIFailureColumn.SelectedIndexChanged, AddressOf cmbKPIFailureColumn_SelectedIndexChanged
    End Sub

    Private Sub AttachAutoCompleteWithTextBox(ByRef txt As DevExpress.XtraEditors.TextEdit)
        'Auto Complete
        Try
            Dim str() As String = Nothing
            Dim dt As New DataTable

            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewAlerts.FocusedNode
            Dim data As DataRowView = lstviewAlerts.GetDataRecordByNode(node)
            Dim AlertRuleID As Integer = -1

            If data IsNot Nothing Then
                AlertRuleID = data.Item(0).ToString
            End If

            Dim parray()() As String = {
                New String() {"@AlertRuleID", AlertRuleID}
            }
            Dim strConnection As String = GetSQL(3837, parray)(0)
            Dim sqlParam As String = GetSQL(3837, parray)(1)

            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            str = dt.Rows.OfType(Of DataRow)().[Select](Function(k) k(0).ToString()).ToArray()

            Dim collection As New AutoCompleteStringCollection()
            collection.AddRange(str)
            txt.MaskBox.AutoCompleteCustomSource = collection
            txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource
            txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub DeleteKPIRule()
        Try
            Dim alertRuleID As Integer = GetFocusedAlertID()
            Dim kpiRuleID As Integer = 0

            Dim strConnection As String, sqlParam As String
            Dim dataKpi As DataRow = gvKPIRules.GetDataRow(gvKPIRules.FocusedRowHandle)

            If dataKpi IsNot Nothing Then
                kpiRuleID = dataKpi.Item("KPI_RULEID")
            End If

            Dim parray()() As String = {
                New String() {"@AlertRuleID", alertRuleID},
                New String() {"@KpiRuleID", kpiRuleID}
            }

            strConnection = GetSQL(3835, parray)(0)
            sqlParam = GetSQL(3835, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            LoadKPIRules(alertRuleID)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Function GetFocusedAlertID() As Integer
        Dim alertRuleID As Integer = 0
        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewAlerts.FocusedNode
        Dim data As DataRowView = lstviewAlerts.GetDataRecordByNode(node)
        If data IsNot Nothing Then
            alertRuleID = data.Item(0).ToString
        End If
        Return alertRuleID
    End Function

    Private Function GetFocusedAlertName() As String
        Dim alertRuleName As String = Nothing
        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewAlerts.FocusedNode
        Dim data As DataRowView = lstviewAlerts.GetDataRecordByNode(node)
        If data IsNot Nothing Then
            alertRuleName = data.Item(1).ToString
        End If
        Return alertRuleName
    End Function

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub GetKpiRuleTypeFields(ByVal kpiRuleType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@kpiRuleType", kpiRuleType}}
        strConnection = GetSQL(3818, parray)(0)
        sqlParam = GetSQL(3818, parray)(1)
        dtKpiRuleTypeFields = New DataTable()
        dtKpiRuleTypeFields = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadEventReport()
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = Nothing
            strConnection = GetSQL(3852, parray)(0)
            sqlParam = GetSQL(3852, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            BindDevExComboBoxWithValueMember(cmbEventReport, dt, "ReportID", "ReportName", "Select")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub DeleteKpiRulesFilter(ByVal filterID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@FilterID", filterID}
        }
        strConnection = GetSQL(3855, parray)(0)
        sqlParam = GetSQL(3855, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadKpiRulesFilter(kpiRuleID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@KPIRuleID", kpiRuleID}
        }
        strConnection = GetSQL(3854, parray)(0)
        sqlParam = GetSQL(3854, parray)(1)
        Dim dtFilter As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(gcKpiRulesFilter, gvKpiRulesFilter, dtFilter, "ALL", {"FilterID"}, "FilterString")
    End Sub

#End Region

#Region "Control Events"

    Private Sub txtAlertSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtAlertSearch.KeyUp
        Try
            If dtAlertName IsNot Nothing Then
                If (txtAlertSearch.Text.Length > 0) Then
                    dtAlertName.DefaultView.RowFilter = "[AlertName] Like '%" & txtAlertSearch.Text & "%'"
                Else
                    dtAlertName.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAddNewAlert_Click(sender As Object, e As EventArgs) Handles btnAddNewAlert.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Select default as [Huawei 3G]
            'If cmbTechnology.SelectedIndex = 0 Then
            '	cmbTechnology.SelectedIndex = 2
            'End If
            ''Select default as [MeanStdDev Threshold]
            'If cmbMethod.SelectedIndex = 0 Then
            '	cmbMethod.SelectedIndex = 4
            'End If

            Dim objDlgAlert As New dlgAlertName()
            objDlgAlert.alertOccurences = seAlertOccurence.EditValue
            objDlgAlert.alertSlidingWinDays = seAlertWindow.EditValue
            objDlgAlert.ShowDialog()

            If (newAlertName IsNot Nothing) Then
                RemoveHandler lstviewAlerts.FocusedNodeChanged, AddressOf lstviewAlerts_FocusedNodeChanged
                LoadAlertList()

                gcKPIRules.DataSource = Nothing
                gvKPIRules.Columns.Clear()
                'cmbMethod.SelectedIndex = 0

                AddHandler lstviewAlerts.FocusedNodeChanged, AddressOf lstviewAlerts_FocusedNodeChanged
                lstviewAlerts.SetFocusedNode(lstviewAlerts.FindNodeByFieldValue("AlertName", newAlertName))
                lstviewAlerts_FocusedNodeChanged(Nothing, Nothing)
            End If
            lstviewAlerts.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteAlert_Click(sender As Object, e As EventArgs) Handles btnDeleteAlert.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim isPowerUser As Boolean = False
            If lblAlertOwner.Text.ToLower = Environment.UserName.ToLower Then
                isPowerUser = True
            End If

            If lblAlertOwner.Text.ToLower <> Environment.UserName.ToLower Then
                'Checking whether the current user (not alert owner) is a power user
                If configMgr.User.IsPowerUser = True Then
                    isPowerUser = True
                Else
                    XtraMessageBox.Show("Current user can't delete an alert as the alert owner is a different user.", "Delete Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    lblAlertOwner.ForeColor = Color.Red
                    lblAlertOwner.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                    isPowerUser = False
                    Exit Sub
                End If
            End If

            If (isPowerUser = True) Then
                If (lstviewAlerts.FocusedNode IsNot Nothing) Then
                    Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewAlerts.FocusedNode
                    If XtraMessageBox.Show("Are you sure to delete alert name: " & GetFocusedAlertName() & "?", "Delete Alert Name", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        DeleteAlerName(GetFocusedAlertID())
                        lstviewAlerts.DeleteNode(node)
                        If lstviewAlerts.Nodes.Count > 0 Then
                            lstviewAlerts.SetFocusedNode(lstviewAlerts.Nodes(0))
                        End If
                        lstviewAlerts.Refresh()
                        btnClearChart_Click(Nothing, Nothing)
                        'lstviewAlerts_FocusedNodeChanged(Nothing, Nothing)
                    End If
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

    Private Sub lstviewAlerts_CellValueChanged(sender As Object, e As DevExpress.XtraTreeList.CellValueChangedEventArgs) Handles lstviewAlerts.CellValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim data As DataRowView = lstviewAlerts.GetDataRecordByNode(e.Node)
            If data IsNot Nothing Then
                If e.Value = "" Then
                    XtraMessageBox.Show("Alert name can not leave blank!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    LoadAlertList()
                Else
                    Dim parray()() As String = {
                        New String() {"@alertName", Chr(39) & e.Value.ToString & Chr(39)},
                        New String() {"@alertRuleID", data.Item(0)}
                    }

                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(3838, parray)(0), GetSQL(3838, parray)(1))
                    GetAlertConfigurationDetails(data.Item(0))
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

    Private Sub lstviewAlerts_FocusedNodeChanged(sender As Object, e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles lstviewAlerts.FocusedNodeChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'cmbMethod.SelectedIndex = 0
            If GetFocusedAlertID() <> 0 Then
                LoadKPIRules(GetFocusedAlertID())
                LoadKPIFailureColumn()
                LoadAlertDataByID(GetFocusedAlertID())

                Dim dr() As DataRow
                dr = dtAlertName.Select("ALERT_RULEID=" & GetFocusedAlertID())
                If dr.Length > 0 Then
                    grpAlertProperties.Enabled = True
                    lblAlertOwner.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                    lblAlertOwner.ForeColor = Color.Black
                    propGrid.Enabled = True
                    gcKPIRules.Enabled = True
                    gvKPIRules.Tag = Environment.UserName
                Else
                    grpAlertProperties.Enabled = True
                    lblAlertOwner.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                    lblAlertOwner.ForeColor = Color.Black
                    propGrid.Enabled = False
                    gcKPIRules.Enabled = False
                    gvKPIRules.Tag = Nothing
                End If
            End If
            btnClearChart_Click(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub lstviewAlerts_ShowingEditor(sender As Object, e As CancelEventArgs) Handles lstviewAlerts.ShowingEditor
        Try
            If GetFocusedAlertID() <> 0 Then
                Dim dr() As DataRow
                dr = dtAlertName.Select("ALERT_RULEID=" & GetFocusedAlertID())
                If dr.Length > 0 Then
                    'If dr(0).Item("AlertOwner").ToString.ToLower <> Environment.UserName.ToLower Then
                    '	e.Cancel = True
                    'End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvKPIRules_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvKPIRules.FocusedRowChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim data As DataRow = gvKPIRules.GetDataRow(gvKPIRules.FocusedRowHandle)

            If data IsNot Nothing Then
                'cmbMethod.SelectedIndex = 0
                'If data.Item(0) = "2G" Then
                '	cmbTechnology.SelectedIndex = 1
                'ElseIf data.Item(0) = "3G" Then
                '	cmbTechnology.SelectedIndex = 2
                'ElseIf data.Item(0) = "4G" Then
                '	cmbTechnology.SelectedIndex = 3
                'ElseIf data.Item(0) = "5G" Then
                '	cmbTechnology.SelectedIndex = 4
                'Else
                '	cmbTechnology.SelectedItem = GetComboItemFromText(data.Item(0), cmbTechnology)
                'End If

                'cmbMethod.SelectedItem = GetComboItemFromText(data.Item(4), cmbMethod)
                LoadKPIRuleTypeFields(CInt(data.Item(8)))
                LoadKpiRulesFilter(CInt(data.Item(5)))

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    'Private Sub cmbMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMethod.SelectedIndexChanged
    '	Try
    '		Me.Cursor = Cursors.WaitCursor
    '		Application.DoEvents()
    '		If cmbMethod.SelectedIndex = 0 Then
    '			propGrid.SelectedObject = Nothing
    '		Else
    '			Dim kpiRuleType As Integer = GetComboItemFromText(cmbMethod.Text.Trim, cmbMethod).Value
    '			LoadKPIRuleTypeFields(kpiRuleType)
    '		End If
    '	Catch ex As Exception
    '		UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '		_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '	Finally
    '		Me.Cursor = Cursors.Default
    '		Application.DoEvents()
    '	End Try
    'End Sub

    Private Sub UpdatePropertiesInDB(changedPropertyItem As GridItem)
        Try
            Dim kPIRuleID As Integer = -1
            Dim dataKpiRulkes As DataRow = gvKPIRules.GetDataRow(gvKPIRules.FocusedRowHandle)
            kPIRuleID = dataKpiRulkes.Item("KPI_RULEID")

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@KPIRuleId", kPIRuleID},
                New String() {"@PropertyName", Chr(39) & changedPropertyItem.PropertyDescriptor.Name & Chr(39)},
                New String() {"@PropertyValue", Chr(39) & changedPropertyItem.Value & Chr(39)}
            }
            strConnection = GetSQL(3823, parray)(0)
            sqlParam = GetSQL(3823, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub propGridControl_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles propGrid.PropertyValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim changedPropertyItem As GridItem = e.ChangedItem
            If (Not changedPropertyItem Is Nothing) Then
                If changedPropertyItem.PropertyDescriptor.Name.ToUpper = "INPUTDATAPERIODINTERVAL" Then
                    objKpiRuleProp.InputDataPeriodInterval = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "INPUTDATASLIDINGWINDOW" Then
                    objKpiRuleProp.InputDataSlidingWindow = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "INPUTDATAMATCHDAYS" Then
                    objKpiRuleProp.InputdataMatchDays = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "InputdataMatchHours" Then
                    objKpiRuleProp.InputdataMatchHours = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "FIXEDLOWERTHRESHOLD" Then
                    objKpiRuleProp.FixedLowerThreshold = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "FIXEDUPPERTRESHOLD" Then
                    objKpiRuleProp.FixedUpperTreshold = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "SIGMAFILTEROUTLIERS" Then
                    objKpiRuleProp.SigmaFilterOutliers = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "PERCLOWERTRESHOLD" Then
                    objKpiRuleProp.PercLowerTreshold = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "PERCUPPERTRESHOLD" Then
                    objKpiRuleProp.PercUpperTreshold = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "ZSCORELOWERTRESHOLD" Then
                    objKpiRuleProp.ZScoreLowerTreshold = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "ZSCOREUPPERTRESHOLD" Then
                    objKpiRuleProp.ZScoreUpperTreshold = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "OCCURENCESTHRESHOLD" Then
                    objKpiRuleProp.OccurencesThreshold = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "OCCURENCESSLIDINGWINDOW" Then
                    objKpiRuleProp.OccurencesSlidingWindow = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "PX" Then
                    objKpiRuleProp.Px = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "PXOPERATOR" Then
                    objKpiRuleProp.PxOperator = changedPropertyItem.Value
                ElseIf changedPropertyItem.PropertyDescriptor.Name.ToUpper = "EXCLUDENIGHTTIMES" Then
                    objKpiRuleProp.ExcludeNightTimes = changedPropertyItem.Value
                End If
                UpdatePropertiesInDB(changedPropertyItem)

                'Update dtAlertConfig and get the config details as the property value gets changed
                Dim iFocuedRow As Integer = gvKPIRules.FocusedRowHandle
                If GetFocusedAlertID() <> 0 Then
                    GetAlertConfigurationDetails(GetFocusedAlertID())
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

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If XtraMessageBox.Show("Are you sure to update alert details?", "Update Alert Properties", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim alertRuleID As Integer = GetFocusedAlertID()

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@AlertRuleID", alertRuleID},
                    New String() {"@Occurences", seAlertOccurence.EditValue},
                    New String() {"@SildingWinDays", seAlertWindow.EditValue}
                }
                strConnection = GetSQL(3828, parray)(0)
                sqlParam = GetSQL(3828, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            LoadChart(dtAlertConfig.Rows(0).Item("ALERT_RULEID"), dtAlertConfig.Rows(0).Item("AlertName"), "")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnClearChart_Click(sender As Object, e As EventArgs) Handles btnClearChart.Click
        ExtraLegendEntryCollection.Clear()
        DefaultSeriesCollection.Clear()
        chAlert.Annotations.Clear()
        chAlert.XAxis.Markers.Clear()
        chAlert.YAxis.Markers.Clear()
        chAlert.LegendBox.ExtraEntries.Clear()
        chAlert.SeriesCollection.Clear()
        dtChartData = Nothing
        chAlert.Refresh()

        gvChartAlert.Columns.Clear()
        gcChartAlert.DataSource = Nothing
    End Sub

    Private Sub tglAlertTest_Click(sender As Object, e As EventArgs) Handles tglAlertTest.Click
        Try
            tglAlertTest.ChangeToggleState()
            If tglAlertTest.ToggleState = CheckState.Checked Then
                tglAlertTest.Text = "Hide Grid"
                sccAlertChart.Collapsed = False
            ElseIf tglAlertTest.ToggleState = CheckState.Unchecked Then
                tglAlertTest.Text = "Show Grid"
                sccAlertChart.Collapsed = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    'Private Sub lstviewKPI_MouseMove(sender As Object, e As MouseEventArgs) Handles lstviewKPI.MouseMove
    '	Try
    '		If e.Button = MouseButtons.Left Then
    '			Dim nd As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewKPI.FocusedNode
    '			Dim dataKpi As DataRowView = lstviewKPI.GetDataRecordByNode(nd)

    '			If dataKpi IsNot Nothing Then
    '				Dim obj() As Object = {"KPIRuleDrag", dataKpi}
    '				lstviewKPI.DoDragDrop(obj, DragDropEffects.Copy)
    '			End If
    '		End If
    '	Catch ex As Exception
    '		UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '		_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '	End Try
    'End Sub

    'Private Sub gcKPIRules_DragDrop(sender As Object, e As DragEventArgs) Handles gcKPIRules.DragDrop
    '	Try
    '		WaitScreen.ShowWaitScreen("Adding KPI to Anomaly, Please Wait.")
    '		Application.DoEvents()

    '		Dim kpiRule() As Object = e.Data.GetData("System.Object[]")
    '		If kpiRule IsNot Nothing Then
    '			If kpiRule(0) = "KPIRuleDrag" Then
    '				Dim drv As DataRowView = CType(kpiRule(1), DataRowView)

    '				If Not dtAlertConfig Is Nothing Then
    '					For Each dtRow As DataRow In dtAlertConfig.Rows
    '						If drv(1).ToString.ToLower = dtRow("KPI_Name").ToString.ToLower And CType(cmbMethod.SelectedItem, IOS.Library.clsComboBoxItem).Value = CInt(dtRow("KPI_RuleType")) Then
    '							Exit Sub
    '						End If
    '					Next
    '				End If

    '				If cmbMethod.SelectedIndex = 0 Then
    '					XtraMessageBox.Show("Select Method first!", "Drag KPI to KPI Rule", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '					Exit Sub
    '				End If
    '				If lstviewAlerts.FocusedNode Is Nothing Then
    '					XtraMessageBox.Show("Select Alert first!", "Drag KPI Rule to Chart", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '					Exit Sub
    '				End If
    '				'AddKpiToKpiRules(drv)
    '			End If
    '		End If
    '		e.Effect = DragDropEffects.None
    '	Catch ex As Exception
    '		UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '		_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
    '	Finally
    '		WaitScreen.CloseWaitScreen()
    '		Application.DoEvents()
    '	End Try
    'End Sub

    Private Sub gvKPIRules_DragOver(sender As Object, e As DragEventArgs) Handles chAlert.DragOver, gcKPIRules.DragOver
        Try
            If e.Data.GetDataPresent("System.Object[]") Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub lstviewAlerts_MouseMove(sender As Object, e As MouseEventArgs) Handles lstviewAlerts.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim dr As DataRow = lstviewAlerts.GetFocusedDataRow()
                If dr IsNot Nothing Then
                    Dim obj() As Object = {"AlertDrag", dr}
                    lstviewAlerts.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvKPIRules_MouseMove(sender As Object, e As MouseEventArgs) Handles gvKPIRules.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim dr As DataRow = gvKPIRules.GetFocusedDataRow()
                If dr IsNot Nothing Then
                    Dim obj() As Object = {"KPIRuleDrag", dr}
                    gcKPIRules.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ceAlertEnabled_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@AlertEnabled", IIf(ceAlertEnabled.Checked, 1, 0)}
            }
            strConnection = GetSQL(3840, parray)(0)
            sqlParam = GetSQL(3840, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceDashboardScore_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@DashboardScoreActive", IIf(ceDashboardScore.Checked, 1, 0)}
            }
            strConnection = GetSQL(3844, parray)(0)
            sqlParam = GetSQL(3844, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtDashboardScore_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDashboardScore.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub seAlertOccurence_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@Occurences", seAlertOccurence.EditValue}
            }
            strConnection = GetSQL(3833, parray)(0)
            sqlParam = GetSQL(3833, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub seAlertWindow_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@SlidingWinDays", seAlertWindow.EditValue}
            }
            strConnection = GetSQL(3834, parray)(0)
            sqlParam = GetSQL(3834, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceEventEmail_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@Email", IIf(ceEventEmail.Checked, 1, 0)}
            }
            strConnection = GetSQL(3829, parray)(0)
            sqlParam = GetSQL(3829, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceEventSNMP_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@Snmp", IIf(ceEventSNMP.Checked, 1, 0)}
            }
            strConnection = GetSQL(3831, parray)(0)
            sqlParam = GetSQL(3831, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceEventReport_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@AlertEventReportEnabled", IIf(ceEventReport.Checked, 1, 0)}
            }
            strConnection = GetSQL(3850, parray)(0)
            sqlParam = GetSQL(3850, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtEventEmail_KeyUp(sender As Object, e As KeyEventArgs) Handles txtEventEmail.KeyUp
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If e.KeyCode = Keys.Enter And Not String.IsNullOrEmpty(txtEventEmail.Text.Trim) Then
                UpdateAlertEmail()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtEventEmail_Leave(sender As Object, e As EventArgs) Handles txtEventEmail.Leave
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Not String.IsNullOrEmpty(txtEventEmail.Text.Trim) Then
                UpdateAlertEmail()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub UpdateAlertEmail()
        'Dim emailList() = txtEventEmail.Text.Trim.Split({",", ";"}, StringSplitOptions.RemoveEmptyEntries)
        'For i As Integer = 0 To emailList.Length - 1
        '    If Not Regex.IsMatch(emailList(i).Trim(), "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") Then
        '        XtraMessageBox.Show("Incorrect Email, Please check", "Alert Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If
        'Next

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@AlertRuleID", GetFocusedAlertID()},
            New String() {"@EmailAddress", "'" & txtEventEmail.Text.Trim & "'"}
        }
        strConnection = GetSQL(3830, parray)(0)
        sqlParam = GetSQL(3830, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub txtEventSNMP_KeyUp(sender As Object, e As KeyEventArgs) Handles txtEventSNMP.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@AlertRuleID", GetFocusedAlertID()},
                    New String() {"@SnmpDesc", "'" & txtEventSNMP.Text.Trim & "'"}
                }
                strConnection = GetSQL(3832, parray)(0)
                sqlParam = GetSQL(3832, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtDashboardScore_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDashboardScore.KeyUp
        Try
            'If e.KeyCode = Keys.Enter Then
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@AlertRuleID", GetFocusedAlertID()},
                New String() {"@DashboardScore", txtDashboardScore.Text.Trim}
            }
            strConnection = GetSQL(3845, parray)(0)
            sqlParam = GetSQL(3845, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            'End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub DeleteKPIRuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteKPIRuleToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If XtraMessageBox.Show("Are you sure to delete kpi rule?", "Delete KPI rule", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                DeleteKPIRule()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvKPIRules_KeyUp(sender As Object, e As KeyEventArgs) Handles gvKPIRules.KeyUp
        Try
            If e.KeyCode = Keys.Delete Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                If XtraMessageBox.Show("Are you sure to delete kpi rule?", "Delete KPI rule", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteKPIRule()
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

    Private Sub cmsKPIRule_Opening(sender As Object, e As CancelEventArgs) Handles cmsKPIRule.Opening
        If gvKPIRules.Tag.ToString.ToLower = Environment.UserName.ToLower Then
            DeleteKPIRuleToolStripMenuItem.Enabled = True
        Else
            DeleteKPIRuleToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub btnAlertProcess_Click(sender As Object, e As EventArgs) Handles btnAlertProcess.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim processDate As Date = deAlertProcessDate.EditValue
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@alertid", GetFocusedAlertID()},
                New String() {"@checkdate", "'" & processDate.ToString("yyyy-MM-dd") & "'"}
            }
            strConnection = GetSQL(3842, parray)(0)
            sqlParam = GetSQL(3842, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnTestKPI_Click(sender As Object, e As EventArgs) Handles btnTestKPI.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim kpiRuleID As Integer = 0
            Dim processDate As Date = deTestKPIRule.EditValue

            Dim dataKpi As DataRow = gvKPIRules.GetDataRow(gvKPIRules.FocusedRowHandle)
            If dataKpi IsNot Nothing Then
                kpiRuleID = dataKpi.Item("KPI_RULEID")
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@kpiruleid", kpiRuleID},
                New String() {"@checkdate", "'" & processDate.ToString("yyyy-MM-dd") & "'"}
            }
            strConnection = GetSQL(3843, parray)(0)
            sqlParam = GetSQL(3843, parray)(1)
            lblCountBreach.Text = IOS.DataLibrary.DataAccessorODBC.ExecuteScalar(strConnection, sqlParam)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cm_CopyGridData_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cm_CopyGridData.Opening
        Try
            Dim conTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.MainView
            tsmi_RecordCount.Text = "Record Count: " & gridView.RowCount.ToString()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_Copy_All_Click(sender As Object, e As EventArgs) Handles tsmi_Copy_All.Click, tsmi_KPI_Rules_Copy_All.Click
        Try
            Dim requestMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim conTemp As ContextMenuStrip = TryCast(requestMenu.GetCurrentParent(), ContextMenuStrip)
            Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView, True, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_Copy_Selection_Click(sender As Object, e As EventArgs) Handles tsmi_Copy_SelectionWOHeader.Click, tsmi_KPI_Rules_Copy_SelectionWOHeader.Click
        Try
            Dim requestMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim conTemp As ContextMenuStrip = TryCast(requestMenu.GetCurrentParent(), ContextMenuStrip)
            Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView, False, False)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbKPIFailureColumn_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbKPIFailureColumn.SelectedIndex > 0 Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@failuresColumnKPIRULEID", TryCast(cmbKPIFailureColumn.SelectedItem, IOS.Library.clsComboBoxItem).Value},
                    New String() {"@AlertRuleID", GetFocusedAlertID()}
                }
                strConnection = GetSQL(3848, parray)(0)
                sqlParam = GetSQL(3848, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                SetMessage("Success: Alert config details updated")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            SetMessage("Error : Alert config details could not be updated")
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
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

    Private Sub tsmi_KPI_Rules_Copy_SelectionWithHeader_Click(sender As Object, e As EventArgs) Handles tsmi_KPI_Rules_Copy_SelectionWithHeader.Click, tsmi_Copy_SelectionWithHeader.Click
        Try
            Dim requestMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim conTemp As ContextMenuStrip = TryCast(requestMenu.GetCurrentParent(), ContextMenuStrip)
            Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView, False, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnAddKPI_Click(sender As Object, e As EventArgs) Handles btnAddKPI.Click
        Try
            Dim objAddKpi As New dlgAddKPIAlertMngr()
            objAddKpi.AlertRuleID = GetFocusedAlertID()

            If gvKPIRules.RowCount <> 0 Then
                objAddKpi.defTech = gvKPIRules.GetFocusedRowCellValue("Technology")
                objAddKpi.defObjectType = gvKPIRules.GetFocusedRowCellValue("ObjectType")
                objAddKpi.defTarget = gvKPIRules.GetFocusedRowCellValue("ObjectReported")
                objAddKpi.defKPIRuleType = gvKPIRules.GetFocusedRowCellValue("KPI_RuleType")
            Else
                objAddKpi.defTech = Nothing
                objAddKpi.defObjectType = Nothing
                objAddKpi.defTarget = Nothing
                objAddKpi.defKPIRuleType = Nothing
            End If

            objAddKpi.ShowDialog()
            LoadKPIRules(GetFocusedAlertID())
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteKPI_Click(sender As Object, e As EventArgs) Handles btnDeleteKPI.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If XtraMessageBox.Show("Are you sure to delete kpi rule?", "Delete KPI rule", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                DeleteKPIRule()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnMethod_Click(sender As Object, e As EventArgs) Handles btnMethod.Click
        Try
            Dim objMethod As New dlgMethodAlertMngr()
            objMethod.kpiRuleID = gvKPIRules.GetFocusedRowCellValue("KPI_RULEID")
            objMethod.kpiRuleType = gvKPIRules.GetFocusedRowCellValue("KPI_RuleType")
            objMethod.ShowDialog()
            LoadKPIRules(GetFocusedAlertID())
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbEventReport_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbEventReport.SelectedIndex > 0 Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@AlertEventReportID", TryCast(cmbEventReport.SelectedItem, Library.clsComboBoxItem).Value},
                    New String() {"@AlertRuleID", GetFocusedAlertID()}
                }
                strConnection = GetSQL(3851, parray)(0)
                sqlParam = GetSQL(3851, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                SetMessage("Success: Alert config details updated")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            SetMessage("Error : Alert config details could not be updated")
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnAddFilter_Click(sender As Object, e As EventArgs) Handles btnAddFilter.Click
        Try
            Dim drKpiRules As DataRow = gvKPIRules.GetFocusedDataRow()

            Dim dr As DataRow = gvKPIRules.GetFocusedDataRow()
            Dim kpiRuleID As Integer = CInt(dr("KPI_RULEID"))

            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewAlerts.FocusedNode
            Dim data As DataRowView = lstviewAlerts.GetDataRecordByNode(node)
            Dim AlertRuleID As Integer = -1

            If data IsNot Nothing Then
                AlertRuleID = data.Item(0).ToString
            End If

            Dim objFilter As New dlgObjFilter("ANOKPIRULES", kpiRuleID)
            objFilter.AlertRuleID = AlertRuleID
            objFilter.dtAlertConfig = dtAlertConfig.Select("[ALERT_RULEID]=" & AlertRuleID).CopyToDataTable
            objFilter.ShowDialog()

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadKpiRulesFilter(kpiRuleID)
            gcKpiRulesFilter.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteFilter_Click(sender As Object, e As EventArgs) Handles btnDeleteFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (gvKpiRulesFilter.SelectedRowsCount > 0) Then
                Dim filterName As String = gvKpiRulesFilter.GetFocusedRowCellValue("FilterString")
                Dim filterID As Integer = gvKpiRulesFilter.GetFocusedRowCellValue("FilterID")

                If XtraMessageBox.Show("Are you sure to delete filter: " & filterName & "?", "Delete Filter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteKpiRulesFilter(filterID)

                    If gvKPIRules.RowCount > 0 Then
                        LoadKpiRulesFilter(CInt(gvKPIRules.GetFocusedRowCellValue("KPI_RULEID")))
                    End If
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

    Private Sub btnCopyFromFilter_Click(sender As Object, e As EventArgs) Handles btnCopyFromFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objCopyFromAlert As New dlgCopyFromAlert()
            objCopyFromAlert.ShowDialog()

            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewAlerts.FocusedNode
            Dim data As DataRowView = lstviewAlerts.GetDataRecordByNode(node)
            Dim TrgAlertRuleID As Integer = -1

            If data IsNot Nothing Then
                TrgAlertRuleID = data.Item(0).ToString
            End If

            If AlertCopyFromCommitted = True Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@SrcAlertRuleID", Me.copyFromSrcAlertRuleID},
                    New String() {"@TrgAlertRuleID", TrgAlertRuleID},
                    New String() {"@SrcKpiRuleID", Me.copyFromSrcKpiRuleID},
                    New String() {"@TrgKpiRuleID", CInt(gvKPIRules.GetFocusedRowCellValue("KPI_RULEID"))},
                    New String() {"@ObjStaticFilter", IIf(Me.copyFilterStringsFromKpiRule = True, 1, "NULL")}
                }
                strConnection = GetSQL(3858, parray)(0)
                sqlParam = GetSQL(3858, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                If copyFilterStringsFromKpiRule = True Then
                    Dim dr As DataRow = gvKPIRules.GetFocusedDataRow()
                    LoadKpiRulesFilter(CInt(dr("KPI_RULEID")))
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvKpiRulesFilter_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvKpiRulesFilter.ShowingEditor
        Try
            If (gvKpiRulesFilter.FocusedColumn().FieldName = "FilterString") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvKpiRulesFilter_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvKpiRulesFilter.CellValueChanged
        Try
            Dim modifiedFilterStr As String = Nothing
            If e.Column.FieldName.ToUpper = "FILTERSTRING" Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim data As DataRow = gvKpiRulesFilter.GetFocusedDataRow()
                If data IsNot Nothing Then

                    If data.Item("FilterString").ToString.ToLower.Contains("in") Or data.Item("FilterString").ToString.ToLower.Contains("not in") Then
                        modifiedFilterStr = data.Item("FilterString").ToString '.Replace("'", "''")
                    Else
                        modifiedFilterStr = data.Item("FilterString").ToString
                    End If

                    'Update kpi rule filter string
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@FilterID", CInt(data.Item("FilterID"))},
                        New String() {"@FilterString", Chr(39) & Replace(modifiedFilterStr, Chr(39), Chr(39) & Chr(39)) & Chr(39)}
                    }
                    strConnection = GetSQL(3859, parray)(0)
                    sqlParam = GetSQL(3859, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
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

#End Region

#Region "Chart Event & Method"

    Private Sub chAlert_DragDrop(sender As Object, e As DragEventArgs) Handles chAlert.DragDrop
        Try

            Dim mm() As Object = e.Data.GetData("System.Object[]")
            If mm IsNot Nothing Then
                Dim dr As DataRow = CType(mm(1), DataRow)
                Dim filter As String = ""
                If mm(0) = "KPIRuleDrag" Then
                    filter = "KPI_RULEID = " & dr.Item(5)
                End If
                LoadChart(dtAlertConfig.Rows(0).Item("ALERT_RULEID"), dtAlertConfig.Rows(0).Item("AlertName"), filter)
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub chAlert_MouseClick(sender As Object, e As MouseEventArgs) Handles chAlert.MouseClick
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfoDefault
        System.Threading.Thread.CurrentThread.CurrentUICulture = CultureUIDefault

        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If e.Button = MouseButtons.Left Then
                Dim myChart As Chart = CType(sender, Chart)
                Dim hit As HitTestInfo = myChart.HitTest(e.X, e.Y)
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
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
        'System.Threading.Thread.CurrentThread.CurrentUICulture = Globalization.CultureInfo.GetCultureInfo("en-US")
        'System.Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.GetCultureInfo("en-US")
    End Sub

    Private Sub LoadChart(AlertRuleID As Integer, AlertName As String, ByVal filter As String)
        If txtObjectNameFilter.Text.Trim = "" Then
            XtraMessageBox.Show("Please enter/select object", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtObjectNameFilter.Focus()
            Exit Sub
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                RemoveHandler ceShowHideBreached.CheckedChanged, AddressOf ceShowHideBreached_CheckedChanged
                RemoveHandler ceShowHideOutlier.CheckedChanged, AddressOf ceShowHideOutlier_CheckedChanged

                ceShowHideOutlier.Checked = True
                ceShowHideBreached.Checked = True
                ceShowHideOutlier.Enabled = True

                AddHandler ceShowHideBreached.CheckedChanged, AddressOf ceShowHideBreached_CheckedChanged
                AddHandler ceShowHideOutlier.CheckedChanged, AddressOf ceShowHideOutlier_CheckedChanged

                dtChartData = New DataTable
                Dim ObjectName As String = txtObjectNameFilter.Text.Trim()
                Dim strConnection As String, sqlParam As String
                Dim kpiRuleTypeNameShort As String = ""
                Dim kpiIndex As Integer = 0

                For Each dtRow As DataRow In dtAlertConfig.Select(filter)
                    Dim parray()() As String = {
                        New String() {"@KPIRuleID", dtRow("KPI_RULEID")},
                        New String() {"@ObjectName", "'" & txtObjectNameFilter.Text.Trim & "'"},
                        New String() {"@FilterThreshold", 0},
                        New String() {"@DaysInChart", seDataPoints.EditValue}
                    }

                    strConnection = GetSQL(3824, parray)(0)
                    sqlParam = GetSQL(3824, parray)(1)
                    Dim dtChart As New DataTable
                    dtChart = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                    kpiRuleTypeNameShort = dtRow("KPI_RuleTypeName_Short").ToString
                    Dim colList() As String = Nothing

                    If dtChart IsNot Nothing Then

                        If dtChartData.Columns.Contains(dtRow("KPI_Name").ToString & "_Value_" & kpiRuleTypeNameShort) Then

                            dtChart.Columns("KPIValue").ColumnName = dtRow("KPI_Name").ToString & kpiIndex & "_Value_" & kpiRuleTypeNameShort
                            dtChart.Columns("LowerThresholdLine").ColumnName = dtRow("KPI_Name").ToString & kpiIndex & "_LowerThreshold_" & kpiRuleTypeNameShort
                            dtChart.Columns("UpperThresholdLine").ColumnName = dtRow("KPI_Name").ToString & kpiIndex & "_UpperThreshold_" & kpiRuleTypeNameShort
                            dtChart.Columns("isBreach").ColumnName = dtRow("KPI_Name").ToString & kpiIndex & "_isBreach_" & kpiRuleTypeNameShort

                            If dtChart.Columns.Contains("isOutlier") Then
                                dtChart.Columns("isOutlier").ColumnName = dtRow("KPI_Name").ToString & kpiIndex & "_isOutlier_" & kpiRuleTypeNameShort
                                colList = {
                                "Period_Start_Time", dtRow("KPI_Name").ToString & kpiIndex & "_Value_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & kpiIndex & "_LowerThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & kpiIndex & "_UpperThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & kpiIndex & "_isBreach_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & kpiIndex & "_isOutlier_" & kpiRuleTypeNameShort
                            }
                            Else
                                colList = {
                                "Period_Start_Time", dtRow("KPI_Name").ToString & kpiIndex & "_Value_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & kpiIndex & "_LowerThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & kpiIndex & "_UpperThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & kpiIndex & "_isBreach_" & kpiRuleTypeNameShort
                            }
                            End If

                        Else

                            dtChart.Columns("KPIValue").ColumnName = dtRow("KPI_Name").ToString & "_Value_" & kpiRuleTypeNameShort
                            dtChart.Columns("LowerThresholdLine").ColumnName = dtRow("KPI_Name").ToString & "_LowerThreshold_" & kpiRuleTypeNameShort
                            dtChart.Columns("UpperThresholdLine").ColumnName = dtRow("KPI_Name").ToString & "_UpperThreshold_" & kpiRuleTypeNameShort
                            dtChart.Columns("isBreach").ColumnName = dtRow("KPI_Name").ToString & "_isBreach_" & kpiRuleTypeNameShort

                            If dtChart.Columns.Contains("isOutlier") Then
                                dtChart.Columns("isOutlier").ColumnName = dtRow("KPI_Name").ToString & "_isOutlier_" & kpiRuleTypeNameShort
                                colList = {
                                "Period_Start_Time", dtRow("KPI_Name").ToString & "_Value_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & "_LowerThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & "_UpperThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & "_isBreach_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & "_isOutlier_" & kpiRuleTypeNameShort
                            }
                            Else
                                colList = {
                                "Period_Start_Time", dtRow("KPI_Name").ToString & "_Value_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & "_LowerThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & "_UpperThreshold_" & kpiRuleTypeNameShort,
                                dtRow("KPI_Name").ToString & "_isBreach_" & kpiRuleTypeNameShort
                            }
                            End If

                        End If

                        Dim dtTemp As DataTable = dtChart.DefaultView.ToTable(False, colList)
                        Dim primkeys(1) As DataColumn
                        primkeys(0) = dtTemp.Columns("Period_Start_time")
                        dtTemp.PrimaryKey = primkeys

                        Dim dtMerge As DataTable = dtTemp.Clone
                        For Each col As DataColumn In dtTemp.Columns
                            If col.ColumnName.ToUpper <> "PERIOD_START_TIME" Then
                                dtMerge.Columns(col.ColumnName).DataType = Type.GetType("System.Decimal")
                            End If
                        Next

                        For Each tempRow As DataRow In dtTemp.Rows
                            dtMerge.ImportRow(tempRow)
                        Next

                        dtChartData.Merge(dtMerge)
                        kpiIndex = kpiIndex + 1

                    End If
                Next
                '****************************
                Dim dataView As New DataView(dtChartData)
                dataView.Sort = "PERIOD_START_TIME ASC"
                Dim dtChartData_Sorted As DataTable = dataView.ToTable()

                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcChartAlert, gvChartAlert, dtChartData_Sorted, "ALL")

                Dim parray1()() As String = {
                    New String() {"@AlertId", AlertRuleID},
                    New String() {"@filter", IIf(filter.Length > 0, " AND " + filter, "")}
                }
                strConnection = GetSQL(3815, parray1)(0)
                sqlParam = GetSQL(3815, parray1)(1)
                Dim dt_chart As New DataTable
                dt_chart = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                If dt_chart IsNot Nothing Then

                    If Not dt_chart.Columns.Contains("KPI_RuleTypeName_Short") Then
                        dt_chart.Columns.Add("KPI_RuleTypeName_Short", GetType(String))
                    End If

                    Dim dtRuleType As DataTable = dtAlertConfig.DefaultView.ToTable(True, {"KPI_RULEID", "KPI_Name", "KPI_RuleTypeName_Short"}).Select("", "KPI_Name ASC").CopyToDataTable()
                    Dim index As Integer = 0

                    For Each drKpi As DataRow In dtAlertConfig.DefaultView.ToTable(True, {"KPI_RULEID", "KPI_Name"}).Select("", "KPI_Name ASC")
                        index = 0
                        For Each dr As DataRow In dt_chart.Select("KPI_RULEID='" & drKpi("KPI_RULEID") & "'", "ChartElements ASC")
                            Dim dr1 As DataRow() = dtRuleType.Select("KPI_RULEID='" & drKpi("KPI_RULEID") & "'")
                            If dr1.Length > 0 Then
                                dr("KPI_RuleTypeName_Short") = dr1(index)("KPI_RuleTypeName_Short")
                            End If
                            index = index + 1
                        Next
                    Next

                    AssignDataToCharts_New(chAlert, dtChartData_Sorted, AlertName, ObjectName, dt_chart)

                    'Add Marker to chart if breached
                    AddBreachedAxisMarker()

                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            Finally
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            End Try
        End If
    End Sub

    Private Sub AddBreachedAxisMarker()
        If ceShowHideBreached.Checked Then
            Dim strBreachedCol() As String = Nothing
            Dim conditionIsBreached As String = ""
            ReDim strBreachedCol(dtAlertConfig.Rows.Count)
            strBreachedCol(0) = "Period_Start_Time"

            Dim i As Integer = 1

            For Each col As DataColumn In dtChartData.Columns
                If col.ColumnName.ToLower.Contains("_isbreach") Then
                    strBreachedCol(i) = col.ColumnName
                    If conditionIsBreached.Length = 0 Then
                        conditionIsBreached = "[" & col.ColumnName & "]=1 "
                    Else
                        conditionIsBreached = conditionIsBreached & " AND [" & col.ColumnName & "]=1"
                    End If
                    i = i + 1
                End If
            Next

            If conditionIsBreached <> "" Then
                Dim drBreach() As DataRow = dtChartData.Select(conditionIsBreached)
                For Each Row As DataRow In drBreach
                    Dim am4 As New AxisMarker(Row("Period_Start_Time").ToString, New Line(Color.FromArgb(180, Color.DarkRed), 3), Row("Period_Start_Time"))
                    am4.LegendEntry.Visible = False
                    am4.Label.Hotspot.ToolTip = Row("Period_Start_Time").ToString
                    am4.Label.Color = Color.Empty
                    am4.Label.Alignment = StringAlignment.Near
                    am4.Label.LineAlignment = StringAlignment.Far
                    am4.BringToFront = True
                    chAlert.XAxis.Markers.Add(am4)
                    chAlert.RefreshChart()
                Next
            End If
        End If
    End Sub

    Public Sub AssignDataToCharts_New(ByRef ch As Chart, ByRef dt As DataTable, ByVal alertName As String, ObjectName As String, dt_chart As DataTable)

        DefaultSeriesCollection.Clear()
        ExtraLegendEntryCollection.Clear()
        chAlert.SeriesCollection.Clear()

        Dim KpiValue = "", KpiOutlier = "", KpiUpperThreshold = "", KpiLowerThreshold As String = ""
        Dim objectscharted As String = ""
        Dim i As Integer
        Dim color_R, color_B, color_G As Integer
        Dim chart_elements() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}

        Dim j As Integer = 0
        Dim rownum As Integer = 0

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
        ch.XAxis.FormatString = "dd/MM/yy"
        ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
        ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
        ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
        ch.LegendBox.Orientation = Orientation.Bottom
        ch.LegendBox.ExtraEntries.Clear()

        ch.TitleBox.Label.Alignment = StringAlignment.Near
        ch.TitleBox.Label.LineAlignment = StringAlignment.Near
        ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "

        Dim kpiIdx As Integer = 0

        Try
            For rownum = 0 To dt_chart.Rows.Count - 1
                Dim drow As DataRow = dt_chart.Rows(rownum)

                KpiValue = "_Value_" & drow("KPI_RuleTypeName_Short").ToString
                KpiOutlier = "_IsOutlier_" & drow("KPI_RuleTypeName_Short").ToString
                KpiLowerThreshold = "_LowerThreshold_" & drow("KPI_RuleTypeName_Short").ToString
                KpiUpperThreshold = "_UpperThreshold_" & drow("KPI_RuleTypeName_Short").ToString

                'If chart_elements.Length > 0 Then
                '    If chart_elements.Contains(drow(4).ToString.Trim & KpiValue) Then
                '        Continue For
                '    End If
                'End If

                Try
                    Do While Not ColumnInDataTable(drow(4).ToString.Trim & KpiValue, dt)
                        rownum = rownum + 1
                        If rownum <= dt_chart.Rows.Count - 1 Then
                            drow = dt_chart.Rows(rownum)
                        Else
                            Exit For
                        End If
                    Loop
                Catch ex As Exception
                End Try

                ch.Annotations.Clear()
                ch.Annotations.Add(New Annotation(alertName.ToUpper))

                If alertName.Length > 3 Then
                    Dim fnt As Font = New Font("Arial", 7, FontStyle.Regular)
                    ch.Annotations(0).Label.Font = fnt
                    ch.Annotations(0).DynamicSize = False
                    Dim textSize As Size = TextRenderer.MeasureText(alertName, New System.Drawing.Font("Arial", 9, GraphicsUnit.Point))
                    ch.Annotations(0).Position = New Point(ch.Width - textSize.Width, 2)
                End If

                If ObjectName <> "" Then
                    objectscharted = ObjectName
                End If

                ch.TitleBox.Label.Text = "Objects: " & objectscharted
                ch.TitleBox.HeaderLabel.Text = drow(4).ToString.Trim

                Dim KPIName() As String = Nothing
                Dim iLowerThreshold As Integer = dt.Select("[" & drow(4).ToString.Trim & KpiLowerThreshold & "]" & " <> 0").Count
                Dim iUpperThreshold As Integer = dt.Select("[" & drow(4).ToString.Trim & KpiUpperThreshold & "]" & " <> 0").Count
                Dim iOutlier As Integer = 0

                If dt.Columns.Contains(drow(4).ToString.Trim & KpiOutlier) Then
                    iOutlier = dt.Select("[" & drow(4).ToString.Trim & KpiOutlier & "]" & " <> 0").Count()
                    Dim dtOutlier As DataTable = dt.DefaultView.ToTable(True, {"PERIOD_START_TIME"})
                    For Each nRow As DataRow In dtOutlier.Rows
                        Dim nrowList() As DataRow = dt.Select("PERIOD_START_TIME='" & nRow("PERIOD_START_TIME") & "'")
                        For Index As Integer = 0 To nrowList.Length - 1
                            If IsDBNull(nrowList(Index).Item(drow(4).ToString.Trim & KpiOutlier)) Then Continue For
                            If nrowList(Index).Item(drow(4).ToString.Trim & KpiOutlier) = 1 Then
                                nrowList(Index).Item(drow(4).ToString.Trim & KpiOutlier) = nrowList(Index).Item(drow(4).ToString.Trim & KpiValue)
                            Else
                                nrowList(Index).Item(drow(4).ToString.Trim & KpiOutlier) = DBNull.Value
                            End If
                        Next
                    Next
                End If

                If chart_elements.Contains(drow(4).ToString.Trim & KpiValue) Then

                    iLowerThreshold = dt.Select("[" & drow(4).ToString.Trim & kpiIdx & KpiLowerThreshold & "]" & " <> 0").Count
                    iUpperThreshold = dt.Select("[" & drow(4).ToString.Trim & kpiIdx & KpiUpperThreshold & "]" & " <> 0").Count

                    If (iLowerThreshold = 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue}
                    ElseIf (iLowerThreshold = 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue, drow(4).ToString.Trim & kpiIdx & KpiOutlier}
                    ElseIf (iLowerThreshold = 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue, drow(4).ToString.Trim & kpiIdx & KpiUpperThreshold}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue, drow(4).ToString.Trim & kpiIdx & KpiLowerThreshold}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue, drow(4).ToString.Trim & kpiIdx & KpiLowerThreshold, drow(4).ToString.Trim & kpiIdx & KpiOutlier}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue, drow(4).ToString.Trim & kpiIdx & KpiLowerThreshold, drow(4).ToString.Trim & kpiIdx & KpiUpperThreshold}
                    ElseIf (iLowerThreshold = 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue, drow(4).ToString.Trim & kpiIdx & KpiUpperThreshold, drow(4).ToString.Trim & kpiIdx & KpiOutlier}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & kpiIdx & KpiValue, drow(4).ToString.Trim & kpiIdx & KpiLowerThreshold, drow(4).ToString.Trim & kpiIdx & KpiUpperThreshold, drow(4).ToString.Trim & kpiIdx & KpiOutlier}
                    End If
                Else
                    If (iLowerThreshold = 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue}
                    ElseIf (iLowerThreshold = 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiOutlier}
                    ElseIf (iLowerThreshold = 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiUpperThreshold}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiLowerThreshold}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold = 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiLowerThreshold, drow(4).ToString.Trim & KpiOutlier}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier = 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiLowerThreshold, drow(4).ToString.Trim & KpiUpperThreshold}
                    ElseIf (iLowerThreshold = 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiUpperThreshold, drow(4).ToString.Trim & KpiOutlier}
                    ElseIf (iLowerThreshold > 0) AndAlso (iUpperThreshold > 0) AndAlso (iOutlier > 0) Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiLowerThreshold, drow(4).ToString.Trim & KpiUpperThreshold, drow(4).ToString.Trim & KpiOutlier}
                    End If
                End If

                For index As Integer = 0 To KPIName.Length - 1
                    If ColumnInDataTable(KPIName(index).ToString.Trim, dt) Then
                        ReDim Preserve chart_elements(j)
                        ReDim Preserve chart_Eltype(j)
                        ReDim Preserve chart_ElColor(j)

                        chart_elements(j) = KPIName(index).ToString.Trim
                        If KPIName(index).ToString.Trim.ToLower.Contains("threshold") Then
                            chart_Eltype(j) = "DOTTEDLINE"
                            chart_ElColor(j) = 0
                        ElseIf KPIName(index).ToString.Trim.ToLower.Contains("isoutlier") Then
                            chart_Eltype(j) = "BLUEDOT"
                            chart_ElColor(j) = 1
                        Else
                            chart_Eltype(j) = drow("ChartElementsType").trim
                            chart_ElColor(j) = CInt(drow("ChartElementsColor"))
                        End If

                        j = j + 1
                    End If
                Next

                kpiIdx = kpiIdx + 1
            Next

            Dim xaxis_valuehigh As DateTime = Today
            If dt.Rows.Count > 0 Then
                xaxis_valuehigh = dt.Compute("MAX(Period_Start_Time)", "")
                xaxis_valuehigh = xaxis_valuehigh.AddDays(1) 'Add 1 day to insert a extra x-axis scale.
            End If

            If xaxis_valuehigh.Date = Now.Date Then
                xaxis_valuehigh = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date))
            End If

            ch.XAxis.ScaleRange.ValueHigh = xaxis_valuehigh
            Dim de As DataEngine = New DataEngine(dt)
            de.DataFields = String2DataFields(chart_elements, "PERIOD_START_TIME")
            de.DataGridFormatString = "N2"
            de.FormatString = "dd/MM/yy"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()
            Dim lastKpiName As String = ""
            Dim currentKpiName As String = ""

            Dim yAxis As Axis = Nothing

            Dim kpiIndex As Integer = 0
            For i = 0 To sc.Count() - 1

                Select Case UCase(chart_Eltype(i).Trim)
                    Case "LINE"
                        sc(i).Type = SeriesType.Line
                        sc(i).Line.Width = 3
                    Case "BAR"
                        sc(i).Type = SeriesType.Bar
                    Case "AREALINE"
                        sc(i).Type = SeriesType.AreaLine
                    Case "DOTTEDLINE"
                        sc(i).Type = SeriesType.Line
                        sc(i).Line.Width = 2
                        sc(i).Line.DashStyle = Drawing2D.DashStyle.Dot
                    Case "BLUEDOT"
                        sc(i).Type = SeriesType.Marker
                        sc(i).Element.Marker.Type = ElementMarkerType.Circle
                        sc(i).DefaultElement.Marker.Visible = True
                        sc(i).DefaultElement.ForceMarker = True
                End Select

                currentKpiName = sc(i).Name.Substring(0, sc(i).Name.LastIndexOf("_")).Substring(0, sc(i).Name.Substring(0, sc(i).Name.LastIndexOf("_")).LastIndexOf("_"))
                'Generate Y-Axis for each KPI in the chart
                If lastKpiName <> currentKpiName Then

                    Dim drKPISettings() As DataRow = dt_chart.Select("KPI_Name='" & currentKpiName & "'")
                    If drKPISettings.Length > 0 Then
                        yAxis = New Axis()

                        If UCase(drKPISettings(drKPISettings.Length - 1)("ChartElementsYAxis").trim) = "LEFT" Then
                            yAxis.Orientation = Orientation.Left
                            If nZ(drKPISettings(drKPISettings.Length - 1)("ChartY1AbsPerc"), "Abs").ToUpper = "PERC" Then
                                yAxis.Percent = True
                            End If
                            yAxis.NumberPrecision = CInt(nZ(drKPISettings(drKPISettings.Length - 1)("chartY1AxisPrecision"), "0"))
                        Else
                            yAxis.Orientation = Orientation.Right
                            If nZ(drKPISettings(drKPISettings.Length - 1)("ChartY2AbsPerc"), "Abs").ToUpper = "PERC" Then
                                yAxis.Percent = True
                            End If
                            yAxis.NumberPrecision = CInt(nZ(drKPISettings(drKPISettings.Length - 1)("chartY2AxisPrecision"), "0"))
                        End If

                        If UCase(drKPISettings(drKPISettings.Length - 1)("ChartElementsType").trim) = "STACKED" Then
                            yAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                        ElseIf UCase(drKPISettings(drKPISettings.Length - 1)("ChartElementsType").trim) = "FULLSTACKED" Then
                            yAxis.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                        Else
                            yAxis.Scale = dotnetCHARTING.WinForms.Scale.Range
                        End If

                        yAxis.Label.Text = currentKpiName

                        If yAxis.NumberPrecision < 2 And Not yAxis.Percent = True Then
                            yAxis.MinimumInterval = 1
                        End If
                    End If
                    kpiIndex = kpiIndex + 1
                End If

                lastKpiName = currentKpiName
                sc(i).YAxis = yAxis
                'sc(i).XAxis = ch.XAxis

                If chart_ElColor(i) = 0 Then
                    sc(i).DefaultElement.Color = Color.Black
                ElseIf chart_ElColor(i) = 1 Then
                    sc(i).DefaultElement.Color = Color.Blue
                Else
                    color_R = CLng(chart_ElColor(i)) Mod 256
                    color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                    color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256
                    sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
                End If

                sc(i).DefaultElement.Marker.Type = i

                If DefaultSeriesCollection.ContainsKey(sc(i).Name) Then
                    DefaultSeriesCollection.Remove(sc(i).Name)
                End If
                DefaultSeriesCollection.Add(sc(i).Name, sc(i))
            Next

            ch.SeriesCollection.Clear()
            ch.SeriesCollection.Add(sc)

            sc = Nothing
            de = Nothing
            ch.XAxis.Markers.Clear()

            ReDim chart_elements(0)
            ReDim chart_Eltype(0)
            ReDim chart_ElColor(0)
            j = 0

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Console.WriteLine(ex.Message.ToString)
        Finally
            ch.RefreshChart()
            ch.ResumeLayout()
        End Try

        mm.ReleaseMemory()
        dt_chart.Dispose()
        dt_chart = Nothing
        System.GC.Collect()
    End Sub

    Private Sub chAlert_SizeChanged(sender As Object, e As EventArgs) Handles chAlert.SizeChanged
        Try
            If chAlert.Annotations.Count > 0 Then
                Dim textSize As Size = TextRenderer.MeasureText(chAlert.Annotations(0).Label.Text, New System.Drawing.Font("Arial", 9, GraphicsUnit.Point))
                chAlert.Annotations(0).Position = New Point(chAlert.Width - textSize.Width, 2)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ShowHideChartSeries(ByRef chart As Chart, ByVal SeriesName As String)
        If chart.SeriesCollection.GetSeries(SeriesName) IsNot Nothing Then
            If chart.SeriesCollection.Count > 1 Then

                chart.SeriesCollection.Remove(chart.SeriesCollection.GetSeries(SeriesName))

                Dim entry As New LegendEntry()
                entry.Name = SeriesName
                entry.LabelStyle.Color = Color.Gray
                entry.Hotspot.ToolTip = SeriesName
                chart.LegendBox.ExtraEntries.Add(entry)

                If ExtraLegendEntryCollection.ContainsKey(SeriesName) Then
                    ExtraLegendEntryCollection.Remove(SeriesName)
                End If
                ExtraLegendEntryCollection.Add(SeriesName, entry)
            End If
            If SeriesName.ToLower.Contains("isoutlier") Then
                RemoveHandler ceShowHideOutlier.CheckedChanged, AddressOf ceShowHideOutlier_CheckedChanged
                ceShowHideOutlier.Checked = False
                AddHandler ceShowHideOutlier.CheckedChanged, AddressOf ceShowHideOutlier_CheckedChanged
            End If
        Else
            If ExtraLegendEntryCollection.ContainsKey(SeriesName) Then
                ExtraLegendEntryCollection.Remove(SeriesName)
                chart.LegendBox.ExtraEntries.Clear()
                chart.SeriesCollection.Clear()
                For Each obj As KeyValuePair(Of String, Series) In DefaultSeriesCollection
                    If ExtraLegendEntryCollection.ContainsKey(obj.Key) = False Then
                        chart.SeriesCollection.Add(obj.Value)
                    End If
                Next

                For Each obj As KeyValuePair(Of String, LegendEntry) In ExtraLegendEntryCollection
                    chart.LegendBox.ExtraEntries.Add(obj.Value)
                Next

                If SeriesName.ToLower.Contains("isoutlier") Then
                    RemoveHandler ceShowHideOutlier.CheckedChanged, AddressOf ceShowHideOutlier_CheckedChanged
                    ceShowHideOutlier.Checked = True
                    AddHandler ceShowHideOutlier.CheckedChanged, AddressOf ceShowHideOutlier_CheckedChanged
                End If
            End If
        End If
        If chart.SeriesCollection.Count = 1 AndAlso ceShowHideOutlier.Checked Then
            ceShowHideOutlier.Enabled = False
        Else
            ceShowHideOutlier.Enabled = True
        End If
        chart.Refresh()
    End Sub

    Private Sub ceShowHideBreached_CheckedChanged(sender As Object, e As EventArgs) 'Handles ceShowHideBreached.CheckedChanged
        Try
            If ceShowHideBreached.Checked Then
                AddBreachedAxisMarker()
            Else
                chAlert.XAxis.Markers.Clear()
                chAlert.RefreshChart()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ceShowHideOutlier_CheckedChanged(sender As Object, e As EventArgs) 'Handles ceShowHideOutlier.CheckedChanged
        Try
            Dim sc As SeriesCollection = chAlert.SeriesCollection
            If ceShowHideOutlier.Checked Then
                For Each strOulier As String In seriesColl
                    chAlert.SeriesCollection.Add(DefaultSeriesCollection(strOulier))
                    If ExtraLegendEntryCollection.ContainsKey(strOulier) Then
                        ExtraLegendEntryCollection.Remove(strOulier)
                        chAlert.LegendBox.ExtraEntries.Clear()

                        For Each obj As KeyValuePair(Of String, LegendEntry) In ExtraLegendEntryCollection
                            chAlert.LegendBox.ExtraEntries.Add(obj.Value)
                        Next
                    End If
                Next
            Else
                If sc.Count = 1 Then
                    ceShowHideOutlier.Checked = True
                    Exit Sub
                End If
                For Each _series As Series In sc
                    If _series.Name.ToLower.Contains("isoutlier") Then
                        seriesColl = New String() {_series.Name}
                    End If
                Next
                If seriesColl IsNot Nothing Then
                    For Each strCoulier As String In seriesColl
                        chAlert.SeriesCollection.Remove(chAlert.SeriesCollection.GetSeries(strCoulier))
                    Next
                End If
            End If
            chAlert.RefreshChart()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

#End Region

End Class

Public Class KPIRuleProperties

    Private _inputDataPeriodInterval As String
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Input Data Period Interval"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property InputDataPeriodInterval() As String
        Get
            Return _inputDataPeriodInterval
        End Get
        Set(ByVal Value As String)
            _inputDataPeriodInterval = Value
        End Set
    End Property

    Private _inputDataSlidingWindow As Integer
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Input Data Sliding Window"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property InputDataSlidingWindow() As Integer
        Get
            Return _inputDataSlidingWindow
        End Get
        Set(ByVal Value As Integer)
            _inputDataSlidingWindow = Value
        End Set
    End Property

    Private _sigmaFilterOutliers As Double
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Sigma Filter Outliers"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property SigmaFilterOutliers() As Double
        Get
            Return _sigmaFilterOutliers
        End Get
        Set(ByVal Value As Double)
            _sigmaFilterOutliers = Value
        End Set
    End Property

    Private _occurencesThreshold As Double
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Occurences Threshold"), DefaultValueAttribute(0), Browsable(False), Description("")>
    Public Property OccurencesThreshold() As String
        Get
            Return _occurencesThreshold
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _occurencesThreshold = Value
                Else
                    XtraMessageBox.Show("Occurences Threshold accepts only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _occurencesThreshold = Nothing
                End If
            Else
                _occurencesThreshold = Value
            End If
        End Set
    End Property

    Private _occurencesSlidingWindow As Integer
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Occurences Sliding Window"), DefaultValueAttribute(0), Browsable(False), Description("")>
    Public Property OccurencesSlidingWindow() As Integer
        Get
            Return _occurencesSlidingWindow
        End Get
        Set(ByVal Value As Integer)
            _occurencesSlidingWindow = Value
        End Set
    End Property

    Private _inputdataMatchDays As Integer
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Input Data Match Days"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property InputdataMatchDays() As Integer
        Get
            Return _inputdataMatchDays
        End Get
        Set(ByVal Value As Integer)
            _inputdataMatchDays = Value
        End Set
    End Property

    Private _InputdataMatchHours As Integer
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Input Data Match Hour"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property InputdataMatchHours() As Integer
        Get
            Return _InputdataMatchHours
        End Get
        Set(ByVal Value As Integer)
            _InputdataMatchHours = Value
        End Set
    End Property

    Private _fixedLowerThreshold As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Fixed Lower Threshold"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property FixedLowerThreshold() As String
        Get
            Return _fixedLowerThreshold
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _fixedLowerThreshold = Value
                Else
                    XtraMessageBox.Show("Fixed Lower Threshold accepts only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _fixedLowerThreshold = Nothing
                End If
            Else
                _fixedLowerThreshold = Value
            End If
        End Set
    End Property

    Private _fixedUpperThreshold As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Fixed Upper Threshold"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property FixedUpperTreshold() As String
        Get
            Return _fixedUpperThreshold
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _fixedUpperThreshold = Value
                Else
                    XtraMessageBox.Show("Fixed Upper Threshold accept only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _fixedUpperThreshold = Nothing
                End If
            Else
                _fixedUpperThreshold = Value
            End If
        End Set
    End Property

    Private _percLowerThreshold As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Perc Lower Threshold"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property PercLowerTreshold() As String
        Get
            Return _percLowerThreshold
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _percLowerThreshold = Value
                Else
                    XtraMessageBox.Show("Perc Lower Threshold accept only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _percLowerThreshold = Nothing
                End If
            Else
                _percLowerThreshold = Value
            End If
        End Set
    End Property

    Private _percUpperThreshold As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Perc Upper Threshold"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property PercUpperTreshold() As String
        Get
            Return _percUpperThreshold
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _percUpperThreshold = Value
                Else
                    XtraMessageBox.Show("Perc Upper Threshold accept only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _percUpperThreshold = Nothing
                End If
            Else
                _percUpperThreshold = Value
            End If
        End Set
    End Property

    Private _zScoreLowerThreshold As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Z Score Lower Threshold"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property ZScoreLowerTreshold() As String
        Get
            Return _zScoreLowerThreshold
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _zScoreLowerThreshold = Value
                Else
                    XtraMessageBox.Show("Z Score Lower Threshold accept only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _zScoreLowerThreshold = Nothing
                End If
            Else
                _zScoreLowerThreshold = Value
            End If
        End Set
    End Property

    Private _zScoreUpperThreshold As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Z Score Upper Threshold"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property ZScoreUpperTreshold() As String
        Get
            Return _zScoreUpperThreshold
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _zScoreUpperThreshold = Value
                Else
                    XtraMessageBox.Show("Z Score Upper Threshold accept only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _zScoreUpperThreshold = Nothing
                End If
            Else
                _zScoreUpperThreshold = Value
            End If
        End Set
    End Property

    Private _Px As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Px - Choose x"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property Px() As String
        Get
            Return _Px
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _Px = Value
                Else
                    XtraMessageBox.Show("Px accept only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _Px = Nothing
                End If
            Else
                _Px = Value
            End If
        End Set
    End Property

    Private _PxOperator As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Px Operator"), DefaultValueAttribute(True), Browsable(False), Description("")>
    Public Property PxOperator() As String
        Get
            Return _PxOperator
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If Value = ">" Or Value = "<" Then
                    _PxOperator = Value
                Else
                    XtraMessageBox.Show("PxOperator accept only '<' or '>'.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _PxOperator = Nothing
                End If
            Else
                _PxOperator = Value
            End If
        End Set
    End Property

    Private _ExcNightTimes As String = Nothing
    <CategoryAttribute("KPI Rule Properties"), DisplayName("Exclude Night Times"), DefaultValueAttribute(0), Browsable(False), Description("")>
    Public Property ExcludeNightTimes() As String
        Get
            Return _ExcNightTimes
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then
                If IsNumeric(Value) Then
                    _ExcNightTimes = Value
                Else
                    XtraMessageBox.Show("Exclude Night Time accept only numeric value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _ExcNightTimes = Nothing
                End If
            Else
                _ExcNightTimes = Value
            End If
        End Set
    End Property
End Class