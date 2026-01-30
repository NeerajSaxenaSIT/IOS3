Imports IOS.DataLibrary
Imports IOS.Library

Public Enum GroupType
    SandboxGroup
    DashboardGroup
    JobGroup
    KpiGroup
    KpiGroupModify
    KpiCategory
    SandboxReport
End Enum

Public Class dlgSBGroupInsert

#Region "Variables/Properties"

    Dim connSandBox As String
    Private _NewGroup As String
    Public alert As String = ""
    Public reportCategoryID As Integer
    Public reportGroupID As Integer
    Public reportName As String = ""
    Public ReadOnly Property NewGroup() As String
        Get
            Return _NewGroup
        End Get
    End Property
    Private _IsGroupPrivate As Boolean
    Public ReadOnly Property IsGroupPrivate() As Boolean
        Get
            Return _IsGroupPrivate
        End Get
    End Property
    Private _GroupTypeInserting As GroupType
    Public Property GroupTypeInserting() As GroupType
        Get
            Return _GroupTypeInserting
        End Get
        Set(value As GroupType)
            _GroupTypeInserting = value
        End Set
    End Property
    Private _KPIGroupID As GroupType
    Public Property KPIGroupID() As GroupType
        Get
            Return _KPIGroupID
        End Get
        Set(value As GroupType)
            _KPIGroupID = value
        End Set
    End Property

    Public Sub SetConnectionString(ByVal connstr As String)
        connSandBox = connstr
    End Sub

#End Region

#Region "Events"

    Private Sub frmDialog_GroupInsert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            lblMessage.Text = ""
            If (GroupTypeInserting = GroupType.SandboxGroup) Then
                lblGroupName.Text = "Report Group Name "
                Me.Text = "Add New Report Group"
                rbPrivate.Checked = True
            ElseIf (GroupTypeInserting = GroupType.DashboardGroup) Then
                lblGroupName.Text = "Dashboard Group Name "
                Me.Text = "Add New Dashboard Group"
                rbPrivate.Checked = True
            ElseIf (GroupTypeInserting = GroupType.JobGroup) Then
                lblGroupName.Text = "Job Group Name "
                Me.Text = "Add New Job Group"
            ElseIf (GroupTypeInserting = GroupType.KpiGroup) Then
                lblGroupName.Text = "KPI Group Name "
                Me.Text = "Add New KPI Group"
            ElseIf (GroupTypeInserting = GroupType.KpiCategory) Then
                lblGroupName.Text = "Category Name "
                btnAddGroup.Text = "Add Category"
                Me.Text = "Add New KPI Category"
                gcPublicPrivate.Enabled = False
                rbPrivate.Checked = True
            ElseIf (GroupTypeInserting = GroupType.SandboxReport) Then
                Me.Text = "Add New Report"
                lblGroupName.Text = "New Report Name"
                gcPublicPrivate.Visible = False
                TableLayoutPanel1.RowStyles(1) = New RowStyle(SizeType.Percent, 20)
                TableLayoutPanel1.RowStyles(3) = New RowStyle(SizeType.Percent, 30)
                btnAddGroup.Text = "Add Report"
                rbPrivate.Checked = True
                lblMessage.Text = alert
                lblMessage.ForeColor = Color.Red
            Else

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
    End Sub

    Private Sub vbtn_Cancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub vbtn_AddGroup_Click(sender As Object, e As EventArgs) Handles btnAddGroup.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            If rbPrivate.Checked = False And rbPublic.Checked = False Then
                SetMessage("Please select Private or Public.")
            Else
                If (GroupTypeInserting = GroupType.SandboxGroup) Then
                    If txtGroupName.Text = "" Then
                        SetMessage("Please enter the Group Name")
                        Exit Sub
                    End If
                    SandboxGroupInsert()
                ElseIf (GroupTypeInserting = GroupType.DashboardGroup) Then
                    If txtGroupName.Text = "" Then
                        SetMessage("Please enter the Group Name")
                        Exit Sub
                    End If
                    DashboardGroupInsert()
                ElseIf (GroupTypeInserting = GroupType.JobGroup) Then
                    If txtGroupName.Text = "" Then
                        SetMessage("Please enter the Group Name")
                        Exit Sub
                    End If
                    JobGroupInsert()
                ElseIf (GroupTypeInserting = GroupType.KpiGroup) Then
                    If txtGroupName.Text = "" Then
                        SetMessage("Please enter the Group Name")
                        Exit Sub
                    End If
                    KpiGroupInsert()
                ElseIf (GroupTypeInserting = GroupType.KpiGroupModify) Then
                    If txtGroupName.Text = "" Then
                        SetMessage("Please enter the Group Name")
                        Exit Sub
                    End If
                    KpiGroupModify()
                ElseIf (GroupTypeInserting = GroupType.KpiCategory) Then
                    If txtGroupName.Text = "" Then
                        SetMessage("Please enter the Category Name")
                        Exit Sub
                    End If
                    CategoryInsert()
                ElseIf (GroupTypeInserting = GroupType.SandboxReport) Then
                    If txtGroupName.Text = "" Then
                        SetMessage("Please enter the Report Name")
                        Exit Sub
                    End If
                    SandboxReportInsert()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

#End Region

#Region "Private Methods"

    Private Sub CategoryInsert()
        Try
            Dim newCategory As String = txtGroupName.Text
            Dim dtExitGroup As DataTable = DataAccessorODBC.GetDataTable(connSandBox, New SQLKpiCategory().SelectAll(False, KPIGroupFields.KPI_CATEGORY_NAME & OperatorConst.Equal & "'" & newCategory & "'"))
            If (dtExitGroup.Rows.Count > 0) Then
                txtGroupName.Text = ""
                SetMessage("Fail : Category Already exists.")
            Else
                DataAccessorODBC.ExecuteNonQuery(connSandBox, SQLKpiCategory.InsertCategory(KPIGroupID, newCategory))
                Me._NewGroup = txtGroupName.Text
                objSandbox.SetMessage("Category Inserted Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Category Insertion Fail")
        End Try
    End Sub

    Private Sub SandboxReportInsert()
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            Dim recordCount As Integer = DataAccessorODBC.ExecuteNonQuery(connSandBox, SQLReports.InsertReports(reportGroupID, reportCategoryID, txtGroupName.Text.Trim()))
            If recordCount > 0 Then
                Me.reportName = txtGroupName.Text.Trim
                Me.Hide()
            ElseIf recordCount = -1 Then
                GroupTypeInserting = GroupType.SandboxReport
                lblMessage.Text = "Report name already exists, try another name."
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & "-" & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
        End Try
    End Sub

    Private Sub KpiGroupInsert()
        Dim isKpiGroupPrivate As Boolean = False
        If rbPrivate.Checked = True Then
            isKpiGroupPrivate = True
        ElseIf rbPublic.Checked = True Then
            isKpiGroupPrivate = False
        End If
        Try
            Dim newGroup As String = txtGroupName.Text
            Dim dtExitGroup As DataTable = DataAccessorODBC.GetDataTable(connSandBox, New SQLKpiGroup().SelectAll(False, KPIGroupFields.KPI_GROUP_NAME & OperatorConst.Equal & "'" & newGroup & "'"))
            If (dtExitGroup.Rows.Count > 0) Then
                txtGroupName.Text = ""
                SetMessage("Fail : Group Already exists.")
            Else
                DataAccessorODBC.ExecuteNonQuery(connSandBox, SQLKpiGroup.InsertGroup(newGroup, isKpiGroupPrivate))
                Me._NewGroup = txtGroupName.Text
                Me._IsGroupPrivate = isKpiGroupPrivate
                objSandbox.SetMessage("Group Inserted Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Group Insertion Fail")
        End Try
    End Sub

    Private Sub KpiGroupModify()
        Dim isKpiGroupPrivate As Boolean = False
        If rbPrivate.Checked = True Then
            isKpiGroupPrivate = True
        ElseIf rbPublic.Checked = True Then
            isKpiGroupPrivate = False
        End If
        Try
            Dim newGroup As String = txtGroupName.Text
            Dim dtExitGroup As DataTable = DataAccessorODBC.GetDataTable(connSandBox, New SQLKpiGroup().SelectAll(False, KPIGroupFields.KPI_GROUP_NAME & OperatorConst.Equal & "'" & newGroup & "'"))
            If (dtExitGroup.Rows.Count > 0) Then
                txtGroupName.Text = ""
                SetMessage("Fail : Group Already exists.")
            Else
                DataAccessorODBC.ExecuteNonQuery(connSandBox, SQLKpiGroup.ModifyGroup(KPIGroupID, newGroup, isKpiGroupPrivate))
                Me._NewGroup = txtGroupName.Text
                Me._IsGroupPrivate = isKpiGroupPrivate
                objSandbox.SetMessage("Group Modified Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Group Modification Fail")
        End Try
    End Sub

    Private Sub SandboxGroupInsert()
        Dim isReportGroupPrivate As Boolean = False
        If rbPrivate.Checked = True Then
            isReportGroupPrivate = True
        ElseIf rbPublic.Checked = True Then
            isReportGroupPrivate = False
        End If
        Try
            Dim newGroup As String = txtGroupName.Text
            Dim dtExitGroup As DataTable = DataAccessorODBC.GetDataTable(connSandBox, New SQLReportGroups().SelectAll(False, ReportGroupsFields.REPORT_GROUP_NAME & OperatorConst.Equal & "'" & newGroup & "'"))
            If (dtExitGroup.Rows.Count > 0) Then
                txtGroupName.Text = ""
                SetMessage("Fail : Group Already exists.")
            Else
                DataAccessorODBC.ExecuteNonQuery(connSandBox, SQLReportGroups.InsertGroup(newGroup, isReportGroupPrivate))
                Me._NewGroup = txtGroupName.Text
                Me._IsGroupPrivate = isReportGroupPrivate
                objSandbox.SetMessage("Group Inserted Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Group Insertion Fail")
        End Try
    End Sub

    Private Sub DashboardGroupInsert()
        Dim isDashBoardGroupPrivate As Boolean = False
        If rbPrivate.Checked = True Then
            isDashBoardGroupPrivate = True
        ElseIf rbPublic.Checked = True Then
            isDashBoardGroupPrivate = False
        End If
        Try
            Dim newGroup As String = txtGroupName.Text
            Dim dtExitGroup As DataTable = DataAccessorODBC.GetDataTable(connSandBox, New SQLDashboardGroups().SelectAll(False, DashboardGroupsFields.DashboardGroupName & OperatorConst.Equal & "'" & newGroup & "'"))
            If (dtExitGroup.Rows.Count > 0) Then
                txtGroupName.Text = ""
                SetMessage("Fail : Group Already exists.")
            Else
                DataAccessorODBC.ExecuteNonQuery(connSandBox, SQLDashboardGroups.Insert(newGroup, isDashBoardGroupPrivate))
                Me._NewGroup = txtGroupName.Text
                Me._IsGroupPrivate = isDashBoardGroupPrivate
                objSandbox.SetMessage("Group Inserted Successfully")
                Me.Hide()
            End If

        Catch ex As Exception
            SetMessage("Error : Group Insertion Fail")
        End Try
    End Sub

    Private Sub JobGroupInsert()
        Dim isJobGroupPrivate As Boolean = False
        If rbPrivate.Checked = True Then
            isJobGroupPrivate = True
        ElseIf rbPublic.Checked = True Then
            isJobGroupPrivate = False
        End If
        Try
            Dim newGroup As String = txtGroupName.Text
            Dim dtExitGroup As DataTable = DataAccessorODBC.GetDataTable(connSandBox, New SQLJobGroups().SelectAll(False, JobGroupFields.JobGroupName & OperatorConst.Equal & "'" & newGroup & "'"))
            If (dtExitGroup.Rows.Count > 0) Then
                txtGroupName.Text = ""
                SetMessage("Fail : Group Already exists.")
            Else
                DataAccessorODBC.ExecuteNonQuery(connSandBox, SQLJobGroups.InsertJobGroup(newGroup, isJobGroupPrivate))
                Me._NewGroup = txtGroupName.Text
                Me._IsGroupPrivate = isJobGroupPrivate
                objSandbox.SetMessage("Group Inserted Successfully")
                Me.Hide()
            End If
        Catch ex As Exception
            SetMessage("Error : Group Insertion Fail")
        End Try
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblMessage.ForeColor = Color.Red
        lblMessage.Visible = True
        lblMessage.Text = message
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub ClearData()
        txtGroupName.Text = ""
        rbPrivate.Checked = False
        rbPublic.Checked = False
    End Sub

#End Region

End Class