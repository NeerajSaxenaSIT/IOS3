Imports DevExpress.CodeParser
Imports DevExpress.XtraEditors
Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgUpdateReportObjects

    Private dtPredefinePeriod As DataTable = Nothing

    Public selectedNodeName As String = Nothing
    Public selectedNodeLevel As Integer = Nothing
    Public reportID As Integer = Nothing
    Public slideID As Integer = Nothing
    Public objectID As Integer = Nothing

#Region "Events"

    Private Sub dlgUpdateReportObjects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadFormObjects()
            BindComboWithPredefinedPeriod(cmbPredefTimeStats)
            LoadResolutionCombo()
            LoadReportObjects()
            PresetDatetimeEditors()
            AddHandler cmbResolution.SelectedIndexChanged, AddressOf cmbResolution_SelectedIndexChanged
            AddHandler cmbPredefTimeStats.SelectedIndexChanged, AddressOf cmbPredefTimeStats_SelectedIndexChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbResolution_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim cmb As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
            If cmb.SelectedItem.ToString = "Daily" Or cmb.SelectedItem.ToString = "Daily BH" Or cmb.SelectedItem.ToString = "Daily BH2" Or cmb.SelectedItem.ToString = "Weekly" Or cmb.SelectedItem.ToString = "Monthly" Then
                dtEditStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False
                dtEditEndTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False

                dtEditStartTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
                dtEditStartTime.Properties.EditFormat.FormatString = "dd/MM/yyyy"
                dtEditStartTime.Properties.EditMask = "dd/MM/yyyy"

                dtEditEndTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
                dtEditEndTime.Properties.EditFormat.FormatString = "dd/MM/yyyy"
                dtEditEndTime.Properties.EditMask = "dd/MM/yyyy"
            ElseIf cmb.SelectedItem.ToString = "Raw" Or cmb.SelectedItem.ToString = "Hourly" Then
                ConfigureDatePickerTimeEditing(dtEditStartTime)

                dtEditStartTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
                dtEditStartTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
                dtEditStartTime.Properties.EditMask = "dd/MM/yyyy HH:mm"

                ConfigureDatePickerTimeEditing(dtEditEndTime)

                dtEditEndTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
                dtEditEndTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
                dtEditEndTime.Properties.EditMask = "dd/MM/yyyy HH:mm"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbPredefTimeStats_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cmb As ComboBoxEdit = CType(sender, ComboBoxEdit)
        If cmb.SelectedIndex > 0 Then
            dtEditStartTime.Enabled = False
            dtEditEndTime.Enabled = False
            Dim dr() As DataRow = dtPredefinePeriod.AsEnumerable().Where(Function(x) x.Field(Of Integer)("PredefinedPeriodID") = TryCast(cmb.SelectedItem, clsComboBoxItem).Value AndAlso x.Field(Of String)("Control") = cmb.Name).ToArray()
            If Not dr Is Nothing Then
                If dr.Count > 0 Then
                    Dim SQL As String = dr(0)("SQL").ToString
                    Dim dtPeriod As DataTable = DataAccessorODBC.GetDataTable(connStrIOSServer, SQL)
                    If dtPeriod IsNot Nothing AndAlso dtPeriod.Rows.Count > 0 Then
                        If cmb.Name.Contains("Stats") Then
                            dtEditStartTime.EditValue = dtPeriod.Rows(0)(0)
                            dtEditEndTime.EditValue = dtPeriod.Rows(0)(1)
                        End If
                    End If
                End If
            End If
        Else
            dtEditStartTime.Enabled = True
            dtEditEndTime.Enabled = True
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            If gvReportObjects.RowCount > 0 Then
                Dim tech As String = gvReportObjects.GetFocusedRowCellValue("Technology").ToString
                If tech.Contains("TopX_") Then tech = tech.Replace("TopX_", "")
                Dim targeType As String = gvReportObjects.GetFocusedRowCellValue("TargetType").ToString
                If dicFrmTechInstances.Count = 0 Then
                    SetMessage("Please open PM " & ChrW(&H2192) & " " & tech)
                    Exit Sub
                ElseIf dicFrmTechInstances.Values.OfType(Of frmTechnology).Count(Function(n) n.InstanceKey.Split(";")(1) = tech.Replace(" ", "")) = 0 Then
                    SetMessage("Please open PM -> " & ChrW(&H2192) & " " & tech)
                    Exit Sub
                End If
                'Update Report Setting From PM Tech Instance

            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim sql As String = Nothing
            Dim connstring As String = Nothing

            'update report period and interval
            If chkPeriodResolution.Checked Then
                Dim parray()() As String = {
                    New String() {"@ReportID", reportID},
                    New String() {"@SlideID", IIf(slideID = Nothing, "NULL", slideID)},
                    New String() {"@ObjectID", IIf(objectID = Nothing, "NULL", objectID)},
                    New String() {"@Interval", Chr(39) & cmbResolution.SelectedItem.ToString.Trim & Chr(39)},
                    New String() {"@PredefinedTime", Chr(39) & cmbPredefTimeStats.SelectedItem.ToString.Trim & Chr(39)},
                    New String() {"@ManualStartTime", Chr(39) & dtEditStartTime.EditValue & Chr(39)},
                    New String() {"@ManualEndTime", Chr(39) & dtEditEndTime.EditValue & Chr(39)}
                }
                sql = GetSQL(8552, parray)(1)
                connstring = GetSQL(8552, parray)(0)
                DataAccessorODBC.ExecuteNonQuery(connstring, sql)
            End If

            sql = Nothing
            connstring = Nothing

            'update report target type and objects
            If chkObjects.Checked Then
                If gvReportObjects.RowCount > 0 Then
                    For iCntr = 0 To gvReportObjects.RowCount - 1
                        Dim selectedObjects = gvReportObjects.GetRowCellValue(iCntr, "ObjectsSelected").Replace("','", "'',''")
                        Dim parray()() As String = {
                            New String() {"@ReportID", reportID},
                            New String() {"@SlideID", IIf(slideID = Nothing, "NULL", slideID)},
                            New String() {"@ObjectID", IIf(objectID = Nothing, "NULL", objectID)},
                            New String() {"@TargetType", Chr(39) & gvReportObjects.GetRowCellValue(iCntr, "TargetType") & Chr(39)},
                            New String() {"@ObjectSelected", Chr(39) & selectedObjects & Chr(39)}
                        }
                        sql = GetSQL(8553, parray)(1)
                        connstring = GetSQL(8553, parray)(0)
                        DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                    Next
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub PresetDatetimeEditors()
        dtEditStartTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        dtEditStartTime.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        dtEditStartTime.Properties.EditMask = "dd/MM/yyyy"

        dtEditEndTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        dtEditEndTime.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        dtEditEndTime.Properties.EditMask = "dd/MM/yyyy"
    End Sub

    Private Sub LoadReportObjects()
        Dim dt As DataTable = clsSQLCommands.GetReportObjects(connStrIOSServer, reportID)
        IOSDevExpressGrid.PopulateDataInGrid(gcReportObjects, gvReportObjects, dt, "ALL", {"Resolution"}, "ObjectsSelected")
    End Sub

    Public Sub BindComboWithPredefinedPeriod(ByRef cmb As ComboBoxEdit)
        Try
            dtPredefinePeriod = clsSQLCommands.GetPredefinedPeriodComboBox(connStrIOSServer)
            If dtPredefinePeriod IsNot Nothing Then
                Dim cmbName As String = cmb.Name
                If dtPredefinePeriod.AsEnumerable().Where(Function(x) x.Field(Of String)("Control") = cmbName).Count > 0 Then
                    cmb.Enabled = True
                    BindDevExComboBoxWithValueMember(cmb, dtPredefinePeriod.AsEnumerable().Where(Function(x) x.Field(Of String)("Control") = cmbName).CopyToDataTable(), "PredefinedPeriodID", "GUIText", "Select", True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadFormObjects()
        If selectedNodeLevel = 0 Then
            lblSelectedReportItem.Text = "Report: " & selectedNodeName
        ElseIf selectedNodeLevel = 1 Then
            lblSelectedReportItem.Text = "Slide: " & selectedNodeName
        ElseIf selectedNodeLevel = 2 Then
            lblSelectedReportItem.Text = "Object: " & IIf(selectedNodeName.Contains("Param"), selectedNodeName.Split("Param")(0), selectedNodeName)
        End If
    End Sub

    Private Sub LoadResolutionCombo()
        cmbResolution.Properties.Items.AddRange({"Select", "Raw", "Hourly", "Daily", "Daily BH", "Daily BH2", "Weekly", "Monthly"})
        cmbResolution.SelectedIndex = 0
    End Sub

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