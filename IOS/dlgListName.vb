Public Class dlgListName

#Region "Variables/Properties"

    Private _isPublic As Boolean
    Public Property IsPublic() As Boolean
        Get
            Return _isPublic
        End Get
        Set(ByVal value As Boolean)
            _isPublic = value
        End Set
    End Property

#End Region

#Region "Private Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub InsertNewAlert()
        Try
            Dim dtRow() As DataRow = Nothing
            If dtCellList IsNot Nothing Then
                dtRow = dtCellList.Select("ListName = '" & txtListName.Text.Trim & "'")
            End If
            If (dtRow.Length > 0) Then
                SetMessage("Fail : List Name already exists.")
                txtListName.Focus()
            Else
                Dim parray()() As String = {
                    New String() {"@ListName", Chr(39) & txtListName.Text.Trim & Chr(39)},
                    New String() {"@ListDescription", Chr(39) & txtDescription.Text & Chr(39)},
                    New String() {"@ListOwner", Chr(39) & Environment.UserName & Chr(39)},
                    New String() {"@IsPublic", IIf(IsPublic = True, 1, 0)}
                }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4515, parray)(0), GetSQL(4515, parray)(1))
                SetMessage("List Name Inserted Successfully")
            End If
        Catch ex As Exception
            SetMessage("Error : List Name Insertion Fail")
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
            If txtListName.Text = "" Then
                SetMessage("Please Enter List Name")
            Else
                InsertNewAlert()
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class
