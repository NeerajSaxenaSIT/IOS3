Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports dotnetCHARTING.WinForms
Imports IOS.Library
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors

Public Class frmAnomaly

#Region "Variables"

    Private dtAlerts As New DataTable
    Private dtCorrelationMethods As New DataTable
    Private dtKPIRules As New DataTable
    Private dtKPICorrelation As New DataTable
    Private dtChartData As New DataTable
    Private dtNote As New DataTable
    Private dtKPICorrelationChartData As New DataTable
    Private dtSuppressedAlert As New DataTable
    Private dtAlertScore As New DataTable

    Private selectedAlertRule As DataRow = Nothing
    Private selectedKPIRule As DataRow = Nothing
    Private selectedAlertScore As DataRow = Nothing

    Private ExtraLegendEntryCollection As New Dictionary(Of String, LegendEntry)
    Private DefaultSeriesCollection As New Dictionary(Of String, Series)

    Private objTechnology As frmTechnology = Nothing

#End Region

#Region "Form Event"

    Private Sub frmAnomaly_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try

            ConfigurAnomalyForm(Me.Name)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frmAnomaly_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            WaitScreen.ShowWaitScreen("Loading...")

            sccTop.SplitterPosition = Math.Abs(Me.Width / 2)
            sccMain.SplitterPosition = Math.Abs(Me.Height / 2)


            'Get KPI Correlation Methods And Bind in ComboBox
            BindKPICorrTypeCombo()

            'Get All Alert Rule & Fill in Grid
            FillAlertGrid()

            'Get All Suppressed Alert Rule & Fill in Grid
            FillSuppressGrid()

            'Get All Alert Score Grid
            FillAlertScoreGrid()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub BindKPICorrTypeCombo()
        Dim strConnection As String, sqlParam As String
        Dim parray()() As String = Nothing

        strConnection = GetSQL(3813, parray)(0)
        sqlParam = GetSQL(3813, parray)(1)

        dtCorrelationMethods = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbCorrType, dtCorrelationMethods, "KPICorrTypeID", "KPICorrelationMethod", , False)
        cmbCorrType.SelectedItem = GetComboItemFromValue(1, cmbCorrType)
    End Sub

    Public Sub ConfigurAnomalyForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                 tsmiModifyAlert, tsmiSuppressAlertAllObjects, tsmiSuppressAlertSelectedObject, tsmiSendObjectToPM, tsmiAddNote, tsmiDeleteSuppression, tsmiExtentSuppression
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

#End Region

#Region "Grid Event"

    Private Sub grdSuppressed_KeyUp(sender As Object, e As KeyEventArgs) Handles grdSuppressed.KeyUp
        If e.KeyCode = Keys.Delete Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                DeleteSuppression()
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                Me.Cursor = Cursors.Default
                Application.DoEvents()
            End Try
        End If
    End Sub

    Private Sub gvSuppressed_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gvSuppressed.RowCellStyle
        Try
            If e.RowHandle > -1 AndAlso e.Column.FieldName = "IsExpired" Then
                If e.CellValue = 1 Then
                    e.Appearance.BackColor = Color.Orange
                Else
                    e.Appearance.BackColor = Color.LightGreen
                End If
                e.Appearance.BackColor2 = Color.White
                e.Appearance.ForeColor = Color.Black
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvAlerts_RowClick(sender As Object, e As RowClickEventArgs) Handles gvAlerts.RowClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            selectedAlertRule = gvAlerts.GetDataRow(e.RowHandle)
            FillAlertNoteGrid()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvAlerts_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) 'Handles gvAlerts.FocusedRowChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            selectedAlertRule = gvAlerts.GetFocusedDataRow()
            If selectedAlertRule IsNot Nothing Then
                Dim strConnection As String, sqlParam As String
                Dim parray()() As String = {
                    New String() {"@AlertID", "'" & selectedAlertRule.Item(2) & "'"},
                    New String() {"@Alertdate", "'" & selectedAlertRule.Item(1) & "'"},
                    New String() {"@Object", "'" & selectedAlertRule.Item(4) & "'"}
                }

                strConnection = GetSQL(3804, parray)(0)
                sqlParam = GetSQL(3804, parray)(1)

                dtKPIRules = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOSDevExpressGrid.PopulateDataInGrid(grdRulesDetected, grdRulesDetected.MainView, dtKPIRules, "ALL")
                IOSDevExpressGrid.ClearGrid(grdPositive)
                IOSDevExpressGrid.ClearGrid(grdNegative)
                gvRulesDetected.OptionsSelection.MultiSelect = True
                btnClearChart_Click(Nothing, Nothing)
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

    Private Sub gvAlerts_MouseMove(sender As Object, e As MouseEventArgs) Handles gvAlerts.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                selectedAlertRule = gvAlerts.GetFocusedDataRow()
                If selectedAlertRule IsNot Nothing Then
                    Dim obj() As Object = {"AlertDrag", selectedAlertRule}
                    grdAlerts.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvRulesDetected_MouseMove(sender As Object, e As MouseEventArgs) Handles gvRulesDetected.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim dr As DataRow = gvRulesDetected.GetFocusedDataRow()
                If dr IsNot Nothing Then
                    Dim obj() As Object = {"KPIRuleDrag", dr}
                    grdRulesDetected.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub grdCorr_DragDrop(sender As Object, e As DragEventArgs) Handles grdPositive.DragDrop, grdNegative.DragDrop
        Try
            WaitScreen.ShowWaitScreen("This can take a few minutes...")

            Dim mm() As Object = e.Data.GetData("System.Object[]")
            If mm IsNot Nothing Then
                Dim dr As DataRow = CType(mm(1), DataRow)
                If mm(0) = "KPIRuleDrag" Then
                    GetKPICorrelationData(dr)
                    FillKPICorrelationGrid()
                End If
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
        End Try
    End Sub

    Private Sub ctrl_DragOver(sender As Object, e As DragEventArgs) Handles grdNegative.DragOver, grdPositive.DragOver, chAnomaly.DragOver
        If e.Data.GetDataPresent("System.Object[]") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub gvPositive_MouseMove(sender As Object, e As MouseEventArgs) Handles gvPositive.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim dr As DataRow = gvPositive.GetFocusedDataRow()
                If dr IsNot Nothing Then
                    Dim obj() As Object = {"PositiveCorrDrag", dr}
                    grdPositive.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvNegative_MouseMove(sender As Object, e As MouseEventArgs) Handles gvNegative.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim dr As DataRow = gvNegative.GetFocusedDataRow()
                If dr IsNot Nothing Then
                    Dim obj() As Object = {"NegativeCorrDrag", dr}
                    grdNegative.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvScore_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        Try
            Dim dtScoreChart As DataTable
            Dim k As Integer = 0

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            chAnomaly.DefaultElement.Marker.Visible = False
            chAnomaly.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
            chAnomaly.LegendBox.DefaultEntry.Value = ""
            chAnomaly.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
            chAnomaly.LegendBox.Visible = True

            chAnomaly.XAxis.TickLabelMode = TickLabelMode.Angled
            chAnomaly.XAxis.TickLabelAngle = 45
            chAnomaly.XAxis.Minimum = 0
            chAnomaly.XAxis.Maximum = 0

            chAnomaly.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
            chAnomaly.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

            chAnomaly.ToolTip.InitialDelay = 1
            chAnomaly.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
            chAnomaly.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
            chAnomaly.CleanupPeriod = 1

            chAnomaly.XAxis.TimeScaleLabels.RangeIntervals.Clear()
            chAnomaly.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
            chAnomaly.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
            chAnomaly.XAxis.TimeInterval = TimeInterval.Days
            chAnomaly.XAxis.FormatString = "dd/MM/yy"
            chAnomaly.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
            chAnomaly.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
            chAnomaly.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
            chAnomaly.LegendBox.Orientation = Orientation.Bottom
            chAnomaly.LegendBox.ExtraEntries.Clear()

            selectedAlertScore = gvScore.GetFocusedDataRow()
            If selectedAlertScore IsNot Nothing Then
                Dim strConnection As String, sqlParam As String
                Dim parray()() As String = {
                    New String() {"@objectname", "'" & selectedAlertScore.Item(1) & "'"}
                }

                strConnection = GetSQL(3847, parray)(0)
                sqlParam = GetSQL(3847, parray)(1)
                dtScoreChart = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

                Dim dtDistinctAlerts As DataTable = dtScoreChart.DefaultView.ToTable(True, "AlertName")
                Dim dtCopy As New DataTable
                dtCopy.Columns.Add("AlertDate", GetType(System.DateTime))
                For Each dr As DataRow In dtDistinctAlerts.Rows
                    dtCopy.Columns.Add(dr("AlertName"), GetType(String))
                Next

                For Each dr As DataRow In dtScoreChart.Rows
                    Dim alertCol As String = dr("AlertName").ToString
                    Dim alertVal As String = dr("DashboardScoreValue").ToString

                    Dim drow As DataRow = dtCopy.NewRow()
                    drow("AlertDate") = dr("AlertDate")
                    drow(alertCol) = alertVal
                    dtCopy.Rows.Add(drow)
                Next

                If dtCopy IsNot Nothing Then

                    chAnomaly.TitleBox.Label.Text = "Objects: " & selectedAlertScore.Item(1).ToString.Trim
                    chAnomaly.TitleBox.HeaderLabel.Text = "Anomaly - Alert Score"
                    chAnomaly.TitleBox.Label.Alignment = StringAlignment.Near
                    chAnomaly.TitleBox.Label.LineAlignment = StringAlignment.Near
                    chAnomaly.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "
                    chAnomaly.Annotations.Clear()
                    chAnomaly.Annotations.Add(New Annotation(""))
                    chAnomaly.YAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                    chAnomaly.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
                    chAnomaly.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart
                    chAnomaly.XAxis.StaticColumnWidth = 10
                    chAnomaly.XAxis.SpacingPercentage = 40
                    chAnomaly.Dock = DockStyle.Fill

                    Dim chart_elements() As String = Nothing
                    k = 0
                    For Each dcol As DataColumn In dtCopy.Columns
                        If dcol.ColumnName.ToUpper.Trim <> "ALERTDATE" Then
                            ReDim Preserve chart_elements(k)
                            chart_elements(k) = dcol.ColumnName
                            k = k + 1
                        End If
                    Next

                    Dim de As DataEngine = New DataEngine(dtCopy)
                    de.DataFields = String2DataFields(chart_elements, "AlertDate")
                    de.DataGridFormatString = "N2"
                    de.FormatString = "dd/MM/yy"

                    Dim sc As New SeriesCollection
                    sc = de.GetSeries()

                    Dim i As Integer = 0
                    For i = 0 To sc.Count() - 1
                        sc(i).Type = SeriesType.Bar
                        sc(i).Line.Width = 3
                        sc(i).EmptyElement.Mode = EmptyElementMode.TreatAsZero

                        sc(i).DefaultElement.Color = Color.FromArgb(255, 255 / sc.Count * i, 200 / (i + 1), 49 + i)
                    Next

                    chAnomaly.SeriesCollection.Clear()
                    chAnomaly.SeriesCollection.Add(sc)

                    dtCopy.Dispose()
                    dtCopy = Nothing

                End If
            End If

            chAnomaly.RefreshChart()
            chAnomaly.ResumeLayout()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Context Menu"

    Private Sub tsmiModifyAlert_Click(sender As Object, e As EventArgs) Handles tsmiModifyAlert.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim alertName As String = selectedAlertRule.Item(3)
            frmAlertManager.Show()
            frmAlertManager.lstviewAlerts.SetFocusedNode(frmAlertManager.lstviewAlerts.FindNodeByFieldValue("AlertName", alertName))
            frmAlertManager.txtObjectNameFilter.Text = selectedAlertRule.Item(4).ToString
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiSuppressAlertAllObjects_Click(sender As Object, e As EventArgs) Handles tsmiSuppressAlertAllObjects.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim objCalendar As New dlgAnomalyCalendar()
            objCalendar.StartPosition = FormStartPosition.CenterScreen
            objCalendar.Text = "Suppress All Objects"
            If objCalendar.ShowDialog() = DialogResult.OK Then
                If selectedAlertRule IsNot Nothing Then
                    SuppressAlert(selectedAlertRule.Item(2), "%", objCalendar.dtNavigator.SelectedRanges.Item(0).StartDate)
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

    Private Sub tsmiSuppressAlertSelectedObject_Click(sender As Object, e As EventArgs) Handles tsmiSuppressAlertSelectedObject.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim objCalendar As New dlgAnomalyCalendar()
            objCalendar.StartPosition = FormStartPosition.CenterScreen
            objCalendar.Text = "Suppress Selected Object"
            If objCalendar.ShowDialog() = DialogResult.OK Then
                If selectedAlertRule IsNot Nothing Then
                    SuppressAlert(selectedAlertRule.Item(2), selectedAlertRule.Item(4), objCalendar.dtNavigator.SelectedRanges.Item(0).StartDate)
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

    Private Sub tsmiAddNote_Click(sender As Object, e As EventArgs) Handles tsmiAddNote.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim objAlertNote As New dlgAlertNote()
            If objAlertNote.ShowDialog() = DialogResult.OK Then
                Dim strConnection As String, sqlParam As String
                Dim parray()() As String = {
                    New String() {"@selectedAlertTriggerid", "" & selectedAlertRule.Item(0) & ""},
                    New String() {"@username", "'" & Environment.UserName & "'"},
                    New String() {"@NoteDescription", "'" & objAlertNote.txtAlertNote.Text & "'"}
                }

                strConnection = GetSQL(3820, parray)(0)
                sqlParam = GetSQL(3820, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                FillAlertNoteGrid()

                Dim alertFilter As String = gvAlerts.FilterPanelText
                If alertFilter <> "" Then
                    Dim dtFiltered As DataTable = dtAlerts.Select(alertFilter).CopyToDataTable()
                    For Each dr As DataRow In dtFiltered.Rows
                        If dr("AlertTriggerID") = selectedAlertRule.Item(0) Then
                            dr("Note") = True
                            dtFiltered.AcceptChanges()
                        End If
                    Next
                    grdAlerts.DataSource = dtFiltered
                    UpdateAlertGridNoteColumn()
                Else
                    FillAlertGrid()
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

    Private Sub tsmiSendObjectToPM_Click(sender As Object, e As EventArgs) Handles tsmiSendObjectToPM.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim rowIndex() As Integer = gvAlerts.GetSelectedRows()
            Dim alertIds As String = "("
            For i As Integer = 0 To rowIndex.Length - 1
                alertIds = alertIds & gvAlerts.GetRowCellValue(rowIndex(i), "AlertID") & ","
            Next
            alertIds = alertIds.TrimEnd(",") & ")"
            Dim dtData As DataTable = IOS.DataLibrary.clsSQLCommands.GetAlertObjectMappingToPM(connStrIOSServer, alertIds)
            Dim selectedObjectName As String = gvAlerts.GetRowCellValue(rowIndex(0), "ObjectName")

            If dtData.Rows.Count > 0 Then
                'For Each dr As DataRow In dtData.DefaultView.ToTable(True, {"Tech"}).Rows
                Dim dr() As DataRow = dtData.Select("OBJECTNAME='" & selectedObjectName & "'")

                If Not objFrmTechList.Exists(Function(x) x.Network.ToUpper.Equals(dr(0)("Tech").ToString)) Then
                    frmMDI.OpenTechFormDynamically(dr(0)("Tech").ToString, objTechnology, True)
                Else
                    objTechnology = objFrmTechList.Where(Function(x) x.Network.Equals(dr(0)("Tech").ToString)).LastOrDefault()
                End If

                'Dim objRows = dtData.DefaultView.ToTable(True, {"OBJECTNAME"}).Rows
                frmMapWindow.SelectionToTreeStep1(dr(0)("Tech").ToString(), dr.Count, False, New IOS.Library.SelectionToTreeFlags())
                For Each item As DataRow In dr
                    frmMapWindow.SelectionToTreeStep2(dr(0)("Tech").ToString(), selectedObjectName, False)     'item("OBJECTNAME").ToString()
                Next
                'Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmiDeleteSuppression_Click(sender As Object, e As EventArgs) Handles tsmiDeleteSuppression.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            DeleteSuppression()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmiExtentSuppression_Click(sender As Object, e As EventArgs) Handles tsmiExtentSuppression.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim objCalendar As New dlgAnomalyCalendar()
            If objCalendar.ShowDialog() = DialogResult.OK Then
                Dim strConnection As String, sqlParam As String
                Dim rowIndex() As Integer
                rowIndex = gvSuppressed.GetSelectedRows()
                For i As Integer = 0 To rowIndex.Length - 1
                    Dim dr As DataRowView = gvSuppressed.GetRow(rowIndex(i))
                    If dr IsNot Nothing Then
                        Dim parray()() As String = {
                            New String() {"@selecteddate", "'" & objCalendar.dtNavigator.SelectedRanges.Item(0).StartDate & "'"},
                            New String() {"@selectedAlertRuleID", "" & dr.Item(0) & ""},
                            New String() {"@selectedObjectName", "'" & dr.Item(2) & "'"}
                        }

                        strConnection = GetSQL(3827, parray)(0)
                        sqlParam = GetSQL(3827, parray)(1)
                        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                        FillSuppressGrid()
                    End If
                Next
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_Copy_All_Click(sender As Object, e As EventArgs) Handles tsmi_Copy_All.Click, tsmi_Alerts_Copy_All.Click, tsmi_Suppressed_Copy_All.Click
        Try
            Dim requestMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim conTemp As ContextMenuStrip = TryCast(requestMenu.GetCurrentParent(), ContextMenuStrip)
            Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView, True, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_Copy_Selection_Click(sender As Object, e As EventArgs) Handles tsmi_Copy_SelectionWOHeader.Click, tsmi_Alerts_Copy_SelectionWOHeader.Click, tsmi_Suppressed_Copy_SelectionWOHeader.Click
        Try
            Dim requestMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim conTemp As ContextMenuStrip = TryCast(requestMenu.GetCurrentParent(), ContextMenuStrip)
            Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView, False, False)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_Alerts_Copy_SelectionWithHeader_Click(sender As Object, e As EventArgs) Handles tsmi_Alerts_Copy_SelectionWithHeader.Click, tsmi_Copy_SelectionWithHeader.Click, tsmi_Suppressed_Copy_SelectionWithHeader.Click
        Try
            Dim requestMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim conTemp As ContextMenuStrip = TryCast(requestMenu.GetCurrentParent(), ContextMenuStrip)
            Dim grvTemp As DevExpress.XtraGrid.GridControl = Nothing
            grvTemp = TryCast(conTemp.SourceControl, DevExpress.XtraGrid.GridControl)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = grvTemp.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(grvTemp, gridView, False, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub cm_CopyGridData_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cm_CopyGridData.Opening
        Try
            Dim conTemp As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            Dim grvTemp As GridControl = TryCast(conTemp.SourceControl, GridControl)
            Dim grdView As GridView = grvTemp.MainView
            tsmi_RecordCount.Text = "Record Count: " & grdView.RowCount.ToString()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub cms_ScoreGrid_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cms_ScoreGrid.Opening
        Try
            Dim cmsScore As ContextMenuStrip = TryCast(sender, ContextMenuStrip)
            Dim grvTemp As GridControl = TryCast(cmsScore.SourceControl, GridControl)
            Dim grdView As GridView = grvTemp.MainView
            tsmi_ScoreRecordCount.Text = "Record Count: " & grdView.RowCount.ToString()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_Send2Alerts_Click(sender As Object, e As EventArgs) Handles tsmi_Send2Alerts.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            selectedAlertScore = gvScore.GetFocusedDataRow()
            If selectedAlertScore IsNot Nothing Then

                'Clear any existing filters applied yet
                If gvAlerts.ActiveFilterString <> "" Then
                    gvAlerts.ActiveFilterString = ""
                    gvAlerts.ClearColumnsFilter()
                    IOSDevExpressGrid.PopulateDataInGrid(grdAlerts, grdAlerts.MainView, dtAlerts, "ALL")
                End If

                'Apply object name filter now
                Dim objectName As String = selectedAlertScore.Item("ObjectName").ToString
                Dim dt As DataTable = dtAlerts.Select("ObjectName='" & objectName & "'").CopyToDataTable
                grdAlerts.DataSource = dt
                xtcAlerts.SelectedTabPageIndex = 0
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_Send2PM_Click(sender As Object, e As EventArgs) Handles tsmi_Send2PM.Click
        Try
            selectedAlertScore = gvScore.GetFocusedDataRow()
            If selectedAlertScore IsNot Nothing Then
                Dim tech As String = selectedAlertScore("IOS_TECH").ToString
                Dim reportedObjectName As String = selectedAlertScore("ReportedObjectType").ToString
                frmMapWindow.SelectionToTreeStep2(tech, reportedObjectName, False)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Chart Event & Method"

    Private Sub chAnomaly_Click(sender As Object, ByVal e As MouseEventArgs) Handles chAnomaly.MouseClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfoDefault
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureUIDefault

            If e.Button = MouseButtons.Left Then
                Dim myChart As Chart = CType(sender, Chart)
                Dim hit As HitTestInfo = myChart.HitTest(e.X, e.Y)
                Dim ChartLegendRect As System.Drawing.Rectangle = myChart.LegendBox.GetRectangle()
                If ChartLegendRect.Contains(e.X, e.Y) Then
                    If TypeOf hit.Object Is LegendEntry Then
                        Dim chartLegendEntry As LegendEntry = CType(hit.Object, LegendEntry)
                        ShowHideChartSeries(myChart, chartLegendEntry.Name)
                    End If
                End If
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info")
            'System.Threading.Thread.CurrentThread.CurrentUICulture = Globalization.CultureInfo.GetCultureInfo("en-US")
            'System.Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.GetCultureInfo("en-US")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadChart(Optional kpi As String = "")
        Try
            Dim strConnection As String, sqlParam As String
            Dim parray1()() As String = {New String() {"@AlertId", "'" & selectedAlertRule.Item(2) & "'"}, New String() {"@filter", ""}}
            strConnection = GetSQL(3815, parray1)(0)
            sqlParam = GetSQL(3815, parray1)(1)
            Dim dt_chart As New DataTable
            dt_chart = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            If dt_chart IsNot Nothing Then

                '          If Not dt_chart.Columns.Contains("KPI_RuleTypeName_Short") Then
                '              dt_chart.Columns.Add("KPI_RuleTypeName_Short", GetType(String))
                '          End If

                '          Dim dtRuleType As DataTable = dtKPIRules.DefaultView.ToTable(True, {"KPI_Name", "KPI_RuleTypeName_Short"}).Select("", "KPI_Name ASC").CopyToDataTable()
                '          Dim index As Integer = 0

                '          For Each drKpi As DataRow In dtKPIRules.DefaultView.ToTable(True, {"KPI_Name"}).Select("", "KPI_Name ASC")
                '              index = 0
                '              For Each dr As DataRow In dt_chart.Select("ChartElements='" & drKpi("KPI_Name") & "'", "ChartElements ASC")
                '                  Dim dr1 As DataRow() = dtRuleType.Select("KPI_Name='" & drKpi("KPI_Name") & "'")
                '                  If dr1.Length > 0 Then
                '	dr("KPI_RuleTypeName_Short") = dr1(0)("KPI_RuleTypeName_Short")
                'End If
                '                  index = index + 1
                '              Next
                '          Next

                If Not dt_chart.Columns.Contains("KPI_RuleTypeName_Short") Then
                    dt_chart.Columns.Add("KPI_RuleTypeName_Short", GetType(String))
                End If

                Dim dtRuleType As DataTable = dtKPIRules.DefaultView.ToTable(True, {"KPI_RULEID", "KPI_Name", "KPI_RuleTypeName_Short"}).Select(IIf(kpi = "", "", "KPI_RULEID='" & kpi & "'"), "KPI_Name ASC").CopyToDataTable()
                Dim kpinameIsBreach_selected As String = ""
                If kpi <> "" Then
                    kpinameIsBreach_selected = dtRuleType(0)("KPI_Name") + "_IsBreach_" + dtRuleType(0)("KPI_RuleTypeName_Short")
                End If
                Dim index As Integer = 0

                For Each drKpi As DataRow In dtKPIRules.DefaultView.ToTable(True, {"KPI_RULEID", "KPI_Name"}).Select("", "KPI_Name ASC")
                    index = 0
                    For Each dr As DataRow In dt_chart.Select("KPI_RULEID='" & drKpi("KPI_RULEID") & "'", "ChartElements ASC")
                        Dim dr1 As DataRow() = dtRuleType.Select("KPI_RULEID='" & drKpi("KPI_RULEID") & "'")
                        If dr1.Length > 0 Then
                            dr("KPI_RuleTypeName_Short") = dr1(index)("KPI_RuleTypeName_Short")
                        End If
                        index = index + 1
                    Next
                Next

                AssignDataToCharts_New(chAnomaly, dtChartData, selectedAlertRule.Item(3), selectedAlertRule.Item(4), dt_chart, kpi)

                If kpi <> "" Then
                    'Add Marker to chart if breached
                    AddAxisMarker(kpinameIsBreach_selected & "_IsBreach")
                End If

                'Add alert detected axis marker
                If dtChartData.Columns.Contains("IsDetected") Then
                    AddAxisMarker("IsDetected")
                End If

                'Add alert triggered axis marker
                If dtChartData.Columns.Contains("IsTriggered") Then
                    AddAxisMarker("IsTriggered")
                End If

                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcChartData, gvChartData, dtChartData, "ALL")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddAxisMarker(condition As String)
        Try
            If condition = "IsDetected" AndAlso ceShowAlertDetected.Checked = False Then
                Exit Sub
            ElseIf condition = "IsTriggered" AndAlso ceShowAlertTriggered.Checked = False Then
                Exit Sub
            ElseIf condition.Contains("_IsBreach") AndAlso ceShowKPIBreach.Checked = False Then
                Exit Sub
            End If

            Dim dtMarker As New DataTable
            If condition <> "" Then
                If dtChartData IsNot Nothing Then
                    If condition.ToLower.Contains("isbreach") Then
                        dtMarker = dtChartData.Select(condition).CopyToDataTable()
                    Else
                        dtMarker = dtChartData.Select("[" & condition & "]=1").CopyToDataTable()
                    End If

                    Dim am As AxisMarker
                    For Each Row As DataRow In dtMarker.Rows
                        If condition = "IsDetected" Then
                            am = New AxisMarker(Row("Period_Start_Time").ToString, New Line(Color.DarkOrange, 2, Drawing2D.DashStyle.Dot), Row("Period_Start_Time"))
                        ElseIf condition = "IsTriggered" Then
                            am = New AxisMarker(Row("Period_Start_Time").ToString, New Line(Color.IndianRed, 3, Drawing2D.DashStyle.Dash), Row("Period_Start_Time"))
                        Else
                            Dim rnd As New Random()
                            am = New AxisMarker(Row("Period_Start_Time").ToString, New Line(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), 2, Drawing2D.DashStyle.Dot), Row("Period_Start_Time"))
                        End If
                        am.LegendEntry.Visible = False
                        am.Label.Hotspot.ToolTip = Row("Period_Start_Time").ToString
                        am.Label.Color = Color.Empty
                        am.Label.Alignment = StringAlignment.Near
                        am.Label.LineAlignment = StringAlignment.Far
                        am.BringToFront = True
                        am.Label.Text = condition
                        chAnomaly.XAxis.Markers.Add(am)
                    Next
                    chAnomaly.RefreshChart()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddAxisMarkerForBreach()
        Try
            If dtChartData IsNot Nothing Then
                Dim strBreachedCol() As String = Nothing
                Dim conditionIsBreached As String = ""
                ReDim strBreachedCol(dtChartData.Rows.Count)
                strBreachedCol(0) = "Period_Start_Time"

                Dim i As Integer = 1

                For Each col As DataColumn In dtChartData.Columns
                    If col.ColumnName.ToLower.Contains("_isbreach") Then
                        strBreachedCol(i) = col.ColumnName
                        If conditionIsBreached.Length = 0 Then
                            conditionIsBreached = "[" & col.ColumnName & "]=1 "
                        Else
                            conditionIsBreached = conditionIsBreached & " OR [" & col.ColumnName & "]=1"
                        End If
                        i = i + 1
                    End If
                Next
                AddAxisMarker(conditionIsBreached)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddSlidingWindowAxisMarker(Optional startDate As Date = Nothing, Optional endDate As Date = Nothing)
        Try
            If ceShowSlidingWindow.Checked = True Then
                Dim shade As AxisMarker = Nothing
                If startDate <> Nothing AndAlso endDate <> Nothing Then
                    shade = New AxisMarker(Format(startDate, "dd/MM/yy") & " - " & Format(endDate, "dd/MM/yy"), New Background(Color.FromArgb(100, Color.Green)), startDate, endDate)
                Else
                    Dim rIndex() As Integer = gvAlerts.GetSelectedRows()
                    If rIndex.Length > 0 Then
                        Dim dr As DataRow = gvAlerts.GetDataRow(rIndex(0))
                        Dim sDate As Date = DateAdd(DateInterval.Day, -1 * dr.Item("#StartDate"), dr.Item("AlertDate"))
                        Dim eDate As Date = dr.Item("AlertDate")
                        shade = New AxisMarker(Format(sDate, "dd/MM/yy") & " - " & Format(eDate, "dd/MM/yy"), New Background(Color.FromArgb(100, Color.Green)), sDate, eDate)
                    End If
                End If
                shade.LegendEntry.Visible = False
                shade.Label.LineAlignment = StringAlignment.Near
                shade.Label.Alignment = StringAlignment.Center
                shade.LegendEntry.Value = ""
                chAnomaly.XAxis.Markers.Add(shade)
                chAnomaly.RefreshChart()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub AssignDataToCharts_New(ByRef ch As Chart, ByRef dt As DataTable, ByVal alertName As String, ObjectName As String, dt_chart As DataTable, Optional SelectedKPI As String = "")
        DefaultSeriesCollection.Clear()
        ExtraLegendEntryCollection.Clear()
        ch.SeriesCollection.Clear()

        Dim objectscharted As String = ""
        Dim i As Integer
        Dim yaxis1 As Axis = Nothing
        Dim yaxis2 As Axis = Nothing
        Dim color_R, color_B, color_G As Integer
        Dim chart_elements() As String = {"0"}
        Dim chart_elementsYAxis() As String = {"0"}
        Dim chart_Eltype() As String = {"Bar"}
        Dim chart_ElColor() As Integer = {0}
        Dim j As Integer = 0
        Dim rownum As Integer = 0
        Dim KpiValue = "", KpiUpperThreshold = "", KpiLowerThreshold As String = ""

        ch.DefaultElement.Marker.Visible = False
        ch.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
        ch.LegendBox.DefaultEntry.Value = ""
        ch.LegendBox.DefaultEntry.Hotspot.ToolTip = "%Name"
        ch.LegendBox.Visible = True

        ch.XAxis.TickLabelMode = TickLabelMode.Angled
        ch.XAxis.TickLabelAngle = 45
        ch.XAxis.Minimum = 0
        ch.XAxis.Maximum = 0

        ch.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Smart

        ch.ToolTip.InitialDelay = 1
        ch.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
        ch.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None
        ch.CleanupPeriod = 1

        ch.XAxis.TimeScaleLabels.RangeIntervals.Clear()
        ch.XAxis.TimeScaleLabels.Mode = TimeScaleLabelMode.Default
        ch.XAxis.TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Default
        ch.XAxis.TimeInterval = TimeInterval.Days
        ch.XAxis.FormatString = "dd/MM/yy"
        ch.XAxis.TimeScaleLabels.DayFormatString = "dd/MM/yy"
        ch.XAxis.TimeScaleLabels.RangeIntervals.Add(TimeInterval.Months)
        ch.XAxis.TimeScaleLabels.MonthFormatString = "MMMM yyyy"
        ch.LegendBox.Orientation = Orientation.Bottom
        ch.LegendBox.ExtraEntries.Clear()

        Try
            For rownum = 0 To dt_chart.Select(IIf(SelectedKPI = "", "", "KPI_RULEID='" & SelectedKPI & "'")).Count - 1
                Dim drow As DataRow = dt_chart.Select(IIf(SelectedKPI = "", "", "KPI_RULEID='" & SelectedKPI & "'"))(rownum)

                If SelectedKPI <> "" Then
                    KpiValue = "_Value_" & drow("KPI_RuleTypeName_Short").ToString
                    KpiLowerThreshold = "_LowerThreshold_" & drow("KPI_RuleTypeName_Short").ToString
                    KpiUpperThreshold = "_UpperThreshold_" & drow("KPI_RuleTypeName_Short").ToString
                Else
                    KpiValue = ""
                    KpiLowerThreshold = ""
                    KpiUpperThreshold = ""
                End If

                If chart_elements.Length > 0 Then
                    If chart_elements.Contains(drow(4).ToString.Trim & KpiValue) Then
                        Continue For
                    End If
                End If
                Try
                    Do While Not ColumnInDataTable(drow(4).ToString.Trim & KpiValue, dt)
                        rownum = rownum + 1
                        If rownum <= dt_chart.Select(IIf(SelectedKPI = "", "", "KPI_RULEID='" & SelectedKPI & "'")).Count - 1 Then
                            drow = dt_chart.Select(IIf(SelectedKPI = "", "", "KPI_RULEID='" & SelectedKPI & "'"))(rownum)
                            If SelectedKPI <> "" Then
                                KpiValue = "_Value_" & drow("KPI_RuleTypeName_Short").ToString
                                KpiLowerThreshold = "_LowerThreshold_" & drow("KPI_RuleTypeName_Short").ToString
                                KpiUpperThreshold = "_UpperThreshold_" & drow("KPI_RuleTypeName_Short").ToString
                            Else
                                KpiValue = ""
                                KpiLowerThreshold = ""
                                KpiUpperThreshold = ""
                            End If
                        Else
                            Exit For
                        End If
                    Loop
                Catch ex As Exception
                End Try

                ch.Annotations.Clear()
                ch.Annotations.Add(New Annotation(alertName.ToUpper))

                If alertName.Length > 3 Then
                    Dim fnt As Font = New Font("Arial", 7, FontStyle.Regular)
                    ch.Annotations(0).Label.Font = fnt
                    ch.Annotations(0).DynamicSize = False
                    Dim textSize As Size = TextRenderer.MeasureText(alertName, New System.Drawing.Font("Arial", 9, GraphicsUnit.Point))
                    ch.Annotations(0).Position = New Point(ch.Width - textSize.Width, 2)
                End If

                If ObjectName <> "" Then
                    objectscharted = ObjectName
                End If

                ch.TitleBox.Label.Text = "Objects: " & objectscharted
                ch.TitleBox.HeaderLabel.Text = drow(4).ToString.Trim

                ch.TitleBox.Label.Alignment = StringAlignment.Near
                ch.TitleBox.Label.LineAlignment = StringAlignment.Near
                ch.DefaultElement.Hotspot.ToolTip = "DATE: %XValue" & Chr(13) & "%SeriesName: %Value "

                Dim KPIName() As String
                If SelectedKPI <> "" Then
                    If dt.Select(drow(4).ToString.Trim & KpiLowerThreshold & " <> 0").Count = 0 Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiUpperThreshold}
                    ElseIf dt.Select(drow(4).ToString.Trim & KpiUpperThreshold & " <> 0").Count = 0 Then
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiLowerThreshold}
                    Else
                        KPIName = {drow(4).ToString.Trim & KpiValue, drow(4).ToString.Trim & KpiLowerThreshold, drow(4).ToString.Trim & KpiUpperThreshold}
                    End If
                Else
                    KPIName = {drow(4).ToString.Trim}
                End If

                For index As Integer = 0 To KPIName.Length - 1
                    If ColumnInDataTable(KPIName(index).ToString.Trim, dt) Then
                        ReDim Preserve chart_elements(j)
                        ReDim Preserve chart_Eltype(j)
                        ReDim Preserve chart_ElColor(j)
                        chart_elements(j) = KPIName(index).ToString.Trim

                        If KPIName(index).ToString.Trim.ToLower.Contains("threshold") Then
                            chart_Eltype(j) = "DOTTEDLINE"
                            chart_ElColor(j) = 0
                        Else
                            chart_Eltype(j) = drow(5).trim
                            chart_ElColor(j) = CInt(drow(14))
                        End If

                        j = j + 1
                    End If
                Next
            Next

            Dim xaxis_valuehigh As DateTime = Today
            If dt.Rows.Count > 0 Then
                xaxis_valuehigh = dt.Compute("MAX(Period_Start_Time)", "")
                xaxis_valuehigh = xaxis_valuehigh.AddDays(1) 'Add 1 day to insert a extra x-axis scale.
            End If

            If xaxis_valuehigh.Date = Now.Date Then
                xaxis_valuehigh = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Hour, 12, xaxis_valuehigh.Date))
            End If

            ch.XAxis.ScaleRange.ValueHigh = xaxis_valuehigh
            Dim de As DataEngine = New DataEngine(dt)
            de.DataFields = String2DataFields(chart_elements, "PERIOD_START_TIME")
            de.DataGridFormatString = "N2"
            de.FormatString = "dd/MM/yy"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()
            Dim lastKpiName As String = ""
            Dim currentKpiName As String = ""
            Dim yAxis As Axis = Nothing
            Dim kpiIndex As Integer = 0
            For i = 0 To sc.Count() - 1

                Select Case UCase(chart_Eltype(i).Trim)
                    Case "LINE"
                        sc(i).Type = SeriesType.Line
                        sc(i).Line.Width = 3
                    Case "BAR"
                        sc(i).Type = SeriesType.Bar
                    Case "AREALINE"
                        sc(i).Type = SeriesType.AreaLine
                    Case "DOTTEDLINE"
                        sc(i).Type = SeriesType.Line
                        sc(i).Line.Width = 2
                        sc(i).Line.DashStyle = Drawing2D.DashStyle.Dot
                    Case "BLUEDOT"
                        sc(i).Type = SeriesType.Marker
                        sc(i).Element.Marker.Type = ElementMarkerType.Circle
                        sc(i).DefaultElement.Marker.Visible = True
                        sc(i).DefaultElement.ForceMarker = True
                End Select

                If SelectedKPI <> "" Then
                    currentKpiName = sc(i).Name.Substring(0, sc(i).Name.LastIndexOf("_")).Substring(0, sc(i).Name.Substring(0, sc(i).Name.LastIndexOf("_")).LastIndexOf("_"))
                Else
                    currentKpiName = sc(i).Name
                End If

                'Generate Y-Axis for each KPI in the chart
                If lastKpiName <> currentKpiName Then

                    Dim drKPISettings() As DataRow = dt_chart.Select("KPI_Name='" & currentKpiName & "'")
                    If drKPISettings.Length > 0 Then
                        yAxis = New Axis()

                        If UCase(drKPISettings(drKPISettings.Length - 1)("ChartElementsYAxis").trim) = "LEFT" Then
                            yAxis.Orientation = Orientation.Left
                            If nZ(drKPISettings(drKPISettings.Length - 1)("ChartY1AbsPerc"), "Abs").ToUpper = "PERC" Then
                                yAxis.Percent = True
                            End If
                            yAxis.NumberPrecision = CInt(nZ(drKPISettings(drKPISettings.Length - 1)("chartY1AxisPrecision"), "0"))
                        Else
                            yAxis.Orientation = Orientation.Right
                            If nZ(drKPISettings(drKPISettings.Length - 1)("ChartY2AbsPerc"), "Abs").ToUpper = "PERC" Then
                                yAxis.Percent = True
                            End If
                            yAxis.NumberPrecision = CInt(nZ(drKPISettings(drKPISettings.Length - 1)("chartY2AxisPrecision"), "0"))
                        End If

                        If UCase(drKPISettings(drKPISettings.Length - 1)("ChartElementsType").trim) = "STACKED" Then
                            yAxis.Scale = dotnetCHARTING.WinForms.Scale.Stacked
                        ElseIf UCase(drKPISettings(drKPISettings.Length - 1)("ChartElementsType").trim) = "FULLSTACKED" Then
                            yAxis.Scale = dotnetCHARTING.WinForms.Scale.FullStacked
                        Else
                            yAxis.Scale = dotnetCHARTING.WinForms.Scale.Range
                        End If

                        yAxis.Label.Text = currentKpiName

                        If yAxis.NumberPrecision < 2 And Not yAxis.Percent = True Then
                            yAxis.MinimumInterval = 1
                        End If
                    End If
                    kpiIndex = kpiIndex + 1
                End If

                lastKpiName = currentKpiName
                sc(i).YAxis = yAxis

                If chart_ElColor(i) = 0 Then
                    sc(i).DefaultElement.Color = Color.Black
                Else
                    color_R = CLng(chart_ElColor(i)) Mod 256
                    color_G = (CLng(chart_ElColor(i)) \ 256) Mod 256
                    color_B = ((CLng(chart_ElColor(i)) \ 256) \ 256) Mod 256

                    sc(i).DefaultElement.Color = Color.FromArgb(255, color_R, color_G, color_B)
                End If

                sc(i).DefaultElement.Marker.Type = i

                If DefaultSeriesCollection.ContainsKey(sc(i).Name) Then
                    DefaultSeriesCollection.Remove(sc(i).Name)
                End If
                DefaultSeriesCollection.Add(sc(i).Name, sc(i))
            Next

            ch.SeriesCollection.Clear()
            ch.SeriesCollection.Add(sc)

            sc = Nothing
            de = Nothing
            ch.XAxis.Markers.Clear()

            ReDim chart_elements(0)
            ReDim chart_elementsYAxis(0)
            ReDim chart_Eltype(0)
            ReDim chart_ElColor(0)
            j = 0

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Console.WriteLine(ex.Message.ToString)
        Finally
            ch.RefreshChart()
            ch.ResumeLayout()
        End Try

        mm.ReleaseMemory()
        dt_chart.Dispose()
        dt_chart = Nothing
        System.GC.Collect()
    End Sub

    Private Sub chAnomaly_SizeChanged(sender As Object, e As EventArgs) Handles chAnomaly.SizeChanged
        Try
            If chAnomaly.Annotations.Count > 0 Then
                Dim textSize As Size = TextRenderer.MeasureText(chAnomaly.Annotations(0).Label.Text, New System.Drawing.Font("Arial", 9, GraphicsUnit.Point))
                chAnomaly.Annotations(0).Position = New Point(chAnomaly.Width - textSize.Width, 2)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub chartAnomaly_DragDrop(sender As Object, e As DragEventArgs) Handles chAnomaly.DragDrop
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim mm() As Object = e.Data.GetData("System.Object[]")
            If mm IsNot Nothing Then
                Dim strConnection As String, sqlParam As String
                Dim dr As DataRow = CType(mm(1), DataRow)
                If mm(0) = "AlertDrag" Then
                    Dim parray()() As String = {
                        New String() {"@AlertID", "'" & dr.Item("AlertID") & "'"},
                        New String() {"@DataPoints", "'" & seDataPoint.Value & "'"},
                        New String() {"@Object", "'" & dr.Item("ObjectName") & "'"}
                    }

                    strConnection = GetSQL(3802, parray)(0)
                    sqlParam = GetSQL(3802, parray)(1)
                    Dim ds As New DataSet
                    ds = IOS.DataLibrary.DataAccessorODBC.GetDataSet(strConnection, sqlParam, iQryTimeOut)
                    dtChartData = ds.Tables(0)
                    LoadChart()

                    Dim sDate As Date = dr.Item("#StartDate")    '* dr.Item("#StartDate") 
                    Dim eDate As Date = dr.Item("AlertDate")
                    AddSlidingWindowAxisMarker(sDate, eDate)

                    'Add Marker to chart based on alert date
                    Dim dtAlertDate As New DataTable
                    Try
                        dtAlertDate = ds.Tables(1)
                    Catch
                    End Try
                    If dtAlertDate.Rows.Count > 0 Then
                        For Each Row As DataRow In dtAlertDate.Rows
                            Dim _marker As New AxisMarker(Row("AlertDate").ToString, New Line(Color.Black, 1), Row("AlertDate"))
                            _marker.LegendEntry.Visible = False
                            _marker.Label.Hotspot.ToolTip = Row("AlertDate").ToString
                            _marker.Label.Color = Color.Empty
                            _marker.Label.Alignment = StringAlignment.Near
                            _marker.Label.LineAlignment = StringAlignment.Far
                            _marker.BringToFront = True
                            chAnomaly.XAxis.Markers.Add(_marker)
                        Next
                        chAnomaly.RefreshChart()
                    End If

                ElseIf mm(0) = "KPIRuleDrag" Then
                    dtChartData = New DataTable
                    For Each drRule As DataRow In dtKPIRules.Select("KPI_RuleID='" & dr.Item("KPI_RULEID") & "'")
                        Dim parray()() As String = {
                            New String() {"@KPIRuleID", drRule.Item(4)},
                            New String() {"@ObjectName", "'" & drRule.Item(3) & "'"},
                            New String() {"@FilterThreshold", 0},
                            New String() {"@DaysInChart", seDataPoint.EditValue}
                        }

                        strConnection = GetSQL(3824, parray)(0)
                        sqlParam = GetSQL(3824, parray)(1)
                        Dim dtChart As New DataTable
                        dtChart = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                        If dtChart IsNot Nothing Then
                            dtChart.Columns("KPIValue").ColumnName = drRule.Item(5).ToString & "_Value_" & drRule.Item("KPI_RuleTypeName_Short").ToString()
                            dtChart.Columns("LowerThresholdLine").ColumnName = drRule.Item(5).ToString & "_LowerThreshold_" & drRule.Item("KPI_RuleTypeName_Short").ToString()
                            dtChart.Columns("UpperThresholdLine").ColumnName = drRule.Item(5).ToString & "_UpperThreshold_" & drRule.Item("KPI_RuleTypeName_Short").ToString()
                            dtChart.Columns("IsBreach").ColumnName = drRule.Item(5).ToString & "_IsBreach_" & drRule.Item("KPI_RuleTypeName_Short").ToString()

                            Dim dtTemp As DataTable = dtChart.DefaultView.ToTable(False, {"Period_Start_Time", drRule.Item(5).ToString & "_Value_" & drRule.Item("KPI_RuleTypeName_Short").ToString(), drRule.Item(5).ToString & "_LowerThreshold_" & drRule.Item("KPI_RuleTypeName_Short").ToString(), drRule.Item(5).ToString & "_UpperThreshold_" & drRule.Item("KPI_RuleTypeName_Short").ToString(), drRule.Item(5).ToString & "_IsBreach_" & drRule.Item("KPI_RuleTypeName_Short").ToString()})
                            Dim primkeys(1) As DataColumn
                            primkeys(0) = dtTemp.Columns("Period_Start_time")
                            dtTemp.PrimaryKey = primkeys
                            dtChartData.Merge(dtTemp)
                        End If
                    Next

                    Dim dataView As New DataView(dtChartData)
                    dataView.Sort = "PERIOD_START_TIME ASC"
                    Dim dtChartData_Sorted As DataTable = dataView.ToTable()
                    dtChartData = dtChartData_Sorted
                    '****************************
                    LoadChart(dr.Item("KPI_RULEID").ToString)
                ElseIf mm(0) = "PositiveCorrDrag" Or mm(0) = "NegativeCorrDrag" Then
                    Dim drKPIRule As DataRow
                    drKPIRule = gvRulesDetected.GetFocusedDataRow()

                    Dim parray()() As String = {
                        New String() {"@kpiruleid", "'" & drKPIRule.Item(4) & "'"},
                        New String() {"@KpiName", "'" & dr.Item(1) & "'"},
                        New String() {"@Tech", "'" & drKPIRule.Item(2) & "'"},
                        New String() {"@DataPoints", "" & seDataPoint.Value & ""},
                        New String() {"@Object", "'" & drKPIRule.Item(3) & "'"}
                    }

                    strConnection = GetSQL(3807, parray)(0)
                    sqlParam = GetSQL(3807, parray)(1)

                    dtKPICorrelationChartData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

                    Dim chartEle() As String = {dr.Item(1)}
                    Dim de As DataEngine = New DataEngine(dtKPICorrelationChartData)
                    de.DataFields = String2DataFields(chartEle, "PERIOD_START_TIME")
                    de.DataGridFormatString = "N2"
                    de.FormatString = "dd/MM/yy"
                    Dim sc As New SeriesCollection
                    sc = de.GetSeries()

                    For i = 0 To sc.Count() - 1

                        sc(i).Type = SeriesType.Line
                        sc(i).Line.Width = 3
                        sc(i).DefaultElement.Color = Color.Black
                        sc(i).DefaultElement.Marker.Type = i

                        Dim new_yaxis As New Axis
                        new_yaxis.Scale = dotnetCHARTING.WinForms.Scale.Range
                        new_yaxis.Label.Text = sc(i).Name
                        sc(i).YAxis = new_yaxis

                        If chAnomaly.SeriesCollection.Contains(chAnomaly.SeriesCollection.GetSeries(sc(i).Name)) = False Then
                            If DefaultSeriesCollection.ContainsKey(sc(i).Name) Then
                                DefaultSeriesCollection.Remove(sc(i).Name)
                            End If
                            DefaultSeriesCollection.Add(sc(i).Name, sc(i))
                            chAnomaly.SeriesCollection.Add(sc(i))
                        End If
                    Next

                    sc = Nothing
                    de = Nothing
                    chAnomaly.RefreshChart()
                End If
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ShowHideChartSeries(ByRef chart As Chart, ByVal SeriesName As String)
        If chart.SeriesCollection.GetSeries(SeriesName) IsNot Nothing Then
            If chart.SeriesCollection.Count > 1 Then

                chart.SeriesCollection.Remove(chart.SeriesCollection.GetSeries(SeriesName))

                Dim entry As New LegendEntry()
                entry.Name = SeriesName
                entry.LabelStyle.Color = Color.Gray
                chart.LegendBox.ExtraEntries.Add(entry)

                If ExtraLegendEntryCollection.ContainsKey(SeriesName) Then
                    ExtraLegendEntryCollection.Remove(SeriesName)
                End If
                ExtraLegendEntryCollection.Add(SeriesName, entry)
            End If
        Else
            If ExtraLegendEntryCollection.ContainsKey(SeriesName) Then
                Dim LegendColl As LegendEntry = ExtraLegendEntryCollection.Item(SeriesName)
                ExtraLegendEntryCollection.Remove(SeriesName)
                chart.LegendBox.ExtraEntries.Clear()
                chart.SeriesCollection.Clear()
                For Each obj As KeyValuePair(Of String, Series) In DefaultSeriesCollection
                    If ExtraLegendEntryCollection.ContainsKey(obj.Key) = False Then
                        chart.SeriesCollection.Add(obj.Value)
                    End If
                Next

                For Each obj As KeyValuePair(Of String, LegendEntry) In ExtraLegendEntryCollection
                    chart.LegendBox.ExtraEntries.Add(obj.Value)
                Next

            End If
        End If
        chart.Refresh()
    End Sub

    Private Sub ceShowAlertDetected_CheckedChanged(sender As Object, e As EventArgs) Handles ceShowAlertDetected.CheckedChanged, ceShowAlertTriggered.CheckedChanged, ceShowKPIBreach.CheckedChanged, ceShowSlidingWindow.CheckedChanged
        chAnomaly.XAxis.Markers.Clear()
        AddAxisMarker("IsDetected")
        AddAxisMarker("IsTriggered")
        AddAxisMarkerForBreach()
        AddSlidingWindowAxisMarker()
        chAnomaly.RefreshChart()
    End Sub

#End Region

#Region "Common Control's Event"

    Private Sub tglAlertTest_Click(sender As Object, e As EventArgs) Handles tglAlertTest.Click
        Try


            tglAlertTest.ChangeToggleState()
            If tglAlertTest.ToggleState = CheckState.Checked Then
                tglAlertTest.Text = "Hide Grid"
                sccChart.Collapsed = False
            ElseIf tglAlertTest.ToggleState = CheckState.Unchecked Then
                tglAlertTest.Text = "Show Grid"
                sccChart.Collapsed = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAlertManager_Click(sender As Object, e As EventArgs) Handles btnAlertManager.Click
        frmAlertManager.Show()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            FillAlertGrid()
            FillSuppressGrid()
            FillAlertScoreGrid()
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdRulesDetected)
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdPositive)
            IOS.Library.IOSDevExpressGrid.ClearGrid(grdNegative)

            btnClearChart_Click(Nothing, Nothing)
            gvAlerts.FocusedRowHandle = 0
            gcAlerts.Refresh()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tbcCorrelationFilter_EditValueChanged(sender As Object, e As EventArgs) Handles tbcCorrelationFilter.EditValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillKPICorrelationGrid()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbCorrType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCorrType.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If gvRulesDetected.RowCount > 0 Then
                Dim dr As DataRow = gvRulesDetected.GetFocusedDataRow()
                GetKPICorrelationData(dr)
                FillKPICorrelationGrid()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnClearChart_Click(sender As Object, e As EventArgs) Handles btnClearChart.Click
        ExtraLegendEntryCollection.Clear()
        DefaultSeriesCollection.Clear()
        chAnomaly.LegendBox.ExtraEntries.Clear()
        chAnomaly.SeriesCollection.Clear()
        chAnomaly.Annotations.Clear()
        chAnomaly.XAxis.Markers.Clear()
        chAnomaly.YAxis.Markers.Clear()
        dtKPICorrelationChartData = Nothing
        dtChartData = Nothing
        chAnomaly.Refresh()

        gvChartData.Columns.Clear()
        gcChartData.DataSource = Nothing
    End Sub

    Private Sub xtcAlerts_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcAlerts.SelectedPageChanged
        Try
            If xtcAlerts.SelectedTabPage.Text.ToLower = "score" Then
                gcNotes.Enabled = False
                gcRules.Enabled = False
                gcCorrelation.Enabled = False
            Else
                gcNotes.Enabled = True
                gcRules.Enabled = True
                gcCorrelation.Enabled = True
            End If
            btnClearChart.PerformClick()
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Helper"

    Private Sub DeleteSuppression()
        Try
            If XtraMessageBox.Show("Are you sure to delete suppression?", "Delete Alert Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim strConnection As String, sqlParam As String
                Dim rowIndex() As Integer
                rowIndex = gvSuppressed.GetSelectedRows()
                For i As Integer = 0 To rowIndex.Length - 1
                    Dim dr As DataRowView = gvSuppressed.GetRow(rowIndex(i))
                    If dr IsNot Nothing Then
                        Dim parray()() As String = {
                                                               New String() {"@selectedAlertRuleID", "" & dr.Item(0) & ""},
                                                               New String() {"@selectedObjectName", "'" & dr.Item(2) & "'"}
                                                               }

                        strConnection = GetSQL(3826, parray)(0)
                        sqlParam = GetSQL(3826, parray)(1)
                        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                        FillSuppressGrid()
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SuppressAlert(alertId As Integer, objectName As String, suppressTillDate As Date)
        Dim strConnection As String, sqlParam As String
        Dim parray()() As String = {
            New String() {"@AlertID", "'" & alertId & "'"},
            New String() {"@ObjectName", "'" & objectName & "'"},
            New String() {"@SuppressTillDate", "'" & suppressTillDate & "'"},
            New String() {"@UserName", "'" & Environment.UserName & "'"}
        }

        strConnection = GetSQL(3808, parray)(0)
        sqlParam = GetSQL(3808, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
        FillSuppressGrid()
    End Sub

    Private Sub FillAlertGrid()
        grdAlerts.SuspendLayout()
        RemoveHandler gvAlerts.FocusedRowChanged, AddressOf gvAlerts_FocusedRowChanged

        GetAlertDataLoadedIntoDT()
        Dim rIndex() As Integer = gvAlerts.GetSelectedRows()
        IOSDevExpressGrid.PopulateDataInGrid(grdAlerts, grdAlerts.MainView, dtAlerts, "ALL")

        UpdateAlertGridNoteColumn()
        gvAlerts.ClearSelection()
        gvAlerts.OptionsSelection.MultiSelect = True
        If gvAlerts.RowCount > 0 Then
            If rIndex.Length > 0 Then
                gvAlerts.SelectRow(rIndex(0))
                gvAlerts.FocusedRowHandle = rIndex(0)
            Else
                gvAlerts.SelectRow(0)
                gvAlerts.FocusedRowHandle = 0
            End If
        End If
        grdAlerts.ResumeLayout()
        AddHandler gvAlerts.FocusedRowChanged, AddressOf gvAlerts_FocusedRowChanged
        gvAlerts_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub UpdateAlertGridNoteColumn()
        If gvAlerts.Columns.Count > 0 Then
            gvAlerts.Columns(0).Visible = False
            gvAlerts.Columns(gvAlerts.Columns.Count - 1).Visible = False

            Dim checkEdit As RepositoryItemCheckEdit = TryCast(grdAlerts.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
            checkEdit.PictureChecked = EmbeddedImage("envelope.png")
            checkEdit.PictureUnchecked = Nothing
            checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
            gvAlerts.Columns("Note").ColumnEdit = checkEdit
        End If
    End Sub

    Private Sub GetAlertDataLoadedIntoDT()
        Dim strConnection As String, sqlParam As String
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3803, parray)(0)
        sqlParam = GetSQL(3803, parray)(1)
        dtAlerts = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub FillSuppressGrid()
        grdSuppressed.SuspendLayout()
        Dim strConnection As String, sqlParam As String
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3825, parray)(0)
        sqlParam = GetSQL(3825, parray)(1)

        dtSuppressedAlert = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(grdSuppressed, grdSuppressed.MainView, dtSuppressedAlert, "ALL")
        gvSuppressed.OptionsSelection.MultiSelect = True

        If gvSuppressed.Columns.Count > 0 Then
            gvSuppressed.Columns(0).Visible = False
        End If
        grdSuppressed.ResumeLayout()
    End Sub

    Private Sub FillAlertScoreGrid()
        RemoveHandler gvScore.FocusedRowChanged, AddressOf gvScore_FocusedRowChanged
        grdScore.SuspendLayout()

        Dim strConnection As String, sqlParam As String
        Dim parray()() As String = Nothing
        strConnection = GetSQL(3846, parray)(0)
        sqlParam = GetSQL(3846, parray)(1)

        dtAlertScore = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(grdScore, grdScore.MainView, dtAlertScore, "ALL", Nothing, "ObjectName")
        gvScore.OptionsSelection.MultiSelect = True

        grdScore.ResumeLayout()
        AddHandler gvScore.FocusedRowChanged, AddressOf gvScore_FocusedRowChanged
    End Sub

    Private Sub FillAlertNoteGrid()
        grdNotes.SuspendLayout()
        If selectedAlertRule IsNot Nothing Then
            Dim strConnection As String, sqlParam As String
            Dim parray()() As String = {
                                           New String() {"@AlertTriggerID", "" & selectedAlertRule.Item(0) & ""}
                                          }
            strConnection = GetSQL(3821, parray)(0)
            sqlParam = GetSQL(3821, parray)(1)
            dtNote = Nothing
            dtNote = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            IOSDevExpressGrid.PopulateDataInGrid(grdNotes, grdNotes.MainView, dtNote, "ALL")
            If gvNotes.Columns.Count > 0 Then
                gvNotes.Columns(0).Visible = False
                gvNotes.AutoFillColumn = gvNotes.Columns(3)
            End If
            gvNotes.OptionsSelection.MultiSelect = True
        End If
        grdNotes.ResumeLayout()
    End Sub

    Private Sub GetKPICorrelationData(dr As DataRow)
        Dim strConnection As String, sqlParam As String
        Dim parray()() As String = {
                                           New String() {"@CorrelationMethod", "" & CType(cmbCorrType.SelectedItem, IOS.Library.clsComboBoxItem).Value & ""},
                                           New String() {"@Tech", "'" & dr.Item(2) & "'"},
                                           New String() {"@kpiruleid", "'" & dr.Item(4) & "'"},
                                           New String() {"@ObjectName", "'" & dr.Item(3) & "'"},
                                           New String() {"@DataPoints", "" & seDataPoint.Value & ""},
                                           New String() {"@kpiname", "'" & dr.Item(5) & "'"},
                                           New String() {"@Object", "0"}
                                          }

        strConnection = GetSQL(3806, parray)(0)
        sqlParam = GetSQL(3806, parray)(1)
        dtKPICorrelation = Nothing
        dtKPICorrelation = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub FillKPICorrelationGrid()
        If dtKPICorrelation IsNot Nothing Then
            Dim CoefCol As String = "PearsonCoefficient"
            If CType(cmbCorrType.SelectedItem, IOS.Library.clsComboBoxItem).Value = 2 Then
                CoefCol = "NXCoefficient"
            End If

            Dim dr1() As DataRow
            dr1 = dtKPICorrelation.Select("[" & CoefCol & "]>=" & GetCorrelationValue(), "[" & CoefCol & "] DESC")
            If dr1.Length > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(grdPositive, grdPositive.MainView, dr1.CopyToDataTable(), "ALL")
            End If

            Dim dr() As DataRow
            dr = dtKPICorrelation.Select("[" & CoefCol & "]<=" & -1 * GetCorrelationValue(), "[" & CoefCol & "] ASC")
            If dr.Length > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(grdNegative, grdNegative.MainView, dr.CopyToDataTable(), "ALL")
            End If
        End If
    End Sub

    Private Function GetCorrelationValue() As Decimal
        Try
            Dim dr() As DataRow
            dr = dtCorrelationMethods.Select("[KPICorrTypeID]=" & CType(cmbCorrType.SelectedItem, IOS.Library.clsComboBoxItem).Value)
            If dr.Length > 0 Then
                Select Case tbcCorrelationFilter.Value
                    Case 0
                        Return dr(0).Item("WeakCorrelation")
                    Case 1
                        Return dr(0).Item("MediumCorrelation")
                    Case 2
                        Return dr(0).Item("StrongCorrelation")
                End Select
            End If
        Catch ex As Exception
        End Try
        Return 0
    End Function

#End Region

End Class