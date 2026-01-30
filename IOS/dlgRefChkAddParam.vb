Public Class dlgRefChkAddParam

#Region "Variables"

    Public templateID As String = Nothing
    Public templateName As String = Nothing
    Public vendor As String = Nothing
    Public moName As String = Nothing
    Public moDatabaseName As String = Nothing
    Public moTableName As String = Nothing
    Public templateMOParamConfigID As Integer = Nothing
    Public templateMOConfigID As Integer = Nothing

#End Region

#Region "Methods"

    Private Sub LoadParamDistinctValues(paramName As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
                                        New String() {"@DatabaseName", "'" & moDatabaseName & "'"},
                                        New String() {"@TableName", "'" & moTableName & "'"},
                                        New String() {"@ColumnName", "'" & paramName & "'"}
        }
        strConnection = GetSQL(4118, parray)(0)
        sqlParam = GetSQL(4118, parray)(1)

        Dim dtColumnValue As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcDistinctValues, gvDistinctValues, dtColumnValue, "ALL",, paramName)
    End Sub

    Private Sub LoadParamNotInMO()
        RemoveHandler cmbParam.SelectedIndexChanged, AddressOf cmbParam_SelectedIndexChanged
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
                                        New String() {"@templateMOConfigID", templateMOConfigID}
        }
        strConnection = GetSQL(4131, parray)(0)
        sqlParam = GetSQL(4131, parray)(1)

        Dim dtParam As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        'close dialog if no params are found in MO
        If dtParam.Rows.Count = 0 Then
            SetMessage("All parameters are in MO of the template")
            btnAddParam.Enabled = False
            Exit Sub
        End If

        btnAddParam.Enabled = True
        BindDevExComboBoxWithValueMember(cmbParam, dtParam, "ColumnName", "ColumnName", "Select Param", False)
        AddHandler cmbParam.SelectedIndexChanged, AddressOf cmbParam_SelectedIndexChanged
    End Sub

    Private Sub ManageParamValues(dropValue)

        If (cmbOperator.Text.Trim = "=") Or (cmbOperator.Text.Trim = "<>") Or (cmbOperator.Text.Trim.ToUpper = "LIKE") Then

            If (txtParamValues.Text.Trim = "") Then
                If IsNumeric(dropValue) Then
                    txtParamValues.Text = dropValue.ToString
                ElseIf IsString(dropValue) Then
                    txtParamValues.Text = "'" & dropValue.ToString & "'"
                End If
            End If

            If txtParamValues.Text.Trim.ToUpper = dropValue.ToString.ToUpper Then
                SetMessage("Param value already exists")
                Exit Sub
            Else
                If IsNumeric(dropValue) Then
                    txtParamValues.Text = dropValue.ToString
                ElseIf IsString(dropValue) Then
                    txtParamValues.Text = "'" & dropValue.ToString & "'"
                End If
            End If

        ElseIf (cmbOperator.Text.Trim = ">") Or (cmbOperator.Text.Trim = "<") Or (cmbOperator.Text.Trim = ">=") Or (cmbOperator.Text.Trim = "<=") Then

            If txtParamValues.Text.Trim = "" Then
                If IsNumeric(dropValue) Then
                    txtParamValues.Text = txtParamValues.Text & "," & dropValue.ToString
                ElseIf IsString(dropValue) Then
                    SetMessage("Only numeric value is allowed")
                    Exit Sub
                End If
            End If

            If txtParamValues.Text.Trim.ToUpper = dropValue.ToString.ToUpper Then
                SetMessage("Param value already exists")
                Exit Sub
            Else
                If IsNumeric(dropValue) Then
                    txtParamValues.Text = dropValue.ToString
                ElseIf IsString(dropValue) Then
                    SetMessage("Only numeric value is allowed")
                    Exit Sub
                End If
            End If

        ElseIf (cmbOperator.Text.Trim.ToUpper = "IN") Then

            If txtParamValues.Text.Trim = "()" Then
                If IsNumeric(dropValue) Then
                    txtParamValues.Text = "(" & dropValue.ToString & ")"
                ElseIf IsString(dropValue) Then
                    txtParamValues.Text = "('" & dropValue.ToString & "')"
                End If
            Else
                Dim tempData As String = txtParamValues.Text.TrimStart("(").TrimEnd(")").Replace("'", "")
                Dim list As List(Of String) = tempData.Split(",").ToList()
                If (list.Contains(dropValue)) Then
                    SetMessage("Param value already exists")
                    Exit Sub
                Else
                    list.Add(dropValue)
                    txtParamValues.Text = "("
                    For Each str As String In list

                        If IsNumeric(str) Then
                            txtParamValues.Text = txtParamValues.Text & str & ","
                        ElseIf IsString(str) Then
                            txtParamValues.Text = txtParamValues.Text & "'" & str & "',"
                        End If

                    Next
                    txtParamValues.Text = txtParamValues.Text.TrimEnd(",") & ")"
                End If
            End If
        ElseIf (cmbOperator.Text.Trim.ToUpper = "RANGE") Then

            If Not IsNumeric(dropValue) Then
                SetMessage("Param value should be numeric for range operator")
                Exit Sub
            End If

            'range operator statement already completed
            If txtParamValues.Text.ToUpper.Contains("BETWEEN") AndAlso txtParamValues.Text.ToUpper.Contains("AND") Then
                SetMessage("Only 2 numeric values are allowed for range operator")
                Exit Sub
            End If

            'range operator value already exists
            If txtParamValues.Text.Contains(dropValue) Then
                SetMessage("Param value already exists")
                Exit Sub
            End If

            If txtParamValues.Text.Trim.ToUpper = "BETWEEN" Then
                txtParamValues.Text = txtParamValues.Text & " " & CInt(dropValue)
            Else
                txtParamValues.Text = txtParamValues.Text & " AND " & dropValue
            End If
        End If

    End Sub

#End Region

#Region "Events"

    Private Sub btnAddParam_Click(sender As Object, e As EventArgs) Handles btnAddParam.Click
        Try
            Dim operatorValue As String = Nothing
            Dim paramValues As String = Nothing

            If grpManualSetValue.Enabled = False Then
                operatorValue = ""
                paramValues = ""
            Else
                operatorValue = cmbOperator.Text.Trim
                If (cmbOperator.Text.Trim = "=" Or cmbOperator.Text.Trim = "<>" Or cmbOperator.Text.Trim = ">" Or cmbOperator.Text.Trim = ">=" Or cmbOperator.Text.Trim = "<" Or
                   cmbOperator.Text.Trim = "<=" Or cmbOperator.Text.Trim.ToUpper = "LIKE") Then
                    paramValues = txtParamValues.Text.Trim.Replace("'", "")
                Else
                    paramValues = txtParamValues.Text.Trim()
                End If
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", templateMOConfigID},
                New String() {"@ParamName", cmbParam.Text.Trim},
                New String() {"@IsAutoSetValue", Chr(39) & IIf(ceSetAutoValue.Checked, 1, 0) & Chr(39)},
                New String() {"@CommonalityValue", Chr(39) & txtCommonalityValue.Text.Trim & Chr(39)},
                New String() {"@Operator", Chr(39) & operatorValue & Chr(39)},
                New String() {"@Value", Chr(39) & paramValues & Chr(39)},
                New String() {"@IsActive", IIf(ceIsEnabled.Checked, 1, 0)}
            }
            strConnection = GetSQL(4136, parray)(0)
            sqlParam = GetSQL(4136, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            frmRefCheck.SaveChangeLog(Me.templateID, Me.moName, Me.templateMOConfigID, "New MO Config Param Added: " & cmbParam.Text.Trim)
            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbParam_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If cmbParam.SelectedIndex > 0 Then
                LoadParamDistinctValues(cmbParam.SelectedItem.ToString)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ceSetAutoValue_CheckedChanged(sender As Object, e As EventArgs) Handles ceSetAutoValue.CheckedChanged
        If ceSetAutoValue.Checked = True Then
            grpManualSetValue.Enabled = False
            txtCommonalityValue.Text = "20"
            txtCommonalityValue.Enabled = True
        Else
            grpManualSetValue.Enabled = True
            txtCommonalityValue.Text = ""
            txtCommonalityValue.Enabled = False
        End If
    End Sub

    Private Sub dlgRefChkAddParam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblTemplateName.Text = templateName
            lblVendor.Text = vendor
            lblMOName.Text = moName
            LoadParamNotInMO()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
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

    Private Sub gcDistinctValues_MouseMove(sender As Object, e As MouseEventArgs) Handles gcDistinctValues.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim data As DataRow = gvDistinctValues.GetFocusedDataRow()
                If data IsNot Nothing Then
                    Dim obj As Object = data.Item(0)
                    gcDistinctValues.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtParamValues_DragDrop(sender As Object, e As DragEventArgs) Handles txtParamValues.DragDrop
        Try
            Dim dropValue As Object = e.Data.GetData("System.String")

            If cmbOperator.SelectedIndex = 0 Then
                SetMessage("Please select an operator first")
                Exit Sub
            End If

            If dropValue IsNot Nothing Then
                ManageParamValues(dropValue)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtParamValues_DragOver(sender As Object, e As DragEventArgs) Handles txtParamValues.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub cmbOperator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOperator.SelectedIndexChanged
        Try
            If cmbOperator.Text.Trim = "=" Or cmbOperator.Text.Trim = ">" Or cmbOperator.Text.Trim = "<" Or cmbOperator.Text.Trim = ">=" Or cmbOperator.Text.Trim = "<=" Or cmbOperator.Text.Trim.ToUpper = "LIKE" Then
                txtParamValues.Text = ""
            ElseIf cmbOperator.Text.Trim.ToUpper = "IN" Then
                txtParamValues.Text = "()"
            ElseIf cmbOperator.Text.Trim.ToUpper = "RANGE" Then
                txtParamValues.Text = "Between"
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class