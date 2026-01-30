Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.XtraLayout
Imports IOS.Library

Public Class dlgRefChkAddMO

    Public templateID As String = Nothing
    Public templateName As String = Nothing
    Public vendor As String = Nothing
    Dim paramName As String = Nothing
    Private dtMO As DataTable
    Private dtParam As DataTable
    Dim FindButton As SimpleButton
    Private tb As Control

    Private Sub dlgRefChkAddMO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblTemplateName.Text = templateName
            lblVendor.Text = vendor
            If ceSetAutoValue.Checked = False Then
                txtCommonalityValue.Enabled = False
            End If
            LoadMOCombo()
            LoadCopyFilerFromMOCombo()
            AddHandler rdoMOBased.CheckedChanged, AddressOf rdoMOBased_CheckedChanged
            AddHandler rdoParamBased.CheckedChanged, AddressOf rdoParamBased_CheckedChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub LoadCopyFilerFromMOCombo()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateID}
        }
        strConnection = GetSQL(4187, parray)(0)
        sqlParam = GetSQL(4187, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        BindDevExLookUpEdit(cmbCopyFilterFromMO, dt, "TemplateMOConfigID", "MOName")
        cmbCopyFilterFromMO.Properties.Columns(0).Visible = False
    End Sub

    Private Sub LoadMOCombo()
        RemoveHandler cmbMO.EditValueChanged, AddressOf cmbMO_EditValueChanged
        'RemoveHandler cmbMO.SelectedIndexChanged, AddressOf cmbMO_SelectedIndexChanged
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@Vendor", Chr(39) & vendor & Chr(39)}
        }
        strConnection = GetSQL(4189, parray)(0)
        sqlParam = GetSQL(4189, parray)(1)
        dtMO = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        BindDevExLookUpEdit(cmbMO, dtMO, "TABLE_NAME", "MONAME")

        cmbMO.Properties.Columns(0).Visible = False
        'AddHandler cmbMO.SelectedIndexChanged, AddressOf cmbMO_SelectedIndexChanged
        AddHandler cmbMO.EditValueChanged, AddressOf cmbMO_EditValueChanged
    End Sub

    Private Sub cmbMO_EditValueChanged(sender As Object, e As EventArgs)
        Try
            cmbMO.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbMO.EditValue <> "" AndAlso cmbMO.EditValue <> "Select MO" Then
                lblSelectedMO.Text = cmbMO.Text.ToString.Replace("MO_2G_", "").Replace("MO_3G_", "").Replace("MO_4G_", "").Replace("MO_5G_", "") 'TryCast(cmbMO.SelectedItem, clsComboBoxItem).Value
                LoadCopyFromObjectsForSelectedMO(txtCopyFromObject, cmbMO.EditValue)
                GetPrimaryKeyColumnsForSelectedMO(cmbMO.EditValue)
            Else
                cmbMO.SelectedText = "Select MO"
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            cmbMO.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadCopyFromObjectsForSelectedMO(ByRef txt As TextEdit, ByVal moTable As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@moTable", Chr(39) & moTable & Chr(39)}
        }
        strConnection = GetSQL(4160, parray)(0)
        sqlParam = GetSQL(4160, parray)(1)
        GetTextboxDataWithAutoCompleteFeature(txt, sqlParam)
    End Sub

    Private Sub GetPrimaryKeyColumnsForSelectedMO(ByVal moTable As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@moTable", Chr(39) & moTable & Chr(39)}
        }
        strConnection = GetSQL(4161, parray)(0)
        sqlParam = GetSQL(4161, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblObjIdentifier.Text = dt.Rows(0)(0).ToString
        End If
    End Sub

    Private Sub LoadMOParamCombo(paramName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@paramName", Chr(39) & paramName & Chr(39)},
            New String() {"@vendor", Chr(39) & vendor & Chr(39)}
        }
        strConnection = GetSQL(4188, parray)(0)
        sqlParam = GetSQL(4188, parray)(1)
        dtParam = DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        Dim dtTemp As DataTable = dtParam.Copy()
        dtTemp.Columns.Remove("MO_NAME")

        searchLookupEditParam.Properties.DataSource = dtTemp
        searchLookupEditParam.Properties.DisplayMember = "PARAMNAME, MONAME"
        searchLookupEditParam.Properties.ValueMember = "MONAME"

        For Each dtCol As DataColumn In dtTemp.Columns
            searchLookupEditParam.Properties.PopupView.Columns(dtCol.ColumnName).BestFit()
        Next
    End Sub

    Private Sub searchLookUpEdit1_Popup(ByVal sender As Object, ByVal e As EventArgs) Handles searchLookupEditParam.Popup
        Try
            Dim popupControl As DevExpress.Utils.Win.IPopupControl = TryCast(sender, DevExpress.Utils.Win.IPopupControl)
            Dim layoutControl As LayoutControl = TryCast(popupControl.PopupWindow.Controls(3).Controls(0), LayoutControl)
            FindButton = TryCast(CType(layoutControl.Items.FindByName("lciButtonFind"), LayoutControlItem).Control, SimpleButton)
            'Find button
            If FindButton IsNot Nothing Then
                RemoveHandler FindButton.Click, AddressOf FindButton_Click
                AddHandler FindButton.Click, AddressOf FindButton_Click
            End If
            'Find textbox
            Dim frm As PopupSearchLookUpEditForm = TryCast((TryCast(sender, DevExpress.Utils.Win.IPopupControl)).PopupWindow, PopupSearchLookUpEditForm)
            tb = frm.Controls.Find("teFind", True)(0)
        Catch
        End Try
    End Sub

    Private Sub FindButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim searchParamKeyWord As String = TryCast(tb, TextEdit).Text
        LoadMOParamCombo(searchParamKeyWord)
    End Sub

    Private Sub searchLookUpEdit1_QueryCloseUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles searchLookupEditParam.QueryCloseUp
        Try
            Dim popupControl As DevExpress.Utils.Win.IPopupControl = TryCast(sender, DevExpress.Utils.Win.IPopupControl)
            Dim layoutControl As LayoutControl = TryCast(popupControl.PopupWindow.Controls(2).Controls(0), LayoutControl)
            FindButton = TryCast(CType(layoutControl.Items.FindByName("lciButtonFind"), LayoutControlItem).Control, SimpleButton)
            If FindButton IsNot Nothing Then
                e.Cancel = False
                RemoveHandler FindButton.Click, AddressOf FindButton_Click
            Else
                e.Cancel = True
            End If
        Catch
        End Try
    End Sub

    Private Sub SearchLookUpEdit1_Closed(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles searchLookupEditParam.Closed
        Try
            Dim searchLookUpEdit = TryCast(sender, SearchLookUpEdit)
            Dim view = searchLookUpEdit.Properties.View
            Dim selectedRows() As Integer = view.GetSelectedRows()
            If selectedRows.Count > 0 Then
                searchLookUpEdit.EditValue = view.GetRowCellValue(selectedRows(0), searchLookUpEdit.Properties.ValueMember)
                If searchLookUpEdit.EditValue IsNot Nothing Then
                    paramName = view.GetRowCellValue(selectedRows(0), "PARAMNAME")
                    lblSelectedMO.Text = searchLookUpEdit.EditValue.ToString.Replace("MO_2G_", "").Replace("MO_3G_", "").Replace("MO_4G_", "").Replace("MO_5G_", "")
                    LoadCopyFromObjectsForSelectedMO(txtCopyFromObject, dtParam.Select("MONAME='" & searchLookUpEdit.EditValue.ToString & "' AND PARAMNAME='" & paramName & "'")(0)("MO_NAME"))
                    GetPrimaryKeyColumnsForSelectedMO(dtParam.Select("MONAME='" & searchLookUpEdit.EditValue.ToString & "' AND PARAMNAME='" & paramName & "'")(0)("MO_NAME"))
                    searchLookUpEdit.ResetText() ' = lblSelectedMO.Text
                End If
            Else
                searchLookUpEdit.EditValue = Nothing
                lblSelectedMO.Text = ""
                lblObjIdentifier.Text = ""
            End If
        Catch
        End Try
    End Sub

    Private Sub rdoMOBased_CheckedChanged(sender As Object, e As EventArgs)
        If rdoMOBased.Checked = True Then
            LoadMOCombo()
            rdoParamBased.Checked = False
            searchLookupEditParam.Enabled = False
            cmbMO.Enabled = True
            cmbMO.SelectedText = "Select MO"
            searchLookupEditParam.Properties.DataSource = Nothing
            lblSelectedMO.Text = ""
            lblObjIdentifier.Text = ""
        End If
    End Sub

    Private Sub rdoParamBased_CheckedChanged(sender As Object, e As EventArgs)
        If rdoParamBased.Checked = True Then
            rdoMOBased.Checked = False
            searchLookupEditParam.Enabled = True
            'cmbMO.SelectedIndex = 0
            cmbMO.EditValue = ""
            cmbMO.Enabled = False
            lblSelectedMO.Text = ""
            lblObjIdentifier.Text = ""
        End If
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
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

    Private Sub btnAddMO_Click(sender As Object, e As EventArgs) Handles btnAddMO.Click
        Try
            If (lblSelectedMO.Text = "") Then
                SetMessage("Please select MO")
                Exit Sub
            End If

            If (ceSetAutoValue.Checked = True) Then
                If (txtCommonalityValue.Text = "") Then
                    SetMessage("Please enter commonality value between 0 and 100")
                    Exit Sub
                End If
            End If

            If cmbPriority.Text = "Select Priority" Then
                SetMessage("Please select priority")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim isAllParameters As Integer = IIf(ceSetAutoValue.Checked = True, 1, 0)
            Dim moTableName As String = Nothing

            If rdoParamBased.Checked Then
                moTableName = dtParam.Select("MONAME='" & lblSelectedMO.Text.ToString & "' AND PARAMNAME='" & paramName & "'")(0)("MO_NAME")
            ElseIf rdoMOBased.Checked Then
                moTableName = cmbMO.EditValue.ToString  'TryCast(cmbMO.SelectedItem, clsComboBoxItem).Value
            End If

            If txtCopyFromObject.Text.Trim <> "" Then
                isAllParameters = 1
                ceSetAutoValue.Checked = False
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateID", templateID},
                New String() {"@VendorTech", Chr(39) & vendor & Chr(39)},
                New String() {"@MO_Name", Chr(39) & lblSelectedMO.Text.ToString & Chr(39)},
                New String() {"@MO_Table", Chr(39) & moTableName & Chr(39)},
                New String() {"@MO_Database", Chr(39) & "data_" + Split(vendor, " ")(0).TrimEnd + "_CM" & Chr(39)},
                New String() {"@isAllParameters", isAllParameters},
                New String() {"@isAutoSetValue", IIf(ceSetAutoValue.Checked = True, 1, 0)},
                New String() {"@CommonalityValue", Chr(39) & txtCommonalityValue.Text.Trim & Chr(39)},
                New String() {"@isActive", IIf(ceIsEnabled.Checked = True, 1, 0)},
                New String() {"@copyFromObject", IIf(txtCopyFromObject.Text = "", "NULL", Chr(39) & txtCopyFromObject.Text.Trim & Chr(39))},
                New String() {"@copyFilterFromMOConfigID", IIf(cmbCopyFilterFromMO.Text = "Select MO", "NULL", CInt(cmbCopyFilterFromMO.EditValue))},
                New String() {"priority", Chr(39) & cmbPriority.SelectedItem.ToString & Chr(39)}
            }
            strConnection = GetSQL(4109, parray)(0)
            sqlParam = GetSQL(4109, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            'Update LatestConfigUpdate column in templates table
            strConnection = Nothing
            sqlParam = Nothing
            parray = {
                New String() {"@TemplateID", templateID}
            }
            strConnection = GetSQL(4183, parray)(0)
            sqlParam = GetSQL(4183, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            'save change log for new mo added
            frmRefCheck.SaveChangeLog(templateID, lblSelectedMO.Text.ToString, 0, "New MO Added: " & lblSelectedMO.Text.ToString)

            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtCommonalityValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCommonalityValue.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub ceSetAutoValue_CheckedChanged(sender As Object, e As EventArgs) Handles ceSetAutoValue.CheckedChanged
        If ceSetAutoValue.Checked = False Then
            txtCommonalityValue.Enabled = False
            txtCommonalityValue.Text = ""
            txtCopyFromObject.Enabled = True
        Else
            txtCommonalityValue.Enabled = True
            txtCopyFromObject.Enabled = False
            txtCopyFromObject.Text = ""
        End If
    End Sub

    Private Sub ceIsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles ceIsEnabled.CheckedChanged
        If ceIsEnabled.Checked = False Then
            txtCopyFromObject.Enabled = False
            txtCopyFromObject.Text = ""
        Else
            txtCopyFromObject.Enabled = True
        End If
    End Sub

End Class