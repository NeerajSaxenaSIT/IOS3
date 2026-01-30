Imports IOS.Library
Imports IOS.DataLibrary

Public Class frmCreateSiteIntProject

#Region "Variables"



#End Region

#Region "Methods"

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub LoadTemplates()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8208, parray)(0)
        sqlParam = GetSQL(8208, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbTemplate, dt, "TemplateID", "TemplateName", "Select")
    End Sub

    Private Sub LoadRISObjects()
        RemoveHandler cmbRISObject.SelectedIndexChanged, AddressOf cmbRISObject_SelectedIndexChanged
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8201, parray)(0)
        sqlParam = GetSQL(8201, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbRISObject, dt, "LOCID", "LOCID", "Select")
        AddHandler cmbRISObject.SelectedIndexChanged, AddressOf cmbRISObject_SelectedIndexChanged
    End Sub

    Private Sub LoadNodeIdentifier()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8226, parray)(0)
        sqlParam = GetSQL(8226, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbNdeIdentifier, dt, "NodeIdentifier", "ENM", "Select")
    End Sub

    Private Sub SetNodeIdentifierForRIS()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@RISObject", Chr(39) & cmbRISObject.SelectedItem.ToString & Chr(39)}
        }
        strConnection = GetSQL(8235, parray)(0)
        sqlParam = GetSQL(8235, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        'BindDevExComboBoxWithValueMember(cmbNdeIdentifier, dt, "NodeIdentifier", "NodeIdentifier", "Select")
        SetComboBox(cmbNdeIdentifier, ComboSelectBased.ValueBased, dt.Rows(0)(0))
    End Sub

    Private Sub LoadUpgradePkgList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8227, parray)(0)
        sqlParam = GetSQL(8227, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbUpgradePkgName, dt, "UpgradePackageName", "UpgradePackageName", "Select")
    End Sub

    Private Sub LoadBBUSet()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@RISObject", Chr(39) & cmbRISObject.SelectedItem.ToString & Chr(39)}
        }
        strConnection = GetSQL(8234, parray)(0)
        sqlParam = GetSQL(8234, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        BindDevExComboBoxWithValueMember(cmbBBUset, dt, "BBU_Set", "BBU_Set", "Select")
        cmbBBUset.SelectedIndex = 1
    End Sub

#End Region

#Region "Events"

    Private Sub frmCreateSiteIntProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadRISObjects()
            LoadTemplates()
            LoadNodeIdentifier()
            LoadUpgradePkgList()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnCreateProject_Click(sender As Object, e As EventArgs) Handles btnCreateProject.Click
        Try
            If txtProjectName.Text.Trim = String.Empty Then
                SetMessage("Please Enter Project Name")
                Exit Sub
            ElseIf cmbTemplate.SelectedIndex = 0 Then
                SetMessage("Please Select Template")
                Exit Sub
            ElseIf cmbRISObject.SelectedIndex = 0 Then
                SetMessage("Please Select RIS Objct")
                Exit Sub
            ElseIf cmbBBUset.SelectedIndex = 0 Then
                SetMessage("Please Select BBU Set")
                Exit Sub
            ElseIf cmbNdeIdentifier.SelectedIndex = 0 Then
                SetMessage("Please Select Node Identifier")
                Exit Sub
            ElseIf cmbUpgradePkgName.SelectedIndex = 0 Then
                SetMessage("Please Select Upgrade Package Name")
                Exit Sub
            ElseIf txtSerialNumber_BBU1.Text.Trim = String.Empty Then
                SetMessage("Please Enter Serial Number BBU1")
                Exit Sub
            ElseIf txtSerialNumber_BBU2.Text.Trim = String.Empty Then
                SetMessage("Please Enter Serial Number BBU2")
                Exit Sub
            End If

            SIProjectName = txtProjectName.Text.Trim

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ProjectName", Chr(39) & txtProjectName.Text.Trim & Chr(39)},
                New String() {"@Owner", Chr(39) & Environment.UserName & Chr(39)},
                New String() {"@RISObject", Chr(39) & cmbRISObject.SelectedItem.ToString & Chr(39)},
                New String() {"@SerialNumber_BBU1", Chr(39) & txtSerialNumber_BBU1.Text.Trim & Chr(39)},
                New String() {"@SerialNumber_BBU2", Chr(39) & txtSerialNumber_BBU2.Text.Trim & Chr(39)},
                New String() {"@SiteIntegrationTemplateID", CInt(CType(cmbTemplate.SelectedItem, clsComboBoxItem).Value)},
                New String() {"@NodeIdentifier", Chr(39) & CStr(CType(cmbNdeIdentifier.SelectedItem, clsComboBoxItem).Value) & Chr(39)},
                New String() {"@UpgradePackage", Chr(39) & CStr(CType(cmbUpgradePkgName.SelectedItem, clsComboBoxItem).Value) & Chr(39)},
                New String() {"@BBU_Set", Chr(39) & cmbBBUset.SelectedItem.ToString & Chr(39)}
            }
            strConnection = GetSQL(8207, parray)(0)
            sqlParam = GetSQL(8207, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            Me.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
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

    Private Sub cmbRISObject_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbRISObject.SelectedIndex > 0 Then
                LoadBBUSet()
                SetNodeIdentifierForRIS()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

End Class