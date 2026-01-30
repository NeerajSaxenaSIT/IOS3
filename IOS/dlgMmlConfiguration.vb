Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgMmlConfiguration

#Region "Variables/Properties"

    Dim connStr As String
    Public mmlConfigID As Integer = 0

    Public Sub SetConnectionString(ByVal conn As String)
        connStr = connStr
    End Sub

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

    Private Sub InsertNewMmlConfig()
        Try
            Dim parray()() As String = {
                                            New String() {"@MMLConfigID", mmlConfigID},
                                            New String() {"@MMLConfigName", Chr(39) & txtConfigName.Text.Trim & Chr(39)},
                                            New String() {"@MMLConfigDescription", Chr(39) & txtDescription.Text & Chr(39)},
                                            New String() {"@Owner", Chr(39) & Environment.UserName & Chr(39)}
                                        }
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4522, parray)(0), GetSQL(4522, parray)(1))
            SetMessage("MML Configuration Clone Inserted Successfully")
        Catch ex As Exception
            SetMessage("Error : Mml Config Clone Insertion Fail")
        End Try
    End Sub

#End Region

#Region "Form Events"

    Private Sub dlgMmlConfiguration_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtConfigName.Text = String.Empty
            txtDescription.Text = String.Empty
        Catch ex As Exception
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

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If txtConfigName.Text = "" Then
                SetMessage("Please Enter Configuration Name")
            Else
                InsertNewMmlConfig()
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class