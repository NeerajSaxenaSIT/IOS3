Imports System.IO
Imports dotnetCHARTING.WinForms
Imports IOS.Library
Imports MapInfo.Styles

Public Class frmEvent

#Region "Variables"

    Private StatusOfMouseClickOnMessage As Boolean = False
    Private conn_IOS As String = ""
    Private sql As String = ""
    Private EventID As Integer
    Private DtId As Integer
    Private dtFailure As New DataTable
    Private dtChartAndGridData As New DataTable
    Private dsDataMessageList As New DataSet
    Private ss2G As String = Nothing
    Private ss3G As String = Nothing
    Private ss4G As String = Nothing
    Private isEventMapped As Boolean = False

    Public eventMapLayerName As String = "DT_EventGrid"
    Public IsNeedToFillMessageList As Boolean = True

#End Region

#Region "Form Events"

    Private Sub frmEvent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Form EventLoad")
        ConfigurEventForm("frmEvent")
        Me.Location = New System.Drawing.Point(5, 60)
        Me.SuspendLayout()
        Me.ResumeLayout()
        Me.BringToFront()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit Form EventLoad")
    End Sub

    Private Sub frmEvent_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If (dlgMappingSelection.Visible) Then
            dlgMappingSelection.BringToFront()
            dlgMappingSelection.TopMost = True
        End If
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub frmEvent_ResizeBegin(sender As Object, e As EventArgs) Handles MyBase.ResizeBegin
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start ResizeBegin Event Form")
        Me.SuspendLayout()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit ResizeBegin Event Form")
    End Sub

    Private Sub frmEvent_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start ResizeEnd Event Form")
        Me.ResumeLayout()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit ResizeEnd Event Form")
    End Sub

#End Region

#Region "Private Methods"

    Private Sub ConfigurEventForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                tsmiDownLoadFile, SelectAllToolStripMenuItem, MessageAll2GToolStripMenuItem, MessageAll3GToolStripMenuItem, SelectAll4GToolStripMenuItem, SaveSettingToolStripMenuItem, ClearSettingToolStripMenuItem
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

    Private Sub HideShowGridColumn()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start HideShowGridColumn")
        For index As Integer = 0 To gvEventsDataGrid.Columns.Count - 1
            gvEventsDataGrid.Columns(index).Visible = True
        Next
        If rdbtn2G.Checked And rdbtn3G.Checked And rdbtn4G.Checked Then
            For index As Integer = 0 To gvEventsDataGrid.Columns.Count - 1
                gvEventsDataGrid.Columns(index).Visible = True
            Next
        ElseIf Not rdbtn2G.Checked And Not rdbtn3G.Checked And Not rdbtn4G.Checked Then
            For index As Integer = 0 To gvEventsDataGrid.Columns.Count - 1
                gvEventsDataGrid.Columns(index).Visible = False
            Next
        ElseIf rdbtn3G.Checked And rdbtn2G.Checked And Not rdbtn4G.Checked Then
            Hide4GColumn()
        ElseIf rdbtn3G.Checked And rdbtn4G.Checked And Not rdbtn2G.Checked Then
            Hide2GColumn()
        ElseIf rdbtn2G.Checked And rdbtn4G.Checked And Not rdbtn3G.Checked Then
            Hide3GColumn()
        ElseIf rdbtn4G.Checked Then
            Hide2GColumn()
            Hide3GColumn()
        ElseIf rdbtn3G.Checked Then
            Hide2GColumn()
            Hide4GColumn()
        ElseIf rdbtn2G.Checked Then
            Hide4GColumn()
            Hide3GColumn()
        End If
        gcEventsDataGrid.Visible = True
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit HideShowGridColumn")
    End Sub

    Private Sub Hide2GColumn()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start Hide2GColumn")
        Dim matchItems As IEnumerable(Of DevExpress.XtraGrid.Columns.GridColumn) = From w In gvEventsDataGrid.Columns
                                                                                   Where w.FieldName.Contains(IOSEntityKeys2G.SC_CellID + "_2G") Or w.FieldName.Contains("MsgL3") Or w.FieldName.Contains(IOSEntityKeys2G.SC_LAC) Or w.FieldName.Contains(IOSEntityKeys2G.RXLEVSUB) Or w.FieldName.Contains(IOSEntityKeys2G.RXQUALSUB) Or w.FieldName.Contains("BCCH") Or w.FieldName.Contains("NCC") Or w.FieldName.Contains("RxLev") Or w.FieldName.Contains("BCC")
                                                                                   Select w
        For Each Item As DevExpress.XtraGrid.Columns.GridColumn In matchItems
            Item.Visible = False
        Next
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit Hide2GColumn")
    End Sub

    Private Sub Hide3GColumn()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start Hide3GColumn")
        Dim matchItems As IEnumerable(Of DevExpress.XtraGrid.Columns.GridColumn) = From w In gvEventsDataGrid.Columns
                                                                                   Where w.FieldName.Contains(IOSEntityKeys3G.CHANNELTYPE + "_3G") Or w.FieldName.Contains(IOSEntityKeys3G.MSGRRC + "_3G") Or w.FieldName.Contains(IOSEntityKeys3G.UARFCN) Or w.FieldName.Contains("AS") Or w.FieldName.Contains("NB")
                                                                                   Select w
        For Each Item As DevExpress.XtraGrid.Columns.GridColumn In matchItems
            Item.Visible = False
        Next
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit Hide3GColumn")
    End Sub

    Private Sub Hide4GColumn()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start Hide4GColumn")
        Dim matchItems2 As IEnumerable(Of DevExpress.XtraGrid.Columns.GridColumn) = From w In gvEventsDataGrid.Columns
                                                                                    Where w.FieldName.Contains(IOSEntityKeys4G.SC_CELLID + "_4G") Or w.FieldName.Contains(IOSEntityKeys4G.MSGRRC + "_4G") Or w.FieldName.Contains(IOSEntityKeys4G.CHANNELTYPE + "_4G") Or w.FieldName.Contains(IOSEntityKeys4G.SC_EARFCN) Or w.FieldName.Contains(IOSEntityKeys4G.SC_PCI) Or w.FieldName.Contains(IOSEntityKeys4G.SC_RSRP) Or w.FieldName.Contains(IOSEntityKeys4G.SC_RSRQ) Or w.FieldName.Contains(IOSEntityKeys4G.SC_TAC) Or w.FieldName.StartsWith("PCI") Or w.FieldName.StartsWith("RSRQ") Or w.FieldName.Contains("RSRP") Or w.FieldName.Contains("TAC")
                                                                                    Select w
        For Each Item As DevExpress.XtraGrid.Columns.GridColumn In matchItems2
            Item.Visible = False
        Next
        For value As Integer = 1 To 6
            Dim nValue As String
            Dim dValue As String
            nValue = "N" + value.ToString()
            dValue = "D" + value.ToString()
            Dim matchItems3 = From w In gvEventsDataGrid.Columns
                              Where w.FieldName.Contains(nValue + "_PCI") Or w.FieldName.Contains(nValue + "_RSRP") Or w.FieldName.Contains(nValue + "_RSRQ") Or w.FieldName.Contains(nValue + "_Dist") Or w.FieldName.Contains(nValue + "_CellId") Or w.FieldName.Contains(nValue + "_TAC") Or w.FieldName.Contains(dValue + "_PCI") Or w.FieldName.Contains(dValue + "_RSRQ") Or w.FieldName.Contains(dValue + "_RSRP") Or w.FieldName.Contains(dValue + "_Dist") Or w.FieldName.Contains(dValue + "_CellId") Or w.FieldName.Contains(dValue + "_TAC")
                              Select w
            For Each Item As DevExpress.XtraGrid.Columns.GridColumn In matchItems3
                Item.Visible = False
            Next
        Next
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit Hide4GColumn")
    End Sub

    Private Sub MapEventDataRowOnMapForm(ByVal id As Integer)
        Me.Cursor = Cursors.WaitCursor
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start MapEventDataRowOnMapForm")
        Try
            If (isEventMapped) Then
                frmMapWindow.SelectFeatureOnEventFormClick(id)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Call Select FeatureOnEvent")
            Else
                frmMapWindow.SetStatus("Event data is not mapped on MapControl")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus("There is an error. Not able to select feature on map.")
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit mapEventDataRowOnmapForm")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GetMessageString()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start MessageString ")
        ss2G = ""
        ss3G = ""
        ss4G = ""
        Dim rowIndex As Integer = 0
        For Each Item As System.Data.DataRowView In vCblMessage.CheckedItems
            If Item.DataView.Count > 0 Then
                Dim rows() As DataRow = dsDataMessageList.Tables(0).Select("msg='" & Item(0).ToString & "'")

                If rows.Length > 0 Then
                    If dsDataMessageList.Tables(0).Rows(rowIndex)("TableName").ToString = "2G" Then
                        ss2G += ",'" + Item(0).ToString + "'"
                    ElseIf dsDataMessageList.Tables(0).Rows(rowIndex)("TableName").ToString = "3G" Then
                        ss3G += ",'" + Item(0).ToString + "'"
                    Else
                        ss4G += ",'" + Item(0).ToString + "'"
                    End If
                Else
                End If
            End If
            rowIndex += 1
        Next

        rowIndex = 0

        If Not ss2G = "" Then
            ss2G = ss2G.Substring(1)
        End If

        If Not ss3G = "" Then
            ss3G = ss3G.Substring(1)
        End If
        If Not ss4G = "" Then
            ss4G = ss4G.Substring(1)
        End If
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit MessageString ")
    End Sub

    Private Sub CreateXaxisMarkerForEvent()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start CreateXaxisMarkerForEvent")
        Dim objIOSChartManager As New IOSChartManager(Me.Chart1)
        Dim eventData As DataView = New DataView(dtFailure, "EventId=" & EventID, "", DataViewRowState.CurrentRows)
        If (eventData.Count > 0) Then
            objIOSChartManager.CreateXaxisMarker(eventData.ToTable(), New Line(Color.OrangeRed, 3), "Technology", "TimeStamp", False, False, "Service")
        End If
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit CreateXaxisMarkerForEvent")
    End Sub

#End Region

#Region "Analysis"

    Private Sub btn_Update_Click(sender As Object, e As EventArgs) Handles btn_Update.Click
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start btn_Update_Click")
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If gvDriveTestModule.RowCount > 0 Then
                If gvDriveTestModule.IsRowSelected(0) = True Then
                    Me.EventID = dtFailure.Rows(0)(0)
                    Me.DtId = dtFailure.Rows(0)(1)
                End If
                If (Me.EventID = 0 Or Me.DtId = 0) Then
                    frmMapWindow.SetStatus("Please select any event!!!!")
                Else
                    Dim objIOSEvent As New IOSEvent(Me.EventID, Me.DtId, txtComments.Text, If(cmbStatus.SelectedIndex = -1, Nothing, cmbStatus.SelectedItem.ToString), If(cmbAnalysisAccept.SelectedIndex = -1, Nothing, cmbAnalysisAccept.SelectedItem.ToString), If(cmbProposalAccept.SelectedIndex = -1, Nothing, cmbProposalAccept.SelectedItem.ToString), If(Not DTPickerImplementationDate.EditValue.HasValue, Nothing, DTPickerImplementationDate.EditValue), Nothing, Nothing, Nothing)
                    Dim status As Integer = objIOSEvent.UpdateEvent(conn_IOS)
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Updated Event")
                    frmMapWindow.SetStatus("Status Updated!!!!")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus("There is an error. Status is not Updated")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit btn_Update_Click")
            Me.Cursor = Cursors.Default
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

#End Region

#Region "Charts"

    Private Sub vCblMessage_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles vCblMessage.ItemCheck
        Try
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start vCblMessage_ItemChecked")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If gvDriveTestModule.RowCount > 0 AndAlso Not StatusOfMouseClickOnMessage Then
                CreateXaxisMarkerForMessage()
            End If
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Created X marker Message ")
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Chart1.RefreshChart()
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit cCbiMessage_ItemChecked ")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Public Sub CreateXaxisMarkerForMessage()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start CreateXaxisMarkerForMessage")
        GetMessageString()
        Dim columnName As String = Nothing
        If (dtChartAndGridData Is Nothing Or dtChartAndGridData.Rows.Count = 0) Then
            dtChartAndGridData = IOSEventHelper.GetEventData(dtChartAndGridData, Me.EventID, Me.DtId, conn_IOS)
        End If
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Get ChartAndGridData from IOSEventHelper.GetEventDate")
        Dim dv As DataView = dtChartAndGridData.Copy().DefaultView()
        Dim msgDAta As DataTable = Nothing
        Dim msgFilter As String = String.Empty
        If (chk_2GCharts.Checked) Then
            If Not String.IsNullOrEmpty(ss2G) Then
                msgFilter += "msgL3 in (" + ss2G + ")"
                columnName = "MsgL3"
            End If
        End If
        If (chk_3GCharts.Checked) Then
            If Not String.IsNullOrEmpty(ss3G) Then
                If (String.IsNullOrEmpty(msgFilter)) Then
                    msgFilter += "MsgRRC_3G in (" + ss3G + ")"
                Else
                    msgFilter += "and MsgRRC_3G in (" + ss3G + ")"
                End If
                columnName = "MsgRRC_3G"
            End If
        End If
        If (chk_4GCharts.Checked) Then
            If Not String.IsNullOrEmpty(ss4G) Then
                If (String.IsNullOrEmpty(msgFilter)) Then
                    msgFilter += "MsgRRC_4G in (" + ss4G + ")"
                Else
                    msgFilter += "and MsgRRC_4G in (" + ss4G + ")"
                End If
                columnName = "MsgRRC_4G"
            End If
        End If
        If Not (String.IsNullOrEmpty(msgFilter)) Then
            dv.RowFilter = msgFilter
            msgDAta = dv.ToTable()
        Else
            Chart1.XAxis.Markers.Clear()
            CreateXaxisMarkerForEvent()
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit CreateXaxisMarkerForMessage")
            Exit Sub
        End If
        Dim objIOSChartManager As New IOSChartManager(Chart1)
        objIOSChartManager.CreateXaxisMarker(msgDAta, New Line(Color.Green, 2), columnName, "timestamp", True, False, "msgL3")
        CreateXaxisMarkerForEvent()
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit CreateXaxisMarkerForMessage")
    End Sub

    Private Sub vCblMessage_MouseDown(sender As Object, e As MouseEventArgs) Handles vCblMessage.MouseDown
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start vCblMessage_MouseDown")
        If e.Button = MouseButtons.Right Then
            vCblMessage.CheckOnClick = False
            StatusOfMouseClickOnMessage = True
        ElseIf e.Button = MouseButtons.Left Then
            vCblMessage.CheckOnClick = True
            StatusOfMouseClickOnMessage = False
        End If
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit vCblMessage_MouseDown")
    End Sub


    Public Sub FillData(ByVal EventID As Integer, ByVal DtId As Integer)
        Try
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start FaillData")
            Dim objEvent As IOSEvent = IOSEventHelper.GetEvent(EventID, DtId, conn_IOS) ''todo

            If (Not objEvent Is Nothing) Then
                txtAnalysisDesc.Text = objEvent.AnalysisDesc
                lblImportedDate.Text = If(Not (objEvent.ImportedDate.HasValue), "", objEvent.ImportedDate.Value.ToString("dd/MM/yyyy"))
                lblRespEngg.Text = objEvent.RespEngg
                txtComments.Text = objEvent.AnalysisComment
                Dim status As Boolean = False
                For Each Item As Object In cmbStatus.Properties.Items
                    If (Item.ToString.Trim().ToLower() = objEvent.Status.Trim().ToLower()) Then
                        cmbStatus.SelectedIndex = cmbStatus.Properties.Items.IndexOf(Item)
                        status = True
                    End If
                Next
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Find cmbStatusSelectedIndex")
                If Not (status) Then
                    cmbStatus.SelectedIndex = -1
                End If
                status = False
                For Each Item As Object In cmbAnalysisAccept.Properties.Items
                    If (Item.ToString.Trim().ToLower() = objEvent.AcceptanceAnalysis.Trim().ToLower()) Then
                        cmbAnalysisAccept.SelectedIndex = cmbAnalysisAccept.Properties.Items.IndexOf(Item)
                        status = True
                    End If
                Next
                If Not (status) Then
                    cmbAnalysisAccept.SelectedIndex = -1
                End If
                status = False
                For Each Item As Object In cmbProposalAccept.Properties.Items
                    If (Item.ToString.Trim().ToLower() = objEvent.AcceptanceProposal.Trim().ToLower()) Then
                        cmbProposalAccept.SelectedIndex = cmbProposalAccept.Properties.Items.IndexOf(Item)
                        status = True
                    End If
                Next
                If Not (status) Then
                    cmbProposalAccept.SelectedIndex = -1
                End If
                '' cmbStatus.Text = objEvent.Status
                'cmbAnalysisAccept.Text = objEvent.AcceptanceAnalysis
                ' cmbProposalAccept.Text = objEvent.AcceptanceProposal
                If Not (objEvent.ImplementationDate.HasValue) Then
                    DTPickerImplementationDate.EditValue = DateTime.Now
                    DTPickerImplementationDate.Text = ""
                Else
                    DTPickerImplementationDate.Text = objEvent.ImplementationDate.Value.ToString("dd/MM/yyyy")
                    DTPickerImplementationDate.EditValue = objEvent.ImplementationDate.Value
                End If
            Else
                frmMapWindow.SetStatus("No data found for Selected EventId")
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit FillData")
        End Try
    End Sub

    Private Sub BindMessagaList(ByVal eventid As Integer, ByVal dtid As Integer)
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start BindMessageList")
        dsDataMessageList = IOS.DataLibrary.clsSQLCommands.GetEventMessageList(conn_IOS, eventid, dtid)
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Get DtMessageList")
        If (Not (dsDataMessageList Is Nothing) And dsDataMessageList.Tables.Count > 0) Then
            vCblMessage.DataSource = dsDataMessageList.Tables(0)
            vCblMessage.DisplayMember = "MSG"
            vCblMessage.ValueMember = "TableName"
        End If
        Dim path As String = GetUserDataPath()
        path = path & "\\MessageCofing.xml"
        Dim ds As New DataSet("message")
        If File.Exists(path) Then
            ds.ReadXml(path)
            If (Not ds Is Nothing) And ds.Tables.Count > 0 Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    For Each c As DevExpress.XtraEditors.Controls.CheckedListBoxItem In vCblMessage.Items
                        If (c.Value.ToString = Convert.ToString(dr("msg"))) Then
                            c.CheckState = CheckState.Checked
                        End If
                    Next
                Next
            End If
        End If
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit BindMessageList ")
    End Sub

    Public Sub CreateChart(ByVal EventID As Integer, ByVal dtid As Integer)
        Try
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start CreateChart")
            Me.SuspendLayout()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Chart1.SuspendLayout()
            Dim yaxis2 As New IOSAxis()
            yaxis2.Orientation = Orientation.Left
            yaxis2.ScaleRange.ValueHigh = -10
            yaxis2.ScaleRange.ValueLow = -120
            yaxis2.Interval = 10
            yaxis2.ElementMarkerType = ElementMarkerType.Circle
            Dim yaxis3 As New IOSAxis
            yaxis3.Orientation = Orientation.Right
            yaxis3.ScaleRange.ValueHigh = 0
            yaxis3.ScaleRange.ValueLow = -30
            yaxis3.Interval = 5
            yaxis3.ElementMarkerType = ElementMarkerType.Triangle

            Dim chartData As New DataTable()
            Dim sqlChart As String = Nothing
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin Set ChartData from IOSEventHelper.GetEventData")
            dtChartAndGridData = IOSEventHelper.GetEventData(dtChartAndGridData, EventID, dtid, conn_IOS)
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Set ChartData from IOSEventHelper.GetEventData")
            chartData = dtChartAndGridData
            Dim ChartElements As New List(Of String)
            ChartElements.Add("TimeStamp")
            If chk_2GCharts.Checked = True Then
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Set 2GChart yaxis Elements")
                yaxis2.ElementListToApply.Add("RxLevSub")
                ChartElements.Add("RxLevSub")
                If (vchkNeighborRadioData.Checked) Then
                    yaxis2.ElementListToApply.Add("N1_RxLev")
                    yaxis2.ElementListToApply.Add("N2_RxLev")
                    yaxis2.ElementListToApply.Add("N3_RxLev")
                    yaxis2.ElementListToApply.Add("N4_RxLev")
                    yaxis2.ElementListToApply.Add("N5_RxLev")
                    yaxis2.ElementListToApply.Add("N6_RxLev")
                    ChartElements.Add("N1_RxLev")
                    ChartElements.Add("N2_RxLev")
                    ChartElements.Add("N3_RxLev")
                    ChartElements.Add("N4_RxLev")
                    ChartElements.Add("N5_RxLev")
                    ChartElements.Add("N6_RxLev")
                End If
            End If
            If chk_3GCharts.Checked = True Then
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Set 3GChart yaxis Elements")
                yaxis2.ElementListToApply.Add("AS1_RSCP")
                yaxis2.ElementListToApply.Add("AS2_RSCP")
                yaxis2.ElementListToApply.Add("AS3_RSCP")
                yaxis3.ElementListToApply.Add("AS1_Ecno")
                yaxis3.ElementListToApply.Add("AS2_Ecno")
                yaxis3.ElementListToApply.Add("AS3_Ecno")

                ChartElements.Add("AS1_RSCP")
                ChartElements.Add("AS2_RSCP")
                ChartElements.Add("AS3_RSCP")
                ChartElements.Add("AS1_Ecno")
                ChartElements.Add("AS2_Ecno")
                ChartElements.Add("AS3_Ecno")
                If (vchkNeighborRadioData.Checked) Then
                    yaxis2.ElementListToApply.Add("NB1_RSCP")
                    yaxis2.ElementListToApply.Add("NB2_RSCP")
                    yaxis2.ElementListToApply.Add("NB3_RSCP")
                    yaxis2.ElementListToApply.Add("NB4_RSCP")
                    yaxis2.ElementListToApply.Add("NB5_RSCP")
                    yaxis2.ElementListToApply.Add("NB6_RSCP")
                    yaxis3.ElementListToApply.Add("NB1_Ecno")
                    yaxis3.ElementListToApply.Add("NB2_Ecno")
                    yaxis3.ElementListToApply.Add("NB3_Ecno")
                    yaxis3.ElementListToApply.Add("NB4_Ecno")
                    yaxis3.ElementListToApply.Add("NB5_Ecno")
                    yaxis3.ElementListToApply.Add("NB6_Ecno")

                    ChartElements.Add("NB1_RSCP")
                    ChartElements.Add("NB2_RSCP")
                    ChartElements.Add("NB3_RSCP")
                    ChartElements.Add("NB4_RSCP")
                    ChartElements.Add("NB5_RSCP")
                    ChartElements.Add("NB6_RSCP")

                    ChartElements.Add("NB1_Ecno")
                    ChartElements.Add("NB2_Ecno")
                    ChartElements.Add("NB3_Ecno")
                    ChartElements.Add("NB4_Ecno")
                    ChartElements.Add("NB5_Ecno")
                    ChartElements.Add("NB6_Ecno")
                End If
            End If
            If chk_4GCharts.Checked = True Then
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Set 4GChart yaxis Elements")
                yaxis2.ElementListToApply.Add("SC_RSRP")
                yaxis3.ElementListToApply.Add("SC_RSRQ")

                ChartElements.Add("SC_RSRP")
                ChartElements.Add("SC_RSRQ")

                If (vchkNeighborRadioData.Checked) Then
                    yaxis2.ElementListToApply.Add("N1_RSRP")
                    yaxis2.ElementListToApply.Add("N2_RSRP")
                    yaxis2.ElementListToApply.Add("N3_RSRP")
                    yaxis2.ElementListToApply.Add("N4_RSRP")
                    yaxis2.ElementListToApply.Add("N5_RSRP")
                    yaxis2.ElementListToApply.Add("N6_RSRP")

                    yaxis3.ElementListToApply.Add("N1_RSRQ")
                    yaxis3.ElementListToApply.Add("N2_RSRQ")
                    yaxis3.ElementListToApply.Add("N3_RSRQ")
                    yaxis3.ElementListToApply.Add("N4_RSRQ")
                    yaxis3.ElementListToApply.Add("N5_RSRQ")
                    yaxis3.ElementListToApply.Add("N6_RSRQ")

                    yaxis2.ElementListToApply.Add("D1_RSRP")
                    yaxis2.ElementListToApply.Add("D2_RSRP")
                    yaxis2.ElementListToApply.Add("D3_RSRP")
                    yaxis2.ElementListToApply.Add("D4_RSRP")
                    yaxis2.ElementListToApply.Add("D5_RSRP")
                    yaxis2.ElementListToApply.Add("D6_RSRP")

                    yaxis3.ElementListToApply.Add("D1_RSRQ")
                    yaxis3.ElementListToApply.Add("D2_RSRQ")
                    yaxis3.ElementListToApply.Add("D3_RSRQ")
                    yaxis3.ElementListToApply.Add("D4_RSRQ")
                    yaxis3.ElementListToApply.Add("D5_RSRQ")
                    yaxis3.ElementListToApply.Add("D6_RSRQ")

                    ChartElements.Add("N1_RSRP")
                    ChartElements.Add("N2_RSRP")
                    ChartElements.Add("N3_RSRP")
                    ChartElements.Add("N4_RSRP")
                    ChartElements.Add("N5_RSRP")
                    ChartElements.Add("N6_RSRP")

                    ChartElements.Add("N1_RSRQ")
                    ChartElements.Add("N2_RSRQ")
                    ChartElements.Add("N3_RSRQ")
                    ChartElements.Add("N4_RSRQ")
                    ChartElements.Add("N5_RSRQ")
                    ChartElements.Add("N6_RSRQ")

                    ChartElements.Add("D1_RSRP")
                    ChartElements.Add("D2_RSRP")
                    ChartElements.Add("D3_RSRP")
                    ChartElements.Add("D4_RSRP")
                    ChartElements.Add("D5_RSRP")
                    ChartElements.Add("D6_RSRP")

                    ChartElements.Add("D1_RSRQ")
                    ChartElements.Add("D2_RSRQ")
                    ChartElements.Add("D3_RSRQ")
                    ChartElements.Add("D4_RSRQ")
                    ChartElements.Add("D5_RSRQ")
                    ChartElements.Add("D6_RSRQ")
                End If
            End If
            If chk_2GCharts.Checked = False And chk_3GCharts.Checked = False And chk_4GCharts.Checked = False Then
                chartData = dtChartAndGridData.Clone()
            End If
            Dim listOfYAxis As New List(Of IOSAxis)
            listOfYAxis.Add(yaxis2)
            listOfYAxis.Add(yaxis3)
            Dim objIOSChartManager As New IOSChartManager(Chart1, chartData, ChartElements, "Signal Strength 2G/3G/4G", listOfYAxis)
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin Set ChartData from IOSEventHelper.GetEventData")
            objIOSChartManager.CreateChartOnTimeStamp(ChartType.Combo, SeriesType.Marker, SeriesType.Line, 6)
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Call objIOSChartManager.CreateChartOnTimeStamp")
            Chart1.TitleBox.Label.Text = "EventID " & EventID ' Chart1.TitleBox.Label.Text + " <br> 
            Chart1.DefaultSeries.EmptyElement.Mode = If((cmbElementMode.SelectedIndex = 1), EmptyElementMode.None, EmptyElementMode.Ignore)

            CreateXaxisMarkerForMessage()
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Call CreateXaxisMarkerForMessage")
            Chart1.RefreshChart()
            Chart1.ResumeLayout()
            '' Application.DoEvents()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "End CreateChart")
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start Chart1_Click")
            If (isEventMapped) Then
                Dim hit As HitTestInfo = Nothing
                Try
                    hit = Chart1.HitTest()
                Catch ex As Exception

                End Try

                If hit IsNot Nothing AndAlso TypeOf (hit.Object) Is Element Then
                    Dim el As Element = CType(hit.Object, Element)
                    Dim columnName As String = hit.Series.Name
                    Dim xValue As DateTime = el.XDateTime
                    Dim yValue As Double = el.YValue

                    Dim ty As Type = dtChartAndGridData.Columns(columnName).DataType
                    Dim tempData As DataTable = dtChartAndGridData.Select(columnName & " IS NOT NULL").CopyToDataTable()
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After selected dtChartAndGridData where IS NOT NULL column value")
                    Dim matchItems = Nothing
                    If (ty.FullName = "System.Int64") Then
                        matchItems = From w In tempData.AsEnumerable()
                                     Where w.Field(Of DateTime)("TimeStamp") = xValue And w.Field(Of Long)(columnName) = yValue
                                     Select w
                    ElseIf (ty.FullName = "System.Int32") Then
                        matchItems = From w In tempData.AsEnumerable()
                                     Where w.Field(Of DateTime)("TimeStamp") = xValue And w.Field(Of Integer)(columnName) = yValue
                                     Select w
                    ElseIf (ty.FullName = "System.Int16") Then
                        matchItems = From w In tempData.AsEnumerable()
                                     Where w.Field(Of DateTime)("TimeStamp") = xValue And w.Field(Of Short)(columnName) = yValue
                                     Select w
                    ElseIf (ty.FullName = "System.Single") Then
                        matchItems = From w In tempData.AsEnumerable()
                                     Where w.Field(Of DateTime)("TimeStamp") = xValue And Math.Round(w.Field(Of System.Single)(columnName)) = Math.Round(yValue)
                                     Select w
                    ElseIf (ty.FullName = "System.Double") Then
                        matchItems = From w In tempData.AsEnumerable()
                                     Where w.Field(Of DateTime)("TimeStamp") = xValue And Math.Round(w.Field(Of Double)(columnName)) = Math.Round(yValue)
                                     Select w
                    Else
                        matchItems = From w In tempData.AsEnumerable()
                                     Where w.Field(Of DateTime)("TimeStamp") = xValue And w.Field(Of String)(columnName) = yValue
                                     Select w
                    End If

                    For Each Item As DataRow In matchItems
                        MapEventDataRowOnMapForm(Convert.ToInt32(Item("id")))
                    Next
                End If
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Set MapEventDataRowOnMapForm")
            Else
                frmMapWindow.SetStatus("Event data is not mapped on MapControl")
            End If
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit Chart1_Click")
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub chk_Charts_CheckedChanged(sender As Object, e As EventArgs) Handles chk_2GCharts.CheckedChanged, chk_3GCharts.CheckedChanged, chk_4GCharts.CheckedChanged, vchkNeighborRadioData.CheckedChanged
        Try

            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start chk_Chart CheckedChanged")
            If gvDriveTestModule.RowCount > 0 Then
                If gvDriveTestModule.IsRowSelected(0) = True Then
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateChart")
                    CreateChart(dtFailure.Rows(0)(0), dtFailure.Rows(0)(1))
                Else
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateChart")
                    CreateChart(Me.EventID, Me.DtId)
                End If
            End If
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit after create chart")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbElementMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbElementMode.SelectedIndexChanged
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start VElementMode Select Change")
        If gvDriveTestModule.RowCount > 0 Then
            If cmbElementMode.SelectedIndex = 0 Then
                Chart1.DefaultSeries.EmptyElement.Mode = EmptyElementMode.Ignore
            Else
                Chart1.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
            End If
            Chart1.RefreshChart()
        End If
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit VElementMode Select Change")
    End Sub

#End Region

#Region "Grids TabPage"

    Private Sub rdbtn2G_CheckedChanged(sender As Object, e As EventArgs) Handles rdbtn2G.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start rdbtn2G CheckedChanged")
            If gvDriveTestModule.RowCount > 0 Then
                If gvDriveTestModule.IsRowSelected(0) = True Then
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                    FillDataGrid(dtFailure.Rows(0)(0), Me.DtId)
                Else
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                    FillDataGrid(EventID, Me.DtId)
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        Finally
            Me.Cursor = Cursors.Default
        End Try
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Filled DataGrid rbdtn2G")
    End Sub

    Private Sub rdbtn3G_CheckedChanged(sender As Object, e As EventArgs) Handles rdbtn3G.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "rdbtn3G_CheckedChanged")
            If gvDriveTestModule.RowCount > 0 Then
                If gvDriveTestModule.IsRowSelected(0) = True Then
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                    FillDataGrid(dtFailure.Rows(0)(0), Me.DtId)
                Else
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                    FillDataGrid(EventID, Me.DtId)
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        Finally
            Me.Cursor = Cursors.Default
        End Try
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Filled DataGrid rbdtn3G")
    End Sub

    Private Sub rdbtn4G_CheckedChanged(sender As Object, e As EventArgs) Handles rdbtn4G.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start rdbtn4G CheckedChanged")
            If gvDriveTestModule.RowCount > 0 Then
                If gvDriveTestModule.IsRowSelected(0) = True Then
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                    FillDataGrid(dtFailure.Rows(0)(0), Me.DtId)
                Else
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                    FillDataGrid(EventID, Me.DtId)
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        Finally
            Me.Cursor = Cursors.Default
        End Try
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Filled DataGrid rbdtn4G")
    End Sub

    Private Sub btnMapEvent_Click(sender As Object, e As EventArgs) Handles btnMapEvent.Click
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start btnMapEvent_Click")
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If dtChartAndGridData Is Nothing Then
                dtChartAndGridData = IOSEventHelper.GetEventData(Me.dtChartAndGridData, Me.EventID, Me.DtId, Me.conn_IOS)
            End If
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Get Event Data")
            'Dim result As String = DateTime.ParseExact("24/5/2009 3:40:00 AM", "dd/MM/yyyy hh:mm:ss.fff", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy hh:mm:ss")
            If (dtChartAndGridData IsNot Nothing) Then
                If (dtChartAndGridData.Rows.Count > 0) Then
                    Dim tempData As New DataTable()
                    gcEventsDataGrid.Refresh()
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Getting Style from DT_EventGrid")
                    Dim dStyle As Style = IOSStyleHelper.GetSytle(TableNames.DT_EventGrid)

                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin Getting AdoNetTableInfo")
                    Dim objAdoNetTableInfo As New IOSTableInfoAdoNet(eventMapLayerName, dtChartAndGridData, dStyle, "x", "y")
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Getting AdoNetTableInfo")
                    frmMapWindow.objMapHelper.ProgressBar = frmMapWindow.ToolStripProgressBar1
                    Application.DoEvents()
                    frmMapWindow.objMapHelper.CreateMapLayerUsingAdoNetTableInfo(objAdoNetTableInfo, 0, 100)
                    If (frmMapWindow.ApplyHavingSettings(objAdoNetTableInfo.OutLayer)) Then
                        Try
                            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateThematics UE 2G Message ")
                            frmMapWindow.DT_CreateThematics(objAdoNetTableInfo.OutLayer, "UE2G", "UE 2G Message", "RxLevSub")
                            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After CreateThematics UE 2G Message ")
                        Catch ex As Exception
                            frmMapWindow.SetStatus(ex.Message)
                        End Try
                        Try
                            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateThematics UE 3G Message ")
                            frmMapWindow.DT_CreateThematics(objAdoNetTableInfo.OutLayer, "UE3G", "UE 3G Message", "RSCP")
                            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After CreateThematics UE 3G Message ")
                        Catch ex As Exception
                            frmMapWindow.SetStatus(ex.Message)
                        End Try

                        Try
                            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateThematics UE 4G Message ")
                            frmMapWindow.DT_CreateThematics(objAdoNetTableInfo.OutLayer, "UE4G", "UE 4G Message", "RSRP")
                            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After CreateThematics UE 4G Message ")
                        Catch ex As Exception
                            frmMapWindow.SetStatus(ex.Message)
                        End Try
                    End If
                    _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Created Dt_CreateThematics 2G,3G,4G")
                    If (gvDriveTestModule.SelectedRowsCount > 0) Then
                        Dim rowIndex() As Integer
                        rowIndex = gvDriveTestModule.GetSelectedRows()
                        If rowIndex.Length > 0 Then
                            Dim data As DataRowView = gvDriveTestModule.GetRow(rowIndex(0))
                            frmMapWindow.SetMappedMessageType(data.DataView.Item(0)(4).ToString)
                        End If
                    End If
                    isEventMapped = True
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus("There is an error. Not able to draw data.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit btnMapEvent")
            Me.Cursor = Cursors.Default
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvEventsDataGrid.RowCellClick
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start RowCellClick")
        Dim id As Integer = gvEventsDataGrid.GetDataRow(gvEventsDataGrid.FocusedRowHandle)(0)
        MapEventDataRowOnMapForm(id)
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "End RowCellClick")
    End Sub

#End Region

#Region "Public Methods"

    Public Sub SetConnectionString(ByVal connstr As String)
        conn_IOS = connstr
    End Sub

    Public Sub FillDataGrid(ByVal EventID As String, ByVal dtid As String, Optional ByVal IsWithMapLayre As Boolean = False)
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start FillDataGrid")
        Dim sqlEvents As String = Nothing
        Dim str2G As String = "msgL3 is Not NULL or msgL3 <> '' or SC_CellID_2G is not NULL or RxLevSub is not NULL or RxQualSub is not NULL or N1_BCC is not NULL or N1_BCCH is not NULL or N1_NCC is not NULL or N1_RxLev is not NULL or N2_BCC is not NULL or N2_BCCH is not NULL or N2_NCC is not NULL or N2_RxLev is not NULL or N3_BCC is not NULL or N3_BCCH is not NULL or N3_NCC is not NULL or N3_RxLev is not NULL or N4_BCC is not NULL or N4_BCCH is not NULL or N4_NCC is not NULL or N4_RxLev is not NULL or N5_RxLev is not NULL or N5_BCC is not NULL or N5_BCCH is not NULL or N5_NCC is not NULL or N6_BCC is not NULL or N6_BCCH is not NULL or N6_NCC is not NULL or N6_RxLev is not NULL"
        Dim str3G As String = "MsgRRC_3G is Not NULL or MsgRRC_3G <> '' or AS1_SC is Not NULL or AS1_RSCP is Not NULL or AS1_Ecno is Not NULL or AS2_SC is Not NULL or AS2_RSCP is Not NULL or AS2_Ecno is Not NULL or AS3_SC is Not NULL or AS3_RSCP is Not NULL or AS3_Ecno is Not NULL or UARFCN is Not NULL or AS1_CellId is Not NULL or AS2_CellId is Not NULL or AS3_CellId is Not NULL or NB1_LAC is Not NULL or NB2_LAC is Not NULL or NB3_LAC is Not NULL or NB4_LAC is Not NULL or NB5_LAC is Not NULL or NB6_LAC is Not NULL or NB1_SC is Not NULL or  NB2_SC is Not NULL or NB3_SC is Not NULL or NB4_SC is Not NULL or NB5_SC is Not NULL or NB6_Sc is Not NULL or NB1_CellId is Not NULL or NB2_CellId is Not NULL or NB3_CellId is Not NULL or NB4_CellId is Not NULL or NB5_CellId is Not NULL or NB6_CellId is Not NULL or NB1_RSCP is Not NULL or NB2_RSCP is Not NULL or NB3_RSCP is Not NULL or NB4_RSCP is Not NULL or NB5_RSCP is Not NULL or NB6_RSCP is Not NULL or NB1_Ecno is Not NULL or NB2_Ecno is Not NULL or NB3_Ecno is Not NULL or NB4_Ecno is Not NULL or NB5_Ecno is Not NULL or NB6_Ecno is Not NULL or ChannelType_3G <> '' "
        Dim str4G As String = "MsgRRC_4G is Not NULL or MsgRRC_4G <> '' or SC_PCI is Not NULL or SC_RSRP is Not NULL or SC_RSRQ is Not NULL or SC_EARFCN is Not NULL or SC_CellId_4G is Not NULL or N1_PCI is Not NULL or  N2_PCI is Not NULL or N3_PCI is Not NULL or N4_PCI is Not NULL or N5_PCI is Not NULL or N6_PCI is Not NULL or N1_CellId is Not NULL or N2_CellId is Not NULL or N3_CellId is Not NULL or N4_CellId is Not NULL or N5_CellId is Not NULL or N6_CellId is Not NULL or N1_TAC is Not NULL or N2_TAC is Not NULL or N3_TAC is Not NULL or N4_TAC is Not NULL or N5_TAC is Not NULL or  N6_TAC is Not NULL or N1_RSRP is Not NULL or N2_RSRP is Not NULL or N3_RSRP is Not NULL or N4_RSRP is Not NULL or N5_RSRP is Not NULL or N6_RSRP is Not NULL or N1_RSRQ is Not NULL or N2_RSRQ is Not NULL or N3_RSRQ is Not NULL or N4_RSRQ is Not NULL or N5_RSRQ is Not NULL or N6_RSRQ is Not NULL or D1_PCI is Not NULL or  D2_PCI is Not NULL or D3_PCI is Not NULL or D4_PCI is Not NULL or D5_PCI is Not NULL or D6_PCI is Not NULL or D1_CellId is Not NULL or D2_CellId is Not NULL or D3_CellId is Not NULL or D4_CellId is Not NULL or D5_CellId is Not NULL or D6_CellId is Not NULL or D1_TAC is Not NULL or D2_TAC is Not NULL or D3_TAC is Not NULL or D4_TAC is Not NULL or D5_TAC is Not NULL or D6_TAC is Not NULL or D1_RSRP is Not NULL or D2_RSRP is Not NULL or D3_RSRP is Not NULL or D4_RSRP is Not NULL or D5_RSRP is Not NULL or D6_RSRP is Not NULL or D1_RSRQ is Not NULL or D2_RSRQ is Not NULL or D3_RSRQ is Not NULL or D4_RSRQ is Not NULL or D5_RSRQ is Not NULL or D6_RSRQ is Not NULL or ChannelType_4G <> '' "

        Try
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin Getting dtChartAndGridData data from IOSEventHelper.GetEventData")
            dtChartAndGridData = IOSEventHelper.GetEventData(dtChartAndGridData, EventID, dtid, conn_IOS)
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Getting dtChartAndGridData data from IOSEventHelper.GetEventData")
            Dim tempData As New DataTable()
            If Not dtChartAndGridData Is Nothing Then
                gcEventsDataGrid.Refresh()
                If (IsWithMapLayre) Then
                    frmMapWindow.objMapHelper.CloseTable("DT_EventGrid")
                    frmMapWindow.objMapHelper.RemoveLayer("DT_EventGrid")
                End If
                Dim filterString As String = String.Empty
                If rdbtn2G.Checked Then
                    If (String.IsNullOrEmpty(filterString)) Then
                        filterString = str2G
                    End If
                End If
                If (rdbtn3G.Checked) Then
                    If (String.IsNullOrEmpty(filterString)) Then
                        filterString = str3G
                    Else
                        filterString += " OR " + str3G
                    End If
                End If
                If (rdbtn4G.Checked) Then
                    If (String.IsNullOrEmpty(filterString)) Then
                        filterString += str4G
                    Else
                        filterString += " OR " + str4G
                    End If
                End If
                If (String.IsNullOrEmpty(filterString)) Then
                    tempData = dtChartAndGridData.Clone()
                Else
                    tempData = New DataView(dtChartAndGridData, filterString, "", DataViewRowState.CurrentRows).ToTable()
                End If
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Create TempData from dtChartAndGridData")
                Dim temp As DataTable = FormatTableDateTimeString(tempData, 3, "dd/MM/yyyy hh:mm:ss:fff")
                gcEventsDataGrid.DataSource = Nothing
                gvEventsDataGrid.Columns.Clear()
                gcEventsDataGrid.DataSource = temp
                gcEventsDataGrid.SuspendLayout()
                gcEventsDataGrid.Refresh()
                HideShowGridColumn()
                gvEventsDataGrid.OptionsClipboard.AllowCopy = True
                gvEventsDataGrid.OptionsView.ColumnAutoWidth = False

                For Each item As DevExpress.XtraGrid.Columns.GridColumn In gvEventsDataGrid.Columns
                    item.AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    item.AppearanceCell.GetTextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                    item.BestFit()
                Next
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After fill Data in Grid and FIT_ALL")
                gcEventsDataGrid.Refresh()
                gcEventsDataGrid.ResumeLayout()
            Else
                gvEventsDataGrid.Columns.Clear()
            End If

        Catch ex As Exception
            gcEventsDataGrid.ResumeLayout()
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit FillDataGrid")
        End Try
    End Sub

    Public Sub GetSelectedFailureEvents(ByVal EventID As Integer, ByVal dtid As Integer)
        Try
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start GetSelectedFailureEvents")
            If gvDriveTestModule.RowCount > 0 Then
                If (EventID = 0) Then
                    Me.EventID = dtFailure.Rows(0)(0)
                Else
                    Me.EventID = EventID
                End If
                If (dtid = 0) Then
                    Me.DtId = dtFailure.Rows(0)(1)
                Else
                    Me.DtId = dtid
                End If
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillData")
                FillData(Me.EventID, Me.DtId)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin BindMessagaList")
                BindMessagaList(EventID, dtid)
                IsNeedToFillMessageList = False
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateChart")
                CreateChart(Me.EventID, Me.DtId)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                FillDataGrid(Me.EventID, Me.DtId, True)

                Dim ii As Integer
                For ii = 0 To gvDriveTestModule.RowCount - 1
                    Dim data As DataRowView = gvDriveTestModule.GetRow(ii)
                    If data.DataView.Item(ii)("EventID").ToString = Me.EventID And data.DataView.Item(ii)(1).ToString = Me.DtId Then
                        gvDriveTestModule.SelectRow(ii)
                    Else
                        gvDriveTestModule.UnselectRow(ii)
                    End If
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit GetSelectedFailureEvents")
        End Try
    End Sub

    Public Sub GetFailureEvents()
        Try
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start FailureEvent")
            If (dtFailure Is Nothing) Then
                Exit Sub
            End If
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Set Dtfalure Seleted Columns")
            dtFailure = GetSelectedColumns(dtFailure)

            Dim temp As DataTable = FormatTableDateTimeString(dtFailure, 3, "dd/MM/yyyy hh:mm:ss")
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Set dgvDriveTest DataSourse")
            gvDriveTestModule.OptionsBehavior.AutoPopulateColumns = True
            gvDriveTestModule.Columns.Clear()
            gcDriveTestModule.DataSource = Nothing
            gcDriveTestModule.DataSource = temp

            If dtFailure.Rows.Count <= 0 Then
                frmMapWindow.SetStatus("No Event Found!!!!")
                Me.Hide()
            Else
                gvDriveTestModule.BestFitColumns()
                gvDriveTestModule.OptionsView.RowAutoHeight = True

                If (statusForEvent) Then
                    If (Me.WindowState = FormWindowState.Minimized) Then
                        Me.WindowState = FormWindowState.Normal
                    End If
                    Me.Show()
                End If
                statusForEvent = False
                For ii As Integer = 0 To gvDriveTestModule.RowCount - 1
                    gvDriveTestModule.UnselectRow(ii)
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus(ex.Message)
        Finally
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit GetFalurEvent")
        End Try
    End Sub

    Public Sub SetFailurData(ByVal data As DataTable)
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start SetFailurData")
        dtFailure = data.Clone()
        For Each Item As DataRow In data.Rows
            dtFailure.ImportRow(Item)
        Next
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit SetFailurData")
    End Sub

    Private Function GetSelectedColumns(ByRef data As DataTable) As DataTable
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, " Start GetSelectedColumns Function")
        Dim index As Integer = 0    'ask
        While (data.Columns.Count > 7)
            Dim colName As String = data.Columns(index).ColumnName.ToLower()
            If (colName = "eventid" Or colName = "dtid" Or colName = "timestamp" Or colName = "failedontech" Or colName = "service" Or colName = "problemeventtype" Or colName = "logfile") Then
                If (colName = "failedontech") Then
                    data.Columns(index).ColumnName = "Technology"
                End If
                If (colName = "problemeventtype") Then
                    data.Columns(index).ColumnName = "Failure Type"
                End If
                index = index + 1
            Else
                data.Columns.RemoveAt(index)
            End If
        End While
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit GetSelectedColumns Function")
        Return data
    End Function

    Public Function FormatTableDateTimeString(ByRef tempData As DataTable, ByVal colIndex As Integer, ByVal datetimeFormat As String) As DataTable
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start FormatTableDateTimeString")
        Dim temp As DataTable = tempData.Clone()
        If (Not temp Is Nothing) Then
            Try
                temp.Columns(colIndex - 1).DataType = "".GetType()
            Catch ex As Exception

            End Try
        End If

        For Each dr As DataRow In tempData.Rows
            Dim r As DataRow = temp.NewRow()
            For index As Integer = 1 To dr.ItemArray().Count()
                If index = colIndex Then
                    'If (dr(index - 1) = CType(DBNull.Value, System.Object)) Then
                    '    r(index - 1) = DBNull.Value
                    'Else
                    r(index - 1) = Convert.ToDateTime(dr(index - 1)).ToString(datetimeFormat)
                    'End If
                Else
                    'If (dr(index - 1) = CType(DBNull.Value, System.Object)) Then
                    '    r(index - 1) = DBNull.Value
                    'Else
                    r(index - 1) = dr(index - 1)
                    'End If
                End If
            Next
            temp.Rows.Add(r)
        Next
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit FormatTableDateTimeString")
        Return temp
    End Function

#End Region

#Region "Context Menu"

    Private Sub cmsMessages_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles cmsMessages.ItemClicked
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start cmsMessages_ItemClicked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim path As String = GetUserDataPath()
            path = path & "\\MessageCofing.xml"
            If e.ClickedItem.Name = SelectAllToolStripMenuItem.Name Then

                For i As Integer = 0 To TryCast(vCblMessage.DataSource, DataTable).Rows.Count - 1
                    If vCblMessage.GetItemCheckState(i) = CheckState.Unchecked Then
                        vCblMessage.SetItemChecked(i, True)
                    End If
                Next
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateXaxisMarkerForMessage")
                CreateXaxisMarkerForMessage()
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Created X marker List")
            ElseIf e.ClickedItem.Name = MessageAll2GToolStripMenuItem.Name Then

                For i As Integer = 0 To TryCast(vCblMessage.DataSource, DataTable).Rows.Count - 1
                    Dim rowView As DataRowView = TryCast(vCblMessage.GetItem(i), DataRowView)
                    If rowView.Item(1).ToString = "2G" Then
                        vCblMessage.SetItemChecked(i, True)
                    Else
                        vCblMessage.SetItemChecked(i, False)
                    End If
                Next

                CreateXaxisMarkerForMessage()
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Created X marker List 2G")
            ElseIf e.ClickedItem.Name = MessageAll3GToolStripMenuItem.Name Then
                For i As Integer = 0 To TryCast(vCblMessage.DataSource, DataTable).Rows.Count - 1
                    Dim rowView As DataRowView = TryCast(vCblMessage.GetItem(i), DataRowView)
                    If rowView.Item(1).ToString = "3G" Then
                        vCblMessage.SetItemChecked(i, True)
                    Else
                        vCblMessage.SetItemChecked(i, False)
                    End If
                Next
                CreateXaxisMarkerForMessage()
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Created X marker List 3G")
            ElseIf e.ClickedItem.Name = SelectAll4GToolStripMenuItem.Name Then
                For i As Integer = 0 To TryCast(vCblMessage.DataSource, DataTable).Rows.Count - 1
                    Dim rowView As DataRowView = TryCast(vCblMessage.GetItem(i), DataRowView)
                    If rowView.Item(1).ToString = "4G" Then
                        vCblMessage.SetItemChecked(i, True)
                    Else
                        vCblMessage.SetItemChecked(i, False)
                    End If
                Next
                CreateXaxisMarkerForMessage()
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "After Created X marker List 4G")
            ElseIf e.ClickedItem.Name = SaveSettingToolStripMenuItem.Name Then
                Dim data As New DataSet("message")
                Dim dt As New DataTable()
                dt.Columns.Add("msg")
                data.Tables.Add(dt)
                If File.Exists(path) Then
                    data.ReadXml(path)
                End If
                If (data.Tables.Count > 0) Then
                    data.Tables(0).Rows.Clear()

                    For i As Integer = 0 To TryCast(vCblMessage.DataSource, DataTable).Rows.Count - 1
                        If vCblMessage.GetItemCheckState(i) = CheckState.Checked Then
                            Dim rowView As DataRowView = TryCast(vCblMessage.GetItem(i), DataRowView)
                            Dim dr As DataRow = data.Tables(0).NewRow()
                            dr("msg") = rowView.Item(0).ToString
                            data.Tables(0).Rows.Add(dr)
                        End If
                    Next

                    data.AcceptChanges()
                    data.WriteXml(path)
                End If
            ElseIf e.ClickedItem.Name = ClearSettingToolStripMenuItem.Name Then
                For i As Integer = 0 To TryCast(vCblMessage.DataSource, DataTable).Rows.Count - 1
                    If vCblMessage.GetItemCheckState(i) = CheckState.Checked Then
                        vCblMessage.SetItemChecked(i, False)
                    End If
                Next
                CreateXaxisMarkerForMessage()
                If File.Exists(path) Then
                    File.Delete(path)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit cmsMessages")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub tsmiDownLoadFile_Click(sender As Object, e As EventArgs) Handles tsmiDownLoadFile.Click
        Me.Cursor = Cursors.WaitCursor
        Dim saveFileDialog1 As New SaveFileDialog
        saveFileDialog1.Title = "Save Drive Test Files"
        'saveFileDialog1.CheckFileExists = True
        saveFileDialog1.CheckPathExists = True
        saveFileDialog1.DefaultExt = "nmf"
        saveFileDialog1.Filter = "Drive Test (*.nmf)|*.nmf|All files (*.*)|*.*"
        Dim saveFileName As String
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            If gvDriveTestModule.RowCount > 0 Then
                Dim row As Integer = gvDriveTestModule.FocusedRowHandle
                Dim dtID As String = gvDriveTestModule.GetRowCellValue(row, gvDriveTestModule.Columns(1)).ToString()
                Dim fileName As String = gvDriveTestModule.GetRowCellValue(row, gvDriveTestModule.Columns(6)).ToString()
                If (fileName IsNot Nothing AndAlso Not String.IsNullOrEmpty(fileName)) Then
                    Dim driveData As DataTable = GetDrivetestData(dtID)
                    If driveData.Rows.Count > 0 Then
                        Dim drivetestName As String = driveData.Rows(0)("DriveTestName")
                        Dim campaignName As String = driveData.Rows(0)("CampaignName")
                        Dim DriveTestFilesPath As String = GetConfigClientKeyValue("DriveTestFilesPath")
                        Dim destinationFilePath As String = DriveTestFilesPath & campaignName & "\" & drivetestName & "\" & fileName
                        If (IsFilePathExist(destinationFilePath)) Then
                            If (saveFileDialog1.ShowDialog() = DialogResult.OK) Then
                                saveFileName = saveFileDialog1.FileName
                                If (SaveFileData(saveFileName, destinationFilePath)) Then
                                    frmMapWindow.SetStatus("File saved.")
                                End If
                            End If
                        Else
                            frmMapWindow.SetStatus("File Not found.")
                        End If
                    Else
                        frmMapWindow.SetStatus("Folder hierarchy not found.")
                    End If
                Else
                    frmMapWindow.SetStatus("File Name doesn't Exist.")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus("There is an error. Not able to draw complete data.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit dgvDriveTestModule_CelMouseClick")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Function IsFilePathExist(ByVal filePath As String) As Boolean
        If (File.Exists(filePath)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetDrivetestData(ByVal dtid As String) As DataTable
        ''Dim sql As String = "select DtID, DriveTestName, CampaignName from dbo.DT_List where Dtid =" & dtid & ""
        Return IOS.DataLibrary.clsSQLCommands.GetDriveTestData(conn_IOS, dtid)
    End Function

    Private Function SaveFileData(ByVal destinationFile As String, ByVal sourceFile As String) As Boolean
        Try
            File.Copy(sourceFile, destinationFile)
            Return True
        Catch
            Return False
        Finally
        End Try
        Return True
    End Function

#End Region

    Private Sub gvDriveTestModule_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvDriveTestModule.RowCellClick
        _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Start gvDriveTestModule_RowCellClick")
        Me.Cursor = Cursors.WaitCursor
        ' Application.DoEvents()
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            isEventMapped = False
            If gvDriveTestModule.RowCount > 0 Then
                Dim row As Integer = e.RowHandle 'dgvDriveTestModule.CurrentCell.RowIndex
                Me.EventID = gvDriveTestModule.GetRowCellValue(row, gvDriveTestModule.Columns(0)) 'dgvDriveTestModule.Rows(row).Cells(0).Value
                Me.DtId = gvDriveTestModule.GetRowCellValue(row, gvDriveTestModule.Columns(1)) 'dgvDriveTestModule.Rows(row).Cells(1).Value
                If Not dtChartAndGridData Is Nothing And dtChartAndGridData.Rows.Count > 0 Then
                    dtChartAndGridData.Rows.Clear()
                End If
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillData")
                FillData(Me.EventID, Me.DtId)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin BindMessagaList")
                BindMessagaList(Me.EventID, Me.DtId)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin CreateChart")
                CreateChart(Me.EventID, Me.DtId)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin FillDataGrid")
                FillDataGrid(Me.EventID, Me.DtId, True)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin tlb_ParcelSelector_Click_Events")
                frmMapWindow.tlb_ParcelSelector_Click_Events(Nothing, Nothing)
                _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Begin DtCreateLineFeatures_Events")
                frmMapWindow.DT_CreateLineFeatures_Events(Me.EventID, Me.DtId)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            frmMapWindow.SetStatus("There is an error. Not able to draw complete data.")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            _logger.SetLogInfo(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exit dgvDriveTestModule_CelMouseClick")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

End Class