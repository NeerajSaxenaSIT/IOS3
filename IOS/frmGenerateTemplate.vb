Imports IOS.DataLibrary
Imports IOS.Configuration
Imports DevExpress.XtraEditors
Imports IOS.Library
Imports System.Text
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmGenerateTemplate

    Public templateName As String = Nothing
    Public templateID As Integer = 0
    Public vendorName As String = Nothing
    Public filterString As String = Nothing
    Private dtParamExclusion As New DataTable
    Private dtFilter As New DataTable
    Private riCmbPriority As RepositoryItemComboBox

    Public copyFromTemplateID As Integer = 0
    Public copyFromTemplateMOConfigID As Integer = 0
    Public copyFilterStringsFromMO As Boolean = False
    Public copyInclusionListFromMO As Boolean = False
    Public copyExclusionListFromMO As Boolean = False
    Public copyParamExclusionListFromTemplate As Boolean = False

#Region "Events"

    Private Sub frmGenerateTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lblSelectedTemplate.Text = Me.templateName
            LoadTechCombo()

            If dtIncList.Columns.Count = 0 Then
                dtIncList.Columns.Add("ListID", GetType(System.Int32))
                dtIncList.Columns.Add("ListName", GetType(System.String))
                dtIncList.Columns.Add("ListType", GetType(System.String))
            End If

            If dtExcList.Columns.Count = 0 Then
                dtExcList.Columns.Add("ListID", GetType(System.Int32))
                dtExcList.Columns.Add("ListName", GetType(System.String))
                dtExcList.Columns.Add("ListType", GetType(System.String))
            End If

            If dtParamExclusion.Columns.Count = 0 Then
                dtParamExclusion.Columns.Add("ParameterName", GetType(System.String))
                dtParamExclusion.Columns.Add("TemplateID", GetType(System.Int32))
            End If

            If dtFilter.Columns.Count = 0 Then
                dtFilter.Columns.Add("FilterString", GetType(System.String))
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frmGenerateTemplate_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not objGenerateTemplate Is Nothing Then
            dtIncList.Columns.Clear()
            dtIncList.Rows.Clear()

            dtExcList.Columns.Clear()
            dtExcList.Rows.Clear()

            dtParamExclusion.Columns.Clear()
            dtParamExclusion.Rows.Clear()

            dtFilter.Columns.Clear()
            dtFilter.Rows.Clear()

            objGenerateTemplate.Dispose()
            objGenerateTemplate = Nothing
        End If
    End Sub

    Private Sub btnAddFilter_Click(sender As Object, e As EventArgs) Handles btnAddFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If (gvListAllMO.GetFocusedRowCellValue("Select") = True) Then
                Me.filterString = Nothing
                Dim objFilter As New dlgObjFilter()
                objFilter.templateID = Me.templateID
                objFilter.moName = gvListAllMO.GetRowCellValue(0, "MOName")
                objFilter.moTable = gvListAllMO.GetRowCellValue(0, "MOTable")
                objFilter.moDatabaseName = gvListAllMO.GetRowCellValue(0, "MODatabase")
                objFilter.joinTable = gvListAllMO.GetRowCellValue(0, "JoinTable")
                objFilter.excludedColumns = gvListAllMO.GetRowCellValue(0, "ExcludedColumns")
                objFilter.filterType = "Mo4Template"
                objFilter.ShowDialog()

                If Not Me.filterString Is Nothing Then
                    Dim dr As DataRow = dtFilter.NewRow()
                    dr("FilterString") = Me.filterString
                    dtFilter.Rows.Add(dr)
                    dtFilter.AcceptChanges()
                    IOSDevExpressGrid.PopulateDataInGrid(gcMOFilter, gvMOFilter, dtFilter, "ALL",, "FilterString")
                    gcMOFilter.Refresh()
                End If
            Else
                XtraMessageBox.Show("Please Select MO", "Select MO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                Exit Sub
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnDeleteFilter_Click(sender As Object, e As EventArgs) Handles btnDeleteFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If (gvMOFilter.SelectedRowsCount > 0) Then
                Dim selectedRowHandle As Integer = gvMOFilter.FocusedRowHandle
                Dim filterName As String = gvMOFilter.GetRowCellValue(selectedRowHandle, "FilterString")
                If XtraMessageBox.Show("Are you sure to delete filter: " & filterName & "?", "Delete Filter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    gvMOFilter.DeleteRow(selectedRowHandle)
                    If gvMOFilter.RowCount > 0 Then
                        gvMOFilter.SelectRow(0)
                    End If
                    gcMOFilter.Refresh()
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

    Private Sub btnListMngr_Click(sender As Object, e As EventArgs) Handles btnListMngr.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            frmListManager.Show()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnParamExclusionAdd_Click(sender As Object, e As EventArgs) Handles btnParamExclusionAdd.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim paramNameToExclude As String = Nothing
            paramNameToExclude = XtraInputBox.Show("Enter Parameter Name To Exclude", "Add Parameter Name To Exclude", "", MessageBoxButtons.OKCancel)

            If paramNameToExclude <> "" Then
                Dim dr As DataRow = dtParamExclusion.NewRow()
                dr("ParameterName") = paramNameToExclude
                dr("TemplateID") = CInt(Me.templateID)
                dtParamExclusion.Rows.Add(dr)
                dtParamExclusion.AcceptChanges()

                lstParamExclusion.DataSource = dtParamExclusion
                lstParamExclusion.DisplayMember = "ParameterName"
                lstParamExclusion.ValueMember = "TemplateID"
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnParamExclusionDelete_Click(sender As Object, e As EventArgs) Handles btnParamExclusionDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If lstParamExclusion.SelectedIndex > -1 Then
                LoadExcludedParams(lstParamExclusion.SelectedIndex)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub lstParamExclusion_KeyUp(sender As Object, e As KeyEventArgs) Handles lstParamExclusion.KeyUp
        If e.KeyCode = Keys.Delete Then
            Try
                If lstParamExclusion.SelectedIndex > -1 Then
                    LoadExcludedParams(lstParamExclusion.SelectedIndex)
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub cmbTech_SelectedIndexChanged(sender As Object, e As EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            grdListAllMO.DataSource = Nothing
            gvListAllMO.Columns.Clear()

            RemoveHandler cmbObjectField.SelectedIndexChanged, AddressOf cmbObjectField_SelectedIndexChanged
            If cmbTech.SelectedIndex > 0 Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@IOSTech", Chr(39) & cmbTech.SelectedItem.ToString & Chr(39)},
                    New String() {"@Vendor", Chr(39) & Me.vendorName & Chr(39)}
                }
                strConnection = GetSQL(4184, parray)(0)
                sqlParam = GetSQL(4184, parray)(1)
                Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                BindDevExComboBoxWithValueMember(cmbObjectField, dt, "JoinTable", "KeyObjectField", "Select Object Field")
            End If
            AddHandler cmbObjectField.SelectedIndexChanged, AddressOf cmbObjectField_SelectedIndexChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbObjectField_SelectedIndexChanged(sender As Object, e As EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbObjectField.SelectedIndex > 0 Then
                Dim sql As String = "Select " & cmbObjectField.SelectedItem.ToString & " From " & TryCast(cmbObjectField.SelectedItem, clsComboBoxItem).Value & " Order By 1"
                GetTextboxDataWithAutoCompleteFeature(txtObject, sql)
                GetAllMOList()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_AddIncList_Click(sender As Object, e As EventArgs) Handles tsmi_AddIncList.Click
        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    Application.DoEvents()

        '    dlgListToAdd.TemplateMOConfigID = 0
        '    dlgListToAdd.MOType = "currentGenTemp"
        '    dlgListToAdd.FilterType = "Inclusion"

        '    If dlgListToAdd.ShowDialog() = DialogResult.OK Then
        '        If dtIncList.Rows.Count > 0 Then
        '            For Each dr As DataRow In dtIncList.Rows
        '                If Not (lstInclusion.Items.Contains("ListName")) Then
        '                    lstInclusion.Items.Add(dr("ListName"))
        '                End If
        '            Next
        '        End If
        '    End If

        'Catch ex As Exception
        '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        '    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        '    Application.DoEvents()
        'End Try
    End Sub

    Private Sub tsmi_AddIncList2AllMO_Click(sender As Object, e As EventArgs) Handles tsmi_AddIncList2AllMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            dlgListToAdd.TemplateID = CInt(Me.templateID)
            dlgListToAdd.MOType = "allGenTemp"
            dlgListToAdd.FilterType = "Inclusion"

            If dlgListToAdd.ShowDialog() = DialogResult.OK Then
                lstInclusion.DataSource = Nothing
                If dtIncList.Rows.Count > 0 Then
                    lstInclusion.DataSource = dtIncList
                    lstInclusion.DisplayMember = "ListName"
                    lstInclusion.ValueMember = "ListID"
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

    Private Sub tsmi_RemoveIncList_Click(sender As Object, e As EventArgs) Handles tsmi_RemoveIncList.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstInclusion.SelectedIndex > -1 Then
                Dim inclusionFilter As String = lstInclusion.SelectedItem(1).ToString
                If XtraMessageBox.Show("Are you sure to remove filter list: " & inclusionFilter & "?", "Remove Template Filter List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim dr As DataRow = dtIncList.Select("ListID=" & lstInclusion.SelectedItem(0))(0)
                    dtIncList.Rows.Remove(dr)
                    dtIncList.AcceptChanges()

                    lstInclusion.DataSource = Nothing
                    lstInclusion.DataSource = dtIncList
                    lstInclusion.DisplayMember = "ListName"
                    lstInclusion.ValueMember = "ListID"
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

    Private Sub tsmi_AddExcList_Click(sender As Object, e As EventArgs) Handles tsmi_AddExcList.Click
        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    Application.DoEvents()

        '    dlgListToAdd.TemplateMOConfigID = 0
        '    dlgListToAdd.MOType = "currentGenTemp"
        '    dlgListToAdd.FilterType = "Exclusion"

        '    If dlgListToAdd.ShowDialog() = DialogResult.OK Then
        '        If dtExcList.Rows.Count > 0 Then
        '            For Each dr As DataRow In dtExcList.Rows
        '                If Not (lstExclusion.Items.Contains("ListName")) Then
        '                    lstExclusion.Items.Add(dr("ListName"))
        '                End If
        '            Next
        '        End If
        '    End If
        'Catch ex As Exception
        '    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        '    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        '    Application.DoEvents()
        'End Try
    End Sub

    Private Sub tsmi_AddExcList2AllMO_Click(sender As Object, e As EventArgs) Handles tsmi_AddExcList2AllMO.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            dlgListToAdd.TemplateID = CInt(Me.templateID)
            dlgListToAdd.MOType = "allGenTemp"
            dlgListToAdd.FilterType = "Exclusion"

            If dlgListToAdd.ShowDialog() = DialogResult.OK Then
                lstExclusion.DataSource = Nothing
                If dtExcList.Rows.Count > 0 Then
                    lstExclusion.DataSource = dtExcList
                    lstExclusion.DisplayMember = "ListName"
                    lstExclusion.ValueMember = "ListID"
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

    Private Sub tsmi_RemoveExcList_Click(sender As Object, e As EventArgs) Handles tsmi_RemoveExcList.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstExclusion.SelectedIndex > -1 Then
                Dim exclusionFilter As String = lstExclusion.SelectedItem(1).ToString
                If XtraMessageBox.Show("Are you sure to remove filter list: " & exclusionFilter & "?", "Remove Template Filter List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim dr As DataRow = dtExcList.Select("ListID=" & lstExclusion.SelectedItem(0))(0)
                    dtExcList.Rows.Remove(dr)
                    dtExcList.AcceptChanges()

                    lstExclusion.DataSource = Nothing
                    lstExclusion.DataSource = dtExcList
                    lstExclusion.DisplayMember = "ListName"
                    lstExclusion.ValueMember = "ListID"
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

    Private Sub gvListAllMO_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvListAllMO.ShowingEditor
        Try
            If (gvListAllMO.FocusedColumn().FieldName = "Select") Or (gvListAllMO.FocusedColumn().FieldName = "Priority") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmsAllMOList_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsAllMOList.Opening
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tsmiSelectAll As Boolean = False
            tsmi_RecordCount.Text = "Record Count: " & gvListAllMO.RowCount.ToString

            For iCnt As Integer = 0 To gvListAllMO.RowCount - 1
                If (gvListAllMO.GetFocusedRowCellValue("Select") = True) Then
                    tsmiSelectAll = True
                Else
                    tsmiSelectAll = False
                End If
            Next

            If (Not tsmiSelectAll) Then
                tsmi_SelectAll.Enabled = True
                tsmi_DeselectAll.Enabled = False
            Else
                tsmi_SelectAll.Enabled = False
                tsmi_DeselectAll.Enabled = True
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_SelectAll_Click(sender As Object, e As EventArgs) Handles tsmi_SelectAll.Click
        Try
            For iCnt As Integer = 0 To gvListAllMO.RowCount - 1
                gvListAllMO.SetRowCellValue(iCnt, "Select", True)
                btnAddFilter.Enabled = True
            Next
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_DeselectAll_Click(sender As Object, e As EventArgs) Handles tsmi_DeselectAll.Click
        Try
            For iCnt As Integer = 0 To gvListAllMO.RowCount - 1
                gvListAllMO.SetRowCellValue(iCnt, "Select", False)
                'btnAddFilter.Enabled = False
            Next
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            lblStatus.Text = "Status: "

            If txtObject.Text = String.Empty AndAlso txtCommonalityValue.Text = String.Empty Then
                lblStatus.Text = lblStatus.Text + "Please Set Object/Commonality Value"
                lblStatus.ForeColor = Color.Red
                Exit Sub
            End If
            lblStatus.ForeColor = Color.Black

            If ceDeleteAll.Checked = True Then
                lblStatus.Text = "Deleting All MO in Template"

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@TemplateID", Me.templateID}
                }

                strConnection = GetSQL(4171, parray)(0)
                sqlParam = GetSQL(4171, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            End If

            Dim filterString As String = ""
            Dim inclusionList As String = ""
            Dim exclusionList As String = ""
            Dim paramToExclude As String = ""

            For iCnt As Integer = 0 To gvMOFilter.RowCount - 1
                If gvMOFilter.GetRowCellValue(iCnt, "FilterString").ToString.Contains("''") Then
                    filterString &= gvMOFilter.GetRowCellValue(iCnt, "FilterString").ToString & "|"
                Else
                    filterString &= gvMOFilter.GetRowCellValue(iCnt, "FilterString").ToString.Replace("'", "''") & "|"
                End If
            Next
            filterString = filterString.TrimEnd("|")

            For iCnt As Integer = 0 To lstInclusion.ItemCount - 1
                inclusionList &= lstInclusion.GetItemValue(iCnt).ToString & "|"
            Next
            inclusionList = inclusionList.TrimEnd("|")

            For iCnt As Integer = 0 To lstExclusion.ItemCount - 1
                exclusionList &= lstExclusion.GetItemValue(iCnt).ToString & "|"
            Next
            exclusionList = exclusionList.TrimEnd("|")

            For iCnt As Integer = 0 To lstParamExclusion.ItemCount - 1
                paramToExclude &= lstParamExclusion.GetItemText(iCnt).ToString & "|"
            Next
            paramToExclude = paramToExclude.TrimEnd("|")

            For iCnt As Integer = 0 To gvListAllMO.RowCount - 1
                If (gvListAllMO.GetRowCellValue(iCnt, "Select") = True) Then
                    lblStatus.Text = "Status: Generating MO: " & gvListAllMO.GetRowCellValue(iCnt, "MOName")
                    Application.DoEvents()

                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@TemplateID", Me.templateID},
                        New String() {"@IOS_TECH", Chr(39) & cmbTech.SelectedItem.ToString & Chr(39)},
                        New String() {"@IndividualMO", Chr(39) & gvListAllMO.GetRowCellValue(iCnt, "MOName") & Chr(39)},
                        New String() {"@MO_CommonKeyField", Chr(39) & gvListAllMO.GetRowCellValue(iCnt, "MO_CommonKeyField") & Chr(39)},
                        New String() {"@ObjectField", Chr(39) & cmbObjectField.SelectedItem.ToString & Chr(39)},
                        New String() {"@ObjectName", Chr(39) & txtObject.Text.Trim & Chr(39)},
                        New String() {"@ObjectStaticFilter", Chr(39) & filterString & Chr(39)},
                        New String() {"@ObjectConditionField", Chr(39) & gvListAllMO.GetRowCellValue(iCnt, "ObjectConditionColumns") & Chr(39)},
                        New String() {"@DeleteExistingConfiguration", Chr(39) & IIf(ceDeleteMO.Checked = True, "1", "0") & Chr(39)},
                        New String() {"@FilterOutLongSwitch", Chr(39) & 1 & Chr(39)},
                        New String() {"@InclusionListID", Chr(39) & inclusionList & Chr(39)},
                        New String() {"@ExclusionListID", Chr(39) & exclusionList & Chr(39)},
                        New String() {"@ParamExlusionFilter", Chr(39) & paramToExclude & Chr(39)},
                        New String() {"@CommonalityValue", Chr(39) & txtCommonalityValue.Text.Trim & Chr(39)},
                        New String() {"@Priority", Chr(39) & gvListAllMO.GetRowCellValue(iCnt, "Priority") & Chr(39)}
                    }
                    strConnection = GetSQL(4170, parray)(0)
                    sqlParam = GetSQL(4170, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                End If
            Next
        Catch ex As Exception
            lblStatus.Text = "Status: Error: " & ex.Message
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        If Not lblStatus.Text.StartsWith("Status: Error:") Then
            lblStatus.Text = "Status: DONE !"
        End If
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCopyFromFilter_Click(sender As Object, e As EventArgs) Handles btnCopyFromFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objCopyFromTemplate As New frmCopyFromTemplate()
            objCopyFromTemplate.vendorName = Me.vendorName
            objCopyFromTemplate.ShowDialog()

            If copyFilterStringsFromMO = True Then
                LoadFilterStrings()
            Else
                IOSDevExpressGrid.ClearGrid(gcMOFilter)
            End If

            LoadFilterLists()

            If copyParamExclusionListFromTemplate = True Then
                LoadParamExclusionList()
            Else
                lstParamExclusion.DataSource = Nothing
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub txtObject_KeyUp(sender As Object, e As KeyEventArgs) Handles txtObject.KeyUp
        Try
            If txtObject.Text = String.Empty Then
                txtCommonalityValue.Enabled = True
            Else
                txtCommonalityValue.Enabled = False
            End If
            txtCommonalityValue.Text = String.Empty
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub txtCommonalityValue_KeyUp(sender As Object, e As KeyEventArgs) Handles txtCommonalityValue.KeyUp
        Try
            Dim iVal As Decimal = 0
            Try
                iVal = Decimal.Parse(txtCommonalityValue.Text, Globalization.NumberStyles.AllowDecimalPoint)
            Catch ex As Exception
                Dim ToolTip1 As New ToolTip
                ToolTip1.IsBalloon = True
                ToolTip1.ToolTipIcon = ToolTipIcon.Error
                ToolTip1.ToolTipTitle = "The allowed range for commonality vlaue is 0-100"
                ToolTip1.Show("Style is not a Decimal value", txtCommonalityValue, New Point(0, -80), 2500)
                txtCommonalityValue.Text = ""
            End Try
            If iVal >= 0 AndAlso iVal <= 100 Then
                Exit Sub
            Else
                Dim ToolTip1 As New ToolTip
                ToolTip1.IsBalloon = True
                ToolTip1.ToolTipIcon = ToolTipIcon.Error
                ToolTip1.ToolTipTitle = "The allowed range for commonality vlaue is 0-100"
                ToolTip1.Show("Value was out of range", txtCommonalityValue, New Point(0, -80), 2500)
                txtCommonalityValue.Text = ""
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub gvListAllMO_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs)
        If e.Value.ToString.ToLower = "select priority" Then
            e.Valid = False
        Else
            e.Valid = True
        End If
    End Sub

    Private Sub gvListAllMO_CustomRowCellEdit(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs)
        Try
            If e.Column.FieldName = "Priority" Then
                e.RepositoryItem = riCmbPriority
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadTechCombo()
        RemoveHandler cmbTech.SelectedIndexChanged, AddressOf cmbTech_SelectedIndexChanged
        If Not Me.vendorName Is Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@Vendor", Chr(39) & Me.vendorName & Chr(39)}
            }
            strConnection = GetSQL(4185, parray)(0)
            sqlParam = GetSQL(4185, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            BindDevExComboBoxWithValueMember(cmbTech, dt, "IOS_TECH", "IOS_TECH", "Select Tech")
        End If
        AddHandler cmbTech.SelectedIndexChanged, AddressOf cmbTech_SelectedIndexChanged
    End Sub

    Private Sub LoadFilterLists()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@templateMOConfigID", Me.copyFromTemplateMOConfigID}
        }
        strConnection = GetSQL(4157, parray)(0)
        sqlParam = GetSQL(4157, parray)(1)

        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        lstInclusion.DataSource = Nothing
        lstExclusion.DataSource = Nothing

        If dt.Rows.Count > 0 Then
            'Fill inclusion filter list
            If copyInclusionListFromMO = True Then
                If dt.Select("InclusionOrExclusion='Inclusion'").Length <> 0 Then
                    Dim dtInclusion As DataTable = dt.Select("InclusionOrExclusion='Inclusion'").CopyToDataTable
                    lstInclusion.DataSource = dtInclusion
                    lstInclusion.DisplayMember = "ListName"
                    lstInclusion.ValueMember = "ListID"
                End If
            End If

            'Fill exclusion filter list
            If copyExclusionListFromMO = True Then
                If dt.Select("InclusionOrExclusion='Exclusion'").Length <> 0 Then
                    Dim dtExclusion As DataTable = dt.Select("InclusionOrExclusion='Exclusion'").CopyToDataTable
                    lstExclusion.DataSource = dtExclusion
                    lstExclusion.DisplayMember = "ListName"
                    lstExclusion.ValueMember = "ListID"
                End If
            End If
        End If
    End Sub

    Private Sub GetAllMOList()
        AddHandler gvListAllMO.ValidatingEditor, AddressOf gvListAllMO_ValidatingEditor
        AddHandler gvListAllMO.CustomRowCellEdit, AddressOf gvListAllMO_CustomRowCellEdit

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@IOSTech", Chr(39) & cmbTech.SelectedItem.ToString & Chr(39)},
            New String() {"@ObjectField", Chr(39) & cmbObjectField.SelectedItem.ToString & Chr(39)}
        }
        strConnection = GetSQL(4186, parray)(0)
        sqlParam = GetSQL(4186, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim columnsToHide() As String = {"MO_CommonKeyField", "MOTable", "MODatabase", "ExcludedColumns", "JoinTable"}
            IOSDevExpressGrid.PopulateDataInGrid(grdListAllMO, gvListAllMO, dt, "ALL", columnsToHide, "MOName")

            Dim riChkMO As RepositoryItemCheckEdit = TryCast(grdListAllMO.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
            riChkMO.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard
            riChkMO.AllowGrayed = False
            riChkMO.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
            riChkMO.ValueChecked = "True"
            gvListAllMO.Columns("Select").ColumnEdit = riChkMO
            btnAddFilter.Enabled = True

            riCmbPriority = TryCast(grdListAllMO.RepositoryItems.Add("ComboBoxEdit"), RepositoryItemComboBox)
            Dim items As String() = {"Select Priority", "Critical", "Major", "Normal"}
            riCmbPriority.Items.AddRange(items)
            'AddHandler riCmbPriority.SelectedIndexChanged, AddressOf riCmbPriority_SelectedIndexChanged
            AddHandler gvListAllMO.ValidatingEditor, AddressOf gvListAllMO_ValidatingEditor
            AddHandler gvListAllMO.CustomRowCellEdit, AddressOf gvListAllMO_CustomRowCellEdit
        Else
            btnAddFilter.Enabled = False
        End If
    End Sub

    Private Sub LoadExcludedParams(index As Integer)
        Dim selectedParam As String = lstParamExclusion.GetItem(index)(1).ToString
        dtParamExclusion = DirectCast(lstParamExclusion.DataSource, DataTable)
        Dim dr As DataRow = dtParamExclusion.Select("ParameterName='" & selectedParam & "'")(0)
        dtParamExclusion.Rows.Remove(dr)
        dtParamExclusion.AcceptChanges()

        lstParamExclusion.DataSource = dtParamExclusion
        lstParamExclusion.DisplayMember = "ParameterName"
        lstParamExclusion.ValueMember = "TemplateID"
        lstParamExclusion.Refresh()
    End Sub

    Private Sub LoadParamExclusionList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", Me.copyFromTemplateID}
        }
        strConnection = GetSQL(4194, parray)(0)
        sqlParam = GetSQL(4194, parray)(1)
        dtParamExclusion = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        lstParamExclusion.DataSource = dtParamExclusion
        lstParamExclusion.DisplayMember = "ParameterName"
        lstParamExclusion.ValueMember = "TemplateID"
        lstParamExclusion.Refresh()
    End Sub

    Private Sub LoadFilterStrings()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", Me.copyFromTemplateMOConfigID}
        }
        strConnection = GetSQL(4195, parray)(0)
        sqlParam = GetSQL(4195, parray)(1)
        dtFilter = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(gcMOFilter, gvMOFilter, dtFilter, "ALL",, "FilterString")
    End Sub

#End Region

End Class