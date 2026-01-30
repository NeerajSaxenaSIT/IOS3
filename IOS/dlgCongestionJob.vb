Public Class dlgCongestionJob

#Region "Variables/Properties"

    Public addNewJob As Boolean = False
    Public copyJob As Boolean = False
    Public copyRule As Boolean = False
    Public tobeCopiedJobID As Integer = 0
    Public tobeCopiedJobName As String = Nothing
    Public tobeCopiedRuleID As Integer = 0
    Public tobeCopiedRuleName As String = Nothing

#End Region

#Region "Form Events"

    Private Sub dlgCongestionJob_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If addNewJob = True Or copyJob = True Then
                lblName.Text = "Congestion Job Name"
                If addNewJob = True Then
                    Me.Text = "Add New Job"
                ElseIf copyJob = True Then
                    Me.Text = "Copy Job " & tobeCopiedJobName & " and all it's rules"
                End If
            ElseIf copyRule = True Then
                lblName.Text = "Congestion Rule Name"
                Me.Text = "Copy Rule " & tobeCopiedRuleName
                ShowSelectJobControls(copyRule)
                LoadJobCombo()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub LoadJobCombo()
        Dim dt As DataTable = IOS.DataLibrary.clsSQLCommands.GetCapacityJobList(connStrIOSServer)
        BindDevExComboBoxWithValueMember(cmbCapJobs, dt, "CapJobID", "CapJobName", "-- Select Job --", True)
    End Sub

    Private Sub ShowSelectJobControls(show As Boolean)
        lblSelectJob.Visible = show
        cmbCapJobs.Visible = show
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
            If addNewJob = True Then
                If txtName.Text = "" Then
                    SetMessage("Please enter the Congestion Job Name")
                Else
                    AddCongestionJob()
                End If
            ElseIf copyJob = True Then
                If txtName.Text = "" Then
                    SetMessage("Please enter the Congestion Job Name")
                Else
                    CopyCongestionJob()
                End If
            ElseIf copyRule = True Then
                If txtName.Text = "" Then
                    SetMessage("Please enter the Congestion Rule Name")
                ElseIf cmbCapJobs.SelectedIndex = 0 Then
                    SetMessage("Please select Congestion Job")
                Else
                    CopyCongestionRule()
                End If
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

#Region "Private Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub AddCongestionJob()
        Try
            Dim dtRow() As DataRow = Nothing
            If dtCongestionJobsList IsNot Nothing Then
                dtRow = dtCongestionJobsList.Select("CapJobName = '" & txtName.Text.Trim & "'")
            End If
            If (dtRow.Length > 0) Then
                SetMessage("Fail : Congestion Job already exists.")
                txtName.Focus()
            Else
                newCongestionJob = txtName.Text.Trim
                Dim parray()() As String = {
                    New String() {"@capJobName", Chr(39) & newCongestionJob & Chr(39)},
                    New String() {"@owner", Chr(39) & Environment.UserName & Chr(39)}
                }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(3003, parray)(0), GetSQL(3003, parray)(1))
                SetMessage("Congestion Job added successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Congestion Job Insertion Fail")
        End Try
    End Sub

    Private Sub CopyCongestionJob()
        Try
            copyJobName = txtName.Text.Trim
            Dim parray()() As String = {
                New String() {"@capJobID", CInt(tobeCopiedJobID)},
                New String() {"@copyJobName", Chr(39) & copyJobName & Chr(39)}
            }
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(3042, parray)(0), GetSQL(3042, parray)(1))
            SetMessage("Congestion Job copied successfully")
            Me.Hide()
        Catch ex As Exception
            SetMessage("Error : Congestion Job Copy Fail")
        End Try
    End Sub

    Private Sub CopyCongestionRule()
        Try
            copyRuleName = txtName.Text.Trim
            Dim parray()() As String = {
                New String() {"@capJobID", CInt(TryCast(cmbCapJobs.SelectedItem, IOS.Library.clsComboBoxItem).Value)},
                New String() {"@capCongRuleID", CInt(tobeCopiedRuleID)},
                New String() {"@copyRuleName", Chr(39) & copyRuleName & Chr(39)}
            }
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(3043, parray)(0), GetSQL(3043, parray)(1))
            SetMessage("Congestion Rule copied successfully")
            Me.Hide()
        Catch ex As Exception
            SetMessage("Error : Congestion Rule Copy Fail")
        End Try
    End Sub

#End Region

End Class