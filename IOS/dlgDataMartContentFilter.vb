Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls

Public Class dlgDataMartContentFilter

    Private dtObjectTypeValues As DataTable = Nothing
    Private dtQuery As DataTable
    Private dtResult As DataTable

    Private riCombo As New RepositoryItemComboBox()
    Private riCheckedCombo As New RepositoryItemCheckedComboBoxEdit()
    Private riOperatorCombo As New RepositoryItemComboBox()
    Private riDateEdit As New RepositoryItemDateEdit()

    Public FilterParamList As New List(Of FilterParam)
    Public reportConnString As String = Nothing
    Public objectTableName As String = Nothing

    Private _reportId As String
    Public Property ReportId() As String
        Get
            Return _reportId
        End Get
        Set(value As String)
            _reportId = value
        End Set
    End Property

    Private _isFilterInserted As Boolean
    Public Property IsFilterInserted() As Boolean
        Get
            Return _isFilterInserted
        End Get
        Set(value As Boolean)
            _isFilterInserted = value
        End Set
    End Property

    Private Sub dlgDataMartContentFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            lbl_MessageResult.Text = ""
            _isFilterInserted = False
            If (_reportId IsNot Nothing) Then
                InitializeGridSources()
                InitializeSandBoxFieldsQuery()
                InitializeSandBoxFieldsResult()
                'GetObjectTypeValues()
                GetReportContentFilter()
                AddHandler flp_DimensionsQuery.SizeChanged, AddressOf flp_Dimensions_SizeChanged
                AddHandler flp_DimensionsResult.SizeChanged, AddressOf flp_Dimensions_SizeChanged
            Else
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub InitializeGridSources()
        dtQuery = New DataTable()
        dtQuery.Columns.Add("Dimension", GetType(String))
        dtQuery.Columns.Add("Operator", GetType(String))
        dtQuery.Columns.Add("Value", GetType(String))
        dtQuery.Columns.Add("LogicalLink", GetType(String))
        dtQuery.Columns.Add("FilterType", GetType(String))
        dtQuery.Columns.Add("ObjectFieldType", GetType(Integer))
        'dtQuery.Columns.Add("Delete", GetType(Integer))

        dtResult = New DataTable()
        dtResult.Columns.Add("Dimension", GetType(String))
        dtResult.Columns.Add("Operator", GetType(String))
        dtResult.Columns.Add("Value", GetType(String))
        dtResult.Columns.Add("LogicalLink", GetType(String))
        dtResult.Columns.Add("FilterType", GetType(String))
        dtResult.Columns.Add("ObjectFieldType", GetType(Integer))
        'dtResult.Columns.Add("Delete", GetType(String))
    End Sub

    Private Sub InitRepositoryItems(ByRef gridCtrl As GridControl)
        riCombo.Items.AddRange(New String() {"Value1", "Value2"})

        riCheckedCombo.Items.Add("Option A")
        riCheckedCombo.Items.Add("Option B")
        riCheckedCombo.SeparatorChar = ","c

        riOperatorCombo.Items.AddRange(New String() {"=", "<>", ">", "<", ">=", "<=", "+", "-", "*", "/", "^", "AND", "OR", "NOT", "LIKE", "IN", "NOT IN"})
        riOperatorCombo.TextEditStyle = TextEditStyles.DisableTextEditor

        riDateEdit.CalendarView = CalendarView.Default

        gridCtrl.RepositoryItems.AddRange(New RepositoryItem() {riCombo, riCheckedCombo, riOperatorCombo, riDateEdit})
    End Sub

    Private Sub InitializeSandBoxFieldsQuery()
        Dim vSandBoxFieldNew As DevExSandBoxField
        If (objSandbox.flp_ValueX.Controls.Count > 0) Then
            For Each flowLayoutPanelXYControls As Object In objSandbox.flp_ValueX.Controls
                Dim vSandBoxFieldExist As DevExSandBoxField = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxFieldExist IsNot Nothing) Then
                    If (vSandBoxFieldExist.Text.ToUpper = "PERIOD_START_TIME") Then 'Or (CType(objSandbox.lbDimensions.DataSource, DataTable).Select("COLUMN_NAME='" & vSandBoxFieldExist.Text & "'").Length > 0) Then
                        vSandBoxFieldNew = New DevExSandBoxField()
                        vSandBoxFieldNew.VSandBoxType = vSandBoxFieldExist.VSandBoxType
                        vSandBoxFieldNew.Name = vSandBoxFieldExist.Name
                        vSandBoxFieldNew.Text = vSandBoxFieldExist.Text
                        vSandBoxFieldNew.CounterID = vSandBoxFieldExist.CounterID
                        vSandBoxFieldNew.SourceObjectID = vSandBoxFieldExist.SourceObjectID
                        vSandBoxFieldNew.SQL_SourceTable = vSandBoxFieldExist.SQL_SourceTable
                        vSandBoxFieldNew.TimeAggregation = vSandBoxFieldExist.TimeAggregation
                        vSandBoxFieldNew.ObjectAggregation = vSandBoxFieldExist.ObjectAggregation
                        vSandBoxFieldNew.ObjectTypeID = vSandBoxFieldExist.ObjectTypeID
                        vSandBoxFieldNew.SortValue = vSandBoxFieldExist.SortValue
                        vSandBoxFieldNew.SQL_KPI_ID = vSandBoxFieldExist.SQL_KPI_ID
                        vSandBoxFieldNew.SQL_KPIFormula = vSandBoxFieldExist.SQL_KPIFormula
                        vSandBoxFieldNew.Tag = ""
                        vSandBoxFieldNew.Width = flp_DimensionsQuery.Width - 25
                        AddHandler vSandBoxFieldNew.DragDrop, AddressOf SandBoxFieldQuery_DragDrop
                        AddHandler vSandBoxFieldNew.MouseDown, AddressOf SandBoxFieldQuery_MouseDown
                        flp_DimensionsQuery.Controls.Add(vSandBoxFieldNew)
                    End If
                End If
            Next
        End If

        If (objSandbox.flp_ValueY.Controls.Count > 0) Then
            For Each flowLayoutPanelXYControls As Object In objSandbox.flp_ValueY.Controls
                Dim vSandBoxFieldExist As DevExSandBoxField = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxFieldExist IsNot Nothing) Then
                    vSandBoxFieldNew = New DevExSandBoxField()
                    vSandBoxFieldNew.VSandBoxType = vSandBoxFieldExist.VSandBoxType
                    vSandBoxFieldNew.Name = vSandBoxFieldExist.Name
                    vSandBoxFieldNew.Text = vSandBoxFieldExist.Text
                    vSandBoxFieldNew.CounterID = vSandBoxFieldExist.CounterID
                    vSandBoxFieldNew.SourceObjectID = vSandBoxFieldExist.SourceObjectID
                    vSandBoxFieldNew.SQL_SourceTable = vSandBoxFieldExist.SQL_SourceTable
                    vSandBoxFieldNew.TimeAggregation = vSandBoxFieldExist.TimeAggregation
                    vSandBoxFieldNew.ObjectAggregation = vSandBoxFieldExist.ObjectAggregation
                    vSandBoxFieldNew.ObjectTypeID = vSandBoxFieldExist.ObjectTypeID
                    vSandBoxFieldNew.SortValue = vSandBoxFieldExist.SortValue
                    vSandBoxFieldNew.SQL_KPI_ID = vSandBoxFieldExist.SQL_KPI_ID
                    vSandBoxFieldNew.SQL_KPIFormula = vSandBoxFieldExist.SQL_KPIFormula
                    vSandBoxFieldNew.Tag = ""
                    vSandBoxFieldNew.Width = flp_DimensionsQuery.Width - 25

                    AddHandler vSandBoxFieldNew.DragDrop, AddressOf SandBoxFieldQuery_DragDrop
                    AddHandler vSandBoxFieldNew.MouseDown, AddressOf SandBoxFieldQuery_MouseDown
                    flp_DimensionsQuery.Controls.Add(vSandBoxFieldNew)
                End If
            Next
        End If

        ' Adding more dimensions from sandbox dimensions list box
        Dim dt As DataTable = TryCast(objSandbox.lbDimensions.DataSource, DataTable)
        If dt IsNot Nothing Then
            objectTableName = objSandbox.lbDimensions.Tag & ".dbo." & dt.Rows(0)("TABLE_NAME").ToString
        End If

        Dim alreadyAdded As Boolean = False
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                alreadyAdded = False

                'For Each ctrl As Object In objSandbox.flp_ValueX.Controls
                '    If (TryCast(ctrl, DevExSandBoxField).Text = dt.Rows(i)(0).ToString) Then
                '        alreadyAdded = True
                '        Exit For
                '    End If
                'Next

                If (Not alreadyAdded) Then
                    vSandBoxFieldNew = New DevExSandBoxField()
                    vSandBoxFieldNew.VSandBoxType = DatamartFieldType.ObjectFld
                    vSandBoxFieldNew.Name = "vSandBoxObjectX_" & dt.Rows(i)(0).ToString
                    vSandBoxFieldNew.Text = dt.Rows(i)(0).ToString
                    'vSandBoxFieldNew.CounterID = vSandBoxFieldExist.CounterID
                    vSandBoxFieldNew.SourceObjectID = TryCast(objSandbox.cmbObjectType.SelectedItem, clsComboBoxItem).Value
                    'vSandBoxFieldNew.SQL_SourceTable = vSandBoxFieldExist.SQL_SourceTable
                    'vSandBoxFieldNew.TimeAggregation = vSandBoxFieldExist.TimeAggregation
                    'vSandBoxFieldNew.ObjectAggregation = vSandBoxFieldExist.ObjectAggregation
                    vSandBoxFieldNew.ObjectTypeID = TryCast(objSandbox.cmbObjectType.SelectedItem, clsComboBoxItem).Value
                    vSandBoxFieldNew.Tag = dt.Rows(i)(1).ToString
                    'vSandBoxFieldNew.SortValue = vSandBoxFieldExist.SortValue
                    'vSandBoxFieldNew.SQL_KPI_ID = vSandBoxFieldExist.SQL_KPI_ID
                    'vSandBoxFieldNew.SQL_KPIFormula = vSandBoxFieldExist.SQL_KPIFormula
                    vSandBoxFieldNew.Width = flp_DimensionsQuery.Width - 25
                    AddHandler vSandBoxFieldNew.DragDrop, AddressOf SandBoxFieldQuery_DragDrop
                    AddHandler vSandBoxFieldNew.MouseDown, AddressOf SandBoxFieldQuery_MouseDown
                    flp_DimensionsQuery.Controls.Add(vSandBoxFieldNew)
                End If
            Next
        End If

        flp_DimensionsQuery.Refresh()
        flp_DimensionsQuery.Update()
    End Sub

    Private Sub InitializeSandBoxFieldsResult()
        Dim vSandBoxFieldNew As DevExSandBoxField
        If (objSandbox.flp_ValueX.Controls.Count > 0) Then
            For Each flowLayoutPanelXYControls As Object In objSandbox.flp_ValueX.Controls
                Dim vSandBoxFieldExist As DevExSandBoxField = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxFieldExist IsNot Nothing) Then
                    If (vSandBoxFieldExist.Text.ToUpper = "PERIOD_START_TIME") Then
                        vSandBoxFieldNew = New DevExSandBoxField()
                        vSandBoxFieldNew.VSandBoxType = vSandBoxFieldExist.VSandBoxType
                        vSandBoxFieldNew.Name = vSandBoxFieldExist.Name
                        vSandBoxFieldNew.Text = vSandBoxFieldExist.Text
                        vSandBoxFieldNew.CounterID = vSandBoxFieldExist.CounterID
                        vSandBoxFieldNew.SourceObjectID = vSandBoxFieldExist.SourceObjectID
                        vSandBoxFieldNew.SQL_SourceTable = vSandBoxFieldExist.SQL_SourceTable
                        vSandBoxFieldNew.TimeAggregation = vSandBoxFieldExist.TimeAggregation
                        vSandBoxFieldNew.ObjectAggregation = vSandBoxFieldExist.ObjectAggregation
                        vSandBoxFieldNew.ObjectTypeID = vSandBoxFieldExist.ObjectTypeID
                        vSandBoxFieldNew.SortValue = vSandBoxFieldExist.SortValue
                        vSandBoxFieldNew.SQL_KPI_ID = vSandBoxFieldExist.SQL_KPI_ID
                        vSandBoxFieldNew.SQL_KPIFormula = vSandBoxFieldExist.SQL_KPIFormula
                        vSandBoxFieldNew.Tag = ""
                        vSandBoxFieldNew.Width = flp_DimensionsResult.Width - 25
                        AddHandler vSandBoxFieldNew.DragDrop, AddressOf SandBoxFieldResult_DragDrop
                        AddHandler vSandBoxFieldNew.MouseDown, AddressOf SandBoxFieldResult_MouseDown
                        flp_DimensionsResult.Controls.Add(vSandBoxFieldNew)
                    End If
                End If
            Next
        End If

        If (objSandbox.flp_ValueY.Controls.Count > 0) Then
            For Each flowLayoutPanelXYControls As Object In objSandbox.flp_ValueY.Controls
                Dim vSandBoxFieldExist As DevExSandBoxField = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                If (vSandBoxFieldExist IsNot Nothing) Then
                    vSandBoxFieldNew = New DevExSandBoxField()
                    vSandBoxFieldNew.VSandBoxType = vSandBoxFieldExist.VSandBoxType
                    vSandBoxFieldNew.Name = vSandBoxFieldExist.Name
                    vSandBoxFieldNew.Text = vSandBoxFieldExist.Text
                    vSandBoxFieldNew.CounterID = vSandBoxFieldExist.CounterID
                    vSandBoxFieldNew.SourceObjectID = vSandBoxFieldExist.SourceObjectID
                    vSandBoxFieldNew.SQL_SourceTable = vSandBoxFieldExist.SQL_SourceTable
                    vSandBoxFieldNew.TimeAggregation = vSandBoxFieldExist.TimeAggregation
                    vSandBoxFieldNew.ObjectAggregation = vSandBoxFieldExist.ObjectAggregation
                    vSandBoxFieldNew.ObjectTypeID = vSandBoxFieldExist.ObjectTypeID
                    vSandBoxFieldNew.SortValue = vSandBoxFieldExist.SortValue
                    vSandBoxFieldNew.SQL_KPI_ID = vSandBoxFieldExist.SQL_KPI_ID
                    vSandBoxFieldNew.SQL_KPIFormula = vSandBoxFieldExist.SQL_KPIFormula
                    vSandBoxFieldNew.Tag = ""
                    vSandBoxFieldNew.Width = flp_DimensionsResult.Width - 25

                    AddHandler vSandBoxFieldNew.DragDrop, AddressOf SandBoxFieldResult_DragDrop
                    AddHandler vSandBoxFieldNew.MouseDown, AddressOf SandBoxFieldResult_MouseDown
                    flp_DimensionsResult.Controls.Add(vSandBoxFieldNew)
                End If
            Next
        End If

        flp_DimensionsResult.Refresh()
        flp_DimensionsResult.Update()
    End Sub

    Private Sub SandBoxFieldQuery_MouseDown(sender As Object, e As MouseEventArgs)
        Dim listControl As DevExSandBoxField = TryCast(sender, DevExSandBoxField)
        If (listControl IsNot Nothing) Then
            If e.Button = MouseButtons.Left Then
                Dim counterDragDropText As String = listControl.Text & "#" & listControl.Tag & "#" & listControl.VSandBoxType
                listControl.DoDragDrop(counterDragDropText, DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub SandBoxFieldResult_MouseDown(sender As Object, e As MouseEventArgs)
        Dim listControl As DevExSandBoxField = TryCast(sender, DevExSandBoxField)
        If (listControl IsNot Nothing) Then
            If e.Button = MouseButtons.Left Then
                Dim counterDragDropText As String = listControl.Text & "#" & listControl.Tag & "#" & listControl.VSandBoxType
                listControl.DoDragDrop(counterDragDropText, DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub SandBoxFieldQuery_DragDrop(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub SandBoxFieldResult_DragDrop(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub flp_Dimensions_SizeChanged(sender As Object, e As EventArgs)
        Try
            For Each ctrl As DevExSandBoxField In flp_DimensionsQuery.Controls
                ctrl.Width = flp_DimensionsQuery.Width - 25
            Next
            For Each ctrl As DevExSandBoxField In flp_DimensionsResult.Controls
                ctrl.Width = flp_DimensionsResult.Width - 25
            Next
        Catch
        End Try
    End Sub

    Private Function GetFilteredParam()
        Dim alteredFilterParam As List(Of FilterParam) = New List(Of FilterParam)
        If xtcReportFilter.SelectedTabPageIndex = 0 Then
            alteredFilterParam = FilterParamList.Where(Function(x) x.FilterType = "QUERY").ToList()
        ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
            alteredFilterParam = FilterParamList.Where(Function(x) x.FilterType = "RESULT").ToList()
        End If
        Return alteredFilterParam
    End Function

    Private Sub GetReportContentFilter()
        Try
            Dim dtExitReportContentFilter As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContentFilter.GetReportContentFilter(_reportId))

            If (dtExitReportContentFilter.Rows.Count > 0) Then

                If dtExitReportContentFilter.Rows(0)("QueryOrResult").ToString.ToUpper = "QUERY" Then
                    GC_FilterStatementQuery.Text = "Filter Statement for " & dtExitReportContentFilter.Rows(0)(ReportContentFilterFields.ReportName).ToString
                    BindReportContentFilter(dtExitReportContentFilter, gcQuery, gvQuery.RowCount)

                    SetSQLQuery()

                Else
                    GC_FilterStatementResult.Text = "Filter Statement for " & dtExitReportContentFilter.Rows(0)(ReportContentFilterFields.ReportName).ToString
                    BindReportContentFilter(dtExitReportContentFilter, gcResult, gvResult.RowCount)

                    SetSQLResult()

                End If

            End If

            'RefreshFilterContents()

        Catch ex As Exception
            SetMessage("Error : Filters Fetching fail.")
        End Try
    End Sub

    Private Sub SetSQLQuery()
        RichTextBoxQuery.Text = ""
        For iCntr As Integer = 0 To gvQuery.RowCount - 1
            'Dim controlTmp As New Control
            'If (gvQuery.Columns(iCntr).FieldName = "Dimension") Then
            'Dim lblFilterDimensionTmp As DevExpress.XtraEditors.LabelControl = TryCast(controlTmp, DevExpress.XtraEditors.LabelControl)
            'If (lblFilterDimensionTmp IsNot Nothing) Then
            AppendText(RichTextBoxQuery, " " & gvQuery.GetRowCellValue(iCntr, "Dimension").ToString.Trim, Color.Black, False)
            'End If
            'ElseIf (controlTmp.Name.Contains("vCmbFilterOperator_")) Then
            'Dim cmbFilterOperatorTmp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
            'If (cmbFilterOperatorTmp IsNot Nothing) Then
            AppendText(RichTextBoxQuery, " " & gvQuery.GetRowCellValue(iCntr, "Operator").ToString.Trim, Color.OrangeRed, False)
            'End If
            'ElseIf (controlTmp.Name.Contains("vDtpFilterValue_")) Then
            'Dim dtpFilterValueTmp As DevExpress.XtraEditors.DateEdit = TryCast(controlTmp, DevExpress.XtraEditors.DateEdit)
            'If (dtpFilterValueTmp IsNot Nothing) Then
            AppendText(RichTextBoxQuery, " '" & gvQuery.GetRowCellValue(iCntr, "Value").ToString.Trim & "' ", Color.Black, False)
            'End If
            'ElseIf (controlTmp.Name.Contains("vTxtFilterValue_")) Then
            'Dim txtFilterValueTmp As DevExpress.XtraEditors.TextEdit = TryCast(controlTmp, DevExpress.XtraEditors.TextEdit)
            'If (txtFilterValueTmp IsNot Nothing) Then

            'AppendText(RichTextBoxQuery, " '" & txtFilterValueTmp.Text.Trim & "' ", Color.Black, False)

            'End If
            'ElseIf (controlTmp.Name.Contains("vCmbLogicalLink_")) Then
            'Dim cmbLogicalLinkTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
            'If (cmbLogicalLinkTemp IsNot Nothing) Then
            'If (cmbLogicalLinkTemp.Enabled) Then
            AppendText(RichTextBoxQuery, " " & gvQuery.GetRowCellValue(iCntr, "LogicalLink").ToString.Trim, Color.Red, False)
            'End If
            'End If
            'ElseIf (controlTmp.Name.Contains("vCmbFilterValue_")) Then
            'Dim cmbFldValTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
            'If (cmbFldValTemp IsNot Nothing) Then
            'If (cmbFldValTemp.Enabled) Then
            'AppendText(RichTextBoxQuery, " " & Chr(39) & cmbFldValTemp.SelectedItem.Text & Chr(39), Color.Red, False)
            'End If
            'End If
            'End If
        Next
    End Sub

    Private Sub SetSQLResult()
        RichTextBoxResult.Text = ""
        For iCntr As Integer = 0 To gvResult.RowCount - 1
            'Dim controlTmp As New Control
            'If (controlTmp.Name.Contains("vlblDimension_")) Then
            '    Dim lblFilterDimensionTmp As DevExpress.XtraEditors.LabelControl = TryCast(controlTmp, DevExpress.XtraEditors.LabelControl)
            '    If (lblFilterDimensionTmp IsNot Nothing) Then
            AppendText(RichTextBoxResult, " " & gvResult.GetRowCellValue(iCntr, "Dimension").ToString.Trim, Color.Black, False)
            'End If
            'ElseIf (controlTmp.Name.Contains("vCmbFilterOperator_")) Then
            '    Dim cmbFilterOperatorTmp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
            '    If (cmbFilterOperatorTmp IsNot Nothing) Then
            AppendText(RichTextBoxResult, " " & gvResult.GetRowCellValue(iCntr, "Operator").ToString.Trim, Color.OrangeRed, False)
            'End If
            'ElseIf (controlTmp.Name.Contains("vDtpFilterValue_")) Then
            '    Dim dtpFilterValueTmp As DevExpress.XtraEditors.DateEdit = TryCast(controlTmp, DevExpress.XtraEditors.DateEdit)
            '    If (dtpFilterValueTmp IsNot Nothing) Then

            AppendText(RichTextBoxResult, " '" & gvResult.GetRowCellValue(iCntr, "Value").ToString.Trim & "' ", Color.Black, False)

            'End If
            'ElseIf (controlTmp.Name.Contains("vTxtFilterValue_")) Then
            '    Dim txtFilterValueTmp As DevExpress.XtraEditors.TextEdit = TryCast(controlTmp, DevExpress.XtraEditors.TextEdit)
            '    If (txtFilterValueTmp IsNot Nothing) Then

            'AppendText(RichTextBoxResult, " '" & txtFilterValueTmp.Text & "' ", Color.Black, False)

            'End If
            'ElseIf (controlTmp.Name.Contains("vCmbLogicalLink_")) Then
            '    Dim cmbLogicalLinkTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
            '    If (cmbLogicalLinkTemp IsNot Nothing) Then
            '        If (cmbLogicalLinkTemp.Enabled) Then
            AppendText(RichTextBoxResult, " " & gvResult.GetRowCellValue(iCntr, "LogicalLink").ToString.Trim, Color.Red, False)
            'End If
            'End If
            'ElseIf (controlTmp.Name.Contains("vCmbFilterValue_")) Then
            '    Dim cmbFldValTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
            '    If (cmbFldValTemp IsNot Nothing) Then
            '        If (cmbFldValTemp.Enabled) Then
            '            AppendText(RichTextBoxResult, " " & cmbFldValTemp.SelectedItem.Text, Color.Red, False)
            '        End If
            '    End If
            'End If
        Next
    End Sub

    Private Sub AppendText(ByRef box As DevExpress.XtraEditors.MemoEdit, ByVal text As String, ByVal color As Color, Optional ByVal AddNewLine As Boolean = False)
        If (AddNewLine) Then
            text += Environment.NewLine
        End If
        box.SelectionStart = box.Text.Length
        box.SelectionLength = 0
        box.ForeColor = color
        box.Text = box.Text + text
        'box.SelectionColor = box.ForeColor
    End Sub

    Private Sub BindReportContentFilter(ByRef dtReportContentFilter As DataTable, ByRef gridCtrl As GridControl, ByVal rowIndex As Integer)

        Dim isFirstIndex As Boolean = True
        Dim objFldFound As Boolean = False
        Dim noOfrows As Integer = dtReportContentFilter.Rows.Count
        Dim rowNo As Integer = 1
        For Each drFilter As DataRow In dtReportContentFilter.Rows
            isFirstIndex = True
            objFldFound = False
            Dim filterParam As FilterParam = New FilterParam()
            Dim drQuery As DataRow = dtQuery.NewRow()

            filterParam.FilterDimension = drFilter(ReportContentFilterFields.FilterDimension)
            filterParam.ObjectFieldType = drFilter(ReportContentFilterFields.ObjectFieldType)
            drQuery("Dimension") = drFilter(ReportContentFilterFields.FilterDimension)
            filterParam.FilterOperator = drFilter(ReportContentFilterFields.FilterOperator)
            drQuery("Operator") = drFilter(ReportContentFilterFields.FilterOperator)

            If (filterParam.FilterDimension.ToUpper = "PERIOD_START_TIME") Then
                filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                drQuery("Value") = drFilter(ReportContentFilterFields.FilterValue)
            Else
                For Each ctrl As DevExSandBoxField In flp_DimensionsQuery.Controls
                    If filterParam.FilterDimension.Contains(".") Then
                        If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.Split(".")(1).ToString) Then
                            filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                            filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                            drQuery("Value") = drFilter(filterParam.FilterDimension.Split(".")(1))
                            objFldFound = True
                            Exit For
                        End If
                    Else
                        If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.ToString) Then
                            filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                            filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                            drQuery("Value") = drFilter(filterParam.FilterDimension)
                            objFldFound = True
                            Exit For
                        End If
                    End If

                Next

                If objFldFound = False Then
                    filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                    filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                    drQuery("Value") = drFilter(ReportContentFilterFields.FilterValue)
                    drQuery("FilterType") = drFilter(ReportContentFilterFields.FilterType)
                End If
            End If

            filterParam.FilterLogicalLink = drFilter(ReportContentFilterFields.LogicalLink)
            If (rowNo = noOfrows) Then
                isFirstIndex = False
                filterParam.FilterLogicalLink = ""
            End If

            drQuery("LogicalLink") = drFilter(ReportContentFilterFields.LogicalLink)
            dtQuery.Rows.Add(drQuery)

            FilterParamList.Add(filterParam)
            rowIndex += 1
            rowNo += 1
            isFirstIndex = False
        Next

        IOSDevExpressGrid.PopulateDataInGrid(gcQuery, gvQuery, dtQuery, "ALL", {"FilterType", "ObjectFieldType"}, "Dimension")

        Dim btnDelete As New RepositoryItemButtonEdit()
        btnDelete.TextEditStyle = TextEditStyles.HideTextEditor
        btnDelete.Buttons(0).Kind = ButtonPredefines.Glyph
        btnDelete.LookAndFeel.UseDefaultLookAndFeel = True
        btnDelete.Buttons(0).Caption = "Delete"
        btnDelete.LookAndFeel.SkinName = "DevExpress Style"
        'btnDelete.LookAndFeel.SkinMaskColor = Color.Blue
        btnDelete.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        AddHandler btnDelete.ButtonClick, AddressOf btnDelete_ButtonClick

        'Add an Unbound Column for the button
        Dim colDelete = gvQuery.Columns.AddField("Delete")
        colDelete.Visible = True
        colDelete.ColumnEdit = btnDelete
        colDelete.ShowButtonMode = Views.Base.ShowButtonModeEnum.ShowAlways

    End Sub

    Private Sub btnDelete_ButtonClick(sender As Object, e As ButtonPressedEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim btnDeleteFilter As DevExpress.XtraEditors.ButtonEdit = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If (btnDeleteFilter IsNot Nothing) Then
                btnDeleteFilter.Enabled = False

                If (FilterParamList.Count > 0) Then
                    Dim _filterParam As FilterParam = Nothing
                    For Each filterParam As FilterParam In FilterParamList
                        If filterParam.FilterDimension.ToString.ToUpper.Contains(gvQuery.GetFocusedRowCellValue("FilterDimension").ToString.ToUpper) Then
                            _filterParam = filterParam
                            Exit For
                        End If
                    Next
                    If (_filterParam IsNot Nothing) Then
                        FilterParamList.Remove(_filterParam)
                    End If
                End If

                If xtcReportFilter.SelectedTabPageIndex = 0 Then

                    Dim datarow = gvQuery.GetDataRow(gvQuery.FocusedRowHandle)
                    dtQuery.Rows.Remove(datarow)

                    If (FilterParamList.Count > 0) Then
                        SetSQLQuery()
                    Else
                        SetSQLQuery()
                    End If

                ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then

                    Dim datarow = gvResult.GetDataRow(gvResult.FocusedRowHandle)
                    dtResult.Rows.Remove(datarow)

                    If (FilterParamList.Count > 0) Then
                        SetSQLResult()
                    Else
                        SetSQLResult()
                    End If

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub SetMessage(ByVal message As String)
        If xtcReportFilter.SelectedTabPageIndex = 0 Then
            lbl_MessageQuery.ForeColor = Color.Red
            lbl_MessageQuery.Visible = True
            lbl_MessageQuery.Text = message
            Timer1.Enabled = True
            Timer1.Start()
            AddHandler Timer1.Tick, AddressOf Timer1_Tick
        ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
            lbl_MessageResult.ForeColor = Color.Red
            lbl_MessageResult.Visible = True
            lbl_MessageResult.Text = message
            Timer2.Enabled = True
            Timer2.Start()
            AddHandler Timer2.Tick, AddressOf Timer2_Tick
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lbl_MessageQuery.Text = ""
        lbl_MessageQuery.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lbl_MessageResult.Text = ""
        lbl_MessageResult.Visible = False
        RemoveHandler Timer2.Tick, AddressOf Timer2_Tick
        Timer2.Enabled = False
        Timer2.Stop()
    End Sub

End Class