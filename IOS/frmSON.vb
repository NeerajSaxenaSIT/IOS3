Imports System.ComponentModel
Imports System.Data.DataTableExtensions
Imports System.Threading
Imports DevExpress.Data
Imports DevExpress.XtraGrid
Imports dotnetCHARTING.WinForms
Imports IOS.Configuration
Imports IOS.DataLibrary
Imports IOS.Library
Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraEditors
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraReports.UI
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class frmSON

#Region "Variables"

    Public networksAll As NetworksAll = Nothing
    Private ds_IOS_ObjectTypes As DataSet = Nothing
    Private dtJobIncon As DataTable = Nothing
    Private dtResultsData As DataTable = Nothing

    Private connectionString As String
    Private cn_SON_Incon As Odbc.OdbcConnection

    Private sCommand_SON_Incon_Queries As Odbc.OdbcCommand
    Private sAdapter_SON_Incon_Queries As Odbc.OdbcDataAdapter

    Private sCommand_SON_Incon_Variables As Odbc.OdbcCommand
    Private sAdapter_SON_Incon_Variables As Odbc.OdbcDataAdapter

    Private sCommand_SON_Incon_Exceptions As Odbc.OdbcCommand
    Private sAdapter_SON_Incon_Exceptions As Odbc.OdbcDataAdapter

    Private SuperDataGrid_Inconsist_ColumnOfCellClicked As String
    Private IsFirstTimeLoading As Boolean = False

    Private objThreadRunManual As Thread
    Private Delegate Sub CallThreadInvokedJobRunManual(ByRef nd As TreeListViewNode, Status As Integer)
    Private objRunManualThreadLock As New Object
    Private dtSONReports As DataTable = Nothing

#End Region

#Region "Form & Control Events"

    Private Sub frmSON_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Get list of technologies...
        tlvSONIncon.Cursor = Cursors.Default

        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Jobs_Load_Inconsist()
            Dim parray()() As String = {New String() {"@Nothing", ""}}
            Dim connstring As String = GetSQL(9317, parray, dt_IOS_SQL)(0)
            tsmi_SON_Incon_Thematic.SelectedItem = tsmi_SON_Incon_Thematic.Items(2)
            xTabPageSONNB.PageVisible = False
            ds_IOS_ObjectTypes = clsSQLCommands.GetObjectConfigurationData(connstring)
            Try
                For Each dr As DataRow In dt_IOS_ObjectConfig.Select("ParamTune = 1", "Tech")
                    Dim liTech As New clsComboBoxItem()
                    liTech.Value = dr("Tech").ToString
                    liTech.Text = dr("Tech").ToString
                    If Not cmbTuningTech.Properties.Items.Contains(liTech) Then
                        cmbTuningTech.Properties.Items.Add(liTech)
                    End If
                Next
            Catch
            End Try
            ConfigurSONForm("frmSON")

            btnCreateReportSON.Enabled = Enabled
            btnReportDesignerSON.Enabled = Enabled
            btnDeleteReportSON.Enabled = Enabled
            cmbModuleReports.Enabled = Enabled
            btnReportSetDefaultSON.Enabled = Enabled

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub cmbSONInconCharts_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSONInconCharts.SelectedValueChanged
        Try
            Application.UseWaitCursor = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tbll As TableLayoutPanel = New TableLayoutPanel
            tbll.Dock = DockStyle.Top
            tbll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.Percent, 100))
            tbll.ColumnCount = 1

            tlpInconsistReports.Controls.Remove(tlpInconsistReports.GetControlFromPosition(0, 1))
            tlpInconsistReports.Controls.Add(tbll, 0, 1)

            tbll.AutoScroll = True

            Try
                tbll.AutoSize = True
                tbll.Controls.Clear()
                tbll.RowStyles.Clear()
                tbll.RowCount = 0
            Catch ex As Exception
            End Try


            If cmbSONInconCharts.SelectedIndex <> -1 Then
                'query chartset in IOS_Jobs_Charts
                Dim dt_sql As DataTable = clsSQLCommands.Get_IOS_Jobs_Charts(connStrIOSServer, CInt(tlvSONIncon.SelectedNode.Text), cmbSONInconCharts.SelectedItem.ToString)
                For Each dr As DataRow In dt_sql.Rows
                    Dim ch As Chart = CreateSingleChart(dr)
                    If Not ch Is Nothing Then
                        tbll.RowCount = tbll.RowCount + 1
                        tbll.RowStyles.Add(New System.Windows.Forms.RowStyle(SizeType.Absolute, 400))
                        tbll.Size = New System.Drawing.Size(CInt(SuperTabPageIncCharts.Width - 10), (tbll.RowCount) * 400)

                        ch.Width = CInt(SuperTabPageIncCharts.Width - 10)
                        ch.Height = 400
                        ch.Dock = DockStyle.Fill
                        tbll.Controls.Add(ch, 0, tbll.RowCount - 1)
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Application.UseWaitCursor = False
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnSONInconSave_Click(sender As Object, e As EventArgs) Handles btnSONInconSave.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            'updating queries
            Dim cmdbuilder As New Odbc.OdbcCommandBuilder(sAdapter_SON_Incon_Queries)
            Dim i As Integer
            cmbSONInconCharts.SelectedItem = Nothing
            Try
                i = sAdapter_SON_Incon_Queries.Update(CType(gcSONInconQueries.DataSource, DataSet), "JobQueries")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            'updating Variables
            Dim cmdbuilder2 As New Odbc.OdbcCommandBuilder(sAdapter_SON_Incon_Variables)
            Dim j As Integer
            Try
                j = sAdapter_SON_Incon_Variables.Update(CType(gcSONInconVariables.DataSource, DataSet), "JobVariables")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            btnSONInconSave.ForeColor = System.Drawing.Color.Black

            'updating Exceptions
            Dim cmdbuilder3 As New Odbc.OdbcCommandBuilder(sAdapter_SON_Incon_Exceptions)
            Dim M As Integer
            Try
                M = sAdapter_SON_Incon_Exceptions.Update(CType(gcSONInconExceptions.DataSource, DataSet), "JobExceptions")
                If M > 0 Then
                    tlvSONIncon_SubItemSelectionChanged(Nothing, Nothing)
                    SuperTabControlInconsist_SelectedIndexChanged(Nothing, Nothing)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            btnSONInconSave.ForeColor = System.Drawing.Color.Black
            MsgBox("Records Updated Queries: " & i & vbCrLf & "Records Updates Variables: " & j & vbCrLf & "Records Updates Exceptions: " & M)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub SuperTabControlInconsist_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SuperTabControlInconsist.SelectedPageChanged
        Application.DoEvents()
        Application.UseWaitCursor = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If tlvSONIncon.SelectedNode IsNot Nothing Then
                Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)

                If SuperTabControlInconsist.SelectedTabPage.Text = "Results" Then
                    If dgvResults.Tag <> jobid Then
                        Dim outputType As Integer = 1
                        dlgJobsAdd.SelectedJobID = jobid

                        FillJobRunID(jobid)
                        FillJobResultsGrid(jobid)
                        'Else
                        '    If SuperTabControlInconsist.SelectedTabPage.Text = "Results" And dgvResults.DataSource Is Nothing Then
                        '        Dim outputType As Integer = 1
                        '        dlgJobsAdd.SelectedJobID = jobid

                        '        FillJobRunID(jobid)
                        '        FillJobResultsGrid(jobid)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        Application.UseWaitCursor = False
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub gvSONInconQueries_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles gvSONInconQueries.CellValueChanged
        btnSONInconSave.ForeColor = System.Drawing.Color.Red
    End Sub

    Private Sub gvSONInconVariables_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles gvSONInconVariables.CellValueChanged
        btnSONInconSave.ForeColor = System.Drawing.Color.Red
    End Sub

    Private Sub gvSONInconQueries_KeyDown(sender As Object, e As KeyEventArgs) Handles gvSONInconQueries.KeyDown, gvSONInconVariables.KeyDown, gvSONInconExceptions.KeyDown
        If e.KeyData = Keys.Delete Then
            Dim gv As Views.Grid.GridView = CType(sender, Views.Grid.GridView)
            Dim rowIndex() As Integer = gv.GetSelectedRows()
            For i As Integer = rowIndex.Length - 1 To 0 Step -1
                gv.DeleteRow(rowIndex(i))
                gv.RefreshData()
            Next
        End If
    End Sub

    Private Sub tsmi_SON_Incon_dgvResult_Mark_Click(sender As Object, e As EventArgs) Handles tsmi_SON_Incon_dgvResult_Mark.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim rnd As Random = New Random()
            Dim cellObj() As Views.Base.GridCell
            cellObj = gvResult.GetSelectedCells
            For i As Integer = 0 To cellObj.Length - 1
                Dim appr As New DevExpress.Utils.AppearanceObject()
                appr.BackColor = System.Drawing.Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))
                Dim obj As New Views.Grid.RowCellStyleEventArgs(cellObj(i).RowHandle, cellObj(i).Column, Views.Base.GridRowCellState.Selected, appr)
                gvResult.SelectCell(cellObj(i))
            Next
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SON_Incon_MapAll_Click(sender As Object, e As EventArgs) Handles tsmi_SON_Incon_MapAll.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If dtResultsData IsNot Nothing Then
                Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
                Dim jobname As String = Replace(tlvSONIncon.SelectedNode.SubItems(1).Text, " ", "_")
                Dim dt_jobs As DataTable = CType(tlvSONIncon.Tag, DataTable)
                Dim dr() As DataRow = dt_jobs.Select("jobid=" & jobid)
                'get thematic
                frmMapWindow.MapDataToSingleLayer(dtResultsData, "SON_" & jobname, dr(0)("InCon_SourceField"), dr(0)("InCon_MapField"), tsmi_SON_Incon_Thematic.Text, SuperDataGrid_Inconsist_ColumnOfCellClicked, dr(0)("InCon_InfoTip").ToString)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SON_Incon_MapFiltered_Click(sender As Object, e As EventArgs) Handles tsmi_SON_Incon_MapFiltered.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If dtResultsData IsNot Nothing Then
                Dim filteredDataView As New DataView(dtResultsData)
                filteredDataView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(gvResult.ActiveFilterCriteria)

                Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
                Dim jobname As String = Replace(tlvSONIncon.SelectedNode.SubItems(1).Text, " ", "_")
                Dim dt_jobs As DataTable = CType(tlvSONIncon.Tag, DataTable)
                Dim dr() As DataRow = dt_jobs.Select("jobid=" & jobid)
                'get thematic
                frmMapWindow.MapDataToSingleLayer(filteredDataView.ToTable(), "SON_" & jobname, dr(0)("InCon_SourceField"), dr(0)("InCon_MapField"), tsmi_SON_Incon_Thematic.Text, SuperDataGrid_Inconsist_ColumnOfCellClicked, dr(0)("InCon_InfoTip").ToString)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SON_Incon_MapSelect_Click(sender As Object, e As EventArgs) Handles tsmi_SON_Incon_MapSelect.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If dtResultsData IsNot Nothing Then
                Dim dt As DataTable = dtResultsData.Clone
                Dim rowIndex() As Integer
                rowIndex = gvResult.GetSelectedRows()
                For i As Integer = 0 To rowIndex.Length - 1
                    Dim dr2 As DataRow = dt.NewRow()
                    For Each col As DevExpress.XtraGrid.Columns.GridColumn In gvResult.Columns
                        dr2(col.AbsoluteIndex) = gvResult.GetRowCellValue(rowIndex(i), col)
                    Next
                    dt.Rows.Add(dr2)
                Next
                dt.AcceptChanges()

                Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
                Dim jobname As String = Replace(tlvSONIncon.SelectedNode.SubItems(1).Text, " ", "_")
                Dim dt_jobs As DataTable = CType(tlvSONIncon.Tag, DataTable)
                Dim dr() As DataRow = dt_jobs.Select("jobid=" & jobid)
                'get thematic
                frmMapWindow.MapDataToSingleLayer(dt, "SON_" & jobname, IIf(IsDBNull(dr(0)("InCon_SourceField")), "", CStr(dr(0)("InCon_SourceField"))), IIf(IsDBNull(dr(0)("InCon_MapField")), "", CStr(dr(0)("InCon_MapField"))), tsmi_SON_Incon_Thematic.Text, SuperDataGrid_Inconsist_ColumnOfCellClicked, IIf(IsDBNull(dr(0)("InCon_InfoTip")), "", CStr(dr(0)("InCon_InfoTip"))))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub NSNRAML20ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NSNRAML20ToolStripMenuItem.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If dtResultsData IsNot Nothing Then
                If XML_Parameters_Validation("NSN - RAML2.0", dtResultsData) Then
                    Dim saveFileDialog1 As New SaveFileDialog()
                    saveFileDialog1.Filter = "XML|*.xml"
                    saveFileDialog1.Title = "Save an XML File"
                    saveFileDialog1.ShowDialog()

                    ' If the file name is not an empty string open it for saving.
                    If saveFileDialog1.FileName <> "" Then
                        Dim success As Boolean = XML_Parameters_NSN(saveFileDialog1.FileName, dtResultsData)
                        If success Then
                            MsgBox("Export Success!")
                        End If
                    End If
                Else
                    MsgBox("Input Table does not contain all columns: Object_DN, Object_GID, ShortName, DefaultValue", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub NSNRAML20ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NSNRAML20ToolStripMenuItem1.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If dtResultsData IsNot Nothing Then
                Dim dt As DataTable = dtResultsData.Clone
                Dim rowIndex() As Integer
                rowIndex = gvResult.GetSelectedRows()

                For i As Integer = 0 To rowIndex.Length - 1
                    Dim dr2 As DataRow = dt.NewRow()
                    For Each col As DevExpress.XtraGrid.Columns.GridColumn In gvResult.Columns
                        dr2(col.AbsoluteIndex) = gvResult.GetRowCellValue(rowIndex(i), col)
                    Next
                    dt.Rows.Add(dr2)
                Next

                If XML_Parameters_Validation("NSN - RAML2.0", dt) Then
                    Dim saveFileDialog1 As New SaveFileDialog()
                    saveFileDialog1.Filter = "XML|*.xml"
                    saveFileDialog1.Title = "Save an XML File"
                    saveFileDialog1.ShowDialog()

                    ' If the file name is not an empty string open it for saving.
                    If saveFileDialog1.FileName <> "" Then
                        Dim success As Boolean = XML_Parameters_NSN(saveFileDialog1.FileName, dt)
                        If success Then
                            MsgBox("Export Success!")
                        End If
                    End If
                Else
                    MsgBox("Input Table does not contain all columns: Object_DN, Object_GID, ShortName, DefaultValue", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub NSNRAML20ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles NSNRAML20ToolStripMenuItem2.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If dtResultsData IsNot Nothing Then
                Dim filteredDataView As New DataView(dtResultsData)
                filteredDataView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(gvResult.ActiveFilterCriteria)

                If XML_Parameters_Validation("NSN - RAML2.0", filteredDataView.ToTable()) Then
                    Dim saveFileDialog1 As New SaveFileDialog()
                    saveFileDialog1.Filter = "XML|*.xml"
                    saveFileDialog1.Title = "Save an XML File"
                    saveFileDialog1.ShowDialog()

                    ' If the file name is not an empty string open it for saving.
                    If saveFileDialog1.FileName <> "" Then
                        Dim success As Boolean = XML_Parameters_NSN(saveFileDialog1.FileName, filteredDataView.ToTable())
                        If success Then
                            MsgBox("Export Success!")
                        End If
                    End If
                Else
                    MsgBox("Input Table does not contain all columns: Object_DN, Object_GID, ShortName, DefaultValue", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiAllowCellCopy_CheckedChanged(sender As Object, e As EventArgs) Handles tsmiAllowCellCopy.CheckedChanged
        Try
            Dim tempGrid As GridControl = frmMapWindow.GetAttachedGrid(sender)
            If tempGrid IsNot Nothing Then
                Dim gridView As Views.Grid.GridView = tempGrid.MainView
                If tsmiAllowCellCopy.Checked Then
                    gridView.OptionsSelection.MultiSelectMode = Views.Grid.GridMultiSelectMode.CellSelect
                Else
                    gridView.OptionsSelection.MultiSelectMode = Views.Grid.GridMultiSelectMode.RowSelect
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmiCopyAllToCSV_Click(sender As Object, e As EventArgs) Handles tsmiCopyAllToCSV.Click
        Try
            btnCSV_Click(Me.btnCSV, Nothing)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmiCopySelectionToClipboard_Click(sender As Object, e As EventArgs) Handles tsmiCopySelectionWOHeader.Click
        Try
            IOSDevExpressGrid.CopyGridDataToClipBoard(dgvResults, gvResult, False, False, {"ROWHASH"})
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmiCopySelectionWithHeader_Click(sender As Object, e As EventArgs) Handles tsmiCopySelectionWithHeader.Click
        Try
            IOSDevExpressGrid.CopyGridDataToClipBoard(dgvResults, gvResult, False, True, {"ROWHASH"})
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmiCopyFilteredToClipboard_Click(sender As Object, e As EventArgs) Handles tsmiCopyFilteredToClipboard.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            Using dtTemp As DataTable = CreateData(jobid, 0, 0, currViewRowFilter)
                If dtTemp IsNot Nothing Then
                    IOSDevExpressGrid.PopulateDataInGrid(gcTemp, gvTemp, dtTemp, "ALL", {"RowHash"})
                    IOSDevExpressGrid.CopyGridDataToClipBoard(gcTemp, gvTemp, True, True, {"ROWHASH"})
                    IOSDevExpressGrid.ClearGrid(gcTemp)
                End If
            End Using
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiExportAllToExcel_Click(sender As Object, e As EventArgs) Handles tsmiExportAllToExcel.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)

            Dim savefiledialog1 As New SaveFileDialog()
            savefiledialog1.FileName = ""
            savefiledialog1.Filter = "Excel Workbook |*.xlsx"
            If savefiledialog1.ShowDialog <> DialogResult.OK Then
                Exit Sub
            End If
            Dim fp As String = savefiledialog1.FileName

            Using dtTemp As DataTable = CreateData(jobid, 0, 0)
                If dtTemp IsNot Nothing Then
                    'IOSDevExpressGrid.PopulateDataInGrid(gcTemp, gvTemp, dtTemp, "ALL", {"RowHash"})
                    'IOSDevExpressGrid.ExportDataGridToExcel(gcTemp)
                    'IOSDevExpressGrid.ClearGrid(gcTemp)

                    ExportDataTableToExcel_OpenXML(dtTemp, fp)

                    MsgBox("Export Completed!")
                    Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(fp))
                End If
            End Using
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Shared Sub ExportDataTableToExcel_OpenXML(ByRef table As DataTable, destination As String)
        Try
            Using workbook = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook)
                Dim workbookPart = workbook.AddWorkbookPart()

                workbook.WorkbookPart.Workbook = New Workbook()
                workbook.WorkbookPart.Workbook.Sheets = New Sheets()

                Dim sheetPart = workbook.WorkbookPart.AddNewPart(Of WorksheetPart)()
                Dim sheetData = New SheetData()
                sheetPart.Worksheet = New Worksheet(sheetData)

                Dim sheets As Sheets = workbook.WorkbookPart.Workbook.GetFirstChild(Of Sheets)()
                Dim relationshipId As String = workbook.WorkbookPart.GetIdOfPart(sheetPart)

                Dim sheetId As UInteger = 1

                If sheets.Elements(Of Sheet)().Count() > 0 Then
                    sheetId = sheets.Elements(Of Sheet)().Select(Function(s) s.SheetId.Value).Max() + 1
                End If

                Dim sheet As New Sheet() With {
                    .Id = relationshipId,
                    .SheetId = sheetId,
                    .Name = table.TableName
                }
                sheets.Append(sheet)

                Dim headerRow As New Row()

                Dim columns As New List(Of String)()
                For Each column As DataColumn In table.Columns
                    columns.Add(column.ColumnName)

                    Dim cell As New Cell()
                    cell.DataType = CellValues.String
                    cell.CellValue = New CellValue(column.ColumnName)
                    headerRow.AppendChild(cell)
                Next

                sheetData.AppendChild(headerRow)

                For Each dsrow As DataRow In table.Rows
                    Dim newRow As New Row()
                    For Each col As String In columns
                        Dim cell As New Cell()

                        If table.Columns(col).DataType = GetType(System.DateTime) Then
                            cell.DataType = CellValues.String
                            If regionalSettings = True Then
                                cell.CellValue = New CellValue(DirectCast(dsrow(col), DateTime).ToString())
                            Else
                                cell.CellValue = New CellValue(CDate(dsrow(col)).ToString("yyyy-MM-dd HH:mm:ss"))
                            End If
                        Else
                            cell.DataType = CellValues.String
                            cell.CellValue = New CellValue(dsrow(col).ToString())
                        End If

                        newRow.AppendChild(cell)
                    Next
                    sheetData.AppendChild(newRow)
                Next
            End Using
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub dbtn_Tuning_CommitJob_Click(sender As Object, e As EventArgs) Handles btnTuningCommitJob.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            'checking input
            If tlvTuneJobs.SelectedNode Is Nothing Then
                gbTuneJobs.ForeColor = System.Drawing.Color.Red
                gbTuneJobs.BackColor = System.Drawing.Color.Orange
                Exit Sub
            End If
            If TreeView_CountCheckedAll(tvTuningObjects.Nodes(0)) = 0 Then
                Exit Sub
            End If
            If tlvTuningKPI.VisibleCount = 0 Then
                Exit Sub
            End If
            If tlvTuningParameter.VisibleCount = 0 Then
                Exit Sub
            End If

            'inserting
            '- insert objects
            '----------------
            Dim seq As Integer = 1
            Dim selectedobjs() As String = Split(Replace(TreeView_Checked2String("Tuning", cmbTuningOT.SelectedItem.ToString, "ObjectID", tvTuningObjects, cmbTuningOT), "IN (", "").TrimEnd(")"), ",")
            Dim selectedobjsIN As String = TreeView_Checked2String("Tuning", cmbTuningOT.SelectedItem.ToString, "Naked", tvTuningObjects, cmbTuningOT)

            Dim JobID As Integer = CInt(tlvTuneJobs.SelectedNode.Text)
            'clearing objects of selected job
            clsSQLCommands.Delete_Tune_Objects(connStrIOSServer, JobID)

            Dim m As Integer = 0
            For Each objs As String In selectedobjs
                '-insert query
                clsSQLCommands.Add_Tune_Objects(connStrIOSServer, JobID, cmbTuningTech.SelectedItem.ToString, cmbTuningOT.SelectedItem.ToString, selectedobjsIN.Split(",")(m))
                m = m + 1
            Next

            '- insert exceptions
            clsSQLCommands.Delete_Tune_ObjectsExceptions(connStrIOSServer, JobID)

            For Each li As DevExpress.XtraEditors.Controls.ListBoxItem In lbTuneObjectExceptions.Items
                '-insert query
                clsSQLCommands.Add_Tune_ObjectsExceptions(connStrIOSServer, JobID, CLng(li.Tag), li.Value.ToString)
            Next

            '- insert KPI
            clsSQLCommands.Delete_Tune_KPI(connStrIOSServer, JobID)
            Dim kpis As String = "IN("
            Dim kpis_avg As String = ""
            Dim kpis_filter_red As String = ""
            Dim kpis_filter_green As String = ""
            Dim kpis_filter_stay As String = ""
            Dim interval As Integer
            Dim breachgreen As Integer = 0
            Dim breachred As Integer = 0
            Try
                For Each nd As TreeListViewNode In tlvTuningKPI.Nodes
                    Dim paramslist As New List(Of Odbc.OdbcParameter)
                    paramslist.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
                    paramslist.Add(New Odbc.OdbcParameter("KPIID", CInt(nd.SubItems(0).Text)))
                    paramslist.Add(New Odbc.OdbcParameter("KPIName", nd.SubItems(1).Text))
                    paramslist.Add(New Odbc.OdbcParameter("KPIInterval", CType(nd.SubItems(6).Text, Integer)))
                    paramslist.Add(New Odbc.OdbcParameter("KPIOperatorGreen", nd.SubItems(2).Text))
                    paramslist.Add(New Odbc.OdbcParameter("KPIThresholdGreen", CType(nd.SubItems(3).Text, Double)))
                    paramslist.Add(New Odbc.OdbcParameter("KPIOperatorRed", nd.SubItems(4).Text))
                    paramslist.Add(New Odbc.OdbcParameter("KPIThresholdRed", CType(nd.SubItems(5).Text, Double)))
                    paramslist.Add(New Odbc.OdbcParameter("breachgreen", CType(nd.SubItems(7).Text, Integer)))
                    paramslist.Add(New Odbc.OdbcParameter("breachRed", CType(nd.SubItems(8).Text, Integer)))

                    Dim sql_insert_kpi As String = "EXEC IOS_Tune_KPI_Add ?, ?, ?, ?, ?, ?, ? , ?, ?, ?"
                    Dim ds As DataSet = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql_insert_kpi, paramslist)

                    kpis = kpis & Chr(39) & nd.SubItems(1).Text & Chr(39) & ","
                    kpis_avg = kpis_avg + "AVG(" + nd.SubItems(1).Text + ") AvgOf" + nd.SubItems(1).Text & ","
                    kpis_filter_red = kpis_filter_red + nd.SubItems(1).Text + nd.SubItems(4).Text + nd.SubItems(5).Text & " AND "
                    kpis_filter_green = kpis_filter_green + nd.SubItems(1).Text + nd.SubItems(2).Text + nd.SubItems(3).Text & " AND "

                    Select Case kpis_filter_red.Contains(">")
                        Case True
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_red, ">", "<")
                        Case False
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_red, "<", ">")
                    End Select
                    Select Case kpis_filter_green.Contains(">")
                        Case True
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_green, ">", "<")
                        Case False
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_green, "<", ">")
                    End Select

                    interval = CInt(nd.SubItems(6).Text)
                    breachgreen = nd.SubItems(7).Text
                    breachred = nd.SubItems(8).Text
                Next
                kpis = kpis.TrimEnd(",") + ")"
                kpis_avg = kpis_avg.TrimEnd(",")
                kpis_filter_green = kpis_filter_green.Substring(0, Len(kpis_filter_green) - 5).Replace(",", ".")
                kpis_filter_red = kpis_filter_red.Substring(0, Len(kpis_filter_red) - 5).Replace(",", ".")
                kpis_filter_stay = kpis_filter_stay.Substring(0, Len(kpis_filter_stay) - 5).Replace(",", ".")
            Catch
            End Try

            'clearing job details queries
            clsSQLCommands.Delete_Jobs_Details(connStrIOSServer, JobID)

            '-- generate KPI sql
            Dim aggr_from As String
            Dim aggr_to As String

            aggr_from = cmbTuneParamObject.Text
            aggr_to = cmbTuningOT.Text
            Dim stross As String = ""

            Try
                stross = clsSQLCommands.Get_CounterTables(connStrIOSServer, cmbTuningTech.Text.ToUpper).Tables(0)(0)(0).ToString
            Catch
            End Try

            Dim sql_kpis As String = SQL_Construct_KPI_Only(cmbTuningTech.Text, aggr_from, aggr_to, kpis, interval, "Daily", selectedobjsIN)
            Dim paramslist2 As New List(Of Odbc.OdbcParameter)
            paramslist2.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
            paramslist2.Add(New Odbc.OdbcParameter("Type", CType("Load", String)))
            paramslist2.Add(New Odbc.OdbcParameter("Sequence", CInt(seq)))
            paramslist2.Add(New Odbc.OdbcParameter("SourceConn", stross))
            paramslist2.Add(New Odbc.OdbcParameter("sql", sql_kpis))
            paramslist2.Add(New Odbc.OdbcParameter("destinationtable", CType("Job" & JobID & "_KPIs", String)))
            paramslist2.Add(New Odbc.OdbcParameter("timeout", CType(300, Integer)))

            Dim ds2 As DataSet = clsSQLCommands.Add_Jobs_Details(connStrIOSServer, CInt(JobID), CType("Load", String), CInt(seq), stross, sql_kpis, CType("Job" & JobID & "_KPIs", String))

            seq = seq + 1

            '- insert Param
            clsSQLCommands.Delete_Tune_Parameter(connStrIOSServer, JobID)
            Dim params As New List(Of String)
            Dim objtypes As New List(Of String)
            Dim ActionsRed As New List(Of Integer)
            Dim ActionsGreen As New List(Of Integer)
            Dim UpperLimit As New List(Of String)
            Dim LowerLimit As New List(Of String)
            Dim StepSize As New List(Of Double)
            Dim allparams As New List(Of String)

            Try
                For Each nd As TreeListViewNode In tlvTuningParameter.Nodes
                    Dim paramslist3 As New List(Of Odbc.OdbcParameter)
                    paramslist3.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
                    paramslist3.Add(New Odbc.OdbcParameter("ParentParamID", nd.SubItems(0).Text))
                    paramslist3.Add(New Odbc.OdbcParameter("ParameterID", nd.SubItems(0).Text))
                    paramslist3.Add(New Odbc.OdbcParameter("ObjectType", nd.SubItems(1).Text))
                    paramslist3.Add(New Odbc.OdbcParameter("ParameterName", nd.SubItems(2).Text))
                    paramslist3.Add(New Odbc.OdbcParameter("StepSize", CDbl(nd.SubItems(3).Text)))
                    paramslist3.Add(New Odbc.OdbcParameter("ActionGreen", nd.SubItems(5).Text))
                    paramslist3.Add(New Odbc.OdbcParameter("ActionRed", nd.SubItems(4).Text))
                    paramslist3.Add(New Odbc.OdbcParameter("UpperLimit", nd.SubItems(6).Text))
                    paramslist3.Add(New Odbc.OdbcParameter("LowerLimit", nd.SubItems(7).Text))

                    Dim ds3 As DataSet = clsSQLCommands.Add_Tune_Parameter(connStrIOSServer, CInt(JobID), nd.SubItems(0).Text, nd.SubItems(0).Text, nd.SubItems(1).Text, nd.SubItems(2).Text, CDbl(nd.SubItems(3).Text), nd.SubItems(5).Text, nd.SubItems(4).Text, nd.SubItems(6).Text, nd.SubItems(7).Text)

                    allparams.Add(nd.SubItems(2).Text)
                    params.Add(nd.SubItems(2).Text)
                    objtypes.Add(nd.SubItems(1).Text.Trim)
                    If nd.SubItems(5).Text.ToLower.Trim = "increase" Then
                        ActionsGreen.Add(1)
                    ElseIf nd.SubItems(5).Text.ToLower.Trim = "decrease" Then
                        ActionsGreen.Add(-1)
                    Else
                        ActionsGreen.Add(-999)
                    End If

                    If nd.SubItems(4).Text.ToLower.Trim = "increase" Then
                        ActionsRed.Add(1)
                    ElseIf nd.SubItems(4).Text.ToLower.Trim = "decrease" Then
                        ActionsRed.Add(-1)
                    Else
                        ActionsRed.Add(-999)
                    End If

                    UpperLimit.Add(nd.SubItems(6).Text)
                    LowerLimit.Add(nd.SubItems(7).Text)
                    StepSize.Add(CDbl(nd.SubItems(3).Text))
                    If nd.Nodes.Count > 0 Then
                        For Each nds As TreeListViewNode In nd.Nodes
                            allparams.Add(nds.SubItems(1).Text)
                            Dim paramslist4 As New List(Of Odbc.OdbcParameter)
                            paramslist4.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
                            paramslist4.Add(New Odbc.OdbcParameter("ParentParamID", nd.SubItems(0).Text))
                            paramslist4.Add(New Odbc.OdbcParameter("ParameterID", nds.SubItems(0).Text))
                            paramslist4.Add(New Odbc.OdbcParameter("ObjectType", nds.SubItems(1).Text))
                            paramslist4.Add(New Odbc.OdbcParameter("ParameterName", nds.SubItems(2).Text))
                            paramslist4.Add(New Odbc.OdbcParameter("StepSize", CDbl(nds.SubItems(3).Text)))
                            paramslist4.Add(New Odbc.OdbcParameter("ActionGreen", nds.SubItems(5).Text))
                            paramslist4.Add(New Odbc.OdbcParameter("ActionRed", nds.SubItems(4).Text))
                            paramslist4.Add(New Odbc.OdbcParameter("UpperLimit", nds.SubItems(6).Text))
                            paramslist4.Add(New Odbc.OdbcParameter("LowerLimit", nds.SubItems(7).Text))

                            Dim ds4 As DataSet = clsSQLCommands.Add_Tune_Parameter(connStrIOSServer, CInt(JobID), nd.SubItems(0).Text, nd.SubItems(0).Text, nd.SubItems(1).Text, nd.SubItems(2).Text, CDbl(nd.SubItems(3).Text), nd.SubItems(5).Text, nd.SubItems(4).Text, nd.SubItems(6).Text, nd.SubItems(7).Text)
                        Next
                    End If
                Next
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            End Try

            Try
                Dim sqlset() As String = Tune_Parameter_SQL()
                Dim ds3 As DataSet = clsSQLCommands.Add_Jobs_Details(connStrIOSServer, CInt(JobID), CType("Load", String), CInt(seq), stross, sql_kpis, CType("Job" & JobID & "_KPIs", String))
                seq = seq + 1
            Catch
                Exit Sub
            End Try

            'filter sql's
            'KPI - red set
            Dim sql_red As String
            Dim sql_green As String
            Dim sql_stay As String

            sql_red = "SELECT * FROM (SELECT CELLID, LAC, SITE, PARENT,  GID, " & kpis_avg & ", COUNT(*) as CountRed FROM Job" & JobID & "_KPIs " _
                & " LEFT OUTER JOIN dbo.IOS_Tune_ObjectsExceptions ON Job" & JobID & "_KPIs.GID = dbo.IOS_Tune_ObjectsExceptions.ObjectID " _
                & " WHERE " & kpis_filter_red & " AND dbo.IOS_Tune_ObjectsExceptions.ObjectID IS NULL  GROUP BY CELLID, LAC,SITE, PARENT,  GID ) as DerivTBL where CountRed >= " & breachred
            sql_green = "SELECT * FROM (SELECT CELLID, LAC, SITE, PARENT,  GID, " & kpis_avg & ", COUNT(*) as CountGreen FROM Job" & JobID & "_KPIs " _
                & " LEFT OUTER JOIN dbo.IOS_Tune_ObjectsExceptions ON Job" & JobID & "_KPIs.GID = dbo.IOS_Tune_ObjectsExceptions.ObjectID " _
                & " WHERE " & kpis_filter_green & " AND dbo.IOS_Tune_ObjectsExceptions.ObjectID IS NULL  GROUP BY CELLID, LAC,SITE, PARENT,  GID ) as DerivTBL where CountGreen >= " & breachgreen
            sql_stay = "SELECT CELLID, LAC, SITE, PARENT,  GID, " & kpis_avg & ", COUNT(*) as CountStay FROM Job" & JobID & "_KPIs " _
                & " LEFT OUTER JOIN dbo.IOS_Tune_ObjectsExceptions ON Job" & JobID & "_KPIs.GID = dbo.IOS_Tune_ObjectsExceptions.ObjectID " _
                & " WHERE " & kpis_filter_stay & " AND dbo.IOS_Tune_ObjectsExceptions.ObjectID IS NULL  GROUP BY CELLID, LAC, SITE, PARENT,  GID "

            Dim paramslist5 As New List(Of Odbc.OdbcParameter)
            paramslist5.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
            paramslist5.Add(New Odbc.OdbcParameter("Type", CType("Filter", String)))
            paramslist5.Add(New Odbc.OdbcParameter("Sequence", CInt(seq)))
            paramslist5.Add(New Odbc.OdbcParameter("SourceConn", stross))
            paramslist5.Add(New Odbc.OdbcParameter("sql", sql_red))
            paramslist5.Add(New Odbc.OdbcParameter("destinationtable", CType("Job" & JobID & "_KPIred", String)))
            paramslist5.Add(New Odbc.OdbcParameter("timeout", CType(300, Integer)))

            Dim sql_job_sql_load As String = "EXEC IOS_Jobs_Details_Add ?, ?, ?, ?, ?, ?, ?"
            ds2 = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql_job_sql_load, paramslist5)
            seq = seq + 1

            Dim paramslist6 As New List(Of Odbc.OdbcParameter)
            paramslist6.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
            paramslist6.Add(New Odbc.OdbcParameter("Type", CType("Filter", String)))
            paramslist6.Add(New Odbc.OdbcParameter("Sequence", CInt(seq)))
            paramslist6.Add(New Odbc.OdbcParameter("SourceConn", stross))
            paramslist6.Add(New Odbc.OdbcParameter("sql", sql_green))
            paramslist6.Add(New Odbc.OdbcParameter("destinationtable", CType("Job" & JobID & "_KPIgreen", String)))
            paramslist6.Add(New Odbc.OdbcParameter("timeout", CType(300, Integer)))
            ds2 = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql_job_sql_load, paramslist6)

            seq = seq + 1
            Dim paramslist7 As New List(Of Odbc.OdbcParameter)
            paramslist7.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
            paramslist7.Add(New Odbc.OdbcParameter("Type", CType("Filter", String)))
            paramslist7.Add(New Odbc.OdbcParameter("Sequence", CInt(seq)))
            paramslist7.Add(New Odbc.OdbcParameter("SourceConn", stross))
            paramslist7.Add(New Odbc.OdbcParameter("sql", sql_stay))
            paramslist7.Add(New Odbc.OdbcParameter("destinationtable", CType("Job" & JobID & "_KPIstay", String)))
            paramslist7.Add(New Odbc.OdbcParameter("timeout", CType(300, Integer)))
            ds2 = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql_job_sql_load, paramslist7)
            seq = seq + 1

            'final table
            Dim sql_params As String = ""
            For i As Integer = 0 To params.Count - 1
                Dim equalsign(2) As String
                If ActionsRed(i) = "1" Then
                    equalsign(0) = " "
                    equalsign(1) = "= "
                Else
                    equalsign(0) = "= "
                    equalsign(1) = " "
                End If
                If ActionsGreen(i) <> -999 And ActionsRed(i) <> -999 Then
                    sql_params = params(i) & " AS OldParamValue, ( CASE  " &
                                                "WHEN COALESCE(CountRed,0)-COALESCE(CountGreen,0) > COALESCE(Countstay,0) THEN " &
                                                    "( CASE  WHEN " & params(i) & " <" & equalsign(0) & UpperLimit(i) & " AND " & params(i) & " >" & equalsign(1) & LowerLimit(i) & " THEN (" & params(i) & " + " & StepSize(i) & " * " & ActionsRed(i) & ") ELSE " & params(i) & " END) " &
                                                "WHEN COALESCE(CountGreen,0) - COALESCE(CountRed,0) > COALESCE(Countstay,0) THEN " &
                                                    "( CASE  WHEN " & params(i) & " >" & equalsign(0) & LowerLimit(i) & " AND " & params(i) & " <" & equalsign(1) & UpperLimit(i) & " THEN (" & params(i) & " + " & StepSize(i) & " * " & ActionsGreen(i) & ") ELSE " & params(i) & " END) " &
                                                "ELSE " & params(i) & " " &
                                                "END) AS NewParamValue, " &
                                                "(CASE  " &
                                                "WHEN COALESCE(CountRed,0) - COALESCE(CountGreen,0) > COALESCE(Countstay,0) THEN 'Red' " &
                                                "WHEN COALESCE(CountGreen,0) - COALESCE(CountRed,0) > COALESCE(Countstay,0) THEN 'Green' " &
                                                "ELSE 'Stay' " &
                                                "END) AS ActionType "

                ElseIf ActionsGreen(i) = -999 And ActionsRed(i) = -999 Then
                    sql_params = params(i) & " AS OldParamValue, ( CASE  " &
                                                " WHEN COALESCE(CountRed,0) - COALESCE(CountGreen,0) > COALESCE(Countstay,0) THEN " & UpperLimit(i) &
                                                " WHEN COALESCE(CountGreen,0) - COALESCE(CountRed,0) > COALESCE(Countstay,0) THEN " & LowerLimit(i) &
                                                " END) AS NewParamValue, " &
                                                "(CASE  " &
                                                "WHEN COALESCE(CountRed,0)-COALESCE(CountGreen,0) > COALESCE(Countstay,0) THEN 'Red' " &
                                                "WHEN COALESCE(CountGreen,0)-COALESCE(CountRed,0) > COALESCE(Countstay,0) THEN 'Green' " &
                                                "ELSE 'Stay' " &
                                                "END) AS ActionType "
                End If

                Dim sql_final As String = "INSERT INTO IOS_Tune_Result (JobRunID, JobID, Tech, ObjectType, PARENT, GID, CELLID, LAC, DN,  ParameterName, OldParamValue, NewParamValue, ActionType) " &
                " (SELECT * FROM (" &
                            " SELECT @uniquejob AS JobRunID, " & JobID & " AS JobID, " & Chr(39) & cmbTuningTech.Text & Chr(39) & " Tech," & Chr(39) & cmbTuneParamObject.Text & Chr(39) & " ObjectType,   Job" & JobID & "_ParamLoad.PARENT, Job" & JobID & "_ParamLoad.GID, Job" & JobID & "_ParamLoad.CELLID, Job" & JobID & "_ParamLoad.LAC, Job" & JobID & "_ParamLoad.DN, " & Chr(39) & params(i) & Chr(39) & " ParameterName, " + sql_params &
                            " FROM Job" & JobID & "_ParamLoad " &
                            " LEFT OUTER JOIN Job" & JobID & "_KPIgreen ON Job" & JobID & "_ParamLoad.GID = Job" & JobID & "_KPIgreen.GID " &
                            " LEFT OUTER JOIN Job" & JobID & "_KPIred ON Job" & JobID & "_ParamLoad.GID = Job" & JobID & "_KPIred.GID " &
                            " LEFT OUTER JOIN Job" & JobID & "_KPIstay ON Job" & JobID & "_ParamLoad.GID = Job" & JobID & "_KPIstay.GID )" &
                            " AS DERIVEDTBL" &
                " WHERE (OldParamValue <> NewParamValue))"

                Dim paramslist8 As New List(Of Odbc.OdbcParameter)
                paramslist8.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
                paramslist8.Add(New Odbc.OdbcParameter("Type", CType("Final", String)))
                paramslist8.Add(New Odbc.OdbcParameter("Sequence", CInt(seq)))
                paramslist8.Add(New Odbc.OdbcParameter("SourceConn", stross))
                paramslist8.Add(New Odbc.OdbcParameter("sql", sql_final))
                paramslist8.Add(New Odbc.OdbcParameter("destinationtable", CType("IOS_Tune_Result", String)))
                paramslist8.Add(New Odbc.OdbcParameter("timeout", CType(300, Integer)))
                ds2 = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql_job_sql_load, paramslist8)

                seq = seq + 1
                If tlvTuningParameter.Nodes(i).Nodes.Count > 0 Then
                    For Each nds As TreeListViewNode In tlvTuningParameter.Nodes(i).Nodes
                        Dim subparams As String = nds.SubItems(2).Text.Trim & " AS OldParamValue, ( CASE  " &
                                                " WHEN IOS_Tune_Result.ActionType =  'Red' THEN " & nds.SubItems(6).Text &
                                                " WHEN IOS_Tune_Result.ActionType =  'Green' THEN " & nds.SubItems(7).Text &
                                                " END) AS NewParamValue "

                        Dim sql_subparams As String = "INSERT INTO IOS_Tune_Result (IOS_Tune_Result.JobRunID, IOS_Tune_Result.JobID, IOS_Tune_Result.Tech, IOS_Tune_Result.ObjectType, IOS_Tune_Result.PARENT, IOS_Tune_Result.GID, IOS_Tune_Result.CELLID, IOS_Tune_Result.LAC, IOS_Tune_Result.DN, ParameterName, OldParamValue, NewParamValue, ActionType) " &
                            " SELECT IOS_Tune_Result.JobRunID,  IOS_Tune_Result.JobID, IOS_Tune_Result.Tech, IOS_Tune_Result.ObjectType, IOS_Tune_Result.PARENT, IOS_Tune_Result.GID, IOS_Tune_Result.CELLID, IOS_Tune_Result.LAC, IOS_Tune_Result.DN, " & Chr(39) & nds.SubItems(2).Text.Trim & Chr(39) & "AS ParameterName, " + subparams + ", ActionType " &
                                        " FROM IOS_Tune_Result INNER JOIN Job" & JobID & "_ParamLoad ON IOS_Tune_Result.GID = Job" & JobID & "_ParamLoad.GID" &
                                        " WHERE IOS_Tune_Result.JobID = " & JobID & " AND IOS_Tune_Result.JobRunID = @uniquejob AND IOS_Tune_Result.ParameterName = " & Chr(39) & params(i) & Chr(39)

                        Dim paramslist9 As New List(Of Odbc.OdbcParameter)
                        paramslist9.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
                        paramslist9.Add(New Odbc.OdbcParameter("Type", CType("Final", String)))
                        paramslist9.Add(New Odbc.OdbcParameter("Sequence", CInt(seq)))
                        paramslist9.Add(New Odbc.OdbcParameter("SourceConn", stross))
                        paramslist9.Add(New Odbc.OdbcParameter("sql", sql_subparams))
                        paramslist9.Add(New Odbc.OdbcParameter("destinationtable", CType("IOS_Tune_Result", String)))
                        paramslist9.Add(New Odbc.OdbcParameter("timeout", CType(300, Integer)))
                        ds2 = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql_job_sql_load, paramslist9)
                        seq = seq + 1
                    Next
                End If
            Next

            Dim dr() As DataRow = dt_IOS_ObjectConfig.Select("Tech = '" & cmbTuningTech.Text & "'")
            Dim vendor As String = dr(0)("Vendor").ToString
            Dim sql_xml As String = ""
            Select Case vendor
                Case "HUAWEI"
                    vendor = "MML_HUAWEI"
                    sql_xml = "SELECT JobRunID, GID, PARENT, CELLID, ParameterName, NewParamValue FROM IOS_Tune_Result WHERE JobRunID = @uniquejob ORDER BY DN ASC"
                Case "NOKIA"
                    vendor = "XML_NSN"
                    sql_xml = "SELECT JobRunID, GID, DN, ParameterName, NewParamValue FROM IOS_Tune_Result WHERE JobRunID = @uniquejob ORDER BY DN ASC"
            End Select
            'generate XML

            Try
                sql_job_sql_load = "EXEC IOS_Jobs_Details_Add " & JobID & ", '" & vendor & "', " & seq & ", " & Chr(39) & connStrIOSServer & Chr(39) & ", " & Chr(39) & Replace(sql_xml, "'", "''") & Chr(39) & ", " & Chr(39) & " " & Chr(39) & ",300"
                DataAccessorSQL.ExecuteNonQuery(connStrIOSServer, sql_job_sql_load)
                seq = seq + 1
            Catch
            End Try

            Try
                sql_job_sql_load = "EXEC IOS_Jobs_Details_Add ?, ?, ?, ?, ?, ?, ?"
                Dim sql_tune_tracking As String = "INSERT INTO dbo.IOS_Tune_Result_Tracking (JobID, JobRunID) VALUES(" & JobID & ",@uniquejob)"
                Dim paramslist10 As New List(Of Odbc.OdbcParameter)
                paramslist10.Add(New Odbc.OdbcParameter("JobID", CInt(JobID)))
                paramslist10.Add(New Odbc.OdbcParameter("Type", CType("Final", String)))
                paramslist10.Add(New Odbc.OdbcParameter("Sequence", CInt(seq)))
                paramslist10.Add(New Odbc.OdbcParameter("SourceConn", stross))
                paramslist10.Add(New Odbc.OdbcParameter("sql", sql_tune_tracking))
                paramslist10.Add(New Odbc.OdbcParameter("destinationtable", CType("IOS_Tune_Result_Tracking", String)))
                paramslist10.Add(New Odbc.OdbcParameter("timeout", CType(300, Integer)))
                ds2 = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql_job_sql_load, paramslist10)
                seq = seq + 1
            Catch
            End Try

            Jobs_Load_Param()
        Catch ex As Exception
            MsgBox("Job Commit Failed - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub dbtn_Tune_KPIPreview_Click(sender As Object, e As EventArgs) Handles btnTuneKPIPreview.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If tlvTuneJobs.SelectedNode Is Nothing Then
                gbTuneJobs.ForeColor = System.Drawing.Color.Red
                gbTuneJobs.BackColor = System.Drawing.Color.Orange
                Exit Sub
            End If

            Dim kpis As String = "IN("
            Dim kpis_avg As String = ""
            Dim kpis_filter_red As String = ""
            Dim kpis_filter_green As String = ""
            Dim kpis_filter_stay As String = ""
            Dim interval As Integer
            Dim breachgreen As Integer = 0
            Dim breachred As Integer = 0
            Try
                For Each nd As TreeListViewNode In tlvTuningKPI.Nodes

                    kpis = kpis & Chr(39) & nd.SubItems(1).Text & Chr(39) & ","
                    kpis_avg = kpis_avg + "AVG(" + nd.SubItems(1).Text + ") AvgOf" + nd.SubItems(1).Text & ","
                    kpis_filter_red = kpis_filter_red + nd.SubItems(1).Text + nd.SubItems(4).Text + nd.SubItems(5).Text & " AND "
                    kpis_filter_green = kpis_filter_green + nd.SubItems(1).Text + nd.SubItems(2).Text + nd.SubItems(3).Text & " AND "

                    Select Case kpis_filter_red.Contains(">")
                        Case True
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_red, ">", "<")
                        Case False
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_red, "<", ">")
                    End Select
                    Select Case kpis_filter_green.Contains(">")
                        Case True
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_green, ">", "<")
                        Case False
                            kpis_filter_stay = kpis_filter_stay + Replace(kpis_filter_green, "<", ">")
                    End Select

                    interval = CInt(nd.SubItems(6).Text)
                    breachgreen = nd.SubItems(7).Text
                    breachred = nd.SubItems(8).Text
                Next
                kpis = kpis.TrimEnd(",") + ")"
                kpis_avg = kpis_avg.TrimEnd(",")
                kpis_filter_green = kpis_filter_green.Substring(0, Len(kpis_filter_green) - 5).Replace(",", ".")
                kpis_filter_red = kpis_filter_red.Substring(0, Len(kpis_filter_red) - 5).Replace(",", ".")
                kpis_filter_stay = kpis_filter_stay.Substring(0, Len(kpis_filter_stay) - 5).Replace(",", ".")
            Catch
            End Try

            Dim sqlset As String = SQL_Construct_KPI_Only(cmbTuningTech.Text, cmbTuneParamObject.Text, cmbTuningOT.Text, kpis, txtTuningKPIInterval.Text, "Daily", "")

            Dim sql_red As String
            Dim sql_green As String
            Dim sql_stay As String

            sql_red = "SELECT * FROM (SELECT CELLID, SITE, PARENT,  GID, " & kpis_avg & ", COUNT(*) as CountBreachRed FROM (" & sqlset & ") Job_KPIs  WHERE " & kpis_filter_red & "  GROUP BY CELLID, SITE, PARENT,  GID ) as DerivTBL where CountBreachRed >= " & breachred
            sql_green = "SELECT * FROM (SELECT CELLID, SITE, PARENT,  GID, " & kpis_avg & ", COUNT(*) as CountBreachGreen FROM (" & sqlset & ") Job_KPIs  WHERE " & kpis_filter_green & "  GROUP BY CELLID, SITE, PARENT,  GID ) as DerivTBL where CountBreachGreen >= " & breachgreen
            sql_stay = "SELECT CELLID, SITE, PARENT,  GID, " & kpis_avg & ", COUNT(*) as CountStay FROM (" & sqlset & ") Job_KPIs WHERE " & kpis_filter_stay & "   GROUP BY CELLID, SITE, PARENT,  GID "


            Dim ds_preview_red As DataSet = DataAccessorODBC.GetDataSet(connectionString, sql_red)
            Dim ds_preview_green As DataSet = DataAccessorODBC.GetDataSet(connectionString, sql_green)
            Dim ds_preview_stay As DataSet = DataAccessorODBC.GetDataSet(connectionString, sql_stay)

            Dim ds_preview As DataSet = ds_preview_red
            ds_preview.Tables(0).Merge(ds_preview_green.Tables(0))
            ds_preview.Tables(0).Merge(ds_preview_stay.Tables(0))


            If Not ds_preview Is Nothing Then
                With gcTuneKPIPreview
                    '.AllowContextMenuFiltering = True
                    '.ColumnsHierarchy.Filters.Clear()
                    '.Clear()
                    .ContextMenuStrip.AllowMerge = True
                    .DataSource = ds_preview.Tables(0)
                    .Refresh()
                    ' .ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                    For k = 0 To gvTuneKPIPreview.Columns.Count - 1
                        gvTuneKPIPreview.Columns.Item(k).FilterMode = ColumnFilterMode.Value
                    Next
                End With
                XTabControlParamStep2.SelectedTabPage = xTabPageTuneKPIPreview
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub dbtn_Tuning_AddKPI_Click(sender As Object, e As EventArgs) Handles btnTuningAddKPI.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If cmbTuningKPI.Text = "" Then
                Exit Sub
            End If
            If cmbTuningOpGreen.Text = "" Then
                Exit Sub
            End If
            If txtTuningKPITresholdGreen.Text = "" Then
                Exit Sub
            End If
            Tune_KPI_Insert(cmbTuningKPI.Text, cmbTuningKPI.Text, cmbTuningOpGreen.Text, CDbl(txtTuningKPITresholdGreen.Text), cmbTuningOpRed.Text, CDbl(txtTuningKPITresholdRed.Text), CInt(txtTuningKPIInterval.Text), CInt(txtTuningBreachGreen.Text), CInt(txtTuningBreachRed.Text))
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub dbtn_Tune_ParamPreview_Click(sender As Object, e As EventArgs) Handles btnTuneParamPreview.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim sqlset() As String = Tune_Parameter_SQL()
            Dim ds_preview As DataSet = DataAccessorODBC.GetDataSet(sqlset(0), sqlset(1))
            If Not ds_preview Is Nothing Then

                With gcTuneParamPreview
                    gvTuneParamPreview.ClearColumnsFilter()
                    .ContextMenuStrip.AllowMerge = True
                    .DataSource = ds_preview.Tables(0)
                    .Refresh()
                    gvTuneParamPreview.BestFitColumns(True)
                    For k = 0 To gvTuneParamPreview.Columns.Count - 1
                        gvTuneParamPreview.Columns.Item(k).OptionsFilter.AllowFilter = True
                    Next
                End With

                gvTuneParamPreview.OptionsView.ColumnAutoWidth = True
                XTabControlParamStep2.SelectedTabPage = xTabPageTuneParamPreview
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub dbtn_Tuning_AddParamMove_Click(sender As Object, e As EventArgs) Handles btnTuningAddParamMove.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If cmbTuningParameter.Text = "" Then
                Exit Sub
            End If

            Try
                If CInt(txtTuningStepSize.Text) = 0 Then
                    MsgBox("Stepsize Incorrect")
                    Exit Sub
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                MsgBox("Stepsize Incorrect")
                Exit Sub
            End Try

            If cmbTuningActionGreen.Text = "" Then
                Exit Sub
            End If
            If cmbTuningActionRed.Text = "" Then
                Exit Sub
            End If
            Try
                If CInt(txtTuningLowerLimit.Text) > CInt(txtTuningUpperLimit.Text) Then
                    MsgBox("LowerLimit/UpperLimit/SetValue Incorrect")
                    Exit Sub
                End If
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                MsgBox("LowerLimit/UpperLimit/SetValue Incorrect")
                Exit Sub
            End Try
            If tlvTuningParameter.SelectedNode Is Nothing Then
                Exit Sub
            End If

            Tune_Parameter_Insert_Sub(tlvTuningParameter.SelectedNode, cmbTuningParameter.Text, cmbTuningParameter.SelectedValue, cmbTuneParamObject.SelectedItem.ToString.Trim, txtTuningStepSize.Text, cmbTuningActionGreen.Text, cmbTuningActionRed.Text, txtTuningUpperLimit.Text, txtTuningLowerLimit.Text)

            tlvTuningParameter.AutoSizeColumn(tlvTuningParameter.Columns(0))
            tlvTuningParameter.UpdateLayout()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub dcmb_Tune_ParamObject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTuneParamObject.SelectedIndexChanged
        Tune_ParameterList_Update()
    End Sub

    Private Sub cmb_Tuning_Tech_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTuningTech.SelectedIndexChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            'update cmb_kpi
            Dim tech As String = cmbTuningTech.Text
            If cmbTuningTech.Text = "" Then
                Exit Sub
            End If

            'Add items to Object Type
            cmbTuningOT.Properties.Items.Clear()
            ObjectTree_LoadTypes(ds_IOS_ObjectTypes.Tables(0), cmbTuningOT, tech)

            cmbTuneParamObject.Properties.Items.Clear()
            For Each dr As DataRow In dt_IOS_ObjectConfig.Select("Tech = '" & tech & "' AND ParamTune = 1")
                cmbTuneParamObject.Properties.Items.Add(dr("Object").ToString)
            Next
            If cmbTuningOT.Properties.Items.Count > 0 Then
                cmbTuningOT.SelectedItem = cmbTuningOT.Properties.Items(0)
                cmbTuneParamObject.SelectedItem = cmbTuneParamObject.Properties.Items(0)
            Else
                Exit Sub
            End If

            Dim parray()() As String = {New String() {"@tech", Chr(39) & tech & Chr(39)}, New String() {"@object", Chr(39) & cmbTuneParamObject.Text & Chr(39)}}
            Dim dtQODBC As DataTable = Nothing
            cmbTuningKPI.Properties.Items.Clear()
            cmbTuningKPI.Text = ""

            Dim connstring As String = GetSQL(9200, parray, dt_IOS_SQL)(0)
            Dim sql As String = GetSQL(9200, parray, dt_IOS_SQL)(1)

            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)
            BindDevExComboBoxWithValueMember(cmbTuningKPI, dtQODBC, "SQLKPI_ID", "KPI_Name", "Select Item...")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub dcmb_Tuning_OT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTuningOT.SelectedIndexChanged
        If Not cmbTuningOT.SelectedItem Is Nothing Then
            'Fill_TreeviewStatsTune(cmbTuningOT.SelectedItem.ToString)
            FillObjectTreeData(tvTuningObjects, cmbTuningTech.SelectedItem.ToString, cmbTuningOT.SelectedItem.ToString)
        End If
    End Sub

    Private Sub tlv_Tune_Jobs_Click(sender As Object, e As EventArgs) Handles tlvTuneJobs.Click
        If Not tlvTuneJobs.SelectedSubItem Is Nothing Then
            gbTuneJobs.ForeColor = System.Drawing.Color.DarkGray
            gbTuneJobs.BackColor = System.Drawing.Color.Transparent
            tlv_Tune_Jobs_SubItemSelectionChanged(Nothing, Nothing)
            tlvTuneJobs.Columns(2).FormatStyle.ContentAlign = HorizontalAlignment.Center
            dlgJobsAdd.SelectedJobID = CInt(tlvTuneJobs.SelectedNode.SubItems(0).Text)
        End If
    End Sub

    Private Sub tlv_Tune_Jobs_SubItemSelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlvTuneJobs.SubItemSelectionChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim sql_select As String = Nothing
            Dim jobid As Integer = CInt(tlvTuneJobs.SelectedNode.Text)
            Dim dt As New DataTable
            'query objects

            sql_select = "SELECT * FROM IOS_Tune_Objects WHERE JobID = " & jobid
            dt = DataAccessorODBC.GetDataTable(connStrIOSServer, sql_select)

            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then
                    cmbTuningTech.SelectedIndex = Nothing
                    cmbTuningOT.SelectedIndex = Nothing
                    tvTuningObjects.Nodes.Clear()
                    tlvTuningKPI.Nodes.Clear()
                    tlvTuningParameter.Nodes.Clear()
                End If
                For Each it As ListViewItem In cmbTuningTech.Properties.Items
                    If it.Text.ToUpper = dt(0)(2).ToString.TrimEnd.ToUpper Then
                        cmbTuningTech.SelectedItem = it
                    End If
                Next

                For Each it As ListViewItem In cmbTuningOT.Properties.Items
                    If it.Text.ToUpper = dt(0)(3).ToString.TrimEnd.ToUpper Then
                        cmbTuningOT.SelectedItem = it
                    End If
                Next

                TreeView_ClearChecks(tvTuningObjects.Nodes(0))
                For Each dr As DataRow In dt.Rows
                    Dim tv_result As TreeNode = Treeview_TextSearch(dr(4).ToString.Trim, tvTuningObjects.Nodes, True)
                    If Not tv_result Is Nothing Then
                        tv_result.Checked = True
                    End If
                Next
            End If
            dt = Nothing
            'dt.Dispose()

            'query object exceptions
            lbTuneObjectExceptions.Items.Clear()
            sql_select = "SELECT * FROM IOS_Tune_ObjectsExceptions WHERE JobID = " & jobid
            dt = DataAccessorODBC.GetDataTable(connStrIOSServer, sql_select)
            For Each dr As DataRow In dt.Rows
                Dim li As ListViewItem = New ListViewItem()
                li.Text = dr(3).ToString.Trim
                li.Tag = dr(2).ToString.Trim
                lbTuneObjectExceptions.Items.Add(li)
            Next
            lbTuneObjectExceptions.Update()

            'KPI
            sql_select = "SELECT * FROM IOS_Tune_KPI WHERE JobID = " & jobid

            If Not dt Is Nothing Then
                tlvTuningKPI.Nodes.Clear()

                For Each dr As DataRow In dt.Rows
                    Tune_KPI_Insert(dr(3).ToString, dr(2).ToString, dr(5).ToString, dr(6).ToString, dr(7).ToString, dr(8).ToString, dr(4).ToString, dr(9).ToString, dr(10).ToString)
                Next
            End If

            'Parameter
            dt = clsSQLCommands.Get_IOS_Tune_Parameter_Data(connStrIOSServer, jobid)
            If Not dt Is Nothing Then
                tlvTuningParameter.Nodes.Clear()

                For Each dr As DataRow In dt.Rows
                    If dr(3).ToString.Trim = dr(4).ToString.Trim Then
                        Tune_Parameter_Insert(dr(5).ToString, CInt(dr(3).ToString), dr(2).ToString, dr(6).ToString, dr(7).ToString, dr(8).ToString, dr(9).ToString, dr(10).ToString)
                    Else
                        Tune_Parameter_Insert_Sub(tlvTuningParameter.FindNode(dr(3).ToString.Trim, ListSearchCriteria.byKey), dr(5).ToString, CInt(dr(4).ToString), dr(2).ToString, dr(6).ToString, dr(7).ToString, dr(8).ToString, dr(9).ToString, dr(10).ToString)
                    End If
                Next
                tlvTuningParameter.ExpandAll()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            tlvTuningKPI.Nodes.Clear()
            tlvTuningParameter.Nodes.Clear()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tlv_Tuning_KPI_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvTuningKPI.SubItemSelectionChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            cmbTuningKPI.SelectedItem = GetComboItemFromValue(CInt(tlvTuningKPI.SelectedNode.SubItems(0).Text.ToString), cmbTuningKPI)
            For Each it As clsComboBoxItem In cmbTuningOpGreen.Properties.Items
                If it.Text = tlvTuningKPI.SelectedNode.SubItems(2).Text.Trim.ToLower Then
                    cmbTuningOpGreen.SelectedItem = it
                    Exit For
                End If
            Next
            For Each it As clsComboBoxItem In cmbTuningOpRed.Properties.Items
                If it.Text = tlvTuningKPI.SelectedNode.SubItems(4).Text.Trim.ToLower Then
                    cmbTuningOpRed.SelectedItem = it
                    Exit For
                End If
            Next

            txtTuningKPITresholdGreen.Text = tlvTuningKPI.SelectedNode.SubItems(3).Text.Trim
            txtTuningKPITresholdRed.Text = tlvTuningKPI.SelectedNode.SubItems(5).Text.Trim
            txtTuningKPIInterval.Text = tlvTuningKPI.SelectedNode.SubItems(6).Text.Trim
            txtTuningBreachGreen.Text = tlvTuningKPI.SelectedNode.SubItems(7).Text.Trim
            txtTuningBreachRed.Text = tlvTuningKPI.SelectedNode.SubItems(8).Text.Trim
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tlv_Tuning_Parameter_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvTuningParameter.SubItemSelectionChanged
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            cmbTuneParamObject.SelectedItem = cmbTuneParamObject.Properties.Items(0)
            cmbTuningParameter.SelectedValue = CInt(tlvTuningParameter.SelectedNode.SubItems(0).Text.ToString)
            txtTuningStepSize.Text = tlvTuningParameter.SelectedNode.SubItems(3).Text.ToString

            Select Case tlvTuningParameter.SelectedNode.SubItems(5).Text.Trim.ToLower
                Case "increase"
                    cmbTuningActionGreen.SelectedItem = cmbTuningActionGreen.Properties.Items(0)
                Case "decrease"
                    cmbTuningActionGreen.SelectedItem = cmbTuningActionGreen.Properties.Items(1)
                Case "set value"
                    cmbTuningActionGreen.SelectedItem = cmbTuningActionGreen.Properties.Items(2)
            End Select
            Select Case tlvTuningParameter.SelectedNode.SubItems(4).Text.Trim.ToLower
                Case "increase"
                    cmbTuningActionRed.SelectedItem = cmbTuningActionRed.Properties.Items(0)
                Case "decrease"
                    cmbTuningActionRed.SelectedItem = cmbTuningActionRed.Properties.Items(1)
                Case "set value"
                    cmbTuningActionRed.SelectedItem = cmbTuningActionRed.Properties.Items(2)
            End Select
            txtTuningLowerLimit.Text = tlvTuningParameter.SelectedNode.SubItems(7).Text.Trim
            txtTuningUpperLimit.Text = tlvTuningParameter.SelectedNode.SubItems(6).Text.Trim
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub xTabControl_ParamTune_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xTabControlParamTune.SelectedPageChanged
        'refresh combobox
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If xTabControlParamTune.SelectedTabPage.Text = "Analyse" Then
                cmbTuneAnalyseJob.Properties.Items.Clear()
                Dim dATA As DataSet = clsSQLCommands.Get_IOS_Jobs_Data(connStrIOSServer)

                cmbTuneAnalyseJob.Properties.Items.Clear()
                If (dATA IsNot Nothing) Then
                    If (dATA.Tables.Count > 0) Then
                        cmbTuneAnalyseJob.SuspendLayout()
                        cmbTuneAnalyseJob.Properties.Items.Insert(0, "Select Job")
                        ''cmbTuneAnalyseJob.Properties.Items(0).IsChecked = True
                        For Each Item As DataRow In dATA.Tables(0).Rows
                            cmbTuneAnalyseJob.Properties.Items.Add(Item("JobName").ToString.Trim)
                        Next
                        cmbTuneAnalyseJob.SelectedIndex = 0
                        cmbTuneAnalyseJob.Refresh()
                        cmbTuneAnalyseJob.ResumeLayout()
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub dcmb_Tune_Analyse_Job_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTuneAnalyseJob.SelectedIndexChanged
        Try
            If cmbTuneAnalyseJob.SelectedIndex = 0 Then
                Exit Sub
            End If
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            'load data
            Dim sql As String = "  SELECT *  FROM (   select *, count(*) CountOfChanges from (" &
                "SELECT convert(datetime,substring(jobrunid,charindex('_', JobRunID,1)+1,8),112) +  convert(datetime, substring(jobrunid,charindex('_', JobRunID,1)+9,2) + ':' + substring(jobrunid,charindex('_', JobRunID,1)+11,2)  + ':' + substring(jobrunid,charindex('_', JobRunID,1)+13,2)  ,112) Date, [ActionType] " &
                "  FROM [dbo].[IOS_Tune_Result] inner join IOS_Jobs on IOS_Tune_Result.JobID = IOS_Jobs.JobID " &
                "  WHERE JobName = '" & cmbTuneAnalyseJob.Text & " ' ) " &
                "  derivtbl " &
                "  group by [Date], [ActionType] " &
                ") x " &
                "pivot" &
                "(" &
                "  sum(CountOfChanges) " &
                "  for ActionType in ([Green], [Red]) " &
                ") p order by date"

            Dim Data As DataSet = DataAccessorSQL.ExecuteDataSet(connStrIOSServer, sql)

            'set chart
            'check if chart is available
            Dim ch As Chart
            Dim yaxis1 As New Axis
            yaxis1.Orientation = Orientation.Left
            yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked

            If Not tlpAnalyseResults.GetControlFromPosition(0, 0) Is Nothing Then
                ch = CType(tlpAnalyseResults.GetControlFromPosition(0, 0), Chart)
                ch.SeriesCollection.Clear()
                ch.Refresh()
            Else
                ch = New Chart
                tlpAnalyseResults.Controls.Add(ch, 0, 0)
                ch.Dock = DockStyle.Fill
                ch.Width = System.Math.Max(tpTuneAnalyse.Width, 800)

                ch.DefaultElement.Marker.Visible = False
                ch.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
                ch.LegendBox.DefaultEntry.Value = ""
                ch.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
                ch.XAxis.TickLabelMode = TickLabelMode.Angled
                ch.XAxis.TickLabelAngle = 45
                ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
                ch.ToolTip.InitialDelay = 1
                ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
                ch.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
                ch.CleanupPeriod = 1
                AddHandler ch.Click, AddressOf chart_TuneResult_Click

                ch.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            End If

            ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
            ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
            ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
            ch.XAxis.TimeInterval = TimeInterval.Days
            ch.XAxis.FormatString = "dd/MM/yy"
            ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
            ch.XAxis.TimeInterval = TimeInterval.Days
            ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
            ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"


            Dim de As DataEngine = New DataEngine(Data.Tables(0))
            de.DataFields = String2DataFields({"Green", "Red"}, "Date")
            de.DataGridFormatString = "N2"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()

            For i = 0 To sc.Count() - 1
                sc(i).Type = SeriesType.Bar
                sc(i).YAxis = yaxis1
                sc(i).DefaultElement.Marker.Type = i
            Next

            ch.SeriesCollection.Clear()
            ch.SeriesCollection.Add(sc)
            ch.Series.Data = Data.Tables(0)

            'set grid
            If Not Data Is Nothing Then
                With gcAnalyseResults
                    .DataSource = Nothing
                    gvAnalyseResults.Columns.Clear()
                    .Refresh()
                    gvAnalyseResults.ClearColumnsFilter()
                    .DataSource = Data.Tables(0)
                    .Refresh()
                    gvAnalyseResults.OptionsCustomization.AllowColumnResizing = True
                    For k = 0 To gvAnalyseResults.Columns.Count - 1
                        gvAnalyseResults.Columns(k).OptionsFilter.AllowFilter = True
                    Next
                    .Refresh()
                End With
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        Tune_Result_Export_Refresh()
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub chart_TuneResult_Click(ByVal sender As Object, ByVal args As EventArgs)
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim ch As Chart = CType(sender, Chart)
            Dim hit As HitTestInfo = Nothing
            Try
                hit = ch.HitTest()
            Catch ex As Exception
            End Try
            Dim xValue As DateTime = Nothing
            Dim yValue As Double = Nothing
            Dim data As DataSet = Nothing

            If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                Dim el As Element = CType(hit.Object, Element)
                Dim columnName As String = hit.Series.Name
                xValue = el.XDateTime
                yValue = el.YValue
                Dim jobrunid As String = xValue.ToString("yyyyMMddHHmmss")

                data = clsSQLCommands.Get_IOS_Tune_Result_Data(connStrIOSServer, cmbTuneAnalyseJob.Text, jobrunid)
            End If

            'set grid
            If Not data Is Nothing Then
                With gcAnalyseResults
                    .DataSource = Nothing
                    gvAnalyseResults.Columns.Clear()
                    .Refresh()
                    gvAnalyseResults.ClearColumnsFilter()
                    .DataSource = data.Tables(0)
                    .Refresh()
                    gvAnalyseResults.OptionsCustomization.AllowColumnResizing = True
                    For k = 0 To gvAnalyseResults.Columns.Count - 1
                        gvAnalyseResults.Columns(k).OptionsFilter.AllowFilter = True
                    Next
                    .Refresh()
                End With
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Jobs_Add_Click(sender As Object, e As EventArgs) Handles tsmiJobsAdd.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tv As TreeListView = CType(cmJobs.SourceControl, TreeListView)
            If tv.Name = "tlvTuneJobs" Then
                dlgJobsAdd.JobType = "Param"
            Else
                dlgJobsAdd.JobType = "Inconsist"
            End If

            dlgJobsAdd.AddOrUpdate = "Add"
            dlgJobsAdd.ShowDialog()
            Jobs_Load_Param()
            Jobs_Load_Inconsist()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Jobs_Edit_Click(sender As Object, e As EventArgs) Handles tsmi_Jobs_Edit.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tv As TreeListView = CType(cmJobs.SourceControl, TreeListView)
            If tv.Name = "tlvTuneJobs" Then
                dlgJobsAdd.JobType = "Param"
            Else
                dlgJobsAdd.JobType = "Inconsist"
            End If
            dlgJobsAdd.AddOrUpdate = "Update"
            dlgJobsAdd.ShowDialog()
            Jobs_Load_Param()
            Jobs_Load_Inconsist()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_Jobs_Delete_Click(sender As Object, e As EventArgs) Handles tsmi_Jobs_Delete.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim tv As TreeListView = CType(cmJobs.SourceControl, TreeListView)
            If tv.Name = "tlvTuneJobs" Then
                dlgJobsAdd.JobType = "Param"
            Else
                dlgJobsAdd.JobType = "Inconsist"
            End If
            clsSQLCommands.Delete_IOS_Jobs(connStrIOSServer, tlvTuneJobs.SelectedNode.SubItems(0).Text.Trim, Environment.UserName.ToString)
            Jobs_Load_Param()
            Jobs_Load_Inconsist()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ExpandAll_Click(sender As Object, e As EventArgs) Handles tsmi_ExpandAll.Click
        Try
            tlvSONIncon.ExpandAll()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_CollapseAll_Click(sender As Object, e As EventArgs) Handles tsmi_CollapseAll.Click
        Try
            tlvSONIncon.CollapseAll()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SON_Incon_dgvResult_AddEx_Click(sender As Object, e As EventArgs) Handles tsmi_SON_Incon_dgvResult_AddEx.Click
        Try
            Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            dlgSONException.jobid = jobid
            dlgSONException.ShowDialog()

            Call tlvSONIncon_SubItemSelectionChanged(Nothing, Nothing)
            Call SuperTabControlInconsist_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlvSONIncon_SubItemSelectionChanged(sender As Object, e As EventArgs) Handles tlvSONIncon.SubItemSelectionChanged
        Try
            lblResultsMsg.Text = ""
            tlpResultsMain.RowStyles(0).Height = 0

            Application.UseWaitCursor = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info: " & tlvSONIncon.SelectedNode.Text, "Invoked")
            Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            IOSDevExpressGrid.ClearGrid(gcJobRunID)
            gvJobRunID.Tag = jobid
            Try
                Dim sql_select As String = Nothing

                'clearing Charts
                Dim tbll As TableLayoutPanel = CType(cmbSONInconCharts.Parent.Parent, TableLayoutPanel)
                Try

                    Dim Control As Windows.Forms.Control = tbll.GetControlFromPosition(0, 1)
                    If Not Control Is Nothing Then
                        tbll.Controls.Remove(Control)
                    End If
                    cmbSONInconCharts.Properties.Items.Clear()
                    cmbSONInconCharts.SelectedIndex = -1

                    RemoveHandler cmbSONInconCharts.SelectedValueChanged, AddressOf cmbSONInconCharts_SelectedValueChanged
                    Dim sql1 As String = "SELECT DISTINCT ChartCategory from dbo.IOS_Jobs_Charts WHERE JobID=" & jobid
                    Dim dt As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, sql1)
                    BindDevExComboBoxWithValueMember(cmbSONInconCharts, dt, "ChartCategory", "ChartCategory")
                    cmbSONInconCharts.SelectedIndex = -1
                    AddHandler cmbSONInconCharts.SelectedValueChanged, AddressOf cmbSONInconCharts_SelectedValueChanged
                Catch ex As Exception

                End Try

                'Load Results

                'Load Config

                'Loading queries
                Dim Sql_queries As String = "SELECT JobDetailID, JobType, SequenceNumber,  SQLString, DestinationTable FROM dbo.IOS_Jobs_Details WHERE JobID = " & jobid & " ORDER BY SequenceNumber ASC"

                If cn_SON_Incon Is Nothing Then
                    cn_SON_Incon = New Odbc.OdbcConnection(connStrIOSServer)
                    cn_SON_Incon.ConnectionTimeout = 5
                    cn_SON_Incon.Open()
                End If

                gcSONInconQueries.DataSource = Nothing
                gvSONInconQueries.Columns.Clear()
                gcSONInconQueries.Refresh()

                Dim ds_queries As DataSet = New DataSet

                sCommand_SON_Incon_Queries = New Odbc.OdbcCommand(Sql_queries, cn_SON_Incon)
                sAdapter_SON_Incon_Queries = New Odbc.OdbcDataAdapter(sCommand_SON_Incon_Queries)
                sAdapter_SON_Incon_Queries.Fill(ds_queries, "JobQueries")

                'IOSDevExpressGrid.PopulateDataInGrid(gcSONInconQueries, gvSONInconQueries, dt, "ALL")

                gcSONInconQueries.DataSource = ds_queries
                gcSONInconQueries.DataMember = "JobQueries"
                gvSONInconQueries.Columns(0).Visible = False
                gvSONInconQueries.Columns("SQLString").Width = gcSONInconQueries.Width - gvSONInconQueries.Columns("JobType").Width - gvSONInconQueries.Columns("SequenceNumber").Width - gvSONInconQueries.Columns("DestinationTable").Width - gvSONInconQueries.Columns("JobDetailID").Width - 10
                gcSONInconQueries.Refresh()

                'Loading Variables
                Dim Sql_variables As String = "SELECT * FROM dbo.IOS_Jobs_Variables WHERE JobID = " & jobid & " ORDER BY VariableName ASC"


                gcSONInconVariables.DataSource = Nothing
                gvSONInconVariables.Columns.Clear()
                gcSONInconVariables.Refresh()
                Dim ds_variables As DataSet = New DataSet

                sCommand_SON_Incon_Variables = New Odbc.OdbcCommand(Sql_variables, cn_SON_Incon)
                sAdapter_SON_Incon_Variables = New Odbc.OdbcDataAdapter(sCommand_SON_Incon_Variables)
                sAdapter_SON_Incon_Variables.Fill(ds_variables, "JobVariables")
                gcSONInconVariables.DataSource = ds_variables
                gcSONInconVariables.DataMember = "JobVariables"
                gvSONInconVariables.Columns(0).Visible = False
                gcSONInconVariables.Refresh()

                'loading exceptions
                Dim sql_except As String = "SELECT  ExceptionID ,ExceptionTimeStamp  ,ExceptionExpiryDate ,ExceptionString  FROM IOS_Jobs_Exceptions  WHERE JobID = " & jobid & " ORDER BY ExceptionID ASC"
                gcSONInconExceptions.DataSource = Nothing
                gvSONInconExceptions.Columns.Clear()
                gcSONInconExceptions.Refresh()
                Dim ds_Exceptions As DataSet = New DataSet

                sCommand_SON_Incon_Exceptions = New Odbc.OdbcCommand(sql_except, cn_SON_Incon)
                sAdapter_SON_Incon_Exceptions = New Odbc.OdbcDataAdapter(sCommand_SON_Incon_Exceptions)
                sAdapter_SON_Incon_Exceptions.Fill(ds_Exceptions, "JobExceptions")
                gcSONInconExceptions.DataSource = ds_Exceptions
                gcSONInconExceptions.DataMember = "JobExceptions"
                gvSONInconExceptions.Columns(0).Visible = False
                gvSONInconExceptions.Columns("ExceptionString").Width = gcSONInconExceptions.Width - gvSONInconExceptions.Columns("ExceptionID").Width - gvSONInconExceptions.Columns("ExceptionTimeStamp").Width - gvSONInconExceptions.Columns("ExceptionExpiryDate").Width - 10
                gvSONInconExceptions.Columns("ExceptionTimeStamp").DisplayFormat.FormatString = "yyyy-MM-dd HH:mm"
                gvSONInconExceptions.Columns("ExceptionExpiryDate").DisplayFormat.FormatString = "yyyy-MM-dd HH:mm"
                gcSONInconExceptions.Refresh()


            Catch ex As Exception
            End Try

            Try
                If SuperTabControlInconsist.SelectedTabPage.Text = "Results" Then
                    FillJobRunID(jobid)
                    dlgJobsAdd.SelectedJobID = jobid
                    FillJobResultsGrid(jobid)
                End If
            Catch ex1 As Exception
            End Try

            LoadModuleReports(jobid)
            SetDefaultReport(jobid)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info: " & tlvSONIncon.SelectedNode.Text, "Completed")
            Application.UseWaitCursor = False
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvResult_DoubleClick(sender As Object, e As EventArgs) Handles gvResult.DoubleClick
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            Dim jobname As String = Replace(tlvSONIncon.SelectedNode.SubItems(1).Text, " ", "_")
            Dim dt_jobs As DataTable = CType(tlvSONIncon.Tag, DataTable)

            Dim dr() As DataRow = dt_jobs.Select("jobid=" & jobid)

            Dim tag(1) As String
            tag(0) = nZ(dr(0)("InCon_SourceField"), "")
            tag(1) = nZ(dr(0)("InCon_MapField"), "")
            SuperTabControlInconsist.Tag = tag
            frmMapWindow.MapGridTable_DoubleClick(SuperTabControlInconsist, e)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvResult_PopupMenuShowing(sender As Object, args As Views.Grid.PopupMenuShowingEventArgs) Handles gvResult.PopupMenuShowing
        Try
            If args.HitInfo.InDataRow AndAlso args.HitInfo.InRowCell Then
                SuperDataGrid_Inconsist_ColumnOfCellClicked = args.HitInfo.Column.FieldName
                tsmi_SON_Incon_ThemeField.Text = "Theme Field:" & SuperDataGrid_Inconsist_ColumnOfCellClicked
                ' check datatype of clicked cell
                Dim datatype As String = gvResult.GetRowCellValue(args.HitInfo.RowHandle, args.HitInfo.Column).GetType().ToString
                If datatype.Contains("String") Then
                    tsmi_SON_Incon_Thematic.SelectedItem = tsmi_SON_Incon_Thematic.Items(2)
                Else
                    tsmi_SON_Incon_Thematic.SelectedItem = tsmi_SON_Incon_Thematic.Items(1)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvResult_RowCellClick(sender As Object, args As Views.Grid.RowCellClickEventArgs) Handles gvResult.RowCellClick
        If args.Button = MouseButtons.Right Then
            cmSONIncondgvResult.Show(Cursor.Position)
            Try
                SuperDataGrid_Inconsist_ColumnOfCellClicked = args.Column.FieldName
                tsmi_SON_Incon_ThemeField.Text = "Theme Field:" & SuperDataGrid_Inconsist_ColumnOfCellClicked
                ' check datatype of clicked cell
                Dim datatype As String = args.CellValue.GetType().ToString
                If datatype.Contains("String") Then
                    tsmi_SON_Incon_Thematic.SelectedItem = tsmi_SON_Incon_Thematic.Items(2)
                Else
                    tsmi_SON_Incon_Thematic.SelectedItem = tsmi_SON_Incon_Thematic.Items(1)
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub gvResult_EndSorting(sender As Object, e As EventArgs) Handles gvResult.EndSorting
        Try
            'gvResult.ClearSelection()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchJobIdName_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchJobIdName.KeyUp
        Try
            If dtJobIncon IsNot Nothing Then
                Jobs_Load_Inconsist()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgvResults_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles dgvResults.ProcessGridKey
        Try
            e.SuppressKeyPress = False
            e.Handled = False
            If (e.Control) AndAlso (e.KeyCode = Keys.Right) Then
                gvResult.FocusedColumn = gvResult.Columns(gvResult.Columns.Count - 1)
                e.SuppressKeyPress = True
                e.Handled = True
            ElseIf (e.Control) AndAlso (e.KeyCode = Keys.Left) Then
                gvResult.FocusedColumn = gvResult.Columns(0)
                e.SuppressKeyPress = True
                e.Handled = True
            End If
        Catch
        End Try
    End Sub

    Private Sub btnReportDesigner_Click(sender As Object, e As EventArgs) Handles btnReportDesignerSON.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dashboardID As Integer = Nothing
            Dim jobID As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            If cmbModuleReports.SelectedIndex > 0 Then
                dashboardID = CInt(TryCast(cmbModuleReports.SelectedItem, clsComboBoxItem).Value)
                Dim objDashDesigner As New frmDashboardDesigner()
                objDashDesigner.dashboardID = dashboardID
                objDashDesigner.dashboardName = cmbModuleReports.SelectedItem.ToString
                objDashDesigner.ShowDialog()
            End If

            LoadModuleReports(jobID)
            SetComboBox(cmbModuleReports, ComboSelectBased.ValueBased, dashboardID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCreateReport_Click(sender As Object, e As EventArgs) Handles btnCreateReportSON.Click
        Try
            Dim newDashboardName As String = Nothing
            If tlvSONIncon.SelectedNode Is Nothing Or tlvSONIncon.SelectedNode.Level = 0 Then
                SetMessage("Please Select Job ID")
                Exit Sub
            End If

            Dim jobID As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            newDashboardName = XtraInputBox.Show("Add New Dashboard:", "SON - Add New Dashboard", "", MessageBoxButtons.OKCancel)
            If newDashboardName.Trim <> "" Then
                Dim parray()() As String = {
                    New String() {"@DashboardName", Chr(39) & newDashboardName & Chr(39)},
                    New String() {"@DashboardOwner", Chr(39) & Environment.UserName & Chr(39)},
                    New String() {"@DashboardModule", Chr(39) & "SON" & Chr(39)},
                    New String() {"@JobID", jobID},
                    New String() {"@AccessFlag", Chr(39) & "Public" & Chr(39)}
                }
                Dim strConnection As String = GetSQL(8101, parray)(0)
                Dim sqlParam As String = GetSQL(8101, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                LoadModuleReports(jobID)
                SetComboBox(cmbModuleReports, ComboSelectBased.TextBased, newDashboardName)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnReportSetDefaultSON_Click(sender As Object, e As EventArgs) Handles btnReportSetDefaultSON.Click
        Try
            Dim jobID As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            If cmbModuleReports.SelectedIndex > 0 Then
                Dim parray()() As String = {
                    New String() {"@JobID", jobID},
                    New String() {"@DashboardID", CInt(TryCast(cmbModuleReports.SelectedItem, clsComboBoxItem).Value)}
                }
                Dim strConnection As String = GetSQL(9320, parray)(0)
                Dim sqlParam As String = GetSQL(9320, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            Else
                SetMessage("Select a dashboard to set as default")
                Exit Sub
            End If
            LoadModuleReports(jobID)
            SetDefaultReport(jobID)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbModuleReports_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim sqlDataSource As SqlDataSource = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbModuleReports.SelectedIndex > 0 Then
                LoadDashboardReport()
            Else
                ReportViewer.Dashboard = Nothing
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If sqlDataSource IsNot Nothing Then
                sqlDataSource.Connection.Close()
                sqlDataSource = Nothing
            End If
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteReportSON_Click(sender As Object, e As EventArgs) Handles btnDeleteReportSON.Click
        Try
            Dim jobID As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            Dim isPowerUser As Boolean = False
            Dim dashboardOwner As String = Nothing

            If cmbModuleReports.SelectedIndex > 0 Then
                If XtraMessageBox.Show("Are you sure to delete report: " & cmbModuleReports.SelectedItem.ToString & "?", "Delete Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()

                    Dim dashboardID As Integer = CInt(TryCast(cmbModuleReports.SelectedItem, clsComboBoxItem).Value)
                    dashboardOwner = dtSONReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("DashboardID") = dashboardID)(0)("DashboardOwner").ToString

                    'checking current user is the report owner
                    If dashboardOwner.ToLower = Environment.UserName.ToLower Then
                        isPowerUser = True
                    End If

                    If dashboardOwner.ToLower <> Environment.UserName.ToLower Then
                        'checking whether the current user (not report owner) is a power user
                        If configMgr.User.IsPowerUser = True Then
                            isPowerUser = True
                        Else
                            SetMessage("Current user can't delete the report as the report owner is a different user.")
                            isPowerUser = False
                        End If
                    End If

                    If (isPowerUser = True) Then
                        DeleteDashboardReport(dashboardID)
                        LoadModuleReports(jobID)
                        SetDefaultReport(jobID)
                    End If
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

    Private Sub tsmi_JobRunManual_Click(sender As Object, e As EventArgs) Handles tsmi_JobRunManual.Click
        Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
        Try

            If tsmi_JobRunManual.Text = "Abort Run Manual!" Then
                objThreadRunManual.Abort()
                tlvSONIncon.SelectedNode.SubItems(4).Text = 0
                tlvSONIncon.Refresh()
                UpdateJobRunManualStatus(0, jobid)
                tsmi_JobRunManual.Text = "Run Manual"
            Else

                UpdateJobRunManualStatus(1, jobid)
                tsmi_JobRunManual.Text = "Abort Run Manual!"

                Dim objRunManual As New JobRunManualClass()
                objRunManual.jobID = jobid
                objRunManual.RunManualStatus = 1
                objRunManual.nd = tlvSONIncon.SelectedNode
                AddHandler objRunManual.ThreadComplete, AddressOf ExecuteAfteRunManualThreadComplete
                objThreadRunManual = New System.Threading.Thread(AddressOf objRunManual.RunManual)
                objThreadRunManual.Start()

            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            'Finally
            '    UpdateJobRunManualStatus(0, jobid)
        End Try
    End Sub

    Private Sub ExecuteAfteRunManualThreadComplete(nd As TreeListViewNode, Status As Integer, ti As Thread)
        SyncLock objRunManualThreadLock
            Dim arg() As Object = {nd, Status}
            Me.BeginInvoke(New CallThreadInvokedJobRunManual(AddressOf SetRunManualLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetRunManualLastStatus(ByRef nd As TreeListViewNode, Status As Integer)
        SyncLock objRunManualThreadLock
            If nd IsNot Nothing Then
                If Status = 0 Then
                    nd.SubItems(4).Text = 0
                    tsmi_JobRunManual.Text = "Run Manual"
                ElseIf Status = 1 Then
                    nd.SubItems(4).Text = 1
                    tsmi_JobRunManual.Text = "Abort Run Manual!"
                ElseIf Status = -1 Then
                    'nd.SubItems(4).Text = -1
                End If

                tlvSONIncon.Refresh()
                'tsmi_JobRunManual.Text = "Run Manual"
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub ReportViewer_ConfigureDataConnection(ByVal sender As Object, ByVal e As DashboardConfigureDataConnectionEventArgs) Handles ReportViewer.ConfigureDataConnection
        e.ConnectionParameters = CreateConnectionParameters()
    End Sub

    Private Function CreateConnectionParameters() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(2000)
        Dim connString As String = connArr(1)
        Return New MsSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .DatabaseName = connString.Split(";")(1).Split("=")(1),
            .AuthorizationType = MsSqlAuthorizationType.SqlServer,
            .UserName = connString.Split(";")(2).Split("=")(1),
            .Password = connString.Split(";")(3).Split("=")(1)
        }
    End Function

#End Region

#Region "Infinite Scrolling"

    Private QueryOffset As Integer = 0
    Private QueryBatchSize As Integer = 0

    Private currViewRowFilter As String = ""
    Private currViewSortStr As String = ""

    Dim _virtualServerModeSrouce As VirtualServerModeSource
    Dim objLock As New Object

    Private Function CreateData(ByVal jobId As String, _offset As Integer, _batchSize As Integer, Optional _customFilter As String = Nothing, Optional _sortExpression As String = Nothing) As DataTable
        Dim jobRunID As String = "NULL"
        Dim iSelectedRows() As Integer = gvJobRunID.GetSelectedRows()
        If iSelectedRows.Length > 0 Then
            jobRunID = "'"
            For iCnt As Integer = 0 To iSelectedRows.Length - 1
                jobRunID &= "''" & gvJobRunID.GetRowCellValue(iSelectedRows(iCnt), "jobrunid") & "'',"
            Next

            jobRunID = jobRunID.TrimEnd(",")
            jobRunID = jobRunID & "'"
        End If

        If _customFilter IsNot Nothing Then
            If _customFilter.Contains("#") Then
                _customFilter = _customFilter.Replace("#", "'")
            End If
        End If

        Dim dtData As DataTable = clsSQLCommands.JobResults(connStrIOSServer, jobId, 1, jobRunID, _offset, _batchSize, _customFilter, _sortExpression)
        If dtData IsNot Nothing Then
            If dtData.Columns.Contains("rowHash") Then dtData.Columns.Remove("rowHash")
        End If
        Return dtData
    End Function

    Private Sub FillJobResultsGrid(jobId As Integer)
        Try
            IsFirstTimeLoading = True
            QueryOffset = 0
            QueryBatchSize = 1000
            currViewRowFilter = ""
            currViewSortStr = ""
            dtResultsData = Nothing

            If (_virtualServerModeSrouce IsNot Nothing) Then
                RemoveHandler _virtualServerModeSrouce.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
                RemoveHandler _virtualServerModeSrouce.ConfigurationChanged, AddressOf VirtualServerModeSource_ConfigurationChanged
                RemoveHandler _virtualServerModeSrouce.MoreRows, AddressOf VirtualServerModeSource_MoreRows
                RemoveHandler _virtualServerModeSrouce.GetUniqueValues, AddressOf VirtualServerModeSource_GetUniqueValues
            End If

            _virtualServerModeSrouce = New VirtualServerModeSource()

            AddHandler _virtualServerModeSrouce.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
            AddHandler _virtualServerModeSrouce.ConfigurationChanged, AddressOf VirtualServerModeSource_ConfigurationChanged
            AddHandler _virtualServerModeSrouce.MoreRows, AddressOf VirtualServerModeSource_MoreRows
            AddHandler _virtualServerModeSrouce.GetUniqueValues, AddressOf VirtualServerModeSource_GetUniqueValues

            dgvResults.DataSource = Nothing
            gvResult.OptionsView.ColumnAutoWidth = False
            gvResult.Columns.Clear()
            dgvResults.DataSource = _virtualServerModeSrouce
            dgvResults.Tag = CInt(tlvSONIncon.SelectedNode.Text)

            If dtResultsData IsNot Nothing Then
                For Each dtCol As DataColumn In dtResultsData.Columns
                    If dtCol.DataType = GetType(DateTime) Then
                        gvResult.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        If regionalSettings = False Then
                            gvResult.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
                        Else
                            gvResult.Columns(dtCol.ColumnName).DisplayFormat.FormatString = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                        End If
                    End If
                    gvResult.Columns(dtCol.ColumnName).BestFit()
                    If gvResult.Columns(dtCol.ColumnName).Width > 500 Then
                        gvResult.Columns(dtCol.ColumnName).Width = 500
                    End If
                Next
                If CDate(dtResultsData.Rows(0)("JobTimeStamp").ToString).ToString("yyyyMMdd") <> Now().ToString("yyyyMMdd") Then
                    lblResultsMsg.Text = "Note: The results data isn't from today"
                    tlpResultsMain.RowStyles(0).Height = 25
                Else
                    lblResultsMsg.Text = ""
                    tlpResultsMain.RowStyles(0).Height = 0
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VirtualServerModeSource_AcquireInnerList(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeAcquireInnerListEventArgs)
        Try
            Dim dtTempColumn As New DataTable
            If dtResultsData Is Nothing Then
                Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
                dtTempColumn = CreateData(jobid, 0, 1, "") ' CreateData is called to initialize column structure for infinite grid
            Else
                dtTempColumn = dtResultsData.Rows.Cast(Of DataRow).Take(1)
            End If
            e.InnerList = dtTempColumn.DefaultView
            e.AddMoreRowsFunc = AddressOf AddMoreRows
            e.ClearAndAddRowsFunc = AddressOf ClearAndAddMoreRows
            e.ReleaseAction = AddressOf ReleaseList
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ReleaseList(ByVal list As IList)
        TryCast(list, DataView).Table.Rows.Clear()
    End Sub

    Public Function AddMoreRows(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStr
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ClearAndAddMoreRows(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            TryCast(list, DataView).Table.Rows.Clear()
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStr
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub VirtualServerModeSource_MoreRows(sender As Object, e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            If IsFirstTimeLoading Then
                gvResult.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
            Else
                gvResult.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Panel
            End If
            e.RowsTask = Task.Factory.StartNew(
              Function()
                  SyncLock objLock
                      Try
                          Dim jobid As Integer = tlvSONIncon.SelectedNode.Text
                          Dim dtData As New DataTable

                          If e.UserData Is Nothing Then
                              If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                                  dtData = CreateData(jobid, QueryOffset, QueryBatchSize, currViewRowFilter, currViewSortStr)
                              Else
                                  dtData = CreateData(jobid, QueryOffset, QueryBatchSize, currViewRowFilter)
                              End If
                          Else
                              dtData = CType(e.UserData, DataView).ToTable()
                          End If

                          If dtResultsData IsNot Nothing Then
                              dtResultsData.Merge(dtData)
                          Else
                              dtResultsData = dtData
                          End If

                          QueryOffset = dtResultsData.Rows.Count
                          Dim nextBatch = dtResultsData.Clone()
                          Dim moreRows As Boolean = True
                          Dim rowCount As Integer = e.CurrentRowCount

                          Do While nextBatch.Rows.Count < dtData.Rows.Count
                              nextBatch.ImportRow(dtResultsData.Rows(rowCount))
                              rowCount += 1
                          Loop
                          moreRows = e.CurrentRowCount + QueryBatchSize <= rowCount
                          Return New VirtualServerModeRowsTaskResult(nextBatch.DefaultView, moreRows, Nothing)
                      Catch
                          Dim dt As New DataTable
                          Return New VirtualServerModeRowsTaskResult(dt.DefaultView, False, Nothing)
                      End Try
                  End SyncLock
              End Function, e.CancellationToken)

            If IsFirstTimeLoading Then
                IsFirstTimeLoading = False
                e.RowsTask.Wait(e.CancellationToken)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VirtualServerModeSource_ConfigurationChanged(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            QueryOffset = 0
            dtResultsData = Nothing

            currViewRowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(e.ConfigurationInfo.Filter)
            If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                currViewSortStr = e.ConfigurationInfo.SortInfo(0).ToString()
            End If

            Dim jobid As Integer = tlvSONIncon.SelectedNode.Text
            Dim dtData As New DataTable
            dtData = CreateData(jobid, QueryOffset, QueryBatchSize, currViewRowFilter, currViewSortStr)
            e.UserData = dtData.DefaultView
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VirtualServerModeSource_GetUniqueValues(ByVal sender As Object, ByVal e As VirtualServerModeGetUniqueValuesEventArgs)
        Try
            e.UniqueValuesTask = New Task(Of Object())(Function()
                                                           Dim dt As New DataTable
                                                           Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
                                                           Dim jobRunID As String = "NULL"
                                                           Dim columnFilter As String = "NULL"

                                                           Dim iSelectedRows() As Integer = gvJobRunID.GetSelectedRows()
                                                           If iSelectedRows.Length > 0 Then
                                                               jobRunID = "'"
                                                               For iCnt As Integer = 0 To iSelectedRows.Length - 1
                                                                   jobRunID &= "''" & gvJobRunID.GetRowCellValue(iSelectedRows(iCnt), "jobrunid") & "'',"
                                                               Next

                                                               jobRunID = jobRunID.TrimEnd(",")
                                                               jobRunID = jobRunID & "'"
                                                           End If

                                                           If gvResult.ActiveFilterCriteria IsNot Nothing AndAlso gvResult.ActiveFilterCriteria.ToString <> "" Then
                                                               columnFilter = "'" & gvResult.ActiveFilterCriteria.ToString.Replace("'", "''") & "'"
                                                           End If

                                                           dt = DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [dbo].[sp_JobResults_Column_Data] " & jobid.ToString() & ",'[" & e.ValuesPropertyName & "]'," & jobRunID & "," & columnFilter)
                                                           Dim filterValue() As Object = Nothing
                                                           If dt IsNot Nothing Then
                                                               filterValue = dt.Rows.OfType(Of DataRow)().Select(Function(x) x.Item(0)).ToArray()
                                                           End If
                                                           Return filterValue
                                                       End Function, e.CancellationToken)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Helper Methods"

    Private Sub FillJobRunID(ByVal jobid As Integer)
        gvJobRunID.Tag = jobid
        Dim strConnection As String = String.Empty
        Dim sqlParam As String = String.Empty
        Dim parray()() As String = {New String() {"@jobid", jobid}}
        strConnection = GetSQL(9318, parray)(0)
        sqlParam = GetSQL(9318, parray)(1)
        Dim dtJobRunID As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(gcJobRunID, gvJobRunID, dtJobRunID, "ALL")
        If dtJobRunID Is Nothing Then Return
        gvJobRunID.AutoFillColumn = gvJobRunID.Columns(0)
        gvJobRunID.Columns(0).Caption = "JobRunID"
        gvJobRunID.Columns(1).Visible = False
    End Sub

    Public Sub ConfigurSONForm(ByVal frmName As String)
        Dim form As EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                tsmi_SON_Incon_dgvResult_AddEx, tsmi_SON_Incon_dgvResult_Mark, tsmi_SON_Incon_Thematic, tsmi_SON_Incon_MapAll, tsmi_SON_Incon_MapFiltered, tsmi_SON_Incon_MapSelect, ExportToXMLToolStripMenuItem, NSNRAML20ToolStripMenuItem,
                ExportToXMLSelectionToolStripMenuItem, NSNRAML20ToolStripMenuItem1, ExportToXMLFilteredToolStripMenuItem, NSNRAML20ToolStripMenuItem2, tsmiCopyAllToCSV, tsmiCopySelectionWOHeader, tsmiCopySelectionWithHeader,
                tsmiExportAllToExcel, tsmiJobsAdd, tsmi_Jobs_Edit, tsmi_Jobs_Delete, btnCreateReportSON, btnReportDesignerSON, btnReportSetDefaultSON, gcSONInconQueries
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

    Private Function CreateSingleChart(ByVal drow As DataRow) As Chart
        Dim objectscharted As String = ""

        'Assign data to Chart
        '*************************
        Dim ch As Chart = New Chart()
        ch.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
        Dim i As Integer
        Dim Y1axislabel, Y2axislabel As String
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis
        Dim yaxis2 As Axis

        Dim color_R, color_B, color_G As Integer
        Dim lastchart As String = ""
        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0

        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1

        Try
            'configures individual chart when new chartline is detected
            If lastchart = "" Or lastchart <> drow(5).ToString Then
                Y1axisAbsorPerc = drow("chartY1AbsPerc").trim
                Y2axisAbsOrPerc = nZ(drow("chartY2AbsPerc"), "Abs")

                Y1axisPrecision = CInt(drow("chartY1axisPrecision"))
                Y2axisPrecision = CInt(nZ(drow("chartY2axisPrecision"), "0"))

                Y1axislabel = nZ(drow("chartY1axisLabels"), " ")
                Y2axislabel = nZ(drow("chartY2axisLabels"), " ")

                ch.DefaultElement.Marker.Visible = False
                ch.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
                ch.LegendBox.DefaultEntry.Value = ""
                ch.XAxis.TickLabelMode = TickLabelMode.Angled
                ch.XAxis.TickLabelAngle = 45
                ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
                ch.ToolTip.InitialDelay = 1
                ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal

                If drow("chartXAxisElement").ToString.ToUpper = "DATE" Then
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.TimeInterval = TimeInterval.Days
                    ch.XAxis.FormatString = "dd/MM/yy"
                    ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                    ch.XAxis.TimeInterval = TimeInterval.Days
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                    ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
                    ch.XAxis.ScaleRange.ValueHigh = Now()
                End If

                ch.Annotations.Clear()
                ch.TitleBox.Label.Text = " "
                ch.TitleBox.HeaderLabel.Text = drow("ChartTitle").Trim

                ch.TitleBox.Label.Alignment = StringAlignment.Near
                ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                ch.DefaultElement.Hotspot.ToolTip = drow("chartXAxisElement").ToString.ToUpper & ": %XValue" & Chr(13) & "%SeriesName: %Value "

                'Y-Axis Settings   
                yaxis1 = New Axis
                yaxis1.Orientation = Orientation.Left
                yaxis1.Label.Text = Y1axislabel

                yaxis2 = New Axis
                yaxis2.Orientation = Orientation.Right

                ReDim Preserve chart_elements(j)
                ReDim Preserve chart_elementsYAxis(j)
                ReDim Preserve chart_Eltype(j)
                ReDim Preserve chart_ElColor(j)
                chart_elements(j) = drow("chartYAxisElement").ToString.Trim

                chart_elementsYAxis(j) = drow("chartElementsYAxis").trim
                chart_Eltype(j) = drow("chartElementsType").trim
                chart_ElColor(j) = CInt(drow("ChartElementsColor"))
                If UCase(chart_elementsYAxis(j)) = "LEFT" Then
                    chart_YaxisScale(0) = drow("chartYaxisScaleProp").trim
                ElseIf UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                    chart_YaxisScale(1) = drow("chartYaxisScaleProp").trim
                End If

                If nZ(drow("chartY1axisLabels"), "").Length > 0 Then
                    yaxis1.Label.Text = drow("chartY1axisLabels").ToString.Trim
                End If
                If nZ(drow("chartY2axisLabels"), "").Length > 0 Then
                    yaxis2.Label.Text = drow("chartY2axisLabels").ToString.Trim
                End If

                If nZ(drow("chartY1AbsPerc"), " ").Length > 1 Then
                    If drow("chartY1AbsPerc").ToString.ToUpper = "PERC" Then
                        yaxis1.Percent = True
                    End If
                End If
                If nZ(drow("chartY2AbsPerc"), " ").Length > 1 Then
                    If drow("chartY2AbsPerc").ToString.ToUpper = "PERC" Then
                        yaxis2.Percent = True
                    End If
                End If

                yaxis1.NumberPrecision = CInt(nZ(drow("chartY1axisPrecision"), 0))
                yaxis2.NumberPrecision = CInt(nZ(drow("chartY2axisPrecision"), 0))

                If UCase(chart_YaxisScale(0)) = "STACKED" Then
                    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                ElseIf UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                Else
                    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                End If
                If UCase(chart_YaxisScale(1)) = "STACKED" Then
                    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                ElseIf UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                Else
                    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                End If

                Dim de As DataEngine = New DataEngine(DataAccessorSQL.ExecuteDataTable(drow("ConnString").ToString, drow("SqlString").ToString))
                de.DataFields = String2DataFields(chart_elements, drow("chartXAxisElement").ToString)
                de.DataGridFormatString = "N2"

                Dim sc As New SeriesCollection
                sc = de.GetSeries()

                For i = 0 To sc.Count() - 1

                    Select Case UCase(chart_Eltype(i).Trim)
                        Case "LINE"
                            sc(i).Type = SeriesType.Line
                            sc(i).Line.Width = 3
                        Case "BAR"
                            sc(i).Type = SeriesType.Bar
                        Case "AREALINE"
                            sc(i).Type = SeriesType.AreaLine
                    End Select
                    Select Case UCase(chart_elementsYAxis(i).Trim)
                        Case "LEFT"
                            sc(i).YAxis = yaxis1
                        Case "RIGHT"
                            sc(i).YAxis = yaxis2
                    End Select

                    color_R = CLng(chart_ElColor(i)) Mod 256
                    color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                    color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                    sc(i).DefaultElement.Color = System.Drawing.Color.FromArgb(255, color_R, color_G, color_B)
                    sc(i).DefaultElement.Marker.Type = i
                Next

                ch.SeriesCollection.Clear()
                ch.SeriesCollection.Add(sc)

                sc = Nothing
                de = Nothing
                ch.XAxis.Markers.Clear()
                ch.RefreshChart()

                ReDim chart_elements(0)
                ReDim chart_elementsYAxis(0)
                ReDim chart_Eltype(0)
                ReDim chart_ElColor(0)
                ReDim chart_YaxisScale(1)
                j = 0
            End If
            Return ch
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Console.WriteLine(ex.Message.ToString)
        End Try
        Return Nothing
    End Function

    Public Sub Jobs_Load_Inconsist()
        tlvSONIncon.Columns(3).ContentType = ColumnContentType.Image
        Dim dtCategory As New DataTable
        Try
            If dtJobIncon Is Nothing Then
                Dim parray()() As String = {New String() {"@Nothing", ""}}
                Dim connstring As String = GetSQL(9317, parray)(0)
                Dim sql As String = GetSQL(9317, parray)(1)
                sql = sql.Replace("LicenseTo", "'" + Environment.UserName + "'")
                dtJobIncon = New DataTable()
                dtJobIncon = DataAccessorODBC.GetDataTable(connstring, sql)
            End If

            tlvSONIncon.Tag = dtJobIncon
            tlvSONIncon.Nodes.Clear()
            dtJobIncon.DefaultView.RowFilter = ""

            If IsNumeric(txtSearchJobIdName.Text) Then
                dtJobIncon.DefaultView.RowFilter = "JobID = " & Val(txtSearchJobIdName.Text)
            Else
                If txtSearchJobIdName.Text.Length > 2 Then
                    dtJobIncon.DefaultView.RowFilter = "JobName Like '%" & txtSearchJobIdName.Text & "%'"
                End If
            End If

            dtCategory = dtJobIncon.DefaultView.ToTable(True, {"JobCategoryID", "JobCategoryName"})
            For Each drCategory As DataRow In dtCategory.Rows
                Dim parentNode As New TreeListViewNode(drCategory(1).ToString)

                For Each dr As DataRow In dtJobIncon.DefaultView.ToTable().Select("JobCategoryID=" & drCategory(0))
                    FillTreeViewSubItems(dr, parentNode)
                Next

                tlvSONIncon.Nodes.Add(parentNode)
                tlvSONIncon.UpdateLayout()
                tlvSONIncon.ResumeUpdate()
                If txtSearchJobIdName.Text.Length > 2 Then
                    tlvSONIncon.ExpandAll()
                End If
            Next

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub FillTreeViewSubItems(dr As DataRow, parentNode As TreeListViewNode)
        Dim nd As TreeListViewNode = New TreeListViewNode(dr(0).ToString)
        nd.CheckBoxVisible = True

        Dim si As TreeListViewSubItem = New TreeListViewSubItem()
        Dim si0 As TreeListViewSubItem = New TreeListViewSubItem(dr(0).ToString)
        Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(dr(1).ToString)
        Dim si4 As TreeListViewSubItem = New TreeListViewSubItem()
        Dim si5 As TreeListViewSubItem = New TreeListViewSubItem(IIf(IsDBNull(dr(16).ToString), 0, IIf(CBool(dr(16).ToString) = False, 0, 1)))

        If dr(6).ToString.ToUpper = "TRUE" Then
            si4.Image = EmbeddedImage("square_green.bmp")
        Else
            si4.Image = EmbeddedImage("square_red.bmp")
        End If

        nd.SubItems.Add(si)
        nd.SubItems.Add(si0)
        nd.SubItems.Add(si1)
        nd.SubItems.Add(si4)
        nd.SubItems.Add(si5)

        parentNode.Nodes.Add(nd)
    End Sub

    Public Sub Jobs_Load_Param()
        'get list of technologies
        Dim dtQODBC As DataTable = Nothing
        tlvTuneJobs.Columns(2).ContentType = ColumnContentType.Image
        Try
            Dim parray()() As String = {New String() {"@Nothing", ""}}
            Dim connstring As String = GetSQL(9300, parray, dt_IOS_SQL)(0)
            Dim sql As String = GetSQL(9300, parray, dt_IOS_SQL)(1)
            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)

            tlvTuneJobs.Nodes.Clear()
            For Each dr As DataRow In dtQODBC.Rows
                Dim nd As TreeListViewNode = New TreeListViewNode(dr(0).ToString)
                nd.CheckBoxVisible = True

                Dim si0 As TreeListViewSubItem = New TreeListViewSubItem(dr(0).ToString)
                Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(dr(1).ToString)
                Dim si4 As TreeListViewSubItem = New TreeListViewSubItem()

                If dr(6).ToString.ToUpper = "TRUE" Then
                    si4.Image = EmbeddedImage("square_green.bmp")
                Else
                    si4.Image = EmbeddedImage("square_red.bmp")
                End If
                nd.SubItems.Add(si0)
                nd.SubItems.Add(si1)
                nd.SubItems.Add(si4)

                tlvTuneJobs.Nodes.Add(nd)
                tlvTuneJobs.UpdateLayout()
                tlvTuneJobs.ResumeUpdate()
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If Not dtQODBC Is Nothing Then
                dtQODBC.Dispose()
                dtQODBC = Nothing
            End If
        End Try
    End Sub

    Public Function XML_Parameters_Validation(ByVal exporttype As String, ByRef dt As DataTable) As Boolean
        Try
            If exporttype = "NSN - RAML2.0" Then
                Dim validation As Integer = 0
                For Each col As DataColumn In dt.Columns
                    If col.Caption.ToUpper = "OBJECT_DN" Then
                        validation = validation + 1
                    End If
                    If col.Caption.ToUpper = "OBJECT_GID" Then
                        validation = validation + 1
                    End If
                    If col.Caption.ToUpper = "SHORTNAME" Then
                        validation = validation + 1
                    End If
                    If col.Caption.ToUpper = "DEFAULTVALUE" Then
                        validation = validation + 1
                    End If
                Next
                If validation = 4 Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
        Return False
    End Function

    Public Function XML_Parameters_NSN(ByVal fn As String, ByVal dt As DataTable) As Boolean
        Try
            Dim objDom As Xml.XmlDocument
            Dim objRaml As Xml.XmlElement
            Dim objCMdata As Xml.XmlElement
            Dim objHeader As Xml.XmlElement
            Dim objLog As Xml.XmlElement
            Dim objMO As Xml.XmlElement = Nothing
            Dim objParam As Xml.XmlElement
            Dim Version, distname, id, objlevel As String

            objDom = New Xml.XmlDocument
            objDom.LoadXml("<?xml version=""1.0"" encoding=""UTF-8""?><raml><cmData/></raml>")

            objRaml = objDom.GetElementsByTagName("raml").Item(0)
            objRaml.SetAttribute("version", "2.0")
            objRaml.SetAttribute("xmlns", "raml20.xsd")

            objCMdata = objDom.GetElementsByTagName("cmData").Item(0)
            objCMdata.SetAttribute("xmlns", "")
            objCMdata.SetAttribute("type", "plan")
            objCMdata.SetAttribute("scope", "all")
            objCMdata.SetAttribute("name", "default")

            'create header
            objHeader = objDom.CreateElement("header")
            objCMdata.AppendChild(objHeader)

            'create logs
            objLog = objDom.CreateElement("log")
            objHeader.AppendChild(objLog)
            objLog.SetAttribute("dateTime", Now.ToString("dd-MM-yyyy_HH-mm-ss"))
            objLog.SetAttribute("action", "created")

            Dim dn_old As String = ""
            dt.DefaultView.Sort = "Object_GID ASC"

            'create XML for BTS param
            For Each dr As DataRow In dt.DefaultView.ToTable.Rows
                If Not dr("DefaultValue").ToString Is Nothing Then
                    If dr("OBJECT_GID").ToString.Trim <> dn_old Then
                        dn_old = dr("OBJECT_GID").ToString.Trim

                        Version = "RN5.0"
                        distname = dr("Object_DN").ToString
                        id = dr("Object_GID").ToString
                        objlevel = Split(Split(distname, "/").Last, "-").First

                        objMO = objDom.CreateElement("managedObject")
                        objCMdata.AppendChild(objMO)
                        objMO.SetAttribute("class", objlevel)
                        objMO.SetAttribute("version", Version)
                        objMO.SetAttribute("distName", distname)
                        objMO.SetAttribute("id", id)
                        objMO.SetAttribute("operation", "update")
                    End If

                    objParam = objDom.CreateElement("p")
                    objMO.AppendChild(objParam)
                    objParam.SetAttribute("name", dr("ShortName").ToString.Trim)
                    objParam.InnerText = dr("DefaultValue").ToString.Trim
                Else
                End If
            Next

            'save XML file
            objDom.Save(fn)
            Process.Start("explorer.exe", "/select," & fn)
            Return True
        Catch ex As Exception
            MsgBox("Failed writing XML: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function Tune_Parameter_SQL() As String()
        Try
            Dim strout(2) As String
            Dim selectedobjsIN As String = TreeView_Checked2String("Tuning", cmbTuningOT.SelectedItem.ToString, "ObjectID", tvTuningObjects, cmbTuningOT)
            Dim JobID As Integer = CInt(tlvTuneJobs.SelectedNode.Text)
            Dim stross As String = ""
            Dim sql_paramofcells As String = ""
            Dim params As String = Nothing

            For Each nd As TreeListViewNode In tlvTuningParameter.FlatNodes
                Dim dt As DataTable = clsSQLCommands.Get_IOS_OSS_Param_Ref_Data(connStrIOSServer, nd.SubItems(0).Text)
                Dim col_param As String = dt(0)("DB_table_name") & "." & dt(0)("DB_column_name") & Chr(32) & nd.SubItems(2).Text
                params = params & col_param & ","
            Next
            params = params.TrimEnd(",")
            Dim parray()() As String = {New String() {"@columns", params}, New String() {"@objs", selectedobjsIN}}

            If cmbTuningTech.Text = networksAll.Network3G1 Then
                stross = GetSQL(9203, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9203, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network2G1 Then
                stross = GetSQL(9202, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9202, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network2G2 Then
                stross = GetSQL(9204, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9242, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network3G2 Then
                stross = GetSQL(9205, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9205, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network2G3 Then
                stross = GetSQL(9206, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9206, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network3G3 Then
                stross = GetSQL(9207, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9207, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network4G1 Then
                stross = GetSQL(9208, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9208, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network4G2 Then
                stross = GetSQL(9209, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9209, parray, dt_IOS_SQL)(1)
            ElseIf cmbTuningTech.Text = networksAll.Network4G3 Then
                stross = GetSQL(9210, parray, dt_IOS_SQL)(0)
                sql_paramofcells = GetSQL(9210, parray, dt_IOS_SQL)(1)
            End If

            strout(0) = stross
            strout(1) = sql_paramofcells
            Return strout
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function SQL_Construct_KPI_Only(ByVal tech As String, ByVal ObjType As String, ByVal aggr_to As String, ByVal kpis As String, ByVal interval As Integer, ByVal resolution As String, ByVal objs As String) As String
        Dim sql_select As String = Nothing
        Dim sql_where_misc As String = Nothing
        Dim sql_where_object As String = Nothing
        Dim sql_where_tables As String = Nothing
        Dim sql_where_period As String = Nothing
        Dim sql_groupby As String = Nothing
        Dim sql_orderby As String = Nothing
        Dim sql_from_time As String = Nothing
        Dim sql_kpi As String = Nothing
        Dim sql_total As String = Nothing

        Dim startdate_string As String = ""
        Dim enddate_string As String = ""

        Dim conn_el As Odbc.OdbcConnection = Nothing
        Dim comm_sql As Odbc.OdbcCommand = Nothing
        Dim comm_Element As Odbc.OdbcCommand = Nothing
        Dim dr_sql As Odbc.OdbcDataReader = Nothing
        Dim dr_element As Odbc.OdbcDataReader = Nothing
        Dim sqlelement As String = Nothing
        Dim sql_sql As String = Nothing

        Try
            'Open connection to server
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()

            'get sqlconstructstat

            'objecttree selection to string

            'set aggr_to

            'set purpose
            Dim purpose As String = "ParamTune"

            'get sql
            Dim StringForSourceTable As String = ""
            Dim aggr_from As String = ""
            'get sql
            sql_sql = "SELECT * FROM qry_IOS_ConstructStatSQL WHERE (((tech)=" & Chr(39) & tech & Chr(39) & ") AND ((Purpose)=" & Chr(39) & purpose & Chr(39) & ") AND ((Aggregate_to)=" & Chr(39) & aggr_to & Chr(39) & ") AND ((ObjectType)=" & Chr(39) & ObjType & Chr(39) & "))"
            comm_sql = New Odbc.OdbcCommand(sql_sql, conn_el)
            dr_sql = comm_sql.ExecuteReader
            sql_from_time = ""

            dr_sql.Read()
            If Not dr_sql.HasRows = 0 Then
                sql_select = dr_sql.GetValue(3).ToString.Trim
                aggr_from = dr_sql("Aggregate_From").ToString.Trim
                If resolution = "Hourly" Then
                    sql_from_time = " " & dr_sql.GetValue(4).ToString.Trim
                    connectionString = dr_sql.GetValue(5).ToString.Trim
                    StringForSourceTable = "_HOUR"
                ElseIf resolution = "Daily" Then
                    sql_from_time = " " & dr_sql.GetValue(6).ToString.Trim
                    connectionString = dr_sql.GetValue(7).ToString.Trim
                    StringForSourceTable = "_DAY"
                ElseIf resolution = "BH" Then
                    sql_from_time = " " & dr_sql.GetValue(8).ToString.Trim
                    connectionString = dr_sql.GetValue(9).ToString.Trim
                    StringForSourceTable = "_BH"
                ElseIf resolution = "Weekly" Then
                    sql_from_time = " " & dr_sql.GetValue(10).ToString.Trim
                    connectionString = dr_sql.GetValue(11).ToString.Trim
                    StringForSourceTable = "_WEEK"
                ElseIf resolution = "WeeklyBH" Then
                    sql_from_time = " " & dr_sql.GetValue(12).ToString.Trim
                    connectionString = dr_sql.GetValue(13).ToString.Trim
                    StringForSourceTable = "_WEEKBH"
                End If

                sql_where_misc = " " & dr_sql.GetValue(14).ToString.Trim
                If cmbTuningOT.Text = "PLMN" Then
                    'to complete
                Else
                    sql_where_object = " " & Replace(dr_sql.GetValue(15), "@object", "").ToString.Trim
                End If
                sql_where_tables = " " & dr_sql.GetValue(16).ToString.Trim
                sql_where_period = " " & Replace(Replace(dr_sql.GetValue(17), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim

                sql_groupby = " " & dr_sql.GetValue(18).ToString.Trim
                sql_orderby = " " & dr_sql.GetValue(19).ToString.Trim
            Else
                ''lblOTStatus.Text = "Error Building Query !"
                ''lblOTStatus.ForeColor = Color.Red
                'Closing and dereferencing
                dr_sql.Close()
                dr_sql = Nothing
                comm_sql.Dispose()
                comm_sql = Nothing
                conn_el.Close()
                conn_el.Dispose()
                conn_el = Nothing
                Return ""
            End If
            'Closing and dereferencing
            dr_sql.Close()
            dr_sql = Nothing
            comm_sql.Dispose()
            comm_sql = Nothing
            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            'get KPI sql
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()
            sqlelement = "SELECT DISTINCT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object FROM IOS_Chart_Configuration INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & tech & Chr(39) & ") and (IOS_SQL_KPI.KPI_Name " & kpis & " )"

            comm_Element = New Odbc.OdbcCommand(sqlelement, conn_el)
            dr_element = comm_Element.ExecuteReader
            sql_kpi = ""
            While dr_element.Read
                sql_kpi = sql_kpi + " " + dr_element.GetValue(0).trim + ", "
            End While
            sql_kpi = sql_kpi.TrimEnd(" ")
            sql_kpi = sql_kpi.TrimEnd(",")


            comm_Element = New Odbc.OdbcCommand(sqlelement, conn_el)
            dr_element = comm_Element.ExecuteReader
            sql_kpi = ""
            Dim sourcetable As String = ""
            Dim joinobjs As String = ""
            Dim aliastable As String = ""
            While dr_element.Read
                sql_kpi = sql_kpi + " " + dr_element.GetValue(0).trim + ", "
                sourcetable = dr_element.GetValue(1).trim
                ''If Not SuperRadioButton_Hourly_3G3.Checked And Not SuperRadioButton_Raw_3G3.Checked Then
                ''    sourcetable = Replace(sourcetable, "_HOUR", StringForSourceTable)   'if _HOUR is base table in KPI then _HOUR must be replaced for day, bh, etc..
                ''    sourcetable = Replace(sourcetable, "_RAW", StringForSourceTable)    'if _MNC1_RAW is base table, then _RAW must be replaced by day, bh, etc..
                ''    If sourcetable.Contains("MNC1") Then sourcetable = Replace(sourcetable, "MNC1", dr_element("Object").ToString.Trim) 'and MNC1 with element
                ''End If
                sourcetable = Replace(sourcetable, "<AggregatedObject>", aggr_from)
                aliastable = dr_element.GetValue(2).ToString.Trim
                joinobjs = dr_element.GetValue(3).ToString.Trim
            End While
            sql_kpi = sql_kpi.TrimEnd(" ")
            sql_kpi = sql_kpi.TrimEnd(",")

            'building sourcetable for multi
            Dim sourcetable_final As String = ""
            If sourcetable.Contains(",") Then
                For i As Integer = 0 To Split(sourcetable, ",").Count - 1
                    sourcetable_final = sourcetable_final + Split(sourcetable, ",")(i) & " " & Split(aliastable, ",")(i) + ", "
                Next
                sourcetable_final = sourcetable_final.Substring(0, Len(sourcetable_final) - 2)
            Else
                sourcetable_final = sourcetable + " " + aliastable
            End If

            'building jointable for multi
            Dim jointable As String = " "
            If joinobjs.Contains(",") Then

                Dim firsttable As String = Split(aliastable, ",")(0)
                For Each obj As String In Split(joinobjs, ",")
                    For i As Integer = 1 To Split(aliastable, ",").Count - 1
                        jointable = jointable + firsttable + "." + obj + " = " + Split(aliastable, ",")(i) + "." + obj + " AND "
                    Next
                Next
                jointable = " AND " & jointable.Substring(0, Len(jointable) - 4)
                aliastable = Split(aliastable, ",")(0)
            End If

            'Closing and dereferencing
            comm_Element.Dispose()
            comm_Element = Nothing
            dr_element.Close()
            dr_element = Nothing
            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            sql_total = sql_select + sql_kpi + " " + sql_from_time + sql_where_misc + sql_where_object + sql_where_tables + sql_where_period + jointable + sql_groupby + sql_orderby
            sql_total = Replace(sql_total, "@sourcetable", sourcetable_final)
            sql_total = Replace(sql_total, "@alias", aliastable)
            sql_total = Replace(sql_total, "@tablejoin", jointable)
            sql_total = Replace(sql_total, "@interval", interval)
            sql_total = Replace(sql_total, "@JobID", CInt(tlvTuneJobs.SelectedNode.Text))

            Return sql_total
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            If Not dr_sql Is Nothing Then
                dr_sql.Close()
                dr_sql = Nothing
            End If
            If Not comm_sql Is Nothing Then
                comm_sql.Dispose()
                comm_sql = Nothing
            End If
            If Not dr_element Is Nothing Then
                dr_element.Close()
                dr_element = Nothing
            End If
            If Not comm_Element Is Nothing Then
                comm_Element.Dispose()
                comm_Element = Nothing
            End If
            If Not conn_el Is Nothing Then
                conn_el.Close()
                conn_el.Dispose()
                conn_el = Nothing
            End If
            Return Nothing
        End Try
    End Function

    Private Sub Tune_KPI_Insert(ByVal KPI As String, ByVal KPI_ID As Integer, ByVal KPIOperatorGreen As String, ByVal KPITresholdGreen As Double,
                                ByVal KPIOperatorRed As String, ByVal KPITresholdRed As Double, ByVal kpiinterval As Integer, ByVal breachgreen As Integer, ByVal breachred As Integer)
        Try
            Dim tlvnode As TreeListViewNode = New TreeListViewNode(KPI_ID)
            Dim tlvnode_sub0 As TreeListViewSubItem = New TreeListViewSubItem(KPI)
            Dim tlvnode_sub1 As TreeListViewSubItem = New TreeListViewSubItem(KPI_ID)
            Dim tlvnode_sub2 As TreeListViewSubItem = New TreeListViewSubItem(KPIOperatorGreen)
            Dim tlvnode_sub3 As TreeListViewSubItem = New TreeListViewSubItem(KPITresholdGreen)
            Dim tlvnode_sub4 As TreeListViewSubItem = New TreeListViewSubItem(KPIOperatorRed)
            Dim tlvnode_sub5 As TreeListViewSubItem = New TreeListViewSubItem(KPITresholdRed)
            Dim tlvnode_sub6 As TreeListViewSubItem = New TreeListViewSubItem(kpiinterval)
            Dim tlvnode_sub7 As TreeListViewSubItem = New TreeListViewSubItem(breachgreen)
            Dim tlvnode_sub8 As TreeListViewSubItem = New TreeListViewSubItem(breachred)

            tlvnode.SubItems.Add(tlvnode_sub1)
            tlvnode.SubItems.Add(tlvnode_sub0)
            tlvnode.SubItems.Add(tlvnode_sub2)
            tlvnode.SubItems.Add(tlvnode_sub3)
            tlvnode.SubItems.Add(tlvnode_sub4)
            tlvnode.SubItems.Add(tlvnode_sub5)
            tlvnode.SubItems.Add(tlvnode_sub6)
            tlvnode.SubItems.Add(tlvnode_sub7)
            tlvnode.SubItems.Add(tlvnode_sub8)

            tlvTuningKPI.Nodes.Add(tlvnode)
            tlvTuningKPI.Refresh()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        tlvTuningKPI.UpdateLayout()
    End Sub

    Private Sub Tune_Parameter_Insert_Sub(ByVal parentnd As TreeListViewNode, ByVal Parameter As String, ByVal Parameter_ID As Integer, ByVal objecttype As String, ByVal StepSize As String,
                                          ByVal ActionGreen As String, ByVal ActionRed As String, ByVal UpperLimit As String, ByVal LowerLimit As String)
        Try
            Dim tlvnode As TreeListViewNode = New TreeListViewNode(Parameter_ID)
            Dim tlvnode_sub1 As TreeListViewSubItem = New TreeListViewSubItem(Parameter)
            Dim tlvnode_sub0 As TreeListViewSubItem = New TreeListViewSubItem(Parameter_ID)
            Dim tlvnode_sub7 As TreeListViewSubItem = New TreeListViewSubItem(objecttype)
            Dim tlvnode_sub2 As TreeListViewSubItem = New TreeListViewSubItem(ActionRed)
            Dim tlvnode_sub3 As TreeListViewSubItem = New TreeListViewSubItem(ActionGreen)
            Dim tlvnode_sub4 As TreeListViewSubItem = New TreeListViewSubItem(UpperLimit)
            Dim tlvnode_sub5 As TreeListViewSubItem = New TreeListViewSubItem(LowerLimit)
            Dim tlvnode_sub6 As TreeListViewSubItem = New TreeListViewSubItem(StepSize)

            tlvnode.SubItems.Add(tlvnode_sub0)
            tlvnode.SubItems.Add(tlvnode_sub7)

            tlvnode.SubItems.Add(tlvnode_sub1)
            tlvnode.SubItems.Add(tlvnode_sub6)
            tlvnode.SubItems.Add(tlvnode_sub2)
            tlvnode.SubItems.Add(tlvnode_sub3)
            tlvnode.SubItems.Add(tlvnode_sub4)
            tlvnode.SubItems.Add(tlvnode_sub5)

            parentnd.Nodes.Add(tlvnode)
            tlvTuningParameter.Refresh()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        tlvTuningParameter.UpdateLayout()
    End Sub

    Private Sub Tune_ParameterList_Update()
        Dim tech As String = cmbTuningTech.SelectedItem.ToString.Trim
        Dim dtQODBC As DataTable = New DataTable

        cmbTuningParameter.Items.Clear()
        cmbTuningParameter.Text = ""
        Try
            Dim parray2()() As String = {New String() {"@tech", Chr(39) & tech.ToUpper & Chr(39)}}
            Dim connstring As String = GetSQL(9201, parray2, dt_IOS_SQL)(0)
            Dim sql As String = GetSQL(9201, parray2, dt_IOS_SQL)(1)

            dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)
            If Not dtQODBC Is Nothing Then
                cmbTuningParameter.DisplayMember = "P_abbr_name"
                cmbTuningParameter.ValueMember = "ID"
                cmbTuningParameter.DataSource = dtQODBC.Copy
                cmbTuningParameter.Items.Insert(0, "Select Item..")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ObjectTree_LoadTypes(ByRef dt As DataTable, ByRef cmb As DevExpress.XtraEditors.ComboBoxEdit, ByVal tech As String)
        If Not dt Is Nothing Then
            For Each drow As DataRow In dt.Select("tech = " & Chr(39) & tech & Chr(39), "loadorder ASC")
                Dim cmbitem As New clsComboBoxItem()
                cmbitem.Text = drow("Object").ToString.ToUpper
                cmbitem.Tag = drow("InternalObjectName").ToString.ToUpper
                cmbitem.Value = drow("Object").ToString
                cmb.Properties.Items.Add(cmbitem)
            Next
            Dim itm As Integer = cmb.Properties.Items.Add("PLMN")
            cmb.SelectedItem = itm

            If tech = networksAll.Network2G1 Or tech = networksAll.Network2G2 Or tech = networksAll.Network3G1 Or tech = networksAll.Network3G2 Or tech = networksAll.Network4G1 Or tech = networksAll.Network4G2 Or tech = networksAll.Network4G3 Or tech = networksAll.Network3G3 Or tech = networksAll.Network2G3 Or tech = networksAll.NetworkMSCCDR Or tech = networksAll.NetworkSGSNCDR Or tech = networksAll.NetworkGGSNCDR Then
                cmb.Properties.Items.Add("TAGS")
            End If
        End If
    End Sub

    Private Sub Tune_Parameter_Insert(ByVal Parameter As String, ByVal Parameter_ID As Integer, ByVal objecttype As String, ByVal StepSize As String, ByVal ActionGreen As String,
                                      ByVal ActionRed As String, ByVal UpperLimit As String, ByVal LowerLimit As String)
        Try
            Dim tlvnode As TreeListViewNode = New TreeListViewNode(Parameter_ID)
            tlvnode.Key = Parameter_ID
            Dim tlvnode_sub1 As TreeListViewSubItem = New TreeListViewSubItem(Parameter)
            Dim tlvnode_sub0 As TreeListViewSubItem = New TreeListViewSubItem(Parameter_ID)
            Dim tlvnode_sub7 As TreeListViewSubItem = New TreeListViewSubItem(objecttype)
            Dim tlvnode_sub2 As TreeListViewSubItem = New TreeListViewSubItem(ActionRed)
            Dim tlvnode_sub3 As TreeListViewSubItem = New TreeListViewSubItem(ActionGreen)
            Dim tlvnode_sub4 As TreeListViewSubItem = New TreeListViewSubItem(UpperLimit)
            Dim tlvnode_sub5 As TreeListViewSubItem = New TreeListViewSubItem(LowerLimit)
            Dim tlvnode_sub6 As TreeListViewSubItem = New TreeListViewSubItem(StepSize)

            tlvnode.SubItems.Add(tlvnode_sub0)
            tlvnode.SubItems.Add(tlvnode_sub7)
            tlvnode.SubItems.Add(tlvnode_sub1)
            tlvnode.SubItems.Add(tlvnode_sub6)
            tlvnode.SubItems.Add(tlvnode_sub2)
            tlvnode.SubItems.Add(tlvnode_sub3)
            tlvnode.SubItems.Add(tlvnode_sub4)
            tlvnode.SubItems.Add(tlvnode_sub5)

            tlvTuningParameter.Nodes.Add(tlvnode)
            tlvTuningParameter.Refresh()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        tlvTuningParameter.UpdateLayout()
    End Sub

    Private Sub Tune_Result_Export_Refresh()
        Try
            'fill export tab
            Dim Data As DataSet = clsSQLCommands.Get_IOS_Tune_Result_Export_Data(connStrIOSServer, cmbTuneAnalyseJob.Text)

            tlvTuneResultsExport.Nodes.Clear()
            For Each dr As DataRow In Data.Tables(0).Rows
                Dim nd As TreeListViewNode = New TreeListViewNode(dr(0).ToString)
                nd.CheckBoxVisible = True
                Dim si0 As TreeListViewSubItem = New TreeListViewSubItem(dr(0).ToString)
                Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(dr(1).ToString)
                Dim si2 As TreeListViewSubItem = New TreeListViewSubItem(nZ(dr(2), ""))
                If IsDate(dr(2)) Then
                    si2.Value = dr(2)
                End If
                Dim si3 As TreeListViewSubItem = New TreeListViewSubItem(nZ(dr(4), ""))

                nd.SubItems.Add(si0)
                nd.SubItems.Add(si1)
                nd.SubItems.Add(si2)
                nd.SubItems.Add(si3)

                Dim str As New List(Of String)
                str.Add(nZ(dr(3).ToString, ""))
                str.Add(nZ(dr(5).ToString, ""))
                nd.Tag = str

                tlvTuneResultsExport.Nodes.Add(nd)
                tlvTuneResultsExport.UpdateLayout()
                tlvTuneResultsExport.ResumeUpdate()
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmSONIncondgvResult_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmSONIncondgvResult.Opening
        Try
            Dim dgv As GridControl = CType(sender.sourcecontrol, GridControl)
            tsmi_RecordCount.Text = "Record Count: " & dgv.DefaultView.RowCount

            'If Not String.IsNullOrEmpty(currViewRowFilter) Then
            '    tsmiCopyFilteredToClipboard.Enabled = True
            '    tsmiExportAllToExcel.Enabled = True
            'Else
            '    tsmiCopyFilteredToClipboard.Enabled = False
            '    tsmiExportAllToExcel.Enabled = False
            'End If

            Dim dt As DataTable = dgv.DataSource
            If dt.Rows.Count = 0 Then
                tsmiMapSelectedNB.Enabled = False
            Else
                If dt.Columns.Contains("S_IOS_CELL_GID") AndAlso dt.Columns.Contains("T_IOS_CELL_GID") Then
                    tsmiMapSelectedNB.Enabled = True
                Else
                    tsmiMapSelectedNB.Enabled = False
                End If
            End If
            dt.Dispose()
            dt = Nothing
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmiMapSelectedNB_Click(sender As Object, e As EventArgs) Handles tsmiMapSelectedNB.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If dtResultsData IsNot Nothing Then
                Dim rIndex() As Integer = CType(gvResult, Views.Grid.GridView).GetSelectedRows()
                Dim dtNBPlot As DataTable = dtResultsData.Clone()
                For i As Integer = 0 To rIndex.Length - 1
                    dtNBPlot.ImportRow(gvResult.GetDataRow(rIndex(i)))
                    ''dtNBPlot.ImportRow(dtResultsData.Rows(rIndex(i)))
                Next
                dtNBPlot.AcceptChanges()

                MapInfo.Engine.Session.Current.Selections.DefaultSelection.Clear()
                If dtNBPlot.Columns.Contains("S_IOS_CELL_GID") Then
                    For Each dr As DataRow In dtNBPlot.DefaultView.ToTable(True, "S_IOS_CELL_GID").Rows
                        IsClearDefaultSelection = False
                        frmMapWindow.Cells_SearchAndDisplay("IOS_CELL_GID", dr.Item("S_IOS_CELL_GID"))
                        IsClearDefaultSelection = True
                    Next
                End If
                MapSelectedNB(dtNBPlot)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCSV_Click(sender As Object, e As EventArgs) Handles btnCSV.Click
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        WaitScreen.ShowWaitScreen("Exporting CSV Data...")
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim jobid As Integer = CInt(tlvSONIncon.SelectedNode.Text)
            Using dtTemp As DataTable = CreateData(jobid, 0, 0)
                If dtTemp IsNot Nothing Then
                    IOSDevExpressGrid.PopulateDataInGrid(gcTemp, gvTemp, dtTemp, "ALL", {"RowHash"})
                    IOSDevExpressGrid.ExportDataGridToCSV(gcTemp)
                    IOSDevExpressGrid.ClearGrid(gcTemp)
                End If
            End Using
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            WaitScreen.CloseWaitScreen()
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lblResultsMsg.Text = ""
            tlpResultsMain.RowStyles(0).Height = 0

            If gvJobRunID.GetSelectedRows().Length > 0 Then
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                FillJobResultsGrid(Val(gvJobRunID.Tag))
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub LoadModuleReports(jobID As Integer)
        RemoveHandler cmbModuleReports.SelectedIndexChanged, AddressOf cmbModuleReports_SelectedIndexChanged
        LoadAllDashboardReports()
        If dtDashboardReports.Rows.Count > 0 Then
            dtSONReports = dtDashboardReports.AsEnumerable().Where(Function(x) x.Field(Of String)("DashboardModule") = "SON").CopyToDataTable()
            Dim dt As DataTable = Nothing
            If dtSONReports.AsEnumerable().Where(Function(x) Not x.IsNull("JobID") AndAlso x.Field(Of Integer)("JobID") = jobID).Count > 0 Then
                dt = dtSONReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("JobID") = jobID).CopyToDataTable()
                BindDevExComboBoxWithTagMember(cmbModuleReports, dt, "DashboardID", "DashboardName", "Select", "DefaultLoad")
            Else
                BindDevExComboBoxWithTagMember(cmbModuleReports, dt, "DashboardID", "DashboardName", "Select", "DefaultLoad")
            End If
        Else
            dtSONReports = Nothing
            BindDevExComboBoxWithTagMember(cmbModuleReports, Nothing, "DashboardID", "DashboardName", "Select", "DefaultLoad")
        End If
        AddHandler cmbModuleReports.SelectedIndexChanged, AddressOf cmbModuleReports_SelectedIndexChanged
    End Sub

    Private Sub SetDefaultReport(jobID As Integer)
        If Not dtSONReports Is Nothing Then
            Dim drDefRpt As DataRow = dtSONReports.AsEnumerable().Where(Function(x) Not x.IsNull("JobID") AndAlso x.Field(Of Integer)("JobID") = jobID AndAlso Not x.IsNull("DefaultLoad") AndAlso x.Field(Of Boolean)("DefaultLoad") = True)(0)
            If drDefRpt IsNot Nothing Then
                SetComboBox(cmbModuleReports, ComboSelectBased.ValueBased, CInt(drDefRpt("DashboardID")))
            Else
                ReportViewer.Dashboard = Nothing
            End If
        Else
            ReportViewer.Dashboard = Nothing
        End If
    End Sub

    Private Sub DeleteDashboardReport(dashboardID As Integer)
        Dim parray()() As String = {
            New String() {"@DashboardID", dashboardID}
        }
        Dim strConnection As String = GetSQL(9321, parray)(0)
        Dim sqlParam As String = GetSQL(9321, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = System.Drawing.Color.Red
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

    Private Sub LoadDashboardReport()
        Try
            Dim dashboardXmlFile As String = Nothing
            Dim dashboardID As Integer = CType(cmbModuleReports.SelectedItem, clsComboBoxItem).Value
            Dim str = dtSONReports.AsEnumerable().Where(Function(x) x.Field(Of Integer)("DashboardID") = dashboardID)(0)("DashboardFile").ToString
            str = str.Replace("''", "'")

            If str.Trim.Contains("<?xml") Then
                dashboardXmlFile = str
            Else
                dashboardXmlFile = GetDecryptedConnectionString(str)
            End If

            Dim ms As New System.IO.MemoryStream()
            ms = StringToStream(dashboardXmlFile)

            If ms.Length <> 0 Then
                ReportViewer.LoadDashboard(ms)
            Else
                ReportViewer.Dashboard = Nothing
            End If
            Application.UseWaitCursor = False

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

End Class

Class JobRunManualClass

    Public jobID As Integer
    Public RunManualStatus As Integer
    Public nd As TreeListViewNode
    Public Event ThreadComplete(nd As TreeListViewNode, Status As Integer, ti As Thread)

    Sub RunManual()
        Try
            RunManualStatus = 1
            UpdateJobRunManualStatus(RunManualStatus, jobID)

            'RunManualStatus = 0
            'UpdateTemplateLastStatus(RunManualStatus, RunManualStatus)

        Catch ex As Exception
            RunManualStatus = 0
            UpdateJobRunManualStatus(RunManualStatus, jobID)
        Finally
            RaiseEvent ThreadComplete(nd, RunManualStatus, Thread.CurrentThread)
        End Try
    End Sub

End Class