Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports IOS.DataLibrary
Imports IOS.Library

Public Class frmRefChkUpdateTemplateObj

#Region "Variables"

    Public objectName As String = Nothing
    Public templateID As Integer = Nothing
    Private iUpdateCntr As Integer = Nothing

#End Region

#Region "Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub CheckTemplateObjectNewParam()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateID", CInt(templateID)},
            New String() {"@ObjectName", Chr(39) & txtObjectName.Text.Trim & Chr(39)}
        }
        strConnection = GetSQL(4199, parray)(0)
        sqlParam = GetSQL(4199, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt IsNot Nothing Then
            IOSDevExpressGrid.PopulateDataInGrid(gcTemplateObject, gvTemplateObject, dt, "ALL", Nothing)
            btnUpdate.Enabled = True
        End If
    End Sub

    Private Sub UpdateTemplateObjectNewParam(templateMOConfigID As Integer, paramName As String, paramValue As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", templateMOConfigID},
            New String() {"@ParamName", Chr(39) & paramName & Chr(39)},
            New String() {"@ParamValue", Chr(39) & paramValue & Chr(39)}
        }
        strConnection = GetSQL(4200, parray)(0)
        sqlParam = GetSQL(4200, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

#End Region

#Region "Events"

    Private Sub frmRefChkUpdateMOParam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If objectName IsNot Nothing Then
                txtObjectName.Text = objectName
            End If
            btnUpdate.Enabled = False
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Try
            If txtObjectName.Text = String.Empty Then
                SetMessage("Please enter object name")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            CheckTemplateObjectNewParam()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If gvTemplateObject.RowCount = 0 Then
                SetMessage("There is no data to update")
                Exit Sub
            End If

            iUpdateCntr = 0

            If gvTemplateObject.ActiveFilterString <> "" Then
                Dim dtFiltered As DataTable = CType(gcTemplateObject.DataSource, DataTable).Select(gvTemplateObject.ActiveFilterString).CopyToDataTable
                If dtFiltered.Rows.Count > 0 Then
                    For Each dr As DataRow In dtFiltered.Rows
                        UpdateTemplateObjectNewParam(dr("MOConfigID").ToString, dr("ParameterName").ToString, dr("ParameterValue").ToString)
                        iUpdateCntr = iUpdateCntr + 1
                    Next
                End If
            Else
                For iCntr = 0 To gvTemplateObject.RowCount - 1
                    UpdateTemplateObjectNewParam(gvTemplateObject.GetRowCellValue(iCntr, "MOConfigID").ToString, gvTemplateObject.GetRowCellValue(iCntr, "ParameterName").ToString, gvTemplateObject.GetRowCellValue(iCntr, "ParameterValue").ToString)
                    iUpdateCntr = iUpdateCntr + 1
                Next
            End If

            SetMessage("Number of Rows Updated: " & iUpdateCntr.ToString)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvTemplateObject_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gvTemplateObject.ShowingEditor
        Try
            If gvTemplateObject.FocusedColumn.FieldName = "ParameterValue" Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch
        End Try
    End Sub

    Private Sub gcTemplateObject_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles gcTemplateObject.ProcessGridKey
        Try
            Dim grid = TryCast(sender, GridControl)
            Dim view = DirectCast(grid.FocusedView, GridView)
            If e.KeyData = Keys.Delete Then
                view.DeleteSelectedRows()
                e.Handled = True
            End If
        Catch
        End Try
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

End Class