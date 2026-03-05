Imports IOS.DataLibrary
Imports IOS.Library

Public Class frmSBReportContentFilters

    'Dim conn_SandBox As String = IOS.Configuration.IOSAppConfigManage.SandBox_Server

    Private dtObjectTypeValues As DataTable = Nothing
    Public FilterParamList As List(Of FilterParam) = New List(Of FilterParam)()
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

    'Public Sub SetConnectionString(ByVal connstr As String)
    'conn_SandBox = connstr
    'End Sub

    Private Sub frm_ReportContentFilters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            lbl_MessageResult.Text = ""
            _isFilterInserted = False
            If (_reportId IsNot Nothing) Then
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

    'Private Sub GetObjectTypeValues()
    '    Dim sql As String = "Select * From " & objectTableName
    '    dtObjectTypeValues = DataAccessorODBC.GetDataTable(reportConnString, sql)
    'End Sub

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
                    If (vSandBoxFieldExist.Text.ToUpper = "PERIOD_START_TIME") Then 'Or (CType(objSandbox.lbDimensions.DataSource, DataTable).Select("COLUMN_NAME= '" & vSandBoxFieldExist.Text & "'").Length > 0) Then
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

    Private Function GetFilteredParam()
        Dim alteredFilterParam As List(Of FilterParam) = New List(Of FilterParam)
        If xtcReportFilter.SelectedTabPageIndex = 0 Then
            alteredFilterParam = FilterParamList.Where(Function(x) x.FilterType = "QUERY").ToList()
        ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
            alteredFilterParam = FilterParamList.Where(Function(x) x.FilterType = "RESULT").ToList()
        End If
        Return alteredFilterParam
    End Function

    Private Sub btn_ReportContentFilterCommitResult_Click(sender As Object, e As EventArgs) Handles btn_ReportContentFilterCommitResult.Click, btn_ReportContentFilterCommitQuery.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If (FilterParamList.Count > 0) Then
                Dim sqlCommand As String = SQLReportContentFilter.DeleteByReportID(_reportId)
                Dim alteredFilterParam As List(Of FilterParam) = GetFilteredParam()
                For Each filters As FilterParam In alteredFilterParam
                    '          filters.FilterDimension = IIf(filters.FilterDimension.Contains("."), filters.FilterDimension, objectTableName & "." & filters.FilterDimension)
                    sqlCommand = sqlCommand & SQLReportContentFilter.InsertReportContent_Filter(_reportId, filters.FilterDimension, filters.FilterOperator, filters.FilterValue, filters.FilterLogicalLink, filters.FilterType, filters.ObjectFieldType)
                Next
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlCommand)
            Else
                Dim sqlCommand As String = SQLReportContentFilter.DeleteByReportID(_reportId)
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, sqlCommand)
            End If

            CheckFilterApply()
            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_CancelResult.Click, btn_CancelQuery.Click
        CheckFilterApply()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
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

    Private Sub GetReportContentFilter()
        Try
            Dim dtExitReportContentFilter As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContentFilter.GetReportContentFilter(_reportId))
            exTLP_FilterStatmentResult.SuspendLayout()

            If (dtExitReportContentFilter.Rows.Count > 0) Then

                If dtExitReportContentFilter.Rows(0)("QueryOrResult").ToString.ToUpper = "QUERY" Then
                    vGBox_FilterStatementQuery.Text = "Filter Statement for " & dtExitReportContentFilter.Rows(0)(ReportContentFilterFields.ReportName).ToString
                    BindReportContentFilter(dtExitReportContentFilter, tlp_ReportContentFilterQuery, tlp_ReportContentFilterQuery.RowCount)

                    SetSQLQuery()

                    CreateEmptyControls(tlp_ReportContentFilterQuery, tlp_ReportContentFilterQuery.RowCount)
                Else
                    vGBox_FilterStatementResult.Text = "Filter Statement for " & dtExitReportContentFilter.Rows(0)(ReportContentFilterFields.ReportName).ToString
                    BindReportContentFilter(dtExitReportContentFilter, tlp_ReportContentFilterResult, tlp_ReportContentFilterResult.RowCount)

                    SetSQLResult()

                    CreateEmptyControls(tlp_ReportContentFilterResult, tlp_ReportContentFilterResult.RowCount)
                End If
            Else
                CreateEmptyControls(tlp_ReportContentFilterQuery, tlp_ReportContentFilterQuery.RowCount)
                CreateEmptyControls(tlp_ReportContentFilterResult, tlp_ReportContentFilterResult.RowCount)
            End If

            tlp_ReportContentFilterQuery.ResumeLayout()
            tlp_ReportContentFilterResult.ResumeLayout()

            RefrashFilterContents()

            exTLP_FilterStatmentQuery.ResumeLayout()
            exTLP_FilterStatmentQuery.Update()
            exTLP_FilterStatmentQuery.Refresh()

            exTLP_FilterStatmentResult.ResumeLayout()
            exTLP_FilterStatmentResult.Update()
            exTLP_FilterStatmentResult.Refresh()
        Catch ex As Exception
            SetMessage("Error : Filters Fetching fail.")
        End Try
    End Sub

    Private Sub RefrashFilterContents()
        If (exTPL_DragdropAndFiltersQuery.RowStyles(0).Height + 40.0! < exTPL_DragdropAndFiltersQuery.Height) Then
            exTPL_DragdropAndFiltersQuery.RowStyles(0).Height = tlp_ReportContentFilterQuery.RowStyles.Count * 28.0!
        End If

        If (exTPL_DragdropAndFiltersResult.RowStyles(0).Height + 40.0! < exTPL_DragdropAndFiltersResult.Height) Then
            exTPL_DragdropAndFiltersResult.RowStyles(0).Height = tlp_ReportContentFilterResult.RowStyles.Count * 28.0!
        End If
    End Sub

    Private Sub BindReportContentFilter(ByRef dtReportContentFilter As DataTable, ByRef tlp_ReportContentFilter As IOS.Library.ExTableLayoutPanel, ByVal rowIndex As Integer)

        Dim isFirstIndex As Boolean = True
        Dim objFldFound As Boolean = False
        Dim noOfrows As Integer = dtReportContentFilter.Rows.Count
        Dim rowNo As Integer = 1
        For Each drFilter As DataRow In dtReportContentFilter.Rows
            isFirstIndex = True
            objFldFound = False
            Dim filterParam As FilterParam = New FilterParam()
            tlp_ReportContentFilter.RowStyles.Insert(rowIndex, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
            tlp_ReportContentFilter.RowCount = tlp_ReportContentFilter.RowCount + 1
            filterParam.FilterDimension = drFilter(ReportContentFilterFields.FilterDimension)
            filterParam.ObjectFieldType = drFilter(ReportContentFilterFields.ObjectFieldType)
            tlp_ReportContentFilter.Controls.Add(CreateLabel("_" & filterParam.FilterDimension, filterParam.FilterDimension, Color.Black), 0, rowIndex)

            filterParam.FilterOperator = drFilter(ReportContentFilterFields.FilterOperator)
            tlp_ReportContentFilter.Controls.Add(GetOperatorCombo("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterOperator), 1, rowIndex)

            If (filterParam.FilterDimension.ToUpper = "PERIOD_START_TIME") Then
                filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                tlp_ReportContentFilter.Controls.Add(GetDatePicker("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterValue), 2, rowIndex)
            Else
                For Each ctrl As DevExSandBoxField In flp_DimensionsQuery.Controls
                    If filterParam.FilterDimension.Contains(".") Then
                        If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.Split(".")(1).ToString) Then
                            filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                            filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                            tlp_ReportContentFilter.Controls.Add(GetFieldValueCombo("_" & filterParam.FilterDimension.Split(".")(1), filterParam.FilterDimension.Split(".")(1), filterParam.FilterValue), 2, rowIndex)
                            objFldFound = True
                            Exit For
                        End If
                    Else
                        If (ctrl.VSandBoxType = DatamartFieldType.ObjectFld) AndAlso (ctrl.Text = filterParam.FilterDimension.ToString) Then
                            filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                            filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                            tlp_ReportContentFilter.Controls.Add(GetFieldValueCombo("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterValue), 2, rowIndex)
                            objFldFound = True
                            Exit For
                        End If
                    End If

                Next

                If objFldFound = False Then
                    filterParam.FilterValue = drFilter(ReportContentFilterFields.FilterValue)
                    filterParam.FilterType = drFilter(ReportContentFilterFields.FilterType)
                    tlp_ReportContentFilter.Controls.Add(GetValueTextBox("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterValue), 2, rowIndex)
                End If
            End If

            filterParam.FilterLogicalLink = drFilter(ReportContentFilterFields.LogicalLink)
            If (rowNo = noOfrows) Then
                isFirstIndex = False
                filterParam.FilterLogicalLink = ""
            End If

            tlp_ReportContentFilter.Controls.Add(GetLogicalLinkCombo("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterLogicalLink, isFirstIndex), 3, rowIndex)
            Dim btnFilterDelete As New DevExpress.XtraEditors.SimpleButton()
            btnFilterDelete.BackColor = System.Drawing.Color.Transparent
            btnFilterDelete.Location = New System.Drawing.Point(3, 3)
            btnFilterDelete.Name = "vbtnFilterDelete_" & drFilter(ReportContentFilterFields.FilterDimension)
            btnFilterDelete.Size = New System.Drawing.Size(133, 23)
            btnFilterDelete.TabIndex = 0
            btnFilterDelete.Text = "Delete"
            btnFilterDelete.Dock = DockStyle.Top
            btnFilterDelete.Enabled = True
            btnFilterDelete.Tag = drFilter(ReportContentFilterFields.FilterDimension)
            AddHandler btnFilterDelete.Click, AddressOf btnFilterDelete_Click

            tlp_ReportContentFilter.Controls.Add(btnFilterDelete, 4, rowIndex)
            FilterParamList.Add(filterParam)
            rowIndex += 1
            rowNo += 1
            isFirstIndex = False
        Next

        tlp_ReportContentFilter.Refresh()

    End Sub

    Private Sub BindReportContentFilter(ByRef _filterParamList As List(Of FilterParam), ByRef tlp_ReportContentFilter As IOS.Library.ExTableLayoutPanel, ByVal rowIndex As Integer)

        Dim noOfrows As Integer = _filterParamList.Count
        Dim rowNo As Integer = 1
        Dim isFirstIndex As Boolean = True
        For Each filterParam As FilterParam In _filterParamList

            tlp_ReportContentFilter.RowStyles.Insert(rowIndex, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
            tlp_ReportContentFilter.RowCount = tlp_ReportContentFilter.RowCount + 1
            tlp_ReportContentFilter.Controls.Add(CreateLabel("_" & filterParam.FilterDimension, filterParam.FilterDimension, Color.Black), 0, rowIndex)

            tlp_ReportContentFilter.Controls.Add(GetOperatorCombo("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterOperator), 1, rowIndex)

            tlp_ReportContentFilter.Controls.Add(GetValueTextBox("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterValue), 2, rowIndex)

            If (rowNo = noOfrows) Then
                isFirstIndex = False
                filterParam.FilterLogicalLink = ""
            End If

            tlp_ReportContentFilter.Controls.Add(GetLogicalLinkCombo("_" & filterParam.FilterDimension, filterParam.FilterDimension, filterParam.FilterLogicalLink, isFirstIndex), 3, rowIndex)
            Dim btnFilterDelete As New DevExpress.XtraEditors.SimpleButton()
            'vbtnFilterDelete.AllowAnimations = True
            btnFilterDelete.BackColor = System.Drawing.Color.Transparent
            btnFilterDelete.Location = New System.Drawing.Point(3, 3)
            btnFilterDelete.Name = "vbtnFilterDelete_" & filterParam.FilterDimension
            'vbtnFilterDelete.RoundedCornersMask = CType(15, Byte)
            btnFilterDelete.Size = New System.Drawing.Size(133, 23)
            btnFilterDelete.TabIndex = 0
            btnFilterDelete.Text = "Delete"
            'vbtnFilterDelete.UseVisualStyleBackColor = False
            btnFilterDelete.Dock = DockStyle.Top
            'vbtnFilterDelete.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
            btnFilterDelete.Enabled = True
            btnFilterDelete.Tag = filterParam.FilterDimension
            AddHandler btnFilterDelete.Click, AddressOf btnFilterDelete_Click
            tlp_ReportContentFilter.Controls.Add(btnFilterDelete, 4, rowIndex)
            rowIndex += 1
            rowNo += 1
            isFirstIndex = False

        Next
        tlp_ReportContentFilter.Refresh()
    End Sub

    Private Sub CreateHeaderControls(ByRef _exTLP_FilterStatment As IOS.Library.ExTableLayoutPanel, ByVal rowIndex As Integer)
        Dim exTLP_ReportContentFilterHeader As IOS.Library.ExTableLayoutPanel = New IOS.Library.ExTableLayoutPanel
        exTLP_ReportContentFilterHeader.Dock = DockStyle.Fill
        exTLP_ReportContentFilterHeader.BackColor = Color.DarkGray
        exTLP_ReportContentFilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        exTLP_ReportContentFilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        exTLP_ReportContentFilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        exTLP_ReportContentFilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        exTLP_ReportContentFilterHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        exTLP_ReportContentFilterHeader.Name = "exTLP_InnerReportContentFilterHeader"
        exTLP_ReportContentFilterHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        exTLP_ReportContentFilterHeader.Controls.Add(CreateLabel("Dimension", "Dimension", Color.White), 0, 0)
        exTLP_ReportContentFilterHeader.Controls.Add(CreateLabel("Operator", "Operator", Color.White), 1, 0)
        exTLP_ReportContentFilterHeader.Controls.Add(CreateLabel("Value", "Value", Color.White), 2, 0)
        exTLP_ReportContentFilterHeader.Controls.Add(CreateLabel("LogicalLink", "Logical Link", Color.White), 3, 0)
        exTLP_ReportContentFilterHeader.Controls.Add(CreateLabel("empty", " ", Color.White), 4, 0)
        exTLP_ReportContentFilterHeader.Refresh()
        _exTLP_FilterStatment.Controls.Add(exTLP_ReportContentFilterHeader, 0, rowIndex)
        _exTLP_FilterStatment.Refresh()
    End Sub

    Private Function CreateLabel(ByVal lblName As String, ByVal lblText As String, ByVal fontColor As System.Drawing.Color) As DevExpress.XtraEditors.LabelControl
        Dim lblObj As New DevExpress.XtraEditors.LabelControl()
        lblObj.BackColor = System.Drawing.Color.Transparent
        lblObj.AutoEllipsis = False
        lblObj.Appearance.ImageAlign = ContentAlignment.TopLeft
        lblObj.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

        lblObj.Name = "vlblDimension" & lblName
        lblObj.Size = New System.Drawing.Size(100, 23)

        If (lblText.Contains(".")) Then
            lblObj.Text = lblText.Split(".")(1).ToString
        Else
            lblObj.Text = lblText
        End If

        lblObj.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
        lblObj.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        lblObj.Dock = System.Windows.Forms.DockStyle.Fill
        lblObj.ForeColor = fontColor
        lblObj.Height = 23
        lblObj.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Return lblObj
    End Function

    Private Function GetOperatorCombo(ByVal cmbName As String, ByVal tagValue As String, ByVal selectedValue As String) As DevExpress.XtraEditors.ComboBoxEdit
        Dim cmbFilterOperator As New DevExpress.XtraEditors.ComboBoxEdit()
        cmbFilterOperator.BackColor = System.Drawing.Color.White
        cmbFilterOperator.Dock = System.Windows.Forms.DockStyle.Top
        Dim ListItem1 As New clsComboBoxItem()
        ListItem1.Text = "="
        ListItem1.Value = "="

        Dim ListItem2 As New clsComboBoxItem()
        ListItem2.Text = "<>"
        ListItem2.Value = "<>"

        Dim ListItem3 As New clsComboBoxItem()
        ListItem3.Text = ">"
        ListItem3.Value = ">"

        Dim ListItem4 As New clsComboBoxItem()
        ListItem4.Text = "<"
        ListItem4.Value = "<"

        Dim ListItem5 As New clsComboBoxItem()
        ListItem5.Text = "<="
        ListItem5.Value = "<="

        Dim ListItem6 As New clsComboBoxItem()
        ListItem6.Text = ">="
        ListItem6.Value = ">="

        Dim ListItem7 As New clsComboBoxItem()
        ListItem7.Text = "+"
        ListItem7.Value = "+"

        Dim ListItem8 As New clsComboBoxItem()
        ListItem8.Text = "-"
        ListItem8.Value = "-"

        Dim ListItem9 As New clsComboBoxItem()
        ListItem9.Text = "*"
        ListItem9.Value = "*"

        Dim ListItem10 As New clsComboBoxItem()
        ListItem10.Text = "/"
        ListItem10.Value = "/"

        Dim ListItem11 As New clsComboBoxItem()
        ListItem11.Text = "^"
        ListItem11.Value = "^"

        Dim ListItem12 As New clsComboBoxItem()
        ListItem12.Text = "()"
        ListItem12.Value = "()"

        Dim ListItem13 As New clsComboBoxItem()
        ListItem13.Text = "AND"
        ListItem13.Value = "AND"

        Dim ListItem14 As New clsComboBoxItem()
        ListItem14.Text = "OR"
        ListItem14.Value = "OR"

        Dim ListItem15 As New clsComboBoxItem()
        ListItem15.Text = "NOT"
        ListItem15.Value = "NOT"

        Dim ListItem16 As New clsComboBoxItem()
        ListItem16.Text = "LIKE"
        ListItem16.Value = "LIKE"

        Dim ListItem17 As New clsComboBoxItem()
        ListItem17.Text = "Round(,)"
        ListItem17.Value = "Round(,)"

        cmbFilterOperator.Properties.Items.Add(ListItem1)
        cmbFilterOperator.Properties.Items.Add(ListItem2)
        cmbFilterOperator.Properties.Items.Add(ListItem3)
        cmbFilterOperator.Properties.Items.Add(ListItem4)
        cmbFilterOperator.Properties.Items.Add(ListItem5)
        cmbFilterOperator.Properties.Items.Add(ListItem6)
        cmbFilterOperator.Properties.Items.Add(ListItem7)
        cmbFilterOperator.Properties.Items.Add(ListItem8)
        cmbFilterOperator.Properties.Items.Add(ListItem9)
        cmbFilterOperator.Properties.Items.Add(ListItem10)
        cmbFilterOperator.Properties.Items.Add(ListItem11)
        cmbFilterOperator.Properties.Items.Add(ListItem12)
        cmbFilterOperator.Properties.Items.Add(ListItem13)
        cmbFilterOperator.Properties.Items.Add(ListItem14)
        cmbFilterOperator.Properties.Items.Add(ListItem15)
        cmbFilterOperator.Properties.Items.Add(ListItem16)
        cmbFilterOperator.Properties.Items.Add(ListItem17)

        cmbFilterOperator.Name = "vCmbFilterOperator" & cmbName
        cmbFilterOperator.Size = New System.Drawing.Size(100, 23)
        cmbFilterOperator.Tag = tagValue
        cmbFilterOperator.Height = 23
        If (selectedValue.Length > 0) Then
            For Each it As clsComboBoxItem In cmbFilterOperator.Properties.Items
                If it.Text = selectedValue Then
                    cmbFilterOperator.SelectedItem = it
                    Exit For
                End If
            Next
        Else
            cmbFilterOperator.SelectedIndex = 1
        End If
        AddHandler cmbFilterOperator.SelectedValueChanged, AddressOf cmbFilterOperator_SelectedItemChanged
        Return cmbFilterOperator
    End Function

    Private Sub cmbFilterOperator_SelectedItemChanged(sender As Object, e As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim cmbFilterOperatorTmp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(sender, DevExpress.XtraEditors.ComboBoxEdit)
            Dim changedIndex As Integer = TryCast(cmbFilterOperatorTmp.Parent, ExTableLayoutPanel).GetRow(cmbFilterOperatorTmp)

            Dim alteredFilterParam As List(Of FilterParam) = GetFilteredParam()
            alteredFilterParam(changedIndex - 1).FilterOperator = cmbFilterOperatorTmp.SelectedItem.ToString

            If xtcReportFilter.SelectedTabPageIndex = 0 Then
                SetSQLQuery()
            ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
                SetSQLResult()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Function GetDatePicker(ByVal txtName As String, ByVal tagValue As String, ByVal selectedValue As String) As DevExpress.XtraEditors.DateEdit
        Dim dtpValue As DevExpress.XtraEditors.DateEdit = New DevExpress.XtraEditors.DateEdit()
        dtpValue.BackColor = System.Drawing.Color.White
        dtpValue.Dock = System.Windows.Forms.DockStyle.Top
        dtpValue.ForeColor = System.Drawing.SystemColors.Desktop
        dtpValue.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        dtpValue.Properties.MaxValue = New Date(2100, 1, 1, 0, 0, 0, 0)
        dtpValue.Properties.MinValue = New Date(1900, 1, 1, 0, 0, 0, 0)
        dtpValue.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True
        dtpValue.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        dtpValue.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        dtpValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        dtpValue.Properties.EditMask = "dd/MM/yyyy HH:mm"
        dtpValue.Name = "vDtpFilterValue" + txtName
        dtpValue.Size = New System.Drawing.Size(100, 23)
        dtpValue.TabIndex = 1
        dtpValue.Text = Date.Now
        dtpValue.EditValue = selectedValue
        dtpValue.Tag = tagValue
        dtpValue.Height = 23
        AddHandler dtpValue.EditValueChanged, AddressOf dtpValue_ValueChanged
        Return dtpValue
    End Function

    Private Sub dtpValue_ValueChanged(sender As Object, e As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim dtEdit As DevExpress.XtraEditors.DateEdit = TryCast(sender, DevExpress.XtraEditors.DateEdit)
            Dim changedIndex As Integer = TryCast(dtEdit.Parent, ExTableLayoutPanel).GetRow(dtEdit)

            Dim alteredFilterParam As List(Of FilterParam) = GetFilteredParam()
            alteredFilterParam(changedIndex - 1).FilterValue = dtEdit.EditValue

            If xtcReportFilter.SelectedTabPageIndex = 0 Then
                SetSQLQuery()
            ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
                SetSQLResult()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Function GetValueTextBox(ByVal txtName As String, ByVal tagValue As String, ByVal selectedValue As String) As DevExpress.XtraEditors.TextEdit
        Dim txtFilterValue As DevExpress.XtraEditors.TextEdit = New DevExpress.XtraEditors.TextEdit()
        txtFilterValue.BackColor = System.Drawing.Color.White
        txtFilterValue.EditValue = "0"
        txtFilterValue.Dock = System.Windows.Forms.DockStyle.Top
        txtFilterValue.ForeColor = System.Drawing.Color.Black
        txtFilterValue.Properties.MaxLength = 1000
        txtFilterValue.Name = "vTxtFilterValue" + txtName
        txtFilterValue.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        txtFilterValue.SelectionLength = 0
        txtFilterValue.SelectionStart = 0
        txtFilterValue.Size = New System.Drawing.Size(100, 23)
        txtFilterValue.TabIndex = 0
        txtFilterValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
        txtFilterValue.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        txtFilterValue.AllowDrop = True
        txtFilterValue.Text = selectedValue
        txtFilterValue.Tag = tagValue
        txtFilterValue.Height = 23
        AddHandler txtFilterValue.DragDrop, AddressOf txtFilterValue_DragDrop
        AddHandler txtFilterValue.DragEnter, AddressOf txtFilterValue_DragEnter
        AddHandler txtFilterValue.TextChanged, AddressOf txtFilterValue_TextChanged
        Return txtFilterValue
    End Function

    Private Sub txtFilterValue_TextChanged(sender As Object, e As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim txtFilterValueTmp As DevExpress.XtraEditors.TextEdit = TryCast(sender, DevExpress.XtraEditors.TextEdit)
            Dim changedIndex As Integer = TryCast(txtFilterValueTmp.Parent, ExTableLayoutPanel).GetRow(txtFilterValueTmp)

            Dim alteredFilterParam As List(Of FilterParam) = GetFilteredParam()
            alteredFilterParam(changedIndex - 1).FilterValue = txtFilterValueTmp.Text.ToString

            If xtcReportFilter.SelectedTabPageIndex = 0 Then
                SetSQLQuery()
            ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
                SetSQLResult()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Function GetLogicalLinkCombo(ByVal cmbName As String, ByVal tagValue As String, ByVal selectedValue As String, Optional ByVal isFirstIndex As Boolean = False) As DevExpress.XtraEditors.ComboBoxEdit
        Dim cmbLogicalLink As DevExpress.XtraEditors.ComboBoxEdit = New DevExpress.XtraEditors.ComboBoxEdit()
        cmbLogicalLink.BackColor = System.Drawing.Color.White
        cmbLogicalLink.Dock = System.Windows.Forms.DockStyle.Top
        Dim ListItem18 As New clsComboBoxItem()
        Dim ListItem19 As New clsComboBoxItem()
        ListItem18.Text = "AND"
        ListItem18.Value = "AND"
        ListItem19.Text = "OR"
        ListItem19.Value = "OR"

        cmbLogicalLink.Properties.Items.Add(ListItem18)
        cmbLogicalLink.Properties.Items.Add(ListItem19)
        cmbLogicalLink.Name = "vCmbLogicalLink" & cmbName
        cmbLogicalLink.Size = New System.Drawing.Size(100, 23)
        cmbLogicalLink.TabIndex = 4
        cmbLogicalLink.Tag = tagValue
        cmbLogicalLink.Height = 23
        If (selectedValue.Length > 0) Then
            For Each it As clsComboBoxItem In cmbLogicalLink.Properties.Items
                If it.Text = selectedValue Then
                    cmbLogicalLink.SelectedItem = it
                    Exit For
                End If
            Next
        Else
            cmbLogicalLink.SelectedIndex = 0
        End If
        cmbLogicalLink.Enabled = isFirstIndex
        AddHandler cmbLogicalLink.SelectedValueChanged, AddressOf cmbLogicalLink_SelectedItemChanged
        Return cmbLogicalLink
    End Function

    Private Sub cmbLogicalLink_SelectedItemChanged(sender As Object, e As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim cmbLogicalLinkTmp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(sender, DevExpress.XtraEditors.ComboBoxEdit)
            Dim changedIndex As Integer = TryCast(cmbLogicalLinkTmp.Parent, ExTableLayoutPanel).GetRow(cmbLogicalLinkTmp)

            Dim alteredFilterParam As List(Of FilterParam) = GetFilteredParam()
            alteredFilterParam(changedIndex - 1).FilterLogicalLink = cmbLogicalLinkTmp.SelectedItem.ToString

            If xtcReportFilter.SelectedTabPageIndex = 0 Then
                SetSQLQuery()
            ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
                SetSQLResult()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Function GetDeleteButton(ByVal name As String) As DevExpress.XtraEditors.SimpleButton
        Dim btnFilterDelete As DevExpress.XtraEditors.SimpleButton = New DevExpress.XtraEditors.SimpleButton()
        btnFilterDelete.BackColor = System.Drawing.Color.Transparent
        btnFilterDelete.Name = "vBtnFilterDelete" & name
        btnFilterDelete.Size = New System.Drawing.Size(133, 23)
        btnFilterDelete.TabIndex = 0
        btnFilterDelete.Text = "Delete"
        btnFilterDelete.Dock = DockStyle.Top
        btnFilterDelete.Enabled = False
        btnFilterDelete.Height = 23
        AddHandler btnFilterDelete.Click, AddressOf btnFilterDelete_Click
        Return btnFilterDelete
    End Function

    Private Sub CreateEmptyControls(ByRef exTLP_ReportContentFilterContaints As IOS.Library.ExTableLayoutPanel, ByVal rowIndex As Integer)
        '' exTLP_ReportContentFilterContaints.RowStyles.Insert(rowIndex, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        '' exTLP_ReportContentFilterContaints.RowCount = exTLP_ReportContentFilterContaints.RowCount + 1

        'Dim vlblFilterDimension As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl()
        'vlblFilterDimension.BackColor = System.Drawing.Color.Transparent
        'vlblFilterDimension.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        'vlblFilterDimension.Ellipsis = False
        'vlblFilterDimension.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        'vlblFilterDimension.Multiline = True
        'vlblFilterDimension.Name = "vlblDimensionFilter"
        'vlblFilterDimension.Text = "< Drag Here >"
        'vlblFilterDimension.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        'vlblFilterDimension.UseMnemonics = True
        'vlblFilterDimension.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        'vlblFilterDimension.Dock = System.Windows.Forms.DockStyle.Fill
        'vlblFilterDimension.ForeColor = Color.Red
        'vlblFilterDimension.AllowDrop = True
        'AddHandler vlblFilterDimension.DragDrop, AddressOf vlblFilterDimension_DragDrop
        'AddHandler vlblFilterDimension.DragEnter, AddressOf vlblFilterDimension_DragEnter
        'exTLP_ReportContentFilterContaints.Controls.Add(vlblFilterDimension, 0, rowIndex)

        ''exTLP_ReportContentFilterContaints.Controls.Add(GetOperatorCombo("", "", ""), 1, rowIndex)

        ''exTLP_ReportContentFilterContaints.Controls.Add(GetValueTextBox("", "", "0"), 2, rowIndex)

        ''exTLP_ReportContentFilterContaints.Controls.Add(GetLogicalLinkCombo("", "", ""), 3, rowIndex)

        ''exTLP_ReportContentFilterContaints.Controls.Add(GetDeleteButton(""), 4, rowIndex)

        'exTLP_ReportContentFilterContaints.Refresh()
    End Sub

    Private Sub btnFilterDelete_Click(sender As Object, e As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim btnDeleteFilter As DevExpress.XtraEditors.SimpleButton = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
            If (btnDeleteFilter IsNot Nothing) Then
                btnDeleteFilter.Enabled = False

                If (FilterParamList.Count > 0) Then
                    Dim _filterParam As FilterParam = Nothing
                    For Each filterParam As FilterParam In FilterParamList
                        If filterParam.FilterDimension.ToString.ToUpper.Contains(btnDeleteFilter.Tag.ToString.ToUpper) Then
                            _filterParam = filterParam
                            Exit For
                        End If
                    Next
                    If (_filterParam IsNot Nothing) Then
                        FilterParamList.Remove(_filterParam)
                    End If
                End If

                If xtcReportFilter.SelectedTabPageIndex = 0 Then

                    tlp_ReportContentFilterQuery.SuspendLayout()
                    Dim lblFilterDimension As DevExpress.XtraEditors.LabelControl = TryCast(tlp_ReportContentFilterQuery.Controls("vlblDimension_" & btnDeleteFilter.Tag.ToString), DevExpress.XtraEditors.LabelControl)
                    If (lblFilterDimension IsNot Nothing) Then
                        Dim indexRow As Integer = tlp_ReportContentFilterQuery.GetRow(lblFilterDimension)
                        RemoveRow(tlp_ReportContentFilterQuery, indexRow)
                    End If
                    If (FilterParamList.Count > 0) Then
                        SetSQLQuery()
                    Else
                        SetSQLQuery()
                    End If
                    tlp_ReportContentFilterQuery.ResumeLayout()

                ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then

                    tlp_ReportContentFilterResult.SuspendLayout()
                    Dim lblFilterDimension As DevExpress.XtraEditors.LabelControl = TryCast(tlp_ReportContentFilterResult.Controls("vlblDimension_" & btnDeleteFilter.Tag.ToString), DevExpress.XtraEditors.LabelControl)
                    If (lblFilterDimension IsNot Nothing) Then
                        Dim indexRow As Integer = tlp_ReportContentFilterResult.GetRow(lblFilterDimension)
                        RemoveRow(tlp_ReportContentFilterResult, indexRow)
                    End If
                    If (FilterParamList.Count > 0) Then
                        SetSQLResult()
                    Else
                        SetSQLResult()
                    End If
                    tlp_ReportContentFilterResult.ResumeLayout()

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Public Sub RemoveRow(ByRef panel As TableLayoutPanel, ByRef rowIndex As Integer)
        panel.RowStyles.RemoveAt(rowIndex)
        Dim columnIndex As Integer
        For columnIndex = 0 To panel.ColumnCount - 1
            Dim Control As Control = panel.GetControlFromPosition(columnIndex, rowIndex)
            panel.Controls.Remove(Control)
        Next
        Dim i As Integer
        If panel.RowCount > 0 Then
            For i = rowIndex + 1 To panel.RowCount - 1
                columnIndex = 0
                For columnIndex = 0 To panel.ColumnCount - 1
                    Dim control As Control = panel.GetControlFromPosition(columnIndex, i)
                    panel.SetRow(control, i - 1)
                Next
            Next
            panel.RowCount -= 1
        End If
    End Sub

    Private Sub vlblFilterDimension_DragDrop(sender As Object, e As DragEventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim items As String = e.Data.GetData(DataFormats.Text).ToString
            Dim dragedObjectText As String = items.Split("#")(0).ToString

            Dim lblDiminstion As DevExpress.XtraEditors.LabelControl = TryCast(sender, DevExpress.XtraEditors.LabelControl)

            If (tlp_ReportContentFilterResult IsNot Nothing) Then

                Dim filterParam As FilterParam = New FilterParam
                Dim vlbl_Dimension As DevExpress.XtraEditors.LabelControl = Nothing
                Dim vCmb_Operator As DevExpress.XtraEditors.ComboBoxEdit
                Dim vDtpDateTimePicker As DevExpress.XtraEditors.DateEdit = Nothing
                Dim vTxt_Value As DevExpress.XtraEditors.TextEdit = Nothing
                Dim vCmb_LogicalLink As DevExpress.XtraEditors.ComboBoxEdit
                Dim inds As Integer = 0
                Dim vlblFilterDimension As DevExpress.XtraEditors.LabelControl = TryCast(tlp_ReportContentFilterResult.Controls("vlblDimensionFilter"), DevExpress.XtraEditors.LabelControl)
                If (vlblFilterDimension IsNot Nothing) Then
                    inds = tlp_ReportContentFilterResult.GetRow(vlblFilterDimension)
                    vlbl_Dimension = CreateLabel("_" & dragedObjectText, dragedObjectText, Color.Black)
                    filterParam.FilterDimension = dragedObjectText

                End If
                vCmb_Operator = GetOperatorCombo("_" & dragedObjectText, dragedObjectText, "=")
                filterParam.FilterOperator = "="

                If (dragedObjectText.ToUpper = "PERIOD_START_TIME") Then

                    vDtpDateTimePicker = GetDatePicker("_" & dragedObjectText, dragedObjectText, Date.Now)
                    filterParam.FilterValue = Date.Now
                Else
                    vTxt_Value = GetValueTextBox("_" & dragedObjectText, dragedObjectText, "0")
                    filterParam.FilterValue = "0"
                End If
                vCmb_LogicalLink = GetLogicalLinkCombo("_" & dragedObjectText, dragedObjectText, "AND", False)
                filterParam.FilterLogicalLink = "AND"

                Dim btnFilterDelete As DevExpress.XtraEditors.SimpleButton = New DevExpress.XtraEditors.SimpleButton()
                btnFilterDelete.BackColor = System.Drawing.Color.Transparent
                btnFilterDelete.Location = New System.Drawing.Point(3, 3)
                btnFilterDelete.Name = "vbtnFilterDelete_" & dragedObjectText
                btnFilterDelete.Size = New System.Drawing.Size(133, 23)
                btnFilterDelete.TabIndex = 0
                btnFilterDelete.Text = "Delete"
                btnFilterDelete.Dock = DockStyle.Top
                btnFilterDelete.Enabled = True
                btnFilterDelete.Tag = dragedObjectText
                AddHandler btnFilterDelete.Click, AddressOf btnFilterDelete_Click

                For Each controlTmp As Control In tlp_ReportContentFilterResult.Controls
                    If (controlTmp.Name.Contains("vCmbLogicalLink_")) Then
                        Dim vCmbLogicalLinkTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                        If (vCmbLogicalLinkTemp IsNot Nothing) Then
                            vCmbLogicalLinkTemp.Enabled = True
                        End If
                    End If
                Next

                vCmb_LogicalLink.Enabled = False
                Dim rowIndexNew As Integer = tlp_ReportContentFilterResult.RowStyles.Count

                tlp_ReportContentFilterResult.RowStyles.Insert(rowIndexNew, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
                RefrashFilterContents()

                tlp_ReportContentFilterResult.SetCellPosition(vlblFilterDimension, New System.Windows.Forms.TableLayoutPanelCellPosition(0, inds + 1))

                tlp_ReportContentFilterResult.Controls.Add(vlbl_Dimension, 0, rowIndexNew)
                tlp_ReportContentFilterResult.Controls.Add(vCmb_Operator, 1, rowIndexNew)
                If (dragedObjectText.ToUpper = "PERIOD_START_TIME") Then
                    tlp_ReportContentFilterResult.Controls.Add(vDtpDateTimePicker, 2, rowIndexNew)
                Else
                    tlp_ReportContentFilterResult.Controls.Add(vTxt_Value, 2, rowIndexNew)
                End If

                tlp_ReportContentFilterResult.Controls.Add(vCmb_LogicalLink, 3, rowIndexNew)
                tlp_ReportContentFilterResult.Controls.Add(btnFilterDelete, 4, rowIndexNew)

                For Each filterParamTem As FilterParam In FilterParamList
                    If (filterParamTem.FilterLogicalLink = "") Then
                        filterParamTem.FilterLogicalLink = "AND"
                    End If
                Next
                FilterParamList.Add(filterParam)
                tlp_ReportContentFilterResult.ResumeLayout()
            End If
            SetSQLResult()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vlblFilterDimension_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub Label_DragEnterEvent(sender As Object, e As DragEventArgs) Handles lbl_ResultDrag.DragEnter, lbl_QueryDrag.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub lbl_ResultDrag_DragDrop(sender As Object, e As DragEventArgs) Handles lbl_ResultDrag.DragDrop
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim items As String = e.Data.GetData(DataFormats.Text).ToString
            Dim draggedObjectText As String = items.Split("#")(0).ToString
            Dim draggedObjectTag As String = items.Split("#")(1).ToString
            Dim draggedObjectFieldType As Integer = items.Split("#")(2).ToString

            If (tlp_ReportContentFilterResult IsNot Nothing) Then
                If tlp_ReportContentFilterResult.Controls.Count > 0 Then
                    For iCnt As Integer = 0 To tlp_ReportContentFilterResult.Controls.Count - 1 Step 5
                        If tlp_ReportContentFilterResult.Controls(iCnt).Text = draggedObjectText Then
                            SetMessage("Fail: Filter already added!")
                            Exit Sub
                        End If
                    Next
                End If

                Dim filterParam As FilterParam = New FilterParam()
                Dim lblDimension As DevExpress.XtraEditors.LabelControl = Nothing
                Dim cmbOperator As DevExpress.XtraEditors.ComboBoxEdit = Nothing
                Dim cmbFieldValue As DevExpress.XtraEditors.ComboBoxEdit = Nothing
                Dim dtpDateTimePicker As DevExpress.XtraEditors.DateEdit = Nothing
                Dim txtValue As DevExpress.XtraEditors.TextEdit = Nothing
                Dim cmbLogicalLink As DevExpress.XtraEditors.ComboBoxEdit = Nothing

                Dim inds As Integer = 0
                lblDimension = CreateLabel("_" & draggedObjectText, draggedObjectText, Color.Black)
                If draggedObjectTag <> "" Then
                    filterParam.FilterDimension = draggedObjectTag & "." & draggedObjectText
                Else
                    filterParam.FilterDimension = draggedObjectText
                End If

                cmbOperator = GetOperatorCombo("_" & draggedObjectText, draggedObjectText, "=")
                filterParam.FilterOperator = "="

                If (draggedObjectText.ToUpper = "PERIOD_START_TIME") Then
                    dtpDateTimePicker = GetDatePicker("_" & draggedObjectText, draggedObjectText, Date.Now)
                    filterParam.FilterValue = Date.Now
                ElseIf draggedObjectFieldType = DatamartFieldType.ObjectFld Then
                    cmbFieldValue = GetFieldValueCombo("_" & draggedObjectText, draggedObjectText)
                    filterParam.FilterValue = cmbFieldValue.SelectedItem.ToString
                Else
                    txtValue = GetValueTextBox("_" & draggedObjectText, draggedObjectText, "0")
                    filterParam.FilterValue = "0"
                End If

                cmbLogicalLink = GetLogicalLinkCombo("_" & draggedObjectText, draggedObjectText, "AND", False)
                filterParam.FilterLogicalLink = ""
                Dim btnFilterDelete As DevExpress.XtraEditors.SimpleButton = New DevExpress.XtraEditors.SimpleButton()
                btnFilterDelete.BackColor = System.Drawing.Color.Transparent
                btnFilterDelete.Location = New System.Drawing.Point(3, 3)
                btnFilterDelete.Name = "vbtnFilterDelete_" & draggedObjectText
                btnFilterDelete.Size = New System.Drawing.Size(133, 23)
                btnFilterDelete.TabIndex = 0
                btnFilterDelete.Text = "Delete"
                btnFilterDelete.Dock = DockStyle.Top
                btnFilterDelete.Enabled = True
                btnFilterDelete.Tag = draggedObjectText
                AddHandler btnFilterDelete.Click, AddressOf btnFilterDelete_Click

                For Each controlTmp As Control In tlp_ReportContentFilterResult.Controls
                    If (controlTmp.Name.Contains("vCmbLogicalLink_")) Then
                        Dim cmbLogicalLinkTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                        If (cmbLogicalLinkTemp IsNot Nothing) Then
                            cmbLogicalLinkTemp.Enabled = True
                        End If
                    End If
                Next

                cmbLogicalLink.Enabled = False
                Dim rowIndexNew As Integer = tlp_ReportContentFilterResult.RowStyles.Count
                tlp_ReportContentFilterResult.RowStyles.Insert(rowIndexNew, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
                If (exTPL_DragdropAndFiltersResult.RowStyles(0).Height + 40.0! < exTPL_DragdropAndFiltersResult.Height) Then
                    exTPL_DragdropAndFiltersResult.RowStyles(0).Height = tlp_ReportContentFilterResult.RowStyles.Count * 25.0!
                End If

                tlp_ReportContentFilterResult.Controls.Add(lblDimension, 0, rowIndexNew)
                tlp_ReportContentFilterResult.Controls.Add(cmbOperator, 1, rowIndexNew)
                If (draggedObjectText.ToUpper = "PERIOD_START_TIME") Then
                    tlp_ReportContentFilterResult.Controls.Add(dtpDateTimePicker, 2, rowIndexNew)
                Else
                    tlp_ReportContentFilterResult.Controls.Add(txtValue, 2, rowIndexNew)
                End If

                tlp_ReportContentFilterResult.Controls.Add(cmbLogicalLink, 3, rowIndexNew)
                tlp_ReportContentFilterResult.Controls.Add(btnFilterDelete, 4, rowIndexNew)
                filterParam.FilterType = "RESULT"
                For Each filterParamTem As FilterParam In FilterParamList
                    If (filterParamTem.FilterLogicalLink = "") Then
                        filterParamTem.FilterLogicalLink = "AND"
                    End If
                Next
                FilterParamList.Add(filterParam)
                tlp_ReportContentFilterResult.ResumeLayout()
            End If
            SetSQLResult()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub lbl_QueryDrag_DragDrop(sender As Object, e As DragEventArgs) Handles lbl_QueryDrag.DragDrop
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim items As String = e.Data.GetData(DataFormats.Text).ToString
            Dim draggedObjectText As String = items.Split("#")(0).ToString
            Dim draggedObjectTag As String = items.Split("#")(1).ToString
            Dim draggedObjectFieldType As Integer = items.Split("#")(2).ToString

            If (tlp_ReportContentFilterQuery IsNot Nothing) Then
                ' Allowing one dimension to be added multiple times in order to have multiple conditions on the same dimension

                'If exTLP_ReportContentFilterContaintsQuery.Controls.Count > 0 Then
                '    For iCnt As Integer = 0 To exTLP_ReportContentFilterContaintsQuery.Controls.Count - 1 Step 5
                '        If exTLP_ReportContentFilterContaintsQuery.Controls(iCnt).Text = draggedObjectText Then
                '            SetMessage("Fail: Filter already added!")
                '            Exit Sub
                '        End If
                '    Next
                'End If

                Dim filterParam As FilterParam = New FilterParam()
                Dim lblDimension As DevExpress.XtraEditors.LabelControl = Nothing
                Dim cmbOperator As DevExpress.XtraEditors.ComboBoxEdit = Nothing
                Dim cmbFieldValue As DevExpress.XtraEditors.ComboBoxEdit = Nothing
                Dim dtpDateTimePicker As DevExpress.XtraEditors.DateEdit = Nothing
                Dim txtValue As DevExpress.XtraEditors.TextEdit = Nothing
                Dim cmbLogicalLink As DevExpress.XtraEditors.ComboBoxEdit = Nothing
                Dim inds As Integer = 0

                lblDimension = CreateLabel("_" & draggedObjectText, draggedObjectText, Color.Black)
                If draggedObjectTag <> "" Then
                    filterParam.FilterDimension = draggedObjectTag & "." & draggedObjectText
                Else
                    filterParam.FilterDimension = draggedObjectText
                End If

                cmbOperator = GetOperatorCombo("_" & draggedObjectText, draggedObjectText, "=")
                filterParam.FilterOperator = "="

                If (draggedObjectText.ToUpper = "PERIOD_START_TIME") Then
                    dtpDateTimePicker = GetDatePicker("_" & draggedObjectText, draggedObjectText, Date.Now)
                    filterParam.FilterValue = Date.Now
                ElseIf draggedObjectFieldType = DatamartFieldType.ObjectFld Then
                    cmbFieldValue = GetFieldValueCombo("_" & draggedObjectText, draggedObjectText)
                    filterParam.FilterValue = cmbFieldValue.SelectedItem.ToString
                Else
                    txtValue = GetValueTextBox("_" & draggedObjectText, draggedObjectText, "0")
                    filterParam.FilterValue = "0"
                End If

                cmbLogicalLink = GetLogicalLinkCombo("_" & draggedObjectText, draggedObjectText, "AND", False)
                filterParam.FilterLogicalLink = ""
                filterParam.ObjectFieldType = draggedObjectFieldType
                Dim btnFilterDelete As DevExpress.XtraEditors.SimpleButton = New DevExpress.XtraEditors.SimpleButton()
                btnFilterDelete.BackColor = System.Drawing.Color.Transparent
                btnFilterDelete.Location = New System.Drawing.Point(3, 3)
                btnFilterDelete.Name = "vbtnFilterDelete_" & draggedObjectText
                btnFilterDelete.Size = New System.Drawing.Size(133, 23)
                btnFilterDelete.TabIndex = 0
                btnFilterDelete.Text = "Delete"
                btnFilterDelete.Dock = DockStyle.Top
                btnFilterDelete.Enabled = True
                btnFilterDelete.Tag = draggedObjectText
                AddHandler btnFilterDelete.Click, AddressOf btnFilterDelete_Click

                For Each controlTmp As Control In tlp_ReportContentFilterQuery.Controls
                    If (controlTmp.Name.Contains("vCmbLogicalLink_")) Then
                        Dim vCmbLogicalLinkTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                        If (vCmbLogicalLinkTemp IsNot Nothing) Then
                            vCmbLogicalLinkTemp.Enabled = True
                        End If
                    End If
                Next

                cmbLogicalLink.Enabled = False
                Dim rowIndexNew As Integer = tlp_ReportContentFilterQuery.RowStyles.Count
                tlp_ReportContentFilterQuery.RowStyles.Insert(rowIndexNew, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
                If (exTPL_DragdropAndFiltersQuery.RowStyles(0).Height + 40.0! < exTPL_DragdropAndFiltersQuery.Height) Then
                    exTPL_DragdropAndFiltersQuery.RowStyles(0).Height = tlp_ReportContentFilterQuery.RowStyles.Count * 25.0!
                End If

                tlp_ReportContentFilterQuery.Controls.Add(lblDimension, 0, rowIndexNew)
                tlp_ReportContentFilterQuery.Controls.Add(cmbOperator, 1, rowIndexNew)
                If (draggedObjectText.ToUpper = "PERIOD_START_TIME") Then
                    tlp_ReportContentFilterQuery.Controls.Add(dtpDateTimePicker, 2, rowIndexNew)
                Else
                    If cmbFieldValue Is Nothing Then
                        tlp_ReportContentFilterQuery.Controls.Add(txtValue, 2, rowIndexNew)
                    Else
                        tlp_ReportContentFilterQuery.Controls.Add(cmbFieldValue, 2, rowIndexNew)
                    End If
                End If

                tlp_ReportContentFilterQuery.Controls.Add(cmbLogicalLink, 3, rowIndexNew)
                tlp_ReportContentFilterQuery.Controls.Add(btnFilterDelete, 4, rowIndexNew)

                filterParam.FilterType = "QUERY"

                For Each filterParamTem As FilterParam In FilterParamList
                    If (filterParamTem.FilterLogicalLink = "") Then
                        filterParamTem.FilterLogicalLink = "AND"
                    End If
                Next
                FilterParamList.Add(filterParam)
                tlp_ReportContentFilterQuery.ResumeLayout()
            End If
            SetSQLQuery()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub SetSQLResult()
        RichTextBoxResult.Text = ""
        For Each controlTmp As Control In tlp_ReportContentFilterResult.Controls
            If (controlTmp.Name.Contains("vlblDimension_")) Then
                Dim lblFilterDimensionTmp As DevExpress.XtraEditors.LabelControl = TryCast(controlTmp, DevExpress.XtraEditors.LabelControl)
                If (lblFilterDimensionTmp IsNot Nothing) Then
                    AppendText(RichTextBoxResult, " " & lblFilterDimensionTmp.Text, Color.Black, False)
                End If
            ElseIf (controlTmp.Name.Contains("vCmbFilterOperator_")) Then
                Dim cmbFilterOperatorTmp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                If (cmbFilterOperatorTmp IsNot Nothing) Then
                    AppendText(RichTextBoxResult, " " & cmbFilterOperatorTmp.SelectedItem.Text, Color.OrangeRed, False)
                End If
            ElseIf (controlTmp.Name.Contains("vDtpFilterValue_")) Then
                Dim dtpFilterValueTmp As DevExpress.XtraEditors.DateEdit = TryCast(controlTmp, DevExpress.XtraEditors.DateEdit)
                If (dtpFilterValueTmp IsNot Nothing) Then
                    AppendText(RichTextBoxResult, " '" & dtpFilterValueTmp.EditValue & "' ", Color.Black, False)
                End If
            ElseIf (controlTmp.Name.Contains("vTxtFilterValue_")) Then
                Dim txtFilterValueTmp As DevExpress.XtraEditors.TextEdit = TryCast(controlTmp, DevExpress.XtraEditors.TextEdit)
                If (txtFilterValueTmp IsNot Nothing) Then
                    AppendText(RichTextBoxResult, " '" & txtFilterValueTmp.Text & "' ", Color.Black, False)
                End If
            ElseIf (controlTmp.Name.Contains("vCmbLogicalLink_")) Then
                Dim cmbLogicalLinkTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                If (cmbLogicalLinkTemp IsNot Nothing) Then
                    If (cmbLogicalLinkTemp.Enabled) Then
                        AppendText(RichTextBoxResult, " " & cmbLogicalLinkTemp.SelectedItem.Text, Color.Red, False)
                    End If
                End If
            ElseIf (controlTmp.Name.Contains("vCmbFilterValue_")) Then
                Dim cmbFldValTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                If (cmbFldValTemp IsNot Nothing) Then
                    If (cmbFldValTemp.Enabled) Then
                        AppendText(RichTextBoxResult, " " & cmbFldValTemp.SelectedItem.Text, Color.Red, False)
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub SetSQLQuery()
        RichTextBoxQuery.Text = ""
        For Each controlTmp As Control In tlp_ReportContentFilterQuery.Controls
            If (controlTmp.Name.Contains("vlblDimension_")) Then
                Dim lblFilterDimensionTmp As DevExpress.XtraEditors.LabelControl = TryCast(controlTmp, DevExpress.XtraEditors.LabelControl)
                If (lblFilterDimensionTmp IsNot Nothing) Then
                    AppendText(RichTextBoxQuery, " " & lblFilterDimensionTmp.Text, Color.Black, False)
                End If
            ElseIf (controlTmp.Name.Contains("vCmbFilterOperator_")) Then
                Dim cmbFilterOperatorTmp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                If (cmbFilterOperatorTmp IsNot Nothing) Then
                    AppendText(RichTextBoxQuery, " " & cmbFilterOperatorTmp.SelectedItem.Text, Color.OrangeRed, False)
                End If
            ElseIf (controlTmp.Name.Contains("vDtpFilterValue_")) Then
                Dim dtpFilterValueTmp As DevExpress.XtraEditors.DateEdit = TryCast(controlTmp, DevExpress.XtraEditors.DateEdit)
                If (dtpFilterValueTmp IsNot Nothing) Then
                    AppendText(RichTextBoxQuery, " '" & dtpFilterValueTmp.EditValue & "' ", Color.Black, False)
                End If
            ElseIf (controlTmp.Name.Contains("vTxtFilterValue_")) Then
                Dim txtFilterValueTmp As DevExpress.XtraEditors.TextEdit = TryCast(controlTmp, DevExpress.XtraEditors.TextEdit)
                If (txtFilterValueTmp IsNot Nothing) Then
                    AppendText(RichTextBoxQuery, " '" & txtFilterValueTmp.Text.Trim & "' ", Color.Black, False)
                End If
            ElseIf (controlTmp.Name.Contains("vCmbLogicalLink_")) Then
                Dim cmbLogicalLinkTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                If (cmbLogicalLinkTemp IsNot Nothing) Then
                    If (cmbLogicalLinkTemp.Enabled) Then
                        AppendText(RichTextBoxQuery, " " & cmbLogicalLinkTemp.SelectedItem.Text, Color.Red, False)
                    End If
                End If
            ElseIf (controlTmp.Name.Contains("vCmbFilterValue_")) Then
                Dim cmbFldValTemp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(controlTmp, DevExpress.XtraEditors.ComboBoxEdit)
                If (cmbFldValTemp IsNot Nothing) Then
                    If (cmbFldValTemp.Enabled) Then
                        AppendText(RichTextBoxQuery, " " & Chr(39) & cmbFldValTemp.SelectedItem.Text & Chr(39), Color.Red, False)
                    End If
                End If
            End If
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

    Private Sub txtFilterValue_DragDrop(sender As Object, e As DragEventArgs)

        Dim items As String = e.Data.GetData(DataFormats.Text).ToString
        Dim dragedObjectText As String = items.Split("#")(0).ToString
        Dim vTxtFilterValue As DevExpress.XtraEditors.TextEdit = TryCast(sender, DevExpress.XtraEditors.TextEdit)
        vTxtFilterValue.Text = dragedObjectText.Trim

    End Sub

    Private Sub txtFilterValue_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
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

    Private Sub ClearData()
        'vtxt_ReportGroupName.Text = ""
        'vrb_Private.Checked = False
        'vrb_Public.Checked = False
    End Sub

    Private Function GetFieldValueCombo(cmbName As String, tagValue As String, Optional selectedVal As String = Nothing) As DevExpress.XtraEditors.ComboBoxEdit
        Dim cmbFilterValue As New DevExpress.XtraEditors.ComboBoxEdit()
        cmbFilterValue.BackColor = System.Drawing.Color.White
        cmbFilterValue.Dock = System.Windows.Forms.DockStyle.Top

        If objectTableName IsNot Nothing Then
            dtObjectTypeValues = DataAccessorODBC.GetDataTable(connStrSandBoxServer, SQLReportContentFilter.GetReportDimensionDistinctValues(tagValue, objectTableName))
            'Dim dtFldVal As DataTable = dtObjectTypeValues.DistinctCol(tagValue).AsEnumerable().OrderBy(Function(x) x.Item(tagValue)).CopyToDataTable
            BindDevExComboBoxWithValueMember(cmbFilterValue, dtObjectTypeValues, tagValue, tagValue)

            cmbFilterValue.Name = "vCmbFilterValue" & cmbName
            cmbFilterValue.Size = New System.Drawing.Size(100, 23)
            cmbFilterValue.Tag = tagValue
            cmbFilterValue.Height = 23
            cmbFilterValue.SelectedIndex = 0
            If selectedVal IsNot Nothing Then
                SetComboBox(cmbFilterValue, ComboSelectBased.TextBased, selectedVal)
            End If

            AddHandler cmbFilterValue.SelectedValueChanged, AddressOf cmbFilterValue_SelectedItemChanged
            Return cmbFilterValue
        End If
        SetMessage("Dimension source data is unavailable")
        Return Nothing
    End Function

    Private Sub cmbFilterValue_SelectedItemChanged(sender As Object, e As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim cmbFilterValueTmp As DevExpress.XtraEditors.ComboBoxEdit = TryCast(sender, DevExpress.XtraEditors.ComboBoxEdit)
            Dim changedIndex As Integer = TryCast(cmbFilterValueTmp.Parent, ExTableLayoutPanel).GetRow(cmbFilterValueTmp)

            Dim alteredFilterParam As List(Of FilterParam) = GetFilteredParam()
            alteredFilterParam(changedIndex - 1).FilterValue = cmbFilterValueTmp.SelectedItem.ToString

            If xtcReportFilter.SelectedTabPageIndex = 0 Then
                SetSQLQuery()
            ElseIf xtcReportFilter.SelectedTabPageIndex = 1 Then
                SetSQLResult()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

End Class

Public Class FilterParam
    Public FilterDimension As String
    Public FilterOperator As String
    Public FilterValue As String
    Public FilterLogicalLink As String
    Public FilterType As String
    Public ObjectFieldType As String
End Class