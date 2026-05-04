Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmThresholdSetCreate

#Region "Variables"

    Private riCmbOperator As RepositoryItemComboBox
    Private ricmbThresholdCalc As RepositoryItemComboBox
    Private dtPMThresholdSetList As DataTable = Nothing
    Private dtTargetType As DataSet = Nothing

    Public thresholdSetTech As String = Nothing
    Public thresholdSetVendor As String = Nothing
    Public newThresholdSetName As String = Nothing
    Public thresholdSetID As Integer = Nothing
    Public thresholdTargetType As String = Nothing
    Public thresholdObjects As String = Nothing

#End Region

#Region "Events"

    Private Sub frmThresholdSetCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            RemoveHandler dtEditStartTime.EditValueChanged, AddressOf dtEditStartTime_EditValueChanged
            RemoveHandler dtEditEndTime.EditValueChanged, AddressOf dtEditEndTime_EditValueChanged
            RemoveHandler cmbPredefTimeStats.SelectedIndexChanged, AddressOf cmbPredefTimeStats_SelectedIndexChanged
            RemoveHandler cmbPredefinedFilterEval.SelectedIndexChanged, AddressOf cmbPredefinedFilterEval_SelectedIndexChanged

            LoadThresholdSetList()
            BindComboWithPredefinedPeriod(cmbPredefTimeStats)
            LoadKPISetList()
            RemoveHandler cmbThresholdSetDateList.SelectedIndexChanged, AddressOf cmbThresholdSetDateList_SelectedIndexChanged
            LoadThresholdSet_DateLists()
            AddHandler cmbThresholdSetDateList.SelectedIndexChanged, AddressOf cmbThresholdSetDateList_SelectedIndexChanged
            GetThresholdSet_CalculationList()
            BindComboWithPredefinedPeriod(cmbPredefinedFilterEval)
            dtTargetType = clsSQLCommands.GetObjectConfigurationData(connStrIOSServer, thresholdSetTech, thresholdSetVendor)
            BindComboWithTargetType(dtTargetType.Tables(0), cmbTargetType, thresholdSetTech)
            SetComboBox(cmbThresholdSets, ComboSelectBased.ValueBased, thresholdSetID)
            Me.newThresholdSetName = cmbThresholdSets.Text

            'Dim thresholdSetDateListiD As Integer = IIf(IsDBNull(dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID)(0)("ThresholdDateListID")), 0, dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID)(0)("ThresholdDateListID"))
            'If thresholdSetDateListiD <> 0 Then
            '    SetComboBox(cmbThresholdSetDateList, ComboSelectBased.ValueBased, thresholdSetDateListiD)
            'Else
            '    cmbThresholdSetDateList.SelectedIndex = 0
            'End If
            'txtThresholdTarget.Text = Me.thresholdTargetType
            'txtThresholdObjects.Text = Me.thresholdObjects

            AddHandler dtEditStartTime.EditValueChanged, AddressOf dtEditStartTime_EditValueChanged
            AddHandler dtEditEndTime.EditValueChanged, AddressOf dtEditEndTime_EditValueChanged
            AddHandler cmbPredefTimeStats.SelectedIndexChanged, AddressOf cmbPredefTimeStats_SelectedIndexChanged
            AddHandler cmbPredefinedFilterEval.SelectedIndexChanged, AddressOf cmbPredefinedFilterEval_SelectedIndexChanged

            cmbKPISets_SelectedIndexChanged(Nothing, Nothing)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim objThresholdSetCreate As New dlgThresholdSetCreate()
            objThresholdSetCreate.thresholdSetTech = Me.thresholdSetTech
            objThresholdSetCreate.dtTargetType = Me.dtTargetType.Tables(0)
            objThresholdSetCreate.defTargetType = Me.thresholdTargetType
            objThresholdSetCreate.ShowDialog()
            If objThresholdSetCreate.DialogResult = DialogResult.OK Then
                LoadThresholdSetList()
                RemoveHandler cmbThresholdSets.SelectedIndexChanged, AddressOf cmbThresholdSets_SelectedIndexChanged
                SetComboBox(cmbThresholdSets, ComboSelectBased.TextBased, Me.newThresholdSetName)
                AddHandler cmbThresholdSets.SelectedIndexChanged, AddressOf cmbThresholdSets_SelectedIndexChanged
                cmbThresholdSets_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim _thresholdSetID As Integer = Nothing
            Dim isPowerUser As Boolean = False

            If cmbThresholdSets.SelectedIndex > 0 Then
                _thresholdSetID = CInt(TryCast(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)
                Dim owner As String = dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = _thresholdSetID)(0)("Owner").ToString
                If owner.ToLower <> Environment.UserName.ToLower Then
                    If configMgr.User.IsPowerUser = True Then
                        isPowerUser = True
                    Else
                        XtraMessageBox.Show("Current user can't delete the Threshold Set as the owner is a different user.", "Delete Threshold Set!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    isPowerUser = True
                End If
                If isPowerUser = True Then
                    If XtraMessageBox.Show("Are you sure to delete Threshold Set: " & cmbThresholdSets.SelectedItem.ToString & "?", "Delete Threshold Set!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Me.newThresholdSetName = Nothing
                        DeleteThresholdSet(_thresholdSetID)
                        LoadThresholdSetList()
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbThresholdSets_SelectedIndexChanged(sender As Object, e As EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            gcThreshold.DataSource = Nothing
            gvThreshold.Columns.Clear()

            RemoveHandler dtEditStartTime.EditValueChanged, AddressOf dtEditStartTime_EditValueChanged
            RemoveHandler dtEditEndTime.EditValueChanged, AddressOf dtEditEndTime_EditValueChanged
            RemoveHandler cmbPredefTimeStats.SelectedIndexChanged, AddressOf cmbPredefTimeStats_SelectedIndexChanged
            RemoveHandler cmbPredefinedFilterEval.SelectedIndexChanged, AddressOf cmbPredefinedFilterEval_SelectedIndexChanged

            If cmbThresholdSets.SelectedIndex > 0 Then
                Me.newThresholdSetName = cmbThresholdSets.Text
                thresholdSetID = CInt(TryCast(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)
                Dim drow() As DataRow = dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID).ToArray()
                lblMethodName.Text = drow(0)("ThresholdMethodName")
                lblMethodDesc.Text = IIf(IsDBNull(drow(0)("ThresholdDescription")), "", drow(0)("ThresholdDescription"))

                If Not IsDBNull(drow(0)("ThresholdTargetType")) Then
                    SetComboBox(cmbTargetType, ComboSelectBased.TextBased, drow(0)("ThresholdTargetType").ToString)
                Else
                    SetComboBox(cmbTargetType, ComboSelectBased.TextBased, "PLMN")
                End If

                txtThresholdObjects.Text = IIf(IsDBNull(drow(0)("ThresholdObject")), "", drow(0)("ThresholdObject"))
                SetComboBox(cmbPredefTimeStats, ComboSelectBased.ValueBased, IIf(IsDBNull(drow(0)("ThresholdPredefinedIntervalID")), 0, drow(0)("ThresholdPredefinedIntervalID")))
                dtEditStartTime.EditValue = IIf(IsDBNull(drow(0)("ThresholdStartDate")), Now(), drow(0)("ThresholdStartDate"))
                dtEditEndTime.EditValue = IIf(IsDBNull(drow(0)("ThresholdEndDate")), Now(), drow(0)("ThresholdEndDate"))

                If cmbPredefTimeStats.SelectedIndex = 0 Then
                    dtEditStartTime.Enabled = True
                    dtEditEndTime.Enabled = True
                Else
                    dtEditStartTime.Enabled = False
                    dtEditEndTime.Enabled = False
                End If

                LoadGridForThresholdSet()
                RemoveHandler cmbThresholdSetDateList.SelectedIndexChanged, AddressOf cmbThresholdSetDateList_SelectedIndexChanged
                LoadThresholdSet_DateLists()
                Dim thresholdSetDateListID As Integer = IIf(IsDBNull(dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID)(0)("ThresholdDateListID")), 0, dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID)(0)("ThresholdDateListID"))
                If thresholdSetDateListID <> 0 Then
                    SetComboBox(cmbThresholdSetDateList, ComboSelectBased.ValueBased, thresholdSetDateListID)
                Else
                    cmbThresholdSetDateList.SelectedIndex = 0
                End If
                AddHandler cmbThresholdSetDateList.SelectedIndexChanged, AddressOf cmbThresholdSetDateList_SelectedIndexChanged

                Dim periodFilterID As Integer = IIf(IsDBNull(dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID)(0)("PeriodFilterID")), 0, dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID)(0)("PeriodFilterID"))
                If periodFilterID <> 0 Then
                    SetComboBox(cmbPredefinedFilterEval, ComboSelectBased.ValueBased, periodFilterID)
                Else
                    cmbPredefinedFilterEval.SelectedIndex = 0
                End If

                txtThresholdObjects.Text = thresholdObjects.Replace("IN ()", "").Replace("IN ('", "").Replace("')", "").Replace("'", "")

                btnDelete.Enabled = False
                grpDateListBased.Enabled = False
                grpTarget.Enabled = False
                grpPeriodSelection.Enabled = False

                Dim isPowerUser As Boolean = False
                Dim owner As String = dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = thresholdSetID)(0)("Owner").ToString
                If owner.ToLower <> Environment.UserName.ToLower Then
                    If configMgr.User.IsPowerUser = True Then
                        isPowerUser = True
                    End If
                Else
                    isPowerUser = True
                End If

                If lblMethodName.Text.Trim.ToLower.Contains("fixed_object") Then
                    If isPowerUser Then
                        btnDelete.Enabled = True
                        grpTarget.Enabled = True
                        grpPeriodSelection.Enabled = True
                        grpDateListBased.Enabled = False
                    End If
                ElseIf lblMethodName.Text.Trim.ToLower.Contains("itself") Then
                    If isPowerUser Then
                        btnDelete.Enabled = True
                        grpTarget.Enabled = False
                        grpPeriodSelection.Enabled = True
                        grpDateListBased.Enabled = False
                        cmbTargetType.SelectedIndex = 0
                        txtThresholdObjects.Text = ""
                    End If
                ElseIf lblMethodName.Text.Trim.ToLower.Contains("datelistbased") Then
                    If isPowerUser Then
                        btnDelete.Enabled = True
                        grpTarget.Enabled = False
                        grpPeriodSelection.Enabled = False
                        grpDateListBased.Enabled = True
                        cmbTargetType.SelectedIndex = 0
                        txtThresholdObjects.Text = ""
                    End If
                ElseIf lblMethodName.Text.Trim.ToLower.Contains("fixed_kpi") Then
                    btnDelete.Enabled = True
                    txtThresholdObjects.Text = ""
                Else
                    btnDelete.Enabled = False
                    grpTarget.Enabled = False
                    grpPeriodSelection.Enabled = False
                    grpDateListBased.Enabled = False
                End If
            Else
                lblMethodName.Text = ""
                lblMethodDesc.Text = ""
                cmbTargetType.SelectedIndex = 0
                txtThresholdObjects.Text = ""
                cmbPredefinedFilterEval.SelectedIndex = 0
                grpTarget.Enabled = False
                grpPeriodSelection.Enabled = False
                grpDateListBased.Enabled = False
            End If

            AddHandler dtEditStartTime.EditValueChanged, AddressOf dtEditStartTime_EditValueChanged
            AddHandler dtEditEndTime.EditValueChanged, AddressOf dtEditEndTime_EditValueChanged
            AddHandler cmbPredefTimeStats.SelectedIndexChanged, AddressOf cmbPredefTimeStats_SelectedIndexChanged
            AddHandler cmbPredefinedFilterEval.SelectedIndexChanged, AddressOf cmbPredefinedFilterEval_SelectedIndexChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadThresholdSetKPIs()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvThreshold_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs)
        If e.Value.ToString.ToLower = "select operator" Then
            e.Valid = False
        Else
            e.Valid = True
        End If
    End Sub

    Private Sub gvThreshold_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        Try
            If e.Column.FieldName = "Operator" Then
                e.RepositoryItem = riCmbOperator
            ElseIf e.Column.FieldName = "ThresholdHoldCalculationName" Then
                e.RepositoryItem = ricmbThresholdCalc
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub riCmbOperator_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cmbOp As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
            If cmbOp.SelectedIndex <> 0 Then
                UpdateThresholdSetDetails("Operator", cmbOp.SelectedItem.ToString)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvThreshold_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvThreshold.ShowingEditor
        Try
            Dim data As DataRow = gvThreshold.GetFocusedDataRow()
            If (gvThreshold.FocusedColumn().FieldName = "FailureValue") And data("ThresholdHoldCalculationName").ToString.Contains("Fixed") Then
                e.Cancel = False
            ElseIf (gvThreshold.FocusedColumn().FieldName = "FailurePercentage") And Not data("ThresholdHoldCalculationName").ToString.Contains("Fixed") Then
                e.Cancel = False
            ElseIf (gvThreshold.FocusedColumn().FieldName = "Operator") Then
                e.Cancel = False
            ElseIf (gvThreshold.FocusedColumn().FieldName = "ThresholdHoldCalculationName") Then
                e.Cancel = False
            ElseIf (gvThreshold.FocusedColumn().FieldName = "StdDev") Then
                e.Cancel = False
            ElseIf (gvThreshold.FocusedColumn().FieldName = "WarningValue") Or (gvThreshold.FocusedColumn().FieldName = "WarningPercentage") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvThreshold_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvThreshold.CellValueChanged
        Try
            Dim data As DataRow = gvThreshold.GetFocusedDataRow()
            If data IsNot Nothing Then
                UpdateThresholdSetDetails(e.Column.FieldName, CDbl(e.Value))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnSaveTargetObjects_Click(sender As Object, e As EventArgs) Handles btnSaveTargetObjects.Click
        Try
            If (cmbTargetType.SelectedItem.ToString <> "") Or (txtThresholdObjects.Text.Trim <> "") Then
                Dim connString As String
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@ThresholdTarget", Chr(39) & cmbTargetType.SelectedItem.ToString & Chr(39)},
                    New String() {"@ThresholdObject", Chr(39) & txtThresholdObjects.Text.Trim & Chr(39)},
                    New String() {"@ThresholdSetID", CInt(CType(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)}
                }
                connString = GetSQL(7024, parray)(0)
                sqlParam = GetSQL(7024, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(connString, sqlParam,, iQryTimeOut)
            Else
                SetMessage("Either Target Type Or Objects Left Empty")
                Exit Sub
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
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

    Private Sub cmbPredefTimeStats_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cmb As ComboBoxEdit = CType(sender, ComboBoxEdit)
        If cmb.SelectedIndex > 0 Then
            If cmb.Name.Contains("Stats") Then
                dtEditStartTime.Enabled = False
                dtEditEndTime.Enabled = False
                UpdateThresholdData("ThresholdPredefinedIntervalID")
            End If
        Else
            dtEditStartTime.Enabled = True
            dtEditEndTime.Enabled = True
        End If
    End Sub

    Private Sub dtEditStartTime_EditValueChanged(sender As Object, e As EventArgs)
        Try
            UpdateThresholdData("ThresholdStartDate")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub dtEditEndTime_EditValueChanged(sender As Object, e As EventArgs)
        Try
            UpdateThresholdData("ThresholdEndDate")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditDateLlist_Click(sender As Object, e As EventArgs) Handles btnEditDateLlist.Click
        Try
            Dim objTSDateListsDetails As New dlgThresholdSetDateListDetails()
            objTSDateListsDetails.thresholdSetDateListID = TryCast(cmbThresholdSetDateList.SelectedItem, clsComboBoxItem).Value
            objTSDateListsDetails.ShowDialog()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnCreateDateLlist_Click(sender As Object, e As EventArgs) Handles btnCreateDateLlist.Click
        Try
            Dim newDateListName As String = Nothing
            newDateListName = XtraInputBox.Show("Add New Date List Name:", "Add New Threshold Set Date List Name", "", MessageBoxButtons.OKCancel)
            If newDateListName.Trim <> "" Then
                Dim parray()() As String = {
                    New String() {"@DateListName", Chr(39) & newDateListName & Chr(39)}
                }
                Dim strConnection As String = GetSQL(7028, parray)(0)
                Dim sqlParam As String = GetSQL(7028, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                RemoveHandler cmbThresholdSetDateList.SelectedIndexChanged, AddressOf cmbThresholdSetDateList_SelectedIndexChanged
                LoadThresholdSet_DateLists()
                SetComboBox(cmbThresholdSetDateList, ComboSelectBased.TextBased, newDateListName)
                AddHandler cmbThresholdSetDateList.SelectedIndexChanged, AddressOf cmbThresholdSetDateList_SelectedIndexChanged
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvThreshold_KeyDown(sender As Object, e As KeyEventArgs) Handles gvThreshold.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim rowIndex() As Integer = gvThreshold.GetSelectedRows()
                If rowIndex.Length > 0 Then
                    For iCntr = 0 To rowIndex.Length - 1
                        Dim thresholdSetFixedID As Integer = CInt(gvThreshold.GetRowCellValue(rowIndex(iCntr), "ThresholdSetFixedID"))
                        DeleteThresholdSetKPI(thresholdSetFixedID)
                    Next
                    LoadGridForThresholdSet()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbPredefinedFilterEval_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cmb As ComboBoxEdit = CType(sender, ComboBoxEdit)
        If cmb.SelectedIndex > 0 Then
            UpdateThresholdData("PeriodFilterID")
        End If
    End Sub

    Private Sub ricmbThresholdCalc_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cmbOp As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
            If cmbOp.SelectedIndex <> 0 Then
                UpdateThresholdSetDetails("ThresholdCalculationID", CInt(TryCast(cmbOp.SelectedItem, clsComboBoxItem).Value))
                UpdateThresholdData("ThresholdCalculationID", CInt(TryCast(cmbOp.SelectedItem, clsComboBoxItem).Value))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ricmbThresholdCalc_ParseEditValue(sender As Object, e As ConvertEditValueEventArgs)
        If e.Value.ToString = "Select" Then
            e.Handled = False
            XtraMessageBox.Show("Value Not Allowed!", "Threshold Calculation Name")
        Else
            e.Value = e.Value.ToString
            e.Handled = True
        End If
    End Sub

    Private Sub gvThreshold_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) 'Handles gvThreshold.CellValueChanging
        Try
            Dim data As DataRow = gvThreshold.GetFocusedDataRow()
            If data IsNot Nothing Then
                If e.Column.FieldName = "FailureValue" Then
                    If data("ThresholdHoldCalculationName") = "Fixed" Then

                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAddKPIs_Click(sender As Object, e As EventArgs) Handles btnAddKPIs.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbThresholdSets.SelectedIndex > 0 Then
                Dim objDialog As New dlgAddKPI2ThresholdSet()
                objDialog.thresholdSetID = CInt(TryCast(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)
                objDialog.kpiSetID = CInt(TryCast(cmbKPISets.SelectedItem, clsComboBoxItem).Value)
                objDialog.ShowDialog()
            Else
                SetMessage("Please Select Threshold Set")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            LoadGridForThresholdSet()

            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteKPI_Click(sender As Object, e As EventArgs) Handles btnDeleteKPI.Click
        Try
            Dim rowIndex() As Integer = gvThreshold.GetSelectedRows()
            If rowIndex.Length > 0 Then
                For iCntr = 0 To rowIndex.Length - 1
                    Dim thresholdSetFixedID As Integer = CInt(gvThreshold.GetRowCellValue(rowIndex(iCntr), "ThresholdSetFixedID"))
                    DeleteThresholdSetKPI(thresholdSetFixedID)
                Next
                LoadGridForThresholdSet()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbKPISets_SelectedIndexChanged(sender As Object, e As EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If cmbKPISets.SelectedIndex > 0 Then
                btnLoad.Enabled = True
                btnAddKPIs.Enabled = True
            Else
                btnLoad.Enabled = False
                btnAddKPIs.Enabled = False
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbThresholdSetDateList_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbThresholdSetDateList.SelectedIndex > 0 Then
                UpdateDateListForThresoldSet()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub bntRename_Click(sender As Object, e As EventArgs) Handles bntRename.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim _thresholdSetID As Integer = Nothing
            Dim isPowerUser As Boolean = False

            If cmbThresholdSets.SelectedIndex > 0 Then
                _thresholdSetID = CInt(TryCast(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)
                Dim owner As String = dtPMThresholdSetList.AsEnumerable().Where(Function(x) x.Field(Of Integer)("ThresholdSetID") = _thresholdSetID)(0)("Owner").ToString
                If owner.ToLower <> Environment.UserName.ToLower Then
                    If configMgr.User.IsPowerUser = True Then
                        isPowerUser = True
                    Else
                        XtraMessageBox.Show("Current user can't rename the Threshold Set as the owner is a different user.", "Rename Threshold Set!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    isPowerUser = True
                End If
                If isPowerUser = True Then
                    Dim renamedThresholdSet As String = Nothing
                    renamedThresholdSet = XtraInputBox.Show("Rename Threshold Set:", "Rename Threshold Set", cmbThresholdSets.SelectedItem.ToString, MessageBoxButtons.OKCancel)
                    If renamedThresholdSet.Trim <> "" Then
                        RenameThresholdSet(_thresholdSetID, renamedThresholdSet)
                        LoadThresholdSetList()
                        SetComboBox(cmbThresholdSets, ComboSelectBased.TextBased, renamedThresholdSet)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadThresholdSetList()
        RemoveHandler cmbThresholdSets.SelectedIndexChanged, AddressOf cmbThresholdSets_SelectedIndexChanged
        dtPMThresholdSetList = GetThresholdSetList(Me.thresholdSetTech)
        BindDevExComboBoxWithValueMember(cmbThresholdSets, dtPMThresholdSetList, "ThresholdSetID", "ThresholdSetName", "Select Threshold Set", False)
        AddHandler cmbThresholdSets.SelectedIndexChanged, AddressOf cmbThresholdSets_SelectedIndexChanged
    End Sub

    Private Sub LoadKPISetList()
        RemoveHandler cmbKPISets.SelectedIndexChanged, AddressOf cmbKPISets_SelectedIndexChanged
        Dim dt As DataTable = GetKPISetList(Me.thresholdSetTech)
        BindDevExComboBoxWithValueMember(cmbKPISets, dt, "KPISetID", "KPISetName", "Select KPI Set", False)
        AddHandler cmbKPISets.SelectedIndexChanged, AddressOf cmbKPISets_SelectedIndexChanged
    End Sub

    Private Sub DeleteThresholdSet(thresholdSetID As Integer)
        Dim parray()() As String = {
            New String() {"@ThresholdSetID", thresholdSetID}
        }
        Dim strConnection As String = GetSQL(7012, parray)(0)
        Dim sqlParam As String = GetSQL(7012, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadGridForThresholdSet()
        RemoveHandler gvThreshold.CustomRowCellEdit, AddressOf gvThreshold_CustomRowCellEdit
        AddHandler gvThreshold.ValidatingEditor, AddressOf gvThreshold_ValidatingEditor

        Dim parray()() As String = {
            New String() {"@ThresholdSetID", CInt(TryCast(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)}
        }
        Dim strConnection As String = GetSQL(7014, parray)(0)
        Dim sqlParam As String = GetSQL(7014, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim cols2Hide() As String = Nothing
        cols2Hide = {"KPISetID", "ThresholdSetFixedID", "ThresholdSetID"}   ', "WarningValue", "WarningPercentage"
        IOSDevExpressGrid.PopulateDataInGrid(gcThreshold, gvThreshold, dt, "ALL", cols2Hide, "KPI_Name")

        riCmbOperator = TryCast(gcThreshold.RepositoryItems.Add("ComboBoxEdit"), RepositoryItemComboBox)
        Dim items As String() = {"Select Operator", "=", "<>", "<", ">", "<=", ">="}
        riCmbOperator.Items.AddRange(items)
        AddHandler riCmbOperator.SelectedIndexChanged, AddressOf riCmbOperator_SelectedIndexChanged

        ricmbThresholdCalc = TryCast(gcThreshold.RepositoryItems.Add("ComboBoxEdit"), RepositoryItemComboBox)
        Dim dtCalc As DataTable = GetThresholdSet_CalculationList()
        BindDevExRepositoryItemComboBoxWithValueMember(ricmbThresholdCalc, dtCalc, "ThresholdCalculationID", "ThresholdHoldCalculationName", "Select")
        AddHandler ricmbThresholdCalc.ParseEditValue, AddressOf ricmbThresholdCalc_ParseEditValue
        AddHandler ricmbThresholdCalc.SelectedIndexChanged, AddressOf ricmbThresholdCalc_SelectedIndexChanged

        AddHandler gvThreshold.CustomRowCellEdit, AddressOf gvThreshold_CustomRowCellEdit
        AddHandler gvThreshold.ValidatingEditor, AddressOf gvThreshold_ValidatingEditor
    End Sub

    Private Sub UpdateThresholdSetDetails(fieldName As String, fieldValue As String)
        If fieldName.ToLower = "operator" Then
            fieldValue = Chr(39) & fieldValue & Chr(39)
        End If
        Dim parray()() As String = {
            New String() {"@ThresholdSetFixedID", CInt(gvThreshold.GetFocusedRowCellValue("ThresholdSetFixedID"))},
            New String() {"@ColName", Chr(39) & fieldName & Chr(39)},
            New String() {"@ColValue", fieldValue}
        }
        Dim strConnection As String = GetSQL(7015, parray)(0)
        Dim sqlParam As String = GetSQL(7015, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub UpdateThresholdData(colName As String, Optional colValue As Integer = Nothing)
        Dim connString As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        If colName.ToLower = "thresholdstartdate" Then
            parray = {
            New String() {"@ThresholdSetID", CInt(CType(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)},
            New String() {"@ColName", Chr(39) & colName & Chr(39)},
            New String() {"@ColValue", Chr(39) & CDate(dtEditStartTime.EditValue).ToString("yyyy-MM-dd") & Chr(39)}
        }
        ElseIf colName.ToLower = "thresholdenddate" Then
            parray = {
            New String() {"@ThresholdSetID", CInt(CType(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)},
            New String() {"@ColName", Chr(39) & colName & Chr(39)},
            New String() {"@ColValue", Chr(39) & CDate(dtEditEndTime.EditValue).ToString("yyyy-MM-dd") & Chr(39)}
        }
        ElseIf colName.ToLower = "thresholdpredefinedintervalid" Then
            parray = {
                New String() {"@ThresholdSetID", CInt(CType(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)},
                New String() {"@ColName", Chr(39) & colName & Chr(39)},
                New String() {"@ColValue", CInt(CType(cmbPredefTimeStats.SelectedItem, clsComboBoxItem).Value)}
            }
        ElseIf colName.ToLower = "periodfilterid" Then
            parray = {
                New String() {"@ThresholdSetID", CInt(CType(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)},
                New String() {"@ColName", Chr(39) & colName & Chr(39)},
                New String() {"@ColValue", CInt(CType(cmbPredefinedFilterEval.SelectedItem, clsComboBoxItem).Value)}
            }
        ElseIf colName.ToLower = "thresholdcalculationid" Then
            parray = {
                New String() {"@ThresholdSetID", CInt(CType(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)},
                New String() {"@ColName", Chr(39) & colName & Chr(39)},
                New String() {"@ColValue", colValue}
            }
        End If
        connString = GetSQL(7025, parray)(0)
        sqlParam = GetSQL(7025, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(connString, sqlParam,, iQryTimeOut)
        dtPMThresholdSetList = GetThresholdSetList(Me.thresholdSetTech)
    End Sub

    Private Sub LoadThresholdSet_DateLists()
        Dim parray()() As String = {
            New String() {"@ThresholdSetID", thresholdSetID}
        }
        Dim strConnection As String = GetSQL(7027, parray)(0)
        Dim sqlParam As String = GetSQL(7027, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            BindDevExComboBoxWithValueMember(cmbThresholdSetDateList, dt, "ThresholdSetDateListID", "DateListName", "Select")
        End If
    End Sub

    Private Sub DeleteThresholdSetKPI(thresholdSetFixedID As Integer)
        Dim parray()() As String = {
            New String() {"@ThresholdSetFixedID", thresholdSetFixedID}
        }
        Dim strConnection As String = GetSQL(7033, parray)(0)
        Dim sqlParam As String = GetSQL(7033, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub BindComboWithTargetType(ByRef dt As DataTable, ByRef cmb As ComboBoxEdit, ByVal tech As String)
        Try
            If Not dt Is Nothing Then
                cmb.Properties.Items.Clear()

                Dim cmbitem As New IOS.Library.clsComboBoxItem()
                cmbitem.Text = "PLMN"
                cmbitem.Value = "PLMN"
                cmbitem.Enabled = True
                cmbitem.Tag = "PLMN"
                cmb.Properties.Items.Add(cmbitem)

                For Each drow As DataRow In dt.AsEnumerable().Where(Function(x) x.Field(Of String)("tech").ToUpper = tech.ToUpper And x.Field(Of Integer)("ObjectTreeEnabled") = 1).OrderBy(Function(x) x.Field(Of Integer)("loadorder"))
                    cmbitem = New IOS.Library.clsComboBoxItem()
                    cmbitem.Text = drow("Object").ToString.ToUpper
                    cmbitem.Value = drow("Object").ToString.ToUpper
                    cmbitem.Enabled = drow("ObjectTreeEnabled")
                    cmbitem.Tag = drow("InternalObjectName").ToString.ToUpper
                    cmb.Properties.Items.Add(cmbitem)
                Next
                cmb.SelectedItem = cmb.Properties.Items(0)

                Select Case thresholdSetTech
                    Case "SGSN", "GGSN", "MGW", "MME", "MSS", "PGW", "SGW", "IMS", "TX", "TRANSPORT"
                    Case Else
                        cmbitem = New IOS.Library.clsComboBoxItem()
                        cmbitem.Text = "TAGS"
                        cmbitem.Value = "TAGS"
                        cmbitem.Enabled = True
                        cmbitem.Tag = "TAGS"
                        cmb.Properties.Items.Add(cmbitem)
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetThresholdSet_CalculationList() As DataTable
        Dim parray()() As String = Nothing
        Dim strConnection As String = GetSQL(7034, parray)(0)
        Dim sqlParam As String = GetSQL(7034, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub LoadThresholdSetKPIs()
        If cmbKPISets.SelectedIndex > 0 Then
            Dim parray()() As String = {
                New String() {"@ThresholdSetID", cmbThresholdSets.SelectedItem.value},
                New String() {"@KPISetID", CInt(TryCast(cmbKPISets.SelectedItem, clsComboBoxItem).Value)}
            }
            Dim strConnection As String = GetSQL(7016, parray)(0)
            Dim sqlParam As String = GetSQL(7016, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            Dim cols2Hide() As String = Nothing
            cols2Hide = {"KPISetID", "ThresholdSetFixedID", "ThresholdSetID"}   ', "WarningValue", "WarningPercentage"
            IOSDevExpressGrid.PopulateDataInGrid(gcThreshold, gvThreshold, dt, "ALL", cols2Hide, "KPI_Name")
        End If
    End Sub

    Private Sub UpdateDateListForThresoldSet()
        Dim parray()() As String = {
            New String() {"@ThresholdDateListID", CInt(TryCast(cmbThresholdSetDateList.SelectedItem, clsComboBoxItem).Value)},
            New String() {"@ThresholdSetID", CInt(TryCast(cmbThresholdSets.SelectedItem, clsComboBoxItem).Value)}
        }
        Dim strConnection As String = GetSQL(7041, parray)(0)
        Dim sqlParam As String = GetSQL(7041, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub RenameThresholdSet(thresholdSetID As Integer, thresholdSetName As String)
        Dim parray()() As String = {
            New String() {"@ThresholdSetName", Chr(39) & thresholdSetName & Chr(39)},
            New String() {"@ThresholdSetID", thresholdSetID}
        }
        Dim strConnection As String = GetSQL(7043, parray)(0)
        Dim sqlParam As String = GetSQL(7043, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

#End Region

End Class