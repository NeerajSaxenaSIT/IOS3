Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports dotnetCHARTING.WinForms
Imports System.Data.SqlClient
Imports System.Data.DataTableExtensions
Imports System.Linq
Imports LidorSystems.IntegralUI.Lists
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports IOS.Library
Imports System.Text.RegularExpressions
Imports IOS.Configuration.ExtensionMethods
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base

Public Class frmKPIManage

#Region "Variables"

    Dim dragDropType As DragDropType = DragDropType.NoDragDrop
    Dim kpiDataBaseName As KPIDataBaseName = KPIDataBaseName.None
    Dim dtTablesInUse As DataTable = Nothing
    Dim dtKPI As System.Data.DataTable = Nothing
    Dim dtTablesAndCounters As System.Data.DataTable = Nothing
    Dim isFirstTime As Boolean = True
    Dim OldSqlKpiForEditName As String = ""
    Dim dbORACLE As String = "ORACLE"
    Dim dbMSSQL As String = "MSSQL"
    Dim strDenominoter As String = "()"
    Dim objfrmTechnology As frmTechnology
    Dim p As Point = Point.Empty

#End Region

#Region "Form & Controls' Events"

    Private Sub frmKPIManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            BindTechnologies()
            ConfigurKPIManagerForm("frmKPIManage")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub frm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub cmbObjectList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbObjectList.SelectedIndexChanged
        Try
            If (cmbObjectList.SelectedIndex = 0) Then
                lblMessage.Text = ""
                IOSDevExpressGrid.ClearGrid(grdKpiList)
                dgvTableCounter.SuspendLayout()
                dgvTableCounter.DataSource = Nothing
                dgvTableCounter.Refresh()
                dgvTableCounter.ResumeLayout()
                ClearUsingTableData()
                txtKPIFormula.Text = ""
                txtKPIDescription.Text = ""
            Else
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                lblMessage.Text = ""
                ClearUsingTableData()
                GetKPI_onSelectObject(cmbTechnology.SelectedItem.ToString, cmbObjectList.SelectedItem.ToString)
                GetTableCounter_onSelectObject(cmbTechnology.SelectedItem.ToString, cmbObjectList.SelectedItem.ToString)
            End If
            txtSearchCounterName.Text = String.Empty
            gvKpiList.OptionsBehavior.Editable = False
            gvKpiList.OptionsBehavior.ReadOnly = True
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Try
            kpiDataBaseName = IOS.Library.KPIDataBaseName.None
            txtKPIFormula.Text = ""
            txtKPIDescription.Text = ""
            If (cmbTechnology.SelectedIndex = 0) Then
                SetMessage("Select Technology")
                cmbObjectList.SuspendLayout()
                cmbObjectList.Properties.Items.Clear()
                cmbObjectList.Properties.Items.Insert(0, "Select object")
                cmbObjectList.SelectedIndex = 0
                cmbObjectList.Refresh()
                cmbObjectList.ResumeLayout()
                ClearUsingTableData()
            Else
                BindObject(cmbTechnology.SelectedItem.ToString)
            End If
            dgvTableCounter.SuspendLayout()
            dgvTableCounter.DataSource = Nothing
            dgvTableCounter.Refresh()
            dgvTableCounter.ResumeLayout()
            txtSearchCounterName.Text = String.Empty
            gvKpiList.OptionsBehavior.Editable = False
            gvKpiList.OptionsBehavior.ReadOnly = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmKPIDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmKPIDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lblMessage.Text = ""
            Console.WriteLine("test:" & gvKpiList.GetFocusedRowCellValue("Creator").ToString.ToLower.Trim)
            If (gvKpiList.GetFocusedRowCellValue("Creator").ToString.ToLower.Trim = Environment.UserName.ToLower.Trim) Then
                Dim sqlKpiID As String = gvKpiList.GetFocusedRowCellValue("SQLKPI_ID")
                If (gvKpiList.GetFocusedRowCellValue("SQLKPI_ID") IsNot Nothing) Then
                    ''Dim sqlDeleteKPI As String = "delete IOS_SQL_KPI where SQLKPI_ID='" & sqlKpiID & "'  and Creator='" & Environment.UserName & "'"
                    IOS.DataLibrary.clsSQLCommands.DeleteSqlKPI(connStrIOSServer, sqlKpiID, Environment.UserName)
                    gvKpiList.DeleteRow(gvKpiList.FocusedRowHandle)
                    Dim deletedRows() As DataRow = dtKPI.Select("SQLKPI_ID='" & sqlKpiID & "'")
                    For Each row As DataRow In deletedRows
                        dtKPI.Rows.Remove(row)
                    Next
                    kpiDataBaseName = IOS.Library.KPIDataBaseName.None
                    SetMessage("KPI Deleted Successfully")
                End If
            Else
                SetMessage("Not an owner of KPI")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmKPIRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmKPIRename.Click
        Try
            If (gvKpiList.RowCount > 0) Then
                gvKpiList.OptionsBehavior.Editable = True
                gvKpiList.OptionsBehavior.ReadOnly = False
                OldSqlKpiForEditName = txtKPIFormula.Text
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub AddNew_KPI()
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        lblMessage.Text = ""
        Dim techName As String = cmbTechnology.SelectedItem.ToString.TrimEnd(" ")
        Dim objectName As String = cmbObjectList.SelectedItem.ToString.TrimEnd(" ")
        Try
            If techName = "Select Tech" Or objectName = "Select object" Then
                SetMessage("Either technology or object is not selected")
                Exit Sub
            End If
            lblMessage.Text = ""
            Dim newKPIName As String = Nothing
            newKPIName = XtraInputBox.Show("KPI Name: ", "Add New KPI", "")
            If newKPIName = "" Then
                Exit Sub
            End If

            If Not (CheckItemExistance(newKPIName, gvKpiList)) Then
                Dim sqlQuery As String = IOS.DataLibrary.clsSQLCommands.GetSqlQueryToAddNewKpi(techName, newKPIName, objectName, Environment.UserName, cmbTechnology.SelectedItem.ToString, cmbObjectList.SelectedItem.ToString, txtKPIDescription.Text.Trim)
                RefreshKPI_GridList(sqlQuery, newKPIName)
                gvKPIList_FocusedRowChanged(Nothing, Nothing)
                kpiDataBaseName = IOS.Library.KPIDataBaseName.None
            Else
                SetMessage("KPI Already Exists")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvKPIList_FocusedRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvKpiList.FocusedRowChanged
        lblMessage.Text = ""
        tlvUsingTableName.Nodes.Clear()
        If (dtTablesInUse IsNot Nothing) Then
            If (dtTablesInUse.Rows.Count > 0) Then
                dtTablesInUse.Rows.Clear()
            End If
        End If
        txtKPIFormula.Text = ""
        If (gvKpiList.RowCount > 0 AndAlso gvKpiList.GetFocusedRowCellValue("SQLKPI_ID") IsNot Nothing) Then
            BindKPISQL_Formula(gvKpiList.GetFocusedRowCellValue("SQLKPI_ID"), gvKpiList.GetFocusedRowCellValue("KPI_Name").ToString)
        End If
    End Sub

    Private Sub gvKpiList_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvKpiList.ShowingEditor
        Try
            If (gvKpiList.FocusedColumn().FieldName = "KPI_Name") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvKpiList_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvKpiList.CellValueChanged
        Try
            Dim data As DataRow = gvKpiList.GetFocusedDataRow()
            If (data IsNot Nothing AndAlso e.Value <> "") Then
                If (e.Column.FieldName.ToUpper = "KPI_NAME") Then
                    Dim newKPI As String = e.Value.ToString
                    Dim kpiID As String = data.Item("SQLKPI_ID")
                    Dim formula As String = OldSqlKpiForEditName & " " & newKPI
                    IOS.DataLibrary.clsSQLCommands.UpdateSqlKpi(connStrIOSServer, newKPI, formula, kpiID)

                    Dim deletedRows() As DataRow = dtKPI.Select("SQLKPI_ID='" & kpiID & "'")
                    For Each row As DataRow In deletedRows
                        row("KPI_Name") = newKPI
                    Next
                    dtKPI.AcceptChanges()
                    OldSqlKpiForEditName = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            gvKpiList.OptionsBehavior.Editable = False
            gvKpiList.OptionsBehavior.ReadOnly = True
        End Try
    End Sub

    Private Sub tlvKPIList_AfterLabelEdit(ByVal sender As System.Object, ByVal e As LidorSystems.IntegralUI.ObjectEditEventArgs)
        Dim tlvKPINode As New LidorSystems.IntegralUI.Lists.TreeListViewSubItem
        tlvKPINode = e.Object
        If (e.Label IsNot Nothing AndAlso tlvKPINode IsNot Nothing) Then
            If Not (e.Label = "") AndAlso Not e.Label = tlvKPINode.Text Then
                Dim newKPI As String = e.Label.Trim()
                Dim kpiID As String = tlvKPINode.Tag.ToString()
                Dim formula As String = OldSqlKpiForEditName & " " & newKPI
                IOS.DataLibrary.clsSQLCommands.UpdateSqlKpi(connStrIOSServer, newKPI, formula, kpiID)

                Dim deletedRows() As DataRow = dtKPI.Select("SQLKPI_ID='" & kpiID & "'")
                For Each row As DataRow In deletedRows
                    row("KPI_Name") = newKPI
                Next
                dtKPI.AcceptChanges()
                OldSqlKpiForEditName = ""
                Exit Sub
            End If
        End If
        e.Cancel = True
    End Sub

    Private Sub txtKPIFormula_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtKPIFormula.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub txtKPIFormula_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtKPIFormula.DragDrop
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim text As String = e.Data.GetData("System.String")

            If txtKPIFormula.SelectedText.Length = 0 AndAlso (txtKPIFormula.SelectionStart = 0 Or txtKPIFormula.Text.Trim.Count - 1 <= txtKPIFormula.SelectionStart) Then
                If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then
                    If (text = "/") Then
                        If (kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL) Then
                            text = "/ NULLIF(" & strDenominoter & ",0)"
                        ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE) Then
                            text = "/ NULLIF(" & strDenominoter & ",0)"
                        ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.None) Then
                            text = "/"
                        End If
                    End If
                    If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
                        txtKPIFormula.Text = text
                    Else
                        If (txtKPIFormula.Text.EndsWith("()")) Then
                            txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 1, text)
                        ElseIf txtKPIFormula.Text.Contains("/ NULLIF((),0)") Then
                            txtKPIFormula.Text = txtKPIFormula.Text.Replace("/ NULLIF((),0)", "/ NULLIF(" + text + ",0)")
                        ElseIf (text = "(") AndAlso txtKPIFormula.SelectionStart = 0 Then
                            txtKPIFormula.Text = text & txtKPIFormula.Text
                        Else
                            txtKPIFormula.Text += " " & text
                        End If
                    End If
                End If

                If dragDropType = DragDropType.ByTableCounter Then
                    text = "ISNULL(" + text + ",0)"

                    Dim items As System.Data.DataTable = e.Data.GetData("System.Data.DataTable")
                    If (items IsNot Nothing AndAlso items.Rows.Count >= 1) Then
                        Dim selectedTableCounterRows As DataRow = items(0) ' dtTablesAndCounters.Select("TableName='" & items.Rows(0).Item("TableName").ToString() & "' and CounterName='" & items.Rows(0).Item("CounterName").ToString() & "' ")
                        Dim internalTableID As String = selectedTableCounterRows(0).ToString()
                        Dim tabeleName As String = selectedTableCounterRows(1).ToString()
                        Dim tabeleCounter As String = selectedTableCounterRows(2).ToString()
                        Dim tableKey As String = selectedTableCounterRows(3).ToString()
                        Dim connectionName As String = selectedTableCounterRows(4).ToString()
                        Dim dataBaseName As String = selectedTableCounterRows(5).ToString()
                        Dim tableAlias As String = selectedTableCounterRows(6).ToString()
                        Dim megaQuery As String = selectedTableCounterRows(7).ToString()

                        Dim tabeleCounterWithIsNUll As String = "ISNULL(" + tableAlias & "." & tabeleCounter + ",0)"


                        If (Not (dataBaseName = kpiDataBaseName.ToString()) AndAlso Not (kpiDataBaseName = IOS.Library.KPIDataBaseName.None)) Then
                            XtraMessageBox.Show("Used KPI Formula is using " & kpiDataBaseName.ToString() & " DataBase Table and you are draging table of " & dataBaseName & " ", "Warning")
                        Else
                            If Not (IsItemExist(tabeleName, tlvUsingTableName)) Then
                                InsertItemInUsingTableTLV(tabeleName, tableAlias)
                                SetRowInDTUsingTable(tabeleName, tableKey, connectionName, dataBaseName, tableAlias, megaQuery)
                                tlvUsingTableName.Refresh()
                                tlvUsingTableName.UpdateLayout()
                                If (dataBaseName = dbMSSQL) Then
                                    kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL
                                ElseIf (dataBaseName = dbORACLE) Then
                                    kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE
                                Else
                                    kpiDataBaseName = IOS.Library.KPIDataBaseName.None
                                End If
                            End If
                            If String.IsNullOrEmpty(txtKPIFormula.Text.Trim) Then
                                txtKPIFormula.Text = " " + tabeleCounterWithIsNUll
                            Else
                                If (txtKPIFormula.Text.Trim.EndsWith("()")) Then
                                    txtKPIFormula.Text = txtKPIFormula.Text.Trim.Insert(txtKPIFormula.Text.Length - 1, tabeleCounterWithIsNUll)
                                ElseIf (txtKPIFormula.Text.Trim.EndsWith("(),0)")) Then
                                    txtKPIFormula.Text = txtKPIFormula.Text.Trim.Insert(txtKPIFormula.Text.Length - 4, tabeleCounterWithIsNUll)
                                    'ElseIf (txtKPIFormula.Text.Trim.EndsWith(",0)")) Then
                                    '    Dim endIndex As Integer = GetMatchingIndexCollection(txtKPIFormula.Text, ",0)").Last ' txtKPIFormula.Text.IndexOf(",0)")
                                    '    Dim listOfIndex As List(Of Integer) = GetMatchingIndexCollection(txtKPIFormula.Text, "(")
                                    '    Dim startIndex As Integer = (From w In listOfIndex Where w < endIndex Select w).Max()
                                    '    Dim sSubString As String = txtKPIFormula.Text.Substring(startIndex, (endIndex - (startIndex)))
                                    '    If (strDenominoter = sSubString) Then
                                    '        txtKPIFormula.Text = txtKPIFormula.Text.Substring(0, txtKPIFormula.Text.IndexOf(strDenominoter)) + "(" & tabeleCounterWithIsNUll & "),0)"
                                    '    ElseIf (String.IsNullOrEmpty(sSubString)) Then
                                    '        txtKPIFormula.Text = txtKPIFormula.Text.Insert(startIndex + 1, tabeleCounterWithIsNUll)
                                    '    Else
                                    '        txtKPIFormula.Text += " " & tabeleCounterWithIsNUll
                                    '    End If
                                ElseIf (txtKPIFormula.Text.EndsWith("(,)")) Then
                                    txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.Text.Length - 2, tabeleCounterWithIsNUll)
                                ElseIf (txtKPIFormula.Text.Contains("()")) Then
                                    Dim ixToInsert As Integer = txtKPIFormula.Text.IndexOf("()")
                                    txtKPIFormula.Text = txtKPIFormula.Text.Insert(ixToInsert + 1, tabeleCounterWithIsNUll)
                                Else
                                    txtKPIFormula.Text += " " & tabeleCounterWithIsNUll
                                End If
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
                ElseIf dragDropType = DragDropType.ByTableCounter Then
                    Dim items As System.Data.DataTable = e.Data.GetData("System.Data.DataTable")
                    If (items IsNot Nothing AndAlso items.Rows.Count >= 1) Then
                        Dim selectedTableCounterRows As DataRow = items(0)
                        txtKPIFormula.Text = txtKPIFormula.Text.Replace(txtKPIFormula.SelectedText, selectedTableCounterRows(6).ToString() & "." & selectedTableCounterRows(2).ToString())
                    End If
                End If
            ElseIf txtKPIFormula.SelectionStart > 0 Then
                If dragDropType = DragDropType.ByAggregrate Or dragDropType = DragDropType.ByOprators Then

                    If (text = "/") Then
                        If (kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL) Then
                            text = "/ NULLIF(" & strDenominoter & ",0)"
                        ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE) Then
                            text = "/ NULLIF(" & strDenominoter & ",0)"
                        ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.None) Then
                            text = "/"
                        End If
                    End If
                    txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.SelectionStart, text)
                ElseIf dragDropType = DragDropType.ByTableCounter Then
                    Dim items As System.Data.DataTable = e.Data.GetData("System.Data.DataTable")
                    If (items IsNot Nothing AndAlso items.Rows.Count >= 1) Then
                        Dim selectedTableCounterRows As DataRow = items(0)
                        txtKPIFormula.Text = txtKPIFormula.Text.Insert(txtKPIFormula.SelectionStart, selectedTableCounterRows(6).ToString() & "." & selectedTableCounterRows(2).ToString())
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnKPITest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_KPITest.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            lblMessage.Text = ""
            Dim kpiFarmula As String = txtKPIFormula.Text.Trim()
            If (Not String.IsNullOrEmpty(kpiFarmula)) Then
                If IsNumeric(kpiFarmula) Then
                    SetMessage("KPI test successful")
                    Exit Sub
                End If
            End If

            If (ValidateControls()) Then
                If (TestKPI()) Then
                    XtraMessageBox.Show("KPI executed successfully")
                Else
                    XtraMessageBox.Show("KPI Not executed successfully.")
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("There is some problem with query. Error: " & ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Function GetRowsFromIOSSQLKPI(ByVal tech As String, ByVal obj As String, ByVal sourcetable As String) As DataRow
        Try
            Dim dt As DataTable = IOS.DataLibrary.clsSQLCommands.GetSqlKpiListFromTech(connStrIOSServer, tech, obj)
            ''IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "SELECT distinct sourcetable, tablealias, JoinObjects FROM [dbo].[IOS_SQL_KPI] where tech = '" & tech & "'  and Object = '" & obj & "' and sourcetable <> '' ")
            dt.CaseSensitive = False
            If dt.Rows.Count > 0 Then

                If dt.Select("sourcetable not like '*<AggregatedObject>*'").Count > 0 Then
                    Return dt.Select("sourcetable not like '*<AggregatedObject>*'")(0)
                Else
                    'check if tablealias is contained in the dt
                    If dt.Select("sourcetable like '*" & sourcetable.Split(".").Last & "*'").Count > 0 Then
                        Return dt.Select("sourcetable like '*" & sourcetable.Split(".").Last & "*'")(0)
                    Else
                        Return Nothing
                    End If
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub btnKPICommit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKPICommit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        lblMessage.Text = ""
        Dim commitStr As String = Nothing
        Dim kpiFormula As String = txtKPIFormula.Text.Replace(Environment.NewLine, "").Replace(vbCr, "").Replace(vbLf, "").Trim
        Dim kpiName As String = Nothing
        Dim kpiId As String = Nothing
        Dim tableKey As String = Nothing
        Dim KpiSQL As String = Nothing
        Dim connectionName As String = Nothing
        Dim dataBaseName As String = Nothing
        Dim tableAlias As String = Nothing
        Dim tableNames As String
        Dim JoinObject As String = ""
        Dim megaQuery As String = ""
        Dim iresult As Integer = -1

        If gvKpiList.RowCount = 0 Then
            SetMessage("Select a KPI from the list")
            Exit Sub
        Else
            lblMessage.Text = ""
            kpiName = gvKpiList.GetFocusedRowCellValue("KPI_Name").ToString
            kpiId = gvKpiList.GetFocusedRowCellValue("SQLKPI_ID").ToString
        End If
        If (Not String.IsNullOrEmpty(kpiFormula)) Then
            If IsNumeric(kpiFormula) Then
                CommitStaticKPI()
            Else
                If (ValidateControls()) Then
                    Try
                        If (TestKPI()) Then
                            tableNames = GetUsingAllTableNames(tableKey, connectionName, dataBaseName, tableAlias, "ByCommit", JoinObject, megaQuery)

                            If (tableNames IsNot Nothing) Then

                                Dim tablenames_original As String = tableNames.TrimEnd(",")
                                tableNames = Replace(tableNames.TrimEnd(","), "<AggregatedObject>", cmbObjectList.Text)

                                'check if IOS_SQL_KPI for this tech is using method of <AggregetedObject>, if not take the content that already exists in IOS_SQL_KPI. This means the queries are likely based on multiple joins, and not meant to be 'single table queries'
                                Dim ExistingRowOfIOSSQLKPI As DataRow = GetRowsFromIOSSQLKPI(cmbTechnology.SelectedItem.ToString, cmbObjectList.SelectedItem.ToString, tablenames_original)
                                If Not ExistingRowOfIOSSQLKPI Is Nothing And megaQuery.ToUpper = "TRUE" Then
                                    tableNames = ExistingRowOfIOSSQLKPI("sourcetable").ToString
                                    tablenames_original = tableNames
                                    tableAlias = ExistingRowOfIOSSQLKPI("tablealias").ToString
                                    JoinObject = ExistingRowOfIOSSQLKPI("JoinObjects").ToString
                                End If

                                Dim tableCount As Integer = tableNames.GetCountItems(",")
                                Dim isUpdate As DialogResult = XtraMessageBox.Show("Do you want to Update KPI?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                If (isUpdate = DialogResult.Yes) Then
                                    If Not (kpiDataBaseName = IOS.Library.KPIDataBaseName.None) Then
                                        If (tableKey IsNot Nothing AndAlso connectionName IsNot Nothing AndAlso dataBaseName IsNot Nothing) Then
                                            If (kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL) Then

                                                If Not kpiFormula.ToUpper.StartsWith("ISNULL") And Not kpiFormula.ToUpper.StartsWith("COALESCE") And txtValueIfNull.Text.TrimEnd <> "" Then
                                                    KpiSQL = "ISNULL(" & kpiFormula & "," & txtValueIfNull.Text & ")" + " [" + kpiName + "]"
                                                Else
                                                    KpiSQL = kpiFormula + " [" + kpiName + "]"
                                                End If
                                            ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE) Then
                                                If Not kpiFormula.ToUpper.StartsWith("NVL") And Not kpiFormula.ToUpper.StartsWith("COALESCE") And txtValueIfNull.Text.TrimEnd <> "" Then
                                                    KpiSQL = "NVL(" & kpiFormula & "," & txtValueIfNull.Text & ")" + " [" + kpiName + "]"
                                                Else
                                                    KpiSQL = kpiFormula + " [" + kpiName + "]"
                                                End If
                                            End If

                                            iresult = IOS.DataLibrary.clsSQLCommands.UpdateSqlKpiAsCommitted(connStrIOSServer, tableCount, cmbTechnology.SelectedItem.ToString, tablenames_original, tableAlias,
                                                                                                            kpiName, KpiSQL, JoinObject, cmbObjectList.SelectedItem.ToString, Environment.UserName, kpiId, txtKPIDescription.Text.Trim)
                                        End If
                                        If (iresult > 0) Then
                                            SetMessage("KPI Successfully Updated")
                                        Else
                                            SetMessage("KPI Not Updated")
                                            objfrmTechnology = Nothing
                                            If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(cmbTechnology.Text)) Then
                                                frmMDI.OpenTechFormDynamically(cmbTechnology.Text, objfrmTechnology, False)
                                            Else
                                                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(cmbTechnology.Text)).LastOrDefault()
                                            End If
                                            objfrmTechnology.FiltersInitialize()
                                        End If
                                    Else
                                        SetMessage("Sorry! DataBase Not Selected")
                                    End If
                                End If
                            End If
                        Else
                            'lblMessage.Text = "KPI Not executed successfully."
                        End If
                    Catch ex As Exception
                        XtraMessageBox.Show("There is some problem with query. Error: " & ex.Message)
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                    Finally
                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
                    End Try
                End If
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub lst_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstOperators.MouseDown, lstAggregateFunction.MouseDown
        Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
        p = New Point(e.X, e.Y)
        Dim selectedIndex As Integer = listControl.IndexFromPoint(p)
        If selectedIndex = -1 Then
            p = Point.Empty
        End If
    End Sub

    Private Sub dgvTableCounter_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles dgvTableCounter.DragDrop
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub dgvTableCounter_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTableCounter.MouseMove
        If (e.Button AndAlso MouseButtons.Left = MouseButtons.Left) Then
            Dim dtData As DataTable = DirectCast(dgvTableCounter.DataSource, DataTable).Clone()
            If dtData IsNot Nothing Then
                For i As Integer = 0 To gvTableCounter.GetSelectedRows().Count - 1
                    dtData.ImportRow(DirectCast(gvTableCounter.GetRow(gvTableCounter.GetSelectedRows()(i)), DataRowView).Row)
                Next
                dtData.AcceptChanges()
                Me.dragDropType = IOS.Library.DragDropType.ByTableCounter
                Dim dropEffect As DragDropEffects = dgvTableCounter.DoDragDrop(dtData, DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub txtKPIFormula_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKPIFormula.Leave
        Dim removeableItems As New List(Of Integer)
        For Each ss As TreeListViewNode In tlvUsingTableName.Nodes
            If Not (txtKPIFormula.Text.ToUpper.Contains((ss.SubItems(1).Text.ToUpper) & ".")) Then
                removeableItems.Add(ss.Index)
            End If
        Next
        Dim isFirstTime As Boolean = True
        Dim counter As Integer = 1
        For Each removeItem As Integer In removeableItems
            If (dtTablesInUse IsNot Nothing AndAlso dtTablesInUse.Rows.Count > 0) Then
                If Not (isFirstTime) Then
                    removeItem = removeItem - counter
                    counter = counter + 1
                End If
                dtTablesInUse.Rows(removeItem).Delete()
                isFirstTime = False
            End If
            tlvUsingTableName.SuspendLayout()
            tlvUsingTableName.Nodes.Item(removeItem).Remove()
            tlvUsingTableName.Refresh()

            tlvUsingTableName.UpdateLayout()
        Next
        If (txtKPIFormula.Text.Trim() = "" AndAlso txtKPIFormula.Text.Trim().Length <= 0) Then
            kpiDataBaseName = IOS.Library.KPIDataBaseName.None
        End If
    End Sub

    Private Sub frm_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        dgvTableCounter.SuspendLayout()
        SetAutoColumnSizeOnGrid(dgvTableCounter, gvTableCounter)
        dgvTableCounter.Refresh()
        dgvTableCounter.ResumeLayout()
    End Sub

    Private Sub txtKPIFormula_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKPIFormula.KeyDown
        Try
            If (e.KeyValue = 191) Then
                e.Handled = False
                e.SuppressKeyPress = True
                Dim lastValue As String = txtKPIFormula.Text
                If (kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL) Then
                    lastValue += "/ NULLIF(" & strDenominoter & ",0)"
                ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE) Then
                    lastValue += "/ NULLIF(" & strDenominoter & ",0)"
                ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.None) Then
                    lastValue += "/"
                End If
                txtKPIFormula.Text = " " & lastValue
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddKpi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddKpi.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            AddNew_KPI()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub txtValueIfNull_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValueIfNull.TextChanged
        Try
            Dim kpiformula As String = txtKPIFormula.Text
            If kpiformula.StartsWith("ISNULL") And IsNumeric(txtValueIfNull.Text) Then
                kpiformula = kpiformula.Substring(0, kpiformula.LastIndexOf(",")) & "," & txtValueIfNull.Text & ")"
                txtKPIFormula.Text = kpiformula
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchCounterName_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCounterName.TextChanged
        Try

            If (String.IsNullOrEmpty(txtSearchCounterName.Text)) Then
                'Exit Sub
                bindTableCounterGridView(dtTablesAndCounters)
            End If
            If Not dtTablesAndCounters Is Nothing Then
                dgvTableCounter.SuspendLayout()
                If (txtSearchCounterName.Text.Trim.Length > 2) Then
                    If (dtTablesAndCounters.Rows.Count > 0) Then
                        Dim dv As New DataView(dtTablesAndCounters, "CounterName LIKE '%" & txtSearchCounterName.Text.Trim & "%'", "", DataViewRowState.CurrentRows)
                        bindTableCounterGridView(dv.ToTable)
                    Else
                        dgvTableCounter.DataSource = Nothing
                        dgvTableCounter.Refresh()
                    End If
                Else
                    bindTableCounterGridView(dtTablesAndCounters)
                End If
                dgvTableCounter.Refresh()
                dgvTableCounter.ResumeLayout()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstOperators_MouseMove(sender As Object, e As MouseEventArgs) Handles lstOperators.MouseMove
        If e.Button = MouseButtons.Left Then
            If (p <> Point.Empty) Then
                Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
                If (listControl IsNot Nothing) Then
                    Dim index As Integer = listControl.IndexFromPoint(p)
                    If (index > -1) Then
                        Me.dragDropType = IOS.Library.DragDropType.ByOprators
                        listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub lstOperators_DrawItem(sender As Object, e As DevExpress.XtraEditors.ListBoxDrawItemEventArgs) Handles lstOperators.DrawItem
        Dim backBrush1 As New SolidBrush(Color.FromArgb(224, 251, 254))
        Dim backBrush2 As New SolidBrush(Color.FromArgb(198, 241, 249))
        Dim backBrush3 As New SolidBrush(Color.FromArgb(253, 192, 47))
        ' declare field representing the text of the item being drawn 
        Dim itemText As String = (TryCast(sender, DevExpress.XtraEditors.ListBoxControl)).GetItemText(e.Index)
        If Not (e.State And DrawItemState.Selected) = 0 Then
            'e.Cache.FillRectangle(backBrush3, e.Bounds)
            'ControlPaint.DrawBorder3D(e.Graphics, e.Bounds)
            e.Cache.DrawString(itemText, New Font(e.Appearance.Font.Name, e.Appearance.Font.Size,
              FontStyle.Bold), New SolidBrush(Color.Black), e.Bounds, e.Appearance.GetStringFormat())
            e.Handled = True
            Exit Sub
        End If
    End Sub

    Private Sub lstAggregateFunction_MouseMove(sender As Object, e As MouseEventArgs) Handles lstAggregateFunction.MouseMove
        If e.Button = MouseButtons.Left Then
            If (p <> Point.Empty) Then
                Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
                If (listControl IsNot Nothing) Then
                    Dim index As Integer = listControl.IndexFromPoint(p)
                    If (index > -1) Then
                        Me.dragDropType = IOS.Library.DragDropType.ByAggregrate
                        listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
                    End If
                End If
            End If
        End If
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

    Private Sub txtSearchKPI_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchKPI.KeyUp
        Try
            If dtKPI IsNot Nothing Then
                If (txtSearchKPI.Text.Length > 2) Then
                    dtKPI.DefaultView.RowFilter = "KPI_Name Like '%" + txtSearchKPI.Text + "%'"
                Else
                    dtKPI.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Helper Methods"

    Private Sub ConfigurKPIManagerForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                 cmKPIDelete, cmKPIRename
            }

            For Each frmControl As Object In formControls
                winCtrl = form.FindControlByName(frmControl.Name)
                If Not winCtrl Is Nothing Then
                    frmControl.Enabled = winCtrl.DefaultEnable
                    frmControl.Visible = winCtrl.DefaultVisible
                End If
            Next
        End If
    End Sub

    Function GetMatchingIndexCollection(ByVal str As String, ByVal pattern As String) As List(Of Integer)
        Dim list As New List(Of Integer)
        For index As Integer = 0 To str.Length - 1
            If (str(index) = pattern) Then
                list.Add(index)
            End If
        Next
        Return list
    End Function

    Private Sub BindTechnologies()
        'get list of technologies
        Dim dtTech As System.Data.DataTable = Nothing
        Try
            ''Dim sql As String = "Select distinct Tech from IOS_Object_Configuration where Tech !='PLMN'"
            dtTech = IOS.DataLibrary.clsSQLCommands.GetTechOtherThanPLMN(connStrIOSServer)
            If Not dtTech Is Nothing Then
                cmbTechnology.Properties.Items.Clear()
                BindDevExComboBoxWithValueMember(cmbTechnology, dtTech, "Tech", "Tech", "Select Tech")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If Not dtTech Is Nothing Then
                dtTech.Dispose()
                dtTech = Nothing
            End If
        End Try
    End Sub

    Private Sub BindObject(ByVal techName As String)
        'get list of Object list
        lblMessage.Text = ""
        Dim dtObject As System.Data.DataTable = Nothing
        Try
            ''Dim sql As String = "SELECT distinct a.ObjectType object from  dbo.[IOS_SQL_Create] a inner join dbo.[IOS_Object_Configuration] b on a.tech=b.tech and a.ObjectType = b.[Object] where a.purpose IN('Charts','TopX') and a.tech='" & techName & "' "
            dtObject = IOS.DataLibrary.clsSQLCommands.GetObjectTypeFromTech(connStrIOSServer, techName)

            If Not dtObject Is Nothing Then
                cmbObjectList.SuspendLayout()
                cmbObjectList.Properties.Items.Clear()
                BindDevExComboBoxWithValueMember(cmbObjectList, dtObject, "object", "object", "Select object")
                cmbObjectList.SelectedIndex = 0
                cmbObjectList.Refresh()
                cmbObjectList.ResumeLayout()
                IOSDevExpressGrid.ClearGrid(grdKpiList)
                ClearUsingTableData()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If Not dtObject Is Nothing Then
                dtObject.Dispose()
                dtObject = Nothing
            End If
        End Try
    End Sub

    Private Sub BindKPI_ToGrid(ByRef data As DataTable, Optional ByVal newNodeToSelect As String = Nothing)
        If data IsNot Nothing Then
            IOSDevExpressGrid.PopulateDataInGrid(grdKpiList, gvKpiList, data, "ALL", {"SQLKPI_ID", "Creator"}, "KPI_Name")
        End If
    End Sub

    Private Sub GetKPI_onSelectObject(ByVal techName As String, ByVal objectName As String)
        txtKPIFormula.Text = ""
        dtKPI = IOS.DataLibrary.clsSQLCommands.GetKpiOnSelectObject(connStrIOSServer, techName, objectName)

        If Not dtKPI Is Nothing Then
            If (dtKPI.Rows.Count > 0) Then
                BindKPI_ToGrid(dtKPI)
            Else
                SetMessage("Selected Object does not have any KPI.")
            End If
        End If
    End Sub

    Private Sub GetTableCounter_onSelectObject(ByVal techName As String, ByVal objectName As String)
        If Not dtTablesAndCounters Is Nothing Then
            dtTablesAndCounters.Dispose()
            dtTablesAndCounters = Nothing
        End If
        dtTablesAndCounters = New DataTable
        dtTablesAndCounters = IOS.DataLibrary.clsSQLCommands.GetTableCounterOnSelectObject(connStrIOSServer, techName, objectName)

        If Not dtTablesAndCounters Is Nothing Then
            dgvTableCounter.SuspendLayout()
            If (dtTablesAndCounters.Rows.Count > 0) Then
                bindTableCounterGridView(dtTablesAndCounters)
            Else
                ''dgvTableCounter.RowsHierarchy.Items.Clear()
            End If
            dgvTableCounter.Refresh()
            dgvTableCounter.ResumeLayout()
        End If
    End Sub

    Private Sub bindTableCounterGridView(ByRef data As DataTable)
        dgvTableCounter.Visible = False
        dgvTableCounter.DataSource = Nothing
        gvTableCounter.Columns.Clear()
        dgvTableCounter.Refresh()

        gvTableCounter.Columns.AddField("TableName").Visible = True
        gvTableCounter.Columns.AddField("CounterName").Visible = True
        gvTableCounter.Columns.AddField("VendorID").Visible = True
        If data IsNot Nothing AndAlso data.Columns.Contains("Description") Then
            gvTableCounter.Columns.AddField("Description").Visible = True
        End If
        gvTableCounter.OptionsBehavior.AutoPopulateColumns = False
        dgvTableCounter.DataSource = data
        dgvTableCounter.RefreshDataSource()
        SetAutoColumnSizeOnGrid(dgvTableCounter, gvTableCounter)
        dgvTableCounter.Visible = True
    End Sub

    Sub SetAutoColumnSizeOnGrid(ByRef gControl As DevExpress.XtraGrid.GridControl, ByRef gdvObject As DevExpress.XtraGrid.Views.Grid.GridView)
        Dim totalColumns As Int32 = gdvObject.VisibleColumns.Count
        Dim frmCatmanagerwidth As Integer = gControl.Width() - 25
        If (frmCatmanagerwidth > 0) Then
            If (totalColumns > 0) Then
                frmCatmanagerwidth = frmCatmanagerwidth / gdvObject.Columns.Count
                Dim k As Integer
                For k = 0 To totalColumns - 1
                    gdvObject.Columns(k).OptionsFilter.AllowFilter = True
                    gdvObject.Columns(k).Width = frmCatmanagerwidth
                Next
            End If
        End If
    End Sub

    Private Sub InsertItemInUsingTableTLV(ByVal textField As String, ByVal tableAlias As String)
        Try
            Dim tlvnode As TreeListViewNode = New TreeListViewNode()
            Dim tlvnode_sub0 As TreeListViewSubItem = New TreeListViewSubItem(textField)
            tlvnode.SubItems.Add(tlvnode_sub0)
            Dim tlvnode_sub1 As TreeListViewSubItem = New TreeListViewSubItem(tableAlias)
            tlvnode.SubItems.Add(tlvnode_sub1)
            tlvUsingTableName.Nodes.Add(tlvnode)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function CheckItemExistance(newItem As String, gv As DevExpress.XtraGrid.Views.Grid.GridView) As Boolean
        If String.IsNullOrWhiteSpace(newItem) Then Return False
        Return gv.LocateByValue("KPI_Name", newItem) >= 0
    End Function

    Private Function IsItemExist(ByVal newItem As String, ByRef treeControl As LidorSystems.IntegralUI.Lists.TreeListView) As Boolean
        Dim isKPI As Boolean = False
        For Each tlvNode As TreeListViewNode In treeControl.Nodes
            If (tlvNode.SubItems(0).Text.ToUpper() = newItem.ToUpper()) Then
                isKPI = True
                Exit For
            End If
        Next
        Return isKPI
    End Function

    Private Sub RefreshKPI_GridList(ByVal sqlCommand As String, Optional ByVal newNodeToSelect As String = Nothing)
        Try
            dtKPI = IOS.DataLibrary.clsSQLCommands.AddNewKpiAndGetList(connStrIOSServer, sqlCommand)
            If Not dtKPI Is Nothing Then
                BindKPI_ToGrid(dtKPI, newNodeToSelect)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub BindKPISQL_Formula(ByVal sqlKpiID As Integer, ByVal kpiName As String)
        Try
            Dim dtKPISQL As System.Data.DataTable = Nothing
            Dim dtRow() As DataRow
            dtKPISQL = IOS.DataLibrary.clsSQLCommands.GetKpiSqlFormula(connStrIOSServer, sqlKpiID)
            If Not dtKPISQL Is Nothing Then
                If dtKPISQL.Rows.Count > 0 Then
                    txtKPIFormula.Text = ValidateKPIFurmula(dtKPISQL.Rows(0)(0).ToString(), kpiName)
                    txtValueIfNull.Text = GetValueIfNull(dtKPISQL.Rows(0)(0).ToString())
                    txtKPIDescription.Text = IIf(IsDBNull(dtKPISQL.Rows(0)("Description").ToString), "", dtKPISQL.Rows(0)("Description").ToString)
                    If Not String.IsNullOrEmpty(txtKPIFormula.Text) Then
                        For Each dr As DataRow In dtKPISQL.Rows
                            tlvUsingTableName.SuspendLayout()
                            Dim tableKPI As String = dr(1).ToString
                            Dim tableAlias As String = dr(2).ToString
                            If (tableKPI.Length > 0 AndAlso tableAlias.Length > 0) Then
                                Dim tableN As String = tableKPI
                                Dim tAlias As String = tableAlias
                                InsertItemInUsingTableTLV(tableN, tAlias)
                                If dtTablesAndCounters IsNot Nothing AndAlso dtTablesAndCounters.Rows.Count > 0 Then
                                    dtRow = dtTablesAndCounters.Select("TableName ='" & tableN & "'")
                                    If (dtRow.Length > 0) Then
                                        SetRowInDTUsingTable(dtRow(0)(1).ToString(), dtRow(0)(3).ToString(), dtRow(0)(4).ToString(), dtRow(0)(5).ToString(), dtRow(0)(6).ToString(), dtRow(0)(7).ToString())
                                        If (dtRow(0)(5).ToString() = dbMSSQL) Then
                                            kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL
                                        ElseIf (dtRow(0)(5).ToString() = dbORACLE) Then
                                            kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        tlvUsingTableName.Refresh()
                        tlvUsingTableName.UpdateLayout()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetRowInDTUsingTable(ByVal tableName As String, ByVal tableKey As String, ByVal connectionName As String, ByVal dbName As String, ByVal tableAlias As String, ByVal megaQuery As String)
        If (isFirstTime) Then
            dtTablesInUse = New DataTable
            dtTablesInUse.Columns.Add("TableName", GetType(String))
            dtTablesInUse.Columns.Add("TableKey", GetType(String))
            dtTablesInUse.Columns.Add("ConnectionName", GetType(String))
            dtTablesInUse.Columns.Add("DataBaseName", GetType(String))
            dtTablesInUse.Columns.Add("TableAlias", GetType(String))
            dtTablesInUse.Columns.Add("MegaQuery", GetType(String))
            isFirstTime = False
        End If
        Dim drNew As DataRow = dtTablesInUse.NewRow
        drNew("TableName") = tableName
        drNew("TableKey") = tableKey
        drNew("ConnectionName") = connectionName
        drNew("DataBaseName") = dbName
        drNew("TableAlias") = tableAlias
        drNew("MegaQuery") = megaQuery
        dtTablesInUse.Rows.Add(drNew)
    End Sub

    Private Sub ClearUsingTableData()
        If (dtTablesInUse IsNot Nothing) Then
            If (dtTablesInUse.Rows.Count > 0) Then
                dtTablesInUse.Rows.Clear()
                tlvUsingTableName.SuspendLayout()
                tlvUsingTableName.Nodes.Clear()
                tlvUsingTableName.Refresh()
                tlvUsingTableName.UpdateLayout()
            End If
        End If
    End Sub

    Private Function GetUsingAllTableNames(ByRef tableKey As String, ByRef connectionName As String, ByRef dataBaseName As String, ByRef tableAlias As String, ByVal callBy As String, ByRef JoinObject As String, ByRef megaQuery As String) As String
        Dim tableNameStr As String = Nothing
        Dim rowCounter As Integer = 1
        Dim isFirstTime As Boolean = True
        Dim firstTableKey As String = Nothing
        Dim alltablekeylist As New List(Of List(Of String))

        If (dtTablesInUse IsNot Nothing) AndAlso (dtTablesInUse.Rows.Count > 0) Then
            For Each dtRow As DataRow In dtTablesInUse.Rows
                If (callBy = "ByCommit") Then
                    tableNameStr += dtRow(0).ToString() & ","
                Else
                    tableNameStr += dtRow(0).ToString() & " " & dtRow(4).ToString() & ","
                End If

                If (dtTablesInUse.Rows.Count > 1) Then
                    Dim tablekeylist As New List(Of String)
                    For Each key As String In dtRow(1).ToString().Split(",")
                        tablekeylist.Add(dtRow(4).ToString() & "." & key)
                    Next
                    alltablekeylist.Add(tablekeylist)
                    JoinObject = JoinObject + dtRow(1).ToString + ","
                Else
                    If (firstTableKey Is Nothing) Then
                        firstTableKey = dtRow(4).ToString() & "." & dtRow(1).ToString()
                    End If
                    tableKey += dtRow(4).ToString() & "." & dtRow(1).ToString() & "="
                    If Not JoinObject.Contains(dtRow(1).ToString) Then
                        JoinObject = JoinObject + dtRow(1).ToString + ","
                    End If
                End If
                tableAlias += dtRow(4).ToString() & ","
                If (isFirstTime) Then
                    connectionName = dtRow(2).ToString()
                    dataBaseName = dtRow(3).ToString()
                    isFirstTime = False
                End If
                rowCounter += 1
                megaQuery = dtRow(5).ToString
            Next
        End If
        If (dtTablesInUse IsNot Nothing) AndAlso (dtTablesInUse.Rows.Count > 1) Then
            For k As Integer = 0 To alltablekeylist(0).Count - 1    '(loop through rows)
                For j As Integer = 0 To alltablekeylist.Count - 1   '(loop through columns)
                    If j + 1 < alltablekeylist(j).Count Then
                        For m As Integer = j + 1 To alltablekeylist.Count - 1
                            tableKey += alltablekeylist(j)(k) + "=" + alltablekeylist(m)(k) + " AND "
                        Next
                    End If
                Next
            Next
            tableKey = tableKey.Substring(0, tableKey.Length - 4)
        End If

        JoinObject = JoinObject.TrimEnd(",")
        Return tableNameStr
    End Function

    Private Function TestKPI() As Boolean
        Dim testStr As String
        Dim kpiName As String = gvKpiList.GetFocusedRowCellValue("KPI_Name").ToString
        Dim kpiFarmula As String = txtKPIFormula.Text.Trim()
        Dim tableKey As String = Nothing
        Dim connectionName As String = Nothing
        Dim dataBaseName As String = Nothing
        Dim tableAlias As String = Nothing
        Dim megaQuery As String = Nothing
        Dim JoinObject As String = ""
        Dim tableNames As String = GetUsingAllTableNames(tableKey, connectionName, dataBaseName, tableAlias, "ByKPITest", JoinObject, megaQuery)
        Try
            If (IsKPIFurmulaValid(kpiFarmula)) Then
                If (tableNames IsNot Nothing) Then
                    tableNames = Replace(tableNames.Remove(tableNames.Length - 1), "<AggregatedObject>", cmbObjectList.Text)
                    Dim tableCount As Integer = tableNames.GetCountItems(",")
                    If (tableKey IsNot Nothing AndAlso connectionName IsNot Nothing AndAlso dataBaseName IsNot Nothing) Then

                        Dim kpiValue As Integer = 0
                        If txtValueIfNull.Text.TrimEnd <> "" Then
                            If Not CDbl(txtValueIfNull.Text) = 0 Then
                                kpiValue = txtValueIfNull.Text
                            End If
                        End If

                        If (dataBaseName.ToUpper() = dbMSSQL) Then
                            If (tableCount > 1) Then
                                testStr = "Select top 1 " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME, ISNULL(" & kpiFarmula & ",0) [" & kpiName & "] from " & tableNames & " Where " & tableKey.Remove(tableKey.Length - 1) & " and " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME > DATEADD(d,-1,GETDATE())  group by " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME"
                            Else
                                testStr = "Select top 1 " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME, ISNULL(" & kpiFarmula & ",0) [" & kpiName & "] from (select top 5 * from  " & tableNames & " where  PERIOD_START_TIME > DATEADD(d,-7,GETDATE())) " & tableAlias.Split(",")(0) & " group by " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME"
                            End If
                            Dim result As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connectionName, testStr)

                            If (result IsNot Nothing AndAlso result.Rows.Count > 0) Then
                                Return True
                            ElseIf (result Is Nothing) Then
                                Return False
                            Else
                                XtraMessageBox.Show("Query did not return any data")
                                Return True
                            End If
                        ElseIf (dataBaseName.ToUpper() = dbORACLE) Then
                            If (tableCount > 1) Then
                                testStr = "Select " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME, NVL(" & kpiFarmula & ",0) [" & kpiName & "] from " & tableNames & " Where " & tableKey.Remove(tableKey.Length - 1) & " And rownum=1 and " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME > SYSDATE - 1 group by " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME"
                            Else
                                testStr = "Select " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME, NVL(" & kpiFarmula & ",0) [" & kpiName & "] from " & tableNames & " Where rownum=1 and " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME > SYSDATE - 1 group by " + tableAlias.Split(",")(0) + ".PERIOD_START_TIME"
                            End If
                            Dim result As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connectionName, testStr)

                            If (result IsNot Nothing AndAlso result.Rows.Count > 0) Then
                                Return True
                            ElseIf (result Is Nothing) Then
                                Return False
                            Else
                                XtraMessageBox.Show("Query did not return any data")
                                Return True
                            End If
                        End If 'DataBase Check End  
                    End If 'tableKey , connectionName and DataBase is Nothing End
                    Return False
                Else
                    SetMessage("Table is not in Table Grid so Not able to find connection string")
                End If   ' Table is Nothing End
            End If 'IsKPIFurmulaValid
        Catch ex As Exception
            XtraMessageBox.Show("There is some problem with query. Error: " & ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        Return Nothing
    End Function

    Private Function IsKPIFurmulaValid(ByVal kpiFurmila As String) As Boolean
        Dim isMatchAggregate As Boolean = False
        kpiFurmila = Replace(kpiFurmila.ToLower, "round", "")
        If (kpiFurmila.IndexOf("(") > 0) Then

            For Each lst As String In lstAggregateFunction.Items
                If kpiFurmila.ToUpper.Contains(lst.ToUpper().Replace("()", "(")) Then
                    isMatchAggregate = True
                    Exit For
                End If
            Next
            If (isMatchAggregate) Then
                If Not (kpiFurmila.Length - kpiFurmila.Replace("(", "").Length = kpiFurmila.Length - kpiFurmila.Replace(")", "").Length) Then
                    XtraMessageBox.Show("Query does not having matching Brackets.")
                    Return False
                Else
                    Return True
                End If
            Else
                XtraMessageBox.Show("Query does not seem to start with a Aggregate Function")
                Return False
            End If
        Else
            For Each lst As String In lstAggregateFunction.Items
                If kpiFurmila.ToUpper.Contains(Replace(lst.ToUpper, "()", "")) Then
                    isMatchAggregate = True
                    Exit For
                End If
            Next
            If (isMatchAggregate) Then
                Return True
            Else
                XtraMessageBox.Show("Query does not seem to start with a Aggregate Function ")
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ValidateKPIFurmula(ByVal kpiFurmila As String, ByVal kpiName As String) As String
        Dim kpiTestStr As String = kpiFurmila
        Dim index As Integer = kpiTestStr.LastIndexOf(kpiName)
        If (index >= 0) Then
            kpiTestStr = kpiTestStr.Substring(0, index).TrimEnd()
        End If
        Return kpiTestStr.TrimEnd("[")
    End Function

    Private Function GetValueIfNull(ByVal kpiformula As String) As String
        Try
            Dim str As String = kpiformula.Split(",").Last.Split(")").First
            If IsNumeric(str) Then
                Return str
            ElseIf Not kpiformula.StartsWith("ISNULL") Or Not kpiformula.StartsWith("COALESCE") Then
                Return ""
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub CommitStaticKPI()
        If (gvTableCounter.RowCount > 1) Then
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            lblMessage.Text = ""
            Dim commitStr As String
            Dim kpiFormula As String = txtKPIFormula.Text.Trim()
            Dim kpiName As String = gvKpiList.GetFocusedRowCellValue("KPI_Name")
            Dim kpiId As String = gvKpiList.GetFocusedRowCellValue("SQLKPI_ID")
            Dim tableKey As String = Nothing
            Dim KpiSQL As String = Nothing
            Dim connectionName As String = Nothing
            Dim dataBaseName As String = Nothing
            Dim JoinObject As String = ""
            Dim megaQuery As String = ""
            Dim firstTable As DataRow = Nothing

            If gvTableCounter.GetSelectedRows().Count > 0 Then
                firstTable = TryCast(gvTableCounter.GetRow(gvTableCounter.GetSelectedRows()(0)), DataRowView).Row
            End If

            Dim tableNames As String = firstTable.Item(0).ToString

            Dim tablenames_original As String = tableNames.TrimEnd(",")
            tableNames = Replace(tableNames.TrimEnd(","), "<AggregatedObject>", cmbObjectList.Text)
            Dim dtRow() As DataRow

            Dim tableCount As Integer = tableNames.GetCountItems(",")
            Dim isUpdate As DialogResult = XtraMessageBox.Show("Do you want to Update Static KPI?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (isUpdate = DialogResult.Yes) Then
                Dim tableKPI As String = firstTable.Item(0).ToString
                Dim tableAlias As String = firstTable.Item(1).ToString

                If (tableKPI.Length > 0 AndAlso tableAlias.Length > 0) Then
                    Dim tableN As String = tableKPI
                    Dim tAlias As String = Nothing

                    dtRow = dtTablesAndCounters.Select("TableName ='" & tableN & "'")
                    If (dtRow.Length > 0) Then
                        tableAlias = dtRow(0)(6).ToString()
                        InsertItemInUsingTableTLV(tableN, tableAlias)
                        SetRowInDTUsingTable(dtRow(0)(1).ToString(), dtRow(0)(3).ToString(), dtRow(0)(4).ToString(), dtRow(0)(5).ToString(), dtRow(0)(6).ToString(), dtRow(0)(7).ToString())
                        If (dtRow(0)(5).ToString() = dbMSSQL) Then
                            kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL
                        ElseIf (dtRow(0)(5).ToString() = dbORACLE) Then
                            kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE
                        End If
                    End If
                    tableNames = GetUsingAllTableNames(tableKey, connectionName, dataBaseName, tAlias, "ByCommit", JoinObject, megaQuery)
                End If

                If Not (kpiDataBaseName = IOS.Library.KPIDataBaseName.None) Then
                    If (tableKey IsNot Nothing AndAlso connectionName IsNot Nothing AndAlso dataBaseName IsNot Nothing) Then
                        If (kpiDataBaseName = IOS.Library.KPIDataBaseName.MSSQL) Then
                            KpiSQL = "(" + kpiFormula + ") [" + kpiName + "]"
                        ElseIf (kpiDataBaseName = IOS.Library.KPIDataBaseName.ORACLE) Then
                            KpiSQL = "(" + kpiFormula + ") [" + kpiName + "]"
                        End If
                        If (tableCount > 1) Then
                            commitStr = "Update [dbo].[IOS_SQL_KPI] set [tech]='" & cmbTechnology.SelectedItem.ToString & "',[sourcetable]='" & tablenames_original & "',[tablealias]='" & tableAlias.TrimEnd(",") & "',[supportcode]=1,[KPI_Name]='" & kpiName & "',[KPI_SQL]='" & KpiSQL & "',[JoinObjects]='" & JoinObject + ", PERIOD_START_TIME" & "',[Object]='" & cmbObjectList.SelectedItem.ToString & "',[Creator]='" & Environment.UserName & "', [Active]=1,[Description]='" & txtKPIDescription.Text.Trim & "' where [tech]='" & cmbTechnology.SelectedItem.ToString & "' and [SQLKPI_ID]='" & kpiId & "'"
                        Else
                            commitStr = "Update [dbo].[IOS_SQL_KPI] set [tech]='" & cmbTechnology.SelectedItem.ToString & "',[sourcetable]='" & tablenames_original & "',[tablealias]='" & tableAlias.TrimEnd(",") & "',[supportcode]=1,[KPI_Name]='" & kpiName & "',[KPI_SQL]='" & KpiSQL & "',[JoinObjects]='',[Object]='" & cmbObjectList.SelectedItem.ToString & "',[Creator]='" & Environment.UserName & "',[Active]=1,[Description]='" & txtKPIDescription.Text.Trim & "' where [tech]='" & cmbTechnology.SelectedItem.ToString & "' and [SQLKPI_ID]='" & kpiId & "'"
                        End If

                        Dim result As Integer = IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, commitStr)
                        If (result > 0) Then
                            SetMessage("Static KPI Successfully Updated")
                        Else
                            SetMessage("KPI Not Updated")
                            objfrmTechnology = Nothing
                            If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(cmbTechnology.Text)) Then
                                frmMDI.OpenTechFormDynamically(cmbTechnology.Text, objfrmTechnology, False)
                            Else
                                objfrmTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(cmbTechnology.Text)).LastOrDefault()
                            End If
                            objfrmTechnology.FiltersInitialize()
                        End If
                    Else
                        SetMessage("Sorry DataBase Not Selected")
                    End If
                    tlvUsingTableName.Refresh()
                    tlvUsingTableName.UpdateLayout()
                End If
            End If
        Else
            SetMessage("Sorry No any table in Table Grid")
        End If
    End Sub

    Private Function ValidateControls() As Boolean
        If (cmbTechnology.SelectedIndex > 0) Then
            If (cmbObjectList.SelectedIndex > 0) Then
                If (gvKpiList.RowCount > 0) Then
                    If Not (txtKPIFormula.Text = "") Then
                        If (tlvUsingTableName.Nodes.Count > 0) Then
                            If IsNumeric(txtValueIfNull.Text) Then
                                Return True
                            Else
                                SetMessage("Value if no data = Skipped")
                                Return True
                            End If
                        Else
                            SetMessage("Tables Not in Use")
                            Return False
                        End If
                    Else
                        SetMessage("Enter Any Formula")
                        Return False
                    End If
                Else
                    SetMessage("Select Any KPI")
                    Return False
                End If
            Else
                SetMessage("Select Object Name")
                Return False
            End If
        Else
            SetMessage("Select Technology Name")
            Return False
        End If
    End Function

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

#End Region

End Class