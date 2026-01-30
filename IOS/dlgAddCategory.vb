Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgAddCategory

#Region "Private Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub InsertNewCategory()
        Try
            Dim dtRow() As DataRow = Nothing
            If dtCategory IsNot Nothing Then
                dtRow = dtCategory.Select("CapJobCategoryName = '" & txtCategoryName.Text.Trim & "'")
            End If
            If (dtRow IsNot Nothing) Then
                If dtRow.Length > 0 Then
                    SetMessage("Fail : Category Name already exists.")
                    txtCategoryName.Focus()
                End If
            Else
                newCapCategory = txtCategoryName.Text.Trim
                Dim parray()() As String = {
                                                New String() {"@CapJobCategoryName", Chr(39) & newCapCategory & Chr(39)}
                                           }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(3005, parray)(0), GetSQL(3005, parray)(1))
                SetMessage("Category Name Inserted Successfully")
                Me.Close()
            End If
        Catch ex As Exception
            SetMessage("Error : Category Insertion Fail")
        End Try
    End Sub

#End Region

#Region "Form Events"

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMessage.Text = ""
        lblMessage.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtCategoryName.Text = "" Then
                SetMessage("Please enter the Category Name")
            Else
                InsertNewCategory()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
     End Sub

    Private Sub vbtn_Cancel_Click(sender As Object, e As EventArgs) Handles vbtn_Cancel.Click
        newCapCategory = Nothing
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class