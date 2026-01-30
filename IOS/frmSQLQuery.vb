Imports System.Data
Imports System.Data.SqlClient
Imports dotnetCHARTING.WinForms
Imports System.IO
Imports System.Globalization
Imports IOS.Library
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class frmSQLQuery

#Region "Variables"

    Dim p As Point = Point.Empty

#End Region

#Region "Helper Methods"

    Private Sub ConfigurQueryBuilderForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)
        End If
    End Sub

    Sub LoadTables()
        lstTables.SuspendLayout()
        lstTables.Items.Clear()
        Dim tables As List(Of String) = frmMapWindow.GetMapTable()
        If (tables IsNot Nothing) Then
            For Each tableName As String In tables
                lstTables.Items.Add(tableName)
            Next
        End If
        lstTables.ResumeLayout()
    End Sub

    Private Function IsValidated() As String
        Dim isValid As Boolean = True
        Dim message As String = String.Empty
        If (String.IsNullOrEmpty(txtFrom.Text)) Then
            message = "Please select any table"
        ElseIf (String.IsNullOrEmpty(txtSelect.Text)) Then
            message = "please select atleast one field"
        ElseIf (String.IsNullOrEmpty(txtDestinationTable.Text)) Then
            message = "Please input destination table name"
        End If
        Return message
    End Function

    Function GetDataFromTemplateLine(ByVal line As String) As String
        Dim splitArray() As String = line.Split("{".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        If (splitArray.Length > 1) Then
            Dim dataArray() As String = splitArray(1).Split("}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            If (dataArray.Length > 0) Then
                Return dataArray(0)
            End If
        End If
        Return String.Empty
    End Function

    Public Function GetSql() As String
        Dim sql As String = String.Empty
        If (String.IsNullOrEmpty(IsValidated())) Then
            sql = "Select " & txtSelect.Text & " from " & txtFrom.Text
            If Not (txtWhere.Text.Trim = "") Then
                sql += " where " & txtWhere.Text
            End If
            If Not (txtGroupBy.Text.Trim = "") Then
                sql += " group by " & txtGroupBy.Text
            End If
            If Not (txtOrderBy.Text.Trim = "") Then
                sql += " order by " & txtOrderBy.Text
            End If
        End If
        Return sql
    End Function

#End Region

#Region "Form & Control Events"

    Private Sub frmSQLQuery_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Me.BringToFront()
        Me.TopMost = True
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub frmSQLQuery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            LoadTables()
            ConfigurQueryBuilderForm(Me.Name)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vtxtFrom_DragDrop(sender As Object, e As DragEventArgs) Handles txtFrom.DragDrop
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim text As String = e.Data.GetData("System.String")
            If (lstTables.Items.Contains(text)) Then
                Dim IsNewTable As Boolean = True
                If String.IsNullOrEmpty(txtFrom.Text.Trim) Then
                    txtFrom.Text = text
                Else
                    Dim exitingTables() As String = txtFrom.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    If Not (exitingTables.Contains(text)) Then
                        txtFrom.Text += "," & text
                        txtWhere.Text = ""
                        For index As Integer = 1 To exitingTables.Length
                            If (index <= 2) Then
                                If (index = 1) Then
                                    txtWhere.Text += exitingTables(index - 1) & ".Obj" & " INTERSECTS "
                                ElseIf (index = 2) Then
                                    txtWhere.Text += exitingTables(index - 1) & ".Obj"
                                End If
                            End If
                        Next
                        If (exitingTables.Length = 1) Then
                            txtWhere.Text += text & ".Obj"
                        End If
                    Else
                        IsNewTable = False
                    End If
                End If
                If IsNewTable AndAlso Not String.IsNullOrEmpty(txtFrom.Text) Then
                    Dim tables() As String = txtFrom.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    lstFields.SuspendLayout()
                    lstFields.Items.Clear()
                    For Each Item As String In tables
                        Dim tableText As String = Item
                        Dim filteredTable = From w In MapInfo.Engine.Session.Current.Catalog.Cast(Of MapInfo.Data.Table)() _
                                            Where w.Alias = tableText _
                                            Select w
                        Dim table As MapInfo.Data.Table = filteredTable.FirstOrDefault()
                        If (table IsNot Nothing) Then
                            For Each column As MapInfo.Data.Column In table.TableInfo.Columns
                                lstFields.Items.Add(tableText & "." & column.Alias)
                            Next
                        End If
                    Next
                    lstFields.ResumeLayout()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vtxtFrom_DragOver(sender As Object, e As DragEventArgs) Handles txtFrom.DragOver, txtSelect.DragOver, txtWhere.DragOver, txtOrderBy.DragOver, txtGroupBy.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub vtxtFrom_TextChanged(sender As Object, e As EventArgs) Handles txtFrom.TextChanged
        If (txtFrom.Text.Length = 0 AndAlso String.IsNullOrEmpty(txtFrom.Text)) Then
            txtSelect.Text = ""
            txtFrom.Text = ""
            txtGroupBy.Text = ""
            txtOrderBy.Text = ""
            txtWhere.Text = ""
            lstFields.Items.Clear()
        End If
    End Sub

    Private Sub vtxtSelect_DragDrop(sender As Object, e As DragEventArgs) Handles txtSelect.DragDrop
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim text As String = e.Data.GetData("System.String")
            Dim isFields As Boolean = lstFields.Items.Contains(text)
            Dim isOperatore As Boolean = lstOperators.Items.Contains(text)
            Dim isAggregateFunction As Boolean = lstAggregateFunction.Items.Contains(text)
            If isFields Or isOperatore Or isAggregateFunction Then
                If String.IsNullOrEmpty(txtSelect.Text.Trim) Then
                    txtSelect.Text = text
                Else
                    If (isFields) Then
                        Dim exitingFields() As String = txtSelect.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        If Not (exitingFields.Contains(text)) Then
                            If (txtSelect.Text.EndsWith("()")) Then
                                txtSelect.Text = txtSelect.Text.Insert(txtSelect.Text.Length - 1, text)
                            Else
                                txtSelect.Text += "," & text
                            End If
                        End If
                    Else
                        txtSelect.Text += "," & text
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vtxtWhere_DragDrop(sender As Object, e As DragEventArgs) Handles txtWhere.DragDrop
        Dim text As String = e.Data.GetData("System.String")
        Dim isFields As Boolean = lstFields.Items.Contains(text)
        Dim isOperatore As Boolean = lstOperators.Items.Contains(text)
        Dim isAggregateFunction As Boolean = lstAggregateFunction.Items.Contains(text)
        If (isFields Or isOperatore) AndAlso (Not isAggregateFunction) Then
            If String.IsNullOrEmpty(txtWhere.Text) Then
                txtWhere.Text = text
            Else
                txtWhere.Text += " " & text
            End If
        End If
    End Sub

    Private Sub vtxtGroupBy_DragDrop(sender As Object, e As DragEventArgs) Handles txtGroupBy.DragDrop
        Dim text As String = e.Data.GetData("System.String")
        If (lstFields.Items.Contains(text)) Then
            If String.IsNullOrEmpty(txtGroupBy.Text.Trim) Then
                txtGroupBy.Text = text
            Else
                Dim exitingFields() As String = txtGroupBy.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                If Not (exitingFields.Contains(text)) Then
                    txtGroupBy.Text += "," & text
                End If
            End If
        End If
    End Sub

    Private Sub vtxtOrderBy_DragDrop(sender As Object, e As DragEventArgs) Handles txtOrderBy.DragDrop
        Dim text As String = e.Data.GetData("System.String")
        If (lstFields.Items.Contains(text)) Then
            If String.IsNullOrEmpty(txtOrderBy.Text.Trim) Then
                txtOrderBy.Text = text & " ASC"
            Else
                Dim exitingFields() As String = txtOrderBy.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                If Not (exitingFields.Contains(text & " ASC")) Then
                    txtOrderBy.Text += "," & text & " ASC"
                End If
            End If
        End If
    End Sub

    Private Sub vlstTables_MouseDown(sender As Object, e As MouseEventArgs) Handles lstTables.MouseDown, lstOperators.MouseDown, lstFields.MouseDown, lstAggregateFunction.MouseDown
        Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
        p = New Point(e.X, e.Y)
        Dim selectedIndex As Integer = listControl.IndexFromPoint(p)
        If selectedIndex = -1 Then
            p = Point.Empty
        End If
    End Sub

    Private Sub vbtnSaveTemplate_Click(sender As Object, e As EventArgs) Handles btnSaveTemplate.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim message As String = IsValidated()
            If (String.IsNullOrEmpty(message)) Then
                Dim fd As New SaveFileDialog
                fd.InitialDirectory = GetUserDataPath() & "\Data\"
                fd.DefaultExt = "QRY"
                fd.Filter = "IOS template|*.QRY"
                fd.Title = "Save the template"
                fd.ShowDialog()
                If fd.FileName <> "" Then
                    File.Create(fd.FileName).Close()
                    Dim TextFile As New StreamWriter(fd.FileName)
                    TextFile.WriteLine("Tables {" & txtFrom.Text & "}")
                    TextFile.WriteLine("Fields {" & txtSelect.Text & "}")
                    TextFile.WriteLine("Where {" & txtWhere.Text & "}")
                    TextFile.WriteLine("Group {" & txtGroupBy.Text & "}")
                    TextFile.WriteLine("Order {" & txtOrderBy.Text & "}")
                    TextFile.WriteLine("Into {" & txtDestinationTable.Text & "}")
                    TextFile.WriteLine("Browse")
                    TextFile.Close()
                    XtraMessageBox.Show("Template has been saved")
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vbtnLoadTemplate_Click(sender As Object, e As EventArgs) Handles btnLoadTemplate.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim fd As New OpenFileDialog
            fd.InitialDirectory = GetUserDataPath() & "\Data\"
            fd.DefaultExt = "QRY"
            fd.Filter = "IOS template|*.QRY"
            fd.Title = "Open template"
            fd.ShowDialog()

            If fd.FileName <> "" Then
                Dim lines() As String = File.ReadAllLines(fd.FileName)
                For Each line As String In lines
                    If (line.StartsWith("Tables")) Then
                        txtFrom.Text = GetDataFromTemplateLine(line)
                    End If
                    If (line.StartsWith("Fields")) Then
                        txtSelect.Text = GetDataFromTemplateLine(line)
                    End If
                    If (line.StartsWith("Where")) Then
                        txtWhere.Text = GetDataFromTemplateLine(line)
                    End If
                    If (line.StartsWith("Group")) Then
                        txtGroupBy.Text = GetDataFromTemplateLine(line)
                    End If
                    If (line.StartsWith("Order")) Then
                        txtOrderBy.Text = GetDataFromTemplateLine(line)
                    End If
                    If (line.StartsWith("Into")) Then
                        txtDestinationTable.Text = GetDataFromTemplateLine(line)
                    End If
                Next
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vbtnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim message As String = IsValidated()
            If (String.IsNullOrEmpty(message)) Then
                Dim sql As String = GetSql()
                Dim result As MapInfo.Data.IResultSetFeatureCollection = frmMapWindow.objMapHelper.ExecuteMapInfoQuery(sql, txtDestinationTable.Text.Trim)
                XtraMessageBox.Show("Tested Successfully")
                result.Close()
            Else
                Me.Cursor = Cursors.Default
                XtraMessageBox.Show(message)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("There is some problem with query. Error: " & ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub vbtnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim message As String = IsValidated()
            If (String.IsNullOrEmpty(message)) Then
                Dim sql As String = GetSql()
                frmMapWindow.objMapHelper.CloseTable(txtDestinationTable.Text.Trim())
                Dim result As MapInfo.Data.IResultSetFeatureCollection = frmMapWindow.objMapHelper.ExecuteMapInfoQuery(sql, txtDestinationTable.Text.Trim)
                Dim fLayer As New MapInfo.Mapping.FeatureLayer(result.Table, result.Alias)
                frmMapWindow.objMapHelper.MapControl.Map.Layers.Add(fLayer)
                XtraMessageBox.Show("Query executed successfully")
                'result.Close()
            Else
                Me.Cursor = Cursors.Default
                XtraMessageBox.Show(message)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("There is some problem with query. Error: " & ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub vbtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtSelect.Text = ""
        txtFrom.Text = ""
        txtGroupBy.Text = ""
        txtOrderBy.Text = ""
        txtWhere.Text = ""
    End Sub

    Private Sub vlstTables_MouseMove(sender As Object, e As MouseEventArgs) Handles lstTables.MouseMove, lstFields.MouseMove, lstOperators.MouseMove, lstAggregateFunction.MouseMove
        If e.Button = MouseButtons.Left Then
            If (p <> Point.Empty) Then
                Dim listControl As DevExpress.XtraEditors.ListBoxControl = TryCast(sender, DevExpress.XtraEditors.ListBoxControl)
                If (listControl IsNot Nothing) Then
                    Dim index As Integer = listControl.IndexFromPoint(p)
                    If (index > -1) Then
                        listControl.DoDragDrop(listControl.Items(index).ToString, DragDropEffects.Copy)
                    End If
                End If
            End If
        End If
    End Sub

#End Region

End Class