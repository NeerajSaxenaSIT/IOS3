Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors

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
            gcQuery.Cursor = Cursors.WaitCursor
            Application.DoEvents()

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

        Catch ex As Exception
            SetMessage("Error : Filters Fetching fail.")
        Finally
            gcQuery.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub SetSQLQuery()
        RichTextBoxQuery.Text = ""
        For iCntr As Integer = 0 To gvQuery.RowCount - 1
            AppendText(RichTextBoxQuery, " " & gvQuery.GetRowCellValue(iCntr, "Dimension").ToString.Trim, Color.Black, False)
            AppendText(RichTextBoxQuery, " " & gvQuery.GetRowCellValue(iCntr, "Operator").ToString.Trim, Color.OrangeRed, False)
            If gvQuery.GetRowCellValue(iCntr, "Operator").ToString.Trim = "IN" Or gvQuery.GetRowCellValue(iCntr, "Operator").ToString.Trim = "NOT IN" Then
                AppendText(RichTextBoxQuery, " " & gvQuery.GetRowCellValue(iCntr, "Value").ToString.Trim & " ", Color.Black, False)
            Else
                AppendText(RichTextBoxQuery, " '" & gvQuery.GetRowCellValue(iCntr, "Value").ToString.Trim & "' ", Color.Black, False)
            End If
            AppendText(RichTextBoxQuery, " " & gvQuery.GetRowCellValue(iCntr, "LogicalLink").ToString.Trim, Color.Red, False)
        Next
    End Sub

    Private Sub SetSQLResult()
        RichTextBoxResult.Text = ""
        For iCntr As Integer = 0 To gvResult.RowCount - 1
            AppendText(RichTextBoxResult, " " & gvResult.GetRowCellValue(iCntr, "Dimension").ToString.Trim, Color.Black, False)
            AppendText(RichTextBoxResult, " " & gvResult.GetRowCellValue(iCntr, "Operator").ToString.Trim, Color.OrangeRed, False)
            AppendText(RichTextBoxResult, " '" & gvResult.GetRowCellValue(iCntr, "Value").ToString.Trim & "' ", Color.Black, False)
            AppendText(RichTextBoxResult, " " & gvResult.GetRowCellValue(iCntr, "LogicalLink").ToString.Trim, Color.Red, False)
        Next
    End Sub

    Private Sub AppendText(ByRef box As DevExpress.XtraEditors.MemoEdit, ByVal text As String, ByVal color As Color, Optional ByVal AddNewLine As Boolean = False)
        If (AddNewLine) Then
            text += Environment.NewLine
        End If
        Dim pattern As String = "\b(NOT IN|IN)\s*$"
        box.SelectionStart = box.Text.Length
        box.SelectionLength = 0
        box.ForeColor = color
        Dim match As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(box.Text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        If match.Success Then
            If text.Contains("(") AndAlso text.Contains(")") Then
                box.Text = box.Text + " " + text.Trim + " "
            Else
                text = text.Replace(" '", "")
                box.Text = box.Text + " (" + String.Join(", ", text.Split(","c).Select(Function(s) $"'{s.Trim()}'")) + ") "
            End If
        Else
            box.Text = box.Text + text
        End If
        'box.SelectionColor = box.ForeColor
    End Sub

    Private Sub BindReportContentFilter(ByRef dtReportContentFilter As DataTable, ByRef gridCtrl As GridControl, ByVal rowIndex As Integer)
        Dim isFirstIndex As Boolean = True
        Dim objFldFound As Boolean = False
        Dim noOfrows As Integer = dtReportContentFilter.Rows.Count
        Dim rowNo As Integer = 1

        If gridCtrl.Name.Contains("Query") Then

            RemoveHandler gvQuery.CustomRowCellEdit, AddressOf gvQuery_CustomRowCellEdit

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
                    drQuery("FilterType") = drFilter(ReportContentFilterFields.FilterType)
                Else
                    For Each ctrl As DevExSandBoxField In flp_DimensionsQuery.Controls
                        If filterParam.FilterDimension.Contains(".") Then
                            If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.Split(".")(1).ToString) Then
                                filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue).ToString.Trim
                                filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                                drQuery("Dimension") = filterParam.FilterDimension.Split(".")(1).ToString
                                drQuery("Value") = drFilter(ReportContentFilterFields.FilterValue)
                                drQuery("FilterType") = drFilter(ReportContentFilterFields.FilterType)
                                objFldFound = True
                                Exit For
                            End If
                        Else
                            If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.ToString) Then
                                filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue).ToString.Trim
                                filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                                drQuery("Value") = drFilter(ReportContentFilterFields.FilterValue)
                                drQuery("FilterType") = drFilter(ReportContentFilterFields.FilterType)
                                objFldFound = True
                                Exit For
                            End If
                        End If

                    Next

                    If objFldFound = False Then
                        filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                        filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                        drQuery("Value") = drFilter(ReportContentFilterFields.FilterValue).ToString.Trim
                        drQuery("FilterType") = drFilter(ReportContentFilterFields.FilterType)
                    End If
                End If

                filterParam.FilterLogicalLink = drFilter(ReportContentFilterFields.LogicalLink)
                If (rowNo = noOfrows) Then
                    isFirstIndex = False
                    filterParam.FilterLogicalLink = ""
                End If

                drQuery("LogicalLink") = drFilter(ReportContentFilterFields.LogicalLink)

                drQuery("ObjectFieldType") = drFilter(ReportContentFilterFields.ObjectFieldType)
                filterParam.ObjectFieldType = drFilter(ReportContentFilterFields.ObjectFieldType)

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
            btnDelete.Tag = "Query"
            btnDelete.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
            AddHandler btnDelete.ButtonClick, AddressOf btnDeleteQuery_ButtonClick

            'Add an Unbound Column for the button
            Dim colDelete = gvQuery.Columns.AddField("Delete")
            colDelete.Tag = "Query"
            colDelete.Visible = True
            colDelete.ColumnEdit = btnDelete
            colDelete.ShowButtonMode = Views.Base.ShowButtonModeEnum.ShowAlways

            AddHandler gvQuery.CustomRowCellEdit, AddressOf gvQuery_CustomRowCellEdit

        ElseIf gridCtrl.Name.Contains("Result") Then

            RemoveHandler gvResult.CustomRowCellEdit, AddressOf gvResult_CustomRowCellEdit

            For Each drFilter As DataRow In dtReportContentFilter.Rows
                isFirstIndex = True
                objFldFound = False
                Dim filterParam As FilterParam = New FilterParam()
                Dim drResult As DataRow = dtResult.NewRow()

                filterParam.FilterDimension = drFilter(ReportContentFilterFields.FilterDimension)
                filterParam.ObjectFieldType = drFilter(ReportContentFilterFields.ObjectFieldType)
                drResult("Dimension") = drFilter(ReportContentFilterFields.FilterDimension)
                filterParam.FilterOperator = drFilter(ReportContentFilterFields.FilterOperator)
                drResult("Operator") = drFilter(ReportContentFilterFields.FilterOperator)

                If (filterParam.FilterDimension.ToUpper = "PERIOD_START_TIME") Then
                    filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                    filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                    drResult("Value") = drFilter(ReportContentFilterFields.FilterValue)
                    drResult("FilterType") = drFilter(ReportContentFilterFields.FilterType)
                Else
                    For Each ctrl As DevExSandBoxField In flp_DimensionsQuery.Controls
                        If filterParam.FilterDimension.Contains(".") Then
                            If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.Split(".")(1).ToString) Then
                                filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                                filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                                drResult("Value") = drFilter(filterParam.FilterValue)
                                drResult("FilterType") = drFilter(filterParam.FilterType)
                                objFldFound = True
                                Exit For
                            End If
                        Else
                            If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.ToString) Then
                                filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                                filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                                drResult("Value") = drFilter(filterParam.FilterValue)
                                drResult("FilterType") = drFilter(filterParam.FilterType)
                                objFldFound = True
                                Exit For
                            End If
                        End If

                    Next

                    If objFldFound = False Then
                        filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                        filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                        drResult("Value") = drFilter(ReportContentFilterFields.FilterValue)
                        drResult("FilterType") = drFilter(ReportContentFilterFields.FilterType)
                    End If
                End If

                filterParam.FilterLogicalLink = drFilter(ReportContentFilterFields.LogicalLink)
                If (rowNo = noOfrows) Then
                    isFirstIndex = False
                    filterParam.FilterLogicalLink = ""
                End If

                drResult("LogicalLink") = drFilter(ReportContentFilterFields.LogicalLink)
                filterParam.ObjectFieldType = drFilter(ReportContentFilterFields.ObjectFieldType)

                dtResult.Rows.Add(drResult)

                FilterParamList.Add(filterParam)
                rowIndex += 1
                rowNo += 1
                isFirstIndex = False
            Next

            IOSDevExpressGrid.PopulateDataInGrid(gcResult, gvResult, dtResult, "ALL", {"FilterType", "ObjectFieldType"}, "Dimension")

            Dim btnDelete As New RepositoryItemButtonEdit()
            btnDelete.TextEditStyle = TextEditStyles.HideTextEditor
            btnDelete.Buttons(0).Kind = ButtonPredefines.Glyph
            btnDelete.LookAndFeel.UseDefaultLookAndFeel = True
            btnDelete.Buttons(0).Caption = "Delete"
            btnDelete.LookAndFeel.SkinName = "DevExpress Style"
            'btnDelete.LookAndFeel.SkinMaskColor = Color.Blue
            btnDelete.Tag = "Result"
            btnDelete.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
            AddHandler btnDelete.ButtonClick, AddressOf btnDeleteResult_ButtonClick

            'Add an Unbound Column for the button
            Dim colDelete = gvResult.Columns.AddField("Delete")
            colDelete.Tag = "Result"
            colDelete.Visible = True
            colDelete.ColumnEdit = btnDelete
            colDelete.ShowButtonMode = Views.Base.ShowButtonModeEnum.ShowAlways

            AddHandler gvResult.CustomRowCellEdit, AddressOf gvResult_CustomRowCellEdit

        End If
    End Sub

    Private Sub gvQuery_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        Dim gc As GridControl = view.GridControl

        If e.Column.FieldName = "Operator" Then
            CreateOperatorCombo(gc)
            e.RepositoryItem = riOperatorCombo
        End If

        If e.Column.FieldName = "Value" Then
            Dim dimensionName As String = view.GetRowCellValue(e.RowHandle, "Dimension").ToString
            Dim value As String = view.GetRowCellValue(e.RowHandle, "Value").ToString

            If dimensionName.ToUpper = "PERIOD_START_TIME" Then
                e.RepositoryItem = riDateEdit
            ElseIf view.GetRowCellValue(e.RowHandle, "Operator") = "IN" Or view.GetRowCellValue(e.RowHandle, "Operator") = "NOT IN" Then
                CreateMultiParamValuesCombo(gc, dimensionName)
                e.RepositoryItem = riCheckedCombo
            Else
                CreateParamValuesCombo(gc, dimensionName)
                e.RepositoryItem = riCombo
            End If
        End If
    End Sub

    Private Sub gvResult_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        Dim gc As GridControl = view.GridControl

        If e.Column.FieldName = "Operator" Then
            CreateOperatorCombo(gc)
            e.RepositoryItem = riOperatorCombo
        End If

        If e.Column.FieldName = "Value" Then
            Dim dimensionName As String = view.GetRowCellValue(e.RowHandle, "Dimension").ToString
            Dim value As String = view.GetRowCellValue(e.RowHandle, "Value").ToString

            If dimensionName.ToUpper = "PERIOD_START_TIME" Then
                e.RepositoryItem = riDateEdit
            ElseIf view.GetRowCellValue(e.RowHandle, "Operator") = "IN" Or view.GetRowCellValue(e.RowHandle, "Operator") = "NOT IN" Then
                CreateMultiParamValuesCombo(gc, dimensionName)
                e.RepositoryItem = riCheckedCombo
            Else
                CreateParamValuesCombo(gc, dimensionName)
                e.RepositoryItem = riCombo
            End If
        End If
    End Sub

    Private Sub btnDeleteQuery_ButtonClick(sender As Object, e As ButtonPressedEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim btnDeleteFilter As ButtonEdit = TryCast(sender, ButtonEdit)
            'If (btnDeleteFilter.Tag = "Query") Then

            If (FilterParamList.Count > 0) Then
                Dim _filterParam As FilterParam = Nothing
                For Each filterParam As FilterParam In FilterParamList
                    If filterParam.FilterDimension.ToString.ToUpper.Contains(gvQuery.GetFocusedRowCellValue("Dimension").ToString.ToUpper) Then
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
            End If
            'End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnDeleteResult_ButtonClick(sender As Object, e As ButtonPressedEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim btnDeleteFilter As DevExpress.XtraEditors.ButtonEdit = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            'If (btnDeleteFilter.Tag = "Result") Then

            If (FilterParamList.Count > 0) Then
                Dim _filterParam As FilterParam = Nothing
                For Each filterParam As FilterParam In FilterParamList
                    If filterParam.FilterDimension.ToString.ToUpper.Contains(gvResult.GetFocusedRowCellValue("Dimension").ToString.ToUpper) Then
                        _filterParam = filterParam
                        Exit For
                    End If
                Next
                If (_filterParam IsNot Nothing) Then
                    FilterParamList.Remove(_filterParam)
                End If
            End If

            If xtcReportFilter.SelectedTabPageIndex = 1 Then
                Dim datarow = gvResult.GetDataRow(gvResult.FocusedRowHandle)
                dtResult.Rows.Remove(datarow)

                If (FilterParamList.Count > 0) Then
                    SetSQLResult()
                Else
                    SetSQLResult()
                End If
            End If
            'End If
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

    Private Sub btn_ReportContentFilterCommit_Click(sender As Object, e As EventArgs) Handles btn_ReportContentFilterCommitQuery.Click, btn_ReportContentFilterCommitResult.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (FilterParamList.Count > 0) Then
                Dim sqlCommand As String = SQLReportContentFilter.DeleteByReportID(_reportId)
                Dim alteredFilterParam As List(Of FilterParam) = GetFilteredParam()
                For Each filters As FilterParam In alteredFilterParam
                    If filters.FilterOperator = "IN" Or filters.FilterOperator = "NOT IN" Then
                        Dim MultiFilterVal As String = ""
                        If filters.FilterValue.Contains("(") Or filters.FilterValue.Contains(")") Then
                            MultiFilterVal = filters.FilterValue.Trim.Replace("('('", "(''").Replace("')')", "'')").Replace("'", "''")
                        Else
                            MultiFilterVal = filters.FilterValue.Trim.Replace("'", "''")
                        End If
                        sqlCommand = sqlCommand & SQLReportContentFilter.InsertReportContent_Filter(_reportId, filters.FilterDimension, filters.FilterOperator, MultiFilterVal, filters.FilterLogicalLink, filters.FilterType, filters.ObjectFieldType)
                    Else
                        sqlCommand = sqlCommand & SQLReportContentFilter.InsertReportContent_Filter(_reportId, filters.FilterDimension, filters.FilterOperator, filters.FilterValue.Trim, filters.FilterLogicalLink, filters.FilterType, filters.ObjectFieldType)
                    End If
                Next
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlCommand)
                IsFilterInserted = True
            Else
                Dim sqlCommand As String = SQLReportContentFilter.DeleteByReportID(_reportId)
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlCommand)
            End If

            CheckFilterApply()

            Me.Close()
        Catch ex As Exception
            IsFilterInserted = False
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_CancelResult.Click, btn_CancelQuery.Click
        CheckFilterApply()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CheckFilterApply()
        Dim dtExitReportContentFilter As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContentFilter.GetReportContentFilter(_reportId))
        If (dtExitReportContentFilter IsNot Nothing) AndAlso (dtExitReportContentFilter.Rows.Count > 0) Then
            _isFilterInserted = True
        Else
            _isFilterInserted = False
        End If
    End Sub

    Private Sub GridControl_DragOver(sender As Object, e As DragEventArgs) Handles gcQuery.DragOver, gcResult.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub gcQuery_DragDrop(sender As Object, e As DragEventArgs) Handles gcQuery.DragDrop
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim items As String = e.Data.GetData(DataFormats.Text).ToString
            Dim draggedObjectText As String = items.Split("#")(0).ToString
            Dim draggedObjectTag As String = items.Split("#")(1).ToString
            Dim draggedObjectFieldType As Integer = items.Split("#")(2).ToString

            Dim filterParam As FilterParam = New FilterParam()

            If dtQuery IsNot Nothing Then

                Dim drQuery As DataRow = dtQuery.NewRow()

                drQuery("Dimension") = draggedObjectText

                If draggedObjectTag <> "" Then
                    filterParam.FilterDimension = draggedObjectTag & "." & draggedObjectText
                Else
                    filterParam.FilterDimension = draggedObjectText
                End If

                drQuery("Operator") = "="
                filterParam.FilterOperator = "="

                If (draggedObjectText.ToUpper = "PERIOD_START_TIME") Then
                    drQuery("Value") = Date.Now.ToString("dd-MM-yyyy")
                    filterParam.FilterValue = Date.Now.ToString("dd-MM-yyyy")
                ElseIf draggedObjectFieldType = DatamartFieldType.ObjectFld Then
                    drQuery("Value") = ""
                    filterParam.FilterValue = ""
                Else
                    drQuery("Value") = "0"
                    filterParam.FilterValue = "0"
                End If

                drQuery("LogicalLink") = ""
                drQuery("FilterType") = "QUERY"

                filterParam.FilterType = "QUERY"

                drQuery("ObjectFieldType") = draggedObjectFieldType
                filterParam.ObjectFieldType = draggedObjectFieldType

                For Each filterParamTem As FilterParam In FilterParamList
                    If (filterParamTem.FilterLogicalLink = "") Then
                        filterParamTem.FilterLogicalLink = "AND"
                    End If
                Next
                FilterParamList.Add(filterParam)

                If dtQuery.Rows.Count > 0 Then
                    Dim lastRowIndex As Integer = dtQuery.Rows.Count - 1
                    dtQuery.Rows(lastRowIndex)("LogicalLink") = "AND"
                    dtQuery.AcceptChanges()
                End If

                dtQuery.Rows.Add(drQuery)
            End If

            If gcQuery.DataSource Is Nothing Then
                IOSDevExpressGrid.PopulateDataInGrid(gcQuery, gvQuery, dtQuery, "ALL", {"FilterType", "ObjectFieldType"}, "Dimension")

                Dim btnDelete As New RepositoryItemButtonEdit()
                btnDelete.TextEditStyle = TextEditStyles.HideTextEditor
                btnDelete.Buttons(0).Kind = ButtonPredefines.Glyph
                btnDelete.LookAndFeel.UseDefaultLookAndFeel = True
                btnDelete.Buttons(0).Caption = "Delete"
                btnDelete.LookAndFeel.SkinName = "DevExpress Style"
                'btnDelete.LookAndFeel.SkinMaskColor = Color.Blue
                btnDelete.Tag = "Query"
                btnDelete.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
                AddHandler btnDelete.ButtonClick, AddressOf btnDeleteQuery_ButtonClick

                'Add an Unbound Column for the button
                Dim colDelete = gvQuery.Columns.AddField("Delete")
                colDelete.Tag = "Query"
                colDelete.Visible = True
                colDelete.ColumnEdit = btnDelete
                colDelete.ShowButtonMode = Views.Base.ShowButtonModeEnum.ShowAlways
            Else
                gcQuery.RefreshDataSource()
            End If

            SetSQLQuery()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gcResult_DragDrop(sender As Object, e As DragEventArgs) Handles gcResult.DragDrop
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim items As String = e.Data.GetData(DataFormats.Text).ToString
            Dim draggedObjectText As String = items.Split("#")(0).ToString
            Dim draggedObjectTag As String = items.Split("#")(1).ToString
            Dim draggedObjectFieldType As Integer = items.Split("#")(2).ToString
            Dim filterParam As FilterParam = New FilterParam()

            If dtResult IsNot Nothing Then

                Dim drResult As DataRow = dtResult.NewRow()

                drResult("Dimension") = draggedObjectText

                If draggedObjectTag <> "" Then
                    filterParam.FilterDimension = draggedObjectTag & "." & draggedObjectText
                Else
                    filterParam.FilterDimension = draggedObjectText
                End If

                drResult("Operator") = "="
                filterParam.FilterOperator = "="

                If (draggedObjectText.ToUpper = "PERIOD_START_TIME") Then
                    drResult("Value") = CDate(Date.Now.ToString("dd-MM-yyyy"))
                    filterParam.FilterValue = CDate(Date.Now.ToString("dd-MM-yyyy"))
                ElseIf draggedObjectFieldType = DatamartFieldType.ObjectFld Then
                    drResult("Value") = draggedObjectText
                    filterParam.FilterValue = draggedObjectText
                Else
                    drResult("Value") = "0"
                    filterParam.FilterValue = "0"
                End If

                drResult("LogicalLink") = "AND"
                drResult("FilterType") = "RESULT"

                filterParam.FilterType = "RESULT"

                drResult("ObjectFieldType") = draggedObjectFieldType
                filterParam.ObjectFieldType = draggedObjectFieldType

                For Each filterParamTem As FilterParam In FilterParamList
                    If (filterParamTem.FilterLogicalLink = "") Then
                        filterParamTem.FilterLogicalLink = "AND"
                    End If
                Next
                FilterParamList.Add(filterParam)

                dtResult.Rows.Add(drResult)
            End If

            If gcResult.DataSource Is Nothing Then
                IOSDevExpressGrid.PopulateDataInGrid(gcResult, gvResult, dtResult, "ALL", {"FilterType", "ObjectFieldType"}, "Dimension")

                Dim btnDelete As New RepositoryItemButtonEdit()
                btnDelete.TextEditStyle = TextEditStyles.HideTextEditor
                btnDelete.Buttons(0).Kind = ButtonPredefines.Glyph
                btnDelete.LookAndFeel.UseDefaultLookAndFeel = True
                btnDelete.Buttons(0).Caption = "Delete"
                btnDelete.LookAndFeel.SkinName = "DevExpress Style"
                btnDelete.Tag = "Result"
                btnDelete.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
                AddHandler btnDelete.ButtonClick, AddressOf btnDeleteResult_ButtonClick

                'Add an Unbound Column for the button
                Dim colDelete = gvResult.Columns.AddField("Delete")
                colDelete.Tag = "Result"
                colDelete.Visible = True
                colDelete.ColumnEdit = btnDelete
                colDelete.ShowButtonMode = Views.Base.ShowButtonModeEnum.ShowAlways
            Else
                gcResult.RefreshDataSource()
            End If

            SetSQLResult()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub gcDragValue_DragOver(sender As Object, e As DragEventArgs) Handles gcQuery.DragOver, gcResult.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub gvQuery_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvQuery.ShowingEditor
        Try
            If (gvQuery.FocusedColumn.FieldName = "Value") Or (gvQuery.FocusedColumn.FieldName = "Operator") Or
                (gvQuery.FocusedColumn.FieldName = "LogicalLink") Or (gvQuery.FocusedColumn.FieldName = "Delete") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvResult_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvResult.ShowingEditor
        Try
            If (gvResult.FocusedColumn.FieldName = "Value") Or (gvResult.FocusedColumn.FieldName = "Operator") Or
                (gvResult.FocusedColumn.FieldName = "LogicalLink") Or (gvResult.FocusedColumn.FieldName = "Delete") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetDynamicValues(dimensionName As String) As Object
        If objectTableName IsNot Nothing Then
            dtObjectTypeValues = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContentFilter.GetReportDimensionDistinctValues(dimensionName, objectTableName))
            Return dtObjectTypeValues
        End If
    End Function

    Private Sub gvQuery_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles gvQuery.CellValueChanged
        Try
            gcQuery.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            SetSQLQuery()
            SetFilterParamList("QUERY")
        Catch ex As Exception
        Finally
            gcQuery.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvResult_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles gvResult.CellValueChanged
        Try
            SetSQLResult()
            SetFilterParamList("RESULT")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetFilterParamList(filterType As String)
        If FilterParamList IsNot Nothing Then
            FilterParamList.RemoveAll(Function(x) x.FilterType?.ToUpper() = filterType)
        End If
        If filterType = "QUERY" Then
            For Each dr As DataRow In dtQuery.Rows
                Dim filterParam As FilterParam = New FilterParam()
                filterParam.FilterDimension = dr("Dimension").ToString
                filterParam.FilterOperator = dr("Operator").ToString
                If filterParam.FilterOperator = "IN" Or filterParam.FilterOperator = "NOT IN" Then
                    If dr("Value").ToString.Contains("(") Or dr("Value").ToString.Contains(")") Then
                        filterParam.FilterValue = " " + dr("Value").ToString + " "
                    Else
                        filterParam.FilterValue = " (" + String.Join(", ", dr("Value").ToString.Split(","c).Select(Function(s) $"'{s.Trim()}'")) + ") "
                    End If
                Else
                    filterParam.FilterValue = dr("Value").ToString
                End If

                filterParam.FilterType = dr("FilterType").ToString
                filterParam.FilterLogicalLink = dr("LogicalLink").ToString
                filterParam.ObjectFieldType = dr("ObjectFieldType").ToString

                FilterParamList.Add(filterParam)
            Next
        ElseIf filterType = "RESULT" Then
            For Each dr As DataRow In dtResult.Rows
                Dim filterParam As FilterParam = New FilterParam()
                filterParam.FilterDimension = dr("Dimension").ToString
                filterParam.FilterOperator = dr("Operator").ToString
                If filterParam.FilterOperator = "IN" Or filterParam.FilterOperator = "NOT IN" Then
                    filterParam.FilterValue = " (" + String.Join(", ", dr("Value").ToString.Split(","c).Select(Function(s) $"'{s.Trim()}'")) + ") "
                Else
                    filterParam.FilterValue = dr("Value").ToString
                End If

                filterParam.FilterType = dr("FilterType").ToString
                filterParam.FilterLogicalLink = dr("LogicalLink").ToString
                filterParam.ObjectFieldType = dr("ObjectFieldType").ToString

                FilterParamList.Add(filterParam)
            Next
        End If
    End Sub

    Private Sub gvQuery_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles gvQuery.RowCellClick
        Try
            Dim gv As GridView = TryCast(sender, GridView)
            Dim gc As GridControl = gv.GridControl

            If e.Column.FieldName = "Operator" Then
                CreateOperatorCombo(gc)
            ElseIf e.Column.FieldName = "Value" AndAlso (gvQuery.GetRowCellValue(e.RowHandle, "Dimension") = "PERIOD_START_TIME") Then
                riDateEdit.EditMask = "dd/MM/yyyy"
                riDateEdit.DisplayFormat.FormatString = "dd/MM/yyyy"
                riDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                riDateEdit.EditFormat.FormatString = "dd/MM/yyyy"
                riDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                riDateEdit.Mask.UseMaskAsDisplayFormat = True
            ElseIf e.Column.FieldName = "Value" Then
                If (gv.GetRowCellValue(e.RowHandle, "Operator").ToString = "IN") Or (gv.GetRowCellValue(e.RowHandle, "Operator").ToString = "NOT IN") Then
                    CreateMultiParamValuesCombo(gc, gv.GetFocusedRowCellValue("Dimension").ToString)
                Else
                    CreateParamValuesCombo(gc, gv.GetFocusedRowCellValue("Dimension").ToString)
                End If
            ElseIf e.Column.FieldName = "Delete" Then
                btnDeleteQuery_ButtonClick(e.Button, Nothing)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvResult_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles gvResult.RowCellClick
        Try
            Try
                Dim gv As GridView = TryCast(sender, GridView)
                Dim gc As GridControl = gv.GridControl

                If e.Column.FieldName = "Operator" Then
                    CreateOperatorCombo(gc)
                ElseIf e.Column.FieldName = "Value" Then
                    If (gv.GetRowCellValue(e.RowHandle, "Operator").ToString = "IN") Or (gv.GetRowCellValue(e.RowHandle, "Operator").ToString = "NOT IN") Then
                        CreateMultiParamValuesCombo(gc, gv.GetFocusedRowCellValue("Dimension").ToString)
                    Else
                        CreateParamValuesCombo(gc, gv.GetFocusedRowCellValue("Dimension").ToString)
                    End If
                ElseIf e.Column.FieldName = "Delete" Then
                    btnDeleteResult_ButtonClick(e.Button, Nothing)
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CreateOperatorCombo(ByRef gc As GridControl)
        Try
            riOperatorCombo = New RepositoryItemComboBox()
            gc.RepositoryItems.Add(riOperatorCombo)
            'RemoveHandler riOperatorCombo.SelectedIndexChanged, AddressOf riOperatorCombo_SelectedIndexChanged
            Dim items As String() = {"=", "<>", ">", "<", ">=", "<=", "+", "-", "*", "/", "^", "AND", "OR", "NOT", "LIKE", "IN", "NOT IN"}
            riOperatorCombo.Items.AddRange(items)
            'AddHandler riOperatorCombo.SelectedIndexChanged, AddressOf riOperatorCombo_SelectedIndexChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub CreateParamValuesCombo(ByRef gc As GridControl, dimenaionName As String)
        Try
            riCombo = New RepositoryItemComboBox()
            'RemoveHandler riCombo.SelectedIndexChanged, AddressOf riCombo_SelectedIndexChanged
            Dim dt As DataTable = GetDynamicValues(dimenaionName)
            If Not dt Is Nothing Then
                gc.RepositoryItems.Clear()
                gc.RepositoryItems.Add(riCombo)
                riCombo.AutoHeight = False
                Dim items As String() = dt.AsEnumerable().Select(Function(x) x.Field(Of Object)(dimenaionName).ToString).ToArray()
                riCombo.Items.AddRange(items)
            Else
                riCombo.Items.Clear()
            End If
            'AddHandler riCombo.SelectedIndexChanged, AddressOf riCombo_SelectedIndexChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub CreateMultiParamValuesCombo(ByRef gc As GridControl, dimenaionName As String)
        Try
            riCheckedCombo = New RepositoryItemCheckedComboBoxEdit()
            'RemoveHandler riCheckedCombo.EditValueChanged, AddressOf riCheckedCombo_EditValueChanged
            Dim dt As DataTable = GetDynamicValues(dimenaionName)
            If Not dt Is Nothing Then
                gc.RepositoryItems.Clear()
                gc.RepositoryItems.Add(riCheckedCombo)
                riCheckedCombo.AutoHeight = False
                Dim items As String() = dt.AsEnumerable().Select(Function(x) x.Field(Of Object)(dimenaionName).ToString).ToArray()
                riCheckedCombo.Items.AddRange(items)
            Else
                riCheckedCombo.Items.Clear()
            End If
            'AddHandler riCheckedCombo.EditValueChanged, AddressOf riCheckedCombo_EditValueChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

End Class