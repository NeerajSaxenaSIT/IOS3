Imports IOS.DataLibrary
Imports IOS.Configuration
Imports DevExpress.XtraEditors
Imports IOS.Library
Imports System.Text
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.Controls

Public Class frmCopyParam2Template

#Region "Variables"

    Public MOName As String = Nothing
    Public CopyType As String = Nothing
    Public vendor As String = Nothing
    Public templateIDCopyFrom As Integer = Nothing
    Public templateMOConfigID As Integer = Nothing

    Private iCopyToCntr As Integer = Nothing

#End Region

#Region "Events"

    Private Sub frmCopyParam2Template_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If CopyType.ToLower = "param2mo" Then
                chkSelectAllParams.Text = "Select All Params"
                LoadAllParamsForMOName()
                LoadAllTemplatesToCopyParam()
            ElseIf CopyType.ToLower = "mo2template" Then
                chkSelectAllParams.Text = "Select All MOs"
                LoadAllMOs()
                LoadAllTemplatesToCopyMO()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub chkSelectAllParams_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelectAllParams.CheckedChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            For iCntr As Integer = 0 To gvMOParam.RowCount - 1
                If chkSelectAllParams.Checked Then
                    gvMOParam.SetRowCellValue(iCntr, "Select", True)
                Else
                    gvMOParam.SetRowCellValue(iCntr, "Select", False)
                End If
            Next

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub chkSelectAllTemplates_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelectAllTemplates.CheckedChanged
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            For iCntr As Integer = 0 To gvTemplateList.RowCount - 1
                If chkSelectAllTemplates.Checked Then
                    gvTemplateList.SetRowCellValue(iCntr, "Select", True)
                Else
                    gvTemplateList.SetRowCellValue(iCntr, "Select", False)
                End If
            Next

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim ParamOrMOSelected As Boolean = False
            Dim TemplateSelected As Boolean = False

            For iCntr As Integer = 0 To gvMOParam.RowCount - 1
                If gvMOParam.GetRowCellValue(iCntr, "Select") = True Then
                    ParamOrMOSelected = True
                End If
            Next

            For iCntr As Integer = 0 To gvTemplateList.RowCount - 1
                If gvTemplateList.GetRowCellValue(iCntr, "Select") = True Then
                    TemplateSelected = True
                End If
            Next

            If ParamOrMOSelected = False Or TemplateSelected = False Then
                If CopyType.ToLower = "param2mo" Then
                    SetMessage("Please Select Param(s) And Template(s) To Copy")
                ElseIf CopyType.ToLower = "mo2template" Then
                    SetMessage("Please Select MO(s) And Template(s) To Copy")
                End If
                Exit Sub
            End If

            iCopyToCntr = 0

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If CopyType.ToLower = "param2mo" Then

                Dim paramConfigID As Integer = 0
                Dim paramName As String = Nothing
                Dim moConfigID As Integer = 0

                For iCntr As Integer = 0 To gvMOParam.RowCount - 1
                    If gvMOParam.GetRowCellValue(iCntr, "Select") = True Then
                        paramConfigID = CInt(gvMOParam.GetRowCellValue(iCntr, "TemplateMOParamConfigID"))
                        paramName = CStr(gvMOParam.GetRowCellValue(iCntr, "ParamName"))
                        For jCntr As Integer = 0 To gvTemplateList.RowCount - 1
                            If gvTemplateList.GetRowCellValue(jCntr, "Select") = True Then
                                moConfigID = CInt(gvTemplateList.GetRowCellValue(jCntr, "TemplateMOConfigID"))
                                CopyParamToTemplateMO(paramConfigID, moConfigID, paramName)
                                iCopyToCntr = iCopyToCntr + 1
                            End If
                        Next
                    End If
                Next

            ElseIf CopyType.ToLower = "mo2template" Then

                Dim moConfigID As Integer = 0
                Dim templateID As Integer = 0

                For iCntr As Integer = 0 To gvMOParam.RowCount - 1
                    If gvMOParam.GetRowCellValue(iCntr, "Select") = True Then
                        moConfigID = CInt(gvMOParam.GetRowCellValue(iCntr, "TemplateMOConfigID"))
                        For jCntr As Integer = 0 To gvTemplateList.RowCount - 1
                            If gvTemplateList.GetRowCellValue(jCntr, "Select") = True Then
                                templateID = CInt(gvTemplateList.GetRowCellValue(jCntr, "TemplateID"))
                                CopyMOToTemplate(moConfigID, templateID)
                                iCopyToCntr = iCopyToCntr + 1
                            End If
                        Next
                    End If
                Next

            End If

            SetMessage("Total Number of Rows Updated: " & iCopyToCntr.ToString)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
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

#End Region

#Region "Methods"

    Private Sub LoadAllParamsForMOName()
        Dim dtParam As DataTable = GetParamListForSelectedMO()

        If dtParam IsNot Nothing Then
            If dtParam.Rows.Count > 0 Then
                dtParam.Columns.Add("Select", Type.GetType("System.Boolean"))
                For Each dr As DataRow In dtParam.Rows
                    dr("Select") = "False"
                Next
                dtParam.AcceptChanges()

                Dim columnsToHide() As String = {"TemplateMOParamConfigID", "TemplateMOConfigID", "IsAutoSetValue", "CommonalityValue", "Operator", "Value", "IsActive", "IsConditionActive", "CopyFromObject", "IsVariable", "InNBI", "ReadOnly"}
                IOSDevExpressGrid.PopulateDataInGrid(gcMOParam, gvMOParam, dtParam, "ALL", columnsToHide, "ParamName")
                gvMOParam.Columns("Select").VisibleIndex = 0

                Dim riChkSelect As RepositoryItemCheckEdit = TryCast(gcMOParam.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                riChkSelect.CheckStyle = CheckStyles.Standard
                riChkSelect.AllowGrayed = False
                riChkSelect.NullStyle = StyleIndeterminate.Unchecked
                gvMOParam.Columns("Select").ColumnEdit = riChkSelect

                gvMOParam.Columns("ParamName").OptionsColumn.ReadOnly = True
                gvMOParam.Columns("ParamName").OptionsColumn.AllowEdit = False
            End If
        End If
    End Sub

    Private Sub LoadAllTemplatesToCopyParam()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateIDCopyFrom},
            New String() {"@MOName", Chr(39) & MOName & Chr(39)}
        }

        strConnection = GetSQL(4203, parray)(0)
        sqlParam = GetSQL(4203, parray)(1)

        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(gcTemplateList, gvTemplateList, dt, "ALL", {"TemplateID", "TemplateMOConfigID"})

                Dim riChkSelect As RepositoryItemCheckEdit = TryCast(gcTemplateList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                riChkSelect.CheckStyle = CheckStyles.Standard
                riChkSelect.AllowGrayed = False
                riChkSelect.NullStyle = StyleIndeterminate.Unchecked
                gvTemplateList.Columns("Select").ColumnEdit = riChkSelect

                gvTemplateList.Columns("TemplateName").OptionsColumn.ReadOnly = True
                gvTemplateList.Columns("TemplateName").OptionsColumn.AllowEdit = False

                gvTemplateList.Columns("MOName").OptionsColumn.ReadOnly = True
                gvTemplateList.Columns("MOName").OptionsColumn.AllowEdit = False

                gvTemplateList.Columns("InfoField").OptionsColumn.ReadOnly = True
                gvTemplateList.Columns("InfoField").OptionsColumn.AllowEdit = False
            End If
        End If
    End Sub

    Private Function GetParamListForSelectedMO() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", templateMOConfigID}
        }

        strConnection = GetSQL(4106, parray)(0)
        sqlParam = GetSQL(4106, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub CopyParamToTemplateMO(paramConfigID As Integer, moConfigID As Integer, paramName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOParamConfigID", paramConfigID},
            New String() {"@TemplateMOConfigID", moConfigID},
            New String() {"@ParamName", Chr(39) & paramName & Chr(39)}
        }

        strConnection = GetSQL(4204, parray)(0)
        sqlParam = GetSQL(4204, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadAllMOs()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", templateIDCopyFrom}
        }
        strConnection = GetSQL(4110, parray)(0)
        sqlParam = GetSQL(4110, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        dt.Columns.Add("Select", Type.GetType("System.Boolean"))
        For Each dr As DataRow In dt.Rows
            dr("Select") = "False"
        Next
        dt.AcceptChanges()

        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                Dim columnsToHide() As String = {"TemplateMOConfigID", "TemplateID", "IOS_Vendor", "MOTable", "MODatabase", "IsAllParameters", "IsAutoSetValue", "CommonalityValue", "IsActive", "CheckMissingNE", "CopyFromObject", "Priority", "InNBI"}
                IOSDevExpressGrid.PopulateDataInGrid(gcMOParam, gvMOParam, dt, "ALL", columnsToHide, "MOName")
                gvMOParam.Columns("Select").VisibleIndex = 0
            End If
        End If
    End Sub

    Private Sub LoadAllTemplatesToCopyMO()
        Dim dt As DataTable = GetTemplateList()

        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                'removing the source template from the right hand side list
                dt.Rows.Remove(dt.Select("TemplateID=" & templateIDCopyFrom)(0))

                dt.Columns.Add("Select", Type.GetType("System.Boolean"))
                For Each dr As DataRow In dt.Rows
                    dr("Select") = "False"
                Next
                dt.AcceptChanges()

                Dim columnsToHide() As String = {"TemplateID", "TemplateVendor", "TemplateDescription", "Owner", "IsLocked", "IsScheduled", "IsEnabled", "LatestConfigUpdate", "LastRunTime", "LastCMDate", "LastStatus"}
                IOSDevExpressGrid.PopulateDataInGrid(gcTemplateList, gvTemplateList, dt, "ALL", columnsToHide, "TemplateName")
                gvTemplateList.Columns("Select").VisibleIndex = 0

                Dim riChkSelect As RepositoryItemCheckEdit = TryCast(gcTemplateList.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
                riChkSelect.CheckStyle = CheckStyles.Standard
                riChkSelect.AllowGrayed = False
                riChkSelect.NullStyle = StyleIndeterminate.Unchecked
                gvTemplateList.Columns("Select").ColumnEdit = riChkSelect

                gvTemplateList.Columns("TemplateName").OptionsColumn.ReadOnly = True
                gvTemplateList.Columns("TemplateName").OptionsColumn.AllowEdit = False
            End If
        End If
    End Sub

    Private Function GetTemplateList() As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateVendor", Chr(39) & vendor.Trim & Chr(39)}
        }
        strConnection = GetSQL(4101, parray)(0)
        sqlParam = GetSQL(4101, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub CopyMOToTemplate(moConfigID As Integer, templateID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@SrcTemplateID", templateIDCopyFrom},
            New String() {"@TemplateMOConfigID", moConfigID},
            New String() {"@TrgTemplateID", templateID}
        }

        strConnection = GetSQL(4205, parray)(0)
        sqlParam = GetSQL(4205, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
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