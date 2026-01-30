Imports IOS.Library
Imports IOS.DataLibrary
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports System.ComponentModel
Imports dotnetCHARTING.WinForms
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository

Public Class frmTiltManagement

#Region "Variables"

    Private cmsSourceControl As GridControl = Nothing

    'Ad Hoc Tilt Manager
    Private ftr As MapInfo.Data.Feature
    Private tiltvalue As Double
    Private cluttervalue As Double
    Private tiltvalueDefault As Double
    Private cluttervalueDefault As Double
    Private cellid As String
    Private cellid_x As String
    Private cellid_y As String
    Private cellid_az As Double
    Private cellid_rc As Double
    Private cellid_ant As String
    Private cellid_et As Double
    Private cellid_mt As Double
    Private recordid As Double
    Private selectedMbtsName As String = Nothing
    Dim an_clutter As Annotation = Nothing
    Dim an_DownClutter As Annotation = Nothing
    Dim an_UpClutter As Annotation = Nothing

    Private riCmbIncInPlan As RepositoryItemComboBox
    Private riCmbRule As RepositoryItemComboBox
    Private dtTreeData As New DataTable
    Private dtValidationData As DataTable = Nothing

    Public selectedCellsOnMap As String = Nothing
    Public dynCellNameColumnsCount As Integer = 0
    Dim imgListValidation As New ImageList

    'MML
    Private dtMmlUserFilter As DataTable = Nothing
    Private dsGetMml As DataSet = Nothing
    Private dsGetMmlRollback As DataSet = Nothing

    'Bulk
    Private dtCampaignsBulk As DataTable = Nothing
    Private dtBulkImport As DataTable = Nothing
    Private objThreadBulk As System.Threading.Thread
    Private Delegate Sub CallThreadInvokedBulk(Row As DataRow, Status As Integer)
    Private objThreadLockBulk As New Object
    Private IsErrorInCopy As Boolean = False
    Private openFileDirectory As String = Nothing

    'Audit
    Private dtCampaignsAudit As DataTable = Nothing
    Private objThreadAudit As System.Threading.Thread
    Private Delegate Sub CallThreadInvokedAudit(Row As DataRow, Status As Integer)
    Private objThreadLockAudit As New Object

#End Region

#Region "Ad Hoc Tilt Manager Properties"

    Public Property tp_cellid As String
        Get
            Return cellid
        End Get
        Set(ByVal value As String)
            cellid = value
        End Set
    End Property

    Public Property tp_cellidx As String
        Get
            Return cellid_x
        End Get
        Set(ByVal value As String)
            cellid_x = value
        End Set
    End Property

    Public Property tp_cellidy As String
        Get
            Return cellid_y
        End Get
        Set(ByVal value As String)
            cellid_y = value
        End Set
    End Property

    Public Property tp_cellid_az As Double
        Get
            Return cellid_az
        End Get
        Set(ByVal value As Double)
            cellid_az = value
        End Set
    End Property

    Public Property tp_cellid_rc As Double
        Get
            Return cellid_rc
        End Get
        Set(ByVal value As Double)
            cellid_rc = value
        End Set
    End Property

    Public Property tp_recordid As Double
        Get
            Return recordid
        End Get
        Set(ByVal value As Double)
            recordid = value
        End Set
    End Property

    Public Property tp_cellid_et As Double
        Get
            Return cellid_et
        End Get
        Set(ByVal value As Double)
            cellid_et = value
        End Set
    End Property

    Public Property tp_cellid_mt As Double
        Get
            Return cellid_mt
        End Get
        Set(ByVal value As Double)
            cellid_mt = value
        End Set
    End Property

    Public Property tp_cellid_ant As String
        Get
            Return cellid_ant
        End Get
        Set(ByVal value As String)
            cellid_ant = value
        End Set
    End Property

    Public Property CustomTilt As Double
        Get
            Return tiltvalue
        End Get
        Set(ByVal value As Double)
            tiltvalue = value
        End Set
    End Property

    Public Property CustomClutter As Double
        Get
            Return cluttervalue
        End Get
        Set(ByVal value As Double)
            cluttervalue = value

        End Set
    End Property

    Public Property CustomTiltDefault As Double
        Get
            Return tiltvalueDefault
        End Get
        Set(ByVal value As Double)
            tiltvalueDefault = value
        End Set
    End Property

    Public Property CustomClutterDefault As Double
        Get
            Return cluttervalueDefault
        End Get
        Set(ByVal value As Double)
            cluttervalueDefault = value
        End Set
    End Property

#End Region

#Region "Events"

    Private Sub frmTiltManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.SuspendLayout()

            HideDescriptionArea(layerPropGridBulk)

            LoadCellList()
            LoadLayers()
            LoadBandList()
            LoadTiltRule()

            If tiltMngrType = "TMADHOC" Then
                xtcMain.SelectedTabPageIndex = 0
            ElseIf tiltMngrType = "TMBULK" Then
                xtcMain.SelectedTabPageIndex = 1
            End If
            xtcMain_SelectedPageChanged(Nothing, Nothing)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub xtcMain_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtcMain.SelectedPageChanged
        Try
            If xtcMain.SelectedTabPageIndex = 0 Then
                ch_TiltManager.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
                clsIOSImageList.GetTiltTreeImages(imgListValidation)
                ManageButtons(False)
                LoadTiltCampaigns()
            ElseIf xtcMain.SelectedTabPageIndex = 1 Then
                LoadCampaignsBulk()
            ElseIf xtcMain.SelectedTabPageIndex = 2 Then
                LoadCampaignsAudit()
            ElseIf xtcMain.SelectedTabPageIndex = 3 Then
                LoadTiltMMLCampaign()
                LoadMmlConfiguration()
                gvMmlCampaign_FocusedRowChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        lblStatus.Text = ""
        lblStatus.Visible = False
        RemoveHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer1.Enabled = False
        Timer1.Stop()
        Me.Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

#Region "Ad Hoc Tilt Manager Events"

    Private Sub ch_TiltManager_Click(ByVal sender As Object, ByVal e As MouseEventArgs)
        Try
            Dim xval As [Object] = Nothing
            Dim hitchart As HitTestInfo = ch_TiltManager.HitTest(e.Location)
            If TypeOf hitchart.Object Is Element Then
                Dim el As Element = CType(hitchart.Object, Element)
                xval = el.Name
                Try
                    frmMapWindow.Location_Map("AntennaTiltClick", CDbl(xval.Split(";")(1)), CDbl(xval.Split(";")(0)))
                Catch ex As Exception

                End Try
            ElseIf TypeOf hitchart.Object Is Annotation Then
                Dim an As Annotation = CType(hitchart.Object, Annotation)
                If an.Label.Text = "Step" Then
                    If ch_TiltManager.Annotations(0).Label.Text = "+0.1" Then
                        ch_TiltManager.Annotations(0).Label.Text = "+1.0"
                        ch_TiltManager.Annotations(1).Label.Text = "-1.0"
                    Else
                        ch_TiltManager.Annotations(0).Label.Text = "+0.1"
                        ch_TiltManager.Annotations(1).Label.Text = "-0.1"
                    End If
                Else
                    Dim newtilt As Double = Nothing
                    Dim currenttilt As Double = ch_TiltManager.Tag(0)
                    Select Case an.Label.Text
                        Case "+0.1"
                            newtilt = currenttilt + CDbl(an.Label.Text.TrimStart("+"))
                        Case "-0.1"
                            newtilt = currenttilt + CDbl(an.Label.Text.TrimStart("+"))
                        Case "+3m"
                            Me.CustomClutter = cluttervalue + 3
                            newtilt = currenttilt
                        Case "-3m"
                            Me.CustomClutter = cluttervalue - 3
                            newtilt = currenttilt
                        Case "Reset"
                            cluttervalue = CustomClutterDefault
                            newtilt = tiltvalueDefault
                    End Select
                    frmMapWindow.TiltManager_Cell_AntennaTiltCoverage(cellid, cellid_x, cellid_y, cellid_ant, cellid_az, cellid_rc, cellid_et, cellid_mt, newtilt, cluttervalue)
                End If
                ch_TiltManager.Refresh()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ch_TiltManager_SizeChanged(sender As Object, e As EventArgs)
        Try
            an_clutter.Position = New System.Drawing.Point(ch_TiltManager.Width - 110, 2)
            an_DownClutter.Position = New System.Drawing.Point(ch_TiltManager.Width - 72, 2)
            an_UpClutter.Position = New System.Drawing.Point(ch_TiltManager.Width - 35, 2)
        Catch
        End Try
    End Sub

    Private Sub tlTiltManager_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs) Handles tlTiltManager.ValidatingEditor
        If (e.Value.ToString.ToLower = "select plan") Or (e.Value.ToString.ToLower = "select rule") Then
            e.Valid = False
        Else
            e.Valid = True
        End If
    End Sub

    Private Sub tlTiltManager_CustomNodeCellEdit(sender As Object, e As GetCustomNodeCellEditEventArgs)
        Try
            If e.Column.FieldName = "IncludeInPlan" Then
                e.RepositoryItem = riCmbIncInPlan
            ElseIf e.Column.FieldName = "Rule" Then
                e.RepositoryItem = riCmbRule
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlTiltManager_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles tlTiltManager.ShowingEditor
        Try
            RemoveHandler tlTiltManager.NodeCellStyle, AddressOf tlTiltManager_NodeCellStyle
            If (tlTiltManager.FocusedColumn.FieldName = "IncludeInPlan") Or (tlTiltManager.FocusedColumn.FieldName = "ETiltPlanned") Or (tlTiltManager.FocusedColumn.FieldName = "Rule") Then
                If (tlTiltManager.FocusedNode("DeviceName") = "") OrElse (tlTiltManager.GetRowCellValue(tlTiltManager.FocusedNode, tlTiltManager.FocusedColumn).ToString.ToUpper = "FORCE SYNCH") Then
                    If tlTiltManager.FocusedColumn.FieldName = "Rule" Then
                        If (tlTiltManager.FocusedNode.Level = 2) AndAlso (tlTiltManager.FocusedNode.ParentNode("Rule").ToString.ToUpper = "MASTER") Then
                            e.Cancel = False
                        Else
                            e.Cancel = True
                        End If
                    Else
                        e.Cancel = True
                    End If
                ElseIf tlTiltManager.FocusedColumn.FieldName = "ETiltPlanned" Then
                    If (tlTiltManager.FocusedNode("Rule").ToString.ToUpper = "MATCH TILT") Or (tlTiltManager.FocusedNode("Rule").ToString.ToUpper = "MATCH VBEAM") Or (tlTiltManager.FocusedNode("Rule").ToString.ToUpper = "LINKED") Then
                        e.Cancel = True
                    Else
                        e.Cancel = False
                    End If
                End If
            Else
                e.Cancel = True
            End If
            AddHandler tlTiltManager.NodeCellStyle, AddressOf tlTiltManager_NodeCellStyle
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlTiltManager_NodeCellStyle(ByVal sender As Object, ByVal e As GetCustomNodeCellStyleEventArgs)
        Try
            If Not e.Node.Tag Is Nothing Then
                e.Appearance.BackColor = e.Node.Tag
            End If
        Catch
        End Try
    End Sub

    Private Sub tlTiltManager_CellValueChanged(sender As Object, e As DevExpress.XtraTreeList.CellValueChangedEventArgs)
        Dim deviceNameLinkedTo As String = Nothing
        If e.Node.Level = 2 Then
            If e.Node("Rule").ToString.ToUpper = "MASTER" Then
                For Each nd As TreeListNode In e.Node.ParentNode.Nodes
                    If nd("Rule").ToString.ToUpper = "MASTER" Then
                        nd("Rule") = ""
                    End If
                Next
            End If
            e.Node("Rule") = "MASTER"
        ElseIf e.Node.Level = 1 Then
            If e.Node("Rule").ToString.ToUpper <> "MASTER" Then
                For Each nd As TreeListNode In e.Node.Nodes
                    If nd("Rule").ToString.ToUpper = "MASTER" Then
                        nd("Rule") = ""
                    End If
                Next
            End If

            If Not e.Node("IncludeInPlan") = "NO" Then
                e.Node("IncludeInPlan") = "YES"
            End If

            If e.Node("Rule").ToString.ToUpper = "MASTER" Or e.Node("Rule").ToString.ToUpper = "MANUAL" Then
                If tglPlanned.ToggleState = CheckState.Checked Then
                    e.Node("ETiltPlanned") = lbl_EtiltPlanned.Text.Trim
                Else
                    e.Node("ETiltPlanned") = lbl_EtiltPlanned.Text.Trim
                End If
            Else
                'e.Node("ETiltPlanned") = ""
            End If

            'Make changes in the tree data table
            Dim deviceRows() As DataRow
            deviceRows = dtTreeData.Select("DeviceName='" & e.Node("DeviceName") & "'")
            For Each deviceRow In deviceRows
                If IsNumeric(e.Node("ETiltPlanned")) Then
                    deviceRow("ETILT_Planned") = e.Node("ETiltPlanned").ToString
                    deviceRow("IncludeInPlan") = IIf(e.Node("IncludeInPlan").ToString = "YES", 1, 0)
                End If

            Next
            dtTreeData.AcceptChanges()

            Try
                'Make changes in the tree linked device
                Dim linkedDeviceNode As TreeListNode = Nothing

                deviceNameLinkedTo = e.Node.Nodes(0)("DEVICELINKEDTO").ToString
                If deviceNameLinkedTo IsNot Nothing AndAlso deviceNameLinkedTo <> "" Then
                    linkedDeviceNode = tlTiltManager.FindNodeByFieldValue("DeviceName", deviceNameLinkedTo)
                End If

                If Not linkedDeviceNode Is Nothing Then

                    linkedDeviceNode("IncludeInPlan") = e.Node("IncludeInPlan")
                    linkedDeviceNode("ETiltPlanned") = e.Node("ETiltPlanned")

                    deviceRows = dtTreeData.Select("DeviceName='" & deviceNameLinkedTo & "'")
                    For Each deviceRow In deviceRows
                        If IsNumeric(e.Node("ETiltPlanned")) Then
                            deviceRow("ETILT_Planned") = e.Node("ETiltPlanned").ToString
                            deviceRow("IncludeInPlan") = IIf(e.Node("IncludeInPlan").ToString = "YES", 1, 0)
                        End If
                    Next
                    dtTreeData.AcceptChanges()
                End If

            Catch ex As Exception
            End Try
        End If

        If e.Node("Rule").ToString.ToUpper = "MASTER" Or e.Node("Rule").ToString.ToUpper = "MANUAL" Then
            tbcETiltSlider.Enabled = True
        Else
            tbcETiltSlider.Enabled = False
        End If

        btnCalculateAndSave.Appearance.BackColor = Color.Yellow
    End Sub

    Private Sub gvSectorList_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            dtPointsTiltManager = Nothing
            LoadCampaignValidation()
            GetTreeListData()

            If (gvSectorList.GetFocusedRowCellValue("MBTSNAME").ToString <> selectedMbtsName) Then
                Dim dt As DataTable = clsSQLCommands.GetCoordinatesForSelectedSector(connStrIOSServer, gvSectorList.GetFocusedRowCellValue("MBTSNAME").ToString, CInt(gvSectorList.GetFocusedRowCellValue("SECTORID")))
                frmMapWindow.SetFocus_SelectedSector_TiltManager(CDbl(dt.Rows(0)("X")), CDbl(dt.Rows(0)("Y")))
            End If
            selectedMbtsName = CStr(gvSectorList.GetFocusedRowCellValue("MBTSNAME"))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tlTiltManager_FocusedNodeChanged(sender As Object, e As FocusedNodeChangedEventArgs)
        Try
            Dim fNode As TreeListNode = e.Node

            Dim resolution As Integer = 0
            If (Me.cmbResolution.SelectedItem.ToString.ToLower = "low") Then
                resolution = 7
            ElseIf (Me.cmbResolution.SelectedItem.ToString.ToLower = "medium") Then
                resolution = 15
            ElseIf (Me.cmbResolution.SelectedItem.ToString.ToLower = "high") Then
                resolution = 25
            End If

            txtETiltValue.Enabled = False

            If fNode.Level = 1 Then

                Dim drFirst As DataRow = Nothing
                Dim drMasterDataForLinked As DataRow = Nothing
                Dim drMaster() As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And TiltRule='" & "MASTER" & "'")

                If fNode("Rule") = "MASTER" And drMaster.Length > 0 Then
                    drFirst = drMaster(0)

                ElseIf fNode("Rule") = "LINKED" And drMaster.Length > 0 Then
                    drMasterDataForLinked = drMaster(0)
                    drFirst = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode("DeviceName") & "' And ETilt=" & fNode("ETilt") & " And DeviceNo=" & fNode("DeviceNo") & " And CellName='" & fNode.Nodes(0)("CellName") & "'")(0)   'drMasterDataForLinked("CellName")
                Else
                    drFirst = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode("DeviceName") & "' And ETilt=" & fNode("ETilt") & " And DeviceNo=" & fNode("DeviceNo") & " And IOS_Layer='" & fNode.Nodes(0)("Layer") & "'")(0)
                End If

                RemoveHandler tbcETiltSlider.MouseUp, AddressOf tbcETiltSlider_MouseUp
                txtETiltValue.Enabled = True

                If IsDBNull(drFirst("ETilt_Planned")) Then
                    tbcETiltSlider.EditValue = CDbl(fNode("ETilt")) * 10
                    txtETiltValue.Text = fNode("ETilt").ToString
                Else
                    tbcETiltSlider.EditValue = CDbl(IIf(drFirst("ETilt_Planned") = 0.0, fNode("ETilt"), drFirst("ETilt_Planned"))) * 10
                    txtETiltValue.Text = IIf(drFirst("ETilt_Planned") = 0.0, fNode("ETilt"), drFirst("ETilt_Planned")).ToString
                End If

                lbl_EtiltPlanned.Text = Math.Round(CDbl(tbcETiltSlider.EditValue) / 10.0, 1).ToString("F1")
                AddHandler tbcETiltSlider.MouseUp, AddressOf tbcETiltSlider_MouseUp

                'For getting not null X, Y and RadiatonCenter column values
                Dim drNotNullObjects As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode("DeviceName") & "'").Where(Function(x) x("X") IsNot DBNull.Value)(0)

                If tglPlanned.Text.ToString.ToUpper = "CURRENT" Then
                    frmMapWindow.TiltManager_Draw_Cell_Wedge(dtTreeData, drFirst("CELLNAME").ToString, fNode("DeviceName"), resolution)
                ElseIf tglPlanned.Text.ToString.ToUpper = "PLANNED" Then
                    frmMapWindow.TiltManager_Draw_Cell_Wedge(dtTreeData, drFirst("CELLNAME").ToString, fNode("DeviceName"), resolution)
                End If

                If (fNode("Rule").ToString.ToUpper = "MATCH TILT") Or (fNode("Rule").ToString.ToUpper = "MATCH VBEAM") Or (fNode("Rule").ToString.ToUpper = "LINKED") Or (fNode("Rule").ToString.ToUpper = "") Then
                    tbcETiltSlider.Enabled = False
                Else
                    tbcETiltSlider.Enabled = True
                End If

            ElseIf fNode.Level = 2 Then

                Dim drFirst As DataRow = Nothing
                Dim drMasterDataForLinked As DataRow = Nothing
                Dim drMaster() As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And TiltRule='" & "MASTER" & "'")

                drFirst = dtTreeData.Select("AntennaType='" & fNode.ParentNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode.ParentNode("DeviceName") & "' And cellname='" & fNode("CellName") & "'")(0)

                RemoveHandler tbcETiltSlider.MouseUp, AddressOf tbcETiltSlider_MouseUp
                txtETiltValue.Enabled = True

                If IsDBNull(drFirst("ETilt_Planned")) Then
                    tbcETiltSlider.EditValue = CDbl(fNode.ParentNode("ETilt")) * 10
                    txtETiltValue.Text = fNode.ParentNode("ETilt").ToString
                Else
                    tbcETiltSlider.EditValue = CDbl(IIf(drFirst("ETilt_Planned") = 0.0, fNode.ParentNode("ETilt"), drFirst("ETilt_Planned"))) * 10
                    txtETiltValue.Text = IIf(drFirst("ETilt_Planned") = 0.0, fNode.ParentNode("ETilt"), drFirst("ETilt_Planned")).ToString
                End If

                lbl_EtiltPlanned.Text = Math.Round(CDbl(tbcETiltSlider.EditValue) / 10.0, 1).ToString("F1")
                AddHandler tbcETiltSlider.MouseUp, AddressOf tbcETiltSlider_MouseUp

                'For getting not null X, Y and RadiatonCenter column values
                Dim drNotNullObjects As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode.ParentNode("DeviceName") & "'").Where(Function(x) x("X") IsNot DBNull.Value)(0)

                If tglPlanned.Text.ToString.ToUpper = "CURRENT" Then
                    frmMapWindow.TiltManager_Draw_Cell_Wedge(dtTreeData, drFirst("CELLNAME").ToString, fNode.ParentNode("DeviceName"), resolution)
                ElseIf tglPlanned.Text.ToString.ToUpper = "PLANNED" Then
                    frmMapWindow.TiltManager_Draw_Cell_Wedge(dtTreeData, drFirst("CellName").ToString, fNode.ParentNode("DeviceName"), resolution)
                End If

                If (fNode.ParentNode("Rule").ToString.ToUpper = "MATCH TILT") Or (fNode.ParentNode("Rule").ToString.ToUpper = "MATCH VBEAM") Or (fNode.ParentNode("Rule").ToString.ToUpper = "LINKED") Or (fNode.ParentNode("Rule").ToString.ToUpper = "") Then
                    tbcETiltSlider.Enabled = False
                    txtETiltValue.Enabled = False
                Else
                    tbcETiltSlider.Enabled = True
                    txtETiltValue.Enabled = True
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnCalculateAndSave_Click(sender As Object, e As EventArgs) Handles btnCalculateAndSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbManualCampaign.SelectedIndex = 0 Then
                XtraMessageBox.Show("Please select campaign", "Submit Campaign", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            selectedTiltCampaignID = CInt(TryCast(cmbManualCampaign.SelectedItem, clsComboBoxItem).Value)

            'Delete Tilt Manual
            DeleteTiltManual()

            'Upload tree data into db
            LoadData_Into_Tilt_Manual_Input()

            'Calculate ETilt
            ExecuteCalculateETilt()

            'Load campaign validation grid
            LoadCampaignValidation()

            dtPointsTiltManager = Nothing

            'Reload tree data from db
            GetTreeListData()

            'collapse all tree nodes
            tlTiltManager.ExpandAll()
            btnManageTree.Text = "Collapse Tree"

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            btnCalculateAndSave.Appearance.BackColor = Nothing
        End Try
    End Sub

    Private Sub btnAddCampaign_Click(sender As Object, e As EventArgs) Handles btnAddCampaign.Click
        Try
            Dim newCampaignName As String = Nothing
            newCampaignName = XtraInputBox.Show("Campaign Name: ", "Add New Tilt Campaign", "")
            If newCampaignName = "" Then
                Exit Sub
            End If
            clsSQLCommands.AddManualTiltCampaign(connStrIOSServer, newCampaignName)
            LoadTiltCampaigns()
            SetComboBox(cmbManualCampaign, ComboSelectBased.TextBased, newCampaignName)
            ManageButtons(True)

            'Clear Tree and chart for the new campaign
            ch_TiltManager.SeriesCollection.Clear()
            ch_TiltManager.Annotations.Clear()
            ch_TiltManager.ClearAll()
            ch_TiltManager.Refresh()
            tlTiltManager.Nodes.Clear()
            tlTiltManager.Refresh()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteCampaign_Click(sender As Object, e As EventArgs) Handles btnDeleteCampaign.Click
        Try
            If cmbManualCampaign.SelectedIndex <> 0 Then
                Dim campaignName As String = cmbManualCampaign.SelectedItem.ToString
                If XtraMessageBox.Show("Are you sure to delete campaign: " & campaignName & "?", "Delete Manual Tilt Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()

                    DeleteManualTiltCampaign(TryCast(cmbManualCampaign.SelectedItem, clsComboBoxItem).Value)
                    LoadTiltCampaigns()

                    cmbManualCampaign.SelectedIndex = 0
                    cmbManualCampaign_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbManualCampaign_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            selectedTiltCampaignID = 0
            selectedTiltCampaignName = Nothing

            If cmbManualCampaign.SelectedIndex <> 0 Then
                selectedTiltCampaignID = CInt(TryCast(cmbManualCampaign.SelectedItem, clsComboBoxItem).Value)
                selectedTiltCampaignName = cmbManualCampaign.SelectedItem.ToString
                FillSectorListForCampaign()
            Else
                gcSectorList.DataSource = Nothing
                gvSectorList.Columns.Clear()
                gcCampaignValidation.DataSource = Nothing
                gvCampaignValidation.Columns.Clear()
                ch_TiltManager.SeriesCollection.Clear()
                ch_TiltManager.Annotations.Clear()
                ch_TiltManager.ClearAll()
                ch_TiltManager.Refresh()
                tlTiltManager.Nodes.Clear()
                tlTiltManager.Refresh()
                ManageButtons(False)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnGenerateTiltCampaign_Click(sender As Object, e As EventArgs) Handles btnGenerateTiltCampaign.Click
        Dim hasData As Boolean = False

        Try
            WaitScreen.ShowWaitScreen("Writing MML/XML data ...")
            Dim dsMML As DataSet = clsSQLCommands.GetMMLDataForTiltCampaign(connStrIOSServer, selectedTiltCampaignID)

            Dim objFileDlg As New SaveFileDialog()
            If openFileDirectory Is Nothing Then
                objFileDlg.InitialDirectory = IO.Directory.GetCurrentDirectory()
            Else
                objFileDlg.InitialDirectory = openFileDirectory
            End If
            objFileDlg.Filter = "Exports|*.txt;*.xml"
            objFileDlg.Title = "Save a TXT/XML File"



            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then
                    Dim OriginalFileName As String = objFileDlg.FileName
                    For i = 0 To dsMML.Tables.Count - 1

                        If i = 0 Then 'HUAWEI
                            Dim Content() As Byte = CSVBytesWriter(dsMML.Tables(i), False)
                            If Content.Length > 0 Then
                                hasData = True

                                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_HUAWEI" + ".txt"
                                Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                                fs.Write(Content, 0, Content.Length)
                                fs.Close()
                            End If

                        ElseIf i = 1 Then 'ERICSSON
                            Dim Content() As Byte = CSVBytesWriter(dsMML.Tables(i), False)
                            If Content.Length > 3 Then
                                hasData = True

                                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_ERICSSON" + ".xml"
                                Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                                fs.Write(Content, 0, Content.Length)
                                fs.Close()
                            End If
                        ElseIf i = 2 Then 'NOKIA
                            Dim Content() As Byte = CSVBytesWriter(dsMML.Tables(i), False)
                            If Content.Length > 0 Then
                                hasData = True

                                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_NOKIA" + ".xml"
                                Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                                fs.Write(Content, 0, Content.Length)
                                fs.Close()
                            End If
                        End If

                    Next
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            If hasdata = True Then
                XtraMessageBox.Show("MML/XML file created successfully", "Get MML/XML", MessageBoxButtons.OK)
            Else
                XtraMessageBox.Show("MML/XML file not created - no data", "Get MML/XML", MessageBoxButtons.OK)
            End If

        End Try
    End Sub

    Private Sub frmLaunchTiltManager_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'Set these global variables to nothing when the form is closed
            selectedTiltCampaignID = 0
            selectedTiltCampaignName = Nothing
            dtPointsTiltManager.Dispose()
            dtPointsTiltManager = Nothing
        Catch
        End Try
    End Sub

    Private Sub tglPlanned_Click(sender As Object, e As EventArgs) Handles tglPlanned.Click
        Try
            If sender IsNot Nothing Then
                If tglPlanned.ToggleState = CheckState.Checked Then
                    tglPlanned.ToggleState = CheckState.Unchecked
                    tglPlanned.Text = "Current"
                ElseIf tglPlanned.ToggleState = CheckState.Unchecked Then
                    tglPlanned.ToggleState = CheckState.Checked
                    tglPlanned.Text = "Planned"
                End If
                dtPointsTiltManager = Nothing

                tbcETiltSlider_MouseUp(Nothing, Nothing)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tbcETiltSlider_ValueChanged(sender As Object, e As EventArgs) Handles tbcETiltSlider.ValueChanged
        lbl_EtiltPlanned.Text = Math.Round((CDbl(tbcETiltSlider.EditValue) / 10), 1).ToString("F1")
    End Sub

    Private Sub tbcETiltSlider_MouseUp(sender As Object, e As MouseEventArgs)

        RemoveHandler tbcETiltSlider.ValueChanged, AddressOf tbcETiltSlider_ValueChanged
        tbcETiltSlider.Enabled = False

        Try
            Dim fNode As TreeListNode = tlTiltManager.FocusedNode
            If fNode.Level = 1 Then
                fNode("ETiltPlanned") = lbl_EtiltPlanned.Text.Trim
            End If
        Catch ex As Exception
        End Try

        Try
            dtPointsTiltManager = Nothing
            lbl_EtiltPlanned.Text = Math.Round((CDbl(tbcETiltSlider.EditValue) / 10), 1).ToString("F1")
            'Dim fNode As TreeListNode = tlTiltManager.FocusedNode
            'If fNode.Level = 1 Then

            If tglPlanned.Text.ToString.ToUpper = "PLANNED" Then
                'Dim drFirst As DataRow = Nothing
                'Dim drMasterDataForLinked As DataRow = Nothing
                'Dim drMaster() As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And TiltRule='" & "MASTER" & "'")

                'If fNode("Rule") = "MASTER" And drMaster.Length > 0 Then
                '    drFirst = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode("DeviceName") & "' And ETilt=" & fNode("ETilt") & " And DeviceNo=" & fNode("DeviceNo") & " And TiltRule='" & "MASTER" & "'")(0)

                'ElseIf fNode("Rule") = "LINKED" And drMaster.Length > 0 Then
                '    drMasterDataForLinked = drMaster(0)
                '    drFirst = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode("DeviceName") & "' And ETilt=" & fNode("ETilt") & " And DeviceNo=" & fNode("DeviceNo") & " And CellName='" & drMasterDataForLinked("CellName") & "'")(0)

                'Else

                '    drFirst = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode("DeviceName") & "' And ETilt=" & fNode("ETilt") & " And DeviceNo=" & fNode("DeviceNo") & " And IOS_Layer='" & fNode.Nodes(0)("Layer") & "'")(0)
                'End If

                ''For getting not null X, Y and RadiatonCenter column values
                'Dim drNotNullObjects As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode("DeviceName") & "'").Where(Function(x) x("X") IsNot DBNull.Value)(0)

                ''frmMapWindow.TiltManager_Cell_AntennaTiltCoverage(drFirst("CellName"), nZ(drNotNullObjects("X"), 0), nZ(drNotNullObjects("Y"), 0), drFirst("AntennaBand"), CDbl(nZ(drFirst("AZIMUTH"), 0)), CDbl(nZ(drNotNullObjects("RadiationCenter"), 0)), CDbl(lbl_EtiltPlanned.Text.Trim), CDbl(nZ(drFirst("MTilt").ToString, 0)), 99, 0, 0, CType(fNode.Tag, Color))

                For Each drTreeData In dtTreeData.Rows
                    If IsNumeric(drTreeData("ETILT_Planned")) Then
                        drTreeData("HPDW") = Math.Round(CDbl(drTreeData("ETILT_Planned") - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                    Else
                        drTreeData("HPDW") = Math.Round(CDbl(drTreeData("ETILT") - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                    End If


                Next
                dtTreeData.AcceptChanges()

                frmMapWindow.TiltManager_Cell_AntennaTiltCoverage_New(dtTreeData)

                'draw wedge for focused node (if devicenode, take first  entry, if cellnode take node itself)
                Draw_Wedge_From_SliderUpdate()

            ElseIf tglPlanned.Text.ToString.ToUpper = "CURRENT" Then

                For Each drTreeData In dtTreeData.Rows
                    drTreeData("HPDW") = Math.Round(CDbl(drTreeData("ETILT") - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                Next
                dtTreeData.AcceptChanges()

                frmMapWindow.TiltManager_Cell_AntennaTiltCoverage_New(dtTreeData)

            End If


            'fNode("ETiltPlanned") = lbl_EtiltPlanned.Text.Trim

            'ElseIf fNode.Level = 2 Then

            'If tglPlanned.Text.ToString.ToUpper = "PLANNED" Then
            'Dim drFirst As DataRow = Nothing
            'Dim drMasterDataForLinked As DataRow = Nothing
            'Dim drMaster() As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode.ParentNode("AntennaType") & "' And TiltRule='" & "MASTER" & "'")

            'drFirst = dtTreeData.Select("AntennaType='" & fNode.ParentNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode.ParentNode("DeviceName") & "' And cellname='" & fNode("CellName") & "'")(0)

            ''For getting not null X, Y and RadiatonCenter column values
            'Dim drNotNullObjects As DataRow = dtTreeData.Select("AntennaType='" & fNode.ParentNode.ParentNode("AntennaType") & "' And DeviceName='" & fNode.ParentNode("DeviceName") & "'").Where(Function(x) x("X") IsNot DBNull.Value)(0)

            ''frmMapWindow.TiltManager_Cell_AntennaTiltCoverage(drFirst("CellName"), nZ(drNotNullObjects("X"), 0), nZ(drNotNullObjects("Y"), 0), drFirst("AntennaBand"), CDbl(nZ(drFirst("AZIMUTH"), 0)), CDbl(nZ(drNotNullObjects("RadiationCenter"), 0)), CDbl(lbl_EtiltPlanned.Text.Trim), CDbl(nZ(drFirst("MTilt").ToString, 0)), 99, 0, 0, CType(fNode.ParentNode.Tag, Color))

            'For Each drTreeData In dtTreeData.Rows
            '            drTreeData("HPDW") = CDbl(drTreeData("ETILT_Planned") - CDbl(drTreeData("VBeamAngle") / 2))
            '        Next
            '        dtTreeData.AcceptChanges()
            '    Else
            '        For Each drTreeData In dtTreeData.Rows
            '            drTreeData("HPDW") = CDbl(drTreeData("ETILT") - CDbl(drTreeData("VBeamAngle") / 2))
            '        Next
            '        dtTreeData.AcceptChanges()
            '    End If

            '    frmMapWindow.TiltManager_Cell_AntennaTiltCoverage_New(dtTreeData)
            '    fNode.ParentNode("ETiltPlanned") = lbl_EtiltPlanned.Text.Trim

            'End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try

        tbcETiltSlider.Enabled = True
        AddHandler tbcETiltSlider.ValueChanged, AddressOf tbcETiltSlider_ValueChanged

    End Sub

    Private Sub btnClearThematics_Click(sender As Object, e As EventArgs) Handles btnClearThematics.Click
        Try
            frmMapWindow.CloseMapTables_RemoveLayerModifiers()
            frmMapWindow.RemoveLabelLayers()
            frmMapWindow.MapControl1.Map.Legends.Clear()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs)
        Try

            Dim tl As TreeList = TryCast(e.SelectedControl, TreeList)
            If tl IsNot Nothing AndAlso dtValidationData IsNot Nothing Then
                If dtValidationData.Rows.Count > 0 Then
                    Dim hitInfo As TreeListHitInfo = tl.CalcHitInfo(e.ControlMousePosition)
                    If hitInfo.HitInfoType = HitInfoType.Cell AndAlso hitInfo.Column.FieldName = "Validation" Then
                        Dim cellInfo As Object = New DevExpress.XtraTreeList.ViewInfo.TreeListCellToolTipInfo(hitInfo.Node, hitInfo.Column, Nothing)
                        Dim hitNode As TreeListNode = hitInfo.Node
                        If hitInfo.Node.Level = 2 Then
                            Dim devicename As String = hitNode.ParentNode("DeviceName").ToString
                            Dim validationMsg As String = ""
                            For Each dr As DataRow In dtValidationData.Select("CELLNAME='" & hitNode("CellName").ToString & "' AND DEVICENAME='" & devicename & "'")
                                validationMsg = validationMsg + CStr(dr("ValidationMsgType")) + ": " + CStr(dr("ValidationMsg")) + vbLf
                            Next
                            validationMsg = validationMsg.TrimEnd(vbLf)

                            e.Info = New DevExpress.Utils.ToolTipControlInfo(cellInfo, validationMsg)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_DeleteSector_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteSector.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbManualCampaign.SelectedIndex > 0 AndAlso gvSectorList.RowCount > 0 Then
                Dim parray()() As String = {
                    New String() {"@CampaignID", CInt(TryCast(cmbManualCampaign.SelectedItem, clsComboBoxItem).Value)},
                    New String() {"@MBTSNAME", Chr(39) & gvSectorList.GetFocusedRowCellValue("MBTSNAME") & Chr(39)},
                    New String() {"@SECTORID", Chr(39) & gvSectorList.GetFocusedRowCellValue("SECTORID") & Chr(39)}
                }
                Dim sql As String = GetSQL(4935, parray)(1)
                Dim connstring As String = GetSQL(4935, parray)(0)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                XtraMessageBox.Show("Sector list deleted successfully.", "Delete Sector List", MessageBoxButtons.OK)

                Me.FillSectorListForCampaign()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnManageTree_Click(sender As Object, e As EventArgs) Handles btnManageTree.Click
        Try
            If btnManageTree.Text.ToUpper = "EXPAND TREE" Then
                tlTiltManager.ExpandAll()
                btnManageTree.Text = "Collapse Tree"
            ElseIf btnManageTree.Text.ToUpper = "COLLAPSE TREE" Then
                tlTiltManager.CollapseAll()
                btnManageTree.Text = "Expand Tree"
            End If
        Catch
        End Try
    End Sub

    Private Sub txtETiltValue_Leave(sender As Object, e As EventArgs) Handles txtETiltValue.Leave
        If txtETiltValue.Text.Trim <> "" AndAlso CDbl(txtETiltValue.Text.Trim) > 15.0 Then
            XtraMessageBox.Show("E-Tilt cannot be beyond 15.0", "E-Tilt setting", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Dim fNode As TreeListNode = tlTiltManager.FocusedNode
            If fNode.Level = 1 Then
                fNode("ETiltPlanned") = txtETiltValue.Text.Trim
            End If
        Catch ex As Exception
        End Try

        Try
            dtPointsTiltManager = Nothing
            lbl_EtiltPlanned.Text = Math.Round(CDbl(txtETiltValue.Text.Trim), 1).ToString("F1")

            If tglPlanned.Text.ToString.ToUpper = "PLANNED" Then

                For Each drTreeData In dtTreeData.Rows
                    If IsNumeric(drTreeData("ETILT_Planned")) Then
                        drTreeData("HPDW") = Math.Round(CDbl(drTreeData("ETILT_Planned") - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                    Else
                        drTreeData("HPDW") = Math.Round(CDbl(drTreeData("ETILT") - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                    End If
                Next
                dtTreeData.AcceptChanges()

                frmMapWindow.TiltManager_Cell_AntennaTiltCoverage_New(dtTreeData)

                'draw wedge for focused node (if devicenode, take first  entry, if cellnode take node itself)
                Draw_Wedge_From_SliderUpdate()

            ElseIf tglPlanned.Text.ToString.ToUpper = "CURRENT" Then

                For Each drTreeData In dtTreeData.Rows
                    drTreeData("HPDW") = Math.Round(CDbl(drTreeData("ETILT") - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                Next
                dtTreeData.AcceptChanges()

                frmMapWindow.TiltManager_Cell_AntennaTiltCoverage_New(dtTreeData)

            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub cmbResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResolution.SelectedIndexChanged
        dtPointsTiltManager = Nothing
    End Sub

#End Region

#Region "MML Campaigns Events"

    Private Sub gvMmlConfig_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles gvMmlConfig.FocusedRowChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim grdView As GridView = CType(sender, GridView)

            RemoveHandler ceIsPublicMML.CheckedChanged, AddressOf ceIsPublicMml_CheckedChanged

            If grdView.FocusedRowHandle > -1 Then
                If gvMmlCampaign.FocusedRowHandle > -1 Then
                    btnValidate.Enabled = True
                Else
                    btnValidate.Enabled = False
                End If

                LoadScripts(grdView.GetRowCellValue(e.FocusedRowHandle, "MMLConfigID"))

                'Dim drConfig As DataRow = GetMmlConfigDetailsByID(grdView.GetRowCellValue(e.FocusedRowHandle, "MMLConfigID"))
                'If drConfig IsNot Nothing Then
                '    lblOwnerMmlConfig.Text = drConfig("MMLConfigOwner").ToString
                '    ceIsPublicMML.Checked = IIf(IsDBNull(drConfig("IsPublic")), False, drConfig("IsPublic"))
                'End If

                'If lblOwnerMmlConfig.Text.ToLower <> Environment.UserName.ToLower Then
                '    ceIsPublicMML.Enabled = False
                '    If ceIsPublicMML.Checked Then
                '        btnMmlConfigClone.Enabled = True
                '        btnMmlConfigDelete.Enabled = True
                '    Else
                '        btnMmlConfigClone.Enabled = False
                '        btnMmlConfigDelete.Enabled = False
                '    End If
                'Else
                '    ceIsPublicMML.Enabled = True
                '    btnMmlConfigClone.Enabled = True
                '    btnMmlConfigDelete.Enabled = True
                'End If
            End If

            AddHandler ceIsPublicMML.CheckedChanged, AddressOf ceIsPublicMml_CheckedChanged
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsPublicMml_CheckedChanged(sender As Object, e As EventArgs)
        Throw New NotImplementedException()
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Bulk Insert MML User Filter...
            Dim resultSetID As String = ""
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drResultSet As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")
            End If
            'InsertMmlUserFilterBulk(resultSetID)

            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                Dim dr1 As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row

                campaignID = dr1("CampaignID")
                resultSetID = dr1("ResultSetID")
                campaignType = dr1("CampaignType")
                mmlConfigID = dr2("MMLConfigID")

                LoadValidationGrid(campaignID, campaignType, resultSetID, mmlConfigID)
                LoadDataGrid(resultSetID)
                LoadExcludedGrid(resultSetID)
                'GetMmlUserFilter(resultSetID)
                'LoadSelectionTree(resultSetID)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnPreFilter_Click(sender As Object, e As EventArgs) Handles btnPreFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing
            Dim resultSetID As String = Nothing

            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                Dim dr1 As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row

                campaignID = dr1("CampaignID")
                resultSetID = dr1("ResultSetID")
                campaignType = dr1("CampaignType")
                mmlConfigID = dr2("MMLConfigID")
                GetMmlUserFilter(resultSetID)
                LoadSelectionTree(resultSetID)
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnMML_Click(sender As Object, e As EventArgs) Handles btnMML.Click
        Dim hasdata As Boolean = False
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Bulk Insert MML User Filter...
            Dim resultSetID As String = ""
            Dim campaignName As String = ""
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drResultSet As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")
            End If
            'InsertMmlUserFilterBulk(resultSetID)

            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then
                Dim dr1 As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row

                campaignName = dr1("CampaignName").ToString
                campaignID = dr1("CampaignID")

                campaignType = dr1("CampaignType")
                mmlConfigID = dr2("MMLConfigID")

                LoadValidationGrid(campaignID, campaignType, resultSetID, mmlConfigID)
                LoadDataGrid(resultSetID)
                GetTiltMmlData(campaignID, resultSetID, campaignType)

                For Each dtMmlScript As DataTable In dsGetMml.Tables
                    If dtMmlScript.Rows.Count > 0 Then
                        hasdata = True
                    End If
                Next

                If hasdata = True Then

                    Dim objFileDlg As New SaveFileDialog()
                    If openFileDirectory Is Nothing Then
                        objFileDlg.InitialDirectory = IO.Directory.GetCurrentDirectory()
                    Else
                        objFileDlg.InitialDirectory = openFileDirectory
                    End If
                    objFileDlg.Filter = "Exports|*.txt;*.xml"
                    objFileDlg.Title = "Save a TXT/XML File"

                    If objFileDlg.ShowDialog() = DialogResult.OK Then
                        If objFileDlg.FileName <> "" Then
                            Dim OriginalFileName As String = objFileDlg.FileName & IIf(txtFileNameSuffix.Text = "", "", "_" & txtFileNameSuffix.Text)
                            'For i = 0 To dsGetMml.Tables.Count - 1

                            '    If i = 0 Then 'HUAWEI
                            '        Dim Content() As Byte = CSVBytesWriter(dsGetMml.Tables(i), False)
                            '        If Content.Length > 0 Then
                            '            hasdata = True

                            '            If cmbOutputLocation.SelectedIndex = 0 Then

                            '                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_HUAWEI" + ".txt"
                            '                Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            '                fs.Write(Content, 0, Content.Length)
                            '                fs.Close()

                            '            End If
                            '        End If

                            '    ElseIf i >= 1 Then 'ERICSSON
                            '        Dim Content() As Byte = CSVBytesWriter(dsGetMml.Tables(i), False)
                            '        If Content.Length > 3 Then
                            '            hasdata = True

                            '            If Not IsDBNull(dsGetMml.Tables(i).Rows(0)("ENM_SOURCE").ToString) Then
                            '                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_" + dsGetMml.Tables(i).Rows(0)("ENM_SOURCE").ToString + "_ERICSSON" + ".xml"
                            '            Else
                            '                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_ERICSSON" + ".xml"
                            '            End If

                            '            Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            '            fs.Write(Content, 0, Content.Length)
                            '            fs.Close()
                            '        End If
                            'ElseIf i = 2 Then 'NOKIA
                            '    Dim Content() As Byte = CSVBytesWriter(dsGetMml.Tables(i), False)
                            '    If Content.Length > 0 Then
                            '        hasdata = True

                            '        objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_NOKIA" + ".xml"
                            '        Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            '        fs.Write(Content, 0, Content.Length)
                            '        fs.Close()
                            '    End If
                            '    End If

                            'Next
                            For Each dtGetMml As DataTable In dsGetMml.Tables

                                Dim s As String = String.Join(vbLf, dtGetMml.Rows.OfType(Of DataRow)().[Select](Function(r) r(0).ToString()))
                                If s.Length > 0 Then

                                    If cmbOutputLocation.SelectedIndex = 0 AndAlso dsGetMml.Tables.IndexOf(dtGetMml) >= 0 Then '0 index used for single file or for ericsson

                                        If OriginalFileName <> "" Then
                                            If dsGetMml.Tables.IndexOf(dtGetMml) = 0 Then
                                                System.IO.File.WriteAllText(OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_Huawei.txt", s)
                                            Else
                                                If Not IsDBNull(dtGetMml.Rows(0)("ENM_SOURCE").ToString) Then
                                                    System.IO.File.WriteAllText(OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_" & dtGetMml.Rows(0)("ENM_SOURCE").ToString & "_Ericsson.xml", s)
                                                Else
                                                    System.IO.File.WriteAllText(OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_Ericsson.xml", s)
                                                End If
                                            End If
                                        End If

                                    ElseIf cmbOutputLocation.SelectedIndex = 1 AndAlso dsGetMml.Tables.IndexOf(dtGetMml) >= 0 Then '1 index used for spilt file in 2MB chunk
                                        Dim offset As Integer = 0
                                        Dim fileCount As Integer = 1
                                        Dim file_name As String = Nothing
                                        Dim outputBytes As String()

                                        If dsGetMml.Tables.IndexOf(dtGetMml) = 0 Then
                                            file_name = OriginalFileName.Substring(0, OriginalFileName.IndexOf("."))
                                        Else
                                            If Not IsDBNull(dtGetMml.Rows(0)("ENM_SOURCE").ToString) Then
                                                file_name = OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_" & dtGetMml.Rows(0)("ENM_SOURCE").ToString
                                            Else
                                                file_name = OriginalFileName.Substring(0, OriginalFileName.IndexOf("."))
                                            End If
                                        End If

                                        Dim fileSize As Integer = Convert.ToInt32(seFileSize.EditValue) * 1024 * 1024     'Chunk file size is like 2MB (2097152 bytes)
                                        Dim ChunkSize As Integer = 0
                                        Dim strData() As String = GetStringArrayFromDataTable(dtGetMml, False)

                                        For Index As Integer = 0 To strData.Length - 1
                                            Dim stringByteCount As Integer = System.Text.Encoding.UTF8.GetByteCount(strData(Index))
                                            If (ChunkSize + stringByteCount) <= fileSize Then
                                                ChunkSize = ChunkSize + stringByteCount
                                                If Index = (strData.Length - 1) Then
                                                    OriginalFileName = file_name & "_" & fileCount & "." & objFileDlg.DefaultExt
                                                    outputBytes = New String(Index - offset) {}
                                                    System.Array.Copy(strData, offset, outputBytes, 0, outputBytes.Length)
                                                    System.IO.File.WriteAllLines(OriginalFileName, outputBytes)
                                                    Exit For
                                                End If
                                            Else
                                                Index = Index - 1
                                                OriginalFileName = file_name & "_" & fileCount & "." & objFileDlg.DefaultExt
                                                outputBytes = New String(Index - offset - 1) {}
                                                System.Array.Copy(strData, offset, outputBytes, 0, outputBytes.Length)
                                                System.IO.File.WriteAllLines(OriginalFileName, outputBytes)
                                                offset = Index
                                                fileCount += 1
                                                ChunkSize = 0
                                            End If
                                        Next
                                    End If
                                End If
                            Next

                        End If
                    End If
                End If
                'GetMmlUserFilter(resultSetID)
                'LoadSelectionTree(resultSetID)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally

            If hasdata = True Then
                XtraMessageBox.Show("MML/XML file created successfully", "Get MML/XML", MessageBoxButtons.OK)
            Else
                XtraMessageBox.Show("MML/XML file not created - no data", "Get MML/XML", MessageBoxButtons.OK)
            End If

            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Function GetStringArrayFromDataTable(ByRef dTable As DataTable, Optional ByVal WithHeader As Boolean = True) As String()
        '--------Columns Name-----------
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim intClmn As Integer = dTable.Columns.Count

        If WithHeader Then
            Dim i As Integer = 0
            For i = 0 To intClmn - 1 Step i + 1
                sb.Append("""" + dTable.Columns(i).ColumnName.ToString() + """")
                If i = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)
        End If

        '--------Data By  Columns---------

        Dim row As DataRow
        For Each row In dTable.Rows
            Dim ir As Integer = 0
            For ir = 0 To intClmn - 1 Step ir + 1
                'sb.Append("""" + row(ir).ToString().Replace("""", """""") + """")
                sb.Append(row(ir).ToString)
                If ir = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)
        Next
        Dim strData() As String = sb.ToString().Split(vbNewLine)
        Return strData
    End Function

    Private Sub gvMmlCampaign_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) 'Handles gvMmlCampaign.FocusedRowChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If gvMmlCampaign.FocusedRowHandle > -1 Then
                If gvMmlConfig.FocusedRowHandle > -1 Then
                    btnValidate.Enabled = True
                    btnPreFilter.Enabled = True

                    gcValidation.DataSource = Nothing
                    gvValidation.Columns.Clear()

                    gcData.DataSource = Nothing
                    gvData.Columns.Clear()

                    gcExcluded.DataSource = Nothing
                    gvExcluded.Columns.Clear()

                    tvSelectionMml.Nodes.Clear()
                Else
                    btnValidate.Enabled = False
                    btnPreFilter.Enabled = False
                End If

                If e IsNot Nothing Then
                    lblOwnerMmlInput.Text = gvMmlCampaign.GetRowCellValue(e.FocusedRowHandle, "CampaignOwner").ToString
                    lblLastEndTimeMml.Text = gvMmlCampaign.GetRowCellValue(e.FocusedRowHandle, "ResultsCreated").ToString
                Else
                    lblOwnerMmlInput.Text = gvMmlCampaign.GetRowCellValue(gvMmlCampaign.FocusedRowHandle, "CampaignOwner").ToString
                    lblLastEndTimeMml.Text = gvMmlCampaign.GetRowCellValue(gvMmlCampaign.FocusedRowHandle, "ResultsCreated").ToString
                End If

                If lblOwnerMmlInput.Text.ToLower <> Environment.UserName.ToLower Then
                    btnDeleteMml.Enabled = False
                Else
                    btnDeleteMml.Enabled = True
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnRefreshMml_Click(sender As Object, e As EventArgs) Handles btnRefreshMml.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            txtSearchMml.Text = String.Empty
            LoadTiltMMLCampaign()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteMml_Click(sender As Object, e As EventArgs) Handles btnDeleteMml.Click
        Try
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Tilt Campaign Result Set", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim strConnection As String = Nothing
                    Dim sqlParam As String = Nothing
                    Dim parray()() As String = {
                        New String() {"@ResultSetID", Chr(39) & dr("ResultSetID") & Chr(39)}
                    }

                    strConnection = GetSQL(4910, parray)(0)
                    sqlParam = GetSQL(4910, parray)(1)
                    IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
                    gvMmlCampaign.DeleteRow(rIndex(0))
                    If gvMmlCampaign.RowCount > 0 Then
                        gvMmlCampaign.ClearSelection()
                        gvMmlCampaign.SelectRow(0)
                        gvMmlCampaign.FocusedRowHandle = 0
                        gvMmlCampaign_FocusedRowChanged(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnMMLRollback_Click(sender As Object, e As EventArgs) Handles btnMMLRollback.Click
        Dim hasdata As Boolean = False
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Bulk Insert MML User Filter...
            Dim resultSetID As String = ""
            Dim campaignName As String = ""
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drResultSet As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                resultSetID = drResultSet("ResultSetID")
            End If
            'InsertMmlUserFilterBulk(resultSetID)

            Dim campaignID As Integer = 0
            Dim mmlConfigID As Integer = 0
            Dim campaignType As String = Nothing

            Dim rIndex2() As Integer = gvMmlConfig.GetSelectedRows()

            If rIndex.Length > 0 AndAlso rIndex2.Length > 0 Then

                Dim dr1 As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim dr2 As DataRow = gvMmlConfig.GetRow(rIndex2(0)).Row

                campaignName = dr1("CampaignName").ToString
                campaignID = dr1("CampaignID")

                campaignType = dr1("CampaignType")
                mmlConfigID = dr2("MMLConfigID")

                LoadValidationGrid(campaignID, campaignType, resultSetID, mmlConfigID)
                LoadDataGrid(resultSetID)
                GetTiltMmlDataRollback(campaignID, resultSetID, campaignType)

                For Each dtMmlScriptRollback As DataTable In dsGetMmlRollback.Tables
                    If dtMmlScriptRollback.Rows.Count > 0 Then
                        hasdata = True
                    End If
                Next

                If hasdata = True Then

                    Dim objFileDlg As New SaveFileDialog()
                    If openFileDirectory Is Nothing Then
                        objFileDlg.InitialDirectory = IO.Directory.GetCurrentDirectory()
                    Else
                        objFileDlg.InitialDirectory = openFileDirectory
                    End If
                    objFileDlg.Filter = "Exports|*.txt;*.xml"
                    objFileDlg.Title = "Save a TXT/XML File"

                    If objFileDlg.ShowDialog() = DialogResult.OK Then
                        If objFileDlg.FileName <> "" Then
                            Dim OriginalFileName As String = objFileDlg.FileName & "_" & IIf(txtFileNameSuffix.Text = "", "", txtFileNameSuffix.Text)
                            'For i = 0 To dsGetMmlRollback.Tables.Count - 1

                            '    If i = 0 Then 'HUAWEI
                            '        Dim Content() As Byte = CSVBytesWriter(dsGetMmlRollback.Tables(i), False)
                            '        If Content.Length > 0 Then
                            '            hasdata = True

                            '            objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_HUAWEI" + ".txt"
                            '            Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            '            fs.Write(Content, 0, Content.Length)
                            '            fs.Close()
                            '        End If

                            '    ElseIf i = 1 Then 'ERICSSON
                            '        Dim Content() As Byte = CSVBytesWriter(dsGetMmlRollback.Tables(i), False)
                            '        If Content.Length > 3 Then
                            '            hasdata = True

                            '            If Not IsDBNull(dsGetMmlRollback.Tables(i).Rows(0)("ENM_SOURCE").ToString) Then
                            '                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_" + dsGetMmlRollback.Tables(i).Rows(0)("ENM_SOURCE").ToString + "_ERICSSON" + ".xml"
                            '            Else
                            '                objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_ERICSSON" + ".xml"
                            '            End If

                            '            Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            '            fs.Write(Content, 0, Content.Length)
                            '            fs.Close()
                            '        End If
                            '    ElseIf i = 2 Then 'NOKIA
                            '        Dim Content() As Byte = CSVBytesWriter(dsGetMmlRollback.Tables(i), False)
                            '        If Content.Length > 0 Then
                            '            hasdata = True

                            '            objFileDlg.FileName = IO.Path.GetDirectoryName(OriginalFileName) + "\" + IO.Path.GetFileNameWithoutExtension(OriginalFileName) + "_NOKIA" + ".xml"
                            '            Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            '            fs.Write(Content, 0, Content.Length)
                            '            fs.Close()
                            '        End If
                            '    End If

                            'Next
                            For Each dtGetMmlRollback As DataTable In dsGetMmlRollback.Tables

                                Dim s As String = String.Join(vbLf, dtGetMmlRollback.Rows.OfType(Of DataRow)().[Select](Function(r) r(0).ToString()))
                                If s.Length > 0 Then

                                    If cmbOutputLocation.SelectedIndex = 0 AndAlso dsGetMmlRollback.Tables.IndexOf(dtGetMmlRollback) >= 0 Then '0 index used for single file or for ericsson

                                        If OriginalFileName <> "" Then
                                            If dsGetMmlRollback.Tables.IndexOf(dtGetMmlRollback) = 0 Then
                                                System.IO.File.WriteAllText(OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_Huawei.txt", s)
                                            Else
                                                If Not IsDBNull(dtGetMmlRollback.Rows(0)("ENM_SOURCE").ToString) Then
                                                    System.IO.File.WriteAllText(OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_" & dtGetMmlRollback.Rows(0)("ENM_SOURCE").ToString & "_Ericsson.xml", s)
                                                Else
                                                    System.IO.File.WriteAllText(OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_Ericsson.xml", s)
                                                End If
                                            End If
                                        End If

                                    ElseIf cmbOutputLocation.SelectedIndex = 1 AndAlso dsGetMmlRollback.Tables.IndexOf(dtGetMmlRollback) >= 0 Then '1 index used for spilt file in 2MB chunk
                                        Dim offset As Integer = 0
                                        Dim fileCount As Integer = 1
                                        Dim file_name As String = Nothing
                                        Dim outputBytes As String()

                                        If dsGetMmlRollback.Tables.IndexOf(dtGetMmlRollback) = 0 Then
                                            file_name = OriginalFileName.Substring(0, OriginalFileName.IndexOf("."))
                                        Else
                                            If Not IsDBNull(dtGetMmlRollback.Rows(0)("ENM_SOURCE").ToString) Then
                                                file_name = OriginalFileName.Substring(0, OriginalFileName.IndexOf(".")) & "_" & dtGetMmlRollback.Rows(0)("ENM_SOURCE").ToString
                                            Else
                                                file_name = OriginalFileName.Substring(0, OriginalFileName.IndexOf("."))
                                            End If
                                        End If

                                        Dim fileSize As Integer = Convert.ToInt32(seFileSize.EditValue) * 1024 * 1024     'Chunk file size is like 2MB (2097152 bytes)
                                        Dim ChunkSize As Integer = 0
                                        Dim strData() As String = GetStringArrayFromDataTable(dtGetMmlRollback, False)

                                        For Index As Integer = 0 To strData.Length - 1
                                            Dim stringByteCount As Integer = System.Text.Encoding.UTF8.GetByteCount(strData(Index))
                                            If (ChunkSize + stringByteCount) <= fileSize Then
                                                ChunkSize = ChunkSize + stringByteCount
                                                If Index = (strData.Length - 1) Then
                                                    OriginalFileName = file_name & "_" & fileCount & "." & objFileDlg.DefaultExt
                                                    outputBytes = New String(Index - offset) {}
                                                    System.Array.Copy(strData, offset, outputBytes, 0, outputBytes.Length)
                                                    System.IO.File.WriteAllLines(OriginalFileName, outputBytes)
                                                    Exit For
                                                End If
                                            Else
                                                Index = Index - 1
                                                OriginalFileName = file_name & "_" & fileCount & "." & objFileDlg.DefaultExt
                                                outputBytes = New String(Index - offset - 1) {}
                                                System.Array.Copy(strData, offset, outputBytes, 0, outputBytes.Length)
                                                System.IO.File.WriteAllLines(OriginalFileName, outputBytes)
                                                offset = Index
                                                fileCount += 1
                                                ChunkSize = 0
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If hasdata = True Then
                XtraMessageBox.Show("MML/XML file created successfully", "Get MML/XML", MessageBoxButtons.OK)
            Else
                XtraMessageBox.Show("MML/XML file not created - no data", "Get MML/XML", MessageBoxButtons.OK)
            End If

            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "Bulk Campaigns Events"

    Private Sub gvTiltCampaigns_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvCampaignsBulk.RowCount > 0 AndAlso e IsNot Nothing Then
                gvCampaignsBulk.ClearSelection()
                gvCampaignsBulk.FocusedRowHandle = e.FocusedRowHandle
                gvCampaignsBulk.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler ceActiveBulk.CheckedChanged, AddressOf ceActiveBulk_CheckedChanged
            RemoveHandler ceIsPublicBulk.CheckedChanged, AddressOf ceIsPublicBulk_CheckedChanged

            LoadImportedData()

            Dim dr As DataRow = gvCampaignsBulk.GetFocusedDataRow()
            If dr IsNot Nothing Then

                lblOwnerBulk.Text = dr("CampaignOwner")
                lblLastRunTimeBulk.Text = IIf(IsDBNull(dr("LastRunTime")), "", dr("LastRunTime").ToString)
                lblLastEndTimeBulk.Text = IIf(IsDBNull(dr("LastEndTime")), "", dr("LastEndTime").ToString)

                If IsDBNull(dr("LastStatus")).ToString = "Running" Then
                    btnRunNowBulk.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowBulk.Text = "Abort Run!"
                Else
                    btnRunNowBulk.LookAndFeel.UseDefaultLookAndFeel = True
                    btnRunNowBulk.Text = "Run Now"
                End If

                Dim drCampaignDetail As DataRow = GetCampaignDetailsByID(dr("CampaignID"))
                If drCampaignDetail IsNot Nothing Then
                    ceActiveBulk.Checked = IIf(IsDBNull(drCampaignDetail("CampaignEnabled")), False, drCampaignDetail("CampaignEnabled"))
                    ceIsPublicBulk.Checked = IIf(IsDBNull(drCampaignDetail("IsPublic")), False, drCampaignDetail("IsPublic"))
                End If

                'Load campaign configuration summary grid
                LoadConfigSummaryGridBulk(dr("CampaignID"))
                LoadResultSetComboBulk(cmbResultSetIDBulk, dr("CampaignID"))
            End If

            lblDataRowCountBulk.Visible = False
            'Enable/disable control if the current user is not the owner of the campaign.
            If lblOwnerBulk.Text.ToLower <> Environment.UserName.ToLower Then
                lblOwnerBulk.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblOwnerBulk.ForeColor = Color.Red
                ceIsPublicBulk.Enabled = False

                If ceIsPublicBulk.Checked Then
                    ceActiveBulk.Enabled = True

                    btnDeleteBulk.Enabled = True
                    grpLayerPropBulk.Enabled = True
                Else
                    ceActiveBulk.Enabled = False
                    btnDeleteBulk.Enabled = False
                    grpLayerPropBulk.Enabled = False
                End If
            Else
                lblOwnerBulk.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblOwnerBulk.ForeColor = Color.Black

                ceIsPublicBulk.Enabled = True
                ceActiveBulk.Enabled = True

                btnDeleteBulk.Enabled = True
                grpLayerPropBulk.Enabled = True
            End If

            AddHandler ceActiveBulk.CheckedChanged, AddressOf ceActiveBulk_CheckedChanged
            AddHandler ceIsPublicBulk.CheckedChanged, AddressOf ceIsPublicBulk_CheckedChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsPublicBulk_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim ceIsPublic As CheckEdit = CType(sender, CheckEdit)
            UpdateCampaignBulk(ceIsPublic.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ceActiveBulk_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim ceActive As CheckEdit = CType(sender, CheckEdit)
            UpdateCampaignBulk(ceActive.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnAddBulk_Click(sender As Object, e As EventArgs) Handles btnAddBulk.Click
        Try
            dlgAddCampaign.CampaignType = "BulkImport"
            If ceIsPublicBulk.Checked Then
                dlgAddCampaign.IsPublic = True
            Else
                dlgAddCampaign.IsPublic = False
            End If
            If dlgAddCampaign.ShowDialog() = DialogResult.OK Then
                LoadCampaignsBulk()
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnRefreshBulk_Click(sender As Object, e As EventArgs) Handles btnRefreshBulk.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If CType(sender, SimpleButton).Tag = "TM_Bulk" Then
                txtSearchBulk.Text = ""
                LoadCampaignsBulk()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCloneBulk_Click(sender As Object, e As EventArgs) Handles btnCloneBulk.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignsBulk.GetRow(rIndex(0)).Row
                dlgCampaignClone.campaignID = dr("CampaignID")
                dlgCampaignClone.campaignType = dr("CampaignType").ToString
                If dlgCampaignClone.ShowDialog() = DialogResult.OK Then
                    LoadCampaignsBulk()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteBulk_Click(sender As Object, e As EventArgs) Handles btnDeleteBulk.Click
        Try
            Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignsBulk.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Tilt Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    DeleteTiltCampaign(dr("CampaignID"), dr("CampaignType").ToString)
                    gvCampaignsBulk.DeleteRow(rIndex(0))
                    If gvCampaignsBulk.RowCount > 0 Then
                        gvCampaignsBulk.ClearSelection()
                        gvCampaignsBulk.SelectRow(0)
                        gvCampaignsBulk.FocusedRowHandle = 0
                        gvTiltCampaigns_FocusedRowChanged(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtSearchBulkImport_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchBulk.KeyUp
        Try
            Dim dtTiltCampBulk As DataTable = CType(gcCampaignsBulk.DataSource, DataTable)
            If dtTiltCampBulk IsNot Nothing Then
                If (txtSearchBulk.Text.Length > 0) Then
                    dtTiltCampBulk.DefaultView.RowFilter = "[CampaignName] Like '%" & txtSearchBulk.Text & "%'"
                Else
                    dtTiltCampBulk.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Try
            Dim ofd As New OpenFileDialog
            ofd.RestoreDirectory = False

            If openFileDirectory Is Nothing Then
                ofd.InitialDirectory = IO.Directory.GetCurrentDirectory()
            Else
                ofd.InitialDirectory = openFileDirectory
            End If
            ofd.DefaultExt = "csv"
            ofd.Filter = "Bulk Import File|*.csv"
            ofd.Title = "Open Bulk Import File"
            ofd.ShowDialog()

            If ofd.FileName <> "" Then
                Dim fileName As String = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\") + 1)
                Dim fileNameExtn As String = fileName.Substring(fileName.LastIndexOf("\") + 1).Split(".")(1)
                If fileNameExtn.ToLower <> "csv" Then
                    SetMessage("File name extension must be CSV only")
                    Exit Sub
                End If
                Dim lines = IO.File.ReadAllLines(ofd.FileName)
                dtBulkImport = New DataTable
                Dim colCount = lines.First.Split(";"c).Length

                'Setting open file dialog initial directory to last path
                openFileDirectory = ofd.FileName.Substring(0, ofd.FileName.Length - fileName.Length)

                'Column count validation
                If colCount <> 2 Then
                    SetMessage("Only 2 columns allowed (CELLNAME;ETILT)")
                    Exit Sub
                End If

                txtImportfileName.Text = fileName.Trim
                Dim campaignID As String = gvCampaignsBulk.GetRowCellValue(gvCampaignsBulk.FocusedRowHandle, "CampaignID")

                If (lines.First.Split(";"c)(0).ToUpper = "CELLNAME" AndAlso lines.First.Split(";"c)(1).ToUpper = "ETILT") Then
                    dtBulkImport.Columns.Add("CampaignID", GetType(String))
                    For i As Int32 = 0 To colCount - 1
                        dtBulkImport.Columns.Add(New DataColumn(lines.First.Split(";"c)(i), GetType(String)))
                    Next

                    For Each line In lines
                        If line.ToString.Trim.ToUpper <> "CELLNAME;ETILT" Then
                            Dim objFields = From field In line.Split(";"c)
                                            Select CType(field, Object)

                            If IsNumeric(objFields(1).ToString) Then
                                Dim newRow = dtBulkImport.Rows.Add()
                                newRow.Item("CampaignID") = campaignID
                                newRow.Item("CELLNAME") = objFields(0).ToString
                                newRow.Item("ETILT") = objFields(1).ToString
                            Else
                                SetMessage("ETILT should be in decimals only (e.g. 3,2 for EU region and 3.2 for US region)")
                                Exit Sub
                            End If
                        End If
                    Next

                Else
                    SetMessage("Column names allowed are CELLNAME and ETILT")
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnImportBulk_Click(sender As Object, e As EventArgs) Handles btnImportBulk.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                If dtBulkImport IsNot Nothing Then
                    DeleteOldImportedDataForCampaignID(CInt(dtBulkImport.Rows(0)(0)))

                    InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[TILT_BulkImport_Input]", dtBulkImport)

                    SetMessage("File imported successfully")
                    txtImportfileName.Text = String.Empty

                    LoadImportedData()
                Else
                    SetMessage("Please select the import csv file")
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            txtImportfileName.Text = String.Empty
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlClient.SqlRowsCopiedEventArgs)
        lblStatus.Text = "Completed - Count: " & args.RowsCopied.ToString
    End Sub

    Private Sub gvConfigSummBulk_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadCellList()
            LoadLayers()
            LoadBandList()

            Dim dr As DataRow = gvConfigSummBulk.GetFocusedDataRow()
            layerPropGridBulk.Tag = Nothing
            layerPropGridBulk.Tag = dr
            LoadLayerPropertiesBulk(layerPropGridBulk, "BulkImport", dr)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnLayerPropertiesAdd_Click(sender As Object, e As EventArgs) Handles btnLayerPropertiesAddBulk.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim campaignID As Integer = gvCampaignsBulk.GetRowCellValue(gvCampaignsBulk.FocusedRowHandle, "CampaignID")
            AddTiltBulkImportConfig(campaignID)
            LoadConfigSummaryGridBulk(campaignID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub BulkImportPropertyGrid_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles layerPropGridBulk.PropertyValueChanged
        Try
            Dim propGrd As PropertyGrid = CType(s, PropertyGrid)
            Dim campaignType As String = "BulkImport"

            Dim changedPropertyItem As GridItem = e.ChangedItem
            If (Not changedPropertyItem Is Nothing) Then
                Dim rIndex2() As Integer = gvCampaignsBulk.GetSelectedRows()
                If propGrd.Tag IsNot Nothing AndAlso rIndex2.Length > 0 Then
                    Dim drConfig As DataRow = CType(propGrd.Tag, DataRow)
                    Dim drCampaign As DataRow = gvCampaignsBulk.GetRow(rIndex2(0)).Row

                    Dim fieldName As String = changedPropertyItem.PropertyDescriptor.Name
                    Dim value As Object = Nothing
                    If changedPropertyItem.PropertyDescriptor.PropertyType = GetType(Boolean) Then
                        value = IIf(changedPropertyItem.Value = True, 1, 0)
                    ElseIf changedPropertyItem.PropertyDescriptor.PropertyType = GetType(Integer) Then
                        value = CInt(changedPropertyItem.Value)
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Exclusion List" Or changedPropertyItem.PropertyDescriptor.Name = "Inclusion List" Then
                        If changedPropertyItem.PropertyDescriptor.Name = "Inclusion List" Then
                            fieldName = "InclusionListId"
                        Else
                            fieldName = "ExclusionListId"
                        End If

                        Dim drList() As DataRow = Nothing
                        drList = dtCellList.Select("ListName='" & changedPropertyItem.Value & "'")
                        If drList.Length > 0 Then
                            value = drList(0).Item("ListID")
                        Else
                            value = 0
                        End If
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Reference Band" Then
                        fieldName = "ReferenceBand"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Target Band" Then
                        fieldName = "TargetBand"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Master Layer" Then
                        fieldName = "MASTERLAYER"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Target Layer" Then
                        fieldName = "TARGETLAYER"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Tilt Lower Limit" Then
                        fieldName = "TILTMIN"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Tilt Upper Limit" Then
                        fieldName = "TILTMAX"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Tilt Rule" Then
                        fieldName = "TILTRULE"
                        value = changedPropertyItem.Value
                    Else
                        value = changedPropertyItem.Value
                    End If

                    If ceApplyConfigAllBulk.Checked Then
                        ApplyConfigPropertyToCampaign(drCampaign.Item("CampaignID"), fieldName, value, campaignType, , True)
                        For iRow As Integer = 0 To gvConfigSummBulk.RowCount - 1
                            gvConfigSummBulk.SetRowCellValue(iRow, gvConfigSummBulk.Columns(fieldName), value)
                        Next
                    Else
                        ApplyConfigPropertyToCampaign(drCampaign.Item("CampaignID"), fieldName, value, campaignType, drConfig.Item("ConfigID"))
                        drConfig.Item(fieldName) = value
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnRunNowBulk_Click(sender As Object, e As EventArgs) Handles btnRunNowBulk.Click
        Try
            Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignsBulk.GetRow(rIndex(0)).Row
                Dim campaignID As Integer = dr("CampaignID")
                Dim campaignType As String = dr("CampaignType").ToString

                If btnRunNowBulk.Text = "Abort Run!" Then
                    objThreadBulk.Abort()
                Else
                    btnRunNowBulk.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowBulk.Text = "Abort Run!"
                    dr("LastStatus") = "Running"
                    gcCampaignsBulk.Refresh()
                    Application.DoEvents()

                    Dim objRunDetect As New RunNowTiltMngr()
                    objRunDetect.campaignID = campaignID
                    objRunDetect.Status = 1
                    objRunDetect.CampaignRow = dr
                    AddHandler objRunDetect.ThreadComplete, AddressOf ExecuteAfterDetectThreadComplete
                    objThreadBulk = New System.Threading.Thread(AddressOf objRunDetect.RunNowBulk)
                    objThreadBulk.Start()

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfterDetectThreadComplete(row As DataRow, Status As Integer, ti As Threading.Thread)
        SyncLock objThreadLockBulk
            GetCampaignsBulk()
            Dim arg() As Object = {row, Status}
            Me.BeginInvoke(New CallThreadInvokedBulk(AddressOf SetBulkCampaignLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetBulkCampaignLastStatus(Row As DataRow, Status As Integer)
        SyncLock objThreadLockBulk
            If Row IsNot Nothing Then
                If Status = 0 Then
                    Row("LastStatus") = "Idle"
                ElseIf Status = 1 Then
                    Row("LastStatus") = "Running"
                ElseIf Status = -1 Then
                    Row("LastStatus") = "Error"
                End If
                gcCampaignsBulk.Refresh()
                btnRunNowBulk.LookAndFeel.UseDefaultLookAndFeel = True
                btnRunNowBulk.Text = "Run Now"
                Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dr As DataRow = gvCampaignsBulk.GetRow(rIndex(0)).Row
                    If Row("CampaignID") = dr("CampaignID") Then
                        LoadResultSetComboBulk(cmbResultSetIDBulk, Row("CampaignID"))
                        LoadResultSummaryDataGridBulk(Row("CampaignID"), cmbResultSetIDBulk.SelectedItem.ToString)
                        xtcTMBulk.SelectedTabPageIndex = 1
                    End If
                End If
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub cmbResultSetIDBulk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResultSetIDBulk.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbResultSetIDBulk.SelectedIndex > 0 Then
                LoadResultSummaryDataGridBulk(gvCampaignsBulk.GetFocusedRowCellValue("CampaignID"), cmbResultSetIDBulk.SelectedItem.ToString.Trim())
                LoadValidationData(gvCampaignsBulk.GetFocusedRowCellValue("CampaignID"), cmbResultSetIDBulk.SelectedItem.ToString.Trim())
                LoadBulkOutputData(cmbResultSetIDBulk.SelectedItem.ToString)
            Else
                IOS.Library.IOSDevExpressGrid.ClearGrid(gcValidationData)
                IOS.Library.IOSDevExpressGrid.ClearGrid(gcSummDataBulk)
                IOS.Library.IOSDevExpressGrid.ClearGrid(gcOutputDataBulk)
                lblDataRowCountBulk.Visible = False
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDetectDataLoadGrid_Click(sender As Object, e As EventArgs) Handles btnDetectDataLoadGrid.Click
        If cmbResultSetIDBulk.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Loading...")
                Dim parray()() As String = {
                    New String() {"@ResultSetID", Chr(39) & cmbResultSetIDBulk.SelectedItem.ToString & Chr(39)},
                    New String() {"@CampaignType", Chr(39) & cmbResultSetIDBulk.Tag & Chr(39)}
                }
                Dim strConnection As String = GetSQL(0, parray)(0)
                Dim sqlParam As String = GetSQL(0, parray)(1)
                Dim dtOutputDataBulk As New DataTable

                dtOutputDataBulk = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcOutputDataBulk, gvOutputDataBulk, dtOutputDataBulk, "ALL")
                lblDataRowCountBulk.Text = "Count of Records: " & gvOutputDataBulk.RowCount
                lblDataRowCountBulk.Visible = True
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Detect Campaign Result Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbResultSetIDBulk.Focus()
        End If
    End Sub

    Private Sub btnBulkDataAllCsv_Click(sender As Object, e As EventArgs) Handles btnDataAllCsvBulk.Click
        If cmbResultSetIDBulk.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Writing data to CSV...")

                Dim dt As DataTable = GetBulkOutputData(cmbResultSetIDBulk.SelectedItem.ToString)

                Dim objFileDlg As New SaveFileDialog()
                objFileDlg.InitialDirectory = IO.Directory.GetCurrentDirectory()
                objFileDlg.Filter = "Comma Delimited|*.csv"
                objFileDlg.Title = "Save a CSV File"
                objFileDlg.FileName = gvCampaignsBulk.GetFocusedRowCellValue("CampaignName") & "_" & cmbResultSetIDBulk.SelectedItem.ToString
                If objFileDlg.ShowDialog() = DialogResult.OK Then
                    If objFileDlg.FileName <> "" Then
                        Dim Content() As Byte = CSVBytesWriter(dt)
                        Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                        fs.Write(Content, 0, Content.Length)
                        fs.Close()
                    End If
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Tilt Manager Result Output Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbResultSetIDBulk.Focus()
        End If
    End Sub

    Private Sub btnResultSetBulk_Click(sender As Object, e As EventArgs) Handles btnDeleteResultSetBulk.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            DeleteCampaignResultSetBulk()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmsCampaigns_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsCampaignsBulk.Opening
        Try
            If lblOwnerBulk.Text.ToLower = Environment.UserName.ToLower Then
                tsmi_RenameCampaignBulk.Enabled = True
            ElseIf lblOwnerBulk.Text.ToLower <> Environment.UserName.ToLower Then
                'Checking whether the current user (not campaign owner) is a power user
                If configMgr.User.IsPowerUser = True Then
                    tsmi_RenameCampaignBulk.Enabled = True
                Else
                    tsmi_RenameCampaignBulk.Enabled = False
                    XtraMessageBox.Show("Current user can't rename the campaign as the campaign owner is a different user.", "Rename Bulk Campaign!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_RenameCampaignBulk_Click(sender As Object, e As EventArgs) Handles tsmi_RenameCampaignBulk.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gvCamp As GridView = CType(cmsCampaignsBulk.SourceControl, GridControl).MainView
            Dim NewCampaignName As String = XtraInputBox.Show("Enter New Bulk Campaign Name: ", "Rename Bulk Campaign", gvCamp.GetFocusedRowCellValue("CampaignName").ToString)
            If Not gvCamp Is Nothing And NewCampaignName <> "" Then
                Dim parray()() As String = {
                    New String() {"@CampaignID", CInt(gvCamp.GetFocusedRowCellValue("CampaignID"))},
                    New String() {"@NewCampaignName", Chr(39) & NewCampaignName & Chr(39)}
                }
                Dim sql As String = GetSQL(4933, parray)(1)
                Dim connstring As String = GetSQL(4933, parray)(0)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                XtraMessageBox.Show("Campaign renamed successfully.", "Rename Bulk Campaign", MessageBoxButtons.OK)

                LoadCampaignsBulk()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnListMngrBulk_Click(sender As Object, e As EventArgs) Handles btnListMngrBulk.Click
        Try
            frmListManager.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tsmi_PasteDataFromClipboard_Click(sender As Object, e As EventArgs) Handles tsmi_PasteDataFromClipboard.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            IsErrorInCopy = False
            gvImportedDataBulk.PasteFromClipboard()
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub gvImportedDataBulk_ClipboardRowPasting(sender As Object, e As ClipboardRowPastingEventArgs) Handles gvImportedDataBulk.ClipboardRowPasting
        Try
            If IsErrorInCopy = True Then
                e.Cancel = True
                Clipboard.Clear()
                Exit Sub
            End If

            If e.OriginalValues.Count > 0 Then
                Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()
                If rIndex.Length > 0 Then
                    dtBulkImport = gcImportedDataBulk.DataSource
                    Dim drCamp As DataRow = gvCampaignsBulk.GetRow(rIndex(0)).Row
                    If e.OriginalValues.Count = 2 Then
                        Dim drData As DataRow
                        drData = dtBulkImport.NewRow()
                        drData(0) = drCamp.Item("CampaignID")
                        drData(1) = e.OriginalValues(0).ToString().Trim()
                        drData(2) = e.OriginalValues(1).ToString().Trim()
                        dtBulkImport.Rows.Add(drData)
                        'lblStatus.Text = "Count of records: " & dt.Rows.Count.ToString
                    ElseIf e.OriginalValues(0).ToString() <> "" Then
                        XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<CELLNAME>;<ETILT>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        e.Cancel = True
                        Clipboard.Clear()
                        IsErrorInCopy = True
                    End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Columns mismatch, columns must be:" & vbNewLine & "<CELLNAME>;<ETILT>" & vbNewLine & vbNewLine & "Do not use headers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Clipboard.Clear()
            IsErrorInCopy = True
        End Try
    End Sub

    Private Sub cmsConfigSummary_Opening(sender As Object, e As CancelEventArgs) Handles cmsConfigSummary.Opening
        Try
            Dim cms As ContextMenuStrip = CType(sender, ContextMenuStrip)
            cmsSourceControl = CType(cms.SourceControl, GridControl)
            If cmsSourceControl IsNot Nothing Then
                Dim Owner As String = ""
                If cmsSourceControl.Tag.ToString.ToUpper = "TM_BULK" Then
                    Owner = lblOwnerBulk.Text
                ElseIf cmsSourceControl.Tag.ToString.ToUpper = "TM_AUDIT" Then
                    Owner = lblOwnerAudit.Text
                End If
                If Owner.ToLower <> Environment.UserName.ToLower And configMgr.User.IsPowerUser = False Then
                    tsmi_DeleteSelectedRows.Enabled = False
                Else
                    tsmi_DeleteSelectedRows.Enabled = True
                End If
            Else
                tsmi_DeleteSelectedRows.Enabled = False
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_DeleteSelectedRows_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteSelectedRows.Click
        Try
            If cmsSourceControl IsNot Nothing Then
                Dim gvConfig As GridView = DirectCast(cmsSourceControl.MainView, GridView)
                Dim drCampaign As DataRow = Nothing

                If XtraMessageBox.Show("Are you sure to delete selected config summary rows?", "Delete Config Summary", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If cmsSourceControl.Tag.ToString.ToUpper = "TM_BULK" Then
                        Dim rIndex2() As Integer = gvCampaignsBulk.GetSelectedRows()
                        If rIndex2.Length > 0 Then
                            drCampaign = gvCampaignsBulk.GetRow(rIndex2(0)).Row
                        End If
                        If drCampaign IsNot Nothing Then
                            Dim rIndex() As Integer = gvConfig.GetSelectedRows()
                            For i As Integer = 0 To rIndex.Length - 1
                                DeleteConfigPropertyBulk(drCampaign.Item("CampaignID"), gvConfig.GetRowCellValue(rIndex(i), "ConfigID"))
                            Next
                            LoadConfigSummaryGridBulk(drCampaign("CampaignID"))
                        End If
                    ElseIf cmsSourceControl.Tag.ToString.ToUpper = "TM_AUDIT" Then
                        Dim rIndex2() As Integer = gvCampaignsAudit.GetSelectedRows()
                        If rIndex2.Length > 0 Then
                            drCampaign = gvCampaignsAudit.GetRow(rIndex2(0)).Row
                        End If
                        If drCampaign IsNot Nothing Then
                            Dim rIndex() As Integer = gvConfig.GetSelectedRows()
                            For i As Integer = 0 To rIndex.Length - 1
                                DeleteConfigPropertyAudit(drCampaign.Item("CampaignID"), gvConfig.GetRowCellValue(rIndex(i), "ConfigID"))
                            Next
                            LoadConfigSummaryGridAudit(drCampaign("CampaignID"))
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ConfigSummGrid_ProcessGridKey(sender As Object, e As KeyEventArgs)
        cmsSourceControl = TryCast(sender, GridControl)
        Dim gvConfig = TryCast(cmsSourceControl.FocusedView, GridView)
        Dim drCampaign As DataRow = Nothing

        If e.KeyData = Keys.Delete Then

            Dim Owner As String = ""
            If cmsSourceControl.Tag.ToString.ToUpper = "TM_BULK" Then
                Owner = lblOwnerBulk.Text
            ElseIf cmsSourceControl.Tag.ToString.ToUpper = "TM_AUDIT" Then
                Owner = lblOwnerAudit.Text
            End If

            If (Owner.ToLower = Environment.UserName.ToLower Or configMgr.User.IsPowerUser = True) Then

                If XtraMessageBox.Show("Are you sure to delete selected config summary rows?", "Delete Config Summary", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If cmsSourceControl.Tag.ToString.ToUpper = "TM_BULK" Then
                        Dim rIndex2() As Integer = gvCampaignsBulk.GetSelectedRows()
                        If rIndex2.Length > 0 Then
                            drCampaign = gvCampaignsBulk.GetRow(rIndex2(0)).Row
                        End If
                        If drCampaign IsNot Nothing Then
                            Dim rIndex() As Integer = gvConfig.GetSelectedRows()
                            For i As Integer = 0 To rIndex.Length - 1
                                DeleteConfigPropertyBulk(drCampaign.Item("CampaignID"), gvConfig.GetRowCellValue(rIndex(i), "ConfigID"))
                            Next
                            LoadConfigSummaryGridBulk(drCampaign("CampaignID"))
                        End If
                    ElseIf cmsSourceControl.Tag.ToString.ToUpper = "TM_AUDIT" Then
                        Dim rIndex2() As Integer = gvCampaignsAudit.GetSelectedRows()
                        If rIndex2.Length > 0 Then
                            drCampaign = gvCampaignsAudit.GetRow(rIndex2(0)).Row
                        End If
                        If drCampaign IsNot Nothing Then
                            Dim rIndex() As Integer = gvConfig.GetSelectedRows()
                            For i As Integer = 0 To rIndex.Length - 1
                                DeleteConfigPropertyAudit(drCampaign.Item("CampaignID"), gvConfig.GetRowCellValue(rIndex(i), "ConfigID"))
                            Next
                            LoadConfigSummaryGridAudit(drCampaign("CampaignID"))
                        End If
                    End If
                    e.Handled = True
                End If
            Else
                XtraMessageBox.Show("Only Campaign Owner or PowerUser can delete.", "Delete Config Summary", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            End If

        End If
    End Sub

    Private Sub cmsLaunchAdHocTiltMngr_Opening(sender As Object, e As CancelEventArgs) Handles cmsLaunchAdHocTiltMngr.Opening
        Try
            Dim cms As ContextMenuStrip = CType(sender, ContextMenuStrip)
            cmsSourceControl = TryCast(cms.SourceControl, GridControl)
            Dim gvOutPutData = TryCast(cmsSourceControl.FocusedView, GridView)

            selectedTiltCampaignName = Nothing

            If gvOutPutData.SelectedRowsCount = 0 Then
                tsmi_AdHocTiltManager.Enabled = False
            Else
                If cmsSourceControl.Tag.ToString.ToUpper = "TM_BULK" Then
                    selectedTiltCampaignName = gvCampaignsBulk.GetFocusedRowCellValue("CampaignName")
                ElseIf cmsSourceControl.Tag.ToString.ToUpper = "TM_AUDIT" Then
                    selectedTiltCampaignName = gvCampaignsAudit.GetFocusedRowCellValue("CampaignName")
                End If

                tsmi_AdHocTiltManager.Enabled = True
                tsmi_AdHocTiltManager.Tag = cmsSourceControl.Tag.ToString
                tsmi_AdHocTiltManager.DropDownItems.Clear()

                'Add tsmi to add new manual tilt campaign
                Dim tsmiAddNewTiltCampaign As ToolStripMenuItem = New ToolStripMenuItem("Add New Tilt Campaign")
                tsmiAddNewTiltCampaign.Tag = cmsSourceControl.Tag.ToString
                AddHandler tsmiAddNewTiltCampaign.Click, AddressOf tsmi_AddNewTiltCampaign_Click
                tsmi_AdHocTiltManager.DropDownItems.Add(tsmiAddNewTiltCampaign)

                tsmi_AdHocTiltManager.DropDownItems.Add(New ToolStripSeparator())

                Dim dt As DataTable = Get_ManualTiltCampaignsList_CurrentUser()

                If dt IsNot Nothing Then
                    For Each dr As DataRow In dt.Rows
                        Dim tsmi As ToolStripMenuItem = New ToolStripMenuItem(dr("CampaignName").ToString)
                        tsmi.Tag = dr("CampaignID").ToString
                        AddHandler tsmi.Click, AddressOf tsmi_TiltCampaignClick
                        tsmi_AdHocTiltManager.DropDownItems.Add(tsmi)
                    Next
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_AddNewTiltCampaign_Click(sender As Object, e As EventArgs) Handles tsmi_AddNewTiltCampaign.Click
        Try
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
            cmsLaunchAdHocTiltMngr.Close()
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            selectedTiltCampaignName = XtraInputBox.Show("Campaign Name: ", "Add New Tilt Campaign", selectedTiltCampaignName & "_AdHoc")
            If selectedTiltCampaignName = "" Then
                Exit Sub
            End If

            selectedTiltCampaignID = Save_ManualTiltCampaign_GetCampaignID()

            Dim gv As GridView = Nothing
            Dim selectedRowsIndex() As Integer = Nothing
            Dim campaignType As String = TryCast(sender, ToolStripMenuItem).Tag

            If campaignType.ToUpper = "TM_BULK" Then
                selectedRowsIndex = gvOutputDataBulk.GetSelectedRows()
                gv = gvOutputDataBulk
            ElseIf campaignType.ToUpper = "TM_AUDIT" Then
                selectedRowsIndex = gvOutputDataAudit.GetSelectedRows()
                gv = gvOutputDataAudit
            End If

            For iCnt As Integer = 0 To selectedRowsIndex.Length - 1

                WaitScreen.ShowWaitScreen("Submitting MBTS: " & gv.GetRowCellValue(selectedRowsIndex(iCnt), ("MBTSNAME")).ToString & " And Sector ID: " & gv.GetRowCellValue(selectedRowsIndex(iCnt), "SECTORID").ToString & " to Campaign: " & selectedTiltCampaignName)
                Application.DoEvents()

                'Delete Tilt Manual for the selected campaignID, NENAME & SECTORID set
                clsSQLCommands.DeleteTiltManual(connStrIOSServer, selectedTiltCampaignID, gv.GetRowCellValue(selectedRowsIndex(iCnt), "MBTSNAME").ToString, CInt(gv.GetRowCellValue(selectedRowsIndex(iCnt), "SECTORID")))

                'Insert Tilt Campaign Manual for the selected campaignID, NENAME & SECTORID set
                Insert_TiltCampaign_Manual(gv.GetRowCellValue(selectedRowsIndex(iCnt), "MBTSNAME").ToString, CInt(gv.GetRowCellValue(selectedRowsIndex(iCnt), "SECTORID")))

                WaitScreen.CloseWaitScreen()
                Application.DoEvents()

            Next

            frmMDI.OpenFormAsDockPanel("Ad Hoc Tilt Manager")

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_TiltCampaignClick(sender As Object, e As EventArgs)
        Try
            selectedTiltCampaignID = TryCast(sender, ToolStripMenuItem).Tag
            'If objFrmAdHocTiltMngr Is Nothing Then
            '    objFrmAdHocTiltMngr = New frmTiltManagement()
            'End If

            Dim gv As GridView = Nothing
            Dim selectedRowsIndex() As Integer = Nothing
            Dim campaignType As String = tsmi_AdHocTiltManager.Tag

            If campaignType.ToUpper = "TM_BULK" Then
                selectedRowsIndex = gvOutputDataBulk.GetSelectedRows()
                gv = gvOutputDataBulk
            ElseIf campaignType.ToUpper = "TM_AUDIT" Then
                selectedRowsIndex = gvOutputDataAudit.GetSelectedRows()
                gv = gvOutputDataAudit
            End If

            For iCnt As Integer = 0 To selectedRowsIndex.Length - 1

                WaitScreen.ShowWaitScreen("Submitting MBTS: " & gv.GetRowCellValue(selectedRowsIndex(iCnt), ("MBTSNAME")).ToString & " And Sector ID: " & gv.GetRowCellValue(selectedRowsIndex(iCnt), ("SECTORID")).ToString & " to Campaign: " & selectedTiltCampaignName)
                Application.DoEvents()

                'Delete Tilt Manual for the selected campaignID, NENAME & SECTORID set
                clsSQLCommands.DeleteTiltManual(connStrIOSServer, selectedTiltCampaignID, gv.GetRowCellValue(selectedRowsIndex(iCnt), ("MBTSNAME")).ToString, CInt(gv.GetRowCellValue(selectedRowsIndex(iCnt), ("SECTORID"))))

                'Insert Tilt Campaign Manual for the selected campaignID, NENAME & SECTORID set
                Insert_TiltCampaign_Manual(gv.GetRowCellValue(selectedRowsIndex(iCnt), "MBTSNAME").ToString, CInt(gv.GetRowCellValue(selectedRowsIndex(iCnt), "SECTORID")))

                WaitScreen.CloseWaitScreen()
                Application.DoEvents()

            Next

            Me.FillSectorListForCampaign()
            'frmMDI.OpenFormAsDockPanel("Ad Hoc Tilt Manager")
            Me.xtcMain.SelectedTabPageIndex = 0
            Me.SetManualCampaignComboBox()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Audit Campaigns Events"

    Private Sub gvCampaignsAudit_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If gvCampaignsAudit.RowCount > 0 AndAlso e IsNot Nothing Then
                gvCampaignsAudit.ClearSelection()
                gvCampaignsAudit.FocusedRowHandle = e.FocusedRowHandle
                gvCampaignsAudit.SelectRow(e.FocusedRowHandle)
            End If
            Application.DoEvents()

            RemoveHandler ceActiveAudit.CheckedChanged, AddressOf ceActiveAudit_CheckedChanged
            RemoveHandler ceIsPublicAudit.CheckedChanged, AddressOf ceIsPublicAudit_CheckedChanged

            LoadInputData()

            Dim dr As DataRow = gvCampaignsAudit.GetFocusedDataRow()
            If dr IsNot Nothing Then

                lblOwnerAudit.Text = dr("CampaignOwner")
                lblLastRunTimeAudit.Text = IIf(IsDBNull(dr("LastRunTime")), "", dr("LastRunTime").ToString)
                lblLastEndTimeAudit.Text = IIf(IsDBNull(dr("LastEndTime")), "", dr("LastEndTime").ToString)

                If IsDBNull(dr("LastStatus")).ToString = "Running" Then
                    btnRunNowAudit.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowAudit.Text = "Abort Run!"
                Else
                    btnRunNowAudit.LookAndFeel.UseDefaultLookAndFeel = True
                    btnRunNowAudit.Text = "Run Now"
                End If

                Dim drCampaignDetail As DataRow = GetCampaignDetailsByID(dr("CampaignID"))
                If drCampaignDetail IsNot Nothing Then
                    ceActiveAudit.Checked = IIf(IsDBNull(drCampaignDetail("CampaignEnabled")), False, drCampaignDetail("CampaignEnabled"))
                    ceIsPublicAudit.Checked = IIf(IsDBNull(drCampaignDetail("IsPublic")), False, drCampaignDetail("IsPublic"))
                End If

                'Load campaign configuration summary grid
                LoadConfigSummaryGridAudit(dr("CampaignID"))
                LoadResultSetComboAudit(cmbResultSetIDAudit, dr("CampaignID"))
            End If

            lblDataRowCountAudit.Visible = False
            'Enable/disable control if the current user is not the owner of the campaign.
            If lblOwnerAudit.Text.ToLower <> Environment.UserName.ToLower Then
                lblOwnerAudit.Font = New Font("Tahoma", 8.25, FontStyle.Bold)
                lblOwnerAudit.ForeColor = Color.Red
                ceIsPublicAudit.Enabled = False

                If ceIsPublicAudit.Checked Then
                    ceActiveAudit.Enabled = True

                    btnDeleteAudit.Enabled = True
                    grpLayerPropAudit.Enabled = True
                Else
                    ceActiveAudit.Enabled = False
                    btnDeleteAudit.Enabled = False
                    grpLayerPropAudit.Enabled = False
                End If
            Else
                lblOwnerAudit.Font = New Font("Tahoma", 8.25, FontStyle.Regular)
                lblOwnerAudit.ForeColor = Color.Black

                ceIsPublicAudit.Enabled = True
                ceActiveAudit.Enabled = True

                btnDeleteAudit.Enabled = True
                grpLayerPropAudit.Enabled = True
            End If

            AddHandler ceActiveAudit.CheckedChanged, AddressOf ceActiveAudit_CheckedChanged
            AddHandler ceIsPublicAudit.CheckedChanged, AddressOf ceIsPublicAudit_CheckedChanged

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ceIsPublicAudit_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim ceIsPublic As CheckEdit = CType(sender, CheckEdit)
            UpdateCampaignAudit(ceIsPublic.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ceActiveAudit_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim ceActive As CheckEdit = CType(sender, CheckEdit)
            UpdateCampaignAudit(ceActive.Tag)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnRefreshAudit_Click(sender As Object, e As EventArgs) Handles btnRefreshAudit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If CType(sender, SimpleButton).Tag = "TM_Audit" Then
                txtSearchAudit.Text = ""
                LoadCampaignsAudit()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnCloneAudit_Click(sender As Object, e As EventArgs) Handles btnCloneAudit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim rIndex() As Integer = gvCampaignsAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignsAudit.GetRow(rIndex(0)).Row
                dlgCampaignClone.campaignID = dr("CampaignID")
                dlgCampaignClone.campaignType = dr("CampaignType").ToString
                If dlgCampaignClone.ShowDialog() = DialogResult.OK Then
                    LoadCampaignsAudit()
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteAudit_Click(sender As Object, e As EventArgs) Handles btnDeleteAudit.Click
        Try
            Dim rIndex() As Integer = gvCampaignsAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignsAudit.GetRow(rIndex(0)).Row
                If XtraMessageBox.Show("Are you sure to delete campaign name: " & dr("CampaignName").ToString & "?", "Delete Audit Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    DeleteTiltCampaign(dr("CampaignID"), dr("CampaignType").ToString)
                    gvCampaignsAudit.DeleteRow(rIndex(0))
                    If gvCampaignsAudit.RowCount > 0 Then
                        gvCampaignsAudit.ClearSelection()
                        gvCampaignsAudit.SelectRow(0)
                        gvCampaignsAudit.FocusedRowHandle = 0
                        gvCampaignsAudit_FocusedRowChanged(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtSearchAudit_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchAudit.KeyUp
        Try
            Dim dtTiltCampAudit As DataTable = CType(gcCampaignsAudit.DataSource, DataTable)
            If dtTiltCampAudit IsNot Nothing Then
                If (txtSearchAudit.Text.Length > 0) Then
                    dtTiltCampAudit.DefaultView.RowFilter = "[CampaignName] Like '%" & txtSearchAudit.Text & "%'"
                Else
                    dtTiltCampAudit.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub gvConfigSummAudit_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            LoadCellList()
            LoadLayers()
            LoadBandList()

            Dim dr As DataRow = gvConfigSummAudit.GetFocusedDataRow()
            layerPropGridAudit.Tag = Nothing
            layerPropGridAudit.Tag = dr
            LoadLayerPropertiesAudit(layerPropGridAudit, "Audit", dr)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub AuditPropertyGrid_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles layerPropGridAudit.PropertyValueChanged
        Try
            Dim propGrd As PropertyGrid = CType(s, PropertyGrid)
            Dim campaignType As String = "Audit"

            Dim changedPropertyItem As GridItem = e.ChangedItem
            If (Not changedPropertyItem Is Nothing) Then
                Dim rIndex2() As Integer = gvCampaignsAudit.GetSelectedRows()
                If propGrd.Tag IsNot Nothing AndAlso rIndex2.Length > 0 Then
                    Dim drConfig As DataRow = CType(propGrd.Tag, DataRow)
                    Dim drCampaign As DataRow = gvCampaignsAudit.GetRow(rIndex2(0)).Row

                    Dim fieldName As String = changedPropertyItem.PropertyDescriptor.Name
                    Dim value As Object = Nothing
                    If changedPropertyItem.PropertyDescriptor.PropertyType = GetType(Boolean) Then
                        value = IIf(changedPropertyItem.Value = True, 1, 0)
                    ElseIf changedPropertyItem.PropertyDescriptor.PropertyType = GetType(Integer) Then
                        value = CInt(changedPropertyItem.Value)
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Exclusion List" Or changedPropertyItem.PropertyDescriptor.Name = "Inclusion List" Then
                        If changedPropertyItem.PropertyDescriptor.Name = "Inclusion List" Then
                            fieldName = "InclusionListId"
                        Else
                            fieldName = "ExclusionListId"
                        End If

                        Dim drList() As DataRow = Nothing
                        drList = dtCellList.Select("ListName='" & changedPropertyItem.Value & "'")
                        If drList.Length > 0 Then
                            value = drList(0).Item("ListID")
                        Else
                            value = 0
                        End If
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Reference Band" Then
                        fieldName = "ReferenceBand"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Target Band" Then
                        fieldName = "TargetBand"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Master Layer" Then
                        fieldName = "MASTERLAYER"
                        value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Target Layer" Then
                        fieldName = "TARGETLAYER"
                        value = changedPropertyItem.Value
                        'ElseIf changedPropertyItem.PropertyDescriptor.Name = "Tilt Lower Limit" Then
                        '    fieldName = "TILTMIN"
                        '    value = changedPropertyItem.Value
                        'ElseIf changedPropertyItem.PropertyDescriptor.Name = "Tilt Upper Limit" Then
                        '    fieldName = "TILTMAX"
                        '    value = changedPropertyItem.Value
                    ElseIf changedPropertyItem.PropertyDescriptor.Name = "Tilt Rule" Then
                        fieldName = "TILTRULE"
                        value = changedPropertyItem.Value
                    Else
                        value = changedPropertyItem.Value
                    End If

                    If ceApplyConfigAllAudit.Checked Then
                        ApplyConfigPropertyToCampaign(drCampaign.Item("CampaignID"), fieldName, value, campaignType, , True)
                        For iRow As Integer = 0 To gvConfigSummAudit.RowCount - 1
                            gvConfigSummAudit.SetRowCellValue(iRow, gvConfigSummAudit.Columns(fieldName), value)
                        Next
                    Else
                        ApplyConfigPropertyToCampaign(drCampaign.Item("CampaignID"), fieldName, value, campaignType, drConfig.Item("ConfigID"))
                        drConfig.Item(fieldName) = value
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub btnRunNowAudit_Click(sender As Object, e As EventArgs) Handles btnRunNowAudit.Click
        Try
            Dim rIndex() As Integer = gvCampaignsAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvCampaignsAudit.GetRow(rIndex(0)).Row
                Dim campaignID As Integer = dr("CampaignID")
                Dim campaignType As String = dr("CampaignType").ToString

                If btnRunNowAudit.Text = "Abort Run!" Then
                    objThreadAudit.Abort()
                Else
                    btnRunNowAudit.LookAndFeel.UseDefaultLookAndFeel = False
                    btnRunNowAudit.Text = "Abort Run!"
                    dr("LastStatus") = "Running"
                    gcCampaignsAudit.Refresh()
                    Application.DoEvents()

                    Dim objRunAudit As New RunNowTiltMngr()
                    objRunAudit.campaignID = campaignID
                    objRunAudit.Status = 1
                    objRunAudit.CampaignRow = dr
                    AddHandler objRunAudit.ThreadCompleteAudit, AddressOf ExecuteAfterDetectThreadCompleteAudit
                    objThreadAudit = New System.Threading.Thread(AddressOf objRunAudit.RunNowAudit)
                    objThreadAudit.Start()

                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteAfterDetectThreadCompleteAudit(row As DataRow, Status As Integer, ti As Threading.Thread)
        SyncLock objThreadLockAudit
            GetCampaignsAudit()
            Dim arg() As Object = {row, Status}
            Me.BeginInvoke(New CallThreadInvokedAudit(AddressOf SetAuditCampaignLastStatus), arg)
        End SyncLock
    End Sub

    Private Sub SetAuditCampaignLastStatus(Row As DataRow, Status As Integer)
        SyncLock objThreadLockAudit
            If Row IsNot Nothing Then
                If Status = 0 Then
                    Row("LastStatus") = "Idle"
                ElseIf Status = 1 Then
                    Row("LastStatus") = "Running"
                ElseIf Status = -1 Then
                    Row("LastStatus") = "Error"
                End If
                gcCampaignsAudit.Refresh()
                btnRunNowAudit.LookAndFeel.UseDefaultLookAndFeel = True
                btnRunNowAudit.Text = "Run Now"
                Dim rIndex() As Integer = gvCampaignsAudit.GetSelectedRows()
                If rIndex.Length > 0 Then
                    Dim dr As DataRow = gvCampaignsAudit.GetRow(rIndex(0)).Row
                    If Row("CampaignID") = dr("CampaignID") Then
                        LoadResultSetComboAudit(cmbResultSetIDAudit, Row("CampaignID"))
                        LoadResultSummaryDataGridBulk(Row("CampaignID"), cmbResultSetIDAudit.SelectedItem.ToString)
                        xtcTMAudit.SelectedTabPageIndex = 1
                    End If
                End If
                Application.DoEvents()
            End If
        End SyncLock
    End Sub

    Private Sub cmbResultSetIDAudit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResultSetIDAudit.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If cmbResultSetIDAudit.SelectedIndex > 0 Then
                LoadResultSummaryDataGridAudit(gvCampaignsAudit.GetFocusedRowCellValue("CampaignID"), cmbResultSetIDAudit.SelectedItem.ToString.Trim())
                LoadValidationDataAudit(gvCampaignsAudit.GetFocusedRowCellValue("CampaignID"), cmbResultSetIDAudit.SelectedItem.ToString.Trim())
                LoadOutputDataAudit(cmbResultSetIDAudit.SelectedItem.ToString)
            Else
                IOS.Library.IOSDevExpressGrid.ClearGrid(gcValidationDataAudit)
                IOS.Library.IOSDevExpressGrid.ClearGrid(gcSummDataAudit)
                IOS.Library.IOSDevExpressGrid.ClearGrid(gcOutputDataAudit)
                lblDataRowCountAudit.Visible = False
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnDeleteResultSetAudit_Click(sender As Object, e As EventArgs) Handles btnDeleteResultSetAudit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            DeleteCampaignResultSetAudit()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub btnListMngrAudit_Click(sender As Object, e As EventArgs) Handles btnListMngrAudit.Click
        Try
            frmListManager.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDataAllCsvAudit_Click(sender As Object, e As EventArgs) Handles btnDataAllCsvAudit.Click
        If cmbResultSetIDAudit.SelectedIndex > 0 Then
            Try
                WaitScreen.ShowWaitScreen("Writing data to CSV...")

                Dim dt As DataTable = GetOutputDataAudit(cmbResultSetIDAudit.SelectedItem.ToString)

                Dim objFileDlg As New SaveFileDialog()
                objFileDlg.InitialDirectory = IO.Directory.GetCurrentDirectory()
                objFileDlg.Filter = "Comma Delimited|*.csv"
                objFileDlg.Title = "Save a CSV File"
                objFileDlg.FileName = gvCampaignsAudit.GetFocusedRowCellValue("CampaignName") & "_" & cmbResultSetIDAudit.SelectedItem.ToString
                If objFileDlg.ShowDialog() = DialogResult.OK Then
                    If objFileDlg.FileName <> "" Then
                        Dim Content() As Byte = CSVBytesWriter(dt)
                        Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                        fs.Write(Content, 0, Content.Length)
                        fs.Close()
                    End If
                End If
            Catch ex As Exception
                UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
                _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
            Finally
                WaitScreen.CloseWaitScreen()
            End Try
        Else
            XtraMessageBox.Show("Select Result Set ID first!", "Tilt Manager Result Output Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbResultSetIDAudit.Focus()
        End If
    End Sub

    Private Sub cmsCampaignsAudit_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsCampaignsAudit.Opening
        Try
            If lblOwnerAudit.Text.ToLower = Environment.UserName.ToLower Then
                tsmi_RenameCampaignAudit.Enabled = True
            ElseIf lblOwnerAudit.Text.ToLower <> Environment.UserName.ToLower Then
                'Checking whether the current user (not campaign owner) is a power user
                If configMgr.User.IsPowerUser = True Then
                    tsmi_RenameCampaignAudit.Enabled = True
                Else
                    tsmi_RenameCampaignAudit.Enabled = False
                    XtraMessageBox.Show("Current user can't rename the campaign as the campaign owner is a different user.", "Rename Audit Campaign!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub tsmi_RenameCampaignAudit_Click(sender As Object, e As EventArgs) Handles tsmi_RenameCampaignAudit.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim gvCamp As GridView = CType(cmsCampaignsAudit.SourceControl, GridControl).MainView
            Dim NewCampaignName As String = XtraInputBox.Show("Enter New Audit Campaign Name: ", "Rename Audit Campaign", gvCamp.GetFocusedRowCellValue("CampaignName").ToString)
            If Not gvCamp Is Nothing And NewCampaignName <> "" Then
                Dim parray()() As String = {
                    New String() {"@CampaignID", CInt(gvCamp.GetFocusedRowCellValue("CampaignID"))},
                    New String() {"@NewCampaignName", Chr(39) & NewCampaignName & Chr(39)}
                }
                Dim sql As String = GetSQL(4933, parray)(1)
                Dim connstring As String = GetSQL(4933, parray)(0)
                IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(connstring, sql)
                XtraMessageBox.Show("Campaign renamed successfully.", "Rename Audit Campaign", MessageBoxButtons.OK)

                LoadCampaignsAudit()
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnLayerPropertiesAddAudit_Click(sender As Object, e As EventArgs) Handles btnLayerPropertiesAddAudit.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim campaignID As Integer = gvCampaignsAudit.GetRowCellValue(gvCampaignsAudit.FocusedRowHandle, "CampaignID")
            AddTiltAuditImportConfig(campaignID)
            LoadConfigSummaryGridAudit(campaignID)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#End Region

#Region "Methods"

#Region "Ad Hoc Tilt Manager Methods"

    Public Sub DeleteTiltManual()
        Dim drSectorList As DataRow = gvSectorList.GetDataRow(gvSectorList.FocusedRowHandle)
        clsSQLCommands.DeleteTiltManual(connStrIOSServer, selectedTiltCampaignID, drSectorList("MBTSNAME").ToString, CInt(drSectorList("SectorID")))
    End Sub

    Private Sub LoadData_Into_Tilt_Manual_Input()
        Dim neName As String = Nothing
        Dim MBTSNAME As String = Nothing
        Dim sectorID As Integer = 0

        Dim antennaType As String = Nothing
        Dim antennaBand As String = Nothing
        Dim azimuth As Integer = 0
        Dim mTilt As String = Nothing
        Dim deviceName As String = Nothing
        Dim deviceNo As String = Nothing
        Dim iosLayer As String = Nothing
        Dim includeInPlan As String = Nothing
        Dim tiltRuleRET As String = Nothing
        Dim tiltRuleCells As String = Nothing
        Dim vBeamAngle As String = Nothing
        Dim eTilt_Current As String = Nothing
        Dim eTilt_Planned As String = Nothing
        Dim cellName As String = Nothing
        Dim cellID As String = Nothing
        Dim technology As String = Nothing
        Dim locationID As String = Nothing
        Dim x As String = Nothing
        Dim y As String = Nothing
        Dim radiationCenter As String = Nothing

        Dim retDevicesTln As TreeListNode = Nothing
        Dim cellsTln As TreeListNode = Nothing

        Dim sectorListRow As DataRow = gvSectorList.GetFocusedDataRow()
        If sectorListRow IsNot Nothing Then
            MBTSNAME = sectorListRow("MBTSNAME").ToString
            sectorID = CInt(sectorListRow("SectorID"))

            For Each antennaTln As TreeListNode In tlTiltManager.Nodes
                'If antennaTln.Level = 0 Then
                antennaType = antennaTln("AntennaType").ToString
                azimuth = antennaTln("Azimuth").ToString
                mTilt = antennaTln("MTilt").ToString

                If antennaTln.Nodes.Count > 0 Then

                    For iCnt = 0 To antennaTln.Nodes.Count - 1
                        retDevicesTln = antennaTln.Nodes(iCnt)

                        deviceName = antennaTln.Nodes(iCnt)("DeviceName").ToString
                        eTilt_Current = retDevicesTln("ETilt").ToString
                        deviceNo = retDevicesTln("DeviceNo").ToString
                        includeInPlan = IIf(retDevicesTln("IncludeInPlan").ToString.ToUpper = "YES", "1", "0")
                        eTilt_Planned = Math.Round(CDec(IIf(retDevicesTln("ETiltPlanned").ToString <> "", retDevicesTln("ETiltPlanned").ToString, eTilt_Current)), 1)
                        tiltRuleRET = retDevicesTln("Rule").ToString

                        If retDevicesTln.Nodes.Count > 0 Then

                            For jCnt = 0 To retDevicesTln.Nodes.Count - 1
                                cellsTln = retDevicesTln.Nodes(jCnt)
                                If tiltRuleRET.ToUpper = "MASTER" Then
                                    tiltRuleCells = cellsTln("Rule").ToString
                                End If
                                technology = cellsTln("Technology").ToString
                                locationID = cellsTln("LocationID").ToString
                                iosLayer = cellsTln("Layer").ToString
                                vBeamAngle = cellsTln("Vangle").ToString
                                cellName = cellsTln("CellName").ToString
                                cellID = cellsTln("CellID").ToString
                                x = cellsTln("X").ToString
                                y = cellsTln("Y").ToString
                                radiationCenter = cellsTln("RADIATIONCENTER").ToString

                                Dim dtCells As DataTable = dtTreeData.Select("CELLNAME=" & Chr(39) & cellName & Chr(39) & " And mbtsname=" & Chr(39) & MBTSNAME & Chr(39) & " And deviceNo=" & Chr(39) & deviceNo & Chr(39)).CopyToDataTable.DefaultView.ToTable()

                                If tiltRuleRET.ToUpper = "MASTER" Then
                                    clsSQLCommands.LoadData_Into_Tilt_Manual_Input(connStrIOSServer, selectedTiltCampaignID, technology, locationID, dtCells(0)("nename").ToString, MBTSNAME, sectorID, antennaType, dtCells(0)("antennaband").ToString, azimuth, mTilt, deviceName, deviceNo, iosLayer, cellName, cellID, includeInPlan, tiltRuleCells, vBeamAngle, eTilt_Current, eTilt_Planned, x, y, radiationCenter, dtCells(0)("DEVICELINKEDTO").ToString)
                                Else
                                    clsSQLCommands.LoadData_Into_Tilt_Manual_Input(connStrIOSServer, selectedTiltCampaignID, technology, locationID, dtCells(0)("nename").ToString, MBTSNAME, sectorID, antennaType, dtCells(0)("antennaband").ToString, azimuth, mTilt, deviceName, deviceNo, iosLayer, cellName, cellID, includeInPlan, tiltRuleRET, vBeamAngle, eTilt_Current, eTilt_Planned, x, y, radiationCenter, dtCells(0)("DEVICELINKEDTO").ToString)
                                End If
                            Next

                        End If
                    Next

                End If
            Next

        End If
    End Sub

    Private Sub ExecuteCalculateETilt()
        Dim drSectorList As DataRow = gvSectorList.GetDataRow(gvSectorList.FocusedRowHandle)
        clsSQLCommands.ExecuteCalculateETilt(connStrIOSServer, selectedTiltCampaignID, drSectorList("MBTSNAME").ToString, drSectorList("SectorID").ToString)
    End Sub

    Private Sub ManageButtons(ByVal val As Boolean)
        btnCalculateAndSave.Enabled = val
        btnGenerateTiltCampaign.Enabled = val
        tglPlanned.Enabled = val
        tbcETiltSlider.Enabled = val
        txtETiltValue.Enabled = val
        tglPlanned.ToggleState = CheckState.Unchecked
        tglPlanned.Text = "Current"
    End Sub

    Private Sub LoadTiltCampaigns()
        RemoveHandler cmbManualCampaign.SelectedIndexChanged, AddressOf cmbManualCampaign_SelectedIndexChanged
        Dim dt As DataTable = clsSQLCommands.GetTiltCampaigns(connStrIOSServer)
        BindDevExComboBoxWithValueMember(cmbManualCampaign, dt, "CampaignID", "CampaignName", "Select Campaign", False)
        AddHandler cmbManualCampaign.SelectedIndexChanged, AddressOf cmbManualCampaign_SelectedIndexChanged
        SetManualCampaignComboBox()
    End Sub

    Public Sub SetManualCampaignComboBox()
        If selectedTiltCampaignID <> 0 Then
            SetComboBox(cmbManualCampaign, ComboSelectBased.ValueBased, selectedTiltCampaignID)
        End If
    End Sub

    Public Sub FillSelectedSectorList()
        RemoveHandler gvSectorList.FocusedRowChanged, AddressOf gvSectorList_FocusedRowChanged
        If Me.selectedCellsOnMap IsNot Nothing Then
            Dim dt As DataTable = clsSQLCommands.GetSectorListForSelectedCells(connStrIOSServer, Me.selectedCellsOnMap)
            IOSDevExpressGrid.PopulateDataInGrid(gcSectorList, gvSectorList, dt, "ALL", Nothing, "MBTSNAME")
        End If
        AddHandler gvSectorList.FocusedRowChanged, AddressOf gvSectorList_FocusedRowChanged
        gvSectorList_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub GetTreeListData()
        RemoveHandler tlTiltManager.FocusedNodeChanged, AddressOf tlTiltManager_FocusedNodeChanged
        RemoveHandler tlTiltManager.NodeCellStyle, AddressOf tlTiltManager_NodeCellStyle
        RemoveHandler tlTiltManager.CustomNodeCellEdit, AddressOf tlTiltManager_CustomNodeCellEdit
        RemoveHandler tlTiltManager.CellValueChanged, AddressOf tlTiltManager_CellValueChanged
        RemoveHandler ToolTipController1.GetActiveObjectInfo, AddressOf ToolTipController1_GetActiveObjectInfo

        Dim drSectorList As DataRow = gvSectorList.GetDataRow(gvSectorList.FocusedRowHandle)

        If drSectorList IsNot Nothing Then
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@CampaignID", selectedTiltCampaignID},
                New String() {"@MBTSNAME", Chr(39) & CStr(drSectorList("MBTSNAME")) & Chr(39)},
                New String() {"@SECTORID", drSectorList("SectorID")}
            }
            strConnection = GetSQL(4900, parray)(0)
            sqlParam = GetSQL(4900, parray)(1)

            dtTreeData = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            LoadTreeList(dtTreeData)

            riCmbIncInPlan = New RepositoryItemComboBox()
            Dim planItems As String() = {"Select Plan", "YES", "NO"}
            riCmbIncInPlan.Items.AddRange(planItems)

            riCmbRule = New RepositoryItemComboBox()
            Dim ruleItems As String() = {"Select Rule", "MANUAL", "MASTER", "MATCH VBEAM", "MATCH TILT", "LINKED", "NONE"}
            riCmbRule.Items.AddRange(ruleItems)

            AddHandler tlTiltManager.ValidatingEditor, AddressOf tlTiltManager_ValidatingEditor
            AddHandler tlTiltManager.CustomNodeCellEdit, AddressOf tlTiltManager_CustomNodeCellEdit
            AddHandler tlTiltManager.NodeCellStyle, AddressOf tlTiltManager_NodeCellStyle
            AddHandler tlTiltManager.FocusedNodeChanged, AddressOf tlTiltManager_FocusedNodeChanged
            AddHandler tlTiltManager.CellValueChanged, AddressOf tlTiltManager_CellValueChanged

            If dtTreeData.Rows.Count > 0 Then
                Dim drFirst As DataRow = Nothing
                Dim drMasterData() As DataRow = Nothing
                Dim fnode As TreeListNode = tlTiltManager.FocusedNode

                drMasterData = dtTreeData.Select("AntennaType='" & fnode("AntennaType") & "' And TiltRule='" & "MASTER" & "'")

                If drMasterData.Length > 0 Then
                    drFirst = drMasterData(0)
                Else
                    drFirst = dtTreeData.Select("AntennaType='" & tlTiltManager.FocusedNode("AntennaType") & "' And DeviceName='" & tlTiltManager.FocusedNode.Nodes(0)("DeviceName") & "' And IOS_Layer='" & tlTiltManager.FocusedNode.Nodes(0).Nodes(0)("Layer") & "'")(0)
                End If

                'For getting not null X, Y and RadiatonCenter column values
                Dim drNotNullObjects As DataRow = dtTreeData.Select("AntennaType='" & tlTiltManager.FocusedNode("AntennaType") & "' And DeviceName='" & drFirst("DeviceName") & "'").Where(Function(x) x("X") IsNot DBNull.Value)(0)

                If tglPlanned.Text.ToString.ToUpper = "PLANNED" Then
                    For Each drTreeData In dtTreeData.Rows
                        drTreeData("HPDW") = Math.Round(CDbl(IIf(IsDBNull(drTreeData("ETILT_Planned")), drTreeData("ETILT"), drTreeData("ETILT_Planned")) - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                    Next
                    dtTreeData.AcceptChanges()
                ElseIf tglPlanned.Text.ToString.ToUpper = "CURRENT" Then
                    For Each drTreeData In dtTreeData.Rows
                        drTreeData("HPDW") = Math.Round(CDbl(drTreeData("ETILT") - CDbl(drTreeData("VBeamAngle") / 2)), 2)
                    Next
                    dtTreeData.AcceptChanges()
                End If

                frmMapWindow.TiltManager_Cell_AntennaTiltCoverage_New(dtTreeData)

                'drFirst("CellName").ToString, nZ(drNotNullObjects("X"), 0), nZ(drNotNullObjects("Y"), 0), drFirst("AntennaBand"), CDbl(nZ(drFirst("AZIMUTH"), 0)), CDbl(nZ(drNotNullObjects("RadiationCenter"), 0)), CDbl(nZ(drFirst("ETilt").ToString, 0)), CDbl(nZ(drFirst("MTilt").ToString, 0)), CDbl(nZ(drFirst("ETilt_Planned"), 99)), 0, 0, CType(tlTiltManager.FocusedNode.Nodes(0).Tag, Color)
                'tlTiltManager.Nodes(0).Expand()
                'tlTiltManager.Nodes(0).Nodes(0).Expand()
                tlTiltManager.ExpandAll()
                btnManageTree.Text = "Collapse Tree"

                If IsDBNull(drFirst("ETilt_Planned")) Then
                    tbcETiltSlider.EditValue = CDbl(drFirst("ETilt")) * 10
                Else
                    tbcETiltSlider.EditValue = CDbl(IIf(drFirst("ETilt_Planned") = 0.0, drFirst("ETilt"), drFirst("ETilt_Planned"))) * 10
                End If

                lbl_EtiltPlanned.Text = Math.Round(CDbl(tbcETiltSlider.EditValue) / 10.0, 1).ToString("F1")
            End If
        Else
            ch_TiltManager.SeriesCollection.Clear()
            ch_TiltManager.Annotations.Clear()
            ch_TiltManager.ClearAll()
            ch_TiltManager.Annotations.Clear()
            ch_TiltManager.Refresh()
            tlTiltManager.Nodes.Clear()
            tlTiltManager.Refresh()
        End If
        AddHandler ToolTipController1.GetActiveObjectInfo, AddressOf ToolTipController1_GetActiveObjectInfo
    End Sub

    Private Sub LoadTreeList(ByRef dt As DataTable)
        Try
            dt.Columns.Add("HPDW", GetType(String))
            dt.Columns.Add("CellColor", GetType(Color))

            tlTiltManager.BeginUnboundLoad()
            tlTiltManager.SuspendLayout()

            ' Clear all nodes
            tlTiltManager.Nodes.Clear()
            Dim rootNode As TreeListNode = Nothing

            Dim dvAntennas As DataView = New DataView(dt)
            Dim cols(2) As String
            cols(0) = "AntennaType"
            cols(1) = "Azimuth"
            cols(2) = "MTilt"
            Dim dtAntennas As DataTable = dvAntennas.ToTable(True, cols)

            Dim rnd As Random = New Random(10)
            Dim clr As Color = Nothing

            Dim isWarning As Integer = Nothing
            Dim isBlocked As Integer = Nothing

            For Each dr As DataRow In dtAntennas.Rows

                rootNode = tlTiltManager.Nodes.Add(New Object() {dr("AntennaType").ToString.Trim, dr("Azimuth").ToString.Trim, dr("MTilt").ToString.Trim})

                Dim colsRETDevices(5) As String
                colsRETDevices(0) = "DEVICENAME"
                colsRETDevices(1) = "ETILT"
                colsRETDevices(2) = "DEVICENO"
                colsRETDevices(3) = "IncludeInPlan"
                colsRETDevices(4) = "TiltRule"
                colsRETDevices(5) = "ETILT_Planned"

                Dim dtRETDevices As DataTable = dt.Select("AntennaType=" & Chr(39) & dr("ANTENNATYPE").ToString & Chr(39)).CopyToDataTable.DefaultView.ToTable(True, colsRETDevices)

                Dim masterTiltRuleRow() As DataRow = dtRETDevices.Select("TiltRule='MASTER'")
                If masterTiltRuleRow.Length > 0 Then
                    Dim drToRemove As DataRow = dtRETDevices.Select("DEVICENAME='" & masterTiltRuleRow(0)("DEVICENAME") & "' And TiltRule =''")(0)
                    dtRETDevices.Rows.Remove(drToRemove)
                    dtRETDevices.AcceptChanges()
                End If

                For Each drRET As DataRow In dtRETDevices.Rows

                    'Set devicename node and sub nodes the same bgcolor
                    clr = Color.FromArgb(150, rnd.Next(128, 255), rnd.Next(128, 255), rnd.Next(128, 255))

                    Dim includeInPlan As String = Nothing
                    If IsDBNull(drRET("IncludeInPlan")) Then
                        includeInPlan = ""
                    Else
                        If (drRET("IncludeInPlan") = True) Then
                            includeInPlan = "YES"
                        Else
                            includeInPlan = "NO"
                        End If
                    End If

                    Dim nodeRET As TreeListNode = tlTiltManager.AppendNode(New Object() {"", "", "", drRET("DEVICENAME").ToString.Trim, drRET("ETILT").ToString.Trim, drRET("DEVICENO").ToString.Trim, includeInPlan,
                                                                           nZ(drRET("ETilt_Planned"), ""), nZ(drRET("TiltRule"), "")}, rootNode, clr)

                    Dim dtCells As DataTable = dt.Select("AntennaType=" & Chr(39) & dr("ANTENNATYPE").ToString & Chr(39) & " And DeviceName=" & Chr(39) & drRET("DEVICENAME").ToString.Trim & Chr(39) &
                                                         " And ETilt=" & drRET("ETILT") & " And DeviceNo=" & Chr(39) & drRET("DEVICENO").ToString.Trim & Chr(39)).CopyToDataTable.DefaultView.ToTable()

                    For Each drCells As DataRow In dtCells.Rows

                        Dim validityMsg As String = Nothing
                        Dim validityImg As Image = Nothing
                        If dtValidationData.Select("CELLNAME='" & drCells("CELLNAME").ToString & "'").Count > 0 Then
                            isWarning = CInt(dtValidationData.Compute("MAX(isWarning)", "CELLNAME='" & drCells("CELLNAME").ToString & "'"))
                            isBlocked = CInt(dtValidationData.Compute("MAX(isBlocked)", "CELLNAME='" & drCells("CELLNAME").ToString & "'"))
                            If isWarning = 1 AndAlso isBlocked = 0 Then
                                validityMsg = "W"
                                validityImg = imgListValidation.Images(0)
                            ElseIf isWarning = 0 AndAlso isBlocked = 1 Then
                                validityMsg = "B"
                                validityImg = imgListValidation.Images(1)
                                nodeRET.SetValue(tlcValidation, validityImg)
                            ElseIf isWarning = 1 AndAlso isBlocked = 1 Then
                                validityMsg = "B"
                                validityImg = imgListValidation.Images(1)
                                nodeRET.SetValue(tlcValidation, validityImg)
                            End If
                        Else
                            validityMsg = ""
                        End If

                        Dim nodeObject As TreeListNode = tlTiltManager.AppendNode(New Object() {"", "", "", "", "", "", "", "", IIf(drCells("TiltRule").ToString.ToUpper = "MASTER", drCells("TiltRule").ToString, ""), validityImg, drCells("Technology").ToString.Trim, drCells("LocationID").ToString.Trim,
                                                                                  drCells("MBTSNAME").ToString.Trim, drCells("SECTORID").ToString.Trim, drCells("IOS_LAYER").ToString.Trim, drCells("VBeamAngle").ToString.Trim,
                                                                                  drCells("CELLNAME").ToString.Trim, drCells("CELLID").ToString.Trim, drCells("X"), drCells("Y"), drCells("RADIATIONCENTER"), drCells("DEVICELINKEDTO")}, nodeRET, clr)

                        Dim drTemp As DataRow = dt.Select("AntennaType=" & Chr(39) & dr("ANTENNATYPE").ToString & Chr(39) & " And DeviceName=" & Chr(39) & drRET("DEVICENAME").ToString.Trim & Chr(39) &
                                                          " And IOS_Layer='" & drCells("IOS_LAYER").ToString.Trim & "' And CELLNAME='" & drCells("CELLNAME").ToString.Trim & "' And NENAME='" & drCells("NENAME").ToString.Trim & "'")(0)

                        drTemp("HPDW") = CDbl(drRET("ETILT") - CDbl(drCells("VBeamAngle") / 2))
                        drTemp("CellColor") = clr
                        dt.AcceptChanges()
                    Next

                Next

            Next

            tlTiltManager.ResumeLayout()
            If tlTiltManager.Nodes.Count > 0 Then
                tlTiltManager.SelectNode(tlTiltManager.Nodes(0))
                tlTiltManager.SetFocusedNode(tlTiltManager.Nodes(0))
                tlTiltManager.AutoFillColumn = tlTiltManager.Columns(0)
            End If
            tlTiltManager.EndUnboundLoad()

            'Dim minLavel As Double = CDbl(dt.Compute("min([HPDW])", String.Empty))

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Public Sub TerrainProfileStart_Tiltmanager(ByRef dt As DataTable, ByVal totaldist As Double, ByVal dtTree As DataTable)
        Try
            Dim tilts(1) As Double
            ''tilts(0) = tilt_elec
            ''tilts(1) = tilt_mech

            ch_TiltManager.Tag = tilts
            ch_TiltManager.LegendBox.Visible = True

            'configure chart
            ch_TiltManager.DefaultElement.Marker.Visible = False
            ch_TiltManager.LegendBox.Orientation = dotnetCHARTING.WinForms.Orientation.Bottom
            ch_TiltManager.LegendBox.DefaultEntry.Value = ""
            ch_TiltManager.XAxis.Clear()
            ch_TiltManager.XAxis.Label.Text = "Distance [km]: " & Math.Round(totaldist, 1)

            ch_TiltManager.ToolTip.InitialDelay = 1
            ch_TiltManager.ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
            ch_TiltManager.DefaultSeries.EmptyElement.Mode = EmptyElementMode.None

            ch_TiltManager.TitleBox.Label.Text = "MBTS Name: " & dtTree.Rows(0)("MBTSNAME") & " - Sector ID:" & dtTree.Rows(0)("SECTORID") & " - Radiation Center:" & dtTree.Rows(0)("RADIATIONCENTER")
            ch_TiltManager.TitleBox.Visible = True
            ''ch_TiltManager.TitleBox.HeaderLabel.Text = "Antenna: " & AntennaInCalculation & "    VBeam: " & Math.Round(vAngle, 1) & "°"
            ch_TiltManager.TitleBox.Label.Alignment = StringAlignment.Near
            ch_TiltManager.TitleBox.CornerTopLeft = BoxCorner.Round
            ch_TiltManager.TitleBox.CornerTopRight = BoxCorner.Round


            Me.CustomClutter = 0
            Me.DoubleBuffered = True

            an_clutter = New Annotation("Clutter")
            an_clutter.DefaultCorner = BoxCorner.Square
            an_clutter.Background.Color = Color.White
            an_clutter.Line.Color = Color.White
            an_clutter.Shadow.Visible = False
            an_clutter.Position = New System.Drawing.Point(ch_TiltManager.Width - 110, 2)
            an_clutter.ToolTip = "Change Clutter Value"

            an_DownClutter = New Annotation("+3m")
            an_DownClutter.Background.Color = Color.LightGray
            an_DownClutter.DefaultCorner = BoxCorner.Round
            an_DownClutter.Position = New System.Drawing.Point(ch_TiltManager.Width - 72, 2)

            an_UpClutter = New Annotation("-3m")
            an_UpClutter.Background.Color = Color.LightGray
            an_UpClutter.DefaultCorner = BoxCorner.Round
            an_UpClutter.Position = New System.Drawing.Point(ch_TiltManager.Width - 35, 2)

            an_DownClutter.Size = New Size(32, 20)
            an_UpClutter.Size = New Size(32, 20)

            ch_TiltManager.Annotations.Clear()
            ch_TiltManager.Annotations.Add(an_clutter)
            ch_TiltManager.Annotations.Add(an_DownClutter)
            ch_TiltManager.Annotations.Add(an_UpClutter)

            ch_TiltManager.DefaultElement.Hotspot.ToolTip = "%SeriesName: %Value "

            AddHandler ch_TiltManager.MouseClick, AddressOf ch_TiltManager_Click
            AddHandler ch_TiltManager.SizeChanged, AddressOf ch_TiltManager_SizeChanged

            Dim yaxis1 As Axis = New Axis
            yaxis1.Orientation = Orientation.Left
            yaxis1.Label.Text = "Elevation [m]"

            'getting min max elevation for yaxis
            Dim min_elev As Double = 100000
            Dim max_elev As Double = -100000
            For Each dr As DataRow In dt.Rows
                min_elev = Math.Min(min_elev, dr("elevation"))
                For Each col As DataColumn In dt.Columns
                    If col.ColumnName.StartsWith("ubh_") AndAlso max_elev = -100000 Then
                        max_elev = Math.Max(max_elev, dt(0)(col) * 1.2)
                        Exit For
                    End If
                Next
            Next

            Dim maxOfTerrain As Double = dt.Compute("MAX(Elevation)", "")

            yaxis1.ScaleRange.ValueHigh = Math.Max(maxOfTerrain * 1.2, max_elev)
            yaxis1.ScaleRange.ValueLow = min_elev

            Dim dataFieldsArray(Me.dynCellNameColumnsCount + 1) As String
            dataFieldsArray(0) = "elevation"

            Dim index As Integer = 1
            For Each col As DataColumn In dt.Columns
                If col.ColumnName.StartsWith("ubh_") Then
                    dataFieldsArray.SetValue(dt.Columns(col.ColumnName).ToString, index)
                    index = index + 1
                End If
            Next

            dataFieldsArray(Me.dynCellNameColumnsCount + 1) = "clutter_height"

            Dim dtCellsWithColor As DataTable = dtTree.DistinctCol({"CELLNAME", "CellColor"})
            Dim de As DataEngine = New DataEngine(dt)

            de.DataFields = String2DataFields(dataFieldsArray, "location")
            de.DataGridFormatString = "N2"

            Dim sc As New SeriesCollection
            sc = de.GetSeries()

            sc(0).Type = SeriesType.AreaLine
            sc(0).YAxis = yaxis1

            For iCnt As Integer = 1 To dataFieldsArray.Length - 2

                Dim cellName As String = dataFieldsArray(iCnt).ToString.Substring("ubh_".Length)
                Dim cellColor As Color = CType(dtCellsWithColor.Select("CELLNAME='" & cellName & "'")(0)("CellColor"), Color)

                sc(iCnt).Type = SeriesType.Line
                sc(iCnt).YAxis = yaxis1
                sc(iCnt).DefaultElement.Color = Color.FromArgb(255, cellColor)
                sc(iCnt).Line.Width = 4
                sc(iCnt).Line.Transparency = 255

            Next

            sc(dataFieldsArray.Length - 1).Type = SeriesType.Line
            sc(dataFieldsArray.Length - 1).YAxis = yaxis1
            sc(dataFieldsArray.Length - 1).DefaultElement.Color = Color.DarkGray

            ch_TiltManager.SeriesCollection.Clear()
            ch_TiltManager.SeriesCollection.Add(sc)


            ch_TiltManager.XAxis.Markers.Clear()
            ch_TiltManager.RefreshChart()
            ch_TiltManager.ResumeLayout()

            sc = Nothing
            de = Nothing


        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If Not ch_TiltManager Is Nothing Then
                ch_TiltManager.Application = "DY4Zd/25XLMFNDYTc7eMQ7RCQfp58yVWNbgx/x0cDkruA0S6d3O/f2qh0jotTM3L"
            End If
        End Try
    End Sub

    Public Sub FillSectorListForCampaign()
        RemoveHandler gvSectorList.FocusedRowChanged, AddressOf gvSectorList_FocusedRowChanged
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@campaignID", selectedTiltCampaignID}
        }
        strConnection = GetSQL(4908, parray)(0)
        sqlParam = GetSQL(4908, parray)(1)

        Dim dt As DataTable = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dt.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcSectorList, gvSectorList, dt, "ALL", Nothing, "MBTSNAME")
            AddHandler gvSectorList.FocusedRowChanged, AddressOf gvSectorList_FocusedRowChanged
            ManageButtons(True)
        Else
            gcSectorList.DataSource = Nothing
            gvSectorList.Columns.Clear()
            ManageButtons(False)
        End If
        gvSectorList_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadCampaignValidation()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", selectedTiltCampaignID},
            New String() {"@MBTSNAME", Chr(39) & gvSectorList.GetFocusedRowCellValue("MBTSNAME").ToString & Chr(39)},
            New String() {"@SECTORID", CInt(gvSectorList.GetFocusedRowCellValue("SECTORID"))}
        }
        strConnection = GetSQL(4934, parray)(0)
        sqlParam = GetSQL(4934, parray)(1)

        dtValidationData = New DataTable
        dtValidationData = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dtValidationData.Rows.Count > 0 Then
            IOSDevExpressGrid.PopulateDataInGrid(gcCampaignValidation, gvCampaignValidation, dtValidationData, "ALL", Nothing, Nothing)
        Else
            gcCampaignValidation.DataSource = Nothing
            gvCampaignValidation.Columns.Clear()
        End If
    End Sub

    Private Sub DeleteManualTiltCampaign(campaignID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@CampaignType", Chr(39) & "MANUAL" & Chr(39)}
        }

        strConnection = GetSQL(4912, parray)(0)
        sqlParam = GetSQL(4912, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub Draw_Wedge_From_SliderUpdate()
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim resolution As Integer = 0
            If (Me.cmbResolution.SelectedItem.ToString.ToLower = "low") Then
                resolution = 7
            ElseIf (Me.cmbResolution.SelectedItem.ToString.ToLower = "medium") Then
                resolution = 15
            ElseIf (Me.cmbResolution.SelectedItem.ToString.ToLower = "high") Then
                resolution = 25
            End If

            Dim treeFocusedNode As TreeListNode = tlTiltManager.FocusedNode
            Dim focusedCellName As String = Nothing
            Dim focusedDeviceName As String = Nothing

            If treeFocusedNode.Level = 0 Then
                focusedCellName = treeFocusedNode.Nodes(0).Nodes(0)("CellName").ToString
                focusedDeviceName = treeFocusedNode.Nodes(0)("DeviceName")
            ElseIf treeFocusedNode.Level = 1 Then
                focusedCellName = treeFocusedNode.Nodes(0)("CellName").ToString
                focusedDeviceName = treeFocusedNode("DeviceName")
            ElseIf treeFocusedNode.Level = 2 Then
                focusedCellName = treeFocusedNode("CellName").ToString
                focusedDeviceName = treeFocusedNode.ParentNode("DeviceName")
            End If

            frmMapWindow.TiltManager_Draw_Cell_Wedge(dtTreeData, focusedCellName, focusedDeviceName, resolution)

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

#End Region

#Region "MML Campaigns Methods"

    Private Sub LoadTiltMMLCampaign()
        Try
            RemoveHandler gvMmlCampaign.FocusedRowChanged, AddressOf gvMmlCampaign_FocusedRowChanged

            Dim dtMmlCamp As New DataTable()
            dtMmlCamp = IOS.DataLibrary.clsSQLCommands.GetTiltMMLCampaigns(connStrIOSServer)
            'dtMmlCamp.Columns(2).Caption = "ID"

            Dim columnsToHide() As String = {"CampaignID", "ResultsCreated", "CampaignType"}
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcMmlCampaign, gvMmlCampaign, dtMmlCamp, "ALL", columnsToHide, "CampaignName")
            gvMmlCampaign.Columns("ResultSetID").VisibleIndex = 0
            AddHandler gvMmlCampaign.FocusedRowChanged, AddressOf gvMmlCampaign_FocusedRowChanged
        Catch
        End Try
    End Sub

    Private Sub LoadMmlConfiguration()
        Try
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = Nothing
            strConnection = GetSQL(4905, parray)(0)
            sqlParam = GetSQL(4905, parray)(1)

            dtMmlConfig = New DataTable()
            dtMmlConfig = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

            BindDevExComboBoxWithValueMember(cmbMMLConfig, dtMmlConfig, "MMLConfigID", "MMLConfigName", "Select...", True)
            cmbMMLConfig.SelectedIndex = 1
            Dim columnsToHide() As String = {"MMLConfigOwner", "MMLConfigDescription", "IsPublic"}
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcMmlConfig, gvMmlConfig, dtMmlConfig, "ALL", columnsToHide, "MMLConfigName")
        Catch
        End Try
    End Sub

    Private Sub LoadScripts(mmlConfigID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@mmlConfigID", mmlConfigID}
        }

        strConnection = GetSQL(4901, parray)(0)
        sqlParam = GetSQL(4901, parray)(1)

        Dim dtScripts = New DataTable()
        dtScripts = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcScripts, gvScripts, dtScripts, "ALL",, "SCRIPT_TEXT")
    End Sub

    Private Sub LoadValidationGrid(campaignID As Integer, campaignType As String, resultSetID As String, mmlConfigID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)},
            New String() {"@MMLConfigID", mmlConfigID}
        }
        'New String() {"@Debug", 0}
        strConnection = GetSQL(4902, parray)(0)
        sqlParam = GetSQL(4902, parray)(1)

        Dim dtValidation = New DataTable()
        dtValidation = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, 300)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcValidation, gvValidation, dtValidation, "ALL")
    End Sub

    Private Sub LoadDataGrid(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4903, parray)(0)
        sqlParam = GetSQL(4903, parray)(1)

        Dim dtData = New DataTable()
        dtData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, 300)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcData, gvData, dtData, "ALL")
    End Sub

    Private Sub LoadExcludedGrid(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4930, parray)(0)
        sqlParam = GetSQL(4930, parray)(1)

        Dim dtExc = New DataTable()
        dtExc = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, 300)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcExcluded, gvExcluded, dtExc, "ALL")
    End Sub

    Private Sub GetMmlUserFilter(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4535, parray)(0)
        sqlParam = GetSQL(4535, parray)(1)

        dtMmlUserFilter = New DataTable()
        dtMmlUserFilter = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadSelectionTree(resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4906, parray)(0)
        sqlParam = GetSQL(4906, parray)(1)

        Dim dtSelTree = New DataTable()
        dtSelTree = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        FillObjectTree(dtSelTree, tvSelectionMml)
        CheckMmlUserFilters()
    End Sub

    Private Sub FillObjectTree(dtData As DataTable, ByRef tree As TreeView)
        Try
            tree.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim rootn As TreeNode = New TreeNode()
            rootn.Text = "ROOT"
            rootn.ImageKey = "EMPTY"
            rootn.SelectedImageKey = "EMPTY"
            tree.Nodes.Clear()
            tree.Nodes.Add(rootn)
            Dim tNode As New TreeNode
            tNode = tree.Nodes(0)

            Dim dtParent As DataTable = dtData.DefaultView.ToTable(True, dtData.Columns(1).ColumnName)
            For Each drParent As DataRow In dtData.Select("ParentID = '" & rootn.Text & "'")
                Dim roottn As TreeNode = New TreeNode()
                roottn.Name = drParent(0).ToString
                roottn.Text = drParent(0).ToString & " (A: " & drParent(5).ToString & "/D: " & drParent(6).ToString & ")"
                roottn.Tag = drParent(4).ToString
                roottn.ImageKey = "EMPTY"
                roottn.SelectedImageKey = "EMPTY"
                tNode.Nodes.Add(roottn)
                PopulateObjectTree(drParent.Table.Columns(1).ColumnName, drParent(0).ToString, roottn, dtData)
            Next
            rootn.Expand()

        Catch ex As Exception
        Finally
            tree.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub PopulateObjectTree(ByVal parentColName As String, ByVal inParentID As String, ByVal inTreeNode As TreeNode, ByVal dt As DataTable)
        Try
            inTreeNode.Nodes.Clear()
            For Each drParent As DataRow In dt.Select(parentColName & "='" & inParentID & "'")
                Dim roottn As TreeNode = New TreeNode()
                roottn.Name = drParent(0).ToString
                roottn.Text = drParent(0).ToString & " (A: " & drParent(5).ToString & "/D: " & drParent(6).ToString & ")"
                roottn.Tag = drParent(4).ToString
                roottn.ImageKey = "EMPTY"
                roottn.SelectedImageKey = "EMPTY"
                inTreeNode.Nodes.Add(roottn)
                PopulateObjectTree(drParent.Table.Columns(1).ColumnName, drParent(0).ToString, roottn, dt)
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Sub CheckMmlUserFilters()
        If dtMmlUserFilter IsNot Nothing Then
            For Each dr As DataRow In dtMmlUserFilter.Rows
                Dim nd() As TreeNode = Nothing
                nd = tvSelectionMml.Nodes.Find(dr(2), True)
                For i As Integer = 0 To nd.Length - 1
                    If dr(1) = nd(i).Parent.Name AndAlso dr(2) = nd(i).Name Then
                        nd(i).Checked = True
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub InsertMmlUserFilterAudit(ByVal resultSetID As String)
        '-- Delete Userfilter for Resultsetid first
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@resultsetid", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(0, parray)(0)
        sqlParam = GetSQL(0, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)


        '-- Bulkinsert selection
        Dim lstCheckedNode As New List(Of TreeNode)
        lstCheckedNode = Treeview_GetCheck(tvSelectionMml.Nodes, True)

        If lstCheckedNode.Count > 0 Then
            Dim dtmmlUserFilterSave As New DataTable()
            dtmmlUserFilterSave.Columns.Add("ResultSetID")
            dtmmlUserFilterSave.Columns.Add("FilterFieldName")
            dtmmlUserFilterSave.Columns.Add("FilterValue")

            For Each checkedNode As TreeNode In lstCheckedNode
                ApplyMmlSelectionTreeChanges(checkedNode, dtmmlUserFilterSave)
            Next

            Dim connArr() As String = GetIOSConnection(1000)
            If connArr.Length > 0 Then
                ''InsertBulkDataToServer(connArr(1), "[" & connArr(2) & "].[dbo].[MML_UserFilter]", dtmmlUserFilterSave, "MML_UserFilter")
            End If
        End If
    End Sub

    Private Sub ApplyMmlSelectionTreeChanges(ByRef nd As TreeNode, ByRef dt As DataTable)
        Try
            Dim rIndex() As Integer = gvMmlCampaign.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim dr As DataRow = gvMmlCampaign.GetRow(rIndex(0)).Row
                Dim ResultSetID As String = dr("ResultSetID").ToString
                Dim drObjectFilter() As DataRow = Nothing

                drObjectFilter = dt.Select("ResultSetID='" & ResultSetID & "' AND FilterFieldName='" & nd.Tag & "' AND FilterValue='" & nd.Name & "'")
                If drObjectFilter.Length = 0 Then
                    Dim drow As DataRow
                    drow = dt.NewRow
                    drow("ResultSetID") = ResultSetID
                    drow("FilterFieldName") = nd.Tag
                    drow("FilterValue") = nd.Name

                    dt.Rows.Add(drow)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetTiltMmlData(campaignID As Integer, resultSetID As String, campaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)},
            New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
        }
        strConnection = GetSQL(4907, parray)(0)
        sqlParam = GetSQL(4907, parray)(1)

        dsGetMml = New DataSet()
        dsGetMml = IOS.DataLibrary.DataAccessorODBC.GetDataSet(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub GetTiltMmlDataRollback(campaignID As Integer, resultSetID As String, campaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)},
            New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
        }
        strConnection = GetSQL(4950, parray)(0)
        sqlParam = GetSQL(4950, parray)(1)

        dsGetMmlRollback = New DataSet()
        dsGetMmlRollback = IOS.DataLibrary.DataAccessorODBC.GetDataSet(strConnection, sqlParam, iQryTimeOut)
    End Sub

#End Region

#Region "Bulk Campaigns Methods"

    Private Sub DeleteOldImportedDataForCampaignID(campaignID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID}
        }
        strConnection = GetSQL(4947, parray)(0)
        sqlParam = GetSQL(4947, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub InsertBulkDataToServer(ConnString As String, DestinationTable As String, dtData As DataTable)
        Using cn As New System.Data.SqlClient.SqlConnection(ConnString)
            cn.Open()
            Using copy As New System.Data.SqlClient.SqlBulkCopy(cn)

                copy.DestinationTableName = DestinationTable
                copy.NotifyAfter = 1000
                AddHandler copy.SqlRowsCopied, AddressOf OnSqlRowsCopied

                copy.ColumnMappings.Add("CampaignID", "CampaignID")
                copy.ColumnMappings.Add("CELLNAME", "CELLNAME")
                copy.ColumnMappings.Add("ETILT", "ETILT")

                copy.WriteToServer(dtData)
            End Using
        End Using
    End Sub

    Private Sub LoadCampaignsBulk()
        Try
            GetCampaignsBulk()

            Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "LastRunTime", "LastEndTime", "CampaignType", "LastStatus", "IsPublic"}
            Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()

            RemoveHandler gvCampaignsBulk.FocusedRowChanged, AddressOf gvTiltCampaigns_FocusedRowChanged
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCampaignsBulk, gvCampaignsBulk, dtCampaignsBulk, "ALL", columnsToHide, "CampaignName")
            gvCampaignsBulk.Columns(0).Caption = "ID"
            gvCampaignsBulk.Columns(0).BestFit()
            AddHandler gvCampaignsBulk.FocusedRowChanged, AddressOf gvTiltCampaigns_FocusedRowChanged

            If gvCampaignsBulk.RowCount > 0 Then
                gvCampaignsBulk.ClearSelection()
                If rIndex.Length > 0 Then
                    gvCampaignsBulk.SelectRow(rIndex(0))
                    gvCampaignsBulk.FocusedRowHandle = rIndex(0)
                Else
                    gvCampaignsBulk.SelectRow(0)
                    gvCampaignsBulk.FocusedRowHandle = 0
                End If
                gvTiltCampaigns_FocusedRowChanged(Nothing, Nothing)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetCampaignsBulk()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(4913, parray)(0)
        sqlParam = GetSQL(4913, parray)(1)

        dtCampaignsBulk = New DataTable()
        dtCampaignsBulk = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Function GetCampaignDetailsByID(CampaignID As Integer) As DataRow
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@CampaignID", CampaignID}}
        strConnection = GetSQL(4914, parray)(0)
        sqlParam = GetSQL(4914, parray)(1)

        Dim dt As New DataTable()
        dt = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Private Sub LoadResultSetComboBulk(ByRef cmb As ComboBoxEdit, CampaignID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@CampaignID", CampaignID}}
        strConnection = GetSQL(4915, parray)(0)
        sqlParam = GetSQL(4915, parray)(1)

        Dim dtResultSetID As New DataTable()
        dtResultSetID = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dtResultSetID.Rows.Count > 0 Then
            BindDevExComboBoxWithValueMember(cmb, dtResultSetID, "ResultSetID", "ResultSetID", "Select")
            SetComboBox(cmbResultSetIDBulk, ComboSelectBased.TextBased, dtResultSetID.Rows(0)(0))
        Else
            ClearComboBox(cmbResultSetIDBulk, "Select")
            cmbResultSetIDBulk.SelectedIndex = 0
        End If
    End Sub

    Private Sub UpdateCampaignBulk(CampaignType As String)
        Dim campaignID As Integer = 0
        Dim parray()() As String = Nothing
        If CampaignType = "TM_Bulk" Then
            Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvCampaignsBulk.GetRow(rIndex(0)).Row
                campaignID = drow("CampaignID")
                parray = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@Enabled", IIf(ceActiveBulk.Checked, 1, 0)},
                    New String() {"@SchNextStartTime", "NULL"},
                    New String() {"@SchRptInterval", "NULL"},
                    New String() {"@IsPublic", IIf(ceIsPublicBulk.Checked, 1, 0)}
                }
            End If
        End If

        Dim strConnection As String = GetSQL(4916, parray)(0)
        Dim sqlParam As String = GetSQL(4916, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub DeleteTiltCampaign(campaignID As Integer, campaignType As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
        }

        strConnection = GetSQL(4917, parray)(0)
        sqlParam = GetSQL(4917, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub SetMessage(ByVal message As String)
        lblStatus.ForeColor = Color.Red
        lblStatus.Visible = True
        lblStatus.Text = message
        Timer1.Enabled = True
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
    End Sub

    Private Sub HideDescriptionArea(ByRef pGrid As PropertyGrid)
        Dim pi As System.Reflection.PropertyInfo
        pi = pGrid.GetType().GetProperty("Controls")
        Dim cc As Control.ControlCollection = pi.GetValue(pGrid, Nothing)
        For Each ctrl As Control In cc
            Dim ct As Type = ctrl.GetType()
            Dim sName As String = ct.Name
            If sName = "DocComment" Then
                pi = ct.GetProperty("Height")
                pi.SetValue(ctrl, 0, Nothing)
                Dim fi As System.Reflection.FieldInfo
                fi = ct.BaseType.GetField("userSized", Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
                fi.SetValue(ctrl, True)
            End If
        Next
        pGrid.Refresh()
    End Sub

    Private Sub LoadConfigSummaryGridBulk(CampaignID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        parray = {
            New String() {"@CampaignID", CampaignID}
        }
        strConnection = GetSQL(4918, parray)(0)
        sqlParam = GetSQL(4918, parray)(1)

        Dim dtConfig As New DataTable()
        dtConfig = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        RemoveHandler gcConfigSummBulk.ProcessGridKey, AddressOf ConfigSummGrid_ProcessGridKey
        RemoveHandler gvConfigSummBulk.FocusedRowChanged, AddressOf gvConfigSummBulk_FocusedRowChanged
        IOSDevExpressGrid.PopulateDataInGrid(gcConfigSummBulk, gvConfigSummBulk, dtConfig, "ALL", {"TILTMIN", "TILTMAX"})
        AddHandler gvConfigSummBulk.FocusedRowChanged, AddressOf gvConfigSummBulk_FocusedRowChanged
        AddHandler gcConfigSummBulk.ProcessGridKey, AddressOf ConfigSummGrid_ProcessGridKey

        If gvConfigSummBulk.RowCount > 0 Then
            gvConfigSummBulk.ClearSelection()
            gvConfigSummBulk.FocusedRowHandle = 0
            gvConfigSummBulk.SelectRow(0)
        End If
        gvConfigSummBulk_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadLayerPropertiesBulk(ByRef propertyGridCtrl As PropertyGrid, ByVal CampaignType As String, Optional dr As DataRow = Nothing)
        Dim layerProperties As New CustomClass()
        propertyGridCtrl.SelectedObject = layerProperties
        layerProperties.Clear()
        Dim dtProperties As New DataTable

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignType", Chr(39) & CampaignType & Chr(39)}
        }
        strConnection = GetSQL(4919, parray)(0)
        sqlParam = GetSQL(4919, parray)(1)
        dtProperties = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        If dtProperties IsNot Nothing Then
            Dim drProp As DataRow
            drProp = dtProperties.NewRow
            drProp("ConfigFieldName") = "Reference Band"
            drProp("ConfigFieldDescription") = "Reference Band"
            drProp("ConfigFieldType") = "ComboBoxLayer"
            drProp("ConfigFieldEditable") = 1
            dtProperties.Rows.Add(drProp)

            drProp = dtProperties.NewRow
            drProp("ConfigFieldName") = "Target Band"
            drProp("ConfigFieldDescription") = "Target Band"
            drProp("ConfigFieldType") = "ComboBoxLayer"
            drProp("ConfigFieldEditable") = 1
            dtProperties.Rows.Add(drProp)

            dtProperties.AcceptChanges()
        End If

        For Each row As DataRow In dtProperties.Rows
            Dim value As Object = Nothing
            Dim fieldValue As Object = Nothing
            Dim fieldName As String = row.Item("ConfigFieldName")
            If dr Is Nothing Then
                value = Convert.ToString(row.Item("ConfigFieldDefaultValue"))
            Else
                If dr.Table.Columns.Contains(fieldName) Then
                    fieldValue = dr(fieldName)
                Else
                    If CampaignType = "BulkImport" And fieldName = "CampaignID" Then
                        Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()
                        If rIndex.Length > 0 Then
                            Dim drow As DataRow = gvCampaignsBulk.GetRow(rIndex(0)).Row
                            fieldValue = drow.Item("CampaignID")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Reference Band" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtBandListTiltMngr.Select("BAND='" & dr.Item("ReferenceBand") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("BAND")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Target Band" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtBandListTiltMngr.Select("BAND='" & dr.Item("TargetBand") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("BAND")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Tilt Rule" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtTiltRule.Select("TiltRule='" & dr.Item("TILTRULE") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("TiltRule")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Master Layer" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtLayer.Select("Layer='" & dr.Item("MASTERLAYER") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("Layer")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Target Layer" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtLayer.Select("Layer='" & dr.Item("TARGETLAYER") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("Layer")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Inclusion List" Then
                        Dim drList() As DataRow = Nothing
                        If IsDBNull(dr.Item("InclusionListId")) Then
                            drList = Nothing
                        Else
                            drList = dtCellList.Select("ListID='" & dr.Item("InclusionListId") & "'")
                        End If
                        If drList IsNot Nothing AndAlso drList.Length > 0 Then
                            fieldValue = drList(0).Item("ListName")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Exclusion List" Then
                        Dim drList() As DataRow = Nothing
                        If IsDBNull(dr.Item("ExclusionListId")) Then
                            drList = Nothing
                        Else
                            drList = dtCellList.Select("ListID='" & dr.Item("ExclusionListId") & "'")
                        End If
                        If drList IsNot Nothing AndAlso drList.Length > 0 Then
                            fieldValue = drList(0).Item("ListName")
                        Else
                            fieldValue = ""
                        End If
                    Else
                        fieldValue = ""
                    End If
                End If
                If row.Item("ConfigFieldType") = "ComboBoxBoolean" Then
                    If fieldValue <> "0" And fieldValue <> "1" Then fieldValue = 0
                    value = Convert.ToBoolean(fieldValue)
                Else
                    value = fieldValue
                End If
            End If

            Dim myProp As New CustomProperty(CampaignType & " Properties", row.Item("ConfigFieldName"), row.Item("ConfigFieldType"), IIf(IsDBNull(row.Item("ConfigFieldDescription")), "", row.Item("ConfigFieldDescription")), Not row.Item("ConfigFieldEditable"), value)
            layerProperties.Add(myProp)
        Next
        propertyGridCtrl.Refresh()
    End Sub

    Private Sub LoadBandList()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(4920, parray)(0)
        sqlParam = GetSQL(4920, parray)(1)

        dtBandListTiltMngr = New DataTable()
        dtBandListTiltMngr = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub LoadTiltRule()
        dtTiltRule = New DataTable()
        dtTiltRule.Columns.Add("TiltRule", GetType(String))

        Dim drTiltRule As DataRow = dtTiltRule.NewRow()
        drTiltRule("TiltRule") = "MATCH VBEAM"
        dtTiltRule.Rows.Add(drTiltRule)

        drTiltRule = dtTiltRule.NewRow()
        drTiltRule("TiltRule") = "MATCH TILT"
        dtTiltRule.Rows.Add(drTiltRule)

        drTiltRule = dtTiltRule.NewRow()
        drTiltRule("TiltRule") = "MANUAL"
        dtTiltRule.Rows.Add(drTiltRule)

        drTiltRule = dtTiltRule.NewRow()
        drTiltRule("TiltRule") = "NOTHING"
        dtTiltRule.Rows.Add(drTiltRule)

        drTiltRule = dtTiltRule.NewRow()
        drTiltRule("TiltRule") = "MASTER"
        dtTiltRule.Rows.Add(drTiltRule)

        dtTiltRule.AcceptChanges()
    End Sub

    Public Sub AddTiltBulkImportConfig(ByVal campaignID As Integer)
        Dim masterLayer As String = Nothing
        Dim targetLayer As String = Nothing
        Dim tiltRule As String = Nothing
        Dim tiltMin As Double = Nothing
        Dim tiltMax As Double = Nothing
        Dim inclusionListID As Integer = 0
        Dim exclusionListID As Integer = 0
        Dim referenceBand As String = Nothing
        Dim targetband As String = Nothing

        Dim categories As GridItemCollection
        If layerPropGridBulk.SelectedGridItem.GridItemType = GridItemType.Category Then
            categories = layerPropGridBulk.SelectedGridItem.Parent.GridItems
        Else
            categories = layerPropGridBulk.SelectedGridItem.Parent.Parent.GridItems
        End If

        For Each category In categories
            If (CType(category, GridItem)).GridItemType = GridItemType.Category Then
                For Each gi As GridItem In (CType(category, GridItem)).GridItems

                    If gi.Label.ToLower = "master layer" Then
                        masterLayer = gi.Value.ToString
                    ElseIf gi.Label.ToLower = "target layer" Then
                        targetLayer = gi.Value.ToString
                    ElseIf gi.Label.ToLower = "tilt rule" Then
                        tiltRule = gi.Value.ToString
                    ElseIf gi.Label.ToLower = "tilt lower limit" Then
                        tiltMin = CDbl(gi.Value.ToString)
                    ElseIf gi.Label.ToLower = "tilt upper limit" Then
                        tiltMax = CDbl(gi.Value.ToString)
                    ElseIf gi.Label.ToLower = "inclusion list" Then
                        If gi.Value <> "" Then
                            inclusionListID = dtCellList.Select("ListName='" & gi.Value.ToString & "'")(0)("ListID")
                        End If
                    ElseIf gi.Label.ToLower = "exclusion list" Then
                        If gi.Value <> "" Then
                            exclusionListID = dtCellList.Select("ListName='" & gi.Value.ToString & "'")(0)("ListID")
                        End If
                    ElseIf gi.Label.ToLower = "reference band" Then
                        referenceBand = gi.Value.ToString
                    ElseIf gi.Label.ToLower = "target band" Then
                        targetband = gi.Value.ToString
                    End If

                Next
            End If
        Next

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@MASTERLAYER", Chr(39) & masterLayer & Chr(39)},
            New String() {"@TARGETLAYER", Chr(39) & targetLayer & Chr(39)},
            New String() {"@TILTRULE", Chr(39) & tiltRule & Chr(39)},
            New String() {"@TILTMIN", tiltMin},
            New String() {"@TILTMAX", tiltMax},
            New String() {"@InclusionListId", IIf(inclusionListID <> 0, inclusionListID, "NULL")},
            New String() {"@ExclusionListId", IIf(exclusionListID <> 0, exclusionListID, "NULL")},
            New String() {"@ReferenceBand", Chr(39) & referenceBand & Chr(39)},
            New String() {"@TargetBand", Chr(39) & targetband & Chr(39)}
        }

        strConnection = GetSQL(4921, parray)(0)
        sqlParam = GetSQL(4921, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub ApplyConfigPropertyToCampaign(ByVal campaignID As Integer, ByVal propName As String, ByVal propValue As String, ByVal campaignType As String, Optional configID As Integer = 0, Optional updateAll As Boolean = False)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing

        If updateAll = True Then
            Dim parray()() As String = {
                New String() {"@CampaignID", campaignID},
                New String() {"@PropertyName", Chr(39) & propName & Chr(39)},
                New String() {"@PropertyValue", Chr(39) & propValue & Chr(39)},
                New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
            }
            strConnection = GetSQL(4922, parray)(0)
            sqlParam = GetSQL(4922, parray)(1)
        Else
            Dim parray()() As String = {
                New String() {"@ConfigId", configID},
                New String() {"@CampaignID", campaignID},
                New String() {"@PropertyName", Chr(39) & propName & Chr(39)},
                New String() {"@PropertyValue", Chr(39) & propValue & Chr(39)},
                New String() {"@CampaignType", Chr(39) & campaignType & Chr(39)}
            }
            strConnection = GetSQL(4923, parray)(0)
            sqlParam = GetSQL(4923, parray)(1)
        End If
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadResultSummaryDataGridBulk(campaignID As Integer, resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        parray = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4928, parray)(0)
        sqlParam = GetSQL(4928, parray)(1)

        Dim dtResultSumm As New DataTable()
        dtResultSumm = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"CampaignID", "ResultSetID"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcSummDataBulk, gvSummDataBulk, dtResultSumm, "ALL", columnsToHide)
    End Sub

    Private Sub LoadValidationData(campaignID As Integer, resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        parray = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4932, parray)(0)
        sqlParam = GetSQL(4932, parray)(1)

        Dim dtValidationData As New DataTable()
        dtValidationData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"CampaignID", "ResultSetID"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcValidationData, gvValidationData, dtValidationData, "ALL", columnsToHide)
    End Sub

    Private Sub LoadImportedData()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", CInt(gvCampaignsBulk.GetFocusedRowCellValue("CampaignID"))}
        }
        strConnection = GetSQL(4924, parray)(0)
        sqlParam = GetSQL(4924, parray)(1)

        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcImportedDataBulk, gvImportedDataBulk, dt, "ALL", {"CampaignID"}, Nothing)
    End Sub

    Private Function GetBulkOutputData(resultSetID As String) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        parray = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4927, parray)(0)
        sqlParam = GetSQL(4927, parray)(1)
        Return IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub LoadBulkOutputData(resultSetID As String)
        Dim dtResultSumm As DataTable = GetBulkOutputData(resultSetID)
        Dim columnsToHide() As String = {"CampaignID", "ResultSetID"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcOutputDataBulk, gvOutputDataBulk, dtResultSumm, "ALL", columnsToHide)
        lblDataRowCountBulk.Text = "Count of Records: " & gvOutputDataBulk.RowCount
        lblDataRowCountBulk.Visible = True
    End Sub

    Private Sub DeleteCampaignResultSetBulk()
        If cmbResultSetIDBulk.SelectedIndex > 0 Then
            Dim parray()() As String = {
                New String() {"@ResultSetID", Chr(39) & cmbResultSetIDBulk.SelectedItem.ToString() & Chr(39)}
            }
            Dim strConnection As String = GetSQL(4929, parray)(0)
            Dim sqlParam As String = GetSQL(4929, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            cmbResultSetIDBulk.Properties.Items.Remove(cmbResultSetIDBulk.SelectedItem)
            cmbResultSetIDBulk.SelectedIndex = 0
        Else
            XtraMessageBox.Show("Please select result set id.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbResultSetIDBulk.Focus()
        End If
    End Sub

    Private Sub DeleteConfigPropertyBulk(campaignID As Integer, configID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        parray = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ConfigID", configID}
        }
        strConnection = GetSQL(4948, parray)(0)
        sqlParam = GetSQL(4948, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

#End Region

#Region "Audit Campaigns Methods"

    Private Sub LoadCampaignsAudit()
        Try
            GetCampaignsAudit()

            Dim columnsToHide() As String = {"CampaignDescription", "CampaignOwner", "LastRunTime", "LastEndTime", "CampaignType", "LastStatus", "IsPublic"}
            Dim rIndex() As Integer = gvCampaignsBulk.GetSelectedRows()

            RemoveHandler gvCampaignsAudit.FocusedRowChanged, AddressOf gvCampaignsAudit_FocusedRowChanged
            IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcCampaignsAudit, gvCampaignsAudit, dtCampaignsAudit, "ALL", columnsToHide, "CampaignName")
            gvCampaignsAudit.Columns(0).Caption = "ID"
            gvCampaignsAudit.Columns(0).BestFit()
            AddHandler gvCampaignsAudit.FocusedRowChanged, AddressOf gvCampaignsAudit_FocusedRowChanged

            If gvCampaignsAudit.RowCount > 0 Then
                gvCampaignsAudit.ClearSelection()
                If rIndex.Length > 0 Then
                    gvCampaignsAudit.SelectRow(rIndex(0))
                    gvCampaignsAudit.FocusedRowHandle = rIndex(0)
                Else
                    gvCampaignsAudit.SelectRow(0)
                    gvCampaignsAudit.FocusedRowHandle = 0
                End If
                gvCampaignsAudit_FocusedRowChanged(Nothing, Nothing)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetCampaignsAudit()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        strConnection = GetSQL(4936, parray)(0)
        sqlParam = GetSQL(4936, parray)(1)

        dtCampaignsAudit = New DataTable()
        dtCampaignsAudit = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Sub

    Private Sub UpdateCampaignAudit(CampaignType As String)
        Dim campaignID As Integer = 0
        Dim parray()() As String = Nothing
        If CampaignType = "TM_Audit" Then
            Dim rIndex() As Integer = gvCampaignsAudit.GetSelectedRows()
            If rIndex.Length > 0 Then
                Dim drow As DataRow = gvCampaignsAudit.GetRow(rIndex(0)).Row
                campaignID = drow("CampaignID")
                parray = {
                    New String() {"@CampaignID", campaignID},
                    New String() {"@Enabled", IIf(ceActiveAudit.Checked, 1, 0)},
                    New String() {"@SchNextStartTime", "NULL"},
                    New String() {"@SchRptInterval", "NULL"},
                    New String() {"@IsPublic", IIf(ceIsPublicAudit.Checked, 1, 0)}
                }
            End If
        End If

        Dim strConnection As String = GetSQL(4916, parray)(0)
        Dim sqlParam As String = GetSQL(4916, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Private Sub LoadConfigSummaryGridAudit(CampaignID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        parray = {
            New String() {"@CampaignID", CampaignID}
        }
        strConnection = GetSQL(4937, parray)(0)
        sqlParam = GetSQL(4937, parray)(1)

        Dim dtConfig As New DataTable()
        dtConfig = DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        RemoveHandler gcConfigSummAudit.ProcessGridKey, AddressOf ConfigSummGrid_ProcessGridKey
        RemoveHandler gvConfigSummAudit.FocusedRowChanged, AddressOf gvConfigSummAudit_FocusedRowChanged
        IOSDevExpressGrid.PopulateDataInGrid(gcConfigSummAudit, gvConfigSummAudit, dtConfig, "ALL", Nothing)
        AddHandler gvConfigSummAudit.FocusedRowChanged, AddressOf gvConfigSummAudit_FocusedRowChanged
        AddHandler gcConfigSummAudit.ProcessGridKey, AddressOf ConfigSummGrid_ProcessGridKey

        If gvConfigSummAudit.RowCount > 0 Then
            gvConfigSummAudit.ClearSelection()
            gvConfigSummAudit.FocusedRowHandle = 0
            gvConfigSummAudit.SelectRow(0)
        End If
        gvConfigSummAudit_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadResultSetComboAudit(ByRef cmb As ComboBoxEdit, CampaignID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {New String() {"@CampaignID", CampaignID}}
        strConnection = GetSQL(4915, parray)(0)
        sqlParam = GetSQL(4915, parray)(1)

        Dim dtResultSetID As New DataTable()
        dtResultSetID = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        If dtResultSetID.Rows.Count > 0 Then
            BindDevExComboBoxWithValueMember(cmb, dtResultSetID, "ResultSetID", "ResultSetID", "Select")
            SetComboBox(cmb, ComboSelectBased.TextBased, dtResultSetID.Rows(0)(0))
        Else
            ClearComboBox(cmb, "Select")
            cmb.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadLayerPropertiesAudit(propertyGridCtrl As PropertyGrid, ByVal CampaignType As String, Optional dr As DataRow = Nothing)
        Dim layerProperties As New CustomClass()
        propertyGridCtrl.SelectedObject = layerProperties
        layerProperties.Clear()
        Dim dtProperties As New DataTable

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignType", Chr(39) & CampaignType & Chr(39)}
        }
        strConnection = GetSQL(4919, parray)(0)
        sqlParam = GetSQL(4919, parray)(1)
        dtProperties = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)

        'If dtProperties IsNot Nothing Then
        '    Dim drProp As DataRow
        '    'drProp = dtProperties.NewRow
        '    'drProp("ConfigFieldName") = "Reference Band"
        '    'drProp("ConfigFieldDescription") = "Reference Band"
        '    'drProp("ConfigFieldType") = "ComboBoxLayer"
        '    'drProp("ConfigFieldEditable") = 1
        '    'dtProperties.Rows.Add(drProp)

        '    drProp = dtProperties.NewRow
        '    drProp("ConfigFieldName") = "Target Band"
        '    drProp("ConfigFieldDescription") = "Target Band"
        '    drProp("ConfigFieldType") = "ComboBoxLayer"
        '    drProp("ConfigFieldEditable") = 1
        '    dtProperties.Rows.Add(drProp)

        '    dtProperties.AcceptChanges()
        'End If

        For Each row As DataRow In dtProperties.Rows
            Dim value As Object = Nothing
            Dim fieldValue As Object = Nothing
            Dim fieldName As String = row.Item("ConfigFieldName")
            If dr Is Nothing Then
                value = Convert.ToString(row.Item("ConfigFieldDefaultValue"))
            Else
                If dr.Table.Columns.Contains(fieldName) Then
                    fieldValue = dr(fieldName)
                Else
                    If CampaignType = "Audit" And fieldName = "CampaignID" Then
                        Dim rIndex() As Integer = gvCampaignsAudit.GetSelectedRows()
                        If rIndex.Length > 0 Then
                            Dim drow As DataRow = gvCampaignsAudit.GetRow(rIndex(0)).Row
                            fieldValue = drow.Item("CampaignID")
                        Else
                            fieldValue = ""
                        End If
                        'ElseIf fieldName = "Reference Band" Then
                        '    Dim drList() As DataRow = Nothing
                        '    drList = dtBandListTiltMngr.Select("BAND='" & dr.Item("ReferenceBand") & "'")
                        '    If drList.Length > 0 Then
                        '        fieldValue = drList(0).Item("BAND")
                        '    Else
                        '        fieldValue = ""
                        '    End If
                    ElseIf fieldName = "Target Band" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtBandListTiltMngr.Select("BAND='" & dr.Item("TargetBand") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("BAND")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Tilt Rule" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtTiltRule.Select("TiltRule='" & dr.Item("TILTRULE") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("TiltRule")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Master Layer" Then
                        Dim drList() As DataRow = Nothing
                        drList = dtLayer.Select("Layer='" & dr.Item("MASTERLAYER") & "'")
                        If drList.Length > 0 Then
                            fieldValue = drList(0).Item("Layer")
                        Else
                            fieldValue = ""
                        End If
                        'ElseIf fieldName = "Target Layer" Then
                        '    Dim drList() As DataRow = Nothing
                        '    drList = dtLayer.Select("Layer='" & dr.Item("TARGETLAYER") & "'")
                        '    If drList.Length > 0 Then
                        '        fieldValue = drList(0).Item("Layer")
                        '    Else
                        '        fieldValue = ""
                        '    End If
                    ElseIf fieldName = "Inclusion List" Then
                        Dim drList() As DataRow = Nothing
                        If IsDBNull(dr.Item("InclusionListId")) Then
                            drList = Nothing
                        Else
                            drList = dtCellList.Select("ListID='" & dr.Item("InclusionListId") & "'")
                        End If
                        If drList IsNot Nothing AndAlso drList.Length > 0 Then
                            fieldValue = drList(0).Item("ListName")
                        Else
                            fieldValue = ""
                        End If
                    ElseIf fieldName = "Exclusion List" Then
                        Dim drList() As DataRow = Nothing
                        If IsDBNull(dr.Item("ExclusionListId")) Then
                            drList = Nothing
                        Else
                            drList = dtCellList.Select("ListID='" & dr.Item("ExclusionListId") & "'")
                        End If
                        If drList IsNot Nothing AndAlso drList.Length > 0 Then
                            fieldValue = drList(0).Item("ListName")
                        Else
                            fieldValue = ""
                        End If
                    Else
                        fieldValue = ""
                    End If
                End If
                If row.Item("ConfigFieldType") = "ComboBoxBoolean" Then
                    If fieldValue <> "0" And fieldValue <> "1" Then fieldValue = 0
                    value = Convert.ToBoolean(fieldValue)
                Else
                    value = fieldValue
                End If
            End If

            Dim myProp As New CustomProperty(CampaignType & " Properties", row.Item("ConfigFieldName"), row.Item("ConfigFieldType"), IIf(IsDBNull(row.Item("ConfigFieldDescription")), "", row.Item("ConfigFieldDescription")), Not row.Item("ConfigFieldEditable"), value)
            layerProperties.Add(myProp)
        Next
        propertyGridCtrl.Refresh()
    End Sub

    Private Function GetOutputDataAudit(resultSetID As String) As DataTable
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        parray = {
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4938, parray)(0)
        sqlParam = GetSQL(4938, parray)(1)
        Return IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
    End Function

    Private Sub LoadOutputDataAudit(resultSetID As String)
        Dim dtResultSumm As DataTable = GetOutputDataAudit(resultSetID)
        Dim columnsToHide() As String = {"CampaignID", "ResultSetID"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcOutputDataAudit, gvOutputDataAudit, dtResultSumm, "ALL", columnsToHide)
        lblDataRowCountAudit.Text = "Count of Records: " & gvOutputDataAudit.RowCount
        lblDataRowCountAudit.Visible = True
    End Sub

    Private Sub LoadValidationDataAudit(campaignID As Integer, resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        parray = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4940, parray)(0)
        sqlParam = GetSQL(4940, parray)(1)

        Dim dtValidationData As New DataTable()
        dtValidationData = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"CampaignID", "ResultSetID"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcValidationDataAudit, gvValidationDataAudit, dtValidationData, "ALL", columnsToHide)
    End Sub

    Private Sub LoadResultSummaryDataGridAudit(campaignID As Integer, resultSetID As String)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing

        parray = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ResultSetID", Chr(39) & resultSetID & Chr(39)}
        }
        strConnection = GetSQL(4939, parray)(0)
        sqlParam = GetSQL(4939, parray)(1)

        Dim dtResultSumm As New DataTable()
        dtResultSumm = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        Dim columnsToHide() As String = {"CampaignID", "ResultSetID"}
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcSummDataAudit, gvSummDataAudit, dtResultSumm, "ALL", columnsToHide)
    End Sub

    Private Sub DeleteCampaignResultSetAudit()
        If cmbResultSetIDAudit.SelectedIndex > 0 Then
            Dim parray()() As String = {
                New String() {"@ResultSetID", Chr(39) & cmbResultSetIDAudit.SelectedItem.ToString() & Chr(39)}
            }
            Dim strConnection As String = GetSQL(4941, parray)(0)
            Dim sqlParam As String = GetSQL(4941, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
            cmbResultSetIDAudit.Properties.Items.Remove(cmbResultSetIDAudit.SelectedItem)
            cmbResultSetIDAudit.SelectedIndex = 0
        Else
            XtraMessageBox.Show("Please select result set id.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbResultSetIDAudit.Focus()
        End If
    End Sub

    Private Sub LoadInputData()
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", CInt(gvCampaignsAudit.GetFocusedRowCellValue("CampaignID"))}
        }
        strConnection = GetSQL(4944, parray)(0)
        sqlParam = GetSQL(4944, parray)(1)

        Dim dt As DataTable = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
        IOS.Library.IOSDevExpressGrid.PopulateDataInGrid(gcInputDataAudit, gvInputDataAudit, dt, "ALL", {"CampaignID"}, Nothing)
    End Sub

    Private Sub DeleteConfigPropertyAudit(campaignID As Integer, configID As Integer)
        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = Nothing
        parray = {
            New String() {"@CampaignID", campaignID},
            New String() {"@ConfigID", configID}
        }
        strConnection = GetSQL(4949, parray)(0)
        sqlParam = GetSQL(4949, parray)(1)
        IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

    Public Sub AddTiltAuditImportConfig(ByVal campaignID As Integer)
        Dim masterLayer As String = Nothing
        Dim targetLayer As String = Nothing
        Dim tiltRule As String = Nothing
        'Dim tiltMin As Double = Nothing
        'Dim tiltMax As Double = Nothing
        Dim inclusionListID As Integer = 0
        Dim exclusionListID As Integer = 0
        'Dim referenceBand As String = Nothing
        'Dim targetband As String = Nothing

        Dim categories As GridItemCollection
        If layerPropGridAudit.SelectedGridItem.GridItemType = GridItemType.Category Then
            categories = layerPropGridAudit.SelectedGridItem.Parent.GridItems
        Else
            categories = layerPropGridAudit.SelectedGridItem.Parent.Parent.GridItems
        End If

        For Each category In categories
            If (CType(category, GridItem)).GridItemType = GridItemType.Category Then
                For Each gi As GridItem In (CType(category, GridItem)).GridItems

                    If gi.Label.ToLower = "master layer" Then
                        masterLayer = gi.Value.ToString
                    ElseIf gi.Label.ToLower = "target band" Then
                        targetLayer = gi.Value.ToString
                    ElseIf gi.Label.ToLower = "tilt rule" Then
                        tiltRule = gi.Value.ToString
                        'ElseIf gi.Label.ToLower = "tilt lower limit" Then
                        '    tiltMin = CDbl(gi.Value.ToString)
                        'ElseIf gi.Label.ToLower = "tilt upper limit" Then
                        '    tiltMax = CDbl(gi.Value.ToString)
                    ElseIf gi.Label.ToLower = "inclusion list" Then
                        If gi.Value <> "" Then
                            inclusionListID = dtCellList.Select("ListName='" & gi.Value.ToString & "'")(0)("ListID")
                        End If
                    ElseIf gi.Label.ToLower = "exclusion list" Then
                        If gi.Value <> "" Then
                            exclusionListID = dtCellList.Select("ListName='" & gi.Value.ToString & "'")(0)("ListID")
                        End If
                        'ElseIf gi.Label.ToLower = "reference band" Then
                        '    referenceBand = gi.Value.ToString
                        'ElseIf gi.Label.ToLower = "target band" Then
                        '    targetband = gi.Value.ToString
                    End If

                Next
            End If
        Next

        Dim strConnection As String = Nothing
        Dim sqlParam As String = Nothing
        Dim parray()() As String = {
            New String() {"@CampaignID", campaignID},
            New String() {"@MASTERLAYER", Chr(39) & masterLayer & Chr(39)},
            New String() {"@TARGETBAND", Chr(39) & targetLayer & Chr(39)},
            New String() {"@TILTRULE", Chr(39) & tiltRule & Chr(39)},
            New String() {"@InclusionListId", IIf(inclusionListID <> 0, inclusionListID, "NULL")},
            New String() {"@ExclusionListId", IIf(exclusionListID <> 0, exclusionListID, "NULL")}
        }

        strConnection = GetSQL(4951, parray)(0)
        sqlParam = GetSQL(4951, parray)(1)
        DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam,, iQryTimeOut)
    End Sub

#End Region

#End Region

End Class

Class RunNowTiltMngr

    Public campaignID As Integer
    Public Status As Integer
    Public CampaignRow As DataRow
    Public Event ThreadComplete(row As DataRow, Status As Integer, ByVal ti As Threading.Thread)
    Public Event ThreadCompleteAudit(row As DataRow, Status As Integer, ByVal ti As Threading.Thread)

    Sub RunNowBulk()
        Try
            Status = 1
            UpdateTiltCampaignLastStatus(campaignID, Status)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@CampaignID", campaignID}
            }

            strConnection = GetSQL(4925, parray)(0)
            sqlParam = GetSQL(4925, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, 600)
            Status = 0
            UpdateTiltCampaignLastStatus(campaignID, Status)
        Catch ex As Exception
            Status = -1
            UpdateTiltCampaignLastStatus(campaignID, Status)
        Finally
            RaiseEvent ThreadComplete(CampaignRow, Status, Threading.Thread.CurrentThread)
        End Try
    End Sub

    Sub RunNowAudit()
        Try
            Status = 1
            UpdateTiltCampaignLastStatus(campaignID, Status)
            Dim strConnection As String = Nothing
            Dim sqlParam As String = Nothing
            Dim parray()() As String = {
                New String() {"@CampaignID", campaignID}
            }

            strConnection = GetSQL(4943, parray)(0)
            sqlParam = GetSQL(4943, parray)(1)
            IOS.DataLibrary.DataAccessorODBC.ExecuteNonQuery(strConnection, sqlParam, 10, 600)
            Status = 0
            UpdateTiltCampaignLastStatus(campaignID, Status)
        Catch ex As Exception
            Status = -1
            UpdateTiltCampaignLastStatus(campaignID, Status)
        Finally
            RaiseEvent ThreadCompleteAudit(CampaignRow, Status, Threading.Thread.CurrentThread)
        End Try
    End Sub

End Class