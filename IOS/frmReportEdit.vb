Imports System.DirectoryServices
Imports System.Drawing.Drawing2D
Imports dotnetCHARTING.WinForms
Imports IOS.Configuration.ReportManager
Imports IOS.DataLibrary
Imports IOS.Library
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports Powerpoint = Microsoft.Office.Interop.PowerPoint
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Presentation
Imports DevExpress.Spreadsheet
Imports DXChart = DevExpress.Spreadsheet.Charts.Chart
Imports DevExpress.XtraEditors
Imports System.IO
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.Native.Sql
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.Pdf

Public Class frmReportEdit

#Region "Variables"

    Dim dtReports As New DataTable
    Dim slidePropertyG As New SlideProperties()
    Dim worsksheetPropertyG As New WorksheetProperties()
    Dim chartPropertyG As New ObjectChartProperties()
    Dim isObjectSelect As Boolean = False
    Dim isStyleBinded As Boolean = False
    Dim clickedNode As TreeListNode = Nothing
    Dim isIndexEventFired As Boolean = False
    Dim _numericUpDownList As New List(Of DevExpress.XtraEditors.SpinEdit)
    Dim objfrmTech As frmTechnology = Nothing
    Dim reportMethod As String = ReportMethodType.OpenXml
    Dim rptCurrOrConfig As String = Nothing
    Dim reportCreated As Boolean = True

    Dim ObjectSelectedFromTreeForCurrent As String = "N/A"
    Dim ObjectSelectedFromTreeTopXForCurrent As String = "N/A"

    Private _strNetwork As String = ""
    Private perSlideObjectID As Integer = Nothing
    Private ObjectsCharted As String
    Private cm_Chart_kpiname As String

    Private dsStats As DataSet
    Private dsStatsObjectTime As DataSet
    Private dsTopx As DataSet
    Private dsHistogramData As New DataSet
    Dim ChartConfig As New ChartConfigDataTables
    Dim dtPredefPeriod As New DataTable
    Dim reportType As String = Nothing
    Dim richTxtStr As RichTextString = Nothing

    Dim _reportProperties As ReportProperties = Nothing
    Dim objChartProperties As New ObjectChartProperties()
    Dim textboxProperties As New ObjectTextBoxProperties()
    Dim objSlideProperties As New SlideProperties()
    Dim objWorksheetProperties As New WorksheetProperties()

    ' For multiple chart objects on a single slide
    Private slidePosition As Integer = 1
    Private chartsPerSlide As Integer = 0
    Private drawingObjectId As UInteger = 0
    Dim dtChartStyleProperties As New DataTable
    Dim chartDataFirstColIndex As Integer = 0

    ' OpenXML presentation parts
    Dim slide As DocumentFormat.OpenXml.Presentation.Slide = Nothing
    Dim slidePart As SlidePart = Nothing
    Private dshbrd As Dashboard
    Dim dtObjectsPerSlide As New DataTable
    Private lstPdfFiles As List(Of String)

    Private tsmi_DB_Reports As ToolStripMenuItem
    Private tsmi_SON_Reports As ToolStripMenuItem

#End Region

    Enum ReportMethodType
        Interop = 0
        OpenXml = 1
        'Excel = 2
    End Enum

    Structure ChartConfigDataTables
        Public ChartGenerationDataTable As DataTable
        Public ChartFillingDataTable As DataTable
    End Structure

#Region "Form & Control's Event"

    Private Sub frmReportEdit_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        System.Threading.Thread.CurrentThread.CurrentUICulture = CultureUIDefault
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfoDefault
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            tlvReports.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Me.ConfigureReportForm("frmReportEdit")
            Me.BringToFront()
            tlvReports.SuspendLayout()
            tlvReports.Nodes.Clear()
            tlvReports.Columns.Clear()
            InitList()
            RefreshReportData()
            BindReport_Treelist()
            LoadAllDashboardReports()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            tlvReports.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            InitList()
            RefreshReportData()
            BindReport_Treelist()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnReportAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportAdd.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim objAddNewReport As New dlgAddNewReport()
            objAddNewReport.reportGroupId = CInt(TryCast(cmbReportGroup.SelectedItem, clsComboBoxItem).Value)
            objAddNewReport.ShowDialog()
            If objAddNewReport.DialogResult = DialogResult.OK Then
                btnRefresh_Click(Nothing, Nothing)
                ExpandTree(NewReportName.Trim.Substring(0, System.Math.Min(49, NewReportName.Trim.Length)))
                tlvReports.FocusedNode = tlvReports.FindNode(Function(x) x.GetDisplayText("Report Name") = NewReportName)
                SetMessag("A new report successfully added")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub propertyGridreport_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles propertyGridreport.PropertyValueChanged
        Try
            Dim changedPropertyItem As GridItem = e.ChangedItem
            Dim selectedNode As TreeListNode = tlvReports.FocusedNode
            Dim sqlCommand As String = Nothing
            If (Not selectedNode Is Nothing) Then
                If (selectedNode.Level = 0) Then
                    If (changedPropertyItem.Label.ToLower = "email") Then
                        sqlCommand = "Set EmailAddress=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "interval") Then
                        sqlCommand = "Set Interval=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "starttime") Then
                        sqlCommand = "Set StartTime=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "isenabled") Then
                        sqlCommand = "Set IsEnabled=" & Chr(39) & IIf(changedPropertyItem.Value.ToString.ToLower = "yes", 1, 0) & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "reporttype") Then
                        If selectedNode.Nodes.Count > 0 AndAlso selectedNode.Nodes(0).HasChildren = True Then
                            If selectedNode.Nodes(0).Nodes(0).Tag Is Nothing Then
                                sqlCommand = "Set ReportType=" & Chr(39) & IIf(changedPropertyItem.Value.ToString.ToLower = "select", "PowerPoint", changedPropertyItem.Value.ToString) & Chr(39)
                                tlvReports.FocusedNode.SetValue("ReportType", changedPropertyItem.Value.ToString)
                            Else
                                _reportProperties.ReportType = IIf(changedPropertyItem.Value.ToString = "PowerPoint", "Excel", "PowerPoint")
                            End If
                        Else
                            sqlCommand = "Set ReportType=" & Chr(39) & IIf(changedPropertyItem.Value.ToString.ToLower = "select", "PowerPoint", changedPropertyItem.Value.ToString) & Chr(39)
                            tlvReports.FocusedNode.SetValue("ReportType", changedPropertyItem.Value.ToString)
                        End If
                    End If
                    Try
                        If (sqlCommand IsNot Nothing) Then
                            clsSQLCommands.UpdateReportDetails(connStrIOSServer, sqlCommand, selectedNode.Tag)
                        End If
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    End Try
                ElseIf (selectedNode.Level = 1) Then
                    If (changedPropertyItem.Label.ToLower = "slidetitle") Then
                        sqlCommand = "Set SlideTitle=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "slidetext") Then
                        sqlCommand = "Set SlideText=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "slideordinal") Then
                        sqlCommand = "Set SlideOrdinal=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "worksheettitle") Then
                        sqlCommand = " Set SlideTitle=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "dashboardtabpages") Then
                        sqlCommand = " Set SelectedPages=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    End If
                    Try
                        If (sqlCommand IsNot Nothing) Then
                            clsSQLCommands.UpdateReportSlide(connStrIOSServer, sqlCommand, selectedNode.Tag, selectedNode.ParentNode.Tag)
                        End If
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    End Try
                    isObjectSelect = True
                    isIndexEventFired = True
                    cmbStyleName_SelectedIndexChanged(Nothing, Nothing)
                ElseIf (selectedNode.Level = 2) Then
                    If (changedPropertyItem.Label.ToLower = "predefinedtime") Then
                        sqlCommand = "Set PredefinedTime=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "topxrowcount") Then
                        sqlCommand = "Set TopXRowCount=" & changedPropertyItem.Value
                    ElseIf (changedPropertyItem.Label.ToLower = "resolution") Then
                        sqlCommand = "Set [Resolution]=" & Chr(39) & changedPropertyItem.Value & Chr(39)
                    ElseIf (changedPropertyItem.Label.ToLower = "objectsselected") Then
                        sqlCommand = "Set [ObjectsSelected]=" & Chr(39) & changedPropertyItem.Value.ToString.Replace("'", "''") & Chr(39)
                    End If
                    Try
                        If (sqlCommand IsNot Nothing) Then
                            clsSQLCommands.UpdateReportObject(connStrIOSServer, sqlCommand, selectedNode.Tag, selectedNode.ParentNode.Tag)
                        End If
                    Catch ex As Exception
                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    End Try
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim tlv As TreeList = tlvReports
        Dim nd As TreeListNode = tlv.FocusedNode
        If nd.Level = 1 Then
            Dim reportID As String = Nothing
            Dim reportName As String = Nothing
            Dim slideID As String = Nothing
            If (nd.Level = 1) Then
                reportID = nd.ParentNode.Tag
                reportName = nd.ParentNode.GetDisplayText("Report Name")
                slideID = nd.Tag
            Else
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim MyApplication As Powerpoint.Application = Nothing
            Dim MyPresentation As Powerpoint.Presentation = Nothing
            'Dim MySlide As Powerpoint.Slide
            Try
                MyApplication = New Powerpoint.Application()
                MyApplication.Visible = True
                MyApplication.WindowState = Powerpoint.PpWindowState.ppWindowMinimized
                Application.DoEvents()

                Dim previewPresentaionPath As String = GetUserDataPath() & "\Data\preview.ppt"
                If (System.IO.File.Exists(previewPresentaionPath)) Then
                    MyPresentation = MyApplication.Presentations.Open(previewPresentaionPath, , , True)
                Else
                    Dim ReportingTemplateFileName As String = GetConfigClientKeyValue("ReportingTemplateFileName")
                    Dim pptfile As String = Application.StartupPath & "\" & ReportingTemplateFileName
                    MyPresentation = MyApplication.Presentations.Open(pptfile, , , True)
                End If

                Dim tempPresentation As Powerpoint.Presentation = MyPresentation
                For Each existSlide As Microsoft.Office.Interop.PowerPoint.Slide In tempPresentation.Slides
                    MyPresentation.Slides(existSlide.SlideNumber).Delete()
                Next

                Dim realuser As String = Nothing
                realuser = Environment.UserName.ToString
                Dim title As String = nd.GetDisplayText("Report Name")
                Dim subtitle As String = "Contact: " & vbTab & vbTab & realuser & vbCr & "Creation Date: " & vbTab & Format(Now, "yyyy-MM-dd")

                Dim dtReportAll As DataTable = clsSQLCommands.GetSlidesByReportID(connStrIOSServer, reportID)
                Dim slideDistinct As DataTable = dtReportAll.AsDataView.ToTable(True, "SlideID", "SlideTitle", "SlideName", "SlideOrdinal", "SlideText")

                Dim dtObjectsPerSlide As DataTable = dtReportAll.Select("SlideID=" & Chr(39) & slideID & Chr(39), "ObjectOrdinal ASC").CopyToDataTable()
                If (dtObjectsPerSlide.Rows.Count > 0) Then
                    Dim slidePoperties As SlideProperties = New SlideProperties()
                    slidePoperties.SlideOrdinal = 0
                    slidePoperties.SlideText = dtObjectsPerSlide.Rows(0)("SlideText").ToString
                    slidePoperties.SlideTitle = dtObjectsPerSlide.Rows(0)("SlideTitle").ToString
                    Dim powerPointSlide As Powerpoint.Slide = CreatePowerPointSlide(slidePoperties, dtObjectsPerSlide.Rows(0)("SlideName").ToString, MyPresentation, reportName, realuser)
                    For Each drSlide As DataRow In dtObjectsPerSlide.Rows
                        Try
                            If (drSlide("ObjectType").ToString() = "Chart") Then
                                Dim objChartProperties As ObjectChartProperties = New ObjectChartProperties()
                                objChartProperties.Technology = drSlide("Technology").ToString
                                objChartProperties.Width = drSlide("ObjectWidth").ToString
                                objChartProperties.Height = drSlide("ObjectHeight")
                                objChartProperties.Top = drSlide("ObjectTopMargin")
                                objChartProperties.Left = drSlide("ObjectLeftMargin")
                                objChartProperties.ObjectScale = drSlide("ObjectScale")
                                CreateChartObjectOnSlide(objChartProperties, drSlide("ObjectName"), powerPointSlide)
                            ElseIf (drSlide("ObjectType").ToString() = "TextBox") Then
                                Dim textboxProperties As ObjectTextBoxProperties = New ObjectTextBoxProperties()
                                textboxProperties.Top = drSlide("ObjectTopMargin").ToString()
                                textboxProperties.Left = drSlide("ObjectLeftMargin").ToString()
                                textboxProperties.FontColor = Color.FromName(drSlide("TextBoxFontColor").ToString())
                                textboxProperties.TextBoxText = drSlide("TextBoxText").ToString()
                                textboxProperties.FontSize = drSlide("TextBoxFontSize").ToString()
                                textboxProperties.IsBold = drSlide("TextBoxFontIsBold").ToString()
                                textboxProperties.IsItalic = drSlide("TextBoxFontIsItalic").ToString()
                                textboxProperties.IsUnderline = drSlide("TextBoxFontIsUnderline").ToString()
                                textboxProperties.Width = drSlide("ObjectWidth")
                                textboxProperties.Height = drSlide("ObjectHeight").ToString()
                                textboxProperties.BorderSize = drSlide("TextBoxBorderSize").ToString()
                                textboxProperties.BoderColor = Color.FromName(drSlide("TextBoxBoderColor").ToString())
                                CreateTextBoxObjectOnSlide(textboxProperties, drSlide("ObjectName").ToString(), powerPointSlide)
                            End If ''Chart and TextBox Condistion
                        Catch ex As Exception
                            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                        End Try
                    Next
                End If
                MyPresentation.SaveAs(GetUserDataPath() & "\Data\preview", Powerpoint.PpSaveAsFileType.ppSaveAsPresentation, Microsoft.Office.Core.MsoTriState.msoCTrue)
                Application.DoEvents()
                MyApplication.WindowState = Powerpoint.PpWindowState.ppWindowMaximized
            Catch ex As Exception
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                MsgBox(ex.Message)
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            Finally
                GC.Collect()
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            End Try
        Else
            SetMessag("Preview show only for slide")
        End If
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnApplyStyle_Click(sender As Object, e As EventArgs) Handles btnApplyStyle.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim selectedNode As TreeListNode = tlvReports.FocusedNode
            Dim reportID As String = Nothing
            Dim slideID As String = Nothing
            Dim styleID As String = Nothing

            styleID = TryCast(cmbStyleName.SelectedItem, clsComboBoxItem).Value
            If (Not selectedNode Is Nothing) Then
                Dim selectedStyle As String = propertyGridreport.Tag
                If (selectedStyle = "Report") Then
                    Exit Sub
                ElseIf (selectedStyle = "Slide") Then
                    clsSQLCommands.UpdateStyleOnSlide(connStrIOSServer, selectedNode.ParentNode.Tag, selectedNode.Tag, styleID)
                    RefreshReportData()
                    SetMessag("Slide style successfully applied.")
                ElseIf (selectedStyle = "Chart") Then
                    clsSQLCommands.UpdateStyleOnObject(connStrIOSServer, selectedNode.Tag, styleID)
                    RefreshReportData()
                    SetMessag("Object style successfully applied.")
                ElseIf (selectedStyle = "TextBox") Then
                    clsSQLCommands.UpdateStyleOnObject(connStrIOSServer, selectedNode.Tag, styleID)
                    RefreshReportData()
                    SetMessag("Object style successfully applied.")
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbStyleName_MouseDown(sender As Object, e As MouseEventArgs) Handles cmbStyleName.MouseDown
        isIndexEventFired = True
    End Sub

    Private Sub txtSearchReport_TextChanged(sender As Object, e As EventArgs) Handles txtSearchReport.TextChanged
        Try
            BindReport_Treelist()
        Catch
        End Try
    End Sub

    Private Sub frmReportEdit_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Try
            sccTop.SplitterPosition = System.Math.Abs(sccTop.Width / 3) * 3
            sccRepEditor.SplitterPosition = System.Math.Abs(sccRepEditor.Width / 3) * 3
        Catch
        End Try
    End Sub

#End Region

#Region "Helper"

    Private Sub ConfigureReportForm(ByVal frmName As String)
        Dim counter As Integer = 0
        ConfigurForm(Me, frmName, counter)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)

        If Not form Is Nothing Then
            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing

            Dim formControls As List(Of Object) = New List(Of Object) From {
                tsmi_ReportSlideAdd, tsmi_ReportRename, tsmi_ReportDelete, tsmi_ReportRunConfigured, tsmi_ReportRunCurrent, tsmi_ReportLock, tsmi_ObjectRename, tsmi_ObjectChartDelete,
                tsmt_SlideRename, tsmt_SlideDelete, tsmi_SlideMoveUp, tsmi_SlideMoveDown, tsmi_SlideObjectAdd, ObjectAddToolStripMenuItem, ObjectRemoveToolStripMenuItem, ObjectMoveUpToolStripMenuItem,
                ObjectMoveDownToolStripMenuItem, ToolStripMenuItem1, ToolStripMenuItem2, tsmi_ObjectChartMoveUp, tsmi_ObjectChartMoveDown, btnReportAdd
            }

            For Each frmControl As Object In formControls
                winCtrl = form.FindControlByName(frmControl.Name)
                If Not winCtrl Is Nothing Then
                    frmControl.Enabled = winCtrl.DefaultEnable
                    frmControl.visible = winCtrl.DefaultVisible
                End If
            Next

        End If
    End Sub

    Private Sub InitList()
        Dim dtReportGroups As DataTable = New DataTable
        'fill combobox  
        Try
            dtReportGroups = clsSQLCommands.GetReportGroups(connStrIOSServer, Environment.UserName)
            BindDevExComboBoxWithValueMember(cmbReportGroup, dtReportGroups, "ReportGroupID", "ReportGroupName", , True)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindReport_Treelist()
        Dim selectStatement As String = String.Empty
        If (txtSearchReport.Text.Trim.Length >= 3) Then
            selectStatement += "ReportName LIKE '%" & txtSearchReport.Text.Trim & "%' AND ReportGroupName=" & Chr(39) & cmbReportGroup.SelectedItem.ToString & Chr(39)
        End If
        If (dtReports Is Nothing) Then
            Exit Sub
        End If

        Dim dtReport As DataTable = Nothing
        If (String.IsNullOrEmpty(selectStatement)) Then
            dtReport = dtReports
        Else
            Dim dv As DataView = New DataView(dtReports, selectStatement, "", DataViewRowState.CurrentRows)
            dtReport = dv.ToTable()
        End If

        tlvReports.BeginUpdate()
        tlvReports.SuspendLayout()

        Try
            Dim colList() As String = {"Report Name", "ReportOwner", "ReportLocked", "ReportType", "Slide Name", "Object Name", "Object Type"}
            tlvReports.Columns.Clear()
            For i As Integer = 0 To colList.Length - 1
                Dim col1 As Columns.TreeListColumn = New Columns.TreeListColumn()
                col1.Caption = colList(i)
                col1.VisibleIndex = i
                If colList(i) = "Report Name" Then
                    tlvReports.AutoFillColumn = col1
                    col1.Visible = True
                ElseIf (colList(i) = "ReportOwner") Or (colList(i) = "ReportLocked") Or (colList(i) = "ReportType") Then
                    col1.Visible = False
                End If
                tlvReports.Columns.Add(col1)
            Next

            ' Clear all nodes
            tlvReports.Nodes.Clear()
            Dim nodeReport As TreeListNode = Nothing

            Dim dvObject As DataView = New DataView(dtReport)
            Dim cols(4) As String
            cols(0) = "ReportID"
            cols(1) = "ReportName"
            cols(2) = "ReportOwner"
            cols(3) = "ReportLocked"
            cols(4) = "ReportType"
            Dim disDataObject As DataTable = dvObject.ToTable(True, cols)

            For Each dr As DataRow In disDataObject.Rows

                nodeReport = tlvReports.Nodes.Add(New Object() {dr("ReportName").ToString.Trim, dr("ReportOwner").ToString.Trim, dr("ReportLocked").ToString.Trim, dr("ReportType").ToString.Trim})
                nodeReport.Tag = dr("ReportID").ToString.Trim

                Dim colsSlide(1) As String
                colsSlide(0) = "SlideID"
                colsSlide(1) = "SlideName"

                Dim disDataSlide As DataTable = dtReport.Select("ReportID = " & Chr(39) & dr("ReportID").ToString & Chr(39)).CopyToDataTable.DefaultView.ToTable(True, colsSlide)

                For Each drSlide As DataRow In disDataSlide.Rows
                    If (Not String.IsNullOrEmpty(drSlide("SlideID").ToString.Trim)) Then

                        Dim nodeSlide As TreeListNode = tlvReports.AppendNode(New Object() {"", "", "", "", drSlide("SlideName").ToString.Trim}, nodeReport)
                        nodeSlide.Tag = drSlide("SlideID").ToString.Trim

                        Dim colsObject(3) As String
                        colsObject(0) = "ObjectID"
                        colsObject(1) = "ObjectNameGUI"
                        colsObject(2) = "ObjectType"
                        colsObject(3) = "ObjectOrdinal"

                        Dim disDataObjects As DataTable = dtReport.Select("SlideID = " & Chr(39) & drSlide("SlideID").ToString & Chr(39), "ObjectOrdinal ASC").CopyToDataTable.DefaultView.ToTable(True, colsObject)

                        For Each drObject As DataRow In disDataObjects.Rows
                            If (Not String.IsNullOrEmpty(drObject("ObjectID").ToString.Trim)) Then

                                Dim nodeObject As TreeListNode = tlvReports.AppendNode(New Object() {"", "", "", "", "", drObject("ObjectNameGUI").ToString.Trim, drObject("ObjectType").ToString.Trim}, nodeSlide)
                                nodeObject.Tag = drObject("ObjectID").ToString.Trim

                            End If
                        Next
                    End If
                Next
            Next

            Me.tlvReports.ResumeLayout()

            If tlvReports.Nodes.Count > 0 Then
                tlvReports.SelectNode(tlvReports.Nodes(0))
                tlvReports.SetFocusedNode(tlvReports.Nodes(0))
                tlvReports.AutoFillColumn = tlvReports.Columns(0)
            End If

            tlvReports.EndUpdate()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Sub RefreshReportData()
        Try
            dtReports = clsSQLCommands.GetAllReport(connStrIOSServer, Environment.UserName)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RefreshReportTree(ByRef tl As DevExpress.XtraTreeList.TreeList)
        tl.SuspendLayout()

        For Each col As Columns.TreeListColumn In tl.Columns
            tl.BestFitColumns()
        Next

        'If tl.Columns.Count > 0 Then
        '    tl.Columns(0).Width = tl.Columns(0).Width + 10
        'End If

        tl.ResumeLayout()
        tl.Refresh()
    End Sub

    ''Private Sub cmReportEditor_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    ''    Try
    ''        Dim tlv As TreeList = CType(cmReportEditor.SourceControl, TreeList)
    ''        Dim nd As TreeListNode = tlv.FocusedNode

    ''        ReportDeleteToolStripMenuItem.Enabled = False
    ''        ReportAdjustToolStripMenuItem.Enabled = False
    ''        ReportRenameToolStripMenuItem.Enabled = False

    ''        If Not nd Is Nothing Then
    ''            If nd.Level = 0 Then
    ''                If nd.Tag.ToString.ToUpper = "TRUE" Then
    ''                    ReportLockToolStripMenuItem.Checked = True
    ''                    If System.Environment.UserName.ToString.ToLower = nd.GetDisplayText("Report Name").ToString.ToLower Then    ''.SubItems(1).Text.ToString.Trim.ToLower
    ''                        ReportDeleteToolStripMenuItem.Enabled = True
    ''                        ReportRenameToolStripMenuItem.Enabled = True
    ''                    End If
    ''                Else
    ''                    ReportLockToolStripMenuItem.Checked = False
    ''                    ReportDeleteToolStripMenuItem.Enabled = True
    ''                    ReportRenameToolStripMenuItem.Enabled = True
    ''                End If
    ''            End If
    ''            If nd.Level = 1 Then
    ''                If nd.ParentNode.Tag.ToString.ToUpper = "TRUE" Then
    ''                    If System.Environment.UserName.ToString.ToLower = nd.ParentNode.GetDisplayText("Report Name").ToString.ToLower Then        ''.SubItems(1).Text.ToString.Trim.ToLower
    ''                        ReportAdjustToolStripMenuItem.Enabled = True
    ''                    End If
    ''                Else
    ''                    ReportAdjustToolStripMenuItem.Enabled = True
    ''                End If

    ''                ToolStripTextBox1.Text = nd.GetDisplayText("Report Name")           ''nd.SubItems(5).Text
    ''                ToolStripTextBox2.Text = nd.GetDisplayText("Report Name")           ''nd.SubItems(6).Text
    ''                ToolStripTextBox2.Height = 69
    ''                ToolStripTextBox2.TextBox.Multiline = True
    ''                ToolStripTextBox2.TextBox.Height = 69
    ''                ToolStripTextBox2.TextBox.Top = 5
    ''                ToolStripTextBox2.TextBox.Left = 30
    ''            End If
    ''        End If
    ''    Catch ex As Exception
    ''    End Try
    ''End Sub

    Public Sub ObjectAddToSlide(ByVal slideID As Integer, ByVal objectName As String, ByVal objectStyleID As Integer, ByVal objecttech As String, objectNameGUI As String,
                                targetType As String, predefinedTime As String, manualStartTime As Date, manualEndTime As Date, resolution As String, objectsSelected As String,
                                topXShowObjects As String, topXDeltaInterval As String, counterType As String, aggregateTo As String, tagid As String, tags_Filter As String, purpose As String, topXRowCount As Integer, rptType As String)
        Try
            clsSQLCommands.InsertReportsObject(connStrIOSServer, slideID, objectName, objectStyleID, objecttech, objectNameGUI,
                                               targetType, predefinedTime, manualStartTime, manualEndTime, resolution, objectsSelected, topXShowObjects, topXDeltaInterval, counterType, aggregateTo, tagid, tags_Filter, purpose, topXRowCount)
            If rptType.ToLower = "powerpoint" Then
                clsSQLCommands.ManageStyle(connStrIOSServer, slideID)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Function SlideAddToReport(ByVal reportID As Integer, ByVal slideName As String, ByVal slideOrdinal As Integer) As Integer
        Try
            Dim dt As DataTable = clsSQLCommands.InsertReportSlide(connStrIOSServer, reportID, slideName, slideOrdinal)
            btnRefresh_Click(Nothing, Nothing)
            Return CInt(dt.Rows(0)(0))
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Public Sub AddNumericUpDown(ByRef NumUpDown As DevExpress.XtraEditors.SpinEdit)
        _numericUpDownList.Add(NumUpDown)
    End Sub

    Private Sub SetNumOfChartsPerRow(ByVal SetToOne As Boolean)
        Try
            For Each nud As DevExpress.XtraEditors.SpinEdit In _numericUpDownList
                If SetToOne = True Then
                    If nud.Value <> 1 Then
                        nud.Tag = nud.Tag + "_nud" + nud.Value.ToString
                        nud.Value = 1
                    End If
                Else
                    If Not nud.Tag Is Nothing Then
                        If nud.Tag.ToString.Contains("_nud") Then
                            nud.Value = nud.Tag.ToString.Substring(nud.Tag.ToString.Length - 1)
                            nud.Tag = nud.Tag.ToString.Substring(0, nud.Tag.ToString.Length - 6)
                        End If
                    End If
                End If
            Next
        Catch
        End Try
    End Sub

    Private Function GetChartFromTechnology(ByVal chartname As String, ByVal tech As String, ByRef ch As Chart) As Chart
        Try
            objfrmTech = Nothing
            If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech.ToUpper.Replace("TOPX_", ""))) Then
                frmMDI.OpenTechFormDynamically(tech.ToUpper.Replace("TOPX_", ""), objfrmTech, False, Nothing, False)
            Else
                objfrmTech = objFrmTechList.Where(Function(x) x.Network.Equals(tech.ToUpper.Replace("TOPX_", ""))).LastOrDefault()
            End If
            If objfrmTech IsNot Nothing Then
                If chartname.Contains("HOURLY") Then
                    For Each chrt As Chart In objfrmTech.tcTabControlHighStats.TabPages(objfrmTech.tcTabControlHighStats.TabPages.Count - 2).Controls
                        If chrt.TitleBox.HeaderLabel.Text = chartname Then
                            ch = chrt
                            Exit For
                        End If
                    Next
                ElseIf chartname.Contains("CLUSTER") Then
                    For Each tp As DevExpress.XtraTab.XtraTabPage In objfrmTech.tcTabControlHighStats.TabPages
                        For Each chrt As Chart In CType(tp.Controls(0), DevExpress.XtraTab.XtraTabControl).TabPages(CType(tp.Controls(0), DevExpress.XtraTab.XtraTabControl).TabPages.Count - 1).Controls(0).Controls
                            If chrt.TitleBox.HeaderLabel.Text = chartname Then
                                ch = chrt
                                Exit For
                            End If
                        Next
                    Next
                Else
                    Dim ChartSetName As String = Nothing
                    If tech.ToUpper.Contains("TOPX") Then
                        ChartSetName = objfrmTech.cmbChartSetNameTopX.Text.Trim
                    Else
                        ChartSetName = objfrmTech.cmbChartSetNameStats.Text.Trim
                    End If
                    Dim parray()() As String = {
                        New String() {"@ChartName", Chr(39) & chartname & Chr(39)},
                        New String() {"@Tech", Chr(39) & tech & Chr(39)}
                    }
                    ',New String() {"@ChartSetName", Chr(39) & ChartSetName & Chr(39)}

                    Dim sql As String = GetSQL(8508, parray)(1)
                    Dim connstring As String = GetSQL(8508, parray)(0)
                    Dim dtQODBC As DataTable = Nothing
                    dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)

                    If dtQODBC Is Nothing Then
                        Exit Function
                    End If

                    Dim drData As DataRow
                    drData = dtQODBC.Rows(0)

                    Dim tabindex As Integer = Nothing

                    Try
                        If drData("CategoryTab").ToString = "Custom" And drData("TechTab").ToString.ToUpper = "TOPX_" & objfrmTech.Network.ToUpper Then
                            tabindex = GetTabIndexOfCustom(objfrmTech.tcTabControlHighTopX)
                        Else
                            tabindex = CInt(drData("CategoryTabIndex"))
                        End If
                    Catch
                    End Try

                    ch = Nothing
                    Try
                        Dim chname As String = drData("ChartName")
                        Select Case tech.ToLower
                            Case objfrmTech.Network.ToLower
                                Dim tpIndex As Integer = objfrmTech.GetTabPageIndex(objfrmTech.tcTabControlHighStats, drData("ObjectTab"))
                                Dim xtcTech As DevExpress.XtraTab.XtraTabControl = CType(objfrmTech.tcTabControlHighStats.TabPages(tpIndex).Controls(0), DevExpress.XtraTab.XtraTabControl)
                                xtcTech.SelectedTabPageIndex = tabindex
                                ch = xtcTech.SelectedTabPage.Controls(0).Controls(drData("ChartIndex"))
                            Case "topx_" & objfrmTech.Network.ToLower
                                Dim tblayout As TableLayoutPanel = objfrmTech.tcTabControlHighTopX.TabPages(tabindex).Controls(0)
                                ch = tblayout.GetControlFromPosition(0, drData("ChartIndex"))
                        End Select
                        If ch Is Nothing Then
                            Dim chs() As System.Windows.Forms.Control = objfrmTech.Controls.Find(chname, True)
                            For Each chfound As Chart In chs
                                If tech.Contains(chfound.Tag.ToString) Then
                                    ch = chfound
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If

            If ch.Name = chartname Then
                Return ch
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return Nothing
    End Function

    Private Sub ChartCopyToClipboard(ByVal chartname As String, ByVal tech As String, ByVal size As Size)
        Try
            'get chart object - through chart config?
            'delete node
            Dim ch As Chart = New Chart()
            ch.AutoSize = False
            ch = Nothing
            objfrmTech = Nothing
            If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(tech.ToUpper.Replace("TOPX_", ""))) Then
                frmMDI.OpenTechFormDynamically(tech.ToUpper.Replace("TOPX_", ""), objfrmTech, False, Nothing, False)
            Else
                objfrmTech = objFrmTechList.Where(Function(x) x.Network.Equals(tech.ToUpper.Replace("TOPX_", ""))).LastOrDefault()
            End If
            If objfrmTech IsNot Nothing Then
                If chartname.Contains("HOURLY") Then
                    For Each chrt As Chart In objfrmTech.tcTabControlHighStats.TabPages(objfrmTech.tcTabControlHighStats.TabPages.Count - 2).Controls
                        If chrt.TitleBox.HeaderLabel.Text = chartname Then
                            ch = chrt
                            Exit For
                        End If
                    Next
                ElseIf chartname.Contains(" CLUSTER ") Then
                    For Each tp As DevExpress.XtraTab.XtraTabPage In objfrmTech.tcTabControlHighStats.TabPages
                        For Each chrt As Chart In CType(tp.Controls(0), DevExpress.XtraTab.XtraTabControl).TabPages(CType(tp.Controls(0), DevExpress.XtraTab.XtraTabControl).TabPages.Count - 1).Controls(0).Controls
                            If chrt.TitleBox.HeaderLabel.Text = chartname Then
                                ch = chrt
                                Exit For
                            End If
                        Next
                    Next
                Else
                    Dim ChartSetName As String = Nothing
                    If tech.ToUpper.Contains("TOPX") Then
                        ChartSetName = objfrmTech.cmbChartSetNameTopX.Text.Trim
                    Else
                        ChartSetName = objfrmTech.cmbChartSetNameStats.Text.Trim
                    End If
                    Dim parray()() As String = {
                        New String() {"@ChartName", Chr(39) & chartname & Chr(39)},
                        New String() {"@Tech", Chr(39) & tech & Chr(39)}
                    }
                    ',New String() {"@ChartSetName", Chr(39) & ChartSetName & Chr(39)}

                    Dim sql As String = GetSQL(8508, parray)(1)
                    Dim connstring As String = GetSQL(8508, parray)(0)
                    Dim dtQODBC As DataTable = Nothing
                    dtQODBC = DataAccessorODBC.GetDataTable(connstring, sql)

                    If dtQODBC Is Nothing Then
                        Exit Sub
                    End If

                    Dim drData As DataRow
                    drData = dtQODBC.Rows(0)

                    Dim tabindex As Integer = Nothing

                    Try
                        If drData("CategoryTab").ToString = "Custom" And drData("TechTab").ToString.ToUpper = "TOPX_" & objfrmTech.Network.ToUpper Then
                            tabindex = GetTabIndexOfCustom(objfrmTech.tcTabControlHighTopX)
                        Else
                            tabindex = CInt(drData("CategoryTabIndex"))
                        End If
                    Catch
                    End Try

                    ch = Nothing
                    Try
                        Dim chname As String = drData("ChartName")
                        Select Case tech.ToLower
                            Case objfrmTech.Network.ToLower
                                Dim tpIndex As Integer = objfrmTech.GetTabPageIndex(objfrmTech.tcTabControlHighStats, drData("ObjectTab"))
                                Dim xtcTech As DevExpress.XtraTab.XtraTabControl = CType(objfrmTech.tcTabControlHighStats.TabPages(tpIndex).Controls(0), DevExpress.XtraTab.XtraTabControl)
                                xtcTech.SelectedTabPageIndex = tabindex
                                ch = xtcTech.SelectedTabPage.Controls(0).Controls(drData("ChartIndex"))
                            Case "topx_" & objfrmTech.Network.ToLower
                                Dim tblayout As TableLayoutPanel = objfrmTech.tcTabControlHighTopX.TabPages(tabindex).Controls(0)
                                ch = tblayout.GetControlFromPosition(0, drData("ChartIndex"))
                        End Select
                        If ch Is Nothing Then
                            Dim chs() As System.Windows.Forms.Control = objfrmTech.Controls.Find(chname, True)
                            For Each chfound As Chart In chs
                                If tech.Contains(chfound.Tag.ToString) Then
                                    ch = chfound
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If

            ''If ch Is Nothing Then
            ''    MsgBox("Chart Not Found: " & chartname & " - " & tech)
            ''End If

            If ch.Name = chartname Then
                Dim size_old As Size = ch.ClientSize
                ch.Dock = DockStyle.None
                ch.AutoSize = False
                ch.Height = size.Height
                ch.Width = size.Width
                ch.ClientSize = size
                ch.AutoSizeMode = AutoSizeMode.GrowAndShrink
                If ch.Tag.ToString.Contains("TopX") Then
                    ch.Annotations(0).Position = New System.Drawing.Point(ch.Width - 70, 2)
                End If
                ch.Refresh()

                If Not ch Is Nothing And Not tech.Contains("Top") Then
                    'adding logo
                    ch.MarginTop = 0

                    ' Next we place the logo on the chart using an annotation.
                    'Dim a As New Annotation(New Background(Application.StartupPath & "\IOS_Logo_Chart.bmp"))
                    'a.DynamicSize = False
                    'a.Position = New System.Drawing.Point(ch.Width - 100, 10)
                    'a.Shadow.Visible = False
                    'ch.Annotations.Add(a)
                    'Dim j As Bitmap = ch.GetChartBitmap

                    Clipboard.SetImage(ch.GetChartBitmap)
                    'ch.Annotations.Remove(a)
                Else
                    Clipboard.SetImage(ch.GetChartBitmap)
                End If

                ch.ClientSize = size_old
                If ch.Tag.ToString.Contains("TopX") Then
                    ch.Annotations(0).Position = New System.Drawing.Point(ch.Width - 70, 2)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Function GetRealNameFromAd(ByVal UsernameToFind As String) As String
        Using searcher As New DirectorySearcher(New DirectoryEntry())
            searcher.PageSize = 1000
            searcher.SearchScope = SearchScope.Subtree
            searcher.Filter = "(&(samAccountType=805306368)(sAMAccountName=" & UsernameToFind & "))"
            searcher.ClientTimeout = New TimeSpan(0, 0, 2)
            searcher.ServerTimeLimit = New TimeSpan(0, 0, 2)
            Using Results As SearchResultCollection = searcher.FindAll
                If Results Is Nothing OrElse Results.Count <> 1 Then
                    Throw New ApplicationException("Invalid number of results returned - either no users were found or more than one user account was found")
                End If
                Using UserDE As DirectoryEntry = Results(0).GetDirectoryEntry
                    Return CStr(UserDE.Properties("givenName").Value) & " " & CStr(UserDE.Properties("sn").Value)
                End Using
            End Using
        End Using
    End Function

    Private Function GetTabIndexOfCustom(ByRef tbc As DevExpress.XtraTab.XtraTabControl) As Integer
        Dim i As Integer = 0
        For Each tp As DevExpress.XtraTab.XtraTabPage In tbc.TabPages
            If tp.Name = "Custom" Then
                Return i
            End If
            i = i + 1
        Next
        Return Nothing
    End Function

    Private Sub ReportLockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim log As String = Nothing
        Try
            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode

            If Not nd Is Nothing Then
                'delete node
                If System.Environment.UserName.ToString.ToLower = nd.GetDisplayText("Report Name").ToLower Then   ''.SubItems(1).Text.ToString.Trim.ToLower
                    Dim locked As Integer = 0
                    If nd.Tag.ToString.ToUpper = "TRUE" Then locked = 0 Else locked = 1

                    Dim parray()() As String = {
                        New String() {"@ReportID", nd.Tag},
                        New String() {"@ReportLocked", locked}
                    }

                    Dim sql As String = GetSQL(8510, parray)(1)
                    Dim connstring As String = GetSQL(8510, parray)(0)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                    btnRefresh_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_EditTitle(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripTextBox1.KeyDown
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If e.KeyCode = Keys.Enter Then
                'update record
                Dim nd As TreeListNode = tlvReports.FocusedNode
                If Not nd Is Nothing Then
                    Dim parray()() As String = {
                        New String() {"@ReportID", nd.ParentNode.Tag},
                        New String() {"@ReportDetailID", nd.GetDisplayText("Report Name")},
                        New String() {"@SlideTitle", Chr(39) & ToolStripTextBox1.Text & Chr(39)}
                    }
                    Dim sql As String = GetSQL(8511, parray)(1)
                    Dim connstring As String = GetSQL(8511, parray)(0)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                    cmsReport.Close()

                    btnRefresh_Click(Nothing, Nothing)
                    tlvReports.Nodes(nd.ParentNode.Tag).Expand()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_EditText(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripTextBox2.KeyDown
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If e.Control And e.KeyCode = Keys.Enter Then
                'update record
                Dim nd As TreeListNode = tlvReports.FocusedNode
                If Not nd Is Nothing Then
                    frmMapWindow.Refresh_IOSSQL()
                    Dim parray()() As String = {
                        New String() {"@ReportID", nd.ParentNode.Tag},
                        New String() {"@ReportDetailID", nd.GetDisplayText("Report Name")},
                        New String() {"@SlideText", Chr(39) & ToolStripTextBox2.Text & Chr(39)}
                    }

                    Dim sql As String = GetSQL(8512, parray)(1)
                    Dim connstring As String = GetSQL(8512, parray)(0)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                    cmsReport.Close()

                    btnRefresh_Click(Nothing, Nothing)
                    tlvReports.Nodes(nd.ParentNode.Tag).Expand()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Common Methods"

    Public Function IsStringDBNull(ByVal source As Object, ByVal defaultValue As Boolean) As Boolean 'TODO
        If source Is DBNull.Value Then
            Return defaultValue
        Else
            Return source
        End If
    End Function

    Public Sub SetMessag(ByVal message As String)
        lblMSG.ForeColor = Color.Red
        lblMSG.Visible = True
        lblMSG.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblMSG.Text = ""
        lblMSG.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Public Sub ExpandTree(ByVal nodeName As String, Optional nodePerantName As String = Nothing)
        Dim foundNode As TreeListNode = Nothing
        tlvReports.SuspendLayout()
        tlvReports.ResumeLayout()
        If (nodePerantName IsNot Nothing) Then
            If (tlvReports IsNot Nothing) AndAlso (tlvReports.Nodes.Count > 0) Then
                tlvReports.FindNode(Function(x) x.Tag = nodePerantName).Expand()
                foundNode = tlvReports.FindNode(Function(x) x.Tag = nodePerantName)
            End If
        Else
            If tlvReports.FindNode(Function(x) x("Report Name") = nodeName) IsNot Nothing Then
                tlvReports.FocusedNode = tlvReports.FindNode(Function(x) x.GetDisplayText("Report Name") = nodeName)
                tlvReports.FocusedNode.Expand()
                ''tlvReports.FocusedNode.EnsureVisible()
            End If
        End If

        If (foundNode IsNot Nothing) Then
            tlvReports.FocusedNode = GetNodeFromName(foundNode, nodeName)
            foundNode.Expand()
            ''tlvReports.FocusedNode.EnsureVisible()
        End If
        tlvReports.Update()
    End Sub

    Private Function GetNodeFromName(ByVal nodes As TreeListNode, ByVal nodeName As String) As TreeListNode
        Dim foundNode As TreeListNode = Nothing
        If (nodes Is Nothing Or String.IsNullOrEmpty(nodeName)) Then
            Return foundNode
        End If
        For Each tn As TreeListNode In nodes.Nodes
            If (tn.GetDisplayText("Report Name").ToLower = nodeName.ToLower) Then
                Return tn
            ElseIf (tn.GetDisplayText("Slide Name").ToLower = nodeName.ToLower) Then
                Return tn
            ElseIf (tn.Nodes.Count > 0) Then
                foundNode = GetNodeFromName(tn, nodeName)
            End If
            If (foundNode IsNot Nothing) Then
                Return foundNode
            End If
        Next
        Return Nothing
    End Function

#End Region

#Region "TreeListView"

    Private Sub tlvReports_AfterCollapse(sender As Object, e As NodeEventArgs) Handles tlvReports.AfterCollapse
        ''RefreshReportTree(tlvReports)
    End Sub

    Private Sub tlvReports_AfterExpand(ByVal sender As Object, ByVal e As NodeEventArgs) Handles tlvReports.AfterExpand
        ''RefreshReportTree(tlvReports)
    End Sub

    Private Sub tlvReports_MouseUp(sender As Object, e As MouseEventArgs) Handles tlvReports.MouseUp
        Dim mousePosition As New System.Drawing.Point(e.X, e.Y)
        clickedNode = Me.tlvReports.GetNodeAt(mousePosition)
        If (e.Button = MouseButtons.Left) Then
            Dim node As TreeListNode = Me.tlvReports.GetNodeAt(mousePosition)
            If (node IsNot Nothing) Then
                Dim comboItem As clsComboBoxItem = Nothing
                If (node.Level = 0) Then
                    propertyGridreport.SelectedObject = BindReportProperties(node.Tag)
                    propertyGridreport.Tag = "Report"
                    SetStyleControls(False)
                    btnApplyStyle.Enabled = False
                    btnPreview.Enabled = False
                    Me.reportType = node("ReportType").ToString.Trim
                    LoadReportStatus(node.Tag)
                ElseIf (node.Level = 1) Then
                    isObjectSelect = True
                    isStyleBinded = False
                    BindSlideStyleCmb()
                    SetStyleControls(True)
                    btnCreateStyle.Enabled = False
                    Dim dt As DataTable = dtReports.Select("ReportID = " & Chr(39) & node.ParentNode.Tag & Chr(39) & " AND SlideID = " & Chr(39) & node.Tag & Chr(39)).CopyToDataTable().DefaultView.ToTable(True, "SlideStyleID")
                    Me.reportType = node.ParentNode("ReportType").ToString.Trim
                    If (dt.Rows.Count > 0) Then
                        comboItem = GetComboItemFromValue(Convert.ToInt32(dt.Rows(0)(0)), cmbStyleName)
                    End If
                    btnApplyStyle.Enabled = IsApplicable(node.ParentNode("ReportOwner").ToString, CBool(node.ParentNode("ReportLocked").ToString))
                    btnPreview.Enabled = True
                ElseIf (node.Level = 2 AndAlso node("Object Type").ToString.ToLower = "chart") Then
                    Me.reportType = node.ParentNode.ParentNode("ReportType").ToString.Trim
                    isObjectSelect = True
                    BindObjectStyleCmb("Chart")
                    SetStyleControls(True)
                    Dim dt As DataTable = dtReports.Select("SlideID = " & Chr(39) & node.ParentNode.Tag & Chr(39) & " AND ObjectID = " & Chr(39) & node.Tag & Chr(39)).CopyToDataTable().DefaultView.ToTable(True, "ObjectStyleID")
                    If (dt.Rows.Count > 0) Then
                        comboItem = GetComboItemFromValue(Convert.ToInt32(dt.Rows(0)(0)), cmbStyleName)
                    End If
                    btnApplyStyle.Enabled = IsApplicable(node.ParentNode.ParentNode("ReportOwner").ToString, CBool(node.ParentNode.ParentNode("ReportLocked").ToString))
                    btnPreview.Enabled = False
                ElseIf (node.Level = 2 AndAlso node("Object Type").ToString.ToLower = "textbox") Then
                    isObjectSelect = True
                    BindObjectStyleCmb("TextBox")
                    SetStyleControls(True)
                    Dim dt As DataTable = dtReports.Select("SlideID = " & Chr(39) & node.ParentNode.Tag & Chr(39) & " AND ObjectID = " & Chr(39) & node.Tag & Chr(39)).CopyToDataTable().DefaultView.ToTable(True, "ObjectStyleID")
                    If (dt.Rows.Count > 0) Then
                        comboItem = GetComboItemFromValue(Convert.ToInt32(dt.Rows(0)(0)), cmbStyleName)
                    End If
                    btnApplyStyle.Enabled = IsApplicable(node.ParentNode.ParentNode("ReportOwner").ToString, CBool(node.ParentNode.ParentNode("ReportLocked").ToString))
                    btnPreview.Enabled = False
                End If
                If (comboItem IsNot Nothing) Then
                    isIndexEventFired = True
                    cmbStyleName.SelectedItem = comboItem
                    isIndexEventFired = True
                Else
                    cmbStyleName.SelectedIndex = 0
                End If
            End If
        End If
        Dim selectedNode As TreeListNode = Me.tlvReports.GetNodeAt(mousePosition)
        If (selectedNode IsNot Nothing) Then
            ScrollAtLastNode(selectedNode)
        End If
    End Sub

    Private Sub LoadReportStatus(reportID As Integer)
        Dim parray()() As String = {
            New String() {"@ReportID", reportID}
        }
        Dim sql As String = GetSQL(8551, parray)(1)
        Dim connstring As String = GetSQL(8551, parray)(0)
        Dim dt = DataAccessorODBC.GetDataTable(connstring, sql)
        LoadGridWithHyperlink(gcReportHistory, gvReportHistory, dt, "WebLink")
    End Sub

    Private Function IsApplicable(ByVal reportOwner As String, ByVal isLocked As Boolean) As Boolean
        If System.Environment.UserName.ToString.ToLower = reportOwner.ToLower Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ScrollAtLastNode(ByVal selectedNode As TreeListNode)
        If (selectedNode.NextNode Is Nothing) Then
            ''tlvReports.EnsureVisible(selectedNode.LastNode)   'TODO
        End If
    End Sub

#End Region

#Region "Objects Style"

    Private Function BindReportProperties(ByVal reportID As Integer) As ReportProperties
        _reportProperties = New ReportProperties()
        Dim dtReportProperties As DataTable = clsSQLCommands.GetReportProperties(connStrIOSServer, reportID)
        If (dtReportProperties.Rows.Count > 0) Then
            _reportProperties.ReportName = dtReportProperties.Rows(0)("ReportName")
            _reportProperties.ReportOwner = dtReportProperties.Rows(0)("ReportOwner")
            _reportProperties.ReportLock = IsStringDBNull(dtReportProperties.Rows(0)("ReportLocked"), False)
            _reportProperties.ReportGroupName = nZ(dtReportProperties.Rows(0)("ReportGroupName"), "")
            _reportProperties.Email = IIf(IsDBNull(dtReportProperties.Rows(0)("EmailAddress")), "", dtReportProperties.Rows(0)("EmailAddress"))

            If IsDBNull(dtReportProperties.Rows(0)("StartTime")) Then
                _reportProperties.StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            Else
                _reportProperties.StartTime = CDate(dtReportProperties.Rows(0)("StartTime")).ToString("yyyy-MM-dd HH:mm")
            End If

            _reportProperties.Interval = IIf(IsDBNull(dtReportProperties.Rows(0)("Interval")), "Select", dtReportProperties.Rows(0)("Interval"))
            If (IsDBNull(dtReportProperties.Rows(0)("IsEnabled"))) Then
                _reportProperties.IsEnabled = "Select"
            Else
                If (CBool(dtReportProperties.Rows(0)("IsEnabled")) = True) Then
                    _reportProperties.IsEnabled = "Yes"
                Else
                    _reportProperties.IsEnabled = "No"
                End If
            End If
            _reportProperties.ReportType = IIf(IsDBNull(dtReportProperties.Rows(0)("ReportType")), "Select", dtReportProperties.Rows(0)("ReportType"))
            Me.reportType = _reportProperties.ReportType
        End If
        Return _reportProperties
    End Function

    Private Function GetSlideStylePropeties(ByVal styleID As String, ByVal isBySlide As Boolean) As SlideProperties
        Dim dtSlideStyle As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, clsSQLCommands.GetSlideStylePropetiesQuery(styleID, isBySlide))
        Dim _slideProperties As SlideProperties = New SlideProperties()
        If (dtSlideStyle.Rows.Count > 0) Then
            slidePropertyG.Height = Convert.ToInt32(dtSlideStyle.Rows(0)("SlideHeight"))
            slidePropertyG.Width = Convert.ToInt32(dtSlideStyle.Rows(0)("SlideWidth"))
            slidePropertyG.Orientation = dtSlideStyle.Rows(0)("SlideOrientation")
            slidePropertyG.StyleOwner = dtSlideStyle.Rows(0)("StyleOwner")
            If IsDBNull(dtSlideStyle.Rows(0)("DashboardID")) Then
                slidePropertyG.TabPages = "All"
            Else
                slidePropertyG.TabPages = IIf(IsDBNull(dtSlideStyle.Rows(0)("DashboardTabPages")), "All", dtSlideStyle.Rows(0)("DashboardTabPages"))
                slidePropertyG.SelectedPages = IIf(IsDBNull(dtSlideStyle.Rows(0)("SelectedPages")), "", dtSlideStyle.Rows(0)("SelectedPages"))
            End If
            If (isObjectSelect) Then
                    slidePropertyG.SlideOrdinal = Convert.ToInt32(dtSlideStyle.Rows(0)("SlideOrdinal"))
                    slidePropertyG.SlideText = nZ(dtSlideStyle.Rows(0)("SlideText"), "")
                    slidePropertyG.SlideTitle = nZ(dtSlideStyle.Rows(0)("SlideTitle"), "")
                    slidePropertyG.SlideName = nZ(dtSlideStyle.Rows(0)("SlideName"), "")
                    isObjectSelect = False
                End If
                _slideProperties = slidePropertyG
            End If
            Return _slideProperties
    End Function

    Private Function GetWorksheetPropeties(ByVal styleID As String, ByVal isByWorksheet As Boolean) As WorksheetProperties
        Dim dtSlideStyle As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, clsSQLCommands.GetSlideStylePropetiesQuery(styleID, isByWorksheet))
        Dim _wsProperties As WorksheetProperties = New WorksheetProperties()
        If (dtSlideStyle.Rows.Count > 0) Then
            If (isObjectSelect) Then
                worsksheetPropertyG.WorksheetOrdinal = Convert.ToInt32(dtSlideStyle.Rows(0)("SlideOrdinal"))
                worsksheetPropertyG.WorksheetTitle = nZ(dtSlideStyle.Rows(0)("SlideTitle"), "")
                isObjectSelect = False
            End If
            _wsProperties = worsksheetPropertyG
        End If
        Return _wsProperties
    End Function

    Private Function GetChartStylePropeties(ByVal styleID As String, ByVal isByObject As Boolean) As ObjectChartProperties
        Dim dtObjectStyle As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, clsSQLCommands.GetChartStylePropetiesQuery(styleID, isByObject))
        Dim _objectProperties As ObjectChartProperties = New ObjectChartProperties()
        If (dtObjectStyle.Rows.Count > 0) Then
            chartPropertyG.StyleName = dtObjectStyle.Rows(0)("ObjectStyleName")
            chartPropertyG.ObjectScale = dtObjectStyle.Rows(0)("ObjectScale")
            chartPropertyG.ObjectType = dtObjectStyle.Rows(0)("ObjectType")
            chartPropertyG.Left = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectLeftMargin"))
            chartPropertyG.Top = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectTopMargin"))
            chartPropertyG.Width = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectWidth"))
            chartPropertyG.Height = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectHeight"))
            chartPropertyG.StyleOwner = dtObjectStyle.Rows(0)("StyleOwner")

            Try
                chartPropertyG.TargetType = IIf(IsDBNull(dtObjectStyle.Rows(0)("TargetType")), "", dtObjectStyle.Rows(0)("TargetType"))
                If (IsDBNull(dtObjectStyle.Rows(0)("ManualStartTime"))) Then
                    chartPropertyG.ManualStartTime = Now().ToString("yyyy/MM/dd")
                Else
                    chartPropertyG.ManualStartTime = CDate(dtObjectStyle.Rows(0)("ManualStartTime")).ToString("yyyy/MM/dd")
                End If
                If (IsDBNull(dtObjectStyle.Rows(0)("ManualEndTime"))) Then
                    chartPropertyG.ManualEndTime = Now().ToString("yyyy/MM/dd")
                Else
                    chartPropertyG.ManualEndTime = CDate(dtObjectStyle.Rows(0)("ManualEndTime")).ToString("yyyy/MM/dd")
                End If
                chartPropertyG.ObjectsSelected = IIf(IsDBNull(dtObjectStyle.Rows(0)("ObjectsSelected")), "", dtObjectStyle.Rows(0)("ObjectsSelected").ToString)
                chartPropertyG.CounterType = IIf(IsDBNull(dtObjectStyle.Rows(0)("CounterType")), "", dtObjectStyle.Rows(0)("CounterType").ToString)
                chartPropertyG.TopXShowObjects = IIf(IsDBNull(dtObjectStyle.Rows(0)("TopX_ShowObjects")), "", dtObjectStyle.Rows(0)("TopX_ShowObjects").ToString)
                chartPropertyG.TopXDeltaInterval = IIf(IsDBNull(dtObjectStyle.Rows(0)("TopX_DeltaInterval")), "", dtObjectStyle.Rows(0)("TopX_DeltaInterval").ToString)
                chartPropertyG.AggregateTo = IIf(IsDBNull(dtObjectStyle.Rows(0)("AggregateTo")), "", dtObjectStyle.Rows(0)("AggregateTo").ToString)
                chartPropertyG.TagID = IIf(IsDBNull(dtObjectStyle.Rows(0)("TagID")), "", dtObjectStyle.Rows(0)("TagID").ToString)
                chartPropertyG.Tags_Filter = IIf(IsDBNull(dtObjectStyle.Rows(0)("Tags_Filter")), "", dtObjectStyle.Rows(0)("Tags_Filter").ToString)
                chartPropertyG.TopXRowCount = IIf(IsDBNull(dtObjectStyle.Rows(0)("TopXRowCount")), 20, CInt(dtObjectStyle.Rows(0)("TopXRowCount")))
                chartPropertyG.Purpose = IIf(IsDBNull(dtObjectStyle.Rows(0)("Purpose").ToString), "", dtObjectStyle.Rows(0)("Purpose").ToString)

                Dim layerProperties As New CustomClass()
                propertyGridreport.SelectedObject = layerProperties

                Dim propPredefTime As New CustomProperty("Setting", "PredefinedTime", "ComboBoxLayer", "Chart Predefined Time", False, IIf(IsDBNull(dtObjectStyle.Rows(0)("PredefinedTime")), "", dtObjectStyle.Rows(0)("PredefinedTime")))
                layerProperties.Add(propPredefTime)
                chartPropertyG.PredefinedTime = propPredefTime.Value

                Dim propResolution As New CustomProperty("Setting", "Resolution", "ComboBoxLayer", "Chart Resolution", False, IIf(IsDBNull(dtObjectStyle.Rows(0)("Resolution")), "", dtObjectStyle.Rows(0)("Resolution")))
                layerProperties.Add(propResolution)
                chartPropertyG.Resolution = propResolution.Value
            Catch ex As Exception
            End Try

            If (isByObject) Then
                chartPropertyG.Technology = dtObjectStyle.Rows(0)("Technology")
                isObjectSelect = False
            End If
        End If
        _objectProperties = chartPropertyG
        Return _objectProperties
    End Function

    Private Function GetTextStylePropeties(ByVal styleID As String, ByVal isByObject As Boolean) As ObjectTextBoxProperties
        'Dim sqlCommand As String = Nothing
        'If (isByObject) Then
        '    sqlCommand = "SELECT RO.[ObjectStyleID],[ObjectType],[ObjectStyleName],[ObjectTopMargin],[ObjectLeftMargin],[TextBoxBoderColor],[TextBoxBorderSize],[TextBoxText],[TextBoxFontColor],[TextBoxFontSize],[TextBoxFontIsBold],[TextBoxFontIsItalic],[TextBoxFontIsUnderline],[TextBoxFontName],Technology,ObjectWidth,ObjectHeight,StyleOwner From IOS_Reports_ObjectStyles ROS"
        '    sqlCommand = sqlCommand + " RIGHT JOIN (Select ObjectID,Technology,ObjectStyleID From IOS_Reports_Objects WHERE ObjectID='" & styleID & "') RO ON RO.ObjectStyleID=ROS.ObjectStyleID"
        '    isObjectSelect = False
        'Else
        '    sqlCommand = "SELECT [ObjectStyleID],[ObjectType],[ObjectStyleName],[ObjectTopMargin],[ObjectLeftMargin],[TextBoxBoderColor],[TextBoxBorderSize],[TextBoxText],[TextBoxFontColor],[TextBoxFontSize],[TextBoxFontIsBold],[TextBoxFontIsItalic],[TextBoxFontIsUnderline],[TextBoxFontName],ObjectWidth,ObjectHeight,StyleOwner From IOS_Reports_ObjectStyles WHERE ObjectType='TextBox' AND ObjectStyleID=" & styleID
        'End If

        If (isByObject) Then
            isObjectSelect = False
        End If

        Dim dtObjectStyle As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, clsSQLCommands.GetTextStylePropetiesQuery(styleID, isByObject))
        Dim _objectProperties As ObjectTextBoxProperties = New ObjectTextBoxProperties()
        If (dtObjectStyle.Rows.Count > 0) Then
            _objectProperties.StyleName = nZ(dtObjectStyle.Rows(0)("ObjectStyleName"), "")
            _objectProperties.ObjectType = dtObjectStyle.Rows(0)("ObjectType")
            _objectProperties.Left = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectLeftMargin"))
            _objectProperties.Top = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectTopMargin"))
            _objectProperties.BoderColor = Color.FromName(dtObjectStyle.Rows(0)("TextBoxBoderColor"))
            _objectProperties.BorderSize = dtObjectStyle.Rows(0)("TextBoxBorderSize")
            _objectProperties.TextBoxText = dtObjectStyle.Rows(0)("TextBoxText")
            _objectProperties.FontColor = Color.FromName(dtObjectStyle.Rows(0)("TextBoxFontColor"))
            _objectProperties.FontSize = dtObjectStyle.Rows(0)("TextBoxFontSize")
            _objectProperties.IsBold = dtObjectStyle.Rows(0)("TextBoxFontIsBold")
            _objectProperties.IsItalic = dtObjectStyle.Rows(0)("TextBoxFontIsItalic")
            _objectProperties.IsUnderline = dtObjectStyle.Rows(0)("TextBoxFontIsUnderline")
            _objectProperties.FontName = dtObjectStyle.Rows(0)("TextBoxFontName")
            _objectProperties.Width = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectWidth"))
            _objectProperties.Height = Convert.ToInt32(dtObjectStyle.Rows(0)("ObjectHeight"))
            _objectProperties.StyleOwner = dtObjectStyle.Rows(0)("StyleOwner")
        End If
        Return _objectProperties
    End Function

    Private Sub BindSlideStyleCmb()
        'Dim sqlCommand As String = "Select SlideStyleID,SlideStyleName,StyleOwner from IOS_Reports_SlideStyles order by SlideStyleID"
        Dim dtSlideStyle As DataTable = clsSQLCommands.GetReportsSlideStyles(connStrIOSServer)
        BindDevExComboBoxWithTagMember(cmbStyleName, dtSlideStyle, "SlideStyleID", "SlideStyleName", "Select Style", "StyleOwner")
    End Sub

    Private Sub BindObjectStyleCmb(ByVal objectType As String)
        Dim dtObjectStyle As DataTable = clsSQLCommands.GetReportsObjectStyles(connStrIOSServer, objectType)
        BindDevExComboBoxWithTagMember(cmbStyleName, dtObjectStyle, "ObjectStyleID", "ObjectStyleName", "Select Style", "StyleOwner")
    End Sub

    Private Sub SetStyleControls(ByVal Isvisible As Boolean)
        lblStyleObject.Visible = Isvisible
        cmbStyleName.Visible = Isvisible
        btnCreateStyle.Enabled = Isvisible
    End Sub

    Private Sub cmbStyleName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStyleName.SelectedIndexChanged
        If (cmbStyleName.SelectedIndex > 0) Then

            Dim objItm As clsComboBoxItem = cmbStyleName.SelectedItem
            If (objItm.Tag.ToLower = Environment.UserName.ToLower) Then
                If (objItm.ToString.ToUpper = "DefaultSlideStyle".ToUpper Or objItm.ToString.ToUpper = "DefaultChartStyle".ToUpper Or objItm.ToString.ToUpper = "DefaultTextBoxStyle".ToUpper) Then
                    btnSaveStyle.Enabled = False
                Else
                    btnSaveStyle.Enabled = True
                End If
            Else
                btnSaveStyle.Enabled = False
            End If
            If (Not isIndexEventFired) Then
                isIndexEventFired = True
                Exit Sub
            End If
            isIndexEventFired = False
            BindStyleProperies(objItm.Value, isObjectSelect)
        Else
            btnSaveStyle.Enabled = False
        End If
    End Sub

    Private Sub BindStyleProperies(ByVal styleID As String, ByVal isByObject As Boolean)
        Dim selectedNode As TreeListNode = tlvReports.FocusedNode
        If (Not selectedNode Is Nothing) Then
            If (selectedNode.Level = 1) Then
                If Me.reportType.ToLower = "excel" Then
                    propertyGridreport.Tag = "Worksheet"
                    propertyGridreport.SelectedObject = GetWorksheetPropeties(If(isByObject, selectedNode.Tag, styleID), isByObject)
                Else
                    propertyGridreport.Tag = "Slide"
                    propertyGridreport.SelectedObject = GetSlideStylePropeties(If(isByObject, selectedNode.Tag, styleID), isByObject)
                End If
            ElseIf (selectedNode.Level = 2 AndAlso selectedNode("Object Type").ToLower = "chart") Then
                propertyGridreport.Tag = "Chart"
                propertyGridreport.SelectedObject = GetChartStylePropeties(If(isByObject, selectedNode.Tag, styleID), isByObject)
            ElseIf (selectedNode.Level = 2 AndAlso selectedNode("Object Type").ToLower = "textbox") Then
                propertyGridreport.Tag = "TextBox"
                propertyGridreport.SelectedObject = GetTextStylePropeties(If(isByObject, selectedNode.Tag, styleID), isByObject)
            End If
        End If
    End Sub

    Private Sub SlideStyleInsert(ByVal sytleName As String, ByRef slidePropertiess As SlideProperties)
        Try
            clsSQLCommands.InsertSlideStyle(connStrIOSServer, sytleName, slidePropertiess.Height, slidePropertiess.Width, slidePropertiess.Orientation)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub SlideStyleModify(ByVal sytleID As String, ByRef slidePropertiess As SlideProperties)
        Try
            clsSQLCommands.UpdateSlideStyle(connStrIOSServer, sytleID, slidePropertiess.Height, slidePropertiess.Width, slidePropertiess.Orientation)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub ChartStyleInsert(ByVal sytleName As String, ByRef objectPropertiess As ObjectChartProperties)
        Try
            clsSQLCommands.InsertChartStyle(connStrIOSServer, sytleName, objectPropertiess.ObjectType, objectPropertiess.Top, objectPropertiess.Left, objectPropertiess.ObjectScale, objectPropertiess.Width, objectPropertiess.Height)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub ChartStyleModify(ByVal sytleID As String, ByRef objectPropertiess As ObjectChartProperties)
        Try
            clsSQLCommands.UpdateChartStyle(connStrIOSServer, sytleID, objectPropertiess.ObjectType, objectPropertiess.Top, objectPropertiess.Left, objectPropertiess.ObjectScale, objectPropertiess.Width, objectPropertiess.Height)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub TextBoxStyleInsert(ByVal sytleName As String, ByRef objectPropertiess As ObjectTextBoxProperties)
        Try
            clsSQLCommands.InsertTextBoxStyle(connStrIOSServer, sytleName, objectPropertiess.ObjectType, objectPropertiess.Top, objectPropertiess.Left, objectPropertiess.BoderColor.Name, objectPropertiess.BorderSize, objectPropertiess.TextBoxText, objectPropertiess.FontColor.Name, objectPropertiess.FontSize, objectPropertiess.IsBold, objectPropertiess.IsItalic, objectPropertiess.IsUnderline, objectPropertiess.FontName, objectPropertiess.Width, objectPropertiess.Height)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub TextBoxStyleModify(ByVal sytleID As String, ByRef objectPropertiess As ObjectTextBoxProperties)
        Try
            clsSQLCommands.UpdateTextBoxStyle(connStrIOSServer, sytleID, objectPropertiess.Top, objectPropertiess.Left, objectPropertiess.BoderColor.Name, objectPropertiess.BorderSize, objectPropertiess.TextBoxText, objectPropertiess.FontColor.Name, objectPropertiess.FontSize, objectPropertiess.IsBold, objectPropertiess.IsItalic, objectPropertiess.IsUnderline, objectPropertiess.FontName, objectPropertiess.Width, objectPropertiess.Height)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnCreateStyle_Click(sender As Object, e As EventArgs) Handles btnCreateStyle.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim newStyleName As String = Nothing
            If (propertyGridreport.Tag = "Slide") Then
                newStyleName = InputBox("Enter New Style Name: ", "")
                If newStyleName <> "" And newStyleName IsNot Nothing Then
                    If (clsSQLCommands.IsValidSlideStyle(connStrIOSServer, newStyleName)) Then
                        Dim sStyle As SlideProperties = New SlideProperties()
                        SlideStyleInsert(newStyleName, sStyle)
                        BindSlideStyleCmb()
                        cmbStyleName.SelectedItem = GetComboItemFromText(newStyleName, cmbStyleName)
                        SetMessag("Slide style successfully added.")
                    Else
                        SetMessag("Slide style already exist.")
                    End If
                End If
            ElseIf (propertyGridreport.Tag = "Chart") Then
                newStyleName = InputBox("Enter New Style Name: ", "")
                If newStyleName <> "" And newStyleName IsNot Nothing Then
                    If (clsSQLCommands.IsObjectStyleValid(connStrIOSServer, newStyleName, "Chart")) Then
                        Dim sStyle As ObjectChartProperties = New ObjectChartProperties()
                        sStyle.ObjectType = "Chart"
                        ChartStyleInsert(newStyleName, sStyle)
                        BindObjectStyleCmb("Chart")
                        cmbStyleName.SelectedItem = GetComboItemFromText(newStyleName, cmbStyleName)
                        SetMessag("Chart object style successfully added.")
                    Else
                        SetMessag("Chart style already exist.")
                    End If
                End If
            ElseIf (propertyGridreport.Tag = "TextBox") Then
                newStyleName = InputBox("Enter New Style Name: ", "")
                If newStyleName <> "" And newStyleName IsNot Nothing Then
                    If (clsSQLCommands.IsObjectStyleValid(connStrIOSServer, newStyleName, "TextBox")) Then
                        Dim sStyle As ObjectTextBoxProperties = New ObjectTextBoxProperties()
                        sStyle.BoderColor = Color.Black
                        sStyle.FontColor = Color.Black
                        sStyle.ObjectType = "TextBox"
                        TextBoxStyleInsert(newStyleName, sStyle)
                        BindObjectStyleCmb("TextBox")
                        cmbStyleName.SelectedItem = GetComboItemFromText(newStyleName, cmbStyleName)
                        SetMessag("TextBox object style successfully added.")
                    Else
                        SetMessag("TextBox style already exist.")
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnSaveStyle_Click(sender As Object, e As EventArgs) Handles btnSaveStyle.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim selectedNode As TreeListNode = tlvReports.FocusedNode
            Dim slideID As String = Nothing
            Dim styleID As String = Nothing
            styleID = TryCast(cmbStyleName.SelectedItem, clsComboBoxItem).Value
            If (Not selectedNode Is Nothing) Then
                Dim selectedObject As String = propertyGridreport.Tag
                If (selectedObject = "Report") Then
                    Exit Sub
                ElseIf (selectedObject = "Slide") Then
                    Dim slideProperty As SlideProperties = propertyGridreport.SelectedObject
                    SlideStyleModify(styleID, slideProperty)
                    RefreshReportData()
                    SetMessag("Slide style successfully saved.")
                ElseIf (selectedObject = "Chart") Then
                    Dim chartProperty As ObjectChartProperties = propertyGridreport.SelectedObject
                    ChartStyleModify(styleID, chartProperty)
                    RefreshReportData()
                    SetMessag("Object style successfully saved.")
                ElseIf (selectedObject = "TextBox") Then
                    Dim textProperty As ObjectTextBoxProperties = propertyGridreport.SelectedObject
                    TextBoxStyleModify(styleID, textProperty)
                    RefreshReportData()
                    SetMessag("Object style successfully saved.")
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "ContextMenu"

#Region "Report ContextMenu"

    Private Sub tsmi_txtReportSlideAdd_KeyDown(sender As Object, e As KeyEventArgs) Handles tsmi_txtReportSlideAdd.KeyDown
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If e.KeyCode = Keys.Enter Then
                Dim nd As TreeListNode = tlvReports.FocusedNode
                If Not nd Is Nothing Then
                    If Not (String.IsNullOrEmpty(tsmi_txtReportSlideAdd.Text.Trim)) Then
                        Dim reportID As Integer = Convert.ToInt32(nd.Tag)
                        Dim slideOrdinal As Integer = clsSQLCommands.GetSlideOrdinal(connStrIOSServer, reportID)
                        SlideAddToReport(reportID, tsmi_txtReportSlideAdd.Text.Trim, slideOrdinal)
                        cmsReport.Close()
                        btnRefresh_Click(Nothing, Nothing)
                        ExpandTree(nd("Report Name"), Nothing)
                        SetMessag("New Slide successfully inserted.")
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cms_Report_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsReport.Opening
        Try
            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode
            tsmi_ReportDelete.Enabled = False
            ''ReportAdjustToolStripMenuItem.Enabled = False
            tsmi_ReportRename.Enabled = False

            If Not nd Is Nothing Then
                If nd.Level = 0 Then
                    If nd("ReportLocked").ToString.ToUpper = "TRUE" Then
                        tsmi_ReportLock.Checked = True
                        If System.Environment.UserName.ToString.ToLower = nd("ReportOwner").Trim.ToLower Then
                            tsmi_ReportDelete.Enabled = True
                            tsmi_ReportRename.Enabled = True
                            tsmi_ReportSlideAdd.Enabled = True
                        Else
                            tsmi_ReportSlideAdd.Enabled = False
                        End If
                    Else
                        tsmi_ReportLock.Checked = False
                        tsmi_ReportSlideAdd.Enabled = True
                        tsmi_ReportDelete.Enabled = True
                        tsmi_ReportRename.Enabled = True
                    End If

                    ' Enable report tsmi(s)
                    tsmi_ReportCopy.Enabled = True
                    tsmi_ReportRunConfigured.Enabled = True
                    tsmi_ReportRunCurrent.Enabled = True
                    tsmi_ReportObjects.Enabled = False

                    ' Disable slide tsmi(s)
                    tsmt_SlideRename.Enabled = False
                    tsmt_SlideDelete.Enabled = False
                    tsmi_SlideMoveUp.Enabled = False
                    tsmi_SlideMoveDown.Enabled = False
                    tsmi_SlideObjectAdd.Enabled = False

                    ' Disable object tsmi(s)
                    tsmi_ObjectRename.Enabled = False
                    tsmi_ObjectChartDelete.Enabled = False
                    tsmi_ObjectChartMoveUp.Enabled = False
                    tsmi_ObjectChartMoveDown.Enabled = False

                    If nd("ReportType").ToString.ToUpper = "POWERPOINT" Then
                        RenameContextMenuItems("Slide")
                        tsmi_ReportSlideAdd.DropDownItems.Clear()
                    ElseIf nd("ReportType").ToString.ToUpper = "EXCEL" Then
                        RenameContextMenuItems("Worksheet")
                        tsmi_ReportSlideAdd.DropDownItems.Clear()
                    ElseIf nd("ReportType").ToString.ToUpper = "DASHBOARDPDF" Then
                        RenameContextMenuItems("Dashboard")
                        tsmi_ReportCopy.Enabled = False

                        tsmi_ReportSlideAdd.DropDownItems.Clear()
                        tsmi_DB_Reports = New ToolStripMenuItem("Dashboard Reports")
                        tsmi_DB_Reports.ToolTipText = "Add Dashboard Reports"
                        AddHandler tsmi_DB_Reports.DropDownOpening, AddressOf tsmi_DB_Reports_DropDownOpening
                        tsmi_ReportSlideAdd.DropDownItems.Add(tsmi_DB_Reports)

                        tsmi_SON_Reports = New ToolStripMenuItem("SON Reports")
                        tsmi_SON_Reports.ToolTipText = "Add SON Reports"
                        AddHandler tsmi_SON_Reports.DropDownOpening, AddressOf tsmi_SON_Reports_DropDownOpening
                        tsmi_ReportSlideAdd.DropDownItems.Add(tsmi_SON_Reports)
                    End If

                ElseIf nd.Level = 1 Then

                    ' Disable report tsmi(s)
                    tsmi_ReportSlideAdd.Enabled = False
                    tsmi_ReportRunConfigured.Enabled = False
                    tsmi_ReportRunCurrent.Enabled = False
                    tsmi_ReportLock.Enabled = False
                    tsmi_ReportObjects.Enabled = False
                    tsmi_ReportCopy.Enabled = False

                    ' Enable slide tsmi(s)
                    tsmt_SlideRename.Enabled = False
                    tsmt_SlideDelete.Enabled = False
                    tsmi_SlideObjectAdd.Enabled = False
                    tsmi_SlideMoveUp.Enabled = False
                    tsmi_SlideMoveDown.Enabled = False

                    If nd.ParentNode("ReportLocked").ToString.ToUpper = "TRUE" Then
                        If System.Environment.UserName.ToString.ToLower = nd.ParentNode("ReportOwner").Trim.ToLower Then
                            tsmt_SlideRename.Enabled = True
                            tsmt_SlideDelete.Enabled = True
                            tsmi_SlideObjectAdd.Enabled = True
                            tsmi_SlideMoveUp.Enabled = True
                            tsmi_SlideMoveDown.Enabled = True
                        End If
                    Else
                        tsmt_SlideRename.Enabled = True
                        tsmt_SlideDelete.Enabled = True
                        tsmi_SlideObjectAdd.Enabled = True
                        tsmi_SlideMoveUp.Enabled = True
                        tsmi_SlideMoveDown.Enabled = True
                    End If
                    tsmitxt_NewTextbox.Text = ""

                    ' Disable object tsmi(s)
                    tsmi_ObjectRename.Enabled = False
                    tsmi_ObjectChartDelete.Enabled = False
                    tsmi_ObjectChartMoveUp.Enabled = False
                    tsmi_ObjectChartMoveDown.Enabled = False

                    If nd.ParentNode("ReportType").ToString.ToUpper = "POWERPOINT" Then
                        RenameContextMenuItems("Slide")
                    ElseIf nd.ParentNode("ReportType").ToString.ToUpper = "EXCEL" Then
                        RenameContextMenuItems("Worksheet")
                    ElseIf nd.ParentNode("ReportType").ToString.ToUpper = "DASHBOARDPDF" Then
                        RenameContextMenuItems("Dashboard")
                        tsmt_SlideRename.Text = "Slide - Rename"
                        tsmt_SlideDelete.Text = "Slide - Delete"
                        tsmi_SlideMoveUp.Enabled = False
                        tsmi_SlideMoveDown.Enabled = False
                        tsmi_SlideObjectAdd.Enabled = False
                    End If

                ElseIf nd.Level = 2 Then

                    ' Disable report tsmi(s)
                    tsmi_ReportSlideAdd.Enabled = False
                    tsmi_ReportRunConfigured.Enabled = False
                    tsmi_ReportRunCurrent.Enabled = False
                    tsmi_ReportLock.Enabled = False
                    tsmi_ReportObjects.Enabled = False
                    tsmi_ReportCopy.Enabled = False

                    ' Disable slide tsmi(s)
                    tsmt_SlideRename.Enabled = False
                    tsmt_SlideDelete.Enabled = False
                    tsmi_SlideMoveUp.Enabled = False
                    tsmi_SlideMoveDown.Enabled = False
                    tsmi_SlideObjectAdd.Enabled = False

                    ' Enable object tsmi(s)
                    tsmi_ObjectRename.Enabled = False
                    tsmi_ObjectChartDelete.Enabled = False
                    tsmi_ObjectChartMoveUp.Enabled = False
                    tsmi_ObjectChartMoveDown.Enabled = False

                    If nd.ParentNode.ParentNode("ReportLocked").ToString.ToUpper = "TRUE" Then
                        If System.Environment.UserName.ToString.ToLower = nd.ParentNode.ParentNode("ReportOwner").Trim.ToLower Then
                            tsmi_ObjectRename.Enabled = True
                            tsmi_ObjectChartDelete.Enabled = True
                            tsmi_ObjectChartMoveUp.Enabled = True
                            tsmi_ObjectChartMoveDown.Enabled = True
                        End If
                    Else
                        tsmi_ObjectRename.Enabled = True
                        tsmi_ObjectChartDelete.Enabled = True
                        tsmi_ObjectChartMoveUp.Enabled = True
                        tsmi_ObjectChartMoveDown.Enabled = True
                    End If
                    If (nd("Object Type").ToString.ToLower = "chart") Then
                        tsmi_ObjectRename.Visible = False
                    Else
                        tsmi_ObjectRename.Visible = True
                    End If

                    If nd.ParentNode.ParentNode("ReportType").ToString.ToUpper = "POWERPOINT" Then
                        RenameContextMenuItems("Slide")
                    ElseIf nd.ParentNode.ParentNode("ReportType").ToString.ToUpper = "EXCEL" Then
                        RenameContextMenuItems("Worksheet")
                    ElseIf nd.ParentNode.ParentNode("ReportType").ToString.ToUpper = "DASHBOARDPDF" Then
                        RenameContextMenuItems("Dashboard")
                    End If

                    If nd.ParentNode.Nodes.Count = 1 Then
                        tsmi_ObjectChartMoveUp.Enabled = False
                        tsmi_ObjectChartMoveDown.Enabled = False
                    End If
                End If
            End If
            tsmi_txtReportSlideAdd.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_DB_Reports_DropDownOpening(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtDashRpts As DataTable = dtDashboardReports.AsEnumerable().Where(Function(x) x.Field(Of String)("DashboardModule") = "Dashboard").OrderBy(Function(x) x.Field(Of String)("DashboardName")).CopyToDataTable
            tsmi_DB_Reports.DropDownItems.Clear()

            Dim ownerDropDown = CType(tsmi_DB_Reports.Owner, ToolStripDropDown)
            Dim itemBounds As Rectangle = tsmi_DB_Reports.Bounds
            Dim screenPoint As Point = ownerDropDown.PointToScreen(New Point(itemBounds.Right, itemBounds.Top))
            Dim items As New List(Of ToolStripMenuItem)

            If Not dtDashRpts Is Nothing Then
                For Each drow As DataRow In dtDashRpts.Rows
                    Dim tsmi_DB As ToolStripMenuItem = New ToolStripMenuItem(drow("DashboardName").ToString.Trim)
                    tsmi_DB.Tag = drow("DashboardID").ToString.Trim
                    'tsmi_DB.ToolTipText = drow("DashboardName").ToString.Trim
                    AddHandler tsmi_DB.Click, AddressOf tsmi_Dashboard_Click
                    items.Add(tsmi_DB)
                Next
            End If

            Dim dd As New ToolStripDropDownMenu()

            For Each it In items
                dd.Items.Add(it)
            Next

            Dim itemHeight As Integer = 24
            dd.MaximumSize = New Size(0, itemHeight * 10)

            tsmi_DB_Reports.DropDown = dd

            tsmi_DB_Reports.DropDownDirection = ToolStripDropDownDirection.Right
            tsmi_DB_Reports.DropDown.Location = screenPoint
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_SON_Reports_DropDownOpening(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim dtSonRpts As DataTable = dtDashboardReports.AsEnumerable().Where(Function(x) x.Field(Of String)("DashboardModule") = "SON").OrderBy(Function(x) x.Field(Of String)("DashboardName")).CopyToDataTable
            tsmi_SON_Reports.DropDownItems.Clear()

            Dim ownerDropDown = CType(tsmi_SON_Reports.Owner, ToolStripDropDown)
            Dim itemBounds As Rectangle = tsmi_SON_Reports.Bounds
            Dim screenPoint As Point = ownerDropDown.PointToScreen(New Point(itemBounds.Right, itemBounds.Top))
            Dim items As New List(Of ToolStripMenuItem)

            If Not dtSonRpts Is Nothing Then
                For Each drow As DataRow In dtSonRpts.Rows
                    Dim tsmi_DB As ToolStripMenuItem = New ToolStripMenuItem(drow("DashboardName").ToString.Trim)
                    tsmi_DB.Tag = drow("DashboardID").ToString.Trim
                    'tsmi_DB.ToolTipText = drow("DashboardName").ToString.Trim
                    AddHandler tsmi_DB.Click, AddressOf tsmi_Dashboard_Click
                    items.Add(tsmi_DB)
                Next
            End If

            Dim dd As New ToolStripDropDownMenu()

            For Each it In items
                dd.Items.Add(it)
            Next

            Dim itemHeight As Integer = 24
            dd.MaximumSize = New Size(0, itemHeight * 10)

            tsmi_SON_Reports.DropDown = dd

            tsmi_SON_Reports.DropDownDirection = ToolStripDropDownDirection.Right
            tsmi_SON_Reports.DropDown.Location = screenPoint
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_Dashboard_Click(sender As Object, e As EventArgs)
        Try
            tlvReports.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim fnd As TreeListNode = tlvReports.FocusedNode
            Dim tsmi As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim reportID As Integer = tlvReports.FocusedNode.Tag
            Dim dashboardID As Integer = tsmi.Tag
            Dim dashboardXmlFile As String = Nothing
            Dim dbTabPages As String = "ALL"

            Dim dtDashboard As DataTable = clsSQLCommands.GetDashboardFileFromID(connStrIOSServer, dashboardID)

            Dim str = dtDashboard.Rows(0)("DashboardFile").ToString
            If str.Trim.Contains("<?xml") Then
                dashboardXmlFile = str
            Else
                dashboardXmlFile = GetDecryptedConnectionString(str)
            End If

            Dim ms As New System.IO.MemoryStream()
            ms = StringToStream(dashboardXmlFile)

            dshbrd = New Dashboard()
            AddHandler dshbrd.ConfigureDataConnection, AddressOf dashboard_ConfigureDataConnection
            dshbrd.LoadFromXml(ms)

            Dim tabContainers = dshbrd.Items.OfType(Of TabContainerDashboardItem)().ToList()
            If tabContainers.Count <> 0 Then
                dbTabPages = ""
                dbTabPages = String.Join(",", tabContainers.
                                         Where(Function(x) x.TabPages IsNot Nothing).
                                         SelectMany(Function(y) y.TabPages).Where(Function(y) Not String.IsNullOrWhiteSpace(y.Name)).
                                         Select(Function(z) z.Name))
            End If

            Dim slideName As String = "Dashboard - " & tsmi.Text
            clsSQLCommands.InsertDashboardSlide(connStrIOSServer, reportID, dashboardID, slideName, dbTabPages)

            btnRefresh_Click(Nothing, Nothing)
            tlvReports.SetFocusedNode(fnd)
            ExpandTree(tsmi.Text, reportID)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tlvReports.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub RenameContextMenuItems(ByVal menuPrefixText As String)
        tsmi_ReportSlideAdd.Text = menuPrefixText & " - Add"
        tsmi_ReportRename.Text = menuPrefixText & " - Rename"
        tsmi_ReportDelete.Text = menuPrefixText & " - Delete"
        tsmt_SlideRename.Text = menuPrefixText & " - Rename"
        tsmt_SlideDelete.Text = menuPrefixText & " - Delete"
        tsmi_SlideMoveUp.Text = menuPrefixText & " - Move Up"
        tsmi_SlideMoveDown.Text = menuPrefixText & " - Move Down"
    End Sub

    Private Sub tsmi_ReportRename_Click(sender As Object, e As EventArgs) Handles tsmi_ReportRename.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode
            Dim NewName As String = InputBox("Enter New Name: ", "", nd.GetDisplayText("Report Name"))
            If Not nd Is Nothing And NewName <> "" Then
                tlvReports.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim parray()() As String = {
                    New String() {"@ReportID", nd.Tag},
                    New String() {"@NewName", Chr(39) & NewName & Chr(39)}
                }
                Dim sql As String = GetSQL(8509, parray)(1)
                Dim connstring As String = GetSQL(8509, parray)(0)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                btnRefresh_Click(Nothing, Nothing)
                ExpandTree(NewName, Nothing)
                SetMessag("Report Successfully renamed.")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tlvReports.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ReportDelete_Click(sender As Object, e As EventArgs) Handles tsmi_ReportDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            tlvReports.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode
            If Not nd Is Nothing Then
                'Dim sqlCommand As String = "EXEC IOS_Reports_Delete " & nd.Key
                'DataAccessorODBC.ExecuteNonQuery(connStrIOSServer, sqlCommand)
                clsSQLCommands.DeleteReport(connStrIOSServer, nd.Tag)
                btnRefresh_Click(Nothing, Nothing)
                SetMessag("Report successfully Deleted.")
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tlvReports.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ReportRunConfigured_Click(sender As Object, e As EventArgs) Handles tsmi_ReportRunConfigured.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

        Me.Cursor = Cursors.WaitCursor
        WaitScreenReportEditor.ShowWaitScreen("Generating Report...")
        Application.DoEvents()

        Dim nd As TreeListNode = tlvReports.FocusedNode
        Me.rptCurrOrConfig = "Config"

        If nd.Level > 0 Then
            Exit Sub
        End If

        Clipboard.GetImage()
        Dim ReportingTemplateFileName As String = GetConfigClientKeyValue("ReportingTemplateFileName")
        Dim pptfile As String = Application.StartupPath & "\" & ReportingTemplateFileName

        Dim ExcelreportFilePath As String = Nothing
        Dim PptreportFilePath As String = Nothing
        Dim dashbaordPDfFilePath As String = Nothing

        If Me.reportType.ToLower = "excel" Then
            CreateConfiguredReportInExcel(nd.Tag, nd.GetDisplayText("Report Name"), ExcelreportFilePath)
        ElseIf Me.reportType.ToLower = "powerpoint" Then
            If Me.reportMethod = ReportMethodType.Interop Then
                CreateReportWithInteropMethod(pptfile, nd.GetDisplayText("Report Name"), nd.Tag)
            ElseIf Me.reportMethod = ReportMethodType.OpenXml Then
                CreateReportWithOpenXMLMethod(pptfile, ReportingTemplateFileName, nd.GetDisplayText("Report Name"), nd.Tag, PptreportFilePath)
            End If
        ElseIf Me.reportType.ToLower = "dashboardpdf" Then
            CreateReport_DashboardPDF(nd.Tag, nd.GetDisplayText("Report Name"), dashbaordPDfFilePath)
        End If

        Me.Cursor = Cursors.Default
        WaitScreenReportEditor.CloseWaitScreen()
        Application.DoEvents()

        If reportCreated = True Then
            XtraMessageBox.Show("Report generated successfully. Attempting to open folder and report...", "Report Editor", MessageBoxButtons.OK)
        End If

        Try
            If reportType.ToLower = "powerpoint" Then
                Process.Start("explorer.exe", "/select," & PptreportFilePath)
            ElseIf reportType.ToLower = "excel" Then
                Process.Start("explorer.exe", "/select," & ExcelreportFilePath)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Failed to open folder location, check folder: " & Application.StartupPath, "Report Editor", MessageBoxButtons.OK)
        End Try

        Try
            If reportType.ToLower = "powerpoint" Then
                Process.Start(PptreportFilePath)
            ElseIf reportType.ToLower = "excel" Then
                Process.Start(ExcelreportFilePath)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Failed to open report, check folder: " & Application.StartupPath, "Report Editor", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub tsmi_ReportRunCurrent_Click(sender As Object, e As EventArgs) Handles tsmi_ReportRunCurrent.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")

        Dim ReportingTemplateFileName As String = GetConfigClientKeyValue("ReportingTemplateFileName")
        Dim pptfile As String = Application.StartupPath & "\" & ReportingTemplateFileName
        Dim ExcelreportFilePath As String = Nothing
        Dim PptreportFilePath As String = Nothing
        Dim dashbaordPDfFilePath As String = Nothing

        Me.Cursor = Cursors.WaitCursor
        WaitScreenReportEditor.ShowWaitScreen("Generating Report...", 0)
        Application.DoEvents()

        Try
            Dim nd As TreeListNode = tlvReports.FocusedNode
            Me.rptCurrOrConfig = "Current"

            If nd.Level > 0 Then
                Exit Sub
            End If

            If Me.reportType.ToLower = "excel" Then
                CreateCurrentReportInExcel(nd.Tag, nd.GetDisplayText("Report Name"), ExcelreportFilePath)
            ElseIf Me.reportType.ToLower = "powerpoint" Then
                frmMDI.WindowState = FormWindowState.Minimized
                If Me.reportMethod = ReportMethodType.Interop Then
                    CreateReportWithInteropMethod(pptfile, nd.GetDisplayText("Report Name"), nd.Tag)
                ElseIf Me.reportMethod = ReportMethodType.OpenXml Then
                    CreateReportWithOpenXMLMethod(pptfile, ReportingTemplateFileName, nd.GetDisplayText("Report Name"), nd.Tag, PptreportFilePath)
                End If
                frmMDI.WindowState = FormWindowState.Maximized
            ElseIf Me.reportType.ToLower = "dashboardpdf" Then
                CreateReport_DashboardPDF(nd.Tag, nd.GetDisplayText("Report Name"), dashbaordPDfFilePath)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "ERROR", ex.Message.ToString & " " & ex.StackTrace.ToString)
        End Try

        Me.Cursor = Cursors.Default
        WaitScreenReportEditor.CloseWaitScreen()
        Application.DoEvents()

        If reportCreated = True Then
            XtraMessageBox.Show("Report generated successfully. Attempting to open folder and report...", "Report Editor", MessageBoxButtons.OK)
        End If

        Try
            If reportType.ToLower = "powerpoint" Then
                Process.Start("explorer.exe", "/select," & PptreportFilePath)
            ElseIf reportType.ToLower = "excel" Then
                Process.Start("explorer.exe", "/select," & ExcelreportFilePath)
            ElseIf reportType.ToLower = "dashboardpdf" Then
                Process.Start("explorer.exe", "/select," & dashbaordPDfFilePath & ".pdf")
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Failed to open folder location, check folder: " & Application.StartupPath, "Report Editor", MessageBoxButtons.OK)
        End Try

        Try
            If reportType.ToLower = "powerpoint" Then
                Process.Start(PptreportFilePath)
            ElseIf reportType.ToLower = "excel" Then
                Process.Start(ExcelreportFilePath)
            ElseIf reportType.ToLower = "dashboardpdf" Then
                Process.Start(dashbaordPDfFilePath & ".pdf")
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Failed to open report, check folder: " & Application.StartupPath, "Report Editor", MessageBoxButtons.OK)
        End Try
    End Sub

#Region "Create Report - Excel"

    Private Sub CreateConfiguredReportInExcel(reportID As Integer, reportName As String, ByRef reportFilePath As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            chartDataFirstColIndex = 0

            Using wb As New Workbook()
                Dim dxCh As DXChart = Nothing
                Dim dtData As DataTable = Nothing

                Dim wsSlide As Worksheet = Nothing
                Dim wsData As Worksheet = Nothing

                Dim chartTopCellRowIndex As Integer = 0
                Dim chartBottomCellColIndex As Integer = 29

                wb.Unit = DevExpress.Office.DocumentUnit.Point
                wb.BeginUpdate()

                Try
                    ' get report data for the selcted report
                    Dim dtReport As DataTable = dtReports.Select("ReportID=" & reportID).CopyToDataTable()
                    ' set excel file name
                    reportFilePath = GetUserDataPath() & "\Data\" & reportName & "_" & Format(Now(), "yyyyMMdd") & ".xlsx"

                    Dim workSheetDistinct As DataTable = dtReport.AsDataView.ToTable(True, "SlideID", "SlideTitle", "SlideName", "SlideOrdinal")
                    wsData = wb.Worksheets.Add("Charts Data")

                    If (workSheetDistinct.Rows.Count > 0) Then

                        Dim worksheetcount As Int16 = 0

                        For Each drSheet As DataRow In workSheetDistinct.Rows
                            worksheetcount = worksheetcount + 1
                            WaitScreenReportEditor.ShowWaitScreen("Generating Report..." & System.Math.Round(100 * worksheetcount / workSheetDistinct.Rows.Count, 0) & "%")

                            wsSlide = wb.Worksheets.Add(drSheet("SlideName").ToString)
                            wb.Worksheets.ActiveWorksheet = wsSlide

                            Dim dtObjectsPerSheet As DataTable = dtReport.Select("SlideID=" & Chr(39) & drSheet("SlideID").ToString & Chr(39), "ObjectOrdinal ASC").CopyToDataTable()

                            If (dtObjectsPerSheet.Rows.Count > 0) Then

                                chartTopCellRowIndex = 0
                                chartBottomCellColIndex = 29

                                For Each drObject As DataRow In dtObjectsPerSheet.Rows

                                    Try

                                        Dim dtReportObject As DataTable = clsSQLCommands.GetSlidesByReportID(connStrIOSServer, reportID).Select("SlideID=" & drObject("SlideID") & " And ObjectID=" & drObject("ObjectID")).CopyToDataTable

                                        perSlideObjectID = CInt(dtReportObject.Rows(0)("ObjectID"))
                                        _strNetwork = dtReportObject.Rows(0)("Technology").ToString

                                        GetObjectStyleProperties("Chart")
                                        GetPredefinedPeriod()

                                        If dtReportObject.Rows(0)("Purpose").ToString.ToLower = "charts" Then

                                            dxCh = wsSlide.Charts.Add(Charts.ChartType.ColumnClustered)

                                            dxCh.TopLeftCell = wsSlide.Cells(chartTopCellRowIndex, 0)
                                            dxCh.BottomRightCell = wsSlide.Cells(chartBottomCellColIndex, 25)

                                            SetChartConfigData(_strNetwork, dtReportObject.Rows(0)("ObjectName").ToString)
                                            Dim dtTemp As DataTable = GetStatsChartData(_strNetwork, dtReportObject.Rows(0)("ObjectName").ToString)

                                            Dim dtKPI As DataTable = ChartConfig.ChartFillingDataTable.DefaultView.ToTable(True, "ChartElements")
                                            Dim lstKPIs As New List(Of String)
                                            dtData = New DataTable

                                            lstKPIs.Add("Date")
                                            dtData.Columns.Add("Date", GetType(String))

                                            For Each dr As DataRow In dtKPI.Rows
                                                lstKPIs.Add(dr("ChartElements").ToString.Trim)
                                                dtData.Columns.Add(dr("ChartElements").ToString.Trim, GetType(Double))
                                            Next

                                            For Each drow As DataRow In dtTemp.Rows
                                                Dim dr As DataRow = dtData.NewRow()
                                                If (drObject("Resolution").ToString.ToUpper = "RAW") Or (drObject("Resolution").ToString.ToUpper = "HOURLY") Then
                                                    dr("Date") = CDate(drow("Date")).ToString("yyyy/MM/dd HH:mm")
                                                Else
                                                    dr("Date") = CDate(drow("Date")).ToString("yyyy/MM/dd")
                                                End If
                                                For Each strKPI In lstKPIs
                                                    If strKPI.ToLower <> "date" Then
                                                        If Not IsDBNull(drow(strKPI)) Then
                                                            dr(strKPI) = CDbl(drow(strKPI))
                                                        End If
                                                    End If
                                                Next
                                                dtData.Rows.Add(dr)
                                                dtData.AcceptChanges()
                                            Next

                                            richTxtStr = New RichTextString()
                                            richTxtStr.AddTextRun(drObject("ObjectNameGUI").ToString, New RichTextRunFont("Calibri", 12, Color.DarkBlue))
                                            wsData.Rows(0)(chartDataFirstColIndex).SetRichText(richTxtStr)

                                            wsData.Import(dtData, True, 1, chartDataFirstColIndex)
                                            AssignDataToStatsChartExcel(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, dtData)

                                            chartDataFirstColIndex = chartDataFirstColIndex + dtData.Columns.Count + 1

                                        ElseIf dtReportObject.Rows(0)("Purpose").ToString.ToLower = "topx" Then

                                            dxCh = wsSlide.Charts.Add(Charts.ChartType.ColumnClustered)

                                            dxCh.TopLeftCell = wsSlide.Cells(chartTopCellRowIndex, 0)
                                            dxCh.BottomRightCell = wsSlide.Cells(chartBottomCellColIndex, 25)

                                            If dtReportObject.Rows(0)("TopX_DeltaInterval").ToString <> "" Then
                                                Dim dsTopX As DataSet = GetTopXChartDataSet(drObject("TopX_ShowObjects"), dtReportObject.Rows(0)("ObjectName").ToString)
                                                AssignDataToTopXChartExcel_Delta(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, dsTopX)
                                            Else
                                                dtData = GetTopXChartData(drObject("TopX_ShowObjects"), dtReportObject.Rows(0)("ObjectName").ToString)
                                                AssignDataToTopXChartExcel(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, dtData)
                                            End If

                                        ElseIf dtReportObject.Rows(0)("Purpose").ToString.ToLower = "objecttime" Then

                                            dxCh = wsSlide.Charts.Add(Charts.ChartType.ColumnClustered)

                                            dxCh.TopLeftCell = wsSlide.Cells(chartTopCellRowIndex, 0)
                                            dxCh.BottomRightCell = wsSlide.Cells(chartBottomCellColIndex, 25)

                                            dtData = GetStatsObjectTimeData(drObject("TargetType").ToString, dtReportObject.Rows(0)("ObjectName").ToString, _strNetwork)

                                            richTxtStr = New RichTextString()
                                            richTxtStr.AddTextRun(drObject("ObjectNameGUI").ToString, New RichTextRunFont("Calibri", 12, Color.DarkBlue))
                                            wsData.Rows(0)(chartDataFirstColIndex).SetRichText(richTxtStr)

                                            wsData.Import(dtData, True, 1, chartDataFirstColIndex)
                                            AssignDataToObjectTimeChartExcel(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, dtData)

                                            chartDataFirstColIndex = chartDataFirstColIndex + dtData.Columns.Count + 1

                                        End If

                                        chartTopCellRowIndex = dxCh.BottomRightCell.RowIndex + 2
                                        chartBottomCellColIndex = dxCh.BottomRightCell.RowIndex + 2 + 29

                                    Catch ex As Exception

                                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                                    End Try

                                Next
                            End If

                        Next

                    End If

                Catch ex As Exception
                    reportCreated = False
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                Finally
                    wb.Worksheets.RemoveAt(0)
                    wb.Worksheets("Charts Data").MoveToEnd()
                    wb.Worksheets.ActiveWorksheet = wb.Worksheets(0)
                    wb.EndUpdate()
                End Try

                wb.Calculate()
                wb.SaveDocument(reportFilePath)

            End Using
        Catch ex As Exception
            reportCreated = False
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Function GetStatsChartData(ByVal tech As String, ByVal chartName As String) As DataTable
        Dim chartSql As String = Nothing
        Using conn_el As New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()
            Using comm_Element As New Odbc.OdbcCommand("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable,COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects,COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj
                                                        FROM IOS_Chart_Configuration INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID  
                                                        WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & tech & Chr(39) & ") AND ChartName = '" & chartName & "' AND (IOS_SQL_KPI.Object = " & Chr(39) & dtChartStyleProperties.Rows(0)("CounterType").ToString & Chr(39) & ")
                                                        AND (sourcetable Is Not null)", conn_el)

                Using dr As Odbc.OdbcDataReader = comm_Element.ExecuteReader
                    While dr.Read
                        chartSql = SQL_Construct(dtChartStyleProperties.Rows(0)("CounterType").ToString, nZ(dr.Item("sourcetable").ToString.Trim, ""), chartName, nZ(dr.Item("CrossTabObj"), ""))
                    End While
                End Using
            End Using
        End Using

        Return GetData(chartSql).Tables(0)
    End Function

    Private Sub AssignDataToStatsChartExcel(ByRef dxCh As DXChart, ByRef wsDt As Worksheet, ByVal chartName As String, ByRef dtData As DataTable)
        Dim dtChartConfig As New DataTable
        dsHistogramData = New DataSet
        Dim username As String = Chr(39) & Environment.UserName.ToString & Chr(39)

        '(((ChartSetName = " & Chr(39) & chartSetName & Chr(39) & "))  AND TechTab = " & Chr(39) & _strNetwork & Chr(39) & " AND CrossTabObj IS NULL
        'And ObjectTab='" & dtChartStyleProperties.Rows(0)("CounterType").ToString & "' AND 
        ChartConfig.ChartFillingDataTable.DefaultView.RowFilter = "ChartName ='" & chartName & "' AND TechTab = " & Chr(39) & _strNetwork & Chr(39) & ""
        ChartConfig.ChartFillingDataTable.DefaultView.Sort = "techtab ASC, objecttabindex ASC, categorytabindex ASC, chartindex ASC, chartelementid ASC"
        dtChartConfig = ChartConfig.ChartFillingDataTable.DefaultView.ToTable()
        ChartConfig.ChartFillingDataTable.DefaultView.RowFilter = ""

        Try
            'add data to excel chart
            dxCh.SelectData(wsDt.Range.FromLTRB(chartDataFirstColIndex, 1, chartDataFirstColIndex + dtData.Columns.Count - 1, dtData.Rows.Count + 1), Charts.ChartDataDirection.Column)
            dxCh.Legend.Position = Charts.LegendPosition.Bottom
            dxCh.Width = dtChartStyleProperties.Rows(0)("ObjectWidth")
            dxCh.Height = dtChartStyleProperties.Rows(0)("ObjectHeight")

            Dim sc As Charts.SeriesCollection = Nothing
            Dim objectscharted As String = ""
            Dim X1AxisLabel As String = "Date"
            Dim Y1axislabel As String = "", Y2axislabel As String = ""
            Dim Y1axisAbsorPerc As String = "", Y2axisAbsOrPerc As String = ""
            Dim Y1axisPrecision As Integer = 0, Y2axisPrecision As Integer = 0
            Dim yaxis1 As Charts.Axis = Nothing
            Dim yaxis2 As Charts.Axis = Nothing
            Dim color_R As Integer = 0, color_B As Integer = 0, color_G As Integer = 0

            Dim chartElements() As String = {"0"}
            Dim chartElementsYAxis() As String = {"0"}
            Dim chartEltype() As String = {"Bar"}
            Dim chartElColor() As Integer = {0}
            Dim chartYAxisScale() As String = {"0", "0"}

            dtChartConfig.DefaultView.RowFilter = "ObjectTab='" & dtChartStyleProperties.Rows(0)("CounterType").ToString & "'"
            Using dtObjectTab = dtChartConfig.DefaultView.ToTable(True, {"ObjectTab", "ObjectTabIndex"})
                For Each drObjectTab As DataRow In dtObjectTab.Select("", "ObjectTabIndex ASC")

                    dtChartConfig.DefaultView.RowFilter = "ObjectTabIndex=" & drObjectTab("ObjectTabIndex")
                    Using dtChartList = dtChartConfig.DefaultView.ToTable(True, {"TechTab", "CategoryTabIndex", "CategoryTab", "ChartIndex", "ChartName", "ChartTitle", "ChartType"})
                        For Each drChart As DataRow In dtChartList.Select("", "CategoryTabIndex, ChartIndex ASC")

                            If drChart("ChartType") = IOSChartType.AlignInterval Then
                                'Do Nothing
                            Else
                                X1AxisLabel = "Date"
                                Y1axislabel = ""
                                Y2axislabel = ""
                                Y1axisAbsorPerc = ""
                                Y2axisAbsOrPerc = ""
                                Y1axisPrecision = 0
                                Y2axisPrecision = 0
                                yaxis1 = Nothing
                                yaxis2 = Nothing
                                color_R = 0
                                color_B = 0
                                color_G = 0

                                ''DefaultChartSettings(ch, drChart("TechTab"))
                                ''ChartTypeSettings(ch, drChart, objectscharted)
                                ''ch.TitleBox.Label.Text = "Objects: " & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString

                                dxCh.Title.Visible = True

                                If Me.rptCurrOrConfig = "Config" Then
                                    dxCh.Title.SetValue(drChart("ChartTitle").Trim & vbCrLf & "Objects: " & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString)
                                Else
                                    dxCh.Title.SetValue(drChart("ChartTitle").Trim & vbCrLf & "Objects: " & ObjectSelectedFromTreeForCurrent)
                                End If

                                Dim colList As String() = {
                                    "ChartElementID", "ChartElements", "chartElementsType", "chartElementsYAxis", "chartYaxisScaleProp", "chartY1axisLabels",
                                    "chartY2axisLabels", "chartY1AbsPerc", "chartY2AbsPerc", "chartY1axisPrecision", "chartY2axisPrecision",
                                    "ChartElementsColor", "SQLKPI_ID", "Sort_dir", "ElmntDisplay", "ChartSetName", "CrossTabObj", "ElementAxis", "ChartType"
                                }

                                'configures individual chart when new chartline is detected
                                dtChartConfig.DefaultView.RowFilter = "ChartName='" & drChart("ChartName") & "'"
                                Using dtKpi = dtChartConfig.DefaultView.ToTable(True, colList)

                                    Dim j As Integer = 0
                                    For Each drKpi As DataRow In dtKpi.Rows

                                        'Dim dtSeries As DataTable = dtKpi.Select("ChartElements='" & ChartSeriesIndex(j).SeriesName.PlainText & "'").CopyToDataTable

                                        Y1axisAbsorPerc = nZ(drKpi("chartY1AbsPerc"), "Abs")
                                        Y2axisAbsOrPerc = nZ(drKpi("chartY2AbsPerc"), "Abs")

                                        Y1axisPrecision = CInt(nZ(drKpi("chartY1axisPrecision"), 0))
                                        Y2axisPrecision = CInt(nZ(drKpi("chartY2axisPrecision"), 0))

                                        If nZ(drKpi("chartY1axisLabels"), "").Length > 0 Then
                                            Y1axislabel = drKpi("chartY1axisLabels").ToString.Trim
                                        End If
                                        If nZ(drKpi("chartY2axisLabels"), "").Length > 0 Then
                                            Y2axislabel = drKpi("chartY2axisLabels").ToString.Trim
                                        End If

                                        If dxCh.ChartType = Charts.ChartType.ScatterLine Then
                                            If dtKpi.Select("ChartType=" & IOSChartType.Scatter)(0)("ElementAxis") = "Y" Then
                                                Y1axislabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(0)("ChartElements").ToString
                                            Else
                                                X1AxisLabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(0)("ChartElements").ToString
                                            End If
                                            If dtKpi.Select("ChartType=" & IOSChartType.Scatter)(1)("ElementAxis") = "X" Then
                                                X1AxisLabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(1)("ChartElements").ToString
                                            Else
                                                Y1axislabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(1)("ChartElements").ToString
                                            End If
                                            ''ch.DefaultElement.Hotspot.ToolTip = X1AxisLabel & ": %XValue" & Chr(13) & Y1axislabel & ": %Value "
                                            ''ch.XAxis.Label.Text = X1AxisLabel
                                        End If

                                        'Y-Axis Settings
                                        'If drKpi("ChartElements").ToString.ToLower = dxCh.Series(j).SeriesName.PlainText.ToLower Then '

                                        'seriesOrdinal = dtData.Columns(drKpi("ChartElements")).Ordinal
                                        'dxCh.Series(seriesOrdinal - 1).AxisGroup = Charts.AxisGroup.Primary

                                        'If drKpi("chartElementsYAxis") = "Left" Then

                                        '    yaxis1 = dxCh.PrimaryAxes(1)
                                        '    yaxis1.Title.Visible = True
                                        '    yaxis1.Title.SetValue(Y1axislabel)

                                        'Else

                                        'dxCh.Series(j).AxisGroup = Charts.AxisGroup.Secondary
                                        'yaxis2 = dxCh.SecondaryAxes(1)
                                        '    yaxis2.Title.Visible = True
                                        '    yaxis2.Title.SetValue(Y2axislabel)

                                        'End If

                                        'If drKpi("chartElementsType").ToString.ToLower.Trim = "bar" Then
                                        '    dxCh.Series(j).ChangeType(Charts.ChartType.ColumnClustered)
                                        'Else
                                        '    dxCh.Series(j).ChangeType(Charts.ChartType.Line)
                                        'End If

                                        'Else

                                        'seriesOrdinal = dtData.Columns(drKpi("ChartElements")).Ordinal
                                        'dxCh.Series(seriesOrdinal - 1).AxisGroup = Charts.AxisGroup.Secondary

                                        'If drKpi("chartElementsYAxis") = "Left" Then
                                        '    yaxis1 = dxCh.PrimaryAxes(1)
                                        '    yaxis1.Title.Visible = True
                                        '    yaxis1.Title.SetValue(Y1axislabel)
                                        'Else
                                        '    yaxis2 = dxCh.SecondaryAxes(1)
                                        '    yaxis2.Title.Visible = True
                                        '    yaxis2.Title.SetValue(Y2axislabel)
                                        'End If

                                        'If drKpi("chartElementsType").ToString.ToLower.Trim = "bar" Then
                                        'dxCh.Series(seriesOrdinal - 1).ChangeType(Charts.ChartType.ColumnClustered)
                                        'Else
                                        'dxCh.Series(seriesOrdinal - 1).ChangeType(Charts.ChartType.Line)
                                        'End If

                                        'End If

                                        'If yaxis1 Is Nothing Then
                                        '    yaxis1 = New Axis()
                                        '    yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                                        'End If
                                        'yaxis1.Label.Text = Y1axislabel

                                        'yaxis1.NumberPrecision = Y1axisPrecision

                                        'If UCase(Y1axisAbsorPerc) = "PERC" Then
                                        '    yaxis1.Percent = True
                                        'End If

                                        'If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                                        '    yaxis1.MinimumInterval = 1
                                        'End If

                                        'If yaxis2 Is Nothing Then
                                        '    yaxis2 = New Axis()
                                        '    yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right
                                        'End If
                                        'yaxis2.Label.Text = Y2axislabel

                                        'yaxis2.NumberPrecision = Y2axisPrecision

                                        'If UCase(Y2axisAbsOrPerc) = "PERC" Then
                                        '    yaxis2.Percent = True
                                        'End If

                                        'If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True And drKpi("chartElementsYAxis").trim = "Right" Then
                                        '    yaxis2.MinimumInterval = 1
                                        'End If

                                        ReDim Preserve chartElements(j)
                                        ReDim Preserve chartElementsYAxis(j)
                                        ReDim Preserve chartEltype(j)
                                        ReDim Preserve chartElColor(j)

                                        chartElements(j) = drKpi("ChartElements").ToString.Trim
                                        chartElementsYAxis(j) = drKpi("chartElementsYAxis").trim
                                        chartEltype(j) = drKpi("chartElementsType").trim
                                        chartElColor(j) = CInt(drKpi("ChartElementsColor"))

                                        If UCase(chartElementsYAxis(j)) = "LEFT" Then
                                            chartYAxisScale(0) = drKpi("chartYaxisScaleProp").trim
                                        ElseIf UCase(chartElementsYAxis(j)) = "RIGHT" Then
                                            chartYAxisScale(1) = drKpi("chartYaxisScaleProp").trim
                                        End If
                                        j = j + 1
                                    Next
                                End Using

                                yaxis1 = dxCh.PrimaryAxes(1)
                                yaxis1.Title.Visible = True
                                yaxis1.Title.SetValue(Y1axislabel)

                                sc = dxCh.Series
                                For i = 0 To chartElements.Count - 1
                                    For j = 0 To sc.Count - 1
                                        If chartElements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                            Select Case UCase(chartElementsYAxis(i).Trim)
                                                Case "LEFT"
                                                    yaxis1 = dxCh.PrimaryAxes(1)
                                                    yaxis1.Title.Visible = True
                                                    yaxis1.Title.SetValue(Y1axislabel)
                                                Case "RIGHT"
                                                    sc(j).AxisGroup = Charts.AxisGroup.Secondary
                                                    yaxis2 = dxCh.SecondaryAxes(1)
                                                    yaxis2.Scaling.AutoMin = True
                                                    yaxis2.Scaling.AutoMax = True
                                                    yaxis2.Title.Visible = True
                                                    yaxis2.Title.SetValue(Y2axislabel)
                                            End Select
                                        End If
                                    Next
                                Next

                                sc = dxCh.Series
                                For i = 0 To chartElements.Count - 1
                                    For j = 0 To sc.Count - 1
                                        If chartElements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                            color_R = CLng(chartElColor(i)) Mod 256
                                            color_G = (CLng(chartElColor(i)) \ 256) Mod 256
                                            color_B = ((CLng(chartElColor(i)) \ 256) \ 256) Mod 256

                                            sc(j).Fill.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))

                                            Select Case UCase(chartEltype(i).Trim)
                                                Case "LINE"
                                                    sc(j).ChangeType(Charts.ChartType.Line)
                                                    sc(j).Outline.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))
                                                Case "BAR"
                                                    If UCase(chartYAxisScale(0)) = "NORMAL" Then
                                                        sc(j).ChangeType(Charts.ChartType.ColumnClustered)
                                                    ElseIf UCase(chartYAxisScale(0)) = "STACKED" Then
                                                        sc(j).ChangeType(Charts.ChartType.ColumnStacked)
                                                    ElseIf UCase(chartYAxisScale(0)) = "FULLSTACKED" Then
                                                        sc(j).ChangeType(Charts.ChartType.ColumnFullStacked)
                                                    End If
                                                    sc(j).GapWidth = 25
                                                Case "AREALINE"
                                                    sc(j).ChangeType(Charts.ChartType.Area)
                                            End Select
                                        End If
                                    Next
                                Next

                                dxCh.PrimaryAxes(0).Position = Charts.AxisPosition.Bottom
                                dxCh.PrimaryAxes(0).TextDirection = Charts.ShapeTextDirection.Horizontal
                                dxCh.PrimaryAxes(0).TextRotation = -45

                                'If UCase(chartYAxisScale(0)) = "STACKED" Then
                                '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                                'ElseIf UCase(chartYAxisScale(0)) = "FULLSTACKED" Then
                                '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                                'Else
                                '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                                'End If

                                'If UCase(chartYAxisScale(1)) = "STACKED" Then
                                '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                                'ElseIf UCase(chartYAxisScale(1)) = "FULLSTACKED" Then
                                '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                                'Else
                                '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                                'End If

                                'ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal

                                If dxCh.ChartType = ChartType.Combo Then
                                    Dim xaxis_valuehigh As DateTime = CDate(GetFromTech_DateTimePicker(2)) 'Add 1 day to insert a extra x-axis scale.

                                    Select Case True
                                        Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw", dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly"
                                            xaxis_valuehigh = xaxis_valuehigh.AddHours(1)
                                        Case Else
                                            xaxis_valuehigh = xaxis_valuehigh.AddDays(1)
                                    End Select

                                    If xaxis_valuehigh.Date = Now.Date Then
                                        Select Case True
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "daily"
                                                xaxis_valuehigh = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date))
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH"
                                                xaxis_valuehigh = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date))
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly"
                                                xaxis_valuehigh = DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date)
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH"
                                                xaxis_valuehigh = DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date)
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly"
                                                xaxis_valuehigh = DateAdd(DateInterval.Month, -1, xaxis_valuehigh.Date)
                                        End Select
                                    End If

                                    'ch.XAxis.ScaleRange.ValueHigh = xaxis_valuehigh
                                ElseIf dxCh.ChartType = Charts.ChartType.ScatterLine Then
                                    Try
                                        'dxCh.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
                                        'Dim minValue As Double = dtData.Compute("Min(" & X1AxisLabel & ")", "")
                                        'Dim maxValue As Double = dtData.Compute("Max(" & X1AxisLabel & ")", "")
                                        'dxCh.XAxis.ScaleRange.ValueLow = IIf(minValue < 0, System.Math.Floor(minValue), System.Math.Ceiling(minValue))
                                        'dxCh.XAxis.ScaleRange.ValueHigh = IIf(maxValue < 0, System.Math.Floor(maxValue), System.Math.Ceiling(maxValue))
                                    Catch
                                    End Try
                                End If

                                'Dim de As DataEngine = New DataEngine(dtData)
                                'If ch.Type = ChartType.Scatter Then
                                '    de.DataFields = "XValue=" & X1AxisLabel & ",YValue=" & Y1axislabel
                                'Else
                                '    de.DataFields = String2DataFields(chartElements, X1AxisLabel)
                                'End If
                                'de.DataGridFormatString = "N2"

                                'If ch.Type = ChartType.Combo Then
                                '    If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Or dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                                '        de.FormatString = "dd/MM/yy HH:mm"
                                '    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                                '        de.FormatString = "dd/MM/yy"
                                '    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                                '        de.FormatString = "MMMM"
                                '    End If
                                'ElseIf ch.Type = ChartType.Scatter Then
                                '    de.FormatString = ""
                                'End If

                                'Dim sc As New SeriesCollection
                                'sc = de.GetSeries()
                                Dim boundaries As String = Nothing
                                Dim dtHistogramData As DataTable = Nothing

                                For i = 0 To dxCh.Series.Count - 1

                                    'Select Case UCase(chartElementsYAxis(i).Trim)
                                    '    Case "LEFT"
                                    '        sc(i).YAxis = yaxis1
                                    '    Case "RIGHT"
                                    '        sc(i).YAxis = yaxis2
                                    'End Select

                                    'color_R = CLng(chartElColor(i)) Mod 256
                                    'color_G = (CLng(chartElColor(i)) \ 256) Mod 256
                                    'color_B = ((CLng(chartElColor(i)) \ 256) \ 256) Mod 256

                                    'dxCh.Series(i).Fill.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))
                                    'sc(i).DefaultElement.Marker.Type = i + 1

                                    'Y-Axis boundaries for all series
                                    Try
                                        If drChart("ChartType") = IOSChartType.Histogram Then
                                            If boundaries IsNot Nothing Then
                                                'boundaries = boundaries & "," & sc(i).GetYValueList()
                                            Else
                                                'boundaries = sc(i).GetYValueList()
                                            End If
                                            If dtHistogramData Is Nothing Then
                                                dtHistogramData = New DataTable()
                                                dtHistogramData.TableName = dxCh.Name
                                                dtHistogramData.Columns.Add(New DataColumn("Bins", GetType(Double)))
                                            End If
                                            'dtHistogramData.Columns.Add(New DataColumn("Freq_" & sc(i).Name, GetType(Integer)))
                                        End If
                                    Catch
                                    End Try
                                Next

                                'Configure histogram chart area
                                Try
                                    If drChart("ChartType") = IOSChartType.Histogram Then
                                        Dim boundariesArr As Double() = StringToDoubleArray(boundaries, New String() {","})
                                        Dim minValue As Double = boundariesArr.Min()
                                        Dim maxValue As Double = boundariesArr.Max()
                                        Dim binsGap As Double = System.Math.Ceiling((maxValue - minValue) / 30)

                                        Dim bins(29) As Double
                                        For index As Integer = 0 To bins.Length - 1
                                            bins(index) = minValue + (binsGap * (index + 1))
                                            Dim drHistogram As DataRow
                                            drHistogram = dtHistogramData.NewRow
                                            drHistogram.Item("Bins") = Convert.ToDouble(bins(index))
                                            dtHistogramData.Rows.Add(drHistogram)
                                        Next
                                        'ch.ExtraChartAreas.Item(0).XAxis.Minimum = minValue
                                        'ch.ExtraChartAreas.Item(0).XAxis.Interval = binsGap

                                        dsHistogramData.Tables.Add(dtHistogramData)

                                        'For i = 0 To sc.Count() - 1
                                        '    Dim freqTable As Series = StatisticalEngine.FrequencyTableOL(sc(i), bins)
                                        '    freqTable.Name = "Freq_" & sc(i).Name
                                        '    freqTable.Type = SeriesType.Bar
                                        '    'ch.ExtraChartAreas.Item(0).SeriesCollection.Add(freqTable)
                                        'Next
                                    End If
                                Catch
                                End Try

                                'ch.SeriesCollection.Clear()
                                'ch.SeriesCollection.Add(sc)

                                'sc = Nothing
                                'de = Nothing
                                'ch.XAxis.Markers.Clear()

                                'ch.RefreshChart()
                                'ch.ResumeLayout()
                                ReDim chartElements(0)
                                ReDim chartElementsYAxis(0)
                                ReDim chartEltype(0)
                                ReDim chartElColor(0)
                                ReDim chartYAxisScale(1)

                            End If
                        Next
                    End Using
                Next
            End Using

        Catch ex As Exception
            'Console.WriteLine(ex.Message.ToString)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)
        End Try
        dtChartConfig.Dispose()
        dtChartConfig = Nothing
    End Sub

    Private Function GetTopXChartData(ByVal targetType As String, ByVal chartName As String) As DataTable
        Dim chartSql As String = Nothing
        Try
            chartSql = SQL_Construct_TopX(targetType, chartName)
        Catch
        End Try
        Return GetData(chartSql).Tables(0)
    End Function

    Private Function GetTopXChartDataSet(ByVal targetType As String, ByVal chartName As String) As DataSet
        Dim chartSql As String = Nothing
        Try
            chartSql = SQL_Construct_TopX(targetType, chartName)
        Catch
        End Try
        Return GetData(chartSql)
    End Function

    Private Sub AssignDataToTopXChartExcel(ByRef dxCh As DXChart, ByRef wsDt As Worksheet, chartName As String, dtData As DataTable, Optional chartname_original As String = Nothing, Optional ChartElementSortOrder() As String = Nothing)
        Dim connstringconfig As String = Nothing
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""
        Dim customTabIndexTopX As Integer = 0
        Dim sc As Charts.SeriesCollection = Nothing
        Dim yaxis1 As Charts.Axis = Nothing
        Dim yaxis2 As Charts.Axis = Nothing

        Dim username As String = Chr(39) & Environment.UserName.ToString & Chr(39)
        sqlchart = clsSQLCommands.GetTopXChartConfigurationSQL(_strNetwork, chartSetName, chartName, username, chartname_original)
        Dim ds_chart As DataSet = DataAccessorODBC.GetDataSet(connStrIOSServer, sqlchart)
        Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to all charts
        '*************************
        'Dim sc As Charts.SeriesCollection = Nothing
        Dim i As Integer
        Dim X1AxisLabel As String = ""
        Dim Y1axislabel As String = "", Y2axislabel As String = ""
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        'Dim yaxis1 As Axis
        'Dim yaxis2 As Axis
        Dim sp As New SmartPalette()
        Dim color_R, color_B, color_G As Integer
        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim chart_elsort() As String = {"0", "0"}
        Dim chart_elvis() As String = {"0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim xval As String = ""

        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1
        Dim scatterObject As String = ""
        Dim primkeys_dt() As DataColumn = Nothing

        Dim primkey_index As Integer = 0
        For Each dc As DataColumn In dtData.Columns
            If dc.DataType = GetType(System.String) Then
                ReDim Preserve primkeys_dt(primkey_index)
                primkeys_dt(primkey_index) = dc
                primkey_index = primkey_index + 1
            End If
        Next
        dtData.PrimaryKey = primkeys_dt

        Dim x As Integer = 0
        Dim obj_columns() As String = Nothing
        For Each dc As DataColumn In dtData.PrimaryKey
            ReDim Preserve obj_columns(x)
            obj_columns(x) = dc.ColumnName
            x = x + 1
        Next

        For rownum = 0 To dt_chart.Rows.Count - 1
            Try
                'collecting elements from chart configuration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                Dim tabindex_new As Integer = CInt(drow(2).ToString)
                chartindex = CInt(drow("ChartIndex").ToString)
                If tabindex_old <> tabindex_new Then
                    tabindex_old = tabindex_new
                End If

                'configures individual chart when new chart line is detected
                If lastchart = "" Or lastchart <> drow("ChartName").ToString Then
                    lastchart = drow("ChartName").ToString.Trim
                    sp.Clear()

                    Y1axisAbsorPerc = drow(13).trim
                    Y2axisAbsOrPerc = nZ(drow(14), "Abs")

                    Y1axisPrecision = CInt(drow(15))
                    Y2axisPrecision = CInt(nZ(drow(16), "0"))

                    If nZ(drow("chartY1axisLabels"), "").Length > 0 Then
                        Y1axislabel = drow("chartY1axisLabels").ToString.Trim
                    End If
                    If nZ(drow("chartY2axisLabels"), "").Length > 0 Then
                        Y2axislabel = drow("chartY2axisLabels").ToString.Trim
                    End If

                    'If CInt(drow(2).ToString) = 99 And tech.ToUpper = _strNetwork.ToUpper Then
                    '    tabindex_new = customTabIndexTopX
                    'End If

                    'Select Case tech.ToLower
                    'Case _strNetwork.ToLower
                    'tblayout = tcTabControlHighTopX.TabPages(tabindex_new).Controls(0) 'drow(2)
                    'ch = tblayout.GetControlFromPosition(0, chartindex) 'drow(4)
                    xval = dtChartStyleProperties.Rows(0)("TargetType").ToString 'flpSourceBtn_GetChecked("topx_" & _strNetwork.ToLower, flpCounterTypeTopX)(0).SourceButtonText
                    'Case Else
                    'Console.WriteLine("AssignData2Charts: problem in tech selection")
                    'Exit Sub
                    'End Select

                    If drow("ChartType") = IOSChartType.Scatter Then
                        'dxCh.Type = ChartType.Scatter
                        'dxCh.Use3D = False
                        'dxCh.DefaultSeries.Type = SeriesType.Marker
                        'dxCh.DefaultSeries.DefaultElement.Transparency = 20
                        'dxCh.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Range
                        'dxCh.XAxis.FormatString = ""
                        'dxCh.DefaultSeries.DefaultElement.ShowValue = False
                        'dxCh.DefaultSeries.DefaultElement.Marker.Visible = True
                        'dxCh.LegendBox.Visible = False
                    End If

                    'dxCh.Annotations.Clear()
                    'dxCh.Annotations.Add(New Annotation(tech))
                    'dxCh.Annotations(0).Position = New System.Drawing.Point(dxCh.Width - 70, 2)
                    'dxCh.Annotations(0).DefaultCorner = BoxCorner.Square
                    'dxCh.Annotations(0).Size = New Size(60, 25)
                    'Dim fnt As System.Drawing.Font = New System.Drawing.Font("Arial", 6, FontStyle.Regular)
                    'dxCh.Annotations(0).Label.Font = fnt

                    'dxCh.TitleBox.Label.Text = drow(6).Trim
                    dxCh.Title.Visible = True
                    dxCh.Title.SetValue(drow(6).Trim)

                    If Me.rptCurrOrConfig = "Config" Then
                        dxCh.Title.SetValue(drow(6).Trim & vbCrLf & "Objects: " & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString)
                    Else
                        dxCh.Title.SetValue(drow(6).Trim & vbCrLf & "Objects: " & ObjectSelectedFromTreeTopXForCurrent)
                    End If

                    'dxCh.DefaultElement.Hotspot.ToolTip = "%SeriesName = %Value"

                    'Y-Axis Settings   
                    'yaxis1 = New Axis
                    'yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    'yaxis1.Label.Text = Y1axislabel

                    'yaxis2 = New Axis
                    'yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right

                    'If dxCh.Type = ChartType.Scatter Then
                    '    If dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ElementAxis") = "Y" Then
                    '        Y1axislabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ChartElements").ToString
                    '    Else
                    '        X1AxisLabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ChartElements").ToString
                    '    End If
                    '    If dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(1)("ElementAxis") = "X" Then
                    '        X1AxisLabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(1)("ChartElements").ToString
                    '    Else
                    '        Y1axislabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(1)("ChartElements").ToString
                    '    End If
                    '    dxCh.DefaultElement.Hotspot.ToolTip = X1AxisLabel & ": %XValue" & Chr(13) & Y1axislabel & ": %Value "
                    '    dxCh.XAxis.Label.Text = X1AxisLabel
                    '    scatterObject = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ObjectTab")
                    'End If

                    'element based
                    Do
                        If ColumnInDataTable(drow(7).trim, dtData) Then
                            ReDim Preserve chart_elements(j)
                            ReDim Preserve chart_elementsYAxis(j)
                            ReDim Preserve chart_Eltype(j)
                            ReDim Preserve chart_ElColor(j)
                            ReDim Preserve chart_elvis(j)

                            chart_elements(j) = drow(7).trim
                            chart_elementsYAxis(j) = drow(9).trim
                            chart_Eltype(j) = drow(8).trim
                            chart_ElColor(j) = CInt(drow(17))

                            'If UCase(chart_elementsYAxis(j)) = "LEFT" Then
                            '    chart_YaxisScale(0) = drow(10).trim
                            '    yaxis1.NumberPrecision = CInt(nZ(drow(15), 0))
                            '    If nZ(drow(11), "").Length > 0 Then
                            '        yaxis1.Label.Text = drow(11).ToString.Trim
                            '    End If
                            '    If nZ(drow(13), " ").Length > 1 Then
                            '        If drow(13).ToString.ToUpper = "PERC" Then
                            '            yaxis1.Percent = True
                            '        End If
                            '    End If
                            '    If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                            '        yaxis1.MinimumInterval = 1

                            '    End If
                            'ElseIf UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                            '    chart_YaxisScale(1) = drow(10).trim
                            '    yaxis2.NumberPrecision = CInt(nZ(drow(16), 0))

                            '    If nZ(drow(12), "").Length > 0 Then
                            '        yaxis2.Label.Text = drow(12).ToString.Trim
                            '    End If
                            '    If nZ(drow(14), " ").Length > 1 Then
                            '        If drow(14).ToString.ToUpper = "PERC" Then
                            '            yaxis2.Percent = True
                            '        End If
                            '    End If
                            '    If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                            '        yaxis2.MinimumInterval = 1

                            '    End If
                            'End If

                            If drow(19).ToString.Trim <> "" Then
                                chart_elsort(0) = drow(7).trim
                                chart_elsort(1) = drow(19).ToString.ToUpper()
                            End If
                            chart_elvis(j) = drow(20).ToString.Trim

                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > dt_chart.Rows.Count - 1 Then
                            Exit Do
                        Else
                            drow = dt_chart.Rows(rownum)
                        End If
                    Loop Until drow(5) <> lastchart
                    rownum = rownum - 1
                    drow = dt_chart.Rows(rownum)

                    'data grid filling
                    'comment: need to skip element if not available in data table!!

                    If chart_elsort(0) <> "0" And ChartElementSortOrder Is Nothing Then
                        dtData.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                    ElseIf Not ChartElementSortOrder Is Nothing Then
                        dtData.DefaultView.Sort = ChartElementSortOrder(0) + " " + ChartElementSortOrder(1)
                    End If

                    Dim columnsfortopx(obj_columns.Length + chart_elements.Length - 1) As String
                    obj_columns.CopyTo(columnsfortopx, 0)
                    chart_elements.CopyTo(columnsfortopx, obj_columns.Count)
                    Dim dt_topx As DataTable = Nothing
                    Dim cellcolumn As String = ""
                    'If tech.ToLower = _strNetwork.ToLower Then
                    Dim dt_subset As DataTable = dtData.DefaultView.ToTable(False, columnsfortopx.Distinct().ToArray())
                    If Not dt_subset Is Nothing AndAlso dt_subset.Rows.Count > 0 Then
                        dt_topx = dt_subset.Rows.Cast(Of DataRow)().Take(dtChartStyleProperties.Rows(0)("TopXRowCount")).CopyToDataTable
                    End If
                    cellcolumn = xval
                    'End If

                    If Not dt_topx Is Nothing Then

                        'If UCase(chart_YaxisScale(0)) = "STACKED" Then
                        '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                        'ElseIf UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                        '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                        'Else
                        '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                        'End If
                        'If UCase(chart_YaxisScale(1)) = "STACKED" Then
                        '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                        'ElseIf UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                        '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                        'Else
                        '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                        'End If

                        'If dxCh.Type = ChartType.Scatter Then
                        '    Try
                        '        dxCh.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
                        '        Dim minValue As Double = dtData.Compute("Min(" & X1AxisLabel & ")", "")
                        '        Dim maxValue As Double = dtData.Compute("Max(" & X1AxisLabel & ")", "")
                        '        dxCh.XAxis.ScaleRange.ValueLow = IIf(minValue < 0, System.Math.Floor(minValue), System.Math.Ceiling(minValue))
                        '        dxCh.XAxis.ScaleRange.ValueHigh = IIf(maxValue < 0, System.Math.Floor(maxValue), System.Math.Ceiling(maxValue))
                        '    Catch
                        '    End Try
                        'End If

                        'chart filling
                        'Dim de As DataEngine = New DataEngine(dt_topx)
                        'If dxCh.ChartType = Charts.ChartType.ScatterLine Then
                        '    de.DataFields = "XValue=" & X1AxisLabel & ",YValue=" & Y1axislabel & ",Object=" & scatterObject
                        'Else
                        '    de.DataFields = String2DataFields_TopX(chart_elements, xval, chart_elvis)
                        'End If
                        'Dim sc As New SeriesCollection
                        'sc = de.GetSeries()

                        'wsDt.Rows(0)(chartDataFirstColIndex).SetValue(drow(6).ToString.Trim)
                        richTxtStr = New RichTextString()
                        richTxtStr.AddTextRun(drow(6).ToString.Trim, New RichTextRunFont("Calibri", 12, Color.DarkBlue))
                        wsDt.Rows(0)(chartDataFirstColIndex).SetRichText(richTxtStr)

                        wsDt.Import(dt_topx, True, 1, chartDataFirstColIndex)
                        dxCh.SelectData(wsDt.Range.FromLTRB(chartDataFirstColIndex, 1, chartDataFirstColIndex + dt_topx.Columns.Count - 1, dt_topx.Rows.Count + 1), Charts.ChartDataDirection.Column)
                        dxCh.Legend.Position = Charts.LegendPosition.Bottom
                        dxCh.Width = dtChartStyleProperties.Rows(0)("ObjectWidth")
                        dxCh.Height = dtChartStyleProperties.Rows(0)("ObjectHeight")

                        yaxis1 = dxCh.PrimaryAxes(1)
                        yaxis1.Title.Visible = True
                        yaxis1.Title.SetValue(Y1axislabel)

                        sc = dxCh.Series
                        For i = 0 To chart_elements.Count - 1
                            For j = 0 To sc.Count - 1
                                If chart_elements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                    Select Case UCase(chart_elementsYAxis(i).Trim)
                                        Case "LEFT"
                                            yaxis1 = dxCh.PrimaryAxes(1)
                                            yaxis1.Title.Visible = True
                                            yaxis1.Title.SetValue(Y1axislabel)
                                        Case "RIGHT"
                                            sc(j).AxisGroup = Charts.AxisGroup.Secondary
                                            yaxis2 = dxCh.SecondaryAxes(1)
                                            yaxis2.Title.Visible = True
                                            yaxis2.Title.SetValue(Y2axislabel)
                                    End Select
                                End If
                            Next
                        Next

                        sc = dxCh.Series
                        For i = 0 To chart_elements.Count - 1
                            For j = 0 To sc.Count - 1
                                If chart_elements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                    color_R = CLng(chart_ElColor(i)) Mod 256
                                    color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                                    color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                                    sc(j).Fill.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))

                                    Select Case UCase(chart_Eltype(i).Trim)
                                        Case "LINE"
                                            sc(j).ChangeType(Charts.ChartType.Line)
                                            sc(j).Outline.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))
                                        Case "BAR"
                                            sc(j).ChangeType(Charts.ChartType.ColumnClustered)
                                            sc(j).GapWidth = 25
                                        Case "AREALINE"
                                            sc(j).ChangeType(Charts.ChartType.Area)
                                    End Select
                                End If
                            Next
                        Next

                        chartDataFirstColIndex = chartDataFirstColIndex + dt_topx.Columns.Count + 1

                        dt_topx.Dispose()
                        dt_topx = Nothing
                        sc = Nothing

                    End If

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim Preserve chart_elvis(0)

                    j = 0
                End If
            Catch ex As Exception

                ReDim chart_elements(0)
                ReDim chart_elementsYAxis(0)
                ReDim chart_Eltype(0)
                ReDim chart_ElColor(0)
                ReDim chart_YaxisScale(1)
                ReDim Preserve chart_elvis(0)
                j = 0
            End Try
        Next
        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing
    End Sub

    Private Function GetStatsObjectTimeData(ByVal targetType As String, ByVal chartName As String, tech As String) As DataTable
        Try
            Dim dtStatsObjectTime As DataTable = Nothing
            cm_Chart_kpiname = chartName.Replace("ObjectTime", "").Trim

            'Constructing STATS SQL
            Dim aggr_from As String = ""
            Dim sql_all As System.Collections.Specialized.StringCollection = New System.Collections.Specialized.StringCollection()
            Dim sql_crosstabobj As New List(Of String)
            Dim sql_tables As String

            'get KPI sql
            Dim conn_el As Odbc.OdbcConnection = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()

            sql_tables = clsSQLCommands.GetProcessStatsQuery(tech, cm_Chart_kpiname)

            Dim comm_Element As Odbc.OdbcCommand = New Odbc.OdbcCommand(sql_tables, conn_el)
            Dim dr As Odbc.OdbcDataReader = comm_Element.ExecuteReader
            Dim sourcetable As String = ""
            Dim aliastable As String = ""
            Dim joinobjects As String = ""

            Dim lstOfSelectedCounterTypes As New List(Of String)
            lstOfSelectedCounterTypes.Add(dtChartStyleProperties.Rows(0)("CounterType").ToString)

            While dr.Read
                If lstOfSelectedCounterTypes.Contains(dr("Object").ToString.ToUpper) Then
                    sourcetable = nZ(dr.GetValue(0).ToString.Trim, "")
                    joinobjects = nZ(dr.GetValue(1).ToString.Trim, "")
                    sql_crosstabobj.Add(nZ(dr.GetValue(2), "").ToString.Trim)
                    aggr_from = nZ(dr.GetValue(3).ToString.Trim, "")
                    sql_all.Add(SQL_Construct_ObjectTime(aggr_from, sourcetable, cm_Chart_kpiname))
                End If
            End While

            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            For Each sql_to_fire As String In sql_all
                dsStatsObjectTime = GetData(sql_to_fire)
                dtStatsObjectTime = CrossTab(dsStatsObjectTime.Tables(0), "DATE", targetType.ToUpper, cm_Chart_kpiname)
            Next

            Return dtStatsObjectTime
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        Return Nothing
    End Function

    Private Sub AssignDataToObjectTimeChartExcel(ByRef dxCh As Charts.Chart, ByRef wsDt As Worksheet, ByVal chartName As String, ByRef dtData As DataTable)
        'Dim sqlchart As String
        Console.WriteLine("Assigning data to chart: " & chartName)

        Dim ds_chart As DataSet = clsSQLCommands.GetChartConfigurationByElementAndChart(connStrIOSServer, _strNetwork, cm_Chart_kpiname, chartName)
        Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to object time charts
        Dim i As Integer
        Dim Y1axislabel As String
        Dim Y2axislabel As String
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Charts.Axis = Nothing
        Dim yaxis2 As Charts.Axis = Nothing
        Dim sp As New SmartPalette()
        Dim sc As Charts.SeriesCollection = Nothing
        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0

        For rownum = 0 To 0
            Try
                'collecting elements from chart confguration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                'configures individual chart when new chartline is detected
                If lastchart = "" Or lastchart <> drow(5).ToString Then
                    lastchart = drow(5).ToString.Trim
                    sp.Clear()

                    Y1axisAbsorPerc = nZ(drow(13), "Abs")
                    Y2axisAbsOrPerc = nZ(drow(14), "Abs")

                    Y1axisPrecision = CInt(nZ(drow(15), "0"))
                    Y2axisPrecision = CInt(nZ(drow(16), "0"))

                    Y1axislabel = nZ(drow(11), " ")
                    Y2axislabel = nZ(drow(12), " ")

                    chart_elementsYAxis(0) = nZ(drow(9), " ")

                    'SetChartXAxis(objChartProp.Technology, objectscharted, dxCh)

                    'dxCh.Annotations.Clear()
                    'dxCh.Annotations.Add(New Annotation(objChartProp.Technology.ToUpper))
                    'dxCh.TitleBox.HeaderLabel.Text = drow(6).Trim & "   -   CLUSTER" & "  -   KPI: " & cm_Chart_kpiname
                    dxCh.Title.Visible = True

                    If Me.rptCurrOrConfig = "Config" Then
                        dxCh.Title.SetValue(drow(6).Trim & "   -   CLUSTER" & "  -   KPI: " & cm_Chart_kpiname & vbCrLf & "Objects: " & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString)
                    Else
                        dxCh.Title.SetValue(drow(6).Trim & "   -   CLUSTER" & "  -   KPI: " & cm_Chart_kpiname & vbCrLf & "Objects: " & ObjectSelectedFromTreeForCurrent)
                    End If

                    'dxCh.TitleBox.Label.Alignment = StringAlignment.Near
                    'dxCh.TitleBox.Label.LineAlignment = StringAlignment.Near

                    If (dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly") Or (dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw") Then
                        'dxCh.DefaultElement.Hotspot.ToolTip = "DATE: <%XValue,dd/MM/yy HH:mm>" & Chr(13) & "%SeriesName: %Value "
                    Else
                        'dxCh.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
                    End If

                    Dim charttitle As String = drow(6).Trim

                    'Y-Axis Settingso   
                    'If chart_elementsYAxis(i).Trim.ToUpper = "LEFT" Then
                    'yaxis1 = New Axis
                    'yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    'yaxis1.Label.Text = Y1axislabel
                    'If UCase(Y1axisAbsorPerc) = "ABS" Then
                    '    yaxis1.Percent = False
                    'ElseIf UCase(Y1axisAbsorPerc) = "PERC" Then
                    '    yaxis1.Percent = True
                    'End If
                    'yaxis1.NumberPrecision = Y1axisPrecision
                    'If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                    '    yaxis1.MinimumInterval = 1
                    'End If
                    'yaxis1.Scale = Scale.Range
                    'Else
                    'yaxis2 = New Axis
                    'yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    'yaxis2.Label.Text = Y2axislabel
                    'If UCase(Y2axisAbsOrPerc) = "PERC" Then
                    '    yaxis2.Percent = True
                    'ElseIf UCase(Y2axisAbsOrPerc) = "ABS" Then
                    '    yaxis2.Percent = False
                    'End If
                    'yaxis2.NumberPrecision = Y2axisPrecision
                    'If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                    '    yaxis2.MinimumInterval = 1
                    'End If
                    'yaxis2.Scale = Scale.Range
                    'End If

                    '+++++++++++++++++++++++++++++++++++++++
                    j = 0

                    For Each col As DataColumn In dtData.Columns
                        ReDim Preserve chart_elements(j)
                        If col.ColumnName.ToUpper <> "DATE" Then
                            chart_elements(j) = col.ColumnName.ToUpper
                            j = j + 1
                        End If
                    Next

                    For Each dr As DataRow In dtData.Rows
                        For Each col As DataColumn In dtData.Columns
                            If dr(col).ToString = "" Then
                                dr(col) = 0
                            End If
                        Next
                    Next

                    'add data to excel chart
                    dxCh.SelectData(wsDt.Range.FromLTRB(chartDataFirstColIndex, 1, chartDataFirstColIndex + dtData.Columns.Count - 1, dtData.Rows.Count + 1), Charts.ChartDataDirection.Column)
                    dxCh.Legend.Position = Charts.LegendPosition.Bottom
                    dxCh.Width = dtChartStyleProperties.Rows(0)("ObjectWidth")
                    dxCh.Height = dtChartStyleProperties.Rows(0)("ObjectHeight")

                    yaxis1 = dxCh.PrimaryAxes(1)
                    yaxis1.Title.Visible = True
                    yaxis1.Title.SetValue(Y1axislabel)

                    sc = dxCh.Series
                    For i = 0 To chart_elements.Count - 1
                        For j = 0 To sc.Count - 1
                            If chart_elements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                Select Case UCase(chart_elementsYAxis(0).Trim)
                                    Case "LEFT"
                                        yaxis1 = dxCh.PrimaryAxes(1)
                                        yaxis1.Title.Visible = True
                                        yaxis1.Title.SetValue(Y1axislabel)
                                    Case "RIGHT"
                                        sc(j).AxisGroup = Charts.AxisGroup.Secondary
                                        yaxis2 = dxCh.SecondaryAxes(1)
                                        yaxis2.Title.Visible = True
                                        yaxis2.Title.SetValue(Y2axislabel)
                                End Select
                            End If
                        Next
                    Next

                    sc = dxCh.Series
                    Dim rnd As Random = New Random(10)

                    For i = 0 To chart_elements.Count - 1
                        For j = 0 To sc.Count - 1
                            If chart_elements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                sc(j).ChangeType(Charts.ChartType.Line)
                                sc(j).Outline.SetSolidFill(Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255)))
                            End If
                        Next
                    Next

                    dxCh.PrimaryAxes(0).Position = Charts.AxisPosition.Bottom
                    dxCh.PrimaryAxes(0).TextDirection = Charts.ShapeTextDirection.Horizontal
                    dxCh.PrimaryAxes(0).TextRotation = -45

                    sc = Nothing
                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    j = 0

                End If

            Catch ex As Exception

            End Try
        Next
        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing
        If Not dtData Is Nothing Then
            dtData.Dispose()
        End If
    End Sub

    Private Sub AssignDataToTopXChartExcel_Delta(ByRef dxCh As Charts.Chart, ByRef wsDt As Worksheet, ByVal chartname As String, ByRef ds As DataSet, Optional chartname_original As String = Nothing, Optional ChartElementSortOrder() As String = Nothing)
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""
        Dim sc As Charts.SeriesCollection = Nothing
        Dim yaxis1 As Charts.Axis = Nothing
        Dim yaxis2 As Charts.Axis = Nothing

        'Dim selected_tabs As String = tvKPITopX.GetKPIChecked2String(1, "ObjectName")
        'Dim selected_charts As String = tvKPITopX.GetKPIChecked2String(2, "ObjectName")
        'Dim selected_kpis As String = tvKPITopX.GetKPIChecked2String(3, "ObjectName")

        Dim username As String = Chr(39) & Environment.UserName.ToString & Chr(39)
        sqlchart = clsSQLCommands.GetTopXChartConfigurationSQL(_strNetwork, chartSetName, chartname, username, chartname_original)
        Dim ds_chart As DataSet = DataAccessorODBC.GetDataSet(connStrIOSServer, sqlchart)
        Dim dt_chart As DataTable = ds_chart.Tables(0)

        'sqlchart = clsSQLCommands.GetTopXChartConfigurationDeltaSQL(tech, cmbChartSetNameTopX.SelectedItem.ToString, selected_tabs, selected_charts, selected_kpis, username, chartname_original)
        'Dim ds_chart As DataSet = DataAccessorODBC.GetDataSet(connStrIOSServer, sqlchart)
        'Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to all charts
        '*************************
        'Dim ch As Chart
        Dim i As Integer
        Dim Y1axislabel, Y2axislabel As String
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim sp As New SmartPalette()
        'Dim sc As New SeriesCollection
        Dim color_R, color_B, color_G As Integer
        Dim tblayout As TableLayoutPanel
        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim chart_elsort() As String = {"0", "0"}
        Dim chart_elvis() As String = {"0"}
        Dim chart_elLineSize() As Integer = {0}
        Dim chart_elShowdatapoints() As Boolean = {False}
        Dim chart_elSeriesVisible() As Boolean = {True}
        Dim chart_elAutoScale() As Boolean = {True}

        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim xval As String = ""

        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1
        Dim dt As DataTable = ds.Tables("Delta")
        Dim x As Integer = 0
        Dim obj_columns() As String = Nothing
        For Each dc As DataColumn In dt.PrimaryKey
            ReDim Preserve obj_columns(x)
            obj_columns(x) = dc.ColumnName
            x = x + 1
        Next

        'Dim KeysToRemove As New List(Of String)
        'If chartname_original Is Nothing Then
        '    For Each key As String In dict_TopXDelta_SeriesCollection.Keys
        '        If key.Split("|")(0).ToUpper = tech.ToUpper Then
        '            KeysToRemove.Add(key)
        '        End If
        '    Next
        'End If
        'For Each keytoRemove In KeysToRemove
        '    dict_TopXDelta_SeriesCollection.Remove(keytoRemove)
        'Next

        For rownum = 0 To dt_chart.Rows.Count - 1
            Try
                'collecting elements from chart configuration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                'TESTING
                Dim tabindex_new As Integer = CInt(drow(2).ToString)
                chartindex = CInt(drow(4).ToString)
                If tabindex_old <> tabindex_new Then
                    tabindex_old = tabindex_new
                End If

                'configures individual chart when new chart line is detected
                If lastchart = "" Or lastchart <> drow(5).ToString Then
                    lastchart = drow(5).ToString.Trim
                    sp.Clear()

                    Y1axisAbsorPerc = drow(13).trim
                    Y2axisAbsOrPerc = nZ(drow(14), "Abs")

                    Y1axisPrecision = CInt(drow(15))
                    Y2axisPrecision = CInt(nZ(drow(16), "0"))
                    Y1axislabel = nZ(drow(11), " ")
                    Y2axislabel = nZ(drow(12), " ")

                    'If CInt(drow(2).ToString) = 99 And tech.ToUpper = "TOPX_" & _strNetwork.ToUpper Then
                    '    tabindex_new = customTabIndexTopX
                    'End If

                    'Select Case tech.ToLower
                    '    Case "topx_" & _strNetwork.ToLower
                    '        tblayout = tcTabControlHighTopX.TabPages(tabindex_new).Controls(0) 'drow(2)
                    '        ch = tblayout.GetControlFromPosition(0, chartindex) 'drow(4)
                    '        xval = flpSourceBtn_GetChecked("topx_" & _strNetwork.ToLower, flpCounterTypeTopX)(0).SourceButtonText
                    '    Case Else
                    '        MsgBox("AssignData2Charts: problem in tech selection")
                    '        Exit Sub
                    'End Select

                    'If chartname_original Is Nothing Then
                    '    ch.Annotations.Clear()
                    '    ch.Annotations.Add(New Annotation(tech))
                    '    ch.Annotations(0).Position = New System.Drawing.Point(ch.Width - 70, 2)
                    '    ch.Annotations(0).DefaultCorner = BoxCorner.Square
                    '    ch.Annotations(0).Size = New Size(60, 25)
                    '    Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                    '    ch.Annotations(0).Label.Font = fnt
                    'End If

                    'ch.Name = drow("ChartName").ToString
                    'ch.TitleBox.Label.Text = drow(6).Trim
                    'ch.DefaultElement.Hotspot.ToolTip = "%SeriesName = %Value" & " "

                    'Y-Axis Settings   
                    'yaxis1 = New Axis
                    'yaxis1.Orientation = Orientation.Left
                    'yaxis1.Label.Text = Y1axislabel

                    'yaxis2 = New Axis
                    'yaxis2.Orientation = Orientation.Right

                    dxCh.Title.Visible = True
                    dxCh.Title.SetValue(drow(6).Trim)

                    If Me.rptCurrOrConfig = "Config" Then
                        dxCh.Title.SetValue(drow(6).Trim & vbCrLf & "Objects: " & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString)
                    Else
                        dxCh.Title.SetValue(drow(6).Trim & vbCrLf & "Objects: " & ObjectSelectedFromTreeTopXForCurrent)
                    End If

                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    'element based
                    Do
                        If ColumnInDataTable(drow(7).trim, dt) Then
                            ReDim Preserve chart_elements(j)
                            ReDim Preserve chart_elementsYAxis(j)
                            ReDim Preserve chart_Eltype(j)
                            ReDim Preserve chart_ElColor(j)
                            ReDim Preserve chart_elvis(j)
                            ReDim Preserve chart_elLineSize(j)
                            ReDim Preserve chart_elShowdatapoints(j)
                            ReDim Preserve chart_elSeriesVisible(j)
                            ReDim Preserve chart_elAutoScale(j)

                            chart_elements(j) = drow(7).trim
                            chart_elementsYAxis(j) = drow(9).trim
                            chart_Eltype(j) = drow(8).trim
                            chart_ElColor(j) = CInt(drow(17))

                            'If UCase(chart_elementsYAxis(j)) = "LEFT" Then
                            '    chart_YaxisScale(0) = drow(10).trim
                            '    yaxis1.NumberPrecision = CInt(nZ(drow(15), 0))
                            '    If nZ(drow(11), "").Length > 0 Then
                            '        yaxis1.Label.Text = drow(11).ToString.Trim
                            '    End If
                            '    If nZ(drow(13), " ").Length > 1 Then
                            '        If drow(13).ToString.ToUpper = "PERC" Then
                            '            yaxis1.Percent = True
                            '        End If
                            '    End If
                            '    If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                            '        yaxis1.MinimumInterval = 1

                            '    End If
                            'ElseIf UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                            '    chart_YaxisScale(1) = drow(10).trim
                            '    yaxis2.NumberPrecision = CInt(nZ(drow(16), 0))

                            '    If nZ(drow(12), "").Length > 0 Then
                            '        yaxis2.Label.Text = drow(12).ToString.Trim
                            '    End If
                            '    If nZ(drow(14), " ").Length > 1 Then
                            '        If drow(14).ToString.ToUpper = "PERC" Then
                            '            yaxis2.Percent = True
                            '        End If
                            '    End If
                            '    If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                            '        yaxis2.MinimumInterval = 1
                            '    End If
                            'End If

                            If drow(19).ToString.Trim <> "" Then
                                chart_elsort(0) = drow(7).trim
                                chart_elsort(1) = drow(19).ToString.ToUpper()
                            End If
                            chart_elvis(j) = drow(20).ToString.Trim
                            chart_elLineSize(j) = nZ(drow("LineSize").ToString.Trim, 3)
                            chart_elShowdatapoints(j) = nZ(drow("ShowDatapoints").ToString.Trim, False)
                            chart_elSeriesVisible(j) = nZ(drow("IsVisible").ToString.Trim, True)
                            chart_elAutoScale(j) = nZ(drow("AutoScale").ToString.Trim, True)

                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > dt_chart.Rows.Count - 1 Then
                            Exit Do
                        Else
                            drow = dt_chart.Rows(rownum)
                        End If
                    Loop Until drow(5) <> lastchart
                    rownum = rownum - 1
                    drow = dt_chart.Rows(rownum)

                    'datagrid filling
                    'comment: need to skip element if not available in datatable!!

                    If chart_elsort(0) <> "0" And ChartElementSortOrder Is Nothing Then
                        dt.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                    ElseIf Not ChartElementSortOrder Is Nothing Then
                        dt.DefaultView.Sort = ChartElementSortOrder(0) + " " + ChartElementSortOrder(1)
                    End If

                    'construct filter
                    'Dim kpifilter As String = Nothing

                    'For Each el As String In chart_elements
                    '    For Each nd As TreeListViewNode In tlvFiltersTopX.Nodes
                    '        If el.ToString = nd.SubItems(0).Text.ToString Then
                    '            If kpifilter = Nothing Then
                    '                kpifilter = el.ToString + "  " + nd.SubItems(1).Text.ToString + "  " + nd.SubItems(2).Text.ToString
                    '            Else
                    '                kpifilter = kpifilter + " AND " + el.ToString + "  " + nd.SubItems(1).Text.ToString + "  " + nd.SubItems(2).Text.ToString
                    '            End If
                    '        End If
                    '    Next
                    'Next

                    'dt.DefaultView.RowFilter = kpifilter
                    Dim chart_elements_count As Integer = chart_elements.Count
                    For k = 0 To chart_elements_count - 1
                        ReDim Preserve chart_elements(chart_elements.Count + 1)
                        ReDim Preserve chart_Eltype(chart_Eltype.Count + 1)
                        ReDim Preserve chart_elementsYAxis(chart_elementsYAxis.Count + 1)
                        ReDim Preserve chart_ElColor(chart_ElColor.Count + 1)
                        ReDim Preserve chart_elShowdatapoints(chart_elShowdatapoints.Count + 1)
                        ReDim Preserve chart_elLineSize(chart_elLineSize.Count + 1)
                        ReDim Preserve chart_elSeriesVisible(chart_elSeriesVisible.Count + 1)
                        ReDim Preserve chart_elAutoScale(chart_elAutoScale.Count + 1)

                        chart_elements(chart_elements.Count - 2) = chart_elements(k) + "_Before"
                        chart_elements(chart_elements.Count - 1) = chart_elements(k) + "_After"
                        chart_Eltype(chart_Eltype.Count - 2) = chart_Eltype(k)
                        chart_Eltype(chart_Eltype.Count - 1) = chart_Eltype(k)
                        chart_elementsYAxis(chart_elementsYAxis.Count - 2) = chart_elementsYAxis(k)
                        chart_elementsYAxis(chart_elementsYAxis.Count - 1) = chart_elementsYAxis(k)
                        chart_ElColor(chart_ElColor.Count - 2) = chart_ElColor(k)
                        chart_ElColor(chart_ElColor.Count - 1) = chart_ElColor(k)
                        chart_elShowdatapoints(chart_elShowdatapoints.Count - 2) = chart_elShowdatapoints(k)
                        chart_elShowdatapoints(chart_elShowdatapoints.Count - 1) = chart_elShowdatapoints(k)
                        chart_elLineSize(chart_elLineSize.Count - 2) = chart_elLineSize(k)
                        chart_elLineSize(chart_elLineSize.Count - 1) = chart_elLineSize(k)
                        chart_elSeriesVisible(chart_elSeriesVisible.Count - 2) = chart_elSeriesVisible(k)
                        chart_elSeriesVisible(chart_elSeriesVisible.Count - 1) = chart_elSeriesVisible(k)
                        chart_elAutoScale(chart_elAutoScale.Count - 2) = chart_elAutoScale(k)
                        chart_elAutoScale(chart_elAutoScale.Count - 1) = chart_elAutoScale(k)
                    Next

                    Dim columnsfortopx(obj_columns.Length + chart_elements.Length - 1) As String
                    obj_columns.CopyTo(columnsfortopx, 0)
                    chart_elements.CopyTo(columnsfortopx, obj_columns.Count)

                    Dim dt_topx As DataTable = Nothing
                    Dim cellcolumn As String = ""

                    Dim dt_subset As DataTable = dt.DefaultView.ToTable(False, columnsfortopx.Distinct().ToArray())
                    If Not dt_subset Is Nothing AndAlso dt_subset.Rows.Count > 0 Then
                        dt_topx = dt_subset.Rows.Cast(Of DataRow)().Take(dtChartStyleProperties.Rows(0)("TopXRowCount")).CopyToDataTable
                    End If
                    cellcolumn = xval

                    'Dim dgCtrl As GridControl = tblayout.GetControlFromPosition(1, chartindex)
                    'dgCtrl.DataSource = Nothing
                    'dgCtrl.DataSource = dt_topx
                    'dgCtrl.Tag = tech
                    'Dim dg As GridView = dgCtrl.MainView
                    'AddHandler dg.KeyDown, AddressOf dgTopXGridView_KeyDown

                    'For Each col As Columns.GridColumn In dg.Columns
                    '    If col.UnboundType = DevExpress.Data.UnboundColumnType.String Then
                    '        For Each srcbtn As IOS.Library.IOSToggleButton In flpCounterTypeTopX.Controls
                    '            If srcbtn.Text.ToLower <> col.Caption.ToLower Then
                    '                col.Visible = True
                    '            Else
                    '                col.Visible = False
                    '                Exit For
                    '            End If
                    '        Next
                    '    Else
                    '        col.Visible = False
                    '    End If
                    'Next

                    'For i = 0 To UBound(chart_elements)
                    '    For Each col As Columns.GridColumn In dg.Columns
                    '        If col.FieldName.ToUpper = chart_elements(i).ToUpper Then
                    '            col.Visible = True
                    '        End If
                    '    Next
                    'Next

                    'dg.Columns(cellcolumn).Visible = True
                    'If Not ChartElementSortOrder Is Nothing Then
                    '    dg.Columns(ChartElementSortOrder(0)).SortOrder = IIf(ChartElementSortOrder(1) = "ASC", DevExpress.Data.ColumnSortOrder.Ascending, DevExpress.Data.ColumnSortOrder.Descending)
                    'End If
                    'dg.OptionsView.ColumnAutoWidth = False
                    'dg.BestFitColumns(True)
                    'dgCtrl.Refresh()

                    If Not dt_topx Is Nothing Then

                        'If UCase(chart_YaxisScale(0)) = "STACKED" Then
                        '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                        'ElseIf UCase(chart_YaxisScale(0)) = "FULLSTACKED" Then
                        '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                        'Else
                        '    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                        'End If
                        'If UCase(chart_YaxisScale(1)) = "STACKED" Then
                        '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                        'ElseIf UCase(chart_YaxisScale(1)) = "FULLSTACKED" Then
                        '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                        'Else
                        '    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                        'End If

                        ''chart filling
                        'Dim de As DataEngine = New DataEngine(dg.DataSource)
                        'de.DataFields = String2DataFields_TopX(chart_elements, xval, chart_elvis)
                        'sc = de.GetSeries()

                        'For i = 0 To sc.Count() - 1

                        '    Select Case UCase(chart_Eltype(i).Trim)
                        '        Case "LINE"
                        '            sc(i).Type = SeriesType.Line
                        '            sc(i).Line.Width = CInt(chart_elLineSize(i))
                        '        Case "BAR"
                        '            sc(i).Type = SeriesType.Bar
                        '        Case "AREALINE"
                        '            sc(i).Type = SeriesType.AreaLine
                        '    End Select

                        '    Select Case UCase(chart_elementsYAxis(i).Trim)
                        '        Case "LEFT"
                        '            sc(i).YAxis = yaxis1
                        '            If chart_elAutoScale(i) = False Then
                        '                yaxis1.Minimum = 0
                        '            End If
                        '        Case "RIGHT"
                        '            sc(i).YAxis = yaxis2
                        '            If chart_elAutoScale(i) = False Then
                        '                yaxis2.Minimum = 0
                        '            End If
                        '    End Select

                        '    color_R = CLng(chart_ElColor(i)) Mod 256
                        '    color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                        '    color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                        '    If sc(i).Name.EndsWith("_Before") Then
                        '        sc(i).DefaultElement.Color = Color.FromArgb(200, 0, 255, 0)
                        '    ElseIf sc(i).Name.EndsWith("_After") Then
                        '        sc(i).DefaultElement.Color = Color.FromArgb(200, 255, 255, 0)
                        '    Else
                        '        sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
                        '    End If

                        '    If CBool(chart_elShowdatapoints(i)) = True Then
                        '        sc(i).DefaultElement.Marker.Type = ElementMarkerType.Circle
                        '        sc(i).DefaultElement.Marker.Size = 5
                        '        sc(i).EmptyElement.Mode = EmptyElementMode.None
                        '        sc(i).DefaultElement.Marker.Visible = True
                        '    Else
                        '        sc(i).DefaultElement.Marker.Type = ElementMarkerType.None
                        '        sc(i).DefaultElement.Marker.Visible = False
                        '    End If

                        '    If chart_elSeriesVisible(i) = True Then
                        '        sc(i).Visible = True
                        '    Else
                        '        sc(i).Visible = False
                        '        'HiddenSeriesCollectionTopX.Add(ch.Name, sc(i).Name)
                        '    End If

                        '    If chartname_original Is Nothing Then
                        '        If chart_elements(i).EndsWith("_Before") Or chart_elements(i).EndsWith("_After") Then
                        '            sc(i).Visible = False
                        '        End If
                        '    Else
                        '        For Each seriesname As String In SeriesInVisible
                        '            If sc(i).Name = seriesname Then
                        '                sc(i).Visible = False
                        '            End If
                        '        Next
                        '    End If
                        'Next

                        richTxtStr = New RichTextString()
                        richTxtStr.AddTextRun(drow(6).ToString.Trim, New RichTextRunFont("Calibri", 12, Color.DarkBlue))
                        wsDt.Rows(0)(chartDataFirstColIndex).SetRichText(richTxtStr)

                        wsDt.Import(dt_topx, True, 1, chartDataFirstColIndex)
                        dxCh.SelectData(wsDt.Range.FromLTRB(chartDataFirstColIndex, 1, chartDataFirstColIndex + dt_topx.Columns.Count - 1, dt_topx.Rows.Count + 1), Charts.ChartDataDirection.Column)
                        dxCh.Legend.Position = Charts.LegendPosition.Bottom
                        dxCh.Width = dtChartStyleProperties.Rows(0)("ObjectWidth")
                        dxCh.Height = dtChartStyleProperties.Rows(0)("ObjectHeight")

                        yaxis1 = dxCh.PrimaryAxes(1)
                        yaxis1.Title.Visible = True
                        yaxis1.Title.SetValue(Y1axislabel)

                        sc = dxCh.Series
                        For i = 0 To chart_elements.Count - 1
                            For j = 0 To sc.Count - 1
                                If chart_elements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                    Select Case UCase(chart_elementsYAxis(i).Trim)
                                        Case "LEFT"
                                            yaxis1 = dxCh.PrimaryAxes(1)
                                            yaxis1.Title.Visible = True
                                            yaxis1.Title.SetValue(Y1axislabel)
                                        Case "RIGHT"
                                            sc(j).AxisGroup = Charts.AxisGroup.Secondary
                                            yaxis2 = dxCh.SecondaryAxes(1)
                                            yaxis2.Title.Visible = True
                                            yaxis2.Title.SetValue(Y2axislabel)
                                    End Select
                                End If
                            Next
                        Next

                        sc = dxCh.Series
                        For i = 0 To chart_elements.Count - 1
                            For j = 0 To sc.Count - 1
                                If chart_elements(i).ToLower = sc(j).SeriesName.PlainText.ToLower Then
                                    color_R = CLng(chart_ElColor(i)) Mod 256
                                    color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                                    color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                                    If sc(j).SeriesName.ToString.EndsWith("_Before") Then
                                        sc(i).Fill.SetSolidFill(Color.FromArgb(200, 0, 255, 0))
                                    ElseIf sc(i).SeriesName.ToString.EndsWith("_After") Then
                                        sc(i).Fill.SetSolidFill(Color.FromArgb(200, 255, 255, 0))
                                    Else
                                        sc(i).Fill.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))
                                    End If

                                    'sc(j).Fill.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))

                                    Select Case UCase(chart_Eltype(i).Trim)
                                        Case "LINE"
                                            sc(j).ChangeType(Charts.ChartType.Line)
                                            'sc(j).Outline.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))
                                            If sc(j).SeriesName.ToString.EndsWith("_Before") Then
                                                sc(i).Outline.SetSolidFill(Color.FromArgb(200, 0, 255, 0))
                                            ElseIf sc(i).SeriesName.ToString.EndsWith("_After") Then
                                                sc(i).Outline.SetSolidFill(Color.FromArgb(200, 255, 255, 0))
                                            Else
                                                sc(i).Outline.SetSolidFill(Color.FromArgb(255, color_R, color_G, color_B))
                                            End If
                                        Case "BAR"
                                            sc(j).ChangeType(Charts.ChartType.ColumnClustered)
                                            sc(j).GapWidth = 25
                                        Case "AREALINE"
                                            sc(j).ChangeType(Charts.ChartType.Area)
                                    End Select
                                End If
                            Next
                        Next

                        chartDataFirstColIndex = chartDataFirstColIndex + dt_topx.Columns.Count + 1

                        'If chartname_original Is Nothing And Not dict_TopXDelta_SeriesCollection.ContainsKey(tech.ToUpper + "|" + ch.Name) Then
                        '    dict_TopXDelta_SeriesCollection.Add(tech.ToUpper + "|" + ch.Name, sc)
                        'End If

                        'ch.SeriesCollection.Clear()
                        'ch.SeriesCollection.Add(sc)

                        dt_topx.Dispose()
                        dt_topx = Nothing
                        sc = Nothing
                        'de = Nothing

                        'check if TopXChart DeltaButtons are present
                        'If chartname_original Is Nothing Then
                        '    TopXCharts_AddDeltaButtons(dxCh)
                        'End If

                    End If

                    'ch.RefreshChart()
                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim Preserve chart_elvis(0)
                    'ReDim Preserve chart_elLineSize(0)
                    'ReDim Preserve chart_elShowdatapoints(False)
                    'ReDim Preserve chart_elSeriesVisible(True)
                    'ReDim Preserve chart_elAutoScale(True)

                    j = 0
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                Console.WriteLine(ex.Message.ToString)
            End Try
        Next

        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing
    End Sub

    Public Sub TopXCharts_AddDeltaButtons(ByRef ch As Chart)
        Try
            Dim an_Delta As Annotation = New Annotation("Delta")
            Dim an_Before As Annotation = New Annotation("Before")
            Dim an_After As Annotation = New Annotation("After")

            an_Delta.Background.Color = Color.Orange
            an_Before.Background.Color = Color.LightGray
            an_After.Background.Color = Color.LightGray

            an_Delta.DefaultCorner = BoxCorner.Round
            an_Before.DefaultCorner = BoxCorner.Round
            an_After.DefaultCorner = BoxCorner.Round

            an_Delta.Line.Color = Color.White
            an_Delta.Shadow.Visible = False
            an_Before.Line.Color = Color.White
            an_Before.Shadow.Visible = False
            an_After.Line.Color = Color.White
            an_After.Shadow.Visible = False

            an_Delta.ToolTip = "View Delta Series"
            an_Before.ToolTip = "View Before Series"
            an_After.ToolTip = "View After Series"

            an_Delta.DynamicSize = False
            an_Before.DynamicSize = False
            an_After.DynamicSize = False

            an_Delta.Size = New Size(30, 18)
            an_Before.Size = New Size(30, 18)
            an_After.Size = New Size(30, 18)

            an_Delta.Label.Font = New System.Drawing.Font("Arial", 6, FontStyle.Regular)
            an_Before.Label.Font = New System.Drawing.Font("Arial", 6, FontStyle.Regular)
            an_After.Label.Font = New System.Drawing.Font("Arial", 6, FontStyle.Regular)

            an_Delta.Position = New System.Drawing.Point(ch.Width - 180, 2)
            an_Before.Position = New System.Drawing.Point(ch.Width - 150, 2)
            an_After.Position = New System.Drawing.Point(ch.Width - 120, 2)

            ch.Annotations.Add(an_Delta)
            ch.Annotations.Add(an_Before)
            ch.Annotations.Add(an_After)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CreateCurrentReportInExcel(reportID As Integer, reportName As String, ByRef reportFilePath As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            chartDataFirstColIndex = 0

            ObjectSelectedFromTreeForCurrent = "N/A"
            ObjectSelectedFromTreeTopXForCurrent = "N/A"


            Using wb As New Workbook()
                Dim dxCh As DXChart = Nothing


                Dim dtData As DataTable = Nothing

                Dim wsSlide As Worksheet = Nothing
                Dim wsData As Worksheet = Nothing

                Dim chartTopCellRowIndex As Integer = 0
                Dim chartBottomCellColIndex As Integer = 29

                wb.Unit = DevExpress.Office.DocumentUnit.Point
                wb.BeginUpdate()


                Try
                    ' get report data for the selcted report
                    Dim dtReport As DataTable = dtReports.Select("ReportID=" & reportID).CopyToDataTable()
                    ' set excel file name
                    reportFilePath = GetUserDataPath() & "\Data\" & reportName & "_" & Format(Now(), "yyyyMMdd") & ".xlsx"

                    Dim workSheetDistinct As DataTable = dtReport.AsDataView.ToTable(True, "SlideID", "SlideTitle", "SlideName", "SlideOrdinal")
                    wsData = wb.Worksheets.Add("Charts Data")

                    If (workSheetDistinct.Rows.Count > 0) Then

                        Dim worksheetcount As Int16 = 0

                        For Each drSheet As DataRow In workSheetDistinct.Rows
                            worksheetcount = worksheetcount + 1
                            WaitScreenReportEditor.ShowWaitScreen("Generating Report..." & System.Math.Round(100 * worksheetcount / workSheetDistinct.Rows.Count, 0) & "%")

                            wsSlide = wb.Worksheets.Add(drSheet("SlideName").ToString)
                            wb.Worksheets.ActiveWorksheet = wsSlide

                            Dim dtObjectsPerSheet As DataTable = dtReport.Select("SlideID=" & Chr(39) & drSheet("SlideID").ToString & Chr(39), "ObjectOrdinal ASC").CopyToDataTable()

                            If (dtObjectsPerSheet.Rows.Count > 0) Then

                                chartTopCellRowIndex = 0
                                chartBottomCellColIndex = 29

                                'chartsPerSlide = CInt(dtObjectsPerSheet.Rows.Count)

                                For Each drObject As DataRow In dtObjectsPerSheet.Rows

                                    Try
                                        Dim dtReportObject As DataTable = clsSQLCommands.GetSlidesByReportID(connStrIOSServer, reportID).Select("SlideID=" & drObject("SlideID") & " And ObjectID=" & drObject("ObjectID")).CopyToDataTable

                                        perSlideObjectID = CInt(dtReportObject.Rows(0)("ObjectID"))
                                        _strNetwork = dtReportObject.Rows(0)("Technology").ToString

                                        If _strNetwork.ToLower.Contains("topx") Then
                                            objfrmTech = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(_strNetwork.Replace("TopX_", "").ToUpper)).LastOrDefault()
                                        Else
                                            objfrmTech = objFrmTechList.Where(Function(x) x.Network.ToUpper.Equals(_strNetwork)).LastOrDefault()
                                        End If

                                        GetObjectStyleProperties("Chart")
                                        GetPredefinedPeriod()

                                        If dtReportObject.Rows(0)("Purpose").ToString.ToLower = "charts" Then

                                            If objfrmTech IsNot Nothing Then
                                                dxCh = wsSlide.Charts.Add(Charts.ChartType.ColumnClustered)

                                                dxCh.TopLeftCell = wsSlide.Cells(chartTopCellRowIndex, 0)
                                                dxCh.BottomRightCell = wsSlide.Cells(chartBottomCellColIndex, 25)

                                                SetChartConfigData(_strNetwork, dtReportObject.Rows(0)("ObjectName").ToString)
                                                Dim dtKPI As DataTable = ChartConfig.ChartFillingDataTable.DefaultView.ToTable(True, "ChartElements")
                                                Dim lstKPIs As New List(Of String)
                                                dtData = New DataTable

                                                lstKPIs.Add("Date")
                                                dtData.Columns.Add("Date", GetType(String))

                                                For Each dr As DataRow In dtKPI.Rows
                                                    lstKPIs.Add(dr("ChartElements").ToString.Trim)
                                                    dtData.Columns.Add(dr("ChartElements").ToString.Trim, GetType(Double))
                                                Next

                                                If objfrmTech.dsStats IsNot Nothing Then
                                                    Dim dtComplete As DataTable = objfrmTech.dsStats.Tables(dtChartStyleProperties.Rows(0)("CounterType").ToString)
                                                    Dim dtTemp As DataTable = dtComplete.DefaultView.ToTable(True, lstKPIs.ToArray())

                                                    For Each drow As DataRow In dtTemp.Rows
                                                        Dim dr As DataRow = dtData.NewRow()
                                                        If (objfrmTech.rdoHourlyStats.Checked) Or (objfrmTech.rdoRawStats.Checked) Then
                                                            dr("Date") = CDate(drow("Date")).ToString("yyyy/MM/dd HH:mm")
                                                        Else
                                                            dr("Date") = CDate(drow("Date")).ToString("yyyy/MM/dd")
                                                        End If
                                                        For Each strKPI In lstKPIs
                                                            If strKPI.ToLower <> "date" Then
                                                                If Not IsDBNull(drow(strKPI)) Then
                                                                    dr(strKPI) = CDbl(drow(strKPI))
                                                                End If
                                                            End If
                                                        Next
                                                        dtData.Rows.Add(dr)
                                                        dtData.AcceptChanges()
                                                    Next

                                                    'getting selected objects in current frmTech
                                                    ObjectSelectedFromTreeForCurrent = objfrmTech.GetSelectedObjectNamesFromTreeView(_strNetwork)

                                                    richTxtStr = New RichTextString()
                                                    richTxtStr.AddTextRun(drObject("ObjectNameGUI").ToString, New RichTextRunFont("Calibri", 12, Color.DarkBlue))
                                                    wsData.Rows(0)(chartDataFirstColIndex).SetRichText(richTxtStr)

                                                    wsData.Import(dtData, True, 1, chartDataFirstColIndex)
                                                    AssignDataToStatsChartExcel(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, dtData)
                                                End If
                                            End If

                                            chartDataFirstColIndex = chartDataFirstColIndex + dtData.Columns.Count + 1

                                        ElseIf dtReportObject.Rows(0)("Purpose").ToString.ToLower = "topx" Then

                                            If objfrmTech IsNot Nothing Then
                                                dxCh = wsSlide.Charts.Add(Charts.ChartType.ColumnClustered)

                                                dxCh.TopLeftCell = wsSlide.Cells(chartTopCellRowIndex, 0)
                                                dxCh.BottomRightCell = wsSlide.Cells(chartBottomCellColIndex, 25)

                                                If objfrmTech.dsTopX IsNot Nothing Then
                                                    dtData = objfrmTech.dsTopX.Tables(0)

                                                    'getting selected objects in current frmTech
                                                    ObjectSelectedFromTreeTopXForCurrent = objfrmTech.GetSelectedObjectNamesFromTreeView("TopX_" & _strNetwork)

                                                    If dtReportObject.Rows(0)("TopX_DeltaInterval").ToString <> "" Then
                                                        AssignDataToTopXChartExcel_Delta(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, objfrmTech.dsTopX)
                                                    Else
                                                        AssignDataToTopXChartExcel(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, dtData)
                                                    End If
                                                End If
                                            End If

                                        ElseIf dtReportObject.Rows(0)("Purpose").ToString.ToLower = "objecttime" Then

                                            If objfrmTech IsNot Nothing Then
                                                dxCh = wsSlide.Charts.Add(Charts.ChartType.ColumnClustered)

                                                dxCh.TopLeftCell = wsSlide.Cells(chartTopCellRowIndex, 0)
                                                dxCh.BottomRightCell = wsSlide.Cells(chartBottomCellColIndex, 25)

                                                If objfrmTech.dsStats_ObjectTime IsNot Nothing Then
                                                    Dim dtStatsObjectTime As DataTable = objfrmTech.dsStats_ObjectTime.Tables(0)
                                                    dtData = CrossTab(dtStatsObjectTime, "DATE", dtChartStyleProperties.Rows(0)("TargetType").ToString, dtReportObject.Rows(0)("ObjectName").ToString.Replace("ObjectTime", ""))

                                                    richTxtStr = New RichTextString()
                                                    richTxtStr.AddTextRun(drObject("ObjectNameGUI").ToString, New RichTextRunFont("Calibri", 12, Color.DarkBlue))
                                                    wsData.Rows(0)(chartDataFirstColIndex).SetRichText(richTxtStr)

                                                    'getting selected objects in current frmTech
                                                    ObjectSelectedFromTreeForCurrent = objfrmTech.GetSelectedObjectNamesFromTreeView(_strNetwork)

                                                    wsData.Import(dtData, True, 1, chartDataFirstColIndex)
                                                    AssignDataToObjectTimeChartExcel(dxCh, wsData, dtReportObject.Rows(0)("ObjectName").ToString, dtData)
                                                End If
                                            End If

                                            chartDataFirstColIndex = chartDataFirstColIndex + dtData.Columns.Count + 1

                                        End If

                                    Catch ex As Exception
                                        reportCreated = False
                                        _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                                    End Try

                                    chartTopCellRowIndex = dxCh.BottomRightCell.RowIndex + 2
                                    chartBottomCellColIndex = dxCh.BottomRightCell.RowIndex + 2 + 29

                                Next

                            End If

                        Next

                    End If

                Catch ex As Exception
                    reportCreated = False
                    _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                Finally
                    wb.Worksheets.RemoveAt(0)
                    wb.Worksheets("Charts Data").MoveToEnd()
                    wb.Worksheets.ActiveWorksheet = wb.Worksheets(0)
                    wb.EndUpdate()
                End Try

                wb.Calculate()
                wb.SaveDocument(reportFilePath)

            End Using
        Catch ex As Exception
            reportCreated = False
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

#End Region

    Private Sub CreateReportWithInteropMethod(ByVal pptxFileName As String, ByVal reportName As String, ByVal reportID As String)
        Dim MyApplication As Powerpoint.Application = Nothing
        Dim MyPresentation As Powerpoint.Presentation = Nothing
        Dim MySlide As Powerpoint.Slide

        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            MyApplication = New Powerpoint.Application()
            MyApplication.Visible = True
            MyApplication.WindowState = Powerpoint.PpWindowState.ppWindowMinimized
            Application.DoEvents()
            MyPresentation = MyApplication.Presentations.Open(pptxFileName, , , True)
            Application.DoEvents()

            'TG SPECIFIC
            '----------
            MySlide = MyPresentation.Slides(1)
            Dim realuser As String = Nothing
            Try
                realuser = GetRealNameFromAd(Environment.UserName)
            Catch ex As Exception
                realuser = Environment.UserName.ToString
            End Try

            Dim title As String = reportName
            Dim subtitle As String = "Contact: " & vbTab & vbTab & realuser & vbCr & "Creation Date: " & vbTab & Format(Now, "yyyy-MM-dd")

            For Each s As Powerpoint.Shape In MySlide.Shapes
                If s.HasTextFrame Then
                    If s.TextFrame.TextRange.Text = "<Title>" Then
                        s.TextFrame.TextRange.Text = title
                    End If
                    If s.TextFrame.TextRange.Text = "<Subtitle>" Then
                        s.TextFrame.TextRange.Text = subtitle
                    End If
                End If
            Next
            Try
                MySlide.HeadersFooters.DateAndTime.Text = ""
                MySlide.HeadersFooters.Footer.Text = ""
            Catch
            End Try
            SetNumOfChartsPerRow(True)

            Dim dtReportAll As DataTable = clsSQLCommands.GetSlidesByReportID(connStrIOSServer, CInt(reportID))
            Dim slideDistinct As DataTable = dtReportAll.AsDataView.ToTable(True, "SlideID", "SlideTitle", "SlideName", "SlideOrdinal", "SlideText")

            If (slideDistinct.Rows.Count > 0) Then
                For Each drObject As DataRow In slideDistinct.Rows

                    Dim dtObjectsPerSlide As DataTable = dtReportAll.Select("SlideID=" & Chr(39) & drObject("SlideID").ToString & Chr(39)).CopyToDataTable()
                    If (dtObjectsPerSlide.Rows.Count > 0) Then
                        Dim slidePoperties As SlideProperties = New SlideProperties()
                        slidePoperties.SlideOrdinal = drObject("SlideOrdinal").ToString
                        slidePoperties.SlideText = drObject("SlideText").ToString
                        slidePoperties.SlideTitle = drObject("SlideTitle").ToString

                        Dim powerPointSlide As Powerpoint.Slide = CreatePowerPointSlide(slidePoperties, drObject("SlideName").ToString, MyPresentation, reportName, realuser)
                        For Each drSlide As DataRow In dtObjectsPerSlide.Rows
                            Try
                                If (drSlide("ObjectType").ToString() = "Chart") Then
                                    Dim objChartProperties As ObjectChartProperties = New ObjectChartProperties()
                                    objChartProperties.Technology = drSlide("Technology").ToString
                                    objChartProperties.Width = drSlide("ObjectWidth").ToString
                                    objChartProperties.Height = drSlide("ObjectHeight")
                                    objChartProperties.Top = drSlide("ObjectTopMargin")
                                    objChartProperties.Left = drSlide("ObjectLeftMargin")
                                    objChartProperties.ObjectScale = drSlide("ObjectScale")
                                    CreateChartObjectOnSlide(objChartProperties, drSlide("ObjectName"), powerPointSlide)
                                ElseIf (drSlide("ObjectType").ToString() = "TextBox") Then
                                    Dim textboxProperties As ObjectTextBoxProperties = New ObjectTextBoxProperties()
                                    textboxProperties.Top = drSlide("ObjectTopMargin").ToString()
                                    textboxProperties.Left = drSlide("ObjectLeftMargin").ToString()
                                    textboxProperties.FontColor = Color.FromName(drSlide("TextBoxFontColor").ToString())
                                    textboxProperties.TextBoxText = drSlide("TextBoxText").ToString()
                                    textboxProperties.FontSize = drSlide("TextBoxFontSize").ToString()
                                    textboxProperties.IsBold = drSlide("TextBoxFontIsBold").ToString()
                                    textboxProperties.IsItalic = drSlide("TextBoxFontIsItalic").ToString()
                                    textboxProperties.IsUnderline = drSlide("TextBoxFontIsUnderline").ToString()
                                    textboxProperties.Width = drSlide("ObjectWidth")
                                    textboxProperties.Height = drSlide("ObjectHeight").ToString()
                                    textboxProperties.BorderSize = drSlide("TextBoxBorderSize").ToString()
                                    textboxProperties.BoderColor = Color.FromName(drSlide("TextBoxBoderColor").ToString())
                                    CreateTextBoxObjectOnSlide(textboxProperties, drSlide("ObjectName").ToString(), powerPointSlide)
                                End If
                            Catch ex As Exception
                                reportCreated = False
                                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
                            End Try
                        Next
                    End If
                Next
            End If

            MyPresentation.SaveAs(GetUserDataPath() & "\Data\" & reportName & "_" & Format(Now, "yyyyMMdd_HHmmss"), Powerpoint.PpSaveAsFileType.ppSaveAsPresentation, Microsoft.Office.Core.MsoTriState.msoCTrue)
            Application.DoEvents()
        Catch ex As Exception
            reportCreated = False
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            MsgBox(ex.Message)
        Finally
            SetNumOfChartsPerRow(False)
            GC.Collect()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

#Region "Create Report - OpenXML Method"

    Private Sub CreateReportWithOpenXMLMethod(ByVal pptxFileName As String, ByVal templateFileName As String, ByVal reportName As String, ByVal reportID As String, ByRef reportFilePath As String)
        Try
            Dim realuser As String = Nothing
            Try
                realuser = GetRealNameFromAd(Environment.UserName)
            Catch ex As Exception
                realuser = Environment.UserName.ToString
            End Try

            slidePosition = 1

            ' Get report data for the selcted report
            Dim dtReport As DataTable = dtReports.Select("ReportID=" & reportID).CopyToDataTable()

            Dim title As String = reportName
            Dim subtitle As String = "Contact: " & vbTab & vbTab & realuser & vbCr & "Creation Date: " & vbTab & Format(Now, "yyyy-MM-dd")
            ' Set pptx name
            reportFilePath = GetUserDataPath() & "\Data\" & reportName & "_" & Format(Now(), "yyyyMMdd_HHmmss") & ".pptx"
            Dim dshBrdImgFilePath As String = GetUserDataPath() & "\Data\" & reportName

            ' Embed template file with the presentation
            CreatePresentationWithTemplate(reportFilePath, templateFileName)

            Dim slideDistinct As DataTable = dtReport.AsDataView.ToTable(True, "SlideID", "SlideTitle", "SlideName", "SlideOrdinal")

            If (slideDistinct.Rows.Count > 0) Then

                Dim slidecount As Int16 = 0

                Using PresentationDoc = PresentationDocument.Open(reportFilePath, True)
                    ReplaceTextInSlide(PresentationDoc, reportName, subtitle)

                    For Each drObject As DataRow In slideDistinct.Rows
                        Dim dtObjectsPerSlide As DataTable = dtReport.Select("SlideID=" & Chr(39) & drObject("SlideID").ToString & Chr(39), "ObjectOrdinal ASC").CopyToDataTable()

                        slidecount = slidecount + 1
                        WaitScreenReportEditor.ShowWaitScreen("Generating Report..." & System.Math.Round(100 * slidecount / slideDistinct.Rows.Count, 0) & "%", 0)

                        'Console.WriteLine(vbCrLf & "************************************************************************************************")
                        'Console.WriteLine("Started creating slide: " & drObject("SlideName").ToString)

                        If (dtObjectsPerSlide.Rows.Count > 0) Then

                            If IsDBNull(dtObjectsPerSlide.Rows(0)("DashboardID")) Then

                                chartsPerSlide = CInt(dtObjectsPerSlide.Rows.Count)
                                objSlideProperties = New SlideProperties()
                                objSlideProperties.SlideOrdinal = drObject("SlideOrdinal").ToString
                                objSlideProperties.SlideTitle = drObject("SlideTitle").ToString
                                objSlideProperties.SlideName = drObject("SlideName").ToString

                                For Each drSlide As DataRow In dtObjectsPerSlide.Rows

                                    Dim dtReportObject As DataTable = clsSQLCommands.GetSlidesByReportID(connStrIOSServer, reportID).Select("SlideID=" & drObject("SlideID") & " And ObjectID=" & drSlide("ObjectID")).CopyToDataTable
                                    'Console.WriteLine(vbCrLf & "************************************************************************************************")
                                    'Console.WriteLine("Started creating chart: " & dtReportObject.Rows(0)("ObjectName").ToString)

                                    Try
                                        If (drSlide("ObjectType").ToString() = "Chart") Then

                                            objChartProperties = New ObjectChartProperties()
                                            objChartProperties.Technology = dtReportObject.Rows(0)("Technology").ToString
                                            objChartProperties.Width = dtReportObject.Rows(0)("ObjectWidth").ToString
                                            objChartProperties.Height = dtReportObject.Rows(0)("ObjectHeight").ToString
                                            objChartProperties.Top = dtReportObject.Rows(0)("ObjectTopMargin").ToString
                                            objChartProperties.Left = dtReportObject.Rows(0)("ObjectLeftMargin").ToString
                                            objChartProperties.ObjectScale = dtReportObject.Rows(0)("ObjectScale").ToString
                                            objChartProperties.TargetType = drSlide("TargetType").ToString
                                            'objChartProperties.ChartName = dtReportObject.Rows(0)("ObjectName").ToString
                                            objChartProperties.Purpose = dtReportObject.Rows(0)("Purpose").ToString
                                            objChartProperties.TopXShowObjects = drSlide("TopX_ShowObjects").ToString
                                            perSlideObjectID = CInt(dtReportObject.Rows(0)("ObjectID"))
                                            objChartProperties.TopXDeltaInterval = IIf(IsDBNull(dtReportObject.Rows(0)("TopX_DeltaInterval")), "", dtReportObject.Rows(0)("TopX_DeltaInterval").ToString)

                                            _strNetwork = objChartProperties.Technology

                                            'get object style properties
                                            GetObjectStyleProperties("Chart")
                                            GetPredefinedPeriod()

                                            'generate chart object
                                            If objChartProperties.Purpose.ToLower = "topx" Then
                                                CreateTopXChart(objChartProperties, dtReportObject.Rows(0)("ObjectName"), objSlideProperties, PresentationDoc)
                                            ElseIf objChartProperties.Purpose.ToLower = "charts" Then
                                                CreateStatsChart(objChartProperties, dtReportObject.Rows(0)("ObjectName"), objSlideProperties, PresentationDoc)
                                            ElseIf objChartProperties.Purpose.ToLower = "objecttime" Then
                                                CreateObjectTimeChart(objChartProperties, dtReportObject.Rows(0)("ObjectName"), objSlideProperties, PresentationDoc)
                                            End If

                                        ElseIf (drSlide("ObjectType").ToString() = "TextBox") Then

                                            Dim textboxProperties As ObjectTextBoxProperties = New ObjectTextBoxProperties()
                                            textboxProperties.Top = dtReportObject.Rows(0)("ObjectTopMargin").ToString()
                                            textboxProperties.Left = dtReportObject.Rows(0)("ObjectLeftMargin").ToString()
                                            textboxProperties.FontColor = Color.FromName(dtReportObject.Rows(0)("TextBoxFontColor").ToString())
                                            textboxProperties.TextBoxText = dtReportObject.Rows(0)("TextBoxText").ToString()
                                            textboxProperties.FontSize = dtReportObject.Rows(0)("TextBoxFontSize").ToString()
                                            textboxProperties.IsBold = dtReportObject.Rows(0)("TextBoxFontIsBold").ToString()
                                            textboxProperties.IsItalic = dtReportObject.Rows(0)("TextBoxFontIsItalic").ToString()
                                            textboxProperties.IsUnderline = dtReportObject.Rows(0)("TextBoxFontIsUnderline").ToString()
                                            textboxProperties.Width = dtReportObject.Rows(0)("ObjectWidth")
                                            textboxProperties.Height = dtReportObject.Rows(0)("ObjectHeight").ToString()
                                            textboxProperties.BorderSize = dtReportObject.Rows(0)("TextBoxBorderSize").ToString()
                                            textboxProperties.BoderColor = Color.FromName(dtReportObject.Rows(0)("TextBoxBoderColor").ToString())

                                            'CreateTextBoxObjectOnSlide(textboxProperties, dtReportObject.Rows(0)("ObjectName").ToString(), powerPointSlide)

                                        End If
                                    Catch ex As Exception
                                        'Console.WriteLine(ex.Message.ToString)
                                        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                                    End Try
                                Next
                            Else

                                objSlideProperties = New SlideProperties()
                                'objSlideProperties.SlideID = CInt(drObject("SlideID"))
                                objSlideProperties.SlideOrdinal = drObject("SlideOrdinal").ToString
                                objSlideProperties.SlideTitle = drObject("SlideTitle").ToString
                                objSlideProperties.SlideName = drObject("SlideName").ToString

                                Dim dashboardXmlFile As String = Nothing
                                Dim dashboardName As String = Nothing

                                Dim dtDashboard As DataTable = clsSQLCommands.GetDashboardFromID(connStrIOSServer, reportID, dtObjectsPerSlide.Rows(0)("DashboardID"))

                                Dim str = dtDashboard.Rows(0)("DashboardFile").ToString
                                dashboardName = dtDashboard.Rows(0)("DashboardName").ToString

                                If str.Trim.Contains("<?xml") Then
                                    dashboardXmlFile = str
                                Else
                                    dashboardXmlFile = GetDecryptedConnectionString(str)
                                End If

                                Dim ms As New System.IO.MemoryStream()
                                ms = StringToStream(dashboardXmlFile)

                                CreateDashboardSlide(ms, dshBrdImgFilePath, dashboardName, objSlideProperties, PresentationDoc)

                            End If
                        End If
                    Next
                End Using
            End If

        Catch ex As Exception
            reportCreated = False
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            MsgBox(ex.Message)
        Finally
            GC.Collect()
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub CreateDashboardSlide(dashboardStream As Stream, outputFolder As String, dashboardName As String, ByRef objSlideProp As SlideProperties, ByRef presDoc As PresentationDocument)
        If Not Directory.Exists(outputFolder) Then
            Directory.CreateDirectory(outputFolder)
        End If

        ' Load dashboard from stream
        dshbrd = New Dashboard()
        AddHandler dshbrd.ConfigureDataConnection, AddressOf dashboard_ConfigureDataConnection
        dshbrd.LoadFromXml(dashboardStream)

        ' Create exporter
        Dim exporter As New DashboardExporter()
        AddHandler exporter.ConnectionError, AddressOf Exporter_ConnectionError
        AddHandler exporter.DataLoadingError, AddressOf Exporter_DataLoadingError
        AddHandler exporter.DashboardItemDataLoadingError, AddressOf Exporter_DashboardItemDataLoadingError

        ' Locate all TabContainers
        Dim tabContainers = dshbrd.Items.OfType(Of TabContainerDashboardItem)().ToList()

        Dim imgOptions As DashboardImageExportOptions = New DashboardImageExportOptions With {
            .ExportFilters = True,
            .ExportParameters = True,
            .Format = DashboardExportImageFormat.Jpeg,
            .ShowTitle = DevExpress.Utils.DefaultBoolean.True,
            .Resolution = 100
        }

        If tabContainers.Count = 0 Then
            ' No tab container → export entire dashboard
            Console.WriteLine("Started Exporting Dashboard As Image: " & dashboardName)
            exporter.ExportToImage(dshbrd, outputFolder & "\" & dashboardName & ".jpg",,, imgOptions)

            Dim presPart As PresentationPart = presDoc.PresentationPart
            InsertDashboardNewSlide(presPart, slidePosition, objSlideProp.SlideTitle, outputFolder & "\" & dashboardName & ".jpg")
            slidePosition = slidePosition + 1
            File.Delete(outputFolder & "\" & dashboardName & ".jpg")
        End If

        ' Iterate through each TabContainer
        For Each tabContainer In tabContainers

            Dim lstImgFiles As New List(Of String)

            For iCntr As Integer = 0 To tabContainer.TabPages.Count - 1
                Dim dashTabPage As DashboardTabPage = tabContainer.TabPages(iCntr)
                Console.WriteLine("Started Exporting Dashboard TabPage As Image: " & dashTabPage.ComponentName)

                lstImgFiles.Add(outputFolder & "\" & dashboardName & iCntr & ".jpg")
                exporter.ExportDashboardItemToImage(dshbrd, dashTabPage.ComponentName, outputFolder & "\" & dashboardName & iCntr & ".jpg",,, imgOptions)
            Next

            'copy dashboard image parts files into a ppt slide
            For Each imgFile In lstImgFiles
                Dim presPart As PresentationPart = presDoc.PresentationPart
                InsertDashboardNewSlide(presPart, slidePosition, objSlideProp.SlideTitle, imgFile)
                slidePosition = slidePosition + 1
            Next

            'delete dashboard parts image files...
            For Each imgFile In lstImgFiles
                File.Delete(imgFile)
            Next
        Next
    End Sub

    Public Sub InsertDashboardNewSlide(ByVal presentationPart As PresentationPart, ByVal position As Integer, ByVal slideTitle As String, imgOutputPath As String)
        ' Declare and instantiate a new slide.
        slide = New DocumentFormat.OpenXml.Presentation.Slide(New CommonSlideData(New ShapeTree()))
        drawingObjectId = 1

        Dim nonVisualProperties As NonVisualGroupShapeProperties = slide.CommonSlideData.ShapeTree.AppendChild(New NonVisualGroupShapeProperties())
        nonVisualProperties.NonVisualDrawingProperties = New DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties() With {.Id = 1, .Name = ""}
        nonVisualProperties.NonVisualGroupShapeDrawingProperties = New NonVisualGroupShapeDrawingProperties()
        nonVisualProperties.ApplicationNonVisualDrawingProperties = New ApplicationNonVisualDrawingProperties()

        ' Specify the group shape properties of the new slide.
        slide.CommonSlideData.ShapeTree.AppendChild(New GroupShapeProperties())

        ' Declare and instantiate the title shape of the new slide.
        'Dim titleShape As DocumentFormat.OpenXml.Presentation.Shape = slide.CommonSlideData.ShapeTree.AppendChild(New DocumentFormat.OpenXml.Presentation.Shape())
        drawingObjectId = (drawingObjectId + 1)

        ' Specify the required shape properties for the title shape. 
        'titleShape.NonVisualShapeProperties = New DocumentFormat.OpenXml.Presentation.NonVisualShapeProperties(New DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties() With {.Id = drawingObjectId, .Name = "Title"},
        '   New DocumentFormat.OpenXml.Presentation.NonVisualShapeDrawingProperties 
        '  (New Drawing.ShapeLocks() With {.NoGrouping = True}),
        '    New ApplicationNonVisualDrawingProperties(New PlaceholderShape() With {.Type = PlaceholderValues.Title}))

        'titleShape.ShapeProperties = New DocumentFormat.OpenXml.Presentation.ShapeProperties()

        ' Specify the text of the title shape.
        'titleShape.TextBody = New DocumentFormat.OpenXml.Presentation.TextBody(New Drawing.BodyProperties, New Drawing.ListStyle, New Drawing.Paragraph(New Drawing.Run(New Drawing.Text() With {.Text = slideTitle})))

        ' Create the slide part for the new slide.
        slidePart = presentationPart.AddNewPart(Of SlidePart)()

        ' Save the new slide part.
        slide.Save(slidePart)

        ' Modify the slide ID list in the presentation part.
        ' The slide ID list should not be null.
        Dim slideIdList As SlideIdList = presentationPart.Presentation.SlideIdList

        ' Find the highest slide ID in the current list.
        Dim maxSlideId As UInt32Value = 1
        Dim prevSlideId As SlideId = Nothing

        For Each slideId As SlideId In slideIdList.ChildElements
            If slideId.Id > maxSlideId Then
                maxSlideId = slideId.Id
            End If

            position -= 1
            If position = 0 Then
                prevSlideId = slideId
            End If

        Next slideId

        maxSlideId = maxSlideId.Value + 1

        Dim smPart As SlideMasterPart = presentationPart.SlideMasterParts.First()
        Dim slPart As SlideLayoutPart = smPart.SlideLayoutParts.SingleOrDefault(Function(s) s.SlideLayout.CommonSlideData.Name.Value = "Title and Content")

        slidePart.AddPart(slPart)

        ' Insert the new slide into the slide list after the previous slide.
        Dim newSlideId As SlideId = slideIdList.InsertAfter(New SlideId(), prevSlideId)
        newSlideId.Id = maxSlideId
        newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart)

        ' Copy chart image to the slide
        CopyDashboardImageToSlide(slide, slidePart, drawingObjectId, imgOutputPath, presentationPart)

        presentationPart.Presentation.Save()
    End Sub

    Private Sub CopyDashboardImageToSlide(ByRef slide As Slide, ByRef slidePart As SlidePart, ByVal drawingObjectId As Integer, imgOutputPath As String, ByRef presPart As PresentationPart)
        Try
            ' Copying chart image to the slide
            Dim bodyPic As DocumentFormat.OpenXml.Presentation.Picture = slide.CommonSlideData.ShapeTree.AppendChild(New DocumentFormat.OpenXml.Presentation.Picture())
            drawingObjectId += 1

            ' Specify the required shape properties for the body shape.
            bodyPic.NonVisualPictureProperties = New NonVisualPictureProperties(
                    New DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties() With {.Id = drawingObjectId, .Name = Path.GetFileName(imgOutputPath)},
                    New NonVisualPictureDrawingProperties(New Drawing.PictureLocks With {.NoChangeAspect = True}),
                    New ApplicationNonVisualDrawingProperties(New PlaceholderShape() With {.Index = 1}))

            Dim part = slidePart.AddImagePart(ImagePartType.Jpeg)

            Using jpgStream As New FileStream(imgOutputPath, FileMode.Open, FileAccess.Read)
                jpgStream.Position = 0
                part.FeedData(jpgStream)
            End Using

            Dim blipFill = New DocumentFormat.OpenXml.Presentation.BlipFill()
            Dim blip1 = New DocumentFormat.OpenXml.Drawing.Blip() With {.Embed = slidePart.GetIdOfPart(part)}
            Dim blipExtensionList1 = New DocumentFormat.OpenXml.Drawing.BlipExtensionList()
            Dim blipExtension1 = New DocumentFormat.OpenXml.Drawing.BlipExtension() With {.Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"}
            Dim useLocalDpi1 = New DocumentFormat.OpenXml.Office2010.Drawing.UseLocalDpi() With {.Val = False}
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main")
            blipExtension1.Append(useLocalDpi1)
            blipExtensionList1.Append(blipExtension1)
            blip1.Append(blipExtensionList1)
            Dim stretch = New DocumentFormat.OpenXml.Drawing.Stretch()
            stretch.Append(New DocumentFormat.OpenXml.Drawing.FillRectangle())
            blipFill.Append(blip1)
            blipFill.Append(stretch)

            bodyPic.Append(blipFill)

            bodyPic.ShapeProperties = New DocumentFormat.OpenXml.Presentation.ShapeProperties()
            bodyPic.ShapeProperties.Transform2D = New DocumentFormat.OpenXml.Drawing.Transform2D()

            Dim emuPerMm As Integer = 12700

            ' Image size (EMUs)
            Dim imageWidth As Long = CLng(1800 * 0.6 * 0.75 * emuPerMm)
            Dim imageHeight As Long = CLng(900 * 0.6 * 0.75 * emuPerMm)

            Dim slideSize = presPart.Presentation.SlideSize
            Dim slideWidth As Long = slideSize.Cx
            Dim slideHeight As Long = slideSize.Cy

            ' Center position
            Dim offsetX As Long = (slideWidth - imageWidth) \ 2
            Dim offsetY As Long = (slideHeight - imageHeight) \ 2

            bodyPic.ShapeProperties.Transform2D.Append(New Drawing.Offset With {
                .X = offsetX,
                .Y = offsetY
            })

            bodyPic.ShapeProperties.Transform2D.Append(New Drawing.Extents With {
                .Cx = imageWidth,
                .Cy = imageHeight
            })

            ' Save the new slide part.
            slide.Save(slidePart)

            chartsPerSlide = chartsPerSlide - 1
            ' Copy next chart image on new slide
            If chartsPerSlide = 0 Then
                slide = Nothing
                slidePart = Nothing
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub dashboard_ConfigureDataConnection(sender As Object, e As DashboardConfigureDataConnectionEventArgs)
        Try
            If dshbrd.DataSources.Count > 1 Then
                'multi data sources of diff kind
                For Each ds As DashboardSqlDataSource In dshbrd.DataSources
                    ds.ConnectionOptions.CommandTimeout = 300
                    If ds.Connection.ProviderKey.ToUpper = "POSTGRES" Then

                        Dim params As PostgreSqlConnectionParameters = TryCast(e.ConnectionParameters, PostgreSqlConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(3000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.PortNumber = connString.Split(";")(1).Split("=")(1)
                            params.UserName = connString.Split(";")(2).Split("=")(1)
                            params.Password = connString.Split(";")(3).Split("=")(1)
                            params.DatabaseName = connString.Split(";")(4).Split("=")(1)
                        End If
                    ElseIf ds.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then

                        Dim params As MsSqlConnectionParameters = TryCast(e.ConnectionParameters, MsSqlConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(2000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.DatabaseName = connString.Split(";")(1).Split("=")(1)
                            params.AuthorizationType = MsSqlAuthorizationType.SqlServer
                            params.UserName = connString.Split(";")(2).Split("=")(1)
                            params.Password = connString.Split(";")(3).Split("=")(1)
                        End If
                    ElseIf ds.Connection.ProviderKey.ToUpper = "ORACLESERVER" Then

                        Dim params As OracleConnectionParameters = TryCast(e.ConnectionParameters, OracleConnectionParameters)
                        If params IsNot Nothing Then
                            Dim connArr() As String = GetIOSConnection(4000)
                            Dim connString As String = GetDecryptedConnectionString(connArr(1))
                            params.ServerName = connString.Split(";")(0).Split("=")(1)
                            params.UserName = connString.Split(";")(1).Split("=")(1)
                            params.Password = connString.Split(";")(2).Split("=")(1)
                            params.ProviderType = OracleProviderType.ODPManaged
                        End If
                    End If
                Next
            Else
                'single data source of a kind
                Dim ds As DashboardSqlDataSource = dshbrd.DataSources(0)
                If ds.Connection.ProviderKey.ToUpper = "MSSQLSERVER" Then
                    e.ConnectionParameters = CreateConnectionParameters()
                ElseIf ds.Connection.ProviderKey.ToUpper = "POSTGRES" Then
                    e.ConnectionParameters = CreateConnectionParametersPostGreSql()
                ElseIf ds.Connection.ProviderKey.ToUpper = "ORACLESERVER" Then
                    e.ConnectionParameters = CreateConnectionParametersOracle()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Function CreateConnectionParameters() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(2000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New MsSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .DatabaseName = connString.Split(";")(1).Split("=")(1),
            .AuthorizationType = MsSqlAuthorizationType.SqlServer,
            .UserName = connString.Split(";")(2).Split("=")(1),
            .Password = connString.Split(";")(3).Split("=")(1)
        }
    End Function

    Private Function CreateConnectionParametersPostGreSql() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(3000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New PostgreSqlConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .PortNumber = connString.Split(";")(1).Split("=")(1),
            .UserName = connString.Split(";")(2).Split("=")(1),
            .Password = connString.Split(";")(3).Split("=")(1),
            .DatabaseName = connString.Split(";")(4).Split("=")(1)
        }
    End Function

    Private Function CreateConnectionParametersOracle() As DataConnectionParametersBase
        Dim connArr() As String = GetIOSConnection(3000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))
        Return New OracleConnectionParameters() With {
            .ServerName = connString.Split(";")(0).Split("=")(1),
            .UserName = connString.Split(";")(1).Split("=")(1),
            .Password = connString.Split(";")(2).Split("=")(1),
            .ProviderType = OracleProviderType.ODPManaged
        }
    End Function

    Private Sub Exporter_ConnectionError(ByVal sender As Object, ByVal e As DashboardExporterConnectionErrorEventArgs)
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", e.Exception.Message)
    End Sub

    Private Sub Exporter_DataLoadingError(ByVal sender As Object, ByVal e As DataLoadingErrorEventArgs)
        For Each [error] As DataLoadingError In e.Errors
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", [error].Error.ToString)
        Next [error]
    End Sub

    Private Sub Exporter_DashboardItemDataLoadingError(ByVal sender As Object, ByVal e As DashboardItemDataLoadingErrorEventArgs)
        For Each [error] As DashboardItemDataLoadingError In e.Errors
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", [error].Error.ToString)
        Next [error]
    End Sub

    Private Sub ReplaceTextInSlide(ByRef presDoc As PresentationDocument, ByVal reportName As String, ByVal subTitle As String)
        ' Editing top slide for title and subtitle
        Dim presPart As PresentationPart = presDoc.PresentationPart
        Dim presentation = presPart.Presentation

        If presentation.SlideIdList IsNot Nothing Then

            For Each slideId As SlideId In presentation.SlideIdList.ChildElements
                Dim slidePartRelationshipId As String = slideId.RelationshipId
                Dim slidePart As SlidePart = CType(presPart.GetPartById(slidePartRelationshipId), SlidePart)

                For Each paragraph In slidePart.Slide.Descendants(Of DocumentFormat.OpenXml.Drawing.Paragraph)()

                    Dim oldText As String = paragraph.InnerText

                    If oldText = "<Title>" Or oldText = "Title" Then
                        ' oldText = oldText.Replace("<", "").Replace(">", "")
                        ' Replacing title text with report name in the top slide
                        If paragraph.InnerText.Contains(oldText) Then
                            Dim newString As String = paragraph.InnerText
                            newString = newString.Replace(paragraph.InnerText, reportName).Replace("<", "").Replace(">", "")

                            For Each child In paragraph.ChildElements
                                If child.InnerText.Equals(oldText) Then
                                    Dim styleRun As DocumentFormat.OpenXml.Drawing.RunProperties = CType(child.ChildElements(0).Clone(), DocumentFormat.OpenXml.Drawing.RunProperties)
                                    child.RemoveAllChildren()
                                    child.Append(styleRun)
                                    child.Append(New DocumentFormat.OpenXml.Drawing.Text(newString))
                                End If
                            Next
                        End If
                    ElseIf oldText = "<Subtitle>" Then
                        ' Replacing subtitle text with username and date in the top slide
                        If paragraph.InnerText.Contains(oldText) Then
                            Dim newString As String = paragraph.InnerText
                            newString = newString.Replace(paragraph.InnerText, subTitle).Replace("<", "").Replace(">", "")

                            For Each child In paragraph.ChildElements
                                If child.InnerText.Equals(oldText) Then
                                    Dim styleRun As DocumentFormat.OpenXml.Drawing.RunProperties = CType(child.ChildElements(0).Clone(), DocumentFormat.OpenXml.Drawing.RunProperties)
                                    child.RemoveAllChildren()
                                    child.Append(styleRun)
                                    child.Append(New DocumentFormat.OpenXml.Drawing.Text(subTitle))
                                End If
                            Next
                        End If

                    End If
                Next

                slidePart.Slide.Save()
            Next
        End If
    End Sub

    Public Sub CreatePresentationWithTemplate(ByVal rptFilePath As String, ByVal templateFileName As String)
        ' Embedding template.potx file along with presentation doc
        Dim byteArray As Byte() = System.IO.File.ReadAllBytes(Application.StartupPath & "\" & templateFileName)
        Using stream As New System.IO.MemoryStream()
            stream.Write(byteArray, 0, CInt(byteArray.Length))
            Using pDoc = PresentationDocument.Open(stream, True)
                pDoc.ChangeDocumentType(PresentationDocumentType.Presentation)
            End Using
            System.IO.File.WriteAllBytes(rptFilePath, stream.ToArray())
        End Using
    End Sub

    Private Sub GetObjectStyleProperties(ByVal objectType As String)
        Dim dtObjectStyle As DataTable = clsSQLCommands.GetReportsObjectStyles(connStrIOSServer, objectType)
        If dtObjectStyle IsNot Nothing Then
            dtChartStyleProperties = DataAccessorODBC.GetDataTable(connStrIOSServer, clsSQLCommands.GetChartStylePropetiesQuery(perSlideObjectID, True))
        End If
    End Sub

    Private Sub GetPredefinedPeriod()
        If (dtChartStyleProperties.Rows(0)("PredefinedTime").ToString.ToLower <> "select") Then
            dtPredefPeriod = clsSQLCommands.GetPredefinedPeriodForChart(connStrIOSServer, dtChartStyleProperties.Rows(0)("PredefinedTime").ToString)
        Else
            If Not dtPredefPeriod Is Nothing Then
                dtPredefPeriod.Rows.Clear()
            End If
        End If
    End Sub

    Public Sub InsertNewSlide(ByVal presentationPart As PresentationPart, ByVal position As Integer, ByVal slideTitle As String, ByRef objChartProperties As ObjectChartProperties, ByRef ch As Chart)
        Try
            ' Declare and instantiate a new slide.
            slide = New DocumentFormat.OpenXml.Presentation.Slide(New CommonSlideData(New ShapeTree()))
            drawingObjectId = 1

            Dim nonVisualProperties As NonVisualGroupShapeProperties = slide.CommonSlideData.ShapeTree.AppendChild(New NonVisualGroupShapeProperties())
            nonVisualProperties.NonVisualDrawingProperties = New DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties() With {.Id = 1, .Name = ""}
            nonVisualProperties.NonVisualGroupShapeDrawingProperties = New NonVisualGroupShapeDrawingProperties()
            nonVisualProperties.ApplicationNonVisualDrawingProperties = New ApplicationNonVisualDrawingProperties()

            ' Specify the group shape properties of the new slide.
            slide.CommonSlideData.ShapeTree.AppendChild(New GroupShapeProperties())

            ' Declare and instantiate the title shape of the new slide.
            Dim titleShape As DocumentFormat.OpenXml.Presentation.Shape = slide.CommonSlideData.ShapeTree.AppendChild(New DocumentFormat.OpenXml.Presentation.Shape())
            drawingObjectId = (drawingObjectId + 1)

            ' Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = New DocumentFormat.OpenXml.Presentation.NonVisualShapeProperties(New _
                DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties() With {.Id = drawingObjectId, .Name = "Title"},
                New DocumentFormat.OpenXml.Presentation.NonVisualShapeDrawingProperties _
                (New Drawing.ShapeLocks() With {.NoGrouping = True}),
                New ApplicationNonVisualDrawingProperties(New PlaceholderShape() With {.Type = PlaceholderValues.Title}))

            titleShape.ShapeProperties = New DocumentFormat.OpenXml.Presentation.ShapeProperties()

            ' Specify the text of the title shape.
            titleShape.TextBody = New DocumentFormat.OpenXml.Presentation.TextBody(New Drawing.BodyProperties, New Drawing.ListStyle, New Drawing.Paragraph(New Drawing.Run(New Drawing.Text() With {.Text = slideTitle})))

            ' Create the slide part for the new slide.
            slidePart = presentationPart.AddNewPart(Of SlidePart)()

            ' Save the new slide part.
            slide.Save(slidePart)

            ' Modify the slide ID list in the presentation part.
            ' The slide ID list should not be null.
            Dim slideIdList As SlideIdList = presentationPart.Presentation.SlideIdList

            ' Find the highest slide ID in the current list.
            Dim maxSlideId As UInt32Value = 1
            Dim prevSlideId As SlideId = Nothing

            For Each slideId As SlideId In slideIdList.ChildElements
                If slideId.Id > maxSlideId Then
                    maxSlideId = slideId.Id
                End If

                position -= 1
                If position = 0 Then
                    prevSlideId = slideId
                End If

            Next slideId

            maxSlideId = maxSlideId.Value + 1

            Dim smPart As SlideMasterPart = presentationPart.SlideMasterParts.First()
            Dim slPart As SlideLayoutPart = smPart.SlideLayoutParts.SingleOrDefault(Function(s) s.SlideLayout.CommonSlideData.Name.Value = "Title and Content")

            slidePart.AddPart(slPart)

            ' Insert the new slide into the slide list after the previous slide.
            Dim newSlideId As SlideId = slideIdList.InsertAfter(New SlideId(), prevSlideId)
            newSlideId.Id = maxSlideId
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart)

            ' Copy chart image to the slide
            CopyChartBitmapToSlide(slide, slidePart, drawingObjectId, ch)

            presentationPart.Presentation.Save()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub CopyChartBitmapToSlide(ByRef slide As DocumentFormat.OpenXml.Presentation.Slide, ByRef slidePart As SlidePart, ByVal drawingObjectId As Integer, ByRef ch As Chart)
        Try

            ' Copying chart image to the slide
            Dim bodyPic As DocumentFormat.OpenXml.Presentation.Picture = slide.CommonSlideData.ShapeTree.AppendChild(New DocumentFormat.OpenXml.Presentation.Picture())
            drawingObjectId += 1

            ' Specify the required shape properties for the body shape.
            bodyPic.NonVisualPictureProperties = New NonVisualPictureProperties(
                    New DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties() With {.Id = drawingObjectId, .Name = "Content Placeholder"},
                    New NonVisualPictureDrawingProperties(New Drawing.PictureLocks With {.NoChangeAspect = True}),
                    New ApplicationNonVisualDrawingProperties(New PlaceholderShape() With {.Index = 1}))

            Dim bmp As Bitmap = ch.GetChartBitmap
            ch.ImageFormat = ImageFormat.Bmp

            Dim part = slidePart.AddImagePart(ImagePartType.Bmp)

            Using bmpStream As New System.IO.MemoryStream()
                ch.GetChartBitmap.Save(bmpStream, Imaging.ImageFormat.Bmp)
                bmpStream.Position = 0
                part.FeedData(bmpStream)
            End Using

            Dim blipFill = New DocumentFormat.OpenXml.Presentation.BlipFill()
            Dim blip1 = New DocumentFormat.OpenXml.Drawing.Blip() With {.Embed = slidePart.GetIdOfPart(part)}
            Dim blipExtensionList1 = New DocumentFormat.OpenXml.Drawing.BlipExtensionList()
            Dim blipExtension1 = New DocumentFormat.OpenXml.Drawing.BlipExtension() With {.Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"}
            Dim useLocalDpi1 = New DocumentFormat.OpenXml.Office2010.Drawing.UseLocalDpi() With {.Val = False}
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main")
            blipExtension1.Append(useLocalDpi1)
            blipExtensionList1.Append(blipExtension1)
            blip1.Append(blipExtensionList1)
            Dim stretch = New DocumentFormat.OpenXml.Drawing.Stretch()
            stretch.Append(New DocumentFormat.OpenXml.Drawing.FillRectangle())
            blipFill.Append(blip1)
            blipFill.Append(stretch)

            bodyPic.Append(blipFill)

            bodyPic.ShapeProperties = New DocumentFormat.OpenXml.Presentation.ShapeProperties()
            bodyPic.ShapeProperties.Transform2D = New DocumentFormat.OpenXml.Drawing.Transform2D()
            bodyPic.ShapeProperties.Transform2D.Append(New DocumentFormat.OpenXml.Drawing.Offset With {
                .X = CInt(objChartProperties.Left) * 12700,
                .Y = CInt(objChartProperties.Top) * 12700
            })

            bodyPic.ShapeProperties.Transform2D.Append(New DocumentFormat.OpenXml.Drawing.Extents With {
                .Cx = CInt(CDbl(objChartProperties.Width) * CDbl(objChartProperties.ObjectScale) * 0.75 * CDbl(12700)),
                .Cy = CInt(CDbl(objChartProperties.Height) * CDbl(objChartProperties.ObjectScale) * 0.75 * CDbl(12700))
            })

            ' Save the new slide part.
            slide.Save(slidePart)

            chartsPerSlide = chartsPerSlide - 1
            ' Copy next chart image on new slide
            If chartsPerSlide = 0 Then
                slide = Nothing
                slidePart = Nothing
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Public Function GetData(sql) As DataSet
        Try
            Dim ds As New System.Data.DataSet
            Using cnQODBC As New System.Data.Odbc.OdbcConnection(connStrIOSServer)
                cnQODBC.ConnectionTimeout = 300
                cnQODBC.Open()
                Using daQODBC As New System.Data.Odbc.OdbcDataAdapter(sql, cnQODBC)
                    daQODBC.SelectCommand.CommandTimeout = 3600
                    daQODBC.Fill(ds)
                End Using
            End Using
            Return ds
        Catch ex As Exception
            'Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)
        End Try
        Return Nothing
    End Function

#Region "TopX Chart"

    Private Sub CreateTopXChart(ByRef objChartProp As ObjectChartProperties, objectName As String, ByRef objSlideProp As SlideProperties, ByRef presDoc As DocumentFormat.OpenXml.Packaging.PresentationDocument)
        Dim nc As New Chart
        nc.Name = objectName
        nc.Dock = DockStyle.Fill
        nc.ToolTip.InitialDelay = 1
        nc.AutoSize = False
        'Chart Default Properties
        nc.DefaultElement.Marker.Visible = False
        nc.LegendBox.Orientation = Orientation.Bottom
        nc.LegendBox.DefaultEntry.Value = ""
        nc.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        nc.LegendBox.DefaultCorner = BoxCorner.Round

        nc.XAxis.TickLabelMode = TickLabelMode.Angled
        nc.XAxis.TickLabelAngle = 45

        nc.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        nc.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        nc.CleanupPeriod = 1

        nc.TitleBox.Position = TitleBoxPosition.Full
        nc.TitleBox.CornerTopLeft = BoxCorner.Round
        nc.TitleBox.CornerTopRight = BoxCorner.Round
        nc.TitleBox.Label.AutoWrap = True
        nc.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"

        If Me.rptCurrOrConfig = "Current" Then
            Console.WriteLine("Getting chart from technology: " & objectName)
            nc = GetChartFromTechnology(objectName, _strNetwork, nc)
        ElseIf Me.rptCurrOrConfig = "Config" Then
            Console.WriteLine("Getting data for chart: " & objectName)
            ProcessTopXChart(nc, objChartProp, objectName)
        End If

        'copy chart image to ppt slide
        Console.WriteLine("Copying image for chart: " & objectName)

        If slide IsNot Nothing AndAlso slidePart IsNot Nothing Then
            CopyChartBitmapToSlide(slide, slidePart, drawingObjectId, nc)
        Else
            Dim presPart As PresentationPart = presDoc.PresentationPart
            InsertNewSlide(presPart, slidePosition, objSlideProp.SlideTitle, objChartProp, nc)
            slidePosition = slidePosition + 1
        End If
        'clsSQLCommands.WriteReportLog(connStr, reportID, "Generated Slide With Title: " & objSlideProp.SlideName)
    End Sub

    Private Sub ProcessTopXChart(ByRef ch As Chart, ByRef objChartProp As ObjectChartProperties, chartName As String)
        Dim chartSql As String = Nothing
        Try
            chartSql = SQL_Construct_TopX(objChartProp.TargetType, chartName)
        Catch
        End Try

        dsTopx = New DataSet
        dsTopx = GetData(chartSql)

        'Console.WriteLine("Assigning data to chart: " & chartName)
        If objChartProp.TopXDeltaInterval.ToString <> "" Then
            AssignDataToTopX_Delta(ch, objChartProp, dsTopx, _strNetwork)
        Else
            AssignDataToTopX(ch, objChartProp, dsTopx.Tables(0), _strNetwork)
        End If
    End Sub

    Private Function SQL_Construct_TopX(ByVal aggr_from As String, ByVal chartName As String) As String
        Dim tech As String = _strNetwork
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

        Dim startdate As Date = Nothing
        Dim enddate As Date = Nothing
        Dim startdate_string As String = Nothing
        Dim enddate_string As String = Nothing
        Dim DeltaDate1 As Date = Nothing
        Dim DeltaDate2 As Date = Nothing
        Dim connectionString As String = ""

        If dtPredefPeriod.Rows.Count > 0 Then
            startdate = CDate(dtPredefPeriod.Rows(0)("start_datetime"))
            enddate = CDate(dtPredefPeriod.Rows(0)("end_datetime"))
            DeltaDate1 = CDate(dtPredefPeriod.Rows(0)("start_datetime"))
            DeltaDate2 = CDate(dtPredefPeriod.Rows(0)("end_datetime"))
        Else
            startdate = CDate(dtChartStyleProperties.Rows(0)("ManualStartTime"))
            enddate = CDate(dtChartStyleProperties.Rows(0)("ManualEndTime"))
            DeltaDate1 = CDate(dtChartStyleProperties.Rows(0)("ManualStartTime"))
            DeltaDate2 = CDate(dtChartStyleProperties.Rows(0)("ManualEndTime"))
        End If

        startdate_string = Chr(39) & startdate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
        enddate_string = Chr(39) & enddate.ToString("yyyy-MM-dd HH:mm") & Chr(39)

        Dim conn_el As Odbc.OdbcConnection = Nothing
        Dim comm_sql As Odbc.OdbcCommand = Nothing
        Dim dr_sql As Odbc.OdbcDataReader = Nothing
        'Dim sql_sql As String

        Dim comm_Element As Odbc.OdbcCommand = Nothing
        Dim dr_element As Odbc.OdbcDataReader = Nothing
        Dim sqlelement As String = Nothing

        Try
            'Open connection to server
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()

            'objecttree selection to string
            aggr_from = dtChartStyleProperties.Rows(0)("TopX_ShowObjects").ToString
            Dim aggr_to As String = dtChartStyleProperties.Rows(0)("AggregateTo").ToString
            Dim objectsel As String = "IN (" & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString & ")"

            If objectsel = "IN ()" Then
                conn_el.Close()
                conn_el.Dispose()
                Return Nothing
            End If

            Dim CMFilter As String = Nothing
            Dim tagid As String = Nothing
            Dim RegionFilter As String = objectsel
            If aggr_to = "TAGS" Then
                tagid = dtChartStyleProperties.Rows(0)("TagID").ToString
                aggr_to = dtChartStyleProperties.Rows(0)("AggregateTo").ToString
                If aggr_to.Contains("CM") Then
                    CMFilter = dtChartStyleProperties.Rows(0)("Tags_Filter").ToString
                End If
                If aggr_to.Contains("Region") Then
                    RegionFilter = dtChartStyleProperties.Rows(0)("RegionFilter").ToString
                End If
            End If

            ' TG change
            'If rdoHourlyTopX.Checked = True Then
            'aggr_to = aggr_to & "_Hourly"
            'End If
            'set purpose
            Dim purpose As String = "TopX"
            'get sql
            comm_sql = New Odbc.OdbcCommand(clsSQLCommands.GetConstructStatsSQL(tech.Replace("TopX_", ""), purpose, aggr_to, aggr_from), conn_el)
            dr_sql = comm_sql.ExecuteReader
            sql_from_time = ""

            dr_sql.Read()
            If Not dr_sql.HasRows = 0 Then
                sql_select = dr_sql("sql_select").ToString.Trim
                Dim deltaInterval As String = IIf(IsNumeric(dtChartStyleProperties.Rows(0)("TopX_DeltaInterval")), dtChartStyleProperties.Rows(0)("TopX_DeltaInterval"), "0")
                If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                    sql_from_time = " " & dr_sql("sql_time_hour").ToString.Trim
                    connectionString = dr_sql("sql_time_hour_connStr").ToString.Trim
                    DeltaDate1 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), startdate)
                    DeltaDate2 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), enddate)

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                    sql_from_time = " " & dr_sql("sql_time_day").ToString.Trim
                    connectionString = dr_sql("sql_time_day_connStr").ToString.Trim
                    DeltaDate1 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), startdate)
                    DeltaDate2 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), enddate)

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH" Then
                    sql_from_time = " " & dr_sql("sql_time_bh").ToString.Trim
                    connectionString = dr_sql("sql_time_bh_connStr").ToString.Trim
                    DeltaDate1 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), startdate)
                    DeltaDate2 = DateAdd(DateInterval.Day, -1 * CInt(deltaInterval), enddate)

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly" Then
                    sql_from_time = " " & dr_sql("sql_time_week").ToString.Trim
                    connectionString = dr_sql("sql_time_week_connStr").ToString.Trim
                    DeltaDate1 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), startdate)
                    DeltaDate2 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), enddate)

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH" Then
                    sql_from_time = " " & dr_sql("sql_time_weekbh").ToString.Trim
                    connectionString = dr_sql("sql_time_weekbh_connStr").ToString.Trim
                    DeltaDate1 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), startdate)
                    DeltaDate2 = DateAdd(DateInterval.WeekOfYear, -1 * CInt(deltaInterval), enddate)

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                    sql_from_time = " " & dr_sql("sql_time_raw").ToString.Trim
                    connectionString = dr_sql("sql_time_raw_connStr").ToString.Trim
                    DeltaDate1 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), startdate)
                    DeltaDate2 = DateAdd(DateInterval.Hour, -1 * CInt(deltaInterval), enddate)

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                    sql_from_time = " " & dr_sql("sql_time_month").ToString.Trim
                    connectionString = dr_sql("sql_time_month_connStr").ToString.Trim
                    DeltaDate1 = DateAdd(DateInterval.Month, -1 * CInt(deltaInterval), startdate)
                    DeltaDate2 = DateAdd(DateInterval.Month, -1 * CInt(deltaInterval), enddate)

                End If

                sql_where_misc = " " & dr_sql("sql_where_misc").ToString.Trim
                If dtChartStyleProperties.Rows(0)("TargetType").ToString = "PLMN" Then
                    'to complete
                Else
                    sql_where_object = " " & Replace(dr_sql("sql_where_object"), "@object", objectsel).ToString.Trim
                End If
                sql_where_tables = " " & dr_sql("sql_where_tables").ToString.Trim

                'If chkEnableComparisonTopX.Checked = False Then
                sql_where_period = " " & Replace(Replace(dr_sql("sql_where_period"), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim
                Dim talias() As String = sql_where_period.Split(".")
                Dim table_alias As String = Replace(talias(0).Replace("AND", "").Trim(), "WHERE ", "")
                'sql_where_period = sql_where_period & filterPeriodstring.Replace("@alias", table_alias)
                'Else
                'sql_where_period = dr_sql("sql_where_period").ToString.Trim
                'End If

                sql_groupby = " " & dr_sql("sql_groupby").ToString.Trim
                sql_orderby = " " & dr_sql("sql_orderby").ToString.Trim
            Else
                dr_sql.Close()
                dr_sql = Nothing
                comm_sql.Dispose()
                comm_sql = Nothing
                conn_el.Close()
                conn_el.Dispose()
                conn_el = Nothing
                Return Nothing
            End If
            dr_sql.Close()
            dr_sql = Nothing
            comm_sql.Dispose()
            comm_sql = Nothing
            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            Dim supportcode As Integer = 0
            If aggr_from.Contains("RNC") Then
                supportcode = 1
            End If

            'KPI Tree Selection -> get string of selection
            'Dim selected_tabs As String = tvKPITopX.GetKPIChecked2String(1, "ObjectName")
            'Dim selected_charts As String = chartname
            'Dim selected_kpi As String = tvKPITopX.GetKPIChecked2String(3, "ObjectName")

            'get KPI sql
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()
            sqlelement = clsSQLCommands.GetChartKPISQL(tech.Replace("TopX_", ""), supportcode, chartName)
            comm_Element = New Odbc.OdbcCommand(sqlelement, conn_el)
            dr_element = comm_Element.ExecuteReader
            sql_kpi = ""

            While dr_element.Read
                sql_kpi = sql_kpi + " " + dr_element.GetValue(0).trim + ", "
            End While
            sql_kpi = sql_kpi.TrimEnd(" ")
            sql_kpi = sql_kpi.TrimEnd(",")

            conn_el.Close()
            comm_Element.Dispose()
            comm_Element = Nothing
            dr_element.Close()
            dr_element = Nothing
            conn_el.Dispose()
            conn_el = Nothing

            sql_total = sql_select + sql_kpi + " " + sql_from_time + sql_where_misc + sql_where_object + sql_where_tables + sql_where_period + sql_groupby + sql_orderby
            sql_total = Replace(sql_total, "@CMFilter", CMFilter)
            sql_total = Replace(sql_total, "@RegionFilter", RegionFilter)
            sql_total = Replace(sql_total, "= @TagID", tagid)
            sql_total = Replace(sql_total, "@TagID", tagid)
            sql_total = Replace(sql_total, "@RegionFilter", RegionFilter)

            'If chkEnableComparisonTopX.Checked = True Then
            '    Dim delta1date_string As String = Chr(39) & DeltaDate1.ToString("yyyy-MM-dd HH:mm") & Chr(39)
            '    Dim delta2date_string As String = Chr(39) & DeltaDate2.ToString("yyyy-MM-dd HH:mm") & Chr(39)
            '    sql_total = Replace(Replace(sql_total, "@starttime", delta1date_string), "@endtime", delta2date_string) + ";" + Replace(Replace(sql_total, "@starttime", startdate_string), "@endtime", enddate_string)
            'End If

            Return sql_total
        Catch ex As Exception
            'Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)

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

    Private Sub AssignDataToTopX_Delta(ByRef ch As Chart, objChartProp As ObjectChartProperties, ds As DataSet, tech As String, Optional ByVal chartname_original As String = Nothing, Optional ByVal ChartElementSortOrder() As String = Nothing)
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""

        'Dim selected_tabs As String = tvKPITopX.GetKPIChecked2String(1, "ObjectName")
        'Dim selected_charts As String = tvKPITopX.GetKPIChecked2String(2, "ObjectName")
        'Dim selected_kpis As String = tvKPITopX.GetKPIChecked2String(3, "ObjectName")
        Dim username As String = Chr(39) & Environment.UserName.ToString & Chr(39)

        sqlchart = clsSQLCommands.GetTopXChartConfigurationSQL(tech, chartSetName, ch.Name, username, chartname_original)
        Dim ds_chart As DataSet = DataAccessorODBC.GetDataSet(connStrIOSServer, sqlchart)
        Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to all charts
        '*************************
        'Dim ch As Chart
        Dim i As Integer
        Dim Y1axislabel, Y2axislabel As String
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis
        Dim yaxis2 As Axis
        Dim sp As New SmartPalette()
        Dim sc As New SeriesCollection
        Dim color_R, color_B, color_G As Integer
        'Dim tblayout As TableLayoutPanel
        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim chart_elsort() As String = {"0", "0"}
        Dim chart_elvis() As String = {"0"}
        Dim chart_elLineSize() As Integer = {0}
        Dim chart_elShowdatapoints() As Boolean = {False}
        Dim chart_elSeriesVisible() As Boolean = {True}
        Dim chart_elAutoScale() As Boolean = {True}

        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim xval As String = ""

        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1
        Dim dt As DataTable = ds.Tables("Delta")
        Dim x As Integer = 0
        Dim obj_columns() As String = Nothing
        For Each dc As DataColumn In dt.PrimaryKey
            ReDim Preserve obj_columns(x)
            obj_columns(x) = dc.ColumnName
            x = x + 1
        Next

        'Dim KeysToRemove As New List(Of String)
        'If chartname_original Is Nothing Then
        '    For Each key As String In dict_TopXDelta_SeriesCollection.Keys
        '        If key.Split("|")(0).ToUpper = tech.ToUpper Then
        '            KeysToRemove.Add(key)
        '        End If
        '    Next
        'End If
        'For Each keytoRemove In KeysToRemove
        '    dict_TopXDelta_SeriesCollection.Remove(keytoRemove)
        'Next

        For rownum = 0 To dt_chart.Rows.Count - 1
            Try
                'collecting elements from chart configuration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                'TESTING
                Dim tabindex_new As Integer = CInt(drow(2).ToString)
                chartindex = CInt(drow(4).ToString)
                If tabindex_old <> tabindex_new Then
                    tabindex_old = tabindex_new
                End If

                'configures individual chart when new chart line is detected
                If lastchart = "" Or lastchart <> drow(5).ToString Then
                    lastchart = drow(5).ToString.Trim
                    sp.Clear()

                    Y1axisAbsorPerc = drow(13).trim
                    Y2axisAbsOrPerc = nZ(drow(14), "Abs")

                    Y1axisPrecision = CInt(drow(15))
                    Y2axisPrecision = CInt(nZ(drow(16), "0"))
                    Y1axislabel = nZ(drow(11), " ")
                    Y2axislabel = nZ(drow(12), " ")

                    'If CInt(drow(2).ToString) = 99 And tech.ToUpper = "TOPX_" & _strNetwork.ToUpper Then
                    '    tabindex_new = customTabIndexTopX
                    'End If

                    'Select Case tech.ToLower
                    '    Case "topx_" & _strNetwork.ToLower
                    '        tblayout = tcTabControlHighTopX.TabPages(tabindex_new).Controls(0) 'drow(2)
                    '        ch = tblayout.GetControlFromPosition(0, chartindex) 'drow(4)
                    '        xval = flpSourceBtn_GetChecked("topx_" & _strNetwork.ToLower, flpCounterTypeTopX)(0).SourceButtonText
                    '    Case Else
                    '        MsgBox("AssignData2Charts: problem in tech selection")
                    '        Exit Sub
                    'End Select

                    'If chartname_original Is Nothing Then
                    '    ch.Annotations.Clear()
                    '    ch.Annotations.Add(New Annotation(tech))
                    '    ch.Annotations(0).Position = New System.Drawing.Point(ch.Width - 70, 2)
                    '    ch.Annotations(0).DefaultCorner = BoxCorner.Square
                    '    ch.Annotations(0).Size = New Size(60, 25)
                    '    Dim fnt As Font = New Font("Arial", 6, FontStyle.Regular)
                    '    ch.Annotations(0).Label.Font = fnt
                    'End If

                    'ch.Name = drow("ChartName").ToString
                    'ch.TitleBox.Label.Text = drow(6).Trim
                    'ch.DefaultElement.Hotspot.ToolTip = "%SeriesName = %Value" & " "

                    ''Y-Axis Settings   
                    'yaxis1 = New Axis
                    'yaxis1.Orientation = Orientation.Left
                    'yaxis1.Label.Text = Y1axislabel

                    'yaxis2 = New Axis
                    'yaxis2.Orientation = Orientation.Right

                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    'element based
                    Do
                        If ColumnInDataTable(drow(7).trim, dt) Then
                            ReDim Preserve chart_elements(j)
                            ReDim Preserve chart_elementsYAxis(j)
                            ReDim Preserve chart_Eltype(j)
                            ReDim Preserve chart_ElColor(j)
                            ReDim Preserve chart_elvis(j)
                            ReDim Preserve chart_elLineSize(j)
                            ReDim Preserve chart_elShowdatapoints(j)
                            ReDim Preserve chart_elSeriesVisible(j)
                            ReDim Preserve chart_elAutoScale(j)

                            chart_elements(j) = drow(7).trim
                            chart_elementsYAxis(j) = drow(9).trim
                            chart_Eltype(j) = drow(8).trim
                            chart_ElColor(j) = CInt(drow(17))
                            If UCase(chart_elementsYAxis(j)) = "LEFT" Then
                                chart_YaxisScale(0) = drow(10).trim
                                yaxis1.NumberPrecision = CInt(nZ(drow(15), 0))
                                If nZ(drow(11), "").Length > 0 Then
                                    yaxis1.Label.Text = drow(11).ToString.Trim
                                End If
                                If nZ(drow(13), " ").Length > 1 Then
                                    If drow(13).ToString.ToUpper = "PERC" Then
                                        yaxis1.Percent = True
                                    End If
                                End If
                                If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                                    yaxis1.MinimumInterval = 1

                                End If
                            ElseIf UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                                chart_YaxisScale(1) = drow(10).trim
                                yaxis2.NumberPrecision = CInt(nZ(drow(16), 0))

                                If nZ(drow(12), "").Length > 0 Then
                                    yaxis2.Label.Text = drow(12).ToString.Trim
                                End If
                                If nZ(drow(14), " ").Length > 1 Then
                                    If drow(14).ToString.ToUpper = "PERC" Then
                                        yaxis2.Percent = True
                                    End If
                                End If
                                If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                                    yaxis2.MinimumInterval = 1
                                End If
                            End If
                            If drow(19).ToString.Trim <> "" Then
                                chart_elsort(0) = drow(7).trim
                                chart_elsort(1) = drow(19).ToString.ToUpper()
                            End If
                            chart_elvis(j) = drow(20).ToString.Trim
                            chart_elLineSize(j) = nZ(drow("LineSize").ToString.Trim, 3)
                            chart_elShowdatapoints(j) = nZ(drow("ShowDatapoints").ToString.Trim, False)
                            chart_elSeriesVisible(j) = nZ(drow("IsVisible").ToString.Trim, True)
                            chart_elAutoScale(j) = nZ(drow("AutoScale").ToString.Trim, True)

                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > dt_chart.Rows.Count - 1 Then
                            Exit Do
                        Else
                            drow = dt_chart.Rows(rownum)
                        End If
                    Loop Until drow(5) <> lastchart
                    rownum = rownum - 1
                    drow = dt_chart.Rows(rownum)

                    'datagrid filling
                    'comment: need to skip element if not available in datatable!!

                    If chart_elsort(0) <> "0" And ChartElementSortOrder Is Nothing Then
                        dt.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                    ElseIf Not ChartElementSortOrder Is Nothing Then
                        dt.DefaultView.Sort = ChartElementSortOrder(0) + " " + ChartElementSortOrder(1)
                    End If

                    'construct filter
                    'Dim kpifilter As String = Nothing

                    'For Each el As String In chart_elements
                    '    For Each nd As TreeListViewNode In tlvFiltersTopX.Nodes
                    '        If el.ToString = nd.SubItems(0).Text.ToString Then
                    '            If kpifilter = Nothing Then
                    '                kpifilter = el.ToString + "  " + nd.SubItems(1).Text.ToString + "  " + nd.SubItems(2).Text.ToString
                    '            Else
                    '                kpifilter = kpifilter + " AND " + el.ToString + "  " + nd.SubItems(1).Text.ToString + "  " + nd.SubItems(2).Text.ToString
                    '            End If
                    '        End If
                    '    Next
                    'Next

                    'dt.DefaultView.RowFilter = kpifilter
                    Dim chart_elements_count As Integer = chart_elements.Count
                    For k = 0 To chart_elements_count - 1
                        ReDim Preserve chart_elements(chart_elements.Count + 1)
                        ReDim Preserve chart_Eltype(chart_Eltype.Count + 1)
                        ReDim Preserve chart_elementsYAxis(chart_elementsYAxis.Count + 1)
                        ReDim Preserve chart_ElColor(chart_ElColor.Count + 1)
                        ReDim Preserve chart_elShowdatapoints(chart_elShowdatapoints.Count + 1)
                        ReDim Preserve chart_elLineSize(chart_elLineSize.Count + 1)
                        ReDim Preserve chart_elSeriesVisible(chart_elSeriesVisible.Count + 1)
                        ReDim Preserve chart_elAutoScale(chart_elAutoScale.Count + 1)

                        chart_elements(chart_elements.Count - 2) = chart_elements(k) + "_Before"
                        chart_elements(chart_elements.Count - 1) = chart_elements(k) + "_After"
                        chart_Eltype(chart_Eltype.Count - 2) = chart_Eltype(k)
                        chart_Eltype(chart_Eltype.Count - 1) = chart_Eltype(k)
                        chart_elementsYAxis(chart_elementsYAxis.Count - 2) = chart_elementsYAxis(k)
                        chart_elementsYAxis(chart_elementsYAxis.Count - 1) = chart_elementsYAxis(k)
                        chart_ElColor(chart_ElColor.Count - 2) = chart_ElColor(k)
                        chart_ElColor(chart_ElColor.Count - 1) = chart_ElColor(k)
                        chart_elShowdatapoints(chart_elShowdatapoints.Count - 2) = chart_elShowdatapoints(k)
                        chart_elShowdatapoints(chart_elShowdatapoints.Count - 1) = chart_elShowdatapoints(k)
                        chart_elLineSize(chart_elLineSize.Count - 2) = chart_elLineSize(k)
                        chart_elLineSize(chart_elLineSize.Count - 1) = chart_elLineSize(k)
                        chart_elSeriesVisible(chart_elSeriesVisible.Count - 2) = chart_elSeriesVisible(k)
                        chart_elSeriesVisible(chart_elSeriesVisible.Count - 1) = chart_elSeriesVisible(k)
                        chart_elAutoScale(chart_elAutoScale.Count - 2) = chart_elAutoScale(k)
                        chart_elAutoScale(chart_elAutoScale.Count - 1) = chart_elAutoScale(k)
                    Next

                    Dim columnsfortopx(obj_columns.Length + chart_elements.Length - 1) As String
                    obj_columns.CopyTo(columnsfortopx, 0)
                    chart_elements.CopyTo(columnsfortopx, obj_columns.Count)

                    Dim dt_topx As DataTable = Nothing
                    Dim cellcolumn As String = ""
                    If tech.ToLower = _strNetwork.ToLower Then
                        Dim dt_subset As DataTable = dt.DefaultView.ToTable(False, columnsfortopx.Distinct().ToArray())
                        If Not dt_subset Is Nothing AndAlso dt_subset.Rows.Count > 0 Then
                            dt_topx = dt_subset.Rows.Cast(Of DataRow)().Take(objChartProp.TopXRowCount).CopyToDataTable
                        End If
                        cellcolumn = xval
                    End If

                    'Dim dgCtrl As GridControl = tblayout.GetControlFromPosition(1, chartindex)
                    'dgCtrl.DataSource = Nothing
                    'dgCtrl.DataSource = dt_topx
                    'dgCtrl.Tag = tech
                    'Dim dg As GridView = dgCtrl.MainView
                    'AddHandler dg.KeyDown, AddressOf dgTopXGridView_KeyDown

                    'For Each col As Columns.GridColumn In dg.Columns
                    '    If col.UnboundType = DevExpress.Data.UnboundColumnType.String Then
                    '        For Each srcbtn As IOS.Library.IOSToggleButton In flpCounterTypeTopX.Controls
                    '            If srcbtn.Text.ToLower <> col.Caption.ToLower Then
                    '                col.Visible = True
                    '            Else
                    '                col.Visible = False
                    '                Exit For
                    '            End If
                    '        Next
                    '    Else
                    '        col.Visible = False
                    '    End If
                    'Next

                    'For i = 0 To UBound(chart_elements)
                    '    For Each col As Columns.GridColumn In dg.Columns
                    '        If col.FieldName.ToUpper = chart_elements(i).ToUpper Then
                    '            col.Visible = True
                    '        End If
                    '    Next
                    'Next

                    'dg.Columns(cellcolumn).Visible = True
                    'If Not ChartElementSortOrder Is Nothing Then
                    '    dg.Columns(ChartElementSortOrder(0)).SortOrder = IIf(ChartElementSortOrder(1) = "ASC", DevExpress.Data.ColumnSortOrder.Ascending, DevExpress.Data.ColumnSortOrder.Descending)
                    'End If
                    'dg.OptionsView.ColumnAutoWidth = False
                    'dg.BestFitColumns(True)
                    'dgCtrl.Refresh()

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

                    'chart filling
                    Dim de As DataEngine = New DataEngine(dt_topx)
                    de.DataFields = String2DataFields_TopX(chart_elements, xval, chart_elvis)
                    sc = de.GetSeries()

                    For i = 0 To sc.Count() - 1

                        Select Case UCase(chart_Eltype(i).Trim)
                            Case "LINE"
                                sc(i).Type = SeriesType.Line
                                sc(i).Line.Width = CInt(chart_elLineSize(i))
                            Case "BAR"
                                sc(i).Type = SeriesType.Bar
                            Case "AREALINE"
                                sc(i).Type = SeriesType.AreaLine
                        End Select

                        Select Case UCase(chart_elementsYAxis(i).Trim)
                            Case "LEFT"
                                sc(i).YAxis = yaxis1
                                If chart_elAutoScale(i) = False Then
                                    yaxis1.Minimum = 0
                                End If
                            Case "RIGHT"
                                sc(i).YAxis = yaxis2
                                If chart_elAutoScale(i) = False Then
                                    yaxis2.Minimum = 0
                                End If
                        End Select

                        color_R = CLng(chart_ElColor(i)) Mod 256
                        color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                        color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                        If sc(i).Name.EndsWith("_Before") Then
                            sc(i).DefaultElement.Color = Color.FromArgb(200, 0, 255, 0)
                        ElseIf sc(i).Name.EndsWith("_After") Then
                            sc(i).DefaultElement.Color = Color.FromArgb(200, 255, 255, 0)
                        Else
                            sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
                        End If

                        If CBool(chart_elShowdatapoints(i)) = True Then
                            sc(i).DefaultElement.Marker.Type = ElementMarkerType.Circle
                            sc(i).DefaultElement.Marker.Size = 5
                            sc(i).EmptyElement.Mode = EmptyElementMode.None
                            sc(i).DefaultElement.Marker.Visible = True
                        Else
                            sc(i).DefaultElement.Marker.Type = ElementMarkerType.None
                            sc(i).DefaultElement.Marker.Visible = False
                        End If

                        If chart_elSeriesVisible(i) = True Then
                            sc(i).Visible = True
                        Else
                            sc(i).Visible = False
                            'HiddenSeriesCollectionTopX.Add(ch.Name, sc(i).Name)
                        End If

                        'If chartname_original Is Nothing Then
                        '    If chart_elements(i).EndsWith("_Before") Or chart_elements(i).EndsWith("_After") Then
                        '        sc(i).Visible = False
                        '    End If
                        'Else
                        'For Each seriesname As String In SeriesInVisible
                        '    If sc(i).Name = seriesname Then
                        '        sc(i).Visible = False
                        '    End If
                        'Next
                        'End If
                    Next

                    'If chartname_original Is Nothing And Not dict_TopXDelta_SeriesCollection.ContainsKey(tech.ToUpper + "|" + ch.Name) Then
                    '    dict_TopXDelta_SeriesCollection.Add(tech.ToUpper + "|" + ch.Name, sc)
                    'End If

                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    dt_topx.Dispose()
                    dt_topx = Nothing
                    sc = Nothing
                    de = Nothing

                    'check if TopXChart DeltaButtons are present
                    If chartname_original Is Nothing Then
                        TopXCharts_AddDeltaButtons(ch)
                    End If

                    ch.RefreshChart()
                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim Preserve chart_elvis(0)
                    ReDim Preserve chart_elLineSize(0)
                    ReDim Preserve chart_elShowdatapoints(False)
                    ReDim Preserve chart_elSeriesVisible(True)
                    ReDim Preserve chart_elAutoScale(True)

                    j = 0
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
                Console.WriteLine(ex.Message.ToString)
            End Try
        Next

        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing
    End Sub

    Private Sub AssignDataToTopX(ByRef ch As Chart, ByRef objChartProp As ObjectChartProperties, ByRef dt As DataTable, ByVal tech As String, Optional ByVal chartname_original As String = Nothing, Optional ByVal ChartElementSortOrder() As String = Nothing)
        Dim connstringconfig As String = Nothing
        Dim sqlchart As String = Nothing
        Dim objectscharted As String = ""
        Dim customTabIndexTopX As Integer = 0

        'Dim selected_tabs As String = tvKPITopX.GetKPIChecked2String(1, "ObjectName")
        'Dim selected_charts As String = tvKPITopX.GetKPIChecked2String(2, "ObjectName")
        Dim username As String = Chr(39) & Environment.UserName.ToString & Chr(39)

        sqlchart = clsSQLCommands.GetTopXChartConfigurationSQL(tech, chartSetName, ch.Name, username, chartname_original)
        Dim ds_chart As DataSet = DataAccessorODBC.GetDataSet(connStrIOSServer, sqlchart)
        Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to all charts
        '*************************
        Dim i As Integer
        Dim X1AxisLabel As String = ""
        Dim Y1axislabel As String = "", Y2axislabel As String = ""
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis
        Dim yaxis2 As Axis
        Dim sp As New SmartPalette()
        Dim color_R, color_B, color_G As Integer
        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim chart_elsort() As String = {"0", "0"}
        Dim chart_elvis() As String = {"0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim xval As String = ""

        Dim tabindex_old As Integer = 0
        Dim chartindex As Integer = -1
        Dim scatterObject As String = ""
        Dim primkeys_dt() As DataColumn = Nothing

        Dim primkey_index As Integer = 0
        For Each dc As DataColumn In dt.Columns
            If dc.DataType = GetType(System.String) Then
                ReDim Preserve primkeys_dt(primkey_index)
                primkeys_dt(primkey_index) = dc
                primkey_index = primkey_index + 1
            End If
        Next
        dt.PrimaryKey = primkeys_dt

        Dim x As Integer = 0
        Dim obj_columns() As String = Nothing
        For Each dc As DataColumn In dt.PrimaryKey
            ReDim Preserve obj_columns(x)
            obj_columns(x) = dc.ColumnName
            x = x + 1
        Next

        For rownum = 0 To dt_chart.Rows.Count - 1
            Try
                'collecting elements from chart configuration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                Dim tabindex_new As Integer = CInt(drow(2).ToString)
                chartindex = CInt(drow("ChartIndex").ToString)
                If tabindex_old <> tabindex_new Then
                    tabindex_old = tabindex_new
                End If

                'configures individual chart when new chart line is detected
                If lastchart = "" Or lastchart <> drow("ChartName").ToString Then
                    lastchart = drow("ChartName").ToString.Trim
                    sp.Clear()

                    Y1axisAbsorPerc = drow(13).trim
                    Y2axisAbsOrPerc = nZ(drow(14), "Abs")

                    Y1axisPrecision = CInt(drow(15))
                    Y2axisPrecision = CInt(nZ(drow(16), "0"))

                    If nZ(drow("chartY1axisLabels"), "").Length > 0 Then
                        Y1axislabel = drow("chartY1axisLabels").ToString.Trim
                    End If
                    If nZ(drow("chartY2axisLabels"), "").Length > 0 Then
                        Y2axislabel = drow("chartY2axisLabels").ToString.Trim
                    End If

                    If CInt(drow(2).ToString) = 99 And tech.ToUpper = _strNetwork.ToUpper Then
                        tabindex_new = customTabIndexTopX
                    End If

                    Select Case tech.ToLower
                        Case _strNetwork.ToLower
                            'tblayout = tcTabControlHighTopX.TabPages(tabindex_new).Controls(0) 'drow(2)
                            'ch = tblayout.GetControlFromPosition(0, chartindex) 'drow(4)
                            xval = objChartProp.TopXShowObjects 'flpSourceBtn_GetChecked("topx_" & _strNetwork.ToLower, flpCounterTypeTopX)(0).SourceButtonText
                        Case Else
                            'Console.WriteLine("AssignData2Charts: problem in tech selection")
                            Exit Sub
                    End Select

                    If drow("ChartType") = IOSChartType.Scatter Then
                        ch.Type = ChartType.Scatter
                        ch.Use3D = False
                        ch.DefaultSeries.Type = SeriesType.Marker
                        ch.DefaultSeries.DefaultElement.Transparency = 20
                        ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Range
                        ch.XAxis.FormatString = ""
                        ch.DefaultSeries.DefaultElement.ShowValue = False
                        ch.DefaultSeries.DefaultElement.Marker.Visible = True
                        ch.LegendBox.Visible = False
                    End If

                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(tech))
                    ch.Annotations(0).Position = New System.Drawing.Point(ch.Width - 70, 2)
                    ch.Annotations(0).DefaultCorner = BoxCorner.Square
                    ch.Annotations(0).Size = New Size(60, 25)
                    Dim fnt As System.Drawing.Font = New System.Drawing.Font("Arial", 6, FontStyle.Regular)
                    ch.Annotations(0).Label.Font = fnt

                    ch.TitleBox.Label.Text = drow(6).Trim
                    ch.DefaultElement.Hotspot.ToolTip = "%SeriesName = %Value"

                    'Y-Axis Settings   
                    yaxis1 = New Axis
                    yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                    yaxis1.Label.Text = Y1axislabel

                    yaxis2 = New Axis
                    yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right

                    If ch.Type = ChartType.Scatter Then
                        If dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ElementAxis") = "Y" Then
                            Y1axislabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ChartElements").ToString
                        Else
                            X1AxisLabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ChartElements").ToString
                        End If
                        If dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(1)("ElementAxis") = "X" Then
                            X1AxisLabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(1)("ChartElements").ToString
                        Else
                            Y1axislabel = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(1)("ChartElements").ToString
                        End If
                        ch.DefaultElement.Hotspot.ToolTip = X1AxisLabel & ": %XValue" & Chr(13) & Y1axislabel & ": %Value "
                        ch.XAxis.Label.Text = X1AxisLabel
                        scatterObject = dt_chart.Select("ChartType=" & IOSChartType.Scatter & " And ChartName='" & lastchart & "'")(0)("ObjectTab")
                    End If

                    'element based
                    Do
                        If ColumnInDataTable(drow(7).trim, dt) Then
                            ReDim Preserve chart_elements(j)
                            ReDim Preserve chart_elementsYAxis(j)
                            ReDim Preserve chart_Eltype(j)
                            ReDim Preserve chart_ElColor(j)
                            ReDim Preserve chart_elvis(j)
                            chart_elements(j) = drow(7).trim
                            chart_elementsYAxis(j) = drow(9).trim
                            chart_Eltype(j) = drow(8).trim
                            chart_ElColor(j) = CInt(drow(17))
                            If UCase(chart_elementsYAxis(j)) = "LEFT" Then
                                chart_YaxisScale(0) = drow(10).trim
                                yaxis1.NumberPrecision = CInt(nZ(drow(15), 0))
                                If nZ(drow(11), "").Length > 0 Then
                                    yaxis1.Label.Text = drow(11).ToString.Trim
                                End If
                                If nZ(drow(13), " ").Length > 1 Then
                                    If drow(13).ToString.ToUpper = "PERC" Then
                                        yaxis1.Percent = True
                                    End If
                                End If
                                If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                                    yaxis1.MinimumInterval = 1

                                End If
                            ElseIf UCase(chart_elementsYAxis(j)) = "RIGHT" Then
                                chart_YaxisScale(1) = drow(10).trim
                                yaxis2.NumberPrecision = CInt(nZ(drow(16), 0))

                                If nZ(drow(12), "").Length > 0 Then
                                    yaxis2.Label.Text = drow(12).ToString.Trim
                                End If
                                If nZ(drow(14), " ").Length > 1 Then
                                    If drow(14).ToString.ToUpper = "PERC" Then
                                        yaxis2.Percent = True
                                    End If
                                End If
                                If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                                    yaxis2.MinimumInterval = 1

                                End If
                            End If
                            If drow(19).ToString.Trim <> "" Then
                                chart_elsort(0) = drow(7).trim
                                chart_elsort(1) = drow(19).ToString.ToUpper()
                            End If
                            chart_elvis(j) = drow(20).ToString.Trim


                            j = j + 1
                        End If
                        rownum = rownum + 1
                        If rownum > dt_chart.Rows.Count - 1 Then
                            Exit Do
                        Else
                            drow = dt_chart.Rows(rownum)
                        End If
                    Loop Until drow(5) <> lastchart
                    rownum = rownum - 1
                    drow = dt_chart.Rows(rownum)

                    'data grid filling
                    'comment: need to skip element if not available in data table!!

                    If chart_elsort(0) <> "0" And ChartElementSortOrder Is Nothing Then
                        dt.DefaultView.Sort = chart_elsort(0) + " " + chart_elsort(1)
                    ElseIf Not ChartElementSortOrder Is Nothing Then
                        dt.DefaultView.Sort = ChartElementSortOrder(0) + " " + ChartElementSortOrder(1)
                    End If

                    Dim columnsfortopx(obj_columns.Length + chart_elements.Length - 1) As String
                    obj_columns.CopyTo(columnsfortopx, 0)
                    chart_elements.CopyTo(columnsfortopx, obj_columns.Count)
                    Dim dt_topx As DataTable = Nothing
                    Dim cellcolumn As String = ""
                    If tech.ToLower = _strNetwork.ToLower Then
                        Dim dt_subset As DataTable = dt.DefaultView.ToTable(False, columnsfortopx.Distinct().ToArray())
                        If Not dt_subset Is Nothing AndAlso dt_subset.Rows.Count > 0 Then
                            dt_topx = dt_subset.Rows.Cast(Of DataRow)().Take(objChartProp.TopXRowCount).CopyToDataTable
                        End If
                        cellcolumn = xval
                    End If

                    If Not dt_topx Is Nothing Then

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

                        If ch.Type = ChartType.Scatter Then
                            Try
                                ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
                                Dim minValue As Double = dt.Compute("Min(" & X1AxisLabel & ")", "")
                                Dim maxValue As Double = dt.Compute("Max(" & X1AxisLabel & ")", "")
                                ch.XAxis.ScaleRange.ValueLow = IIf(minValue < 0, System.Math.Floor(minValue), System.Math.Ceiling(minValue))
                                ch.XAxis.ScaleRange.ValueHigh = IIf(maxValue < 0, System.Math.Floor(maxValue), System.Math.Ceiling(maxValue))
                            Catch
                            End Try
                        End If

                        'chart filling
                        Dim de As DataEngine = New DataEngine(dt_topx)
                        If ch.Type = ChartType.Scatter Then
                            de.DataFields = "XValue=" & X1AxisLabel & ",YValue=" & Y1axislabel & ",Object=" & scatterObject
                        Else
                            de.DataFields = String2DataFields_TopX(chart_elements, xval, chart_elvis)
                        End If
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
                            If ch.Type = ChartType.Scatter Then
                                yaxis1.Label.Text = Y1axislabel
                                sc(i).Type = SeriesType.Marker
                            End If
                            Select Case UCase(chart_elementsYAxis(i).Trim)
                                Case "LEFT"
                                    sc(i).YAxis = yaxis1
                                Case "RIGHT"
                                    sc(i).YAxis = yaxis2
                            End Select

                            color_R = CLng(chart_ElColor(i)) Mod 256
                            color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                            color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                            sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
                        Next

                        ch.SeriesCollection.Clear()
                        ch.SeriesCollection.Add(sc)

                        dt_topx.Dispose()
                        dt_topx = Nothing
                        sc = Nothing
                        de = Nothing

                        ch.RefreshChart()
                    End If

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    ReDim Preserve chart_elvis(0)

                    j = 0
                End If
            Catch ex As Exception
                'Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & vbCrLf & ex.InnerException.ToString)
                'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)
                ReDim chart_elements(0)
                ReDim chart_elementsYAxis(0)
                ReDim chart_Eltype(0)
                ReDim chart_ElColor(0)
                ReDim chart_YaxisScale(1)
                ReDim Preserve chart_elvis(0)
                j = 0
            End Try
        Next
        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing

    End Sub

    Private Function String2DataFields_TopX(ByVal str() As String, ByVal xval As String, ByVal elvis() As String) As String
        Dim stroutput As String
        Dim i As Integer
        stroutput = "XValue=" & xval
        For i = 0 To UBound(str)
            stroutput = stroutput & "," & " Yvalue=" & str(i)
        Next
        String2DataFields_TopX = stroutput
    End Function

    Public Function ColumnInDataTable(ByVal columname As String, ByRef dt As DataTable) As Boolean
        For Each col As DataColumn In dt.Columns
            If col.Caption.ToString.Trim.ToUpper = columname.ToUpper Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region "Time Based Chart"

    Private Sub CreateStatsChart(ByRef objChartProp As ObjectChartProperties, objectName As String, ByRef objSlideProp As SlideProperties, ByRef presDoc As DocumentFormat.OpenXml.Packaging.PresentationDocument)
        Dim nc As New dotnetCHARTING.WinForms.Chart
        nc.Name = objectName
        nc.Width = objChartProp.Width
        nc.Height = objChartProp.Height
        'Chart Default Properties
        nc.DefaultElement.Marker.Visible = False
        nc.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        nc.LegendBox.DefaultEntry.Value = ""
        nc.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        nc.LegendBox.Visible = True
        nc.LegendBox.DefaultCorner = BoxCorner.Round

        nc.XAxis.TickLabelMode = TickLabelMode.Angled
        nc.XAxis.TickLabelAngle = 45
        nc.XAxis.Minimum = 0
        nc.XAxis.Maximum = 0

        nc.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        nc.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

        nc.ToolTip.InitialDelay = 1
        nc.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        nc.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        nc.CleanupPeriod = 1

        nc.TitleBox.Position = TitleBoxPosition.Full
        nc.TitleBox.CornerTopLeft = BoxCorner.Round
        nc.TitleBox.CornerTopRight = BoxCorner.Round
        nc.TitleBox.Label.AutoWrap = True
        nc.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"

        If Me.rptCurrOrConfig = "Current" Then
            Console.WriteLine("Getting chart from technology: " & objectName)
            nc = GetChartFromTechnology(objectName, _strNetwork, nc)
            nc.Width = objChartProp.Width
            nc.Height = objChartProp.Height
        ElseIf Me.rptCurrOrConfig = "Config" Then
            SetChartConfigData(objChartProp.Technology, objectName)
            Console.WriteLine("Getting data for chart: " & objectName)
            ProcessStatsChart(nc, objChartProp, objectName)
        End If

        'copy chart image to ppt slide
        Console.WriteLine("Copying image for chart: " & objectName)

        If slide IsNot Nothing AndAlso slidePart IsNot Nothing Then
            CopyChartBitmapToSlide(slide, slidePart, drawingObjectId, nc)
        Else
            Dim presPart As PresentationPart = presDoc.PresentationPart
            InsertNewSlide(presPart, slidePosition, objSlideProp.SlideTitle, objChartProp, nc)
            slidePosition = slidePosition + 1
        End If
    End Sub

    Private Sub SetChartConfigData(tech As String, chartName As String)
        Dim sql As String = Nothing
        'WHERE (((ChartSetName = " & Chr(39) & chartSetName & Chr(39) & ") OR (ChartSetName = " & Chr(39) & Environment.UserName.ToString & Chr(39) & ")) AND
        sql = "SELECT * FROM IOS_Chart_Configuration WHERE TechTab = " & Chr(39) & tech & Chr(39) & " AND ChartName = " & Chr(39) & chartName & Chr(39) & "     
               ORDER BY techtab, objecttab, objecttabindex, categorytabindex, chartindex,ChartTitle ASC"
        Dim dt_charts As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, sql)
        Dim dtChartGeneration As New DataTable
        dtChartGeneration = dt_charts.DefaultView.ToTable(True, {"techtab", "categorytabindex", "categorytab", "chartindex", "chartname", "objecttab", "objecttabindex", "ChartTitle"})
        ChartConfig.ChartFillingDataTable = dt_charts
        ChartConfig.ChartGenerationDataTable = dtChartGeneration
    End Sub

    Private Sub ProcessStatsChart(ByRef ch As dotnetCHARTING.WinForms.Chart, ByRef objChartProp As ObjectChartProperties, chartName As String)
        Dim chartSql As String = Nothing
        Using conn_el As New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()
            Using comm_Element As New Odbc.OdbcCommand("SELECT DISTINCT COALESCE(IOS_SQL_KPI.sourcetable,'') AS sourcetable,COALESCE(IOS_SQL_KPI.JoinObjects,'') AS JoinObjects,COALESCE(IOS_Chart_Configuration.CrossTabObj,'') AS CrossTabObj
                                                        FROM IOS_Chart_Configuration INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID  
                                                        WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & objChartProp.Technology & Chr(39) & ")
                                                        AND ChartName = '" & chartName & "' AND (IOS_SQL_KPI.Object = " & Chr(39) & dtChartStyleProperties.Rows(0)("CounterType").ToString & Chr(39) & ")
                                                        AND (sourcetable Is Not null)", conn_el)

                Using dr As Odbc.OdbcDataReader = comm_Element.ExecuteReader
                    While dr.Read
                        chartSql = SQL_Construct(dtChartStyleProperties.Rows(0)("CounterType").ToString, nZ(dr.Item("sourcetable").ToString.Trim, ""), chartName, nZ(dr.Item("CrossTabObj"), ""))
                    End While
                End Using
            End Using
        End Using

        dsStats = New DataSet
        dsStats = GetData(chartSql)
        Console.WriteLine("Assigning data to chart: " & chartName)
        AssignDataToCharts(ch, objChartProp, dsStats.Tables(0))
    End Sub

    Private Sub AssignDataToCharts(ByRef ch As Chart, ByRef objChartProp As ObjectChartProperties, ByRef dtData As DataTable)
        Dim dtChartConfig As New DataTable
        dsHistogramData = New DataSet
        'If dtChartSubset Is Nothing Then
        'Dim selected_obj As String = tvKPIStats.GetKPIChecked2String(1, "ObjectName")
        'Dim selected_charts As String = tvKPIStats.GetKPIChecked2String(3, "ObjectName")
        Dim username As String = Chr(39) & Environment.UserName.ToString & Chr(39)
        'Dim ChartConfig As ChartConfigDataTables = TabControl.Tag

        '(((ChartSetName = " & Chr(39) & chartSetName & Chr(39) & "))  AND TechTab = " & Chr(39) & objChartProp.Technology & Chr(39) & " AND CrossTabObj IS NULL
        '                                                            And ObjectTab='" & dtChartStyleProperties.Rows(0)("CounterType").ToString & "' AND 

        ChartConfig.ChartFillingDataTable.DefaultView.RowFilter = "ChartName ='" & ch.Name & "' AND TechTab = " & Chr(39) & objChartProp.Technology & Chr(39) & " "
        ChartConfig.ChartFillingDataTable.DefaultView.Sort = "techtab ASC, objecttabindex ASC, categorytabindex ASC, chartindex ASC, chartelementid ASC"
        dtChartConfig = ChartConfig.ChartFillingDataTable.DefaultView.ToTable()
        ChartConfig.ChartFillingDataTable.DefaultView.RowFilter = ""
        'Else
        '    dtChartSubset.DefaultView.RowFilter = "CrossTabObj IS NULL"
        '    dtChartSubset.DefaultView.Sort = "techtab ASC, objecttabindex ASC, categorytabindex ASC, chartindex ASC, chartelementid ASC"
        '    dtChartConfig = dtChartSubset.DefaultView.ToTable
        '    dtChartSubset.DefaultView.RowFilter = ""
        'End If

        Try
            'Assign data to all charts
            'Dim ch As Chart
            Dim objectscharted As String = ""
            Dim X1AxisLabel As String = "Date"
            Dim Y1axislabel As String = "", Y2axislabel As String = ""
            Dim Y1axisAbsorPerc As String = "", Y2axisAbsOrPerc As String = ""
            Dim Y1axisPrecision As Integer = 0, Y2axisPrecision As Integer = 0
            Dim yaxis1 As Axis = Nothing
            Dim yaxis2 As Axis = Nothing
            Dim color_R As Integer = 0, color_B As Integer = 0, color_G As Integer = 0
            Dim tabIndexNew As Integer = 0
            'Dim chartIndex As Integer = 0

            Dim chartElements() As String = {"0"}
            Dim chartElementsYAxis() As String = {"0"}
            Dim chartEltype() As String = {"Bar"}
            Dim chartElColor() As Integer = {0}
            Dim chartYAxisScale() As String = {"0", "0"}

            dtChartConfig.DefaultView.RowFilter = "ObjectTab='" & dtChartStyleProperties.Rows(0)("CounterType").ToString & "'"
            Using dtObjectTab = dtChartConfig.DefaultView.ToTable(True, {"ObjectTab", "ObjectTabIndex"})
                For Each drObjectTab As DataRow In dtObjectTab.Select("", "ObjectTabIndex ASC")

                    'tabcontrol = GetTabControlFromTech(tech).TabPages(GetTabPageIndex(tcTabControlHighStats, drObjectTab("ObjectTab").ToString.Trim)).Controls(0)
                    dtChartConfig.DefaultView.RowFilter = "ObjectTabIndex=" & drObjectTab("ObjectTabIndex")
                    Using dtChartList = dtChartConfig.DefaultView.ToTable(True, {"TechTab", "CategoryTabIndex", "CategoryTab", "ChartIndex", "ChartName", "ChartTitle", "ChartType"})
                        For Each drChart As DataRow In dtChartList.Select("", "CategoryTabIndex, ChartIndex ASC")
                            'chartIndex = CInt(drChart("ChartIndex").ToString) * NumOfObjects + IndexOfObject
                            'If CInt(drChart("CategoryTabIndex").ToString) = 99 Then
                            '    tabIndexNew = GetTabPageIndex(tabcontrol, "Custom")
                            'Else
                            '    tabIndexNew = drChart("CategoryTabIndex")
                            'End If

                            'If tabIndexNew = 10 And chartIndex = 1 Then
                            'Console.WriteLine("")
                            'End If

                            'ch = tabcontrol.TabPages(tabIndexNew).Controls(0).Controls(chartIndex)

                            If drChart("ChartType") = IOSChartType.AlignInterval Then
                                'If tsmi_ObjectAggregationOnOff.Checked = False Then
                                '    Dim dsTemp As New DataSet
                                '    dsTemp.Tables.Add(dtData)
                                '    ProcessStatsCompareTime_Custom(tech, dsTemp, connStr, drChart("ChartName"), chartSetName, drObjectTab("ObjectTab").ToString.Trim, ch, ObjectName)
                                'Else
                                'ProcessStatsCompareTime_Custom(tech, dsStats, connStr, drChart("ChartName"), chartSetName, drObjectTab("ObjectTab").ToString.Trim, ch)
                                'End If
                            Else
                                X1AxisLabel = "Date"
                                Y1axislabel = ""
                                Y2axislabel = ""
                                Y1axisAbsorPerc = ""
                                Y2axisAbsOrPerc = ""
                                Y1axisPrecision = 0
                                Y2axisPrecision = 0
                                yaxis1 = Nothing
                                yaxis2 = Nothing
                                color_R = 0
                                color_B = 0
                                color_G = 0

                                'Default chart settings
                                DefaultChartSettings(ch, drChart("TechTab"))

                                'Settings For Chart Type
                                ChartTypeSettings(ch, drChart, objectscharted)

                                'If ObjectName <> "" Then
                                'objectscharted = ObjectName
                                'End If

                                ch.TitleBox.HeaderLabel.Text = drChart("ChartTitle").Trim
                                ch.TitleBox.Label.Text = "Objects: " & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString

                                Dim colList As String() = {
                                    "ChartElementID", "ChartElements", "chartElementsType", "chartElementsYAxis", "chartYaxisScaleProp", "chartY1axisLabels",
                                    "chartY2axisLabels", "chartY1AbsPerc", "chartY2AbsPerc", "chartY1axisPrecision", "chartY2axisPrecision",
                                    "ChartElementsColor", "SQLKPI_ID", "Sort_dir", "ElmntDisplay", "ChartSetName", "CrossTabObj", "ElementAxis", "ChartType"
                                }

                                'configures individual chart when new chartline is detected
                                dtChartConfig.DefaultView.RowFilter = "ChartName='" & drChart("ChartName") & "'"
                                Using dtKpi = dtChartConfig.DefaultView.ToTable(True, colList)
                                    Dim j As Integer = 0
                                    For Each drKpi As DataRow In dtKpi.Rows
                                        Y1axisAbsorPerc = nZ(drKpi("chartY1AbsPerc"), "Abs")
                                        Y2axisAbsOrPerc = nZ(drKpi("chartY2AbsPerc"), "Abs")

                                        Y1axisPrecision = CInt(nZ(drKpi("chartY1axisPrecision"), 0))
                                        Y2axisPrecision = CInt(nZ(drKpi("chartY2axisPrecision"), 0))

                                        If nZ(drKpi("chartY1axisLabels"), "").Length > 0 Then
                                            Y1axislabel = drKpi("chartY1axisLabels").ToString.Trim
                                        End If
                                        If nZ(drKpi("chartY2axisLabels"), "").Length > 0 Then
                                            Y2axislabel = drKpi("chartY2axisLabels").ToString.Trim
                                        End If

                                        If ch.Type = ChartType.Scatter Then
                                            If dtKpi.Select("ChartType=" & IOSChartType.Scatter)(0)("ElementAxis") = "Y" Then
                                                Y1axislabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(0)("ChartElements").ToString
                                            Else
                                                X1AxisLabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(0)("ChartElements").ToString
                                            End If
                                            If dtKpi.Select("ChartType=" & IOSChartType.Scatter)(1)("ElementAxis") = "X" Then
                                                X1AxisLabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(1)("ChartElements").ToString
                                            Else
                                                Y1axislabel = dtKpi.Select("ChartType=" & IOSChartType.Scatter)(1)("ChartElements").ToString
                                            End If
                                            ch.DefaultElement.Hotspot.ToolTip = X1AxisLabel & ": %XValue" & Chr(13) & Y1axislabel & ": %Value "
                                            ch.XAxis.Label.Text = X1AxisLabel
                                        End If

                                        'Y-Axis Settings  
                                        If yaxis1 Is Nothing Then
                                            yaxis1 = New Axis()
                                            yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                                        End If
                                        yaxis1.Label.Text = Y1axislabel

                                        yaxis1.NumberPrecision = Y1axisPrecision

                                        If UCase(Y1axisAbsorPerc) = "PERC" Then
                                            yaxis1.Percent = True
                                        End If

                                        If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                                            yaxis1.MinimumInterval = 1
                                        End If

                                        If yaxis2 Is Nothing Then
                                            yaxis2 = New Axis()
                                            yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Right
                                        End If
                                        yaxis2.Label.Text = Y2axislabel

                                        yaxis2.NumberPrecision = Y2axisPrecision

                                        If UCase(Y2axisAbsOrPerc) = "PERC" Then
                                            yaxis2.Percent = True
                                        End If

                                        If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True And drKpi("chartElementsYAxis").trim = "Right" Then
                                            yaxis2.MinimumInterval = 1
                                        End If

                                        ReDim Preserve chartElements(j)
                                        ReDim Preserve chartElementsYAxis(j)
                                        ReDim Preserve chartEltype(j)
                                        ReDim Preserve chartElColor(j)

                                        chartElements(j) = drKpi("ChartElements").ToString.Trim
                                        chartElementsYAxis(j) = drKpi("chartElementsYAxis").trim
                                        chartEltype(j) = drKpi("chartElementsType").trim
                                        chartElColor(j) = CInt(drKpi("ChartElementsColor"))

                                        If UCase(chartElementsYAxis(j)) = "LEFT" Then
                                            chartYAxisScale(0) = drKpi("chartYaxisScaleProp").trim
                                        ElseIf UCase(chartElementsYAxis(j)) = "RIGHT" Then
                                            chartYAxisScale(1) = drKpi("chartYaxisScaleProp").trim
                                        End If
                                        j = j + 1
                                    Next
                                End Using

                                If UCase(chartYAxisScale(0)) = "STACKED" Then
                                    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                                ElseIf UCase(chartYAxisScale(0)) = "FULLSTACKED" Then
                                    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                                Else
                                    yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                                End If

                                If UCase(chartYAxisScale(1)) = "STACKED" Then
                                    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                                ElseIf UCase(chartYAxisScale(1)) = "FULLSTACKED" Then
                                    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                                Else
                                    yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                                End If

                                ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal

                                If ch.Type = ChartType.Combo Then
                                    Dim xaxis_valuehigh As DateTime = CDate(GetFromTech_DateTimePicker(2)) 'Add 1 day to insert a extra x-axis scale.

                                    Select Case True
                                        Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw", dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly"
                                            xaxis_valuehigh = xaxis_valuehigh.AddHours(1)
                                        Case Else
                                            xaxis_valuehigh = xaxis_valuehigh.AddDays(1)
                                    End Select

                                    If xaxis_valuehigh.Date = Now.Date Then
                                        Select Case True
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "daily"
                                                xaxis_valuehigh = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date))
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH"
                                                xaxis_valuehigh = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date))
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly"
                                                xaxis_valuehigh = DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date)
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH"
                                                xaxis_valuehigh = DateAdd(DateInterval.WeekOfYear, -1, xaxis_valuehigh.Date)
                                            Case dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly"
                                                xaxis_valuehigh = DateAdd(DateInterval.Month, -1, xaxis_valuehigh.Date)
                                        End Select
                                    End If

                                    ch.XAxis.ScaleRange.ValueHigh = xaxis_valuehigh
                                ElseIf ch.Type = ChartType.Scatter Then
                                    Try
                                        ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
                                        Dim minValue As Double = dtData.Compute("Min(" & X1AxisLabel & ")", "")
                                        Dim maxValue As Double = dtData.Compute("Max(" & X1AxisLabel & ")", "")
                                        ch.XAxis.ScaleRange.ValueLow = IIf(minValue < 0, System.Math.Floor(minValue), System.Math.Ceiling(minValue))
                                        ch.XAxis.ScaleRange.ValueHigh = IIf(maxValue < 0, System.Math.Floor(maxValue), System.Math.Ceiling(maxValue))
                                    Catch
                                    End Try
                                End If

                                Dim de As DataEngine = New DataEngine(dtData)
                                If ch.Type = ChartType.Scatter Then
                                    de.DataFields = "XValue=" & X1AxisLabel & ",YValue=" & Y1axislabel
                                Else
                                    de.DataFields = String2DataFields(chartElements, X1AxisLabel)
                                End If
                                de.DataGridFormatString = "N2"

                                If ch.Type = ChartType.Combo Then
                                    If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Or dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                                        de.FormatString = "dd/MM/yy HH:mm"
                                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                                        de.FormatString = "dd/MM/yy"
                                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                                        de.FormatString = "MMMM"
                                    End If
                                ElseIf ch.Type = ChartType.Scatter Then
                                    de.FormatString = ""
                                End If

                                Dim sc As New SeriesCollection
                                sc = de.GetSeries()

                                Dim LeftAxisDivisor As Int32 = 1
                                Dim RightAxisDivisor As Int32 = 1
                                Dim LeftAxisLabelAddition As String = ""
                                Dim RightAxisLabelAddition As String = ""

                                For i = 0 To sc.Count - 1
                                    Dim MaxValueOfSeries As Double = sc(i).Calculate("test", Calculation.Maximum).YValue
                                    If MaxValueOfSeries > 1000000000 Then
                                        If MaxValueOfSeries > 1000000000000 Then
                                            Select Case chartElementsYAxis(i).ToString().Trim().ToUpper()
                                                Case "LEFT"
                                                    LeftAxisDivisor = 1000000
                                                    LeftAxisLabelAddition = " Million"
                                                    Exit Select
                                                Case "RIGHT"
                                                    RightAxisDivisor = 1000000
                                                    RightAxisLabelAddition = " Million"
                                                    Exit Select
                                            End Select
                                        Else
                                            Select Case chartElementsYAxis(i).ToString().Trim().ToUpper()
                                                Case "LEFT"
                                                    If LeftAxisDivisor < 1000 Then
                                                        LeftAxisDivisor = 1000
                                                        LeftAxisLabelAddition = " Thousand"
                                                    End If
                                                    Exit Select
                                                Case "RIGHT"
                                                    If RightAxisDivisor < 1000 Then
                                                        RightAxisDivisor = 1000
                                                        RightAxisLabelAddition = " Thousand"
                                                    End If
                                                    Exit Select
                                            End Select
                                        End If

                                    End If
                                Next

                                Dim boundaries As String = Nothing
                                Dim dtHistogramData As DataTable = Nothing
                                For i = 0 To sc.Count() - 1
                                    If ch.Type = ChartType.Combo Then
                                        Select Case UCase(chartEltype(i).Trim)
                                            Case "LINE"
                                                sc(i).Type = SeriesType.Line
                                                sc(i).Line.Width = 3
                                            Case "BAR"
                                                sc(i).Type = SeriesType.Bar
                                            Case "AREALINE"
                                                sc(i).Type = SeriesType.AreaLine
                                        End Select
                                    ElseIf ch.Type = ChartType.Scatter Then
                                        yaxis1.Label.Text = Y1axislabel
                                        sc(i).Type = SeriesType.Marker
                                    End If

                                    Select Case UCase(chartElementsYAxis(i).Trim)
                                        Case "LEFT"
                                            sc(i).YAxis = yaxis1
                                        Case "RIGHT"
                                            sc(i).YAxis = yaxis2
                                    End Select


                                    Select Case chartElementsYAxis(i).ToString().Trim().ToUpper()
                                        Case "LEFT"

                                            If LeftAxisDivisor > 1 Then
                                                sc(i) = Series.Divide(sc(i), LeftAxisDivisor)
                                            End If
                                            If Not yaxis1.Label.Text.Contains(LeftAxisLabelAddition) Then
                                                yaxis1.Label.Text = yaxis1.Label.Text + LeftAxisLabelAddition
                                            End If

                                            sc(i).YAxis = yaxis1
                                            Exit Select
                                        Case "RIGHT"
                                            If RightAxisDivisor > 1 Then
                                                sc(i) = Series.Divide(sc(i), RightAxisDivisor)
                                            End If

                                            If Not yaxis2.Label.Text.Contains(RightAxisLabelAddition) Then
                                                yaxis2.Label.Text = yaxis2.Label.Text + RightAxisLabelAddition
                                            End If
                                            sc(i).YAxis = yaxis2
                                            Exit Select
                                    End Select

                                    color_R = CLng(chartElColor(i)) Mod 256
                                    color_G = (CLng(chartElColor(i)) \ 256) Mod 256
                                    color_B = ((CLng(chartElColor(i)) \ 256) \ 256) Mod 256

                                    sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
                                    sc(i).DefaultElement.Marker.Type = i + 1

                                    'Y-Axis boundaries for all series
                                    Try
                                        If drChart("ChartType") = IOSChartType.Histogram Then
                                            If boundaries IsNot Nothing Then
                                                boundaries = boundaries & "," & sc(i).GetYValueList()
                                            Else
                                                boundaries = sc(i).GetYValueList()
                                            End If
                                            If dtHistogramData Is Nothing Then
                                                dtHistogramData = New DataTable()
                                                dtHistogramData.TableName = ch.Name
                                                dtHistogramData.Columns.Add(New DataColumn("Bins", GetType(Double)))
                                            End If
                                            dtHistogramData.Columns.Add(New DataColumn("Freq_" & sc(i).Name, GetType(Integer)))
                                        End If
                                    Catch
                                    End Try
                                Next

                                'Configure histogram chart area
                                Try
                                    If drChart("ChartType") = IOSChartType.Histogram Then
                                        Dim boundariesArr As Double() = StringToDoubleArray(boundaries, New String() {","})
                                        Dim minValue As Double = boundariesArr.Min()
                                        Dim maxValue As Double = boundariesArr.Max()
                                        Dim binsGap As Double = System.Math.Ceiling((maxValue - minValue) / 30)

                                        Dim bins(29) As Double
                                        For index As Integer = 0 To bins.Length - 1
                                            bins(index) = minValue + (binsGap * (index + 1))
                                            Dim drHistogram As DataRow
                                            drHistogram = dtHistogramData.NewRow
                                            drHistogram.Item("Bins") = Convert.ToDouble(bins(index))
                                            dtHistogramData.Rows.Add(drHistogram)
                                        Next
                                        ch.ExtraChartAreas.Item(0).XAxis.Minimum = minValue
                                        ch.ExtraChartAreas.Item(0).XAxis.Interval = binsGap

                                        dsHistogramData.Tables.Add(dtHistogramData)

                                        For i = 0 To sc.Count() - 1
                                            Dim freqTable As Series = StatisticalEngine.FrequencyTableOL(sc(i), bins)
                                            freqTable.Name = "Freq_" & sc(i).Name
                                            freqTable.Type = SeriesType.Bar
                                            ch.ExtraChartAreas.Item(0).SeriesCollection.Add(freqTable)
                                        Next
                                    End If
                                Catch
                                End Try

                                ch.SeriesCollection.Clear()
                                ch.SeriesCollection.Add(sc)

                                sc = Nothing
                                de = Nothing
                                ch.XAxis.Markers.Clear()

                                ch.RefreshChart()
                                ch.ResumeLayout()
                                ReDim chartElements(0)
                                ReDim chartElementsYAxis(0)
                                ReDim chartEltype(0)
                                ReDim chartElColor(0)
                                ReDim chartYAxisScale(1)

                            End If
                        Next
                    End Using
                Next
            End Using

        Catch ex As Exception
            'Console.WriteLine(ex.Message.ToString)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)
        End Try
        dtChartConfig.Dispose()
        dtChartConfig = Nothing
    End Sub

    Private Function GetFromTech_DateTimePicker(ByVal startorend As Integer) As String
        Try
            If startorend = 1 Then Return dtChartStyleProperties.Rows(0)("ManualStartTime") Else Return dtChartStyleProperties.Rows(0)("ManualEndTime")
        Catch ex As Exception
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)
        End Try
        Return Nothing
    End Function

    Public Function StringToDoubleArray(ByVal Input As String, ByVal Separators As String()) As Double()
        Dim StringArray() As String = Input.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
        Dim DoubleList As New List(Of Double)

        For z = 0 To StringArray.Length - 1
            Dim TempVal As Double
            If Double.TryParse(StringArray(z), TempVal) = True Then
                DoubleList.Add(TempVal)
            End If
        Next
        Return DoubleList.ToArray()
    End Function

    Public Function String2DataFields(ByRef str() As String, ByRef xval As String) As String
        Dim stroutput As String
        Dim i As Integer
        stroutput = "XValue=" & xval ' a(0)
        For i = 0 To UBound(str)
            stroutput = stroutput & "," & " Yvalue=" & str(i)
        Next
        String2DataFields = stroutput
    End Function

    Private Sub DefaultChartSettings(ByRef ch As dotnetCHARTING.WinForms.Chart, ByVal tech As String)
        ch.Type = ChartType.Combo
        ch.TempDirectory = "temp"
        ch.Annotations.Clear()
        ch.Annotations.Add(New Annotation(tech.ToUpper))
        ch.Annotations(0).Size = New Size(50, 30)
        If tech.Length > 3 Then
            Dim fnt As System.Drawing.Font = New System.Drawing.Font("Arial", 6, FontStyle.Regular)
            ch.Annotations(0).Label.Font = fnt
        End If
        ch.MarginBottom = 5
        ch.ExtraChartAreas.Clear()
        ch.TitleBox.Label.Alignment = StringAlignment.Near
        ch.TitleBox.Label.LineAlignment = StringAlignment.Near
        ch.XAxis.Label.Text = ""
        ch.LegendBox.Visible = True
        ch.DefaultSeries.DefaultElement.Marker.Visible = False
    End Sub

    Private Sub ChartTypeSettings(ByRef ch As dotnetCHARTING.WinForms.Chart, ByVal dRow As DataRow, ByRef objcharted As String)
        If dRow("ChartType") = IOSChartType.Scatter Then
            ch.Type = ChartType.Scatter
            ch.Use3D = False
            ch.DefaultSeries.Type = SeriesType.Marker
            ch.DefaultSeries.DefaultElement.Transparency = 20
            ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Range
            ch.XAxis.FormatString = ""
            ch.DefaultSeries.DefaultElement.ShowValue = False
            ch.DefaultSeries.DefaultElement.Marker.Visible = True
            ch.LegendBox.Visible = False

        ElseIf dRow("ChartType") = IOSChartType.Histogram Then
            ch.Type = ChartType.Combo
            ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Vertical
            ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Normal
            Dim histChartArea As New ChartArea()
            histChartArea.HeightPercentage = 40
            histChartArea.YAxis.Label.Text = "Frequency"
            histChartArea.XAxis.Label.Text = "Bins"
            histChartArea.YAxis.Interval = 1
            histChartArea.DefaultElement.Hotspot.ToolTip = "Bins: %XValue" & Chr(13) & "%SeriesName: %Value "
            ch.ExtraChartAreas.Add(histChartArea)
            ch.DefaultElement.Hotspot.ToolTip = "%SeriesName: %Value "

        ElseIf dRow("ChartType") = IOSChartType.Radar Then
            ch.Type = ChartType.Radar
            ch.XAxis.RadarMode = RadarMode.Polar
            ch.RadarLabelMode = RadarLabelMode.Angled
            ch.DefaultSeries.Type = SeriesType.Line
            ch.DefaultSeries.DefaultElement.Transparency = 35

        ElseIf dRow("ChartType") = IOSChartType.Pie Then
            ch.Type = ChartType.Pie
            ch.PieLabelMode = PieLabelMode.Outside
            ch.DefaultSeries.DefaultElement.ShowValue = True

        ElseIf dRow("ChartType") = IOSChartType.Combo Then
            SetChartXAxis(dRow("TechTab").ToString, objcharted, ch)
            If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Or dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                ch.DefaultElement.Hotspot.ToolTip = "DATE: <%XValue,dd/MM/yy HH:mm>" & Chr(13) & "%SeriesName: %Value "
            Else
                ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
            End If
        End If
        objcharted = ObjectsCharted
    End Sub

    Public Sub SetChartXAxis(ByVal tech As String, ByRef objcharted As String, ByRef ch As dotnetCHARTING.WinForms.Chart)
        Try
            Dim TimeResolution As String = Nothing
            Select Case tech.ToUpper
                Case _strNetwork.ToUpper
                    objcharted = ObjectsCharted
                    If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                        TimeResolution = "Hourly"
                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                        TimeResolution = "Daily"
                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH" Then
                        TimeResolution = "BusyHour"
                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly" Then
                        TimeResolution = "Weekly"
                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeekBH" Then
                        TimeResolution = "WeekBH"
                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                        TimeResolution = "Raw"
                    ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                        TimeResolution = "Monthly"
                    End If
                Case Else
                    MsgBox("AssignData2Charts: problem in tech selection")
                    Exit Sub
            End Select

            If Not ch Is Nothing Then
                If TimeResolution = "Hourly" Or TimeResolution = "Raw" Then
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.FormatString = "HH:mm"
                    ch.XAxis.TimeScaleLabels.HourFormatString = "HH:mm"
                    ch.XAxis.TimeInterval = TimeInterval.Hours
                    ch.XAxis.TimeScaleLabels.DayFormatString = "ddd dd/MM/yy"
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Days)

                ElseIf TimeResolution = "Daily" Then
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                    ch.XAxis.TimeInterval = TimeInterval.Days
                    ch.XAxis.FormatString = "dd/MM/yy"
                    ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                    ch.XAxis.TimeInterval = TimeInterval.Days
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                    ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"

                ElseIf TimeResolution = "BusyHour" Then
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                    ch.XAxis.TimeInterval = TimeInterval.Days
                    ch.XAxis.FormatString = "dd/MM/yy"
                    ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
                    ch.XAxis.TimeInterval = TimeInterval.Days
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                    ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"

                ElseIf TimeResolution = "Weekly" Then
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                    ch.XAxis.TimeInterval = TimeInterval.Weeks
                    ch.XAxis.FormatString = "dd/MM/yy"
                    'ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM"
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Years)
                    'ch.XAxis.TimeScaleLabels.YearFormatString = "yyyy"
                    ch.XAxis.TimeIntervalAdvanced.StartDayOfWeek = CInt(DayOfWeek.Monday)

                ElseIf TimeResolution = "WeekBH" Then
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                    ch.XAxis.TimeInterval = TimeInterval.Months
                    ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM"
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Years)
                    ch.XAxis.TimeScaleLabels.YearFormatString = "yyyy"

                ElseIf TimeResolution = "Monthly" Then
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
                    ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
                    ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
                    ch.XAxis.TimeInterval = TimeInterval.Months
                    ch.XAxis.FormatString = "MMMM"
                    ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM"
                    'ch.XAxis.TimeInterval = TimeInterval.Months
                    ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
                    ch.XAxis.TimeScaleLabels.YearFormatString = "yyyy"
                End If
            End If
        Catch
        End Try
    End Sub

    Public Function MyJoinMethod(ByVal LeftTable As DataTable, ByVal RightTable As DataTable, ByVal LeftPrimaryColumn As String, ByVal RightPrimaryColumn As String) As DataTable
        'first create the datatable columns 
        Try
            Dim mydataSet As DataSet = New DataSet()
            mydataSet.Tables.Add("  ")
            Dim myDataTable As DataTable = mydataSet.Tables(0)

            'add left table columns 

            Dim dcLeftTableColumns(LeftTable.Columns.Count - 1) As DataColumn
            LeftTable.Columns.CopyTo(dcLeftTableColumns, 0)
            '  Console.WriteLine("LeftTable 1:  " & LeftTable.Columns(0).DataType.ToString)
            For Each LeftTableColumn As DataColumn In dcLeftTableColumns
                If Not myDataTable.Columns.Contains(LeftTableColumn.ColumnName.ToString()) Then
                    Dim dcol As DataColumn = New DataColumn(LeftTableColumn.ColumnName.ToString, LeftTableColumn.DataType)
                    myDataTable.Columns.Add(dcol)
                    If dcol.ColumnName.ToUpper = "DATE" Then
                        myDataTable.PrimaryKey = New DataColumn() {myDataTable.Columns("Date")}
                    End If
                End If
            Next
            '  Console.WriteLine("myTable:  " & myDataTable.Columns(0).DataType.ToString)

            'now add right table columns 
            Dim dcRightTableColumns(RightTable.Columns.Count - 1) As DataColumn
            RightTable.Columns.CopyTo(dcRightTableColumns, 0)

            For Each RightTableColumn As DataColumn In dcRightTableColumns
                If Not myDataTable.Columns.Contains(RightTableColumn.ToString()) Then
                    If (RightTableColumn.ToString() <> RightPrimaryColumn) Then
                        myDataTable.Columns.Add(RightTableColumn.ToString())
                    End If
                End If
            Next

            'add left-table data to mytable 
            ' Console.WriteLine("LeftTable:  " & LeftTable.Columns(0).DataType.ToString)
            For Each LeftTableDataRows As DataRow In LeftTable.Rows
                myDataTable.ImportRow(LeftTableDataRows)
            Next

            Dim var As ArrayList = New ArrayList() 'this variable holds the id's which have joined 
            ' Console.WriteLine(myDataTable.Columns(0).DataType.ToString)

            ' Dim myTableIDs As ArrayList = New ArrayList()
            ' myTableIDs = DataSetToArrayList(0, myDataTable)
            Dim LeftTableIDs As ArrayList = New ArrayList()
            LeftTableIDs = DataSetToArrayList(0, LeftTable)
            'Dim RightTableIDs As ArrayList = New ArrayList()
            ' RightTableIDs = DataSetToArrayList(0, RightTable)

            'import righttable which having not equal Id's with lefttable 

            For Each rightTableDataRows As DataRow In RightTable.Rows
                If (LeftTableIDs.Contains(rightTableDataRows(0))) Then

                    Dim wherecondition As String = "[" + myDataTable.Columns(0).ColumnName + "]='#" + rightTableDataRows(0).ToString() + "#'"
                    'Dim dr() As DataRow = myDataTable.Select(wherecondition)
                    Dim dr As DataRow = myDataTable.Rows.Find(rightTableDataRows(0))
                    Dim iIndex As Integer = myDataTable.Rows.IndexOf(dr)

                    For Each dc As DataColumn In RightTable.Columns
                        If dc.Ordinal <> 0 Then
                            myDataTable.Rows(iIndex)(dc.ColumnName.ToString().Trim()) = rightTableDataRows(dc.ColumnName.ToString().Trim())
                        End If
                    Next
                Else

                    Dim count As Integer = myDataTable.Rows.Count
                    Dim row As DataRow = myDataTable.NewRow()
                    row(0) = rightTableDataRows(0).ToString()
                    myDataTable.Rows.Add(row)

                    For Each dc As DataColumn In RightTable.Columns
                        If dc.Ordinal <> 0 Then
                            myDataTable.Rows(count)(dc.ColumnName.ToString().Trim()) = rightTableDataRows(dc.ColumnName.ToString().Trim()).ToString()
                        End If
                    Next
                End If
            Next

            Return myDataTable
        Catch ex As Exception
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)
            Return Nothing
        End Try
    End Function

    Public Function DataSetToArrayList(ByVal ColumnIndex As Integer, ByVal dataTable As DataTable) As ArrayList
        Dim output As ArrayList = New ArrayList()
        For Each row As DataRow In dataTable.Rows
            output.Add(row(ColumnIndex))
        Next
        Return output
    End Function

    Private Function SQL_Construct(ByVal objtype As String, ByVal aliastable As String, ByVal chartname As String, ByVal CrossTabObj As String) As String

        Dim tech As String = _strNetwork
        Dim sql_select As String = Nothing
        Dim sql_where_misc As String = ""
        Dim sql_where_object As String = ""
        Dim sql_where_tables As String = ""
        Dim sql_where_period As String = ""
        Dim sql_groupby As String = Nothing
        Dim sql_orderby As String = Nothing
        Dim sql_from_time As String = Nothing
        Dim sql_kpi As String = Nothing
        Dim sql_total As String = Nothing
        Dim connectionString As String = Nothing

        'setting fixed time interval of 8 days
        Dim startdate As Date = Nothing
        Dim enddate As Date = Nothing

        If dtPredefPeriod.Rows.Count > 0 Then
            startdate = CDate(dtPredefPeriod.Rows(0)("start_datetime"))
            enddate = CDate(dtPredefPeriod.Rows(0)("end_datetime"))
        Else
            startdate = CDate(dtChartStyleProperties.Rows(0)("ManualStartTime"))
            enddate = CDate(dtChartStyleProperties.Rows(0)("ManualEndTime"))
        End If

        Dim startdate_string As String = Chr(39) & startdate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
        Dim enddate_string As String = Chr(39) & enddate.ToString("yyyy-MM-dd HH:mm") & Chr(39)

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

            'if aliastable is nothing and chartname is not, then get the aliastable (used for sql building based on chart)
            If aliastable Is Nothing And Not chartname Is Nothing Then
                Dim dt_alias As DataTable = clsSQLCommands.GetConfigChartSourceTableData(connStrIOSServer, tech, chartname)
                aliastable = dt_alias(0)(0).ToString
                chartname = Nothing
            End If

            'objecttree selection to string
            Dim objectsel As String = dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString
            If dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString = "'PLMN'" Then objectsel = "IN('PLMN')"
            ObjectsCharted = "IN (" & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString & ")"

            Dim aggr_to As String = dtChartStyleProperties.Rows(0)("TargetType").ToString
            Dim aggr_from As String = Nothing

            Dim CMFilter As String = Nothing
            Dim RegionFilter As String = objectsel
            Dim tagid As String = Nothing

            If aggr_to = "TAGS" Then
                tagid = dtChartStyleProperties.Rows(0)("TagID").ToString
                ObjectsCharted = dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString
                objectsel = ObjectsCharted

                aggr_to = dtChartStyleProperties.Rows(0)("AggregateTo").ToString
                If dtChartStyleProperties.Rows(0)("AggregateTo").ToString.Contains("CM") Then
                    CMFilter = dtChartStyleProperties.Rows(0)("Tags_Filter").ToString
                End If
                If dtChartStyleProperties.Rows(0)("AggregateTo").ToString.Contains("Region") Then
                    RegionFilter = dtChartStyleProperties.Rows(0)("Tags_Filter").ToString
                End If
            End If

            'set purpose
            Dim purpose As String = "Charts"
            'If IsObjectAggregated = False Then
            '    purpose = "ObjectTime"
            'End If

            Dim StringForSourceTable As String = ""
            'get sql
            sql_sql = "SELECT * FROM qry_IOS_ConstructStatSQL WHERE (((tech)=" & Chr(39) & tech & Chr(39) & ") AND ((Purpose)=" & Chr(39) & purpose & Chr(39) & ") AND ((Aggregate_to)=" & Chr(39) & aggr_to & Chr(39) & ") AND ((ObjectType)=" & Chr(39) & objtype & Chr(39) & "));"
            comm_sql = New Odbc.OdbcCommand(sql_sql, conn_el)
            dr_sql = comm_sql.ExecuteReader
            sql_from_time = ""

            dr_sql.Read()
            If Not dr_sql.HasRows = 0 Then
                If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                    StringForSourceTable = "_HOUR"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                    StringForSourceTable = "_DAY"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH" Then
                    StringForSourceTable = "_BH"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly" Then
                    StringForSourceTable = "_WEEK"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH" Then
                    StringForSourceTable = "_WEEKBH"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                    StringForSourceTable = "_RAW"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                    StringForSourceTable = "_MONTH"
                End If

                For Each c As String In Split(dr_sql.GetValue(3).ToString.Trim, " ")
                    If c.ToUpper.Contains("PERIOD_START_TIME") Then
                        Dim aliasInPeriodStartTime As String = c.Split(".")(0)
                        'filterPeriodstring = Replace(filterPeriodstring, "@alias", aliasInPeriodStartTime)
                    End If
                Next
                sql_select = dr_sql("sql_select").ToString.Trim

                aggr_from = dr_sql("Aggregate_From").ToString.Trim

                If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                    sql_from_time = " " & dr_sql("sql_time_hour").ToString.Trim
                    connectionString = dr_sql("sql_time_hour_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                    sql_from_time = " " & dr_sql("sql_time_day").ToString.Trim
                    connectionString = dr_sql("sql_time_day_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH" Then
                    sql_from_time = " " & dr_sql("sql_time_bh").ToString.Trim
                    connectionString = dr_sql("sql_time_bh_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly" Then
                    sql_from_time = " " & dr_sql("sql_time_week").ToString.Trim
                    connectionString = dr_sql("sql_time_week_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH" Then
                    sql_from_time = " " & dr_sql("sql_time_weekbh").ToString.Trim
                    connectionString = dr_sql("sql_time_weekbh_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                    sql_from_time = " " & dr_sql("sql_time_raw").ToString.Trim
                    connectionString = dr_sql("sql_time_raw_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                    sql_from_time = " " & dr_sql("sql_time_month").ToString.Trim
                    connectionString = dr_sql("sql_time_month_connStr").ToString.Trim

                End If

                sql_where_misc = " " & dr_sql("sql_where_misc").ToString.Trim
                If dtChartStyleProperties.Rows(0)("TargetType").ToString = "PLMN" Then
                    'to complete
                Else
                    sql_where_object = " " & Replace(dr_sql("sql_where_object"), "@object", ObjectsCharted).ToString.Trim
                End If
                sql_where_tables = " " & dr_sql("sql_where_tables").ToString.Trim
                sql_where_period = " " & Replace(Replace(dr_sql("sql_where_period"), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim

                'If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                '    For Each c As String In Split(dr_sql("sql_groupby").ToString.Trim.ToString.Trim, " ")
                '        If c.ToUpper.Contains("PERIOD_START_TIME") Then
                '            If (StringForSourceTable = "_HOUR" Or StringForSourceTable = "_RAW") And dtChartStyleProperties.Rows(0)("Resolution").ToString <> "Daily" Then
                '                sql_groupby = sql_groupby + " DATEADD(hh, DATEDIFF(hh, 0, " & c & " ), 0) "
                '            Else
                '                sql_groupby = sql_groupby + " DATEADD(dd, DATEDIFF(dd, 0, " & c & " ), 0) "
                '            End If
                '        Else
                '            sql_groupby = sql_groupby + c + " "
                '        End If
                '    Next
                '    sql_groupby = " " + sql_groupby
                'Else
                sql_groupby = " " & dr_sql("sql_groupby").ToString.Trim
                '  End If

                'If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                '    For Each c As String In Split(dr_sql("sql_orderby").ToString.Trim, " ")
                '        If c.ToUpper.Contains("PERIOD_START_TIME") Then
                '            If (StringForSourceTable = "_HOUR" Or StringForSourceTable = "_RAW") And dtChartStyleProperties.Rows(0)("Resolution").ToString <> "Daily" Then
                '                sql_orderby = sql_orderby + " DATEADD(hh, DATEDIFF(hh, 0, " & c & " ), 0) "
                '            Else
                '                sql_orderby = sql_orderby + " DATEADD(dd, DATEDIFF(dd, 0, " & c & " ), 0) "
                '            End If
                '        Else
                '            sql_orderby = sql_orderby + c + " "
                '        End If
                '    Next
                'sql_orderby = " " + sql_orderby
                'Else
                sql_orderby = " " & dr_sql("sql_orderby").ToString.Trim
                'End If
            Else
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

            'Dim selected_tabs As String = tvKPIStats.GetKPIChecked2String(2, "ObjectName")
            'Dim selected_charts As String = tvKPIStats.GetKPIChecked2String(3, "ObjectName")
            'Dim selected_kpi As String = tvKPIStats.GetKPIChecked2String(4, "ObjectName")
            Dim supportcode As Integer = 0
            'get KPI sql
            conn_el = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()
            If aggr_from.Contains("BSC") Then supportcode = 1

            Dim crosstabkpisql As String = ""
            If Not CrossTabObj Is Nothing And Not CrossTabObj = "" Then
                crosstabkpisql = " AND IOS_Chart_Configuration.CrossTabObj='" & CrossTabObj & Chr(39)
            End If
            If chartname = Nothing Then
                sqlelement = "SELECT DISTINCT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object FROM IOS_Chart_Configuration " &
                             " INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID " &
                             " WHERE (IOS_Chart_Configuration.TechTab = " & Chr(39) & tech & Chr(39) & ")  AND (IOS_SQL_KPI.sourcetable = " & Chr(39) & aliastable & Chr(39) & ") AND supportcode >= " & supportcode & ";"
            Else
                sqlelement = "SELECT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object FROM IOS_Chart_Configuration " &
                             " INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID " &
                             " WHERE (IOS_Chart_Configuration.ChartName = " & Chr(39) & chartname & Chr(39) & " ) AND (IOS_Chart_Configuration.TechTab = " & Chr(39) & tech & Chr(39) & ")  AND " &
                             " (IOS_SQL_KPI.sourcetable= " & Chr(39) & aliastable & Chr(39) & ") AND (IOS_Chart_Configuration.ObjectTab = " & Chr(39) & objtype & Chr(39) & ") " &
                             " GROUP BY IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object, IOS_Chart_Configuration.CategoryTabIndex, IOS_Chart_Configuration.ChartIndex " &
                             " ORDER BY IOS_Chart_Configuration.CategoryTabIndex, IOS_Chart_Configuration.ChartIndex ;"
            End If

            '- add identification of primary keys if sourcetable has multiple tables and agg_from is diff from objtype
            If aggr_from <> objtype And aliastable.Split(",").Count > 1 Then
                Dim tableToIdentifyPrimKey As String = Replace(Replace(Split(Split(aliastable, ",")(0), ".").Last, "<AggregatedObject>", aggr_from).Trim, "_RAW", "_DAY")
                Dim dbToIdentifyPrimKey As String = Split(Split(aliastable, ",")(0), ".").First.Trim

                sqlelement = sqlelement & vbCrLf & "Select schema_name(tab.schema_id) as [schema_name],  tab.[name] as table_name,   pk.[name] as pk_name,   substring(column_names, 1, len(column_names)-1) as [columns]
                                                    from " + dbToIdentifyPrimKey + ".sys.tables tab
                                                    left outer join " + dbToIdentifyPrimKey + ".sys.indexes pk on tab.object_id = pk.object_id  and pk.is_primary_key = 1
                                                    cross apply (select col.[name] + ', '
                                                    from " + dbToIdentifyPrimKey + ".sys.index_columns ic
                                                    inner join " + dbToIdentifyPrimKey + ".sys.columns col on ic.object_id = col.object_id and ic.column_id = col.column_id
                                                    where ic.object_id = tab.object_id and ic.index_id = pk.index_id
                                                    order by col.column_id for xml path ('') ) D (column_names) where tab.[name] = '" + tableToIdentifyPrimKey + "' 
                                                    order by schema_name(tab.schema_id),
                                                    tab.[name];"
            End If

            comm_Element = New Odbc.OdbcCommand(sqlelement, conn_el)
            dr_element = comm_Element.ExecuteReader
            sql_kpi = ""
            Dim sourcetable As String = ""
            Dim joinobjs As String = ""
            Dim sql_kpi_builder As New System.Text.StringBuilder

            While dr_element.Read
                sql_kpi_builder.Append(" " + dr_element.GetValue(0).trim + ", ")
                'sql_kpi = sql_kpi + " " + dr_element.GetValue(0).trim + ", "
                sourcetable = dr_element.GetValue(1).trim
                If dtChartStyleProperties.Rows(0)("Resolution").ToString <> "Raw" And Not sourcetable Is Nothing Then
                    If sourcetable.Contains("_HOUR") Then
                        sourcetable = Replace(sourcetable, "_HOUR", StringForSourceTable) 'if _HOUR is base table in KPI then _HOUR must be replaced for day, bh, etc..
                    ElseIf sourcetable.Contains("_RAW") Then
                        sourcetable = Replace(sourcetable, "_RAW", StringForSourceTable) 'if _MNC1_RAW is base table, then _RAW must be replaced by day, bh, etc..
                    Else
                        If sourcetable.EndsWith("]") Then
                            sourcetable = sourcetable.Substring(0, Len(sourcetable) - 1) + StringForSourceTable + "]"
                        Else
                            sourcetable = sourcetable + StringForSourceTable
                        End If
                        If sourcetable.Contains("MNC1") Then sourcetable = Replace(sourcetable, "MNC1", aggr_from) 'and MNC1 with element
                    End If
                End If
                sourcetable = Replace(sourcetable, "<AggregatedObject>", aggr_from)
                aliastable = dr_element.GetValue(2).ToString.Trim
                joinobjs = dr_element.GetValue(3).ToString.Trim
            End While
            sql_kpi = sql_kpi_builder.ToString
            sql_kpi = sql_kpi.TrimEnd(" ")
            sql_kpi = sql_kpi.TrimEnd(",")

            If dr_element.NextResult AndAlso dr_element.HasRows Then
                joinobjs = Replace(dr_element.GetValue(3), " ", "")
            End If
            'building sourcetable for multi
            Dim sourcetable_final As String = ""
            'If Not sourcetable Is Nothing Then
            '	If sourcetable.Contains(",") Then
            '		For i As Integer = 0 To Split(sourcetable, ",").Count - 1
            '			sourcetable_final = sourcetable_final + Split(sourcetable, ",")(i) & " " & Split(aliastable, ",")(i) + ", "
            '		Next
            '		sourcetable_final = sourcetable_final.Substring(0, Len(sourcetable_final) - 2)
            '	Else
            '		sourcetable_final = sourcetable + " " + aliastable
            '	End If
            'End If
            If Not sourcetable Is Nothing Then
                If sourcetable.Contains(",") Then
                    'get first table name and first alias
                    Dim firstTable As String = Split(sourcetable, ",")(0)
                    Dim firstAlias As String = Split(aliastable, ",")(0)
                    sourcetable_final = firstTable + " " + firstAlias
                    For i As Integer = 1 To Split(sourcetable, ",").Count - 1
                        sourcetable_final = sourcetable_final + " inner join "
                        sourcetable_final = sourcetable_final + Split(sourcetable, ",")(i) & " " & Split(aliastable, ",")(i) + " ON "
                        For j = 0 To joinobjs.Split(",").Count - 1
                            sourcetable_final = sourcetable_final + firstAlias + "." + joinobjs.Split(",")(j) + "=" + aliastable.Split(",")(i) + "." + joinobjs.Split(",")(j)
                            If j < joinobjs.Split(",").Count - 1 Then
                                sourcetable_final = sourcetable_final + " AND "
                            End If
                        Next
                    Next
                    aliastable = Split(aliastable, ",")(0)
                Else
                    sourcetable_final = sourcetable + " " + aliastable
                End If
            End If

            'building jointable for multi
            Dim jointable As String = " "
            'If joinobjs.Contains(",") Then

            '	Dim firsttable As String = Split(aliastable, ",")(0)

            '	For Each obj As String In Split(joinobjs, ",")
            '		For i As Integer = 1 To Split(aliastable, ",").Count - 1
            '			jointable = jointable + firsttable + "." + obj + " = " + Split(aliastable, ",")(i) + "." + obj + " AND "
            '		Next
            '	Next
            '	jointable = " AND " & jointable.Substring(0, Len(jointable) - 4)
            '	aliastable = Split(aliastable, ",")(0)
            'End If

            Dim sql_crosstab_select As String = ""
            Dim sql_crosstab_groupby As String = ""
            If CrossTabObj <> "" Then
                sql_crosstab_select = "@alias." + CrossTabObj + ","
                sql_crosstab_groupby = ",@alias." + CrossTabObj
            End If

            'Closing and dereferencing
            comm_Element.Dispose()
            comm_Element = Nothing
            dr_element.Close()
            dr_element = Nothing
            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing

            sql_total = sql_select + sql_crosstab_select + sql_kpi + " " + sql_from_time + sql_where_misc + sql_where_object + sql_where_tables + sql_where_period + jointable + sql_groupby + sql_crosstab_groupby + sql_orderby

            sql_total = Replace(sql_total, "@sourcetable", sourcetable_final)
            sql_total = Replace(sql_total, "@alias", aliastable)
            sql_total = Replace(sql_total, "@object", ObjectsCharted)
            sql_total = Replace(sql_total, "@CrossTabObj", CrossTabObj)
            sql_total = Replace(sql_total, "@tablejoin", jointable)
            sql_total = Replace(sql_total, "@CMFilter", CMFilter)
            sql_total = Replace(sql_total, "@RegionFilter", RegionFilter)
            sql_total = Replace(sql_total, "= @TagID", tagid)
            sql_total = Replace(sql_total, "@TagID", tagid)

            Return sql_total

        Catch ex As Exception
            '  clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & vbCrLf & ex.InnerException.ToString)

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

#End Region

#Region "Object Time Chart"

    Private Sub CreateObjectTimeChart(ByRef objChartProp As ObjectChartProperties, objectName As String, ByRef objSlideProp As SlideProperties, ByRef presDoc As DocumentFormat.OpenXml.Packaging.PresentationDocument)
        Dim nc As New dotnetCHARTING.WinForms.Chart
        nc.Name = objectName
        nc.Width = objChartProp.Width
        nc.Height = objChartProp.Height
        'Chart Default Properties
        nc.DefaultElement.Marker.Visible = False
        nc.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        nc.LegendBox.DefaultEntry.Value = ""
        nc.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        nc.LegendBox.Visible = True
        nc.LegendBox.DefaultCorner = BoxCorner.Round

        nc.XAxis.TickLabelMode = TickLabelMode.Angled
        nc.XAxis.TickLabelAngle = 45
        nc.XAxis.Minimum = 0
        nc.XAxis.Maximum = 0

        nc.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        nc.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

        nc.ToolTip.InitialDelay = 1
        nc.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        nc.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        nc.CleanupPeriod = 1

        nc.TitleBox.Position = TitleBoxPosition.Full
        nc.TitleBox.CornerTopLeft = BoxCorner.Round
        nc.TitleBox.CornerTopRight = BoxCorner.Round
        nc.TitleBox.Label.AutoWrap = True
        nc.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"

        If Me.rptCurrOrConfig = "Current" Then
            Console.WriteLine("Getting chart from technology: " & objectName)
            nc = GetChartFromTechnology(objectName, _strNetwork, nc)
        ElseIf Me.rptCurrOrConfig = "Config" Then
            Console.WriteLine("Getting data for chart: " & objectName)
            ProcessStatsObjectTime(nc, objChartProp, objectName)
        End If

        'copy chart image to ppt slide
        Console.WriteLine("Copying image for chart: " & objectName)

        If slide IsNot Nothing AndAlso slidePart IsNot Nothing Then
            CopyChartBitmapToSlide(slide, slidePart, drawingObjectId, nc)
        Else
            Dim presPart As PresentationPart = presDoc.PresentationPart
            InsertNewSlide(presPart, slidePosition, objSlideProp.SlideTitle, objChartProp, nc)
            slidePosition = slidePosition + 1
        End If
    End Sub

    Private Sub ProcessStatsObjectTime(ByRef ch As dotnetCHARTING.WinForms.Chart, ByRef objChartProp As ObjectChartProperties, chartName As String)
        Try
            Dim dtStatsObjectTime As DataTable
            cm_Chart_kpiname = chartName.Replace("ObjectTime", "").Trim

            'Constructing STATS SQL
            '***********************
            Dim aggr_from As String = ""
            'aggr_from = tcTabControlHighStats.SelectedTabPage.Text

            'If aggr_from = "" Then
            '    lblObjTreeStatusStats.Text = "Choose Source Type!"
            '    lblObjTreeStatusStats.ForeColor = Color.Red
            '    Exit Sub
            'End If

            Dim sql_all As System.Collections.Specialized.StringCollection = New System.Collections.Specialized.StringCollection()
            Dim sql_crosstabobj As New List(Of String)
            Dim sql_tables As String
            'get list of tables

            'get KPI sql
            Dim conn_el As Odbc.OdbcConnection = New Odbc.OdbcConnection(connStrIOSServer)
            conn_el.ConnectionTimeout = 5
            conn_el.Open()

            sql_tables = clsSQLCommands.GetProcessStatsQuery(objChartProp.Technology, cm_Chart_kpiname)

            Dim comm_Element As Odbc.OdbcCommand = New Odbc.OdbcCommand(sql_tables, conn_el)
            Dim dr As Odbc.OdbcDataReader = comm_Element.ExecuteReader
            Dim sourcetable As String = ""
            Dim aliastable As String = ""
            Dim joinobjects As String = ""

            Dim lstOfSelectedCounterTypes As New List(Of String)
            'For Each srcbtn As SourceButton In flpSourceBtn_GetChecked(_strNetwork, flpCounterTypeStats)
            lstOfSelectedCounterTypes.Add(dtChartStyleProperties.Rows(0)("CounterType").ToString)
            'Next

            While dr.Read
                If lstOfSelectedCounterTypes.Contains(dr("Object").ToString.ToUpper) Then
                    sourcetable = nZ(dr.GetValue(0).ToString.Trim, "")
                    joinobjects = nZ(dr.GetValue(1).ToString.Trim, "")
                    sql_crosstabobj.Add(nZ(dr.GetValue(2), "").ToString.Trim)
                    aggr_from = nZ(dr.GetValue(3).ToString.Trim, "")
                    sql_all.Add(SQL_Construct_ObjectTime(aggr_from, sourcetable, cm_Chart_kpiname))
                End If
            End While

            conn_el.Close()
            conn_el.Dispose()
            conn_el = Nothing
            'loop through all tables

            'If sql_all.Count = 0 Then
            '    btnApplyStats.PerformClick() 'aborting
            '    Exit Sub
            'End If

            'connstring = connectionString
            'Threads_Fired = 0

            'ds_list.Clear()
            't_list.Clear()

            'Launching 3G stats threads
            '*****************************
            For Each sql_to_fire As String In sql_all
                'Dim stats_t As New Thread_Stats
                'stats_t.connstring = connstring
                'stats_t.sql_total = sql_to_fire
                'stats_t.ds_name = sql_crosstabobj(Threads_Fired)
                'AddHandler stats_t.ThreadComplete, AddressOf Process_Stats_ObjectTime_ThreadEnd

                'Dim thread_x = New Threading.Thread(AddressOf stats_t.GetData)
                'thread_x.Start()
                't_list.Add(thread_x)
                'Threads_Fired = Threads_Fired + 1
                dsStatsObjectTime = GetData(sql_to_fire)
                dtStatsObjectTime = CrossTab(dsStatsObjectTime.Tables(0), "DATE", objChartProp.TargetType.ToUpper, cm_Chart_kpiname)

                'Dim dtclone As DataTable = New DataTable
                'dtclone = dtStatsObjectTime.Copy
                'dtclone.TableName = cm_Chart_kpiname
                'If Not dsStatsObjectTime_Stored Is Nothing Then
                '    If dsStatsObjectTime_Stored.Tables.Contains(cm_Chart_kpiname) Then
                '        dsStatsObjectTime_Stored.Tables.Remove(cm_Chart_kpiname)
                '    End If
                'Else
                '    dsStatsObjectTime_Stored = New DataSet
                'End If
                'dsStatsObjectTime_Stored.Tables.Add(dtclone)
                Console.WriteLine("Assigning data to chart: " & chartName)
                AssignDataToObjectTime(ch, objChartProp, dtStatsObjectTime, cm_Chart_kpiname)
            Next

        Catch ex As Exception
            'Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message.ToString)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Function SQL_Construct_ObjectTime(ByVal objtype As String, ByVal aliastable As String, ByVal kpiname As String) As String
        Dim tech As String = _strNetwork
        Dim sql_select As String = Nothing
        Dim sql_where_misc As String = ""
        Dim sql_where_object As String = ""
        Dim sql_where_tables As String = ""
        Dim sql_where_period As String = ""
        Dim sql_groupby As String = Nothing
        Dim sql_orderby As String = Nothing
        Dim sql_from_time As String = Nothing
        Dim sql_kpi As String = Nothing
        Dim sql_total As String = Nothing
        Dim aggr_from As String = Nothing
        Dim connectionString As String = Nothing

        'setting fixed time interval of 8 days
        Dim startdate As Date = Nothing
        Dim enddate As Date = Nothing

        If dtPredefPeriod.Rows.Count > 0 Then
            startdate = CDate(dtPredefPeriod.Rows(0)("start_datetime"))
            enddate = CDate(dtPredefPeriod.Rows(0)("end_datetime"))
        Else
            startdate = CDate(dtChartStyleProperties.Rows(0)("ManualStartTime"))
            enddate = CDate(dtChartStyleProperties.Rows(0)("ManualEndTime"))
        End If

        Dim startdate_string As String = Chr(39) & startdate.ToString("yyyy-MM-dd HH:mm") & Chr(39)
        Dim enddate_string As String = Chr(39) & enddate.ToString("yyyy-MM-dd HH:mm") & Chr(39)

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

            'if aliastable is nothing and chartname is not, then get the aliastable (used for sql building based on chart)

            'objecttree selection to string
            Dim objectsel As String = ""

            objectsel = dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString
            If dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString = "PLMN" Then objectsel = "IN('PLMN')"
            ObjectsCharted = "IN (" & dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString & ")"

            'set aggr_to
            Dim aggr_to As String = dtChartStyleProperties.Rows(0)("TargetType").ToString
            Dim CMFilter As String = Nothing
            Dim tagid As String = Nothing
            Dim RegionFilter As String = ""
            If aggr_to = "TAGS" Then
                tagid = dtChartStyleProperties.Rows(0)("TagID").ToString
                ObjectsCharted = dtChartStyleProperties.Rows(0)("ObjectsSelected").ToString
                objectsel = ObjectsCharted

                aggr_to = dtChartStyleProperties.Rows(0)("AggregateTo").ToString
                If dtChartStyleProperties.Rows(0)("AggregateTo").ToString.Contains("CM") Then
                    CMFilter = dtChartStyleProperties.Rows(0)("Tags_Filter").ToString
                End If
                If dtChartStyleProperties.Rows(0)("AggregateTo").ToString.Contains("Region") Then
                    RegionFilter = dtChartStyleProperties.Rows(0)("Tags_Filter").ToString
                End If
            End If
            Dim purpose As String = "ObjectTime"

            Dim chartname As String = Nothing
            aggr_from = "%"

            'If objTechExportOption IsNot Nothing Then
            '    If ((Export2Excel = True) AndAlso (objTechExportOption.CellBasedExport = True)) Then
            '        aggr_from = objtype
            '        purpose = "ObjectTimeBreak"
            '    End If
            'End If

            Dim StringForSourceTable As String = ""
            'get sql
            sql_sql = clsSQLCommands.GetConstructStatsSQLObjectTime(tech, purpose, aggr_to, aggr_from, objtype)
            comm_sql = New Odbc.OdbcCommand(sql_sql, conn_el)
            dr_sql = comm_sql.ExecuteReader
            sql_from_time = ""

            dr_sql.Read()
            If Not dr_sql.HasRows = 0 Then
                sql_select = dr_sql("sql_select").ToString.Trim

                aggr_from = dr_sql("Aggregate_From").ToString.Trim
                If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                    sql_from_time = " " & dr_sql("sql_time_hour").ToString.Trim
                    connectionString = dr_sql("sql_time_hour_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                    sql_from_time = " " & dr_sql("sql_time_day").ToString.Trim
                    connectionString = dr_sql("sql_time_day_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH" Then
                    sql_from_time = " " & dr_sql("sql_time_bh").ToString.Trim
                    connectionString = dr_sql("sql_time_bh_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly" Then
                    sql_from_time = " " & dr_sql("sql_time_week").ToString.Trim
                    connectionString = dr_sql("sql_time_week_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH" Then
                    sql_from_time = " " & dr_sql("sql_time_weekbh").ToString.Trim
                    connectionString = dr_sql("sql_time_weekbh_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                    sql_from_time = " " & dr_sql("sql_time_raw").ToString.Trim
                    connectionString = dr_sql("sql_time_raw_connStr").ToString.Trim

                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                    sql_from_time = " " & dr_sql("sql_time_month").ToString.Trim
                    connectionString = dr_sql("sql_time_month_connStr").ToString.Trim
                End If

                If dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly" Then
                    StringForSourceTable = "_HOUR"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Daily" Then
                    StringForSourceTable = "_DAY"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "DailyBH" Then
                    StringForSourceTable = "_BH"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Weekly" Then
                    StringForSourceTable = "_WEEK"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "WeeklyBH" Then
                    StringForSourceTable = "_WEEKBH"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw" Then
                    StringForSourceTable = "_RAW"
                ElseIf dtChartStyleProperties.Rows(0)("Resolution").ToString = "Monthly" Then
                    StringForSourceTable = "_MONTH"
                End If

                sql_where_misc = " " & dr_sql("sql_where_misc").ToString.Trim
                If dtChartStyleProperties.Rows(0)("TargetType").ToString = "PLMN" Then
                    sql_where_object = " " & Replace(dr_sql("sql_where_object").ToString, "@object", " LIKE '%' ").ToString.Trim
                Else
                    sql_where_object = " " & Replace(dr_sql("sql_where_object").ToString, "@object", ObjectsCharted).ToString.Trim
                End If

                'If (Export2Excel = True) AndAlso (objectsel = "") Then
                '    If sql_where_misc.Contains("WHERE") Then
                '        sql_where_object = ""
                '    Else
                '        sql_where_object = " WHERE 1=1 "
                '    End If
                'End If

                sql_where_tables = " " & dr_sql("sql_where_tables").ToString.Trim

                'If cm_Chart_ObjectCompareOrMapKPI = "tsmi_Chart_LaunchObjTime" Then
                sql_where_period = " " & Replace(Replace(dr_sql("sql_where_period"), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim
                'ElseIf cm_Chart_ObjectCompareOrMapKPI = "tsmi_Chart_MapKPI" Or cm_Chart_ObjectCompareOrMapKPI = "tsmi_Chart_LaunchObj" Then
                '    startdate_string = Chr(39) & cm_Chart_MapKPI_Date.ToString("yyyy-MM-dd HH:mm") & Chr(39)
                '    enddate_string = Chr(39) & cm_Chart_MapKPI_Date.ToString("yyyy-MM-dd HH:mm") & Chr(39)
                '    sql_where_period = " " & Replace(Replace(dr_sql("sql_where_period"), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim & filterPeriodstring
                'Else
                '    sql_where_period = " " & Replace(Replace(dr_sql("sql_where_period"), "@starttime", startdate_string), "@endtime", enddate_string).ToString.Trim & filterPeriodstring
                'End If

                sql_groupby = " " & dr_sql("sql_groupby").ToString.Trim

                'If cm_Chart_ObjectCompareOrMapKPI = "tsmi_ChartCompareElementsGroupBy" Then
                '    sql_groupby = sql_groupby.Split(",")(0) & ", " & cm_Chart_GroupByName
                'End If

                sql_orderby = " " & dr_sql("sql_orderby").ToString.Trim
            Else
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
            Dim supportcode As Integer = 0


            If Not kpiname Is Nothing Then
                sqlelement = clsSQLCommands.GetSqlElementQueryReport(supportcode, tech, aliastable, kpiname)
            Else
                sqlelement = "SELECT IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object FROM IOS_Chart_Configuration " _
                       & " INNER JOIN IOS_SQL_KPI ON IOS_Chart_Configuration.SQLKPI_ID = IOS_SQL_KPI.SQLKPI_ID " _
                       & " WHERE (IOS_Chart_Configuration.ChartName = " & Chr(39) & chartname & Chr(39) & " ) AND (IOS_Chart_Configuration.TechTab = " & Chr(39) & tech & Chr(39) & ")  AND " _
                       & " (IOS_SQL_KPI.sourcetable= " & Chr(39) & aliastable & Chr(39) & ") AND (IOS_Chart_Configuration.ObjectTab = " & Chr(39) & objtype & Chr(39) & ") " _
                       & " GROUP BY IOS_SQL_KPI.KPI_SQL, IOS_SQL_KPI.sourcetable, IOS_SQL_KPI.tablealias, IOS_SQL_KPI.JoinObjects, IOS_SQL_KPI.Object, IOS_Chart_Configuration.CategoryTabIndex, IOS_Chart_Configuration.ChartIndex " _
                       & " ORDER BY IOS_Chart_Configuration.CategoryTabIndex, IOS_Chart_Configuration.ChartIndex"
            End If

            '- add identification of primary keys if sourcetable has multiple tables and agg_from is diff from objtype
            If aggr_from <> objtype And aliastable.Split(",").Count > 1 Then
                Dim tableToIdentifyPrimKey As String = Replace(Replace(Split(Split(aliastable, ",")(0), ".").Last, "<AggregatedObject>", aggr_from).Trim, "_RAW", "_DAY")
                Dim dbToIdentifyPrimKey As String = Split(Split(aliastable, ",")(0), ".").First.Trim

                sqlelement = sqlelement & vbCrLf & " select schema_name(tab.schema_id) as [schema_name],  tab.[name] as table_name,   pk.[name] as pk_name,   substring(column_names, 1, len(column_names)-1) as [columns]
                        from " + dbToIdentifyPrimKey + ".sys.tables tab
                        left outer join " + dbToIdentifyPrimKey + ".sys.indexes pk on tab.object_id = pk.object_id  and pk.is_primary_key = 1
                        cross apply (select col.[name] + ', '
                            from " + dbToIdentifyPrimKey + ".sys.index_columns ic
                            inner join " + dbToIdentifyPrimKey + ".sys.columns col on ic.object_id = col.object_id and ic.column_id = col.column_id
                            where ic.object_id = tab.object_id and ic.index_id = pk.index_id
                            order by col.column_id for xml path ('') ) D (column_names) where tab.[name] = '" + tableToIdentifyPrimKey + "' 
                    order by schema_name(tab.schema_id),
                        tab.[name];"
            End If

            comm_Element = New Odbc.OdbcCommand(sqlelement, conn_el)
            dr_element = comm_Element.ExecuteReader
            sql_kpi = ""
            Dim sourcetable As String = ""
            Dim joinobjs As String = ""

            While dr_element.Read
                sql_kpi = sql_kpi + " " + dr_element.GetValue(0).trim + ", "

                sourcetable = dr_element.GetValue(1).trim
                If dtChartStyleProperties.Rows(0)("Resolution").ToString <> "Raw" Then
                    If sourcetable.Contains("_HOUR") Then
                        sourcetable = Replace(sourcetable, "_HOUR", StringForSourceTable) 'if _HOUR is base table in KPI then _HOUR must be replaced for day, bh, etc..
                    ElseIf sourcetable.Contains("_RAW") Then
                        sourcetable = Replace(sourcetable, "_RAW", StringForSourceTable) 'if _MNC1_RAW is base table, then _RAW must be replaced by day, bh, etc..
                    Else
                        If sourcetable.EndsWith("]") Then
                            sourcetable = sourcetable.Substring(0, Len(sourcetable) - 1) + StringForSourceTable + "]"
                        Else
                            sourcetable = sourcetable + StringForSourceTable
                        End If
                    End If
                    If sourcetable.Contains("MNC1") Then sourcetable = Replace(sourcetable, "MNC1", aggr_from) 'and MNC1 with element
                End If
                sourcetable = Replace(sourcetable, "<AggregatedObject>", aggr_from)
                aliastable = dr_element.GetValue(2).ToString.Trim
                joinobjs = dr_element.GetValue(3).ToString.Trim
            End While
            sql_kpi = sql_kpi.TrimEnd(" ")
            sql_kpi = sql_kpi.TrimEnd(",")

            If dr_element.NextResult AndAlso dr_element.HasRows Then
                joinobjs = Replace(dr_element.GetValue(3), " ", "")
            End If

            Dim sourcetable_final As String = ""
            If Not sourcetable Is Nothing Then
                If sourcetable.Contains(",") Then
                    'get first table name and first alias
                    Dim firstTable As String = Split(sourcetable, ",")(0)
                    Dim firstAlias As String = Split(aliastable, ",")(0)
                    sourcetable_final = firstTable + " " + firstAlias
                    For i As Integer = 1 To Split(sourcetable, ",").Count - 1
                        sourcetable_final = sourcetable_final + " inner join "
                        sourcetable_final = sourcetable_final + Split(sourcetable, ",")(i) & " " & Split(aliastable, ",")(i) + " ON "
                        For j = 0 To joinobjs.Split(",").Count - 1
                            sourcetable_final = sourcetable_final + firstAlias + "." + joinobjs.Split(",")(j) + "=" + aliastable.Split(",")(i) + "." + joinobjs.Split(",")(j)
                            If j < joinobjs.Split(",").Count - 1 Then
                                sourcetable_final = sourcetable_final + " AND "
                            End If
                        Next
                    Next
                    aliastable = Split(aliastable, ",")(0)
                Else
                    sourcetable_final = sourcetable + " " + aliastable
                End If
            End If

            '         'building jointable for multi
            '         Dim jointable As String = " "

            '         'building sourcetable for multi
            '         Dim sourcetable_final As String = ""
            'If sourcetable.Contains(",") Then
            '	For i As Integer = 0 To Split(sourcetable, ",").Count - 1
            '		sourcetable_final = sourcetable_final + Split(sourcetable, ",")(i) & " " & Split(aliastable, ",")(i) + ", "
            '	Next
            '	sourcetable_final = sourcetable_final.Substring(0, Len(sourcetable_final) - 2)
            'Else
            '	sourcetable_final = sourcetable + " " + aliastable
            'End If

            ''building jointable for multi
            Dim jointable As String = " "
            'If joinobjs.Contains(",") Then

            '	Dim firsttable As String = Split(aliastable, ",")(0)

            '	For Each obj As String In Split(joinobjs, ",")
            '		For i As Integer = 1 To Split(aliastable, ",").Count - 1
            '			jointable = jointable + firsttable + "." + obj + " = " + Split(aliastable, ",")(i) + "." + obj + " AND "
            '		Next
            '	Next
            '	jointable = " AND " + jointable.Substring(0, Len(jointable) - 4)
            '	aliastable = Split(aliastable, ",")(0)
            'End If

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
            sql_total = Replace(sql_total, "@object", objectsel)
            sql_total = Replace(sql_total, "@tablejoin", jointable)
            sql_total = Replace(sql_total, "= @TagID", tagid)
            sql_total = Replace(sql_total, "@TagID", tagid)
            sql_total = Replace(sql_total, "@RegionFilter", RegionFilter)

            Return sql_total
        Catch ex As Exception
            'Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message.ToString)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message & " - " & ex.StackTrace)
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

    Public Function CrossTab(ByRef dtS As DataTable, ByVal leftColumn As String, ByVal topField As String, ByVal dataValue As String, Optional ByVal pFix As String = "F_") As DataTable
        Try
            If dtS Is Nothing Then
                Return Nothing
            End If

            Dim dtOut As New DataTable
            Dim dtRowTitle As New DataTable
            Dim dtColHeader As New DataTable
            dtRowTitle = dtS.DefaultView.ToTable(True, dtS.Columns(leftColumn).ColumnName)
            dtColHeader = dtS.DefaultView.ToTable(True, dtS.Columns(topField).ColumnName)

            Dim dColx As New DataColumn
            dColx.ColumnName = leftColumn
            dColx.Caption = leftColumn
            dColx.DataType = System.Type.GetType("System.DateTime")
            dtOut.Columns.Add(dColx)

            pFix = pFix.Replace(",", "_").Replace("'", "")

            For Each drow As DataRow In dtColHeader.Rows
                Dim dCol As New DataColumn
                dCol.DataType = GetType(Double)
                dCol.ColumnName = pFix & drow.Item(topField).ToString.Trim
                dtOut.Columns.Add(dCol)
            Next

            Dim drowx As DataRow
            For Each drow As DataRow In dtRowTitle.Rows
                drowx = dtOut.NewRow()
                drowx.Item(0) = drow.Item(leftColumn)
                dtOut.Rows.Add(drowx)
            Next

            Dim xVal As Int32 = 0
            Dim yVal As Int32 = 0

            For Each mRow As DataRow In dtS.Rows
                Dim xRowVal As String = mRow.Item(leftColumn).ToString
                Dim dataVal As String = mRow.Item(dataValue).ToString
                Dim yColVal As String = mRow.Item(topField).ToString.Trim

                For Each nRow As DataRow In dtOut.Select("[" & leftColumn & "]='" & xRowVal & "'") '.Rows
                    If xRowVal = nRow.Item(0).ToString Then
                        For xVal = 0 To nRow.Table.Columns.Count() - 1
                            If nRow.Table.Columns(xVal).ColumnName = pFix & yColVal Then
                                Dim rIndex As Int32 = dtOut.Rows.IndexOf(nRow)
                                dtOut.Rows(rIndex).Item(xVal) = dataVal
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
            Next

            dtOut.DefaultView.Sort = dtOut.Columns(0).ColumnName
            Return dtOut
        Catch ex As Exception
            'Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message.ToString)
            'clsSQLCommands.WriteReportLog(connStr, reportID, "Error: " & ex.Message.ToString)
        End Try
        Return Nothing
    End Function

    Private Sub AssignDataToObjectTime(ByRef ch As dotnetCHARTING.WinForms.Chart, ByRef objChartProp As ObjectChartProperties, ByRef dt As DataTable, ByVal Chartname As String)
        'Dim sqlchart As String
        Dim objectscharted As String = ""

        Dim ds_chart As DataSet = clsSQLCommands.GetChartConfigurationByElementAndChart(connStrIOSServer, objChartProp.Technology, cm_Chart_kpiname, Chartname)
        Dim dt_chart As DataTable = ds_chart.Tables(0)

        'Assign data to object time charts
        '*********************************
        Dim i As Integer
        Dim Y1axislabel As String
        Dim Y2axislabel As String
        Dim Y1axisAbsorPerc, Y2axisAbsOrPerc As String
        Dim Y1axisPrecision, Y2axisPrecision As Integer
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim sp As New SmartPalette()
        Dim sc As New SeriesCollection
        Dim lastchart As String = ""

        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim chart_YaxisScale() As String = {"0", "0"}
        Dim j As Integer = 0
        Dim rownum As Integer = 0

        For rownum = 0 To 0
            Try
                'collecting elements from chart confguration
                Dim drow As DataRow = dt_chart.Rows(rownum)

                'configures individual chart when new chartline is detected
                If lastchart = "" Or lastchart <> drow(5).ToString Then
                    lastchart = drow(5).ToString.Trim
                    sp.Clear()

                    Y1axisAbsorPerc = nZ(drow(13), "Abs")
                    Y2axisAbsOrPerc = nZ(drow(14), "Abs")

                    Y1axisPrecision = CInt(nZ(drow(15), "0"))
                    Y2axisPrecision = CInt(nZ(drow(16), "0"))

                    Y1axislabel = nZ(drow(11), " ")
                    Y2axislabel = nZ(drow(12), " ")

                    chart_elementsYAxis(0) = nZ(drow(9), " ")

                    SetChartXAxis(objChartProp.Technology, objectscharted, ch)

                    ch.Annotations.Clear()
                    ch.Annotations.Add(New Annotation(objChartProp.Technology.ToUpper))
                    ch.TitleBox.HeaderLabel.Text = drow(6).Trim & "   -   CLUSTER" & "  -   KPI: " & cm_Chart_kpiname

                    ch.TitleBox.Label.Alignment = StringAlignment.Near
                    ch.TitleBox.Label.LineAlignment = StringAlignment.Near

                    If (dtChartStyleProperties.Rows(0)("Resolution").ToString = "Hourly") Or (dtChartStyleProperties.Rows(0)("Resolution").ToString = "Raw") Then
                        ch.DefaultElement.Hotspot.ToolTip = "DATE: <%XValue,dd/MM/yy HH:mm>" & Chr(13) & "%SeriesName: %Value "
                    Else
                        ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
                    End If

                    Dim charttitle As String = drow(6).Trim

                    'Y-Axis Settingso   
                    If chart_elementsYAxis(i).Trim.ToUpper = "LEFT" Then
                        yaxis1 = New Axis
                        yaxis1.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                        yaxis1.Label.Text = Y1axislabel
                        If UCase(Y1axisAbsorPerc) = "ABS" Then
                            yaxis1.Percent = False
                        ElseIf UCase(Y1axisAbsorPerc) = "PERC" Then
                            yaxis1.Percent = True
                        End If
                        yaxis1.NumberPrecision = Y1axisPrecision
                        If yaxis1.NumberPrecision < 2 And Not yaxis1.Percent = True Then
                            yaxis1.MinimumInterval = 1
                        End If
                        yaxis1.Scale = dotnetCHARTING.WinForms.Scale.Range
                    Else
                        yaxis2 = New Axis
                        yaxis2.Orientation = dotnetCHARTING.WinForms.Orientation.Left
                        yaxis2.Label.Text = Y2axislabel
                        If UCase(Y2axisAbsOrPerc) = "PERC" Then
                            yaxis2.Percent = True
                        ElseIf UCase(Y2axisAbsOrPerc) = "ABS" Then
                            yaxis2.Percent = False
                        End If
                        yaxis2.NumberPrecision = Y2axisPrecision
                        If yaxis2.NumberPrecision < 2 And Not yaxis2.Percent = True Then
                            yaxis2.MinimumInterval = 1
                        End If
                        yaxis2.Scale = dotnetCHARTING.WinForms.Scale.Range
                    End If

                    '+++++++++++++++++++++++++++++++++++++++
                    j = 0

                    For Each col As DataColumn In dt.Columns
                        ReDim Preserve chart_elements(j)
                        If col.ColumnName.ToUpper <> "DATE" Then
                            chart_elements(j) = col.ColumnName.ToUpper
                            j = j + 1
                        End If

                    Next
                    For Each dr As DataRow In dt.Rows
                        For Each col As DataColumn In dt.Columns
                            If dr(col).ToString = "" Then
                                dr(col) = 0
                            End If
                        Next
                    Next

                    Dim de As DataEngine = New DataEngine(dt)
                    de.DataFields = String2DataFields(chart_elements, "Date")
                    sc = de.GetSeries()

                    Dim rnd As Random = New Random(10)

                    For i = 0 To sc.Count() - 1
                        sc(i).Type = SeriesType.Line
                        sc(i).Line.Width = 3
                        If yaxis1 IsNot Nothing Then
                            sc(i).YAxis = yaxis1
                        Else
                            sc(i).YAxis = yaxis2
                        End If
                        sc(i).DefaultElement.Color = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
                        sc(i).DefaultElement.Marker.Type = i
                    Next
                    ch.SeriesCollection.Clear()
                    ch.SeriesCollection.Add(sc)

                    HideChartScaleIfNoDataStats(ch, dt)

                    sc = Nothing
                    de = Nothing

                    ch.XAxis.Markers.Clear()
                    ch.RefreshChart()
                    ch.Visible = True
                    Dim tn As TreeNode = New TreeNode()
                    tn.Text = charttitle
                    tn.Tag = ch.Name

                    ReDim chart_elements(0)
                    ReDim chart_elementsYAxis(0)
                    ReDim chart_Eltype(0)
                    ReDim chart_ElColor(0)
                    ReDim chart_YaxisScale(1)
                    j = 0

                End If

            Catch ex As Exception
                'Console.WriteLine(ex.Message.ToString)
                'clsSQLCommands.WriteReportLog(connStrIOSServer, reportID, "Error: " & ex.Message.ToString)
            End Try
        Next
        dt_chart.Dispose()
        ds_chart.Dispose()
        dt_chart = Nothing
        ds_chart = Nothing
        If Not dt Is Nothing Then
            dt.Dispose()
        End If
    End Sub

    Public Sub HideChartScaleIfNoDataStats(ByRef ch As dotnetCHARTING.WinForms.Chart, ByRef dtData As DataTable)
        ch.XAxis.ScaleBreakStyle = ScaleBreakStyle.None
        ch.XAxis.ScaleBreaks.Clear()
        Dim sDate As Date = CDate(dtChartStyleProperties.Rows(0)("ManualStartTime"))
        Dim eDate As Date = CDate(dtChartStyleProperties.Rows(0)("ManualEndTime"))
        Dim dtAxis As DataTable = dtData.DefaultView.ToTable(True, dtData.Columns(0).ColumnName)
        While sDate <= eDate
            Dim dr() As DataRow = dtAxis.Select(dtData.Columns(0).ColumnName & "='" & sDate & "'")
            If dr.Length = 0 Then
                ch.XAxis.ScaleBreaks.Add(New dotnetCHARTING.WinForms.ScaleRange(sDate, sDate))
            End If
            'If xtcPSFilterStats.SelectedTabPage.Text = "Hours" Then
            '    sDate = sDate.AddHours(1)
            'Else
            sDate = sDate.AddDays(1)
            'End If
        End While
    End Sub

#End Region

#End Region

    Private Function CreatePowerPointSlide(ByVal slideStyle As SlideProperties, ByVal slideName As String, ByRef popwerPointPresentation As Powerpoint.Presentation, ByVal reportName As String, ByVal realUser As String) As Powerpoint.Slide
        Dim powerPointSlide As Powerpoint.Slide = Nothing
        Dim ReportingSlideType As Integer = GetConfigClientKeyValue("ReportingSlideType")
        Dim slidetype As Integer = ReportingSlideType
        powerPointSlide = popwerPointPresentation.Slides.Add(slideStyle.SlideOrdinal + 1, slidetype)
        If slideStyle.SlideTitle <> "" Then
            powerPointSlide.Shapes.Title.TextFrame.TextRange.Text = slideStyle.SlideTitle
        Else
            powerPointSlide.Shapes.Title.TextFrame.TextRange.Text = slideName
        End If

        Try
            powerPointSlide.HeadersFooters.DateAndTime.Text = Format(Now, "yyyy-MM-dd").ToString
            powerPointSlide.HeadersFooters.Footer.Text = realUser & " / " & reportName
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
        Return powerPointSlide
    End Function

    Public Shared Function ResizeImage(ByVal image As Image, ByVal size As Size) As Image
        Dim newWidth As Integer = size.Width
        Dim newHeight As Integer = size.Height
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Private Sub CreateChartObjectOnSlide(ByVal chartProperties As ObjectChartProperties, ByVal objectName As String, ByRef slide As Powerpoint.Slide)
        ChartCopyToClipboard(objectName, chartProperties.Technology, New Size(chartProperties.Width, chartProperties.Height))
        Application.DoEvents()
        Dim ShapePasted As Microsoft.Office.Interop.PowerPoint.ShapeRange = slide.Shapes.PasteSpecial(Powerpoint.PpPasteDataType.ppPasteBitmap, Microsoft.Office.Core.MsoTriState.msoFalse, "", 0, "", Microsoft.Office.Core.MsoTriState.msoFalse)

        Try
            ShapePasted.Top = Convert.ToSingle(chartProperties.Top)
            ShapePasted.Left = Convert.ToSingle(chartProperties.Left)
        Catch ex As Exception
        End Try

        Try
            ShapePasted.Height = Convert.ToSingle(chartProperties.Height)
            ShapePasted.Width = Convert.ToSingle(chartProperties.Width)
        Catch ex As Exception
        End Try

        Dim scaling As Double = 1
        Try
            scaling = CDbl(chartProperties.ObjectScale)
            ShapePasted.ScaleHeight(scaling, Microsoft.Office.Core.MsoTriState.msoTrue)
            ShapePasted.ScaleWidth(scaling, Microsoft.Office.Core.MsoTriState.msoTrue)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CreateTextBoxObjectOnSlide(ByVal textBoxProperties As ObjectTextBoxProperties, ByVal objectName As String, ByRef slide As Powerpoint.Slide)
        Dim txtboxTopMargin As Integer = Convert.ToSingle(textBoxProperties.Top)
        Dim fontCol As System.Drawing.Color = textBoxProperties.FontColor
        Dim borderCol As System.Drawing.Color = textBoxProperties.BoderColor
        Dim shTextBox As Microsoft.Office.Interop.PowerPoint.Shape = slide.Shapes.AddTextbox(1, textBoxProperties.Left, textBoxProperties.Top, textBoxProperties.Width, textBoxProperties.Height) '***4  

        With shTextBox
            .TextFrame.AutoSize = Powerpoint.PpAutoSize.ppAutoSizeNone
            .AutoShapeType = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRoundedRectangle
            .TextFrame.TextRange.Text = If(String.IsNullOrEmpty(textBoxProperties.TextBoxText), objectName, textBoxProperties.TextBoxText)
            .TextFrame.TextRange.Font.Size = textBoxProperties.FontSize
            .TextFrame.TextRange.Font.Bold = If(textBoxProperties.IsBold, -1, 0)
            .TextFrame.TextRange.Font.Italic = If(textBoxProperties.IsItalic, -1, 0)
            .TextFrame.TextRange.Font.Underline = If(textBoxProperties.IsUnderline, -1, 0)
            .TextFrame.TextRange.Font.Color.RGB = RGB(fontCol.R, fontCol.G, fontCol.B)

            .Fill.Visible = Microsoft.Office.Core.MsoTriState.msoTrue
            .Fill.Solid()
            .Fill.ForeColor.RGB = RGB(Color.Transparent.R, Color.Transparent.G, Color.Transparent.B)
            .Line.Weight = textBoxProperties.BorderSize '2.0#
            .Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue
            .Line.ForeColor.RGB = RGB(borderCol.R, borderCol.G, borderCol.B)
        End With
    End Sub

    Private Sub tsmi_ReportLock_Click(sender As Object, e As EventArgs) Handles tsmi_ReportLock.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode
            If Not nd Is Nothing Then
                If System.Environment.UserName.ToString.ToLower = nd("ReportOwner").Trim.ToLower Then
                    Dim locked As Integer = 0
                    If nd("ReportLocked").ToString.ToUpper = "TRUE" Then locked = 0 Else locked = 1
                    Dim parray()() As String = {
                        New String() {"@ReportID", nd.Tag},
                        New String() {"@ReportLocked", locked}
                    }
                    Dim sql As String = GetSQL(8510, parray)(1)
                    Dim connstring As String = GetSQL(8510, parray)(0)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                    btnRefresh_Click(Nothing, Nothing)
                    ExpandTree(nd("Report Name"), Nothing)
                    SetMessag("Report lock successfully modified.")
                Else
                    SetMessag("Sorry! You can't change report lock.")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ReportCopy_Click(sender As Object, e As EventArgs) Handles tsmi_ReportCopy.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim newReportName As String = Nothing
            newReportName = XtraInputBox.Show("Enter New Report Name", "Copy Report", "", MessageBoxButtons.OKCancel)

            If newReportName <> String.Empty Then
                newReportName = newReportName.Trim.Substring(0, System.Math.Min(49, newReportName.Trim.Length))
                Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
                Dim nd As TreeListNode = tlv.FocusedNode
                If Not nd Is Nothing Then
                    Dim parray()() As String = {
                        New String() {"@ReportID", CInt(nd.Tag)},
                        New String() {"@NewReportName", Chr(39) & newReportName & Chr(39)},
                        New String() {"@ReportOwner", Chr(39) & Environment.UserName.ToString & Chr(39)}
                    }
                    Dim connstring As String = GetSQL(8548, parray)(0)
                    Dim sql As String = GetSQL(8548, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                    btnRefresh_Click(Nothing, Nothing)
                    ExpandTree(newReportName, Nothing)
                    SetMessag("Report copied successfully.")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#Region "Dashboard PDF Report"

    Private Function CreateReport_DashboardPDF(reportID As Integer, reportName As String, ByRef reportFilePath As String) As Boolean
        ' Set report PDF name
        lstPdfFiles = New List(Of String)
        Dim tempFilePath As String = GetUserDataPath() & "\Data\"
        reportFilePath = GetUserDataPath() & "\Data\" & reportName & "_" & Format(Now(), "yyyyMMdd_HHmmss")

        Dim slideDistinct As DataTable = dtReports.Select("ReportID=" & reportID).CopyToDataTable.AsDataView.ToTable(True, "SlideID", "SlideName", "SlideOrdinal")

        If (slideDistinct.Rows.Count > 0) Then
            For Each drObject As DataRow In slideDistinct.Rows
                dtObjectsPerSlide = dtReports.Select("SlideID=" & Chr(39) & drObject("SlideID").ToString & Chr(39)).CopyToDataTable()

                If (dtObjectsPerSlide.Rows.Count > 0) Then

                    Dim dashboardXmlFile As String = Nothing
                    Dim dashboardName As String = Nothing
                    Dim dbTabPages As String = Nothing

                    Dim dtDashboard As DataTable = clsSQLCommands.GetDashboardFromID(connStrIOSServer, reportID, dtObjectsPerSlide.Rows(0)("DashboardID"))

                    Dim str = dtDashboard.Rows(0)("DashboardFile").ToString
                    dashboardName = dtDashboard.Rows(0)("DashboardName").ToString
                    dbTabPages = IIf(IsDBNull(dtDashboard.Rows(0)("SelectedPages")), "All", dtDashboard.Rows(0)("SelectedPages").ToString)

                    If str.Trim.Contains("<?xml") Then
                        dashboardXmlFile = str
                    Else
                        dashboardXmlFile = GetDecryptedConnectionString(str)
                    End If

                    Dim ms As New System.IO.MemoryStream()
                    ms = StringToStream(dashboardXmlFile)

                    ExportDashboardItemToPdf(ms, tempFilePath, dashboardName, dbTabPages)

                End If
            Next

            'merge dashboard parts pdf files into a single pdf file...
            WaitScreenReportEditor.ShowWaitScreen("Merging PDF Files...")
            Using pdfDocProcessor As New PdfDocumentProcessor()
                pdfDocProcessor.CreateEmptyDocument(reportFilePath & ".pdf")
                For Each pdfFile In lstPdfFiles
                    pdfDocProcessor.AppendDocument(pdfFile)
                Next
            End Using

            'delete dashboard parts pdf files...
            For Each pdfFile In lstPdfFiles
                File.Delete(pdfFile)
            Next

        End If
        Return True
    End Function

    Public Sub ExportDashboardItemToPdf(dashboardStream As Stream, outputFolder As String, dashboardName As String, dbPages As String)
        If Not Directory.Exists(outputFolder) Then
            Directory.CreateDirectory(outputFolder)
        End If

        ' Load dashboard from stream
        dshbrd = New Dashboard()
        AddHandler dshbrd.ConfigureDataConnection, AddressOf dashboard_ConfigureDataConnection
        dshbrd.LoadFromXml(dashboardStream)

        ' Create exporter
        Dim exporter As New DashboardExporter()
        AddHandler exporter.ConnectionError, AddressOf Exporter_ConnectionError
        AddHandler exporter.DataLoadingError, AddressOf Exporter_DataLoadingError
        AddHandler exporter.DashboardItemDataLoadingError, AddressOf Exporter_DashboardItemDataLoadingError

        ' Locate all TabContainers
        Dim tabContainers = dshbrd.Items.OfType(Of TabContainerDashboardItem)().ToList()

        Dim pdfOptions As DashboardPdfExportOptions = New DashboardPdfExportOptions With {
            .PageLayout = DashboardExportPageLayout.Landscape,
            .ExportParameters = True,
            .ExportFilters = True,
            .DashboardStatePosition = DashboardStateExportPosition.Below,
            .GridPrintHeadersOnEveryPage = True,
            .PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4,
            .ChartAutomaticPageLayout = True,
            .AutoFitPageCount = 1,
            .DocumentScaleMode = DashboardExportDocumentScaleMode.AutoFitToPagesWidth,
            .DashboardAutomaticPageLayout = True,
            .ShowTitle = DevExpress.Utils.DefaultBoolean.True
        }

        If tabContainers.Count = 0 Then
            ' No tab container → export entire dashboard
            Console.WriteLine("Started Exporting Dashboard: " & dashboardName)
            lstPdfFiles.Add(outputFolder & "\" & dashboardName & ".pdf")
            WaitScreenReportEditor.ShowWaitScreen("Generating: " & dashboardName)
            exporter.ExportToPdf(dshbrd, outputFolder & "\" & dashboardName & ".pdf",,, pdfOptions)
            Return
        End If

        Dim lstPages As List(Of String) = dbPages.Split(","c).Select(Function(s) s.Trim()).ToList()

        ' Iterate through each TabContainer
        For Each tabContainer In tabContainers

            For iCntr As Integer = 0 To tabContainer.TabPages.Count - 1

                Dim dashTabPage As DashboardTabPage = tabContainer.TabPages(iCntr)
                If dbPages.ToUpper = "ALL" Then
                    Console.WriteLine("Started Exporting Dashboard TabPage: " & dashTabPage.ComponentName)

                    lstPdfFiles.Add(outputFolder & "\" & dashboardName & iCntr & ".pdf")
                    WaitScreenReportEditor.ShowWaitScreen("Generating: " & dashboardName)
                    exporter.ExportDashboardItemToPdf(dshbrd, dashTabPage.ComponentName, outputFolder & "\" & dashboardName & iCntr & ".pdf",,, pdfOptions)
                ElseIf lstPages.Any(Function(s) s.Equals(dashTabPage.Name, StringComparison.OrdinalIgnoreCase)) Then
                    Console.WriteLine("Started Exporting Dashboard TabPage: " & dashTabPage.ComponentName)

                    lstPdfFiles.Add(outputFolder & "\" & dashboardName & iCntr & ".pdf")
                    WaitScreenReportEditor.ShowWaitScreen("Generating: " & dashboardName)
                    exporter.ExportDashboardItemToPdf(dshbrd, dashTabPage.ComponentName, outputFolder & "\" & dashboardName & iCntr & ".pdf",,, pdfOptions)
                End If

            Next
        Next
    End Sub

#End Region

#End Region

#Region "Slide ContextMenu"

    Private Sub tsmt_SlideRename_Click(sender As Object, e As EventArgs) Handles tsmt_SlideRename.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode
            If nd IsNot Nothing Then
                Dim NewName As String = InputBox("Enter New Name: ", "", nd("Slide Name").ToString)
                If Not nd Is Nothing And NewName <> "" Then
                    If NewName.Contains("WS") AndAlso NewName.Length >= 30 Then
                        NewName = NewName.Substring(0, System.Math.Min(30, NewName.Length))
                    End If
                    clsSQLCommands.UpdateReportSlideName(connStrIOSServer, nd.Tag, NewName)
                    btnRefresh_Click(Nothing, Nothing)
                    ExpandTree(NewName, nd.ParentNode.Tag)
                    nd.ParentNode.Expand()
                    SetMessag("Slide successfully renamed.")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmt_SlideDelete_Click(sender As Object, e As EventArgs) Handles tsmt_SlideDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            tlvReports.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode

            If Not nd Is Nothing Then
                clsSQLCommands.DeleteReportSlideName(connStrIOSServer, nd.Tag, nd.ParentNode.Tag)
                btnRefresh_Click(Nothing, Nothing)
                ExpandTree(nd.ParentNode("Report Name").ToString, Nothing)
                SetMessag("Slide successfully deleted.")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tlvReports.Cursor = Cursors.WaitCursor
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SlideMoveUp_Click(sender As Object, e As EventArgs) Handles tsmi_SlideMoveUp.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlv As TreeList = tlvReports
            Dim nd As TreeListNode = tlv.FocusedNode
            If Not (nd.PrevNode Is Nothing) Then
                If Not (nd Is Nothing) Then

                    clsSQLCommands.MoveReportSlide(connStrIOSServer, nd.Tag, nd.ParentNode.Tag, nd.PrevNode.Tag)
                    btnRefresh_Click(Nothing, Nothing)
                    ExpandTree(nd.GetDisplayText("Slide Name"), nd.ParentNode.Tag)
                    SetMessag("Slide successfully moved up.")
                End If
            Else
                SetMessag("No any previous node")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_SlideMoveDown_Click(sender As Object, e As EventArgs) Handles tsmi_SlideMoveDown.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlv As TreeList = tlvReports
            Dim nd As TreeListNode = tlv.FocusedNode
            If Not (nd.NextNode Is Nothing) Then
                If Not nd Is Nothing Then

                    clsSQLCommands.MoveReportSlide(connStrIOSServer, nd.Tag, nd.ParentNode.Tag, nd.NextNode.Tag)
                    btnRefresh_Click(Nothing, Nothing)
                    ExpandTree(nd.GetDisplayText("Slide Name"), nd.ParentNode.Tag)
                    SetMessag("Slide successfully moved down.")
                End If
            Else
                SetMessag("No any next node")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmitxt_NewTextbox_KeyDown(sender As Object, e As KeyEventArgs) Handles tsmitxt_NewTextbox.KeyDown
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If e.KeyCode = Keys.Enter Then
                Dim nd As TreeListNode = tlvReports.FocusedNode
                If Not nd Is Nothing Then
                    If Not (String.IsNullOrEmpty(tsmitxt_NewTextbox.Text.Trim)) Then
                        ObjectAddToSlide(nd.Tag, tsmitxt_NewTextbox.Text.Trim, 2, " ", tsmitxt_NewTextbox.Text.Trim, "", "", Now(), Now(), "", "", "", "", "", "", "", "", "", 0, "")
                        cmsReport.Close()
                        btnRefresh_Click(Nothing, Nothing)
                        ExpandTree(nd.GetDisplayText("Slide Name"), nd.ParentNode.Tag)
                        SetMessag("New textbox object successfully inserted.")
                    End If
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Object ContextMenu"

    Private Sub tsmi_ObjectRename_Click(sender As Object, e As EventArgs) Handles tsmi_ObjectRename.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode
            Dim NewName As String = InputBox("Enter New Object Name: ", "", nd("Object Name"))

            If Not nd Is Nothing And NewName <> "" Then
                clsSQLCommands.RenameReportObject(connStrIOSServer, NewName, nd.Tag)
                btnRefresh_Click(Nothing, Nothing)
                ExpandTree(nd.ParentNode.GetDisplayText("Report Name"), Nothing)
                SetMessag("Object Successfully renamed.")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ObjectChartDelete_Click(sender As Object, e As EventArgs) Handles tsmi_ObjectChartDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
            Dim nd As TreeListNode = tlv.FocusedNode
            If Not nd Is Nothing Then
                clsSQLCommands.DeleteChartObject(connStrIOSServer, nd.ParentNode.Tag, nd.Tag)
                clsSQLCommands.ManageStyle(connStrIOSServer, nd.ParentNode.Tag)
                btnRefresh_Click(Nothing, Nothing)
                ''ExpandTree(nd.ParentNode.Tag, Nothing)
                nd.ParentNode.Expand()
                nd.ParentNode.ParentNode.Expand()
                SetMessag("Object successfully deleted.")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ObjectChartMoveUp_Click(sender As Object, e As EventArgs) Handles tsmi_ObjectChartMoveUp.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
        Try
            Dim nd As TreeListNode = tlv.FocusedNode
            If Not nd Is Nothing Then

                tlv.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim objTag As String = nd.Tag
                If Not nd.PrevNode Is Nothing Then
                    clsSQLCommands.MoveSlideObject(connStrIOSServer, nd.ParentNode.Tag, nd.Tag, nd.PrevNode.Tag)
                    btnRefresh_Click(Nothing, Nothing)
                    ExpandTree(nd.GetDisplayText("Object Name"), nd.ParentNode.Tag)
                    ExpandTree(nd.ParentNode.ParentNode.GetDisplayText("Report Name"), Nothing)
                    tlvReports.FocusedNode = tlvReports.FindNode(Function(x) x.Tag = objTag)
                    SetMessag("Object successfully moved up")
                Else
                    SetMessag("Object cannot move up")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tlv.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_ObjectChartMoveDown_Click(sender As Object, e As EventArgs) Handles tsmi_ObjectChartMoveDown.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim tlv As TreeList = CType(cmsReport.SourceControl, TreeList)
        Try
            Dim nd As TreeListNode = tlv.FocusedNode
            If Not nd Is Nothing Then

                tlv.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim objTag As String = nd.Tag
                If Not nd.NextNode Is Nothing Then
                    clsSQLCommands.MoveSlideObject(connStrIOSServer, nd.ParentNode.Tag, nd.Tag, nd.NextNode.Tag)
                    btnRefresh_Click(Nothing, Nothing)
                    ExpandTree(nd.GetDisplayText("Object Name"), nd.ParentNode.Tag)
                    ExpandTree(nd.ParentNode.ParentNode.GetDisplayText("Report Name"), Nothing)
                    tlvReports.FocusedNode = tlvReports.FindNode(Function(x) x.Tag = objTag)
                    SetMessag("Object successfully moved down")
                Else
                    SetMessag("Object cannot move down")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            tlv.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#End Region

End Class
