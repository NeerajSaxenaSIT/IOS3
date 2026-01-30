Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgAlertName

#Region "Variables/Properties"

    Dim connAlert As String
    Public alertOccurences As Integer = 0
    Public alertSlidingWinDays As Integer = 0

    Public Sub SetConnectionString(ByVal connstr As String)
        connAlert = connstr
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

    Private Sub InsertNewAlert()
        Try
            Dim dtRow() As DataRow = Nothing
            If dtAlertName IsNot Nothing Then
                dtRow = dtAlertName.Select("AlertName = '" & txtAlertName.Text.Trim & "'")
            End If
            If (dtRow.Length > 0) Then
                SetMessage("Fail : Alert Name already exists.")
                txtAlertName.Focus()
            Else
                newAlertName = txtAlertName.Text.Trim
                Dim parray()() As String = {
                    New String() {"@AlertName", Chr(39) & newAlertName & Chr(39)},
                    New String() {"@AlertOccurences", alertOccurences},
                    New String() {"@AlertSlidingWinDays", alertSlidingWinDays},
                    New String() {"@AlertOwner", Chr(39) & Environment.UserName & Chr(39)}
                }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(3814, parray)(0), GetSQL(3814, parray)(1))
                SetMessage("Alert Name Inserted Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Alert Insertion Fail")
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
            If txtAlertName.Text = "" Then
                SetMessage("Please enter the Alert Name")
            Else
                InsertNewAlert()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub vbtn_Cancel_Click(sender As Object, e As EventArgs) Handles vbtn_Cancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class
