Imports LidorSystems.IntegralUI.Lists
Imports IOS.Library

Public Class dlgChartCategory

#Region "Properties"

    Private _CategoryTab As String = Nothing
    Public ReadOnly Property CategoryTab() As String
        Get
            Return _CategoryTab
        End Get
    End Property

    Private _CategoryTabIndex As String = Nothing
    Public ReadOnly Property CategoryTabIndex() As String
        Get
            Return _CategoryTabIndex
        End Get
    End Property

#End Region

#Region "Form & Controls Event"

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
    End Sub

    Private Sub btnCharCategoryInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCharCategoryInsert.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If Not (txtChartCategoryName.Text.Trim = "") AndAlso Not (lblCategoryIndex.Text.Trim = "") Then
                If Not (txtChartCategoryName.Text.ToUpper = "CUSTOM") Then
                    If (frmChartCustomization.IsCategoryExist(txtChartCategoryName.Text)) Then
                        _CategoryTab = txtChartCategoryName.Text
                        _CategoryTabIndex = lblCategoryIndex.Text
                        lblMessage.Visible = False
                        Me.Close()
                    Else
                        SetMessage("Catagory already exists")
                        'lblMessage.Visible = True
                        txtChartCategoryName.Text = ""
                        txtChartCategoryName.Focus()
                    End If
                Else
                    SetMessage("The name Custom can't be use")
                    lblMessage.Visible = True
                End If
            Else
                SetMessage("Please Enter Category Tab or Category Tab Index")
                lblMessage.Visible = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub frm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BringToFront()
        GetCategoryMaxIndex()
        txtChartCategoryName.Focus()
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

#End Region

#Region "Helper"

    Private Sub GetCategoryMaxIndex()
        Dim tech As String = frmChartCustomization.cmbTechnology.SelectedItem.ToString
        Dim _chartSetName As String = frmChartCustomization.cmbChartSetName.SelectedItem.ToString
        Dim objecttab As String = frmChartCustomization.cmbObjectType.SelectedItem.ToString

        Dim tabIndex As Integer = IOS.DataLibrary.clsSQLCommands.GetMaxCategoryIndex(connStrIOSServer, tech, _chartSetName, objecttab)
        If (tabIndex <= 0) Then
            lblCategoryIndex.Text = "1"
        Else
            lblCategoryIndex.Text = (tabIndex + 1).ToString()
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

#End Region

End Class
