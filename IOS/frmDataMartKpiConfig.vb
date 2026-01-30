Imports IOS.Library
Imports IOS.DataLibrary
Imports IOS.Configuration
Imports DevExpress.XtraEditors

Public Class frmDatamartKpiConfig

#Region "Variables"

    Private pt As Point = Point.Empty
    Private strDenominator As String = "()"
    Private KpiIDToModify As String = String.Empty

    Public dragDropType As DragDropType = DragDropType.NoDragDrop
    Public kpiNameToModify As String = Nothing
    Public kpiConfigObjectType As String = Nothing
    Public isModifyKpiRequest As Boolean = False
    Public list_of_used_tables As New List(Of String)
    Public kpiGroupID As Integer = Nothing

#End Region

#Region "Methods"

    Public Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Function GetSourceTableIdByCounterId(ByVal counterId As String) As String
        Try
            If (Not String.IsNullOrEmpty(counterId)) Then
                Dim dtTechPackCounterMeasurment As DataTable = objSandbox.dt_TechPackCounter.SelectedRowsAsTable(TechnologyPackageCountersFields.COUNTER_ID & OperatorConst.Equal & counterId)
                If (dtTechPackCounterMeasurment.IsValid) Then
                    Return dtTechPackCounterMeasurment.Rows(0)(TechnologyPackageCountersFields.SQL_SOURCE_TABLE).ToString
                End If
            End If
        Catch EX As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & EX.Message)
        End Try
        Return Nothing
    End Function

    Private Function GetMatchingIndexCollection(ByVal str As String, ByVal pattern As String) As List(Of Integer)
        Dim list As New List(Of Integer)
        For index As Integer = 0 To str.Length - 1
            If (str(index) = pattern) Then
                list.Add(index)
            End If
        Next
        Return list
    End Function

    Private Function TestKPI() As Boolean
        Dim testStr As String = ""
        Dim kpiName As String = txtKPIName.Text.Trim()
        Dim kpiFarmula As String = txtKPIFormula.Text.Trim()

        Dim tableNames As String = String.Join(",", list_of_used_tables.ToArray) '' GetUsingAllTableNames(tableKey, connectionName, dataBaseName, tableAlias, "ByKPITest", JoinObject, megaQuery)
        Dim selectCMD As String = SQLTechnologyMeasurements.GetPrimaryKey(String.Join(",", list_of_used_tables), "")
        Dim measurementPrimaryKeyDt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)
        Dim stFirst As String = measurementPrimaryKeyDt(0)(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
        Dim pkFirst As String = measurementPrimaryKeyDt(0)(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
        Dim pkfields As String = stFirst + "." + Replace(pkFirst, ",", ", " + stFirst + ".")
        Try
            If (IsKPIFormulaValid(kpiFarmula)) Then
                If (tableNames.Trim.Length > 1) Then
                    Dim tableCount As Integer = tableNames.Split(",").Count
                    Dim kpiValue As Integer = 0
                    If (tableCount > 1) Then
                        'testStr = GetSourceTable()
                        testStr = "SELECT TOP 1 " + pkfields + ", ISNULL(" & kpiFarmula & ",0) [" & kpiName & "] FROM " & tableNames.Split(",")(0) & " " & GetSourceTable() & " GROUP BY " + pkfields
                    Else
                        testStr = "SELECT TOP 1 " + pkfields + ", ISNULL(" & kpiFarmula & ",0) [" & kpiName & "] FROM " & tableNames & " GROUP BY " + pkfields
                    End If

                    Dim counterFilters As String = TechnologyPackageCountersFields.SQL_SOURCE_TABLE & OperatorConst.Equal & tableNames.Split(",")(0)
                    Dim dtSourceTableCon As DataTable = objSandbox.dt_TechPackCounter.Select("SQL_SourceTable='" + tableNames.Split(",")(0) + "'").CopyToDataTable()

                    If (dtSourceTableCon.IsValid) Then
                        Dim result As DataTable = DataAccessorODBC.GetDataTable(dtSourceTableCon.Rows(0)(TechnologyPackageCountersFields.SQL_CONNSTRING).ToString, testStr)
                        If (result IsNot Nothing AndAlso result.Rows.Count > 0) Then
                            lblKPIConfigStatus.Text = "KPI OK"
                            Return True
                        ElseIf (result Is Nothing) Then
                            lblKPIConfigStatus.Text = "KPI OK"
                            Return False
                        Else
                            lblKPIConfigStatus.Text = "KPI OK"
                            Return True
                        End If
                    Else
                        SetMessage("Test Connection Not found.")
                        Return False
                    End If
                Else
                    SetMessage("Table is not in Table Grid so Not able to find connection string.")
                    Return False
                End If
            End If
            Return Nothing
        Catch ex As Exception
            lblKPIConfigStatus.Text = "KPI Not OK"
            SetMessage("There is some problem with query. Error: " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "ERROR", testStr & " " & ex.Message)
        End Try
        Return Nothing
    End Function

    Private Function IsKPIFormulaValid(ByVal kpiFormula As String) As Boolean
        Dim kpiTestStr As String = kpiFormula
        Dim bracesCounter As Integer = kpiFormula.Length - kpiFormula.Replace("(", "").Length
        Dim isMatchAggregate As Boolean = False
        kpiFormula = Replace(kpiFormula.ToLower, "round", "")
        If (kpiFormula.IndexOf("(") > 0) Then

            For i As Integer = 0 To lstAggregateFunction.ItemCount - 1
                If kpiFormula.ToUpper.Contains(lstAggregateFunction.Items.Item(i).ToString.ToUpper().Replace("()", "(")) Then
                    isMatchAggregate = True
                    Exit For
                End If
            Next

            If (isMatchAggregate) Then
                If Not (kpiFormula.Length - kpiFormula.Replace("(", "").Length = kpiFormula.Length - kpiFormula.Replace(")", "").Length) Then
                    SetMessage("Query does not have matching brackets.")
                    Return False
                Else
                    Return True
                End If
            Else
                SetMessage("Query does not seem to start with an aggregate function")
                Return False
            End If
        Else

            For i As Integer = 0 To lstAggregateFunction.ItemCount - 1
                If kpiFormula.ToUpper.Contains(Replace(lstAggregateFunction.Items.Item(i).ToString.ToUpper, "()", "")) Then
                    isMatchAggregate = True
                    Exit For
                End If
            Next

            If (isMatchAggregate) Then
                Return True
            Else
                SetMessage("Query does not seem to start with an aggregate function")
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ValidateControlsForKPIConfig() As Boolean
        If (objSandbox.cmbObjectType.SelectedIndex > 0) Then
            If (Not txtKPIName.Text.Trim.Length = 0) Then
                If Not (txtKPIFormula.Text = "") Then
                    Return True
                Else
                    SetMessage("Enter Any Formula.")
                    Return False
                End If
            Else
                SetMessage("Enter any KPI Name.")
                Return False
            End If
        Else
            SetMessage("Select Object Name.")
            Return False
        End If
    End Function

    Private Function GetSourceTable() As String
        Dim from_fieldTemp As String = ""
        Dim stList() As String = list_of_used_tables.ToArray()
        Dim indexST As Integer = 0
        Dim stFirst As String = ""
        Dim pkFirst As String = ""

        Dim selectCMD As String = SQLTechnologyMeasurements.GetPrimaryKey(String.Join(",", list_of_used_tables), "")
        Dim measurementPrimaryKeyDt As DataTable = DataAccessorODBC.GetDataTable(connStrSandBoxServer, selectCMD)
        Dim isFirstTime As Boolean = True
        If (measurementPrimaryKeyDt.IsValid) Then

            Dim stCurrent As String = ""
            Dim pkCurrent As String = ""

            from_fieldTemp = String.Empty
            For Each stDR As DataRow In measurementPrimaryKeyDt.Rows
                If (isFirstTime) Then
                    stFirst = stDR(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
                    pkFirst = stDR(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
                    ''from_fieldTemp = stFirst
                Else
                    stCurrent = stDR(TechnologyMeasurementsFields.SQL_SOURCE_TABLE).ToString
                    pkCurrent = stDR(TechnologyMeasurementsFields.PRIMARY_KEY).ToString
                End If
                Dim pkCounter As Integer = pkFirst.Split(",").Count '' If PrimaryKey has more then one 
                If (Not isFirstTime) Then
                    If (pkCounter = 1) Then
                        from_fieldTemp = " INNER JOIN " & stCurrent & " ON " & stFirst & "." & pkFirst & " = " & stCurrent & "." & pkCurrent
                    Else
                        from_fieldTemp = from_fieldTemp & " INNER JOIN " & stCurrent & " ON " & stFirst & "." & pkFirst.Split(",")(0).ToString & " = " & stCurrent & "." & pkCurrent.Split(",")(0).ToString
                    End If
                    For index = 1 To pkCurrent.Split(",").Count - 1
                        If (index > pkCounter) Then
                            from_fieldTemp = from_fieldTemp & " AND " & stFirst & "." & pkFirst.Split(",")(pkCounter).ToString & " = " & stCurrent & "." & pkCurrent.Split(",")(index).ToString
                        Else
                            from_fieldTemp = from_fieldTemp & " AND " & stFirst & "." & pkFirst.Split(",")(index).ToString.Trim & " = " & stCurrent & "." & pkCurrent.Split(",")(index).ToString.Trim
                        End If
                    Next
                End If
                isFirstTime = False
            Next
        End If
        Return from_fieldTemp
    End Function

    Public Sub GetKPIFormulaAndDescription(ByVal kpiID As String)
        Try
            txtKPIFormula.Text = String.Empty
            txtKPIDescription.Text = String.Empty
            Dim counterFilters As String = TechnologyPackageKPIFields.KPI_ID & OperatorConst.Equal & kpiID

            Dim dtTechnologyPackageKPI As DataTable = objSandbox.dt_TechnologyPackageKPI.SelectedRowsAsTable(counterFilters)
            If (dtTechnologyPackageKPI.IsValid) Then
                KpiIDToModify = dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_ID)
                txtKPIFormula.Text = dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_SQL)
                txtKPIDescription.Text = dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_DESCRIPTION)
                lblKPICreator.Text = IIf(IsDBNull(dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_CREATOR)), "", dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.KPI_CREATOR))
                If (dtTechnologyPackageKPI.Rows(0)(TechnologyPackageKPIFields.IS_PRIVATE) = True) Then
                    rbKPIConfigPrivate.Checked = True
                    rbKPIConfigPublic.Checked = False
                Else
                    rbKPIConfigPrivate.Checked = False
                    rbKPIConfigPublic.Checked = True
                End If
                RefreshUsedTableInKPI()
            Else
                ''SetMessage("Sorry ! No KPI Formula and Description.")
            End If

            If Me.isModifyKpiRequest = False Then
                btnTestKPI.Enabled = True
                btnCommitKPI.Enabled = False
            Else
                If (lblKPICreator.Text.ToUpper = Environment.UserName.ToUpper) Then
                    btnTestKPI.Enabled = True
                    btnCommitKPI.Enabled = True
                Else
                    btnTestKPI.Enabled = True
                    btnCommitKPI.Enabled = True
                End If
            End If

            '    End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub RefreshUsedTableInKPI()
        list_of_used_tables.Clear()
        If (txtKPIFormula.Text.Trim().Length >= 0) Then
            Dim usingTables As String() = txtKPIFormula.Text.Split(".")
            If (usingTables.Count > 1) Then
                For Each sTable As String In usingTables
                    If (sTable(sTable.Length - 1) = "]") Then
                        Dim startIndex As Integer = sTable.LastIndexOf("[")
                        Dim endIndex As Integer = sTable.LastIndexOf("]")
                        Dim strTable As String = sTable.Substring(startIndex, endIndex - startIndex + 1)
                        If Not list_of_used_tables.Contains(strTable) Then
                            list_of_used_tables.Add(strTable)
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub ConfigureIOSKPIConfig(ByVal frmName As String)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
            If Not form Is Nothing Then
                Dim counter As Integer = 0
                ConfigurForm(Me, frmName, counter)

                Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
                Dim formControls As List(Of Object) = New List(Of Object) From {
                     rbKPIConfigPrivate, rbKPIConfigPublic
                }

                For Each frmControl As Object In formControls
                    winCtrl = form.FindControlByName(frmControl.Name)
                    If Not winCtrl Is Nothing Then
                        frmControl.Enabled = winCtrl.DefaultEnable
                        frmControl.Visible = winCtrl.DefaultVisible
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Events"

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub txtKPIFormula_DragOver(sender As Object, e As DragEventArgs) Handles txtKPIFormula.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub lstAggregateFunction_MouseMove(sender As Object, e As MouseEventArgs) Handles lstAggregateFunction.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                If (pt <> Point.Empty) Then
                    Dim listControl As ListBoxControl = TryCast(sender, ListBoxControl)
                    If (listControl IsNot Nothing) Then
                        Dim index As Integer = listControl.IndexFromPoint(pt)
                        If (index > -1) Then
                            Me.dragDropType = DragDropType.ByAggregrate
                            listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
                        End If
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub txtKPIFormula_DragDrop(sender As Object, e As DragEventArgs) Handles txtKPIFormula.DragDrop
        Dim text As String = e.Data.GetData("System.String")
        Try
            If txtKPIFormula.SelectedText.Length = 0 AndAlso txtKPIFormula.SelectionStart = 0 Then
                If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then
                    If (text = "/") Then
                        '' If (kpiDataBaseName = SandBoxApp.KPIDataBaseName.MSSQL) Then
                        text = "/ NULLIF((),0)"
                        'ElseIf (kpiDataBaseName = SandBoxApp.KPIDataBaseName.ORACLE) Then
                        '    text = "/ NULLIF(" & strDenominator & ",0)"
                        'ElseIf (kpiDataBaseName = SandBoxApp.KPIDataBaseName.None) Then
                        '    text = "/"
                        'End If
                    End If
                    If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
                        txtKPIFormula.Text = text
                    Else
                        If txtKPIFormula.Text.EndsWith("()") Then
                            txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 1, text)
                        ElseIf txtKPIFormula.Text.Contains("/ NULLIF((),0)") Then
                            txtKPIFormula.Text = txtKPIFormula.Text.Replace("/ NULLIF((),0)", "/ NULLIF(" + text + ",0)")
                        Else
                            txtKPIFormula.Text += " " & text
                        End If
                    End If
                End If
                If dragDropType = DragDropType.ByCounter Then
                    ''Dim items() As VIBlend.WinForms.Controls.ListItem = e.Data.GetData("VIBlend.WinForms.Controls.vCheckedListBox.Items[])")
                    Dim items As List(Of String) = text.Split("#").ToList
                    If (items.Count >= 1) Then
                        ' Dim selectedTableCounterRows() As DataRow = dt_TechnologyPackageKPI.Select("TableName='" & items(9).Value.ToString() & "' and CounterName='" & items(10).Value.ToString() & "' ")

                        'If Not (IsItemExist(tabeleName, tlv_UsingTableName)) Then
                        '    ' InsertItemInUsingTableTLV(tabeleName, tableAlias)
                        '    'SetRowInDTUsingTable(tabeleName, tableKey, connectionName, dataBaseName, tableAlias, megaQuery)
                        '    'tlv_UsingTableName.Refresh()
                        '    'tlv_UsingTableName.UpdateLayout()
                        '    If (dataBaseName = dbMSSQL) Then
                        '        kpiDataBaseName = IOS.KPIDataBaseName.MSSQL
                        '    ElseIf (dataBaseName = dbORACLE) Then
                        '        kpiDataBaseName = IOS.KPIDataBaseName.ORACLE
                        '    Else
                        '        kpiDataBaseName = IOS.KPIDataBaseName.None
                        '    End If
                        'End If
                        Dim sourceTableAsTableAlias As String = GetSourceTableIdByCounterId(items(1)) '' items(0)
                        If (sourceTableAsTableAlias Is Nothing) Then
                            SetMessage("No Source Table found.")
                            Exit Sub
                        End If

                        'Dim counterNameAsTableCounter As String = items(1)
                        If Not list_of_used_tables.Contains(sourceTableAsTableAlias) Then
                            'Dim guiTimeResolution As String = cmbTimeResolution.SelectedItem.ToString
                            'Dim timeaggregationSuffix As String = String.Empty

                            'Dim guiObjectTableType As String = cmbObjectType.SelectedItem.ToString
                            'Dim suffixTimeAndObject As List(Of String) = New List(Of String)


                            'Dim suffixTime As String = ""
                            'Dim suffixObject As New List(Of String)

                            'suffixTime = GetTimeSuffix(sourceTableAsTableAlias, guiTimeResolution, guiObjectTableType)(0)
                            'suffixObject = GetObjectSuffix(sourceTableAsTableAlias, guiTimeResolution, guiObjectTableType)

                            'Dim st As String = ""
                            'If suffixTime <> "" And suffixObject(0) <> "_" + guiObjectTableType Then
                            '    st = "[" + sourceTableAsTableAlias.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
                            'Else
                            '    st = "[" + sourceTableAsTableAlias.Replace("[", "").Replace("]", "") & suffixObject(0) & suffixTime + "]"
                            'End If
                            list_of_used_tables.Add(sourceTableAsTableAlias)
                        End If

                        Dim tableAliasAndTableCounter = sourceTableAsTableAlias & ".[" & items(0) & "]"
                        If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
                            txtKPIFormula.Text = tableAliasAndTableCounter
                        Else

                            If (txtKPIFormula.Text.Contains("()")) Then

                                'Dim CharNo As New Integer
                                'CharNo = vtxt_KPIFormula.Text.IndexOf("[]")
                                Dim indexOfBrackets As Integer = txtKPIFormula.Text.IndexOf("()")
                                txtKPIFormula.Text = txtKPIFormula.Text.Remove(indexOfBrackets, 2)
                                txtKPIFormula.Text = txtKPIFormula.Text.Insert(indexOfBrackets, "(" & tableAliasAndTableCounter & ")")

                                'vtxt_KPIFormula.Text = vtxt_KPIFormula.Text.Remove(vtxt_KPIFormula.Text.IndexOf("[]"), 2)
                                'vtxt_KPIFormula.Text = vtxt_KPIFormula.Text.Insert(vtxt_KPIFormula.Text.Length - 1, tableAliasAndTableCounter)
                            ElseIf (txtKPIFormula.Text.EndsWith(",0)")) Then
                                Dim endIndex As Integer = txtKPIFormula.Text.IndexOf(",0)")
                                Dim listOfIndex As List(Of Integer) = GetMatchingIndexCollection(txtKPIFormula.Text, "(")
                                Dim startIndex As Integer = (From w In listOfIndex
                                                             Where w < endIndex
                                                             Select w).Max()
                                Dim sSubString As String = txtKPIFormula.Text.Substring(startIndex + 1, (endIndex - (startIndex + 1)))
                                If (strDenominator = sSubString) Then
                                    txtKPIFormula.Text = txtKPIFormula.Text.Substring(0, txtKPIFormula.Text.IndexOf(strDenominator)) + tableAliasAndTableCounter & ",0)"
                                ElseIf (String.IsNullOrEmpty(sSubString)) Then
                                    txtKPIFormula.Text = txtKPIFormula.Text.Insert(startIndex + 1, tableAliasAndTableCounter)
                                Else
                                    txtKPIFormula.Text += tableAliasAndTableCounter
                                    ' vtxt_KPIFormula.Text = vtxt_KPIFormula.Text.Insert(vtxt_KPIFormula.Text.Length - 3, tableAlias & "." & tabeleCounter)
                                End If
                            ElseIf txtKPIFormula.Text.EndsWith("()") Then
                                txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 1, tableAliasAndTableCounter)
                            Else
                                txtKPIFormula.Text += tableAliasAndTableCounter
                            End If

                        End If
                    End If
                End If
            ElseIf txtKPIFormula.SelectedText.Length > 0 Then
                If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then
                    If text = "Avg()" OrElse text = "Sum()" OrElse text = "Count()" OrElse text = "Min()" OrElse text = "Max()" Then
                        txtKPIFormula.Text = txtKPIFormula.Text.Replace(txtKPIFormula.SelectedText, text.Substring(0, text.IndexOf("(")))
                    Else
                        txtKPIFormula.Text = txtKPIFormula.Text.Replace(txtKPIFormula.SelectedText, text)
                    End If
                ElseIf dragDropType = DragDropType.ByCounter Then
                    Dim items As List(Of String) = text.Split("#").ToList
                    If (items.Count >= 1) Then
                        Dim sourceTableAsTableAlias As String = GetSourceTableIdByCounterId(items(1))
                        txtKPIFormula.Text = txtKPIFormula.Text.Replace(txtKPIFormula.SelectedText, sourceTableAsTableAlias & ".[" & items(0) & "]")
                    End If
                End If
            ElseIf txtKPIFormula.SelectionStart > 0 Then
                If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then

                    If (text = "/") Then
                        text = "/ NULLIF((),0)"
                    End If
                    If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
                        txtKPIFormula.Text = text
                    Else
                        If (txtKPIFormula.Text.EndsWith("()")) Then
                            txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 1, text)
                        ElseIf txtKPIFormula.Text.Contains("/ NULLIF((),0)") Then
                            txtKPIFormula.Text = txtKPIFormula.Text.Replace("/ NULLIF((),0)", "/ NULLIF(" + text + ",0)")
                        Else
                            txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.SelectionStart, text)
                        End If
                    End If
                ElseIf dragDropType = DragDropType.ByCounter Then
                    Dim items As List(Of String) = text.Split("#").ToList
                    If (items.Count >= 1) Then
                        Dim sourceTableAsTableAlias As String = GetSourceTableIdByCounterId(items(1))
                        If (sourceTableAsTableAlias Is Nothing) Then
                            SetMessage("No Source Table found.")
                            Exit Sub
                        Else
                            txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.SelectionStart, sourceTableAsTableAlias & ".[" & items(0) & "]")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub lstOperators_MouseDown(sender As Object, e As MouseEventArgs) Handles lstOperators.MouseDown, lstAggregateFunction.MouseDown
        Dim listControl As ListBoxControl = TryCast(sender, ListBoxControl)
        pt = New Point(e.X, e.Y)
        Dim selectedIndex As Integer = listControl.IndexFromPoint(pt)
        If selectedIndex = -1 Then
            pt = Point.Empty
        End If
    End Sub

    Private Sub lstOperators_MouseMove(sender As Object, e As MouseEventArgs) Handles lstOperators.MouseMove
        If e.Button = MouseButtons.Left Then
            If (pt <> Point.Empty) Then
                Dim listControl As ListBoxControl = TryCast(sender, ListBoxControl)
                If (listControl IsNot Nothing) Then
                    Dim index As Integer = listControl.IndexFromPoint(pt)
                    If (index > -1) Then
                        Me.dragDropType = DragDropType.ByOprators
                        listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnTestKPI_Click(sender As Object, e As EventArgs) Handles btnTestKPI.Click
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Dim kpiFormula As String = txtKPIFormula.Text.Trim()
            If (Not String.IsNullOrEmpty(kpiFormula)) Then
                If IsNumeric(kpiFormula) Then
                    lblKPIConfigStatus.Text = "Test successfully."
                    Exit Sub
                End If
            End If

            If (ValidateControlsForKPIConfig()) Then
                If (TestKPI()) Then
                    SetMessage("KPI executed successfully")
                Else
                    SetMessage("KPI not executed successfully.")
                End If
            End If
        Catch ex As Exception
            SetMessage("There is some problem with query. Error: " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCommitKPI_Click(sender As Object, e As EventArgs) Handles btnCommitKPI.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim isPrivate As Boolean = IIf(rbKPIConfigPublic.Checked, False, True)
            Dim modifyStatus As Boolean = False

            If objSandbox.cmbReportTechnology.SelectedIndex = 0 Then
                SetMessage("Please Select Technology")
                Exit Sub
            Else
                If (txtKPIName.Text.Trim = String.Empty) Or (txtKPIFormula.Text.Trim = String.Empty) Then
                    SetMessage("Either KPI Name Or KPI Formula left empty")
                    Exit Sub
                End If
            End If

            Dim objKPIModify As New dlgKPIModify(objSandbox.cmbReportTechnology.SelectedItem.ToString)
            objKPIModify.Creator = lblKPICreator.Text

            Dim dr() As DataRow = objSandbox.dt_TechnologyPackageKPI.Select("KPINAME='" & txtKPIName.Text & "'")
            If Not dr Is Nothing AndAlso dr.Count = 0 Then
                objKPIModify.kpiModifyOption = KPIModifyOption.Add
            Else
                SetMessage("KPI Name already exists in Technology Package...Rename KPI Name in Text Box")
                'Open dialog to confirm whether new KPI is going to be added or need to modify existing KPI.
                objKPIModify.fromLeft = Me.Left + (Me.Width / 2) - (objKPIModify.Width / 2)
                objKPIModify.fromTop = Me.Top + Me.Height
                objKPIModify.ShowDialog()
            End If

            Me.UseWaitCursor = True
            Application.DoEvents()

            If objKPIModify.kpiModifyOption = KPIModifyOption.Add Then
                DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLTechnologyKPIs.Insert(TryCast(objSandbox.cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, txtKPIName.Text, txtKPIDescription.Text, txtKPIFormula.Text, isPrivate, kpiGroupID))
                SetMessage("KPI Successfully Added.")
                modifyStatus = True
            ElseIf objKPIModify.kpiModifyOption = KPIModifyOption.Update Then
                If (lblKPICreator.Text.ToUpper = Environment.UserName.ToUpper) Then
                    DataAccessorODBC.ExecuteNonQuery(connStrSandBoxServer, SQLTechnologyKPIs.Update(objSandbox.lstTechKPI.FocusedNode.Tag, TryCast(objSandbox.cmbReportTechnology.SelectedItem, clsComboBoxItem).Value, txtKPIName.Text, txtKPIDescription.Text, txtKPIFormula.Text, isPrivate))
                    SetMessage("KPI Successfully Updated.")
                    modifyStatus = True

                    Try
                        'updating vsandboxfield if available
                        Dim vSandBoxFieldModel As New EntityModel.SandBoxFieldModel()
                        Dim vSandBoxElement As DevExSandBoxField = New DevExSandBoxField()

                        For Each flowLayoutPanelXYControls As Object In objSandbox.flp_ValueY.Controls
                            vSandBoxElement = TryCast(flowLayoutPanelXYControls, DevExSandBoxField)
                            If vSandBoxElement.SQL_KPI_ID = objSandbox.lstTechKPI.FocusedNode.Tag Then
                                vSandBoxElement.SQL_KPIFormula = txtKPIFormula.Text
                            End If
                        Next
                    Catch ex As Exception
                    End Try
                Else
                    SetMessage("Only KPI creator can modify.")
                End If
            End If
            If (modifyStatus = True) Then
                objSandbox.RefreshKPITree()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.UseWaitCursor = False
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub frmDatamartKpiConfig_Load(sender As Object, e As EventArgs) Handles Me.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            ConfigureIOSKPIConfig("frmDatamartKpiConfig")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class