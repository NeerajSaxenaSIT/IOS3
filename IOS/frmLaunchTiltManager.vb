Imports IOS.Library
Imports IOS.DataLibrary
Imports IOS.Configuration
Imports DevExpress.XtraEditors
Imports DevExpress.XtraTreeList
Imports dotnetCHARTING.WinForms
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository

Public Class frmLaunchTiltManager

#Region "Variables Declaration"

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

#End Region

#Region "Properties"

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

    Private Sub frmLaunchTiltManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ch_TiltManager.Application = "FMO675rMTuz7ge/IXZez6Fl5RHDf0Vn5aOCJx7IDBrKiXoRxp6GbdHoYZZA3waRwu81GkRjB7v0zdrgxgKnSg6Vn+Q2ZRcqSTqI96jvXaTI="
            clsIOSImageList.GetTiltTreeImages(imgListValidation)
            ManageButtons(False)
            LoadTiltCampaigns()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

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

    Private Sub tlTiltManager_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
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
                MessageBox.Show("Please select campaign", "Submit Campaign", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            btnManageTree.Text = "Expand Tree"

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            btnCalculateAndSave.Appearance.BackColor = Nothing
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAddCampaign.Click
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
                If MessageBox.Show("Are you sure to delete campaign: " & campaignName & "?", "Delete Manual Tilt Campaign", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

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
        Try
            WaitScreen.ShowWaitScreen("Writing MML data to CSV...")
            Dim dsMML As DataSet = clsSQLCommands.GetMMLDataForTiltCampaign(connStrIOSServer, selectedTiltCampaignID)

            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.InitialDirectory = "C:\"
            'objFileDlg.Filter = "Comma Delimited|*.TXT"
            'objFileDlg.Title = "Save a TXT File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then
                    For i = 0 To dsMML.Tables.Count - 1

                        If i = 0 Then 'HUAWEI
                            Dim Content() As Byte = CSVBytesWriter(dsMML.Tables(i), False)
                            objFileDlg.FileName = "HUAWEI_MML_" + objFileDlg.FileName
                            Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            fs.Write(Content, 0, Content.Length)
                            fs.Close()
                        ElseIf i = 1 Then 'ERICSSON
                            Dim Content() As Byte = CSVBytesWriter(dsMML.Tables(i), False)
                            objFileDlg.FileName = "ERICSSON_XML_" + objFileDlg.FileName
                            Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            fs.Write(Content, 0, Content.Length)
                            fs.Close()
                        ElseIf i = 2 Then 'NOKIA
                            Dim Content() As Byte = CSVBytesWriter(dsMML.Tables(i), False)
                            objFileDlg.FileName = "NOKIA_XML_" + objFileDlg.FileName
                            Dim fs As System.IO.FileStream = objFileDlg.OpenFile()
                            fs.Write(Content, 0, Content.Length)
                            fs.Close()
                        End If

                    Next
                End If
            End If

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            MessageBox.Show("MML file created successfully", "Get MML", MessageBoxButtons.OK)
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
                MessageBox.Show("Sector list deleted successfully.", "Delete Sector List", MessageBoxButtons.OK)

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
            MessageBox.Show("E-Tilt cannot be beyond 15.0", "E-Tilt setting", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

#End Region

#Region "Methods"

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
        '  RemoveHandler tlTiltManager.CustomDrawNodeCell, AddressOf tlTiltManager_CustomDrawNodeCell
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
            '    AddHandler tlTiltManager.CustomDrawNodeCell, AddressOf tlTiltManager_CustomDrawNodeCell

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

            sc = Nothing
            de = Nothing
            ch_TiltManager.XAxis.Markers.Clear()
            ch_TiltManager.RefreshChart()
            ch_TiltManager.ResumeLayout()

        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        Finally
            If Not ch_TiltManager Is Nothing Then
                ch_TiltManager.Application = "FMO675rMTuz7ge/IXZez6Fl5RHDf0Vn5aOCJx7IDBrKiXoRxp6GbdHoYZZA3waRwu81GkRjB7v0zdrgxgKnSg6Vn+Q2ZRcqSTqI96jvXaTI="
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

    Private Sub cmbResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResolution.SelectedIndexChanged
        dtPointsTiltManager = Nothing
    End Sub



#End Region

End Class