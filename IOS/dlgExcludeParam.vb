Imports DevExpress.XtraEditors
Imports IOS.DataLibrary
Imports IOS.Library

Public Class dlgExcludeParam

#Region "Variables/Properties"

    Dim connExcParam As String
    Public templateID As Integer = Nothing
    Public vendor As String = Nothing
    Public moTable As String = Nothing

    Public Sub SetConnectionString(ByVal connstr As String)
        connExcParam = connstr
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

    Private Sub InsertParamToExclude()
        Try
            paramToExclude = txtParamName.Text.Trim
            If paramToExclude <> "" Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@templateID", CInt(templateID)},
                    New String() {"@parameterName", Chr(39) & paramToExclude & Chr(39)}
                }
                strConnection = GetSQL(4166, parray)(0)
                sqlParam = GetSQL(4166, parray)(1)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

                SetMessage("Parameter To Exculde Added Successfully")
                frmRefCheck.SaveChangeLog(Me.templateID, "", 0, "Param exclusion: " & paramToExclude & " added to the template")
                Me.Hide()
            End If

        Catch ex As Exception
            SetMessage("Error : Exclude Parameter Insertion Fail")
        End Try
    End Sub

    Private Sub LoadParamToExcludeForSelectedMO(ByRef txt As TextEdit)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@vendor", Chr(39) & Me.vendor & Chr(39)},
            New String() {"@moTable", Chr(39) & Me.moTable & Chr(39)}
        }
        strConnection = GetSQL(4175, parray)(0)
        sqlParam = GetSQL(4175, parray)(1)
        GetTextboxDataWithAutoCompleteFeature(txt, sqlParam)
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
            If txtParamName.Text = "" Then
                SetMessage("Please enter the Param Name")
            Else
                InsertParamToExclude()
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

    Private Sub dlgExcludeParam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            LoadParamToExcludeForSelectedMO(txtParamName)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

End Class
