Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraTreeList
Imports LidorSystems.IntegralUI.Lists

Public Class dlgCondition

#Region "Variables/Properties"

    Private dtFields As DataTable = Nothing
    Private dtValues As DataTable = Nothing
    Private dtDragValue As New DataTable
    Private dtCondition As DataTable = Nothing
    Dim rangeCount As Integer = 0
    Dim rangeColumn = Nothing

    Public templateID As String
    Public templateMOParamConfigID As String
    Public templateMOConfigID As String
    Public moName As String
    Public sourceMODBName As String
    Public sourceMOTableName As String
    Public paramName As String

#End Region

#Region "Events"

    Private Sub dlgCondition_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'lbl.Text = "<" & Me.sourceMOTableName & ">.<" & Me.paramName & ">"
            btnAddElse.Enabled = False
            txtParamSetValueElse.Enabled = False

            LoadConditionFields()
            AddColumnsToDragGrid()
            AddColumnToConditionGrid()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnAddThen_Click(sender As Object, e As EventArgs) Handles btnAddThen.Click
        Try
            dtCondition.Rows.Clear()
            Dim conditionString As String = GetConditionString()

            Dim dr As DataRow = dtCondition.NewRow()
            dr("Condition") = conditionString

            dtCondition.Rows.Add(dr)
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCondition, gvCondition, dtCondition, "ALL",, "Condition")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnAddElse_Click(sender As Object, e As EventArgs) Handles btnAddElse.Click
        Try
            dtCondition.Rows.Clear()

            Dim dr As DataRow = dtCondition.NewRow()
            dr("Condition") = "ELSE "

            dtCondition.Rows.Add(dr)
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCondition, gvCondition, dtCondition, "ALL",, "Condition")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        Try
            Dim conditionString As String = Nothing
            Dim dr As DataRow = gvConditionFields.GetFocusedDataRow()
            If dr IsNot Nothing Then
                sourceMODBName = dr("databaseName").ToString
                sourceMOTableName = dr("tableName").ToString
            End If

            If dtCondition.Rows.Count = 0 Then
                XtraMessageBox.Show("Please add one or more conditions", "Add Condition", MessageBoxButtons.OK)
                Exit Sub
            End If

            conditionString = gvCondition.GetFocusedDataRow()("Condition").ToString

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOParamConfigID", templateMOParamConfigID},
                New String() {"@ParamName", Chr(39) & paramName & Chr(39)},
                New String() {"@ConditionTable", Chr(39) & sourceMODBName & ".dbo." & sourceMOTableName & Chr(39)},
                New String() {"@ConditionString", Chr(39) & conditionString & Chr(39)},
                New String() {"@ParamSetValue", IIf(conditionString = "ELSE ", Chr(39) & txtParamSetValueElse.Text.Trim & Chr(39), Chr(39) & txtParamSetValue.Text.Trim & Chr(39))}
            }
            strConnection = GetSQL(4130, parray)(0)
            sqlParam = GetSQL(4130, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            XtraMessageBox.Show("Condition added successfully", "Add Condition", MessageBoxButtons.OK)
            frmRefCheck.SaveChangeLog(Me.templateID, Me.moName, templateMOConfigID, "New condition: " & conditionString & " added to the template")
            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gcValue_MouseMove(sender As Object, e As MouseEventArgs) Handles gcValue.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim data As DataRowView = gvValue.GetRow(gvValue.FocusedRowHandle)
                If data IsNot Nothing Then
                    Dim obj() As Object = {data.Item(0)}
                    gcValue.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gcDragValue_DragDrop(sender As Object, e As DragEventArgs) Handles gcDragValue.DragDrop
        Try
            Dim dropValue() As Object = e.Data.GetData("System.Object[]")
            If dropValue IsNot Nothing Then
                Dim rowNumber As Integer = -1
                Dim isNewValue As Boolean = True
                Dim gvDropRowValue As Object = New Object()
                Dim conditionColumnName As String = gvConditionFields.GetFocusedRowCellValue("ColumnName")

                If cmbOperator.Text.Trim() <> "Select Operator" Then

                    If gvDragValue.RowCount = 0 Then

                        Dim dr As DataRow = dtDragValue.NewRow()
                        dr("ColumnName") = conditionColumnName
                        dr("Operator") = cmbOperator.Text.Trim()
                        dr("ColumnValue") = dropValue(0)
                        dtDragValue.Rows.Add(dr)
                        gcDragValue.DataSource = dtDragValue

                    ElseIf (cmbOperator.Text.Trim() = "=" OrElse cmbOperator.Text.Trim() = "<>" OrElse cmbOperator.Text.Trim() = ">" OrElse cmbOperator.Text.Trim() = "<" OrElse cmbOperator.Text.Trim() = "<=" OrElse cmbOperator.Text.Trim() = ">=" OrElse cmbOperator.Text.Trim().ToLower() = "like" OrElse cmbOperator.Text.Trim().ToLower() = "not like") Then
                        For rowNumber = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(rowNumber)
                            If gvDropRowValue(0) = conditionColumnName And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) = CStr(dropValue(0)) Then
                                isNewValue = False
                                Exit For
                            ElseIf gvDropRowValue(0) = conditionColumnName And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) <> CStr(dropValue(0)) Then
                                gvDropRowValue(2) = dropValue(0)
                                isNewValue = False
                            ElseIf gvDropRowValue(0) = conditionColumnName And gvDropRowValue(1) <> cmbOperator.Text.Trim() And gvDropRowValue(1).ToString().ToLower() <> "in" And gvDropRowValue(1) <> "range" And gvDropRowValue(2) = CStr(dropValue(0)) Then
                                gvDropRowValue(1) = cmbOperator.Text.Trim()
                                isNewValue = False
                            ElseIf gvDropRowValue(0) = conditionColumnName And gvDropRowValue(1) <> cmbOperator.Text.Trim() And gvDropRowValue(1).ToString().ToLower() <> "in" And gvDropRowValue(1).ToString().ToLower() <> "range" And gvDropRowValue(2) <> CStr(dropValue(0)) Then
                                gvDropRowValue(2) = dropValue(0)
                                gvDropRowValue(1) = cmbOperator.Text.Trim()
                                isNewValue = False
                            End If
                        Next
                        If isNewValue = True Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("ColumnName") = conditionColumnName
                            dr("Operator") = cmbOperator.Text.Trim()
                            dr("ColumnValue") = dropValue(0)
                            dtDragValue.Rows.Add(dr)
                        End If

                    ElseIf cmbOperator.Text.Trim().ToLower() = "range" Then
                        For i As Integer = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(i)
                            If gvDropRowValue(1).ToLower() = "range" Then
                                rangeCount += 1
                                rangeColumn = gvDropRowValue(0).ToString()
                            ElseIf gvDropRowValue(0) = conditionColumnName Then
                                isNewValue = False
                            End If
                        Next
                        If rangeCount < 2 AndAlso isNewValue = True AndAlso rangeColumn = Nothing Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("ColumnName") = conditionColumnName
                            dr("Operator") = cmbOperator.Text.Trim()
                            dr("ColumnValue") = dropValue(0)
                            dtDragValue.Rows.Add(dr)
                        ElseIf rangeCount < 2 AndAlso isNewValue = True AndAlso rangeColumn = conditionColumnName Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("ColumnName") = conditionColumnName
                            dr("Operator") = cmbOperator.Text.Trim()
                            dr("ColumnValue") = dropValue(0)
                            dtDragValue.Rows.Add(dr)
                        ElseIf rangeCount > 2 Then
                            xtraMessageBox.Show("For range operator only two values are allowed.", "Add Condition")
                        Else
                            XtraMessageBox.Show("For range operator column name should be same.", "Add Condition")
                            rangeCount = 0
                        End If

                    ElseIf ((cmbOperator.Text.Trim().ToLower() = "in") Or (cmbOperator.Text.Trim().ToLower() = "not in")) Then
                        For i As Integer = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(i)
                            If gvDropRowValue(0) = conditionColumnName And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) = CStr(dropValue(0)) Then
                                XtraMessageBox.Show("Column and its value already present. Please choose other one.", "Obj Filter Condition")
                                isNewValue = False
                                Exit For
                            End If
                        Next

                        If isNewValue = True Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("ColumnName") = conditionColumnName
                            dr("Operator") = cmbOperator.Text.ToString
                            dr("ColumnValue") = dropValue(0).ToString
                            dtDragValue.Rows.Add(dr)
                        End If

                    End If
                    IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcDragValue, gvDragValue, dtDragValue, "ALL",, "ColumnValue")
                Else
                    XtraMessageBox.Show("Please select operator!", "Add Condition", MessageBoxButtons.OK)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gcDragValue_DragOver(sender As Object, e As DragEventArgs) Handles gcDragValue.DragOver, gcValue.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub gvConditionFields_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvConditionFields.RowCount > 0 AndAlso e IsNot Nothing Then
                gvConditionFields.ClearSelection()
                gvConditionFields.FocusedRowHandle = e.FocusedRowHandle
                gvConditionFields.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {}

            Dim dr As DataRow = gvConditionFields.GetFocusedDataRow()
            If dr IsNot Nothing Then
                parray = {
                          New String() {"@DatabaseName", Chr(39) & dr("databaseName").ToString & Chr(39)},
                          New String() {"@moTable", Chr(39) & dr("tableName").ToString & Chr(39)},
                          New String() {"@columnName", Chr(39) & dr("ColumnName") & Chr(39)}
                }
                strConnection = GetSQL(4120, parray)(0)
                sqlParam = GetSQL(4120, parray)(1)

                dtValues = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcValue, gvValue, dtValues, "ALL",, dr("ColumnName"))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtSearchField_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchField.KeyUp
        Try
            If dtFields IsNot Nothing Then
                If (txtSearchField.Text.Length > 2) Then
                    dtFields.DefaultView.RowFilter = "ColumnName Like '%" + txtSearchField.Text + "%'"
                Else
                    dtFields.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchValue_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchValue.KeyUp
        Try
            If dtValues IsNot Nothing Then
                If (txtSearchValue.Text.Length > 2) Then
                    Dim colName As String = gvConditionFields.GetFocusedRowCellValue("ColumnName")
                    dtValues.DefaultView.RowFilter = colName & " Like '%" + txtSearchValue.Text + "%'"
                Else
                    dtValues.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub AddColumnsToDragGrid()
        gvDragValue.Columns.Clear()

        dtDragValue = New DataTable()
        dtDragValue.Columns.Add("ColumnName")
        dtDragValue.Columns.Add("Operator")
        dtDragValue.Columns.Add("ColumnValue")
    End Sub

    Private Sub AddColumnToConditionGrid()
        gvCondition.Columns.Clear()

        dtCondition = New DataTable()
        dtCondition.Columns.Add("Condition")
    End Sub

    Private Sub LoadConditionFields()
        RemoveHandler gvConditionFields.FocusedRowChanged, AddressOf gvConditionFields_FocusedRowChanged
        Dim strConnection As String
        Dim sqlParam As String

        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", "'" & templateMOConfigID & "'"}
        }

        strConnection = GetSQL(4119, parray)(0)
        sqlParam = GetSQL(4119, parray)(1)
        dtFields = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"databaseName", "schemaName", "tableName"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcConditionFields, gvConditionFields, dtFields, "ALL", columnsToHide, "ColumnName")
        AddHandler gvConditionFields.FocusedRowChanged, AddressOf gvConditionFields_FocusedRowChanged
        gvConditionFields_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Function GetConditionString() As String
        Dim conditionString As String = Nothing
        Dim rangeRowCounter As Integer = 0

        Dim inRowCounter As Integer = 0
        Dim inFilterStr As String = ""
        Dim inFilterValueStr As String = ""
        Dim objName As String = "[" & gvConditionFields.GetFocusedRowCellValue("tableName").ToString & "]"

        For i As Integer = 0 To gvDragValue.RowCount - 1
            Dim gvRowValue = gvDragValue.GetRow(i)
            If (gvRowValue("Operator").ToString() = "=" Or gvRowValue("Operator").ToString() = "<>" Or gvRowValue("Operator").ToString() = "<" Or gvRowValue("Operator").ToString() = ">" Or gvRowValue("Operator").ToString() = "<=" Or gvRowValue("Operator").ToString() = ">=") Then
                conditionString &= objName & ".[" & gvRowValue("ColumnName").ToString() & "] " & gvRowValue("Operator").ToString() & " ''" & gvRowValue("ColumnValue").ToString() & "'' "
                If gvDragValue.RowCount - 1 > i Then
                    conditionString &= " AND "
                End If
            ElseIf (gvRowValue("Operator").ToString().ToLower() = "like") Or (gvRowValue("Operator").ToString().ToLower() = "not like") Then
                conditionString &= objName & ".[" & gvRowValue("ColumnName").ToString() & "] " & gvRowValue("Operator").ToString().ToUpper() & " ''%" & gvRowValue("ColumnValue").ToString() & "%''"
                If gvDragValue.RowCount - 1 > i Then
                    conditionString &= " AND "
                End If
            ElseIf (gvRowValue("Operator").ToString().ToLower = "range") Then
                If rangeRowCounter = 0 Then
                    conditionString = objName & ".[" & gvRowValue("ColumnName").ToString() & "] BETWEEN " & gvRowValue("ColumnValue").ToString() & " AND "
                End If
                rangeRowCounter = rangeRowCounter + 1
                If rangeRowCounter = 2 Then
                    conditionString &= gvRowValue("ColumnValue").ToString() & " AND "
                    rangeRowCounter = rangeRowCounter + 1
                End If
            ElseIf (gvRowValue("Operator").ToString().ToLower() = "in") Or (gvRowValue("Operator").ToString().ToLower() = "not in") Then
                If inRowCounter = 0 Then
                    inFilterStr = objName & ".[" & gvRowValue("ColumnName").ToString() & "] " & gvRowValue("Operator").ToString().ToUpper() & " ("
                End If
                inRowCounter = inRowCounter + 1
                If inRowCounter = 1 Then
                    inFilterValueStr &= "''" & gvRowValue("ColumnValue").ToString() & "''"
                ElseIf inRowCounter > 1 Then
                    inFilterValueStr &= ",''" & gvRowValue("ColumnValue").ToString() & "''"
                End If
            End If
        Next

        If inFilterStr <> "" Then
            inFilterValueStr = inFilterValueStr.TrimEnd(",")
            conditionString &= inFilterStr & inFilterValueStr & ")"
        End If

        If rangeRowCounter > 0 Then
            conditionString = conditionString.Substring(0, conditionString.Length - 4)
        End If

        Return conditionString
    End Function

#End Region

End Class