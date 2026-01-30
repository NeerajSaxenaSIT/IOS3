Imports DevExpress.XtraEditors
Imports System.Threading
Imports IOS.DataLibrary

Public Class dlgAddKPIAlertMngr

#Region "Variables"

	Private dtKPI As DataTable = Nothing
	Private dtObj As DataTable = Nothing
	'Private dtKpiRuleTypeFields As DataTable = Nothing

	Public defTech As String = Nothing
	Public defObjectType As String = Nothing
	Public defTarget As String = Nothing
	Public defKPIRuleType As Integer = Nothing
	Public AlertRuleID As Integer = Nothing

#End Region

#Region "Events"

	Private Sub dlgAddKPIAlertMngr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Try
			Me.Cursor = Cursors.WaitCursor
			Application.DoEvents()

			LoadTechnology()
			If Me.defTech IsNot Nothing Then
				SetComboBox(cmbTechnology, ComboSelectBased.TextBased, defTech)
			End If
			LoadKPIRuleType()
			If Me.defKPIRuleType <> Nothing Then
				SetComboBox(cmbMethod, ComboSelectBased.ValueBased, defKPIRuleType)
			End If
			cmbInterval.SelectedIndex = 0
		Catch ex As Exception
			UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
			_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
		End Try
		Me.Cursor = Cursors.Default
		Application.DoEvents()
	End Sub

	Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cmbTechnology.SelectedIndexChanged
		Try
			Me.Cursor = Cursors.WaitCursor
			Application.DoEvents()
			If cmbTechnology.SelectedIndex > 0 Then
				lstviewKPI.DataSource = Nothing
				lstviewKPI.Columns.Clear()

				LoadCounterType()
				GetObjectDataFromTech()
				If Me.defObjectType IsNot Nothing Then
					Dim vendorObj As String = dtObj.AsEnumerable().Where(Function(x) x.Field(Of String)("CommonObject") = defObjectType)(0)("VendorObject")
					SetComboBox(cmbObject, ComboSelectBased.TextBased, vendorObj)
				End If
				cmbObject_SelectedIndexChanged(Nothing, Nothing)
			End If
		Catch ex As Exception
			UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
			_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
		Finally
			Me.Cursor = Cursors.Default
			Application.DoEvents()
		End Try
	End Sub

	Private Sub cmbObject_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles cmbObject.SelectedIndexChanged
		Try
			Me.Cursor = Cursors.WaitCursor
			Application.DoEvents()
			If cmbObject.SelectedIndex > -1 Then
				LoadKPIs()
				LoadTargetType()
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
			'If xtraMessageBox.Show("Are you sure to add new kpi rule?", "Add New Kpi Rule", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
			AddKpiToKpiRules()
			'End If
		Catch ex As Exception
			UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
			_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
		Finally
			Me.Cursor = Cursors.Default
			Application.DoEvents()
		End Try
	End Sub

	Private Sub txtSearckKPI_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearckKPI.KeyUp
		Try
			If dtKPI IsNot Nothing Then
				If (txtSearckKPI.Text.Length > 0) Then
					dtKPI.DefaultView.RowFilter = "KPI_Name Like '%" + txtSearckKPI.Text + "%'"
				Else
					dtKPI.DefaultView.RowFilter = ""
				End If
			End If
		Catch ex As Exception
			UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
			_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
		End Try
	End Sub

#End Region

#Region "Methods"

	Private Sub LoadTechnology()
		RemoveHandler cmbTechnology.SelectedIndexChanged, AddressOf cmbTechnology_SelectedIndexChanged

		Dim strConnection As String = Nothing
		Dim sqlParam As String = Nothing
		Dim parray()() As String = Nothing
		strConnection = GetSQL(3817, parray)(0)
		sqlParam = GetSQL(3817, parray)(1)

		Dim dtTech As New DataTable()
		dtTech = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
		BindDevExComboBoxWithValueMember(cmbTechnology, dtTech, "VendorTech", "VendorTech", "Select Technology", True)

		AddHandler cmbTechnology.SelectedIndexChanged, AddressOf cmbTechnology_SelectedIndexChanged
	End Sub

	Private Sub LoadCounterType()
		RemoveHandler cmbObject.SelectedIndexChanged, AddressOf cmbObject_SelectedIndexChanged

		Dim strConnection As String = Nothing
		Dim sqlParam As String = Nothing
		Dim parray()() As String = {New String() {"@selectedTech", "'" & cmbTechnology.Text.Trim & "'"}}
		strConnection = GetSQL(3800, parray)(0)
		sqlParam = GetSQL(3800, parray)(1)
		Dim dtObject As New DataTable()
		dtObject = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
		BindDevExComboBoxWithValueMember(cmbObject, dtObject, "VendorObject", "VendorObject", , True)

		AddHandler cmbObject.SelectedIndexChanged, AddressOf cmbObject_SelectedIndexChanged
	End Sub

	Private Sub GetObjectDataFromTech()
		'Get ObjectType & ObjectReported from tech...
		Dim strConnection As String = Nothing
		Dim sqlParam As String = Nothing
		Dim parray1()() As String = {
			New String() {"@VendorTech", "'" & cmbTechnology.Text.Trim & "'"}
		}
		strConnection = GetSQL(3836, parray1)(0)
		sqlParam = GetSQL(3836, parray1)(1)

		dtObj = New DataTable()
		dtObj = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
	End Sub

	Private Sub LoadKPIRuleType()
		Dim strConnection As String = Nothing
		Dim sqlParam As String = Nothing
		Dim parray()() As String = Nothing
		strConnection = GetSQL(3819, parray)(0)
		sqlParam = GetSQL(3819, parray)(1)
		Dim dt As New DataTable()
		dt = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

		BindDevExComboBoxWithValueMember(cmbMethod, dt, "KPI_RuleType", "KPI_RuleTypeName", "Select Method", True)
	End Sub

	Private Sub LoadKPIs()
		Dim strConnection As String = Nothing
		Dim sqlParam As String = Nothing
		Dim parray()() As String = {
			New String() {"@selectedTech", "'" & cmbTechnology.Text.Trim & "'"},
			New String() {"@selectedObject", "'" & cmbObject.Text.Trim & "'"}
		}
		strConnection = GetSQL(3801, parray)(0)
		sqlParam = GetSQL(3801, parray)(1)

		dtKPI = New DataTable()
		dtKPI = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

		lstviewKPI.DataSource = Nothing
		lstviewKPI.Columns.Clear()
		lstviewKPI.DataSource = dtKPI
		lstviewKPI.BestFitColumns()
		lstviewKPI.Columns(0).Visible = False
	End Sub

	Private Sub LoadTargetType()
		'Populate Target Type combo
		Dim strConnection As String = Nothing
		Dim sqlParam As String = Nothing
		Dim parray()() As String = {
			New String() {"@selectedTech", "'" & cmbTechnology.Text.Trim & "'"},
			New String() {"@selectedObject", "'" & cmbObject.Text.Trim & "'"}
		}
		strConnection = GetSQL(3841, parray)(0)
		sqlParam = GetSQL(3841, parray)(1)

		Dim dtTarget As New DataTable()
		dtTarget = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
		BindDevExComboBoxWithValueMember(cmbTarget, dtTarget, "CommonReportedObject", "CommonReportedObject", , True)
		If Me.defTarget IsNot Nothing Then
			SetComboBox(cmbTarget, ComboSelectBased.TextBased, defTarget)
		End If
	End Sub

	Private Sub AddKpiToKpiRules(Optional dtKpi As DataRowView = Nothing)
		Dim alertRuleID As Integer = Me.AlertRuleID
		Dim kpiSqlID As Integer = 0
		Dim kpiRuleType As Integer = 0
		Dim technology As String = ""
		Dim objectType As String = ""
		Dim objectReported As String = ""

		If dtKpi IsNot Nothing Then
			kpiSqlID = dtKpi.Item(0).ToString
		Else
			Dim nd As DevExpress.XtraTreeList.Nodes.TreeListNode = lstviewKPI.FocusedNode
			Dim dataKpi As DataRowView = lstviewKPI.GetDataRecordByNode(nd)

			If dataKpi IsNot Nothing Then
				kpiSqlID = dataKpi.Item(0).ToString
			End If
		End If

		'Dim dataKpiRules As DataRow = gvKPIRules.GetDataRow(gvKPIRules.FocusedRowHandle)
		kpiRuleType = CType(cmbMethod.SelectedItem, IOS.Library.clsComboBoxItem).Value

		'If dataKpiRules Is Nothing Then
		'	technology = cmbTechnology.Text.ToUpper
		'	objectType = dtObj.Select("VendorObject = '" + cmbObject.Text + "'")(0)("CommonObject").ToString
		'	objectReported = cmbTarget.Text.Trim
		'Else
		technology = cmbTechnology.Text.ToUpper
		objectType = dtObj.Select("VendorObject = '" + cmbObject.Text + "'")(0)("CommonObject").ToString
		objectReported = cmbTarget.Text.Trim

		If (defTarget IsNot Nothing) AndAlso (defTarget <> objectReported) Then
			XtraMessageBox.Show("Object reported other than " & defTarget & ", not allowed for an Alert", "Add KPI to KPI Rule", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If
		'End If

		'Adding KPI through a separate thread...
		Me.Cursor = Cursors.WaitCursor
		lblMessage.Text = "Adding KPI..."
		Application.DoEvents()

		Dim objAddKpi As New AddKPIClass()
		objAddKpi.AlertRuleID = alertRuleID
		objAddKpi.AddKpiStatus = 1
		objAddKpi.kpiSqlID = kpiSqlID
		objAddKpi.kpiRuleType = kpiRuleType
		objAddKpi.technology = technology
		objAddKpi.objectType = objectType
		objAddKpi.objectReported = objectReported
		objAddKpi.lc = lblMessage
		objAddKpi.Interval = cmbInterval.Text.ToUpper
		AddHandler objAddKpi.ThreadComplete, AddressOf frmMDI.ExecuteAfteAddKPiThreadComplete
		objThreadAddKPI = New Thread(AddressOf objAddKpi.AddKPI)
		objThreadAddKPI.Start()
	End Sub

#End Region

End Class