Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.XtraForm
Imports DevExpress.XtraEditors

Public Class dlgObjFilter

#Region "Variables"

    Public filterType As String
    Public templateID As Integer = 0
    Private targetTable As String
    Private targetColumn As String

    Private templateMOConfigID As Integer = 0
    Private capCongestionRuleID As Integer = 0
    Private nbiReportID As Integer = 0
    Private capDatabaseName As String

    Public moDatabaseName As String
    Public moTable As String
    Public moName As String
    Public joinTable As String
    Public excludedColumns As String

    Dim dtDragValue As DataTable = New DataTable()
    Dim rangeCount As Integer = 0
    Dim rangeColumn = Nothing
    Dim dtField = New System.Data.DataTable()
    Dim dtColumnValue As DataTable = New System.Data.DataTable()

    Private kpiRuleID As Integer = 0
    Public AlertRuleID As Integer = 0
    Public dtAlertConfig As DataTable = Nothing

#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal _filterType As String, ByVal _ID As Integer)     ', ByVal TableName As String, ByVal ColumnName As String
        'targetTable = TableName
        'targetColumn = ColumnName
        filterType = _filterType

        If filterType.ToUpper = "MOCONFIG" Then
            Me.templateMOConfigID = _ID
        ElseIf filterType.ToUpper = "CONGESTIONRULE" Then
            Me.capCongestionRuleID = _ID
        ElseIf filterType.ToUpper = "MO4TEMPLATE" Then
            'Do Nothing
        ElseIf filterType.ToUpper = "NBIREPORT" Then
            Me.nbiReportID = _ID
        ElseIf filterType.ToUpper = "ANOKPIRULES" Then
            Me.kpiRuleID = _ID
        End If
        InitializeComponent()
    End Sub

#Region "Form Events"

    Private Sub dlgObjFilter_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnAddFilter.Enabled = True
            btnAddFilter2AllMO.Enabled = True
            btnAddFilter2AllMO.Visible = True
            btnAddFilterAllKpiRules.Enabled = True
            btnAddFilterAllKpiRules.Visible = True

            If filterType.ToUpper = "MOCONFIG" Then
                LoadObjectFilterFieldsForTemplateMOConfig(templateMOConfigID)
                btnAddFilterAllKpiRules.Visible = False
            ElseIf filterType.ToUpper = "CONGESTIONRULE" Then
                btnAddFilter2AllMO.Enabled = False
                btnAddFilterAllKpiRules.Visible = False
                LoadObjectFilterFieldsForCongestionRule(capCongestionRuleID)
            ElseIf filterType.ToUpper = "MO4TEMPLATE" Then
                btnAddFilter.Enabled = False
                btnAddFilterAllKpiRules.Visible = False
                LoadObjectFilterFieldsForMO()
            ElseIf filterType.ToUpper = "NBIREPORT" Then
                btnAddFilter2AllMO.Visible = False
                btnAddFilterAllKpiRules.Visible = False
                LoadObjectFilterFieldsForNBIReport(nbiReportID)
            ElseIf filterType.ToUpper = "ANOKPIRULES" Then
                btnAddFilter2AllMO.Visible = False
                btnAddFilterAllKpiRules.Visible = True
                LoadObjectFilterFieldsForKPIRules(kpiRuleID)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvField_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvField.GetFocusedDataRow()
            Dim objField As String = Nothing

            If dr IsNot Nothing Then
                objField = dr.Item(0).ToString
                If filterType.ToUpper = "MOCONFIG" Or filterType.ToUpper = "MO4TEMPLATE" Then
                    LoadTemplateMOConfigFieldValues(dr(0).ToString(), dr.Item(2).ToString(), dr.Item(3).ToString())
                ElseIf filterType.ToUpper = "CONGESTIONRULE" Or filterType.ToUpper = "NBIREPORT" Or filterType.ToUpper = "ANOKPIRULES" Then
                    LoadCapCongestionRuleFieldValues(dr.Item(2).ToString(), dr.Item(3).ToString())
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvField_RowClick(sender As Object, e As Views.Grid.RowClickEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dr As DataRow = gvField.GetRow(e.RowHandle).Row
            Dim objField As String = Nothing

            If dr IsNot Nothing Then
                objField = dr.Item(0).ToString
                If filterType.ToUpper = "MOCONFIG" Or filterType.ToUpper = "MO4TEMPLATE" Then
                    LoadTemplateMOConfigFieldValues(dr.Item(0).ToString(), dr.Item(2).ToString(), dr.Item(3).ToString())
                ElseIf filterType.ToUpper = "CONGESTIONRULE" Or filterType.ToUpper = "NBIREPORT" Or filterType.ToUpper = "ANOKPIRULES" Then
                    LoadCapCongestionRuleFieldValues(dr.Item(2).ToString(), dr.Item(3).ToString())
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gcColumnValue_MouseMove(sender As Object, e As MouseEventArgs) Handles gcColumnValue.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim data As DataRow = gvColumnValue.GetFocusedDataRow()
                If data IsNot Nothing Then
                    Dim obj() As Object = {gvColumnValue.FocusedColumn.FieldName, data.Item(0)}
                    gcColumnValue.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gcDragValue_DragDrop(sender As Object, e As DragEventArgs) Handles gcDragValue.DragDrop
        Dim dropValue() As Object = e.Data.GetData("System.Object[]")
        If dropValue IsNot Nothing Then
            Dim rowNumber As Integer
            Dim isNewValue As Boolean = True
            Dim gvDropRowValue As Object = New Object()
            Dim drField As DataRow = gvField.GetFocusedDataRow()

            If cmbOperator.Text.Trim() <> "Select Operator" Then

                If filterType.ToUpper = "MOCONFIG" Or filterType.ToUpper = "MO4TEMPLATE" Or filterType.ToUpper = "ANOKPIRULES" Then

                    If gvDragValue.RowCount = 0 Then
                        dtDragValue.Columns.Add("Schema", System.Type.GetType("System.String"))
                        dtDragValue.Columns.Add("Parameter", System.Type.GetType("System.String"))
                        dtDragValue.Columns.Add("Op", System.Type.GetType("System.String"))
                        dtDragValue.Columns.Add("Value", System.Type.GetType("System.String"))

                        Dim dr As DataRow = dtDragValue.NewRow()
                        dr("Schema") = drField("databaseName") & "." & drField("schemaName") & "." & drField("tableName") & "."
                        dr("Parameter") = dropValue(0)
                        dr("Op") = cmbOperator.Text.Trim()
                        dr("Value") = dropValue(1)
                        dtDragValue.Rows.Add(dr)
                        gcDragValue.DataSource = dtDragValue

                    ElseIf (cmbOperator.Text.Trim() = "=" OrElse cmbOperator.Text.Trim() = "<>" OrElse cmbOperator.Text.Trim() = ">" OrElse cmbOperator.Text.Trim() = "<" OrElse cmbOperator.Text.Trim() = "<=" OrElse cmbOperator.Text.Trim() = ">=" OrElse cmbOperator.Text.Trim().ToLower() = "like" OrElse cmbOperator.Text.Trim().ToLower() = "not like") Then
                        For rowNumber = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(rowNumber)
                            If gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) = CStr(dropValue(1)) Then
                                'XtraMessageBox.Show("Column and its value already present. Please choose other one.")
                                isNewValue = False
                                Exit For
                            ElseIf gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) <> CStr(dropValue(1)) Then
                                'XtraMessageBox.Show("You can change column's value and its operator only.")
                                gvDropRowValue(2) = dropValue(1)
                                isNewValue = False
                            ElseIf gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) <> cmbOperator.Text.Trim() And gvDropRowValue(1).ToString().ToLower() <> "in" And gvDropRowValue(1) <> "range" And gvDropRowValue(2) = CStr(dropValue(1)) Then
                                gvDropRowValue(1) = cmbOperator.Text.Trim()
                                isNewValue = False
                            ElseIf gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) <> cmbOperator.Text.Trim() And gvDropRowValue(1).ToString().ToLower() <> "in" And gvDropRowValue(1).ToString().ToLower() <> "range" And gvDropRowValue(2) <> CStr(dropValue(1)) Then
                                gvDropRowValue(2) = dropValue(1)
                                gvDropRowValue(1) = cmbOperator.Text.Trim()
                                isNewValue = False
                            End If
                        Next
                        If isNewValue = True Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Schema") = drField("databaseName") & "." & drField("schemaName") & "." & drField("tableName") & "."
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        End If

                    ElseIf cmbOperator.Text.Trim().ToLower() = "range" Then
                        For i As Integer = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(i)
                            If gvDropRowValue(1).ToLower() = "range" Then
                                rangeCount += 1
                                rangeColumn = gvDropRowValue(0).ToString()
                            ElseIf gvDropRowValue(0) = dropValue(0) Then
                                isNewValue = False
                            End If
                        Next
                        If rangeCount < 2 AndAlso isNewValue = True AndAlso rangeColumn = Nothing Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Schema") = drField("databaseName") & "." & drField("schemaName") & "." & drField("tableName") & "."
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        ElseIf rangeCount < 2 AndAlso isNewValue = True AndAlso rangeColumn = dropValue(0).ToString() Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Schema") = drField("databaseName") & "." & drField("schemaName") & "." & drField("tableName") & "."
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        ElseIf rangeCount > 2 Then
                            XtraMessageBox.Show("For range operator only two values are allowed.", "Obj Filter Condition")
                        Else
                            XtraMessageBox.Show("For range operator column name should be same.", "Obj Filter Condition")
                            rangeCount = 0
                        End If

                    ElseIf ((cmbOperator.Text.Trim().ToLower() = "in") Or (cmbOperator.Text.Trim().ToLower() = "not in")) Then
                        For i As Integer = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(i)
                            If gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) = dropValue(1) Then
                                XtraMessageBox.Show("Column and its value already present. Please choose other one.", "Obj Filter Condition")
                                isNewValue = False
                                Exit For
                            End If
                        Next
                        If isNewValue = True Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Schema") = drField("databaseName") & "." & drField("schemaName") & "." & drField("tableName") & "."
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        End If
                    End If

                    Dim columnsToHide() As String = {"Schema"}
                    IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcDragValue, gvDragValue, dtDragValue, "ALL", columnsToHide, "Value")

                ElseIf filterType.ToUpper = "CONGESTIONRULE" Or filterType.ToUpper = "NBIREPORT" Then

                    If gvDragValue.RowCount = 0 Then
                        dtDragValue.Columns.Add("Parameter", System.Type.GetType("System.String"))
                        dtDragValue.Columns.Add("Op", System.Type.GetType("System.String"))
                        dtDragValue.Columns.Add("Value", System.Type.GetType("System.String"))

                        Dim dr As DataRow = dtDragValue.NewRow()
                        dr("Parameter") = dropValue(0)
                        dr("Op") = cmbOperator.Text.Trim()
                        dr("Value") = dropValue(1)
                        dtDragValue.Rows.Add(dr)
                        gcDragValue.DataSource = dtDragValue

                    ElseIf (cmbOperator.Text.Trim() = "=" OrElse cmbOperator.Text.Trim() = "<>" OrElse cmbOperator.Text.Trim() = ">" OrElse cmbOperator.Text.Trim() = "<" OrElse cmbOperator.Text.Trim() = "<=" OrElse cmbOperator.Text.Trim() = ">=" OrElse cmbOperator.Text.Trim().ToLower() = "like" OrElse cmbOperator.Text.Trim().ToLower() = "not like") Then
                        For rowNumber = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(rowNumber)
                            If gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) = dropValue(1) Then
                                'XtraMessageBox.Show("Column and its value already present. Please choose other one.")
                                isNewValue = False
                                Exit For
                            ElseIf gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) <> dropValue(1) Then
                                'XtraMessageBox.Show("You can change column's value and its operator only.")
                                gvDropRowValue(2) = dropValue(1)
                                isNewValue = False
                            ElseIf gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) <> cmbOperator.Text.Trim() And gvDropRowValue(1).ToString().ToLower() <> "in" And gvDropRowValue(1) <> "range" And gvDropRowValue(2) = dropValue(1) Then
                                gvDropRowValue(1) = cmbOperator.Text.Trim()
                                isNewValue = False
                            ElseIf gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) <> cmbOperator.Text.Trim() And gvDropRowValue(1).ToString().ToLower() <> "in" And gvDropRowValue(1).ToString().ToLower() <> "range" And gvDropRowValue(2) <> dropValue(1) Then
                                gvDropRowValue(2) = dropValue(1)
                                gvDropRowValue(1) = cmbOperator.Text.Trim()
                                isNewValue = False
                            End If
                        Next
                        If isNewValue = True Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        End If

                    ElseIf cmbOperator.Text.Trim().ToLower() = "range" Then
                        For i As Integer = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(i)
                            If gvDropRowValue(1).ToLower() = "range" Then
                                rangeCount += 1
                                rangeColumn = gvDropRowValue(0).ToString()
                            ElseIf gvDropRowValue(0) = dropValue(0) Then
                                isNewValue = False
                            End If
                        Next
                        If rangeCount < 2 AndAlso isNewValue = True AndAlso rangeColumn = Nothing Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        ElseIf rangeCount < 2 AndAlso isNewValue = True AndAlso rangeColumn = dropValue(0).ToString() Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        ElseIf rangeCount > 2 Then
                            XtraMessageBox.Show("For range operator only two values are allowed.", "Obj Filter Condition")
                        Else
                            XtraMessageBox.Show("For range operator column name should be same.", "Obj Filter Condition")
                            rangeCount = 0
                        End If

                    ElseIf ((cmbOperator.Text.Trim().ToLower() = "in") Or (cmbOperator.Text.Trim().ToLower() = "not in")) Then
                        For i As Integer = 0 To gvDragValue.RowCount - 1
                            gvDropRowValue = gvDragValue.GetRow(i)
                            If gvDropRowValue(0) = dropValue(0) And gvDropRowValue(1) = cmbOperator.Text.Trim() And gvDropRowValue(2) = dropValue(1) Then
                                XtraMessageBox.Show("Column and its value already present. Please choose other one.", "Obj Filter Condition")
                                isNewValue = False
                                Exit For
                            End If
                        Next
                        If isNewValue = True Then
                            Dim dr As DataRow = dtDragValue.NewRow()
                            dr("Parameter") = dropValue(0)
                            dr("Op") = cmbOperator.Text.Trim()
                            dr("Value") = dropValue(1)
                            dtDragValue.Rows.Add(dr)
                        End If
                    End If

                    IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcDragValue, gvDragValue, dtDragValue, "ALL", , "Value")

                End If
            Else
                XtraMessageBox.Show("Please Select Operator", "Obj Filter Condition")
            End If
        End If
        e.Effect = DragDropEffects.None
    End Sub

    Private Sub gcColumnValue_DragOver(sender As Object, e As DragEventArgs) Handles gcColumnValue.DragOver, gcDragValue.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub txtFieldSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtFieldSearch.KeyUp
        Try
            If dtField IsNot Nothing Then
                If (txtFieldSearch.Text.Length > 2) Then
                    dtField.DefaultView.RowFilter = "ColumnName Like '%" + txtFieldSearch.Text + "%'"
                Else
                    dtField.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            If dtColumnValue IsNot Nothing Then
                If (txtSearch.Text.Length > 2) Then
                    dtColumnValue.DefaultView.RowFilter = dtColumnValue.Columns(0).ToString() & " Like '%" + txtSearch.Text + "%'"
                Else
                    dtColumnValue.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetFilterString() As String
        Dim filterString As String = ""
        Dim rangeRowCounter As Integer = 0

        Dim inRowCounter As Integer = 0
        Dim inFilterStr As String = ""
        Dim inFilterValueStr As String = ""

        For i As Integer = 0 To gvDragValue.RowCount - 1
            Dim gvRowValue = gvDragValue.GetRow(i)
            If (gvRowValue("Op").ToString() = "=" Or gvRowValue("Op").ToString() = "<>" Or gvRowValue("Op").ToString() = "<" Or gvRowValue("Op").ToString() = ">" Or gvRowValue("Op").ToString() = "<=" Or gvRowValue("Op").ToString() = ">=") Then
                filterString += gvRowValue("Schema").ToString() & gvRowValue("Parameter").ToString() & " " & gvRowValue("Op").ToString() & " ''" & gvRowValue("Value").ToString() & "'' "
                If gvDragValue.RowCount - 1 > i Then
                    filterString += " AND "
                End If
            ElseIf (gvRowValue("Op").ToString().ToLower() = "like") Or (gvRowValue("Op").ToString().ToLower() = "not like") Then
                filterString += gvRowValue("Schema").ToString() & gvRowValue("Parameter").ToString() & " " & gvRowValue("Op").ToString().ToUpper() & " ''%" & gvRowValue("Value").ToString() & "%''"
                If gvDragValue.RowCount - 1 > i Then
                    filterString += " AND "
                End If
            ElseIf (gvRowValue("Op").ToString().ToLower = "range") Then
                If rangeRowCounter = 0 Then
                    filterString = gvRowValue("Schema").ToString() & gvRowValue("Parameter").ToString() & " BETWEEN " & gvRowValue("Value").ToString() & " AND "
                End If
                rangeRowCounter = rangeRowCounter + 1
                If rangeRowCounter = 2 Then
                    filterString += gvRowValue("Value").ToString() & " AND "
                    rangeRowCounter = rangeRowCounter + 1
                End If
            ElseIf (gvRowValue("Op").ToString().ToLower() = "in") Or (gvRowValue("Op").ToString().ToLower() = "not in") Then
                If inRowCounter = 0 Then
                    inFilterStr = gvRowValue("Schema").ToString() & gvRowValue("Parameter").ToString() & " " & gvRowValue("Op").ToString().ToUpper() & " ("
                End If
                inRowCounter = inRowCounter + 1
                If inRowCounter = 1 Then
                    inFilterValueStr += "''" & gvRowValue("Value").ToString() & "''"
                ElseIf inRowCounter > 1 Then
                    inFilterValueStr += ",''" & gvRowValue("Value").ToString() & "''"
                End If
            End If
        Next

        If inFilterStr <> "" Then
            inFilterValueStr = inFilterValueStr.TrimEnd(",")
            filterString += inFilterStr & inFilterValueStr & ")"
        End If

        If rangeRowCounter > 0 Then
            filterString = filterString.Substring(0, filterString.Length - 4)
        End If

        Return filterString
    End Function

    Private Function GetFilterStringWithoutSchema() As String
        Dim filterString As String = ""
        Dim rangeRowCounter As Integer = 0

        Dim inRowCounter As Integer = 0
        Dim inFilterStr As String = ""
        Dim inFilterValueStr As String = ""

        For i As Integer = 0 To gvDragValue.RowCount - 1
            Dim gvRowValue = gvDragValue.GetRow(i)
            If (gvRowValue("Op").ToString() = "=" Or gvRowValue("Op").ToString() = "<>" Or gvRowValue("Op").ToString() = "<" Or gvRowValue("Op").ToString() = ">" Or gvRowValue("Op").ToString() = "<=" Or gvRowValue("Op").ToString() = ">=") Then
                filterString += gvRowValue("Parameter").ToString() & " " & gvRowValue("Op").ToString() & " ''" & gvRowValue("Value").ToString() & "'' "
                If gvDragValue.RowCount - 1 > i Then
                    filterString += " AND "
                End If
            ElseIf (gvRowValue("Op").ToString().ToLower() = "like") Or (gvRowValue("Op").ToString().ToLower() = "not like") Then
                filterString += gvRowValue("Parameter").ToString() & " " & gvRowValue("Op").ToString().ToUpper() & " ''%" & gvRowValue("Value").ToString() & "%''"
                If gvDragValue.RowCount - 1 > i Then
                    filterString += " AND "
                End If
            ElseIf (gvRowValue("Op").ToString().ToLower = "range") Then
                If rangeRowCounter = 0 Then
                    filterString = gvRowValue("Parameter").ToString() & " BETWEEN " & gvRowValue("Value").ToString() & " AND "
                End If
                rangeRowCounter = rangeRowCounter + 1
                If rangeRowCounter = 2 Then
                    filterString += gvRowValue("Value").ToString() & " AND "
                    rangeRowCounter = rangeRowCounter + 1
                End If
            ElseIf (gvRowValue("Op").ToString().ToLower() = "in") Or (gvRowValue("Op").ToString().ToLower() = "not in") Then
                If inRowCounter = 0 Then
                    inFilterStr = gvRowValue("Parameter").ToString() & " " & gvRowValue("Op").ToString().ToUpper() & " ("
                End If
                inRowCounter = inRowCounter + 1
                If inRowCounter = 1 Then
                    inFilterValueStr += "''" & gvRowValue("Value").ToString() & "''"
                ElseIf inRowCounter > 1 Then
                    inFilterValueStr += ",''" & gvRowValue("Value").ToString() & "''"
                End If
            End If
        Next

        If inFilterStr <> "" Then
            inFilterValueStr = inFilterValueStr.TrimEnd(",")
            filterString += inFilterStr & inFilterValueStr & ")"
        End If

        If rangeRowCounter > 0 Then
            filterString = filterString.Substring(0, filterString.Length - 4)
        End If
        Return filterString
    End Function

    Private Sub btnAddFilter_Click(sender As Object, e As EventArgs) Handles btnAddFilter.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If filterType.ToUpper = "MOCONFIG" Then

                Dim filterString As String = GetFilterString()

                If filterString <> "" Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@TemplateMOConfigID", templateMOConfigID},
                        New String() {"@FilterString", Chr(39) & filterString & Chr(39)}
                    }
                    strConnection = GetSQL(4121, parray)(0)
                    sqlParam = GetSQL(4121, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                    frmRefCheck.SaveChangeLog(Me.templateID, Me.moName, Me.templateMOConfigID, "Filter: " & filterString & " added to MO: " & Me.moName)
                End If

            ElseIf filterType.ToUpper = "CONGESTIONRULE" Then

                Dim filterString As String = GetFilterStringWithoutSchema()

                If filterString <> "" Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@CapCongestionRuleID", capCongestionRuleID},
                        New String() {"@FilterString", Chr(39) & filterString & Chr(39)}
                    }
                    strConnection = GetSQL(3019, parray)(0)
                    sqlParam = GetSQL(3019, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                End If

            ElseIf filterType.ToUpper = "NBIREPORT" Then

                Dim filterString As String = GetFilterStringWithoutSchema()

                If filterString <> "" Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@ReportID", nbiReportID},
                        New String() {"@FilterString", Chr(39) & filterString & Chr(39)}
                    }
                    strConnection = GetSQL(8532, parray)(0)
                    sqlParam = GetSQL(8532, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                End If

            ElseIf filterType.ToUpper = "ANOKPIRULES" Then

                Dim filterString As String = GetFilterString()

                If filterString <> "" Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@AlertRuleID", Me.AlertRuleID},
                        New String() {"@KPIRuleID", kpiRuleID},
                        New String() {"@FilterString", Chr(39) & filterString & Chr(39)}
                    }
                    strConnection = GetSQL(3857, parray)(0)
                    sqlParam = GetSQL(3857, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                End If

            End If

            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnAddFilter2AllMO_Click(sender As Object, e As EventArgs) Handles btnAddFilter2AllMO.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim filterString As String = GetFilterString()

            If filterType.ToUpper = "MOCONFIG" Then

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@templateID", Me.templateID},
                    New String() {"@filterString", Chr(39) & filterString & Chr(39)}
                }
                strConnection = GetSQL(4163, parray)(0)
                sqlParam = GetSQL(4163, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                frmRefCheck.SaveChangeLog(Me.templateID, "", 0, "Filter: " & filterString & " added to all MOs in the template")

            ElseIf filterType.ToUpper = "MO4TEMPLATE" Then
                objGenerateTemplate.filterString = filterString
            End If

            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvDragValue_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvDragValue.CellValueChanged
        Try
            If ((gvDragValue.GetFocusedDataRow().Item("Op").ToString.ToUpper = "LIKE") Or (gvDragValue.GetFocusedDataRow().Item("Op").ToString.ToUpper = "NOT LIKE")) AndAlso (e.Column.FieldName.ToUpper = "VALUE") Then
                gvDragValue.GetFocusedDataRow().Item("Value") = e.Value.ToString
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvDragValue_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvDragValue.ShowingEditor
        Try
            If ((gvDragValue.GetFocusedDataRow().Item("Op").ToString.ToUpper = "LIKE") Or (gvDragValue.GetFocusedDataRow().Item("Op").ToString.ToUpper = "NOT LIKE")) AndAlso (gvDragValue.FocusedColumn().FieldName = "Value") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAddFilterAllKpiRules_Click(sender As Object, e As EventArgs) Handles btnAddFilterAllKpiRules.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim filterString As String = GetFilterString()

            If filterType.ToUpper = "ANOKPIRULES" Then

                For Each dr As DataRow In dtAlertConfig.Rows
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@AlertRuleID", Me.AlertRuleID},
                        New String() {"@KPIRuleID", CInt(dr("KPI_RULEID"))},
                        New String() {"@FilterString", Chr(39) & filterString & Chr(39)}
                    }
                    strConnection = GetSQL(3860, parray)(0)
                    sqlParam = GetSQL(3860, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                Next

            End If

            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvField_ColumnFilterChanged(sender As Object, e As EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            gvField_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Private Methods"

    Private Sub LoadObjectFilterFieldsForTemplateMOConfig(ByVal templateMOConfigID As String)
        RemoveHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        RemoveHandler gvField.RowClick, AddressOf gvField_RowClick
        RemoveHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        Dim strConnection As String
        Dim sqlParam As String
        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", CInt(templateMOConfigID)}
        }
        strConnection = GetSQL(4105, parray)(0)
        sqlParam = GetSQL(4105, parray)(1)
        dtField = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        moDatabaseName = dtField.rows(0)("databaseName").ToString
        Dim columnsToHide() As String = {"databaseName", "schemaName", "tableName"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcField, gvField, dtField, "ALL", columnsToHide, "ColumnName")
        AddHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        AddHandler gvField.RowClick, AddressOf gvField_RowClick
        AddHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        gvField_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadObjectFilterFieldsForCongestionRule(ByVal CapCongRuleID As String)
        RemoveHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        RemoveHandler gvField.RowClick, AddressOf gvField_RowClick
        RemoveHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        Dim strConnection As String
        Dim sqlParam As String
        Dim parray()() As String = {New String() {"@CapCongestionRuleID", "'" + CapCongRuleID + "'"}}
        strConnection = GetSQL(3011, parray)(0)
        sqlParam = GetSQL(3011, parray)(1)
        dtField = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        capDatabaseName = dtField.rows(0)("databaseName").ToString
        Dim columnsToHide() As String = {"databaseName", "schemaName", "tableName"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcField, gvField, dtField, "ALL", columnsToHide, "ColumnName")
        AddHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        AddHandler gvField.RowClick, AddressOf gvField_RowClick
        AddHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        gvField_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadTemplateMOConfigFieldValues(ByVal moDBName As String, ByVal tableName As String, ByVal columnName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@DatabaseName", Chr(39) & moDBName & Chr(39)},
            New String() {"@TableName", "'" + tableName + "'"},
            New String() {"@ColumnName", "'" + columnName + "'"}
        }

        strConnection = GetSQL(4118, parray)(0)
        sqlParam = GetSQL(4118, parray)(1)

        dtColumnValue = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcColumnValue, gvColumnValue, dtColumnValue, "ALL",, columnName)
    End Sub

    Private Sub LoadCapCongestionRuleFieldValues(ByVal tableName As String, ByVal columnName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@DatabaseName", Chr(39) & capDatabaseName & Chr(39)},
            New String() {"@TableName", "'" + tableName + "'"},
            New String() {"@ColumnName", "'" + columnName + "'"}
        }

        strConnection = GetSQL(3012, parray)(0)
        sqlParam = GetSQL(3012, parray)(1)

        dtColumnValue = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcColumnValue, gvColumnValue, dtColumnValue, "ALL",, columnName)
    End Sub

    Private Sub LoadObjectFilterFieldsForMO()
        RemoveHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        RemoveHandler gvField.RowClick, AddressOf gvField_RowClick
        RemoveHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        Dim strConnection As String
        Dim sqlParam As String
        Dim parray()() As String = {
            New String() {"@MOName", "'" + Me.moName + "'"},
            New String() {"@MOTable", "'" + Me.moTable + "'"},
            New String() {"@MODatabase", "'" + Me.moDatabaseName + "'"},
            New String() {"@JoinTable", "'" + Me.joinTable + "'"},
            New String() {"@ExcludedColumns", "'" + Me.excludedColumns + "'"}
        }
        strConnection = GetSQL(4169, parray)(0)
        sqlParam = GetSQL(4169, parray)(1)
        dtField = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        moDatabaseName = dtField.rows(0)("databaseName").ToString
        Dim columnsToHide() As String = {"databaseName", "schemaName", "tableName"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcField, gvField, dtField, "ALL", columnsToHide, "ColumnName")
        AddHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        AddHandler gvField.RowClick, AddressOf gvField_RowClick
        AddHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        gvField_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadObjectFilterFieldsForNBIReport(nbiReportID As Integer)
        RemoveHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        RemoveHandler gvField.RowClick, AddressOf gvField_RowClick
        RemoveHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        Dim strConnection As String
        Dim sqlParam As String
        Dim parray()() As String = {New String() {"@ReportID", nbiReportID}}
        strConnection = GetSQL(8531, parray)(0)
        sqlParam = GetSQL(8531, parray)(1)
        dtField = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        capDatabaseName = dtField.rows(0)("databaseName").ToString
        Dim columnsToHide() As String = {"databaseName", "schemaName", "tableName"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcField, gvField, dtField, "ALL", columnsToHide, "ColumnName")
        AddHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        AddHandler gvField.RowClick, AddressOf gvField_RowClick
        AddHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        gvField_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadObjectFilterFieldsForKPIRules(kpiRuleID As Integer)
        RemoveHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        RemoveHandler gvField.RowClick, AddressOf gvField_RowClick
        RemoveHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        Dim strConnection As String
        Dim sqlParam As String
        Dim parray()() As String = {New String() {"@KpiRuleID", kpiRuleID}}
        strConnection = GetSQL(3856, parray)(0)
        sqlParam = GetSQL(3856, parray)(1)
        dtField = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        capDatabaseName = dtField.rows(0)("databaseName").ToString
        Dim columnsToHide() As String = {"databaseName", "schemaName", "tableName"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcField, gvField, dtField, "ALL", columnsToHide, "ColumnName")
        AddHandler gvField.FocusedRowChanged, AddressOf gvField_FocusedRowChanged
        AddHandler gvField.RowClick, AddressOf gvField_RowClick
        AddHandler gvField.ColumnFilterChanged, AddressOf gvField_ColumnFilterChanged
        gvField_FocusedRowChanged(Nothing, Nothing)
    End Sub

#End Region

End Class