Imports System.ComponentModel
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports IOS.Library
Imports LidorSystems.IntegralUI.Lists
Imports DevExpress.XtraEditors
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Columns
Imports System.Data.SqlClient
Imports System.IO

Public Class frmPMView

#Region "Variables"

    Private dtTech As DataTable = Nothing
    Private dtMmList As DataTable = Nothing
    Private dtCounterList As DataTable = Nothing
    Private dtMmPKColList As DataTable = Nothing
    Private p As Point
    Private SelectedRowIndex As Integer = -1
    Private RowListToHide As New List(Of Integer)
    Private counterFilterString As String = ""
    Private dtPMView As DataTable
    Private datetimeEdit As RepositoryItemDateEdit
    Private CountersTakenFromGrid As String = ""
    Private LastSearchKeyUpTime As DateTime = Nothing
    Private FilterQueryForDistinctDataColumn As String = ""
    Private Count_OT As Integer = 0
    Private dtCheckedObjs As DataTable = Nothing

#End Region

#Region "Methods"

    Private Sub ConfigurPMViewForm(ByVal frmName As String)
        Dim form As IOS.Configuration.EntityModel.IOSForm = configMgr.FindFormByName(frmName)
        If Not form Is Nothing Then
            Dim counter As Integer = 0
            ConfigurForm(Me, frmName, counter)

            Dim winCtrl As IOS.Configuration.EntityModel.Control = Nothing
            Dim formControls As List(Of Object) = New List(Of Object) From {
                cm_OT_tsmi_copy, cm_OT_tsmi_paste, cm_OT_tsmi_CheckChilds, tsmi_OT_UnCheck
            }

            For Each frmControl As Object In formControls
                winCtrl = form.FindControlByName(frmControl.Name)
                If Not winCtrl Is Nothing Then
                    frmControl.Enabled = winCtrl.DefaultEnable
                    frmControl.Visible = winCtrl.DefaultVisible
                End If
            Next

        End If
    End Sub

    Sub HideShowLeftControls()
        cmbTargetObject.Enabled = True
        tvObjectTree.Enabled = True
    End Sub

    Sub ClearComboBox(ByRef control As DevExpress.XtraEditors.ComboBoxEdit, ByVal firstItem As String)
        control.SuspendLayout()
        control.Properties.Items.Clear()
        control.Properties.Items.Insert(0, firstItem)
        control.SelectedIndex = 0
        control.Refresh()
        control.ResumeLayout()
    End Sub

    Private Sub BindTechnology()
        If (dtTech Is Nothing) Then
            If (dt_IOS_ObjectConfig IsNot Nothing) Then
                dtTech = New DataView(dt_IOS_ObjectConfig, "TemplateManager=1 and Vendor='" & cmbVendor.SelectedItem.ToString & "'", "Technology", DataViewRowState.CurrentRows).ToTable(True, "Technology")
            End If
        End If
        BindDevExComboBoxWithValueMember(cmbTechnology, dtTech, "Technology", "Technology", "Select Technology")
    End Sub

    Private Sub BindVendor()
        Dim dtCMVendor As DataTable = Nothing
        If (dt_IOS_ObjectConfig IsNot Nothing) Then
            dtCMVendor = New DataView(dt_IOS_ObjectConfig, "TemplateManager=1", "Vendor", DataViewRowState.CurrentRows).ToTable(True, "Vendor")
        End If
        BindDevExComboBoxWithValueMember(cmbVendor, dtCMVendor, "Vendor", "Vendor", "Select Vendor")
        ClearComboBox(cmbTechnology, "Select Technology")
        ClearComboBox(cmbTargetObject, "Object Type")
    End Sub

    Private Function GetTechnologyName(ByVal tech As String, ByVal vendor As String, ByVal returnObjectColumnsName As String) As String
        Dim rows() As DataRow = dt_IOS_ObjectConfig.Select("Vendor='" & vendor & "' AND Technology='" & tech & "' AND ParamHistory=1")
        If (rows.Count > 0) Then
            Return rows(0)(returnObjectColumnsName).ToString
        End If
        Return ""
    End Function

    Private Sub GetMeasurementList(ByVal vendorName As String, Optional ByVal technology As String = Nothing, Optional ByVal templateName As String = Nothing)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String
            Dim sqlParam As String

            Dim parray()() As String = {
                New String() {"@vendorName", "'" & vendorName & "'"},
                New String() {"@techName", IIf(technology Is Nothing, "NULL", "'" & technology & "'")}
            }

            strConnection = GetSQL(3700, parray)(0)
            sqlParam = GetSQL(3700, parray)(1)

            dtMmList = New System.Data.DataTable()
            dtMmList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            lstViewMeasurement.Columns.Clear()
            lstViewMeasurement.DataSource = dtMmList
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub GetListOfCounters(ByVal vendorName As String, ByVal measurement As String, Optional ByVal IsAllCounter As Boolean = False)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim strConnection As String
            Dim sqlParam As String

            Dim tech As String = ""
            If IsAllCounter Then
                If cmbTechnology.SelectedIndex > 0 Then
                    tech = cmbTechnology.SelectedItem.ToString()
                End If
            End If

            Dim parray()() As String = {
                New String() {"@vendorName", "'" & vendorName & "'"},
                New String() {"@measurement", "'" & measurement & "'"},
                New String() {"@tech", "'" & tech & "'"}
            }

            strConnection = GetSQL(3702, parray)(0)
            sqlParam = GetSQL(3702, parray)(1)

            dtCounterList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            tlCounterList.DataSource = Nothing
            tlCounterList.Columns.Clear()
            tlCounterList.DataSource = dtCounterList
            If tlCounterList.Columns.Count > 0 Then
                tlCounterList.Columns(0).Caption = "Measurement Name"
                tlCounterList.Columns(1).Caption = "Counter Name"
                tlCounterList.Columns(2).Visible = False
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub LoadObjectTypeCombo(ByVal mmName As String)
        dtMmPKColList = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC PM_GetMmTablePKColumnsWithOrdinal '" & cmbVendor.SelectedItem.ToString & "', '" & mmName & "'")
        BindDevExComboBoxWithTagMember(cmbTargetObject, dtMmPKColList, "COLUMN_NAME", "COLUMN_NAME", "Select Object", "ORDINAL_POSITION", True)
        tvObjectTree.Nodes.Clear()
    End Sub

    Private Sub GetSelectedNodes(ByRef list As List(Of KeyValuePair(Of String, List(Of String))))
        Dim obj As KeyValuePair(Of String, List(Of String))
        'For Each node As TreeNode In nodes
        '    If (node.Checked And node.Text <> "PLMN") Then
        '        obj = Nothing
        '        If list.Exists(Function(x) x.Key = node.Tag) Then
        '            obj = list.FirstOrDefault(Function(x) x.Key = node.Tag)
        '            obj.Value.Add(node.Text)
        '        Else
        '            Dim value As New List(Of String)
        '            value.Add(node.Text)
        '            obj = New KeyValuePair(Of String, List(Of String))(node.Tag, value)
        '            list.Add(obj)
        '        End If
        '    End If
        '    GetSelectedNodes(node.Nodes, list)
        'Next

        Dim tagCol As String = Nothing
        If cmbTargetObject.SelectedIndex > 0 Then
            Dim view As New DataView(dtMmPKColList)
            view.Sort = "ORDINAL_POSITION DESC"
            Dim dtTemp As DataTable = view.ToTable()
            Dim selectedColOrdinal As Integer = cmbTargetObject.SelectedItem.Tag

            For Each dtRow As DataRow In dtTemp.Rows
                If CInt(dtRow.Item(1)) <= selectedColOrdinal Then
                    tagCol = tagCol & ", " & dtRow.Item(0)
                End If
            Next

            If tagCol IsNot Nothing Then
                tagCol = tagCol.TrimStart(",")
            End If
        End If

        For iCntr As Integer = 0 To lstTreeObjects.ItemCount - 1
            If list.Exists(Function(x) x.Key = tagCol) Then
                obj = list.FirstOrDefault(Function(x) x.Key = tagCol)
                obj.Value.Add(CType(lstTreeObjects.GetItem(iCntr), DataRowView).Row(0).ToString)
            Else
                Dim value As New List(Of String)
                value.Add(CType(lstTreeObjects.GetItem(iCntr), DataRowView).Row(0).ToString)
                obj = New KeyValuePair(Of String, List(Of String))(tagCol, value)
                list.Add(obj)
            End If
        Next
    End Sub

    Private Sub EnableSearchCheckBox()
        If cmbVendor.SelectedIndex > 0 And cmbTechnology.SelectedIndex > 0 Then
            chkSearchAllParameter.Enabled = True
        Else
            chkSearchAllParameter.Enabled = False
            chkSearchAllParameter.Checked = False
        End If
    End Sub

    Private Sub LoadObjectTreeFromMeasurement(strColumns As String, objectFilter As String)
        tvObjectTree.SuspendLayout()
        tvObjectTree.Nodes.Clear()
        tvObjectTree.Columns.Clear()
        Application.DoEvents()

        Dim dtObjectTree As DataTable = Nothing
        Dim node As TreeListNode = lstViewMeasurement.FocusedNode
        Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
        Dim mmName As String = Nothing
        If data IsNot Nothing Then
            mmName = data.Item(0).ToString
        End If

        dtObjectTree = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [PM_GetObjectTreeData] '" & cmbVendor.SelectedItem.ToString & "', '" & strColumns & "', '" & mmName & "', '" & objectFilter & "'")
        FillObjectTreeList(tvObjectTree, dtObjectTree)
        tvObjectTree.ResumeLayout()
    End Sub

    Private Sub FillObjectTreeList(ByRef tl As TreeList, ByRef dt As DataTable)
        Try
            tl.Cursor = Cursors.WaitCursor
            tl.BeginUnboundLoad()
            Application.DoEvents()

            Dim colList() As String = {"PLMN", dt.Columns(0).ColumnName}
            tl.Columns.Clear()
            For i As Integer = 0 To colList.Length - 1
                Dim col1 As New TreeListColumn()
                col1.Caption = colList(i)
                col1.VisibleIndex = i
                If colList(i) = "PLMN" Then
                    tl.AutoFillColumn = col1
                    col1.Visible = True
                Else
                    col1.Visible = False
                End If
                tl.Columns.Add(col1)
            Next
            tl.Nodes.Clear()

            Dim tlNode As TreeListNode = tl.Nodes.Add(New Object() {"PLMN", "PLMN"})

            For Each drParent As DataRow In dt.Rows
                Dim subNode As TreeListNode = tl.AppendNode(New Object() {drParent(dt.Columns(dt.Columns.Count - 1).ColumnName), drParent(dt.Columns(dt.Columns.Count - 1).ColumnName)}, tlNode)
            Next

        Catch ex As Exception
        Finally
            tl.EndUnboundLoad()
            If tl.Nodes.Count > 0 Then
                tl.SelectNode(tl.Nodes(0))
                tl.SetFocusedNode(tl.Nodes(0))
                tl.CollapseAll()
                tl.ExpandToLevel(0)
            End If
            tl.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Public Sub PopulateObjectTreeList(ByRef tl As TreeList, ParentID As String, rNode As TreeListNode, dt As DataTable)
        Dim foundRows() As DataRow = Nothing
        foundRows = dt.Select(dt.Columns(dt.Columns.Count - 1).ColumnName & " = " & Chr(39) & ParentID & Chr(39))

        rNode.Nodes.Clear()

        If foundRows.Length > 0 Then
            For Each row As DataRow In foundRows
                If row.Item(0).ToString <> "" Then
                    Dim parentnode As TreeListNode = tl.AppendNode(New Object() {row.Item(dt.Columns(0).ColumnName), row.Item(dt.Columns(0).ColumnName)}, rNode)
                    'PopulateObjectTreeList(tl, row.Item(dt.Columns(0).ColumnName), parentnode, dt)
                End If
            Next row
        Else
            'If row.Item(0).ToString <> "" Then
            Dim parentNode As TreeListNode = tl.AppendNode(New Object() {ParentID}, rNode)
            'PopulateObjectTreeList(tl, row.Item(dt.Columns(0).ColumnName), parentNode, dt)
            'End If
        End If
    End Sub

    Private Sub FillObjectTree(dtData As DataTable, ByRef tree As TreeView)
        Try
            tree.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            Dim rootn As TreeNode = New TreeNode()
            rootn.Text = "PLMN"
            rootn.ImageKey = "EMPTY"
            rootn.SelectedImageKey = "EMPTY"
            tree.Nodes.Clear()
            tree.Nodes.Add(rootn)
            Dim tNode As New TreeNode
            tNode = tree.Nodes(0)

            Dim dtParent As DataTable = dtData.DefaultView.ToTable(True, dtData.Columns(dtData.Columns.Count - 1).ColumnName)
            For Each drParent As DataRow In dtParent.Rows
                Dim roottn As TreeNode = New TreeNode()
                roottn.Text = drParent(0).ToString
                roottn.Tag = dtData.Columns(dtData.Columns.Count - 1).ColumnName
                roottn.ImageKey = "EMPTY"
                roottn.SelectedImageKey = "EMPTY"
                tNode.Nodes.Add(roottn)
                PopulateObjectTree(drParent.Table.Columns(0).ColumnName, roottn.Text, roottn, dtData)
            Next
            tNode.ExpandAll()
            System.GC.Collect()
        Catch ex As Exception
        Finally
            tree.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub PopulateObjectTree(ByVal parentColName As String, ByVal inParentID As String, ByVal inTreeNode As TreeNode, ByVal dt As DataTable)
        Try
            Dim dtChild As New DataTable
            For i As Integer = dt.Columns.Count - 1 To 0 Step -1
                If parentColName = dt.Columns(i).ColumnName Then
                    If i - 1 < 0 Then Exit Sub
                    dtChild = dt.DefaultView.ToTable(True, dt.Columns(i - 1).ColumnName, parentColName)
                    Exit For
                End If
            Next i

            inTreeNode.Nodes.Clear()
            For Each drParent As DataRow In dtChild.Select(parentColName & "='" & inParentID & "'")
                Dim roottn As TreeNode = New TreeNode()
                roottn.Text = drParent(0).ToString
                roottn.Tag = drParent.Table.Columns(0).ColumnName
                roottn.ImageKey = "EMPTY"
                roottn.SelectedImageKey = "EMPTY"
                inTreeNode.Nodes.Add(roottn)
                PopulateObjectTree(drParent.Table.Columns(0).ColumnName, roottn.Text, roottn, dt)
            Next
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try
    End Sub

    Private Function Treeview_GetNodeLevel(Optional ByVal subtech As String = Nothing) As Integer 'TODO
        If subtech = "PLMN" Then Return 0
        Dim dr() As DataRow = dtMmPKColList.Select("COLUMN_NAME=" & Chr(39) & subtech & Chr(39))
        If Not dr Is Nothing Then
            If dr.Count > 0 Then
                Dim level As Integer = ObjectTree_GetLevel(CInt(dr(0)("ORDINAL_POSITION").ToString))
                Return level
            End If
        End If
        Return Nothing
    End Function

    Function ObjectTree_GetLevel(ByVal id As Integer) As Integer
        Dim level As Integer = 0
        Dim dr() As DataRow = dtMmPKColList.Select("ORDINAL_POSITION=" & Chr(39) & id & Chr(39))
        If (dr.Count > 0) Then
            If dr(0)("ORDINAL_POSITION").ToString <> 0 Then
                level = CInt(dr(0)("ORDINAL_POSITION").ToString) - 1
            End If
        End If

        Return level
    End Function

    Private Sub FillCheckedObjectsIntoList(nd As TreeListNode)
        Try
            tvObjectTree.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If nd IsNot Nothing AndAlso nd.Checked = True Then
                If nd.GetDisplayText(cmbTargetObject.SelectedItem.ToString) = "PLMN" Then
                    For Each tln As TreeListNode In nd.Nodes
                        Dim drCO As DataRow = dtCheckedObjs.NewRow()
                        drCO("ObjectName") = tln.GetDisplayText(cmbTargetObject.SelectedItem.ToString)
                        dtCheckedObjs.Rows.Add(drCO)
                        dtCheckedObjs.AcceptChanges()
                    Next
                    RefreshObjectsInListBox()
                    Count_OT = lstTreeObjects.ItemCount
                Else
                    If dtCheckedObjs.AsEnumerable().Where(Function(x) x.Field(Of String)("ObjectName") = nd.GetDisplayText(cmbTargetObject.SelectedItem.ToString)).Count = 0 Then
                        Dim drCO As DataRow = dtCheckedObjs.NewRow()
                        drCO("ObjectName") = nd.GetDisplayText(cmbTargetObject.SelectedItem.ToString)
                        dtCheckedObjs.Rows.Add(drCO)
                        dtCheckedObjs.AcceptChanges()
                        RefreshObjectsInListBox()
                        Count_OT = lstTreeObjects.ItemCount
                    End If
                End If
            End If
        Catch
        Finally
            tvObjectTree.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub RemoveUncheckedObjectFromList(nd As TreeListNode)
        If dtCheckedObjs IsNot Nothing Then
            If dtCheckedObjs.Rows.Count > 0 Then
                If nd IsNot Nothing AndAlso nd.Checked = False Then
                    Dim dr As DataRow = dtCheckedObjs.AsEnumerable().Where(Function(x) x.Field(Of String)("ObjectName") = nd.GetDisplayText(cmbTargetObject.SelectedItem.ToString))(0)
                    dtCheckedObjs.Rows.Remove(dr)
                    dtCheckedObjs.AcceptChanges()
                    RefreshObjectsInListBox()
                    Count_OT = lstTreeObjects.ItemCount
                End If
            End If
        End If
    End Sub

    Private Sub RefreshObjectsInListBox()
        lstTreeObjects.Items.Clear()
        lstTreeObjects.DisplayMember = "ObjectName"
        lstTreeObjects.ValueMember = "ObjectName"
        lstTreeObjects.Tag = cmbTargetObject.SelectedItem.ToString
        lstTreeObjects.DataSource = dtCheckedObjs
    End Sub

#End Region

#Region "Infinite Scrolling"

    Private QueryOffset As Integer = 0
    Private IsFirstTimeLoading As Boolean = False
    Private currViewRowFilter As String = ""
    Private currViewSortStr As String = ""

    Dim _virtualServerModeSrouce As VirtualServerModeSource
    Dim objLock As New Object

    Private Function CreateData(ByVal _measurement As String, _vendor As String, _offset As Integer, _batchSize As Integer, Optional _counters As String = Nothing, Optional _customFilter As String = Nothing, Optional _sortExpression As String = Nothing) As DataTable
        Try
            FilterQueryForDistinctDataColumn = ""

            If CountersTakenFromGrid <> "" Then
                _counters = CountersTakenFromGrid
            End If

            Dim strConnection As String
            Dim sqlParam As String
            Dim startTime As Date = dePMViewStart.EditValue
            Dim endTime As Date = dePMViewEnd.EditValue

            Dim counterList As String = GetCounters()

            Dim objList As New List(Of KeyValuePair(Of String, List(Of String)))
            Dim filterQry As String = Nothing
            GetSelectedNodes(objList)  'tvObjectTree.Nodes,

            For Each itm As KeyValuePair(Of String, List(Of String)) In objList
                If filterQry IsNot Nothing Then
                    filterQry = filterQry & " AND "
                Else
                    filterQry = " WHERE "
                End If
                filterQry = filterQry & itm.Key & " IN ("
                For Each itm1 As String In itm.Value
                    filterQry = filterQry & "''" & itm1 & "'',"
                Next
                filterQry = filterQry.TrimEnd(",") & ")"
            Next

            If counterFilterString <> "" Then
                filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & counterFilterString
            End If

            If _customFilter IsNot Nothing AndAlso _customFilter <> "" Then
                filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & _customFilter.Replace("'", "''")
            End If

            Dim startTimeString As String = startTime.ToString("yyyy-MM-dd HH:mm:ss")
            Dim endTimeString As String = endTime.ToString("yyyy-MM-dd HH:mm:ss")
            Dim periodFilterQry As String = " Period_Start_Time >= ''" & startTimeString & "''  And  Period_Start_Time <= ''" & endTimeString & "''"
            filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & periodFilterQry


            FilterQueryForDistinctDataColumn = filterQry

            Dim parray()() As String = {
                New String() {"@vendorName", _vendor},
                New String() {"@measurement", "'" & _measurement & "'"},
                New String() {"@param", IIf(_counters Is Nothing Or _counters = "", "'" & counterList & "'", "'" & _counters & "'")},
                New String() {"@n", _offset},
                New String() {"@m", _batchSize},
                New String() {"@filter", Chr(39) & filterQry & Chr(39)},
                New String() {"@sortExpr", IIf(String.IsNullOrEmpty(_sortExpression), "NULL", Chr(39) & _sortExpression & Chr(39))}
            }

            strConnection = GetSQL(3701, parray)(0)
            sqlParam = GetSQL(3701, parray)(1)

            Dim dtPMViewTemp As New DataTable
            dtPMViewTemp = IOS.DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
            Return dtPMViewTemp

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

        Return Nothing

    End Function

    Private Sub MeasurementToGrid(ByVal measurement As String, vendor As String, noOfRows As Integer, Optional counters As String = Nothing)
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.UseWaitCursor = True
            Application.DoEvents()
            IsFirstTimeLoading = True
            lblMeasurementName.Text = "Current Data: " & measurement.ToString
            lblMeasurementName.Tag = measurement.ToString
            gcPMView.Tag = "MeasurementData"

            QueryOffset = 0
            lblQueryBatchSize.Visible = True
            txtQueryBatchSize.Visible = True
            currViewRowFilter = ""
            currViewSortStr = ""
            dtPMView = Nothing

            If (_virtualServerModeSrouce IsNot Nothing) Then
                RemoveHandler _virtualServerModeSrouce.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
                RemoveHandler _virtualServerModeSrouce.ConfigurationChanged, AddressOf virtualServerModeSource_ConfigurationChanged
                RemoveHandler _virtualServerModeSrouce.MoreRows, AddressOf VirtualServerModeSource_MoreRows
                RemoveHandler _virtualServerModeSrouce.GetUniqueValues, AddressOf virtualServerModeSource_GetUniqueValues
            End If

            _virtualServerModeSrouce = New VirtualServerModeSource()

            AddHandler _virtualServerModeSrouce.AcquireInnerList, AddressOf VirtualServerModeSource_AcquireInnerList
            AddHandler _virtualServerModeSrouce.ConfigurationChanged, AddressOf virtualServerModeSource_ConfigurationChanged
            AddHandler _virtualServerModeSrouce.MoreRows, AddressOf VirtualServerModeSource_MoreRows
            AddHandler _virtualServerModeSrouce.GetUniqueValues, AddressOf virtualServerModeSource_GetUniqueValues

            gcPMView.DataSource = Nothing
            gvPMView.OptionsView.ColumnAutoWidth = False
            gvPMView.Columns.Clear()
            gcPMView.DataSource = _virtualServerModeSrouce

            datetimeEdit = New RepositoryItemDateEdit()
            datetimeEdit.VistaEditTime = DevExpress.Utils.DefaultBoolean.True
            datetimeEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime
            datetimeEdit.Mask.UseMaskAsDisplayFormat = True
            If regionalSettings = False Then
                datetimeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm:ss"
            Else
                datetimeEdit.Mask.EditMask = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
            End If

            If dtPMView IsNot Nothing Then
                For Each dtCol As DataColumn In dtPMView.Columns
                    If dtCol.DataType = GetType(DateTime) Then
                        gvPMView.Columns(dtCol.ColumnName).ColumnEdit = datetimeEdit
                        gvPMView.Columns(dtCol.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        If regionalSettings = False Then
                            gvPMView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss"
                        Else
                            gvPMView.Columns(dtCol.ColumnName).DisplayFormat.FormatString = CultureInfoDefault.DateTimeFormat.ShortDatePattern & " " & CultureInfoDefault.DateTimeFormat.ShortTimePattern
                        End If
                    End If
                Next
            End If

            gvPMView.OptionsView.BestFitMaxRowCount = 1
            gvPMView.BestFitColumns()

        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.UseWaitCursor = False
            Application.DoEvents()
        End Try
    End Sub

    Private Sub VirtualServerModeSource_AcquireInnerList(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeAcquireInnerListEventArgs)
        Try
            Dim dtTempColumn As New DataTable
            If dtPMView Is Nothing Then
                Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    dtTempColumn = CreateData(data.Item(0).ToString(), cmbVendor.SelectedItem.ToString, 0, 1) ' CreateData is called to initialize column structure for infinite grid
                End If
            Else
                dtTempColumn = dtPMView.Rows.Cast(Of System.Data.DataRow).Take(1)
            End If

            If dtTempColumn IsNot Nothing Then
                e.InnerList = dtTempColumn.DefaultView
            End If
            e.AddMoreRowsFunc = AddressOf AddMoreRows
            e.ClearAndAddRowsFunc = AddressOf ClearAndAddMoreRows
            e.ReleaseAction = AddressOf ReleaseList
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub ReleaseList(ByVal list As IList)
        TryCast(list, DataView).Table.Rows.Clear()
    End Sub

    Public Function AddMoreRows(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStr
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ClearAndAddMoreRows(ByVal list As IList, ByVal en As IEnumerable) As IList
        Try
            Dim data = TryCast(en, DataView)
            TryCast(list, DataView).Table.Rows.Clear()
            For Each dr As DataRow In data.Table.Rows
                TryCast(list, DataView).Table.Rows.Add(dr.ItemArray)
            Next dr
            TryCast(list, DataView).Sort = currViewSortStr
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub VirtualServerModeSource_MoreRows(sender As Object, e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            If gcPMView.Tag = "MeasurementData" Then
                If IsFirstTimeLoading Then
                    gvPMView.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator
                Else
                    gvPMView.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Panel
                End If
                e.RowsTask = Task.Factory.StartNew(
              Function()
                  SyncLock objLock
                      Try
                          Dim counterFilter As String = GetCounters()
                          Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                          Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
                          If data IsNot Nothing Then
                              Dim dtData As New DataTable
                              If e.UserData Is Nothing Then
                                  If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                                      dtData = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, QueryOffset, Val(txtQueryBatchSize.Text), counterFilter, currViewRowFilter, currViewSortStr)
                                  Else
                                      dtData = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, QueryOffset, Val(txtQueryBatchSize.Text), counterFilter, currViewRowFilter)
                                  End If
                              Else
                                  dtData = CType(e.UserData, DataView).ToTable()
                              End If

                              If dtPMView IsNot Nothing Then
                                  dtPMView.Merge(dtData)
                                  QueryOffset = dtPMView.Rows.Count
                              Else
                                  dtPMView = dtData
                              End If
                              'QueryOffset = dtPMView.Rows.Count

                              Dim nextBatch = Nothing
                              If dtPMView IsNot Nothing Then
                                  nextBatch = dtPMView.Clone()
                              End If

                              Dim moreRows As Boolean = True
                              Dim rowCount As Integer = e.CurrentRowCount

                              If nextBatch IsNot Nothing AndAlso dtData IsNot Nothing Then
                                  Do While nextBatch.Rows.Count < dtData.Rows.Count
                                      nextBatch.ImportRow(dtPMView.Rows(rowCount))
                                      rowCount += 1
                                  Loop
                                  moreRows = e.CurrentRowCount + Val(txtQueryBatchSize.Text) <= rowCount
                                  Return New VirtualServerModeRowsTaskResult(nextBatch.DefaultView, moreRows, Nothing)
                              End If
                          Else
                              Dim dt As New DataTable
                              Return New VirtualServerModeRowsTaskResult(dt.DefaultView, False, Nothing)
                          End If
                      Catch
                          Dim dt As New DataTable
                          Return New VirtualServerModeRowsTaskResult(dt.DefaultView, False, Nothing)
                      End Try
                  End SyncLock
                  Return Nothing
              End Function, e.CancellationToken)
                If IsFirstTimeLoading Then
                    IsFirstTimeLoading = False
                    e.RowsTask.Wait(e.CancellationToken)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub virtualServerModeSource_ConfigurationChanged(ByVal sender As Object, ByVal e As DevExpress.Data.VirtualServerModeRowsEventArgs)
        Try
            QueryOffset = 0
            dtPMView = Nothing

            currViewRowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(e.ConfigurationInfo.Filter)
            If TypeOf e.ConfigurationInfo.Filter Is GroupOperator Then
                Try
                    If CType(e.ConfigurationInfo.Filter, GroupOperator).Operands.Count > 0 Then
                        Dim objGO As New GroupOperator()
                        Dim lastColumn As String = ""
                        For Each obj In CType(e.ConfigurationInfo.Filter, GroupOperator).Operands
                            If TypeOf (obj) Is BinaryOperator AndAlso CType(CType(obj, BinaryOperator).LeftOperand, OperandProperty).PropertyName.ToLower() <> lastColumn Then
                                lastColumn = CType(CType(obj, BinaryOperator).LeftOperand, OperandProperty).PropertyName.ToLower()
                                If CType(CType(obj, BinaryOperator).LeftOperand, OperandProperty).PropertyName.ToLower() = "period_start_time" Then
                                    Dim rightOperand As OperandValue = CType(CType(CType(e.ConfigurationInfo.Filter, GroupOperator).Operands(0), BinaryOperator).RightOperand, OperandValue)
                                    Dim value As Date = CDate(rightOperand.Value)
                                    objGO.Operands.Add(New BetweenOperator(CType(CType(CType(e.ConfigurationInfo.Filter, GroupOperator).Operands(0), BinaryOperator).LeftOperand, OperandProperty).PropertyName,
                                    New DateTime(value.Year, value.Month, value.Day, 0, 0, 0),
                                    New DateTime(value.Year, value.Month, value.Day, 23, 59, 59)))
                                Else
                                    objGO.Operands.Add(obj)
                                End If
                            ElseIf TypeOf (obj) Is FunctionOperator Then
                                objGO.Operands.Add(obj)
                            End If
                        Next
                        If currViewRowFilter.Contains(objGO.ToString) Then
                            currViewRowFilter = currViewRowFilter
                        Else
                            currViewRowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(objGO)
                        End If
                        currViewRowFilter = currViewRowFilter.Replace("#", "'")
                    End If
                Catch ex As Exception
                End Try
            End If

            If e.ConfigurationInfo.SortInfo IsNot Nothing AndAlso e.ConfigurationInfo.SortInfo.Length > 0 Then
                currViewSortStr = e.ConfigurationInfo.SortInfo(0).ToString()
            End If

            Dim counterFilter As String = GetCounters()
            Dim node As TreeListNode = lstViewMeasurement.FocusedNode
            Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
            If data IsNot Nothing Then
                Dim dtData As New DataTable
                dtData = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, QueryOffset, Val(txtQueryBatchSize.Text), counterFilter, currViewRowFilter, currViewSortStr)
                If dtData IsNot Nothing Then
                    e.UserData = dtData.DefaultView
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub virtualServerModeSource_GetUniqueValues(ByVal sender As Object, ByVal e As VirtualServerModeGetUniqueValuesEventArgs)
        e.UniqueValuesTask =
            New System.Threading.Tasks.Task(Of Object())(Function()
                                                             Dim node As TreeListNode = lstViewMeasurement.FocusedNode
                                                             Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
                                                             If data IsNot Nothing Then
                                                                 Dim dt As New DataTable
                                                                 dt = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, "EXEC [dbo].[PM_Measurement_Column_Data] '" & cmbVendor.SelectedItem.ToString & "', '" & data.Item(0).ToString() & "', '[" & e.ValuesPropertyName & "]' , '" & FilterQueryForDistinctDataColumn & "'")
                                                                 Dim filterValue() As Object
                                                                 filterValue = dt.Rows.OfType(Of DataRow)().Select(Function(x) x.Item(0)).ToArray()
                                                                 Return filterValue
                                                             Else
                                                                 Return Nothing
                                                             End If
                                                         End Function, e.CancellationToken)
    End Sub

#End Region

#Region "Events"

    Private Sub frmPMView_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.DoubleBuffered = True
            Me.SuspendLayout()
            Me.WindowState = FormWindowState.Normal
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.Location = Screen.FromControl(frmMDI).Bounds.Location
            Me.BringToFront()

            dePMViewStart.EditValue = DateAdd(DateInterval.Day, -1, New DateTime(Now().Year, Now().Month, Now.Day, 0, 0, 0))
            dePMViewEnd.EditValue = New DateTime(Now().Year, Now().Month, Now.Day, Now().Hour + 1, 0, 0)

            dePMViewStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
            dePMViewStart.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
            dePMViewStart.Properties.EditMask = "dd/MM/yyyy HH:mm"

            dePMViewEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
            dePMViewEnd.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
            dePMViewEnd.Properties.EditMask = "dd/MM/yyyy HH:mm"

            'DevExpress.Utils.Filtering.ExcelFilterOptions.Default.DefaultDateFilterType = DevExpress.Utils.Filtering.Internal.CustomUIFilterType.Equals
            DevExpress.Utils.Filtering.ExcelFilterOptions.Default.ShowNulls = False
            'DevExpress.Utils.Filtering.ExcelFilterOptions.Default.PreferredTabType = DevExpress.Utils.Filtering.ExcelFilterOptions.TabType.Filters
            DevExpress.Utils.Filtering.ExcelFilterOptions.Default.PreferredDateTimeValuesTabFilterType = DevExpress.Utils.Filtering.ExcelFilterOptions.DateTimeValuesTabFilterType.List

            BindVendor()
            BindComboWithPredefinedPeriod(cmbPmViewPreDef)
            Me.ResumeLayout()
            ConfigurPMViewForm("frmPMView")
            gcPMView.Tag = "MeasurementData"
            FiltersInitialize()
            ceLoadObjectTree_CheckedChanged(Nothing, Nothing)
            GetDataTableForCheckedObjects()
            sccObjects.SplitterPosition = Math.Abs(sccObjects.Height / 2) * 1.5
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub GetDataTableForCheckedObjects()
        dtCheckedObjs = New DataTable
        dtCheckedObjs.Columns.Add("ObjectName", GetType(String))
    End Sub

    Private Sub cmbTechnology_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTechnology.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            ClearComboBox(cmbTargetObject, "Object Type")
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcPMView)
            If (cmbVendor.SelectedIndex > 0 AndAlso cmbTechnology.SelectedIndex > 0) Then
                GetMeasurementList(cmbVendor.SelectedItem.ToString, cmbTechnology.SelectedItem.ToString)
            ElseIf cmbTechnology.SelectedIndex = 0 AndAlso cmbVendor.SelectedIndex > 0 Then
                GetMeasurementList(cmbVendor.SelectedItem.ToString)
            End If

            EnableSearchCheckBox()
            chkSearchAllParameter.Checked = False
            If dtCheckedObjs IsNot Nothing Then
                dtCheckedObjs.Rows.Clear()
            End If
            RefreshObjectsInListBox()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            lstViewMeasurement.DataSource = Nothing
            tlCounterList.DataSource = Nothing
            ClearComboBox(cmbTechnology, "Select Technology")
            ClearComboBox(cmbTargetObject, "Object Type")
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcPMView)

            If cmbVendor.SelectedIndex > 0 Then
                BindTechnology()
            End If
            EnableSearchCheckBox()
            If dtCheckedObjs IsNot Nothing Then
                dtCheckedObjs.Rows.Clear()
            End If
            RefreshObjectsInListBox()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    'Private Sub txtSearchObject_KeyUp(sender As Object, e As KeyEventArgs)
    '    Me.Cursor = Cursors.WaitCursor
    '    Application.DoEvents()
    '    Try
    '        tvObjectTree.SuspendLayout()
    '        tvObjectTree.Nodes.Clear()
    '        tvObjectTree.Refresh()
    '        tvObjectTree.ResumeLayout()
    '        If (cmbTargetObject.SelectedIndex > 0) Then
    '            Dim strCols As String = Nothing
    '            If Not cmbTargetObject.SelectedItem Is Nothing Then
    '                Dim view As New DataView(dtMmPKColList)
    '                view.Sort = "ORDINAL_POSITION DESC"
    '                Dim dtTemp As DataTable = view.ToTable()
    '                Dim selectedColOrdinal As Integer = cmbTargetObject.SelectedItem.Tag

    '                For Each dtRow As DataRow In dtTemp.Rows
    '                    If CInt(dtRow.Item(1)) <= selectedColOrdinal Then
    '                        strCols = strCols & ", " & dtRow.Item(0)
    '                    End If
    '                Next
    '            End If
    '            strCols = strCols.TrimStart(",")
    '            If txtSearchObject.Text.Length = 0 Then
    '                tvObjectTree.Nodes.Clear()
    '                'LoadObjectTreeFromMeasurement(strCols, txtSearchObject.Text.Trim)
    '            ElseIf txtSearchObject.Text.Length > 1 Then
    '                LoadObjectTreeFromMeasurement(strCols, txtSearchObject.Text.Trim)
    '            End If
    '        End If
    '        tvObjectTree.Name = "tvObjectTree"
    '    Catch ex As Exception
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '        Application.DoEvents()
    '    End Try
    'End Sub

    'Private Sub TreeViewStats_AfterCheck(sender As Object, e As TreeViewEventArgs)
    '    Try
    '        CheckTreeNodeAndCount(e.Node, 0, Nothing)
    '        If e.Node.Checked = True Then
    '            'Count_OT = Count_OT + 1
    '            FillCheckedObjectsIntoList(e.Node)
    '        ElseIf e.Node.Checked = False Then
    '            'Count_OT = Count_OT - 1
    '            RemoveUncheckedObjectFromList(e.Node)
    '        End If

    '        lblTreeObjectsCount.Text = "#: " & CStr(Count_OT)

    '    Catch ex As Exception
    '        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
    '    End Try
    'End Sub

    Private Sub tvObjectTree_DragOver(sender As Object, e As DragEventArgs) Handles tvObjectTree.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    'Private Sub TreeViewStats_MouseDown(sender As Object, e As MouseEventArgs)
    '    Dim tree As TreeView = TryCast(sender, TreeView)
    '    If (tree IsNot Nothing) Then
    '        Dim item As TreeViewHitTestInfo = tree.HitTest(e.Location)
    '        If item.Node IsNot Nothing Then
    '            If (e.Button = MouseButtons.Left) Then
    '                tree.DoDragDrop(item.Node, DragDropEffects.Copy)
    '            Else
    '                tree.SelectedNode = item.Node
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub tvObjectTree_MouseDown(sender As Object, e As MouseEventArgs) Handles tvObjectTree.MouseDown
        Dim tl As TreeList = TryCast(sender, TreeList)
        If (tl IsNot Nothing) Then
            Dim item As TreeListHitInfo = tl.CalcHitInfo(e.Location)
            If item.Node IsNot Nothing Then
                If (e.Button = MouseButtons.Left) Then
                    tl.DoDragDrop(item.Node, DragDropEffects.Copy)
                Else
                    tl.FocusedNode = item.Node
                End If
            End If
        End If
    End Sub

    Private Sub lstViewMO_MouseMove(sender As Object, e As MouseEventArgs) Handles lstViewMeasurement.MouseMove
        Try
            If e.Button = MouseButtons.Left Then
                Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    Dim obj() As Object = {"MM2Grid", data.Item(0)}
                    lstViewMeasurement.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lstViewMO_SelectedIndexChanged(sender As Object, e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles lstViewMeasurement.FocusedNodeChanged
        Try
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
            Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
            Dim data_old As DataRowView = lstViewMeasurement.GetDataRecordByNode(e.OldNode)

            Dim MMList As String = Nothing
            Dim MMList_old As String = Nothing
            If Not data_old Is Nothing Then
                MMList_old = data_old.Item(0).ToString
            End If

            If chkSearchAllParameter.Checked = False Then

                If cmbVendor.SelectedIndex > 0 Then
                    If data IsNot Nothing Then
                        MMList = data.Item(0).ToString
                        Dim tech As String = Nothing
                        If cmbTechnology.SelectedIndex > 0 Then
                            tech = cmbTechnology.SelectedItem.ToString()
                        End If
                        GetListOfCounters(cmbVendor.SelectedItem.ToString, MMList)
                    End If
                Else
                    tlCounterList.DataSource = Nothing
                End If
            End If

            If ceLoadObjectTree.Checked Then
                MMList = Replace(Replace(Replace(Replace(Replace(Replace(MMList, "_RAW", ""), "_DAY", ""), "_WEEK", ""), "_MONTH", ""), "_HOUR", ""), "_BH", "")
                MMList_old = Replace(Replace(Replace(Replace(Replace(Replace(MMList_old, "_RAW", ""), "_DAY", ""), "_WEEK", ""), "_MONTH", ""), "_HOUR", ""), "_BH", "")

                If data IsNot Nothing And MMList <> MMList_old Then
                    LoadObjectTypeCombo(data.Item(0).ToString)

                    Count_OT = 0
                    lblTreeObjectsCount.Text = "#: " & CStr(Count_OT)
                    dtCheckedObjs.Rows.Clear()

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub gcPMView_DragDrop(sender As Object, e As DragEventArgs) Handles gcPMView.DragDrop
        Try
            Dim singleCounterDropped As Boolean = False
            Dim mm() As Object = e.Data.GetData("System.Object[]")
            If mm IsNot Nothing Then
                If mm(0) = "MM2Grid" Then
                    CountersTakenFromGrid = ""

                    If chkSearchAllParameter.Checked = False Then
                        GetListOfCounters(cmbVendor.SelectedItem.ToString(), mm(1))
                    End If

                    MeasurementToGrid(mm(1), cmbVendor.SelectedItem.ToString, Val(txtQueryBatchSize.Text))
                ElseIf mm(0) = "Counter2Grid" Then

                    Dim data As DataRowView = Nothing
                    If mm(1).GetType().Name = "DataRowView" Then
                        data = CType(mm(1), DataRowView)
                        singleCounterDropped = True
                    Else
                        data = CType(mm(1)(0), DataRowView)
                        singleCounterDropped = False
                    End If

                    If gcPMView.DataSource Is Nothing Or lblMeasurementName.Tag <> data.Item(0).ToString() Then
                        gcPMView.DataSource = Nothing
                        gvPMView.Columns.Clear()

                        Dim counterFilter As String = ""
                        For Each dr As DataRow In dtCounterList.Select("[Constraint_Type]='PRIMARY KEY' And [MMNAME]='" & data.Item(0).ToString() & "'")
                            counterFilter = counterFilter & ", [" & dr.Item(1).ToString() & "]"
                        Next

                        If singleCounterDropped = True Then
                            counterFilter = counterFilter & IIf(counterFilter.Contains("[" & data.Item(1).ToString() & "]"), "", ", [" & data.Item(1).ToString() & "]")
                        Else
                            For iCntr As Integer = 0 To mm(1).Count - 1
                                counterFilter = counterFilter & IIf(counterFilter.Contains("[" & mm(1).Item(iCntr)(1).ToString() & "]"), "", ", [" & mm(1).Item(iCntr)(1).ToString() & "]")
                            Next
                        End If

                        counterFilter = counterFilter.TrimStart(", ")
                        CountersTakenFromGrid = counterFilter

                        MeasurementToGrid(data.Item(0).ToString(), cmbVendor.SelectedItem.ToString, Val(txtQueryBatchSize.Text), counterFilter)
                    Else
                        If lblMeasurementName.Tag = data.Item(0).ToString() Then
                            Dim gCOl As GridColumn = Nothing
                            gCOl = gvPMView.Columns(data.Item(1).ToString())
                            If gCOl Is Nothing Then

                                Dim counterFilter As String = ""
                                For Each col As GridColumn In gvPMView.Columns
                                    counterFilter = counterFilter & ", [" & col.FieldName & "]"
                                Next

                                If singleCounterDropped = True Then
                                    counterFilter = counterFilter & IIf(counterFilter.Contains("[" & data.Item(1).ToString() & "]"), "", ", [" & data.Item(1).ToString() & "]")
                                Else
                                    For iCntr As Integer = 0 To mm(1).Count - 1
                                        counterFilter = counterFilter & IIf(counterFilter.Contains("[" & mm(1).Item(iCntr)(1).ToString() & "]"), "", ", [" & mm(1).Item(iCntr)(1).ToString() & "]")
                                    Next
                                End If
                                counterFilter = counterFilter.TrimStart(", ")
                                CountersTakenFromGrid = counterFilter

                                MeasurementToGrid(data.Item(0).ToString(), cmbVendor.SelectedItem.ToString, Val(txtQueryBatchSize.Text), counterFilter)
                            Else
                                gCOl.Visible = True
                            End If
                        End If
                    End If
                End If
            End If
            e.Effect = DragDropEffects.None
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Function GetCounters() As String
        Dim counterFilter As String = ""
        Dim dtCountersForMeasurement As DataTable = Nothing

        If chkSearchAllParameter.Checked = True Then
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
            Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)

            For Each dr As DataRow In dtCounterList.Select("MMNAME = '" & data.Item(0) & "'", "Constraint_Type DESC")
                counterFilter = counterFilter & ", [" & dr(1).ToString & "]"
            Next
        Else
            For iRow As Integer = 0 To dtCounterList.Rows.Count - 1
                counterFilter = counterFilter & ", [" & dtCounterList.Rows(iRow)(1).ToString & "]"
            Next
        End If

        counterFilter = counterFilter.TrimStart(", ")
        Return counterFilter
    End Function

    Private Sub btnGetData_Click(sender As Object, e As EventArgs) Handles btnGetData.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
            Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
            If data IsNot Nothing Then
                MeasurementToGrid(data.Item(0), cmbVendor.SelectedItem.ToString, Val(txtQueryBatchSize.Text))
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            QueryOffset = 0
            currViewRowFilter = ""
            currViewSortStr = ""

            gvPMView.Columns.Clear()
            tvObjectTree.Nodes.Clear()
            cmbTargetObject.SelectedIndex = 0
            lblMeasurementName.Text = String.Empty
            lblMeasurementName.Tag = Nothing
            tlvFilters.Nodes.Clear()
            gcPMView.DataSource = Nothing
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cmbTargetObject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTargetObject.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If (cmbTargetObject.SelectedIndex > 0) Then
                AddHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
                Dim strCols As String = Nothing
                If Not cmbTargetObject.SelectedItem Is Nothing Then
                    Dim view As New DataView(dtMmPKColList)
                    view.Sort = "ORDINAL_POSITION DESC"
                    view.RowFilter = "COLUMN_NAME<>'PERIOD_START_TIME'"
                    Dim dtTemp As DataTable = view.ToTable()
                    Dim selectedColOrdinal As Integer = cmbTargetObject.SelectedItem.Tag

                    For Each dtRow As DataRow In dtTemp.Rows
                        If CInt(dtRow.Item(1)) <= selectedColOrdinal Then
                            strCols = strCols & ", " & dtRow.Item(0)
                        End If
                    Next
                End If
                strCols = strCols.TrimStart(",")
                LoadObjectTreeFromMeasurement(strCols, "%")
            Else
                Count_OT = 0
                If dtCheckedObjs IsNot Nothing Then
                    dtCheckedObjs.Rows.Clear()
                End If
                lblTreeObjectsCount.Text = "#: " & CStr(Count_OT)
                tvObjectTree.Nodes.Clear()
                tvObjectTree.Columns.Clear()
                RemoveHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub txtSearchPH_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchPH.KeyUp
        Try
            If dtCounterList IsNot Nothing Then
                If txtSearchPH.Text.Length > 2 Then
                    dtCounterList.DefaultView.RowFilter = "COLNAME LIKE '%" & txtSearchPH.Text & "%'"
                Else
                    dtCounterList.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtSearchMM_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearchMM.KeyUp
        Try
            If dtMmList IsNot Nothing Then
                If (txtSearchMM.Text.Length > 2) Then
                    dtMmList.DefaultView.RowFilter = "MeasurementName Like '%" + txtSearchMM.Text + "%'"
                Else
                    dtMmList.DefaultView.RowFilter = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlParameterList_MouseMove(sender As Object, e As MouseEventArgs) Handles tlCounterList.MouseMove
        Try
            If (e.Button = MouseButtons.Left) Then
                If tlCounterList.Selection.Count = 1 Then
                    Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = tlCounterList.FocusedNode
                    Dim data As DataRowView = tlCounterList.GetDataRecordByNode(node)
                    If data IsNot Nothing Then
                        Dim obj() As Object = {"Counter2Grid", data}
                        tlCounterList.DoDragDrop(obj, DragDropEffects.Copy)
                    End If
                Else
                    Dim selectedNodes = tlCounterList.Selection
                    Dim nodesData As New List(Of DataRowView)
                    For Each nd As DevExpress.XtraTreeList.Nodes.TreeListNode In selectedNodes
                        Dim data As DataRowView = tlCounterList.GetDataRecordByNode(nd)
                        nodesData.Add(data)
                    Next
                    Dim obj() As Object = {"Counter2Grid", nodesData}
                    tlCounterList.DoDragDrop(obj, DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tlParameterList_DragOver(sender As Object, e As DragEventArgs) Handles tlCounterList.DragOver, lstViewMeasurement.DragOver, gcPMView.DragOver, tlvFilters.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub lstParamFilterDragged_MouseDown(sender As Object, e As MouseEventArgs)
        Try
            p = Nothing
            Dim lstCtrl As DevExpress.XtraEditors.CheckedListBoxControl = TryCast(sender, DevExpress.XtraEditors.CheckedListBoxControl)
            If (lstCtrl IsNot Nothing) Then
                p = New Point(e.X, e.Y)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDump2Csv_Click(sender As Object, e As EventArgs) Handles btnDump2Csv.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtQuery As New DataTable
            Dim sqlQuery As String = Nothing

            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Comma Delimited|*.csv"
            objFileDlg.Title = "Save a CSV File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    Dim objCsvExportOption As New dlgCsvExportOption()
                    objCsvExportOption.ShowDialog()

                    If objCsvExportOption.DialogResult = DialogResult.OK Then

                        WaitScreen.ShowWaitScreen("Exporting to CSV...")
                        Application.DoEvents()

                        Dim strConnection As String
                        Dim sqlParam As String
                        Dim startTime As Date = dePMViewStart.EditValue
                        Dim endTime As Date = dePMViewEnd.EditValue

                        Dim counterList As String = Nothing
                        If CountersTakenFromGrid = "" Then
                            counterList = GetCounters()
                        Else
                            counterList = CountersTakenFromGrid
                        End If

                        Dim objList As New List(Of KeyValuePair(Of String, List(Of String)))
                        Dim filterQry As String = Nothing
                        GetSelectedNodes(objList)  'tvObjectTree.Nodes,

                        For Each itm As KeyValuePair(Of String, List(Of String)) In objList
                            If filterQry IsNot Nothing Then
                                filterQry = filterQry & " AND "
                            Else
                                filterQry = " WHERE "
                            End If
                            filterQry = filterQry & itm.Key & " IN ("
                            For Each itm1 As String In itm.Value
                                filterQry = filterQry & "''" & itm1 & "'',"
                            Next
                            filterQry = filterQry.TrimEnd(",") & ")"
                        Next

                        If counterFilterString <> "" Then
                            filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & counterFilterString
                        End If

                        'If _customFilter IsNot Nothing AndAlso _customFilter <> "" Then
                        '    filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & _customFilter.Replace("'", "''")
                        'End If

                        Dim startTimeString As String = startTime.ToString("yyyy-MM-dd HH:mm:ss")
                        Dim endTimeString As String = endTime.ToString("yyyy-MM-dd HH:mm:ss")
                        Dim periodFilterQry As String = " Period_Start_Time >= ''" & startTimeString & "''  And  Period_Start_Time <= ''" & endTimeString & "''"
                        filterQry = IIf(filterQry Is Nothing, " WHERE ", filterQry & " AND ") & periodFilterQry
                        filterQry = IIf(currViewRowFilter <> "", filterQry & " AND " & currViewRowFilter.Replace("'", "''"), filterQry)

                        FilterQueryForDistinctDataColumn = filterQry
                        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                        Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)

                        Dim parray()() As String = {
                            New String() {"@vendorName", Chr(39) & cmbVendor.SelectedItem.ToString & Chr(39)},
                            New String() {"@measurement", Chr(39) & data.Item(0).ToString & Chr(39)},
                            New String() {"@param", Chr(39) & counterList & Chr(39)},
                            New String() {"@n", 0},
                            New String() {"@m", 0},
                            New String() {"@filter", Chr(39) & filterQry & Chr(39)},
                            New String() {"@sortExpr", IIf(String.IsNullOrEmpty(currViewSortStr), "NULL", Chr(39) & currViewSortStr & Chr(39))}
                        }

                        strConnection = GetSQL(3703, parray)(0)
                        sqlParam = GetSQL(3703, parray)(1)
                        dtQuery = DataLibrary.DataAccessorODBC.GetDataTable(strConnection, sqlParam, iQryTimeOut)
                        sqlQuery = dtQuery.Rows(0)("SqlQuery")

                        If sqlQuery IsNot Nothing Then
                            ExportDataToCSV(sqlQuery, objFileDlg.FileName, objCsvExportOption.fileDelimiter)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub ExportDataToCSV(sqlQuery As String, fileName As String, fileDelimiter As String)
        Dim connArr() As String = GetIOSConnection(2000)
        Dim connString As String = GetDecryptedConnectionString(connArr(1))

        Using sourceConnection As SqlConnection = New SqlConnection(connString)
            sourceConnection.Open()
            Dim commandSourceData As SqlCommand = New SqlCommand(sqlQuery, sourceConnection)
            commandSourceData.CommandTimeout = 1000
            'Dim datareader As SqlDataReader = commandSourceData.ExecuteReader()

            Dim bufferSize = 1024 * 1024 '1Mb

            If File.Exists(fileName) Then
                File.Delete(fileName)
            End If

            Using FileObject As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bufferSize)
                Using StreamWriterObj As New StreamWriter(FileObject)
                    Using dataReader As SqlDataReader = commandSourceData.ExecuteReader()
                        Dim FieldCount As Integer = dataReader.FieldCount - 1

                        StreamWriterObj.Write(String.Format("{0}", dataReader.GetName(0)))
                        For i = 1 To FieldCount
                            StreamWriterObj.Write(fileDelimiter)
                            StreamWriterObj.Write(String.Format("{0}", dataReader.GetName(i)))
                        Next
                        StreamWriterObj.WriteLine()

                        Do While dataReader.Read()
                            StreamWriterObj.Write(dataReader.Item(0))
                            For i = 1 To FieldCount
                                StreamWriterObj.Write(fileDelimiter)
                                StreamWriterObj.Write(dataReader.Item(i))
                            Next
                            StreamWriterObj.WriteLine()
                        Loop
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnDump2Xls_Click(sender As Object, e As EventArgs) Handles btnDump2Xls.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Dim dtExport As New DataTable

            Dim objFileDlg As New SaveFileDialog()
            objFileDlg.Filter = "Excel Workbook |*.xlsx"
            objFileDlg.Title = "Save an excel File"

            If objFileDlg.ShowDialog() = DialogResult.OK Then
                If objFileDlg.FileName <> "" Then

                    WaitScreen.ShowWaitScreen("Querying Data...")
                    Application.DoEvents()

                    Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                    Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
                    If gcPMView.Tag = "MeasurementData" Or gcPMView.Tag Is Nothing Or gcPMView.Tag = "" Then
                        If data IsNot Nothing Then
                            dtExport = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, 0, 0, GetCounters(), currViewRowFilter)
                        End If
                    End If

                    WaitScreen.CloseWaitScreen()

                    If dtExport.Rows.Count > 0 Then
                        WaitScreen.ShowWaitScreen("Exporting to Excel...")
                        Application.DoEvents()

                        ExportDataTableToExcel_Stream(dtExport, objFileDlg.FileName)

                        WaitScreen.CloseWaitScreen()
                    End If
                End If
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            WaitScreen.CloseWaitScreen()
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub chkSearchAllParameter_CheckedChanged(sender As Object, e As EventArgs) Handles chkSearchAllParameter.CheckedChanged
        If chkSearchAllParameter.Checked Then
            GetListOfCounters(cmbVendor.SelectedItem.ToString, "", True)
        Else
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
            Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
            Dim MMList As String = ""
            If data IsNot Nothing Then
                MMList = data.Item(0).ToString
            End If
            GetListOfCounters(cmbVendor.SelectedItem.ToString, MMList, False)
        End If
    End Sub

    Private Sub tlCounterList_FocusedNodeChanged(sender As Object, e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles tlCounterList.FocusedNodeChanged
        Try
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = tlCounterList.FocusedNode
            Dim data As DataRowView = tlCounterList.GetDataRecordByNode(node)

            If data IsNot Nothing Then
                lstViewMeasurement.SetFocusedNode(lstViewMeasurement.FindNodeByFieldValue("MeasurementName", data.Item(0).ToString))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ceLoadObjectTree_CheckedChanged(sender As Object, e As EventArgs) Handles ceLoadObjectTree.CheckedChanged
        Try
            If ceLoadObjectTree.Checked Then
                cmbTargetObject.Enabled = True
                tvObjectTree.Enabled = True

                Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
                If data IsNot Nothing Then
                    LoadObjectTypeCombo(data.Item(0).ToString)
                End If
            Else
                cmbTargetObject.Enabled = False
                tvObjectTree.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbPredefTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPmViewPreDef.SelectedIndexChanged
        Dim cmb As DevExpress.XtraEditors.ComboBoxEdit = CType(sender, DevExpress.XtraEditors.ComboBoxEdit)
        If cmb.SelectedIndex > 0 Then
            Dim dr() As DataRow = dtPredefinePeriod.Select("PredefinedPeriodID = " & TryCast(cmb.SelectedItem, clsComboBoxItem).Value & " And Control='" & cmb.Name & "'")
            If Not dr Is Nothing Then
                If dr.Count > 0 Then
                    Dim SQL As String = dr(0)("SQL").ToString
                    Dim dtPeriod As New DataTable
                    dtPeriod = IOS.DataLibrary.DataAccessorODBC.GetDataTable(connStrIOSServer, SQL)
                    If dtPeriod IsNot Nothing AndAlso dtPeriod.Rows.Count > 0 Then
                        dePMViewStart.EditValue = dtPeriod.Rows(0)(0)
                        dePMViewEnd.EditValue = dtPeriod.Rows(0)(1)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub tvObjectTree_FilterCheckVisible(ByRef nds As TreeListNodes)
        Try
            For Each nd As TreeListNode In nds
                If nd.HasChildren Then
                    tvObjectTree_FilterCheckVisible(nd.Nodes)
                End If
                If nd.Visible = True Then
                    nd.Checked = True
                Else
                    nd.Checked = False
                End If
            Next
        Catch
        End Try
    End Sub

    Private Sub tvObjectTree_NodeChanged(sender As Object, e As NodeChangedEventArgs)
        Try
            RemoveHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
            If e.ChangeType = DevExpress.XtraTreeList.NodeChangeTypeEnum.CheckedState Then
                If e.Node.CheckState = CheckState.Checked Then
                    If tvObjectTree.FindFilterText <> "" Then
                        tvObjectTree_FilterCheckVisible(e.Node.Nodes)
                    Else
                        e.Node.CheckAll()
                    End If
                    FillCheckedObjectsIntoList(e.Node)
                Else
                    e.Node.UncheckAll()
                    RemoveUncheckedObjectFromList(e.Node)
                End If
                tvObjectTree.CheckParentNode(e.Node)
            End If
            lblTreeObjectsCount.Text = "#: " & CStr(Count_OT)
        Catch
        Finally
            AddHandler tvObjectTree.NodeChanged, AddressOf tvObjectTree_NodeChanged
        End Try
    End Sub

#End Region

#Region "Context Menu Code"

    Dim cm_OT_SourceControl As System.Windows.Forms.Control

    Private Sub cmObjectTree_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmObjectTree.Opening
        Try
            Dim targetObject As String = Nothing
            'Dim tech As String = tvObjectTree.Tag
            Dim countchecked As Integer = 0
            targetObject = cmbTargetObject.SelectedItem.ToString

            'count checked boxes
            countchecked = tvObjectTree.GetEndCheckedNodes().Count 'TreeView_CountCheckedNodes(tvObjectTree.Nodes(0))

            'enable/disable copy
            If countchecked > 0 Then
                cm_OT_tsmi_copy.Text = "Copy - Objects: " & countchecked
                cm_OT_tsmi_copy.Enabled = True
            Else
                cm_OT_tsmi_copy.Text = "Copy"
                cm_OT_tsmi_copy.Enabled = False
            End If

            'check clipboard
            Dim s As String = Clipboard.GetText()                  'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim i, j As Integer
            If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                cm_OT_tsmi_paste.Text = "Paste - Objects: ?"
                cm_OT_tsmi_paste.Enabled = True
            Else
                Dim clipboardmatches As Integer = 0
                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                    For j = 0 To bufferCell.Length - 1
                        If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                            bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                        End If
                        If bufferCell(j).Trim <> "" Then
                            If Not Treelist_TextSearch(bufferCell(j).Trim, tvObjectTree.Nodes, True, cmbTargetObject.SelectedItem.ToString) Is Nothing Then
                                clipboardmatches = clipboardmatches + 1
                            End If
                        End If
                    Next
                Next

                'enable/disable paste
                If clipboardmatches > 0 Then
                    cm_OT_tsmi_paste.Text = "Paste - Objects: " & clipboardmatches
                    cm_OT_tsmi_paste.Enabled = True
                Else
                    cm_OT_tsmi_paste.Text = "Paste"
                    cm_OT_tsmi_paste.Enabled = False
                End If
                tvObjectTree.Cursor = Cursors.Arrow
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cm_OT_tsmi_copy_Click(sender As Object, e As EventArgs) Handles cm_OT_tsmi_copy.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Clipboard.Clear()
        Dim tgtObject As String = Nothing
        Try
            tgtObject = cmbTargetObject.SelectedItem.ToString
            Dim copystring As String = TreeList_Checked2String(cmbVendor.SelectedItem.ToString() & " " & cmbTechnology.SelectedItem.ToString(), tgtObject, "NewLine", tvObjectTree, cmbTargetObject, cmbTargetObject.SelectedItem.ToString)
            If Not copystring Is Nothing Or copystring <> "" Then
                Clipboard.SetText(copystring)
            End If
            copystring = Nothing
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_OT_tsmi_paste_Click(sender As Object, e As EventArgs) Handles cm_OT_tsmi_paste.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Dim aggr_to As String = Nothing
        tvObjectTree.Cursor = Cursors.WaitCursor
        Try
            aggr_to = cmbTargetObject.SelectedItem.ToString
            Dim ExactMatch As Boolean = True
            ExactMatch = True

            Dim s As String = Clipboard.GetText()                   'Get clipboard data as a string
            Dim rows() As String = s.Split(ControlChars.NewLine)    'Split into rows
            Dim i, j As Integer
            Dim clipboardmatches As Integer = 0
            Dim mbresult As MsgBoxResult = MsgBoxResult.Ok

            If s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length > 100 Then
                mbresult = MsgBox("An estimated " & s.Split(ControlChars.Tab).Length * s.Split(ControlChars.NewLine).Length & " strings on clipboard are detected. Selection can take long. Do you wish to continue selection?", MsgBoxStyle.OkCancel)
            End If

            If mbresult = MsgBoxResult.Ok Then
                For i = 0 To rows.Length - 1
                    'Split row into cells
                    Dim bufferCell() As String = rows(i).Split(ControlChars.Tab)
                    For j = 0 To bufferCell.Length - 1
                        If bufferCell(j).ToString.Contains(ControlChars.Lf) Then
                            bufferCell(j) = bufferCell(j).ToString.Replace(ControlChars.Lf, "")
                        End If
                        Dim tv_result As TreeListNode = Treelist_TextSearch(bufferCell(j).Trim, tvObjectTree.Nodes, ExactMatch, cmbTargetObject.SelectedItem.ToString)
                        If Not tv_result Is Nothing Then
                            If tv_result.Checked = False Then
                                tv_result.Checked = True
                            End If
                        End If
                    Next
                Next
            End If
            tvObjectTree.Cursor = Cursors.Arrow
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub cm_OT_tsmi_CheckChilds_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cm_OT_tsmi_CheckChilds.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            DevEx_ObjectTree_CheckChild(tvObjectTree.Nodes(0))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_OT_UnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_OT_UnCheck.Click
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Invoked")
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            DevEx_TreeView_ClearChecks(tvObjectTree.Nodes(0))
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
        UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Info", "Completed")
    End Sub

    Private Sub tsmi_AllowCellCopy_CheckedChanged(sender As Object, e As EventArgs) Handles tsmi_AllowCellCopy.CheckedChanged
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            If tsmi_AllowCellCopy.Checked Then
                gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            Else
                gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_CopySelection_Click(sender As Object, e As EventArgs) Handles tsmi_CopySelectionWOHeader.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, False)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_CopyFilteredToClipboard_Click(sender As Object, e As EventArgs) Handles tsmi_CopyFilteredToClipboard.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
            Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(node)
            If data IsNot Nothing Then
                Using dtTemp As DataTable = CreateData(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, 0, 0, GetCounters(), currViewRowFilter)
                    If dtTemp IsNot Nothing Then
                        IOSDevExpressGrid.PopulateDataInGrid(gcTemp, gvTemp, dtTemp, "ALL")
                    End If
                End Using
            End If
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(gcTemp, gvTemp, True, True)
            IOS.Library.IOSDevExpressGrid.ClearGrid(gcTemp)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_CopySelectionWithHeader_Click(sender As Object, e As EventArgs) Handles tsmi_CopySelectionWithHeader.Click
        Try
            Dim tempGrid As DevExpress.XtraGrid.GridControl = frmMapWindow.GetAttachedGrid(sender)
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = tempGrid.MainView
            IOS.Library.IOSDevExpressGrid.CopyGridDataToClipBoard(tempGrid, gridView, False, True)
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub tsmi_ClearAllObjs_Click(sender As Object, e As EventArgs) Handles tsmi_ClearAllObjs.Click
        Try
            lstTreeObjects.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstTreeObjects.ItemCount > 0 Then
                dtCheckedObjs.Rows.Clear()
                RefreshObjectsInListBox()

                Count_OT = 0
                lblTreeObjectsCount.Text = "#: " & Count_OT.ToString
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            lstTreeObjects.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub tsmi_DeleteObjs_Click(sender As Object, e As EventArgs) Handles tsmi_DeleteObjs.Click
        Try
            lstTreeObjects.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If lstTreeObjects.ItemCount > 0 Then
                Dim drList As New List(Of DataRow)
                For Each selItm As Object In lstTreeObjects.SelectedItems
                    Dim dr As DataRow = dtCheckedObjs.AsEnumerable().Where(Function(x) x.Field(Of String)("ObjectName") = DirectCast(selItm, DataRowView).Row.Item(0))(0)
                    drList.Add(dr)
                Next

                For Each dr In drList
                    'UncheckTreeNode_TextSearch(dr("ObjectName"), tvObjectTree.Nodes, True)
                    dtCheckedObjs.Rows.Remove(dr)
                Next

                Count_OT = lstTreeObjects.ItemCount
                lblTreeObjectsCount.Text = "#: " & Count_OT.ToString

            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        Finally
            lstTreeObjects.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Function UncheckTreeNode_TextSearch(SearchString As String, Nds As TreeNodeCollection, Optional ExactMatch As Boolean = False) As TreeNode
        Try
            Dim ret As TreeNode
            For Each tn As TreeNode In Nds
                If ExactMatch = True Then
                    If tn.Text.ToLower = SearchString.ToLower AndAlso tn.Checked = True Then
                        tn.Checked = False
                    End If
                Else
                    If tn.GetString().IndexOf(SearchString) <> -1 Then
                        tn.Checked = False
                    End If
                End If

                If tn.Nodes.Count > 0 Then
                    ret = UncheckTreeNode_TextSearch(SearchString, tn.Nodes, ExactMatch)
                    If Not ret Is Nothing Then
                        ret.Checked = False
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Private Sub cmSelectedObjs_Opening(sender As Object, e As CancelEventArgs) Handles cmSelectedObjs.Opening
        Try
            If lstTreeObjects.ItemCount = 0 Then
                tsmi_ClearAllObjs.Enabled = False
                tsmi_DeleteObjs.Enabled = False
            Else
                tsmi_ClearAllObjs.Enabled = True
                tsmi_DeleteObjs.Enabled = True
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

#End Region

#Region "Filter"

    Public Sub FiltersInitialize()
        Dim col1 As TreeListViewColumn = New TreeListViewColumn("Counter", "")
        Dim col2 As TreeListViewColumn = New TreeListViewColumn("Op", "")
        Dim col3 As TreeListViewColumn = New TreeListViewColumn("Value", "")
        col1.Width = 180
        col2.Width = 30
        col3.Width = 80
        Try
            If tlvFilters.Columns.Count = 0 Then
                tlvFilters.Columns.Clear()
                tlvFilters.Columns.Add(col1)
                tlvFilters.Columns.Add(col2)
                tlvFilters.Columns.Add(col3)
            End If
        Catch ex As Exception
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & ex.Message)
        End Try

        tlvFilters.AllowDrag = True
        tlvFilters.AllowDrop = True
        tlvFilters.DragDropMode = LidorSystems.IntegralUI.DragDropMode.Custom
        tlvFilters.LabelEdit = True
    End Sub

    Private Sub tlvFilters_AfterLabelEdit(sender As Object, e As LidorSystems.IntegralUI.ObjectEditEventArgs) Handles tlvFilters.AfterLabelEdit
        Try
            Dim tlv As TreeListView = DirectCast(sender, TreeListView)
            If TypeOf e.[Object] Is TreeListViewSubItem Then
                Dim node As TreeListViewSubItem = DirectCast(e.[Object], TreeListViewSubItem)
                If e.Label IsNot Nothing Then
                    If node.Index = 0 Then
                        e.Cancel = True
                        XtraMessageBox.Show("Not allowed to change counter name!", "Node Label Edit")
                    ElseIf node.Index = 1 Then
                        If Not e.Label.IndexOfAny(New Char() {">"c, "<"c, "="c}) >= 0 Then
                            ' Cancel the label edit action, inform the user, and
                            ' place the node in edit mode again.

                            e.Cancel = True
                            XtraMessageBox.Show("The valid characters are '<','>','<>','='", "Node Label Edit")
                            node.BeginEdit()
                        Else
                            counterFilterString = ApplyCounterFilters(node, e.Label.ToString, node.Parent.SubItems(2).Text)
                            If gcPMView.Tag = "MeasurementData" Then
                                Dim counterFilter As String = GetCounters()
                                Dim nde As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                                Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(nde)
                                If data IsNot Nothing Then
                                    MeasurementToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, Val(txtQueryBatchSize.Text), counterFilter)
                                End If
                            End If
                        End If
                    ElseIf node.Index = 2 Then
                        If Not IsNumeric(e.Label) And (node.Parent.SubItems(1).Text = ">"c Or node.Parent.SubItems(1).Text = "<"c) Then
                            e.Cancel = True
                            'XtraMessageBox.Show(Invalid input, use numerics!", "Node Label Edit")
                            node.BeginEdit()
                        Else
                            counterFilterString = ApplyCounterFilters(node, node.Parent.SubItems(1).Text, e.Label.ToString)
                            If gcPMView.Tag = "MeasurementData" Then
                                Dim counterFilter As String = GetCounters()
                                Dim nde As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
                                Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(nde)
                                If data IsNot Nothing Then
                                    MeasurementToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, Val(txtQueryBatchSize.Text), counterFilter)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            e.Cancel = False
            tlv.ResumeUpdate()
            tlv.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Function ApplyCounterFilters(Optional ByRef node As TreeListViewSubItem = Nothing, Optional op As String = Nothing, Optional value As String = Nothing) As String
        counterFilterString = ""
        'CType(gcPMView.DataSource, DataTable).DefaultView.RowFilter = ""

        If node IsNot Nothing Then
            If IsNumeric(value) Then
                counterFilterString += "[" & node.Parent.SubItems(0).Text & "]" & " " & op & " " & value & " And "
            Else
                counterFilterString += "[" & node.Parent.SubItems(0).Text & "]" & " " & op & " '" & value & "' And "
            End If
        End If

        tlvFilters.Update()
        For Each nd As TreeListViewNode In tlvFilters.Nodes
            If node IsNot Nothing AndAlso nd.SubItems(0).Text = node.Parent.SubItems(0).Text Then Continue For
            If IsNumeric(nd.SubItems(2).Text) Then
                counterFilterString += "[" & nd.SubItems(0).Text & "]" & " " & nd.SubItems(1).Text & " " & nd.SubItems(2).Text & " And "
            Else
                counterFilterString += "[" & nd.SubItems(0).Text & "]" & " " & nd.SubItems(1).Text & " '" & nd.SubItems(2).Text & "' And "
            End If
        Next
        tlvFilters.Refresh()

        If counterFilterString <> "" Then
            counterFilterString = counterFilterString.Substring(0, counterFilterString.Length - 5)
            ''If gcPMView.DataSource IsNot Nothing Then
            ''CType(gcPMView.DataSource, DataTable).DefaultView.RowFilter = counterFilterString
            ''gcPMView.RefreshDataSource()
            ''End If
        End If
        Return counterFilterString
    End Function

    Private Sub tlvFilters_DragDrop(sender As Object, e As DragEventArgs) Handles tlvFilters.DragDrop
        Try

            If e.Data.GetDataPresent("System.Object[]") Then
                If (e.Effect = DragDropEffects.Copy) Or (e.Effect = DragDropEffects.Move) Then
                    Dim tlv As TreeListView = DirectCast(sender, TreeListView)
                    Dim mm() As Object = e.Data.GetData("System.Object[]")
                    If mm IsNot Nothing Then
                        Dim data As DataRowView = CType(mm(1), DataRowView)
                        tlv_ParamFilters_Add(tlv, data.Item(1).ToString())
                    End If
                End If
            End If
            'ApplyCounterFilters()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlvFilters_KeyDown(sender As Object, e As KeyEventArgs) Handles tlvFilters.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim tlv As TreeListView = DirectCast(sender, TreeListView)
                Dim node As TreeListViewNode = tlv.SelectedNode
                If Not node Is Nothing Then
                    tlv.Nodes.RemoveAt(node.Index)
                End If
            End If
            counterFilterString = ApplyCounterFilters()
            Dim mmNode As DevExpress.XtraTreeList.Nodes.TreeListNode = lstViewMeasurement.FocusedNode
            Dim data As DataRowView = lstViewMeasurement.GetDataRecordByNode(mmNode)
            If data IsNot Nothing Then
                MeasurementToGrid(data.Item(0).ToString, cmbVendor.SelectedItem.ToString, Val(txtQueryBatchSize.Text))
            End If
        Catch
        End Try
    End Sub

    Private Sub tlv_ParamFilters_Add(ByRef tlv As TreeListView, ByVal item As String)
        Try
            For Each nd As TreeListViewNode In tlv.Nodes
                If (nd.SubItems(0).Text = item.ToString) Then
                    Exit Sub
                End If
            Next

            tlv.LabelEdit = True
            Dim newnode As TreeListViewNode = New TreeListViewNode(item)
            newnode.Tag = item
            Dim si1 As TreeListViewSubItem = New TreeListViewSubItem(item)
            newnode.SubItems.Add(si1)
            Dim si2 As TreeListViewSubItem = New TreeListViewSubItem("=")

            newnode.SubItems.Add(si2)
            Dim si3 As TreeListViewSubItem = New TreeListViewSubItem("100")

            newnode.SubItems.Add(si3)
            tlv.Nodes.Add(newnode)

            tlv.Columns(0).Width = 180
            tlv.ResumeUpdate()
            tlv.Refresh()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmsCurrentData_Opening(sender As Object, e As CancelEventArgs) Handles cmsCurrentData.Opening
        Try
            Dim dgv As DevExpress.XtraGrid.GridControl = CType(sender.sourcecontrol, DevExpress.XtraGrid.GridControl)
            tsmiRecordCount.Text = "Record Count: " & dgv.DefaultView.RowCount

            If String.IsNullOrEmpty(currViewRowFilter) Then
                tsmi_CopyFilteredToClipboard.Enabled = False
            Else
                tsmi_CopyFilteredToClipboard.Enabled = True
            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class