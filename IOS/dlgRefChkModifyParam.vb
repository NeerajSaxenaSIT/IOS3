Public Class dlgRefChkModifyParam

#Region "Variables"

    Public templateID As String = Nothing
    Public templateName As String = Nothing
    Public vendor As String = Nothing
    Public moName As String = Nothing
    Public paramName As String = Nothing
    Public moDatabaseName As String = Nothing
    Public moTableName As String = Nothing
    Public templateMOParamConfigID As Integer = Nothing
    Public templateMOConfigID As Integer = Nothing

#End Region

#Region "Methods"

    Private Sub LoadParamDistinctValues()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@DatabaseName", "'" & moDatabaseName & "'"},
            New String() {"@TableName", "'" + moTableName + "'"},
            New String() {"@ColumnName", "'" + paramName + "'"}
        }
        strConnection = GetSQL(4118, parray)(0)
        sqlParam = GetSQL(4118, parray)(1)

        Dim dtColumnValue As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcDistinctValues, gvDistinctValues, dtColumnValue, "ALL",, paramName)
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
                'If IsNumeric(dropValue) Then
                '    txtParamValues.Text = "(" & dropValue.ToString & ")"
                'ElseIf IsString(dropValue) Then
                txtParamValues.Text = "('" & dropValue.ToString & "')"
                'End If
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

                        'If IsNumeric(str) Then
                        '    txtParamValues.Text = txtParamValues.Text & str & ","
                        'ElseIf IsString(str) Then
                        txtParamValues.Text = txtParamValues.Text & "'" & str & "',"
                        'End If

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

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub LoadCopyFromObjectsForSelectedMO(ByRef txt As DevExpress.XtraEditors.TextEdit, ByVal moTable As String)
        RemoveHandler txtCopyFromObject.TextChanged, AddressOf txtCopyFromObject_TextChanged
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@moTable", Chr(39) & moTable & Chr(39)}
        }
        strConnection = GetSQL(4160, parray)(0)
        sqlParam = GetSQL(4160, parray)(1)
        GetTextboxDataWithAutoCompleteFeature(txt, sqlParam)
        AddHandler txtCopyFromObject.TextChanged, AddressOf txtCopyFromObject_TextChanged
    End Sub

    Private Sub GetPrimaryKeyColumnsForSelectedMO(ByVal moTable As String)
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@moTable", Chr(39) & moTable & Chr(39)}
            }
            strConnection = GetSQL(4161, parray)(0)
            sqlParam = GetSQL(4161, parray)(1)
            lblObjIdentifier.Text = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut).Rows(0)(0).ToString
        Catch
        End Try
    End Sub

    Public Sub LoadMatchVariableCombo()
        RemoveHandler cmbMatchVariable.SelectedIndexChanged, AddressOf cmbMatchVariable_SelectedIndexChanged
        Dim strConnection As String
        Dim sqlParam As String
        Dim parray()() As String = {
            New String() {"@TemplateMOConfigID", Me.templateMOConfigID}
        }
        strConnection = GetSQL(4105, parray)(0)
        sqlParam = GetSQL(4105, parray)(1)
        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbMatchVariable, dt, "ColumnName", "ColumnName", "Select Column")
        AddHandler cmbMatchVariable.SelectedIndexChanged, AddressOf cmbMatchVariable_SelectedIndexChanged
    End Sub

#End Region

#Region "Events"

    Private Sub btnModifyParam_Click(sender As Object, e As EventArgs) Handles btnModifyParam.Click
        Try
            Dim paramValues As String = Nothing
            If cmbOperator.Text.Trim = "=" Or cmbOperator.Text.Trim = "<>" Or cmbOperator.Text.Trim = ">" Or cmbOperator.Text.Trim = ">=" Or cmbOperator.Text.Trim = "<" Or cmbOperator.Text.Trim = "<=" Or cmbOperator.Text.Trim.ToUpper = "LIKE" Then
                paramValues = txtParamValues.Text.Trim.Replace("'", "")
            ElseIf cmbOperator.Text.Trim.ToUpper = "IN" Then
                If (Not txtParamValues.Text.Contains("'")) Then
                    Dim paramParts() As String = txtParamValues.Text.TrimStart("(").TrimEnd(")").Split(",")
                    For i As Integer = 0 To paramParts.Length - 1
                        paramValues &= "''" & paramParts(i) & "''" & ","
                    Next
                    paramValues = paramValues.TrimEnd(",")
                    paramValues = "(" & paramValues & ")"
                Else
                    paramValues = txtParamValues.Text.Trim.Replace("'", "''")
                End If
            Else
                paramValues = txtParamValues.Text.Trim()
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOParamConfigID", templateMOParamConfigID},
                New String() {"@TemplateMOConfigID", templateMOConfigID},
                New String() {"@IsAutoSetValue", Chr(39) & IIf(ceSetAutoValue.Checked, 1, 0) & Chr(39)},
                New String() {"@CommonalityValue", Chr(39) & txtCommonalityValue.Text.Trim & Chr(39)},
                New String() {"@Operator", Chr(39) & cmbOperator.Text.Trim & Chr(39)},
                New String() {"@Value", Chr(39) & paramValues & Chr(39)},
                New String() {"@IsActive", IIf(ceIsEnabled.Checked, 1, 0)},
                New String() {"@copyFromObject", IIf(txtCopyFromObject.Text = "", "NULL", Chr(39) & txtCopyFromObject.Text.Trim & Chr(39))},
                New String() {"@MatchVariable", IIf(cmbMatchVariable.SelectedIndex = 0, "NULL", Chr(39) & cmbMatchVariable.Text.Trim & Chr(39))}
            }
            strConnection = GetSQL(4127, parray)(0)
            sqlParam = GetSQL(4127, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            Me.Close()
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

    Private Sub dlgRefChkModifyParam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblTemplateName.Text = templateName
            lblVendor.Text = vendor
            lblMOName.Text = moName
            lblParamName.Text = paramName
            LoadParamDistinctValues()
            LoadCopyFromObjectsForSelectedMO(txtCopyFromObject, Me.moTableName)
            LoadCopyFromObjectsForSelectedMO(txtCopyFromObjectAll, Me.moTableName)
            GetPrimaryKeyColumnsForSelectedMO(Me.moTableName)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
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

    Private Sub btnModifyAllParam_Click(sender As Object, e As EventArgs) Handles btnModifyAllParam.Click
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@TemplateMOConfigID", templateMOConfigID},
                New String() {"@IsAutoSetValue", Chr(39) & IIf(ceSetAutoValueAllParam.Checked, 1, 0) & Chr(39)},
                New String() {"@CommonalityValue", Chr(39) & txtCommonalityValueAllParam.Text.Trim & Chr(39)},
                New String() {"@IsActive", IIf(ceIsEnabledAllParam.Checked, 1, 0)},
                New String() {"@copyFromObject", IIf(txtCopyFromObjectAll.Text = "", "NULL", Chr(39) & txtCopyFromObjectAll.Text.Trim & Chr(39))}
            }
            strConnection = GetSQL(4128, parray)(0)
            sqlParam = GetSQL(4128, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

            Me.Close()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
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
            Dim dropValue As Object = Nothing
            dropValue = IIf(e.Data.GetData("System.String") Is Nothing, e.Data.GetData("System.Int32"), e.Data.GetData("System.String"))

            If cmbOperator.SelectedIndex < 0 Then
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

    Private Sub ceSetAutoValueAllParam_CheckedChanged(sender As Object, e As EventArgs) Handles ceSetAutoValueAllParam.CheckedChanged
        If ceSetAutoValueAllParam.Checked = True Then
            grpOption1.Enabled = False
            ceIsEnabledAllParam.Checked = True
            txtCommonalityValueAllParam.Text = "20"
        Else
            grpOption1.Enabled = True
            ceIsEnabledAllParam.Checked = False
            txtCommonalityValueAllParam.Text = ""
        End If
    End Sub

    Private Sub txtCopyFromObject_TextChanged(sender As Object, e As EventArgs)
        If txtCopyFromObject.Text = "" Then
            grpAutoSetValue.Enabled = True
            grpManualSetValue.Enabled = True
        Else
            grpAutoSetValue.Enabled = False
            grpManualSetValue.Enabled = False
        End If
    End Sub

    Private Sub cmbMatchVariable_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbMatchVariable.SelectedIndex = 0 Then
                grpAutoSetValue.Enabled = True
                grpManualSetValue.Enabled = True
                grpCopyFromObject.Enabled = True
                grpOption2.Enabled = True
                lblIsEnabled.Enabled = True
                ceIsEnabled.Enabled = True
            Else
                grpAutoSetValue.Enabled = False
                grpManualSetValue.Enabled = False
                grpCopyFromObject.Enabled = False
                grpOption2.Enabled = False
                lblIsEnabled.Enabled = False
                ceIsEnabled.Enabled = False
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

#End Region

End Class