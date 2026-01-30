Imports IOS.Library
Imports IOS.DataLibrary
Imports IOS.Configuration
Imports System.Xml.Schema
Imports DevExpress.XtraGrid.Views.Base
Imports Newtonsoft.Json
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports System.IO
Imports DevExpress.XtraEditors.Repository

Public Class frmGenerateXML

#Region "Variables"

    Private openFileDirectory As String = Nothing
    Private IsErrorInCopy As Boolean = False
    Private dtInputData As DataTable = Nothing
    Private ribeGetReportEnabled As Repository.RepositoryItemButtonEdit
    Private ribeGetReportDisabled As Repository.RepositoryItemButtonEdit
    Private xmlVendor As String = Nothing
    Private rimeServResp As RepositoryItemMemoEdit
    Private xmlJobID As Integer = Nothing

#End Region

#Region "Private Methods"

#Region "Huawei"

    Private Sub LoadXmlJobList()
        RemoveHandler gvXmlJobs.FocusedRowChanged, AddressOf gvXmlJobs_FocusedRowChanged
        RemoveHandler gvXmlJobs.RowCellStyle, AddressOf gvXmlJobs_RowCellStyle

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@Username", Chr(39) & Environment.UserName & Chr(39)}
        }
        strConnection = GetSQL(6500, parray)(0)
        sqlParam = GetSQL(6500, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcXmlJobs, gvXmlJobs, dt, "ALL", Nothing, Nothing)
            For Each gvc As GridColumn In gvXmlJobs.Columns
                If (gvc.FieldName = "Validated") Or (gvc.FieldName = "RESTAPIStatus") Then
                    gvc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                End If
            Next
            gvXmlJobs.FocusedRowHandle = gvXmlJobs.LocateByValue("XMLJobID", xmlJobID)
        Else
            IOSDevExpressGrid.ClearGrid(gcXmlJobs)
        End If

        AddHandler gvXmlJobs.FocusedRowChanged, AddressOf gvXmlJobs_FocusedRowChanged
        AddHandler gvXmlJobs.RowCellStyle, AddressOf gvXmlJobs_RowCellStyle
    End Sub

    Private Sub LoadValidationData()
        xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6501, parray)(0)
        sqlParam = GetSQL(6501, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcValidation, gvValidation, dt, "ALL", Nothing, Nothing)

            Dim dr() As DataRow = dt.Select("Check='# of Objects in Output'")
            If dr(0)("Result") > 500 Then
                btnProvision.Enabled = False
                btnExecute.Enabled = False
                btnRollbackProvision.Enabled = False
                btnExecuteRollback.Enabled = False
            Else
                btnProvision.Enabled = True
                btnExecute.Enabled = True
                btnRollbackProvision.Enabled = True
                btnExecuteRollback.Enabled = True
            End If
        Else
            IOSDevExpressGrid.ClearGrid(gcValidation)
            btnProvision.Enabled = True
            btnExecute.Enabled = True
            btnRollbackProvision.Enabled = True
            btnExecuteRollback.Enabled = True
        End If
    End Sub

    Private Sub LoadInputData()
        xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6502, parray)(0)
        sqlParam = GetSQL(6502, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        RemoveHandler gcInputData.ProcessGridKey, AddressOf InputDataGrid_ProcessGridKey
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcInputData, gvInputData, dt, "ALL", {"XMLInputID"}, Nothing)
        Else
            IOSDevExpressGrid.ClearGrid(gcInputData)
        End If
        AddHandler gcInputData.ProcessGridKey, AddressOf InputDataGrid_ProcessGridKey
    End Sub

    Private Sub LoadOutputData()
        xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6503, parray)(0)
        sqlParam = GetSQL(6503, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcOutputData, gvOutputData, dt, "ALL", Nothing, Nothing)
        Else
            IOSDevExpressGrid.ClearGrid(gcOutputData)
        End If
    End Sub

    Private Sub LoadErrorsData()
        xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        Dim dt As DataTable = Nothing

        If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then

            parray = {
                New String() {"@XMLJobID", xmlJobID}
            }
            strConnection = GetSQL(6504, parray)(0)
            sqlParam = GetSQL(6504, parray)(1)
            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            If dt.Rows.Count > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(gcErrors, gvErrors, dt, "ALL", Nothing, Nothing)
            Else
                IOSDevExpressGrid.ClearGrid(gcErrors)
            End If

        ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then

            Dim dtVR As New DataTable
            parray = {
                New String() {"@XMLJobID", xmlJobID},
                New String() {"@JobStatus", Chr(39) & "VALIDATED" & Chr(39)}
            }
            strConnection = GetSQL(6530, parray)(0)
            sqlParam = GetSQL(6530, parray)(1)
            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            For Each dr As DataRow In dt.Rows

                Dim rawData As String = System.Text.Encoding.UTF8.GetString(dr("ValidationResult"))
                Dim lineData() As String = Split(rawData, Environment.NewLine)

                Dim row As DataRow = Nothing
                For iCntr = 0 To lineData.Count - 1
                    If iCntr = 0 Then
                        Dim strArray As String() = lineData(iCntr).Split(","c)
                        For Each sCol As String In strArray
                            If Not ColumnInDataTable(sCol, dtVR) Then
                                dtVR.Columns.Add(sCol, GetType(String))
                            End If
                        Next
                    Else
                        If lineData(iCntr) <> String.Empty Then
                            row = dtVR.NewRow()
                            row.ItemArray = SplitWithQuotes(lineData(iCntr), ",")
                            dtVR.Rows.Add(row)
                        End If
                    End If
                Next

                If Not ColumnInDataTable("XMLProvisionID", dtVR) Then
                    dtVR.Columns.Add("XMLProvisionID", GetType(Integer))
                End If
                If Not ColumnInDataTable("XMLFileID", dtVR) Then
                    dtVR.Columns.Add("XMLFileID", GetType(Integer))
                End If
                If Not ColumnInDataTable("ENMJobID", dtVR) Then
                    dtVR.Columns.Add("ENMJobID", GetType(Integer))
                End If
                If Not ColumnInDataTable("ENMJobExportID", dtVR) Then
                    dtVR.Columns.Add("ENMJobExportID", GetType(Integer))
                End If
                If Not ColumnInDataTable("Datetime", dtVR) Then
                    dtVR.Columns.Add("Datetime", GetType(String))
                End If
                If Not ColumnInDataTable("JobStatus", dtVR) Then
                    dtVR.Columns.Add("JobStatus", GetType(String))
                End If

                row("XMLProvisionID") = dr("XMLProvisionID")
                row("XMLFileID") = dr("XMLFileID")
                row("ENMJobID") = dr("ENMJobID")
                row("ENMJobExportID") = dr("ENMJobExportID")
                row("Datetime") = dr("Datetime")
                row("JobStatus") = dr("JobStatus")

                dtVR.AcceptChanges()
            Next

            If dtVR.Rows.Count > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(gcErrors, gvErrors, dtVR, "ALL", Nothing, Nothing)
            Else
                IOSDevExpressGrid.ClearGrid(gcErrors)
            End If

        End If
    End Sub

    Private Sub LoadProvisionResult()
        xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        Dim dt As DataTable = Nothing

        If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
            parray = {
                New String() {"@XMLJobID", xmlJobID}
            }
            strConnection = GetSQL(6523, parray)(0)
            sqlParam = GetSQL(6523, parray)(1)
            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            If dt.Rows.Count > 0 Then

                IOSDevExpressGrid.PopulateDataInGrid(gcProvisionResult, gvProvisionResult, dt, "ALL", {"XMLCMTaskID", "ReportPathUrlData"}, Nothing)

                Dim rimeProvResult As New Repository.RepositoryItemMemoEdit()
                rimeProvResult.ReadOnly = True
                rimeProvResult.Appearance.Options.UseTextOptions = True
                rimeProvResult.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                gcProvisionResult.RepositoryItems.Add(rimeProvResult)
                gvProvisionResult.Columns("ProvisionResult").ColumnEdit = rimeProvResult
                gvProvisionResult.OptionsView.RowAutoHeight = True

                Dim unbColumn As GridColumn = gvProvisionResult.Columns.AddField("GetReport")
                unbColumn.VisibleIndex = gvProvisionResult.Columns.Count
                unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean

                ribeGetReportEnabled = New Repository.RepositoryItemButtonEdit()
                ribeGetReportEnabled.TextEditStyle = TextEditStyles.HideTextEditor
                ribeGetReportEnabled.LookAndFeel.UseDefaultLookAndFeel = False
                ribeGetReportEnabled.LookAndFeel.SkinName = "DevExpress Style"
                ribeGetReportEnabled.LookAndFeel.SkinMaskColor = Color.Blue
                ribeGetReportEnabled.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
                ribeGetReportEnabled.Buttons(0).Enabled = True

                AddHandler ribeGetReportEnabled.ButtonClick, AddressOf btnGetReport_ButtonClick

                ribeGetReportDisabled = New Repository.RepositoryItemButtonEdit()
                ribeGetReportDisabled.TextEditStyle = TextEditStyles.HideTextEditor
                ribeGetReportDisabled.LookAndFeel.UseDefaultLookAndFeel = False
                ribeGetReportDisabled.LookAndFeel.SkinName = "DevExpress Style"
                ribeGetReportDisabled.LookAndFeel.SkinMaskColor = Color.Gray
                ribeGetReportDisabled.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
                ribeGetReportDisabled.Buttons(0).Enabled = False

                ribeGetReportEnabled.Buttons(0).Caption = "Get Report"
                ribeGetReportEnabled.Buttons(0).Kind = ButtonPredefines.Glyph
                gvProvisionResult.Columns("GetReport").ColumnEdit = ribeGetReportEnabled

                ribeGetReportDisabled.Buttons(0).Caption = ""
                ribeGetReportDisabled.Buttons(0).Kind = ButtonPredefines.Glyph
                gvProvisionResult.Columns("GetReport").ColumnEdit = ribeGetReportDisabled

                gvProvisionResult.Columns("ProvisionResult").Width = 500
                gvProvisionResult.Columns("ProvisionResult").OptionsColumn.FixedWidth = True
            Else
                IOSDevExpressGrid.ClearGrid(gcProvisionResult)
            End If

        ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
            parray = {
                New String() {"@XMLJobID", xmlJobID}
            }

            'New String() {"@JobStatus", Chr(39) & "EXECUTED" & Chr(39)}

            strConnection = GetSQL(6530, parray)(0)
            sqlParam = GetSQL(6530, parray)(1)
            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            If dt.Rows.Count > 0 Then
                Dim cols2Hide() As String = {"ENMJobExportID", "ValidationResult"}
                IOSDevExpressGrid.PopulateDataInGrid(gcProvisionResult, gvProvisionResult, dt, "ALL", cols2Hide, "ServiceResponse")

                rimeServResp = New RepositoryItemMemoEdit()
                rimeServResp.ReadOnly = True
                rimeServResp.Appearance.Options.UseTextOptions = True
                rimeServResp.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                gcProvisionResult.RepositoryItems.Add(rimeServResp)
                gvProvisionResult.Columns("ServiceResponse").ColumnEdit = rimeServResp
                gvProvisionResult.OptionsView.RowAutoHeight = True

            Else
                IOSDevExpressGrid.ClearGrid(gcProvisionResult)
            End If
        End If

    End Sub

    Private Sub btnGetReport_ButtonClick(sender As Object, e As ButtonPressedEventArgs)
        Try
            Dim xmlCMTaskID As Integer = CInt(gvProvisionResult.GetFocusedRowCellValue("XMLCMTaskID"))
            Dim reportData() As Byte = Nothing

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@XMLCMTaskID", xmlCMTaskID}
            }
            strConnection = GetSQL(6525, parray)(0)
            sqlParam = GetSQL(6525, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            If (Not dt Is Nothing) AndAlso (Not IsDBNull(dt.Rows(0)("ReportPathUrlResponse"))) Then

                Dim objSFD As New SaveFileDialog()
                objSFD.RestoreDirectory = False
                If openFileDirectory Is Nothing Then
                    objSFD.InitialDirectory = IO.Directory.GetCurrentDirectory()
                Else
                    objSFD.InitialDirectory = openFileDirectory
                End If

                objSFD.Filter = "ZIP|*.zip"
                objSFD.Title = "Save ZIP File"
                objSFD.ShowDialog()
                openFileDirectory = IO.Path.GetDirectoryName(objSFD.FileName)
                reportData = CType(dt.Rows(0)("ReportPathUrlResponse"), Byte())

                Dim fs As System.IO.FileStream
                fs = New System.IO.FileStream(objSFD.FileName, System.IO.FileMode.Create)
                fs.Write(reportData, 0, reportData.Length)
                fs.Close()

                XtraMessageBox.Show("Provision report downloaded successfully", "Provision Report Download", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Public Sub LoadXmlJobLog(xmlJobID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6508, parray)(0)
        sqlParam = GetSQL(6508, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcXmlLogMsgs, gvXmlLogMsgs, dt, "ALL", Nothing, "LogMessage")
        Else
            IOSDevExpressGrid.ClearGrid(gcXmlLogMsgs)
        End If
    End Sub

    Private Sub GenerateXMLFiles(xmlJobID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        'fetch all the potential xsd files to create xml files
        If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
            parray = {
                New String() {"@XMLJobID", xmlJobID}
            }
            strConnection = GetSQL(6511, parray)(0)
            sqlParam = GetSQL(6511, parray)(1)
            Dim dtXsdFiles As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            If dtXsdFiles IsNot Nothing AndAlso dtXsdFiles.Rows.Count > 0 Then
                For Each drXsdFile As DataRow In dtXsdFiles.Rows
                    'create xml data & save into sql
                    strConnection = Nothing
                    sqlParam = Nothing
                    parray = {
                        New String() {"@XmlJobID", xmlJobID},
                        New String() {"@XsdFile", Chr(39) & drXsdFile("xsdFile") & Chr(39)}
                    }
                    strConnection = GetSQL(6506, parray)(0)
                    sqlParam = GetSQL(6506, parray)(1)
                    DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, iQryTimeOut)
                Next
            End If
        ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
            strConnection = Nothing
            sqlParam = Nothing
            parray = {
                New String() {"@XmlJobID", xmlJobID}
            }
            strConnection = GetSQL(6528, parray)(0)
            sqlParam = GetSQL(6528, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, iQryTimeOut)
        End If
    End Sub

    Private Function GetXmlOutputDataForXmlJobID(ByVal xmlJobID As Integer) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6518, parray)(0)
        sqlParam = GetSQL(6518, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Function GetJsonStringFromXmlDoc(ByRef xmlDoc As Xml.XmlDocument) As String

        Dim strjson As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim strFinal As String = Nothing
        Dim json As String = JsonConvert.SerializeXmlNode(xmlDoc, Formatting.Indented, True)
        Dim jsonObject As Linq.JObject = JsonConvert.DeserializeObject(json)
        Dim nes As Linq.JToken = jsonObject.SelectToken("subsession")
        Dim neChildren As Linq.JEnumerable(Of Linq.JToken) = nes.Children(Of Linq.JToken)

        strjson.Append("{\""nes\"": [")

        If neChildren.ToList().Count = 1 Then

            Dim subNE As Linq.JToken = neChildren.ToList()(0)
            Dim subNEChildren As Linq.JEnumerable(Of Linq.JToken) = subNE.Children(Of Linq.JToken)

            For Each xyz As Linq.JToken In subNEChildren.ToList()

                Dim subXyz As Linq.JEnumerable(Of Linq.JToken) = xyz.Children(Of Linq.JToken)

                strjson.Append("{")

                Dim neid As String = subXyz.ToList()(0).ToString.ToString
                strjson.Append(neid.Replace("@neid", "neId").Replace("""", "\""") & ",")

                Dim operation As String = subXyz.ToList()(1).ToString
                strjson.Append(operation.Replace("@", "").Replace("""", "\""") & ",")

                Dim strModule As String = subXyz.ToList()(2).ToString

                strjson.Append("\""modules\"": [{")

                Dim modulePart As Linq.JToken = subXyz.ToList()(2)

                For Each submodule As Linq.JToken In modulePart.ToList()

                    Dim moi As Linq.JEnumerable(Of Linq.JToken) = submodule.Children(Of Linq.JToken)

                    strjson.Append(moi.ToList()(0).ToString.Replace("@", "").Replace("""", "\""") & ",")

                    For Each submoi As Linq.JToken In moi.ToList()(1)

                        Dim moiPart As String = Nothing
                        'If submoi.Count <> 1 Then
                        '    strjson &= "\""mois\"": "
                        '    moiPart = submoi.ToString.Replace(vbCrLf, "").Replace(" ", "")
                        '    moiPart = moiPart.Replace("@", "").Replace("""", "\""")

                        '    strjson &= moiPart
                        'Else
                        strjson.Append("\""mois\"": [")
                        moiPart = submoi.ToString.Replace(vbCrLf, "").Replace(" ", "")
                        moiPart = moiPart.Replace("@", "").Replace("""", "\""")

                        strjson.Append(moiPart & "]")
                        'End If

                    Next

                    strjson = strjson.Replace("[[", "[")
                    strjson = strjson.Replace("]]", "]")
                    strjson.Append("}]")

                Next

                strjson.Append("},")

            Next

            strFinal = strjson.ToString.TrimEnd(",")
            strFinal &= "]}"

        Else

            For Each subNE As Linq.JToken In neChildren.ToList()

                Dim subNEChildren As Linq.JEnumerable(Of Linq.JToken) = subNE.Children(Of Linq.JToken)

                For Each xyz As Linq.JToken In subNEChildren.ToList()

                    Dim subXyz As Linq.JEnumerable(Of Linq.JToken) = xyz.Children(Of Linq.JToken)

                    For Each m As Linq.JToken In subXyz.ToList()

                        strjson.Append("{")

                        Dim mn As Linq.JEnumerable(Of Linq.JToken) = m.Children(Of Linq.JToken)

                        Dim neid As String = mn.ToList()(0).ToString
                        strjson.Append(neid.Replace("@neid", "neId").Replace("""", "\""") & ",")

                        Dim operation As String = Nothing
                        If mn.ToList().Count = 1 Then
                            operation = subXyz.ToList()(1).ToString
                            strjson.Append(operation.Replace("@", "").Replace("""", "\""") & ",")
                            Continue For
                        Else
                            operation = mn.ToList()(1).ToString
                            strjson.Append(operation.Replace("@", "").Replace("""", "\""") & ",")
                        End If

                        Dim strModule As String = Nothing
                        If mn.ToList().Count = 1 Then
                            strModule = subXyz.ToList()(2).ToString
                            Continue For
                        Else
                            strModule = mn.ToList()(2).ToString
                        End If

                        strjson.Append("\""modules\"": [{")

                        Dim modulePart As Linq.JToken = Nothing
                        If mn.ToList().Count = 1 Then
                            modulePart = subXyz.ToList()(2)
                        Else
                            modulePart = mn.ToList()(2)
                        End If

                        For Each submodule As Linq.JToken In modulePart.ToList()

                            Dim moi As Linq.JEnumerable(Of Linq.JToken) = submodule.Children(Of Linq.JToken)

                            Dim sb_SubModule As System.Text.StringBuilder = New System.Text.StringBuilder()

                            sb_SubModule.Append(moi.ToList()(0).ToString.Replace("@", "").Replace("""", "\""") & ",")

                            For Each submoi As Linq.JToken In moi.ToList()(1)

                                Dim moiPart As String = Nothing

                                sb_SubModule.Append("\""mois\"": [")
                                moiPart = submoi.ToString.Replace(vbCrLf, "").Replace(" ", "")
                                moiPart = moiPart.Replace("@", "").Replace("""", "\""")

                                sb_SubModule.Append(moiPart & "]")
                                'End If

                            Next



                            sb_SubModule = sb_SubModule.Replace("[[", "[")
                            sb_SubModule = sb_SubModule.Replace("]]", "]")
                            sb_SubModule.Append("}]")

                            strjson.Append(sb_SubModule.ToString)
                        Next

                    Next

                    strjson.Append("},")

                Next

            Next

            strFinal = strjson.ToString.TrimEnd(",")
            strFinal &= "]}"

        End If

        Return strFinal
    End Function

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub Call_QueryCMDataTask()

        Dim canUserExecute As Boolean = False
        If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
            If configMgr.User.IsPowerUser = True Then
                canUserExecute = True
            Else
                XtraMessageBox.Show("Only the XML job owner or the power user can execute", "Execute XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                canUserExecute = False
                Exit Sub
            End If
        Else
            canUserExecute = True
        End If

        If (canUserExecute = True) Then

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtOutputXml As DataTable = GetXmlOutputDataForXmlJobID(xmlJobID)

            SetMessage(dtOutputXml.Rows.Count.ToString & " XML files to be provisioned for XML Job ID: " & xmlJobID)

            If Not dtOutputXml Is Nothing Then

                If dtOutputXml.Rows.Count > 0 Then
                    gvXmlJobs.SetFocusedRowCellValue("RESTAPIStatus", "RUNNING")
                    gcXmlJobs.Refresh()
                    Application.DoEvents()

                    For Each drXml As DataRow In dtOutputXml.Rows

                        If Not IsDBNull(drXml("JSONNewConfig")) Then
                            If bgWorker.CancellationPending = True Then
                                UpdateRestAPIStatus(xmlJobID, "KILLED")
                                Exit Sub
                            End If
                            RunQueryingCMDataTask(xmlJobID, CInt(drXml("XMLFileID")))
                        Else
                            Exit Sub
                        End If

                    Next

                End If

            End If

        End If

    End Sub

    Public Sub RunQueryingCMDataTask(ByVal xmlJobID As Integer, ByVal xmlFileID As Integer)
        Dim objXSR As New XmlServRef.XmlProvisionServiceSoapClient()
        Try
            Dim userToken As String = Nothing
            Dim jobStatus As String = Nothing
            Dim jobExecResult As String = Nothing
            Dim xmlCMTaskID As String = Nothing
            Dim qryCMTaskUri As String = Nothing
            Dim qryResponse As String = Nothing

            'service request for user access token
            Dim tokenRequestResult As String = objXSR.GetAuthorizeToken()
            Dim setCMDataResponse As String = Nothing

            If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then

                userToken = tokenRequestResult.Split("^")(1)
                Dim provisionID As String = UpdateProvisionStatus(xmlJobID, xmlFileID, userToken, "Request Token Received")

                'service request for set CM data
                setCMDataResponse = objXSR.SetCMData(userToken, xmlJobID, CStr(xmlFileID), provisionID)

                If setCMDataResponse.Split("^")(0).ToString.ToUpper = "OK" Then

                    xmlCMTaskID = setCMDataResponse.Split("^")(1).ToString
                    qryCMTaskUri = setCMDataResponse.Split("^")(2).ToString

                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Received Query CM Data Task URI: " & qryCMTaskUri & " For XML Job ID: " & xmlJobID)

                    jobStatus = "STARTED"
                    Do While jobStatus.ToUpper <> "COMPLETED"

                        If bgWorker.CancellationPending = True Then
                            UpdateRestAPIStatus(xmlJobID, "KILLED")
                            Exit Sub
                        End If

                        'sleep the running thread for 5 sec
                        Threading.Thread.Sleep(5000)
                        UpdateRestAPIStatus(xmlJobID, jobStatus.ToUpper)

                        qryResponse = objXSR.QueryCMDataTask(userToken, xmlJobID, xmlCMTaskID, qryCMTaskUri)

                        If qryResponse.Split("^")(0).ToString.ToUpper = "OK" Then

                            jobStatus = qryResponse.Split("^")(1).ToString()
                            jobExecResult = qryResponse.Split("^")(2).ToString()

                            'update status to xml job grid for user info
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, jobStatus, jobExecResult)

                            If jobStatus.Trim.ToUpper = "RUNNING" Then
                                If jobExecResult.Trim.ToUpper = "ACTIVE SUCCESS" Or jobExecResult.Trim.ToUpper = "ACTIVE FAIL" Then
                                    LoadXmlJobLog(xmlJobID)
                                    Exit Do
                                End If
                            End If

                            'Save report path url query reqult to database
                            If jobStatus.ToUpper = "COMPLETED" Then
                                Dim reportPathUrl As String = GetReportPathUrl(xmlCMTaskID)
                                objXSR.QueryCMDataTaskReportPathUrl(userToken, xmlJobID, xmlCMTaskID, reportPathUrl)
                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Queried Report Path URI: " & reportPathUrl & " For XML Job ID: " & xmlJobID)
                                Exit Do
                            Else
                                bgWorker.ReportProgress(0, xmlJobID & ":" & jobStatus.ToUpper & " - " & jobExecResult.ToUpper)
                            End If

                        Else
                            XtraMessageBox.Show("Query CM Data - Error occured: " & qryResponse.Split("^")(1).ToString, "Query CM Data Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Do
                        End If

                    Loop

                Else
                    XtraMessageBox.Show("Set CM Data - Error occured: " & setCMDataResponse.Split("^")(1).ToString, "Set CM Data Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    jobStatus = "FAILED"
                    jobExecResult = "FAILED"
                    qryResponse = "FAILED"
                End If
            Else
                userToken = tokenRequestResult.Split("^")(1)
            End If

        Catch ex As Exception
        Finally
            objXSR = Nothing
        End Try
    End Sub

    Private Function GetReportPathUrl(xmlCMTaskID As String) As String
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLCMTaskID", xmlCMTaskID}
        }
        strConnection = GetSQL(6524, parray)(0)
        sqlParam = GetSQL(6524, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt IsNot Nothing Then
            Return dt.Rows(0)("ReportPathUrl").ToString
        End If
        Return Nothing
    End Function

    Private Sub Call_QueryCMDataTask_RollBack()

        Dim canUserExecute As Boolean = False
        If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
            If configMgr.User.IsPowerUser = True Then
                canUserExecute = True
            Else
                XtraMessageBox.Show("Only the XML job owner or the power user can execute", "Execute XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                canUserExecute = False
                Exit Sub
            End If
        Else
            canUserExecute = True
        End If

        If (canUserExecute = True) Then

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtOutputXml As DataTable = GetXmlOutputDataForXmlJobID(xmlJobID)

            If Not dtOutputXml Is Nothing Then

                If dtOutputXml.Rows.Count > 0 Then
                    gvXmlJobs.SetFocusedRowCellValue("RESTAPIStatus", "RUNNING")
                    gcXmlJobs.Refresh()
                    Application.DoEvents()

                    For Each drXml As DataRow In dtOutputXml.Rows

                        If Not IsDBNull(drXml("JSONRollBack")) Then
                            RunQueryingCMDataTaskRollBack(xmlJobID, CInt(drXml("XMLFileID")))
                        Else
                            Exit Sub
                        End If

                    Next

                End If

            End If

        End If

    End Sub

    Public Sub RunQueryingCMDataTaskRollBack(ByVal xmlJobID As Integer, ByVal xmlFileID As Integer)
        Dim objXSR As New XmlServRef.XmlProvisionServiceSoapClient()
        Try
            Dim userToken As String = Nothing
            Dim jobStatus As String = Nothing
            Dim jobExecResult As String = Nothing
            Dim xmlCMTaskID As String = Nothing
            Dim qryCMTaskUri As String = Nothing
            Dim qryResponse As String = Nothing

            'service request for user access token
            Dim tokenRequestResult As String = objXSR.GetAuthorizeToken()
            Dim setCMDataResponse As String = Nothing

            If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then

                userToken = tokenRequestResult.Split("^")(1)
                Dim provisionID As String = UpdateProvisionStatus(xmlJobID, xmlFileID, userToken, "Request Token Received")

                'service request for set CM data
                setCMDataResponse = objXSR.SetCMDataRollBack(userToken, xmlJobID, CStr(xmlFileID), provisionID)

                If setCMDataResponse.Split("^")(0).ToString.ToUpper = "OK" Then

                    'web api URI for querying CM data task request
                    xmlCMTaskID = setCMDataResponse.Split("^")(1).ToString
                    qryCMTaskUri = setCMDataResponse.Split("^")(2).ToString

                    UpdateLogForXmlJobId(xmlJobID, "XML Provision RollBack - Received Query CM Data Task URI: " & qryCMTaskUri & " For XML Job ID: " & xmlJobID)

                    jobStatus = "STARTED"
                    Do While jobStatus.ToUpper <> "COMPLETED"

                        'sleep the ruuning thread for 5 sec
                        Threading.Thread.Sleep(5000)
                        UpdateRestAPIStatus(xmlJobID, jobStatus.ToUpper)

                        'service request loop for querying CM data task
                        qryResponse = objXSR.QueryCMDataTask(userToken, xmlJobID, xmlCMTaskID, qryCMTaskUri)

                        If qryResponse.Split("^")(0).ToString.ToUpper = "OK" Then

                            jobStatus = qryResponse.Split("^")(1).ToString()
                            jobExecResult = qryResponse.Split("^")(2).ToString()

                            'update status to mxl job grid for user info 
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, jobStatus, jobExecResult)

                            'Save report path url query reqult to database
                            If jobStatus.ToUpper = "COMPLETED" Then
                                Dim reportPathUrl As String = GetReportPathUrl(xmlCMTaskID)
                                objXSR.QueryCMDataTaskReportPathUrl(userToken, xmlJobID, xmlCMTaskID, reportPathUrl)
                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Queried Report Path URI: " & reportPathUrl & " For XML Job ID: " & xmlJobID)
                                Exit Do
                            Else
                                bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & jobStatus.ToUpper & " - " & jobExecResult.ToUpper)
                            End If

                        Else
                            XtraMessageBox.Show("Query CM Data - Error occured: " & qryResponse.Split("^")(1).ToString, "Query CM Data Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Do
                        End If

                    Loop

                Else
                    XtraMessageBox.Show("Set CM Data - Error occured: " & setCMDataResponse.Split("^")(1).ToString, "Set CM Data Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    jobStatus = "FAILED"
                    jobExecResult = "FAILED"
                    qryResponse = "FAILED"
                End If
            Else
                userToken = tokenRequestResult.Split("^")(1)
            End If

        Catch ex As Exception
            'jobStatus = "Error Occured"
        Finally
            objXSR = Nothing
        End Try
    End Sub

    Private Sub UpdateRestAPIStatus(xmlJobID As Integer, jobStatus As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XmlJobID", xmlJobID},
            New String() {"@RestAPIStatus", IIf(jobStatus <> "", Chr(39) & jobStatus.TrimEnd(".").ToUpper & Chr(39), "NULL")}
        }
        strConnection = GetSQL(6522, parray)(0)
        sqlParam = GetSQL(6522, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Function GetQueryCMTaskData(ByVal xmlJobID As Integer) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6520, parray)(0)
        sqlParam = GetSQL(6520, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Function GetXmlNewConfigData() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))}
        }
        strConnection = GetSQL(6509, parray)(0)
        sqlParam = GetSQL(6509, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Function GetXmlRollBackData() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))}
        }
        strConnection = GetSQL(6512, parray)(0)
        sqlParam = GetSQL(6512, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub SaveJsonNewConfig(ByVal xmlJobID As Integer)
        'fetch xml new config data from sql
        Dim dt As DataTable = GetXmlNewConfigData()

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows

                Dim xmlStr As String = dr("XMLNewConfig").ToString
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.LoadXml(xmlStr)

                Dim elmNE As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("NE")
                For Each node As Xml.XmlNode In elmNE
                    Dim attr As Xml.XmlAttributeCollection = node.Attributes
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("neversion")))
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("netype")))
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("xsi:type")))
                Next

                Dim elmModule As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("module")
                For Each node As Xml.XmlNode In elmModule
                    Dim attr As Xml.XmlAttributeCollection = node.Attributes
                    If attr.ItemOf("xsi:type").Name.ToLower = "xsi:type" Then
                        Dim moduleName As Xml.XmlAttribute = xmlDoc.CreateAttribute("moduleName")
                        moduleName.Value = attr.ItemOf("xsi:type").Value
                        node.Attributes.SetNamedItem(moduleName)
                        node.Attributes.Remove(attr.Remove(attr.ItemOf("xsi:type")))
                    End If
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("productversion")))
                Next

                Dim elmMOI As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("moi")
                For Each node As Xml.XmlNode In elmMOI
                    Dim attr As Xml.XmlAttributeCollection = node.Attributes

                    If attr.ItemOf("xsi:type").Name.ToLower = "xsi:type" Then
                        Dim mocName As Xml.XmlAttribute = xmlDoc.CreateAttribute("mocName")
                        mocName.Value = attr.ItemOf("xsi:type").Value
                        node.Attributes.SetNamedItem(mocName)
                        node.Attributes.Remove(attr.Remove(attr.ItemOf("xsi:type")))
                    End If

                    node.Attributes.Remove(attr.Remove(attr.ItemOf("modifier")))
                    Dim modifier As Xml.XmlAttribute = xmlDoc.CreateAttribute("modifier")
                    modifier.Value = "update"
                    node.Attributes.SetNamedItem(modifier)
                Next

                Dim json As String = GetJsonStringFromXmlDoc(xmlDoc)

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@JSONNewConfig", Chr(39) & json & Chr(39)},
                    New String() {"@XMLFileID", CInt(dr("XMLFileID"))}
                }
                strConnection = GetSQL(6516, parray)(0)
                sqlParam = GetSQL(6516, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                UpdateLogForXmlJobId(xmlJobID, "Completed Saving JSON For XML Job ID: " & xmlJobID)

            Next

        End If
    End Sub

    Private Sub SaveJsonRollBack(ByVal xmlJobID As Integer)
        'fetch xml rollback data from sql
        Dim dt As DataTable = GetXmlRollBackData()

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows

                Dim xmlRollBackStr As String = dr("XMLRollback").ToString

                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.LoadXml(xmlRollBackStr)

                Dim elmNE As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("NE")
                For Each node As Xml.XmlNode In elmNE
                    Dim attr As Xml.XmlAttributeCollection = node.Attributes
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("neversion")))
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("netype")))
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("xsi:type")))
                Next

                Dim elmModule As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("module")
                For Each node As Xml.XmlNode In elmModule
                    Dim attr As Xml.XmlAttributeCollection = node.Attributes
                    If attr.ItemOf("xsi:type").Name.ToLower = "xsi:type" Then
                        Dim moduleName As Xml.XmlAttribute = xmlDoc.CreateAttribute("moduleName")
                        moduleName.Value = attr.ItemOf("xsi:type").Value
                        node.Attributes.SetNamedItem(moduleName)
                        node.Attributes.Remove(attr.Remove(attr.ItemOf("xsi:type")))
                    End If
                    node.Attributes.Remove(attr.Remove(attr.ItemOf("productversion")))
                Next

                Dim elmMOI As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("moi")
                For Each node As Xml.XmlNode In elmMOI
                    Dim attr As Xml.XmlAttributeCollection = node.Attributes

                    If attr.ItemOf("xsi:type").Name.ToLower = "xsi:type" Then
                        Dim mocName As Xml.XmlAttribute = xmlDoc.CreateAttribute("mocName")
                        mocName.Value = attr.ItemOf("xsi:type").Value
                        node.Attributes.SetNamedItem(mocName)
                        node.Attributes.Remove(attr.Remove(attr.ItemOf("xsi:type")))
                    End If

                    node.Attributes.Remove(attr.Remove(attr.ItemOf("modifier")))
                    Dim modifier As Xml.XmlAttribute = xmlDoc.CreateAttribute("modifier")
                    modifier.Value = "update"
                    node.Attributes.SetNamedItem(modifier)
                Next

                Dim jsonRollBack As String = GetJsonStringFromXmlDoc(xmlDoc)

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@JSONRollBack", Chr(39) & jsonRollBack & Chr(39)},
                    New String() {"@XMLFileID", CInt(dr("XMLFileID"))}
                }
                strConnection = GetSQL(6517, parray)(0)
                sqlParam = GetSQL(6517, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                UpdateLogForXmlJobId(xmlJobID, "Completed Saving RollBack JSON For XML Job ID: " & xmlJobID)

            Next

        End If
    End Sub

    Public Sub SetQueryCMTaskJobStatus(xmlJobID As Integer, xmlFileID As Integer, status As String, execResult As String)
        Select Case status.ToUpper
            Case "INITIATING"
                SetMessage("Query CM Data Task Result: " & status & " For XMLFile ID: " & xmlFileID)
            Case "ABNORMAL"
                SetMessage("Query CM Data Task Result: " & status & " For XMLFile ID: " & xmlFileID)
            Case "SUSPENDED"
                SetMessage("Query CM Data Task Result: " & status & " For XMLFile ID: " & xmlFileID)
            Case "COMPLETED"
                SetMessage("Query CM Data Task Result: " & status & " For XMLFile ID: " & xmlFileID)
                SetMessage("Query CM Data Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "RUNNING"
                If execResult = "" Then
                    SetMessage("Query CM Data Task Result: " & status & " For XMLFile ID: " & xmlFileID)
                Else
                    SetMessage("Query CM Data Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
                End If
            Case "JOBCREATED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "XMLATTACHED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "UNPROCESSABLEENTITY"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "ERROR"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "JOBVALIDATING"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "JOBVALIDATED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "JOBPARSING"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "JOBPARSED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "JOBEXECUTING"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "JOBEXECUTED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "FILENOTATTACHED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "FILEATTACHING"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "IMPORTFAILED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case "PARSED"
                SetMessage("Ericsson CM Task Result: " & execResult & " For XMLFile ID: " & xmlFileID)
            Case Else
                SetMessage("Query CM Data Task Result: " & status & " For XMLFile ID: " & xmlFileID)

        End Select

        Application.DoEvents()

        'update xml job list with rest api status
        If status.ToLower = "initiating" Or status.ToLower = "abnormal" Or status.ToLower = "suspended" Then
            UpdateRestAPIStatus(xmlJobID, status)
        Else
            UpdateRestAPIStatus(xmlJobID, execResult)
        End If

        'update xml log 
        UpdateLogForXmlJobId(xmlJobID, "XML Provision - Query CM Data Task Response: " & status & " - " & execResult & " With File ID: " & xmlFileID & " For XML Job ID: " & xmlJobID)

        'reload xml jobs list
        'LoadXmlJobList()
        'gvXmlJobs.FocusedRowHandle = gvXmlJobs.LocateByValue("XMLJobID", xmlJobID)

        'reload provision result
        'LoadProvisionResult()
        'xtcSubTop.SelectedTabPageIndex = 4

        'reload xml job log
        'LoadXmlJobLog(xmlJobID)
    End Sub

    Private Sub DeleteInputDataBulk(xmlInputID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        parray = {
            New String() {"@XMLInputID", xmlInputID}
        }
        strConnection = GetSQL(6526, parray)(0)
        sqlParam = GetSQL(6526, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub RenameXmlJob(ByVal xmlJobID As Integer, ByVal xmlJobName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobName", Chr(39) & xmlJobName & Chr(39)},
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6527, parray)(0)
        sqlParam = GetSQL(6527, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadRightPanelGrids()
        If xtcSubTop.SelectedTabPageIndex = 0 Then
            LoadValidationData()
        ElseIf xtcSubTop.SelectedTabPageIndex = 1 Then
            LoadInputData()
        ElseIf xtcSubTop.SelectedTabPageIndex = 2 Then
            LoadOutputData()
        ElseIf xtcSubTop.SelectedTabPageIndex = 3 Then
            LoadErrorsData()
        ElseIf xtcSubTop.SelectedTabPageIndex = 4 Then
            LoadProvisionResult()
        End If
    End Sub

    Public Sub UpdateLogForXmlJobId(xmlJobID As Integer, logMessage As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XmlJobID", xmlJobID},
            New String() {"@LogMessage", Chr(39) & logMessage & Chr(39)}
        }
        strConnection = GetSQL(6521, parray)(0)
        sqlParam = GetSQL(6521, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Public Function UpdateProvisionStatus(ByVal xmlJObID As Integer, ByVal xmlFileID As Integer, ByVal accessToken As String, ByVal status As String) As String
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJObID},
            New String() {"@XMLFileID", xmlFileID},
            New String() {"@UserName", Chr(39) & Environment.UserName & Chr(39)},
            New String() {"@RequestToken", Chr(39) & accessToken & Chr(39)},
            New String() {"@Status", Chr(39) & status & Chr(39)},
            New String() {"@ProvisionMethod", IIf(xmlVendor.ToUpper = Vendor.HUAWEI.ToString, Chr(39) & "JSON" & Chr(39), Chr(39) & "XML" & Chr(39))}
        }
        strConnection = GetSQL(6515, parray)(0)
        sqlParam = GetSQL(6515, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut).Rows(0)(0).ToString
    End Function

#End Region

#Region "Ericsson"

    Private Sub Call_ImportXmlJobEricsson()
        Dim canUserExecute As Boolean = False
        If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
            If configMgr.User.IsPowerUser = True Then
                canUserExecute = True
            Else
                XtraMessageBox.Show("Only the XML job owner or the power user can execute", "Execute Import XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                canUserExecute = False
                Exit Sub
            End If
        Else
            canUserExecute = True
        End If

        If (canUserExecute = True) Then

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtOutputXml As DataTable = GetXmlOutputDataForXmlJobID(xmlJobID)

            If Not dtOutputXml Is Nothing Then

                If dtOutputXml.Rows.Count > 0 Then
                    gvXmlJobs.SetFocusedRowCellValue("RESTAPIStatus", "RUNNING")
                    gcXmlJobs.Refresh()
                    Application.DoEvents()

                    For Each drXml As DataRow In dtOutputXml.Rows

                        If Not IsDBNull(drXml("XMLNewConfig")) Then
                            If bgWorker.CancellationPending = True Then
                                UpdateRestAPIStatus(xmlJobID, "KILLED")
                                Exit Sub
                            End If
                            RunXmlImportJobEricsson(xmlJobID, CInt(drXml("XMLFileID")), CStr(drXml("RestAPI_URLKey")))
                        Else
                            Exit Sub
                        End If

                    Next

                End If

            End If

        End If

    End Sub

    Private Sub RunXmlImportJobEricsson(xmlJobID As Integer, xmlFileID As Integer, restAPIKey As String)
        Dim objXSR As New XmlServRef.XmlProvisionServiceSoapClient()
        Dim enmJobID As String = Nothing
        Try
            Dim userToken As String = Nothing
            Dim xmlCMTaskID As String = Nothing
            Dim enmJobExportID As String = Nothing
            Dim enmValidatonID As String = Nothing
            Dim attachFileResponse As String = Nothing
            Dim validateJobResponse As String = Nothing
            Dim executeJobResponse As String = Nothing
            Dim fileName As String = Nothing
            Dim validationResult As String = Nothing
            Dim provisionID As String = Nothing

            'service request for user access session cookie
            Dim tokenRequestResult As String = objXSR.LoginUserEricsson(restAPIKey)
            Dim createJobIdResponse As String = Nothing

            If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then

                userToken = tokenRequestResult.Split("^")(1)
                provisionID = UpdateProvisionStatus(xmlJobID, xmlFileID, userToken, "User Session Cookie Received")

                'service request to create a new job id
                createJobIdResponse = objXSR.CreateImportJobEricsson(restAPIKey, userToken, "XmlJob" & xmlJobID)

                If createJobIdResponse IsNot Nothing Then
                    If createJobIdResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then

                        enmJobID = createJobIdResponse.Split("^")(1).ToString
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBCREATED", "ENM JOB CREATED")
                        bgWorker.ReportProgress(0, xmlJobID & ":" & "ENM JOB CREATED")
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - Received New ENM Job ID: " & enmJobID & " For XML Job ID: " & xmlJobID)

                        'start attaching xml file for enm job
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILEATTACHING", "ATTACHING XML FILE...")
                        bgWorker.ReportProgress(0, xmlJobID & ":" & "ATTACHING XML FILE...")

                        'service request to attach an xml file with enm job id
                        attachFileResponse = objXSR.AttachXmlFileToImportJobEricsson(restAPIKey, userToken, enmJobID, xmlJobID, xmlFileID, "NewConfig")

                        If attachFileResponse Is Nothing Then
                            attachFileResponse = "FILE NOT ATTACHED"
                            UpdateRestAPIStatus(xmlJobID, attachFileResponse)
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILENOTATTACHED", attachFileResponse)
                            bgWorker.ReportProgress(0, xmlJobID & ":" & attachFileResponse)
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job File Atachment Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            Exit Sub
                        End If

                        If attachFileResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then
                            fileName = attachFileResponse.Split("^")(2).ToString
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "XMLATTACHED", "XML FILE ATTACHED")
                            bgWorker.ReportProgress(0, xmlJobID & ":" & "XML FILE ATTACHED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: " & fileName & " Attached To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            'service request to validate the import job
                            validateJobResponse = objXSR.ParseAndValidateImportJobEricsson(restAPIKey, userToken, enmJobID)

                        ElseIf attachFileResponse.Split("^")(0).ToString.ToUpper = "UNPROCESSABLE ENTITY" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "UNPROCESSABLEENTITY", "UNPROCESSABLE ENTITY")
                            bgWorker.ReportProgress(0, xmlJobID & ":" & "UNPROCESSABLE ENTITY")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: Attachment Falied To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            Exit Sub
                        ElseIf attachFileResponse.Split("^")(0).ToString.ToUpper = "ERROR" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "ERROR", "ERROR OCCURED")
                            bgWorker.ReportProgress(0, xmlJobID & ":" & "ERROR OCCURED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: Attachment Falied To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            Exit Sub
                        End If

                        'save provisioning data and get xml cm task id
                        xmlCMTaskID = SaveEricssonProvisionValidationResult(provisionID, xmlJobID, xmlFileID, enmJobID)

                        If validateJobResponse.Split("^")(0).ToString.ToUpper = "ACCEPTED" Then

                            Dim validResult As String = ""
                            Do While validResult <> "VALIDATED"

                                If bgWorker.CancellationPending = True Then
                                    UpdateRestAPIStatus(xmlJobID, "KILLED")
                                    Exit Sub
                                End If

                                'sleep the running thread for 2 sec
                                Threading.Thread.Sleep(2000)

                                Dim validationResponse As String = validateJobResponse.Split("^")(1).ToString
                                enmValidatonID = validationResponse.Split(",")(0).Split(":")(1)
                                validResult = objXSR.GetImportJobsEricssonValidationResponse(restAPIKey, userToken, xmlCMTaskID, enmValidatonID).Split("^")(1)

                                If validResult = "VALIDATED" Then

                                    executeJobResponse = ""
                                    Do While executeJobResponse.ToUpper <> "ACCEPTED"

                                        If bgWorker.CancellationPending = True Then
                                            UpdateRestAPIStatus(xmlJobID, "KILLED")
                                            Exit Sub
                                        End If

                                        'sleep the running thread for 2 sec
                                        Threading.Thread.Sleep(2000)

                                        UpdateRestAPIStatus(xmlJobID, "Job Validated")
                                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATED", "JOB VALIDATED")
                                        bgWorker.ReportProgress(0, xmlJobID & ":" & "JOB VALIDATED")
                                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validated Successfully For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                        If executeJobResponse.ToUpper = "" Then
                                            'service request to execute the import job
                                            executeJobResponse = objXSR.ExecuteImportJobEricsson(restAPIKey, userToken, enmJobID, xmlCMTaskID)
                                            If executeJobResponse.Split("^")(0).ToUpper = "ACCEPTED" Then

                                                UpdateRestAPIStatus(xmlJobID, "EXECUTED")
                                                SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "JOB EXECUTED")
                                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Executed Successfully For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                                                xtcSubTop.TabPages(3).Appearance.Header.BackColor = Nothing

                                                bgWorker.ReportProgress(0, xmlJobID & ":" & "JOB EXECUTED")
                                                Exit Do

                                            ElseIf executeJobResponse.ToUpper = "EXECUTING" Then

                                                UpdateRestAPIStatus(xmlJobID, "Job Executing")
                                                SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTING", "JOB EXECUTING")
                                                bgWorkerExecute.ReportProgress(0, xmlJobID & ":" & "JOB EXECUTING")
                                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Executing For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                            ElseIf executeJobResponse.Split("^")(0).ToUpper = "ERROR" Then

                                                If executeJobResponse.Split("^")(1).ToUpper = "CONFLICT" Then
                                                    Dim conflictMsg As String = executeJobResponse.Split("^")(2)
                                                    Dim jsonObject As Linq.JObject = JsonConvert.DeserializeObject(conflictMsg)
                                                    Dim errMsg = jsonObject.SelectToken("errors").Children().ToList()(0).Last().ToString.Split(":")(1).Trim

                                                    UpdateRestAPIStatus(xmlJobID, "EXECUTE JOB FAILED")
                                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTE JOB FAILED")
                                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - " & errMsg.ToString.Replace("""", "") & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                                                    Exit Sub
                                                End If

                                                UpdateRestAPIStatus(xmlJobID, "EXECUTE JOB FAILED")
                                                SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTE JOB FAILED")
                                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Execution Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                                bgWorker.ReportProgress(0, xmlJobID & ":" & "EXECUTE JOB FAILED")
                                                enmJobExportID = objXSR.GetImportJobsEricssonExportID(restAPIKey, userToken, enmJobID).ToString.Split("^")(1)

                                                If enmJobExportID IsNot Nothing Then
                                                    Dim enmJonExportResult As String = Nothing
                                                    enmJonExportResult = objXSR.DownloadImportJobsEricssonFile(restAPIKey, userToken, enmJobID, enmJobExportID, xmlCMTaskID)
                                                    Dim vrParts() As String = enmJonExportResult.ToString.Split("^")
                                                    If vrParts(1) = "ValidationResultSaved" Then
                                                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - " & vrParts(1) & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                                                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTION RESULT RECEIVED")
                                                        xtcSubTop.TabPages(3).Appearance.Header.BackColor = Color.DarkRed
                                                    End If
                                                End If

                                                Exit Do

                                            End If

                                        Else
                                            bgWorker.ReportProgress(0, xmlJobID & ":" & executeJobResponse.ToUpper & " - ERROR")
                                        End If

                                    Loop

                                ElseIf validResult = "PARSING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsing")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSING", "JOB PARSING")
                                    bgWorker.ReportProgress(0, xmlJobID & ":" & "JOB PARSING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsing For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                ElseIf validResult = "PARSED" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsed")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSED", "JOB PARSED")
                                    bgWorker.ReportProgress(0, xmlJobID & ":" & "JOB PARSED")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                    Exit Do
                                ElseIf validResult = "VALIDATING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Validating")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATING", "JOB VALIDATING")
                                    bgWorker.ReportProgress(0, xmlJobID & ":" & "JOB VALIDATING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validating For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                Else
                                    UpdateRestAPIStatus(xmlJobID, validResult)
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, validResult, validResult)
                                    bgWorker.ReportProgress(0, xmlJobID & ":" & validResult)
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validation Result: " & validResult & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                End If

                            Loop

                        Else
                            XtraMessageBox.Show("Validate Import Job - Error occured: " & validateJobResponse.Split("^")(1).ToString, "Validate Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            UpdateRestAPIStatus(xmlJobID, validateJobResponse.Split("^")(1).ToString)
                        End If

                        'service request to logout user
                        objXSR.LogoutUserEricsson(restAPIKey)
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                    Else
                        XtraMessageBox.Show("Create Import Job - Error occured: " & createJobIdResponse.Split("^")(1).ToString, "Create Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateRestAPIStatus(xmlJobID, createJobIdResponse.Split("^")(1).ToString)
                        attachFileResponse = "IMPORT FAILED"
                        validateJobResponse = "IMPORT FAILED"
                        executeJobResponse = "IMPORT FAILED"
                    End If
                Else
                    UpdateRestAPIStatus(xmlJobID, "ENM Job Creation Failed")
                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "ENMJOBFAILED", "ENM Job Failed")
                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ENM JOB FAILED")
                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Create Import Job Result: Failed For XML Job ID: " & xmlJobID)
                End If
            Else
                userToken = tokenRequestResult.Split("^")(1)
                UpdateRestAPIStatus(xmlJobID, userToken)
            End If

        Catch ex As Exception
            UpdateRestAPIStatus(xmlJobID, "IMPORT FAILED")
            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "IMPORTFAILED", "IMPORT FAILED")
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            objXSR.LogoutUserEricsson(restAPIKey)
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            objXSR = Nothing
        End Try
    End Sub

    Private Function SaveEricssonProvisionValidationResult(xmlProvID As Integer, xmlJobID As Integer, xmlFileID As Integer, enmJobID As Integer) As String
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLProvisionID", xmlProvID},
            New String() {"@XMLJobID", xmlJobID},
            New String() {"@XMLFileID", xmlFileID},
            New String() {"@ENMJobID", enmJobID}
        }
        strConnection = GetSQL(6529, parray)(0)
        sqlParam = GetSQL(6529, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut).Rows(0)(0).ToString
    End Function

    Private Function SaveNewConfigXmlFileEricsson(xmlJobID As Integer, xmlFileID As Integer) As String
        Dim dtNewConfig As DataTable = Nothing
        Dim fileName As String = Nothing
        Dim filePath As String = IOSAppConfigManage.GetSaveEricssonXmlFilePath

        'fetch xml new config data
        dtNewConfig = GetXmlNewConfigDataEricsson(xmlJobID, xmlFileID)

        If dtNewConfig IsNot Nothing AndAlso dtNewConfig.Rows.Count > 0 Then

            'save xml file
            For Each dr As DataRow In dtNewConfig.Rows
                fileName = "NewConfig" & CStr(xmlJobID)
                Dim xmlStr As String = dr("XMLNewConfig").ToString
                fileName = fileName & "_" & dr("RestAPI_URLKey") & ".xml"
                Dim xmlDoc As New Xml.XmlDocument()
                xmlDoc.LoadXml(xmlStr)
                xmlDoc.Save(filePath & "\" & fileName)
            Next

        End If
        Return fileName
    End Function

    Private Function GetXmlNewConfigDataEricsson(xmlJobID As Integer, xmlFileID As Integer) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID},
            New String() {"@XMLFileID", xmlFileID}
        }
        strConnection = GetSQL(6531, parray)(0)
        sqlParam = GetSQL(6531, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub Call_ImportXmlJobEricsson_Rollback()
        Dim canUserExecute As Boolean = False
        If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
            If configMgr.User.IsPowerUser = True Then
                canUserExecute = True
            Else
                XtraMessageBox.Show("Only the XML job owner or the power user can execute", "Execute Import XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                canUserExecute = False
                Exit Sub
            End If
        Else
            canUserExecute = True
        End If

        If (canUserExecute = True) Then

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtOutputXml As DataTable = GetXmlOutputDataForXmlJobID(xmlJobID)

            If Not dtOutputXml Is Nothing Then

                If dtOutputXml.Rows.Count > 0 Then
                    gvXmlJobs.SetFocusedRowCellValue("RESTAPIStatus", "RUNNING")
                    gcXmlJobs.Refresh()
                    Application.DoEvents()

                    For Each drXml As DataRow In dtOutputXml.Rows

                        If Not IsDBNull(drXml("XMLRollback")) Then
                            If bgWorkerRollBack.CancellationPending = True Then
                                UpdateRestAPIStatus(xmlJobID, "KILLED")
                                Exit Sub
                            End If
                            RunXmlImportJobEricsson_Rollback(xmlJobID, CInt(drXml("XMLFileID")), CStr(drXml("RestAPI_URLKey")))
                        Else
                            Exit Sub
                        End If

                    Next

                End If

            End If

        End If
    End Sub

    Private Sub RunXmlImportJobEricsson_Rollback(xmlJobID As Integer, xmlFileID As Integer, restAPIKey As String)
        Dim objXSR As New XmlServRef.XmlProvisionServiceSoapClient()
        Dim enmJobID As String = Nothing
        Try
            Dim userToken As String = Nothing
            Dim xmlCMTaskID As String = Nothing
            Dim enmJobExportID As String = Nothing
            Dim enmValidatonID As String = Nothing
            Dim attachFileResponse As String = Nothing
            Dim validateJobResponse As String = Nothing
            Dim executeJobResponse As String = Nothing
            Dim validationResult As String = Nothing
            Dim fileName As String = Nothing
            Dim provisionID As String = Nothing

            'service request for user access session cookie
            Dim tokenRequestResult As String = objXSR.LoginUserEricsson(restAPIKey)
            Dim createJobIdResponse As String = Nothing

            If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then

                userToken = tokenRequestResult.Split("^")(1)
                provisionID = UpdateProvisionStatus(xmlJobID, xmlFileID, userToken, "User Session Cookie Received")

                'service request to create a new job id
                createJobIdResponse = objXSR.CreateImportJobEricsson(restAPIKey, userToken, "XmlJob" & xmlJobID)

                If createJobIdResponse IsNot Nothing Then
                    If createJobIdResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then

                        enmJobID = createJobIdResponse.Split("^")(1).ToString
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBCREATED", "ENM JOB CREATED")
                        bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ENM JOB CREATED")
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - Received New ENM Job ID: " & enmJobID & " For XML Job ID: " & xmlJobID)

                        'start attaching xml file for enm job
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILEATTACHING", "ATTACHING XML FILE...")
                        bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ATTACHING XML FILE...")

                        'service request to attach an xml file with enm job id
                        attachFileResponse = objXSR.AttachXmlFileToImportJobEricsson(restAPIKey, userToken, enmJobID, xmlJobID, xmlFileID, "Rollback")

                        If attachFileResponse Is Nothing Then
                            attachFileResponse = "FILE NOT ATTACHED"
                            UpdateRestAPIStatus(xmlJobID, attachFileResponse)
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILENOTATTACHED", attachFileResponse)
                            bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & attachFileResponse)
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job File Atachment Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            Exit Sub
                        End If

                        If attachFileResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "XMLATTACHED", "ROLLBACK XML FILE ATTACHED")
                            bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ROLLBACK XML FILE ATTACHED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Rollback XML File: " & fileName & " Attached To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            'service request to validate the import job
                            validateJobResponse = objXSR.ParseAndValidateImportJobEricsson(restAPIKey, userToken, enmJobID)

                        ElseIf attachFileResponse.Split("^")(0).ToString.ToUpper = "UNPROCESSABLE ENTITY" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "UNPROCESSABLEENTITY", "UNPROCESSABLE ENTITY")
                            bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "UNPROCESSABLE ENTITY")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: Attachment Falied To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            Exit Sub
                        ElseIf attachFileResponse.Split("^")(0).ToString.ToUpper = "ERROR" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "ERROR", "ERROR OCCURED")
                            bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ERROR OCCURED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: Attachment Falied To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            Exit Sub
                        End If

                        'save provisioning data and get xml cm task id
                        xmlCMTaskID = SaveEricssonProvisionValidationResult(provisionID, xmlJobID, xmlFileID, enmJobID)

                        If validateJobResponse.Split("^")(0).ToString.ToUpper = "ACCEPTED" Then

                            Dim validResult As String = ""
                            Do While validResult <> "VALIDATED"

                                If bgWorkerRollBack.CancellationPending = True Then
                                    UpdateRestAPIStatus(xmlJobID, "KILLED")
                                    Exit Sub
                                End If

                                'sleep the running thread for 2 sec
                                Threading.Thread.Sleep(2000)

                                Dim validationResponse As String = validateJobResponse.Split("^")(1).ToString
                                enmValidatonID = validationResponse.Split(",")(0).Split(":")(1)
                                validResult = objXSR.GetImportJobsEricssonValidationResponse(restAPIKey, userToken, xmlCMTaskID, enmValidatonID).Split("^")(1)

                                If validResult = "VALIDATED" Then

                                    executeJobResponse = ""
                                    Do While executeJobResponse.ToUpper <> "ACCEPTED"

                                        If bgWorkerRollBack.CancellationPending = True Then
                                            UpdateRestAPIStatus(xmlJobID, "KILLED")
                                            Exit Sub
                                        End If

                                        'sleep the running thread for 2 sec
                                        Threading.Thread.Sleep(2000)

                                        UpdateRestAPIStatus(xmlJobID, "Job Validated")
                                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATED", "JOB VALIDATED")
                                        bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ERROR VALIDATED")
                                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validated Successfully For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                        If executeJobResponse.ToUpper = "" Then
                                            'service request to execute an import job
                                            executeJobResponse = objXSR.ExecuteImportJobEricsson(restAPIKey, userToken, enmJobID, xmlCMTaskID)
                                            If executeJobResponse.Split("^")(0).ToUpper = "ACCEPTED" Then

                                                UpdateRestAPIStatus(xmlJobID, "EXECUTED")
                                                SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "JOB EXECUTED")
                                                bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ERROR EXECUTED")
                                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Executed Successfully For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                                                xtcSubTop.TabPages(3).Appearance.Header.BackColor = Nothing

                                                bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & " - EXECUTED")
                                                Exit Do

                                            ElseIf executeJobResponse.ToUpper = "EXECUTING" Then

                                                UpdateRestAPIStatus(xmlJobID, "Job Executing")
                                                SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTING", "JOB EXECUTING")
                                                bgWorkerExecute.ReportProgress(0, xmlJobID & ":" & "JOB EXECUTING")
                                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Executing For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                            ElseIf executeJobResponse.Split("^")(0).ToUpper = "ERROR" Then

                                                If executeJobResponse.Split("^")(1).ToUpper = "CONFLICT" Then
                                                    Dim conflictMsg As String = executeJobResponse.Split("^")(2)
                                                    Dim jsonObject As Linq.JObject = JsonConvert.DeserializeObject(conflictMsg)
                                                    Dim errMsg = jsonObject.SelectToken("errors").Children().ToList()(0).Last().ToString.Split(":")(1).Trim

                                                    UpdateRestAPIStatus(xmlJobID, "EXECUTE JOB FAILED")
                                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTE JOB FAILED")
                                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - " & errMsg.ToString.Replace("""", "") & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                                                    Exit Sub
                                                End If

                                                UpdateRestAPIStatus(xmlJobID, "EXECUTE JOB FAILED")
                                                SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTE JOB FAILED")
                                                bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "EXECUTE JOB FAILED")
                                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Execution Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                                bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & " - FAILED")
                                                enmJobExportID = objXSR.GetImportJobsEricssonExportID(restAPIKey, userToken, enmJobID).ToString.Split("^")(1)

                                                If enmJobExportID IsNot Nothing Then
                                                    validationResult = objXSR.DownloadImportJobsEricssonFile(restAPIKey, userToken, enmJobID, enmJobExportID, xmlCMTaskID)
                                                    Dim vrParts() As String = validationResult.ToString.Split("^")
                                                    If vrParts(1) = "ValidationResultSaved" Then
                                                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - " & vrParts(1) & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTION RESULT RECEIVED")
                                                        xtcSubTop.TabPages(3).Appearance.Header.BackColor = Color.DarkRed
                                                    End If
                                                End If

                                                Exit Do

                                            End If
                                        Else
                                            bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & executeJobResponse.ToUpper & " - ERROR")
                                        End If

                                    Loop

                                ElseIf validResult = "PARSING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsing")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSING", "JOB PARSING")
                                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "JOB PARSING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsing For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                ElseIf validResult = "PARSED" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsed")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSED", "JOB PARSED")
                                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "JOB PARSED")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                    Exit Do
                                ElseIf validResult = "VALIDATING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Validating")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATING", "JOB VALIDATING")
                                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "JOB VALIDATING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validating For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                Else
                                    UpdateRestAPIStatus(xmlJobID, validResult)
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, validResult, validResult)
                                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & validResult)
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validation Result: " & validResult & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                End If

                            Loop

                        Else
                            XtraMessageBox.Show("Validate Import Job - Error occured: " & validateJobResponse.Split("^")(1).ToString, "Validate Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            UpdateRestAPIStatus(xmlJobID, validateJobResponse.Split("^")(1).ToString)
                        End If

                        'service request to logout user
                        objXSR.LogoutUserEricsson(restAPIKey)
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                    Else
                        XtraMessageBox.Show("Create Import Job - Error occured: " & createJobIdResponse.Split("^")(1).ToString, "Create Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateRestAPIStatus(xmlJobID, createJobIdResponse.Split("^")(1).ToString)
                        attachFileResponse = "IMPORT FAILED"
                        validateJobResponse = "IMPORT FAILED"
                        executeJobResponse = "IMPORT FAILED"
                    End If
                Else
                    UpdateRestAPIStatus(xmlJobID, "ENM Job Creation Failed")
                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "ENMJOBFAILED", "ENM Job Failed")
                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ENM JOB FAILED")
                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Create Import Job Result: Failed For XML Job ID: " & xmlJobID)
                End If
            Else
                userToken = tokenRequestResult.Split("^")(1)
                UpdateRestAPIStatus(xmlJobID, userToken)
            End If

        Catch ex As Exception
            UpdateRestAPIStatus(xmlJobID, "IMPORT FAILED")
            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "IMPORTFAILED", "IMPORT FAILED")
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            objXSR.LogoutUserEricsson(restAPIKey)
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            objXSR = Nothing
        End Try
    End Sub

    Private Function SaveRollbackXmlFileEricsson(xmlJobID As Integer, xmlFileID As Integer) As String
        Dim dtRollback As DataTable = Nothing
        Dim fileName As String = Nothing
        Dim filePath As String = IOSAppConfigManage.GetSaveEricssonXmlFilePath

        'fetch xml rollback data
        dtRollback = GetXmlRollBackDataEricsson(xmlJobID, xmlFileID)

        If dtRollback IsNot Nothing AndAlso dtRollback.Rows.Count > 0 Then

            'save rollback xml file
            For Each dr As DataRow In dtRollback.Rows
                fileName = "Rollback" & CStr(xmlJobID)
                Dim xmlRollBackStr As String = dr("XMLRollback").ToString
                fileName = fileName & "_" & dr("RestAPI_URLKey") & ".xml"
                Dim xmlDoc As New Xml.XmlDocument()
                xmlDoc.LoadXml(xmlRollBackStr)
                xmlDoc.Save(filePath & "\" & fileName)
            Next

        End If
        Return fileName
    End Function

    Private Function GetXmlRollBackDataEricsson(xmlJobID As Integer, xmlFileID As Integer) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID},
            New String() {"@XMLFileID", xmlFileID}
        }
        strConnection = GetSQL(6532, parray)(0)
        sqlParam = GetSQL(6532, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Function GetXmlEnmJobsForExecutionEricsson(xmlJobID As Integer) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@XMLJobID", xmlJobID}
        }
        strConnection = GetSQL(6533, parray)(0)
        sqlParam = GetSQL(6533, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

#Region "Partial Provision"

    Private Sub btnCreateValidate_Click(sender As Object, e As EventArgs) Handles btnCreateValidate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            bgWorkerPartial.WorkerReportsProgress = True
            bgWorkerPartial.RunWorkerAsync()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub bgWorkerPartial_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorkerPartial.DoWork
        Try
            If xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                Call_ImportXmlJobEricsson_PartialProvision()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgWorkerPartial_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorkerPartial.ProgressChanged, bgWorkerPartialRollback.ProgressChanged, bgWorkerExecute.ProgressChanged
        ', bgWorkerExecuteRollback.ProgressChanged
        Try
            xmlJobID = CInt(e.UserState.ToString.Split(":")(0))
            Dim jobStatus As String = e.UserState.ToString.Split(":")(1).ToString
            gvXmlJobs.SetRowCellValue(gvXmlJobs.LocateByValue("XMLJobID", xmlJobID), "RESTAPIStatus", jobStatus)
            gcXmlJobs.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgWorkerPartial_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorkerPartial.RunWorkerCompleted, bgWorkerPartialRollback.RunWorkerCompleted, bgWorkerExecute.RunWorkerCompleted
        ', bgWorkerExecuteRollback.RunWorkerCompleted
        Try
            LoadXmlJobList()
            Try
                Call gvXmlJobs_RowCellStyle(gvXmlJobs, Nothing)
            Catch
            End Try

            LoadXmlJobLog(xmlJobID)

            LoadProvisionResult()
            xtcSubTop.SelectedTabPageIndex = 4

            Timer1_Tick(Nothing, Nothing)

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            bgWorkerPartial.CancelAsync()
            bgWorkerPartial.Dispose()

            bgWorkerPartialRollback.CancelAsync()
            bgWorkerPartialRollback.Dispose()

            bgWorkerExecute.CancelAsync()
            bgWorkerExecute.Dispose()

            'bgWorkerExecuteRollback.CancelAsync()
            'bgWorkerExecuteRollback.Dispose()
        End Try
    End Sub

    Private Sub Call_ImportXmlJobEricsson_PartialProvision()
        Dim canUserExecute As Boolean = False
        If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
            If configMgr.User.IsPowerUser = True Then
                canUserExecute = True
            Else
                XtraMessageBox.Show("Only the XML job owner or the power user can validate", "Validate Import XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                canUserExecute = False
                Exit Sub
            End If
        Else
            canUserExecute = True
        End If

        If (canUserExecute = True) Then

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtOutputXml As DataTable = GetXmlOutputDataForXmlJobID(xmlJobID)

            If Not dtOutputXml Is Nothing Then

                If dtOutputXml.Rows.Count > 0 Then
                    gvXmlJobs.SetFocusedRowCellValue("RESTAPIStatus", "RUNNING")
                    gcXmlJobs.Refresh()
                    Application.DoEvents()

                    For Each drXml As DataRow In dtOutputXml.Rows

                        If Not IsDBNull(drXml("XMLNewConfig")) Then
                            If bgWorkerPartial.CancellationPending = True Then
                                UpdateRestAPIStatus(xmlJobID, "KILLED")
                                Exit Sub
                            End If
                            RunXmlImportJobEricsson_PartialProvision(xmlJobID, CInt(drXml("XMLFileID")), CStr(drXml("RestAPI_URLKey")))
                        Else
                            Exit Sub
                        End If

                    Next

                End If

            End If

        End If

    End Sub

    Private Sub RunXmlImportJobEricsson_PartialProvision(xmlJobID As Integer, xmlFileID As Integer, restAPIKey As String)
        Dim objXSR As New XmlServRef.XmlProvisionServiceSoapClient()
        Dim enmJobID As String = Nothing
        Try
            Dim userToken As String = Nothing
            Dim xmlCMTaskID As String = Nothing
            Dim enmJobExportID As String = Nothing
            Dim enmValidatonID As String = Nothing
            Dim attachFileResponse As String = Nothing
            Dim validateJobResponse As String = Nothing
            Dim fileName As String = Nothing
            Dim validationResult As String = Nothing
            Dim provisionID As String = Nothing
            Dim createJobResponse As String = Nothing

            'service request for user access session cookie
            Dim tokenRequestResult As String = objXSR.LoginUserEricsson(restAPIKey)

            If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then

                userToken = tokenRequestResult.Split("^")(1)
                provisionID = UpdateProvisionStatus(xmlJobID, xmlFileID, userToken, "User Session Cookie Received")

                'service request to create a new enm job id
                createJobResponse = objXSR.CreateImportJobEricsson(restAPIKey, userToken, "XmlJob" & xmlJobID)

                If createJobResponse IsNot Nothing Then
                    If createJobResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then

                        enmJobID = createJobResponse.Split("^")(1).ToString
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBCREATED", "ENM JOB CREATED")
                        bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "ENM JOB CREATED")
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - Received New ENM Job ID: " & enmJobID & " For XML Job ID: " & xmlJobID)

                        'start attaching xml file for enm job
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILEATTACHING", "ATTACHING XML FILE...")
                        bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "ATTACHING XML FILE...")

                        'service request to attach xml file with enm job id
                        attachFileResponse = objXSR.AttachXmlFileToImportJobEricsson(restAPIKey, userToken, enmJobID, xmlJobID, xmlFileID, "NewConfig")

                        If attachFileResponse Is Nothing Then
                            attachFileResponse = "FILE NOT ATTACHED"
                            UpdateRestAPIStatus(xmlJobID, attachFileResponse)
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILENOTATTACHED", attachFileResponse)
                            bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & attachFileResponse)
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job File Atachment Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                            Exit Sub
                        End If

                        If attachFileResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then
                            fileName = attachFileResponse.Split("^")(2).ToString
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "XMLATTACHED", "XML FILE ATTACHED")
                            bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "XML FILE ATTACHED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: " & fileName & " Attached To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            'service request to validate the import job
                            validateJobResponse = objXSR.ParseAndValidateImportJobEricsson(restAPIKey, userToken, enmJobID)

                        ElseIf attachFileResponse.Split("^")(0).ToString.ToUpper = "UNPROCESSABLE ENTITY" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "UNPROCESSABLEENTITY", "UNPROCESSABLE ENTITY")
                            bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "UNPROCESSABLE ENTITY")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: Attachment Falied To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                            Exit Sub
                        ElseIf attachFileResponse.Split("^")(0).ToString.ToUpper = "ERROR" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "ERROR", "ERROR OCCURED")
                            bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "ERROR OCCURED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - XML File: Attachment Falied To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                            Exit Sub
                        End If

                        'save provisioning data and get xml cm task id
                        xmlCMTaskID = SaveEricssonProvisionValidationResult(provisionID, xmlJobID, xmlFileID, enmJobID)

                        If validateJobResponse.Split("^")(0).ToString.ToUpper = "ACCEPTED" Then

                            Dim validResult As String = ""
                            Do While validResult <> "VALIDATED"

                                If bgWorkerPartial.CancellationPending = True Then
                                    UpdateRestAPIStatus(xmlJobID, "KILLED")
                                    Exit Sub
                                End If

                                'sleep the running thread for 2 sec
                                Threading.Thread.Sleep(2000)

                                Dim validationResponse As String = validateJobResponse.Split("^")(1).ToString
                                enmValidatonID = validationResponse.Split(",")(0).Split(":")(1)
                                validResult = objXSR.GetImportJobsEricssonValidationResponse(restAPIKey, userToken, xmlCMTaskID, enmValidatonID).Split("^")(1)

                                If validResult = "VALIDATED" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Validated")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATED", "JOB VALIDATED")
                                    bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "JOB VALIDATED")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validated Successfully For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                ElseIf validResult = "VALIDATING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Validating")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATING", "JOB VALIDATING")
                                    bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "JOB VALIDATING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validating For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                ElseIf validResult = "PARSING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsing")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSING", "JOB PARSING")
                                    bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "JOB PARSING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsing For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                ElseIf validResult = "PARSED" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsed")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSED", "JOB PARSED")
                                    bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & "JOB PARSED")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                    Exit Do
                                Else
                                    UpdateRestAPIStatus(xmlJobID, validResult)
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, validResult, validResult)
                                    bgWorkerPartial.ReportProgress(0, xmlJobID & ":" & validResult)
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validation Result: " & validResult & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                    Exit Do
                                End If

                            Loop

                        Else
                            XtraMessageBox.Show("Validate Import Job - Error occured: " & validateJobResponse.Split("^")(1).ToString, "Validate Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            UpdateRestAPIStatus(xmlJobID, validateJobResponse.Split("^")(1).ToString)
                        End If

                        'service request to logout user
                        objXSR.LogoutUserEricsson(restAPIKey)
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                    Else
                        XtraMessageBox.Show("Create Import Job - Error occured: " & createJobResponse.Split("^")(1).ToString, "Create Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateRestAPIStatus(xmlJobID, createJobResponse.Split("^")(1).ToString)
                        attachFileResponse = "IMPORT FAILED"
                        validateJobResponse = "IMPORT FAILED"
                    End If
                Else
                    UpdateRestAPIStatus(xmlJobID, "ENM Job Creation Failed")
                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "ENMJOBFAILED", "ENM Job Failed")
                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ENM JOB FAILED")
                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Create Import Job Result: Failed For XML Job ID: " & xmlJobID)
                End If
            Else
                userToken = tokenRequestResult.Split("^")(1)
                UpdateRestAPIStatus(xmlJobID, userToken)
            End If

        Catch ex As Exception
            UpdateRestAPIStatus(xmlJobID, "IMPORT FAILED")
            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "IMPORTFAILED", "IMPORT FAILED")
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            objXSR.LogoutUserEricsson(restAPIKey)
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            objXSR = Nothing
        End Try
    End Sub

    Private Sub btnCreateValidateRollback_Click(sender As Object, e As EventArgs) Handles btnCreateValidateRollback.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            bgWorkerPartialRollback.WorkerReportsProgress = True
            bgWorkerPartialRollback.RunWorkerAsync()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub bgWorkerPartialRollback_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorkerPartialRollback.DoWork
        Try
            If xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                Call_ImportXmlJobEricsson_Rollback_PartialProvision()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub Call_ImportXmlJobEricsson_Rollback_PartialProvision()
        Dim canUserExecute As Boolean = False
        If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
            If configMgr.User.IsPowerUser = True Then
                canUserExecute = True
            Else
                XtraMessageBox.Show("Only the XML job owner or the power user can validate", "Validate Import XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                canUserExecute = False
                Exit Sub
            End If
        Else
            canUserExecute = True
        End If

        If (canUserExecute = True) Then

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtOutputXml As DataTable = GetXmlOutputDataForXmlJobID(xmlJobID)

            If Not dtOutputXml Is Nothing Then

                If dtOutputXml.Rows.Count > 0 Then
                    gvXmlJobs.SetFocusedRowCellValue("RESTAPIStatus", "RUNNING")
                    gcXmlJobs.Refresh()
                    Application.DoEvents()

                    For Each drXml As DataRow In dtOutputXml.Rows

                        If Not IsDBNull(drXml("XMLRollback")) Then
                            If bgWorkerPartialRollback.CancellationPending = True Then
                                UpdateRestAPIStatus(xmlJobID, "KILLED")
                                Exit Sub
                            End If
                            RunXmlImportJobEricsson_Rollback_PartialProvision(xmlJobID, CInt(drXml("XMLFileID")), CStr(drXml("RestAPI_URLKey")))
                        Else
                            Exit Sub
                        End If

                    Next

                End If

            End If

        End If
    End Sub

    Private Sub RunXmlImportJobEricsson_Rollback_PartialProvision(xmlJobID As Integer, xmlFileID As Integer, restAPIKey As String)
        Dim objXSR As New XmlServRef.XmlProvisionServiceSoapClient()
        Dim enmJobID As String = Nothing
        Try
            Dim userToken As String = Nothing
            Dim xmlCMTaskID As String = Nothing
            Dim enmJobExportID As String = Nothing
            Dim enmValidatonID As String = Nothing
            Dim attachFileResponse As String = Nothing
            Dim validateJobResponse As String = Nothing
            Dim validationResult As String = Nothing
            Dim fileName As String = Nothing
            Dim provisionID As String = Nothing
            Dim createJobResponse As String = Nothing

            'service request for user access session cookie
            Dim tokenRequestResult As String = objXSR.LoginUserEricsson(restAPIKey)

            If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then

                userToken = tokenRequestResult.Split("^")(1)
                provisionID = UpdateProvisionStatus(xmlJobID, xmlFileID, userToken, "User Session Cookie Received")

                'service request to create a new job id
                createJobResponse = objXSR.CreateImportJobEricsson(restAPIKey, userToken, "XmlJob" & xmlJobID)

                If createJobResponse IsNot Nothing Then
                    If createJobResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then

                        enmJobID = createJobResponse.Split("^")(1).ToString
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBCREATED", "ENM JOB CREATED")
                        bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & "ENM JOB CREATED")
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - Received New ENM Job ID: " & enmJobID & " For XML Job ID: " & xmlJobID)

                        'start attaching xml file for enm job
                        SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILEATTACHING", "ATTACHING XML FILE...")
                        bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & "ATTACHING XML FILE...")

                        'service request to attach an xml file with enm job id
                        attachFileResponse = objXSR.AttachXmlFileToImportJobEricsson(restAPIKey, userToken, enmJobID, xmlJobID, xmlFileID, "Rollback")

                        If attachFileResponse Is Nothing Then
                            attachFileResponse = "FILE NOT ATTACHED"
                            UpdateRestAPIStatus(xmlJobID, attachFileResponse)
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "FILENOTATTACHED", attachFileResponse)
                            bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & attachFileResponse)
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job File Atachment Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                            Exit Sub
                        End If

                        If attachFileResponse.Split("^")(0).ToString.ToUpper = "CREATED" Then
                            UpdateRestAPIStatus(xmlJobID, "File Attached To Import Job")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "XMLATTACHED", "ROLLBACK XML FILE ATTACHED")
                            bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & "ROLLBACK XML FILE ATTACHED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Rollback XML File: " & fileName & " Attached To Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            'service request to validate the import job
                            validateJobResponse = objXSR.ParseAndValidateImportJobEricsson(restAPIKey, userToken, enmJobID)
                        End If

                        'save provisioning data and get xml cm task id
                        xmlCMTaskID = SaveEricssonProvisionValidationResult(provisionID, xmlJobID, xmlFileID, enmJobID)

                        If validateJobResponse.Split("^")(0).ToString.ToUpper = "ACCEPTED" Then

                            Dim validResult As String = ""
                            Do While validResult <> "VALIDATED"

                                If bgWorkerPartialRollback.CancellationPending = True Then
                                    UpdateRestAPIStatus(xmlJobID, "KILLED")
                                    Exit Sub
                                End If

                                'sleep the running thread for 2 sec
                                Threading.Thread.Sleep(2000)

                                Dim validationResponse As String = validateJobResponse.Split("^")(1).ToString
                                enmValidatonID = validationResponse.Split(",")(0).Split(":")(1)
                                validResult = objXSR.GetImportJobsEricssonValidationResponse(restAPIKey, userToken, xmlCMTaskID, enmValidatonID).Split("^")(1)

                                If validResult = "VALIDATED" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Validated")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATED", "JOB VALIDATED")
                                    bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & "JOB VALIDATED")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validated Successfully For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                    Exit Do
                                ElseIf validResult = "VALIDATING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Validating")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBVALIDATING", "JOB VALIDATING")
                                    bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & "JOB VALIDATING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validating For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                ElseIf validResult = "PARSING" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsing")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSING", "JOB PARSING")
                                    bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & "JOB PARSING")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsing For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                ElseIf validResult = "PARSED" Then
                                    UpdateRestAPIStatus(xmlJobID, "Job Parsed")
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBPARSED", "JOB PARSED")
                                    bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & "JOB PARSED")
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Parsed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                    Exit Do
                                Else
                                    UpdateRestAPIStatus(xmlJobID, validResult)
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, validResult, validResult)
                                    bgWorkerPartialRollback.ReportProgress(0, xmlJobID & ":" & validResult)
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Validation Result: " & validResult & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                                End If

                            Loop

                        Else
                            XtraMessageBox.Show("Validate Import Job - Error occured: " & validateJobResponse.Split("^")(1).ToString, "Validate Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            UpdateRestAPIStatus(xmlJobID, validateJobResponse.Split("^")(1).ToString)
                        End If

                        'service request to logout user
                        objXSR.LogoutUserEricsson(restAPIKey)
                        UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                    Else
                        XtraMessageBox.Show("Create Import Job - Error occured: " & createJobResponse.Split("^")(1).ToString, "Create Import Job Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateRestAPIStatus(xmlJobID, createJobResponse.Split("^")(1).ToString)
                        attachFileResponse = "IMPORT FAILED"
                        validateJobResponse = "IMPORT FAILED"
                    End If
                Else
                    UpdateRestAPIStatus(xmlJobID, "ENM Job Creation Failed")
                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "ENMJOBFAILED", "ENM Job Failed")
                    bgWorkerRollBack.ReportProgress(0, xmlJobID & ":" & "ENM JOB FAILED")
                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - Create Import Job Result: Failed For XML Job ID: " & xmlJobID)
                End If
            Else
                userToken = tokenRequestResult.Split("^")(1)
                UpdateRestAPIStatus(xmlJobID, userToken)
            End If

        Catch ex As Exception
            UpdateRestAPIStatus(xmlJobID, "IMPORT FAILED")
            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "IMPORTFAILED", "IMPORT FAILED")
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            objXSR.LogoutUserEricsson(restAPIKey)
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            objXSR = Nothing
        End Try
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            bgWorkerExecute.WorkerReportsProgress = True
            bgWorkerExecute.RunWorkerAsync()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub bgWorkerExecute_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorkerExecute.DoWork
        Try
            If xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                Call_ImportXmlJobEricsson_PartialProvision_Execute()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub Call_ImportXmlJobEricsson_PartialProvision_Execute()
        Dim canUserExecute As Boolean = False
        If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
            If configMgr.User.IsPowerUser = True Then
                canUserExecute = True
            Else
                XtraMessageBox.Show("Only the XML job owner or the power user can validate", "Validate Import XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                canUserExecute = False
                Exit Sub
            End If
        Else
            canUserExecute = True
        End If

        If (canUserExecute = True) Then

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtValidatedJobs As DataTable = GetXmlEnmJobsForExecutionEricsson(xmlJobID)

            If Not dtValidatedJobs Is Nothing Then

                If dtValidatedJobs.Rows.Count > 0 Then
                    gvXmlJobs.SetFocusedRowCellValue("RESTAPIStatus", "RUNNING")
                    gcXmlJobs.Refresh()
                    Application.DoEvents()

                    For Each drJob As DataRow In dtValidatedJobs.Rows

                        If Not IsDBNull(drJob("ENMJobID")) Then
                            If bgWorkerExecute.CancellationPending = True Then
                                UpdateRestAPIStatus(xmlJobID, "KILLED")
                                Exit Sub
                            End If
                            RunXmlImportJobEricsson_PartialProvision_Execute(xmlJobID, CInt(drJob("XMLFileID")), CInt(drJob("ENMJobID")), CInt(drJob("XMLCMTaskID")), CStr(drJob("RestAPI_URLKey")))
                        Else
                            Exit Sub
                        End If

                    Next
                Else
                    XtraMessageBox.Show("Import Job cannot be executed since the validation isn't completed!", "Import XML Job Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            End If

        End If

    End Sub

    Private Sub RunXmlImportJobEricsson_PartialProvision_Execute(xmlJobID As Integer, xmlFileID As Integer, enmJobID As Integer, xmlCMTaskID As Integer, restAPIKey As String)
        Dim objXSR As New XmlServRef.XmlProvisionServiceSoapClient()
        Try
            Dim userToken As String = Nothing
            Dim executeJobResponse As String = Nothing
            Dim enmJobExportID As String = Nothing

            'service request for user access session cookie
            Dim tokenRequestResult As String = objXSR.LoginUserEricsson(restAPIKey)

            If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then

                userToken = tokenRequestResult.Split("^")(1)

                executeJobResponse = ""
                Do While executeJobResponse.ToUpper <> "EXECUTED"

                    If bgWorkerExecute.CancellationPending = True Then
                        UpdateRestAPIStatus(xmlJobID, "KILLED")
                        Exit Sub
                    End If

                    'sleep the running thread for 2 sec
                    Threading.Thread.Sleep(2000)

                    If executeJobResponse.ToUpper = "" Then
                        'service request to execute the import job
                        executeJobResponse = objXSR.ExecuteImportJobEricsson(restAPIKey, userToken, enmJobID, xmlCMTaskID)
                        If executeJobResponse.Split("^")(0).ToUpper = "ACCEPTED" Then

                            UpdateRestAPIStatus(xmlJobID, "EXECUTED")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "JOB EXECUTED")
                            bgWorkerExecute.ReportProgress(0, xmlJobID & ":" & "JOB EXECUTED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Executed Successfully For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            xtcSubTop.TabPages(3).Appearance.Header.BackColor = Nothing

                            bgWorkerExecute.ReportProgress(0, xmlJobID & ":" & "JOB EXECUTED")
                            Exit Do

                        ElseIf executeJobResponse.ToUpper = "EXECUTING" Then

                            UpdateRestAPIStatus(xmlJobID, "Job Executing")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTING", "JOB EXECUTING")
                            bgWorkerExecute.ReportProgress(0, xmlJobID & ":" & "JOB EXECUTING")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Executing For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                        ElseIf executeJobResponse.Split("^")(0).ToUpper = "ERROR" Then

                            If executeJobResponse.Split("^")(1).ToUpper = "CONFLICT" Then
                                Dim conflictMsg As String = executeJobResponse.Split("^")(2)
                                Dim jsonObject As Linq.JObject = JsonConvert.DeserializeObject(conflictMsg)
                                Dim errMsg = jsonObject.SelectToken("errors").Children().ToList()(0).Last().ToString.Split(":")(1).Trim

                                UpdateRestAPIStatus(xmlJobID, "EXECUTE JOB FAILED")
                                SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTE JOB FAILED")
                                UpdateLogForXmlJobId(xmlJobID, "XML Provision - " & errMsg.ToString.Replace("""", "") & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                                Exit Do
                            End If

                            UpdateRestAPIStatus(xmlJobID, "EXECUTE JOB FAILED")
                            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTE JOB FAILED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Job Execution Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

                            bgWorkerExecute.ReportProgress(0, xmlJobID & ":" & "EXECUTE JOB FAILED")
                            enmJobExportID = objXSR.GetImportJobsEricssonExportID(restAPIKey, userToken, enmJobID).ToString.Split("^")(1)

                            If enmJobExportID IsNot Nothing Then
                                Dim enmJonExportResult As String = Nothing
                                enmJonExportResult = objXSR.DownloadImportJobsEricssonFile(restAPIKey, userToken, enmJobID, enmJobExportID, xmlCMTaskID)
                                Dim vrParts() As String = enmJonExportResult.ToString.Split("^")
                                If vrParts(1) = "ValidationResultSaved" Then
                                    UpdateLogForXmlJobId(xmlJobID, "XML Provision - " & vrParts(1) & " For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
                                    SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "JOBEXECUTED", "EXECUTION RESULT RECEIVED")
                                    xtcSubTop.TabPages(3).Appearance.Header.BackColor = Color.DarkRed
                                End If
                            End If

                            Exit Do
                        End If
                    Else
                        bgWorkerExecute.ReportProgress(0, xmlJobID & ":" & executeJobResponse.ToUpper & " - ERROR")
                    End If

                Loop

            Else
                userToken = tokenRequestResult.Split("^")(1)
                UpdateRestAPIStatus(xmlJobID, userToken)
            End If

            'service request to logout user
            objXSR.LogoutUserEricsson(restAPIKey)
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)

        Catch ex As Exception
            UpdateRestAPIStatus(xmlJobID, "IMPORT FAILED")
            SetQueryCMTaskJobStatus(xmlJobID, xmlFileID, "IMPORTFAILED", "IMPORT FAILED")
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - Import Failed For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            objXSR.LogoutUserEricsson(restAPIKey)
            UpdateLogForXmlJobId(xmlJobID, "XML Provision - User Logged Out For Import Job ID: " & enmJobID & " And XML Job ID: " & xmlJobID)
            objXSR = Nothing
        End Try
    End Sub

    Private Sub btnExecuteRollback_Click(sender As Object, e As EventArgs) Handles btnExecuteRollback.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'bgWorkerExecuteRollback.WorkerReportsProgress = True
            'bgWorkerExecuteRollback.RunWorkerAsync()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#End Region

#End Region

#Region "Events"

    Private Sub gvXmlJobs_RowCellStyle(sender As Object, e As RowCellStyleEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        If e IsNot Nothing Then
            If Not IsDBNull(view.GetRowCellValue(e.RowHandle, "RESTAPIStatus")) Then
                Dim _status As String = CStr(view.GetRowCellValue(e.RowHandle, "RESTAPIStatus"))
                If e.Column.FieldName = "RESTAPIStatus" Then
                    If _status.ToUpper = "IMPORT SUCCESSFUL" Or _status.ToUpper = "COMPLETED" Or _status.ToUpper = "PARTIALLY COMPLETED" Or _status.ToUpper.Contains("IMPORT SKIPPED") Or
                        _status.ToUpper = "JOB EXECUTED" Or _status.ToUpper = "XML FILE ATTACHED" Or _status.ToUpper = "JOB VALIDATED" Or _status.ToUpper = "ATTACHING XML FILE..." Then
                        e.Appearance.BackColor = Color.LightGreen
                        e.Appearance.ForeColor = Color.Black
                    ElseIf _status.ToUpper = "VALIDATE BEGIN" Or _status.ToUpper = "VALIDATE SUCCESS" Or _status.ToUpper = "VALIDATE FAIL" Or _status.ToUpper = "PREACTIVE BEGIN" Or _status.ToUpper = "PREACTIVE SUCCESS" Or _status.ToUpper = "PREACTIVE FAIL" Or
                           _status.ToUpper = "ACTIVE BEGIN" Or _status.ToUpper = "ACTIVE SUCCESS" Or _status.ToUpper = "ACTIVE FAIL" Or _status.ToUpper = "PROCESS SCRIPT BEGIN" Or _status.ToUpper = "PROCESS SCRIPT SUCCESS" Or _status.ToUpper = "PROCESS SCRIPT FAIL" Or
                           _status.ToUpper = "GENERATESCRIPT BEGIN" Or _status.ToUpper = "GENERATESCRIPT SUCCESS" Or _status.ToUpper = "GENERATESCRIPT FAIL" Or _status.ToUpper = "FILE NOT ATTACHED" Or _status.ToUpper = "CANCELLING" Or _status.ToUpper = "JOB PARSING" Or
                           _status.ToUpper = "JOB PARSED" Or _status.ToUpper = "JOB VALIDATING" Or _status.ToUpper = "JOB EXECUTING" Then
                        e.Appearance.BackColor = Color.Orange
                        e.Appearance.ForeColor = Color.Black
                    ElseIf _status.ToUpper = "INITIATING" Or _status.ToUpper = "ABNORMAL" Or _status.ToUpper = "SUSPENDED" Then
                        e.Appearance.BackColor = Color.Yellow
                        e.Appearance.ForeColor = Color.Black
                    ElseIf _status.ToUpper = "IMPORT FAILED" Or _status.ToUpper = "EXECUTE JOB FAILED" Or _status.ToUpper = "UNAUTHORIZED" Or _status.ToUpper = "UNPROCESSABLE ENTITY" Or _status.ToUpper = "ERROR OCCURED" Then
                        e.Appearance.BackColor = Color.OrangeRed
                        e.Appearance.ForeColor = Color.Black
                    ElseIf _status.ToUpper.Contains("RUNNING") Or _status.ToUpper.Contains("INITIATING") Then
                        e.Appearance.BackColor = Color.LightGray
                        e.Appearance.ForeColor = Color.Black
                    ElseIf _status.ToUpper = "KILLED" Or _status.ToUpper = "CANCELLED" Then
                        e.Appearance.BackColor = Color.Black
                        e.Appearance.ForeColor = Color.White
                    Else
                        e.Appearance.BackColor = Color.White
                        e.Appearance.ForeColor = Color.Black
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

    Private Sub frmGenerateXML_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadXmlJobList()
            gvXmlJobs.FocusedRowHandle = 0
            gvXmlJobs_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub gvXmlJobs_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvXmlJobs.RowCount > 0 AndAlso e IsNot Nothing Then
                gvXmlJobs.ClearSelection()
                gvXmlJobs.FocusedRowHandle = e.FocusedRowHandle
                gvXmlJobs.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            xmlVendor = CStr(gvXmlJobs.GetFocusedRowCellValue("XMLVendor"))
            'Load XML job log
            LoadXmlJobLog(xmlJobID)

            xtcSubTop.SelectedTabPageIndex = 0

            LoadRightPanelGrids()

            If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
                grpPartialProvision.Visible = False
            ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                grpPartialProvision.Visible = True
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub xtcSubTop_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcSubTop.SelectedPageChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If xtcSubTop.SelectedTabPageIndex <> 0 Then
                LoadRightPanelGrids()
            End If


        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If gvXmlJobs.FocusedRowHandle >= 0 Then

                xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))

                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@XMLJobID", xmlJobID}
                }
                strConnection = GetSQL(6507, parray)(0)
                sqlParam = GetSQL(6507, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, iQryTimeOut)

                If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
                    'generate new config/rollback xml files
                    GenerateXMLFiles(xmlJobID)

                    'save json string for xml new config
                    SaveJsonNewConfig(xmlJobID)

                    'save json string for xml rollback
                    SaveJsonRollBack(xmlJobID)

                ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                    'generate new config/rollback xml files
                    GenerateXMLFiles(xmlJobID)
                End If

                UpdateRestAPIStatus(xmlJobID, "")

                'reload xml jobs list
                LoadXmlJobList()
                gvXmlJobs.FocusedRowHandle = gvXmlJobs.LocateByValue("XMLJobID", xmlJobID)

                'reload xml job log
                LoadXmlJobLog(xmlJobID)

                xtcSubTop.SelectedTabPageIndex = 0

                LoadRightPanelGrids()

            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnSaveXml_Click(sender As Object, e As EventArgs) Handles btnSaveXml.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim xmlFileName As String = Nothing
            Dim pathPart As String = Nothing
            Dim filePath As String = Nothing
            Dim fileName As String = Nothing
            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim dtNewConfig As DataTable = Nothing
            Dim dtRollback As DataTable = Nothing

            Dim objSFD As New SaveFileDialog()
            objSFD.RestoreDirectory = False
            If openFileDirectory Is Nothing Then
                objSFD.InitialDirectory = IO.Directory.GetCurrentDirectory()
            Else
                objSFD.InitialDirectory = openFileDirectory
            End If
            objSFD.Filter = "XML|*.xml"
            objSFD.Title = "Save XML Files"
            objSFD.ShowDialog()
            xmlFileName = objSFD.FileName
            pathPart = xmlFileName.Split(".")(0)
            filePath = pathPart.Replace(xmlFileName.Split(".")(0).Substring(xmlFileName.Split(".")(0).LastIndexOf("\")), "")
            fileName = xmlFileName.Split(".")(0).Substring(xmlFileName.Split(".")(0).LastIndexOf("\")).TrimStart("\")

            'fetch xml new config data from sql
            dtNewConfig = GetXmlNewConfigData()

            If dtNewConfig IsNot Nothing AndAlso dtNewConfig.Rows.Count > 0 Then

                'save xml data into file
                For Each dr As DataRow In dtNewConfig.Rows

                    Dim xmlStr As String = dr("XMLNewConfig").ToString
                    If xmlFileName <> "" Then
                        Dim xmlDoc As New System.Xml.XmlDocument()
                        xmlDoc.LoadXml(xmlStr)
                        If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
                            xmlDoc.Save(filePath & "\" & fileName & "_" & dr("XMLXsdUsed").ToString.Split(".")(0) & ".xml")
                        ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                            xmlDoc.Save(filePath & "\" & fileName & "_" & dr("RestAPI_URLKey") & ".xml")
                        End If
                    End If

                Next

            End If

            'fetch xml rollback data from sql
            dtRollback = GetXmlRollBackData()

            If dtRollback IsNot Nothing AndAlso dtRollback.Rows.Count > 0 Then

                'save rollback xml data into file
                For Each dr As DataRow In dtRollback.Rows

                    Dim xmlRollBackStr As String = dr("XMLRollback").ToString
                    If xmlFileName <> "" Then
                        Dim xmlDoc As New System.Xml.XmlDocument()
                        xmlDoc.LoadXml(xmlRollBackStr)
                        If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
                            xmlDoc.Save(filePath & "\" & "RollBack" & fileName & "_" & dr("XMLXsdUsed").ToString.Split(".")(0) & ".xml")
                        ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                            xmlDoc.Save(filePath & "\" & "RollBack" & fileName & "_" & dr("RestAPI_URLKey") & ".xml")
                        End If
                    End If

                Next

            End If

            If dtNewConfig.Rows.Count > 0 AndAlso dtRollback.Rows.Count > 0 Then
                XtraMessageBox.Show("XML (new config + rollback) files saved successfully for XML Job ID: " & xmlJobID, "Saving XML Files", MessageBoxButtons.OK)
            Else
                XtraMessageBox.Show("XML (new config + rollback) files couldn't be created for XML Job ID: " & xmlJobID, "XML Files Not Created", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            'reload xml job log
            LoadXmlJobLog(xmlJobID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnProvision_Click(sender As Object, e As EventArgs) Handles btnProvision.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            bgWorker.WorkerReportsProgress = True
            bgWorker.RunWorkerAsync()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRollbackProvision_Click(sender As Object, e As EventArgs) Handles btnRollbackProvision.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            bgWorkerRollBack.WorkerReportsProgress = True
            bgWorkerRollBack.RunWorkerAsync()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnXmlJobsAdd_Click(sender As Object, e As EventArgs) Handles btnXmlJobsAdd.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim objAddXmlJOb As New dlgAddXMlJob()
            objAddXmlJOb.ShowDialog()

            If newXMLJob IsNot Nothing AndAlso newXMLJob <> "" Then
                LoadXmlJobList()
                gvXmlJobs.FocusedRowHandle = gvXmlJobs.LocateByValue("XMLJobName", newXMLJob)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnXmlJobsDelete_Click(sender As Object, e As EventArgs) Handles btnXmlJobsDelete.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim canUserDelete As Boolean = False
            If gvXmlJobs.SelectedRowsCount > 0 Then
                If XtraMessageBox.Show("Are you sure to delete XML Job: " & CStr(gvXmlJobs.GetFocusedRowCellValue("XMLJobName")) & "?", "Delete XML Job", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner").ToString.ToLower <> Environment.UserName.ToLower Then
                        If configMgr.User.IsPowerUser = True Then
                            canUserDelete = True
                        Else
                            XtraMessageBox.Show("Only the XML job owner or the power user can delete", "Delete XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            canUserDelete = False
                            Exit Sub
                        End If
                    Else
                        canUserDelete = True
                    End If

                    If (canUserDelete = True) Then
                        Dim strConnection As String = Nothing
                        Dim sqlParam As String = Nothing
                        Dim parray()() As String = {
                        New String() {"@XMLJobID", CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))}
                    }
                        strConnection = GetSQL(6514, parray)(0)
                        sqlParam = GetSQL(6514, parray)(1)
                        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    End If
                    LoadXmlJobList()
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnXmlJobsRefresh_Click(sender As Object, e As EventArgs) Handles btnXmlJobsRefresh.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'reload xml jobs list
            LoadXmlJobList()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub ExecuteAfterQueryCMTaskThreadComplete(xmlJobID As Integer, jobStatus As String, jobExecResult As String, ti As Threading.Thread)
        'SyncLock objQryCMTaskThreadLock
        'Dim arg() As Object = {xmlJobID, jobStatus, jobExecResult}
        'Me.BeginInvoke(New CallThreadQueryCMTask(AddressOf SetQueryCMTaskJobStatus), arg)
        'End SyncLock
    End Sub

    Private Sub btnKillProvision_Click(sender As Object, e As EventArgs) Handles btnKillProvision.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            xmlJobID = gvXmlJobs.GetFocusedRowCellValue("XMLJobID")
            Dim objXmlServRef As New XmlServRef.XmlProvisionServiceSoapClient()
            Dim deleteCMDataResponse As String = Nothing

            'get kill provision uri for the selected xml job id
            Dim dt As DataTable = GetQueryCMTaskData(xmlJobID)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    Dim href As String = dt.Rows(0)("Href").ToString
                    UpdateLogForXmlJobId(xmlJobID, "XML Kill Provision - Request URI: " & href & " For XMLJobID: " & xmlJobID)

                    'service request for user access token
                    Dim tokenRequestResult As String = objXmlServRef.GetAuthorizeToken()
                    Dim accessToken As String = Nothing

                    If tokenRequestResult.Split("^")(0).ToString.ToUpper = "OK" Then
                        accessToken = tokenRequestResult.Split("^")(1)

                        'service request for delete cm data task
                        deleteCMDataResponse = objXmlServRef.DeleteSetCMDataTask(accessToken, xmlJobID, CStr(href))

                        If deleteCMDataResponse.Split("^")(0).ToString.ToUpper = "OK" Then
                            UpdateRestAPIStatus(xmlJobID, "KILLED")
                            UpdateLogForXmlJobId(xmlJobID, "XML Kill Provision - Response: " & deleteCMDataResponse.Split("^")(1).ToString & " and " & deleteCMDataResponse.Split("^")(2).ToString & " For XMLJobID: " & xmlJobID)
                        End If

                    End If

                End If
            End If

            'reload xml jobs list
            LoadXmlJobList()
            gvXmlJobs.FocusedRowHandle = gvXmlJobs.LocateByValue("XMLJobID", xmlJobID)

            'reload xml job log
            LoadXmlJobLog(xmlJobID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            bgWorker.CancelAsync()
            bgWorker.Dispose()

            bgWorkerRollBack.CancelAsync()
            bgWorkerRollBack.Dispose()

            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_PasteDataFromClipboard_Click(sender As Object, e As EventArgs) Handles tsmi_PasteDataFromClipboard.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            IsErrorInCopy = False

            dtInputData = New DataTable
            dtInputData.Columns.Add("XMLJobID", GetType(Integer))
            dtInputData.Columns.Add("VENDOR", GetType(String))
            dtInputData.Columns.Add("MO", GetType(String))
            dtInputData.Columns.Add("NENAME", GetType(String))
            dtInputData.Columns.Add("ObjectName", GetType(String))
            dtInputData.Columns.Add("ParameterName", GetType(String))
            dtInputData.Columns.Add("ParameterValue", GetType(String))
            dtInputData.Columns.Add("RollBackValue", GetType(String))

            'copy data from clipboard to local data table
            gvInputData.PasteFromClipboard()

            'bulk insert input data into db
            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                If dtInputData IsNot Nothing Then
                    InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[XML_InputData]", dtInputData)
                    SetMessage("Input data imported successfully")
                    LoadInputData()
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

    Private Sub gvInputData_ClipboardRowPasting(sender As Object, e As ClipboardRowPastingEventArgs) Handles gvInputData.ClipboardRowPasting
        Try
            If IsErrorInCopy = True Then
                e.Cancel = True
                Clipboard.Clear()
                Exit Sub
            End If

            If e.OriginalValues.Count > 0 Then
                Dim rIndex() As Integer = gvInputData.GetSelectedRows()
                If rIndex.Length > 0 Then

                    xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
                    If e.OriginalValues.Count = 7 Then

                        Dim drData As DataRow
                        drData = dtInputData.NewRow()
                        drData(0) = xmlJobID
                        drData(1) = e.OriginalValues(0).ToString().Trim()
                        drData(2) = e.OriginalValues(1).ToString().Trim()
                        drData(3) = e.OriginalValues(2).ToString().Trim()
                        drData(4) = e.OriginalValues(3).ToString().Trim()
                        drData(5) = e.OriginalValues(4).ToString().Trim()
                        drData(6) = e.OriginalValues(5).ToString().Trim()
                        drData(7) = e.OriginalValues(6).ToString().Trim()
                        dtInputData.Rows.Add(drData)

                    ElseIf e.OriginalValues(0).ToString() <> "" Then
                        XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<VENDOR>,<MO>,<NENAME>,<ObjectName>,<ParameterName>,<ParameterValue>,<RollBackValue>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        e.Cancel = True
                        Clipboard.Clear()
                        IsErrorInCopy = True
                    End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<VENDOR>,<MO>,<NENAME>,<ObjectName>,<ParameterName>,<ParameterValue>,<RollBackValue>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Clipboard.Clear()
            IsErrorInCopy = True
        End Try
    End Sub

    Private Sub InsertBulkDataToServer(ConnString As String, DestinationTable As String, dtData As DataTable)
        Using cn As New System.Data.SqlClient.SqlConnection(ConnString)
            cn.Open()
            Using copy As New System.Data.SqlClient.SqlBulkCopy(cn)

                copy.DestinationTableName = DestinationTable
                copy.NotifyAfter = 1000
                AddHandler copy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                copy.ColumnMappings.Add("XMLJobID", "XMLJobID")
                copy.ColumnMappings.Add("VENDOR", "VENDOR")
                copy.ColumnMappings.Add("MO", "MO")
                copy.ColumnMappings.Add("NENAME", "NENAME")
                copy.ColumnMappings.Add("ObjectName", "ObjectName")
                copy.ColumnMappings.Add("ParameterName", "ParameterName")
                copy.ColumnMappings.Add("ParameterValue", "ParameterValue")
                copy.ColumnMappings.Add("RollBackValue", "RollBackValue")

                copy.WriteToServer(dtData)
            End Using
        End Using
    End Sub

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlClient.SqlRowsCopiedEventArgs)
        lblMessage.Text = "Completed - Count: " & args.RowsCopied.ToString
    End Sub

    Private Sub InputDataGrid_ProcessGridKey(sender As Object, e As KeyEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If e.KeyData = Keys.Delete Then
                If XtraMessageBox.Show("Are you sure to delete selected input data rows?", "Delete Input Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim rIndex() As Integer = gvInputData.GetSelectedRows()
                    If rIndex.Length > 0 Then
                        For i As Integer = 0 To rIndex.Length - 1
                            DeleteInputDataBulk(CInt(gvInputData.GetRowCellValue(rIndex(i), "XMLInputID")))
                        Next
                        LoadInputData()
                    End If
                    e.Handled = True

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

    Private Sub tsmi_DeleteSelectedRows_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteSelectedRows.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If XtraMessageBox.Show("Are you sure to delete selected input data rows?", "Delete Input Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim rIndex() As Integer = gvInputData.GetSelectedRows()
                If rIndex.Length > 0 Then
                    For i As Integer = 0 To rIndex.Length - 1
                        DeleteInputDataBulk(CInt(gvInputData.GetRowCellValue(rIndex(i), "XMLInputID")))
                    Next
                    LoadInputData()
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

    Private Sub tsmi_RenameXmlJob_Click(sender As Object, e As EventArgs) Handles tsmi_RenameXmlJob.Click
        Try
            cmXmlJobs.Close()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
            Dim xmlJObOwner As String = CStr(gvXmlJobs.GetFocusedRowCellValue("XMLJobOwner"))

            Dim isPowerUser As Boolean = False
            If (xmlJObOwner.ToLower <> Environment.UserName.ToLower) Then
                If configMgr.User.IsPowerUser = True Then
                    isPowerUser = True
                Else
                    XtraMessageBox.Show("Only the job owner or the power user can rename the job name", "Rename XML Job!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    isPowerUser = False
                    Exit Sub
                End If
            Else
                'template owner
                isPowerUser = True
            End If

            If (isPowerUser = True) Then
                Dim renamedJobName As String = XtraInputBox.Show("Rename XML Job Name: ", "Rename XML Job", CStr(gvXmlJobs.GetFocusedRowCellValue("XMLJobName")))
                If renamedJobName = "" Then
                    Exit Sub
                Else
                    RenameXmlJob(xmlJobID, renamedJobName)
                    LoadXmlJobList()
                    gvXmlJobs.FocusedRowHandle = gvXmlJobs.LocateByValue("XMLJobID", xmlJobID)
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvProvisionResult_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles gvProvisionResult.CustomRowCellEdit
        If e.Column.FieldName = "GetReport" Then
            If CBool(gvProvisionResult.GetRowCellValue(gvProvisionResult.FocusedRowHandle, "ReportPathUrlData")) = True Then
                e.RepositoryItem = ribeGetReportEnabled
            Else
                e.RepositoryItem = ribeGetReportDisabled
            End If
        End If
    End Sub

    Private Sub bgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork
        Try
            If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
                Call_QueryCMDataTask()
            ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                Call_ImportXmlJobEricsson()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorker.ProgressChanged, bgWorkerRollBack.ProgressChanged
        Try
            xmlJobID = CInt(e.UserState.ToString.Split(":")(0))
            Dim jobStatus As String = e.UserState.ToString.Split(":")(1).ToString
            gvXmlJobs.SetRowCellValue(gvXmlJobs.LocateByValue("XMLJobID", xmlJobID), "RESTAPIStatus", jobStatus)
            gcXmlJobs.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted, bgWorkerRollBack.RunWorkerCompleted
        Try
            LoadXmlJobList()
            Try
                Call gvXmlJobs_RowCellStyle(gvXmlJobs, Nothing)
            Catch
            End Try

            LoadXmlJobLog(xmlJobID)

            LoadProvisionResult()
            xtcSubTop.SelectedTabPageIndex = 4

            Timer1_Tick(Nothing, Nothing)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            bgWorker.CancelAsync()
            bgWorker.Dispose()

            bgWorkerRollBack.CancelAsync()
            bgWorkerRollBack.Dispose()
        End Try
    End Sub

    Private Sub bgWorkerRollBack_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorkerRollBack.DoWork
        Try
            If xmlVendor.ToUpper = Vendor.HUAWEI.ToString Then
                Call_QueryCMDataTask_RollBack()
            ElseIf xmlVendor.ToUpper = Vendor.ERICSSON.ToString Then
                Call_ImportXmlJobEricsson_Rollback()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnXmlJobsInsert_Click(sender As Object, e As EventArgs) Handles btnXmlJobsInsert.Click
        Try
            If gvXmlJobs.RowCount > 0 Then
                Dim objXmlJobInsert As New dlgXmlJobInsert()
                objXmlJobInsert.xmlJobID = CInt(gvXmlJobs.GetFocusedRowCellValue("XMLJobID"))
                objXmlJobInsert.xmlJobName = gvXmlJobs.GetFocusedRowCellValue("XMLJobName").ToString
                objXmlJobInsert.xmlJobVendor = gvXmlJobs.GetFocusedRowCellValue("XMLVendor").ToString
                objXmlJobInsert.ShowDialog()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

End Class