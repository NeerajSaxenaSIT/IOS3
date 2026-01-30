Imports IOS.Library
Imports IOS.DataLibrary

Public Class frmRefChkBulkUpdate

#Region "Variables"

    Private dtSearchFiltered As DataTable = Nothing

    Public vendor As String = Nothing
    Public itemType As String = Nothing
    Public strSearchGridFilter As String = Nothing

#End Region

#Region "Events"

    Private Sub frmRefChkBulkUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtTemp As DataTable = Nothing
            If itemType.ToLower = "filterstring" Then
                lblUpdateInfo.Text = lblUpdateInfo.Text & " Filter String" & vbCrLf & vbCrLf & strSearchGridFilter.Replace(" And ", vbCrLf)
                If dtFilterStrings IsNot Nothing AndAlso dtFilterStrings.Rows.Count > 0 Then
                    dtSearchFiltered = dtFilterStrings.Select(strSearchGridFilter).CopyToDataTable
                    dtTemp = dtSearchFiltered.DistinctCol({"FilterString"})
                    BindDevExComboBoxWithValueMember(cmbItem, dtTemp, "FilterString", "FilterString", "Select")
                    tlpMain.RowStyles(1).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(1).Height = 28
                    lblSetNewValue.Text = "Set New Value"
                    lblSetNewValue.Visible = True
                    txtNewValue.Visible = True
                    tlpMain.RowStyles(2).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(2).Height = 0
                    rdoInclusion.Visible = False
                    rdoExclusion.Visible = False
                    btnBulkUpdate.Text = "Update"
                End If
            ElseIf itemType.ToLower = "incexcobject" Then
                lblUpdateInfo.Text = lblUpdateInfo.Text & " Inclusion/Exclusion Objects" & vbCrLf & vbCrLf & strSearchGridFilter.Replace(" And ", vbCrLf)
                If dtIncExcObjects IsNot Nothing AndAlso dtIncExcObjects.Rows.Count > 0 Then
                    dtSearchFiltered = dtIncExcObjects.Select(strSearchGridFilter).CopyToDataTable
                    'dtTemp = dtSearchFiltered.DistinctCol({"ListID", "ListName"})
                    'BindDevExComboBoxWithValueMember(cmbItem, dtTemp, "ListID", "ListName", "Select")
                    FillList()
                    tlpMain.RowStyles(1).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(1).Height = 0
                    lblSetNewValue.Visible = False
                    txtNewValue.Visible = False
                    tlpMain.RowStyles(2).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(2).Height = 28
                    rdoInclusion.Visible = True
                    rdoExclusion.Visible = True
                    btnBulkUpdate.Text = "Add"
                End If
            ElseIf itemType.ToLower = "exclusionparam" Then
                lblUpdateInfo.Text = lblUpdateInfo.Text & " Excluded Parameters" & vbCrLf & vbCrLf & strSearchGridFilter.Replace(" And ", vbCrLf)
                If dtExcludedParams IsNot Nothing AndAlso dtExcludedParams.Rows.Count > 0 Then
                    dtSearchFiltered = dtExcludedParams.Select(strSearchGridFilter).CopyToDataTable
                    'dtTemp = dtSearchFiltered.DistinctCol({"ParameterName"})
                    'BindDevExComboBoxWithValueMember(cmbItem, dtTemp, "ParameterName", "ParameterName", "Select")
                    LoadParamToExcludeForSelectedMO()
                    tlpMain.RowStyles(0).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(0).Height = 0
                    lblCombo.Visible = False
                    cmbItem.Visible = False
                    tlpMain.RowStyles(1).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(1).Height = 28
                    lblSetNewValue.Text = "Enter Param Name"
                    lblSetNewValue.Visible = True
                    txtNewValue.Visible = True
                    tlpMain.RowStyles(2).SizeType = SizeType.Absolute
                    tlpMain.RowStyles(2).Height = 0
                    lblListType.Visible = False
                    rdoInclusion.Visible = False
                    rdoExclusion.Visible = False
                    btnBulkUpdate.Text = "Add"
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

    Private Sub btnBulkUpdate_Click(sender As Object, e As EventArgs) Handles btnBulkUpdate.Click
        Try
            If itemType.ToLower = "filterstring" Then
                If cmbItem.SelectedIndex = 0 Then
                    SetMessage("Please Select Filter String To Update")
                    cmbItem.Focus()
                    Exit Sub
                ElseIf txtNewValue.Text.Trim = String.Empty Then
                    SetMessage("Please Set Filter String")
                    txtNewValue.Focus()
                    Exit Sub
                End If
            ElseIf itemType.ToLower = "incexcobject" Then
                If cmbItem.SelectedIndex = 0 Then
                    SetMessage("Please Select List Item To Add")
                    cmbItem.Focus()
                    Exit Sub
                End If
            ElseIf itemType.ToLower = "exclusionparam" Then
                If txtNewValue.Text.Trim = String.Empty Then
                    SetMessage("Please Select Parameter To Add")
                    txtNewValue.Focus()
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            BulkUpdateItemValue()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
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

#End Region

#Region "Methods"

    Private Sub BulkUpdateItemValue()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        If itemType.ToLower = "filterstring" Then
            Dim foundRows As DataRow() = dtSearchFiltered.Select("FilterString='" & cmbItem.SelectedItem.ToString.Replace("'", "''") & "'")
            If foundRows.Length > 0 Then
                For Each dr As DataRow In foundRows
                    Dim parray()() As String = {
                        New String() {"@FilterString", Chr(39) & txtNewValue.Text.Trim.Replace("'", "''") & Chr(39)},
                        New String() {"@TemplateMOFilterID", CInt(dr("TemplateMOFilterID"))},
                        New String() {"@TemplateMOConfigID", CInt(dr("TemplateMOConfigID"))}
                    }
                    strConnection = GetSQL(4209, parray)(0)
                    sqlParam = GetSQL(4209, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                Next
            End If
        ElseIf itemType.ToLower = "incexcobject" Then
            Dim dtTemp As DataTable = dtSearchFiltered.DistinctCol("TemplateID")
            If dtTemp.Rows.Count > 0 Then
                For Each drTemp As DataRow In dtTemp.Rows
                    Dim parray()() As String = {
                        New String() {"@TemplateID", CInt(drTemp("TemplateID"))},
                        New String() {"@ListID", CInt(CType(cmbItem.SelectedItem, clsComboBoxItem).Value)},
                        New String() {"@ListType", IIf(rdoInclusion.Checked, Chr(39) & "Inclusion" & Chr(39), Chr(39) & "Exclusion" & Chr(39))}
                    }
                    strConnection = GetSQL(4210, parray)(0)
                    sqlParam = GetSQL(4210, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                Next
            End If
        ElseIf itemType.ToLower = "exclusionparam" Then
            Dim dtTemp As DataTable = dtSearchFiltered.DistinctCol("TemplateID")
            If dtTemp.Rows.Count > 0 Then
                For Each drTemp As DataRow In dtTemp.Rows
                    Dim parray()() As String = {
                        New String() {"@templateID", CInt(drTemp("TemplateID"))},
                        New String() {"@parameterName", Chr(39) & txtNewValue.Text.Trim & Chr(39)}
                    }
                    strConnection = GetSQL(4166, parray)(0)
                    sqlParam = GetSQL(4166, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                Next
            End If
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

    Private Sub FillList()
        Dim strConnection As String, sqlParam As String
        strConnection = GetSQL(4512, Nothing)(0)
        sqlParam = GetSQL(4512, Nothing)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbItem, dt, "ListID", "ListName", "Select")
    End Sub

    Private Sub LoadParamToExcludeForSelectedMO()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@vendor", Chr(39) & Me.vendor & Chr(39)},
            New String() {"@moTable", Chr(39) & "" & Chr(39)}
        }
        strConnection = GetSQL(4175, parray)(0)
        sqlParam = GetSQL(4175, parray)(1)
        GetTextboxDataWithAutoCompleteFeature(txtNewValue, sqlParam)
    End Sub

#End Region

End Class