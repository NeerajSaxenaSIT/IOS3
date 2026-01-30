Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports System.ComponentModel
Imports DevExpress.XtraEditors.Repository
Imports System.Threading
Imports System.Xml
Imports System.Text
Imports DevExpress.XtraEditors.Controls
Imports System.IO.Compression
Imports System.IO
Imports System.Security.AccessControl

Public Class frmSiteIntegration

#Region "Variables"

    Private x As String = Nothing
    Private y As String = Nothing
    Private z As String = Nothing

    'Private riCmbRISObj As RepositoryItemComboBox
    'Private riCmbTemplate As RepositoryItemComboBox
    'Private dtRISObject As DataTable = Nothing
    'Private dtTemplates As DataTable = Nothing
    Private riCmbNI As RepositoryItemComboBox
    Private riCmbUPN As RepositoryItemComboBox

    Private dtNodeIdentifier As DataTable = Nothing
    Private dtUPNameList As DataTable = Nothing

    Private objGenXMLThread As Thread
    Private Delegate Sub CallThreadInvokedGenXMLFile()
    Private objGenXMlThreadLock As New Object
    Private dtValidationErrors As DataTable = Nothing
    Private dtXmlFiles As DataTable = Nothing

#End Region

#Region "Form Load Event"

    Private Sub frmSiteIntegration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'LoadRISObjects()
            'LoadTemplates()
            LoadNodeIdentifier()
            LoadUpgradePackageName()
            LoadProjects()
            LoadValidationChecks()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

#End Region

#Region "Project"

    Private Sub LoadProjects()
        RemoveHandler gvProject.RowCellClick, AddressOf gvProject_RowCellClick
        RemoveHandler gvProject.ShowingEditor, AddressOf gvProject_ShowingEditor
        RemoveHandler gvProject.CustomRowCellEdit, AddressOf gvProject_CustomRowCellEdit
        RemoveHandler gvProject.FocusedRowChanged, AddressOf gvProject_FocusedRowChanged
        RemoveHandler gvProject.CellValueChanged, AddressOf gvProject_CellValueChanged

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8200, parray)(0)
        sqlParam = GetSQL(8200, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim cols2Hide() As String = {"SI_ProjectTimeStamp", "SI_TemplateID"}
        IOSDevExpressGrid.PopulateDataInGrid(gridProject, gvProject, dt, "ALL", cols2Hide, Nothing)

        AddHandler gvProject.RowCellClick, AddressOf gvProject_RowCellClick
        AddHandler gvProject.ShowingEditor, AddressOf gvProject_ShowingEditor
        AddHandler gvProject.CustomRowCellEdit, AddressOf gvProject_CustomRowCellEdit
        AddHandler gvProject.FocusedRowChanged, AddressOf gvProject_FocusedRowChanged
        AddHandler gvProject.CellValueChanged, AddressOf gvProject_CellValueChanged
        gvProject_FocusedRowChanged(gvProject, Nothing)
    End Sub

    Private Sub DeleteProject()
        Dim prjID As Integer = CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@SI_ProjectID", prjID}
        }
        strConnection = GetSQL(8210, parray)(0)
        sqlParam = GetSQL(8210, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub btnCreateProject_Click(sender As Object, e As EventArgs) Handles btnCreateProject.Click
        Try
            SIProjectName = Nothing
            Dim objCreateSIPrj As New frmCreateSiteIntProject()
            objCreateSIPrj.ShowDialog()
            LoadProjects()
            LoadReviewRISTabPagesData()
            gvProject.SetFocusedRowCellValue("SI_ProjectName", SIProjectName)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRefreshProject_Click(sender As Object, e As EventArgs) Handles btnRefreshProject.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadProjects()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub UpdateProject()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))}
        }
        strConnection = GetSQL(8237, parray)(0)
        sqlParam = GetSQL(8237, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub btnDeleteProject_Click(sender As Object, e As EventArgs) Handles btnDeleteProject.Click
        Try
            If gvProject.RowCount > 0 Then
                Dim prjName As String = CStr(gvProject.GetFocusedRowCellValue("SI_ProjectName"))
                If XtraMessageBox.Show("Are you sure to delete project: " & prjName & "?", "Delete Project", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    DeleteProject()
                    LoadProjects()
                End If
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gvProject_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        Try
            Dim gv As GridView = TryCast(sender, GridView)
            Dim gc As GridControl = gv.GridControl
            'Dim paramName As String = Nothing
            If e.Column.FieldName = "NodeIdentifier" Then
                CreateNodeIdCombo(gc)
                e.RepositoryItem = riCmbNI
            ElseIf e.Column.FieldName = "UpgradePackageName" Then
                'paramName = gv.GetFocusedRowCellValue("RIS_Object").ToString
                CreateUPNCombo(gc)
                e.RepositoryItem = riCmbUPN
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try '
    End Sub

    Private Sub gvProject_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            lblValidateStatus.Text = ""
            If gvProject.FocusedRowHandle >= 0 Then
                'Load_ReviewRIS_2G()
                Load_ReviewRIS_3G()
                Load_ReviewRIS_4G()
                Load_ReviewRIS_5G()
                Load_ReviewRIS_IP()
                Load_ReviewRIS_BB()
                Load_ReviewRIS_Config()
                LoadGenFilesList()
                LoadGenStatus()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub gvProject_RowCellClick(sender As Object, e As Views.Grid.RowCellClickEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gv As GridView = TryCast(sender, GridView)
            Dim gc As GridControl = gv.GridControl
            If e.Column.FieldName = "NodeIdentifier" Then
                CreateNodeIdCombo(gc)
            ElseIf e.Column.FieldName = "UpgradePackageName" Then
                CreateUPNCombo(gc)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub CreateNodeIdCombo(gc As GridControl)
        Try
            riCmbNI = New RepositoryItemComboBox()
            RemoveHandler riCmbNI.SelectedIndexChanged, AddressOf riCmbNI_SelectedIndexChanged
            If Not dtNodeIdentifier Is Nothing Then
                gc.RepositoryItems.Clear()
                gc.RepositoryItems.Add(riCmbNI)
                Dim items As String() = dtNodeIdentifier.AsEnumerable().Select(Function(x) x.Field(Of String)("NodeIdentifier")).ToArray()
                riCmbNI.Items.AddRange(items)
            End If
            AddHandler riCmbNI.SelectedIndexChanged, AddressOf riCmbNI_SelectedIndexChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub CreateUPNCombo(gc As GridControl)
        Try
            riCmbUPN = New RepositoryItemComboBox()
            RemoveHandler riCmbUPN.SelectedIndexChanged, AddressOf riCmbUPN_SelectedIndexChanged
            If Not dtUPNameList Is Nothing Then
                gc.RepositoryItems.Clear()
                gc.RepositoryItems.Add(riCmbUPN)
                riCmbUPN.AutoHeight = False
                Dim items As String() = dtUPNameList.AsEnumerable().Select(Function(x) x.Field(Of String)("UpgradePackageName")).ToArray()
                riCmbUPN.Items.AddRange(items)
            End If
            AddHandler riCmbUPN.SelectedIndexChanged, AddressOf riCmbUPN_SelectedIndexChanged
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub riCmbNI_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim riCmb As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
            If riCmb.SelectedIndex >= 0 Then
                'Dim NI As String = dtNodeIdentifier.Select("ENM='" & riCmb.SelectedItem.ToString & "'")(0)("NodeIdentifier")
                UpdateSIProjectDetails(CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID")), "NodeIdentifier", riCmb.SelectedItem.ToString)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub riCmbUPN_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim riCmb As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
            If riCmb.SelectedIndex >= 0 Then
                UpdateSIProjectDetails(CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID")), "UpgradePackageName", riCmb.SelectedItem.ToString)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub UpdateSIProjectDetails(siPrjID As Integer, colToUpdate As String, colValue As Object)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@SI_ProjectID", siPrjID},
            New String() {"@ColumnToUpdate", Chr(39) & colToUpdate & Chr(39)},
            New String() {"@ColumnValue", Chr(39) & colValue & Chr(39)}
        }
        strConnection = GetSQL(8211, parray)(0)
        sqlParam = GetSQL(8211, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadUpgradePkgList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8227, parray)(0)
        sqlParam = GetSQL(8227, parray)(1)
        dtUPNameList = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub gvProject_ShowingEditor(sender As Object, e As CancelEventArgs)
        Try
            Dim gv As GridView = TryCast(sender, GridView)
            If (gv.FocusedColumn().FieldName = "NodeIdentifier") Or (gv.FocusedColumn().FieldName = "UpgradePackageName") Or
                (gv.FocusedColumn().FieldName = "SerialNumber_BBU1") Or (gv.FocusedColumn().FieldName = "SerialNumber_BBU2") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvProject_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        Try
            If e.Column.FieldName = "SerialNumber_BBU1" Then
                Dim gv As GridView = DirectCast(sender, GridView)
                UpdateSIProjectDetails(CInt(gv.GetFocusedRowCellValue("SI_ProjectID")), e.Column.FieldName, e.Value.ToString)
            ElseIf e.Column.FieldName = "SerialNumber_BBU2" Then
                Dim gv As GridView = DirectCast(sender, GridView)
                UpdateSIProjectDetails(CInt(gv.GetFocusedRowCellValue("SI_ProjectID")), e.Column.FieldName, e.Value.ToString)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    'Private Sub LoadRISObjects()
    '    Dim strConnection As String = Nothing
    '    Dim sqlParam As String = Nothing
    '    Dim parray()() As String = Nothing
    '    strConnection = GetSQL(8201, parray)(0)
    '    sqlParam = GetSQL(8201, parray)(1)
    '    dtRISObject = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    'End Sub

    'Private Sub LoadTemplates()
    '    Dim strConnection As String = Nothing
    '    Dim sqlParam As String = Nothing
    '    Dim parray()() As String = Nothing
    '    strConnection = GetSQL(8208, parray)(0)
    '    sqlParam = GetSQL(8208, parray)(1)
    '    dtTemplates = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    'End Sub

    Private Sub LoadNodeIdentifier()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8226, parray)(0)
        sqlParam = GetSQL(8226, parray)(1)
        dtNodeIdentifier = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadUpgradePackageName()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8227, parray)(0)
        sqlParam = GetSQL(8227, parray)(1)
        dtUPNameList = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub tsmi_UpdateProject_Click(sender As Object, e As EventArgs) Handles tsmi_UpdateProject.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If gvProject.RowCount > 0 Then
                UpdateProject()
                gvProject_FocusedRowChanged(gvProject, Nothing)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Review RIS"

    Private Sub LoadReviewRISTabPagesData()
        'Load_ReviewRIS_2G()
        Load_ReviewRIS_3G()
        Load_ReviewRIS_4G()
        Load_ReviewRIS_5G()
        Load_ReviewRIS_IP()
        Load_ReviewRIS_BB()
        Load_ReviewRIS_Config()
    End Sub

    Private Sub btnRevRISSave2G_Click(sender As Object, e As EventArgs) Handles btnRevRISSave2G.Click
        Try

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRevRISSave3G_Click(sender As Object, e As EventArgs) Handles btnRevRISSave3G.Click
        Try

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRevRISSave4G_Click(sender As Object, e As EventArgs) Handles btnRevRISSave4G.Click
        Try

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRevRISSave5G_Click(sender As Object, e As EventArgs) Handles btnRevRISSave5G.Click
        Try

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRevRISSaveIP_Click(sender As Object, e As EventArgs) Handles btnRevRISSaveIP.Click
        Try

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub btnRevRISSaveBB_Click(sender As Object, e As EventArgs) Handles btnRevRISSaveBB.Click
        Try

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub xtcReviewRIS_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcReviewRIS.SelectedPageChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If xtcReviewRIS.SelectedTabPageIndex = 0 Then
                'Load_ReviewRIS_2G()
            ElseIf xtcReviewRIS.SelectedTabPageIndex = 1 Then
                Load_ReviewRIS_3G()
            ElseIf xtcReviewRIS.SelectedTabPageIndex = 2 Then
                Load_ReviewRIS_4G()
            ElseIf xtcReviewRIS.SelectedTabPageIndex = 3 Then
                Load_ReviewRIS_5G()
            ElseIf xtcReviewRIS.SelectedTabPageIndex = 4 Then
                Load_ReviewRIS_IP()
            ElseIf xtcReviewRIS.SelectedTabPageIndex = 5 Then
                Load_ReviewRIS_BB()
            ElseIf xtcReviewRIS.SelectedTabPageIndex = 6 Then
                Load_ReviewRIS_Config()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Function GetReviewRISDataForTech(techType As String) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@SI_ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))},
            New String() {"@TechType", Chr(39) & techType & Chr(39)}
        }
        strConnection = GetSQL(8236, parray)(0)
        sqlParam = GetSQL(8236, parray)(1)
        Return DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub Load_ReviewRIS_2G()
        Dim dt As DataTable = GetReviewRISDataForTech("2G")
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridRevRIS2G, gvRevRIS2G, dt, "ALL", Nothing, Nothing)
            xtcReviewRIS.TabPages(0).Appearance.Header.BackColor = Nothing
        Else
            IOSDevExpressGrid.ClearGrid(gridRevRIS2G)
            xtcReviewRIS.TabPages(0).Appearance.Header.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub Load_ReviewRIS_3G()
        RemoveHandler gvRevRIS3G.CellValueChanged, AddressOf gvRevRIS3G_CellValueChanged
        RemoveHandler gvRevRIS3G.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
        Dim dt As DataTable = GetReviewRISDataForTech("3G")
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridRevRIS3G, gvRevRIS3G, dt, "ALL", Nothing, Nothing)
            xtcReviewRIS.TabPages(1).Appearance.Header.BackColor = Nothing
        Else
            IOSDevExpressGrid.ClearGrid(gridRevRIS3G)
            xtcReviewRIS.TabPages(1).Appearance.Header.BackColor = Color.Yellow
        End If
        AddHandler gvRevRIS3G.CellValueChanged, AddressOf gvRevRIS3G_CellValueChanged
        AddHandler gvRevRIS3G.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
    End Sub


    Private Sub Load_ReviewRIS_4G()
        RemoveHandler gvRevRIS4G.CellValueChanged, AddressOf gvRevRIS4G_CellValueChanged
        RemoveHandler gvRevRIS4G.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
        Dim dt As DataTable = GetReviewRISDataForTech("4G")
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridRevRIS4G, gvRevRIS4G, dt, "ALL", Nothing, Nothing)
            xtcReviewRIS.TabPages(2).Appearance.Header.BackColor = Nothing
        Else
            IOSDevExpressGrid.ClearGrid(gridRevRIS4G)
            xtcReviewRIS.TabPages(2).Appearance.Header.BackColor = Color.Yellow
        End If
        AddHandler gvRevRIS4G.CellValueChanged, AddressOf gvRevRIS4G_CellValueChanged
        AddHandler gvRevRIS4G.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
    End Sub

    Private Sub Load_ReviewRIS_5G()
        RemoveHandler gvRevRIS5G.CellValueChanged, AddressOf gvRevRIS5G_CellValueChanged
        RemoveHandler gvRevRIS5G.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
        Dim dt As DataTable = GetReviewRISDataForTech("5G")
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridRevRIS5G, gvRevRIS5G, dt, "ALL", Nothing, Nothing)
            xtcReviewRIS.TabPages(3).Appearance.Header.BackColor = Nothing
        Else
            IOSDevExpressGrid.ClearGrid(gridRevRIS5G)
            xtcReviewRIS.TabPages(3).Appearance.Header.BackColor = Color.Yellow
        End If
        AddHandler gvRevRIS5G.CellValueChanged, AddressOf gvRevRIS5G_CellValueChanged
        AddHandler gvRevRIS5G.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
    End Sub

    Private Sub Load_ReviewRIS_IP()
        RemoveHandler gvRevRISIP.CellValueChanged, AddressOf gvRevRISIP_CellValueChanged
        RemoveHandler gvRevRISIP.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
        Dim dt As DataTable = GetReviewRISDataForTech("IP")
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridRevRISIP, gvRevRISIP, dt, "ALL", Nothing, Nothing)
            xtcReviewRIS.TabPages(4).Appearance.Header.BackColor = Nothing
        Else
            IOSDevExpressGrid.ClearGrid(gridRevRISIP)
            xtcReviewRIS.TabPages(4).Appearance.Header.BackColor = Color.Yellow
        End If
        AddHandler gvRevRISIP.CellValueChanged, AddressOf gvRevRISIP_CellValueChanged
        AddHandler gvRevRISIP.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
    End Sub

    Private Sub Load_ReviewRIS_BB()
        RemoveHandler gvRevRISBB.CellValueChanged, AddressOf gvRevRISBB_CellValueChanged
        RemoveHandler gvRevRISBB.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
        Dim dt As DataTable = GetReviewRISDataForTech("BB")
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridRevRISBB, gvRevRISBB, dt, "ALL", Nothing, Nothing)
            xtcReviewRIS.TabPages(5).Appearance.Header.BackColor = Nothing
        Else
            IOSDevExpressGrid.ClearGrid(gridRevRISBB)
            xtcReviewRIS.TabPages(5).Appearance.Header.BackColor = Color.Yellow
        End If
        AddHandler gvRevRISBB.CellValueChanged, AddressOf gvRevRISBB_CellValueChanged
        AddHandler gvRevRISBB.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
    End Sub

    Private Sub Load_ReviewRIS_Config()
        RemoveHandler gvRevRISConfig.CellValueChanged, AddressOf gvRevRISConfig_CellValueChanged
        RemoveHandler gvRevRISConfig.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
        Dim dt As DataTable = GetReviewRISDataForTech("CONFIG")
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridRevRISConfig, gvRevRISConfig, dt, "ALL", Nothing, Nothing)
            xtcReviewRIS.TabPages(6).Appearance.Header.BackColor = Nothing
        Else
            IOSDevExpressGrid.ClearGrid(gridRevRISConfig)
            xtcReviewRIS.TabPages(6).Appearance.Header.BackColor = Color.Yellow
        End If
        AddHandler gvRevRISConfig.CellValueChanged, AddressOf gvRevRISConfig_CellValueChanged
        AddHandler gvRevRISConfig.ShowingEditor, AddressOf gvRevRIS_ShowingEditor
    End Sub

    Private Sub gvRevRIS_ShowingEditor(sender As Object, e As CancelEventArgs)
        Try
            Dim gv As GridView = DirectCast(sender, GridView)
            If gv.Name.Contains("3G") Or gv.Name.Contains("4G") Or gv.Name.Contains("5G") Then
                If gv.FocusedColumn.FieldName = "SI_ProjectID" Or gv.FocusedColumn.FieldName = "CELL_NAME" Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            ElseIf gv.Name.Contains("IP") Then
                If gv.FocusedColumn.FieldName = "SI_ProjectID" Or gv.FocusedColumn.FieldName = "baseband" Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            ElseIf gv.Name.Contains("BB") Then
                If gv.FocusedColumn.FieldName = "SI_ProjectID" Or gv.FocusedColumn.FieldName = "BASE_BAND_NAME" Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            ElseIf gv.Name.Contains("Config") Then
                If gv.FocusedColumn.FieldName = "SI_ProjectID" Or gv.FocusedColumn.FieldName = "SI_FileID" Or gv.FocusedColumn.FieldName = "BASEBAND" Or gv.FocusedColumn.FieldName = "CONFIGURATION_PACKAGE" Or
                   gv.FocusedColumn.FieldName = "AntennaUnitGroupId" Or gv.FocusedColumn.FieldName = "AuPortId" Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub gvRevRIS3G_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gv As GridView = DirectCast(sender, GridView)
            Dim colValue As String = Nothing
            If e.Column.ColumnType.FullName = "System.Int32" Then
                colValue = CInt(e.Value)
            ElseIf e.Column.ColumnType.FullName = "System.Double" Then
                colValue = CDbl(e.Value)
            Else
                colValue = Chr(39) & CStr(e.Value) & Chr(39)
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ColumnName", "[" & e.Column.FieldName & "]"},
                New String() {"@ColumnValue", colValue},
                New String() {"@SI_ProjectID", CInt(gv.GetFocusedRowCellValue("SI_ProjectID"))},
                New String() {"@CELL_NAME", Chr(39) & CStr(gv.GetFocusedRowCellValue("CELL_NAME")) & Chr(39)}
            }
            strConnection = GetSQL(8228, parray)(0)
            sqlParam = GetSQL(8228, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvRevRIS4G_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gv As GridView = DirectCast(sender, GridView)
            Dim colValue As String = Nothing
            If e.Column.ColumnType.FullName = "System.Int32" Then
                colValue = CInt(e.Value)
            ElseIf e.Column.ColumnType.FullName = "System.Double" Then
                colValue = CDbl(e.Value)
            Else
                colValue = Chr(39) & CStr(e.Value) & Chr(39)
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ColumnName", "[" & e.Column.FieldName & "]"},
                New String() {"@ColumnValue", colValue},
                New String() {"@SI_ProjectID", CInt(gv.GetFocusedRowCellValue("SI_ProjectID"))},
                New String() {"@CELL_NAME", Chr(39) & CStr(gv.GetFocusedRowCellValue("CELL_NAME")) & Chr(39)}
            }
            strConnection = GetSQL(8229, parray)(0)
            sqlParam = GetSQL(8229, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvRevRIS5G_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gv As GridView = DirectCast(sender, GridView)
            Dim colValue As String = Nothing
            If e.Column.ColumnType.FullName = "System.Int32" Then
                colValue = CInt(e.Value)
            ElseIf e.Column.ColumnType.FullName = "System.Double" Then
                colValue = CDbl(e.Value)
            Else
                colValue = Chr(39) & CStr(e.Value) & Chr(39)
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ColumnName", "[" & e.Column.FieldName & "]"},
                New String() {"@ColumnValue", colValue},
                New String() {"@SI_ProjectID", CInt(gv.GetFocusedRowCellValue("SI_ProjectID"))},
                New String() {"@CELL_NAME", Chr(39) & CStr(gv.GetFocusedRowCellValue("CELL_NAME")) & Chr(39)}
            }
            strConnection = GetSQL(8230, parray)(0)
            sqlParam = GetSQL(8230, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvRevRISIP_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gv As GridView = DirectCast(sender, GridView)
            Dim colValue As String = Nothing
            If e.Column.ColumnType.FullName = "System.Int32" Then
                colValue = CInt(e.Value)
            ElseIf e.Column.ColumnType.FullName = "System.Double" Then
                colValue = CDbl(e.Value)
            Else
                colValue = Chr(39) & CStr(e.Value) & Chr(39)
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ColumnName", "[" & e.Column.FieldName & "]"},
                New String() {"@ColumnValue", colValue},
                New String() {"@SI_ProjectID", CInt(gv.GetFocusedRowCellValue("SI_ProjectID"))},
                New String() {"@baseband", Chr(39) & CStr(gv.GetFocusedRowCellValue("baseband")) & Chr(39)}
            }
            strConnection = GetSQL(8231, parray)(0)
            sqlParam = GetSQL(8231, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvRevRISBB_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gv As GridView = DirectCast(sender, GridView)
            Dim colValue As String = Nothing
            If e.Column.ColumnType.FullName = "System.Int32" Then
                colValue = CInt(e.Value)
            ElseIf e.Column.ColumnType.FullName = "System.Double" Then
                colValue = CDbl(e.Value)
            Else
                colValue = Chr(39) & CStr(e.Value) & Chr(39)
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ColumnName", "[" & e.Column.FieldName & "]"},
                New String() {"@ColumnValue", colValue},
                New String() {"@SI_ProjectID", CInt(gv.GetFocusedRowCellValue("SI_ProjectID"))},
                New String() {"@BASE_BAND_NAME", Chr(39) & CStr(gv.GetFocusedRowCellValue("BASE_BAND_NAME")) & Chr(39)}
            }
            strConnection = GetSQL(8232, parray)(0)
            sqlParam = GetSQL(8232, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvRevRISConfig_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gv As GridView = DirectCast(sender, GridView)
            Dim colValue As String = Nothing
            If e.Column.ColumnType.FullName = "System.Int32" Then
                colValue = CInt(e.Value)
            ElseIf e.Column.ColumnType.FullName = "System.Double" Then
                colValue = CDbl(e.Value)
            Else
                colValue = Chr(39) & CStr(e.Value) & Chr(39)
            End If

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@ColumnName", "[" & e.Column.FieldName & "]"},
                New String() {"@ColumnValue", colValue},
                New String() {"@SI_ProjectID", CInt(gv.GetFocusedRowCellValue("SI_ProjectID"))},
                New String() {"@SI_FileID", CInt(gv.GetFocusedRowCellValue("SI_FileID"))},
                New String() {"@BASEBAND", Chr(39) & CStr(gv.GetFocusedRowCellValue("BASEBAND")) & Chr(39)},
                New String() {"@CONFIGURATION_PACKAGE", Chr(39) & CStr(gv.GetFocusedRowCellValue("CONFIGURATION_PACKAGE")) & Chr(39)},
                New String() {"@AntennaUnitGroupId", Chr(39) & CStr(gv.GetFocusedRowCellValue("AntennaUnitGroupId")) & Chr(39)},
                New String() {"@AuPortId", CDbl(gv.GetFocusedRowCellValue("AuPortId"))}
            }
            strConnection = GetSQL(8233, parray)(0)
            sqlParam = GetSQL(8233, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Validate"

    Private Sub btnRunValidate_Click(sender As Object, e As EventArgs) Handles btnRunValidate.Click
        Dim dt As DataTable = Nothing
        Try
            lblValidateStatus.Text = "Validation started"
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@SI_ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))}
            }
            strConnection = GetSQL(8218, parray)(0)
            sqlParam = GetSQL(8218, parray)(1)
            dt = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(gridValidateCheckResults, gvValidateCheckResults, dt, "ALL", Nothing, Nothing)
                lblValidateStatus.Text = dt.Rows.Count.ToString & " inconsistencies found"
                dtValidationErrors = dt.DistinctCol("ValidationID")
            Else
                IOSDevExpressGrid.ClearGrid(gridValidateCheckResults)
            End If
            gvValidationsCheck.Focus()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            If dt.Rows.Count = 0 Then
                lblValidateStatus.Text = "No inconsistencies found"
            End If
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadValidationChecks()
        RemoveHandler gvValidationsCheck.RowStyle, AddressOf gvValidationsCheck_RowStyle

        Dim parray()() As String = Nothing
        Dim strConnection As String = GetSQL(8212, parray)(0)
        Dim sqlParam As String = GetSQL(8212, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(gridValidationsCheck, gvValidationsCheck, dt, "ALL", {"ValidationID"}, "ValidationName")

        Dim riChkActive As RepositoryItemCheckEdit = TryCast(gridValidationsCheck.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
        riChkActive.CheckStyle = CheckStyles.Standard
        riChkActive.AllowGrayed = False
        riChkActive.NullStyle = StyleIndeterminate.Unchecked
        gvValidationsCheck.Columns("IsActive").ColumnEdit = riChkActive
        AddHandler riChkActive.CheckedChanged, AddressOf riChkActive_CheckedChanged

        AddHandler gvValidationsCheck.RowStyle, AddressOf gvValidationsCheck_RowStyle
    End Sub

    Private Sub gvValidationsCheck_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            If (e IsNot Nothing) AndAlso (e.RowHandle >= 0) Then
                If (gvValidationsCheck.RowCount > 0) AndAlso (dtValidationErrors IsNot Nothing) AndAlso (dtValidationErrors.Rows.Count > 0) Then
                    If (dtValidationErrors.Select("ValidationID=" & gvValidationsCheck.GetRowCellValue(e.RowHandle, "ValidationID")).Count > 0) AndAlso (gvValidationsCheck.GetRowCellValue(e.RowHandle, "IsActive") = True) Then
                        e.Appearance.BackColor = Color.Orange
                    Else
                        e.Appearance.BackColor = Nothing
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub gvValidationsCheck_ShowingEditor(sender As Object, e As CancelEventArgs) Handles gvValidationsCheck.ShowingEditor
        Try
            If gvValidationsCheck.FocusedColumn().FieldName = "IsActive" Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch
        End Try
    End Sub

    Private Sub riChkActive_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim chkBox As CheckEdit = TryCast(sender, CheckEdit)
            If chkBox IsNot Nothing Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@ValidationID", CInt(gvValidationsCheck.GetFocusedRowCellValue("ValidationID"))},
                    New String() {"@IsActive", IIf(chkBox.Checked = True, 1, 0)}
                }
                strConnection = GetSQL(8220, parray)(0)
                sqlParam = GetSQL(8220, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                gvValidationsCheck.SelectRow(gvValidationsCheck.FocusedRowHandle)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadValidationResults()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(8209, parray)(0)
        sqlParam = GetSQL(8209, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOSDevExpressGrid.PopulateDataInGrid(gridValidateCheckResults, gvValidateCheckResults, dt, "ALL", Nothing, Nothing)
    End Sub

#End Region

#Region "Generate"

    Private Sub LoadGenFilesList()
        RemoveHandler gvGenFilesList.FocusedRowChanged, AddressOf gvGenFilesList_FocusedRowChanged
        RemoveHandler gvGenFilesList.RowStyle, AddressOf gvGenFilesList_RowStyle
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@SI_ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))}
        }
        strConnection = GetSQL(8214, parray)(0)
        sqlParam = GetSQL(8214, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridGenFilesList, gvGenFilesList, dt, "ALL", Nothing, "FileConfigName")
            gvGenFilesList_FocusedRowChanged(gvGenFilesList, Nothing)
        Else
            IOSDevExpressGrid.ClearGrid(gridGenFilesList)
        End If
        AddHandler gvGenFilesList.FocusedRowChanged, AddressOf gvGenFilesList_FocusedRowChanged
        AddHandler gvGenFilesList.RowStyle, AddressOf gvGenFilesList_RowStyle
    End Sub

    Private Sub gvGenFilesList_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            If e IsNot Nothing Then
                If (gvGenFilesList.RowCount > 0) AndAlso (dtXmlFiles IsNot Nothing) AndAlso (dtXmlFiles.Rows.Count > 0) Then
                    If dtXmlFiles.Select("FileID=" & gvGenFilesList.GetRowCellValue(e.RowHandle, "FileConfigID")).Count > 0 Then
                        e.Appearance.BackColor = Color.Yellow
                    Else
                        e.Appearance.BackColor = Nothing
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub LoadGenStatus()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))}
        }
        strConnection = GetSQL(8224, parray)(0)
        sqlParam = GetSQL(8224, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gridGenStatus, gvGenStatus, dt, "ALL", Nothing, Nothing)
        Else
            IOSDevExpressGrid.ClearGrid(gridGenStatus)
        End If
    End Sub

    Private Sub gvGenFilesList_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If gvGenFilesList.RowCount > 0 Then
                LoadGenTranslationData()
                LoadXmlFile()
                LoadGenStatus()
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadXmlFile()
        RemoveHandler txtXMLFile.EditValueChanged, AddressOf txtXMLFile_EditValueChanged
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@SI_ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))},
            New String() {"@FileID", CInt(gvGenFilesList.GetFocusedRowCellValue("FileConfigID"))}
        }
        strConnection = GetSQL(8217, parray)(0)
        sqlParam = GetSQL(8217, parray)(1)
        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim xmlPrettyPrint As String = Nothing
        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("FileXML").ToString.Contains("]]>]]>") Then
                xmlPrettyPrint = dt.Rows(0)("FileXML").ToString
            Else
                xmlPrettyPrint = PrettyXml(dt.Rows(0)("FileXML").ToString)
            End If
            txtXMLFile.Text = xmlPrettyPrint.ToString
        Else
            txtXMLFile.Text = String.Empty
        End If
        btnSaveXML.Appearance.BackColor = Nothing
        AddHandler txtXMLFile.EditValueChanged, AddressOf txtXMLFile_EditValueChanged
    End Sub

    Private Sub LoadGenTranslationData()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        If CStr(gvGenFilesList.GetFocusedRowCellValue("FileConfigName")).Contains("SiteEquipment") Then
            Dim parray()() As String = {
                New String() {"@SI_ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))}
            }
            strConnection = GetSQL(8223, parray)(0)
            sqlParam = GetSQL(8223, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            If dt.Rows.Count > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(gridGenTranslationGrid, gvGenTranslationGrid, dt, "ALL", Nothing, Nothing)
            Else
                IOSDevExpressGrid.ClearGrid(gridGenTranslationGrid)
            End If
        Else
            Dim parray()() As String = {
                New String() {"@SI_ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))},
                New String() {"@FileID", CInt(gvGenFilesList.GetFocusedRowCellValue("FileConfigID"))}
            }
            strConnection = GetSQL(8215, parray)(0)
            sqlParam = GetSQL(8215, parray)(1)
            Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            If dt.Rows.Count > 0 Then
                IOSDevExpressGrid.PopulateDataInGrid(gridGenTranslationGrid, gvGenTranslationGrid, dt, "ALL", Nothing, "FieldValue")
            Else
                IOSDevExpressGrid.ClearGrid(gridGenTranslationGrid)
            End If
        End If
    End Sub

    Private Sub btnLoadGrids_Click(sender As Object, e As EventArgs) Handles btnLoadGrids.Click
        Try
            lblGenStatus2.Text = "Loading Grids Started"

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@SI_ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))}
            }
            strConnection = GetSQL(8213, parray)(0)
            sqlParam = GetSQL(8213, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            xtcGenerate_TabIndexChanged(xtcGenerate, Nothing)
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            lblGenStatus2.Text = "Loading Grids Completed"
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnGenerateXML_Click(sender As Object, e As EventArgs) Handles btnGenerateXML.Click
        Try
            lblStatus.Text = "Generating XML...This can take a few minutes. Press Refresh Status button to update status."

            If btnGenerateXML.Text = "Generate XML" Then
                btnGenerateXML.Text = "Abort"
                xtcGenerate.SelectedTabPageIndex = 2
            ElseIf btnGenerateXML.Text = "Abort" Then
                objGenXMLThread.Abort()
                btnGenerateXML.Text = "Generate XML"
                lblStatus.Text = ""
                Exit Sub
            End If

            Dim objGenXML As New GenerateXMLClass()
            objGenXML.SI_ProjectID = CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))
            AddHandler objGenXML.ThreadComplete, AddressOf ExecuteAfterGenXMLThreadComplete
            objGenXMLThread = New Threading.Thread(AddressOf objGenXML.GenerateXMlFile)
            objGenXMLThread.Start()
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfterGenXMLThreadComplete(ti As Thread)
        If objGenXMLThread.ThreadState = Threading.ThreadState.AbortRequested Then
            Exit Sub
        End If

        SyncLock objGenXMlThreadLock
            Me.BeginInvoke(New CallThreadInvokedGenXMLFile(AddressOf ShowXMLFile))
        End SyncLock
    End Sub

    Private Sub ShowXMLFile()
        If objGenXMLThread.ThreadState = Threading.ThreadState.AbortRequested Then
            Exit Sub
        End If

        SyncLock objGenXMlThreadLock
            LoadXmlFile()
        End SyncLock

        btnGenerateXML.Text = "Generate XML"
        lblStatus.Text = ""
    End Sub

    Private Shared Function PrettyXml(ByVal xml As String) As String
        Dim stringBuilder = New StringBuilder()
        Dim element = XElement.Parse(xml)
        Dim settings = New XmlWriterSettings()
        settings.OmitXmlDeclaration = True
        settings.Indent = True
        settings.NewLineOnAttributes = True

        Using xmlWrite = XmlWriter.Create(stringBuilder, settings)
            element.Save(xmlWrite)
        End Using
        Return stringBuilder.ToString()
    End Function

    Private Sub btnGeneratePackage_Click(sender As Object, e As EventArgs) Handles btnGeneratePackage.Click
        Try
            dtXmlFiles = New DataTable()
            dtXmlFiles.Columns.Add("FileID", GetType(Integer))
            Dim drXmlFiles As DataRow = Nothing

            Dim objXFBD As New XtraFolderBrowserDialog()
            objXFBD.SelectedPath = IO.Directory.GetCurrentDirectory()

            If objXFBD.ShowDialog() = DialogResult.OK Then

                lblGenStatus2.Text = "XML files package generation started"
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim filePath As String = objXFBD.SelectedPath
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))}
                }
                strConnection = GetSQL(8221, parray)(0)
                sqlParam = GetSQL(8221, parray)(1)
                Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                Dim dtBBU As DataTable = dt.DistinctCol("BBU")
                Dim di As DirectoryInfo = Nothing
                Dim diSub As DirectoryInfo = Nothing

                'Iterating through each BBU (Base Band)
                For Each drBBU As DataRow In dtBBU.Rows

                    Dim dtBBUFiles As DataTable = dt.Select("BBU='" & drBBU("BBU") & "'").CopyToDataTable
                    Dim zipFilePath As String = filePath & "/" & drBBU("BBU").ToString & ".zip"

                    'create BBU named main folder for project info file
                    Dim bbuDirectoryName As String = filePath & "/" & drBBU("BBU").ToString
                    If Not Directory.Exists(bbuDirectoryName) Then
                        di = Directory.CreateDirectory(bbuDirectoryName)
                        Dim ds As DirectorySecurity = di.GetAccessControl()
                        ds.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.CreateFiles, AccessControlType.Allow))
                        di.SetAccessControl(ds)
                    End If

                    'create BBU named sub folder for other files
                    Dim bbuSubDirectoryName As String = filePath & "/" & drBBU("BBU").ToString & "/" & drBBU("BBU").ToString
                    If Not Directory.Exists(bbuSubDirectoryName) Then
                        diSub = Directory.CreateDirectory(bbuSubDirectoryName)
                        Dim dsSub As DirectorySecurity = diSub.GetAccessControl()
                        dsSub.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.CreateFiles, AccessControlType.Allow))
                        diSub.SetAccessControl(dsSub)
                    End If

                    For Each drBBUFiles As DataRow In dtBBUFiles.Rows
                        If drBBUFiles("FileName").ToString.Contains("ProjectInfo") Then

                            If IsDBNull(drBBUFiles("FileName")) Or IsDBNull(drBBUFiles("FileXML")) Then
                                drXmlFiles = dtXmlFiles.NewRow()
                                drXmlFiles("FileID") = drBBUFiles("FileID")
                                dtXmlFiles.Rows.Add(drXmlFiles)
                            Else
                                Dim fp = Path.Combine(bbuDirectoryName, drBBUFiles("FileName").ToString)
                                Using writer = New StreamWriter(fp, False)
                                    writer.Write(drBBUFiles("FileXML").ToString)
                                End Using
                            End If

                        ElseIf drBBUFiles("FileName").ToString.Contains("DeltaRNC") Then

                            If IsDBNull(drBBUFiles("FileName")) Or IsDBNull(drBBUFiles("FileXML")) Then
                                drXmlFiles = dtXmlFiles.NewRow()
                                drXmlFiles("FileID") = drBBUFiles("FileID")
                                dtXmlFiles.Rows.Add(drXmlFiles)
                            Else
                                Dim diDR As DirectoryInfo = Nothing
                                Dim zipFilePathDeltaRNC As String = filePath & "/" & drBBUFiles("FileName").ToString.Split(".")(0) & ".zip"
                                'create delta rnc folder
                                Dim srcFilePathDeltaRNC As String = filePath & "/" & drBBUFiles("FileName").ToString.Split(".")(0)
                                If Not Directory.Exists(srcFilePathDeltaRNC) Then
                                    diDR = Directory.CreateDirectory(srcFilePathDeltaRNC)
                                    Dim dsDR As DirectorySecurity = diDR.GetAccessControl()
                                    dsDR.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.CreateFiles, AccessControlType.Allow))
                                    diDR.SetAccessControl(dsDR)
                                End If

                                Dim fp = Path.Combine(srcFilePathDeltaRNC, drBBUFiles("FileName").ToString)
                                Using writer As New StreamWriter(fp, False)
                                    writer.Write(drBBUFiles("FileXML").ToString)
                                End Using

                                ZipFile.CreateFromDirectory(srcFilePathDeltaRNC, zipFilePathDeltaRNC, CompressionLevel.Optimal, False)
                                diDR.Delete(True)
                            End If

                        Else

                            If IsDBNull(drBBUFiles("FileName")) Or IsDBNull(drBBUFiles("FileXML")) Then
                                drXmlFiles = dtXmlFiles.NewRow()
                                drXmlFiles("FileID") = drBBUFiles("FileID")
                                dtXmlFiles.Rows.Add(drXmlFiles)
                            Else
                                Dim fp = Path.Combine(bbuSubDirectoryName, drBBUFiles("FileName").ToString)
                                Using writer As New StreamWriter(fp, False)
                                    writer.Write(drBBUFiles("FileXML").ToString)
                                End Using
                            End If

                        End If
                    Next

                    ZipFile.CreateFromDirectory(bbuDirectoryName, zipFilePath, CompressionLevel.Optimal, False)
                    diSub.Delete(True)
                    di.Delete(True)

                Next
            End If

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            gvGenFilesList.Focus()
            lblGenStatus2.Text = "XML files package generation completed"
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvGenTranslationGrid_ShowingEditor(sender As Object, e As CancelEventArgs) Handles gvGenTranslationGrid.ShowingEditor
        Try
            Dim gv As GridView = TryCast(sender, GridView)
            If (gv.FocusedColumn().FieldName = "FieldValue") Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvGenTranslationGrid_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles gvGenTranslationGrid.CellValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If e.Column.FieldName = "FieldValue" Then
                Dim gv As GridView = DirectCast(sender, GridView)
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))},
                    New String() {"@TemplateID", CInt(gvProject.GetFocusedRowCellValue("SI_TemplateID"))},
                    New String() {"@FileID", CInt(gv.GetFocusedRowCellValue("FileID"))},
                    New String() {"@KeyField", Chr(39) & CStr(gv.GetFocusedRowCellValue("KeyField")) & Chr(39)},
                    New String() {"@FieldName", Chr(39) & CStr(gv.GetFocusedRowCellValue("FieldName")) & Chr(39)},
                    New String() {"@FieldValue", Chr(39) & CStr(gv.GetFocusedRowCellValue("FieldValue")) & Chr(39)}
                }
                strConnection = GetSQL(8219, parray)(0)
                sqlParam = GetSQL(8219, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRefreshStatus_Click(sender As Object, e As EventArgs) Handles btnRefreshStatus.Click
        Try
            LoadGenStatus()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub txtXMLFile_EditValueChanged(sender As Object, e As EventArgs)
        Try
            btnSaveXML.Appearance.BackColor = Color.OrangeRed
        Catch
        End Try
    End Sub

    Private Sub btnSaveXML_Click(sender As Object, e As EventArgs) Handles btnSaveXML.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If txtXMLFile.Text <> String.Empty Then
                Dim strConnection As String = Nothing
                Dim sqlParam As String = Nothing
                Dim parray()() As String = {
                    New String() {"@FileXML", Chr(39) & txtXMLFile.Text.Trim & Chr(39)},
                    New String() {"@ProjectID", CInt(gvProject.GetFocusedRowCellValue("SI_ProjectID"))},
                    New String() {"@FileID", CInt(gvGenFilesList.GetFocusedRowCellValue("FileConfigID"))}
                }
                strConnection = GetSQL(8225, parray)(0)
                sqlParam = GetSQL(8225, parray)(1)
                DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            lblGenStatus2.Text = "XML file saved successfully"
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            btnSaveXML.Appearance.BackColor = Nothing
        End Try
    End Sub

    Private Sub xtcGenerate_TabIndexChanged(sender As Object, e As EventArgs) Handles xtcGenerate.TabIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If xtcGenerate.SelectedTabPageIndex = 0 Then
                LoadGenTranslationData()
            ElseIf xtcGenerate.SelectedTabPageIndex = 1 Then
                LoadXmlFile()
            ElseIf xtcGenerate.SelectedTabPageIndex = 2 Then
                LoadGenStatus()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

End Class

Class GenerateXMLClass

    Public SI_ProjectID As Integer
    Public Event ThreadComplete(ti As Threading.Thread)

    Sub GenerateXMlFile()
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@SI_ProjectID", SI_ProjectID}
            }
            strConnection = GetSQL(8216, parray)(0)
            sqlParam = GetSQL(8216, parray)(1)
            DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, 1800)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            RaiseEvent ThreadComplete(Threading.Thread.CurrentThread)
        End Try
    End Sub

End Class