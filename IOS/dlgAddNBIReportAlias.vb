Public Class dlgAddNBIReportAlias

#Region "Variables/Properties"

    Public reportID As Integer = 0

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

    Private Sub InsertNewAlias()
        Try
            Dim parray()() As String = {
                New String() {"@ReportID", CInt(reportID)},
                New String() {"@SourceColumn", Chr(39) & txtSourceColumn.Text.Trim & Chr(39)},
                New String() {"@Alias", Chr(39) & txtAlias.Text.Trim & Chr(39)}
            }
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(8541, parray)(0), GetSQL(8541, parray)(1))
            SetMessage("Column Alias Inserted Successfully")
            Me.Hide()
        Catch ex As Exception
            SetMessage("Error : Alias Insertion Failed!")
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
            If txtSourceColumn.Text = "" Then
                SetMessage("Please enter the Source Column")
                Exit Sub
            ElseIf txtAlias.Text = "" Then
                SetMessage("Please enter the Alias")
                Exit Sub
            Else
                InsertNewAlias()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class