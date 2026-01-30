Public Class dlgCMTemplate

#Region "Variables/Properties"

    Dim connTemplate As String
    Public templateVendor As String = Nothing
    Public copyToTemplateID As Integer = Nothing
    Public actionType As String = Nothing

    Public Sub SetConnectionString(ByVal connstr As String)
        connTemplate = connstr
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

    Private Sub InsertNewTemplate()
        Try
            Dim dtRow() As DataRow = Nothing
            If dtCMTemplate IsNot Nothing Then
                dtRow = dtCMTemplate.Select("TemplateName = '" & txtTemplateName.Text.Trim & "'")
            End If
            If (dtRow IsNot Nothing AndAlso dtRow.Length > 0) Then
                SetMessage("Fail : Template Name already exists.")
                txtTemplateName.Focus()
            Else
                newTemplateName = txtTemplateName.Text.Trim
                Dim parray()() As String = {
                    New String() {"@TemplateVendor", Chr(39) & templateVendor & Chr(39)},
                    New String() {"@TemplateName", Chr(39) & newTemplateName & Chr(39)},
                    New String() {"@Owner", Chr(39) & Environment.UserName.ToString & Chr(39)},
                    New String() {"@isLocked", 0},
                    New String() {"@isScheduled", 0},
                    New String() {"@isEnabled", 1}
                }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4100, parray)(0), GetSQL(4100, parray)(1))
                SetMessage("Template Name Inserted Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Template Insertion Failed")
        End Try
    End Sub

    Private Sub CopyTemplate()
        Try
            Dim dtRow() As DataRow = Nothing
            If dtCMTemplate IsNot Nothing Then
                dtRow = dtCMTemplate.Select("TemplateName = '" & txtTemplateName.Text.Trim & "'")
            End If
            If (dtRow IsNot Nothing AndAlso dtRow.Length > 0) Then
                SetMessage("Fail : Template Name already exists.")
                txtTemplateName.Focus()
            Else
                newTemplateName = txtTemplateName.Text.Trim
                Dim parray()() As String = {
                    New String() {"@copyToTemplateID", copyToTemplateID},
                    New String() {"@templateName", Chr(39) & newTemplateName & Chr(39)},
                    New String() {"@templateOwner", Chr(39) & Environment.UserName.ToString & Chr(39)}
                }
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(GetSQL(4156, parray)(0), GetSQL(4156, parray)(1))
                SetMessage("Template Name Copied Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Template Copying Failed")
        End Try
    End Sub

#End Region

#Region "Form Events"

    Private Sub dlgCMTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If Me.actionType.ToUpper = "ADD" Then
                Me.Text = "Add New Template"
            ElseIf Me.actionType.ToUpper = "COPY" Then
                Me.Text = "Copy Template"
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
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
            If txtTemplateName.Text = "" Then
                SetMessage("Please enter the Template Name")
            Else
                If Me.actionType.ToUpper = "ADD" Then
                    InsertNewTemplate()
                ElseIf Me.actionType.ToUpper = "COPY" Then
                    CopyTemplate()
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

End Class